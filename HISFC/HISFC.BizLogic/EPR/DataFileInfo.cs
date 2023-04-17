using System;
using System.Collections;
namespace FS.HISFC.BizLogic.EPR
{
    /// <summary>
    /// DataFileInfo ��ժҪ˵����
    /// </summary>
    public class DataFileInfo : FS.FrameWork.Management.Database, FS.HISFC.Models.Base.IManagement
    {
        public DataFileInfo()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #region IManagement ��Ա
        public System.Collections.ArrayList GetList()
        {
            return null;
        }

        /// <summary>
        /// Ĭ����ʾ�����ļ�
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public System.Collections.ArrayList GetList(FS.HISFC.Models.File.DataFileParam param)
        {
            return this.GetList(param, 0, true);
        }



        /// <summary>
        /// ���ģ���б�
        /// </summary>
        /// <param name="param"></param>
        /// <param name="isAll"></param>
        /// <returns></returns>
        public System.Collections.ArrayList GetModualList(FS.HISFC.Models.File.DataFileParam param, bool isAll)
        {
            string strSql = "";
            if (isAll)//ȫԺģ�壬������Ч��
            {
                if (this.Sql.GetSql("Manager.DataFileInfo.Select.Modual.All.1", ref strSql) == -1) return null;
                try
                {
                    strSql = string.Format(strSql, param.ID, param.Type);
                }
                catch
                {
                    this.Err = "�����������!";
                    return null;
                }
            }
            else
            {
                if (this.Sql.GetSql("Manager.DataFileInfo.Select.Modual.All.2", ref strSql) == -1) return null;
                try
                {
                    strSql = string.Format(strSql, param.ID, param.Type, ((FS.HISFC.Models.Base.Employee)this.Operator).Dept.ID);
                }
                catch
                {
                    this.Err = "�����������!";
                    return null;
                }
            }
            return this.myGetFiles(param, strSql);
        }

        /// <summary>
        /// ��ʾ�����ļ� 0 �����ļ� ,1 ģ���ļ�
        /// </summary>
        /// <param name="param"></param>
        /// <param name="iType"></param>
        /// <param name="isAll">�Ƿ�ȫԺ ֻ��ģ���ļ���Ч</param>
        /// <returns></returns>
        public System.Collections.ArrayList GetList(FS.HISFC.Models.File.DataFileParam param, int iType, bool isAll)
        {
            string strSql = "";

            if (iType == 0)//�����ļ� 
            {
                if (this.Sql.GetSql("Manager.DataFileInfo.Select.Data.1", ref strSql) == -1) return null;
                try
                {
                    strSql = string.Format(strSql, param.ID, param.Type);
                }
                catch
                {
                    this.Err = "�����������!";
                    return null;
                }
            }
            else//ģ���ļ�
            {
                if (isAll)//ȫԺģ�壬������Ч��
                {
                    if (this.Sql.GetSql("Manager.DataFileInfo.Select.Modual.1", ref strSql) == -1) return null;
                    try
                    {
                        strSql = string.Format(strSql, param.ID, param.Type);
                    }
                    catch
                    {
                        this.Err = "�����������!";
                        return null;
                    }
                }
                else
                {
                    if (this.Sql.GetSql("Manager.DataFileInfo.Select.Modual.2", ref strSql) == -1) return null;
                    try
                    {
                        strSql = string.Format(strSql, param.ID, param.Type, ((FS.HISFC.Models.Base.Employee)this.Operator).Dept.ID);
                    }
                    catch
                    {
                        this.Err = "�����������!";
                        return null;
                    }
                }
            }
            return this.myGetFiles(param, strSql);

        }

        private ArrayList myGetFiles(FS.HISFC.Models.File.DataFileParam param, string strSql)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.File.DataFileInfo DataFileInfo = null;
            if (this.ExecQuery(strSql) == -1) return null;

