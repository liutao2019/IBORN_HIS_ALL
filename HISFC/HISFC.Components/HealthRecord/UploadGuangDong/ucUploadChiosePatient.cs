using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.HealthRecord.UploadGuangDong
{
    public partial class ucUploadChiosePatient : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucUploadChiosePatient()
        {
            InitializeComponent();
        }
        FS.HISFC.BizLogic.HealthRecord.Base baseMana = new FS.HISFC.BizLogic.HealthRecord.Base();

        FS.HISFC.BizLogic.HealthRecord.DeptShift deptShiftMana = new FS.HISFC.BizLogic.HealthRecord.DeptShift();
        FS.HISFC.BizLogic.HealthRecord.Fee healthfeeMana = new FS.HISFC.BizLogic.HealthRecord.Fee();
        FS.HISFC.BizLogic.HealthRecord.Tumour tumourMana = new FS.HISFC.BizLogic.HealthRecord.Tumour();
        FS.HISFC.BizLogic.HealthRecord.Operation operMana = new FS.HISFC.BizLogic.HealthRecord.Operation();
        FS.HISFC.BizLogic.HealthRecord.Diagnose diagnoseMana = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

        FS.HISFC.BizProcess.Integrate.RADT pa = new FS.HISFC.BizProcess.Integrate.RADT();
        FS.HISFC.BizLogic.HealthRecord.Base baseDml = new FS.HISFC.BizLogic.HealthRecord.Base();

        FS.HISFC.BizLogic.Manager.Constant constMana = new FS.HISFC.BizLogic.Manager.Constant();
        /// <summary>
        /// 错误
        /// </summary>
        string err = "";
        /// <summary>
        /// 已录入，不需要上传的
        /// </summary>
        string errNotNeed = "";

        /// <summary>
        /// SqlID
        /// </summary>
        string sqlID = "Case.CaseInfo.UpLoad";

        /// <summary>
        /// SqlID
        /// </summary>
        [Description("Sql语句ID号"), Category("查询设置")]
        public string SqlID
        {
            get
            {
                return sqlID;
            }
            set
            {
                sqlID = value;
            }
        }

        private bool isAllDeptUsedCaseBase = false;
        /// <summary>
        /// 所有科室都使用了病案首页上传首页内容（使资料全面）
        /// </summary>
        [Description("true 是取sqlid“Case.CaseInfo.UpLoad.MetCasBase”  false 非取sqlid“Case.CaseInfo.UpLoad” "), Category("查询设置")]
        public bool IsAllDeptUsedCaseBase
        {
            get
            {
                return this.isAllDeptUsedCaseBase;
            }
            set
            {
                this.isAllDeptUsedCaseBase = value;
            }
        }

        private bool isNewEmr = false;
        /// <summary>
        ///使用最新点子病历（病案首页用电子病历形式实现）
        /// </summary>
        [Description("ture 最新电子病历实现病案首页 false uc实现 "), Category("查询设置")]
        public bool IsNewEmr
        {
            get
            {
                return this.isNewEmr;
            }
            set
            {
                this.isNewEmr = value;
            }
        }

        private bool isDrgs = false;
        /// <summary>
        ///使用最新点子病历（病案首页用电子病历形式实现）
        /// </summary>
        [Description("ture 最新首页（drgs）上传 false 2012年以前版本上传 "), Category("查询设置")]
        public bool IsDrgs
        {
            get
            {
                return this.isDrgs;
            }
            set
            {
                this.isDrgs = value;
            }
        }

        /// <summary>
        /// 病案主表数据 true 是 false 非
        /// </summary>
        private bool isMetCasBase = true;
        /// <summary>
        /// 不需要上传数据的住院次数
        /// </summary>
        private int inTimes = 0;
        /// <summary>
        /// 上传数据的住院次数
        /// </summary>
        private int uploadInTimes = 0;
        /// <summary>
        /// 工具bar
        /// </summary>
        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        
        /// <summary>
        /// 初始化工具栏按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("测试连接", "测试连接", FS.FrameWork.WinForms.Classes.EnumImageList.S设置, true, false, null);
            toolBarService.AddToolButton("上传", "上传", FS.FrameWork.WinForms.Classes.EnumImageList.D导出, true, false, null);
            toolBarService.AddToolButton("全选", "全选", FS.FrameWork.WinForms.Classes.EnumImageList.Q全选, true, false, null);
            toolBarService.AddToolButton("全不选", "全不选", FS.FrameWork.WinForms.Classes.EnumImageList.Q全不选, true, false, null);
            toolBarService.AddToolButton("未上传", "未上传", FS.FrameWork.WinForms.Classes.EnumImageList.G过滤, true, false, null);
            toolBarService.AddToolButton("更新住院次数", "更新住院次数", FS.FrameWork.WinForms.Classes.EnumImageList.G过滤, true, false, null);
            toolBarService.AddToolButton("更新住院次数2", "更新住院次数2", FS.FrameWork.WinForms.Classes.EnumImageList.G过滤, true, false, null);
            
            return toolBarService;
        }
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "测试连接":
                    ucSetConnectSqlServer uc = new ucSetConnectSqlServer();
                    this.ShowControl(uc);
                    break;
                case "上传":
                    this.Save();
                    break;
                case "全选":
                    this.SetCheck(true);
                    break;
                case "全不选":
                    this.SetCheck(false);
                    break;
                case "未上传":
                    this.UnUpload();
                    break;
                case "更新住院次数":
                    this.UpdateTimes();
                    break;

                case "更新住院次数2":
                    this.UpdateTimes2();
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            return this.Save();          
        }

        public override int Export(object sender, object neuObject)
        {
            Export();
            return base.Export(sender, neuObject);
        }
        /// <summary>
        /// 导出数据 
        /// </summary>
        private void Export()
        {
            bool ret = false;
            //导出数据
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "Excel|.xls";
                saveFileDialog1.FileName = "";

                saveFileDialog1.Title = "导出数据";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    //以Excel 的形式导出数据
                    ret = neuFpEnter1.SaveExcel(saveFileDialog1.FileName);
                    if (ret)
                    {
                        MessageBox.Show("导出成功！");
                    }
                }
            }
            catch (Exception ex)
            {
                //出错了
                MessageBox.Show(ex.Message);
            }
        }
        public void UpdateTimes()
        {
            DataTable dt = Function.Getdate();
            FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface uploadMana = null;
            try
            {
                uploadMana = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface();
            }
            catch
            { }
            foreach (DataRow dr in dt.Rows)
            {
                int result = uploadMana.GetBATimes(dr[0].ToString(), dr[1].ToString(), dr[2].ToString());

                Function.UpdateTimes(dr[0].ToString(), result.ToString());

            }
            

 
        }

        public void UpdateTimes2()
        {
            DataTable dt = Function.Getdate2();
           
            foreach (DataRow dr in dt.Rows)
            {
                string  result = Function.gettimes2(dr[1].ToString(), dr[2].ToString());

                Function.UpdateTimes(dr[0].ToString(), result.ToString());

            }



        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int Save()
        {
            if (this.neuFpEnter1_Sheet1.RowCount < 1)
            {
                MessageBox.Show("没有要上传的数据，请单击【查询】按钮，查询到数据再上传！","提示!");
                return -1;
            }
            int uploadNum = 0;
            for (int row = 0; row < this.neuFpEnter1_Sheet1.RowCount; row++)
            {
                if (this.neuFpEnter1_Sheet1.Cells[row, 0].Text == "True")
                {
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("请稍候！");
                    Application.DoEvents();
                    FS.HISFC.Models.HealthRecord.Base caseInfo = this.neuFpEnter1_Sheet1.Rows[row].Tag as FS.HISFC.Models.HealthRecord.Base;
                    int intReturn = Upload(caseInfo);
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    if (intReturn == -1)
                    {
                        this.neuFpEnter1_Sheet1.Cells[row, 7].Text = "上传失败";
                    }
                    else if (intReturn == 0)
                    {
                        this.neuFpEnter1_Sheet1.Cells[row, 7].Text = "已录入，不需要上传";
                    }
                    else if (intReturn == -2)
                    {
                        this.neuFpEnter1_Sheet1.Cells[row, 7].Text = "连接失败，请检查病案服务器是否关闭";
                    }
                    else if (intReturn == -3)
                    {
                        this.neuFpEnter1_Sheet1.Cells[row, 7].Text = "该患者有未上传数据，请先上传之前出院的信息";
                    }
                    else
                    {
                        uploadNum++;
                        this.neuFpEnter1_Sheet1.Cells[row, 7].Text = "已上传";
                        try
                        {
                            this.UpdateUploadStat(caseInfo.PatientInfo.ID,this.uploadInTimes);
                        }
                        catch
                        {

                        }
                    }
                }
            }
            this.lblSucceedUploadNum.Text = "已经上传" + uploadNum.ToString() + "条数据";
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show("上传完成！");
            return 0;
        }
        /// <summary>
        /// 选择
        /// </summary>
        /// <param name="isCheck">true 全选 false 不全选</param>
        private void SetCheck(bool isCheck)
        {
            for (int row = 0; row < this.neuFpEnter1_Sheet1.RowCount; row++)
            {
                if (isCheck)
                {
                    this.neuFpEnter1_Sheet1.Cells[row, 0].Text = "True";
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[row, 0].Text = "False";
                }
            }
        }
        /// <summary>
        /// 未上传
        /// </summary>
        private void UnUpload()
        {
            for (int row = 0; row < this.neuFpEnter1_Sheet1.RowCount; row++)
            {
                if (this.neuFpEnter1_Sheet1.Cells[row, 7].Text == "已上传")
                {
                    this.neuFpEnter1_Sheet1.Cells[row, 0].Text = "False";
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[row, 0].Text = "True";
                }
            }
        }
        /// <summary>
        /// 获取患者信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.cbCaseBase.Checked)
            {
                this.isAllDeptUsedCaseBase = true;
            }
            else
            {
                this.isAllDeptUsedCaseBase = false;
            }
            ArrayList PatientList=this.GetUploadInpatientNo("0",this.dtBegin.Value.Date,this.dtEnd.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59));
            if (PatientList == null)
            {
                return -1;
            }
            if (PatientList.Count == 0)
            {
                MessageBox.Show("没有采集到有效的出院病案首页记录！");
                return -1;
            }
            this.neuFpEnter1_Sheet1.RowCount = 0;
            foreach (FS.HISFC.Models.RADT.PatientInfo info in PatientList)
            {
                FS.HISFC.Models.HealthRecord.Base caseInfo= this.GetpatientInfo(info.ID);
                this.SetFarPoint(caseInfo);
            }
            return base.OnQuery(sender, neuObject);
        }

        /// <summary>
        /// 获取需要上传的数据
        /// </summary>
        /// <param name="sendFlag"></param>
        /// <param name="beginTime">出院开始日期</param>
        /// <param name="endTime">结束日期</param>
        /// <returns></returns>
        private ArrayList GetUploadInpatientNo(string sendFlag, DateTime beginTime, DateTime endTime)
        {
            if (this.cbInputPatientNo.Checked == true) //增加一个输入住院号收集数据上传的功能 2011-4-26 chengym
            {
                string patientNo = this.txtPatientNo.Text;
                ArrayList pTrueAl = new ArrayList();
                string[] temp = patientNo.Split(',');
                if (temp.Length > 0)
                {
                    string pEnd = "";
                    for (int i = 0; i < temp.Length; i++)
                    {
                        ArrayList alTemp = new ArrayList();
                        pEnd = temp[i].PadLeft(10, '0');//居然存在住院流水号跟住院号不同的情况，所以这里改回住院号查询
                        FS.HISFC.BizProcess.Integrate.RADT pMgr = new FS.HISFC.BizProcess.Integrate.RADT();
                        alTemp = pMgr.QueryInpatientNoByPatientNo(pEnd);
                        if (alTemp.Count > 0)
                        {
                            foreach (FS.FrameWork.Models.NeuObject p in alTemp)//全部加载进去，上传的时候会通过接口进行判断上传哪一个
                            {
                                FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                                patientInfo.ID = p.ID;
                                patientInfo.PID.PatientNO = pEnd;
                                patientInfo.Name = p.Name;
                                if (p.Memo != "O" && p.Memo != "B")
                                {
                                    continue;
                                }
                                pTrueAl.Add(patientInfo);
                            }
                        }
                    }

                }
                return pTrueAl;
            }
            else //原来默认按照时间段上传的
            {
                string strSQL = "";
                if (this.isAllDeptUsedCaseBase)
                {
                    sqlID = "Case.CaseInfo.UpLoad.MetCasBase";
                    if (this.baseMana.Sql.GetSql(sqlID, ref strSQL) == -1)
                    {
                        MessageBox.Show("获取SQL语句出错[" + sqlID + "]");
                        return null;
                    }
                }
                else
                {
                    if (this.baseMana.Sql.GetSql(sqlID, ref strSQL) == -1)
                    {
                        MessageBox.Show("获取SQL语句出错[" + sqlID + "]");
                        return null;
                    }
                }
                try
                {
                    strSQL = string.Format(strSQL, sendFlag, beginTime.ToString(), endTime.ToString());
                }
                catch
                {
                    return null;
                }

                this.baseMana.ExecQuery(strSQL);
                ArrayList al = new ArrayList();
                FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

                try
                {
                    while (this.baseMana.Reader.Read())
                    {
                        patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

                        patientInfo.ID = this.baseMana.Reader[0].ToString();
                        patientInfo.PID.PatientNO = this.baseMana.Reader[1].ToString();
                        patientInfo.User01 = this.baseMana.Reader[2].ToString().PadLeft(2, '0');

                        al.Add(patientInfo);
                    }
                    return al;
                }
                catch
                {
                    return null;
                }
                finally
                {
                    this.baseMana.Reader.Close();
                }
            }
        }
        /// <summary>
        /// 取住院信息
        /// </summary>
        /// <param name="inpatientNo">住院流水号</param>
        /// <returns></returns>
        private FS.HISFC.Models.HealthRecord.Base GetpatientInfo(string inpatientNo)
        {

            FS.HISFC.Models.HealthRecord.Base patientinfo = new FS.HISFC.Models.HealthRecord.Base();
            FS.HISFC.Models.HealthRecord.Base pat = new FS.HISFC.Models.HealthRecord.Base();
            FS.HISFC.Models.HealthRecord.Base pati = new FS.HISFC.Models.HealthRecord.Base();
            patientinfo = this.baseDml.GetCaseBaseInfo(inpatientNo); //2

            if (patientinfo == null || patientinfo.PatientInfo.ID == "") //met_cas_base 获取不到 从 fin_ipr_inmaininfo 里获取
            {
                patientinfo = new FS.HISFC.Models.HealthRecord.Base();
                patientinfo.PatientInfo = this.pa.QueryPatientInfoByInpatientNO(inpatientNo); //1
                string uploadState = this.baseDml.GetCaseUploadState(inpatientNo);
                if (uploadState == "1")
                {
                    patientinfo.UploadStatu = "1";
                }
                else
                {
                    patientinfo.UploadStatu = "0";
                }
                patientinfo.OutDept.Name = patientinfo.PatientInfo.PVisit.PatientLocation.Dept.Name;
                patientinfo.PatientInfo.PVisit.TemporaryLocation.Memo = "0";
                this.isMetCasBase = false;
            }
            else
            {
                patientinfo.PatientInfo.PVisit.TemporaryLocation.Memo = "1";
                this.isMetCasBase = true;
            }
            if (patientinfo.PatientInfo == null || patientinfo.PatientInfo.ID == "")
            {
                return null;
            }
            patientinfo.PatientInfo.Age = "0";

            return patientinfo;
        }
        /// <summary>
        /// Farpiont赋值
        /// </summary>
        /// <param name="caseInfo">患者病案首页实体</param>
        private void SetFarPoint(FS.HISFC.Models.HealthRecord.Base caseInfo)
        {
            this.neuFpEnter1_Sheet1.Rows.Add(this.neuFpEnter1_Sheet1.RowCount, 1);
            int row= this.neuFpEnter1_Sheet1.RowCount -1 ;
            this.neuFpEnter1_Sheet1.Rows[row].Tag = caseInfo;
            this.neuFpEnter1_Sheet1.Cells[row, 0].Text = "True";
            this.neuFpEnter1_Sheet1.Cells[row, 1].Text = caseInfo.PatientInfo.PID.PatientNO;
            this.neuFpEnter1_Sheet1.Cells[row, 2].Text = caseInfo.PatientInfo.Name;
            //增加入院科室
            this.neuFpEnter1_Sheet1.Cells[row, 3].Text = caseInfo.InDept.Name;
            this.neuFpEnter1_Sheet1.Cells[row, 4].Text = caseInfo.OutDept.Name;
            this.neuFpEnter1_Sheet1.Cells[row, 5].Text = caseInfo.PatientInfo.PVisit.OutTime.ToShortDateString();
            //增加管床医生
            this.neuFpEnter1_Sheet1.Cells[row, 6].Text = caseInfo.PatientInfo.PVisit.AdmittingDoctor.Name;

            if (caseInfo.UploadStatu == "1")
            {
                this.neuFpEnter1_Sheet1.Cells[row, 7].Text = "已上传";
            }
            else
            {
                this.neuFpEnter1_Sheet1.Cells[row, 7].Text = "未上传";
            }
            if (this.isMetCasBase)
            {
                this.neuFpEnter1_Sheet1.Rows[row].BackColor = System.Drawing.Color.LightCyan;
            }
        }
        /// <summary>
        /// 单个患者上传
        /// </summary>
        /// <param name="caseInfo"></param>
        /// <returns></returns>
        public int Upload(FS.HISFC.Models.HealthRecord.Base caseInfo)
        {
            if (caseInfo.PatientInfo.PVisit.TemporaryLocation.Memo == "0")
            {
                this.isMetCasBase = false;
            }
            else
            {
                this.isMetCasBase = true;
            }
            if (!isDrgs)
            {
                #region 传统uc病案首页上传
                FS.HISFC.BizProcess.Integrate.RADT dgInpatient = new FS.HISFC.BizProcess.Integrate.RADT();
                this.inTimes = caseInfo.PatientInfo.InTimes;

                //出生地为空 则赋籍贯值  
                if (caseInfo.PatientInfo.AreaCode != null && caseInfo.PatientInfo.AreaCode.Trim().Equals(""))
                {
                    caseInfo.PatientInfo.AreaCode = caseInfo.PatientInfo.DIST;
                }
                //转科
                ArrayList alChangeDept = new ArrayList();
                if (isMetCasBase)
                {
                    alChangeDept = deptShiftMana.QueryChangeDeptFromShiftApply(caseInfo.PatientInfo.ID, "2");
                }
                else
                {
                    alChangeDept = deptShiftMana.QueryChangeDeptFromShiftApply(caseInfo.PatientInfo.ID, "1");
                }
                if (alChangeDept == null)
                {
                    return -1;
                }

                //费用
                ArrayList alFee = new ArrayList();
                //if (isMetCasBase)
                //{
                //    alFee = healthfeeMana.QueryCaseFeeState(inpatientNo);
                //}
                //else
                //{
                alFee = healthfeeMana.QueryFeeInfoState(caseInfo.PatientInfo.ID);
                //}
                if (alFee == null)
                {
                    return -1;
                }


                #region 获取 婴儿信息、手术信息、诊断信息
                ArrayList babyAl = new ArrayList();
                ArrayList opAl = new ArrayList();
                ArrayList opAlReal = new ArrayList();
                ArrayList alDiagnose = new ArrayList();
                FS.HISFC.Models.HealthRecord.Tumour TumourInfo = new FS.HISFC.Models.HealthRecord.Tumour();
                ArrayList tumourDetailAl = new ArrayList();
                if (isMetCasBase)
                {
                    FS.HISFC.BizLogic.HealthRecord.Baby babyMgr = new FS.HISFC.BizLogic.HealthRecord.Baby();
                    babyAl = babyMgr.QueryBabyByInpatientNo(caseInfo.PatientInfo.ID);
                    caseInfo.PatientInfo.User03 = babyAl.Count.ToString();//婴儿数量 

                    FS.HISFC.BizLogic.HealthRecord.Operation opMgr = new FS.HISFC.BizLogic.HealthRecord.Operation();
                    opAl = opMgr.QueryOperationByInpatientNo(caseInfo.PatientInfo.ID);

                    foreach (FS.HISFC.Models.HealthRecord.OperationDetail opInfo in opAl)  //防止一些错误数据 有空需要从录入处更改
                    {
                        if (opInfo.OperationDate.Year.ToString().Trim() == "1" || opInfo.OperationInfo.Name.Trim() == "无")
                        {
                            continue;
                        }
                        opAlReal.Add(opInfo);
                    }

                    alDiagnose = this.baseDml.QueryCaseDiagnoseByInpatientNo(caseInfo.PatientInfo.ID);

                    FS.HISFC.BizLogic.HealthRecord.Tumour tumourMgr = new FS.HISFC.BizLogic.HealthRecord.Tumour();
                    TumourInfo = tumourMgr.GetTumour(caseInfo.PatientInfo.ID);
                    tumourDetailAl = tumourMgr.QueryTumourDetail(caseInfo.PatientInfo.ID);
                }
                #endregion

                FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface uploadMana = null;
                try
                {
                    uploadMana = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface();
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    dgInpatient.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                }
                catch (Exception ex)
                {
                    try
                    {
                        uploadMana.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                    }
                    catch
                    {
                    }
                    MessageBox.Show("连接失败!" + ex.Message);

                    return -2;
                }

                try
                {
                    //begin 2012-1-21 判断在出院日期前是否存在未上传的数据 chengym 避免上传的住院次数翻转
                    if (!cbInputPatientNo.Checked)
                    {
                        ArrayList isHavedNoUploadList = new ArrayList();
                        int isHaveTpatientvisit = 0;
                        isHavedNoUploadList = this.baseDml.GetIsHavedNoUpload(caseInfo.PatientInfo.PID.PatientNO, caseInfo.PatientInfo.PVisit.OutTime);
                        if (isHavedNoUploadList!=null &&isHavedNoUploadList.Count > 0)
                        {
                            foreach (FS.HISFC.Models.RADT.PatientInfo pInfo in isHavedNoUploadList)
                            {
                                isHaveTpatientvisit = uploadMana.GetIsHavedNoUpload(pInfo.PID.PatientNO, pInfo.PVisit.InTime);
                                if (isHaveTpatientvisit != 1)
                                {
                                    return -3;
                                }
                            }
                        }
                    }
                    //end 2012-1-21
                    //第一： 判断是否需要上传   需要上传(1 已上传未录入 2未上传) 3 不需要上传
                   // int intTemp = uploadMana.GetIsNeedUpload(caseInfo.PatientInfo.PID.PatientNO.Substring(4), caseInfo.PatientInfo.ID);
                    int intTemp = uploadMana.GetIsNeedUpload(this.PatientNoChang(caseInfo.PatientInfo.PID.PatientNO.Substring(this.PatientNoSubstr())), caseInfo.PatientInfo.ID);
                    if (intTemp == -1)
                    {
                        uploadMana.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("上传出错！" + uploadMana.Err);

                        return -1;
                    }
                    if (intTemp == 3)
                    {
                        uploadMana.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return 0;
                    }

                    //第二：判断是否更改 住院次数上传（解决不了本地和广东省病案3.0的次数不对应的问题） 
                    string Times = "";
                   // if (uploadMana.GetInTimes(caseInfo.PatientInfo.PID.PatientNO.Substring(4), caseInfo.PatientInfo.ID, intTemp, ref Times) == -1)
                    if(uploadMana.GetInTimes(this.PatientNoChang(caseInfo.PatientInfo.PID.PatientNO.Substring(this.PatientNoSubstr())),caseInfo.PatientInfo.ID,intTemp,ref Times)==-1)
                    {
                        uploadMana.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("上传出错！" + uploadMana.Err);

                        return -1;
                    }
                    if (intTemp == 1)
                    {
                        if (Times != "0")
                        {
                            caseInfo.PatientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(Times);
                        }
                    }
                    else
                    {
                        if (Times != "0")
                        {
                            caseInfo.PatientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(Times) + 1;
                        }
                    }
                    this.uploadInTimes = 0;
                    this.uploadInTimes = caseInfo.PatientInfo.InTimes;
                    #region 复制
                    //主表获取的数据才考虑按历史信息补全患者基本信息
                    if (!isMetCasBase)
                    {
                        //FS.HISFC.Models.RADT.PatientInfo patientinfoTemp = uploadMana.GetPatientFromBA(caseInfo.PatientInfo.PID.PatientNO.Substring(4));

                        FS.HISFC.Models.RADT.PatientInfo patientinfoTemp = uploadMana.GetPatientFromBA(this.PatientNoChang(caseInfo.PatientInfo.PID.PatientNO.Substring(this.PatientNoSubstr())));
                        if (patientinfoTemp == null)
                        {
                        }
                        else
                        {
                            try
                            {
                                if (caseInfo.PatientInfo.Name.Trim() == patientinfoTemp.Name.Trim())
                                {
                                    //if (patientinfo.PatientInfo.MaritalStatus.ID.ToString().Trim() == "")
                                    //{
                                    //    patientinfo.PatientInfo.MaritalStatus.ID = patientinfoTemp.MaritalStatus.ID;
                                    //}
                                    if (patientinfoTemp.Profession.ID.Trim() != "")
                                    {
                                        caseInfo.PatientInfo.Profession.ID = patientinfoTemp.Profession.ID;
                                    }
                                    if (patientinfoTemp.PVisit.InSource.ID.Trim() != "")
                                    {
                                        caseInfo.PatientInfo.PVisit.InSource.ID = patientinfoTemp.PVisit.InSource.ID;
                                    }

                                    if (caseInfo.PatientInfo.CompanyName.Trim() == "")
                                    {
                                        caseInfo.PatientInfo.CompanyName = patientinfoTemp.CompanyName; //工作单位
                                    }

                                    if (caseInfo.PatientInfo.PhoneBusiness.Trim() == "")
                                    {
                                        caseInfo.PatientInfo.PhoneBusiness = patientinfoTemp.PhoneBusiness; //单位电话
                                    }
                                    if (caseInfo.PatientInfo.BusinessZip.Trim() == "")
                                    {
                                        caseInfo.PatientInfo.BusinessZip = patientinfoTemp.BusinessZip; //单位邮编
                                    }

                                    if (caseInfo.PatientInfo.AddressHome.Trim() == "")
                                    {
                                        caseInfo.PatientInfo.AddressHome = patientinfoTemp.AddressHome; //户口或家庭所在
                                    }
                                    if (caseInfo.PatientInfo.HomeZip.Trim() == "")
                                    {
                                        caseInfo.PatientInfo.HomeZip = patientinfoTemp.HomeZip; //户口或家庭邮政编码
                                    }
                                    //patientinfo.PatientInfo.Kin.Name = patientinfoTemp.Kin.Name; //联系人姓名

                                    if (patientinfoTemp.Kin.Name.Trim() == caseInfo.PatientInfo.Kin.Name.Trim())
                                    {
                                        if (caseInfo.PatientInfo.Kin.RelationPhone.Trim() == "")
                                        {
                                            caseInfo.PatientInfo.Kin.RelationPhone = patientinfoTemp.Kin.RelationPhone; //联系人电话
                                        }

                                        //patientinfo.PatientInfo.Kin.RelationAddress = patientinfoTemp.Kin.RelationAddress; //联系人住址
                                        //patientinfo.PatientInfo.Kin.Relation.ID = patientinfoTemp.Kin.Relation.ID; //联系人关系
                                    }

                                    caseInfo.AnaphyFlag = patientinfoTemp.User02;
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                    #endregion

                    #region HIS_BA1 主表信息
                    //end 2011-3-30 ch
                    //if (uploadMana.DeleteHISBA1(patientinfo.PatientInfo.PID.PatientNO.Substring(4),patientinfo.PatientInfo.InTimes) == -1)
                    //{
                    //    uploadMana.Rollback();
                    //    FS.FrameWork.Management.PublicTrans.RollBack();
                    //    MessageBox.Show("删除表ba1失败!");

                    //    return -1;
                    //}
                    if (uploadMana.DeleteHISBA1ByFzyid(caseInfo.PatientInfo.ID) == -1)
                    {
                        uploadMana.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("删除表ba1失败!");
                        return -1;
                    }
                    if (uploadMana.InsertPatientInfoBA1(caseInfo, alFee, alChangeDept, new ArrayList(), this.isMetCasBase) == -1)
                    {
                        uploadMana.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("插入表ba1失败!");

                        return -1;
                    }
                    try//清空Fzkdate转科日期为1900-1-1的情况 无论成功与否，不管
                    {
                        uploadMana.UpdateHISBA1Fzkdate(caseInfo.PatientInfo.PID.PatientNO);
                    }
                    catch
                    {
                    }
                    #endregion
                    #region  HIS_BA2转科信息
                    if (alChangeDept != null && alChangeDept.Count > 0)
                    {
                       // if (uploadMana.DeleteHISBA2(caseInfo.PatientInfo.PID.PatientNO.Substring(4), caseInfo.PatientInfo.InTimes) == -1)
                        if (uploadMana.DeleteHISBA2(this.PatientNoChang(caseInfo.PatientInfo.PID.PatientNO.Substring(this.PatientNoSubstr())), caseInfo.PatientInfo.InTimes) == -1)
                        {
                            uploadMana.Rollback();
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("删除表ba2失败!");
                            return -1;
                        }
                        foreach (FS.HISFC.Models.RADT.Location changeInfo in alChangeDept)
                        {
                            if (uploadMana.InsertHISBA2(caseInfo, changeInfo) == -1)
                            {
                                uploadMana.Rollback();
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("插入表ba2失败!");
                                return -1;
                            }
                        }
                    }
                    #endregion
                    if (isMetCasBase)
                    {
                        #region HIS_BA3 诊断信息
                        if (alDiagnose != null && alDiagnose.Count > 0)
                        {
                            //if (uploadMana.DeleteHISBA3(caseInfo.PatientInfo.PID.PatientNO.Substring(4), caseInfo.PatientInfo.InTimes) == -1)
                            if (uploadMana.DeleteHISBA3(this.PatientNoChang(caseInfo.PatientInfo.PID.PatientNO.Substring(this.PatientNoSubstr())), caseInfo.PatientInfo.InTimes) == -1)
                            {
                                uploadMana.Rollback();
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("删除表ba3失败!");
                                return -1;
                            }
                            foreach (FS.HISFC.Models.HealthRecord.Diagnose diagNoseInfo in alDiagnose)
                            {
                                if (uploadMana.InsertHISBA3(caseInfo, diagNoseInfo) == -1)
                                {
                                    uploadMana.Rollback();
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("插入表ba3失败!");
                                    return -1;
                                }
                            }
                        }
                        #endregion
                        #region HIS_BA4 手术信息
                        if (opAlReal.Count > 0)
                        {
                           // if (uploadMana.DeleteHISBA4(caseInfo.PatientInfo.PID.PatientNO.Substring(4), caseInfo.PatientInfo.InTimes) == -1)
                            if (uploadMana.DeleteHISBA4(this.PatientNoChang(caseInfo.PatientInfo.PID.PatientNO.Substring(this.PatientNoSubstr())), caseInfo.PatientInfo.InTimes) == -1)
                            {
                                uploadMana.Rollback();
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("删除表ba4失败!");
                                return -1;
                            }
                            foreach (FS.HISFC.Models.HealthRecord.OperationDetail op in opAlReal)
                            {
                                if (uploadMana.insertHisBa4(caseInfo, op) == -1)
                                {
                                    uploadMana.Rollback();
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("插入表ba4失败!");
                                    return -1;
                                }
                            }
                        }
                        #endregion
                        #region HIS_BA5 妇婴信息
                        if (babyAl.Count > 0)
                        {
                            //if (uploadMana.DeleteHISBA5(caseInfo.PatientInfo.PID.PatientNO.Substring(4), caseInfo.PatientInfo.InTimes) == -1)
                            if (uploadMana.DeleteHISBA5(this.PatientNoChang(caseInfo.PatientInfo.PID.PatientNO.Substring(this.PatientNoSubstr())), caseInfo.PatientInfo.InTimes) == -1)
                            {
                                uploadMana.Rollback();
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("删除表ba5失败!");
                                return -1;
                            }

                            foreach (FS.HISFC.Models.HealthRecord.Baby babyInfo in babyAl)
                            {
                                if (uploadMana.insertHisBa5(caseInfo, babyInfo) == -1)
                                {
                                    uploadMana.Rollback();
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("插入表ba5失败!");
                                    return -1;
                                }
                            }
                        }
                        #endregion
                        #region HIS_BA6 HIS_BA7 肿瘤卡
                        if (TumourInfo != null)
                        {
                            if ((TumourInfo.Tumour_Type != null || TumourInfo.Tumour_Stage != null || TumourInfo.Rmodeid != null || TumourInfo.Rprocessid != null || TumourInfo.Rdeviceid != null || TumourInfo.Cmodeid != null || TumourInfo.Cmethod != null)
           && (TumourInfo.Tumour_Type != "" || TumourInfo.Tumour_Stage != "" || TumourInfo.Rmodeid != "" || TumourInfo.Rprocessid != "" || TumourInfo.Rdeviceid != "" || TumourInfo.Cmodeid != "" || TumourInfo.Cmethod != ""))
                            {
                               // if (uploadMana.DeleteHISBA6(caseInfo.PatientInfo.PID.PatientNO.Substring(4), caseInfo.PatientInfo.InTimes) == -1)
                                if (uploadMana.DeleteHISBA6(this.PatientNoChang(caseInfo.PatientInfo.PID.PatientNO.Substring(this.PatientNoSubstr())), caseInfo.PatientInfo.InTimes) == -1)
                                {
                                    uploadMana.Rollback();
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("删除表ba6失败!");
                                    return -1;
                                }
                                if (uploadMana.InsertHISBA6(caseInfo, TumourInfo) == -1)
                                {
                                    uploadMana.Rollback();
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("插入表ba6失败!");
                                    return -1;
                                }

                                if (tumourDetailAl != null && tumourDetailAl.Count > 0)
                                {
                                   // if (uploadMana.DeleteHISBA7(caseInfo.PatientInfo.PID.PatientNO.Substring(4), caseInfo.PatientInfo.InTimes) == -1)
                                    if (uploadMana.DeleteHISBA7(this.PatientNoChang(caseInfo.PatientInfo.PID.PatientNO.Substring(this.PatientNoSubstr())), caseInfo.PatientInfo.InTimes) == -1)
                                    {
                                        uploadMana.Rollback();
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        MessageBox.Show("删除表ba7失败!");
                                        return -1;
                                    }
                                    foreach (FS.HISFC.Models.HealthRecord.TumourDetail tumourDetailInfo in tumourDetailAl)
                                    {
                                        if (uploadMana.InsertHISBA7(caseInfo, tumourDetailInfo) == -1)
                                        {
                                            uploadMana.Rollback();
                                            FS.FrameWork.Management.PublicTrans.RollBack();
                                            MessageBox.Show("插入表ba7失败!");
                                            return -1;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    uploadMana.Commit();
                    FS.FrameWork.Management.PublicTrans.Commit();
                    return 1;
                }
                catch (Exception ex)
                {
                    uploadMana.Rollback();
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("上传失败!" + ex.Message);

                    return -1;
                }
                #endregion
            }
            else
            {
                #region Drgs 传统uc病案首页上传
                FS.HISFC.BizProcess.Integrate.RADT dgInpatient = new FS.HISFC.BizProcess.Integrate.RADT();
                this.inTimes = caseInfo.PatientInfo.InTimes;

                //出生地为空 则赋籍贯值  
                if (caseInfo.PatientInfo.AreaCode != null && caseInfo.PatientInfo.AreaCode.Trim().Equals(""))
                {
                    caseInfo.PatientInfo.AreaCode = caseInfo.PatientInfo.DIST;
                }
                //转科
                ArrayList alChangeDept = new ArrayList();
                if (isMetCasBase)
                {
                    alChangeDept = deptShiftMana.QueryChangeDeptFromShiftApply(caseInfo.PatientInfo.ID, "2");
                }
                else
                {
                    alChangeDept = deptShiftMana.QueryChangeDeptFromShiftApply(caseInfo.PatientInfo.ID, "1");
                }
                if (alChangeDept == null)
                {
                    return -1;
                }

                #region 费用
                DataSet dsFee = new DataSet();

                if (healthfeeMana.QueryFeeForDrgsByInpatientNO(caseInfo.PatientInfo.ID, ref dsFee) == -1)
                {
                    MessageBox.Show("获取费用出错！");
                    return -1;
                }
                if (dsFee == null)
                {
                    return -1;
                }
                #endregion

                #region 获取 婴儿信息、手术信息、诊断信息
                ArrayList babyAl = new ArrayList();
                ArrayList opAl = new ArrayList();
                ArrayList opAlReal = new ArrayList();
                ArrayList alDiagnose = new ArrayList();
                FS.HISFC.Models.HealthRecord.Tumour TumourInfo = new FS.HISFC.Models.HealthRecord.Tumour();
                ArrayList tumourDetailAl = new ArrayList();
                if (isMetCasBase)
                {
                    FS.HISFC.BizLogic.HealthRecord.Baby babyMgr = new FS.HISFC.BizLogic.HealthRecord.Baby();
                    babyAl = babyMgr.QueryBabyByInpatientNo(caseInfo.PatientInfo.ID);
                    caseInfo.PatientInfo.User03 = babyAl.Count.ToString();//婴儿数量 

                    FS.HISFC.BizLogic.HealthRecord.Operation opMgr = new FS.HISFC.BizLogic.HealthRecord.Operation();
                    opAl = opMgr.QueryOperationByInpatientNo(caseInfo.PatientInfo.ID);

                    foreach (FS.HISFC.Models.HealthRecord.OperationDetail opInfo in opAl)  //防止一些错误数据 有空需要从录入处更改
                    {
                        if (opInfo.OperationDate.Year.ToString().Trim() == "1" || opInfo.OperationInfo.Name.Trim() == "无")
                        {
                            continue;
                        }
                        opAlReal.Add(opInfo);
                    }

                    alDiagnose = this.baseDml.QueryCaseDiagnoseByInpatientNo(caseInfo.PatientInfo.ID);

                    FS.HISFC.BizLogic.HealthRecord.Tumour tumourMgr = new FS.HISFC.BizLogic.HealthRecord.Tumour();
                    TumourInfo = tumourMgr.GetTumour(caseInfo.PatientInfo.ID);
                    tumourDetailAl = tumourMgr.QueryTumourDetail(caseInfo.PatientInfo.ID);
                }
                #endregion

                FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface uploadMana = null;
                try
                {
                    uploadMana = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface();
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    dgInpatient.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                }
                catch (Exception ex)
                {
                    try
                    {
                        uploadMana.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                    }
                    catch
                    {
                    }
                    MessageBox.Show("连接失败!" + ex.Message);

                    return -2;
                }

                try
                {
                    //begin 2012-1-21 判断在出院日期前是否存在未上传的数据 chengym 避免上传的住院次数翻转
                    if (!cbInputPatientNo.Checked)
                    {
                        ArrayList isHavedNoUploadList = new ArrayList();
                        int isHaveTpatientvisit = 0;
                        isHavedNoUploadList = this.baseDml.GetIsHavedNoUpload(caseInfo.PatientInfo.PID.PatientNO, caseInfo.PatientInfo.PVisit.OutTime);
                        if (isHavedNoUploadList != null && isHavedNoUploadList.Count > 0)
                        {
                            foreach (FS.HISFC.Models.RADT.PatientInfo pInfo in isHavedNoUploadList)
                            {
                                isHaveTpatientvisit = uploadMana.GetIsHavedNoUpload(pInfo.PID.PatientNO, pInfo.PVisit.InTime);
                                if (isHaveTpatientvisit != 1)
                                {
                                    return -3;
                                }
                            }
                        }
                    }
                    //end 2012-1-21

                    //第一： 判断是否需要上传   需要上传(1 已上传未录入 2未上传) 3 不需要上传
                   // int intTemp = uploadMana.GetIsNeedUpload(caseInfo.PatientInfo.PID.PatientNO.Substring(4), caseInfo.PatientInfo.ID);
                    int intTemp = uploadMana.GetIsNeedUpload(this.PatientNoChang(caseInfo.PatientInfo.PID.PatientNO.Substring(this.PatientNoSubstr())), caseInfo.PatientInfo.ID);
                    if (intTemp == -1)
                    {
                        uploadMana.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("上传出错！" + uploadMana.Err);

                        return -1;
                    }
                    if (intTemp == 3)
                    {
                        uploadMana.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return 0;
                    }

                    //第二：判断是否更改 住院次数上传（解决不了本地和广东省病案3.0的次数不对应的问题） 
                    string Times = "";
                   // if (uploadMana.GetInTimes(caseInfo.PatientInfo.PID.PatientNO.Substring(4), caseInfo.PatientInfo.ID, intTemp, ref Times) == -1)
                    if(uploadMana.GetInTimes(this.PatientNoChang(caseInfo.PatientInfo.PID.PatientNO.Substring(this.PatientNoSubstr())),caseInfo.PatientInfo.ID,intTemp,ref Times)==-1)
                    {
                        uploadMana.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("上传出错！" + uploadMana.Err);

                        return -1;
                    }
                    if (intTemp == 1)
                    {
                        if (Times != "0")
                        {
                            caseInfo.PatientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(Times);
                        }
                    }
                    else
                    {
                        if (Times != "0")
                        {
                            caseInfo.PatientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(Times) + 1;
                        }
                    }
                    this.uploadInTimes = 0;
                    this.uploadInTimes = caseInfo.PatientInfo.InTimes;
                    #region 复制
                    //主表获取的数据才考虑按历史信息补全患者基本信息
                    if (!isMetCasBase)
                    {
                       // FS.HISFC.Models.RADT.PatientInfo patientinfoTemp = uploadMana.GetPatientFromBA(caseInfo.PatientInfo.PID.PatientNO.Substring(4));
                        FS.HISFC.Models.RADT.PatientInfo patientinfoTemp = uploadMana.GetPatientFromBA(this.PatientNoChang(caseInfo.PatientInfo.PID.PatientNO.Substring(this.PatientNoSubstr())));

                        if (patientinfoTemp == null)
                        {
                        }
                        else
                        {
                            try
                            {
                                if (caseInfo.PatientInfo.Name.Trim() == patientinfoTemp.Name.Trim())
                                {
                                    //if (patientinfo.PatientInfo.MaritalStatus.ID.ToString().Trim() == "")
                                    //{
                                    //    patientinfo.PatientInfo.MaritalStatus.ID = patientinfoTemp.MaritalStatus.ID;
                                    //}
                                    if (patientinfoTemp.Profession.ID.Trim() != "")
                                    {
                                        caseInfo.PatientInfo.Profession.ID = patientinfoTemp.Profession.ID;
                                    }
                                    if (patientinfoTemp.PVisit.InSource.ID.Trim() != "")
                                    {
                                        caseInfo.PatientInfo.PVisit.InSource.ID = patientinfoTemp.PVisit.InSource.ID;
                                    }

                                    if (caseInfo.PatientInfo.CompanyName.Trim() == "")
                                    {
                                        caseInfo.PatientInfo.CompanyName = patientinfoTemp.CompanyName; //工作单位
                                    }

                                    if (caseInfo.PatientInfo.PhoneBusiness.Trim() == "")
                                    {
                                        caseInfo.PatientInfo.PhoneBusiness = patientinfoTemp.PhoneBusiness; //单位电话
                                    }
                                    if (caseInfo.PatientInfo.BusinessZip.Trim() == "")
                                    {
                                        caseInfo.PatientInfo.BusinessZip = patientinfoTemp.BusinessZip; //单位邮编
                                    }

                                    if (caseInfo.PatientInfo.AddressHome.Trim() == "")
                                    {
                                        caseInfo.PatientInfo.AddressHome = patientinfoTemp.AddressHome; //户口或家庭所在
                                    }
                                    if (caseInfo.PatientInfo.HomeZip.Trim() == "")
                                    {
                                        caseInfo.PatientInfo.HomeZip = patientinfoTemp.HomeZip; //户口或家庭邮政编码
                                    }
                                    //patientinfo.PatientInfo.Kin.Name = patientinfoTemp.Kin.Name; //联系人姓名

                                    if (patientinfoTemp.Kin.Name.Trim() == caseInfo.PatientInfo.Kin.Name.Trim())
                                    {
                                        if (caseInfo.PatientInfo.Kin.RelationPhone.Trim() == "")
                                        {
                                            caseInfo.PatientInfo.Kin.RelationPhone = patientinfoTemp.Kin.RelationPhone; //联系人电话
                                        }

                                        //patientinfo.PatientInfo.Kin.RelationAddress = patientinfoTemp.Kin.RelationAddress; //联系人住址
                                        //patientinfo.PatientInfo.Kin.Relation.ID = patientinfoTemp.Kin.Relation.ID; //联系人关系
                                    }

                                    caseInfo.AnaphyFlag = patientinfoTemp.User02;
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                    #endregion

                    #region HIS_BA1 主表信息
                    //end 2011-3-30 ch
                    //if (uploadMana.DeleteHISBA1(patientinfo.PatientInfo.PID.PatientNO.Substring(4),patientinfo.PatientInfo.InTimes) == -1)
                    //{
                    //    uploadMana.Rollback();
                    //    FS.FrameWork.Management.PublicTrans.RollBack();
                    //    MessageBox.Show("删除表ba1失败!");

                    //    return -1;
                    //}
                    if (uploadMana.DeleteHISBA1ByFzyid(caseInfo.PatientInfo.ID) == -1)
                    {
                        uploadMana.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("删除表ba1失败!");
                        return -1;
                    }
                    if (uploadMana.InsertPatientInfoBA1Drgs(caseInfo, dsFee, alChangeDept, alDiagnose,this.isMetCasBase) == -1)
                   {
                        uploadMana.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("插入表ba1失败!");
                        return -1;
                    }
                    try//清空Fzkdate转科日期为1900-1-1的情况 无论成功与否，不管
                    {
                        uploadMana.UpdateHISBA1Fzkdate(caseInfo.PatientInfo.PID.PatientNO);
                    }
                    catch
                    {
                    }
                    #endregion
                    #region  HIS_BA2转科信息
                    if (alChangeDept != null && alChangeDept.Count > 0)
                    {
                       // if (uploadMana.DeleteHISBA2(caseInfo.PatientInfo.PID.PatientNO.Substring(4), caseInfo.PatientInfo.InTimes) == -1)
                        if (uploadMana.DeleteHISBA2(this.PatientNoChang(caseInfo.PatientInfo.PID.PatientNO.Substring(this.PatientNoSubstr())), caseInfo.PatientInfo.InTimes) == -1)
                        {
                            uploadMana.Rollback();
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("删除表ba2失败!");
                            return -1;
                        }
                        foreach (FS.HISFC.Models.RADT.Location changeInfo in alChangeDept)
                        {
                            if (changeInfo.Dept.ID != null && changeInfo.Dept.ID != "")
                            {
                                if (uploadMana.InsertHISBA2(caseInfo, changeInfo) == -1)
                                {
                                    uploadMana.Rollback();
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("插入表ba2失败!");
                                    return -1;
                                }
                            }
                        }
                    }
                    #endregion
                    if (isMetCasBase)
                    {
                        #region HIS_BA3 诊断信息
                        if (alDiagnose != null && alDiagnose.Count > 0)
                        {
                           // if (uploadMana.DeleteHISBA3(caseInfo.PatientInfo.PID.PatientNO.Substring(4), caseInfo.PatientInfo.InTimes) == -1)
                            if (uploadMana.DeleteHISBA3(this.PatientNoChang(caseInfo.PatientInfo.PID.PatientNO.Substring(this.PatientNoSubstr())), caseInfo.PatientInfo.InTimes) == -1)
                            {
                                uploadMana.Rollback();
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("删除表ba3失败!");
                                return -1;
                            }
                            foreach (FS.HISFC.Models.HealthRecord.Diagnose diagNoseInfo in alDiagnose)
                            {
                                if (uploadMana.InsertHISBA3Drgs(caseInfo, diagNoseInfo) == -1)
                                {
                                    uploadMana.Rollback();
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("插入表ba3失败!");
                                    return -1;
                                }
                            }
                        }
                        #endregion
                        #region HIS_BA4 手术信息
                        if (opAlReal.Count > 0)
                        {
                           // if (uploadMana.DeleteHISBA4(caseInfo.PatientInfo.PID.PatientNO.Substring(4), caseInfo.PatientInfo.InTimes) == -1)
                            if (uploadMana.DeleteHISBA4(this.PatientNoChang(caseInfo.PatientInfo.PID.PatientNO.Substring(this.PatientNoSubstr())), caseInfo.PatientInfo.InTimes) == -1)
                            {
                                uploadMana.Rollback();
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("删除表ba4失败!");
                                return -1;
                            }
                            foreach (FS.HISFC.Models.HealthRecord.OperationDetail op in opAlReal)
                            {
                                if (uploadMana.insertHisBa4Drgs(caseInfo, op) == -1)
                                {
                                    uploadMana.Rollback();
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("插入表ba4失败!");
                                    return -1;
                                }
                            }
                        }
                        #endregion
                        #region HIS_BA5 妇婴信息
                        if (babyAl.Count > 0)
                        {
                            //if (uploadMana.DeleteHISBA5(caseInfo.PatientInfo.PID.PatientNO.Substring(4), caseInfo.PatientInfo.InTimes) == -1)
                            if (uploadMana.DeleteHISBA5(this.PatientNoChang(caseInfo.PatientInfo.PID.PatientNO.Substring(this.PatientNoSubstr())), caseInfo.PatientInfo.InTimes) == -1)
                            {
                                uploadMana.Rollback();
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("删除表ba5失败!");
                                return -1;
                            }

                            foreach (FS.HISFC.Models.HealthRecord.Baby babyInfo in babyAl)
                            {
                                if (uploadMana.insertHisBa5Drgs(caseInfo, babyInfo) == -1)
                                {
                                    uploadMana.Rollback();
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("插入表ba5失败!");
                                    return -1;
                                }
                            }
                        }
                        #endregion
                        #region HIS_BA6 HIS_BA7 肿瘤卡
                        if (TumourInfo != null)
                        {
                            if ((TumourInfo.Tumour_Type != null || TumourInfo.Tumour_Stage != null || TumourInfo.Rmodeid != null || TumourInfo.Rprocessid != null || TumourInfo.Rdeviceid != null || TumourInfo.Cmodeid != null || TumourInfo.Cmethod != null)
           && (TumourInfo.Tumour_Type != "" || TumourInfo.Tumour_Stage != "" || TumourInfo.Rmodeid != "" || TumourInfo.Rprocessid != "" || TumourInfo.Rdeviceid != "" || TumourInfo.Cmodeid != "" || TumourInfo.Cmethod != ""))
                            {
                             //   if (uploadMana.DeleteHISBA6(caseInfo.PatientInfo.PID.PatientNO.Substring(4), caseInfo.PatientInfo.InTimes) == -1)
                                if (uploadMana.DeleteHISBA6(this.PatientNoChang(caseInfo.PatientInfo.PID.PatientNO.Substring(this.PatientNoSubstr())), caseInfo.PatientInfo.InTimes) == -1)
                                {
                                    uploadMana.Rollback();
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("删除表ba6失败!");
                                    return -1;
                                }
                                if (uploadMana.InsertHISBA6Drgs(caseInfo, TumourInfo) == -1)
                                {
                                    uploadMana.Rollback();
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("插入表ba6失败!");
                                    return -1;
                                }
                                if (TumourInfo.Gy1 == 0)
                                {
                                    try
                                    {
                                       // uploadMana.UpdateHISBA6FYRQ(caseInfo.PatientInfo.PID.PatientNO.Substring(4));
                                        uploadMana.UpdateHISBA6FYRQ(this.PatientNoChang(caseInfo.PatientInfo.PID.PatientNO.Substring(this.PatientNoSubstr())));
                                    }
                                    catch
                                    {
                                    }
                                }
                                if (TumourInfo.Gy2 == 0)
                                {
                                    try
                                    {
                                      //  uploadMana.UpdateHISBA6FQRQ(caseInfo.PatientInfo.PID.PatientNO.Substring(4));
                                        uploadMana.UpdateHISBA6FQRQ(this.PatientNoChang(caseInfo.PatientInfo.PID.PatientNO.Substring(this.PatientNoSubstr())));
                                    }
                                    catch
                                    {
                                    }
                                }
                                if (TumourInfo.Gy3 == 0)
                                {
                                    try
                                    {
                                      //  uploadMana.UpdateHISBA6FZRQ(caseInfo.PatientInfo.PID.PatientNO.Substring(4));
                                        uploadMana.UpdateHISBA6FZRQ(this.PatientNoChang(caseInfo.PatientInfo.PID.PatientNO.Substring(this.PatientNoSubstr())));
                                    }
                                    catch
                                    {
                                    }
                                }

                                if (tumourDetailAl != null && tumourDetailAl.Count > 0)
                                {
                                   // if (uploadMana.DeleteHISBA7(caseInfo.PatientInfo.PID.PatientNO.Substring(4), caseInfo.PatientInfo.InTimes) == -1)
                                    if (uploadMana.DeleteHISBA7(this.PatientNoChang(caseInfo.PatientInfo.PID.PatientNO.Substring(this.PatientNoSubstr())), caseInfo.PatientInfo.InTimes) == -1)
                                    {
                                        uploadMana.Rollback();
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        MessageBox.Show("删除表ba7失败!");
                                        return -1;
                                    }
                                    foreach (FS.HISFC.Models.HealthRecord.TumourDetail tumourDetailInfo in tumourDetailAl)
                                    {
                                        if (uploadMana.InsertHISBA7Drgs(caseInfo, tumourDetailInfo) == -1)
                                        {
                                            uploadMana.Rollback();
                                            FS.FrameWork.Management.PublicTrans.RollBack();
                                            MessageBox.Show("插入表ba7失败!");
                                            return -1;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    uploadMana.Commit();
                    FS.FrameWork.Management.PublicTrans.Commit();
                    return 1;
                }
                catch (Exception ex)
                {
                    uploadMana.Rollback();
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("上传失败!" + ex.Message);

                    return -1;
                }
                #endregion
            }
        }
        /// <summary>
        /// 更新上传标志
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="inTimes"></param>
        /// <returns></returns>
        private int UpdateUploadStat(string inpatientNo,int inTimes)
        {
            string strSQL = string.Empty;
            if (this.isMetCasBase)
            {
                strSQL = @"update met_cas_base t set t.uploadstatu='1',t.in_times='{1}' where t.inpatient_no='{0}'";
                this.UpdateUploadStatFin(inpatientNo,inTimes);
            }
            else
            {
                strSQL = @"update fin_ipr_inmaininfo i set i.casesend_flag='1',i.in_times='{1}' where i.inpatient_no='{0}'";
            }

            try
            {
                strSQL = string.Format(strSQL, inpatientNo,inTimes.ToString());
            }
            catch
            {
                return -1;
            }

            return this.baseMana.ExecNoQuery(strSQL);
        }
        /// <summary>
        /// 更新上传标志
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="inTimes"></param>
        /// <returns></returns>
        private int UpdateUploadStatFin(string inpatientNo,int inTimes)
        {
            string strSQL = string.Empty;
            strSQL = @"update fin_ipr_inmaininfo i set i.casesend_flag='1',i.in_times='{1}' where i.inpatient_no='{0}'";
            try
            {
                strSQL = string.Format(strSQL, inpatientNo,inTimes.ToString());
            }
            catch
            {
                return -1;
            }

            return this.baseMana.ExecNoQuery(strSQL);
        }
         /// <summary>
        /// 显示查询结果控件
        /// </summary>
        /// <param name="c">结果控件</param>
        private int ShowControl(Control c)
        {
            try
            {
                System.Windows.Forms.Form form = new Form();
                form.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                if (form.Controls.Count > 0)
                {
                    form.Controls.Clear();
                }
                if (!form.Controls.Contains(c))
                {
                    form.Controls.Add(c);
                }
                form.Size = new Size(c.Width + 10, c.Height + 50);
                form.StartPosition = FormStartPosition.CenterScreen;
                form.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show("控件加载错误:" + c.Name + ex.Message.ToString());
                return -1;
            }

            return 0;
        }
        /// <summary>
        /// 输入住院号框的屏蔽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbInputPatientNo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbInputPatientNo.Checked == true)
            {
                this.txtPatientNo.Visible = true;
            }
            else if (this.cbInputPatientNo.Checked == false)
            {
                this.txtPatientNo.Visible = false;
            }
        }

        //住院号转病历号
        private string PatientNoChang(string patientNo)
        {
            string ret = string.Empty;
            //是否需要转换
            ArrayList al = this.constMana.GetList("CasePatientNoChang");
            if (al != null && al.Count > 0)//需要转换
            {
                ret = patientNo.Replace("v", "V");

                if (ret.IndexOf("V") >= 0)
                {
                    ret = ret.Replace("V", "0");
                    ret = "V" + ret.Substring(1);
                }
                else
                {
                    ret = patientNo;
                }
            }
            else
            {
                ret = patientNo;
            }
            return ret;
        }

        /// <summary>
        ///  上传病案号位数
        ///  没有设置常数：返回8位 否则按照实际返回
        /// </summary>
        /// <returns></returns>
        private int PatientNoSubstr()
        {
            int ret = 2;//8位 
            FS.FrameWork.Models.NeuObject obj = this.constMana.GetConstant("CASEPNOSUBSTR", "1");
            //无维护情况上传8位
            if (obj == null)
            {
                ret = 2;
                return ret;
            }
            if (obj.Memo == "")
            {
                ret = 2;
                return ret;
            }
            else if (obj.Memo.ToUpper() == "TRUE")
            {
                ret = 2;
                return ret;
            }
            else
            {
                int uplaodNum = 0;
                try
                {
                    uplaodNum = FS.FrameWork.Function.NConvert.ToInt32(obj.Memo);
                }
                catch
                {
                    uplaodNum = 0;
                }
                if (uplaodNum == 0)
                {
                    ret = 2;
                    return ret;
                }
                else
                {
                    ret = 10 - uplaodNum;
                }
            }
            return ret;
        }
    }
}
