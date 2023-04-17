using System;
using System.Collections;


using FS.FrameWork.Models;
using FS.HISFC.Models;

using FS.HISFC.Models.Base;

namespace FS.HISFC.BizLogic.Manager {

	/// <summary>
	/// ���ҷ����ά��������
	/// cuipeng
	/// </summary>
	public class DepartmentStatManager : DataBase {
		/// <summary>
		/// 
		/// </summary>
		public DepartmentStatManager() {
		}
						
			
		/// <summary>
		/// ȡ���ҷ�����ȫ�������б�
		/// </summary>
		/// <returns></returns>
		public ArrayList LoadAll() {
			string sql = "";
			if(this.GetSQL("Manager.DepartmentStatManager.LoadAll",ref sql)== -1)
				return null;

			if(this.ExecQuery(sql) == -1) return null;

			ArrayList list = new ArrayList();

			while(this.Reader.Read()) {
				FS.HISFC.Models.Base.DepartmentStat info = PrepareData();
                
				list.Add(info);
			}
			this.Reader.Close();

			return list;
		}


        /// <summary>
        /// ���ݿ��ҷ�����뼰�ڵ�ȼ���ȡ�˷����µĿ����б�
        /// </summary>
        /// <returns></returns>
        public ArrayList LoadDepartmentStatAndByNodeKind(string statCode,string nodekind)
        {
            string sql = "";
            if (this.GetSQL("Manager.DepartmentStatManager.LoadDepartmentStatByNodeKind", ref sql) == -1)
                return null;

            try
            {
                sql = string.Format(sql, statCode,nodekind);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            if (this.ExecQuery(sql) == -1) return null;

            ArrayList list = new ArrayList();

            while (this.Reader.Read())
            {
                FS.HISFC.Models.Base.DepartmentStat info = PrepareData();

                list.Add(info);
            }
            this.Reader.Close();

            return list;
        }
		
					
		/// <summary>
		/// ���ݿ��ҷ�����룬ȡ�˷����µĿ����б�
		/// </summary>
		/// <returns></returns>
		public ArrayList LoadDepartmentStat(string statCode) {
			string sql = "";
			if(this.GetSQL("Manager.DepartmentStatManager.LoadDepartmentStat",ref sql)== -1)
				return null;

			try {
				sql = string.Format(sql, statCode);
			}
			catch(Exception ex) {
				this.Err = ex.Message;
				return null;
			}

			if(this.ExecQuery(sql) == -1) return null;

			ArrayList list = new ArrayList();

			while(this.Reader.Read()) {
				FS.HISFC.Models.Base.DepartmentStat info = PrepareData();
                
				list.Add(info);
			}
			this.Reader.Close();

			return list;
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="endNode"></param>
		/// <returns></returns>
		public ArrayList LoadByNodeKind(bool endNode) {
			int nodeKind = 1;
			if(endNode) {
				nodeKind = 1;
			}
			else
				nodeKind = 0;
			string sql = "";
			if(this.GetSQL("Manager.DepartmentStatManager.LoadByNodeKind",ref sql)== -1)
				return null;

			try {
				sql=string.Format(sql,nodeKind);
			}
			catch(Exception ex) {
				this.ErrCode=ex.Message;
				this.Err=" "+ex.Message;
				this.WriteErr();
				return null;
			}

			if(this.ExecQuery(sql) == -1) return null;

			ArrayList list = new ArrayList();

			while(this.Reader.Read()) {
				FS.HISFC.Models.Base.DepartmentStat info = PrepareData();
                
				list.Add(info);
			}
			this.Reader.Close();

			return list;
		}
		

		/// <summary>
		/// 
		/// </summary>
		/// <param name="statCode"></param>
		/// <param name="endNode"></param>
		/// <returns></returns>
		public ArrayList LoadByNodeKind(string statCode, bool endNode) {
			int nodeKind = 1;
			if(endNode) {
				nodeKind = 1;
			}
			else
				nodeKind = 0;
			string sql = "";
			if(this.GetSQL("Manager.DepartmentStatManager.LoadByStatNodeKind",ref sql)== -1)
				return null;

			try {
				sql=string.Format(sql,statCode,nodeKind);
			}
			catch(Exception ex) {
				this.ErrCode=ex.Message;
				this.Err=" "+ex.Message;
				this.WriteErr();
				return null;
			}

			if(this.ExecQuery(sql) == -1) return null;

			ArrayList list = new ArrayList();

			while(this.Reader.Read()) {
				FS.HISFC.Models.Base.DepartmentStat info = PrepareData();
                
				list.Add(info);
			}
			this.Reader.Close();

			return list;
		}
		
		
		/// <summary>
		/// ����ͳ�Ʒ�����룬�������ұ�����ȡ�������¼��ڵ������Ϣ��
		/// </summary>
		/// <param name="statCode">ͳ�ƴ������</param>
		/// <param name="parDeptCode">�������ұ���</param>
		/// <param name="nodeKind">��������: 0��ʵ����, 1���ҷ���(�����), 2ȫ������</param>
		/// <returns></returns>
		public ArrayList LoadChildren(string statCode, string parDeptCode, int nodeKind) {
			string sql = "";
			if(this.GetSQL("Manager.DepartmentStatManager.LoadChildren",ref sql)== -1)
				return null;

			try {
				sql=string.Format(sql, statCode, parDeptCode, nodeKind);
			}
			catch(Exception ex) {
				this.ErrCode=ex.Message;
				this.Err=" "+ex.Message;
				this.WriteErr();
				return null;
			}

			//ִ��sql���
			if(this.ExecQuery(sql) == -1) return null;

			ArrayList list = new ArrayList();
			while(this.Reader.Read()) {
				FS.HISFC.Models.Base.DepartmentStat info = PrepareData();     
				if (info == null) {
					this.Reader.Close();
					return null;
				}
				list.Add(info);
			}
			this.Reader.Close();

			return list;
		}
		
		
		/// <summary>
		/// ����ͳ�Ʒ�����룬�������ұ�����ȡ�������¼��ڵ������Ϣ��
		/// </summary>
		/// <param name="statCode">ͳ�ƴ������</param>
		/// <param name="parDeptCode">�������ұ���</param>
		/// <returns></returns>
		public ArrayList LoadChildrenUnionDept(string statCode, string parDeptCode) {
			string sql = "";
			if(this.GetSQL("Manager.DepartmentStatManager.LoadChildrenUnionDept",ref sql)== -1)
				return null;

			try {
				sql=string.Format(sql, statCode, parDeptCode); 
			}
			catch(Exception ex) {
				this.ErrCode=ex.Message;
				this.Err=" "+ex.Message;
				this.WriteErr();
				return null;
			}

			//ִ��sql���
			if(this.ExecQuery(sql) == -1) return null;


			ArrayList list = new ArrayList();
			FS.HISFC.Models.Base.Department dept = null;
			while(this.Reader.Read()) {
				dept = new FS.HISFC.Models.Base.Department();
				dept.ID = this.Reader[0].ToString();		//0���ұ���
				dept.Name = this.Reader[1].ToString();		//1��������
				dept.SpellCode = this.Reader[2].ToString();	//2ƴ����
				dept.WBCode = this.Reader[4].ToString();		//3�����
				dept.DeptType.ID = this.Reader[4].ToString();	//4��������
				dept.User01 = dept.DeptType.ID.ToString();		//�������ͱ���
				dept.User02 = dept.DeptType.Name;				//������������
				list.Add(dept);
			}
			this.Reader.Close();

			return list;
		}
		

		/// <summary>
		/// ����ͳ�Ʒ�����룬���ӿ��ұ�����ȡ�丸���ڵ������Ϣ��
		/// </summary>
		/// <param name="statCode">ͳ�Ʒ������</param>
		/// <param name="deptCode">���ұ���(���ӿ���)</param>
		/// <returns></returns>
		public ArrayList LoadByChildren(string statCode, string deptCode) {
			string sql = "";
			if(this.GetSQL("Manager.DepartmentStatManager.LoadAll",ref sql)== -1)
				return null;

			string sqlWhere = "";
			if(this.GetSQL("Manager.DepartmentStatManager.LoadByChildren",ref sqlWhere)== -1)
				return null;

			try {
				sql=string.Format(sql + " " + sqlWhere, statCode, deptCode);
			}
			catch(Exception ex) {
				this.ErrCode=ex.Message;
				this.Err=" "+ex.Message;
				this.WriteErr();
				return null;
			}

			//ִ��sql���
			if(this.ExecQuery(sql) == -1) return null;

			ArrayList list = new ArrayList();
			while(this.Reader.Read()) {
				FS.HISFC.Models.Base.DepartmentStat info = PrepareData();     
				if (info == null) {
					this.Reader.Close();
					return null;
				}
				list.Add(info);
			}
			
			this.Reader.Close();
			return list;
		}
		

		/// <summary>
		/// ����ͳ�Ʒ�����룬�������ұ�����ȡ���ӽڵ������Ϣ��
		/// </summary>
		/// <param name="statCode">ͳ�Ʒ������</param>
		/// <param name="parDeptCode">�������ұ���</param>
		/// <returns></returns>
        public ArrayList LoadByParent(string statCode, string parDeptCode) {

            string sql = "";
            if (this.GetSQL("Manager.DepartmentStatManager.LoadAll", ref sql) == -1)
                return null;

			string sqlWhere = "";
            if (this.GetSQL("Manager.DepartmentStatManager.LoadByParent", ref sqlWhere) == -1)
				return null;

			try {
                sql =sql+" "+ string.Format(sqlWhere, statCode, parDeptCode);
			}
			catch(Exception ex) {
				this.ErrCode=ex.Message;
				this.Err=" "+ex.Message;
				this.WriteErr();
				return null;
			}

			//ִ��sql���
			if(this.ExecQuery(sql) == -1) return null;

			ArrayList list = new ArrayList();
			while(this.Reader.Read()) {
				FS.HISFC.Models.Base.DepartmentStat info = PrepareData();     
				if (info == null) {
					this.Reader.Close();
					return null;
				}
				list.Add(info);
			}
			this.Reader.Close();
			
			return list;
		}

        /// <summary>
        /// ���Ҷ���Ȩ������
        /// </summary>
        /// <param name="Code">һ��Ȩ�ޱ���</param>
        /// <returns></returns>
        public int DepartMentClassCount(string Code)
        {
            string Sql = string.Empty;
            if (this.GetSQL("Manager.DepartmentStatManager.SelectClassCount", ref Sql) == -1)
            {
                this.Err = "����SQL���ʧ�ܣ�";
                return -1;
            }
            try
            {
                Sql = string.Format(Sql, Code);
            }
            catch 
            {
                this.Err = "����Ȩ��ʧ�ܣ�";
                return -1;
            }
            return FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(Sql));
        }

		/// <summary>
		/// ���ݲ���Ա������ȡ����Ա���Ե�¼�Ŀ�����Ϣ��
		/// </summary>
		/// <param name="operCode"></param>
		/// <returns></returns>
		public ArrayList GetMultiDept(string operCode) {
			string sql = "";
			if(this.GetSQL("Manager.DepartmentStatManager.GetMultiDept",ref sql)== -1)
				return null;

			try {
				sql=string.Format(sql, operCode);
			}
			catch(Exception ex) {
				this.ErrCode=ex.Message;
				this.Err=" "+ex.Message;
				this.WriteErr();
				return null;
			}

			//ִ��sql���
			if(this.ExecQuery(sql) == -1) return null;

			ArrayList list = new ArrayList();
			while(this.Reader.Read()) {
				FS.HISFC.Models.Base.DepartmentStat info = PrepareData();     
				if (info == null) {
					this.Reader.Close();
					return null;
				}
				list.Add(info);
			}
			this.Reader.Close();

			return list;
		}

        /// <summary>
        /// ���ݲ���Ա������ȡ����Ա���Ե�¼�Ŀ�����Ϣ��
        /// </summary>
        /// <param name="operCode"></param>
        /// <returns></returns>
        public ArrayList GetMultiDeptNew(string operCode)
        {
            string sql = "";
            if (this.GetSQL("Manager.DepartmentStatManager.GetMultiDeptNew", ref sql) == -1)
                return null;

            try
            {
                sql = string.Format(sql, operCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = " " + ex.Message;
                this.WriteErr();
                return null;
            }

            //ִ��sql���
            if (this.ExecQuery(sql) == -1) return null;

            ArrayList list = new ArrayList();
            while (this.Reader.Read())
            {
                FS.HISFC.Models.Base.DepartmentStat info = PrepareData();
                if (info == null)
                {
                    this.Reader.Close();
                    return null;
                }
                list.Add(info);
            }
            this.Reader.Close();

            return list;
        }

        /// <summary>
        /// ���ݲ���Ա������ȡ����Ա���Ե�¼�Ŀ�����Ϣ��
        /// </summary>
        /// <param name="operCode">����Ա����</param>
        /// <param name="class3Code">����Ȩ�ޱ���</param>
        /// <returns></returns>
        public ArrayList GetMultiDept(string operCode, string class3Code)
        {
            string sql = "";
            if (this.GetSQL("SOC.Manager.DepartmentStatManager.GetMultiDeptNew", ref sql) == -1)
                return null;

            try
            {
                sql = string.Format(sql, operCode, class3Code);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = " " + ex.Message;
                this.WriteErr();
                return null;
            }

            //ִ��sql���
            if (this.ExecQuery(sql) == -1) return null;

            ArrayList list = new ArrayList();
            while (this.Reader.Read())
            {
                FS.HISFC.Models.Base.DepartmentStat info = PrepareData();
                if (info == null)
                {
                    this.Reader.Close();
                    return null;
                }
                list.Add(info);
            }
            this.Reader.Close();

            return list;
        }

        /// <summary>
        /// ���ݲ���Ա������ȡ����Ա���Ե�¼�Ŀ�����Ϣ��{36DEFA19-3650-443f-A173-E2A355FA00C2}
        /// </summary>
        /// <param name="operCode"></param>
        /// <returns></returns>
        public ArrayList GetMultiDeptNewForNurser(string operCode)
        {
            string sql = "";
            if (this.GetSQL("Manager.DepartmentStatManager.GetMultiDeptNewForNuser", ref sql) == -1)
                return null;

            try
            {
                sql = string.Format(sql, operCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = " " + ex.Message;
                this.WriteErr();
                return null;
            }

            //ִ��sql���
            if (this.ExecQuery(sql) == -1) return null;

            ArrayList list = new ArrayList();
            while (this.Reader.Read())
            {
                FS.HISFC.Models.Base.DepartmentStat info = PrepareData();
                if (info == null)
                {
                    this.Reader.Close();
                    return null;
                }
                list.Add(info);
            }
            this.Reader.Close();

            return list;
        }
//		/// <summary>
//		/// 
//		/// </summary>
//		/// <param name="id"></param>
//		/// <returns></returns>
//		public FS.HISFC.Models.Base.DepartmentStat LoadByPrimaryKey(string id) {			
//			string sql = "";
//			if(this.GetSQL("Manager.DepartmentStatManager.LoadByPrimaryKey",ref sql)== -1)
//				return null;
//
//			try {
//				sql=string.Format(sql, id);
//			}
//			catch(Exception ex) {
//				this.ErrCode=ex.Message;
//				this.Err=" "+ex.Message;
//				this.WriteErr();
//				return null;
//			}
//
//			if(this.ExecQuery(sql) == -1) return null;
//
//			if(this.Reader.Read()) {
//				return PrepareData();				 			
//			}
//
//			return null;
//
//				
//		}


		/// <summary>
		/// ȡĳһͳ�ƴ����µĿ��ҷ����������
		/// </summary>
		/// <returns></returns>
		public string GetMaxCode(string statCode) {
			string sql = "";
			if(this.GetSQL("Manager.DepartmentStatManager.GetMaxCode",ref sql)== -1) {
				this.Err = "�Ҳ���SQL���Manager.DepartmentStatManager.GetMaxCode";
				return "-1";
			}

			try {
				sql=string.Format(sql,statCode);
			}
			catch(Exception ex) {
				this.ErrCode=ex.Message;
				this.Err=" "+ex.Message;
				this.WriteErr();
				return "-1";
			}
			return this.ExecSqlReturnOne(sql); 
		}


		/// <summary>
		/// �����ͳ�����в���һ����¼
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int InsertDepartmentStat(FS.HISFC.Models.Base.DepartmentStat info) {
			string strSql = "";
			
			if (this.GetSQL("Manager.DepartmentStatManager.InsertDepartmentStat",ref strSql)==-1) return -1;
			try {				
				strSql = string.Format( strSql, 
					info.StatCode,		//ͳ�ƴ���
					info.PardepCode,	//�������ұ���
					info.PardepName,	//������������
					info.DeptCode,		//���ұ���
					info.DeptName,		//��������
					info.SpellCode,		//ƴ����
					info.WBCode,		//�����
					info.NodeKind,		//�ڵ�����
					info.GradeCode,		//�ڵ�ȼ�
					info.SortId,		//����
					((int)info.ValidState).ToString(),	//�Ƿ���Ч
					FrameWork.Function.NConvert.ToInt32(info.ExtFlag),	//��չ���
					FrameWork.Function.NConvert.ToInt32(info.Ext1Flag), //��չ���
					info.Memo,			//��ע
					this.Operator.ID );	//������
			}
			catch(Exception ex) {
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return -1;
			}

			//ִ��SQL���
			int parm = this.ExecNoQuery(strSql);
			return parm;

			//�²���ڵ�ĸ��ڵ����ͱ�Ϊ���ҷ���
			//return this.UpdateNodeKind(info.StatCode, info.PardepCode, 0);
		}
		
				
		/// <summary>
		/// ���¿��ҷ�����е�һ����¼
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int UpdateDepartmentStat(FS.HISFC.Models.Base.DepartmentStat info) {			
			//ȡSQL���
			string strSql = "";
			if (this.GetSQL("Manager.DepartmentStatManager.UpdateDepartmentStat",ref strSql)==-1) return -1;
			
			//�滻����
			try {   				
				strSql = string.Format( strSql,
					info.PkID, 
					info.StatCode,		//ͳ�ƴ���
					info.PardepCode,	//�������ұ���
					info.PardepName,	//������������
					info.DeptCode,		//���ұ���
					info.DeptName,		//��������
					info.SpellCode,		//ƴ����
					info.WBCode,		//�����
					info.NodeKind,		//�ڵ�����
					info.GradeCode,		//�ڵ�ȼ�
					info.SortId,		//����
					((int)info.ValidState).ToString(),	//�Ƿ���Ч
					FrameWork.Function.NConvert.ToInt32(info.ExtFlag),	//��չ���
					FrameWork.Function.NConvert.ToInt32(info.Ext1Flag), //��չ���
					info.Memo,			//��ע
					this.Operator.ID );	//������
			}
			catch(Exception ex) {
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return -1;
			}      			

			try {
				return this.ExecNoQuery(strSql);
			}
			catch(Exception ex) {
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return -1;
			}
		}


        /// <summary>
        /// ���¿��ҽṹ�еĿ�����----����ά������ʱ���ã����ҽṹ��Ӧ�ô洢���������׵��¿�������ͬ����
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public int UpdateDeptName(FS.HISFC.Models.Base.Department dept)
        {
            string strSql = "";
            if (this.GetSQL("Manager.DepartmentStatManager.UpdateDeptName", ref strSql) == -1)
            {
                strSql = @"update com_deptstat t
                            set t.dept_name = '{1}',
                                t.spell_code = '{2}',
		                        t.wb_code = '{3}'
                            where t.dept_code = '{0}'";
            }

            strSql = string.Format(strSql, dept.ID, dept.Name, dept.SpellCode, dept.WBCode);
            return this.ExecNoQuery(strSql);
        }


		//		/// <summary>
		//		/// ���¿��ҷ�����е�һ����¼�Ľڵ�����
		//		/// </summary>
		//		/// <param name="info"></param>
		//		/// <returns></returns>
		//		public int UpdateNodeKind(string statCode, string deptCode, int nodeKind) {			
		//			string strSql = "";
		//			if (this.GetSQL("Manager.DepartmentStatManager.UpdateNodeKind",ref strSql)==-1) return -1;
		//			
		//			try {   				
		//				strSql = string.Format( strSql, statCode, deptCode, nodeKind, this.Operator.ID);
		//			}
		//			catch(Exception ex) {
		//				this.ErrCode=ex.Message;
		//				this.Err=ex.Message;
		//				return -1;
		//			}      			
		// 
		//			try {
		//				return this.ExecNoQuery(strSql);
		//			}
		//			catch(Exception ex) {
		//				this.ErrCode=ex.Message;
		//				this.Err=ex.Message;
		//				return -1;
		//			}
		//		}
		//		
		//
		//		/// <summary>
		//		/// ���¿��ҷ�����е�һ����¼�Ľڵ�����
		//		/// </summary>
		//		/// <param name="info">���ҽṹ��</param>
		//		/// <returns></returns>
		//		public int UpdateNodeKind(FS.HISFC.Models.Base.DepartmentStat info) {			
		//			string strSql = "";
		//			if (this.GetSQL("Manager.DepartmentStatManager.UpdateNodeKind",ref strSql)==-1) return -1;
		//			
		//			try {   				
		//				strSql = string.Format( strSql, info.StatCode, info.DeptCode, info.NodeKind, this.Operator.ID);
		//			}
		//			catch(Exception ex) {
		//				this.ErrCode=ex.Message;
		//				this.Err=ex.Message;
		//				return -1;
		//			}      			
		// 
		//			try {
		//				return this.ExecNoQuery(strSql);
		//			}
		//			catch(Exception ex) {
		//				this.ErrCode=ex.Message;
		//				this.Err=ex.Message;
		//				return -1;
		//			}
		//		}
		

		/// <summary>
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int Delete(DepartmentStat info) {
			//ɾ����ǰ�ڵ�
			return Delete(info.StatCode, info.DeptCode, info.PardepCode);

			//��ɾ���ڵ�ĸ��ڵ��ΪҶ�ӽڵ�
			//return this.UpdateNodeKind(info.StatCode, info.PardepCode, 1);
		}
		


		/// <summary>
		/// ɾ��ĳһ�����µĿ��ҽڵ�
		/// </summary>
		/// <param name="statCode">ͳ�ƴ���</param>
		/// <param name="deptCode">���ұ���</param>
        /// <param name="pardepCode">����/���� ����</param>
		/// <returns></returns>
        public int Delete(string statCode, string deptCode, string pardepCode)
        {
            //��Ҫ���ϸ����pardepCode���ܱ�֤׼ȷ��λ huangchw
			string strSql = "";
			if (this.GetSQL("Manager.DepartmentStatManager.DeleteDepartmentStat",ref strSql)==-1) return -1;
				
			try {   				
				strSql = string.Format(strSql, statCode, deptCode, pardepCode);
			}
			catch(Exception ex) {
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return -1;
			}      			

			try {
				return this.ExecNoQuery(strSql);
			}
			catch(Exception ex) {
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return -1;
			}
		}
		

		/// <summary>
		/// �����ݿ���������Ҳ����ɾ��еȼ��ṹ�Ŀ����б�
		/// ����һ���ڵ��б�
		/// </summary>
		/// <returns></returns>
		public ArrayList LoadLevelViewDepartemt(string statCode) {
			//ȡĳһ���ҷ����µĿ����б�����
			ArrayList deptstats = this.LoadDepartmentStat(statCode);
			ArrayList results = new ArrayList();

			if(deptstats.Count <= 0) 
				return results;

			Array state = Array.CreateInstance(typeof(System.Int32),deptstats.Count);
			int n = 0;

			foreach(FS.HISFC.Models.Base.DepartmentStat stat in deptstats) {
				if(stat.PardepCode == "AAAA") {
					results.Add(stat);
					state.SetValue(1,n);//[n] = 1;
					RecursionDept(stat,state,deptstats);
				}
				++n;
			}

			return results;
		}


		/// <summary>
		/// ȡ���ҷ������ݣ�������ʵ����
		/// </summary>
		/// <returns></returns>
		private FS.HISFC.Models.Base.DepartmentStat PrepareData() {
			FS.HISFC.Models.Base.DepartmentStat info = new FS.HISFC.Models.Base.DepartmentStat();

			try {
				info.PkID = this.Reader[0].ToString();
				info.StatCode = this.Reader[1].ToString();
				info.PardepCode = this.Reader[2].ToString();
				info.PardepName = this.Reader[3].ToString();
				info.DeptCode = this.Reader[4].ToString();
				info.DeptName = this.Reader[5].ToString();
				info.SpellCode = this.Reader[6].ToString();
				info.WBCode = this.Reader[7].ToString();
				info.NodeKind = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[8]);
                info.GradeCode = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[9]);
                info.SortId = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[10]);
                info.ValidState = (EnumValidState) FS.FrameWork.Function.NConvert.ToInt32(this.Reader[11]);
				info.ExtFlag = FrameWork.Function.NConvert.ToBoolean(this.Reader[12].ToString());
				info.Ext1Flag = FrameWork.Function.NConvert.ToBoolean(this.Reader[13].ToString());
				info.Memo = this.Reader[14].ToString();
			}
			catch(Exception e) {
				this.ErrCode = e.Message;
				this.Err = e.Message;
				return null;
			}

			return info;

				
		}
		
				
		/// <summary>
		/// 
		/// </summary>
		/// <param name="stat"></param>
		/// <param name="state"></param>
		/// <param name="deptStats"></param>
		private void RecursionDept(FS.HISFC.Models.Base.DepartmentStat stat,Array state, ArrayList deptStats) {
			//if(stat.NodeKind == 1) return;
			int index = 0;
			foreach(FS.HISFC.Models.Base.DepartmentStat info in deptStats) {
				if((int)state.GetValue(index) == 0) {
					if(stat.DeptCode == info.PardepCode&&stat.StatCode == info.StatCode) {
						stat.Childs.Add(info);
						state.SetValue(1,index);//state[index] = 1;
						RecursionDept(info,state,deptStats);
					}
				}
				++index;
			}
		}

		
		/// <summary>
		///  ����ĳ���ڵ��µ����� �����б�
		/// </summary>
		/// <param name="ParDeptCode"></param>
		/// <returns></returns>
		/// zhangjunyi@FS.com  
		public ArrayList GetdistrictForFinance(string ParDeptCode,string statCode) {
			ArrayList al = new ArrayList();
			try {
				string strSql="";
				if(ParDeptCode=="") {
					//ѡ�� �Ǻ��ƺ�¥ ����סԺ��
					if (this.GetSQL("Manager.DepartmentStatManager.Getdistrict",ref strSql)==-1) return null;
				}
				else {
					//ѡ�� ���ƺ�¥ ��סԺ�� �µ���ϸstrSql = string.Format(strSql, class1, deptCode);
					if (this.GetSQL("Manager.DepartmentStatManager.GetdistrictDetail",ref strSql)==-1) return null;
					strSql = string.Format(strSql,ParDeptCode,statCode);
				}
				if(this.ExecQuery(strSql)< 0) return null;
				FS.FrameWork.Models.NeuObject obj =null;
				while(this.Reader.Read()) {
					obj = new NeuObject();
					obj.ID = Reader[0].ToString();// ���ұ���
					obj.Name = Reader[1].ToString();// ��������
					obj.User01 = Reader[2].ToString();// �����ұ���
					obj.User02 =Reader[3].ToString();//����������
					al.Add(obj);
					obj= null;
				}
			}
			catch(Exception ee) {
				this.Err = ee.Message;
				al =null;
			}
			return al;
		}
		

