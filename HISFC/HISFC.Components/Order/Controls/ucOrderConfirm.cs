using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FarPoint.Win.Spread;
using FarPoint.Win;
namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// [功能描述: 医嘱审核]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2004-10-12]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucOrderConfirm : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucOrderConfirm()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 长期医嘱数量
        /// </summary>
        private int LongOrderCount = 0;

        /// <summary>
        /// 临时医嘱数量
        /// </summary>
        private int ShortOrderCount = 0;

        /// <summary>
        /// 配置文件
        /// </summary>
        string strFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath +
            FS.FrameWork.WinForms.Classes.Function.SettingPath + "fpOrderConfirm.xml";

        //DataTable dtMain;
        //DataSet myDataSet;

        /// <summary>
        /// 长嘱明细
        /// </summary>
        DataTable dtChild1;

        /// <summary>
        /// 临嘱明细
        /// </summary>
        DataTable dtChild2;

        /// <summary>
        /// 当前医嘱
        /// </summary>
        FS.FrameWork.Models.NeuObject OrderId = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 组合号
        /// </summary>
        FS.FrameWork.Models.NeuObject ComboNo = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 当前选中的单元格
        /// </summary>
        protected FarPoint.Win.Spread.Cell CurrentCellName;

        /// <summary>
        /// 选中患者的住院流水号
        /// </summary>
        string PatientId = "";

        /// <summary>
        /// 特殊医嘱类型
        /// </summary>
        string speOrderType = "";

        /// <summary>
        /// 所有在用科室
        /// </summary>
        Hashtable hsDepts = new Hashtable();

        /// <summary>
        /// 患者信息列表
        /// </summary>
        protected ArrayList alpatientinfos;

        /// <summary>
        /// IOP接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.IHE.IOP iop = null;

        /// <summary>
        /// 当前登陆科室
        /// </summary>
        private FS.HISFC.Models.Base.Department currentDept = null;

        private FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();

        #endregion

        #region 属性

        /// <summary>
        /// 是否判断欠费，欠费是否提示
        /// </summary>
        private FS.HISFC.Models.Base.MessType messType = FS.HISFC.Models.Base.MessType.Y;

        /// <summary>
        /// 是否判断欠费，欠费是否提示
        /// </summary>
        [Category("审核设置"), Description("Y：判断欠费,不允许继续收费,M：判断欠费，提示是否继续收费,N：不判断欠费")]
        public FS.HISFC.Models.Base.MessType MessageType
        {
            set
            {
                messType = value;
            }
            get
            {
                return messType;
            }
        }

        /// <summary>
        /// 当前医嘱类型
        /// </summary>
        protected FS.HISFC.Models.Order.EnumType myShowOrderType = FS.HISFC.Models.Order.EnumType.LONG;

        /// <summary>
        /// 显示医嘱类型
        /// </summary>
        public FS.HISFC.Models.Order.EnumType ShowOrderType
        {
            get
            {
                return this.myShowOrderType;
            }
            set
            {
                this.myShowOrderType = value;
                if (this.myShowOrderType == FS.HISFC.Models.Order.EnumType.LONG)
                {
                    this.fpSpread.ActiveSheetIndex = 0;
                }
                else
                {
                    this.fpSpread.ActiveSheetIndex = 1;
                }
            }
        }

        /// <summary>
        /// 操作员变量
        /// </summary>
        protected FS.HISFC.Models.Base.Employee myOperator;

        /// <summary>
        /// 当前操作员
        /// </summary>
        protected FS.HISFC.Models.Base.Employee Operator
        {
            get
            {
                if (myOperator == null)
                    myOperator = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                return myOperator;
            }
        }

        /// <summary>
        /// 设置护士站看到医嘱的类型,逗号隔开
        /// </summary>
        [Category("审核设置"), Description("设置护士站看到医嘱的类型,逗号隔开 会诊:CONS 科室:DEPTXXX 医技:DEPTXXX 其他:OTHER")]
        public string SpeOrderType
        {
            set
            {
                this.speOrderType = value;
            }
            get
            {
                return this.speOrderType;
            }
        }

        /// <summary>
        /// 退费申请是否自动确认
        /// </summary>
        private bool autoQuitFeeApply = false;

        /// <summary>
        /// 退费申请是否自动确认
        /// </summary>
        [Category("审核设置"), Description("是否直接退费，对于非本科室收费项目，只能是申请")]
        public bool AutoQuitFeeApply
        {
            set
            {
                this.autoQuitFeeApply = value;
            }
            get
            {
                return this.autoQuitFeeApply;
            }
        }

        /// <summary>
        /// 停止医嘱退费时的默认退费数量
        /// 1 停止时间后的收费数量；0 默认退0 
        /// </summary>
        private int defaultQuitQty = 1;

        /// <summary>
        /// 停止医嘱退费时的默认退费数量
        /// 0 默认退0； 1 停止时间后的收费数量；
        /// </summary>
        [Category("审核设置"), Description("停止医嘱退费时的默认退费数量：0 默认退0； 1 停止时间后的收费数量")]
        public int DefaultQuitQty
        {
            get
            {
                return defaultQuitQty;
            }
            set
            {
                defaultQuitQty = value;
            }
        }

        /// <summary>
        /// 欠费患者审核保存时是否同时计费
        /// </summary>
        private bool lackDealModel = true;

        /// <summary>
        /// 欠费患者审核保存时是否同时计费
        /// </summary>
        [Category("审核设置"), Description("欠费患者审核保存时是否同时计费？")]
        public bool LackDealModel
        {
            get
            {
                return lackDealModel;
            }
            set
            {
                lackDealModel = value;
            }
        }



        /// <summary>
        /// 停止医嘱是否自动退口服药
        /// </summary>
        private bool isQuitPOQty = false;

        /// <summary>
        /// 停止医嘱是否自动退口服药
        /// </summary>
        [Category("审核设置"), Description("停止医嘱是否自动退口服药？")]
        public bool IsQuitPOQty
        {
            get
            {
                return isQuitPOQty;
            }
            set
            {
                isQuitPOQty = value;
            }
        }

        /// <summary>
        /// 出院带药是否判断警戒线
        /// </summary>
        private bool iSCDCharge = false;
        /// <summary>
        /// 出院带药是否判断警戒线
        /// </summary>
        [Category("审核设置"), Description("出院带药是否判断警戒线？")]
        public bool ISCDCharge
        {
            get
            {
                return iSCDCharge;
            }
            set
            {
                iSCDCharge = value;
            }
        }

        #endregion

        #region 方法

        #region 初始化

        /// <summary>
        /// 初始化各个控件
        /// </summary>
        private void InitControl()
        {
            this.InitFp();
            ucSubtblManager1 = new ucSubtblManager();
            this.ucSubtblManager1.IsVerticalShow = true;
            this.ucSubtblManager1.SetRightShow();
            this.DockingManager();
            this.ucSubtblManager1.ShowSubtblFlag += new ucSubtblManager.ShowSubtblFlagEvent(ucSubtblManager1_ShowSubtblFlag);

            #region 预停止医嘱的医嘱状态用紫色表示
            //addby xuewj 2010-11-3 {344D145C-A30A-4ad1-86ED-CBCF80C751FA}

            base.OnStatusBarInfo(null, "(绿色：新开)(蓝色：审核)(黄色：执行)(红色：作废)(紫色：预停止)");

            ArrayList alDeptALL = this.deptManager.GetDeptmentAll();

            foreach (FS.HISFC.Models.Base.Department dept in alDeptALL)
            {
                if (!this.hsDepts.Contains(dept.ID))
                {
                    hsDepts.Add(dept.ID, dept);
                }
            }

            #endregion
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            tv = sender as TreeView;
            if (tv != null && tv.CheckBoxes == false)
                tv.CheckBoxes = true;
            this.InitControl();

            return null;
        }

        /// <summary>
        /// 初始化fpTreeView1
        /// </summary>
        private void InitFp()
        {
            this.fpSpread.ChildViewCreated += new FarPoint.Win.Spread.ChildViewCreatedEventHandler(fpSpread_ChildViewCreated);

            this.fpSpread.Sheets[0].SheetName = "长期医嘱";
            this.fpSpread.Sheets[1].SheetName = "临时医嘱";
            this.fpSpread.Sheets[0].Columns[0].Visible = false;
            this.fpSpread.Sheets[0].Columns[1].Label = "审核［长期］";
            this.fpSpread.Sheets[0].Columns[2].Label = "患者姓名";
            this.fpSpread.Sheets[0].Columns[3].Label = "床号";
            //{4ED608A1-4AAA-433a-B0F8-ABE8EA029E1C}添加 性别，年龄栏
            this.fpSpread.Sheets[0].Columns[4].Label = "性别";
            this.fpSpread.Sheets[0].Columns[5].Label = "年龄";
            this.fpSpread.Sheets[0].Columns[6].Label = "主管医生";
            this.fpSpread.Sheets[0].Columns[7].Label = "温馨提示：请继续分解长期医嘱。";
            this.fpSpread.Sheets[0].ColumnHeader.Columns[7].ForeColor = Color.Red;
            this.fpSpread.Sheets[0].RowCount = 0;
            this.fpSpread.Sheets[0].ColumnCount = 8;
            this.fpSpread.Sheets[0].Columns[1].Width = 100;
            this.fpSpread.Sheets[0].Columns[2].Width = 100;
            this.fpSpread.Sheets[0].Columns[6].Width = 100;
            this.fpSpread.Sheets[0].Columns[7].Width = 200;
            this.fpSpread.Sheets[0].GrayAreaBackColor = Color.WhiteSmoke;

            this.fpSpread.Sheets[1].Columns[0].Visible = false;
            this.fpSpread.Sheets[1].Columns[1].Label = "审核［临时］";
            this.fpSpread.Sheets[1].Columns[2].Label = "患者姓名";
            this.fpSpread.Sheets[1].Columns[3].Label = "床号";
            //{4ED608A1-4AAA-433a-B0F8-ABE8EA029E1C}
            this.fpSpread.Sheets[1].Columns[4].Label = "性别";
            this.fpSpread.Sheets[1].Columns[5].Label = "年龄";
            this.fpSpread.Sheets[1].Columns[6].Label = "主管医生";
            this.fpSpread.Sheets[1].RowCount = 0;
            this.fpSpread.Sheets[1].ColumnCount = 7;
            this.fpSpread.Sheets[1].GrayAreaBackColor = Color.WhiteSmoke;
            this.fpSpread.Sheets[1].Columns[1].Width = 100;
            this.fpSpread.Sheets[1].Columns[2].Width = 100;
            this.fpSpread.Sheets[1].Columns[6].Width = 100;

            this.fpSpread.Sheets[0].DataAutoSizeColumns = false;
            this.fpSpread.Sheets[1].DataAutoSizeColumns = false;

            this.fpSpread.Sheets[0].Rows.Get(-1).BackColor = Color.LightSkyBlue;
            this.fpSpread.Sheets[1].Rows.Get(-1).BackColor = Color.LightSkyBlue;

            //this.fpSpread.Sheets[0].CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(ucOrderConfirm_CellChanged);
            //this.fpSpread.Sheets[1].CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(ucOrderConfirm_CellChanged);
            this.fpSpread.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread_CellClick);
            this.fpSpread.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread_CellDoubleClick);
        }

        #endregion

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (tv != null && this.tv.CheckBoxes == false)
                tv.CheckBoxes = true;
            return base.OnSetValue(neuObject, e);
        }

        /// <summary>
        /// 患者信息
        /// </summary>
        /// <param name="alValues"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValues(ArrayList alValues, object e)
        {
            this.alpatientinfos = alValues;

            this.QueryOrder();

            #region {839D3A8A-49FA-4d47-A022-6196EB1A5715}
            if (this.tv != null && this.tv.CheckBoxes)
            {
                foreach (TreeNode parentNode in tv.Nodes)
                {
                    SetTree(parentNode);
                }
            }
            #endregion
            return 0;
        }
        public override void Refresh()
        {
            this.fpSpread.StopCellEditing();
            FS.HISFC.Models.Base.Employee empl = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

            string strInpatientNo = ""; //当前病历号
            string strName = "";//当前项目名

            List<string> OrderIDs = new List<string>();

            #region 长期医嘱

            for (int i = 0; i < this.fpSpread.Sheets[0].Rows.Count; i++) //长期医嘱
            {
                #region 医嘱处理
                strInpatientNo = this.fpSpread.Sheets[0].Cells[i, 0].Text;//当前的患者
                strName = this.fpSpread.Sheets[0].Cells[i, 2].Text;//当前的患者

                //当前患者的医嘱列表页 sv
                FarPoint.Win.Spread.SheetView sv = this.fpSpread.Sheets[0].GetChildView(i, 0);

                if (sv != null)
                {
                    for (int j = 0; j < sv.Rows.Count; j++)//医嘱项目
                    {
                        if (sv.Cells[j, (int)EnumOrderColumns.CheckFlag].Text.ToUpper() == "TRUE")
                        {
                            string orderid = sv.Cells[j, int.Parse(OrderId.ID)].Text;//医嘱项目处理
                            if (!string.IsNullOrEmpty(orderid))
                            {
                                OrderIDs.Add(orderid);
                            }
                        }
                    }
                }
                #endregion
            }
            
            #endregion

            #region 临时医嘱

            for (int i = 0; i < this.fpSpread.Sheets[1].Rows.Count; i++) //临时医嘱
            {
                #region 医嘱处理
                strInpatientNo = this.fpSpread.Sheets[1].Cells[i, 0].Text;//当前的患者
                strName = this.fpSpread.Sheets[1].Cells[i, 2].Text;//当前的患者

                //当前患者的医嘱列表页 sv
                FarPoint.Win.Spread.SheetView sv = this.fpSpread.Sheets[1].GetChildView(i, 0);

                if (sv != null)
                {
                    for (int j = 0; j < sv.Rows.Count; j++)//医嘱项目
                    {
                        if (sv.Cells[j, (int)EnumOrderColumns.CheckFlag].Text.ToUpper() == "TRUE")
                        {
                            string orderid = sv.Cells[j, int.Parse(OrderId.ID)].Text;//医嘱项目处理
                            if (!string.IsNullOrEmpty(orderid))
                            {
                                OrderIDs.Add(orderid);
                            }
                        }

                    }
                }
                #endregion
            }

            #endregion

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            if (CacheManager.InOrderMgr.StopOrder(OrderIDs) == -1)
                FS.FrameWork.Management.PublicTrans.RollBack();
            else
                FS.FrameWork.Management.PublicTrans.Commit();

            base.Refresh();
        }
        private int SetTree(TreeNode parentNode)
        {
            foreach (TreeNode node in parentNode.Nodes)
            {
                if (node.Checked)
                {
                    if (node.Tag != null)
                    {
                        if (node.Tag is FS.HISFC.Models.RADT.PatientInfo)
                        {
                            FS.HISFC.Models.RADT.PatientInfo patientInfo = node.Tag as FS.HISFC.Models.RADT.PatientInfo;
                            if (node.Checked)
                            {
                                switch (patientInfo.Sex.ID.ToString())
                                {
                                    case "F":
                                        //男
                                        if (patientInfo.ID.IndexOf("B") > 0)
                                            node.ImageIndex = 10;	//婴儿女
                                        else
                                            node.ImageIndex = 6;	//成年女
                                        break;
                                    case "M":
                                        if (patientInfo.ID.IndexOf("B") > 0)
                                            node.ImageIndex = 8;	//婴儿男
                                        else
                                            node.ImageIndex = 4;	//成年男
                                        break;
                                    default:
                                        node.ImageIndex = 4;
                                        break;
                                }
                                FS.HISFC.Components.Common.Classes.Function.DelLabel((node.Tag as FS.HISFC.Models.RADT.PatientInfo).ID);
                            }
                        }
                    }
                }
                if (node.Nodes.Count > 0)
                {
                    SetTree(node);
                }
            }

            return 1;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            if (FS.FrameWork.WinForms.Classes.Function.Msg("是否确定要保存?", 422) == DialogResult.No)
            {
                return -1;
            }
            this.fpSpread.StopCellEditing();

            //FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //this.CacheManager.OrderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //CacheManager.OrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //this.radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            FS.HISFC.Models.Base.Employee empl = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

            FS.HISFC.Models.RADT.PatientInfo patientInfo = null;
            string errInfo = "";

            string strInpatientNo = ""; //当前病历号
            string strName = "";//当前项目名

            string strComboNo = "";

            string lackFeeInfo = "";

            string stockDeptId = string.Empty;

            ///是否欠费
            bool isLackFee = false;

            #region 长期医嘱

            for (int i = 0; i < this.fpSpread.Sheets[0].Rows.Count; i++) //长期医嘱
            {
                #region 医嘱处理
                strInpatientNo = this.fpSpread.Sheets[0].Cells[i, 0].Text;//当前的患者
                strName = this.fpSpread.Sheets[0].Cells[i, 2].Text;//当前的患者
                strComboNo = "";

                //当前患者的医嘱列表页 sv
                FarPoint.Win.Spread.SheetView sv = this.fpSpread.Sheets[0].GetChildView(i, 0);
                ArrayList alOrders = new ArrayList();

                if (Classes.Function.CheckPatientState(strInpatientNo, ref patientInfo, ref errInfo) == -1)
                {
                    //FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show(errInfo);
                    return -1;
                }

                isLackFee = false;
                //欠费提示
                if (CacheManager.FeeIntegrate.IsPatientLackFee(patientInfo))
                {
                    if (!lackFeeInfo.Contains(patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "床  " + patientInfo.Name))
                    {
                        lackFeeInfo += "\r\n" + patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "床  " + patientInfo.Name;
                    }

                    if (!lackDealModel)
                    {
                        isLackFee = true;
                    }
                }

                if (sv != null)
                {
                    for (int j = 0; j < sv.Rows.Count; j++)//医嘱项目
                    {
                        if (sv.Cells[j, (int)EnumOrderColumns.CheckFlag].Text.ToUpper() == "TRUE")
                        {
                            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在处理长期医嘱...");
                            Application.DoEvents();
                            string orderid = sv.Cells[j, int.Parse(OrderId.ID)].Text;//医嘱项目处理
                            FS.HISFC.Models.Order.Inpatient.Order order = CacheManager.InOrderMgr.QueryOneOrder(orderid);
                            if (order == null)
                            {
                                //CacheManager.OrderIntegrate.fee.Rollback();
                                //FS.FrameWork.Management.PublicTrans.RollBack();
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                if (MessageBox.Show("床号[" + patientInfo.PVisit.PatientLocation.Bed.ID.Substring(3) + "] 患者[" + patientInfo.Name + "]" + "\n医嘱已经发生变化，请刷新后再次审核！\n是否继续保存其他患者？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    return -1;
                                }
                                else
                                {
                                    //FS.FrameWork.Management.PublicTrans.BeginTransaction();
                                    //this.CacheManager.OrderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                    //CacheManager.OrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                    //this.radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                }
                            }

                            //此处存可退费数量
                            if (sv.Cells[j, (int)EnumOrderColumns.MoOrderID].Tag != null)
                            {
                                order.Item.User03 = sv.Cells[j, (int)EnumOrderColumns.MoOrderID].Tag.ToString();
                            }
                            order.Patient.Name = strName;
                            alOrders.Add(order);
                        }
                    }

                    //if (Classes.Function.CheckMoneyAlert(patientInfo, alOrders, messType) == -1)
                    //{
                    //    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    //    return -1;
                    //}

                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    if (CacheManager.OrderIntegrate.SaveChecked(patientInfo, alOrders, true, empl.Nurse.ID, this.autoQuitFeeApply, !isLackFee, iSCDCharge) == -1)
                    {
                        //CacheManager.OrderIntegrate.fee.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack(); ;
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        if (MessageBox.Show("床号[" + patientInfo.PVisit.PatientLocation.Bed.ID.Substring(3) + "] 患者[" + patientInfo.Name + "]\n" + CacheManager.OrderIntegrate.Err + "\n是否继续保存其他患者？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            CacheManager.OrderIntegrate.Err = "";
                            return -1;
                        }
                        else
                        {
                            CacheManager.OrderIntegrate.Err = "";
                            //FS.FrameWork.Management.PublicTrans.BeginTransaction();
                            //this.CacheManager.OrderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                            //CacheManager.OrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                            //this.radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        }
                    }
                    else
                    {
                        //CacheManager.OrderIntegrate.fee.Commit();
                        FS.FrameWork.Management.PublicTrans.Commit();
                        //FS.FrameWork.Management.PublicTrans.BeginTransaction();
                        //this.CacheManager.OrderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        //CacheManager.OrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        //this.radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                        //用于自动退费时，无法直接退费项目的提示信息
                        if (!string.IsNullOrEmpty(CacheManager.OrderIntegrate.Err))
                        {
                            MessageBox.Show(CacheManager.OrderIntegrate.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            CacheManager.OrderIntegrate.Err = "";
                        }
                    }
                }
                #endregion
            }

            #endregion

            #region 临时医嘱

            for (int i = 0; i < this.fpSpread.Sheets[1].Rows.Count; i++) //临时医嘱
            {
                #region 医嘱处理
                strInpatientNo = this.fpSpread.Sheets[1].Cells[i, 0].Text;//当前的患者
                strName = this.fpSpread.Sheets[1].Cells[i, 2].Text;//当前的患者
                strComboNo = "";

                //当前患者的医嘱列表页 sv
                FarPoint.Win.Spread.SheetView sv = this.fpSpread.Sheets[1].GetChildView(i, 0);
                ArrayList alOrders = new ArrayList();
                if (Classes.Function.CheckPatientState(strInpatientNo, ref patientInfo, ref errInfo) == -1)
                {
                    //FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show(errInfo);
                    return -1;
                }

                isLackFee = false;
                //欠费提示
                if (CacheManager.FeeIntegrate.IsPatientLackFee(patientInfo))
                {
                    if (!lackFeeInfo.Contains(patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "床  " + patientInfo.Name))
                    {
                        lackFeeInfo += "\r\n" + patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "床  " + patientInfo.Name;
                    } 
                    
                    if (!lackDealModel)
                    {
                        isLackFee = true;
                    }
                }

                if (sv != null)
                {
                    for (int j = 0; j < sv.Rows.Count; j++)//医嘱项目
                    {
                        if (sv.Cells[j, (int)EnumOrderColumns.CheckFlag].Text.ToUpper() == "TRUE")
                        {
                            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在处理临时医嘱...");
                            Application.DoEvents();
                            string orderid = sv.Cells[j, int.Parse(OrderId.ID)].Text;//医嘱项目处理
                            FS.HISFC.Models.Order.Inpatient.Order order = CacheManager.InOrderMgr.QueryOneOrder(orderid);
                            if (order == null)
                            {
                                //CacheManager.OrderIntegrate.fee.Rollback();
                                //FS.FrameWork.Management.PublicTrans.RollBack();
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                if (MessageBox.Show("床号[" + patientInfo.PVisit.PatientLocation.Bed.ID.Substring(3) + "] 患者[" + patientInfo.Name + "]\n" + "医嘱已经发生变化，请刷新后再次审核！\n是否继续保存其他患者？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    return -1;
                                }
                                else
                                {
                                    //FS.FrameWork.Management.PublicTrans.BeginTransaction();
                                    //this.CacheManager.OrderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans); 
                                    //CacheManager.OrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                    //this.radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                }
                            }

                            //此处存可退费数量
                            if (sv.Cells[j, (int)EnumOrderColumns.MoOrderID].Tag != null)
                            {
                                order.Item.User03 = sv.Cells[j, (int)EnumOrderColumns.MoOrderID].Tag.ToString();
                            }
                            order.Patient.Name = strName;

                            if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                            {
                                stockDeptId = order.StockDept.ID;
                            }

                            alOrders.Add(order);
                        }

                    }

                    if (Classes.Function.CheckMoneyAlert(patientInfo, alOrders, messType) == -1)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        return -1;
                    }

                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    CacheManager.OrderIntegrate.MessageType = messType;
                    if (CacheManager.OrderIntegrate.SaveChecked(patientInfo, alOrders, false, empl.Nurse.ID, this.autoQuitFeeApply, !isLackFee, iSCDCharge) == -1)
                    {
                        //CacheManager.OrderIntegrate.fee.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack(); ;
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                        if (MessageBox.Show("床号[" + patientInfo.PVisit.PatientLocation.Bed.ID.Substring(3) + "] 患者[" + patientInfo.Name + "]\n" + CacheManager.OrderIntegrate.Err + "\n是否继续保存其他患者？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            CacheManager.OrderIntegrate.Err = "";
                            return -1;
                        }
                        else
                        {
                            CacheManager.OrderIntegrate.Err = "";
                            //FS.FrameWork.Management.PublicTrans.BeginTransaction();
                            //this.CacheManager.OrderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                            //CacheManager.OrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                            //this.radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        }
                    }
                    else
                    {
                        //CacheManager.OrderIntegrate.fee.Commit();
                        FS.FrameWork.Management.PublicTrans.Commit();
                        //FS.FrameWork.Management.Transaction 
                        //FS.FrameWork.Management.PublicTrans.BeginTransaction();
                        //this.CacheManager.OrderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        //CacheManager.OrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        //this.radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                        //用于自动退费时，无法直接退费项目的提示信息
                        if (!string.IsNullOrEmpty(CacheManager.OrderIntegrate.Err))
                        {
                            MessageBox.Show(CacheManager.OrderIntegrate.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            CacheManager.OrderIntegrate.Err = "";
                        }
                    }

                    #region addby xuewj 2010-03-12 HL7消息 send：op---receiver：of

                    if (this.iop == null)
                    {
                        this.iop = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IHE.IOP)) as FS.HISFC.BizProcess.Interface.IHE.IOP;
                    }
                    if (this.iop != null)
                    {
                        this.iop.PlaceOrder(alOrders);
                    }

                    #endregion

                }
                #endregion
            }

            #endregion

            /// CacheManager.OrderIntegrate.fee.Commit();
            //FS.FrameWork.Management.PublicTrans.Commit();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            this.QueryOrder();

            string showTip = "还存在未审核医嘱：\r\n\r\n";
            bool show = false;

            if (fpSpread.Sheets[0].Rows.Count > 0) //长期医嘱
            {
                showTip += "长期医嘱\r\n\r\n";
                show = true;
            }

            if (fpSpread.Sheets[1].Rows.Count > 0) //长期医嘱
            {
                showTip += "临时医嘱\r\n\r\n";
                show = true;
            }
            if (show)
            {
                Classes.Function.ShowBalloonTip(3, "注意", showTip + "\r\n", ToolTipIcon.Warning);
            }

            if (!string.IsNullOrEmpty(lackFeeInfo))
            {
                if (messType == FS.HISFC.Models.Base.MessType.N && !lackDealModel)
                {
                    MessageBox.Show("以下患者存在欠费情况！\r\n" + lackFeeInfo, "欠费提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lackFeeInfo = "";
                }
                else
                {
                    Components.Order.Classes.Function.ShowBalloonTip(10, "欠费提示", "以下患者存在欠费情况！\r\n" + lackFeeInfo, System.Windows.Forms.ToolTipIcon.Info);
                }
            }

            #region 药房盘点提示
            if (!string.IsNullOrEmpty(stockDeptId))
            {
                string strSql = @"select * from pha_com_checkstatic t where t.drug_dept_code = '{0}' and t.foper_time <= sysdate and t.check_state = '0'";
                strSql = string.Format(strSql, stockDeptId);
                if (FS.FrameWork.Function.NConvert.ToInt32(CacheManager.InOrderMgr.ExecSqlReturnOne(strSql)) > 0)
                {
                    MessageBox.Show("温馨提示,药房盘点期间，除急需使用药品，请暂缓到药房取药！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion
            return 0;
        }

        /// <summary>
        /// 是否需要在医嘱内添加"@"附材标志
        /// </summary>
        /// <param name="operFlag"></param>
        /// <param name="isShowSubtblFlag">是否显示附材</param>
        /// <param name="sender"></param>
        void ucSubtblManager1_ShowSubtblFlag(string operFlag, bool isShowSubtblFlag, object sender)
        {
            string s = this.CurrentCellName.Text;
            if (!isShowSubtblFlag)
            {
                //更新医嘱标志
                if (s.Substring(0, 1) == "@")
                {
                    this.CurrentCellName.Text = s.Substring(1);
                }
            }
            else
            {
                if (s.Substring(0, 1) != "@")
                {
                    this.CurrentCellName.Text = "@" + s;
                }
            }
            if (this.dockingManager != null)
                this.dockingManager.HideAllContents();
        }

        #region 吸附窗口处理附材

        public Crownwood.Magic.Docking.DockingManager dockingManager;
        private Crownwood.Magic.Docking.Content content;
        private Crownwood.Magic.Docking.WindowContent wc;
        ucSubtblManager ucSubtblManager1 = null;

        /// <summary>
        /// 附材管理窗口
        /// </summary>
        public void DockingManager()
        {
            this.dockingManager = new Crownwood.Magic.Docking.DockingManager(this, Crownwood.Magic.Common.VisualStyle.IDE);
            this.dockingManager.OuterControl = this.panelMain;		//在OuterControl后加入的控件不受停靠窗口的影响

            content = new Crownwood.Magic.Docking.Content(this.dockingManager);
            content.Control = ucSubtblManager1;

            Size ucSize = content.Control.Size;

            content.Title = "附材管理";
            content.FullTitle = "附材管理";
            content.AutoHideSize = ucSize;
            content.DisplaySize = ucSize;

            this.dockingManager.Contents.Add(content);
        }
        #endregion

        /// <summary>
        /// 查询医嘱
        /// </summary>
        private void QueryOrder()
        {
            if (this.alpatientinfos == null) return;
            this.fpSpread.ChildViewCreated += new FarPoint.Win.Spread.ChildViewCreatedEventHandler(fpSpread_ChildViewCreated);

            this.myShowOrderType = FS.HISFC.Models.Order.EnumType.SHORT;//临时医嘱初始化
            this.fpSpread.Sheets[1].DataSource = CreateDataSetShort(this.alpatientinfos);

            this.myShowOrderType = FS.HISFC.Models.Order.EnumType.LONG;//长期医嘱初始化
            this.fpSpread.Sheets[0].DataSource = CreateDataSetLong(this.alpatientinfos);

            this.fpSpread.Sheets[0].Columns[0].Visible = false;
            this.fpSpread.Sheets[0].Columns[2].Locked = true;
            this.fpSpread.Sheets[0].Columns[3].Locked = true;
            this.fpSpread.Sheets[0].Columns[7].ForeColor = Color.Red;
            this.fpSpread.Sheets[0].GrayAreaBackColor = Color.WhiteSmoke;
            this.fpSpread.Sheets[0].OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;

            Classes.Function.DrawCombo(this.fpSpread.Sheets[0], (int)EnumOrderColumns.CombNo, (int)EnumOrderColumns.ConbFlag, 1);

            this.fpSpread.Sheets[1].Columns[0].Visible = false;
            this.fpSpread.Sheets[1].Columns[2].Locked = true;
            this.fpSpread.Sheets[1].Columns[3].Locked = true;
            this.fpSpread.Sheets[1].GrayAreaBackColor = Color.WhiteSmoke;
            this.fpSpread.Sheets[1].OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;

            Classes.Function.DrawCombo(this.fpSpread.Sheets[1], (int)EnumOrderColumns.CombNo, (int)EnumOrderColumns.ConbFlag, 1);


            this.ExpandAll();//展开

            this.RefreshView();//刷新列表信息

            SetQuitTimes();
        }

        /// <summary>
        /// 婴儿的费用是否可以收取到妈妈身上
        /// </summary>
        private string motherPayAllFee = "";

        /// <summary>
        /// 设置退费次数
        /// </summary>
        private void SetQuitTimes()
        {
            if (this.defaultQuitQty == 0)
            {
            }
            else if (this.defaultQuitQty == 1)
            {
                string patientNo = "";
                if (string.IsNullOrEmpty(motherPayAllFee))
                {
                    motherPayAllFee = CacheManager.ContrlManager.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.SysConst.Use_Mother_PayAllFee, false, "0");
                }

                //是否存在口服药
                bool isPO = false;

                for (int k = 0; k <= 1; k++)
                {
                    for (int i = 0; i < this.fpSpread.Sheets[k].Rows.Count; i++)
                    {
                        this.fpSpread.BackColor = System.Drawing.Color.Azure;
                        try
                        {
                            FarPoint.Win.Spread.SheetView sv = this.fpSpread.Sheets[k].GetChildView(i, 0);

                            if (sv != null)
                            {
                                for (int j = 0; j < sv.Rows.Count; j++)
                                {
                                    //停止时间
                                    if (sv.Cells[j, (int)EnumOrderColumns.EndDate].Text != "")
                                    {
                                        DateTime dtEnd = FS.FrameWork.Function.NConvert.ToDateTime(sv.Cells[j, (int)EnumOrderColumns.EndDate].Text);

                                        FS.HISFC.Models.Order.Inpatient.Order order = CacheManager.InOrderMgr.QueryOneOrder(sv.Cells[j, (int)EnumOrderColumns.MoOrderID].Text);

                                        #region 长期医嘱

                                        if (order.OrderType.IsDecompose)
                                        {
                                            if (order.Status == 4)
                                            {
                                                sv.RowHeader.Rows[j].Label = "重整";
                                                sv.Cells[j, (int)EnumOrderColumns.OrderType].Tag = "NO";
                                                sv.RowHeader.Rows[j].ForeColor = Color.Red;
                                            }
                                            else
                                            {
                                                sv.RowHeader.Rows[j].ForeColor = Color.Black;

                                                ArrayList alExeOrder = CacheManager.InOrderMgr.QueryExecOrderByOneOrder(order.ID, order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug ? "1" : "2");

                                                int dcTimes = 0;

                                                foreach (FS.HISFC.Models.Order.ExecOrder exe in alExeOrder)
                                                {
                                                    if (!SOC.HISFC.BizProcess.Cache.Common.IsPOUsage(exe.Order.Usage.ID))
                                                    {
                                                        if (exe.IsCharge)
                                                        {
                                                            if (exe.DateUse > order.EndTime)
                                                            {
                                                                //这里应该显示应退的执行档次数
                                                                dcTimes++;

                                                                #region
                                                                //婴儿的费用收在妈妈的身上 
                                                                //if (motherPayAllFee == "1")
                                                                //{
                                                                //    patientNo = this.radtIntegrate.QueryBabyMotherInpatientNO(exe.Order.Patient.ID);
                                                                //    //没有找到母亲的住院号，认为此患者不是婴儿
                                                                //    if (patientNo == "-1" || string.IsNullOrEmpty(patientNo))
                                                                //    {
                                                                //        patientNo = exe.Order.Patient.ID;
                                                                //    }
                                                                //}
                                                                //else
                                                                //{
                                                                //    patientNo = exe.Order.Patient.ID;
                                                                //}
                                                                //ArrayList feeItemListTempArray = feeManager.GetItemListByExecSQN(patientNo, exe.ID, exe.Order.Item.ItemType);

                                                                //if (feeItemListTempArray == null || feeItemListTempArray.Count <= 0)
                                                                //{
                                                                //    continue;
                                                                //}
                                                                //else
                                                                //{
                                                                //    dcTimes++;
                                                                //}
                                                                #endregion
                                                            }
                                                            else
                                                            {
                                                                //护理级别在停止当天默认退1。而且，当天入院，当天出院，需要收取护理费用
                                                                if (order.Item.SysClass.ID.ToString() == "UN"
                                                                    && exe.DateUse.Date == order.EndTime.Date
                                                                    && exe.DateUse.Date != order.MOTime.Date)
                                                                {
                                                                    dcTimes++;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else if (this.isQuitPOQty)
                                                    {
                                                        if (exe.DateUse > order.EndTime && exe.IsCharge)
                                                        {
                                                            //这里应该显示应退的执行档次数
                                                            dcTimes++;

                                                            #region
                                                            //婴儿的费用收在妈妈的身上 
                                                            //if (motherPayAllFee == "1")
                                                            //{
                                                            //    patientNo = this.radtIntegrate.QueryBabyMotherInpatientNO(exe.Order.Patient.ID);
                                                            //    //没有找到母亲的住院号，认为此患者不是婴儿
                                                            //    if (patientNo == "-1" || string.IsNullOrEmpty(patientNo))
                                                            //    {
                                                            //        patientNo = exe.Order.Patient.ID;
                                                            //    }
                                                            //}
                                                            //else
                                                            //{
                                                            //    patientNo = exe.Order.Patient.ID;
                                                            //}
                                                            //ArrayList feeItemListTempArray = feeManager.GetItemListByExecSQN(patientNo, exe.ID, exe.Order.Item.ItemType);

                                                            //if (feeItemListTempArray == null || feeItemListTempArray.Count <= 0)
                                                            //{
                                                            //    continue;
                                                            //}
                                                            //else
                                                            //{
                                                            //    dcTimes++;
                                                            //}
                                                            #endregion
                                                        }

                                                    }
                                                    else
                                                    {
                                                        isPO = true;
                                                    }
                                                }

                                                //标识 标识
                                                if (!order.OrderType.isCharge || order.Item.ID == "999")
                                                {
                                                    //sv.Cells[j, (int)EnumOrderColumns.OrderType].Tag = "NO";
                                                }
                                                sv.Cells[j, (int)EnumOrderColumns.MoOrderID].Tag = dcTimes;
                                                //此处存最终的次数
                                                sv.RowHeader.Rows[j].Tag = dcTimes;

                                                if (dcTimes > 0)
                                                {
                                                    sv.RowHeader.Rows[j].Label = "退" + dcTimes.ToString();
                                                    sv.RowHeader.Rows[j].BackColor = Color.Pink;
                                                }
                                                else
                                                {
                                                    sv.RowHeader.Rows[j].Label = "退" + dcTimes.ToString();
                                                }
                                            }
                                        }
                                        #endregion

                                        #region 临时医嘱

                                        else
                                        {
                                            ArrayList alExeOrder = CacheManager.InOrderMgr.QueryExecOrderByOneOrder(order.ID, order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug ? "1" : "2");

                                            int dcTimes = 0;

                                            foreach (FS.HISFC.Models.Order.ExecOrder exe in alExeOrder)
                                            {
                                                if (!exe.Order.OrderType.IsDecompose && exe.IsCharge)
                                                {
                                                    //婴儿的费用收在妈妈的身上 
                                                    if (motherPayAllFee == "1")
                                                    {
                                                        patientNo = CacheManager.RadtIntegrate.QueryBabyMotherInpatientNO(exe.Order.Patient.ID);
                                                        //没有找到母亲的住院号，认为此患者不是婴儿
                                                        if (patientNo == "-1" || string.IsNullOrEmpty(patientNo))
                                                        {
                                                            patientNo = exe.Order.Patient.ID;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        patientNo = exe.Order.Patient.ID;
                                                    }

                                                    ArrayList feeItemListTempArray = CacheManager.InPatientFeeMgr.GetItemListByExecSQN(patientNo, exe.ID, exe.Order.Item.ItemType);

                                                    if (feeItemListTempArray == null || feeItemListTempArray.Count <= 0)
                                                    {
                                                        continue;
                                                    }
                                                    else
                                                    {
                                                        dcTimes++;
                                                    }
                                                }
                                            }

                                            //标识 标识
                                            if (!order.OrderType.isCharge || order.Item.ID == "999")
                                            {
                                                //sv.Cells[j, (int)EnumOrderColumns.OrderType].Tag = "NO";
                                            }

                                            //临嘱默认都是全退
                                            //int dcTimes = 1;
                                            //if (SOC.HISFC.BizProcess.Cache.Common.IsPOUsage(order.Usage.ID))
                                            //{
                                            //    if (!this.isQuitPOQty)
                                            //    {
                                            //        dcTimes = 0;
                                            //        isPO = true;
                                            //    }
                                            //}

                                            sv.Cells[j, (int)EnumOrderColumns.MoOrderID].Tag = dcTimes;
                                            //此处存最终的次数
                                            sv.RowHeader.Rows[j].Tag = dcTimes;

                                            if (dcTimes > 0)
                                            {
                                                sv.RowHeader.Rows[j].Label = "退" + dcTimes.ToString();
                                                sv.RowHeader.Rows[j].BackColor = Color.Pink;
                                            }
                                            else
                                            {
                                                sv.RowHeader.Rows[j].Label = "退" + dcTimes.ToString();
                                            }
                                        }
                                        #endregion
                                    }
                                }
                            }
                        }
                        catch { }

                        if (isPO)
                        {
                            Classes.Function.ShowBalloonTip(3, "提示", "\r\n口服药如需退费请自行选择退费次数！\r\n", ToolTipIcon.Info);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 展开全部节点
        /// </summary>
        private void ExpandAll()
        {
            for (int j = 0; j < this.fpSpread.Sheets.Count; j++)
            {
                for (int i = 0; i < this.fpSpread.Sheets[j].Rows.Count; i++)
                {
                    this.fpSpread.Sheets[j].ExpandRow(i, true);
                    SheetView sv = this.fpSpread.Sheets[j].GetChildView(i, 0);
                    this.SetChildViewStyle(sv);
                }
            }
        }

        /// <summary>
        /// 获得列序号 (没用了吧）
        /// </summary>
        /// <param name="name"></param>
        /// <param name="iSheet"></param>
        /// <returns></returns>
        [Obsolete("没用了吧？", true)]
        private int GetColumnIndex(string name, int iSheet)
        {
            DataTable dt = null;
            if (iSheet == 0)
            {
                dt = dtChild1;
            }
            else
            {
                dt = dtChild2;
            }
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == name)
                    return i;
            }
            MessageBox.Show("缺少列" + Name);
            return -1;
        }

        /// <summary>
        /// 返回长期医嘱的dataSet
        /// </summary>
        /// <param name="alPatient"></param>
        /// <returns></returns>
        private DataSet CreateDataSetLong(ArrayList alPatient)
        {
            DataTable dtMain;
            DataSet myDataSet;

            //定义传出DataSet
            myDataSet = new DataSet();
            myDataSet.EnforceConstraints = false;//是否遵循约束规则
            //定义类型
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtBool = System.Type.GetType("System.Boolean");
            System.Type dtInt = System.Type.GetType("System.Int32");
            //定义表********************************************************
            //Main Table

            dtMain = myDataSet.Tables.Add("TableMain");
            //{4ED608A1-4AAA-433a-B0F8-ABE8EA029E1C}
            dtMain.Columns.AddRange(new DataColumn[] 
            { 
                new DataColumn("ID", dtStr), 
                new DataColumn("审核", dtBool), 
                new DataColumn("患者姓名", dtStr), 
                new DataColumn("床号", dtStr),
                new DataColumn("性别",dtStr),
                new DataColumn("年龄",dtStr),
                new DataColumn("主管医生",dtStr),
                new DataColumn ("温馨提示：请继续分解长期医嘱。",dtStr )
            });
            //ChildTable1

            dtChild1 = myDataSet.Tables.Add("TableChild1");
            dtChild1.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID",dtStr),
                new DataColumn("医嘱流水号", dtStr),
                new DataColumn("组合号", dtStr),
                new DataColumn("审核", dtBool),
                new DataColumn("系统类别",dtStr),
                new DataColumn("医嘱名称", dtStr),
                new DataColumn("组", dtStr),
                new DataColumn("规格", dtStr),
                new DataColumn("每次量", dtStr),
                new DataColumn("频次", dtStr),
                new DataColumn("用法", dtStr),
                new DataColumn("数量", dtStr),
                new DataColumn("首日量", dtStr),
                new DataColumn("医嘱类型", dtStr),
                new DataColumn("急", dtBool),
                new DataColumn("开始时间", dtStr),
                new DataColumn("停止时间", dtStr),
                new DataColumn("开立医生", dtStr),
                new DataColumn("执行科室", dtStr),
                new DataColumn("开立时间", dtStr),
                new DataColumn("停止医生", dtStr),
                new DataColumn("备注", dtStr),
                new DataColumn("顺序号", dtStr),
                new DataColumn("批注",dtStr),
                new DataColumn("状态",dtStr),
                new DataColumn("皮试",dtStr),
                new DataColumn("滴速",dtStr)

            });

            this.OrderId.ID = "1";
            this.ComboNo.ID = "2";

            this.fpSpread.Sheets[0].RowCount = 0;

            string tempCombNo = "";
            this.LongOrderCount = 0;
            FS.HISFC.Models.RADT.PatientInfo p = null;
            for (int i = 0; i < alPatient.Count; i++)
            {
                p = (FS.HISFC.Models.RADT.PatientInfo)alPatient[i];

                //查询未审核的医嘱--判断查询医嘱类型
                ArrayList al = CacheManager.InOrderMgr.QueryIsConfirmOrder(p.ID, myShowOrderType, false);

                al = this.DealOperationOrder(al);

                if (al.Count > 0)
                {
                    this.LongOrderCount = this.LongOrderCount + al.Count;
                    //{4ED608A1-4AAA-433a-B0F8-ABE8EA029E1C}
                    //dtMain.Rows.Add(new Object[] { p.ID, false, p.Name, p.PVisit.PatientLocation.Bed.ID.Substring(4) });//添加行
                    dtMain.Rows.Add(new Object[] { p.ID, false, p.Name, p.PVisit.PatientLocation.Bed.ID.Substring(4), p.Sex.Name, p.Age.ToString(),p.PVisit.AttendingDoctor.Name,""});
                    for (int j = 0; j < al.Count; j++)
                    {
                        FS.HISFC.Models.Order.Inpatient.Order o = al[j] as FS.HISFC.Models.Order.Inpatient.Order;

                        if (o.IsPermission) //已经有权限的药品
                            o.Item.Name = "【√】" + o.Item.Name;

                        # region 同一个组合取一次就可以了
                        if (tempCombNo != o.Combo.ID)
                        {
                            int count = CacheManager.InOrderMgr.QuerySubtbl(o.Combo.ID).Count;
                            tempCombNo = o.Combo.ID;
                            if (count > 0)
                                o.Item.Name = "@" + o.Item.Name; //显示附材
                        }
                        # endregion
                        if (o.Item.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))//药品
                        {
                            FS.HISFC.Models.Pharmacy.Item item = o.Item as FS.HISFC.Models.Pharmacy.Item;

                            dtChild1.Rows.Add(new Object[] 
                            {
                                o.Patient.ID,
                                o.ID,
                                o.Combo.ID,
                                false,
                                o.Item.SysClass.Name,
                                o.Item.Name,
                                "",
                                o.Item.Specs,
                                o.DoseOnce.ToString()+item.DoseUnit ,
                                o.Frequency.ID,
                                o.Usage.Name,
                                o.Item.Qty ==0 ? "":(o.Item.Qty.ToString()+o.Unit),
                                o.FirstUseNum,
                                o.OrderType.Name,
                                o.IsEmergency,
                                o.BeginTime.ToString("MM-dd HH:mm"),
                                o.EndTime.ToString("MM-dd HH:mm") == "01-01 00:00" ? "":o.EndTime.ToString("MM-dd HH:mm"),
                                o.ReciptDoctor.Name,
                                o.ExeDept.Name,
                                o.MOTime,
                                o.DCOper.Name,
                                o.Memo,
                                o.SortID,
                                o.Note,
                                Classes.Function.OrderStatus(o.Status),
                                
                                CacheManager.OutOrderMgr.TransHypotest(o.HypoTest),
                                o.Dripspreed
                            });

                        }
                        else if (o.Item.GetType() == typeof(FS.HISFC.Models.Fee.Item.Undrug))
                        {

                            dtChild1.Rows.Add(new Object[] 
                            {
                                o.Patient.ID,
                                o.ID,
                                o.Combo.ID,
                                false,
                                o.Item.SysClass.Name,
                                o.Item.Name,
                                "",
                                o.Item.Specs,
                                "" ,
                                o.Frequency.ID,
                                "",
                                o.Item.Qty.ToString()+o.Unit,
                                o.FirstUseNum,
                                o.OrderType.Name,
                                o.IsEmergency,
                                o.BeginTime.ToString("MM-dd HH:mm"),
                                o.EndTime.ToString("MM-dd HH:mm") == "01-01 00:00" ? "":o.EndTime.ToString("MM-dd HH:mm"),
                                o.ReciptDoctor.Name,
                                o.ExeDept.Name,
                                o.MOTime,
                                o.DCOper.Name,
                                o.Memo,
                                o.SortID,
                                o.Note,
                                Classes.Function.OrderStatus(o.Status),
                                CacheManager.OutOrderMgr.TransHypotest(o.HypoTest),
                                o.Dripspreed
                            });
                        }
                    }
                }
            }
            //表关联显示
            myDataSet.Relations.Add("TableChild1", dtMain.Columns["ID"], dtChild1.Columns["ID"]);

            return myDataSet;
        }

        /// <summary>
        /// 返回临时医嘱的DataSet
        /// </summary>
        /// <param name="alPatient"></param>
        /// <returns></returns>
        private DataSet CreateDataSetShort(ArrayList alPatient)
        {
            DataTable dtMain;
            DataSet myDataSet;

            //ataTable dtChild1;
            //定义传出DataSet
            myDataSet = new DataSet();
            myDataSet.EnforceConstraints = false;//是否遵循约束规则

            //定义类型
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtBool = System.Type.GetType("System.Boolean");
            System.Type dtInt = System.Type.GetType("System.Int32");
            //定义表********************************************************

            //Main Table
            dtMain = myDataSet.Tables.Add("TableMain");
            //{4ED608A1-4AAA-433a-B0F8-ABE8EA029E1C}
            dtMain.Columns.AddRange(new DataColumn[] 
            {
                new DataColumn("ID", dtStr), 
                new DataColumn("审核", dtBool), 
                new DataColumn("患者姓名", dtStr), 
                new DataColumn("床号", dtStr),
                new DataColumn("性别",dtStr),
                new DataColumn("年龄",dtStr),
                new DataColumn("主管医生",dtStr),
            });

            //ChildTable1
            dtChild2 = myDataSet.Tables.Add("TableChild1");
            dtChild2.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID",dtStr),
                new DataColumn("医嘱流水号", dtStr),
                new DataColumn("组合号", dtStr),
                new DataColumn("审核", dtBool),
                new DataColumn("系统类别",dtStr),
                new DataColumn("医嘱名称", dtStr),
                new DataColumn("组", dtStr),
                new DataColumn("规格", dtStr),
                new DataColumn("每次量", dtStr),  
                new DataColumn("频次", dtStr),
                new DataColumn("用法", dtStr),
                new DataColumn("数量", dtStr),
                new DataColumn("付数", dtStr), 
                new DataColumn("医嘱类型", dtStr),
                new DataColumn("急", dtBool),	  
                new DataColumn("开始时间", dtStr),
                new DataColumn("停止时间", dtStr),
                new DataColumn("开立医生", dtStr),
                new DataColumn("执行科室", dtStr),
                new DataColumn("开立时间", dtStr),
                new DataColumn("停止医生", dtStr),
                new DataColumn("备注", dtStr),
                new DataColumn("顺序号", dtStr),
                new DataColumn("批注",dtStr),
                new DataColumn("状态",dtStr),
                new DataColumn("皮试",dtStr),
                new DataColumn("滴速",dtStr)
            });
            this.OrderId.ID = "1";
            this.ComboNo.ID = "2";


            this.fpSpread.Sheets[1].RowCount = 0;

            string tempCombNo = "";
            this.ShortOrderCount = 0;
            for (int i = 0; i < alPatient.Count; i++)
            {
                FS.HISFC.Models.RADT.PatientInfo p = (FS.HISFC.Models.RADT.PatientInfo)alPatient[i];
                //查询未审核的医嘱--判断查询医嘱类型
                ArrayList al = CacheManager.InOrderMgr.QueryIsConfirmOrder(p.ID, myShowOrderType, false);	//查询未审核的医嘱

                al = this.DealOperationOrder(al);

                if (al.Count > 0)
                {
                    this.ShortOrderCount = this.ShortOrderCount + al.Count;

                    //{C3C32101-297D-40c1-97BA-46938537002B}  床位号截取
                    string bedNO = p.PVisit.PatientLocation.Bed.ID;
                    if (bedNO.Length > 4)
                    {
                        bedNO = bedNO.Substring(4);
                    }
                    //{4ED608A1-4AAA-433a-B0F8-ABE8EA029E1C}
                    dtMain.Rows.Add(new Object[] { p.ID, false, p.Name, bedNO, p.Sex.Name, p.Age.ToString(), p.PVisit.AttendingDoctor.Name });//添加行
                    for (int j = 0; j < al.Count; j++)
                    {
                        FS.HISFC.Models.Order.Inpatient.Order o = al[j] as FS.HISFC.Models.Order.Inpatient.Order;

                        if (o.IsPermission) //
                            o.Item.Name = "【√】" + o.Item.Name;
                        

                        # region 同一个组合取一次就可以了
                        if (tempCombNo != o.Combo.ID)
                        {
                            int count = CacheManager.InOrderMgr.QuerySubtbl(o.Combo.ID).Count;
                            tempCombNo = o.Combo.ID;
                            if (count > 0)
                                o.Item.Name = "@" + o.Item.Name; //显示附材
                        }
                        # endregion
                        if (o.Item.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))
                        {
                            FS.HISFC.Models.Pharmacy.Item item = o.Item as FS.HISFC.Models.Pharmacy.Item;

                            dtChild2.Rows.Add(new Object[] 
                            {
                                o.Patient.ID,
                                o.ID,
                                o.Combo.ID,
                                false,
                                o.Item.SysClass.Name,
                                o.Item.Name,
                                "",
                                o.Item.Specs,
                                o.DoseOnce.ToString()+item.DoseUnit ,
                                o.Frequency.ID,
                                o.Usage.Name,
                                o.Item.Qty ==0 ? "":(o.Item.Qty.ToString()+o.Unit),
                                o.HerbalQty==0 ? "":o.HerbalQty.ToString(),
                                o.OrderType.Name,
                                o.IsEmergency,
                                o.BeginTime.ToString("MM-dd HH:mm"),
                                o.EndTime.ToString("MM-dd HH:mm") == "01-01 00:00" ? "":o.EndTime.ToString("MM-dd HH:mm"),
                                o.ReciptDoctor.Name,
                                o.ExeDept.Name,
                                o.MOTime,
                                o.DCOper.Name,
                                o.Memo,
                                o.SortID,
                                o.Note,
                                Classes.Function.OrderStatus(o.Status),
                                CacheManager.OutOrderMgr.TransHypotest(o.HypoTest),
                                o.Dripspreed
                            });

                        }
                        else if (o.Item.GetType() == typeof(FS.HISFC.Models.Fee.Item.Undrug))
                        {
                            dtChild2.Rows.Add(new Object[] 
                            {
                                o.Patient.ID,
                                o.ID,
                                o.Combo.ID,
                                false,
                                o.Item.SysClass.Name,
                                o.Item.Name,
                                "",
                                o.Item.Specs,
                                "" ,
                                o.Frequency.ID,
                                "",
                                o.Item.Qty.ToString()+o.Unit,
                                "",
                                o.OrderType.Name,
                                o.IsEmergency,
                                o.BeginTime.ToString("MM-dd HH:mm"),
                                o.EndTime.ToString("MM-dd HH:mm") == "01-01 00:00" ? "":o.EndTime.ToString("MM-dd HH:mm"),
                                o.ReciptDoctor.Name,
                                o.ExeDept.Name,
                                o.MOTime,
                                o.DCOper.Name,
                                o.Memo,
                                o.SortID,
                                o.Note,
                                Classes.Function.OrderStatus(o.Status),
                                CacheManager.OutOrderMgr.TransHypotest(o.HypoTest),
                                o.Dripspreed
                            });
                        }

                    }
                }
            }
            //关联
            myDataSet.Relations.Add("TableChild1", dtMain.Columns["ID"], dtChild2.Columns["ID"]);

            return myDataSet;
        }

        /// <summary>
        /// 处理特殊显示的医嘱
        /// </summary>
        /// <param name="alOrders"></param>
        /// <returns></returns>
        private ArrayList DealOperationOrder(ArrayList alOrders)
        {
            ArrayList alNews = new ArrayList();

            if (this.currentDept == null)
            {
                this.currentDept = hsDepts[(this.deptManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID] as FS.HISFC.Models.Base.Department;
            }

            if (this.currentDept == null)
            {
                MessageBox.Show("获取当前科室出错！");
                return null;
            }

            //不设置，默认查看全部,原算法仍生效
            if (this.speOrderType.Length <= 0)
            {
                foreach (FS.HISFC.Models.Order.Inpatient.Order order in alOrders)
                {
                    /*
                     *0 普通
                     *1 手术
                     *2 麻醉
                     *3 ICU
                     *4 CCU
                     */
                    //登陆为手术室的护士
                    //区分开立的医嘱和停止的医嘱
                    #region 新开立的医嘱
                    if (order.Status == 0)
                    {
                        if (this.currentDept.SpecialFlag == "1")
                        {
                            if ((hsDepts[order.ReciptDept.ID] as FS.HISFC.Models.Base.Department).SpecialFlag == "2" ||
                                    (hsDepts[order.ReciptDept.ID] as FS.HISFC.Models.Base.Department).SpecialFlag == "1")
                            {
                                order.ReciptDept = hsDepts[order.ReciptDept.ID] as FS.HISFC.Models.Base.Department;
                                alNews.Add(order);
                            }
                        }
                        else
                        {
                            if ((hsDepts[order.ReciptDept.ID] as FS.HISFC.Models.Base.Department).SpecialFlag != "2" &&
                                     (hsDepts[order.ReciptDept.ID] as FS.HISFC.Models.Base.Department).SpecialFlag != "1")
                            {
                                order.ReciptDept = hsDepts[order.ReciptDept.ID] as FS.HISFC.Models.Base.Department;
                                alNews.Add(order);
                            }
                        }
                    }
                    #endregion

                    #region 停止的医嘱
                    else
                    {
                        //系统没有存停止科室，所以停止的医嘱 所有人都可以看到 审核
                        order.ReciptDept = hsDepts[order.ReciptDept.ID] as FS.HISFC.Models.Base.Department;
                        alNews.Add(order);
                    }
                    #endregion
                }
            }
            else
            {
                //只要包含一个，就认为可以看到
                string[] speStr = this.speOrderType.Split(',');

                foreach (FS.HISFC.Models.Order.Inpatient.Order order in alOrders)
                {
                    if (order.SpeOrderType.Length <= 0)
                    {
                        /*
                         *0 普通
                         *1 手术
                         *2 麻醉
                         *3 ICU
                         *4 CCU
                         */
                        //登陆为手术室的护士
                        if (this.currentDept.SpecialFlag == "1")
                        {
                            if ((hsDepts[order.ReciptDept.ID] as FS.HISFC.Models.Base.Department).SpecialFlag == "2" ||
                                    (hsDepts[order.ReciptDept.ID] as FS.HISFC.Models.Base.Department).SpecialFlag == "1")
                            {
                                order.ReciptDept = hsDepts[order.ReciptDept.ID] as FS.HISFC.Models.Base.Department;
                                alNews.Add(order);
                            }
                        }
                        else
                        {
                            if ((hsDepts[order.ReciptDept.ID] as FS.HISFC.Models.Base.Department).SpecialFlag != "2" &&
                                     (hsDepts[order.ReciptDept.ID] as FS.HISFC.Models.Base.Department).SpecialFlag != "1")
                            {
                                order.ReciptDept = hsDepts[order.ReciptDept.ID] as FS.HISFC.Models.Base.Department;
                                alNews.Add(order);
                            }
                        }
                    }
                    else
                    {
                        bool isAdd = false;

                        foreach (string s in speStr)
                        {
                            if (order.SpeOrderType.IndexOf(s) >= 0)
                            {
                                isAdd = true;
                                break;
                            }
                        }

                        if (isAdd)
                            alNews.Add(order);
                    }
                }
            }

            return alNews;
        }

        /// <summary>
        /// 设置医嘱明细显示
        /// </summary>
        /// <param name="sv"></param>
        public void SetChildViewStyle(FarPoint.Win.Spread.SheetView sv)
        {
            this.SetChildViewStyle(sv, true);
        }

        /// <summary>
        /// 设置医嘱明细显示
        /// </summary>
        /// <param name="sv"></param>
        /// <param name="SetChildViewStyle"></param>
        public void SetChildViewStyle(FarPoint.Win.Spread.SheetView sv, bool SetChildViewStyle)
        {
            try
            {
                //Make the header font italic
                sv.ColumnHeader.DefaultStyle.Font = this.fpSpread.Font;
                sv.ColumnHeader.DefaultStyle.Border = new EmptyBorder();
                sv.ColumnHeader.DefaultStyle.BackColor = Color.White;
                sv.ColumnHeader.DefaultStyle.ForeColor = Color.Black;
                //Change the sheet corner color
                sv.SheetCornerStyle.BackColor = Color.White;
                sv.SheetCornerStyle.Border = new EmptyBorder();

                //Clear the autotext
                sv.RowHeader.AutoText = HeaderAutoText.Blank;

                sv.RowHeader.DefaultStyle.BackColor = Color.Honeydew;
                sv.RowHeader.DefaultStyle.ForeColor = Color.Black;

                sv.ColumnHeaderVisible = true;
                sv.RowHeaderVisible = SetChildViewStyle;
                sv.RowHeaderAutoText = HeaderAutoText.Numbers;
                for (int i = 0; i < sv.RowCount; i++)
                {
                    sv.Rows[i].Height = 20;
                }
                //sv.CellChanged += new SheetViewEventHandler(sv_CellChanged);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            sv.DataAutoSizeColumns = false;
            sv.OperationMode = OperationMode.SingleSelect;


            //hide or show the ID column
            sv.Columns[(int)EnumOrderColumns.PatientID].Visible = false;
            sv.Columns[(int)EnumOrderColumns.MoOrderID].Visible = false;
            sv.Columns[(int)EnumOrderColumns.CombNo].Visible = false;
            sv.Columns[(int)EnumOrderColumns.CheckFlag].Visible = true;
            sv.Columns[(int)EnumOrderColumns.CheckFlag].Width = 15;
            sv.Columns[(int)EnumOrderColumns.CheckFlag].Locked = true;

            sv.Columns[(int)EnumOrderColumns.SysClassName].Width = 60;
            sv.Columns[(int)EnumOrderColumns.SysClassName].Locked = true;

            sv.Columns[(int)EnumOrderColumns.ItemName].Width = 200;
            sv.Columns[(int)EnumOrderColumns.ItemName].Locked = true;
            sv.Columns[(int)EnumOrderColumns.ConbFlag].Width = 15;
            sv.Columns[(int)EnumOrderColumns.ConbFlag].Locked = true;
            sv.Columns[(int)EnumOrderColumns.Specs].Width = 62;
            sv.Columns[(int)EnumOrderColumns.Specs].Locked = true;
            sv.Columns[(int)EnumOrderColumns.DoseOne].Width = 48;
            sv.Columns[(int)EnumOrderColumns.DoseOne].Locked = true;
            sv.Columns[(int)EnumOrderColumns.FrequencyID].Width = 37;
            sv.Columns[(int)EnumOrderColumns.FrequencyID].Locked = true;
            sv.Columns[(int)EnumOrderColumns.UsageName].Width = 33;
            sv.Columns[(int)EnumOrderColumns.UsageName].Locked = true;
            sv.Columns[(int)EnumOrderColumns.Qty].Width = 35;
            sv.Columns[(int)EnumOrderColumns.Qty].Locked = true;
            sv.Columns[(int)EnumOrderColumns.FuOrFirstDays].Width = 42;
            sv.Columns[(int)EnumOrderColumns.FuOrFirstDays].Font = new Font(this.fpSpread.Font.Name, this.fpSpread.Font.Size, System.Drawing.FontStyle.Bold);
            sv.Columns[(int)EnumOrderColumns.FuOrFirstDays].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sv.Columns[(int)EnumOrderColumns.FuOrFirstDays].Locked = true;
            sv.Columns[(int)EnumOrderColumns.OrderType].Width = 59;
            sv.Columns[(int)EnumOrderColumns.OrderType].Locked = true;
            sv.Columns[(int)EnumOrderColumns.EmergencyFlag].Width = 19;
            sv.Columns[(int)EnumOrderColumns.EmergencyFlag].Visible = false;
            sv.Columns[(int)EnumOrderColumns.BeginDate].Width = 63;
            sv.Columns[(int)EnumOrderColumns.BeginDate].Locked = true;
            sv.Columns[(int)EnumOrderColumns.EndDate].Width = 63;
            sv.Columns[(int)EnumOrderColumns.EndDate].Locked = true;
            sv.Columns[(int)EnumOrderColumns.RecipeDoctName].Width = 59;
            sv.Columns[(int)EnumOrderColumns.RecipeDoctName].Locked = true;
            sv.Columns[(int)EnumOrderColumns.ExecDeptName].Width = 59;
            sv.Columns[(int)EnumOrderColumns.ExecDeptName].Locked = true;
            sv.Columns[(int)EnumOrderColumns.MoDate].Width = 59;
            sv.Columns[(int)EnumOrderColumns.MoDate].Locked = true;

            sv.Columns[(int)EnumOrderColumns.MoDate].Visible = false;
            sv.Columns[(int)EnumOrderColumns.SortID].Visible = false;
            sv.Columns[(int)EnumOrderColumns.Memo].Font = new Font(this.fpSpread.Font.Name, this.fpSpread.Font.Size, System.Drawing.FontStyle.Bold);
        }

        /// <summary>
        /// 刷新列表信息
        /// </summary>
        protected void RefreshView()
        {
            for (int k = 0; k < 2; k++)
            {
                for (int i = 0; i < this.fpSpread.Sheets[k].Rows.Count; i++) //长期医嘱-临时医嘱
                {
                    this.fpSpread.BackColor = System.Drawing.Color.Azure;
                    try
                    {
                        FarPoint.Win.Spread.SheetView sv = this.fpSpread.Sheets[k].GetChildView(i, 0);
                        if (sv != null)
                        {
                            sv.Columns[(int)EnumOrderColumns.Specs].Font = new Font("Arial", 10, System.Drawing.FontStyle.Bold);
                            sv.Columns[(int)EnumOrderColumns.DoseOne].Font = new Font("Arial", 10, System.Drawing.FontStyle.Bold);
                            for (int j = 0; j < sv.Rows.Count; j++)
                            {
                                //医嘱项目
                                string note = sv.Cells[j, (int)EnumOrderColumns.Tip].Text;//批注
                                if (sv.Cells.Get(j, (int)EnumOrderColumns.MoState).Text == "停止/取消") sv.Rows[j].BackColor = Color.FromArgb(255, 222, 222);//医嘱状态，医嘱作废审核
                                sv.SetNote(j, (int)EnumOrderColumns.ItemName, note);
                                if ((bool)sv.Cells[j, (int)EnumOrderColumns.EmergencyFlag].Value)
                                {
                                    sv.Rows[j].Label = "急";
                                    sv.RowHeader.Rows[j].BackColor = System.Drawing.Color.Pink;
                                }
                                int hypotest = 0;
                                if (sv.Cells[j, (int)EnumOrderColumns.Hypotest].Text == "阳性")
                                {
                                    hypotest = 3;
                                }
                                else if (sv.Cells[j, (int)EnumOrderColumns.Hypotest].Text == "阴性")
                                {
                                    hypotest = 4;
                                }
                                //int hypotest = FS.FrameWork.Function.NConvert.ToInt32(sv.Cells[j, 24].Text);//皮试
                                #region 没用了吧？？
                                string sTip = "需不需皮试";//Function.TipHypotest;
                                if (sv.Cells[j, (int)EnumOrderColumns.ItemName].Text.Length > 3)
                                {
                                    if ((sv.Cells[j, (int)EnumOrderColumns.ItemName].Text.Substring(sv.Cells[j, (int)EnumOrderColumns.ItemName].Text.Length - 3) == "［＋］"
                                    || sv.Cells[j, (int)EnumOrderColumns.ItemName].Text.Substring(sv.Cells[j, (int)EnumOrderColumns.ItemName].Text.Length - 3) == "［－］"))
                                    {
                                        sv.Cells[j, (int)EnumOrderColumns.ItemName].Text = sv.Cells[j, (int)EnumOrderColumns.ItemName].Text.Substring(0, sv.Cells[j, (int)EnumOrderColumns.ItemName].Text.Length - 3);
                                    }
                                }
                                try
                                {
                                    if (sv.Cells[j, (int)EnumOrderColumns.ItemName].Text.Length > 3)
                                        if (sv.Cells[j, (int)EnumOrderColumns.ItemName].Text.Substring(sv.Cells[j, (int)EnumOrderColumns.ItemName].Text.Length - sTip.Length, sTip.Length) == sTip)
                                            sv.Cells[j, (int)EnumOrderColumns.ItemName].Text = sv.Cells[j, (int)EnumOrderColumns.ItemName].Text.Substring(0, sv.Cells[j, (int)EnumOrderColumns.ItemName].Text.Length - sTip.Length);
                                }
                                catch (Exception ex)
                                {
                                    //MessageBox.Show(ex.Message);
                                }
                                #endregion
                                sv.Cells[j, (int)EnumOrderColumns.ItemName].ForeColor = Color.Black;
                                if (hypotest == 3)
                                {
                                    sv.Cells[j, (int)EnumOrderColumns.ItemName].Text += "［＋］";//皮试
                                    sv.Cells[j, (int)EnumOrderColumns.ItemName].ForeColor = Color.Red;
                                }
                                else if (hypotest == 4)
                                {
                                    sv.Cells[j, (int)EnumOrderColumns.ItemName].Text += "［－］";
                                }
                                else if (hypotest == 2)
                                {
                                }

                                //不再显示备注，备注在后面已经有显示了
                                //显示顺序号
                                //if (sv.RowHeader.Cells[j, 0].Text != "急")
                                //    sv.RowHeader.Cells[j, 0].Text = sv.Cells[j, 21].Text;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("刷新医嘱备注信息出错！", "Sorry");
                    }
                }
            }
        }

        #endregion

        #region 事件

        /// <summary>
        /// 该函数没有起作用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fpSpread_ChildViewCreated(object sender, FarPoint.Win.Spread.ChildViewCreatedEventArgs e)
        {
            this.SetChildViewStyle(e.SheetView);
        }

        void fpSpread_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.RowHeader || e.Row < 0)
                return;

            //判断当前的停靠窗口是否已显示 如未显示 则显示停靠窗口
            try
            {
                if (e.View.Sheets[0].Columns[2].Label == "组合号") //是childtable1
                {
                    if (this.content != null && this.content.Visible == false)
                    {
                        if (wc == null && this.dockingManager != null)
                        {
                            wc = this.dockingManager.AddContentWithState(content, Crownwood.Magic.Docking.State.DockRight);
                            this.dockingManager.AddContentToWindowContent(content, wc);
                        }
                        if (this.dockingManager != null)
                            this.dockingManager.ShowContent(this.content);
                    }
                    if (this.ucSubtblManager1 != null && !e.RowHeader && !e.ColumnHeader)		//点击非列标题与行标题
                    {
                        ucSubtblManager1.OrderID = this.OrderId.Name;
                        ucSubtblManager1.ComboNo = this.ComboNo.Name;
                        this.CurrentCellName = e.View.Sheets[0].Cells[e.View.Sheets[0].ActiveRowIndex, (int)EnumOrderColumns.ItemName];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        FarPoint.Win.Spread.CellClickEventArgs cellClickEvent = null;
        int curRow = 0;

        void fpSpread_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Row < 0)
                return;
            if (e.ColumnHeader == true)
                return;
            if (e.Column > 0)
            {
                try
                {
                    int active = this.fpSpread.ActiveSheetIndex;
                    if (e.View.Sheets.Count <= active) active = 0;
                    curRow = active;
                    if (e.View.Sheets[active].Columns[2].Label == "组合号") //子表1
                    {
                        if (e.Button == MouseButtons.Left) //左键
                        {
                            this.OrderId.Name = e.View.Sheets[active].Cells[e.Row, int.Parse(this.OrderId.ID)].Text;
                            this.ComboNo.Name = e.View.Sheets[active].Cells[e.Row, int.Parse(this.ComboNo.ID)].Text;
                            this.PatientId = e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.PatientID].Text;//住院流水号

                            if (e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.CheckFlag].Text.ToUpper() == "TRUE")
                            {
                                e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.CheckFlag].Text = "False";
                                e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.CheckFlag].BackColor = Color.White;
                            }
                            else
                            {
                                e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.CheckFlag].Text = "True";
                                e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.CheckFlag].BackColor = Color.Blue;
                            }

                            // {6B70B558-72C9-4DEF-874F-DABD0A9B5198}               ;
                            //{CB5C628A-EA63-41e7-9D38-3F3DF2E78834}

                            if (e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.CheckFlag].Text.ToUpper() == "TRUE")
                            {
                                FS.HISFC.Models.Order.Inpatient.Order order = new FS.HISFC.Models.Order.Inpatient.Order();
                                order = CacheManager.InOrderMgr.QueryOneOrder(this.OrderId.Name);
                               
                                string flag =CacheManager.InOrderMgr.GetDrugSpecialFlag(order.Item.ID);
                                if (flag == "1")
                                    MessageBox.Show("该药品属于A级高警示药品，注意核对药品名称、规格、用法、剂量、使用浓度及滴速等，请核对！！");
                                if (flag == "2")
                                    MessageBox.Show("该药品属于B级高警示药品，注意核对药品名称、规格、用法、剂量等！！");
                                if (flag == "4")
                                    MessageBox.Show("该药品属于易混淆药品，注意核对药品名称、规格等！！");

                            }


                            //更新组合的医嘱选择信息
                            for (int i = 0; i < e.View.Sheets[active].RowCount; i++)
                            {
                                if (e.View.Sheets[active].Cells[i, int.Parse(this.ComboNo.ID)].Text == this.ComboNo.Name
                                    && i != e.Row)
                                {
                                    e.View.Sheets[active].Cells[i, (int)EnumOrderColumns.CheckFlag].Text = e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.CheckFlag].Text;
                                    e.View.Sheets[active].Cells[i, (int)EnumOrderColumns.CheckFlag].BackColor = e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.CheckFlag].BackColor;
                                }
                            }
                        }
                        else//右键
                        {
                            this.OrderId.Name = e.View.Sheets[active].Cells[e.Row, int.Parse(this.OrderId.ID)].Text;
                            string strItemName = e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.ItemName].Text;
                            this.PatientId = e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.PatientID].Text;//住院流水号 
                            cellClickEvent = e;
                            ContextMenu menu = new ContextMenu();
                            MenuItem mnuTip = new MenuItem("批注");//批注
                            mnuTip.Click += new EventHandler(mnuTip_Click);

                            //取消修改取药科室功能先

                            //MenuItem mnuChangeDept = new MenuItem("修改取药科室");//修改取药科室
                            //mnuChangeDept.Click += new EventHandler(mnuChangeDept_Click);

                            //menu.MenuItems.Add(mnuChangeDept);


                            MenuItem mnuDcTimes = new MenuItem("退费次数");
                            mnuDcTimes.Click += new EventHandler(mnuDcTimes_Click);

                            menu.MenuItems.Add(mnuTip);

                            //长嘱界面，停止时间不为空
                            if (active == 0 && e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.EndDate].Text != "")
                            {
                                menu.MenuItems.Add(mnuDcTimes);
                            }

                            this.fpSpread.ContextMenu = menu;
                            //Function.PopMenu(menu, obj.Item.ID, false);

                        }
                    }
                    else if (e.View.Sheets[active].Columns[2].Label == "患者姓名")//主表
                    {
                        if (e.Button == MouseButtons.Left)
                        {
                            if (e.View.Sheets[active].Cells[e.Row, 1].Text.ToUpper() == "TRUE")
                            {
                                e.View.Sheets[active].Cells[e.Row, 1].Text = "false";

                            }
                            else
                            {
                                e.View.Sheets[active].Cells[e.Row, 1].Text = "True";

                            }
                            //更新子表的选择
                            try
                            {
                                FarPoint.Win.Spread.SheetView sv = e.View.Sheets[active].GetChildView(e.Row, 0);//(FarPoint.Win.Spread.SpreadView).GetChildWorkbooks()[e.Row];
                                if (sv.Columns[3].Label == "审核")
                                {
                                    for (int i = 0; i < sv.Rows.Count; i++)
                                    {
                                        sv.Cells[i, 3].Text = e.View.Sheets[active].Cells[e.Row, 1].Text;
                                    }

                                    #region 注释
                                    //string A = "";
                                    //string B = "";
                                    //string C = "";

                                    //for (int i = 0; i < sv.Rows.Count; i++) 
                                    //{
                                    //    //e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.MoOrderID].Text;
                                    //    FS.HISFC.Models.Order.Inpatient.Order order = new FS.HISFC.Models.Order.Inpatient.Order();
                                    //    string b = OrderId.ID;
                                    //    string a = e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.MoOrderID].Text;
                                    //    return;
                                    //    order = CacheManager.InOrderMgr.QueryOneOrder(e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.MoOrderID].Text);

                                    //    string flag = CacheManager.InOrderMgr.GetDrugSpecialFlag(order.Item.ID);
                                    //    if (flag == "1")
                                    //        A += order.Item.Name + " ";
                                    //    if (flag == "2")
                                    //        B += order.Item.Name + " ";
                                    //    if (flag == "3")
                                    //        C += order.Item.Name + " ";
                                    //}

                                    //string message = "";
                                    //if (A != "")
                                    //    message += A + ",属于A级";
                                    //if(B!="")
                                    //    message += B + ",属于B级";
                                    //if(C != "")
                                    //    message += C + ",属于C级";
                                    //if(message!="")
                                    //    MessageBox.Show(message+"高危药品，请核对！！");
                                    #endregion

                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                return;
                            }
                            this.OrderId.Name = "";
                            this.ComboNo.Name = "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }

        /// <summary>
        /// 退费次数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mnuDcTimes_Click(object sender, EventArgs e)
        {
            //FarPoint.Win.Spread.SheetView sv = this.fpSpread.Sheets[this.fpSpread.ActiveSheetIndex].GetChildView(curRow, 0);
            if (cellClickEvent == null)
            {
                return;
            }

            int dcTimes = 0;
            //允许退费的最大次数
            int dcMaxTimes = 0;

            try
            {
                dcTimes = FS.FrameWork.Function.NConvert.ToInt32(cellClickEvent.View.Sheets[curRow].Cells[cellClickEvent.Row, (int)EnumOrderColumns.MoOrderID].Tag);

                dcMaxTimes = FS.FrameWork.Function.NConvert.ToInt32(cellClickEvent.View.Sheets[curRow].RowHeader.Rows[cellClickEvent.Row].Tag);

                //if (dcMaxTimes <= 0)
                //{
                //    MessageBox.Show("该条医嘱停止时间之后，没有已执行数量！");
                //    return;
                //}

                FS.FrameWork.WinForms.Controls.NeuNumericUpDown nuTimes = new FS.FrameWork.WinForms.Controls.NeuNumericUpDown();

                nuTimes.Font = new Font("宋体", 14, FontStyle.Bold);
                nuTimes.Dock = DockStyle.Fill;

                Form baseForm = new Form();
                baseForm.Size = new Size(300, 110);

                FS.FrameWork.WinForms.Controls.NeuLabel lblTip = new FS.FrameWork.WinForms.Controls.NeuLabel();
                lblTip.Text = "退费次数默认为医嘱停止时间后的执行次数";
                lblTip.Dock = DockStyle.Bottom;

                FS.FrameWork.WinForms.Controls.NeuButton btOK = new FS.FrameWork.WinForms.Controls.NeuButton();
                btOK.Text = "保  存";
                btOK.Dock = DockStyle.Bottom;
                btOK.Click += new EventHandler(btOK_Click);

                baseForm.Controls.Add(btOK);
                baseForm.Controls.Add(lblTip);
                baseForm.Controls.Add(nuTimes);

                //不限制退费的数量，避免分解多天后，不能退费
                nuTimes.Maximum = 1000;
                nuTimes.Minimum = 0;

                if (cellClickEvent != null)
                {
                    baseForm.Text = cellClickEvent.View.Sheets[curRow].Cells[cellClickEvent.Row, (int)EnumOrderColumns.ItemName].Text;
                    nuTimes.Value = dcTimes;
                    //医嘱停止时间之前的不允许自动退费，这种情况，只能手动退费
                    //不限制退费次数，对于如果超出所有可退费数量时，不做处理
                    //nuTimes.Maximum = dcMaxTimes;
                    nuTimes.Focus();
                }
                baseForm.MaximizeBox = false;
                baseForm.MinimizeBox = false;
                baseForm.StartPosition = FormStartPosition.CenterScreen;
                baseForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btOK_Click(object sender, EventArgs e)
        {
            //FarPoint.Win.Spread.SheetView sv = this.fpSpread.Sheets[this.fpSpread.ActiveSheetIndex].GetChildView(this.fpSpread.Sheets[this.fpSpread.ActiveSheetIndex].ActiveRowIndex, 0);

            if (cellClickEvent != null)
            {
                string combNo = cellClickEvent.View.Sheets[curRow].Cells[cellClickEvent.Row, (int)EnumOrderColumns.CombNo].Text;

                for (int i = 0; i < cellClickEvent.View.Sheets[curRow].Rows.Count; i++)
                {
                    //嘱托、描述医嘱次数为0
                    if (cellClickEvent.View.Sheets[curRow].Cells[i, (int)EnumOrderColumns.OrderType].Tag != null && cellClickEvent.View.Sheets[curRow].Cells[i, (int)EnumOrderColumns.OrderType].Tag.ToString() == "NO")
                    {
                        continue;
                    }

                    if (cellClickEvent.View.Sheets[curRow].Cells[i, (int)EnumOrderColumns.CombNo].Text == combNo)
                    {
                        cellClickEvent.View.Sheets[curRow].Cells[cellClickEvent.Row, (int)EnumOrderColumns.MoOrderID].Tag =
                            ((((sender as FS.FrameWork.WinForms.Controls.NeuButton).Parent as Form).Controls[2]) as FS.FrameWork.WinForms.Controls.NeuNumericUpDown).Value;

                        cellClickEvent.View.Sheets[curRow].RowHeader.Rows[i].Label = "退" + cellClickEvent.View.Sheets[curRow].Cells[cellClickEvent.Row, (int)EnumOrderColumns.MoOrderID].Tag.ToString();
                    }
                }
            }
            ((sender as FS.FrameWork.WinForms.Controls.NeuButton).Parent as Form).Close();
        }

        /// <summary>
        /// 批注单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuTip_Click(object sender, EventArgs e)
        {
            ucTip ucTip1 = new ucTip();
            string OrderID = this.OrderId.Name;
            int iHypotest = CacheManager.InOrderMgr.QueryOrderHypotest(OrderID);
            if (iHypotest == -1)
            {
                MessageBox.Show(CacheManager.OutOrderMgr.Err);
                return;
            }
            #region 非药品医嘱不显示皮试页
            FS.HISFC.Models.Order.Order o = CacheManager.OutOrderMgr.QueryOneOrder(this.OrderId.Name);
            //if (o.Item.IsPharmacy == false)
            if (o.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                ucTip1.Hypotest = 1;
            }
            #endregion
            ucTip1.Tip = CacheManager.InOrderMgr.QueryOrderNote(OrderID);
            ucTip1.Hypotest = iHypotest;
            ucTip1.OKEvent += new myTipEvent(ucTip1_OKEvent);
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucTip1);
        }

        /// <summary>
        /// 修改执行科室事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuChangeDept_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.Order.Inpatient.Order order = CacheManager.OrderIntegrate.QueryOneOrder(this.OrderId.Name);
            FS.FrameWork.Models.NeuObject dept = ucChangeStoreDept.ChangeStoreDept(order);
            if (dept == null) return;
            order.StockDept = dept;
            if (CacheManager.InOrderMgr.UpdateOrder(order) < 0)
            {
                MessageBox.Show(CacheManager.OutOrderMgr.Err);
                return;
            }
        }

        /// <summary>
        /// 批注事件
        /// </summary>
        /// <param name="Tip"></param>
        /// <param name="Hypotest"></param>
        private void ucTip1_OKEvent(string Tip, int Hypotest)
        {
            if (CacheManager.InOrderMgr.UpdateFeedback(this.PatientId, this.OrderId.Name, Tip, Hypotest) == -1)
            {
                MessageBox.Show(CacheManager.OutOrderMgr.Err);
                CacheManager.OutOrderMgr.Err = "";
                return;
            }

            //SheetView sv=  this.fpSpread.ActiveSheet.GetChildView(this.fpSpread.ActiveSheet.ActiveRowIndex, 0);
            if (Hypotest == 3)
            {
                cellClickEvent.View.Sheets[curRow].Cells[cellClickEvent.Row, (int)EnumOrderColumns.Hypotest].Text = "阳性";
            }
            else if (Hypotest == 4)
            {
                cellClickEvent.View.Sheets[curRow].Cells[cellClickEvent.Row, (int)EnumOrderColumns.Hypotest].Text = "阴性";
            }

            cellClickEvent.View.Sheets[curRow].Cells[cellClickEvent.Row, (int)EnumOrderColumns.Tip].Text = Tip;
            FS.HISFC.Models.RADT.PatientInfo p = CacheManager.RadtIntegrate.GetPatientInfoByPatientNO(this.PatientId);
            RefreshView();
        }

        #endregion
    }

    /// <summary>
    /// 列设置
    /// </summary>
    public enum EnumOrderColumns
    {
        /// <summary>
        /// 患者住院流水号
        /// </summary>
        PatientID = 0,

        /// <summary>
        /// 医嘱流水号
        /// </summary>
        MoOrderID = 1,

        /// <summary>
        /// 组合号
        /// </summary>
        CombNo = 2,

        /// <summary>
        /// 审核
        /// </summary>
        CheckFlag = 3,

        /// <summary>
        /// 系统类别
        /// </summary>
        SysClassName = 4,

        /// <summary>
        /// 项目名称
        /// </summary>
        ItemName = 5,

        /// <summary>
        /// 组标记
        /// </summary>
        ConbFlag = 6,

        /// <summary>
        /// 规格
        /// </summary>
        Specs = 7,

        /// <summary>
        /// 每次量
        /// </summary>
        DoseOne = 8,

        /// <summary>
        /// 频次
        /// </summary>
        FrequencyID = 9,

        /// <summary>
        /// 用法名称
        /// </summary>
        UsageName = 10,

        /// <summary>
        /// 数量
        /// </summary>
        Qty = 11,

        /// <summary>
        /// 付数或首日量
        /// </summary>
        FuOrFirstDays = 12,

        /// <summary>
        /// 医嘱类型
        /// </summary>
        OrderType = 13,

        /// <summary>
        /// 加急标记
        /// </summary>
        EmergencyFlag = 14,

        /// <summary>
        /// 开始时间
        /// </summary>
        BeginDate = 15,

        /// <summary>
        /// 停止时间
        /// </summary>
        EndDate = 16,

        /// <summary>
        /// 开立医生
        /// </summary>
        RecipeDoctName = 17,

        /// <summary>
        /// 执行科室
        /// </summary>
        ExecDeptName = 18,

        /// <summary>
        /// 开立时间
        /// </summary>
        MoDate = 19,

        /// <summary>
        /// 停止医生
        /// </summary>
        DcDoctName = 20,

        /// <summary>
        /// 备注
        /// </summary>
        Memo = 21,

        /// <summary>
        /// 顺序号
        /// </summary>
        SortID = 22,

        /// <summary>
        /// 批注
        /// </summary>
        Tip = 23,

        /// <summary>
        /// 医嘱状态
        /// </summary>
        MoState = 24,

        /// <summary>
        /// 皮试
        /// </summary>
        Hypotest = 25
    }
}
