using System;
using FS.HISFC.Models;
using System.Collections;
using FS.FrameWork.Models;
using System.Data;
using System.Collections.Generic;
namespace FS.HISFC.BizLogic.Order
{
    /// <summary>
    /// ҽ�������ࡣ
    /// 
    /// <˵��>
    ///     1 2007-04 ������Һ���Ĵ���
    ///         ִ�е����뺯�����Ӳ����Ƿ�����Һ
    ///     2 ���Ӳ�ѯ�����º���
    /// </˵��>
    /// </summary>
    public class Order : FS.FrameWork.Management.Database
    {
        public Order()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ��̬��

        /// <summary>
        /// ����Һ�÷�
        /// </summary>
        private static System.Collections.Hashtable hsCompoundUsage = null;

        /// <summary>
        /// �Ƿ�ʹ��ҩƷִ�С��۷ѷֿ����� 0 ͬʱ���� 1 ��ͬʱ����
        /// </summary>
        bool bCharge;

        /// <summary>
        /// �Ƿ��Ѿ����²�ѯ������bCharge
        /// </summary>
        bool isGet_bCharge = false;

        /// <summary>
        /// ҩƷ��Ʒ�ʱ��ҩƷ�����Ƿ���ҩƷͬʱ�Ʒ� 1 ��ʿվ�Ʒ� 0 ҩ���Ʒ�
        /// </summary>
        bool bChargeSubtbl;

        /// <summary>
        /// �Ƿ��Ѿ����²�ѯ������bChargeSubtbl
        /// </summary>
        bool isGet_bChargeSubtbl = false;

        /// <summary>
        /// �ֽ��ֹʱ�䣨Сʱ��
        /// </summary>
        protected int iHour = 12;

        /// <summary>
        /// �ֽ��ֹʱ�䣨���ӣ�
        /// </summary>
        protected int iMinute = 1;

        /// <summary>
        /// ��ǰ����վ����
        /// </summary>
        protected string strNurseStationCode = "";

        protected FS.FrameWork.Management.ControlParam controler = new FS.FrameWork.Management.ControlParam();

        /// <summary>
        /// ϵͳ��ǰʱ��
        /// </summary>
        DateTime dtCurTime = new DateTime();

        /// <summary>
        /// Сʱ�Ʒ�ҽ����Ƶ�δ��� {97FA5C9D-F454-4aba-9C36-8AF81B7C9CCF} СʱƵ��
        /// </summary>
        private string hourFerquenceID = "";

        #endregion

        #region ����
        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [Obsolete("��InsertOrder���棡", true)]
        public int CreateOrder(FS.HISFC.Models.Order.Inpatient.Order order)
        {
            return this.InsertOrder(order);
        }


        [Obsolete("QueryOrderSubtbl������", true)]
        public ArrayList QueryOrderSub(string InPatientNo)
        {
            return this.QueryOrderSubtbl(InPatientNo);
        }
        [Obsolete("��InsertExecOrder������", true)]
        public int CreateExec(FS.HISFC.Models.Order.ExecOrder ExecOrder)
        {
            return this.InsertExecOrder(ExecOrder);
        }

        [Obsolete("UpdateRecordExec����", true)]
        public int RecordExec(FS.HISFC.Models.Order.ExecOrder ExecOrder)
        {
            return this.UpdateRecordExec(ExecOrder);
        }

        [Obsolete("��UpdateChargeExec����", true)]
        public int ChargeExec(FS.HISFC.Models.Order.ExecOrder ExecOrder)
        {
            return UpdateChargeExec(ExecOrder);
        }
        [Obsolete("UpdateDrugExec����", true)]
        public int DrugExec(FS.HISFC.Models.Order.ExecOrder ExecOrder)
        {
            return this.UpdateDrugExec(ExecOrder);
        }

        [Obsolete("QueryExecOrder(inpatientNo,itemType);����", true)]
        public ArrayList QueryPatientExec(string inpatientNo, string itemType)
        {
            return this.QueryExecOrder(inpatientNo, itemType);
        }
        [Obsolete("��QueryExecOrder(InPatientNo,ItemType,IsValid)����", true)]
        public ArrayList QueryValidOrder(string InPatientNo, string ItemType, bool IsValid)
        {
            return this.QueryExecOrder(InPatientNo, ItemType, IsValid);
        }
        [Obsolete("QueryExecOrder(inpatientNo,itemType,isCharge)����", true)]
        public ArrayList QueryChargeOrder(string inpatientNo, string itemType, bool isCharge)
        {
            return QueryExecOrder(inpatientNo, itemType, isCharge);
        }
        [Obsolete("QueryExecOrderByDrugFlag(InPatientNo,DateTimeBegin,DateTimeEnd,  DrugFlag)����", true)]
        public ArrayList QueryOrderDrugFlag(string InPatientNo, DateTime DateTimeBegin, DateTime DateTimeEnd, int DrugFlag)
        {
            return this.QueryExecOrderByDrugFlag(InPatientNo, DateTimeBegin, DateTimeEnd, DrugFlag);
        }
        [Obsolete("QueryExecOrderByDrugFlag(InPatientNo,DrugFlag)����", true)]
        public ArrayList QueryOrderDrugFlag(string InPatientNo, int DrugFlag)
        {
            return this.QueryExecOrderByDrugFlag(InPatientNo, DrugFlag);
        }
        #endregion

        #region ��ɾ��

