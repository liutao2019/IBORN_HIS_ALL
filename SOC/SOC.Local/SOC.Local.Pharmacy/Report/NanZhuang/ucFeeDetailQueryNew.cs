using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Pharmacy.Report.NanZhuang
{
    public partial class ucFeeDetailQueryNew : Base.BaseReport
    {

        private bool isCustomTextToTag = false;
        [Category("Query查询设置"),Description("查询时，是否将Text进行查询（将Text转换为Tag）")]
        public bool IsCustomTextToTag
        {
            get
            {
                return isCustomTextToTag;
            }
            set
            {
                isCustomTextToTag = value;
            }
        }

        public ucFeeDetailQueryNew()
        {
            InitializeComponent();

            this.SQLIndexs = "NanZhuang.Fee.FeeDetail.Query";
            this.MainTitle = "患者处方、治疗项目明细表";
            this.MidAdditionTitle = "";
            this.RightAdditionTitle = "";
            this.IsDeptAsCondition = false;
            this.QueryDataWhenInit = false;
            this.IsUseCustomType = true;
            this.CustomTypeSQL = @"SELECT A.EMPL_CODE id, A.EMPL_NAME name, '' memo,A.SPELL_CODE spell_code, A.WB_CODE                                        wb_code,A.EMPL_CODE user_code
                                    FROM COM_EMPLOYEE A
                                    WHERE A.EMPL_TYPE = 'D'";

            if (this.cmbDept.alItems != null && this.cmbDept.alItems.Count > 0)
            {
                this.cmbDept.SelectedIndex = 0;
                this.cmbDept.Tag = (this.cmbDept.alItems[0] as FS.FrameWork.Models.NeuObject).ID;
            }
            FS.HISFC.BizProcess.Integrate.Manager inteMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            System.Collections.ArrayList alDoctor = inteMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (alDoctor == null)
            {
                MessageBox.Show("查询医生出错：" + inteMgr.Err);
                return;
            }

            this.ncbDoctor.AddItems(alDoctor);
            this.QueryEndHandler += new DelegateQueryEnd(ucFeeDetailQuery_QueryEndHandler);
            this.OperationStartHandler += new DelegateOperationStart(ucFeeDetailQuery_OperationStartHandler);
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns></returns>
        private new string[] GetQueryConditions()
        {
            if (this.IsDeptAsCondition && this.IsTimeAsCondition)
            {
                string[] parm = { "", "", "", "", "", "" };
                if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                {
                    parm[0] = this.cmbDept.Tag.ToString();
                }
                parm[1] = this.dtStart.Value.ToString();
                parm[2] = this.dtEnd.Value.ToString();
                parm[3] = this.GetParm()[0];
                parm[4] = this.GetParm()[1];

                return parm;
            }
            if (!this.IsDeptAsCondition && this.IsTimeAsCondition)
            {
                string[] parm = { "", "", "", "", "", "", "", "" };
                parm[0] = this.dtStart.Value.ToString();
                parm[1] = this.dtEnd.Value.ToString();
                parm[2] = this.GetParm()[0];
                parm[3] = this.GetParm()[1];
                return parm;
            }
            if (this.IsDeptAsCondition && !this.IsTimeAsCondition)
            {
                string[] parm = { "", "", "", "", "", "", "" };
                if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                {
                    parm[0] = this.cmbDept.Tag.ToString();
                }
                parm[1] = this.GetParm()[0];
                parm[2] = this.GetParm()[1];
                return parm;
            }

            string[] parmNull = { "Null", "Null", "Null", "Null", "Null", "Null" };
            return parmNull;
        }

        /// <summary>
        /// 获取不定查询条件
        /// </summary>
        /// <returns></returns>
        private string[] GetParm()
        {
            string doctorNO = "All";
            if (this.ncbDoctor.Tag != null && !string.IsNullOrEmpty(this.ncbDoctor.Tag.ToString()) && !string.IsNullOrEmpty(this.ncbDoctor.Text.Trim()))
            {
                doctorNO = this.ncbDoctor.Tag.ToString();
            }
            string patientInfo = this.nPatientInfo.Text;
            if (string.IsNullOrEmpty(patientInfo))
            {
                patientInfo = "All";
            }
            return new string[] { doctorNO, patientInfo };

        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryAdditionConditions = this.GetQueryConditions();
            this.IsNeedAdditionConditions = true;
            return base.OnQuery(sender, neuObject);
        }

        void ucFeeDetailQuery_OperationStartHandler(string type)
        {
            if (type == "query")
            {
                if (this.isCustomTextToTag)
                {
                    this.cmbCustomType.Tag = this.cmbCustomType.Text;
                }
            }
        }

        void ucFeeDetailQuery_QueryEndHandler()
        {
            if (this.fpSpread1_Sheet1.RowCount == 0)
            {
                return;
            }
            //this.fpSpread1_Sheet1.Columns[0].Visible = false;
            string lastRowMemo = this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 0].Text;
            for (int rowIndex = this.fpSpread1_Sheet1.RowCount - 1; rowIndex > -1; rowIndex--)
            {
                string nowRowMemo = this.fpSpread1_Sheet1.Cells[rowIndex, 0].Text;
               

                if (lastRowMemo != nowRowMemo)
                {
                    this.fpSpread1_Sheet1.AddRows(rowIndex + 1, 1);
                    this.fpSpread1_Sheet1.Cells[rowIndex + 1, 1].ColumnSpan = this.fpSpread1_Sheet1.ColumnCount - 2;
                    this.fpSpread1_Sheet1.Cells[rowIndex + 1, 1].Text = lastRowMemo;
                    this.fpSpread1_Sheet1.Cells[rowIndex + 1, 1].Font = new Font("宋体", this.Font.Size, FontStyle.Bold);
                    this.fpSpread1_Sheet1.Rows[rowIndex + 1].Tag = "SpanRow";
                    lastRowMemo = nowRowMemo;
                }

                if (rowIndex == 0 && lastRowMemo == nowRowMemo)
                {
                    this.fpSpread1_Sheet1.AddRows(0, 1);
                    this.fpSpread1_Sheet1.Cells[0, 1].ColumnSpan = this.fpSpread1_Sheet1.ColumnCount - 2;
                    this.fpSpread1_Sheet1.Cells[0, 1].Text = nowRowMemo;
                    this.fpSpread1_Sheet1.Cells[0, 1].Font = new Font("宋体", this.Font.Size, FontStyle.Bold);
                    this.fpSpread1_Sheet1.Rows[0].Tag = "SpanRow";
                }
            }
        }
    }
}