		/// <summary>
		/// ���ؿ��Һ��� ��ȫԺ���µ�һ���ڵ�����в��ŵı��������  ���ڱ����ӡ�� �ṩ ���ƺ�¥��סԺ��
		/// </summary>
		/// <returns></returns>
		/// zhangjunyi@FS.com
		public ArrayList GetSubTreeNodeForFinance() {
			ArrayList al = new ArrayList();
			try {
				string strSql="";
				//ȡSQL���
				if (this.GetSQL("Manager.DepartmentStatManager.GetSubTreeNode",ref strSql)==-1) return null;
				if(this.ExecQuery(strSql)< 0) return null;
				FS.FrameWork.Models.NeuObject obj =null;
				while(this.Reader.Read()) {
					obj = new NeuObject();
					obj.ID = Reader[0].ToString();// ���ұ���
					obj.Name = Reader[1].ToString();// ��������
					obj.User01 = Reader[2].ToString();// �����ұ���
					obj.User02 =Reader[3].ToString();//����������
					al.Add(obj);
					obj= null;
				}
			}
			catch(Exception ee) {
				this.Err = ee.Message;
				al =null;
			}
			return al;
		}
		
		
		/// <summary>
		/// ȡ��ҽ��������ҽ������������ �µ����в����б� 
		/// </summary>
		/// <param name="str"> </param>
		/// <returns></returns>
		/// zhangjunyi@FS.com  ����������
		public string GetPactdStringList(string str) {
			string strSql="";
			try {
				switch(str) {
						//ȡ��ҽ�� �º�ͬ��λ����
					case "��ҽ��":   if (this.GetSQL("Manager.DepartmentStatManager.GetDeptIdStringList1",ref strSql)==-1) return null;
						break;
						//ȡ����ҽ�� �º�ͬ��λ����
					case "����ҽ��": if (this.GetSQL("Manager.DepartmentStatManager.GetDeptIdStringList2",ref strSql)==-1) return null; 
						break;
						//ȡ���������µĺ�ͬ��λ����
					case "��������": if (this.GetSQL("Manager.DepartmentStatManager.GetDeptIdStringList3",ref strSql)==-1) return null; 
						break; 
						//ȡ�� ����ҽ�� ��ҽ��   ���������� �����в����б�
					case "����": if (this.GetSQL("Manager.DepartmentStatManager.GetDeptIdStringList4",ref strSql)==-1) return null; 
						break; 
				}
			}
			catch(Exception ee) {
				this.Err = ee.Message;
				strSql="";
			}

			return strSql;
		}

        /// <summary>
        /// ���ݲ���Ա������ȡ����Ա���ڴ�����µ�С�����б�
        /// </summary>
        /// <param name="operCode"></param>
        /// <returns></returns>
        public ArrayList GetMultiSubDept(string operCode)
        {
            string sql = "";
            if (this.GetSQL("Manager.DepartmentStatManager.GetAllStationInDept", ref sql) == -1)
                return null;

            try
            {
                sql = string.Format(sql, operCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = " " + ex.Message;
                this.WriteErr();
                return null;
            }

            //ִ��sql���
            if (this.ExecQuery(sql) == -1) return null;

            ArrayList list = new ArrayList();
            while (this.Reader.Read())
            {
                FS.HISFC.Models.Base.DepartmentStat info = PrepareData();
                if (info == null)
                {
                    this.Reader.Close();
                    return null;
                }
                list.Add(info);
            }
            this.Reader.Close();

            return list;
        }
	}
	
}