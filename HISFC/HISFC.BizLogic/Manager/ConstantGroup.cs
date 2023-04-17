using System;
using System.Collections;
using FS.FrameWork.Function;
namespace FS.HISFC.BizLogic.Manager {
	/// <summary>
	/// SysGroup ��ժҪ˵����
	/// ϵͳ��
	/// </summary>
	public class ConstantGroup: DataBase,FS.HISFC.Models.Base.IManagement {
		public ConstantGroup() {
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		#region IManagement ��Ա

		/// <summary>
		/// ȡȫ������
		/// </summary>
		/// <returns></returns>
		public System.Collections.ArrayList GetList() {
			// TODO:  ��� ConstantGroup.GetList ʵ��
			string sql="";
			if(this.GetSQL("Manager.ConstantGroup.Select",ref sql)==-1) return null;
			return myList(sql);
		}


		/// <summary>
		/// ȡĳһ�������п���ά���ĳ���
		/// </summary>
		/// <returns></returns>
		public System.Collections.ArrayList GetList(string constType) {
			// TODO:  ��� ConstantGroup.GetList ʵ��
			string sql="";
			if(this.GetSQL("Manager.ConstantGroup.GetList",ref sql)==-1) return null;
			return myList(sql);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public FS.FrameWork.Models.NeuObject Get(object obj) {
			// TODO:  ��� ConstantGroup.Get ʵ��
			string constType = obj.ToString();
			string sql="",sql1="";
			if(this.GetSQL("Manager.ConstantGroup.Select",ref sql)==-1) return null;
			if(this.GetSQL("Manager.ConstantGroup.Get.Where",ref sql1)==-1) return null;
			sql = sql+""+ sql1;
			sql = string.Format(sql, constType);
			ArrayList al =myList(sql);
			if(al ==null || al.Count ==0) return null;
			return al[0] as FS.FrameWork.Models.NeuObject;			
		}


		/// <summary>
		/// ɾ��һ������
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int Del(object obj) {
			// TODO:  ��� ConstantGroup.Del ʵ��
			string groupCode = obj.ToString();
			string strSql="";
			if(this.GetSQL("Manager.ConstantGroup.Delete",ref strSql)==-1) return -1;
			try {
				strSql=string.Format(strSql,groupCode);			
			}
			catch(Exception ex) {
				this.Err="��ֵʱ�����"+ex.Message;
				this.WriteErr();
				return -1;
			}
			if(this.ExecNoQuery(strSql)<=0) return -1;
			return 0;
		}


		public int SetList(System.Collections.ArrayList al) {
			// TODO:  ��� ConstantGroup.SetList ʵ��
			return 0;
		}


		public int Insert(FS.HISFC.Models.Admin.ConstantGroup obj) {
			string strSql="";
			if(this.GetSQL("Manager.ConstantGroup.Insert",ref strSql)==-1) return -1;
			try {
				string[] s = this.mySetInfo(obj);
				strSql=string.Format(strSql,s);			
			}
			catch(Exception ex) {
				this.Err="��ֵʱ�����"+ex.Message;
				this.WriteErr();
				return -1;
			}
			if(this.ExecNoQuery(strSql)<=0) return -1;
			return 0;
		}


		public int Updatge(FS.HISFC.Models.Admin.ConstantGroup obj) {
			string strSql="";
			if(this.GetSQL("Manager.ConstantGroup.Update",ref strSql)==-1) return -1;
			try {
				string[] s = this.mySetInfo(obj);
				strSql=string.Format(strSql,s);			
			}
			catch(Exception ex) {
				this.Err="��ֵʱ�����"+ex.Message;
				this.WriteErr();
				return -1;
			}
			if(this.ExecNoQuery(strSql)<=0) return -1;
			return 0;
		}


		public int Set(FS.FrameWork.Models.NeuObject obj) {
			return 0;
		}

		#endregion
		#region ˽��
		/// <summary>
		/// ����б�
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		private ArrayList myList(string sql) {
			if(this.ExecQuery(sql)==-1) return null;
			ArrayList al=new ArrayList();

			try {
				while(this.Reader.Read()) {
					FS.HISFC.Models.Admin.ConstantGroup obj = new FS.HISFC.Models.Admin.ConstantGroup();
					obj.PargrpCode =this.Reader[0].ToString();   //����������
					obj.CurgrpCode =this.Reader[1].ToString();   //����������
					obj.ID=this.Reader[2].ToString();            //�������
					obj.Name =this.Reader[3].ToString();         //��������
					obj.ControlName = this.Reader[4].ToString(); //���ù��ܿؼ�
					obj.Memo =this.Reader[5].ToString();         //��ע
					al.Add(obj);
				}
				this.Reader.Close();
				return al;
			}
			catch{return null;}
		}


		private string[] mySetInfo(object obj) {
			FS.HISFC.Models.Admin.ConstantGroup o = obj as FS.HISFC.Models.Admin.ConstantGroup;
			try {
				string[] s = {
								 o.PargrpCode,   //����������
								 o.CurgrpCode,   //����������
								 o.ID ,          //�������
								 o.Name ,        //��������
								 o.ControlName,  //���ù��ܿؼ�
								 o.Memo          //��ע
							 };
				
				return s;
			}			
			catch(Exception ex) {
				this.Err = ex.Message;
				return null;
			}
		}
		#endregion


	}
}
