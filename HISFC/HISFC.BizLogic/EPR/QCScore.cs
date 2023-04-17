using System;
using System.Collections;
namespace FS.HISFC.BizLogic.EPR
{
	/// <summary>
	/// QCScoreSet ��ժҪ˵����
	/// �����������۱�׼
	/// </summary>
	public class QCScore:FS.FrameWork.Management.Database 
	{
		/// <summary>
		/// �����������۱�׼ҵ���
		/// </summary>
		public QCScore()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		

		#region �ʿ���������
		/// <summary>
		/// ����һ�����۱�׼
		/// </summary>
		/// <returns></returns>
		public int InsertQCScoreSet(FS.HISFC.Models.EPR.QCScore  obj)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.QCScoreSet.InsertQCScoreSet",ref strSql)==-1) return -1;
			try
			{
				strSql = string.Format(strSql,obj.ID ,obj.Name ,obj.Type,obj.Memo, obj.MiniScore, obj.TotalScore, obj.User02);
			}
			catch(Exception ex)
			{
				this.Err = "����Ĳ�����\n"+ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		
		/// <summary>
		/// ɾ��һ�����۱�׼
		/// </summary>
		/// <returns></returns>
		public int DeleteQCScoreSet(string id)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.QCScoreSet.DeleteQCScoreSet",ref strSql)==-1) return -1;
			return this.ExecNoQuery(strSql,id);
		}
		
		/// <summary>
		/// ����һ�����۱�׼
		/// </summary>
		/// <returns></returns>
		public int UpdateQCScoreSet(FS.HISFC.Models.EPR.QCScore obj)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.QCScoreSet.UpdateQCScoreSet",ref strSql)==-1) return -1;
			try
			{
				strSql = string.Format(strSql,obj.ID ,obj.Name, obj.Type ,obj.Memo, obj.MiniScore, obj.TotalScore, obj.User02);
			}
			catch(Exception ex)
			{
				this.Err = "����Ĳ�����\n"+ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}

		/// <summary>
		/// �������۱�׼��Ŀ�ܷ�ֵ
		/// </summary>
		/// <returns></returns>
		public int UpdateQCScoreSetTypeTotalScore(FS.HISFC.Models.EPR.QCScore obj)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.QCScoreSet.UpdateQCScoreSetTypeTotalScore",ref strSql)==-1) return -1;
			try
			{
				strSql = string.Format(strSql, obj.Type, obj.TotalScore);
			}
			catch(Exception ex)
			{
				this.Err = "����Ĳ�����\n"+ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}

		/// <summary>
		/// �������������Ϣ���۱�׼-����ID��ѯ���۱�׼
		/// </summary>
		/// <param name="inpatientNo"></param>
		/// <param name="ID"></param>
		/// <returns></returns>
		public FS.HISFC.Models.EPR.QCScore GetQCScoreSet(string ID)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.QCScoreSet.GetQCScoreSet",ref strSql)==-1) return null;
			strSql = string.Format(strSql,ID);
			ArrayList al =  this.myGetQCScoreSet(strSql);
			if(al ==null || al.Count == 0) return null;
			return al[0] as FS.HISFC.Models.EPR.QCScore;
		}

		/// <summary>
		/// �������������Ϣ���۱�׼-��ѯ�������۱�׼
		/// </summary>
		/// <param name="inpatientNo"></param>
		/// <param name="ID"></param>
		/// <returns></returns>
		public ArrayList GetQCScoreSetList()
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.QCScoreSet.GetQCScoreSetList",ref strSql)==-1) return null;
			return this.myGetQCScoreSet(strSql);
		}

