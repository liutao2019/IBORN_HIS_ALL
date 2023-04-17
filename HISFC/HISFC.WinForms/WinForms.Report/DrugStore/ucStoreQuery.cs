using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;
using FS.FrameWork.Function;

namespace FS.WinForms.Report.DrugStore
{
    /// <summary>
    /// ����ѯ ��ʱ��ѯ����
    /// </summary>
    public partial class ucStoreQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 
        /// </summary>
        public ucStoreQuery()
        {
            InitializeComponent();
        }

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();


        private HISFC.Components.Pharmacy.Out.ucPhaOut phaOutManager = null;

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            return base.OnInit(sender, neuObject, param);
        }

        public override int Export(object sender, object neuObject)
        {
            if (this.neuSpread1.Export() == 1)
            {
                MessageBox.Show(Language.Msg("�����ɹ�"));
            }

            return base.Export(sender, neuObject);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();

            return base.OnQuery(sender, neuObject);
        }

        #endregion

        #region �����

        /// <summary>
        /// ���Ұ�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ��ʾ����ҩƷ������Sql
        /// </summary>
        private string sql1 = "";

        /// <summary>
        /// ��ʾָ��ҩƷ������Sql
        /// </summary>
        private string sql2 = "";

        /// <summary>
        /// ��ʾ��ϸ��Ϣ
        /// </summary>
        private string sql3 = "";

        /// <summary>
        /// ������ͼ
        /// </summary>
        private System.Data.DataView dv = null;

        #endregion

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Init()
        {
            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
            List<FS.HISFC.Models.Pharmacy.Item> itemList = itemManager.QueryItemList(true);
            //this.cmbDrug.AddItems(new ArrayList(itemList.ToArray()));
            

            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList alDept = deptManager.GetDeptmentAll();
            this.deptHelper = new FS.FrameWork.Public.ObjectHelper(alDept);

            #region ����Sql

            this.sql1 = @"
select s.trade_name ҩƷ����,s.specs ���, 
        s.pack_unit ��λ,
       s.spell_code ƴ����,s.wb_code �����,s.custom_code �Զ�����,
     s.DRUG_CODE ҩƷ����, s.valid_state ͣ��
from  pha_com_baseinfo s
where  s.VALID_STATE = '1'";

//            this.sql1 = @"
//select s.trade_name ҩƷ����,s.specs ���,t.drug_dept_code ������,
//       round(t.store_sum / t.pack_qty,2) �����,s.pack_unit ��λ,
//       s.spell_code ƴ����,s.wb_code �����,s.custom_code �Զ�����,
//	   t.drug_code ҩƷ����,t.drug_dept_code ������,t.valid_state ͣ��
//from   pha_com_stockinfo t,pha_com_baseinfo s
//where  t.drug_code = s.drug_code";

//            this.sql2 = @"
//select s.trade_name ҩƷ����,s.specs ���,t.drug_dept_code ������,
//       round(t.store_sum / t.pack_qty,2) �����,s.pack_unit ��λ,
//       s.spell_code ƴ����,s.wb_code �����,s.custom_code �Զ�����,
//	   t.drug_code ҩƷ����,t.drug_dept_code ������,t.valid_state ͣ��
//from   pha_com_stockinfo t,pha_com_baseinfo s
//where  t.drug_code = '{0}'
//and    t.drug_code = s.drug_code";

//            this.sql3 = @"select t.group_code ����,t.batch_no ����,s.trade_name ҩƷ����,s.specs ���,
//       round(t.store_sum / t.pack_qty,2) �����,s.pack_unit ��λ,
//       s.spell_code ƴ����,s.wb_code �����,s.custom_code �Զ�����,
//       t.drug_code ҩƷ����,t.drug_dept_code ������
//from   pha_com_storage t,pha_com_baseinfo s
//where  t.drug_code = '{0}'
//and    t.drug_dept_code = '{1}'
//and    t.drug_code = s.drug_code
//";

            #endregion

            this.neuSpread1_Sheet1.DefaultStyle.Locked = true;
            this.neuSpread1_Sheet2.DefaultStyle.Locked = true;
        }

        /// <summary>
        /// ��ʽ�� 
        /// </summary>
        private void SetFormat()
        {
            //this.neuSpread1_Sheet1.Columns[5].Visible = false;
            this.neuSpread1_Sheet1.Columns[6].Visible = false;
            this.neuSpread1_Sheet1.Columns[7].Visible = false;

            //this.neuSpread1_Sheet1.Columns[8].Visible = false;
            //this.neuSpread1_Sheet1.Columns[9].Visible = false;

            //this.neuSpread1_Sheet1.Columns[10].Visible = false;

            //RefreshFpFlag(this.neuSpread1_Sheet1);
        }

        /// <summary>
        /// ˢ����ʾҩƷ��������ʾͣ�ñ�־
        /// </summary>
        //private void RefreshFpFlag(FarPoint.Win.Spread.SheetView sheet)
        //{
        //    if (sheet.RowCount > 0)
        //    {
        //        for (int i = 0; i < sheet.RowCount; i++)
        //        {
        //            if (sheet.Cells[i, 10].Text.ToString().Trim() == "1")
        //                sheet.Cells[i, 2].BackColor = System.Drawing.Color.Red;
        //            else
        //                sheet.Cells[i, 2].BackColor = System.Drawing.Color.White;
        //        }
        //    }
        //}

        /// <summary>
        /// ����ĳ����ɫ
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="column"></param>
        /// <param name="valu"></param>
        private void SetColumnsColore(FarPoint.Win.Spread.SheetView sheet, int column, Color valu)
        {
            sheet.Columns[column].BackColor = valu;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Query()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڽ��в�ѯ...���Ժ�");
            Application.DoEvents();

            string exeSql = "";
            if (this.ckAll.Checked)			//ȫ��ҩƷ
            {
                exeSql = this.sql1;
            }
            else
            {
                if (this.cmbDrug.Tag != null && this.cmbDrug.Tag.ToString() != "")
                {
                    exeSql = string.Format(this.sql2, this.cmbDrug.Tag.ToString());
                }
            }

            FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();

            DataSet ds = new DataSet();

            if (dataManager.ExecQuery(exeSql, ref ds) == -1)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(Language.Msg("ִ��Sql��䷢������" + dataManager.Err));
                return;
            }

            //if (ds != null && ds.Tables.Count > 0)
            //{
            //    foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
            //    {
            //        if (ds.Tables[0].Columns.Contains("������"))
            //            dr["������"] = this.deptHelper.GetName(dr["������"].ToString());
            //    }
            //}

            this.dv = new DataView(ds.Tables[0]);

            this.neuSpread1_Sheet1.DataSource = dv;

            this.SetFormat();

            if (this.neuSpread1.ActiveSheet != this.neuSpread1_Sheet1)
            {
                this.neuSpread1.ActiveSheet = this.neuSpread1_Sheet1;
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Init();

            base.OnLoad(e);
        }

        private void txtFilter_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (this.dv != null)
                {
                    string rowFilter = string.Format("ƴ���� like '%{0}%' or ҩƷ���� like '%{0}%' or ����� like '%{0}%' or �Զ����� like '%{0}%'", this.txtFilter.Text);

                    this.dv.RowFilter = rowFilter;

                    //RefreshFpFlag(this.neuSpread1_Sheet1);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string drugCode = string.Empty;

            //if (this.phaOutManager.DeptInfo.Memo == "PI")
            //{
                //drugCode = this.phaOutManager.FpSheetView.Cells[e.Row, (int)ColumnSet.ColDrugNO].Text;
            drugCode = this.neuSpread1_Sheet1.Cells[e.Row, 6].Text;// this.phaOutManager.FpSheetView.Cells[e.Row, 6].Text;   // (int)ColumnSet.ColDrugNO].Text;
            //using (LiaoCheng.HISFC.Components.Pharmacy.frmEveryStore frm = new LiaoCheng.HISFC.Components.Pharmacy.frmEveryStore())
            //{
            //    frm.DrugCode = drugCode;
            //    frm.ShowDialog();
            //}

            using (FS.HISFC.Components.Pharmacy.Out.frmEveryStore frm = new FS.HISFC.Components.Pharmacy.Out.frmEveryStore())
            {
                frm.DrugCode = drugCode;
                frm.ShowDialog();
            }
            //}

            //if (this.neuSpread1.ActiveSheet == this.neuSpread1_Sheet2)
            //{
            //    this.neuSpread1.ActiveSheet = this.neuSpread1_Sheet1;
            //}
            //else
            //{
            //    string drugCode = this.neuSpread1_Sheet1.Cells[e.Row, 8].Text;
            //    string deptCode = this.neuSpread1_Sheet1.Cells[e.Row, 9].Text;

            //    FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();
            //    DataSet ds = new DataSet();

            //    string exeSql = string.Format(this.sql3, drugCode, deptCode);

            //    if (dataManager.ExecQuery(exeSql, ref ds) == -1)
            //    {
            //        MessageBox.Show(Language.Msg(dataManager.Err));
            //    }

            //    this.neuSpread1_Sheet2.DataSource = ds;

            //    this.neuSpread1_Sheet2.Columns[6].Visible = false;
            //    this.neuSpread1_Sheet2.Columns[7].Visible = false;
            //    this.neuSpread1_Sheet2.Columns[8].Visible = false;
            //    this.neuSpread1_Sheet2.Columns[9].Visible = false;
            //    this.neuSpread1_Sheet2.Columns[10].Visible = false;

            //    this.neuSpread1.ActiveSheet = this.neuSpread1_Sheet2;

            //}
        }
    }
}
