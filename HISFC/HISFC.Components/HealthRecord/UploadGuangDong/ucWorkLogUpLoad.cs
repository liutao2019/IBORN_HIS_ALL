using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.HealthRecord.UploadGuangDong
{
    /// <summary>
    /// [功能描述: 上传 医生门诊工作日志（tWorkLog） 急诊工作日志（不分科）（tEmergeLogNoKs） 专科门诊病人数（tSpecialLog）]<br></br>
    /// 东莞：sql ：  Report.DoctWordStat    常数：  tWorkLog  参数 0  常数：专家科室对照 CASEWORKLOGSPDOC   普通科室对照 ：CASETWORKLOG 
    /// [创 建 者:　成郁明]<br></br>
    /// [创建时间: 2011-07-22]<br></br>
    public partial class ucWorkLogUpLoad : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucWorkLogUpLoad()
        {
            InitializeComponent();
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
        }
        FS.HISFC.BizLogic.HealthRecord.Base baseMgr = new FS.HISFC.BizLogic.HealthRecord.Base();
         
        List<FS.FrameWork.Models.NeuObject> deptList = null;
        List<FS.FrameWork.Models.NeuObject> zkDeptList = null;
        List<FS.FrameWork.Models.NeuObject> docList = null;

        private bool isMut = true;

        /// <summary>
        /// 是否多院区
        /// </summary>
        [Category("多院区"), Description("是否多院区")]
        public bool IsMut
        {
            get { return this.isMut; }
            set { this.isMut = value; }
        }
        /// <summary>
        /// 查询上传的日志是否为单日的
        /// </summary>
        bool issingleday;

        System.Collections.ArrayList alUpload = null;
        DateTime begin;
        DateTime end;
        string Type = string.Empty;
        
        private FS.FrameWork.WinForms.Forms.ToolBarService tool = new FS.FrameWork.WinForms.Forms.ToolBarService();

        private void ucUpLoad_Load(object sender, EventArgs e)
        {
            FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface upLoadInterFace = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface();
            DateTime today = this.baseMgr.GetDateTimeFromSysDateTime();
            this.neuDateTimePickerFrom.Value = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);
            this.neuDateTimePickerTo.Value = new DateTime(today.Year, today.Month, today.Day, 23, 59, 59);
            this.neuSpread1_Sheet1.ColumnHeader.Rows[0].Height = 30f;
           
            //病案3.0的科室表内容 和 医生表内容，用于获取统一号；
            deptList = upLoadInterFace.GetDeptCodeByCode();//普通科室

            zkDeptList = upLoadInterFace.GetZkDeptCodeByCode();// 专科科室

            docList = upLoadInterFace.GetDocCodeByCode();//医生

        }

        /// <summary>
        /// 初始化，添加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit( object sender, object neuObject, object param )
        {
            this.tool.AddToolButton("医生门诊工作日志", "医生门诊工作日志",FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);
            this.tool.AddToolButton("急诊工作日志", "急诊工作日志", FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);
            this.tool.AddToolButton("专科门诊病人数", "专科门诊病人数", FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);
            this.tool.AddToolButton("临床路径统计", "临床路径统计", FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);
            this.tool.AddToolButton("住院病房日志", "住院病房日志", FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);

            this.tool.AddToolButton("上传", "上传", FS.FrameWork.WinForms.Classes.EnumImageList.J借出, true, false, null);

            return tool;
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            //MessageBox.Show ("aa");

            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;

            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            switch (Type)
            {
                case "1":
                    p.PrintPreview(250, 20, this.neuSpread1);
                    break;

                case "2":
                    p.PrintPreview(100,20,this.neuSpread1);
                    break;

                case "3":
                    p.PrintPreview(80,10,this.neuSpread1);
                    break;
            }
            //p.PrintPreview(this.neuPanel2);
            return base.OnPrint(sender, neuObject);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Export(object sender, object neuObject)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "(Excel文件)|*.xls";
            sf.FileName = a.Text;
            if (DialogResult.OK == sf.ShowDialog())
            {
                this.neuSpread1.SaveExcel(sf.FileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
            }
            return base.Export(sender, neuObject);
        }

        /// <summary>
        /// 自定义功能按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked( object sender, ToolStripItemClickedEventArgs e )
        {
            int days=0;
            switch (e.ClickedItem.Text)
            {
                case "医生门诊工作日志":
                    days=this.MutDateTime();
                    if (days == 1)//一天
                    {
                        Type = "tWorkLog";
                        this.SetMSpecial(Type);
                        this.a.Text = "医生门诊工作日志";
                    }
                    else
                    {
                        Type = "tWorkLog";
                        this.SetMutUpload(Type, days);
                        this.a.Text = "医生门诊工作日志";
                    }
                    break;
                case "急诊工作日志":
                    days=this.MutDateTime();
                    if (days == 1)//一天
                    {
                        Type = "tEmergeLog";
                        this.SetMSpecial(Type);
                        this.a.Text = "急诊工作日志";
                    }
                    else
                    {
                        Type = "tEmergeLog";
                        this.SetMutUpload(Type, days);
                        this.a.Text = "急诊工作日志";
                    }
                    break;
                case "专科门诊病人数":
                    days=this.MutDateTime();
                    if (days == 1)//一天
                    {
                        Type = "tSpecialLog";
                        this.SetMSpecial(Type);
                        this.a.Text = "专科门诊病人数";
                    }
                    else
                    {
                        Type = "tSpecialLog";
                        this.SetMutUpload(Type, days);
                        this.a.Text = "专科门诊病人数";
                    }
                    break;
                case "住院病房日志":
                    days=this.MutDateTime();
                    if (days == 1)//一天
                    {
                        Type = "TZyWardWorklog";
                        this.SetMSpecial(Type);
                        this.a.Text = "住院病房日志";
                    }
                    else //多天
                    {
                        Type = "TZyWardWorklog";
                        this.SetMutUpload(Type, days);
                        this.a.Text = "住院病房日志";
                    }
                    break;
                case "上传":
                    this.SetEntity();
                    break;
                case "临床路径统计":
                    Type = "clinicPath";
                    this.SetMSpecial(Type);
                    this.a.Text = "临床路径统计";
                    break;

            }
            base.ToolStrip_ItemClicked( sender, e );
        }
        /// <summary>
        /// 判断时间间隔
        /// 
        /// </summary>
        /// <returns></returns>
        private int MutDateTime()
        {
            int ret;
            TimeSpan ts = neuDateTimePickerTo.Value.Date - neuDateTimePickerFrom.Value.Date;
            if (ts.Days > 0)
            {
                ret = ts.Days +1;
                MessageBox.Show("选择时间跨度大于1天，界面将不显示数据，按时间段批量上传");
            }
            else if (ts.Days == 0)
            {
                 ret = 1;
            }
            else
            {
                MessageBox.Show("结束时间不能小于开始时间");
                ret = 0;
            }
            return ret;
        }
        System.Data.DataSet dsMSpecil = null;

        System.Data.DataView drView = null;

        /// <summary>
        /// 查询数据方法
        /// </summary>
        /// <param name="Type"></param>
        private void SetMSpecial(string Type)
        {
            FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface upLoadInterFace = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface();
            FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.WorklogFuntion hh = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.WorklogFuntion();
            dsMSpecil = new DataSet();
            this.neuSpread1_Sheet1.DataSource = "";
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.ClearColumnHeaderSpanCells();
            if (neuSpread1_Sheet1.RowCount > 0)
            {
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.Rows.Count);
            }
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询数据，请您耐心等待...");
            Application.DoEvents();
           
            //上传的时间只能在同一天
            if (neuDateTimePickerFrom.Value.Date == neuDateTimePickerTo.Value.Date)
            {
                issingleday = true;
            }
            else { issingleday = false; }

            //东莞多院区，使用静态函数获取数据吧
            string Sql = string.Empty;
            string execError = string.Empty;
//            if (isMut)
//            {
//                if (Type == "clinicPath")
//                {
//                    Sql = @"SELECT (SELECT hos_name FROM COM_HOSPITALINFO )  AS 院区名称 ,count(*)  AS 例数  FROM MET_CAS_BASE 
//                            WHERE OUT_DATE BETWEEN '{0}' AND '{1}'  AND ect_numb='1' ";
//                    Sql = string.Format(Sql, neuDateTimePickerFrom.Value.Date, neuDateTimePickerTo.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59));
//                }
//                else
//                {
//                    Sql = hh.GetWorkLogUploadSql(Type, neuDateTimePickerFrom.Value, neuDateTimePickerTo.Value);
//                }

//                if (Sql != string.Empty)
//                {
//                    FS.UFC.Common.Classes.Function.ExecSQL(Sql, ref execError, ref dsMSpecil);
//                }
//            }
//            else
//            {
                if (hh.GetMSpecial(ref dsMSpecil, Type, neuDateTimePickerFrom.Value, neuDateTimePickerTo.Value) == -1)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("获得MSpecial出错！" + hh.Err);
                    return;
                }
            //}
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            drView = new System.Data.DataView(dsMSpecil.Tables[0]);
            this.neuSpread1.DataSource = drView;
            this.setFP();

            //this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
        }

        /// <summary>
        /// 多天上传
        /// </summary>
        /// <param name="Type"></param>
        private void SetMutUpload(string Type, int days)
        {
            FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface upLoadInterFace = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface();
            FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.WorklogFuntion hh = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.WorklogFuntion();
            dsMSpecil = new DataSet();
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询数据，请您耐心等待...");
            Application.DoEvents();

            string Sql = string.Empty;
            string execError = string.Empty;

            DateTime dtBegin = neuDateTimePickerFrom.Value.Date;
            alUpload = new System.Collections.ArrayList();

            for (int day = 0; day < days; day++)
            {
                if (day > 0)
                {
                    dtBegin = dtBegin.AddDays(1);
                }
                if (hh.GetMSpecial(ref dsMSpecil, Type, dtBegin, dtBegin) == -1)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("获得MSpecial出错！" + hh.Err);
                    return;
                }
                if (dsMSpecil == null || dsMSpecil.Tables.Count == 0 || dsMSpecil.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("未获取到数据！" + hh.Err);
                    return;
                }
                FS.HISFC.Models.HealthRecord.Base b = new FS.HISFC.Models.HealthRecord.Base();
                switch (Type)
                {
                    case "tWorkLog":
                        foreach (DataRow rows in dsMSpecil.Tables[0].Rows)
                        {
                            b = new FS.HISFC.Models.HealthRecord.Base();
                            b.PatientInfo.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(rows[0].ToString());//日期
                            b.InDept.ID = rows[1].ToString();//统一科号
                            b.InDept.Name = rows[2].ToString();//科室名称
                            b.PatientInfo.DoctorReceiver.ID = rows[3].ToString();//统一医生工号
                            b.PatientInfo.DoctorReceiver.Name = rows[4].ToString();//医生姓名
                            b.PatientInfo.DoctorReceiver.Memo = rows[5].ToString();//医生职称
                            b.OutDept.ID = rows[6].ToString();//统一专科号
                            b.OutDept.Name = rows[7].ToString();//专科号
                            //b.OutDept.ID = "chengym";//统一专科号 测试用
                            b.PatientInfo.Memo = rows[8].ToString();//工时
                            b.PatientInfo.User01 = rows[9].ToString();//诊疗人次
                            b.PatientInfo.User02 = rows[10].ToString();//专科门诊
                            b.PatientInfo.User03 = rows[11].ToString();//专家门诊
                            b.PatientInfo.UserCode = rows[12].ToString();//健康检查
                            b.PatientInfo.WBCode = rows[13].ToString();//手术例数
                            b.PatientInfo.PVisit.User01 = rows[14].ToString();
                            b.PatientInfo.PVisit.User02 = rows[15].ToString();
                            b.PatientInfo.PVisit.OutTime = this.baseMgr.GetDateTimeFromSysDateTime();
                            alUpload.Add(b);
                        }
                        //MessageBox.Show("设置实体成功tWorkLog");
                        break;
                    case "tEmergeLog":
                        foreach (DataRow rows in dsMSpecil.Tables[0].Rows)
                        {
                            b = new FS.HISFC.Models.HealthRecord.Base();
                            b.PatientInfo.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(rows[0].ToString());//日期
                            b.InDept.ID = rows[1].ToString();//统一科号
                            b.InDept.Name = rows[2].ToString();//科室名称
                            b.PatientInfo.ID = rows[3].ToString();//医生人数
                            //b.PatientInfo.ID = "100000"; //测试用
                            b.PatientInfo.Memo = rows[4].ToString();//工时
                            b.PatientInfo.User01 = rows[5].ToString();//急诊人次
                            b.PatientInfo.User02 = rows[6].ToString();//死亡人数
                            b.PatientInfo.User03 = rows[7].ToString();//抢救人数
                            b.PatientInfo.UserCode = rows[8].ToString();//抢救成功人数
                            b.PatientInfo.WBCode = rows[9].ToString();//出车次数
                            b.PatientInfo.PVisit.User01 = rows[10].ToString();//手术例数
                            b.PatientInfo.PVisit.OutTime = this.baseMgr.GetDateTimeFromSysDateTime();//输入日期
                            alUpload.Add(b);
                        }
                        //MessageBox.Show("设置实体成功tEmergeLogNoKs");
                        break;
                    case "tSpecialLog":
                        foreach (DataRow rows in dsMSpecil.Tables[0].Rows)
                        {
                            b = new FS.HISFC.Models.HealthRecord.Base();

                            b.PatientInfo.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(rows[0].ToString());//日期
                            b.InDept.ID = rows[1].ToString();//统一科号
                            b.InDept.Name = rows[2].ToString();//科室名称
                            b.OutDept.ID = rows[3].ToString();//统一专科号
                            b.OutDept.Name = rows[4].ToString();//专科号
                            //b.OutDept.ID = "chengym";//统一专科号 测试用
                            b.PatientInfo.ID = rows[5].ToString();//诊疗人数
                            b.PatientInfo.PVisit.OutTime = this.baseMgr.GetDateTimeFromSysDateTime();//输入日期
                            alUpload.Add(b);
                        }
                        break;
                    case "TZyWardWorklog":
                        FS.HISFC.Models.HealthRecord.Case.PatientMove patientMove = null;

                        foreach (DataRow rows in dsMSpecil.Tables[0].Rows)
                        {
                            patientMove = new FS.HISFC.Models.HealthRecord.Case.PatientMove();
                            patientMove.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(rows[0].ToString());
                            patientMove.DeptCode = rows[1].ToString();
                            patientMove.OperDept = rows[2].ToString();
                            patientMove.BedNum = FS.FrameWork.Function.NConvert.ToInt32(rows[3].ToString());

                            patientMove.OriginalNum = FS.FrameWork.Function.NConvert.ToInt32(rows[5].ToString());
                            patientMove.InNum = FS.FrameWork.Function.NConvert.ToInt32(rows[6].ToString());
                            patientMove.OtherDeptIn = FS.FrameWork.Function.NConvert.ToInt32(rows[7].ToString());
                            patientMove.OtherRegionIn = FS.FrameWork.Function.NConvert.ToInt32(rows[8].ToString());
                            patientMove.OutNum = FS.FrameWork.Function.NConvert.ToInt32(rows[9].ToString());
                            patientMove.DeadNum = FS.FrameWork.Function.NConvert.ToInt32(rows[10].ToString());
                            patientMove.ToOtherDept = FS.FrameWork.Function.NConvert.ToInt32(rows[11].ToString());
                            patientMove.ToOtherRegion = FS.FrameWork.Function.NConvert.ToInt32(rows[12].ToString());
                            patientMove.PatientNum = FS.FrameWork.Function.NConvert.ToInt32(rows[13].ToString());
                            patientMove.AccompanyNum = FS.FrameWork.Function.NConvert.ToInt32(rows[14].ToString());
                            patientMove.BeduseNum = FS.FrameWork.Function.NConvert.ToInt32(rows[15].ToString());
                            patientMove.Memo = rows[16].ToString();
                            alUpload.Add(patientMove);
                        }
                        break;
                    default:
                        break;
                }
            }
            try
            {
                upLoadInterFace = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface();
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接失败!" + ex.Message);
                //return -1;
            }
            int deleteNum = 0;
            int intReturn = 0;
            bool isExist = false;
            try
            {
                switch (Type)
                {
                    case "tWorkLog":
                        intReturn = upLoadInterFace.InserttWorkLog(alUpload, begin.ToShortDateString(), end.ToShortDateString(), ref isExist);
                        break;
                    case "tEmergeLog":
                        intReturn = upLoadInterFace.InserttEmergeLog(alUpload, begin.ToShortDateString(), end.ToShortDateString(), ref isExist);
                        break;
                    case "tSpecialLog":
                        intReturn = upLoadInterFace.InserttSpecialLog(alUpload, begin.ToShortDateString(), end.ToShortDateString(), ref isExist);
                        break;
                    case "TZyWardWorklog":
                        intReturn = upLoadInterFace.InsertTZyWardWorklog(alUpload, begin.ToShortDateString(), end.ToShortDateString(), ref deleteNum);
                        break;
                }
                if (intReturn == -1)
                {
                    upLoadInterFace.Rollback();
                    MessageBox.Show(upLoadInterFace.Err + ",上传出错！");
                    return;
                }
                else
                {
                    if (isExist)
                    {
                        if (DialogResult.No == MessageBox.Show("已经有相关记录，要覆盖吗？", "警告",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1))
                        {
                            upLoadInterFace.Rollback();
                            MessageBox.Show("取消上传");
                            return;
                        }
                    }
                    upLoadInterFace.Commit();
                }
                MessageBox.Show("上传成功" + intReturn.ToString() + "条记录！");
            }
            catch (Exception ex)
            {
                upLoadInterFace.Rollback();
                MessageBox.Show(ex.Message);
                //return -1;
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }
        private void setFP()
        {
            switch (Type)
            {
                case "tWorkLog":
                    this.neuSpread1_Sheet1.Columns.Get(1).AllowAutoSort = true;
                    this.neuSpread1_Sheet1.Columns.Get(2).AllowAutoSort = true;
                    this.neuSpread1_Sheet1.Columns.Get(4).AllowAutoSort = true;
                    this.neuSpread1_Sheet1.Columns.Get(9).AllowAutoSort = true;

                    this.neuSpread1_Sheet1.Columns.Get(0).Width = 85;
                    this.neuSpread1_Sheet1.Columns.Get(1).Width = 50;
                    this.neuSpread1_Sheet1.Columns.Get(2).Width = 80;
                    this.neuSpread1_Sheet1.Columns.Get(3).Width = 50;
                    this.neuSpread1_Sheet1.Columns.Get(4).Width = 80;
                    this.neuSpread1_Sheet1.Columns.Get(5).Width = 40;
                    this.neuSpread1_Sheet1.Columns.Get(6).Width = 40;
                    this.neuSpread1_Sheet1.Columns.Get(7).Width = 80;
                    this.neuSpread1_Sheet1.Columns.Get(8).Width = 60;
                    this.neuSpread1_Sheet1.Columns.Get(9).Width = 70;
                    this.neuSpread1_Sheet1.Columns.Get(10).Width = 30;
                    this.neuSpread1_Sheet1.Columns.Get(11).Width = 30;
                    this.neuSpread1_Sheet1.Columns.Get(12).Width = 30;
                    this.neuSpread1_Sheet1.Columns.Get(13).Width = 30;
                    this.neuSpread1_Sheet1.Columns.Get(14).Width = 30;
                    this.neuSpread1_Sheet1.Columns.Get(15).Width = 80;
                    break;
                case "tEmergeLogNoKs":

                    this.neuSpread1_Sheet1.Columns.Get(0).Width = 85;
                    this.neuSpread1_Sheet1.Columns.Get(1).Width = 50;
                    this.neuSpread1_Sheet1.Columns.Get(2).Width = 80;
                    this.neuSpread1_Sheet1.Columns.Get(3).Width = 50;
                    this.neuSpread1_Sheet1.Columns.Get(4).Width = 80;
                    this.neuSpread1_Sheet1.Columns.Get(5).Width = 40;
                    this.neuSpread1_Sheet1.Columns.Get(6).Width = 40;
                    this.neuSpread1_Sheet1.Columns.Get(7).Width = 80;
                    this.neuSpread1_Sheet1.Columns.Get(8).Width = 60;
                    this.neuSpread1_Sheet1.Columns.Get(9).Width = 100;

                    break;
                case "tSpecialLog":

                    this.neuSpread1_Sheet1.Columns.Get(0).Width = 85;
                    this.neuSpread1_Sheet1.Columns.Get(1).Width = 80;
                    this.neuSpread1_Sheet1.Columns.Get(2).Width = 80;
                    this.neuSpread1_Sheet1.Columns.Get(3).Width = 80;
                    this.neuSpread1_Sheet1.Columns.Get(4).Width = 60;
                    this.neuSpread1_Sheet1.Columns.Get(5).Width = 60;
                    this.neuSpread1_Sheet1.Columns.Get(6).Width = 85;

                    break;
            
            }

        }

        /// <summary>
        /// 上传方法
        /// </summary>
        private void SetEntity()
        {
            FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface upLoadInterFace = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface();
            FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.WorklogFuntion hh = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.WorklogFuntion();
            begin = this.neuDateTimePickerFrom.Value;
            end = this.neuDateTimePickerTo.Value;
            if (!issingleday)
            {
                MessageBox.Show("本次上传的日志超过1天，请重新选择日期查询，再尝试上传！", "警告");
                return;
                //if (MessageBox.Show("本次上传的日志超过1天，将造成数据重复，是否继续上传？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                //{
                //    return;
                //}
            }
            alUpload = new System.Collections.ArrayList();
            FS.HISFC.Models.HealthRecord.Base b = new FS.HISFC.Models.HealthRecord.Base(); 
            switch (Type)
            {
                case "tWorkLog":
                    for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                    {
                        b = new FS.HISFC.Models.HealthRecord.Base();
                        b.PatientInfo.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.Cells[i, 0].Text);//日期
                        foreach(FS.FrameWork.Models.NeuObject obj in deptList)
                        {
                            if (obj.Memo == this.neuSpread1_Sheet1.Cells[i, 1].Text.Trim())
                            {
                                b.InDept.ID = obj.ID;//统一科号
                                b.InDept.Name = obj.Name;//科室名称
                                break;
                            }
                        }
                        foreach (FS.FrameWork.Models.NeuObject info in docList)
                        {
                            if (info.Memo == this.neuSpread1_Sheet1.Cells[i, 3].Text.Trim())
                            {
                                b.PatientInfo.DoctorReceiver.ID = info.ID;//统一医生工号
                                b.PatientInfo.DoctorReceiver.Name = info.Name;//医生姓名
                                break;
                            }
                        }
                        b.PatientInfo.DoctorReceiver.Memo = this.neuSpread1_Sheet1.Cells[i, 5].Text;//医生职称
                        if (this.neuSpread1_Sheet1.Cells[i, 6].Text.Trim() == "")
                        {
                            b.OutDept.ID = "";//统一专科号
                            b.OutDept.Name = "";//专科号
                        }
                        else
                        {
                            foreach (FS.FrameWork.Models.NeuObject objInfo in zkDeptList)
                            {
                                if (objInfo.Memo == this.neuSpread1_Sheet1.Cells[i, 6].Text.Trim())
                                {
                                    b.OutDept.ID = objInfo.ID;//统一专科号
                                    b.OutDept.Name = objInfo.Name;//专科号
                                    break;
                                }
                            }
                        }
                        //b.OutDept.ID = "chengym";//统一专科号 测试用
                        b.PatientInfo.Memo = this.neuSpread1_Sheet1.Cells[i, 8].Text;//工时
                        b.PatientInfo.User01 = this.neuSpread1_Sheet1.Cells[i, 9].Text;//诊疗人次
                        b.PatientInfo.User02 = this.neuSpread1_Sheet1.Cells[i, 10].Text;//专科门诊
                        b.PatientInfo.User03 = this.neuSpread1_Sheet1.Cells[i, 11].Text;//专家门诊
                        b.PatientInfo.UserCode = this.neuSpread1_Sheet1.Cells[i, 12].Text;//健康检查
                        b.PatientInfo.WBCode = this.neuSpread1_Sheet1.Cells[i, 13].Text;//手术例数
                        b.PatientInfo.PVisit.User01 = this.neuSpread1_Sheet1.Cells[i, 14].Text;
                        b.PatientInfo.PVisit.User02 = this.neuSpread1_Sheet1.Cells[i, 15].Text;
                        b.PatientInfo.PVisit.OutTime = this.baseMgr.GetDateTimeFromSysDateTime();
                        alUpload.Add(b);
                    }
                    //MessageBox.Show("设置实体成功tWorkLog");
                    break;
                case "tEmergeLog":
                    for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                    {
                        b = new FS.HISFC.Models.HealthRecord.Base();
                        b.PatientInfo.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.Cells[i, 0].Text);//日期
                        b.InDept.ID = this.neuSpread1_Sheet1.Cells[i, 1].Text;//统一科号
                        b.InDept.Name = this.neuSpread1_Sheet1.Cells[i, 2].Text;//科室名称
                        b.PatientInfo.ID = this.neuSpread1_Sheet1.Cells[i, 3].Text;//医生人数
                        //b.PatientInfo.ID = "100000"; //测试用
                        b.PatientInfo.Memo = this.neuSpread1_Sheet1.Cells[i, 4].Text;//工时
                        b.PatientInfo.User01 = this.neuSpread1_Sheet1.Cells[i, 5].Text;//急诊人次
                        b.PatientInfo.User02 = this.neuSpread1_Sheet1.Cells[i, 6].Text;//死亡人数
                        b.PatientInfo.User03 = this.neuSpread1_Sheet1.Cells[i, 7].Text;//抢救人数
                        b.PatientInfo.UserCode = this.neuSpread1_Sheet1.Cells[i, 8].Text;//抢救成功人数
                        b.PatientInfo.WBCode = this.neuSpread1_Sheet1.Cells[i, 9].Text;//出车次数
                        b.PatientInfo.PVisit.User01 = this.neuSpread1_Sheet1.Cells[i, 10].Text;//手术例数
                        b.PatientInfo.PVisit.OutTime = this.baseMgr.GetDateTimeFromSysDateTime();//输入日期
                        alUpload.Add(b);
                    }
                    //MessageBox.Show("设置实体成功tEmergeLogNoKs");
                    break;
                case "tSpecialLog":
                    for (int i = 0; i < this.neuSpread1_Sheet1.RowCount;i++ )
                    {
                        b = new FS.HISFC.Models.HealthRecord.Base();

                        b.PatientInfo.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.Cells[i, 0].Text);//日期
                        foreach (FS.FrameWork.Models.NeuObject obj in deptList)
                        {
                            if (obj.Memo == this.neuSpread1_Sheet1.Cells[i, 1].Text.Trim())
                            {
                                b.InDept.ID = obj.ID;//统一科号
                                b.InDept.Name = obj.Name;//科室名称
                                break;
                            }
                        }
                        if (this.neuSpread1_Sheet1.Cells[i, 3].Text.Trim() == "")
                        {
                            b.OutDept.ID = "";//统一专科号
                            b.OutDept.Name = "";//专科号
                        }
                        else
                        {
                            foreach (FS.FrameWork.Models.NeuObject objInfo in zkDeptList)
                            {
                                if (objInfo.Memo == this.neuSpread1_Sheet1.Cells[i, 3].Text.Trim())
                                {
                                    b.OutDept.ID = objInfo.ID;//统一专科号
                                    b.OutDept.Name = objInfo.Name;//专科号
                                    break;
                                }
                            }
                        }
                        //b.OutDept.ID = "chengym";//统一专科号 测试用
                        b.PatientInfo.ID = this.neuSpread1_Sheet1.Cells[i, 5].Text;//诊疗人数
                        b.PatientInfo.PVisit.OutTime = this.baseMgr.GetDateTimeFromSysDateTime();//输入日期
                        alUpload.Add(b);
                    }
                    //MessageBox.Show( "设置实体成功tSpecialLog" );
                    break;
                case "TZyWardWorklog":
                    FS.HISFC.Models.HealthRecord.Case.PatientMove patientMove = null;
                    for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                    {
                        patientMove = new FS.HISFC.Models.HealthRecord.Case.PatientMove();
                        patientMove.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.Cells[i, 0].Text);
                        patientMove.DeptCode = this.neuSpread1_Sheet1.Cells[i, 1].Text;
                        patientMove.OperDept = this.neuSpread1_Sheet1.Cells[i, 2].Text;
                        patientMove.BedNum = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 3].Text);

                        patientMove.OriginalNum = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 5].Text);
                        patientMove.InNum = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 6].Text);
                        patientMove.OtherDeptIn = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 7].Text);
                        patientMove.OtherRegionIn = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 8].Text);
                        patientMove.OutNum = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 9].Text);
                        patientMove.DeadNum = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 10].Text);
                        patientMove.ToOtherDept = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 11].Text);
                        patientMove.ToOtherRegion = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 12].Text);
                        patientMove.PatientNum = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 13].Text);
                        patientMove.AccompanyNum = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 14].Text);
                        patientMove.BeduseNum = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 15].Text);
                        patientMove.Memo = this.neuSpread1_Sheet1.Cells[i, 16].Text;
                        alUpload.Add(patientMove);
                    }
                    break;
                default:
                    break;
            }

            try
            {
                upLoadInterFace = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface();
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接失败!" + ex.Message);
                //return -1;
            }
            int deleteNum = 0;
            int intReturn=0;
            bool isExist = false;
            try
            {
                switch (Type)
                {
                    case "tWorkLog":
                        intReturn = upLoadInterFace.InserttWorkLog(alUpload, begin.ToShortDateString(), end.ToShortDateString(), ref isExist);
                        break;
                    case "tEmergeLog":
                        intReturn = upLoadInterFace.InserttEmergeLog(alUpload, begin.ToShortDateString(), end.ToShortDateString(), ref isExist);
                        break;
                    case "tSpecialLog":
                        intReturn = upLoadInterFace.InserttSpecialLog(alUpload, begin.ToShortDateString(), end.ToShortDateString(), ref isExist);
                        break;
                    case "TZyWardWorklog":
                        intReturn = upLoadInterFace.InsertTZyWardWorklog(alUpload, begin.ToShortDateString(), end.ToShortDateString(), ref deleteNum);
                        break;
                }
                if (intReturn == -1)
                {
                    upLoadInterFace.Rollback();
                    MessageBox.Show(upLoadInterFace.Err + ",上传出错！");
                    return ;
                }
                else
                {
                    if (isExist)
                    {
                        if (DialogResult.No == MessageBox.Show("已经有相关记录，要覆盖吗？", "警告",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1))
                        {
                            upLoadInterFace.Rollback();
                            MessageBox.Show("取消上传");
                            return ;
                        }
                    }
                    upLoadInterFace.Commit();
                }

                //if (intReturn<upLoadInterFace.Count )
                //{
                //    upLoadInterFace.Rollback();
                //}
                //else
                //{
                //    upLoadInterFace.Commit();
                //    
                //}
                MessageBox.Show("上传成功" + intReturn.ToString() + "条记录！");
                //return intReturn;
            }
            catch (Exception ex)
            {
                upLoadInterFace.Rollback();
                MessageBox.Show(ex.Message);
                //return -1;
            }
            //MessageBox.Show("haolema?"+intReturn.ToString());
        }        

    }
}
