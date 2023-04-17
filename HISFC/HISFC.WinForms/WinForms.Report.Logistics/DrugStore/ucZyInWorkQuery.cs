using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.Report.Logistics.DrugStore
{
    /// <summary>
    /// [��������: סԺ��������ѯ]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-03]<br></br>
    /// <�޸ļ�¼ 
    ///		 ��ʵ�� Ȩ��ϵͳ����
    ///  />
    /// </summary>
    public partial class ucZyInWorkQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucZyInWorkQuery()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// ҩƷ����ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();     
        #endregion

        #region ����

        /// <summary>
        /// ��ѯ��ʼʱ��
        /// </summary>
        public DateTime BeginTime
        {
            get
            {
                return FS.FrameWork.Function.NConvert.ToDateTime(this.dtBegin.Text);
            }
        }

        /// <summary>
        /// ��ѯ��ֹʱ��
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                return FS.FrameWork.Function.NConvert.ToDateTime(this.dtEnd.Text);
            }
        }

        #endregion

        #region ��������Ϣ

        /// <summary>
        /// ���幤��������
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="NeuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object NeuObject, object param)
        {
            //���ӹ�����
            //this.toolBarService.AddToolButton("����", "������ǰ��������Ϣ", 0, true, false, null);
            return this.toolBarService;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();

            return 1;
        }

        public override int Export(object sender, object neuObject)
        {
            this.Export();

            return 1;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "����")
            {
                this.Export();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

        /// <summary>
        /// ���ݲ�ѯ
        /// </summary>
        private void Query()
        {
            if (this.dtBegin.Value>this.dtEnd.Value)
            {
                MessageBox.Show("��ʼʱ�䲻�ܴ�����ֹʱ��");
            }
            
            
            if (this.cmbDept.SelectedValue == null || this.cmbDept.SelectedValue.ToString() == "")
            {
                MessageBox.Show(Language.Msg("��ѡ���ѯҩ��"));
                return;
            }

            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ�����Ժ�...");
                Application.DoEvents();

                //��ѯͳ��
                DataSet ds = new DataSet();
                if (this.itemManager.ExecQuery("Pharmarcy.Report.Inpatient.Query", ref ds, this.cmbDept.SelectedValue.ToString(), this.BeginTime.ToString(), this.EndTime.ToString()) == -1)
                {
                    Function.ShowMsg("���ݲ�ѯʧ�ܣ��������Ա��ϵ��" + this.itemManager.Err);
                    return;
                }

                //��ʾͳ�ƽ��
                this.neuSpread1_Sheet1.DataSource = ds;
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
        /// ��ϸ��ѯ
        /// </summary>
        /// <param name="operInfo">����Ա</param>
        /// <param name="class3MeaningCode">���ͱ���</param>
        private void QueryDetail(FS.FrameWork.Models.NeuObject operInfo,string class3MeaningCode)
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڲ�ѯ�����Ժ�..."));
                Application.DoEvents();

                DataSet ds = new DataSet();
                if (this.itemManager.ExecQuery("Pharmarcy.Report.Inpatient.DetailQuery", ref ds, this.cmbDept.SelectedValue.ToString(),operInfo.ID, this.BeginTime.ToString(), this.EndTime.ToString(),class3MeaningCode) == -1)
                {
                    Function.ShowMsg("���ݲ�ѯʧ�ܣ��������Ա��ϵ��" + this.itemManager.Err);
                    return;
                }

                if (ds.Tables.Count > 0)
                {
                    //����¼��Чʱ ��ʱ��Ч���ڴ洢ҽ����ҩ���� ���δ�ʱ��������ʾ
                    if (ds.Tables[0].Columns.Contains("ȡ������") && ds.Tables[0].Columns.Contains("��Ч���") )
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (dr["��Ч���"].ToString() == "��Ч")
                            {
                                dr["ȡ������"] = "";
                            }
                        }
                    }
                }

                this.neuSpread1_Sheet2.DataSource = ds;

                this.neuSpread1_Sheet2.SheetName = operInfo.Name + " ��ϸ��ҩ��Ϣ";
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
        /// ����
        /// </summary>
        private void Export()
        {
            if (this.neuSpread1.Export() == 1)
            {
                MessageBox.Show(Language.Msg("�����ɹ�"));
            }
        }

        //��ӡԤ��
        public override int PrintPreview(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print printview = new FS.FrameWork.WinForms.Classes.Print();

            //printview.PrintPreview(0, 0, this.neuTabControl1.SelectedTab);
            printview.PrintPreview(this.neuPanel2);
            return base.OnPrintPreview(sender, neuObject);
        }

        //��ӡ
        protected override int OnPrint(object sender, object neuObject)
        {
            this.neuSpread1.PrintSheet(0);
            return base.OnPrint(sender, neuObject);
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ������� ���Ժ�...");
                Application.DoEvents();

                //Ĭ��ʱ���				
                System.DateTime sysTime = this.itemManager.GetDateTimeFromSysDateTime();

                this.dtBegin.Value = new DateTime(sysTime.Year, sysTime.Month, sysTime.Day, 0, 0, 0);
                this.dtEnd.Value = new DateTime(sysTime.Year, sysTime.Month, sysTime.Day, 23, 59, 59);

                #region ����ҩ���б�

                //ҩ��ѡ��
                FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
                ArrayList al = deptManager.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.P);
                if (al == null)
                {
                    Function.ShowMsg("����ҩ���б�ʧ��\n" + this.itemManager.Err);
                    return;
                }
                this.cmbDept.AddItems(al);
                this.cmbDept.DataSource = al;
                this.cmbDept.DisplayMember = "Name";
                this.cmbDept.ValueMember = "ID";

                #endregion

                this.neuSpread1_Sheet1.DefaultStyle.Locked = true;
                this.neuSpread1_Sheet2.DefaultStyle.Locked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }

            base.OnLoad(e);
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            try
            {
                if (this.neuSpread1_Sheet1.RowCount == 0)
                {
                    return;
                }

                if (!this.neuSpread1.Sheets.Contains(this.neuSpread1_Sheet2))
                    this.neuSpread1.Sheets.Add(this.neuSpread1_Sheet2);

                if (this.neuSpread1.ActiveSheet == this.neuSpread1_Sheet1)
                {
                    string operCode = this.neuSpread1_Sheet1.Cells[e.Row, 0].Text;
                    string operName = this.neuSpread1_Sheet1.Cells[e.Row, 1].Text;
                    string class3Meaning = this.neuSpread1_Sheet1.Cells[e.Row, 2].Text == "��ҩ"?"Z2":"Z1";


                    FS.FrameWork.Models.NeuObject operInfo = new FS.FrameWork.Models.NeuObject();
                    operInfo.ID = operCode;
                    operInfo.Name = operName;

                    this.QueryDetail(operInfo,class3Meaning);
                }

                this.neuSpread1.ActiveSheet = this.neuSpread1_Sheet2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
