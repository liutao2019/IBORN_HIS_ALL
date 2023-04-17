using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.Report.Logistics.Pharmacy
{
    /// <summary>
    /// ҩƷ��ϸ��
    /// </summary>
    public partial class ucItemizedBill: FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucItemizedBill()
        {
            InitializeComponent();

            this.cmbDrug.SelectedIndexChanged += new EventHandler(cmbDrug_SelectedIndexChanged);
        }

        private void cmbDrug_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbDrug.Tag != null)
            {
                if (this.hsDrug.ContainsKey(this.cmbDrug.Tag.ToString()))
                {
                    FS.HISFC.Models.Pharmacy.Item info = this.hsDrug[this.cmbDrug.Tag.ToString()] as FS.HISFC.Models.Pharmacy.Item;

                    this.lbTitle.Text = info.NameCollection.RegularName + "��ϸ��";
                    this.lbDrugDept.Text = "������:" + this.DrugDept.Name;
                    this.lbSpecs.Text = "���:" + info.Specs;
                    this.lbDosage.Text = "����:" + this.dosageHelper.GetName(info.DosageForm.ID.ToString());
                    this.lbPackUnit.Text = "��װ��λ:" + info.PackUnit;
                    if (this.hsProduce.ContainsKey(info.Product.Producer.ID))
                    {
                        this.lbCompany.Text = "��������:" + this.hsProduce[info.Product.Producer.ID].ToString();
                    }
                }
            }
        }

        #region �����    Y00000000800

        /// <summary>
        /// ҩƷ�б�
        /// </summary>
        private System.Collections.Hashtable hsDrug = new Hashtable();

        /// <summary>
        /// ��������
        /// </summary>
        private System.Collections.Hashtable hsProduce = new Hashtable();

        /// <summary>
        /// ����
        /// </summary>
        private System.Collections.Hashtable hsDept = new Hashtable();

        private FS.FrameWork.Public.ObjectHelper dosageHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ҩƷ������ϸ��
        /// </summary>
        private string outItemizedBillSql = @"select   to_char(t.exam_date,'yyyy-mm-dd hh:mi'),t.out_list_code,
         s.class3_name,t.drug_storage_code,round(sum(t.out_num / t.pack_qty),2),
         t.retail_price,sum(t.sale_cost),t.BATCH_NO,t.purchase_price
from     pha_com_output t,com_priv_class3 s
where    t.out_type = s.class3_code
and      t.drug_dept_code = '{0}' and t.drug_code = '{1}'
and      t.exam_date >= to_date('{2}','yyyy-mm-dd hh24:mi:ss') 
and      t.exam_date < to_date('{3}','yyyy-mm-dd hh24:mi:ss') 
and      s.class2_code = '0320'
group by s.class3_name,t.retail_price,t.out_list_code,t.drug_storage_code,t.BATCH_NO,
         to_char(t.exam_date,'yyyy-mm-dd hh:mi'),t.purchase_price";

        /// <summary>
        /// ҩƷ�����ϸ�� 
        /// </summary>
        private string inItemizedBillSql = @"select   to_char(t.oper_date,'yyyy-mm-dd hh:mi'),t.in_list_code,
         s.class3_name,(select tt.fac_name from pha_com_company tt where tt.fac_code=t.company_code),round(sum(t.in_num / t.pack_qty),2),
         t.retail_price,sum(t.retail_cost),t.BATCH_NO,t.purchase_price
from     pha_com_input t,com_priv_class3 s
where    t.in_type = s.class3_code
and      t.drug_dept_code = '{0}' and t.drug_code = '{1}'
and      t.oper_date >= to_date('{2}','yyyy-mm-dd hh24:mi:ss') 
and      t.oper_date < to_date('{3}','yyyy-mm-dd hh24:mi:ss') 
and      s.class2_code = '0310'
group by s.class3_name,t.retail_price,t.in_list_code,t.company_code,t.BATCH_NO,
         to_char(t.oper_date,'yyyy-mm-dd hh:mi'),t.purchase_price";

        /// <summary>
        /// ҩƷ������ϸ��
        /// </summary>
        private string adjuseItemizedBillSql = @"select   to_char(t.inure_time,'yyyy-mm-dd hh:mi'),t.adjust_bill_code,
         decode(t.profit_flag,'1','��ӯ','0','����'),
         '',round(sum(t.store_sum / t.pack_qty),2),
         t.retail_price,sum(round(t.store_sum / t.pack_qty * t.retail_price,2))
