using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Order.Controls
{
    public partial class ucOrderExecMultiInput : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucOrderExecMultiInput()
        {
            InitializeComponent();
        }

        #region 属性

        #endregion

        #region 变量

        /// <summary>
        /// 医嘱列表
        /// </summary>
        ArrayList list = new ArrayList();

        /// <summary>
        /// 开始日期
        /// </summary>
        string fromDate = DateTime.Now.ToString("yyyyMMddHHmmss");

        /// <summary>
        /// 结束日期
        /// </summary>
        string toDate = DateTime.Now.ToString("yyyyMMddHHmmss");

        /// <summary>
        /// 护士站代码
        /// </summary>
        private string nurseCell = string.Empty;

        /// <summary>
        /// 获得登陆科室对应护士站
        /// </summary>
        public string NurseCell
        {
            get
            {
                FS.FrameWork.Models.NeuObject deptObj = new FS.FrameWork.Models.NeuObject();
                ArrayList nurseList = new ArrayList();
                deptObj.ID = CacheManager.LogEmpl.Dept.ID;

                #region {EE43A001-7E2E-45e7-B2AB-8E59523D86F1}

                FS.HISFC.Models.Base.Department myDept = SOC.HISFC.BizProcess.Cache.Common.GetDept(deptObj.ID);
                if (myDept.DeptType.ID.ToString() == FS.HISFC.Models.Base.EnumDepartmentType.N.ToString())
                {
                    return deptObj.ID;
                }
                else
                {
                    nurseList = CacheManager.InterMgr.QueryNurseStationByDept(deptObj);
                    if (nurseList != null && nurseList.Count > 0)
                    {
                        deptObj = nurseList[0] as FS.FrameWork.Models.NeuObject;
                        nurseCell = deptObj.ID;
                        return nurseCell;
                    }
                }
                //nurseList = deptManager.GetNurseStationFromDept(deptObj);
                //if (nurseList != null && nurseList.Count > 0)
                //{
                //    deptObj = nurseList[0] as FS.FrameWork.Models.NeuObject;
                //    nurseCell = deptObj.ID;
                //    return nurseCell;
                //}
                #endregion
                return null;
            }
        }

        /// <summary>
        /// 是否已录入  false代表未录入
        /// </summary>
        private bool isInPuted = false;

        public bool IsInPuted
        {
            get
            {
                return this.rdoIsExamed.Checked;
            }
        }


        /// <summary>
        /// 患者信息
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();

        private FS.HISFC.BizLogic.Manager.Person person = new FS.HISFC.BizLogic.Manager.Person();

        #endregion

        #region 方法

        /// <summary>
        /// 检查是否数据是否有效
        /// </summary>
        /// <returns></returns>
        private int CheckValue()
        {
            if (this.rdoIsAll.Checked)
            {

            }
            else
            {
                if (string.IsNullOrEmpty(patient.ID))
                {
                    MessageBox.Show("按单个患者查询需要输入住院号，并回车");
                    this.ucQueryInpatientNo1.Focus();
                    return -1;
                }
            }
            return 1;
        }

        private int Query()
        {
            int returnValue = -1;

            this.Clear();
            this.GetQueryDate();

            if (this.rdoIsAll.Checked)
            {
                returnValue = this.QueryOrderListForAll();
            }
            else
            {
                returnValue = this.QueryOrderListForSingle();
            }

            if (returnValue < 0)
            {
                MessageBox.Show(CacheManager.InOrderMgr.Err);
                return -1;
            }

            return 1;
        }

        public void GetQueryDate()
        {
            fromDate = this.dtBegin.Value.ToString("yyyyMMddHHmmss");
            toDate = this.dtEnd.Value.ToString("yyyyMMddHHmmss");
        }

        /// <summary>
        /// 查询单个患者
        /// </summary>
        /// <returns></returns>
        private int QueryOrderListForSingle()
        {
            if (this.CheckValue() < 0)
            {
                return -1;
            }
            list = CacheManager.InOrderMgr.QueryOrderExedInfoByNurseCell(NurseCell, patient.ID, fromDate, toDate, IsInPuted);

            return 1;
        }

        /// <summary>
        /// 查询全部患者
        /// </summary>
        /// <returns></returns>
        private int QueryOrderListForAll()
        {
            if (this.CheckValue() < 0)
            {
                return -1;
            }
            list = CacheManager.InOrderMgr.QueryOrderExedInfoByNurseCell(NurseCell, "AAAA", fromDate, toDate, IsInPuted);
            return 1;
        }

        private void SetComboFlag()
        {
            for (int i = 0; i < list.Count - 1; i++)
            {

                FS.HISFC.Models.Order.OrderBill ord = list[i] as FS.HISFC.Models.Order.OrderBill;
                FS.HISFC.Models.Order.OrderBill ord2 = list[i + 1] as FS.HISFC.Models.Order.OrderBill;

                if (ord == null || ord2 == null)
                {
                    break;
                }
                else
                {
                    string combo1 = string.Empty;
                    string combo2 = string.Empty;

                    combo1 = ord.Order.Combo.ID;
                    combo2 = ord2.Order.Combo.ID;

                    if (!string.IsNullOrEmpty(combo1) || !string.IsNullOrEmpty(combo2))
                    {
                        if (combo1 == combo2)
                        {
                            if (this.neuSpread1_Sheet1.Cells[i, 3].Text == "┃")
                            {

                                this.neuSpread1_Sheet1.Cells[i + 1, 3].Text = "┃";

                            }
                            else
                            {
                                this.neuSpread1_Sheet1.Cells[i, 3].Text = "┓";
                                this.neuSpread1_Sheet1.Cells[i + 1, 3].Text = "┃";
                            }
                            if (i + 1 == list.Count - 1)
                            {
                                this.neuSpread1_Sheet1.Cells[i + 1, 3].Text = "┛";
                            }
                        }
                        else if (this.neuSpread1_Sheet1.Cells[i, 3].Text == "┃")
                        {
                            this.neuSpread1_Sheet1.Cells[i, 3].Text = "┛";
                        }
                    }
                }


            }
        }


        /// <summary>
        /// 设置查询结果显示
        /// </summary>
        /// <returns></returns>
        private int SetDataTable()
        {
            FS.HISFC.Models.Order.OrderBill obj;
            this.SetFpStyle();
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    obj = list[i] as FS.HISFC.Models.Order.OrderBill;

                    this.neuSpread1_Sheet1.Rows.Add(i, 1);
                    this.neuSpread1_Sheet1.Rows[i].Tag = obj;
                    this.neuSpread1_Sheet1.Cells[i, 0].Text = obj.Order.Patient.PVisit.PatientLocation.Bed.ID.ToString();
                    this.neuSpread1_Sheet1.Cells[i, 1].Text = obj.Order.Patient.Name;
                    this.neuSpread1_Sheet1.Cells[i, 2].Text = obj.Order.Name;
                    this.neuSpread1_Sheet1.Cells[i, 3].Value = "";
                    this.neuSpread1_Sheet1.Cells[i, 4].Value = obj.Order.Memo;
                    this.neuSpread1_Sheet1.Cells[i, 5].Text = obj.Order.MOTime.ToString("yyyy-MM-dd HH:mm:ss");
                    this.neuSpread1_Sheet1.Cells[i, 6].Text = this.getOrderStatusName(obj.Order.Status);
                    this.neuSpread1_Sheet1.Cells[i, 7].Text = obj.Order.Qty + obj.Order.Unit;
                    this.neuSpread1_Sheet1.Cells[i, 8].Text = obj.Order.ExecOper.Name;
                    if (obj.Order.ExecOper.OperTime.Year > 1000)
                    {
                        this.neuSpread1_Sheet1.Cells[i, 9].Text = obj.Order.ExecOper.OperTime.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells[i, 9].Text = "";
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show("赋值出错" + e.Message);
                return -1;
            }
            return 1;
        }

        private void SetFpStyle()
        {
            this.neuSpread1_Sheet1.ColumnCount = 10;
            this.neuSpread1_Sheet1.Columns[0].Label = "床号";
            this.neuSpread1_Sheet1.Columns[0].Width = 50;
            this.neuSpread1_Sheet1.Columns[1].Label = "姓名";
            this.neuSpread1_Sheet1.Columns[1].Width = 100;
            this.neuSpread1_Sheet1.Columns[2].Label = "医嘱名称";
            this.neuSpread1_Sheet1.Columns[2].Width = 200;
            this.neuSpread1_Sheet1.Columns[3].Label = "组合标记";
            this.neuSpread1_Sheet1.Columns[3].Width = 50;
            this.neuSpread1_Sheet1.Columns[4].Label = "备注";
            this.neuSpread1_Sheet1.Columns[4].Width = 50;
            this.neuSpread1_Sheet1.Columns[5].Label = "开立时间";
            this.neuSpread1_Sheet1.Columns[5].Width = 100;
            this.neuSpread1_Sheet1.Columns[6].Label = "医嘱状态";
            this.neuSpread1_Sheet1.Columns[6].Width = 40;
            this.neuSpread1_Sheet1.Columns[7].Label = "数量";
            this.neuSpread1_Sheet1.Columns[7].Width = 50;
            this.neuSpread1_Sheet1.Columns[8].Label = "执行护士";
            this.neuSpread1_Sheet1.Columns[8].Width = 100;
            this.neuSpread1_Sheet1.Columns[9].Label = "执行时间";
            this.neuSpread1_Sheet1.Columns[9].Width = 200;
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType1 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucOrderExecMultiInput));
            dateTimeCellType1.Calendar = ((System.Globalization.Calendar)dateTimeCellType1.Calendar);
            dateTimeCellType1.DateDefault = DateTime.Now;
            dateTimeCellType1.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined;
            dateTimeCellType1.UserDefinedFormat = "yyyy-MM-dd HH:mm:ss";
            this.neuSpread1_Sheet1.Columns[9].CellType = dateTimeCellType1;
        }

        /// <summary>
        /// 根据医嘱状态获得医嘱名称
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private string getOrderStatusName(int p)
        {
            switch (p)
            {
                case 0: return "开立";
                case 1: return "审核";
                case 2: return "执行";
                case 3: return "作废";
                case 4: return "重整";
                default: return "状态不明";

            }
        }


        private void Clear()
        {
            try
            {
                this.neuSpread1_Sheet1.Rows.Count = 0;
                this.list.Clear();
            }
            catch
            {
            }
        }



        #endregion

        #region 事件

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                ArrayList arrlist = new ArrayList();
                if (CacheManager.LogEmpl.EmployeeType.Name == "护士" 
                    || CacheManager.LogEmpl.EmployeeType.Name == "医生")
                {
                    arrlist.Add(CacheManager.LogEmpl);
                }
               this.neuFpEnter1.SetColumnList(this.neuSpread1_Sheet1, 8, SOC.HISFC.BizProcess.Cache.Common.GetEmployee());
            }
            catch
            {
            }
            this.SetBegDate();
            base.OnLoad(e);

        }

        private void SetBegDate()
        {
            dtBegin.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btQuery_Click(object sender, EventArgs e)
        {
            if (this.Query() < 0 || list == null || list.Count == 0)
            {
                return;
            }
            if (this.SetDataTable() < 0)
            {
                return;
            }
            SetComboFlag();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSave_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1_Sheet1.RowCount == 0)
            {
                MessageBox.Show("没有数据需要保存");
                return;
            }
           
            list.Clear();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            CacheManager.InOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            bool isSucess = true;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if ((this.neuSpread1_Sheet1.Cells[i, 8].Text != "" && this.neuSpread1_Sheet1.Cells[i, 9].Text == "")
                    || (this.neuSpread1_Sheet1.Cells[i, 8].Text == "" && this.neuSpread1_Sheet1.Cells[i, 9].Text != ""))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("执行护士和执行时间必须同时存在！"));
                    isSucess = false;
                    break;
                }
                list.Add(this.neuSpread1_Sheet1.Rows[i].Tag);
                FS.HISFC.Models.Order.OrderBill ord = list[i] as FS.HISFC.Models.Order.OrderBill;
                if (ord != null)
                {
                    ord.Order.ExtendFlag1 = this.neuSpread1_Sheet1.Cells[i, 9].Text;

                    FS.FrameWork.Models.NeuObject ob = new FS.FrameWork.Models.NeuObject();
                    
                    //ord.Order.ExtendFlag2 = personHelper.GetID(this.neuSpread1_Sheet1.Cells[i, 8].Text);

                    if (!string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, 8].Text))
                    {
                        ArrayList temp = person.GetPersonByName(this.neuSpread1_Sheet1.Cells[i, 8].Text);
                        if (temp.Count > 0)
                        {
                            ord.Order.ExtendFlag2 = ((FS.HISFC.Models.Base.Employee)temp[0]).ID;
                        }
                        else
                        {
                            this.neuSpread1_Sheet1.Cells[i, 8].Text = "";
                            MessageBox.Show("签名护士工号不存在，请确认此工号存在 ");
                            return;
                        }
                    }
                    if (string.IsNullOrEmpty(ord.Order.ExtendFlag1))
                    {
                        ord.Order.ExtendFlag1 = DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    if (string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, 8].Text))
                    {
                        ord.Order.ExtendFlag2 = "";
                    }

                    if (!string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, 8].Text))
                    {
                        if (string.IsNullOrEmpty(ord.Order.ExtendFlag2))
                        {
                            ord.Order.ExtendFlag2 = this.neuSpread1_Sheet1.Cells[i, 8].Text.Trim();
                            ord.Order.ExtendFlag1 = DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        if (CacheManager.InOrderMgr.UpdatePrnOrder(ord.Order.Patient.ID, ord.Order) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("保存失败" + CacheManager.InOrderMgr.Err);
                            return;
                        }
                    }

                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("医嘱信息为空");
                    return;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            if (isSucess)
            {
                MessageBox.Show("保存成功");
            }
            if (this.Query() < 0 || list == null)
            {
                MessageBox.Show("重新加载失败！请退出重新查询！");
                return;
            }
            if (this.SetDataTable() < 0)
            {
                return;
            }
            SetComboFlag();


        }

        /// <summary>
        /// 全部患者单选按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoIsAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoIsAll.Checked)
            {
                this.ucQueryInpatientNo1.Enabled = false;
            }
            else
            {
                this.ucQueryInpatientNo1.Enabled = true;
            }

        }

        /// <summary>
        /// 住院号回车事件
        /// </summary>
        private void ucQueryInpatientNo1_myEvent()
        {
            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo.Trim() == "")
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("住院号错误，没有找到该患者", 111);
                this.ucQueryInpatientNo1.Focus();
                return;
            }
            patient.ID = this.ucQueryInpatientNo1.InpatientNo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private int fpSpread3_SetItem(FS.FrameWork.Models.NeuObject obj)
        {
            if (this.neuSpread1_Sheet1.ActiveColumnIndex == 8)
            {
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.neuFpEnter1.getCurrentList(this.neuSpread1_Sheet1, 8);
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                if (item != null)
                {
                    int currow = this.neuSpread1_Sheet1.ActiveRowIndex;
                    neuSpread1_Sheet1.ActiveCell.Text = item.Name;
                    neuSpread1_Sheet1.ActiveCell.Tag = item;
                    neuSpread1_Sheet1.Cells[currow, 9].Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    neuSpread1_Sheet1.SetActiveCell(this.neuSpread1_Sheet1.ActiveRowIndex + 1, 8);
                    SetCom(currow, item.Name);
                }
            }
            return 0;
        }
        #endregion


        private void SetCom(int currow, string name)
        {
            string combo = null;
            //string execname=null;
            //int currow = this.neuSpread1_Sheet1.ActiveRowIndex;
            if (!string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[currow, 8].Text))
            {
                if (this.neuSpread1_Sheet1.ActiveColumnIndex == 8)
                {
                    //execname = this.neuSpread1_Sheet1.Cells[currow, 7].Text;
                    combo = (list[currow] as FS.HISFC.Models.Order.OrderBill).Order.Combo.ID;
                }
                for (int i = 0; i < list.Count - 1; i++)
                {
                    if (i != currow)
                    {
                        FS.HISFC.Models.Order.OrderBill ord = list[i] as FS.HISFC.Models.Order.OrderBill;

                        if (ord != null)
                        {

                            string combo1 = string.Empty;
                            string combo2 = string.Empty;

                            combo1 = ord.Order.Combo.ID;

                            if (!string.IsNullOrEmpty(combo))
                            {
                                if (combo1 == combo)
                                {
                                    this.neuSpread1_Sheet1.Cells[i, 9].Text = this.neuSpread1_Sheet1.Cells[currow, 9].Text;
                                    this.neuSpread1_Sheet1.Cells[i, 8].Text = name;
                                }
                            }
                        }
                    }
                }

            }
        }

        private void txtItemFilter_TextChanged(object sender, EventArgs e)
        {
            this.Filter();

        }
        string strFilter = string.Empty;
        //拼音管理类
        FS.HISFC.BizLogic.Manager.Spell mySpell = new  FS.HISFC.BizLogic.Manager.Spell();
         //根据名称生产拼音码和五笔码
        FS.HISFC.Models.Base.Spell spell = new  FS.HISFC.Models.Base.Spell();
        private void Filter()
        {
            strFilter = this.txtItemFilter.Text.Trim();
            spell=(FS.HISFC.Models.Base.Spell)mySpell.Get(strFilter);
            strFilter = spell.SpellCode;
            if (strFilter == null)
            {
                strFilter = "";
            }
            this.SetFilter(strFilter);
        }

        private void SetFilter(string filterString)
        {
            if (this.neuSpread1_Sheet1.Rows.Count > 0)
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    this.neuSpread1_Sheet1.Rows[i].Height = 20;
                    if (string.IsNullOrEmpty(filterString))
                    {
                    }
                    else
                    {                        
                        spell = (FS.HISFC.Models.Base.Spell)mySpell.Get(this.neuSpread1_Sheet1.Cells[i, 2].Value.ToString());
                        if (spell.SpellCode.IndexOf(filterString) < 0)
                        {
                            this.neuSpread1_Sheet1.Rows[i].Height = 0;
                        }
                    }
                }
            }
        }
    }
}
