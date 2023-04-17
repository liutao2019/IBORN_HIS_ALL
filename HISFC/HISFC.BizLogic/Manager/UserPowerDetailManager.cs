using System;
using System.Collections;
using FS.FrameWork.Models;
using FS.HISFC.Models;
using FS.HISFC.Models.Admin;
using System.Collections.Generic;

namespace FS.HISFC.BizLogic.Manager
{

    /// <summary>
    /// ��ԱȨ�޷�����ϸ����
    /// cuipeng 
    /// </summary>
    public class UserPowerDetailManager : DataBase
    {

        /// <summary>
        /// ���캯��
        /// </summary>
        public UserPowerDetailManager()
        {
        }

        /// <summary>
        /// ȡ������ϸ����
        /// </summary>	
        public ArrayList LoadAll()
        {
            //ȡSQL���
            string sqlstring = PrepareSQL("Manager.UserPowerDetailManager.LoadAll", null);

            return this.myGetList(sqlstring);
        }

        /// <summary>
        /// ������Ա���룬���ݴ���ȡ��Ա�ڸ������е�Ȩ�ޡ�
        /// </summary>
        /// <returns></returns>
        public ArrayList LoadByUserCode(string userCode, string class1Code)
        {
            //ȡSQL���
            string sqlstring = PrepareSQL("Manager.UserPowerDetailManager.LoadByUserCode", userCode, class1Code);

            return this.myGetList(sqlstring);
        }

        /// <summary>
        /// ������Ա���룬���ݴ��࣬����ȡ��Ա�ڱ������е�Ȩ�ޡ�
        /// </summary>
        /// <returns></returns>
        public ArrayList LoadByUserCode(string userCode, string class1Code, string deptCode)
        {
            string sqlstring = PrepareSQL("Manager.UserPowerDetailManager.LoadByUserCode.DeptCode", userCode, class1Code, deptCode);

            return this.myGetList(sqlstring);
        }

