using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace Neusoft.HISFC.Integrate.PhysicalExamination
{
    class Company : IntegrateBase
    {
        #region ����
        //��쵥λ������
        protected static Neusoft.HISFC.Management.PhysicalExamination.Company mgrCompany = new Neusoft.HISFC.Management.PhysicalExamination.Company();
        #endregion

        #region ���к���
        #region ����
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="trans"></param>
        public override void SetTrans(System.Data.IDbTransaction trans)
        {
            this.trans = trans;
            mgrCompany.SetTrans(trans);
        }
        #endregion

        #region ��ѯ���е���쵥λ��Ϣ ���ض�̬����
        /// <summary>
        /// ��ѯ���е���쵥λ��Ϣ ���ض�̬����
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryCompany()
        {
            this.SetDB(mgrCompany);
            return mgrCompany.QueryCompany();
        }
        #endregion

        #region ��ѯĳ��ID����쵥λ��Ϣ
        /// <summary>
        /// ��ѯĳ��ID����쵥λ��Ϣ
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Neusoft.HISFC.Object.Pharmacy.Company GetCompanyByID(string ID)
        {
            this.SetDB(mgrCompany);
            return mgrCompany.GetCompanyByID(ID);
        }
        #endregion

        #region ���ӻ�ɾ��һ������
        /// <summary>
        /// ���ӻ�ɾ��һ������
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public int AddOrUpdate(Neusoft.HISFC.Object.Pharmacy.Company company)
        {
            this.SetDB(mgrCompany);
            return mgrCompany.AddOrUpdate(company);
        }
        #endregion

        #region  ɾ��һ������
        /// <summary>
        /// ɾ��һ������
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public int DeleteInfo(Neusoft.HISFC.Object.Pharmacy.Company company)
        {
            this.SetDB(mgrCompany);
            return mgrCompany.DeleteInfo(company);
        }
        #endregion
        #region �Ƿ��Ѿ�����
        /// <summary>
        /// �Ƿ�
        /// </summary>
        /// <param name="ComCode"></param>
        /// <returns>-1 ���� ��1 û���ù� 2 �ù�</returns>
        public int IsExistCompany(string comCode)
        {
            this.SetDB(mgrCompany);
            return mgrCompany.IsExistCompany(comCode);
        }
        #endregion

        #endregion
    }
}
