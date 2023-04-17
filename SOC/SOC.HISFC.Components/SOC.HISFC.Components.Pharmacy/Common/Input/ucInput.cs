using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.HISFC.Components.Pharmacy.Base;

namespace FS.SOC.HISFC.Components.Pharmacy.Common.Input
{
    /// <summary>
    /// [功能描述: 入库统一界面]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-3]<br></br>
    /// </summary>
    public partial class ucInput : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucInput()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 入库相关业务流程处理对应的class对象(已被接口标准化)
        /// </summary>
        Base.IBaseBiz IBaseBiz = null;       

        /// <summary>
        /// 入库业务管理（保留工厂模式）
        /// </summary>
        //InputBizManager InputBizManager = new InputBizManager();
        IPharmacyBizManager InputBizManager;

        /// <summary>
        /// 权限科室
        /// </summary>
        FS.FrameWork.Models.NeuObject priveDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 权限帮助类，用户选择入库类别后可以快速的获取权限实体
        /// </summary>
        FS.FrameWork.Public.ObjectHelper inputTypeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 人员管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Person personMgr = new FS.HISFC.BizLogic.Manager.Person();

        /// <summary>
        /// 领药人帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper empHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 科室默认的领药人帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptGetPerSonHelper = new FS.FrameWork.Public.ObjectHelper();
        #endregion

        #region IPreArrange 成员

        public int PreArrange()
        {
            if (this.DesignMode)
            {
                return 0;
            }
            return this.SetPriveDept();
        }       

        #endregion

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        private int Init()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载基础数据...");
            Application.DoEvents();

            this.SetInputType();

            try
            {
                string lastType = "";
                string fileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PharmacyInputSetting.xml";
                string operCode = FS.FrameWork.Management.Connection.Operator.ID;
                lastType = SOC.Public.XML.SettingFile.ReadSetting(fileName, "InputType", "Cur" + this.priveDept.ID + operCode, lastType);
                for (int i = 0; i < this.inputTypeHelper.ArrayObject.Count; i++)
                {
                    FS.FrameWork.Models.NeuObject prive = this.inputTypeHelper.ArrayObject[i] as FS.FrameWork.Models.NeuObject;
                    if (prive.ID == lastType)
                    {
                        this.ncmbInputType.SelectedIndex = i;
                    }
                }
                if (this.ncmbInputType.SelectedIndex < 0)
                {
                    this.ncmbInputType.SelectedIndex = 0;
                }
                int param = this.SetBizInstance();
                if (param == 1)
                {
                    this.SetFromDept();
                    this.SetTargetDept();
                    this.SetGetPersonList();
                }
            }
            catch { }

            this.nlbPriveDept.DoubleClick += new EventHandler(nlbPriveDept_DoubleClick);
            this.ncmbInputType.SelectedIndexChanged += new EventHandler(ncmbInputType_SelectedIndexChanged);
            this.ncmbInputType.KeyPress += new KeyPressEventHandler(c_KeyPress);

            this.ncmbFromDept.SelectedIndexChanged += new EventHandler(ncmbFromDept_SelectedIndexChanged);
            this.ncmbFromDept.KeyPress += new KeyPressEventHandler(c_KeyPress);
            this.ncmbFromDept.TextChanged +=new EventHandler(ncmbFromDept_TextChanged);

            this.ncmbTargetDept.KeyPress += new KeyPressEventHandler(c_KeyPress);
            this.ncmbTargetDept.SelectedIndexChanged += new EventHandler(ncmbTargetDept_SelectedIndexChanged);
            this.ncmbTargetDept.TextChanged += new EventHandler(ncmbTargetDept_TextChanged);

            this.ncmbGetPerson.KeyPress += new KeyPressEventHandler(c_KeyPress);
            this.ncmbGetPerson.TextChanged += new EventHandler(ncmbGetPerson_TextChanged);
            this.ncmbGetPerson.SelectedIndexChanged += new EventHandler(ncmbGetPerson_SelectedIndexChanged);
            //>>{A848B607-175D-43b3-8109-C57F1E15F135}默认焦点-供货公司
            this.ncmbFromDept.Select();
            this.ncmbFromDept.SelectAll();
            this.ncmbFromDept.Focus();
            //<<
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();


