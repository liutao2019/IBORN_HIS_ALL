using System;

using System.Collections;
using FS.HISFC.Models;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.BizLogic.Manager
{
	/// <summary>
	/// 常数管理类
	/// 接口都为 Get,Set,Del GetList
	/// </summary>
	public class Constant:FS.FrameWork.Management.Database 
	{
		public Constant()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

        #region 常数类型
//		/// <summary>
//		/// 常数表
//		/// </summary>
//		/// //TYPE      VARCHAR2(20)                  常数类型 
//
//		//CODE      VARCHAR2(20)                  编码     
//		//NAME      VARCHAR2(50) Y                名称     
//		//MEMO      VARCHAR2(50) Y                备注     
//		//SPELLCODE VARCHAR2(20) Y                拼音码   
//		//SORTID    NUMBER       Y                顺序号 
//		//OPER_CODE VARCHAR2(20) Y                操作员  
// 
//		//OPER_TIME DATE         Y                操作时间 
	
		#endregion
		
        #region "常数表"
        /// <summary>
        /// 根据类型获得常数所有在用的列(到大科)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ArrayList GetListbyDept(string type, string dept_code)
        {
            string strSql = "";

            ArrayList al = new ArrayList();
            //接口说明
            //返回
            if (this.Sql.GetSql("Speciment.BizProcess.Manager.ConstantbyDept", ref strSql) == -1)
                return null;

            try
            {
                strSql = string.Format(strSql, type, dept_code);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "接口错误！" + ex.Message;
                this.WriteErr();
                return null;
            }
            if (this.ExecQuery(strSql) == -1)
                return null;
            FS.HISFC.Models.Base.Const cons;
            while (this.Reader.Read())
            {
                cons = new Const();
                cons.ID = this.Reader[1].ToString();
                cons.Name = this.Reader[2].ToString();
                cons.Memo = this.Reader[3].ToString();
                cons.SpellCode = this.Reader[4].ToString();
                cons.WBCode = this.Reader[5].ToString();
                cons.UserCode = this.Reader[6].ToString();
                if (!Reader.IsDBNull(7))
                    cons.SortID = Convert.ToInt32(this.Reader[7]);
                cons.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[8].ToString());
                cons.OperEnvironment.ID = this.Reader[9].ToString();
                if (!Reader.IsDBNull(10))
                    cons.OperEnvironment.OperTime = Convert.ToDateTime(this.Reader[10].ToString());


                al.Add(cons);
            }
            this.Reader.Close();
            return al;
        }

		/// <summary>
		/// 根据类型获得常数所有在用的列
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public ArrayList GetList(string type) {	
			string strSql="";

			ArrayList al=new ArrayList();
			//接口说明
			//返回
			if(this.Sql.Sql.GetSql("Manager.Constant.2",ref strSql)==-1) return null;

			try {
				strSql=string.Format(strSql,type);
			}
			catch(Exception ex) {
				this.ErrCode=ex.Message;
				this.Err="接口错误！"+ex.Message;
				this.WriteErr();
				return null;
			}
			if(this.ExecQuery(strSql)==-1) return null;
			//FS.FrameWork.Models.NeuObject NeuObject;
			FS.HISFC.Models.Base.Const cons ;
			while(this.Reader.Read()) {
				//NeuObject=new NeuObject();
				cons = new Const();
				//cons.Type = (Const.enuConstant)(Reader[0].ToString());
				cons.ID=this.Reader[1].ToString();
				cons.Name=this.Reader[2].ToString();
				cons.Memo=this.Reader[3].ToString();
				cons.SpellCode=this.Reader[4].ToString();
				cons.WBCode = this.Reader[5].ToString();
				cons.UserCode = this.Reader[6].ToString();
				if(!Reader.IsDBNull(7)) 
					cons.SortID=Convert.ToInt32(this.Reader[7]);
				cons.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[8].ToString());
				cons.OperEnvironment.ID=this.Reader[9].ToString();
				if(!Reader.IsDBNull(10))
					cons.OperEnvironment.OperTime = Convert.ToDateTime(this.Reader[10].ToString());
				
				
				al.Add(cons);
			}
			this.Reader.Close();
			return al;
		}

        /// <summary>
        /// 根据类型获得sql所特定常数所有在用的列
        /// {1A1ECA02-C14B-4c25-9962-7797FDE2F7E2}
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ArrayList GetListAsSql(string strSql, string type)
        {

            ArrayList al = new ArrayList();
            //接口说明
            //返回
            if (this.ExecQuery(strSql) == -1) return null;
            //FS.FrameWork.Models.NeuObject NeuObject;
            FS.HISFC.Models.Base.Const cons;
            while (this.Reader.Read())
            {
                //NeuObject=new NeuObject();
                cons = new Const();
                //cons.Type = (Const.enuConstant)(Reader[0].ToString());
                cons.ID = this.Reader[1].ToString();
                cons.Name = this.Reader[2].ToString();
                cons.Memo = this.Reader[3].ToString();
                cons.SpellCode = this.Reader[4].ToString();
                cons.WBCode = this.Reader[5].ToString();
                cons.UserCode = this.Reader[6].ToString();
                if (!Reader.IsDBNull(7))
                    cons.SortID = Convert.ToInt32(this.Reader[7]);
                cons.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[8].ToString());
                cons.OperEnvironment.ID = this.Reader[9].ToString();
                if (!Reader.IsDBNull(10))
                    cons.OperEnvironment.OperTime = Convert.ToDateTime(this.Reader[10].ToString());

                al.Add(cons);
            }
            this.Reader.Close();
            return al;
        }

        /// <summary>
        /// 获取所有有效的账户类型
        /// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ArrayList GetAccountTypeList()
        {
            return this.GetAccountTypeList("ALL");
        }

        /// <summary>
        /// 根据类型获取账户类型
        /// {ECECDF2F-BA74-4615-A240-C442BE0A0074}
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ArrayList GetAccountTypeList(string type)
        {
            string strSql = "";

            ArrayList al = new ArrayList();
            //接口说明
            //返回           

            try
            {
                if (this.Sql.Sql.GetSql("Manager.Constant.4", ref strSql) == -1) return null;
                strSql = string.Format(strSql, type);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "接口错误！" + ex.Message;
                this.WriteErr();
                return null;
            }
            if (this.ExecQuery(strSql) == -1) return null;
            FS.HISFC.Models.Base.Const accountType;
            while (this.Reader.Read())
            {
                accountType = new Const();
                accountType.ID = this.Reader[0].ToString();
                accountType.Name = this.Reader[1].ToString();
                accountType.Memo = this.Reader[2].ToString();//备注
                accountType.SpellCode = this.Reader[3].ToString();
                accountType.WBCode = this.Reader[4].ToString();
                accountType.User01 = this.Reader[5].ToString();//执行科室
                accountType.User02 = this.Reader[6].ToString();//开始时间
                accountType.User03 = this.Reader[7].ToString();//截止时间
                accountType.UserCode = this.Reader[8].ToString();//创建人
                accountType.OperEnvironment.ID = this.Reader[10].ToString();
                accountType.OperEnvironment.OperTime = Convert.ToDateTime(this.Reader[11].ToString());
                al.Add(accountType);
            }
            this.Reader.Close();
            return al;
        }

		/// <summary>
		/// 获得常数所有在用的列
		/// </summary>
		/// <param name="constType"></param>
		/// <returns></returns>
        public ArrayList GetList(EnumConstant constType)
		{	
			string type = constType.ToString();
			return this.GetList(type);
		}
		/// <summary>
		/// 获得常数所有列
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public ArrayList GetAllList(string type) {	
			string strSql="";

			ArrayList al= new ArrayList();
			//接口说明
			//返回
			if(this.Sql.GetSql("Manager.Constant.1",ref strSql)==-1) return null;

			try {
				strSql=string.Format(strSql,type);
			}
			catch(Exception ex) {
				this.ErrCode=ex.Message;
				this.Err="接口错误！"+ex.Message;
				this.WriteErr();
				return null;
			}

		
			if(this.ExecQuery(strSql)==-1) return null;
			FS.HISFC.Models.Base.Const cons ;
			while(this.Reader.Read()) {
				cons = new Const();
				
				cons.ID=this.Reader[1].ToString();
				cons.Name=this.Reader[2].ToString();
				cons.Memo=this.Reader[3].ToString();
				cons.SpellCode=this.Reader[4].ToString();
				cons.WBCode = this.Reader[5].ToString();
				cons.UserCode = this.Reader[6].ToString();
				if(!Reader.IsDBNull(7)) 
					cons.SortID=Convert.ToInt32(this.Reader[7]);
				cons.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[8].ToString());
				cons.OperEnvironment.ID=this.Reader[9].ToString();
				if(!Reader.IsDBNull(10))
					cons.OperEnvironment.OperTime = Convert.ToDateTime(this.Reader[10].ToString());				
				al.Add(cons);
			}
			this.Reader.Close();
			return al;
		}
 
		/// <summary>
		/// 获得常数所有列
		/// </summary>
		/// <param name="constType"></param>
		/// <returns></returns>
        public ArrayList GetAllList(EnumConstant constType)
		{	
			string type =   constType.ToString();
			return this.GetAllList(type);
		}
 
		/// <summary>
		/// 插入常数表中一条记录
		/// </summary>
		/// <param name="type"></param>
		/// <param name="cons"></param>
		/// <returns></returns>
		public int InsertItem(string type, Const cons) {
			string strSQL = "";

			if(this.Sql.GetSql("Manager.Constant.ItemType.modify.2",ref strSQL)==-1) {
				return -1;
			}

			try {
				strSQL=string.Format(strSQL,
					type,cons.ID,cons.Name,cons.Memo,cons.SpellCode,cons.WBCode,cons.UserCode, cons.SortID.ToString(),FS.FrameWork.Function.NConvert.ToInt32(cons.IsValid),this.Operator.ID);
			}
			catch(Exception ex) {
				this.ErrCode=ex.Message;
				this.Err="接口错误！"+ex.Message;
				this.WriteErr();
				return -1;
			}
			
			return this.ExecNoQuery(strSQL);

		}

		/// <summary>
		/// 更新常数表中的一条记录
		/// </summary>
		/// <param name="type"></param>
		/// <param name="cons"></param>
		/// <returns></returns>
        public int UpdateItem(string type, Const cons)
        {
            string strSQL = "";

            if (this.Sql.GetSql("Manager.Constant.ItemType.modify.1", ref strSQL) == -1)
            {
                return -1;
            }
            //更新信息不成功再插入新信息
            //传入参数 
            //0 type 常数类型,1 Name （查询条件）,2 ID,3 Memo,4 user01,5 user02,6 user03,7 operator
            try
            {
                strSQL = string.Format(strSQL,
                    type, cons.ID, cons.Name, cons.Memo, cons.SpellCode, cons.WBCode, cons.UserCode, cons.SortID, FS.FrameWork.Function.NConvert.ToInt32(cons.IsValid), this.Operator.ID);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "接口错误！" + ex.Message;
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

		/// <summary>
		/// 设置/更新常数列
		/// </summary>
		/// <param name="type"></param>
		/// <param name="cons"></param>
		/// <returns></returns>
        public int SetConstant(string type, Const cons)
        {
            int i;
            i = this.UpdateItem(type, cons);

            if (i == 0)
            {
                i = this.InsertItem(type, cons);
            }
            return i;
        }
		
		/// <summary>
		/// 删除常数列 
		/// </summary>
		/// <param name="type"></param>
		/// <param name="ID"></param>
		/// <returns></returns>
		public int DelConstant(string type,string ID) {
			string strSql="";
			//接口说明
			//返回
			//0 成功,-1失败
			//

			if(this.Sql.GetSql("Manager.Constant.ItemType.del.1",ref strSql)==-1) {
				return -1;
			}
			//更新信息不成功再插入新信息
			//传入参数 
			//0 type 常数类型,1  ID
			try {
				strSql=string.Format(strSql,type,ID);
			}
			catch(Exception ex) {
				this.ErrCode=ex.Message;
				this.Err="接口错误！"+ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
        /// <summary>
        /// 删除常数列 {3F1D29EA-0A9D-4703-938E-AB3E51257672}
        /// </summary>
        /// <param name="type"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int DelConstant(string type)
        {
            string strSql = "";
            //接口说明
            //返回
            //0 成功,-1失败
            //

            if (this.Sql.GetSql("Manager.Constant.ItemType.del.2", ref strSql) == -1)
            {
                return -1;
            }
            //更新信息不成功再插入新信息
            //传入参数 
            //0 type 常数类型,1  ID
            try
            {
                strSql = string.Format(strSql, type);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "接口错误！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
		/// <summary>
		/// 获得常数列的一个实体
		/// </summary>
		/// <param name="type"></param>
		/// <param name="ID"></param>
		/// <returns></returns>
        public NeuObject GetConstant(string type, string ID)
        {
			string strSql="";
			//接口说明
			//返回
			//0 ID
			
			if(this.Sql.GetSql("Manager.Constant.ItemType.getone.1",ref strSql)==-1) return null;
			//更新信息不成功再插入新信息
			//传入参数 
			//0 type 常数类型,1 ID
			try {
				strSql=string.Format(strSql,
					type,ID);
			}
			catch(Exception ex) {
				this.ErrCode=ex.Message;
				this.Err="接口错误！"+ex.Message;
				this.WriteErr();
				return null;
			}
			
			if(this.ExecQuery(strSql)==-1) return null;//没有reader.Read()
			try {
				Const cons = new Const();
				if (!this.Reader.Read()) return cons;
				//cons.Type = (Const.enuConstant)Convert.ToInt32(Reader[0].ToString());

				cons.ID=this.Reader[1].ToString();
				cons.Name=this.Reader[2].ToString();
				cons.Memo=this.Reader[3].ToString();
				cons.SpellCode=this.Reader[4].ToString();
				cons.WBCode = this.Reader[5].ToString();
				cons.UserCode = this.Reader[6].ToString();
				if(!Reader.IsDBNull(7))
					cons.SortID=Convert.ToInt32(this.Reader[7].ToString());
				cons.IsValid =FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[8].ToString());
				cons.OperEnvironment.ID=this.Reader[9].ToString();
				if(!Reader.IsDBNull(10))
					cons.OperEnvironment.OperTime = Convert.ToDateTime(this.Reader[10].ToString());
			
				this.Reader.Close();
				return cons;
			}
			catch 
			{
				if(!Reader.IsClosed)
				{
					Reader.Close();
				}
				return null;
			}
		}

		/// <summary>
		/// 获得常数列的一个实体
		/// </summary>
		/// <param name="type"></param>
		/// <param name="ID"></param>
		/// <returns></returns>
        public NeuObject GetConstant(EnumConstant type, string ID)
		{
            return this.GetConstant(type.ToString(), ID);
		}

        /// <summary>
        /// 根据类型和Name列进行查询
        /// </summary>
        /// <param name="type">Type列</param>
        /// <param name="name">Name列</param>
        /// <returns></returns>
        public ArrayList GetListByTypeAndName(string type, string name)
        {
            string strSql = "";
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Base.Const cons;

            if (this.Sql.GetSql("Manager.Constant.3", ref strSql) == -1)
            {
                return null;
            }
            try
            {
                strSql = string.Format(strSql, type, name);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "接口错误！" + ex.Message;
                this.WriteErr();
                return null;
            }
            if (this.ExecQuery(strSql) == -1) return null;
            while (this.Reader.Read())
            {
                cons = new Const();
                cons.ID = this.Reader[1].ToString();
                cons.Name = this.Reader[2].ToString();
                cons.Memo = this.Reader[3].ToString();
                cons.SpellCode = this.Reader[4].ToString();
                cons.WBCode = this.Reader[5].ToString();
                cons.UserCode = this.Reader[6].ToString();
                if (!Reader.IsDBNull(7))
                    cons.SortID = Convert.ToInt32(this.Reader[7]);
                cons.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[8].ToString());
                cons.OperEnvironment.ID = this.Reader[9].ToString();
                if (!Reader.IsDBNull(10))
                    cons.OperEnvironment.OperTime = Convert.ToDateTime(this.Reader[10].ToString());

                al.Add(cons);
            }
            this.Reader.Close();
            return al;
        }

        /// <summary>
        /// 获取某个类型的最大名称（收费发票用）
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetMaxName(string type)
        {
            string strSql = "";
            if (this.Sql.GetSql("Manager.Constant.ItemType.GetMaxName", ref strSql) == -1)
            {
                strSql = "select max(to_number(name)) from com_dictionary where type='{0}'";
            }

            strSql = string.Format(strSql, type);
            return this.ExecSqlReturnOne(strSql, "00");
        }

        /// <summary>
        /// 判断名称是否已经存在
        /// </summary>
        /// <param name="type"></param>
        /// <param name="ID"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistName(string type, string ID, string name)
        {
            string strSql = "";
            if (this.Sql.GetSql("Manager.Constant.ItemType.IsExistName", ref strSql) == -1)
            {
                strSql = "select 1 from com_dictionary where type='{0}' and code<>'{1}' and name='{2}'";
            }

            strSql = string.Format(strSql, type,ID,name);
            if (this.ExecQuery(strSql) < 0)//存在报错
            {
                return true;
            }
            if (this.Reader.Read())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
		#endregion
		
        #region 常数限制表
		/// <summary>
		/// 插入常数限制信息
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int InsertConstriantDictionary(ConstriantDictionary info)
		{
			string strSql = "";
			
			if (this.Sql.GetSql("Manager.ConstriantDictionaryManagerImpl.InsertConstriantDictionary",ref strSql)==-1) return -1;
			try
			{
				strSql = string.Format(strSql,info.Type, info.Id, info.SqlType, info.ConstraintSql, 
									          info.OperEnvironment.ID);
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		/// <summary>
		/// 更新常数限制信息
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int UpdateConstriantDictionary(ConstriantDictionary info)
		{			
			string strSql = "";
			if (this.Sql.GetSql("Manager.ConstriantDictionaryManagerImpl.UpdateConstriantDictionary",ref strSql)==-1) return -1;
			
			try
			{   				
				strSql = string.Format(strSql,info.Type, info.Id, info.SqlType, info.ConstraintSql, 
					                          info.OperEnvironment.ID);

			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return -1;
			}      			

			try
			{
				return this.ExecNoQuery(strSql);
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return -1;
			}
		}
		/// <summary>
		/// 先更新再插入
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int UpdateAndInsert(ConstriantDictionary info)
		{
			int temp = 0;
			
			temp = this.UpdateConstriantDictionary(info);

			if(temp == 0)
				return this.InsertConstriantDictionary(info);
			else if(temp > 0)
				return 0;
			else
				return -1;
		}
		/// <summary>
		/// 删除限制常数信息
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int Delete(ConstriantDictionary info)
		{
			string strSql = "";
			if (this.Sql.GetSql("Manager.ConstriantDictionaryManagerImpl.DeleteConstriantDictionary",ref strSql)==-1) return -1;
				
			try
			{   				
				strSql = string.Format(strSql,info.Type,info.Id);

			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return -1;
			}      			

			try
			{
				return this.ExecNoQuery(strSql);
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return -1;
			}
		}
		/// <summary>
		/// 取单条记录
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int SelectOneConstriantDictionary(ConstriantDictionary info)
		{
			string strSQL = "";
			if(this.Sql.GetSql("Manager.ConstriantDictionaryManagerImpl.OneConstriantDictionary", ref strSQL) == -1)
				return -1;
			
			try
			{
				strSQL = string.Format(strSQL, info.Type, info.ID);
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return -1;
			}      			

			try
			{
				return this.ExecNoQuery(strSQL);
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return -1;
			}
			
		}
		/// <summary>
		/// 获得类别下所有信息
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public ArrayList SelectConstriantDictionary(String type)
		{
			string strSql = "";
			ArrayList al = new ArrayList();
			ConstriantDictionary obj;
			if (this.Sql.GetSql("Manager.ConstriantDictionaryManagerImpl.SelectConstriantDictionary",ref strSql)==-1) return null;
			try
			{
				strSql = string.Format(strSql,type); 
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return null;
			}
			
			if(this.ExecQuery(strSql) == -1)
				return null;

			while(this.Reader.Read())
			{
				obj = new ConstriantDictionary();
				
				obj.Type = Reader[0].ToString();
				obj.Id = Reader[1].ToString();
				obj.SqlType = Reader[2].ToString();
				obj.ConstraintSql = Reader[3].ToString();
				obj.OperEnvironment.ID = Reader[4].ToString();
				if(!Reader.IsDBNull(5))
					obj.OperEnvironment.OperTime = Convert.ToDateTime(Reader[5].ToString());
				
				al.Add(obj);
			}
			this.Reader.Close();
			return al;
		}
		/// <summary>
		/// 查询常数是否可以删除修改
		/// </summary>
		/// <param name="idString"></param>
		/// <param name="code"></param>
		/// <returns></returns>
		public int CanDeleteCons(string idString, string code)
		{
			string strSQL = "";
			string judgeSql = "";
			string flag = "";
			int count = 0;
			if(this.Sql.GetSql("Manager.ConstriantDictionaryManagerImpl.CONSTRAINT", ref strSQL) == -1)
				return -1;
			
			try
			{
				strSQL = string.Format(strSQL, idString);
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				WriteErr();
				return -1;
			}      			
			
			if(this.ExecQuery(strSQL) == -1)
			{
				this.Reader.Close();
				return -1;
			}

			while(this.Reader.Read())
			{
				flag = Reader[0].ToString();
				judgeSql = Reader[1].ToString();
			}
//			this.Reader.Close();

			if(flag == "")
			{
				this.Reader.Close();
				return -1;
			}

			switch(flag.Trim())
			{
				case "1"://可删除修改
					if(judgeSql == "")
					{
						return -1;
					}
					else
					{
						try
						{
							this.ExecQuery(judgeSql + "'" +code+ "'");
						}
						catch
						{
							this.Reader.Close();
							return 0;
						}
						while(this.Reader.Read())
						{
							count = Convert.ToInt32(Reader[0].ToString());
						}

						this.Reader.Close();

						if(count == 0)
							return -1;
						else
							return 0;
					}
				case "2"://不可删除修改
					return 1;
				case "3"://不可删除可修改
					return 2;
			}
			return -1;
	
		}


		#endregion
		
		#region 报表 
		public int UpdateComHospitalinfoDate(string BeginDate,string EndDate)
		{
			string sql="";
			if(this.Sql.GetSql("Manager.Constant.UpdateComHospitalinfoDate",ref sql)==-1) return -1;
			sql = string.Format(sql,BeginDate,EndDate);
			return this.ExecNoQuery(sql);
		}
		public int UpdateStringValue(string stringValud)
		{
			string sql="";
			if(this.Sql.GetSql("Manager.Constant.UpdateStringValue",ref sql)==-1) return -1;
			sql = string.Format(sql,stringValud);
			return this.ExecNoQuery(sql);
		}
		/// <summary>
		/// 获取医院的名称
		/// </summary>
		/// <returns></returns>
		public string GetHospitalName()
		{
			string sql="";
			if(this.Sql.GetSql("Manager.Constant.GetHospitalName",ref sql)==-1) return "";
			//			sql = string.Format(sql,BeginDate,EndDate);
			return this.ExecSqlReturnOne(sql);
		}
		#endregion 
	}
	
}
