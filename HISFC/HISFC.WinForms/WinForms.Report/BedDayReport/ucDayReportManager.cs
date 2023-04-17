using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using FS.FrameWork.Function;
using System.Collections;

namespace FS.WinForms.Report.BedDayReport
{
    public partial class ucDayReportManager : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDayReportManager()
        {
            InitializeComponent();
        }

        private FS.HISFC.BizLogic.RADT.InpatientDayReport dayReportManager = new FS.HISFC.BizLogic.RADT.InpatientDayReport();

        protected DateTime StatDate
        {
            get
            {
                return NConvert.ToDateTime(this.neuDateTimePicker1.Text);
            }
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryDayReport();

            return base.OnQuery(sender, neuObject);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveDayReport();

            return base.OnSave(sender, neuObject);
        }

        #region {07448623-4975-4279-8C25-27CAE84EA4BA}
        private OperateType operType = OperateType.All;

        /// <summary>
        /// 数据权限设置
        /// </summary>
        [Category("控件设置"), Description("操作数据的范围")]
        public OperateType OperateType
        {
            get
            {
                return this.operType;
            }
            set
            {
                this.operType = value;
            }
        }

        #endregion
        /// <summary>
        /// 获取床位日报
        /// </summary>
        protected void QueryDayReport()
        {
            ArrayList alStatList = dayReportManager.GetInpatientDayReportList(this.StatDate);
            if (alStatList == null)
            {
                MessageBox.Show("获取床位日报统计汇总信息发生错误");
                return;
            }

            this.neuSpread1_Sheet1.Rows.Count = 0;
            Hashtable hsDayReportDept = new Hashtable();

            foreach (FS.HISFC.Models.HealthRecord.InpatientDayReport info in alStatList)
            {
                hsDayReportDept.Add(info.ID, null);


                #region {07448623-4975-4279-8C25-27CAE84EA4BA}

                if (this.operType == OperateType.All)
                {
                    this.neuSpread1_Sheet1.Rows.Add(0, 1);
                    this.AddDataToFp(0, info);
                }
                else if (this.operType == OperateType.Dept)
                {
                    if (info.ID == ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID)
                    {
                        this.neuSpread1_Sheet1.Rows.Add(0, 1);
                        this.AddDataToFp(0, info);
                    }
                } 
                #endregion
            }

            #region {07448623-4975-4279-8C25-27CAE84EA4BA}

            if (this.operType == OperateType.All)
            {
                ArrayList al = this.QueryDeptStat();
                if (al != null)
                {
                    foreach (FS.FrameWork.Models.NeuObject tempStat in al)
                    {
                        if (!hsDayReportDept.ContainsKey(tempStat.ID))
                        {
                            FS.HISFC.Models.HealthRecord.InpatientDayReport temp = new FS.HISFC.Models.HealthRecord.InpatientDayReport();
                            temp.ID = tempStat.ID;
                            temp.Name = tempStat.Name;
                            temp.DateStat = this.StatDate;

                            this.neuSpread1_Sheet1.Rows.Add(0, 1);

                            this.AddDataToFp(0, temp);
                        }
                    }
                }
            } 
            #endregion
        }

