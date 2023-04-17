using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FSDataWindow.Controls
{
    public partial class frmPreviewDataWindow45 : Form
    {
        public frmPreviewDataWindow45()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ��ǰ���ݴ���
        /// </summary>
        protected NeuDataWindow45 dwControl = null;

        #endregion

        #region ����

        /// <summary>
        /// ��ǰ���ݴ���
        /// </summary>
        public NeuDataWindow45 PreviewDataWindow 
        {
            get 
            {
                return this.dwControl;
            }
            set
            {
                this.dwControl = value;


                this.ShareData();
            }
        }

        #endregion

        #region ����

        protected virtual void ShareData()
        {
            if (this.dwControl == null) 
            {
                return;
            }
   //             dw_show.setredraw(false)
   // dw_show.create(adw_data.describe("datawindow.syntax"))
   // dw_show.settransobject(sqlca)
   // //Ϊ���ܹ���ʾ�������ݴ����е�����
   // dw_show.deleterow(dw_show.insertrow(0))
   // if  adw_data.rowcount() > 0 then
   //     dw_show.object.data = adw_data.object.data
   //     //
		
   // end if
	
   // //��ʼ�����ں�Ԥ�����ݴ���
   // //this.bringtotop = TRUE
   // //this.windowstate = maximized!
   // dw_show.Object.DataWindow.Print.Preview = true
   // dw_show.bringtotop = true
   // //ȷ��Ԥ�����ڵ���ʾ����
   // wf_iffull()
   //dw_show.setredraw(true)

            this.dwPreview.SetRedrawOff();
            this.dwPreview.Create(this.dwControl.Describe("datawindow.syntax"));
            this.dwControl.ShareData(this.dwPreview);
            this.dwPreview.PrintProperties.Preview = true;
            this.dwPreview.SetRedrawOn();
        }

        #endregion

        private void tbPrint_Click(object sender, EventArgs e)
        {
            this.dwPreview.Print();
        }

        protected override void OnClosed(EventArgs e)
        {
            
            this.DialogResult = DialogResult.OK;

            base.OnClosed(e);
        }
    }
}