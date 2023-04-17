using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace Report.Logistics.DrugStore
{
    /// <summary>
    /// <�޸ļ�¼>
    ///    1����ʼ��ʱ��ѡ��ؼ� by Sunjh 2010-8-23 {67896C13-1E0C-492e-B6A3-0843CC915D9E}
    /// </�޸ļ�¼>
    /// </summary>
    public partial class ucPhaDispensedQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPhaDispensedQuery()
        {
            InitializeComponent();
            GetItemzFunctionInfo();
            InitCbInOutType();
            if (this.neuTabControl1.Contains(this.tabDetail))
            {
                this.neuTabControl1.TabPages.Remove(tabDetail);
            }
        }
        protected FS.HISFC.Models.Base.Employee employee = null;
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
        private FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();
        private FS.HISFC.Models.Base.Employee emp = new FS.HISFC.Models.Base.Employee();

        /// <summary>
        /// ��ʼ��ҩƷ���
        /// </summary>
        /// <returns></returns>
        private bool GetItemzFunctionInfo()
        {
            FS.HISFC.BizLogic.RADT.InPatient Manager = new FS.HISFC.BizLogic.RADT.InPatient();
            System.Collections.ArrayList alUsecodeList = new ArrayList();
            this.cbPhaName.alItems.Clear();
            this.cbPhaName.Items.Clear();
            FS.HISFC.Models.Base.Spell sp = new FS.HISFC.Models.Base.Spell();
            sp.Name = "ȫ��";
            sp.ID = "ALL";
            alUsecodeList.Add(sp);
            string strSql = @" select tt.drug_code,tt.trade_name,tt.spell_code,tt.wb_code from pha_com_baseinfo tt";

            strSql = string.Format(strSql);
            DataSet ds = new DataSet();
            if (Manager.ExecQuery(strSql, ref ds) == -1)
            {
                return false;
            }
            if (ds == null || ds.Tables[0] == null)
            {
                MessageBox.Show("��ѯ����", "����,ҩƷ���ش���");
            }
            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    FS.HISFC.Models.Base.Spell obj = new FS.HISFC.Models.Base.Spell();
                    obj.ID = ds.Tables[0].Rows[i][0].ToString();
                    obj.Name = ds.Tables[0].Rows[i][1].ToString();
                    obj.SpellCode = ds.Tables[0].Rows[i][2].ToString();
                    obj.WBCode = ds.Tables[0].Rows[i][3].ToString();
                    alUsecodeList.Add(obj);
                }
                int c = this.cbPhaName.AddItems(alUsecodeList);

            }
            else
            {
                return false;
            }
            this.cbPhaName.SelectedIndex = 0;
            return true;
        }
        /// <summary>
        /// ��ʼ�����������
        /// </summary>
        /// <returns></returns>
        private bool InitCbInOutType()
        {
            ArrayList alInOut = new ArrayList();
            FS.HISFC.Models.Base.Spell obj1 = new FS.HISFC.Models.Base.Spell();
            obj1.ID = "IN";
            obj1.Name = "���";
            alInOut.Add(obj1);
            FS.HISFC.Models.Base.Spell obj2 = new FS.HISFC.Models.Base.Spell();
            obj2.ID = "OUT";
            obj2.Name = "����";
            alInOut.Add(obj2);
            this.cbInOutType.AddItems(alInOut);
            this.cbInOutType.SelectedIndex = 0;
            return true;
        }
        /// <summary>
        /// ��ѯ�����Ϣ
        /// </summary>
        private void QueryInPut()
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڲ�ѯ�����Ժ�..."));
                Application.DoEvents();

                DataSet ds = new DataSet();
                emp = (FS.HISFC.Models.Base.Employee)dept.Operator;
                if (this.itemManager.ExecQuery("FS.Pha.Out.Info", ref ds, this.dtpFrom.Value.ToString(),this.dtpTo.Value.ToString(),this.emp.Dept.ID,this.cbPhaName.SelectedItem.ID) == -1)
                {
                    MessageBox.Show(Language.Msg("���ݲ�ѯʧ�ܣ��������Ա��ϵ��" + this.itemManager.Err));
                    return;
                }

                this.nfpStat.DataSource = ds;
                this.nfpStat_Sheet1.SheetName = emp.Dept.Name+"�����Ϣ��ϸ";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }
        private void QueryOutPut()
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڲ�ѯ�����Ժ�..."));
                Application.DoEvents();

                DataSet ds = new DataSet();
                emp = (FS.HISFC.Models.Base.Employee)dept.Operator;
                if (this.itemManager.ExecQuery("FS.Pha.Dispense", ref ds, this.dtpFrom.Value.ToString(), this.dtpTo.Value.ToString(), this.emp.Dept.ID, this.cbPhaName.SelectedItem.ID) == -1)
                {
                    MessageBox.Show(Language.Msg("���ݲ�ѯʧ�ܣ��������Ա��ϵ��" + this.itemManager.Err));
                    return;
                }

                this.nfpStat.DataSource = ds;
                this.nfpStat_Sheet1.SheetName = emp.Dept.Name + "������Ϣ��ϸ";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }

        }
        private void QueryDetail()
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڲ�ѯ�����Ժ�..."));
                Application.DoEvents();

                string deptCode;

                DataSet ds = new DataSet();
                emp = (FS.HISFC.Models.Base.Employee)dept.Operator;
                deptCode = this.nfpStat_Sheet1.Cells[this.nfpStat_Sheet1.ActiveRowIndex, 6].Text.ToString(); //this.nfpStat_Sheet1.Cells[this.nfpStat_Sheet1.ActiveRowIndex, this.nfpStat_Sheet1.Columns["���ұ���"].Index].Text;
                
                string phaname=this.nfpStat_Sheet1.Cells[this.nfpStat_Sheet1.ActiveRowIndex,0].Text.ToString();
                if (this.itemManager.ExecQuery("FS.Pha.DispensePerPatient", ref ds, this.dtpFrom.Value.ToString(), this.dtpTo.Value.ToString(), this.emp.Dept.ID, phaname,deptCode) == -1)
                {
                    MessageBox.Show(Language.Msg("���ݲ�ѯʧ�ܣ��������Ա��ϵ��" + this.itemManager.Err));
                    return;
                }

                this.nfpDetail.DataSource = ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }
        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            if (dtpFrom.Value > dtpTo.Value)
            {
                MessageBox.Show("��ʼʱ�䲻�ܴ��ڽ�ֹʱ�䣡");
                return -1;
            }
            if (this.cbInOutType.SelectedItem.ID == "IN")
            {
                if (this.neuTabControl1.Contains(tabDetail))
                {
                    this.neuTabControl1.TabPages.Remove(tabDetail);
                }
                this.QueryInPut();
            }
            else 
            {
                if (this.neuTabControl1.Contains(tabDetail))
                {
                    this.neuTabControl1.TabPages.Remove(tabDetail);
                }
                this.QueryOutPut();
            }
            return 1;
        }

        private void nfpStat_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.cbInOutType.SelectedItem.ID == "IN")
            {
                return;
            }
            if (this.neuTabControl1.Contains(tabDetail) == false)
            {
                this.neuTabControl1.TabPages.Add(tabDetail);
            }
            this.neuTabControl1.SelectedIndex = 1;
            QueryDetail();
        }


        protected override void OnLoad(EventArgs e)
        {
            //��ʼ��ʱ��ѡ��ؼ� by Sunjh 2010-8-23 {67896C13-1E0C-492e-B6A3-0843CC915D9E}
            this.dtpFrom.Value = DateTime.Now.Date;
            this.dtpTo.Value = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            base.OnLoad(e);
        }

         //��ӡԤ��
        public override int PrintPreview(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print printview = new FS.FrameWork.WinForms.Classes.Print();
    
            printview.PrintPreview(0, 0, this.neuTabControl1.SelectedTab);
            return base.OnPrintPreview(sender, neuObject);
        }

        //��ӡ
        protected override int OnPrint(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            print.PrintPage(0, 0, this.neuTabControl1.SelectedTab);

            return base.OnPrint(sender, neuObject);
        }

        //����
        public override int Export(object sender, object neuObject)
        {
            if (this.neuTabControl1.SelectedIndex ==0)
            {
                return nfpStat.Export();
            }
            else 
            {
                return nfpDetail.Export();
            }

        }

       
    }
}
