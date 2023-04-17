using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Pharmacy.Base
{
    /// <summary>
    /// [控件描述: 药品全院特限授权管理 {1A398A34-0718-47ed-AAE9-36336430265E}]
    /// [创 建 人: Sunjh]
    /// [创建时间: 2010-10-1]
    /// </summary>
    public partial class ucSpeDrugOutSpe : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucSpeDrugOutSpe()
        {
            InitializeComponent();
            //this.Init();
        }

        #region 变量
        FS.HISFC.BizLogic.Pharmacy.Item itemPha = new FS.HISFC.BizLogic.Pharmacy.Item();
        /// <summary>
        /// 药品常数管理类
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Constant consManager = new FS.HISFC.BizLogic.Pharmacy.Constant();
        ArrayList alDept = new ArrayList();
        ArrayList alPerson = new ArrayList();
        ArrayList alSpeDrug = new ArrayList();
        ArrayList alSpeDrug1 = new ArrayList();
        FS.HISFC.Models.Pharmacy.EnumDrugSpecialType speType = FS.HISFC.Models.Pharmacy.EnumDrugSpecialType.Dept;

        /// <summary>
        /// 当前活动SheetView
        /// </summary>
        protected FarPoint.Win.Spread.SheetView ActiveSv
        {
            get
            {
                return this.neuSpread1.ActiveSheet;
            }
        }
        #endregion

        #region 初始化工具栏
        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("删除", "删除一条数据", FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            toolBarService.AddToolButton("增加", "增加一条数据", FS.FrameWork.WinForms.Classes.EnumImageList.T添加, true, false, null);
            return toolBarService;
        }
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "删除")
            {
                this.DelSpeDrugCompare();
            }
            else if (e.ClickedItem.Text == "增加")
            {
                this.AddSpeDrugCompare();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }
        #endregion

        #region 方法
        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            this.InitSpeDrug();

            base.OnLoad(e);
        }

        /// <summary>
        /// 增加一条特限药品对照
        /// </summary>
        private void AddSpeDrugCompare()
        {
            this.ActiveSv.Rows.Add(ActiveSv.Rows.Count, 1);
        }

        /// <summary>
        /// 初始化科室、人员、特显药品
        /// </summary>
        /// <returns></returns>
        private int Init()
        {
            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
            this.alDept = deptManager.GetDeptmentAll();
            if (this.alDept == null)
            {
                MessageBox.Show(Language.Msg("科室列表加载失败") + deptManager.Err);
                return -1;
            }
            

            FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
            this.alPerson = personManager.GetEmployeeAll();
            if (this.alPerson == null)
            {
                MessageBox.Show(Language.Msg("人员列表加载失败") + personManager.Err);
                return -1;
            }

            alSpeDrug = itemPha.QueryAllSpeDrug();
            if (alSpeDrug == null)
            {
                MessageBox.Show(itemPha.Err);
                return -1;
            }

            #region 加上全部

            if (this.alSpeDrug.Count > 0)
            {
                FS.HISFC.Models.Pharmacy.Item item = new FS.HISFC.Models.Pharmacy.Item();
                item.ID = "全部";
                item.Name = "全部";
                item.SpellCode = "qb";
                
                alSpeDrug1.Add(item);

            }

            //{902C80AC-58AE-4ff1-9B5A-0E72C80B025B}  edit by shizj 2010-05-03 

            //foreach (FS.HISFC.Models.Pharmacy.Item item  in alSpeDrug)
            //{
            //    alSpeDrug1.Add(item);
            //}
            foreach (FS.HISFC.Models.Pharmacy.DrugSpecial drugSpe in alSpeDrug)
            {
                alSpeDrug1.Add( drugSpe.Item );
            }

            #endregion
            return 0;
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.ActiveSv.ActiveColumnIndex == 1)
            {
                this.PopSpeItem(e.Row);
            }
            if (this.ActiveSv.ActiveColumnIndex == 3)
            {
                this.PopSpeItem(e.Row);
            }
        }

        /// <summary>
        /// 根据双击的单元格不同，显示不同的内容
        /// </summary>
        /// <param name="iIndex"></param>
        public void PopSpeItem(int iIndex)
        {
            ArrayList alData = new ArrayList();

            if (this.ActiveSv.ActiveColumn.Index == 1)
            {
                if (this.ActiveSv == this.neuSpread1_Sheet1)
                {
                    alData = this.alDept;
                }
                else
                {
                    alData = this.alPerson;
                }
                FS.FrameWork.Models.NeuObject speObj = new FS.FrameWork.Models.NeuObject();
                if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(alData, ref speObj) == 1)
                {
                    this.ActiveSv.Cells[iIndex, 0].Text = speObj.ID.ToString();
                    this.ActiveSv.Cells[iIndex, 1].Text = speObj.Name.ToString();
                }
            }
            if (this.ActiveSv.ActiveColumn.Index == 3)
            {
                alData = this.alSpeDrug1;
                FS.FrameWork.Models.NeuObject speObj = new FS.FrameWork.Models.NeuObject();

                if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(alData, ref speObj) == 1)
                {
                    this.ActiveSv.Cells[iIndex, 2].Text = speObj.ID.ToString();
                    this.ActiveSv.Cells[iIndex, 3].Text = speObj.Name.ToString();
                }
            }
        }

        /// <summary>
        /// 初始化已有维护
        /// </summary>
        private void InitSpeDrug()
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;
            this.neuSpread1_Sheet2.Rows.Count = 0;
            DataTable dt = new DataTable();
            
            FS.HISFC.Models.Pharmacy.EnumDrugSpecialType speType = FS.HISFC.Models.Pharmacy.EnumDrugSpecialType.Dept;
            dt = consManager.GetSpeDruCompareDepOrPerson(speType);
            
            if (dt == null)
            {
                MessageBox.Show(consManager.Err);
                return;
            }
            this.neuSpread1_Sheet1.Rows.Count = dt.Rows.Count;
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                this.neuSpread1_Sheet1.Cells[row, 0].Text = dt.Rows[row][0].ToString();
                this.neuSpread1_Sheet1.Cells[row, 1].Text = dt.Rows[row][1].ToString();
                this.neuSpread1_Sheet1.Cells[row, 2].Text = dt.Rows[row][2].ToString();
                this.neuSpread1_Sheet1.Cells[row, 3].Text = dt.Rows[row][3].ToString();
                this.neuSpread1_Sheet1.Cells[row, 4].Text = dt.Rows[row][4].ToString();
                this.neuSpread1_Sheet1.Cells[row, 5].Text = dt.Rows[row][5].ToString();
            }
           
            //this.neuSpread1_Sheet1.DataSource = dt.DefaultView;

            speType = FS.HISFC.Models.Pharmacy.EnumDrugSpecialType.Doc;
            dt = consManager.GetSpeDruCompareDepOrPerson(speType);
            if (dt == null)
            {
                MessageBox.Show(itemPha.Err);
                return;
            }
            this.neuSpread1_Sheet2.Rows.Count = dt.Rows.Count;
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                this.neuSpread1_Sheet2.Cells[row, 0].Text = dt.Rows[row][0].ToString();
                this.neuSpread1_Sheet2.Cells[row, 1].Text = dt.Rows[row][1].ToString();
                this.neuSpread1_Sheet2.Cells[row, 2].Text = dt.Rows[row][2].ToString();
                this.neuSpread1_Sheet2.Cells[row, 3].Text = dt.Rows[row][3].ToString();
                this.neuSpread1_Sheet2.Cells[row, 4].Text = dt.Rows[row][4].ToString();
                this.neuSpread1_Sheet2.Cells[row, 5].Text = dt.Rows[row][5].ToString();
            }
            
            //this.neuSpread1_Sheet2.DataSource = dt.DefaultView;
        }

        /// <summary>
        /// 删除一条信息
        /// </summary>
        private int DelSpeDrugCompare()
        {
            if (this.ActiveSv.Rows.Count <= 0)
            {
                return 1;
            }

            DialogResult rs = MessageBox.Show(Language.Msg("确认删除该条特限信息吗?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
            {
                return 0;
            }
            FS.HISFC.Models.Pharmacy.DrugSpecial druSpe = new FS.HISFC.Models.Pharmacy.DrugSpecial();
            druSpe.User01 = this.ActiveSv.Cells[ActiveSv.ActiveRow.Index, 0].Text;
            druSpe.ID = this.ActiveSv.Cells[ActiveSv.ActiveRow.Index, 1].Text;
            druSpe.Item.ID = this.ActiveSv.Cells[ActiveSv.ActiveRow.Index, 3].Text;
            if (consManager.DelSpeDrugCompare(druSpe) == -1)
            {
                MessageBox.Show(consManager.Err);
                return -1;
            }

            this.ActiveSv.RemoveRows(this.ActiveSv.ActiveRow.Index, 1);
            MessageBox.Show("删除成功");
            return 0;
        }

        /// <summary>
        /// 保存特限药品维护信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            this.Valid();

            if (this.ActiveSv == this.neuSpread1_Sheet1)
            {
                speType = FS.HISFC.Models.Pharmacy.EnumDrugSpecialType.Dept;
            }
            else
            {
                speType = FS.HISFC.Models.Pharmacy.EnumDrugSpecialType.Doc;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            #region 先删除部门或者人员的全部数据
            int flag = consManager.DelSpeDrugDepOrPerCompare(speType);
            if (flag == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("删除Pha_Com_SpeDrug_per_Dep中的数据出错");
                return -1;
            }
            #endregion
            #region  保存数据
            for (int row = 0; row < this.ActiveSv.Rows.Count; row++)
            {
                FS.HISFC.Models.Pharmacy.DrugSpecial drugSpe = new FS.HISFC.Models.Pharmacy.DrugSpecial();
                //this.ActiveSv.Rows[row].Tag as FS.HISFC.Models.Pharmacy.DrugSpecial;

                drugSpe.ID = this.ActiveSv.Cells[row, 0].Text;
                drugSpe.Name = this.ActiveSv.Cells[row, 1].Text;
                drugSpe.Item.ID = this.ActiveSv.Cells[row, 2].Text;
                drugSpe.Item.Name = this.ActiveSv.Cells[row, 3].Text;
                drugSpe.Memo = this.ActiveSv.Cells[row, 4].Text;  //备注信息
                drugSpe.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(consManager.GetSysDateTime());
                drugSpe.Oper.ID = this.consManager.Operator.ID;
                drugSpe.SpeType = speType;
                if (this.ActiveSv.Cells[row, 5].Text == "有效")
                {
                    drugSpe.User01 = "0";
                }
                else
                {
                    drugSpe.User01 = "1";
                }

                flag = consManager.InsertSpeDrugDepOrPerCompare(drugSpe);
                if (flag == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    if (consManager.DBErrCode == 1)
                    {
                        MessageBox.Show("第" + row.ToString() + "行与前面的数据重复，请先删除再进行保存");
                        return -1;
                    }
                    else
                    {
                        MessageBox.Show("保存数据出错");
                        return -1;
                    }
                    
                }
                
            }
            #endregion
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("保存成功");
            return base.OnSave(sender, neuObject);
        }
        /// <summary>
        /// 判断是否有效
        /// </summary>
        private void Valid()
        {
            for(int row=0;row<this.ActiveSv.Rows.Count;row++)
            {
                if (this.ActiveSv.Cells[row, 0].Text == "")
                {
                    MessageBox.Show("第" + (row).ToString() + "行的" + this.ActiveSv.SheetName + "编码不能为空");
                    return;
                }
                if (this.ActiveSv.Cells[row, 1].Text == "")
                {
                    MessageBox.Show("第" + (row).ToString() + "行的" + this.ActiveSv.SheetName + "名称不能为空");
                    return;
                }
                if (this.ActiveSv.Cells[row, 2].Text == "")
                {
                    MessageBox.Show("第" + (row).ToString() + "行的药品编码不能为空");
                    return;
                }
                if (this.ActiveSv.Cells[row, 3].Text == "")
                {
                    MessageBox.Show("第" + (row).ToString() + "行的药品名称不能为空"); 
                    return;
                }
                if (this.ActiveSv.Cells[row, 5].Text == "")
                {
                    MessageBox.Show("第" + (row).ToString() + "行的有效性不能为空");
                    return;
                }
            }
        }
        #endregion
    }
}
