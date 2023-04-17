using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.Pharmacy.Common.Check
{
    public partial class frmSetFStory : Form
    {
        public frmSetFStory(string stockDeptNO, string checkBillNO, string batchCheckState,string settingFileName)
        {
            InitializeComponent();

            this.curStockDeptNO = stockDeptNO;
            this.curBillCheckNO = checkBillNO;
            this.curSettingFileName = settingFileName;

            if (batchCheckState == "0")
            {
                this.ncbManagerBatch.Checked = false;
                this.ncbManagerBatch.Enabled = false;
            }
            else if (batchCheckState == "1")
            {
                this.ncbManagerBatch.Checked = true;
                this.ncbManagerBatch.Enabled = false;
            }
            else
            {
                this.ncbManagerBatch.Enabled = true;
            }
            this.Load += new EventHandler(frmSetFStory_Load);
            this.nbtCancel.Click += new EventHandler(nbtCancel_Click);
            this.nbtFAsValid.Click += new EventHandler(nbtFAsValid_Click);
            this.nbtFAsAll.Click += new EventHandler(nbtFAsAll_Click);
            this.neuTreeView1.AfterCheck += new TreeViewEventHandler(neuTreeView1_AfterCheck);
            this.neuTreeView2.AfterCheck += new TreeViewEventHandler(neuTreeView1_AfterCheck);
            this.neuTreeView3.AfterCheck += new TreeViewEventHandler(neuTreeView1_AfterCheck);

        }

        private string curBillCheckNO = "";
        private string curStockDeptNO = "";
        private string curSettingFileName = "";
     
        protected int FStore(string type)
        {
            FS.SOC.HISFC.BizLogic.Pharmacy.Check checkMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Check();
            string SQL = @" select count(*) 
                            from   pha_com_output o 
                            where  o.drug_storage_code = '{0}'
                            and    o.out_state <> '2'
                            and    o.out_date > sysdate - 60";


            string count = checkMgr.ExecSqlReturnOne(string.Format(SQL, this.curStockDeptNO));

            if (count == "-1")
            {
                Function.ShowMessage("检查入库情况发生错误，请与系统管理员联系并报告错误：" + checkMgr.Err, MessageBoxIcon.Error);
                return 0;
            }

            if (FS.FrameWork.Function.NConvert.ToInt32(count) > 0)
            {
                Function.ShowMessage("还存在没有入库的药品，目前不可以封账，请【核准入库】！", MessageBoxIcon.Information);
                return 0;
            }

            int days = 365;
            if (this.ncbDaysValid.Checked)
            {
                if (!int.TryParse(this.ntxtDays.Text, out days))
                {
                    Function.ShowMessage("天数不是正数，请更改！", MessageBoxIcon.Information);
                    this.ntxtDays.Select();
                    this.ntxtDays.SelectAll();
                    this.ntxtDays.Focus();
                    return 0;
                }
                if (days <= 0)
                {
                    Function.ShowMessage("天数必须大于零，请更改！", MessageBoxIcon.Information);
                    this.ntxtDays.Select();
                    this.ntxtDays.SelectAll();
                    this.ntxtDays.Focus();
                    return 0;
                }
            }
            bool isManageBatch = this.ncbManagerBatch.Checked;

            string memo = "";
            if (!string.IsNullOrEmpty(this.curBillCheckNO))
            {
                memo = "系统对已经在单据号" + this.curBillCheckNO + "内的药品不再进行封账";
            }

            DialogResult dr = MessageBox.Show("封账开始前，请通知库房所有人员停止出入库、调价等其他业务操作\n" + memo + "\n确认封账吗?", "提示>>", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel)
            {
                return 0;
            }
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("请稍候...");
            Application.DoEvents();

            List<FS.HISFC.Models.Pharmacy.Item> alItem = checkMgr.QueryItemList();
            if (alItem == null)
            {
                Function.ShowMessage("获取药品基本信息发生错误，请与系统管理员联系并报告错误：" + checkMgr.Err, MessageBoxIcon.Error);
                return 0;
            }

            Hashtable hsItem = new Hashtable();
            foreach (FS.HISFC.Models.Pharmacy.Item item in alItem)
            {
                hsItem.Add(item.ID, item);
            }


            //对所有药品进行封帐处理
            DateTime curFOperTime = checkMgr.GetDateTimeFromSysDateTime();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            ArrayList alFStored = new ArrayList();
            ArrayList alDetail = checkMgr.CloseAll(curStockDeptNO, isManageBatch, days);

            if (alDetail == null)
            {
                Function.ShowMessage("封账发生错误，请与系统管理员联系并报告错误：" + checkMgr.Err, MessageBoxIcon.Error);
                return 0;
            }

            if (alDetail.Count == 0)
            {
                Function.ShowMessage("没有库存!", MessageBoxIcon.Information);
                return 0;
            }

            FS.HISFC.Models.Pharmacy.Check checkStatic = new FS.HISFC.Models.Pharmacy.Check();

            checkStatic.CheckNO = this.curBillCheckNO;				            //盘点单号
            checkStatic.StockDept.ID = this.curStockDeptNO;			        //库房编码
            checkStatic.State = "0";					            //封帐状态
            checkStatic.User01 = "0";						        //盘亏金额
            checkStatic.User02 = "0";						        //盘盈金额

            checkStatic.FOper.ID = checkMgr.Operator.ID;   //封帐人
            checkStatic.FOper.OperTime = curFOperTime;				    //封帐时间
            checkStatic.Operation.Oper = checkStatic.FOper;               //操作人


            //获取新盘点单号
            if (string.IsNullOrEmpty(this.curBillCheckNO))
            {
                this.curBillCheckNO = checkMgr.GetCheckCode(this.curStockDeptNO);
                if (string.IsNullOrEmpty(this.curBillCheckNO))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("获取盘点单号发生错误，请与系统管理员联系并报告错误：" + checkMgr.Err, MessageBoxIcon.Error);
                    return 0;
                }

                checkStatic.CheckNO = this.curBillCheckNO;				            //盘点单号
               
                if (checkMgr.InsertCheckStatic(checkStatic) != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("插入盘点汇总信息发生错误，请与系统管理员联系并报告错误：" + checkMgr.Err, MessageBoxIcon.Error);
                    return 0;
                }
            }
            else
            {
                alFStored = checkMgr.QueryCheckDetailByCheckCode(this.curStockDeptNO, this.curBillCheckNO);
                if (alFStored == null)
                {
                    Function.ShowMessage("封账发生错误，请与系统管理员联系并报告错误：" + checkMgr.Err, MessageBoxIcon.Error);
                    return 0;
                }
            }

           

            foreach (FS.HISFC.Models.Pharmacy.Check checkDetail in alDetail)
            {
                bool isFStored = false;
                foreach (FS.HISFC.Models.Pharmacy.Check c in alFStored)
                {
                    //这个地方可能会发生前后两次是否分批盘点模式不一样
                    if (c.Item.ID == checkDetail.Item.ID)
                    {
                        isFStored = true;
                        break;
                    }
                }

                if (isFStored)
                {
                    continue;
                }

                checkDetail.CheckNO = checkStatic.CheckNO;

                decimal purchasePrice = 0;
                if (isManageBatch)
                {
                    purchasePrice = checkDetail.Item.PriceCollection.PurchasePrice;
                }
                checkDetail.Item = hsItem[checkDetail.Item.ID] as FS.HISFC.Models.Pharmacy.Item;
                if (isManageBatch)
                {
                    checkDetail.Item.PriceCollection.PurchasePrice = purchasePrice;
                }
                if (this.ncbGlobalValid.Checked && checkDetail.Item.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
                {
                    continue;
                }
                if (type == "1")
                {
                    if (!this.CheckTypeSelected("0", checkDetail.Item.Type.ID)
                        || !this.CheckTypeSelected("1", checkDetail.Item.Quality.ID)
                        || !this.CheckTypeSelected("2", checkDetail.Item.DosageForm.ID)
                        )
                    {
                        continue;
                    }
                }
                checkDetail.Operation = checkStatic.Operation;

                checkDetail.AdjustQty = checkDetail.PackQty * checkDetail.Item.PackQty + checkDetail.MinQty;
                checkDetail.CStoreQty = checkDetail.AdjustQty;
                checkDetail.ProfitLossQty = checkDetail.AdjustQty - checkDetail.FStoreQty;

                if (checkDetail.ProfitLossQty < 0)
                {
                    checkDetail.ProfitStatic = "0";
                }
                else if (checkDetail.ProfitLossQty == 0)
                {
                    checkDetail.ProfitStatic = "2";
                }
                else
                {
                    checkDetail.ProfitStatic = "1";
                }

                //对盘点明细表插入数据
                if (checkMgr.InsertCheckDetail(checkDetail) != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("插入盘点明细信息发生错误，请与系统管理员联系并报告错误：" + checkMgr.Err, MessageBoxIcon.Error);
                    return 0;
                }

            }

            FS.FrameWork.Management.PublicTrans.Commit();

            SOC.Public.XML.SettingFile.SaveSetting(this.curSettingFileName, "FStoreSetting", "Days", this.ntxtDays.Text);

            return 1;
        }

        protected int Init()
        {
            this.neuTreeView1.CheckBoxes = true;
            this.neuTreeView2.CheckBoxes = true;
            this.neuTreeView3.CheckBoxes = true;

            this.ntxtDays.Text = SOC.Public.XML.SettingFile.ReadSetting(this.curSettingFileName, "FStoreSetting", "Days", this.ntxtDays.Text);
            

            //FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
            //ArrayList al = constantMgr.GetAllList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE);

            SOC.HISFC.BizProcess.Cache.Common.InitDrugType();
            SOC.HISFC.BizProcess.Cache.Common.GetDrugQualityName("--");
            SOC.HISFC.BizProcess.Cache.Common.GetDrugDoseModualName("--");

            TreeNode nodeType = new TreeNode();
            nodeType.Text = "药品类别";
            nodeType.ImageIndex = 0;
            nodeType.SelectedImageIndex = 1;
            nodeType.Tag = "Type";
            this.neuTreeView1.Nodes.Add(nodeType);

            foreach (FS.HISFC.Models.Base.Const c in SOC.HISFC.BizProcess.Cache.Common.drugTypeHelper.ArrayObject)
            {
                TreeNode n = new TreeNode();
                n.Tag = c;
                n.Text = c.Name;
                n.ImageIndex = 0;
                n.SelectedImageIndex = 1;
                nodeType.Nodes.Add(n);
            }

            this.neuTreeView1.ExpandAll();

            //al = constantMgr.GetAllList(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY);

            TreeNode nodeQuality = new TreeNode();
            nodeQuality.Text = "药品性质";
            nodeQuality.ImageIndex = 0;
            nodeQuality.SelectedImageIndex = 1;
            nodeQuality.Tag = "Type";
            this.neuTreeView2.Nodes.Add(nodeQuality);

            foreach (FS.HISFC.Models.Base.Const c in SOC.HISFC.BizProcess.Cache.Common.drugQualityHelper.ArrayObject)
            {
                TreeNode n = new TreeNode();
                n.Tag = c;
                n.Text = c.Name;
                n.ImageIndex = 0;
                n.SelectedImageIndex = 1;
                nodeQuality.Nodes.Add(n);
            }

            this.neuTreeView2.ExpandAll();


            //al = constantMgr.GetAllList(FS.HISFC.Models.Base.EnumConstant.DOSAGEFORM);




            Hashtable hsDosageForm = new Hashtable();

            foreach (FS.HISFC.Models.Base.Const c in SOC.HISFC.BizProcess.Cache.Common.drugDoseModualHelper.ArrayObject)
            {
                TreeNode n = new TreeNode();
                n.Tag = c;
                n.Text = c.Name;
                n.ImageIndex = 0;
                n.SelectedImageIndex = 1;
                TreeNode nodeDosageForm = new TreeNode();

                if (!hsDosageForm.Contains(c.Memo))
                {
                    nodeDosageForm.Text = "药品剂型";
                    if (!string.IsNullOrEmpty(c.Memo))
                    {
                        nodeDosageForm.Text = "药品剂型-" + c.Memo;
                    }
                    nodeDosageForm.ImageIndex = 0;
                    nodeDosageForm.SelectedImageIndex = 1;
                    nodeDosageForm.Tag = "Type";
                    this.neuTreeView3.Nodes.Add(nodeDosageForm);
                    hsDosageForm.Add(c.Memo, nodeDosageForm);
                }
                else
                {
                    nodeDosageForm = hsDosageForm[c.Memo] as TreeNode;
                }
                nodeDosageForm.Nodes.Add(n);
            }
            if (this.neuTreeView3.Nodes.Count == 1)
            {
                this.neuTreeView3.ExpandAll();
            }
            return 1;
        }

        private bool CheckTypeSelected(string type, string typeNO)
        {
            ArrayList alNode = new ArrayList();
            if (type == "0")
            {
                alNode.AddRange(this.neuTreeView1.Nodes[0].Nodes);
            }
            else if (type == "1")
            {
                alNode.AddRange(this.neuTreeView2.Nodes[0].Nodes);
            }
            else if (type == "2")
            {
                foreach (TreeNode node in this.neuTreeView3.Nodes)
                {
                    alNode.AddRange(node.Nodes);
                }
            }
            if (alNode.Count == 0)
            {
                return true;
            }
            foreach (TreeNode node in alNode)
            {
                if (node.Checked)
                {
                    FS.HISFC.Models.Base.Const c = node.Tag as FS.HISFC.Models.Base.Const;
                    if (c.ID == typeNO)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        void neuTreeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                foreach (TreeNode node in e.Node.Nodes)
                {
                    node.Checked = e.Node.Checked;
                }
            }
        }

        void nbtFAsAll_Click(object sender, EventArgs e)
        {
            if (this.neuTreeView1.Nodes.Count == 0)
            {
                return;
            }
            bool isChosen = false;
            foreach (TreeNode node in this.neuTreeView1.Nodes[0].Nodes)
            {
                if (node.Checked)
                {
                    isChosen = true;
                    break;
                }
            }
            if (!isChosen)
            {
                foreach (TreeNode node in this.neuTreeView2.Nodes[0].Nodes)
                {
                    if (node.Checked)
                    {
                        isChosen = true;
                        break;
                    }
                }
            }
            if (!isChosen)
            {
                Function.ShowMessage("请您选择封账范围：药品类别或者药品剂型!", MessageBoxIcon.Information);
                return;
            }
            if (this.FStore("1") == 1)
            {
                Function.ShowMessage("封账完成!", MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
        }

        void nbtFAsValid_Click(object sender, EventArgs e)
        {
            if (this.FStore("0") == 1)
            {
                Function.ShowMessage("封账完成!", MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
        }

        void nbtCancel_Click(object sender, EventArgs e)
        {
            //如果窗口关闭后还调用其中的变量等就等于访问了无效的内存，所以只能Hide
            //this.Hide();

            this.DialogResult = DialogResult.Cancel;
        }

        void frmSetFStory_Load(object sender, EventArgs e)
        {
            this.Init();
        }
    }
}
