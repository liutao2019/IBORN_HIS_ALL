using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Neusoft.UFC.Common.Controls;

namespace UFC.Pharmacy
{
    /// <summary>
    /// [��������: ����������ര�� ʵ���Զ��崰�ڹ���]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-04]<br></br>
    /// <˵��>
    ///     1 ÿ�����ð�ť��ʾǰ �����ð�ť 
    /// </˵��>
    /// </summary>
    public partial class frmIMABaseForm : Neusoft.NFC.Interface.Forms.BaseStatusBar
    {
        public frmIMABaseForm()
        {
            InitializeComponent();

            this.WindowState = FormWindowState.Maximized;
            this.ProgressRun(true);
        }

        private Neusoft.UFC.Common.Controls.ucIMAInOutBase ucBaseCompoent = null;

        /// <summary>
        /// ���Ӱ�ť
        /// </summary>
        private System.Collections.Hashtable hsAddButton = null;

        /// <summary>
        /// ��ӹ������
        /// </summary>
        /// <param name="ucBaseCompoent"></param>
        /// <returns></returns>
        protected int AddIMABaseCompoent(Neusoft.UFC.Common.Controls.ucIMAInOutBase ucBaseCompoent)
        {
            try
            {
                this.ucBaseCompoent = ucBaseCompoent;

                this.ctrlPanel.Controls.Clear();                

                ucBaseCompoent.Dock = DockStyle.Fill;

                ucBaseCompoent.BackColor = System.Drawing.Color.MintCream;

                ucBaseCompoent.SetToolButtonVisibleEvent += new ucIMAInOutBase.SetToolButtonVisibleHandler(ucBaseCompoent_SetToolButtonVisibleEvent);

                ucBaseCompoent.AddToolButtonEvent += new ucIMAInOutBase.AddToolButtonHandler(ucBaseCompoent_AddToolButtonEvent);

                this.ctrlPanel.Controls.Add(ucBaseCompoent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Neusoft.NFC.Management.Language.Msg(ex.Message));
                return -1;
            }
            return 1;
        }
       
        /// <summary>
        /// ������ť
        /// </summary>
        /// <param name="buttonText"></param>
        /// <param name="trsipDescription"></param>
        /// <param name="image"></param>
        /// <param name="locationIndex"></param>
        /// <param name="isAddSeparator"></param>
        /// <param name="e"></param>
        protected virtual void AddToolButton(string buttonText, string trsipDescription, System.Drawing.Image image, int locationIndex, bool isAddSeparator, System.EventHandler e)
        {
            if (isAddSeparator)                 //���ӷָ���
            {
                System.Windows.Forms.ToolStripSeparator separtor = new ToolStripSeparator();
                this.toolStrip1.Items.Insert(locationIndex, separtor);

                if (this.hsAddButton == null)
                {
                    this.hsAddButton = new System.Collections.Hashtable();
                }
                this.hsAddButton.Add(this.hsAddButton.Count, separtor);
                locationIndex = locationIndex + 1;
            }

            System.Windows.Forms.ToolStripButton trisButton = new ToolStripButton();

            trisButton.Text = buttonText;                   //��ť����
            trisButton.ToolTipText = trsipDescription;      //��ť��ʾ
            trisButton.TextImageRelation = TextImageRelation.ImageAboveText;
            trisButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            trisButton.Image = image;

            trisButton.Tag = e;

            this.toolStrip1.Items.Insert(locationIndex, trisButton);

            if (this.hsAddButton == null)
            {
                this.hsAddButton = new System.Collections.Hashtable();
            }
            this.hsAddButton.Add(this.hsAddButton.Count, trisButton);
            //image.Dispose();
        }

