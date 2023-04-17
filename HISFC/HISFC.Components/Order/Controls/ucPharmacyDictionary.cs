using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// [��������: ҩƷ�ֵ�]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucPharmacyDictionary : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPharmacyDictionary()
        {
            InitializeComponent();

            isUserRetailPrice2 = controlMgr.GetControlParam("HNPHA2", true, false);
        }

        #region ����

        /// <summary>
        /// ��ʾ�����ļ�
        /// </summary>
        public string FilePath
        {
            get
            {
                return FS.FrameWork.WinForms.Classes.Function.SettingPath + "PharmacyOrderInfo.xml";
            }
        }

        /// <summary>
        /// ҵ���
        /// </summary>
        FS.HISFC.BizLogic.Pharmacy.Item phaMgr = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// ���Ʋ�����
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// �Ƿ���������
        /// </summary>
        private bool isUserRetailPrice2 = false;

        DataView dv = null;

        /// <summary>
        /// ���ͽڵ�ѡ���������
        /// </summary>
        private string filterTree = " 1=1 ";

        /// <summary>
        /// �������������
        /// </summary>
        private string filterInput = " 1=1 ";

        /// <summary>
        /// �Ƿ���ʾ�����Ϣ
        /// </summary>
        protected bool isShowStorage = false;

        /// <summary>
        /// ���������Ƿ���������������߱���
        /// </summary>
        [Description("�Ƿ���ʾ�����Ϣ"), Category("����")]
        public bool IsShowStorage
        {
            get
            {
                return this.isShowStorage;
            }
            set
            {
                this.isShowStorage = value;

                this.pnlBottom.Visible = true;
            }
        }

        /// <summary>
        /// �Ƿ���ʾҩƷ˵����
        /// </summary>
        private bool isShowPhaIntroduction = false;

        /// <summary>
        /// ���������Ƿ���������������߱���
        /// </summary>
        [Description("�Ƿ���ʾҩƷ˵����"), Category("����")]
        public bool IsShowPhaIntroduction
        {
            get
            {
                return isShowPhaIntroduction;
            }
            set
            {
                isShowPhaIntroduction = value;

                this.tbPhaInfo.Visible = value;
            }
        }

        #endregion

        /// <summary>
        /// ��ѯ��ʾ�����Ϣ
        /// </summary>
        /// <param name="drugCode"></param>
        private void ShowStorage(string drugCode)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ���ҩƷ�����Ϣ ���Ժ�.....");
            Application.DoEvents();
            DataSet ds = new DataSet();
            if (this.phaMgr.ExecQuery("Order.GetPharmacy.Storage.OrderInfo", ref ds, drugCode) == -1)
            {
                MessageBox.Show("��ȡҩƷ�����Ϣ����\n" + this.phaMgr.Err + "���˳�����");
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return;
            }
            try
            {
                DataView dvStore = new DataView(ds.Tables[0]);
                this.fpStorage_Sheet1.DataSource = dvStore;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                GC.Collect();
                return;
            }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }

        /// <summary>
        /// ˫����ʾ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string drugCode = string.Empty;
            if (this.fpSpread1_Sheet1.RowCount > 0 && e.Row >= 0)
            {
                drugCode = this.fpSpread1_Sheet1.Cells[e.Row, 0].Text;

                if (string.IsNullOrEmpty(drugCode))
                {
                    this.pnlBottom.Visible = false;
                }
                else
                {
                    if (this.isShowStorage)
                    {
                        this.pnlBottom.Visible = true;
                        this.ShowStorage(drugCode);
                    }
                }
            }
        }

        /// <summary>
        /// ˫���ָ������ؿ����Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSplitter1_DoubleClick(object sender, EventArgs e)
        {
            this.pnlBottom.Visible = false;
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucPharmacyDictionary_Load(object sender, System.EventArgs e)
        {
            this.neuLinkLabel1.Click += new EventHandler(linkLabel1_Click);
            this.txtFilter.TextChanged += new EventHandler(textBox1_TextChanged);
            this.fpSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpSpread1_ColumnWidthChanged);
            this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread1_CellDoubleClick);

            try
            {
                this.PassInit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.pnlBottom.Visible = false;//{E556A187-D9CA-4837-86ED-A57FA4FF23C4}

            //this.ShowPharmacyInfo();
        }

        /// <summary>
        /// ��ʼ��������ҩ
        /// </summary>
        /// <returns></returns>
        public int PassInit()
        {
            //FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("������ҩϵͳ��ʼ�� ���Ժ�...");
            //Application.DoEvents();
            ////if (Pass.Pass.PassInit(this.myItem.Operator.ID, this.myItem.Operator.Name, ((FS.HISFC.Models.RADT.Person)this.myItem.Operator).Dept.ID, ((FS.HISFC.Models.RADT.Person)this.myItem.Operator).Dept.Name, false) == -1)
            ////{
            ////    MessageBox.Show(Pass.Pass.Err);
            ////    return -1;
            ////}
            //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            return 1;
        }

        /// <summary>
        /// ����ҩƷ������Ϣ
        /// </summary>
        public void ShowPharmacyInfo()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ���ҩƷ��Ϣ ���Ժ�.....");
            Application.DoEvents();
            DataSet ds = new DataSet();
            if (isUserRetailPrice2)
            {
                if (this.phaMgr.ExecQuery("Pharmacy.Item.OrderInfoByRetailPrice2", ref ds, "") == -1)
                {
                    MessageBox.Show("��ȡҩƷ��Ϣ����\n" + this.phaMgr.Err + "���˳�����");
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return;
                }
            }
            else
            {
                if (this.phaMgr.ExecQuery("Pharmacy.Item.OrderInfo", ref ds, "") == -1)
                {
                    MessageBox.Show("��ȡҩƷ��Ϣ����\n" + this.phaMgr.Err + "���˳�����");
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return;
                }
            }
            try
            {
                dv = new DataView(ds.Tables[0]);
                this.fpSpread1_Sheet1.DataSource = dv;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                GC.Collect();
                return;
            }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }

            try
            {
                if (System.IO.File.Exists(this.FilePath))
                {
                    FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1_Sheet1, this.FilePath);
                }
                else
                {
                    FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1_Sheet1, this.FilePath);
                }


            }
            catch (Exception ex)
            {
                //MessageBox.Show("��ȡ������ʾ�����ļ�ʱ����! ���˳�����" + ex.Message);
                //GC.Collect();
                return;
            }
        }

        /// <summary>
        /// ���ù���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_Click(object sender, EventArgs e)
        {
            FS.HISFC.Components.Common.Controls.ucSetColumn uc = new FS.HISFC.Components.Common.Controls.ucSetColumn();
            uc.FilePath = this.FilePath;
            uc.SetColVisible(true, true, false, false);
            uc.SetDataTable(this.FilePath, this.fpSpread1.Sheets[0]);
            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��ʾ����";
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
            uc.DisplayEvent += new EventHandler(ucSetColumn_DisplayEvent);
            this.ucSetColumn_DisplayEvent(null, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucSetColumn_DisplayEvent(object sender, EventArgs e)
        {
            this.ShowPharmacyInfo();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.revertRowColor(fpSpread1_Sheet1);
            string queryCode = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtFilter.Text);
            queryCode = FS.FrameWork.Public.String.TakeOffSpecialChar(queryCode, "'", "%");
            queryCode = queryCode + "%";
            this.filterInput = "((��Ʒ��ƴ���� LIKE '%" + queryCode + "') OR " +
                "(��Ʒ������� LIKE '%" + queryCode + "') OR " +
                "(��Ʒ���Զ����� LIKE '%" + queryCode + "') OR " +
                "(��Ʒ���� LIKE '" + queryCode + "') OR" +
                "(ͨ����ƴ���� LIKE '" + queryCode + "') OR " +
                "(ͨ��������� LIKE '" + queryCode + "') OR " +
                "(ͨ���� LIKE '" + queryCode + "') OR (Ӣ���� LIKE '" + queryCode + "') )";

            this.SetFilter();
            this.setRowColor(fpSpread1_Sheet1);
            if (System.IO.File.Exists(this.FilePath))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1_Sheet1, this.FilePath);
            }
        }


        private void SetFilter()
        {
            //��Ϲ�������
            string filter = this.filterTree + " AND " + this.filterInput;
            //��������
            dv.RowFilter = filter;
        }

        /// <summary>
        /// ������Ŀͣ����ɫ�仯
        /// </summary>
        /// <param name="sheet"></param>
        private void setRowColor(FarPoint.Win.Spread.SheetView sheet)
        {
            for (int rows = 0; rows < sheet.RowCount;rows++ )
            {
                for (int colums = 0; colums < sheet.ColumnCount; colums++)
                {
                    if (sheet.Cells[rows, colums].Text == "ͣ��")
                    {
                        sheet.Rows[rows].BackColor = Color.FromArgb(255, 128, 128);
                    }
                }
            }
        }

        private void revertRowColor(FarPoint.Win.Spread.SheetView sheet)
        {
            for (int rows = 0; rows < sheet.RowCount; rows++)
            {
                if (rows % 2 == 0)
                {
                    sheet.Rows[rows].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
                }
                else
                {
                    sheet.Rows[rows].BackColor = System.Drawing.Color.Empty;
                }
            }
            sheet.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
          
        }

        private void fpSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1_Sheet1, this.FilePath);
        }

        #region IReport ��Ա

        public void Export()
        {
            // TODO:  ��� ucPharmacyDictionary.Export ʵ��
            try
            {
                string fileName = "";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".xls";
                dlg.Filter = "Microsoft Excel ������ (*.xls)|*.xls";
                DialogResult result = dlg.ShowDialog();

                if (result == DialogResult.OK)
                {
                    fileName = dlg.FileName;
                    this.fpSpread1.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Query()
        {
            // TODO:  ��� ucPharmacyDictionary.Query ʵ��
            this.ShowPharmacyInfo();
        }


        #endregion

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.Query();
            this.setRowColor(this.fpSpread1_Sheet1);
            return null;
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.PrintPreview(this);
            return 0;
        }

        /// <summary>
        /// ������ʾҩƷ˵����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string strCode = this.fpSpread1_Sheet1.Cells[e.Row, 0].Text;
            FS.HISFC.BizLogic.Pharmacy.Item phaMgr = new FS.HISFC.BizLogic.Pharmacy.Item();
            FS.HISFC.Models.Pharmacy.Item item = new FS.HISFC.Models.Pharmacy.Item();
            item = phaMgr.GetItem(strCode);

            this.tpDescription.Text = item.Name + " [ " + item.Specs + " ] ҩƷ��ϸ��Ϣ";

            this.neutext.Text = item.Product.Manual;
        }
    }
}
