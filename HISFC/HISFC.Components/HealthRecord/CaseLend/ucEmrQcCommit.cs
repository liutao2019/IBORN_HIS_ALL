using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.HealthRecord.CaseLend
{
    /// <summary>
    /// 电子病历归档
    /// </summary>
    public partial class ucEmrQcCommit : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 
        /// </summary>
        public ucEmrQcCommit()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 借阅业务类 不是太规则
        /// </summary>
        FS.HISFC.BizLogic.HealthRecord.CaseCard caseCardMgr = new FS.HISFC.BizLogic.HealthRecord.CaseCard();
        //病案基本信息操作类
        private FS.HISFC.BizLogic.HealthRecord.Base baseDml = new FS.HISFC.BizLogic.HealthRecord.Base();

        private int  maxLendDays = 30;
        
        /// <summary>
        /// 电子病历借阅
        /// </summary>
        [Category("最大借阅天数"), Description("电子病历的借阅天数，超过最大天数，自动归还不允许查询")]
        public int MaxLendDays
        {
            get { return this.maxLendDays; }
            set { this.maxLendDays = value; }
        }

        private bool isQueryConsultingCommit = false;

        /// <summary>
        /// 查询科主任提交数据归档
        /// </summary>
        [Category("界面设置"), Description("false医师提交 ture科主任提交")]
        public bool IsQueryConsultingCommit
        {
            get { return this.isQueryConsultingCommit; }
            set { this.isQueryConsultingCommit = value; }
        }

        private bool isCancel = false;

        /// <summary>
        /// 取消归档
        /// </summary>
        [Category("界面设置"), Description("取消归档")]
        public bool IsCancel
        {
            get { return this.isCancel; }
            set { this.isCancel = value; }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return base.OnQuery(sender, neuObject);
        }
        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            DateTime dtBegin = this.dateTimePicker1.Value.Date;
            DateTime dtEnd = this.dateTimePicker2.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            string Status =string.Empty;
            if (isQueryConsultingCommit)//状态 0未提交 1医师提交 2科主任提交 3病案室提交 -1科主任退回 -2病案室退回 4病案室借出（借阅）
            {
                Status = "2";
            }
            else
            {
                Status = "1";
            }
            if (this.isCancel)//取消归档不设置日期
            {
                dtBegin = System.DateTime.Now.Date.AddYears(-10);
                dtEnd = System.DateTime.Now;
                Status = "3";
            }
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            DataSet ds = new DataSet();
            caseCardMgr.QueryCaseCommitByCommitDateAndStatus(dtBegin, dtEnd, Status, ref ds);
            if (ds == null || ds.Tables.Count<=0)
            {
                return;
            }
            this.fpSpread1_Sheet1.RowCount = 0;
            int row = this.fpSpread1_Sheet1.RowCount;
            foreach (DataRow dtrow in ds.Tables[0].Rows)
            {
                this.fpSpread1_Sheet1.Rows.Add(row, 1);
                this.fpSpread1_Sheet1.Cells[row,0].CellType = checkType;
                this.fpSpread1_Sheet1.Cells[row, 0].Text = "False";
                this.fpSpread1_Sheet1.Cells[row, 1].Text = dtrow[0]== DBNull.Value ? string.Empty : dtrow[0].ToString();
                this.fpSpread1_Sheet1.Cells[row, 2].Text = dtrow[1] == DBNull.Value ? string.Empty : dtrow[1].ToString();
                this.fpSpread1_Sheet1.Cells[row, 3].Text = dtrow[2] == DBNull.Value ? string.Empty : dtrow[2].ToString();
                this.fpSpread1_Sheet1.Cells[row, 4].Text = dtrow[3] == DBNull.Value ? string.Empty : dtrow[3].ToString();
                this.fpSpread1_Sheet1.Cells[row, 5].Text = dtrow[4] == DBNull.Value ? string.Empty : dtrow[4].ToString();
                this.fpSpread1_Sheet1.Cells[row, 6].Text = dtrow[5] == DBNull.Value ? string.Empty : dtrow[5].ToString();
            }
            
        }

        /// <summary>
        ///  保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return base.OnSave(sender, neuObject);
        }
        /// <summary>
        /// 保存更新日期
        /// </summary>
        protected void Save()
        {
            
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.caseCardMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.baseDml.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            FS.HISFC.Models.HealthRecord.Lend info = new FS.HISFC.Models.HealthRecord.Lend();
            List<FS.HISFC.Models.HealthRecord.Lend> listInfo = new List<FS.HISFC.Models.HealthRecord.Lend>();

            FS.HISFC.Models.HealthRecord.Base baseInfo = new FS.HISFC.Models.HealthRecord.Base();
            List<FS.HISFC.Models.HealthRecord.Base> listBase = new List<FS.HISFC.Models.HealthRecord.Base>();
            for (int row = 0; row < this.fpSpread1_Sheet1.RowCount; row++)
            {
                if (this.fpSpread1_Sheet1.Cells[row, 0].Text == "False")
                {
                    continue;
                }
                info = new FS.HISFC.Models.HealthRecord.Lend();
                info.ID = this.fpSpread1_Sheet1.Cells[row, 1].Text;
                if (isCancel)
                {
                    info.LendStus = "-2";
                }
                else
                {
                    info.LendStus = "3";//病案提交状态
                }
                info.LendDate=this.caseCardMgr.GetDateTimeFromSysDateTime();
                info.PrerDate = info.LendDate.AddDays(this.maxLendDays);
                info.OperInfo.ID = this.caseCardMgr.Operator.ID;
                listInfo.Add(info);

                baseInfo = new FS.HISFC.Models.HealthRecord.Base();
                baseInfo.PatientInfo.ID = this.fpSpread1_Sheet1.Cells[row, 1].Text;
                if (isCancel)
                {
                    baseInfo.PatientInfo.CaseState = "2";
                }
                else
                {
                    baseInfo.PatientInfo.CaseState = "4";
                }
                baseInfo.LendStat = "I";
                listBase.Add(baseInfo);
            }

            foreach (FS.HISFC.Models.HealthRecord.Lend obj in listInfo)
            {
                if (this.caseCardMgr.UpdateEmrQcCommit(obj) < 0)
                {
                    if (this.isCancel)
                    {
                        MessageBox.Show("取消病历归档失败！");
                    }
                    else
                    {
                        MessageBox.Show("病历归档失败！");
                    }
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return;
                }
            }

            foreach (FS.HISFC.Models.HealthRecord.Base objBase in listBase)
            {
                if (this.baseDml.UpdateBaseCaseStus(objBase.PatientInfo.ID,"",objBase.PatientInfo.CaseState,objBase.LendStat) < 0)
                {
                    if (this.isCancel)
                    {
                        MessageBox.Show("取消病历封存失败！");
                    }
                    else
                    {
                        MessageBox.Show("病历封存失败！");
                    }
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            this.fpSpread1_Sheet1.RowCount = 0;
            if (this.isCancel)
            {
                MessageBox.Show("取消病历归档成功！");
            }
            else
            {
                MessageBox.Show("病历提交成功！");
            }
            return;
        }
        /// <summary>
        /// 全选全不选事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                for (int row = 0; row < this.fpSpread1_Sheet1.RowCount; row++)
                {
                    this.fpSpread1_Sheet1.Cells[row, 0].Text = "True";
                    this.checkBox1.Text = "全不选";
                }
            }
            else
            {
                for (int row = 0; row < this.fpSpread1_Sheet1.RowCount; row++)
                {
                    this.fpSpread1_Sheet1.Cells[row, 0].Text = "False";
                }
                this.checkBox1.Text = "全选";
            }
        }
        /// <summary>
        /// 查询一个月30天内的归档数据
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            this.dateTimePicker1.Value = this.caseCardMgr.GetDateTimeFromSysDateTime().Date.AddDays(-30);
            this.Query();
            base.OnLoad(e);
        }
        
    }
}
