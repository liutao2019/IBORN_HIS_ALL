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

namespace FS.SOC.Local.OutpatientFee.GuangZhou.Gyzl.Charge
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
            al.Add("挂号患者信息");
            //DateTime dtNow = registerMgr.GetDateTimeFromSysDateTime();
            // {DD27333B-4CBF-4bb2-845D-8D28D616937E}
            DateTime dtBenTime = DateTime.Parse(registerMgr.GetDateTimeFromSysDateTime().ToShortDateString() + " 00:00:00");

            DateTime dtEndTime = DateTime.Parse(registerMgr.GetDateTimeFromSysDateTime().ToShortDateString() + " 23:59:59");
            //string deptCode = ((FS.HISFC.Models.Base.Employee)registerMgr.Operator).Dept.ID;
            FS.HISFC.Models.Base.Department currDept = new FS.HISFC.Models.Base.Department();
            currDept = (FS.HISFC.Models.Base.Department)(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept);
            
            ArrayList detail = this.queryPatientList(dtBenTime, dtEndTime, currDept.ID);

            if (detail == null)
            {
                CommonController.CreateInstance().MessageBox("查询终端划价患者出错!" + this.registerMgr.Err, MessageBoxIcon.Error);
                return;
            }
            ArrayList patientList = new ArrayList();
            foreach (FS.FrameWork.Models.NeuObject obj in detail)
            {
                //{09B08BE4-9AC8-4094-9D25-0CA5B8C615AB}
                //去除列表上显示院区的代码
                if (obj.User01 == currDept.HospitalID)
                {
                    if (QueryCountToPay(obj.ID) > 0)
                    {
                        patientList.Add(obj);
                    }
                }

            }
            al.AddRange(patientList);

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
                register.Mark1 = "1";//3EF17191-E618-42A9-A86E-6C63DE7AEE3C根据挂号信息进来收费的标注为1
                base.OnAfterSelect(e);
            }

        }

        private int QueryCountToPay(string clincCode)// {F53BD032-1D92-4447-8E20-6C38033AA607}
        {
            int count = 0;
            string str = "";
            string sql = @"select count(*) from fin_opb_feedetail
                            where CLINIC_CODE = '{0}'
                              and pay_flag = '0'";
            sql = string.Format(sql, clincCode);
            str = this.registerMgr.ExecSqlReturnOne(sql, "");
            if (string.IsNullOrEmpty(str))
            {
                count = 0;
            }
            else
            {
                count = int.Parse(str);
            }
            return count;
        }

        private ArrayList queryPatientList(DateTime beginTime, DateTime endTime, string deptCode)
        {
            // {49E44612-BEDA-4e3e-8F4E-254D4A7525EA}
            string sql = @"select t.clinic_code,
                                       t.name,
                                       decode(t.sex_code, 'M', '男', '女') sex,
                                       (select a.hospital_id
                                         from com_department a where a.dept_code = t.dept_code) hospital_id
                                         from fin_opr_register t
                                        where  t.reg_date >=to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                                        and t.reg_date <=to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                        and t.valid_flag='1'
                                        order by t.name
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
                    obj.User01 = this.registerMgr.Reader[3].ToString();
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
