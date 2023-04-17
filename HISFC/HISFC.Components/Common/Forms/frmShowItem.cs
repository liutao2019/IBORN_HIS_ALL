using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace FS.HISFC.Components.Common.Forms
{
    /// <summary>
    /// ��Ŀѡ��
    /// </summary>
    public partial class frmShowItem : Form
    {
        /// <summary>
        /// ����
        /// </summary>
        public frmShowItem()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                lnkMore.Click += new EventHandler(lnkMore_Click);
                this.primitiveSize = this.Size;
                this.titlePanelSize = this.pnMain.Size;


                this.ckbSpell.CheckedChanged += new EventHandler(QueryTypeChanged);
                this.chbWB.CheckedChanged += new EventHandler(QueryTypeChanged);
                this.chkDeptItem.CheckedChanged += new EventHandler(QueryTypeChanged);
                this.cbxIsReal.CheckedChanged += new EventHandler(QueryTypeChanged);
            }
        }

        #region ����

        //���뷨
        public delegate void SelectWriteWay(int i);

        /// <summary>
        /// ��������
        /// </summary>
        FS.HISFC.Components.Common.Controls.ucSetColumnForOrder ucSetXML = new FS.HISFC.Components.Common.Controls.ucSetColumnForOrder();

        /// <summary>
        /// ���Ʋ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam contrIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        /// <summary>
        /// ���뷨
        /// </summary>
        public event SelectWriteWay writeWay = null;

        ///
        public bool isShowIsReal = true;

        //�Ƿ����ҽ��
        public delegate void IsCompanyRang(bool flag);

        /// <summary>
        /// �Ƿ����ҽ��
        /// </summary>
        public event IsCompanyRang companyRang;

        //�Ƿ�Ƴ���
        public delegate void IsDeptUsedItem(bool flag);

        /// <summary>
        /// �Ƿ�Ƴ���
        /// </summary>
        public event IsDeptUsedItem isDeptUsedItem = null;

        /// <summary>
        /// ����ҽ��վˢ���б��ǣ��籣��ǣ��ķ�ʽ��0 ��ˢ�£�1 ʹ�ö��̣߳�2 ���̹߳���ˢ�£�3 ��ʼ�����أ����ݺ�ͬ��λ�����籣����У�
        /// </summary>
        public int IsUserThread = 0;

        private const int WM_NCLBUTTONDOWN = 0xA1;

        private const int HTCSPTION = 2;

        [DllImport("user32.dll")]
        private static extern int SendMessage(int hWnd, int wMsg, int wParm, int lParm);

        [DllImport("user32.dll")]
        private static extern int ReleaseCapture();

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage((int)this.Handle, WM_NCLBUTTONDOWN, HTCSPTION, 0);
        }
        FarPoint.Win.Spread.FpSpread sheetView = null;

        /// <summary>
        /// �Ƿ�����������
        /// </summary>
        public bool IsFilterWBCode
        {
            get
            {
                return chbWB.Checked;
            }
        }

        /// <summary>
        /// �Ƿ���ƴ�������
        /// </summary>
        public bool IsFilterSpellCode
        {
            get
            {
                return ckbSpell.Checked;
            }
        }

        /// <summary>
        /// ע������
        /// </summary>
        private string memo;
        public string Memo
        {
            get
            {
                return memo;
            }
            set
            {
                memo = value;
            }
        }

        #endregion

        public void Init()
        {
            if (DesignMode)
            {
                return;
            }
            if (this.sheetView != null)
            {
                this.sheetView.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(sheetView_ColumnWidthChanged);
            }
            if (this.sheeViewDetail != null)
            {
                this.sheeViewDetail.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(sheeViewDetail_ColumnWidthChanged);
            }

            SetDeptItem(false);

            isShowIsReal = contrIntegrate.GetControlParam<bool>("201026", false, true);
        }

        void frmShowItem_MouseWheel(object sender, MouseEventArgs e)
        {
            if (IsUserThread == 2)
            {
                FS.HISFC.Models.SIInterface.Compare compareItem = null;

                if (this.ActiveControl.GetType().IsSubclassOf(typeof(FarPoint.Win.Spread.FpSpread)))
                {
                    Font font_Bold = new Font(((FarPoint.Win.Spread.FpSpread)ActiveControl).Font.FontFamily, ((FarPoint.Win.Spread.FpSpread)ActiveControl).Font.Size, FontStyle.Bold);
                    Font font_Regular = new Font(((FarPoint.Win.Spread.FpSpread)ActiveControl).Font.FontFamily, ((FarPoint.Win.Spread.FpSpread)ActiveControl).Font.Size, FontStyle.Regular);

                    //ֻˢ�µ�ǰ��ʾ������
                    for (int i = ((FarPoint.Win.Spread.FpSpread)ActiveControl).GetViewportTopRow(0);
                        i < ((FarPoint.Win.Spread.FpSpread)ActiveControl).GetViewportBottomRow(0) + 1;
                        i++)
                    {
                        if (i >= ((FarPoint.Win.Spread.FpSpread)ActiveControl).Sheets[0].RowCount)
                        {
                            return;
                        }
                        if (((FarPoint.Win.Spread.FpSpread)ActiveControl).Sheets[0].Cells[i, (int)FS.HISFC.Components.Common.Controls.EnumMainColumnSet.LackFlag].Text == "��")
                        {
                            ((FarPoint.Win.Spread.FpSpread)ActiveControl).Sheets[0].Rows[i].BackColor = Color.LightCoral;
                        }
                        else
                        {
                            ((FarPoint.Win.Spread.FpSpread)ActiveControl).Sheets[0].Rows[i].BackColor = Color.Transparent;
                        }

                        #region ��ʾҽ���ȼ���Ϣ

                        //if (Classes.Function.GetCompareItemInfo(((FarPoint.Win.Spread.FpSpread)ActiveControl).Sheets[0].Cells[i, (int)FS.HISFC.Components.Common.Controls.EnumMainColumnSet.ItemCode].Text.Trim(), ref compareItem) == -1)
                        //{
                        //    ((FarPoint.Win.Spread.FpSpread)ActiveControl).Sheets[0].Rows[i].Label = i.ToString();
                        //    ((FarPoint.Win.Spread.FpSpread)ActiveControl).Sheets[0].RowHeader.Rows[i].Font = font_Regular;
                        //    ((FarPoint.Win.Spread.FpSpread)ActiveControl).Sheets[0].RowHeader.Rows[i].ForeColor = ((FarPoint.Win.Spread.FpSpread)ActiveControl).Sheets[0].ColumnHeader.Columns[0].ForeColor;
                        //}
                        //else
                        //{
                        //    //ҽ�����
                        //    switch (compareItem.CenterItem.ItemGrade)
                        //    {
                        //        case "1":
                        //            ((FarPoint.Win.Spread.FpSpread)ActiveControl).Sheets[0].Rows[i].Label = "��";
                        //            ((FarPoint.Win.Spread.FpSpread)ActiveControl).Sheets[0].RowHeader.Rows[i].Font = font_Bold;
                        //            ((FarPoint.Win.Spread.FpSpread)ActiveControl).Sheets[0].RowHeader.Rows[i].ForeColor = Color.Red;
                        //            break;
                        //        case "2":
                        //            ((FarPoint.Win.Spread.FpSpread)ActiveControl).Sheets[0].Rows[i].Label = "��";
                        //            ((FarPoint.Win.Spread.FpSpread)ActiveControl).Sheets[0].RowHeader.Rows[i].Font = font_Bold;
                        //            ((FarPoint.Win.Spread.FpSpread)ActiveControl).Sheets[0].RowHeader.Rows[i].ForeColor = Color.Red;
                        //            break;
                        //        default:
                        //            ((FarPoint.Win.Spread.FpSpread)ActiveControl).Sheets[0].Rows[i].Label = i.ToString();
                        //            ((FarPoint.Win.Spread.FpSpread)ActiveControl).Sheets[0].RowHeader.Rows[i].Font = font_Regular;
                        //            ((FarPoint.Win.Spread.FpSpread)ActiveControl).Sheets[0].RowHeader.Rows[i].ForeColor = ((FarPoint.Win.Spread.FpSpread)ActiveControl).Sheets[0].ColumnHeader.Columns[0].ForeColor;
                        //            break;
                        //    }
                        //}
                        #endregion
                    }
                }
            }
        }

        void sheeViewDetail_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.sheeViewDetail.Sheets[0], this.sheetDetailXMLFileName);
        }

        void sheetView_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.sheetView.Sheets[0], this.sheetXMLFileName);
        }

        /// <summary>
        /// ��Ŀ��ϸ�б������ļ�
        /// </summary>
        protected string sheetDetailXMLFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath +
            FS.FrameWork.WinForms.Classes.Function.SettingPath + "OrderItemDetail.xml";

        /// <summary>
        /// ��Ŀ�б������ļ�
        /// </summary>
        protected string sheetXMLFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath +
            FS.FrameWork.WinForms.Classes.Function.SettingPath + "OrderItem.xml";

        /// <summary>
        /// 
        /// </summary>
        private FarPoint.Win.Spread.FpSpread sheeViewDetail = null;


        /// <summary>
        /// ���FarPoint
        /// </summary>
        /// <param name="c"></param>
        public void AddControl(Control c)
        {
            if (c.GetType().IsSubclassOf(typeof(FarPoint.Win.Spread.FpSpread)))
                sheetView = c as FarPoint.Win.Spread.FpSpread;

            c.Dock = DockStyle.Fill;
            //c.Height = FS.FrameWork.Function.NConvert.ToInt32(((FarPoint.Win.Spread.FpSpread)c).Sheets[0].RowCount * ((FarPoint.Win.Spread.FpSpread)c).Sheets[0].Rows[0].Height) + 100;
            //c.Width = PanelMain.Width * 2;
            c.Visible = true;

            this.PanelMain.Controls.Add(c);
        }

        #region �����û�Ĭ������

        /// <summary>
        /// �����û�Ĭ������
        /// </summary>
        private void SaveUserDefaultSetting()
        {
            FS.HISFC.BizLogic.Manager.UserDefaultSetting settingManager = new FS.HISFC.BizLogic.Manager.UserDefaultSetting();

            string setting1 = "";
            string setting2 = "";
            string setting3 = "";
            string errInfo = "";
            if (this.GetUserDefaultSetting(ref setting1, ref setting2, ref setting3, ref errInfo) == -1)
            {
                MessageBox.Show(errInfo);
                return;
            }

            FS.HISFC.Models.Base.UserDefaultSetting settingObj = settingManager.Query(settingManager.Operator.ID);

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            if (settingObj == null)
            {
                settingObj = new FS.HISFC.Models.Base.UserDefaultSetting();
                settingObj.Empl.ID = settingManager.Operator.ID;

                settingObj.Setting1 = setting1;
                settingObj.Setting2 = setting2;
                settingObj.Setting3 = setting3;

                settingObj.Oper.ID = settingManager.Operator.ID;
                settingObj.Oper.OperTime = settingManager.GetDateTimeFromSysDateTime();

                if (settingManager.Insert(settingObj) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�����û�������Ϣ����" + settingManager.Err);
                    return;
                }
            }
            else
            {
                settingObj.Setting1 = setting1;
                settingObj.Setting2 = setting2;
                settingObj.Setting3 = setting3;

                settingObj.Oper.ID = settingManager.Operator.ID;
                settingObj.Oper.OperTime = settingManager.GetDateTimeFromSysDateTime();

                if (settingManager.Update(settingObj) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�����û�������Ϣ����" + settingManager.Err);
                    return;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
        }

        /// <summary>
        /// �����Ƿ�ʹ�ÿ��ҳ�����Ŀ
        /// </summary>
        /// <param name="isUseDeptItem">true ʹ��</param>
        public void SetDeptItem(bool isUseDeptItem)
        {
            this.chkDeptItem.CheckedChanged -= new EventHandler(QueryTypeChanged);
            this.chkDeptItem.Checked = isUseDeptItem;
            this.chkDeptItem.CheckedChanged += new EventHandler(QueryTypeChanged);
        }

        /// <summary>
        /// ���ò�ѯ���뷨���
        /// </summary>
        /// <param name="queryType"></param>
        public void SetQueryType(string queryType)
        {
            switch (queryType)
            {
                case "00":
                    this.ckbSpell.Checked = false;
                    this.chbWB.Checked = false;
                    break;
                case "01":
                    this.ckbSpell.Checked = false;
                    this.chbWB.Checked = true;
                    break;
                case "10":
                    this.ckbSpell.Checked = true;
                    this.chbWB.Checked = false;
                    break;
                case "11":
                    this.ckbSpell.Checked = true;
                    this.chbWB.Checked = true;
                    break;
                default:
                    this.ckbSpell.Checked = true;
                    this.chbWB.Checked = true;
                    break;
            }
        }

        /// <summary>
        /// ��ȡ��ǰ�û�Ĭ������
        /// </summary>
        /// <param name="setting1">ƴ���롢�����</param>
        /// <param name="setting2">���ҳ�����Ŀ</param>
        /// <param name="setting3">�Ƿ�ȷ����</param>
        /// <param name="errInfo">������Ϣ</param>
        /// <returns></returns>
        private int GetUserDefaultSetting(ref string setting1, ref string setting2, ref string setting3, ref string errInfo)
        {
            try
            {
                //setting1 ƴ���롢�����
                setting1 = FS.FrameWork.Function.NConvert.ToInt32(this.ckbSpell.Checked).ToString() + FS.FrameWork.Function.NConvert.ToInt32(this.chbWB.Checked).ToString();

                //�Ƴ�����Ŀ
                setting2 = FS.FrameWork.Function.NConvert.ToInt32(this.chkDeptItem.Checked).ToString();

                //�Ƿ�ȷ����
                setting3 = FS.FrameWork.Function.NConvert.ToInt32(this.cbxIsReal.Checked).ToString();
            }
            catch (Exception ex)
            {
                errInfo = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ���õ�ǰ�û�Ĭ������
        /// </summary>
        public void SetUserDefaultSetting()
        {
            FS.HISFC.BizLogic.Manager.UserDefaultSetting settingManager = new FS.HISFC.BizLogic.Manager.UserDefaultSetting();

            FS.HISFC.Models.Base.UserDefaultSetting settingObj = settingManager.Query(settingManager.Operator.ID);

            if (settingObj != null)
            {
                this.SetQueryType(settingObj.Setting1);
                this.chkDeptItem.Checked = FS.FrameWork.Function.NConvert.ToBoolean(string.IsNullOrEmpty(settingObj.Setting2) ? "0" : settingObj.Setting2);

                if (isShowIsReal)
                {
                    this.cbxIsReal.Checked = FS.FrameWork.Function.NConvert.ToBoolean(string.IsNullOrEmpty(settingObj.Setting3) ? "0" : settingObj.Setting3);
                }
            }
        }
        #endregion

        /// <summary>
        /// ˢ���б�
        /// </summary>
        public void RefreshFP()
        {
            if (sheeViewDetail == null && sheetView != null)//����ĳЩ�ط����õ�ʱ��sheeViewDetail����Ϊnull��so������ж�add by sunm
            {
                sheeViewDetail = new FarPoint.Win.Spread.FpSpread();
                sheeViewDetail.Sheets.Add(new FarPoint.Win.Spread.SheetView());
                sheeViewDetail.Sheets[0].ColumnCount = sheetView.Sheets[0].ColumnCount;
            }

            if (sheeViewDetail != null)
            {
                if (System.IO.File.Exists(this.sheetDetailXMLFileName))
                {
                    FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.sheeViewDetail.Sheets[0], this.sheetDetailXMLFileName);
                }
                else
                {
                    FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.sheeViewDetail.Sheets[0], this.sheetDetailXMLFileName);
                }
            }

            if (this.sheetView != null)
            {
                if (System.IO.File.Exists(this.sheetXMLFileName))
                {
                    FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.sheetView.Sheets[0], this.sheetXMLFileName);
                }
                else
                {
                    FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.sheetView.Sheets[0], this.sheetXMLFileName);
                }
            }
        }

        /// <summary>
        /// toolTip
        /// </summary>
        public string TipText
        {
            set
            {
                this.lblTip.Text = value;
            }
        }
        DataView dv = null;

        /// <summary>
        /// ��ǰ��ͼ
        /// </summary>
        public DataView DataView
        {
            set
            {
                this.dv = value;
            }
        }

        /// <summary>
        /// �Ƿ�ȷ����
        /// </summary>
        public bool IsReal
        {
            get
            {
                return !this.cbxIsReal.Checked;
            }
            set
            {
                this.cbxIsReal.Checked = value;
            }
        }

        private void lblLis_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void lblSet_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (sheetView == null)
                return;
            ucSetXML = new FS.HISFC.Components.Common.Controls.ucSetColumnForOrder();
            ucSetXML.SetColVisible(true, true, false, false);
            ucSetXML.SetDataTable(this.sheetDetailXMLFileName, sheetView.ActiveSheet);
            FS.FrameWork.WinForms.Classes.Function.PopForm.TopMost = true;

            if (FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucSetXML) == DialogResult.OK)
            {
                FS.HISFC.Components.Common.Controls.ucSetColumnForOrder.ReadXml(this.sheetDetailXMLFileName, sheetView.ActiveSheet);
            }
            FS.FrameWork.WinForms.Classes.Function.PopForm.TopMost = false;
        }


        #region {9A40A1FE-C527-4f86-B6F5-E7F52FDD28C9}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lnkMore_Click(object sender, EventArgs e)
        {
            if (lnkMore.Text == "��ʾ5��")
            {
                this.ResizeBottom();
                this.lnkMore.Text = "����...";
            }
            else
            {
                //sheeViewDetail.Sheets[0].RowCount
                for (int i = 5; i < sheeViewDetail.Sheets[0].RowCount; i++)
                    sheeViewDetail.Sheets[0].SetRowVisible(i, true);
                this.resizebottomFP();
                this.lnkMore.Text = "��ʾ5��";
            }
        }

        /// <summary>
        /// ȫ���ؼ��Ĵ�С
        /// </summary>
        private Size primitiveSize;

        /// <summary>
        /// ��������ϸ��Ϣ�Ŀؼ���С
        /// </summary>
        private Size titlePanelSize;

        public void ResizeBottom()
        {
            sheeViewDetail.ActiveSheet.ColumnHeader.Visible = true;
            sheeViewDetail.ActiveSheet.Rows.Default.Height = 25F;
            sheeViewDetail.ActiveSheet.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.lnkMore.Visible = false;

            if (sheeViewDetail.Sheets[0].Rows.Count == 0 && string.IsNullOrEmpty(Memo))
            {
                this.pnlBottom.Visible = false;
                this.statusStrip1.Visible = false;

                this.Size = titlePanelSize;
            }
            else if (sheeViewDetail.Sheets[0].RowCount > 0 || !string.IsNullOrEmpty(Memo))
            {
                this.pnlBottom.Visible = true;
                this.statusStrip1.Visible = false;

                this.Size = this.primitiveSize;
            }
            else
            {
                this.pnlBottom.Visible = false;
                this.statusStrip1.Visible = false;

                this.Size = this.titlePanelSize;
            }
        }

        private void resizebottomFP()
        {
            //this.pnlBottom.Height = 140 + sheeViewDetail.Sheets[0].RowCount <= 10 ? sheeViewDetail.Sheets[0].RowCount : 10 * (20);
            sheeViewDetail.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Always;

        }

        /// <summary>
        /// �����ϸ�б�
        /// </summary>
        /// <param name="c"></param>
        public void AddBottomControl(Control c)
        {
            if (c.GetType().IsSubclassOf(typeof(FarPoint.Win.Spread.FpSpread)))
            {
                sheeViewDetail = c as FarPoint.Win.Spread.FpSpread;
                //sheeViewDetail.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Always;
                //sheeViewDetail.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Always;
                sheeViewDetail.Sheets[0].GrayAreaBackColor = pnlBottom.BackColor;
                sheeViewDetail.Sheets[0].DefaultStyle.BackColor = pnlBottom.BackColor;
                sheeViewDetail.Sheets[0].RowHeader.DefaultStyle.BackColor = pnlBottom.BackColor;
                this.statusbarText.Text = "��ʾ����չ��Ϣ��ҽ���ο�";
            }
            else
            {
                this.statusbarText.Text = "��ʾ����չ��Ϣ��ҽ���ο�";
            }
            c.Dock = DockStyle.Fill;
            c.Visible = true;
            this.pnlBottom.Controls.Add(c);

            //FS.FrameWork.WinForms.Controls.NeuPanel cPanel = new FS.FrameWork.WinForms.Controls.NeuPanel();
            //cPanel.Dock = DockStyle.Fill;
            //cPanel.Controls.Add(c);

            //this.pnlBottom.Controls.Add(cPanel);
        }

        /// <summary>
        /// ���ע������
        /// </summary>
        /// <param name="c"></param>
        public void AddBottomMemoControl(Control c)
        {
            FS.FrameWork.WinForms.Controls.NeuPanel memoPanel = null;
            if (c.GetType().IsSubclassOf(typeof(FS.FrameWork.WinForms.Controls.NeuPanel)))
            {
                memoPanel = c as FS.FrameWork.WinForms.Controls.NeuPanel;
                memoPanel.BackColor = pnlBottom.BackColor;
            }
            c.Size = new Size(700, 30);
            c.Dock = DockStyle.Top;
            c.Visible = true;

            this.pnlBottom.Controls.Add(c);
        }

        #endregion

        private void lklb_exit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
        }
        #region {E68EC2D3-2E6B-4062-A194-9E3C88B1AA98}
        private void ckbSpell_Click(object sender, EventArgs e)
        {
            if (this.ckbSpell.CheckState == CheckState.Checked)
            {
                this.ckbSpell.CheckState = CheckState.Checked;
                this.chbWB.CheckState = CheckState.Unchecked;
            }
            else
            {
                this.ckbSpell.CheckState = CheckState.Unchecked;
                this.chbWB.CheckState = CheckState.Checked;
            }
            // if (this.chbWB.CheckState == CheckState.Checked && this.ckbSpell.CheckState == CheckState.Checked)
            //{
            //    this.writeWay(2);
            //    return;
            //}
            this.writeWay(this.ckbSpell.CheckState == CheckState.Checked ? 0 : 1);
        }

        private void chbWB_Click(object sender, EventArgs e)
        {
            if (this.chbWB.CheckState == CheckState.Checked)
            {
                this.chbWB.CheckState = CheckState.Checked;
                this.ckbSpell.CheckState = CheckState.Unchecked;
            }
            else
            {
                this.chbWB.CheckState = CheckState.Unchecked;
                this.ckbSpell.CheckState = CheckState.Checked;
            }
            //if (this.chbWB.CheckState == CheckState.Checked && this.ckbSpell.CheckState == CheckState.Checked)
            //{
            //    this.writeWay(2);
            //    return;
            //}
            this.writeWay(this.ckbSpell.CheckState == CheckState.Checked ? 0 : 1);

        }
        #endregion

        private void chkCompantDrug_CheckedChanged(object sender, EventArgs e)
        {
            //if (this.chkCompantDrug.CheckState == CheckState.Checked)
            //{
            //    this.companyRang(true);
            //}
            //else
            //{
            //    this.companyRang(false);
            //}

        }

        private void chkDeptItem_Click(object sender, EventArgs e)
        {
        }
        public void hideDeptUsedCheckBox()
        {
            this.chkDeptItem.CheckState = CheckState.Unchecked;
        }
        public void showDeptUsedCheckBox()
        {
            this.chkDeptItem.CheckState = CheckState.Checked;
        }

        private void cbxIsReal_CheckedChanged(object sender, EventArgs e)
        {
            //this.IsReal = !this.cbxIsReal.Checked;
        }

        private void QueryTypeChanged(object sender, EventArgs e)
        {
            this.SaveUserDefaultSetting();
        }

        private void frmShowItem_Load(object sender, EventArgs e)
        {
            //this.SetDeptItem(false);
        }

        private void chkDeptItem_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkDeptItem.CheckState == CheckState.Checked)
            {
                if (this.isDeptUsedItem != null)
                {
                    this.isDeptUsedItem(true);
                }
            }
            if (this.chkDeptItem.CheckState == CheckState.Unchecked)
            {
                if (this.isDeptUsedItem != null)
                {
                    this.isDeptUsedItem(false);
                }
            }
        }
    }
}