            while (this.Reader.Read())
            {
                DataFileInfo = new FS.HISFC.Models.File.DataFileInfo();

                DataFileInfo.Param = (FS.HISFC.Models.File.DataFileParam)param.Clone();

                // TODO:  ��� DataFileInfo.GetList ʵ��
                try
                {
                    DataFileInfo.ID = this.Reader[0].ToString();//�ļ����
                }
                catch
                { }
                try
                {
                    DataFileInfo.Param.ID = this.Reader[1].ToString();//������
                }
                catch
                { }
                try
                {
                    DataFileInfo.Param.Type = this.Reader[2].ToString();//ϵͳ���� 0 ���Ӳ��� 1 �������뵥
                }
                catch
                { }
                try
                {
                    DataFileInfo.Type = this.Reader[3].ToString();//�ļ����� �����ļ� ģ���ļ�
                }
                catch
                { }
                try
                {
                    DataFileInfo.Name = this.Reader[4].ToString();//�ļ��� ˵������
                }
                catch
                { }
                try
                {
                    DataFileInfo.Param.Http = this.Reader[5].ToString();//http
                }
                catch
                { }
                try
                {
                    DataFileInfo.Param.IP = this.Reader[6].ToString();//������
                }
                catch
                { }
                try
                {
                    DataFileInfo.Param.Folders = this.Reader[7].ToString();//·����
                }
                catch
                { }
                try
                {
                    DataFileInfo.Param.FileName = this.Reader[8].ToString();//�ļ���
                }
                catch
                { }
                try
                {
                    DataFileInfo.Index1 = this.Reader[9].ToString();//����1
                }
                catch
                { }
                try
                {
                    DataFileInfo.Index2 = this.Reader[10].ToString();//����2
                }
                catch
                { }
                try
                {
                    DataFileInfo.Memo = this.Reader[11].ToString();//��ע
                }
                catch
                { }
                try
                {
                    DataFileInfo.DataType = this.Reader[12].ToString();//�ض�����
                }
                catch
                { }
                try
                {
                    DataFileInfo.valid = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[13].ToString());//��Ч��־
                }
                catch
                { }
                try
                {
                    DataFileInfo.UseType = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[14].ToString());//�û�����
                    DataFileInfo.Count = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[15].ToString());//ʹ�ô���
                }
                catch
                { }
                al.Add(DataFileInfo);
            }
            this.Reader.Close();
            return al;
        }

        /// <summary>
        /// ɾ���ļ�
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="iType"> 0 �����ļ� 1 ģ���ļ�</param>
        /// <returns></returns>
        public int Del(object strID, int iType)
        {
            // TODO:  ��� DataFileInfo.Del ʵ��
            string strSql = "";
            if (iType == 0)//����
            {
                if (this.Sql.GetSql("Manager.DataFileInfo.Delete.1", ref strSql) == -1) return -1;
            }
            else //ģ��
            {
                if (this.Sql.GetSql("Manager.DataFileInfo.Delete.Modual.1", ref strSql) == -1) return -1;
            }
            try
            {
                strSql = string.Format(strSql, strID);
            }
            catch
            {
                this.Err = "�����������!";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ɾ�������ļ�
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public int Del(object strID)
        {
            return this.Del(strID, 0);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="al"></param>
        /// <returns></returns>
        public int SetList(System.Collections.ArrayList al)
        {
            // TODO:  ��� DataFileInfo.SetList ʵ��
            return 0;
        }
        /// <summary>
        /// �����ļ�
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject Get(object id)
        {
            return this.Get(id, 0);
        }
        /// <summary>
        /// ���datafileinfo
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject Get(object id, int iType)
        {
            // TODO:  ��� DataFileInfo.Get ʵ��
            string strSql = "";
            if (iType == 0)
            {
                if (this.Sql.GetSql("Manager.DataFileInfo.Select.5", ref strSql) == -1) return null;
            }
            else
            {
                if (this.Sql.GetSql("Manager.DataFileInfo.Select.Modual.5", ref strSql) == -1) return null;
            }
            try
            {
                strSql = string.Format(strSql, id);
            }
            catch
            {
                this.Err = "�����������!";
                return null;
            }


            if (this.ExecQuery(strSql) == -1) return null;
            FS.HISFC.Models.File.DataFileInfo DataFileInfo = null;

            if (this.Reader.Read())
            {
                DataFileInfo = new FS.HISFC.Models.File.DataFileInfo();

                DataFileInfo.ID = this.Reader[0].ToString();//�ļ����

                DataFileInfo.Param.ID = this.Reader[1].ToString();//������

                DataFileInfo.Param.Type = this.Reader[2].ToString();//ϵͳ���� 0 ���Ӳ��� 1 �������뵥

                DataFileInfo.Type = this.Reader[3].ToString();//�ļ����� �����ļ� ģ���ļ�

                DataFileInfo.Name = this.Reader[4].ToString();//�ļ��� ˵������

                DataFileInfo.Param.Http = this.Reader[5].ToString();//http

                DataFileInfo.Param.IP = this.Reader[6].ToString();//������

                DataFileInfo.Param.Folders = this.Reader[7].ToString();//·����

                DataFileInfo.Param.FileName = this.Reader[8].ToString();//�ļ���

                DataFileInfo.Index1 = this.Reader[9].ToString();//����1

                DataFileInfo.Index2 = this.Reader[10].ToString();//����2

                DataFileInfo.Memo = this.Reader[11].ToString();//��ע

                DataFileInfo.DataType = this.Reader[12].ToString();//�ض�����

                DataFileInfo.Param.IsInDB = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[13]);//�Ƿ������ݿ�
                try
                {
                    DataFileInfo.valid = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[14]);//��Ч����
                }
                catch { }
                try
                {
                    DataFileInfo.UseType = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[15].ToString());//�û�����
                    DataFileInfo.Count = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[16].ToString());//ʹ�ô���                
                }
                catch { }
            }
            this.Reader.Close();
            return DataFileInfo;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int SetInValid(FS.FrameWork.Models.NeuObject obj, int iType)
        {
            FS.HISFC.Models.File.DataFileInfo DataFileInfo = null;
            try
            {
                DataFileInfo = obj as FS.HISFC.Models.File.DataFileInfo;
            }
            catch
            {
                return -1;
            }
            DataFileInfo.valid = 1;//��Ч
            return this.Set(DataFileInfo, iType);
        }
        /// <summary>
        /// ����Ϊ����
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="iType"></param>
        /// <returns></returns>
        public int SetValid(FS.FrameWork.Models.NeuObject obj, int iType)
        {
            FS.HISFC.Models.File.DataFileInfo DataFileInfo = null;
            try
            {
                DataFileInfo = obj as FS.HISFC.Models.File.DataFileInfo;
            }
            catch
            {
                return 0;
            }
            DataFileInfo.valid = 0;//��Ч
            return this.Set(DataFileInfo, iType);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Set(FS.FrameWork.Models.NeuObject obj)
        {
            return this.Set(obj, 0);
        }


        protected int setModual(FS.FrameWork.Models.NeuObject obj)
        {
            #region
            string strSql = "", strSql1 = "";
            FS.HISFC.Models.File.DataFileInfo DataFileInfo = null;

            DataFileInfo = obj as FS.HISFC.Models.File.DataFileInfo;
            if (DataFileInfo == null)
            {
                this.Err = "����ʵ���DataFileInfo.";
                return -1;
            }

            if (this.Sql.GetSql("Manager.DataFileInfo.Update.Modual.1", ref strSql) == -1) return -1;
            if (this.Sql.GetSql("Manager.DataFileInfo.Insert.Modual.1", ref strSql1) == -1) return -1;

            string[] s = new string[18];
            try
            {

                s[0] = DataFileInfo.Param.ID;//������
                s[1] = DataFileInfo.Param.Type;//ϵͳ���� 0 ���Ӳ��� 1 �������뵥
                s[2] = DataFileInfo.Type;//�ļ����� �����ļ� ģ���ļ�
                s[3] = DataFileInfo.Name;//�ļ��� ˵������
                s[4] = DataFileInfo.Param.Http;//�����ļ�ͷ��
                s[5] = DataFileInfo.Param.IP;//������
                s[6] = DataFileInfo.Param.Folders;//·����
                s[7] = DataFileInfo.Param.FileName;//�ļ���
                s[8] = DataFileInfo.Index1;//����1
                s[9] = DataFileInfo.Index2;//����2
                s[10] = DataFileInfo.Memo;//��ע
                s[11] = DataFileInfo.DataType;//�ض����� �ļ��������
                s[12] = DataFileInfo.Data;//����
                s[13] = this.Operator.ID;//������ID
                s[14] = DataFileInfo.ID;//�ļ���
                s[15] = DataFileInfo.valid.ToString();//��Ч��� 0 ��Ч 1 ����
                s[16] = DataFileInfo.UseType.ToString(); //�û�����
                s[17] = DataFileInfo.Count.ToString();//ʹ�ô���
                strSql = string.Format(strSql, s);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            #endregion

            if (this.ExecNoQuery(strSql) <= 0)//update
            {
                strSql = string.Format(strSql1, s);
                if (this.ExecNoQuery(strSql) <= 0)//insert
                {
                    return -1;
                }
            }
            return 0;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Set(FS.FrameWork.Models.NeuObject obj, int iType)
        {
            // TODO:  ��� DataFileInfo.Set ʵ��
            #region
            string strSql = "", strSql1 = "";
            FS.HISFC.Models.File.DataFileInfo DataFileInfo = null;
            try
            {
                DataFileInfo = obj as FS.HISFC.Models.File.DataFileInfo;
            }
            catch
            {
                return -1;
            }
            if (iType == 0)
            {
                if (this.Sql.GetSql("Manager.DataFileInfo.Update.1", ref strSql) == -1) return -1;
                if (this.Sql.GetSql("Manager.DataFileInfo.Insert.1", ref strSql1) == -1) return -1;
            }
            else
            {
                //if(this.Sql.GetSql("Manager.DataFileInfo.Update.Modual.1",ref strSql)==-1) return -1;
                //if(this.Sql.GetSql("Manager.DataFileInfo.Insert.Modual.1",ref strSql1)==-1) return -1;
                return this.setModual(obj);
            }
            string[] s = new string[17];
            try
            {
                try
                {
                    s[0] = DataFileInfo.Param.ID;//������
                }
                catch { }
                try
                {
                    s[1] = DataFileInfo.Param.Type;//ϵͳ���� 0 ���Ӳ��� 1 �������뵥
                }
                catch { }
                try
                {
                    s[2] = DataFileInfo.Type;//�ļ����� �����ļ� ģ���ļ�
                }
                catch { }
                try
                {
                    s[3] = DataFileInfo.Name;//�ļ��� ˵������
                }
                catch { }
                try
                {
                    s[4] = DataFileInfo.Param.Http;//�����ļ�ͷ��
                }
                catch { }
                try
                {
                    s[5] = DataFileInfo.Param.IP;//������
                }
                catch { }
                try
                {
                    s[6] = DataFileInfo.Param.Folders;//·����
                }
                catch { }
                try
                {
                    s[7] = DataFileInfo.Param.FileName;//�ļ���
                }
                catch { }
                try
                {
                    s[8] = DataFileInfo.Index1;//����1
                }
                catch { }
                try
                {
                    s[9] = DataFileInfo.Index2;//����2
                }
                catch { }
                try
                {
                    s[10] = DataFileInfo.Memo;//��ע
                }
                catch { }
                try
                {
                    s[11] = DataFileInfo.DataType;//�ض����� �ļ��������
                }
                catch { }
                try
                {
                    s[12] = DataFileInfo.Data;//����
                }
                catch { }
                try
                {
                    s[13] = this.Operator.ID;//������ID
                }
                catch { }
                try
                {
                    s[14] = DataFileInfo.ID;//�ļ���
                }
                catch { }
                try
                {
                    s[15] = DataFileInfo.valid.ToString();//��Ч��� 0 ��Ч 1 ����
                    s[16] = DataFileInfo.UseType.ToString();//��Ч��� 0 ��Ч 1 ����
                }
                catch { }
                strSql = string.Format(strSql, s);
            #endregion
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            if (this.ExecNoQuery(strSql) <= 0)//update
            {
                strSql = string.Format(strSql1, s);
                if (this.ExecNoQuery(strSql) <= 0)//insert
                {
                    return -1;
                }
            }
            return 0;
        }
        public string GetNewFileID()
        {
            string strSql = "";
            if (this.Sql.GetSql("Manager.DataFileInfo.GetNewID", ref strSql) == -1) return "-1";
            return this.ExecSqlReturnOne(strSql);
        }
        #endregion

        #region add by lijp 2011-11-16 �������뵥���

        /// <summary>
        /// ���datafileinfo
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public System.Collections.Generic.IList<FS.HISFC.Models.File.DataFileInfo> Get(object id, string dataType)
        {
            // TODO:  ��� DataFileInfo.Get ʵ��
            string strSql = "";
            if (this.Sql.GetSql("Manager.DataFileInfo.Select.6", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, id, dataType);
            }
            catch
            {
                this.Err = "�����������!";
                return null;
            }


            if (this.ExecQuery(strSql) == -1) return null;
            FS.HISFC.Models.File.DataFileInfo DataFileInfo = null;
            System.Collections.Generic.IList<FS.HISFC.Models.File.DataFileInfo> listFileInfo = new System.Collections.Generic.List<FS.HISFC.Models.File.DataFileInfo>();
            while (this.Reader.Read())
            {
                DataFileInfo = new FS.HISFC.Models.File.DataFileInfo();

                DataFileInfo.ID = this.Reader[0].ToString();//�ļ����

                DataFileInfo.Param.ID = this.Reader[1].ToString();//������

                DataFileInfo.Param.Type = this.Reader[2].ToString();//ϵͳ���� 0 ���Ӳ��� 1 �������뵥

                DataFileInfo.Type = this.Reader[3].ToString();//�ļ����� �����ļ� ģ���ļ�

                DataFileInfo.Name = this.Reader[4].ToString();//�ļ��� ˵������

                DataFileInfo.Param.Http = this.Reader[5].ToString();//http

                DataFileInfo.Param.IP = this.Reader[6].ToString();//������

                DataFileInfo.Param.Folders = this.Reader[7].ToString();//·����

                DataFileInfo.Param.FileName = this.Reader[8].ToString();//�ļ���

                DataFileInfo.Index1 = this.Reader[9].ToString();//����1

                DataFileInfo.Index2 = this.Reader[10].ToString();//����2

                DataFileInfo.Memo = this.Reader[11].ToString();//��ע

                DataFileInfo.DataType = this.Reader[12].ToString();//�ض�����

                DataFileInfo.Param.IsInDB = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[13]);//�Ƿ������ݿ�
                try
                {
                    DataFileInfo.valid = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[14]);//��Ч����
                }
                catch { }
                try
                {
                    DataFileInfo.UseType = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[15].ToString());//�û�����
                    DataFileInfo.Count = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[16].ToString());//ʹ�ô���                
                }
                catch { }
                listFileInfo.Add(DataFileInfo);
            }
            this.Reader.Close();
            return listFileInfo;
        }

        #endregion

    }//81067437 
}
