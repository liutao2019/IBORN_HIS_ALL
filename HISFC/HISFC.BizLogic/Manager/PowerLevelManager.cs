using System;
using System.Collections;
using FS.FrameWork.Function;

using FS.FrameWork.Models;
using FS.HISFC.Models;
using FS.HISFC.Models.Admin;

namespace FS.HISFC.BizLogic.Manager {

	/// <summary>
	///	Ȩ�޵ȼ�������Եȼ�3���ȼ�3�а����˵ȼ�1��2����Ϣ��
	/// </summary>
	public class PowerLevelManager : DataBase {
		/// <summary>
		/// 
		/// </summary>
		public PowerLevelManager() {
			 
		}
			
			
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sqlName"></param>
		/// <param name="values"></param>
		/// <returns></returns>
		private string PrepareSQL(string sqlName,params string[] values) {
			string strSql = string.Empty;
			if (this.GetSQL(sqlName,ref  strSql) == 0 ) {
				try {
					if(values != null)
						strSql= string.Format(strSql,values);
				}
				catch(Exception ex) {
					this.Err=ex.Message;
					this.ErrCode=ex.Message;
					strSql = string.Empty;
				}
			}
			return strSql;
		}
			

		/// <summary>
		/// ����һ��Ȩ�ޱ��룬ȡ����2��3��Ȩ����Ϣ
		/// </summary>
		/// <returns></returns>
		public ArrayList LoadLevel3ByLevel1(string class1) {
			ArrayList levels = new ArrayList();

			//ȡsql���
			string sqlstring = PrepareSQL("Manager.PowerLevelManager.LoadLevel3ByLevel1", class1);
			 
			if(sqlstring == string.Empty)
				return levels;
                               	
			
			try {
				//ִ��sql��䣬ȡ����
				this.ExecQuery(sqlstring);	
				PowerLevelClass3 level3 = null;
				while(this.Reader.Read()) {
//					PowerLevelClass3 level3 = new PowerLevelClass3();
//					level3.Class2Code = this.Reader[0].ToString();
//					level3.Class3Code = this.Reader[1].ToString();
//					level3.Class3Name = this.Reader[2].ToString();
//					level3.Class3MeaningCode = this.Reader[3].ToString();
//					level3.Class3MeaningName = this.Reader[4].ToString();
//					level3.FinFlag = FrameWork.Function.NConvert.ToBoolean(this.Reader[5].ToString());
//					level3.DelFlag = FrameWork.Function.NConvert.ToBoolean(this.Reader[6].ToString());
//					level3.GrantFlag = FrameWork.Function.NConvert.ToBoolean(this.Reader[7].ToString());
//					level3.Class3JoinCode = this.Reader[8].ToString();
//					level3.JoinGroupCode = this.Reader[9].ToString();
//					level3.JoinGroupOrder = FrameWork.Function.NConvert.ToInt32(this.Reader[10].ToString());
//					level3.CheckFlag  = FrameWork.Function.NConvert.ToBoolean(this.Reader[11].ToString());
//					level3.Memo = this.Reader[12].ToString();
//					level3.PowerLevelClass2.Class2Name = this.Reader[13].ToString();
//					level3.PowerLevelClass2.Class1Code = this.Reader[14].ToString();
					level3 = PrepareLevel3Data();
					levels.Add(level3);   
				}		
				this.Reader.Close();		               
			}   
			catch(Exception ex) {
				this.ErrCode=ex.Message;
				this.Err=ex.Message;  	
				return null;
			}
			return levels;
		}
		
		
		/// <summary>
		/// ���ݶ���Ȩ�ޱ��룬ȡ��������Ȩ����Ϣ
		/// </summary>
		/// <returns></returns>
		public ArrayList LoadLevel3ByLevel2(string class2) {
			ArrayList levels = new ArrayList();

			//ȡsql���
			string sqlstring = PrepareSQL("Manager.PowerLevelManager.LoadLevel3ByLevel2", class2);
			 
			if(sqlstring == string.Empty)
				return levels;
                               	
			
			try {
				//ִ��sql��䣬ȡ����
				this.ExecQuery(sqlstring);	
				PowerLevelClass3 level3 = null;
				while(this.Reader.Read()) {
					level3 = PrepareLevel3Data();
					levels.Add(level3);   
				}				               
				this.Reader.Close();
			}   
			catch(Exception ex) {
				this.ErrCode=ex.Message;
				this.Err=ex.Message;  	
				return null;
			}
			return levels;
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="class2Code"></param>
		/// <param name="class3Code"></param>
		/// <returns></returns>
		public PowerLevelClass3 LoadLevel3ByPrimaryKey(string class2Code,string class3Code) {
			return LoadLevel3ByPrimaryKey(class2Code,class3Code,true);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		/// 

		public PowerLevelClass3 LoadLevel3ByPrimaryKey(string class2Code,string class3Code,bool lazy) {
			string sqlstring = PrepareSQL("Manager.PowerLevelManager.LoadLevel3ByPrimaryKey",new string[]{class2Code,class3Code});

			 
			if(sqlstring == string.Empty)
				return null;

			
                               			
			try {
				this.ExecQuery(sqlstring);
				PowerLevelClass3 level3 = new PowerLevelClass3();
				if(this.Reader.Read()) {
					level3.Class2Code = this.Reader[0].ToString();
					level3.Class3Code = this.Reader[1].ToString();
					level3.Class3Name = this.Reader[2].ToString();
					level3.Class3MeaningCode = this.Reader[3].ToString();
					level3.Class3MeaningName = this.Reader[4].ToString();
					level3.FinFlag = FrameWork.Function.NConvert.ToBoolean(this.Reader[5].ToString());
					level3.DelFlag = FrameWork.Function.NConvert.ToBoolean(this.Reader[6].ToString());
					level3.GrantFlag = FrameWork.Function.NConvert.ToBoolean(this.Reader[7].ToString());
					level3.Class3JoinCode = this.Reader[8].ToString();
					level3.JoinGroupCode = this.Reader[9].ToString();
					level3.JoinGroupOrder = FrameWork.Function.NConvert.ToInt32(this.Reader[10].ToString());
					level3.CheckFlag  = FrameWork.Function.NConvert.ToBoolean(this.Reader[11].ToString());
					level3.Memo = this.Reader[12].ToString();

					if(!lazy) {
						PowerLevel2Manager level2Mgr = new PowerLevel2Manager(); 
						level3.PowerLevelClass2 = level2Mgr.LoadLevel2ByPrimaryKey(level3.Class2Code,lazy);
					}
				}
				this.Reader.Close();
				return level3;
			}   
			catch(Exception ex) {
				this.ErrCode=ex.Message;
				this.Err=ex.Message;  
				return null; 
			}
		}
		

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private PowerLevelClass3 PrepareLevel3Data() {		
			PowerLevelClass3 level3 = new PowerLevelClass3();
			level3.Class2Code = this.Reader[0].ToString();
			level3.Class3Code = this.Reader[1].ToString();
			level3.Class3Name = this.Reader[2].ToString();
			level3.Class3MeaningCode = this.Reader[3].ToString();
			level3.Class3MeaningName = this.Reader[4].ToString();
			level3.FinFlag = FrameWork.Function.NConvert.ToBoolean(this.Reader[5].ToString());
			level3.DelFlag = FrameWork.Function.NConvert.ToBoolean(this.Reader[6].ToString());
			level3.GrantFlag = FrameWork.Function.NConvert.ToBoolean(this.Reader[7].ToString());
			level3.Class3JoinCode = this.Reader[8].ToString();
			level3.JoinGroupCode = this.Reader[9].ToString();
			level3.JoinGroupOrder = FrameWork.Function.NConvert.ToInt32(this.Reader[10].ToString());
			level3.CheckFlag  = FrameWork.Function.NConvert.ToBoolean(this.Reader[11].ToString());
			level3.Memo = this.Reader[12].ToString();
			level3.PowerLevelClass2.Class2Name = this.Reader[13].ToString();
			level3.PowerLevelClass2.Class1Code = this.Reader[14].ToString();

			return level3;

		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int InsertPowerLevelClass3(PowerLevelClass3 info) {
			string strSql = "";
			
			if (this.GetSQL("Manager.PowerLevelManager.InsertPowerLevelClass3",ref strSql)==-1) return -1;
			try {
				strSql = string.Format(strSql, 
					info.Class2Code, 
					info.Class3Code, 
					info.Class3Name, 
					info.Class3MeaningCode, 
					info.Class3MeaningName, 
					NConvert.ToInt32(info.FinFlag).ToString(), 
					NConvert.ToInt32(info.DelFlag).ToString(), 
					NConvert.ToInt32(info.GrantFlag).ToString(), 
					info.Class3JoinCode, 
					info.JoinGroupCode, 
					info.JoinGroupOrder, 
					NConvert.ToInt32(info.CheckFlag).ToString(), 
					info.Memo, 
					this.Operator.ID);
			}
			catch(Exception ex) {
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int UpdatePowerLevelClass3(PowerLevelClass3 info) {			
			string strSql = "";
			if (this.GetSQL("Manager.PowerLevelManager.UpdatePowerLevelClass3",ref strSql)==-1) return -1;
			
			try {   				
				strSql = string.Format(strSql, 
					info.Class2Code, 
					info.Class3Code, 
					info.Class3Name, 
					info.Class3MeaningCode, 
					info.Class3MeaningName, 
					NConvert.ToInt32(info.FinFlag).ToString(), 
					NConvert.ToInt32(info.DelFlag).ToString(), 
					NConvert.ToInt32(info.GrantFlag).ToString(), 
					info.Class3JoinCode, 
					info.JoinGroupCode, 
					info.JoinGroupOrder, 
					NConvert.ToInt32(info.CheckFlag).ToString(), 
					info.Memo, 
					this.Operator.ID);
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
		/// ɾ��һ������Ȩ��
		/// </summary>
		/// <param name="class2Code">����Ȩ�ޱ���</param>
		/// <param name="class3Code">����Ȩ�ޱ���</param>
		/// <returns></returns>
		public int Delete(string class2Code, string class3Code) {
			string strSql = "";
			if (this.GetSQL("Manager.PowerLevelManager.DeletePowerLevelClass3",ref strSql)==-1) return -1;
				
			try {   				
				strSql = string.Format(strSql, class2Code, class3Code);

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
		/// ���ݶ�������ɾ�����������Ӧ������Ȩ��
		/// </summary>
		/// <param name="class2Code">����Ȩ�ޱ���</param>
		/// <returns></returns>
		public int Delete(string class2Code) {
			return this.Delete(class2Code, "ALL");
		}


		/// <summary>
		/// ���ݶ���Ȩ�ޱ���ȡϵͳ����Ȩ�޺���
		/// </summary>
		/// <param name="class2Code"></param>
		/// <returns></returns>
		public ArrayList LoadLevel3Meaning(string class2Code) {
			string sqlstring = PrepareSQL("Manager.PowerLevelManager.LoadLevel3Meaning",class2Code);
			if(sqlstring == string.Empty)
				return null;

			ArrayList al = new ArrayList();
                               			
			try {
				this.ExecQuery(sqlstring);

				FS.FrameWork.Models.NeuObject obj = null;
				while(this.Reader.Read()) {
					obj = new NeuObject();
					obj.ID     = this.Reader[0].ToString();
					obj.Name   = this.Reader[1].ToString();
					obj.User01 = this.Reader[2].ToString();
					obj.User02 = this.Reader[3].ToString();
					obj.Memo   = this.Reader[4].ToString();
					al.Add(obj);
				}

				this.Reader.Close();
			}   
			catch(Exception ex) {
				this.ErrCode=ex.Message;
				this.Err=ex.Message;  	
				return null;
			}

			return al;

		}


		/// <summary>
		/// ����ϵͳȨ�ޱ���һ����¼
		/// </summary>
		/// <param name="NeuObject"></param>
		/// <returns></returns>
		public int InsertLevel3Meaning(FS.FrameWork.Models.NeuObject NeuObject) {
			string strSql = "";
			
			if (this.GetSQL("Manager.PowerLevelManager.InsertLevel3Meaning",ref strSql)==-1) return -1;
			try {
				strSql = string.Format(strSql,NeuObject.ID, NeuObject.Name, NeuObject.User01, NeuObject.User02, NeuObject.Memo);
			}
			catch(Exception ex) {
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		

		/// <summary>
		/// ɾ��ϵͳȨ�޼�¼
		/// </summary>
		/// <param name="class2Code">����Ȩ�ޱ����ϵͳȨ�ޱ���</param>
		/// <param name="class3MeaningCode">ϵͳȨ�ޱ���</param>
		/// <returns></returns>
		public int DeleteLevel3Meaning(string class2Code, string class3MeaningCode) {
			string strSql = "";
			if (this.GetSQL("Manager.PowerLevelManager.DeleteLevel3Meaning",ref strSql)==-1) {
				return -1;
			}
				
			try {   				
				strSql = string.Format(strSql,class2Code, class3MeaningCode);

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
		/// ɾ��ϵͳȨ�޼�¼
		/// </summary>
		/// <param name="class2Code">����Ȩ�ޱ���</param>
		/// <returns></returns>
		public int DeleteLevel3Meaning(string class2Code) {
			return this.DeleteLevel3Meaning(class2Code, "ALL");
		}
	}

	public class PowerLevel1Manager : DataBase {
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sqlName"></param>
		/// <param name="values"></param>
		/// <returns></returns>
		private string PrepareSQL(string sqlName,params string[] values) {
			string strSql = string.Empty;
			if (this.GetSQL(sqlName,ref  strSql) == 0 ) {
				try {
					if(values != null)
						strSql= string.Format(strSql,values);
				}
				catch(Exception ex) {
					this.Err=ex.Message;
					this.ErrCode=ex.Message;
					strSql = string.Empty;
				}
			}
			return strSql;
		}
			

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public ArrayList LoadLevel1All() {
			string sqlstring = PrepareSQL("Manager.PowerLevelManager.LoadLevel1All",null);

			ArrayList PowerLevelClass1s = new ArrayList();
			if(sqlstring == string.Empty)
				return PowerLevelClass1s ;

			try {
				this.ExecQuery(sqlstring);
				while(this.Reader.Read()) {
				
					PowerLevelClass1 info = PrepareLevel1Data();					 
					if(info  != null)
						PowerLevelClass1s.Add(info );
				}
				this.Reader.Close();
			}   
			catch(Exception ex) {
				this.ErrCode=ex.Message;
				this.Err=ex.Message; 	
				return null;
			}

			return PowerLevelClass1s;
		}	

		
		/// <summary>
		/// ���ݲ�����ȡ�û���ά���Ĵ����б�
		/// </summary>
		/// <param name="statCode">����</param>
		/// <returns></returns>
		public ArrayList LoadLevel1Available(string statCode) {
			string sqlstring = PrepareSQL("Manager.PowerLevelManager.LoadLevel1Available",statCode);

			ArrayList PowerLevelClass1s = new ArrayList();
			if(sqlstring == string.Empty)
				return PowerLevelClass1s ;

			try {
				this.ExecQuery(sqlstring);
				while(this.Reader.Read()) {
				
					PowerLevelClass1 info = PrepareLevel1Data();					 
					if(info  != null)
						PowerLevelClass1s.Add(info );
				}
				this.Reader.Close();
			}   
			catch(Exception ex) {
				this.ErrCode=ex.Message;
				this.Err=ex.Message; 	
				return null;
			}

			return PowerLevelClass1s;
		}	


		/// <summary>
		/// 
		/// </summary>
		/// <param name="id0"></param>
		/// <returns></returns>
		public PowerLevelClass1 LoadLevel1ByPrimaryKey(string id0) {
			string strSql = "";
			
			if (this.GetSQL("Manager.PowerLevelManager.LoadLevel1ByPrimaryKey",ref strSql)==-1) return null;
			try {
				strSql = string.Format(strSql, id0);
			}
			catch(Exception ex) {
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return null; 
			}
			try {
				this.ExecQuery(strSql);
				if(this.Reader.Read())
					return PrepareLevel1Data();		

				this.Reader.Close();

			}   
			catch(Exception ex) {
				this.ErrCode=ex.Message;
				this.Err=ex.Message; 	
				return null; 
			}		
			return new PowerLevelClass1();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private PowerLevelClass1 PrepareLevel1Data() {
			PowerLevelClass1 info = new PowerLevelClass1();
			info.Class1Code = this.Reader[0].ToString();     //һ��Ȩ�޷����룬Ȩ�����ͣ���Ӧ�ڱ�COM_DEPTSTAT.STAT_CODE
			info.Class1Name = this.Reader[1].ToString();     //һ��Ȩ�޷�������
			info.UniteFlag = FrameWork.Function.NConvert.ToBoolean(this.Reader[2].ToString()); //�Ƿ�����ͳһά��0��������1������
			info.TypeProperty = FrameWork.Function.NConvert.ToInt32(this.Reader[3]); //�������ԣ�0�������ӷ��ֻ࣬�����¼������Զ�����ң�1�����ҷ��������Աֻ�������ռ����ң���2�����ڿ��ҷ�������������Ա��3ֻ��ά�����ҹ�ϵ��������������Ա��4������ӿ��Һ���Ա
			info.UniteCode = this.Reader[4].ToString();      //ͳһά���룺��ͬ�ı���ͳһά����һ��
			info.VocationType = this.Reader[5].ToString();   //����ҵ����
			info.VocationName = this.Reader[6].ToString();   //����ҵ��������
			info.Memo = this.Reader[7].ToString();			 //��ע
            info.ValidState = NConvert.ToBoolean(this.Reader[8]) ;     //��Ч״̬(1��Ч��0��Ч)
			info.ID = info.Class1Code;
			info.Name = info.Class1Name;
				
			return info;
		}

	}

	public class PowerLevel2Manager : DataBase {
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sqlName"></param>
		/// <param name="values"></param>
		/// <returns></returns>
		private string PrepareSQL(string sqlName,params string[] values) {
			string strSql = string.Empty;
			if (this.GetSQL(sqlName,ref  strSql) == -1 )  return null;
			try {
				if(values != null)
					strSql= string.Format(strSql,values);
			}
			catch(Exception ex) {
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				strSql = string.Empty;
			}
			return strSql;
		}
			

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public ArrayList LoadLevel2All(string class1) {
			string sqlstring = PrepareSQL("Manager.PowerLevelManager.LoadLevel2All",class1);
			if (sqlstring == null) return null;
			
			ArrayList PowerLevelClass2s = new ArrayList();
			try {
				this.ExecQuery(sqlstring);
				while(this.Reader.Read()) {
				
					PowerLevelClass2 info = PrepareLevel2Data();					 
					if(info  != null)
						PowerLevelClass2s.Add(info );
				}
				this.Reader.Close();

			}   
			catch(Exception ex) {
				this.ErrCode=ex.Message;
				this.Err=ex.Message; 	
				return null;
			}

			return PowerLevelClass2s;
		}	


		/// <summary>
		/// 
		/// </summary>
		/// <param name="id0"></param>
		/// <returns></returns>
		public PowerLevelClass2 LoadLevel2ByPrimaryKey(string id0) {
			return LoadLevel2ByPrimaryKey(id0,true);
		
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="id0"></param>
		/// <param name="lazy"></param>
		/// <returns></returns>
		public PowerLevelClass2 LoadLevel2ByPrimaryKey(string id0,bool lazy) {
			string strSql = "";
			
			if (this.GetSQL("Manager.PowerLevelManager.LoadLevel2ByPrimaryKey",ref strSql)==-1) return null;
			try {
				strSql = string.Format(strSql, id0);
			}
			catch(Exception ex) {
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return null; 
			}
			PowerLevelClass2 level2 = PrepareLevel2Data();		
			try {
				this.ExecQuery(strSql);
				if(this.Reader.Read()) {
					if(!lazy) {
						PowerLevel1Manager level1Mgr = new PowerLevel1Manager();
						level2.PowerLevelClass1 = level1Mgr.LoadLevel1ByPrimaryKey(level2.Class1Code);
					}
				}
				this.Reader.Close();
			}   
			catch(Exception ex) {
				this.ErrCode=ex.Message;
				this.Err=ex.Message; 	
				return null; 
			}			 
			return level2;
		}

		
		/// <summary>
		/// �������Ȩ�ޱ���һ����¼
		/// </summary>
		/// <param name="class2"></param>
		/// <returns></returns>
		public int InsertLevel2(FS.HISFC.Models.Admin.PowerLevelClass2 class2) {
			string strSql = "";
			
			if (this.GetSQL("Manager.PowerLevelManager.InsertPowesrLevelClass2",ref strSql)==-1) return -1;
			try {
				strSql = string.Format(strSql,
					class2.Class1Code,   //һ��Ȩ�ޱ���
					class2.Class2Code,   //����Ȩ�ޱ���
					class2.Class2Name,   //����Ȩ������
					class2.Memo,         //��ע
					this.Operator.ID,    //�����˱���
					class2.Flag);		 //�����ǣ�1�жϴ���Ȩ��ʱ��ֻҪ����Ȩ�޾�������룬����Ҫ�û�ѡ�����
			}
			catch(Exception ex) {
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		

		/// <summary>
		/// ɾ������Ȩ�޼�¼
		/// </summary>
		/// <param name="class1Code">һ��Ȩ�ޱ���</param>
		/// <param name="class2Code">����Ȩ�ޱ���</param>
		/// <returns></returns>
		public int DeleteLevel2(string class1Code, string class2Code) {
			string strSql = "";
			if (this.GetSQL("Manager.PowerLevelManager.DeleteLevel2",ref strSql)==-1) {
				return -1;
			}
				
			try {   				
				strSql = string.Format(strSql,class1Code, class2Code);

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
		/// ɾ������Ȩ�޼�¼
		/// </summary>
		/// <param name="class1Code">һ��Ȩ�ޱ���</param>
		/// <returns></returns>
		public int DeleteLevel2(string class1Code) {
			return DeleteLevel2(class1Code, "ALL");
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private PowerLevelClass2 PrepareLevel2Data() {
			PowerLevelClass2 info = new PowerLevelClass2();
		 
			info.Class1Code = this.Reader[0].ToString();
			info.Class2Code = this.Reader[1].ToString();
			info.Class2Name = this.Reader[2].ToString();
			info.Memo       = this.Reader[3].ToString();
			info.ValidState = NConvert.ToBoolean(this.Reader[4]);
			info.Flag		= this.Reader[5].ToString();

			info.ID = info.Class2Code;
			info.Name = info.Class2Name ;
				
			return info;
		}

	}
}