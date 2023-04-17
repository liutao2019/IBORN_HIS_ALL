using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Pharmacy
{
    public partial class ucEasySet : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucEasySet()
        {
            InitializeComponent();
        }        

        /// <summary>
        /// ��ǰFp
        /// </summary>
        internal FarPoint.Win.Spread.SheetView FpSv
        {
            get
            {
                return this.neuSpread1_Sheet1;
            }
            set
            {
                this.neuSpread1_Sheet1 = value;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        internal delegate int DataManagerDelegate();

        /// <summary>
        /// �¼�����
        /// </summary>
        internal event DataManagerDelegate SaveFinishedEvent;

        /// <summary>
        /// ���ݳ�ʼ��
        /// </summary>
        internal event DataManagerDelegate InitDataEvent;

        /// <summary>
        /// Fp��ʽ��
        /// </summary>
        /// <param name="label">��ǩ��ͷ</param>
        /// <param name="width">�п�</param>
        /// <param name="visible">���Ƿ�ɼ�</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        internal int InitFp(string[] label, int[] width, bool[] visible)
        {
            return 1;
        }

        /// <summary>
        /// ���ݳ�ʼ��
        /// </summary>
        /// <returns></returns>
        internal int InitData()
        {
            if (this.InitDataEvent != null)
            {
                return this.InitDataEvent();
            }

            return 1;         
        }

        /// <summary>
        /// ���ڹر�
        /// </summary>
        private void Close()
        { 
            if (this.ParentForm != null)
            {
                this.ParentForm.Close();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            this.InitData();

            base.OnLoad(e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.SaveFinishedEvent != null)
            {
                if (this.SaveFinishedEvent() == 1)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }
    }
}
