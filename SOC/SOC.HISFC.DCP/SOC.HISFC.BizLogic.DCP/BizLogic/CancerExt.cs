using System;
using System.Collections;

namespace FS.SOC.HISFC.BizLogic.DCP
{
    /// <summary>
    /// CancerExt ��ժҪ˵����
    /// �������������ҵ��
    /// </summary>
    public class CancerExt : FS.FrameWork.Management.Database
    {
        public CancerExt()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }



        private FS.HISFC.DCP.Object.CancerAddExt cancerAddExt;


        /// <summary>
        /// ��ȡ�������濨�Ĳ���
        /// </summary>
        /// <param name="cancerAddress">ʵ��</param>
        /// <returns>��������</returns>
        private string[] myGetCancerAddExtReportParm(FS.HISFC.DCP.Object.CancerAddExt cancerAddExt)
        {
            string[] strParm = {   cancerAddExt.Report_No,
								   cancerAddExt.Class_Code,
								   cancerAddExt.Class_Name,
								   cancerAddExt.Item_Code,
								   cancerAddExt.Item_Name,
								   cancerAddExt.Item_Demo  								   
							   };
            return strParm;
        }
        /// <summary>
        /// ���� ��������
        /// </summary>
        public int InsertCancerExt(FS.HISFC.DCP.Object.CancerAddExt cancerAddExt)
        {
            string strSQL = "";
            if (this.Sql.GetSql("DCP.CancerExt.Insert", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�DCP.CancerExt.Insert�ֶ�";
                return -1;
            }
            try
            {
                string[] strParm = this.myGetCancerAddExtReportParm(cancerAddExt);  //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);    //�滻SQL����еĲ���
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��sqlʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);


        }



        /// <summary>
        /// ���±��濨
        /// </summary>
        /// <param name="cancerAdd">����ʵ��</param>
        /// <returns></returns>
        public int UpdateConcerAddExtReport(FS.HISFC.DCP.Object.CancerAddExt cancerAddExt)
        {
            string strSQL = "";
            if (this.Sql.GetSql("DCP.CancerExt.Update", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�DCP.CancerExt.Update�ֶ�";
                return -1;
            }
            try
            {
                string[] strParm = this.myGetCancerAddExtReportParm(cancerAddExt);//ȡ�����б�
                strSQL = string.Format(strSQL, strParm);    //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��sqlʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// �������濨ɾ��
        /// </summary>
        /// <param name="ReportNO">���</param>
        /// <returns></returns>
        public int DeleteCancerAddExtReport(string ReportNO)
        {
            string strSQL = "";
            if (this.Sql.GetSql("DCP.CancerExt.DeleteByNo", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�DCP.CancerExt.DeleteByNo�ֶ�";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, ReportNO);    //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��sqlʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }



        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="strSQL">sql���</param>
        /// <returns>����ʵ������</returns>
        public ArrayList myGetCancerAddExtReport(string ReportNO)
        {
            ArrayList al = new ArrayList();
            //FS.HISFC.DCP.Object.CancerAddExt  cancerAddExt;
            string strSQL = "";
            if (this.Sql.GetSql("DCP.CancerExt.Query", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�DCP.CancerExt.Query�ֶ�";
                return null;
            }
            string strSQLWhere = "";
            if (this.Sql.GetSql("DCP.CancerExt.Where", ref strSQLWhere) == -1)
            {
                this.Err = "û���ҵ�DCP.CancerExt.Where�ֶ�";
                return null;
            }


            try
            {
                strSQLWhere = string.Format(strSQLWhere, ReportNO);    //�滻SQL����еĲ���
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��strSQLWhereʱ�����" + ex.Message;
                this.WriteErr();
                return null;
            }


            this.ProgressBarText = "���ڼ�������������չ���濨��Ϣ...";
            this.ProgressBarValue = 0;
            this.ExecQuery(strSQL + strSQLWhere);

            try
            {
                while (this.Reader.Read())
                {
                    cancerAddExt = new FS.HISFC.DCP.Object.CancerAddExt();
                    cancerAddExt.Report_No = this.Reader[0].ToString();
                    cancerAddExt.Class_Code = this.Reader[1].ToString();
                    cancerAddExt.Class_Name = this.Reader[2].ToString();
                    cancerAddExt.Item_Code = this.Reader[3].ToString();
                    cancerAddExt.Item_Name = this.Reader[4].ToString();
                    cancerAddExt.Item_Demo = this.Reader[5].ToString();
                    al.Add(cancerAddExt);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��ȡ������չ���濨��Ϣʱȡֵ��ֵ��ʵ�����ʱ����" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }


            this.ProgressBarValue = -1;
            return al;
        }

    }
}
