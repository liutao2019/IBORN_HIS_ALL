 using System;
using System.Collections;
namespace FS.HISFC.BizLogic.EPR
{
	/// <summary>
	/// GetFile ��ժҪ˵����
	/// ����ļ�
	/// </summary>
	public class DataFile:FS.FrameWork.Management.Database
	{
		public DataFile(string type)
		{
			try
			{
				dtParam = this.ParamManager.Get(type) as FS.HISFC.Models.File.DataFileParam;
				if(dtParam==null) return;
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;				
				return;
			}
		}
		public DataFile()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

        public FS.HISFC.BizLogic.EPR.DataFileParam ParamManager = new FS.HISFC.BizLogic.EPR.DataFileParam();
        public FS.HISFC.BizLogic.EPR.DataFileInfo FileManager = new FS.HISFC.BizLogic.EPR.DataFileInfo();
		private FS.HISFC.Models.File.DataFileParam  dtParam= null;
		private FS.HISFC.Models.File.DataFileInfo dtFile =null;
		
		#region ����
		/// <summary>
		/// ��ǰ�ļ��б�
		/// </summary>
		public ArrayList alFiles;
		/// <summary>
		/// ��ǰģ���б�
		/// </summary>
		public ArrayList alModuals;
	
		/// <summary>
		/// ��������ʾ����
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public int SetType(string type)
		{
			try
			{
				dtParam = this.ParamManager.Get(type) as FS.HISFC.Models.File.DataFileParam;
				if(dtParam==null) return -1;
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;				
				return -1;
			}
			return 0;
		}
		/// <summary>
		/// �������
		/// </summary>
		/// <returns></returns>
		public int GetModuals(bool isAll)
		{
			try
			{
			
				alModuals = FileManager.GetList(dtParam,1,isAll);
				return alModuals.Count;
			}
			catch{return -1;}
		}
		/// <summary>
		/// �������
		/// </summary>
		/// <param name="param"></param>
		/// <returns></returns>
		public int GetFiles(params string[] param)
		{
			try
			{
				this.dtFile = new FS.HISFC.Models.File.DataFileInfo();
				this.dtFile.Param =(FS.HISFC.Models.File.DataFileParam)this.dtParam.Clone();
				this.dtFile.Param.ID = string.Format(this.dtParam.ID,param);
				this.dtFile.Param.Type = this.dtParam.Type;
				alFiles = FileManager.GetList(this.dtFile.Param);
				return alFiles.Count;
			}
			catch{return -1;}
		}
		/// <summary>
		/// ��ò���
		/// </summary>
		public FS.HISFC.Models.File.DataFileParam DataFileParam
		{
			get
			{
				if(this.dtParam==null)this.dtParam=new FS.HISFC.Models.File.DataFileParam();
				return this.dtParam;
			}
			set
			{
				this.dtParam = value;
			}
		}
		/// <summary>
		/// ����ļ���Ϣ
		/// </summary>
		public FS.HISFC.Models.File.DataFileInfo DataFileInfo
		{
			get
			{
				if(this.dtFile==null)this.dtFile =new FS.HISFC.Models.File.DataFileInfo();
				return this.dtFile;
			}
			set
			{
				this.dtFile = value;
			}
		}

		
		#endregion

