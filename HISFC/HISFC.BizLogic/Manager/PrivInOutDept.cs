using System;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.HISFC.BizLogic.Manager {
	/// <summary>
	/// �������ҹ�����
	/// writed by cuipeng
	/// 2005-3
	/// </summary>
	public class PrivInOutDept : DataBase {

		public PrivInOutDept() {
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		/// <summary>
		/// ���ݿ��ұ���ȡһ������������Ϣ
		/// </summary>
		/// <param name="deptCode">���ұ���</param>
		/// <returns>��������</returns>
		private FS.HISFC.Models.Base.PrivInOutDept GetPrivInOutDept(string deptCode) {
			string strSQL = "";
			//ȡSELECT���
			if (this.GetSQL("Manager.PrivInOutDept.GetPrivInOutDept",ref strSQL) == -1) {
				this.Err="û���ҵ�Manager.PrivInOutDept.GetPrivInOutDept�ֶ�!";
				return null;
			}
			
			string strWhere = "";
			//ȡWHERE���
			if (this.GetSQL("Manager.PrivInOutDept.GetPrivInOutDept.Where",ref strWhere) == -1) {
				this.Err="û���ҵ�Manager.PrivInOutDept.GetPrivInOutDept.Where�ֶ�!";
				return null;
			}

			//��ʽ��SQL���
			try {
				strSQL += " " +strWhere;
				strSQL = string.Format(strSQL, deptCode);
			}
			catch (Exception ex) {
				this.Err = "��ʽ��SQL���ʱ����Manager.PrivInOutDept.GetPrivInOutDept.Where:" + ex.Message;
				return null;
			}

			//ȡ��������
			ArrayList al = this.myGetPrivInOutDept(strSQL);
			if(al == null) 
				return null;

			if(al.Count == 0) 
				return new FS.HISFC.Models.Base.PrivInOutDept();

			return al[0] as FS.HISFC.Models.Base.PrivInOutDept;
		}


		/// <summary>
		/// ȡ���������б�
		/// </summary>
		/// <returns>�����������飬������null</returns>
		public ArrayList GetPrivInOutDeptList(string dept, string privCode) {
			string strSQL = "";
			//ȡSELECT���
			if (this.GetSQL("Manager.PrivInOutDept.GetPrivInOutDept",ref strSQL) == -1) {
				this.Err="û���ҵ�Manager.PrivInOutDept.GetPrivInOutDept�ֶ�!";
				return null;
			}

			try {  
				strSQL=string.Format(strSQL, dept, privCode);	//�滻SQL����еĲ�����
			}
			catch(Exception ex) {
				this.Err = "��ʽ��SQL���ʱ����Manager.PrivInOutDept.GetPrivInOutDept:" + ex.Message;
				this.WriteErr();
				return null;
			}
			//ȡ������������
			return this.myGetPrivInOutDept(strSQL);
		}


		/// <summary>
		/// ��������ұ��в���һ����¼
		/// </summary>
		/// <param name="PrivInOutDept">������չ������</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int InsertPrivInOutDept(FS.HISFC.Models.Base.PrivInOutDept PrivInOutDept) {
			string strSQL="";
			//ȡ���������SQL���
			if(this.GetSQL("Manager.PrivInOutDept.InsertPrivInOutDept",ref strSQL) == -1) {
				this.Err="û���ҵ�Manager.PrivInOutDept.InsertPrivInOutDept�ֶ�!";
				return -1;
			}
			try {  
				string[] strParm = myGetParmPrivInOutDept( PrivInOutDept );     //ȡ�����б�
				strSQL=string.Format(strSQL, strParm);            //�滻SQL����еĲ�����
			}
			catch(Exception ex) {
				this.Err = "��ʽ��SQL���ʱ����Manager.PrivInOutDept.InsertPrivInOutDept:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}
		
		
		/// <summary>
		/// ���³������ұ���һ����¼
		/// </summary>
		/// <param name="PrivInOutDept">������չ������</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int UpdatePrivInOutDept(FS.HISFC.Models.Base.PrivInOutDept PrivInOutDept) {
			string strSQL="";
			//ȡ���²�����SQL���
			if(this.GetSQL("Manager.PrivInOutDept.UpdatePrivInOutDept",ref strSQL) == -1) {
				this.Err="û���ҵ�Manager.PrivInOutDept.UpdatePrivInOutDept�ֶ�!";
				return -1;
			}
			try {  
				string[] strParm = myGetParmPrivInOutDept( PrivInOutDept );     //ȡ�����б�
				strSQL=string.Format(strSQL, strParm);            //�滻SQL����еĲ�����
			}
			catch(Exception ex) {
				this.Err = "��ʽ��SQL���ʱ����Manager.PrivInOutDept.UpdatePrivInOutDept:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}
		
		
		/// <summary>
		/// ɾ���������ұ���һ����¼
		/// </summary>
		/// <param name="class2Code">����Ȩ�ޱ���</param>
		/// <param name="deptCode">���ű���</param>
		/// <param name="inOutDeptCode">Ҫɾ���ĳ������ұ���</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int DeletePrivInOutDept(string class2Code, string deptCode, string inOutDeptCode) {
			string strSQL="";
			//ȡɾ��������SQL���
			if(this.GetSQL("Manager.PrivInOutDept.DeletePrivInOutDept",ref strSQL) == -1) {
				this.Err="û���ҵ�Manager.PrivInOutDept.DeletePrivInOutDept�ֶ�!";
				return -1;
			}
			try {  
				strSQL=string.Format(strSQL, class2Code, deptCode, inOutDeptCode);//�滻SQL����еĲ�����
			}
			catch(Exception ex) {
				this.Err = "��ʽ��SQL���ʱ����Manager.PrivInOutDept.DeletePrivInOutDept:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}
		

		/// <summary>
		/// ɾ��ĳһ���ŵ�ȫ����������
		/// </summary>
		/// <param name="deptCode">���ű���</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int DeletePrivInOutDeptAll(string class2Code, string deptCode) {
			string strSQL="";
			//ȡɾ��������SQL���
			if(this.GetSQL("Manager.PrivInOutDept.DeletePrivInOutDeptAll",ref strSQL) == -1) {
				this.Err="û���ҵ�Manager.PrivInOutDept.DeletePrivInOutDeptAll�ֶ�!";
				return -1;
			}
			try {  
				strSQL=string.Format(strSQL, class2Code, deptCode);    //�滻SQL����еĲ�����
			}
			catch(Exception ex) {
				this.Err = "��ʽ��SQL���ʱ����Manager.PrivInOutDept.DeletePrivInOutDeptAll:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}
		

		/// <summary>
		/// ������Ա���Ա䶯���ݣ�����ִ�и��²��������û���ҵ����Ը��µ����ݣ������һ���¼�¼
		/// </summary>
		/// <param name="PrivInOutDept">��������ʵ��</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		private int SetPrivInOutDept(FS.HISFC.Models.Base.PrivInOutDept PrivInOutDept) {
			int parm;
			//ִ�и��²���
			parm = UpdatePrivInOutDept(PrivInOutDept);

			//���û���ҵ����Ը��µ����ݣ������һ���¼�¼
			if (parm == 0 ) {
				parm = InsertPrivInOutDept(PrivInOutDept);
			}
			return parm;
		}


		/// <summary>
		/// ȡ���������б�������һ�����߶���
		/// ˽�з����������������е���
		/// </summary>
		/// <param name="SQLString">SQL���</param>
		/// <returns>����������Ϣ��������</returns>
		private ArrayList myGetPrivInOutDept(string SQLString) {
			ArrayList al=new ArrayList();                
			FS.HISFC.Models.Base.PrivInOutDept PrivInOutDept; //��������ʵ��
			this.ProgressBarText="���ڼ�����������...";
			this.ProgressBarValue=0;
			
			//ִ�в�ѯ���
			if (this.ExecQuery(SQLString)==-1) {
				this.Err="��ó�������ʱ��ִ��SQL������"+this.Err;
				this.ErrCode="-1";
				return null;
			}
			try {
				while (this.Reader.Read()) {
					//ȡ��ѯ����еļ�¼
					PrivInOutDept = new FS.HISFC.Models.Base.PrivInOutDept();
					PrivInOutDept.Role.Grade2.ID = this.Reader[0].ToString();		//0 Ȩ�����:����0501-��⣬0502-����
					PrivInOutDept.ID = this.Reader[1].ToString();				//1 ���ұ���
					PrivInOutDept.Dept.ID   = this.Reader[2].ToString();	//2 ��ҩ����ҩ��λ��
					PrivInOutDept.Dept.Name = this.Reader[3].ToString();	//3 ��ҩ����ҩ��λ����(ֻ�ڲ������ݵ�ʱ��ʹ�ã��������ڳ�����)
					PrivInOutDept.User01 = this.Reader[4].ToString();			//4 ����Ա���� 
					PrivInOutDept.User02 = this.Reader[5].ToString();			//5 ����ʱ��
					PrivInOutDept.Memo   = this.Reader[6].ToString();			//6 ��ע
					PrivInOutDept.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[7].ToString());	//7 ����
					al.Add(PrivInOutDept);
				}
			}//�׳�����
			catch(Exception ex) {
				this.Err="��ó���������Ϣʱ����"+ex.Message;
				this.ErrCode="-1";
				return null;
			}
			this.Reader.Close();

			this.ProgressBarValue=-1;
			return al;
		}


		/// <summary>
		/// ���update����insert�������ұ�Ĵ����������
		/// </summary>
		/// <param name="PrivInOutDept">��������ʵ��</param>
		/// <returns>�ַ�������</returns>
		private string[] myGetParmPrivInOutDept(FS.HISFC.Models.Base.PrivInOutDept PrivInOutDept) {
			string[] strParm={   
								 PrivInOutDept.Role.Grade2.ID,		//0 Ȩ�����:����0501-��⣬0502-����
								 PrivInOutDept.ID,				//1 ���ұ���
								 PrivInOutDept.Dept.ID,	//2 ��ҩ����ҩ��λ��
								 PrivInOutDept.Dept.Name,	//3 ��ҩ����ҩ��λ����(ֻ�ڲ������ݵ�ʱ��ʹ�ã��������ڳ�����)
								 this.Operator.ID,				//4 ����Ա����׼�����ϣ�
								 PrivInOutDept.Memo,			//5 ��ע
								 PrivInOutDept.SortID.ToString()//6 ����
							 };				 
			return strParm;
		}

		
	}

}
