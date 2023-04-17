using System;
using System.Collections;
using FS.FrameWork.Function;
using FS.HISFC.Models.Fee.Inpatient;
using FS.HISFC.Models.RADT;
using System.Collections.Generic;
using System.Data;

namespace FS.HISFC.BizLogic.Fee
{
    /// <summary>
    /// ReturnApply<br></br>
    /// [��������: �˷����룬��׼ҵ����]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2006-10-01]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class ReturnApply : FS.FrameWork.Management.Database
    {
        #region ˽�з���

        #region �˷�����       

        /// <summary>
        /// ����SQL������˷�������Ϣ
        /// </summary>
        /// <param name="sql">ִ�е�SQL���</param>
        /// <param name="args">SQL���Ĳ���</param>
        /// <returns>�ɹ�:�˷�������Ϣ���� ʧ��:null û�в��ҵ����ݷ���Ԫ����Ϊ0��ArrayList</returns>
        private ArrayList QueryReturnApplysBySql(string sql, params string[] args)
        {
            //ִ��SQL���ʧ��
            if (this.ExecQuery(sql, args) == -1)
            {
                return null;
            }

            ArrayList returnApplys = new ArrayList();

            try
            {
                FS.HISFC.Models.Fee.ReturnApply returnApply;//��ʱ��ҩ����ʵ��

                //ѭ����ȡ����
                while (this.Reader.Read())
                {
                    returnApply = new FS.HISFC.Models.Fee.ReturnApply();

                    returnApply.ID = this.Reader[0].ToString();//������ˮ��
                    returnApply.ApplyBillNO = this.Reader[1].ToString();//���뵥�ݺ�
                    returnApply.Patient.ID = this.Reader[2].ToString();//סԺ��ˮ��
                    returnApply.Name = this.Reader[3].ToString();//��������
                    returnApply.IsBaby = NConvert.ToBoolean(this.Reader[4].ToString());//Ӥ�����
                    ((FS.HISFC.Models.RADT.PatientInfo)returnApply.Patient).PVisit.PatientLocation.Dept.ID = this.Reader[5].ToString();//�������ڿ���
                    ((FS.HISFC.Models.RADT.PatientInfo)returnApply.Patient).PVisit.PatientLocation.NurseCell.ID = this.Reader[6].ToString();//���ڲ���
                    //returnApply.Item.IsPharmacy = NConvert.ToBoolean(this.Reader[7].ToString());//ҩƷ��־1ҩƷ/0��ҩ
                    returnApply.Item.ItemType = (HISFC.Models.Base.EnumItemType)NConvert.ToInt32(this.Reader[7].ToString());//ҩƷ��־1ҩƷ/0��ҩ
                    //�����ҩƷ��Item����ΪҩƷʵ��
                    if (returnApply.Item.ItemType == HISFC.Models.Base.EnumItemType.Drug)
                    {
                        returnApply.Item = new FS.HISFC.Models.Pharmacy.Item();
                    }
                    else if (returnApply.Item.ItemType == HISFC.Models.Base.EnumItemType.UnDrug)//���򴴽�Ϊ��ҩƷʵ��
                    {
                        returnApply.Item = new FS.HISFC.Models.Fee.Item.Undrug();
                    }
                    else
                    {
                        returnApply.Item = new FS.HISFC.Models.FeeStuff.MaterialItem();
                    }
                    //���¸�ֵ
                    returnApply.Item.ItemType = (HISFC.Models.Base.EnumItemType)NConvert.ToInt32(this.Reader[7].ToString());//ҩƷ��־1ҩƷ/0��ҩ
                    returnApply.Item.ID = this.Reader[8].ToString();//��Ŀ����
                    returnApply.Item.Name = this.Reader[9].ToString();//��Ŀ����
                    returnApply.Item.Specs = this.Reader[10].ToString();//���
                    returnApply.Item.Price = NConvert.ToDecimal(this.Reader[11].ToString());//���ۼ�
                    returnApply.Item.Qty = NConvert.ToDecimal(this.Reader[12].ToString());//������ҩ���������Ը��������������
                    returnApply.Days = NConvert.ToDecimal(this.Reader[13].ToString());//����
                    returnApply.Item.PriceUnit = this.Reader[14].ToString(); //�Ƽ۵�λ
                    returnApply.ExecOper.Dept.ID = this.Reader[15].ToString();//ִ�п���
                    returnApply.Oper.ID = this.Reader[16].ToString();//����Ա����
                    returnApply.Oper.OperTime = NConvert.ToDateTime(this.Reader[17].ToString());//����ʱ��
                    returnApply.Oper.Dept.ID = this.Reader[18].ToString();//����Ա���ڿ���
                    returnApply.RecipeOper.Dept.ID = returnApply.Oper.Dept.ID;
                    returnApply.RecipeNO = this.Reader[19].ToString();//��Ӧ�շ���ϸ������
                    returnApply.SequenceNO = NConvert.ToInt32(this.Reader[20].ToString());//��Ӧ��������ˮ��
                    returnApply.ConfirmBillNO = this.Reader[21].ToString();
                    returnApply.IsConfirmed = NConvert.ToBoolean(Reader[22].ToString());//��ҩȷ�ϱ�ʶ 0δȷ��/1ȷ��
                    returnApply.ConfirmOper.Dept.ID = this.Reader[23].ToString();//ȷ�Ͽ��Ҵ���
                    returnApply.ConfirmOper.ID = this.Reader[24].ToString();//ȷ���˱���
                    returnApply.ConfirmOper.OperTime = NConvert.ToDateTime(this.Reader[25].ToString());//ȷ��ʱ��
                    returnApply.CancelType = (FS.HISFC.Models.Base.CancelTypes)NConvert.ToInt32(Reader[26].ToString());//�˷ѱ�ʶ 0δ�˷�/1�˷�
                    returnApply.CancelOper.ID = this.Reader[27].ToString();//�˷�ȷ����
                    returnApply.CancelOper.OperTime = NConvert.ToDateTime(this.Reader[28].ToString());//�˷�ȷ��ʱ��
                    returnApply.FeePack = this.Reader[29].ToString();
                    returnApply.Item.PackQty = NConvert.ToDecimal(this.Reader[30].ToString());
                    returnApply.UndrugComb.ID = this.Reader[31].ToString();
                    returnApply.UndrugComb.Name = this.Reader[32].ToString();

                    if (this.Reader.FieldCount > 33)
                    {
                        returnApply.Patient.PID.CardNO = this.Reader[33].ToString();
                    }
                    if (this.Reader.FieldCount > 34)
                    {
                        returnApply.Order.ID = this.Reader[34].ToString();
                    }
                    if (this.Reader.FieldCount > 35)
                    {
                        returnApply.ExecOrder.ID = this.Reader[35].ToString();
                    }
                    if (this.Reader.FieldCount > 36)
                    {
                        returnApply.FeeOper.OperTime = NConvert.ToDateTime(this.Reader[36].ToString());
                    }
                    if (this.Reader.FieldCount > 37)
                    {
                        returnApply.UndrugComb.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[37].ToString());
                    }
                    returnApplys.Add(returnApply);
                }//ѭ������

                this.Reader.Close();
            }//�׳�����
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                return null;
            }

