using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Neusoft.HISFC.BizProcess.Interface.Common;

namespace HISTIMEJOB
{
    /// <summary>
    /// <br></br>
    /// [功能描述: 医保定时上传]<br></br>
    /// [创 建 者: ]<br></br>
    /// [创建时间: 2010-04-08]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class FinSi : Neusoft.FrameWork.Management.Database, IJob
    {
        #region "变量"
        //服务器时间
        public DateTime dtServerDateTime;
        //待处理患者集合
        public ArrayList alPatientInfo = new ArrayList();
        public string JobArg = string.Empty;

        //显示的文本框,引用主窗口的文本框
        public Neusoft.FrameWork.WinForms.Controls.NeuRichTextBox rtbLogo = null;
        //费用管理类		
        public Neusoft.HISFC.BizLogic.Fee.InPatient feeInpatient = new Neusoft.HISFC.BizLogic.Fee.InPatient();

        public Neusoft.HISFC.BizLogic.Fee.Interface feeInterface = new  Neusoft.HISFC.BizLogic.Fee.Interface();
        //患者管理类
        private Neusoft.HISFC.BizLogic.RADT.InPatient radtInpatient = new Neusoft.HISFC.BizLogic.RADT.InPatient();
        #endregion



        #region "函数"
        #region 更新住院主表费用
        /// <summary>
        /// 更新住院主表费用
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int UpdateInMainInfoCost(Neusoft.HISFC.Models.RADT.PatientInfo patient)
        {
            #region 
            string strSql = " UPDATE fin_ipr_inmaininfo" +
                               " SET TOT_COST = '{1}'," +
                               " OWN_COST = '{2}'," +
                               " PAY_COST = '{3}'," +
                               " PUB_COST = '{4}'," +
                               " FREE_COST = '{5}'" +
                             " WHERE inpatient_no = '{0}'" +
                               " AND in_state in ('I', 'B', 'R', 'C')";
            try
            {
                strSql = string.Format(strSql,
                                              patient.ID,
                                              patient.SIMainInfo.TotCost.ToString(),
                                              patient.SIMainInfo.OwnCost.ToString(),
                                              patient.SIMainInfo.PayCost.ToString(),
                                              patient.SIMainInfo.PubCost.ToString(),
                                              (patient.FT.PrepayCost - patient.SIMainInfo.OwnCost).ToString());
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                string logoText = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " 更新主表[" + patient.ID.ToString() + "]：" + strSql + "\r\n";
                System.IO.TextWriter output = System.IO.File.AppendText("UpdateMainInfoLogo.txt");
                output.WriteLine(logoText);
                output.Close();
            }
            catch { }

      
            #endregion
            return this.ExecNoQuery(strSql);
        }
        #endregion
        /// <summary>
        /// 获取占用床位列表
        /// </summary>
        /// <returns>null失败</returns>
        public ArrayList GetPatientInfos(string pactCode)
        {
            ArrayList allPatientInfos = new ArrayList();
            string strSql = "SELECT B.INPATIENT_NO,B.IN_STATE,B.* FROM FIN_IPR_INMAININFO B WHERE B.PACT_CODE = '{0}' AND B.IN_STATE IN ('B','I') AND B.TOT_COST > 0";
          
            strSql = string.Format(strSql, pactCode);
            if (Reader != null && Reader.IsClosed == false)
                Reader.Close();

            if (this.ExecQuery(strSql) == -1) return null;
            while (this.Reader.Read())
            {
                Neusoft.HISFC.Models.RADT.PatientInfo patientInfo = new Neusoft.HISFC.Models.RADT.PatientInfo();
                patientInfo.ID = Reader[0].ToString();
                allPatientInfos.Add(patientInfo);
            }
            this.Reader.Close();
            return allPatientInfos;
        }

        /// <summary>
        /// 固定费用收取
        /// </summary>
        /// <returns>1 成功 －1 失败</returns>
        public int reCharge()
        {
            try
            {
                //服务器时间
                dtServerDateTime = this.GetDateTimeFromSysDateTime();

                if (dtServerDateTime == DateTime.MinValue)
                {
                    return -1;
                }
                string[] pacts = this.JobArg.Split('|');
                for (int j = 0; j < pacts.Length ; j++)
                {
                    //获取占床全部数据集合
                    this.alPatientInfo = this.GetPatientInfos(pacts[j].ToString().Trim());
                    if (alPatientInfo == null)
                    {
                        WriteErr();
                        return -1;//如果没有取到占床列表则返回
                    }

                    for (int i = 0; i < alPatientInfo.Count; i++)
                    {
                        Neusoft.HISFC.Models.RADT.PatientInfo pi = (Neusoft.HISFC.Models.RADT.PatientInfo)alPatientInfo[i];
                        //判断患者的住院流水号					
                        if (string.IsNullOrEmpty(pi.ID) == true)
                        {
                            this.Err = pi.ID + "床的患者住院流水号为空!";
                            this.WriteErr();
                            continue;
                        }

                        //处理每个患者
                        try
                        {
                            if (SetFeeByPerson(pi, dtServerDateTime) == -1)
                            {
                                continue;
                            }
                        }
                        catch (Exception ex)
                        {
                            this.Err = pi.ID + ex.Message;
                            return -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Err = "定时上传错误!" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return 1;
        }


        /// <summary>
        /// 按患者收取固定费用
        /// </summary>
        /// <param name="bed">床位实体</param>
        /// <param name="operDate">操作时间</param>
        /// <returns>1成功 －1失败</returns>
        public int SetFeeByPerson(Neusoft.HISFC.Models.RADT.PatientInfo pi, DateTime operDate)
        {
            //获取患者基本信息
            Neusoft.HISFC.Models.RADT.PatientInfo patientInfo = this.radtInpatient.QueryPatientInfoByInpatientNO(pi.ID);

            if (JobArg.IndexOf(patientInfo.Pact.ID) < 0)
            {
                this.Err = "这个程序不处理这个合同单位的患者！" + patientInfo.ID;
                this.WriteErr();
                return 1;
            }

            if (patientInfo == null)
            {
                this.Err = pi.ID + this.radtInpatient.Err;
                WriteErr();
                return -1;
            }
            //处理这个科的患者
            if (DoFinSiFee(patientInfo) == -1)
            {
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 查找患者费用明细
        /// </summary>
        /// <param name="patientInfo"></param>
        private int DoFinSiFee(Neusoft.HISFC.Models.RADT.PatientInfo patient)
        {
            /// <summary>
            /// 非药品明细
            /// </summary>
            ArrayList alItemList;

            /// <summary>
            /// 药品明细
            /// </summary>
            ArrayList alMedicineList;

            string errText = "";

            #region 查未上传的费用明细

            //查询
            alItemList = this.feeInpatient.QueryItemListsForBalance(patient.ID);
            if (alItemList == null)
            {
                errText = "查询患者非药品信息出错" + this.feeInpatient.Err;
                return -1;
            }

            alMedicineList = this.feeInpatient.QueryMedicineListsForBalance(patient.ID);
            if (alMedicineList == null)
            {
                errText = "查询患者药品信息出错" + this.feeInpatient.Err;
                return -1;
            }

            ArrayList alFeeItemLists = new ArrayList();
            //添加汇总信息
            foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList  item in alItemList)
            {
                alFeeItemLists.Add(item);
            }

            foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList  medicineList in alMedicineList)
            {
                alFeeItemLists.Add(medicineList);
            }
          

            #endregion
            #region 用医保代理
            try
            {
                #region  加待遇算法支持
                int revInt = -1;
                patient.SIMainInfo.SiEmployeeCode = "HHKO1";
                patient.SIMainInfo.SiEmployeeName = "管理员";


                Neusoft.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy newMedcareInterfaceInstance = new Neusoft.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
                revInt = newMedcareInterfaceInstance.SetPactCode(patient.Pact.ID);
                if (revInt == -1)
                {
                    this.Err = "初始化新待遇算法出错！" + newMedcareInterfaceInstance.ErrMsg;
                    WriteErr();
                    return -1;
                }
                long revLong = newMedcareInterfaceInstance.Connect();
                if (revLong < 0)
                {
                    this.Err = "初始化连接新待遇算法出错！" + newMedcareInterfaceInstance.ErrMsg;
                    WriteErr();
                    return -1;
                }
                #endregion
                Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
             
                ArrayList alForPerBalance = new ArrayList();
                if (newMedcareInterfaceInstance.PreBalanceInpatient(patient, ref alFeeItemLists) < 0)
                {
                    this.Err = "预结算失败：" + newMedcareInterfaceInstance.ErrMsg +"住院流水号" + patient.ID;
                    WriteErr();
                    return -1;
                }
                //更新住院主表
                if (UpdateInMainInfoCost(patient) < 0)
                {
                    this.Err = "预结算更新主表失败：" + newMedcareInterfaceInstance.ErrMsg + "住院流水号" + patient.ID;
                    WriteErr();
                    return -1;
                }

                revLong = newMedcareInterfaceInstance.Commit();
                if (revLong < 0)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    newMedcareInterfaceInstance.Rollback();
                    this.Err = "待遇算法提交数据出错！" + newMedcareInterfaceInstance.ErrMsg + "住院流水号" + patient.ID;
                    WriteErr();
                    return -1;
                }
                else
                {
                    Neusoft.FrameWork.Management.PublicTrans.Commit();
                }
            }
            catch (Exception ex)
            {
                this.Err = "上传，预结失败！" + ex.Message;
                WriteErr();
                return -1;
            }
            #endregion
           
            return 1;
        }

        /// <summary>
        /// 写错误日志
        /// </summary>
        public override void WriteErr()
        {
            this.myMessage = this.Err;
            base.WriteErr();
        }
        #endregion


        #region IJob 成员
        private string myMessage = ""; //传递消息
        public string Message
        {
            // TODO:  添加 calculateFee.Con setter 实现
            get
            {
                return this.myMessage;
            }
        }
        public System.Data.OracleClient.OracleConnection Con
        {
            set
            {
                // TODO:  添加 calculateFee.Con setter 实现
            }
        }

        public int Start()
        {
            // TODO:  添加 calculateFee.Start 实现
            return this.reCharge();
        }
        #endregion

    }
}