        /// <summary>
        /// ���ݴ��࣬����ȡ�ڱ���������Ȩ�޵���Ա��
        /// </summary>
        /// <returns>NeuObject����</returns>
        public ArrayList LoadUser(string class1Code, string deptCode)
        {
            //ȡSQl���
            string strSQL = PrepareSQL("Manager.UserPowerDetailManager.LoadUser", class1Code, deptCode);

            ArrayList al = new ArrayList();
            if (strSQL == string.Empty) return null;
            try
            {
                UserPowerDetail info;
                this.ExecQuery(strSQL);
                while (this.Reader.Read())
                {
                    info = new UserPowerDetail();
                    //ȡ�ֶ�����
                    info.User.ID = this.Reader[0].ToString();    //����
                    info.User.Name = this.Reader[1].ToString();    //����
                    info.Class1Code = this.Reader[2].ToString();    //һ��Ȩ�޷����룬Ȩ������
                    info.Dept.ID = this.Reader[3].ToString();    //Ȩ�޲���
                    info.User01 = this.Reader[4].ToString();   //�Ա�
                    al.Add(info);
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                al.Clear();
            }

            return al;
        }

        /// <summary>
        /// ������Ա���룬���ű���,����Ȩ��ȡ��Ա��ӵ�е�Ȩ�ޡ�
        /// </summary>
        /// <param name="userCode">Ȩ����Ա����</param>
        /// <param name="class2Code">����Ȩ����</param>
        /// <param name="deptCode">���ұ���</param>
        /// <returns>�ɹ�����Ȩ�޼��� ʧ�ܷ���null</returns>
        public List<FS.FrameWork.Models.NeuObject> QueryUserPrivCollection(string userCode, string class2Code, string deptCode)
        {
            string sqlstring = PrepareSQL("Manager.UserPowerDetailManager.LoadUserPriv", userCode, class2Code, deptCode);
            if (sqlstring == null) return null;

            List<FS.FrameWork.Models.NeuObject> al = new List<NeuObject>();
            try
            {
                FS.FrameWork.Models.NeuObject info;
                this.ExecQuery(sqlstring);
                while (this.Reader.Read())
                {
                    info = new NeuObject();
                    info.ID = this.Reader[0].ToString();		//����Ȩ�ޱ���
                    info.Name = this.Reader[1].ToString();		//����Ȩ������
                    info.User01 = this.Reader[2].ToString();	//�Ƿ������Ȩ
                    al.Add(info);
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

            return al;
        }


        /// <summary>
        /// ������Ա����,����Ȩ��ȡ��Ա��ӵ�е�Ȩ�ޡ�{36EF5259-F88E-42EC-AE9F-9EA5FB37D9A5}
        /// </summary>
        /// <param name="userCode">Ȩ����Ա����</param>
        /// <param name="class2Code">����Ȩ����</param>
        /// <returns>�ɹ�����Ȩ�޼��� ʧ�ܷ���null</returns>
        public List<FS.FrameWork.Models.NeuObject> QueryUserPrivByUsercode(string userCode, string class2Code)
        {
            string sqlstring = PrepareSQL("Manager.UserPowerDetailManager.LoadUserPriv1", userCode, class2Code);
            if (sqlstring == null) return null;

            List<FS.FrameWork.Models.NeuObject> al = new List<NeuObject>();
            try
            {
                FS.FrameWork.Models.NeuObject info;
                this.ExecQuery(sqlstring);
                while (this.Reader.Read())
                {
                    info = new NeuObject();
                    info.ID = this.Reader[0].ToString();		//����Ȩ�ޱ���
                    info.Name = this.Reader[1].ToString();		//����Ȩ������
                    info.User01 = this.Reader[2].ToString();	//�Ƿ������Ȩ
                    al.Add(info);
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

            return al;
        }

        /// <summary>
        /// ������Ա���룬����Ȩ�ޱ���ȡ��Աӵ��Ȩ�޵Ĳ���
        /// </summary>
        /// <param name="userCode">����Ա����</param>
        /// <param name="class2Code">����Ȩ����</param>
        /// <returns>�ɹ����ؾ���Ȩ�޵Ŀ��Ҽ��� ʧ�ܷ���null</returns>        
        public List<FS.FrameWork.Models.NeuObject> QueryUserPriv(string userCode, string class2Code)
        {
            //ȡSQL���
            string sqlstring = PrepareSQL("Manager.UserPowerDetailManager.LoadPrivDept", userCode, class2Code);
            if (sqlstring == null)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            //ȡ����
            List<FS.FrameWork.Models.NeuObject> al = new List<NeuObject>();
            try
            {
                FS.FrameWork.Models.NeuObject info;
                this.ExecQuery(sqlstring);
                while (this.Reader.Read())
                {
                    info = new NeuObject();
                    info.ID = this.Reader[0].ToString();  //���ұ���
                    info.Name = this.Reader[1].ToString();  //��������
                    info.User01 = this.Reader[2].ToString();  //����Ȩ�ޱ���
                    info.User02 = this.Reader[3].ToString();  //����Ȩ������
                    info.User03 = this.Reader[4].ToString();  //����Ȩ�������ǣ�1�жϴ���Ȩ��ʱ��ֻҪ����Ȩ�޾�������룬����Ҫ�û�ѡ�����
                    info.Memo = this.Reader[5].ToString();  //��������
                    al.Add(info);
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

            return al;
        }

        /// <summary>
        /// ȡ����Ա��ӵ�е�Ȩ�޲�������
        /// </summary>
        /// <param name="userCode">����Ա����</param>
        /// <param name="class2Code">����Ȩ����</param>
        /// <param name="class3Code">����Ȩ����</param>
        /// <returns>�ɹ����ؾ���Ȩ�޵Ŀ��Ҽ��� ʧ�ܷ���null</returns>
        public List<FS.FrameWork.Models.NeuObject> QueryUserPriv(string userCode, string class2Code, string class3Code)
        {
            //ȡSQL���
            string sqlstring = PrepareSQL("Manager.UserPowerDetailManager.QueryUserPriv.1", userCode, class2Code, class3Code);
            if (sqlstring == null)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            //ȡ����
            List<FS.FrameWork.Models.NeuObject> al = new List<NeuObject>();
            try
            {
                FS.FrameWork.Models.NeuObject info;
                this.ExecQuery(sqlstring);
                while (this.Reader.Read())
                {
                    info = new NeuObject();
                    info.ID = this.Reader[0].ToString();        //���ұ���
                    info.Name = this.Reader[1].ToString();      //��������
                    info.Memo = this.Reader[2].ToString();      //��������
                    al.Add(info);
                }
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// ����Ȩ���ж�
        /// </summary>
        /// <param name="userCode">�û�����</param>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="class2Code">����Ȩ��</param>
        /// <param name="class3Code">����Ȩ��</param>
        /// <returns>����Ȩ�޷���True ������Ȩ�޷���False</returns>
        public bool JudgeUserPriv(string userCode, string deptCode, string class2Code, string class3Code)
        {
            List<FS.FrameWork.Models.NeuObject> userDeptList = this.QueryUserPrivCollection(userCode, class2Code, deptCode);
            if (userDeptList == null)
            {
                return false;
            }

            foreach (FS.FrameWork.Models.NeuObject info in userDeptList)
            {
                if (info.ID == class3Code)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// ȡӵ���ض�Ȩ�޵���Ա�б������ȷ��ʱ���Դ�class1Code��class2Code��class3Code
        /// </summary>
        /// <returns>NeuObject����</returns>
        public ArrayList QueryAllPrivUser(string class1Code, string class2Code, string class3code)
        {
            string strSql = string.Empty;
            //ȡSQl���
            strSql = PrepareSQL("Manager.UserPowerDetailManager.LoadAllUser", class1Code, class2Code, class3code);

            ArrayList al = new ArrayList();
            if (strSql == string.Empty) return null;
            try
            {
                UserPowerDetail info;
                this.ExecQuery(strSql);
                while (this.Reader.Read())
                {
                    info = new UserPowerDetail();
                    //ȡ�ֶ�����
                    info.User.ID = this.Reader[0].ToString();    //����
                    info.User.Name = this.Reader[1].ToString();    //����
                    info.Class1Code = this.Reader[2].ToString();    //һ��Ȩ�޷����룬Ȩ������
                    info.Dept.ID = this.Reader[3].ToString();    //Ȩ�޲���
                    info.User01 = this.Reader[4].ToString();   //�Ա�
                    info.User02 = this.Reader[5].ToString();//��Ա���ڿ���
                    info.User03 = this.Reader[6].ToString();//��Ա���ڲ���
                    al.Add(info);
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                al.Clear();
            }

            return al;
        }
     
        #region ��������

        /// <summary>
        /// ������ԱȨ����ϸ����һ����¼
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertUserPowerDetail(UserPowerDetail info)
        {
            string strSql = "";
            //��ȡSQL���
            if (this.GetSQL("Manager.UserPowerDetailManager.InsertUserPowerDetail", ref strSql) == -1) return -1;
            //��ȡ����
            try
            {
                string[] strParm = myGetParm(info);      //ȡ�����б�
                strSql = string.Format(strSql, strParm);    //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ������ԱȨ����ϸ����һ����¼
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateUserPowerDetail(UserPowerDetail info)
        {
            string strSql = "";
            //��ȡSQL���
            if (this.GetSQL("Manager.UserPowerDetailManager.UpdateUserPowerDetail", ref strSql) == -1) return -1;
            //��ȡ����
            try
            {
                string[] strParm = myGetParm(info);      //ȡ�����б�
                strSql = string.Format(strSql, strParm);    //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="class1"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public int Delete(string userCode, string class1, string deptCode)
        {
            string strSql = "";
            if (this.GetSQL("Manager.UserPowerDetailManager.DeleteUserPowerDetail", ref strSql) == -1) return -1;

            try
            {
                strSql = string.Format(strSql, deptCode, userCode, class1);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// ���ݴ����SQL��䣬ִ�����ݿ��ѯ��������������
        /// </summary>
        /// <returns></returns>
        private ArrayList myGetList(string strSQL)
        {
            if (strSQL == null) return null;
            ArrayList UserPowerDetails = new ArrayList();
            try
            {
                UserPowerDetail info;
                this.ExecQuery(strSQL);
                while (this.Reader.Read())
                {
                    info = new UserPowerDetail();
                    info.Dept.ID = this.Reader[0].ToString();		//���ұ���
                    info.User.ID = this.Reader[1].ToString();		//�û�����
                    info.Class1Code = this.Reader[2].ToString();	//һ��Ȩ�ޱ���
                    info.Class2Code = this.Reader[3].ToString();	//����Ȩ�ޱ���
                    info.PowerLevelClass.Class3Code = this.Reader[4].ToString();	//����Ȩ�ޱ���
                    info.GrantDept = this.Reader[5].ToString();		//��Ȩ����
                    info.GrantEmpl = this.Reader[6].ToString();		//��Ȩ��
                    info.GrantFlag = FrameWork.Function.NConvert.ToBoolean(this.Reader[7].ToString());	//�Ƿ������Ȩ
                    info.Memo = this.Reader[8].ToString();			//��ע
                    UserPowerDetails.Add(info);
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

            return UserPowerDetails;
        }

        /// <summary>
        /// ���update����insertʱ�����������
        /// </summary>
        /// <returns></returns>
        private string[] myGetParm(UserPowerDetail info)
        {
            string[] strParm ={
				info.Dept.ID,                     //���ұ���
				info.User.ID,                     //�û�����
				info.Class1Code,                  //һ��Ȩ�ޱ���
				info.Class2Code,                  //����Ȩ�ޱ���
				info.PowerLevelClass.Class3Code,  //����Ȩ�ޱ���
				info.GrantDept,                   //��Ȩ����
				info.GrantEmpl,                   //��Ȩ��
				FS.FrameWork.Function.NConvert.ToInt32(info.GrantFlag).ToString(), //�Ƿ������Ȩ����
			    this.Operator.ID,				  //�����˱���
				info.Memo	                      //��ע
			};
            return strParm;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        private string PrepareSQL(string sqlName, params string[] values)
        {
            string strSql = string.Empty;
            if (this.GetSQL(sqlName, ref  strSql) == -1)
            {
                this.Err = "�Ҳ���sql���:" + sqlName;
                return null;
            }
            try
            {
                if (values != null)
                    strSql = string.Format(strSql, values);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                strSql = null;
            }
            return strSql;
        }

        #endregion

        #region ��Ч

        /// <summary>
        /// ������Ա���룬����Ȩ�ޱ���ȡ��Աӵ��Ȩ�޵Ĳ���
        /// </summary>
        /// <returns></returns>
        [System.Obsolete("�ع� ����ΪQueryUserPriv ����", true)]
        public ArrayList LoadPrivDept(string userCode, string class2Code)
        {
            return null;
        }


        
        /// <summary>
        /// ������Ա���룬���ű���,����Ȩ��ȡ��Ա��ӵ�е�Ȩ�ޡ�
        /// </summary>
        /// <returns></returns>
        [System.Obsolete("�ع� ����ΪQueryUserPrivCollection ����", true)]
        public ArrayList LoadUserPriv(string userCode, string class2Code, string deptCode)
        {
            return null;
        }
        #endregion

    }

}