            return returnApplys;
        }
        /// <summary>
        /// ���update����insert�˷�����Ĵ����������
        /// </summary>
        /// <param name="returnApply">�˷�����ʵ��</param>
        /// <returns>��������</returns>
        private string[] GetReturnApplyParams(FS.HISFC.Models.Fee.ReturnApply returnApply)
        {
            string deptCode = string.Empty;
            string nurseCellCode = string.Empty;

            if (returnApply.Patient is FS.HISFC.Models.RADT.PatientInfo)
            {
                deptCode = ((FS.HISFC.Models.RADT.PatientInfo)returnApply.Patient).PVisit.PatientLocation.Dept.ID;
                nurseCellCode = ((FS.HISFC.Models.RADT.PatientInfo)returnApply.Patient).PVisit.PatientLocation.NurseCell.ID;
            }
            else if (returnApply.Patient is FS.HISFC.Models.Registration.Register)
            {
                deptCode = ((FS.HISFC.Models.Registration.Register)returnApply.Patient).DoctorInfo.Templet.Dept.ID;
                nurseCellCode = string.Empty;
            }

            if (string.IsNullOrEmpty(deptCode) || string.IsNullOrEmpty(nurseCellCode))// {15CDA661-3D42-4c15-A32B-F88CC1CD7907}
            {

                string sql = string.Empty;
                if (returnApply.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
                {
                    sql = @"select INHOS_DEPTCODE,NURSE_CELL_CODE from fin_ipb_itemlist h 
                        where h.recipe_no = '{0}' 
                        and h.item_code = '{1}'
                        and h.sequence_no = '{2}'";
                }
                else if (returnApply.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    sql = @"select INHOS_DEPTCODE,NURSE_CELL_CODE from fin_ipb_medicinelist o 
                        where o.recipe_no = '{0}' 
                        and o.DRUG_CODE = '{1}'
                        and o.sequence_no = '{2}'";
                }
                sql = string.Format(sql, returnApply.RecipeNO, returnApply.Item.ID, returnApply.SequenceNO.ToString());
                DataSet dsResult = null;
                if (this.ExecQuery(sql, ref dsResult) != -1)
                {
                    DataTable dtResult = dsResult.Tables[0];
                    foreach (DataRow dr in dtResult.Rows)
                    {
                        deptCode = dr["INHOS_DEPTCODE"].ToString();
                        nurseCellCode = dr["NURSE_CELL_CODE"].ToString();
                    }
                }
            }
            string[] args =
			{
				//returnApply.ID ,                   //0������ˮ��
                this.GetSequence("Fee.ApplyReturn.CancelApplyNO"),
				returnApply.ApplyBillNO,              //1���뵥�ݺ�
				returnApply.Patient.ID,           //2סԺ��ˮ��
				returnApply.Patient.Name,                  //3��������
				NConvert.ToInt32(returnApply.IsBaby).ToString(),//4Ӥ�����
				deptCode,               //5�������ڿ���
				nurseCellCode,      //6���ڲ���
				//FS.FrameWork.Function.NConvert.ToInt32(returnApply.Item.IsPharmacy).ToString(),              //7ҩƷ��־,1ҩƷ/2��ҩ
                ((int)(returnApply.Item.ItemType)).ToString(),              //7ҩƷ��־,1ҩƷ/2��ҩ
				returnApply.Item.ID,               //8��Ŀ����
				returnApply.Item.Name,             //9��Ŀ����
				returnApply.Item.Specs,            //10���
				returnApply.Item.Price.ToString(), //11���ۼ�
				returnApply.Item.Qty.ToString(),//12������ҩ���������Ը��������������
				returnApply.Days.ToString(),       //13����
				returnApply.Item.PriceUnit,        //14�Ƽ۵�λ
				returnApply.ExecOper.Dept.ID,              //15ִ�п���
				this.Operator.ID,                  //16����Ա����
				returnApply.Oper.OperTime.ToString(),   //17����ʱ��
				returnApply.Oper.Dept.ID,              //18����Ա���ڿ���
				returnApply.RecipeNO,              //19��Ӧ�շ���ϸ������
				returnApply.SequenceNO.ToString(), //20��Ӧ��������ˮ��
                returnApply.ConfirmBillNO,//ȷ�ϵ���
				FS.FrameWork.Function.NConvert.ToInt32(returnApply.IsConfirmed).ToString(),           //21��ҩȷ�ϱ�ʶ 0δȷ��/1ȷ��
				returnApply.ConfirmOper.Dept.ID,           //22ȷ�Ͽ��Ҵ���
				returnApply.ConfirmOper.ID,           //23ȷ���˱���
				returnApply.ConfirmOper.OperTime.ToString(),//24ȷ��ʱ��
				((int)returnApply.CancelType).ToString(),            //25�˷ѱ�ʶ 0δ�˷�/1�˷�
				returnApply.CancelOper.ID,            //26�˷�ȷ����
				returnApply.CancelOper.OperTime.ToString(),  //27�˷�ȷ��ʱ��
                returnApply.FeePack,
                returnApply.Item.PackQty.ToString(),
                returnApply.UndrugComb.ID,
                returnApply.UndrugComb.Name,
                //returnApply.StockNo //���ʿ�����
                returnApply.Patient.PID.CardNO,
                returnApply.Order.ID,
                returnApply.ExecOrder.ID,
                returnApply.FeeOper.OperTime.ToString("yyyy-MM-dd HH:mm:ss")
			};

            return args;
        }

        /// <summary>
        /// ����˷�����SELECT���
        /// </summary>
        /// <returns>�ɹ�:����˷�����SELECT��� ʧ��:null</returns>
        private string GetReturnApplySelectSql()
        {
            string sql = string.Empty;

            //ȡSQL���
            if (this.Sql.GetCommonSql("Fee.ApplyReturn.GetApplyReturnList", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.ApplyReturn.GetApplyReturnList��SQL���!";

                return null;
            }

            return sql;
        }

        /// <summary>
        /// ����˷�������Ϣ
        /// </summary>
        /// <param name="whereIndex">where����</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�: �˷�������Ϣ ʧ��: null û���ҵ�����ArrayList.Count = 0</returns>
        private ArrayList QueryReturnApplys(string whereIndex, params string[] args)
        {
            string select = string.Empty;
            string where = string.Empty;

            select = this.GetReturnApplySelectSql();

            if (this.Sql.GetCommonSql(whereIndex, ref where) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + whereIndex + "��SQL���";

                return null;
            }

            return this.QueryReturnApplysBySql(select + " " + where, args);
        }

        #endregion

        #region �����˷�����

        /// <summary>
        /// ���update����insert�����˷�����Ĵ����������
        /// </summary>
        /// <param name="returnApplyMet"></param>
        /// <returns></returns>
        private string[] GetReturnApplyMetParams(FS.HISFC.Models.Fee.ReturnApplyMet returnApplyMet)
        {
            string[] args =
			{
                returnApplyMet.ApplyNo, //APPLY_NO ������ˮ��
                returnApplyMet.OutNo,//OUT_NO ���ⵥ��ˮ��
                returnApplyMet.StockNo,//STOCK_NO ������
                returnApplyMet.RecipeNo,//RECIPE_NO ������
                returnApplyMet.SequenceNo,//SEQUENCE_NO ��������Ŀ��ˮ��
                returnApplyMet.Item.ID,//ITEM_CODE ��Ʒ����
                returnApplyMet.Item.Name,//ITEM_NAME ��Ʒ����
                returnApplyMet.Item.Specs,//SPECS	���
                returnApplyMet.Item.PriceUnit,//STAT_UNIT ������λ
                returnApplyMet.Item.Price.ToString(),//SALE_PRICE ���۵���
                returnApplyMet.Item.Qty.ToString(),//OUT_NUM ��������
                ((int)returnApplyMet.ApplyFlag).ToString()//CANCELFLAF ȷ�ϱ�ʶ��0���룬1ȡ����2ȷ�ϣ�
			};

            return args;
        }

        /// <summary>
        /// ��������˷�����SELECT���
        /// </summary>
        /// <returns>�ɹ�:����˷�����SELECT��� ʧ��:null</returns>
        private string GetReturnApplyMetSelectSql()
        {
            string sql = string.Empty;

            //ȡSQL���
            if (this.Sql.GetCommonSql("Fee.ApplyReturn.GetApplyReturnMetList", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.ApplyReturn.GetApplyReturnMetList��SQL���!";

                return null;
            }

            return sql;
        }

        /// <summary>
        /// ����SQL������˷�������Ϣ
        /// </summary>
        /// <param name="sql">ִ�е�SQL���</param>
        /// <param name="args">SQL���Ĳ���</param>
        /// <returns>�ɹ�:�˷�������Ϣ���� ʧ��:null û�в��ҵ����ݷ���Ԫ����Ϊ0��ArrayList</returns>
        private List<HISFC.Models.Fee.ReturnApplyMet> QueryReturnApplyMetBySql(string sql, params string[] args)
        {
            //ִ��SQL���ʧ��
            if (this.ExecQuery(sql, args) == -1)
            {
                return null;
            }

            //ArrayList returnApplyMetList = new ArrayList();
            List<HISFC.Models.Fee.ReturnApplyMet> returnApplyMetList = new List<FS.HISFC.Models.Fee.ReturnApplyMet>();
            try
            {
                FS.HISFC.Models.Fee.ReturnApplyMet returnApplyMet;//��ʱ��ҩ����ʵ��

                //ѭ����ȡ����
                while (this.Reader.Read())
                {
                    returnApplyMet = new FS.HISFC.Models.Fee.ReturnApplyMet();

                    returnApplyMet.ApplyNo = this.Reader[0].ToString();//APPLY_NO ������ˮ��
                    returnApplyMet.OutNo = this.Reader[1].ToString();//OUT_NO ���ⵥ��ˮ��
                    returnApplyMet.StockNo = this.Reader[2].ToString();//STOCK_NO ������
                    returnApplyMet.RecipeNo = this.Reader[3].ToString();//RECIPE_NO ������
                    returnApplyMet.SequenceNo = this.Reader[4].ToString();//SEQUENCE_NO ��������Ŀ��ˮ��
                    returnApplyMet.Item.ID = this.Reader[5].ToString();//ITEM_CODE ��Ʒ����
                    returnApplyMet.Item.Name = this.Reader[6].ToString();//ITEM_NAME ��Ʒ����
                    returnApplyMet.Item.Specs = this.Reader[7].ToString();//SPECS	���
                    returnApplyMet.Item.PriceUnit = this.Reader[8].ToString();//STAT_UNIT ������λ
                    returnApplyMet.Item.Price = FrameWork.Function.NConvert.ToDecimal(this.Reader[9].ToString());//SALE_PRICE ���۵���
                    returnApplyMet.Item.Qty = FrameWork.Function.NConvert.ToDecimal(this.Reader[10].ToString());//OUT_NUM ��������
                    returnApplyMet.ApplyFlag = (HISFC.Models.Base.CancelTypes)(FrameWork.Function.NConvert.ToInt32(this.Reader[11].ToString()));//CANCELFLAF ȷ�ϱ�ʶ��0���룬1ȡ����2ȷ�ϣ�

                    returnApplyMetList.Add(returnApplyMet);
                }//ѭ������

                this.Reader.Close();
            }//�׳�����
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                return null;
            }

            return returnApplyMetList;
        }

        /// <summary>
        /// ����SQL������˷�������Ϣ
        /// </summary>
        /// <param name="sql">ִ�е�SQL���</param>
        /// <param name="args">SQL���Ĳ���</param>
        /// <returns>�ɹ�:�˷�������Ϣ���� ʧ��:null û�в��ҵ����ݷ���Ԫ����Ϊ0��ArrayList</returns>
        private List<HISFC.Models.FeeStuff.Output> QueryQueryOutPutFromApplyBySql(string sql, params string[] args)
        {
            //ִ��SQL���ʧ��
            if (this.ExecQuery(sql, args) == -1)
            {
                return null;
            }

            //ArrayList returnApplyMetList = new ArrayList();
            List<HISFC.Models.FeeStuff.Output> list = new List<HISFC.Models.FeeStuff.Output>();
            try
            {
                HISFC.Models.FeeStuff.Output outItem;//��ʱ��ҩ����ʵ��

                //ѭ����ȡ����
                while (this.Reader.Read())
                {
                    outItem = new FS.HISFC.Models.FeeStuff.Output();
                    outItem.ID = this.Reader[1].ToString();//OUT_NO ���ⵥ��ˮ��
                    outItem.StoreBase.StockNO = this.Reader[2].ToString();//STOCK_NO ������
                    outItem.RecipeNO = this.Reader[3].ToString();//RECIPE_NO ������
                    outItem.SequenceNO = NConvert.ToInt32(this.Reader[4]);//SEQUENCE_NO ��������Ŀ��ˮ��
                    outItem.StoreBase.Item.ID = this.Reader[5].ToString();//ITEM_CODE ��Ʒ����
                    outItem.StoreBase.Item.Name = this.Reader[6].ToString();//ITEM_NAME ��Ʒ����
                    outItem.StoreBase.Item.Specs = this.Reader[7].ToString();//SPECS	���
                    outItem.StoreBase.Item.PriceUnit = this.Reader[8].ToString();//STAT_UNIT ������λ
                    outItem.StoreBase.Item.Price = FrameWork.Function.NConvert.ToDecimal(this.Reader[9].ToString());//SALE_PRICE ���۵���
                    outItem.StoreBase.Item.Qty = FrameWork.Function.NConvert.ToDecimal(this.Reader[10].ToString());//OUT_NUM ��������
                    list.Add(outItem);
                }//ѭ������

                this.Reader.Close();
            }//�׳�����
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                return null;
            }

            return list;
        }
        /// <summary>
        /// ���������˷�������Ϣ������ʳ�����Ϣ
        /// </summary>
        /// <param name="whereIndex">where����</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�: �˷�������Ϣ ʧ��: null û���ҵ�����ArrayList.Count = 0</returns>
        private List<HISFC.Models.FeeStuff.Output> QueryOutPutByApply(string whereIndex, params string[] args)
        {
            string select = string.Empty;
            string where = string.Empty;

            select = this.GetReturnApplyMetSelectSql();

            if (this.Sql.GetCommonSql(whereIndex, ref where) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + whereIndex + "��SQL���";

                return null;
            }

            return this.QueryQueryOutPutFromApplyBySql(select + " " + where, args);
        }

        /// <summary>
        /// ��������˷�������Ϣ
        /// </summary>
        /// <param name="whereIndex">where����</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�: �˷�������Ϣ ʧ��: null û���ҵ�����ArrayList.Count = 0</returns>
        private List<HISFC.Models.Fee.ReturnApplyMet> QueryReturnApplyMet(string whereIndex, params string[] args)
        {
            string select = string.Empty;
            string where = string.Empty;

            select = this.GetReturnApplyMetSelectSql();

            if (this.Sql.GetCommonSql(whereIndex, ref where) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + whereIndex + "��SQL���";

                return null;
            }

            return this.QueryReturnApplyMetBySql(select + " " + where, args);
        }

        #endregion

        #endregion

        #region ���з���

        #region �˷�����

        /// <summary>
        /// ���ݷ�Ʊ�Ż�ȡ��ȷ��(��ҩȷ��/�˷�ȷ��)��Ϣ
        /// </summary>
        ///  <param name="inpatientNo">������ˮ��</param>
        /// <param name="invoiceNo">��Ʊ��</param>
        /// <param name="isConfirm">�Ƿ���ҩȷ��/ҽ���˷�ȷ��</param>
        /// <param name="isCharged">�Ƿ��˷�ȷ��</param>
        /// <param name="drugFlag">ҩƷ��� 1 ҩƷ	0 ��ҩƷ A ȫ��</param>
        /// <returns>�ɹ��������д�ȷ���������� ʧ�ܷ���null</returns>
        public ArrayList GetList(string inpatientNo, string invoiceNo, bool isConfirm, bool isCharged, string drugFlag)
        {
            ArrayList al = new ArrayList();

            //string strWhere = ""; //Where����
            string chargeFlag = "0"; //�˷�״̬��0δ�˷ѣ����룩��1���˷ѣ���׼��
            string confrmFlag = "0"; //��ҩ״̬��0δȷ�� 1 ��ȷ��

            //��������״̬����
            if (isConfirm)
                confrmFlag = "1";
            else
                confrmFlag = "0";
            if (isCharged)
                chargeFlag = "1";
            else
                chargeFlag = "0";

            //ȡ�˷���������			
            return this.QueryReturnApplys("Fee.ApplyReturn.GetApplyReturnList.Where.ByInvoice", inpatientNo, invoiceNo, confrmFlag, chargeFlag, drugFlag);
        }

        /// <summary>
        /// ȡ�˷�������ˮ��
        /// </summary>
        /// <returns>"-1"����oterhs �ɹ�</returns>
        public string GetReturnApplySequence()
        {
            return this.GetSequence("Fee.ApplyReturn.GetApplyReturnID");
        }

        /// <summary>
        /// ��ȡ�˷����뵥��
        /// </summary>
        /// <returns></returns>
        public string GetReturnApplyBillCode()
        {
            return this.GetSequence("Fee.ApplyReturn.GetBillCode");
        }

        /// <summary>
        /// ���ݻ���סԺ��ˮ�ţ�ȡ�˻��ߵ��˷���������
        /// </summary>
        /// <param name="inpatientNO">����סԺ��ˮ��</param>
        /// <returns>�ɹ�:�˷��������� ʧ��: null û�в��ҵ����ݷ���Ԫ����Ϊ0��ArrayList</returns>
        public ArrayList QueryReturnApplys(string inpatientNO)
        {
            return this.QueryReturnApplys(string.Empty, inpatientNO);
        }

        /// <summary>
        /// ��õ����˷�������Ϣ
        /// </summary>
        /// <param name="inpatientNO">������ˮ��</param>
        /// <param name="applySequence">������ˮ��</param>
        /// <returns>�ɹ� �˷�����ʵ�� ʧ�� null</returns>
        public FS.HISFC.Models.Fee.ReturnApply GetReturnApplyByApplySequence(string inpatientNO, string applySequence)
        {
            ArrayList tempList = this.QueryReturnApplys("Fee.ApplyReturn.GetApplyReturnWhere", inpatientNO, applySequence);

            if (tempList == null || tempList.Count == 0)
            {
                return null;
            }
            else
            {
                return tempList[0] as FS.HISFC.Models.Fee.ReturnApply;
            }
        }

        /// <summary>
        /// ���ݻ���סԺ��ˮ�ţ�ȡ�˻��ߵ��˷���������,�����������Ƿ�ȷ��
        /// </summary>
        /// <param name="inpatientNO">����סԺ��ˮ��</param>
        /// <param name="isCharged">�Ƿ�ȷ�ϵ�����</param>
        /// <returns>�ɹ�:�˷��������� ʧ��: null û�в��ҵ����ݷ���Ԫ����Ϊ0��ArrayList</returns>
        public ArrayList QueryReturnApplys(string inpatientNO, bool isCharged)
        {
            return this.QueryReturnApplys("Fee.ApplyReturn.GetApplyReturnList.Where", inpatientNO, NConvert.ToInt32(isCharged).ToString());
        }

        /// <summary>
        /// ���ݻ���סԺ��ˮ�ţ�ȡ�˻��ߵ��˷���������
        /// </summary>
        /// <param name="inpatientNO">����סԺ��ˮ��</param>
        /// <param name="deptCode">�˷Ѳ���Ա���ڿ���</param>
        /// <param name="isCharged">�Ƿ�ȷ������</param>
        /// <param name="isPhamacy">�Ƿ�ҩƷ</param>
        /// <returns>�ɹ�:�˷��������� ʧ��: null û�в��ҵ����ݷ���Ԫ����Ϊ0��ArrayList</returns>
        public ArrayList QueryReturnApplys(string inpatientNO, string deptCode, bool isCharged, bool isPhamacy)
        {
            return this.QueryReturnApplys("Fee.ApplyReturn.GetApplyReturnList.Where.isUndrug", inpatientNO, deptCode, NConvert.ToInt32(isCharged).ToString(),
                NConvert.ToInt32(isPhamacy).ToString());
        }

        /// <summary>
        /// ���ݻ���סԺ��ˮ�ţ�ȡ�˻��ߵ��˷���������
        /// </summary>
        /// <param name="inpatientNO">����סԺ��ˮ��</param>
        /// <param name="deptCode">�˷Ѳ���Ա���ڿ���</param>
        /// <param name="isCharged">�Ƿ�ȷ������</param>
        /// <param name="ItemType">��Ŀ���</param>
        /// <returns>�ɹ�:�˷��������� ʧ��: null û�в��ҵ����ݷ���Ԫ����Ϊ0��ArrayList</returns>
        public ArrayList QueryReturnApplys(string inpatientNO, bool isCharged, HISFC.Models.Base.EnumItemType ItemType)
        {
            return this.QueryReturnApplys("Fee.ApplyReturn.GetApplyReturnList.Where.isUndrug", inpatientNO, "AAAA", NConvert.ToInt32(isCharged).ToString(),
                ((int)ItemType).ToString());
        }

        /// <summary>
        /// ���ݻ���סԺ��ˮ�ţ�ȡ�˻��ߵ��˷���������
        /// </summary>
        /// <param name="inpatientNO">����סԺ��ˮ��</param>
        /// <param name="isCharged">�Ƿ�ȷ������</param>
        /// <param name="isPhamacy">�Ƿ��ҩƷ</param>
        /// <returns>�ɹ������˷��������� ʧ�ܷ���null</returns>
        public ArrayList QueryReturnApplys(string inpatientNO, bool isCharged,bool isPhamacy)
        {
            return this.QueryReturnApplys(inpatientNO, "AAAA", isCharged, isPhamacy);
        }

        /// <summary>
        /// ���ݻ���סԺ��ˮ�š��Ƿ�ȷ�� ���Ƿ�ҩȡ����ҩƷ�˷���������
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <param name="isCharged">�Ƿ��˷�ȷ��</param>
        /// <param name="isConfirm">�Ƿ�ҩ</param>
        /// <returns>�ɹ�:�˷��������� ʧ��: null û�в��ҵ����ݷ���Ԫ����Ϊ0��ArrayList</returns>
        public ArrayList QueryDrugReturnApplys(string inpatientNO, bool isCharged, bool isConfirm)
        {
            return this.QueryReturnApplys("Fee.ApplyReturn.GetApplyReturnList.Where.DrugList", inpatientNO, NConvert.ToInt32(isCharged).ToString(),
                NConvert.ToInt32(isConfirm).ToString());
        }

        /// <summary>
        /// ����һ���˷������¼
        /// </summary>
        /// <param name="returnApply">�˷�����ʵ��</param>
        /// <returns>�ɹ�: 1 ʧ��: -1 û�в������� : 0</returns>
        public int InsertReturnApply(FS.HISFC.Models.Fee.ReturnApply returnApply)
        {
            return this.UpdateSingleTable("Fee.ApplyReturn.InsertApplyReturn", this.GetReturnApplyParams(returnApply));
        }

        /// <summary>
        /// ȡ���˷����� ��״̬Ϊ��Ч 2
        /// </summary>
        /// <param name="applySequence">������ˮ��</param>
        /// <param name="operCode">ȡ����</param>
        /// <returns>�ɹ�1 ʧ�ܣ�1 �޼�¼ 0</returns>
        public int CancelReturnApply(string applySequence, string operCode)
        {
            return this.UpdateSingleTable("Fee.ApplyReturn.CancelApplyReturn", applySequence, operCode);
        }

        /// <summary>
        /// ɾ��һ���˷������¼
        /// </summary>
        /// <param name="applySequence">�˷�������ˮ��</param>
        /// <returns>�ɹ�1 ʧ�ܣ�1 �޼�¼ 0</returns>
        public int DeleteReturnApply(string applySequence)
        {
            return this.UpdateSingleTable("Fee.ApplyReturn.DeleteApplyReturn", applySequence);
        }

        /// <summary>
        /// ȷ���˷�����
        /// </summary>
        /// <param name="returnApply">�˷�����ʵ��</param>
        /// <returns>�ɹ�1 ʧ�ܣ�1 �޼�¼ 0</returns>
        public int ConfirmApply(FS.HISFC.Models.Fee.ReturnApply returnApply)
        {
            return this.UpdateSingleTable("Fee.ApplyReturn.ConfirmApply", returnApply.ID, returnApply.ApplyBillNO, ((int)returnApply.CancelType).ToString(),
               returnApply.ConfirmOper.ID, returnApply.ConfirmOper.OperTime.ToString(), returnApply.Item.ID);
            
            
        }

        /// <summary>
        /// ��ѯһ��ʱ���������˷ѵĻ���
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="confirmFlag">ȷ�ϱ��</param>
        /// <returns>�ɹ�:�˷�����Ļ�����Ϣ���� ʧ��: null û�в��ҵ����ݷ���Ԫ����Ϊ0��ArrayList</returns>
        public ArrayList QueryAppliedPatientsByTime(string beginTime, string endTime, string confirmFlag)
        {

            string sql = string.Empty;//��ѯ���뻼�ߵ�SQL���

            ArrayList patients = new ArrayList();

            if (this.Sql.GetCommonSql("Fee.ApplyReturn.QueryPatients", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.ApplyReturn.QueryPatients��SQL���!";

                return null;
            }

            try
            {
                sql = string.Format(sql, beginTime.ToString(), endTime.ToString(), confirmFlag);
            }
            catch (Exception e)
            {
                this.Err = e.Message;

                return null;
            }

            try
            {
                //ִ��SQL���
                if (this.ExecQuery(sql) == -1)
                {
                    return null;
                }

                //ѭ����ȡ����
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject patient = new FS.FrameWork.Models.NeuObject();

                    patient.ID = this.Reader[2].ToString();//סԺ��ˮ��
                    patient.Name = this.Reader[3].ToString();//����
                    patient.Memo = this.Reader[4].ToString();//��ʿվ����

                    patients.Add(patient);
                }//ѭ������

                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = e.Message;

                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                return null;
            }

            return patients;
        }

        /// <summary>
        /// �˷�����
        /// </summary>
        /// <param name="patient">���߻�����Ϣʵ��</param>
        /// <param name="feeItemList">������ϸ</param>
        /// <param name="operTime">����ʱ��</param>
        /// <returns>�ɹ�: 1 ʧ��: -1 û�в���������Ϣ: 0</returns>
        public int Apply(PatientInfo patient, FeeItemList feeItemList, DateTime operTime)
        {
            //�����˷�����ʵ��
            FS.HISFC.Models.Fee.ReturnApply applyReturn = new FS.HISFC.Models.Fee.ReturnApply();

            //������ʵ���Ӧ���˷�����ʵ��
            applyReturn.Patient = patient.Clone();
            applyReturn.IsBaby = feeItemList.IsBaby;//Ӥ�����
            applyReturn.Item = feeItemList.Item;//��Ŀ����
            applyReturn.Days = feeItemList.Days;//����
            applyReturn.ExecOper.Dept = feeItemList.ExecOper.Dept;//ִ�п���
            applyReturn.ExecOrder.ID = !string.IsNullOrEmpty(feeItemList.ExecOrder.ID)?feeItemList.ExecOrder.ID:feeItemList.FeeOper.OperTime.ToString();//ִ�е���ˮ�Ż��շ�ʱ��
            applyReturn.CancelOper.OperTime = operTime;//����ʱ��
            applyReturn.CancelOper.Dept = ((FS.HISFC.Models.Base.Employee)this.Operator).Dept;//����Ա���ڿ���
            applyReturn.RecipeNO = feeItemList.RecipeNO;//��Ӧ�շ���ϸ������
            applyReturn.SequenceNO = feeItemList.SequenceNO;//��Ӧ��������ˮ��
            applyReturn.IsConfirmed = feeItemList.IsConfirmed;//��ҩȷ��
            //�˷�����ȷ�ϵ��ݺ�
            applyReturn.ApplyBillNO = feeItemList.User02;//ȷ�ϵ���
            applyReturn.ConfirmOper.Dept = ((FS.HISFC.Models.Base.Employee)this.Operator).Dept;//ȷ�Ͽ��Ҵ���
            applyReturn.ConfirmOper.ID = this.Operator.ID;//ȷ���˱���
            applyReturn.ConfirmOper.OperTime = operTime;//ȷ��ʱ��
            applyReturn.CancelType = FS.HISFC.Models.Base.CancelTypes.Canceled; //.Valid;//�˷ѱ�ʶ
            //applyReturn.Item.IsPharmacy = feeItemList.Item.IsPharmacy;
            applyReturn.Item.ItemType = feeItemList.Item.ItemType;
            applyReturn.UndrugComb = feeItemList.UndrugComb;
            //������Ϣ
            applyReturn.MateList = feeItemList.MateList;
            //ҽ����Ϣ
            applyReturn.Order.ID = feeItemList.Order.ID;
            applyReturn.ExecOrder.ID = feeItemList.ExecOrder.ID;
            applyReturn.FeeOper.OperTime = feeItemList.FeeOper.OperTime;

            int resultValue = 0;
            //�˷������
            resultValue = this.InsertReturnApply(applyReturn);
            if (resultValue < 0) return -1;
            //����������ϸ��
            if (applyReturn.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                resultValue = this.InsertReturnApplyMet(applyReturn);
                if (resultValue < 0) return -1;
            }
            return 1;
        }

        /// <summary>
        /// ���ݲ����š������š�������ˮ�š�ȷ�ϱ�ǲ�ѯ�˷�������Ϣ
        /// </summary>
        /// <param name="cardNO">������</param>
        /// <param name="recipeNO">������</param>
        /// <param name="seqenceNO">������ˮ��</param>
        /// <param name="confirmFlag">ȷ�ϱ��</param>
        /// <returns></returns>
        public ArrayList QueryReturnApplys(string cardNO, string recipeNO, int seqenceNO, string confirmFlag)
        {
            string sql = string.Empty;

            //ȡSQL���
            if (this.Sql.GetCommonSql("Fee.ApplyReturn.GetDirectFeeApplyReturnList", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.ApplyReturn.GetApplyReturnListByCardNO��SQL���!";

                return null;
            }
            return QueryReturnApplysBySql(sql, cardNO,
                                         recipeNO,
                                         seqenceNO.ToString(),
                                         confirmFlag);
        }

        /// <summary>
        /// ���ݴ����źʹ�������ˮ�Ų�ѯ�˷����룬����ҽ��վ�ж�ҩ���Ƿ��Ѿ������˷������ˡ�
        /// create by lh 10-05-24
        /// {08CC9125-1F28-4f5d-BF05-517108088111}
        /// </summary>
        /// <param name="recipeNo"></param>
        /// <param name="sequenceNo"></param>
        /// <returns></returns>
        public ArrayList QueryReturnApplysByRecipeNoSequenceNo(string inpatientNO, string recipeNo, string sequenceNo)
        {
            return this.QueryReturnApplys("Fee.ApplyReturn.GetApplyReturnList.Where.recipe", inpatientNO, recipeNo, sequenceNo);
        }

        /// <summary>
        /// ��ȡ�˷ѵ����б�
        /// </summary>
        /// <param name="inPatientNo">סԺ��ˮ��</param>
        /// <param name="deptCode">�˷ѿ���</param>
        /// <param name="dtBegin">��ѯ��ʼʱ��</param>
        /// <param name="dtEnd">��ѯ��ֹʱ��</param>
        /// <param name="chargeFlag">�շѱ�־</param>
        /// <returns>�ɹ�����nueobject���� ʧ�ܷ���null</returns>
        public ArrayList GetList(string inPatientNo, string deptCode, System.DateTime dtBegin, System.DateTime dtEnd, string chargeFlag)
        {
            string strSQL = "";
            ArrayList al = new ArrayList();

            //ȡSQL���
            if (this.Sql.GetCommonSql("Fee.ApplyReturn.GetBillList", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Fee.ApplyReturn.GetBillList�ֶ�!";
                return null;
            }

            try
            {
                strSQL = string.Format(strSQL, inPatientNo, deptCode, dtBegin.ToString(), dtEnd.ToString(), chargeFlag);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "�����������ȷ��Fee.ApplyReturn.GetBillList" + ex.Message;
                return null;
            }
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "ȡ��ҩ����ʱ����" + this.Err;
                return null;
            }
            try
            {
                FS.FrameWork.Models.NeuObject info;
                while (this.Reader.Read())
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = this.Reader[0].ToString();		//�˷ѵ��ݺ�
                    info.Memo = this.Reader[1].ToString();		//ҩƷ��־ 1 ҩƷ 0 ��ҩƷ
                    info.User01 = this.Reader[2].ToString();	//�˷�ȷ�ϱ�־ 1 ȷ�� 0 δȷ��
                    info.User02 = this.Reader[3].ToString();	//��ע ��סԺ�� ����ҩ��
                    al.Add(info);
                }
                this.Reader.Close();
            }//�׳�����
            catch (Exception ex)
            {
                this.Err = "����˿�����ʱ����" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            return al;
        }
        #endregion

        #region �����˷�����

        /// <summary>
        /// ����һ�������˷������¼
        /// </summary>
        /// <param name="returnApply">�˷�����ʵ��</param>
        /// <returns>�ɹ�: 1 ʧ��: -1 û�в������� : 0</returns>
        //{CEA4E2A5-A045-4823-A606-FC5E515D824D}
        public int InsertReturnApplyMet(FS.HISFC.Models.Fee.ReturnApplyMet returnApplyMet)
        {
            return this.UpdateSingleTable("Fee.ApplyReturn.InsertApplyReturnMet", this.GetReturnApplyMetParams(returnApplyMet));
        }

        /// <summary>
        /// ���ݷ�����Ϣ���������˷������¼
        /// </summary>
        /// <param name="feeItemList">������Ϣ</param>
        /// <returns>1 �ɹ� -1ʧ��</returns>
        //{CEA4E2A5-A045-4823-A606-FC5E515D824D}
        public int InsertReturnApplyMet(FS.HISFC.Models.Fee.ReturnApply applyItem)
        {

            List<FS.HISFC.Models.Fee.ReturnApplyMet> list = GetApplyMetItem(applyItem);
            if (list.Count == 0) return 1;
            int resultValue = 0;
            foreach (FS.HISFC.Models.Fee.ReturnApplyMet returnApplyMet in list)
            {
                resultValue = InsertReturnApplyMet(returnApplyMet);
                if (resultValue < 0)
                    return -1;
            }
            return 1;
            
        }

        /// <summary>
        /// ���ݷ�����Ϣ���������˷�����ʵ�弯��
        /// </summary>
        /// <param name="feeItemList">������Ϣ</param>
        /// <returns>�˷�����ʵ�弯��</returns>
        //{CEA4E2A5-A045-4823-A606-FC5E515D824D}
        private List<FS.HISFC.Models.Fee.ReturnApplyMet> GetApplyMetItem(FS.HISFC.Models.Fee.ReturnApply applyItem)
        {
            List<FS.HISFC.Models.Fee.ReturnApplyMet> list = new List<FS.HISFC.Models.Fee.ReturnApplyMet>();
            FS.HISFC.Models.Fee.ReturnApplyMet item = null;
            string recipeNo = applyItem.RecipeNO;
            string sequenceNo = applyItem.SequenceNO.ToString();
            foreach (HISFC.Models.FeeStuff.Output outItem in applyItem.MateList)
            {
                item = new FS.HISFC.Models.Fee.ReturnApplyMet();
                item.Item = outItem.StoreBase.Item;
                item.Item.Qty = outItem.ReturnApplyNum;
                item.RecipeNo = recipeNo;
                item.SequenceNo = sequenceNo;
                item.StockNo = outItem.StoreBase.StockNO;
                item.OutNo = outItem.ID;
                item.ApplyNo = applyItem.ID;
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// ����һ�������˷������¼
        /// </summary>
        /// <param name="returnApply">�˷�����ʵ��</param>
        /// <returns>�ɹ�: 1 ʧ��: -1 û�в������� : 0</returns>
        //{CEA4E2A5-A045-4823-A606-FC5E515D824D}
        public int UpdateReturnApplyMet(FS.HISFC.Models.Fee.ReturnApplyMet returnApplyMet)
        {
            return this.UpdateSingleTable("Fee.ApplyReturn.UpdateApplyReturnMet", this.GetReturnApplyMetParams(returnApplyMet));
        }

        /// <summary>
        /// ���������˷�����״̬
        /// </summary>
        /// <param name="returnApplyMetList">�����˷�����ʵ�弯��</param>
        /// <returns>1�ɹ� -1ʧ��</returns>
        //{CEA4E2A5-A045-4823-A606-FC5E515D824D}
        public int UpdateReturnApplyState(List<FS.HISFC.Models.Fee.ReturnApplyMet> returnApplyMetList,FS.HISFC.Models.Base.CancelTypes applyFalg)
        {
            foreach (FS.HISFC.Models.Fee.ReturnApplyMet returnApplyMet in returnApplyMetList)
            {
                returnApplyMet.ApplyFlag = applyFalg;
                if (UpdateReturnApplyMet(returnApplyMet) <= 0)
                    return -1;
            }
            return 1;
        }

        /// <summary>
        /// ����������Ż�ȡ�����˷������¼
        /// </summary>
        /// <param name="applyNo">�������</param>
        /// <returns>�����˷������¼</returns>
        //{CEA4E2A5-A045-4823-A606-FC5E515D824D}
        public List<HISFC.Models.Fee.ReturnApplyMet> QueryReturnApplyMetByApplyNo(string applyNo,HISFC.Models.Base.CancelTypes applyFlag)
        {
            return this.QueryReturnApplyMet("Fee.ApplyReturn.GetApplyReturnMetList.Where.ApplyNoAndState", applyNo, ((int)applyFlag).ToString());
        }

        /// <summary>
        /// ����������Ż�ȡ�����˷������¼
        /// </summary>
        /// <param name="applyNo">�������</param>
        /// <param name="applyFlag">ȷ�ϱ�ʶ��0���룬1�˷ѣ�2���ϣ�</param>
        /// <returns></returns>
        //{CEA4E2A5-A045-4823-A606-FC5E515D824D}
        public List<HISFC.Models.FeeStuff.Output> QueryOutPutByApplyNo(string applyNo, HISFC.Models.Base.CancelTypes applyFlag)
        {
            return this.QueryOutPutByApply("Fee.ApplyReturn.GetApplyReturnMetList.Where.ApplyNoAndState", applyNo, ((int)applyFlag).ToString());
        }

        #endregion

        #endregion

        #region ���Ϸ���

        /// <summary>
        /// ��ѯһ��ʱ���������˷ѵĻ���
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ConfirmFlag"></param>
        /// <returns></returns>
        [Obsolete("����,ʹ��QueryAppliedPatientsbyTime()", true)]
        public ArrayList QueryPatients(string BeginDate, string EndDate,string ConfirmFlag)
        {
            #region sql
            //			SELECT distinct parent_code,   --����ҽ�ƻ�������
            //					current_code,   --����ҽ�ƻ�������
            //					inpatient_no,   --סԺ��ˮ��
            //					name,   --��������
            //					nurse_cell_code    --���ڲ���
            //				FROM met_nui_cancelitem   --�����˷������
            //				WHERE parent_code='[��������]' and current_code='[��������]'
            //				and oper_date>=to_date('{0}','yyyy-mm-dd HH24:mi:ss')
            //				and oper_date<=to_date('{1}','yyyy-mm-dd HH24:mi:ss')
            //				and confirm_flag='{3}'
            #endregion
            string strSql = "";
            ArrayList al = new ArrayList();

            if (Sql.GetSql("Fee.ApplyReturn.QueryPatients", ref strSql) == -1)
            {
                this.Err = "û���ҵ�Fee.ApplyReturn.QueryPatients��SQL���!";
                return null;
            }
            try
            {
                strSql = string.Format(strSql, BeginDate, EndDate, ConfirmFlag);
            }
            catch (Exception e)
            {
                this.Err = "�����������ȷ��Fee.ApplyReturn.QueryPatients" + e.Message;
                return null;
            }
            try
            {
                if (this.ExecQuery(strSql) == -1)
                {
                    return null;
                }
                while (Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = Reader[2].ToString();//סԺ��ˮ��
                    obj.Name = Reader[3].ToString();//����
                    obj.Memo = Reader[4].ToString();//��ʿվ����

                    al.Add(obj);
                }
            }
            catch (Exception e)
            {
                this.Err = "ִ��Sql������Fee.ReturnApply.QueryPatients" + e.Message;
                return null;
            }
            return al;
        }



        /// <summary>
        /// ɾ��һ���˷������¼
        /// </summary>
        /// <param name="applySquence">�˷�������ˮ��</param>
        /// <returns>�ɹ�1 ʧ�ܣ�1 �޼�¼ 0</returns>
        [Obsolete("����,ʹ��DeleteReturnApply()", true)]
        public int DeleteApplyReturn(string applySquence)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Fee.ReturnApply.DeleteApplyReturn", ref strSql) == -1)
            {
                this.Err = "û���ҵ�Fee.ReturnApply.DeleteApplyReturn�ֶ�!";
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, ID);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "�����������ȷ��Fee.ReturnApply.DeleteApplyReturn" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ����һ���˷������¼
        /// </summary>
        /// <param name="info">��ҩ̨ʵ��</param>
        /// <returns>1�ɹ���-1ʧ��</returns>
        [Obsolete("����,ʹ��InsertReturnApply()", true)]
        public int InsertApplyReturn(FS.HISFC.Models.Fee.ReturnApply info)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Fee.ReturnApply.InsertApplyReturn", ref strSql) == -1)
            {
                this.Err = "û���ҵ�Fee.ReturnApply.InsertApplyReturn�ֶ�!";
                return -1;
            }
            try
            {
                string[] strParm = GetReturnApplyParams(info);  //ȡ�����б�
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = "�����������ȷ��Fee.ReturnApply.InsertApplyReturn" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ȡ�˷�������ˮ��
        /// </summary>
        /// <returns>"-1"����oterhs �ɹ�</returns>
        [Obsolete("����,ʹ��GetReturnApplySequence()", true)]
        public string GetApplyReturnID()
        {
            return this.GetSequence("Fee.ReturnApply.GetApplyReturnID");
        }

        /// <summary>
        /// ���ݻ���סԺ��ˮ�ţ�ȡ�˻��ߵ��˷���������
        /// </summary>
        /// <param name="inPatientNo">����סԺ��ˮ��</param>
        /// <returns>�˷���������</returns>
        [Obsolete("����,ʹ��QueryReturnApplys()", true)]
        public ArrayList GetList(string inPatientNo)
        {
            ArrayList al = new ArrayList();
            string strSQL = "";  //SELECT���

            //ȡSQL���
            if (this.Sql.GetCommonSql("Fee.ReturnApply.GetApplyReturnList", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Fee.ReturnApply.GetApplyReturnList�ֶ�!";
                return null;
            }

            try
            {
                strSQL = string.Format(strSQL, inPatientNo);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "�����������ȷ��Fee.ReturnApply.GetApplyReturnList" + ex.Message;
                return null;
            }
            //ȡ��ҩ̨�����б�			
            return this.QueryReturnApplysBySql(strSQL);
        }

        /// <summary>
        /// ���ݻ���סԺ��ˮ�ţ�ȡ�˻��ߵ��˷���������
        /// </summary>
        /// <param name="inPatientNo">����סԺ��ˮ��</param>
        /// <param name="isCharged">�Ƿ�ȷ�ϵ�����</param>
        /// <returns>�˷���������</returns>
        [Obsolete("����,ʹ��QueryReturnApplys()", true)]
        public ArrayList GetList(string inPatientNo, bool isCharged)
        {
            ArrayList al = new ArrayList();
            string strSQL = "";  //SELECT���
            string strWhere = ""; //Where����
            string chargeFlag = "0"; //�˷�״̬��0δ�˷ѣ����룩��1���˷ѣ���׼��

            //ȡSQL���
            if (this.Sql.GetCommonSql("Fee.ReturnApply.GetApplyReturnList", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Fee.ReturnApply.GetApplyReturnList�ֶ�!";
                return null;
            }


            //ȡWhere���
            if (this.Sql.GetCommonSql("Fee.ReturnApply.GetApplyReturnList.Where", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�Fee.ReturnApply.GetApplyReturnList.Where�ֶ�!";
                return null;
            }

            //��������״̬����
            if (isCharged)
                chargeFlag = "1";
            else
                chargeFlag = "0";

            try
            {
                strSQL = string.Format(strSQL + " " + strWhere, inPatientNo, chargeFlag);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "�����������ȷ��Fee.ReturnApply.GetApplyReturnList.Where" + ex.Message;
                return null;
            }
            //ȡ��ҩ̨�����б�			
            return this.QueryReturnApplysBySql(strSQL);
        }

        /// <summary>
        /// ���ݻ���סԺ��ˮ�ţ�ȡ�˻��ߵ��˷���������
        /// </summary>
        /// <param name="inPatientNo">����סԺ��ˮ��</param>
        /// <param name="operDept">�˷Ѳ���Ա���ڿ���</param>
        /// <param name="isCharged">�Ƿ�ȷ������</param>
        /// <param name="isUndrug">�Ƿ��ҩƷ</param>
        /// <returns>�ɹ������˷��������� ʧ�ܷ���null</returns>
        [Obsolete("����,ʹ��QueryReturnApplys()", true)]
        public ArrayList GetList(string inPatientNo, string operDept,bool isCharged,bool isUndrug)
        {
            ArrayList al = new ArrayList();
            string strSQL = "";  //SELECT���
            string strWhere = ""; //Where����
            string chargeFlag = "0"; //�˷�״̬��0δ�˷ѣ����룩��1���˷ѣ���׼��
            string drugFlag = "2"; //ҩƷ��ǣ�1 ҩƷ	2 ��ҩƷ

            //ȡSQL���
            if (this.Sql.GetCommonSql("Fee.ReturnApply.GetApplyReturnList", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Fee.ReturnApply.GetApplyReturnList�ֶ�!";
                return null;
            }


            //ȡWhere���
            if (this.Sql.GetCommonSql("Fee.ReturnApply.GetApplyReturnList.Where.isUndrug", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�Fee.ReturnApply.GetApplyReturnList.Where.isUndrug�ֶ�!";
                return null;
            }

            //��������״̬����
            if (isCharged)
                chargeFlag = "1";
            else
                chargeFlag = "0";

            if (isUndrug)
                drugFlag = "0";     //��ҩƷ
            else
                drugFlag = "1";		//ҩƷ

            try
            {
                strSQL = string.Format(strSQL + " " + strWhere, inPatientNo, operDept, chargeFlag, drugFlag);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "�����������ȷ��Fee.ReturnApply.GetApplyReturnList.Where" + ex.Message;
                return null;
            }
            //ȡ��ҩ̨�����б�			
            return this.QueryReturnApplysBySql(strSQL);
        }

        /// <summary>
        /// ���ݻ���סԺ��ˮ�š��Ƿ�ȷ�� ���Ƿ�ҩȡ����ҩƷ�˷���������
        /// Add By liangjz 2005-10
        /// </summary>
        /// <param name="inPatientNo">סԺ��ˮ��</param>
        /// <param name="isCharged">�Ƿ��˷�ȷ��</param>
        /// <param name="isConfirm">�Ƿ�ҩ</param>
        /// <returns>�ɹ������˷��������顢ʧ�ܷ���null</returns>
        [Obsolete("����,ʹ��QueryDrugReturnApplys()", true)]
        public ArrayList GetDrugList(string inPatientNo, bool isCharged,bool isConfirm)
        {
            ArrayList al = new ArrayList();
            string strSQL = "";  //SELECT���
            string strWhere = ""; //Where����
            string chargeFlag = "0"; //�˷�״̬��0δ�˷ѣ����룩��1���˷ѣ���׼��
            string confirmFlag = "0"; //��ҩ��� 0 δ��ҩ 1 �ѷ�ҩ

            //ȡSQL���
            if (this.Sql.GetCommonSql("Fee.ApplyReturn.GetApplyReturnList", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Fee.ReturnApply.GetApplyReturnList�ֶ�!";
                return null;
            }
            //ȡWhere���
            if (this.Sql.GetCommonSql("Fee.ApplyReturn.GetApplyReturnList.Where.DrugList", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�Fee.ReturnApply.GetApplyReturnList.Where.DrugList�ֶ�!";
                return null;
            }

            //��������״̬����
            if (isCharged)
                chargeFlag = "1";
            else
                chargeFlag = "0";

            if (isConfirm)
                confirmFlag = "1";       //�ѷ�ҩ
            else
                confirmFlag = "0";		 //δ��ҩ

            try
            {
                strSQL = string.Format(strSQL + " " + strWhere, inPatientNo, chargeFlag, confirmFlag);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "�����������ȷ��" + ex.Message;
                return null;
            }
            //ȡ��ҩ̨�����б�			
            return this.QueryReturnApplysBySql(strSQL);
        }

        /// <summary>
        /// ȡ���˷����� ��״̬Ϊ��Ч 2
        /// </summary>
        /// <param name="ID">������ˮ��</param>
        /// <param name="operCode">ȡ����</param>
        /// <returns>�ɹ�1 ʧ�ܣ�1 �޼�¼ 0</returns>
        [Obsolete("����,ʹ��CancelReturnApply()", true)]
        public int CancelApplyReturn(string ID, string operCode)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Fee.ReturnApply.CancelApplyReturn", ref strSql) == -1)
            {
                this.Err = "û���ҵ�Fee.ReturnApply.CancelApplyReturn�ֶ�!";
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, ID, operCode);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "�����������ȷ��Fee.ReturnApply.CancelApplyReturn" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        #endregion

        //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        #region �˻�����
        /// <summary>
        /// ���ݲ�����ʱ��β�ѯ������Ϣ
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isDrug"></param>
        /// <returns></returns>
        public ArrayList GetApplyReturn(string cardNO, bool isConfirm, bool isCharge, bool isDrug)
        {
            string sql = string.Empty;

            //ȡSQL���
            if (this.Sql.GetCommonSql("Fee.ApplyReturn.GetApplyReturnListByCardNO", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.ApplyReturn.GetApplyReturnListByCardNO��SQL���!";

                return null;
            }
            return QueryReturnApplysBySql(sql, cardNO,
                                         NConvert.ToInt32(isConfirm).ToString(),
                                         NConvert.ToInt32(isCharge).ToString(),
                                         NConvert.ToInt32(isDrug).ToString());

        }

        /// <summary>
        /// �����˷�������˷ѱ�ʶ
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <param name="sequenceNO"></param>
        /// <returns></returns>
        public int UpdateApplyCharge(FS.HISFC.Models.Fee.ReturnApply returnApply)
        {
            return this.UpdateSingleTable("Fee.ApplyReturn.ChargeApplyReturn",
                                   returnApply.ID,
                                   returnApply.CancelOper.ID,
                                   returnApply.CancelOper.OperTime.ToString());
        }
        #endregion
    }
}