using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using FS.FrameWork.Function;
using FS.HISFC.Models.HealthRecord.EnumServer;

namespace FS.HISFC.BizLogic.HealthRecord
{
    /// <summary>
    /// <br>ICDCategory</br>
    /// <Font color='#FF1111'>[��������: ICD����ҵ���]</Font><br></br>
    /// [�� �� ��: ]<br>���S</br>
    /// [����ʱ��: ]<br>2009-06-08</br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///		/>
    /// </summary>
    public class ICDCategory : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ICDCategory()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ����

        #region ˽��

        /// <summary>
        /// ���update����insert�Ĵ����������
        /// </summary>
        /// <param name="undrug">��ҩƷʵ��</param>
        /// <returns>��������</returns>
        private string[] GetParams(FS.HISFC.Models.HealthRecord.ICDCategory ICDCategory)
        {
            string[] args = new string[]
			{	
				ICDCategory.ID,
                ICDCategory.ParentID,
                ICDCategory.SeqNO,
                ICDCategory.Name,
                ICDCategory.SpellCode,
                ICDCategory.SortID,
                ICDCategory.Range,
                ICDCategory.Sfbr
			};

            return args;
        }

        private ArrayList GetListBySql(string sql)
        {
            ArrayList alICDTemps = new ArrayList(); //���ڷ��ط�ҩƷ��Ϣ������

            //ִ�е�ǰSql���
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = this.Sql.Err;

                return null;
            }

