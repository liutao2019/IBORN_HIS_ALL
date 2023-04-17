using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.DrugStore.Compound
{
    public partial class ucCompoundLableQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCompoundLableQuery()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 药品业务类
        /// </summary>
        private FS.SOC.HISFC.BizLogic.Pharmacy.Compound compoundMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Compound();

        /// <summary>
        /// 存查询出的配液明细信息
        /// </summary>
        private System.Collections.Hashtable hsComDetail = new Hashtable();

        #endregion

        #region 属性

        #endregion

        #region 工具栏信息

        /// <summary>
        /// 定义工具栏服务
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 初始化工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="NeuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object NeuObject, object param)
        {
            //增加工具栏
            this.toolBarService.AddToolButton("全选", "选择全部申请", FS.FrameWork.WinForms.Classes.EnumImageList.Q全选, true, false, null);
            this.toolBarService.AddToolButton("全不选", "取消全部申请选择", FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);
            //this.toolBarService.AddToolButton("刷新", "刷新患者列表显示", FS.FrameWork.WinForms.Classes.EnumImageList.A刷新, true, false, null);

            return this.toolBarService;
        }

        /// <summary>
        /// 工具栏按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "全选":
                    this.Check(true);
                    break;
                case "全不选":
                    this.Check(false);
                    break;
                //case "刷新":
                //    this.ShowList();
                //    break;
            }

        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化界面
        /// </summary>
        private void Init()
        {
            #region 给查询类型赋值

            FS.FrameWork.Models.NeuObject obj1 = new FS.FrameWork.Models.NeuObject();
            obj1.ID = "D";
            obj1.Name = "摆药单";
            cmbType.Items.Add(obj1);
            FS.FrameWork.Models.NeuObject obj2 = new FS.FrameWork.Models.NeuObject();
            obj2.ID = "C";
            obj2.Name = "批次号";
            cmbType.Items.Add(obj2);
            cmbType.SelectedIndex = 0;

            #endregion

            #region 初 始 化 药 品 类 标 列 表

            //{3FCCCB14-B8D7-4c63-A3C6-0AD30BA21782} 静配中心按照药品扩展属性分类摆药
            FS.HISFC.BizLogic.Manager.Constant csMager = new FS.HISFC.BizLogic.Manager.Constant();
            ArrayList alDrugTypeList = csMager.GetList("DrugExtType");

            FS.FrameWork.Models.NeuObject nObj = new FS.FrameWork.Models.NeuObject();
            nObj.ID = "ALL";
            nObj.Name = "--全部--";
            alDrugTypeList.Insert(0, nObj);

            this.neuCmbDrugType.AddItems(alDrugTypeList);
            this.neuCmbDrugType.SelectedIndex = 0;

            #endregion

            DateTime systime = FS.FrameWork.Function.NConvert.ToDateTime(this.compoundMgr.GetSysDateTime());

            this.dtStart.Text = systime.ToString("yyyy-MM-dd 00:00:00");
            this.dtEnd.Text = systime.ToString("yyyy-MM-dd 23:59:59");
            this.fpCompoundSelect_Sheet1.Columns.Get(0).Visible = false;

        }

        /// <summary>
        /// 查询一段时间内的批次信息
        /// </summary>
        private ArrayList QueryCompoundBath()
        {

            FS.FrameWork.Models.NeuObject obj = ((FS.HISFC.Models.Base.Employee)this.compoundMgr.Operator).Dept;
            string[] parm;

            if (this.ckChange.Checked)
            {
                if (this.cmbType.SelectedItem.ToString() == "摆药单")
                {
                    parm = new string[] { "A", this.txtCardNo.Text.Trim(), "A", "A", "A", "2", "Z1" };
                }
                else
                {
                    parm = new string[] { "A", "A", "A", "A", this.txtCardNo.Text.Trim(), "2", "Z1" };
                }
            }
            else
            {
                parm = new string[] { obj.ID, "A", this.dtStart.Text, this.dtEnd.Text, "A", "2", "Z1" };
            }

            ArrayList alComp = this.compoundMgr.QueryCompoundInfoByParm(parm);

            return alComp;
        }

        /// <summary>
        /// 添加批次信息
        /// </summary>
        private void AddDateToComSelect()
        {
            ArrayList alComSelect = this.QueryCompoundBath();

            //this.fpCompoundSelect_Sheet1.Columns.Get(1).MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;
            this.fpCompoundSelect_Sheet1.Rows.Count = 0;

            if (alComSelect == null || alComSelect.Count == 0)
            {
                return;
            }

            for (int i = 0; i < alComSelect.Count; i++)
            {
                FS.FrameWork.Models.NeuObject obj=alComSelect[i] as FS.FrameWork.Models.NeuObject;

                this.fpCompoundSelect_Sheet1.RowCount++;

                this.fpCompoundSelect_Sheet1.Cells[i, 0].Text = obj.ID;//科室编码
                this.fpCompoundSelect_Sheet1.Cells[i, 1].Text = obj.Name;//科室名称
                this.fpCompoundSelect_Sheet1.Cells[i, 2].Value = false;//选择
                this.fpCompoundSelect_Sheet1.Cells[i, 3].Text = obj.Memo;//批次号
                this.fpCompoundSelect_Sheet1.Cells[i, 4].Text = obj.User01;//用药时间
            }

            this.fpCompoundSelect_Sheet1.Columns.Get(0).Visible = false;

        }

        /// <summary>
        /// 向明细表中添加数据
        /// </summary>
        /// <param name="alApply">查询结果数据</param>
        /// <returns></returns>
        private int AddDateToComDetial(ArrayList alApply)
        {
            int i = 0;

            if (this.fpCompoundDetial_Sheet1.Rows.Count > 1)
            {
                i = this.fpCompoundDetial_Sheet1.Rows.Count;
            }
            else if (this.fpCompoundDetial_Sheet1.Rows.Count == 1)
            {
                i = 1;
            }

            FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alApply)
            {

                this.fpCompoundDetial_Sheet1.Rows.Add(i, 1);

                if (info.UseTime != System.DateTime.MinValue)
                {
                    this.fpCompoundDetial_Sheet1.Cells[i, (int)ColumnSet.ColUseTime].Text = info.UseTime.ToString();
                }

                if (info.PatientNO.Length > 7)
                {
                    this.fpCompoundDetial_Sheet1.Cells[i, (int)ColumnSet.ColBedName].Text = "[" + info.PatientNO.Substring(7) + "]" + info.User02;
                }
                else
                {
                    this.fpCompoundDetial_Sheet1.Cells[i, (int)ColumnSet.ColBedName].Text = "[" + info.PatientNO + "]" + info.User02;
                }

                this.fpCompoundDetial_Sheet1.Cells[i, (int)ColumnSet.ColTradeNameSpecs].Text = info.Item.Name + "[" + info.Item.Specs + "]";
                this.fpCompoundDetial_Sheet1.Cells[i, (int)ColumnSet.ColRetailPrice].Text = info.Item.PriceCollection.RetailPrice.ToString();
                this.fpCompoundDetial_Sheet1.Cells[i, (int)ColumnSet.ColDoseOnce].Text = info.DoseOnce.ToString() + " " + info.Item.DoseUnit;
                this.fpCompoundDetial_Sheet1.Cells[i, (int)ColumnSet.ColQty].Text = (info.Operation.ApplyQty * info.Days).ToString() + " " + info.Item.MinUnit;
                this.fpCompoundDetial_Sheet1.Cells[i, (int)ColumnSet.ColFrequency].Text = info.Frequency.ID;

                this.fpCompoundDetial_Sheet1.Cells[i, (int)ColumnSet.ColComboNO].Text = info.CombNO + info.UseTime.ToString();//组合编号+用药时间

                //if (this.isOutCompoudn == false)
                //{
                //    this.fpCompoundDetial_Sheet1.Cells[i, (int)ColumnSet.ColComboNO].Text = info.CombNO + info.UseTime.ToString();//组合编号+用药时间
                //}
                //else
                //{
                //    //给门诊用的 {F0F32E8B-6E72-4c8e-87DD-7149CDFE8935}
                //    this.fpCompoundDetial_Sheet1.Cells[i, (int)ColumnSet.ColComboNO].Text = info.CombNO;//组合编号
                //}

                this.fpCompoundDetial_Sheet1.Cells[i, (int)ColumnSet.ColUsage].Text = info.Usage.Name;
                this.fpCompoundDetial_Sheet1.Cells[i, (int)ColumnSet.ColUseTime].Text = info.UseTime.ToString();

                //{A428F1DE-9061-4ee6-87BA-93FC41D8BEA0}
                this.fpCompoundDetial_Sheet1.Cells[i, (int)ColumnSet.ColDoctor].Text = info.RecipeInfo.Name + "-" + info.RecipeInfo.ID;
                this.fpCompoundDetial_Sheet1.Cells[i, (int)ColumnSet.ColApplyTime].Text = info.Operation.ApplyOper.OperTime.ToString();
                this.fpCompoundDetial_Sheet1.Cells[i, (int)ColumnSet.ColCompoundGroup].Text = info.CompoundGroup;
                this.fpCompoundDetial_Sheet1.Cells[i, (int)ColumnSet.ColDrugBill].Text = info.DrugNO;
                this.fpCompoundDetial_Sheet1.Rows[i].Tag = info;
 
            }

            this.DrawCombo(fpCompoundDetial_Sheet1, (int)ColumnSet.ColComboNO, (int)ColumnSet.ColCombo);

            this.fpCompoundDetial_Sheet1.Columns.Get((int)ColumnSet.ColComboNO).Visible = false;

            return 1;
        }

        /// <summary>
        /// 删除没有勾选的信息
        /// </summary>
        /// <returns></returns>
        private void RemoveDateToComDetial()
        {
            for (int i = 0; i < this.fpCompoundDetial_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut apply = this.fpCompoundDetial_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;

                if (hsComDetail.ContainsKey(apply.CompoundGroup))
                {
                    this.fpCompoundDetial_Sheet1.Rows.Remove(i, FS.FrameWork.Function.NConvert.ToInt32(hsComDetail[apply.CompoundGroup].ToString()));
                }
            }
        }

        /// <summary>
        /// 查询配液明细
        /// </summary>
        /// <returns></returns>
        private ArrayList QieryCompoundDetail(string compoundGroup)
        {
            FS.FrameWork.Models.NeuObject obj = ((FS.HISFC.Models.Base.Employee)this.compoundMgr.Operator).Dept;

            ArrayList alDetail = this.compoundMgr.QueryCompoundApplyOutForJP(compoundGroup, false, obj.ID, "Z1");

            return alDetail;
        }

        /// <summary>
        /// 获取所有当前选中的数据
        /// </summary>
        /// <returns></returns>
        internal ArrayList GetCheckData()
        {
            ArrayList al = new ArrayList();

            for (int i = 0; i < this.fpCompoundDetial_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut apply = this.fpCompoundDetial_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;

                //apply.CompoimdStore = this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColCompoundStore].Text;

                al.Add(apply);
            }

            return al;
        }

        private int Print()
        {
            ArrayList alOriginal = this.GetCheckData();

            #region 按患者排序

            CompareApplyOut com = new CompareApplyOut();
            alOriginal.Sort(com);

            #endregion

            //这个功能专门摆药以及审核使用，标签不在此打印
            //{10DE0921-42E5-4bfd-999F-F00839D7D777}
            //return Function.PrintCompound(alOriginal, true, true);
            return 1;
        }

        /// <summary>
        /// 选中/不选中
        /// </summary>
        /// <param name="isCheck"></param>
        /// <returns></returns>
        public int Check(bool isCheck)
        {
            if (this.fpCompoundSelect_Sheet1.Rows.Count == 0)
            {
                return 0;
            }
            List<string> listComp = new List<string>();
            this.fpCompoundSelect.ButtonClicked -= new FarPoint.Win.Spread.EditorNotifyEventHandler(fpCompoundSelect_ButtonClicked);
            for (int i = 0; i < this.fpCompoundSelect_Sheet1.Rows.Count; i++)
            {
                listComp.Add(this.fpCompoundSelect_Sheet1.Cells[i, 3].Text);
                this.fpCompoundSelect_Sheet1.Cells[i, 2].Value = isCheck;
            }
            this.fpCompoundSelect.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpCompoundSelect_ButtonClicked);

            ArrayList alTotalDetail = new ArrayList();
            if (isCheck)
            {
                for (int irow = 0; irow < listComp.Count; irow++)
                {
                    ArrayList alDetail = this.QieryCompoundDetail(listComp[irow].ToString());
                    alTotalDetail.InsertRange(0, alDetail);//.AddRange(alDetail);
                }
                //alTotalDetail.Sort(new NoSort());
                this.AddDateToComDetial(alTotalDetail);
            }
            else
            {
                this.fpCompoundDetial_Sheet1.Rows.Count = 0;
            }

            return 1;
        }

        #region 组合医嘱 传入的对象，column 组合项目列
        public void DrawCombo(object sender, int column, int DrawColumn, int ChildViewLevel)
        {
            switch (sender.GetType().ToString().Substring(sender.GetType().ToString().LastIndexOf(".") + 1))
            {
                case "SheetView":
                    FarPoint.Win.Spread.SheetView o = sender as FarPoint.Win.Spread.SheetView;
                    int i = 0;
                    string tmp = "", curComboNo = "";
                    if (ChildViewLevel == 0)
                    {
                        for (i = 0; i < o.RowCount; i++)
                        {
                            #region "画"
                            if (o.Cells[i, column].Text == "0")
                                o.Cells[i, column].Text = "";
                            tmp = o.Cells[i, column].Text + "";
                            o.Cells[i, column].Tag = tmp;
                            if (curComboNo != tmp && tmp != "") //是头
                            {
                                curComboNo = tmp;
                                o.Cells[i, DrawColumn].Text = "┏";
                                try
                                {
                                    if (o.Cells[i - 1, DrawColumn].Text == "┃")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "┗";
                                    }
                                    else if (o.Cells[i - 1, DrawColumn].Text == "┏")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "";
                                    }
                                }
                                catch
                                {
                                }
                            }
                            else if (curComboNo == tmp && tmp != "")
                            {
                                //{F46C0059-9EF3-4f04-A7A7-6401455D9631}
                                if (curComboNo != "0001-1-1 0:00:00")
                                {
                                    o.Cells[i, DrawColumn].Text = "┃";
                                }
                                else
                                {
                                    o.Cells[i - 1, DrawColumn].Text = "";
                                }
                            }
                            else if (curComboNo != tmp && tmp == "")
                            {
                                try
                                {
                                    if (o.Cells[i - 1, DrawColumn].Text == "┃")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "┗";
                                    }
                                    else if (o.Cells[i - 1, DrawColumn].Text == "┏")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "";
                                    }
                                }
                                catch
                                {
                                }
                                o.Cells[i, DrawColumn].Text = "";
                                curComboNo = "";
                            }
                            if (i == o.RowCount - 1 && o.Cells[i, DrawColumn].Text == "┃")
                                o.Cells[i, DrawColumn].Text = "┗";
                            if (i == o.RowCount - 1 && o.Cells[i, DrawColumn].Text == "┏")
                                o.Cells[i, DrawColumn].Text = "";
                            //o.Cells[i, DrawColumn].ForeColor = System.Drawing.Color.Red;
                            #endregion
                        }
                    }
                    else if (ChildViewLevel == 1)
                    {
                        for (int m = 0; m < o.RowCount; m++)
                        {
                            FarPoint.Win.Spread.SheetView c = o.GetChildView(m, 0);
                            for (int j = 0; j < c.RowCount; j++)
                            {
                                #region "画"
                                if (c.Cells[j, column].Text == "0")
                                    c.Cells[j, column].Text = "";
                                tmp = c.Cells[j, column].Text + "";

                                c.Cells[j, column].Tag = tmp;
                                if (curComboNo != tmp && tmp != "") //是头
                                {
                                    curComboNo = tmp;
                                    c.Cells[j, DrawColumn].Text = "┏";
                                    try
                                    {
                                        if (c.Cells[j - 1, DrawColumn].Text == "┃")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "┗";
                                        }
                                        else if (c.Cells[j - 1, DrawColumn].Text == "┏")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "";
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }
                                else if (curComboNo == tmp && tmp != "")
                                {
                                    c.Cells[j, DrawColumn].Text = "┃";
                                }
                                else if (curComboNo != tmp && tmp == "")
                                {
                                    try
                                    {
                                        if (c.Cells[j - 1, DrawColumn].Text == "┃")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "┗";
                                        }
                                        else if (c.Cells[j - 1, DrawColumn].Text == "┏")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "";
                                        }
                                    }
                                    catch
                                    {
                                    }
                                    c.Cells[j, DrawColumn].Text = "";
                                    curComboNo = "";
                                }
                                if (j == c.RowCount - 1 && c.Cells[j, DrawColumn].Text == "┃")
                                    c.Cells[j, DrawColumn].Text = "┗";
                                if (j == c.RowCount - 1 && c.Cells[j, DrawColumn].Text == "┏")
                                    c.Cells[j, DrawColumn].Text = "";
                                //c.Cells[j, DrawColumn].ForeColor = System.Drawing.Color.Red;
                                #endregion

                            }
                        }
                    }
                    break;
            }

        }

        /// <summary>
        /// 画组合号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="column"></param>
        /// 		/// <param name="DrawColumn"></param>
        public void DrawCombo(object sender, int column, int DrawColumn)
        {
            DrawCombo(sender, column, DrawColumn, 0);
        }

        #endregion

        #endregion

        #region 事件

        protected override void OnLoad(EventArgs e)
        {
            this.Init();

            base.OnLoad(e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.AddDateToComSelect();

            this.fpCompoundDetial_Sheet1.Rows.Count = 0;

            return base.OnQuery(sender, neuObject);
        }

        private void ckChange_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckChange.Checked)
            {
                this.lblDrugBill.Enabled = true;
                this.cmbType.Enabled = true;
                this.txtCardNo.Enabled = true;
                this.dtStart.Enabled = false;
                this.dtEnd.Enabled = false;
            }
            else
            {
                this.lblDrugBill.Enabled = false;
                this.cmbType.Enabled = false;
                this.txtCardNo.Enabled = false;
                this.dtStart.Enabled = true;
                this.dtEnd.Enabled = true;
            }
        }

        private void fpCompoundSelect_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            int index = this.fpCompoundSelect_Sheet1.ActiveRowIndex;
            string compoundGroup = this.fpCompoundSelect_Sheet1.Cells[index, 3].Text;
            ArrayList alComDetail = this.QieryCompoundDetail(compoundGroup);

            if (alComDetail == null || alComDetail.Count == 0)
            {
                return;
            }

            this.hsComDetail.Clear();

            for (int i = 0; i < alComDetail.Count; i++)
            {

                if (!hsComDetail.ContainsKey(this.fpCompoundSelect_Sheet1.Cells[e.Row, 3].Text))
                {
                    hsComDetail.Add(this.fpCompoundSelect_Sheet1.Cells[e.Row, 3].Text, alComDetail.Count);
                }
            }

            if (e.Column == 2)
            {
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.fpCompoundSelect_Sheet1.Cells[e.Row, 2].Value))
                {
                    this.AddDateToComDetial(alComDetail);
                }
                else
                {
                    RemoveDateToComDetial();
                }
            }
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            //this.Print();

            return base.OnPrint(sender, neuObject);
        }

        #endregion

        #region 数组列

        /// <summary>
        /// 列设置
        /// </summary>
        private enum ColumnSet
        {
            /// <summary>
            /// 科室名称
            /// </summary>
            //ColDepartMent,
            /// <summary>
            /// 批次号
            /// </summary>
            ColCompoundGroup,
            /// <summary>
            /// 摆药单号
            /// </summary>
            ColDrugBill,
            /// <summary>
            /// 住院号 姓名
            /// </summary>
            ColBedName,
            /// <summary>
            /// 选中
            /// </summary>
            //ColSelect,
            /// <summary>
            /// 组合
            /// </summary>
            ColCombo,
            /// <summary>
            /// 药品名称 规格
            /// </summary>
            ColTradeNameSpecs,
            /// <summary>
            /// 零售价
            /// </summary>
            ColRetailPrice,
            /// <summary>
            /// 用量
            /// </summary>
            ColDoseOnce,
            /// <summary>
            /// 剂量单位
            /// </summary>
            //ColDoseUnit,
            /// <summary>
            /// 总量
            /// </summary>
            ColQty,
            /// <summary>
            /// 单位
            /// </summary>
            //ColUnit,
            /// <summary>
            /// 频次
            /// </summary>
            ColFrequency,
            /// <summary>
            /// 用法
            /// </summary>
            ColUsage,
            /// <summary>
            /// 用药时间
            /// </summary>
            ColUseTime,
            /// <summary>
            /// 医嘱类型
            /// </summary>
            //ColOrderType,
            /// <summary>
            /// 开方医生
            /// </summary>
            ColDoctor,
            /// <summary>
            /// 申请时间
            /// </summary>
            ColApplyTime,
            /// <summary>
            /// 组合号
            /// </summary>
            ColComboNO,
            /// <summary>
            /// 备注 张旭添加
            /// </summary>
            //ColMemo,
            /// <summary>
            /// 配液存储条件
            /// </summary>
            ColCompoundStore
        }

        #endregion

        #region  排序 {F0F32E8B-6E72-4c8e-87DD-7149CDFE8935}

        public class CompareApplyOut : IComparer
        {
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut o1 = (x as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = (y as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();

                string oX = o1.UseTime.ToShortDateString();//o1.User01;          //患者姓名
                string oY = o2.UseTime.ToShortDateString();// o2.User01;          //患者姓名

                int nComp;

                if (oX == null)
                {
                    nComp = (oY != null) ? -1 : 0;
                }
                else if (oY == null)
                {
                    nComp = 1;
                }
                else
                {
                    nComp = string.Compare(oX.ToString(), oY.ToString());
                }
                if (nComp == 0)
                {
                    nComp = string.Compare(o1.ApplyDept.ID, o2.ApplyDept.ID);
                }
                if (nComp == 0)
                {
                    nComp = string.Compare(o1.PatientNO, o2.PatientNO);
                }
                if (nComp == 0)
                {
                    nComp = string.Compare(o1.CombNO, o2.CombNO);
                }
                if (nComp == 0)
                {
                    nComp = string.Compare(o1.UseTime.ToString(), o2.UseTime.ToString());
                }

                return nComp;
            }

        }

        #endregion

    }
}