            return 1;
        }

        /// <summary>
        /// 初始化领药人列表
        /// </summary>
        private void SetGetPersonList()
        {
           
            ArrayList alPerson = personMgr.GetUserEmployeeAll();
            if (alPerson != null)
            {
                this.ncmbGetPerson.AddItems(alPerson);
                empHelper.ArrayObject = alPerson;
            }
  
            this.nlbGetPerson.Visible = this.nlbTargetDept.Visible;
            this.ncmbGetPerson.Visible = this.ncmbTargetDept.Visible;

            ArrayList allNeuObj = consMgr.GetAllList("DEFAULTGETPERSON");
            if (allNeuObj != null && allNeuObj.Count > 0)
            {
                deptGetPerSonHelper.ArrayObject = allNeuObj;
            }
        }

        /// <summary>
        /// 设置权限科室
        /// </summary>
        /// <returns></returns>
        private int SetPriveDept()
        {
            int param = Function.ChoosePriveDept("0310", ref this.priveDept);
            if (param == 0 || param == -1 || this.priveDept == null || string.IsNullOrEmpty(this.priveDept.ID))
            {
                return -1;
            }

            this.ShowStatusBarTip("库存科室：" + priveDept.Name);
            this.nlbPriveDept.Text = "您选择的科室是【" + this.priveDept.Name + "】";

            return 1;
        }

        /// <summary>
        /// 设置操作员拥有权限的入库列表
        /// </summary>
        /// <returns></returns>
        private int SetInputType()
        {
            #region 获取当前操作员具有的权限集合
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager powerDetailManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();

            List<FS.FrameWork.Models.NeuObject> listPrive = powerDetailManager.QueryUserPrivCollection(powerDetailManager.Operator.ID, "0310", this.priveDept.ID);
            if (listPrive == null)
            {
                Function.ShowMessage("读取操作员操作权限集合时出错!\n" + powerDetailManager.Err, MessageBoxIcon.Error);
                return -1;
            }

            #endregion

            #region 获取三级权限涵义码

            FS.HISFC.Models.Admin.PowerLevelClass3 privClass3;

            FS.HISFC.BizLogic.Manager.PowerLevelManager powerLevelManager = new FS.HISFC.BizLogic.Manager.PowerLevelManager();
            foreach (FS.FrameWork.Models.NeuObject info in listPrive)
            {
                privClass3 = powerLevelManager.LoadLevel3ByPrimaryKey("0310", info.ID);
                if (privClass3 == null)
                {
                    Function.ShowMessage("获取三级权限涵义码出错!" + powerLevelManager.Err, MessageBoxIcon.Error);
                    return -1;
                }
                info.Memo = privClass3.Class3MeaningCode;
            }
            this.inputTypeHelper.ArrayObject = new ArrayList(listPrive.ToArray());
            this.ncmbInputType.AddItems(this.inputTypeHelper.ArrayObject);
            #endregion

            return 1;
        }

        /// <summary>
        /// 设置业务管理实例
        /// </summary>
        /// <returns></returns>
        private int SetBizInstance()
        {
            if (!string.IsNullOrEmpty(this.ncmbInputType.Text) && this.ncmbInputType.Tag != null)
            {
                FS.FrameWork.Models.NeuObject prive = this.inputTypeHelper.GetObjectFromID(this.ncmbInputType.Tag.ToString());
                if (prive != null)
                {
                    #region 当前入库类别对应的业务管理类（接口）实例
                    if (this.IBaseBiz != null)
                    {
                        if (this.IBaseBiz.PriveType != null && this.IBaseBiz.PriveType.ID == prive.ID && this.IBaseBiz.PriveType.Memo == prive.Memo)
                        {
                            return 0;
                        }
                        //释放原有业务管理类实例的资源
                        this.IBaseBiz.Dispose();
                        this.npanelInputInfo.Controls.Clear();
                    }
                    //获取当前入库类别对应的业务管理类（接口）实例
                    this.IBaseBiz = null;
                    if (InputBizManager == null)
                    {
                        InputBizManager = Function.GetInputBizManager();
                    }
                    this.IBaseBiz = InputBizManager.GetBizImplement(prive);

                    if (this.IBaseBiz != null)
                    {
                        this.IBaseBiz.PriveType = prive.Clone();
                        //初始化业务管理类实例的数据，这个可能是一般入库、发票补录等需要的基本数据
                        this.IBaseBiz.SetStockDept(this.priveDept.Clone());
                        if (this.ncmbFromDept.Text != "" && this.ncmbFromDept.Tag != null && this.ncmbFromDept.Tag.ToString() != "")
                        {
                            FS.FrameWork.Models.NeuObject fromDept = new FS.FrameWork.Models.NeuObject();
                            fromDept.ID = this.ncmbFromDept.Tag.ToString();
                            this.IBaseBiz.SetFromDept(fromDept);
                        }

                        FS.FrameWork.WinForms.Forms.frmBaseForm frmBase = (FS.FrameWork.WinForms.Forms.frmBaseForm)this.ParentForm;

                        this.IBaseBiz.Init(this.ucDataChooseList1, this.ucDataDetail1, frmBase.toolBar1);
                        
                        if (this.IBaseBiz.InputInfoControl != null)
                        {
                            this.npanelInputInfo.Height = this.IBaseBiz.InputInfoControl.Height;
                            this.IBaseBiz.InputInfoControl.Dock = DockStyle.Fill;
                            this.npanelInputInfo.Controls.Add(this.IBaseBiz.InputInfoControl);

                        }
                        else
                        {
                            this.npanelInputInfo.Controls.Clear();
                            this.npanelInputInfo.Height = 0;
                        }
                        this.IBaseBiz.SetFromDeptEven -= new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.SetFromDeptHander(IBaseBiz_SetFromDeptHander);
                        this.IBaseBiz.SetFromDeptEven += new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.SetFromDeptHander(IBaseBiz_SetFromDeptHander);
                        
                    }
                    else
                    {
                        Function.ShowMessage("没有实现二级权限0310的三级权限" + prive.ID + "代表的业务管理类，请与系统管理员联系！", MessageBoxIcon.Error);
                        return -1;
                    }
                   
                    #endregion

                    #region 保存操作类别
                    string fileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PharmacyInputSetting.xml";
                    string operCode = FS.FrameWork.Management.Connection.Operator.ID;
                    SOC.Public.XML.SettingFile.SaveSetting(fileName, "InputType", "Cur" + this.priveDept.ID + operCode, prive.ID);
                    #endregion

                }
            }

            return 1;
        }

        /// <summary>
        /// 设置入库来源，在SetBizInstance后调用
        /// 供货公司和院外机构、单位等都作为供货公司用供货入库，即一般入库；
        /// 本院科室都用科室（回笼）入库，即特殊入库
        /// </summary>
        /// <returns></returns>
        private int SetFromDept()
        {
            if (this.IBaseBiz == null)
            {
                Function.ShowMessage("没有实现" + this.ncmbInputType.Text + "代表的业务管理类，请与系统管理员联系！", MessageBoxIcon.Error);
                return -1;
            }

            string fromDeptTypeName = "";
            ArrayList al = this.IBaseBiz.GetFromDeptArray(ref fromDeptTypeName);
            this.nlbFromDept.Text = fromDeptTypeName;
            this.ncmbFromDept.Tag = "";
            this.ncmbFromDept.Text = "";
            if (al != null)
            {
                this.ncmbFromDept.AddItems(al);
            }
            else
            {
                Function.ShowMessage(this.ncmbInputType.Text + "代表的业务管理类中GetFromDeptArray函数返回null，请与系统管理员联系！", MessageBoxIcon.Error);
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 设置出库科室即领药科室，用于即入即出
        /// </summary>
        /// <returns></returns>
        private int SetTargetDept()
        {

            if (this.IBaseBiz == null)
            {
                Function.ShowMessage("没有实现" + this.ncmbInputType.Text + "代表的业务管理类，请与系统管理员联系！", MessageBoxIcon.Error);
                return -1;
            }
            ArrayList al = this.IBaseBiz.GetTargetDeptArray();
            this.ncmbFromDept.Tag = "";
            this.ncmbFromDept.Text = "";
            if (al != null)
            {
                this.ncmbTargetDept.AddItems(al);
                this.ncmbTargetDept.Visible = true;
                this.nlbTargetDept.Visible = true;
            }
            else
            {
                this.ncmbTargetDept.Visible = false;
                this.nlbTargetDept.Visible = false;
            }
            return 1;
        }

        /// <summary>
        /// 提供在状态栏第一panel显示信息的功能
        /// </summary>
        /// <param name="text">显示信息的文本</param>
        private void ShowStatusBarTip(string text)
        {
            if (this.ParentForm != null)
            {
                if (this.ParentForm is FS.FrameWork.WinForms.Forms.frmBaseForm)
                {
                    ((FS.FrameWork.WinForms.Forms.frmBaseForm)this.ParentForm).statusBar1.Panels[1].Text = "  " + text + "       ";
                }
            }
        }

        #endregion

        #region 事件
        
        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            base.OnLoad(e);
        }
        
        void nlbPriveDept_DoubleClick(object sender, EventArgs e)
        {
            //this.SetPriveDept();
        }

        void c_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (sender is ComboBox && this.ucDataChooseList1 != null)
                {
                    if (((Control)sender).Name == this.ncmbFromDept.Name && !this.ncmbTargetDept.Visible)
                    {
                        this.ucDataChooseList1.SetFocusToFilter();
                    }
                    else

                        if (((Control)sender).Name == this.ncmbGetPerson.Name)
                        {
                            this.ucDataChooseList1.SetFocusToFilter();
                        }
                        else
                        {
                            SendKeys.Send("{Tab}");
                        }
                }
            }
        }

        void ncmbInputType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int param = this.SetBizInstance();
            if (param == 1)
            {
                this.SetFromDept();
                this.SetTargetDept();
                this.SetGetPersonList();
            }
        }

        void ncmbInputType_TextChanged(object sender, EventArgs e)
        {
            int param = this.SetBizInstance();
            if (param == 1)
            {
                this.SetFromDept();
                this.SetTargetDept();
                this.SetGetPersonList();
            }
        }

        void ncmbFromDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ncmbFromDept.Text != "" && this.ncmbFromDept.Tag != null && this.ncmbFromDept.Tag.ToString() != "")
            {
                if (this.IBaseBiz != null)
                {
                    FS.FrameWork.Models.NeuObject fromDept = new FS.FrameWork.Models.NeuObject();
                    fromDept.ID = this.ncmbFromDept.Tag.ToString();
                    this.IBaseBiz.SetFromDept(fromDept);
                }
            }
            else 
            {
                if (this.IBaseBiz != null)
                {
                    this.IBaseBiz.SetFromDept(new FS.FrameWork.Models.NeuObject());
                }
            }
        }

        void ncmbFromDept_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.ncmbFromDept.Text))
            {
                if (this.IBaseBiz != null)
                {
                    this.IBaseBiz.SetFromDept(new FS.FrameWork.Models.NeuObject());
                }
            }
        }

        private void SetDefaultGetPerson()
        {
            if (this.ncmbTargetDept.Tag == null)
            {
                this.ncmbGetPerson.Tag = null;
            }
            string deptObj = this.ncmbTargetDept.Tag.ToString();
            if (!string.IsNullOrEmpty(deptObj))
            {
                FS.FrameWork.Models.NeuObject deptGetPersonObj = deptGetPerSonHelper.GetObjectFromID(deptObj);
                if (deptGetPersonObj != null)
                {
                    FS.FrameWork.Models.NeuObject empInfo = empHelper.GetObjectFromID(deptGetPersonObj.Name);
                    this.ncmbGetPerson.Tag = empInfo.ID;
                    this.ncmbGetPerson.Text = empInfo.Name;
                }
                else
                {
                    this.ncmbGetPerson.Tag = string.Empty;
                    this.ncmbGetPerson.Text = string.Empty;
                }
            }
           
        }

        void ncmbGetPerson_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.ncmbGetPerson.Text))
            {
                if (this.IBaseBiz != null)
                {
                    this.IBaseBiz.SetGetPerson(new FS.FrameWork.Models.NeuObject());
                }
            }
        }

        void ncmbGetPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ncmbGetPerson.Text != "" && this.ncmbGetPerson.Tag != null && this.ncmbGetPerson.Tag.ToString() != "")
            {
                if (this.IBaseBiz != null)
                {
                    FS.FrameWork.Models.NeuObject getPerson = new FS.FrameWork.Models.NeuObject();
                    getPerson.ID = this.ncmbGetPerson.Tag.ToString();
                    this.IBaseBiz.SetGetPerson(getPerson);
                }
            }
            else
            {
                if (this.IBaseBiz != null)
                {
                    this.IBaseBiz.SetGetPerson(new FS.FrameWork.Models.NeuObject());
                }
            }
        }

        void ncmbTargetDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ncmbTargetDept.Text != "" && this.ncmbTargetDept.Tag != null && this.ncmbTargetDept.Tag.ToString() != "")
            {
                if (this.IBaseBiz != null)
                {
                    FS.FrameWork.Models.NeuObject targetDept = new FS.FrameWork.Models.NeuObject();
                    targetDept.ID = this.ncmbTargetDept.Tag.ToString();
                    this.IBaseBiz.SetTargetDept(targetDept);
                }
            }
            else
            {
                if (this.IBaseBiz != null)
                {
                    this.IBaseBiz.SetTargetDept(new FS.FrameWork.Models.NeuObject());
                }
            }
            this.SetDefaultGetPerson();
        }

        void ncmbTargetDept_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.ncmbTargetDept.Text))
            {
                if (this.IBaseBiz != null)
                {
                    this.IBaseBiz.SetTargetDept(new FS.FrameWork.Models.NeuObject());
                }
            }
            this.SetDefaultGetPerson();
        }

        void IBaseBiz_SetFromDeptHander(FS.FrameWork.Models.NeuObject fromDept)
        {
            this.ncmbFromDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(fromDept.ID);
            this.ncmbFromDept.Tag = fromDept.ID;
        }

        #endregion

        #region 工具栏
        
        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            //toolBarService.AddToolButton("入库单", "入库单据列表", FS.FrameWork.WinForms.Classes.EnumImageList.R入库单, true, false, null);
            //toolBarService.AddToolButton("出库单", "出库单据列表", FS.FrameWork.WinForms.Classes.EnumImageList.C出库单, true, false, null);
            //toolBarService.AddToolButton("导入", "导入数据", FS.FrameWork.WinForms.Classes.EnumImageList.D导入, true, false, null);
            toolBarService.AddToolButton("删除", "删除数据", FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);

            return toolBarService;
        }
       
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (IBaseBiz != null)
            {
                IBaseBiz.ToolbarAfterClick(e.ClickedItem.Text);
                //if (e.ClickedItem.Text == "入库单")
                //{
                //    if (this.ucDataDetail1.FpSpread.Sheets[0].RowCount > 0)
                //    {
                //        this.ucDataDetail1.FpSpread.Select();
                //        this.ucDataDetail1.FpSpread.Sheets[0].SetActiveCell(0, 0);
                //        this.ucDataDetail1.SetFocus();
                //    }
                //}
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            //{83EFE62B-800E-48e6-A3C4-8D939E8DEE51}
            if (MessageBox.Show("保存后单据将立即生效，确定进行保存操作吗？", "提示", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return -1;
            }

            int param = IBaseBiz.ToolbarAfterClick("保存");
            if (param == -2)
            {
                this.ncmbFromDept.Select();
                this.ncmbFromDept.SelectAll();
                this.ncmbFromDept.Focus();
            }
            else if (param == -3 && this.ncmbTargetDept.Visible)
            {
                this.ncmbTargetDept.Select();
                this.ncmbTargetDept.SelectAll();
                this.ncmbTargetDept.Focus();
            }
            else if (param == -4)
            {
                this.ucDataChooseList1.SetFocusToFilter();
            }
            else if (param == 1)
            {
                this.ncmbFromDept.Select();
                this.ncmbFromDept.SelectAll();
                this.ncmbFromDept.Focus();
            }
            return base.OnSave(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            IBaseBiz.ToolbarAfterClick("导出");
            return base.Export(sender, neuObject);
        }

        #endregion

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.ucDataDetail1.ContainsFocus)
                {
                    if (this.ucDataDetail1.SetFocus(true) == 0)
                    {
                        this.ucDataChooseList1.SetFocusToFilter();
                    }

                }
            }
            this.IBaseBiz.ProcessDialogKey(keyData);
            return base.ProcessDialogKey(keyData);
        }

    }
}
