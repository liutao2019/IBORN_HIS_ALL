using System;
using System.Collections.Generic;
using System.Text;
using System.Data ;
using System.Collections ;

namespace FS.HISFC.BizLogic.Manager
{
    public class ItemInfoQuery : DataBase 
    {
        ///��ҩƷ��Ŀ��ѯά������
        ///������ѯ,�����Ҳ�ѯ,����Ŀ��ѯ ��Ŀ�շ����
        ///ά��

        public ItemInfoQuery()
        {
            
        }

        /// <summary>
        /// ��ȡ��ѯ�������
        /// </summary>
        /// <returns></returns>
        public string GetSequence()
        {
            string querySql = "SELECT SEQ_FIN_COM_ITEMQUERY.Nextval FROM dual";

            try
            {
                return this.ExecSqlReturnOne(querySql);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                WriteErr();
                return "";
            }
        }

        /// <summary>
        /// ������Ŀ��ѯ��Ϣά��,�������
        /// </summary>
        /// <param name="ament"></param>
        /// <returns></returns>
        public int InsertItemInfoAment(FS.HISFC.Models.Base.Const ament)
        {
            if (ament == null)
            {
                return 0;
            }

            string insertSql = string.Empty;

            if (this.GetSQL("UniReport.InsertItemQueryInfo", ref insertSql) == -1)
            {
                this.Err = "��ȡSql������,������:UniReport.InsertItemQueryInfo";
                WriteErr();
                return -1;
            }

            try
            {
                insertSql = string.Format(insertSql, FS.FrameWork.Function.NConvert.ToInt32(ament.ID),     // ����
                                                  ament.Name,    //��ѯ����
                                                  ament.User01,  //���Ҵ���
                                                  ament.User02,  //��Ŀ����
                                                  ament.User03,   //��Ŀ����
                                                  FS.FrameWork.Function.NConvert.ToInt32(ament.Memo),  //˳���
                                                  this.Operator.ID,//����Ա
                                                  ament.SpellCode);  
                if (ExecNoQuery(insertSql) < 0)
                {
                    this.Err = "��������ʧ��,�����:" + this.ErrCode;
                    WriteErr();
                    return -1;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                WriteErr();

                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ����ά������ (��������)
        /// </summary>
        /// <param name="mentArr"></param>
        /// <returns></returns>
        public int InsertItemInfoAment(ArrayList mentArr)
        {
            if (mentArr == null || mentArr.Count <= 0)
            {
                this.Err = "��������Ϊ��,��˶Ժ��ٽ��б���";
                WriteErr();
                return 0;
            }

            string insertSql = string.Empty;

            if (this.GetSQL("UniReport.InsertItemQueryInfo", ref insertSql) == -1)
            {
                this.Err = "��ȡSql������,������:UniReport.InsertItemQueryInfo";
                WriteErr();
                return -1;
            }

            for (int i = 0; i < mentArr.Count; i++)
            {
                FS.HISFC.Models.Base.Const ament = mentArr[i] as FS.HISFC.Models.Base.Const;

                string sqleach = insertSql;

                try
                {
                    sqleach = string.Format(insertSql, ament.ID,     // ��ѯ����
                                                      ament.Name,    //���Ҵ���
                                                      ament.User01,  //��������
                                                      ament.User02,  //��Ŀ����
                                                      ament.User03,   //��Ŀ����
                                                      FS.FrameWork.Function.NConvert.ToInt32(ament.Memo),  //˳���
                                                      this.Operator.ID,
                                                      ament.SpellCode);  //����Ա
                    if (ExecNoQuery(sqleach) < 0)
                    {
                        this.Err = "��������ʧ��,�����:" + this.ErrCode;
                        WriteErr();
                        return -1;
                    }                         
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    WriteErr();

                    if (!this.Reader.IsClosed)
                    {
                        this.Reader.Close();
                    }
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="amentObj"></param>
        /// <returns></returns>
        public int UpdateItemInfoAment(FS.HISFC.Models.Base.Const amentObj)
        {
            if (amentObj == null)
            {
                this.Err = "����ʵ��Ϊ��!";
                WriteErr();
                return 0;
            }

            string updateSql = "";

            if (this.GetSQL("UniReport.UpdateItemQueryInfo", ref updateSql) == -1)
            {
                this.Err = "��������������,������:UniReport.UpdateItemQueryInfo";
                WriteErr();
                return -1;
            }

            try
            {
                updateSql = string.Format(updateSql, FS.FrameWork.Function.NConvert.ToInt32(amentObj.ID),     // ����
                                                   amentObj.Name,    //��ѯ����
                                                   amentObj.User01,  //���Ҵ���
                                                   amentObj.User02,  //��Ŀ����
                                                   amentObj.User03,   //��Ŀ����
                                                   FS.FrameWork.Function.NConvert.ToInt32(amentObj.Memo),  //˳���
                                                   this.Operator.ID,
                                                   amentObj.SpellCode);  //����Ա
                if (ExecNoQuery(updateSql) < 0)
                {
                    this.Err = "��������ʧ��,�����:" + this.ErrCode;
                    WriteErr();
                    return -1;
                }   
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                WriteErr();

                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amentObj"></param>
        /// <returns></returns>
        public int DeleteItemInfoAment(FS.FrameWork.Models.NeuObject amentObj)
        {
            if (amentObj == null)
            {
                this.Err = "��Ϣʵ��Ϊ��,��˶Ժ��ٽ���ɾ��!";
                WriteErr();
                return 0;
            }

            string deleteSql = "";
            if (this.GetSQL("UniReport.DeleteItemQueryInfo", ref deleteSql) == -1)
            {
                this.Err = "��ȡSql���ʧ��,������:UniReport.DeleteItemQueryInfo";
                WriteErr();
                return -1;
            }

            try
            {
                deleteSql = string.Format(deleteSql, FS.FrameWork.Function.NConvert.ToInt32(amentObj.ID));
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                WriteErr();
                return -1;
            }
            return this.ExecNoQuery(deleteSql);
        }

        /// <summary>
        /// ��ѯ��ά������Ŀ��ѯ��Ϣ
        /// </summary>
        /// <param name="deptCode">Ȩ�޿���</param>
        /// <param name="queryType">��ѯ����</param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int QueryItemQueryMend(string deptCode,string queryType,ref DataSet ds)
        {
            string querySql = "";

            if (this.GetSQL("UniReport.GetItemQueryInfoByDeptQueryType", ref querySql) == -1)
            {
                this.Err = "��ȡSql������,������:UniReport.GetItemQueryInfoByDeptQueryType";
                WriteErr();
                ds = null;
                return -1;
            }
            try
            {
                querySql = string.Format(querySql, deptCode, queryType);
            }
            catch (Exception ex)
            {
                this.Err = "������ʽ������" + ex.Message;
                WriteErr();
                ds = null;
                return -1;
            }

            return this.ExecQuery(querySql, ref ds);
        }

        /// <summary>
        /// ��ѯ��ά������Ŀ��ѯ��Ϣ
        /// </summary>
        /// <param name="deptCode">Ȩ�޿���</param>
        /// <param name="queryType">��ѯ����</param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int QueryItemQueryMend_Const(string deptCode, string queryType, ref DataSet ds)
        {
            string querySql = "";

            if (this.GetSQL("UniReport.GetItemQueryInfoByDeptQueryType.Const", ref querySql) == -1)
            {
                this.Err = "��ȡSql������,������:UniReport.GetItemQueryInfoByDeptQueryType.Const";
                WriteErr();
                ds = null;
                return -1;
            }
            try
            {
                querySql = string.Format(querySql, deptCode, queryType);
            }
            catch (Exception ex)
            {
                this.Err = "������ʽ������" + ex.Message;
                WriteErr();
                ds = null;
                return -1;
            }

            return this.ExecQuery(querySql, ref ds);
        }

        /// <summary>
        /// ͨ����ѯ���ͻ�ÿ��ұ��������
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ArrayList GetDeptByItemQueryType(string type)
        {
            string querySql = "";
            ArrayList al = new ArrayList();
            FS.FrameWork.Models.NeuObject obj;

            if (this.GetSQL("UniReport.GetDeptByItemQuery", ref querySql) == -1)
            {
                this.Err = "��ȡSql������,������:UniReport.GetDeptByItemQuery";
                WriteErr();
                return null;
            }

            try
            {
                querySql = string.Format(querySql, type);
            }
            catch (Exception ex)
            {
                this.Err = "������ʽ������" + ex.Message;
                WriteErr();
                return null;
            }

            if (this.ExecQuery(querySql) == -1)
            {
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = this.Reader[0].ToString();
                    obj.Name = this.Reader[1].ToString();
                    al.Add(obj);
                }
            }
            catch(Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "��ȡ������Ϣʧ��";
                this.WriteErr();
                return null;
            }

            return al;
        }

        /// <summary>
        /// ɾ����ѯ�����µľ�����Ŀ
        /// </summary>
        /// <param name="queryType">��ѯ����</param>
        /// <param name="deptCode">���ұ���</param>
        /// <returns></returns>
        public int DeleteItemInfoAment(string queryType, string deptCode)
        {
            string deleteSql = "";
            if (this.GetSQL("UniReport.DeleteItemQueryList", ref deleteSql) == -1)
            {
                this.Err = "UniReport.DeleteItemQueryList";
                WriteErr();
                return -1;
            }

            try
            {
                deleteSql = string.Format(deleteSql, queryType, deptCode);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                WriteErr();
                return -1;
            }
            return this.ExecNoQuery(deleteSql);
        }    
    }
}