from     pha_com_adjustpricedetail t
where    t.drug_dept_code = '{0}' and t.drug_code = '{1}'
and      t.inure_time >= to_date('{2}','yyyy-mm-dd hh24:mi:ss') 
and      t.inure_time < to_date('{3}','yyyy-mm-dd hh24:mi:ss') 
and      t.current_state = '1'
group by t.retail_price,t.adjust_bill_code,decode(t.profit_flag,'1','��ӯ','0','����'),
         to_char(t.inure_time,'yyyy-mm-dd hh:mi')";

        /// <summary>
        /// ҩƷ�̵���ϸ��
        /// </summary>
        private string checkItemizedBillSql = @"select   to_char(t.oper_date,'yyyy-mm-dd hh:mi'),t.check_code,
         decode(t.profit_flag,'1','��ӯ','0','�̿�','2','�̵���ӯ��'),
         '',round(sum(t.cstore_num / t.pack_qty),2),
         t.retail_price,sum(round(t.cstore_num / t.pack_qty * t.retail_price,2)),t.BATCH_NO
from     pha_com_checkdetail t
where    t.drug_dept_code = '{0}' and t.drug_code = '{1}'
and      t.oper_date >= to_date('{2}','yyyy-mm-dd hh24:mi:ss') 
and      t.oper_date < to_date('{3}','yyyy-mm-dd hh24:mi:ss') 
and      t.check_state = '1'
and      t.profit_flag != '2'
group by t.retail_price,t.check_code,decode(t.profit_flag,'1','��ӯ','0','�̿�','2','�̵���ӯ��'),t.BATCH_NO,
         to_char(t.oper_date,'yyyy-mm-dd hh:mi')";

        /// <summary>
        /// ҩƷ�½���ϸ����
        /// </summary>
        private string msCurrentItemizedBillSql = @"select   to_char(t.to_date,'yyyy-mm-dd hh:mi'),'',
         '���ڿ��','',round(sum(t.current_month_num / t.pack_qty),2),
         t.retail_price,sum(t.current_month_cost),t.BATCH_NO
from     pha_com_ms_drug t
where    t.drug_dept_code = '{0}' and t.drug_code = '{1}'
and      t.to_date >= to_date('{2}','yyyy-mm-dd hh24:mi:ss') 
and      t.to_date <= to_date('{3}','yyyy-mm-dd hh24:mi:ss') 
group by t.retail_price,to_char(t.to_date,'yyyy-mm-dd hh:mi'),t.BATCH_NO";

        /// <summary>
        /// ҩƷ�½���ϸ����
        /// </summary>
        private string msLastItemizedBillSql = @"select   to_char(t.to_date,'yyyy-mm-dd hh:mi'),'',
         '����ת��','',round(sum(t.last_month_num / t.pack_qty),2),
         t.retail_price,sum(t.last_month_num),t.BATCH_NO
from     pha_com_ms_drug t
where    t.drug_dept_code = '{0}' and t.drug_code = '{1}'
and      t.to_date >= to_date('{2}','yyyy-mm-dd hh24:mi:ss') 
and      t.to_date <= to_date('{3}','yyyy-mm-dd hh24:mi:ss') 
group by t.retail_price,to_char(t.to_date,'yyyy-mm-dd hh:mi'),t.BATCH_NO";

        /// <summary>
        /// ��ȡ��ѯ��ʼʱ��
        /// </summary>
        private string getQueryBeginDate = @"select  max(t.from_date)
from    pha_com_ms_dept t
where   t.drug_dept_code = '{0}'
and     t.from_date <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
and     t.to_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')";

        private string getQueryEndDate = @"select  max(t.to_date)
