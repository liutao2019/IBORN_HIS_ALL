using System;
using System.Collections;
using FS.HISFC.Models.Preparation;
using FS.FrameWork.Function;
using System.Collections.Generic;

namespace FS.HISFC.BizLogic.Pharmacy
{
    /// <summary>
    /// [��������: �Ƽ�������]<br></br>
    /// [�� �� ��: Dorian]<br></br>
    /// [����ʱ��: 2008]<br></br>
    /// <�޸ļ�¼>
    ///    
    /// </�޸ļ�¼>
    /// </summary>
	public class Preparation:DataBase
	{
		public Preparation()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		
		#region ���ƴ���ά��

		/// <summary>
		/// �����Ƽ�����ʵ���ȡ��������
		/// </summary>
		/// <param name="prescription">�Ƽ�����ʵ��</param>
		/// <returns>�ɹ����ض�Ӧ�Ĳ�������</returns>
		private string[] myGetPrescriptionParam(PrescriptionBase prescription)
		{
			string[] strParm = 
				{
                    ((int)prescription.ItemType).ToString(),//��Ŀ���
					prescription.ID,					//��Ʒ����
					prescription.Name,					//��Ʒ����
					prescription.ProductSpecs,			//��Ʒ���
                    ((int)prescription.MaterialType).ToString(),
					prescription.Material.ID,				//ԭ�ϱ���
					prescription.Material.Name,				//ԭ������
					prescription.Specs,			            //ԭ�Ϲ��
                    prescription.MaterialPackQty.ToString(),
                    prescription.Price.ToString(),          //����
					prescription.NormativeQty.ToString(),	//��׼������
					prescription.NormativeUnit,				//��λ
					prescription.OperEnv.Name,
					prescription.OperEnv.OperTime.ToString(),		//����ʱ��
					prescription.Memo
				};
			return strParm;
		}

		/// <summary>
		/// ִ��Sql����ȡ����ʵ��
		/// </summary>
		/// <param name="strSql">��ִ��Sql���</param>
		/// <returns>�ɹ�����ʵ������ ʧ�ܷ���null �޼�¼���ؿ�����</returns>
        private List<FS.HISFC.Models.Preparation.PrescriptionBase> myGetPrescription(string strSql)
		{
            List<FS.HISFC.Models.Preparation.PrescriptionBase> al = new List<PrescriptionBase>();
            PrescriptionBase prescription;

			if (this.ExecQuery(strSql) == -1)
			{
				this.Err = "ִ��Sql������\n" + strSql + this.Err;
				return null;
			}

			try
			{
				while (this.Reader.Read())
				{
                    prescription = new PrescriptionBase();

                    prescription.ItemType = (FS.HISFC.Models.Base.EnumItemType)(NConvert.ToInt32(this.Reader[0]));
					prescription.ID = this.Reader[1].ToString();			    //��Ʒ����
					prescription.Name = this.Reader[2].ToString();			    //��Ʒ����
					prescription.ProductSpecs = this.Reader[3].ToString();		//��Ʒ���
                    prescription.MaterialType = (EnumMaterialType)(NConvert.ToInt32(this.Reader[4]));
					prescription.Material.ID = this.Reader[5].ToString();		//ԭ�ϱ���
					prescription.Material.Name = this.Reader[6].ToString();		//ԭ������
					prescription.Specs = this.Reader[7].ToString();	            //ԭ�Ϲ��
                    prescription.MaterialPackQty = NConvert.ToDecimal(this.Reader[8]);
                    prescription.Price = NConvert.ToDecimal(this.Reader[9]);    //����
					prescription.NormativeQty = NConvert.ToDecimal(this.Reader[10].ToString());
					prescription.NormativeUnit = this.Reader[11].ToString();
					prescription.OperEnv.Name = this.Reader[12].ToString();
					prescription.OperEnv.OperTime = NConvert.ToDateTime(this.Reader[13].ToString());
					prescription.Memo = this.Reader[14].ToString();

					al.Add(prescription);
				}
			}
			catch (Exception ex)
			{
				this.Err = "��Reader�ڻ�ȡ������Ϣ����" + ex.Message;
				return null;
			}
			finally
			{
				this.Reader.Close();
			}

			return al;
		}

