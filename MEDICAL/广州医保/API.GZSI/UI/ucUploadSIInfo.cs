using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using API.GZSI.Business;


namespace API.GZSI.UI
{
    /// <summary>
    /// 创建日期：2019-11-25
    /// 创建人：Giiber
    /// 功能说明：医保病案上传功能
    /// </summary>
    public partial class ucUploadSIInfo : FS.FrameWork.WinForms.Controls.ucBaseControl
    {

        #region 业务管理类

        /// <summary>
        /// 住院患者信息管理类
        /// </summary>
        private FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// 医保数据操作
        /// </summary>
        private LocalManager localMgr = new LocalManager();

        #endregion 

        #region 变量

        /// <summary>
        /// 交易流水号
        /// </summary>
        string SerialNumber = string.Empty;

        /// <summary>
        /// 交易版本号
        /// </summary>
        string strTransVersion = string.Empty;

        /// <summary>
        /// 交易验证码
        /// </summary>
        string strVerifyCode = string.Empty;

        /// <summary>
        /// 公共返回值
        /// </summary>
        int returnvalue = 0;

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        public ucUploadSIInfo()
        {
            InitializeComponent();
            BindEvents();
            InitContorls();
        }

        /// <summary>
        /// 绑定事件
        /// </summary>
        private void BindEvents()
        {
            //病案
            this.fpUnupload.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(fpUnupload_CellClick);
            this.chkALL.CheckedChanged += new EventHandler(chkALL_CheckedChanged);

            //结算清单
            this.neuSpread1.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(fpUnupload_JSQD_CellClick);
        }

        /// <summary>
        /// 解绑事件
        /// </summary>
        private void UnbindEvents()
        {
            //病案
            this.chkALL.CheckedChanged -= new EventHandler(chkALL_CheckedChanged);
            this.fpUnupload.CellClick -= new FarPoint.Win.Spread.CellClickEventHandler(fpUnupload_CellClick);

            //结算清单
            this.neuSpread1.CellClick -= new FarPoint.Win.Spread.CellClickEventHandler(fpUnupload_JSQD_CellClick);
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        /// <returns></returns>
        protected int InitContorls()
        {
            this.dtpBegin.Value = DateTime.Now.Date.AddMonths(-1);
            this.dtpEnd.Value = DateTime.Now.Date;
            return 1;
        }


        #region 工具栏

        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 增加ToolBar控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("病案上传", "确定上传", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z暂存, true, false, null);
            toolBarService.AddToolButton("医嘱上传", "医嘱上传", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Y医嘱, true, false, null);
            toolBarService.AddToolButton("病历上传", "病历上传", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B病历, true, false, null);
            toolBarService.AddToolButton("医疗保障基金结算清单上传", "医疗保障基金结算清单上传", (int)FS.FrameWork.WinForms.Classes.EnumImageList.M明细, true, false, null);
            toolBarService.AddToolButton("取消上传", "取消上传", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);

            return this.toolBarService;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.refreshData();
            return 1;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveUpload();
            return 1;
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "病案上传":
                    this.UploadBrsy();
                    break;
                case "医嘱上传":
                    this.UploadOrder();
                    break;
                case "病历上传":
                    this.UploadCaseDoc();
                    break;
                case "医疗保障基金结算清单上传":
                    this.UploadJjqd();
                    break;
                case "取消上传":
                    this.CancelUpload();
                    break;
                default:
                    break;
            }
        }

        #endregion

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <returns></returns>
        private int refreshData()
        {
            this.ClearSheet();
            DateTime begin = this.dtpBegin.Value.Date;
            DateTime end = this.dtpEnd.Value.Date;
            string patientNO = this.tbInpatientNO.Text;
            string name = this.tbName.Text;

            if (string.IsNullOrEmpty(patientNO))
            {
                patientNO = "ALL";
            }

            if (string.IsNullOrEmpty(name))
            {
                name = "ALL";
            }

            List<FS.HISFC.Models.RADT.PatientInfo> siPatientList = this.localMgr.GetPatientForUpload(begin, end.AddDays(1), patientNO, name);

            if (siPatientList == null)
            {
                MessageBox.Show("查询患者里列表失败：" + this.localMgr.Err);
                return -1;
            }
            else
            {
                this.setData(siPatientList);
            }
 
            return 1;
        }

