using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace FS.HISFC.BizLogic.HealthRecord
{
    /// <summary>
    /// ����ת��ҵ��
    /// </summary>
    public class DeptShift : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// ת�Ƽ�¼  ���strType���� 1 �ӱ�������ѯ ��� ����2 �Ӳ�����������ѯ 
        /// </summary>
        /// <param name="inpatienNo">סԺ��ˮ��</param>
        /// <param name="strType">��ʶ</param>
        /// <returns></returns>
        public ArrayList QueryChangeDeptFromShiftApply(string inpatienNo, string strType)
        {
            string strSql = "";
            ArrayList list = new ArrayList();
            if (strType == "1")
            {
                if (this.Sql.GetSql("Case.BaseDML.GetChangeDeptFromShiftApply1", ref strSql) == -1) return null;
            }
            if (strType == "2")
            {
                if (this.Sql.GetSql("Case.BaseDML.GetChangeDeptFromShiftApply2", ref strSql) == -1) return null;
            }
            try
            {
                strSql = string.Format(strSql, inpatienNo);
                //��ѯ
                this.ExecQuery(strSql);
                FS.HISFC.Models.RADT.Location info = null;
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.RADT.Location();
                    info.Dept.ID = Reader[0].ToString(); //���ұ���
                    info.Dept.Name = Reader[1].ToString();//��������
                    info.Dept.Memo = Reader[2].ToString();// ת��ʱ��
                    if (strType == "2")
                    {
                        info.User03 = Reader[3].ToString(); //�������
                        info.Floor = Reader[4].ToString();//�ڿ�����
                    }
                    list.Add(info);
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                list = null;
            }
            return list;
        }

        /// <summary>
        /// ���»�����һ����Ϣ
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertOrUpdate(FS.HISFC.Models.RADT.Location info)
        {
            int rowCount = UpdateChangeDept(info);
            if (rowCount <= 0)
            {
                return InsertChangeDept(info);
            }
            return rowCount;
        }

        /// <summary>
        /// ������ұ���� 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertChangeDept(FS.HISFC.Models.RADT.Location info)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.BaseDML.InsertChangeDept", ref strSql) == -1) return -1;
            try
            {
                //��ȡ����ֵ
                info.User03 = this.GetSequence("Case.BaseDML.InsertChangeDeptSequence");
                if (info.Building == "" || info.Building == null) //����Ա
                {
                    info.Building = this.Operator.ID;
                }
                object[] mm = GetInfo(info);
                if (mm == null)
                {
                    this.Err = "ҵ����ʵ���л�ȡ�ַ��������";
                    return -1;
                }
                strSql = string.Format(strSql, mm);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        public int UpdateChangeDept(FS.HISFC.Models.RADT.Location info)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.BaseDML.UpdateChangeDept", ref strSql) == -1) return -1;
            try
            {
                object[] mm = GetInfo(info);
                if (mm == null)
                {
                    this.Err = "ҵ����ʵ���л�ȡ�ַ��������";
                    return -1;
                }
                strSql = string.Format(strSql, mm);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ɾ��һ��ת������
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int DeleteChangeDept(FS.HISFC.Models.RADT.Location info)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.BaseDML.DeleteChangeDept", ref strSql) == -1) return -1;
            try
            {
                //��ʽ���ַ���
                strSql = string.Format(strSql, info.User02, info.User03); //user02 סԺ��ˮ�� user03�������
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ɾ��һ��ת������
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int DeleteChangeDept(string InpatientNO)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.BaseDML.DeleteChangeDeptByInpatientNO", ref strSql) == -1) return -1;
            try
            {
                //��ʽ���ַ���
                strSql = string.Format(strSql,InpatientNO); //user02 סԺ��ˮ�� user03�������
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        private object[] GetInfo(FS.HISFC.Models.RADT.Location info)
        {
            try
            {
                object[] s = new object[10];
                s[0] = info.User02;		//סԺ��ˮ��
                s[1] = info.User03; //�������      
                s[2] = info.Dept.ID; //���ұ���
                s[3] = info.Dept.Name;//��������
                s[4] = info.Dept.Memo; //ת��ʱ��
                s[5] = "0"; //�ڿ�����
                //s[5] = info.Floor; //�ڿ�����
                s[6] = info.Building; //����Ա 
                return s;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }
        #region  �µ��Ӳ���ת����Ϣ
        /// <summary>
        /// ת�Ƽ�¼��ͼ
        /// </summary>
        /// <param name="inpatienNo">סԺ��ˮ��</param>
        /// <param name="strType">��ʶ</param>
        /// <returns></returns>
        public ArrayList QueryChangeDeptFromView(string inpatienNo)
        {
            string strSql = @"select dept_code,dept_name,change_date,happen_no,days from view_met_cas_changedept where inpatient_no='{0}'";
            ArrayList list = new ArrayList();
            
            try
            {
                strSql = string.Format(strSql, inpatienNo);
                //��ѯ
                this.ExecQuery(strSql);
                FS.HISFC.Models.RADT.Location info = null;
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.RADT.Location();
                    info.Dept.ID = Reader[0].ToString(); //���ұ���
                    info.Dept.Name = Reader[1].ToString();//��������
                    info.Dept.Memo = Reader[2].ToString();// ת��ʱ��
                    info.User03 = Reader[3].ToString(); //�������
                    info.Floor = Reader[4].ToString();//�ڿ�����
                    
                    list.Add(info);
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                list = null;
            }
            return list;
        }

        #endregion
        #region ����
        /// <summary>
        /// ת�Ƽ�¼  ���strType���� 1 �ӱ�������ѯ ��� ����2 �Ӳ�����������ѯ 
        /// </summary>
        /// <param name="inpatienNo">סԺ��ˮ��</param>
        /// <param name="strType">��ʶ</param>
        /// <returns></returns>
        [Obsolete("����,�ô��� QueryChangeDeptFromShiftApply",true)]
        public ArrayList GetChangeDeptFromShiftApply(string inpatienNo, string strType)
        {
            string strSql = "";
            ArrayList list = new ArrayList();
            if (strType == "1")
            {
                if (this.Sql.GetSql("Case.BaseDML.GetChangeDeptFromShiftApply1", ref strSql) == -1) return null;
            }
            if (strType == "2")
            {
                if (this.Sql.GetSql("Case.BaseDML.GetChangeDeptFromShiftApply2", ref strSql) == -1) return null;
            }
            try
            {
                strSql = string.Format(strSql, inpatienNo);
                //��ѯ
                this.ExecQuery(strSql);
                FS.HISFC.Models.RADT.Location info = null;
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.RADT.Location();
                    info.Dept.ID = Reader[0].ToString(); //���ұ���
                    info.Dept.Name = Reader[1].ToString();//��������
                    info.Dept.Memo = Reader[2].ToString();// ת��ʱ��
                    if (strType == "2")
                    {
                        info.User03 = Reader[3].ToString(); //�������
                        info.Floor = Reader[4].ToString();//�ڿ�����
                    }
                    list.Add(info);
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                list = null;
            }
            return list;
        }
        /// <summary>
        /// ��ѯ���� 
        /// </summary>
        /// <param name="inpatienNo">סԺ��ˮ��</param>
        /// <param name="strType">1 ��ѯ�Ƿ���ת�� 2 ��ѯ������Ϣ</param>
        /// <returns></returns>
        public string QueryWardNoBedNOByInpatienNO(string inpatienNo, string strType)
        {
            string strSql = "";
            string returnStr = "";
            ArrayList list = new ArrayList();
            if (strType == "1")
            {
                if (this.Sql.GetSql("Case.BaseDML.ComShiftdate.RB", ref strSql) == -1) return null;
            }
            if (strType == "2")
            {
                if (this.Sql.GetSql("Case.BaseDML.WardNoBedNO", ref strSql) == -1) return null;
            }
            try
            {
                strSql = string.Format(strSql, inpatienNo);
                //��ѯ
                this.ExecQuery(strSql);

                while (this.Reader.Read())
                {
                    returnStr = this.Reader[0].ToString();
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                returnStr = "";
            }
            return returnStr;
        }
        #endregion 
    }
}
