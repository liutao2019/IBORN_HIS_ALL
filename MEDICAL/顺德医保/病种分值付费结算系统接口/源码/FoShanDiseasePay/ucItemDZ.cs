using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Collections;
using FS.HISFC.Models.Fee;
using FS.FrameWork.WinForms.Forms;
using FS.HISFC.BizProcess.Integrate.FeeInterface;
using FS.HISFC.Models.Base;
using FS.FrameWork.Function;
using FoShanDiseasePay.DataBase;
using FoShanDiseasePay.Jobs;
using System.Text.RegularExpressions;

namespace FoShanDiseasePay
{
    /// <summary>
    /// 项目对照维护
    /// </summary>
    public partial class ucItemDZ : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ucItemDZ()
        {
            InitializeComponent();


        }

        #region 事件
        /// 工具栏
        /// </summary>
        private ToolBarService toolBarService = new ToolBarService();

        FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 患者入出转管理
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();


        FS.HISFC.Models.RADT.PatientInfo inPatientInfo = null;

        /// <summary>
        /// 项目帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper itemInfoHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 项目信息
        /// </summary>
        ArrayList al = new ArrayList();

        /// <summary>
        /// 医保项目编码对照：0国标码；1自定义码；默认为0国标码
        /// </summary>
        private string itemCodeCompareType = "0";
        /// <summary>
        /// 控制参数业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        FoShanDiseasePay.BizLogic.OutManager oMgr = new FoShanDiseasePay.BizLogic.OutManager();
        /// <summary>
        /// 页面加载事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            //清屏
            this.Clear();
            al = oMgr.QueryItemInfo();
            if (al == null)
            {
                MessageBox.Show("加载项目信息失败！");
            }
            else
            {
                itemInfoHelper.ArrayObject = al;
                this.cmbItemInfo.AddItems(al);
            }

