using System;
using System.Collections;
namespace FS.HISFC.BizLogic.EPR
{
	/// <summary>
	/// QC ��ժҪ˵����
	/// ��������
	/// </summary>
	public class QC:FS.FrameWork.Management.Database 
	{
		/// <summary>
		/// ��������ҵ���
		/// </summary>
		public QC()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		

		#region �����������ݲ���
		/// <summary>
		/// ����һ���ļ���Ϣ
		/// </summary>
		/// <returns></returns>
		public int InsertQCData(FS.HISFC.Models.EPR.QC qc)
		{
			if(this.IsHaveSameEMRFile(qc.ID,qc.Index) ==true)return 0;

			string strSql = "";
			if(this.Sql.GetSql("EPR.QC.InsertQCData",ref strSql)==-1) return -1;
			return this.ExecNoQuery(strSql,qc.ID ,qc.PatientInfo.ID ,qc.PatientInfo.Name,qc.PatientInfo.PVisit.PatientLocation.Dept.ID,qc.Index,qc.Name,this.Operator.ID );
		}
		
		/// <summary>
		///  �����ļ�״̬
		/// </summary>
		/// <param name="id"></param>
		/// <param name="State"></param>
		/// <returns></returns>
		public int UpdateQCDataState(string id,int State)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.QC.UpdateQCDataState",ref strSql)==-1) return -1;
			return this.ExecNoQuery(strSql,id,State.ToString(),this.Operator.ID);
		}

		/// <summary>
		///  ���ݵ�ǰ�Ѿ��еĲ����ж��Ƿ���ӵĲ���ҳ�����ظ����
		/// </summary>
		/// <param name="inpatientNo"></param>
		/// <param name="EMRName"></param>
		/// <param name="isOnly"></param>
		/// <returns></returns>
		public bool IsCanAddByQC(string  inpatientNo,string EMRName,bool isOnly)
		{
			if(isOnly==false) return true;
			return this.IsHaveSameEMRName(inpatientNo,EMRName);
		}
		

