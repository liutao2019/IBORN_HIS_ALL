using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using FS.FrameWork.Function;

namespace FS.HISFC.BizLogic.Manager
{
    /// <summary>
    /// ������<br>ICDMedicare</br>
    /// <Font color='#FF1111'>[��������: �û�Ĭ��������Ϣ��]</Font><br></br>
    /// [�� �� ��: ]<br>���S</br>
    /// [����ʱ��: ]<br>2009-04-17</br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///		/>
    /// </summary>
    public class UserDefaultSetting : DataBase
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public UserDefaultSetting()
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
        private string[] GetParams(FS.HISFC.Models.Base.UserDefaultSetting setting)
        {
            string[] args = 
			{	
				setting.Empl.ID,
                setting.Setting1,
                setting.Setting2,
                setting.Setting3,
                setting.Setting4,
                setting.Setting5,
                setting.Setting6,
                setting.Setting7,
                setting.Setting8,
                setting.Setting9,
                setting.Setting10,
                setting.Oper.ID,
                setting.Oper.OperTime.ToString()
			};

            return args;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private ArrayList GetListBySql(string sql)
        {
            ArrayList alSettings = new ArrayList(); //���ڷ��ط�ҩƷ��Ϣ������

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
                    FS.HISFC.Models.Base.UserDefaultSetting setting = new FS.HISFC.Models.Base.UserDefaultSetting();

                    setting.Empl.ID = this.Reader[0].ToString();
                    setting.Setting1 = this.Reader[1].ToString();
                    setting.Setting2 = this.Reader[2].ToString();
                    setting.Setting3 = this.Reader[3].ToString();
                    setting.Setting4 = this.Reader[4].ToString();
                    setting.Setting5 = this.Reader[5].ToString();
                    setting.Setting6 = this.Reader[6].ToString();
                    setting.Setting7 = this.Reader[7].ToString();
                    setting.Setting8 = this.Reader[8].ToString();
                    setting.Setting9 = this.Reader[9].ToString();
                    setting.Setting10 = this.Reader[10].ToString();
                    setting.Oper.ID = this.Reader[11].ToString();
                    setting.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[12].ToString());

                    alSettings.Add(setting);
                }//ѭ������
            }
            catch (Exception e)
            {
                this.Err = "����û�������Ϣ����" + e.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return alSettings;
        }

        private int SetUserSettings(FS.HISFC.Models.Base.UserDefaultSetting setting, string sqlIndex)
        {
            string sql = "";

            try
            {
                //��ȡ��ѯSQL���
                if (this.GetSQL(sqlIndex, ref sql) == -1)
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

            string[] strParam = this.GetParams(setting);

            sql = string.Format(sql, strParam);
            return this.ExecNoQuery(sql);
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ѯ�û�������Ϣ
        /// </summary>
        public FS.HISFC.Models.Base.UserDefaultSetting Query(string emplCode)
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
                if (this.GetSQL("Manager.UserDefaultSetting.Query", ref strSelect) == -1)
                {
                    this.Err = "��ȡSQL���ʧ��,����:Manager.UserDefaultSetting.Query";
                    return null;
                }
                //��ȡ��ѯwhere���
                if (this.GetSQL("Manager.UserDefaultSetting.Where", ref strWhere) == -1)
                {
                    this.Err = "��ȡSQL���ʧ��,����:Manager.UserDefaultSetting.Where";
                    return null;
                }
                try
                {
                    //��ʽ��SQL��� --------------------------------icd_code-valid_state-type-owner
                    strSelect = string.Format(strSelect + strWhere, emplCode);
                }
                catch (Exception ex)
                {
                    this.Err = "SQL��丳ֵ����!" + ex.Message;
                }
                //��ȡ����
                arryList = this.GetListBySql(strSelect);
                if (arryList == null || arryList.Count == 0)
                {
                    this.Err = "û���ҵ��ò�����������Ϣ!";
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

            return arryList[0] as FS.HISFC.Models.Base.UserDefaultSetting;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public int Insert(FS.HISFC.Models.Base.UserDefaultSetting setting)
        {
            return SetUserSettings(setting, "Manager.UserDefaultSetting.Insert");
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public int Update(FS.HISFC.Models.Base.UserDefaultSetting setting)
        {
            return SetUserSettings(setting, "Manager.UserDefaultSetting.Update");
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public int Delete(FS.HISFC.Models.Base.UserDefaultSetting setting)
        {
            return SetUserSettings(setting, "Manager.UserDefaultSetting.Delete");
        }

        #endregion

        #endregion
    }
}
