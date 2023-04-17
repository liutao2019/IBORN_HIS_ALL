using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Order.Controls
{
    public partial class ucMedicalTeam : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucMedicalTeam()
        {
            InitializeComponent();
        }
        #region 域

        /// <summary>
        /// 医疗组业务层
        /// </summary>
        FS.HISFC.BizLogic.Order.MedicalTeam medicalTeamLogic = new FS.HISFC.BizLogic.Order.MedicalTeam();

        /// <summary>
        /// 医疗组对应医生业务层
        /// </summary>
        FS.HISFC.BizLogic.Order.MedicalTeamForDoct medicalTeamForDoctLogic = new FS.HISFC.BizLogic.Order.MedicalTeamForDoct();
        #endregion
        #region 属性

        #endregion

        #region 方法
        /// <summary>
        /// 初始化树
        /// </summary>
        /// <returns></returns>
        private int InitTreeView()
        {
            this.tvDept.Nodes.Clear();

            ArrayList alDeptListInhos = CacheManager.InterMgr.GetDeptmentIn(FS.HISFC.Models.Base.EnumDepartmentType.I);
            //{C7C34C7A-B0D6-4643-992D-33A1793047D0}feng.ch
            //门诊科室
            ArrayList alDeptListOuthos = CacheManager.InterMgr.GetDeptmentIn(FS.HISFC.Models.Base.EnumDepartmentType.C);
            //终端科室
            ArrayList alDeptListTemhos = CacheManager.InterMgr.GetDeptmentIn(FS.HISFC.Models.Base.EnumDepartmentType.T);
            if (alDeptListInhos == null)
            {
                MessageBox.Show("查询住院科室出错");
                return 1;
            }
            //{C7C34C7A-B0D6-4643-992D-33A1793047D0}
            if (alDeptListOuthos == null)
            {
                MessageBox.Show("查询门诊科室出错");
                return 1;
            }
            //{C7C34C7A-B0D6-4643-992D-33A1793047D0}
            if (alDeptListTemhos == null)
            {
                MessageBox.Show("查询终端科室出错");
                return 1;
            }

            this.tvDept.ImageList = this.tvDept.deptImageList;

            TreeNode rootNode = new TreeNode("住院科室", 1, 1);
            //{C7C34C7A-B0D6-4643-992D-33A1793047D0}
            rootNode.Tag = "AAAA";
            TreeNode rootNode1 = new TreeNode("门诊科室", 1, 1);
            rootNode1.Tag = "AAAA";
            TreeNode rootNode2 = new TreeNode("终端科室", 1, 1);
            rootNode2.Tag = "AAAA";
            for (int i = 0; i < alDeptListInhos.Count; i++)
            {
                FS.FrameWork.Models.NeuObject deptObj = alDeptListInhos[i] as FS.FrameWork.Models.NeuObject;

                TreeNode node = new TreeNode();
                node.Tag = deptObj;
                node.Text = deptObj.Name;
                node.SelectedImageIndex = 0;
                node.ImageIndex = 0;

                rootNode.Nodes.Add(node);


            }
            //{C7C34C7A-B0D6-4643-992D-33A1793047D0}
            for (int i = 0; i < alDeptListOuthos.Count; i++)
            {
                FS.FrameWork.Models.NeuObject deptObj = alDeptListOuthos[i] as FS.FrameWork.Models.NeuObject;

                TreeNode node = new TreeNode();
                node.Tag = deptObj;
                node.Text = deptObj.Name;
                node.SelectedImageIndex = 0;
                node.ImageIndex = 0;

                rootNode1.Nodes.Add(node);


            }
            for (int i = 0; i < alDeptListTemhos.Count; i++)
            {
                FS.FrameWork.Models.NeuObject deptObj = alDeptListTemhos[i] as FS.FrameWork.Models.NeuObject;

                TreeNode node = new TreeNode();
                node.Tag = deptObj;
                node.Text = deptObj.Name;
                node.SelectedImageIndex = 0;
                node.ImageIndex = 0;

                rootNode2.Nodes.Add(node);


            }
            //rootNode.ExpandAll();
            //{C7C34C7A-B0D6-4643-992D-33A1793047D0}
            //rootNode1.ExpandAll();
            //rootNode2.ExpandAll();
            this.tvDept.Nodes.Add(rootNode);
            //{C7C34C7A-B0D6-4643-992D-33A1793047D0}
            this.tvDept.Nodes.Add(rootNode1);
            this.tvDept.Nodes.Add(rootNode2);

            return 1;
        }

        /// <summary>
        /// 根据科室查询医疗组信息
        /// </summary>
        /// <returns></returns>
        private int QueryMedicalTeamByDept(string deptCode)
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;
            List<FS.HISFC.Models.Order.Inpatient.MedicalTeam> medicalTeamList = this.medicalTeamLogic.QueryMedicalTeamByDept(deptCode);

            if (medicalTeamLogic == null)
            {
                MessageBox.Show("查询科室的医疗组失败!\n" + this.medicalTeamLogic.Err);
                return -1;
            }

            this.SetFarpointValue(medicalTeamList);


            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="medicalTeamList"></param>
        private void SetFarpointValue(List<FS.HISFC.Models.Order.Inpatient.MedicalTeam> medicalTeamList)
        {

            for (int i = 0; i < medicalTeamList.Count; i++)
            {
                FS.HISFC.Models.Order.Inpatient.MedicalTeam obj = medicalTeamList[i];

                int count = this.neuSpread1_Sheet1.Rows.Count;
                this.neuSpread1_Sheet1.Rows.Add(count, 1);

                this.neuSpread1_Sheet1.Cells[count, 1].Text = obj.ID;
                this.neuSpread1_Sheet1.Cells[count, 2].Text = obj.Name;
                this.neuSpread1_Sheet1.Cells[count, 3].Value = obj.IsValid;
                this.neuSpread1_Sheet1.Cells[count, 4].Text = obj.Dept.Name;
                this.neuSpread1_Sheet1.Rows[count].Tag = obj;

            }

            
        }

        /// <summary>
        /// 根据科室和医疗组信息查询医生信息
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="medicalTeamCode"></param>
        /// <returns></returns>
        private int QueryDoctByDeptAndMedicalTeam(string deptCode, string medicalTeamCode)
        {
            this.neuSpread2_Sheet1.Rows.Count = 0;
            List<FS.HISFC.Models.Order.Inpatient.MedicalTeamForDoct> medicalTeamForDoctList = this.medicalTeamForDoctLogic.QueryQueryMedicalTeamForDoctInfo(deptCode, medicalTeamCode,"All");
            if (medicalTeamForDoctList == null)
            {
                MessageBox.Show("查询医生出错!\n" + this.medicalTeamForDoctLogic.Err);
                return -1;
            }

            for (int i = 0; i < medicalTeamForDoctList.Count; i++)
            {
                FS.HISFC.Models.Order.Inpatient.MedicalTeamForDoct obj = medicalTeamForDoctList[i];
                this.neuSpread2_Sheet1.Rows.Add(0, 1);
                this.neuSpread2_Sheet1.Cells[0, 1].Text = obj.Doct.Name;
                this.neuSpread2_Sheet1.Cells[0, 2].Value = obj.IsValid;
                this.neuSpread2_Sheet1.Rows[0].Tag = obj;
            }



            return 1;
        }

        /// <summary>
        /// 添加医疗组
        /// </summary>
        private int AddMedicalTeam()
        {
            if (this.tvDept.SelectedNode.Level == 0) return -1;
            Forms.frmMedcialTeam frmMedcialTeam = new FS.HISFC.Components.Order.Forms.frmMedcialTeam();

            FS.HISFC.Models.Order.Inpatient.MedicalTeam medicalTeam = new FS.HISFC.Models.Order.Inpatient.MedicalTeam();

            medicalTeam.Dept = this.tvDept.SelectedNode.Tag as FS.FrameWork.Models.NeuObject;
            frmMedcialTeam.MedicalTeam = medicalTeam;

            frmMedcialTeam.ShowDialog();

            //重新刷新
            this.QueryMedicalTeamByDept(medicalTeam.Dept.ID);

            return 1;
        }


        /// <summary>
        /// 添加医疗组
        /// </summary>
        private int ModifyMedicalTeam()
        {
            if (this.tvDept.SelectedNode.Level == 0) return -1;
            Forms.frmMedcialTeam frmMedcialTeam = new FS.HISFC.Components.Order.Forms.frmMedcialTeam();

            FS.HISFC.Models.Order.Inpatient.MedicalTeam medicalTeam = this.neuSpread1_Sheet1.ActiveRow.Tag as FS.HISFC.Models.Order.Inpatient.MedicalTeam;

            medicalTeam.Dept = this.tvDept.SelectedNode.Tag as FS.FrameWork.Models.NeuObject;
            frmMedcialTeam.MedicalTeam = medicalTeam;

            frmMedcialTeam.ShowDialog();

            //重新刷新
            this.QueryMedicalTeamByDept(medicalTeam.Dept.ID);

            return 1;
        }

        /// <summary>
        /// 添加医生
        /// </summary>
        /// <returns></returns>
        private int AddDoct()
        {
            FS.HISFC.Models.Order.Inpatient.MedicalTeam obj = this.neuSpread1_Sheet1.ActiveRow.Tag as FS.HISFC.Models.Order.Inpatient.MedicalTeam;
            if (obj == null)
            {
                MessageBox.Show("请选择医疗组信息");
                return -1;
            }

            Forms.frmAddDoct frmAddDoct = new FS.HISFC.Components.Order.Forms.frmAddDoct();
            
            if (obj == null) return -1;
            frmAddDoct.MedicalTeam = obj;
            frmAddDoct.ShowDialog();
            this.QueryDoctByDeptAndMedicalTeam(obj.Dept.ID, obj.ID);

            return 1;
        }

        /// <summary>
        /// 停用启用医疗组
        /// </summary>
        /// <returns></returns>
        private int ProcessMedicalTeamValidFlag(bool isValid )
        {
            int returnValue = 0;
            string errText = string.Empty;
            
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
               
                bool isSelectecd = FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, 0].Value);

                if (isSelectecd)
                {
                    FS.HISFC.Models.Order.Inpatient.MedicalTeam medcialTeamObj = this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.MedicalTeam;

                    //更新医疗组标记
                   returnValue =  this.medicalTeamLogic.UpdateValidFlag(FS.FrameWork.Function.NConvert.ToInt32( isValid).ToString(), medcialTeamObj.Dept.ID, medcialTeamObj.ID);
                   if (returnValue < 0)
                   {
                       FS.FrameWork.Management.PublicTrans.RollBack();
                       MessageBox.Show("更新医疗组状态失败!" + this.medicalTeamLogic.Err);
                       return -1;
                   }
                   if (isValid)
                   {
                       continue; //停用一起停，启用不一起启用
                   }
                    //更新医疗组内的医生
                    returnValue = ProcessDoctValidFlag(medcialTeamObj.Dept.ID, medcialTeamObj.ID, false, ref errText);
                    if (returnValue < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(errText);
                        return -1;
                    }
                }
                
            }
            
            FS.FrameWork.Management.PublicTrans.Commit();

            if (isValid)
            {

                MessageBox.Show("启用成功！");
            }
            else
            {
                MessageBox.Show("停用成功！");
            }
            this.QueryMedicalTeamByDept((this.tvDept.SelectedNode.Tag as FS.FrameWork.Models.NeuObject).ID);

            return 1;
        }

        private int ProcessDoctValidFlag(string deptCode, string medicalTeamCode, bool isValid,ref string errText)
        {
            int returnValue = this.medicalTeamForDoctLogic.UpdateValidFlag(FS.FrameWork.Function.NConvert.ToInt32(isValid).ToString(), deptCode, medicalTeamCode);
            if (returnValue < 0)
            {
                errText = "更细医生状态出错!\n" + this.medicalTeamForDoctLogic.Err ;
                return -1;
            }


            return 1;
        }

        /// <summary>
        /// 停用启用医生
        /// </summary>
        /// <param name="isValidFlag"></param>
        /// <returns></returns>
        private int ProcessDoctValidFlag(bool isValidFlag)
        {
            FS.HISFC.Models.Order.Inpatient.MedicalTeam obj = this.neuSpread1_Sheet1.ActiveRow.Tag as FS.HISFC.Models.Order.Inpatient.MedicalTeam;
            int returnValue = 0;
            string errText = string.Empty;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            int j = 0;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                bool isSelectecd = FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread2_Sheet1.Cells[i, 0].Value);

                if (isSelectecd)
                {
                    j++;
                    FS.HISFC.Models.Order.Inpatient.MedicalTeamForDoct medcialTeamForDoctObj = this.neuSpread2_Sheet1.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.MedicalTeamForDoct;

                    returnValue = this.medicalTeamForDoctLogic.UpdateValidFlag(medcialTeamForDoctObj.MedcicalTeam.Dept.ID, medcialTeamForDoctObj.MedcicalTeam.ID, medcialTeamForDoctObj.Doct.ID);
                    if (returnValue < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新医生状态出错!\n" + this.medicalTeamForDoctLogic.Err);
                        return -1;
                    }
                }

            }
            if (j == 0) //没有选择
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("请选择要更新的医生");
                return -1;
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.Commit();

                if (isValidFlag)
                {
                    MessageBox.Show("启用成功");
                }
                else
                {
                    MessageBox.Show("停用成功");
                }

            }
            this.QueryDoctByDeptAndMedicalTeam(obj.Dept.ID,obj.ID);
            return 1;
        }
        /// <summary>
        /// 删除科室下所有医疗组信息
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        private int DeleteMedicalTeamAll()
        {
            if (this.tvDept.SelectedNode.Level == 0)
            {
                return -1;
            }

            FS.FrameWork.Models.NeuObject deptObj = this.tvDept.SelectedNode.Tag as FS.FrameWork.Models.NeuObject;
            string deptCode = deptObj.ID;
            int returnValue = 1;
            string errText = string.Empty;
            int j = 0;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                bool isSelectecd = FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, 0].Value);
                
                if (isSelectecd)
                {


                    j++;
                    FS.HISFC.Models.Order.Inpatient.MedicalTeam medcialTeamObj = this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.MedicalTeam;

                    List<FS.HISFC.Models.Order.Inpatient.MedicalTeamForDoct> doctList = this.medicalTeamForDoctLogic.QueryQueryMedicalTeamForDoctInfo(medcialTeamObj.Dept.ID, medcialTeamObj.ID, "All");

                    if (doctList == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("查询护理组下医生失败！\n" + this.medicalTeamForDoctLogic.Err);
                        return -1;

                    }

                    if (doctList.Count > 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("护理组:" + medcialTeamObj.Name +"有医生信息，不能删除！\n请先删除医生信息！");
                        return -1;
                        
                    }

                    returnValue = this.medicalTeamLogic.DeleteMedicalTeam(deptCode, medcialTeamObj.ID);
                    if (returnValue < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(errText);
                        return -1;
                    }
                }
                 
            }

            if (j == 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("请选择要删除的医疗组信息");
                return -1;
            }
            else
            {

                FS.FrameWork.Management.PublicTrans.Commit();

                MessageBox.Show("删除成功");
            }
            this.QueryMedicalTeamByDept(deptCode);
            this.neuSpread2_Sheet1.Rows.Count = 0;
            return 1;
        }

        private int DeleteMedicalTeamForDoctAll()
        {
            FS.HISFC.Models.Order.Inpatient.MedicalTeam obj = this.neuSpread1_Sheet1.ActiveRow.Tag as FS.HISFC.Models.Order.Inpatient.MedicalTeam;
            int returnValue = 0;
            string errText = string.Empty;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            int j = 0;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                bool isSelectecd = FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread2_Sheet1.Cells[i, 0].Value);
                
                if (isSelectecd)
                {
                    j++;
                    FS.HISFC.Models.Order.Inpatient.MedicalTeamForDoct medcialTeamForDoctObj = this.neuSpread2_Sheet1.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.MedicalTeamForDoct;

                    returnValue = this.medicalTeamForDoctLogic.DeleteMedicalTeamMedicalTeamForDoct(medcialTeamForDoctObj.MedcicalTeam.Dept.ID, medcialTeamForDoctObj.MedcicalTeam.ID, medcialTeamForDoctObj.Doct.ID);
                    if (returnValue < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("删除医生出错!\n" + this.medicalTeamForDoctLogic.Err);
                        return -1;
                    }
                }

            }
            if (j == 0) //没有选择
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("请选择要删除的医疗组信息");
                return -1;
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.Commit();

                MessageBox.Show("删除成功");
               
            }
            this.QueryDoctByDeptAndMedicalTeam(obj.Dept.ID,obj.ID);
            return 1;

        }


        #endregion

        #region 重载方法
        protected override void OnLoad(EventArgs e)
        {
            this.InitTreeView();
            base.OnLoad(e);
        }

        FS.FrameWork.WinForms.Forms.ToolBarService toolBarServic = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarServic.AddToolButton("增加医疗组", "增加医疗组", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);
            toolBarServic.AddToolButton("修改医疗组", "增加医疗组", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            toolBarServic.AddToolButton("删除医疗组", "删除医疗组", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
           // toolBarServic.AddToolButton("全删医疗组", "全删医疗组", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            toolBarServic.AddToolButton("增加医生", "增加医生", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);
            toolBarServic.AddToolButton("删除医生", "删除医生", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            toolBarServic.AddToolButton("停用医疗组", "停用医疗组", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            toolBarServic.AddToolButton("启用医疗组", "启用医疗组", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            toolBarServic.AddToolButton("停用医生", "停用医生", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            toolBarServic.AddToolButton("启用停用", "启用停用", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);


            return this.toolBarServic;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "增加医疗组":
                    {
                        this.AddMedicalTeam();
                        break;
                    }
                case "删除医疗组":
                    {
                        this.DeleteMedicalTeamAll();
                        break;
                    }
                case "修改医疗组":
                    {
                        this.ModifyMedicalTeam();
                        break;
                    }
                case "增加医生":
                    {
                        this.AddDoct();
                        break;
                    }
                case "删除医生":
                    {
                        this.DeleteMedicalTeamForDoctAll();
                        break;
                    }
                case "停用医疗组":
                    {
                        this.ProcessMedicalTeamValidFlag(false);
                        break;
                    }
                case "启用医疗组":
                    {
                        this.ProcessMedicalTeamValidFlag(true);
                        break;
                    }
                case "停用医生":
                    {
                        this.ProcessDoctValidFlag(false);
                        break;
                    }
                case "启用停用":
                    {
                        this.ProcessDoctValidFlag(true);
                        break;
                    }

                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }
        #endregion

        private void tvDept_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread2_Sheet1.RowCount = 0;
            if (e.Node.Level == 0)
            {
                this.tabPage1.Text = "住院科室";
                
                return;
            }
            else
            {
                FS.FrameWork.Models.NeuObject obj = e.Node.Tag as FS.FrameWork.Models.NeuObject;
                this.QueryMedicalTeamByDept(obj.ID);
                this.tabPage1.Text = "科室名称:" + obj.Name;
            }
        }

        private void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader) return;
           

            FS.HISFC.Models.Order.Inpatient.MedicalTeam obj = this.neuSpread1_Sheet1.Rows[e.Row].Tag as FS.HISFC.Models.Order.Inpatient.MedicalTeam;

            this.tabPage2.Text = obj.Name;

            if (obj == null) return;

            this.QueryDoctByDeptAndMedicalTeam(obj.Dept.ID, obj.ID);


        }
    }
}
