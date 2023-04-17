using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Base;
using System.Collections;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using System.Net;

namespace FS.HISFC.Components.Speciment.Print
{
    public partial class ucOColForm : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private OperApplyManage operApplyManage = new OperApplyManage();
        private OperApply operApply = new OperApply();
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        private string printer = "";
        private string canPrint = "";

        public string CanPrint
        {
            set
            {
                canPrint = value;
            }
            get
            {
                return canPrint;
            }
        }

        /// <summary>
        /// 打印机的名字
        /// </summary>
        public string Printer
        {
            get
            {
                return printer;
            }
            set
            {
                printer = value;
            }
        }

        public ucOColForm()
        {
            InitializeComponent();
        }

        private string GenerateSql()
        {
            string sql = "select distinct * from SPEC_OPERAPPLY where ID > 0";
            if (dtpStart.Value != null)
                sql += " and OPERTIME>=to_date('" + dtpStart.Value.Date.ToString() + "','yyyy-mm-dd hh24:mi:ss')";
            if (dtpEnd.Value != null)
                sql += " and OPERTIME<=to_date('" + dtpEnd.Value.AddDays(1.0).Date.ToString() + "','yyyy-mm-dd hh24:mi:ss')";
            if (cmbOperDept.Tag != null && cmbOperDept.Text != "") sql += " and OPERDEPTNAME = '" + cmbOperDept.Text.Trim() + "'";
            return sql;
        }

        /// <summary>
        /// 加载手术申请单
        /// </summary>
        private void LoadOperApply()
        {
            string sql = GenerateSql();
            ArrayList arrList = operApplyManage.GetOperApplyBySql(sql);
            if (arrList == null || arrList.Count <= 0)
            {
                return;
            }
            foreach (OperApply apply in arrList)
            {
                if (apply.HadCollect == "0")
                {
                    TreeNode tnNotGet = tvApply.Nodes[0];
                    TreeNode tn = new TreeNode();
                    tn.Text = apply.PatientName + " [" + apply.OperName + " " + apply.MainDiaName + "] ";
                    tn.Tag = apply;
                    tnNotGet.Nodes.Add(tn);
                }
                if (apply.HadCollect == "3")
                {
                    TreeNode tnNotGet = tvApply.Nodes[1];
                    TreeNode tn = new TreeNode();
                    tn.Text = apply.PatientName + " [" + apply.OperName + " " + apply.MainDiaName + "] ";
                    tn.Tag = apply;
                    tnNotGet.Nodes.Add(tn);
                }
            }
            if (tvApply.Nodes[0].Nodes.Count > 0)
            {
                tvApply.SelectedNode = tvApply.Nodes[0].Nodes[0];
            }
            tvApply.Nodes[0].Text = "未取样本  (共:" + tvApply.Nodes[0].Nodes.Count.ToString() + " 例)";
            tvApply.Nodes[1].Text = "已取消样本  (共:" + tvApply.Nodes[1].Nodes.Count.ToString() + " 例)";
        }


        /// <summary>
        /// 取消样本
        /// </summary>
        private void CancelSpec()
        {
            DialogResult d = MessageBox.Show("确定取消?", "取消", MessageBoxButtons.YesNo);
            if (d == DialogResult.No)
            {
                return;
            }
            TreeNode tn = tvApply.SelectedNode;
            if (tn == null)
            {
                MessageBox.Show("请选择需要取消的样本");
                return;
            }
            if (tn.Tag == null)
            {
                MessageBox.Show("获取样本信息失败");
                return;
            }

            try
            {
                OperApply operApply = tn.Tag as OperApply;
                if (operApplyManage.UpdateColFlag(operApply.OperApplyId.ToString(), "3") == -1)
                {
                    MessageBox.Show("操作失败");
                    return;
                }
                MessageBox.Show("操作成功");
            }
            catch
            {
                MessageBox.Show("已送标本不能取消！");
            }
        }

        /// <summary>
        /// 撤销取消标本
        /// </summary>
        private void ReCancelSpec()
        {
            TreeNode tn = tvApply.SelectedNode;
            if (tn == null)
            {
                MessageBox.Show("请选择需要撤销取消的样本");
                return;
            }
            if (tn.Tag == null)
            {
                MessageBox.Show("获取样本信息失败");
                return;
            }

            try
            {
                OperApply operApply = tn.Tag as OperApply;
                if (operApplyManage.UpdateColFlag(operApply.OperApplyId.ToString(), "0") == -1)
                {
                    MessageBox.Show("操作失败");
                    return;
                }
                MessageBox.Show("操作成功");
            }
            catch
            {
                //MessageBox.Show("已送标本不能取消！");
            }
        }

