using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.NFC.Management;
using Neusoft.NFC.Function;

namespace Neusoft.UFC.Material
{
    /// <summary>
    /// ����ѯ ��ʱ��ѯ����
    /// </summary>
    public partial class ucStoreQuery : Neusoft.NFC.Interface.Controls.ucBaseControl
    {
        /// <summary>
        /// 
        /// </summary>
        public ucStoreQuery()
        {
            InitializeComponent();
        }

        #region ������

        private Neusoft.NFC.Interface.Forms.ToolBarService toolBarService = new Neusoft.NFC.Interface.Forms.ToolBarService();

        protected override Neusoft.NFC.Interface.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
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
        private Neusoft.NFC.Public.ObjectHelper deptHelper = new Neusoft.NFC.Public.ObjectHelper();

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
       // private string sql3 = "";

        /// <summary>
        /// ������ͼ
        /// </summary>
        private System.Data.DataView dv = null;
        
        /// <summary>
        /// ����Ա���ұ���
        /// </summary>
        private  string stroeageCode = string.Empty;

        #endregion

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Init()
        {
           // Neusoft.HISFC.Management.Pharmacy.Item itemManager = new Neusoft.HISFC.Management.Pharmacy.Item();
           // List<Neusoft.HISFC.Object.Pharmacy.Item> itemList = itemManager.QueryItemList(true);
            //this.cmbDrug.AddItems(new ArrayList(itemList.ToArray()));


            Neusoft.HISFC.Management.Manager.Department deptManager = new Neusoft.HISFC.Management.Manager.Department();
            
            ArrayList alDept = deptManager.GetDeptmentAll();
            this.deptHelper = new Neusoft.NFC.Public.ObjectHelper(alDept);
           
            string operCode = string.Empty;
            Neusoft.NFC.Management.DataBaseManger data = new Neusoft.NFC.Management.DataBaseManger();

            operCode = ((Neusoft.HISFC.Object.Base.Employee)data.Operator).ID;
            
            //ȡ�ò���Ա���ڿ��Ҽ���
            stroeageCode = ((Neusoft.HISFC.Object.Base.Employee)data.Operator).Dept.ID;

            #region ����Sql

            //            this.sql1 = @"
            //select s.trade_name ҩƷ����,s.specs ���,t.drug_dept_code ������,
            //       round(t.store_sum / t.pack_qty,2) �����,s.pack_unit ��λ,
            //       s.spell_code ƴ����,s.wb_code �����,s.custom_code �Զ�����,
            //	   t.drug_code ҩƷ����,t.drug_dept_code ������,t.valid_state ͣ��
            //from   pha_com_stockinfo t,pha_com_baseinfo s
            //where  t.drug_code = s.drug_code";
            //and t.storage_code= '{stroeageCode}'
            this.sql1 = @"
           select s.item_name ��Ʒ����,s.specs ���,t.store_num ���,t.price ����,t.stat_unit ��װ��λ,t.store_cost �����,t.valid_state ��Ч��־,s.item_code,s.kind_code,s.spell_code ƴ����,t.place_code
           from  log_mat_baseinfo s,log_mat_stock t
           where t.item_code=s.item_code
            and t.storage_code= '{0}'"
                ;
            this.sql2 = @"
         select s.item_name ��Ʒ����,s.item_code,s.kind_code,s.spell_code ƴ����,s.specs ���,t.store_num ���,t.price ����,t.stat_unit ��װ��λ,t.store_cost �����,,t.valid_state ��Ч��־s.item_code,s.kind_code,s.spell_code ƴ����,t.place_code
         from  log_mat_baseinfo s,log_mat_stock t
         where t.item_code=s.item_code
         and t.storage_code= '{0}'
         and t.item_code = '{1}'";

            #endregion

            this.neuSpread1_Sheet1.DefaultStyle.Locked = true;
            //this.neuSpread1_Sheet2.DefaultStyle.Locked = true;
        }

