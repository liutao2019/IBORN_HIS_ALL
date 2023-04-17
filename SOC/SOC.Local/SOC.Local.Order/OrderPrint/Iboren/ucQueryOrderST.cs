using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Order.OrderPrint.Iboren
{
    /// <summary>
    /// �Һ������ѯ
    /// </summary>
    public partial class ucQueryOrderST : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        //{C42F14B0-81D2-4eae-B708-6431EA819622}
        public ucQueryOrderST()
        {
            InitializeComponent();

            this.init();

            this.txtCardNo.KeyDown += new KeyEventHandler(txtCardNo_KeyDown);
            this.txtInvoice.KeyDown += new KeyEventHandler(txtInvoice_KeyDown);
            this.txtName.KeyDown += new KeyEventHandler(txtName_KeyDown);
            //this.fpSpread1.CellDoubleClick+=new FarPoint.Win.Spread.CellClickEventHandler(fpSpread1_CellDoubleClick);
            //this.fpSpread1.CellClick+=new FarPoint.Win.Spread.CellClickEventHandler(fpSpread1_CellClick);
        }

        /// <summary>
        /// ������Ϣ����
        /// </summary>
        Hashtable hsPatient = new Hashtable();

        /// <summary>
        /// ������
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        protected FS.FrameWork.Public.ObjectHelper personHelper = null;

        /// <summary>
        /// סԺ
        /// </summary>
        protected FS.HISFC.BizLogic.Order.Order ordMgr = new FS.HISFC.BizLogic.Order.Order();

        /// <summary>
        /// ����
        /// </summary>
        protected FS.HISFC.BizLogic.Order.OutPatient.Order outOrdMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void init()
        {
            this.dateBegin.Value = this.ordMgr.GetDateTimeFromSysDateTime().Date;
            this.dateEnd.Value = this.dateBegin.Value;

            //�Һſ���
            FS.HISFC.BizProcess.Integrate.Manager deptMgr = new FS.HISFC.BizProcess.Integrate.Manager();

            ArrayList al = deptMgr.QueryRegDepartment();
            if (al == null) al = new ArrayList();

            this.cmbDept.AddItems(al);

            //�Һ�ҽ��            
            al = deptMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (al == null) al = new ArrayList();

            this.cmbDoct.AddItems(al);

            FS.HISFC.Models.Base.Const c1 = new FS.HISFC.Models.Base.Const();
            FS.HISFC.Models.Base.Const c2 = new FS.HISFC.Models.Base.Const();
            ArrayList alTemp = new ArrayList();
            c1.ID = "0";
            c1.Name = "����";
            alTemp.Add(c1);
            c2.ID = "1";
            c2.Name = "סԺ";
            alTemp.Add(c2);
            this.cmbSeeFlag.ClearItems();
            this.cmbSeeFlag.AddItems(alTemp);

            this.InitFarpoint();

            ////����Ա
            //al = deptMgr.QueryEmployeeAll();
            //if (al == null) al = new ArrayList();

            //this.cmbOper.AddItems(al);
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        private void Query()
        {
            if (this.fpSpread1_Sheet1.RowCount > 0)
                this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);

            string where1 = this.getSingleWhere();

            string where2 = "";

            if (this.getCompoundWhere(ref where2) == -1) return;

            if (where1 != "" && where2 != "")
            {
                where1 = where1 + " AND " + where2;
            }
            else if (where2 != "")
            {
                where1 = where2;
            }
            else if (where1 == "" && where2 == "")
            {
                MessageBox.Show("��ָ����ѯ����!", "��ʾ");
                return;
            }

            ArrayList alOrder = new ArrayList();
            if (this.cmbSeeFlag.Text == "����")
            {
                //{65BAEEB6-D491-4616-925A-EE90F0325048}
                where1 = where1 + @" and not exists (select 1 from  met_ord_recipe_ext_st st 
                                    where st.card_no=met_ord_recipedetail.card_no
                                    and met_ord_recipedetail.see_no =st.see_no
                                    and met_ord_recipedetail.sequence_no =st.recipe_no)";
                string where3 = " where " + where1 + " and met_ord_recipedetail.item_code in (select b.drug_code from pha_com_baseinfo b where b.drug_quality in  ( 'P1','P2','S1','SY','O1'))";

                alOrder = outOrdMgr.QueryOrderForST(where3);
            }
            else
            {

                //{65BAEEB6-D491-4616-925A-EE90F0325048}
                string where3 = " where " + where1 + " and met_ipm_order.item_code in (select b.drug_code from pha_com_baseinfo b where b.drug_quality in  ( 'P1','P2','S1','SY','O1'))";
                alOrder = ordMgr.QueryOrderBase(where3);
            }
            if (alOrder == null || alOrder.Count == 0)
            {
                MessageBox.Show("û���ҵ���Ч��Ϣ��");
                return;
            }
            this.addDetail(alOrder);
            this.SetPatientInfo();
        }

        private void QueryST()
        {
            string where = "where ";

            if (this.txtCardNo.Text.Trim() != "")
            {
                string cardNo = this.txtCardNo.Text.Trim().PadLeft(10, '0');
                this.txtCardNo.Text = cardNo;
                this.txtCardNo.SelectAll();
                where =where+ " met_ord_recipe_ext_st.card_no ='" + cardNo + "'";
               
            }
            else
            {
                if (this.cmbSeeFlag.Text == "����")
                {
                    if (this.txtName.Text.Trim() != "")
                    {
                        where =where+ "  met_ord_recipe_ext_st.clinic_code in ( select r.clinic_code from fin_opr_register r where r.name='" + this.txtName.Text.Trim() + "')";
                       
                    }
                }
                else
                {
                    if (this.txtName.Text.Trim() != "")
                    {
                        where =where+ "  met_ord_recipe_ext_st.clinic_code in (select i.inpatient_no from fin_ipr_inmaininfo i where i.name ='" + this.txtName.Text.Trim() + "')";
                       
                    }
                }
            }
            if (this.checkBox1.Checked)
            {
                if (this.dateBegin.Value > this.dateEnd.Value)
                {
                    MessageBox.Show("��ѯ��ʼʱ�䲻�ܴ��ڽ���ʱ��!", "��ʾ");
                    return ;
                }

                if (where != "")
                    where = where + " AND ";

                where = where + "  met_ord_recipe_ext_st.exec_date >=to_date('" + this.dateBegin.Value.ToString() + "','yyyy-mm-dd HH24:mi:ss')" +
                    " AND   met_ord_recipe_ext_st.exec_date <=to_date('" + this.dateEnd.Value.AddDays(1).ToString() + "','yyyy-mm-dd HH24:mi:ss')";
            }

            if (this.checkBox2.Checked && this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
            {
                if (where != "")
                    where = where + " AND ";

                where = where + "met_ord_recipe_ext_st.recipe_dept_code = '" + this.cmbDept.Tag.ToString() + "'";
            }

            if (this.checkBox3.Checked && this.cmbDoct.Tag != null && this.cmbDoct.Tag.ToString() != "")
            {
                if (where != "")
                    where = where + " AND ";

                where = where + "met_ord_recipe_ext_st.recipe_doc_code = '" + this.cmbDoct.Tag.ToString() + "'";
            }
            ArrayList alOrder = ordMgr.QueryOrderBySQL(where);
            this.addDetailST(alOrder);
            this.SetPatientInfo();
        }

        private void Print()
        {
            ucRecipePrintST ucRecipePrintST = new ucRecipePrintST();

            ArrayList alTemp = new ArrayList();
            //{65BAEEB6-D491-4616-925A-EE90F0325048}
            alTemp = this.GetOrderSTInfo();
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                //�������㵱ǰӦ�ô�ӡ�ڼ���
                int printIndex = 0;
                if ((bool)this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.isChoose].Value)
                {
                    ucRecipePrintST = new ucRecipePrintST();
                    FS.HISFC.Models.RADT.PatientInfo pi 
                        =this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.isChoose].Tag as FS.HISFC.Models.RADT.PatientInfo;
                    ucRecipePrintST.SetPatientInfo(pi);

                    ArrayList alTemp1 = new ArrayList();
                    alTemp1.Add(alTemp[printIndex]);
                    ucRecipePrintST.MakaLabel(alTemp1);
                    ucRecipePrintST.PrintRecipe();
                    printIndex++;
                }
            } 

        }

        private string getSingleWhere()
        {
            string where = "";

            if (this.cmbSeeFlag.Text == "����")
            {
                if (this.txtCardNo.Text.Trim() != "")
                {
                    string cardNo = this.txtCardNo.Text.Trim().PadLeft(10, '0');
                    this.txtCardNo.Text = cardNo;
                    this.txtCardNo.SelectAll();
                    where = " met_ord_recipedetail.status in ('1','2') and met_ord_recipedetail.card_no ='" + cardNo + "'";
                    return where;
                }

                if (this.txtName.Text.Trim() != "")
                {
                    where = " met_ord_recipedetail.status in ('1','2') and met_ord_recipedetail.clinic_code in ( select r.clinic_code from fin_opr_register r where r.name='" + this.txtName.Text.Trim() + "')";
                    return where;
                }
            }
            else
            {
                if (this.txtCardNo.Text.Trim() != "")
                {
                    where = " met_ipm_order.patient_no like '%" + this.txtCardNo.Text.Trim() + "'";
                    return where;
                }
                if (this.txtName.Text.Trim() != "")
                {
                    where = " met_ipm_order.inpatient_no in (select i.inpatient_no from fin_ipr_inmaininfo i where i.name ='" + this.txtName.Text.Trim() + "')";
                    return where;
                }
            }

            return "";
        }


        private int getCompoundWhere(ref string rtn)
        {
            string where = "";

            if (this.cmbSeeFlag.Text == "����")
            {
                if (this.checkBox1.Checked)
                {
                    if (this.dateBegin.Value > this.dateEnd.Value)
                    {
                        MessageBox.Show("��ѯ��ʼʱ�䲻�ܴ��ڽ���ʱ��!", "��ʾ");
                        rtn = "";
                        return -1;
                    }

                    where = "  met_ord_recipedetail.reg_date >=to_date('" + this.dateBegin.Value.ToString() + "','yyyy-mm-dd HH24:mi:ss')" +
                        " AND   met_ord_recipedetail.reg_date <=to_date('" + this.dateEnd.Value.AddDays(1).ToString() + "','yyyy-mm-dd HH24:mi:ss')";
                }

                if (this.checkBox2.Checked && this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                {
                    if (where != "")
                        where = where + " AND ";

                    where = where + " met_ord_recipedetail.dept_code = '" + this.cmbDept.Tag.ToString() + "'";
                }

                if (this.checkBox3.Checked && this.cmbDoct.Tag != null && this.cmbDoct.Tag.ToString() != "")
                {
                    if (where != "")
                        where = where + " AND ";

                    where = where + " met_ord_recipedetail.DOCT_CODE = '" + this.cmbDoct.Tag.ToString() + "'";
                }
            }
            else
            {
                if (this.checkBox1.Checked)
                {
                    if (this.dateBegin.Value > this.dateEnd.Value)
                    {
                        MessageBox.Show("��ѯ��ʼʱ�䲻�ܴ��ڽ���ʱ��!", "��ʾ");
                        rtn = "";
                        return -1;
                    }

                    where = " met_ipm_order.MO_DATE >=to_date('" + this.dateBegin.Value.ToString() + "','yyyy-mm-dd HH24:mi:ss')" +
                        " AND met_ipm_order.MO_DATE <=to_date('" + this.dateEnd.Value.AddDays(1).ToString() + "','yyyy-mm-dd HH24:mi:ss')";
                }
                if (this.checkBox2.Checked && this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                {
                    if (where != "")
                        where = where + " AND ";

                    where = where + " met_ipm_order.dept_code = '" + this.cmbDept.Tag.ToString() + "'";
                }

                if (this.checkBox3.Checked && this.cmbDoct.Tag != null && this.cmbDoct.Tag.ToString() != "")
                {
                    if (where != "")
                        where = where + " AND ";

                    where = where + " met_ipm_order.doc_code = '" + this.cmbDoct.Tag.ToString() + "'";
                }

            }
            rtn = where;
            return 0;
        }

        private int Save()
        {
            ArrayList alOrderST = this.GetOrderSTInfo();
            if (alOrderST.Count == 0)
            {
                return 0;
            }

            this.ordMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            foreach (FS.HISFC.Models.Order.OrderST order in alOrderST)
            {
                if (order.IsInput)
                {
                    if (ordMgr.UpdateOrderST(order) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                }
                else
                {
                    if (ordMgr.InsertOrderST(order) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            return 1;
        }


        #region ����farpoint��Ϣ
        private void addDetail(ArrayList al)
        {
            if (this.cmbSeeFlag.Text == "����")
            {
                FS.HISFC.BizProcess.Integrate.Common.ControlParam contrlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                if (this.personHelper == null)
                {
                    this.personHelper = new FS.FrameWork.Public.ObjectHelper(this.interMgr.QueryEmployeeAll());
                }
                this.SetFarpointOutPatientDataShow(al);
            }
            else if (this.cmbSeeFlag.Text == "סԺ")
            {
                FS.HISFC.BizProcess.Integrate.Common.ControlParam contrlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                if (this.personHelper == null)
                {
                    this.personHelper = new FS.FrameWork.Public.ObjectHelper(this.interMgr.QueryEmployeeAll());
                }
                this.SetFarpointInPatientDataShow(al);
            }
        }

        private void addDetailST(ArrayList al)
        {
            if (this.cmbSeeFlag.Text == "����")
            {
                FS.HISFC.BizProcess.Integrate.Common.ControlParam contrlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                if (this.personHelper == null)
                {
                    this.personHelper = new FS.FrameWork.Public.ObjectHelper(this.interMgr.QueryEmployeeAll());
                }
                this.SetFarpointOutPatientSTDataShow(al);
            }
            else if (this.cmbSeeFlag.Text == "סԺ")
            {
                FS.HISFC.BizProcess.Integrate.Common.ControlParam contrlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                if (this.personHelper == null)
                {
                    this.personHelper = new FS.FrameWork.Public.ObjectHelper(this.interMgr.QueryEmployeeAll());
                }
                this.SetFarpointOutPatientSTDataShow(al);
            }
        }

        /// <summary>
        /// ���ﴦ����Ϣ
        /// </summary>
        /// <param name="alOutPatientOrder"></param>
        private void SetFarpointOutPatientDataShow(ArrayList alOutPatientOrder)
        {
            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
            foreach (FS.HISFC.Models.Order.OutPatient.Order Order in alOutPatientOrder)
            {
                this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);
                int row = this.fpSpread1_Sheet1.RowCount - 1;
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.isChoose, false, false);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.isPrine, "δ��ӡ", false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.name, Order.Patient.Name);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.inOutType, "����", false);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.cardNo, Order.Patient.PID.CardNO, false);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.recipeNo, Order.ID, false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.itemName, Order.Item.Name);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.usage, Order.Usage.ID, false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.usage, Order.Usage.Name);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.once_dose, Order.DoseOnce, false);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.dose_unit, Order.DoseUnit, false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.frequse, Order.Frequency.Name);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.days, Order.HerbalQty, false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.recipeDoc, Order.ReciptDoctor.Name);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.recipeDept, (deptMgr.GetDeptmentById(Order.ReciptDept.ID)).Name);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.discardedDose, "", true);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.auditDoc, "", true);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.auditDoc, "");
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.execDoc, "", true);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.execDoc, "");
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.execDate, Order.RegTime, true);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.seeNo, Order.SeeNO);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.isInput, false);

                this.fpSpread1_Sheet1.Cells[row, (Int32)enumColumnName.name].Tag = Order.Patient.ID;
                this.fpSpread1_Sheet1.Cells[row, (Int32)enumColumnName.recipeDoc].Tag = Order.ReciptDoctor.ID;
                this.fpSpread1_Sheet1.Cells[row, (Int32)enumColumnName.itemName].Tag = Order.Item.ID;
                this.fpSpread1_Sheet1.Cells[row, (Int32)enumColumnName.usage].Tag = Order.Usage.ID;
                this.fpSpread1_Sheet1.Cells[row, (Int32)enumColumnName.frequse].Tag = Order.Frequency.ID;
                this.fpSpread1_Sheet1.Cells[row, (Int32)enumColumnName.recipeDept].Tag = Order.ReciptDept.ID;
                //order.Audit_doc_code = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.auditDoc].Tag.ToString();
                //order.Exec_doc_code = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.execDoc].Tag.ToString();
                this.fpSpread1_Sheet1.Cells[row, (Int32)enumColumnName.isInput].Tag = false;
            }
        }

        /// <summary>
        /// �����ѵ��뾫�鷽��Ϣ
        /// </summary>
        /// <param name="alOutPatientOrderST"></param>
        private void SetFarpointOutPatientSTDataShow(ArrayList alOutPatientOrderST)
        {
            if (alOutPatientOrderST == null)
            {
                return;
            }
            foreach (FS.HISFC.Models.Order.OrderST order in alOutPatientOrderST)
            {
                this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);
                int row = this.fpSpread1_Sheet1.RowCount - 1;
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.isChoose,false, false);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.isPrine,order.Is_prine?"�Ѵ�ӡ":"δ��ӡ", false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.name, order.Name);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.inOutType, order.Inouttype=="O"?"����":"סԺ", false);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.cardNo, order.Card_no, false);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.recipeNo, order.Recipe_no, false);
                //this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.itemName, order.Item_code, false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.itemName, order.Item_name);
                //this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.usage, order.Usage_code, false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.usage, order.Usage_name);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.once_dose, order.Once_dose, false);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.dose_unit, order.Dose_unit, false);
                //this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.frequse, order.Fre_code, false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.frequse, order.Fre_name);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.days, order.Days, false);
                //this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.recipeDoc, order.Recipe_doc_code, false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.recipeDoc, order.Recipe_doc_name);
                //this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.recipeDept, order.Recipe_dept_code, false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.recipeDept, order.Recipe_dept_name);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.discardedDose, order.Discarded_dose, false);
                //this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.auditDoc, order.Audit_doc_code, false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.auditDoc, order.Audit_doc_name);
                //this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.execDoc, order.Exec_doc_code, false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.execDoc, order.Exec_doc_name);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.execDate, order.Exec_date, false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.seeNo, order.See_no);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.isInput, true);

                this.fpSpread1_Sheet1.Cells[row, (Int32)enumColumnName.name].Tag = order.Clinic_no;
                this.fpSpread1_Sheet1.Cells[row, (Int32)enumColumnName.recipeDoc].Tag = order.Recipe_doc_code;
                this.fpSpread1_Sheet1.Cells[row, (Int32)enumColumnName.itemName].Tag = order.Item_code;
                this.fpSpread1_Sheet1.Cells[row, (Int32)enumColumnName.usage].Tag = order.Usage_code;
                this.fpSpread1_Sheet1.Cells[row, (Int32)enumColumnName.frequse].Tag = order.Fre_code;
                this.fpSpread1_Sheet1.Cells[row, (Int32)enumColumnName.recipeDept].Tag = order.Recipe_dept_code;
                //order.Audit_doc_code = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.auditDoc].Tag.ToString();
                //order.Exec_doc_code = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.execDoc].Tag.ToString();
                this.fpSpread1_Sheet1.Cells[row, (Int32)enumColumnName.isInput].Tag = true;
            }
        }

        /// <summary>
        /// סԺ������Ϣ
        /// </summary>
        /// <param name="alInPatientOrder"></param>
        private void SetFarpointInPatientDataShow(ArrayList alInPatientOrder)
        {
            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
            foreach (FS.HISFC.Models.Order.Inpatient.Order Order in alInPatientOrder)
            {
                this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);
                int row = this.fpSpread1_Sheet1.RowCount - 1;
                this.fpSpread1_Sheet1.Rows[row].Tag = Order;
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.isChoose, false, false);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.isPrine,"δ��ӡ", false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.name, Order.Patient.Name);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.inOutType, "סԺ", false);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.cardNo, Order.Patient.PID.PatientNO, false);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.recipeNo, Order.ID, false);
                //this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.itemName, Order.Item.ID, false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.itemName, Order.Item.Name);
                //this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.usage, Order.Usage.ID, false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.usage, Order.Usage.Name);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.once_dose, Order.DoseOnce, false);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.dose_unit, Order.DoseUnit, false);
                //this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.frequse, Order.Frequency.ID, false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.frequse, Order.Frequency.Name);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.days, Order.HerbalQty, false);
                //this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.recipeDoc, Order.ReciptDoctor.ID, false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.recipeDoc, Order.ReciptDoctor.Name);
                //this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.recipeDept, Order.ReciptDept.ID, false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.recipeDept, (deptMgr.GetDeptmentById(Order.ReciptDept.ID)).Name);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.discardedDose, "", true);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.auditDoc, "", true);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.auditDoc, "");
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.execDoc, "", true);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.execDoc, "");
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.execDate, "", true);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.seeNo, "");
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.isInput, false);

                this.fpSpread1_Sheet1.Cells[row, (Int32)enumColumnName.name].Tag = Order.Patient.ID;
                this.fpSpread1_Sheet1.Cells[row, (Int32)enumColumnName.recipeDoc].Tag = Order.ReciptDoctor.ID;
                this.fpSpread1_Sheet1.Cells[row, (Int32)enumColumnName.itemName].Tag = Order.Item.ID;
                this.fpSpread1_Sheet1.Cells[row, (Int32)enumColumnName.usage].Tag = Order.Usage.ID;
                this.fpSpread1_Sheet1.Cells[row, (Int32)enumColumnName.frequse].Tag = Order.Frequency.ID;
                this.fpSpread1_Sheet1.Cells[row, (Int32)enumColumnName.recipeDept].Tag = Order.ReciptDept.ID;
                //order.Audit_doc_code = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.auditDoc].Tag.ToString();
                //order.Exec_doc_code = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.execDoc].Tag.ToString();
                this.fpSpread1_Sheet1.Cells[row, (Int32)enumColumnName.isInput].Tag = false;
            }
        }

        /// <summary>
        /// סԺ�ѵ��뾫�鷽��Ϣ
        /// </summary>
        /// <param name="alInPatientOrderST"></param>
        private void SetFarpointInPatientSTDataShow(ArrayList alInPatientOrderST)
        {
            foreach (FS.HISFC.Models.Order.OrderST order in alInPatientOrderST)
            {
                this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);
                int row = this.fpSpread1_Sheet1.RowCount - 1;
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.isChoose, false, false);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.isPrine, order.Is_prine ? "�Ѵ�ӡ" : "δ��ӡ", false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.name, order.Name);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.inOutType, order.Inouttype == "O" ? "����" : "סԺ", false);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.cardNo, order.Card_no, false);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.recipeNo, order.Recipe_no, false);
                //this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.itemName, order.Item_code, false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.itemName, order.Item_name);
                //this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.usage, order.Usage_code, false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.usage, order.Usage_name);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.once_dose, order.Once_dose, false);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.dose_unit, order.Dose_unit, false);
                //this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.frequse, order.Fre_code, false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.frequse, order.Fre_name);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.days, order.Days, false);
                //this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.recipeDoc, order.Recipe_doc_code, false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.recipeDoc, order.Recipe_doc_name);
                //this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.recipeDept, order.Recipe_dept_code, false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.recipeDept, order.Recipe_dept_name);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.discardedDose, order.Discarded_dose, false);
                //this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.auditDoc, order.Audit_doc_code, false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.auditDoc, order.Audit_doc_name);
                //this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.execDoc, order.Exec_doc_code, false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.execDoc, order.Exec_doc_name);
                this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.execDate, order.Exec_date, false);
                this.fpSpread1_Sheet1.SetText(row, (Int32)enumColumnName.seeNo, order.See_no);
                //this.fpSpread1_Sheet1.SetValue(row, (Int32)enumColumnName.isInput, true);

                this.fpSpread1_Sheet1.Cells[row, (Int32)enumColumnName.name].Tag = order.Clinic_no;
                this.fpSpread1_Sheet1.Cells[row, (Int32)enumColumnName.recipeDoc].Tag = order.Recipe_doc_code;
                this.fpSpread1_Sheet1.Cells[row, (Int32)enumColumnName.itemName].Tag = order.Item_code;
                this.fpSpread1_Sheet1.Cells[row, (Int32)enumColumnName.usage].Tag = order.Usage_code;
                this.fpSpread1_Sheet1.Cells[row, (Int32)enumColumnName.frequse].Tag = order.Fre_code;
                this.fpSpread1_Sheet1.Cells[row, (Int32)enumColumnName.recipeDept].Tag = order.Recipe_dept_code;
                this.fpSpread1_Sheet1.Cells[row, (Int32)enumColumnName.isInput].Tag = true;
                //order.Audit_doc_code = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.auditDoc].Tag.ToString();
                //order.Exec_doc_code = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.execDoc].Tag.ToString();
            }
        }

        private void InitFarpoint()
        {
            this.fpSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Cell;
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(Int32)enumColumnName.isChoose].Label = "ѡ��";
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.fpSpread1_Sheet1.Columns[(Int32)enumColumnName.isChoose].CellType = checkBoxCellType;
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(Int32)enumColumnName.isPrine].Label = "�Ƿ��ӡ";
            this.fpSpread1_Sheet1.Columns[(Int32)enumColumnName.isPrine].Width = 0;
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(Int32)enumColumnName.name].Label = "����";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(Int32)enumColumnName.inOutType].Label = "����/סԺ";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(Int32)enumColumnName.cardNo].Label = "�����/סԺ��";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(Int32)enumColumnName.recipeNo].Label = "������";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(Int32)enumColumnName.itemName].Label = "ҽ��";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(Int32)enumColumnName.usage].Label = "�÷�";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(Int32)enumColumnName.once_dose].Label = "����";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(Int32)enumColumnName.dose_unit].Label = "������λ";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(Int32)enumColumnName.frequse].Label = "Ƶ��";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(Int32)enumColumnName.days].Label = "����";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(Int32)enumColumnName.recipeDoc].Label = "����ҽ��";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(Int32)enumColumnName.recipeDept].Label = "��������";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(Int32)enumColumnName.discardedDose].Label = "������";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(Int32)enumColumnName.auditDoc].Label = "���ҽ��";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(Int32)enumColumnName.execDoc].Label="ִ����";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(Int32)enumColumnName.execDate].Label="ִ��ʱ��";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(Int32)enumColumnName.seeNo].Label = "�������";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(Int32)enumColumnName.isInput].Label = "�Ƿ��Ѿ�����";
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(Int32)enumColumnName.isInput].Width = 0;
            this.fpSpread1_Sheet1.ColumnHeader.Columns[(Int32)enumColumnName.batchNo].Label = "ҩƷ����";

            this.fpSpread1_Sheet1.Columns[(Int32)enumColumnName.name].Locked = true;
            this.fpSpread1_Sheet1.Columns[(Int32)enumColumnName.inOutType].Locked = true;
            this.fpSpread1_Sheet1.Columns[(Int32)enumColumnName.cardNo].Locked = true;
            this.fpSpread1_Sheet1.Columns[(Int32)enumColumnName.recipeNo].Locked = true;
            this.fpSpread1_Sheet1.Columns[(Int32)enumColumnName.itemName].Locked = true;
            this.fpSpread1_Sheet1.Columns[(Int32)enumColumnName.usage].Locked = true;
            this.fpSpread1_Sheet1.Columns[(Int32)enumColumnName.once_dose].Locked = true;
            this.fpSpread1_Sheet1.Columns[(Int32)enumColumnName.dose_unit].Locked = true;
            this.fpSpread1_Sheet1.Columns[(Int32)enumColumnName.frequse].Locked = true;
            this.fpSpread1_Sheet1.Columns[(Int32)enumColumnName.days].Locked = true;
            this.fpSpread1_Sheet1.Columns[(Int32)enumColumnName.recipeDoc].Locked = true;
            this.fpSpread1_Sheet1.Columns[(Int32)enumColumnName.recipeDept].Locked = true;
            //this.fpSpread1_Sheet1.Columns[(Int32)enumColumnName.discardedDose].Locked = "������";
            this.fpSpread1_Sheet1.Columns[(Int32)enumColumnName.auditDoc].Locked = true;
            //this.fpSpread1_Sheet1.Columns[(Int32)enumColumnName.execDoc].Locked = true;
            //this.fpSpread1_Sheet1.Columns[(Int32)enumColumnName.execDate].Locked = "ִ��ʱ��";
            this.fpSpread1_Sheet1.Columns[(Int32)enumColumnName.seeNo].Locked = true;
            this.fpSpread1_Sheet1.Columns[(Int32)enumColumnName.isInput].Locked = true;
        }

        private void SetPatientInfo()
        {

            if (this.cmbSeeFlag.Text == "סԺ")
            {
                FS.HISFC.BizLogic.RADT.InPatient inpatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    if (!hsPatient.ContainsKey(this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.cardNo].Text))
                    {
                        FS.HISFC.Models.RADT.PatientInfo pi
                            = inpatientMgr.QueryPatientInfoByInpatientNONew(this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.name].Tag.ToString());
                        hsPatient.Add(this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.cardNo].Text, pi);
                        this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.name].Text=pi.Name;
                        this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.isChoose].Tag = pi;
                    }
                    else
                    {
                        FS.HISFC.Models.RADT.PatientInfo pi
                            =hsPatient[this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.cardNo].Text] as FS.HISFC.Models.RADT.PatientInfo;
                        this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.name].Text = pi.Name;
                        this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.isChoose].Tag = pi;
                    }
                }
            }

            if (this.cmbSeeFlag.Text == "����")
            {
                FS.HISFC.BizProcess.Integrate.RADT radtMgr = new FS.HISFC.BizProcess.Integrate.RADT();
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    if (!hsPatient.ContainsKey(this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.cardNo].Text))
                    {
                        FS.HISFC.Models.RADT.PatientInfo pi
                            = radtMgr.QueryComPatientInfo(this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.cardNo].Text);
                        this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.name].Text = pi.Name;
                        this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.isChoose].Tag = pi;
                    }
                    else
                    {
                        FS.HISFC.Models.RADT.PatientInfo pi
                            = hsPatient[this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.cardNo].Text] as FS.HISFC.Models.RADT.PatientInfo;
                        this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.name].Text = pi.Name;
                        this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.isChoose].Tag = pi;
                    }
                }
            }
        }
    

        private ArrayList GetOrderSTInfo()
        {
            ArrayList alOrderST = new ArrayList();
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                if ((bool)fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.isChoose].Value == true)
                {
                    FS.HISFC.Models.Order.OrderST order = new FS.HISFC.Models.Order.OrderST();
                    order.Is_prine = (this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.isPrine].Text == "�Ѵ�ӡ" ? true : false);
                    order.Name = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.name].Text;
                    order.Clinic_no = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.name].Tag.ToString();
                    order.Inouttype = (this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.inOutType].Text == "����" ? "O" : "I");
                    order.Card_no = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.cardNo].Text;
                    order.Recipe_no = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.recipeNo].Text;
                    order.Item_code = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.itemName].Tag.ToString();
                    order.Item_name = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.itemName].Text;
                    order.Usage_code = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.usage].Tag.ToString();
                    order.Usage_name = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.usage].Text;
                    order.Once_dose
                            = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.once_dose].Text);
                    order.Dose_unit = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.dose_unit].Text;
                    order.Fre_code = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.frequse].Tag.ToString();
                    order.Fre_name = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.frequse].Text;
                    order.Days = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.days].Text);
                    order.Recipe_doc_code = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.recipeDoc].Tag.ToString();
                    order.Recipe_doc_name = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.recipeDoc].Text;
                    order.Recipe_dept_code = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.recipeDept].Tag.ToString();
                    order.Recipe_dept_name = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.recipeDept].Text;
                    order.Discarded_dose
                            = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.discardedDose].Text);
                    //order.Audit_doc_code = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.auditDoc].Tag.ToString();
                    order.Audit_doc_name = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.auditDoc].Text;
                    //order.Exec_doc_code = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.execDoc].Tag.ToString();
                    order.Exec_doc_name = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.execDoc].Text;
                    order.Exec_date
                            = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.execDate].Text);
                    order.See_no = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.seeNo].Text;
                    order.IsInput = (bool)this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.isInput].Tag;
                    order.Ext_memo1 = this.fpSpread1_Sheet1.Cells[i, (Int32)enumColumnName.batchNo].Text;

                    FS.HISFC.Models.Order.Inpatient.Order Order = this.fpSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                    //����ʱ��
                    order.Ext_memo2 = Order.MOTime.ToString();
                    //{FE3599DC-8D27-43f1-A847-B65FC6C6BF3B}
                    order.Memo = Order.Qty + Order.Unit;

                    alOrderST.Add(order);
                }
                
            }
            return alOrderST;
        }

        #endregion

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.Q.GetHashCode())
            {
                this.Query();

                return true;
            }
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.X.GetHashCode())
            {
                this.FindForm().Close();
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                this.FindForm().Close();
            }
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.P.GetHashCode())
            {
                this.fpSpread1.PrintSheet(0);
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Query();
                this.QueryST();
            }
        }

        private void txtInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Query();
                this.QueryST();
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Query();
                this.QueryST();
            }
        }

        private void fpSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column == (Int32)enumColumnName.isChoose)
            {
                if ((bool)fpSpread1_Sheet1.Cells[e.Row, e.Column].Value == false)
                {
                    this.fpSpread1_Sheet1.SetValue(e.Row, (Int32)enumColumnName.isChoose, true);
                }
                else
                {
                    this.fpSpread1_Sheet1.SetValue(e.Row, (Int32)enumColumnName.isChoose, false);
                }
            }
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return base.OnQuery(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            //this.fpSpread1.PrintSheet(0);

            return base.OnPrint(sender, neuObject);
        }
        public override int Export(object sender, object neuObject)
        {
            if (this.fpSpread1.Export() == 1)
            {
                MessageBox.Show("�����ɹ�");
            }
            return base.Export(sender, neuObject);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            if (this.Save() < 0)
            {
                MessageBox.Show(this.ordMgr.Err);
            }
            return base.OnSave(sender, neuObject);
        }

        public override int Print(object sender, object neuObject)
        {
            this.Print();
            return base.Print(sender, neuObject);
        }

        private void dateEnd_ValueChanged(object sender, EventArgs e)
        {

        }

        private enum enumColumnName
        {
            /// <summary>
            /// ѡ��
            /// </summary>
            isChoose, 

            /// <summary>
            /// �Ƿ��ӡ
            /// </summary>
            isPrine,

            /// <summary>
            /// ��������
            /// </summary>
            name,

            /// <summary>
            /// ����/סԺ
            /// </summary>
            inOutType,

            /// <summary>
            /// �����/סԺ��
            /// </summary>
            cardNo,

            /// <summary>
            /// ������
            /// </summary>
            recipeNo,

            /// <summary>
            /// ҽ��
            /// </summary>
            itemName,

            /// <summary>
            /// �÷�
            /// </summary>
            usage,

            /// <summary>
            /// ����
            /// </summary>
            once_dose,

            /// <summary>
            /// ������λ
            /// </summary>
            dose_unit,

            /// <summary>
            /// Ƶ��
            /// </summary>
            frequse,

            /// <summary>
            /// ����
            /// </summary>
            days,

            /// <summary>
            /// ����ҽ��
            /// </summary>
            recipeDoc,

            /// <summary>
            /// ��������
            /// </summary>
            recipeDept,

            /// <summary>
            /// ������
            /// </summary>
            discardedDose,

            /// <summary>
            /// ���ҽ��
            /// </summary>
            auditDoc,

            /// <summary>
            /// ִ����
            /// </summary>
            execDoc,

            /// <summary>
            /// ִ��ʱ��
            /// </summary>
            execDate,

            /// <summary>
            /// ������ţ������ã�
            /// </summary>
            seeNo,

            /// <summary>
            /// �Ƿ��Ѿ�����
            /// </summary>
            isInput,

            /// <summary>
            /// ҩƷ����
            /// </summary>
            batchNo
        }
    }
}