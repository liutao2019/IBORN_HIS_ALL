using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
namespace FS.HISFC.Components.Common.Controls
{
    public partial class cmbPayType : FS.FrameWork.WinForms.Controls.NeuComboBox
    {
         public cmbPayType()
        {
            InitializeComponent();
        }

        public cmbPayType(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            this.initControl();
            this.SelectedIndexChanged+=new EventHandler(cmbPayType_SelectedIndexChanged);
        }

        #region "����"
        /// <summary>
        /// �Ƿ񵯳�
        /// </summary>
        private bool bPop = true;

        /// <summary>
        /// ������λ
        /// </summary>
        private string workUnit = "";
        #endregion

        #region "ʵ�����"

        /// <summary>
        /// ����ʵ��
        /// </summary>
        public FS.HISFC.Models.Base.Bank bank = new FS.HISFC.Models.Base.Bank();

        #endregion

        #region"����"
        /// <summary>
        /// ��������

        /// </summary>
        public bool Pop
        {
            get
            {
                return this.bPop;
            }
            set
            {
                this.bPop = value;
            }
        }
        /// <summary>
        /// ������λ
        /// </summary>
        public string WorkUnit
        {
            get
            {
                return this.workUnit;
            }
            set
            {
                this.workUnit = value;
            }
        }
        #endregion 

        #region ����
        /// <summary>
        /// ��ʼ���ؼ�

        /// </summary>
        private void initControl()
        {
            this.Items.Clear();


            //סԺ��ʾ����֧����ʽ��


            try
            {
                //{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
                FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
                //this.AddItems(FS.HISFC.Models.Fee.EnumPayTypeService.List());
                this.AddItems(managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYMODES));
            }
            catch (Exception ex)
            {
                MessageBox.Show("initControl" + ex.Message);
            }
        }

        /// <summary>
        /// ��ʾbank�ؼ�
        /// </summary>
        private void ShowBank()
        {

            FS.FrameWork.WinForms.Forms.BaseForm f;
            f = new FS.FrameWork.WinForms.Forms.BaseForm();

            ucBank Bank = new ucBank();

            Bank.Dock = System.Windows.Forms.DockStyle.Fill;
            f.Controls.Add(Bank);

            Bank.Bank = this.bank;
            f.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            f.Size = new System.Drawing.Size(295, 240);
            f.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            f.Text = "ѡ������";
            f.ShowDialog();
        }

        /// <summary>
        /// ��ȡ֧����ʽ����
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetNameByID(string ID)
        {
            foreach(FS.HISFC.Models.Base.Const con in this.alItems)
            {
                if(con.ID==ID)
                {
                    return con.Name;
                }
            }
            return "";
        }
        #endregion

        #region �¼�
        private void cmbPayType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (this.bPop == false) return;
                if(this.Tag == null || this.Tag.ToString()==string.Empty) return;
                this.bank = new FS.HISFC.Models.Base.Bank();
                //FS.HISFC.Models.Fee.EnumPayType payType =
                //    (FS.HISFC.Models.Fee.EnumPayType)Enum.Parse(typeof(FS.HISFC.Models.Fee.EnumPayType), this.Tag.ToString());
                //switch (payType)
                //{
                //    //��ǿ�
                //    case FS.HISFC.Models.Fee.EnumPayType.DB:

                //        break;
                //    //֧Ʊ
                //    case FS.HISFC.Models.Fee.EnumPayType.CH:
                //        this.ShowBank();
                //        break;
                //    //���ÿ�

                //    case FS.HISFC.Models.Fee.EnumPayType.CD:

                //        break;
                //    //��Ʊ
                //    case FS.HISFC.Models.Fee.EnumPayType.PO:
                //        this.ShowBank();
                //        break;

                //    default:
                //        break;
                //}
                FS.FrameWork.Models.NeuObject payType = this.SelectedItem as FS.FrameWork.Models.NeuObject;
                switch (payType.ID)
                {
                    //��ǿ�
                    case "DB":

                        break;
                    //֧Ʊ
                    case "CH":
                        this.ShowBank();
                        break;
                    //���ÿ�

                    case "CD":

                        break;
                    //��Ʊ
                    case "PO":
                        this.ShowBank();
                        break;

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("cmbPayType_SelectedIndexChanged" + ex.Message);
                return;
            }
        }
        #endregion
    }
}