            this.itemCodeCompareType = this.controlParamIntegrate.GetControlParam<string>("FSMZ16", false, "0");
            base.OnLoad(e);
        }


        /// <summary>
        /// 增加ToolBar控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {

            //this.toolBarService.AddToolButton("待上传患者", "待上传患者查询", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);
            //this.toolBarService.AddToolButton("一键上传", "一键上传病种分值付费接口", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);

       

            return this.toolBarService;
        }

        /// <summary>
        /// 定义toolbar按钮click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //switch (e.ClickedItem.Text)
            //{
            //    case "待上传患者":
            //        this.Clear();
            //        break;
            //}

            base.ToolStrip_ItemClicked(sender, e);
        }

        private string strClNo = "";
        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            this.cmbItemInfo.Tag = "";
            this.cmbItemInfo.Text = "";
            this.lblSBH.Text = "";
        }
        protected override int OnQuery(object sender, object neuObject)
        {
            //this.Clear();
            string itemCode = this.cmbItemInfo.Tag.ToString();
            string itemName = this.cmbItemInfo.Text;
            if (string.IsNullOrEmpty(itemName.Trim()) || string.IsNullOrEmpty(itemCode))
            {
                itemCode = "ALL";
            }
            DataTable al = this.oMgr.GetItemInfo(itemCode);
            this.ToFP(al);
            //this.fpHaveUploaded
            return base.OnQuery(sender, neuObject);
        }

        private void ToFP(DataTable al)
        {
            this.fpItemInfo_Sheet1.Rows.Count = 0;

            int rowIndex = this.fpItemInfo_Sheet1.Rows.Count;
            if (al != null && al.Rows.Count > 0)
            {
                foreach (DataRow dRow in al.Rows)
                {

                    this.fpItemInfo_Sheet1.Rows.Add(rowIndex, 1);
                    this.fpItemInfo_Sheet1.Cells[rowIndex, (int)ColItemInfo.ColItemCode].Value = dRow[(int)ColItemInfo.ColItemCode].ToString();
                    this.fpItemInfo_Sheet1.Cells[rowIndex, (int)ColItemInfo.ColInputCode].Value = dRow[(int)ColItemInfo.ColInputCode].ToString();
                    this.fpItemInfo_Sheet1.Cells[rowIndex, (int)ColItemInfo.ColGBCode].Value = dRow[(int)ColItemInfo.ColGBCode].ToString();
                    this.fpItemInfo_Sheet1.Cells[rowIndex, (int)ColItemInfo.ColSBCode].Value = dRow[(int)ColItemInfo.ColSBCode].ToString();//社保项目编码
                    this.fpItemInfo_Sheet1.Cells[rowIndex, (int)ColItemInfo.ColItemName].Value = dRow[(int)ColItemInfo.ColItemName].ToString();
                    this.fpItemInfo_Sheet1.Cells[rowIndex, (int)ColItemInfo.ColSpecs].Value = dRow[(int)ColItemInfo.ColSpecs].ToString();
                    this.fpItemInfo_Sheet1.Cells[rowIndex, (int)ColItemInfo.ColPrice].Value = dRow[(int)ColItemInfo.ColPrice].ToString();//基准价格
                    this.fpItemInfo_Sheet1.Cells[rowIndex, (int)ColItemInfo.ColUnit].Value = dRow[(int)ColItemInfo.ColUnit].ToString();
                    this.fpItemInfo_Sheet1.Cells[rowIndex, (int)ColItemInfo.ColApproveInfo].Value = dRow[(int)ColItemInfo.ColApproveInfo].ToString();//批准文号
                    this.fpItemInfo_Sheet1.Cells[rowIndex, (int)ColItemInfo.ColProducerName].Value = dRow[(int)ColItemInfo.ColProducerName].ToString();
                    rowIndex++;
                }
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            if (this.cmbItemInfo.Tag == null || string.IsNullOrEmpty(this.cmbItemInfo.Text))
            {
                MessageBox.Show("请选择本院项目！");
                return -1;
            }
            if (string.IsNullOrEmpty(this.lblSBH.Text.Trim()))
            {
                MessageBox.Show("请输入社保项目编码！");
                return -1;

            }
            if(al == null)
            {
                MessageBox.Show("项目列表为空，未加载数据！");
                return -1;
            }
            FS.HISFC.Models.Base.Const itemInfo = new Const();
            foreach(FS.HISFC.Models.Base.Const obj in al)
            {
                if (this.cmbItemInfo.Tag.ToString() == obj.ID)
                {
                    itemInfo = obj;
                    break;
                }
            }
            string type = itemInfo.Memo;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            con.SetTrans(FS.FrameWork.Management.PublicTrans.Trans); //设置事务

            if (InsertCompareInfo(type, itemInfo.ID, this.lblSBH.Text.Trim()) <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("插入FIN_COM_COMPARE信息失败！" + this.con.Err);
                return -1;

            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("对照成功！");
            return base.OnSave(sender, neuObject);
        }


        private int InsertCompareInfo(string type, string itemCode,string sbh)
        {
            if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(itemCode) || string.IsNullOrEmpty(sbh))
            {
                this.con.Err = "项目信息为空！";
                return -1;

            }
            // {B7DA1304-1FCB-4c01-B96A-1BEABD23F79C}
            string sql = "";
            if (type == "1")
            {
                if (itemCodeCompareType == "0")
                {
                    sql = @"insert into FIN_COM_COMPARE
                    (select '99',a.gb_code,a.item_name,'','{0}','',a.item_name,'',a.specs ,
                    '','','','','','',a.unit_price2,'','','','','','','','',sysdate,'','','','',''from fin_com_undruginfo a  where a.item_code= '{1}')";
               
                }
                else
                {
                    sql = @"insert into FIN_COM_COMPARE
                    (select '99',a.input_code,a.item_name,'','{0}','',a.item_name,'',a.specs ,
                    '','','','','','',a.unit_price2,'','','','','','','','',sysdate,'','','','',''from fin_com_undruginfo a  where a.item_code= '{1}')";
                }
            }
            else if (type == "2")
            {
                if (itemCodeCompareType == "0")
                {
                    sql = @"insert into FIN_COM_COMPARE
                    (select '99',a.gb_code,a.trade_name,'','{0}','',a.trade_name,'',a.specs ,
                    '','','','','','',a.wholesale_price,'','','','','','','','',sysdate,'','','','',''from pha_com_baseinfo a  where a.drug_code= '{1}')
                    ";
                }
                else
                {
                    sql = @"insert into FIN_COM_COMPARE
                    (select '99',a.custom_code,a.trade_name,'','{0}','',a.trade_name,'',a.specs ,
                    '','','','','','',a.wholesale_price,'','','','','','','','',sysdate,'','','','',''from pha_com_baseinfo a  where a.drug_code= '{1}')
                    ";
                }

            }

            sql = string.Format(sql, sbh, itemCode);


            return this.con.ExecNoQuery(sql);
        }
        #endregion


        /// <summary>
        /// 项目信息
        /// </summary>
        private enum ColItemInfo
        {
            /// <summary>
            /// 项目编码
            /// </summary>
            ColItemCode = 0,
            /// <summary>
            /// 自定义码
            /// </summary>
            ColInputCode = 1,

            /// <summary>
            /// 国家编码
            /// </summary>
            ColGBCode = 2,

            /// <summary>
            /// 社保项目编码
            /// </summary>
            ColSBCode = 3,

            /// <summary>
            /// 项目名称
            /// </summary>
            ColItemName = 4,

            /// <summary>
            /// 规格
            /// </summary>
            ColSpecs = 5,

            /// <summary>
            /// 基准价格
            /// </summary>
            ColPrice = 6,

            /// <summary>
            /// 单位
            /// </summary>
            ColUnit = 7,

            /// <summary>
            /// 批准文号
            /// </summary>
            ColApproveInfo = 8,

            /// <summary>
            /// 生产厂家
            /// </summary>
            ColProducerName = 9

        }
    }
}
