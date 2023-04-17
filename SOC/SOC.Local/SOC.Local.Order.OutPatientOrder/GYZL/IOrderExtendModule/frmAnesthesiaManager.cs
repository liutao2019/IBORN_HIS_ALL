using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Order.OutPatientOrder.GYZL.IOrderExtendModule
{

    public partial class frmAnesthesiaManager : FS.FrameWork.WinForms.Forms.BaseForm, 
        FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
    {
        #region ���캯��
        public frmAnesthesiaManager()
        {
            InitializeComponent();
            this.toolStrip1.ItemClicked += new ToolStripItemClickedEventHandler(toolStrip1_ItemClicked);
        }
        #endregion
        #region IOutPatientOrderPrint ��Ա

        public int PrintOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            return 1;
        }

        public int PrintOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder, bool isPreview)
        {
            if (regObj == null)
            {
                return -1;
            }
            FS.HISFC.Models.RADT.Patient curPatient = new FS.HISFC.Models.RADT.Patient();
            curPatient = regObj as FS.HISFC.Models.RADT.Patient;
            this.Init(curPatient);
            this.ShowDialog();
            return 1;
        }

        public void PreviewOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.Generic.IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            return;
        }

        /// <summary>
        /// ���û��߻�����Ϣ
        /// </summary>
        /// <param name="register"></param>
        public void SetPatientInfo(FS.HISFC.Models.Registration.Register register)
        {
            return;
        }

        /// <summary>
        /// ���ô�ӡ
        /// </summary>
        /// <param name="IList"></param>
        public void SetPrintValue(IList<FS.HISFC.Models.Order.OutPatient.Order> IList, string judPrint, bool isPreview)
        {
            return;
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        private void PrintPage(string judPrint)
        {
            return;
        }

        #endregion

        private FS.SOC.Local.Order.OutPatientOrder.GYZL.IOrderExtendModule.ucCardInfo ucCardInfo = null;

        public void Init(FS.HISFC.Models.RADT.Patient patient)
        {
            if (ucCardInfo == null)
            {
                ucCardInfo = new FS.SOC.Local.Order.OutPatientOrder.GYZL.IOrderExtendModule.ucCardInfo(patient);
            }
            else
            {
                this.ucCardInfo.SetValue(patient);
            }
            this.AddControl();
            this.AddToolStripItem();
        }

        /// <summary>
        /// ��ӿؼ�
        /// </summary>
        private void AddControl()
        {
            this.panelFill.Controls.Add(this.ucCardInfo);
            ucCardInfo.Dock = DockStyle.Fill;
        }

        /// <summary>
        /// ��ӹ�����
        /// </summary>
        private void AddToolStripItem()
        {
            this.toolStrip1.Items.Clear();

            this.toolStrip1.Items.Add("����", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.B����));
            this.toolStrip1.Items.Add("�˳�", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T�˳�));

            foreach (ToolStripItem t in this.toolStrip1.Items)
            {
                t.TextImageRelation = TextImageRelation.ImageAboveText;
            }
        }

        public void SetControlProperty(FS.HISFC.Models.RADT.Patient patient)
        {
            this.ucCardInfo.SetValue(patient);
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if ("����".Equals(e.ClickedItem.Text))
            {
                if (this.ucCardInfo.SavePatientInfo() > 0)
                {
                    MessageBox.Show("����ɹ���");
                    return;
                }
            }
            else if ("�˳�".Equals(e.ClickedItem.Text))
            {
                this.Hide();
            }
        }

        private void frmAnesthesiaManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
            }
        }

        #region IOutPatientOrderPrint Members


        public void SetPage(string pageStr)
        {
        }

        #endregion
    }
}

