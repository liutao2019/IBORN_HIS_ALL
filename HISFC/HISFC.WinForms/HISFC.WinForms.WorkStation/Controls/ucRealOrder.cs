using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;

namespace Neusoft.HISFC.WinForms.WorkStation.Controls
{
    /// <summary>
    /// 医嘱管理主窗口
    /// </summary>
    public partial class ucRealOrder : Neusoft.FrameWork.WinForms.Controls.ucBaseForm,Neusoft.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        /// <summary>
        /// 当前控件
        /// </summary>
        private Control CurrentControl;

        /// <summary>
        /// 医嘱管理
        /// </summary>
        public ucRealOrder()
        {

            InitializeComponent();

            this.SetTree(this.tvDoctorPatientList1);
            this.iQueryControlable = this.ucOrder1 as Neusoft.FrameWork.WinForms.Forms.IQueryControlable;
            this.iControlable = this.ucOrder1 as Neusoft.FrameWork.WinForms.Forms.IControlable;
            this.CurrentControl = this.ucOrder1;
            this.tbGroup.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.Z组套);
            this.tbEMR.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.B病历);
            this.tbEMR.Text = "病历";
            this.tbQueryOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.C查询);
            this.tbExitOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.T退出);
            this.tbPrintOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.D打印);
            this.tbGroup.CheckState = CheckState.Unchecked;
            this.Resize += new EventHandler(frmOrder_Resize);
            #region  {49026086-DCA3-4af4-A064-58F7479C324A}
            this.ucOrder1.refreshGroup += new Neusoft.HISFC.Components.Order.Controls.RefreshGroupTree(ucOrder1_refreshGroup);
            #endregion
            this.neuTextBox1.MouseDoubleClick += new MouseEventHandler(neuTextBox1_MouseDoubleClick);
        }

        void neuTextBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode parent = new TreeNode();

            foreach (TreeNode node in this.tvDoctorPatientList1.Nodes)
            {
                if (node.Text.IndexOf("本科室") >= 0)
                {
                    parent = node;
                    break;
                }
            }

            if (Neusoft.FrameWork.WinForms.Classes.Function.PopShowControl(uc) == DialogResult.OK)
            {
                TreeNode node = new TreeNode();

                if (Neusoft.HISFC.Components.Common.Controls.ucQueryInpatient.patient.PVisit.PatientLocation.Dept.ID
                    == (this.consultation.Operator as Neusoft.HISFC.Models.Base.Employee).Dept.ID)
                {
                    Neusoft.HISFC.Components.Common.Controls.ucQueryInpatient.patient.Patient.Memo = "科室";
                }
                else
                {
                    Neusoft.HISFC.Components.Common.Controls.ucQueryInpatient.patient.Patient.Memo = "医技";
                }

                node.Tag = Neusoft.HISFC.Components.Common.Controls.ucQueryInpatient.patient;
                node.Text = Neusoft.HISFC.Components.Common.Controls.ucQueryInpatient.patient.Name;

                parent.Nodes.Add(node);
                parent.ExpandAll();
            }
        }

        #region {49026086-DCA3-4af4-A064-58F7479C324A}
        /// <summary>
        /// 刷新组套树
        /// </summary>
        private void ucOrder1_refreshGroup()
        {
            this.tvGroup.RefrshGroup();
        }
        #endregion
        //{A5409134-55B5-42d9-A264-25060169A64B}
        private Neusoft.FrameWork.Public.ObjectHelper frequencyHelper = new Neusoft.FrameWork.Public.ObjectHelper();
        private Neusoft.FrameWork.Public.ObjectHelper orderTypeHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        private Neusoft.HISFC.Components.Common.Controls.ucQueryInpatient uc = new Neusoft.HISFC.Components.Common.Controls.ucQueryInpatient();
        /// <summary>
        /// 传染病上报类
        /// </summary>
        private Neusoft.HISFC.BizProcess.Interface.DCP.IDCP dcpInstance = null;


        void frmOrder_Resize(object sender, EventArgs e)
        {
            this.panelTree.Height = this.Height - 162;

        }


        Neusoft.HISFC.Components.Common.Controls.tvDoctorGroup tvGroup = null;//组套
        bool isEditGroup = false;
        private void ucRealOrder_Load(object sender, EventArgs e)
        {
            this.tbFilter.DropDownItemClicked += new ToolStripItemClickedEventHandler(toolStrip1_ItemClicked_1);
            this.AddOrderHandle();
            this.initButton(false);

            this.tbAddOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.Y医嘱);
            this.tbComboOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.H合并);
            this.tbCancelOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.Q取消);
            this.tbDelOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.S删除);
            this.tbOperation.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.Z诊断);
            this.tbSaveOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.B保存);
            this.tbCheck.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.H换单);
            this.tb1Exit.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.T退出);
            this.tbEMR.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.B病历);
            this.tsbHerbal.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.M明细);
            ///{FB86E7D8-A148-4147-B729-FD0348A3D670}  增加医嘱重整按钮
            this.tbRetidyOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.Y医嘱);

            this.tbLevelOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.Z子级);

            #region add by xuewj 增加化疗按钮 {1F2B9330-7A32-4da4-8D60-3A4568A2D1D8}
            this.tbAssayCure.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.H护理);
            #endregion
            #region {3559BBC9-3799-4a46-AB1A-716601D34543}
            this.tbDiseaseReport.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.J健康档案);
            this.tbFilter.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.C查找);
            #endregion
            //加入选择医生按钮{D5517722-7128-4d0c-BBC4-1A5558A39A03}
            this.tbChooseDoct.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.H换医师);

            this.tbLisResultPrint.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.H化验);
            this.tbPacsResultPrint.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.S设备);
            this.tbDcAllLongOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.Q全退);
            this.panelTree.Height = this.Height - 162;
            //{A5409134-55B5-42d9-A264-25060169A64B}
            ArrayList alFrequency = Neusoft.HISFC.Components.Order.Classes.Function.HelperFrequency.ArrayObject;
            if (alFrequency != null)
            {
                this.frequencyHelper = new Neusoft.FrameWork.Public.ObjectHelper(alFrequency);
            }
            this.toolStrip1.Focus();

            #region {F6767B0D-4BA4-4920-863F-40912AC3B554}

            Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlParamMgr = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();

            string treeSearchMode = "1";

            treeSearchMode = controlParamMgr.GetControlParam<string>("200303", true, "1");

            if (treeSearchMode == "2")
            {
                this.ucQueryInpatientNo1.Visible = true;
            }
            else
            {
                this.ucQueryInpatientNo1.Visible = false;
            }

            #endregion

            #region {0768F6B2-B3FD-42f4-B83E-3422E3A319E2}
            this.SetMenuVisible();
            #endregion

            this.IsDoubleSelectValue = false;

            this.ucOrder1.OnRefreshGroupTree += new EventHandler(ucOrder1_OnRefreshGroupTree);

            Neusoft.HISFC.BizProcess.Integrate.Manager manager = new Neusoft.HISFC.BizProcess.Integrate.Manager();
            ArrayList alOrderType = manager.QueryOrderTypeList();
            if (alOrderType != null)

                orderTypeHelper.ArrayObject = alOrderType;

            Application.DoEvents();

            if (this.dcpInstance == null)
            {
                this.dcpInstance = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.DCP.IDCP)) as Neusoft.HISFC.BizProcess.Interface.DCP.IDCP;
            }

            if (this.dcpInstance != null)
            {
                this.dcpInstance.LoadNotice(this, Neusoft.HISFC.Models.Base.ServiceTypes.I);
            }
      
            this.formID = "M" + this.formID;

            this.panel2.Visible = false;
        }

        void ucOrder1_OnRefreshGroupTree(object sender, EventArgs e)
        {
            this.tvGroup.RefrshGroup();
        }


        #region 私有函数

        private void StatrEmr(string formText, string formType, string tag, string controlAssembly, string controlType, string treeAssembly, string treeType)
        {
            string title = "";// GetTitle();
            Neusoft.HISFC.Models.Base.Employee oper = Neusoft.FrameWork.Management.Connection.Operator as Neusoft.HISFC.Models.Base.Employee;
            if (title.Length <= 0)
            {
                System.Diagnostics.Process pro = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo star = new System.Diagnostics.ProcessStartInfo();
                star.FileName = Application.StartupPath + "\\HIS4.5\\Bridge.exe";
                string argument = oper.ID;
                argument += " " + oper.Name;
                argument += " " + oper.Dept.ID;
                argument += " " + oper.Dept.Name;
                if (formType.Length <= 0)
                {
                    argument += " " + formText + " " + "FormBase";
                }
                else
                {
                    argument += " " + formText + " " + formType;
                }
                if (tag.Length <= 0)
                {
                    argument += " " + "empty" + " " + controlAssembly + " " + controlType;
                }
                else
                {
                    argument += " " + tag + " " + controlAssembly + " " + controlType;
                }
                if (oper.Nurse.ID.Length <= 0)
                {
                    argument += " " + "EMPTY" + " " + oper.CurrentGroup.ID;
                }
                else
                {
                    argument += " " + oper.Nurse.ID + " " + oper.CurrentGroup.Name;

                }
                argument += " " + treeType;
                star.Arguments = argument;
                pro.StartInfo = star;
                pro.Start();
            }
        }
        /// <summary>
        /// {0768F6B2-B3FD-42f4-B83E-3422E3A319E2}
        /// 菜单设置可见性
        /// </summary>
        private void SetMenuVisible()
        {
            Neusoft.HISFC.BizProcess.Integrate.Manager dictionaryMgr = new Neusoft.HISFC.BizProcess.Integrate.Manager();

            ArrayList alset = dictionaryMgr.GetConstantList("INPATMENU");

            Neusoft.FrameWork.Public.ObjectHelper setHelper = new Neusoft.FrameWork.Public.ObjectHelper();

            setHelper.ArrayObject = alset;

            if (setHelper.GetObjectFromID(this.tb1Exit.Name) == null)
            {
                this.tb1Exit.Visible = false;
            }
            if (setHelper.GetObjectFromID(this.tbAddOrder.Name) == null)
            {
                this.tbAddOrder.Visible = false;
            }
            if (setHelper.GetObjectFromID(this.tbAssayCure.Name) == null)
            {
                this.tbAssayCure.Visible = false;
            }
            if (setHelper.GetObjectFromID(this.tbCancelOrder.Name) == null)
            {
                this.tbCancelOrder.Visible = false;
            }
            if (setHelper.GetObjectFromID(this.tbCheck.Name) == null)
            {
                this.tbCheck.Visible = false;
            }
            if (setHelper.GetObjectFromID(this.tbChooseDoct.Name) == null)
            {
                this.tbChooseDoct.Visible = false;
            }
            if (setHelper.GetObjectFromID(this.tbComboOrder.Name) == null)
            {
                this.tbComboOrder.Visible = false;
            }
            if (setHelper.GetObjectFromID(this.tbDelOrder.Name) == null)
            {
                this.tbDelOrder.Visible = false;
            }
            if (setHelper.GetObjectFromID(this.tbDiseaseReport.Name) == null)
            {
                this.tbDiseaseReport.Visible = false;
            }
            if (setHelper.GetObjectFromID(this.tbExitOrder.Name) == null)
            {
                this.tbExitOrder.Visible = false;
            }
            if (setHelper.GetObjectFromID(this.tbFilter.Name) == null)
            {
                this.tbFilter.Visible = false;
            }
            if (setHelper.GetObjectFromID(this.tbGroup.Name) == null)
            {
                this.tbGroup.Visible = false;
            }
            if (setHelper.GetObjectFromID(this.tbLisResultPrint.Name) == null)
            {
                this.tbLisResultPrint.Visible = false;
            }
            if (setHelper.GetObjectFromID(this.tbOperation.Name) == null)
            {
                this.tbOperation.Visible = false;
            }
            if (setHelper.GetObjectFromID(this.tbPacsResultPrint.Name) == null)
            {
                this.tbPacsResultPrint.Visible = false;
            }
            if (setHelper.GetObjectFromID(this.tbPrintOrder.Name) == null)
            {
                this.tbPrintOrder.Visible = false;
            }
            if (setHelper.GetObjectFromID(this.tbQueryOrder.Name) == null)
            {
                this.tbQueryOrder.Visible = false;
            }
            if (setHelper.GetObjectFromID(this.tbRefresh.Name) == null)
            {
                this.tbRefresh.Visible = false;
            }
            if (setHelper.GetObjectFromID(this.tbRetidyOrder.Name) == null)
            {
                this.tbRetidyOrder.Visible = false;
            }
            if (setHelper.GetObjectFromID(this.tbSaveOrder.Name) == null)
            {
                this.tbSaveOrder.Visible = false;
            }
            if (setHelper.GetObjectFromID(this.tsbHerbal.Name) == null)
            {
                this.tsbHerbal.Visible = false;
            }
            if (setHelper.GetObjectFromID(this.tbLevelOrder.Name) == null)
            {
                this.tbLevelOrder.Visible = false;
            }
            if (setHelper.GetObjectFromID(this.tbEMR.Name) == null)
            {
                this.tbEMR.Visible = false;
            }
        }

        private void initButton(bool isDisign)
        {
            this.tbGroup.Enabled = !isDisign;
            tbRefresh.Enabled = !isDisign;
            this.tbAddOrder.Enabled = !isDisign;
            this.tbComboOrder.Enabled = isDisign;
            this.tbCancelOrder.Enabled = isDisign;
            this.tbCheck.Enabled = isDisign;
            //this.tbOperation.Enabled = false;
            this.tbOperation.Enabled = isDisign;
            this.tbAssayCure.Enabled = isDisign;
            this.tbDelOrder.Enabled = isDisign;
            this.tbExitOrder.Enabled = isDisign;
            //this.tbFilter.Enabled = !isDisign;
            this.tbQueryOrder.Enabled = !isDisign;
            this.tbSaveOrder.Enabled = isDisign;
            this.tsbHerbal.Enabled = isDisign;
            this.tbLevelOrder.Enabled = isDisign;
            //this.tbDcAllLongOrder.Enabled = isDisign;
            //{D5517722-7128-4d0c-BBC4-1A5558A39A03}
            this.tbChooseDoct.Enabled = isDisign;
            if (isDisign) //开立
            {
                if (tvGroup == null)
                {
                    tvGroup = new Neusoft.HISFC.Components.Common.Controls.tvDoctorGroup();
                    tvGroup.Type = Neusoft.HISFC.Components.Common.Controls.enuType.Order;
                    tvGroup.Init();
                    tvGroup.SelectOrder += new Neusoft.HISFC.Components.Common.Controls.SelectOrderHandler(tvGroup_SelectOrder);
                }
                tvGroup.Dock = DockStyle.Fill;
                tvGroup.Visible = true;

                this.tvDoctorPatientList1.Visible = false;
                this.panelTree.Controls.Add(tvGroup);
                //{D5517722-7128-4d0c-BBC4-1A5558A39A03}
                //判断当前人员是否医生
                if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).EmployeeType.ID.ToString() == Neusoft.HISFC.Models.Base.EnumEmployeeType.D.ToString())
                {
                    this.tbChooseDoct.Enabled = false;
                }
                else
                {
                    this.tbChooseDoct.Enabled = true;
                }
                #region {190B18B2-9CF0-4b44-BB93-63A15387AD0B}
                if (this.ucOrder1.OrderType == Neusoft.HISFC.Models.Order.EnumType.LONG)
                {
                    this.tsbHerbal.Enabled = false;
                    this.tbLevelOrder.Enabled = false;
                    this.tbOperation.Enabled = false;
                }
                #endregion

            }
            else
            {
                this.tvDoctorPatientList1.Visible = true;
                if (tvGroup != null)
                    tvGroup.Visible = false;
            }
        }

        void tvGroup_SelectOrder(System.Collections.ArrayList alOrders)
        {
            //{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988} //草药弹出草药开立界面
            ArrayList alHerbal = new ArrayList(); //草药

            string comboID = "";
            int subCombNo = 0;
            Neusoft.HISFC.Models.Order.Inpatient.Order myorder = null;

            try
            {
                foreach (Neusoft.HISFC.Models.Order.Inpatient.Order order in alOrders)
                {
                    myorder = order.Clone();
                    if (myorder.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        ((Neusoft.HISFC.Models.Pharmacy.Item)myorder.Item).DoseUnit = myorder.DoseUnit;
                    }

                    myorder.Patient.PVisit.PatientLocation.Dept.ID = ((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).Dept.ID;
                    if (fillOrder(ref myorder) != -1)
                    {
                        #region 判断开立权限

                        string error = "";

                        int ret = 1;

                        ret = Components.Order.Classes.Function.JudgeEmplPriv(order, consultation.Operator,
                            (consultation.Operator as Neusoft.HISFC.Models.Base.Employee).Dept, Neusoft.HISFC.Models.Base.DoctorPrivType.SpecialDrug, false, ref error);

                        if (ret <= 0)
                        {
                            MessageBox.Show(error);
                            continue;
                        }

                        #endregion

                        if (order.Combo.ID != "" && order.Combo.ID != comboID)//新的
                        {
                            subCombNo = ucOrder1.GetMaxCombNo(-1);
                        }
                        comboID = order.Combo.ID;
                        myorder.SubCombNO = subCombNo;

                        if (order.Item.SysClass.ID.ToString() == "PCC") //草药
                        {
                            if (this.ucOrder1.fpOrder.ActiveSheetIndex == 0)
                            {
                                MessageBox.Show("项目[" + myorder.Item.Name + "]类别为" + myorder.Item.SysClass.ToString() + "，不可以开立为长期医嘱！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                continue;
                            }
                            alHerbal.Add(order);
                        }
                        else
                        {
                            if (myorder.OrderType.IsDecompose)
                            {
                                if (this.ucOrder1.fpOrder.ActiveSheetIndex == 0)
                                {
                                    //{2D788D8F-D5DB-447d-A3E1-282F0A446F1F}
                                    if (myorder.ExtendFlag6 == null || myorder.ExtendFlag6 == "")
                                    {
                                        myorder.ExtendFlag6 = "1";
                                    }

                                    this.ucOrder1.AddNewOrder(myorder, 0);
                                }
                                else
                                {
                                    #region 长期组套复制成临嘱

                                    try
                                    {
                                        if (myorder.OrderType.IsCharge)
                                        {
                                            myorder.OrderType = orderTypeHelper.GetObjectFromID("LZ") as Neusoft.HISFC.Models.Order.OrderType;
                                        }
                                        else
                                        {
                                            myorder.OrderType = orderTypeHelper.GetObjectFromID("ZL") as Neusoft.HISFC.Models.Order.OrderType;
                                        }

                                        if (myorder.OrderType == null)
                                        {
                                            continue;
                                        }

                                        Neusoft.HISFC.Components.Order.Classes.Function.SetDefaultFrequency(myorder);

                                        myorder.Qty = 0;

                                        if (myorder.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                                        {
                                            myorder.Unit = ((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).MinUnit;
                                        }
                                        else
                                        {
                                            myorder.Unit = myorder.Unit;
                                        }

                                        this.ucOrder1.AddNewOrder(myorder, 1);

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("长期医嘱转换为临时医嘱出错！" + ex.Message);
                                    }
                                    #endregion
                                }
                            }
                            else
                            {
                                if (this.ucOrder1.fpOrder.ActiveSheetIndex == 1)
                                {
                                    this.ucOrder1.AddNewOrder(myorder, 1);
                                }
                                else
                                {
                                    #region 临时组套复制成长嘱

                                    try
                                    {
                                        if (myorder.OrderType.IsCharge)
                                        {
                                            myorder.OrderType = orderTypeHelper.GetObjectFromID("CZ") as Neusoft.HISFC.Models.Order.OrderType;
                                        }
                                        else
                                        {
                                            myorder.OrderType = orderTypeHelper.GetObjectFromID("ZC") as Neusoft.HISFC.Models.Order.OrderType;
                                        }

                                        if (myorder.OrderType == null)
                                        {
                                            continue;
                                        }

                                        //判断是否可以复制
                                        bool b = false;
                                        string strSysClass = myorder.Item.SysClass.ID.ToString();
                                        //if (myorder.Item.SysClass.ID.ToString().Length > 1)
                                        //    strSysClass = myorder.Item.SysClass.ID.ToString().Substring(0, 2);
                                        myorder.HerbalQty = 0;
                                        //临时医嘱复制为长嘱，总量为0
                                        myorder.Qty = 0;
                                        //myorder.ExtendFlag6 = "0";

                                        switch (strSysClass)
                                        {
                                            case "UO":				//手术
                                            case "UC":				//检查
                                            case "PCC":				//中草药
                                            case "MC"://会诊
                                            case "MRB"://转床
                                            case "MRD": //转科
                                            case "MRH": //预约出院
                                            case "UL": //检验

                                                b = false;
                                                break;
                                            default:
                                                Neusoft.HISFC.Components.Order.Classes.Function.SetDefaultFrequency(myorder);
                                                b = true;
                                                break;
                                        }
                                        if (b == false)
                                        {
                                            MessageBox.Show("项目[" + myorder.Item.Name + "]类别为" + myorder.Item.SysClass.ToString() + "，不可以开立为长期医嘱！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            continue;
                                        }

                                        //{2D788D8F-D5DB-447d-A3E1-282F0A446F1F}
                                        if (myorder.ExtendFlag6 == null || myorder.ExtendFlag6 == "")
                                        {
                                            myorder.ExtendFlag6 = "1";
                                        }

                                        this.ucOrder1.AddNewOrder(myorder, 0);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("临时医嘱转换为长期医嘱出错！" + ex.Message);
                                    }

                                    #endregion
                                }
                            }
                        }
                    }
                }

                if (alHerbal.Count > 0)
                {
                    this.ucOrder1.AddHerbalOrders(alHerbal);
                }
                this.ucOrder1.RefreshCombo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int fillOrder(ref Neusoft.HISFC.Models.Order.Inpatient.Order order)
        {
            string err = "";
            //if (order.Item.IsPharmacy)
            if (order.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
            //if (order.Item.IsPharmacy)
            {
                if (Neusoft.HISFC.BizProcess.Integrate.Order.FillPharmacyItemWithStockDept(null, ref order, out err) == -1)
                {
                    MessageBox.Show(err);
                    return -1;
                }
            }
            else
            {
                if (Neusoft.HISFC.BizProcess.Integrate.Order.FillFeeItem(null, ref order, out err) == -1)
                {
                    MessageBox.Show(err);
                    return -1;
                }
            }
            //{A5409134-55B5-42d9-A264-25060169A64B}
            Neusoft.FrameWork.Models.NeuObject trueFrequency = this.frequencyHelper.GetObjectFromID(order.Frequency.ID);
            if (trueFrequency != null)
            {
                order.Frequency = trueFrequency as Neusoft.HISFC.Models.Order.Frequency;
            }

            return 0;
        }

        /// <summary>
        /// 设置按钮
        /// </summary>
        /// <param name="isEdit">是否开立模式</param>
        private void InitButtonGroup(bool isEdit)
        {
            this.tbAddOrder.Enabled = !isEdit;
            this.tbSaveOrder.Enabled = isEdit;
            this.tbRefresh.Enabled = !isEdit;
            this.isEditGroup = isEdit;
            //{EB959BC4-9120-478a-B527-74A1D7EF4C9E}
            this.tbComboOrder.Enabled = isEdit;
            this.tbCancelOrder.Enabled = isEdit;
            //{74E478F5-BDDD-4637-9F5A-E251AF9AA72F}
            this.tbRetidyOrder.Enabled = !isEdit;
            //this.tbDcAllLongOrder.Enabled = !isEdit;
            this.tbDelOrder.Enabled = isEdit;

            if (isEdit) //开立
            {
                if (tvGroup == null)
                {
                    tvGroup = new Neusoft.HISFC.Components.Common.Controls.tvDoctorGroup();
                    tvGroup.Type = Neusoft.HISFC.Components.Common.Controls.enuType.Order;
                    tvGroup.Init();
                    tvGroup.SelectOrder += new Neusoft.HISFC.Components.Common.Controls.SelectOrderHandler(tvGroup_SelectOrder);
                }
                tvGroup.Dock = DockStyle.Fill;
                tvGroup.Visible = true;

                this.tvDoctorPatientList1.Visible = false;
                this.panelTree.Controls.Add(tvGroup);

                this.panel2.Visible = true;
            }
            else
            {
                this.tvDoctorPatientList1.Visible = false;
                if (tvGroup != null)
                    tvGroup.Visible = true;

                this.panel2.Visible = false;
            }
        }

        private void AddOrderHandle()
        {
            this.ucOrder1.OrderCanCancelComboChanged += new Neusoft.HISFC.Components.Order.Controls.ucOrder.EventButtonHandler(ucOrder1_OrderCanCancelComboChanged);
            this.ucOrder1.OrderCanOperatorChanged += new Neusoft.HISFC.Components.Order.Controls.ucOrder.EventButtonHandler(ucOrder1_OrderCanOperatorChanged);
            this.ucOrder1.OrderCanSetCheckChanged += new Neusoft.HISFC.Components.Order.Controls.ucOrder.EventButtonHandler(ucOrder1_OrderCanSetCheckChanged);
        }

        void ucOrder1_OrderCanSetCheckChanged(bool b)
        {
            this.tbCheck.Enabled = b;
        }

        void ucOrder1_OrderCanOperatorChanged(bool b)
        {
            this.tbOperation.Enabled = b;
            this.tbAssayCure.Enabled = b;
            #region {190B18B2-9CF0-4b44-BB93-63A15387AD0B}
            this.tsbHerbal.Enabled = b;
            this.tbLevelOrder.Enabled = b;
            #endregion
        }

        void ucOrder1_OrderCanCancelComboChanged(bool b)
        {
            this.tbCancelOrder.Enabled = b;
        }
        #endregion

        Neusoft.HISFC.Models.RADT.PatientInfo patient = new Neusoft.HISFC.Models.RADT.PatientInfo();
        Neusoft.HISFC.BizProcess.Integrate.RADT inpatientManager = new Neusoft.HISFC.BizProcess.Integrate.RADT();
        Neusoft.HISFC.BizLogic.Order.Consultation consultation = new Neusoft.HISFC.BizLogic.Order.Consultation();
        protected string inpatientNo;
        ArrayList co = null;

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F9)
            {
                if (this.patient == null)
                {
                    MessageBox.Show("请选择具体的患者！");
                    return false;
                }
                this.StatrEmr("电子病历", "ShowDialog", "HIS45", "UFC.EPR", "UFC.EPR.Query.ucEPRMain", "", this.patient.ID);

            }
            //return true;
            return base.ProcessDialogKey(keyData);
        }

        private void toolStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == this.tbAddOrder)
            {
                //选择子节点
                if (this.tvDoctorPatientList1.SelectedNode.Parent != null && this.tvDoctorPatientList1.SelectedNode.Parent.Tag != null)
                {
                    int count = 0;
                    count = this.tvDoctorPatientList1.SelectedNode.Parent.GetNodeCount(false);
                    //判断所选节点父节点如果为会诊患者,则判断有无开立医嘱的权限/
                    //如果不是会诊患者则不需要进行判断,都可以进行开立医嘱
                    if (this.tvDoctorPatientList1.SelectedNode.Parent.Text == ("会诊患者" + "(" + count.ToString() + ")"))
                    {
                        patient = this.tvDoctorPatientList1.SelectedNode.Tag as Neusoft.HISFC.Models.RADT.PatientInfo;

                        //处理床位号截位
                        string bedNO = patient.PVisit.PatientLocation.Bed.ID;
                        if (bedNO.Length > 4)
                        {
                            bedNO = bedNO.Substring(4);
                        }
                        //处理住院号显示
                        string patientNO = patient.PID.PatientNO;
                        if (string.IsNullOrEmpty(patientNO) == true)
                        {
                            patientNO = patient.ID;
                        }

                        inpatientNo = patient.ID;
                        co = consultation.QueryConsulation(this.inpatientNo);
                        if (co != null || co.Count != 0)
                            for (int i = 0; i < co.Count; i++)
                            {
                                Neusoft.HISFC.Models.Order.Consultation obj = co[i] as Neusoft.HISFC.Models.Order.Consultation;
                                //根据会诊患者有效的会诊单信息,判断医生是否有对该会诊患者开立医嘱权限
                                if ((Neusoft.FrameWork.Management.Connection.Operator.ID == obj.DoctorConsultation.ID) &&
                                    (obj.EndTime >= consultation.GetDateTimeFromSysDateTime())
                                    && (obj.IsCreateOrder))
                                {
                                    if (this.ucOrder1.Add() == 0)
                                        this.initButton(true);
                                    break;//{3541798B-AF9C-415c-AFAA-8BD22A34A808}
                                }
                                else
                                {
                                    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("对不起,您没有对该患者开立医嘱的权限!"), "提示");
                                    return;
                                }
                            }
                    }
                    else
                    {
                        Neusoft.HISFC.Models.RADT.PatientInfo patient1 = this.tvDoctorPatientList1.SelectedNode.Tag as Neusoft.HISFC.Models.RADT.PatientInfo;

                        //处理床位号截位
                        string bedNO = patient1.PVisit.PatientLocation.Bed.ID;
                        if (bedNO.Length > 4)
                        {
                            bedNO = bedNO.Substring(4);
                        }
                        //处理住院号显示
                        string patientNO = patient1.PID.PatientNO;
                        if (string.IsNullOrEmpty(patientNO) == true)
                        {
                            patientNO = patient1.ID;
                        }

                        //this.Text = "您正在操作的患者为 住院号：" + patientNO + " 姓名：" + patient1.Name + " 性别：" + patient1.Sex.Name + " 年龄：" + consultation.GetAge(patient1.Birthday) + " 床号:" + bedNO;

                        //判断是否请假患者
                        if (patient1.PVisit.PatientLocation.Bed != null && patient1.PVisit.PatientLocation.Bed.Status.ID.ToString() == Neusoft.HISFC.Models.Base.EnumBedStatus.R.ToString())
                        {
                            //请假患者不能开医嘱，须先销假，主要为防止请假后开立长嘱下次执行时间不对，这个bug有点2
                            MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("患者请假中，如需开立医嘱请先销假"));
                            return;
                        }

                        if (this.ucOrder1.Add() == 0)
                            this.initButton(true);
                    }
                }
            }
            else if (e.ClickedItem == this.tbCheck)
            {
                this.ucOrder1.AddTest();
            }
            else if (e.ClickedItem == this.tbRefresh)
            {
                //刷新
                this.tvDoctorPatientList1.RefreshInfo();
            }
            else if (e.ClickedItem == this.tbGroup)
            {
                if (this.tbGroup.CheckState == CheckState.Unchecked)
                {
                    this.tbGroup.CheckState = CheckState.Checked;
                    this.ucOrder1.SetEditGroup(true);
                    this.ucOrder1.SetPatient(null);
                    this.InitButtonGroup(true);
                }
                else
                {
                    this.tbGroup.CheckState = CheckState.Unchecked;
                    this.ucOrder1.SetEditGroup(false);
                    this.InitButtonGroup(false);


                    if (this.tvDoctorPatientList1.SelectedNode != null &&
                        this.tvDoctorPatientList1.SelectedNode.Tag != null)
                    {
                        this.ucOrder1.Query(this.tvDoctorPatientList1.SelectedNode, this.tvDoctorPatientList1.SelectedNode.Tag);
                    }
                }
            }
            else if (e.ClickedItem == this.tbOperation)
            {
                Neusoft.HISFC.Models.RADT.PatientInfo pi = (Neusoft.HISFC.Models.RADT.PatientInfo)this.tvDoctorPatientList1.SelectedNode.Tag;
                frmOperation frmOpt = new frmOperation(pi);
                frmOpt.ShowDialog();
            }
            else if (e.ClickedItem == this.tbAssayCure)
            {
                this.ucOrder1.AddAssayCure();
            }
            else if (e.ClickedItem == this.tbDelOrder)
            {
                this.ucOrder1.Delete();
            }
            else if (e.ClickedItem == this.tbQueryOrder)
            {
                try
                {
                    this.ucOrder1.Query(this.tvDoctorPatientList1.SelectedNode, this.tvDoctorPatientList1.SelectedNode.Tag);
                }
                catch { }
            }
            else if (e.ClickedItem == this.tbPrintOrder)
            {
                this.ucOrder1.PrintOrder();
            }
            else if (e.ClickedItem == this.tbComboOrder)
            {
                this.ucOrder1.ComboOrder();
            }
            else if (e.ClickedItem == this.tbCancelOrder)
            {
                this.ucOrder1.CancelCombo();
            }
            else if (e.ClickedItem == this.tbExitOrder)
            {
                if (this.isEditGroup)
                {
                    if (this.tbGroup.CheckState == CheckState.Checked)
                    {
                        this.tbGroup.CheckState = CheckState.Unchecked;
                    }
                    else
                    {
                        this.tbGroup.CheckState = CheckState.Checked;
                    }
                    this.ucOrder1.SetEditGroup(false);
                }
                else
                {
                    if (this.ucOrder1.ExitOrder() == 0)
                        this.initButton(false);

                    //避免失去患者焦点，此处不再刷新列表，如果想刷新，点击刷新按钮
                    //tvDoctorPatientList1.RefreshInfo();
                }
            }
            else if (e.ClickedItem == this.tbInValid)
            {
                this.ucOrder1.Filter(Neusoft.HISFC.Components.Order.Controls.EnumFilterList.Invalid);
            }
            else if (e.ClickedItem == this.tbValid)
            {
                this.ucOrder1.Filter(Neusoft.HISFC.Components.Order.Controls.EnumFilterList.Valid);
            }
            else if (e.ClickedItem == this.tbAll)
            {
                this.ucOrder1.Filter(Neusoft.HISFC.Components.Order.Controls.EnumFilterList.All);
            }
            else if (e.ClickedItem == this.tbToday)
            {
                this.ucOrder1.Filter(Neusoft.HISFC.Components.Order.Controls.EnumFilterList.Today);
            }
            else if (e.ClickedItem == this.tbNew)
            {
                this.ucOrder1.Filter(Neusoft.HISFC.Components.Order.Controls.EnumFilterList.New);
            }
            else if (e.ClickedItem == this.tbSaveOrder)
            {
                //
                if (isEditGroup)
                {
                    SaveGroup();
                }
                else
                {
                    if (this.ucOrder1.Save() == -1)
                    {
                    }
                    else
                    {
                        this.initButton(false);
                    }
                }
            }
            else if (e.ClickedItem == this.tsbHerbal)
            {
                this.ucOrder1.HerbalOrder();
            }
            else if (e.ClickedItem == this.tbLevelOrder)
            {
                this.ucOrder1.AddLevelOrders();
            }
            else if (e.ClickedItem == this.tbChooseDoct)//{D5517722-7128-4d0c-BBC4-1A5558A39A03}
            {
                this.ucOrder1.ChooseDoctor();
            }
            ///增加医嘱重整按钮
            else if (e.ClickedItem == this.tbRetidyOrder)
            {
                if (this.ucOrder1.IsDesignMode == false)
                {
                    this.ucOrder1.ReTidyOrder();
                }
                else
                {
                    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("非开立状态下才允许进行医嘱重整"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (e.ClickedItem == this.tbDiseaseReport)
            {
                if (this.dcpInstance == null)
                {
                    this.dcpInstance = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.DCP.IDCP)) as Neusoft.HISFC.BizProcess.Interface.DCP.IDCP;
                }

                if (this.dcpInstance != null)
                {
                    Neusoft.HISFC.Models.RADT.PatientInfo patientInfo = this.tvDoctorPatientList1.SelectedNode.Tag as Neusoft.HISFC.Models.RADT.PatientInfo;

                    this.dcpInstance.RegisterDiseaseReport(this, patientInfo, Neusoft.HISFC.Models.Base.ServiceTypes.I);
                }
            }
            else if (e.ClickedItem == this.tbLisResultPrint)
            {
                this.ucOrder1.ShowLisResult();
            }
            else if (e.ClickedItem == this.tbPacsResultPrint)
            {
                patient = this.tvDoctorPatientList1.SelectedNode.Tag as Neusoft.HISFC.Models.RADT.PatientInfo;

                if (this.patient == null || this.patient.PID.ID == "" || this.patient.PID.ID == null)
                {
                    MessageBox.Show("请选择一个患者！");
                    return;

                }

                try
                {
                    string patientNo = patient.ID;
                    this.ucOrder1.ShowPacsResultByPatient(patientNo);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            //停止全部长期医嘱 houwb 2011-3-11 {46E8908F-4248-4a40-89B1-530CA5796CD4}
            else if (e.ClickedItem == this.tbDcAllLongOrder)
            {
                this.ucOrder1.DcAllLongOrder();
            }
            else if (e.ClickedItem == this.tbEMR)
            {
                if (this.patient == null)
                {
                    MessageBox.Show("请选择具体的患者！");
                    return;
                }
                Process[] processes = System.Diagnostics.Process.GetProcesses();
                foreach (Process p in processes)
                {
                    if (p.ProcessName == "Bridge")
                    {
                        p.Kill();
                    }
                }

                this.StatrEmr("电子病历", "ShowDialog", "HIS45", "UFC.EPR", "UFC.EPR.Query.ucEPRMain", "", this.patient.ID);
            }
        }

        private void SaveGroup()
        {
            Neusoft.HISFC.Components.Common.Forms.frmOrderGroupManager group = new Neusoft.HISFC.Components.Common.Forms.frmOrderGroupManager();

            try
            {
                group.IsManager = (Neusoft.FrameWork.Management.Connection.Operator as Neusoft.HISFC.Models.Base.Employee).IsManager;
            }
            catch
            { }

            ArrayList al = new ArrayList();
            #region 长期临时一起保存组套{11F97F55-F747-4ad9-A74F-086635D5EBD9}
            for (int i = 0; i < this.ucOrder1.fpOrder.Sheets[0].Rows.Count; i++)//长期医嘱
            {
                //{F4CA5CB3-0C23-4e0e-978D-5B72711A6C86}
                Neusoft.HISFC.Models.Order.Inpatient.Order longorderTemp = this.ucOrder1.GetObjectFromFarPoint(i, 0);
                if (longorderTemp == null)
                {
                    continue;
                }

                //Neusoft.HISFC.Models.Order.Inpatient.Order longorder = this.ucOrder1.GetObjectFromFarPoint(i, 0).Clone();
                Neusoft.HISFC.Models.Order.Inpatient.Order longorder = longorderTemp.Clone();
                if (longorder == null)
                {
                    MessageBox.Show("获得医嘱出错！");
                }
                else
                {
                    string s = longorder.Item.Name;
                    string sno = longorder.Combo.ID;
                    //保存医嘱组套 默认开立时间为 零点
                    longorder.BeginTime = new DateTime(longorder.BeginTime.Year, longorder.BeginTime.Month, longorder.BeginTime.Day, 0, 0, 0);
                    al.Add(longorder);
                }
            }
            for (int i = 0; i < this.ucOrder1.fpOrder.Sheets[1].Rows.Count; i++)//临时医嘱
            {
                //{F4CA5CB3-0C23-4e0e-978D-5B72711A6C86}
                Neusoft.HISFC.Models.Order.Inpatient.Order shortorderTemp = this.ucOrder1.GetObjectFromFarPoint(i, 1);
                if (shortorderTemp == null)
                {
                    continue;
                }
                //Neusoft.HISFC.Models.Order.Inpatient.Order shortorder = this.ucOrder1.GetObjectFromFarPoint(i, 1).Clone();
                Neusoft.HISFC.Models.Order.Inpatient.Order shortorder = shortorderTemp.Clone();
                if (shortorder == null)
                {
                    MessageBox.Show("获得医嘱出错！");
                }
                else
                {
                    string s = shortorder.Item.Name;
                    string sno = shortorder.Combo.ID;
                    //保存医嘱组套 默认开立时间为 零点
                    shortorder.BeginTime = new DateTime(shortorder.BeginTime.Year, shortorder.BeginTime.Month, shortorder.BeginTime.Day, 0, 0, 0);
                    al.Add(shortorder);
                }
            }
            #endregion
            if (al.Count > 0)
            {
                group.alItems = al;
                group.ShowDialog();
                this.tvGroup.RefrshGroup();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab.Controls.Count > 0)
            {
                this.iQueryControlable = this.tabControl1.SelectedTab.Controls[0] as Neusoft.FrameWork.WinForms.Forms.IQueryControlable;
                this.iControlable = this.tabControl1.SelectedTab.Controls[0] as Neusoft.FrameWork.WinForms.Forms.IControlable;
                this.CurrentControl = this.tabControl1.SelectedTab.Controls[0];
            }
        }

        private void ucQueryInpatientNo1_myEvent()
        {
            if (string.IsNullOrEmpty(this.ucQueryInpatientNo1.InpatientNo))
            {
                return;
            }
            #region {640B03D9-8D8D-4720-A05A-42691EAADD0B} 开立状态不能查询患者
            if (this.ucOrder1.IsDesignMode)
            {
                MessageBox.Show("开立状态不能查询患者");
                return;
            }
            #endregion
            this.tvDoctorPatientList1.QueryPaitent(this.ucQueryInpatientNo1.InpatientNo, this.consultation.Operator as Neusoft.HISFC.Models.Base.Employee);
        }

        private void tvDoctorPatientList1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.tvDoctorPatientList1.SelectedNode.Tag != null
               && this.tvDoctorPatientList1.SelectedNode.Tag.ToString() != ""
               && this.tvDoctorPatientList1.SelectedNode.Parent != null)
            {
                if (this.tvDoctorPatientList1.SelectedNode.Parent.Tag != null
                    && this.tvDoctorPatientList1.SelectedNode.Parent.Tag.ToString() != "")
                {
                    if (this.tvDoctorPatientList1.SelectedNode.Parent.Tag.ToString() == "QueryPatient")
                    {
                        if (this.CurrentControl.GetType().FullName == "Neusoft.HISFC.Components.Order.Controls.ucOrder")
                        {
                            this.tbAddOrder.Enabled = false;
                        }
                        //else if (this.CurrentControl.GetType().FullName == "Neusoft.HISFC.Components.Order.Controls.ucOrderPrint")
                        //{

                        //}
                        //else
                        //{
                        //    base.tree.SelectedNode = this.tvDoctorPatientList1.SelectedNode.Parent;
                        //}
                    }
                    else
                    {
                        if (this.CurrentControl.GetType().FullName == "Neusoft.HISFC.Components.Order.Controls.ucOrder")
                        {
                            this.tbAddOrder.Enabled = true;

                            patient = this.tvDoctorPatientList1.SelectedNode.Tag as Neusoft.HISFC.Models.RADT.PatientInfo;
                            string bedNO = patient.PVisit.PatientLocation.Bed.ID;
                            if (bedNO.Length > 4)
                            {
                                bedNO = bedNO.Substring(4);
                            }
                            this.Text = "您正在操作的患者为 住院号：" + patient.PID.PatientNO + " 姓名：" + patient.Name + " 性别：" + patient.Sex.Name + " 年龄：" + consultation.GetAge(patient.Birthday) + " 床号:" + bedNO;
                        }
                    }
                }
            }
            else
            {
                this.Text = "";
            }
        }

        #region IInterfaceContainer 成员    {E53A21A7-2B74-4b48-A9F4-9E05F8FA11A2}

        public Type[] InterfaceTypes
        {
            get
            {
                return new Type[] { 
                    typeof(Neusoft.HISFC.BizProcess.Interface.DCP.IDCP) ,
                typeof(Neusoft.HISFC.BizProcess.Interface.Common.IPacs)};
            }
        }

        #endregion

        private void ucRealOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.ucOrder1.RelePacsInterface();
            this.ucOrder1.QuitPass();
        }
    }
}