        /// <summary>
        /// ������ҽ��(������ҽ����¼)
        /// </summary>
        /// <param name="order"></param>
        /// <returns>0 success -1 fail</returns>
        public int InsertOrder(FS.HISFC.Models.Order.Inpatient.Order order)
        {
            #region ������ҽ��
            //������ҽ��
            //Order.Order.CreateOrder.1
            //���룺71
            //			//������0 
            #endregion

            string strSql = "";

            if (this.Sql.GetCommonSql("Order.Order.CreateOrder.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            strSql = GetOrderInfo(strSql, order);
            if (strSql == null) return -1;

            return this.ExecNoQuery(strSql);
        }


        /// <summary>
        /// ����ҽ��
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int UpdateOrder(FS.HISFC.Models.Order.Inpatient.Order order)
        {
            #region ����ҽ��
            //����ҽ��
            //Order.Order.CreateOrder.1
            //���룺71
            //������0 
            #endregion
            string strSql = "";

            if (this.Sql.GetCommonSql("Order.Order.updateOrder.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            strSql = GetOrderInfo(strSql, order);
            if (strSql == null) return -1;
            return this.ExecNoQuery(strSql);
        }


        /// <summary>
        /// ɾ��ҽ��
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int DeleteOrder(FS.HISFC.Models.Order.Order order)
        {

            #region ɾ��ҽ��
            //ɾ��ҽ��(ҽ��δ��Ч״̬)
            //Order.Order.delOrder.1
            //���룺0 id
            //������0 
            #endregion
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.Order.delOrder.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, order.ID);
            }
            catch
            {
                this.Err = "����������ԣ�Order.Order.delOrder.1";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }


        /// <summary>
        /// ����ִ�е�(����ִ�е���¼)
        /// </summary>
        /// <param name="execOrder"></param>
        /// <returns>0 success -1 fail</returns>
        public int InsertExecOrder(FS.HISFC.Models.Order.ExecOrder execOrder)
        {
            string strSql = "";
            string strItemType = "";

            strItemType = JudgeItemType(execOrder.Order);
            if (strItemType == "")
            {
                return -1;
            }

            #region "ҩ��ִ�е�"
            if (strItemType == "1")
            {
                FS.HISFC.Models.Pharmacy.Item objPharmacy;
                objPharmacy = (FS.HISFC.Models.Pharmacy.Item)execOrder.Order.Item;

                if (this.Sql.GetCommonSql("Order.ExecOrder.CreateExec.Drug.1", ref strSql) == -1) 
                    return -1;

                #region ��Һ�ж�  ��� ��������Manager

                if (execOrder.Order.OrderType.IsDecompose)      //����Ҫ�ֽ��ҽ���������´���
                {
                    if (Order.hsCompoundUsage == null)
                    {
                        Order.hsCompoundUsage = new Hashtable();

                        FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
                        consManager.SetTrans(this.Trans);

                        ArrayList alList = consManager.GetList("CompoundUsage");
                        if (alList == null)  //�����д��󷵻�
                        {
                            Order.hsCompoundUsage = new Hashtable();
                        }
                        foreach (FS.HISFC.Models.Base.Const cons in alList)
                        {
                            Order.hsCompoundUsage.Add(cons.ID, null);
                        }
                    }

                    if (Order.hsCompoundUsage.ContainsKey(execOrder.Order.Usage.ID))
                    {
                        execOrder.Order.Compound.IsNeedCompound = true;
                    }
                }

                #endregion

                try
                {
                    string[] s ={
                                    execOrder.ID,
                                    execOrder.Order.Patient.ID,
                                    execOrder.Order.Patient.PID.PatientNO,
                                    execOrder.Order.Patient.PVisit.PatientLocation.Dept.ID,
                                    execOrder.Order.Patient.PVisit.PatientLocation.NurseCell.ID,
                                    strItemType,
                                    execOrder.Order.Item.ID,
                                    execOrder.Order.Item.Name,
                                    execOrder.Order.Item.UserCode,
                                    execOrder.Order.Item.SpellCode,
                                    execOrder.Order.Item.SysClass.ID.ToString(),
                                    execOrder.Order.Item.SysClass.Name,
                                    objPharmacy.Specs,
                                    objPharmacy.BaseDose.ToString(),
                                    objPharmacy.DoseUnit,
                                    objPharmacy.MinUnit,
                                    objPharmacy.PackQty.ToString(),
                                    objPharmacy.DosageForm.ID,
                                    objPharmacy.Type.ID,
                                    objPharmacy.Quality.ID.ToString(),
                                    objPharmacy.Price.ToString(),
                                    execOrder.Order.Usage.ID,
                                    execOrder.Order.Usage.Name,
                                    execOrder.Order.Usage.Memo,
                                    execOrder.Order.Frequency.ID,
                                    execOrder.Order.Frequency.Name,
                                    execOrder.Order.DoseOnce.ToString(),
                                    execOrder.Order.Qty.ToString(),
                                    execOrder.Order.Unit,
                                    execOrder.Order.HerbalQty.ToString(),
                                    execOrder.Order.OrderType.ID,
                                    execOrder.Order.ID,
                                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.Order.OrderType.IsDecompose).ToString(),
                                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.Order.OrderType.IsCharge).ToString(),
                                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.Order.OrderType.IsNeedPharmacy).ToString(),
                                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.Order.OrderType.IsPrint).ToString(),
                                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.Order.Item.IsNeedConfirm).ToString(),
                                    execOrder.Order.ReciptDoctor.ID,
                                    execOrder.Order.ReciptDoctor.Name,
                                    execOrder.DateUse.ToString(),
                                    execOrder.DCExecOper.OperTime.ToString(),
                                    execOrder.Order.ReciptDept.ID,
                                    execOrder.Order.BeginTime.ToString(),
                                    execOrder.DCExecOper.ID,
                                    execOrder.ChargeOper.ID,
                                    execOrder.ChargeOper.Dept.ID,
                                    execOrder.ChargeOper.OperTime.ToString(),
                                    execOrder.Order.StockDept.ID,
                                    execOrder.Order.ExeDept.ID,
                                    execOrder.Order.ExecOper.ID,
                                    execOrder.ExecOper.Dept.ID,
                                    execOrder.ExecOper.OperTime.ToString(),
                                    execOrder.DateDeco.ToString(),
                                    execOrder.Order.BabyNO.ToString(),
                                    execOrder.Order.Combo.ID,
                                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.Order.Combo.IsMainDrug).ToString(),
                                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.Order.IsHaveSubtbl).ToString(),
                                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.IsValid).ToString(),
                                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.Order.IsStock).ToString(),
                                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.IsExec).ToString(),
                                    execOrder.DrugFlag.ToString(),
                                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.IsCharge).ToString(),
                                    execOrder.Order.Note,
                                    execOrder.Order.Memo,
                                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.Order.IsBaby).ToString(),
                                    execOrder.Order.ReciptNO,
                                    execOrder.Order.SequenceNO.ToString(),
                                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.Order.Compound.IsNeedCompound).ToString()
                                };

                    strSql = string.Format(strSql, s);
                }
                catch (Exception ex)
                {
                    this.Err = "����ֵʱ�����" + ex.Message;
                    this.WriteErr();
                    return -1;
                }
            }

            #endregion

            #region "��ҩ��ִ�е�"

            else if (strItemType == "2")
            {
                FS.HISFC.Models.Fee.Item.Undrug undrug = (FS.HISFC.Models.Fee.Item.Undrug)execOrder.Order.Item;

                if (this.Sql.GetCommonSql("Order.ExecOrder.CreateExec.Undrug.1", ref strSql) == -1)
                {
                    return -1;
                }

                try
                {
                    string[] s ={
                                    execOrder.ID,
                                    execOrder.Order.Patient.ID,
                                    execOrder.Order.Patient.PID.PatientNO,
                                    execOrder.Order.Patient.PVisit.PatientLocation.Dept.ID,
                                    execOrder.Order.Patient.PVisit.PatientLocation.NurseCell.ID,
                                    strItemType,
                                    execOrder.Order.Item.ID,
                                    execOrder.Order.Item.Name,
                                    execOrder.Order.Item.UserCode,
                                    execOrder.Order.Item.SpellCode,
                                    execOrder.Order.Item.SysClass.ID.ToString(),
                                    execOrder.Order.Item.SysClass.Name,
                                    undrug.Specs,
                                    undrug.Price.ToString(),
                                    execOrder.Order.Usage.ID,
                                    execOrder.Order.Usage.Name,
                                    execOrder.Order.Usage.Memo,
                                    execOrder.Order.Frequency.ID,
                                    execOrder.Order.Frequency.Name,
                                    execOrder.Order.DoseOnce.ToString(),
                                    execOrder.Order.Qty.ToString(),
                                    execOrder.Order.Unit,
                                    execOrder.Order.HerbalQty.ToString(),
                                    execOrder.Order.OrderType.ID,
                                    execOrder.Order.ID,
                                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.Order.OrderType.IsDecompose).ToString(),
                                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.Order.OrderType.IsCharge).ToString(),
                                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.Order.OrderType.IsNeedPharmacy).ToString(),
                                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.Order.OrderType.IsPrint).ToString(),
                                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.Order.Item.IsNeedConfirm).ToString(),
                                    execOrder.Order.ReciptDoctor.ID,
                                    execOrder.Order.ReciptDoctor.Name,
                                    execOrder.DateUse.ToString(),
                                    execOrder.DCExecOper.OperTime.ToString(),
                                    execOrder.Order.ReciptDept.ID,
                                    execOrder.Order.BeginTime.ToString(),
                                    execOrder.DCExecOper.ID,
                                    execOrder.ChargeOper.ID,
                                    execOrder.ChargeOper.Dept.ID,
                                    execOrder.ChargeOper.OperTime.ToString(),
                                    execOrder.Order.StockDept.ID,
                                    execOrder.Order.ExeDept.ID,
                                    execOrder.ExecOper.ID,
                                    execOrder.ExecOper.Dept.ID,
                                    execOrder.ExecOper.OperTime.ToString(),
                                    execOrder.DateDeco.ToString(),
                                    execOrder.Order.ExeDept.Name,
                                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.Order.IsBaby).ToString(),
                                    execOrder.Order.BabyNO.ToString(),
                                    execOrder.Order.Combo.ID,
                                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.Order.Combo.IsMainDrug).ToString(),
                                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.Order.IsSubtbl).ToString(),
                                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.Order.IsHaveSubtbl).ToString(),
                                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.IsValid).ToString(),
                                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.IsExec).ToString(),
                                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.IsCharge).ToString(),
                                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.Order.IsEmergency).ToString(),
                                    execOrder.Order.CheckPartRecord,
                                    execOrder.Order.Note,
                                    execOrder.Order.Memo,
                                    execOrder.Order.ReciptNO,
                                    execOrder.Order.SequenceNO.ToString(),
                                    execOrder.Order.Sample.Name,
                                    execOrder.IsConfirm?"1":"0"/*ȷ�ϱ��{DA77B01B-63DF-4559-B264-798E54F24ABB}*/,
                                    execOrder.Order.ApplyNo
                                };

                    strSql = string.Format(strSql, s);
                }
                catch (Exception ex)
                {
                    this.Err = "����ֵʱ�����" + ex.Message;
                    this.WriteErr();
                    return -1;
                }
            }
            #endregion

            if (strSql == null)
            {
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        #endregion

        #region ST��ɾ��
        /// <summary>
        /// ���STҽ����Ϣ
        /// </summary>
        /// <param name="sqlOrder"></param>
        /// <param name="Order"></param>
        /// <returns></returns>
        private string GetOrderSTInfo(string sqlOrder, FS.HISFC.Models.Order.OrderST Order)
        {
            #region "�ӿ�˵��"

            #endregion

            try
            {
                System.Object[] st = { 
                                              Order.Clinic_no,
                                              Order.Is_prine?"1":"0",
                                              Order.Name,
                                              Order.Inouttype ,
                                              Order.Card_no    ,
                                              Order.Recipe_no  ,
                                              Order.Item_code  ,
                                              Order.Item_name  ,
                                              Order.Usage_code  ,
                                              Order.Usage_name   ,
                                              Order.Once_dose.ToString()    ,
                                              Order.Dose_unit    ,
                                              Order.Fre_code     ,
                                              Order.Fre_name    ,
                                              Order.Days        ,
                                              Order.Recipe_doc_code ,
                                              Order.Recipe_doc_name ,
                                              Order.Recipe_dept_code ,
                                              Order.Recipe_dept_name ,
                                              Order.Discarded_dose.ToString()  ,
                                              Order.Audit_doc_code   ,
                                              Order.Audit_doc_name  ,
                                              Order.Exec_doc_code   ,
                                              Order.Exec_doc_name   ,
                                              Order.Exec_date       ,
                                              Order.Comb_no         ,
                                              Order.Memo            ,
                                              Order.Ext_memo        ,
                                              Order.Ext_memo1       ,
                                              Order.Ext_memo2       ,
                                              Order.See_no           ,
                                              Order.Hospital_id,
                                              Order.Hospital_name
                                        };
                sqlOrder = string.Format(sqlOrder, st);
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����" + ex.Message;
                this.WriteErr();
                return null;
            }
            
            return sqlOrder;
        }

        /// <summary>
        /// ������ҽ��ST
        /// </summary>
        /// <param name="order"></param>
        /// <returns>0 success -1 fail</returns>
        public int InsertOrderST(FS.HISFC.Models.Order.OrderST order)
        {
            #region ������ҽ��
            //������ҽ��
            //Order.Order.CreateOrderST.1
            //���룺71
            //			//������0 
            #endregion

            string strSql = "";

            if (this.Sql.GetCommonSql("Order.Order.CreateOrderST.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            strSql = GetOrderSTInfo(strSql, order);
            if (strSql == null) return -1;

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ����ҽ��ST
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int UpdateOrderST(FS.HISFC.Models.Order.OrderST order)
        {
            #region ����ҽ��
            #endregion
            string strSql = "";

            if (this.Sql.GetCommonSql("Order.Order.updateOrderST.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            strSql = GetOrderSTInfo(strSql, order);
            if (strSql == null) return -1;
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ɾ��ҽ��ST
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int DeleteOrderST(FS.HISFC.Models.Order.OrderST order)
        {

            #region ɾ��ҽ��
            //ɾ��ҽ��(ҽ��δ��Ч״̬)
            //Order.Order.delOrder.1
            //���룺0 id
            //������0 
            #endregion
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.Order.delOrderST.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, order.Recipe_no,order.See_no);
            }
            catch
            {
                this.Err = "����������ԣ�Order.Order.delOrderST.1";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// �����Զ���WHERE��ѯSTҽ��
        /// </summary>
        /// <param name="inPateintNo"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public ArrayList QueryOrderBySQL(string whereSQL)
        {
            string sql = "";
            ArrayList al = new ArrayList();
            sql = OrderSTQuerySelect();
            sql = sql + "\r\n " + whereSQL;
            return this.MyOrderSTQuery(sql);
        }

        /// ��ѯ������Ϣ��select��䣨��where������
        private string OrderSTQuerySelect()
        {
            #region �ӿ�˵��
            //Order.Order.QueryOrder.Select.1
            //���룺0
            //������sql.select
            #endregion
            string sql = "";
            if (this.Sql.GetCommonSql("Order.Order.QueryOrderST.Select.1", ref sql) == -1)
            {
                this.Err = "û���ҵ�Order.Order.QueryOrderST.Select.1�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            return sql;
        }

        /// <summary>
        /// ˽�к�������ѯҽ��ST��Ϣ
        /// </summary>
        /// <param name="SQLPatient"></param>
        /// <returns></returns>
        private ArrayList MyOrderSTQuery(string SQLPatient)
        {
            ArrayList al = new ArrayList();

            if (this.ExecQuery(SQLPatient) == -1)
            {
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Order.OrderST Order = new FS.HISFC.Models.Order.OrderST();
                    #region "ҽ��ST��Ϣ"
                    try
                    {
                        Order.Clinic_no = this.Reader[0].ToString();
                        Order.Is_prine = this.Reader[1].ToString() == "0" ? false : true;
                        Order.Name = this.Reader[2].ToString();
                        Order.Inouttype = this.Reader[3].ToString();
                        Order.Card_no = this.Reader[4].ToString();
                        Order.Recipe_no = this.Reader[5].ToString();
                        Order.Item_code = this.Reader[6].ToString();
                        Order.Item_name = this.Reader[7].ToString();
                        Order.Usage_code = this.Reader[8].ToString();
                        Order.Usage_name = this.Reader[9].ToString();
                        Order.Once_dose = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[10].ToString());
                        Order.Dose_unit = this.Reader[11].ToString();
                        Order.Fre_code = this.Reader[12].ToString();
                        Order.Fre_name = this.Reader[13].ToString();
                        Order.Days = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[14].ToString());
                        Order.Recipe_doc_code = this.Reader[15].ToString();
                        Order.Recipe_doc_name = this.Reader[16].ToString();
                        Order.Recipe_dept_code = this.Reader[17].ToString();
                        Order.Recipe_dept_name = this.Reader[18].ToString();
                        Order.Discarded_dose = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[19].ToString());
                        Order.Audit_doc_code = this.Reader[20].ToString();
                        Order.Audit_doc_name = this.Reader[21].ToString();
                        Order.Exec_doc_code = this.Reader[22].ToString();
                        Order.Exec_doc_name = this.Reader[23].ToString();
                        Order.Exec_date = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[24].ToString());
                        Order.Comb_no = this.Reader[25].ToString();
                        Order.Memo = this.Reader[26].ToString();
                        Order.Ext_memo = this.Reader[27].ToString();
                        Order.Ext_memo1 = this.Reader[28].ToString();
                        Order.Ext_memo2 = this.Reader[29].ToString();
                        Order.See_no = this.Reader[30].ToString();

                    }
                    catch (Exception ex)
                    {
                        this.Err = "���ҽ��ST��Ϣ����" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    #endregion
                    al.Add(Order);
                }
            }
            catch (Exception ex)
            {
                this.Err = "���ҽ��ST��Ϣ����" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            this.Reader.Close();
            return al;
        }
        #endregion

        #region "ҽ������"

        #region ҽ������
        /// <summary>
        /// ��������ֹͣ��ҽ���������ĸ���Ҳֹͣ
        /// </summary>
        /// <param name="orderIDs"></param>
        /// <returns></returns>
        public int StopOrder(List<string> orderIDs)
        {
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Order.Order.StopOrder", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
            }
            foreach (string orderID in orderIDs)
            {
                string sql = string.Format(strSql, orderID);
                if (this.ExecNoQuery(sql) < 0)
                    return -1;
            }
            return 1;
        }


        /// <summary>
        /// ����ҽ�� -����Order.ID =="" or =="-1" ���µ� ���룬�����ĸ���
        /// ����ҽ��
        /// </summary>
        /// <param name="Order"></param>
        /// <returns></returns>
        public int SetOrder(FS.HISFC.Models.Order.Inpatient.Order Order)
        {
            if (Order.ID == "" || Order.ID == "-1")
            {
                string s = this.GetNewOrderID();
                if (s == null || s == "-1") return -1;
                Order.ID = s;
                return this.InsertOrder(Order);
            }
            else
            {
                return this.UpdateOrder(Order);
            }
        }

        /// <summary>
        /// ֹͣҽ��
        /// Order.Status = 1Ԥֹͣ;Order.Status = 3ֱ��ֹͣ
        /// </summary>
        /// <param name="Order">ҽ����Ϣ</param>
        /// <returns>0 success -1 fail</returns>
        public int DcOneOrder(FS.HISFC.Models.Order.Order Order)
        {
            #region ֹͣҽ��
            //ֹͣҽ��(ҽ������Ч״̬)
            //Order.Order.dcOrder.1
            //���룺0 id��1 ֹͣ��id,2ֹͣ��������3ֹͣʱ��,4ҽ��״̬ ,5ֹͣԭ����룬6ֹͣԭ������ 
            //������0 
            #endregion
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.Order.dcOrder.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                if (Order.EndTime == DateTime.MinValue)//�ж�ֹͣʱ��
                    Order.EndTime = this.GetDateTimeFromSysDateTime();

                strSql = string.Format(strSql, Order.ID, Order.DCOper.ID, Order.DCOper.Name, Order.EndTime.ToString(), Order.Status.ToString(), Order.DcReason.ID, Order.DcReason.Name);
            }
            catch
            {
                this.Err = "����������ԣ�Order.Order.dcOrder.1";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ɾ��ҽ������
        /// </summary>
        /// <param name="ComboID"></param>
        /// <returns></returns>
        public int DeleteOrderSubtbl(string ComboID)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.Order.delOrder.2", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, ComboID);
            }
            catch
            {
                this.Err = "����������ԣ�Order.Order.delOrder.2";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// '���
        ///��ҽ����ϳ����ҽ��һ��ִ��
        ///������usage�÷���ͬ
        ///      frq  Ƶ����ͬ
        /// </summary>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int ComboOrder(ArrayList alOrder)
        {
            string strUsage = "", strFrq = "";
            string strSql = "";
            string strCombNo = "";
            #region ���
            //���
            //Order.Order.ComboOrder.1
            //���룺0 orderid 1��Ϻ� 2�Ƿ���ҩ
            //������0 
            #endregion

            if (alOrder == null)
            {
                return -1;
            }

            strCombNo = this.GetNewOrderComboID();
            if (strCombNo == "" || strCombNo == null)
            {
                this.Err = FS.FrameWork.Management.Language.Msg("ҽ����Ϻ�Ϊ�գ�������ϣ�");
                return -1;
            }
            for (int i = 0; i < alOrder.Count; i++)
            {
                FS.HISFC.Models.Order.Order objOrder = new FS.HISFC.Models.Order.Order();
                objOrder = (FS.HISFC.Models.Order.Order)alOrder[i];
                if (i == 0)
                {
                    strUsage = objOrder.Usage.ID;
                    strFrq = objOrder.Frequency.ID;
                }
                if (strUsage != objOrder.Usage.ID)
                {
                    this.Err = FS.FrameWork.Management.Language.Msg(objOrder.SubCombNO.ToString() + "��  " + objOrder.Item.Name + "[" + objOrder.Item.Specs + "]" + "�÷���һ��");
                    return -1;
                }
                if (strFrq != objOrder.Frequency.ID)
                {
                    this.Err = objOrder.Item.Name + FS.FrameWork.Management.Language.Msg(objOrder.SubCombNO.ToString() + "��  " + objOrder.Item.Name + "[" + objOrder.Item.Specs + "]" + "Ƶ�β�һ��");
                    return -1;
                }

                if (this.Sql.GetCommonSql("Order.Order.ComboOrder.1", ref strSql) == -1)
                {
                    this.Err = this.Sql.Err;
                    return -1;
                }
                try
                {
                    strSql = string.Format(strSql, objOrder.ID, strCombNo, FS.FrameWork.Function.NConvert.ToInt32(objOrder.Combo.IsMainDrug).ToString());
                }
                catch
                {
                    this.Err = "����������ԣ�Order.Order.ComboOrder.1";
                    return -1;
                }
                if (this.ExecNoQuery(strSql) <= 0)
                {
                    return -1;
                }
            }
            return 0;
        }

        #endregion

        #region "��ѯҽ��"
        /// <summary>
        /// ��ѯ����ҽ��
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <returns></returns>
        public ArrayList QueryOrder(string inpatientNO)
        {
            #region ��ѯ����ҽ��
            //��ѯ����ҽ��
            //Order.Order.QueryOrder.1
            //���룺0 inpatientno
            //������ArrayList
            #endregion

            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            sql = OrderQuerySelect();
            if (sql == null) return null;
            if (this.Sql.GetCommonSql("Order.Order.QueryOrder.1", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.Order.QueryOrder.1�ֶ�!";
                return null;
            }
            sql = sql + " " + string.Format(sql1, inpatientNO);
            return this.MyOrderQuery(sql);
        }

        /// <summary>
        /// ����ͬһ�����ҽ������
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="combno"></param>
        /// <returns></returns>
        public int QueryOrderCountByCombno(string inpatientNO, string combno)
        {
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            if (sql == null) return 0;
            if (this.Sql.GetCommonSql("Order.Order.QueryOrder.CombCount", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.Order.QueryOrder.CombCount�ֶ�!";
                return 0;
            }
            sql = string.Format(sql1, inpatientNO, combno);
            return FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(sql));
        }

        /// <summary>
        /// ��ѯ����ҽ���ĸ���
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryOrderSubtbl(string inpatientNO)
        {
            #region ��ѯ����ҽ���ĸ���
            //��ѯ����ҽ��
            //Order.Order.QueryOrder.1
            //���룺0 inpatientno
            //������ArrayList
            #endregion
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            sql = OrderQuerySelect();
            if (sql == null) return null;
            if (this.Sql.GetCommonSql("Order.Order.QueryOrder.Sub.1", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.Order.QueryOrder.1�ֶ�!";
                return null;
            }
            sql = sql + " " + string.Format(sql1, inpatientNO);
            return this.MyOrderQuery(sql); ;
        }

        /// <summary>
        /// ����ĳһ�����ߵ���Ч���� {24F859D1-3399-4950-A79D-BCCFBEEAB939}
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <returns></returns>
        public ArrayList QueryOrdeSub(string inpatientNO, string itemcode)
        {
            #region ��ѯ����ҽ��
            //��ѯ����ҽ��
            //Order.Order.QueryOrder.1
            //���룺0 inpatientno
            //������ArrayList
            #endregion

            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            sql = OrderQuerySelect();
            if (sql == null) return null;
            if (this.Sql.GetCommonSql("Order.Order.QueryOrder.6", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.Order.QueryOrder.1�ֶ�!";
                return null;
            }
            sql = sql + " " + string.Format(sql1, inpatientNO, itemcode);
            return this.MyOrderQuery(sql);
        }
        /// <summary>
        /// ��״̬��ѯҽ��
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ArrayList QueryOrder(string inpatientNO, int status)
        {
            return this.QueryOrderByState(inpatientNO, status.ToString());
            #region ��״̬��ѯҽ��
            //��״̬��ѯҽ��
            //Order.Order.QueryOrder.2
            //���룺0 inpatientno 2 status
            //������ArrayList
            #endregion
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            sql = OrderQuerySelect();
            if (sql == null) return null;
            if (this.Sql.GetCommonSql("Order.Order.QueryOrder.2", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.Order.QueryOrder.2�ֶ�!";
                return null;
            }
            sql = sql + " " + string.Format(sql1, inpatientNO, "'" + status.ToString() + "'");
            return this.MyOrderQuery(sql);
        }

        /// <summary>
        /// ����״̬��ѯҽ�������Զ��״̬
        /// </summary>
        /// <param name="inPateintNo"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public ArrayList QueryOrderByState(string inPateintNo, string state)
        {
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            sql = OrderQuerySelect();
            if (sql == null) return null;
            if (this.Sql.GetCommonSql("Order.Order.QueryOrder.2", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.Order.QueryOrder.2�ֶ�!";
                return null;
            }
            sql = sql + " " + string.Format(sql1, inPateintNo, state);
            return this.MyOrderQuery(sql);
        }

        /// <summary>
        /// ��ѯ��˵ķǸ���ҽ��
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="status"></param>
        /// <param name="isSubtbl"></param>
        /// <returns></returns>
        public ArrayList QueryOrder(string inpatientNO, int status, bool isSubtbl)
        {
            #region ��״̬��ѯҽ��
            //��״̬��ѯҽ��
            //Order.Order.QueryOrder.2
            //���룺0 inpatientno 2 status
            //������ArrayList
            #endregion

            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            sql = OrderQuerySelect();
            if (sql == null) return null;
            if (this.Sql.GetCommonSql("Order.Order.QueryOrder.ForConfirmQuery", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.Order.QueryOrder.ForConfirmQuery�ֶ�!";
                return null;
            }
            string flag = "";
            if (isSubtbl)
            {
                flag = "1";
            }
            else
            {
                flag = "0";
            }
            sql = sql + " " + string.Format(sql1, inpatientNO, status.ToString(), flag);
            return this.MyOrderQuery(sql);
        }
        /// <summary>
        /// ������ʱ���ѯҽ��
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public ArrayList QueryOrder(string inpatientNO, DateTime beginTime, DateTime endTime)
        {
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            #region ������ʱ���ѯҽ��
            //������ʱ���ѯҽ��
            //Order.Order.QueryOrder.3
            //���룺0 inpatientno 1BeginTime 2EndTime
            //������ArrayList
            #endregion

            sql = OrderQuerySelect();
            if (sql == null) return null;
            if (this.Sql.GetCommonSql("Order.Order.QueryOrder.3", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.Order.QueryOrder.3�ֶ�!";
                return null;
            }
            sql = sql + " " + string.Format(sql1, inpatientNO, beginTime, endTime);
            return this.MyOrderQuery(sql);
        }
        /// <summary>
        /// ������Ч��Ҫ��ӡ�Ļ���Ѳ�ؿ���Ϣ
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="usage"></param>
        /// <param name="isPrint"></param>
        /// <returns></returns>
        public ArrayList QueryCircuitCard(string inpatientNO, string usage, bool isPrint)
        {
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            #region ������Ч��Ҫ��ӡ�Ļ���Ѳ�ؿ���Ϣ
            //������Ч��Ҫ��ӡ�Ļ���Ѳ�ؿ���Ϣ
            //���룺0 inpatientno 1Usage, 2 Isprint
            //������ArrayList
            #endregion

            sql = OrderQuerySelect();
            if (sql == null) return null;
            if (this.Sql.GetCommonSql("Order.Order.QueryCircuitCard.1", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.Order.QueryCircuitCard.1�ֶ�!";
                return null;
            }
            sql = sql + " " + string.Format(sql1, inpatientNO, usage, FS.FrameWork.Function.NConvert.ToInt32(isPrint).ToString());
            return this.MyOrderQuery(sql);
        }
        /// <summary>
        /// ��ҽ�����Ͳ�ѯҽ��
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ArrayList QueryOrder(string inpatientNO, string type)
        {
            #region ��ҽ�����Ͳ�ѯҽ��
            //��ҽ�����Ͳ�ѯҽ��
            //Order.Order.QueryOrder.4
            //���룺0 inpatientno 1Type
            //������ArrayList
            #endregion
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            sql = OrderQuerySelect();
            if (sql == null) return null;
            if (this.Sql.GetCommonSql("Order.Order.QueryOrder.4", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.Order.QueryOrder.4�ֶ�!";
                return null;
            }
            sql = sql + " " + string.Format(sql1, inpatientNO, type);
            return this.MyOrderQuery(sql);
        }
        /// <summary>
        /// ��ѯ��Ժ��ҩ��
        /// </summary>
        /// <param name="inpatientNO">������ˮ��</param>
        /// <returns></returns>
        public System.Data.DataSet QueryOutHosDrug(string inpatientNO)
        {
            string sql = "Order.Order.Query.QueryOutHosDrug";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            sql = string.Format(sql, inpatientNO);
            System.Data.DataSet ds = new System.Data.DataSet();
            if (this.ExecQuery(sql, ref ds) == -1) return null;
            return ds;
        }
        /// <summary>
        /// ��ѯ��ٴ�ҩ��
        /// </summary>
        /// <param name="inpatientNO">������ˮ��</param>
        /// <returns></returns>
        public System.Data.DataSet QueryTempOutHosDrug(string inpatientNO)
        {
            string sql = "Order.Order.Query.QueryTempOutHosDrug";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            sql = string.Format(sql, inpatientNO);
            System.Data.DataSet ds = new System.Data.DataSet();
            if (this.ExecQuery(sql, ref ds) == -1) return null;
            return ds;
        }
        /// <summary>
        /// ��ѯ��Ժ������Ŀ
        /// </summary>
        /// <param name="inpatientNO">������ˮ��</param>
        /// <returns></returns>
        public ArrayList QueryOutHosCure(string inpatientNO)
        {
            string sql = "Order.Order.Query.QueryOutHosCure";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            sql = string.Format(sql, inpatientNO);
            ArrayList alCure = new ArrayList();
            try
            {
                alCure = this.myGetOutHosCure(sql);
            }
            catch
            {
                return null;
            }
            return alCure;
        }

        /// <summary>
        /// ��ҽ����ˮ�Ų�ѯҽ����Ϣ-������Ч����
        /// </summary>
        /// <param name="OrderNO"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Order.Inpatient.Order QueryOneOrder(string OrderNO)
        {
            string sql = "", sql1 = "";
            ArrayList al = null;
            #region ��ҽ����ˮ�Ų�ѯҽ����Ϣ
            //��ҽ����ˮ�Ų�ѯҽ����Ϣ
            //Order.Order.QueryOrder.5
            //���룺0 OrderNo
            //������ArrayList
            #endregion
            sql = OrderQuerySelect();
            if (sql == null) return null;
            if (this.Sql.GetCommonSql("Order.Order.QueryOrder.5", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.Order.QueryOrder.5�ֶ�!";
                return null;
            }
            sql = sql + " " + string.Format(sql1, OrderNO);
            al = this.MyOrderQuery(sql);
            if (al == null || al.Count == 0 || al.Count > 1) return null;
            return al[0] as FS.HISFC.Models.Order.Inpatient.Order;
        }

        /// <summary>
        /// ͨ��ҽ���Ų�ѯҽ��״̬
        /// </summary>
        /// <param name="OrderNO"></param>
        /// <returns></returns>
        public int QueryOneOrderState(string OrderNO)
        {
            string sql = string.Empty;
            if (this.Sql.GetCommonSql("Order.Order.QueryOneOrderState.1", ref sql) == -1)
            {
                this.Err = "û���ҵ�Order.Order.QueryOneOrderState.1�ֶ�!";
                return -1;
            }
            try
            {
                sql = string.Format(sql, OrderNO);
            }
            catch
            {
                this.Err = "����ֵ����";
                this.WriteErr();
                return -1;
            }

            return FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(sql));
        }

        /// <summary>
        /// ��ҽ�����ͣ�����/��ʱ����״̬����ѯҽ�������������ģ�
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ArrayList QueryOrder(string inpatientNO, FS.HISFC.Models.Order.EnumType type, int status)
        {
            #region ��ҽ�����ͣ�����/��ʱ����״̬����ѯҽ�������������ģ�
            //��ҽ�����ͣ�����/��ʱ����״̬����ѯҽ�������������ģ�
            //Order.Order.QueryOrder.where.6
            //���룺0 inpatientno 1 Type 2 status
            //������ArrayList
            #endregion

            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            sql = OrderQuerySelect();
            if (sql == null) return null;
            if (this.Sql.GetCommonSql("Order.Order.QueryOrder.where.6", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.Order.QueryOrder.where.6�ֶ�!";
                return null;
            }
            string strType = "1";
            if (type == FS.HISFC.Models.Order.EnumType.LONG)
            {
                strType = "1";
            }
            else
            {
                strType = "0";
            }
            sql = sql + " " + string.Format(sql1, inpatientNO, strType, status.ToString());
            return this.MyOrderQuery(sql);
        }

        /// <summary>
        /// ��ѯ�Ƿ���˵�ҽ�� - ����������
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="type"></param>
        /// <param name="isConfirmed"></param>
        /// <returns></returns>
        public ArrayList QueryIsConfirmOrder(string inpatientNO, FS.HISFC.Models.Order.EnumType type, bool isConfirmed)
        {
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            sql = OrderQuerySelect();
            if (sql == null) return null;
            if (this.Sql.GetCommonSql("Order.Order.QueryIsConfirmOrder.where.1", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.Order.QueryIsConfirmOrder.where.1�ֶ�!";
                return null;
            }
            string strType = "1";
            if (type == FS.HISFC.Models.Order.EnumType.LONG)
            {
                strType = "1";
            }
            else
            {
                strType = "0";
            }
            try
            {
                sql = sql + " " + string.Format(sql1, inpatientNO, FS.FrameWork.Function.NConvert.ToInt32(isConfirmed).ToString(), strType);
            }
            catch { return null; }
            return this.MyOrderQuery(sql);
        }

        /// <summary>
        /// ��ѯ����ҽ��
        /// </summary>
        /// <param name="InPatientNO"></param>
        /// <returns></returns>
        public ArrayList QueryDcOrder(string InPatientNO)
        {
            #region ��ѯ����ҽ��
            //��ѯ����ҽ��
            //Order.Order.QueryOrder.1
            //���룺0 inpatientno
            //������ArrayList
            #endregion
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            sql = OrderQuerySelect();
            if (sql == null) return null;
            if (this.Sql.GetCommonSql("Order.Order.QueryOrder.OrderPrint", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.Order.QueryOrder.1�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            sql = sql + " " + string.Format(sql1, InPatientNO);
            return this.MyOrderQuery(sql);
        }
        /// <summary>
        /// ��ҽ��״̬,ֹͣ��˱�־ ��ѯҽ�������������ģ�
        /// ֹͣδ���ҽ����ѯ
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="isConfirm"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ArrayList QueryDcOrder(string inpatientNO, int status, bool isConfirm)
        {
            #region  ��ҽ��״̬,��˱�־ ��ѯҽ�������������ģ�
            // ��ҽ��״̬,��˱�־ ��ѯҽ�������������ģ�
            //Order.Order.QueryDcOrder.where.1
            //���룺0 inpatientno 1 status 2 IsConfirm
            //������ArrayList
            #endregion
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            sql = OrderQuerySelect();
            if (sql == null) return null;
            if (this.Sql.GetCommonSql("Order.Order.QueryDcOrder.where.1", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.Order.QueryDcOrder.where.1�ֶ�!";
                return null;
            }
            sql = sql + " " + string.Format(sql1, inpatientNO, status.ToString(), FS.FrameWork.Function.NConvert.ToInt32(isConfirm));
            return this.MyOrderQuery(sql);
        }

        /// <summary>
        /// ���ҽ����Ŀ�ĸ�����Ϣ(��Ϻţ�
        /// </summary>
        /// <param name="combNo"></param>
        /// <returns></returns>
        public ArrayList QuerySubtbl(string combNo)
        {
            #region ���ҽ����Ŀ�ĸ�����Ϣ(��Ϻţ�
            //���ҽ����Ŀ�ĸ�����Ϣ(��Ϻţ�
            //Order.Order.QueryOrder.where.7
            //���룺0 inpatientno 1 CombNo 
            //������ArrayList
            #endregion
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            sql = OrderQuerySelect();
            if (sql == null) return null;
            if (this.Sql.GetCommonSql("Order.Order.QueryOrder.where.7", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.Order.QueryOrder.where.7�ֶ�!";
                return null;
            }
            sql = sql + " " + string.Format(sql1, combNo);
            return this.MyOrderQuery(sql);
        }
        /// <summary>
        /// �ӷ�ҩƷִ�е�����ĳ����ҩƷ�����Ƿ����ִ�м�¼
        /// {24F859D1-3399-4950-A79D-BCCFBEEAB939}
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="undrugID"></param>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public ArrayList QueryExecOrderSubtblCurrentDay(string inpatientNo, string undrugID, string deptID)
        {

            string[] s;
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();

            s = ExecOrderQuerySelect("2");

            for (int i = 0; i <= s.GetUpperBound(0); i++)
            {
                sql = s[i];
                if (sql == null) return null;
                if (this.Sql.GetCommonSql("Order.ExecOrder.QueryExecOrderSubtblCurrentDay", ref sql1) == -1)
                {
                    this.Err = "û���ҵ�Order.ExecOrder.QueryExecOrderBySubtblFeeMode.1�ֶ�!";
                    return null;
                }
                sql = sql + " " + string.Format(sql1, inpatientNo, undrugID, deptID);
                addExecOrder(al, sql);
            }
            return al;
        }
        /// <summary>
        /// ���ҽ��Ƥ����Ϣ
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns>-1���� 1����ҪƤ��/2��ҪƤ�ԣ�δ��/3Ƥ����/4Ƥ����</returns>
        public int QueryOrderHypotest(string orderNo)
        {
            #region ���ҽ��Ƥ����Ϣ
            //Order.Order.QueryOrderHypotest.1
            //���룺0 OrderNo 
            //������int 1����ҪƤ��/2��ҪƤ�ԣ�δ��/3Ƥ����/4Ƥ����
            #endregion
            string sql = "";
            int Hypotest = -1;

            if (this.Sql.GetCommonSql("Order.Order.QueryOrderHypotest.1", ref sql) == -1)
            {
                this.Err = "û���ҵ�Order.Order.QueryOrderHypotest.1�ֶ�!";
                return -1;
            }
            sql = string.Format(sql, orderNo);
            if (this.ExecQuery(sql) < 0) return -1;

            if (this.Reader.Read())
            {
                Hypotest = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0]);
            }
            else
            {
                Hypotest = 1;
            }

            this.Reader.Close();

            return Hypotest;
        }

        /// <summary>
        /// ���ҽ����ע��Ϣ
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public string QueryOrderNote(string orderNo)
        {
            #region ���ҽ��������ע��Ϣ
            //Order.Order.QueryOrderNote.1
            //���룺0 OrderNo 
            //������string
            #endregion
            string sql = "";
            string Note = "";

            if (this.Sql.GetCommonSql("Order.Order.QueryOrderNote.1", ref sql) == -1)
            {
                this.Err = "û���ҵ�Order.Order.QueryOrderNote.1�ֶ�!";
                return "";
            }
            sql = string.Format(sql, orderNo);
            if (this.ExecQuery(sql) < 0) return "";

            if (this.Reader.Read())
            {
                Note = this.Reader[0].ToString();
            }
            this.Reader.Close();

            return Note;
        }

        /// <summary>
        /// ��ҽ�����ͣ�����/��ʱ����״̬����ѯҽ�����������ģ�
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ArrayList QueryOrderWithSubtbl(string inpatientNO, FS.HISFC.Models.Order.EnumType type, int status)
        {
            #region ��ҽ�����ͣ�����/��ʱ����״̬����ѯҽ�����������ģ�
            //��ҽ�����ͣ�����/��ʱ����״̬����ѯҽ�����������ģ�
            //Order.Order.QueryOrder.where.6
            //���룺0 inpatientno 1 Type 2 status
            //������ArrayList
            #endregion
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            sql = OrderQuerySelect();
            if (sql == null) return null;
            if (this.Sql.GetCommonSql("Order.Order.QueryOrderWithSubtbl.where.1", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.Order.QueryOrderWithSubtbl.where.1�ֶ�!";
                return null;
            }
            string strType = "1";
            if (type == FS.HISFC.Models.Order.EnumType.LONG)
            {
                strType = "1";
            }
            else
            {
                strType = "0";
            }
            sql = sql + " " + string.Format(sql1, inpatientNO, strType, status.ToString());
            return this.MyOrderQuery(sql);
        }
        /// <summary>
        /// ��ѯ��Ч��ҽ��
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ArrayList QueryValidOrderWithSubtbl(string inpatientNO, FS.HISFC.Models.Order.EnumType type)
        {
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            sql = OrderQuerySelect();
            if (sql == null) return null;
            if (this.Sql.GetCommonSql("Order.Order.QueryOrderWithSubtbl.where.2", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.Order.QueryOrderWithSubtbl.where.2�ֶ�!";
                return null;
            }
            string strType = "1";
            if (type == FS.HISFC.Models.Order.EnumType.LONG)
            {
                strType = "1";
            }
            else
            {
                strType = "0";
            }
            sql = sql + " " + string.Format(sql1, inpatientNO, strType);
            return this.MyOrderQuery(sql);
        }
        #endregion

        #region ��ˮ��
        /// <summary>
        /// ���ҽ����ˮ��
        /// </summary>
        /// <returns></returns>
        public string GetNewOrderID()
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Management.Order.GetNewOrderID", ref sql) == -1) return null;
            string strReturn = this.ExecSqlReturnOne(sql);
            if (strReturn == "-1" || strReturn == "") return null;
            return strReturn;
        }
        /// <summary>
        /// ���ҽ��ִ����ˮ��
        /// </summary>
        /// <returns></returns>
        public string GetNewOrderExecID()
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Management.Order.GetNewOrderExecID", ref sql) == -1) return null;
            string strReturn = this.ExecSqlReturnOne(sql);
            if (strReturn == "-1" || strReturn == "") return null;
            return strReturn;
        }
        /// <summary>
        /// �����ҽ��������
        /// </summary>
        /// <returns></returns>
        public string GetNewOrderComboID()
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Management.Order.GetComboID", ref sql) == -1) return null;
            string strReturn = this.ExecSqlReturnOne(sql);
            if (strReturn == "-1" || strReturn == "") return null;
            return strReturn;
        }
        #endregion

        #region "ҽ�����"

        /// <summary>   
        /// ����ҽ��������Ϣ����ʿ��ע��Ƥ�Խ����
        /// ����Ƥ��ǰ���жϸñ�־�Ƿ���ҪƤ�ԣ�1����ҪƤ��/2��ҪƤ�ԣ�δ��/3Ƥ����/4Ƥ������
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="orderNo"></param>
        /// <param name="notes">��ע</param>
        /// <param name="hypotest">Ƥ�ԣ�1����ҪƤ��/2��ҪƤ�ԣ�δ��/3Ƥ����/4Ƥ������</param>
        /// <returns></returns>
        public int UpdateFeedback(string inpatientNo, string orderNo, string notes, int hypotest)
        {
            #region ����ҽ��������Ϣ
            //����ҽ��������Ϣ
            //Order.Order.Updatefeedback.1
            //���룺0 inpatientNo,1orderID,2 NOTES,3 hypotest
            //������0 
            #endregion
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.Order.Updatefeedback.1", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, inpatientNo, orderNo, notes, hypotest.ToString());
            }
            catch
            {
                this.Err = "����������ԣ�Order.Order.Updatefeedback.1";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ����ҽ��ִ�б��
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public int UpdateOrderExecuted(string orderNo)
        {
            #region ����ҽ��ִ�����
            //����ҽ��ִ�����
            //Order.Order.UpdateExecOrder.1
            //���룺0 orderID 1 ����Ա 2 ����ʱ��
            //������0 
            #endregion
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.Order.UpdateExecOrder.1", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, orderNo, this.Operator.ID, this.GetSysDateTime().ToString());
            }
            catch
            {
                this.Err = "����������ԣ�Order.Order.UpdateExecOrder.1";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ����ҽ���������
        /// ����Ϊ����
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public int UpdateChargeOrder(string inpatientNo, string orderNo)
        {
            #region ����ҽ���������
            //����ҽ���������
            //Order.Order.UpdateChargeOrder.1
            //���룺0 inpatientNo,1orderID 2 ����Ա 3 ����ʱ��
            //������0 
            #endregion
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.Order.UpdateChargeOrder.1", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, inpatientNo, orderNo, this.Operator.ID, this.GetSysDateTime().ToString());
            }
            catch
            {
                this.Err = "����������ԣ�Order.Order.UpdateChargeOrder.1";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ����ҽ����������
        /// </summary>
        /// <param name="orderNo">����ҽ������</param>
        /// <param name="num"></param>
        /// <returns></returns>
        public int UpdateSubtblNum(string orderNo, decimal num)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.UpdateSubNum.1", ref strSql) == -1)
            {
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, orderNo, num);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(strSql);

        }
        #endregion


        #endregion

        #region "ҽ��ִ�д���

        #region "����ִ�е�"

        /// <summary>
        /// ��ʿվ������ʱҽ����ע��Ϣ
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="txt"></param>
        /// <returns></returns>
        public int UpdateExecTime(string orderNo, string txt)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.ExecOrder.UpdateExecTime", ref strSql) == -1)
            {
                this.Err = "û���ҵ�Order.ExecOrder.UpdateExecTimeDrug�ֶ�";
                return -1;
            }
            strSql = string.Format(strSql, orderNo, txt);
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ����ִ�е���Ч���
        /// </summary>
        /// <param name="execOrderID"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public int UpdateExecValidFlag(string execOrderID, bool isPharmacy, string flag)
        {
            string strSql = "";
            string strIndex = "";

            if (isPharmacy)			//ҩƷִ�е�
                strIndex = "Order.Update.UpdateExecValidFlag.1";
            else					//��ҩƷִ�е�
                strIndex = "Order.Update.UpdateExecValidFlag.2";

            if (this.Sql.GetCommonSql(strIndex, ref strSql) == -1)
            {
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, execOrderID, flag);
            }
            catch (Exception ex)
            {
                this.Err = "�����������!" + strIndex + ex.Message;
                return -1;
            }
            return ExecNoQuery(strSql);
        }

        /// <summary>
        /// ����ִ�е� �ǳ������
        /// </summary>
        /// <param name="SqnNo"></param>
        /// <param name="isDrug"></param>
        /// <param name="dcPerson"></param>
        /// <returns></returns>
        public int DcExecImmediate(string SqnNo, bool isDrug, FS.FrameWork.Models.NeuObject dcPerson)
        {
            string strSql = "";
            if (isDrug)
            {
                if (this.Sql.GetCommonSql("Order.ExecOrder.DcExecImmediate.UnNormal.Drug", ref strSql) == -1)
                {
                    this.Err = "Can't Find Sql";
                    return -1;
                }
            }
            else
            {
                if (this.Sql.GetCommonSql("Order.ExecOrder.DcExecImmediate.UnNormal.UnDrug", ref strSql) == -1)
                {
                    this.Err = "Can't Find Sql";
                    return -1;
                }
            }
            try
            {
                strSql = System.String.Format(strSql, SqnNo, dcPerson.ID, "0");
            }
            catch
            {
                this.Err = "�����������";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ����ִ�е�
        /// </summary>
        /// <param name="dcPerson">ִ�е���Ϣ</param>
        /// <returns>0 success -1 fail</returns>
        public int DcExecImmediate(FS.HISFC.Models.Order.Order Order, FS.FrameWork.Models.NeuObject dcPerson)
        {
            #region ����ִ�е�
            //����ִ�е�(ҽ��ֹͣ��ֱ������)
            //Order.ExecOrder.DcExecImmediate
            //���룺0 id��1 ֹͣ��id,2ֹͣ��������3ֹͣʱ��,4���ϱ�־ 
            //������0 
            #endregion

            string strSql = "", strSqlName = "Order.ExecOrder.DcExecImmediate.";
            string strType = "";
            strType = this.JudgeItemType(Order);
            if (strType == "")
            {
                return -1;
            }
            strSqlName = strSqlName + strType;

            if (this.Sql.GetCommonSql(strSqlName, ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, Order.ID, dcPerson.ID, dcPerson.Name);
            }
            catch
            {
                this.Err = "�����������" + strSqlName;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ָֹͣ��ִ�е�
        /// </summary>
        /// <param name="execOrder"></param>
        /// <param name="dcPerson"></param>
        /// <returns></returns>
        public int DcExecImmediate(FS.HISFC.Models.Order.ExecOrder execOrder, FS.FrameWork.Models.NeuObject dcPerson)
        {
            #region ����ִ�е�
            //����ִ�е�(ҽ��ֹͣ��ֱ������)
            //Order.ExecOrder.DcExecImmediate
            //���룺0 id��1 ֹͣ��id,2ֹͣ��������3ֹͣʱ��,4���ϱ�־ 
            //������0 
            #endregion
            string strSql = "", strSqlName = "Order.ExecOrder.DcExecImmediateByExecOrderID.";
            string strType = "";

            strType = this.JudgeItemType(execOrder.Order);
            if (strType == "") return -1;

            strSqlName = strSqlName + strType;

            if (this.Sql.GetCommonSql(strSqlName, ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, execOrder.ID, dcPerson.ID, dcPerson.Name);
            }
            catch
            {
                this.Err = "�����������" + strSqlName;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ��ҽ����ˮ������ִ�е�
        /// </summary>
        /// <param name="Order"></param>
        /// <returns>0 success -1 fail</returns>
        public int DcExecLater(FS.HISFC.Models.Order.Inpatient.Order Order, FS.FrameWork.Models.NeuObject dcPerson)
        {
            #region ��ҽ����ˮ������ִ�е�
            //����ִ�е�(ҽ��ֹͣ��ֱ������)
            //Order.ExecOrder.DcExecLater
            //���룺0 orderid��1 ֹͣ��id,2ֹͣ��������3ֹͣʱ��
            //������0 
            #endregion

            string strSql = "", strSqlName = "Order.ExecOrder.DcExecLater.";

            string strType = "";
            strType = this.JudgeItemType(Order);
            if (strType == "") return -1;

            strSqlName = strSqlName + strType;

            if (this.Sql.GetCommonSql(strSqlName, ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                if (Order.OrderType.IsDecompose)
                {
                    //��������ֹͣʱ��֮���ִ�е�
                    strSql = string.Format(strSql, Order.ID, dcPerson.ID, dcPerson.Name, Order.EndTime);
                }
                else
                {
                    //�������Ͽ�����ʹ�ã�ʱ��֮���ִ�е�
                    strSql = string.Format(strSql, Order.ID, dcPerson.ID, dcPerson.Name, Order.MOTime);
                }
            }
            catch
            {
                this.Err = "�����������" + strSqlName;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }


        /// <summary>
        /// ����ҽ��״̬
        /// Ϊ�Ѿ�ִ��
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateOrderStatus(string orderNo, int status)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Order.Update.OrderStatus", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, orderNo, status.ToString());
            }
            catch (Exception ex)
            {
                this.Err = "����������ԣ�Order.Update.OrderStatus" + ex.Message;
                this.WriteErr();
                return -1;
            }
            //if(this.ExecNoQuery(strSql) <= 0) return -1;
            //return 0;
            return this.ExecNoQuery(strSql);
        }


        /// <summary>
        /// ����ҽ�������
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="sortID"></param>
        /// <returns></returns>
        public int UpdateOrderSortID(string orderID, string sortID)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.Order.updateOrderSort.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            return this.ExecNoQuery(strSql, orderID, sortID);
        }
        #endregion

        #region ���´�ӡ���

        /// <summary>
        /// ����ִ�е���ӡ���ͨ��ִ�е���ˮ��
        /// </summary>
        /// <param name="execOrderID"></param>
        /// <param name="itemType">1 ҩƷ ,2 ��ҩƷ</param>
        /// <returns></returns>
        public int UpdateExecOrderPrinted(string execOrderID, string itemType)
        {
            string strSql = "";
            if (itemType == "2")
            {
                //Order.ExecOrder.UpdateExecUndrugPrintFlag
                if (this.Sql.GetCommonSql("Order.ExecOrder.UpdateExecUndrugPrintFlag", ref strSql) == -1)
                {
                    this.Err = this.Sql.Err;
                    return -1;
                }
                try
                {
                    strSql = string.Format(strSql, execOrderID, this.Operator.ID);
                }
                catch
                {
                    this.Err = "����������ԣ�Order.ExecOrder.UpdateExecUndrugPrintFlag";
                    this.WriteErr();
                    return -1;
                }
            }
            else if (itemType == "1")
            {
                //Order.ExecOrder.UpdateExecDrugPrintFlag
                if (this.Sql.GetCommonSql("Order.ExecOrder.UpdateExecDrugPrintFlag", ref strSql) == -1)
                {
                    this.Err = this.Sql.Err;
                    return -1;
                }
                try
                {
                    strSql = string.Format(strSql, execOrderID, this.Operator.ID);
                }
                catch
                {
                    this.Err = "����������ԣ�Order.ExecOrder.UpdateExecDrugPrintFlag";
                    this.WriteErr();
                    return -1;
                }
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ����ִ�е���ӡ���ͨ��ҽ����ˮ��
        /// </summary>
        /// <param name="execOrderID"></param>
        /// <param name="itemType">1 ҩƷ ,2 ��ҩƷ</param>
        /// <returns></returns>
        public int UpdateExecOrderPrintedByMoOrder(string moOrder, string dt1, string dt2, string itemType)
        {
            string strSql = "";
            if (itemType == "2")
            {
                //Order.ExecOrder.UpdateExecUndrugPrintFlag
                if (this.Sql.GetCommonSql("Order.ExecOrder.UpdateExecUndrugPrintFlagByMoOrder", ref strSql) == -1)
                {
                    this.Err = this.Sql.Err;
                    return -1;
                }
                try
                {
                    strSql = string.Format(strSql, moOrder, dt1, dt2, this.Operator.ID);
                }
                catch
                {
                    this.Err = "����������ԣ�Order.ExecOrder.UpdateExecUndrugPrintFlagByMoOrder";
                    this.WriteErr();
                    return -1;
                }
            }
            else if (itemType == "1")
            {
                //Order.ExecOrder.UpdateExecDrugPrintFlag
                if (this.Sql.GetCommonSql("Order.ExecOrder.UpdateExecDrugPrintFlagByMoOrder", ref strSql) == -1)
                {
                    this.Err = this.Sql.Err;
                    return -1;
                }
                try
                {
                    strSql = string.Format(strSql, moOrder, dt1, dt2, this.Operator.ID);
                }
                catch
                {
                    this.Err = "����������ԣ�Order.ExecOrder.UpdateExecDrugPrintFlagByMoOrder";
                    this.WriteErr();
                    return -1;
                }
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// �����շѴ�ӡ
        /// </summary>
        /// <param name="execOrderID"></param>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public int UpdateExecNeedFeePrinted(string execOrderID, string itemType)
        {
            string strSql = "";
            if (itemType == "1")
            {
                //Order.ExecOrder.Drug.UpdateExecNeedFeePrinted
                if (this.Sql.GetCommonSql("Order.ExecOrder.Drug.UpdateExecNeedFeePrinted", ref strSql) == -1)
                {
                    this.Err = this.Sql.Err;
                    return -1;
                }
                try
                {
                    strSql = string.Format(strSql, execOrderID, this.Operator.ID);
                }
                catch
                {
                    this.Err = "����������ԣ�Order.ExecOrder.Drug.UpdateExecNeedFeePrinted";
                    this.WriteErr();
                    return -1;
                }
            }
            else if (itemType == "2")
            {
                //Order.ExecOrder.Undrug.UpdateExecNeedFeePrinted
                if (this.Sql.GetCommonSql("Order.ExecOrder.Undrug.UpdateExecNeedFeePrinted", ref strSql) == -1)
                {
                    this.Err = this.Sql.Err;
                    return -1;
                }
                try
                {
                    strSql = string.Format(strSql, execOrderID, this.Operator.ID);
                }
                catch
                {
                    this.Err = "����������ԣ�Order.ExecOrder.Undrug.UpdateExecNeedFeePrinted";
                    this.WriteErr();
                    return -1;
                }
            }
            return this.ExecNoQuery(strSql);
        }
        ///<summary>
        /// �����շѴ�ӡ
        /// </summary>
        /// <param name="execOrderID"></param>
        /// <returns></returns>
        public int UpdateTransfusionPrinted(string execOrderID)
        {
            int rev = -1;

            string strSql = "";
            //Order.ExecOrder.Drug.UpdateExecNeedFeePrinted
            if (this.Sql.GetCommonSql("Order.ExecOrder.UpdateTransfusionPrinted", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, execOrderID, this.Operator.ID);
            }
            catch
            {
                this.Err = "����������ԣ�Order.ExecOrder.UpdateTransfusionPrinted";
                this.WriteErr();
                return -1;
            }
            rev = this.ExecNoQuery(strSql);
            if (rev == -1)
            {
                return rev;
            }

            if (this.Sql.GetCommonSql("Order.ExecOrder.UpdateTransfusionPrinted.Undrug", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, execOrderID, this.Operator.ID);
            }
            catch
            {
                this.Err = "����������ԣ�Order.ExecOrder.UpdateTransfusionPrinted.Undrug";
                this.WriteErr();
                return -1;
            }
            rev = this.ExecNoQuery(strSql);
            return rev;
        }

        /// <summary>
        /// ����Ѳ�ؿ���ӡ���
        /// </summary>
        /// <param name="execOrderID"></param>
        /// <returns></returns>
        public int UpdateCircultPrinted(string execOrderID)
        {
            int rev = -1;
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.ExecOrder.UpdateCircultPrinted", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, execOrderID, this.Operator.ID);
            }
            catch
            {
                this.Err = "����������ԣ�Order.ExecOrder.UpdateCircultPrinted";
                this.WriteErr();
                return -1;
            }
            rev = this.ExecNoQuery(strSql);
            if (rev == -1)
            {
                return -1;
            }

            if (this.Sql.GetCommonSql("Order.ExecOrder.UpdateCircultPrinted.Undrug", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, execOrderID, this.Operator.ID);
            }
            catch
            {
                this.Err = "����������ԣ�Order.ExecOrder.UpdateCircultPrinted.Undrug";
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        public int UpdateCircultPrinted(string moOrder, string dt1, string dt2)
        {
            int rev = -1;
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.ExecOrder.UpdateCircultPrinted.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, moOrder, dt1, dt2);
            }
            catch
            {
                this.Err = "����������ԣ�Order.ExecOrder.UpdateCircultPrinted.1";
                this.WriteErr();
                return -1;
            }
            rev = this.ExecNoQuery(strSql);
            if (rev == -1)
            {
                return -1;
            }

            if (this.Sql.GetCommonSql("Order.ExecOrder.UpdateCircultPrinted.Undrug.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, moOrder, dt1, dt2);
            }
            catch
            {
                this.Err = "����������ԣ�Order.ExecOrder.UpdateCircultPrinted.Undrug.1";
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }



        #endregion

        #region "��ѯҽ��ִ����Ϣ"
        /// <summary>
        /// ��ѯ����ҽ��ִ�����
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="itemType">"" ȫ����1ҩ2��ҩ</param>
        /// <returns></returns>
        public ArrayList QueryExecOrder(string inpatientNo, string itemType)
        {
            #region ��ѯ����ҽ��ִ�����
            //��ѯ����ҽ��ִ�������ҩ����ҩ��
            //Order.ExecOrder.QueryPatientExec.1
            //���룺0 inpatientno
            //������ArrayList
            #endregion
            string[] s;
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();

            s = ExecOrderQuerySelect(itemType);
            for (int i = 0; i <= s.GetUpperBound(0); i++)
            {
                sql = s[i];
                if (sql == null) return null;
                if (this.Sql.GetCommonSql("Order.ExecOrder.QueryPatientExec.1", ref sql1) == -1)
                {
                    this.Err = "û���ҵ�Order.ExecOrder.QueryPatientExec.1�ֶ�!";
                    return null;
                }
                sql = sql + " " + string.Format(sql1, inpatientNo);
                addExecOrder(al, sql);
            }
            return al;
        }
        /// <summary>
        /// ����ѯ�Ƿ���Чִ��ҽ��
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="itemType">"" ȫ����1ҩ2��ҩ</param>
        /// <param name="isValid">�Ƿ���Ч</param>
        /// <returns></returns>
        public ArrayList QueryExecOrder(string inpatientNo, string itemType, bool isValid)
        {
            #region ����ѯ��Чִ��ҽ��
            //����ѯ��Чִ��ҽ��
            //Order.ExecOrder.QueryValidOrder.1
            //���룺0 inpatientno 1  IsValid
            //������ArrayList
            #endregion

            string[] s;
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();

            s = ExecOrderQuerySelect(itemType);
            for (int i = 0; i <= s.GetUpperBound(0); i++)
            {
                sql = s[i];
                if (sql == null) return null;
                if (this.Sql.GetCommonSql("Order.ExecOrder.QueryValidOrder.1", ref sql1) == -1)
                {
                    this.Err = "û���ҵ�Order.ExecOrder.QueryValidOrder.1�ֶ�!";
                    return null;
                }
                sql = sql + " " + string.Format(sql1, inpatientNo, FS.FrameWork.Function.NConvert.ToInt32(isValid).ToString());
                addExecOrder(al, sql);
            }
            return al;
        }

        /// <summary>
        /// �������뵥�Ų���δ����ִ��ȷ�ϵ�ҽ����ֻ������ҩƷ�ģ�
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="applyNO"></param>
        /// <returns></returns>
        public ArrayList QueryExecOrderByApplyNO(string inpatientNO, string applyNO)
        {
            string[] s;
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();

            s = ExecOrderQuerySelect("2");
            for (int i = 0; i <= s.GetUpperBound(0); i++)
            {
                sql = s[i];
                if (sql == null) return null;
                if (this.Sql.GetCommonSql("Order.ExecOrder.QueryApplyOrder.1", ref sql1) == -1)
                {
                    this.Err = "û���ҵ�Order.ExecOrder.QueryApplyOrder.1�ֶ�!";
                    return null;
                }
                sql = sql + " " + string.Format(sql1, inpatientNO, applyNO);
                addExecOrder(al, sql);
            }
            return al;
        }

        /// <summary>
        /// ����ѯ�Ƿ�ִ��ҽ��
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="itemType">"" ȫ����1ҩ2��ҩ</param>
        /// <param name="isExec"></param>
        /// <returns></returns>
        public ArrayList QueryExecOrderIsExec(string inpatientNo, string itemType, bool isExec)
        {
            #region ����ѯ�Ƿ�ִ��ҽ��
            //����ѯ�Ƿ�ִ��ҽ��
            //Order.ExecOrder.QueryExecOrder.1
            //���룺0 inpatientno 1 IsExec
            //������ArrayList
            #endregion

            string[] s;
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            s = ExecOrderQuerySelect(itemType);
            for (int i = 0; i <= s.GetUpperBound(0); i++)
            {
                sql = s[i];
                if (sql == null) return null;

                //{DA77B01B-63DF-4559-B264-798E54F24ABB}
                if (itemType == "")
                {
                    return null;
                }
                string strSqlName = "Order.ExecOrder.QueryExecOrder." + itemType;
                //{DA77B01B-63DF-4559-B264-798E54F24ABB}
                if (this.Sql.GetCommonSql(strSqlName, ref sql1) == -1)
                {
                    this.Err = "û���ҵ�" + strSqlName + "�ֶ�!";
                    return null;
                }
                sql = sql + " " + string.Format(sql1, inpatientNo, FS.FrameWork.Function.NConvert.ToInt32(isExec).ToString());
                addExecOrder(al, sql);
            }
            return al;
        }
        /// <summary>
        /// ����ѯִ��ҽ����Ϣ
        /// </summary>
        /// <param name="execOrderID"></param>
        /// <param name="itemType"></param>
        /// <returns>FS.HISFC.Models.Order.ExecOrder</returns>
        public FS.HISFC.Models.Order.ExecOrder QueryExecOrderByExecOrderID(string execOrderID, string itemType)
        {
            #region ����ѯ�Ƿ�ִ��ҽ��
            //����ѯ�Ƿ�ִ��ҽ��
            //Order.ExecOrder.QueryExecOrder.0
            //���룺0 ExecOrderID
            //������FS.HISFC.Models.Order.ExecOrder
            #endregion
            string[] s;
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            s = ExecOrderQuerySelect(itemType);
            for (int i = 0; i <= s.GetUpperBound(0); i++)
            {
                sql = s[i];
                if (sql == null) return null;
                if (this.Sql.GetCommonSql("Order.ExecOrder.QueryExecOrder.0", ref sql1) == -1)
                {
                    this.Err = "û���ҵ�Order.ExecOrder.QueryExecOrder.0�ֶ�!";
                    return null;
                }
                sql = sql + " " + string.Format(sql1, execOrderID);
                addExecOrder(al, sql);
            }
            if (al.Count > 0) return al[0] as FS.HISFC.Models.Order.ExecOrder;
            return null;
        }

        /// <summary>
        /// ��ִ�в��Ų�ѯ�Ƿ�ִ��ҽ��
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="itemType"></param>
        /// <param name="isExec"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public ArrayList QueryExecOrderByDept(string inpatientNo, string itemType, bool isExec, string deptCode)
        {
            #region ��ִ�в��Ų�ѯ�Ƿ�ִ��ҽ��
            //Order.ExecOrder.QueryExecOrderByDept.1
            //���룺0 inpatientno 1 IsExec 2 deptid
            //������ArrayList
            #endregion
            string[] s;
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            s = ExecOrderQuerySelect(itemType);
            for (int i = 0; i <= s.GetUpperBound(0); i++)
            {
                sql = s[i];
                if (sql == null) return null;
                if (this.Sql.GetCommonSql("Order.ExecOrder.QueryExecOrderByDept.1", ref sql1) == -1)
                {
                    this.Err = "û���ҵ�Order.ExecOrder.QueryExecOrder.1�ֶ�!";
                    return null;
                }
                sql = sql + " " + string.Format(sql1, inpatientNo, FS.FrameWork.Function.NConvert.ToInt32(isExec).ToString(), deptCode);
                addExecOrder(al, sql);
            }
            return al;
        }

        /// <summary>
        /// ����ѯ�Ƿ��շ�ҽ��
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="itemType"></param>
        /// <param name="isCharge"></param>
        /// <returns></returns>
        public ArrayList QueryExecOrderIsCharg(string inpatientNo, string itemType, bool isCharge)
        {
            #region ����ѯ�Ƿ��շ�ҽ��
            //����ѯ�Ƿ��շ�ҽ��
            //Order.ExecOrder.QueryChargeOrder.1
            //���룺0 inpatientno 1  IsCharge
            //������ArrayList
            #endregion
            string[] s;
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();

            s = ExecOrderQuerySelect(itemType);
            for (int i = 0; i <= s.GetUpperBound(0); i++)
            {
                sql = s[i];
                if (sql == null) return null;
                if (this.Sql.GetCommonSql("Order.ExecOrder.QueryChargeOrder.1", ref sql1) == -1)
                {
                    this.Err = "û���ҵ�Order.ExecOrder.QueryChargeOrder.1�ֶ�!";
                    return null;
                }
                sql = sql + " " + string.Format(sql1, inpatientNo, FS.FrameWork.Function.NConvert.ToInt32(isCharge).ToString());
                addExecOrder(al, sql);
            }
            return al;
        }

        /// <summary>
        /// ����ѯ��ҩ״̬ҽ��
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="drugFlag"></param>
        /// <returns></returns>
        public ArrayList QueryExecOrderByDrugFlag(string inpatientNo, DateTime beginTime, DateTime endTime, int drugFlag)
        {
            string[] s;
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();

            s = ExecOrderQuerySelect("1");
            sql = s[0];
            if (sql == null) return null;
            if (this.Sql.GetCommonSql("Order.ExecOrder.QueryOrderDrugFlag.1", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.ExecOrder.QueryOrderDrugFlag.1�ֶ�!";
                return null;
            }
            sql = sql + " " + string.Format(sql1, inpatientNo, drugFlag.ToString(), beginTime, endTime);
            return this.myExecOrderQuery(sql);
        }

        /// <summary>
        /// ����������ѯִ�е�
        /// </summary>
        /// <param name="Type">1 ҩƷ�� 2 ��ҩƷ</param>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public ArrayList BetchQueryExecOrder(string Type, string whereSql)
        {
            string sql = ExecOrderQuerySelect(Type)[0];
            sql = sql + "\r\n" + whereSql;
            return myExecOrderQuery(sql);
        }

        /// <summary>
        /// ����ѯ��ҩ״̬ҽ��
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="drugFlag"></param>
        /// <returns></returns>
        public ArrayList QueryExecOrderByDrugFlag(string inpatientNo, int drugFlag)
        {
            #region ����ѯ��ҩ״̬ҽ��
            //����ѯ��ҩ״̬ҽ��
            //Order.ExecOrder.QueryOrderDrugFlag.1
            //���룺0 inpatientno 1  DrugFlag
            //������ArrayList
            #endregion

            string[] s;
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();

            s = ExecOrderQuerySelect("1");
            sql = s[0];
            if (sql == null) return null;
            if (this.Sql.GetCommonSql("Order.ExecOrder.QueryOrderDrugFlag.2", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.ExecOrder.QueryOrderDrugFlag.2�ֶ�!";
                return null;
            }
            sql = sql + " " + string.Format(sql1, inpatientNo, drugFlag.ToString());
            return this.myExecOrderQuery(sql);
        }
        /// <summary>
        /// ��ҽ����ˮ�Ų�ѯҽ��ִ����Ϣ
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="itemType">1ҩ2��ҩ""ȫ��</param>
        /// <returns></returns>
        public ArrayList QueryExecOrderByOneOrder(string orderNo, string itemType)
        {
            string[] s;
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            #region ��ҽ����ˮ�Ų�ѯҽ��ִ����Ϣ
            //��ҽ����ˮ�Ų�ѯҽ��ִ����Ϣ
            //Order.ExecOrder.QueryOrder.where.5
            //���룺0 OrderNo
            //������ArrayList
            #endregion
            s = ExecOrderQuerySelect(itemType);
            for (int i = 0; i <= s.GetUpperBound(0); i++)
            {
                sql = s[i];
                if (sql == null) return null;
                if (this.Sql.GetCommonSql("Order.ExecOrder.Query.where.5", ref sql1) == -1)
                {
                    this.Err = "û���ҵ�Order.ExecOrder.Query.where.5�ֶ�!";
                    return null;
                }
                sql = sql + " " + string.Format(sql1, orderNo);
                addExecOrder(al, sql);
            }
            return al;
        }

        /// <summary>
        /// ͨ��һ����ҽ����ѯ���Ӧ�ĸ�����Ϣ{92A0CF31-27BC-472e-9C15-1ED2C8A81F36} by zhang.xs 2010.10.19
        /// </summary>
        /// <param name="execSqn">ִ�����к�</param>
        /// <param name="combNO">��Ϻ�</param>
        /// <returns></returns>
        public ArrayList QueryExecOrderSubtblByMainOrder(string execSqn, string combNO)
        {
            string[] s;
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            s = ExecOrderQuerySelect("2");
            for (int i = 0; i <= s.GetUpperBound(0); i++)
            {
                sql = s[i];
                if (sql == null) return null;
                if (this.Sql.GetCommonSql("Order.ExecOrderSubtbl.Query.where", ref sql1) == -1)
                {
                    this.Err = "û���ҵ�Order.ExecOrderSubtbl.Query.where�ֶ�!";
                    return null;
                }
                sql = sql + " " + string.Format(sql1, execSqn, combNO);
                addExecOrder(al, sql);
            }
            return al;
        }

        /// <summary>
        ///  ͨ��һ����ҽ����ѯ���Ӧ��ִ����Ϣ
        /// </summary>
        /// <param name="moOrder">ҽ����ˮ��</param>
        /// <param name="undrugCode">��ҩƷ���</param>
        /// <param name="confirmFlag">ȷ�ϱ�� 0δȷ��/1��ȷ��</param>
        /// <returns></returns>
        public ArrayList QueryExecOrderByOrderNo(string moOrder,string undrugCode,string confirmFlag)
        {
            string[] s;
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            s = ExecOrderQuerySelect("2");
            for (int i = 0; i <= s.GetUpperBound(0); i++)
            {
                sql = s[i];
                if (sql == null) return null;
                if (this.Sql.GetCommonSql("Order.ExecOrder.Query.where.1", ref sql1) == -1)
                {
                    this.Err = "û���ҵ�Order.ExecOrder.Query.where.1�ֶ�!";
                    return null;
                }
                sql = sql + " " + string.Format(sql1, moOrder,undrugCode,confirmFlag);
                addExecOrder(al, sql);
            }
            return al;
        }

        /// <summary>
        ///  ������Һ����ѯ
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="usageCode"></param>
        /// <param name="isPrinted"></param>
        /// <returns></returns>
        public ArrayList QueryOrderExec(string inpatientNo, DateTime beginTime, DateTime endTime, string usageCode, bool isPrinted)
        {
            string[] s;
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();

            s = ExecOrderQuerySelect("1");
            sql = s[0];
            if (sql == null)
                return null;

            if (usageCode.Contains("'"))
            {
                if (this.Sql.GetCommonSql("Order.ExecOrder.QueryOrderExec.NotSeprate.1", ref sql1) == -1)
                {
                    this.Err = "û���ҵ�Order.ExecOrder.QueryOrderExec.NotSeprate.1�ֶ�!";
                    this.WriteErr();
                    return null;
                }
            }
            else
            {
                if (this.Sql.GetCommonSql("Order.ExecOrder.QueryOrderExec.1", ref sql1) == -1)
                {
                    this.Err = "û���ҵ�Order.ExecOrder.QueryOrderExec.1�ֶ�!";
                    this.WriteErr();
                    return null;
                }
            }
            sql = sql + " " + string.Format(sql1, inpatientNo, beginTime.ToString(), endTime.ToString(), usageCode, FS.FrameWork.Function.NConvert.ToInt32(isPrinted).ToString());
            al = this.myExecOrderQuery(sql);

            s = ExecOrderQuerySelect("2");
            sql = s[0];
            if (sql == null)
                return null;

            if (usageCode.Contains("'"))
            {
                if (this.Sql.GetCommonSql("Order.ExecOrder.QueryUndrugOrderExec.NotSeprate.1", ref sql1) == -1)
                {
                    this.Err = "û���ҵ�Order.ExecOrder.QueryUndrugOrderExec.1�ֶ�!";
                    this.WriteErr();
                    return null;
                }
            }
            else
            {
                if (this.Sql.GetCommonSql("Order.ExecOrder.QueryUndrugOrderExec.1", ref sql1) == -1)
                {
                    this.Err = "û���ҵ�Order.ExecOrder.QueryUndrugOrderExec.1�ֶ�!";
                    this.WriteErr();
                    return null;
                }
            }
            sql = sql + " " + string.Format(sql1, inpatientNo, beginTime.ToString(), endTime.ToString(), usageCode, FS.FrameWork.Function.NConvert.ToInt32(isPrinted).ToString());
            ArrayList all = new ArrayList();
            all = this.myExecOrderQuery(sql);
            if (all != null)
            {
                al.AddRange(all);
            }

            return al;
        }

        /// <summary>
        /// ��ѯѲ�ؿ���Ϣ
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="usageCode"></param>
        /// <param name="isPrinted"></param>
        /// <returns></returns>
        public ArrayList QueryOrderCircult(string inpatientNo, DateTime beginTime, DateTime endTime, string usageCode, bool isPrinted)
        {
            string[] s;
            string sql = "", sql1 = "";

            s = ExecOrderQuerySelect("1");

            sql = s[0];
            if (sql == null) return null;
            if (this.Sql.GetCommonSql("Order.ExecOrder.QueryOrderExec.Circlue", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.ExecOrder.QueryOrderExec.Circlue�ֶ�!";
                this.WriteErr();
                return null;
            }
            sql = sql + " " + string.Format(sql1, inpatientNo, beginTime.ToString(), endTime.ToString(), usageCode, FS.FrameWork.Function.NConvert.ToInt32(isPrinted).ToString());
            return this.myExecOrderQuery(sql);
        }

        /// <summary>
        /// ��û���ִ�е�-ҩƷ�ͷ�ҩƷ
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="isPrinted"></param>
        /// <returns></returns>
        public ArrayList QueryOrderExecBill(string inpatientNo, DateTime beginTime, DateTime endTime, string billNo, bool isPrinted)
        {
            #region ����ִ�е���ѯ
            //����ִ�е���ѯ
            //Order.ExecOrder.QueryOrderExecBill
            //���룺0InpatientNo,1 ִ�е���ˮ�� 2DateTimeBegin,3 DateTimeEnd,4 PrintFlag
            //������ArrayList
            #endregion
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();

            if (this.Sql.GetCommonSql("Order.ExecOrder.QueryOrderExecBill.1", ref sql) == -1)
            {
                this.Err = "û���ҵ�Order.ExecOrder.QueryOrderExecBill.1�ֶ�!";
                return null;
            }
            sql = string.Format(sql, inpatientNo, billNo, beginTime.ToString(), endTime.ToString(), FS.FrameWork.Function.NConvert.ToInt32(isPrinted).ToString());
            addExecOrder(al, sql);
            //
            if (this.Sql.GetCommonSql("Order.ExecOrder.QueryOrderExecBill.2", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.ExecOrder.QueryOrderExecBill.2�ֶ�!";
                return null;
            }
            sql1 = string.Format(sql1, inpatientNo, billNo, beginTime.ToString(), endTime.ToString(), FS.FrameWork.Function.NConvert.ToInt32(isPrinted).ToString());
            addExecOrder(al, sql1);

            return al;
        }

        /// <summary>
        ///  ��ѯһ������ж�������Ҫִ�е�ҽ��
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="combno"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="billNo"></param>
        /// <param name="isPrinted"></param>
        /// <returns></returns>
        public ArrayList QueryOrderExecCountByCombno(string inpatientNo, string combno, DateTime beginTime, DateTime endTime, string billNo, bool isPrinted)
        {

            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();

            if (this.Sql.GetCommonSql("Order.ExecOrder.QueryOrderExecBill.3", ref sql) == -1)
            {
                this.Err = "û���ҵ�Order.ExecOrder.QueryOrderExecBill.1�ֶ�!";
                return null;
            }
            sql = string.Format(sql, inpatientNo, combno, billNo, beginTime.ToString(), endTime.ToString(), FS.FrameWork.Function.NConvert.ToInt32(isPrinted).ToString());
            addExecOrder(al, sql);
            //
            if (this.Sql.GetCommonSql("Order.ExecOrder.QueryOrderExecBill.4", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.ExecOrder.QueryOrderExecBill.2�ֶ�!";
                return null;
            }
            sql1 = string.Format(sql1, inpatientNo, combno, billNo, beginTime.ToString(), endTime.ToString(), FS.FrameWork.Function.NConvert.ToInt32(isPrinted).ToString());
            addExecOrder(al, sql1);
            return al;
        }

        /// <summary>
        /// ��û��߼��鵥��Ϣ
        /// </summary>
        /// <param name="inpatientNo">����סԺ��</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="isPrinted">��ӡ���</param>
        /// <returns></returns>
        public ArrayList QueryOrderLisApplyBill(string inpatientNo, DateTime beginTime, DateTime endTime, bool isPrinted)
        {
            #region ����ִ�е���ѯ
            //����ִ�е���ѯ
            //Order.ExecOrder.QueryOrderLisApplyBill
            //���룺0 InpatientNo,1 DateTimeBegin,2 DateTimeEnd,4 PrintFlag
            //������ArrayList
            #endregion

            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            string[] s = ExecOrderQuerySelect("2");
            if (s.Length > 0)
            {
                sql = s[0];
            }
            else
            {
                return null;
            }
            if (this.Sql.GetCommonSql("Order.ExecOrder.QueryOrderLisApplyBill", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.ExecOrder.QueryOrderLisApplyBill�ֶ�!";
                return null;
            }
            sql1 = string.Format(sql1, inpatientNo, beginTime.ToString(), endTime.ToString(), FS.FrameWork.Function.NConvert.ToInt32(isPrinted).ToString());
            sql1 = sql + " " + sql1;
            addExecOrder(al, sql1);
            return al;
        }
        /// <summary>
        /// ��ѯҽ���շѵ�
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="itemType"></param>
        /// <param name="isPrinted"></param>
        /// <returns></returns>
        public ArrayList QueryExecOrderBillNeedCharge(string inpatientNo, string itemType, bool isPrinted)
        {
            string[] s;
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();

            s = ExecOrderQuerySelect(itemType);
            sql = s[0];
            if (sql == null) return null;
            if (this.Sql.GetCommonSql("Order.ExecDrugUnDrug.QueryNoCharged.1", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.ExecDrugUnDrug.QueryNoCharged.1�ֶ�!";
                return null;
            }
            sql = sql + " " + string.Format(sql1, inpatientNo, FS.FrameWork.Function.NConvert.ToInt32(isPrinted).ToString());
            return this.myExecOrderQuery(sql);
        }
        /// <summary>
        /// ��ѯҽ��ִ�е����ݴ�����
        /// </summary>
        /// <param name="itemType"></param>
        /// <param name="receiptNo"></param>
        /// <returns></returns>
        public ArrayList QueryExecOrderBillByReceiptNo(string itemType, string receiptNo)
        {
            string[] s;
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();

            s = ExecOrderQuerySelect(itemType);
            sql = s[0];
            if (sql == null) return null;
            if (this.Sql.GetCommonSql("Order.QueryOrderExecBill.ReceiptNo", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.QueryOrderExecBill.ReceiptNo�ֶ�!";
                return null;
            }
            sql = sql + " " + string.Format(sql1, receiptNo);
            return this.myExecOrderQuery(sql);
        }
        /// <summary>
        /// ��ѯ��Ҫ��ҩ��ҽ����Ϣ
        /// </summary>
        /// <param name="deptcode"></param>
        /// <returns></returns>
        public ArrayList QureyExecOrderNeedSendDrug(string deptcode)
        {
            string[] s;
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();

            s = ExecOrderQuerySelect("1");

            sql = s[0];
            if (sql == null) return null;

            if (this.Sql.GetCommonSql("Order.ExecOrder.QueryNeedDrug", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.ExecOrder.QueryNeedDrug�ֶ�!";
                return null;
            }
            sql = sql + " " + string.Format(sql1, deptcode);
            addExecOrder(al, sql);

            return al;
        }
        /// <summary>
        /// ��ѯҩƷִ��ҽ��ͨ��סԺ��ˮ��
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public DataSet QueryExecDrugOrderByInpatientNo(string inpatientNo)
        {
            string strSql = "";
            DataSet dataSet = new DataSet();

            //ȡSQL���
            if (this.Sql.GetCommonSql("Order.Report.ExecDrugByInpatientNo", ref strSql) == -1)
            {
                this.Err = "û���ҵ�Order.Report.ExecDrugByInpatientNo�ֶ�!";
                return null;
            }
            try
            {
                strSql = string.Format(strSql, inpatientNo);    //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����Order.Report.ExecDrugByInpatientNo��" + ex.Message;
                this.WriteErr();
                return null;
            }

            //����SQL���ȡ��ѯ���
            if (this.ExecQuery(strSql, ref dataSet) == -1) return null;

            return dataSet;
        }

        /// <summary>
        /// ������ˮ�Ų�ѯ һ��ʱ���ڵ�ҽ��ִ�����
        /// </summary>
        /// <param name="inPatientNo"></param>
        /// <param name="Type">��� 1 ҩƷ 2��ҩƷ</param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ArrayList QueryExecOrder(string inPatientNo, string type, DateTime begin, DateTime end)
        {
            string[] s;
            string sql = "";
            ArrayList al = new ArrayList();

            s = ExecOrderQuerySelect(type);
            sql = s[0];
            if (sql == null)
            {
                return null;
            }

            string whereSql = "";
            if (type == "1")
            {
                if (this.Sql.GetCommonSql("Order.ExecOrder.Query.byInpatientNo.Durg", ref whereSql) == -1)
                {
                    this.Err = "û���ҵ�Order.ExecOrder.Query.byInpatientNo.Durg�ֶ�!";
                    return null;
                }
            }
            else
            {
                if (this.Sql.GetCommonSql("Order.ExecOrder.Query.byInpatientNo.UnDurg", ref whereSql) == -1)
                {
                    this.Err = "û���ҵ�Order.ExecOrder.Query.byInpatientNo.UnDurg�ֶ�!";
                    return null;
                }
            }
            whereSql = string.Format(whereSql, inPatientNo, begin, end);

            return this.myExecOrderQuery(sql + whereSql);
        }

        /// <summary>
        /// ����סԺ��ˮ�ź�ҽ����ˮ�Ų�ѯ����ҽ��
        /// </summary>
        /// <param name="inPatientNo"></param>
        /// <param name="type">���� 1ҩƷ 2��ҩƷ</param>
        /// <param name="strOrderID">����ҽ����ˮ����ɵ��ַ��� ��IN��ѯ</param>
        /// <returns></returns>
        public ArrayList QueryOrder(string inPatientNo, string type, string strOrderID)
        {
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            sql = OrderQuerySelect();
            if (sql == null) return null;
            if (this.Sql.GetCommonSql("Order.Order.QueryOrder.ByID", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.Order.QueryOrder.ByID�ֶ�!";
                return null;
            }
            sql = sql + " " + string.Format(sql1, inPatientNo, type, strOrderID);
            return this.MyOrderQuery(sql);
        }

        /// <summary>
        /// ���ݻ���סԺ��ˮ�ţ���ѯ��ҩƷҽ��ִ�е���Ϣ
        /// writed by cuipeng
        /// 2005-06
        /// </summary>
        /// <param name="inpatientNo">����סԺ��ˮ��</param>
        /// <returns></returns>
        public DataSet QueryExecUndrugOrderByInpatientNo(string inpatientNo)
        {
            string strSql = "";
            DataSet dataSet = new DataSet();

            //ȡSQL���
            if (this.Sql.GetCommonSql("Order.Report.ExecUndrugByInpatientNo", ref strSql) == -1)
            {
                this.Err = "û���ҵ�Order.Report.ExecUndrugByInpatientNo�ֶ�!";
                return null;
            }
            try
            {
                strSql = string.Format(strSql, inpatientNo);    //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����Order.Report.ExecUndrugByInpatientNo��" + ex.Message;
                this.WriteErr();
                return null;
            }

            //����SQL���ȡ��ѯ���
            if (this.ExecQuery(strSql, ref dataSet) == -1) return null;

            return dataSet;
        }


        /// <summary>
        /// ����סԺ��ˮ�š�ҩƷ���롢ʱ��μ��������ۼ���ҩ���  
        /// writed by liangjz 2005-06
        /// </summary>
        /// <param name="inpatientNo">סԺ��ˮ��</param>
        /// <param name="drugCode">ҩƷ����</param>
        /// <param name="myBeginTime">��ʼʱ��</param>
        /// <param name="myEndTime">��ֹʱ��</param>
        /// <returns>dataset</returns>
        public DataSet QueryTotalUseDrug(string inpatientNo, string drugCode, DateTime myBeginTime, DateTime myEndTime)
        {
            string strSql = "";
            DataSet dataSet = new DataSet();

            //ȡSQL���
            if (this.Sql.GetCommonSql("Order.Report.TotalUseDrug", ref strSql) == -1)
            {
                this.Err = "û���ҵ�Order.Report.TotalUseDrug�ֶ�!";
                return null;
            }
            try
            {
                strSql = string.Format(strSql, inpatientNo, drugCode, myBeginTime.ToString(), myEndTime.ToString());    //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����Order.Report.TotalUseDrug��" + ex.Message;
                this.WriteErr();
                return null;
            }

            //����SQL���ȡ��ѯ���
            if (this.ExecQuery(strSql, ref dataSet) == -1) return null;

            return dataSet;
        }
        /// <summary>
        /// ����ʱ�䣬��С���ã����Ҳ�ѯ�շѵ�ҩƷ��Ϣ
        /// </summary>
        /// <param name="minFee"></param>
        /// <param name="deptCode"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public DataSet QueryChargedMedicine(string minFee, string deptCode, string dtBegin, string dtEnd)
        {
            string strSql = "";
            DataSet dsMedicine = new DataSet();
            if (this.Sql.GetCommonSql("Fee.Item.QueryChargedMedicine", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql";
                return null;
            }
            strSql = System.String.Format(strSql, minFee, deptCode, dtBegin, dtEnd);
            this.ExecQuery(strSql, ref dsMedicine);
            return dsMedicine;
        }
        /// <summary>
        /// ����ʱ�䣬��С���ã����Ҳ�ѯ�շѵķ�ҩƷ��Ϣ
        /// </summary>
        /// <param name="minFee"></param>
        /// <param name="deptCode"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public DataSet QueryChargedItem(string minFee, string deptCode, string dtBegin, string dtEnd)
        {
            string strSql = "";
            DataSet dsItem = new DataSet();
            if (this.Sql.GetCommonSql("Fee.Item.QueryChargedItem", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql";
                return null;
            }
            strSql = System.String.Format(strSql, minFee, deptCode, dtBegin, dtEnd);
            this.ExecQuery(strSql, ref dsItem);
            return dsItem;
        }
        /// <summary>
        /// ����ʱ�䣬�����ѯ�շѵ�ҩƷ��ϸ��Ϣ
        /// </summary>
        /// <param name="code"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public DataSet QueryChargedMedicineDetail(string code, string deptCode, string dtBegin, string dtEnd)
        {
            string strSql = "";
            DataSet dsMedicine = new DataSet();
            if (this.Sql.GetCommonSql("Fee.Item.QueryChargedMedicineDetail", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql";
                return null;
            }
            strSql = System.String.Format(strSql, code, deptCode, dtBegin, dtEnd);
            this.ExecQuery(strSql, ref dsMedicine);
            return dsMedicine;
        }
        /// <summary>
        /// ����ʱ�䣬�����ѯ�շѵķ�ҩƷ��ϸ��Ϣ
        /// </summary>
        /// <param name="code"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public DataSet QueryChargedItemDetail(string code, string deptCode, string dtBegin, string dtEnd)
        {
            string strSql = "";
            DataSet dsItem = new DataSet();
            if (this.Sql.GetCommonSql("Fee.Item.QueryChargedItemDetail", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql";
                return null;
            }
            strSql = System.String.Format(strSql, code, deptCode, dtBegin, dtEnd);
            this.ExecQuery(strSql, ref dsItem);
            return dsItem;
        }
        #endregion

        #region �жϳ�Ժ��ҩ���Ƿ�ȫ���շ�
        /// <summary>
        /// �жϳ�Ժ��ҩ���Ƿ�ȫ���շ�
        /// </summary>
        /// <param name="inpatient_no"></param>
        /// <returns></returns>
        public int IsCanPrintOutHosDrug(string inpatient_no, ref bool bReturn)
        {
            bReturn = false;
            string sql = "Order.ExecOrder.QueryIsCanPrintOutHosDrug";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1)
            {
                this.Err = "�޷��鵽Order.ExecOrder.QueryIsCanPrintOutHosDrug";
                return -1;
            }
            try
            {
                sql = string.Format(sql, inpatient_no);
            }
            catch
            {
                this.Err = "Order.ExecOrder.QueryIsCanPrintOutHosDrug��������!";
                return -1;
            }
            int i = FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(sql));
            if (i < 0)
            {

                return -1;
            }
            else if (i == 0)
            {
                bReturn = true;//���Գ�Ժ
                return 0;
            }
            else
            {
                return 0;
            }
        }
        #endregion

        /// <summary>
        /// ����ҽ���Ż�ȡ��Ӧ����
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="moOrder"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int QueryOrderFees(string inpatientNo, string moOrder, ref DataSet ds)
        {
            string sql = "Order.ExecOrder.QueryOrderFees";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1)
            {
                this.Err = "�޷��鵽Order.ExecOrder.QueryOrderFees";
                return -1;
            }
            try
            {
                sql = string.Format(sql, inpatientNo, moOrder);
            }
            catch
            {
                this.Err = "Order.ExecOrder.QueryOrderFees��������!";
                return -1;
            }

            this.ExecQuery(sql, ref ds);

            return 1;
        }

        #endregion

        #region "ҽ����˱���"
        /// <summary>
        /// ���ҽ�� -���δ��˺����ϵ�ҽ��
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int ConfirmOrder(FS.HISFC.Models.Order.Inpatient.Order order, bool IsCharge, System.DateTime dt)
        {
            #region ���ҽ��
            //���ҽ����ʹҽ��������Ч״̬
            //Order.Order.ConfirmOrder.1
            //���룺0 id,1 confirmcode,2 status 3confirmtime
            //������0 
            #endregion

            try
            {
                string strSql = "";
                if (order.Status == 0)//δ��˵�ҽ��
                {
                    if (this.Sql.GetCommonSql("Order.Order.ConfirmOrder.1", ref strSql) == -1)
                    {
                        return -1;
                    }

                    //if(order.Item.IsPharmacy==false && IsCharge)//�շѵķ�ҩƷ
                    if (order.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug 
                        && IsCharge)//�շѵķ�ҩƷ    
                    {
                        order.Status = 2;//��ִ�б��
                    }
                    else
                    {
                        order.Status = 1;//����˱��
                    }
                }
                else if (order.Status == 3 || order.Status == 4)//ֹͣ����ҽ��
                {
                    if (this.Sql.GetCommonSql("Order.Order.ConfirmOrder.2", ref strSql) == -1)
                        return -1;
                }
                else//����
                {
                    if (order.IsSubtbl)
                    {
                        if (this.Sql.GetCommonSql("Order.Order.ConfirmOrder.2", ref strSql) == -1)
                            return -1;
                    }
                    else
                    {
                        this.Err = FS.FrameWork.Management.Language.Msg("ҽ��״̬�����ϣ�������ˣ�");
                        return -1;
                    }
                }
                try//��ֵ
                {
                    strSql = string.Format(strSql, order.ID, this.Operator.ID, order.Status.ToString(), dt);
                }
                catch
                {
                    this.Err = "����������ԣ�Order.Order.ConfirmOrder.1";
                    this.WriteErr();
                    return -1;
                }
                int intErr = 0;
                intErr = this.ExecNoQuery(strSql);//ִ��ҽ��
                if (intErr == 0)
                {
                    this.Err = order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "�� " + order.Patient.Name + "\r\n\r\n" + order.SubCombNO.ToString() + "�� " + order.Item.Name + "[" + order.Item.Specs + "] " + "\r\n\r\n�����仯��������ˣ�\n ��ˢ�½������¼���ҽ����Ϣ��";
                    return -1;
                }
                else if (intErr < 0)
                {
                    this.Err = FS.FrameWork.Management.Language.Msg(order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "�� " + order.Patient.Name + "\r\n\r\n" + order.SubCombNO.ToString() + "�� " + order.Item.Name + "[" + order.Item.Specs + "] " + "\r\n\r\n���ʧ�ܣ�") + Err;
                    return -1;
                }
            }
            catch (Exception ex)
            {
                this.Err = "���ҽ�������쳣��" + ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ���ҽ�������漰ִ�е����շѽӿ��ɱ��ֲ���ɣ�
        ///(��ˡ�ִ�б�����;ͨ���Ƿ��շѸ����շѱ�ǣ�
        /// </summary>
        /// <param name="order"></param>
        /// <param name="isCharge"></param>
        /// <param name="execID"></param>
        /// <returns></returns>
        private int ConfirmAndExecOrder(FS.HISFC.Models.Order.Inpatient.Order order, bool isCharge, string execID)
        {
            FS.HISFC.Models.Base.Employee CurUser = (FS.HISFC.Models.Base.Employee)this.Operator;
            FS.HISFC.Models.Order.ExecOrder objExec = new FS.HISFC.Models.Order.ExecOrder();

            //��ֵҽ����Ŀ
            objExec.Order = order;

            //��Ǹ���Ŀ�Ƿ����ն�ȷ����Ŀ
            if (order.Item.GetType() == typeof(FS.HISFC.Models.Fee.Item.Undrug))
            {
                objExec.Order.Item.IsNeedConfirm = ((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm;
            }

            //�����ǻ�ʿ�Ѿ�������ҽ���ˣ��ն�ȷ����IsExec�������Ƿ�ȷ��ִ��
            objExec.IsConfirm = true;

            #region ����ҽ��
            if (order.OrderType.IsDecompose == false) //��ʱҽ��
            {
                //ִ�п����Ǳ���ʿվ �� �����ն�����
                //if ((order.ExeDept.ID == order.Patient.PVisit.PatientLocation.Dept.ID) ||
                //    (JudgeItemType(objExec.Order) == "2" &&
                //    ((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm == false))
                //{
                //    //��ҩƷ Order.OrderType.IsCharge == false
                //    if (JudgeItemType(objExec.Order) == "2" &&
                //        ((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm == false)
                //    {
                //        //����ִ�е�ִ�б�־
                //        objExec.IsExec = true;
                //        objExec.ExecOper.ID = CurUser.ID;
                //        objExec.Order.ExeDept.ID = order.ExeDept.ID;
                //        objExec.ExecOper.OperTime = dtCurTime;
                //        //����ҽ������ִ�б�־
                //        if (this.UpdateOrderExecuted(order.ID) < 0)
                //            return -1;
                //    }
                //}


                //----���ֲ�Ƿ�----
                if (isCharge)
                {
                    //����ִ�е����ʱ�־
                    objExec.IsCharge = true;
                    objExec.ChargeOper.ID = CurUser.ID;
                    objExec.ChargeOper.Dept.ID = CurUser.Dept.ID;
                    objExec.ChargeOper.OperTime = dtCurTime;

                    //�������շ���Ŀ ����ִ�б��Ϊ��ִ�С�
                    objExec.IsExec = true;
                    objExec.ExecOper.ID = CurUser.ID;
                    objExec.Order.ExeDept.ID = order.ExeDept.ID;
                    objExec.ExecOper.OperTime = dtCurTime;

                    //����ҽ������ִ�б�־
                    if (this.UpdateOrderExecuted(order.ID) < 0)
                        return -1;
                }

                //�Բ���Ҫ�ն�ȷ�ϵ� ��Ȼ��Ҫ��ִ�п��ҽ��и�ֵ
                objExec.ExecOper.ID = CurUser.ID;
                objExec.Order.ExeDept.ID = order.ExeDept.ID;
                objExec.ExecOper.Dept = order.ExeDept;
                objExec.ExecOper.OperTime = dtCurTime;

                //����ִ�е�
                if (execID == "")
                {
                    try
                    {
                        objExec.ID = GetNewOrderExecID();
                    }
                    catch { }
                }
                else
                {
                    objExec.ID = execID;
                }

                if (objExec.ID == "-1" || objExec.ID == "") return -1;


                if (JudgeItemType(objExec.Order) == "1")//ҩƷ��ִ�б��
                {
                    objExec.IsExec = true;
                    objExec.ExecOper.ID = CurUser.ID;
                    objExec.Order.ExeDept.ID = order.ExeDept.ID;
                    objExec.ExecOper.OperTime = dtCurTime;
                    //ҩƷ����С��λ
                    if (objExec.Order.Unit == ((FS.HISFC.Models.Pharmacy.Item)objExec.Order.Item).MinUnit)//��С��λ
                    {

                    }
                    else
                    {
                        objExec.Order.Qty = objExec.Order.Qty * objExec.Order.Item.PackQty; ;//�����С��λ
                        objExec.Order.Unit = ((FS.HISFC.Models.Pharmacy.Item)objExec.Order.Item).MinUnit;
                    }
                }
                else//��ҩƷ
                {
                    /*
                     * ��ҩƷ��ִ��������Ƿ���Ҫ�ն�ȷ�Ͼ��������շ�����޹�
                     * ��ʿվȷ���շ�ֻ��ѯ��ִ�б�־����Ŀ���ն���Ŀ���ڻ�ʿվ�շ�
                     */
                    objExec.IsExec = !objExec.Order.Item.IsNeedConfirm;
                }

                objExec.IsValid = true;
                objExec.DateUse = order.BeginTime;
                objExec.DateDeco = dtCurTime;
                objExec.DrugFlag = 0; //Ĭ��Ϊ����Ҫ����

                if (this.InsertExecOrder(objExec) < 0)
                {
                    return -1;
                }
            }
            #endregion


            //���ҽ��
            if (this.ConfirmOrder(order, isCharge, dtCurTime) < 0)
            {
                return -1;
            }
            return 0;

        }
        /// <summary>
        /// ������˺���
        /// </summary>
        /// <param name="order"></param>
        /// <param name="isCharge"></param>
        /// <param name="execID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int ConfirmAndExecOrder(FS.HISFC.Models.Order.Inpatient.Order order, bool isCharge, string execID, DateTime dt)
        {
            this.dtCurTime = dt;
            return this.ConfirmAndExecOrder(order, isCharge, execID);
        }
        #endregion

        #region "ҽ���ֽ�"

        /// <summary>
        /// ��ǰ����ҽ��
        /// </summary>
        public ArrayList AlAllOrders = new ArrayList();

        public Hashtable HsUsageAndTime = new Hashtable();

        /// <summary>
        /// ���ϵͳ���õķֽ�ʱ��
        /// </summary>
        /// <returns></returns>
        public void GetDecomposeTime(FS.HISFC.Models.Order.Inpatient.Order order, ref int hour, ref int minute)
        {
            int hour_back = hour;
            int minute_back = minute;

            #region ��ҩƷ

            if (order.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                if (order.IsSubtbl)
                {
                    //����ȥ�Ҷ�Ӧ��ҽ��
                    string usageCode = "NONE";

                    foreach (FS.HISFC.Models.Order.Inpatient.Order ord in this.AlAllOrders)
                    {
                        if (ord.Combo.ID == order.Combo.ID)
                        {
                            usageCode = ord.Usage.ID;
                            break;
                        }
                    }

                    if (string.IsNullOrEmpty(usageCode))
                    {
                        usageCode = "NONE";
                    }

                    FS.FrameWork.Models.NeuObject obj = new NeuObject();

                    obj = HsUsageAndTime[usageCode] as FS.FrameWork.Models.NeuObject;

                    if (obj != null && obj.Memo.Length > 0)
                    {
                        try
                        {
                            hour = FS.FrameWork.Function.NConvert.ToDateTime(obj.Memo).Hour;
                            minute = FS.FrameWork.Function.NConvert.ToDateTime(obj.Memo).Minute;
                        }
                        catch
                        {
                            //û��ά���Ͱ������ڵĿ��Ʋ�����,�����Ѿ�����ʱ�䣬�˴���������
                            hour = hour_back;//Ĭ��12��
                            minute = minute_back;
                        }
                    }
                    else
                    {
                        //û��ά���Ͱ������ڵĿ��Ʋ�����,�����Ѿ�����ʱ�䣬�˴���������
                        hour = hour_back;//Ĭ��12��
                        minute = minute_back;
                    }
                }
                else
                {
                    //��ҩƷ������÷���ȡ�÷���Ӧ�ķֽ�ʱ�䣬û��ȡNONE
                    FS.FrameWork.Models.NeuObject obj = new NeuObject();

                    if (!string.IsNullOrEmpty(order.Usage.ID))
                    {
                        obj = HsUsageAndTime[order.Usage.ID] as FS.FrameWork.Models.NeuObject;
                    }
                    else
                    {
                        obj = HsUsageAndTime["NONE"] as FS.FrameWork.Models.NeuObject;
                    }

                    if (obj != null && obj.Memo.Length > 0)
                    {
                        try
                        {
                            hour = FS.FrameWork.Function.NConvert.ToDateTime(obj.Memo).Hour;
                            minute = FS.FrameWork.Function.NConvert.ToDateTime(obj.Memo).Minute;
                        }
                        catch
                        {
                            //û��ά���Ͱ������ڵĿ��Ʋ�����,�����Ѿ�����ʱ�䣬�˴���������
                            hour = hour_back;//Ĭ��12��
                            minute = minute_back;
                        }
                    }
                    else
                    {
                        //û��ά���Ͱ������ڵĿ��Ʋ�����,�����Ѿ�����ʱ�䣬�˴���������
                        hour = hour_back;//Ĭ��12��
                        minute = minute_back;
                    }
                }
            }
            #endregion

            #region ҩƷ
            else
            {
                FS.FrameWork.Models.NeuObject obj = new NeuObject();

                if (order.Usage.ID.Length > 0)
                {
                    obj = HsUsageAndTime[order.Usage.ID] as FS.FrameWork.Models.NeuObject;
                }
                else
                {
                    obj = HsUsageAndTime["NONE"] as FS.FrameWork.Models.NeuObject;
                }

                if (obj != null && obj.Memo.Length > 0)
                {
                    try
                    {
                        hour = FS.FrameWork.Function.NConvert.ToDateTime(obj.Memo).Hour;
                        minute = FS.FrameWork.Function.NConvert.ToDateTime(obj.Memo).Minute;
                    }
                    catch
                    {
                        //û��ά���Ͱ������ڵĿ��Ʋ�����,�����Ѿ�����ʱ�䣬�˴���������
                        hour = hour_back;//Ĭ��12��
                        minute = minute_back;
                    }
                }
                else
                {
                    //û��ά���Ͱ������ڵĿ��Ʋ�����,�����Ѿ�����ʱ�䣬�˴���������
                    hour = hour_back;//Ĭ��12��
                    minute = minute_back;
                }
            }
            #endregion
            return;
        }

        /// <summary>
        /// ҽ���ֽ⵽�´�ʱ��
        /// </summary>
        string s = null;

        /// <summary>
        /// ���ϵͳ���õķֽ�ʱ��
        /// </summary>
        /// <returns></returns>
        public void DecomposeTime(string nurseStationCode)
        {
            //if (nurseStationCode == strNurseStationCode)
            //{

            //}
            if (nurseStationCode == "")
            {

            }
            else //�仯
            {
                //������
                controler.SetTrans(this.Trans);
                //ҽ���ֽ⵽�´ε�ʱ��
                if (string.IsNullOrEmpty(s))
                {
                    s = controler.QueryControlerInfo("200011", false);
                }
                this.strNurseStationCode = nurseStationCode;
                if (s == "-1" || s == "")
                {
                    iHour = 12;//Ĭ��12��
                    iMinute = 01;
                    return;
                }
                iHour = FS.FrameWork.Function.NConvert.ToDateTime(s).Hour;
                iMinute = FS.FrameWork.Function.NConvert.ToDateTime(s).Minute;
            }
            return;
        }

        /// <summary>
        /// {97FA5C9D-F454-4aba-9C36-8AF81B7C9CCF} �ֽⷽ��
        /// </summary>
        /// <param name="order"></param>
        /// <param name="days"></param>
        /// <param name="isCharge"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int DecomposeOrderToNow(FS.HISFC.Models.Order.Inpatient.Order order, int days, bool isCharge, DateTime dt)
        {
            dtCurTime = dt;
            int myDays = 0;
            myDays = System.DateTime.Compare(order.NextMOTime.Date, dt.Date);
            return DecomposeOrder(order, days, isCharge);
        }


        /// <summary>
        /// ���طֽ⺯��
        /// </summary>
        /// <param name="order"></param>
        /// <param name="days"></param>
        /// <param name="isCharge"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int DecomposeOrder(FS.HISFC.Models.Order.Inpatient.Order order, int days, bool isCharge, DateTime dt)
        {
            //dtCurTime = dt;

            //�ϴηֽ�ʱ�� Ҳȡ�趨ֵ houwb 2012-2-9 22:07:12
            //dtCurTime = new DateTime(dt.Year, dt.Month, dt.Day, 12, 01, 0);

            CurUser = (FS.HISFC.Models.Base.Employee)this.Operator;
            DecomposeTime(CurUser.Nurse.ID);
            GetDecomposeTime(order, ref iHour, ref iMinute);

            dtCurTime = new DateTime(dt.Year, dt.Month, dt.Day, iHour, iMinute, 0);
            return DecomposeOrder(order, days, isCharge);
        }

        /// <summary>
        /// ��ǰ�շ�Ա
        /// </summary>
        FS.HISFC.Models.Base.Employee CurUser = null;

        FS.HISFC.Models.Order.ExecOrder objExec = null;

        /// <summary>
        ///  ҩ����ҩ���ֽ�
        ///  Days�ֽ�����IsCharge �Ƿ��շ�
        /// </summary>
        /// <param name="order"></param>
        /// <param name="days">�ֽ�����</param>
        /// <param name="isCharge">�Ƿ��շ�</param>
        /// <returns></returns>
        public int DecomposeOrder(FS.HISFC.Models.Order.Inpatient.Order order, int days, bool isCharge)
        {
            #region ҽ���ֽ�ʱ���ҩƷ���İ�ҩ�Ʒѣ��ֽⲻ�շѣ���Ҫȷ��״̬

            //�Ƿ�ʹ��ҩƷִ�С��۷ѷֿ����� 0 ͬʱ���� 1 ��ͬʱ����
            if (!isGet_bCharge)
            {
                bCharge = FS.FrameWork.Function.NConvert.ToBoolean(this.controler.QueryControlerInfo("S00020", false));
                isGet_bCharge = true;
            }

            //��ҩ�Ʒ�
            if (!bCharge)
            {
                if (order.IsSubtbl)
                {
                    //ҩƷ��Ʒ�ʱ��ҩƷ�����Ƿ���ҩƷͬʱ�Ʒ� 1 ��ʿվ�Ʒ� 0 ҩ���Ʒ�
                    if (!this.isGet_bChargeSubtbl)
                    {
                        bChargeSubtbl = FS.FrameWork.Function.NConvert.ToBoolean(this.controler.QueryControlerInfo("200050", false));
                        isGet_bChargeSubtbl = true;
                    }
                    //ҩƷ������ҩƷ��Ʒ�
                    if (!bChargeSubtbl)
                    {
                        ArrayList alSubtblOrder = this.QueryOrderByCombNO(order.Combo.ID, true);
                        if (alSubtblOrder == null || alSubtblOrder.Count <= 1)
                        {
                            this.Err = "���ݸ��Ĳ�ѯ��ҩ����";
                            return -1;
                        }
                        foreach (FS.HISFC.Models.Order.Inpatient.Order subtblOrder in alSubtblOrder)
                        {
                            if (!subtblOrder.IsSubtbl)
                            {
                                //ȷ������ҩΪҩƷ
                                if (subtblOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                                {
                                    order.Item.IsNeedConfirm = true;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            #endregion

            //��õ�ǰ����Ա
            CurUser = (FS.HISFC.Models.Base.Employee)this.Operator;

            objExec = new FS.HISFC.Models.Order.ExecOrder();

            //��ֵҽ����Ŀ
            objExec.Order = order;

            //��Ǹ���Ŀ�Ƿ����ն�ȷ����Ŀ
            if (order.Item.GetType() == typeof(FS.HISFC.Models.Fee.Item.Undrug))
            {
                objExec.Order.Item.IsNeedConfirm = ((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm;
            }

            //�ϴηֽ�ʱ��
            //DateTime dtCurDeco = new DateTime(dtCurTime.Year, dtCurTime.Month, dtCurTime.Day, iHour, iMinute, 00);
            DateTime dtCurDeco = dtCurTime;

            //�ֽ�����
            decimal DecAmount = -1;

            //�ֽ�������
            int Cycle = 0;

            #region �ֽⳤ��

            //���ڡ�����Ч���ֽ�ʱ��С��ָ��ʱ���ҽ���͸���(���г����ֽⲻ�շ�)
            if (order.OrderType.IsDecompose
                && (order.Status == 1 || order.Status == 2)
                && order.NextMOTime <= dtCurDeco.AddDays(days))
            {

                //-------����ÿ������--------	
                if (order.OrderType.IsCharge)
                {
                    #region ��Ҫ�Ʒѡ�ҩƷҽ����Ҫ����ҩƷȡ���������
                    if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Item objPharmacy;
                        objPharmacy = (FS.HISFC.Models.Pharmacy.Item)order.Item;

                        DecAmount = ComputeAmount(objPharmacy.ID, objPharmacy.DosageForm.ID, order.DoseOnce, objPharmacy.BaseDose, order.Patient.PVisit.PatientLocation.Dept.ID);
                        if (DecAmount >= 0)
                        {
                            order.Item.Qty = DecAmount;
                        }
                    }
                    #endregion
                    //����Ʒѡ�ҩƷҽ��ֱ�ӻ��ÿ������
                    //��ҩƷ�����ģ�ֱ�ӻ��ÿ��ִ������		
                }

                //�ֽ�������
                Cycle = System.Convert.ToInt16(order.Frequency.Days[0]);

                if (order.Frequency.Days.Length > 1)//�ֽ�����Ϊ����
                {
                    Cycle = 1;
                }

                //�Ƿ��ѷֽ⣬�ֽ����·ֽ�ʱ��
                bool bIsHaveDecompose = false;

                //�ֽ����ִ�е�����
                int iMOCount = 0;

                //���Ĭ�ϵķֽ��ֹʱ���
                DecomposeTime(CurUser.Nurse.ID);
                this.GetDecomposeTime(order, ref iHour, ref iMinute);


                //Сʱ�Ʒ�ҽ����Ƶ��
                if (string.IsNullOrEmpty(this.hourFerquenceID))
                {
                    this.hourFerquenceID = this.controler.QueryControlerInfo("200042", false);
                    if (string.IsNullOrEmpty(hourFerquenceID) == true)
                    {
                        this.hourFerquenceID = "NONE";
                    }
                }

                //��ʱҽ���ֽ�ʱ�䵽���ηֽ�ʱ�䣫days
                if (order.Frequency.ID == this.hourFerquenceID)
                {
                    iHour = dtCurDeco.Hour;
                    iMinute = dtCurDeco.Minute;
                }

                //����ʱ��
                DateTime dtTemp = dtCurDeco.AddDays(days);
                DateTime dtEndTime;

                dtEndTime = new DateTime(dtTemp.Year, dtTemp.Month, dtTemp.Day, iHour, iMinute, 0);

                //�´�ִ������<ָ�������ڣ��ֽ�������
                int Count = System.Convert.ToInt16(new TimeSpan(order.NextMOTime.Date.Ticks - dtCurDeco.Date.Ticks).TotalDays);

                if (order.Frequency.Days.Length > 1)
                {
                    #region �ֽ�ҽ��Ϊ���ڵ�
                    for (int i = 0; i <= days; i++)
                    {
                        for (int k = 1; k < order.Frequency.Days.Length; k++)//ѭ��������
                        {
                            int week = dtCurTime.AddDays(i).DayOfWeek.GetHashCode();
                            if (week == 0)
                                week = 7;

                            if (order.Frequency.Days[k] == week.ToString())//�ҵ����µ�
                            {
                                if (this.DecomposeTime(order, dtCurDeco, dtEndTime, isCharge, objExec, dtCurTime, CurUser, i) == -1)
                                {
                                    return -1;
                                }
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region �ֽ�ҽ��Ϊ����ÿ���
                    while (Count <= (days + Cycle - 1))
                    {
                        #region �ֽ�ʱ��
                        for (int i = 0; i <= order.Frequency.Times.GetUpperBound(0); i++)
                        {
                            DateTime dt = new DateTime();
                            try
                            {
                                dt = FS.FrameWork.Function.NConvert.ToDateTime(order.Frequency.Times[i]);
                            }
                            catch
                            {
                            }
                            if (dt.GetType().ToString() != "System.DateTime")
                            {
                                return -1;
                            }
                            DateTime dtUseTime = new DateTime(dtCurDeco.AddDays(Count).Year, dtCurDeco.AddDays(Count).Month, dtCurDeco.AddDays(Count).Day, dt.Hour, dt.Minute, dt.Second);

                            //��ҩʱ��>=�´�ִ������and��ҩʱ��<�ֽ����ʱ�䣿
                            //wolf �����ˣ���֪���᲻������,���뿿Ψһ���������ظ���¼Date_NexMO
                            if (dtUseTime >= order.NextMOTime && dtUseTime < dtEndTime)
                            {
                                //���ĵ�Ԥֹͣ����
                                if (order.IsSubtbl)
                                {
                                    ArrayList al = this.QueryOrderByCombNO(order.Combo.ID, false);
                                    if (al.Count > 0)
                                    {
                                        FS.HISFC.Models.Order.Inpatient.Order subOrder = al[0] as FS.HISFC.Models.Order.Inpatient.Order;
                                        if (dtUseTime > subOrder.EndTime && subOrder.EndTime != DateTime.MinValue)
                                        {
                                            order.EndTime = subOrder.EndTime;
                                        }
                                    }
                                }



                                //��ҩʱ���Ƿ����ҽ��ֹͣʱ��?
                                if (dtUseTime > order.EndTime && order.EndTime != DateTime.MinValue)
                                {
                                    //ֹͣ����ҽ��
                                    order.Status = 3;
                                    order.DCOper.OperTime = order.EndTime;
                                    if (DcOneOrder(order) < 0)
                                        return -1;
                                }
                                else
                                {
                                    //----���ֲ�Ƿ�----
                                    if (isCharge)
                                    {
                                        //����ִ�е����ʱ�־
                                        objExec.IsCharge = true;
                                        objExec.ChargeOper.ID = CurUser.ID;
                                        objExec.ChargeOper.Dept.ID = CurUser.Dept.ID;
                                        objExec.ChargeOper.OperTime = dtCurTime;
                                    }
                                    //����ִ�е�
                                    try
                                    {
                                        objExec.ID = GetNewOrderExecID();
                                    }
                                    catch { }
                                    if (objExec.ID == "-1" || objExec.ID == "") return -1;
                                    objExec.IsValid = true;
                                    objExec.DateUse = dtUseTime;
                                    objExec.DateDeco = dtCurTime;
                                    objExec.DrugFlag = 0; //Ĭ��Ϊ����Ҫ����

                                    if (objExec.Order.Item.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))//ҩƷ
                                    {
                                        try
                                        {   //����ͨ��������������С��λ���ڼ������
                                            //ԭ�����жϵ�Frequency.User01 ����Ϊʹ�� ExecDose����
                                            //if(objExec.Order.Frequency.User01 != "" && objExec.Order.Frequency.User01 != null) //����Ƶ��
                                            if (objExec.Order.ExecDose != "" && objExec.Order.ExecDose != null) //����Ƶ��
                                            {
                                                string[] tempDoseOnce = objExec.Order.ExecDose.Split('-');
                                                decimal tempDoseOnceDec = Convert.ToDecimal(tempDoseOnce[i]);
                                                objExec.Order.Qty = tempDoseOnceDec / ((FS.HISFC.Models.Pharmacy.Item)objExec.Order.Item).BaseDose;
                                                objExec.Order.DoseOnce = tempDoseOnceDec;
                                                if (objExec.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)//(objExec.Order.Item.GetType().ToString() == "FS.HISFC.Models.Pharmacy.Item")
                                                {
                                                    decimal decAmount = 0;
                                                    FS.HISFC.Models.Pharmacy.Item objPharmacy;
                                                    objPharmacy = (FS.HISFC.Models.Pharmacy.Item)objExec.Order.Item;
                                                    if (objExec.Order.Item.ID == "999")
                                                    {
                                                        if (objExec.Order.Item.Qty == 0)
                                                        {
                                                            objExec.Order.Item.Qty = objExec.Order.DoseOnce;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        decAmount = ComputeAmount(objPharmacy.ID, objPharmacy.DosageForm.ID, tempDoseOnceDec, objPharmacy.BaseDose, order.Patient.PVisit.PatientLocation.Dept.ID);
                                                        if (decAmount >= 0)
                                                        {
                                                            objExec.Order.Item.Qty = decAmount;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (objExec.Order.Item.ID == "999")
                                                {
                                                    if (objExec.Order.Item.Qty == 0)
                                                    {
                                                        objExec.Order.Item.Qty = objExec.Order.DoseOnce;
                                                    }
                                                }
                                                else
                                                {
                                                    if (DecAmount != -1)
                                                        objExec.Order.Qty = DecAmount;
                                                    else
                                                        objExec.Order.Qty = objExec.Order.DoseOnce / ((FS.HISFC.Models.Pharmacy.Item)objExec.Order.Item).BaseDose;
                                                }
                                            }
                                            if (objExec.Order.Item.ID == "999")
                                            {
                                                if (string.IsNullOrEmpty(((FS.HISFC.Models.Pharmacy.Item)objExec.Order.Item).MinUnit))
                                                {
                                                    objExec.Order.DoseUnit = ((FS.HISFC.Models.Pharmacy.Item)objExec.Order.Item).DoseUnit;
                                                }
                                                if (string.IsNullOrEmpty(((FS.HISFC.Models.Pharmacy.Item)objExec.Order.Item).MinUnit))
                                                {
                                                    ((FS.HISFC.Models.Pharmacy.Item)objExec.Order.Item).MinUnit = objExec.Order.DoseUnit;
                                                }
                                            }
                                            objExec.Order.Unit = ((FS.HISFC.Models.Pharmacy.Item)objExec.Order.Item).MinUnit;
                                        }
                                        catch
                                        {
                                            this.Err = "����Ϊ�㡣";
                                            this.WriteErr();
                                        }
                                    }


                                    //����Q1H�� ��ʼʱ��Ϊ����ģ���ʼʱ����β���Ǯ������ִ��
                                    if (objExec.DateUse == order.BeginTime && order.Frequency.ID.ToUpper() == "Q1H")
                                    {
                                    }
                                    else
                                    {
                                        //����ִ�е�
                                        if (this.InsertExecOrder(objExec) == -1)
                                        {
                                            if (this.DBErrCode != 1)
                                                return -1;
                                        }
                                    }
                                    iMOCount++;//{F1C96C96-F829-4ea1-AD07-10DBCC091C16}�ֽ����
                                    bIsHaveDecompose = true;//{F1C96C96-F829-4ea1-AD07-10DBCC091C16}
                                }
                            }
                        }
                        #endregion
                        Count = Count + Cycle;
                    }
                    #endregion
                }

                #region �����´θ���ʱ��

                DateTime dtNex = new DateTime();

                //{97FA5C9D-F454-4aba-9C36-8AF81B7C9CCF}
                //��ҽ�����´ηֽ�ʱ��Days + Cycle
                if (days < Cycle)
                {
                    if (days == 0)
                    {
                        //ֻ�ֽ⵽����,�´ηֽ�ʱ�丳�ɵ���,
                    }
                    else
                    {
                        days = Cycle;
                    }
                }


                if (Cycle > 1)
                {
                    //{F1C96C96-F829-4ea1-AD07-10DBCC091C16}�޸�QOD��20������
                    if (bIsHaveDecompose)
                    {
                        //���Ƶ�εĻ���������1ҽ�����´ηֽ�ʱ��Ӧ�ü��ϣ����ηֽ����*Ƶ�εĻ�����������
                        dtNex = order.NextMOTime.AddDays(iMOCount * Cycle);

                        order.NextMOTime = new DateTime(dtNex.Year, dtNex.Month, dtNex.Day, 0, 0, 0);

                    }
                    else
                    {
                    }
                }
                else
                {
                    dtNex = dtCurDeco.AddDays(days);

                    order.NextMOTime = new DateTime(dtNex.Year, dtNex.Month, dtNex.Day, iHour, iMinute, 0);
                }

                order.CurMOTime = dtCurDeco;

                //���·ֽ�ʱ�䣨���Σ��´Σ�
                if (UpdateDecoTime(order) <= 0)
                {
                    return -1;
                }
                #endregion
            }
            #endregion

            return 0;
        }

        /// <summary>
        /// �����ڷֽ�ҽ��
        /// </summary>
        /// <param name="order"></param>
        /// <param name="dtCurDeco"></param>
        /// <param name="dtEndTime"></param>
        /// <param name="isCharge"></param>
        /// <param name="objExec"></param>
        /// <param name="dtCurTime"></param>
        /// <param name="curUser"></param>
        /// <param name="addDays"></param>
        /// <returns></returns>
        private int DecomposeTime(FS.HISFC.Models.Order.Inpatient.Order order, DateTime dtCurDeco, DateTime dtEndTime, bool isCharge, FS.HISFC.Models.Order.ExecOrder objExec, DateTime dtCurTime, FS.HISFC.Models.Base.Employee curUser, int addDays)
        {
            #region �ֽ�ʱ��
            for (int i = 0; i <= order.Frequency.Times.GetUpperBound(0); i++)
            {
                DateTime dt = new DateTime();
                try
                {
                    dt = FS.FrameWork.Function.NConvert.ToDateTime(order.Frequency.Times[i]);
                }
                catch
                {
                }
                if (dt.GetType().ToString() != "System.DateTime")
                {
                    return -1;
                }
                DateTime dtUseTime = new DateTime(dtCurDeco.AddDays(addDays).Year, dtCurDeco.AddDays(addDays).Month, dtCurDeco.AddDays(addDays).Day, dt.Hour, dt.Minute, dt.Second);

                //��ҩʱ��>=�´�ִ������and��ҩʱ��<�ֽ����ʱ�䣿
                if (dtUseTime >= order.NextMOTime && dtUseTime < dtEndTime)
                {
                    //��ҩʱ���Ƿ����ҽ��ֹͣʱ��?
                    if (dtUseTime > order.EndTime && order.EndTime != DateTime.MinValue)
                    {
                        //ֹͣ����ҽ��
                        order.Status = 3;
                        order.DCOper.OperTime = order.EndTime;
                        if (DcOneOrder(order) < 0) return 0;
                    }
                    else
                    {
                        //----���ֲ�Ƿ�----
                        if (isCharge)
                        {
                            //����ִ�е����ʱ�־
                            objExec.IsCharge = true;
                            objExec.ChargeOper.ID = curUser.ID;
                            objExec.ChargeOper.Dept.ID = curUser.Dept.ID;
                            objExec.ChargeOper.OperTime = dtCurTime;
                        }
                        //����ִ�е�
                        try
                        {
                            objExec.ID = GetNewOrderExecID();
                        }
                        catch
                        {
                        }
                        if (objExec.ID == "-1" || objExec.ID == "") return 0;
                        objExec.IsValid = true;
                        objExec.DateUse = dtUseTime;
                        objExec.DateDeco = dtCurTime;
                        objExec.DrugFlag = 0; //Ĭ��Ϊ����Ҫ����

                        if (objExec.Order.Item.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))//ҩƷ
                        {
                            try
                            {   //����ͨ��������������С��λ���ڼ������
                                objExec.Order.Qty = objExec.Order.DoseOnce / ((FS.HISFC.Models.Pharmacy.Item)objExec.Order.Item).BaseDose;
                                objExec.Order.Unit = ((FS.HISFC.Models.Pharmacy.Item)objExec.Order.Item).MinUnit;
                            }
                            catch
                            {
                                this.Err = FS.FrameWork.Management.Language.Msg("����Ϊ�㡣");
                                this.WriteErr();
                            }
                        }

                        if (this.InsertExecOrder(objExec) == -1)
                        {
                            return -1;
                        }
                    }
                }
            }
            return 0;
            #endregion
        }

        /// <summary>
        /// ��÷ֽ�ȡ����׼
        /// </summary>
        /// <param name="drugCode"></param>
        /// <param name="doseCode"></param>
        /// <param name="doseOnce"></param>
        /// <param name="baseDose"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public decimal ComputeAmount(string drugCode, string doseCode, decimal doseOnce, decimal baseDose, string deptCode)
        {
            #region ��÷ֽ�ȡ����׼
            //��÷ֽ�ȡ����׼
            //Order.Order.ComputeAmount.1
            //���룺0 DrugCode
            //������0 UNIT_STAT
            #endregion

            #region {AD50C155-BE2D-47b8-8AF9-4AF3548A2726}
            //�����Ż�
            string UnitSate = string.Empty;

            if (htDrugedProperty.Contains(drugCode + deptCode))
            {
                UnitSate = htDrugedProperty[drugCode + deptCode].ToString();
            }
            else
            {
                UnitSate = this.GetDrugProperty(drugCode, doseCode, deptCode);

                htDrugedProperty.Add(drugCode + deptCode, UnitSate);
            }
            #endregion

            decimal Amount = 0;
            if (baseDose == 0)
                return Amount;

            //0 ���ɲ�� 1 �ɲ�ֲ�ȡ�� 2 �ɲ����ȡ��
            switch (UnitSate)
            {
                case "0"://�����ԣ�����ȡ��
                    Amount = (decimal)System.Math.Ceiling((decimal)doseOnce / (decimal)baseDose);
                    //Amount = (decimal)System.Math.Ceiling((double)((decimal)doseOnce / (decimal)baseDose));
                    break;
                case "1"://���ԣ���ҩʱ��ȡ��
                    Amount = System.Convert.ToDecimal(doseOnce) / baseDose;
                    break;
                case "2"://���ԣ���ҩʱ��ȡ�� 
                    Amount = System.Convert.ToDecimal(doseOnce) / baseDose;
                    break;
                default://
                    Amount = System.Convert.ToDecimal(doseOnce) / baseDose;
                    break;
            }
            return Amount;
        }

        /// <summary>
        /// ��ȡҩƷ��ҩ����
        /// </summary>
        /// <param name="drugCode">ҩƷ����</param>
        /// <param name="deptCode">���ұ���</param>
        /// <returns>�ɹ�������ҩ���� 0 ���ɲ�� 1 �ɲ�ֲ�ȡ�� 2 �ɲ����ȡ����ʧ�ܷ���NULL</returns>
        public string GetDrugProperty(string drugCode, string doseCode, string deptCode)
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.Sql.GetCommonSql("Pharmacy.Item.GetDrugProperty", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Item.GetDrugProperty�ֶ�!";
                return null;
            }

            //��ʽ��SQL���
            try
            {
                strSQL = string.Format(strSQL, drugCode, doseCode, deptCode);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Pharmacy.Item.GetDrugProperty:" + ex.Message;
                return null;
            }

            //ִ�в�ѯ���
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "�����ҩ������Ϣʱ��ִ��SQL������" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            string drugProperty = "";
            if (this.Reader.Read())
            {
                drugProperty = this.Reader[0].ToString();
            }
            else
            {
                drugProperty = "1";
            }
            this.Reader.Close();
            return drugProperty;
        }


        //{6B70B558-72C9-4DEF-874F-DABD0A9B5198}
         /// <summary>
         /// ��ȡ��ΣҩƷ��ʶ
         /// </summary>
        /// <param name="itemCode">ҩƷ���</param>
         /// <returns></returns>
        public string GetDrugSpecialFlag(string itemCode)
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.Sql.GetCommonSql("Pharmacy.Item.GetDrugSpecialFlag", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Item.GetDrugSpecialFlag�ֶ�!";
                return null;
            }

            //��ʽ��SQL���
            try
            {
                strSQL = string.Format(strSQL, itemCode);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Pharmacy.Item.GetDrugSpecialFlag:" + ex.Message;
                return null;
            }

            //ִ�в�ѯ���
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "���ҩƷ��Σ��ʶ��Ϣʱ��ִ��SQL������" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            string specialflag = "";
            if (this.Reader.Read())
            {
                specialflag = this.Reader[0].ToString();
            }
            else
            {
                specialflag = "";
            }
            this.Reader.Close();
            return specialflag;
        }

        /// <summary>
        /// ���·ֽ�ʱ�䣨���Σ��´Σ�
        /// </summary>
        /// <param name="order">ҽ��id,(���Σ��´�)�ֽ�ʱ��</param>
        /// <returns></returns>
        public int UpdateDecoTime(FS.HISFC.Models.Order.Order order)
        {
            #region ���·ֽ�ʱ��
            //���·ֽ�ʱ��
            //Order.Order.UpdateDecoTime.1
            //���룺0 orderID 1 Date_CurMO 2 Date_NexMO
            //������0 
            #endregion
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.Order.UpdateDecoTime.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, order.ID, order.CurMOTime.ToString(), order.NextMOTime.ToString());
            }
            catch
            {
                this.Err = "����������ԣ�Order.Order.UpdateDecoTime.1";
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// �´ηֽ�ʱ��
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="days">+_����</param>
        /// <returns></returns>
        public int UpdateDecoTime(string inpatientNo, int days)
        {
            #region ���·ֽ�ʱ��
            //���·ֽ�ʱ��
            //Order.Order.UpdateDecoTime.2
            //���룺0 inpatientNo 1 days
            //������0 
            #endregion
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.Order.UpdateDecoTime.2", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                if (days > 0)
                    strSql = string.Format(strSql, inpatientNo, "+" + days.ToString());
                else
                    strSql = string.Format(strSql, inpatientNo, days.ToString());
            }
            catch
            {
                this.Err = "����������ԣ�Order.Order.UpdateDecoTime.2";
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// �´ηֽ�ʱ��
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="nextDecoDateTime">�´ηֽ�ʱ��</param>
        /// <returns></returns>
        public int UpdateDecoTime(string inpatientNo, DateTime nextDecoDateTime)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.Order.UpdateDecoTime.3", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, inpatientNo, nextDecoDateTime.ToString());
            }
            catch
            {
                this.Err = "����������ԣ�Order.Order.UpdateDecoTime.3";
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        //{C685B311-7747-45fa-A62C-E53C24B67CAD}
        /// <summary>
        /// ��ѯ�Ƿ����ͬһ����ͬһ������������Ʒ���Ϣ
        /// </summary>
        /// <param name="unDrugCode">��Ŀ����</param>
        /// <param name="useTime">ִ��ʱ��</param>
        /// <returns>�ɹ���������������ͬ��ִ����ˮ�����飬����NULL</returns>
        public ArrayList SelectIfExistTheSameUN(string unDrugCode, DateTime useTime, string patientID)
        {
            string strSql = "";
            ArrayList sqnList = new ArrayList();
            FS.FrameWork.Models.NeuObject obj = null;
            if (this.Sql.GetCommonSql("Order.ExecOrder.SelectIfExistTheSameUN", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                strSql = string.Format(strSql, unDrugCode, useTime.ToString(), patientID);
                this.ExecQuery(strSql);
                while (this.Reader.Read())
                {
                    obj = new NeuObject();
                    obj.ID = this.Reader[0].ToString(); //ִ�е���ˮ��
                    obj.Memo = this.Reader[1].ToString(); //ҽ����ˮ��
                    sqnList.Add(obj);
                }
            }
            catch
            {
                this.Err = "����������ԣ�Order.ExecOrder.SelectIfExistTheSameUN";
                this.WriteErr();
                return null;
            }
            return sqnList;
        }

        #region {AD50C155-BE2D-47b8-8AF9-4AF3548A2726}

        /// <summary>
        /// ��ҩ���Ա�
        /// </summary>
        private Hashtable htDrugedProperty = new Hashtable();

        /// <summary>
        /// Ϊ���Ż��������ϵ�ҽ���ֽⱣ��ʱ����ҩƷִ�е��õĺ���
        /// </summary>
        /// <param name="execOrder"></param>
        /// <param name="isExec">����ʱ�Ƿ�����ִ��״̬��Ƿ�ѻ��ߵ�ȷ���շѣ��Ƕ���ִ��δ�շ�ִ�е������շ�</param>
        /// <returns></returns>
        public int UpdateForConfirmExecDrug(FS.HISFC.Models.Order.ExecOrder execOrder, bool isExec)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.Order.UpdateForConfirmExecDrug.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, execOrder.ID,//ִ�е�ID
                    execOrder.DrugFlag,//ҩƷ���ͱ��
                    execOrder.ChargeOper.ID,//�����˴���
                    execOrder.ChargeOper.Dept.ID,//���˿��Ҵ���
                    execOrder.ChargeOper.OperTime.ToString(),//����ʱ��
                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.IsCharge).ToString(),//���˱��
                    execOrder.Order.ReciptNO,//������
                    execOrder.Order.SequenceNO,//������ˮ�� 
                    execOrder.ExecOper.ID, //ִ�л�ʿ����
                    execOrder.Order.ExeDept.ID, //ִ�п��Ҵ���
                    execOrder.ExecOper.OperTime.ToString(), //ִ��ʱ��
                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.IsExec).ToString(),
                    FS.FrameWork.Function.NConvert.ToInt32(isExec).ToString() //0 ҽ��ִ�У�1 ҽ��ȷ���շ�
                                         );//ִ�б��
            }
            catch
            {
                this.Err = "����������ԣ�Order.Order.UpdateForConfirmExecDrug.1";
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// Ϊ���Ż��������ϵ�ҽ���ֽⱣ��ʱ����ҩƷִ�е��õĺ���
        /// </summary>
        /// <param name="execOrder">ҩƷִ�е�</param>
        /// <returns></returns>
        [Obsolete("���ϣ�ò��û�ã����Ƽ�����ģʽ", true)]
        public int UpdateForConfirmExecDrugNoExecFlag(FS.HISFC.Models.Order.ExecOrder execOrder)
        {
            string strSql = @"UPDATE met_ipm_execdrug --ҩ��ִ�е�
                               SET druged_flag   = '{1}', --1���跢��/2���з���/3��ɢ����/4����ҩ
                                   charge_usercd = '{2}', --�����˴���
                                   charge_deptcd = '{3}', --���˿��Ҵ���
                                   charge_date   = to_date('{4}', 'yyyy-mm-dd HH24:mi:ss'), --����ʱ��
                                   charge_flag   = '{5}', --���˱��1������/2��
                                   RECIPE_NO     = '{6}', --������
                                   SEQUENCE_NO   = {7}, --������ˮ���
                                   exec_usercd   = '{8}', --ִ�л�ʿ����
                                   exec_deptcd   = '{9}', --ִ�п��Ҵ���
                                   exec_date     = to_date('{10}', 'yyyy-mm-dd HH24:mi:ss'), --ִ��ʱ��
                                   exec_flag     = '{11}' --0��ִ��/1��                   
                             WHERE exec_sqn = '{0}'
                               and valid_flag = fun_get_valid --and exec_flag = '0' ";
            //if (this.Sql.GetCommonSql("Order.Order.UpdateForConfirmExecDrug.1", ref strSql) == -1)
            //{
            //    this.Err = this.Sql.Err;
            //    return -1;
            //}
            try
            {
                strSql = string.Format(strSql, execOrder.ID,//ִ�е�ID
                                         execOrder.DrugFlag,//ҩƷ���ͱ��
                                         execOrder.ChargeOper.ID,//�����˴���
                                         execOrder.ChargeOper.Dept.ID,//���˿��Ҵ���
                                         execOrder.ChargeOper.OperTime.ToString(),//����ʱ��
                                         FS.FrameWork.Function.NConvert.ToInt32(execOrder.IsCharge).ToString(),//���˱��
                                         execOrder.Order.ReciptNO,//������
                                         execOrder.Order.SequenceNO,//������ˮ�� 
                                         execOrder.ExecOper.ID, //ִ�л�ʿ����
                                         execOrder.Order.ExeDept.ID, //ִ�п��Ҵ���
                                         execOrder.ExecOper.OperTime.ToString(), //ִ��ʱ��
                                         FS.FrameWork.Function.NConvert.ToInt32(execOrder.IsExec).ToString());//ִ�б��
            }
            catch
            {
                this.Err = "����������ԣ�Order.Order.UpdateForConfirmExecDrug.1";
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// Ϊ���Ż��������ϵ�ҽ���ֽⱣ��ʱ���·�ҩƷִ�е��õĺ���
        /// </summary>
        /// <param name="execOrder">��ҩƷִ�е�</param>
        /// <param name="isExec">����ʱ�Ƿ�����ִ��״̬��Ƿ�ѻ��ߵ�ȷ���շѣ��Ƕ���ִ��δ�շ�ִ�е������շ�</param>
        /// <returns></returns>
        public int UpdateForConfirmExecUnDrug(FS.HISFC.Models.Order.ExecOrder execOrder, bool isExec)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.Order.UpdateForConfirmExecUnDrug.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, execOrder.ID,//ִ�е�ID
                    execOrder.ChargeOper.ID,//�����˴���
                    execOrder.ChargeOper.Dept.ID,//���˿��Ҵ���
                    execOrder.ChargeOper.OperTime.ToString(),//����ʱ��
                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.IsCharge).ToString(),//���˱��
                    execOrder.Order.ReciptNO,//������
                    execOrder.Order.SequenceNO,//������ˮ�� 
                    execOrder.ExecOper.ID, //ִ�л�ʿ����
                    execOrder.Order.ExeDept.ID, //ִ�п��Ҵ���
                    execOrder.ExecOper.OperTime.ToString(), //ִ��ʱ��
                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.IsExec).ToString(),//ִ�б��
                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.IsConfirm).ToString(),
                    FS.FrameWork.Function.NConvert.ToInt32(isExec).ToString() //0 ҽ��ִ�У�1 ҽ��ȷ���շ�
                    );//����ȷ�ϱ��

            }
            catch
            {
                this.Err = "����������ԣ�Order.Order.UpdateForConfirmExecUnDrug.1";
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        #endregion

        #endregion

        #region "ҽ��ֹͣ����"
        /// <summary>
        /// ֹͣҽ��
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="sysClass"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int DcOrder(string inpatientNo, FS.HISFC.Models.Base.SysClassEnumService sysClass, DateTime dt)
        {
            ArrayList al = new ArrayList();
            DateTime dtBegin = new DateTime(2005, 1, 1);
            al = this.QueryOrder(inpatientNo, dtBegin, dt);
            if (al == null) return -1;
            foreach (FS.HISFC.Models.Order.Inpatient.Order order in al)
            {
                if (order.Status == 1 || order.Status == 0)//��Ч��������,��ִ��ҽ����
                {
                    if (sysClass == null)//ȫ����ֹͣ
                    {
                        order.Status = 3;
                        order.DCOper.OperTime = dt;
                        order.DCOper.ID = this.Operator.ID;
                        order.DCOper.Name = this.Operator.Name;
                        order.DcReason.ID = "0";
                        if (order.OrderType.IsDecompose)
                        {
                            if (this.DcOneOrder(order) == -1)
                            {
                                return -1;
                            }
                        }
                    }
                    else //���ֹͣ //ָֹͣ���ĳ���ҽ��  �磺����ȵȡ�
                    {
                        if (order.Item.SysClass.ID.ToString() == sysClass.ID.ToString() && order.OrderType.IsDecompose)
                        {
                            order.Status = 3;
                            order.DCOper.OperTime = dt;
                            order.DCOper.ID = this.Operator.ID;
                            order.DCOper.Name = this.Operator.Name;
                            order.DcReason.ID = "0";
                            if (this.DcOneOrder(order) == -1)
                            {
                                return -1;
                            }
                        }
                    }
                }
            }
            this.Err = "ҽ��ֹͣ�ɹ���";
            return 0;
        }
        /// <summary>
        /// ֹͣҽ��
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int DcOrder(string inpatientNo, DateTime dt)
        {
            return DcOrder(inpatientNo, null, dt);
        }
        /// <summary>
        /// ֹͣҽ����������ֱ��ֹͣ��Ԥֹͣ��
        /// ֹͣԭ��Order.DcReason
        /// </summary>
        /// <param name="order"></param>
        /// <param name="isAllDc">���ҽ���Ƿ�ȫ��ֹͣ</param>
        /// <param name="ReturnMemo">����ҽ��ԭʼ״̬</param>
        /// <returns></returns>
        public int DcOrder(FS.HISFC.Models.Order.Inpatient.Order order, bool isAllDc, out string ReturnMemo)
        {
            //����ҽ��ִ�����
            ReturnMemo = "";
            if (order.Status == 2)
            {
                ReturnMemo = "����ҽ���Ѿ��Ѿ�ִ�У���ȷ�Ϻ��˷ѣ�";
            }
            //�Ƿ������ҽ��
            ArrayList al = new ArrayList();
            al = this.QueryOrderByCombNO(order.Combo.ID, false);

            ArrayList temp = this.QueryOrderByCombNO(order.Combo.ID, true);
            al.AddRange(temp);
            //�Ƿ������ҽ��
            if (al.Count > 1)
            {
                //�Ƿ�ֹͣ�����Ŀ�еĵ���
                if (isAllDc)
                {
                    FS.HISFC.Models.Order.Inpatient.Order obj;
                    for (int i = 0; i < al.Count; i++)
                    {
                        obj = (FS.HISFC.Models.Order.Inpatient.Order)al[i];
                        //����¼�����洦��
                        if (obj.ID == order.ID)
                            continue;
                        //ִ�е���ҽ��DC
                        obj.DCOper = order.DCOper;
                        obj.DcReason = order.DcReason;
                        if (obj.EndTime == DateTime.MinValue)
                        {
                            obj.EndTime = order.DCOper.OperTime;
                        }
                        obj.Status = 3;
                        if (DcOneOrderDeal(obj) < 0)
                            return -1;
                    }
                }
                else
                {
                    //�Ƿ���ҩ
                    if (order.Combo.IsMainDrug)
                    {
                        //��ҩҽ�����ܵ���DC
                        this.Err = "���ܵ���������ҩҽ����";
                        return -1;
                    }
                }
            }
            //else�������Ŀִ�е���ҽ������
            //ִ�е���ҽ��DC

            if (order.EndTime == DateTime.MinValue)
            {
                order.EndTime = order.DCOper.OperTime;
            }
            if (DcOneOrderDeal(order) < 0)
                return -1;
            return 0;

        }

        /// <summary>
        /// ��ȡ��Ժҽ������������Ժ�������ֵ�������
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public int GetOutOrder(string inpatientNo, ref FS.HISFC.Models.Order.Inpatient.Order order)
        {
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            sql = OrderQuerySelect();
            if (sql == null)
            {
                return -1;
            }

            try
            {
                if (this.Sql.GetCommonSql("Order.Order.QueryOrder.OutOrder", ref sql1) == -1)
                {
                    this.Err = "û���ҵ�[Order.Order.QueryOrder.OutOrder]�ֶ�!";
                    this.ErrCode = "-1";
                    this.WriteErr();
                    return -1;
                }
                sql = sql + " " + string.Format(sql1, inpatientNo);

                ArrayList alOrder = this.MyOrderQuery(sql);
                if (alOrder == null)
                {
                    return -1;
                }
                else if (alOrder.Count <= 0)
                {
                    return 0;
                }
                else
                {
                    order = alOrder[0] as FS.HISFC.Models.Order.Inpatient.Order;
                    return 1;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// ��ȡת��ҽ����������ת�ơ������ֵ�������
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public int GetShiftOutOrder(string inpatientNo, ref FS.HISFC.Models.Order.Inpatient.Order order)
        {
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            sql = OrderQuerySelect();
            if (sql == null)
            {
                return -1;
            }

            try
            {
                if (this.Sql.GetCommonSql("Order.Order.QueryOrder.ShiftOutOrder", ref sql1) == -1)
                {
                    this.Err = "û���ҵ�[Order.Order.QueryOrder.ShiftOutOrder]�ֶ�!";
                    this.ErrCode = "-1";
                    this.WriteErr();
                    return -1;
                }
                sql = sql + " " + string.Format(sql1, inpatientNo);

                ArrayList alOrder = this.MyOrderQuery(sql);
                if (alOrder == null)
                {
                    return -1;
                }
                else if (alOrder.Count <= 0)
                {
                    return 0;
                }
                else
                {
                    order = alOrder[0] as FS.HISFC.Models.Order.Inpatient.Order;
                    return 1;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// ��ȡ�����ڲ���������������
        /// add by yerl
        /// </summary>
        public int GetFeeInfoCount(string inpatientNo)
        {
            string sql=string.Empty;
            if (this.Sql.GetCommonSql("Order.OrderCount", ref sql) < 0)
            {
                return -1;
            }
            sql = string.Format(sql, inpatientNo);
            string result = this.ExecSqlReturnOne(sql);
            int rtn=-1;
            int.TryParse(result, out rtn);
            return rtn;
        }
        /// <summary>
        /// ��ȡת��ҽ��������ת�����͵�ҽ����
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public int GetShiftOutOrderType(string inpatientNo, ref FS.HISFC.Models.Order.Inpatient.Order order)
        {
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            sql = OrderQuerySelect();
            if (sql == null)
            {
                return -1;
            }

            try
            {
                if (this.Sql.GetCommonSql("Order.Order.QueryOrder.GetShiftOutOrderType", ref sql1) == -1)
                {
                    //                    sql1 = @" where inpatient_no='{0}'
                    //                                    and (class_code = 'MRD'
                    //                                    or item_name like '%ת��%')
                    //                                    and decmps_state='0'";

                    sql1 = @" where inpatient_no='{0}'
                                   and (class_code = 'MRD'
                                  or item_name like '%ת��%')
                                    and decmps_state='0'
                                  AND  dept_code=( SELECT t.dept_code FROM  FIN_IPR_INMAININFO t WHERE t.inpatient_no='{0}' ) "

                        ;
                }
                sql = sql + " " + string.Format(sql1, inpatientNo);

                ArrayList alOrder = this.MyOrderQuery(sql);
                if (alOrder == null)
                {
                    return -1;
                }
                else if (alOrder.Count <= 0)
                {
                    return 0;
                }
                else
                {
                    order = alOrder[0] as FS.HISFC.Models.Order.Inpatient.Order;
                    return 1;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// ��黼���Ƿ��п�����ҽ��
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns>-1 ����  0 û��ҽ��  1 �ѿ�����ҽ��</returns>
        public int IsOwnOrders(string inpatientNo)
        {
            string sql = OrderQuerySelect();
            if (sql == null)
            {
                return -1;
            }

            string sqlWhere = "";
            if (this.Sql.GetCommonSql("Order.Order.QueryOrder.IsOwnOrders", ref sqlWhere) == -1)
            {
                sqlWhere = @" where inpatient_no = '{0}'";
            }
            sql = sql + " " + string.Format(sqlWhere, inpatientNo);

            ArrayList alOrder = this.MyOrderQuery(sql);
            if (alOrder == null)
            {
                return -1;
            }
            else if (alOrder.Count == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// ��ʿվ��Ժ�Ǽǣ��Զ�ֹͣȫ������
        /// ֹͣ��Ϊ��ֹͣ�������״̬
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="dcDoct">ֹͣҽ��</param>
        /// <param name="confirmNurse">��˻�ʿ</param>
        /// <param name="dcReasonCode">ֹͣԭ�����</param>
        /// <param name="dcReasonName">ֹͣԭ��</param>
        /// <returns></returns>
        public int AutoDcOrder(string inpatientNo, FS.FrameWork.Models.NeuObject dcDoct, FS.FrameWork.Models.NeuObject confirmNurse, string dcReasonCode, string dcReasonName)
        {
            #region SQL
            //update met_ipm_order
            //set 
            //dc_flag='1', --ֹͣ���
            //date_end=to_date('{1}','yyyy-mm-dd	HH24:mi:ss'),--ҽ������ʱ��
            //dc_date=to_date('{1}','yyyy-mm-dd	HH24:mi:ss'), --ֹͣʱ��
            //dc_code='{2}',--DCԭ�����
            //dc_name='{3}',--DCԭ������
            //dc_doccd='{4}',--DCҽʦ����
            //dc_docnm='{5}',--DCҽʦ����
            //dc_usercd='{6}',--Dc��Ա����
            //dc_usernm='{7}',--Dc��Ա����
            //DC_CONFIRM_FLAG  ='1', --��Ժ�Ǽ�ֹͣ �Զ����
            //DC_CONFIRM_DATE = to_date('{8}','yyyy-mm-dd	HH24:mi:ss'),	--ȷ��ʱ��
            //DC_CONFIRM_OPER='{9}'	 --ȷ����Ա����
            //where inpatient_no='{0}'--סԺ��ˮ��
            //and decmps_state='1' --����
            //and mo_stat in ('1','2') --ֹֻͣ����˺���ִ�еĳ���
            #endregion

            string strSql = "";
            //ֹͣȫ������
            if (this.Sql.GetCommonSql("Order.Order.DcOrder.AutoDcAllLong", ref strSql) == -1)
            {
                this.Err = "�Ҳ���SQL���:Order.Order.DcOrder.AutoDcAllLong";
                return -1;
            }
            try
            {
                strSql = string.Format(strSql,
                                        inpatientNo,        //����סԺ��ˮ��
                                        this.GetDateTimeFromSysDateTime(),//ҽ������ʱ��
                    //this.GetDateTimeFromSysDateTime(),//ֹͣʱ��
                                        dcReasonCode,//DCԭ�����
                                        dcReasonName,//DCԭ������
                                        dcDoct.ID,//DCҽʦ����
                                        dcDoct.Name,//DCҽʦ����
                                        confirmNurse.ID,//Dc��Ա����
                                        confirmNurse.Name,//Dc��Ա����
                                        this.GetDateTimeFromSysDateTime(),//ȷ��ʱ��
                                        confirmNurse.ID//ȷ����Ա����
                                    );
            }
            catch
            {
                this.Err = "����������ԣ�Order.Order.DcOrder.AutoDcAllLong";
                return -1;
            }

            return this.ExecNoQuery(strSql);

            return 1;
        }

        /// <summary>
        /// ���ݻ���סԺ�š�ϵͳ����ֹͣԭ��ֹͣ���ߴ˴����ҽ�����������û�д�����Ҫֹͣҽ��ִ�е�
        /// 0 ����סԺ��ˮ��
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="sysClassCode">ϵͳ���  00ʱֹͣȫ��ҽ����ִ�е�</param>
        /// <param name="dcDate">ֹͣʱ��</param>
        /// <param name="dcReasonCode">ֹͣԭ�����</param>
        /// <param name="dcReasonName">ֹͣԭ������</param>
        /// <returns></returns>
        public int DcOrder(string inpatientNo, string sysClassCode, DateTime dcDate, string dcReasonCode, string dcReasonName)
        {
            /*1��ֹͣҽ����״̬Ϊ2��3
             *2��ִֹͣ�е�Ϊ����ǰֹͣʱ��֮���δ�շѵ���Чִ�е�
             * */
            string strSql = "";
            //ֹͣҽ������
            if (this.Sql.GetCommonSql("Order.Order.DcOrder.All", ref strSql) == -1)
            {
                this.Err = "�Ҳ���SQL���:Order.Order.DcOrder.All";
                return -1;
            }
            try
            {
                strSql = string.Format(strSql,
                                        inpatientNo,        //����סԺ��ˮ��
                                        sysClassCode,       //ϵͳ���
                                        dcDate.ToString(),  //ֹͣ����
                                        dcReasonCode,       //ֹͣԭ�����
                                        dcReasonName,       //ֹͣԭ������
                                        this.Operator.ID,   //ֹͣ��
                                        this.Operator.Name  //ֹͣ������
                                    );
            }
            catch
            {
                this.Err = "����������ԣ�Order.Order.DcOrder.All";
                return -1;
            }
            int parm = this.ExecNoQuery(strSql);
            if (parm == -1) return -1;

            //���sysClassCode��Ч��������"00"������ֻ������ֹͣҽ����������ִ�е����ݣ�����ִֹͣ�е�
            if (sysClassCode == "00")
            {
                //ֹͣҩƷҽ��ִ�е�
                if (this.Sql.GetCommonSql("Order.ExecOrder.DcExecAll.Drug", ref strSql) == -1)
                {
                    this.Err = "�Ҳ���SQL���:Order.ExecOrder.DcExecAll.Drug";
                    return -1;
                }
                try
                {
                    strSql = string.Format(strSql, inpatientNo, dcDate.ToString(), this.Operator.ID, dcReasonName);
                }
                catch
                {
                    this.Err = "����������ԣ�Order.ExecOrder.DcExecAll.Drug";
                    return -1;
                }

                parm = this.ExecNoQuery(strSql);
                if (parm == -1) return -1;

                //ֹͣ��ҩƷҽ��ִ�е�
                if (this.Sql.GetCommonSql("Order.ExecOrder.DcExecAll.Undrug", ref strSql) == -1)
                {
                    this.Err = "�Ҳ���SQL���:Order.ExecOrder.DcExecAll.Undrug";
                    return -1;
                }
                try
                {
                    strSql = string.Format(strSql, inpatientNo, dcDate.ToString(), this.Operator.ID, dcReasonName);
                }
                catch
                {
                    this.Err = "����������ԣ�Order.ExecOrder.DcExecAll.Undrug";
                    return -1;
                }
                parm = this.ExecNoQuery(strSql);
                if (parm == -1)
                {
                    return -1;
                }
            }

            return parm;
        }

        /// <summary>
        /// /// ���ݻ���סԺ�ź�ֹͣԭ��ֹͣ��������ҽ��������ҽ��ִ�е�
        /// </summary>
        /// <param name="inpatientNo">0 ����סԺ��ˮ��</param>
        /// <param name="dcDate">ֹͣʱ��</param>
        /// <param name="dcReasonCode">ֹͣԭ�����</param>
        /// <param name="dcReasonName">ֹͣԭ������</param>
        /// <returns></returns>
        public int DcOrder(string inpatientNo, DateTime dcDate, string dcReasonCode, string dcReasonName)
        {
            return this.DcOrder(inpatientNo, "00", dcDate, dcReasonCode, dcReasonName);
        }

        /// <summary>
        /// ִ�е���ҽ��DC����
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private int DcOneOrderDeal(FS.HISFC.Models.Order.Inpatient.Order order)
        {
            //��ȡϵͳʱ��
            DateTime CurTime = this.GetDateTimeFromSysDateTime();

            //�����Ƿ�Ԥֹͣ����ֻ����ֹͣʱ��֮���ִ�е�
            #region ����ִ�е�
            ////����ֹͣ����ʱҽ�� or ֹͣʱ��С�ڵ��ڵ�ǰʱ�䣩
            //if (order.OrderType.IsDecompose == false || order.EndTime <= CurTime)
            //{
            //    //�����ϱ��
            //    order.Status = 3;
            //    if (this.DcExecImmediate(order, this.Operator) < 0)
            //    {
            //        this.Err = FS.FrameWork.Management.Language.Msg("����ҽ��ִ����Ϣʧ�ܣ�");
            //        return -1;
            //    }
            //}
            ////Ԥֹͣ������ҽ������ֹͣʱ����ڵ��ڵ�ǰʱ�䣩
            //else
            //{
            if (this.DcExecLater(order, this.Operator) < 0)
            {
                this.Err = FS.FrameWork.Management.Language.Msg("����ҽ��ִ����Ϣʧ�ܣ�");
                return -1;
            }
            //}
            #endregion

            #region ֹͣҽ��
            //ִ��ֹͣҽ����
            if (this.DcOneOrder(order) < 0)
            {
                this.Err = FS.FrameWork.Management.Language.Msg("ֹͣҽ��ʧ�ܣ�");
                return -1;
            }
            #endregion
            return 0;
        }

        /// <summary>
        /// ����ϺŲ�ѯҽ��
        /// �������ݿ��SQL���������ĿǰisSubtbl����û����
        /// </summary>
        /// <param name="combno">��Ϻ�</param>
        /// <param name="isSubtbl">Ŀǰ������</param>
        /// <returns></returns>
        public ArrayList QueryOrderByCombNO(string combno, bool isSubtbl)
        {
            #region ��״̬��ѯҽ��
            //����ϺŲ�ѯҽ��
            //Order.Order.QueryOrderByCombno.where.1
            //���룺0 combno 1 IsSub
            //������ArrayList
            #endregion
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            sql = OrderQuerySelect();
            if (sql == null) return null;
            if (this.Sql.GetCommonSql("Order.Order.QueryOrderByCombno.where.1", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.Order.QueryOrderByCombno.where.1�ֶ�!";
                return null;
            }
            sql = sql + " " + string.Format(sql1, combno, FS.FrameWork.Function.NConvert.ToInt32(isSubtbl).ToString());
            return this.MyOrderQuery(sql);
        }

        #endregion

        #region ȡ��ֹͣҽ��

        /// <summary>
        /// ĳ��ҽ���Ƿ���ֹͣ���
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public string GetDCConfirmFlag(string orderID)
        {
            string sql = "";
            try
            {
                if (this.Sql.GetCommonSql("NewOrder.Order.Query.DCConfirmFlag", ref sql) == -1)
                {
                    Err = "����SQL����NewOrder.Order.Query.DCConfirmFlag";
                    return null;
                }
                sql = string.Format(sql, orderID);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                return null;
            }
            return this.ExecSqlReturnOne(sql);
        }

        /// <summary>
        /// ȡ��ֹͣҽ��
        /// </summary>
        /// <param name="order"></param>
        /// <param name="isAllDc">���ҽ���Ƿ�ȫ��ֹͣ</param>
        /// <returns></returns>
        public int CancelDcOrder(FS.HISFC.Models.Order.Inpatient.Order order, bool isAllDc)
        {
            order = this.QueryOneOrder(order.ID);

            if (!(order.Status == 3 || order.EndTime > DateTime.MinValue))
            {
                this.Err = "ҽ������ֹͣ״̬����ˢ��ҽ����";
                return -1;
            }

            //�Ƿ������ҽ��
            ArrayList al = new ArrayList();
            al = this.QueryOrderByCombNO(order.Combo.ID, false);

            //ֹͣ�����ͬ�ĸ���
            ArrayList temp = this.QueryOrderByCombNO(order.Combo.ID, true);
            al.AddRange(temp);

            //�Ƿ�ֹͣ�����Ŀ�еĵ���
            if (isAllDc)
            {
                FS.HISFC.Models.Order.Inpatient.Order obj = null;
                for (int i = 0; i < al.Count; i++)
                {
                    obj = (FS.HISFC.Models.Order.Inpatient.Order)al[i];
                    //����¼�����洦��
                    if (obj.ID == order.ID)
                        continue;

                    //ִ�е���ҽ��DC
                    if (CancelDcOneOrderDeal(obj) < 0)
                    {
                        return -1;
                    }
                }
            }

            //ִ�е���ҽ��DC
            if (CancelDcOneOrderDeal(order) < 0)
            {
                return -1;
            }
            return 0;

        }

        /// <summary>
        /// ȡ��ֹͣ����ҽ��
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private int CancelDcOneOrderDeal(FS.HISFC.Models.Order.Inpatient.Order order)
        {

            order.DcReason = new NeuObject("", "", "");
            order.DCOper.ID = string.Empty;
            order.DCOper.Name = string.Empty;
            order.Status = 2;

            //ȡ��ִֹͣ�е�
            if (this.CancelDcExecImmediate(order, order.DcReason) < 0)
            {
                return -1;
            }
            order.DCOper.OperTime = DateTime.MinValue;
            order.EndTime = DateTime.MinValue;

            //ȡ��ֹͣҽ����
            if (this.CancelDcOneOrder(order) < 0)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ȡ��ֹͣҽ����
        /// Order.Status = 1Ԥֹͣ;Order.Status = 3ֱ��ֹͣ
        /// </summary>
        /// <param name="Order">ҽ����Ϣ</param>
        /// <returns>0 success -1 fail</returns>
        public int CancelDcOneOrder(FS.HISFC.Models.Order.Inpatient.Order Order)
        {
            #region ȡ��ֹͣҽ��
            //ֹͣҽ��(ҽ������Ч״̬)
            //Order.Order.dcOrder.1
            //���룺0 id��1 ֹͣ��id,2ֹͣ��������3ֹͣʱ��,4ҽ��״̬ ,5ֹͣԭ����룬6ֹͣԭ������ 
            //������0 
            #endregion
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.Order.DCOrder.CanCel", ref strSql) == -1)
            {
                this.Err = "ȡ��ֹͣҽ��ʧ�ܣ�" + this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, Order.ID, Order.DCOper.ID, Order.DCOper.Name, Order.EndTime.ToString(), Order.Status.ToString(), Order.DcReason.ID, Order.DcReason.Name);
            }
            catch
            {
                this.Err = "ȡ��ֹͣҽ��ʧ�ܣ�" + "����������ԣ�Order.Order.DCOrder.CanCel";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ȡ��ִֹͣ�е�
        /// </summary>
        /// <param name="dcPerson">ִ�е���Ϣ</param>
        /// <returns>0 success -1 fail</returns>
        public int CancelDcExecImmediate(FS.HISFC.Models.Order.Inpatient.Order Order, FS.FrameWork.Models.NeuObject dcPerson)
        {
            #region ����ִ�е�
            //����ִ�е�(ҽ��ֹͣ��ֱ������)
            //Order.ExecOrder.DcExecImmediate
            //���룺0 id��1 ֹͣ��id,2ֹͣ��������3ֹͣʱ��,4���ϱ�־ 
            //������0 
            #endregion

            string strSql = "";
            string strSqlName = "";

            if (Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                strSqlName = "Order.ExecOrder.DcExecImmediate.Cancel.Drug";
            }
            else
            {
                strSqlName = "Order.ExecOrder.DcExecImmediate.Cancel.unDrug";
            }

            if (this.Sql.GetCommonSql(strSqlName, ref strSql) == -1)
            {
                this.Err = "ȡ��ֹͣҽ��ִ�е�ʧ�ܣ�" + this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, Order.ID, dcPerson.ID, Order.DCOper.OperTime.ToString());
            }
            catch
            {
                this.Err = "ȡ��ֹͣҽ��ִ�е�ʧ�ܣ�" + "�����������" + strSqlName;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        #endregion

        #region ��Һ��Ϣ

        /// <summary>
        /// ��������Һ��Ϣ
        /// </summary>
        /// <param name="isNurse">�Ƿ񰴲������� ��deptCode����Ϊ��������</param>
        /// <param name="deptCode">����</param>
        /// <param name="dtBegin">��ʼִ��ʱ��</param>
        /// <param name="dtEnd">��ִֹ��ʱ��</param>
        /// <param name="isExec">״̬ �Ƿ��ѯ����Һ��</param>
        /// <returns>�ɹ����ش���Һ��Ϣ ʧ�ܷ���null</returns>
        public ArrayList QueryExecOrderForCompound(bool isNurse, string deptCode, DateTime dtBegin, DateTime dtEnd, bool isExec)
        {
            string[] s;
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();

            s = ExecOrderQuerySelect("1");
            sql = s[0];
            if (sql == null)
            {
                return null;
            }

            if (isNurse)
            {
                if (this.Sql.GetCommonSql("Order.QueryOrderExecCompound.NurseCell", ref sql1) == -1)
                {
                    this.Err = "û���ҵ�Order.QueryOrderExecCompound.NurseCell�ֶ�!";
                    return null;
                }
            }
            else
            {
                if (this.Sql.GetCommonSql("Order.QueryOrderExecCompound.DeptCode", ref sql1) == -1)
                {
                    this.Err = "û���ҵ�Order.QueryOrderExecCompound.DeptCode�ֶ�!";
                    return null;
                }
            }

            string state = "0";
            if (isExec)
            {
                state = "1";
            }
            else
            {
                state = "0";
            }

            sql = sql + " " + string.Format(sql1, deptCode, dtBegin.ToString(), dtEnd.ToString(), state);

            return this.myExecOrderQuery(sql);
        }

        /// <summary>
        /// ��Һ��Ϣ����
        /// </summary>
        /// <param name="execOrderID">ִ�е���ˮ��</param>
        /// <param name="compound">��Һ��Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1 �޼�¼���·���0</returns>
        public int UpdateOrderCompound(string execOrderID, FS.HISFC.Models.Order.Compound compound)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.ExecOrder.UpdateOrderCompound", ref strSql) == -1)
            {
                this.Err = "û���ҵ�Order.ExecOrder.UpdateOrderCompound�ֶ�";
                return -1;
            }
            strSql = string.Format(strSql, execOrderID,
                                           FS.FrameWork.Function.NConvert.ToInt32(compound.IsExec).ToString(),
                                           compound.CompoundOper.ID, compound.CompoundOper.Dept.ID,
                                           compound.CompoundOper.OperTime.ToString());

            return this.ExecNoQuery(strSql);
        }

        #endregion

        #region ����
        /// <summary>
        /// ��û���
        /// </summary>
        /// <param name="sysClassID"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Order.EnumMutex QueryMutex(string sysClassID)
        {
            string sql = "";
            FS.HISFC.Models.Order.EnumMutex mutex = FS.HISFC.Models.Order.EnumMutex.None;
            if (this.Sql.GetCommonSql("Order.QueryMutex.1", ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return mutex;
            }
            sql = string.Format(sql, sysClassID);
            string strMutex = "";
            strMutex = this.ExecSqlReturnOne(sql);
            try
            {
                mutex = (FS.HISFC.Models.Order.EnumMutex)FS.FrameWork.Function.NConvert.ToInt32(strMutex);
                return mutex;
            }
            catch
            {
                return mutex;
            }
        }
        #endregion

        #region ˽�к���

        /// <summary>
        /// �����Ŀ����
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        private ArrayList myGetOutHosCure(string strSql)
        {
            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSql) == -1) return null;
            while (this.Reader.Read())
            {
                FS.HISFC.Models.Fee.Inpatient.FeeItemList item = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                try
                {
                    item.Name = this.Reader[0].ToString();//��Ŀ����
                    item.Item.Price = System.Convert.ToDecimal(this.Reader[1].ToString());//�۸�
                    item.Order.Qty = System.Convert.ToDecimal(this.Reader[2].ToString());//����
                    item.Order.Unit = this.Reader[3].ToString();//��λ
                    item.Order.ExeDept.ID = this.Reader[4].ToString();//ִ�п���
                    item.User01 = this.Reader[5].ToString();//ִ�����
                }
                catch (Exception ex)
                {
                    this.Err = "��ü�鵥��Ϣ����" + ex.Message;
                    this.WriteErr();
                    return null;
                }
                al.Add(item);
            }
            this.Reader.Close();
            return al;
        }

        /// <summary>
        /// ���ҽ����Ϣ
        /// </summary>
        /// <param name="sqlOrder"></param>
        /// <param name="Order"></param>
        /// <returns></returns>
        private string GetOrderInfo(string sqlOrder, FS.HISFC.Models.Order.Inpatient.Order Order)
        {
            #region "�ӿ�˵��"
            //0 IDҽ����ˮ��
            //������Ϣ����  
            //			1 סԺ��ˮ��   2סԺ������     3���߿���id      4���߻���id
            //ҽ��������Ϣ
            // ������Ŀ��Ϣ
            //	       5��Ŀ���      6��Ŀ����       7��Ŀ����      8��Ŀ������,    9��Ŀƴ���� 
            //	       10��Ŀ������ 11��Ŀ�������  12ҩƷ���     13ҩƷ��������  14������λ       
            //         15��С��λ     16��װ����,     17���ʹ���     18ҩƷ���  ,   19ҩƷ����
            //         20���ۼ�       21�÷�����      22�÷�����     23�÷�Ӣ����д  24Ƶ�δ���  
            //         25Ƶ������     26ÿ�μ���      27��Ŀ����     28�Ƽ۵�λ      29ʹ������			  
            // ����ҽ������
            //		   30ҽ�������� 31ҽ���������  32ҽ���Ƿ�ֽ�:1����/2��ʱ     33�Ƿ�Ʒ� 
            //		   34ҩ���Ƿ���ҩ 35��ӡִ�е�    36�Ƿ���Ҫȷ��  
            // ����ִ�����
            //		   37����ҽʦId   38����ҽʦname  39��ʼʱ��      40����ʱ��     41��������
            //		   42����ʱ��     43¼����Ա����  44¼����Ա����  45�����ID     46���ʱ��       
            //		   47DCԭ�����   48DCԭ������    49DCҽʦ����    50DCҽʦ����   51Dcʱ��
            //         52ִ����Ա���� 53ִ��ʱ��      54ִ�п��Ҵ���  55ִ�п������� 
            //		   56���ηֽ�ʱ�� 57�´ηֽ�ʱ��
            // ����ҽ������
            //		   58�Ƿ�Ӥ����1��/2��          59�������  	  60������     61��ҩ��� 
            //		   62�Ƿ񸽲�'1'  63�Ƿ��������  64ҽ��״̬      65�ۿ���     66ִ�б��1δִ��/2��ִ��/3DCִ�� 
            //		   67ҽ��˵��                     68�Ӽ����:1��ͨ/2�Ӽ�         69�������
            //         70��鲿λ��ע                 71��ע          72����,          73 ������������,
            //         74 ȡҩҩ������ 
            #endregion
            string strItemType = "";
            if (Order.CurMOTime == DateTime.MinValue)
            {
                Order.CurMOTime = Order.BeginTime;
            }
            if (Order.NextMOTime == DateTime.MinValue)
            {
                Order.NextMOTime = Order.BeginTime;
            }
            //�ж�ҩƷ/��ҩƷ

            if (Order.Item.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))
            {
                FS.HISFC.Models.Pharmacy.Item objPharmacy;
                objPharmacy = (FS.HISFC.Models.Pharmacy.Item)Order.Item;
                strItemType = "1";
                try
                {
                    System.Object[] s = { Order.ID,
                                          Order.Patient.ID,
                                          Order.Patient.PID.PatientNO,
                                          Order.Patient.PVisit.PatientLocation.Dept.ID,
                                          Order.Patient.PVisit.PatientLocation.NurseCell.ID,
										  strItemType,
                                          Order.Item.ID,
                                          Order.Item.Name,
                                          Order.Item.UserCode,
                                          Order.Item.SpellCode,
										  Order.Item.SysClass.ID.ToString(),
                                          Order.Item.SysClass.Name,
                                          objPharmacy.Specs,
                                          objPharmacy.BaseDose,
                                          objPharmacy.DoseUnit,
                                          objPharmacy.MinUnit,
                                          objPharmacy.PackQty,
										  objPharmacy.DosageForm.ID,
                                          objPharmacy.Type.ID,
                                          objPharmacy.Quality.ID,
                                          //objPharmacy.Price,
                                          Order.Item.Price,
										  Order.Usage.ID,
                                          Order.Usage.Name,
                                          Order.Usage.Memo,
                                          Order.Frequency.ID,
                                          Order.Frequency.Name,
										  Order.DoseOnce,
                                          Order.Qty,Order.Unit,
                                          Order.HerbalQty.ToString(),
										  Order.OrderType.ID,
                                          Order.OrderType.Name,
                                          FS.FrameWork.Function.NConvert.ToInt32(Order.OrderType.IsDecompose),
                                          FS.FrameWork.Function.NConvert.ToInt32(Order.OrderType.IsCharge),
										  FS.FrameWork.Function.NConvert.ToInt32(Order.OrderType.IsNeedPharmacy),
                                          FS.FrameWork.Function.NConvert.ToInt32(Order.OrderType.IsPrint),
                                          FS.FrameWork.Function.NConvert.ToInt32(Order.Item.IsNeedConfirm),
										  Order.ReciptDoctor.ID,
                                          Order.ReciptDoctor.Name,
                                          Order.BeginTime,
                                          Order.EndTime,
                                          Order.ReciptDept.ID,
										  Order.MOTime,
                                          Order.Oper.ID,
                                          Order.Oper.Name,
                                          Order.Nurse.ID,
                                          Order.ConfirmTime,
										  Order.DcReason.ID,
                                          Order.DcReason.Name,
                                          Order.DCOper.ID,
                                          Order.DCOper.Name,
                                          Order.DCOper.OperTime,
										  Order.ExecOper.ID,
                                          Order.ExecOper.OperTime,
                                          Order.ExeDept.ID,
                                          Order.ExeDept.Name,
										  Order.CurMOTime,
                                          Order.NextMOTime,
										  FS.FrameWork.Function.NConvert.ToInt32(Order.IsBaby),
                                          Order.BabyNO,
                                          Order.Combo.ID,
                                          FS.FrameWork.Function.NConvert.ToInt32(Order.Combo.IsMainDrug),
										  FS.FrameWork.Function.NConvert.ToInt32(Order.IsSubtbl),
                                          FS.FrameWork.Function.NConvert.ToInt32(Order.IsHaveSubtbl),
                                          Order.Status,
                                          FS.FrameWork.Function.NConvert.ToInt32(Order.IsStock),
                                          Order.ExecStatus,
										  Order.Note,
                                          FS.FrameWork.Function.NConvert.ToInt32(Order.IsEmergency),
                                          Order.SortID,
                                          Order.Memo,
                                          Order.CheckPartRecord,
                                          FS.FrameWork.Function.NConvert.ToInt32(Order.Reorder),
                                          Order.Sample.Name,
                                          Order.StockDept.ID,
                                          //objPharmacy.IsAllergy==true ?"2":"1",
                                          ((Int32)Order.HypoTest).ToString(),
                                          FS.FrameWork.Function.NConvert.ToInt32(Order.IsPermission),
                                          Order.Package.ID,
                                          Order.Package.Name,
                                          Order.ExtendFlag1,
                                          Order.ExtendFlag2,
                                          Order.ReTidyInfo,
                                          Order.Frequency.Time,
                                          Order.ExecDose,
                                          Order.ApplyNo,
                                          Order.DoseOnceDisplay,//{0D81AD2A-4F10-4fe5-9F79-5C6393384318}
                                          Order.DoseUnitDisplay,//{0D81AD2A-4F10-4fe5-9F79-5C6393384318}
                                          Order.FirstUseNum,//{0D81AD2A-4F10-4fe5-9F79-5C6393384318}
                                          Order.Patient.PVisit.PatientLocation.Bed.ID,
                                          Order.PageNo.ToString(),
                                          Order.RowNo.ToString(),
                                          Order.SpeOrderType,
                                          Order.SubCombNO.ToString(), //��� 2011-7-3 houwb
                                          Order.GetFlag,//ҽ����ӡ���
                                          
                                          //{6B70B558-72C9-4DEF-874F-DABD0A9B5198}
                                          Order.Dripspreed,
                                          objPharmacy.GBCode,
                                          Order.Hospital_id,
                                          Order.Hospital_name
                                         };//�¼�����Ƶ��

                    string myErr = "";
                    if (FS.FrameWork.Public.String.CheckObject(out myErr, s) == -1)
                    {
                        this.Err = myErr;
                        this.WriteErr();
                        return null;
                    }
                    sqlOrder = string.Format(sqlOrder, s);
                }
                catch (Exception ex)
                {
                    this.Err = "����ֵʱ�����" + ex.Message;
                    this.WriteErr();
                    return null;
                }
            }
            else if (Order.Item.GetType() == typeof(FS.HISFC.Models.Fee.Item.Undrug))
            {
                FS.HISFC.Models.Fee.Item.Undrug objAssets;
                objAssets = (FS.HISFC.Models.Fee.Item.Undrug)Order.Item;
                strItemType = "2";

                try
                {
                    string[] s = { Order.ID,
                                   Order.Patient.ID,
                                   Order.Patient.PID.PatientNO,
                                   Order.Patient.PVisit.PatientLocation.Dept.ID,
                                   Order.Patient.PVisit.PatientLocation.NurseCell.ID,
								   strItemType,
                                   Order.Item.ID,
                                   Order.Item.Name,
                                   Order.Item.UserCode,
                                   Order.Item.SpellCode,
								   Order.Item.SysClass.ID.ToString(),
                                   Order.Item.SysClass.Name,
                                   objAssets.Specs,
                                   "0",
                                   "",
                                   "",
                                   "0",
                                   "",
                                   "",
                                   "",
                                   objAssets.Price.ToString(),
								   Order.Usage.ID,
                                   Order.Usage.Name,
                                   Order.Usage.Memo,
                                   Order.Frequency.ID,
                                   Order.Frequency.Name,
								   Order.DoseOnce.ToString(),
                                   Order.Qty.ToString(),
                                   Order.Unit,
                                   Order.HerbalQty.ToString(),
								   Order.OrderType.ID,
                                   Order.OrderType.Name,
                                   FS.FrameWork.Function.NConvert.ToInt32(Order.OrderType.IsDecompose).ToString(),
                                   FS.FrameWork.Function.NConvert.ToInt32(Order.OrderType.IsCharge).ToString(),
								   FS.FrameWork.Function.NConvert.ToInt32(Order.OrderType.IsNeedPharmacy).ToString(),
                                   FS.FrameWork.Function.NConvert.ToInt32(Order.OrderType.IsPrint).ToString(),
                                   FS.FrameWork.Function.NConvert.ToInt32(Order.Item.IsNeedConfirm).ToString(),
								   Order.ReciptDoctor.ID,
                                   Order.ReciptDoctor.Name,
                                   Order.BeginTime.ToString(),
                                   Order.EndTime.ToString(),
                                   Order.ReciptDept.ID,
								   Order.MOTime.ToString(),
                                   Order.Oper.ID,
                                   Order.Oper.Name,
                                   Order.Nurse.ID,
                                   Order.ConfirmTime.ToString(),
								   Order.DcReason.ID,
                                   Order.DcReason.Name,
                                   Order.DCOper.ID,
                                   Order.DCOper.Name,
                                   Order.DCOper.OperTime.ToString(),
								   Order.ExecOper.ID,
                                   Order.ExecOper.OperTime.ToString(),
                                   Order.ExeDept.ID,
                                   Order.ExeDept.Name,
								   Order.CurMOTime.ToString(),
                                   Order.NextMOTime.ToString(),
								   FS.FrameWork.Function.NConvert.ToInt32(Order.IsBaby).ToString(),
                                   Order.BabyNO.ToString(),
                                   Order.Combo.ID,
                                   FS.FrameWork.Function.NConvert.ToInt32(Order.Combo.IsMainDrug).ToString(),
								   FS.FrameWork.Function.NConvert.ToInt32(Order.IsSubtbl).ToString(),
                                   FS.FrameWork.Function.NConvert.ToInt32(Order.IsHaveSubtbl).ToString(),
                                   Order.Status.ToString(),
                                   FS.FrameWork.Function.NConvert.ToInt32(Order.IsStock).ToString(),
                                   Order.ExecStatus.ToString(),
								   Order.Note,
                                   FS.FrameWork.Function.NConvert.ToInt32(Order.IsEmergency).ToString(),
                                   Order.SortID.ToString(),
                                   Order.Memo,
                                   Order.CheckPartRecord,
                                   FS.FrameWork.Function.NConvert.ToInt32(Order.Reorder).ToString(),
                                   Order.Sample.Name,
                                   Order.StockDept.ID,
								   "",
                                   FS.FrameWork.Function.NConvert.ToInt32(Order.IsPermission).ToString(),
                                   Order.Package.ID,
                                   Order.Package.Name,
                                   Order.ExtendFlag1,
                                   Order.ExtendFlag2,
                                   Order.ReTidyInfo,
                                   Order.Frequency.Time,
                                   Order.ExecDose,
                                   Order.ApplyNo,
                                   Order.DoseOnceDisplay,//{0D81AD2A-4F10-4fe5-9F79-5C6393384318}
                                   Order.DoseUnitDisplay,//{0D81AD2A-4F10-4fe5-9F79-5C6393384318}
                                   Order.FirstUseNum,//{0D81AD2A-4F10-4fe5-9F79-5C6393384318}
                                   Order.Patient.PVisit.PatientLocation.Bed.ID,
                                   Order.PageNo.ToString(),
                                   Order.RowNo.ToString(),
                                   Order.SpeOrderType,
                                          Order.SubCombNO.ToString(), //��� 2011-7-3 houwb
                                          Order.GetFlag, //ҽ����ӡ���
                                           "",
                                          objAssets.GBCode,
                                          Order.Hospital_id,
                                          Order.Hospital_name
                                 };//�¼�����Ƶ��
                    sqlOrder = string.Format(sqlOrder, s);
                }
                catch (Exception ex)
                {
                    this.Err = "����ֵʱ�����" + ex.Message;
                    this.WriteErr();
                    return null;
                }
            }
            //{8F86BB0D-9BB4-4c63-965D-969F1FD6D6B2} ҽ�����İ����� by gengxl //{BE3C743B-7343-4e12-A1AF-4B2793C8F9BB}
            else if (Order.Item.GetType() == typeof(FS.HISFC.Models.FeeStuff.MaterialItem))
            {
                //{BE3C743B-7343-4e12-A1AF-4B2793C8F9BB}
                FS.HISFC.Models.FeeStuff.MaterialItem objAssets;
                //{BE3C743B-7343-4e12-A1AF-4B2793C8F9BB}
                objAssets = (FS.HISFC.Models.FeeStuff.MaterialItem)Order.Item;
                strItemType = "2";

                try
                {
                    string[] s = { Order.ID,
                                   Order.Patient.ID,
                                   Order.Patient.PID.PatientNO,
                                   Order.Patient.PVisit.PatientLocation.Dept.ID,
                                   Order.Patient.PVisit.PatientLocation.NurseCell.ID,
								   strItemType,
                                   Order.Item.ID,
                                   Order.Item.Name,
                                   Order.Item.UserCode,
                                   Order.Item.SpellCode,
								   Order.Item.SysClass.ID.ToString(),
                                   Order.Item.SysClass.Name,
                                   objAssets.Specs,
                                   "0",
                                   "",
                                   "",
                                   "0",
                                   "",
                                   "",
                                   "",
                                   objAssets.Price.ToString(),
								   Order.Usage.ID,
                                   Order.Usage.Name,
                                   Order.Usage.Memo,
                                   Order.Frequency.ID,
                                   Order.Frequency.Name,
								   Order.DoseOnce.ToString(),
                                   Order.Qty.ToString(),
                                   Order.Unit,
                                   Order.HerbalQty.ToString(),
								   Order.OrderType.ID,
                                   Order.OrderType.Name,
                                   FS.FrameWork.Function.NConvert.ToInt32(Order.OrderType.IsDecompose).ToString(),
                                   FS.FrameWork.Function.NConvert.ToInt32(Order.OrderType.IsCharge).ToString(),
								   FS.FrameWork.Function.NConvert.ToInt32(Order.OrderType.IsNeedPharmacy).ToString(),
                                   FS.FrameWork.Function.NConvert.ToInt32(Order.OrderType.IsPrint).ToString(),
                                   FS.FrameWork.Function.NConvert.ToInt32(Order.Item.IsNeedConfirm).ToString(),
								   Order.ReciptDoctor.ID,
                                   Order.ReciptDoctor.Name,
                                   Order.BeginTime.ToString(),
                                   Order.EndTime.ToString(),
                                   Order.ReciptDept.ID,
								   Order.MOTime.ToString(),
                                   Order.Oper.ID,
                                   Order.Oper.Name,
                                   Order.Nurse.ID,
                                   Order.ConfirmTime.ToString(),
								   Order.DcReason.ID,
                                   Order.DcReason.Name,
                                   Order.DCOper.ID,
                                   Order.DCOper.Name,
                                   Order.DCOper.OperTime.ToString(),
								   Order.ExecOper.ID,
                                   Order.ExecOper.OperTime.ToString(),
                                   Order.ExeDept.ID,
                                   Order.ExeDept.Name,
								   Order.CurMOTime.ToString(),
                                   Order.NextMOTime.ToString(),
								   FS.FrameWork.Function.NConvert.ToInt32(Order.IsBaby).ToString(),
                                   Order.BabyNO.ToString(),
                                   Order.Combo.ID,
                                   FS.FrameWork.Function.NConvert.ToInt32(Order.Combo.IsMainDrug).ToString(),
								   FS.FrameWork.Function.NConvert.ToInt32(Order.IsSubtbl).ToString(),
                                   FS.FrameWork.Function.NConvert.ToInt32(Order.IsHaveSubtbl).ToString(),
                                   Order.Status.ToString(),
                                   FS.FrameWork.Function.NConvert.ToInt32(Order.IsStock).ToString(),
                                   Order.ExecStatus.ToString(),
								   Order.Note,
                                   FS.FrameWork.Function.NConvert.ToInt32(Order.IsEmergency).ToString(),
                                   Order.SortID.ToString(),
                                   Order.Memo,
                                   Order.CheckPartRecord,
                                   FS.FrameWork.Function.NConvert.ToInt32(Order.Reorder).ToString(),
                                   Order.Sample.Name,
                                   Order.StockDept.ID,
								   "",
                                   FS.FrameWork.Function.NConvert.ToInt32(Order.IsPermission).ToString(),
                                   Order.Package.ID,
                                   Order.Package.Name,
                                   Order.ExtendFlag1,
                                   Order.ExtendFlag2,
                                   Order.ReTidyInfo,
                                   Order.Frequency.Time,
                                   Order.ExecDose,
                                   Order.ApplyNo,
                                   Order.DoseOnceDisplay,//{0D81AD2A-4F10-4fe5-9F79-5C6393384318}
                                   Order.DoseUnitDisplay,//{0D81AD2A-4F10-4fe5-9F79-5C6393384318}
                                   Order.FirstUseNum,//{0D81AD2A-4F10-4fe5-9F79-5C6393384318}
                                   Order.Patient.PVisit.PatientLocation.Bed.ID,
                                   Order.PageNo.ToString(),
                                   Order.RowNo.ToString(),
                                   Order.SpeOrderType,
                                          Order.SubCombNO.ToString(), //��� 2011-7-3 houwb
                                          Order.GetFlag, //ҽ����ӡ���
                                          "",
                                          objAssets.GbCode,
                                          Order.Hospital_id,
                                          Order.Hospital_name
                                  };//�¼�����Ƶ��
                    sqlOrder = string.Format(sqlOrder, s);
                }
                catch (Exception ex)
                {
                    this.Err = "����ֵʱ�����" + ex.Message;
                    this.WriteErr();
                    return null;
                }
            }
            else
            {
                this.Err = "��Ŀ���ͳ���";
                return null;
            }
            return sqlOrder;
        }

        /// ��ѯ������Ϣ��select��䣨��where������
        private string OrderQuerySelect()
        {
            #region �ӿ�˵��
            //Order.Order.QueryOrder.Select.1
            //���룺0
            //������sql.select
            #endregion
            string sql = "";
            if (this.Sql.GetCommonSql("Order.Order.QueryOrder.Select.1", ref sql) == -1)
            {
                this.Err = "û���ҵ�Order.Order.QueryOrder.Select.1�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            return sql;
        }

        /// <summary>
        /// ����WhereSQLID��ѯҽ��
        /// </summary>
        /// <param name="whereSQLIndex"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public ArrayList QueryOrderBase(string whereSQLIndex, params object[] args)
        {
            string whereSQL = "";
            if (this.Sql.GetCommonSql(whereSQLIndex, ref whereSQL) == -1)
            {
                this.Err = "û���ҵ�SQL��䣬IDΪ" + whereSQLIndex + "!";
                return null;
            }

            whereSQL = string.Format(whereSQL, args);

            return this.QueryOrderBase(whereSQL);
        }

        /// <summary>
        /// ����Where������ѯҽ��
        /// </summary>
        /// <param name="whereSQL"></param>
        /// <returns></returns>
        public ArrayList QueryOrderBase(string whereSQL)
        {
            string sql = OrderQuerySelect();
            if (sql == null)
            {
                return null;
            }

            sql = sql + "\r\n" + whereSQL;

            return this.MyOrderQuery(sql);
        }

        /// <summary>
        /// ˽�к�������ѯҽ����Ϣ
        /// </summary>
        /// <param name="SQLPatient"></param>
        /// <returns></returns>
        private ArrayList MyOrderQuery(string SQLPatient)
        {
            ArrayList al = new ArrayList();

            if (this.ExecQuery(SQLPatient) == -1)
            {
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Order.Inpatient.Order objOrder = new FS.HISFC.Models.Order.Inpatient.Order();
                    #region "������Ϣ"
                    //������Ϣ����  
                    //			1 סԺ��ˮ��   2סԺ������     3���߿���id      4���߻���id
                    try
                    {
                        objOrder.Patient.ID = this.Reader[1].ToString();
                        objOrder.Patient.PID.PatientNO = this.Reader[2].ToString();
                        objOrder.Patient.PVisit.PatientLocation.Dept.ID = this.Reader[3].ToString();
                        objOrder.InDept.ID = this.Reader[3].ToString();
                        objOrder.Patient.PVisit.PatientLocation.NurseCell.ID = this.Reader[4].ToString();

                    }
                    catch (Exception ex)
                    {
                        this.Err = "��û��߻�����Ϣ����" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    #endregion

                    #region "��Ŀ��Ϣ"
                    //ҽ��������Ϣ
                    // ������Ŀ��Ϣ
                    //	       5��Ŀ���      6��Ŀ����       7��Ŀ����      8��Ŀ������,    9��Ŀƴ���� 
                    //	       10��Ŀ������ 11��Ŀ�������  12ҩƷ���     13ҩƷ��������  14������λ       
                    //         15��С��λ     16��װ����,     17���ʹ���     18ҩƷ���  ,   19ҩƷ����
                    //         20���ۼ�       21�÷�����      22�÷�����     23�÷�Ӣ����д  24Ƶ�δ���  
                    //         25Ƶ������     26ÿ�μ���      27��Ŀ����     28�Ƽ۵�λ      29ʹ������			  
                    // �ж�ҩƷ/��ҩƷ
                    //         25Ƶ������     26ÿ�μ���      27��Ŀ����     28�Ƽ۵�λ      29ʹ������			  
                    // 73 �������� ����
                    if (this.Reader[5].ToString() == "1")//ҩƷ
                    {
                        FS.HISFC.Models.Pharmacy.Item objPharmacy = new FS.HISFC.Models.Pharmacy.Item();
                        try
                        {
                            objPharmacy.ID = this.Reader[6].ToString();
                            objPharmacy.Name = this.Reader[7].ToString();
                            objPharmacy.UserCode = this.Reader[8].ToString();
                            objPharmacy.SpellCode = this.Reader[9].ToString();
                            objPharmacy.SysClass.ID = this.Reader[10].ToString();

                            objPharmacy.Specs = this.Reader[12].ToString();

                            if (!this.Reader.IsDBNull(13))
                                objPharmacy.BaseDose = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[13].ToString());

                            objPharmacy.DoseUnit = this.Reader[14].ToString();
                            objOrder.DoseUnit = this.Reader[14].ToString();
                            objPharmacy.MinUnit = this.Reader[15].ToString();

                            if (!this.Reader.IsDBNull(16))
                                objPharmacy.PackQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[16].ToString());

                            objPharmacy.DosageForm.ID = this.Reader[17].ToString();
                            objPharmacy.Type.ID = this.Reader[18].ToString();
                            objPharmacy.Quality.ID = this.Reader[19].ToString();
                            // �Ƽ۵�λ
                            objPharmacy.PriceUnit = this.Reader[28].ToString();

                            //try
                            //{
                            if (!this.Reader.IsDBNull(20)) objPharmacy.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20].ToString());
                            //}
                            //catch{}					
                            objOrder.Usage.ID = this.Reader[21].ToString();
                            objOrder.Usage.Name = this.Reader[22].ToString();
                            objOrder.Usage.Memo = this.Reader[23].ToString();


                            if (!this.Reader.IsDBNull(87))
                            {
                                objOrder.DoseOnceDisplay = this.Reader[87].ToString();
                            }
                            if (!this.Reader.IsDBNull(88))
                            {
                                objOrder.DoseUnitDisplay = this.Reader[88].ToString();
                            }


                        }
                        catch (Exception ex)
                        {
                            this.Err = "���ҽ����Ŀ��Ϣ����" + ex.Message;
                            this.WriteErr();
                            return null;
                        }
                        objOrder.Item = objPharmacy;
                    }
                    else if (this.Reader[5].ToString() == "2")//��ҩƷ
                    {
                        FS.HISFC.Models.Fee.Item.Undrug objAssets = new FS.HISFC.Models.Fee.Item.Undrug();
                        try
                        {
                            objAssets.ID = this.Reader[6].ToString();
                            objAssets.Name = this.Reader[7].ToString();
                            objAssets.UserCode = this.Reader[8].ToString();
                            objAssets.SpellCode = this.Reader[9].ToString();
                            objAssets.SysClass.ID = this.Reader[10].ToString();
                            //objAssets.SysClass.Name = this.Reader[11].ToString();
                            objAssets.Specs = this.Reader[12].ToString();
                            objOrder.Usage.ID = this.Reader[21].ToString();
                            objOrder.Usage.Name = this.Reader[22].ToString();
                            objOrder.Usage.Memo = this.Reader[23].ToString();
                            //							try
                            //							{
                            if (!this.Reader.IsDBNull(20)) objAssets.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20].ToString());
                            //}
                            //							catch{}	
                            objAssets.PriceUnit = this.Reader[28].ToString();
                            //������������
                            objOrder.Sample.Name = this.Reader[73].ToString();
                        }
                        catch (Exception ex)
                        {
                            this.Err = "���ҽ����Ŀ��Ϣ����" + ex.Message;
                            this.WriteErr();
                            return null;
                        }
                        objOrder.Item = objAssets;
                    }


                    objOrder.Frequency.ID = this.Reader[24].ToString();
                    objOrder.Frequency.Name = this.Reader[25].ToString();
                    //try
                    //{
                    if (!this.Reader.IsDBNull(26)) objOrder.DoseOnce = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[26].ToString());//}
                    //catch{}
                    //try
                    //{
                    if (!this.Reader.IsDBNull(27)) objOrder.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[27].ToString());//}
                    //catch{}
                    objOrder.Unit = this.Reader[28].ToString();
                    //try
                    //{
                    if (!this.Reader.IsDBNull(29)) objOrder.HerbalQty = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[29].ToString());//}
                    //catch{}

                    #endregion

                    #region "ҽ������"
                    // ����ҽ������
                    //		   30ҽ�������� 31ҽ���������  32ҽ���Ƿ�ֽ�:1����/2��ʱ     33�Ƿ�Ʒ� 
                    //		   34ҩ���Ƿ���ҩ 35��ӡִ�е�    36�Ƿ���Ҫȷ��   
                    try
                    {
                        objOrder.ID = this.Reader[0].ToString();
                        objOrder.ExtendFlag1 = this.Reader[78].ToString();//��ʱҽ��ִ��ʱ�䣭���Զ���
                        objOrder.OrderType.ID = this.Reader[30].ToString();
                        objOrder.OrderType.Name = this.Reader[31].ToString();
                        //try
                        //{
                        if (!this.Reader.IsDBNull(32)) objOrder.OrderType.IsDecompose = System.Convert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[32].ToString()));//}
                        //catch{}
                        //try
                        //{
                        if (!this.Reader.IsDBNull(33)) objOrder.OrderType.IsCharge = System.Convert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[33].ToString()));//}
                        //catch{}
                        //try
                        //{
                        if (!this.Reader.IsDBNull(34)) objOrder.OrderType.IsNeedPharmacy = System.Convert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[34].ToString()));//}
                        //catch{}
                        //try
                        //{
                        if (!this.Reader.IsDBNull(35)) objOrder.OrderType.IsPrint = System.Convert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[35].ToString()));//}
                        //catch{}
                        //try
                        //{
                        if (!this.Reader.IsDBNull(36)) objOrder.Item.IsNeedConfirm = System.Convert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[36].ToString()));//}
                        //catch{}
                        objOrder.Dripspreed = this.Reader[96].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.Err = "���ҽ��������Ϣ����" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    #endregion

                    #region "ִ�����"
                    // ����ִ�����
                    //		   37����ҽʦId   38����ҽʦname  39��ʼʱ��      40����ʱ��     41��������
                    //		   42����ʱ��     43¼����Ա����  44¼����Ա����  45�����ID     46���ʱ��       
                    //		   47DCԭ�����   48DCԭ������    49DCҽʦ����    50DCҽʦ����   51Dcʱ��
                    //         52ִ����Ա���� 53ִ��ʱ��      54ִ�п��Ҵ���  55ִ�п������� 
                    //		   56���ηֽ�ʱ�� 57�´ηֽ�ʱ��
                    try
                    {
                        objOrder.ReciptDoctor.ID = this.Reader[37].ToString();
                        objOrder.ReciptDoctor.Name = this.Reader[38].ToString();
                        //try{
                        if (!this.Reader.IsDBNull(39)) objOrder.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[39].ToString());
                        //}
                        //catch{}
                        //try{
                        if (!this.Reader.IsDBNull(40)) objOrder.EndTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[40].ToString());
                        //}
                        //catch{}
                        objOrder.ReciptDept.ID = this.Reader[41].ToString();//InDept.ID
                        //try{
                        if (!this.Reader.IsDBNull(42)) objOrder.MOTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[42].ToString());
                        //}
                        //catch{}
                        objOrder.Oper.ID = this.Reader[43].ToString();
                        objOrder.Oper.Name = this.Reader[44].ToString();
                        objOrder.Nurse.ID = this.Reader[45].ToString();
                        //try{
                        if (!this.Reader.IsDBNull(46)) objOrder.ConfirmTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[46].ToString());
                        //}
                        //catch{}
                        objOrder.DcReason.ID = this.Reader[47].ToString();
                        objOrder.DcReason.Name = this.Reader[48].ToString();
                        objOrder.DCOper.ID = this.Reader[49].ToString();
                        objOrder.DCOper.Name = this.Reader[50].ToString();
                        //try{
                        if (!this.Reader.IsDBNull(51))
                            objOrder.DCOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[51].ToString());
                        //}
                        //catch{}
                        objOrder.ExecOper.ID = this.Reader[52].ToString();
                        //try{
                        if (!this.Reader.IsDBNull(53)) objOrder.ExecOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[53].ToString());

                        objOrder.ExeDept.ID = this.Reader[54].ToString();
                        objOrder.ExeDept.Name = this.Reader[55].ToString();

                        objOrder.ExecOper.Dept.ID = this.Reader[54].ToString();
                        objOrder.ExecOper.Dept.Name = this.Reader[55].ToString();

                        if (!this.Reader.IsDBNull(56)) objOrder.CurMOTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[56].ToString());

                        if (!this.Reader.IsDBNull(57)) objOrder.NextMOTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[57].ToString());

                    }
                    catch (Exception ex)
                    {
                        this.Err = "���ҽ��ִ�������Ϣ����" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    #endregion

                    #region "ҽ������"
                    // ����ҽ������
                    //		   58�Ƿ�Ӥ����1��/2��          59�������  	  60������     61��ҩ��� 
                    //		   62�Ƿ񸽲�'1'  63�Ƿ��������  64ҽ��״̬      65�ۿ���     66ִ�б��1δִ��/2��ִ��/3DCִ�� 
                    //		   67ҽ��˵��                     68�Ӽ����:1��ͨ/2�Ӽ�         69�������
                    //         70 ��ע       ,       71��鲿λ��ע    ,72 �������,74 ȡҩҩ������ 81 �Ƿ�Ƥ�� 
                    //         84 ���뵥��
                    try
                    {

                        if (!this.Reader.IsDBNull(58)) objOrder.IsBaby = FS.FrameWork.Function.NConvert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[58].ToString()));

                        if (!this.Reader.IsDBNull(59)) objOrder.BabyNO = this.Reader[59].ToString();

                        objOrder.Combo.ID = this.Reader[60].ToString();

                        if (!this.Reader.IsDBNull(61)) objOrder.Combo.IsMainDrug = FS.FrameWork.Function.NConvert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[61].ToString()));

                        if (!this.Reader.IsDBNull(62)) objOrder.IsSubtbl = FS.FrameWork.Function.NConvert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[62].ToString()));

                        if (!this.Reader.IsDBNull(63)) objOrder.IsHaveSubtbl = FS.FrameWork.Function.NConvert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[63].ToString()));

                        if (!this.Reader.IsDBNull(64)) objOrder.Status = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[64].ToString());

                        if (!this.Reader.IsDBNull(65)) objOrder.IsStock = FS.FrameWork.Function.NConvert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[65].ToString()));


                        if (!this.Reader.IsDBNull(66)) objOrder.ExecStatus = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[66].ToString());

                        objOrder.Note = this.Reader[67].ToString();

                        if (!this.Reader.IsDBNull(68)) objOrder.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[68].ToString()));

                        if (!this.Reader.IsDBNull(69)) objOrder.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[69]);

                        objOrder.Memo = this.Reader[70].ToString();
                        objOrder.CheckPartRecord = this.Reader[71].ToString();
                        objOrder.Reorder = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[72].ToString());
                        objOrder.StockDept.ID = this.Reader[74].ToString();//ȡҩҩ������
                        try
                        {
                            if (!this.Reader.IsDBNull(75)) objOrder.IsPermission = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[75]);//������ҩ֪��
                        }
                        catch { }
                        objOrder.Package.ID = this.Reader[76].ToString();
                        objOrder.Package.Name = this.Reader[77].ToString();
                        objOrder.ExtendFlag2 = this.Reader[79].ToString();
                        objOrder.ReTidyInfo = this.Reader[80].ToString();
                        try
                        {
                            if (!this.Reader.IsDBNull(81))
                            {
                                objOrder.HypoTest = (FS.HISFC.Models.Order.EnumHypoTest)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[81].ToString());
                            }
                        }
                        catch
                        {
                            objOrder.HypoTest = 0;
                        }

                        objOrder.Frequency.Time = this.Reader[82].ToString(); //ִ��ʱ��
                        objOrder.ExecDose = this.Reader[83].ToString();//����Ƶ�μ���
                        if (!this.Reader.IsDBNull(84)) objOrder.ApplyNo = this.Reader[84].ToString(); //���뵥��


                        if (!this.Reader.IsDBNull(85))
                        {
                            objOrder.DCNurse.ID = this.Reader[85].ToString();
                        }
                        if (!this.Reader.IsDBNull(86))
                        {
                            objOrder.DCNurse.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[86].ToString());
                        }


                        if (!this.Reader.IsDBNull(89))
                        {
                            objOrder.FirstUseNum = this.Reader[89].ToString();
                        }

                        if (objOrder.FirstUseNum.Length <= 0)
                            objOrder.FirstUseNum = "0";

                        if (!this.Reader.IsDBNull(90))
                        {
                            objOrder.Patient.PVisit.PatientLocation.Bed.ID = this.Reader[90].ToString();
                        }
                        if (!this.Reader.IsDBNull(91))
                        {
                            objOrder.PageNo = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[91].ToString());
                        }
                        if (!this.Reader.IsDBNull(92))
                        {
                            objOrder.RowNo = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[92].ToString());
                        }

                        if (this.Reader[5].ToString() == "1")
                        {
                            if (objOrder.DoseOnceDisplay.Length <= 0)
                                objOrder.DoseOnceDisplay = objOrder.DoseOnce.ToString();

                            if (objOrder.DoseUnitDisplay.Length <= 0)
                                objOrder.DoseUnitDisplay = (objOrder.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit.ToString();
                        }
                        // {C903923D-C0F7-4665-83C4-6CB8CC529155}
                        if (this.Reader.FieldCount >= 94)
                        {
                            objOrder.GetFlag = this.Reader[93].ToString();
                        }

                        // {C903923D-C0F7-4665-83C4-6CB8CC529155}
                        if (this.Reader.FieldCount >= 95)
                        {
                            objOrder.SpeOrderType = this.Reader[94].ToString();
                        }


                        //��� 2011-7-3 houwb
                        if (this.Reader.FieldCount >= 96)
                        {
                            objOrder.SubCombNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[95]);
                        }


                        //CountryCode 2011-7-3 houwb
                        if (this.Reader.FieldCount >= 98)
                        {
                            objOrder.CountryCode = this.Reader[97].ToString();
                        }


                    }
                    catch (Exception ex)
                    {
                        this.Err = "���ҽ��������Ϣ����" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    #endregion

                    al.Add(objOrder);
                }
            }
            catch (Exception ex)
            {
                this.Err = "���ҽ����Ϣ����" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            this.Reader.Close();
            return al;
        }



        /// <summary>
        /// �����������Ĵ�ӡ
        /// </summary>
        /// <returns></returns>
        private ArrayList MyOrderQueryOperate(string SQLPatient)
        {
            ArrayList al = new ArrayList();

            if (this.ExecQuery(SQLPatient) == -1)
            {
                return null;
            }
            try
            {
                int i = 1;
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Order.Inpatient.Order objOrder = new FS.HISFC.Models.Order.Inpatient.Order();
                    #region "������Ϣ"
                    //������Ϣ����  
                    //			1 סԺ��ˮ��   2סԺ������     3���߿���id      4���߻���id
                    try
                    {
                        objOrder.Patient.ID = this.Reader[1].ToString();
                        objOrder.Patient.PID.PatientNO = this.Reader[2].ToString();
                        objOrder.Patient.PVisit.PatientLocation.Dept.ID = this.Reader[3].ToString();
                        objOrder.InDept.ID = this.Reader[3].ToString();
                        objOrder.Patient.PVisit.PatientLocation.NurseCell.ID = this.Reader[4].ToString();

                    }
                    catch (Exception ex)
                    {
                        this.Err = "��û��߻�����Ϣ����" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    #endregion

                    #region "��Ŀ��Ϣ"
                    //ҽ��������Ϣ
                    // ������Ŀ��Ϣ
                    //	       5��Ŀ���      6��Ŀ����       7��Ŀ����      8��Ŀ������,    9��Ŀƴ���� 
                    //	       10��Ŀ������ 11��Ŀ�������  12ҩƷ���     13ҩƷ��������  14������λ       
                    //         15��С��λ     16��װ����,     17���ʹ���     18ҩƷ���  ,   19ҩƷ����
                    //         20���ۼ�       21�÷�����      22�÷�����     23�÷�Ӣ����д  24Ƶ�δ���  
                    //         25Ƶ������     26ÿ�μ���      27��Ŀ����     28�Ƽ۵�λ      29ʹ������			  
                    // �ж�ҩƷ/��ҩƷ
                    //         25Ƶ������     26ÿ�μ���      27��Ŀ����     28�Ƽ۵�λ      29ʹ������			  
                    // 73 �������� ����
                    if (this.Reader[5].ToString() == "1")//ҩƷ
                    {
                        FS.HISFC.Models.Pharmacy.Item objPharmacy = new FS.HISFC.Models.Pharmacy.Item();
                        try
                        {
                            objPharmacy.ID = this.Reader[6].ToString();
                            objPharmacy.Name = this.Reader[7].ToString();
                            objPharmacy.UserCode = this.Reader[8].ToString();
                            objPharmacy.SpellCode = this.Reader[9].ToString();
                            objPharmacy.SysClass.ID = this.Reader[10].ToString();

                            objPharmacy.Specs = this.Reader[12].ToString();

                            if (!this.Reader.IsDBNull(13))
                                objPharmacy.BaseDose = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[13].ToString());

                            objPharmacy.DoseUnit = this.Reader[14].ToString();
                            objOrder.DoseUnit = this.Reader[14].ToString();
                            objPharmacy.MinUnit = this.Reader[15].ToString();

                            if (!this.Reader.IsDBNull(16))
                                objPharmacy.PackQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[16].ToString());

                            objPharmacy.DosageForm.ID = this.Reader[17].ToString();
                            objPharmacy.Type.ID = this.Reader[18].ToString();
                            objPharmacy.Quality.ID = this.Reader[19].ToString();
                            // �Ƽ۵�λ
                            objPharmacy.PriceUnit = this.Reader[28].ToString();

                            //try
                            //{
                            if (!this.Reader.IsDBNull(20)) objPharmacy.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20].ToString());
                            //}
                            //catch{}					
                            objOrder.Usage.ID = this.Reader[21].ToString();
                            objOrder.Usage.Name = this.Reader[22].ToString();
                            objOrder.Usage.Memo = this.Reader[23].ToString();


                            if (!this.Reader.IsDBNull(87))
                            {
                                objOrder.DoseOnceDisplay = this.Reader[87].ToString();
                            }
                            if (!this.Reader.IsDBNull(88))
                            {
                                objOrder.DoseUnitDisplay = this.Reader[88].ToString();
                            }


                        }
                        catch (Exception ex)
                        {
                            this.Err = "���ҽ����Ŀ��Ϣ����" + ex.Message;
                            this.WriteErr();
                            return null;
                        }
                        objOrder.Item = objPharmacy;
                    }
                    else if (this.Reader[5].ToString() == "2")//��ҩƷ
                    {
                        FS.HISFC.Models.Fee.Item.Undrug objAssets = new FS.HISFC.Models.Fee.Item.Undrug();
                        try
                        {
                            objAssets.ID = this.Reader[6].ToString();
                            objAssets.Name = this.Reader[7].ToString();
                            objAssets.UserCode = this.Reader[8].ToString();
                            objAssets.SpellCode = this.Reader[9].ToString();
                            objAssets.SysClass.ID = this.Reader[10].ToString();
                            //objAssets.SysClass.Name = this.Reader[11].ToString();
                            objAssets.Specs = this.Reader[12].ToString();
                            objOrder.Usage.ID = this.Reader[21].ToString();
                            objOrder.Usage.Name = this.Reader[22].ToString();
                            objOrder.Usage.Memo = this.Reader[23].ToString();
                            //							try
                            //							{
                            if (!this.Reader.IsDBNull(20)) objAssets.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20].ToString());
                            //}
                            //							catch{}	
                            objAssets.PriceUnit = this.Reader[28].ToString();
                            //������������
                            objOrder.Sample.Name = this.Reader[73].ToString();
                        }
                        catch (Exception ex)
                        {
                            this.Err = "���ҽ����Ŀ��Ϣ����" + ex.Message;
                            this.WriteErr();
                            return null;
                        }
                        objOrder.Item = objAssets;
                    }


                    objOrder.Frequency.ID = this.Reader[24].ToString();
                    objOrder.Frequency.Name = this.Reader[25].ToString();
                    //try
                    //{
                    if (!this.Reader.IsDBNull(26)) objOrder.DoseOnce = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[26].ToString());//}
                    //catch{}
                    //try
                    //{
                    if (!this.Reader.IsDBNull(27)) objOrder.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[27].ToString());//}
                    //catch{}
                    objOrder.Unit = this.Reader[28].ToString();
                    //try
                    //{
                    if (!this.Reader.IsDBNull(29)) objOrder.HerbalQty = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[29].ToString());//}
                    //catch{}

                    #endregion

                    #region "ҽ������"
                    // ����ҽ������
                    //		   30ҽ�������� 31ҽ���������  32ҽ���Ƿ�ֽ�:1����/2��ʱ     33�Ƿ�Ʒ� 
                    //		   34ҩ���Ƿ���ҩ 35��ӡִ�е�    36�Ƿ���Ҫȷ��   
                    try
                    {
                        objOrder.ID = this.Reader[0].ToString();
                        objOrder.ExtendFlag1 = this.Reader[78].ToString();//��ʱҽ��ִ��ʱ�䣭���Զ���
                        objOrder.OrderType.ID = this.Reader[30].ToString();
                        objOrder.OrderType.Name = this.Reader[31].ToString();
                        //try
                        //{
                        if (!this.Reader.IsDBNull(32)) objOrder.OrderType.IsDecompose = System.Convert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[32].ToString()));//}
                        //catch{}
                        //try
                        //{
                        if (!this.Reader.IsDBNull(33)) objOrder.OrderType.IsCharge = System.Convert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[33].ToString()));//}
                        //catch{}
                        //try
                        //{
                        if (!this.Reader.IsDBNull(34)) objOrder.OrderType.IsNeedPharmacy = System.Convert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[34].ToString()));//}
                        //catch{}
                        //try
                        //{
                        if (!this.Reader.IsDBNull(35)) objOrder.OrderType.IsPrint = System.Convert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[35].ToString()));//}
                        //catch{}
                        //try
                        //{
                        if (!this.Reader.IsDBNull(36)) objOrder.Item.IsNeedConfirm = System.Convert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[36].ToString()));//}
                        //catch{}
                        objOrder.Dripspreed = this.Reader[96].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.Err = "���ҽ��������Ϣ����" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    #endregion

                    #region "ִ�����"
                    // ����ִ�����
                    //		   37����ҽʦId   38����ҽʦname  39��ʼʱ��      40����ʱ��     41��������
                    //		   42����ʱ��     43¼����Ա����  44¼����Ա����  45�����ID     46���ʱ��       
                    //		   47DCԭ�����   48DCԭ������    49DCҽʦ����    50DCҽʦ����   51Dcʱ��
                    //         52ִ����Ա���� 53ִ��ʱ��      54ִ�п��Ҵ���  55ִ�п������� 
                    //		   56���ηֽ�ʱ�� 57�´ηֽ�ʱ��
                    try
                    {
                        objOrder.ReciptDoctor.ID = this.Reader[37].ToString();
                        objOrder.ReciptDoctor.Name = this.Reader[38].ToString();
                        //try{
                        if (!this.Reader.IsDBNull(39)) objOrder.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[39].ToString());
                        //}
                        //catch{}
                        //try{
                        if (!this.Reader.IsDBNull(40)) objOrder.EndTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[40].ToString());
                        //}
                        //catch{}
                        objOrder.ReciptDept.ID = this.Reader[41].ToString();//InDept.ID
                        //try{
                        if (!this.Reader.IsDBNull(42)) objOrder.MOTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[42].ToString());
                        //}
                        //catch{}
                        objOrder.Oper.ID = this.Reader[43].ToString();
                        objOrder.Oper.Name = this.Reader[44].ToString();
                        objOrder.Nurse.ID = this.Reader[45].ToString();
                        //try{
                        if (!this.Reader.IsDBNull(46)) objOrder.ConfirmTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[46].ToString());
                        //}
                        //catch{}
                        objOrder.DcReason.ID = this.Reader[47].ToString();
                        objOrder.DcReason.Name = this.Reader[48].ToString();
                        objOrder.DCOper.ID = this.Reader[49].ToString();
                        objOrder.DCOper.Name = this.Reader[50].ToString();
                        //try{
                        if (!this.Reader.IsDBNull(51))
                            objOrder.DCOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[51].ToString());
                        //}
                        //catch{}
                        objOrder.ExecOper.ID = this.Reader[52].ToString();
                        //try{
                        if (!this.Reader.IsDBNull(53)) objOrder.ExecOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[53].ToString());

                        objOrder.ExeDept.ID = this.Reader[54].ToString();
                        objOrder.ExeDept.Name = this.Reader[55].ToString();

                        objOrder.ExecOper.Dept.ID = this.Reader[54].ToString();
                        objOrder.ExecOper.Dept.Name = this.Reader[55].ToString();

                        if (!this.Reader.IsDBNull(56)) objOrder.CurMOTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[56].ToString());

                        if (!this.Reader.IsDBNull(57)) objOrder.NextMOTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[57].ToString());

                    }
                    catch (Exception ex)
                    {
                        this.Err = "���ҽ��ִ�������Ϣ����" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    #endregion

                    #region "ҽ������"
                    // ����ҽ������
                    //		   58�Ƿ�Ӥ����1��/2��          59�������  	  60������     61��ҩ��� 
                    //		   62�Ƿ񸽲�'1'  63�Ƿ��������  64ҽ��״̬      65�ۿ���     66ִ�б��1δִ��/2��ִ��/3DCִ�� 
                    //		   67ҽ��˵��                     68�Ӽ����:1��ͨ/2�Ӽ�         69�������
                    //         70 ��ע       ,       71��鲿λ��ע    ,72 �������,74 ȡҩҩ������ 81 �Ƿ�Ƥ�� 
                    //         84 ���뵥��
                    try
                    {

                        if (!this.Reader.IsDBNull(58)) objOrder.IsBaby = FS.FrameWork.Function.NConvert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[58].ToString()));

                        if (!this.Reader.IsDBNull(59)) objOrder.BabyNO = this.Reader[59].ToString();

                        objOrder.Combo.ID = this.Reader[60].ToString();

                        if (!this.Reader.IsDBNull(61)) objOrder.Combo.IsMainDrug = FS.FrameWork.Function.NConvert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[61].ToString()));

                        if (!this.Reader.IsDBNull(62)) objOrder.IsSubtbl = FS.FrameWork.Function.NConvert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[62].ToString()));

                        if (!this.Reader.IsDBNull(63)) objOrder.IsHaveSubtbl = FS.FrameWork.Function.NConvert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[63].ToString()));

                        if (!this.Reader.IsDBNull(64)) objOrder.Status = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[64].ToString());

                        if (!this.Reader.IsDBNull(65)) objOrder.IsStock = FS.FrameWork.Function.NConvert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[65].ToString()));


                        if (!this.Reader.IsDBNull(66)) objOrder.ExecStatus = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[66].ToString());

                        objOrder.Note = this.Reader[67].ToString();

                        if (!this.Reader.IsDBNull(68)) objOrder.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[68].ToString()));

                        if (!this.Reader.IsDBNull(69)) objOrder.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[69]);

                        objOrder.Memo = this.Reader[70].ToString();
                        objOrder.CheckPartRecord = this.Reader[71].ToString();
                        objOrder.Reorder = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[72].ToString());
                        objOrder.StockDept.ID = this.Reader[74].ToString();//ȡҩҩ������
                        try
                        {
                            if (!this.Reader.IsDBNull(75)) objOrder.IsPermission = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[75]);//������ҩ֪��
                        }
                        catch { }
                        objOrder.Package.ID = this.Reader[76].ToString();
                        objOrder.Package.Name = this.Reader[77].ToString();
                        objOrder.ExtendFlag2 = this.Reader[79].ToString();
                        objOrder.ReTidyInfo = this.Reader[80].ToString();
                        try
                        {
                            if (!this.Reader.IsDBNull(81))
                            {
                                objOrder.HypoTest = (FS.HISFC.Models.Order.EnumHypoTest)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[81].ToString());
                            }
                        }
                        catch
                        {
                            objOrder.HypoTest = 0;
                        }

                        objOrder.Frequency.Time = this.Reader[82].ToString(); //ִ��ʱ��
                        objOrder.ExecDose = this.Reader[83].ToString();//����Ƶ�μ���
                        if (!this.Reader.IsDBNull(84)) objOrder.ApplyNo = this.Reader[84].ToString(); //���뵥��


                        if (!this.Reader.IsDBNull(85))
                        {
                            objOrder.DCNurse.ID = this.Reader[85].ToString();
                        }
                        if (!this.Reader.IsDBNull(86))
                        {
                            objOrder.DCNurse.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[86].ToString());
                        }


                        if (!this.Reader.IsDBNull(89))
                        {
                            objOrder.FirstUseNum = this.Reader[89].ToString();
                        }

                        if (objOrder.FirstUseNum.Length <= 0)
                            objOrder.FirstUseNum = "0";

                        if (!this.Reader.IsDBNull(90))
                        {
                            objOrder.Patient.PVisit.PatientLocation.Bed.ID = this.Reader[90].ToString();
                        }
                        if (!this.Reader.IsDBNull(91))
                        {
                            int PageNo = i / 24;
                            //objOrder.PageNo = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[91].ToString());

                            objOrder.PageNo = PageNo;
                        }
                        if (!this.Reader.IsDBNull(92))
                        {
                            int RowNo= i % 24;
                            objOrder.RowNo = RowNo;
                            //objOrder.RowNo = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[92].ToString());
                        }

                        if (this.Reader[5].ToString() == "1")
                        {
                            if (objOrder.DoseOnceDisplay.Length <= 0)
                                objOrder.DoseOnceDisplay = objOrder.DoseOnce.ToString();

                            if (objOrder.DoseUnitDisplay.Length <= 0)
                                objOrder.DoseUnitDisplay = (objOrder.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit.ToString();
                        }
                        // {C903923D-C0F7-4665-83C4-6CB8CC529155}
                        //if (this.Reader.FieldCount >= 94)
                        //{
                        //    objOrder.GetFlag = this.Reader[93].ToString();
                        //}

                        objOrder.GetFlag = "0";

                        // {C903923D-C0F7-4665-83C4-6CB8CC529155}
                        if (this.Reader.FieldCount >= 95)
                        {
                            objOrder.SpeOrderType = this.Reader[94].ToString();
                        }


                        //��� 2011-7-3 houwb
                        if (this.Reader.FieldCount >= 96)
                        {
                            objOrder.SubCombNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[95]);
                        }


                        //CountryCode 2011-7-3 houwb
                        if (this.Reader.FieldCount >= 98)
                        {
                            objOrder.CountryCode = this.Reader[97].ToString();
                        }


                    }
                    catch (Exception ex)
                    {
                        this.Err = "���ҽ��������Ϣ����" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    #endregion
                    i++;
                    al.Add(objOrder);
                }
            }
            catch (Exception ex)
            {
                this.Err = "���ҽ����Ϣ����" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            this.Reader.Close();
            return al;
        
        
        }

        /// <summary>
        /// �ж�ҽ����Ŀ���1ҩƷ/2��ҩƷ
        /// </summary>
        /// <param name="Order"></param>
        /// <returns></returns>
        private string JudgeItemType(FS.HISFC.Models.Order.Order Order)
        {
            string strItem = "";

            if (Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                strItem = "1";
            }
            else
            {
                strItem = "2";
            }
            return strItem;
        }


        //��Ӳ�ѯ��Ϣ
        private void addExecOrder(ArrayList al, string sqlOrder)
        {
            ArrayList al1 = null;
            try
            {
                al1 = this.myExecOrderQuery(sqlOrder);
                //al1 = myExecOrderQueryByDataSet(sqlOrder);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
            }
            if (al1 == null) return;

            al.AddRange(al1);
            //			foreach(Object.Order.ExecOrder ExecOrder in al1)
            //			{
            //				al.Add(ExecOrder);
            //			}
        }

        /// <summary>
        /// ��ѯ������Ϣ��select��䣨��where������
        /// </summary>
        /// <param name="Type">"" ҩƷ��ҩƷ���飬1 ҩƷ�� 2 ��ҩƷ</param>
        /// <returns></returns>
        private string[] ExecOrderQuerySelect(string Type)
        {
            #region �ӿ�˵��
            //Order.ExecOrder.QueryOrder.Select.1
            //���룺0
            //������sql.select
            #endregion
            string[] s = null;
            string sql = "";
            if (Type == "")
            {
                s = new string[2];
            }
            else
            {
                s = new string[1];
            }
            if (Type == "1" || Type == "")
            {
                if (this.Sql.GetCommonSql("Order.ExecOrder.QueryOrder.Select.1", ref sql) == -1)
                {
                    this.Err = "û���ҵ�Order.ExecOrder.QueryOrder.Select.1�ֶ�!";
                    this.ErrCode = "-1";
                    this.WriteErr();
                    return null;
                }
                s[0] = sql;
            }
            else if (Type == "2" || Type == "")
            {
                if (this.Sql.GetCommonSql("Order.ExecOrder.QueryOrder.Select.2", ref sql) == -1)
                {
                    this.Err = "û���ҵ�Order.ExecOrder.QueryOrder.Select.2�ֶ�!";
                    this.ErrCode = "-1";
                    this.WriteErr();
                    return null;
                }
                if (Type == "")
                {
                    s[1] = sql;
                }
                else
                {
                    s[0] = sql;
                }
            }
            return s;
        }

        private ArrayList myExecOrderQueryByDataSet(string sql)
        {
            DataSet ds = new DataSet();
            if (this.ExecQuery(sql, ref ds) == -1)
            {
                return null;
            }
            if (ds == null)
            {
                return null;
            }
            if (ds.Tables[0] == null)
            {
                return null;
            }

            ArrayList al = new ArrayList();

            FS.HISFC.Models.Order.ExecOrder objOrder;

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                objOrder = new FS.HISFC.Models.Order.ExecOrder();

                objOrder.Order.Patient.ID = row[0].ToString();
                objOrder.Order.Patient.PID.PatientNO = row[1].ToString();
                objOrder.Order.Patient.PVisit.PatientLocation.Dept.ID = row[3].ToString();
                objOrder.Order.Patient.PVisit.PatientLocation.NurseCell.ID = row[4].ToString();
                objOrder.Order.NurseStation.ID = row[4].ToString();
                objOrder.Order.InDept.ID = row[3].ToString();

                if (row[5].ToString() == "1")//ҩƷ
                {
                    FS.HISFC.Models.Pharmacy.Item objPharmacy = new FS.HISFC.Models.Pharmacy.Item();

                    #region ҩƷ
                    objPharmacy.ID = row[6].ToString();
                    objPharmacy.Name = row[7].ToString();
                    objPharmacy.UserCode = row[8].ToString();
                    objPharmacy.SpellCode = row[9].ToString();
                    objPharmacy.SysClass.ID = row[10].ToString();
                    //objPharmacy.SysClass.Name = row[11].ToString();
                    objPharmacy.Specs = row[12].ToString();
                    //try
                    //{
                    objPharmacy.BaseDose = FS.FrameWork.Function.NConvert.ToDecimal(row[13]);
                    //}
                    //catch{}
                    objPharmacy.DoseUnit = row[14].ToString();
                    objPharmacy.MinUnit = row[15].ToString();
                    //try
                    //{
                    objPharmacy.PackQty = FS.FrameWork.Function.NConvert.ToDecimal(row[16]);
                    //}
                    //catch{}
                    objPharmacy.DosageForm.ID = row[17].ToString();
                    objPharmacy.Type.ID = row[18].ToString();
                    objPharmacy.Quality.ID = row[19].ToString();
                    //try
                    //{
                    objPharmacy.Price = FS.FrameWork.Function.NConvert.ToDecimal(row[20]);
                    //}
                    //catch{}	
                    objOrder.Order.Item = objPharmacy;


                    objOrder.Order.Usage.ID = row[21].ToString();
                    objOrder.Order.Usage.Name = row[22].ToString();
                    objOrder.Order.Usage.Memo = row[23].ToString();
                    objOrder.Order.Frequency.ID = row[24].ToString();
                    objOrder.Order.Frequency.Name = row[25].ToString();
                    //try
                    //{
                    objOrder.Order.DoseOnce = FS.FrameWork.Function.NConvert.ToDecimal(row[26]);
                    //}
                    //catch{}
                    objOrder.Order.DoseUnit = objPharmacy.DoseUnit;
                    //try
                    //{
                    objOrder.Order.Qty = FS.FrameWork.Function.NConvert.ToDecimal(row[27]);
                    //}
                    //catch{}
                    objOrder.Order.Unit = row[28].ToString();
                    //try
                    //{
                    objOrder.Order.HerbalQty = FS.FrameWork.Function.NConvert.ToInt32(row[29]);

                    objOrder.ID = row[0].ToString();
                    objOrder.Order.ID = row[30].ToString();
                    objOrder.Order.OrderType.ID = row[31].ToString();
                    objOrder.Order.OrderType.IsDecompose = FS.FrameWork.Function.NConvert.ToBoolean(row[32]);
                    objOrder.Order.OrderType.IsCharge = FS.FrameWork.Function.NConvert.ToBoolean(row[33]);
                    objOrder.Order.OrderType.IsNeedPharmacy = FS.FrameWork.Function.NConvert.ToBoolean(row[34]);
                    objOrder.Order.OrderType.IsPrint = FS.FrameWork.Function.NConvert.ToBoolean(row[35]);
                    objOrder.Order.Item.IsNeedConfirm = FS.FrameWork.Function.NConvert.ToBoolean(row[36]);
                    objOrder.Order.ReciptDoctor.ID = row[37].ToString();
                    objOrder.Order.ReciptDoctor.Name = row[38].ToString();
                    objOrder.DateUse = FS.FrameWork.Function.NConvert.ToDateTime(row[39]);
                    objOrder.DCExecOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(row[40]);
                    objOrder.Order.ReciptDept.ID = row[41].ToString();
                    objOrder.Order.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(row[42]);
                    objOrder.DCExecOper.ID = row[43].ToString();
                    objOrder.ChargeOper.ID = row[44].ToString();
                    objOrder.ChargeOper.Dept.ID = row[45].ToString();
                    objOrder.ChargeOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(row[46]);
                    objOrder.Order.StockDept.ID = row[47].ToString();
                    objOrder.Order.ExeDept.ID = row[48].ToString();
                    objOrder.ExecOper.ID = row[49].ToString();

                    if (row[50].ToString() != "")
                        objOrder.Order.ExeDept.ID = row[50].ToString();
                    objOrder.Order.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(row[51]);
                    objOrder.DateDeco = FS.FrameWork.Function.NConvert.ToDateTime(row[52]);
                    objOrder.Order.IsBaby = FS.FrameWork.Function.NConvert.ToBoolean(row[53]);
                    objOrder.Order.BabyNO = row[54].ToString();
                    objOrder.Order.Combo.ID = row[55].ToString();
                    objOrder.Order.Combo.IsMainDrug = FS.FrameWork.Function.NConvert.ToBoolean(row[56]);
                    objOrder.Order.IsHaveSubtbl = FS.FrameWork.Function.NConvert.ToBoolean(row[57]);
                    objOrder.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(row[58]);
                    objOrder.Order.IsStock = FS.FrameWork.Function.NConvert.ToBoolean(row[59]);
                    objOrder.IsExec = FS.FrameWork.Function.NConvert.ToBoolean(row[60]);
                    objOrder.DrugFlag = FS.FrameWork.Function.NConvert.ToInt32(row[61]);
                    objOrder.IsCharge = FS.FrameWork.Function.NConvert.ToBoolean(row[62]);
                    objOrder.Order.Note = row[63].ToString();
                    objOrder.Order.Memo = row[64].ToString();
                    objOrder.Order.ReciptNO = row[65].ToString();
                    objOrder.Order.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(row[66]);
                    //objOrder.Order.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(row[67]);//ҩƷ������ʶ----��ʱ����
                    #endregion
                }
                if (row[5].ToString() == "2")//��ҩƷ
                {
                    FS.HISFC.Models.Fee.Item.Undrug objAssets = new FS.HISFC.Models.Fee.Item.Undrug();
                    #region ��ҩƷ

                    // ������Ŀ��Ϣ
                    //	       5��Ŀ���      6��Ŀ����       7��Ŀ����      8��Ŀ������,    9��Ŀƴ���� 
                    //	       10��Ŀ������ 11��Ŀ�������  12���         13���ۼ�        14�÷�����   
                    //         15�÷�����     16�÷�Ӣ����д  17Ƶ�δ���     18Ƶ������      19ÿ������
                    //         20��Ŀ����     21�Ƽ۵�λ      22ʹ�ô���	
                    objAssets.ID = row[6].ToString();
                    objAssets.Name = row[7].ToString();
                    objAssets.UserCode = row[8].ToString();
                    objAssets.SpellCode = row[9].ToString();
                    objAssets.SysClass.ID = row[10].ToString();
                    //objAssets.SysClass.Name = row[11].ToString();
                    objAssets.Specs = row[12].ToString();
                    objAssets.Price = FS.FrameWork.Function.NConvert.ToDecimal(row[13].ToString());
                    objAssets.PriceUnit = row[21].ToString();
                    objOrder.Order.Item = objAssets;
                    objOrder.Order.Usage.ID = row[14].ToString();
                    objOrder.Order.Usage.Name = row[15].ToString();
                    objOrder.Order.Usage.Memo = row[16].ToString();
                    objOrder.Order.Frequency.ID = row[17].ToString();
                    objOrder.Order.Frequency.Name = row[18].ToString();
                    objOrder.Order.DoseOnce = FS.FrameWork.Function.NConvert.ToDecimal(row[19]);
                    objOrder.Order.Qty = FS.FrameWork.Function.NConvert.ToDecimal(row[20]);
                    objOrder.Order.Unit = row[21].ToString();
                    objOrder.Order.DoseUnit = objOrder.Order.Unit;
                    objOrder.Order.HerbalQty = FS.FrameWork.Function.NConvert.ToInt32(row[22]);

                    objOrder.ID = row[0].ToString();
                    objOrder.Order.OrderType.ID = row[23].ToString();
                    objOrder.Order.ID = row[24].ToString();
                    objOrder.Order.OrderType.IsDecompose = FS.FrameWork.Function.NConvert.ToBoolean(row[25]);
                    objOrder.Order.OrderType.IsCharge = FS.FrameWork.Function.NConvert.ToBoolean(row[26]);
                    objOrder.Order.OrderType.IsNeedPharmacy = FS.FrameWork.Function.NConvert.ToBoolean(row[27]);
                    objOrder.Order.OrderType.IsPrint = FS.FrameWork.Function.NConvert.ToBoolean(row[28]);
                    objOrder.Order.Item.IsNeedConfirm = FS.FrameWork.Function.NConvert.ToBoolean(row[29]);
                    objOrder.Order.ReciptDoctor.ID = row[30].ToString();
                    objOrder.Order.ReciptDoctor.Name = row[31].ToString();
                    objOrder.DateUse = FS.FrameWork.Function.NConvert.ToDateTime(row[32]);
                    objOrder.DCExecOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(row[33]);
                    objOrder.Order.ReciptDept.ID = row[34].ToString();
                    objOrder.Order.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(row[35]);
                    objOrder.DCExecOper.ID = row[36].ToString();
                    objOrder.ChargeOper.ID = row[37].ToString();
                    objOrder.ChargeOper.Dept.ID = row[38].ToString();
                    objOrder.ChargeOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(row[39]);
                    objOrder.Order.StockDept.ID = row[40].ToString();
                    objOrder.Order.ExeDept.ID = row[41].ToString();
                    objOrder.ExecOper.ID = row[42].ToString();
                    objOrder.Order.ExeDept.ID = row[43].ToString();
                    objOrder.ExecOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(row[44]);
                    objOrder.DateDeco = FS.FrameWork.Function.NConvert.ToDateTime(row[45]);
                    objOrder.Order.ExeDept.Name = row[46].ToString();
                    objOrder.Order.IsBaby = FS.FrameWork.Function.NConvert.ToBoolean(row[47]);
                    objOrder.Order.BabyNO = row[48].ToString();
                    objOrder.Order.Combo.ID = row[49].ToString();
                    objOrder.Order.Combo.IsMainDrug = FS.FrameWork.Function.NConvert.ToBoolean(row[50]);
                    objOrder.Order.IsSubtbl = FS.FrameWork.Function.NConvert.ToBoolean(row[51]);
                    objOrder.Order.IsHaveSubtbl = FS.FrameWork.Function.NConvert.ToBoolean(row[52]);
                    objOrder.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(row[53]);
                    objOrder.IsExec = FS.FrameWork.Function.NConvert.ToBoolean(row[54]);
                    objOrder.IsCharge = FS.FrameWork.Function.NConvert.ToBoolean(row[55]);
                    objOrder.Order.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(row[56]);
                    objOrder.Order.CheckPartRecord = row[57].ToString();

                    objOrder.Order.Note = row[58].ToString();
                    objOrder.Order.Memo = row[59].ToString();
                    objOrder.Order.ReciptNO = row[60].ToString();
                    objOrder.Order.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(row[61]);
                    objOrder.Order.StockDept.ID = row[62].ToString();
                    try
                    {
                        objOrder.Order.Sample.Name = row[63].ToString();			//��������
                        objOrder.Order.Sample.Memo = row[64].ToString();			//���������
                    }
                    catch { }
                    #endregion
                }

                al.Add(objOrder);
            }
            return al;
        }

        /// <summary>
        /// ˽�к�������ѯҽ����Ϣ
        /// </summary>
        /// <param name="SQLPatient"></param>
        /// <returns></returns>
        private ArrayList myExecOrderQuery(string SQLPatient)
        {
            ArrayList al = new ArrayList();

            if (this.ExecQuery(SQLPatient) == -1)
            {
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Order.ExecOrder objOrder = new FS.HISFC.Models.Order.ExecOrder();

                    //0��ִ����ˮ��
                    objOrder.ID = this.Reader[0].ToString();
                    //1��סԺ��ˮ��
                    objOrder.Order.Patient.ID = this.Reader[1].ToString();
                    //2��סԺ��
                    objOrder.Order.Patient.PID.PatientNO = this.Reader[2].ToString();
                    //3��סԺ����
                    objOrder.Order.Patient.PVisit.PatientLocation.Dept.ID = this.Reader[3].ToString();
                    objOrder.Order.InDept.ID = this.Reader[3].ToString();
                    //4��סԺ����
                    objOrder.Order.Patient.PVisit.PatientLocation.NurseCell.ID = this.Reader[4].ToString();
                    objOrder.Order.NurseStation.ID = this.Reader[4].ToString();

                    #region ҩƷ

                    //5����Ŀ����
                    if (this.Reader[5].ToString() == "1")//ҩƷ
                    {
                        FS.HISFC.Models.Pharmacy.Item objPharmacy = new FS.HISFC.Models.Pharmacy.Item();

                        #region "��Ŀ��Ϣ"

                        //6��ҩƷ��Ŀ����
                        objPharmacy.ID = this.Reader[6].ToString();
                        //7��ҩƷ����
                        objPharmacy.Name = this.Reader[7].ToString();
                        //8��ҩƷ�Զ�����
                        objPharmacy.UserCode = this.Reader[8].ToString();
                        //9��ҩƷƴ����
                        objPharmacy.SpellCode = this.Reader[9].ToString();
                        //10��ϵͳ������
                        objPharmacy.SysClass.ID = this.Reader[10].ToString();
                        //11��ϵͳ�������
                        objPharmacy.SysClass.ID = this.Reader[11].ToString();
                        //12��ҩƷ���
                        objPharmacy.Specs = this.Reader[12].ToString();
                        //13����������
                        objPharmacy.BaseDose = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[13]);
                        //14��������λ
                        objPharmacy.DoseUnit = this.Reader[14].ToString();
                        //15����С��λ
                        objPharmacy.MinUnit = this.Reader[15].ToString();
                        //16����װ����
                        objPharmacy.PackQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[16]);
                        //17�����ͱ���
                        objPharmacy.DosageForm.ID = this.Reader[17].ToString();
                        //18��ҩƷ���
                        objPharmacy.Type.ID = this.Reader[18].ToString();
                        //19��ҩƷ����
                        objPharmacy.Quality.ID = this.Reader[19].ToString();
                        //20�����ۼ�
                        objPharmacy.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20]);

                        objOrder.Order.Item = objPharmacy;
                        #endregion

                        //21���÷�����
                        objOrder.Order.Usage.ID = this.Reader[21].ToString();
                        //22���÷�����
                        objOrder.Order.Usage.Name = this.Reader[22].ToString();
                        //23���÷�Ӣ������
                        objOrder.Order.Usage.Memo = this.Reader[23].ToString();
                        //24��Ƶ�α���
                        objOrder.Order.Frequency.ID = this.Reader[24].ToString();
                        //25��Ƶ������
                        objOrder.Order.Frequency.Name = this.Reader[25].ToString();
                        //26��ÿ������
                        objOrder.Order.DoseOnce = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[26]);
                        //ÿ��������λ ò�����Ϊ�յ�...
                        objOrder.Order.DoseUnit = objPharmacy.DoseUnit;
                        //27������
                        objOrder.Order.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[27]);
                        //28��������λ
                        objOrder.Order.Unit = this.Reader[28].ToString();
                        //29������/����
                        objOrder.Order.HerbalQty = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[29]);

                        #region "ҽ������"

                        //30��ҽ����ˮ��
                        objOrder.Order.ID = this.Reader[30].ToString();
                        //31��ҽ��������
                        objOrder.Order.OrderType.ID = this.Reader[31].ToString();
                        //32���Ƿ�ֽ�
                        objOrder.Order.OrderType.IsDecompose = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[32]);
                        //33���Ƿ��շ�ҽ��
                        objOrder.Order.OrderType.IsCharge = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[33]);
                        //34���Ƿ���Ҫ��ҩ
                        objOrder.Order.OrderType.IsNeedPharmacy = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[34]);
                        //35���Ƿ���Ҫ��ӡ
                        objOrder.Order.OrderType.IsPrint = FS.FrameWork.Function.NConvert.ToBoolean(Reader[35]);
                        //36���Ƿ���Ҫȷ��
                        objOrder.Order.Item.IsNeedConfirm = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[36]);
                        #endregion

                        #region "ִ�����"
                        //37������ҽ������
                        objOrder.Order.ReciptDoctor.ID = this.Reader[37].ToString();
                        //38������ҽ������
                        objOrder.Order.ReciptDoctor.Name = this.Reader[38].ToString();
                        //39��ʹ��ʱ��
                        objOrder.DateUse = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[39]);
                        //40������ʱ��
                        objOrder.DCExecOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[40]);
                        //41���������ұ���
                        objOrder.Order.ReciptDept.ID = this.Reader[41].ToString();
                        //42������ʱ��
                        objOrder.Order.MOTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[42]);
                        //43�������˱���
                        objOrder.DCExecOper.ID = this.Reader[43].ToString();
                        //44���շ��˱���
                        objOrder.ChargeOper.ID = this.Reader[44].ToString();
                        //45���շѿ���
                        objOrder.ChargeOper.Dept.ID = this.Reader[45].ToString();
                        //46���շ�ʱ��
                        objOrder.ChargeOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[46]);
                        //47��ȡҩҩ������
                        objOrder.Order.StockDept.ID = this.Reader[47].ToString();
                        //48��ִ�п��ұ���
                        objOrder.Order.ExeDept.ID = this.Reader[48].ToString();
                        //49��ִ����
                        objOrder.ExecOper.ID = this.Reader[49].ToString();
                        //50��ִ�п��ұ���
                        if (this.Reader[50].ToString() != "")
                        {
                            objOrder.Order.ExeDept.ID = this.Reader[50].ToString();
                        }
                        //51��ִ��ʱ��
                        objOrder.ExecOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[51]);
                        //52���ֽ�ʱ��
                        objOrder.DateDeco = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[52]);
                        #endregion

                        #region "ҽ������"

                        try
                        {
                            //53���Ƿ�Ӥ��
                            objOrder.Order.IsBaby = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[53]);

                            //54��Ӥ�����
                            objOrder.Order.BabyNO = this.Reader[54].ToString();
                            //55����Ϻ�
                            objOrder.Order.Combo.ID = this.Reader[55].ToString();
                            //56���Ƿ���ҩ
                            objOrder.Order.Combo.IsMainDrug = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[56]);
                            //57���Ƿ��������
                            objOrder.Order.IsHaveSubtbl = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[57]);
                            //58����Ч���
                            objOrder.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[58]);
                            //59���Ƿ��ҩ�����
                            objOrder.Order.IsStock = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[59]);
                            //60���Ƿ���ִ��
                            objOrder.IsExec = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[60]);
                            //61�����ͱ�� 1���跢��/2���з���/3��ɢ����/4����ҩ
                            objOrder.DrugFlag = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[61]);
                            //62���Ƿ����շ�
                            objOrder.IsCharge = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[62]);
                            //63����ע��Ϣ
                            objOrder.Order.Note = this.Reader[63].ToString();
                            //64��ҽ����ע
                            objOrder.Order.Memo = this.Reader[64].ToString();
                            //65��������
                            objOrder.Order.ReciptNO = this.Reader[65].ToString();
                            //66����������ˮ��
                            objOrder.Order.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[66]);


                            //67����ҩ����...ľ�д洢������
                            //objOrder.dr = this.Reader[67].ToString();

                            if (!Reader.IsDBNull(68))
                            {
                                //68����ҩʱ��
                                objOrder.DrugedTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[68].ToString());
                            }
                            if (this.Reader.FieldCount >= 70)
                            {
                                //69��ҽ����ʼʱ��
                                objOrder.Order.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[69].ToString());
                            }
                            if (this.Reader.FieldCount >= 71)
                            {
                                //70������ҽ����ʶ ����ҽ����
                                objOrder.Order.SpeOrderType = this.Reader[70].ToString();
                            }
                            if (this.Reader.FieldCount >= 72)
                            {
                                //71���Ƿ�Ӽ�
                                objOrder.Order.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[71]);
                            }
                            if (this.Reader.FieldCount >= 73)
                            {
                                //72��Ƥ�Ա��
                                objOrder.Order.HypoTest = (FS.HISFC.Models.Order.EnumHypoTest)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[72].ToString());
                            }
                            if (this.Reader.FieldCount >= 74)
                            {
                                //73�����
                                objOrder.Order.SubCombNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[73]);
                            }
                            if (this.Reader.FieldCount >= 75)
                            {
                                //74��ҽ�������
                                objOrder.Order.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[74]);
                            }
                            if (this.Reader.FieldCount >= 76)
                            {
                                //75������
                                objOrder.Order.Patient.PVisit.PatientLocation.Bed.ID = this.Reader[75].ToString();
                            }
                            if (this.Reader.FieldCount >= 77)
                            {
                                //76��ҽ��״̬
                                objOrder.Order.Status = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[76]);
                            }
                            if (this.Reader.FieldCount >= 78)
                            {
                                //77��ҽ��ֹͣʱ��
                                objOrder.Order.EndTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[77]);
                            }
                        }
                        catch (Exception ex)
                        {
                            this.Err = "���ҽ��������Ϣ����" + ex.Message;
                            this.WriteErr();
                            return null;
                        }
                        #endregion
                    }
                    #endregion

                    #region ��ҩƷ

                    //5����Ŀ����
                    else if (this.Reader[5].ToString() == "2")
                    {
                        FS.HISFC.Models.Fee.Item.Undrug undrugItem = new FS.HISFC.Models.Fee.Item.Undrug();

                        //6����ҩƷ����
                        undrugItem.ID = this.Reader[6].ToString();
                        //7����ҩƷ��Ŀ
                        undrugItem.Name = this.Reader[7].ToString();
                        //8����ҩƷ�Զ�����
                        undrugItem.UserCode = this.Reader[8].ToString();
                        //9����ҩƷƴ����
                        undrugItem.SpellCode = this.Reader[9].ToString();
                        //10��ϵͳ���
                        undrugItem.SysClass.ID = this.Reader[10].ToString();
                        //11��ϵͳ�������
                        undrugItem.SysClass.Name = this.Reader[11].ToString();
                        //12�����
                        undrugItem.Specs = this.Reader[12].ToString();
                        //13���۸�
                        undrugItem.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[13].ToString());
                        //14���÷�����
                        objOrder.Order.Usage.ID = this.Reader[14].ToString();
                        //15���÷�����
                        objOrder.Order.Usage.Name = this.Reader[15].ToString();
                        //16���÷�Ӣ����..�÷���ע
                        objOrder.Order.Usage.Memo = this.Reader[16].ToString();
                        //17��Ƶ�α���
                        objOrder.Order.Frequency.ID = this.Reader[17].ToString();
                        //18��Ƶ������
                        objOrder.Order.Frequency.Name = this.Reader[18].ToString();
                        //19��ÿ������
                        objOrder.Order.DoseOnce = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[19]);
                        //20������
                        objOrder.Order.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20]);
                        undrugItem.Qty = objOrder.Order.Qty;
                        //21����λ
                        undrugItem.PriceUnit = this.Reader[21].ToString();
                        objOrder.Order.Unit = this.Reader[21].ToString();
                        objOrder.Order.DoseUnit = objOrder.Order.Unit;
                        //22������
                        objOrder.Order.HerbalQty = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[22]);

                        objOrder.Order.Item = undrugItem;

                        //23��ҽ�����
                        objOrder.Order.OrderType.ID = this.Reader[23].ToString();
                        //24��ҽ����ˮ��
                        objOrder.Order.ID = this.Reader[24].ToString();
                        //25���Ƿ�ֽ�
                        objOrder.Order.OrderType.IsDecompose = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[25]);
                        //26���Ƿ�Ʒ�ҽ��
                        objOrder.Order.OrderType.IsCharge = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[26]);
                        //27���Ƿ���Ҫ��ҩ...
                        objOrder.Order.OrderType.IsNeedPharmacy = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[27]);
                        //28���Ƿ���Ҫ��ӡ
                        objOrder.Order.OrderType.IsPrint = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[28]);
                        //29���Ƿ���Ҫȷ��
                        objOrder.Order.Item.IsNeedConfirm = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[29]);
                        //30������ҽ������
                        objOrder.Order.ReciptDoctor.ID = this.Reader[30].ToString();
                        //31������ҽ������
                        objOrder.Order.ReciptDoctor.Name = this.Reader[31].ToString();
                        //32��ʹ��ʱ��
                        objOrder.DateUse = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[32]);
                        //33������ʱ��
                        objOrder.DCExecOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[33]);
                        //34���������ұ���
                        objOrder.Order.ReciptDept.ID = this.Reader[34].ToString();
                        //35��ҽ��ʱ��
                        objOrder.Order.MOTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[35]);
                        //36��������
                        objOrder.DCExecOper.ID = this.Reader[36].ToString();
                        //37���շ���
                        objOrder.ChargeOper.ID = this.Reader[37].ToString();
                        //38���շѿ���
                        objOrder.ChargeOper.Dept.ID = this.Reader[38].ToString();
                        //39���շ�ʱ��
                        objOrder.ChargeOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[39]);
                        //40���ۿ����
                        objOrder.Order.StockDept.ID = this.Reader[40].ToString();
                        //41��ִ�п��ұ���
                        objOrder.Order.ExeDept.ID = this.Reader[41].ToString();
                        //42��ִ����
                        objOrder.ExecOper.ID = this.Reader[42].ToString();
                        //43��ִ�п��ұ��� ���ã��ο�41
                        //objOrder.ExeDept.ID = this.Reader[43].ToString();//����ֶξ���û�õ�
                        //44��ִ��ʱ��
                        objOrder.ExecOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[44]);
                        //45���ֽ�ʱ��
                        objOrder.DateDeco = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[45]);
                        //46��ִ�п�������
                        objOrder.Order.ExeDept.Name = this.Reader[46].ToString();

                        //47���Ƿ�Ӥ��
                        objOrder.Order.IsBaby = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[47]);

                        //48��Ӥ�����
                        objOrder.Order.BabyNO = this.Reader[48].ToString();

                        //49����Ϻ�
                        objOrder.Order.Combo.ID = this.Reader[49].ToString();
                        //50���Ƿ�����Ŀ
                        objOrder.Order.Combo.IsMainDrug = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[50]);
                        //51���Ƿ񸽲�
                        objOrder.Order.IsSubtbl = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[51]);
                        //52���Ƿ��������
                        objOrder.Order.IsHaveSubtbl = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[52]);
                        //53���Ƿ���Ч
                        objOrder.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[53]);
                        //54���Ƿ���ִ��
                        objOrder.IsExec = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[54]);
                        //55���Ƿ����շ�
                        objOrder.IsCharge = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[55]);
                        //56���Ƿ�Ӽ�
                        objOrder.Order.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[56]);//��ҩƷ����
                        //57����鲿λ����������
                        objOrder.Order.CheckPartRecord = this.Reader[57].ToString();
                        //58����ע��Ϣ
                        objOrder.Order.Note = this.Reader[58].ToString();
                        //59��ҽ����ע
                        objOrder.Order.Memo = this.Reader[59].ToString();
                        //60��������
                        objOrder.Order.ReciptNO = this.Reader[60].ToString();
                        //61��������ˮ��
                        objOrder.Order.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[61]);
                        //62���ۿ���ұ���
                        objOrder.Order.StockDept.ID = this.Reader[62].ToString();


                        if (this.Reader.FieldCount >= 64)
                        {
                            //63����������
                            objOrder.Order.Sample.Name = this.Reader[63].ToString();			//��������
                        }

                        if (this.Reader.FieldCount >= 65)
                        {
                            //64�����������
                            objOrder.Order.Sample.Memo = this.Reader[64].ToString();			//���������
                        }

                        if (this.Reader.FieldCount >= 66)
                        {
                            //65��ҽ����ʼʱ��
                            objOrder.Order.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[65].ToString());
                        }

                        if (this.Reader.FieldCount >= 67)
                        {
                            //66������ҽ����ʶ ����ҽ����
                            objOrder.Order.SpeOrderType = this.Reader[66].ToString();
                        }
                        if (this.Reader.FieldCount >= 68)
                        {
                            //67���Ƿ�Ӽ�
                            objOrder.Order.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[67]);
                        }
                        if (this.Reader.FieldCount >= 69)
                        {
                            //68��Ƥ�Ա��
                            objOrder.Order.HypoTest = (FS.HISFC.Models.Order.EnumHypoTest)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[68].ToString());
                        }
                        if (this.Reader.FieldCount >= 70)
                        {
                            //69�����
                            objOrder.Order.SubCombNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[69]);
                        }
                        if (this.Reader.FieldCount >= 71)
                        {
                            //70��ҽ�������
                            objOrder.Order.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[70]);
                        }
                        if (this.Reader.FieldCount >= 72)
                        {
                            //71������
                            objOrder.Order.Patient.PVisit.PatientLocation.Bed.ID = this.Reader[71].ToString();
                        }
                        if (this.Reader.FieldCount >= 73)
                        {
                            //72��ҽ��״̬
                            objOrder.Order.Status = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[72]);
                        }
                        if (this.Reader.FieldCount >= 74)
                        {
                            //73��ҽ��ֹͣʱ��
                            objOrder.Order.EndTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[73]);
                        }
                    }
                    #endregion

                    al.Add(objOrder);
                }
            }
            catch (Exception ex)
            {
                this.Err = "���ҽ����Ϣ����" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return al;
        }

        #endregion

        #region ��ҩ����ҽ���ӿ�
        /// <summary>
        /// ִ�м�¼
        /// ����ҽ��ִ����Ϣ
        /// ��ҽ������ʹ��
        /// </summary>
        /// <param name="execOrder">ִ�е���Ϣ</param>
        /// <returns>0 success -1 fail</returns>
        public int UpdateRecordExec(FS.HISFC.Models.Order.ExecOrder execOrder)
        {
            #region ִ�м�¼
            //ִ�м�¼
            //Order.ExecOrder.RecordExec.1
            //���룺0 id��1 ִ����id,2ִ�п��ң�3ִ�п������� 4ִ��ʱ��,5ִ�б�־ 
            //������0 
            #endregion

            string strSql = "", strSqlName = "Order.ExecOrder.RecordExec.";
            string strItemType = "";

            strItemType = JudgeItemType(execOrder.Order);
            if (strItemType == "") return -1;
            strSqlName = strSqlName + strItemType;

            if (this.Sql.GetCommonSql(strSqlName, ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, execOrder.ID, execOrder.Order.Oper.ID, execOrder.Order.ExeDept.ID, execOrder.Order.ExeDept.Name, execOrder.Order.ExecOper.OperTime.ToString(), FS.FrameWork.Function.NConvert.ToInt32(execOrder.IsExec).ToString(), FS.FrameWork.Function.NConvert.ToInt32(execOrder.IsConfirm).ToString()/*ȷ�ϱ��{DA77B01B-63DF-4559-B264-798E54F24ABB}*/);
            }
            catch
            {
                this.Err = "����������ԣ�" + strSqlName;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// �շѼ�¼
        /// ����ִ��ҽ���շ��ˣ��շѱ�ǣ���Ʊ�ŵ�
        /// �Է��ÿ���ʹ��
        /// </summary>
        /// <param name="execOrder">ִ�е���Ϣ</param>
        /// <returns>0 success -1 fail</returns>
        public int UpdateChargeExec(FS.HISFC.Models.Order.ExecOrder execOrder)
        {
            #region �շѼ�¼
            //�շѼ�¼
            //Order.ExecOrder.Charge.
            //���룺0 id��1 �շ���id,2�շѿ���ID��3�շ�ʱ��,5�շѱ�־ 6 ������ 7������ˮ���
            //������0 
            #endregion
            string strSql = "", strSqlName = "Order.ExecOrder.Charge.";
            string strItemType = "";
            strItemType = JudgeItemType(execOrder.Order);
            if (strItemType == "") return -1;
            //�������
            string strSqlNameExt = "Order.ExecOrder.Charge.Ext.";
            strSqlNameExt = strSqlNameExt + strItemType;
            this.Sql.GetCommonSql(strSqlNameExt, ref strSql);
            if (string.IsNullOrEmpty(strSql))
            {
                strSqlName = strSqlName + strItemType;
                if (this.Sql.GetCommonSql(strSqlName, ref strSql) == -1)
                {
                    this.Err = this.Sql.Err;
                    return -1;
                }
            }
            try
            {
                strSql = string.Format(strSql, execOrder.ID,
                    execOrder.ChargeOper.ID, execOrder.ChargeOper.Dept.ID, execOrder.ChargeOper.OperTime.ToString(),
                    FS.FrameWork.Function.NConvert.ToInt32(execOrder.IsCharge).ToString(),
                    execOrder.Order.ReciptNO, execOrder.Order.SequenceNO, FS.FrameWork.Function.NConvert.ToInt32(execOrder.IsValid));
            }
            catch (Exception ex)
            {
                this.Err = "����������ԣ�" + strSqlName + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ��ҩ��¼
        /// ��ҩ������ʹ��,����DrugFlag
        /// </summary>
        /// <param name="execOrder">ִ�е���Ϣ</param>
        /// <returns>0 success -1 fail</returns>
        public int UpdateDrugExec(FS.HISFC.Models.Order.ExecOrder execOrder)
        {
            #region ��ҩ��¼
            //��ҩ��¼
            //Order.ExecOrder.DrugExec.
            //���룺0 id��1 ��ҩ״̬ 
            //������0 
            #endregion
            string strSql = "";
            string strItemType = "";

            strItemType = JudgeItemType(execOrder.Order);
            if (strItemType != "1")
            {
                this.Err = FS.FrameWork.Management.Language.Msg("�봫��ҩƷҽ��!");
                return -1;
            }

            if (this.Sql.GetCommonSql("Order.ExecOrder.DrugExec.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, execOrder.ID, execOrder.DrugFlag.ToString());
            }
            catch (Exception ex)
            {
                this.Err = "����������ԣ�Order.ExecOrder.DrugExec.1" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ����ҽ����ҩ���
        /// ��ҩ���Ľӿ�
        /// ��ҩ������ʹ��
        /// </summary>
        /// <param name="execOrderID">ִ�е�ID</param>
        /// <param name="orderNo">����ID</param>
        /// <param name="userID">����Ա</param>
        /// <param name="deptID">��ҩ����</param>
        /// <returns>-1 ʧ�� 0 �ɹ�</returns>
        public int UpdateOrderDruged(string execOrderID, string orderNo, string userID, string deptID)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.Update.ExecOrder.Druged", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, execOrderID, userID, deptID);
            }
            catch (Exception ex)
            {
                this.Err = "����������ԣ�Order.Update.ExecOrder.Druged" + ex.Message;
                this.WriteErr();
                return -1;
            }
            if (this.ExecNoQuery(strSql) <= 0) return -1;//����ִ�е���ҩ���

            if (orderNo == "")
            {
                FS.HISFC.Models.Order.ExecOrder obj = this.QueryExecOrderByExecOrderID(execOrderID, "1");//���������Ϣ
                if (obj == null)
                {
                    this.Err = "������ҩ��ǳ���" + this.Err;
                    this.WriteErr();
                    return -1;
                }
                return this.UpdateOrderStatus(obj.Order.ID, 3);
            }
            else
            {
                return this.UpdateOrderStatus(orderNo, 3);
            }
        }
        /// <summary>
        /// ����ҽ����ҩ���
        /// ��ҩ������ʹ��
        /// </summary>
        /// <param name="execOrderID">ִ�е�ID</param>
        /// <param name="userID">����Ա</param>
        /// <param name="deptID">��ҩ����</param>
        /// <returns>-1 ʧ�� 0 �ɹ�</returns>
        public int UpdateOrderDruged(string execOrderID, string userID, string deptID)
        {
            return UpdateOrderDruged(execOrderID, "", userID, deptID);
        }


        /// <summary>
        /// ���Ͱ�ҩ֪ͨ\���÷�ҩ��ʽ
        /// 0���跢��/1���з���/2��ɢ����/3����ҩ
        /// ��ҩ������ʹ��
        /// </summary>
        /// <param name="execOrderID"></param>
        /// <param name="drugFlag">0���跢��/1���з���/2��ɢ����/3����ҩ</param>
        /// <returns></returns>
        public int SetDrugFlag(string execOrderID, int drugFlag)
        {
            #region ���·��ͷ�ʽ
            //���·��ͷ�ʽ
            //Order.Order.SetDrugFlag.1
            //���룺0 OrderExecID��1 DrugFlag
            //������0 
            #endregion
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.Order.SetDrugFlag.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, execOrderID, drugFlag.ToString());
            }
            catch
            {
                this.Err = "����������ԣ�Order.Order.SetDrugFlag.1";
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ���·���֪ͨ
        /// ��ҩ������ʹ��
        /// </summary>
        /// <param name="nurse"></param>
        /// <returns></returns>
        public int SendInformation(FS.FrameWork.Models.NeuObject nurse)
        {
            #region ���·���֪ͨ
            //���·���֪ͨ
            //���룺0 nurseid��1 nursename,2������ID 3 ������������3����ʱ��
            //������0 
            #endregion
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.Order.send.insert.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, nurse.ID, nurse.Name, this.Operator.ID, this.Operator.Name, (this.GetDateTimeFromSysDateTime().Date).ToString());
            }
            catch
            {
                this.Err = "����������ԣ�Order.Order.send.insert.1";
                return -1;
            }
            if (this.ExecNoQuery(strSql) >= 0) return 0;

            if (this.Sql.GetCommonSql("Order.Order.send.update.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, nurse.ID, nurse.Name, this.Operator.ID, this.Operator.Name, (this.GetDateTimeFromSysDateTime().Date).ToString());
            }
            catch
            {
                this.Err = "����������ԣ�Order.Order.send.update.1";
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);

        }



        #endregion

        #region "Lis��������"
        /// <summary>
        /// ����lis�����
        /// ��LIS����ʹ��
        /// </summary>
        /// <param name="execOrderID"></param>
        /// <param name="barCode"></param>
        /// <returns></returns>
        public int UpdateExecOrderLisBarCode(string execOrderID, string barCode)
        {
            string strSql = "";
            //Order.ExecOrder.UpdateLisBarCode
            if (this.Sql.GetCommonSql("Order.ExecOrder.UpdateLisBarCode", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, execOrderID, barCode);
            }
            catch
            {
                this.Err = "����������ԣ�Order.ExecOrder.UpdateLisBarCode";
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ����LIS�Ѿ���ӡ
        /// ��LIS����ʹ��
        /// </summary>
        /// <param name="execOrderID"></param>
        /// <returns></returns>
        public int UpdateExecOrderLisPrint(string execOrderID)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Order.ExecOrder.UpdateLisPrinted", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, execOrderID);
            }
            catch
            {
                this.Err = "����������ԣ�UpdateExecOrderLisPrint";
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        #endregion

        #region ҽ����ѯ

        /// <summary>
        /// ����ִ�п��Ҳ�ѯ��Ҫȷ����Ŀ�Ļ��ߵ����ڿ���
        /// </summary>
        /// <param name="deptID">ִ�п���</param>
        /// <returns></returns>
        public ArrayList QueryPatientDeptByConfirmDeptID(string deptID)
        {
            ArrayList alDept = new ArrayList();
            string strSQL = "";

            if (this.Sql.GetCommonSql("Order.ExecOrder.QueryPatientDept.NeedConfirm.1", ref strSQL) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, deptID);
            }
            catch
            {
                this.Err = "����������ԣ�Order.ExecOrder.QueryPatientDept.NeedConfirm.1";
                return null;
            }

            if (this.ExecQuery(strSQL) == -1)
            {
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject obj = new NeuObject();

                    obj.ID = this.Reader[0].ToString();

                    alDept.Add(obj);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��ѯ����" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            this.Reader.Close();

            return alDept;
        }


        /// <summary>
        /// ����ִ�п��Ҳ�ѯ��Ҫȷ����Ŀ�Ļ��ߵ����ڿ���
        /// </summary>
        /// <param name="deptID">ִ�п���</param>
        /// <returns></returns>
        public ArrayList QueryPatientDeptByConfirmDeptID(string deptID, DateTime beginTime, DateTime endTime)
        {
            ArrayList alDept = new ArrayList();
            string strSQL = "";

            if (this.Sql.GetCommonSql("Order.ExecOrder.QueryPatientDept.NeedConfirm.2", ref strSQL) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, deptID, beginTime.ToString("yyyy-MM-dd HH:mm:ss"), endTime.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            catch
            {
                this.Err = "����������ԣ�Order.ExecOrder.QueryPatientDept.NeedConfirm.2";
                return null;
            }

            if (this.ExecQuery(strSQL) == -1)
            {
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject obj = new NeuObject();

                    obj.ID = this.Reader[0].ToString();

                    alDept.Add(obj);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��ѯ����" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            this.Reader.Close();

            return alDept;
        }

        /// <summary>
        /// ����ִ�п��ҡ��������ڿ��Ҳ�ѯ��Ҫȷ����Ŀ�Ļ���
        /// </summary>
        /// <param name="confirmDept">ִ�п���</param>
        /// <param name="patientDept">�������ڿ���</pQueryOrderaram>
        /// <returns></returns>
        public ArrayList QueryPatientByConfirmDeptAndPatDept(string confirmDept, string patientDept)
        {
            ArrayList alPatient = new ArrayList();
            string strSQL = "";

            if (this.Sql.GetCommonSql("Order.ExecOrder.QueryPatient.NeedConfirm.1", ref strSQL) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, confirmDept, patientDept);
            }
            catch
            {
                this.Err = "����������ԣ�Order.ExecOrder.QueryPatient.NeedConfirm.1";
                return null;
            }

            if (this.ExecQuery(strSQL) == -1)
            {
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject obj = new NeuObject();

                    obj.ID = this.Reader[0].ToString();

                    alPatient.Add(obj);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��ѯ����" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            this.Reader.Close();

            return alPatient;
        }

        /// <summary>
        /// ����ִ�п��ҡ��������ڿ��Ҳ�ѯ��Ҫȷ����Ŀ�Ļ���
        /// </summary>
        /// <param name="confirmDept">ִ�п���</param>
        /// <param name="patientDept">�������ڿ���</param>
        /// <returns></returns>
        public ArrayList QueryPatientByConfirmDeptAndPatDept(string confirmDept, string patientDept, DateTime beginTime, DateTime endTime)
        {
            ArrayList alPatient = new ArrayList();
            string strSQL = "";

            if (this.Sql.GetCommonSql("Order.ExecOrder.QueryPatient.NeedConfirm.2", ref strSQL) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, confirmDept, patientDept, beginTime.ToString("yyyy-MM-dd HH:mm:ss"), endTime.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            catch
            {
                this.Err = "����������ԣ�Order.ExecOrder.QueryPatient.NeedConfirm.2";
                return null;
            }

            if (this.ExecQuery(strSQL) == -1)
            {
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject obj = new NeuObject();

                    obj.ID = this.Reader[0].ToString();

                    alPatient.Add(obj);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��ѯ����" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            this.Reader.Close();

            return alPatient;
        }

        #region {5197289A-AB55-410b-81EE-FC7C1B7CB5D7}
        /// <summary>
        /// У�鳤�ڷ�ҩƷҽ��ִ�е���ʿ�Ƿ�ֽⱣ��
        /// </summary>
        /// <param name="execOrderID">ִ�е���ˮ��</param>
        /// <returns></returns>
        public bool CheckLongUndrugIsConfirm(string execOrderID)
        {
            string strSQL = "";

            if (this.Sql.GetCommonSql("Order.ExecOrder.LongUndrug.CheckIsConfirm.1", ref strSQL) == -1)
            {
                this.Err = this.Sql.Err;
                return false;
            }

            try
            {
                strSQL = string.Format(strSQL, execOrderID);
            }
            catch
            {
                this.Err = "����������ԣ�Order.ExecOrder.LongUndrug.CheckIsConfirm.1";
                return false;
            }

            string flag = this.ExecSqlReturnOne(strSQL, "0");

            return FS.FrameWork.Function.NConvert.ToBoolean(flag);
        }
        #endregion

        #endregion

        #region ҽ����ӡ

        /// <summary>
        /// ��ѯҽ������ӡ��ҽ��
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <returns></returns>
        public ArrayList QueryPrnOrder(string inpatientNO)
        {
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            sql = OrderQuerySelect();
            if (sql == null)
            {
                return null;
            }
            if (this.Sql.GetCommonSql("Order.OrderPrn.QueryOrderByPatient", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.OrderPrn.QueryOrderByPatient�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            sql = sql + " " + string.Format(sql1, inpatientNO);
            return this.MyOrderQuery(sql);
        }

        /// <summary>
        /// ��ѯҽ������ӡ�ĵ�ǰҳҽ��
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <param name="decmpsState">ҽ������:1����/0��ʱ</param>
        /// <param name="currentPageNo">��ǰҳ��</param>
        /// <returns></returns>
        public ArrayList QueryPrnOrderByPageNo(string inpatientNO, string orderType, int currentPageNo)
        {
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            sql = OrderQuerySelect();
            if (sql == null)
            {
                return null;
            }
            if (this.Sql.GetCommonSql("Order.OrderPrn.QueryOrderByPatientAndPageNo", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.OrderPrn.QueryOrderByPatientAndPageNo�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            sql = sql + " " + string.Format(sql1, inpatientNO, orderType, currentPageNo);
            return this.MyOrderQuery(sql);
        }

        /// <summary>
        /// ��ѯҽ������ӡ�ĵ�ǰҳҽ��
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <param name="decmpsState">ҽ������:1����/0��ʱ</param>
        /// <returns></returns>
        public ArrayList QueryPrnOrderByOrderType(string inpatientNO, string orderType)
        {
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            sql = OrderQuerySelect();
            if (sql == null)
            {
                return null;
            }
            if (this.Sql.GetCommonSql("Order.OrderPrn.QueryOrderByPatientAndOrderType", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.OrderPrn.QueryOrderByPatientAndOrderType�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            sql = sql + " " + string.Format(sql1, inpatientNO, orderType);
            return this.MyOrderQuery(sql);
        }


        /// <summary>
        /// ��ӡ������¼��
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryPrnOperateOrderByInpatientNO(string inpatientNO)
        {
            string sql = "";
            ArrayList al = new ArrayList();
            if (sql == null)
            {
                return null;
            }
            if (this.Sql.GetCommonSql("Order.OrderPrn.QueryOperateOrderByPatient", ref sql) == -1)
            {
                this.Err = "û���ҵ�Order.OrderPrn.QueryOperateOrderByPatient�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            sql = string.Format(sql, inpatientNO);
            return this.MyOrderQueryOperate(sql);
        
        
        
        }
        /// <summary>
        /// ��ѯCAǩ��ͼƬ��"byte[]"���ʹ��� GDCA�ӿ� {F343D875-59FD-4401-A193-84DF7B506BD0} 2014-12-05 by lixuelong
        /// </summary>
        /// <param name="employeeNO">Ա������</param>
        /// <returns></returns>
        public byte[] QueryEmplSignDataByEmplNo(string employeeNO)
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Ca.OrderPrn.QueryEmplSignDataByEmplNo", ref sql) == -1)
            {
                this.Err = "û���ҵ�Ca.OrderPrn.QueryEmplSignDataByEmplNo�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            sql = string.Format(sql, employeeNO);
            System.Data.DataSet ds = new System.Data.DataSet();
            if (this.ExecQuery(sql, ref ds) == -1) return null;
            if (ds.Tables[0].Rows.Count == 0) return null;
            byte[] byteData = (byte[])ds.Tables[0].Rows[0].ItemArray[0];
            return byteData;
        }

        /// <summary>
        /// ������ȡ��־
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="moOrder"></param>
        /// <param name="newFlag"></param>
        /// <param name="oldFlag"></param>
        /// <returns></returns>
        public int UpdateGetFlag(string inpatientNo, string moOrder, string newFlag, string oldFlag)
        {
            string strSql = "";
            /*
                update met_ipm_order o
                set o.get_flag = '{2}'
                where o.inpatient_no = '{0}'
                and   o.mo_order = '{1}'
                and   o.get_flag = '{3}'
             */
            if (this.Sql.GetCommonSql("Order.Order.UpdateGetFlag", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql:Order.Order.UpdateGetFlag";
                return -1;
            }

            strSql = System.String.Format(strSql, inpatientNo, moOrder, newFlag, oldFlag);

            return this.ExecNoQuery(strSql); ;
        }

        /// <summary>
        /// ����ҳ�����ȡ��־
        /// -----�ϲ� UpdateGetFlag �� UpdatePageNoAndRowNo ����  by huangchw 2012-09-12
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="moOrder"></param>
        /// <param name="pageNo"></param>
        /// <param name="rowNo"></param>
        /// <param name="newFlag"></param>
        /// <param name="oldFlag"></param>
        /// <returns></returns>
        public int UpdatePageRowNoAndGetflag(
            string inpatientNo, string moOrder, string pageNo, string rowNo, string newFlag, string oldFlag)
        {
            string strSql = "";
            /*
                update met_ipm_order o
                set o.pageno   = '{2}',
                    o.rowno    = '{3',
                    o.get_flag = '{4}'
                where o.inpatient_no = '{0}'
                  and o.mo_order = '{1}'
                  and o.get_flag = '{5}'
             */
            if (this.Sql.GetCommonSql("Order.Order.UpdatePageRowNoAndGetflag", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql:Order.Order.UpdatePageRowNoAndGetflag";
                return -1;
            }

            strSql = System.String.Format(strSql, inpatientNo, moOrder, pageNo, rowNo, newFlag, oldFlag);

            return this.ExecNoQuery(strSql); ;
        }

        /// <summary>
        /// ����ҳ����к�
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="moOrder"></param>
        /// <param name="pageNo"></param>
        /// <param name="rowNo"></param>
        /// <returns></returns>
        public int UpdatePageNoAndRowNo(string inpatientNo, string moOrder, string pageNo, string rowNo)
        {
            string strSql = "";
            /*
                update met_ipm_order o
                set o.pageno = '{2}',
                o.rowno = '{3}'
                where o.inpatient_no = '{0}'
                and   o.mo_order = '{1}'
             */
            if (this.Sql.GetCommonSql("Order.Order.UpdatePageNoAndRowNo", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql:Order.Order.UpdatePageNoAndRowNo";
                return -1;
            }

            strSql = System.String.Format(strSql, inpatientNo, moOrder, pageNo, rowNo);

            return this.ExecNoQuery(strSql); ;
        }

        /// <summary>
        /// ��ȡ�Ѵ�ӡ�����ҳ��
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        [Obsolete("���Ƽ�ʹ�ã�����ʹ��GetPrintInfo", false)]
        public int GetMaxPageNo(string inpatientNo, string orderType)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Order.Order.GetMaxPageNo", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql:Order.Order.GetMaxPageNo";
                return -1;
            }

            strSql = System.String.Format(strSql, inpatientNo, orderType);

            return FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(strSql));
        }

        /// <summary>
        /// ��ȡ�Ѵ�ӡ�����ҳ����к�
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="isLong"></param>
        /// <param name="maxPageNo"></param>
        /// <param name="maxRowNo"></param>
        /// <returns></returns>
        public int GetPrintInfo(string inpatientNo, bool isLong, ref int maxPageNo, ref int maxRowNo)
        {
            string strSql = @"select t.pageno,t.rowno
                            from met_ipm_order t
                            where t.inpatient_no='{0}'
                            and t.DECMPS_STATE = '{1}'
                            order by t.pageno desc,t.rowno desc";

            //if (this.Sql.GetCommonSql("Order.Order.GetMaxPageNo", ref strSql) == -1)
            //{
            //    this.Err = "Can't Find Sql:Order.Order.GetMaxPageNo";
            //    return -1;
            //}

            string orderType = "1";
            if (!isLong)
            {
                orderType = "0";
            }

            try
            {
                strSql = System.String.Format(strSql, inpatientNo, orderType);

                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    maxPageNo = FS.FrameWork.Function.NConvert.ToInt32(Reader[0]);
                    maxRowNo = FS.FrameWork.Function.NConvert.ToInt32(Reader[1]);
                    break;
                }
            }
            catch (Exception ex)
            {
                Err = Err + "\r\n" + ex.Message;
                maxPageNo = 0;
                maxRowNo = 0;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// �������ҳ�Ż������к�
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="orderType"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        [Obsolete("���Ƽ�ʹ�ã�����ʹ��GetPrintInfo", false)]
        public int GetMaxRowNoByPageNo(string inpatientNo, string orderType, string pageNo)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Order.Order.GetMaxRowNoByPageNo", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql:Order.Order.GetMaxRowNoByPageNo";
                return -1;
            }

            strSql = System.String.Format(strSql, inpatientNo, orderType, pageNo);

            return FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(strSql));
        }

        #region ҽ��������

        /// <summary>
        /// ҽ������ӡ����
        /// </summary>
        /// <param name="lineNO">�к�</param>
        /// <param name="pageNO">ҳ��</param>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <param name="orderType">0 ������1 ������ALL ȫ��</param>
        /// <param name="prnFlag">��ӡ���</param>
        /// <returns></returns>
        public int ResetOrderPrint(string lineNO, string pageNO, string inpatientNO, string orderType, string prnFlag)
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Order.OrderPrn.UpdateOrderPrint", ref sql) == -1)
            {
                this.Err = "û���ҵ�Order.OrderPrn.UpdateOrderPrint�ֶ�!";
                return -1;
            }
            sql = string.Format(sql, lineNO, pageNO, inpatientNO, orderType, prnFlag);
            return this.ExecNoQuery(sql);
        }

        #endregion

        #endregion

        #region


        /// <summary>
        /// {97FA5C9D-F454-4aba-9C36-8AF81B7C9CCF} ��չҽ��
        /// ��ҽ����ˮ�Ų�ѯһ��ʱ����Ӧ���շѵ���δ�շѵ�ҽ��
        /// </summary>
        /// <param name="inpatientNo">סԺ��ˮ��</param>
        /// <param name="itemType">��Ŀ����</param>
        /// <param name="orderID">ҽ����ˮ��</param>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <returns></returns>
        public ArrayList QueryUnFeeExecOrderByOrderID(string inpatientNo, string itemType, string orderID, DateTime beginDate, DateTime endDate)
        {

            string[] s;
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();

            s = ExecOrderQuerySelect(itemType);
            for (int i = 0; i <= s.GetUpperBound(0); i++)
            {
                sql = s[i];
                if (sql == null)
                    return null;
                if (this.Sql.GetCommonSql("Order.ExecOrder.QueryUnFeeExecOrderByOrderID.1", ref sql1) == -1)
                {
                    this.Err = "û���ҵ�Order.ExecOrder.QueryUnFeeExecOrderByOrderID.1�ֶ�!";
                    return null;
                }
                sql = sql + " " + string.Format(sql1, inpatientNo, orderID, beginDate.ToString(), endDate.ToString());
                addExecOrder(al, sql);
            }
            return al;
        }



        #endregion

        #region ҽ������  {FB86E7D8-A148-4147-B729-FD0348A3D670} ���Ӻ���

        /// <summary>
        /// ҽ����������һ����ҽ��״̬��4����ʾ��ҽ���鵵
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public int OrderReform(string OrderID)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.Update.Reform", ref strSql) == -1)
            {
                return -1;
            }

            return this.ExecNoQuery(strSql, OrderID);
        }

        #endregion
        //{7ADC94B3-C691-4c55-89E9-09398DCAA498}
        #region ��ʱҽ��������ѯ��¼��
        /// <summary>
        /// ͨ����ʿվ��ѯ���ߵ�ҽ��ִ�����
        /// </summary>
        /// <param name="nurseCell"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="isInPuted">�Ƿ���¼��ִ�����</param>
        /// <returns></returns>
        public ArrayList QueryOrderExedInfoByNurseCell(string nurseCell, string patientNo, string fromDate, string toDate, bool isInPuted)
        {
            ArrayList al = new ArrayList();
            string sql = string.Empty;
            if (this.Sql.GetCommonSql("Order.ExecOrder.QueryExedInfoByNurseCell", ref sql) < 0)
            {
                this.Err = "û���ҵ�Order.ExecOrder.QueryExedInfoByNurseCell�ֶ�!";
                return null;
            }
            if (isInPuted)
            {
                sql = string.Format(sql, nurseCell, patientNo, fromDate, toDate, "1");
            }
            else
            {
                sql = string.Format(sql, nurseCell, patientNo, fromDate, toDate, "0");
            }

            if (this.ExecQuery(sql) == -1)
            {
                return null;
            }
            try
            {
                FS.HISFC.Models.Order.OrderBill objOrderBill;
                while (this.Reader.Read())
                {
                    objOrderBill = new FS.HISFC.Models.Order.OrderBill();
                    objOrderBill.Order.ID = this.Reader[2].ToString();
                    objOrderBill.Order.Patient.ID = this.Reader[0].ToString();
                    objOrderBill.Order.Patient.Name = this.Reader[1].ToString();
                    objOrderBill.Order.Name = this.Reader[3].ToString();
                    objOrderBill.Order.MOTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[7].ToString());
                    objOrderBill.Order.ExecOper.ID = this.Reader[4].ToString();
                    objOrderBill.Order.ExecOper.Name = this.Reader[5].ToString();
                    objOrderBill.Order.ExecOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6].ToString());
                    objOrderBill.Order.Status = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[8].ToString());
                    al.Add(objOrderBill);
                }
            }
            catch (Exception e)
            {
                this.Err = "ʵ�帳ֵ����" + e.Message;
                return null;
            }
            this.Reader.Close();
            return al;




        }
        /// <summary>
        /// ������ʱҽ�����Ĵ�ӡ���ִ�л�ʿ��ִ��ʱ��
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public int UpdatePrnOrder(string id, FS.HISFC.Models.Order.Order ord)
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Order.OrderPrn.Update", ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                sql = string.Format(sql, id, ((FS.FrameWork.Models.NeuObject)(ord)).ID, FS.FrameWork.Function.NConvert.ToDateTime(ord.ExtendFlag1).ToString(), ord.ExtendFlag2);
            }
            catch
            {
                this.Err = "����������ԣ�Order.OrderPrn.Update";
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(sql);
        }
        #endregion

        /// <summary>
        /// ����Ƥ����Ϣ
        /// </summary>
        /// <param name="i"></param>
        /// <returns>0 (����ҪƤ��);1 [����]; 2 [��Ƥ��]; 3 [+]; 4 [-];</returns>
        public string TransHypotest(FS.HISFC.Models.Order.EnumHypoTest HypotestCode)
        {
            //return FS.FrameWork.Public.EnumHelper.Current.GetName(HypotestCode);
            switch ((int)HypotestCode)
            {
                case 0:
                    //return "����ҪƤ��";
                    return "";
                case 1:
                    return "[����]";
                case 2:
                    return "[��Ƥ��]";
                case 3:
                    return "[+]";
                case 4:
                    return "[-]";
                default:
                    return "[����]";
            }
        }

        /// <summary>
        /// ���ô����ŷ������ʱͬ������ҽ��ִ�е�����Ӧ������
        /// </summary>
        /// <param name="execOrderID">ҽ��ִ�е���ˮ��</param>
        /// <param name="isPharmacy">�Ƿ�ҩƷ</param>
        /// <param name="newRecipeNo">������´�����</param>
        /// <returns>�ɹ����� ���� ���� ʧ�ܷ���-1 ����Ӧ��¼���� 0</returns>
        public int UpdateExecRecipeNo(string execOrderID, bool isPharmacy, string newRecipeNo)
        {
            string strSql = "";
            string strIndex = "";
            if (isPharmacy)			//ҩƷִ�е�
                strIndex = "Order.Update.UpdateExecRecipeNo.1";
            else					//��ҩƷִ�е�
                strIndex = "Order.Update.UpdateExecRecipeNo.2";
            if (this.Sql.GetCommonSql(strIndex, ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, execOrderID, newRecipeNo);
            }
            catch (Exception ex)
            {
                this.Err = "�����������!" + strIndex + ex.Message;
                return -1;
            }
            return ExecNoQuery(strSql);
        }

        /// <summary>
        /// ��ѯҳ�� �к� ��ȡ��־ �Ƿ�δ���³ɹ�
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int CheckPageRowNoAndGetFlag(string patientID, string type)
        {
            string strSql = "";
            int count = 0;
            if (this.Sql.GetCommonSql("Order.Order.CheckPageRowNoAndGetFlag", ref strSql) == -1)
            {
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, patientID, type);
            }
            catch (Exception ex)
            {
                this.Err = "�����������!";
                return -1;
            }

            try
            {
                count = FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(strSql));
            }
            catch (Exception ex)
            {
                this.Err = "��ȡҳ����Ϣ����" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return -1;
            }
            this.Reader.Close();
            return count;
        }


        /// <summary>
        /// ��ʿվҽ��ȷ���շ�
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="orderType"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int ExecChargeConfirm(string patientID, string orderType, ref DataSet ds)
        {
            string strSql = null;
            string decmpsState = null;

            if (this.Sql.GetCommonSql("Nurse.Order.ExecChargeComfirm.Query", ref strSql) == -1)
            {
                this.Err = "δ�ҵ� Nurse.Order.ExecChargeComfirm.Query ��䡣";
                return -1;
            }

            if (orderType.ToUpper().Equals("LONG"))
            {
                decmpsState = "1";
            }
            else if (orderType.ToUpper().Equals("SHORT"))
            {
                decmpsState = "0";
            }

            try
            {
                strSql = string.Format(strSql, patientID, decmpsState);
            }
            catch (Exception ex)
            {
                this.Err = "�����������";
                return -1;
            }

            return this.ExecQuery(strSql, ref ds);
        }

    }
}