		#region �ڵ����
		/// <summary>
		/// �����㵽���ݿ�
		/// </summary>
		/// <param name="Table"></param>
		/// <param name="dt"></param>
		/// <param name="ParentText"></param>
		/// <param name="NodeText"></param>
		/// <param name="NodeValue"></param>
		/// <returns></returns>
		public int SaveNodeToDataStore(string Table,FS.HISFC.Models.File.DataFileInfo dt,string ParentText,string NodeText,string NodeValue)
		{			
			string strSql ="";
			string sql="";

			if(this.Sql.GetSql("Management.DataFile.Select",ref strSql)==-1) return -1;
			try
			{
				FS.FrameWork.Public.String.FormatString (strSql,out sql,Table,dt.ID,dt.Type,dt.DataType,dt.Name,dt.Index1,dt.Index2,ParentText,NodeText,NodeValue,this.Operator.ID);
			}
			catch(Exception ex)
			{
				this.Err ="Management.DataFile.Select��ֵʱ�����"+ex.Message;
				this.WriteErr();
				return -1;
			}
			if(this.ExecQuery(sql)==-1) return -1;
			if(this.Reader.Read())//�м�¼��ִ�и���
			{
				if(NodeValue == this.Reader[0].ToString())//��ͬ����
				{
					this.Reader.Close();
					return 0;
				}
				else
				{
					if(this.Sql.GetSql("Management.DataFile.UpdateToDataStore",ref strSql)==-1) return -1;
				}
			}
			else//�޼�¼��ִ�в���
			{
				if(this.Sql.GetSql("Management.DataFile.InsertToDataStore",ref strSql)==-1) return -1;
			}
			try
			{
				FS.FrameWork.Public.String.FormatString (strSql,out sql,Table,dt.ID,dt.Type,dt.DataType,dt.Name,dt.Index1,dt.Index2,ParentText,NodeText,NodeValue,this.Operator.ID);
			}
			catch(Exception ex){
				this.Err ="SaveNodeToDataStore��ֵʱ�����"+ex.Message;
				this.WriteErr();
				return -1;
			}
			try
			{
				this.Reader.Close();
			}
			catch{}
			return this.ExecNoQuery(sql);
		}
		/// <summary>
		/// ɾ����㡡
		/// </summary>
		/// <param name="Table"></param>
		/// <param name="dt"></param>
		/// <returns></returns>
		public int DeleteAllNodeFromDataStore(string Table,FS.HISFC.Models.File.DataFileInfo dt)
		{
			string strSql ="",sql="";
			if(this.Sql.GetSql("Management.DataFile.DeleteAllNodeFromDataStore",ref strSql)==-1) return -1;
			try
			{
				sql = string.Format(strSql,Table,dt.ID);
			}
			catch(Exception ex)
			{
				this.Err ="DeleteNode��ֵʱ�����"+ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(sql);
		}
		
		/// <summary>
		/// ��ýڵ�����
		/// </summary>
		/// <param name="Table"></param>
		/// <param name="inpatientNo"></param>
		/// <param name="nodeName"></param>
		/// <returns></returns>
		public string GetNodeValueFormDataStore(string Table,string inpatientNo,string nodeName)
		{
			string strSql ="",sql="";
			if(this.Sql.GetSql("Management.DataFile.GetNodeValueFormDataStore",ref strSql)==-1) return "-1";
			try
			{
				sql = string.Format(strSql,Table,inpatientNo,nodeName);
			}
			catch(Exception ex)
			{
				this.Err ="GetNodeValueFormDataStore��ֵʱ�����"+ex.Message;
				this.WriteErr();
				return "-1";
			}
			return this.ExecSqlReturnOne(sql);
		}
		#endregion

        //#region ���ֶβ���

        ///// <summary>
        ///// ���ļ����뵽���ݿ���
        ///// </summary>
        ///// <param name="dt"></param>
        ///// <param name="fileData">������ļ�����</param>
        ///// <returns></returns>
        //public int ImportToDatabase(FS.HISFC.Models.File.DataFileInfo dt,byte[] fileData)
        //{
        //    string strSql ="",sql="";
        //    if(dt.ID == null||dt.ID =="") return -1;

        //    if(this.Sql.GetSql("Management.DataFile.ImportToDatabase",ref strSql)==-1) return -1;
        //    try
        //    {
        //        sql = string.Format(strSql,dt.ID);
        //    }
        //    catch(Exception ex)
        //    {
        //        this.Err ="ImportToDatabase��ֵʱ�����"+ex.Message;
        //        this.WriteErr();
        //        return -1;
        //    }
			
        //    return this.InputBlob(sql,fileData);
        //}

        ///// <summary>
        ///// ����ļ� 
        ///// </summary>
        ///// <param name="dt"></param>
        ///// <param name="fileData"></param>
        ///// <returns></returns>
        //public int ExportFromDatabase(FS.HISFC.Models.File.DataFileInfo dt,ref byte[] fileData)
        //{
        //    string strSql ="",sql="";
        //    if(dt.ID == null||dt.ID =="") return -1;

        //    if(this.Sql.GetSql("Management.DataFile.ExportFromDatabase",ref strSql)==-1) return -1;
        //    try
        //    {
        //        sql = string.Format(strSql,dt.ID);
        //    }
        //    catch(Exception ex)
        //    {
        //        this.Err ="ExportFromDatabase��ֵʱ�����"+ex.Message;
        //        this.WriteErr();
        //        return -1;
        //    }
			
        //    fileData = this.OutputBlob(sql);
        //    return 0;
        //}

        ///// <summary>
        ///// ���ļ����뵽���ݿ���
        ///// </summary>
        ///// <param name="dt"></param>
        ///// <param name="fileData">������ļ�����</param>
        ///// <returns></returns>
        //public int ImportToDatabaseModual(FS.HISFC.Models.File.DataFileInfo dt, byte[] fileData)
        //{
        //    string strSql = "", sql = "";
        //    if (dt.ID == null || dt.ID == "") return -1;

        //    if (this.Sql.GetSql("Management.DataFile.ImportToDatabase", ref strSql) == -1) return -1;
        //    try
        //    {
        //        sql = string.Format(strSql, dt.ID);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Err = "ImportToDatabase��ֵʱ�����" + ex.Message;
        //        this.WriteErr();
        //        return -1;
        //    }

        //    return this.InputBlob(sql, fileData);
        //}

        ///// <summary>
        ///// ����ļ� 
        ///// </summary>
        ///// <param name="dt"></param>
        ///// <param name="fileData"></param>
        ///// <returns></returns>
        //public int ExportFromDatabaseModual(FS.HISFC.Models.File.DataFileInfo dt, ref byte[] fileData)
        //{
        //    string strSql = "", sql = "";
        //    if (dt.ID == null || dt.ID == "") return -1;

        //    if (this.Sql.GetSql("Management.DataFile.ExportFromDatabase", ref strSql) == -1) return -1;
        //    try
        //    {
        //        sql = string.Format(strSql, dt.ID);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Err = "ExportFromDatabase��ֵʱ�����" + ex.Message;
        //        this.WriteErr();
        //        return -1;
        //    }

        //    fileData = this.OutputBlob(sql);
        //    return 0;
        //}

        ///// <summary>
        ///// ���ļ����뵽���ݿ���
        ///// </summary>
        ///// <param name="dt"></param>
        ///// <param name="fileData">������ļ�����</param>
        ///// <returns></returns>
        //public int ImportToDatabase(FS.HISFC.Models.File.DataFileInfo dt,string fileData)
        //{
        //    string strSql ="",sql ="";
        //    if(dt.ID == null||dt.ID =="") return -1;

        //    if(this.Sql.GetSql("Management.DataFile.ImportToDatabase",ref strSql)==-1) return -1;
			
        //    try
        //    {
        //        sql = string.Format(strSql,dt.ID);
        //    }
        //    catch(Exception ex)
        //    {
        //        this.Err ="ImportToDatabase��ֵʱ�����"+ex.Message;
        //        this.WriteErr();
        //        return -1;
        //    }
        //    return this.InputLong(sql,fileData);
        //}

        ///// <summary>
        ///// ����ļ� 
        ///// </summary>
        ///// <param name="dt"></param>
        ///// <param name="fileData"></param>
        ///// <returns></returns>
        //public int ExportFromDatabase(FS.HISFC.Models.File.DataFileInfo dt,ref string fileData)
        //{
        //    string strSql ="",sql="";
        //    if(dt.ID == null||dt.ID =="") return -1;

        //    if(this.Sql.GetSql("Management.DataFile.ExportFromDatabase",ref strSql)==-1) return -1;
        //    try
        //    {
        //        sql = string.Format(strSql,dt.ID);
        //    }
        //    catch(Exception ex)
        //    {
        //        this.Err ="ExportFromDatabase��ֵʱ�����"+ex.Message;
        //        this.WriteErr();
        //        return -1;
        //    }
			
        //    fileData = this.ExecSqlReturnOne(sql);
        //    if(fileData =="-1") return -1;
        //    return 0;
        //}

        //#endregion
        #region ���ֶβ���

        /// <summary>
        /// ���ļ����뵽���ݿ���
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fileData">������ļ�����</param>
        /// <returns></returns>
        public int ImportToDatabase(FS.HISFC.Models.File.DataFileInfo dt, byte[] fileData)
        {
            string strSql = "", sql = "";
            if (dt.ID == null || dt.ID == "") return -1;
            if (dt.Type.Trim() == "0")//����
            {
                if (this.Sql.GetSql("Management.DataFile.ImportToDatabase.byte", ref strSql) == -1) return -1;
            }
            else if (dt.Type.Trim() == "1") //ģ��
            {
                if (this.Sql.GetSql("Management.DataFile.ImportToDatabase.Modual.byte", ref strSql) == -1) return -1;
            }
            else
            {
                this.Err = "δ֪�ļ�����";
                return -1;
            }

            try
            {
                sql = string.Format(strSql, dt.ID);
            }
            catch (Exception ex)
            {
                this.Err = "ImportToDatabase��ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }

            return this.InputBlob(sql, fileData);
        }

        /// <summary>
        /// ����ļ� 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fileData"></param>
        /// <returns></returns>
        public int ExportFromDatabase(FS.HISFC.Models.File.DataFileInfo dt, ref byte[] fileData)
        {
            string strSql = "", sql = "";
            if (dt.ID == null || dt.ID == "") return -1;

            if (dt.Type.Trim() == "0")//����
            {
                if (this.Sql.GetSql("Management.DataFile.ExportFromDatabase.byte", ref strSql) == -1) return -1;
            }
            else if (dt.Type.Trim() == "1") //ģ��
            {
                if (this.Sql.GetSql("Management.DataFile.ExportFromDatabase.Modual.byte", ref strSql) == -1) return -1;
            }
            else
            {
                this.Err = "δ֪�ļ�����";
                return -1;
            }

            try
            {
                sql = string.Format(strSql, dt.ID);
            }
            catch (Exception ex)
            {
                this.Err = "ExportFromDatabase��ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }

            fileData = this.OutputBlob(sql);
            return 0;
        }



        /// <summary>
        /// ���ļ����뵽���ݿ���
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fileData">������ļ�����</param>
        /// <returns></returns>
        public int ImportToDatabase(FS.HISFC.Models.File.DataFileInfo dt, string fileData)
        {

            string strSql = "", sql = "";
            if (dt.ID == null || dt.ID == "") return -1;

            if (dt.Type.Trim() == "0")//����
            {
                if (this.Sql.GetSql("Management.DataFile.ImportToDatabase", ref strSql) == -1) return -1;
            }
            else if (dt.Type.Trim() == "1") //ģ��
            {
                if (this.Sql.GetSql("Management.DataFile.ImportToDatabase.Modual", ref strSql) == -1) return -1;
            }
            else
            {
                this.Err = "δ֪�ļ�����";
                return -1;
            }

            try
            {
                sql = string.Format(strSql, dt.ID);
            }
            catch (Exception ex)
            {
                this.Err = "ImportToDatabase��ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.InputLong(sql, fileData);

        }

        /// <summary>
        /// ����ļ� 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fileData"></param>
        /// <returns></returns>
        public int ExportFromDatabase(FS.HISFC.Models.File.DataFileInfo dt, ref string fileData)
        {
            string strSql = "", sql = "";
            if (dt.ID == null || dt.ID == "") return -1;
            if (dt.Type.Trim() == "0")//����
            {
                if (this.Sql.GetSql("Management.DataFile.ExportFromDatabase", ref strSql) == -1) return -1;
            }
            else if (dt.Type.Trim() == "1") //ģ��
            {
                if (this.Sql.GetSql("Management.DataFile.ExportFromDatabase.Modual", ref strSql) == -1) return -1;
            }
            else
            {
                this.Err = "δ֪�ļ�����";
                return -1;
            }

            try
            {
                sql = string.Format(strSql, dt.ID);
            }
            catch (Exception ex)
            {
                this.Err = "ExportFromDatabase��ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }

            fileData = this.ExecSqlReturnOne(sql);
            if (fileData == "-1") return -1;
            return 0;
        }

        #endregion


        #region add by lijp 2011-11-16 �������뵥���

        /// <summary>
        /// ����ļ� 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fileData"></param>
        /// <returns></returns>
        public int ExportFromDatabasePacsApply(FS.HISFC.Models.File.DataFileInfo dt, ref byte[] fileData)
        {
            string strSql = "", sql = "";
            if (dt.ID == null || dt.ID == "") return -1;

            if (dt.Type.Trim() == "0")//����
            {
                if (this.Sql.GetSql("Pacs.Management.DataFile.ExportFromDatabase.byte", ref strSql) == -1) return -1;
            }
            else if (dt.Type.Trim() == "1") //ģ��
            {
                if (this.Sql.GetSql("Management.DataFile.ExportFromDatabase.Modual.byte", ref strSql) == -1) return -1;
            }
            else
            {
                this.Err = "δ֪�ļ�����";
                return -1;
            }

            try
            {
                sql = string.Format(strSql, dt.ID);
            }
            catch (Exception ex)
            {
                this.Err = "ExportFromDatabase��ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }

            fileData = this.OutputBlob(sql);
            return 0;
        }

        /// <summary>
        /// ���ļ����뵽���ݿ���
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fileData">������ļ�����</param>
        /// <returns></returns>
        public int ImportToDatabasePacsApply(FS.HISFC.Models.File.DataFileInfo dt, byte[] fileData)
        {
            string strSql = "", sql = "";
            if (dt.ID == null || dt.ID == "") return -1;
            if (dt.Type.Trim() == "0")//����
            {
                if (this.Sql.GetSql("Pacs.Management.DataFile.ImportToDatabase.byte", ref strSql) == -1) return -1;
            }
            else if (dt.Type.Trim() == "1") //ģ��
            {
                if (this.Sql.GetSql("Management.DataFile.ImportToDatabase.Modual.byte", ref strSql) == -1) return -1;
            }
            else
            {
                this.Err = "δ֪�ļ�����";
                return -1;
            }

            try
            {
                sql = string.Format(strSql, dt.ID);
            }
            catch (Exception ex)
            {
                this.Err = "ImportToDatabase��ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }

            return this.InputBlob(sql, fileData);
        }

        /// <summary>
        /// �������뵥
        /// </summary>
        /// <param name="pacsComList"></param>
        /// <param name="pacsList"></param>
        /// <param name="pacsItems"></param>
        /// <returns></returns>
        public int SavePacsApply(ArrayList pacsComList, ArrayList pacsList, ArrayList pacsItems)
        {
            //���뵥����
            int ret = UpdatePacsApply(pacsComList, pacsList);
            if (ret == -1) return -1;
            //if (ret >0) return 0;
            if (ret == 0)
            {
                ret = InsertPacsApply(pacsComList, pacsList);
                if (ret == -1) return -1;
            }
            if (pacsItems.Count != 0)
            {
                //ȫ��ɾ��
                if (DeletPacsItem(pacsComList[0].ToString()) == -1) return -1;
                foreach (string a in pacsItems)
                {
                    if (a != "" && a != string.Empty)
                    {
                        if (InsertPacsItem(pacsComList[0].ToString(), a) == -1) return -1;
                    }
                }
                if (pacsComList[3].ToString() == "1")
                {
                    string sql = @" Update Met_Ipm_Order
                                  Set Apply_NO='{0}' Where 
                                COMB_NO=( Select COMB_NO From Met_Ipm_Order Where MO_ORDER='{1}' And INPATIENT_NO='{2}' )
                                ";
                    sql = String.Format(sql, pacsComList[0].ToString(), pacsItems[0].ToString().Split(';')[0], pacsComList[2].ToString());
                    if (this.ExecNoQuery(sql) <= 0)
                        return -1;
                }
            }
            return 0;
        }

        /// <summary>
        /// ������뵥��Ϣ
        /// </summary>
        /// <param name="pacsApp"></param>
        /// <returns></returns>
        private int InsertPacsApply(ArrayList pacsComList, ArrayList pacsList)
        {
            string[] arr = new string[]
            {
            pacsComList[0].ToString(),//���뵥��Ϣ��Ψһ��ʶ
            pacsComList[1].ToString(),//���뵥����
            pacsComList[2].ToString(),//��ˮ��
            pacsComList[3].ToString(),//��ˮ�ű�ʶ;0���1סԺ��2���
            pacsComList[4].ToString(),//�豸����ID��Ҳ���ж����뵥���͵ĸ���
            pacsList[0].ToString(),//���뵥���,��������
            pacsList[1].ToString(),//���뵥����ʷ���ֱ���Բ�ͬ����������Ӧ�Ĳ�ʷ
            pacsList[2].ToString(),//���뵥����
            pacsList[3].ToString(),//�Ƿ��շѣ�0����1���ǣ�
            pacsList[4].ToString(),//����ҽ��
            pacsList[5].ToString(),//������ȷ�ϣ�ҽ��
            pacsList[6].ToString(),//ִ�п���

            pacsList[7].ToString(),//�������ʱ��
            pacsList[8].ToString()==""?this.Sql.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss"):pacsList[8].ToString(),//����ʱ��
            pacsList[9].ToString(),//ȡ������
            pacsList[10].ToString(),//ȡ�������뻼�ߵĹ�ϵ
            pacsList[11].ToString()==""?this.Sql.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss"):pacsList[11].ToString(),//ԤԼ���ʱ��

            //pacsList[12].ToString(),//���뵥״̬
            pacsComList[5].ToString(),//ģ���
            pacsList[12].ToString(),
            pacsList[13].ToString(),
            pacsList[14].ToString()

             };
            string sql = string.Empty;
            if (this.Sql.GetSql("Pacs.PacsApplication.Insert", ref sql) == -1) return -1;
            sql = string.Format(sql, arr);
            int ret = this.ExecNoQuery(sql);
            if (ret == -1) return -1;
            return ret;
        }

        /// <summary>
        /// �޸����뵥��Ϣ
        /// </summary>
        /// <param name="pacsApp"></param>
        /// <returns></returns>
        private int UpdatePacsApply(ArrayList pacsComList, ArrayList pacsList)
        {

            string[] arr = new string[]
            {
            pacsComList[0].ToString(),//���뵥��Ϣ��Ψһ��ʶ
            pacsComList[1].ToString(),//���뵥����
            pacsComList[2].ToString(),//��ˮ��
            pacsComList[3].ToString(),//��ˮ�ű�ʶ;0���1סԺ��2���
            pacsComList[4].ToString(),//�豸����ID��Ҳ���ж����뵥���͵ĸ���
            pacsList[0].ToString(),//���뵥���,��������
            pacsList[1].ToString(),//���뵥����ʷ���ֱ���Բ�ͬ����������Ӧ�Ĳ�ʷ
            pacsList[2].ToString(),//���뵥����
            pacsList[3].ToString(),//�Ƿ��շѣ�0����1���ǣ�
            pacsList[4].ToString(),//����ҽ��
            pacsList[5].ToString(),//������ȷ�ϣ�ҽ��
            pacsList[6].ToString(),//ִ�п���
            pacsList[7].ToString(),//�������ʱ��
            pacsList[8].ToString()==""?this.Sql.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss"):pacsList[8].ToString(),//����ʱ��
            pacsList[9].ToString(),//ȡ������
            pacsList[10].ToString(),//ȡ�������뻼�ߵĹ�ϵ
            pacsList[11].ToString()==""?this.Sql.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss"):pacsList[11].ToString(),//ԤԼ���ʱ��

            //pacsList[12].ToString(),//���뵥״̬
            pacsComList[5].ToString(),//ģ���
            pacsList[12].ToString(),
            pacsList[13].ToString(),
            pacsList[14].ToString()
             };
            string sql = string.Empty;
            if (this.Sql.GetSql("Pacs.PacsApplication.Update", ref sql) == -1) return -1;
            sql = string.Format(sql, arr);
            int ret = this.ExecNoQuery(sql);
            if (ret == -1) return -1;
            return ret;
        }

        /// <summary>
        /// ��������Ŀ
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private int InsertPacsItem(string applicationId, string item)
        {
            string[] arr1 = item.Split(';');

            string[] arr = new string[]
            {
               applicationId,
               arr1[2],  //ItemID
               FS.FrameWork.Management.Connection.Operator.ID,
               new FS.FrameWork.Management.DataBaseManger().GetSysDateTime().ToString(),
               arr1[0], //OrderID
               arr1[1], 
               arr1[3],  //��鷽��
               arr1[4]   //��鲿λ
            };

            string sql = string.Empty;
            if (this.Sql.GetSql("Pacs.Items.Insert", ref sql) == -1) return -1;
            sql = string.Format(sql, arr);
            int ret = this.ExecNoQuery(sql);
            if (ret == -1) return -1;
            return ret;
        }

        /// <summary>
        /// ɾ�������Ŀ
        /// </summary>
        /// <param name="ApplicationID"></param>
        /// <returns></returns>
        private int DeletPacsItem(string ApplicationID)
        {
            string sql = string.Empty;
            if (this.Sql.GetSql("Pacs.Items.DeleteByApplicationID", ref sql) == -1) return -1;
            sql = string.Format(sql, ApplicationID);
            int ret = this.ExecNoQuery(sql);
            if (ret == -1) return -1;
            return ret;
        }

        #endregion
    }
}
