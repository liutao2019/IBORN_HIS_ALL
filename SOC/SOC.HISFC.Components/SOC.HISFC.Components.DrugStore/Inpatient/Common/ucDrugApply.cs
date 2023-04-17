using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.DrugStore.Inpatient.Common
{
    /// <summary>
    /// [功能描述: 住院病区患者摆药申请查询，发送]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// <修改记录 
    ///		
    ///  />
    /// </summary>
    public partial class ucDrugApply : ucDrugBase, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucDrugApply()
        {
            InitializeComponent();
        }


        bool isHavePrive = true;

        FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();
        Hashtable hsDrugMessage = new Hashtable();
        Hashtable hsDrugBill = new Hashtable();
        Hashtable hsPatient = new Hashtable();     

        #region 属性

        private string drugedApplyState = "2";

        /// <summary>
        /// 发药后的状态(和发药是否需要核准有关P00109)
        /// </summary>
        [Category("设置"), Browsable(true), Description("发药后的状态(和发药是否需要核准有关P00109)")]
        public string DrugedApplyState
        {
            get { return drugedApplyState; }
            set
            {
                if (value != "2" && value != "1")
                {
                    return;
                }
                drugedApplyState = value; 
            }
        }

        private bool isAllowDruged = false;

        /// <summary>
        /// 是否允许在此界面进行发药保存
        /// </summary>
        [Category("设置"), Browsable(true), Description("是否允许在此界面进行发药保存")]
        public bool IsAllowDruged
        {
            get { return isAllowDruged; }
            set { isAllowDruged = value; }
        }

        private bool isAllowApplyIn = false;

        /// <summary>
        /// 是否允许在此界面进行内部入库申请
        /// </summary>
        [Category("设置"), Browsable(false), Description("是否允许在此界面进行内部入库申请")]
        public bool IsAllowApplyIn
        {
            get { return isAllowApplyIn; }
            set { isAllowApplyIn = value; }
        }

        private bool isPrintWhenDruged = true;

        /// <summary>
        /// 发药保存后是否打印
        /// </summary>
        [Category("设置"), Browsable(true), Description("发药保存后是否打印")]
        public bool IsPrintWhenDruged
        {
            get { return isPrintWhenDruged; }
            set { isPrintWhenDruged = value; }
        }

        private int curApplyOutValidDays = 100;

        /// <summary>
        /// 形成摆药通知的发药申请有效天数
        /// </summary>
        [Category("设置"), Browsable(true), Description("形成摆药通知的发药申请有效天数")]
        public int ApplyOutValidDays
        {
            get { return curApplyOutValidDays; }
            set { curApplyOutValidDays = value; }
        }

        private int curMaxRowNumOnceSend = 10;

        /// <summary>
        /// 紧急发送的申请记录行数
        /// </summary>
        [Category("设置"), Browsable(true), Description("紧急发送的申请记录行数")]
        public int MaxRowNumOnceSend
        {
            get { return curMaxRowNumOnceSend; }
            set { curMaxRowNumOnceSend = value; }
        }

        private string curSplitPatientBillClassNO = "R,O";

        /// <summary>
        /// 保存时必须按照患者分单摆药单：R退药单，O出院带药
        /// </summary>
        [Description("保存时必须按照患者分单摆药单：R退药单，O出院带药"), Category("设置"), Browsable(true)]
        public string SplitPatientBillClassNO
        {
            get { return curSplitPatientBillClassNO; }
            set { curSplitPatientBillClassNO = value; }
        }

        private Function.EnumInpatintDrugApplyType curInpatintDrugApplyType = Function.EnumInpatintDrugApplyType.临时发送;

        /// <summary>
        /// 住院发药申请方式
        /// </summary>
        [Description("住院发药申请方式"), Category("设置"), Browsable(true)]
        public Function.EnumInpatintDrugApplyType InpatintDrugApplyType
        {
            get { return curInpatintDrugApplyType; }
            set { curInpatintDrugApplyType = value; }
        }

        /// <summary>
        /// 是否显示汇总打印
        /// </summary>
        private bool isShowTotalDrugBill = false;

        /// <summary>
        /// 是否显示汇总打印
        /// </summary>
        [Category("设置"), Description("是否显示汇总打印"), DefaultValue(false)]
        public bool IsShowTotalDrugBill
        {
            get
            {
                return this.isShowTotalDrugBill;
            }
            set
            {
                this.isShowTotalDrugBill = value;
            }

        }

        #endregion

        #region 初始化、查询、数据显示等

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            //如果是库房，则可以查看所有科室的申请
            //否则只可以查看操作员登录的科室
            ArrayList alDept = new ArrayList();
            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();

            if (this.PriveDept.Memo == "P" || this.PriveDept.Memo == "PI" || ((FS.HISFC.Models.Base.Employee)deptMgr.Operator).IsManager)
            {
                alDept.AddRange(deptMgr.GetDeptmentByType("N"));
                alDept.AddRange(deptMgr.GetDeptmentByType("T"));
                alDept.AddRange(deptMgr.GetDeptmentByType("OP"));
            }
            else
            {
                FS.HISFC.BizLogic.Manager.DepartmentStatManager deptStatMgr = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
                ArrayList alLogionDept = deptStatMgr.GetMultiDeptNew(deptStatMgr.Operator.ID);

                if (alLogionDept == null)
                {
                    alDept.Add(this.PriveDept.Clone());
                }
                foreach (FS.HISFC.Models.Base.DepartmentStat info in alLogionDept)
                {
                    FS.HISFC.Models.Base.Department dept = deptMgr.GetDeptmentById(info.DeptCode);
                    alDept.Add(dept);
                }
            }
            FS.HISFC.Models.Base.Department allTemp = new FS.HISFC.Models.Base.Department();
            allTemp.ID = "All";
            allTemp.Name = "全部";
            alDept.Insert(0, allTemp);
            this.cmbDept.AddItems(alDept);
            //>>{91460250-9B93-49ad-B203-1659D94E5227}增加开立科室
          
            this.cmbRecipeDept.AddItems(alDept);
            //<<

            try
            {
                if (this.priveDept.Memo != "P")
                {
                    this.cmbDept.Tag = ((FS.HISFC.Models.Base.Employee)deptMgr.Operator).Dept.ID;
                }
                else
                {
                    this.cmbDept.SelectedIndex = 0;
                }
            }
            catch { }
            this.SetConcentratedSendInfo();

            this.nlbBlue.Visible = (this.InpatintDrugApplyType == Function.EnumInpatintDrugApplyType.全区发送);
            this.nlbConcentratedSendInfo.Visible = (this.InpatintDrugApplyType == Function.EnumInpatintDrugApplyType.全区发送);

            this.neuDateTimePicker1.Value = this.neuDateTimePicker1.Value.Date;
            this.neuDateTimePicker2.Value = this.neuDateTimePicker2.Value.Date.AddDays(1);
            this.ngbRadix.Visible = this.IsAllowApplyIn;
            if (this.InpatintDrugApplyType == Function.EnumInpatintDrugApplyType.临时发送)
            {
                this.ncbUnSend.Visible = false;
                this.ncbApply.Text = "未执行";
            }
            else
            {
                this.ncbApply.Checked = false;
            }
            SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem("初始化");
            this.ncmbDrug.AddItems(SOC.HISFC.BizProcess.Cache.Pharmacy.itemHelper.ArrayObject);

            this.rbDrugBillList.Checked = FS.FrameWork.Function.NConvert.ToBoolean(SOC.Public.XML.SettingFile.ReadSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugApplySetting.xml", "ShowSetting", "ListType", "False"));

        }

        private void Query()
        {
            this.ucDrugDetail1.Clear();
            this.tvMessageBaseTree.Clear();

            string applyDeptNO = "All";

            if (this.cmbDept.Tag != null && !string.IsNullOrEmpty(this.cmbDept.Text))
            {
                applyDeptNO = this.cmbDept.Tag.ToString();
            }
            //>>{91460250-9B93-49ad-B203-1659D94E5227}
            string recipeDeptId = "All";
            if (this.cmbRecipeDept.Tag != null && !string.IsNullOrEmpty(this.cmbRecipeDept.Text))
            {
                recipeDeptId = this.cmbRecipeDept.Tag.ToString();
            }
            //<<
            string patient = "All";
            if (!string.IsNullOrEmpty(this.ntxtPatientNO.Text))
            {
                patient = this.ntxtPatientNO.Text;
            }
            if (FS.FrameWork.Public.String.TakeOffSpecialChar(patient) != patient)
            {
                MessageBox.Show("住院号处录入了特殊字符，请更改！");
                return;
            }
            //if (applyDeptNO == "All" && patient == "All")
            //{
            //    MessageBox.Show("请您要录入申请科室或患者住院号！");
            //    return;
            //}

            string state = "All";
            if (this.ncbApply.Checked)
            {
                state = "0";
            }
            if (this.ncbUnSend.Checked && this.InpatintDrugApplyType != Function.EnumInpatintDrugApplyType.临时发送)
            {
                state = "0";
            }
            if (this.ncbDruged.Checked)
            {
                state = this.DrugedApplyState;
            }
            if ((this.ncbApply.Checked 
                ||(this.ncbUnSend.Checked 
                && this.InpatintDrugApplyType!= Function.EnumInpatintDrugApplyType.临时发送)) 
                && this.ncbDruged.Checked)
            {
                state = "All";
            }
            string drugNO = "All";

            if (this.ncmbDrug.Tag != null && !string.IsNullOrEmpty(this.ncmbDrug.Text))
            {
                drugNO = this.ncmbDrug.Tag.ToString();
            }

            bool isRadix = this.ncbRadix.Checked | ((this.ncbUnapplyIn.Checked | this.ncbApplyIned.Checked) & this.ncbDruged.Checked);

            ArrayList alApplyOut = new ArrayList();



            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("请稍后...");
            Application.DoEvents();

            if (isRadix)
            {
                string applyInState = "All";
                if (this.ncbUnapplyIn.Checked)
                {
                    applyInState = "0";
                }
                if (this.ncbApplyIned.Checked)
                {
                    applyInState = "1";
                }
                if (this.ncbUnapplyIn.Checked && this.ncbApplyIned.Checked)
                {
                    applyInState = "All";
                }

                alApplyOut = drugStoreMgr.QueryRadixApplyOutList(applyDeptNO,recipeDeptId, this.neuDateTimePicker1.Value, this.neuDateTimePicker2.Value, patient, drugNO, state, applyInState);
            }
            else
            {
                alApplyOut = drugStoreMgr.QueryApplyOutList(applyDeptNO,recipeDeptId, this.neuDateTimePicker1.Value, this.neuDateTimePicker2.Value, patient, drugNO, state);
            }

            if (alApplyOut == null)
            {
                MessageBox.Show("获取科室发药申请发生错误，请与系统管理员联系并报告错误：" + drugStoreMgr.Err, "提示>>", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (this.rbDrugBillList.Checked)
            {
                this.ShowBill(alApplyOut);
            }
            else if (this.rbPatientList.Checked)
            {
                this.ShowPatient(alApplyOut);
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        private void ShowBill(ArrayList alApplyOut)
        {
            bool isStockDept = false;
            if (SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(this.priveDept.ID).DeptType.ID.ToString() == "P")
            {
                isStockDept = true;
            }
            ArrayList alDrugMessage = new ArrayList();
            ArrayList alDrugBill = new ArrayList();

            hsDrugMessage.Clear();
            hsDrugBill.Clear();

            this.tvMessageBaseTree.Clear();
            string key = "";
            int param =-2;
            string info = "";
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alApplyOut)
            {
                if (isStockDept)
                {
                    if (applyOut.StockDept.ID != this.priveDept.ID)
                    {
                        continue;
                    }
                }
                if (!this.ncbShowInvalidData.Checked)
                {
                    if (applyOut.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
                    {
                        continue;
                    }
                }
                if (this.ncbShowIQuitData.Checked)
                {
                    if (applyOut.BillClassNO != "R")
                    {
                        continue;
                    }

                }
                if (this.InpatintDrugApplyType == Function.EnumInpatintDrugApplyType.按单发送)
                {
                    if (!this.ncbUnSend.Checked && (string.IsNullOrEmpty(applyOut.DrugNO) || applyOut.DrugNO == "0") && applyOut.State == "0")
                    {
                        continue;
                    }
                    if (!this.ncbApply.Checked && (!string.IsNullOrEmpty(applyOut.DrugNO) && applyOut.DrugNO != "0") && applyOut.State == "0")
                    {
                        continue;
                    }
                }
                if (this.InpatintDrugApplyType == Function.EnumInpatintDrugApplyType.全区发送)
                {
                    if (param == -2)
                    {
                        param = this.drugStoreMgr.GetDrugConcentratedSendInfo(this.cmbDept.Tag.ToString(), ref info);
                    }
                    if (!this.ncbUnSend.Checked && param != 1 && applyOut.SendType == 2 && applyOut.State == "0")
                    {
                        continue;
                    }
                    if (!this.ncbApply.Checked && (param == 1 || applyOut.SendType != 2) && applyOut.State == "0")
                    {
                        continue;
                    }
                }
                //已经有单号的按照单号显示
                if (!string.IsNullOrEmpty(applyOut.DrugNO) && applyOut.DrugNO != "0")
                {
                    key = applyOut.DrugNO + applyOut.ApplyDept.ID + applyOut.StockDept.ID;
                    if (hsDrugBill.Contains(key))
                    {
                        (hsDrugBill[key] as ArrayList).Add(applyOut);
                    }
                    else
                    {
                        ArrayList alDrugBillApply = new ArrayList();
                        alDrugBillApply.Add(applyOut);
                        hsDrugBill.Add(key, alDrugBillApply);

                        FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass = new FS.HISFC.Models.Pharmacy.DrugBillClass();
                        drugBillClass.ApplyDept.ID = applyOut.ApplyDept.ID;
                        drugBillClass.ApplyDept.Name = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(applyOut.ApplyDept.ID);
                        drugBillClass.DrugBillNO = applyOut.DrugNO;
                        drugBillClass.ID = applyOut.BillClassNO;
                        drugBillClass.User02 = applyOut.StockDept.ID;
                        drugBillClass.Name = SOC.HISFC.BizProcess.Cache.Pharmacy.GetDrugBillClassName(applyOut.BillClassNO);

                        alDrugBill.Add(drugBillClass);
                    }
                    continue;
                }

                //没有单号的按照摆药通知格式显示
                key = applyOut.BillClassNO + applyOut.ApplyDept.ID + applyOut.StockDept.ID;
                if (this.InpatintDrugApplyType == Function.EnumInpatintDrugApplyType.全区发送)
                {
                    key += applyOut.SendType.ToString();
                }
                if (Function.Contains(this.SplitPatientBillClassNO, applyOut.BillClassNO))
                {
                    key += applyOut.PatientNO;
                }
                if (hsDrugMessage.Contains(key))
                {
                    (hsDrugMessage[key] as ArrayList).Add(applyOut);
                }
                else
                {
                    ArrayList alDrugMessageApply = new ArrayList();
                    alDrugMessageApply.Add(applyOut);
                    hsDrugMessage.Add(key, alDrugMessageApply);

                    FS.HISFC.Models.Pharmacy.DrugMessage drugMessage = new FS.HISFC.Models.Pharmacy.DrugMessage();
                    drugMessage.ApplyDept.ID = applyOut.ApplyDept.ID;
                    drugMessage.DrugBillClass.ID = applyOut.BillClassNO;
                    drugMessage.SendType = applyOut.SendType;
                   
                    FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass = drugStoreMgr.GetDrugBillClass(drugMessage.DrugBillClass.ID);
                    drugMessage.DrugBillClass.Name = drugBillClass.Name + "(" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(applyOut.StockDept.ID) + ")";
                    if (Function.Contains(this.SplitPatientBillClassNO, applyOut.BillClassNO))
                    {
                        string bedNO = applyOut.BedNO;
                        if (bedNO.Length > 4)
                        {
                            bedNO = bedNO.Substring(4);
                        }
                        //drugMessage.User01习惯上借用与患者住院流水号
                        drugMessage.User02 = applyOut.PatientNO;
                        drugMessage.DrugBillClass.Name = "【" + bedNO + "床】" + applyOut.PatientName + drugBillClass.Name + "(" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(applyOut.StockDept.ID) + ")";
                    }
                    drugMessage.StockDept.ID = applyOut.StockDept.ID;

                    alDrugMessage.Add(drugMessage);
                }
            }

            FS.HISFC.Models.Pharmacy.DrugControl drugControl = new FS.HISFC.Models.Pharmacy.DrugControl();
            drugControl.ShowLevel = 1;
            if (alDrugMessage.Count > 0)
            {
                this.tvMessageBaseTree.ShowDrugMessage(alDrugMessage, drugControl, false, null, true, (this.InpatintDrugApplyType == Function.EnumInpatintDrugApplyType.全区发送));
            }
            if (alDrugBill.Count > 0)
            {
                this.tvMessageBaseTree.ShowDrugBillClass(alDrugBill, true);
            }
           
        }

        private void ShowPatient(ArrayList alApplyOut)
        {
            ArrayList alDrugMessage = new ArrayList();
            this.hsPatient.Clear();
            this.tvMessageBaseTree.Clear();

            int param = -2;
            string info = "";
            ArrayList allDrugMessage = new ArrayList();
            Hashtable hsDrugMessageNode = new Hashtable();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alApplyOut)
            {
                if (!this.ncbShowInvalidData.Checked)
                {
                    if (applyOut.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
                    {
                        continue;
                    }
                }
                if (this.ncbShowIQuitData.Checked)
                {
                    if (applyOut.BillClassNO != "R")
                    {
                        continue;
                    }
                }
                if (this.InpatintDrugApplyType == Function.EnumInpatintDrugApplyType.按单发送)
                {
                    if (!this.ncbUnSend.Checked && (string.IsNullOrEmpty(applyOut.DrugNO) || applyOut.DrugNO == "0") && applyOut.State == "0")
                    {
                        continue;
                    }
                    if (!this.ncbApply.Checked && (!string.IsNullOrEmpty(applyOut.DrugNO) && applyOut.DrugNO != "0") && applyOut.State == "0")
                    {
                        continue;
                    }
                }
                if (this.InpatintDrugApplyType == Function.EnumInpatintDrugApplyType.全区发送)
                {
                    if (param == -2)
                    {
                        param = this.drugStoreMgr.GetDrugConcentratedSendInfo(this.cmbDept.Tag.ToString(), ref info);
                    }
                    if (!this.ncbUnSend.Checked && param != 1 && applyOut.SendType == 2 && applyOut.State == "0")
                    {
                        continue;
                    }
                    if (!this.ncbApply.Checked && (param == 1 || applyOut.SendType != 2) && applyOut.State == "0")
                    {
                        continue;
                    }
                }

                if (hsPatient.Contains("All"))
                {
                    ((ArrayList)hsPatient["All"]).Add(applyOut);
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(applyOut);
                    hsPatient.Add("All", al);
                }


                if (hsPatient.Contains(applyOut.PatientNO))
                {
                    (hsPatient[applyOut.PatientNO] as ArrayList).Add(applyOut);
                }
                else
                {
                    ArrayList alPatientApply = new ArrayList();
                    alPatientApply.Add(applyOut);
                    hsPatient.Add(applyOut.PatientNO,alPatientApply);

                    FS.HISFC.Models.Pharmacy.DrugMessage drugMessage = new FS.HISFC.Models.Pharmacy.DrugMessage();
                    drugMessage.ApplyDept.ID = applyOut.ApplyDept.ID;
                    drugMessage.DrugBillClass.ID = applyOut.BillClassNO;
                    FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass = SOC.HISFC.BizProcess.Cache.Pharmacy.GetDrugBillClass(drugMessage.DrugBillClass.ID);
                    drugMessage.DrugBillClass.Name = drugBillClass.Name + "(" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(applyOut.StockDept.ID) + ")";

                    drugMessage.StockDept.ID = applyOut.StockDept.ID;
                    drugMessage.User01 = applyOut.PatientNO;

                    alDrugMessage.Add(drugMessage);


                    TreeNode node = new TreeNode();
                    string bedNO = applyOut.BedNO;
                    if (bedNO.Length > 4)
                    {
                        bedNO = bedNO.Substring(4);
                    }

                    node.Text = "【" + bedNO + "床】" + applyOut.PatientName;
                    node.ImageIndex = 6;
                    node.SelectedImageIndex = 6;
                    node.Tag = drugMessage;

                    //if (this.tvMessageBaseTree.Nodes.Count == 0)
                    //{
                    //    TreeNode deptNode = new TreeNode();
                    //    deptNode.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(applyOut.ApplyDept.ID)+"";
                    //    this.tvMessageBaseTree.Nodes.Add(deptNode);

                    //    FS.HISFC.Models.Pharmacy.DrugMessage drugMessageRoot = drugMessage.Clone();
                    //    drugMessageRoot.User01 = "All";
                    //    deptNode.Tag = drugMessageRoot;
                    //}
                    if (allDrugMessage.Contains(drugMessage.ApplyDept.ID))
                    {
                        int index = this.tvMessageBaseTree.Nodes.IndexOf(hsDrugMessageNode[drugMessage.ApplyDept.ID] as TreeNode);
                        this.tvMessageBaseTree.Nodes[index].Nodes.Add(node);
                    }
                    else
                    {
                        TreeNode deptNode = new TreeNode();
                        deptNode.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(applyOut.ApplyDept.ID) + "";
                        this.tvMessageBaseTree.Nodes.Add(deptNode);

                        FS.HISFC.Models.Pharmacy.DrugMessage drugMessageRoot = drugMessage.Clone();
                        drugMessageRoot.User01 = "All";
                        deptNode.Tag = drugMessageRoot;
                        allDrugMessage.Add(drugMessage.ApplyDept.ID);
                        hsDrugMessageNode.Add(drugMessage.ApplyDept.ID, deptNode);
                        //this.tvMessageBaseTree.Nodes[0].Nodes.Add(deptNode);
                        int index = this.tvMessageBaseTree.Nodes.IndexOf(deptNode);
                        this.tvMessageBaseTree.Nodes[index].Nodes.Add(node);
                    }
                }
            }
            this.tvMessageBaseTree.ExpandAll();

        }

        /// <summary>
        /// 显示摆药通知信息
        /// </summary>
        /// <param name="drugMessage">摆药通知信息</param>
        private void ShowDrugMessage(ArrayList alDrugMessage)
        {
            if (alDrugMessage == null)
            {
                //this.ShowMessage("查询摆药通知发生错误：" + drugStoreMgr.Err, MessageBoxIcon.Error);
                return;
            }
            this.ucDrugDetail1.Clear();
            this.ucDrugDetail1.ShowDrugMessage(alDrugMessage, this.IsAutoSelected);
            this.ucDrugDetail1.SetTabPageVisible(true, false, false);
        }

        /// <summary>
        /// 显示摆药通知信息
        /// </summary>
        /// <param name="alDrugBill">摆药单数组</param>
        /// <param name="isAdd">是否追加到界面</param>
        private void ShowDrugBill(ArrayList alDrugBill, bool isAdd)
        {
            if (alDrugBill == null)
            {
                this.ShowMessage("查询摆药单发生错误：" + drugStoreMgr.Err, MessageBoxIcon.Error);
            }
            if (!isAdd)
            {
                this.ucDrugDetail1.Clear();
            }
            this.ucDrugDetail1.ShowDrugClassBill(alDrugBill, this.IsAutoSelected, isAdd);
            if (!isAdd)
            {
                this.ucDrugDetail1.SetTabPageVisible(true, false, false);
            }
        }

        /// <summary>
        /// 显示摆药通知信息
        /// </summary>
        /// <param name="drugMessage">摆药通知信息</param>
        private void ShowApplyOutDetail(FS.HISFC.Models.Pharmacy.DrugMessage drugMessage)
        {
            string info = "";
            int param = this.drugStoreMgr.GetDrugConcentratedSendInfo(drugMessage.ApplyDept.ID, ref info);

            ArrayList alApplyOut = new ArrayList();
            if (string.IsNullOrEmpty(drugMessage.User01))
            {
                string key = drugMessage.DrugBillClass.ID + drugMessage.ApplyDept.ID + drugMessage.StockDept.ID;

                if (this.InpatintDrugApplyType == Function.EnumInpatintDrugApplyType.全区发送)
                {
                    key += drugMessage.SendType.ToString();
                }

                //出院带药等需要分开患者显示
                key += drugMessage.User02;

                if (!hsDrugMessage.Contains(key))
                {
                    return;
                }
                alApplyOut = hsDrugMessage[key] as ArrayList;


                FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass = drugStoreMgr.GetDrugBillClass(drugMessage.DrugBillClass.ID);
                if (drugBillClass != null)
                {
                    if (drugBillClass.PrintType.ID.ToString() != "1")
                    {
                        this.ucDrugDetail1.SelectTabPage(2);
                    }

                    //明细显示
                    this.ucDrugDetail1.Clear();
                
                    this.ucDrugDetail1.ShowDetail(alApplyOut, false, this.EnumQtyShowType, true, this.curInpatintDrugApplyType, param == 1);

                    ArrayList alValidApplyOut = new ArrayList();
                    for (int index = 0; index < alApplyOut.Count; index++)
                    {
                        FS.HISFC.Models.Pharmacy.ApplyOut applyOut = alApplyOut[index] as FS.HISFC.Models.Pharmacy.ApplyOut;
                        if (applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid || applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Ignore)
                        {
                            continue;
                        }
                        alValidApplyOut.Add(applyOut);
                    }

                    List<FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill> listInpatientBill = this.IInpatientDrug.ShowDrugBill(alValidApplyOut, drugBillClass, drugMessage.StockDept);
                    this.ucDrugDetail1.ShowBill(listInpatientBill);

                    //本地接口返回的控件可能不止一个，返回明细则显示明细摆药单，返回汇总则显示汇总摆药单，返回两个则两个都显示
                    if (this.IInpatientDrug == null)
                    {
                        this.ShowMessage("没有实现单据打印接口：FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientDrug", MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    //维护数据被删除了，非严重错误
                    this.ShowMessage("查询摆药单分类信息出错：" + drugStoreMgr.Err, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                if (!hsPatient.Contains(drugMessage.User01))
                {
                    return;
                }
                alApplyOut = hsPatient[drugMessage.User01] as ArrayList;

                //明细显示
                this.ucDrugDetail1.Clear();
                this.ucDrugDetail1.ShowDetail(alApplyOut, false, this.EnumQtyShowType,true,this.curInpatintDrugApplyType,param==1);
                this.ucDrugDetail1.SetTabPageVisible(false, true, true);
                if (this.IsShowTotalDrugBill)
                {
                    FS.HISFC.Models.Pharmacy.DrugBillClass AA = new FS.HISFC.Models.Pharmacy.DrugBillClass();
                    AA.Name = "发药单";
                    AA.ID = "total";
                    string tempApplyDeptNO = "All";

                    if (this.cmbDept.Tag != null && !string.IsNullOrEmpty(this.cmbDept.Text))
                    {
                        tempApplyDeptNO = this.cmbDept.Tag.ToString();
                    }
                    AA.ApplyDept.ID = tempApplyDeptNO;
                    List<FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill> listTempBill = this.IInpatientDrug.ShowDrugBill(alApplyOut, AA, drugMessage.StockDept);
                    this.ucDrugDetail1.ShowBill(listTempBill);
                }
                this.ucDrugDetail1.SelectTabPage(2);
            }
        }

        /// <summary>
        /// 显示摆药通知信息
        /// </summary>
        /// <param name="drugBill">摆药通知信息</param>
        private void ShowApplyOutDetail(FS.HISFC.Models.Pharmacy.DrugBillClass drugBill)
        {
            ArrayList alApplyOut = new ArrayList();
            string key = drugBill.DrugBillNO + drugBill.ApplyDept.ID + drugBill.User02;
           
            string info = "";
            int param = this.drugStoreMgr.GetDrugConcentratedSendInfo(drugBill.ApplyDept.ID, ref info);

            if (!hsDrugBill.Contains(key))
            {
                return;
            }
            //alApplyOut = hsDrugBill[key] as ArrayList;
            alApplyOut = this.drugStoreMgr.QueryApplyOutListByBill(drugBill.DrugBillNO, "'0','1','2'");
            if (alApplyOut == null)
            {
                this.ShowMessage("根据单号获取摆药明细发生错误，请与系统管理员联系并报告错误：" + drugStoreMgr.Err);
                return;
            }
            if ((hsDrugBill[key] as ArrayList).Count != alApplyOut.Count)
            {
                this.ShowMessage("注意：您选择的单据申请时间不在选择的开始时间和结束时间内，系统忽略时间根据单号自动查询了整张单的数据");
            }

            FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass = drugStoreMgr.GetDrugBillClass(drugBill.ID);
            drugBillClass.DrugBillNO = drugBill.DrugBillNO;

            if (drugBillClass != null)
            {
                if (drugBillClass.PrintType.ID.ToString() != "1")
                {
                    this.ucDrugDetail1.SelectTabPage(2);
                }

                //明细显示
                this.ucDrugDetail1.Clear();
                this.ucDrugDetail1.ShowDetail(alApplyOut, false, this.EnumQtyShowType, true, this.curInpatintDrugApplyType,param==1);

                ArrayList alValidApplyOut = new ArrayList();
                for (int index = 0; index < alApplyOut.Count; index++)
                {
                    FS.HISFC.Models.Pharmacy.ApplyOut applyOut = alApplyOut[index] as FS.HISFC.Models.Pharmacy.ApplyOut;
                    if (applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid || applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Ignore)
                    {
                        continue;
                    }
                    alValidApplyOut.Add(applyOut);
                }

                List<FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill> listInpatientBill = this.IInpatientDrug.ShowDrugBill(alValidApplyOut, drugBillClass, SOC.HISFC.BizProcess.Cache.Common.GetDept(drugBill.User02));
                this.ucDrugDetail1.ShowBill(listInpatientBill);

                //本地接口返回的控件可能不止一个，返回明细则显示明细摆药单，返回汇总则显示汇总摆药单，返回两个则两个都显示
                if (this.IInpatientDrug == null)
                {
                    this.ShowMessage("没有实现单据打印接口：FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientDrug", MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void SelectAllDetailData(bool isSelectAll)
        {
            this.ucDrugDetail1.SelectAllData(isSelectAll);
        }

        private void SelectDetailDataWithTime()
        {
            FS.FrameWork.WinForms.Forms.frmChooseDate frmChooseDate = new FS.FrameWork.WinForms.Forms.frmChooseDate();
            frmChooseDate.Init();
            try
            {
                string time = SOC.Public.XML.SettingFile.ReadSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStore.xml", "CurDrugBeginUsetime", "Dept" + this.PriveDept.ID, frmChooseDate.DateBegin.ToString("HH:mm:ss"));
                DateTime dt = DateTime.Parse(frmChooseDate.DateBegin.ToString("yyyy-MM-dd " + time));
                frmChooseDate.DateBegin = dt;

                time = SOC.Public.XML.SettingFile.ReadSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStore.xml", "CurDrugEndUsetime", "Dept" + this.PriveDept.ID, frmChooseDate.DateEnd.ToString("HH:mm:ss"));
                dt = DateTime.Parse(frmChooseDate.DateEnd.ToString("yyyy-MM-dd " + time));
                frmChooseDate.DateEnd = dt;
            }
            catch { }

            frmChooseDate.ShowDialog(this);

            SOC.Public.XML.SettingFile.SaveSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStore.xml", "CurDrugBeginUsetime", "Dept" + this.PriveDept.ID, frmChooseDate.DateBegin.ToString("HH:mm:ss"));
            SOC.Public.XML.SettingFile.SaveSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStore.xml", "CurDrugEndUsetime", "Dept" + this.PriveDept.ID, frmChooseDate.DateEnd.ToString("HH:mm:ss"));
            this.ucDrugDetail1.SelectData(frmChooseDate.DateBegin, frmChooseDate.DateEnd);
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="alPrintData">applyout实体数组</param>
        /// <param name="drugMessage">摆药通知信息，主要是取得摆药单分类名称</param>
        /// <param name="billNO">摆药单号，drugMessage的备注中有，提出来看得明白一些</param>
        /// <param name="stockDept">实际发药科室</param>
        /// <returns></returns>
        private int Print(ArrayList alPrintData, FS.HISFC.Models.Pharmacy.DrugMessage drugMessage, string billNO, FS.FrameWork.Models.NeuObject stockDept)
        {
            if (this.IInpatientDrug == null)
            {
                this.ShowMessage("没有实现接口：FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientDrug", MessageBoxIcon.Error);
                return -1;
            }
            return this.IInpatientDrug.OnSavePrint(alPrintData, drugMessage, billNO, stockDept);
        }


        #endregion

        #region 发药保存
        /// <summary>
        /// 发药保存
        /// </summary>
        /// <returns></returns>
        private int DrugSave()
        {
            if (!this.IsAllowDruged)
            {
                this.ShowMessage("系统未设置发药保存，请与系统管理员联系！", MessageBoxIcon.Information);
                return 0;
            }
            if (!this.isHavePrive)
            {
                this.ShowMessage("您没有权限，如需获取权限请与系统管理员联系！", MessageBoxIcon.Information);
                return 0;
            }

            DialogResult dr = MessageBox.Show(this, "发药时将扣除【"+this.StockDept.Name+"】的库存，确认发药吗？", "提示>>", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel)
            {
                return 0;
            }

            int param = 0;
            ArrayList alSelectData = this.ucDrugDetail1.GetSelectData();
            

            if (alSelectData != null && alSelectData.Count > 0)
            {
                if (alSelectData[0] is FS.HISFC.Models.Pharmacy.DrugMessage)
                {
                    param = this.SaveDrugMessage(alSelectData);
                }
                else if (alSelectData[0] is FS.HISFC.Models.Pharmacy.ApplyOut)
                {
                    Hashtable hsSameBillApply = new Hashtable();
                    Hashtable hsDrugMessage = new Hashtable();
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alSelectData)
                    {
                        //已经发药、作废的数据不再发药
                        if (applyOut.State != "0" || applyOut.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
                        {
                            continue;
                        }
                        //退药的数据必须保证退到原发药科室
                        if (applyOut.BillClassNO == "R" && applyOut.StockDept.ID != this.StockDept.ID)
                        {
                            continue;
                        }
                        if (hsSameBillApply.Contains(applyOut.BillClassNO + applyOut.ApplyDept.ID + applyOut.StockDept.ID))
                        {
                            (hsSameBillApply[applyOut.BillClassNO + applyOut.ApplyDept.ID + applyOut.StockDept.ID] as ArrayList).Add(applyOut);
                        }
                        else
                        {
                            ArrayList alDrugBillApply = new ArrayList();
                            alDrugBillApply.Add(applyOut);
                            hsSameBillApply.Add(applyOut.BillClassNO + applyOut.ApplyDept.ID + applyOut.StockDept.ID, alDrugBillApply);

                            FS.HISFC.Models.Pharmacy.DrugMessage drugMessage = new FS.HISFC.Models.Pharmacy.DrugMessage();
                            drugMessage.ApplyDept.ID = applyOut.ApplyDept.ID;
                            drugMessage.DrugBillClass.ID = applyOut.BillClassNO;
                            FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass = drugStoreMgr.GetDrugBillClass(drugMessage.DrugBillClass.ID);
                            drugMessage.DrugBillClass.Name = drugBillClass.Name;
                            drugMessage.StockDept.ID = applyOut.StockDept.ID;
                            drugMessage.Memo = applyOut.DrugNO;
                            hsDrugMessage.Add(applyOut.BillClassNO + applyOut.ApplyDept.ID + applyOut.StockDept.ID, drugMessage);
                        }
                    }
                    foreach (string key in hsSameBillApply.Keys)
                    {
                        ArrayList alApplyOut = hsSameBillApply[key] as ArrayList;
                        FS.HISFC.Models.Pharmacy.DrugMessage curDrugMessage = hsDrugMessage[key] as FS.HISFC.Models.Pharmacy.DrugMessage;

                        param = this.SaveApplyOut(alApplyOut,curDrugMessage);
                        if (param <= 0)
                        {
                            break;
                        }
                    }
                }
                if (param > 0 && !this.IsPrintWhenDruged)
                {
                    this.ShowMessage("操作成功！");
                }
               
                //保存完后刷新数据
                this.Query();
            }

            ArrayList alSendBill = this.ucDrugDetail1.GetSelectDrugBill();
            foreach (FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass in alSendBill)
            {
                //检索科室摆药申请明细数据
                ArrayList al = this.drugStoreMgr.QueryApplyOutListByBill(drugBillClass.DrugBillNO, "0");
                if (al == null)
                {
                    this.ShowMessage("根据摆药通知信息获取摆药申请明细信息发生错误 " + this.drugStoreMgr.Err);
                    return -1;
                }
                if (al.Count > 0)
                {
                    FS.HISFC.Models.Pharmacy.DrugMessage curDrugMessage = new FS.HISFC.Models.Pharmacy.DrugMessage();
                    curDrugMessage.ApplyDept.ID = drugBillClass.ApplyDept.ID;
                    curDrugMessage.ApplyDept.Name = drugBillClass.ApplyDept.Name;
                    curDrugMessage.DrugBillClass = drugBillClass.Clone();
                    curDrugMessage.StockDept.ID = this.PriveDept.ID;
                    curDrugMessage.ID = drugBillClass.Oper.ID;
                    curDrugMessage.Name = drugBillClass.Oper.Name;
                    curDrugMessage.DrugBillClass.Memo = drugBillClass.DrugBillNO;
                    param = this.SaveApplyOut(al, curDrugMessage);
                }
            }

            return param;
        }

        /// <summary>
        /// 保存摆药通知
        /// </summary>
        /// <param name="alData">摆药通知实体数组</param>
        /// <returns></returns>
        private int SaveDrugMessage(ArrayList alData)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在保存...");
            Application.DoEvents();

            string info = "";

            foreach (FS.HISFC.Models.Pharmacy.DrugMessage message in alData)
            {
                #region 对选中的申请数据进行保存

                message.SendFlag = 1;                     //摆药通知中的数据全部被核准SendFlag=1，更新摆药通知信息。
                //message.SendType = drugMessage.SendType; //处理此摆药通知中的摆药申请数据时，取摆药台的发送类型。
                message.SendType = this.DrugControl.SendType;

                //检索科室摆药申请明细数据
                ArrayList al = this.drugStoreMgr.QueryApplyOutList(message);
                if (al == null)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    this.ShowMessage("根据摆药通知信息获取摆药申请明细信息发生错误 " + this.drugStoreMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }
                if (message.DrugBillClass.ID == "R")
                {
                    //第三个参数是药柜科室，传入null表示不处理药柜流程
                    if (Function.DrugReturnConfirm(al, message, null, this.StockDept) != 1)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        return -1;
                    }
                }
                else
                {
                    //第三个参数是药柜科室，传入null表示不处理药柜流程
                    if (Function.DrugConfirm(al, message, null, this.StockDept, ref info) != 1)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.ShowMessage("请与系统管理联系并报告错误：" + info);
                        return -1;
                    }
                }

                //打印数据
                if (this.IsPrintWhenDruged)
                {
                    this.Print(al, message, message.DrugBillClass.Memo, this.StockDept);
                }
                #endregion
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            return 1;
        }

        /// <summary>
        /// 保存摆药申请
        /// </summary>
        /// <returns></returns>
        private int SaveApplyOut(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugMessage curDrugMessage)
        {
            string info = "";
            if (curDrugMessage == null)
            {
                return 0;
            }
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在保存...");
            Application.DoEvents();

            if (curDrugMessage.DrugBillClass.ID == "R")
            {
                if (Function.DrugReturnConfirm(alData, curDrugMessage, null, this.StockDept) == -1)
                {
                    return -1;
                }
            }
            else
            {
                if (Function.DrugConfirm(alData, curDrugMessage, null, this.StockDept, ref info) == -1)
                {
                    this.ShowMessage("请与系统管理员联系并报告错误：" + info);
                    return -1;
                }
            }

            //打印数据
            if (this.IsPrintWhenDruged)
            {
                this.Print(alData, curDrugMessage, curDrugMessage.DrugBillClass.Memo, this.StockDept);
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            return 1;
        }
        #endregion

        #region 药品集中、紧急、按单发送
        private int SendDrug(string sendType)
        {           
            string applyDeptNO = "All";
            string info = "";
            int param = 0;
            
            if (this.cmbDept.Tag != null && !string.IsNullOrEmpty(this.cmbDept.Text))
            {
                applyDeptNO = this.cmbDept.Tag.ToString();
            }
            if (applyDeptNO == "All")
            {
                this.ShowMessage("请选择科室！");
                return 0;
            }
            if (sendType == "1")
            {
                #region 全区发送

                param = this.drugStoreMgr.GetDrugConcentratedSendInfo(applyDeptNO, ref info);
                if (param == 1)
                {
                    this.ShowMessage(info);
                    return 0;
                }
                else if (param == -1)
                {
                    this.ShowMessage(info);
                    return -1;
                }
                else
                {
                    DialogResult dr = MessageBox.Show(this, "全区发送一天只能操作一次，请您确认本科室或者本病区药品医嘱审核、分解是否完成？", "温馨提示>>", MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.Cancel)
                    {
                        return 0;
                    }
                    #region
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("请稍后...");

                    param = this.drugStoreMgr.DeleteDrugConcentratedSendInfo(applyDeptNO);
                    if (param == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.ShowMessage(this.drugStoreMgr.Err);
                        return -1;
                    }
                    param = this.drugStoreMgr.UpdateApplyOutSendType(applyDeptNO, "1", "2", ApplyOutValidDays);
                    if (param == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.ShowMessage(this.drugStoreMgr.Err);
                        return -1;
                    }
                    else if (param == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.ShowMessage("操作已经取消，目前该科室还没有发药申请，请您确认：\n1、选择的科室是否正确\n2、医嘱是否已经审核、分解\n3、药品是否已经被药房配发");
                        return -1;
                    }
                    param = this.drugStoreMgr.InsertDrugConcentratedSendInfo(applyDeptNO, ApplyOutValidDays);
                    if (param == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.ShowMessage(this.drugStoreMgr.Err);
                        return -1;
                    }
                    else if (param == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.ShowMessage("操作已经取消，形成药房集中摆药通知失败，请与系统管理员联系！");
                        return -1;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    #endregion
                }
                #endregion

            }
            else if (sendType == "4")
            {
                #region 紧急发送
                ArrayList alApplyOut = this.ucDrugDetail1.GetSelectData();

                if (alApplyOut == null || alApplyOut.Count == 0 || !(alApplyOut[0] is FS.HISFC.Models.Pharmacy.ApplyOut))
                {
                    this.ShowMessage("请您在【明细摆药】中选择数据，特别提醒您一次只能发送" + this.curMaxRowNumOnceSend.ToString() + "行数据");
                    return 0;
                }

                ArrayList al = new ArrayList();
                int druged = 0;
                int canceled = 0;
                foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alApplyOut)
                {
                    if (applyOut.Item.Quality.ID == "T")
                    {
                        canceled++;
                        continue;
                    }
                    if (applyOut.State != "0")
                    {
                        druged++;
                        continue;
                    }
                    if (applyOut.SendType == 1)
                    {
                        DialogResult dr = MessageBox.Show(this, applyOut.Item.Name + "[" + applyOut.Item.Specs + "]" + "已经全区发送，请您确认是否更改为紧急发送？", "温馨提示>>", MessageBoxButtons.OKCancel);
                        if (dr == DialogResult.Cancel)
                        {
                            canceled++;
                            continue;
                        }
                    }
                    al.Add(applyOut);
                }
                if (al.Count == 0)
                {
                    this.ShowMessage("操作已经取消：您选择的数据中" + druged.ToString() + "行已经发药，" + canceled.ToString() + "行已经全区发送！\n特别提醒您一次只能发送" + this.curMaxRowNumOnceSend.ToString() + "行数据");
                    return 0;
                }

                if (al.Count > this.curMaxRowNumOnceSend)
                {
                    this.ShowMessage("操作已经取消：特别提醒您一次只能发送" + this.curMaxRowNumOnceSend.ToString() + "行数据");
                    return 0;
                }

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("请稍后...");

                foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in al)
                {
                    param = this.drugStoreMgr.UpdateApplyOutSendType(applyOut.ID, "4", applyOut.SendType.ToString());
                    if (param == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.ShowMessage("操作失败：" + this.drugStoreMgr.Err);
                        return -1;
                    }
                    else if (param == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.ShowMessage("数据已经发生变化，请刷新或者重新查询");
                        return 0;
                    }
                    FS.HISFC.Models.Pharmacy.DrugMessage drugMessage = new FS.HISFC.Models.Pharmacy.DrugMessage();
                    drugMessage.ApplyDept.ID = applyOut.ApplyDept.ID;
                    drugMessage.ApplyDept.Name = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(applyOut.ApplyDept.ID);
                    drugMessage.DrugBillClass.ID = applyOut.BillClassNO;
                    drugMessage.ID = applyOut.BillClassNO;
                    drugMessage.DrugBillClass.Name = SOC.HISFC.BizProcess.Cache.Pharmacy.GetDrugBillClassName(applyOut.BillClassNO);
                    drugMessage.Name = drugMessage.DrugBillClass.Name;

                    drugMessage.SendType = 4;
                    drugMessage.SendFlag = 0;
                    drugMessage.StockDept.ID = applyOut.StockDept.ID;
                    if (this.drugStoreMgr.SetDrugMessage(drugMessage) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.ShowMessage("操作失败：" + this.drugStoreMgr.Err);
                        return -1;
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                #endregion

                param = al.Count;
            }
            else if (sendType == "A")
            {
                #region 按单发送
                ArrayList alSelectData = this.ucDrugDetail1.GetSelectData();
                if (alSelectData != null && alSelectData.Count > 0)
                {
                    if (alSelectData[0] is FS.HISFC.Models.Pharmacy.DrugMessage)
                    {
                        FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在发送...");
                        FS.FrameWork.Management.PublicTrans.BeginTransaction();

                        for (int index = alSelectData.Count - 1; index > -1; index--)
                        {
                            FS.HISFC.Models.Pharmacy.DrugMessage drugMessage = alSelectData[index] as FS.HISFC.Models.Pharmacy.DrugMessage;
                            if (Function.Contains(this.SplitPatientBillClassNO, drugMessage.DrugBillClass.ID))
                            {
                                ArrayList al = this.drugStoreMgr.QueryApplyOutList(drugMessage, this.ApplyOutValidDays);
                                if (al == null)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                    this.ShowMessage("根据摆药通知信息获取摆药申请明细信息发生错误 " + this.drugStoreMgr.Err, MessageBoxIcon.Error);
                                    return -1;
                                }
                            
                                param = this.SendApplyOutAsBill(al, ref info);
                                if (param < 1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                    this.ShowMessage(info);
                                    return -1;
                                }
                                alSelectData.RemoveAt(index);
                            }
                        }
                        if (alSelectData.Count > 0)
                        {
                            param = this.SendDrugMessageAsBill(alSelectData, ref info);
                            if (param < 1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                this.ShowMessage(info);
                                return -1;
                            }
                        }
                        FS.FrameWork.Management.PublicTrans.Commit();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                    }
                    else if (alSelectData[0] is FS.HISFC.Models.Pharmacy.ApplyOut)
                    {
                        FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在发送...");
                        FS.FrameWork.Management.PublicTrans.BeginTransaction();

                        param = this.SendApplyOutAsBill(alSelectData, ref info);
                        if (param < 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            this.ShowMessage(info);
                            return -1;
                        }

                        FS.FrameWork.Management.PublicTrans.Commit();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                    }
                }
                #endregion
            }
            if (param > 0)
            {
                this.ShowMessage("操作成功！");
                this.Query();
            }

            return 1;
        }

        private int SendDrugMessageAsBill(ArrayList alDrugMessage, ref string info)
        {
            foreach (FS.HISFC.Models.Pharmacy.DrugMessage drugMessage in alDrugMessage)
            {
                //更新单号
                string billNO = this.drugStoreMgr.GetNewDrugBillNO();
                if (billNO == "" || billNO == "-1")
                {
                    info = "获取单号失败，请与系统管理员联系并报告错误：" + this.drugStoreMgr.Err;
                    return -1;
                }
                int param = this.drugStoreMgr.UpdateApplyOutDrugBillNO(drugMessage.ApplyDept.ID, drugMessage.StockDept.ID, drugMessage.DrugBillClass.ID, this.ApplyOutValidDays, DateTime.Now, billNO);
                if (param == -1)
                {
                    info = "发送失败，请与系统管理员联系并报告错误：" + this.drugStoreMgr.Err;
                    return -1;
                }
                if (param == 0)
                {
                    info = "数据已经发生变化，请重新查询";
                    return -1;
                }
            }
            return 1;

        }

        private int SendApplyOutAsBill(ArrayList alApplyOut, ref string info)
        {
            Hashtable hsSameBillClass = new Hashtable();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyout in alApplyOut)
            {
                if (applyout.State == "0" && applyout.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid && (applyout.DrugNO == "" || applyout.DrugNO == "0"))
                {
                    string key = applyout.BillClassNO + applyout.ApplyDept.ID + applyout.StockDept.ID;
                    if (Function.Contains(this.SplitPatientBillClassNO, applyout.BillClassNO))
                    {
                        key = applyout.BillClassNO + applyout.ApplyDept.ID + applyout.StockDept.ID + applyout.PatientNO;
                    }
                    if (hsSameBillClass.Contains(key))
                    {
                        ((ArrayList)hsSameBillClass[key]).Add(applyout);
                    }
                    else
                    {
                        ArrayList altmp = new ArrayList();
                        altmp.Add(applyout);
                        hsSameBillClass.Add(key, altmp);
                    }
                }
            }
            if (hsSameBillClass.Count == 0)
            {
                info = "请选择有效的数据：红色作废、已经发药、已经发送到药房的数据不能再次发送";
                return -1;
            }


            foreach (ArrayList alValidApply in hsSameBillClass.Values)
            {
                //更新单号
                string billNO = this.drugStoreMgr.GetNewDrugBillNO(((FS.HISFC.Models.Pharmacy.ApplyOut)alValidApply[0]).StockDept.ID);
                if (billNO == "" || billNO == "-1")
                {
                    info = "获取单号失败，请与系统管理员联系并报告错误：" + this.drugStoreMgr.Err;
                    return -1;
                }

                DateTime systime = this.drugStoreMgr.GetDateTimeFromSysDateTime();

                foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyout in alValidApply)
                {
                    int param = this.drugStoreMgr.UpdateApplyOutDrugBillNO(applyout.ID, systime, billNO);
                    if (param == -1)
                    {
                        info = "发送失败，请与系统管理员联系并报告错误：" + this.drugStoreMgr.Err;
                        return -1;
                    }
                    if (param == 0)
                    {
                        info = "数据已经发生变化，请重新查询";
                        return -1;
                    }
                }
            }

            return 1; 
 
        }

        private int SetConcentratedSendInfo()
        {
            if (this.InpatintDrugApplyType != Function.EnumInpatintDrugApplyType.全区发送)
            {
                return 0;
            }
            string info = "";
            try
            {
                int param = this.drugStoreMgr.GetDrugConcentratedSendInfo(this.cmbDept.Tag.ToString(), ref info);
                this.nlbConcentratedSendInfo.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(this.cmbDept.Tag.ToString()) + " " + info;
            }
            catch { }

            return 1;
        }
        #endregion

        #region 事件
        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            this.tvMessageBaseTree.AfterSelect += new TreeViewEventHandler(tvMessageBaseTree_AfterSelect);
            this.ncbDruged.CheckedChanged += new EventHandler(ncbDruged_CheckedChanged);
            this.nlbStockDept.DoubleClick += new EventHandler(nlbStockDept_DoubleClick);
            this.cmbDept.SelectedIndexChanged += new EventHandler(cmbDept_SelectedIndexChanged);
            this.rbDrugBillList.CheckedChanged += new EventHandler(rbDrugBillList_CheckedChanged);
            base.OnLoad(e);
        }

        void rbDrugBillList_CheckedChanged(object sender, EventArgs e)
        {
            SOC.Public.XML.SettingFile.SaveSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugApplySetting.xml", "ShowSetting", "ListType", this.rbDrugBillList.Checked.ToString());
        }

        void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetConcentratedSendInfo();
        }

        void nlbStockDept_DoubleClick(object sender, EventArgs e)
        {
            this.SetPriveDept();
        }

        void ncbDruged_CheckedChanged(object sender, EventArgs e)
        {
            this.ncbUnapplyIn.Enabled = this.ncbDruged.Checked;
            this.ncbApplyIned.Enabled = this.ncbDruged.Checked;
        }

        public override int Query(object sender, object neuObject)
        {
            this.Query();
            return base.Query(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            this.ucDrugDetail1.Export();
            return base.Export(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.ucDrugDetail1.Print();
            return base.OnPrint(sender, neuObject);
        }

        void tvMessageBaseTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                if (e.Node.Tag == null)
                {
                    return;
                }
                if (e.Node.Tag is FS.HISFC.Models.Pharmacy.DrugMessage)
                {                    
                    FS.HISFC.Models.Pharmacy.DrugMessage drugMessage = e.Node.Tag as FS.HISFC.Models.Pharmacy.DrugMessage;
                    //这个是患者列表节点
                    if (drugMessage.User01 == "All")
                    {
                        this.ShowApplyOutDetail(drugMessage);
                    }
                    else
                    {
                        this.ShowDrugMessage(this.tvMessageBaseTree.GetDrugMessageList(e.Node));
                        this.ShowDrugBill(this.tvMessageBaseTree.GetDrugBill(e.Node),true);
                    }
                }
                else if (e.Node.Tag is FS.HISFC.Models.Pharmacy.DrugBillClass)
                {
                    this.ShowDrugBill(this.tvMessageBaseTree.GetDrugBill(e.Node), true);
                }
            }
            else
            {
                if (e.Node.Tag == null)
                {
                    return;
                }
                if (e.Node.Tag is FS.HISFC.Models.Pharmacy.DrugMessage)
                {
                    FS.HISFC.Models.Pharmacy.DrugMessage drugMessage = e.Node.Tag as FS.HISFC.Models.Pharmacy.DrugMessage;
                    this.ShowApplyOutDetail(drugMessage);
                }
                else if (e.Node.Tag is FS.HISFC.Models.Pharmacy.DrugBillClass)
                {
                    FS.HISFC.Models.Pharmacy.DrugBillClass drugBill = e.Node.Tag as FS.HISFC.Models.Pharmacy.DrugBillClass;
                    this.ShowApplyOutDetail(drugBill);
                }
            }
        }
        #endregion

        #region 工具栏

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            ToolBarService.AddToolButton("全选", "选择所有数据", FS.FrameWork.WinForms.Classes.EnumImageList.Q全选, true, false, null);
            ToolBarService.AddToolButton("全不选", "取消所有数据选择", FS.FrameWork.WinForms.Classes.EnumImageList.Q全不选, true, false, null);
            ToolBarService.AddToolButton("用药时间", "按照用药时间选择数据", FS.FrameWork.WinForms.Classes.EnumImageList.R日期, true, false, null);
            ToolBarService.AddToolButton("发药", "根据选择数据对扣库科室削减库存", FS.FrameWork.WinForms.Classes.EnumImageList.B摆药单, true, false, null);
            ToolBarService.AddToolButton("全区发送", "通知药房集中摆药", FS.FrameWork.WinForms.Classes.EnumImageList.Y药品, true, false, null);
            ToolBarService.AddToolButton("紧急发送", "通知药房紧急摆药", FS.FrameWork.WinForms.Classes.EnumImageList.M明细, true, false, null);
            ToolBarService.AddToolButton("按单发送", "以单据形式整体发送", FS.FrameWork.WinForms.Classes.EnumImageList.D打包, true, false, null);
            
            base.OnInit(sender, neuObject, param);
            return this.ToolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "全选")
            {
                this.SelectAllDetailData(true);
            }
            else if (e.ClickedItem.Text == "全不选")
            {
                this.SelectAllDetailData(false);
            }
            else if (e.ClickedItem.Text == "用药时间")
            {
                this.SelectDetailDataWithTime();
            }
            else if (e.ClickedItem.Text == "发药")
            {
                this.DrugSave();
            }
            else if (e.ClickedItem.Text == "全区发送")
            {
                if (this.InpatintDrugApplyType != Function.EnumInpatintDrugApplyType.全区发送)
                {
                    this.ShowMessage("程序没有设置全区发送功能，请与系统管理员联系！");
                }
                else
                {
                    this.SendDrug("1");
                }
            }
            else if (e.ClickedItem.Text == "紧急发送")
            {
                if (this.InpatintDrugApplyType != Function.EnumInpatintDrugApplyType.全区发送)
                {
                    this.ShowMessage("程序没有设置紧急发送功能，请与系统管理员联系！");
                }
                else
                {
                    this.SendDrug("4");
                }
            }
            else if (e.ClickedItem.Text == "按单发送")
            {
                if (this.InpatintDrugApplyType != Function.EnumInpatintDrugApplyType.全区发送)
                {
                    this.ShowMessage("程序没有设置按单发送功能，请与系统管理员联系！");
                }
                else
                {
                    this.SendDrug("A");
                }
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

        #region IPreArrange 成员

        public int PreArrange()
        {
            return this.SetPriveDept();
        }


        /// <summary>
        /// 设置权限科室
        /// </summary>
        /// <returns></returns>
        private int SetPriveDept()
        {
            #region 权限科室
            if (this.IsCheckPrivePower)
            {
                if (string.IsNullOrEmpty(PrivePowerString))
                {
                    PrivePowerString = "0320+Z1";
                }
                if (PrivePowerString.Split('+').Length < 2)
                {
                    PrivePowerString = PrivePowerString + "+Z1";
                }
                string[] prives = PrivePowerString.Split('+');
                int param = Function.ChoosePriveDept(prives[0], prives[1], ref this.priveDept);
                if (param == 0 || param == -1 || this.priveDept == null || string.IsNullOrEmpty(this.priveDept.ID))
                {
                    this.isHavePrive = false;
                }
                if (this.isHavePrive)
                {
                    this.StockDept = this.PriveDept.Clone();
                }
            }
            if(this.PriveDept==null || string.IsNullOrEmpty(this.PriveDept.ID))
            {
                //登录科室中没有科室类型
                FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
                FS.HISFC.Models.Base.Department dept = deptMgr.GetDeptmentById(((FS.HISFC.Models.Base.Employee)this.drugStoreMgr.Operator).Dept.ID);
                if (dept == null)
                {
                    MessageBox.Show("获取科室信息发生错误，请与系统管理员联系并报告错误：" + deptMgr.Err, "提示>>", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
                this.PriveDept = dept as FS.FrameWork.Models.NeuObject;
                this.priveDept.Memo = dept.DeptType.ID.ToString();


                this.StockDept = PriveDept.Clone();
            }
            if (this.IsAllowDruged)
            {
                this.ShowStatusBarTip("扣库科室：" + this.StockDept.Name);
                this.nlbStockDept.Text = "【扣库科室：" + this.StockDept.Name + "】";
            }
            #endregion

            return 1;
        }
       
        #endregion
    }
}