		/// <summary>
		/// ���Ƽ��������ڲ����¼�¼
		/// </summary>
		/// <param name="prescription">�Ƽ�����ʵ��</param>
		/// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int InsertPrescription(PrescriptionBase prescription)
		{
			string strSQL="";
			if(this.GetSQL("Pharmacy.PPR.Prescription.InsertPrescription",ref strSQL) == -1)
				return -1;
			try 
			{
				string[] strParm = this.myGetPrescriptionParam( prescription ); //ȡ�����б�
				strSQL = string.Format(strSQL, strParm);					//�滻SQL����еĲ�����
			}
			catch(Exception ex) 
			{
				this.Err="��ʽ����¼��SQl������ֵʱ����Pharmacy.PPR.Prescription.InsertPrescription" + ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}

		/// <summary>
		/// ����һ�����д���������Ϣ
		/// </summary>
		/// <param name="prescription">�Ƽ�����ʵ��</param>
		/// <returns>�ɹ����ظ������� ʧ�ܷ���-1</returns>
        protected int UpdatePrescription(PrescriptionBase prescription)
		{
			string strSQL="";
			if(this.GetSQL("Pharmacy.PPR.Prescription.UpdatePrescription",ref strSQL) == -1)
				return -1;
			try 
			{
				string[] strParm = this.myGetPrescriptionParam( prescription ); //ȡ�����б�
				strSQL = string.Format(strSQL, strParm);					//�滻SQL����еĲ�����
			}
			catch(Exception ex) 
			{
				this.Err="��ʽ����¼��SQl������ֵʱ����Pharmacy.PPR.Prescription.UpdatePrescription" + ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}

		/// <summary>
		/// ��ִ�и��²��� ���²��ɹ���ִ�в������
		/// </summary>
		/// <param name="prescription">�Ƽ�����ʵ��</param>
		/// <returns>�ɹ����ز�����¼�� ʧ�ܷ���-1</returns>
        public int SetPrescription(PrescriptionBase prescription)
		{
			int parm = this.UpdatePrescription(prescription);
			if (parm < 1)
			{
				parm = this.InsertPrescription(prescription);
			}
			return parm;
		}

		/// <summary>
		/// ɾ�����ƴ�����¼
		/// </summary>
		/// <param name="drugCode">��Ʒ����</param>
        /// <param name="itemType">��Ŀ���</param>
		/// <param name="materialCode">ԭ�ϱ���</param>
		/// <returns>�ɹ�����ɾ������  ʧ�ܷ���-1</returns>
		public int DelPrescription(string drugCode,FS.HISFC.Models.Base.EnumItemType itemType,string materialCode)
		{
			string strSQL=""; 
			if(this.GetSQL("Pharmacy.PPR.Prescription.DelPrescription",ref strSQL) == -1) return -1;
			try 
			{
				strSQL = string.Format(strSQL, drugCode,((int)itemType).ToString(),materialCode);
			}
			catch 
			{
				this.Err="�����������ȷ��Pharmacy.PPR.Prescription.DelPrescription";
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}

		/// <summary>
		/// ɾ��ĳ����Ʒ�����ƴ���
		/// </summary>
		/// <param name="drugCode">��Ʒ����</param>
        /// <param name="itemType">��Ŀ���</param>
		/// <returns>�ɹ�����ɾ����¼��  ʧ�ܷ���-1</returns>
		public int DelPrescription(string drugCode,FS.HISFC.Models.Base.EnumItemType itemType)
		{
			return this.DelPrescription(drugCode,itemType,"AAAA");
		}

		/// <summary>
		/// ��ȡĳ��Ʒ���ƴ�����Ϣ
		/// </summary>
		/// <param name="drugCode">�ɹ�����</param>
        /// <param name="itemType">��Ŀ���</param>
		/// <returns>�ɹ��������ƴ������� ʧ�ܷ���null</returns>
        public List<FS.HISFC.Models.Preparation.PrescriptionBase> QueryPrescription(string drugCode,FS.HISFC.Models.Base.EnumItemType itemType)
		{
			string strSelect = "";
			string strSQL = "";
			if (this.GetSQL("Pharmacy.PPR.Prescription.GetPrescription",ref strSelect) == -1)
				return null;
			if(this.GetSQL("Pharmacy.PPR.Prescription.GetPrescription.Where.1",ref strSQL) == -1)
				return null;
			try 
			{
				strSQL = strSelect + strSQL;
                strSQL = string.Format(strSQL, drugCode, ((int)itemType).ToString());
			}
			catch (Exception ex) 
			{
				this.Err = "��ʽ��SQL���ʱ����Pharmacy.PPR.Prescription.GetPrescription:" + ex.Message;
				return null;
			}
			return this.myGetPrescription(strSQL);
		}

        /// <summary>
        /// ����ԭ����𡢳�Ʒ�����ȡ���ô�����Ϣ
        /// </summary>
        /// <param name="drugCode">��Ʒ����</param>
        /// <param name="itemType">��Ŀ���</param>
        /// <param name="materialType">ԭ�����</param>
        /// <returns>�ɹ��������ô�����Ϣ���� ʧ�ܷ���null</returns>
        public List<FS.HISFC.Models.Preparation.PrescriptionBase> QueryPrescription(string drugCode, FS.HISFC.Models.Base.EnumItemType itemType, EnumMaterialType materialType)
        {
            List<FS.HISFC.Models.Preparation.PrescriptionBase> alList = this.QueryPrescription(drugCode,itemType);
            if (alList == null)
            {
                return null;
            }

            List<FS.HISFC.Models.Preparation.PrescriptionBase> alTypeList = new List<PrescriptionBase>();

            foreach (FS.HISFC.Models.Preparation.PrescriptionBase info in alList)
            {
                if (info.MaterialType == materialType)                
                {
                    alTypeList.Add(info);
                }
            }

            return alTypeList;
        }

        /// <summary>
        /// ��ȡ���ô����б�
        /// </summary>
        /// <returns></returns>
        public List<FS.FrameWork.Models.NeuObject> QueryPrescriptionList(FS.HISFC.Models.Base.EnumItemType itemType)
        {
            string strSelect = "";
            if (this.GetSQL("Pharmacy.PPR.Prescription.GetPrescriptionList", ref strSelect) == -1)
            {
                return null;
            }

            strSelect = string.Format(strSelect, ((int)itemType).ToString());

            List<FS.FrameWork.Models.NeuObject> alList =  new List<FS.FrameWork.Models.NeuObject>();
            if (this.ExecQuery(strSelect) == -1)
            {
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();

                    info.ID = this.Reader[0].ToString();			//��Ʒ����
                    info.Name = this.Reader[1].ToString();			//��Ʒ����
                    info.Memo = this.Reader[2].ToString();		    //��Ʒ���

                    alList.Add(info);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��Reader�ڻ�ȡ������Ϣ����" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return alList;
        }

        /// <summary>
        /// �Ƽ���Ʒ���ƴ�����Ϣ����
        /// </summary>
        /// <param name="baseList"></param>
        /// <returns></returns>
        public int SavePrescription(List<FS.HISFC.Models.Preparation.PrescriptionBase> baseList)
        {
            FS.HISFC.Models.Preparation.PrescriptionBase tempPrescription = baseList[0];

            if (this.DelPrescription(tempPrescription.ID, tempPrescription.ItemType) == -1)
            {
                return -1;
            }

            foreach (FS.HISFC.Models.Preparation.PrescriptionBase info in baseList)
            {
                if (this.InsertPrescription(info) == -1)
                {
                    return -1;
                }
            }

            return 1;
        }

        #region ҩƷ���ƴ�����Ϣ

        /// <summary>
        /// ����ת������Ҳ֪���Ƚ϶��ģ�����û�취�ˡ���
        /// </summary>
        /// <param name="basePrescription"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Preparation.Prescription ConvertBaseToChild(FS.HISFC.Models.Preparation.PrescriptionBase basePrescription)
        {
            FS.HISFC.Models.Preparation.Prescription info = new Prescription();
            info.ItemType = basePrescription.ItemType;
            info.Drug.ID = basePrescription.ID;
            info.Drug.Name = basePrescription.Name;
            info.Drug.Specs = basePrescription.ProductSpecs;
            info.MaterialType = basePrescription.MaterialType;
            info.Material = basePrescription.Material;
            info.Specs = basePrescription.Specs;
            info.MaterialPackQty = basePrescription.MaterialPackQty;
            info.Price = basePrescription.Price;
            info.NormativeQty = basePrescription.NormativeQty;
            info.NormativeUnit = basePrescription.NormativeUnit;
            info.OperEnv = basePrescription.OperEnv;
            info.Memo = basePrescription.Memo;
            
            return info;
        }

        public List<FS.HISFC.Models.Preparation.Prescription> QueryDrugPrescription(string drugCode)
        {
            List<FS.HISFC.Models.Preparation.PrescriptionBase> baseList = this.QueryPrescription(drugCode, FS.HISFC.Models.Base.EnumItemType.Drug);
            if (baseList == null)
            {
                return null;
            }

            List<FS.HISFC.Models.Preparation.Prescription> drugPrescriptionList = new List<Prescription>();

            foreach (FS.HISFC.Models.Preparation.PrescriptionBase info in baseList)
            {
                FS.HISFC.Models.Preparation.Prescription drugPrescription = this.ConvertBaseToChild(info);

                drugPrescriptionList.Add(drugPrescription);
            }

            return drugPrescriptionList;
        }

        public List<FS.HISFC.Models.Preparation.Prescription> QueryDrugPrescription(string drugCode, FS.HISFC.Models.Preparation.EnumMaterialType materialType)
        {
            List<FS.HISFC.Models.Preparation.Prescription> drugPrescriptionList = this.QueryDrugPrescription(drugCode);
            if (drugPrescriptionList == null)
            {
                return null;
            }

            List<FS.HISFC.Models.Preparation.Prescription> alTypeList = new List<Prescription>();

            foreach (FS.HISFC.Models.Preparation.Prescription info in drugPrescriptionList)
            {
                if (info.MaterialType == materialType)
                {
                    alTypeList.Add(info);
                }
            }

            return alTypeList;
        }

        #endregion

        #endregion

        #region �Ƽ�����

        #region ��������ɾ���Ĳ���

        /// <summary>
		/// �����Ƽ�����Ϣ��ȡ��������
		/// </summary>
		/// <param name="preparation">�Ƽ�����Ϣʵ��</param>
		/// <returns>�ɹ����ض�Ӧ�Ĳ�������</returns>
		private string[] myGetPreparationParam(FS.HISFC.Models.Preparation.Preparation preparation)
		{
			
			string[] strParam = {
									preparation.PlanNO,					//�����ƻ����
									preparation.Drug.ID,				//��Ʒ����
									preparation.Drug.Name,				//��Ʒ����
									preparation.Drug.Specs,				//��Ʒ���
									preparation.Drug.PackUnit,			//��װ��λ
									preparation.Drug.PackQty.ToString(),//��װ����
									((int)preparation.State).ToString(),					//״̬
									preparation.PlanQty.ToString(),		//�ƻ���Һ��
									preparation.Unit,					//��Һ����λ
									preparation.BatchNO,				//������Ʒ����
									preparation.PlanEnv.ID,				//�ƻ���
									preparation.PlanEnv.OperTime.ToString(),	//�ƻ�ʱ��
									preparation.ConfectEnv.ID,			//������
									preparation.ConfectEnv.OperTime.ToString(),	//����ʱ��
									preparation.AssayQty.ToString(),	//�ͼ�����
									NConvert.ToInt32(preparation.IsAssayEligible).ToString(),	//�������Ƿ�ϸ� 0 ���ϸ� 1 �ϸ�
									preparation.AssayEnv.ID,				//������
									preparation.AssayEnv.OperTime.ToString(),	//����ʱ��	
									preparation.InputState,				//���״̬ 0 ����� 1 ��ʽ���
									preparation.InputQty.ToString(),	//�������
									preparation.InputEnv.ID,				//�����
									preparation.InputEnv.OperTime.ToString(),	//���ʱ��	
									preparation.CheckResult,			//������
									preparation.CheckOper,				//���Ա
									preparation.Memo,					//��ע	
									NConvert.ToInt32(preparation.IsClear).ToString(),			//�Ƿ��峡 1 �峡 0 δ�峡
                                    preparation.ProcessState,
									preparation.OperEnv.ID,				//����Ա
									preparation.OperEnv.OperTime.ToString(),	//����ʱ��
									preparation.Extend1,				//��չ���
									preparation.Extend2,				//��չ���1
									preparation.Extend3,				//��չ���2
                                    preparation.CostPrice.ToString()
								};
			return strParam;
		}

		/// <summary>
		/// ִ��Sql����ȡ�Ƽ�����Ϣ
		/// </summary>
		/// <param name="strSql">��ִ��Sql���</param>
		/// <returns>�ɹ�����ʵ������ ʧ�ܷ���null �޼�¼���ؿ�����</returns>
		private List<FS.HISFC.Models.Preparation.Preparation> myGetPreparation(string strSql)
		{
            List<FS.HISFC.Models.Preparation.Preparation> al = new List < FS.HISFC.Models.Preparation.Preparation >();
			FS.HISFC.Models.Preparation.Preparation info;

			if (this.ExecQuery(strSql) == -1)
			{
				this.Err = "ִ��Sql������\n" + strSql + this.Err;
				return null;
			}

			try
			{
				while (this.Reader.Read())
				{
					info = new FS.HISFC.Models.Preparation.Preparation();

					info.PlanNO = this.Reader[0].ToString();			//�����ƻ�����
					info.Drug.ID = this.Reader[1].ToString();			//��Ʒ����
					info.Drug.Name = this.Reader[2].ToString();			//��Ʒ����
					info.Drug.Specs = this.Reader[3].ToString();		//��Ʒ���
					info.Drug.PackUnit = this.Reader[4].ToString();		//��װ��λ
					info.Drug.PackQty = NConvert.ToDecimal(this.Reader[5].ToString());	//��װ����
					info.State = (EnumState)(NConvert.ToInt32(this.Reader[6].ToString()));								//״̬
					info.PlanQty = NConvert.ToDecimal(this.Reader[7].ToString());		//�ƻ�����
					info.Unit = this.Reader[8].ToString();								//��λ
					info.BatchNO = this.Reader[9].ToString();							//����
					info.PlanEnv.ID = this.Reader[10].ToString();							//�ƻ���
					info.PlanEnv.OperTime = NConvert.ToDateTime(this.Reader[11].ToString());	//�ƻ�ʱ��
					info.ConfectEnv.ID = this.Reader[12].ToString();						//������
					info.ConfectEnv.OperTime = NConvert.ToDateTime(this.Reader[13].ToString());	//����ʱ��
					info.AssayQty = NConvert.ToDecimal(this.Reader[14].ToString());		//�ͼ�����
					info.IsAssayEligible = NConvert.ToBoolean(this.Reader[15].ToString());//�������Ƿ�ϸ�
					info.AssayEnv.ID = this.Reader[16].ToString();						//������
					info.AssayEnv.OperTime = NConvert.ToDateTime(this.Reader[17].ToString());	//����ʱ��
					info.InputState = this.Reader[18].ToString();						//���״̬
					info.InputQty = NConvert.ToDecimal(this.Reader[19].ToString());		//�������
					info.InputEnv.ID = this.Reader[20].ToString();						//�����
					info.InputEnv.OperTime = NConvert.ToDateTime(this.Reader[21].ToString());	//���ʱ��
					info.CheckResult = this.Reader[22].ToString();						//������
					info.CheckOper = this.Reader[23].ToString();						//���Ա
					info.Memo = this.Reader[24].ToString();
					info.IsClear = NConvert.ToBoolean(this.Reader[25].ToString());		//�Ƿ��峡
                    info.ProcessState = this.Reader[26].ToString();
					info.OperEnv.ID = this.Reader[27].ToString();
					info.OperEnv.OperTime = NConvert.ToDateTime(this.Reader[28].ToString());
					info.Extend1 = this.Reader[29].ToString();
					info.Extend2 = this.Reader[30].ToString();
					info.Extend3 = this.Reader[31].ToString();
                    info.CostPrice = NConvert.ToDecimal(this.Reader[32]);

					al.Add(info);
				}
			}
			catch (Exception ex)
			{
				this.Err = "��Reader�ڻ�ȡ�Ƽ�����Ϣ��Ϣ����" + ex.Message;
				return null;
			}
			finally
			{
				this.Reader.Close();
			}

			return al;
		}

		/// <summary>
		/// ���Ƽ������ڲ����¼�¼
		/// </summary>
		/// <param name="preparation">�Ƽ���ʵ��</param>
		/// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
		protected int InsertPreparation(FS.HISFC.Models.Preparation.Preparation preparation)
	
		{
			string strSQL="";
			if(this.GetSQL("Pharmacy.PPR.Preparation.InsertPreparation",ref strSQL) == -1) return -1;
			try 
			{
				string[] strParm = this.myGetPreparationParam( preparation ); //ȡ�����б�
				strSQL = string.Format(strSQL, strParm);				//�滻SQL����еĲ�����
			}
			catch(Exception ex) 
			{
				this.Err="��ʽ����¼��SQl������ֵʱ����Pharmacy.PPR.Preparation.InsertPreparation" + ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}
		
        /// <summary>
		/// ����һ�������Ƽ�������Ϣ
		/// </summary>
		/// <param name="preparation">�Ƽ���ʵ��</param>
		/// <returns>�ɹ����ظ������� ʧ�ܷ���-1</returns>
		protected int UpdatePreparation(FS.HISFC.Models.Preparation.Preparation preparation)
		
		{
			string strSQL="";
			if(this.GetSQL("Pharmacy.PPR.Preparation.UpdatePreparation",ref strSQL) == -1) return -1;
			try 
			{
				string[] strParm = this.myGetPreparationParam( preparation ); //ȡ�����б�
				strSQL = string.Format(strSQL, strParm);					//�滻SQL����еĲ�����
			}
			catch(Exception ex) 
			{
				this.Err="��ʽ����¼��SQl������ֵʱ����Pharmacy.PPR.Preparation.UpdatePreparation" + ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}	
		
        /// <summary>
		/// ɾ���Ƽ���¼
		/// </summary>
		/// <param name="planNo">�����ƻ�����</param>
		/// <param name="drugCode">��Ʒ����</param>
		/// <returns>�ɹ�����ɾ������  ʧ�ܷ���-1</returns>
		public int DelPreparation(string planNo,string drugCode)
		
		{
			string strSQL=""; 
			if(this.GetSQL("Pharmacy.PPR.Preparation.DelPreparation",ref strSQL) == -1) return -1;
			try 
			{
				strSQL = string.Format(strSQL, planNo,drugCode);
			}
			catch 
			{
				this.Err="�����������ȷ��Pharmacy.PPR.Preparation.DelPreparation";
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}
		
        /// <summary>
		/// ɾ���Ƽ���¼
		/// </summary>
		/// <param name="planNo">�����ƻ�����</param>
		/// <returns>�ɹ�����ɾ������  ʧ�ܷ���-1</returns>
		public int DelPreparation(string planNo)
		{
			return this.DelPreparation(planNo,"AAAA");
        }

        #endregion
		
		/// <summary>
		/// ��ȡ�Ƽ���Ϣ
		/// </summary>
		/// <param name="planNo">�����ƻ�����</param>
		/// <param name="drugCode">��Ʒ����</param>
        /// <param name="stateCollection">����״̬ 0 �ƻ� 1 ����  2 ���Ʒ��װ 3 ���Ʒ���� 4 ��Ʒ���װ 5 ��Ʒ���� 6 ��Ʒ���</param>
		/// <returns>�ɹ����ػ�ȡ��Ϣ���� ʧ�ܷ���null</returns>
        public List<FS.HISFC.Models.Preparation.Preparation> QueryPreparation(string planNo, string drugCode, params FS.HISFC.Models.Preparation.EnumState[] stateCollection)
		{
			string strSelect = "";
			string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Preparation.GetPreparation", ref strSelect) == -1)
            {
                return null;
            }
            if (this.GetSQL("Pharmacy.PPR.Where.DrugState", ref strSQL) == -1)
            {
                return null;
            }

            string states = "";
            foreach (EnumState state in stateCollection)
            {
                states = states + ((int)state).ToString() + "','";
            }

			try 
			{
				strSQL = strSelect + strSQL;
                strSQL = string.Format(strSQL, planNo, states, drugCode);
			}
			catch (Exception ex) 
			{
				this.Err = "��ʽ��SQL���ʱ����Pharmacy.PPR.Preparation.GetPreparation:" + ex.Message;
				return null;
			}
			return this.myGetPreparation(strSQL);
		}
		
        /// <summary>
		/// ��ȡ�Ƽ���Ϣ
		/// </summary>
		/// <param name="planNo">�����ƻ�����</param>
        /// <param name="stateCollection">����״̬ 0 �ƻ� 1 ���� 2 ���Ʒ��װ 3 ���Ʒ���� 4 ��Ʒ���װ 5 ��Ʒ���� 6 ��Ʒ���</param>
		/// <returns>�ɹ����ػ�ȡ��Ϣ���� ʧ�ܷ���null</returns>
        public List<FS.HISFC.Models.Preparation.Preparation> QueryPreparation(string planNo, params FS.HISFC.Models.Preparation.EnumState[] stateCollection)
		{
            return this.QueryPreparation(planNo, "AAAA", stateCollection);
		}
		
        /// <summary>
		/// ��ȡ�Ƽ���Ϣ
		/// </summary>
		/// <param name="state">����״̬ 0 �ƻ� 1 ���� 2 ���Ʒ��װ 3 ���Ʒ���� 4 ��Ʒ���װ 5 ��Ʒ���� 6 ��Ʒ���</param>
		/// <returns>�ɹ����ػ�ȡ��Ϣ���� ʧ�ܷ���null</returns>
        public List<FS.HISFC.Models.Preparation.Preparation> QueryPreparation(FS.HISFC.Models.Preparation.EnumState state)
		
		{
			string strSelect = "";
			string strSQL = "";
			if (this.GetSQL("Pharmacy.PPR.Preparation.GetPreparation",ref strSelect) == -1)
				return null;
			if(this.GetSQL("Pharmacy.PPR.Preparation.Where.State",ref strSQL) == -1)
				return null;
			try 
			{
				strSQL = strSelect + strSQL;
				strSQL = string.Format(strSQL,((int)state).ToString());
			}
			catch (Exception ex) 
			{
				this.Err = "��ʽ��SQL���ʱ����Pharmacy.PPR.Preparation.GetPreparation:" + ex.Message;
				return null;
			}
			return this.myGetPreparation(strSQL);
		}

        /// <summary>
        /// ��ִ�и��²��� ���²��ɹ���ִ�в������
        /// </summary>
        /// <param name="preparation">�Ƽ���ʵ��</param>
        /// <returns>�ɹ����ز�����¼�� ʧ�ܷ���-1</returns>
        public int PreparationPlan(FS.HISFC.Models.Preparation.Preparation preparation)
        {
            int parm = this.UpdatePreparation(preparation);
            if (parm < 1)
            {
                parm = this.InsertPreparation(preparation);
            }
            return parm;
        }

        /// <summary>
        /// �����Ƽ����üƻ�
        /// </summary>
        /// <param name="info">�Ƽ����üƻ�����Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int UpdatePreparationConfect(FS.HISFC.Models.Preparation.Preparation info)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Preparation.UpdatePreparationConfect", ref strSQL) == -1)
            {
                this.Err = "�Ҳ���SQL��䣡Pharmacy.PPR.Preparation.UpdatePreparationConfect";
                return -1;
            }
            try
            {
                //ȡ�����б�
                string[] strParm = {
									   info.PlanNO, 
									   info.Drug.ID,                                        
									   info.OperEnv.ID,					    //����Ա
									   info.ConfectEnv.OperTime.ToString(),		//����ʱ��
									   NConvert.ToInt32(info.IsClear).ToString()
								   };
                strSQL = string.Format(strSQL, strParm);              //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "�����Ƽ������SQl������ֵ����" + ex.Message;
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// �����Ƽ����ü���
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdatePreparationAssay(FS.HISFC.Models.Preparation.Preparation info)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Preparation.UpdatePreparationAssay", ref strSQL) == -1)
            {
                this.Err = "�Ҳ���SQL��䣡Pharmacy.PPR.Preparation.UpdatePreparationAssay";
                return -1;
            }
            try
            {
                //ȡ�����б�
                string[] strParm = {
									   info.PlanNO, 
									   info.Drug.ID, 
                                       info.AssayQty.ToString(),
                                       NConvert.ToInt32(info.IsAssayEligible).ToString(),
									   info.OperEnv.ID,					    //����Ա
									   info.OperEnv.OperTime.ToString(),		//����ʱ��
									   NConvert.ToInt32(info.IsClear).ToString()
								   };
                strSQL = string.Format(strSQL, strParm);              //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "�����Ƽ������SQl������ֵ����" + ex.Message;
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// �Ƽ���Ʒ������
        /// </summary>
        /// <param name="preparation">�Ƽ���ʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int UpdatePreparationInput(FS.HISFC.Models.Preparation.Preparation preparation)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Preparation.UpdatePreparationInput", ref strSQL) == -1)
            {
                this.Err = "�Ҳ���SQL��䣡Pharmacy.PPR.Preparation.UpdatePreparationInput";
                return -1;
            }
            try
            {
                //ȡ�����б�
                string[] strParm = {
									   preparation.PlanNO, 
									   preparation.Drug.ID,
									   preparation.InputState,				//��Ʒ���״̬ 0 ����� 1 ��ʽ���
									   preparation.InputQty.ToString(),		//�������
									   preparation.InputEnv.ID,				//��������
									   preparation.InputEnv.OperTime.ToString(),	//������ʱ��
									   preparation.CheckResult,				//������
									   preparation.CheckOper,				//���Ա
									   preparation.Extend1,					//��չ�ֶ�  (���ʱ�洢����ƽ�� 0 ���ϸ� 1 �ϸ�)
									   preparation.Extend2,				//��չ�ֶ�1 (���ʱ�洢�����ʿ���� 0 ���ϸ� 1 �ϸ�)
									   NConvert.ToInt32(preparation.IsClear).ToString(),
                                       preparation.CostPrice.ToString()
								   };
                strSQL = string.Format(strSQL, strParm);					//�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "�����Ƽ������SQl������ֵ����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// �Ƽ�״̬����
        /// </summary>
        /// <param name="preparation">�Ƽ���Ϣ</param>
        /// <param name="saveState">���ݱ���</param>
        /// <param name="oldStateCollection">����ĸ���ǰ״̬</param>
        /// <returns>�ɹ����ظ���Ӱ����Ŀ����ʧ�ܷ��أ�1</returns>
        public int UpdatePreparationState(FS.HISFC.Models.Preparation.Preparation preparation, EnumState saveState,params EnumState[] oldStateCollection)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Preparation.UpdatePreparationState", ref strSQL) == -1)
            {
                this.Err = "�Ҳ���SQL��䣡Pharmacy.PPR.Preparation.UpdatePreparationState";
                return -1;
            }
            string oldStates = "";
            foreach (EnumState state in oldStateCollection)
            {
                oldStates = oldStates + ((int)state).ToString() + "','";
            }
            try
            {
                //ȡ�����б�
                string[] strParm = {
									   preparation.PlanNO, 
									   preparation.Drug.ID,
                                       oldStates,
                                       ((int)saveState).ToString(),                                       
									   this.Operator.ID						//����Ա
								   };
                strSQL = string.Format(strSQL, strParm);					//�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "�����Ƽ������SQl������ֵ����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        #endregion

        #region �Ƽ�����

        #region ��������ɾ���Ĳ���

        /// <summary>
        /// �����Ƽ�������Ϣ��ȡ��������
        /// </summary>
        /// <param name="expand">�Ƽ�������Ϣʵ��</param>
        /// <returns>�ɹ����ض�Ӧ�Ĳ�������</returns>
        private string[] myGetExpandParam(FS.HISFC.Models.Preparation.Expand expand)
        {

            string[] strParam = {
									expand.PlanNO,					                    //�����ƻ����
									expand.Prescription.Drug.ID,				        //��Ʒ����
									expand.Prescription.Drug.Name,				        //��Ʒ����
									expand.Prescription.Drug.Specs,				        //��Ʒ���
                                    ((int)expand.Prescription.MaterialType).ToString(), //����ԭ������
                                    expand.Prescription.Material.ID,                    //ԭ�ϱ���
                                    expand.Prescription.Material.Name,                  //ԭ������
                                    expand.Prescription.Specs,                          //ԭ�Ϲ��
                                    expand.Prescription.Price.ToString(),               //ԭ�ϼ۸�
                                    expand.Prescription.NormativeQty.ToString(),        //��׼������
                                    expand.Prescription.NormativeUnit,                  //��λ
                                    expand.PlanQty.ToString(),                          //�ƻ���Һ��
                                    expand.PlanExpand.ToString(),                       //����������
                                    expand.StoreQty.ToString(),                         //�����
                                    expand.FacutalExpand.ToString(),                    //ʵ�������� 
                                    NConvert.ToInt32(expand.ExecOutput).ToString(),     //�Ƿ���ִ�пۿ�                   
									expand.Memo,					                    //��ע	
									expand.Prescription.OperEnv.Name,				    //����Ա
									expand.Prescription.OperEnv.OperTime.ToString(), 	//����ʱ��
                                    expand.Prescription.MaterialPackQty.ToString()
								};
            return strParam;
        }

        /// <summary>
        /// ִ��Sql����ȡ�Ƽ�������Ϣ
        /// </summary>
        /// <param name="strSql">��ִ��Sql���</param>
        /// <returns>�ɹ�����ʵ������ ʧ�ܷ���null �޼�¼���ؿ�����</returns>
        private List<FS.HISFC.Models.Preparation.Expand> myGetExpandList(string strSql)
        {
            List<FS.HISFC.Models.Preparation.Expand> al = new List<FS.HISFC.Models.Preparation.Expand>();
            FS.HISFC.Models.Preparation.Expand info;

            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "ִ��Sql������\n" + strSql + this.Err;
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.Preparation.Expand();

                    info.PlanNO = this.Reader[0].ToString();			//�����ƻ�����
                    info.Prescription.Drug.ID = this.Reader[1].ToString();			//��Ʒ����
                    info.Prescription.Drug.Name = this.Reader[2].ToString();			//��Ʒ����
                    info.Prescription.Drug.Specs = this.Reader[3].ToString();		//��Ʒ���
                    info.Prescription.MaterialType = ((EnumMaterialType)(NConvert.ToInt32(this.Reader[4]))); //����ԭ������
                    info.Prescription.Material.ID = this.Reader[5].ToString();                  //ԭ�ϱ���
                    info.Prescription.Material.Name = this.Reader[6].ToString();               //ԭ������
                    info.Prescription.Specs = this.Reader[7].ToString();                          //ԭ�Ϲ��
                    info.Prescription.Price = NConvert.ToDecimal(this.Reader[8]);              //ԭ�ϼ۸�
                    info.Prescription.NormativeQty = NConvert.ToDecimal(this.Reader[9]);       //��׼������
                    info.Prescription.NormativeUnit = this.Reader[10].ToString();                 //��λ
                    info.PlanQty = NConvert.ToDecimal(this.Reader[11]);                                     //�ƻ���Һ��
                    info.PlanExpand = NConvert.ToDecimal(this.Reader[12]);                                  //����������
                    info.StoreQty = NConvert.ToDecimal(this.Reader[13]);                                   //�����
                    info.FacutalExpand = NConvert.ToDecimal(this.Reader[14]);                              //ʵ��������   
                    info.ExecOutput = NConvert.ToBoolean(this.Reader[15]);                  //�Ƿ���ִ�пۿ� 1 ��ִ�� 0 δִ�� 
                    info.Memo = this.Reader[16].ToString();
                    info.Prescription.OperEnv.Name = this.Reader[17].ToString();
                    info.Prescription.OperEnv.OperTime = NConvert.ToDateTime(this.Reader[18].ToString());
                    info.Prescription.MaterialPackQty = NConvert.ToDecimal(this.Reader[19]);

                    al.Add(info);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��Reader�ڻ�ȡ�Ƽ�����Ϣ��Ϣ����" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// ���Ƽ����ı��ڲ����¼�¼
        /// </summary>
        /// <param name="expand">�Ƽ�����ʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int InsertExpand(FS.HISFC.Models.Preparation.Expand expand)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Preparation.InsertExpand", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = this.myGetExpandParam(expand); //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);				//�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ����¼��SQl������ֵʱ����Pharmacy.PPR.Preparation.InsertExpand" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ����һ�������Ƽ����ı���Ϣ
        /// </summary>
        /// <param name="expand">�Ƽ�����ʵ��</param>
        /// <returns>�ɹ����ظ������� ʧ�ܷ���-1</returns>
        protected int UpdateExpand(FS.HISFC.Models.Preparation.Expand expand)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Preparation.UpdateExpand", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = this.myGetExpandParam(expand); //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);					//�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ����¼��SQl������ֵʱ����Pharmacy.PPR.Preparation.UpdateExpand" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ɾ���Ƽ����ļ�¼
        /// </summary>
        /// <param name="planNo">�����ƻ�����</param>
        /// <param name="productCode">��Ʒ����</param>
        /// <param name="type">ԭ�����</param>
        /// <param name="materialCode">ԭ�ϱ���</param>
        /// <returns>�ɹ�����ɾ������  ʧ�ܷ���-1</returns>
        public int DelExpand(string planNo, string productCode,FS.HISFC.Models.Preparation.EnumMaterialType type,string materialCode)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Preparation.DelExpand", ref strSQL) == -1) return -1;
            try
            {
                strSQL = string.Format(strSQL, planNo, productCode, ((int)type).ToString(), materialCode);
            }
            catch
            {
                this.Err = "�����������ȷ��Pharmacy.PPR.Preparation.DelExpand";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        #endregion

        /// <summary>
        /// �����Ƽ�������Ϣ
        /// </summary>
        /// <param name="expand">�Ƽ�������Ϣ</param>
        /// <returns>�ɹ�����1  ʧ�ܷ��أ�1</returns>
        public int SetExpand(FS.HISFC.Models.Preparation.Expand expand)
        {
            int param = this.UpdateExpand(expand);
            if (param == 0)
            {
                return this.InsertExpand(expand);
            }

            return param;
        }

        /// <summary>
        /// ���ݵ��ݺš���Ʒ�����ȡ��Ʒ������Ϣ
        /// </summary>
        /// <param name="listCode">���ݺ�</param>
        /// <param name="productCode">��Ʒ����</param>
        /// <returns>�ɹ����س�Ʒ������Ϣ ʧ�ܷ���null</returns>
        public List<FS.HISFC.Models.Preparation.Expand> QueryExpand(string listCode, string productCode)
        {
            string strSelect = "";
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Prescription.GetExpand", ref strSelect) == -1)
            {
                return null;
            }
            if (this.GetSQL("Pharmacy.PPR.Prescription.GetExpand.Where", ref strSQL) == -1)
            {
                return null;
            }
            try
            {
                strSQL = strSelect + strSQL;
                strSQL = string.Format(strSQL, listCode, productCode);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Pharmacy.PPR.Prescription.GetPrescription:" + ex.Message;
                return null;
            }
            return  this.myGetExpandList(strSQL);
        }

        /// <summary>
        /// �Ƽ�ԭ��������Ϣ��ȡ
        /// </summary>
        /// <param name="info">�Ƽ�ԭ����Ϣ</param>
        /// <returns>�ɹ�����������Ϣ���ϣ�ʧ�ܷ���null</returns>
        public List<FS.HISFC.Models.Preparation.Expand> QueryExpand(FS.HISFC.Models.Preparation.Preparation info, FS.FrameWork.Models.NeuObject stockDept)
        {
            //{8840008D-2FEA-4471-B404-B05E25832120}  ��������Ϣ������ʱ����ȡ���
            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new Item();
            if (this.Trans != null)
            {
                itemManager.SetTrans(this.Trans);
            }

            List<FS.HISFC.Models.Preparation.Expand> expandList = this.QueryExpand(info.PlanNO, info.Drug.ID);
            if (expandList == null)
            {
                return null;
            }
            else if (expandList.Count == 0)
            {
                List<FS.HISFC.Models.Preparation.Prescription> prescriptionList = this.QueryDrugPrescription(info.Drug.ID);
                if (prescriptionList == null)
                {
                    return null;
                }
                if (prescriptionList.Count == 0)
                {
                    this.Err = "δ�����Ƽ����ô���";
                    return null;
                }

                FS.HISFC.Models.Preparation.Expand expand = null;
                foreach (FS.HISFC.Models.Preparation.Prescription prescription in prescriptionList)
                {
                    expand = new Expand();

                    expand.Prescription = prescription;
                    expand.PlanNO = info.PlanNO;
                    expand.PlanExpand = expand.Prescription.NormativeQty * info.PlanQty;
                    expand.PlanQty = info.PlanQty;

                    //{8840008D-2FEA-4471-B404-B05E25832120}  ��������Ϣ������ʱ����ȡ���
                    if (stockDept != null)
                    {
                        decimal storeQty = 0;
                        if (itemManager.GetStorageNum(stockDept.ID, expand.Prescription.Material.ID, out storeQty) == -1)
                        {
                            this.Err = "����ԭ�Ͽ�淢������" + itemManager.Err;
                            return null;
                        }
                        expand.StoreQty = storeQty;
                    }
                    //{8840008D-2FEA-4471-B404-B05E25832120}  ��ȡ���

                    //{E261A3CB-0A68-4a9e-99A0-4A6ED1ACFA4B}
                    expand.FacutalExpand = expand.PlanExpand;

                    expandList.Add(expand);
                }
            }

            return expandList;

        }

        /// <summary>
        /// �Ƽ�ԭ��������Ϣ��ȡ
        /// </summary>
        /// <param name="info">�Ƽ�ԭ����Ϣ</param>
        /// <returns>�ɹ�����������Ϣ���ϣ�ʧ�ܷ���null</returns>
        public List<FS.HISFC.Models.Preparation.Expand> QueryExpand(FS.HISFC.Models.Supply.Product info)
        {
            List<FS.HISFC.Models.Preparation.Expand> expandList = this.QueryExpand(info.ProductiveListNO, info.UnDrug.ID);
            if (expandList == null)
            {
                return null;
            }
            else if (expandList.Count == 0)
            {
                List<FS.HISFC.Models.Preparation.PrescriptionBase> prescriptionList = this.QueryPrescription(info.UnDrug.ID, FS.HISFC.Models.Base.EnumItemType.UnDrug);
                if (prescriptionList == null)
                {
                    return null;
                }
                if (prescriptionList.Count == 0)
                {
                    this.Err = "δ�����Ƽ����ô���";
                    return null;
                }

                FS.HISFC.Models.Preparation.Expand expand = null;
                foreach (FS.HISFC.Models.Preparation.PrescriptionBase prescription in prescriptionList)
                {
                    expand = new Expand();

                    expand.Prescription = prescription as Prescription;
                    expand.PlanNO = info.ProductiveListNO;
                    expand.PlanExpand = expand.Prescription.NormativeQty * info.PlanQty;
                    expand.PlanQty = info.PlanQty;
                    expand.StoreQty = 0;
                    expand.FacutalExpand = 0;

                    expandList.Add(expand);
                }
            }

            return expandList;

        }

        #endregion

        #region ����������������

        /// <summary>
        /// ��ȡSql���ִ�в�������
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private string[] GetProcessParam(FS.HISFC.Models.Preparation.Process p)
        {
            string[] param = new string[] { 
                                            p.Preparation.PlanNO,
                                            p.Preparation.Drug.ID,
                                            p.Preparation.Drug.Name,
                                            p.Preparation.Drug.Specs,
                                            p.Preparation.Drug.PackUnit,
                                            p.Preparation.Drug.PackQty.ToString(),
                                            ((int)p.Preparation.State).ToString(),
                                            p.ProcessItem.ID,
                                            p.ProcessItem.Name,
                                            p.ResultQty.ToString(),
                                            p.ResultStr,
                                            p.Oper.ID,
                                            p.Oper.OperTime.ToString(),
                                            p.Extend,
                                            p.ItemType,
                                            NConvert.ToInt32(p.IsEligibility).ToString()
                                          };

            return param;
        }

        /// <summary>
        /// ִ��Sql����ȡ������������
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        private List<FS.HISFC.Models.Preparation.Process> ExecSqlForPreparationProcess(string strSql)
        {
            List<FS.HISFC.Models.Preparation.Process> al = new List<FS.HISFC.Models.Preparation.Process>();
            FS.HISFC.Models.Preparation.Process info;

            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "ִ��Sql������\n" + strSql + this.Err;
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.Preparation.Process();

                    info.Preparation.PlanNO = this.Reader[0].ToString();			//�����ƻ�����
                    info.Preparation.Drug.ID = this.Reader[1].ToString();			//��Ʒ����
                    info.Preparation.Drug.Name = this.Reader[2].ToString();			//��Ʒ����
                    info.Preparation.Drug.Specs = this.Reader[3].ToString();		//��Ʒ���
                    info.Preparation.Drug.PackUnit = this.Reader[4].ToString();
                    info.Preparation.Drug.PackQty = NConvert.ToDecimal(this.Reader[5]);
                    info.Preparation.State = (EnumState)(NConvert.ToInt32(this.Reader[6]));
                    info.ProcessItem.ID = this.Reader[7].ToString();
                    info.ProcessItem.Name = this.Reader[8].ToString();

                    info.ResultQty = NConvert.ToDecimal(this.Reader[9].ToString());

                    info.ResultStr = this.Reader[10].ToString();
                    info.Oper.ID = this.Reader[11].ToString();
                    info.Oper.OperTime = NConvert.ToDateTime(this.Reader[12].ToString());
                    info.Extend = this.Reader[13].ToString();

                    info.ItemType = this.Reader[14].ToString();
                    info.IsEligibility = NConvert.ToBoolean(this.Reader[15]);

                    al.Add(info);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��Reader�ڻ�ȡ�Ƽ�����Ϣ��Ϣ����" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// ��ȡ�������̼�¼��Ϣ
        /// </summary>
        /// <param name="planNO">�����ƻ�����</param>
        /// <param name="drugCode">��Ʒ����</param>
        /// <param name="state">����״̬</param>
        /// <returns>�ɹ����ع������̼�¼��Ϣ ʧ�ܷ���null</returns>
        public List<FS.HISFC.Models.Preparation.Process> QueryProcess(string planNO, string drugCode, string state)
        {
            string strSelect = "";
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Preparation.GetProcess", ref strSelect) == -1)
            {
                return null;
            }
            if (this.GetSQL("Pharmacy.PPR.Preparation.GetProcess.Where", ref strSQL) == -1)
            {
                return null;
            }

            try
            {
                strSQL = strSelect + strSQL;
                strSQL = string.Format(strSQL, planNO, drugCode, state);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Pharmacy.PPR.Preparation.GetProcess:" + ex.Message;
                return null;
            }
            return this.ExecSqlForPreparationProcess(strSQL);
        }

        /// <summary>
        /// �����Ƽ���������������Ϣ
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int InsertProcess(FS.HISFC.Models.Preparation.Process p)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Preparation.InsertProcess", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = this.GetProcessParam(p); //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);				//�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ����¼��SQl������ֵʱ����Pharmacy.PPR.Preparation.InsertProcess" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// �����Ƽ���������������Ϣ
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int UpdateProcess(FS.HISFC.Models.Preparation.Process p)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Preparation.UpdateProcess", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = this.GetProcessParam(p); //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);					//�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ����¼��SQl������ֵʱ����Pharmacy.PPR.Preparation.UpdateProcess" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        public int SetProcess(FS.HISFC.Models.Preparation.Process p)
        {
            int parm = this.UpdateProcess(p);
            if (parm == -1)
            {
                return -1;
            }
            else if (parm == 0)
            {
                return this.InsertProcess(p);
            }
            return 1;
        }

        /// <summary>
        /// ɾ��������������
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int DelProcess(FS.HISFC.Models.Preparation.Process p)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Preparation.DelProcess", ref strSQL) == -1) return -1;
            try
            {
                //string[] strParm = this.GetProcessParam(p); //ȡ�����б�
                strSQL = string.Format(strSQL, p.Preparation.PlanNO, p.Preparation.Drug.ID, ((int)p.Preparation.State).ToString(), p.ProcessItem.ID);					//�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ����¼��SQl������ֵʱ����Pharmacy.PPR.Preparation.DelProcess" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }


        #endregion

        #region �Ƽ���Ʒģ�� �� ����ģ��

        /// <summary>
        /// ��ȡģ����ϸ��¼��ˮ��
        /// </summary>
        /// <returns>�ɹ�������ˮ�� ʧ�ܷ���null</returns>
        private string GetStencilSequence()
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Preparation.GetStencilSequence", ref strSQL) == -1)
                return null;
            string strReturn = this.ExecSqlReturnOne(strSQL);
            if (strReturn == "-1")
            {
                this.Err = "ȡҩƷ���ⵥ��ˮ��ʱ����" + this.Err;
                return null;
            }
            return strReturn;
        }

        /// <summary>
        /// �����Ƽ���Ʒģ���ȡ��������
        /// </summary>
        /// <param name="stencil">�Ƽ���Ʒģ��</param>
        /// <returns>�ɹ����ض�Ӧ�Ĳ�������</returns>
        private string[] myGetStencilParam(Stencil stencil)
        {
            stencil.Item.ID = stencil.ID;

            string[] strParam = 
                {
                    stencil.ID,                 //��ˮ����
                    stencil.Drug.ID,			//��Ʒ����
                    stencil.Drug.Name,			//��Ʒ����
                    stencil.Drug.Specs,			//��Ʒ���
                    ((int)stencil.Kind).ToString(),				//ģ����� 0 ���Ʒ����ģ��  1 ��Ʒ����ģ�� 2 �������� 3 ����
                    stencil.Type.ID,			//������
                    stencil.Type.Name,			//�������
                    stencil.ItemType.ToString(),//��Ŀ���
                    stencil.Item.ID,			//��Ŀ����
                    stencil.Item.Name,			//��Ŀ����
                    stencil.StandardMin.ToString(), //��׼������Сֵ
                    stencil.StandardMax.ToString(), //��׼�������ֵ
                    stencil.StandardDes,            //��׼����
                    stencil.Memo,				//��ע
                    stencil.Extend,				//��չ�ֶ�
                    stencil.Name,			    //ά����
                    stencil.OperEnv.OperTime.ToString() //ά��ʱ��
                };
            return strParam;
        }

        /// <summary>
        /// ִ��Sql����ȡ��Ʒģ��ʵ��
        /// </summary>
        /// <param name="strSql">��ִ��Sql���</param>
        /// <returns>�ɹ�����ʵ������ ʧ�ܷ���null �޼�¼���ؿ�����</returns>
        private List<Stencil> myGetStencil(string strSql)
        {
            List<Stencil> al = new List<Stencil>();
            Stencil stencil;

            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "ִ��Sql������\n" + strSql + this.Err;
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    stencil = new Stencil();

                    stencil.ID = this.Reader[0].ToString();                 //��ˮ����
                    stencil.Drug.ID = this.Reader[1].ToString();			//��Ʒ����
                    stencil.Drug.Name = this.Reader[2].ToString();			//��Ʒ����
                    stencil.Drug.Specs = this.Reader[3].ToString();			//��Ʒ���
                    stencil.Kind = (EnumStencialType)(NConvert.ToInt32(this.Reader[4].ToString()));				//���
                    stencil.Type.ID = this.Reader[5].ToString();			//������
                    stencil.Type.Name = this.Reader[6].ToString();			//�������
                    stencil.ItemType = (EnumStencilItemType)Enum.Parse(typeof(EnumStencilItemType), this.Reader[7].ToString());
                    stencil.Item.ID = this.Reader[8].ToString();			//��Ŀ����
                    stencil.Item.Name = this.Reader[9].ToString();			//��Ŀ����
                    stencil.StandardMin = NConvert.ToDecimal(this.Reader[10]);
                    stencil.StandardMax = NConvert.ToDecimal(this.Reader[11]);
                    stencil.StandardDes = this.Reader[12].ToString();
                    stencil.Memo = this.Reader[13].ToString();				//��ע
                    stencil.Extend = this.Reader[14].ToString();
                    stencil.OperEnv.ID = this.Reader[15].ToString();
                    stencil.OperEnv.OperTime = NConvert.ToDateTime(this.Reader[16].ToString());

                    stencil.Item.ID = stencil.ID;

                    al.Add(stencil);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��Reader�ڻ�ȡ��Ʒģ����Ϣ����" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// ���Ƽ���Ʒģ����ڲ����¼�¼
        /// </summary>
        /// <param name="stencil">�Ƽ���Ʒģ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int InsertStencil(Stencil stencil)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Stencil.InsertStencil", ref strSQL) == -1) return -1;
            try
            {
                if (stencil.ID == "" || stencil.ID == null)
                {
                    stencil.ID = this.GetStencilSequence();
                }
                if (stencil.ID == "" || stencil.ID == null)
                {
                    return -1;
                }
                stencil.Item.ID = stencil.ID;

                string[] strParm = this.myGetStencilParam(stencil); //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);				//�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ����¼��SQl������ֵʱ����Pharmacy.PPR.Stencil.InsertStencil" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ����һ�����г�Ʒģ��������Ϣ
        /// </summary>
        /// <param name="stencil">�Ƽ���Ʒģ��</param>
        /// <returns>�ɹ����ظ������� ʧ�ܷ���-1</returns>
        protected int UpdateStencil(Stencil stencil)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Stencil.UpdateStencil", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = this.myGetStencilParam(stencil); //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);					//�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ����¼��SQL������ֵʱ����Pharmacy.PPR.Stencil.UpdateStencil" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ��ִ�и��²��� ���²��ɹ���ִ�в������
        /// </summary>
        /// <param name="stencil">�Ƽ���Ʒģ��</param>
        /// <returns>�ɹ����ز�����¼�� ʧ�ܷ���-1</returns>
        public int SetStencil(Stencil stencil)
        {
            if (stencil.ID == null || stencil.ID == "")
            {
                return this.InsertStencil(stencil);
            }
            int parm = this.UpdateStencil(stencil);
            if (parm < 1)
            {
                parm = this.InsertStencil(stencil);
            }
            return parm;
        }

        /// <summary>
        /// ɾ����Ʒģ���¼
        /// </summary>
        /// <param name="id">��ˮ����</param
        /// <returns>�ɹ�����ɾ������  ʧ�ܷ���-1</returns>
        public int DelStencil(string id)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Stencil.DelStencil", ref strSQL) == -1) return -1;
            try
            {
                strSQL = string.Format(strSQL, id);
            }
            catch
            {
                this.Err = "�����������ȷ��Pharmacy.PPR.Stencil.DelStencil";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ɾ��ĳ����Ʒ�����ƴ���
        /// </summary>
        /// <param name="drugCode">��Ʒ����</param>
        /// <param name="kind">ģ����� </param>
        /// <returns>�ɹ�����ɾ����¼��  ʧ�ܷ���-1</returns>
        public int DelStencil(string drugCode, EnumStencialType kind)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Stencil.DelStencil.Drug", ref strSQL) == -1) return -1;
            try
            {
                strSQL = string.Format(strSQL, drugCode,((int)kind).ToString());
            }
            catch
            {
                this.Err = "�����������ȷ��Pharmacy.PPR.Stencil.DelStencil";
                return -1;
            }
            return this.ExecNoQuery(strSQL);

        }

        /// <summary>
        /// ��ȡĳ��Ʒģ����Ϣ
        /// </summary>
        /// <param name="drugCode">��Ʒ����</param>
        /// <param name="kind">ģ����� </param>
        /// <returns>�ɹ����س�Ʒģ������ ʧ�ܷ���null</returns>
        public List<Stencil> QueryStencil(string drugCode, EnumStencialType kind)
        {
            string strSelect = "";
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Stencil.GetStencil", ref strSelect) == -1)
                return null;
            if (this.GetSQL("Pharmacy.PPR.Stencil.GetStencil.Where.1", ref strSQL) == -1)
                return null;
            try
            {
                strSQL = strSelect + strSQL;
                strSQL = string.Format(strSQL, drugCode, ((int)kind).ToString());
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Pharmacy.PPR.Stencil.GetStencil:" + ex.Message;
                return null;
            }
            return this.myGetStencil(strSQL);
        }

        /// <summary>
        /// ��ȡĳ��Ʒģ����Ϣ
        /// </summary>
        /// <param name="kind">ģ�����</param>
        /// <returns>�ɹ����س�Ʒģ������ ʧ�ܷ���null</returns>
        public List<Stencil> QueryStencil(EnumStencialType kind)
        {
            return this.QueryStencil("AAAA", kind);
        }

        #endregion

        #region ��ʱ��ʹ��

        #region �������Ƽ��������

        /// <summary>
        /// ����ȷ�� �����Ƽ���
        /// </summary>
        /// <param name="confect">�Ƽ�����ʵ��</param>
        /// <returns>�ɹ����ظ������� ʧ�ܷ���-1</returns>
        public int UpdatePreparation(Confect confect)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Preparation.UpdatePreparationConfect", ref strSQL) == -1)
            {
                this.Err = "�Ҳ���SQL��䣡Pharmacy.PPR.Preparation.UpdatePreparationConfect";
                return -1;
            }
            try
            {
                //ȡ�����б�
                string[] strParm = {
                                       confect.PlanNO, 
                                       confect.Drug.ID, 
                                       confect.Name,					//����Ա
                                       confect.ConfectEnv.OperTime.ToString(),		//����ʱ��
                                       NConvert.ToInt32(confect.IsClear).ToString(),
                                       this.Operator.ID						//����Ա
                                   };
                strSQL = string.Format(strSQL, strParm);              //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "�����Ƽ������SQl������ֵ����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// �԰��Ʒ/��Ʒ��������Ƽ�����
        /// </summary>
        /// <param name="assay">�Ƽ�����ʵ��</param>
        /// <returns>�ɹ����ظ������� ʧ�ܷ���-1</returns>
        public int UpdatePreparation(Assay assay)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Preparation.UpdatePreparationAssay", ref strSQL) == -1)
            {
                this.Err = "�Ҳ���SQL��䣡Pharmacy.PPR.Preparation.UpdatePreparationAssay";
                return -1;
            }
            try
            {
                //ȡ�����б�
                string[] strParm = {
                                       assay.PlanNO, 
                                       assay.Drug.ID,
                                       ((int)assay.State).ToString(),
                                       assay.AssayQty.ToString(),		//�ͼ�����
                                       NConvert.ToInt32(assay.IsEligibility).ToString(),
                                       assay.Name,					//������
                                       assay.AssayEnv.OperTime.ToString(),		//����ʱ��
                                       NConvert.ToInt32(assay.IsClear).ToString(),
                                       this.Operator.ID					//����Ա
                                   };
                strSQL = string.Format(strSQL, strParm);					//�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "�����Ƽ������SQl������ֵ����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ���Ƽ��峡��¼�����Ƽ�����
        /// </summary>
        /// <param name="clear">�Ƽ��峡</param>
        /// <returns>�ɹ����ظ������� ʧ�ܷ���-1</returns>
        public int UpdatePreparation(Clear clear)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Preparation.UpdatePreparationClear", ref strSQL) == -1)
            {
                this.Err = "�Ҳ���SQL��䣡Pharmacy.PPR.Preparation.UpdatePreparationClear";
                return -1;
            }
            try
            {
                //ȡ�����б�
                string[] strParm = {
                                       clear.PlanNO, 
                                       clear.Drug.ID,
                                       ((int)clear.State).ToString(),
                                       NConvert.ToInt32(clear.IsCleaner).ToString(),
                                       clear.Name,
                                       clear.ClearEnv.OperTime.ToString()
                                   };
                strSQL = string.Format(strSQL, strParm);					//�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "�����Ƽ������SQl������ֵ����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        #endregion        

        #region ��Ʒ����
        /// <summary>
        /// �����Ƽ��Ƽ�������Ϣ��ȡ��������
        /// </summary>
        /// <param name="confect">�Ƽ�����ʵ��</param>
        /// <returns>�ɹ����ض�Ӧ�Ĳ�������</returns>
        private string[] myGetConfectParam(Confect confect)
        {
            string[] strParam = 
                {
                    confect.PlanNO,						//�����ƻ�����
                    confect.Drug.ID,					//��Ʒ����
                    confect.Drug.Name,					//��Ʒ����
                    confect.Drug.Specs,					//���
                    confect.Drug.PackUnit,				//��Ʒ��װ��λ
                    confect.Drug.PackQty.ToString(),	//��Ʒ��װ����
                    confect.Unit,						//��Һ����λ
                    confect.BatchNO,					//��Ʒ����
                    confect.PlanQty.ToString(),			//�ƻ���Һ��
                    confect.AssayQty.ToString(),		//�ͼ���Ʒ��
                    NConvert.ToInt32(confect.IsWhole).ToString(),		//�����豸�Ƿ����
                    NConvert.ToInt32(confect.IsCleanness).ToString(),	//�����豸�Ƿ����
                    confect.ScaleFlag,					//ҩ����ƽ����У��
                    confect.StetlyardFlag,				//���Ӻ���У��
                    confect.Regulations,				//�����
                    confect.Quality,					//�������
                    confect.Execute,					//����ִ�����
                    confect.Name,				//������
                    confect.ConfectEnv.OperTime.ToString(),		//����ʱ��
                    confect.CheckOper,					//���Ƹ�����
                    confect.Memo,						
                    confect.Extend1
                };
            return strParam;
        }

        /// <summary>
        /// ִ��Sql����ȡ�Ƽ���������
        /// </summary>
        /// <param name="strSql">��ִ��Sql���</param>
        /// <returns>�ɹ�����ʵ������ ʧ�ܷ���null �޼�¼���ؿ�����</returns>
        private ArrayList myGetConfect(string strSql)
        {
            ArrayList al = new ArrayList();
            Confect confect;

            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "ִ��Sql������\n" + strSql + this.Err;
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    confect = new Confect();

                    confect.PlanNO = this.Reader[0].ToString();				//�����ƻ�����
                    confect.Drug.ID = this.Reader[1].ToString();			//��Ʒ����
                    confect.Drug.Name = this.Reader[2].ToString();			//��Ʒ����
                    confect.Drug.Specs = this.Reader[3].ToString();			//��Ʒ���
                    confect.Drug.PackUnit = this.Reader[4].ToString();		//��Ʒ��װ��λ
                    confect.Drug.PackQty = NConvert.ToDecimal(this.Reader[5].ToString());	//��Ʒ��װ����
                    confect.Unit = this.Reader[6].ToString();				//�ƻ���Һ����λ
                    confect.BatchNO = this.Reader[7].ToString();			//��Ʒ����
                    confect.PlanQty = NConvert.ToDecimal(this.Reader[8].ToString());		//�ƻ���Һ��
                    confect.AssayQty = NConvert.ToDecimal(this.Reader[9].ToString());		//�ͼ���Ʒ��
                    confect.IsWhole = NConvert.ToBoolean(this.Reader[10].ToString());		//�豸�Ƿ����
                    confect.IsCleanness = NConvert.ToBoolean(this.Reader[11].ToString());	//�豸�Ƿ����
                    confect.ScaleFlag = this.Reader[12].ToString();
                    confect.StetlyardFlag = this.Reader[13].ToString();
                    confect.Regulations = this.Reader[14].ToString();
                    confect.Quality = this.Reader[15].ToString();
                    confect.Execute = this.Reader[16].ToString();
                    confect.Name = this.Reader[17].ToString();
                    confect.ConfectEnv.OperTime = NConvert.ToDateTime(this.Reader[18].ToString());
                    confect.CheckOper = this.Reader[19].ToString();
                    confect.Memo = this.Reader[20].ToString();
                    confect.Extend1 = this.Reader[21].ToString();

                    al.Add(confect);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��Reader�ڻ�ȡ��Ʒģ����Ϣ����" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }
        /// <summary>
        /// ���Ƽ�������Ϣ�ڲ����¼�¼
        /// </summary>
        /// <param name="confect">�Ƽ�����ʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int InsertConfect(Confect confect)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Confect.InsertConfect", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = this.myGetConfectParam(confect); //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);				//�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ����¼��SQl������ֵʱ����Pharmacy.PPR.Confect.InsertConfect" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }
        /// <summary>
        /// ����һ�������Ƽ�������Ϣ
        /// </summary>
        /// <param name="confect">�Ƽ�����ʵ��</param>
        /// <returns>�ɹ����ظ������� ʧ�ܷ���-1</returns>
        protected int UpdateConfect(Confect confect)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Confect.UpdateConfect", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = this.myGetConfectParam(confect); //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);					//�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ����¼��SQl������ֵʱ����Pharmacy.PPR.Confect.UpdateConfect" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }
        /// <summary>
        /// ��ִ�и��²��� ���²��ɹ���ִ�в������
        /// </summary>
        /// <param name="confect">�Ƽ�����ʵ��</param>
        /// <returns>�ɹ����ز�����¼�� ʧ�ܷ���-1</returns>
        public int SetConfect(Confect confect)
        {
            int parm = this.UpdateConfect(confect);
            if (parm < 1)
            {
                parm = this.InsertConfect(confect);
            }
            return parm;
        }
        /// <summary>
        /// ��ȡĳ��Ʒ������Ϣ
        /// </summary>
        /// <param name="planNo">�����ƻ�����</param>
        /// <param name="drugCode">��Ʒ����</param>
        /// <returns>�ɹ����س�Ʒģ������ ʧ�ܷ���null</returns>
        public ArrayList QueryConfect(string planNo, string drugCode)
        {
            string strSelect = "";
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Confect.GetConfect", ref strSelect) == -1)
                return null;
            if (this.GetSQL("Pharmacy.PPR.Where.DrugCode", ref strSQL) == -1)
                return null;
            try
            {
                strSQL = strSelect + strSQL;
                strSQL = string.Format(strSQL, planNo, drugCode);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Pharmacy.PPR.Confect.GetConfect:" + ex.Message;
                return null;
            }
            return this.myGetConfect(strSQL);
        }
        /// <summary>
        /// ��ȡĳ��Ʒ������Ϣ
        /// </summary>
        /// <param name="planNo">�����ƻ�����</param>
        /// <returns>�ɹ����س�Ʒģ������ ʧ�ܷ���null</returns>
        public ArrayList QueryConfect(string planNo)
        {
            return this.QueryConfect(planNo, "AAAA");
        }
        #endregion

        #region ��Ʒ��װ
        /// <summary>
        /// �����Ƽ��Ƽ���װ��Ϣ��ȡ��������
        /// </summary>
        /// <param name="division">�Ƽ���װʵ��</param>
        /// <returns>�ɹ����ض�Ӧ�Ĳ�������</returns>
        private string[] myGetDivisiontParam(Division division)
        {
            string[] strParam = 
                {
                    division.PlanNO,					//�����ƻ�����
                    division.Drug.ID,					//��Ʒ����
                    division.Drug.Name,					//��Ʒ����
                    division.Drug.Specs,				//���
                    division.Drug.PackUnit,				//��Ʒ��װ��λ
                    division.Drug.PackQty.ToString(),	//��Ʒ��װ����
                    division.Unit,						//��Һ����λ
                    division.BatchNO,					//��Ʒ����
                    division.PlanQty.ToString(),		//�ƻ���Һ��
                    division.DivisionQty.ToString(),	//��װ��
                    division.WasterQty.ToString(),		//��Ʒ��
                    division.AssayQty.ToString(),		//�ͼ���Ʒ��
                    division.DivisionParam.ToString(),	//����ƽ��
                    NConvert.ToInt32(division.IsWhole).ToString(),		//�豸�Ƿ����
                    NConvert.ToInt32(division.IsCleanness).ToString(),	//�豸�Ƿ����
                    division.Regulations,				//�����
                    division.Quality,					//�������
                    division.Execute,					//����ִ�����
                    division.Name,				//��װ��
                    division.DivisionEnv.OperTime.ToString(),	//��װʱ��
                    division.InceptOper,				//����ƽ���
                    division.Memo,						
                    division.Extend1
                };
            return strParam;
        }

        /// <summary>
        /// ִ��Sql����ȡ�Ƽ���װ����
        /// </summary>
        /// <param name="strSql">��ִ��Sql���</param>
        /// <returns>�ɹ�����ʵ������ ʧ�ܷ���null �޼�¼���ؿ�����</returns>
        private ArrayList myGetDivision(string strSql)
        {
            ArrayList al = new ArrayList();
            Division division;

            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "ִ��Sql������\n" + strSql + this.Err;
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    division = new Division();

                    division.PlanNO = this.Reader[0].ToString();				//�����ƻ�����
                    division.Drug.ID = this.Reader[1].ToString();				//��Ʒ����
                    division.Drug.Name = this.Reader[2].ToString();				//��Ʒ����
                    division.Drug.Specs = this.Reader[3].ToString();			//��Ʒ���
                    division.Drug.PackUnit = this.Reader[4].ToString();			//��Ʒ��װ��λ
                    division.Drug.PackQty = NConvert.ToDecimal(this.Reader[5].ToString());	//��Ʒ��װ����
                    division.Unit = this.Reader[6].ToString();					//�ƻ���Һ����λ
                    division.BatchNO = this.Reader[7].ToString();				//��Ʒ����
                    division.PlanQty = NConvert.ToDecimal(this.Reader[8].ToString());		//�ƻ���Һ��
                    division.DivisionQty = NConvert.ToDecimal(this.Reader[9].ToString());	//��װ��Ʒ��
                    division.WasterQty = NConvert.ToDecimal(this.Reader[10].ToString());	//��Ʒ��
                    division.AssayQty = NConvert.ToDecimal(this.Reader[11].ToString());		//�ͼ���Ʒ��
                    division.DivisionParam = NConvert.ToDecimal(this.Reader[12].ToString());//����ƽ��
                    division.IsWhole = NConvert.ToBoolean(this.Reader[13].ToString());		//�豸�Ƿ����
                    division.IsCleanness = NConvert.ToBoolean(this.Reader[14].ToString());	//�豸�Ƿ����
                    division.Regulations = this.Reader[15].ToString();
                    division.Quality = this.Reader[16].ToString();
                    division.Execute = this.Reader[17].ToString();
                    division.Name = this.Reader[18].ToString();
                    division.DivisionEnv.OperTime = NConvert.ToDateTime(this.Reader[19].ToString());
                    division.InceptOper = this.Reader[20].ToString();
                    division.Memo = this.Reader[21].ToString();
                    division.Extend1 = this.Reader[22].ToString();

                    al.Add(division);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��Reader�ڻ�ȡ��Ʒ��װ��Ϣ����" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }
        /// <summary>
        /// ���Ƽ���װ��Ϣ�ڲ����¼�¼
        /// </summary>
        /// <param name="division">�Ƽ���װʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int InsertDivision(Division division)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Division.InsertDivision", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = this.myGetDivisiontParam(division); //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);				//�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ����¼��SQl������ֵʱ����Pharmacy.PPR.Division.InsertDivision" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }
        /// <summary>
        /// ����һ�������Ƽ���װ��Ϣ
        /// </summary>
        /// <param name="division">�Ƽ���װʵ��</param>
        /// <returns>�ɹ����ظ������� ʧ�ܷ���-1</returns>
        protected int UpdateDivision(Division division)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Division.UpdateDivision", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = this.myGetDivisiontParam(division); //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);					//�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ����¼��SQl������ֵʱ����Pharmacy.PPR.Division.UpdateDivision" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }
        /// <summary>
        /// ��ִ�и��²��� ���²��ɹ���ִ�в������
        /// </summary>
        /// <param name="division">�Ƽ���װʵ��</param>
        /// <returns>�ɹ����ز�����¼�� ʧ�ܷ���-1</returns>
        public int SetDivision(Division division)
        {
            int parm = this.UpdateDivision(division);
            if (parm < 1)
            {
                parm = this.InsertDivision(division);
            }
            return parm;
        }
        /// <summary>
        /// ��ȡĳ��Ʒ��װ��Ϣ
        /// </summary>
        /// <param name="planNo">�����ƻ�����</param>
        /// <param name="drugCode">��Ʒ����</param>
        /// <returns>�ɹ����س�Ʒģ������ ʧ�ܷ���null</returns>
        public ArrayList QueryDivision(string planNo, string drugCode)
        {
            string strSelect = "";
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Division.GetDivision", ref strSelect) == -1)
                return null;
            if (this.GetSQL("Pharmacy.PPR.Where.DrugCode", ref strSQL) == -1)
                return null;
            try
            {
                strSQL = strSelect + strSQL;
                strSQL = string.Format(strSQL, planNo, drugCode);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Pharmacy.PPR.Division.GetDivision:" + ex.Message;
                return null;
            }
            return this.myGetDivision(strSQL);
        }
        /// <summary>
        /// ��ȡĳ��Ʒ��װ��Ϣ
        /// </summary>
        /// <param name="planNo">�����ƻ�����</param>
        /// <returns>�ɹ����س�Ʒģ������ ʧ�ܷ���null</returns>
        public ArrayList QueryDivision(string planNo)
        {
            return this.QueryDivision(planNo, "AAAA");
        }
        #endregion

        #region ��Ʒ���װ
        /// <summary>
        /// �����Ƽ��Ƽ����װ��Ϣ��ȡ��������
        /// </summary>
        /// <param name="package">�Ƽ����װʵ��</param>
        /// <returns>�ɹ����ض�Ӧ�Ĳ�������</returns>
        private string[] myGetPackageParam(Package package)
        {
            string[] strParam = 
                {
                    package.PlanNO,						//�����ƻ�����
                    package.Drug.ID,					//��Ʒ����
                    package.Drug.Name,					//��Ʒ����
                    package.Drug.Specs,					//���
                    package.Drug.PackUnit,				//��Ʒ��װ��λ
                    package.Drug.PackQty.ToString(),	//��Ʒ��װ����
                    package.Unit,						//��Һ����λ
                    package.BatchNO,					//��Ʒ����
                    package.PlanQty.ToString(),			//�ƻ���Һ��
                    package.DivisionQty.ToString(),		//��װ��Ʒ��
                    package.PackingQty.ToString(),		//���װ��Ʒ��
                    package.WasterQty.ToString(),		//���װ��Ʒ��
                    package.PacParam.ToString(),		//����ƽ��
                    package.FinParam.ToString(),		//��Ʒ��
                    NConvert.ToInt32(package.IsCleanness).ToString(),		//�Ƿ����
                    NConvert.ToInt32(package.IsClear).ToString(),			//�Ƿ��峡
                    package.Regulations,				//�����
                    package.Quality,					//�������
                    package.Execute,					//����ִ�����
                    package.Name,				//���װ��
                    package.PackingEnv.OperTime.ToString(),		//���װʱ��
                    package.CheckOper,					//���װ������
                    package.InceptOper,					//��������
                    package.Memo,						
                    package.Extend1
                };
            return strParam;
        }

        /// <summary>
        /// ִ��Sql����ȡ�Ƽ����װ����
        /// </summary>
        /// <param name="strSql">��ִ��Sql���</param>
        /// <returns>�ɹ�����ʵ������ ʧ�ܷ���null �޼�¼���ؿ�����</returns>
        private ArrayList myGetPackage(string strSql)
        {
            ArrayList al = new ArrayList();
            Package package;

            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "ִ��Sql������\n" + strSql + this.Err;
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    package = new Package();

                    package.PlanNO = this.Reader[0].ToString();				//�����ƻ�����
                    package.Drug.ID = this.Reader[1].ToString();			//��Ʒ����
                    package.Drug.Name = this.Reader[2].ToString();			//��Ʒ����
                    package.Drug.Specs = this.Reader[3].ToString();			//��Ʒ���
                    package.Drug.PackUnit = this.Reader[4].ToString();		//��Ʒ��װ��λ
                    package.Drug.PackQty = NConvert.ToDecimal(this.Reader[5].ToString());	//��Ʒ��װ����
                    package.Unit = this.Reader[6].ToString();				//�ƻ���Һ����λ
                    package.BatchNO = this.Reader[7].ToString();			//��Ʒ����
                    package.PlanQty = NConvert.ToDecimal(this.Reader[8].ToString());		//�ƻ���Һ��
                    package.DivisionQty = NConvert.ToDecimal(this.Reader[9].ToString());	//��װ��Ʒ��
                    package.PackingQty = NConvert.ToDecimal(this.Reader[10].ToString());	//���װ��Ʒ��
                    package.WasterQty = NConvert.ToDecimal(this.Reader[11].ToString());		//��Ʒ��
                    package.PacParam = NConvert.ToDecimal(this.Reader[12].ToString());		//����ƽ��
                    package.FinParam = NConvert.ToDecimal(this.Reader[13].ToString());		//��Ʒ��
                    package.IsCleanness = NConvert.ToBoolean(this.Reader[14].ToString());	//�Ƿ����
                    package.IsClear = NConvert.ToBoolean(this.Reader[15].ToString());		//�Ƿ��峡
                    package.Regulations = this.Reader[16].ToString();
                    package.Quality = this.Reader[17].ToString();
                    package.Execute = this.Reader[18].ToString();
                    package.Name = this.Reader[19].ToString();
                    package.PackingEnv.OperTime = NConvert.ToDateTime(this.Reader[20].ToString());
                    package.CheckOper = this.Reader[21].ToString();
                    package.InceptOper = this.Reader[22].ToString();
                    package.Memo = this.Reader[23].ToString();
                    package.Extend1 = this.Reader[24].ToString();

                    al.Add(package);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��Reader�ڻ�ȡ��Ʒ���װ��Ϣ����" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }
        /// <summary>
        /// ���Ƽ����װ��Ϣ�ڲ����¼�¼
        /// </summary>
        /// <param name="package">�Ƽ����װʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int InsertPackage(Package package)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Package.InsertPackage", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = this.myGetPackageParam(package); //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);				//�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ����¼��SQl������ֵʱ����Pharmacy.PPR.Package.InsertPackage" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }
        /// <summary>
        /// ����һ�������Ƽ����װ��Ϣ
        /// </summary>
        /// <param name="package">�Ƽ����װʵ��</param>
        /// <returns>�ɹ����ظ������� ʧ�ܷ���-1</returns>
        protected int UpdatePackage(Package package)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Package.UpdatePackage", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = this.myGetPackageParam(package); //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);					//�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ����¼��SQl������ֵʱ����Pharmacy.PPR.Package.UpdatePackage" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }
        /// <summary>
        /// ��ִ�и��²��� ���²��ɹ���ִ�в������
        /// </summary>
        /// <param name="package">�Ƽ����װʵ��</param>
        /// <returns>�ɹ����ز�����¼�� ʧ�ܷ���-1</returns>
        public int SetPackage(Package package)
        {
            int parm = this.UpdatePackage(package);
            if (parm < 1)
            {
                parm = this.InsertPackage(package);
            }
            return parm;
        }
        /// <summary>
        /// ��ȡĳ��Ʒ���װ��Ϣ
        /// </summary>
        /// <param name="planNo">�����ƻ�����</param>
        /// <param name="drugCode">��Ʒ����</param>
        /// <returns>�ɹ����س�Ʒģ������ ʧ�ܷ���null</returns>
        public ArrayList QueryPackage(string planNo, string drugCode)
        {
            string strSelect = "";
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Package.GetPackage", ref strSelect) == -1)
                return null;
            if (this.GetSQL("Pharmacy.PPR.Where.DrugCode", ref strSQL) == -1)
                return null;
            try
            {
                strSQL = strSelect + strSQL;
                strSQL = string.Format(strSQL, planNo, drugCode);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Pharmacy.PPR.Package.GetPackage:" + ex.Message;
                return null;
            }
            return this.myGetPackage(strSQL);
        }
        /// <summary>
        /// ��ȡĳ��Ʒ���װ��Ϣ
        /// </summary>
        /// <param name="planNo">�����ƻ�����</param>
        /// <returns>�ɹ����س�Ʒģ������ ʧ�ܷ���null</returns>
        public ArrayList QueryPackage(string planNo)
        {
            return this.QueryPackage(planNo, "AAAA");
        }
        #endregion

        #region �Ƽ��峡��¼
        /// <summary>
        /// �����Ƽ��Ƽ��峡��Ϣ��ȡ��������
        /// </summary>
        /// <param name="clear">�Ƽ��峡ʵ��</param>
        /// <returns>�ɹ����ض�Ӧ�Ĳ�������</returns>
        private string[] myGetClearParam(Clear clear)
        {
            string[] strParam = {
                                    clear.PlanNO,						//�������̱���
                                    ((int)clear.State).ToString(),						//�������̱�־ ״̬ 1 ���� 2 ���Ʒ��װ 3 ���Ʒ����4 ��Ʒ���װ 5 ��Ʒ���� 6 ��Ʒ���
                                    clear.Drug.ID,						//��Ʒ����
                                    clear.Drug.Name,					//��Ʒ����
                                    clear.Drug.Specs,					//���
                                    clear.BatchNO,						//����
                                    clear.PrivPlanNO,					//��һ���������ƻ���
                                    clear.PrivDrugNum,					//��һ���γ�Ʒ����
                                    NConvert.ToInt32(clear.IsMaterial).ToString(),	//�����Ƿ�ϸ�
                                    NConvert.ToInt32(clear.IsMid).ToString(),		//�м�Ʒ�Ƿ�ϸ�
                                    NConvert.ToInt32(clear.IsWaster).ToString(),	//������Ʒ�Ƿ�ϸ�
                                    NConvert.ToInt32(clear.IsTechnics).ToString(),	//�����Ƿ�ϸ�
                                    NConvert.ToInt32(clear.IsTool).ToString(),		//�����Ƿ�ϸ�
                                    NConvert.ToInt32(clear.IsContainer).ToString(),	//�����Ƿ�ϸ�
                                    NConvert.ToInt32(clear.IsEquipment).ToString(),	//�����豸�Ƿ�ϸ�
                                    NConvert.ToInt32(clear.IsWorkShop).ToString(),	//���������Ƿ�ϸ�
                                    NConvert.ToInt32(clear.IsCleaner).ToString(),	//����Ƿ�ϸ�
                                    clear.Name,
                                    clear.ClearEnv.OperTime.ToString(),
                                    clear.CheckOper,
                                    clear.Memo,
                                    clear.Extend1
                                };
            return strParam;
        }
        /// <summary>
        /// ִ��Sql����ȡ�Ƽ��峡����
        /// </summary>
        /// <param name="strSql">��ִ��Sql���</param>
        /// <returns>�ɹ�����ʵ������ ʧ�ܷ���null �޼�¼���ؿ�����</returns>
        private ArrayList myGetClear(string strSql)
        {
            ArrayList al = new ArrayList();
            Clear clear;

            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "ִ��Sql������\n" + strSql + this.Err;
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    clear = new Clear();

                    clear.PlanNO = this.Reader[0].ToString();				//�����ƻ�����
                    clear.State = (EnumState)(NConvert.ToInt32(this.Reader[1].ToString()));				//�������̱�־ ״̬ 1 ���� 2 ���Ʒ��װ 3 ���Ʒ���� 4 ��Ʒ���װ 5 ��Ʒ���� 6 ��Ʒ���
                    clear.Drug.ID = this.Reader[2].ToString();				//��Ʒ����
                    clear.Drug.Name = this.Reader[3].ToString();			//��Ʒ����
                    clear.Drug.Specs = this.Reader[4].ToString();			//��Ʒ���
                    clear.BatchNO = this.Reader[5].ToString();				//��Ʒ����
                    clear.PrivPlanNO = this.Reader[6].ToString();			//��һ���������ƻ���
                    clear.PrivDrugNum = this.Reader[7].ToString();			//��һ���γ�Ʒ����
                    clear.IsMaterial = NConvert.ToBoolean(this.Reader[8].ToString());	//�����Ƿ�ϸ�
                    clear.IsMid = NConvert.ToBoolean(this.Reader[9].ToString());		//�м�Ʒ�Ƿ�ϸ�
                    clear.IsWaster = NConvert.ToBoolean(this.Reader[10].ToString());	//�������Ƿ�ϸ�
                    clear.IsTechnics = NConvert.ToBoolean(this.Reader[11].ToString());	//�����Ƿ�ϸ�
                    clear.IsTool = NConvert.ToBoolean(this.Reader[12].ToString());		//�����Ƿ�ϸ�
                    clear.IsContainer = NConvert.ToBoolean(this.Reader[13].ToString());	//�����Ƿ�ϸ�
                    clear.IsEquipment = NConvert.ToBoolean(this.Reader[14].ToString());	//�����豸�Ƿ�ϸ�
                    clear.IsWorkShop = NConvert.ToBoolean(this.Reader[15].ToString());	//���������Ƿ�ϸ�
                    clear.IsCleaner = NConvert.ToBoolean(this.Reader[16].ToString());	//����Ƿ�ϸ�
                    clear.Name = this.Reader[17].ToString();
                    clear.ClearEnv.OperTime = NConvert.ToDateTime(this.Reader[18].ToString());
                    clear.CheckOper = this.Reader[19].ToString();
                    clear.Memo = this.Reader[20].ToString();
                    clear.Extend1 = this.Reader[21].ToString();

                    al.Add(clear);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��Reader�ڻ�ȡ��Ʒ�峡��Ϣ����" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }
        /// <summary>
        /// ���Ƽ��峡��Ϣ�ڲ����¼�¼
        /// </summary>
        /// <param name="clear">�Ƽ��峡ʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int InsertClear(Clear clear)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Clear.InsertClear", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = this.myGetClearParam(clear); //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);				//�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ����¼��SQl������ֵʱ����Pharmacy.PPR.Clear.InsertClear" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }
        /// <summary>
        /// ����һ�������Ƽ��峡��Ϣ
        /// </summary>
        /// <param name="clear">�Ƽ��峡ʵ��</param>
        /// <returns>�ɹ����ظ������� ʧ�ܷ���-1</returns>
        protected int UpdateClear(Clear clear)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Clear.UpdateClear", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = this.myGetClearParam(clear); //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);					//�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ����¼��SQl������ֵʱ����Pharmacy.PPR.Clear.UpdateClear" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }
        /// <summary>
        /// ��ִ�и��²��� ���²��ɹ���ִ�в������
        /// </summary>
        /// <param name="clear">�Ƽ��峡ʵ��</param>
        /// <returns>�ɹ����ز�����¼�� ʧ�ܷ���-1</returns>
        public int SetClear(Clear clear)
        {
            int parm = this.UpdateClear(clear);
            if (parm < 1)
            {
                parm = this.InsertClear(clear);
            }
            return parm;
        }
        /// <summary>
        /// ��ȡĳ��Ʒ�峡��Ϣ
        /// </summary>
        /// <param name="planNo">�����ƻ�����</param>
        /// <param name="state">�����ƻ�״̬</param>
        /// <param name="drugCode">��Ʒ����</param>
        /// <returns>�ɹ����س�Ʒģ������ ʧ�ܷ���null</returns>
        public ArrayList QueryClear(string planNo, string state, string drugCode)
        {
            string strSelect = "";
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Clear.GetClear", ref strSelect) == -1)
                return null;
            if (this.GetSQL("Pharmacy.PPR.Where.DrugState", ref strSQL) == -1)
                return null;
            try
            {
                strSQL = strSelect + strSQL;
                strSQL = string.Format(strSQL, planNo, state, drugCode);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Pharmacy.PPR.Clear.GetClear:" + ex.Message;
                return null;
            }
            return this.myGetClear(strSQL);
        }
        /// <summary>
        /// ��ȡĳ��Ʒ�峡��Ϣ
        /// </summary>
        /// <param name="planNo">�����ƻ�����</param>
        /// <param name="state">�����ƻ�״̬</param>
        /// <returns>�ɹ����س�Ʒģ������ ʧ�ܷ���null</returns>
        public ArrayList QueryClear(string planNo, string state)
        {
            return this.QueryClear(planNo, state, "AAAA");
        }

        /// <summary>
        /// ��ȡ������Ҫ�峡������Ƽ��б�
        /// </summary>
        /// <param name="isClear">�Ƿ��ѯ�������峡��¼���Ƽ���Ϣ True �������峡��¼ False δ�����峡��¼</param>
        /// <param name="beginDate">��ѯ��ʼʱ��</param>
        /// <param name="endDate">��ѯ����ʱ��</param>
        /// <returns>�ɹ����ض�Ӧ���� ʧ�ܷ���null</returns>
        public List<FS.HISFC.Models.Preparation.Preparation> QueryClearList(bool isClear, DateTime beginDate, DateTime endDate)
        {
            string strSelect = "";
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Preparation.GetPreparation", ref strSelect) == -1)
                return null;
            if (this.GetSQL("Pharmacy.PPR.Clear.Where.ClearList", ref strSQL) == -1)
                return null;
            try
            {
                strSQL = strSelect + strSQL;
                strSQL = string.Format(strSQL, NConvert.ToInt32(isClear), beginDate.ToString(), endDate.ToString());
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Pharmacy.PPR.Preparation.GetPreparation:" + ex.Message;
                return null;
            }
            return this.myGetPreparation(strSQL);
        }
        #endregion

        #region �Ƽ�����
        /// <summary>
        /// �����Ƽ��Ƽ�������Ϣ��ȡ��������
        /// </summary>
        /// <param name="assay">�Ƽ�����ʵ��</param>
        /// <returns>�ɹ����ض�Ӧ�Ĳ�������</returns>
        private string[] myGetAssayParam(Assay assay)
        {
            string[] strParam = {
                                    assay.PlanNO,						//0 �������̱���
                                    ((int)assay.State).ToString(),						//1 �������̱�־ ״̬ 1 ���� 2 ���Ʒ��װ 3 ���Ʒ���� 4 ��Ʒ���װ 5 ��Ʒ���� 6 ��Ʒ���
                                    assay.Drug.ID,						//2 ��Ʒ����
                                    assay.Drug.Name,					//3 ��Ʒ����
                                    assay.Drug.Specs,					//4 ���
                                    assay.BatchNO,						//5 ����
                                    assay.ReportNum,						//6 ��������
                                    assay.Name,					//7 �ͼ���
                                    assay.ApplyEnv.OperTime.ToString(),			//8 �ͼ�ʱ��
                                    assay.DivisionQty.ToString(),		//9 ������
                                    assay.AssayQty.ToString(),			//10 �ͼ���
                                    assay.Unit,							//11 ��λ
                                    assay.Stencil.Type.ID,				//12 ������
                                    assay.Stencil.Type.Name,			//13 �������
                                    assay.Stencil.Item.ID,				//14 ��Ŀ����
                                    assay.Stencil.Item.Name,			//15 ��Ŀ����
                                    assay.Content.ToString(),			//16 ���麬��
                                    assay.ResultQty.ToString(),			//17 ��ֵ������
                                    assay.ResultStr,					//18 �ַ�������
                                    NConvert.ToInt32(assay.IsEligibility).ToString(),	//19 �Ƿ�ϸ�
                                    assay.AssayRule,					//20 �����׼����
                                    assay.Name,					//21
                                    assay.CheckOper,					//22
                                    assay.AssayEnv.OperTime.ToString(),			//23
                                    assay.Memo,							//24
                                    assay.Extend1						//25
                                };
            return strParam;
        }
        /// <summary>
        /// ִ��Sql����ȡ�Ƽ���������
        /// </summary>
        /// <param name="strSql">��ִ��Sql���</param>
        /// <returns>�ɹ�����ʵ������ ʧ�ܷ���null �޼�¼���ؿ�����</returns>
        private ArrayList myGetAssay(string strSql)
        {
            ArrayList al = new ArrayList();
            Assay assay;

            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "ִ��Sql������\n" + strSql + this.Err;
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    assay = new Assay();

                    assay.PlanNO = this.Reader[0].ToString();				//�����ƻ�����
                    assay.State = (EnumState)(NConvert.ToInt32(this.Reader[1].ToString()));				//�������̱�־ ״̬ 1 ���� 2 ���Ʒ��װ 3 ���Ʒ���� 4 ��Ʒ���װ 5 ��Ʒ���� 6 ��Ʒ���
                    assay.Drug.ID = this.Reader[2].ToString();				//��Ʒ����
                    assay.Drug.Name = this.Reader[3].ToString();			//��Ʒ����
                    assay.Drug.Specs = this.Reader[4].ToString();			//��Ʒ���
                    assay.BatchNO = this.Reader[5].ToString();				//��Ʒ����
                    assay.ReportNum = this.Reader[6].ToString();				//��������
                    assay.Name = this.Reader[7].ToString();						//�ͼ���
                    assay.ApplyEnv.OperTime = NConvert.ToDateTime(this.Reader[8].ToString());	//�ͼ�����
                    assay.DivisionQty = NConvert.ToDecimal(this.Reader[9].ToString());	//������
                    assay.AssayQty = NConvert.ToDecimal(this.Reader[10].ToString());		//�ͼ���
                    assay.Unit = this.Reader[11].ToString();							//��λ
                    assay.Stencil.Type.ID = this.Reader[12].ToString();					//������
                    assay.Stencil.Type.Name = this.Reader[13].ToString();				//�������
                    assay.Stencil.Item.ID = this.Reader[14].ToString();					//��Ŀ����
                    assay.Stencil.Item.Name = this.Reader[15].ToString();				//��Ŀ����
                    assay.Content = NConvert.ToDecimal(this.Reader[16].ToString());		//���麬��
                    assay.ResultQty = NConvert.ToDecimal(this.Reader[17].ToString());	//��ֵ������
                    assay.ResultStr = this.Reader[18].ToString();	//�ַ�������
                    assay.IsEligibility = NConvert.ToBoolean(this.Reader[19].ToString());//����Ƿ�ϸ�
                    assay.AssayRule = this.Reader[20].ToString();						//�����׼����
                    assay.Name = this.Reader[21].ToString();
                    assay.CheckOper = this.Reader[22].ToString();
                    assay.AssayEnv.OperTime = NConvert.ToDateTime(this.Reader[23].ToString());
                    assay.Memo = this.Reader[24].ToString();
                    assay.Extend1 = this.Reader[25].ToString();

                    al.Add(assay);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��Reader�ڻ�ȡ��Ʒ������Ϣ����" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }
        /// <summary>
        /// ���Ƽ�������Ϣ�ڲ����¼�¼
        /// </summary>
        /// <param name="assay">�Ƽ�����ʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int InsertAssay(Assay assay)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Assay.InsertAssay", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = this.myGetAssayParam(assay); //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);				//�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ����¼��SQl������ֵʱ����Pharmacy.PPR.Assay.InsertAssay" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }
        /// <summary>
        /// ����һ�������Ƽ�������Ϣ
        /// </summary>
        /// <param name="assay">�Ƽ�����ʵ��</param>
        /// <returns>�ɹ����ظ������� ʧ�ܷ���-1</returns>
        protected int UpdateAssay(Assay assay)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Assay.UpdateAssay", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = this.myGetAssayParam(assay); //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);					//�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ����¼��SQl������ֵʱ����Pharmacy.PPR.Assay.UpdateAssay" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }
        /// <summary>
        /// ��ִ�и��²��� ���²��ɹ���ִ�в������
        /// </summary>
        /// <param name="assay">�Ƽ�����ʵ��</param>
        /// <returns>�ɹ����ز�����¼�� ʧ�ܷ���-1</returns>
        public int SetAssay(Assay assay)
        {
            int parm = this.UpdateAssay(assay);
            if (parm < 1)
            {
                parm = this.InsertAssay(assay);
            }
            return parm;
        }
        /// <summary>
        /// ɾ��һ��������Ϣ
        /// </summary>
        /// <param name="assay">����ʵ��</param>
        /// <returns>�ɹ�����ɾ������ ʧ�ܷ���-1</returns>
        public int DelAssay(Assay assay)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Assay.DelAssay", ref strSQL) == -1) return -1;
            try
            {
                strSQL = string.Format(strSQL, assay.PlanNO, assay.State, assay.Drug.ID, assay.Stencil.Type.ID, assay.Stencil.Item.ID);
            }
            catch
            {
                this.Err = "�����������ȷ��Pharmacy.PPR.Assay.DelAssay";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }
        /// <summary>
        /// ��ȡĳ��Ʒ������Ϣ
        /// </summary>
        /// <param name="planNo">�����ƻ�����</param>
        /// <param name="state">�����ƻ�״̬</param>
        /// <param name="drugCode">��Ʒ����</param>
        /// <returns>�ɹ����س�Ʒģ������ ʧ�ܷ���null</returns>
        public ArrayList QueryAssay(string planNo, string state, string drugCode)
        {
            string strSelect = "";
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Assay.GetAssay", ref strSelect) == -1)
                return null;
            if (this.GetSQL("Pharmacy.PPR.Where.DrugState", ref strSQL) == -1)
                return null;
            try
            {
                strSQL = strSelect + strSQL;
                strSQL = string.Format(strSQL, planNo, state, drugCode);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Pharmacy.PPR.Assay.GetAssay:" + ex.Message;
                return null;
            }
            return this.myGetAssay(strSQL);
        }
        /// <summary>
        /// ��ȡĳ��Ʒ������Ϣ
        /// </summary>
        /// <param name="planNo">�����ƻ�����</param>
        /// <param name="state">�����ƻ�״̬</param>
        /// <returns>�ɹ����س�Ʒģ������ ʧ�ܷ���null</returns>
        public ArrayList QueryAssay(string planNo, string state)
        {
            return this.QueryAssay(planNo, state, "AAAA");
        }
        #endregion


        /// <summary>
        /// �����Ƽ�����״̬
        /// </summary>
        /// <param name="planNo">�����ƻ�����</param>
        /// <param name="drugCode">��Ʒ����</param>
        /// <param name="state">�Ƽ�״̬</param>
        /// <param name="isClear">�Ƿ��峡</param>
        /// <returns>�ɹ����ظ������� ʧ�ܷ���-1</returns>
        public int UpdatePreparation(string planNo, string drugCode, FS.HISFC.Models.Preparation.EnumState state, bool isClear)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.PPR.Preparation.UpdatePreparationState", ref strSQL) == -1)
            {
                this.Err = "�Ҳ���SQL��䣡Pharmacy.PPR.Preparation.UpdatePreparationState";
                return -1;
            }
            try
            {
                //ȡ�����б�
                string[] strParm = {
                                       planNo, 
                                       drugCode,
                                       ((int)state).ToString(),
                                       NConvert.ToInt32(isClear).ToString(),
                                       this.Operator.ID
                                   };
                strSQL = string.Format(strSQL, strParm);              //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "�����Ƽ������SQl������ֵ����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// �����Ƽ���Ʒ��ȡ�Ƽ�ԭ�ϳ�����Ϣ����
        /// applyOut.Item.User01 �洢��׼������
        /// </summary>
        /// <param name="preparation">�Ƽ���Ʒ��Ϣ</param>
        /// <returns>�ɹ����س���������Ϣ���� ʧ�ܷ���null</returns>
        public ArrayList QueryMaterialApplyOut(FS.HISFC.Models.Preparation.Preparation preparation)
        {
            ArrayList al = new ArrayList();

            #region ������Ϣ��ȡ ���ô������������
            List<FS.HISFC.Models.Preparation.Prescription> alPrescription = this.QueryDrugPrescription(preparation.Drug.ID);
            if (alPrescription == null)
                return null;
            if (alPrescription.Count == 0)
            {
                this.Err = preparation.Drug.Name + "��  �ó�Ʒδ�������ƴ���ά��";
                return null;
            }
            FS.FrameWork.Models.NeuObject operDept;
            try
            {
                operDept = ((FS.HISFC.Models.Base.Employee)this.Operator).Dept;
                if (operDept == null)
                {
                    this.Err = "δ��ȷ��ȡ����Ա������Ϣ";
                    return null;
                }
            }
            catch
            {
                this.Err = "���ݲ���Ա��ȡ�������ҳ���";
                return null;
            }
            #endregion

            FS.HISFC.BizLogic.Pharmacy.Item itemMgr = new Item();
            //itemMgr.SetTrans(this.command.Transaction);	

            FS.HISFC.Models.Pharmacy.ApplyOut applyOut;
            foreach (Prescription info in alPrescription)
            {
                #region ApplyOutʵ�帳ֵ
                applyOut = new FS.HISFC.Models.Pharmacy.ApplyOut();
                try
                {
                    FS.HISFC.Models.Pharmacy.Item itemobj = itemMgr.GetItem(info.Material.ID);
                    if (itemobj == null)
                    {
                        this.Err = "��ȡҩƷ������Ϣʧ��" + this.Err;
                        return null;
                    }
                    applyOut.Item = itemobj;
                    applyOut.ApplyDept = operDept;						//������ң��������� 
                    applyOut.StockDept = operDept;						//��ҩҩ����ִ�п���
                    applyOut.SystemType = "R1";							//�������ͣ�"R1" 
                    applyOut.Operation.ApplyOper.OperTime = preparation.OperEnv.OperTime;            //����ʱ�䣽����ʱ��
                    applyOut.Days = 1;								//��ҩ����
                    applyOut.IsPreOut = false;							//�Ƿ�Ԥ�ۿ��
                    applyOut.IsCharge = true;							//�Ƿ��շ�

                    applyOut.PatientNO = info.Drug.ID;					//��Ʒ����

                    applyOut.PatientDept = operDept;						//���߹Һſ��� 
                    applyOut.State = "2";								//��������״̬:0����,1��ҩ,2��׼
                    applyOut.ShowState = "0";

                    applyOut.Item.User01 = info.NormativeQty.ToString();
                    applyOut.Operation.ApplyQty = preparation.PlanQty / 1000 * info.NormativeQty;

                    applyOut.BillNO = preparation.PlanNO;					//���뵥��

                    applyOut.Operation.ApproveOper.Dept = applyOut.StockDept;

                    applyOut.Operation.ApproveQty = applyOut.Operation.ApplyQty;
                    applyOut.DrugNO = "3003";
                }
                catch (Exception ex)
                {
                    this.Err = "��������ʵ�帳ֵʱ����" + ex.Message;
                    return null;
                }
                #endregion

                al.Add(applyOut);
            }
            return al;
        }

        /// <summary>
        /// ���ݳ���������Ϣ����ԭ�ϳ���/��������
        /// </summary>
        /// <param name="applyOut">����������Ϣ</param>
        /// <param name="isApply">�Ƿ���Ҫ���ͳ�������</param>
        /// <returns>�ɹ�������1 ʧ�ܷ���-1</returns>
        public int MaterialOutput(FS.HISFC.Models.Pharmacy.ApplyOut applyOut, bool isApply)
        {
            FS.HISFC.BizLogic.Pharmacy.Item itemMgr = new Item();
            //itemMgr.SetTrans(this.command.Transaction);	

            if (isApply)
            {
                if (itemMgr.InsertApplyOut(applyOut) == -1)
                    return -1;
            }
            else
            {
                if (itemMgr.Output(applyOut) != 1)
                    return -1;
            }
            return 1;
        }

        /// <summary>
        /// ԭ�ϳ���
        /// </summary>
        /// <param name="preparation">�Ƽ���ʵ��</param>
        /// <param name="isApply">�Ƿ���Ҫ������</param>
        /// <returns>�ɹ�������1 ʧ�ܷ���-1</returns>
        public int MaterialOutput(FS.HISFC.Models.Preparation.Preparation preparation, bool isApply)
        {
            ArrayList alMaterial = this.QueryMaterialApplyOut(preparation);
            if (alMaterial == null)
            {
                return -1;
            }

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alMaterial)
            {
                if (this.MaterialOutput(info, isApply) == -1)
                    return -1;
            }
            return 1;
        }

        /// <summary>
        /// ��Ʒ���
        /// </summary>
        /// <param name="pprList">�Ƽ�����</param>
        /// <param name="pprDept">�Ƽ���������</param>
        /// <param name="stockDept">������</param>
        /// <param name="isApply">�Ƿ���Ҫ���������</param>
        /// <returns></returns>
        public int DrugInput(List<FS.HISFC.Models.Preparation.Preparation> pprList, FS.FrameWork.Models.NeuObject pprDept,FS.FrameWork.Models.NeuObject stockDept,bool isApply)
        {
            FS.HISFC.BizLogic.Pharmacy.Item itemMgr = new Item();
            
            if (itemMgr.ProduceInput(pprList, pprDept, stockDept, isApply) == -1)
            {
                return -1;
            }
            return 1;
        }

        #endregion

        #region �Ƽ����۹�ʽ
        /// <summary>
		/// ���Ƽ����۹�ʽ�����¼�¼
		/// </summary>
		/// <param name="preparation"></param>
		/// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
		public int InsertCostPrice(FS.HISFC.Models.Preparation.CostPrice costPrice)
	
		{
			string strSQL="";
			if(this.GetSQL("Pharmacy.PPR.Preparation.InsertCostPrice",ref strSQL) == -1) return -1;
			try 
			{
                string [ ] strParm = this.myGetCostPriceParam ( costPrice ); //ȡ�����б�
				strSQL = string.Format(strSQL, strParm);				//�滻SQL����еĲ�����
			}
			catch(Exception ex) 
			{
                this.Err = "��ʽ����¼��SQl������ֵʱ����Pharmacy.PPR.Preparation.InsertCostPrice" + ex.Message;
				return -1;
			}

            return this.ExecNoQuery ( strSQL );

            
		}
        /// <summary>
        /// �޸��Ƽ����۹�ʽ��¼
        /// </summary>
        /// <param name="preparation"></param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int UpdateCostPrice ( FS.HISFC.Models.Preparation.CostPrice costPrice )
        {
            string strSQL = "";
            if ( this.GetSQL ( "Pharmacy.PPR.Preparation.UpdateCostPrice" , ref strSQL ) == -1 )
                return -1;
            try
            {
                string [ ] strParm = this.myGetCostPriceParam ( costPrice ); //ȡ�����б�
                strSQL = string.Format ( strSQL , strParm );			//�滻SQL����еĲ�����
            }
            catch ( Exception ex )
            {
                this.Err = "��ʽ����¼��SQl������ֵʱ����Pharmacy.PPR.Preparation.UpdateCostPrice" + ex.Message;
                return -1;
            }

            return this.ExecNoQuery ( strSQL );


        }
        /// <summary>
        /// ����Ƿ����
        /// </summary>
        /// <param name="drugCode"></param>
        public Boolean IsHaveCostPriceFormula(string drugCode)
        {
            string strSQL = "";
            if ( this.GetSQL ( "Pharmacy.PPR.Preparation.IsHaveCostPriceFormula" , ref strSQL ) == -1 )
                return false;
            try
            {

                strSQL = string.Format ( strSQL , drugCode );			//�滻SQL����еĲ�����
            }
            catch ( Exception ex )
            {
                this.Err = "��ʽ����¼��SQl������ֵʱ����Pharmacy.PPR.Preparation.IsHaveCostPriceFormula" + ex.Message;
                return false;
            }

            this.ExecQuery ( strSQL );
            try
            {
                while ( this.Reader.Read ( ) )
                {
                    string count = this.Reader [ 0 ].ToString();
                    if ( count=="0" )
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch ( Exception e )
            {
                this.Err = e.Message;
                return false;
            }
            finally
            {
                this.Reader.Close ( );
            }
            return true;
        }

        /// <summary>
        /// �����Ƽ�������Ϣ��ȡ��������
        /// </summary>
        /// <param name="preparation"></param>
        /// <returns>�ɹ����ض�Ӧ�Ĳ�������</returns>
        private string [ ] myGetCostPriceParam ( FS.HISFC.Models.Preparation.CostPrice costPrice )
        {

            string [ ] strParam = {
                                    costPrice.ID,
                                    costPrice.Name,
                                    costPrice.Specs,
                                    costPrice.PactUnit,
                                    costPrice.PactQty.ToString(),
                                    costPrice.MinUnit,
                                    costPrice.CostPriceFormula,
                                    costPrice.SalePriceFormula,
                                    costPrice.PriceSource,
                                    ((int)costPrice.PriceRate).ToString(),
                                    costPrice.Memo,
                                    costPrice.Extend,
                                    costPrice.Oper.Name,
                                    costPrice.Oper.OperTime.ToString()
                                   
                                   
									
								};
            return strParam;
        }
        /// <summary>
        /// ����ҩƷ�����ȡ�ɱ����㹫ʽ
        /// </summary>
        /// <param name="drugCode"></param>
        /// <returns></returns>
        public string GetCostPriceFormula ( string drugCode )
        {
            string strSQL = "";
            string costPriceFormula = string.Empty;
            if ( this.GetSQL ( "Pharmacy.PPR.Preparation.GetCostPriceFormula" , ref strSQL ) == -1 )
                return "";
                
            try
            {

                strSQL = string.Format ( strSQL , drugCode );
            }
            catch ( Exception ex )
            {
                this.Err = "��ʽ����¼��SQl������ֵʱ����Pharmacy.PPR.Preparation.GetCostPriceFormula" + ex.Message;
                
            }
            this.ExecQuery ( strSQL );
            try
            {

                while ( this.Reader.Read ( ) )
                {
                    costPriceFormula = this.Reader [ 0 ].ToString ( );
                }

            }
            catch ( Exception e )
            {
                this.Err = e.Message;

            }
            finally
            {
                this.Reader.Close ( );
            }
            return costPriceFormula;
        }
        #endregion
    }
}