        private int AddDataToFp(int rowIndex,FS.HISFC.Models.HealthRecord.InpatientDayReport info)
        {
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColDeptCode].Text = info.ID;
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColDeptName].Text = info.Name;

            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColStarandNum].Text = info.BedStand.ToString();
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColAddNum].Text = info.BedAdd.ToString();
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColFreeNum].Text = info.BedFree.ToString();
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColBeginNum].Text = info.BeginningNum.ToString();
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColInNum].Text = info.InNormal.ToString();
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColTransferIn].Text = info.InTransfer.ToString();
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColTransferOut].Text = info.OutTransfer.ToString();
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColOutNu].Text = info.OutNormal.ToString();
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColEndNum].Text = info.EndNum.ToString();
            
            //if (this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColStarandNum].Text == "0")
            //    this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColStarandNum].Text == "";
            if (this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColStarandNum].Text == "0")
                this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColStarandNum].Text = "";
            if (this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColAddNum].Text == "0")
                this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColAddNum].Text = "";
            if (this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColFreeNum].Text == "0")
                this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColFreeNum].Text = "";
            if (this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColBeginNum].Text == "0")
                this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColBeginNum].Text = "";
            if (this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColInNum].Text == "0")
                this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColInNum].Text = "";
            if (this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColTransferIn].Text == "0")
                this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColTransferIn].Text = "";
            if (this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColTransferOut].Text == "0")
                this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColTransferOut].Text = "";
            if (this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColOutNu].Text == "0")
                this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColOutNu].Text = "";
            if (this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColEndNum].Text == "0")
                this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColEndNum].Text = "";
          
            this.neuSpread1_Sheet1.Rows[rowIndex].Tag = info;

            return 1;
        }

        /// <summary>
        /// 获取病案科室结构
        /// </summary>
        protected ArrayList QueryDeptStat()
        {
            FS.HISFC.BizLogic.Manager.DepartmentStatManager myDeptManager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            //ArrayList al = myDeptManager.LoadDepartmentStat("72");
            ArrayList al = myDeptManager.LoadDepartmentStatAndByNodeKind("72","1");
            //FS.HISFC.BizLogic.Manager.DepartmentStatManager myDepartmentStatManager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            //ArrayList al = myDepartmentStatManager.LoadDepartmentStatAndByNodeKind("72", "0");

            if (al == null)
            {
                MessageBox.Show("获取病案科室结构失败");
                return null;
            }

            return al;
        }

        /// <summary>
        /// 日报明细查询
        /// </summary>
        /// <returns>成功返回1 失败返回-1</returns>
        protected int QueryDayReportDetail(DateTime dtDate,string deptCode,string nurseCell)
        {
            ArrayList alDetail = this.dayReportManager.GetDayReportDetailList(this.StatDate, this.StatDate.AddDays(1), deptCode, nurseCell);
            if (alDetail != null)
            {
 
            }

            return 1;
        }

        /// <summary>
        /// 由Fp内获取日报数据
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        protected FS.HISFC.Models.HealthRecord.InpatientDayReport GetDayReport(int rowIndex)
        {
            FS.HISFC.Models.HealthRecord.InpatientDayReport info = this.neuSpread1_Sheet1.Rows[rowIndex].Tag as FS.HISFC.Models.HealthRecord.InpatientDayReport;
            if (info == null)
            {
                return null;
            }

            info.ID = this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColDeptCode].Text;
            info.Name = this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColDeptName].Text;
            info.BedStand = NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColStarandNum].Text);
            info.BedAdd = NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColAddNum].Text);
            info.BedFree = NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColFreeNum].Text);
            info.BeginningNum = NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColBeginNum].Text);
            info.InNormal = NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColInNum].Text);
            info.InTransfer = NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColTransferIn].Text);
            info.OutTransfer = NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColTransferOut].Text);
            info.OutNormal = NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColOutNu].Text);
            info.EndNum = NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColEndNum].Text);

            return info;
        }

        /// <summary>
        /// 日报保存
        /// </summary>
        /// <returns></returns>
        protected int SaveDayReport()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.dayReportManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.HealthRecord.InpatientDayReport info = this.GetDayReport(i);

                int param = this.dayReportManager.UpdateInpatientDayReport(info);
                if (param == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("床位日报更新失败"));
                    return -1;
                }
                else if (param == 0)
                {
                    if (this.dayReportManager.InsertInpatientDayReport(info) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("床位日报更新失败"));
                        return -1;
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("保存成功");

            return 1;
        }

        private enum ColumnSet
        {
            ColDeptName,
            ColStarandNum,
            ColAddNum,
            ColFreeNum,
            ColBeginNum,
            ColInNum,
            ColTransferIn,
            ColTransferOut,
            ColOutNu,
            ColEndNum,
            ColDeptCode
        }
        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.PrintPage(30, 10, this);

        }
        protected override int OnPrint(object sender, object neuObject)
        {
            this.Print();
            return base.OnPrint(sender, neuObject);
        }
        /// <summary>
        /// 导出
        /// </summary>
        private void Export()
        {
            if (this.neuSpread1.Export() == 1)
            {
                MessageBox.Show(Language.Msg("导出成功"));
            }
        }
        public override int Export(object sender, object neuObject)
        {
            this.Export();

            return base.Export(sender, neuObject);
        }

        /// <summary>
        /// {2D052C32-3BA2-4302-B41B-97AC7C8810E7}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucDayReportManager_Load(object sender, EventArgs e)
        {
            //床位日报明细没有实现，先隐藏掉
            this.neuSpread1_Sheet2.Visible = false;
        }
    }

    #region {07448623-4975-4279-8C25-27CAE84EA4BA}

    /// <summary>
    /// 操作类型
    /// </summary>
    public enum OperateType
    {
        /// <summary>
        /// 科室
        /// </summary>
        Dept,

        /// <summary>
        /// 所有
        /// </summary>
        All
    }

    #endregion
}
