using System;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.HISFC.BizLogic.Manager
{
	/// <summary>
	/// ComExtInfo ��ժҪ˵����
	/// </summary>
	[Obsolete("��ExtendParam������",true)]
	public class ComExtInfo:DataBase
	{
		public ComExtInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		/// <summary>
		/// ȡ�ض���Ŀ���ض��������չ����
		/// </summary>
		/// <param name="PropertyCode">���Ա���</param>
		/// <param name="ItemCode">��Ŀ����</param>
		/// <returns>��Ŀ����</returns>
		public FS.HISFC.Models.Base.ComExtInfo GetComExtInfo(string PropertyCode,string ItemCode) 
		{
			string strSQL = "";
			string strWhere = "";
			//ȡSELECT���
			if (this.GetSQL("Manager.ComExtInfo.GetComExtInfoList",ref strSQL) == -1) 
			{
				this.Err="û���ҵ�Manager.ComExtInfo.GetComExtInfoList�ֶ�!";
				return null;
			}
			if (this.GetSQL("Manager.ComExtInfo.And.ItemID",ref strWhere) == -1) 
			{
				this.Err="û���ҵ�Manager.ComExtInfo.And.ItemID�ֶ�!";
				return null;
			}
			//��ʽ��SQL���
			try 
			{
				strSQL += " " +strWhere;
				strSQL = string.Format(strSQL, PropertyCode, ItemCode);
			}
			catch (Exception ex) 
			{
				this.Err = "��ʽ��SQL���ʱ����Manager.ComExtInfo.And.ItemID:" + ex.Message;
				return null;
			}

			//ȡ��Ŀ��������
			ArrayList al = this.myGetComExtInfo(strSQL);
			if(al == null) return null;

			if(al.Count == 0) 
				return new FS.HISFC.Models.Base.ComExtInfo();

			return al[0] as FS.HISFC.Models.Base.ComExtInfo;
		}


		/// <summary>
		/// ȡ��Ŀ����ֵ����
		/// </summary>
		/// <param name="PropertyCode">�������</param>
		/// <param name="ItemCode">��Ŀ����</param>
		/// <returns>��ֵ����</returns>
		public decimal GetComExtInfoNumber(string PropertyCode,string ItemCode) 
		{
			//ȡ��Ŀ��Ŀ����չ����ʵ��
			FS.HISFC.Models.Base.ComExtInfo ext = this.GetComExtInfo(PropertyCode, ItemCode);
			if(ext == null) 
				return 0M;
			else
				return ext.NumberProperty;
		}


		/// <summary>
		/// ȡ��Ŀ���ַ�����
		/// </summary>
		/// <param name="PropertyCode">�������</param>
		/// <param name="ItemCode">��Ŀ����</param>
		/// <returns>�ַ�����</returns>
		public string GetComExtInfoString(string PropertyCode,string ItemCode) 
		{
			//ȡ��Ŀ��չ����ʵ��
			FS.HISFC.Models.Base.ComExtInfo ext = this.GetComExtInfo(PropertyCode, ItemCode);
			if(ext == null) 
				return "";
			else
				return ext.StringProperty;
		}

		
		/// <summary>
		/// ȡ��Ŀ����������
		/// </summary>
		/// <param name="PropertyCode">�������</param>
		/// <param name="ItemCode">��Ŀ����</param>
		/// <returns>��������</returns>
		public DateTime GetComExtInfoDateTime(string PropertyCode,string ItemCode) 
		{
			//ȡ��Ŀ��չ����ʵ��
			FS.HISFC.Models.Base.ComExtInfo ext = this.GetComExtInfo(PropertyCode, ItemCode);
			if(ext == null) 
				return DateTime.MinValue;
			else
				return ext.DateProperty;
		}

		/// <summary>
		/// ȡĳһ��չ��������
		/// </summary>
		/// <param name="propertyCode">���Ա���</param>
		/// <returns>��Ŀ�������飬������null</returns>
		public ArrayList GetComExtInfoList(string propertyCode) 
		{
			string strSQL = "";
			//string strWhere = "";
			//ȡSELECT���
			if (this.GetSQL("Manager.ComExtInfo.GetComExtInfoList",ref strSQL) == -1) 
			{
				this.Err="û���ҵ�Manager.ComExtInfo.GetComExtInfoList�ֶ�!";
				return null;
			}

			//��ʽ��SQL���
			try 
			{
				strSQL = string.Format(strSQL, propertyCode);
			}
			catch (Exception ex) 
			{
				this.Err = "��ʽ��SQL���ʱ����Manager.ComExtInfo.GetComExtInfoList:" + ex.Message;
				return null;
			}

			//ȡ��Ŀ��������
			return this.myGetComExtInfo(strSQL);
		}


		/// <summary>
		/// ����Ŀ���Ա��в���һ����¼
		/// </summary>
		/// <param name="departmentExt">������չ������</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int InsertComExtInfo(FS.HISFC.Models.Base.ComExtInfo departmentExt) 
		{
			string strSQL="";
			//ȡ���������SQL���
			if(this.GetSQL("Manager.ComExtInfo.InsertComExtInfo",ref strSQL) == -1) 
			{
				this.Err="û���ҵ�Manager.ComExtInfo.InsertComExtInfo�ֶ�!";
				return -1;
			}
			try 
			{  
				string[] strParm = myGetParmComExtInfo( departmentExt );     //ȡ�����б�
				strSQL=string.Format(strSQL, strParm);            //�滻SQL����еĲ�����
			}
			catch(Exception ex) 
			{
				this.Err = "��ʽ��SQL���ʱ����Manager.ComExtInfo.InsertComExtInfo:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}
		
		
		/// <summary>
		/// ������Ŀ���Ա���һ����¼
		/// </summary>
		/// <param name="departmentExt">��Ŀ��չ������</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int UpdateComExtInfo(FS.HISFC.Models.Base.ComExtInfo departmentExt) 
		{
			string strSQL="";
			//ȡ���²�����SQL���
			if(this.GetSQL("Manager.ComExtInfo.UpdateComExtInfo",ref strSQL) == -1) 
			{
				this.Err="û���ҵ�Manager.ComExtInfo.UpdateComExtInfo�ֶ�!";
				return -1;
			}
			try 
			{  
				string[] strParm = myGetParmComExtInfo( departmentExt );     //ȡ�����б�
				strSQL=string.Format(strSQL, strParm);						//�滻SQL����еĲ�����
			}
			catch(Exception ex) 
			{
				this.Err = "��ʽ��SQL���ʱ����Manager.ComExtInfo.UpdateComExtInfo:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}
		
		
		/// <summary>
		/// ɾ����Ŀ���Ա���һ����¼
		/// </summary>
		/// <param name="deptCode">��Ŀ����</param>
		/// <param name="propertyCode">���Ա���</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int DeleteComExtInfo(string deptCode, string propertyCode) 
		{
			string strSQL="";
			//ȡɾ��������SQL���
			if(this.GetSQL("Manager.ComExtInfo.DeleteComExtInfo",ref strSQL) == -1) 
			{
				this.Err="û���ҵ�Manager.ComExtInfo.DeleteComExtInfo�ֶ�!";
				return -1;
			}
			try 
			{  
				//����������ӵ���Ŀ���Ե�����ֱ�ӷ���
				strSQL=string.Format(strSQL, deptCode, propertyCode);    //�滻SQL����еĲ�����
			}
			catch(Exception ex) 
			{
				this.Err = "��ʽ��SQL���ʱ����Manager.ComExtInfo.DeleteComExtInfo:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}
		

		/// <summary>
		/// ������Ŀ��չ�������ݣ�����ִ�и��²��������û���ҵ����Ը��µ����ݣ������һ���¼�¼
		/// </summary>
		/// <param name="departmentExt">��Ŀ��չ����ʵ��</param>
		/// <returns>1�ɹ� -1ʧ��</returns>
		public int SetComExtInfo(FS.HISFC.Models.Base.ComExtInfo departmentExt) 
		{
			int parm;
			//ִ�и��²���
			parm = UpdateComExtInfo(departmentExt);

			//���û���ҵ����Ը��µ����ݣ������һ���¼�¼
			if (parm == 0 ) 
			{
				parm = InsertComExtInfo(departmentExt);
			}
			return parm;
		}


		/// <summary>
		/// ȡ��Ŀ������Ϣ�б�������һ�����߶�����Ŀ��չ����
		/// ˽�з����������������е���
		/// writed by cuipeng
		/// 2005-1
		/// </summary>
		/// <param name="SQLString">SQL���</param>
		/// <returns>��Ŀ������Ϣ��������</returns>
		private ArrayList myGetComExtInfo(string SQLString) 
		{
			ArrayList al=new ArrayList();                
			FS.HISFC.Models.Base.ComExtInfo departmentExt; //��Ŀ������Ϣʵ��
			this.ProgressBarText="���ڼ�����Ŀ���Ե���Ϣ...";
			this.ProgressBarValue=0;
			
			//ִ�в�ѯ���
			if (this.ExecQuery(SQLString)==-1) 
			{
				this.Err="�����Ŀ������Ϣʱ��ִ��SQL������"+this.Err;
				this.ErrCode="-1";
				return null;
			}
			try 
			{
				while (this.Reader.Read()) 
				{
					//ȡ��ѯ����еļ�¼
					departmentExt = new FS.HISFC.Models.Base.ComExtInfo();
					departmentExt.Item.ID   = this.Reader[0].ToString();          //0 ��Ŀ����
					departmentExt.PropertyCode   = this.Reader[1].ToString();     //2 ���Ա���
					departmentExt.PropertyName   = this.Reader[2].ToString();     //3 ��������
					departmentExt.StringProperty = this.Reader[3].ToString();     //4 �ַ����� 
					departmentExt.NumberProperty = NConvert.ToDecimal(this.Reader[4].ToString()); //5 ��ֵ����
					departmentExt.DateProperty   = NConvert.ToDateTime(this.Reader[5].ToString());//6 ��������
					departmentExt.Memo      = this.Reader[6].ToString();          //7 ��ע
					departmentExt.OperEnvironment.ID  = this.Reader[7].ToString();          //8 ��������
					departmentExt.OperEnvironment.OperTime  = NConvert.ToDateTime(this.Reader[8].ToString());     //9 ����ʱ��
					this.ProgressBarValue++;
					al.Add(departmentExt);
				}
			}//�׳�����
			catch(Exception ex) 
			{
				this.Err="�����Ŀ������Ϣʱ����"+ex.Message;
				this.ErrCode="-1";
				return null;
			}
			this.Reader.Close();

			this.ProgressBarValue=-1;
			return al;
		}


		/// <summary>
		/// ���update����insert��Ŀ���Ա�Ĵ����������
		/// </summary>
		/// <param name="departmentExt">��Ŀ������</param>
		/// <returns>�ַ�������</returns>
		private string[] myGetParmComExtInfo(FS.HISFC.Models.Base.ComExtInfo departmentExt) 
		{

			string[] strParm={   
								 departmentExt.Item.ID,                  //0 ��Ŀ����
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
