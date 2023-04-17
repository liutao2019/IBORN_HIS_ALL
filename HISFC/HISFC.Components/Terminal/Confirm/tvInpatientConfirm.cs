﻿using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.HISFC.Components.Terminal.Confirm
{
    public partial class tvInpatientConfirm : FS.HISFC.Components.Common.Controls.tvPatientList
    {
        public tvInpatientConfirm()
        {
            InitializeComponent(); 
        }

        public tvInpatientConfirm(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #region 变量

        private FS.FrameWork.Public.ObjectHelper deptHelper = null;

        private DateTime beginTime = DateTime.Now;
        private DateTime endTime = DateTime.Now;
        private Hashtable hsDept = new Hashtable();
        private Hashtable hsPatient = new Hashtable();
        private Hashtable hsTempDeptNodes = new Hashtable();
        private Hashtable hsTempPatientNodes = new Hashtable();
        private Hashtable hsPatientBranch = new Hashtable();

        /// <summary>
        /// 当前树中的科室字典
        /// </summary>
        private ArrayList allCurDept = new ArrayList();
        public ArrayList AllCurDept
        {
            get { return allCurDept; }
            set { allCurDept = value; }
        }

        /// <summary>
        /// 是否展开树节点
        /// </summary>
        private bool isExpand = true;
        public bool IsExpand
        {
            get { return isExpand; }
            set {isExpand = value;}
        }
        
        /// <summary>
        /// 操作员

        /// </summary>
        private FS.HISFC.Models.Base.Employee oper = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

        #endregion

        #region 方法

        //public void Init()
        //{
        //    FS.HISFC.BizProcess.Integrate.Manager deptManager = new FS.HISFC.BizProcess.Integrate.Manager();

        //    ArrayList alDept = deptManager.GetDeptmentAllValid();
        //    if (alDept == null)
        //    {
        //        return;
        //    }

        //    this.deptHelper = new FS.FrameWork.Public.ObjectHelper(alDept);

        //    //this.ImageList = this.deptImageList;
        //    //this.RefreshTree();
        //    this.Refresh();
        //}

        public void Init(DateTime beginTime, DateTime endTime)
        {
            this.beginTime = beginTime;
            this.endTime = endTime;

            if (this.deptHelper == null)
            {
                FS.HISFC.BizProcess.Integrate.Manager deptManager = new FS.HISFC.BizProcess.Integrate.Manager();

                ArrayList alDept = deptManager.GetDeptmentAllValid();
                if (alDept == null)
                {
                    return;
                }

                this.deptHelper = new FS.FrameWork.Public.ObjectHelper(alDept);
            }

            this.Refresh();
        }

        string operDept = "";
        public string OperDept
        {
            get
            {
                if (string.IsNullOrEmpty(operDept))
                {
                    return oper.Dept.ID;
                }
                return operDept;
            }
            set
            {
                operDept = value;
            }
        }
        ///// <summary>
        ///// 列表显示
        ///// </summary>
        ///// <returns></returns>
        //public int RefreshTree()
        //{
        //    FS.HISFC.BizProcess.Integrate.Order orderManager = new FS.HISFC.BizProcess.Integrate.Order();
        //    FS.HISFC.BizProcess.Integrate.Manager deptManager = new FS.HISFC.BizProcess.Integrate.Manager();
        //    FS.HISFC.BizProcess.Integrate.RADT patientManager = new FS.HISFC.BizProcess.Integrate.RADT();
        //    if (string.IsNullOrEmpty(operDept))
        //    {
        //        operDept = oper.Dept.ID;
        //    }
        //    ArrayList alDept = orderManager.QueryPatientDeptByConfirmDeptID(operDept);

        //    this.Nodes.Clear();

        //    System.Windows.Forms.TreeNode deptNode = new System.Windows.Forms.TreeNode();

        //    foreach (FS.FrameWork.Models.NeuObject dept in alDept)
        //    {
        //        deptNode = new System.Windows.Forms.TreeNode();

        //        deptNode.Text = this.deptHelper.GetName(dept.ID);
        //        deptNode.ImageIndex = 0;
        //        deptNode.SelectedImageIndex = 1;
        //        deptNode.Tag = deptManager.GetDepartment(dept.ID);

        //        this.Nodes.Add(deptNode);

        //        ArrayList alPatient = orderManager.QueryPatientByConfirmDeptAndPatDept(operDept, dept.ID);
        //        foreach (FS.FrameWork.Models.NeuObject patient in alPatient)
        //        {
        //            System.Windows.Forms.TreeNode patientNode = new System.Windows.Forms.TreeNode();
        //            FS.HISFC.Models.RADT.PatientInfo patientInfo = patientManager.QueryPatientInfoByInpatientNO(patient.ID);
        //            patientNode.Text = patientInfo.Name;
        //            patientNode.ImageIndex = 6;
        //            patientNode.SelectedImageIndex = 7;
        //            patientNode.Tag = patientInfo;

        //            deptNode.Nodes.Add(patientNode);
        //        }

        //    }

        //    return 1;
        //}

        ///// <summary>
        ///// 根据病区站得到欠费患者
        ///// </summary>
        ///// <param name="al"></param>
        //private void addPatientList(ArrayList al)
        //{
        //    FS.HISFC.BizProcess.Integrate.Order orderManager = new FS.HISFC.BizProcess.Integrate.Order(); 
        //    FS.HISFC.BizProcess.Integrate.RADT radtManager = new FS.HISFC.BizProcess.Integrate.RADT();
        //    if (string.IsNullOrEmpty(operDept))
        //    {
        //        operDept = oper.Dept.ID;
        //    }
        //    ArrayList alDept = orderManager.QueryPatientDeptByConfirmDeptID(operDept);

        //    ArrayList al1 = new ArrayList();
        //    foreach (FS.FrameWork.Models.NeuObject objDept in alDept)
        //    {
        //        string deptName = this.deptHelper.GetName(objDept.ID);
        //        string deptCode = objDept.ID;
        //        al1.Clear();
        //        al1 = orderManager.QueryPatientByConfirmDeptAndPatDept(operDept, deptCode);
        //        if (al1 != null && al1.Count > 0)
        //        {
        //            al.Add(deptName);
        //            foreach (FS.FrameWork.Models.NeuObject patient in al1)
        //            {
        //                FS.HISFC.Models.RADT.PatientInfo patientInfo = radtManager.QueryPatientInfoByInpatientNO(patient.ID);
        //                al.Add(patientInfo);
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// 根据病区站得到欠费患者
        /// </summary>
        /// <param name="al"></param>
        private void addPatientList(ArrayList al,DateTime beginTime,DateTime endTime)
        {
            hsTempDeptNodes.Clear();
            hsTempPatientNodes.Clear();
            hsPatientBranch.Clear();

            FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
            FS.HISFC.BizProcess.Integrate.RADT radtManager = new FS.HISFC.BizProcess.Integrate.RADT();
            if (string.IsNullOrEmpty(operDept))
            {
                operDept = oper.Dept.ID;
            }
            ArrayList alDept = orderManager.QueryPatientDeptByConfirmDeptID(operDept, beginTime, endTime);

            this.AllCurDept = alDept.Clone() as ArrayList;

            ArrayList al1 = new ArrayList();
            foreach (FS.FrameWork.Models.NeuObject objDept in alDept)
            {
                string deptName = this.deptHelper.GetName(objDept.ID);
                string deptCode = objDept.ID;
                al1.Clear();
                al1 = orderManager.QueryPatientByConfirmDeptAndPatDept(operDept, deptCode, beginTime, endTime);
                if (al1 != null && al1.Count > 0)
                {
                    //如果存在科室的患者
                    if (!hsDept.ContainsKey(deptCode))
                    {
                        hsDept.Add(deptCode, deptName);
                        al.Add(deptName + "|" + deptCode);
                    }

                    if (!hsTempDeptNodes.ContainsKey(deptCode))
                    {
                        hsTempDeptNodes.Add(deptCode, deptName);
                    }

                    foreach (FS.FrameWork.Models.NeuObject patient in al1)
                    {
                        if (!hsPatient.ContainsKey(patient.ID))
                        {
                            FS.HISFC.Models.RADT.PatientInfo patientInfo = radtManager.QueryPatientInfoByInpatientNO(patient.ID);
                            al.Add(patientInfo);
                            hsPatient.Add(patientInfo.ID, patientInfo);
                        }

                        if (!hsTempPatientNodes.ContainsKey(patient.ID))
                        {
                            hsTempPatientNodes.Add(patient.ID, patient);
                        }
                    }
                }
            }
        }

        public override void Refresh()
        {
            //this.BeginUpdate();
            if (hsPatient == null || hsPatient.Count == 0)
            {
                this.Nodes.Clear();
            }

            hsPatientBranch.Clear();

            ArrayList al = new ArrayList();//患者列表
            addPatientList(al, beginTime, endTime);

            foreach (TreeNode node in this.Nodes)
            {
                
                foreach (TreeNode childNode in node.Nodes)
                {
                    if (!hsTempPatientNodes.ContainsKey(childNode.Name))
                    {
                        hsPatient.Remove(childNode.Name);
                        childNode.Remove();
                    }
                    if (!hsPatientBranch.Contains((childNode.Tag as FS.HISFC.Models.RADT.PatientInfo).ID))
                    {
                        hsPatientBranch.Add((childNode.Tag as FS.HISFC.Models.RADT.PatientInfo).ID, node.Index);
                    }
                }

                if (!hsTempDeptNodes.ContainsKey(node.Name))
                {
                    hsDept.Remove(node.Name);
                    node.Remove();
                    continue;
                }
            }

            //显示所有患者列表
            this.SetPatient(al);
            //this.EndUpdate();
        }

        public TreeNode FindDefaultNode(FS.HISFC.Models.RADT.PatientInfo PatientInfo)
        {
            foreach (TreeNode node in this.Nodes)
            {
                node.Collapse();
            }
            this.SelectedNode = null;
            int Branch = FS.FrameWork.Function.NConvert.ToInt32(hsPatientBranch[PatientInfo.ID]);
            TreeNode nodePatient = this.FindNode(Branch, PatientInfo);
            return nodePatient;
        }

        public int FindDefaultDept(string deptInfo)
        {
            foreach (TreeNode node in this.Nodes)
            {
                node.Collapse();
            }
            this.SelectedNode = Nodes[deptInfo];
            Nodes[deptInfo].ExpandAll();
            return 1;
        }


        /// <summary>
        /// 刷新列表
        /// </summary>
        protected override void RefreshList()
        {
            int Branch = 0;
            if (hsPatient == null || hsPatient.Count == 0)
            {
                this.Nodes.Clear();
            }
            hsPatientBranch.Clear();
            for (int i = 0; i < this.myPatients.Count; i++)
            {
                System.Windows.Forms.TreeNode newNode = new System.Windows.Forms.TreeNode();
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                //类型为叶
                if (this.myPatients[i].GetType().ToString() == "FS.HISFC.Models.RADT.PatientInfo")
                {
                    try
                    {
                        FS.HISFC.Models.RADT.PatientInfo PatientInfo = (FS.HISFC.Models.RADT.PatientInfo)this.myPatients[i];
                        obj.ID = PatientInfo.PID.PatientNO;
                        obj.Name = PatientInfo.Name;
                        try
                        {
                            obj.Memo = PatientInfo.PVisit.PatientLocation.Bed.ID;
                            try
                            {	//请假
                                if (PatientInfo.PVisit.PatientLocation.Bed.Status.ID.ToString() == "R")
                                {
                                    obj.Name = obj.Name + "【请假】";
                                }
                            }
                            catch { }
                        }
                        catch
                        {//无病床信息
                        }
                        obj.User01 = PatientInfo.PVisit.PatientLocation.Dept.Name;
                        obj.User02 = PatientInfo.PVisit.InState.Name;
                        obj.User03 = PatientInfo.Sex.ID.ToString();
                        //入院不超过24小时,显示(新)
                        if (this.IsShowNewPatient)
                        {
                            if (dtToday < PatientInfo.PVisit.InTime.Date.AddDays(1)) obj.Name = obj.Name + "(新)";
                        }
                        if (this.Nodes.ContainsKey(PatientInfo.PVisit.PatientLocation.Dept.ID))
                        {
                            Branch = this.Nodes[PatientInfo.PVisit.PatientLocation.Dept.ID].Index;
                        }

                        this.AddTreeNode(Branch, obj, PatientInfo.ID, PatientInfo);
                        if (!hsPatientBranch.Contains(PatientInfo.ID))
                        {
                            hsPatientBranch.Add(PatientInfo.ID, Branch);
                        }
                    }
                    catch { }
                }
                else
                {//为干
                    //分割字符串 text|tag 标识结点
                    string all = this.myPatients[i].ToString();
                    string[] s = all.Split('|');

                    newNode.Text = s[0];
                    newNode.ToolTipText = s[0];
                    try
                    {
                        newNode.Name = s[1];
                        newNode.Tag = s[1];
                    }
                    catch { newNode.Tag = ""; }
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
            if (this.IsExpand)
            {
                this.ExpandAll();
            }
            try//wolf added ensure node visible 
            {
                TreeNode temp = this.SelectedNode;
                this.SelectedNode = null;
                if (temp == null && this.Nodes.Count > 0 && this.Nodes[0].Nodes.Count > 0)
                {
                    //this.SelectedNode = this.Nodes[0].FirstNode;
                    this.SelectedNode = temp;
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

        #endregion

    }
}
