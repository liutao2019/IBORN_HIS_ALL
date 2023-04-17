using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.InpatientFee.Components.Balance
{
    /// <summary>
    /// [功能描述: 欠费平帐患者树]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-4]<br></br>
    /// </summary>
    public partial class tvPatientListForArrear : FS.HISFC.Components.Common.Controls.tvPatientList
    {
        public tvPatientListForArrear()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        DateTime beginTime = DateTime.Now;

        /// <summary>
        /// 结束时间
        /// </summary>
        DateTime endTime = DateTime.Now;

        /// <summary>
        /// 患者列表
        /// </summary>
        private Hashtable hsPatient = new Hashtable();

        /// <summary>
        /// 住院费用业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.InPatient inpatientFeeManager = new FS.HISFC.BizLogic.Fee.InPatient();
        

        public void Init(DateTime beginTime, DateTime endTime)
        {
            this.beginTime = beginTime;
            this.endTime = endTime;

            this.Refresh();
        }

        public override void Refresh()
        {
            //欠费记录（未处理）
            ArrayList al = new ArrayList();
            al.Add("欠费患者（未平帐）");

            ArrayList detail = this.inpatientFeeManager.QueryBalancesBySaveTime(this.beginTime, this.endTime, "0");
            if (detail == null)
            {
                CommonController.CreateInstance().MessageBox("查询未处理欠费记录出错!" + this.inpatientFeeManager.Err, MessageBoxIcon.Error);
                return;
            }
            al.AddRange(detail);

            //al.Add("欠费患者（已平帐）");

            // detail = this.inpatientFeeManager.QueryBalancesBySaveTime(this.beginTime, this.endTime, "1");
            //if (detail == null)
            //{
            //    CommonController.CreateInstance().MessageBox("查询已处理欠费记录出错!" + this.inpatientFeeManager.Err, MessageBoxIcon.Error);
            //    return;
            //}
            //al.AddRange(detail);

            this.SetPatient(al);
        }

        protected override void RefreshList()
        {
            int Branch = 0;
            FS.SOC.HISFC.InpatientFee.BizProcess.RADT radtMananger = new FS.SOC.HISFC.InpatientFee.BizProcess.RADT();
            this.Nodes.Clear();
            for (int i = 0; i < this.myPatients.Count; i++)
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                //类型为叶
                if (this.myPatients[i] is FS.HISFC.Models.Fee.Inpatient.Balance)
                {
                    try
                    {
                        FS.HISFC.Models.Fee.Inpatient.Balance b=this.myPatients[i] as FS.HISFC.Models.Fee.Inpatient.Balance;
                        if (this.Nodes[Branch].Nodes.ContainsKey(b.Patient.ID))
                        {
                            ArrayList al = this.Nodes[Branch].Nodes[b.Patient.ID].Tag as ArrayList;
                            al.Add(b);
                        }
                        else
                        {
                            ArrayList al = new ArrayList();
                            FS.HISFC.Models.RADT.PatientInfo patient = radtMananger.GetPatientInfo(b.Patient.ID);
                            obj =patient;
                            obj.Name = obj.Name;
                            obj.Memo = patient.InTimes.ToString();
                            al.Add(b);
                            this.AddTreeNode(Branch, obj, b.Patient.ID, al);
                        }
                    }
                    catch { }
                }
                else
                {
                    System.Windows.Forms.TreeNode newNode = new System.Windows.Forms.TreeNode();
                    //为干
                    //分割字符串 text|tag 标识结点
                    string all = this.myPatients[i].ToString();

                    newNode.Text = all;
                    newNode.ToolTipText = all;
                    try
                    {
                        newNode.ImageIndex = this.BranchImageIndex;
                        newNode.SelectedImageIndex = this.BranchSelectedImageIndex;
                    }
                    catch { }

                    Branch = this.Nodes.Add(newNode);
                }
            }
            if (this.bIsShowCount)
            {
                foreach (System.Windows.Forms.TreeNode node in this.Nodes)
                {
                    int count = 0;
                    count = node.GetNodeCount(false);
                    node.Text = node.ToolTipText + "(" + count.ToString() + ")";
                }
            }
            this.ExpandAll();
            try//wolf added ensure node visible 
            {
                TreeNode temp = this.SelectedNode;
                this.SelectedNode = null;
                if (temp == null && this.Nodes.Count > 0 && this.Nodes[0].Nodes.Count > 0)
                {
                    this.SelectedNode = this.Nodes[0].FirstNode;
                }
                else
                {
                    this.SelectedNode = temp;
                }
                this.SelectedNode.EnsureVisible();
            }
            catch { }

            if (this.Nodes.Count == 0) this.AddRootNode();
        }

    }
}
