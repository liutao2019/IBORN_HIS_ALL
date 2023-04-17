using System;
using System.Collections;
using System.Text;

namespace FS.HISFC.BizLogic.Order
{
    /// <summary>
    /// CurePhase ��ժҪ˵����
    /// �������ƽ׶���Ϣ������
    /// </summary>
    public class CurePhase : FS.FrameWork.Management.Database
    {
        public CurePhase()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ˽�з���

        /// <summary>
        /// ��ʽ��SQL���
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="curePhase"></param>
        /// <returns></returns>
        private string FormatCurePhaseInfo(string strSql, FS.HISFC.Models.Order.CurePhase curePhase)
        {
            string mySql = "";
            try
            {
                System.Object[] s = {curePhase.PatientID,//סԺ��ˮ��
									 curePhase.ID,//���к�
									 curePhase.Dept.ID,//���Ҵ���
									 curePhase.Dept.Name,//��������
									 curePhase.CurePhaseInfo.ID,//���ƽ׶���Ϣ����
									 curePhase.CurePhaseInfo.Name,//���ƽ׶���Ϣ����
									 curePhase.StartTime.ToString(),//��ʼʱ��
									 curePhase.EndTime.ToString(),//����ʱ��
									 curePhase.Doctor.ID,//���
                                     curePhase.Doctor.Name,//��������
                                     FS.FrameWork.Function.NConvert.ToInt32(curePhase.IsVaild).ToString(),//��Ч��
									 curePhase.Remark,//��ע
									 curePhase.Oper.ID,//����Ա
									 curePhase.Oper.OperTime.ToString(),//��������
									};
				string myErr = "";
                if (FS.FrameWork.Public.String.CheckObject(out myErr, s) == -1)
                {
                    this.Err = myErr;
                    this.WriteErr();
                    return null;
                }
                mySql = string.Format(strSql, s);
            }
            catch (System.Exception ex)
            {
                this.Err = "����ֵʱ�����" + ex.Message;
                this.WriteErr();
                return null;
            }
            return mySql;
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        private ArrayList myCurePhaseQuery(string strSql)
        {
            ArrayList al = new ArrayList();

            if (this.ExecQuery(strSql) == -1) return null;
            
            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Order.CurePhase curePhase = new FS.HISFC.Models.Order.CurePhase();
                    try
                    {
                        curePhase.PatientID = this.Reader[0].ToString();
                        curePhase.ID = this.Reader[1].ToString();//���к�
                        curePhase.Dept.ID = this.Reader[2].ToString();//���Ҵ���
                        curePhase.Dept.Name = this.Reader[3].ToString();//��������
                        curePhase.CurePhaseInfo.ID = this.Reader[4].ToString();//���ƽ׶���Ϣ����
                        curePhase.CurePhaseInfo.Name = this.Reader[5].ToString();//���ƽ׶���Ϣ����
                        curePhase.StartTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6].ToString());//��ʼʱ��
                        curePhase.EndTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[7].ToString());//����ʱ��
                        curePhase.Doctor.ID = this.Reader[8].ToString();//���
                        curePhase.Doctor.Name = this.Reader[9].ToString();//��������
                        curePhase.IsVaild = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[10].ToString());//��Ч��
                        curePhase.Remark = this.Reader[11].ToString();//��ע
                        curePhase.Oper.ID = this.Reader[12].ToString();//����Ա
                        curePhase.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[13].ToString());//��������
                    }
                    catch (Exception ex)
                    {
                        this.Err = "��û������ƽ׶���Ϣ����" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    al.Add(curePhase);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��û������ƽ׶���Ϣ����" + ex.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return al;
        }

        #endregion

        #region ���з���

        /// <summary>
        /// ����һ������
        /// </summary>
        /// <param name="curePhase"></param>
        /// <returns></returns>
        public int InsertCurePhase(FS.HISFC.Models.Order.CurePhase curePhase)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Order.CurePhase.Insert.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            strSql = this.FormatCurePhaseInfo(strSql, curePhase);
            if (strSql == null)
            {
                this.Err = "��ʽ��Sql���ʱ����";
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// �������ƽ׶���Ϣ
        /// </summary>
        /// <param name="curePhase"></param>
        /// <returns></returns>
        public int UpdateCurePhase(FS.HISFC.Models.Order.CurePhase curePhase)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Order.CurePhase.Update.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            strSql = this.FormatCurePhaseInfo(strSql, curePhase);
            if (strSql == null)
            {
                this.Err = "��ʽ��Sql���ʱ����";
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ȡ���ƽ׶���Ϣ����
        /// </summary>
        /// <returns></returns>
        public string GetNewCurePhaseID()
		{
			string sql = "";
            if (this.Sql.GetCommonSql("Order.CurePhase.GetNewCurePhaseID", ref sql) == -1) return null;
			string strReturn = this.ExecSqlReturnOne(sql);
			if(strReturn == "-1" || strReturn == "") return null;
			return strReturn;
		}

        /// <summary>
        /// ����סԺ��ˮ�Ų�ѯ
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <returns></returns>
        public ArrayList QueryCurePhaseByInPatientNO(string inPatientNO)
        {
            string strSql = "";
            string strSql1 = "";
            ArrayList al = new ArrayList();

            if (this.Sql.GetCommonSql("Order.CurePhase.QueryCurePhase.Select", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }

            if (this.Sql.GetCommonSql("Order.CurePhase.QueryCurePhase.Where1", ref strSql1) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            strSql = strSql + " " + string.Format(strSql1,inPatientNO);

            return this.myCurePhaseQuery(strSql);
        }

        /// <summary>
        /// �������кŲ�ѯ
        /// </summary>
        /// <param name="seqNO"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Order.CurePhase QuerCurePhaseBySeq(string seqNO)
        {
            string strSql = "";
            string strSql1 = "";
            ArrayList al = new ArrayList();

            if (this.Sql.GetCommonSql("Order.CurePhase.QueryCurePhase.Select", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }

            if (this.Sql.GetCommonSql("Order.CurePhase.QueryCurePhase.Where2", ref strSql1) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                strSql = strSql + " " + string.Format(strSql1, seqNO);
            }
            catch 
            {
                this.Err = "����������ԣ�";
                return null;
            }
            al = this.myCurePhaseQuery(strSql);
            if (al != null && al.Count > 0)
            {
                return al[0] as FS.HISFC.Models.Order.CurePhase;
            }
            else
            {
                return null;
            }
        }

        #endregion

    }
}
