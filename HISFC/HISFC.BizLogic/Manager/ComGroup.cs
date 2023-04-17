using System;
using System.Collections;
namespace FS.HISFC.BizLogic.Manager
{
	/// <summary>
	/// ComGroup ��ժҪ˵����
	/// </summary>
	public class ComGroup :DataBase 
	{
		public ComGroup()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ��ȡ��������
		/// </summary>
		/// <returns></returns>
		public ArrayList GetAllGroups()
		{
			#region sql
//			SELECT group_id,   --����ID
//					group_name,   --��������
//					spell_code,   --����ƴ����
//					input_code,   --����������
//					group_kind,   --��������  0 .������  1 .������
//					dept_code,   --���׿���
//					sort_id,   --��ʾ˳��
//					valid_flag,   --��Ч��־��1��Ч/2��Ч
//					remark,   --���ױ�ע
//					oper_code,   --����Ա
//					oper_date    --����ʱ��
//				FROM fin_com_group   --������Ϣ��,���������շѡ�סԺ�շѡ���ʿվ�շѡ�����շѡ��������ס��ն�����
			#endregion
			#region
//			ArrayList List = null;
//			string strSql = "";
//			if (this.GetSQL("Manager.ComGroup.GetAllGroups",ref strSql)==-1) return null;
//			try
//			{				
//				if(this.ExecQuery(strSql)==-1)return null;
//				List = new ArrayList();
//				FS.HISFC.Models.Fee.ComGroup  info =null;
//				while(this.Reader.Read())
//				{
//					info =new FS.HISFC.Models.Fee.ComGroup();
//					info.ID=Reader[0].ToString();
//					info.Name=Reader[1].ToString();
//					info.spellCode=Reader[2].ToString();
//					info.inputCode=Reader[3].ToString();
//					info.groupKind=Reader[4].ToString();
//					info.deptCode=Reader[5].ToString();
//					info.sortId=System.Convert.ToInt32(Reader[6].ToString());
//					info.validFlag=Reader[7].ToString();
//					info.reMark=Reader[8].ToString();
//					info.operCode=Reader[9].ToString();
//					info.operDate=FS.FrameWork.Function.NConvert.ToDateTime(Reader[10].ToString());
//					
//					List.Add(info);					
//				}
//				this.Reader.Close();
//			}
//			catch(Exception ee)
//			{
//				this.Err = "Manager.ComGroup.GetAllGroups"+ee.Message;
//				this.ErrCode=ee.Message;
//				WriteErr();
//				return null;
//			}
//			return List;
			#endregion
			return  this.GetAllGroups("1");
		}

