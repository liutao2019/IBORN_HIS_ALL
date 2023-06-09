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
    public partial class ucOrderExeQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucOrderExeQuery()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                this.neuSpread1.ActiveSheetChanged += new EventHandler(neuSpread1_ActiveSheetChanged);
                neuSpread2.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(neuSpread2_CellDoubleClick);
            }
        }

        /// <summary>
        /// 业务层
        /// </summary>
        private FS.HISFC.BizLogic.Order.ChargeBill myCharegeBill = new FS.HISFC.BizLogic.Order.ChargeBill();

        /// <summary>
        /// 医嘱执行档内容
        /// </summary>
        private FS.HISFC.Models.Order.ExecOrder myExeOrder = null;

        private ArrayList alExeOrder = null;

        //传递患者信息类
        private FS.HISFC.Models.RADT.PatientInfo patient;
        /// <summary>
        /// 页面属性，接收传过来的患者信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            get
            {
                return this.patient;
            }
            set
            {
                this.patient = value;
                if (patient != null)
                {
                    this.ShowData(this.patient.ID);
                }
            }

        }

        /// <summary>
        /// 是否自动保存 格式文件
        /// </summary>
        private bool isAutoSaveXML = true;

        /// <summary>
        /// 是否自动保存 格式文件
        /// </summary>
        [Description("是否自动保存 格式文件"), Category("设置")]
        public bool IsAutoSaveXML
        {
            get
            {
                return isAutoSaveXML;
            }
            set
            {
                isAutoSaveXML = value;
            }
        }

        DataSet myDataSetDrug = new DataSet();
        DataSet myDataSetUndrug = new DataSet();

        DataView myDataViewDrug = new DataView();//药品过滤
        DataView myDataViewUndrug = new DataView();//非药品过滤

        string filterInput = "1=1";	//输入码过滤条件
        string filterExec  = "1=1";
        string filterValid = "1=1";	//是否有效过滤条件
        string filterType = "1=1";//医嘱类型

        string drugQuery   = FS.FrameWork.WinForms.Classes.Function.SettingPath + @"\ucOrderExeQuery_Drug.xml";
        string undrugQuery = FS.FrameWork.WinForms.Classes.Function.SettingPath + @"\ucOrderExeQuery_UnDrug.xml";

        /// <summary>
        /// 传递病人实体
        /// </summary>
        /// <param name="neuObject"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (this.tv.CheckBoxes == true)
            {
                this.tv.CheckBoxes = false;
            }
            this.tv.ExpandAll();
            this.Patient = neuObject as FS.HISFC.Models.RADT.PatientInfo;
            return base.OnSetValue(neuObject, e);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.patient != null)
            {
                this.ShowData(this.patient.ID);
            }

            return base.OnQuery(sender, neuObject);
        }

        /// <summary>
        /// 设置药品执行档显示格式
        /// </summary>
        protected void SetFormatForDrug()
        {
            if (System.IO.File.Exists(this.drugQuery))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuSpread1_Sheet1, this.drugQuery);
                //this.RefreshDrugBackColor();
            }
            else
            {
                #region 缺省的
                this.neuSpread1_Sheet1.Columns.Get(0).Label = "标记";
                this.neuSpread1_Sheet1.Columns.Get(0).Width = 56F;
                FarPoint.Win.Spread.CellType.CheckBoxCellType cellcbkBJ = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                this.neuSpread1_Sheet1.Columns[0].CellType = cellcbkBJ;
                this.neuSpread1_Sheet1.Columns[0].Locked = false;
                this.neuSpread1_Sheet1.Columns.Get(1).Label = "开立医生";
                this.neuSpread1_Sheet1.Columns.Get(1).Width = 56F;
                this.neuSpread1_Sheet1.Columns.Get(2).Label = "医嘱类型";
                this.neuSpread1_Sheet1.Columns.Get(2).Width = 56F;
                this.neuSpread1_Sheet1.Columns.Get(3).Label = "有效";
                this.neuSpread1_Sheet1.Columns.Get(3).Width = 35F;
                this.neuSpread1_Sheet1.Columns.Get(4).Label = "发送状态";
                this.neuSpread1_Sheet1.Columns.Get(4).Width = 59F;
                this.neuSpread1_Sheet1.Columns.Get(5).Label = "药品名称";
                this.neuSpread1_Sheet1.Columns.Get(5).Width = 117F;
                this.neuSpread1_Sheet1.Columns.Get(6).Label = "规格";
                this.neuSpread1_Sheet1.Columns.Get(6).Width = 71F;
                this.neuSpread1_Sheet1.Columns.Get(7).Label = "用量";
                this.neuSpread1_Sheet1.Columns.Get(7).Width = 60F;
                this.neuSpread1_Sheet1.Columns.Get(8).Label = "单位";
                this.neuSpread1_Sheet1.Columns.Get(8).Width = 35F;
                this.neuSpread1_Sheet1.Columns.Get(9).Label = "应执行时间";
                this.neuSpread1_Sheet1.Columns.Get(9).Width = 110F;
                this.neuSpread1_Sheet1.Columns.Get(10).Label = "分解时间";
                this.neuSpread1_Sheet1.Columns.Get(10).Width = 60F;
                this.neuSpread1_Sheet1.Columns.Get(11).Label = "记账标记";
                this.neuSpread1_Sheet1.Columns.Get(11).Width = 56F;
                this.neuSpread1_Sheet1.Columns.Get(12).Label = "记账时间";
                this.neuSpread1_Sheet1.Columns.Get(12).Width = 110F;
                this.neuSpread1_Sheet1.Columns.Get(13).Label = "发药时间";
                this.neuSpread1_Sheet1.Columns.Get(13).Width = 110F;
                this.neuSpread1_Sheet1.Columns.Get(14).Label = "医嘱时间";
                this.neuSpread1_Sheet1.Columns.Get(14).Width = 110F;
                this.neuSpread1_Sheet1.Columns.Get(15).Label = "停止时间";
                this.neuSpread1_Sheet1.Columns.Get(15).Width = 110F;
                this.neuSpread1_Sheet1.Columns.Get(16).Label = "频次";
                this.neuSpread1_Sheet1.Columns.Get(16).Width = 47F;
                this.neuSpread1_Sheet1.Columns.Get(17).Label = "每次剂量";
                this.neuSpread1_Sheet1.Columns.Get(17).Width = 56F;
                this.neuSpread1_Sheet1.Columns.Get(18).Label = "单位";
                this.neuSpread1_Sheet1.Columns.Get(18).Width = 35F;
                this.neuSpread1_Sheet1.Columns.Get(19).Label = "包装数";
                this.neuSpread1_Sheet1.Columns.Get(19).Width = 53F;
                this.neuSpread1_Sheet1.Columns.Get(20).Label = "付数";
                this.neuSpread1_Sheet1.Columns.Get(20).Width = 45F;
                this.neuSpread1_Sheet1.Columns.Get(21).Label = "用法";
                this.neuSpread1_Sheet1.Columns.Get(21).Width = 54F;
                this.neuSpread1_Sheet1.Columns.Get(22).Label = "取药药房";
                this.neuSpread1_Sheet1.Columns.Get(22).Width = 111F;
                this.neuSpread1_Sheet1.Columns.Get(23).Label = "医嘱说明";
                this.neuSpread1_Sheet1.Columns.Get(23).Width = 74F;
                this.neuSpread1_Sheet1.Columns.Get(24).Label = "备注";
                this.neuSpread1_Sheet1.Columns.Get(24).Width = 51F;
                this.neuSpread1_Sheet1.Columns.Get(25).Label = "医嘱号";
                this.neuSpread1_Sheet1.Columns.Get(25).Width = 70F;
                this.neuSpread1_Sheet1.Columns.Get(26).Label = "组合号";
                this.neuSpread1_Sheet1.Columns.Get(26).Width = 67F;
                this.neuSpread1_Sheet1.Columns.Get(27).Label = "执行号";
                this.neuSpread1_Sheet1.Columns.Get(27).Width = 69F;
                this.neuSpread1_Sheet1.Columns.Get(28).Label = "附材";
                this.neuSpread1_Sheet1.Columns.Get(28).Width = 38F;
                this.neuSpread1_Sheet1.Columns.Get(29).Label = "执行科室";
                this.neuSpread1_Sheet1.Columns.Get(29).Width = 127F;
                this.neuSpread1_Sheet1.Columns.Get(30).Label = "记账人";
                this.neuSpread1_Sheet1.Columns.Get(30).Width = 45F;
                this.neuSpread1_Sheet1.Columns.Get(31).Label = "发药科室";
                this.neuSpread1_Sheet1.Columns.Get(31).Width = 104F;
                this.neuSpread1_Sheet1.Columns.Get(32).Label = "发药人";
                this.neuSpread1_Sheet1.Columns.Get(32).Width = 45F;
                this.neuSpread1_Sheet1.Columns.Get(33).Label = "停止人";
                this.neuSpread1_Sheet1.Columns.Get(33).Width = 45F;
                this.neuSpread1_Sheet1.Columns.Get(34).Label = "处方号";
                this.neuSpread1_Sheet1.Columns.Get(34).Width = 65F;
                this.neuSpread1_Sheet1.Columns.Get(35).Label = "处方内流水号";
                //this.neuSpread1_Sheet1.Columns.Get(35).Visible = false;
                this.neuSpread1_Sheet1.Columns.Get(36).Label = "发送单打印标记";
                //this.neuSpread1_Sheet1.Columns.Get(36).Visible = false;
                this.neuSpread1_Sheet1.Columns.Get(37).Label = "打印时间";
                //this.neuSpread1_Sheet1.Columns.Get(37).Visible = false;
                this.neuSpread1_Sheet1.Columns.Get(38).Label = "剂型代码";
                //this.neuSpread1_Sheet1.Columns.Get(38).Visible = false;
                this.neuSpread1_Sheet1.Columns.Get(39).Label = "药品编码";
                //this.neuSpread1_Sheet1.Columns.Get(39).Visible = false;
                this.neuSpread1_Sheet1.Columns.Get(40).Label = "住院科室";
                //this.neuSpread1_Sheet1.Columns.Get(40).Visible = false;
                this.neuSpread1_Sheet1.Columns.Get(41).Label = "护理站";
                //this.neuSpread1_Sheet1.Columns.Get(41).Visible = false;
                this.neuSpread1_Sheet1.Columns.Get(42).Label = "开立科室";
                //this.neuSpread1_Sheet1.Columns.Get(42).Visible = false;
                this.neuSpread1_Sheet1.Columns.Get(43).Label = "拼音码";
                //this.neuSpread1_Sheet1.Columns.Get(43).Visible = false;
                this.neuSpread1_Sheet1.Columns.Get(44).Label = "五笔码";
                //this.neuSpread1_Sheet1.Columns.Get(44).Visible = false;

                //this.RefreshDrugBackColor();

                this.neuSpread1_Sheet1.DefaultStyle.Locked = true;
                this.neuSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
                this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
                this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
                this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
                this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 30F;
                //this.neuSpread1_Sheet1.SetColumnAllowAutoSort(-1, true);
                #endregion
            }

            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ExtendedSelect;
        }

        /// <summary>
        /// 设置非药品执行档显示格式
        /// </summary>
        protected void SetFormatForUnDrug()
        {
            if (System.IO.File.Exists(this.undrugQuery))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuSpread2_Sheet1, this.undrugQuery);
            }
            else
            {
                #region 缺省设置

                this.neuSpread2_Sheet1.Columns.Get(0).Label = "开立医生";
                this.neuSpread2_Sheet1.Columns.Get(0).Width = 56F;
                this.neuSpread2_Sheet1.Columns.Get(1).Label = "医嘱类型";
                this.neuSpread2_Sheet1.Columns.Get(1).Width = 56F;
                this.neuSpread2_Sheet1.Columns.Get(2).Label = "有效";
                this.neuSpread2_Sheet1.Columns.Get(2).Width = 35F;

                this.neuSpread2_Sheet1.Columns.Get(3).Label = "项目名称";
                this.neuSpread2_Sheet1.Columns.Get(3).Width = 117F;
                this.neuSpread2_Sheet1.Columns.Get(4).Label = "用量";
                this.neuSpread2_Sheet1.Columns.Get(4).Width = 60F;
                this.neuSpread2_Sheet1.Columns.Get(5).Label = "单位";
                this.neuSpread2_Sheet1.Columns.Get(5).Width = 35F;
                this.neuSpread2_Sheet1.Columns.Get(6).Label = "应执行时间";
                this.neuSpread2_Sheet1.Columns.Get(6).Width = 110F;
                this.neuSpread2_Sheet1.Columns.Get(7).Label = "分解时间";
                this.neuSpread2_Sheet1.Columns.Get(7).Width = 60F;
                this.neuSpread2_Sheet1.Columns.Get(8).Label = "记账标记";
                this.neuSpread2_Sheet1.Columns.Get(8).Width = 56F;
                this.neuSpread2_Sheet1.Columns.Get(9).Label = "记账时间";
                this.neuSpread2_Sheet1.Columns.Get(9).Width = 110F;
                this.neuSpread2_Sheet1.Columns.Get(10).Label = "医嘱时间";
                this.neuSpread2_Sheet1.Columns.Get(10).Width = 110F;
                this.neuSpread2_Sheet1.Columns.Get(11).Label = "停止时间";
                this.neuSpread2_Sheet1.Columns.Get(11).Width = 110F;
                this.neuSpread2_Sheet1.Columns.Get(12).Label = "频次";
                this.neuSpread2_Sheet1.Columns.Get(12).Width = 50F;
                this.neuSpread2_Sheet1.Columns.Get(13).Label = "医嘱说明";
                this.neuSpread2_Sheet1.Columns.Get(13).Width = 51F;

                this.neuSpread2_Sheet1.Columns.Get(14).Label = "备注";
                this.neuSpread2_Sheet1.Columns.Get(14).Width = 51F;
                this.neuSpread2_Sheet1.Columns.Get(15).Label = "医嘱号";
                this.neuSpread2_Sheet1.Columns.Get(15).Width = 70F;

                this.neuSpread2_Sheet1.Columns.Get(16).Label = "组合号";
                this.neuSpread2_Sheet1.Columns.Get(16).Width = 67F;
                this.neuSpread2_Sheet1.Columns.Get(17).Label = "执行号";
                this.neuSpread2_Sheet1.Columns.Get(17).Width = 69F;
                this.neuSpread2_Sheet1.Columns.Get(18).Label = "附材";
                this.neuSpread2_Sheet1.Columns.Get(18).Width = 38F;
                this.neuSpread2_Sheet1.Columns.Get(19).Label = "执行科室";
                this.neuSpread2_Sheet1.Columns.Get(19).Width = 127F;
                this.neuSpread2_Sheet1.Columns.Get(20).Label = "记账人";
                this.neuSpread2_Sheet1.Columns.Get(20).Width = 45F;
                this.neuSpread2_Sheet1.Columns.Get(21).Label = "停止人";
                this.neuSpread2_Sheet1.Columns.Get(21).Width = 45F;
                this.neuSpread2_Sheet1.Columns.Get(22).Label = "处方号";
                this.neuSpread2_Sheet1.Columns.Get(22).Width = 65F;
                this.neuSpread2_Sheet1.Columns.Get(23).Label = "处方内流水号";
                this.neuSpread2_Sheet1.Columns.Get(23).Visible = false;
                this.neuSpread2_Sheet1.Columns.Get(24).Label = "发送单打印标记";
                this.neuSpread2_Sheet1.Columns.Get(24).Visible = false;
                this.neuSpread2_Sheet1.Columns.Get(25).Label = "打印时间";
                this.neuSpread2_Sheet1.Columns.Get(25).Visible = false;
                this.neuSpread2_Sheet1.Columns.Get(26).Label = "非药品编码";
                this.neuSpread2_Sheet1.Columns.Get(26).Visible = false;
                this.neuSpread2_Sheet1.Columns.Get(27).Label = "住院科室";
                this.neuSpread2_Sheet1.Columns.Get(27).Visible = false;
                this.neuSpread2_Sheet1.Columns.Get(28).Label = "护理站";
                this.neuSpread2_Sheet1.Columns.Get(28).Visible = false;
                this.neuSpread2_Sheet1.Columns.Get(29).Label = "开立科室";
                this.neuSpread2_Sheet1.Columns.Get(29).Visible = false;
                this.neuSpread2_Sheet1.Columns.Get(30).Label = "拼音码";
                this.neuSpread2_Sheet1.Columns.Get(30).Visible = false;
                this.neuSpread2_Sheet1.Columns.Get(31).Label = "五笔码";
                this.neuSpread2_Sheet1.Columns.Get(31).Visible = false;

                RefreshUndrugFlag();

                this.neuSpread2_Sheet1.DefaultStyle.Locked = true;
                this.neuSpread2_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
                this.neuSpread2_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
                this.neuSpread2_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
                this.neuSpread2_Sheet1.RowHeader.Columns.Default.Resizable = false;
                this.neuSpread2_Sheet1.RowHeader.Columns.Get(0).Width = 30F;
                //this.neuSpread1_Sheet1.SetColumnAllowAutoSort(-1, true);
                #endregion
            }
            this.neuSpread2_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ExtendedSelect;
        }

        protected void RefreshUndrugFlag()
        {
            //this.neuSpread2_Sheet1.Columns.Add(0, 1);//添加新列
            //this.neuSpread2_Sheet1.Columns.Get(0).Label = "标记";
            //this.neuSpread2_Sheet1.Columns.Get(0).Width = 60F;
            //for (int i = 0; i < this.neuSpread2.Sheets[0].RowCount; i++)
            //{
                //int iFee = int.Parse(this.neuSpread2.Sheets[0].Cells[i, 19].Text);
                //if (iFee == 1)
                //{
                //    this.neuSpread2.Sheets[0].Cells[i, 0].Text = "已收费";
                //}
                //else
                //{
                //    if (this.neuSpread2.Sheets[0].Cells[i, 24].Text == "1")
                //    {
                //        this.neuSpread2.Sheets[0].Cells[i, 0].Text = "已收费";
                //    }
                //    else
                //    {
                //        this.neuSpread2.Sheets[0].Cells[i, 0].Text = "出单未收费";
                //    }
                //}
            //}
        }

        protected void RefreshDrugBackColor()
        {
            for (int i = 0; i < this.neuSpread1.Sheets[0].RowCount; i++)
            {
                string strValid = this.neuSpread1_Sheet1.Cells[i, 3].Text;
                this.neuSpread1_Sheet1.Rows[i].BackColor = Color.White;
                if (strValid == "无效")
                {
                    this.neuSpread1_Sheet1.Rows[i].BackColor = Color.FromArgb(255, 128, 128);
                }
            }
            for (int i = 0; i < this.neuSpread2.Sheets[0].RowCount; i++)
            {
                string strValid = this.neuSpread2.Sheets[0].Cells[i, 2].Text;
                this.neuSpread2_Sheet1.Rows[i].BackColor = Color.White;
                if (strValid == "无效")
                {
                    this.neuSpread2_Sheet1.Rows[i].BackColor = Color.FromArgb(255, 128, 128);
                }
            }
        }

        /// <summary>
        /// 显示数据
        /// </summary>
        public void ShowData(string inPatientNo)
        {
            //this.Patient = this.patientManager.PatientQuery(inPatientNo);
            if (this.Patient == null || this.Patient.ID == "")
            {
                //清空数据
                this.ClearData();
                return;
            }

            //显示药品执行档信息
            if (this.Patient.ID == "") return;

            #region 取药品执行档数据
            this.myDataSetDrug = CacheManager.InOrderMgr.QueryExecDrugOrderByInpatientNo(this.Patient.ID);
            if (this.myDataSetDrug == null)
            {
                MessageBox.Show(CacheManager.InOrderMgr.Err);
                return;
            }

            //将数据中的编码转换为名称
            for (int i = 0; i < this.myDataSetDrug.Tables[0].Rows.Count; i++)
            {
                this.myDataSetDrug.Tables[0].Rows[i]["取药药房"] = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(this.myDataSetDrug.Tables[0].Rows[i]["取药药房"].ToString());
                this.myDataSetDrug.Tables[0].Rows[i]["执行科室"] = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(this.myDataSetDrug.Tables[0].Rows[i]["执行科室"].ToString());
                this.myDataSetDrug.Tables[0].Rows[i]["发药科室"] = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(this.myDataSetDrug.Tables[0].Rows[i]["发药科室"].ToString());

                //this.myDataSetDrug.Tables[0].Rows[i]["医嘱类型"] = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(this.myDataSetDrug.Tables[0].Rows[i]["医嘱类型"].ToString());
            }
            //将取得的数据显示到控件中
            this.myDataViewDrug = new DataView(this.myDataSetDrug.Tables[0]);
            this.neuSpread1_Sheet1.DataSource = this.myDataViewDrug;
            //设置显示格式
            this.SetFormatForDrug();
            #endregion

            #region 取非药品执行档数据
            this.myDataSetUndrug = CacheManager.InOrderMgr.QueryExecUndrugOrderByInpatientNo(this.Patient.ID);
            if (this.myDataSetUndrug == null)
            {
                MessageBox.Show(CacheManager.InOrderMgr.Err);
                return;
            }
            //将数据中的编码转换为名称
            for (int i = 0; i < this.myDataSetUndrug.Tables[0].Rows.Count; i++)
            {
                this.myDataSetUndrug.Tables[0].Rows[i]["执行科室"] = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(this.myDataSetUndrug.Tables[0].Rows[i]["执行科室"].ToString());

                //this.myDataSetUndrug.Tables[0].Rows[i]["医嘱类型"] = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(this.myDataSetUndrug.Tables[0].Rows[i]["医嘱类型"].ToString());
            }
            //将取得的DataSet付给显示控件
            this.myDataViewUndrug = new DataView(this.myDataSetUndrug.Tables[0]);
            this.neuSpread2_Sheet1.DataSource = this.myDataViewUndrug;
            //设置显示格式
            this.SetFormatForUnDrug();
            #endregion

            RefreshDrugBackColor();
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        public void ClearData()
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;
            this.neuSpread2_Sheet1.Rows.Count = 0;
        }



        /// <summary>
        /// 更新有效标记
        /// </summary>
        /// <param name="validFlag"></param>
        private int SetValidFlag(bool validFlag)
        {
            string strValid = (validFlag ? "有效" : "无效");
            string strToDo = (validFlag ? "恢复" : "作废");

            if (MessageBox.Show("确定要设置选中记录为" + strValid + "么？", "询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Cancel)
            {
                return 1;
            }

            FS.HISFC.Models.Order.ExecOrder exeOrder = null;

            string feeItems = "";

            //是否更新了数据，更新到了就提示
            bool isDoSomeThing = false;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            if (this.neuTabControl1.SelectedTab == tabPage1)
            {
                for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
                {
                    if (neuSpread1_Sheet1.IsSelected(i, 0))
                    {
                        string execOrderID = this.neuSpread1_Sheet1.Cells[i, 27].Text.Trim();

                        exeOrder = CacheManager.InOrderMgr.QueryExecOrderByExecOrderID(execOrderID, "1");
                        if (exeOrder == null)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("查找执行档记录出错！\r\n\r\n" + CacheManager.InOrderMgr.Err);
                            return -1;
                        }

                        int rev = 1;

                        if (validFlag)
                        {
                            if (exeOrder.IsValid)
                            {
                                MessageBox.Show("记录【" + exeOrder.Order.Item.Name + "】 时间：" + exeOrder.DateUse.ToString() + " 已经是有效状态！");
                                continue;
                            }
                            rev = CacheManager.InOrderMgr.UpdateExecValidFlag(execOrderID, true, "1");
                        }
                        else
                        {
                            if (!exeOrder.IsValid)
                            {
                                MessageBox.Show("记录【" + exeOrder.Order.Item.Name + "】 时间：" + exeOrder.DateUse.ToString() + " 已经是无效状态！");
                                continue;
                            }
                            rev = CacheManager.InOrderMgr.DcExecImmediate(exeOrder, CacheManager.InOrderMgr.Operator);
                        }

                        if (rev < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("记录【" + exeOrder.Order.Item.Name + "】 时间：" + exeOrder.DateUse.ToString() + " " + strToDo + "出错！\r\n" + exeOrder.Order.Item.Name + "\r\n\r\n" + CacheManager.InOrderMgr.Err);
                            return -1;
                        }
                        else if (rev == 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("记录【" + exeOrder.Order.Item.Name + "】 时间：" + exeOrder.DateUse.ToString() + " " + strToDo + "出错！\r\n" + exeOrder.Order.Item.Name + "\r\n\r\n没有找到对应的执行信息！");
                            return -1;
                        }

                        if (!validFlag && this.neuSpread1_Sheet1.Cells[i, 11].Text.Trim() == "已记")
                        {
                            feeItems += "\r\n" + exeOrder.DateUse.ToString("MM-dd HH:mm") + "【" + exeOrder.Order.Item.Name + "】";
                        }

                        isDoSomeThing = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < neuSpread2_Sheet1.RowCount; i++)
                {
                    if (neuSpread2_Sheet1.IsSelected(i, 0))
                    {
                        string execOrderID = this.neuSpread2_Sheet1.Cells[i, 17].Text.Trim();

                        exeOrder = CacheManager.InOrderMgr.QueryExecOrderByExecOrderID(execOrderID, "2");
                        if (exeOrder == null)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("查找执行档记录出错！\r\n\r\n" + CacheManager.InOrderMgr.Err);
                            return -1;
                        }

                        int rev = 1;

                        if (validFlag)
                        {
                            if (exeOrder.IsValid)
                            {
                                MessageBox.Show("记录【" + exeOrder.Order.Item.Name + "】 时间：" + exeOrder.DateUse.ToString() + " 已经是有效状态！");
                                continue;
                            }
                            rev = CacheManager.InOrderMgr.UpdateExecValidFlag(execOrderID, false, "1");
                        }
                        else
                        {
                            if (!exeOrder.IsValid)
                            {
                                MessageBox.Show("记录【" + exeOrder.Order.Item.Name + "】 时间：" + exeOrder.DateUse.ToString() + " 已经是无效状态！");
                                continue;
                            }
                            rev = CacheManager.InOrderMgr.DcExecImmediate(exeOrder, CacheManager.InOrderMgr.Operator);
                        }

                        if (rev < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("记录【" + exeOrder.Order.Item.Name + "】 时间：" + exeOrder.DateUse.ToString() + " " + strToDo + "出错！\r\n" + exeOrder.Order.Item.Name + "\r\n\r\n" + CacheManager.InOrderMgr.Err);
                            return -1;
                        }
                        else if (rev == 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("记录【" + exeOrder.Order.Item.Name + "】 时间：" + exeOrder.DateUse.ToString() + " " + strToDo + "出错！\r\n" + exeOrder.Order.Item.Name + "\r\n\r\n没有找到对应的执行信息！");
                            return -1;
                        }

                        if (!validFlag && this.neuSpread1_Sheet1.Cells[i, 11].Text.Trim() == "已记")
                        {
                            feeItems += "\r\n" + exeOrder.DateUse.ToString("MM-dd HH:mm") + "【" + exeOrder.Order.Item.Name + "】";
                        }
                        isDoSomeThing = true;
                    }
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            if (!string.IsNullOrEmpty(feeItems))
            {
                FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "", feeItems + "\r\n\r\n已经收费，请注意退费！", ToolTipIcon.Warning);
            }

            if (isDoSomeThing)
            {
                MessageBox.Show("操作成功！");
            }


            this.ShowData(this.patient.ID);

            return 1;
        }

        /// <summary>
        /// 更新有效标记
        /// </summary>
        /// <param name="RowIndex"></param>
        /// <param name="flag">0 设置有效；1 设置无效</param>
        /// <param name="drugFlag">药品标记</param>
        private void SetValidFlag(int RowIndex, string flag,string drugFlag)
        {
            DialogResult r;

            if (drugFlag == "1")
            {
                //if (this.neuSpread1_Sheet1.Cells[RowIndex, 3].Text.Trim() == "有效")
                //{
                //    return;
                //}

                //if (this.neuSpread1_Sheet1.Cells[RowIndex, 4].Text.Trim() != "未发送")
                //{
                //    MessageBox.Show("只有未发送的药品才可以操作！");
                //    return;
                //}

                if (flag == "0")
                {
                    r = MessageBox.Show("确定要恢复该条记录的有效性吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                }
                else
                {
                    r = MessageBox.Show("确定要作废该条记录吗?,该操作不可撤销", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                }
                if (r == DialogResult.Cancel)
                {
                    return;
                }

                string execOrderID = this.neuSpread1_Sheet1.Cells[RowIndex, 27].Text.Trim();

                if (execOrderID == null || execOrderID.Length <= 0)
                {
                    MessageBox.Show("执行流水号为空！");
                    return;
                }
                if (flag == "1")
                {
                    if (this.neuSpread1_Sheet1.Cells[RowIndex, 3].Text.Trim() != "有效")
                    {
                        MessageBox.Show("该条记录已经作废！");
                        return;
                    }

                    this.myExeOrder = new FS.HISFC.Models.Order.ExecOrder();
                    FS.HISFC.Models.Pharmacy.Item objPharmacy = new FS.HISFC.Models.Pharmacy.Item();
                    objPharmacy.ID = this.neuSpread1_Sheet1.Cells[RowIndex, 39].Text;//药品编码
                    objPharmacy.Name = this.neuSpread1_Sheet1.Cells[RowIndex, 5].Text;//药品名称
                    objPharmacy.Specs = this.neuSpread1_Sheet1.Cells[RowIndex, 6].Text;//药品规格
                    objPharmacy.Memo = this.neuSpread1_Sheet1.Cells[RowIndex, 22].Text;//取药药房
                    this.myExeOrder.Order.Item = objPharmacy;//执行档项目
                    this.myExeOrder.Order.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[RowIndex, 7].Value);//药品数量
                    this.myExeOrder.Order.Unit = this.neuSpread1_Sheet1.Cells[RowIndex, 8].Text;//药品单位
                    this.myExeOrder.ID = execOrderID;

                    int _Ret = CacheManager.InOrderMgr.DcExecImmediate(this.myExeOrder, CacheManager.InOrderMgr.Operator);

                    //int _Ret = CacheManager.InOrderMgr.UpdateExecValidFlag(execOrderID, true, flag);

                    if (_Ret < 0)
                    {
                        MessageBox.Show("作废记录出错！\r\n" + CacheManager.InOrderMgr.Err);
                        return;
                    }
                    else if (_Ret == 0)
                    {
                        MessageBox.Show("作废记录出错！\r\n没有找到该条执行信息！");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("作废记录成功！");
                    }

                    if (this.neuSpread1_Sheet1.Cells[RowIndex, 11].Text.Trim() == "已记")
                    {
                        MessageBox.Show("该记录已经收费，请退费！");
                    }
                }
                else
                {
                    int _Ret = CacheManager.InOrderMgr.UpdateExecValidFlag(execOrderID, true, "1");

                    if (_Ret < 0)
                    {
                        MessageBox.Show("恢复记录出错！\r\n" + CacheManager.InOrderMgr.Err);
                        return;
                    }
                    else if (_Ret == 0)
                    {
                        MessageBox.Show("恢复记录出错！\r\n没有找到该条执行信息！");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("恢复记录成功！");
                    }
                }
            }
            else
            {
                if (flag == "0")
                {
                    r = MessageBox.Show("确定要恢复该条记录的有效性吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                }
                else
                {
                    //不允许医生取消已确认的终端项目的医嘱
                    string execOrderId = this.neuSpread2_Sheet1.Cells[RowIndex, 17].Text.Trim();
                    this.myExeOrder = CacheManager.InOrderMgr.QueryExecOrderByExecOrderID(execOrderId, "2");
                    if (myExeOrder.Order.OrderType.Type == FS.HISFC.Models.Order.EnumType.SHORT
                        && myExeOrder.Order.Status == 2
                        && myExeOrder.Order.Item.IsNeedConfirm)
                    {
                        ArrayList execOrderList = CacheManager.InOrderMgr.QueryExecOrderByOrderNo(myExeOrder.Order.ID, myExeOrder.Order.Item.ID, "1");
                        if (execOrderList.Count > 0)
                        {
                            MessageBox.Show("[" + myExeOrder.Order.ExeDept.Name + "]已经对[" + myExeOrder.Order.Item.Name + "]进行收费确认，请与[" + myExeOrder.Order.ExeDept.Name + "]联系，" + "\n\n"
                                + "确认患者是否已经执行[" + myExeOrder.Order.Item.Name + "]，如果已经执行，则该条医嘱不允许作废");
                            return;
                        }
                    }
                    r = MessageBox.Show("确定要作废该条记录吗?,该操作不可撤销", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                }
                if (r == DialogResult.Cancel)
                {
                    return;
                }

                string execOrderID = this.neuSpread2_Sheet1.Cells[RowIndex, 17].Text.Trim();
                this.myExeOrder = new FS.HISFC.Models.Order.ExecOrder();
                myExeOrder.ID = execOrderID;

                if (flag == "1")
                {
                    int _Ret = CacheManager.InOrderMgr.DcExecImmediate(this.myExeOrder, CacheManager.InOrderMgr.Operator);

                    //int _Ret = CacheManager.InOrderMgr.UpdateExecValidFlag(execOrderID, true, flag);

                    if (_Ret < 0)
                    {
                        MessageBox.Show("作废记录出错！\r\n" + CacheManager.InOrderMgr.Err);
                        return;
                    }
                    else if (_Ret == 0)
                    {
                        MessageBox.Show("作废记录出错！\r\n没有找到该条执行信息！");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("作废记录成功！");
                    } 
                    
                    if (this.neuSpread2_Sheet1.Cells[RowIndex, 8].Text.Trim() == "已记")
                    {
                        MessageBox.Show("该记录已经收费，请退费！");
                    }
                }
                else
                {
                    int _Ret = CacheManager.InOrderMgr.UpdateExecValidFlag(execOrderID, false, "1");

                    if (_Ret < 0)
                    {
                        MessageBox.Show("恢复记录出错！\r\n" + CacheManager.InOrderMgr.Err);
                        return;
                    }
                    else if (_Ret == 0)
                    {
                        MessageBox.Show("恢复记录出错！\r\n没有找到该条执行信息！");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("恢复记录成功！");
                    }
                }
            }
            this.ShowData(this.patient.ID);
        }

        /// <summary>
        /// 设置过滤条件,过滤数据
        /// </summary>
        private void SetFilter()
        {
            //过滤数据
            //药品
            if (this.myDataViewDrug.Table != null && this.myDataViewDrug.Table.Rows.Count > 0)
            {
                this.myDataViewDrug.RowFilter = this.filterInput + " AND " + this.filterValid + " AND " + this.filterExec + " AND " + this.filterType;
                this.SetFormatForDrug();
            }

            //非药品
            if (this.myDataViewUndrug.Table != null && this.myDataViewUndrug.Table.Rows.Count > 0)
            {
                this.myDataViewUndrug.RowFilter = this.filterInput + " AND " + this.filterValid + " AND " + this.filterType;
                this.SetFormatForUnDrug();
            }

            this.RefreshDrugBackColor();
        }

        /// <summary>
        /// 按发送状态过滤医嘱
        /// </summary>
        /// <param name="State"></param>
        public void Filter1(int State)
        {
            if (this.Patient == null) return;
            if (this.Patient.ID == "") return;

            //查询时候才能过滤
            switch (State)
            {
                case 0://全部
                    this.filterExec = "1=1";
                    break;
                case 1://当天
                    this.filterExec = "发送状态 = '已发送'";//3
                    break;
                case 2://无效
                    this.filterExec = "发送状态 = '未发送'";
                    break;
                case 3:
                    this.filterExec = "发送状态 = '已发药'";
                    break;
                default:
                    this.filterExec = "1=1";
                    this.filterValid = "1=1";
                    this.filterType = "1=1";
                    break;
            }
            this.SetFilter();
        }

        /// <summary>
        /// 过滤医嘱显示
        /// </summary>
        /// <param name="State"></param>
        public void Filter2(int State)
        {
            if (this.Patient == null) return;
            if (this.Patient.ID == "") return;
            //查询时候才能过滤
            switch (State)
            {
                case 0://全部
                    this.filterValid = "1=1";
                    break;
                case 1://当天
                    this.filterValid = "有效 = '有效'";//3
                    break;
                case 2://有效
                    this.filterValid = "有效 = '无效'";
                    break;
                default:
                    this.filterExec = "1=1";
                    this.filterValid = "1=1";
                    this.filterType = "1=1";
                    break;
            }
            this.SetFilter();
        }

        /// <summary>
        /// 过滤医嘱显示
        /// </summary>
        /// <param name="State"></param>
        public void Filter3(int State)
        {
            if (this.Patient == null) return;
            if (this.Patient.ID == "") return;
            //查询时候才能过滤
            switch (State)
            {
                case 0://全部
                    this.filterType = "1=1";
                    break;
                case 1://长期医嘱
                    this.filterType = "医嘱类型 = '长期医嘱'";
                    break;
                case 2://临时医嘱
                    this.filterType = "医嘱类型 = '临时医嘱'";
                    break;
                default:
                    this.filterExec = "1=1";
                    this.filterValid = "1=1";
                    this.filterType = "1=1";
                    break;
            }
            this.SetFilter();
        }

        /// <summary>
        /// load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucOrderExeQuery_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 有效状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter2(this.neuComboBox2.SelectedIndex);
        }

        /// <summary>
        /// 发送状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter1(this.neuComboBox1.SelectedIndex);
        }

        /// <summary>
        /// 检索拼音码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTextBox1_TextChanged(object sender, EventArgs e)
        {
            //取输入码
            string queryCode = this.neuTextBox1.Text.Trim().ToUpper() ;
            //if (this.chbMisty.Checked)
            //{
            //    queryCode = "%" + queryCode + "%";
            //}
            //else
            //{
                queryCode = queryCode + "%";
            //}

            //设置过滤条件
            this.filterInput = "((拼音码 LIKE '%" + queryCode + "%') OR " +
                "(五笔码 LIKE '%" + queryCode + "%') OR " +
                "(名称 LIKE '%" + queryCode + "%') )";

            //过滤药品数据
            this.SetFilter();
        }

        /// <summary>
        /// fp1保存为 xml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            if (this.isAutoSaveXML)
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.drugQuery);
            }
        }

        /// <summary>
        /// fp2保存为xml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread2_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            if (this.isAutoSaveXML)
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread2_Sheet1, this.undrugQuery);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void neuSpread1_ActiveSheetChanged(object sender, EventArgs e)
        {
            RefreshDrugBackColor();
        }

        /// <summary>
        /// 双击设置药品执行档为有效或取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.neuTabControl1.SelectedTab == this.tabPage1)
            {
                if (this.neuSpread1_Sheet1.ActiveRowIndex >= 0)
                {
                    string strValid = this.neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.ActiveRowIndex, 3].Text;

                    if (strValid == "无效")
                    {
                        this.SetValidFlag(this.neuSpread1_Sheet1.ActiveRowIndex, "0", "1");
                    }
                    else
                    {
                        this.SetValidFlag(this.neuSpread1_Sheet1.ActiveRowIndex, "1", "1");
                    }
                }
            }
            else
            {
                if (this.neuSpread2_Sheet1.ActiveRowIndex >= 0)
                {
                    string strValid = this.neuSpread2.Sheets[0].Cells[neuSpread2_Sheet1.ActiveRowIndex, 2].Text;

                    if (strValid == "无效")
                    {
                        this.SetValidFlag(this.neuSpread2_Sheet1.ActiveRowIndex, "0", "0");
                    }
                    else
                    {
                        this.SetValidFlag(this.neuSpread2_Sheet1.ActiveRowIndex, "1", "0");
                    }
                }
            }
        }

        /// <summary>
        /// 双击设置非药品执行档为有效或取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void neuSpread2_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.neuSpread2_Sheet1.ActiveRowIndex >= 0)
            {
                string strValid = this.neuSpread2.Sheets[0].Cells[neuSpread2_Sheet1.ActiveRowIndex, 2].Text;

                if (strValid == "无效")
                {
                    this.SetValidFlag(this.neuSpread2_Sheet1.ActiveRowIndex, "0", "0");
                }
                else
                {
                    this.SetValidFlag(this.neuSpread2_Sheet1.ActiveRowIndex, "1", "0");
                }
            }
        }

        /// <summary>
        /// 医嘱类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter3(this.neuComboBox3.SelectedIndex);
        }

        /// <summary>
        /// 打印函数,取接口反射
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            //无用 先屏蔽了 2011-9-18 houwb
            return 1;

            if (this.GetItemInfo() == -1) 
                return -1;

            FS.HISFC.Components.Order.Classes.IOrderExeQuery printInterFace = null;

            printInterFace = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.Components.Order.Classes.IOrderExeQuery)) as FS.HISFC.Components.Order.Classes.IOrderExeQuery;
            if (printInterFace != null)
            {
                printInterFace.patientInfoObj = this.patient;
                if (printInterFace.SetValue(this.alExeOrder) == 1)
                {
                    printInterFace.Print();
                }
            }
            return base.OnPrint(sender, neuObject);
        }

        private int GetItemInfo()
        {
            if (this.neuTabControl1.SelectedTab == this.tabPage1)
            {
                Hashtable myhashtable = new Hashtable();
                for (int a = 0; a < this.neuSpread1_Sheet1.RowCount; a++)
                {
                    if (FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[a, 0].Value) == true)
                    {
                        string strDrugStoreName = this.neuSpread1_Sheet1.Cells[a, 21].Text;
                        if (!myhashtable.ContainsKey(strDrugStoreName))
                        {
                            myhashtable.Add(strDrugStoreName, strDrugStoreName);
                        }
                    }
                }
                if (myhashtable.Count > 1)
                {
                    System.Windows.Forms.MessageBox.Show("您选择了多个药房的药品，请认真核对打印药方！");
                    return -1;
                }
                this.alExeOrder = new ArrayList();
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    if (FS.FrameWork.Function.NConvert.ToBoolean( this.neuSpread1_Sheet1.Cells[i, 0].Value) == true)
                    {
                        if (this.neuSpread1_Sheet1.Cells[i, 2].Text == "长期医嘱")
                        {
                            System.Windows.Forms.MessageBox.Show("您在第【" + (i + 1).ToString() + " 】行选择了长期医嘱,请选择临时医嘱打印药方！");
                            return -1;

                        }
                        if (this.neuSpread1_Sheet1.Cells[i, 21].Text == this.neuSpread1_Sheet1.Cells[i, 21].Text)
                        {
                            
                        }
                        try
                        {
                            this.myExeOrder = new FS.HISFC.Models.Order.ExecOrder();
                            FS.HISFC.Models.Pharmacy.Item objPharmacy = new FS.HISFC.Models.Pharmacy.Item();
                            objPharmacy.ID    = this.neuSpread1_Sheet1.Cells[i, 39].Text;//药品编码
                            objPharmacy.Name  = this.neuSpread1_Sheet1.Cells[i, 5].Text;//药品名称
                            objPharmacy.Specs = this.neuSpread1_Sheet1.Cells[i, 6].Text;//药品规格
                            objPharmacy.Memo  = this.neuSpread1_Sheet1.Cells[i, 21].Text;//取药药房
                            this.myExeOrder.Order.Item = objPharmacy;//执行档项目
                            this.myExeOrder.Order.Qty  = FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, 7].Value);//药品数量
                            this.myExeOrder.Order.Unit = this.neuSpread1_Sheet1.Cells[i, 8].Text;//药品单位
                            this.alExeOrder.Add(this.myExeOrder);
                        }
                        catch(Exception ex)
                        {
                            return -1;
                        }
                        
                    }
                }
            }
            return 1;
        }

        #region addby xuewj 2009-8-24 恢复误操作的作废项目，以便使患者能正常用药,作废执行档 {01F18F48-887D-4d2a-A0F9-757B61A5B8A6}

        /// <summary>
        /// 恢复执行档记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVaildExecOrder_Click(object sender, EventArgs e)
        {
            this.SetValidFlag(true);
            //if (this.neuTabControl1.SelectedTab == this.tabPage1)
            //{
            //    if (this.neuSpread1_Sheet1.ActiveRowIndex >= 0 && this.neuSpread1_Sheet1.RowCount > 0)
            //    {
            //        this.SetValidFlag(this.neuSpread1_Sheet1.ActiveRowIndex, "0", "1");
            //    }
            //}
            //else if (this.neuTabControl1.SelectedTab == this.tabPage2)
            //{
            //    if (this.neuSpread2_Sheet1.ActiveRowIndex >= 0 && this.neuSpread2_Sheet1.RowCount > 0)
            //    {
            //        this.SetValidFlag(this.neuSpread2_Sheet1.ActiveRowIndex, "0", "0");
            //    }
            //}
        }

        /// <summary>
        /// 设置为无效
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUNVaildExecOrder_Click(object sender, EventArgs e)
        {
            this.SetValidFlag(false);
            //if (this.neuTabControl1.SelectedTab == this.tabPage1)
            //{
            //    if (this.neuSpread1_Sheet1.ActiveRowIndex >= 0 && this.neuSpread1_Sheet1.RowCount > 0)
            //    {
            //        this.SetValidFlag(this.neuSpread1_Sheet1.ActiveRowIndex, "1", "1");
            //    }
            //}
            //else
            //{
            //    if (this.neuSpread2_Sheet1.ActiveRowIndex >= 0 && this.neuSpread2_Sheet1.RowCount > 0)
            //    {
            //        this.SetValidFlag(this.neuSpread2_Sheet1.ActiveRowIndex, "1", "0");
            //    }
            //}
        }

        #endregion

        private void neuLinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.neuTabControl1.SelectedTab == this.tabPage1)
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.drugQuery);
            }
            else
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread2_Sheet1, this.undrugQuery);
            }

            MessageBox.Show("格式保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 列设置
        /// </summary>
        //Enum EnumColumSet
        //{

        //}
    }
}
