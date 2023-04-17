using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.Pharmacy.Common.Check
{
    /// <summary>
    /// [功能描述: 静态盘点]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-04]<br></br>
    /// </summary>
    public partial class ucStaticCheck : ucBaseCheck, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucStaticCheck()
        {
            InitializeComponent();
        }

        #region 属性
        private int curDays = 90;

        /// <summary>
        /// CurDays天内无入出库业务的库存为0的不封账
        /// </summary
        [Description("CurDays天内无入出库业务的库存为0的不封账"), Category("设置"), Browsable(true)]
        public int Days
        {
            get { return curDays; }
            set { curDays = value; }
        }

        #endregion

        #region 权限处理

        /// <summary>
        /// 设置权限科室
        /// </summary>
        /// <returns></returns>
        private int SetPriveDept()
        {
            FS.FrameWork.Models.NeuObject priveDept = new FS.FrameWork.Models.NeuObject();
            int param = Function.ChoosePriveDept("0305", ref priveDept);
            if (param == 0 || param == -1 || priveDept == null || string.IsNullOrEmpty(priveDept.ID))
            {
                return -1;
            }

            this.nlbPriveDept.Text = "您选择的科室是【" + priveDept.Name + "】";

            this.curStockDept = priveDept;

            return 1;
        }

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

        #region 封账

        protected int FStorage()
        {
            if (!this.CheckFStorePrive())
            {
                Function.ShowMessage("您没有权限！", MessageBoxIcon.Information);
                return 0;
            }
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("请稍后...");
            Application.DoEvents();

            object interfaceImplement = InterfaceManager.GetExtendBizImplement();

            //没有接口实现（当然也可能是业务扩展本地化有问题），则不处理扩展业务
            if (interfaceImplement == null)
            {
                return DefaultFStorage();
            }

            ArrayList alDetail = ((FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IPharmacyBizExtend)interfaceImplement).SetCheckDetail(this.curStockDept.ID);
            if (alDetail == null)
            {

                Function.ShowMessage("封账失败，错误信息：请确认是否完成所有入出库业务！", MessageBoxIcon.Information);
                return 0;
                //return DefaultFStorage();
            }
            

            this.curIDataDetail.FpSpread.DataSource = null;
            this.dtDetail.Clear();
            this.dtDetail.AcceptChanges();
            this.hsCheck.Clear();

            foreach (FS.HISFC.Models.Pharmacy.Check check in alDetail)
            {
                this.curCheckBillNO = check.CheckNO;

                if (this.AddObjectToDataTable(check) == -1)
                {
                    continue;
                }
            }
            this.curIDataDetail.FpSpread.DataSource = this.dtDetail.DefaultView;
            this.SetTotInfoAndColor();
            this.dtDetail.AcceptChanges();

            this.curIDataDetail.FpSpread.ReadSchema(this.detailDataSettingFileName);

            this.ShowList();

            Function.ShowMessage("封账完成!", MessageBoxIcon.Information);
            return 1;
        }

        protected int DefaultFStorage()
        {
            List<FS.HISFC.Models.Pharmacy.Check> checkList = this.itemMgr.QueryCheckList(this.curStockDept.ID, "0", "ALL");
            if (checkList == null)
            {
                Function.ShowMessage("获取封账列表发生错误，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                return -1;
            }
            if (checkList.Count > 0)
            {
                Function.ShowMessage("已经存在盘点单，不可以再封账!", MessageBoxIcon.Information);
                return 0;
            }

//            string SQL = @" select count(*) 
//                            from   pha_com_output o 
//                            where  o.drug_storage_code = '{0}'
//                            and    o.out_state <> '2'";

            
//            string count = this.itemMgr.ExecSqlReturnOne(string.Format(SQL,this.curStockDept.ID));

//            if (count == "-1")
//            {
//                Function.ShowMessage("检查入库情况发生错误，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
//                return -1;
//            }

//            if (FS.FrameWork.Function.NConvert.ToInt32(count) > 0)
//            {
//                Function.ShowMessage("还存在没有入库的药品，目前不可以封账，请【核准入库】！", MessageBoxIcon.Information);
//                return 0;
//            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            DialogResult dr = MessageBox.Show("封账开始前，请通知库房所有人员停止出入库、调价等其他业务操作\n\n确认封账吗?", "提示>>", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel)
            {
                return 0;
            }
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("请稍候...");
            Application.DoEvents();

            List<FS.HISFC.Models.Pharmacy.Item> alItem = this.itemMgr.QueryItemList();
            if (alItem == null)
            {
                Function.ShowMessage("获取药品基本信息发生错误，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                return -1;
            }

            Hashtable hsItem = new Hashtable();
            foreach (FS.HISFC.Models.Pharmacy.Item item in alItem)
            {
                hsItem.Add(item.ID, item);
            }

            //对所有药品进行封帐处理
            curFOperTime = this.itemMgr.GetDateTimeFromSysDateTime();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            FS.SOC.HISFC.BizLogic.Pharmacy.Constant phaConstantMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Constant();
            bool isManageBatch = phaConstantMgr.IsManageBatch(this.curStockDept.ID);

            FS.SOC.HISFC.BizLogic.Pharmacy.Check checkMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Check();           

            //ArrayList alDetail = this.itemMgr.CheckCloseByTotal(this.curStockDept.ID, false, true, true);
            ArrayList alDetail = checkMgr.CloseAll(this.curStockDept.ID, isManageBatch, this.Days);

            if (alDetail == null)
            {
                Function.ShowMessage("封账发生错误，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                return -1;
            }

            if (alDetail.Count == 0)
            {
                Function.ShowMessage("没有库存!", MessageBoxIcon.Information);
                return 0;
            }

            //获取新盘点单号
            string checkNO = this.itemMgr.GetCheckCode(this.curStockDept.ID);
            if (string.IsNullOrEmpty(checkNO))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                Function.ShowMessage("获取盘点单号发生错误，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                return -1;
            }

            //保存新生成的盘点单号
            this.curCheckBillNO = checkNO;

            FS.HISFC.Models.Pharmacy.Check checkStatic = new FS.HISFC.Models.Pharmacy.Check();

            checkStatic.CheckNO = checkNO;				            //盘点单号
            checkStatic.StockDept = this.curStockDept;			        //库房编码
            checkStatic.State = "0";					            //封帐状态
            checkStatic.User01 = "0";						        //盘亏金额
            checkStatic.User02 = "0";						        //盘盈金额

            checkStatic.FOper.ID = this.itemMgr.Operator.ID;   //封帐人
            checkStatic.FOper.OperTime = this.curFOperTime;				    //封帐时间
            checkStatic.Operation.Oper = checkStatic.FOper;               //操作人

            if (this.itemMgr.InsertCheckStatic(checkStatic) != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                Function.ShowMessage("插入盘点汇总信息发生错误，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                return -1;
            }

            this.curIDataDetail.FpSpread.DataSource = null;
            this.dtDetail.Clear();
            this.dtDetail.AcceptChanges();
            this.hsCheck.Clear();

            foreach (FS.HISFC.Models.Pharmacy.Check checkDetail in alDetail)
            {
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
                if (this.itemMgr.InsertCheckDetail(checkDetail) != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.curIDataDetail.FpSpread.DataSource = this.dtDetail.DefaultView;
                    Function.ShowMessage("插入盘点明细信息发生错误，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }

                //if (this.AddObjectToDataTable(checkDetail) == -1)
                //{
                //    continue;
                //}
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            this.curIDataDetail.FpSpread.DataSource = this.dtDetail.DefaultView;
            this.SetTotInfoAndColor();
            this.dtDetail.AcceptChanges();

            this.curIDataDetail.FpSpread.ReadSchema(this.detailDataSettingFileName);

            this.ShowList();

            Function.ShowMessage("封账完成!", MessageBoxIcon.Information);

            return 1;
        }

        #endregion

        #region 工具栏
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("封账", "封锁库存账务", FS.FrameWork.WinForms.Classes.EnumImageList.F封帐, true, false, null);

            return base.OnInit(sender, neuObject, param);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "封账")
            {
                this.FStorage();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion
    }
}