        /// <summary>
        /// 待上传患者列表点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpUnupload_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column == 0)
            {
                bool selected = false;
                if (this.fpUnupload_Sheet1.Cells[e.Row, e.Column].Value != null && (bool)this.fpUnupload_Sheet1.Cells[e.Row, e.Column].Value)
                {
                    selected = true;
                }
                this.fpUnupload_Sheet1.Cells[e.Row, e.Column].Value = !selected;
            }
            else
            {
                foreach (FarPoint.Win.Spread.Row row in this.fpUnupload_Sheet1.Rows)
                {
                    this.fpUnupload_Sheet1.Cells[row.Index, (int)EnumUnupdateCol.Select].Value = false;
                }

                this.fpUnupload_Sheet1.Cells[e.Row, (int)EnumUnupdateCol.Select].Value = true;
            }
        }

        private void fpUnupload_JSQD_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column == 0)
            {
                bool selected = false;
                if (this.fpUnupload_JSQD.Cells[e.Row, e.Column].Value != null && (bool)this.fpUnupload_JSQD.Cells[e.Row, e.Column].Value)
                {
                    selected = true;
                }
                this.fpUnupload_JSQD.Cells[e.Row, e.Column].Value = !selected;
            }
            else
            {
                foreach (FarPoint.Win.Spread.Row row in this.fpUnupload_JSQD.Rows)
                {
                    this.fpUnupload_JSQD.Cells[row.Index, (int)EnumUnupdateCol.Select].Value = false;
                }

                this.fpUnupload_JSQD.Cells[e.Row, (int)EnumUnupdateCol.Select].Value = true;
            }
        }

        /// <summary>
        /// 待上传患者全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkALL_CheckedChanged(object sender, EventArgs e)
        {
            foreach (FarPoint.Win.Spread.Row row in this.fpUnupload_Sheet1.Rows)
            {
                this.fpUnupload_Sheet1.Cells[row.Index, (int)EnumUnupdateCol.Select].Value = this.chkALL.Checked;
            }

            foreach (FarPoint.Win.Spread.Row row in this.fpUnupload_JSQD.Rows)
            {
                this.fpUnupload_JSQD.Cells[row.Index, (int)EnumUnupdateCol.Select].Value = this.chkALL.Checked;
            }
        }


        /// <summary>
        /// 上传病案首页
        /// </summary>
        /// <returns></returns>
        private int UploadBrsy()
        {
            if (this.tabSheet.SelectedIndex != 0)
            {
                MessageBox.Show("请勾选选择要上传病案的患者！");
                return -1;
            }

            List<FS.HISFC.Models.RADT.PatientInfo> selectedPatient = this.getSelectedPatient();

            if (selectedPatient == null || selectedPatient.Count == 0)
            {
                MessageBox.Show("请勾选选择要上传病案的患者！");
                return -1;
            }

            try
            {
                string errMsg = string.Empty;
                string tips = "共{0}个患者,正在上传第{1}个";

                int count = selectedPatient.Count;
                int index = 0;

                foreach (FS.HISFC.Models.RADT.PatientInfo patient in selectedPatient)
                {
                    index++;

                    string tipStr = string.Format(tips, count.ToString(), index.ToString());

                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(tipStr);
                    Application.DoEvents();

                    if (this.uploadBrsyInfo(patient, ref errMsg) < 0)
                    {
                        //回滚
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("上传病案信息发生错误：" + errMsg);
                        return -1;
                    }

                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("上传病案信息发生错误：" + ex.Message);
                return -1;
            }

            MessageBox.Show("上传信息成功！");
            this.refreshData();
            return 1;
        }

        /// <summary>
        /// 上传医嘱
        /// </summary>
        /// <returns></returns>
        protected int UploadOrder()
        {
            if (this.tabSheet.SelectedIndex != 0)
            {
                MessageBox.Show("请勾选选择要上传医嘱的患者！");
                return -1;
            }

            List<FS.HISFC.Models.RADT.PatientInfo> selectedPatient = this.getSelectedPatient();

            if (selectedPatient == null || selectedPatient.Count == 0)
            {
                MessageBox.Show("请勾选选择要上传医嘱的患者！");
                return -1;
            }

            try
            {
                string errMsg = string.Empty;
                string tips = "共{0}个患者,正在上传第{1}个";

                int count = selectedPatient.Count;
                int index = 0;

                foreach (FS.HISFC.Models.RADT.PatientInfo patient in selectedPatient)
                {
                    index++;

                    string tipStr = string.Format(tips, count.ToString(), index.ToString());

                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(tipStr);
                    Application.DoEvents();

                    if (this.uploadOrderInfo(patient, ref errMsg) < 0)
                    {
                        //回滚
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("上传患者医嘱发生错误：" + errMsg);
                        return -1;
                    }

                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("上传患者医嘱发生错误：" + ex.Message);
                return -1;
            }

            MessageBox.Show("上传医嘱成功！");
            this.refreshData();
            return 1;
        }

        /// <summary>
        /// 上传病历信息
        /// </summary>
        /// <returns></returns>
        private int UploadCaseDoc()
        {
            if (this.tabSheet.SelectedIndex != 0)
            {
                MessageBox.Show("请勾选选择要上传病历的患者！");
                return -1;
            }

            List<FS.HISFC.Models.RADT.PatientInfo> selectedPatient = this.getSelectedPatient();

            if (selectedPatient == null || selectedPatient.Count == 0)
            {
                MessageBox.Show("请勾选选择要上传病历的患者！");
                return -1;
            }

            try
            {
                string errMsg = string.Empty;
                string tips = "共{0}个患者,正在上传第{1}个";

                int count = selectedPatient.Count;
                int index = 0;

                foreach (FS.HISFC.Models.RADT.PatientInfo patient in selectedPatient)
                {
                    index++;

                    string tipStr = string.Format(tips, count.ToString(), index.ToString());

                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(tipStr);
                    Application.DoEvents();

                    if (this.uploadCaseDocInfo(patient, ref errMsg) < 0)
                    {
                        //回滚
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("上传病历发生错误：" + errMsg);
                        return -1;
                    }

                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("上传病历发生错误：" + ex.Message);
                return -1;
            }

            MessageBox.Show("上传病历成功！");
            this.refreshData();
            return 1;
        }

        /// <summary>
        /// 上传医疗保障基金结算清单
        /// </summary>
        /// <returns></returns>
        protected int UploadJjqd()
        {
            if (this.tabSheet.SelectedIndex != 0)
            {
                MessageBox.Show("请勾选选择要上传结算清单的患者！");
                return -1;
            }

            List<FS.HISFC.Models.RADT.PatientInfo> selectedPatient = this.getSelectedPatient();

            if (selectedPatient == null || selectedPatient.Count == 0)
            {
                MessageBox.Show("请勾选选择要上传结算清单的患者！");
                return -1;
            }

            try
            {
                string errMsg = string.Empty;
                string tips = "共{0}个患者,正在上传第{1}个";

                int count = selectedPatient.Count;
                int index = 0;

                foreach (FS.HISFC.Models.RADT.PatientInfo patient in selectedPatient)
                {
                    index++;

                    string tipStr = string.Format(tips, count.ToString(), index.ToString());

                    //FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(tipStr);
                    Application.DoEvents();

                    if (this.uploadJsqdInfo(patient, ref errMsg) < 0)
                    {
                        //回滚
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("上传医疗保障基金结算清单发生错误：" + errMsg);
                        return -1;
                    }
                    else {
                        //上传成功，保存结果
                        this.localMgr.UpdateUploadDRG(patient.SIMainInfo.RegNo);
                    }
                    //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("上传医疗保障基金结算清单发生错误：" + ex.Message);
                return -1;
            }

            MessageBox.Show("上传成功！");
            this.refreshData();
            return 1;
        }

        /// <summary>
        /// 保存上传
        /// </summary>
        /// <returns></returns>
        protected int SaveUpload()
        {
            if (this.tabSheet.SelectedIndex != 0)
            {
                MessageBox.Show("请选择需要进行保存的患者！");
                return -1;
            }

            List<FS.HISFC.Models.RADT.PatientInfo> selectedPatient = this.getSelectedPatient();

            if (selectedPatient == null || selectedPatient.Count == 0)
            {
                MessageBox.Show("请选择需要进行保存的患者！");
                return -1;
            }

            try
            {
                string errMsg = string.Empty;
                string tips = "共{0}个患者,正在上传第{1}个";

                int count = selectedPatient.Count;
                int index = 0;

                foreach (FS.HISFC.Models.RADT.PatientInfo patient in selectedPatient)
                {
                    index++;

                    string tipStr = string.Format(tips, count.ToString(), index.ToString());

                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(tipStr);
                    Application.DoEvents();
                    if (this.setSiUploadFlag(patient, "1", ref errMsg) < 0)
                    {
                        MessageBox.Show("保存上传状态发生错误：" + errMsg);
                        return -1;
                    }

                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存上传状态发生错误：" + ex.Message);
                return -1;
            }

            MessageBox.Show("保存成功！");
            this.refreshData();
            return 1;
        }

        /// <summary>
        /// 取消上传
        /// </summary>
        /// <returns></returns>
        protected int CancelUpload()
        {
            if (this.tabSheet.SelectedIndex != 1)
            {
                MessageBox.Show("请选择要取消上传的患者信息！");
                return -1;
            }


            List<FS.HISFC.Models.RADT.PatientInfo> selectedPatient = this.getSelectedPatientForCancel();

            if (selectedPatient == null || selectedPatient.Count == 0)
            {
                MessageBox.Show("请选择要取消上传的患者信息！");
                return -1;
            }

            try
            {
                string errMsg = string.Empty;
                string tips = "共{0}个患者,正在取消第{1}个";

                int count = selectedPatient.Count;
                int index = 0;

                foreach (FS.HISFC.Models.RADT.PatientInfo patient in selectedPatient)
                {
                    index++;

                    string tipStr = string.Format(tips, count.ToString(), index.ToString());

                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(tipStr);
                    Application.DoEvents();

                    if (this.dropUploadInfo(patient, ref errMsg) < 0)
                    {
                        //回滚
                        MessageBox.Show("取消上传发生错误：" + errMsg);
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        return -1;
                    }

                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("取消上传发生错误：" + ex.Message);
                return -1;
            }

            MessageBox.Show("取消上传成功！");
            this.refreshData();
            return 1;
        }

        /// <summary>
        /// 获取勾选的患者
        /// </summary>
        /// <returns></returns>
        private List<FS.HISFC.Models.RADT.PatientInfo> getSelectedPatient()
        {
            List<FS.HISFC.Models.RADT.PatientInfo> selectedPatient = new List<FS.HISFC.Models.RADT.PatientInfo>();

            foreach (FarPoint.Win.Spread.Row row in this.fpUnupload_JSQD.Rows)
            {
                if (this.fpUnupload_JSQD.Cells[row.Index, 0].Value != null && (bool)this.fpUnupload_JSQD.Cells[row.Index, (int)EnumUnupdateCol.Select].Value == true)
                {
                    if (row.Tag is FS.HISFC.Models.RADT.PatientInfo)
                    {
                        selectedPatient.Add(row.Tag as FS.HISFC.Models.RADT.PatientInfo);
                    }
                }
            }

            return selectedPatient;
        }

        /// <summary>
        /// 获取需要取消上传的患者
        /// </summary>
        /// <returns></returns>
        private List<FS.HISFC.Models.RADT.PatientInfo> getSelectedPatientForCancel()
        {
            List<FS.HISFC.Models.RADT.PatientInfo> selectedPatient = new List<FS.HISFC.Models.RADT.PatientInfo>();

            FarPoint.Win.Spread.Model.CellRange cr = this.fpUploaded_Sheet1.GetSelection(0);

            if (cr != null)
            {
                FS.HISFC.Models.RADT.PatientInfo patient = (this.fpUploaded_Sheet1.Rows[cr.Row]).Tag as FS.HISFC.Models.RADT.PatientInfo;
                selectedPatient.Add(patient);
            }

            return selectedPatient;
        }

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="siPatientList"></param>
        private void setData(List<FS.HISFC.Models.RADT.PatientInfo> siPatientList)
        {
            foreach (FS.HISFC.Models.RADT.PatientInfo patient in siPatientList)
            {
                if (!patient.SIMainInfo.IsSIUploaded)
                {
                    this.addRowToUnupdateFp(patient);
                }
                else
                {
                    this.addRowToUpdatedFp(patient);
                }

                //if (!patient.SIMainInfo.IsSIUploadedJSQD)
                //{
                //    this.addRowToUnupdateFp_JSQD(patient);
                //}
                //else {
                //    this.addRowToUpdatedFp_JSQD(patient);
                //}
            }
        }

        #region 病案
        /// <summary>
        /// 增加未上传患者
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private int addRowToUnupdateFp(FS.HISFC.Models.RADT.PatientInfo obj)
        {
            int rowIndex = this.fpUnupload_Sheet1.RowCount;
            this.fpUnupload_Sheet1.Rows.Add(this.fpUnupload_Sheet1.RowCount, 1);
            this.fpUnupload_Sheet1.Cells[rowIndex, (int)EnumUnupdateCol.Select].Value = false;
            this.fpUnupload_Sheet1.Cells[rowIndex, (int)EnumUnupdateCol.PatientType].Text = obj.SIMainInfo.MedicalType.Name;
            this.fpUnupload_Sheet1.Cells[rowIndex, (int)EnumUnupdateCol.CardNO].Text = obj.PID.CardNO;
            this.fpUnupload_Sheet1.Cells[rowIndex, (int)EnumUnupdateCol.Name].Text = obj.Name;
            this.fpUnupload_Sheet1.Cells[rowIndex, (int)EnumUnupdateCol.IDCard].Text = obj.IDCard;
            this.fpUnupload_Sheet1.Cells[rowIndex, (int)EnumUnupdateCol.Sex].Text = obj.Sex.ID.ToString() == "F" ? "女" : "男";
            this.fpUnupload_Sheet1.Cells[rowIndex, (int)EnumUnupdateCol.PayKind].Text = obj.Pact.Name;
            this.fpUnupload_Sheet1.Cells[rowIndex, (int)EnumUnupdateCol.InvoiceNO].Text = obj.SIMainInfo.InvoiceNo;
            this.fpUnupload_Sheet1.Cells[rowIndex, (int)EnumUnupdateCol.ZJE].Value = obj.SIMainInfo.Medfee_sumamt;
            this.fpUnupload_Sheet1.Cells[rowIndex, (int)EnumUnupdateCol.TCJE].Value = obj.SIMainInfo.Fund_pay_sumamt;
            this.fpUnupload_Sheet1.Cells[rowIndex, (int)EnumUnupdateCol.ZFJE].Value = obj.SIMainInfo.Psn_part_am;
            this.fpUnupload_Sheet1.Cells[rowIndex, (int)EnumUnupdateCol.JSY].Text = obj.SIMainInfo.OperInfo.ID;
            this.fpUnupload_Sheet1.Cells[rowIndex, (int)EnumUnupdateCol.CYSJ].Text = obj.PVisit.OutTime.ToString();
            this.fpUnupload_Sheet1.Cells[rowIndex, (int)EnumUnupdateCol.JSSJ].Text = obj.SIMainInfo.OperDate.ToString();
            this.fpUnupload_Sheet1.Rows[rowIndex].Tag = obj;
            return 1;
        }

        /// <summary>
        /// 增加上传患者
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>  
        private int addRowToUpdatedFp(FS.HISFC.Models.RADT.PatientInfo obj)
        {
            int rowIndex = this.fpUploaded_Sheet1.RowCount;
            this.fpUploaded_Sheet1.Rows.Add(this.fpUploaded_Sheet1.RowCount, 1);
            this.fpUploaded_Sheet1.Cells[rowIndex, (int)EnumUpdatedCol.PatientType].Text = obj.SIMainInfo.MedicalType.Name;
            this.fpUploaded_Sheet1.Cells[rowIndex, (int)EnumUpdatedCol.CardNO].Text = obj.PID.CardNO;
            this.fpUploaded_Sheet1.Cells[rowIndex, (int)EnumUpdatedCol.Name].Text = obj.Name;
            this.fpUploaded_Sheet1.Cells[rowIndex, (int)EnumUpdatedCol.IDCard].Text = obj.IDCard;
            this.fpUploaded_Sheet1.Cells[rowIndex, (int)EnumUpdatedCol.Sex].Text = obj.Sex.ID.ToString() == "F" ? "女" : "男";
            this.fpUploaded_Sheet1.Cells[rowIndex, (int)EnumUpdatedCol.PayKind].Text = obj.Pact.Name;
            this.fpUploaded_Sheet1.Cells[rowIndex, (int)EnumUpdatedCol.InvoiceNO].Text = obj.SIMainInfo.InvoiceNo;
            this.fpUploaded_Sheet1.Cells[rowIndex, (int)EnumUpdatedCol.ZJE].Value = obj.SIMainInfo.Medfee_sumamt;
            this.fpUploaded_Sheet1.Cells[rowIndex, (int)EnumUpdatedCol.TCJE].Value = obj.SIMainInfo.Fund_pay_sumamt;
            this.fpUploaded_Sheet1.Cells[rowIndex, (int)EnumUpdatedCol.ZFJE].Value = obj.SIMainInfo.Psn_part_am;
            this.fpUploaded_Sheet1.Cells[rowIndex, (int)EnumUpdatedCol.JSY].Text = obj.SIMainInfo.OperInfo.ID;
            this.fpUploaded_Sheet1.Cells[rowIndex, (int)EnumUpdatedCol.CYSJ].Text = obj.PVisit.OutTime.ToString();
            this.fpUploaded_Sheet1.Cells[rowIndex, (int)EnumUpdatedCol.JSSJ].Text = obj.SIMainInfo.OperDate.ToString();
            this.fpUploaded_Sheet1.Rows[rowIndex].Tag = obj;
            return 1;
        }
        #endregion

        #region 结算清单
        private int addRowToUnupdateFp_JSQD(FS.HISFC.Models.RADT.PatientInfo obj)
        {
            int rowIndex = this.fpUnupload_JSQD.RowCount;
            this.fpUnupload_JSQD.Rows.Add(this.fpUnupload_JSQD.RowCount, 1);
            this.fpUnupload_JSQD.Cells[rowIndex, (int)EnumUnupdateCol.Select].Value = false;
            this.fpUnupload_JSQD.Cells[rowIndex, (int)EnumUnupdateCol.PatientType].Text = obj.SIMainInfo.MedicalType.Name;
            this.fpUnupload_JSQD.Cells[rowIndex, (int)EnumUnupdateCol.CardNO].Text = obj.PID.CardNO;
            this.fpUnupload_JSQD.Cells[rowIndex, (int)EnumUnupdateCol.Name].Text = obj.Name;
            this.fpUnupload_JSQD.Cells[rowIndex, (int)EnumUnupdateCol.IDCard].Text = obj.IDCard;
            this.fpUnupload_JSQD.Cells[rowIndex, (int)EnumUnupdateCol.Sex].Text = obj.Sex.ID.ToString() == "F" ? "女" : "男";
            this.fpUnupload_JSQD.Cells[rowIndex, (int)EnumUnupdateCol.PayKind].Text = obj.Pact.Name;
            this.fpUnupload_JSQD.Cells[rowIndex, (int)EnumUnupdateCol.InvoiceNO].Text = obj.SIMainInfo.InvoiceNo;
            this.fpUnupload_JSQD.Cells[rowIndex, (int)EnumUnupdateCol.ZJE].Value = obj.SIMainInfo.Medfee_sumamt;
            this.fpUnupload_JSQD.Cells[rowIndex, (int)EnumUnupdateCol.TCJE].Value = obj.SIMainInfo.Fund_pay_sumamt;
            this.fpUnupload_JSQD.Cells[rowIndex, (int)EnumUnupdateCol.ZFJE].Value = obj.SIMainInfo.Psn_part_am;
            this.fpUnupload_JSQD.Cells[rowIndex, (int)EnumUnupdateCol.JSY].Text = obj.SIMainInfo.OperInfo.ID;
            this.fpUnupload_JSQD.Cells[rowIndex, (int)EnumUnupdateCol.CYSJ].Text = obj.PVisit.OutTime.ToString();
            this.fpUnupload_JSQD.Cells[rowIndex, (int)EnumUnupdateCol.JSSJ].Text = obj.SIMainInfo.OperDate.ToString();
            this.fpUnupload_JSQD.Rows[rowIndex].Tag = obj;
            return 1;
        }

        private int addRowToUpdatedFp_JSQD(FS.HISFC.Models.RADT.PatientInfo obj)
        {
            int rowIndex = this.fpUploaded_JSQD.RowCount;
            this.fpUploaded_JSQD.Rows.Add(this.fpUploaded_JSQD.RowCount, 1);
            this.fpUploaded_JSQD.Cells[rowIndex, (int)EnumUpdatedCol.PatientType].Text = obj.SIMainInfo.MedicalType.Name;
            this.fpUploaded_JSQD.Cells[rowIndex, (int)EnumUpdatedCol.CardNO].Text = obj.PID.CardNO;
            this.fpUploaded_JSQD.Cells[rowIndex, (int)EnumUpdatedCol.Name].Text = obj.Name;
            this.fpUploaded_JSQD.Cells[rowIndex, (int)EnumUpdatedCol.IDCard].Text = obj.IDCard;
            this.fpUploaded_JSQD.Cells[rowIndex, (int)EnumUpdatedCol.Sex].Text = obj.Sex.ID.ToString() == "F" ? "女" : "男";
            this.fpUploaded_JSQD.Cells[rowIndex, (int)EnumUpdatedCol.PayKind].Text = obj.Pact.Name;
            this.fpUploaded_JSQD.Cells[rowIndex, (int)EnumUpdatedCol.InvoiceNO].Text = obj.SIMainInfo.InvoiceNo;
            this.fpUploaded_JSQD.Cells[rowIndex, (int)EnumUpdatedCol.ZJE].Value = obj.SIMainInfo.Medfee_sumamt;
            this.fpUploaded_JSQD.Cells[rowIndex, (int)EnumUpdatedCol.TCJE].Value = obj.SIMainInfo.Fund_pay_sumamt;
            this.fpUploaded_JSQD.Cells[rowIndex, (int)EnumUpdatedCol.ZFJE].Value = obj.SIMainInfo.Psn_part_am;
            this.fpUploaded_JSQD.Cells[rowIndex, (int)EnumUpdatedCol.JSY].Text = obj.SIMainInfo.OperInfo.ID;
            this.fpUploaded_JSQD.Cells[rowIndex, (int)EnumUpdatedCol.CYSJ].Text = obj.PVisit.OutTime.ToString();
            this.fpUploaded_JSQD.Cells[rowIndex, (int)EnumUpdatedCol.JSSJ].Text = obj.SIMainInfo.OperDate.ToString();
            this.fpUploaded_JSQD.Rows[rowIndex].Tag = obj;
            return 1;
        }

        #endregion

        /// <summary>
        /// 清空表格
        /// </summary>
        /// <returns></returns>
        protected int ClearSheet()
        {
            this.fpUnupload_Sheet1.RowCount = 0;
            this.fpUploaded_Sheet1.RowCount = 0;
            this.fpUnupload_JSQD.RowCount = 0;
            this.fpUploaded_JSQD.RowCount = 0;
            return 1;
        }

        /// <summary>
        /// 清空所有信息
        /// </summary>
        /// <returns></returns>
        protected int Clear()
        {
            this.tbInpatientNO.Text = "";
            this.tbName.Text = "";
            this.ClearSheet();
            return 1;
        }

        #region 上传/取消

        /// <summary>
        /// 上传病案信息至医保系统
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private int uploadBrsyInfo(FS.HISFC.Models.RADT.PatientInfo patient, ref string errMsg)
        {
            InPatient4401 inPatient4401 = new InPatient4401();
            Models.Request.RequestGzsiModel4401 requestGzsiModel4401 = new API.GZSI.Models.Request.RequestGzsiModel4401();
            Models.Response.ResponseGzsiModel4401 responseGzsiModel4401 = new Models.Response.ResponseGzsiModel4401();
            requestGzsiModel4401.baseinfo = new API.GZSI.Models.Request.RequestGzsiModel4401.Baseinfo();
            requestGzsiModel4401.diseinfo = new List<API.GZSI.Models.Request.RequestGzsiModel4401.Diseinfo>();
            requestGzsiModel4401.oprninfo = new List<API.GZSI.Models.Request.RequestGzsiModel4401.Oprninfo>();
            requestGzsiModel4401.icuinfo = new List<API.GZSI.Models.Request.RequestGzsiModel4401.Icuinfo>();

            DataTable dtBaseInfo = new DataTable();
            DataTable dtDiseInfo = new DataTable();
            DataTable dtOprnInfo = new DataTable();
            DataTable dtICUInfo = new DataTable();

            //病案首页信息
            if (this.localMgr.GetBasyBaseInfo(patient.SIMainInfo.RegNo, ref dtBaseInfo) < 0)
            {
                errMsg = this.localMgr.Err;
                return -1;
            }
            else if (dtBaseInfo.Rows.Count == 0)
            {
                errMsg = "未找到病案首页信息";
                return -1;
            }

            requestGzsiModel4401.baseinfo = Common.Function.DataRowToObject<API.GZSI.Models.Request.RequestGzsiModel4401.Baseinfo>(dtBaseInfo.Rows[0]);
            //旧医保数据前面补0，补充长度至30
            if (!requestGzsiModel4401.baseinfo.mdtrt_id.StartsWith("H"))
            {
                requestGzsiModel4401.baseinfo.mdtrt_id = requestGzsiModel4401.baseinfo.mdtrt_id.PadLeft(30, '0');
            }

            //诊断信息
            if (this.localMgr.GetBasyDiseInfo(patient.SIMainInfo.RegNo, ref dtDiseInfo) < 0)
            {
                errMsg = this.localMgr.Err;
                return -1;
            }
            foreach (System.Data.DataRow row in dtDiseInfo.Rows)
            {
                API.GZSI.Models.Request.RequestGzsiModel4401.Diseinfo dise = new API.GZSI.Models.Request.RequestGzsiModel4401.Diseinfo();
                dise = Common.Function.DataRowToObject<API.GZSI.Models.Request.RequestGzsiModel4401.Diseinfo>(row);
                requestGzsiModel4401.diseinfo.Add(dise);
            }

            //手术信息
            if (this.localMgr.GetBasyOprnInfo(patient.SIMainInfo.RegNo, ref dtOprnInfo) < 0)
            {
                errMsg = this.localMgr.Err;
                return -1;
            }
            foreach (System.Data.DataRow row in dtOprnInfo.Rows)
            {
                API.GZSI.Models.Request.RequestGzsiModel4401.Oprninfo oprn = new API.GZSI.Models.Request.RequestGzsiModel4401.Oprninfo();
                oprn = Common.Function.DataRowToObject<API.GZSI.Models.Request.RequestGzsiModel4401.Oprninfo>(row);
                requestGzsiModel4401.oprninfo.Add(oprn);
            }

            //ICU信息
            if (this.localMgr.GetBasyICUInfo(patient.SIMainInfo.RegNo, ref dtICUInfo) < 0)
            {
                errMsg = this.localMgr.Err;
                return -1;
            }
            foreach (System.Data.DataRow row in dtICUInfo.Rows)
            {
                API.GZSI.Models.Request.RequestGzsiModel4401.Icuinfo icu = new API.GZSI.Models.Request.RequestGzsiModel4401.Icuinfo();
                icu = Common.Function.DataRowToObject<API.GZSI.Models.Request.RequestGzsiModel4401.Icuinfo>(row);
                requestGzsiModel4401.icuinfo.Add(icu);
            }

            if (requestGzsiModel4401.icuinfo.Count == 0)
            {
                API.GZSI.Models.Request.RequestGzsiModel4401.Icuinfo icu = new API.GZSI.Models.Request.RequestGzsiModel4401.Icuinfo();
                icu.icu_codeid = "";
                icu.inpool_icu_time = "";
                icu.out_icu_time = "";
                icu.medins_orgcode = "";
                icu.nurscare_lv_code = "";
                icu.nurscare_lv_name = "";
                icu.nurscare_days = "";
                icu.back_icu = "";
                icu.vali_flag = "1";
                requestGzsiModel4401.icuinfo.Add(icu);
            }

            returnvalue = inPatient4401.CallService(requestGzsiModel4401, ref responseGzsiModel4401, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue == -1)
            {
                errMsg = inPatient4401.ErrorMsg;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 上传医嘱信息至医保系统
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private int uploadOrderInfo(FS.HISFC.Models.RADT.PatientInfo patient, ref string errMsg)
        {
            InPatient4402 inPatient4402 = new InPatient4402();
            Models.Request.RequestGzsiModel4402 requestGzsiModel4402 = new API.GZSI.Models.Request.RequestGzsiModel4402();
            Models.Response.ResponseGzsiModel4402 responseGzsiModel4402 = new Models.Response.ResponseGzsiModel4402();
            requestGzsiModel4402.data = new List<API.GZSI.Models.Request.RequestGzsiModel4402.Data>();

            DataTable dtOrderInfo = new DataTable();
            //获取医嘱信息
            if (this.localMgr.GetOrerInfo(patient.SIMainInfo.RegNo, ref dtOrderInfo) < 0)
            {
                errMsg = this.localMgr.Err;
                return -1;
            }
            else if (dtOrderInfo.Rows.Count == 0)
            {
                errMsg = "未查询到医嘱信息！";
                return -1;
            }
            foreach (System.Data.DataRow row in dtOrderInfo.Rows)
            {
                API.GZSI.Models.Request.RequestGzsiModel4402.Data order = new API.GZSI.Models.Request.RequestGzsiModel4402.Data();
                order = Common.Function.DataRowToObject<API.GZSI.Models.Request.RequestGzsiModel4402.Data>(row);
                requestGzsiModel4402.data.Add(order);
            }

            returnvalue = inPatient4402.CallService(requestGzsiModel4402, ref responseGzsiModel4402, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue == -1)
            {
                errMsg = inPatient4402.ErrorMsg;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 上传住院病历
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private int uploadCaseDocInfo(FS.HISFC.Models.RADT.PatientInfo patient, ref string errMsg)
        {

            InPatient4701 inPatient4701 = new InPatient4701();
            Models.Request.RequestGzsiModel4701 requestGzsiModel4701 = new API.GZSI.Models.Request.RequestGzsiModel4701();
            Models.Response.ResponseGzsiModel4701 responseGzsiModel4701 = new Models.Response.ResponseGzsiModel4701();

            requestGzsiModel4701.adminfo = new API.GZSI.Models.Request.RequestGzsiModel4701.Adminfo();
            requestGzsiModel4701.diseinfo = new List<API.GZSI.Models.Request.RequestGzsiModel4701.Diseinfo>();
            requestGzsiModel4701.coursrinfo = new API.GZSI.Models.Request.RequestGzsiModel4701.Coursinfo();
            requestGzsiModel4701.oprninfo = new List<API.GZSI.Models.Request.RequestGzsiModel4701.Oprninfo>();
            requestGzsiModel4701.rescinfo = new List<API.GZSI.Models.Request.RequestGzsiModel4701.Rescinfo>();
            requestGzsiModel4701.dieinfo = new API.GZSI.Models.Request.RequestGzsiModel4701.Dieinfo();
            requestGzsiModel4701.dscginfo = new API.GZSI.Models.Request.RequestGzsiModel4701.Dscginfo();

            DataTable dtAdminfo = new DataTable();
            DataTable dtDiseinfo = new DataTable();
            DataTable dtCoursinfo = new DataTable();
            DataTable dtOprninfo = new DataTable();
            DataTable dtRescinfo = new DataTable();
            DataTable dtDieinfo = new DataTable();
            DataTable dtDscginfo = new DataTable();

            //1、入院信息
            if (this.localMgr.GetAdminfo(patient.SIMainInfo.RegNo, ref dtAdminfo) < 0)
            {
                errMsg = this.localMgr.Err;
                return -1;
            }
            else if (dtAdminfo.Rows.Count == 0)
            {
                errMsg = "未找到入院信息";
                return -1;
            }
            requestGzsiModel4701.adminfo = Common.Function.DataRowToObject<API.GZSI.Models.Request.RequestGzsiModel4701.Adminfo>(dtAdminfo.Rows[0]);
            //旧医保数据前面补0，补充长度至30
            if (!requestGzsiModel4701.adminfo.mdtrt_id.StartsWith("H"))
            {
                requestGzsiModel4701.adminfo.mdtrt_id = requestGzsiModel4701.adminfo.mdtrt_id.PadLeft(30, '0');
            }

            //2、诊断信息
            if (this.localMgr.GetDiseinfo(patient.SIMainInfo.RegNo, ref dtDiseinfo) < 0)
            {
                errMsg = this.localMgr.Err;
                return -1;
            }
            else if (dtDiseinfo.Rows.Count > 0)
            {
                foreach (System.Data.DataRow row in dtDiseinfo.Rows)
                {
                    API.GZSI.Models.Request.RequestGzsiModel4701.Diseinfo dise = new API.GZSI.Models.Request.RequestGzsiModel4701.Diseinfo();
                    dise = Common.Function.DataRowToObject<API.GZSI.Models.Request.RequestGzsiModel4701.Diseinfo>(row);
                    requestGzsiModel4701.diseinfo.Add(dise);
                }
            }
            else
            {
                errMsg = "未找到诊断信息";
                return -1;
            }

            //3、病程信息
            if (this.localMgr.GetCoursinfo(patient.SIMainInfo.RegNo, ref dtCoursinfo) < 0)
            {
                MessageBox.Show(this.localMgr.Err);
                return -1;
            }
            else if (dtCoursinfo.Rows.Count == 0)
            {
                errMsg = "未找到病程信息";
                return -1;
            }
            requestGzsiModel4701.coursrinfo = Common.Function.DataRowToObject<API.GZSI.Models.Request.RequestGzsiModel4701.Coursinfo>(dtCoursinfo.Rows[0]);

            //4、手术信息
            if (this.localMgr.GetOprninfo(patient.SIMainInfo.RegNo, ref dtOprninfo) < 0)
            {
                errMsg = this.localMgr.Err;
                return -1;
            }
            else if (dtOprninfo.Rows.Count > 0)
            {
                foreach (System.Data.DataRow row in dtOprninfo.Rows)
                {
                    API.GZSI.Models.Request.RequestGzsiModel4701.Oprninfo oprn = new API.GZSI.Models.Request.RequestGzsiModel4701.Oprninfo();
                    oprn = Common.Function.DataRowToObject<API.GZSI.Models.Request.RequestGzsiModel4701.Oprninfo>(row);
                    requestGzsiModel4701.oprninfo.Add(oprn);
                }
            }

            //抢救信息
            if (this.localMgr.GetRescinfo(patient.SIMainInfo.RegNo, ref dtRescinfo) < 0)
            {
                errMsg = this.localMgr.Err;
                return -1;
            }
            else if (dtRescinfo.Rows.Count > 0)
            {
                foreach (System.Data.DataRow row in dtRescinfo.Rows)
                {
                    API.GZSI.Models.Request.RequestGzsiModel4701.Rescinfo resc = new API.GZSI.Models.Request.RequestGzsiModel4701.Rescinfo();
                    resc = Common.Function.DataRowToObject<API.GZSI.Models.Request.RequestGzsiModel4701.Rescinfo>(row);
                    requestGzsiModel4701.rescinfo.Add(resc);
                }
            }

            if (requestGzsiModel4701.rescinfo.Count == 0)
            {
                API.GZSI.Models.Request.RequestGzsiModel4701.Rescinfo resc = new API.GZSI.Models.Request.RequestGzsiModel4701.Rescinfo();
                resc.dept = requestGzsiModel4701.adminfo.dept_code;
                resc.dept_name = requestGzsiModel4701.adminfo.dept_name;
                resc.bedno = requestGzsiModel4701.adminfo.bedno;
                resc.diag_name = "-";
                resc.diag_code = "-";
                resc.dise_name = "-";
                resc.dise_code = "-";
                resc.wardarea_name = "-";
                resc.cond_chg = "-";
                resc.resc_mes = "-";
                resc.oprn_oprt_code = "-";
                resc.oprn_oprt_name = "-";
                resc.oprn_oper_part = "-";
                resc.itvt_name = "-";
                resc.oprt_mtd = "-";
                resc.oprt_cnt = "0";
                resc.resc_begntime = "0001-01-01";
                resc.resc_endtime = "0001-01-01";
                resc.dise_item_name = "-";
                resc.dise_ccls = "-";
                resc.dise_ccls_qunt = "0.0000";
                resc.dise_ccls_code = "0";
                resc.mnan = "-";
                resc.resc_psn_list = "-";
                resc.proftechttl_code = "0";
                resc.doc_code = "-";
                resc.dr_name = "-";
                resc.vali_flag = "0";
                requestGzsiModel4701.rescinfo.Add(resc);
            }

            //死亡信息
            if (this.localMgr.GetDiseinfo(patient.SIMainInfo.RegNo, ref dtDieinfo) < 0)
            {
                errMsg = this.localMgr.Err;
                return -1;
            }
            requestGzsiModel4701.dieinfo = Common.Function.DataRowToObject<API.GZSI.Models.Request.RequestGzsiModel4701.Dieinfo>(dtDieinfo.Rows[0]);

            if(false)//if (string.IsNullOrEmpty(requestGzsiModel4701.dieinfo.die_diag_code))
            {
                requestGzsiModel4701.dieinfo.adm_dise = "-";
                requestGzsiModel4701.dieinfo.adm_info = "-";
                requestGzsiModel4701.dieinfo.adm_time = requestGzsiModel4701.adminfo.adm_time;
                requestGzsiModel4701.dieinfo.agre_corp_dset = "-";
                requestGzsiModel4701.dieinfo.atddr_code = requestGzsiModel4701.adminfo.atddr_code;
                requestGzsiModel4701.dieinfo.atddr_name = requestGzsiModel4701.adminfo.atddr_name;
                requestGzsiModel4701.dieinfo.bedno = requestGzsiModel4701.adminfo.bedno;
                requestGzsiModel4701.dieinfo.chfdr_name = requestGzsiModel4701.adminfo.chfdr_name;
                requestGzsiModel4701.dieinfo.dept = requestGzsiModel4701.adminfo.dept_code;
                requestGzsiModel4701.dieinfo.dept_name = requestGzsiModel4701.adminfo.dept_name;
                requestGzsiModel4701.dieinfo.die_diag_code = "-";
                requestGzsiModel4701.dieinfo.die_dise_name = "-";
                requestGzsiModel4701.dieinfo.die_drt_rea = "-";
                requestGzsiModel4701.dieinfo.die_drt_rea_code = "-";
                requestGzsiModel4701.dieinfo.die_time = "0001-01-01";
                requestGzsiModel4701.dieinfo.ipdr_name = requestGzsiModel4701.adminfo.ipdr_name;
                requestGzsiModel4701.dieinfo.sign_time = "0001-01-01";
                requestGzsiModel4701.dieinfo.trt_proc_dscr = "-";
                requestGzsiModel4701.dieinfo.vali_flag = "0";
            }

            //出院小结
            if (this.localMgr.GetDscginfo(patient.SIMainInfo.RegNo, ref dtDscginfo) < 0)
            {
                errMsg = this.localMgr.Err;
                return -1;
            }
            else if (dtDscginfo.Rows.Count == 0)
            {
                errMsg = "未找到出院小结";
                return -1;
            }
            requestGzsiModel4701.dscginfo = Common.Function.DataRowToObject<API.GZSI.Models.Request.RequestGzsiModel4701.Dscginfo>(dtDscginfo.Rows[0]);

            returnvalue = inPatient4701.CallService(requestGzsiModel4701, ref responseGzsiModel4701, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue == -1)
            {
                errMsg = inPatient4701.ErrorMsg;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 上传医疗保障基金结算清单
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private int uploadJsqdInfo(FS.HISFC.Models.RADT.PatientInfo patient, ref string errMsg)
        {
            InPatient4101 inPatient4101 = new InPatient4101();
            Models.Request.RequestGzsiModel4101 requestGzsiModel4101 = new API.GZSI.Models.Request.RequestGzsiModel4101();
            Models.Response.ResponseGzsiModel4101 responseGzsiModel4101 = new Models.Response.ResponseGzsiModel4101();
            requestGzsiModel4101.setlinfo = new API.GZSI.Models.Request.RequestGzsiModel4101.Setlinfo();
            requestGzsiModel4101.payinfo = new List<API.GZSI.Models.Request.RequestGzsiModel4101.Payinfo>();
            requestGzsiModel4101.opspdiseinfo = new List<API.GZSI.Models.Request.RequestGzsiModel4101.Opspdiseinfo>();
            requestGzsiModel4101.diseinfo = new List<API.GZSI.Models.Request.RequestGzsiModel4101.Diseinfo>();
            requestGzsiModel4101.iteminfo = new List<API.GZSI.Models.Request.RequestGzsiModel4101.Iteminfo>();
            requestGzsiModel4101.oprninfo = new List<API.GZSI.Models.Request.RequestGzsiModel4101.Oprninfo>();
            requestGzsiModel4101.icuinfo = new List<API.GZSI.Models.Request.RequestGzsiModel4101.Icuinfo>();

            DataTable dtSetlinfo = new DataTable();
            DataTable dtPayinfo = new DataTable();
            DataTable dtOpspdiseinfo = new DataTable();
            DataTable dtDiseinfo = new DataTable();
            DataTable dtIteminfo = new DataTable();
            DataTable dtOprninfo = new DataTable();
            DataTable dtIcuinfo = new DataTable();

            //结算信息
            if (this.localMgr.GetJsqdSetlInfo(patient.SIMainInfo.RegNo, ref dtSetlinfo) < 0)
            {
                errMsg = this.localMgr.Err;
                return -1;
            }
            else if (dtSetlinfo.Rows.Count == 0)
            {
                errMsg = "未找到结算信息";
                return -1;
            }

            requestGzsiModel4101.setlinfo = Common.Function.DataRowToObject<API.GZSI.Models.Request.RequestGzsiModel4101.Setlinfo>(dtSetlinfo.Rows[0]);
            //旧医保数据前面补0，补充长度至30
            if (!requestGzsiModel4101.setlinfo.mdtrt_id.StartsWith("H")) {
                requestGzsiModel4101.setlinfo.mdtrt_id = requestGzsiModel4101.setlinfo.mdtrt_id.PadLeft(30, '0');
                requestGzsiModel4101.setlinfo.setl_id = requestGzsiModel4101.setlinfo.setl_id.PadLeft(30, '0');
            }


            //基金信息
            if (this.localMgr.GetJsqdPayInfo(patient.SIMainInfo.RegNo, ref dtPayinfo) < 0)
            {
                errMsg = this.localMgr.Err;
                return -1;
            }
            foreach (System.Data.DataRow row in dtPayinfo.Rows)
            {
                API.GZSI.Models.Request.RequestGzsiModel4101.Payinfo pay = new API.GZSI.Models.Request.RequestGzsiModel4101.Payinfo();
                pay = Common.Function.DataRowToObject<API.GZSI.Models.Request.RequestGzsiModel4101.Payinfo>(row);
                requestGzsiModel4101.payinfo.Add(pay);
            }

            //门诊慢特病诊断信息
            if (this.localMgr.GetJsqdOpspDiseinfo(patient.SIMainInfo.RegNo, ref dtOpspdiseinfo) < 0)
            {
                errMsg = this.localMgr.Err;
                return -1;
            }
            foreach (System.Data.DataRow row in dtOpspdiseinfo.Rows)
            {
                API.GZSI.Models.Request.RequestGzsiModel4101.Opspdiseinfo opspDiseinfo = new API.GZSI.Models.Request.RequestGzsiModel4101.Opspdiseinfo();
                opspDiseinfo = Common.Function.DataRowToObject<API.GZSI.Models.Request.RequestGzsiModel4101.Opspdiseinfo>(row);
                requestGzsiModel4101.opspdiseinfo.Add(opspDiseinfo);
            }

            //住院诊断信息
            if (this.localMgr.GetJsqdDiseInfo(patient.SIMainInfo.RegNo, ref dtDiseinfo) < 0)
            {
                errMsg = this.localMgr.Err;
                return -1;
            }
            foreach (System.Data.DataRow row in dtDiseinfo.Rows)
            {
                API.GZSI.Models.Request.RequestGzsiModel4101.Diseinfo dise = new API.GZSI.Models.Request.RequestGzsiModel4101.Diseinfo();
                dise = Common.Function.DataRowToObject<API.GZSI.Models.Request.RequestGzsiModel4101.Diseinfo>(row);
                requestGzsiModel4101.diseinfo.Add(dise);
            }

            //收费项目信息
            if (this.localMgr.GetJsqdIteminfo(patient.SIMainInfo.RegNo, ref dtIteminfo) < 0)
            {
                errMsg = this.localMgr.Err;
                return -1;
            }
            foreach (System.Data.DataRow row in dtIteminfo.Rows)
            {
                API.GZSI.Models.Request.RequestGzsiModel4101.Iteminfo item = new API.GZSI.Models.Request.RequestGzsiModel4101.Iteminfo();
                item = Common.Function.DataRowToObject<API.GZSI.Models.Request.RequestGzsiModel4101.Iteminfo>(row);
                requestGzsiModel4101.iteminfo.Add(item);
            }

            //本地为空，调用接口
            if (dtIteminfo == null || dtIteminfo.Rows.Count <= 0) {
                CommonService5260 CommonService5260 = new CommonService5260();
                Models.Request.RequestGzsiModel5260 RequestGzsiModel5260 = new Models.Request.RequestGzsiModel5260();
                Models.Response.ResponseGzsiModel5260 ResponseGzsiModel5260 = new Models.Response.ResponseGzsiModel5260();
                Models.Request.RequestGzsiModel5260.Data Data5260 = new Models.Request.RequestGzsiModel5260.Data();
                Data5260.psn_no = requestGzsiModel4101.setlinfo.mdtrt_id.StartsWith("H") ? patient.SIMainInfo.Psn_no : "4401000000" + patient.SIMainInfo.Psn_no;
                Data5260.setl_id = requestGzsiModel4101.setlinfo.setl_id;
                Data5260.mdtrt_id = requestGzsiModel4101.setlinfo.mdtrt_id;
                RequestGzsiModel5260.data = Data5260;
                if (CommonService5260.CallService(RequestGzsiModel5260, ref ResponseGzsiModel5260) < 0)
                {
                    MessageBox.Show(CommonService5260.ErrorMsg);
                    return -1;
                }

                requestGzsiModel4101.iteminfo = new List<Models.Request.RequestGzsiModel4101.Iteminfo>();
                foreach (Models.Response.ResponseGzsiModel5260.Setldetail setldetail in ResponseGzsiModel5260.output.setldetail) {
                    API.GZSI.Models.Request.RequestGzsiModel4101.Iteminfo item = new API.GZSI.Models.Request.RequestGzsiModel4101.Iteminfo();
                    item.med_chrgitm = setldetail.med_chrgitm_type;
                    item.amt = setldetail.item_sumamt;
                    item.claa_sumfee = setldetail.item_claa_amt;
                    item.clab_amt = setldetail.item_clab_amt;
                    item.oth_amt = setldetail.item_oth_amt;
                    item.fulamt_ownpay_amt = (Convert.ToDecimal(setldetail.item_ownpay_amt) + Convert.ToDecimal(setldetail.overlmt_selfpay)).ToString();
                    requestGzsiModel4101.iteminfo.Add(item);
                }

            }


            //手术信息
            if (this.localMgr.GetJsqdOprnInfo(patient.SIMainInfo.RegNo, ref dtOprninfo) < 0)
            {
                errMsg = this.localMgr.Err;
                return -1;
            }
            foreach (System.Data.DataRow row in dtOprninfo.Rows)
            {
                API.GZSI.Models.Request.RequestGzsiModel4101.Oprninfo oprn = new API.GZSI.Models.Request.RequestGzsiModel4101.Oprninfo();
                oprn = Common.Function.DataRowToObject<API.GZSI.Models.Request.RequestGzsiModel4101.Oprninfo>(row);
                requestGzsiModel4101.oprninfo.Add(oprn);
            }

            //重症监护信息
            if (this.localMgr.GetJsqdIcuInfo(patient.SIMainInfo.RegNo, ref dtIcuinfo) < 0)
            {
                errMsg = this.localMgr.Err;
                return -1;
            }
            foreach (System.Data.DataRow row in dtIcuinfo.Rows)
            {
                API.GZSI.Models.Request.RequestGzsiModel4101.Icuinfo icu = new API.GZSI.Models.Request.RequestGzsiModel4101.Icuinfo();
                icu = Common.Function.DataRowToObject<API.GZSI.Models.Request.RequestGzsiModel4101.Icuinfo>(row);
                requestGzsiModel4101.icuinfo.Add(icu);
            }


            returnvalue = inPatient4101.CallService(requestGzsiModel4101, ref responseGzsiModel4101, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue < 0)
            {
                errMsg = inPatient4101.ErrorMsg;
                return -1;
            }
            API.GZSI.Print.uc4101.ucPrint4101 ucPrint = new API.GZSI.Print.uc4101.ucPrint4101();
            ucPrint.RequestModel = requestGzsiModel4101;
            ucPrint.PatientInfo = patient;
            ucPrint.SetValue();
            ucPrint.Print();

            return 1;
        }

        /// <summary>
        /// 删除各类医保系统的上传信息
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private int dropUploadInfo(FS.HISFC.Models.RADT.PatientInfo patient, ref string errMsg)
        {
            if (this.setSiUploadFlag(patient, "0", ref errMsg) < 0)
            {
                return -1;
            }

            return 1;
        }

        #endregion

        #region 辅助函数

        /// <summary>
        /// 设置医保登记表上传标识
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private int setSiUploadFlag(FS.HISFC.Models.RADT.PatientInfo patient, string uploadFlag, ref string errMsg)
        {
            int rtn = this.inpatientManager.setSiUploadFlag(patient.ID, uploadFlag);

            if (rtn < 0)
            {
                errMsg = "更新患者上传标识出错,患者姓名：" + patient.Name + ";\r\n";
                errMsg += this.inpatientManager.Err;
                return -1;
            }

            if (rtn == 0)
            {
                errMsg = "更新患者上传标识出错,患者姓名：" + patient.Name + ";\r\n";
                errMsg += "未更新到有效记录";
                return -1;
            }

            return 1;
        }

        #endregion

        #region 列枚举

        /// <summary>
        /// 未上传列表
        /// </summary>
        private enum EnumUnupdateCol
        {
            /// <summary>
            /// 选择
            /// </summary>
            Select = 0,

            /// <summary>
            /// 类别
            /// </summary>
            PatientType = 1,

            /// <summary>
            /// 病历号
            /// </summary>
            CardNO = 2,

            /// <summary>
            /// 姓名
            /// </summary>
            Name = 3,

            /// <summary>
            /// 身份证号
            /// </summary>
            IDCard = 4,

            /// <summary>
            /// 性别
            /// </summary>
            Sex = 5,

            /// <summary>
            /// 结算类型
            /// </summary>
            PayKind = 6,

            /// <summary>
            /// 发票号
            /// </summary>
            InvoiceNO = 7,

            /// <summary>
            /// 总金额
            /// </summary>
            ZJE = 8,

            /// <summary>
            /// 统筹金额
            /// </summary>
            TCJE = 9,

            /// <summary>
            /// 自费金额
            /// </summary>
            ZFJE = 10,

            /// <summary>
            /// 结算员
            /// </summary>
            JSY = 11,

            /// <summary>
            /// 出院时间
            /// </summary>
            CYSJ = 12,

            /// <summary>
            /// 结算时间
            /// </summary>
            JSSJ = 13

        }

        /// <summary>
        /// 已上传列表
        /// </summary>
        private enum EnumUpdatedCol
        {

            /// <summary>
            /// 类别
            /// </summary>
            PatientType = 0,

            /// <summary>
            /// 病历号
            /// </summary>
            CardNO = 1,

            /// <summary>
            /// 姓名
            /// </summary>
            Name = 2,

            /// <summary>
            /// 身份证号
            /// </summary>
            IDCard = 3,

            /// <summary>
            /// 性别
            /// </summary>
            Sex = 4,

            /// <summary>
            /// 结算类型
            /// </summary>
            PayKind = 5,

            /// <summary>
            /// 发票号
            /// </summary>
            InvoiceNO = 6,

            /// <summary>
            /// 总金额
            /// </summary>
            ZJE = 7,

            /// <summary>
            /// 统筹金额
            /// </summary>
            TCJE = 8,

            /// <summary>
            /// 自费金额
            /// </summary>
            ZFJE = 9,

            /// <summary>
            /// 结算员
            /// </summary>
            JSY = 10,

            /// <summary>
            /// 出院时间
            /// </summary>
            CYSJ = 11,

            /// <summary>
            /// 结算时间
            /// </summary>
            JSSJ = 12
        }

        #endregion 
    }
}
