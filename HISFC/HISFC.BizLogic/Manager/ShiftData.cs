using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;

namespace FS.HISFC.BizLogic.Manager
{
    /// <summary>
    /// [��������: �����¼������]<br></br>
    /// [�� �� ��: dorian]<br></br>
    /// [����ʱ��: 2007-04]<br></br>
    /// <˵��>
    ///     
    /// </˵��>
    /// </summary>
	public class ShiftData :DataBase
	{
		public ShiftData()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
        }

        #region �������Լ�¼����

        /// <summary>
        /// ��ȡSql��������
        /// </summary>
        /// <param name="shiftProperty">������Լ�¼��</param>
        /// <returns>�ɹ����ز������� ʧ�ܷ���null</returns>
        private string[] GetSqlParamForShiftProperty(FS.HISFC.Models.Base.ShiftProperty shiftProperty)
        {
            string[] strParam = new string[]{
                                                shiftProperty.ReflectClass.ID,
                                                shiftProperty.ReflectClass.Name,                                                
                                                shiftProperty.Property.ID,
                                                shiftProperty.Property.Name,
                                                shiftProperty.PropertyDescription,
                                                FS.FrameWork.Function.NConvert.ToInt32(shiftProperty.IsRecord).ToString(),
                                                shiftProperty.ShiftCause,
                                                shiftProperty.Memo,
                                                shiftProperty.Oper.ID,
                                                shiftProperty.Oper.OperTime.ToString()
                                            };

            return strParam;
        }

        /// <summary>
        /// ִ��sql����ȡ���������Ϣ
        /// </summary>
        /// <param name="strExe">��ִ�е�sql���</param>
        /// <returns>�ɹ�����List���� ʧ�ܷ���null</returns>
        private List<FS.HISFC.Models.Base.ShiftProperty> ExecSqlForShiftProperty(string strExe)
        {
            List<FS.HISFC.Models.Base.ShiftProperty> al = new List<FS.HISFC.Models.Base.ShiftProperty>();
            FS.HISFC.Models.Base.ShiftProperty sf = null;

            if (this.ExecQuery(strExe) == -1)
            {
                this.Err = "ִ��Sql��䷢���쳣" + this.Err;
                return null;
            }

            try
            {             
                while (this.Reader.Read())
                {
                    sf = new FS.HISFC.Models.Base.ShiftProperty();

                    sf.ReflectClass.ID = this.Reader[0].ToString();
                    sf.ReflectClass.Name = this.Reader[1].ToString();
                    sf.Property.ID = this.Reader[2].ToString();
                    sf.Property.Name = this.Reader[3].ToString();
                    sf.PropertyDescription = this.Reader[4].ToString();
                    sf.IsRecord = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[5]);
                    sf.ShiftCause = this.Reader[6].ToString();
                    sf.Memo = this.Reader[7].ToString();
                    sf.Oper.ID = this.Reader[8].ToString();
                    sf.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[9]);

                    al.Add(sf);
                }