            try
            {
                //ѭ����ȡ����
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.HealthRecord.ICDCategory ICDCategory = new FS.HISFC.Models.HealthRecord.ICDCategory();

                    ICDCategory.ID = Reader[0].ToString();
                    ICDCategory.ParentID = Reader[1].ToString();
                    ICDCategory.SeqNO = Reader[2].ToString();
                    ICDCategory.Name = Reader[3].ToString();
                    ICDCategory.SpellCode = Reader[4].ToString();
                    ICDCategory.SortID = Reader[5].ToString();
                    ICDCategory.Range = Reader[6].ToString();
                    ICDCategory.Sfbr = Reader[7].ToString();

                    alICDTemps.Add(ICDCategory);
                }//ѭ������
            }
            catch (Exception e)
            {
                this.Err = "�����Ϸ��������Ϣ����" + e.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return alICDTemps;
        }


        private int SetICDCategory(FS.HISFC.Models.HealthRecord.ICDCategory icdTemp, string sqlIndex)
        {
            string sql = "";

            try
            {
                //��ȡ��ѯSQL���
                if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
                {
                    this.Err = "��ȡSQL���ʧ��,����:" + sqlIndex + "";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1; // ���ִ��󷵻�null
            }

            string[] strParam = this.GetParams(icdTemp);

            sql = string.Format(sql, strParam);
            return this.ExecNoQuery(sql);
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ѯICDģ��
        /// </summary>
        public ArrayList QueryCategoryBySortID(string sortID)
        {
            //�����ַ����� ,�洢��ѯ����SQL���
            string strSelect = "";
            //�����ַ����� ,�洢WHERE ����SQL���
            string strWhere = "";
            //���嶯̬���� ,�洢��ѯ������Ϣ
            ArrayList arryList = new ArrayList();
            try
            {
                //��ȡ��ѯSQL���
                if (this.Sql.GetSql("HealthRecord.ICDCategory.Query", ref strSelect) == -1)
                {
                    this.Err = "��ȡSQL���ʧ��,����:HealthRecord.ICDTemplate.Query";
                    return null;
                }
                //��ȡ��ѯwhere���
                if (this.Sql.GetSql("HealthRecord.ICDCategory.Where.BySortID", ref strWhere) == -1)
                {
                    this.Err = "��ȡSQL���ʧ��,����:HealthRecord.ICDCategory.Where.BySortID";
                    return null;
                }
                try
                {
                    //��ʽ��SQL��� --------------------------------icd_code-valid_state-type-owner
                    strSelect = string.Format(strSelect + strWhere, sortID);
                }
                catch (Exception ex)
                {
                    this.Err = "SQL��丳ֵ����!" + ex.Message;
                }
                //��ȡ����
                arryList = this.GetListBySql(strSelect);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null; // ���ִ��󷵻�null
            }
            finally
            {
                if (this.Reader != null)
                {
                    this.Reader.Close();
                }
            }

            return arryList;
        }

        /// <summary>
        /// ��ѯICDģ��
        /// </summary>
        public ArrayList QueryCategoryByParentID(string parentID, string sortID)
        {
            //�����ַ����� ,�洢��ѯ����SQL���
            string strSelect = "";
            //�����ַ����� ,�洢WHERE ����SQL���
            string strWhere = "";
            //���嶯̬���� ,�洢��ѯ������Ϣ
            ArrayList arryList = new ArrayList();
            try
            {
                //��ȡ��ѯSQL���
                if (this.Sql.GetSql("HealthRecord.ICDCategory.Query", ref strSelect) == -1)
                {
                    this.Err = "��ȡSQL���ʧ��,����:HealthRecord.ICDTemplate.Query";
                    return null;
                }
                //��ȡ��ѯwhere���
                if (this.Sql.GetSql("HealthRecord.ICDCategory.Where.ByParentID", ref strWhere) == -1)
                {
                    this.Err = "��ȡSQL���ʧ��,����:HealthRecord.ICDCategory.Where.ByParentID";
                    return null;
                }
                try
                {
                    //��ʽ��SQL��� --------------------------------icd_code-valid_state-type-owner
                    strSelect = string.Format(strSelect + strWhere, parentID, sortID);
                }
                catch (Exception ex)
                {
                    this.Err = "SQL��丳ֵ����!" + ex.Message;
                }
                //��ȡ����
                arryList = this.GetListBySql(strSelect);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null; // ���ִ��󷵻�null
            }
            finally
            {
                this.Reader.Close();
            }

            return arryList;
        }

        /// <summary>
        /// ��ѯICDģ��
        /// </summary>
        public ICDCategory GetICDCategory(string ID)
        {
            //�����ַ����� ,�洢��ѯ����SQL���
            string strSelect = "";
            //�����ַ����� ,�洢WHERE ����SQL���
            string strWhere = "";
            //���嶯̬���� ,�洢��ѯ������Ϣ
            ArrayList arryList = new ArrayList();
            try
            {
                //��ȡ��ѯSQL���
                if (this.Sql.GetSql("HealthRecord.ICDCategory.Query", ref strSelect) == -1)
                {
                    this.Err = "��ȡSQL���ʧ��,����:HealthRecord.ICDTemplate.Query";
                    return null;
                }
                //��ȡ��ѯwhere���
                if (this.Sql.GetSql("HealthRecord.ICDCategory.Where.ByID", ref strWhere) == -1)
                {
                    this.Err = "��ȡSQL���ʧ��,����:HealthRecord.ICDCategory.Where.ByID";
                    return null;
                }
                try
                {
                    //��ʽ��SQL��� --------------------------------icd_code-valid_state-type-owner
                    strSelect = string.Format(strSelect + strWhere, ID);
                }
                catch (Exception ex)
                {
                    this.Err = "SQL��丳ֵ����!" + ex.Message;
                }
                //��ȡ����
                arryList = this.GetListBySql(strSelect);
                if (arryList != null && arryList.Count > 0)
                {
                    return arryList[0] as ICDCategory;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null; // ���ִ��󷵻�null
            }
            finally
            {
                this.Reader.Close();
            }

            return null;
        }

        /// <summary>
        /// ������Ϸ���
        /// </summary>
        /// <param name="ICDCategory"></param>
        /// <returns></returns>
        public int Insert(FS.HISFC.Models.HealthRecord.ICDCategory ICDCategory)
        {
            return SetICDCategory(ICDCategory, "HealthRecord.ICDCategory.Insert");
        }

        /// <summary>
        /// ������Ϸ���
        /// </summary>
        /// <param name="ICDCategory"></param>
        /// <returns></returns>
        public int Update(FS.HISFC.Models.HealthRecord.ICDCategory ICDCategory)
        {
            return SetICDCategory(ICDCategory, "HealthRecord.ICDCategory.Update");
        }

        /// <summary>
        /// ɾ����Ϸ���
        /// </summary>
        /// <param name="ICDCategory"></param>
        /// <returns></returns>
        public int Delete(FS.HISFC.Models.HealthRecord.ICDCategory ICDCategory)
        {
            return SetICDCategory(ICDCategory, "HealthRecord.ICDCategory.Delete");
        }

        #endregion

        #endregion
    }
}
