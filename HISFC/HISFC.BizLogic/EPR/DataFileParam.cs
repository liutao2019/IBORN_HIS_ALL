using System;
using System.Collections;
namespace FS.HISFC.BizLogic.EPR
{
    /// <summary>
    /// Class1 ��ժҪ˵����
    /// </summary>
    public class DataFileParam : FS.FrameWork.Management.Database, FS.HISFC.Models.Base.IManagement
    {
        public DataFileParam()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #region IManagement ��Ա
        /// <summary>
        /// ����б�
        /// </summary>
        /// <returns></returns>
        public System.Collections.ArrayList GetList()
        {
            // TODO:  ��� DataFileParam.GetList ʵ��
            string sql = "";
            if (this.Sql.GetSql("Manager.DataFileParam.GetList", ref sql) == -1) return null;
            if (this.ExecQuery(sql) == -1) return null;
            ArrayList al = new ArrayList();
            //if(this.Reader.HasRows)

            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.File.DataFileParam datafileparam = new FS.HISFC.Models.File.DataFileParam();
                    datafileparam.ID = this.Reader[0].ToString();
                    datafileparam.Name = this.Reader[1].ToString();
                    datafileparam.IsInDB = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[2].ToString());
                    datafileparam.Type = this.Reader[3].ToString();
                    datafileparam.IP = this.Reader[4].ToString();
                    datafileparam.Folders = this.Reader[5].ToString();
                    datafileparam.ModualFolders = this.Reader[6].ToString();
                    datafileparam.Http = this.Reader[7].ToString();
                    datafileparam.User01 = this.Reader[8].ToString();
                    datafileparam.User02 = this.Reader[9].ToString();
                    al.Add(datafileparam);
                }

            }
            catch
            {
                return null;
            }

            this.Reader.Close();
            return al;
        }

        public int Del(object obj)
        {
            // TODO:  ��� DataFileParam.Del ʵ��
            return 0;
        }

        public int SetList(System.Collections.ArrayList al)
        {
            // TODO:  ��� DataFileParam.SetList ʵ��
            return 0;
        }

        public FS.FrameWork.Models.NeuObject Get(object strType)
        {
            // TODO:  ��� DataFileParam.Get ʵ��
            //�ӿ����� Manager.DataFileParam.Get.1
            //<!--0 ϵͳ���, 1 ����, 2 �Ƿ������ݿ�,3 ���ݱ���,4 ip ,5 �����ļ���,6 �����ļ��� ,7 http-->
            string strSql = "";
            if (this.Sql.GetSql("Manager.DataFileParam.Get.1", ref strSql) == -1) return null;
            FS.HISFC.Models.File.DataFileParam DataFileParam = new FS.HISFC.Models.File.DataFileParam();
            try
            {
                //DataFileParam=sender as FS.HISFC.Models.Base.DataFileParam;
                strSql = string.Format(strSql, strType);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }
            if (this.ExecQuery(strSql) == -1) return null;
            try
            {
                this.Reader.Read();
            }
            catch { return null; }
            try
            {
                DataFileParam.Type = this.Reader[0].ToString();//ϵͳ���
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
            }
            try
            {
                DataFileParam.ID = this.Reader[1].ToString();//����
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
            }
            try
            {
                DataFileParam.IsInDB = System.Convert.ToBoolean(int.Parse(this.Reader[2].ToString()));//�Ƿ������ݿ�
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
            }
            try
            {
                DataFileParam.Name = this.Reader[3].ToString();//���ݱ���
                DataFileParam.IP = this.Reader[4].ToString();//IP
                DataFileParam.Folders = this.Reader[5].ToString();//�����ļ�����
                DataFileParam.ModualFolders = this.Reader[6].ToString();//�����ļ�����
                DataFileParam.Http = this.Reader[7].ToString();//�����ļ�����
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();

            }
            this.Reader.Close();

            //�ӿ����� Manager.Ftp.Get.1
            //<!--0 ip,1 username,2 pwd,3 root-->
            if (this.Sql.GetSql("Manager.Ftp.Get.1", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, DataFileParam.IP);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }
            if (this.ExecQuery(strSql) == -1) return null;
            try
            {
                if (DataFileParam.IsInDB) //���ݿ����治��ȡftp
                {
                }
                else
                {
                    if (this.ExecQuery(strSql) == -1) return null;
                    if (this.Reader.Read())
                    {
                        DataFileParam.UserName = this.Reader[1].ToString();
                        DataFileParam.PassWord = this.Reader[2].ToString();
                        DataFileParam.Root = this.Reader[3].ToString();
                    }
                    else
                    {
                        this.Err = "û������ftp!";
                        return null;
                    }
                }
                this.Reader.Close();

            }
            catch { return null; }

            return DataFileParam;
        }

        /// <summary>
        /// ���ò���
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Set(FS.FrameWork.Models.NeuObject obj)
        {
            // TODO:  ��� DataFileParam.Set ʵ��
            return 0;
        }
        #endregion
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Update(FS.FrameWork.Models.NeuObject obj)
        {
            // TODO:  ��� DataFileParam.Update ʵ��
            string sql = "", sql1 = "";
            if (this.Sql.GetSql("Manager.DataFileParam.Update", ref sql) == -1) return -1;
            if (this.Sql.GetSql("Manager.DataFileParam.Update.2", ref sql1) == -1) return -1;
            FS.HISFC.Models.File.DataFileParam o = obj as FS.HISFC.Models.File.DataFileParam;
            if (o == null) return -1;
            try
            {
                sql = string.Format(sql, o.ID, o.Name, o.IsInDB.GetHashCode().ToString(),
                    o.Type, o.IP, o.Folders, o.ModualFolders, o.Http, this.Operator.ID);
                sql1 = string.Format(sql1, o.ID, o.Name, o.IsInDB.GetHashCode().ToString(),
                    o.Type, o.IP, o.Folders, o.ModualFolders, o.Http, this.Operator.ID);
            }
            catch (Exception ex) { this.Err = ex.Message; return -1; }
            if (this.ExecNoQuery(sql) <= 0) return -1;
            if (this.ExecNoQuery(sql1) < 0) return -1;
            return 0;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Insert(FS.FrameWork.Models.NeuObject obj)
        {
            string sql = "";
            if (this.Sql.GetSql("Manager.DataFileParam.Insert", ref sql) == -1) return -1;
            FS.HISFC.Models.File.DataFileParam o = obj as FS.HISFC.Models.File.DataFileParam;
            if (o == null) return -1;
            try
            {
                sql = string.Format(sql, o.ID, o.Name, o.IsInDB.GetHashCode().ToString(),
                    o.Type, o.IP, o.Folders, o.ModualFolders, o.Http, this.Operator.ID);
            }
            catch (Exception ex) { this.Err = ex.Message; return -1; }
            if (this.ExecNoQuery(sql) <= 0) return -1;
            return 0;
        }

    }
}
