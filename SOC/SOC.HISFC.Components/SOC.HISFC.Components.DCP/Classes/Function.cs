using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.SOC.HISFC.Components.DCP.Classes
{
    public class Function
    {
        static Function()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        /// <summary>
        /// ��Ҫ��������ϣ���ע��Ϊƥ���
        /// </summary>
        public static System.Collections.Hashtable hsDiag;

        public static System.Data.DataTable dtDiag = null;

        private static FS.SOC.HISFC.BizProcess.DCP.Common commonProcess = new FS.SOC.HISFC.BizProcess.DCP.Common();

        /// <summary>
        /// ת��״̬
        /// </summary>
        /// <param name="state">״̬</param>
        /// <returns>string����</returns>
        public static string ConvertState(FS.SOC.HISFC.DCP.Enum.ReportState state)
        {
            return ((int)state).ToString();
        }

        /// <summary>
        /// ת����������
        /// </summary>
        /// <param name="type">��������</param>
        /// <returns>��������</returns>
        public static string ConverPatientType(FS.SOC.HISFC.DCP.Enum.PatientType type)
        {
            string name = "";
            switch (type)
            {
                case FS.SOC.HISFC.DCP.Enum.PatientType.C:
                    name = "����";
                    break;
                case FS.SOC.HISFC.DCP.Enum.PatientType.I:
                    name = "סԺ";
                    break;
                case FS.SOC.HISFC.DCP.Enum.PatientType.O:
                    name = "����";
                    break;
                default:
                    break;
            }
            return name;
        }

        /// <summary>
        /// ��黼���Ƿ��Ѿ�����
        /// </summary>
        /// <param name="patient">����ʵ��</param>
        /// <param name="diseaseCode">��������</param>
        /// <returns>true�Ѿ�����</returns>
        public static bool CheckPatientNeedReport(FS.HISFC.Models.RADT.Patient patient, string diseaseCode,ref bool isMustReport)
        {
            //���������ͻ��ߺż���
            string sql = @" where (patient_no = '{0}' or patient_name = '{1}') and DISEASE_CODE in ('{2}') and state not in ('3','4') order by report_date desc";
            sql = string.Format(sql, patient.PID.CardNO, patient.Name, diseaseCode.Replace(",", "','"));

            FS.SOC.HISFC.BizLogic.DCP.DiseaseReport dpMgr = new FS.SOC.HISFC.BizLogic.DCP.DiseaseReport();

            System.Collections.ArrayList alReport = dpMgr.GetCommonReportListByWhere(sql);

            //û�б���
            if (alReport == null || alReport.Count == 0)
            {
                return true;
            }

            //�б���
            foreach (FS.HISFC.DCP.Object.CommonReport report in alReport)
            {
                //������Ҫ���Ǳ���ʱ�䣬�������Ⱥ�����ݲ���Ϊ�Ǳ�������
                if (dpMgr.GetDateTimeFromSysDateTime().AddYears(-1) >= report.ReportTime)
                {
                    //����һ���ֱ���ϱ�
                    return true;
                }

                //�Ƿ�ͬ�༲��
                if (diseaseCode.IndexOf(report.Disease.ID) != -1)
                {
                    string msg = "�����Ѿ�������"
                        + "���ţ�" + report.ReportNO
                        + "�������ƣ�" + report.Disease.Name
                        + "����ʱ�䣺" + report.ReportTime.ToString()
                        + "\n�Ƿ��������";
                    isMustReport = false;
                    System.Windows.Forms.DialogResult dr = System.Windows.Forms.MessageBox.Show(Language.Msg(msg), Language.Msg("��ʾ"), System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information);
                    if (dr == System.Windows.Forms.DialogResult.No)
                    {
                        return false;
                    }
                    else
                    {
                        break;
                    }

                }
            }
            return true;
        }


        // {9A497C15-596A-420d-8AA8-27766FFB760E} ��黼���Ƿ��Ѿ���д������ԭ��
        //2015-1-5-yeph

        /// <summary>
        /// ��黼���Ƿ��Ѿ���д������ԭ��
        /// </summary>
        /// <param name="patient">����ʵ��</param>
        /// <param name="diseaseCode">��������</param>
        /// <returns>true�Ѿ�����</returns>
        public static bool CheckPatientNeedResonOfNot(FS.HISFC.Models.RADT.Patient patient, string diseaseName,ref bool isReport)
        {
            //���������ͻ��ߺż���
            string sql = @" 
                          
                               WHERE CLINO_NO = '{0}'
                             AND DIAG_NAME='{1}'
                            ";
            sql = string.Format(sql,((FS.FrameWork.Models.NeuObject)(patient)).ID, diseaseName);

            FS.SOC.HISFC.BizLogic.DCP.DiseaseReport dpMgr = new FS.SOC.HISFC.BizLogic.DCP.DiseaseReport();

            System.Collections.ArrayList alReport = dpMgr.GetReasonOfNotNeed(sql);

            //û�б���
            if (alReport == null || alReport.Count == 0)
            {
                return true;
            }

            //�б���
            else
            {
               
                    string msg = "�����Ѿ���д�����ԭ��"
                        + "�������ƣ�" + diseaseName
                        + "\n�Ƿ������д";
                    isReport = false;
                    System.Windows.Forms.DialogResult dr = System.Windows.Forms.MessageBox.Show(Language.Msg(msg), Language.Msg("��ʾ"), System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information);
                    if (dr == System.Windows.Forms.DialogResult.No)
                    {
                        return false;
                    }
                    
                }
            
            return true;
        }



        /// <summary>
        /// ������������ж��Ƿ�Ⱦ��
        /// </summary>
        /// <param name="diagName">�������</param>
        /// <param name="diseaseCode">�������봮[���ܶ���]</param>
        /// <returns>true�Ǵ�Ⱦ��</returns>
        public static bool CheckDiagNose(string diagName, out string diseaseCode)
        {
            //ȥ���ո�
            diagName = diagName.Trim();
            diseaseCode = "";

            if (string.IsNullOrEmpty(diagName))
            {
                return false;
            }

            if (hsDiag == null || dtDiag == null)
            {
                hsDiag = new System.Collections.Hashtable();
                
                System.Collections.ArrayList alDiag = commonProcess.QueryConstantList("INFDIAGNOSE");

                foreach (FS.HISFC.Models.Base.Const con in alDiag)
                {
                    if (!hsDiag.Contains(con.Memo))
                    {
                        hsDiag.Add(con.Memo, con);
                    }
                }
            }
            

            //��ȷƥ��
            if (hsDiag.Contains(diagName))
            {
                diseaseCode = ((FS.HISFC.Models.Base.Const)hsDiag[diagName]).UserCode;
                return true;
            }

            //ģ��ƥ��
            string sql = "select code,name,mark,input_code from com_dictionary t where t.type='INFDIAGNOSE' and '{0}' like mark";
            FS.FrameWork.Management.DataBaseManger dbManager = new DataBaseManger();
            if (dbManager.ExecQuery(string.Format(sql, FS.FrameWork.Public.String.TakeOffSpecialChar(diagName))) > 0)
            {
                try
                {
                    if (dbManager.Reader.Read())
                    {
                        diseaseCode = dbManager.Reader[3].ToString();
                        return true;
                    }
                }
                catch (Exception e)
                {
                    return false;
                }
                finally
                {
                    if (dbManager.Reader != null && !dbManager.Reader.IsClosed)
                    {
                        dbManager.Reader.Close();
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// ����û��Ƿ���Ȩ�����
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="privType"></param>
        /// <returns></returns>
        public static bool CheckUserPriv(string operCode, string privType)
        {
            List<FS.FrameWork.Models.NeuObject> alPriv = null;

            FS.SOC.HISFC.BizProcess.DCP.Permission permissionProcess = new FS.SOC.HISFC.BizProcess.DCP.Permission();
            alPriv = permissionProcess.QueryUserPriv(operCode, privType);
            if (alPriv == null || alPriv.Count == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// �ж��Ƿ�Ϊ�Ǹ�������
        /// </summary>
        /// <param name="xpressions">�ַ���</param>
        /// <returns>true ��</returns>
        public static bool IsUnInt(string expressions)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(expressions, @"^\d+$");
        }

        public static bool IsControl(string expressions)
        {
            //^[A-Za-z]+$����//ƥ����26��Ӣ����ĸ��ɵ��ַ���
            //^[A-Z]+$����//ƥ����26��Ӣ����ĸ�Ĵ�д��ɵ��ַ���
            //^[a-z]+$����//ƥ����26��Ӣ����ĸ��Сд��ɵ��ַ���
            //^[A-Za-z0-9]+$����//ƥ�������ֺ�26��Ӣ����ĸ��ɵ��ַ���
            //^\w+$����//ƥ�������֡�26��Ӣ����ĸ�����»�����ɵ��ַ���

            return false;

        }

        /// <summary>
        /// �ж��Ƿ���Ȩ��
        /// </summary>
        /// <param name="class2Code">����Ȩ��</param>
        /// <param name="class3Code">����Ȩ��</param>
        /// <returns>true�У�false��</returns>
        public static bool JugePrive(string class2Code, string class3Code)
        {
            if (string.IsNullOrEmpty(class2Code) || string.IsNullOrEmpty(class3Code))
            {
                return false;
            }
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager powerDetailManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            List<FS.FrameWork.Models.NeuObject> listPrive = powerDetailManager.QueryUserPriv(powerDetailManager.Operator.ID, class2Code, class3Code);
            if (listPrive == null)
            {
                return false;
            }

            return listPrive.Count > 0;
        }

        /// <summary>
        /// ȡ��ǰ����Ա�Ƿ���ĳһȨ�ޡ�
        /// </summary>
        /// <param name="class2Code">����Ȩ�ޱ���</param>
        /// <returns>True ��Ȩ��, False ��Ȩ��</returns>
        public static bool JugePrive(string class2Code)
        {
            List<FS.FrameWork.Models.NeuObject> al = null;
            //Ȩ�޹�����
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager privManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            //ȡ����Աӵ��Ȩ�޵Ŀ���
            al = privManager.QueryUserPriv(privManager.Operator.ID, class2Code);

            if (al == null || al.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// ����Ƿ��ǹ���Ա
        /// </summary>
        /// <param name="operCode">����Ա����</param>
        /// <returns>true �ǣ�false ��</returns>
        public static bool JugeManager(string operCode)
        {
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager privManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            return ((FS.HISFC.Models.Base.Employee)privManager.Operator).IsManager;
        }

    }
}