		/// <summary>
		/// ��ȡ��������
		/// </summary>
		/// <param name="GroupKind">0 �����ã�1������,ALL ȫ��</param>
		/// <returns></returns>
		public ArrayList GetAllGroups(string GroupKind)
		{
			ArrayList List = null;
			string strSql = "";
			if (this.GetSQL("Manager.ComGroup.GetAllGroups",ref strSql)==-1) return null;
			try
			{	
				strSql = string.Format(strSql,GroupKind);
				if(this.ExecQuery(strSql)==-1)return null;
				List = new ArrayList();
				FS.HISFC.Models.Fee.ComGroup  info =null;
				while(this.Reader.Read())
				{
					info =new FS.HISFC.Models.Fee.ComGroup();
					info.ID=Reader[0].ToString();
					info.Name=Reader[1].ToString();
					info.spellCode=Reader[2].ToString();
					info.inputCode=Reader[3].ToString();
					info.groupKind=Reader[4].ToString();
					info.deptCode=Reader[5].ToString();
					info.sortId=System.Convert.ToInt32(Reader[6].ToString());
                    info.ValidState = (FS.HISFC.Models.Base.EnumValidState)(FS.FrameWork.Function.NConvert.ToInt32(Reader[7].ToString()));                    
					info.reMark=Reader[8].ToString();
					info.operCode=Reader[9].ToString();
					info.operDate=FS.FrameWork.Function.NConvert.ToDateTime(Reader[10].ToString());
                    //���Ӹ����ڵ�
                    info.ParentGroupID = this.Reader[12].ToString();
					
					List.Add(info);					
				}
				this.Reader.Close();
			}
			catch(Exception ee)
			{
				this.Err = "Manager.ComGroup.GetAllGroups"+ee.Message;
				this.ErrCode=ee.Message;
				WriteErr();
				return null;
			}
			return List;
		}

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="GroupKind">0 �����ã�1������,ALL ȫ��</param>
        /// <returns></returns>
        public ArrayList GetAllGroupsByRoot(string GroupKind)
        {
            ArrayList List = null;
            string strSql = "";
            if (this.GetSQL("Manager.ComGroup.GetAllGroupsByRoot", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, GroupKind);
                if (this.ExecQuery(strSql) == -1) return null;
                List = new ArrayList();
                FS.HISFC.Models.Fee.ComGroup info = null;
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.Fee.ComGroup();
                    info.ID = Reader[0].ToString();
                    info.Name = Reader[1].ToString();
                    info.spellCode = Reader[2].ToString();
                    info.inputCode = Reader[3].ToString();
                    info.groupKind = Reader[4].ToString();
                    info.deptCode = Reader[5].ToString();
                    info.sortId = System.Convert.ToInt32(Reader[6].ToString());
                    info.ValidState = (FS.HISFC.Models.Base.EnumValidState)(FS.FrameWork.Function.NConvert.ToInt32(Reader[7].ToString()));
                    info.reMark = Reader[8].ToString();
                    info.operCode = Reader[9].ToString();
                    info.operDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[10].ToString());
                    //���Ӹ����ڵ�
                    info.ParentGroupID = this.Reader[12].ToString();

                    List.Add(info);
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = "Manager.ComGroup.GetAllGroupsByRoot" + ee.Message;
                this.ErrCode = ee.Message;
                WriteErr();
                return null;
            }
            return List;
        }

