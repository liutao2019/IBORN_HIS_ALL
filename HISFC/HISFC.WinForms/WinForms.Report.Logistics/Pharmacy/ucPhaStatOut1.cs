using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;
namespace FS.Report.Logistics.Pharmacy
{
    public partial class ucPhaStatOut1 : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucPhaStatOut1()
        {
            InitializeComponent();
            base.LeftControl = QueryControls.Tree;
        }

        private FS.FrameWork.Public.ObjectHelper itemTypeHelper = new FS.FrameWork.Public.ObjectHelper();

        private FS.FrameWork.Public.ObjectHelper qualityHelper = new FS.FrameWork.Public.ObjectHelper();

        private FS.FrameWork.Public.ObjectHelper privTypeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 库存性质名称集合
        /// </summary>
        private string[] qualityNameCollection = null;

        private string[] privTypeNameCollection = null;

        private string drugType = string.Empty;

        private string drugQuality = string.Empty;

        private string privType = string.Empty;

        private string orugtypeName=string.Empty;

        private string orugqualityName=string.Empty;

        private string outtypeName = string.Empty;

        protected override void OnLoad()
        {
            
            base.OnLoad();
            //设置时间范围
            DateTime now = DateTime.Now;
            DateTime dt = new DateTime(DateTime.Now.Year, 1, 1);
            this.dtpBeginTime.Value = dt;


            FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
            ArrayList alItemType = consManager.GetList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE);
            if (alItemType == null)
            {
                MessageBox.Show(Language.Msg("根据常数类别获取药品类型名称发生错误!") + consManager.Err);
                itemTypeHelper = new FS.FrameWork.Public.ObjectHelper();
                return;
            }

            FS.FrameWork.Models.NeuObject itemTypeObj = new FS.FrameWork.Models.NeuObject();
            itemTypeObj.ID = "ALL";
            itemTypeObj.Name = "全部";

            alItemType.Insert(0, itemTypeObj);

            itemTypeHelper = new FS.FrameWork.Public.ObjectHelper(alItemType);

            this.cmbType.AddItems(alItemType);
            ////////////////////////////////////////////
            ArrayList alQuality = consManager.GetList(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY);
            if (alQuality == null)
            {
                MessageBox.Show(Language.Msg("根据常数类别获取药品性质发生错误!") + consManager.Err);
                this.qualityHelper = new FS.FrameWork.Public.ObjectHelper();
                return;
            }

            this.qualityNameCollection = new string[alQuality.Count];
            int iIndex = 0;

            foreach (FS.FrameWork.Models.NeuObject qualityInfo in alQuality)
            {
                qualityNameCollection[iIndex] = qualityInfo.Name;
                iIndex++;
            }

            FS.FrameWork.Models.NeuObject qualityObj = new FS.FrameWork.Models.NeuObject();
            qualityObj.ID = "ALL";
            qualityObj.Name = "全部";

            alQuality.Insert(0, qualityObj);
            this.qualityHelper = new FS.FrameWork.Public.ObjectHelper(alQuality);

            this.cmbQuality.AddItems(alQuality);
            /////////////////////////////////////////////////////////////
           FS.HISFC.BizLogic.Manager.PowerLevelManager levelManager = new FS.HISFC.BizLogic.Manager.PowerLevelManager();
            ArrayList alLevel = levelManager.LoadLevel3ByLevel2("0320");
            if (alLevel == null)
            {
                MessageBox.Show(Language.Msg("根据常数类别获取出库类型名称发生错误!") + consManager.Err);
               this.privTypeHelper = new FS.FrameWork.Public.ObjectHelper();
                return;
           }

           ArrayList alNeuLevel = new ArrayList();
           FS.FrameWork.Models.NeuObject objLevel = null;

         foreach (FS.HISFC.Models.Admin.PowerLevelClass3 info in alLevel)
           {
               objLevel = new FS.FrameWork.Models.NeuObject();

               objLevel.ID = info.Class3Code;
               objLevel.Name = info.Class3Name;

               alNeuLevel.Add(objLevel);

          }

          objLevel = new FS.FrameWork.Models.NeuObject();

          objLevel.ID = "ALL";
          objLevel.Name = "全部";

          alNeuLevel.Insert(0, objLevel);
          this.cmbPrivType.AddItems(alNeuLevel);
       
        }


        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        protected override int OnDrawTree()
        {
            if (tvLeft == null)
            {
            return -1;
            }
            ArrayList deptList = managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.PI);

            TreeNode parentTreeNode = new TreeNode("全部");
            tvLeft.Nodes.Add(parentTreeNode);
            foreach (FS.HISFC.Models.Base.Department dept in deptList)
            {
            TreeNode deptNode = new TreeNode();
            deptNode.Tag = dept.ID;
            deptNode.Text = dept.Name;
            parentTreeNode.Nodes.Add(deptNode);
            }

            parentTreeNode.ExpandAll();

            return base.OnDrawTree();
        }
        
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbType.SelectedIndex >= 0)
            {
                //drugType = ((FS.FrameWork.Models.NeuObject)cmbType.Items[this.cmbType.SelectedIndex]).ID;
                drugType = cmbType.SelectedItem.ID;
                orugtypeName = cmbType.SelectedItem.Name;
                //MessageBox.Show(drugType);
                
            }
        }

        private void cmbQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbQuality.SelectedIndex >= 0)
            {
                //drugType = ((FS.FrameWork.Models.NeuObject)cmbType.Items[this.cmbType.SelectedIndex]).ID;
                drugQuality = cmbQuality.SelectedItem.ID;
                orugqualityName = cmbQuality.SelectedItem.Name;
                //MessageBox.Show(drugQuality);

            }
        }

        private void cmbPrivType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPrivType.SelectedIndex >= 0)
            {
                //drugType = ((FS.FrameWork.Models.NeuObject)cmbType.Items[this.cmbType.SelectedIndex]).ID;
                privType = cmbPrivType.SelectedItem.ID;
                outtypeName = cmbPrivType.SelectedItem.Name;
                //MessageBox.Show(privType);

            }
        }

        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
            return -1;
            }
            TreeNode selectNode = tvLeft.SelectedNode;

            if (selectNode.Level == 0)
            {
            return -1;
            }
            string deptCode = selectNode.Tag.ToString();
            string deptName = selectNode.Text.ToString();

            return base.OnRetrieve(base.beginTime, base.endTime, deptCode, drugType, drugQuality, privType, orugtypeName, orugqualityName, outtypeName);
        }

        private void neuLabel3_Click(object sender, EventArgs e)
        {

        }
        

    }
}
