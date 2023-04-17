using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.HISFC.Components.Order.OutPatient.Controls
{
    /// <summary>
    /// [功能描述：检验医嘱开立]
    /// [创 建 者：薛文进]
    /// [创建时间：2009-3-17]
    /// </summary>
    public partial class ucLab : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucLab()
        {
            InitializeComponent();
        }

        #region 业务层

        /// <summary>
        /// 整合的管理类
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Manager conMgr = new Neusoft.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 门诊医嘱管理类
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Fee medicalTermMgr = new Neusoft.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 医嘱管理类
        /// </summary>
        private Neusoft.HISFC.BizLogic.Order.Order orderManager = new Neusoft.HISFC.BizLogic.Order.Order();

        /// <summary>
        /// 医嘱术语对照关系管理类
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Fee undrugCompareMgr = new Neusoft.HISFC.BizProcess.Integrate.Fee();

        #endregion

        #region 变量
        

        /// <summary>
        /// 检验医嘱 
        /// </summary>
        private List<Neusoft.HISFC.Models.Fee.Item.Undrug> orderTermList = null;

        /// <summary>
        /// 当前大类的检验医嘱
        /// </summary>
        private List<Neusoft.HISFC.Models.Fee.Item.Undrug> currventOrderList = null;

        /// <summary>
        /// 当前大类中已排斥的医嘱
        /// </summary>
        private List<Neusoft.HISFC.Models.Fee.Item.Undrug> currventUnSelected = new List<Neusoft.HISFC.Models.Fee.Item.Undrug>();

        /// <summary>
        /// 当前大类中选中的医嘱
        /// </summary>
        private List<Neusoft.HISFC.Models.Fee.Item.Undrug> currventSelected = new List<Neusoft.HISFC.Models.Fee.Item.Undrug>();

        /// <summary>
        /// 检验大类
        /// </summary>
        ArrayList al = null;

        /// <summary>
        /// 由样本类型排斥的项目坐标集合
        /// </summary>
        ArrayList sampleForbidTerm = new ArrayList();

        /// <summary>
        /// 所有已选择的医嘱对应的物价项目集合
        /// </summary>
        Hashtable htUndrug = new Hashtable();

        /// <summary>
        /// 所有物价项目集合
        /// </summary>
        Hashtable htUndrugAll = new Hashtable();

        /// <summary>
        /// 所有已选择的医嘱对应的排斥项目的坐标集合
        /// </summary>
        Hashtable htSelectedCell = new Hashtable();

        /// <summary>
        /// 全部医嘱术语和物价项目对照关系
        /// </summary>
        private ArrayList compareAl = null;

        private ArrayList sampleAl = null;

        /// <summary>
        /// 选择的医嘱
        /// </summary>
        private List<Neusoft.HISFC.Models.Order.Inpatient.Order> inOrderList = new List<Neusoft.HISFC.Models.Order.Inpatient.Order>();
        /// <summary>
        /// 选择的医嘱
        /// </summary>
        private List<Neusoft.HISFC.Models.Order.OutPatient.Order> outOrderList = new List<Neusoft.HISFC.Models.Order.OutPatient.Order>();

        /// <summary>
        /// 当前操作员
        /// </summary>
        private Neusoft.HISFC.Models.Base.Employee emp = Neusoft.FrameWork.Management.Connection.Operator as Neusoft.HISFC.Models.Base.Employee;

        /// <summary>
        /// sheetView
        /// </summary>
        FarPoint.Win.Spread.SheetView view = new FarPoint.Win.Spread.SheetView();
        
        /// <summary>
        /// 每行显示的医嘱个数
        /// </summary>
        int columnCount=0;

        /// <summary>
        /// 每个项目的子列
        /// </summary>
        int termColumn = 0;

        /// <summary>
        /// 可是所属地标志
        /// </summary>
        string deptflag = "";

        /// <summary>
        /// 选择的前一个样本
        /// </summary>
        string sampleCode = "";

        string mark = "";

        bool isEdit = false;

        Neusoft.HISFC.Models.RADT.Patient patientInfo = new Neusoft.HISFC.Models.RADT.Patient();

        public Neusoft.HISFC.Models.RADT.Patient PatientInfo
        {
            get { return patientInfo; }
            set { patientInfo = value; }
        }



        ArrayList orderList = new ArrayList();

        public ArrayList OrderList
        {
            get { return orderList; }
            set { orderList = value; }
        }

        /// <summary>
        /// 是否门诊使用
        /// </summary>
        private bool isClinic = false;

        //{EB33C62A-C82A-4517-8181-3776137950C9} 检验加备注 20100119
        /// <summary>
        /// 备注
        /// </summary>
        private ArrayList alMemo = null;

        #endregion

        #region 属性

        /// <summary>
        /// 选择的医嘱
        /// </summary>
        public List<Neusoft.HISFC.Models.Order.Inpatient.Order> InOrderList
        {
            get { return inOrderList; }
            set { inOrderList = value; }
        }

        /// <summary>
        /// 选择的医嘱
        /// </summary>
        public List<Neusoft.HISFC.Models.Order.OutPatient.Order> OrderAL
        {
            get { return outOrderList; }
            set { outOrderList = value; }
        }

        /// <summary>
        /// 是否门诊使用
        /// </summary>
        public bool IsClinic
        {
            set
            {
                this.isClinic = value;
                this.lblSendTime.Visible = !value;
                this.dtpSendTime.Visible = !value;
            }
            get
            {
                return this.isClinic;
            }
        }
        #endregion

        #region 初始化

        /// <summary>
        /// 界面初始化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            if (this.DesignMode == true) return;

            this.fpParTerm.MouseMove -= new MouseEventHandler(this.fpParTerm_MouseMove);
            this.cmbDept.SelectedIndexChanged -= new EventHandler(this.cmbDept_SelectedIndexChanged);
            //初始化界面
            this.Init();
            //查询物价项目
            this.QueryCompare();
            //查询检验医嘱
            this.QueryOrderTerm();
            //查询检验大类
            this.QueryType();

            isEdit = false;
            if (this.AddOrder() == -1)
            {
                this.orderList = new ArrayList();
                this.InitOrderList();
                this.ParentForm.Close();
            }
            this.cmbDept.SelectedIndexChanged += new EventHandler(this.cmbDept_SelectedIndexChanged);
            this.fpParTerm.MouseMove += new MouseEventHandler(this.fpParTerm_MouseMove);
            DateTime t = new DateTime();
            try
            {
                t = orderManager.GetDateTimeFromSysDateTime();
            }
            catch
            {
                t = DateTime.Now;
            }
            if (dtpSendTime.Tag == null)
            {
                dtpSendTime.Value = t.Date.AddDays(1).AddHours(6);
            }

            //{EB33C62A-C82A-4517-8181-3776137950C9} 检验加备注 20100119
            if (alMemo == null)
            {
                this.alMemo = this.conMgr.QueryConstantList("LABMEMO");

                this.cmbMemo.AddItems(alMemo);
            }

            base.OnLoad(e);
        }

        /// <summary>
        /// 初始化界面
        /// </summary>
        /// <returns></returns>
        private int Init()
        {
            Neusoft.HISFC.Models.Base.Department dept=this.conMgr.GetDepartment(this.emp.Dept.ID);
            if(dept==null)
            {
                MessageBox.Show("查询科室信息出错!"+this.conMgr.Err);
                return -1;
            }
            
            #region 初始化FarPoint
            //每行显示个数
            columnCount = 4;
            //每个项目的子列
            termColumn = 2;

            view = this.fpParTerm_Sheet1;
            view.RemoveColumns(0, view.Columns.Count);
            view.AddColumns(0, columnCount * termColumn);
            view.RemoveRows(0, view.Rows.Count);
            view.RowHeaderVisible = false;
            for (int i = 0; i < columnCount * termColumn; i += termColumn)
            {
                //列标题被几个色
                if (i % (termColumn * 2) == 0)
                {
                    view.ColumnHeader.Columns[i].BackColor = Color.SkyBlue;
                    view.ColumnHeader.Columns[i + termColumn - 1].BackColor = Color.SkyBlue;
                }
                //列格式
                if (i % termColumn == 0)
                {
                    view.Columns[i].Label = "检验项目";
                    view.Columns[i].Width = 160F;
                    view.Columns[i + termColumn - 1].Label = "选择";
                    view.Columns[i + termColumn - 1].Width = 20F;
                }
            }
            #endregion

            #region 初始化下拉列表
            //{7267BF81-14C9-40d2-B6E8-F1D887E9AD1B} 提取共用标本LABSAMPLE 20100118
            //sampleAl = this.conMgr.GetConstantList("LABSAMPLE");
            sampleAl = Neusoft.UFC.Order.OutPatient.Classes.Function.HelperLabSample.ArrayObject.Clone() as ArrayList;
            if (sampleAl == null)
            {
                MessageBox.Show("初始化标本类型失败!" + this.conMgr.Err);
                return -1;
            }
            this.cmbSample.AddItems(sampleAl);

            ArrayList execDeptAL = this.conMgr.GetConstantList("LABDEPT");
            if (execDeptAL == null)
            {
                MessageBox.Show("初始化执行科室失败!" + this.conMgr.Err);
                return -1;
            }
            this.cmbDept.AddItems(execDeptAL);
            this.cmbDept.SelectedIndex = 0;
            #endregion

            return 1;
        }

        /// <summary>
        /// 查询检验医嘱
        /// </summary>
        private void QueryOrderTerm()
        {
            List<Neusoft.HISFC.Models.Fee.Item.Undrug> tmpOrderTermList = this.medicalTermMgr.QueryMedicalTermBySysClass("UL");
            this.orderTermList = new List<Neusoft.HISFC.Models.Order.MedicalTerm>();
            foreach (Neusoft.HISFC.Models.Order.MedicalTerm term in tmpOrderTermList)
            {
                if (IsClinic && term.ApplicabilityArea == "2")
                {//门诊排除住院的
                    continue;
                }
                else if (!IsClinic && term.ApplicabilityArea == "1")
                {//住院就排除门诊的
                    continue;
                }
                else
                {
                    this.orderTermList.Add(term);
                }
            }
            //this.orderTermList = this.medicalTermMgr.QueryMedicalTermBySysClass("UL");
            if (this.orderTermList == null)
            {
                MessageBox.Show("查询检验项目失败!" + this.medicalTermMgr.Err);
                return;
            }
            //this.AddDataToFp(orderTermList);
            //this.currventOrderList = this.orderTermList;
            //this.currventUnSelected = this.orderTermList;
        }

        /// <summary>
        /// 查询检验大类
        /// </summary>
        private void QueryType()
        {
            //取检验大类
            al = this.conMgr.GetConstantList("ZRCLINIC");
            if (al == null)
            {
                MessageBox.Show("查询检验大类失败!" + this.conMgr.Err);
                return;
            }

            //动态加载radioBtn
            for (int i = 0; i < al.Count; i++)
            {
                Neusoft.NFC.Object.NeuObject conInfo = al[i] as Neusoft.NFC.Object.NeuObject;
                Neusoft.NFC.Interface.Controls.NeuRadioButton radioBtn = new Neusoft.NFC.Interface.Controls.NeuRadioButton();
                radioBtn.Name = conInfo.ID;
                radioBtn.Text = conInfo.Name;
                radioBtn.Tag = conInfo.Memo;
                radioBtn.CheckedChanged += new EventHandler(radioBtn_CheckedChanged);
                radioBtn.AutoSize = true;
                this.neuPanel1.Controls.Add(radioBtn);
                //5为每行显示的radioButton个数
                int row = i / 6 + 1;
                Point p = new Point(neuLabel2.Location.X, neuLabel2.Location.Y);
                p.X += 80 * i;
                p.Y += 27 * row;
                radioBtn.Location = p;

                if (i == 0)
                {
                    radioBtn.Checked = true;
                }
            }
        }

        /// <summary>
        /// 查询物价项目
        /// </summary>
        private void QueryCompare()
        {
            this.compareAl = this.undrugCompareMgr.QueryCompareUndrug("UL", this.deptflag);
            if (compareAl == null)
            {
                MessageBox.Show("查询医嘱术语-物价对照失败!" + this.undrugCompareMgr.Err);
                return;
            }
            if (this.deptflag == "1")
            {//国疗
                //将物价项目和医嘱术语存放在哈希表中
                foreach (Neusoft.NFC.Object.NeuObject obj in this.compareAl)
                {
                    //key为医嘱术语编码，Value为对照关系实体
                    if (htUndrugAll.Contains(obj.Memo))
                    {
                        Neusoft.NFC.Object.NeuObject info = htUndrugAll[obj.Memo] as Neusoft.NFC.Object.NeuObject;
                        info.ID += obj.ID + "|";
                        info.Name += "【" + obj.Name + "】";
                    }
                    else
                    {
                        obj.Name = "【" + obj.Name + "】";
                        obj.ID += "|";
                        htUndrugAll.Add(obj.Memo, obj);
                    }
                }
            }
            else
            {
                //将物价项目和医嘱术语存放在哈希表中
                foreach (Neusoft.NFC.Object.NeuObject obj in this.compareAl)
                {
                    string centerItemGrade = obj.User01;
                    if (centerItemGrade == "1")
                    {
                        centerItemGrade = "(甲)";
                    }
                    else if (centerItemGrade == "2")
                    {
                        centerItemGrade = "(乙)";
                    }
                    else if (centerItemGrade == "3")
                    {
                        centerItemGrade = "(丙)";
                    }
                    else
                    {
                        centerItemGrade = "(？)";
                    }
                    //key为医嘱术语编码，Value为对照关系实体
                    if (htUndrugAll.Contains(obj.Memo))
                    {
                        Neusoft.NFC.Object.NeuObject info = htUndrugAll[obj.Memo] as Neusoft.NFC.Object.NeuObject;
                        info.ID += obj.ID + "|";
                        info.Name += "【" + obj.Name + centerItemGrade + "】";
                    }
                    else
                    {
                        obj.Name = "【" + obj.Name + centerItemGrade + "】";
                        obj.ID += "|";
                        htUndrugAll.Add(obj.Memo, obj);
                    }
                }
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 添加数据到farpoint
        /// </summary>
        /// <param name="medicalTermList"></param>
        private void AddDataToFp(List<Neusoft.HISFC.Models.Fee.Item.Undrug> medicalTermList)
        {
            //计算FarPoint行数
            int rowCount = medicalTermList.Count % columnCount == 0 ? medicalTermList.Count / columnCount : medicalTermList.Count / columnCount + 1;
            view.Rows.Remove(0, view.Rows.Count);

            if (medicalTermList.Count == 0)
            {
                return;
            }

            view.Rows.Add(0, rowCount);
            //医嘱项目赋值
            for (int count = 0, row = 0; row < rowCount; count += columnCount, row++)
            {
                view.Rows[row].Height = 30F;
                for (int i = 0; i < columnCount; i++)
                {
                    //超出索引范围则返回
                    if (row * columnCount + i > medicalTermList.Count - 1)
                    {
                        break;
                    }
                    for (int j = 0; j < termColumn; j++)
                    {
                        //medicalTerm的Use01存在当前checkBox的坐标 格式为： 行坐标|列坐标
                        FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                        if (j == termColumn - 1)
                        {
                            view.Cells[row, i * termColumn + j].CellType = checkBoxType;
                            view.Cells[row, i * termColumn + j].Value = false;
                            medicalTermList[count + i].User01 = row.ToString() + "|" + (i * termColumn + j).ToString();

                            view.Cells[row, i * termColumn + j].Tag = medicalTermList[count + i];
                            if(!this.htUndrugAll.Contains(medicalTermList[count+i].ID))
                            {
                                this.view.Cells[row, i * termColumn + j].Locked = true;
                                this.view.Cells[row, i * termColumn].Locked = true;
                                this.view.Cells[row, i * termColumn + j].BackColor = Color.LightGray;
                                this.view.Cells[row, i * termColumn].BackColor = Color.LightGray;
                                this.currventUnSelected.Remove(medicalTermList[count + i]);
                            }

                        }
                        if (j == 0)
                        {
                            view.Cells[row, i * termColumn + j].Text = medicalTermList[count + i].Name;
                            //view.Cells[row, i * termColumn + j + 1].Text = medicalTermList[count + i].SpellCode;
                            //view.Cells[row, i * termColumn + j + 2].Text = medicalTermList[count + i].WBCode;

                        }
                    }
                }
            }
        }

        /*算法描述
         * 排斥条件：1.项目大类排斥 2.样本排斥 3.设备代码排斥 4.物价排斥
         * 
         * 一：选择
         * 1.项目大类排斥：由radioBtn_CheckedChanged实现
         * 2.样本排斥：通过选择样本类型，锁定不是同一个样本的项目
         * 3.设备代码排斥：判断未选择的医嘱的设备代码与当前选择的设备代码是否一致，一致则保留，不一致则锁定
         * 4.物价排斥：判断未选择的医嘱对应的物价项目中在已经选择的医嘱（包括当前选择的）的对应物价项目中是否存在，存在则锁定
         * 二：取消选择
         * 判断取消的医嘱对应的排斥项目的坐标在由样本类型排斥的坐标集以及其他已经选择的医嘱对应的排斥项目的坐标集中是否存在，不存在则借出锁定
         * 坐标集：对应的单元格的坐标 由Neusoft.NFC.Object.NeuObject存储 ID:行坐标 NAME:列坐标
        */
        /// <summary>
        /// 排斥算法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpParTerm_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (view.ActiveCell.CellType is FarPoint.Win.Spread.CellType.CheckBoxCellType)
            {
                if (view.ActiveCell.Tag != null || view.ActiveCell.Tag.ToString() != "")
                {
                    Neusoft.HISFC.Models.Order.MedicalTerm medicalTermInfo = view.ActiveCell.Tag as Neusoft.HISFC.Models.Order.MedicalTerm;

                    //判断鼠标点到的是否是锁定项目，是则返回
                    if (view.ActiveCell.Locked)
                    {
                        return;
                    }
                    //判断是否选择了样本类型，流程要求先选择样本类型才能选择医嘱
                    if (this.mark == "2")
                    {
                        if (this.cmbSample.Tag == null || this.cmbSample.Tag.ToString() == "")
                        {
                            this.view.ActiveCell.Value = !(bool)this.view.ActiveCell.Value;
                            MessageBox.Show("请选择样本类型!", "提示");
                            return;
                        }
                    }
                    #region 样本排斥
                    else if (this.mark == "1" && (bool)view.ActiveCell.Value)
                    {
                        //if (this.orderList.Count>0&&(this.orderList[0] as Neusoft.HISFC.Models.Order.OutPatient.OrderTerm).Item.ID != "")
                        //{

                        //}
                         if (this.cmbSample.Tag == null || this.cmbSample.Tag.ToString() == ""||isEdit)
                        {
                            string sample = medicalTermInfo.CheckBody;
                            if (!medicalTermInfo.CheckBody.Contains("|"))
                            {
                                sample += "|";
                            }
                            string[] sampleIndex = sample.Split('|');
                            ArrayList sampleChangedAL = new ArrayList();
                            foreach (string s in sampleIndex)
                            {
                                if (s != "")
                                {
                                    foreach (Neusoft.NFC.Object.NeuObject objInfom in this.cmbSample.alItems)
                                    {
                                        if (s == objInfom.ID)
                                        {
                                            //{7267BF81-14C9-40d2-B6E8-F1D887E9AD1B} 提取共用标本LABSAMPLE 20100118
                                            //sampleChangedAL.Add(this.conMgr.GetConstant("LABSAMPLE", s));
                                            sampleChangedAL.Add(Neusoft.UFC.Order.OutPatient.Classes.Function.HelperLabSample.GetObjectFromID(s));
                                        }
                                    }
                                    
                                }
                            }
                            this.cmbSample.ClearItems();
                            this.cmbSample.AddItems(sampleChangedAL);
                            if (sampleChangedAL.Count == 1)
                            {
                                this.cmbSample.SelectedIndexChanged -= new EventHandler(this.cmbSample_SelectedIndexChanged);
                                this.cmbSample.SelectedIndex = 0;
                                this.cmbSample.SelectedIndexChanged += new EventHandler(this.cmbSample_SelectedIndexChanged);

                            }
                            if (isEdit)
                            {
                                this.cmbSample.SelectedIndexChanged -= new EventHandler(this.cmbSample_SelectedIndexChanged);
                                this.cmbSample.SelectedIndex = 0;
                                this.cmbSample.SelectedIndexChanged += new EventHandler(this.cmbSample_SelectedIndexChanged);
                            }
                            List<Neusoft.HISFC.Models.Order.MedicalTerm> list = new List<Neusoft.HISFC.Models.Order.MedicalTerm>();
                            foreach (Neusoft.HISFC.Models.Order.MedicalTerm medicalInfo in this.currventUnSelected)
                            {
                                if (medicalInfo.ID == medicalTermInfo.ID)
                                {
                                    continue;
                                }
                                bool isExist = false;
                                //样本类型不包括所选样本则锁定
                                if (medicalInfo.CheckBody != "")
                                {
                                    string sampleInfo = medicalInfo.CheckBody;
                                    if (!medicalInfo.CheckBody.Contains("|"))
                                    {
                                        sampleInfo += "|";
                                    }
                                    string[] checkBody = sampleInfo.Split('|');
                                    
                                    foreach (string info in checkBody)
                                    {
                                        isExist = false;
                                        if (info != "")
                                        {
                                            foreach (Neusoft.NFC.Object.NeuObject m in sampleChangedAL)
                                            {
                                                if (info == m.ID)
                                                {
                                                    isExist = true;
                                                    break;
                                                }
                                            }
                                            if (isExist)
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (medicalInfo.CheckBody == "" || (medicalInfo.CheckBody != "" && !isExist))
                                {
                                    string[] cellIndex = medicalInfo.User01.Split('|');
                                    view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[0]),
                                                    Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[1])].Locked = true;
                                    view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[0]),
                                                    Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[1])].BackColor = Color.LightGray;
                                    view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[0]),
                                                   Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[1])-this.termColumn+1].BackColor = Color.LightGray;
                                    Neusoft.NFC.Object.NeuObject obj = new Neusoft.NFC.Object.NeuObject();
                                    obj.ID = cellIndex[0];
                                    obj.Name = cellIndex[1];
                                    this.sampleForbidTerm.Add(obj);
                                    //this.currventForbid.Add(medicalInfo);
                                }
                                else
                                {
                                    list.Add(medicalInfo);
                                }
                            }
                            this.currventUnSelected = list;
                            this.currventUnSelected.Add(medicalTermInfo);

                        }
                    }
                    #endregion
                    //当前单元格的坐标
                    string[] activecellIndex = medicalTermInfo.User01.Split('|');

                    #region 取消选择
                    //取消选择
                    if (!(bool)view.ActiveCell.Value)
                    {
                        //将当前对照关系从所有已选择的医嘱对应的物价项目集合中移除
                        foreach (Neusoft.NFC.Object.NeuObject obj in this.compareAl)
                        {
                            if (obj.Memo == medicalTermInfo.ID)
                            {
                                Neusoft.NFC.Object.NeuObject objUndrug = htUndrugAll[obj.Memo] as Neusoft.NFC.Object.NeuObject;

                                string[] index = objUndrug.ID.Split('|');
                                foreach (string info in index)
                                {
                                    if (info != "")
                                    {
                                        //{37E6A2CF-3100-468a-8F9F-616C3F89660D}  采血管合并 20100105
                                        if (Neusoft.UFC.Order.OutPatient.Classes.Function.HelperCuvette.GetObjectFromID(info) != null)
                                        {//采血管不算
                                            continue;
                                        }
                                        this.htUndrug.Remove(info);
                                    }
                                }
                            }
                        }
                        //增加到未选择的医嘱集合中
                        this.currventUnSelected.Add(medicalTermInfo);
                        this.currventSelected.Remove(medicalTermInfo);
                        
                        if (this.mark == "1")
                        {
                            if (this.currventSelected.Count == 0)
                            {
                                Neusoft.HISFC.Models.Order.MedicalTerm medicalTermC = null;
                                foreach (Neusoft.NFC.Object.NeuObject objInfo in this.sampleForbidTerm)
                                {
                                    medicalTermC = new Neusoft.HISFC.Models.Order.MedicalTerm();
                                    view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(objInfo.ID), Neusoft.NFC.Function.NConvert.ToInt32(objInfo.Name)].Locked = false;
                                    view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(objInfo.ID), Neusoft.NFC.Function.NConvert.ToInt32(objInfo.Name)].BackColor = Color.White;
                                    view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(objInfo.ID), Neusoft.NFC.Function.NConvert.ToInt32(objInfo.Name) - this.termColumn + 1].BackColor = Color.White;
                                    medicalTermC = view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(objInfo.ID),
                                        Neusoft.NFC.Function.NConvert.ToInt32(objInfo.Name)].Tag as Neusoft.HISFC.Models.Order.MedicalTerm;

                                    this.currventUnSelected.Add(medicalTermC);

                                }
                                ArrayList al = this.htSelectedCell[medicalTermInfo.User01] as ArrayList;
                                foreach (Neusoft.NFC.Object.NeuObject objInfos in al)
                                {
                                    medicalTermC = new Neusoft.HISFC.Models.Order.MedicalTerm();
                                    if (view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(objInfos.ID), Neusoft.NFC.Function.NConvert.ToInt32(objInfos.Name)].Locked)
                                    {
                                        view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(objInfos.ID), Neusoft.NFC.Function.NConvert.ToInt32(objInfos.Name)].Locked = false;
                                        view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(objInfos.ID), Neusoft.NFC.Function.NConvert.ToInt32(objInfos.Name)].BackColor = Color.White;
                                        view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(objInfos.ID), Neusoft.NFC.Function.NConvert.ToInt32(objInfos.Name) - this.termColumn + 1].BackColor = Color.White;

                                        medicalTermC = view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(objInfos.ID),
                                            Neusoft.NFC.Function.NConvert.ToInt32(objInfos.Name)].Tag as Neusoft.HISFC.Models.Order.MedicalTerm;

                                        this.currventUnSelected.Add(medicalTermC);
                                    }
                                }
                                this.currventSelected = new List<Neusoft.HISFC.Models.Order.MedicalTerm>();

                                this.sampleForbidTerm.Clear();
                                this.htSelectedCell.Clear();
                                this.cmbSample.AddItems(this.sampleAl);
                                this.sampleCode = "";
                                this.htSelectedCell.Clear();
                                this.htUndrug.Clear();
                                this.AddDataToFp(this.currventOrderList);
                            }
                            else if (this.currventSelected.Count > 0)
                            {
                                this.cmbSample.ClearItems();
                                ArrayList sampleList = new ArrayList();
                                string sample = this.currventSelected[0].CheckBody;
                                if (!medicalTermInfo.CheckBody.Contains("|"))
                                {
                                    sample += "|";
                                }
                                string[] sampleIndex0 = sample.Split('|');
                                if (this.currventSelected.Count > 1)
                                {
                                    bool isExist = false;
                                    for (int i = 1; i < this.currventSelected.Count; i++)
                                    {
                                        sample = this.currventSelected[i].CheckBody;
                                        if (!medicalTermInfo.CheckBody.Contains("|"))
                                        {
                                            sample += "|";
                                        }
                                        string[] sampleIndexI = sample.Split('|');
                                        ArrayList sampleChangedAL = new ArrayList();
                                        foreach (string s in sampleIndexI)
                                        {
                                            isExist = false;
                                            if (s != "")
                                            {
                                                foreach (string index in sampleIndex0)
                                                {
                                                    if (index != "" && index == s)
                                                    {
                                                        isExist = true;
                                                        break;
                                                    }
                                                }
                                                if (isExist)
                                                {
                                                    //{7267BF81-14C9-40d2-B6E8-F1D887E9AD1B} 提取共用标本LABSAMPLE 20100118
                                                    //Neusoft.NFC.Object.NeuObject sampleObj = this.conMgr.GetConstant("LABSAMPLE", s);
                                                    Neusoft.NFC.Object.NeuObject sampleObj = Neusoft.UFC.Order.OutPatient.Classes.Function.HelperLabSample.GetObjectFromID(s);
                                                    bool isExistSample = false;
                                                    foreach (Neusoft.NFC.Object.NeuObject objSample in sampleList)
                                                    {
                                                        if (objSample.ID==sampleObj.ID)
                                                        {
                                                            isExistSample = true;
                                                        }
                                                    }
                                                    if (!isExistSample)
                                                    {
                                                        sampleList.Add(sampleObj);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (this.currventSelected.Count == 1)
                                {
                                    foreach (string s in sampleIndex0)
                                    {
                                        if (s != "")
                                        {
                                            //{7267BF81-14C9-40d2-B6E8-F1D887E9AD1B} 提取共用标本LABSAMPLE 20100118
                                            //Neusoft.NFC.Object.NeuObject sampleObj = this.conMgr.GetConstant("LABSAMPLE", s);
                                            Neusoft.NFC.Object.NeuObject sampleObj = UFC.Order.OutPatient.Classes.Function.HelperLabSample.GetObjectFromID(s);
                                            sampleList.Add(sampleObj);
                                        }
                                    }
                                }
                                this.cmbSample.ClearItems();
                                this.cmbSample.AddItems(sampleList);
                                if (sampleList.Count == 1)
                                {
                                    this.cmbSample.SelectedIndexChanged -= new EventHandler(this.cmbSample_SelectedIndexChanged);
                                    this.cmbSample.SelectedIndex = 0;
                                    this.cmbSample.SelectedIndexChanged += new EventHandler(this.cmbSample_SelectedIndexChanged);
                                }
                            }
                        }

                        //去当前医嘱对应的排斥项目坐标集，存放在”检验项目“列的Tag中
                        ArrayList cellList = view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(activecellIndex[0]),
                            Neusoft.NFC.Function.NConvert.ToInt32(activecellIndex[1]) - this.termColumn + 1].Tag as ArrayList;
                        if (cellList == null || cellList.Count == 0)
                        {
                            return;
                        }

                        foreach (Neusoft.NFC.Object.NeuObject forbidCell in cellList)
                        {
                            bool isExist = false;
                            //判断当前医嘱对应的排斥项目坐标集在被样本类型排斥的坐标集中是否存在
                            if (this.mark == "2")
                            {
                                foreach (Neusoft.NFC.Object.NeuObject objInfo in this.sampleForbidTerm)
                                {
                                    if (forbidCell.ID == objInfo.ID && forbidCell.Name == objInfo.Name)
                                    {
                                        isExist = true;
                                        break;
                                    }
                                }
                            }
                            //不存在则判断在其他已选择的医嘱对应的排斥项目坐标集中是否存在
                            if (!isExist)
                            {
                                Neusoft.NFC.Object.NeuObject objinfo2 = new Neusoft.NFC.Object.NeuObject();
                                objinfo2.ID = activecellIndex[0];
                                objinfo2.Name = activecellIndex[1];
                                ArrayList cellList1 = this.htSelectedCell[medicalTermInfo.User01] as ArrayList;
                                this.htSelectedCell.Remove(medicalTermInfo.User01);

                                foreach (DictionaryEntry dtEntrt in htSelectedCell)
                                {
                                    foreach (Neusoft.NFC.Object.NeuObject objInfo4 in (dtEntrt.Value as ArrayList))
                                    {
                                        if (forbidCell == objInfo4)
                                        {
                                            isExist = true;
                                            break;
                                        }
                                    }
                                    if (isExist)
                                    {
                                        break;
                                    }


                                }
                                //仍然不存在则说明该单元格可以解锁
                                if (!isExist)
                                {
                                    view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(forbidCell.ID), Neusoft.NFC.Function.NConvert.ToInt32(forbidCell.Name)].Locked = false;
                                    view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(forbidCell.ID), Neusoft.NFC.Function.NConvert.ToInt32(forbidCell.Name)].BackColor = Color.White;
                                    view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(forbidCell.ID), Neusoft.NFC.Function.NConvert.ToInt32(forbidCell.Name)-termColumn+1].BackColor = Color.White;
                                    Neusoft.HISFC.Models.Order.MedicalTerm medicalTermC = view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(forbidCell.ID),
                                        Neusoft.NFC.Function.NConvert.ToInt32(forbidCell.Name)].Tag as Neusoft.HISFC.Models.Order.MedicalTerm;

                                    this.currventUnSelected.Add(medicalTermC);

                                }
                                
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    #endregion
                    #region 选中
                    //选择
                    else
                    {
                        //添加到所有已选择的医嘱对应的物价项目集合中
                        foreach (Neusoft.NFC.Object.NeuObject obj in this.compareAl)
                        {
                            if (obj.Memo == medicalTermInfo.ID)
                            {
                                Neusoft.NFC.Object.NeuObject objUndrug = htUndrugAll[obj.Memo] as Neusoft.NFC.Object.NeuObject;

                                string[] index = objUndrug.ID.Split('|');
                                foreach (string info in index)
                                {
                                    if (!this.htUndrug.Contains(info) && info != "")
                                    {
                                        //{37E6A2CF-3100-468a-8F9F-616C3F89660D}  采血管合并 20100105
                                        if (Neusoft.UFC.Order.OutPatient.Classes.Function.HelperCuvette.GetObjectFromID(info) != null)
                                        {//采血管不算
                                            continue;
                                        }
                                        this.htUndrug.Add(info, "");
                                    }
                                }

                            }
                        }
                        this.currventUnSelected.Remove(medicalTermInfo);

                        //暂存医嘱信息
                        this.currventSelected.Add(medicalTermInfo);
                        
                        //项目排斥-物价排斥
                        List<Neusoft.HISFC.Models.Order.MedicalTerm> list = new List<Neusoft.HISFC.Models.Order.MedicalTerm>();
                        //清空当前选择的医嘱对应的物价排斥项目
                        view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(activecellIndex[0]),
                        Neusoft.NFC.Function.NConvert.ToInt32(activecellIndex[1]) - termColumn + 1].Tag = "";

                        foreach (Neusoft.HISFC.Models.Order.MedicalTerm medicalInfo in this.currventUnSelected)
                        {
                            if (medicalInfo.ID == medicalTermInfo.ID)
                            {
                                continue;
                            }
                            if (!htUndrugAll.Contains(medicalInfo.ID))
                            {
                                break;
                            }
                            string[] undrugIndex = ((Neusoft.NFC.Object.NeuObject)htUndrugAll[medicalInfo.ID]).ID.Split('|');
                            string[] cellIndex = medicalInfo.User01.Split('|');

                            bool isExist = false;
                            //物价排斥
                            foreach (string undrugID in undrugIndex)
                            {
                                if (htUndrug.Contains(undrugID) && undrugID != "")
                                {
                                    view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[0]),
                                        Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[1])].Locked = true;
                                    view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[0]),
                                        Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[1])].BackColor = Color.LightGray;
                                    view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[0]),
                                        Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[1])-termColumn+1].BackColor = Color.LightGray;
                                    Neusoft.NFC.Object.NeuObject obj = new Neusoft.NFC.Object.NeuObject();
                                    obj.ID = activecellIndex[0];
                                    obj.Name = activecellIndex[1];
                                    ArrayList CellList = new ArrayList();
                                    if (view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(obj.ID), Neusoft.NFC.Function.NConvert.ToInt32(obj.Name) - this.termColumn + 1].Tag == null ||
                                        view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(obj.ID), Neusoft.NFC.Function.NConvert.ToInt32(obj.Name) - this.termColumn + 1].Tag.ToString() == "")
                                    {
                                        view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(obj.ID), Neusoft.NFC.Function.NConvert.ToInt32(obj.Name) - this.termColumn + 1].Tag = CellList;
                                    }
                                    else
                                    {
                                        CellList = view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(obj.ID), Neusoft.NFC.Function.NConvert.ToInt32(obj.Name) - this.termColumn + 1].Tag as ArrayList;
                                    }
                                    Neusoft.NFC.Object.NeuObject objCell = new Neusoft.NFC.Object.NeuObject();
                                    objCell.ID = cellIndex[0];
                                    objCell.Name = cellIndex[1];
                                    CellList.Add(objCell);
                                    //this.currventForbid.Add(medicalInfo);
                                    isExist = true;
                                    break;
                                }
                            }
                            if (isExist)
                            {
                                continue;
                            }
                            else
                            {
                                //项目排斥
                                if (medicalTermInfo.MachineNO != medicalInfo.MachineNO)
                                {

                                    view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[0]),
                                        Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[1])].Locked = true;
                                    view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[0]),
                                        Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[1])].BackColor = Color.LightGray;
                                    view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[0]),
                                        Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[1])-termColumn+1].BackColor = Color.LightGray;
                                    Neusoft.NFC.Object.NeuObject obj = new Neusoft.NFC.Object.NeuObject();
                                    obj.ID = activecellIndex[0];
                                    obj.Name = activecellIndex[1];
                                    ArrayList CellList = new ArrayList();
                                    if (view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(obj.ID), Neusoft.NFC.Function.NConvert.ToInt32(obj.Name) - this.termColumn + 1].Tag == null ||
                                        view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(obj.ID), Neusoft.NFC.Function.NConvert.ToInt32(obj.Name) - this.termColumn + 1].Tag.ToString() == "")
                                    {
                                        view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(obj.ID), Neusoft.NFC.Function.NConvert.ToInt32(obj.Name) - this.termColumn + 1].Tag = CellList;
                                    }
                                    else
                                    {
                                        CellList = view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(obj.ID), Neusoft.NFC.Function.NConvert.ToInt32(obj.Name) - this.termColumn + 1].Tag as ArrayList;
                                    }
                                    Neusoft.NFC.Object.NeuObject objCell = new Neusoft.NFC.Object.NeuObject();
                                    objCell.ID = cellIndex[0];
                                    objCell.Name = cellIndex[1];
                                    CellList.Add(objCell);
                                    //this.currventForbid.Add(medicalInfo);
                                }
                                else
                                {
                                    list.Add(medicalInfo);
                                }

                            }

                        }
                        this.currventUnSelected = list;
                        Neusoft.NFC.Object.NeuObject obj5 = new Neusoft.NFC.Object.NeuObject();
                        obj5.ID = activecellIndex[0];
                        obj5.Name = activecellIndex[1];
                        ArrayList cellList2 = view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(obj5.ID), Neusoft.NFC.Function.NConvert.ToInt32(obj5.Name) - this.termColumn + 1].Tag as ArrayList;
                        if (cellList2 == null)
                        {
                            cellList2 = new ArrayList();
                        }
                        if (!this.htSelectedCell.ContainsKey(obj5.ID + "|" + obj5.Name))
                        {
                            this.htSelectedCell.Add(obj5.ID + "|" + obj5.Name, cellList2);
                        }
                    }
                    #endregion
                }
            }
        }
        private void InitOrderList()
        {
            if (this.IsClinic)
            {
                this.outOrderList = new List<Neusoft.HISFC.Models.Order.OutPatient.OrderTerm>();
            }
            else
            {
                this.inOrderList = new List<Neusoft.HISFC.Models.Order.Inpatient.OrderTerm>();
            }
        }

        private int GetOrder()
        {
            if(this.currventSelected.Count==0)
            {
                InitOrderList();
                return 0;
            }
            if (this.cmbSample.Tag == null || this.cmbSample.Tag.ToString() == "")
            {
                MessageBox.Show("请选择标本!", "提示");
                return -1;
            }
            string comboID = "";
            try
            {
                comboID = orderManager.GetNewOrderComboID();//添加组合号;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Neusoft.NFC.Management.Language.Msg("获取医嘱组合号出错" + ex.Message));
                return -1;
            }
            foreach (Neusoft.HISFC.Models.Order.MedicalTerm medicalTerm in this.currventSelected)
            {
                if (IsClinic)
                {
                    Neusoft.HISFC.Models.Order.OutPatient.OrderTerm orderTermInfo = new Neusoft.HISFC.Models.Order.OutPatient.OrderTerm();
                    if (orderTermInfo.Item.GetType() != typeof(Neusoft.HISFC.Models.Order.MedicalTerm))
                    {
                        orderTermInfo.Item = new Neusoft.HISFC.Models.Order.MedicalTerm();
                    }
                    orderTermInfo.Patient.Pact = this.patientInfo.Pact;
                    orderTermInfo.Patient.PID = this.patientInfo.PID;
                    orderTermInfo.OrderTermID = medicalTerm.ID;
                    orderTermInfo.Item.ID = medicalTerm.ID;
                    orderTermInfo.Item.Name = medicalTerm.Name;
                    orderTermInfo.IsEmergency = this.cbxQuick.Checked;
                    orderTermInfo.Item.ItemType = medicalTerm.ItemType;
                    orderTermInfo.IsSubtbl = false;
                    orderTermInfo.Combo.ID = comboID;
                    orderTermInfo.Item.SysClass.ID = medicalTerm.SysClass.ID;
                    orderTermInfo.ReciptDept = this.emp.Dept;
                    orderTermInfo.ReciptDoctor.ID = this.emp.ID;
                    orderTermInfo.ReciptDoctor.Name = this.emp.Name;
                    orderTermInfo.ExeDept.ID = this.cmbDept.Tag.ToString();
                    orderTermInfo.ExeDept.Name = this.cmbDept.Text;
                    orderTermInfo.CheckPartRecord = this.cmbSample.Tag.ToString();
                    orderTermInfo.Sample.ID = this.cmbSample.Tag.ToString();
                    orderTermInfo.Sample.Name = this.cmbSample.Text;
                    orderTermInfo.Qty = 1;
                    if (medicalTerm.PriceUnit == "")
                    {
                        orderTermInfo.Unit = "人次";
                    }
                    else
                    {
                        orderTermInfo.Unit = medicalTerm.PriceUnit;
                    }
                    //{EB33C62A-C82A-4517-8181-3776137950C9} 检验加备注 20100119
                    orderTermInfo.Memo = this.cmbMemo.Text;

                    this.OrderAL.Add(orderTermInfo);
                }
                else
                {
                    Neusoft.HISFC.Models.Order.Inpatient.OrderTerm orderTermInfo = new Neusoft.HISFC.Models.Order.Inpatient.OrderTerm(); if (orderTermInfo.Item.GetType() != typeof(Neusoft.HISFC.Models.Order.MedicalTerm))
                    {
                        orderTermInfo.Item = new Neusoft.HISFC.Models.Order.MedicalTerm();
                    }
                    orderTermInfo.Patient.Pact = this.patientInfo.Pact;
                    orderTermInfo.Patient.PID = this.patientInfo.PID;
                    orderTermInfo.OrderTermID = medicalTerm.ID;
                    orderTermInfo.Item.ID = medicalTerm.ID;
                    orderTermInfo.Item.Name = medicalTerm.Name;
                    orderTermInfo.IsEmergency = this.cbxQuick.Checked;
                    orderTermInfo.Item.ItemType = medicalTerm.ItemType;
                    orderTermInfo.IsSubtbl = false;
                    orderTermInfo.Combo.ID = comboID;
                    orderTermInfo.Item.SysClass.ID = medicalTerm.SysClass.ID;
                    orderTermInfo.ReciptDept = this.emp.Dept;
                    orderTermInfo.ReciptDoctor.ID = this.emp.ID;
                    orderTermInfo.ReciptDoctor.Name = this.emp.Name;
                    orderTermInfo.ExeDept.ID = this.cmbDept.Tag.ToString();
                    orderTermInfo.ExeDept.Name = this.cmbDept.Text;
                    orderTermInfo.CheckPartRecord = this.cmbSample.Tag.ToString();
                    orderTermInfo.Sample.ID = this.cmbSample.Tag.ToString();
                    orderTermInfo.Sample.Name = this.cmbSample.Text;
                    orderTermInfo.Qty = 1;

                    orderTermInfo.BeginTime = this.dtpSendTime.Value;

                    if (medicalTerm.PriceUnit == "")
                    {
                        orderTermInfo.Unit = "人次";
                    }
                    else
                    {
                        orderTermInfo.Unit = medicalTerm.PriceUnit;
                    }
                    //{EB33C62A-C82A-4517-8181-3776137950C9} 检验加备注 20100119
                    orderTermInfo.Memo = this.cmbMemo.Text;

                    this.InOrderList.Add(orderTermInfo);
                }
                
                //this.orderAL.Add(orderTermInfo);
            }
            return 1;
        }

        private int AddOrder()
        {
            if (this.orderList.Count>0)
            {
                Neusoft.HISFC.Models.Order.Order orderTerm = this.orderList[0] as Neusoft.HISFC.Models.Order.Order;

                if (orderTerm.Item.ID == "")
                {
                    return 0;
                }
                
                List<Neusoft.HISFC.Models.Order.MedicalTerm> termInfoList = this.medicalTermMgr.QueryMedicalTermByID(orderTerm.Item.ID);
                if (termInfoList == null)
                {
                    MessageBox.Show("未取到医嘱信息！" + this.medicalTermMgr.Err);
                    return -1;
                }
                if (termInfoList.Count == 0)
                {
                    return 0;
                }
                this.cmbDept.Tag = orderTerm.ExeDept.ID;
                foreach (Control c in this.neuPanel1.Controls)
                {
                    if (c.GetType() == typeof(Neusoft.NFC.Interface.Controls.NeuRadioButton))
                    {
                        Neusoft.NFC.Interface.Controls.NeuRadioButton radioBtn = c as Neusoft.NFC.Interface.Controls.NeuRadioButton;
                        if (radioBtn.Name == termInfoList[0].ItemArea)
                        {
                            //ArrayList orderTermList = this.orderList.Clone() as ArrayList;
                            radioBtn.Checked = true;
                            isEdit = true;
                            //this.orderList = orderTermList;
                            break;
                        }
                    }
                }
                this.cmbSample.Tag = orderTerm.Sample.ID;
                this.cbxQuick.Checked = orderTerm.IsEmergency;

                //{EB33C62A-C82A-4517-8181-3776137950C9} 检验加备注 20100119
                this.cmbMemo.Text = orderTerm.Memo;

                ArrayList al = new ArrayList();
                foreach (Neusoft.HISFC.Models.Order.MedicalTerm medicalTermInfo in this.currventUnSelected)
                {
                    al.Add(medicalTermInfo.Clone());
                }
                foreach (Neusoft.HISFC.Models.Order.Order orderTermInfo in this.orderList)
                {
                    foreach (Neusoft.HISFC.Models.Order.MedicalTerm medicalTermInfo in al)
                    {
                        if (orderTermInfo.Item.ID == medicalTermInfo.ID)
                        {
                            string[] cellIndex=medicalTermInfo.User01.Split('|');
                            view.SetActiveCell(Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[0]),
                                Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[1]));
                            view.ActiveCell.Value =true;
                            this.fpParTerm_ButtonClicked(null,null);
                        }
                    }
                }

            }
            return 1;
        }

        #endregion

        #region 事件

        private void radioBtn_CheckedChanged(object sender, EventArgs e)
        {
            
            foreach (Control c in this.neuPanel1.Controls)
            {
                if (c.GetType()==typeof(Neusoft.NFC.Interface.Controls.NeuRadioButton))
                {
                    Neusoft.NFC.Interface.Controls.NeuRadioButton radioBtn = c as Neusoft.NFC.Interface.Controls.NeuRadioButton;
                    if (radioBtn.Checked)
                    {
                        ClearInfo();
                        isEdit = false;
                        //this.orderList.Clear();
                        this.mark = radioBtn.Tag.ToString();
                        foreach (Neusoft.NFC.Object.NeuObject objInfo in this.al)
                        {
                            if (radioBtn.Name == objInfo.ID)
                            {
                                if (radioBtn.Tag != null || radioBtn.Tag.ToString() != "")
                                {
                                    this.mark = radioBtn.Tag.ToString();
                                }
                               
                                //根据检验大类查找检验医嘱信息
                                QueryOrderByType(objInfo);

                                if (objInfo.Name.Contains("病毒"))
                                {//特殊需求 "病毒"大类默认第一个样本
                                    if (this.cmbSample.alItems != null && this.cmbSample.alItems.Count > 0)
                                    {
                                        this.cmbSample.SelectedIndex = 0;
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
            }
            
        }

        private void QueryOrderByType(Neusoft.FrameWork.Models.NeuObject objInfo)
        {
            List<Neusoft.HISFC.Models.Order.MedicalTerm> tmpOrderTermList = this.medicalTermMgr.QueryMedicalTermBySysClass("UL");
            this.currventOrderList = new List<Neusoft.HISFC.Models.Order.MedicalTerm>();

            if (objInfo.ID == "")
            {
                tmpOrderTermList = this.medicalTermMgr.QueryMedicalTermByArea("ALL", "UL", this.cmbDept.Tag.ToString());
            }
            else
            {
                tmpOrderTermList = this.medicalTermMgr.QueryMedicalTermByArea(objInfo.ID, "UL", this.cmbDept.Tag.ToString());
            }
            
            foreach (Neusoft.HISFC.Models.Order.MedicalTerm term in tmpOrderTermList)
            {
                if (IsClinic && term.ApplicabilityArea == "2")
                {
                    continue;
                }
                //if (!isClinic)
                //{
                //    //国疗
                //    if (deptflag == "1")
                //    {
                //        if (term.ApplicabilityArea == ((int)Neusoft.HISFC.Models.Order.EnumAppArea.ALL).ToString() ||
                //            term.ApplicabilityArea == ((int)Neusoft.HISFC.Models.Order.EnumAppArea.ALLINPATIENT).ToString() ||
                //            term.ApplicabilityArea == ((int)Neusoft.HISFC.Models.Order.EnumAppArea.SPECIALALL).ToString() ||
                //            term.ApplicabilityArea == ((int)Neusoft.HISFC.Models.Order.EnumAppArea.SEPCIALINPATIENT).ToString())
                //        {
                //            this.currventOrderList.Add(term);
                //        }
                //    }
                //    //普通
                //    else
                //    {
                //        if (term.ApplicabilityArea == ((int)Neusoft.HISFC.Models.Order.EnumAppArea.ALL).ToString() ||
                //            term.ApplicabilityArea == ((int)Neusoft.HISFC.Models.Order.EnumAppArea.ALLINPATIENT).ToString() ||
                //            term.ApplicabilityArea == ((int)Neusoft.HISFC.Models.Order.EnumAppArea.NORMALALL).ToString() ||
                //            term.ApplicabilityArea == ((int)Neusoft.HISFC.Models.Order.EnumAppArea.NORMALINPATIENT).ToString())
                //        {
                //            this.currventOrderList.Add(term);
                //        }
                //    }
                //}
                else if (!IsClinic && term.ApplicabilityArea == "1")
                {
                    continue;
                }
                //else if (isClinic)
                //{
                //    国疗
                //    if (deptflag == "1")
                //    {
                //        if (term.ApplicabilityArea == "5")
                //        {
                //        }
                //        if (term.ApplicabilityArea == ((int)Neusoft.HISFC.Models.Order.EnumAppArea.ALL).ToString() ||
                //            term.ApplicabilityArea == ((int)Neusoft.HISFC.Models.Order.EnumAppArea.ALLCLINIC).ToString() ||
                //            term.ApplicabilityArea == ((int)Neusoft.HISFC.Models.Order.EnumAppArea.SPECIALALL).ToString() ||
                //            term.ApplicabilityArea == ((int)Neusoft.HISFC.Models.Order.EnumAppArea.SPECIALCLINIC).ToString())
                //        {
                //            this.currventOrderList.Add(term);
                //        }
                //    }
                //    普通
                //    else
                //    {
                //        if (term.ApplicabilityArea == ((int)Neusoft.HISFC.Models.Order.EnumAppArea.ALL).ToString() ||
                //           term.ApplicabilityArea == ((int)Neusoft.HISFC.Models.Order.EnumAppArea.ALLCLINIC).ToString() ||
                //           term.ApplicabilityArea == ((int)Neusoft.HISFC.Models.Order.EnumAppArea.NORMALALL).ToString() ||
                //           term.ApplicabilityArea == ((int)Neusoft.HISFC.Models.Order.EnumAppArea.NORMALCLINIC).ToString())
                //        {
                //            this.currventOrderList.Add(term);
                //        }
                //    }
                //}
                else
                {
                    this.currventOrderList.Add(term);
                }
            }
            
            if (this.currventOrderList == null)
            {
                MessageBox.Show("查询医嘱术语失败!" + this.medicalTermMgr.Err);
                //return;
            }

            this.AddDataToFp(this.currventOrderList);
            for (int i = 0; i < this.currventOrderList.Count; i++)
            {
                Neusoft.HISFC.Models.Order.MedicalTerm orderTermInfo = this.currventOrderList[i] as Neusoft.HISFC.Models.Order.MedicalTerm;
                this.currventUnSelected.Add(orderTermInfo.Clone());
            }
        }

        private void ClearInfo()
        {
            this.cmbSample.ClearItems();
            this.cmbSample.AddItems(sampleAl);
            this.cmbSample.Tag = "";
            this.currventOrderList = null;
            this.currventUnSelected = new List<Neusoft.HISFC.Models.Order.MedicalTerm>();
            this.currventSelected = new List<Neusoft.HISFC.Models.Order.MedicalTerm>();
            this.htSelectedCell.Clear();
            this.htUndrug.Clear();
            this.sampleForbidTerm.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.InitOrderList();
            this.orderList = new ArrayList();
            this.ParentForm.Close();
        }

        private void cmbSample_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbSample.Tag != null && this.cmbSample.Tag.ToString() != "")
            {
                if (this.mark == "1")
                {
                    if ((this.orderList.Count == 0 || (this.orderList.Count > 0 && (this.orderList[0] as Neusoft.HISFC.Models.Order.Order).Item.ID == "")) &&
                        this.currventSelected.Count == 0)
                    {
                        this.cmbSample.Tag = "";
                        MessageBox.Show("请先选择医嘱!", "提示");
                    }
                    //第一次选样本
                    if (this.currventSelected.Count > 0)
                    {
                        if (this.sampleCode == "")
                        {
                            List<Neusoft.HISFC.Models.Order.MedicalTerm> list1 = new List<Neusoft.HISFC.Models.Order.MedicalTerm>();
                            foreach (Neusoft.HISFC.Models.Order.MedicalTerm medicalInfo in this.currventUnSelected)
                            {
                                bool isExist = false;
                                //样本类型不包括所选样本则锁定
                                if (medicalInfo.CheckBody != "")
                                {
                                    string sample = medicalInfo.CheckBody;
                                    if (!medicalInfo.CheckBody.Contains("|"))
                                    {
                                        sample += "|";
                                    }
                                    string[] checkBody = sample.Split('|');

                                    foreach (string info in checkBody)
                                    {
                                        if (info != "" && info == this.cmbSample.Tag.ToString())
                                        {
                                            isExist = true;
                                            break;
                                        }
                                    }
                                }
                                if (medicalInfo.CheckBody == "" || (medicalInfo.CheckBody != "" && !isExist))
                                {
                                    string[] cellIndex = medicalInfo.User01.Split('|');
                                    view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[0]),
                                                    Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[1])].Locked = true;
                                    view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[0]),
                                                    Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[1])].BackColor = Color.LightGray;
                                    view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[0]),
                                        Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[1])-termColumn+1].BackColor = Color.LightGray;
                                    Neusoft.NFC.Object.NeuObject obj = new Neusoft.NFC.Object.NeuObject();
                                    obj.ID = cellIndex[0];
                                    obj.Name = cellIndex[1];
                                    this.sampleForbidTerm.Add(obj);
                                    //this.currventForbid.Add(medicalInfo);
                                }
                                else
                                {
                                    list1.Add(medicalInfo);
                                }
                            }
                            this.currventUnSelected = list1;
                        }
                        //之前已经选过一次
                        else if (this.sampleCode != this.cmbSample.Tag.ToString())
                        {
                            bool isExist = false;
                            List<Neusoft.HISFC.Models.Order.MedicalTerm> cellList2 = new List<Neusoft.HISFC.Models.Order.MedicalTerm>();
                            foreach (Neusoft.HISFC.Models.Order.MedicalTerm medicalTerm in this.currventUnSelected)
                            {
                                isExist = false;
                                string sample = medicalTerm.CheckBody;
                                if (!medicalTerm.CheckBody.Contains("|"))
                                {
                                    sample += "|";
                                }
                                string[] cellList = sample.Split('|');
                                foreach (string s in cellList)
                                {
                                    if (s != "" && s == this.cmbSample.Tag.ToString())
                                    {
                                        isExist = true;
                                        break;
                                    }
                                }
                                if (!isExist)
                                {
                                    string[] cellListInfo = medicalTerm.User01.Split('|');
                                    this.view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(cellListInfo[0]),
                                        Neusoft.NFC.Function.NConvert.ToInt32(cellListInfo[1])].Locked = true;
                                    this.view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(cellListInfo[0]),
                                        Neusoft.NFC.Function.NConvert.ToInt32(cellListInfo[1])].BackColor = Color.LightGray;
                                    view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(cellListInfo[0]),
                                        Neusoft.NFC.Function.NConvert.ToInt32(cellListInfo[1]) - termColumn + 1].BackColor = Color.LightGray;

                                    this.sampleForbidTerm.Add(new Neusoft.NFC.Object.NeuObject(cellListInfo[0], cellListInfo[1], ""));
                                }
                                else
                                {
                                    cellList2.Add(medicalTerm);
                                }
                            }
                            this.currventUnSelected = cellList2;
                            isExist = false;
                            foreach (Neusoft.NFC.Object.NeuObject objInfo in this.sampleForbidTerm)
                            {
                                isExist = false;
                                foreach (Neusoft.HISFC.Models.Order.MedicalTerm medicalTermInfo in this.currventSelected)
                                {
                                    if (htSelectedCell.Contains(medicalTermInfo.User01))
                                    {
                                        ArrayList forbidCellList = htSelectedCell[medicalTermInfo.User01] as ArrayList;


                                        foreach (Neusoft.NFC.Object.NeuObject objInfoForbid in forbidCellList)
                                        {
                                            if (objInfo.ID == objInfoForbid.ID && objInfo.Name == objInfoForbid.Name)
                                            {
                                                isExist = true;
                                                break;
                                            }
                                        }
                                        if (isExist)
                                        {
                                            break;
                                        }
                                    }

                                }
                                //如果仍然不存在判断是否包含所选的样本
                                if (!isExist)
                                {
                                    Neusoft.HISFC.Models.Order.MedicalTerm medical = this.view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(objInfo.ID),
                                     Neusoft.NFC.Function.NConvert.ToInt32(objInfo.Name)].Tag as Neusoft.HISFC.Models.Order.MedicalTerm;
                                    string sample = medical.CheckBody;
                                    if (!medical.CheckBody.Contains("|"))
                                    {
                                        sample += "|";
                                    }
                                    string[] checkBody = sample.Split('|');
                                    foreach (string s in checkBody)
                                    {
                                        if (s != "" && s == this.cmbSample.Tag.ToString())
                                        {
                                            this.view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(objInfo.ID),
                                                Neusoft.NFC.Function.NConvert.ToInt32(objInfo.Name)].Locked = false;
                                            this.view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(objInfo.ID),
                                                Neusoft.NFC.Function.NConvert.ToInt32(objInfo.Name)].BackColor = Color.White;
                                            this.view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(objInfo.ID),
                                                Neusoft.NFC.Function.NConvert.ToInt32(objInfo.Name)-termColumn+1].BackColor = Color.White;
                                            this.currventUnSelected.Add(medical);
                                        }
                                    }

                                }
                            }


                        }
                    }

                    if (this.cmbSample.Tag != null)
                    {
                        this.sampleCode = this.cmbSample.Tag.ToString();
                    }

                    return;

                }

                else if (this.mark == "2")
                {
                    if (this.currventSelected.Count >0 && this.cmbSample.Tag.ToString() != sampleCode)
                    {
                        if (MessageBox.Show("已选择医嘱，更换样本将会清除所有医嘱，是否继续？", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            this.cmbSample.Tag = sampleCode;
                            return;
                        }
                    }
                }
            this.sampleCode = this.cmbSample.Tag.ToString();
            this.currventSelected = new List<Neusoft.HISFC.Models.Order.MedicalTerm>();
            this.currventUnSelected = new List<Neusoft.HISFC.Models.Order.MedicalTerm>();
            this.AddDataToFp(this.currventOrderList);
            //标本排斥
            List<Neusoft.HISFC.Models.Order.MedicalTerm> list = new List<Neusoft.HISFC.Models.Order.MedicalTerm>();
            //this.currventForbid = new List<Neusoft.HISFC.Models.Order.MedicalTerm>();
            this.sampleForbidTerm.Clear();
            this.htSelectedCell.Clear();
            foreach (Neusoft.HISFC.Models.Order.MedicalTerm medicalInfo in this.currventOrderList)
            {
                bool isExist = false;
                //样本类型不包括所选样本则锁定
                if (medicalInfo.CheckBody != "")
                {
                    string sample = medicalInfo.CheckBody;
                    if (!medicalInfo.CheckBody.Contains("|"))
                    {
                        sample += "|";
                    }
                    string[] checkBody = sample.Split('|');

                    foreach (string info in checkBody)
                    {
                        if (info != "" && info == this.cmbSample.Tag.ToString())
                        {
                            isExist = true;
                            break;
                        }
                    }
                }
                if (medicalInfo.CheckBody == "" || (medicalInfo.CheckBody != "" && !isExist))
                {
                    string[] cellIndex = medicalInfo.User01.Split('|');
                    view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[0]),
                                    Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[1])].Locked = true;
                    view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[0]),
                                    Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[1])].BackColor = Color.LightGray;
                    view.Cells[Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[0]),
                                    Neusoft.NFC.Function.NConvert.ToInt32(cellIndex[1])-termColumn+1].BackColor = Color.LightGray;
                    Neusoft.NFC.Object.NeuObject obj = new Neusoft.NFC.Object.NeuObject();
                    obj.ID = cellIndex[0];
                    obj.Name = cellIndex[1];
                    this.sampleForbidTerm.Add(obj);
                    //this.currventForbid.Add(medicalInfo);
                }
                else
                {
                    list.Add(medicalInfo);
                }
            }
            this.currventUnSelected = list;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.GetOrder()<1)
            {
                return;
            }

            this.orderList = new ArrayList();
            this.ParentForm.Close();
        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
            {
                if (this.cmbDept.SelectedIndex != 0)
                {
                    ClearInfo();
                    for(int i=0;i< this.neuPanel1.Controls.Count;i++)
                    {
                        Control c=this.neuPanel1.Controls[i];
                        if (c.GetType() == typeof(Neusoft.NFC.Interface.Controls.NeuRadioButton))
                        {
                            this.neuPanel1.Controls.Remove(c);
                            i--;
                        }
                    }
                    this.mark = "1";
                    this.QueryOrderByType(new Neusoft.NFC.Object.NeuObject());

                }
                else if (this.cmbDept.SelectedIndex == 0)
                {
                    
                    this.QueryType();
                }
            }
        }

        private void fpParTerm_MouseMove(object sender, MouseEventArgs e)
        {
            FarPoint.Win.Spread.Model.CellRange cellRang= this.fpParTerm.GetCellFromPixel(0, 0, e.X, e.Y);
            if (cellRang.Row == -1 || cellRang.Column == -1)
            {
                this.lblPrice.Text = "";
                return;
            }
            int index = cellRang.Column / termColumn;
            int column=index * this.termColumn + termColumn - 1;
            Neusoft.HISFC.Models.Order.MedicalTerm medicalTermInfo = this.fpParTerm_Sheet1.Cells[cellRang.Row,column].Tag as Neusoft.HISFC.Models.Order.MedicalTerm;
            if (medicalTermInfo == null)
            {
                this.lblPrice.Text = "";
                return;
            }
            
            //查询物价项目
            if (htUndrugAll.Contains(medicalTermInfo.ID))
            {
                this.lblPrice.Text = ((Neusoft.NFC.Object.NeuObject)htUndrugAll[medicalTermInfo.ID]).Name;
            }
            else
            {
                this.lblPrice.Text = "该医嘱未对照物价项目，请对照！";
            }
        }

        private void cbxQuick_CheckedChanged(object sender, EventArgs e)
        {
            if (!isClinic)
            {
                if (cbxQuick.Checked)
                {
                    this.dtpSendTime.Value = this.orderManager.GetDateTimeFromSysDateTime().AddMinutes(3);
                }
                else
                {
                    this.dtpSendTime.Value = this.orderManager.GetDateTimeFromSysDateTime().Date.AddDays(1).AddHours(6);
                }
            }


        }

        #endregion

    }
}
