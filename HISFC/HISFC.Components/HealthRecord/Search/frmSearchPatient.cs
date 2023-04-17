using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace FS.HISFC.Components.Common.Forms
{
    public partial class frmSearchPatient : Form
    {
        public frmSearchPatient()
        {
            InitializeComponent();
        }

        FS.HISFC.Models.HealthRecord.Base baseObj = new FS.HISFC.Models.HealthRecord.Base();

        public delegate void ListShowdelegate(FS.HISFC.Models.HealthRecord.Base obj);
        public event ListShowdelegate SelectItem;

        #region 方法

        #region 枚举
        private enum Cols
        {
            outDept, //出院日期
            outTime,//入院日期
            strName,//姓名
            sexName,//性别
            inpatientNO,//住院流水号
            caseNo,//病案号
            patientNO,//住院号
            times,//第几次
            Memo

        }
        #endregion 

        /// <summary>
        /// 查询符合条件的信息
        /// zhangjunyi@FS.com 2005.6.29
        /// </summary>
        private void SearchInfo()
        {
            try
            {
                //病案基本信息操作类
                FS.HISFC.BizLogic.HealthRecord.Base baseDml = new FS.HISFC.BizLogic.HealthRecord.Base();
                string strWhere = "";
                if (this.neuTabControl2.SelectedIndex == 0)  //基本信息
                {
                    strWhere = this.ucCustomQuery1.GetWhereString();
                    if (strWhere == "")
                    {
                        MessageBox.Show("请输入查询条件！");
                        return;
                    }
                        strWhere = " where " + strWhere;
                }
                else if (this.neuTabControl2.SelectedIndex == 1)  //诊断信息
                {
                    strWhere = this.ucCustomQuery2.GetWhereString();
                    if (strWhere == "")
                    {
                        MessageBox.Show("请输入查询条件！");
                        return;
                    }
                    //strWhere = "WHERE Inpatient_No IN  (select Inpatient_No from met_cas_diagnose WHERE " + strWhere + " )";
                    strWhere = "WHERE Inpatient_No IN  (select Inpatient_No from met_cas_diagnose WHERE " + strWhere + " )";//之前加了个表后患无穷啊
                }
                else   //手术信息
                {
                    strWhere = this.ucCustomQuery3.GetWhereString();
                    if (strWhere == "")
                    {
                        MessageBox.Show("请输入查询条件！");
                        return;
                    }
                    strWhere = "WHERE Inpatient_No IN  (select Inpatient_No from met_cas_operationdetail WHERE " + strWhere + " )";
                }


                //等待窗口
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询数据，请稍候...");
                Application.DoEvents();
                ArrayList list = baseDml.QueryCaseBaseInfoByOwnConditions(strWhere);

                if (list == null)
                {
                    MessageBox.Show("查询数据失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return;
                }
                this.fpSpread1_Sheet1.RowCount = 0;
                foreach (FS.HISFC.Models.HealthRecord.Base obj in list)
                {
                    int row = this.fpSpread1_Sheet1.Rows.Count;
                    this.fpSpread1_Sheet1.Rows.Add(row, 1);
                    this.fpSpread1_Sheet1.Cells[row, (int)Cols.outDept].Text = obj.OutDept.Name;//出院科室
                    this.fpSpread1_Sheet1.Cells[row, (int)Cols.outTime].Text = obj.PatientInfo.PVisit.OutTime.ToString();//出院时间
                    this.fpSpread1_Sheet1.Cells[row, (int)Cols.strName].Text = obj.PatientInfo.Name;//姓名
                    this.fpSpread1_Sheet1.Cells[row, (int)Cols.sexName].Text = obj.PatientInfo.Sex.Name;//性别
                    this.fpSpread1_Sheet1.Cells[row, (int)Cols.inpatientNO].Text = obj.PatientInfo.ID;//住院流水号
                    this.fpSpread1_Sheet1.Cells[row, (int)Cols.caseNo].Text = obj.CaseNO; //病案号
                    this.fpSpread1_Sheet1.Cells[row, (int)Cols.caseNo].Tag = obj; //病案号
                    this.fpSpread1_Sheet1.Cells[row, (int)Cols.patientNO].Text = obj.PatientInfo.PID.PatientNO;//住院号
                    this.fpSpread1_Sheet1.Cells[row, (int)Cols.times].Text = obj.PatientInfo.InTimes.ToString();//入院次数
                    this.fpSpread1_Sheet1.Cells[row, (int)Cols.Memo].Text = obj.FourDiseasesReport;//诊断名称 2012-1-10 chengym 四病报告不知道哪里加的，这里用来处理诊断信息
                } 

                //插入数据
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 自定义快捷键
        /// zhangjunyi@FS.com 2005.6.29
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData.GetHashCode() == Keys.F.GetHashCode() + Keys.Alt.GetHashCode())
            {
                //查询
                this.SearchInfo();
            }
            if (keyData.GetHashCode() == Keys.R.GetHashCode() + Keys.Alt.GetHashCode())
            {
                //重置
            }
            if (keyData.GetHashCode() == Keys.X.GetHashCode() + Keys.Alt.GetHashCode())
            {
                //关闭
                this.Close();
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// 获取当前选择的项
        /// </summary>
        /// <returns></returns>
        public FS.HISFC.Models.HealthRecord.Base GetCaseInfo()
        {
            int Row = this.fpSpread1_Sheet1.ActiveRowIndex;
            if (Row == -1)
            {
                return null;
            }
            baseObj = (FS.HISFC.Models.HealthRecord.Base)this.fpSpread1_Sheet1.Cells[Row, (int)Cols.caseNo].Tag;
            return baseObj;
        }

        #region  上移下移
        /// <summary>
        /// 下一行
        /// </summary>
        public void NextRow()
        {
            if (this.fpSpread1_Sheet1.RowCount == 0)
            {
                return;
            }
            int _Row = fpSpread1_Sheet1.ActiveRowIndex;
            if (_Row < this.fpSpread1_Sheet1.RowCount - 1)
            {
                _Row = _Row + 1;
                fpSpread1_Sheet1.ActiveRowIndex = _Row;
                fpSpread1_Sheet1.AddSelection(_Row, 0, 1, 0);
            }
        }
        /// <summary>
        /// 前一行
        /// </summary>
        public void PriorRow()
        {
            if (this.fpSpread1_Sheet1.RowCount == 0)
            {
                return;
            }
            int _Row = fpSpread1_Sheet1.ActiveRowIndex;
            if (_Row > 0)
            {
                _Row = _Row - 1;
                fpSpread1_Sheet1.ActiveRowIndex = _Row;
                fpSpread1_Sheet1.AddSelection(_Row, 0, 1, 0);
            }
        }
        #endregion 
        #endregion

        private void frmSearchPatient_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        private void neuToolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            switch (this.neuToolBar1.Buttons.IndexOf(e.Button))
            {
                case 0:
                    //查询
                    SearchInfo();
                    break;
                case 1:
                    this.ucCustomQuery1.btnReset_Click(sender, e);
                    //重置
                    break;
                case 2: //guan关闭
                    this.Close();
                    break;
            }
        }

        private void fpSpread1_DoubleClick(object sender, EventArgs e)
        {
            baseObj = GetCaseInfo();
            SelectItem(baseObj);
            this.Close();
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            baseObj = GetCaseInfo();
            SelectItem(baseObj);
            this.Close();
        }

    }
}