﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.Local.OutpatientFee.GuangZhou.Gyzl.PreCharge
{
    public partial class tvPatientTree : FS.HISFC.Components.Common.Controls.tvPatientList
    {
        public tvPatientTree()
        {
            InitializeComponent();
        }

        private FS.HISFC.BizLogic.Registration.Register registerMgr = new FS.HISFC.BizLogic.Registration.Register();

        public override void Refresh()
        {
            //终端划价患者
            ArrayList al = new ArrayList();
            al.Add("当前科室患者信息列表");
            DateTime dtNow = registerMgr.GetDateTimeFromSysDateTime();
            string deptCode = ((FS.HISFC.Models.Base.Employee)registerMgr.Operator).Dept.ID;
            ArrayList detail = this.queryPatientList(dtNow.Date, dtNow, deptCode);
            if (detail == null)
            {
                CommonController.CreateInstance().MessageBox("查询终端划价患者出错!" + this.registerMgr.Err, MessageBoxIcon.Error);
                return;
            }
            al.AddRange(detail);

            this.SetPatient(al);
        }

        protected override void RefreshList()
        {
            int Branch = 0;
            this.Nodes.Clear();
            for (int i = 0; i < this.myPatients.Count; i++)
            {
                FS.FrameWork.Models.NeuObject obj = null;
                //类型为叶
                if (this.myPatients[i] is FS.FrameWork.Models.NeuObject)
                {
                    obj = this.myPatients[i] as FS.FrameWork.Models.NeuObject;
                    this.AddTreeNode(Branch, obj, obj.Name,obj);
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
                if (temp != null)
                {
                    //this.SelectedNode = this.Nodes[0].FirstNode;
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

        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            if (e.Node.Tag is FS.HISFC.Models.Registration.Register)
            {
                base.OnAfterSelect(e);
            }
            else if (e.Node.Tag is FS.FrameWork.Models.NeuObject)
            {
                //查找患者信息
                string id = ((FS.FrameWork.Models.NeuObject)e.Node.Tag).ID;

                FS.HISFC.Models.Registration.Register register = registerMgr.GetByClinic(id);
                if (register != null)
                {
                    e.Node.Tag = register;
                }

                base.OnAfterSelect(e);
            }

        }

        private ArrayList queryPatientList(DateTime beginTime, DateTime endTime, string deptCode)
        {
            string sql = @"select distinct r.clinic_code 门诊流水号,
                                  r.name 患者姓名,
                                  decode(r.sex_code,'M','男','女') 性别
                             from fin_opr_register r, met_ord_recipedetail m
                            where m.clinic_code = r.clinic_code
                              and m.oper_date >=to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                              and m.oper_date <=to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                              and m.exec_dpcd = '{2}'
                                        ";

            if (this.registerMgr.ExecQuery(string.Format(sql, beginTime, endTime, deptCode)) < 0)
            {
                return null;
            }

            ArrayList patientList = new ArrayList();//结算信息实体数组

            try
            {
                //循环读取数据
                while (this.registerMgr.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = this.registerMgr.Reader[0].ToString();
                    obj.Name = this.registerMgr.Reader[1].ToString();
                    obj.Memo = this.registerMgr.Reader[2].ToString();
                    patientList.Add(obj);
                }
            }
            finally
            {
                if (this.registerMgr.Reader != null && this.registerMgr.Reader.IsClosed == false)
                {
                    this.registerMgr.Reader.Close();
                }
            }

            return patientList;
        }

    }
}