                return al;
            }
            catch (Exception ex)
            {
                this.Err = "��Reader�ڶ�ȡ���ݷ����쳣" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// ִ��sql����ȡ��������б���Ϣ
        /// </summary>
        /// <param name="strExe">��ִ�е�sql���</param>
        /// <returns>�ɹ�����List���� ʧ�ܷ���null</returns>
        private List<FS.FrameWork.Models.NeuObject> ExecSqlForShiftPropertyList(string strExe)
        {
            List<FS.FrameWork.Models.NeuObject> al = new List<FS.FrameWork.Models.NeuObject>();
            FS.FrameWork.Models.NeuObject sfList = null;

            if (this.ExecQuery(strExe) == -1)
            {
                this.Err = "ִ��Sql��䷢���쳣" + this.Err;
                return null;
            }

            try
            {              
                while (this.Reader.Read())
                {
                    sfList = new FS.FrameWork.Models.NeuObject();

                    sfList.ID = this.Reader[0].ToString();          //ReflectClass ID
                    sfList.Name = this.Reader[1].ToString();        //ReflectClass Name 

                    al.Add(sfList);
                }

                return al;
            }
            catch (Exception ex)
            {
                this.Err = "��Reader�ڶ�ȡ���ݷ����쳣" + ex.Message;
                return null;
            }
            finally
            {                
                this.Reader.Close();
            }
        }

        /// <summary>
        /// ���ݲ���
        /// </summary>
        /// <param name="sf"></param>
        /// <returns></returns>
        public int InsertShiftProperty(FS.HISFC.Models.Base.ShiftProperty sf)
        {
            string strSQL = "";
            if (this.GetSQL("Manager.ShiftData.InsertShiftProperty", ref strSQL) == -1) return -1;
            string[] strParm;
            try
            {
                strParm = this.GetSqlParamForShiftProperty(sf);  //ȡ�����б�
                strSQL = string.Format( strSQL , strParm );    //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(strSQL, strParm);
        }

        /// <summary>
        /// ���������Ϣɾ��
        /// </summary>
        /// <param name="ReflectClassID">�������������</param>
        /// <param name="propertyID">�������</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1 �޼�¼����0</returns>
        public int DelShiftProperty(string ReflectClassID, string propertyID)
        {
            string strSQL = ""; //����ҩƷ����ɾ��ĳһҩƷ��Ϣ��DELETE���
            if (this.GetSQL("Manager.ShiftProperty.DelShiftProperty.Detail", ref strSQL) == -1)
            {
                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, ReflectClassID, propertyID);
            }
            catch
            {
                this.Err = "����������ԣ�Manager.ShiftProperty.DelShiftProperty.Detail";
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ���������Ϣɾ��
        /// </summary>
        /// <param name="ReflectClassID">�������������</param>
        /// <returns>�ɹ����ش��ڵ���1 ʧ�ܷ���-1 �޼�¼����0</returns>
        public int DelShiftProperty(string ReflectClassID)
        {
            string strSQL = ""; //����ҩƷ����ɾ��ĳһҩƷ��Ϣ��DELETE���
            if (this.GetSQL("Manager.ShiftProperty.DelShiftProperty.Type", ref strSQL) == -1)
            {
                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, ReflectClassID);
            }
            catch
            {
                this.Err = "����������ԣ�Manager.ShiftProperty.DelShiftProperty.Type";
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ��ȡ��������б���Ϣ
        /// </summary>
        /// <returns>�ɹ������б���Ϣ ʧ�ܷ���null</returns>
        public List<FS.FrameWork.Models.NeuObject> QueryShiftPropertyList()
        {
            string strSelect = "";  //���ȫ��ҩƷ��Ϣ��SELECT���

            //ȡSELECT���
            if (this.GetSQL("Manager.ShiftProperty.List", ref strSelect) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Item.Info�ֶ�!";
                return null;
            }

            //����SQL���ȡҩƷ�����鲢��������
            return this.ExecSqlForShiftPropertyList(strSelect);
        }

        /// <summary>
        /// ��ȡ���������Ϣ
        /// </summary>
        /// <param name="ReflectClassID">������</param>
        /// <returns>�ɹ�������Ϣ ʧ�ܷ���null</returns>
        public List<FS.HISFC.Models.Base.ShiftProperty> QueryShiftProperty(string ReflectClassID)
        {
            string strSelect = "";  //���ȫ��ҩƷ��Ϣ��SELECT���
            string strWhere = "";

            //ȡSELECT���
            if (this.GetSQL("Manager.ShiftProperty.Select", ref strSelect) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Item.Info�ֶ�!";
                return null;
            }

            //ȡWHERE�������
            if (this.GetSQL("Manager.ShiftProperty.Where.ReflectClass", ref strWhere) == -1)
            {

                this.Err = "û���ҵ�Manager.ShiftProperty.Where.ReflectClass�ֶ�!";
                return null;
            }
            try
            {
                strSelect = strSelect + strWhere;

                strSelect = string.Format(strSelect, ReflectClassID);
            }
            catch
            {
                this.Err = "SQL������ʼ��ʧ��";
                return null;
            }

            //����SQL���ȡҩƷ�����鲢��������
            return this.ExecSqlForShiftProperty(strSelect);
        }

        #endregion

        #region �����¼

        /// <summary>
		/// ���ñ����Ϣ-��������Ϣ��
		/// insert
		/// </summary>
        /// <param name="itemCode">��Ŀ����</param>
        /// <param name="itemType">��Ŀ����</param>
        /// <param name="originalData">���ǰ����</param>
        /// <param name="newData">���������</param>
        /// <param name="shiftCause">���ԭ��</param>
		/// <returns>0 �ɹ�  -1ʧ��</returns>
        public int SetShiftData(string itemCode,string itemType,FS.FrameWork.Models.NeuObject originalData,FS.FrameWork.Models.NeuObject newData,string shiftCause)
        {
            ///����sql�ַ���
            string strSql = string.Empty;

            if (GetSQL("Manager.ShiftRecord.ShiftData", ref strSql) == -1)
            {
                return -1;
            }

            try
            {
                strSql = string.Format(strSql,
                                      itemCode,
                                      itemType,
                                      originalData.ID,
                                      originalData.Name,
                                      newData.ID,
                                      newData.Name,
                                      shiftCause,
                                      "",
                                      this.Operator.ID
                                      ); 

            }
            catch
            {
                Err = "�����������Manager.ShiftRecord.ShiftData!";
                WriteErr();
                return -1;
            }

            if (ExecNoQuery(strSql) != 1)
            {
                return -1;
            };

            return 1;

        }
        #endregion

        #region ԭ�������ѯ����

        /// <summary>
		/// ��ѯ�����־
		/// </summary>
		/// <param name="beginTime"></param>
		/// <param name="endTime"></param>
		/// <param name="InpatientNo"></param>
		/// <returns></returns>
		public ArrayList GetShiftData(System.DateTime beginTime, System.DateTime endTime,string InpatientNo)
		{
			ArrayList List =null;
			try
			{
				string strSql = "";
				//select clinic_no,happen_no,shift_type,old_data_code,old_data_name,new_data_code,new_data_name,shift_cause,mark from com_shiftdata;
				if (this.GetSQL("Manager.ShiftData.GetShiftData",ref strSql)==-1) return null;
				strSql = string.Format(strSql,beginTime,endTime,InpatientNo);
				this.ExecQuery(strSql);
				List = new ArrayList();
				FS.HISFC.Models.Invalid.CShiftData  info =null;
				while(this.Reader.Read())
				{
					info = new FS.HISFC.Models.Invalid.CShiftData();
					info.ClinicNo =Reader[9].ToString();			//סԺ��ˮ��
					if(Reader[1]!=DBNull.Value)
					{
						info.HappenNo = Convert.ToUInt32(Reader[1]);	//�������
					}
					info.ShitType =Reader[2].ToString();			//�������
					info.OldDataCode =Reader[3].ToString();			//���ǰ���ݱ���
					info.OldDataName =Reader[4].ToString();			//���ǰ��������
					info.NewDataCode =Reader[5].ToString();			//��������ݱ���
					info.NewDataName =Reader[6].ToString();			//��������ݱ���
					info.ShitCause = Reader[7].ToString();			//���ԭ��
					info.Mark = Reader[8].ToString();				//��ע
					info.User01 = Reader[0].ToString();				//��������
					info.User02 = Reader[10].ToString();			//����ʱ��
					List.Add(info);
				}
				this.Reader.Close();
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
			return List;
		}

		/// <summary>
		/// ��ȡ�����Ϣ
		/// </summary>
		/// <returns></returns>
		public DataSet GetShift(string beginTime ,string EndTime ,string InpatientNo,string Type,string DeptCode)
		{
			try
			{
				System.Data.DataSet ds = new DataSet();
				string strSql = "";
				if (this.GetSQL("Manager.ShiftData.GetShiftDataList",ref strSql)==-1) return null;
				strSql = string.Format(strSql,beginTime,EndTime, InpatientNo,Type, DeptCode);
				if (this.ExecQuery(strSql,ref ds) == -1 ) return null;
				return ds;
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
        }

        #endregion
    }
}
