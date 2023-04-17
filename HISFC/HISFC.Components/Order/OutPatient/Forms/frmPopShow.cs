using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Order.OutPatient.Forms
{
    public partial class frmPopShow : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmPopShow()
        {
            InitializeComponent();
        }

        #region ����
        private string name = "";//����
        
        public bool isPrice = false;//�Ƿ�������۸�ģʽ
        public bool isDays = false;//�Ƿ��޸Ŀ���������
        string filePath = Application.StartupPath + "\\HISDefaultValue.xml";//�����ļ�·��
        private bool isCancel = true;

        public bool IsCancel
        {
            get { return isCancel; }
            set { isCancel = value; }
        }
        #endregion

        #region ����
        public string ModuleName
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                this.txtname.Text = this.name;
            }
        }
        #endregion
        
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        #region ����
        /// <summary>
		/// ��ݼ�
		/// </summary>
		/// <param name="keyData"></param>
		/// <returns></returns>
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if(keyData == Keys.Enter)
			{
				this.Save();
			}
			if(keyData == Keys.Escape)
			{
				this.name = "1";
				this.Close();
			}
			return base.ProcessDialogKey (keyData);
		}

        /// <summary>
        /// ����
        /// </summary>
        private void Save()
        {
            this.name = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtname.Text.Trim());//{10F8D472-0C7D-4c10-AFDF-11234A37FEFF}

            if (this.name.Length <= 0)
            {
                MessageBox.Show("��������ݲ���Ϊ�գ����������룡");
                return;
            }

            if (this.isPrice || this.isDays)
            {
                decimal price = 0m;
                try
                {
                    price = FS.FrameWork.Function.NConvert.ToDecimal(this.name);
                }
                catch
                {
                    MessageBox.Show("����۸�ĸ�ʽ����ȷ�����������룡");
                    return;
                }
                if (price <= 0)
                {
                    MessageBox.Show("����ļ۸���С�ڻ����0��");
                    return;
                }
                if (price >= 100000)
                {
                    MessageBox.Show("����ļ۸����");
                    return;
                }
            }
            if (this.isDays)
            {

                if (FS.FrameWork.Function.NConvert.ToDecimal(this.name) > 9)
                {
                    MessageBox.Show("�������ܴ���9����Ϊ�Һ���Ч����Ϊ5�죡");
                    return;
                }
                if (System.IO.File.Exists(this.filePath) == false)
                {
                    MessageBox.Show("û���ҵ����������ļ�:" + filePath + ",��֪ͨ��Ϣ����Աά����");
                }
                else
                {
                    //FS.neHISFC.Components.Interface.Classes.Function.SaveDefaultValue("SeeHistoryDays", this.name);
                    FS.FrameWork.WinForms.Classes.Function.SaveDefaultValue("SeeHistoryDays", this.name);
                    MessageBox.Show("������ʾ���ﻼ���б��������ɹ���");
                }
            }

            this.isCancel = false;
            this.Close();
        }
        #endregion 
    }
}