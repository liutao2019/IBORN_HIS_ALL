using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FS.HISFC.Models.HealthRecord.EnumServer;
namespace FS.HISFC.BizLogic.HealthRecord
{
    public class CaseReport : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// �������� ��ȡ��Ϣ
        /// </summary>
        /// <returns></returns>
        public string GetInfoIndex(string beginDate, string EndDate, System.Data.DataSet ds, FS.HISFC.Models.HealthRecord.EnumServer.ReportIndexs type, string deptList)
        {
            string strSql = "";
            //FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            try
            {
                switch (type)
                {
                    case ReportIndexs.NameIndex: //����������
                        if (this.Sql.GetSql("Case.CaseReport.GetInfoIndex.NameIndex", ref strSql) == -1) return null;
                        strSql = string.Format(strSql, beginDate, EndDate);
                        break;
                    case ReportIndexs.DeathIndex: //����������
                        if (this.Sql.GetSql("Case.CaseReport.GetInfoIndex.DeathIndex", ref strSql) == -1) return null;
                        strSql = string.Format(strSql, beginDate, EndDate, deptList);
                        break;
                    case ReportIndexs.DepartDept://��Ժ�ֿƵǼǱ�
                        if (this.Sql.GetSql("Case.CaseReport.GetInfoIndex.DepartDept", ref strSql) == -1) return null;
                        strSql = string.Format(strSql, beginDate, EndDate, deptList);
                        break;
                    case ReportIndexs.Zhidaoban: //ְ�������ʾ�����
                        if (this.Sql.GetSql("Case.CaseReport.GetInfoIndex.Zhidaoban", ref strSql) == -1) return null;
                        strSql = string.Format(strSql, beginDate, EndDate);
                        break;
                    case ReportIndexs.BeforeODept: //������ǰƽ��סԺ��ͳ��
                        if (this.Sql.GetSql("Case.CaseReport.GetInfoIndex.BeforeODept", ref strSql) == -1) return null;
                        strSql = string.Format(strSql, beginDate, EndDate);
                        break;
                    case ReportIndexs.AfterODept: //��������ƽ��סԺ��ͳ��
                        if (this.Sql.GetSql("Case.CaseReport.GetInfoIndex.AfterODept", ref strSql) == -1) return null;
                        strSql = string.Format(strSql, beginDate, EndDate);
                        break;
                    case ReportIndexs.BeforeOperation: //������������ͳ�Ʊ�
                        if (this.Sql.GetSql("Case.CaseReport.GetInfoIndex.BeforeOperation", ref strSql) == -1) return null;
                        strSql = string.Format(strSql, beginDate, EndDate);
                        break;
                    case ReportIndexs.ComeBackInWeek: //һ���ڸ���Ժ
                        if (this.Sql.GetSql("Case.CaseReport.GetInfoIndex.ComeBackInWeek", ref strSql) == -1) return null;
                        strSql = string.Format(strSql, beginDate, EndDate);
                        break;
                    case ReportIndexs.Infection:// ��Ⱦ�����鱨��
                        if (this.Sql.GetSql("Case.CaseReport.GetInfoIndex.Infection", ref strSql) == -1) return null;
                        strSql = string.Format(strSql, beginDate, EndDate);
                        break;
                    case ReportIndexs.CaseUserfrequence:// ����ʹ��Ƶ��ͳ�Ʊ� 
                        if (this.Sql.GetSql("Case.CaseReport.GetInfoIndex.CaseUserfrequence", ref strSql) == -1) return null;
                        strSql = string.Format(strSql, beginDate, EndDate);
                        break;
                    case ReportIndexs.DoctorUserfrequence:// ҽʦʹ��Ƶ��ͳ�Ʊ� 
                        if (this.Sql.GetSql("Case.CaseReport.GetInfoIndex.DoctorUserfrequence", ref strSql) == -1) return null;
                        strSql = string.Format(strSql, beginDate, EndDate);
                        break;
                    case ReportIndexs.InputPerson:// ¼����ԱƵ��ͳ�Ʊ� 
                        if (this.Sql.GetSql("Case.CaseReport.GetInfoIndex.InputPerson", ref strSql) == -1) return null;
                        strSql = string.Format(strSql, beginDate, EndDate);
                        break;
                    case ReportIndexs.ICDDiagPerson:// ��ϱ�����Ա������ͳ�� 
                        if (this.Sql.GetSql("Case.CaseReport.GetInfoIndex.ICDDiagPerson", ref strSql) == -1) return null;
                        strSql = string.Format(strSql, beginDate, EndDate);
                        break;
                    case ReportIndexs.DiseaseAndOutState:// ���ַ�����ת��ͳ�Ʊ� 
                        if (this.Sql.GetSql("Manager.Constant.UpdateComHospitalinfoDate", ref strSql) == -1) return null;
                        strSql = string.Format(strSql, beginDate, EndDate);
                        if (this.ExecNoQuery(strSql) != 1)
                        {
                            this.Err = "����com_hospitalinfoʧ��";
                            return null;
                        }
                        if (this.Sql.GetSql("Case.CaseReport.GetInfoIndex.DiseaseAndOutState", ref strSql) == -1) return null;
                        break;
                    case ReportIndexs.TumourDiseaseAndOutState:// ��������ת��ͳ�Ʊ� 
                        if (this.Sql.GetSql("Manager.Constant.UpdateComHospitalinfoDate", ref strSql) == -1) return null;
                        strSql = string.Format(strSql, beginDate, EndDate);
                        if (this.ExecNoQuery(strSql) != 1)
                        {
                            this.Err = "����com_hospitalinfoʧ��";
                            return null;
                        }
                        if (this.Sql.GetSql("Case.CaseReport.GetInfoIndex.TumourDiseaseAndOutState", ref strSql) == -1) return null;
                        break;
                    case ReportIndexs.BorrowCase:// ��ϱ�����Ա������ͳ�� 
                        if (this.Sql.GetSql("Case.CaseReport.GetInfoIndex.BorrowCase", ref strSql) == -1) return null;
                        strSql = string.Format(strSql, beginDate, EndDate);
                        break;
                    case ReportIndexs.BackUpCase:// ��������Ա������ͳ�� 
                        if (this.Sql.GetSql("Case.CaseReport.GetInfoIndex.BorrowCase", ref strSql) == -1) return null;
                        strSql = string.Format(strSql, beginDate, EndDate);
                        break;
                    case ReportIndexs.OperationCoding1:// ����������Ա������ͳ�� 1 
                        if (this.Sql.GetSql("Case.CaseReport.GetInfoIndex.OperationCoding1", ref strSql) == -1) return null;
                        strSql = string.Format(strSql, beginDate, EndDate);
                        break;
                    case ReportIndexs.OperationCoding2:// ����������Ա������ͳ�� 2 
                        if (this.Sql.GetSql("Case.CaseReport.GetInfoIndex.OperationCoding2", ref strSql) == -1) return null;
                        strSql = string.Format(strSql, beginDate, EndDate);
                        break;
                    case ReportIndexs.DiagCoding:// ��ϱ�����Ա������ͳ�Ʊ� 2 
                        if (this.Sql.GetSql("Case.CaseReport.GetInfoIndex.DiagCoding", ref strSql) == -1) return null;
                        strSql = string.Format(strSql, beginDate, EndDate);
                        break;
                }
                this.ExecQuery(strSql, ref ds);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            return strSql;
        }
        /// <summary>
        /// ��ȡ������Ϣ ���� ����������
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public ArrayList GetInfoClassify(string beginDate, string EndDate, FS.HISFC.Models.HealthRecord.EnumServer.ReportIndexs type)
        {
            string strSql = "";
            ArrayList list = new ArrayList();
            try
            {
                switch (type)
                {
                    case ReportIndexs.OperationClassisy:
                        if (this.Sql.GetSql("Case.CaseReport.GetInfoIndex.OperationClassisy", ref strSql) == -1) return null;
                        break;
                    case ReportIndexs.DiseaseClassify:
                        if (this.Sql.GetSql("Case.CaseReport.GetInfoIndex.DiseaseClassify", ref strSql) == -1) return null;
                        break;
                }
                strSql = string.Format(strSql, beginDate, EndDate);
                this.ExecQuery(strSql);
                FS.FrameWork.Models.NeuObject obj = null;
                while (this.Reader.Read())
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = this.Reader[0].ToString(); //����
                    obj.Name = this.Reader[1].ToString(); // ����
                    obj.User01 = this.Reader[2].ToString(); //סԺ�� 
                    list.Add(obj);
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                this.Err = ex.Message;
                return null;
            }
            return list;

        }
        public string GetPersonSum(string beginDate, string EndDate)
        {
            string strSql = "";
            try
            {
                if (this.Sql.GetSql("Case.CaseReport.GetPersonNum", ref strSql) == -1) return null;
                strSql = string.Format(strSql, beginDate, EndDate);
                return this.ExecSqlReturnOne(strSql);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }
    }

    
}