        /// <summary>
        /// 根据手术申请单设置页面
        /// </summary>
        /// <param name="operApply">手术申请单</param>
        private void GetSpecInfoByOperApply()
        {
            lblName.Text = operApply.PatientName;
            lblInHosNum.Text = operApply.InHosNum;
            lblOperTime.Text = operApply.OperTime.ToString();
            lblOperAdd.Text = operApply.OperLocation;
            lblMainDoc.Text = operApply.MediDoc.MainDoc.Name;
            lblDiagnose.Text = operApply.MainDiaName;
        }

        protected override int OnPrintPreview(object sender, object neuObject)
        {
            try
            {
                 
                FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
                p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;               
                
                p.PrintPreview(0, 0, neuPanel2);                
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            return base.OnPrintPreview(sender, neuObject);            
        }

        private void tvApply_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //如果选择是根部节点返回
            TreeNode tn = tvApply.SelectedNode;          
            if (tn == null)
                return;
            if (tn.Tag == null || tn.Tag.ToString() == "0")
            {
                return;
            }
            operApply = tn.Tag as OperApply;
            if (operApply == null)
            {
                return;
            }
            if (tn.Parent.Tag.ToString() == "0")
            {
                GetSpecInfoByOperApply();
            }             
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            try
            {
                if (tabControl1.SelectedTab == tabPage1)
                {
                    FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
                    p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                    p.PrintPreview(this.neuPanel2);
                }
                if (tabControl1.SelectedTab == tabPage2)
                {
                    IPAddress[] addressList = Dns.GetHostAddresses(Dns.GetHostName());
                    string ip = addressList[0].ToString();
                    ip = "10.10.10.171";

                    foreach (string s in canPrint.Split(','))
                    {
                        if (s.Trim() == ip.Trim())
                        {
                            foreach (TreeNode tn in tvApply.Nodes[0].Nodes)
                            {
                                if (tn.Checked)
                                {
                                    OperApply o = tn.Tag as OperApply;
                                    ucOperCard ucOC = new ucOperCard();
                                    ucOC.HisBarCode = o.OperApplyId.ToString().PadLeft(12, '0');
                                    ucOC.Name = o.PatientName;
                                    ucOC.Loc = o.OperLocation;
                                    ucOC.InHosNum = o.InHosNum;
                                    ucOC.Time = o.OperTime.ToString();
                                    ucOC.Diagnose = o.MainDiaName + "  (" + o.OperName + ")";
                                    ucOC.Printer = this.printer;
                                    ucOC.Print();
                                }
                            }
                            return 0;
                        }
                    }
                }
                MessageBox.Show("打印不了！");
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            return base.OnPrint(sender, neuObject);
        }

        private void ucOColForm_Load(object sender, EventArgs e)
        {
            ArrayList alDepts = new ArrayList();
            Employee loginPerson = new Employee();
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

            FS.HISFC.BizLogic.Manager.DepartmentStatManager manager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            alDepts = manager.GetMultiDept(loginPerson.ID);           
            this.cmbOperDept.AddItems(alDepts);
            Query(null, null);
        }

        public override int Query(object sender, object neuObject)
        {
            try
            {
                tvApply.Nodes[0].Nodes.Clear();
                LoadOperApply();
            }
            catch
            { }
            return base.Query(sender, neuObject);

        }

        private void grp_Enter(object sender, EventArgs e)
        {

        }

        private void tvApply_AfterCheck(object sender, TreeViewEventArgs e)
        {
        }

        private void tvApply_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode tn = e.Node;
            if (tn.Level == 0)
            {
                if (tvApply.Nodes[0].Checked)
                {
                    for (int i = 0; i < tvApply.Nodes[0].Nodes.Count; i++)
                    {
                        tvApply.Nodes[0].Nodes[i].Checked = true;
                    }
                }
                if (!tvApply.Nodes[0].Checked)
                {
                    for (int i = 0; i < tvApply.Nodes[0].Nodes.Count; i++)
                    {
                        tvApply.Nodes[0].Nodes[i].Checked = false;
                    }
                }
            }
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
             //this.toolBarService.AddToolButton("打印位置信息", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.A打印, true, false, null);
            this.toolBarService.AddToolButton("取消样本", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);

            this.toolBarService.AddToolButton("撤销取消", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.F返回, true, false, null);

            return this.toolBarService;
            //return base.OnInit(sender, neuObject, param);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                 case "取消样本":
                    this.CancelSpec();
                    Query(null, null);
                    break;
                 case "撤销取消":
                    this.ReCancelSpec();
                    Query(null, null);
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

    }
}