//		/// <summary>
//		/// �������������Ϣ���۱�׼-��ѯ�������۱�׼
//		/// </summary>
//		/// <param name="inpatientNo"></param>
//		/// <param name="ID"></param>
//		/// <returns></returns>
//		public ArrayList GetQCScoreSetTypeList()
//		{
//			string strSql = "";
//			if(this.Sql.GetSql("EPR.QCScoreSet.GetQCScoreSetTypeList",ref strSql)==-1) return null;
//			return this.myGetQCScoreSetTypeList(strSql);
//		}
//		
		#region "˽��"
		private ArrayList myGetQCScoreSet(string sql)
		{
			if(this.ExecQuery(sql)==-1) return null;
			ArrayList al = new ArrayList();
			while(this.Reader.Read())
			{
				FS.HISFC.Models.EPR.QCScore  qcScoreSet = new FS.HISFC.Models.EPR.QCScore ();
				qcScoreSet.ID = this.Reader[0].ToString();
				qcScoreSet.Name = this.Reader[1].ToString();
				qcScoreSet.Type = this.Reader[2].ToString();
				qcScoreSet.Memo = this.Reader[3].ToString();
				qcScoreSet.MiniScore = this.Reader[4].ToString();
				qcScoreSet.TotalScore = this.Reader[5].ToString();
				qcScoreSet.User02 = this.Reader[6].ToString();
				qcScoreSet.User03 = this.Reader[7].ToString();
				al.Add(qcScoreSet);
			}
			this.Reader.Close();
			return al;
		}

//		private ArrayList myGetQCScoreSetTypeList(string sql)
//		{
//			if(this.ExecQuery(sql)==-1) return null;
//			ArrayList al = new ArrayList();
//			while(this.Reader.Read())
//			{
//				FS.FrameWork.Models.NeuObject qcScoreSetType = new FS.FrameWork.Models.NeuObject();
//				qcScoreSetType.ID = this.Reader[0].ToString();
//				qcScoreSetType.User01 = this.Reader[1].ToString();
//				al.Add(qcScoreSetType);
//			}
//			this.Reader.Close();
//			return al;
//		}

		/// <summary>
		/// �������۱�׼�䶯���ݣ�����ִ�и��²��������û���ҵ����Ը��µ����ݣ������һ���¼�¼
		/// </summary>
		/// <param name="qcScoreSet">���۱�׼</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int SetQCScoreSet(FS.HISFC.Models.EPR.QCScore qcScoreSet) 
		{
			int param;
			//ִ�и��²���
			param = UpdateQCScoreSet(qcScoreSet);

			//���û���ҵ����Ը��µ����ݣ������һ���¼�¼
			if (param == 0 || param == -1) 
			{
				param = InsertQCScoreSet(qcScoreSet);
			}

			param = this.UpdateQCScoreSetTypeTotalScore(qcScoreSet);
			return param;
		}
		#endregion
		#endregion

		#region ��������
		/// <summary>
		/// ����һ�����۱�׼
		/// </summary>
		/// <returns></returns>
		public int InsertQCScore(FS.HISFC.Models.EPR.QCScore  obj)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.QC.QCScore.InsertScore",ref strSql)==-1) return -1;
			try
			{
				strSql = string.Format(strSql,obj.ID ,obj.PatientInfo.ID,obj.PatientInfo.Name,obj.Name ,obj.MiniScore,obj.User01,obj.Memo,this.Operator.ID);
			}
			catch(Exception ex)
			{
				this.Err = "����Ĳ�����\n"+ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		
		/// <summary>
		/// ɾ��һ�����۱�׼
		/// </summary>
		/// <returns></returns>
		public int DeleteQCScore(string id,string inpatientNo)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.QC.QCDeleteScore",ref strSql)==-1) return -1;
			return this.ExecNoQuery(strSql,id,inpatientNo);
		}
		
		/// <summary>
		/// ɾ����������
		/// </summary>
		/// <returns></returns>
		public int DeleteQCScore(string inpatientNo)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.QC.QCDeleteScoreByInpatientNo",ref strSql)==-1) return -1;
			return this.ExecNoQuery(strSql,inpatientNo);
		}

		/// <summary>
		/// ����һ�����۱�׼
		/// </summary>
		/// <returns></returns>
		public int UpdateQCScore(FS.HISFC.Models.EPR.QCScore obj)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.QC.QCScore.UpdateScore",ref strSql)==-1) return -1;
			try
			{
				strSql = string.Format(strSql,obj.ID ,obj.PatientInfo.ID,obj.PatientInfo.Name,obj.Name ,obj.MiniScore,obj.User01,obj.Memo,this.Operator.ID);
			}
			catch(Exception ex)
			{
				this.Err = "����Ĳ�����\n"+ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}