		/// <summary>
		/// �����Ƿ���Ϸ��ͬ��ָ�����ƵĲ����ļ�
		/// </summary>
		/// <param name="inpatientNo"></param>
		/// <param name="EMRName"></param>
		/// <returns></returns>
		public bool IsHaveSameEMRName(string inpatientNo,string EMRName)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.QC.IsHaveSameEMRName",ref strSql)==-1) return false;
			if(this.ExecQuery(strSql,inpatientNo,EMRName)==-1) return false;
			if(this.Reader.Read()) return true;
			return false;
		}

		/// <summary>
		/// �Ƿ�����ͬ�Ĳ����ļ�
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool IsHaveSameEMRFile(string id,string index)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.QC.IsHaveSameEMRFile",ref strSql)==-1) return false;
			if(this.ExecQuery(strSql,id,index)==-1) return false;
			if(this.Reader.Read()) return true;
			return false;
		}

		/// <summary>
		/// ����ļ��ʿ�����
		/// </summary>
		/// <param name="fileID"></param>
		/// <returns></returns>
		public FS.HISFC.Models.EPR.QC GetQCData(string fileID)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.QC.GetQCData.1",ref strSql)==-1) return null;
			strSql = string.Format(strSql,fileID);
			ArrayList al = this.myGetQCData(strSql);
			if(al == null || al.Count<=0) return null;
			return al[0] as FS.HISFC.Models.EPR.QC;
		}

		/// <summary>
		/// ǩ��
		/// </summary>
		/// <param name="fileID"></param>
		/// <returns></returns>
		public int SignEmrPage(string fileID)
		{
			FS.HISFC.Models.EPR.QC obj = this.GetQCData(fileID);
			if(obj == null) 
			{
				this.Err = "û���ҵ��ļ���¼�����ȱ��没��ҳȻ����ǩ��������";
				return -1;
			}
			string s ="";
			string sTip = "�ļ��Ѿ�{0},���ܽ���ǩ������!";
			if(obj.QCData.State.ToString() =="0") //�½���
			{
				return this.UpdateQCDataState(fileID,1);
			}
			else if(obj.QCData.State.ToString() =="1")
			{
				s ="ǩ��";
			}
			else if(obj.QCData.State.ToString() =="2")
			{
				s ="���";
			}
			else if(obj.QCData.State.ToString() =="3")
			{
				s ="ɾ��";
			}
			sTip = string.Format(sTip,s);
			this.Err = sTip;
			return -1;
		}
        /// <summary>
        /// ǩ������
        /// ��ҪΪ���̼�¼ 
        /// ���� ǩ���ˣ�ǩ�����ڣ��ϼ�ǩ����
        /// </summary>
        /// <param name="id"></param>
        /// <param name="State"></param>
        /// <returns></returns>
        public int SignEmrPage(FS.HISFC.Models.EPR.QC qc)
        {
            string strSql = "";
            if (this.Sql.GetSql("EPR.QC.UpdateQCData", ref strSql) == -1) return -1;
            return this.ExecNoQuery(strSql, qc.ID, qc.Index, qc.QCData.Saver.ID, qc.QCData.Saver.Memo, qc.QCData.Sealer.ID);
        }
        /// <summary>
        /// �Ƿ�ǩ��
        /// </summary>
        /// <param name="fileID"></param>
        /// <returns></returns>
        public bool IsSign(string fileID)
        {
            FS.HISFC.Models.EPR.QC obj = this.GetQCData(fileID);
            if (obj == null)
            {
                return false;
            }
            if (obj.QCData.State.ToString() == "0") //�½���
            {
                return false;
            }
            else
            {
                return true;
            }

        }
		/// <summary>
		/// ���
		/// </summary>
		/// <param name="inpatientNo"></param>
		/// <returns></returns>
		public int Seal(string inpatientNo)
		{
			if(this.IsSeal(inpatientNo) )
			{
				this.Err = "�����Ѿ���棬����ִ�з�������";
				return -1;
			}
			string strSql = "";
			if(this.Sql.GetSql("EPR.QC.Update.2",ref strSql)==-1) return -1;
			return this.ExecNoQuery(strSql,inpatientNo,this.Operator.ID );
		}
		/// <summary>
		/// �Ƿ���
		/// </summary>
		/// <param name="inpatientNo"></param>
		/// <returns></returns>
		public bool IsSeal(string inpatientNo)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.QC.IsSeal",ref strSql)==-1) return false;
			if(this.ExecQuery(strSql,inpatientNo)==-1) return false;
            bool b = this.Reader.Read();
            this.Reader.Close();
            return b;
		}
		/// <summary>
		/// ���--�ָ���ǩ������
		/// </summary>
		/// <param name="inpatientNo"></param>
		/// <returns></returns>
		public int UnSeal(string inpatientNo)
		{
			if(this.IsSeal(inpatientNo)==false )
			{
				this.Err = "����û�з�棬����ִ�н�������";
				return -1;
			}
			string strSql = "";
			if(this.Sql.GetSql("EPR.QC.Update.3",ref strSql)==-1) return -1;
			return this.ExecNoQuery(strSql,inpatientNo);
		}
		
		/// <summary>
		/// �������������Ϣ-��ѯ���õĲ�����Ϣ
		/// </summary>
		/// <param name="inpatientNo"></param>
		/// <param name="EMRName"></param>
		/// <returns></returns>
		public ArrayList GetQCData(string inpatientNo,string EMRName)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.QC.GetQCData.2",ref strSql)==-1) return null;
			strSql = string.Format(strSql,inpatientNo,EMRName);
			return this.myGetQCData(strSql);
		}
		/// <summary>
		/// ���� ���� ��ѯ�ļ��б�
		/// </summary>
		/// <param name="strWhere"></param>
		/// <returns></returns>
		public ArrayList GetQCDataBySqlWhere(string strWhere)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.QC.GetQCData.Select",ref strSql)==-1) return null;
			strSql = strSql +" "+strWhere;
			return this.myGetQCData(strSql);
		}

		
		/// <summary>
		/// ��ýڵ�����
		/// </summary>
		/// <param name="inpatientNo"></param>
		/// <param name="nodeName"></param>
		/// <returns></returns>
		public string GetControlValue(string inpatientNo,string nodeName)
		{
			string strSql ="",sql="";
			if(this.Sql.GetSql("Management.DataFile.GetNodeValueFormDataStore",ref strSql)==-1) return "-1";
			try
			{
				sql = string.Format(strSql,"DataStore_Emr",inpatientNo,nodeName);
			}
			catch(Exception ex)
			{
				this.Err ="GetNodeValueFormDataStore��ֵʱ�����"+ex.Message;
				this.WriteErr();
				return "-1";
			}
			return this.ExecSqlReturnOne(sql);
		}
		#region "˽��"
		private ArrayList myGetQCData(string sql)
		{
			if(this.ExecQuery(sql)==-1) return null;
			ArrayList al = new ArrayList();
			while(this.Reader.Read())
			{
				FS.HISFC.Models.EPR.QC qc = new FS.HISFC.Models.EPR.QC();
				qc.ID = this.Reader[0].ToString();
				qc.PatientInfo.ID = this.Reader[1].ToString();
				qc.PatientInfo.Name = this.Reader[2].ToString();
				qc.PatientInfo.PVisit.PatientLocation.Dept.ID = this.Reader[3].ToString();
				qc.Index = this.Reader[4].ToString();
				qc.Name = this.Reader[5].ToString();
				qc.QCData.State = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[6].ToString());
				
				if(!this.Reader.IsDBNull(7))
						 qc.QCData.Creater.Memo = this.Reader[7].ToString();
				if(!this.Reader.IsDBNull(8))
					qc.QCData.Creater.ID = this.Reader[8].ToString();
				if(!this.Reader.IsDBNull(9))
					qc.QCData.Saver.Memo = this.Reader[9].ToString();
					
				if(!this.Reader.IsDBNull(10))
					qc.QCData.Saver.ID = this.Reader[10].ToString();
				if(!this.Reader.IsDBNull(11))
					qc.QCData.Sealer.Memo = this.Reader[11].ToString();
				if(!this.Reader.IsDBNull(12))
					qc.QCData.Sealer.ID  = this.Reader[12].ToString();
				if(!this.Reader.IsDBNull(13))
					qc.QCData.Deleter.Memo = this.Reader[13].ToString();
				if(!this.Reader.IsDBNull(14))
					qc.QCData.Deleter.ID = this.Reader[14].ToString();
				
				al.Add(qc);
			}
			this.Reader.Close();
			return al;
		}
		#endregion

		#endregion

		#region ����������������
		/// <summary>
		/// ����һ����������������Ϣ
		/// </summary>
		/// <returns></returns>
		public int InsertQCCondition(FS.HISFC.Models.EPR.QCConditions qc)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.QC.InsertQCCondition",ref strSql)==-1) return -1;
			return this.ExecNoQuery(strSql,qc.ID,qc.Name,qc.Memo,qc.Conditions,qc.Acion.Name,this.Operator.ID);
		}
		
		/// <summary>
		///  ������������������Ϣ
		/// </summary>
		/// <param name="qc"></param>
		/// <returns></returns>
		public int UpdateQCCondition(FS.HISFC.Models.EPR.QCConditions qc)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.QC.UpdateQCCondition",ref strSql)==-1) return -1;
			return this.ExecNoQuery(strSql,qc.ID,qc.Name,qc.Memo,qc.Conditions,qc.Acion.Name,this.Operator.ID);
		}
		
		/// <summary>
		/// ɾ��
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public int DeleteQCCondition(string ID)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.QC.DeleteQCCondition",ref strSql)==-1) return -1;
			return this.ExecNoQuery(strSql,ID);
		}
		/// <summary>
		/// ���ȫ������
		/// </summary>
		/// <returns></returns>
		public ArrayList GetQCConditionList()
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.QC.SelectQCCondition.1",ref strSql)==-1) return null;
			return this._execConditionSelect(strSql);
		}
		/// <summary>
		/// ���һ������
		/// </summary>
		/// <returns></returns>
		public FS.HISFC.Models.EPR.QCConditions GetQCCondition(string ID)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.QC.SelectQCCondition.2",ref strSql)==-1) return null;
			strSql = string.Format(strSql,ID);
			ArrayList al = this._execConditionSelect(strSql);
			if(al == null)
			{
				return null;
			}
			else
			{
				return al[0] as FS.HISFC.Models.EPR.QCConditions;
			}
		}
		protected ArrayList _execConditionSelect(string sql)
		{
			if(this.ExecQuery(sql)==-1) return null;
			ArrayList al = new ArrayList();
			while(this.Reader.Read())
			{
				try
				{
					FS.HISFC.Models.EPR.QCConditions qc = new FS.HISFC.Models.EPR.QCConditions();
					qc.ID= this.Reader[0].ToString();
					qc.Name= this.Reader[1].ToString();
					qc.Memo= this.Reader[2].ToString();
					qc.Conditions= this.Reader[3].ToString();
					qc.Acion.Name= this.Reader[4].ToString();
					qc.User01= this.Reader[5].ToString();
					qc.User02 = this.Reader[6].ToString();
					al.Add(qc);
				}
				catch(Exception ex){
					this.Err = ex.Message;
					this.WriteErr();
				return null;}	
			}
			this.Reader.Close();
			return al;
		}
		#endregion

		#region ������������
		/// <summary>
		/// ���������������
		/// </summary>
		/// <returns></returns>
		public ArrayList GetQCName()
		{
			string sql = "EPR.QC.GetQCname";
			if(this.Sql.GetSql(sql,ref sql)==-1) return null;
			if(this.ExecQuery(sql)==-1) return null;
			ArrayList al = new ArrayList();
			while(this.Reader.Read())
			{
				FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
				obj.ID = this.Reader[0].ToString();
				obj.Name = this.Reader[1].ToString();
				obj.Memo = this.Reader[2].ToString();
				try
				{
					obj.User01 = this.Reader[3].ToString();
					obj.User02 = this.Reader[4].ToString();
				}
				catch{}
				al.Add(obj);
			}
			this.Reader.Close();
			return al;
		}
		#endregion
        #region �ʿ������������
        /// <summary>
        /// ���ȫ����������
        /// </summary>
        /// <returns></returns>
        public ArrayList GetQCInputCondition()
        {
            string strSql = "";
            if (this.Sql.GetSql("EPR.QC.SelectQCInputCondition.1", ref strSql) == -1) return null;

            if (this.ExecQuery(strSql) == -1) return null;
            return _getQCInutCondition(strSql);
        }
        /// <summary>
        ///  ���ȫ����������
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public ArrayList GetQCInputCondition(string inpatientNo)
        {
            string strSql = "";
            if (this.Sql.GetSql("EPR.QC.SelectQCInputCondition.2", ref strSql) == -1) return null;

            if (this.ExecQuery(strSql, inpatientNo) == -1) return null;
            return _getQCInutCondition(strSql);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="al"></param>
        /// <returns></returns>
        public int SaveQCInputCondition(ArrayList al)
        {
            foreach (FS.FrameWork.Models.NeuObject obj in al)
            {
                if (this.UpdateQCInputCondition(obj) <= 0)
                {
                    if (this.InsertQCInputCondition(obj) <= 0)
                    {
                        return -1;
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// ����һ����������������Ϣ
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertQCInputCondition(FS.FrameWork.Models.NeuObject obj)
        {
            string strSql = "";
            if (this.Sql.GetSql("EPR.QC.InsertQCInputCondition", ref strSql) == -1) return -1;
            return this.ExecNoQuery(strSql, obj.ID, obj.Name.Trim(), obj.Memo.TrimStart(), obj.User01.Trim(), obj.User02, obj.User03);
        }

        /// <summary>
        /// ������������������Ϣ
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdateQCInputCondition(FS.FrameWork.Models.NeuObject obj)
        {
            string strSql = "";
            if (this.Sql.GetSql("EPR.QC.UpdateQCInputCondition", ref strSql) == -1) return -1;
            return this.ExecNoQuery(strSql, obj.ID, obj.Name.Trim(), obj.Memo.TrimStart(), obj.User01.Trim(), obj.User02, obj.User03);
        }

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        private ArrayList _getQCInutCondition(string strSql)
        {
            ArrayList al = new ArrayList();
            while (this.Reader.Read())
            {
                try
                {
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = this.Reader[0].ToString();
                    obj.Name = this.Reader[1].ToString().Trim();
                    obj.Memo = this.Reader[2].ToString().Trim();
                    obj.User01 = this.Reader[3].ToString();
                    obj.User02 = this.Reader[4].ToString();
                    obj.User03 = this.Reader[5].ToString();
                    al.Add(obj);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                    return null;
                }
            }
            this.Reader.Close();
            return al;
        }
        #endregion

        #region �����ʿ����ݲ���
        /// <summary>
        /// ����һ���ʿ���Ϣ
        /// </summary>
        /// <returns></returns>
        public int InsertQCPatientData(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.EPR.QCConditions qc)
        {
            string strSql = "";
            if (this.Sql.GetSql("EPR.QC.Inpatient.Insert", ref strSql) == -1) return -1;
            return this.ExecNoQuery(strSql,
                patient.ID,
                patient.Name,
                patient.PVisit.PatientLocation.Dept.ID,
                patient.PVisit.PatientLocation.Dept.Name,
                patient.PVisit.AdmittingDoctor.ID,
                patient.PVisit.AdmittingDoctor.Name,
                patient.PVisit.OutTime.ToString(),
                patient.Memo,
                qc.ID,
                qc.Name,
                qc.Memo,
                this.Operator.ID
                );
        }

        /// <summary>
        ///  ������������������Ϣ
        /// </summary>
        /// <param name="qc"></param>
        /// <returns></returns>
        public int UpdateQCPatientData(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.EPR.QCConditions qc)
        {
            if (this.DeleteQCPatientData(patient.ID, qc.ID) == -1) return -1;
            return this.InsertQCPatientData(patient, qc);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int DeleteQCPatientData(string inpatientNo, string qcid)
        {
            string strSql = "";
            if (this.Sql.GetSql("EPR.QC.Inpatient.Delete", ref strSql) == -1) return -1;
            return this.ExecNoQuery(strSql, inpatientNo, qcid);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int QueryQCPatientData(string inpatientNo, ref System.Data.DataSet ds)
        {
            string strSql = "";
            if (this.Sql.GetSql("EPR.QC.Inpatient.Where.1", ref strSql) == -1) return -1;
            strSql = string.Format(strSql, inpatientNo);
            return this._execQueryQCPatientData(strSql, ref ds);
            //<SQL id="EPR.QC.Inpatient.Where.1" Memo="��ѯWhere������������" input="1" output="10"><![CDATA[ 
            //where	   INPATIENT_NO ='{0}'
            //]]></SQL>
            //<SQL id="EPR.QC.Inpatient.Where.2" Memo="��ѯWhere�����������ߵ��ʿ�����" input="1" output="10"><![CDATA[ 
            //where	   INPATIENT_NO ='{0}' and  QC_CODE   ='{1}'
            //]]></SQL>
            //<SQL id="EPR.QC.Inpatient.Where.3" Memo="��ѯWhere������������" input="1" output="10"><![CDATA[ 
            //where	  dept_code ='{0}' 
            //]]></SQL>
        }
        /// <summary>
        /// ��ѯ���ߵ��ʿ�����
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="qccode"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int QueryQCPatientData(string inpatientNo, string qccode, ref System.Data.DataSet ds)
        {
            string strSql = "";
            if (this.Sql.GetSql("EPR.QC.Inpatient.Where.2", ref strSql) == -1) return -1;
            strSql = string.Format(strSql, inpatientNo, qccode);
            return this._execQueryQCPatientData(strSql, ref ds);

        }

        /// <summary>
        /// ��ѯ���ߵ��ʿ�����
        /// </summary>
        /// <param name="deptcode"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int QueryQCPatientDataByDept(string deptcode, ref System.Data.DataSet ds)
        {
            string strSql = "";
            if (this.Sql.GetSql("EPR.QC.Inpatient.Where.3", ref strSql) == -1) return -1;
            strSql = string.Format(strSql, deptcode);
            return this._execQueryQCPatientData(strSql, ref ds);

        }
        /// <summary>
        /// ˽�л����ʿ����ݲ�ѯ����
        /// </summary>
        /// <param name="wheresql"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        protected int _execQueryQCPatientData(string wheresql, ref System.Data.DataSet ds)
        {
            string strSql = "";

            if (this.Sql.GetSql("EPR.QC.Inpatient.Select", ref strSql) == -1) return -1;//��ѯsql

            if (this.ExecQuery(strSql + "\n" + wheresql, ref ds) == -1) return -1;//��ѯ+Where�������в�ѯ

            return 0;
        }


        #endregion
	}
}
