using System;
using FS.HISFC.Models;
using System.Collections;
using FS.FrameWork.Models;

namespace FS.HISFC.BizLogic.Order
{
	/// <summary>
	/// Permission ��ժҪ˵����
	/// ����Ȩ��
	/// </summary>
	public class Permission:FS.FrameWork.Management.Database
	{
		public Permission()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ��ɾ��
		/// <summary>
		/// ����Ȩ�޼�¼
		/// </summary>
		/// <param name="consultation"></param>
		/// <returns></returns>
		public int InsertPermission(FS.HISFC.Models.Order.Consultation consultation)
		{
			#region "�ӿ�"
			//            ,   --סԺ��ˮ��
			//            ,   --��Ȩ���Ҵ���
			//            ,   --��Ȩҽʦ����
			//            ,   --��Ȩҽʦ����
			//            ,   --ҽ��Ȩ��
			//            ,   --������ʼ��
			//            ,   --����������
			//            ,   --��ע
			//            ,   --�û�����
			//             ); --��Ȩʱ��
			#endregion
			string strSql = "";
		
			if (this.Sql.GetCommonSql("Order.Permission.InsertItem.1",ref strSql) == -1)
			{
				this.Err = this.Sql.Err;
				return -1;
			}
			try
			{
				strSql = string.Format(strSql,consultation.InpatientNo,consultation.DeptConsultation.ID,
					consultation.DoctorConsultation.ID,consultation.DoctorConsultation.Name,consultation.Name,consultation.BeginTime.ToString(),
					consultation.EndTime.ToString(),consultation.Memo,this.Operator.ID);
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		/// <summary>
		/// ����Ȩ�޼�¼
		/// </summary>
		/// <param name="consultation"></param>
		/// <returns></returns>
		public int UpdatePermission(FS.HISFC.Models.Order.Consultation consultation)
		{
			#region "�ӿ�"
			//            ,   --��ˮ��
			//            ,   --סԺ��ˮ��
			//            ,   --��Ȩ���Ҵ���
			//            ,   --��Ȩҽʦ����
			//            ,   --��Ȩҽʦ����
			//            ,   --ҽ��Ȩ��
			//            ,   --������ʼ��
			//            ,   --����������
			//            ,   --��ע
			//            ,   --�û�����
			//             ); --��Ȩʱ��
			#endregion
			string strSql = "";
			if (this.Sql.GetCommonSql("Order.Permission.UpdateItem.1",ref strSql) == -1)
			{
				this.Err = this.Sql.Err;
				return -1;
			}
			try
			{
				strSql = string.Format(strSql,consultation.ID,consultation.InpatientNo,consultation.DeptConsultation.ID,
					consultation.DoctorConsultation.ID,consultation.DoctorConsultation.Name,consultation.Name,
					consultation.BeginTime.ToString(),consultation.EndTime.ToString()
					,consultation.Memo,this.Operator.ID);
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		/// <summary>
		/// ɾ��Ȩ�޼�¼
		/// </summary>
		/// <param name="consultationNo"></param>
		/// <returns></returns>
		public int DeletePermission( string consultationNo )
		{
			string strSql = "";
			#region "�ӿ�"
			//���룺0 ConsultaionNo
			//������0
			#endregion
		
			if (this.Sql.GetCommonSql("Order.Permission.DeleteItem.1",ref strSql) == -1)
			{
				this.Err = this.Sql.Err;
				return -1;
			}
			try
			{
				strSql = string.Format(strSql,consultationNo);
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		#endregion

		#region ��ѯ
		/// <summary>
		/// ���ҽ�ƻ���Ȩ���б�
		/// </summary>
		/// <param name="inpatientNo"></param>
		/// <returns></returns>
		public ArrayList QueryPermission(string inpatientNo)
		{
			ArrayList al = new ArrayList();
			string strSql = "";
			//Order.Permission.Select.1
			//���룺0  InpatientNo
			//����:     0 ,   --��ˮ��
			//           1 ,   --סԺ��ˮ��
			//           2 ,   --��Ȩ���Ҵ���
			//           3 ,   --��Ȩҽʦ����
			//           4 ,   --��Ȩҽʦ����
			//           5 ,   --ҽ��Ȩ��
			//           6 ,   --������ʼ��
			//           7,   --����������
			//           8 ,   --��ע
			//           9 ,   --�û�����
			//           10  ); --��Ȩʱ��
			if(this.Sql.GetCommonSql("Order.Permission.Select.1",ref strSql)==0)
			{
				try
				{
					strSql=string.Format(strSql,inpatientNo);
				}
				catch(Exception ex)
				{
					this.Err=ex.Message;
					this.ErrCode=ex.Message;
					this.WriteErr();
					return null;
				}
				if(this.ExecQuery(strSql) == -1) return null;
				while(this.Reader.Read())
				{
					FS.HISFC.Models.Order.Consultation obj = new FS.HISFC.Models.Order.Consultation();
					try
					{
						obj.ID = this.Reader[0].ToString();
						obj.InpatientNo = this.Reader[1].ToString();
						obj.DeptConsultation.ID = this.Reader[2].ToString();
						obj.DoctorConsultation.ID = this.Reader[3].ToString();
						obj.DoctorConsultation.Name = this.Reader[4].ToString();
						obj.Name = this.Reader[5].ToString();
						obj.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6].ToString());
						obj.EndTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[7].ToString());
						obj.Memo = this.Reader[8].ToString();						
						obj.User01 = this.Reader[9].ToString();
						obj.User02 = this.Reader[10].ToString();
					}
					catch{}
					al.Add(obj);
				}
				this.Reader.Close();
			}
			else
			{
				return null;
			}
			return al;
		}

        /// <summary>
        /// ���ҽ�ƻ���Ȩ���б�
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public ArrayList QueryPermissionByDoct(string doctCode)
        {
            ArrayList al = new ArrayList();
            string strSql = "";
            //Order.Permission.Select.1
            //���룺0  doctCode
            //����:     0 ,   --��ˮ��
            //           1 ,   --סԺ��ˮ��
            //           2 ,   --��Ȩ���Ҵ���
            //           3 ,   --��Ȩҽʦ����
            //           4 ,   --��Ȩҽʦ����
            //           5 ,   --ҽ��Ȩ��
            //           6 ,   --������ʼ��
            //           7,   --����������
            //           8 ,   --��ע
            //           9 ,   --�û�����
            //           10  ); --��Ȩʱ��
            if (this.Sql.GetCommonSql("Order.Permission.Select.2", ref strSql) == 0)
            {
                try
                {
                    strSql = string.Format(strSql, doctCode);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.ErrCode = ex.Message;
                    this.WriteErr();
                    return null;
                }
                if (this.ExecQuery(strSql) == -1) return null;
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Order.Consultation obj = new FS.HISFC.Models.Order.Consultation();
                    try
                    {
                        obj.ID = this.Reader[0].ToString();
                        obj.InpatientNo = this.Reader[1].ToString();
                        obj.DeptConsultation.ID = this.Reader[2].ToString();
                        obj.DoctorConsultation.ID = this.Reader[3].ToString();
                        obj.DoctorConsultation.Name = this.Reader[4].ToString();
                        obj.Name = this.Reader[5].ToString();
                        obj.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6].ToString());
                        obj.EndTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[7].ToString());
                        obj.Memo = this.Reader[8].ToString();
                        obj.User01 = this.Reader[9].ToString();
                        obj.User02 = this.Reader[10].ToString();
                    }
                    catch { }
                    al.Add(obj);
                }
                this.Reader.Close();
            }
            else
            {
                return null;
            }
            return al;
        }
		#endregion
	}
}