        /// <summary>
        /// ��ʽ�� 
        /// </summary>
        private void SetFormat()
        {
            //   this.neuSpread1_Sheet1.Columns[5].Visible = false;
            //  this.neuSpread1_Sheet1.Columns[6].Visible = false;
            this.neuSpread1_Sheet1.Columns[7].Visible = false;

            this.neuSpread1_Sheet1.Columns[8].Visible = false;
            this.neuSpread1_Sheet1.Columns[9].Visible = false;

            this.neuSpread1_Sheet1.Columns[10].Visible = false;

            RefreshFpFlag(this.neuSpread1_Sheet1);
        }

        /// <summary>
        /// ˢ����ʾҩƷ��������ʾͣ�ñ�־
        /// </summary>
        private void RefreshFpFlag(FarPoint.Win.Spread.SheetView sheet)
        {
            if (sheet.RowCount > 0)
            {
                for (int i = 0; i < sheet.RowCount; i++)
                {
                    if (sheet.Cells[i, 10].Text.ToString().Trim() == "1")
                        sheet.Cells[i, 2].BackColor = System.Drawing.Color.Red;
                    else
                        sheet.Cells[i, 2].BackColor = System.Drawing.Color.White;
                }
            }
        }

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
            Neusoft.NFC.Interface.Classes.Function.ShowWaitForm("���ڽ��в�ѯ...���Ժ�");
            Application.DoEvents();

            string exeSql = "";
            if (this.ckAll.Checked)			//ȫ��ҩƷ
            {
                exeSql = string.Format( this.sql1,stroeageCode);
            }
            else
            {
                if (this.cmbDrug.Tag != null && this.cmbDrug.Tag.ToString() != "")
                {
                    exeSql = string.Format(this.sql2,stroeageCode, this.cmbDrug.Tag.ToString());
                }
                else
                {
                    Neusoft.NFC.Interface.Classes.Function.HideWaitForm();
                    return;
                }
            }

            Neusoft.NFC.Management.DataBaseManger dataManager = new Neusoft.NFC.Management.DataBaseManger();

            DataSet ds = new DataSet();

            if (dataManager.ExecQuery(exeSql, ref ds) == -1)
            {
                Neusoft.NFC.Interface.Classes.Function.HideWaitForm();
                MessageBox.Show(Language.Msg("ִ��Sql��䷢������" + dataManager.Err));
                return;
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
                {
                    if (ds.Tables[0].Columns.Contains("������"))
                        dr["������"] = this.deptHelper.GetName(dr["������"].ToString());
                }
            }

            this.dv = new DataView(ds.Tables[0]);

            this.neuSpread1_Sheet1.DataSource = dv;

            this.SetFormat();

            if (this.neuSpread1.ActiveSheet != this.neuSpread1_Sheet1)
            {
                this.neuSpread1.ActiveSheet = this.neuSpread1_Sheet1;
            }

            Neusoft.NFC.Interface.Classes.Function.HideWaitForm();
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
                    string rowFilter = string.Format("ƴ���� like '%{0}%'", this.txtFilter.Text);

                    this.dv.RowFilter = rowFilter;

                    RefreshFpFlag(this.neuSpread1_Sheet1);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       // private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        //{
            //if (this.neuSpread1.ActiveSheet == this.neuSpread1_Sheet2)
            //{
           //     this.neuSpread1.ActiveSheet = this.neuSpread1_Sheet1;
            //}
            //else
            
             //   string drugCode = this.neuSpread1_Sheet1.Cells[e.Row, 8].Text;
             //   string deptCode = this.neuSpread1_Sheet1.Cells[e.Row, 9].Text;

              //  Neusoft.NFC.Management.DataBaseManger dataManager = new Neusoft.NFC.Management.DataBaseManger();
              //  DataSet ds = new DataSet();

             //   string exeSql = string.Format(this.sql3, drugCode, deptCode);

              //  if (dataManager.ExecQuery(exeSql, ref ds) == -1)
             //   {
              //      MessageBox.Show(Language.Msg(dataManager.Err));
             //   }
                        

            
        
    }
}