        /// <summary>
        /// ���ݿ��һ�ȡ��������{9F3CF1C0-AF96-4d17-96B1-6B34636A42A7}
        /// </summary>
        /// <param name="GroupKind">0 �����ã�1������,ALL ȫ��</param>
        /// <returns></returns>
        public ArrayList GetValidGroupListByRoot(string deptCode)
        {
            ArrayList List = null;
            string strSql = "";
            if (this.GetSQL("Manager.ComGroup.GetAllGroupsByRootAndDeptCode", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, deptCode);
                if (this.ExecQuery(strSql) == -1) return null;
                List = new ArrayList();
                FS.HISFC.Models.Fee.ComGroup info = null;
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.Fee.ComGroup();
                    info.ID = Reader[0].ToString();
                    info.Name = Reader[1].ToString();
                    info.spellCode = Reader[2].ToString();
                    info.inputCode = Reader[3].ToString();
                    info.groupKind = Reader[4].ToString();
                    info.deptCode = Reader[5].ToString();
                    info.sortId = System.Convert.ToInt32(Reader[6].ToString());
                    info.ValidState = (FS.HISFC.Models.Base.EnumValidState)(FS.FrameWork.Function.NConvert.ToInt32(Reader[7].ToString()));
                    info.reMark = Reader[8].ToString();
                    info.operCode = Reader[9].ToString();
                    info.operDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[10].ToString());
                    //���Ӹ����ڵ�
                    info.ParentGroupID = this.Reader[11].ToString();

                    List.Add(info);
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = "Manager.ComGroup.GetAllGroupsByRootAndDeptCode" + ee.Message;
                this.ErrCode = ee.Message;
                WriteErr();
                return null;
            }
            return List;
        }

        /// <summary>
        /// ��ȡ��������{9F3CF1C0-AF96-4d17-96B1-6B34636A42A7}
        /// </summary>
        /// <param name="GroupKind">0 �����ã�1������,ALL ȫ��</param>
        /// <returns></returns>
        public ArrayList GetGroupsByDeptParent(string GroupKind, string deptCode, string parentGroupID)
        {
            ArrayList List = null;
            string strSql = "";
            if (this.GetSQL("Manager.ComGroup.GetGroupsByDeptParent", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, GroupKind,deptCode,parentGroupID);
                if (this.ExecQuery(strSql) == -1) return null;
                List = new ArrayList();
                FS.HISFC.Models.Fee.ComGroup info = null;
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.Fee.ComGroup();
                    info.ID = Reader[0].ToString();
                    info.Name = Reader[1].ToString();
                    info.spellCode = Reader[2].ToString();
                    info.inputCode = Reader[3].ToString();
                    info.groupKind = Reader[4].ToString();
                    info.deptCode = Reader[5].ToString();
                    info.sortId = System.Convert.ToInt32(Reader[6].ToString());
                    info.ValidState = (FS.HISFC.Models.Base.EnumValidState)(FS.FrameWork.Function.NConvert.ToInt32(Reader[7].ToString()));
                    info.reMark = Reader[8].ToString();
                    info.operCode = Reader[9].ToString();
                    info.operDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[10].ToString());
                    //���Ӹ����ڵ�
                    info.ParentGroupID = this.Reader[12].ToString();

                    List.Add(info);
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = "Manager.ComGroup.GetAllGroups" + ee.Message;
                this.ErrCode = ee.Message;
                WriteErr();
                return null;
            }
            return List;
        }

		public FS.HISFC.Models.Fee.ComGroup GetComGroupByGroupID(string GroupID)
		{
			string strSql = "";
			//select group_id,group_name ,a.spell_code,a.input_code,group_Kind,dept_name,a.sort_id,valid_flag,remark from fin_com_group a ,com_department b where a.dept_code =b.dept_code and group_id ='{0}' and a.PARENT_CODE ='[��������]'   and a.CURRENT_CODE  ='[��������]';
			FS.HISFC.Models.Fee.ComGroup info = null;
			try
			{
				if (this.GetSQL("Manager.ComGroup.GetComGroupByGroupID",ref strSql)==-1) return null;
				strSql = string.Format(strSql,GroupID);
				this.ExecQuery(strSql);
				while(this.Reader.Read())
				{
					info = new FS.HISFC.Models.Fee.ComGroup();
					info.ID =Reader[0].ToString();
					info.Name =Reader[1].ToString();
					info.spellCode =Reader[2].ToString();
					info.inputCode =Reader[3].ToString();
					info.groupKind =Reader[4].ToString();
					info.deptName = Reader[5].ToString();
					if(Reader[6]!=DBNull.Value)
					{
						info.sortId =Convert.ToInt32(Reader[6]);
					}
					else
					{
						info.sortId =0;
					}
                    info.ValidState = (FS.HISFC.Models.Base.EnumValidState)(FS.FrameWork.Function.NConvert.ToInt32(Reader[7].ToString()));
					info.reMark =Reader[8].ToString();
				}
				this.Reader.Close();
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
			return info;
		}
		public  int InsertInToComGroup(FS.HISFC.Models.Fee.ComGroup info )
		{
			string strSql ="";
            string OperCode = this.Operator.ID;
            //{9F3CF1C0-AF96-4d17-96B1-6B34636A42A7}���Ӹ����ڵ�
            string[] str = new string[] { info.ID, info.Name, info.spellCode, info.inputCode, info.groupKind, info.deptName, info.sortId.ToString(), ((int)info.ValidState).ToString(), info.reMark, OperCode,info.ParentGroupID};
			try
			{
				//insert into fin_com_group values('[��������]','[��������]','{0}','{1}','{2}','{3}','{4}',(select dept_code from com_department where dept_name ='{5}' and PARENT_CODE ='[��������]' and CURRENT_CODE  ='[��������]'),'{6}','{7}','{8}','{9}',sysdate)
				if (this.GetSQL("Manager.ComGroup.InsertInToComGroup",ref strSql)==-1) return -1;
				
                //strSql = string.Format(strSql, info.ID, info.Name, info.spellCode, info.inputCode, info.groupKind, info.deptName, info.sortId, ((int)info.ValidState).ToString(), info.reMark, OperCode);
			}
			catch(Exception ee)
			{
				this.Err= ee.Message;
			}
			return this.ExecNoQuery(strSql,str);
		}
		public  int ModefyComGroup(FS.HISFC.Models.Fee.ComGroup info)
		{
			string strSql ="";
			try
			{
				//update fin_com_group set GROUP_NAME ='{0}',SPELL_COD ='{1}',INPUT_CODE='{2}',GROUP_KIND='{3}',DEPT_CODE=(select dept_code from com_department where dept_name ='{4}' and PARENT_CODE ='[��������]' and CURRENT_CODE  ='[��������]') ,SORT_ID ={5},VALID_FLAG ='{6}',REMARK ='{7}',OPER_CODE ='{8}' where GROUP_ID ='{9}' and PARENT_CODE ='[��������]' and CURRENT_CODE  ='[��������]'
				if (this.GetSQL("Manager.ComGroup.ModefyComGroup",ref strSql)==-1) return -1;
				string OperCode = this.Operator.ID;
                strSql = string.Format(strSql, info.Name, info.spellCode, info.inputCode, info.groupKind, info.deptName, info.sortId, ((int)info.ValidState).ToString(), info.reMark, OperCode, info.ID);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		public  int DeleteComGroup(FS.HISFC.Models.Fee.ComGroup com)
		{
			string strSql ="";
			try
			{
				//delete fin_com_group where group_id = '{0}'
				if (this.GetSQL("Manager.ComGroup.DeleteComGroup",ref strSql)==-1) return -1;
				strSql = string.Format(strSql,com.ID);
				if(this.ExecNoQuery(strSql)==-1)return -1;
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				WriteErr();
				return -1;
			}		
			return 0;
		}
		/// <summary>
		/// ɾ������������ϸ
		/// </summary>
		/// <param name="groupID"></param>
		/// <returns></returns>
		public int DelGroupDetails(string groupID)
		{
			#region sql
			//DELETE FROM fin_com_groupdetail 
			//		WHERE parent_code='[��������]' and current_code='[��������]' and group_id='{0}'
			#endregion
			string strSql="";
			
			if(GetSQL("Manager.ComGroup.DeleteDetails",ref strSql)==-1)return -1;
			try
			{
				strSql=string.Format(strSql,groupID);
				if(ExecNoQuery(strSql)==-1)return -1;
			}
			catch(Exception e)
			{
				this.Err="Manager.ComGroup.DeleteDetails!"+e.Message;
				WriteErr();
				return -1;
			}
			return 0;
		}
		/// <summary>
		/// ��ѯָ����������ģ������
		/// </summary>
		/// <param name="DeptID">���ұ���</param>
		/// <returns>���׶����б�</returns>
		public ArrayList GetOpsTreeComGroup(string DeptID)
		{
			ArrayList List = null;
			string strSql = "";
			if (this.GetSQL("Manager.ComGroup.GetOpsTreeComGroup",ref strSql)==-1) return null;
			strSql = string.Format(strSql,DeptID);
			try
			{
				this.ExecQuery(strSql);
				List = new ArrayList();				
				while(this.Reader.Read())
				{
					FS.HISFC.Models.Fee.ComGroup info =new FS.HISFC.Models.Fee.ComGroup();
					info.deptCode  =Reader[0].ToString();
					info.ID = Reader[1].ToString();
					info.Name =Reader[2].ToString();
					List.Add(info);
				}
				Reader.Close();
			}
			catch(Exception ee)
			{
				this.Err = "Manager.ComGroup.GetOpsTreeComGroup";
				this.ErrCode = ee.Message;
				this.WriteErr();
				return null;
			}
			return List;
		}
		/// <summary>
		/// �����һ�ȡ������Ч����
		/// </summary>
		/// <returns></returns>
		public ArrayList GetValidGroupList(string deptID)
		{
			#region sql
//			 SELECT group_id,   --����ID
//					group_name,   --��������
//					spell_code,   --����ƴ����
//					input_code,   --����������
//					group_kind,   --��������  0 .������  1 .������
//					dept_code,   --���׿���
//					sort_id,   --��ʾ˳��
//					valid_flag,   --��Ч��־��1��Ч/2��Ч
//					remark,   --���ױ�ע
//					oper_code,   --����Ա
//					oper_date    --����ʱ��
//			   FROM fin_com_group   --������Ϣ��,���������շѡ�סԺ�շѡ���ʿվ�շѡ�����շѡ��������ס��ն�����
//			  WHERE parent_code='[��������]' and current_code='[��������]' and dept_code='{0}' and valid_flag='1'
			#endregion
			string strSql="";
			ArrayList group=new ArrayList();

			if(this.GetSQL("Manager.ComGroup.GetValidGroupList",ref strSql)==-1)return null;
			try
			{
				strSql=string.Format(strSql,deptID);
				if(this.ExecQuery(strSql)==-1)return null;
				while(Reader.Read())
				{
					FS.HISFC.Models.Fee.ComGroup c=new FS.HISFC.Models.Fee.ComGroup();
					c.ID=Reader[0].ToString();
					c.Name=Reader[1].ToString();
					c.spellCode=Reader[2].ToString();
					c.inputCode=Reader[3].ToString();
					c.groupKind=Reader[4].ToString();
					c.deptCode=Reader[5].ToString();
					if(Reader[6]!=DBNull.Value)
					{
						c.sortId =Convert.ToInt32(Reader[6].ToString());
					}
					else
					{
						c.sortId =0;
					}
                    c.ValidState = (FS.HISFC.Models.Base.EnumValidState)(FS.FrameWork.Function.NConvert.ToInt32(Reader[7].ToString()));
					c.reMark=Reader[8].ToString();
					c.operCode=Reader[9].ToString();
					c.operDate=FS.FrameWork.Function.NConvert.ToDateTime(Reader[10].ToString());
					group.Add(c);
				}
				Reader.Close();
			}
			catch(Exception e)
			{
				this.Err="Manager.ComGroup.GetValidGroupList!"+e.Message;
				WriteErr();
				if(Reader.IsClosed!=true)Reader.Close();
				return null;
			}
			return group;
		}
		/// <summary>
		/// ���ݿ��Ҵ������������ȡ����
		/// </summary>
		/// <param name="GroupKind">0 �����ã�1������,ALL ȫ��</param>
		/// <param name="deptID">���Ҵ���</param>
		/// <returns></returns>
		public ArrayList GetGroupsByDept(string GroupKind,string deptID)
		{
			#region sql
			//			 SELECT group_id,   --����ID
			//					group_name,   --��������
			//					spell_code,   --����ƴ����
			//					input_code,   --����������
			//					group_kind,   --��������  0 .������  1 .������
			//					dept_code,   --���׿���
			//					sort_id,   --��ʾ˳��
			//					valid_flag,   --��Ч��־��1��Ч/2��Ч
			//					remark,   --���ױ�ע
			//					oper_code,   --����Ա
			//					oper_date    --����ʱ��
			//			   FROM fin_com_group   --������Ϣ��,���������շѡ�סԺ�շѡ���ʿվ�շѡ�����շѡ��������ס��ն�����
			//			  WHERE parent_code='[��������]' and current_code='[��������]' and dept_code='{0}' and valid_flag='1'
			//                 ( and group_kind = '{2}'  or 'ALL'='{2}')
			#endregion
			string strSql="";
			ArrayList group=new ArrayList();

			if(this.GetSQL("Manager.ComGroup.GetGroupsByDept",ref strSql)==-1)return null;
			try
			{
				strSql=string.Format(strSql,deptID,GroupKind);
				if(this.ExecQuery(strSql)==-1)return null;
				while(Reader.Read())
				{
					FS.HISFC.Models.Fee.ComGroup c=new FS.HISFC.Models.Fee.ComGroup();
					c.ID=Reader[0].ToString();
					c.Name=Reader[1].ToString();
					c.spellCode=Reader[2].ToString();
					c.inputCode=Reader[3].ToString();
					c.groupKind=Reader[4].ToString();
					c.deptCode=Reader[5].ToString();
					if(Reader[6]!=DBNull.Value)
					{
						c.sortId =Convert.ToInt32(Reader[6].ToString());
					}
					else
					{
						c.sortId =0;
					}
                    c.ValidState = (FS.HISFC.Models.Base.EnumValidState)(FS.FrameWork.Function.NConvert.ToInt32(Reader[7].ToString()));
					c.reMark=Reader[8].ToString();
					c.operCode=Reader[9].ToString();
					c.operDate=FS.FrameWork.Function.NConvert.ToDateTime(Reader[10].ToString());
					group.Add(c);
				}
				Reader.Close();
			}
			catch(Exception e)
			{
				this.Err="Manager.ComGroup.GetGroupsByDept!"+e.Message;
				WriteErr();
				if(Reader.IsClosed!=true)Reader.Close();
				return null;
			}
			return group;
		}

        /// <summary>
        /// ���������������ױ�����ұ���
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="groupID"></param>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public ArrayList QueryGroupsByName(string groupName , string deptID)
        {
            string strSql = "";
            ArrayList group = new ArrayList();

            if (this.GetSQL("Manager.ComGroup.QueryGroupsByName", ref strSql) == -1) return null;
            try
            {
                string [] str = new string[] { groupName, deptID };
                //strSql = string.Format(strSql, groupName, deptID);
                if (this.ExecQuery(strSql, str) == -1) return null;
                while (Reader.Read())
                {
                    FS.HISFC.Models.Fee.ComGroup c = new FS.HISFC.Models.Fee.ComGroup();
                    c.ID = Reader[0].ToString();
                    c.Name = Reader[1].ToString();
                    c.spellCode = Reader[2].ToString();
                    c.inputCode = Reader[3].ToString();
                    c.groupKind = Reader[4].ToString();
                    c.deptCode = Reader[5].ToString();
                    if (Reader[6] != DBNull.Value)
                    {
                        c.sortId = Convert.ToInt32(Reader[6].ToString());
                    }
                    else
                    {
                        c.sortId = 0;
                    }
                    c.ValidState = (FS.HISFC.Models.Base.EnumValidState)(FS.FrameWork.Function.NConvert.ToInt32(Reader[7].ToString()));
                    c.reMark = Reader[8].ToString();
                    c.operCode = Reader[9].ToString();
                    c.operDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[10].ToString());
                    group.Add(c);
                }
                Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = "Manager.ComGroup.GetGroupsByDept!" + e.Message;
                WriteErr();
                if (Reader.IsClosed != true) Reader.Close();
                return null;
            }
            return group;
        }
        /// <summary>
        /// �����һ�ȡ������Ч����
        /// </summary>
        /// <returns></returns>
        public ArrayList GetAllGroupList(string deptID)
        {
            #region sql
            //			 SELECT group_id,   --����ID
            //					group_name,   --��������
            //					spell_code,   --����ƴ����
            //					input_code,   --����������
            //					group_kind,   --��������  0 .������  1 .������
            //					dept_code,   --���׿���
            //					sort_id,   --��ʾ˳��
            //					valid_flag,   --��Ч��־��1��Ч/2��Ч
            //					remark,   --���ױ�ע
            //					oper_code,   --����Ա
            //					oper_date    --����ʱ��
            //			   FROM fin_com_group   --������Ϣ��,���������շѡ�סԺ�շѡ���ʿվ�շѡ�����շѡ��������ס��ն�����
            //			  WHERE parent_code='[��������]' and current_code='[��������]' and dept_code='{0}' and valid_flag='1'
            #endregion
            string strSql = "";
            ArrayList group = new ArrayList();

            if (this.GetSQL("Manager.ComGroup.GetAllGroupList", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, deptID);
                if (this.ExecQuery(strSql) == -1) return null;
                while (Reader.Read())
                {
                    FS.HISFC.Models.Fee.ComGroup c = new FS.HISFC.Models.Fee.ComGroup();
                    c.ID = Reader[0].ToString();
                    c.Name = Reader[1].ToString();
                    c.spellCode = Reader[2].ToString();
                    c.inputCode = Reader[3].ToString();
                    c.groupKind = Reader[4].ToString();
                    c.deptCode = Reader[5].ToString();
                    if (Reader[6] != DBNull.Value)
                    {
                        c.sortId = Convert.ToInt32(Reader[6].ToString());
                    }
                    else
                    {
                        c.sortId = 0;
                    }
                    c.ValidState = (FS.HISFC.Models.Base.EnumValidState)(FS.FrameWork.Function.NConvert.ToInt32(Reader[7].ToString()));
                    c.reMark = Reader[8].ToString();
                    c.operCode = Reader[9].ToString();
                    c.operDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[10].ToString());
                    group.Add(c);
                }
                Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = "Manager.ComGroup.GetValidGroupList!" + e.Message;
                WriteErr();
                if (Reader.IsClosed != true) Reader.Close();
                return null;
            }
            return group;
        }
	}
}