//		/// <summary>
//		/// �������������Ϣ���۱�׼-����ID��ѯ���۱�׼
//		/// </summary>
//		/// <param name="ID"></param>
//		/// <returns></returns>
//		public FS.HISFC.Models.EPR.QCScore GetQCScore(string ID)
//		{
//			string strSql = "";
//			if(this.Sql.GetSql("EPR.QCScoreSet.GetQCScoreSet",ref strSql)==-1) return null;
//			strSql = string.Format(strSql,ID);
//			ArrayList al =  this.myGetQCScoreSet(strSql);
//			if(al ==null || al.Count == 0) return null;
//			return al[0] as FS.HISFC.Models.EPR.QCScore;
//		}

		/// <summary>
		/// �������������Ϣ���۱�׼-��ѯ�������۱�׼
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public ArrayList GetQCScoreList(string inpatientNo)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.QC.QCScore.SelectAllScore",ref strSql)==-1) return null;
			try
			{
				strSql = string.Format(strSql,inpatientNo);
			}
			catch{this.Err = "EPR.QC.QCScore.SelectAllScore �������ԣ�";return null;}
			return this.myGetQCScoreSet(strSql);
		}

		#region "˽��"
		private ArrayList myGetQCScoreList(string sql)
		{
			if(this.ExecQuery(sql)==-1) return null;
			ArrayList al = new ArrayList();
			while(this.Reader.Read())
			{
				FS.HISFC.Models.EPR.QCScore  obj = new FS.HISFC.Models.EPR.QCScore ();
				obj.ID = this.Reader[0].ToString();
				obj.PatientInfo.ID = this.Reader[0].ToString();
				obj.PatientInfo.Name = this.Reader[0].ToString();
				obj.Name = this.Reader[0].ToString();
				obj.MiniScore = this.Reader[0].ToString();
				obj.User01 = this.Reader[0].ToString();
				obj.Memo = this.Reader[0].ToString();
				al.Add(obj);
			}
			this.Reader.Close();
			return al;
		}
		#endregion
	
		#endregion


		
	}
}


//namespace FS.HISFC.Models.EPR
//{
//    /// <summary>
//    /// QCScoreSet ��ժҪ˵����
//    /// </summary>
//    public class QCScore:FS.FrameWork.Models.NeuObject 
//    {
//        public QCScore()
//        {
//            //
//            // TODO: �ڴ˴���ӹ��캯���߼�
//            //
//        }
//        private FS.HISFC.Models.RADT.PatientInfo myPatientInfo = new FS.HISFC.Models.RADT.PatientInfo();
//        /// <summary>
//        /// ������Ϣ
//        /// </summary>
//        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
//        {
//            get
//            {
//                return this.myPatientInfo;
//            }
//            set
//            {
//                this.myPatientInfo = value;
//            }
//        }
//        private string type;
//        /// <summary>
//        /// ��Ŀ���
//        /// </summary>
//        public string Type
//        {
//            get
//            {
//                return this.type;
//            }
//            set
//            {
//                this.type = value;
//            }
//        }

//        private string totalScore;
//        /// <summary>
//        /// ��Ŀ����ܷ�ֵ
//        /// </summary>
//        public string TotalScore
//        {
//            get
//            {
//                return this.totalScore;
//            }
//            set
//            {
//                this.totalScore = value;
//            }
//        }
//        private string miniScore;
//        /// <summary>
//        /// ��С��ֵ
//        /// </summary>
//        public string MiniScore
//        {
//            get
//            {
//                return this.miniScore;
//            }
//            set
//            {
//                this.miniScore = value;
//            }
//        }
//        /// <summary>
//        /// ��¡
//        /// </summary>
//        /// <returns></returns>
//        public QCScore Clone()
//        {
//            QCScore score = base.Clone() as QCScore;
//            score.PatientInfo = this.PatientInfo.Clone();
//            return score;
//        }
//    }
//}