        /// <summary>
        /// ����������
        /// </summary>
        protected virtual void ResetToolBar()
        {
            if (this.hsAddButton != null)
            {
                foreach (ToolStripItem stripItem in this.hsAddButton.Values)
                {
                    this.toolStrip1.Items.Remove(stripItem);
                }

                this.hsAddButton.Clear();
                this.hsAddButton = null;
            }
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <returns></returns>
        protected virtual int OnDel()
        {
            this.ucBaseCompoent.OnDelete();

            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        protected virtual int OnSave()
        {
            this.ucBaseCompoent.Save(null, null);

            return 1;
        }

        /// <summary>
        /// ���뵥
        /// </summary>
        /// <returns></returns>
        protected virtual int OnApplyList()
        {
            this.ucBaseCompoent.OnApplyList();

            return 1;
        }

        /// <summary>
        /// ��ⵥ
        /// </summary>
        /// <returns></returns>
        protected virtual int OnInList()
        {
            this.ucBaseCompoent.OnInList();

            return 1;
        }

        /// <summary>
        /// ���ⵥ
        /// </summary>
        /// <returns></returns>
        protected virtual int OnOutList()
        {
            this.ucBaseCompoent.OnOutList();

            return 1;
        }

        /// <summary>
        /// �ɹ���
        /// </summary>
        /// <returns></returns>
        protected virtual int OnStockList()
        {
            this.ucBaseCompoent.OnStockList();

            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        protected virtual int OnExport()
        {
            this.ucBaseCompoent.OnExport();

            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        protected virtual int OnImport()
        {
            this.ucBaseCompoent.OnImport();

            return 1;
        }

        /// <summary>
        /// ���ù�������ť��ʾ���
        /// </summary>
        /// <param name="isShowApplyButton"></param>
        /// <param name="isShowInButton"></param>
        /// <param name="isShowOutButton"></param>
        /// <param name="isShowStockButton"></param>
        /// <param name="isShowDelButton"></param>
        /// <param name="isShowExport"></param>
        /// <param name="isShowImport"></param>
        protected virtual void SetToolBarButtonVisible(bool isShowApplyButton, bool isShowInButton, bool isShowOutButton, bool isShowStockButton, bool isShowDelButton, bool isShowExport, bool isShowImport)
        {
            this.ResetToolBar();

            this.tsbApplyList.Visible = isShowApplyButton;
            this.tsbInList.Visible = isShowInButton;
            this.tsbOutList.Visible = isShowOutButton;
            this.tsbStockList.Visible = isShowStockButton;
            this.tsbDel.Visible = isShowDelButton;
            this.tsbExport.Visible = isShowExport;
            this.tsbImport.Visible = isShowImport;
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == this.tsbExit)      //�˳�
            {
                this.Close();
            }
            if (e.ClickedItem == this.tsbDel)       //ɾ��
            {
                this.OnDel();
            }
            if (e.ClickedItem == this.tsbSave)      //����
            {
                this.OnSave();
            }
            if (e.ClickedItem == this.tsbApplyList) //���뵥
            {
                this.OnApplyList();
            }
            if (e.ClickedItem == this.tsbInList)    //��ⵥ
            {
                this.OnInList();
            }
            if (e.ClickedItem == this.tsbOutList)   //���ⵥ
            {
                this.OnOutList();
            }
            if (e.ClickedItem == this.tsbStockList)//�ɹ���
            {
                this.OnStockList();
            }
            if (e.ClickedItem == this.tsbExport)    //����
            {
                this.OnExport();
            }
            if (e.ClickedItem == this.tsbImport)    //����
            {
                this.OnImport();
            }
            if (e.ClickedItem.Tag == null)
            {
                return;
            }

            System.EventHandler eHandler = e.ClickedItem.Tag as System.EventHandler;
            if (eHandler != null)
            {
                eHandler(null, System.EventArgs.Empty);
            }
        }

        private void ucBaseCompoent_AddToolButtonEvent(string text, string toolstrip, Image image, int location, bool isAddSeparator, EventHandler e)
        {
            this.AddToolButton(text, toolstrip, image, location, isAddSeparator, e);
        }

        private void ucBaseCompoent_SetToolButtonVisibleEvent(bool isShowApply, bool isShowIn, bool isShowOut, bool isShowStock, bool isShowDel, bool isShowExport, bool isShowImport)
        {
            this.SetToolBarButtonVisible(isShowApply, isShowIn, isShowOut, isShowStock, isShowDel, isShowExport, isShowImport);
        }

    }
}