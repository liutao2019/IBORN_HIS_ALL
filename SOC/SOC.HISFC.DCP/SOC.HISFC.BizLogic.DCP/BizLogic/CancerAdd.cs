using System;
using System.Collections;

namespace FS.SOC.HISFC.BizLogic.DCP
{
	/// <summary>
	/// CancerAdd ��ժҪ˵����
	/// ��������ҵ��
	/// </summary>
	public class CancerAdd : FS.FrameWork.Management.Database
	{
		public CancerAdd()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		/// <summary>
		/// ��ȡ�������濨�Ĳ���
		/// </summary>
		/// <param name="cancerAddress">ʵ��</param>
		/// <returns>��������</returns>
		private string[] myGetCancerAddReportParm(FS.HISFC.DCP.Object.CancerAdd cancerAdd)
		{
			string[] strParm = {
							cancerAdd.REPORT_NO       ,
							cancerAdd.MEIDCAL_CARD          ,
							cancerAdd.NATION                ,
							cancerAdd.WORK_TYPE             ,
							cancerAdd.MARRIAGE             ,
							cancerAdd.DIAGNOSTIC_ICD       ,
							cancerAdd.REGISTER_PROVINCE    ,
							cancerAdd.REGISTER_CITY        ,
							cancerAdd.REGISTER_DISTRICT    ,
							cancerAdd.REGISTER_STREET       ,
							cancerAdd.REGISTER_HOUSENUMBER  ,
							cancerAdd.REGISTER_POST         ,
							cancerAdd.WORK_PLACE            ,
							cancerAdd.CONTACT_PERSON        ,
							cancerAdd.RELATIONSHIP          ,
							cancerAdd.CONTACT_PERSON_TEL    ,
							cancerAdd.CONTACT_PERSON_ADDR   ,
							cancerAdd.CLINICAL_T            ,
							cancerAdd.CLINICAL_N            ,
							cancerAdd.CLINICAL_M            ,
							cancerAdd.TERM_TNM              ,
							cancerAdd.PATHOLOGY_CHECK       ,
							cancerAdd.PATHOLOGY_NO          ,
							cancerAdd.PATHOLOGY_TYPE        ,
							cancerAdd.PATHOLOGY_DEGREE      ,
							cancerAdd.ICD_O                 ,
							cancerAdd.TREATMENT             ,
							cancerAdd.DEATH_REASON          ,
							cancerAdd.OLD_DIAGNOSES         ,
							cancerAdd.OLD_DIAGNOSES_DATE.ToString()  ,
				            cancerAdd.District        ,
				            cancerAdd.HandPhone        
								   
							   };
			return strParm;
		}
 /// <summary>
 /// ���� ��������
 /// </summary>
		public int InsertCancerAdd(FS.HISFC.DCP.Object.CancerAdd cancerAdd  )
		{
			string strSQL = "";
			if (this.Sql.GetSql("DCP.CancerAdd.Insert", ref strSQL) == -1)
			{
				this.Err = "û���ҵ�DCP.CancerAdd.Insert�ֶ�";
				return -1;
			}
			try 
			{ 
				string[] strParm = this.myGetCancerAddReportParm(cancerAdd);  //ȡ�����б�
				strSQL = string.Format(strSQL, strParm);    //�滻SQL����еĲ�����
			} 
			catch (Exception ex )
			{
				this.Err = "��ʽ��sqlʱ�����" + ex.Message;
				this.WriteErr();
				return -1;
			}
            return this.ExecNoQuery(strSQL);


		}



