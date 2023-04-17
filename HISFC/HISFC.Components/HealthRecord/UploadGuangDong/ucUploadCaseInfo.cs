using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.HealthRecord.UploadGuangDong
{
    /// <summary>
    /// 集中上传病案信息
    /// </summary>
    public partial class ucUploadCaseInfo : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 
        /// </summary>
        public ucUploadCaseInfo()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.HealthRecord.Base baseMana = new FS.HISFC.BizLogic.HealthRecord.Base();
         
        FS.HISFC.BizLogic.HealthRecord.DeptShift deptShiftMana = new FS.HISFC.BizLogic.HealthRecord.DeptShift();
        FS.HISFC.BizLogic.HealthRecord.Fee healthfeeMana = new FS.HISFC.BizLogic.HealthRecord.Fee();
        FS.HISFC.BizLogic.HealthRecord.Tumour tumourMana = new FS.HISFC.BizLogic.HealthRecord.Tumour();
        FS.HISFC.BizLogic.HealthRecord.Operation operMana = new FS.HISFC.BizLogic.HealthRecord.Operation();
        FS.HISFC.BizLogic.HealthRecord.Diagnose diagnoseMana = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

        FS.HISFC.BizProcess.Integrate.RADT pa = new FS.HISFC.BizProcess.Integrate.RADT();
        FS.HISFC.BizLogic.HealthRecord.Base baseDml = new FS.HISFC.BizLogic.HealthRecord.Base();
        /// <summary>
        /// 错误
        /// </summary>
        string err = "";
        /// <summary>
        /// 已录入，不需要上传的
        /// </summary>
        string errNotNeed = "";

        /// <summary>
        /// SqlID
        /// </summary>
        string sqlID = "Case.CaseInfo.UpLoad";

        /// <summary>
        /// SqlID
        /// </summary>
        [Description("Sql语句ID号"), Category("查询设置")]
        public string SqlID
        {
            get
            {
                return sqlID;
            }
            set
            {
                sqlID = value;
            }
        }

        private bool isAllDeptUsedCaseBase=false;
        /// <summary>
        /// 所有科室都使用了病案首页上传首页内容（使资料全面）
        /// </summary>
        [Description("true 是取sqlid“Case.CaseInfo.UpLoad.MetCasBase”  false 非取sqlid“Case.CaseInfo.UpLoad” "), Category("查询设置")]
        public bool IsAllDeptUsedCaseBase
        {
            get
            {
                return this.isAllDeptUsedCaseBase;
            }
            set
            {
                this.isAllDeptUsedCaseBase = value;
            }
        }
        private bool isDrgs = false;
        /// <summary>
        ///使用最新点子病历（病案首页用电子病历形式实现）
        /// </summary>
        [Description("ture 最新首页（drgs）上传 false 2012年以前版本上传 "), Category("查询设置")]
        public bool IsDrgs
        {
            get
            {
                return this.isDrgs;
            }
            set
            {
                this.isDrgs = value;
            }
        }
        /// <summary>
        /// 病案主表数据 true 是 false 非
        /// </summary>
        private bool isMetCasBase = true;
        /// <summary>
        /// 不需要上传数据的住院次数
        /// </summary>
        private int inTimes = 0;

        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("测试连接", "测试连接", FS.FrameWork.WinForms.Classes.EnumImageList.S设置, true, false, null);
            return toolBarService;
        }
        /// <summary>
        /// 获取需要上传的数据
        /// </summary>
        /// <param name="sendFlag"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public ArrayList GetUploadInpatientNo(string sendFlag, DateTime beginTime, DateTime endTime)
        {
            if (this.checkBox1.Checked == true) //增加一个输入住院号收集数据上传的功能 2011-4-26 chengym
            {
                string patientNo = this.textBox1.Text;
                ArrayList pTrueAl = new ArrayList();
                string[] temp = patientNo.Split(',');
                if (temp.Length > 0)
                {
                    string pEnd = "";
                    string Sqlwhere = "";
                    for (int i = 0; i < temp.Length; i++)
                    {
                        ArrayList alTemp = new ArrayList();
                        //pEnd = "ZY%" + temp[i].PadLeft(10, '0');
                        //Sqlwhere = @" where  inpatient_no like  '{0}'";
                        pEnd = temp[i].PadLeft(10, '0');//居然存在住院流水号跟住院号不同的情况，所以这里改回住院号查询
                        //Sqlwhere = @" where  patient_no ='{0}'  AND (in_state = 'O' or in_state = 'B')";
                        //try
                        //{
                        //    Sqlwhere = string.Format(Sqlwhere, pEnd);
                        //}
                        //catch
                        //{
                        //}
                        FS.HISFC.BizProcess.Integrate.RADT pMgr = new FS.HISFC.BizProcess.Integrate.RADT();
                        alTemp = pMgr.QueryInpatientNoByPatientNo(pEnd);// pMgr.PatientInfoGet(Sqlwhere);//这个函数好像以前没有见过，注意
                        if (alTemp.Count > 0)
                        {
                            foreach (FS.FrameWork.Models.NeuObject p in alTemp)//全部加载进去，上传的时候会通过接口进行判断上传哪一个
                            {
                                FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                                patientInfo.ID = p.ID;
                                patientInfo.PID.PatientNO = pEnd;
                                patientInfo.Name = p.Name;
                                if (p.Memo != "O" && p.Memo!="B")
                                {
                                    continue;
                                }
                                pTrueAl.Add(patientInfo);
                            }
                        }
                    }

                }
                return pTrueAl;
            }
            else //原来默认按照时间段上传的
            {
                string strSQL = "";
                if (this.isAllDeptUsedCaseBase)
                {
                    sqlID = "Case.CaseInfo.UpLoad.MetCasBase";
                    if (this.baseMana.Sql.GetSql(sqlID, ref strSQL) == -1)
                    {
                        MessageBox.Show("获取SQL语句出错[" + sqlID + "]");
                        return null;
                    }
                }
                else
                {
                    if (this.baseMana.Sql.GetSql(sqlID, ref strSQL) == -1)
                    {
                        MessageBox.Show("获取SQL语句出错[" + sqlID + "]");
                        return null;
                    }
                }
                //            string strSQL = @"select inpatient_no,card_no,in_times from fin_ipr_inmaininfo 
                //where INPATIENT_NO='ZY020000024615'
                //order by card_no
                //";
                try
                {
                    strSQL = string.Format(strSQL, sendFlag, beginTime.ToString(), endTime.ToString());
                }
                catch
                {
                    return null;
                }

                this.baseMana.ExecQuery(strSQL);
                ArrayList al = new ArrayList();
                FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

                try
                {
                    while (this.baseMana.Reader.Read())
                    {
                        patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

                        patientInfo.ID = this.baseMana.Reader[0].ToString();
                        patientInfo.PID.PatientNO = this.baseMana.Reader[1].ToString();
                        patientInfo.User01 = this.baseMana.Reader[2].ToString().PadLeft(2, '0');

                        al.Add(patientInfo);
                    }
                    return al;
                }
                catch
                {
                    return null;
                }
                finally
                {
                    this.baseMana.Reader.Close();
                }
            }
        }

        /// <summary>
        /// 取住院信息


        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public FS.HISFC.Models.HealthRecord.Base GetpatientInfo(string inpatientNo)
        {
            FS.HISFC.Models.HealthRecord.Base patientinfo = new FS.HISFC.Models.HealthRecord.Base();
			FS.HISFC.Models.HealthRecord.Base pat = new FS.HISFC.Models.HealthRecord.Base();
			FS.HISFC.Models.HealthRecord.Base pati = new FS.HISFC.Models.HealthRecord.Base();
            //modify 2011-3-24 ch 病案首页全面使用后 2 否则使用 1；
            //patientinfo.PatientInfo = this.pa.QueryPatientInfoByInpatientNO(inpatientNo); //1
            
            patientinfo = this.baseDml.GetCaseBaseInfo(inpatientNo); //2
            if (patientinfo == null || patientinfo.PatientInfo.ID == "") //met_cas_base 获取不到 从 fin_ipr_inmaininfo 里获取
            {
                patientinfo = new FS.HISFC.Models.HealthRecord.Base();
                patientinfo.PatientInfo = this.pa.QueryPatientInfoByInpatientNO(inpatientNo); //1
                this.isMetCasBase = false;
            }
            else
            {
                this.isMetCasBase = true;//2012-1-9 
            }
            //if (patientinfo.CePi == null)
            //{
            //    MessageBox.Show("从主表获取的数据");
            //}
            if (patientinfo.PatientInfo == null || patientinfo.PatientInfo.ID == "")
            {
                return null;
            }
            #region  屏蔽在上传函数中已经做了处理 2012-1-9 chengym
            //FS.HISFC.BizLogic.Manager.Constant consMana = new FS.HISFC.BizLogic.Manager.Constant();
            //try
            //{
            //    patientinfo.PatientInfo.Country.Name = consMana.GetConstant(FS.HISFC.Models.Base.EnumConstant.COUNTRY.ToString(), patientinfo.PatientInfo.Country.ID).Name;

            //    patientinfo.PatientInfo.Nationality.Name = consMana.GetConstant(FS.HISFC.Models.Base.EnumConstant.NATION.ToString(), patientinfo.PatientInfo.Nationality.ID).Name;

            //}
            //catch
            //{
            //    return null;
            //}

            //string mariStatusID=patientinfo.PatientInfo.MaritalStatus.ID.ToString();
            //if (mariStatusID.Equals("4"))
            //{
            //    patientinfo.PatientInfo.MaritalStatus.ID = "5";
            //    patientinfo.PatientInfo.MaritalStatus.Name = "其它";
            //}
            //if (mariStatusID.Equals("5"))
            //{
            //    patientinfo.PatientInfo.MaritalStatus.ID = "5";
            //    patientinfo.PatientInfo.MaritalStatus.Name = "其它";
            //}
            //if (mariStatusID.Equals("6"))
            //{
            //    patientinfo.PatientInfo.MaritalStatus.ID = "4";
            //    patientinfo.PatientInfo.MaritalStatus.Name = "丧偶";
            //}

            //string age = operMana.GetAge(patientinfo.PatientInfo.Birthday);

            //int index = age.IndexOf('岁');

            //if (index > 0)
            //{
            //    age = "Y" + age.Substring(0, age.Length - 1);
            //}
            //else
            //{
            //    index = age.IndexOf('月');
            //    int index1 = age.IndexOf('天');
            //    if (index > 0 &&  index1<=0)
            //    {
            //        age = "M" + age.Substring(0, age.Length - 1);
            //    }
            //    else if (index > 0 && index1 > 0)
            //    {
            //        string[] str = age.Split('月');
            //        if (str.Length == 2)
            //        {
            //            age = "M" + str[0].ToString();
            //            age = age +"D"+str[1].Replace("天", "");
            //        }
            //        else
            //        {
            //            age = "M" + age.Substring(0, age.Length - 1);
            //        }
            //    }
            //    else
            //    {
            //        index = age.IndexOf('天');

            //        if (index > 0)
            //        {
            //            age = "D" + age.Substring(0, age.Length - 1);
            //        }
            //    }
            //}

            //patientinfo.PatientInfo.Age = age;

			//FS.HISFC.BizLogic.HealthRecord.Base bas = new FS.HISFC.BizLogic.HealthRecord.Base();

			//pat = bas.QueryInAndOutDept(patientinfo.PatientInfo.ID, "B");
			//patientinfo.InDept.ID = pat.InDept.ID;
			//patientinfo.InDept.Name = pat.InDept.Name;
			//pati = bas.QueryInAndOutDept(patientinfo.PatientInfo.ID, "O");
			//patientinfo.OutDept.ID = pati.OutDept.ID;
			//patientinfo.OutDept.Name = pati.OutDept.Name;

            //patientinfo.OutDept.ID = patientinfo.PatientInfo.PVisit.PatientLocation.Dept.ID;
            //patientinfo.OutDept.Name = patientinfo.PatientInfo.PVisit.PatientLocation.Dept.Name;

            //patientinfo.InDept.ID = patientinfo.PatientInfo.PVisit.PatientLocation.Dept.ID;
            //patientinfo.InDept.Name = patientinfo.PatientInfo.PVisit.PatientLocation.Dept.Name;
            #endregion
            patientinfo.PatientInfo.Age = "0";

            return patientinfo;
        }

        /// <summary>
        /// 单个患者上传
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public int Upload(string inpatientNo)
        {
            
            if (!isDrgs)
            {
                #region Drgs 传统uc病案首页上传
                FS.HISFC.Models.HealthRecord.Base patientinfo = new FS.HISFC.Models.HealthRecord.Base();
                FS.HISFC.BizProcess.Integrate.RADT dgInpatient = new FS.HISFC.BizProcess.Integrate.RADT();
                patientinfo = this.GetpatientInfo(inpatientNo);

                if (patientinfo == null)
                {
                    return -1;
                }
                this.inTimes = patientinfo.PatientInfo.InTimes;
              
                //出生地为空 则赋籍贯值  
                if (patientinfo.PatientInfo.AreaCode != null && patientinfo.PatientInfo.AreaCode.Trim().Equals(""))
                {
                    patientinfo.PatientInfo.AreaCode = patientinfo.PatientInfo.DIST;
                }
                //转科
                ArrayList alChangeDept = new ArrayList();
                if (isMetCasBase)
                {
                    alChangeDept = deptShiftMana.QueryChangeDeptFromShiftApply(inpatientNo, "2");
                }
                else
                {
                    alChangeDept = deptShiftMana.QueryChangeDeptFromShiftApply(inpatientNo, "1");
                }
                if (alChangeDept == null)
                {
                    return -1;
                }

                //费用
                ArrayList alFee = new ArrayList();
                //if (isMetCasBase)
                //{
                //    alFee = healthfeeMana.QueryCaseFeeState(inpatientNo);
                //}
                //else
                //{
                alFee = healthfeeMana.QueryFeeInfoState(inpatientNo);
                //}
                if (alFee == null)
                {
                    return -1;
                }


                #region 获取 婴儿信息、手术信息、诊断信息
                ArrayList babyAl = new ArrayList();
                ArrayList opAl = new ArrayList();
                ArrayList opAlReal = new ArrayList();
                ArrayList alDiagnose = new ArrayList();
                FS.HISFC.Models.HealthRecord.Tumour TumourInfo = new FS.HISFC.Models.HealthRecord.Tumour();
                ArrayList tumourDetailAl = new ArrayList();
                if (isMetCasBase)
                {
                    FS.HISFC.BizLogic.HealthRecord.Baby babyMgr = new FS.HISFC.BizLogic.HealthRecord.Baby();
                    babyAl = babyMgr.QueryBabyByInpatientNo(patientinfo.PatientInfo.ID);
                    patientinfo.PatientInfo.User03 = babyAl.Count.ToString();//婴儿数量 

                    FS.HISFC.BizLogic.HealthRecord.Operation opMgr = new FS.HISFC.BizLogic.HealthRecord.Operation();
                    opAl = opMgr.QueryOperationByInpatientNo(patientinfo.PatientInfo.ID);

                    foreach (FS.HISFC.Models.HealthRecord.OperationDetail opInfo in opAl)  //防止一些错误数据 有空需要从录入处更改
                    {
                        if (opInfo.OperationDate.Year.ToString().Trim() == "1" || opInfo.OperationInfo.Name.Trim() == "无")
                        {
                            continue;
                        }
                        opAlReal.Add(opInfo);
                    }

                    alDiagnose = this.baseDml.QueryCaseDiagnoseByInpatientNo(patientinfo.PatientInfo.ID);

                    FS.HISFC.BizLogic.HealthRecord.Tumour tumourMgr = new FS.HISFC.BizLogic.HealthRecord.Tumour();
                    TumourInfo = tumourMgr.GetTumour(patientinfo.PatientInfo.ID);
                    tumourDetailAl = tumourMgr.QueryTumourDetail(patientinfo.PatientInfo.ID);
                }
                #endregion

                //add test 
                //FS.HISFC.Management.HealthRecord.InterfaceFunction fun =new FS.HISFC.Management.HealthRecord.InterfaceFunction();
                //fun.GetBaseInfoBA1(patientinfo, alFee, alChangeDept, null);
                //end add test

                FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface uploadMana = null;
                try
                {
                    uploadMana = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface();
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    dgInpatient.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                }
                catch (Exception ex)
                {
                    try
                    {
                        uploadMana.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                    }
                    catch
                    {
                    }
                    MessageBox.Show("连接失败!" + ex.Message);

                    return -2;
                }

                try
                {
                    #region  原来东莞、肿瘤判断是否上传  发现有漏传现象 现改成根据住院号和出院日期判断，住院次数由广东省病案3.0系统处理 chengym
                    //string times=patientinfo.PatientInfo.InTimes.ToString().PadLeft(2, '0');
                    //int intTemp = uploadMana.GetIsHaveNew(patientinfo.PatientInfo.PID.PatientNO.Substring(4),patientinfo.PatientInfo.PID.PatientNO,patientinfo.PatientInfo.Name, ref times, patientinfo.PatientInfo.PVisit.OutTime);

                    //if (intTemp == 3)
                    //{
                    //    uploadMana.Rollback();
                    //    FS.FrameWork.Management.PublicTrans.RollBack();
                    //    return 1;
                    //}

                    //if (intTemp == -1)
                    //{
                    //    uploadMana.Rollback();
                    //    FS.FrameWork.Management.PublicTrans.RollBack();
                    //    MessageBox.Show("上传出错！"+uploadMana.Err);

                    //    return - 1;
                    //}
                    //if (intTemp == 0)//姓名错误，跳过
                    //{
                    //    //uploadMana.Rollback();
                    //    //FS.FrameWork.Management.PublicTrans.RollBack();
                    //    //return - 1;
                    //}

                    //if (intTemp == 2)//修改住院次数
                    //{
                    //    patientinfo.PatientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(times);
                    //    patientinfo.PatientInfo.InTimes++;
                    //    //修改住院主表住院次数
                    //    FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.WorklogFuntion upload = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.WorklogFuntion();
                    //    if (upload.UpdatePatientInTimes(patientinfo.PatientInfo.ID, patientinfo.PatientInfo.InTimes) == -1)
                    //    {
                    //        uploadMana.Rollback();
                    //        FS.FrameWork.Management.PublicTrans.RollBack();
                    //        MessageBox.Show("更新住院次数出错！");
                    //        return -1;
                    //    }
                    //}
                    #endregion

                    #region 屏蔽 改根据住院流水号判断
                    //第一： 判断是否需要上传  
                    //int intTemp = uploadMana.GetIsHave(patientinfo.PatientInfo.PID.PatientNO.Substring(4), patientinfo.PatientInfo.PVisit.OutTime);
                    //if (intTemp == -1)
                    //{
                    //    uploadMana.Rollback();
                    //    FS.FrameWork.Management.PublicTrans.RollBack();
                    //    MessageBox.Show("上传出错！" + uploadMana.Err);

                    //    return -1;
                    //}

                    //if (intTemp == 2 || intTemp==1)
                    //{
                    //    uploadMana.Rollback();
                    //    FS.FrameWork.Management.PublicTrans.RollBack();
                    //    return 1;
                    //}
                    ////第二：判断是否更改 住院次数上传（解决不了本地和广东省病案3.0的次数不对应的问题） 
                    //string inTimes = "";
                    //if (uploadMana.GetInTimes(patientinfo.PatientInfo.PID.PatientNO.Substring(4), ref inTimes) == -1)
                    //{
                    //    uploadMana.Rollback();
                    //    FS.FrameWork.Management.PublicTrans.RollBack();
                    //    MessageBox.Show("上传出错！" + uploadMana.Err);

                    //    return -1;
                    //}
                    //if (inTimes != "0")
                    //{
                    //    patientinfo.PatientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(inTimes) + 1;
                    //}

                    #endregion
                    //begin 2012-1-21 判断在出院日期前是否存在未上传的数据 chengym 避免上传的住院次数翻转
                    if (!checkBox1.Checked)
                    {
                        ArrayList isHavedNoUploadList = new ArrayList();
                        int isHaveTpatientvisit = 0;
                        isHavedNoUploadList = this.baseDml.GetIsHavedNoUpload(patientinfo.PatientInfo.PID.PatientNO, patientinfo.PatientInfo.PVisit.OutTime);
                        if (isHavedNoUploadList != null && isHavedNoUploadList.Count > 0)
                        {
                            foreach (FS.HISFC.Models.RADT.PatientInfo pInfo in isHavedNoUploadList)
                            {
                                isHaveTpatientvisit = uploadMana.GetIsHavedNoUpload(pInfo.PID.PatientNO, pInfo.PVisit.InTime);
                                if (isHaveTpatientvisit != 1)
                                {
                                    return -3;
                                }
                            }
                        }
                    }
                    //end 2012-1-21
                    //第一： 判断是否需要上传   需要上传(1 已上传未录入 2未上传) 3 不需要上传
                    int intTemp = uploadMana.GetIsNeedUpload(patientinfo.PatientInfo.PID.PatientNO.Substring(4), patientinfo.PatientInfo.ID);
                    if (intTemp == -1)
                    {
                        uploadMana.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("上传出错！" + uploadMana.Err);

                        return -1;
                    }
                    if (intTemp == 3)
                    {
                        uploadMana.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return 0;
                    }

                    //第二：判断是否更改 住院次数上传（解决不了本地和广东省病案3.0的次数不对应的问题） 
                    string Times = "";
                    if (uploadMana.GetInTimes(patientinfo.PatientInfo.PID.PatientNO.Substring(4), patientinfo.PatientInfo.ID, intTemp, ref Times) == -1)
                    {
                        uploadMana.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("上传出错！" + uploadMana.Err);

                        return -1;
                    }
                    if (intTemp == 1)
                    {
                        if (Times != "0")
                        {
                            patientinfo.PatientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(Times);
                        }
                    }
                    else
                    {
                        if (Times != "0")
                        {
                            patientinfo.PatientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(Times) + 1;
                        }
                    }
                    #region 复制
                    //主表获取的数据才考虑按历史信息补全患者基本信息
                    if (!isMetCasBase)
                    {
                        FS.HISFC.Models.RADT.PatientInfo patientinfoTemp = uploadMana.GetPatientFromBA(patientinfo.PatientInfo.PID.PatientNO.Substring(4));

                        if (patientinfoTemp == null)
                        {
                        }
                        else
                        {
                            try
                            {
                                if (patientinfo.PatientInfo.Name.Trim() == patientinfoTemp.Name.Trim())
                                {
                                    //if (patientinfo.PatientInfo.MaritalStatus.ID.ToString().Trim() == "")
                                    //{
                                    //    patientinfo.PatientInfo.MaritalStatus.ID = patientinfoTemp.MaritalStatus.ID;
                                    //}
                                    if (patientinfoTemp.Profession.ID.Trim() != "")
                                    {
                                        patientinfo.PatientInfo.Profession.ID = patientinfoTemp.Profession.ID;
                                    }
                                    if (patientinfoTemp.PVisit.InSource.ID.Trim() != "")
                                    {
                                        patientinfo.PatientInfo.PVisit.InSource.ID = patientinfoTemp.PVisit.InSource.ID;
                                    }

                                    if (patientinfo.PatientInfo.CompanyName.Trim() == "")
                                    {
                                        patientinfo.PatientInfo.CompanyName = patientinfoTemp.CompanyName; //工作单位
                                    }

                                    if (patientinfo.PatientInfo.PhoneBusiness.Trim() == "")
                                    {
                                        patientinfo.PatientInfo.PhoneBusiness = patientinfoTemp.PhoneBusiness; //单位电话
                                    }
                                    if (patientinfo.PatientInfo.BusinessZip.Trim() == "")
                                    {
                                        patientinfo.PatientInfo.BusinessZip = patientinfoTemp.BusinessZip; //单位邮编
                                    }

                                    if (patientinfo.PatientInfo.AddressHome.Trim() == "")
                                    {
                                        patientinfo.PatientInfo.AddressHome = patientinfoTemp.AddressHome; //户口或家庭所在
                                    }
                                    if (patientinfo.PatientInfo.HomeZip.Trim() == "")
                                    {
                                        patientinfo.PatientInfo.HomeZip = patientinfoTemp.HomeZip; //户口或家庭邮政编码
                                    }
                                    //patientinfo.PatientInfo.Kin.Name = patientinfoTemp.Kin.Name; //联系人姓名

                                    if (patientinfoTemp.Kin.Name.Trim() == patientinfo.PatientInfo.Kin.Name.Trim())
                                    {
                                        if (patientinfo.PatientInfo.Kin.RelationPhone.Trim() == "")
                                        {
                                            patientinfo.PatientInfo.Kin.RelationPhone = patientinfoTemp.Kin.RelationPhone; //联系人电话
                                        }

                                        //patientinfo.PatientInfo.Kin.RelationAddress = patientinfoTemp.Kin.RelationAddress; //联系人住址
                                        //patientinfo.PatientInfo.Kin.Relation.ID = patientinfoTemp.Kin.Relation.ID; //联系人关系
                                    }

                                    patientinfo.AnaphyFlag = patientinfoTemp.User02;
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                    #endregion

                    #region HIS_BA1 主表信息
                    //end 2011-3-30 ch
                    //if (uploadMana.DeleteHISBA1(patientinfo.PatientInfo.PID.PatientNO.Substring(4),patientinfo.PatientInfo.InTimes) == -1)
                    //{
                    //    uploadMana.Rollback();
                    //    FS.FrameWork.Management.PublicTrans.RollBack();
                    //    MessageBox.Show("删除表ba1失败!");

                    //    return -1;
                    //}
                    if (uploadMana.DeleteHISBA1ByFzyid(patientinfo.PatientInfo.ID) == -1)
                    {
                        uploadMana.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("删除表ba1失败!");
                        return -1;
                    }
                    if (uploadMana.InsertPatientInfoBA1(patientinfo, alFee, alChangeDept, new ArrayList(), this.isMetCasBase) == -1)
                    {
                        uploadMana.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("插入表ba1失败!");

                        return -1;
                    }
                    try//清空Fzkdate转科日期为1900-1-1的情况 无论成功与否，不管
                    {
                        uploadMana.UpdateHISBA1Fzkdate(patientinfo.PatientInfo.PID.PatientNO);
                    }
                    catch
                    {
                    }
                    #endregion
                    #region  HIS_BA2转科信息
                    if (alChangeDept != null && alChangeDept.Count > 0)
                    {
                        if (uploadMana.DeleteHISBA2(patientinfo.PatientInfo.PID.PatientNO.Substring(4), patientinfo.PatientInfo.InTimes) == -1)
                        {
                            uploadMana.Rollback();
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("删除表ba2失败!");
                            return -1;
                        }
                        foreach (FS.HISFC.Models.RADT.Location changeInfo in alChangeDept)
                        {
                            if (changeInfo.Dept.Name.Trim() == "-")
                            {
                                continue;
                            }
                            if (uploadMana.InsertHISBA2(patientinfo, changeInfo) == -1)
                            {
                                uploadMana.Rollback();
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("插入表ba2失败!");
                                return -1;
                            }
                        }
                    }
                    #endregion
                    if (isMetCasBase)
                    {
                        #region HIS_BA3 诊断信息
                        if (alDiagnose != null && alDiagnose.Count > 0)
                        {
                            if (uploadMana.DeleteHISBA3(patientinfo.PatientInfo.PID.PatientNO.Substring(4), patientinfo.PatientInfo.InTimes) == -1)
                            {
                                uploadMana.Rollback();
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("删除表ba3失败!");
                                return -1;
                            }
                            foreach (FS.HISFC.Models.HealthRecord.Diagnose diagNoseInfo in alDiagnose)
                            {
                                if (uploadMana.InsertHISBA3(patientinfo, diagNoseInfo) == -1)
                                {
                                    uploadMana.Rollback();
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("插入表ba3失败!");
                                    return -1;
                                }
                            }
                        }
                        #endregion
                        #region HIS_BA4 手术信息
                        if (opAlReal.Count > 0)
                        {
                            if (uploadMana.DeleteHISBA4(patientinfo.PatientInfo.PID.PatientNO.Substring(4), patientinfo.PatientInfo.InTimes) == -1)
                            {
                                uploadMana.Rollback();
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("删除表ba4失败!");
                                return -1;
                            }
                            foreach (FS.HISFC.Models.HealthRecord.OperationDetail op in opAlReal)
                            {
                                if (uploadMana.insertHisBa4(patientinfo, op) == -1)
                                {
                                    uploadMana.Rollback();
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("插入表ba4失败!");
                                    return -1;
                                }
                            }
                        }
                        #endregion
                        #region HIS_BA5 妇婴信息
                        if (babyAl.Count > 0)
                        {
                            if (uploadMana.DeleteHISBA5(patientinfo.PatientInfo.PID.PatientNO.Substring(4), patientinfo.PatientInfo.InTimes) == -1)
                            {
                                uploadMana.Rollback();
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("删除表ba5失败!");
                                return -1;
                            }

                            foreach (FS.HISFC.Models.HealthRecord.Baby babyInfo in babyAl)
                            {
                                if (uploadMana.insertHisBa5(patientinfo, babyInfo) == -1)
                                {
                                    uploadMana.Rollback();
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("插入表ba5失败!");
                                    return -1;
                                }
                            }
                        }
                        #endregion
                        #region HIS_BA6 HIS_BA7 肿瘤卡
                        if (TumourInfo != null)
                        {
                            if ((TumourInfo.Tumour_Type != null || TumourInfo.Tumour_Stage != null || TumourInfo.Rmodeid != null || TumourInfo.Rprocessid != null || TumourInfo.Rdeviceid != null || TumourInfo.Cmodeid != null || TumourInfo.Cmethod != null)
               && (TumourInfo.Tumour_Type != "" || TumourInfo.Tumour_Stage != "" || TumourInfo.Rmodeid != "" || TumourInfo.Rprocessid != "" || TumourInfo.Rdeviceid != "" || TumourInfo.Cmodeid != "" || TumourInfo.Cmethod != ""))
                            {
                                if (uploadMana.DeleteHISBA6(patientinfo.PatientInfo.PID.PatientNO.Substring(4), patientinfo.PatientInfo.InTimes) == -1)
                                {
                                    uploadMana.Rollback();
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("删除表ba6失败!");
                                    return -1;
                                }
                                if (uploadMana.InsertHISBA6(patientinfo, TumourInfo) == -1)
                                {
                                    uploadMana.Rollback();
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("插入表ba6失败!");
                                    return -1;
                                }
                                if (TumourInfo.Gy1 == 0)
                                {
                                    try
                                    {
                                        uploadMana.UpdateHISBA6FYRQ(patientinfo.PatientInfo.PID.PatientNO.Substring(4));
                                    }
                                    catch
                                    {
                                    }
                                }
                                if (TumourInfo.Gy2 == 0)
                                {
                                    try
                                    {
                                        uploadMana.UpdateHISBA6FQRQ(patientinfo.PatientInfo.PID.PatientNO.Substring(4));
                                    }
                                    catch
                                    {
                                    }
                                }
                                if (TumourInfo.Gy3 == 0)
                                {
                                    try
                                    {
                                        uploadMana.UpdateHISBA6FZRQ(patientinfo.PatientInfo.PID.PatientNO.Substring(4));
                                    }
                                    catch
                                    {
                                    }
                                }

                                if (tumourDetailAl != null && tumourDetailAl.Count > 0)
                                {
                                    if (uploadMana.DeleteHISBA7(patientinfo.PatientInfo.PID.PatientNO.Substring(4), patientinfo.PatientInfo.InTimes) == -1)
                                    {
                                        uploadMana.Rollback();
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        MessageBox.Show("删除表ba7失败!");
                                        return -1;
                                    }
                                    foreach (FS.HISFC.Models.HealthRecord.TumourDetail tumourDetailInfo in tumourDetailAl)
                                    {
                                        if (uploadMana.InsertHISBA7(patientinfo, tumourDetailInfo) == -1)
                                        {
                                            uploadMana.Rollback();
                                            FS.FrameWork.Management.PublicTrans.RollBack();
                                            MessageBox.Show("插入表ba7失败!");
                                            return -1;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    uploadMana.Commit();
                    FS.FrameWork.Management.PublicTrans.Commit();
                    return 1;
                }
                catch (Exception ex)
                {
                    uploadMana.Rollback();
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("上传失败!" + ex.Message);

                    return -1;
                }
                #endregion
            }
            else
            {
                #region Drgs uc病案首页上传
                FS.HISFC.Models.HealthRecord.Base caseInfo = new FS.HISFC.Models.HealthRecord.Base();
                FS.HISFC.BizProcess.Integrate.RADT dgInpatient = new FS.HISFC.BizProcess.Integrate.RADT();
                caseInfo = this.GetpatientInfo(inpatientNo);
                this.inTimes = caseInfo.PatientInfo.InTimes;
                if (caseInfo == null)
                {
                    return -1;
                }
                
                //出生地为空 则赋籍贯值  
                if (caseInfo.PatientInfo.AreaCode != null && caseInfo.PatientInfo.AreaCode.Trim().Equals(""))
                {
                    caseInfo.PatientInfo.AreaCode = caseInfo.PatientInfo.DIST;
                }
                //转科
                ArrayList alChangeDept = new ArrayList();
                if (isMetCasBase)
                {
                    alChangeDept = deptShiftMana.QueryChangeDeptFromShiftApply(caseInfo.PatientInfo.ID, "2");
                }
                else
                {
                    alChangeDept = deptShiftMana.QueryChangeDeptFromShiftApply(caseInfo.PatientInfo.ID, "1");
                }
                if (alChangeDept == null)
                {
                    return -1;
                }

                #region 费用
                DataSet dsFee = new DataSet();

                if (healthfeeMana.QueryFeeForDrgsByInpatientNO(caseInfo.PatientInfo.ID, ref dsFee) == -1)
                {
                    MessageBox.Show("获取费用出错！");
                    return -1;
                }
                if (dsFee == null)
                {
                    return -1;
                }
                #endregion

                #region 获取 婴儿信息、手术信息、诊断信息
                ArrayList babyAl = new ArrayList();
                ArrayList opAl = new ArrayList();
                ArrayList opAlReal = new ArrayList();
                ArrayList alDiagnose = new ArrayList();
                FS.HISFC.Models.HealthRecord.Tumour TumourInfo = new FS.HISFC.Models.HealthRecord.Tumour();
                ArrayList tumourDetailAl = new ArrayList();
                if (isMetCasBase)
                {
                    FS.HISFC.BizLogic.HealthRecord.Baby babyMgr = new FS.HISFC.BizLogic.HealthRecord.Baby();
                    babyAl = babyMgr.QueryBabyByInpatientNo(caseInfo.PatientInfo.ID);
                    caseInfo.PatientInfo.User03 = babyAl.Count.ToString();//婴儿数量 

                    FS.HISFC.BizLogic.HealthRecord.Operation opMgr = new FS.HISFC.BizLogic.HealthRecord.Operation();
                    opAl = opMgr.QueryOperationByInpatientNo(caseInfo.PatientInfo.ID);

                    foreach (FS.HISFC.Models.HealthRecord.OperationDetail opInfo in opAl)  //防止一些错误数据 有空需要从录入处更改
                    {
                        if (opInfo.OperationDate.Year.ToString().Trim() == "1" || opInfo.OperationInfo.Name.Trim() == "无")
                        {
                            continue;
                        }
                        opAlReal.Add(opInfo);
                    }

                    alDiagnose = this.baseDml.QueryCaseDiagnoseByInpatientNo(caseInfo.PatientInfo.ID);

                    FS.HISFC.BizLogic.HealthRecord.Tumour tumourMgr = new FS.HISFC.BizLogic.HealthRecord.Tumour();
                    TumourInfo = tumourMgr.GetTumour(caseInfo.PatientInfo.ID);
                    tumourDetailAl = tumourMgr.QueryTumourDetail(caseInfo.PatientInfo.ID);
                }
                #endregion

                FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface uploadMana = null;
                try
                {
                    uploadMana = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface();
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    dgInpatient.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                }
                catch (Exception ex)
                {
                    try
                    {
                        uploadMana.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                    }
                    catch
                    {
                    }
                    MessageBox.Show("连接失败!" + ex.Message);

                    return -2;
                }

                try
                {
                    //begin 2012-1-21 判断在出院日期前是否存在未上传的数据 chengym 避免上传的住院次数翻转
                    if (!checkBox1.Checked)
                    {
                        ArrayList isHavedNoUploadList = new ArrayList();
                        int isHaveTpatientvisit = 0;
                        isHavedNoUploadList = this.baseDml.GetIsHavedNoUpload(caseInfo.PatientInfo.PID.PatientNO, caseInfo.PatientInfo.PVisit.OutTime);
                        if (isHavedNoUploadList != null && isHavedNoUploadList.Count > 0)
                        {
                            foreach (FS.HISFC.Models.RADT.PatientInfo pInfo in isHavedNoUploadList)
                            {
                                isHaveTpatientvisit = uploadMana.GetIsHavedNoUpload(pInfo.PID.PatientNO, pInfo.PVisit.InTime);
                                if (isHaveTpatientvisit != 1)
                                {
                                    return -3;
                                }
                            }
                        }
                    }
                    //end 2012-1-21
                    //第一： 判断是否需要上传   需要上传(1 已上传未录入 2未上传) 3 不需要上传
                    int intTemp = uploadMana.GetIsNeedUpload(caseInfo.PatientInfo.PID.PatientNO.Substring(4), caseInfo.PatientInfo.ID);
                    if (intTemp == -1)
                    {
                        uploadMana.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("上传出错！" + uploadMana.Err);

                        return -1;
                    }
                    if (intTemp == 3)
                    {
                        uploadMana.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return 0;
                    }

                    //第二：判断是否更改 住院次数上传（解决不了本地和广东省病案3.0的次数不对应的问题） 
                    string Times = "";
                    if (uploadMana.GetInTimes(caseInfo.PatientInfo.PID.PatientNO.Substring(4), caseInfo.PatientInfo.ID, intTemp, ref Times) == -1)
                    {
                        uploadMana.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("上传出错！" + uploadMana.Err);

                        return -1;
                    }
                    if (intTemp == 1)
                    {
                        if (Times != "0")
                        {
                            caseInfo.PatientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(Times);
                        }
                    }
                    else
                    {
                        if (Times != "0")
                        {
                            caseInfo.PatientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(Times) + 1;
                        }
                    }
                    #region 复制
                    //主表获取的数据才考虑按历史信息补全患者基本信息
                    if (!isMetCasBase)
                    {
                        FS.HISFC.Models.RADT.PatientInfo patientinfoTemp = uploadMana.GetPatientFromBA(caseInfo.PatientInfo.PID.PatientNO.Substring(4));

                        if (patientinfoTemp == null)
                        {
                        }
                        else
                        {
                            try
                            {
                                if (caseInfo.PatientInfo.Name.Trim() == patientinfoTemp.Name.Trim())
                                {
                                    //if (patientinfo.PatientInfo.MaritalStatus.ID.ToString().Trim() == "")
                                    //{
                                    //    patientinfo.PatientInfo.MaritalStatus.ID = patientinfoTemp.MaritalStatus.ID;
                                    //}
                                    if (patientinfoTemp.Profession.ID.Trim() != "")
                                    {
                                        caseInfo.PatientInfo.Profession.ID = patientinfoTemp.Profession.ID;
                                    }
                                    if (patientinfoTemp.PVisit.InSource.ID.Trim() != "")
                                    {
                                        caseInfo.PatientInfo.PVisit.InSource.ID = patientinfoTemp.PVisit.InSource.ID;
                                    }

                                    if (caseInfo.PatientInfo.CompanyName.Trim() == "")
                                    {
                                        caseInfo.PatientInfo.CompanyName = patientinfoTemp.CompanyName; //工作单位
                                    }

                                    if (caseInfo.PatientInfo.PhoneBusiness.Trim() == "")
                                    {
                                        caseInfo.PatientInfo.PhoneBusiness = patientinfoTemp.PhoneBusiness; //单位电话
                                    }
                                    if (caseInfo.PatientInfo.BusinessZip.Trim() == "")
                                    {
                                        caseInfo.PatientInfo.BusinessZip = patientinfoTemp.BusinessZip; //单位邮编
                                    }

                                    if (caseInfo.PatientInfo.AddressHome.Trim() == "")
                                    {
                                        caseInfo.PatientInfo.AddressHome = patientinfoTemp.AddressHome; //户口或家庭所在
                                    }
                                    if (caseInfo.PatientInfo.HomeZip.Trim() == "")
                                    {
                                        caseInfo.PatientInfo.HomeZip = patientinfoTemp.HomeZip; //户口或家庭邮政编码
                                    }
                                    //patientinfo.PatientInfo.Kin.Name = patientinfoTemp.Kin.Name; //联系人姓名

                                    if (patientinfoTemp.Kin.Name.Trim() == caseInfo.PatientInfo.Kin.Name.Trim())
                                    {
                                        if (caseInfo.PatientInfo.Kin.RelationPhone.Trim() == "")
                                        {
                                            caseInfo.PatientInfo.Kin.RelationPhone = patientinfoTemp.Kin.RelationPhone; //联系人电话
                                        }

                                        //patientinfo.PatientInfo.Kin.RelationAddress = patientinfoTemp.Kin.RelationAddress; //联系人住址
                                        //patientinfo.PatientInfo.Kin.Relation.ID = patientinfoTemp.Kin.Relation.ID; //联系人关系
                                    }

                                    caseInfo.AnaphyFlag = patientinfoTemp.User02;
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                    #endregion

                    #region HIS_BA1 主表信息
                    //end 2011-3-30 ch
                    //if (uploadMana.DeleteHISBA1(patientinfo.PatientInfo.PID.PatientNO.Substring(4),patientinfo.PatientInfo.InTimes) == -1)
                    //{
                    //    uploadMana.Rollback();
                    //    FS.FrameWork.Management.PublicTrans.RollBack();
                    //    MessageBox.Show("删除表ba1失败!");

                    //    return -1;
                    //}
                    if (uploadMana.DeleteHISBA1ByFzyid(caseInfo.PatientInfo.ID) == -1)
                    {
                        uploadMana.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("删除表ba1失败!");
                        return -1;
                    }
                    if (uploadMana.InsertPatientInfoBA1Drgs(caseInfo, dsFee, alChangeDept, alDiagnose,this.isMetCasBase) == -1)
                    {
                        uploadMana.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("插入表ba1失败!");
                        return -1;
                    }
                    try//清空Fzkdate转科日期为1900-1-1的情况 无论成功与否，不管
                    {
                        uploadMana.UpdateHISBA1Fzkdate(caseInfo.PatientInfo.PID.PatientNO);
                    }
                    catch
                    {
                    }
                    #endregion
                    #region  HIS_BA2转科信息
                    if (alChangeDept != null && alChangeDept.Count > 0)
                    {
                        if (uploadMana.DeleteHISBA2(caseInfo.PatientInfo.PID.PatientNO.Substring(4), caseInfo.PatientInfo.InTimes) == -1)
                        {
                            uploadMana.Rollback();
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("删除表ba2失败!");
                            return -1;
                        }
                        foreach (FS.HISFC.Models.RADT.Location changeInfo in alChangeDept)
                        {
                            if (uploadMana.InsertHISBA2(caseInfo, changeInfo) == -1)
                            {
                                uploadMana.Rollback();
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("插入表ba2失败!");
                                return -1;
                            }
                        }
                    }
                    #endregion
                    if (isMetCasBase)
                    {
                        #region HIS_BA3 诊断信息
                        if (alDiagnose != null && alDiagnose.Count > 0)
                        {
                            if (uploadMana.DeleteHISBA3(caseInfo.PatientInfo.PID.PatientNO.Substring(4), caseInfo.PatientInfo.InTimes) == -1)
                            {
                                uploadMana.Rollback();
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("删除表ba3失败!");
                                return -1;
                            }
                            foreach (FS.HISFC.Models.HealthRecord.Diagnose diagNoseInfo in alDiagnose)
                            {
                                if (uploadMana.InsertHISBA3Drgs(caseInfo, diagNoseInfo) == -1)
                                {
                                    uploadMana.Rollback();
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("插入表ba3失败!");
                                    return -1;
                                }
                            }
                        }
                        #endregion
                        #region HIS_BA4 手术信息
                        if (opAlReal.Count > 0)
                        {
                            if (uploadMana.DeleteHISBA4(caseInfo.PatientInfo.PID.PatientNO.Substring(4), caseInfo.PatientInfo.InTimes) == -1)
                            {
                                uploadMana.Rollback();
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("删除表ba4失败!");
                                return -1;
                            }
                            foreach (FS.HISFC.Models.HealthRecord.OperationDetail op in opAlReal)
                            {
                                if (uploadMana.insertHisBa4Drgs(caseInfo, op) == -1)
                                {
                                    uploadMana.Rollback();
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("插入表ba4失败!");
                                    return -1;
                                }
                            }
                        }
                        #endregion
                        #region HIS_BA5 妇婴信息
                        if (babyAl.Count > 0)
                        {
                            if (uploadMana.DeleteHISBA5(caseInfo.PatientInfo.PID.PatientNO.Substring(4), caseInfo.PatientInfo.InTimes) == -1)
                            {
                                uploadMana.Rollback();
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("删除表ba5失败!");
                                return -1;
                            }

                            foreach (FS.HISFC.Models.HealthRecord.Baby babyInfo in babyAl)
                            {
                                if (uploadMana.insertHisBa5Drgs(caseInfo, babyInfo) == -1)
                                {
                                    uploadMana.Rollback();
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("插入表ba5失败!");
                                    return -1;
                                }
                            }
                        }
                        #endregion
                        #region HIS_BA6 HIS_BA7 肿瘤卡
                        if (TumourInfo != null)
                        {
                            if ((TumourInfo.Tumour_Type != null || TumourInfo.Tumour_Stage != null || TumourInfo.Rmodeid != null || TumourInfo.Rprocessid != null || TumourInfo.Rdeviceid != null || TumourInfo.Cmodeid != null || TumourInfo.Cmethod != null)
               && (TumourInfo.Tumour_Type != "" || TumourInfo.Tumour_Stage != "" || TumourInfo.Rmodeid != "" || TumourInfo.Rprocessid != "" || TumourInfo.Rdeviceid != "" || TumourInfo.Cmodeid != "" || TumourInfo.Cmethod != ""))
                            {
                                if (uploadMana.DeleteHISBA6(caseInfo.PatientInfo.PID.PatientNO.Substring(4), caseInfo.PatientInfo.InTimes) == -1)
                                {
                                    uploadMana.Rollback();
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("删除表ba6失败!");
                                    return -1;
                                }
                                if (uploadMana.InsertHISBA6Drgs(caseInfo, TumourInfo) == -1)
                                {
                                    uploadMana.Rollback();
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("插入表ba6失败!");
                                    return -1;
                                }
                                if (TumourInfo.Gy1 == 0)
                                {
                                    try
                                    {
                                        uploadMana.UpdateHISBA6FYRQ(caseInfo.PatientInfo.PID.PatientNO.Substring(4));
                                    }
                                    catch
                                    {
                                    }
                                }
                                if (TumourInfo.Gy2 == 0)
                                {
                                    try
                                    {
                                        uploadMana.UpdateHISBA6FQRQ(caseInfo.PatientInfo.PID.PatientNO.Substring(4));
                                    }
                                    catch
                                    {
                                    }
                                }
                                if (TumourInfo.Gy3 == 0)
                                {
                                    try
                                    {
                                        uploadMana.UpdateHISBA6FZRQ(caseInfo.PatientInfo.PID.PatientNO.Substring(4));
                                    }
                                    catch
                                    {
                                    }
                                }
                                if (tumourDetailAl != null && tumourDetailAl.Count > 0)
                                {
                                    if (uploadMana.DeleteHISBA7(caseInfo.PatientInfo.PID.PatientNO.Substring(4), caseInfo.PatientInfo.InTimes) == -1)
                                    {
                                        uploadMana.Rollback();
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        MessageBox.Show("删除表ba7失败!");
                                        return -1;
                                    }
                                    foreach (FS.HISFC.Models.HealthRecord.TumourDetail tumourDetailInfo in tumourDetailAl)
                                    {
                                        if (uploadMana.InsertHISBA7Drgs(caseInfo, tumourDetailInfo) == -1)
                                        {
                                            uploadMana.Rollback();
                                            FS.FrameWork.Management.PublicTrans.RollBack();
                                            MessageBox.Show("插入表ba7失败!");
                                            return -1;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    uploadMana.Commit();
                    FS.FrameWork.Management.PublicTrans.Commit();
                    return 1;
                }
                catch (Exception ex)
                {
                    uploadMana.Rollback();
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("上传失败!" + ex.Message);

                    return -1;
                }
                #endregion
            }
        }

        /// <summary>
        /// 更新上传标志
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        private int UpdateUploadStat(string inpatientNo)
        {
            string strSQL = @"update met_cas_base t set t.uploadstatu='1' where t.inpatient_no='{0}'";

            try
            {
                strSQL = string.Format(strSQL, inpatientNo);
            }
            catch
            {
                return -1;
            }

            return this.baseMana.ExecNoQuery(strSQL);
        }
        /// <summary>
        /// 更新上传标志
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        private int UpdateUploadStatFin(string inpatientNo)
        {
            string strSQL = @"update fin_ipr_inmaininfo i set i.casesend_flag='1' where i.inpatient_no='{0}'";

            try
            {
                strSQL = string.Format(strSQL, inpatientNo);
            }
            catch
            {
                return -1;
            }

            return this.baseMana.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            this.neuLabel1.Text = "";

            ArrayList al = this.GetUploadInpatientNo("0", this.neuDateTimePicker1.Value.Date, this.neuDateTimePicker2.Value.AddDays(1).Date);

            if (al.Count == 0)
            {
                MessageBox.Show("没有采集到有效的出院记录！");

                return -1;
            }

            if (MessageBox.Show("总共收集到" + al.Count.ToString() + "条记录" + "是否上传?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return 1;
            }
            int uploadNum = 0;
            for (int i = 1; i <= al.Count; i++)
            {
                //this.neuLabel1.Text =  "已经上传" + i.ToString() + "条数据";

                int temp = (int)(((decimal)i / (decimal)al.Count) * 100);
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(temp);
                Application.DoEvents();

                FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

                patientInfo = al[i - 1] as FS.HISFC.Models.RADT.PatientInfo;

                int intReturn = this.Upload(patientInfo.ID);
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                if (intReturn == -1)
                {
                    this.err += "病历号：" + patientInfo.PID.PatientNO + "次数：" + this.inTimes.ToString() + "上传失败！";
                }
                else if (intReturn == 0)
                {
                    this.errNotNeed += "病历号：" + patientInfo.PID.PatientNO + "次数：" + this.inTimes.ToString() +"已录入，不需要上传！";
                }
                else if (intReturn == -2)
                {
                    this.neuLabel1.Text = "已经上传0条数据";
                    return -1;
                }
                else if (intReturn == -3)
                {
                    this.errNotNeed += "病历号：" + patientInfo.PID.PatientNO + "次数：" + this.inTimes.ToString() + "该患者有未上传数据，请先上传之前出院的信息";
                }
                else
                {
                    uploadNum++;//上传条数
                    if (this.UpdateUploadStat(patientInfo.ID) != 1)
                    {
                        //MessageBox.Show(patientInfo.ID);
                    }
                    this.UpdateUploadStatFin(patientInfo.ID);//更新主表
                }
            }

            if (this.err != "")
            {
                this.neuTextBox1.Text = this.err;
            }
            if (this.errNotNeed != "")
            {
                this.neuTextBox2.Text = this.errNotNeed;
            }
            this.err = "";
            this.errNotNeed = "";
            //2012-1-9 chengym
            this.neuLabel1.Text = "已经上传" + uploadNum.ToString() + "条数据";

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            MessageBox.Show("上传完成！");

            return base.OnSave(sender, neuObject);
        }

        /// <summary>
        /// 输入住院号框的屏蔽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked == true)
            {
                this.textBox1.Visible = true;
            }
            else if (this.checkBox1.Checked == false)
            {
                this.textBox1.Visible = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "测试连接":
                    ucSetConnectSqlServer uc = new ucSetConnectSqlServer();
                    this.ShowControl(uc);
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
        }
        /// <summary>
        /// 显示查询结果控件
        /// </summary>
        /// <param name="c">结果控件</param>
        private int ShowControl(Control c)
        {
            try
            {
                System.Windows.Forms.Form form = new Form();
                form.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                if (form.Controls.Count > 0)
                {
                    form.Controls.Clear();
                }
                if (!form.Controls.Contains(c))
                {
                    form.Controls.Add(c);
                }
                form.Size = new Size(c.Width + 10, c.Height + 50);
                form.StartPosition = FormStartPosition.CenterScreen;
                form.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show("控件加载错误:" + c.Name + ex.Message.ToString());
                return -1;
            }

            return 0;
        }
    }
}
