using System;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.HISFC.BizLogic.Manager {
	/// <summary>
	/// ������չȨ�޹�����
	/// writed by cuipeng
	/// 2005-3
	/// </summary>
	[Obsolete("��ExtendParam������",true)]
	public class DepartmentExt : DataBase {

		public DepartmentExt() {
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		/// <summary>
		/// ȡĳһ���ң��ض��������չ����
		/// </summary>
		/// <param name="PropertyCode">���Ա���</param>
		/// <param name="DeptCode">���ұ���</param>
		/// <returns>��������</returns>
		public FS.HISFC.Models.Base.DepartmentExt GetDepartmentExt(string PropertyCode,string DeptCode) {
			string strSQL = "";
			string strWhere = "";
			//ȡSELECT���
			if (this.GetSQL("Manager.DepartmentExt.GetDepartmentExtList",ref strSQL) == -1) {
				this.Err="û���ҵ�Manager.DepartmentExt.GetDepartmentExtList�ֶ�!";
				return null;
			}
			if (this.GetSQL("Manager.DepartmentExt.And.DeptID",ref strWhere) == -1) {
				this.Err="û���ҵ�Manager.DepartmentExt.And.DeptID�ֶ�!";
				return null;
			}
			//��ʽ��SQL���
			try {
				strSQL += " " +strWhere;
				strSQL = string.Format(strSQL, PropertyCode, DeptCode);
			}
			catch (Exception ex) {
				this.Err = "��ʽ��SQL���ʱ����Manager.DepartmentExt.And.DeptID:" + ex.Message;
				return null;
			}

			//ȡ������������
			ArrayList al = this.myGetDepartmentExt(strSQL);
			if(al == null) return null;

			if(al.Count == 0) 
				return new FS.HISFC.Models.Base.DepartmentExt();

			return al[0] as FS.HISFC.Models.Base.DepartmentExt;
		}


		/// <summary>
		/// ȡ���ҵ���ֵ����
		/// </summary>
		/// <param name="PropertyCode">�������</param>
		/// <param name="DeptCode">���ұ���</param>
		/// <returns>��ֵ����</returns>
		public decimal GetDepartmentExtNumber(string PropertyCode,string DeptCode) {
			//ȡ������չ����ʵ��
			FS.HISFC.Models.Base.DepartmentExt ext = this.GetDepartmentExt(PropertyCode, DeptCode);
			if(ext == null) 
				return 0M;
			else
				return ext.NumberProperty;
		}


		/// <summary>
		/// ȡ���ҵ��ַ�����
		/// </summary>
		/// <param name="PropertyCode">�������</param>
		/// <param name="DeptCode">���ұ���</param>
		/// <returns>�ַ�����</returns>
		public string GetDepartmentExtString(string PropertyCode,string DeptCode) {
			//ȡ������չ����ʵ��
			FS.HISFC.Models.Base.DepartmentExt ext = this.GetDepartmentExt(PropertyCode, DeptCode);
			if(ext == null) 
				return "";
			else
				return ext.StringProperty;
		}


		/// <summary>
		/// ȡ���ҵ���������
		/// </summary>
		/// <param name="PropertyCode">�������</param>
		/// <param name="DeptCode">���ұ���</param>
		/// <returns>��������</returns>
		public DateTime GetDepartmentExtDateTime(string PropertyCode,string DeptCode) {
			//ȡ������չ����ʵ��
			FS.HISFC.Models.Base.DepartmentExt ext = this.GetDepartmentExt(PropertyCode, DeptCode);
			if(ext == null) 
				return DateTime.MinValue;
			else
				return ext.DateProperty;
		}


		/// <summary>
		/// ȡȫԺ���ҵ�ĳһ��չ��������
		/// </summary>
		/// <param name="propertyCode">���Ա���</param>
		/// <returns>�����������飬������null</returns>
		public ArrayList GetDepartmentExtList(string propertyCode) {
			string strSQL = "";
			//string strWhere = "";
			//ȡSELECT���
			if (this.GetSQL("Manager.DepartmentExt.GetDepartmentExtList",ref strSQL) == -1) {
				this.Err="û���ҵ�Manager.DepartmentExt.GetDepartmentExtList�ֶ�!";
				return null;
			}

			//��ʽ��SQL���
			try {
				strSQL = string.Format(strSQL, propertyCode);
			}
			catch (Exception ex) {
				this.Err = "��ʽ��SQL���ʱ����Manager.DepartmentExt.GetDepartmentExtList:" + ex.Message;
				return null;
			}

			//ȡ������������
			return this.myGetDepartmentExt(strSQL);
		}


		/// <summary>
		/// ��������Ա��в���һ����¼
		/// </summary>
		/// <param name="departmentExt">������չ������</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int InsertDepartmentExt(FS.HISFC.Models.Base.DepartmentExt departmentExt) {
			string strSQL="";
			//ȡ���������SQL���
			if(this.GetSQL("Manager.DepartmentExt.InsertDepartmentExt",ref strSQL) == -1) {
				this.Err="û���ҵ�Manager.DepartmentExt.InsertDepartmentExt�ֶ�!";
				return -1;
			}
			try {  
				string[] strParm = myGetParmDepartmentExt( departmentExt );     //ȡ�����б�
				strSQL=string.Format(strSQL, strParm);            //�滻SQL����еĲ�����
			}
			catch(Exception ex) {
				this.Err = "��ʽ��SQL���ʱ����Manager.DepartmentExt.InsertDepartmentExt:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}
		
		
		/// <summary>
		/// ���¿������Ա���һ����¼
		/// </summary>
		/// <param name="departmentExt">������չ������</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int UpdateDepartmentExt(FS.HISFC.Models.Base.DepartmentExt departmentExt) {
			string strSQL="";
			//ȡ���²�����SQL���
			if(this.GetSQL("Manager.DepartmentExt.UpdateDepartmentExt",ref strSQL) == -1) {
				this.Err="û���ҵ�Manager.DepartmentExt.UpdateDepartmentExt�ֶ�!";
				return -1;
			}
			try {  
				string[] strParm = myGetParmDepartmentExt( departmentExt );     //ȡ�����б�
				strSQL=string.Format(strSQL, strParm);            //�滻SQL����еĲ�����
			}
			catch(Exception ex) {
				this.Err = "��ʽ��SQL���ʱ����Manager.DepartmentExt.UpdateDepartmentExt:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}
		
				
		/// <summary>
		/// ���¿������Ա���һ����¼
		/// </summary>
		/// <param name="departmentExt">������չ������</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int UpdateAll(FS.HISFC.Models.Base.DepartmentExt departmentExt) {
			string strSQL="";
			//ȡ���²�����SQL���
			if(this.GetSQL("Manager.DepartmentExt.UpdateAll",ref strSQL) == -1) {
				this.Err="û���ҵ�Manager.DepartmentExt.UpdateAll�ֶ�!";
				return -1;
			}
			try {  
				string[] strParm = myGetParmDepartmentExt( departmentExt );     //ȡ�����б�
				strSQL=string.Format(strSQL, strParm);            //�滻SQL����еĲ�����
			}
			catch(Exception ex) {
				this.Err = "��ʽ��SQL���ʱ����Manager.DepartmentExt.UpdateAll:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}
		
		/// <summary>
		/// ɾ���������Ա���һ����¼
		/// </summary>
		/// <param name="deptCode">���ұ���</param>
		/// <param name="propertyCode">���Ա���</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int DeleteDepartmentExt(string deptCode, string propertyCode) {
			string strSQL="";
			//ȡɾ��������SQL���
			if(this.GetSQL("Manager.DepartmentExt.DeleteDepartmentExt",ref strSQL) == -1) {
				this.Err="û���ҵ�Manager.DepartmentExt.DeleteDepartmentExt�ֶ�!";
				return -1;
			}
			try {  
				//����������ӵĿ������Ե�����ֱ�ӷ���
				strSQL=string.Format(strSQL, deptCode, propertyCode);    //�滻SQL����еĲ�����
			}
			catch(Exception ex) {
				this.Err = "��ʽ��SQL���ʱ����Manager.DepartmentExt.DeleteDepartmentExt:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}
		

		/// <summary>
		/// ���������չ�������ݣ�����ִ�и��²��������û���ҵ����Ը��µ����ݣ������һ���¼�¼
		/// </summary>
		/// <param name="departmentExt">������չ����ʵ��</param>
		/// <returns>1�ɹ� -1ʧ��</returns>
		public int SetDepartmentExt(FS.HISFC.Models.Base.DepartmentExt departmentExt) {
			int parm;
			//ִ�и��²���
			parm = UpdateDepartmentExt(departmentExt);

			//���û���ҵ����Ը��µ����ݣ������һ���¼�¼
			if (parm == 0 ) {
				parm = InsertDepartmentExt(departmentExt);
			}
			return parm;
		}


		/// <summary>
		/// ȡ����������Ϣ�б�������һ�����߶���������չ����
		/// ˽�з����������������е���
		/// writed by cuipeng
		/// 2005-1
		/// </summary>
		/// <param name="SQLString">SQL���</param>
		/// <returns>����������Ϣ��������</returns>
		private ArrayList myGetDepartmentExt(string SQLString) {
			ArrayList al=new ArrayList();                
			FS.HISFC.Models.Base.DepartmentExt departmentExt; //����������Ϣʵ��
			this.ProgressBarText="���ڼ����������Ե���Ϣ...";
			this.ProgressBarValue=0;
			
			//ִ�в�ѯ���
			if (this.ExecQuery(SQLString)==-1) {
				this.Err="��ÿ���������Ϣʱ��ִ��SQL������"+this.Err;
				this.ErrCode="-1";
				return null;
			}
			try {
				while (this.Reader.Read()) {
					//ȡ��ѯ����еļ�¼
					departmentExt = new FS.HISFC.Models.Base.DepartmentExt();
					departmentExt.Dept.ID   = this.Reader[0].ToString();          //0 ���ұ���
					departmentExt.Dept.Name = this.Reader[1].ToString();          //1 ��������
					departmentExt.PropertyCode   = this.Reader[2].ToString();     //2 ���Ա���
					departmentExt.PropertyName   = this.Reader[3].ToString();     //3 ��������
					departmentExt.StringProperty = this.Reader[4].ToString();     //4 �ַ����� 
					departmentExt.NumberProperty = NConvert.ToDecimal(this.Reader[5].ToString()); //5 ��ֵ����
					departmentExt.DateProperty   = NConvert.ToDateTime(this.Reader[6].ToString());//6 ��������
					departmentExt.Memo      = this.Reader[7].ToString();          //7 ��ע
					departmentExt.OperEnvironment.ID  = this.Reader[8].ToString();          //8 ��������
					departmentExt.OperEnvironment.OperTime  = NConvert.ToDateTime(this.Reader[9].ToString());     //9 ����ʱ��
					departmentExt.User01    = this.Reader[10].ToString();         //��������
					this.ProgressBarValue++;
					al.Add(departmentExt);
				}
			}//�׳�����
			catch(Exception ex) {
				this.Err="��ÿ���������Ϣʱ����"+ex.Message;
				this.ErrCode="-1";
				return null;
			}
			this.Reader.Close();

			this.ProgressBarValue=-1;
			return al;
		}


		/// <summary>
		/// ���update����insert�������Ա�Ĵ����������
		/// </summary>
		/// <param name="departmentExt">����������</param>
		/// <returns>�ַ�������</returns>
		private string[] myGetParmDepartmentExt(FS.HISFC.Models.Base.DepartmentExt departmentExt) {

			string[] strParm={   
								 departmentExt.Dept.ID,                  //0 ���ұ���
								 departmentExt.PropertyCode.ToString(),  //1 ���Ա���
								 departmentExt.PropertyName.ToString(),  //2 ��������
								 departmentExt.StringProperty.ToString(),//3 �ַ����� 
								 departmentExt.NumberProperty.ToString(),//4 ��ֵ����
								 departmentExt.DateProperty.ToString(),  //5 ��������
								 departmentExt.Memo.ToString(),          //6 ��ע
								 this.Operator.ID,                       //7 ����Ա����
			};								 
			return strParm;
		}

		
	}

}
