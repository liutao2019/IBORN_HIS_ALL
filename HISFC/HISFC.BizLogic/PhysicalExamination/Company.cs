using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace FS.HISFC.BizLogic.PhysicalExamination
{
    /// <summary>
    /// Company<br></br>
    /// [��������: ��쵥λ��]<br></br>
    /// [�� �� ��: �ſ���]<br></br>
    /// [����ʱ��: 2007-03-2]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class Company : FS.FrameWork.Management.Database
    {
        #region ���к���
        #region ��ѯ���е���쵥λ��Ϣ ���ض�̬����
        /// <summary>
        /// ��ѯ���е���쵥λ��Ϣ ���ض�̬����
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryCompany()
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.Company.GetCompany", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.Company�ֶ�!";
                return null;
            }

            //ȡ��λ��Ϣ����
            return this.myGetItem(strSQL);
        }
        #endregion

        #region ��ѯĳ��ID����쵥λ��Ϣ
        /// <summary>
        /// ��ѯĳ��ID����쵥λ��Ϣ
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Pharmacy.Company GetCompanyByID(string ID)
        {
            FS.HISFC.Models.Pharmacy.Company com = new FS.HISFC.Models.Pharmacy.Company();
            string strSQL = "";
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.Company.GetCompany", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.Company�ֶ�!";
                return null;
            }

            //ȡ��λ��Ϣ����
            ArrayList list = this.myGetItem(strSQL);
            if (list == null)
            {
                return null;
            }
            if (list.Count == 0)
            {
                return com;
            }
            return (FS.HISFC.Models.Pharmacy.Company)list[0];
        }
        #endregion

        #region ���ӻ�ɾ��һ������
        /// <summary>
        /// ���ӻ�ɾ��һ������
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public int AddOrUpdate(FS.HISFC.Models.Pharmacy.Company company)
        {
            if (UpdateInfo(company) <= 0)
            {
                if (this.InsertInfo(company) == -1)
                {
                    return -1;
                }
            }
            return 1;
        }
        #endregion

        #region  ɾ��һ������
        /// <summary>
        /// ɾ��һ������
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public int DeleteInfo(FS.HISFC.Models.Pharmacy.Company company)
        {
            string strSQL = "";
            //ȡɾ��������SQL���
            if (this.Sql.GetSql("Exami.Company.DeleteInfo", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.Company.DeleteInfo�ֶ�!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, company.ID);    //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Exami.Company.DeleteInfo" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }
        #endregion

        #region  ��ȡ���
        public string GetCHKSequence()
        {
            return this.GetSequence("Exami.Company.GetSEQ");
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
            string strSQL = "";
            //ȡ���²�����SQL���
            if (this.Sql.GetSql("Exami.Company.IsExistCompany", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.Company.IsExistCompany�ֶ�!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, comCode);            //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Exami.Company.IsExistCompany:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            this.ExecQuery(strSQL);
            while (this.Reader.Read())
            {
                return 2;
            }
            return 1;
        }
        #endregion
        #endregion

        #region  ˽�г�Ա����
        #region ����һ������
        /// <summary>
        /// ����һ������
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        protected int InsertInfo(FS.HISFC.Models.Pharmacy.Company company)
        {
            string strSQL = "";
            //ȡ���������SQL���
            if (this.Sql.GetSql("Exami.Company.AddInfo", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.Company.AddInfo�ֶ�!";
                return -1;
            }
            try
            {
                string[] strParm = myGetParmItem(company);     //ȡ�����б�
                //strSQL = string.Format(strSQL, strParm);            //�滻SQL����еĲ�����
                return this.ExecNoQuery(strSQL, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Exami.Company.AddInfo:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            
        }
        #endregion

        #region ȡ��λ��Ϣ�����б�������һ�����߶���
        /// <summary>
        /// ȡ��λ��Ϣ�����б�������һ�����߶���
        /// ˽�з����������������е���
        /// </summary>
        /// <param name="SQLString">SQL���</param>
        /// <returns>��λ��Ϣ��������</returns>
        protected ArrayList myGetItem(string SQLString)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Pharmacy.Company company = null; //��λ��Ŀ��Ϣʵ��
            //ִ�в�ѯ���
            if (this.ExecQuery(SQLString) == -1)
            {
                this.Err = "��õ�λ��Ŀ��Ϣʱ��ִ��SQL������" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    //ȡ��ѯ����еļ�¼
                    company = new FS.HISFC.Models.Pharmacy.Company();
                    company.ID = this.Reader[0].ToString();  //��˾���� //0 
                    company.Name = this.Reader[1].ToString();//��˾����//1
                    company.Type = this.Reader[2].ToString(); //��λ���//2
                    company.SpellCode = this.Reader[3].ToString();//ƴ����//3
                    company.WBCode = this.Reader[4].ToString();//�����//4
                    company.RelationCollection.Email = this.Reader[5].ToString(); //�ʼ���ַ//5
                    company.RelationCollection.Phone = this.Reader[6].ToString();//�绰����/6
                    company.OpenAccounts = this.Reader[7].ToString(); //�����ʺ�7
                    company.RelationCollection.LinkMan.Name = this.Reader[8].ToString();//��˾��ϵ��8
                    company.RelationCollection.Address = this.Reader[9].ToString(); //��λ��ַ9
                    company.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(Reader[10].ToString()); //��Ч��־10
                    company.Oper.Name = this.Reader[11].ToString(); //����Ա11
                    company.Oper.ID = this.Reader[12].ToString();//����Ա����12
                    company.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[13].ToString()); //����ʱ��13
                    company.RelationCollection.FaxCode = this.Reader[14].ToString();//����
                    company.Memo = this.Reader[15].ToString();//��ע
                    company.User01 = this.Reader[16].ToString(); //�ֻ���
                    al.Add(company);
                }
            }//�׳�����
            catch (Exception ex)
            {
                this.Err = "��õ�λ��Ŀ��Ϣ��Ϣʱ����" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            this.Reader.Close();


            return al;
        }
        #endregion

        #region �޸�һ������
        /// <summary>
        /// �޸�һ������
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        protected int UpdateInfo(FS.HISFC.Models.Pharmacy.Company company)
        {
            string strSQL = "";
            //ȡ���²�����SQL���
            if (this.Sql.GetSql("Exami.Company.UpdateInfo", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.Company.UpdateInfo�ֶ�!";
                return -1;
            }
            try
            {
                string[] strParm = myGetParmItem(company);     //ȡ�����б�
                //strSQL = string.Format(strSQL, strParm);            //�滻SQL����еĲ�����
                return this.ExecNoQuery(strSQL, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Exami.Company.UpdateInfo:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            
        }

        #endregion

        #region ���update����insert��λ��Ŀ��Ϣ��Ĵ����������
        /// <summary>
        /// ���update����insert��λ��Ŀ��Ϣ��Ĵ����������
        /// </summary>
        /// <param name="Item">��λ��Ŀ��Ϣʵ��</param>
        /// <returns>�ַ�������</returns>
        private string[] myGetParmItem(FS.HISFC.Models.Pharmacy.Company company)
        {
            string IsValid = "0";
            if (company.IsValid)
            {
                IsValid = "1";
            }
            string[] strParm ={	company.ID,  //��˾���� //0 
								company.Name ,//��˾����//1
								company.Type , //��λ���//2
								company.SpellCode  ,//ƴ����//3
								company.WBCode ,//�����//4
								company.RelationCollection.Email , //�ʼ���ַ//5
								company.RelationCollection.Phone ,//�绰����/6
								company.OpenAccounts , //�����ʺ�7
								company.RelationCollection.LinkMan.Name,//��˾��ϵ��8
								company.RelationCollection.Address , //��λ��ַ9
								IsValid , //��Ч��־10
								company.Oper.ID ,//����Ա����11
							    company.RelationCollection.FaxCode ,//����12
					            company.Memo,//13
								company.User01 // �ֻ���
							 };
            return strParm;
        }

        #endregion

        #endregion 
    }
}
