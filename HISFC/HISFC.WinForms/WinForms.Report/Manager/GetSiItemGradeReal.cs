using System;
using System.Collections.Generic;
using System.Text;

namespace FS.WinForms.Report.Manager
{
    /// <summary>
    /// ��������: ����ҽ���ȼ�
    /// 
    ///  {112B7DB5-0462-4432-AD9D-17A7912FFDBE} 
    /// </summary>
    public class GetSiItemGradeReal : FS.FrameWork.Management.Database, FS.HISFC.BizProcess.Interface.FeeInterface.IGetSiItemGrade
    {
        #region ����
        System.Collections.Hashtable ht = new System.Collections.Hashtable();
        #endregion


        public GetSiItemGradeReal()
        {
            //���������Ŀ
            this.GetItems();
        }

        #region IGetSiItemGrade ��Ա

        /// <summary>
        /// ���ݺ�ͬ��λ��Ŀ�����ȡҽ���ȼ�
        /// </summary>
        /// <param name="pactID"></param>
        /// <param name="hisItemCode"></param>
        /// <param name="siGrade"></param>
        /// <returns></returns>
        public int GetSiItemGrade(string pactID, string hisItemCode, ref string siGrade)
        {
            if (this.ht.ContainsKey(pactID + hisItemCode))
            {
                siGrade = this.ht[pactID + hisItemCode].ToString();
                //ת��
                siGrade = this.GetGrade(siGrade);
            }
            else
            {
                //�Է�
                siGrade = "4";
            }
            return 1;
        }

        /// <summary>
        /// ������Ŀ�����ȡҽ���ȼ�
        /// </summary>
        /// <param name="hisItemCode">ҽԺ��Ŀ����</param>
        /// <param name="siGrade">ҽ���ȼ�</param>
        /// <returns></returns>
        public int GetSiItemGrade(string hisItemCode, ref string siGrade)
        {


            if (this.ht.ContainsKey(this.defaultPactCode + hisItemCode)) //����
            {
                siGrade = this.ht[this.defaultPactCode + hisItemCode].ToString();
                //ת��
                siGrade = this.GetGrade(siGrade);
            }
            else
            {
                //�Է�
                siGrade = "4";
            }

            //ת��

            return 1;

        }

        private string defaultPactCode = "";

        /// <summary>
        /// ȡ������Ŀ
        /// </summary>
        /// <returns></returns>
        protected virtual System.Collections.Hashtable GetItems()
        {
            string strSql = string.Format("select a.pact_code,a.his_code, a.center_item_grade from fin_com_compare a ");
            int result = this.ExecQuery(strSql);
            string strPactAndItem = string.Empty;
            string strGrade = string.Empty;
            if (result == -1)
            {

                this.Err = "��ѯ������Ϣ��ʧ��";
                return null;
            }
            while (this.Reader.Read())
            {
                if (this.defaultPactCode == "")
                {
                    this.defaultPactCode = this.Reader[0].ToString();
                }
                strPactAndItem = this.Reader[0].ToString() + this.Reader[1].ToString();
                strGrade = this.Reader[2].ToString();
                this.ht.Add(strPactAndItem, strGrade);
            }
            this.Reader.Close();
            return ht;
        }

        /// <summary>
        /// Ԥ��
        /// </summary>
        /// <param name="strCenterGrade"></param>
        /// <returns></returns>
        protected virtual string GetGrade(string strCenterGrade)
        {
            return strCenterGrade;
        }

        #endregion
    }
}