from    pha_com_ms_dept t
where   t.drug_dept_code = '{0}'
and     t.from_date <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
and     t.to_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')";

        /// <summary>
        /// ҵ�������
        /// </summary>
        FS.FrameWork.Management.DataBaseManger dataManagment = new FS.FrameWork.Management.DataBaseManger();

        #endregion

        #region ����

        /// <summary>
        /// ͳ��ҩƷ
        /// </summary>
        protected string DrugCode
        {
            get
            {
                return this.currentDrugCode;

                if (this.cmbDrug.Tag != null)
                {
                    return this.cmbDrug.Tag.ToString();
                }
                return null;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        protected FS.FrameWork.Models.NeuObject DrugDept
        {
            get
            {
                return ((FS.HISFC.Models.Base.Employee)this.dataManagment.Operator).Dept;
            }
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ������� ���Ժ�...");
            Application.DoEvents();

            FS.HISFC.BizLogic.Pharmacy.Item itemManagment = new FS.HISFC.BizLogic.Pharmacy.Item();

            List<FS.HISFC.Models.Pharmacy.Item> itemDictionary = itemManagment.QueryItemAvailableList();
            //sdh 20190107 ��ʾͨ����
            foreach(FS.HISFC.Models.Pharmacy.Item it1 in itemDictionary)
            {
                it1.Name = it1.NameCollection.RegularName;
                it1.Memo = it1.Specs;

                it1.SpellCode = it1.NameCollection.RegularSpell.SpellCode;
                it1.WBCode = it1.NameCollection.RegularSpell.WBCode;
                it1.UserCode = it1.NameCollection.RegularSpell.UserCode;                                   
            }

            if (itemDictionary == null)
            {
                MessageBox.Show("����ҩƷ�б�������" + itemManagment.Err);
                return;
            }
            
            //this.cmbDrug.AddItems(new System.Collections.ArrayList(itemDictionary.ToArray()));

            foreach (FS.HISFC.Models.Pharmacy.Item info in itemDictionary)
            {
                this.hsDrug.Add(info.ID, info);
            }

            this.ucDrugList1.ShowPharmacyList();
            //this.ucDrugList1.ShowPharmacyList(false);  �Ƿ���ʾЭ������
            this.ucDrugList1.SetFormat(null, new int[] { 10, 170, 90,20,60,170, 90,20,20,20,20,170}, null);
            this.ucDrugList1.ChooseDataEvent += new FS.HISFC.Components.Common.Controls.ucDrugList.ChooseDataHandler(ucDrugList1_ChooseDataEvent);

            FS.HISFC.BizLogic.Pharmacy.Constant phaCons = new FS.HISFC.BizLogic.Pharmacy.Constant();

            ArrayList alProduce = phaCons.QueryCompany("0");
            if (alProduce == null)
            {
                MessageBox.Show("�������������б�������" + phaCons.Err);
                return;
            }
            foreach (FS.HISFC.Models.Pharmacy.Company compyInfo in alProduce)
            {
                this.hsProduce.Add(compyInfo.ID, compyInfo.Name);
            }

            FS.HISFC.BizLogic.Manager.Department deptManagment = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList alDept = deptManagment.GetDeptmentAll();
            if (alDept == null)
            {
                MessageBox.Show("���ؿ����б�������" + phaCons.Err);
                return;
            }
            foreach (FS.HISFC.Models.Base.Department deptInfo in alDept)
            {
                this.hsDept.Add(deptInfo.ID, deptInfo.Name);
            }

            FS.HISFC.BizLogic.Manager.Constant consManagment = new FS.HISFC.BizLogic.Manager.Constant();
            ArrayList alDosage = consManagment.GetList("DOSAGEFORM");
            if (alDosage == null)
            {
                MessageBox.Show("���ؼ����б�������" + consManagment.Err);
                return;
            }
            this.dosageHelper = new FS.FrameWork.Public.ObjectHelper(alDosage);

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            base.OnLoad(e);
        }

        private string currentDrugCode = null;

        void ucDrugList1_ChooseDataEvent(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            string drugCode = sv.Cells[activeRow, 0].Text;

            if (this.hsDrug.ContainsKey(drugCode))
            {
                this.currentDrugCode = drugCode;

                FS.HISFC.Models.Pharmacy.Item info = this.hsDrug[drugCode] as FS.HISFC.Models.Pharmacy.Item;

                this.lbTitle.Text = info.NameCollection.RegularName + "��ϸ��";
                this.lbDrugDept.Text = "������:" + this.DrugDept.Name;
                this.lbSpecs.Text = "���:" + info.Specs;
                this.lbDosage.Text = "����:" + this.dosageHelper.GetName(info.DosageForm.ID.ToString());
                this.lbPackUnit.Text = "��װ��λ:" + info.PackUnit;
                if (this.hsProduce.ContainsKey(info.Product.Producer.ID))
                {
                    this.lbCompany.Text = "��������:" + this.hsProduce[info.Product.Producer.ID].ToString();
                }
            }
        }

        public override int Export(object sender, object neuObject)
        {
            if (this.neuSpread1.Export() == 1)
            {
                MessageBox.Show("�����ɹ�!");
            }

            return base.Export(sender, neuObject);
        }

        private ArrayList QueryItemizedData(string execSql,DataSource dataSource,DateTime beginDate,DateTime endDate)
        {
            if (dataManagment.ExecQuery(execSql, this.DrugDept.ID,this.DrugCode,beginDate.ToString(),endDate.ToString()) == -1)
            {
                MessageBox.Show("ִ�в�ѯSql��䷢������" + this.dataManagment.Err);
                return null;
            }

            ArrayList alData = new ArrayList();
            while (this.dataManagment.Reader.Read())
            {
                ItemizedBill info = new ItemizedBill();

                info.Source = dataSource;

                info.OperDate = this.dataManagment.Reader[0].ToString();    //����
                info.ListNO = this.dataManagment.Reader[1].ToString();      //���ݺ�
                info.OperType = this.dataManagment.Reader[2].ToString();    //Ȩ�����
                info.TargetDeptNO = this.dataManagment.Reader[3].ToString();
                info.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.dataManagment.Reader[4]);
                info.RetailPrice = FS.FrameWork.Function.NConvert.ToDecimal(this.dataManagment.Reader[5]);
                info.Cost = FS.FrameWork.Function.NConvert.ToDecimal(this.dataManagment.Reader[6]);
                if (dataManagment.Reader.FieldCount > 7)
                {
                    info.BatchNo = this.dataManagment.Reader[7].ToString();
                }
                if (dataManagment.Reader.FieldCount > 8)
                {
                    info.PurchasePrice = FS.FrameWork.Function.NConvert.ToDecimal(this.dataManagment.Reader[8]);
                }
                alData.Add(info);
            }

            return alData;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.dtBegin.Value > this.dtEnd.Value)
            {
                MessageBox.Show( "��ѯ��ʼʱ�䲻�ܴ��ڲ�ѯ��ֹʱ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return -1;
            }

            string drugCode = this.ucDrugList1.CurrentSheetView.Cells[this.ucDrugList1.CurrentSheetView.ActiveRowIndex, 0].Text;

            if (this.hsDrug.ContainsKey(drugCode))
            {
                this.currentDrugCode = drugCode;

                FS.HISFC.Models.Pharmacy.Item info = this.hsDrug[drugCode] as FS.HISFC.Models.Pharmacy.Item;

                this.lbTitle.Text = info.NameCollection.RegularName + "��ϸ��";
                this.lbDrugDept.Text = "������:" + this.DrugDept.Name;
                this.lbSpecs.Text = "���:" + info.Specs;
                this.lbDosage.Text = "����:" + this.dosageHelper.GetName(info.DosageForm.ID.ToString());
                this.lbPackUnit.Text = "��װ��λ:" + info.PackUnit;
                if (this.hsProduce.ContainsKey(info.Product.Producer.ID))
                {
                    this.lbCompany.Text = "��������:" + this.hsProduce[info.Product.Producer.ID].ToString();
                }
            }

            if (string.IsNullOrEmpty(this.DrugCode))
            {
                MessageBox.Show("��ѡ����ͳ�Ƶ�ҩƷ");
                return -1;
            }

            DateTime queryBeginDate = System.DateTime.Now.AddYears(-100);
            DateTime queryEndDate = System.DateTime.Now.AddYears(100);

            #region ��ȡͳ����ʼʱ��

            FS.FrameWork.Management.DataBaseManger dataManagement = new FS.FrameWork.Management.DataBaseManger();

            string execBeginSql = string.Format(this.getQueryBeginDate, this.DrugDept.ID, FS.FrameWork.Function.NConvert.ToDateTime(this.dtBegin.Text).ToString());

            string beginDateString = dataManagement.ExecSqlReturnOne(execBeginSql);

            queryBeginDate = FS.FrameWork.Function.NConvert.ToDateTime(beginDateString);

            string execEndSql = string.Format(this.getQueryEndDate, this.DrugDept.ID, FS.FrameWork.Function.NConvert.ToDateTime(this.dtEnd.Text).ToString());
            string endDateString = dataManagement.ExecSqlReturnOne(execEndSql);

            queryEndDate = FS.FrameWork.Function.NConvert.ToDateTime(endDateString);
            if (queryEndDate == DateTime.MinValue)
            {
                queryEndDate = dataManagement.GetDateTimeFromSysDateTime().AddDays(1);
            }

            #endregion

            this.lbStatDate.Text = "�������:" + queryBeginDate.ToString() + " - " + queryEndDate.ToString();

            ArrayList alAllData = new ArrayList();

            #region ���ݼ���

            ArrayList alInData = this.QueryItemizedData(this.inItemizedBillSql, DataSource.In, queryBeginDate,queryEndDate);
            if (alInData == null)
            {
                return -1;
            }

            alAllData.AddRange(alInData);

            ArrayList alOutData = this.QueryItemizedData(this.outItemizedBillSql, DataSource.Out, queryBeginDate, queryEndDate);
            if (alOutData == null)
            {
                return -1;
            }

            alAllData.AddRange(alOutData);

            ArrayList alAdjustData = this.QueryItemizedData(this.adjuseItemizedBillSql, DataSource.Adjust, queryBeginDate, queryEndDate);
            if (alAdjustData == null)
            {
                return -1;
            }

            alAllData.AddRange(alAdjustData);

            ArrayList alCheckData = this.QueryItemizedData(this.checkItemizedBillSql, DataSource.Check, queryBeginDate, queryEndDate);
            if (alCheckData == null)
            {
                return -1;
            }

            alAllData.AddRange(alCheckData);

            ArrayList alMsCurrentData = this.QueryItemizedData(this.msCurrentItemizedBillSql, DataSource.MonthCurrent, queryBeginDate, queryEndDate);
            if (alMsCurrentData == null)
            {
                return -1;
            }

            alAllData.AddRange(alMsCurrentData);

            ArrayList alMsLastData = this.QueryItemizedData(this.msLastItemizedBillSql, DataSource.MonthLast, queryBeginDate, queryEndDate);
            if (alMsLastData == null)
            {
                return -1;
            }

            alAllData.AddRange(alMsLastData);


            #endregion

            CompareDate sortData = new CompareDate();
            alAllData.Sort(sortData);

            return this.ShowData(alAllData);
        }

        /// <summary>
        /// ���ݼ�����ʾ
        /// </summary>
        /// <param name="alData">��������</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int ShowData(ArrayList alData)
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;

            this.neuSpread1_Sheet1.Rows.Count = 0;

            int iRowIndex = 0;
            string currentMonthStr = "";

            for (int i = 0; i < alData.Count; i++)
            {
                ItemizedBill info = alData[i] as ItemizedBill;

                this.neuSpread1_Sheet1.Rows.Add(iRowIndex,1);

                this.neuSpread1_Sheet1.Cells[iRowIndex, 0].Text = info.OperDate;    //����
                this.neuSpread1_Sheet1.Cells[iRowIndex, 1].Text = info.ListNO;      //ƾ֤���
                this.neuSpread1_Sheet1.Cells[iRowIndex, 2].Text = info.TargetDeptNO;      //ժҪ

                // �����
                if (info.Source == DataSource.In || info.Source == DataSource.Out)
                {
                    this.neuSpread1_Sheet1.Cells[iRowIndex, 3].Text = info.PurchasePrice.ToString();
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[iRowIndex, 3].Text = "";
                }

                //����������ʾ sdh 2010-01-07
                this.neuSpread1_Sheet1.Cells[iRowIndex, 12].Text = info.BatchNo;//����

                string source = info.OperType;

                if ((info.Source == DataSource.In) || (info.Source == DataSource.Adjust && info.OperType == "��ӯ") ||
                    (info.Source == DataSource.Check && info.OperType == "��ӯ"))
                {
                    this.neuSpread1_Sheet1.Cells[iRowIndex, 4].Text = info.Qty.ToString();
                    this.neuSpread1_Sheet1.Cells[iRowIndex, 5].Text = info.RetailPrice.ToString();
                    this.neuSpread1_Sheet1.Cells[iRowIndex, 6].Text = info.Cost.ToString();

                    if (this.hsProduce.ContainsKey(info.TargetDeptNO))
                    {
                        source = source + "(" + this.hsProduce[info.TargetDeptNO] + ")";
                    }
                }
                else if ((info.Source == DataSource.Out) || (info.Source == DataSource.Adjust && info.OperType == "����") ||
                    (info.Source == DataSource.Check && info.OperType == "�̿�"))
                {
                    this.neuSpread1_Sheet1.Cells[iRowIndex, 7].Text = info.Qty.ToString();
                    this.neuSpread1_Sheet1.Cells[iRowIndex, 8].Text = info.RetailPrice.ToString();
                    this.neuSpread1_Sheet1.Cells[iRowIndex, 9].Text = info.Cost.ToString();

                    if (this.hsDept.ContainsKey(info.TargetDeptNO))
                    {
                        source = source + "(" + this.hsDept[info.TargetDeptNO] + ")";
                    }
                }
                else if (info.Source == DataSource.MonthCurrent || info.Source == DataSource.MonthLast)
                {
                    this.neuSpread1_Sheet1.Cells[iRowIndex, 10].Text = info.Qty.ToString();
                    this.neuSpread1_Sheet1.Cells[iRowIndex, 11].Text = info.Cost.ToString();

                    this.neuSpread1_Sheet1.Rows[iRowIndex].Tag = info.Source.ToString();
                }

                this.neuSpread1_Sheet1.Cells[iRowIndex, 2].Text = source;      //ժҪ
                if (info.Source == DataSource.In)
                {
                    this.neuSpread1_Sheet1.Cells[iRowIndex, 2].Text = source + "(" + info.TargetDeptNO + ")";
                }

                if (info.OperType == "���ڿ��")
                {
                    iRowIndex++;

                    this.neuSpread1_Sheet1.Rows.Add(iRowIndex, 1);
                    this.neuSpread1_Sheet1.Cells[iRowIndex, 0].Text = info.OperDate.Substring(0, 7);
                    this.neuSpread1_Sheet1.Cells[iRowIndex, 2].Text = "���ںϼ�";

                    this.neuSpread1_Sheet1.Rows[iRowIndex].Tag = "1";       //��ʶ�ϼ���
                }

                iRowIndex++;
                
                currentMonthStr = info.OperDate.Substring(0, 8);

                //if (string.IsNullOrEmpty(currentMonthStr) == true)
                //{
                //    currentMonthStr = info.OperDate.Substring(0, 7);
                //}
                //else
                //{
                //    if (currentMonthStr != info.OperDate.Substring(0, 7))       //����ͬһ���µ�����
                //    {
                //        this.neuSpread1_Sheet1.Rows.Add(iRowIndex, 1);
                //        this.neuSpread1_Sheet1.Cells[iRowIndex, 0].Text = info.OperDate.Substring(0, 7);
                //        this.neuSpread1_Sheet1.Cells[iRowIndex, 2].Text = "���ںϼ�";

                //        this.neuSpread1_Sheet1.Rows[iRowIndex].Tag = "1";       //��ʶ�ϼ���

                //        iRowIndex++;

                //        currentMonthStr = info.OperDate.Substring(0, 7);
                //    }
                //}
            }

            //this.neuSpread1_Sheet1.Rows.Add(iRowIndex, 1);
            //this.neuSpread1_Sheet1.Cells[iRowIndex, 0].Text = currentMonthStr;
            //this.neuSpread1_Sheet1.Cells[iRowIndex, 2].Text = "���ںϼ�";

            //this.neuSpread1_Sheet1.Rows[iRowIndex].Tag = "1";

           this.neuSpread1_Sheet1.Rows.Add(iRowIndex, 1);
           this.neuSpread1_Sheet1.Cells[iRowIndex, 0].Text = "";
           this.neuSpread1_Sheet1.Cells[iRowIndex, 2].Text = "��ǰ�ϼ�";

           this.neuSpread1_Sheet1.Rows[iRowIndex].Tag = "1";       //��ʶ�ϼ���

           this.SumByMonth();

            return 1;
        }

        /// <summary>
        /// ���»���
        /// </summary>
        /// <returns></returns>
        protected int SumByMonth()
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {               
                decimal debitQty = FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, 4].Text);
                decimal debitCost = FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, 6].Text);

                decimal creditQty = -FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, 7].Text);
                decimal creditCost = -FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, 9].Text);

                if (this.neuSpread1_Sheet1.Cells[i, 2].Text == "��ӯ" || this.neuSpread1_Sheet1.Cells[i,2].Text == "��ӯ")
                {
                    this.neuSpread1_Sheet1.Cells[i, 10].Text = debitQty.ToString();
                    this.neuSpread1_Sheet1.Cells[i, 11].Text = debitCost.ToString();
                }
                else if (this.neuSpread1_Sheet1.Cells[i, 2].Text == "����" || this.neuSpread1_Sheet1.Cells[i,2].Text == "�̿�")
                {
                    this.neuSpread1_Sheet1.Cells[i, 10].Text = (-creditQty).ToString();
                    this.neuSpread1_Sheet1.Cells[i, 11].Text = (-creditCost).ToString();
                }
                else if ((this.neuSpread1_Sheet1.Rows[i].Tag != null) && (this.neuSpread1_Sheet1.Rows[i].Tag.ToString() != "1"))    //�Ǻϼ���
                {
                    if ((this.neuSpread1_Sheet1.Rows[i].Tag.ToString() == DataSource.MonthCurrent.ToString()) ||
                    (this.neuSpread1_Sheet1.Rows[i].Tag.ToString() == DataSource.MonthLast.ToString()))
                    {
                        //�����и�ֵ����
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        this.neuSpread1_Sheet1.Cells[i, 10].Text = (debitQty + creditQty).ToString();
                        this.neuSpread1_Sheet1.Cells[i, 11].Text = (debitCost + creditCost).ToString();
                    }
                    else
                    {
                        decimal privTotQty = FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i - 1, 10].Text);
                        decimal privTotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i - 1, 11].Text);

                        this.neuSpread1_Sheet1.Cells[i, 10].Text = (privTotQty + debitQty + creditQty).ToString();
                        this.neuSpread1_Sheet1.Cells[i, 11].Text = (privTotCost + debitCost + creditCost).ToString();
                    }
                }
            }

            return 1;
        }

        /// <summary>
        /// ������
        /// </summary>
        public class CompareDate : IComparer
        {
            /// <summary>
            /// ���򷽷�
            /// </summary>
            public int Compare(object x, object y)
            {
                ItemizedBill o1 = (x as ItemizedBill);
                ItemizedBill o2 = (y as ItemizedBill);

                string oX = o1.OperDate;          //��������
                string oY = o2.OperDate;          //

                int nComp;

                if (oX == null)
                {
                    nComp = (oY != null) ? -1 : 0;
                }
                else if (oY == null)
                {
                    nComp = 1;
                }
                else
                {
                    nComp = string.Compare(oX.ToString(), oY.ToString());
                }

                return nComp;
            }
        }
    }

    internal enum DataSource
    {
        In,
        Out,
        Adjust,
        MonthCurrent,
        Check,
        MonthLast
    }

    internal class ItemizedBill
    {
        /// <summary>
        /// ������Դ
        /// </summary>
        public DataSource Source;

        /// <summary>
        /// ��������
        /// </summary>
        public string OperDate;

        /// <summary>
        /// ���ݺ�
        /// </summary>
        public string ListNO;

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        public string OperType;

        /// <summary>
        /// Ŀ�����
        /// </summary>
        public string TargetDeptNO;

        /// <summary>
        /// ����
        /// </summary>
        public decimal Qty;

        /// <summary>
        /// ���ۼ�
        /// </summary>
        public decimal RetailPrice;

        /// <summary>
        ///  ���
        /// </summary>
        public decimal Cost;

        /// <summary>
        /// ����
        /// </summary>
        public string BatchNo;

        /// <summary>
        /// �����
        /// </summary>
        public decimal PurchasePrice;
    }
}