		/// <summary>
		/// ���±��濨
		/// </summary>
		/// <param name="cancerAdd">����ʵ��</param>
		/// <returns></returns>
		public int UpdateCancerAddReport(FS.HISFC.DCP.Object.CancerAdd cancerAdd )
		{
			string strSQL = "";
			if (this.Sql.GetSql("DCP.CancerAdd.UpdateByNo", ref strSQL) == -1)
			{
				this.Err = "û���ҵ�DCP.CancerAdd.UpdateByNo�ֶ�";
				return -1;
			}
			try
			{
				string[] strParm = this.myGetCancerAddReportParm(cancerAdd);//ȡ�����б�
				strSQL = string.Format(strSQL, strParm);    //�滻SQL����еĲ�����
			}
			catch (Exception ex)
			{
				this.Err = "��ʽ��sqlʱ�����" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}

		/// <summary>
		/// �������濨ɾ��
		/// </summary>
		/// <param name="ReportNO">���</param>
		/// <returns></returns>
		public int DeleteCancerAdd(string ReportNO)
		{
			string strSQL = "";
			if (this.Sql.GetSql("DCP.CancerAdd.DeleteByNo", ref strSQL) == -1)
			{
				this.Err = "û���ҵ�DCP.CancerAdd.DeleteByNo�ֶ�";
				return -1;
			}
			try
			{
				strSQL = string.Format(strSQL, ReportNO);    //�滻SQL����еĲ�����
			}
			catch (Exception ex)
			{
				this.Err = "��ʽ��sqlʱ�����" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}



		/// <summary>
		/// ��ȡ��������
		/// </summary>
		/// <param name="strSQL">sql���</param>
		/// <returns>��ȡ��������ʵ������</returns>
		private ArrayList myGetCancerAddReport(string strSQL)
		{
			ArrayList al = new ArrayList();
			FS.HISFC.DCP.Object.CancerAdd  cancerAdd;
			this.ProgressBarText = "���ڼ����������濨��Ϣ...";
			this.ProgressBarValue = 0;
			this.ExecQuery(strSQL);
			try
			{
				while (this.Reader.Read())
				{
					        cancerAdd = new FS.HISFC.DCP.Object.CancerAdd();
                    		cancerAdd.REPORT_NO    =this.Reader[0].ToString();
							cancerAdd.MEIDCAL_CARD  = this.Reader[1].ToString();
							cancerAdd.NATION        =this.Reader[2].ToString();            
							cancerAdd.WORK_TYPE     =this.Reader[3].ToString();           
							cancerAdd.MARRIAGE      =this.Reader[4].ToString();          
							cancerAdd.DIAGNOSTIC_ICD     =this.Reader[5].ToString();       
							cancerAdd.REGISTER_PROVINCE   =this.Reader[6].ToString();
							cancerAdd.REGISTER_CITY       =this.Reader[7].ToString();
							cancerAdd.REGISTER_DISTRICT    =this.Reader[8].ToString();
							cancerAdd.REGISTER_STREET       =this.Reader[9].ToString();
							cancerAdd.REGISTER_HOUSENUMBER  =this.Reader[10].ToString();
							cancerAdd.REGISTER_POST         =this.Reader[11].ToString();
							cancerAdd.WORK_PLACE            =this.Reader[12].ToString();
							cancerAdd.CONTACT_PERSON        =this.Reader[13].ToString();
							cancerAdd.RELATIONSHIP          =this.Reader[14].ToString();
							cancerAdd.CONTACT_PERSON_TEL    =this.Reader[15].ToString();
							cancerAdd.CONTACT_PERSON_ADDR   =this.Reader[16].ToString();
							cancerAdd.CLINICAL_T            =this.Reader[17].ToString();
							cancerAdd.CLINICAL_N            =this.Reader[18].ToString();
							cancerAdd.CLINICAL_M            =this.Reader[19].ToString();
							cancerAdd.TERM_TNM              =this.Reader[20].ToString();
							cancerAdd.PATHOLOGY_CHECK       =this.Reader[21].ToString();
							cancerAdd.PATHOLOGY_NO          =this.Reader[22].ToString();
							cancerAdd.PATHOLOGY_TYPE        =this.Reader[23].ToString();
							cancerAdd.PATHOLOGY_DEGREE      =this.Reader[24].ToString();
							cancerAdd.ICD_O                 =this.Reader[25].ToString();
							cancerAdd.TREATMENT             =this.Reader[26].ToString();
							cancerAdd.DEATH_REASON          =this.Reader[27].ToString();
							cancerAdd.OLD_DIAGNOSES         =this.Reader[28].ToString();
							cancerAdd.OLD_DIAGNOSES_DATE = System.Convert .ToDateTime( this.Reader[29].ToString());
                            cancerAdd.District=this.Reader[30].ToString();
					        cancerAdd.HandPhone=this.Reader[31].ToString();


				al.Add(cancerAdd);
				}
			}
			catch (Exception ex)
			{
				this.Err = "��ȡ�������濨��Ϣʱ��ִ��SQL����" + ex.Message;
				this.ErrCode = "-1";
				this.WriteErr();
				return null;
			}
			finally
			{
				this.Reader.Close();
			}
			this.ProgressBarValue = -1;
			return al;
		}


		/// <summary>
		/// ���ݱ�Ų�ѯ�������濨(��ȷ��ѯ)
		/// </summary>
		/// <param name="reportNO"></param>
		/// <returns></returns>
		public FS.HISFC.DCP.Object.CancerAdd GetCancerAddByNO(string reportNO)
		{
			string strSQL = "";
			ArrayList al = new ArrayList();
			if(this.Sql.GetSql("DCP.CancerAdd.Query",ref strSQL) == -1) 
			{
				this.Err = "û���ҵ�DCP.CancerAdd.Query�ֶ�";
				return null;
			}
			string strSQLWhere = "";
			if (this.Sql.GetSql("DCP.CancerAdd.Where", ref strSQLWhere) == -1)
			{
				this.Err = "û���ҵ�DCP.CancerAdd.Where�ֶ�";
				return null;
			}
			strSQLWhere = string.Format(strSQLWhere, reportNO);
			al = this.myGetCancerAddReport(strSQL + strSQLWhere);
			if (al.Count == 1)
			{
				return al[0] as FS.HISFC.DCP.Object.CancerAdd;
			}

			return null;
		}      


	}
}
