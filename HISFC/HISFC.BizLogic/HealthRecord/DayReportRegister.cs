using System;
using System.Collections;
using System.Text;

namespace FS.HISFC.BizLogic.HealthRecord
{
    /// <summary>
    /// [��������: �����ձ�ά��]<br></br>
    /// [�� �� ��: ��ȫ]<br></br>
    /// [����ʱ��: 2007-09-17]<br></br>
    /// 
    /// <�޸ļ�¼
    ///		�޸��� =
    ///		�޸�ʱ�� =
    ///		�޸�Ŀ�� =
    ///		�޸����� =
    ///  />
    /// </summary>
    public class DayReportRegister : FS.FrameWork.Management.Database
    {
        public DayReportRegister()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        /// <summary>
        /// ��ѯĳ�յ������ձ�
        /// </summary>
        /// <param name="statTime"></param>
        /// <returns></returns>
        public ArrayList QueryByStatTime(DateTime statTime)
        {
            ArrayList list = null;
            string strSql = "";
            if (this.Sql.GetSql("Case.OpbDayReport.SelectOpbDayReport", ref strSql) == -1) return null;
            strSql = string.Format(strSql, statTime.ToString());

            try
            {
                //��ѯ
                this.ExecQuery(strSql);

                FS.HISFC.Models.HealthRecord.DayReportRegister regDayReport = null;
                list = new ArrayList();
                while (this.Reader.Read())
                {
                    regDayReport = new FS.HISFC.Models.HealthRecord.DayReportRegister();

                    regDayReport.DateStat = statTime;
                    regDayReport.Dept.ID = this.Reader[0].ToString(); //���ұ���
                    regDayReport.Dept.Name = this.Reader[1].ToString(); //��������
                    regDayReport.ClinicNum = FrameWork.Function.NConvert.ToInt32(this.Reader[2]); //��������
                    regDayReport.EmcNum = FrameWork.Function.NConvert.ToInt32(this.Reader[3]);  //��������
                    regDayReport.EmcDeadNum = FrameWork.Function.NConvert.ToInt32(this.Reader[4]);  //������������
                    regDayReport.ObserveNum = FrameWork.Function.NConvert.ToInt32(this.Reader[5]); //�۲�����
                    regDayReport.ObserveDeadNum = FrameWork.Function.NConvert.ToInt32(this.Reader[6]); //�۲���������
                    regDayReport.ReDiagnoseNum = FrameWork.Function.NConvert.ToInt32(this.Reader[7]); //��������
                    regDayReport.ClcDiagnoseNum = FrameWork.Function.NConvert.ToInt32(this.Reader[8]); //��������
                    regDayReport.SpecialNum = FrameWork.Function.NConvert.ToInt32(this.Reader[9]); //ר����������
                    regDayReport.HosInsuranceNum = FrameWork.Function.NConvert.ToInt32(this.Reader[10]); //ҽ����������
                    regDayReport.BdCheckNum = FrameWork.Function.NConvert.ToInt32(this.Reader[11]); //���/�����������
                    regDayReport.Oper.ID = this.Reader[12].ToString(); //����Ա����
                    regDayReport.Oper.Name = this.Reader[13].ToString(); //����Ա����
                    regDayReport.OperDate = FrameWork.Function.NConvert.ToDateTime(this.Reader[14]); //����ʱ��

                    list.Add(regDayReport);
                }

                return list;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// �������ձ� �������п����б�
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryAllDept(DateTime statTime)
        {
            ArrayList list = null;
            string strSql = "";
            if (this.Sql.GetSql("Case.OpbDayReport.SelectAllDept", ref strSql) == -1) return null;

            try
            {
                //��ѯ
                this.ExecQuery(strSql);

                FS.HISFC.Models.HealthRecord.DayReportRegister regDayReport = null;
                list = new ArrayList();
                while (this.Reader.Read())
                {
                    regDayReport = new FS.HISFC.Models.HealthRecord.DayReportRegister();

                    regDayReport.DateStat = statTime;
                    regDayReport.Dept.ID = this.Reader[0].ToString(); //���ұ���
                    regDayReport.Dept.Name = this.Reader[1].ToString(); //��������
                    regDayReport.ClinicNum = FrameWork.Function.NConvert.ToInt32(this.Reader[2]); //��������
                    regDayReport.EmcNum = FrameWork.Function.NConvert.ToInt32(this.Reader[3]);  //��������
                    regDayReport.EmcDeadNum = FrameWork.Function.NConvert.ToInt32(this.Reader[4]);  //������������
                    regDayReport.ObserveNum = FrameWork.Function.NConvert.ToInt32(this.Reader[5]); //�۲�����
                    regDayReport.ObserveDeadNum = FrameWork.Function.NConvert.ToInt32(this.Reader[6]); //�۲���������
                    regDayReport.ReDiagnoseNum = FrameWork.Function.NConvert.ToInt32(this.Reader[7]); //��������
                    regDayReport.ClcDiagnoseNum = FrameWork.Function.NConvert.ToInt32(this.Reader[8]); //��������
                    regDayReport.SpecialNum = FrameWork.Function.NConvert.ToInt32(this.Reader[9]); //ר����������
                    regDayReport.HosInsuranceNum = FrameWork.Function.NConvert.ToInt32(this.Reader[10]); //ҽ����������
                    regDayReport.BdCheckNum = FrameWork.Function.NConvert.ToInt32(this.Reader[11]); //���/�����������
                    regDayReport.Oper.ID = this.Reader[12].ToString(); //����Ա����
                    regDayReport.Oper.Name = this.Reader[13].ToString(); //����Ա����
                    regDayReport.OperDate = FrameWork.Function.NConvert.ToDateTime(this.Reader[14]); //����ʱ��

                    list.Add(regDayReport);
                }

                return list;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// ���������ձ� 1�ɹ�, -1ʧ��   ��met_cas_opbdayreport
        /// </summary>
        /// <param name="arrayList"></param>
        /// <returns></returns>
        public int InsertOpdDayReport(ArrayList arrayList)
        {
            string strSql = "";
            string strTemp = "";
            if (this.Sql.GetSql("Case.OpbDayReport.InsertOpbDayReport", ref strSql) == -1) return -1;

            try
            {
                foreach (FS.HISFC.Models.HealthRecord.DayReportRegister dayReport in arrayList)
                {
                    strTemp = strSql;
                    strTemp = string.Format(strTemp,
                             dayReport.DateStat.ToString(),
                             dayReport.Dept.ID,
                             dayReport.ClinicNum,
                             dayReport.EmcNum,
                             dayReport.EmcDeadNum,
                             dayReport.ObserveNum,
                             dayReport.ObserveDeadNum,
                             dayReport.ReDiagnoseNum,
                             dayReport.ClcDiagnoseNum,
                             dayReport.SpecialNum,
                             dayReport.HosInsuranceNum,
                             dayReport.BdCheckNum,
                             dayReport.Oper.ID);

                    if (this.ExecNoQuery(strTemp) < 0) return -1;
                }
            }
            catch (Exception Ex)
            {
                this.Err = Ex.Message; 
                return -1;
            }

            return 1;
        }

        public int UpdateOpdDayReport(ArrayList arrayList)
        {
            string strSql = "";
            string strTemp = "";
            if (this.Sql.GetSql("Case.OpbDayReport.UpdateOpbDayReport", ref strSql) == -1) return -1;

            try
            {
                foreach (FS.HISFC.Models.HealthRecord.DayReportRegister dayReport in arrayList)
                {
                    strTemp = strSql;
                    strTemp = string.Format(strTemp,
                             dayReport.DateStat.ToString(),
                             dayReport.Dept.ID,
                             dayReport.ClinicNum,
                             dayReport.EmcNum,
                             dayReport.EmcDeadNum,
                             dayReport.ObserveNum,
                             dayReport.ObserveDeadNum,
                             dayReport.ReDiagnoseNum,
                             dayReport.ClcDiagnoseNum,
                             dayReport.SpecialNum,
                             dayReport.HosInsuranceNum,
                             dayReport.BdCheckNum);

                    if (this.ExecNoQuery(strTemp) < 0) return -1;
                }
            }
            catch (Exception Ex)
            {
                this.Err = Ex.Message; 
                return -1;
            }

            return 1;
        }
    }
}
