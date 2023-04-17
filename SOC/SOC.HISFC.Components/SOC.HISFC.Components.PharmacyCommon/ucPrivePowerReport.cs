using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.PharmacyCommon
{
    public partial class ucPrivePowerReport : BaseReport
    {
        /// <summary>
        /// ucPrivePowerReport<br></br>
        /// [��������: ucPrivePowerReport����Ȩ�ޱ�������ҩƷ������]<br></br>
        /// [�� �� ��: zengft]<br></br>
        /// [����ʱ��: 2008-9-6]<br></br>
        /// <�޸ļ�¼ 
        ///		�޸���='' 
        ///		�޸�ʱ��='yyyy-mm-dd' 
        ///		�޸�Ŀ��=''
        ///		�޸�����=''
        ///  />
        /// </summary>
        public ucPrivePowerReport()
        {
            InitializeComponent();

            this.ntxtBillNO.KeyPress += new KeyPressEventHandler(ntxtBillNO_KeyPress);
        }


      

        /// <summary>
        /// �Զ������
        /// </summary>
        //private FS.HISFC.Models.Pharmacy.EnumDrugStencil drugStencilType = FS.HISFC.Models.Pharmacy.EnumDrugStencil.Common1;

    
        /// <summary>
        /// ��ӡʱ�Ƿ���ʾ���򵥾ݣ��Ǳ����ʽ��
        /// </summary>
        //private int rePrintBillNOColIndex = -1;

        /// <summary>
        /// ��������
        /// </summary>
        //private FS.HISFC.PharmacyInterface.BillType billType;

        /// <summary>
        /// ��������
        /// </summary>
        //[Category("��չ��Ϣ"), Description("��������"), Browsable(true)]
        //public FS.HISFC.Integrate.PharmacyInterface.BillType BillType
        //{
        //    get { return billType; }
        //    set { billType = value; }
        //}

        /// <summary>
        /// ��ӡʱ�Ƿ���ʾ���򵥾ݣ��Ǳ����ʽ��
        /// </summary>
        //[Category("��չ��Ϣ"), Description("��ӡʱ�Ƿ���ʾ���򵥾ݣ��Ǳ����ʽ��"), Browsable(true)]
        //public int RePrintBillNOColIndex
        //{
        //    get { return rePrintBillNOColIndex; }
        //    set { rePrintBillNOColIndex = value; }
        //}



        /// <summary>
        /// �Զ������
        /// </summary>
        //[Category("��չ��Ϣ"), Description("�Զ������"), Browsable(true)]
        //public FS.HISFC.Models.Pharmacy.EnumDrugStencil DrugStencilType
        //{
        //    get { return drugStencilType; }
        //    set { drugStencilType = value; }
        //}

       

        /// <summary>
        /// 
        /// </summary>
        [Category("��չ��Ϣ"), Description("���ع�����˾���߿���"), Browsable(true)]
        public myDeptType ShowTypeName
        {
            get
            {
                if (this.nlblDept.Text.Contains(myDeptType.��˾.ToString()))
                {
                    return myDeptType.��˾;
                }
                else if (this.nlblDept.Text.Contains( myDeptType.����.ToString()))
                {
                    return myDeptType.����;
                }
                else if (this.nlblDept.Text.Contains( myDeptType.��λ.ToString()))
                {
                    return myDeptType.��λ;
                }
                return myDeptType.����;
            }
            set
            {
                this.nlblDept.Text = value.ToString() + "��";
            }
        }


        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void InitData()
        {
            FS.SOC.HISFC.BizLogic.Pharmacy.InOut itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();
            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
            FS.HISFC.BizLogic.Manager.Constant  conMgr = new FS.HISFC.BizLogic.Manager.Constant ();
            FS.SOC.HISFC.BizLogic.Pharmacy.Constant pconMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Constant();
            try
            {
                List<FS.HISFC.Models.Pharmacy.Item> alDrug = itemMgr.QueryItemList(true);
                System.Collections.ArrayList alObject = new System.Collections.ArrayList();
                foreach (FS.HISFC.Models.Pharmacy.Item item in alDrug)
                {
                    FS.HISFC.Models.Base.Spell neuObject = item as FS.HISFC.Models.Base.Spell;
                    neuObject.ID = item.ID;
                    neuObject.Name = item.Name;;
                    neuObject.Memo = item.Specs;
                    alObject.Add(neuObject);
                }
                this.ncmbDrug.AddItems(alObject);
                if (ShowTypeName == myDeptType.����)
                {
                    this.ncmbDept.AddItems(deptMgr.GetDeptmentAll());
                }
                else if (ShowTypeName == myDeptType.��˾)
                {
                    this.ncmbDept.AddItems(pconMgr.QueryCompany("1"));
                }
                else if (ShowTypeName == myDeptType.��λ)
                {
                    ArrayList alDept = deptMgr.GetDeptmentAll();
                    alDept.AddRange(pconMgr.QueryCompany("1"));
                    alDept.AddRange(pconMgr.QueryCompany("2"));
                    this.ncmbDept.AddItems(alDept);
                }
                else
                {
                    this.nlblDept.Enabled = false;
                    this.ncmbDept.Enabled = false;
                }

                this.ncmbDrugType.AddItems(conMgr.GetAllList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        /// <summary>
        /// ��ȡ��ѯ����
        /// </summary>
        /// <returns></returns>
        private new string[] GetQueryConditions()
        {
            if (!this.IsUseCustomType)
            {
                if (this.IsDeptAsCondition && this.IsTimeAsCondition)
                {
                    string[] parm = { "", "", "", "", "", "", "", "" };
                    if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                    {
                        parm[0] = this.cmbDept.Tag.ToString();
                    }
                    if (string.IsNullOrEmpty(this.ntxtBillNO.Text))
                    {
                        parm[1] = this.dtStart.Value.ToString();
                        parm[2] = this.dtEnd.Value.ToString();
                    }
                    else
                    {
                        parm[1] = this.dtStart.MinDate.ToString();
                        parm[2] = this.dtEnd.MaxDate.ToString();
                    }
                    parm[3] = this.GetParm()[0];
                    parm[4] = this.GetParm()[1];
                    parm[5] = this.GetParm()[2];
                    parm[6] = this.GetParm()[3];

                    return parm;
                }
                if (!this.IsDeptAsCondition && this.IsTimeAsCondition)
                {
                    string[] parm = { "", "", "", "", "", "", "" };
                    if (string.IsNullOrEmpty(this.ntxtBillNO.Text))
                    {
                        parm[0] = this.dtStart.Value.ToString();
                        parm[1] = this.dtEnd.Value.ToString();
                    }
                    else
                    {
                        parm[0] = this.dtStart.MinDate.ToString();
                        parm[1] = this.dtEnd.MaxDate.ToString();
                    }
                    parm[2] = this.GetParm()[0];
                    parm[3] = this.GetParm()[1];
                    parm[4] = this.GetParm()[2];
                    parm[5] = this.GetParm()[3];
                    return parm;
                }
                if (this.IsDeptAsCondition && !this.IsTimeAsCondition)
                {
                    string[] parm = { "", "", "", "", "", "" };
                    if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                    {
                        parm[0] = this.cmbDept.Tag.ToString();
                    }
                    parm[1] = this.GetParm()[0];
                    parm[2] = this.GetParm()[1];
                    parm[3] = this.GetParm()[2];
                    parm[4] = this.GetParm()[3];
                    return parm;
                }
            }
            else
            {
                if (this.IsDeptAsCondition && this.IsTimeAsCondition)
                {
                    string[] parm = { "", "", "", "AAAA", "", "", "", "" };
                    if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                    {
                        parm[0] = this.cmbDept.Tag.ToString();
                    }
                    if (string.IsNullOrEmpty(this.ntxtBillNO.Text))
                    {
                        parm[1] = this.dtStart.Value.ToString();
                        parm[2] = this.dtEnd.Value.ToString();
                    }
                    else
                    {
                        parm[1] = this.dtStart.MinDate.ToString();
                        parm[2] = this.dtEnd.MaxDate.ToString();
                    }
                    if (this.cmbCustomType.Tag != null && !string.IsNullOrEmpty(this.cmbCustomType.Tag.ToString()) && !string.IsNullOrEmpty(this.cmbCustomType.Text.Trim()))
                    {
                        parm[3] = this.cmbCustomType.Tag.ToString();
                    }
                    parm[4] = this.GetParm()[0];
                    parm[5] = this.GetParm()[1];
                    parm[6] = this.GetParm()[2];
                    parm[7] = this.GetParm()[3];

                    return parm;
                }
                if (!this.IsDeptAsCondition && this.IsTimeAsCondition)
                {
                    string[] parm = { "", "", "AAAA", "", "", "", "" };
                    if (string.IsNullOrEmpty(this.ntxtBillNO.Text))
                    {
                        parm[0] = this.dtStart.Value.ToString();
                        parm[1] = this.dtEnd.Value.ToString();
                    }
                    else
                    {
                        parm[0] = this.dtStart.MinDate.ToString();
                        parm[1] = this.dtEnd.MaxDate.ToString();
                    }
                    if (this.cmbCustomType.Tag != null && !string.IsNullOrEmpty(this.cmbCustomType.Tag.ToString()) && !string.IsNullOrEmpty(this.cmbCustomType.Text.Trim()))
                    {
                        parm[2] = this.cmbCustomType.Tag.ToString();
                    }
                    parm[3] = this.GetParm()[0];
                    parm[4] = this.GetParm()[1];
                    parm[5] = this.GetParm()[2];
                    parm[6] = this.GetParm()[3];
                    return parm;
                }
                if (this.IsDeptAsCondition && !this.IsTimeAsCondition)
                {
                    string[] parm = { "", "AAAA", "", "", "", "" };
                    if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                    {
                        parm[0] = this.cmbDept.Tag.ToString();
                    }
                    if (this.cmbCustomType.Tag != null && !string.IsNullOrEmpty(this.cmbCustomType.Tag.ToString()) && !string.IsNullOrEmpty(this.cmbCustomType.Text.Trim()))
                    {
                        parm[1] = this.cmbCustomType.Tag.ToString();
                    }
                    parm[2] = this.GetParm()[0];
                    parm[3] = this.GetParm()[1];
                    parm[4] = this.GetParm()[2];
                    parm[5] = this.GetParm()[3];
                    return parm;
                }
            }
            string[] parmNull = { "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null" };
            return parmNull;
        }

        /// <summary>
        /// ��ȡ������ѯ����
        /// </summary>
        /// <returns></returns>
        private string[] GetParm()
        {
            string billNO = this.ntxtBillNO.Text.Trim();
            if (string.IsNullOrEmpty(billNO))
            {
                billNO = "AAAA";
            }

            string drugNO = "AAAA";
            if (this.ncmbDrug.Tag != null && !string.IsNullOrEmpty(this.ncmbDrug.Tag.ToString()) && !string.IsNullOrEmpty(this.ncmbDrug.Text.Trim()))
            {
                drugNO = this.ncmbDrug.Tag.ToString();
            }
            string deptNO = "AAAA";
            if (this.ncmbDept.Tag != null && !string.IsNullOrEmpty(this.ncmbDept.Tag.ToString()) && !string.IsNullOrEmpty(this.ncmbDept.Text.Trim()))
            {
                deptNO = this.ncmbDept.Tag.ToString();
            }
            string drugType = "AAAA";
            if (this.ncmbDrugType.Tag != null && !string.IsNullOrEmpty(this.ncmbDrugType.Tag.ToString()) && !string.IsNullOrEmpty(this.ncmbDrugType.Text.Trim()))
            {
                drugType = this.ncmbDrugType.Tag.ToString();
            }
          
            return new string[] { billNO, drugNO, deptNO, drugType };
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryAdditionConditions = this.GetQueryConditions();
            this.IsNeedAdditionConditions = true;
            return base.OnQuery(sender, neuObject);
        }

        public override int Print(object sender, object neuObject)
        {
            //try
            //{
            //    //if (this.RePrintBillNOColIndex > -1 && this.fpSpread1_Sheet1.RowCount > 0)
            //    if (this.fpSpread1_Sheet1.RowCount > 0)
            //    {
            //        System.Windows.Forms.DialogResult dr = MessageBox.Show(this, "�����ṩ���񵥾ݴ�ӡ�Ͳ�ѯ���ݴ�ӡ����ѡ��\n�ǣ���ӡ�Ͻ�����ĵ���\n�񣺴�ӡ��ʾ����\nȡ��������ӡ", "��ʾ>>", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            //        if (this.fpSpread1_Sheet1.ActiveRowIndex < 0)
            //        {
            //            this.fpSpread1_Sheet1.ActiveRowIndex = 0;
            //        }
            //        //string billNO = this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, this.rePrintBillNOColIndex].Text;
            //        //if (string.IsNullOrEmpty(billNO))
            //        //{
            //        //    this.ShowBalloonTip(10, "��ܰ��ʾ", "��ӡʧ�ܣ���" + (this.fpSpread1_Sheet1.ActiveRowIndex + 1).ToString() + "��" + this.rePrintBillNOColIndex.ToString() + "��" + "�Ҳ�������");
            //        //    return -1;
            //        //}
            //        if (dr == DialogResult.Yes)
            //        {
            //            FS.SOC.HISFC.BizLogic.Pharmacy.InOut itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();

            //            System.Collections.ArrayList alPrintData = new System.Collections.ArrayList();

            //            switch (this.billType)
            //            {
            //                case FS.HISFC.Integrate.PharmacyInterface.BillType.Check:
            //                    FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
            //                    FS.HISFC.Models.Base.Department d = deptMgr.GetDeptmentById(this.cmbDept.Tag.ToString());
            //                    if (d.DeptType.ID.ToString() == "PI")
            //                    {
            //                        //DialogResult dialogr = MessageBox.Show(this, "�Ƿ��ӡ��ϸ����", "��ʾ>>", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            //                        //if (dialogr == DialogResult.Yes)
            //                        {
            //                            alPrintData = itemMgr.QueryCheckDetailResult(this.cmbDept.Tag.ToString(), billNO);
            //                        }
            //                        //else
            //                        //{
            //                        //    alPrintData = itemMgr.QueryCheckDetailByCheckCode(this.cmbDept.Tag.ToString(), billNO);
            //                        //}
            //                    }
            //                    else
            //                    {
            //                        alPrintData = itemMgr.QueryCheckDetailByCheckCode(this.cmbDept.Tag.ToString(), billNO);
            //                    }
            //                    break;
            //                case FS.HISFC.Integrate.PharmacyInterface.BillType.Input:
            //                    alPrintData = itemMgr.QueryInputInfoByListID(this.cmbDept.Tag.ToString(), billNO, "AAAA", "AAAA");
            //                    break;
            //                case FS.HISFC.Integrate.PharmacyInterface.BillType.Output:
            //                    alPrintData = itemMgr.QueryOutputInfo(this.cmbDept.Tag.ToString(), billNO, "A");
            //                    break;
            //            }

            //            Function.Base.PrintBill(alPrintData, this.billType);
            //            return 1;
            //        }
            //        else if (dr == DialogResult.Cancel)
            //        {
            //            return 0;
            //        }
            //    }
            //}
            //catch(Exception ex)
            //{
            //    this.ShowBalloonTip(10, "��ʾ��", "��ӡʧ��" + ex.Message);
            //}
            return base.Print(sender, neuObject);
        }

        /// <summary>
        /// ���ش���
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            //�����ʱ����ѯ
            if (DesignMode)
            {
                return;
            }
            this.QueryDataWhenInit = false;


            this.InitData();
            base.OnLoad(e);
            if (this.cmbDept.alItems != null && this.cmbDept.alItems.Count > 0)
            {
                this.cmbDept.SelectedIndex = 0;
                this.cmbDept.Tag = (this.cmbDept.alItems[0] as FS.FrameWork.Models.NeuObject).ID;
            }

        }

        /// <summary>
        /// ���ð�ť�����ò�ѯʱ��Ϊ�½�ʱ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int SetPrint(object sender, object neuObject)
        {
            //if (!this.IsUseMonthStoreTime)
            //{
            //    if (!string.IsNullOrEmpty(this.getMonthStoreTimeSQL))
            //    {
            //        DataSet ds = new DataSet();
            //        FS.SOC.HISFC.BizLogic.Pharmacy.InOut itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();
            //        if (itemMgr.ExecQuery(string.Format(this.getMonthStoreTimeSQL,(this.cmbDept.Tag==null?"":this.cmbDept.Tag.ToString())), ref ds) == -1)
            //        {
            //            this.ShowBalloonTip(10, "����", "��ȡ�½�ʱ��η�������");
            //            this.ncmbTime.Visible = false;
            //        }
            //        try
            //        {
            //            FarPoint.Win.Spread.SheetView sv = new FarPoint.Win.Spread.SheetView();
            //            FarPoint.Win.Spread.FpSpread fp = new FarPoint.Win.Spread.FpSpread();
            //            fp.Sheets.Add(sv);
            //            sv.DataSource = ds;
            //            fp.SelectionBlockOptions = FarPoint.Win.Spread.SelectionBlockOptions.Rows;
            //            sv.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            //            fp.CellDoubleClick -= new FarPoint.Win.Spread.CellClickEventHandler(fp_CellDoubleClick);
            //            fp.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fp_CellDoubleClick);
            //            fp.Dock = DockStyle.Fill;
            //            Form form = new Form();
            //            //form.ControlBox = false;
            //            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            //            form.Text = "ʱ�����ã���˫��ѡ��ʱ��";
            //            form.Height = 300;
            //            sv.Columns[0].Width = 348F;
            //            sv.ColumnHeader.Visible = false;
            //            sv.RowHeader.Visible = false;
            //            form.Width = 350;
            //            form.StartPosition = FormStartPosition.CenterScreen;
            //            form.Show(this.FindForm());
            //            form.Controls.Add(fp);
            //        }
            //        catch
            //        { }

            //    }
            //}
            return base.SetPrint(sender, neuObject);
        }

        //void fp_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        //{

        //    //string[] times = ((FarPoint.Win.Spread.SheetView)sender).Cells[e.Row, e.Column].Text.Split('��', '|', '��');
        //    string[] times = ((FarPoint.Win.Spread.FpSpread)sender).Sheets[0].Cells[e.Row, 0].Text.Split('��', '|', '��');

        //    try
        //    {
        //        this.dtStart.Value = FS.FrameWork.Function.NConvert.ToDateTime(times[0].Trim());
        //        this.dtEnd.Value = FS.FrameWork.Function.NConvert.ToDateTime(times[1].Trim());
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ʱ���ʽ����" + this.ncmbTime.SelectedText + "\n��ȷ��ʽ��yyyy-MM-dd hh24:mm:ss �� yyyy-MM-dd hh24:mm:ss");
        //    }
            
        //    ((FarPoint.Win.Spread.FpSpread)sender).FindForm().Close();
        //}
       
       


        void ntxtBillNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.ncmbDrug.Select();
                this.ncmbDrug.Focus();
            }
        }
        
        /// <summary>
        /// ��˾����
        /// </summary>
        public enum myDeptType
        {
            ����,
            ��˾,
            ��λ,
            ����
        }
    }
}
