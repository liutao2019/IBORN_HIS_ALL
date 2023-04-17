using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Operation;

namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// [��������: ����Ǽǿؼ�]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2006-12-14]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucRegistrationAnae : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucRegistrationAnae()
        {
            InitializeComponent();
        }

        #region �ֶ�

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #endregion

        #region �¼�

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("ȡ��", "ȡ��", 1, true, false, null);
            return this.toolBarService;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            if (neuDateTimePicker1.Value > neuDateTimePicker2.Value)
            {
                MessageBox.Show("��ʼʱ�䲻�ܴ��ڽ���ʱ��");
                return -1;
            }
            string strBegin = neuDateTimePicker1.Value.Year.ToString() + "-" + neuDateTimePicker1.Value.Month.ToString() + "-" + neuDateTimePicker1.Value.Day.ToString() + " 00:00:00";
            string strEnd = neuDateTimePicker2.Value.Year.ToString() + "-" + neuDateTimePicker2.Value.Month.ToString() + "-" + neuDateTimePicker2.Value.Day.ToString() + " 23:59:59";
            this.ucRegistrationTree1.RefreshList(FS.FrameWork.Function.NConvert.ToDateTime(strBegin), FS.FrameWork.Function.NConvert.ToDateTime(strEnd));
            return base.OnQuery(sender, neuObject);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            TreeNode currentSelectNode = new TreeNode();
            currentSelectNode = this.ucRegistrationTree1.SelectedNode;
            if (currentSelectNode == null)
                return -1;
            if (currentSelectNode.Tag == null)
                return -1;
            if (currentSelectNode.Parent == null || currentSelectNode.Parent.Tag == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ѡ��Ǽǻ���"));
                return -1;
            }
            if (currentSelectNode.Parent.Tag.ToString() == "NO_Register" || currentSelectNode.Parent.Tag.ToString() == "Cancel")
            {
                if (this.ucRegistrationAnaeForm1.Save(ucRegistrationAnaeForm.OperType.Reco) >= 0)
                {
                    this.OnQuery(sender, neuObject);
                }
            }
            else if (currentSelectNode.Parent.Tag.ToString() == "Register")
            {

                if (this.ucRegistrationAnaeForm1.Save(ucRegistrationAnaeForm.OperType.Modify) >= 0)
                {
                    this.OnQuery(sender, neuObject);
                }
            }
            this.OnQuery(sender, neuObject);
            return base.OnSave(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.ucRegistrationAnaeForm1.Print();
            return base.OnPrint(sender, neuObject);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "ȡ��")
            {

            }
            base.ToolStrip_ItemClicked(sender, e);
        }
        #endregion

        private void ucRegistrationTree1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode select = this.ucRegistrationTree1.SelectedNode;

            if (select == null)
                return;
            if (select.Tag == null)
                return;

            TreeNode parent = select.Parent;
            if (parent == null) return;

            if (parent.Tag.ToString() == "NO_Register" || parent.Tag.ToString() == "Cancel")
            {
                this.ucRegistrationAnaeForm1.OperationApplication = select.Tag as OperationAppllication;
                this.ucRegistrationAnaeForm1.Focus();
            }
            else if (parent.Tag.ToString() == "Register"||parent.Tag.ToString()=="YES_NO_Register")
            {
                this.ucRegistrationAnaeForm1.AnaeRecord = select.Tag as AnaeRecord;
                this.ucRegistrationAnaeForm1.Focus();
            }
        }

        private void neuDateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (this.neuDateTimePicker2.Value < this.neuDateTimePicker1.Value)
            {
                neuDateTimePicker2.Value = this.neuDateTimePicker1.Value;
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            //{8DA8B1D6-DDD6-4329-B661-F4BDAE45DB66}
            this.ucRegistrationTree1.Init();
            base.OnLoad(e);
        }
    }
}
