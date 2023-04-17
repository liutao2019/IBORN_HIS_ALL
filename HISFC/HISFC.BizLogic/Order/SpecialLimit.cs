using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.BizLogic.Order
{
    /// <summary>
    /// 
    /// ҽ��ҩƷ���ƹ�����
    /// </summary>
    public class SpecialLimit : FS.FrameWork.Management.Database
    {
        public SpecialLimit()
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
        /// <param name="pharmacyLimit"></param>
        /// <returns></returns>
        private string FormatPharcamyLimit(string strSql, FS.HISFC.Models.Order.PharmacyLimit pharmacyLimit)
        {
            string mySql = "";
            try
            {
                System.Object[] s = {pharmacyLimit.ID,//סԺ��ˮ��
									 FS.FrameWork.Function.NConvert.ToInt32(pharmacyLimit.IsLeaderCheck).ToString(),//���к�
									 FS.FrameWork.Function.NConvert.ToInt32(pharmacyLimit.IsNeedRecipe).ToString(),//���Ҵ���
                                     FS.FrameWork.Function.NConvert.ToInt32(pharmacyLimit.IsValid).ToString(),//��Ч��
									 pharmacyLimit.Remark,//��ע
									 pharmacyLimit.Oper.ID,//����Ա
									 pharmacyLimit.Oper.OperTime.ToString(),//��������
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
        private ArrayList myPharmacyLimitQuery(string strSql)
        {
            ArrayList al = new ArrayList();

            if (this.ExecQuery(strSql) == -1) return null;
            
            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Order.PharmacyLimit obj = new FS.HISFC.Models.Order.PharmacyLimit();
                    try
                    {
                        obj.ID = this.Reader[0].ToString();
                        obj.IsLeaderCheck = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[1].ToString());
                        obj.IsNeedRecipe = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[2].ToString());
                        obj.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[3].ToString());//��Ч��
                        obj.Remark = this.Reader[4].ToString();//��ע
                        obj.Oper.ID = this.Reader[5].ToString();//����Ա
                        obj.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6].ToString());//��������
                    }
                    catch (Exception ex)
                    {
                        this.Err = "���ҽ��ҩƷ������Ϣ����" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    al.Add(obj);
                }
            }
            catch (Exception ex)
            {
                this.Err = "���ҽ��ҩƷ������Ϣ����" + ex.Message;
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
        /// <param name="pharmacyLimit"></param>
        /// <returns></returns>
        public int InsertSpecialLimit(FS.HISFC.Models.Order.PharmacyLimit pharmacyLimit)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Order.SpecialLimit.Insert.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            strSql = this.FormatPharcamyLimit(strSql, pharmacyLimit);
            if (strSql == null)
            {
                this.Err = "��ʽ��Sql���ʱ����";
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        /// <param name="pharmacyLimit"></param>
        /// <returns></returns>
        public int UpdateSpecialLimit(FS.HISFC.Models.Order.PharmacyLimit pharmacyLimit)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Order.SpecialLimit.Update.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            strSql = this.FormatPharcamyLimit(strSql, pharmacyLimit);
            if (strSql == null)
            {
                this.Err = "��ʽ��Sql���ʱ����";
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pharmacyID"></param>
        /// <returns></returns>
        public int DeleteSpecialLimitByID(string pharmacyID)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Order.SpecialLimit.Delete.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, pharmacyID);
            }
            catch
            {
                this.Err = "����������ԣ�";
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ��ѯȫ������
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryPharmacyLimit()
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Order.SpecialLimit.Select.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            return this.myPharmacyLimitQuery(strSql);
        }

        /// <summary>
        /// ��ѯһ��
        /// </summary>
        /// <param name="pharmacyID"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Order.PharmacyLimit QueryPharmacyLimitByID(string pharmacyID)
        {
            string strSql = "";
            string strSql1 = "";
            ArrayList al = new ArrayList();

            if (this.Sql.GetCommonSql("Order.SpecialLimit.Select.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }

            if (this.Sql.GetCommonSql("Order.SpecialLimit.Where.1", ref strSql1) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                strSql = strSql + " " + string.Format(strSql1, pharmacyID);
            }
            catch
            {
                this.Err = "����������ԣ�";
                return null;
            }
            al = this.myPharmacyLimitQuery(strSql);
            if (al != null && al.Count > 0)
            {
                return al[0] as FS.HISFC.Models.Order.PharmacyLimit;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ��ѯ��Ч����
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryValid()
        {
            string strSql = "";
            string strSql1 = "";
            ArrayList al = new ArrayList();

            if (this.Sql.GetCommonSql("Order.SpecialLimit.Select.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }

            if (this.Sql.GetCommonSql("Order.SpecialLimit.Where.2", ref strSql1) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                strSql = strSql + " " + strSql1;
            }
            catch
            {
                this.Err = "����������ԣ�";
                return null;
            }

            return this.myPharmacyLimitQuery(strSql);
        }

        #endregion

    }
}
