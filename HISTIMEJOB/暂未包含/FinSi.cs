using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Neusoft.HISFC.BizProcess.Interface.Common;

namespace HISTIMEJOB
{
    /// <summary>
    /// <br></br>
    /// [��������: ҽ����ʱ�ϴ�]<br></br>
    /// [�� �� ��: ]<br></br>
    /// [����ʱ��: 2010-04-08]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class FinSi : Neusoft.FrameWork.Management.Database, IJob
    {
        #region "����"
        //������ʱ��
        public DateTime dtServerDateTime;
        //�������߼���
        public ArrayList alPatientInfo = new ArrayList();
        public string JobArg = string.Empty;

        //��ʾ���ı���,���������ڵ��ı���
        public Neusoft.FrameWork.WinForms.Controls.NeuRichTextBox rtbLogo = null;
        //���ù�����		
        public Neusoft.HISFC.BizLogic.Fee.InPatient feeInpatient = new Neusoft.HISFC.BizLogic.Fee.InPatient();

        public Neusoft.HISFC.BizLogic.Fee.Interface feeInterface = new  Neusoft.HISFC.BizLogic.Fee.Interface();
        //���߹�����
        private Neusoft.HISFC.BizLogic.RADT.InPatient radtInpatient = new Neusoft.HISFC.BizLogic.RADT.InPatient();
        #endregion



        #region "����"
        #region ����סԺ�������
        /// <summary>
        /// ����סԺ�������
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
                string logoText = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " ��������[" + patient.ID.ToString() + "]��" + strSql + "\r\n";
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
        /// ��ȡռ�ô�λ�б�
        /// </summary>
        /// <returns>nullʧ��</returns>
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
        /// �̶�������ȡ
        /// </summary>
        /// <returns>1 �ɹ� ��1 ʧ��</returns>
        public int reCharge()
        {
            try
            {
                //������ʱ��
                dtServerDateTime = this.GetDateTimeFromSysDateTime();

                if (dtServerDateTime == DateTime.MinValue)
                {
                    return -1;
                }
                string[] pacts = this.JobArg.Split('|');
                for (int j = 0; j < pacts.Length ; j++)
                {
                    //��ȡռ��ȫ�����ݼ���
                    this.alPatientInfo = this.GetPatientInfos(pacts[j].ToString().Trim());
                    if (alPatientInfo == null)
                    {
                        WriteErr();
                        return -1;//���û��ȡ��ռ���б��򷵻�
                    }

                    for (int i = 0; i < alPatientInfo.Count; i++)
                    {
                        Neusoft.HISFC.Models.RADT.PatientInfo pi = (Neusoft.HISFC.Models.RADT.PatientInfo)alPatientInfo[i];
                        //�жϻ��ߵ�סԺ��ˮ��					
                        if (string.IsNullOrEmpty(pi.ID) == true)
                        {
                            this.Err = pi.ID + "���Ļ���סԺ��ˮ��Ϊ��!";
                            this.WriteErr();
                            continue;
                        }

                        //����ÿ������
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
                this.Err = "��ʱ�ϴ�����!" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return 1;
        }


        /// <summary>
        /// ��������ȡ�̶�����
        /// </summary>
        /// <param name="bed">��λʵ��</param>
        /// <param name="operDate">����ʱ��</param>
        /// <returns>1�ɹ� ��1ʧ��</returns>
        public int SetFeeByPerson(Neusoft.HISFC.Models.RADT.PatientInfo pi, DateTime operDate)
        {
            //��ȡ���߻�����Ϣ
            Neusoft.HISFC.Models.RADT.PatientInfo patientInfo = this.radtInpatient.QueryPatientInfoByInpatientNO(pi.ID);

            if (JobArg.IndexOf(patientInfo.Pact.ID) < 0)
            {
                this.Err = "������򲻴��������ͬ��λ�Ļ��ߣ�" + patientInfo.ID;
                this.WriteErr();
                return 1;
            }

            if (patientInfo == null)
            {
                this.Err = pi.ID + this.radtInpatient.Err;
                WriteErr();
                return -1;
            }
            //��������ƵĻ���
            if (DoFinSiFee(patientInfo) == -1)
            {
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// ���һ��߷�����ϸ
        /// </summary>
        /// <param name="patientInfo"></param>
        private int DoFinSiFee(Neusoft.HISFC.Models.RADT.PatientInfo patient)
        {
            /// <summary>
            /// ��ҩƷ��ϸ
            /// </summary>
            ArrayList alItemList;

            /// <summary>
            /// ҩƷ��ϸ
            /// </summary>
            ArrayList alMedicineList;

            string errText = "";

            #region ��δ�ϴ��ķ�����ϸ

            //��ѯ
            alItemList = this.feeInpatient.QueryItemListsForBalance(patient.ID);
            if (alItemList == null)
            {
                errText = "��ѯ���߷�ҩƷ��Ϣ����" + this.feeInpatient.Err;
                return -1;
            }

            alMedicineList = this.feeInpatient.QueryMedicineListsForBalance(patient.ID);
            if (alMedicineList == null)
            {
                errText = "��ѯ����ҩƷ��Ϣ����" + this.feeInpatient.Err;
                return -1;
            }

            ArrayList alFeeItemLists = new ArrayList();
            //��ӻ�����Ϣ
            foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList  item in alItemList)
            {
                alFeeItemLists.Add(item);
            }

            foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList  medicineList in alMedicineList)
            {
                alFeeItemLists.Add(medicineList);
            }
          

            #endregion
            #region ��ҽ������
            try
            {
                #region  �Ӵ����㷨֧��
                int revInt = -1;
                patient.SIMainInfo.SiEmployeeCode = "HHKO1";
                patient.SIMainInfo.SiEmployeeName = "����Ա";


                Neusoft.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy newMedcareInterfaceInstance = new Neusoft.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
                revInt = newMedcareInterfaceInstance.SetPactCode(patient.Pact.ID);
                if (revInt == -1)
                {
                    this.Err = "��ʼ���´����㷨����" + newMedcareInterfaceInstance.ErrMsg;
                    WriteErr();
                    return -1;
                }
                long revLong = newMedcareInterfaceInstance.Connect();
                if (revLong < 0)
                {
                    this.Err = "��ʼ�������´����㷨����" + newMedcareInterfaceInstance.ErrMsg;
                    WriteErr();
                    return -1;
                }
                #endregion
                Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
             
                ArrayList alForPerBalance = new ArrayList();
                if (newMedcareInterfaceInstance.PreBalanceInpatient(patient, ref alFeeItemLists) < 0)
                {
                    this.Err = "Ԥ����ʧ�ܣ�" + newMedcareInterfaceInstance.ErrMsg +"סԺ��ˮ��" + patient.ID;
                    WriteErr();
                    return -1;
                }
                //����סԺ����
                if (UpdateInMainInfoCost(patient) < 0)
                {
                    this.Err = "Ԥ�����������ʧ�ܣ�" + newMedcareInterfaceInstance.ErrMsg + "סԺ��ˮ��" + patient.ID;
                    WriteErr();
                    return -1;
                }

                revLong = newMedcareInterfaceInstance.Commit();
                if (revLong < 0)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    newMedcareInterfaceInstance.Rollback();
                    this.Err = "�����㷨�ύ���ݳ���" + newMedcareInterfaceInstance.ErrMsg + "סԺ��ˮ��" + patient.ID;
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
                this.Err = "�ϴ���Ԥ��ʧ�ܣ�" + ex.Message;
                WriteErr();
                return -1;
            }
            #endregion
           
            return 1;
        }

        /// <summary>
        /// д������־
        /// </summary>
        public override void WriteErr()
        {
            this.myMessage = this.Err;
            base.WriteErr();
        }
        #endregion


        #region IJob ��Ա
        private string myMessage = ""; //������Ϣ
        public string Message
        {
            // TODO:  ��� calculateFee.Con setter ʵ��
            get
            {
                return this.myMessage;
            }
        }
        public System.Data.OracleClient.OracleConnection Con
        {
            set
            {
                // TODO:  ��� calculateFee.Con setter ʵ��
            }
        }

        public int Start()
        {
            // TODO:  ��� calculateFee.Start ʵ��
            return this.reCharge();
        }
        #endregion

    }
}
