using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace IBorn.SI.MedicalInsurance.InPatient
{
    /// <summary>
    /// 创建日期：2019-11-25
    /// 创建人：Giiber
    /// 功能说明：医保病案上传功能
    /// </summary>
    public partial class ucUploadCaseInfo : FS.FrameWork.WinForms.Controls.ucBaseControl
    {

        #region 业务管理类

        /// <summary>
        /// 住院患者信息管理类
        /// </summary>
        private FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();

        #endregion 

        /// <summary>
        /// 初始化
        /// </summary>
        public ucUploadCaseInfo()
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
            this.fpUnupload.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(fpUnupload_CellClick);
            this.chkALL.CheckedChanged += new EventHandler(chkALL_CheckedChanged);
        }

        /// <summary>
        /// 解绑事件
        /// </summary>
        private void UnbindEvents()
        {
            this.chkALL.CheckedChanged -= new EventHandler(chkALL_CheckedChanged);
            this.fpUnupload.CellClick -= new FarPoint.Win.Spread.CellClickEventHandler(fpUnupload_CellClick);
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
            toolBarService.AddToolButton("确定上传", "确定上传", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolBarService.AddToolButton("取消上传", "取消上传", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);

            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "确定上传":
                    this.OnSave(null, null);
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
                foreach(FarPoint.Win.Spread.Row row in this.fpUnupload_Sheet1.Rows)
                {
                    this.fpUnupload_Sheet1.Cells[row.Index, (int)EnumUnupdateCol.Select].Value = false;
                }

                this.fpUnupload_Sheet1.Cells[e.Row, (int)EnumUnupdateCol.Select].Value = true;
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
        }

        /// <summary>
        /// 查询患者列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
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

            List<FS.HISFC.Models.RADT.PatientInfo> siPatientList = this.inpatientManager.GetInPatientByDatePatientNO(begin, end.AddDays(1), patientNO, name);

            if (siPatientList == null)
            {
                MessageBox.Show("查询患者里列表失败：" + this.inpatientManager.Err);
                return -1;
            }
            else
            {
                this.setData(siPatientList);
            }

            return base.OnQuery(sender, neuObject);
        }

        /// <summary>
        /// 保存上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            if (this.tabSheet.SelectedIndex != 0)
            {
                MessageBox.Show("请选择待上传列表进行保存！");
                return -1;
            }

            List<FS.HISFC.Models.RADT.PatientInfo> selectedPatient = this.getSelectedPatient();

            if (selectedPatient == null || selectedPatient.Count == 0)
            {
                MessageBox.Show("请勾选选择要上传的患者信息！");
                return -1;
            }

            try
            {
                IBorn.SI.GuangZhou.Base.SIDataBase.Open(IBorn.SI.GuangZhou.Base.SIDataBase.SIServer);


                IBorn.SI.GuangZhou.Base.SIDataBase.BeginTranscation();
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

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

                    if (this.updateInfo(patient, ref errMsg) < 0)
                    {
                        //回滚
                        MessageBox.Show("上传发生错误：" + errMsg);
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        IBorn.SI.GuangZhou.Base.SIDataBase.Rollback();
                        return -1;
                    }

                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                }

                FS.FrameWork.Management.PublicTrans.Commit();
                IBorn.SI.GuangZhou.Base.SIDataBase.Commit();
            }
            catch(Exception ex)
            {
                MessageBox.Show("上传发生错误：" + ex.Message);
                FS.FrameWork.Management.PublicTrans.RollBack();
                IBorn.SI.GuangZhou.Base.SIDataBase.Rollback();
                return -1;
            }
            finally
            {
                IBorn.SI.GuangZhou.Base.SIDataBase.Close();
            }

            MessageBox.Show("上传信息成功！");
            this.OnQuery(null,null);
            return base.OnSave(sender, neuObject);
        }

        /// <summary>
        /// 取消上传
        /// </summary>
        /// <returns></returns>
        protected int CancelUpload()
        {
            if (this.tabSheet.SelectedIndex != 1)
            {
                MessageBox.Show("请选择已上传列表进行【取消上传】操作！");
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
                IBorn.SI.GuangZhou.Base.SIDataBase.Open(IBorn.SI.GuangZhou.Base.SIDataBase.SIServer);


                IBorn.SI.GuangZhou.Base.SIDataBase.BeginTranscation();
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

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

                    if (this.deleteUpdateInfo(patient, ref errMsg) < 0)
                    {
                        //回滚
                        MessageBox.Show("删除上传信息发生错误：" + errMsg);
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        IBorn.SI.GuangZhou.Base.SIDataBase.Rollback();
                        return -1;
                    }

                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                }

                FS.FrameWork.Management.PublicTrans.Commit();
                IBorn.SI.GuangZhou.Base.SIDataBase.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除上传信息发生错误：" + ex.Message);
                FS.FrameWork.Management.PublicTrans.RollBack();
                IBorn.SI.GuangZhou.Base.SIDataBase.Rollback();
                return -1;
            }
            finally
            {
                IBorn.SI.GuangZhou.Base.SIDataBase.Close();
            }

            MessageBox.Show("取消上传成功！");
            this.OnQuery(null, null);
            return 1;
        }

        /// <summary>
        /// 获取勾选的患者
        /// </summary>
        /// <returns></returns>
        private List<FS.HISFC.Models.RADT.PatientInfo> getSelectedPatient()
        {
            List<FS.HISFC.Models.RADT.PatientInfo> selectedPatient = new List<FS.HISFC.Models.RADT.PatientInfo>();

            foreach (FarPoint.Win.Spread.Row row in this.fpUnupload_Sheet1.Rows)
            {
                if (this.fpUnupload_Sheet1.Cells[row.Index, 0].Value != null && (bool)this.fpUnupload_Sheet1.Cells[row.Index, (int)EnumUnupdateCol.Select].Value == true)
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
        /// 清空表格
        /// </summary>
        /// <returns></returns>
        protected int ClearSheet()
        {
            this.fpUnupload_Sheet1.RowCount = 0;
            this.fpUploaded_Sheet1.RowCount = 0;
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

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="siPatientList"></param>
        private void setData(List<FS.HISFC.Models.RADT.PatientInfo> siPatientList)
        {
            foreach(FS.HISFC.Models.RADT.PatientInfo patient in siPatientList)
            {
                if (!patient.SIMainInfo.IsSIUploaded)
                {
                    this.addRowToUnupdateFp(patient);
                }
                else
                {
                    this.addRowToUpdatedFp(patient);
                }
            }
        }

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
            this.fpUnupload_Sheet1.Cells[rowIndex, (int)EnumUnupdateCol.ZJE].Value = 0;
            this.fpUnupload_Sheet1.Cells[rowIndex, (int)EnumUnupdateCol.TCJE].Value = 0;
            this.fpUnupload_Sheet1.Cells[rowIndex, (int)EnumUnupdateCol.ZFJE].Value = 0;
            this.fpUnupload_Sheet1.Cells[rowIndex, (int)EnumUnupdateCol.JSY].Text = obj.SIMainInfo.OperInfo.ID;
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
            this.fpUploaded_Sheet1.Cells[rowIndex, (int)EnumUpdatedCol.ZJE].Value = 0;
            this.fpUploaded_Sheet1.Cells[rowIndex, (int)EnumUpdatedCol.TCJE].Value = 0;
            this.fpUploaded_Sheet1.Cells[rowIndex, (int)EnumUpdatedCol.ZFJE].Value = 0;
            this.fpUploaded_Sheet1.Cells[rowIndex, (int)EnumUpdatedCol.JSY].Text = obj.SIMainInfo.OperInfo.ID;
            this.fpUploaded_Sheet1.Cells[rowIndex, (int)EnumUpdatedCol.JSSJ].Text = obj.SIMainInfo.OperDate.ToString();
            this.fpUploaded_Sheet1.Rows[rowIndex].Tag = obj;
            return 1;
        }

        #region 上传信息至医保系统

        /// <summary>
        /// 上传各类信息至医保系统
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private int updateInfo(FS.HISFC.Models.RADT.PatientInfo patient, ref string errMsg)
        {
            if (this.uploadBasybrxx(patient, ref errMsg) < 0)
            {
                return -1;
            }

            if (this.uploadBasyzdxx(patient, ref errMsg) < 0)
            {
                return -1;
            }

            if (this.uploadBasyzdxxmx(patient, ref errMsg) < 0)
            {
                return -1;
            }

            if (this.uploadBasyssxx(patient, ref errMsg) < 0)
            {
                return -1;
            }

            if (this.uploadBasyssxxmx(patient, ref errMsg) < 0)
            {
                return -1;
            }

            if (this.uploadBasyfmxx(patient, ref errMsg) < 0)
            {
                return -1;
            }

            if (this.uploadBasyfmxxmx(patient, ref errMsg) < 0)
            {
                return -1;
            }

            if (this.uploadCyxj(patient, ref errMsg) < 0)
            {
                return -1;
            }

            if (this.setSiUploadFlag(patient, "1", ref errMsg) < 0)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 上传病案首页信息
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private int uploadBasybrxx(FS.HISFC.Models.RADT.PatientInfo patient, ref string errMsg)
        {
            //查询语句
            string selectSql = @"select akb020, akb026, ykc700, yzy201, pka435, pka439, bz1, bz2, bz3, drbz from his_snyd_basybrxx_view
                                  where ykc700 = '{0}'";

            //插入语句
            string insertSql = @"INSERT INTO HIS_SNYD_BASYBRXX (AKB020 ,AKB026 ,YKC700 ,YZY201 ,PKA435 ,PKA439 ,BZ1   ,BZ2   ,BZ3   ,DRBZ)
                                                        VALUES ('{0}'  ,'{1}'  ,'{2}'  ,{3}    ,'{4}'  ,'{5}'  ,'{6}' ,'{7}' ,'{8}' ,{9})";

            string select = string.Format(selectSql, patient.SIMainInfo.RegNo);

            string errMessage = string.Empty;
            int rtn = this.selectInsert(select, insertSql, ref errMessage);

            if (rtn < 0)
            {
                errMsg = "上传患者病案首页信息失败！患者姓名：" + patient.Name + ";\r\n";
                errMsg += errMessage;
                return -1;
            }

            if (rtn == 0)
            {
                errMsg = "上传患者病案首页信息失败！患者姓名：" + patient.Name + ";\r\n";
                errMsg += "未找到该患者的病案首页信息，可能是病案系统还未上传该患者的病案！";
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 上传病案首页诊断信息
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private int uploadBasyzdxx(FS.HISFC.Models.RADT.PatientInfo patient, ref string errMsg)
        {
            //查询语句
            string selectSql = @"select akb020, aab299, yab600, akb026, akb021, ykc700, aab301, yab060, aac002, aac043, aac044, yzy003, bz1, bz2, bz3, drbz 
                                   from his_snyd_basyzdxx_view
                                  where ykc700 = '{0}'";

            //插入语句
            string insertSql = @"INSERT INTO HIS_SNYD_BASYZDXX (AKB020, AAB299, YAB600, AKB026, AKB021,
                                                                YKC700, AAB301, YAB060, AAC002, AAC043,
                                                                AAC044, YZY003, BZ1,    BZ2,    BZ3,
                                                                DRBZ)
                                                        VALUES ('{0}', '{1}', '{2}', '{3}', '{4}',
                                                                '{5}', '{6}', '{7}', '{8}', '{9}',
                                                                '{10}', {11}, '{12}', '{13}', '{14}',
                                                                {15})";

            string select = string.Format(selectSql, patient.SIMainInfo.RegNo);

            string errMessage = string.Empty;
            int rtn = this.selectInsert(select, insertSql, ref errMessage);

            if (rtn < 0)
            {
                errMsg = "上传患者诊断信息失败！患者姓名：" + patient.Name + ";\r\n";
                errMsg += errMessage;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 上传病案首页诊断信息明细
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private int uploadBasyzdxxmx(FS.HISFC.Models.RADT.PatientInfo patient, ref string errMsg)
        {
            //查询语句
            string selectSql = @"select akb020, ykc700, yzy201, yzy202, akc185, akc196, yzy205, yzy206, bz1, bz2, bz3, drbz 
                                   from his_snyd_basyzdxxmx_view
                                  where ykc700 = '{0}'";

            //插入语句
            string insertSql = @"INSERT INTO HIS_SNYD_BASYZDXXMX (AKB020 ,YKC700 ,YZY201 ,YZY202 ,AKC185
                                                                 ,AKC196 ,YZY205 ,YZY206 ,BZ1 ,BZ2
                                                                 ,BZ3 ,DRBZ)
                                                          VALUES ('{0}' ,'{1}' ,{2} ,'{3}' ,'{4}'
                                                                 ,'{5}' ,'{6}' ,'{7}' ,'{8}' ,'{9}'
                                                                 ,'{10}' ,{11})";

            string select = string.Format(selectSql, patient.SIMainInfo.RegNo);

            string errMessage = string.Empty;
            int rtn = this.selectInsert(select, insertSql, ref errMessage);

            if (rtn < 0)
            {
                errMsg = "上传患者诊断明细信息失败！患者姓名：" + patient.Name + ";\r\n";
                errMsg += errMessage;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 上传病案首页手术信息
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private int uploadBasyssxx(FS.HISFC.Models.RADT.PatientInfo patient, ref string errMsg)
        {
            //查询语句
            string selectSql = @"select akb020, aab299, yab600, akb026, akb021, ykc700, aab301, yab060, aac002, aac043, aac044, bz1, bz2, bz3, drbz 
                                   from his_snyd_basyssxx_view
                                  where ykc700 = '{0}'";

            //插入语句
            string insertSql = @"INSERT INTO HIS_SNYD_BASYSSXX (AKB020 ,AAB299 ,YAB600 ,AKB026 ,AKB021 
                                                               ,YKC700 ,AAB301 ,YAB060 ,AAC002 ,AAC043
                                                               ,AAC044 ,BZ1 ,BZ2 ,BZ3 ,DRBZ)
                                                        VALUES ('{0}' ,'{1}' ,'{2}' ,'{3}' ,'{4}'
                                                               ,'{5}' ,'{6}' ,'{7}' ,'{8}' ,'{9}'
                                                               ,'{10}' ,'{11}' ,'{12}' ,'{13}' ,{14})";

            string select = string.Format(selectSql, patient.SIMainInfo.RegNo);

            string errMessage = string.Empty;
            int rtn = this.selectInsert(select, insertSql, ref errMessage);

            if (rtn < 0)
            {
                errMsg = "上传患者病案首页手术信息失败！患者姓名：" + patient.Name + ";\r\n";
                errMsg += errMessage;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 上传病案首页手术信息明细
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private int uploadBasyssxxmx(FS.HISFC.Models.RADT.PatientInfo patient, ref string errMsg)
        {            
            //查询语句
            string selectSql = @"select akb020, ykc700, yzy201, yzy207, yzy208, yzy209, yzy210, yzy211, yzy212, yzy213, yzy214, 
                                        yzy215, yzy216, yzy217, yzy218, yzy219, yzy220, yzy221, yzy222, yzy223, yzy224, yzy225, 
                                        yzy226, yzy227, yzy228, bz1, bz2, bz3, drbz 
                                   from his_snyd_basyssxxmx_view
                                  where ykc700 = '{0}'";

            //插入语句
            string insertSql = @"INSERT INTO HIS_SNYD_BASYSSXXMX (AKB020 ,YKC700 ,YZY201 ,YZY207 ,YZY208
                                                                 ,YZY209 ,YZY210 ,YZY211 ,YZY212 ,YZY213
                                                                 ,YZY214 ,YZY215 ,YZY216 ,YZY217 ,YZY218
                                                                 ,YZY219 ,YZY220 ,YZY221 ,YZY222 ,YZY223
                                                                 ,YZY224 ,YZY225 ,YZY226 ,YZY227 ,YZY228
                                                                 ,BZ1, BZ2, BZ3, DRBZ)
                                                         VALUES ('{0}' ,'{1}' ,{2} ,'{3}' ,'{4}'
                                                                ,'{5}' ,'{6}' ,'{7}' ,'{8}' ,'{9}'
                                                                ,'{10}' ,'{11}' ,'{12}' ,'{13}' ,{14}
                                                                ,'{15}'  ,'{16}' ,'{17}' ,'{18}' ,'{19}'
                                                                ,'{20}' ,'{21}'  ,'{22}'  ,'{23}'  ,'{24}'
                                                                ,'{25}' ,'{26}' ,'{27}'  ,{28})";

            string select = string.Format(selectSql, patient.SIMainInfo.RegNo);

            string errMessage = string.Empty;
            int rtn = this.selectInsert(select, insertSql, ref errMessage);

            if (rtn < 0)
            {
                errMsg = "上传患者病案首页手术明细信息失败！患者姓名：" + patient.Name + ";\r\n";
                errMsg += errMessage;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 住院病人产科分娩婴儿信息
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private int uploadBasyfmxx(FS.HISFC.Models.RADT.PatientInfo patient, ref string errMsg)
        {
            //查询语句
            string selectSql = @"select akb020, aab299, yab600, akb026, akb021, ykc700, aab301, yab060, aac002, aac043, aac044, bz1, bz2, bz3, drbz 
                                   from his_snyd_basyfmxx_view
                                  where ykc700 = '{0}'";

            //插入语句
            string insertSql = @"INSERT INTO HIS_SNYD_BASYFMXX (AKB020 ,AAB299 ,YAB600 ,AKB026 ,AKB021
                                                               ,YKC700 ,AAB301 ,YAB060 ,AAC002 ,AAC043
                                                               ,AAC044 ,BZ1 ,BZ2 ,BZ3 ,DRBZ)
                                                        VALUES ('{0}' ,'{1}' ,'{2}' ,'{3}' ,'{4}'
                                                               ,'{5}' ,'{6}'  ,'{7}' ,'{8}' ,'{9}'
                                                               ,'{10}' ,'{11}' ,'{12}' ,'{13}' ,{14})";

            string select = string.Format(selectSql, patient.SIMainInfo.RegNo);

            string errMessage = string.Empty;
            int rtn = this.selectInsert(select, insertSql, ref errMessage);

            if (rtn < 0)
            {
                errMsg = "上传患者产科分娩婴儿信息失败！患者姓名：" + patient.Name + ";\r\n";
                errMsg += errMessage;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 住院病人产科分娩婴儿信息明细表
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private int uploadBasyfmxxmx(FS.HISFC.Models.RADT.PatientInfo patient, ref string errMsg)
        {
            //查询语句
            string selectSql = @"select akb020, ykc700, yzy201, aac004, yzy230, yzy231, yzy232, yzy233, yzy234, yzy235, 
                                        yzy236, yzy237, yzy238, bz1, bz2, bz3, drbz 
                                   from his_snyd_basyfmxxmx_view
                                  where ykc700 = '{0}'";

            //插入语句
            string insertSql = @"INSERT INTO HIS_SNYD_BASYFMXXMX (AKB020 ,YKC700 ,YZY201 ,AAC004 ,YZY230
                                                                 ,YZY231 ,YZY232 ,YZY233 ,YZY234 ,YZY235
                                                                 ,YZY236 ,YZY237 ,YZY238 ,BZ1 ,BZ2
                                                                 ,BZ3 ,DRBZ)
                                                          VALUES ('{0}' ,'{1}' ,{2} ,'{3}' ,'{4}'
                                                                  ,{5} ,'{6}' ,'{7}' ,'{8}' ,'{9}'
                                                                  ,{10} ,'{11}' ,'{12}' ,'{13}'  ,'{14}' 
                                                                  ,'{15}' ,{16})";

            string select = string.Format(selectSql, patient.SIMainInfo.RegNo);

            string errMessage = string.Empty;
            int rtn = this.selectInsert(select, insertSql, ref errMessage);

            if (rtn < 0)
            {
                errMsg = "上传患者产科分娩婴儿明细信息失败！患者姓名：" + patient.Name + ";\r\n";
                errMsg += errMessage;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 上传出院小结/死亡记录/24小时入院记录/24小时内入院死亡记录/产科出院小结
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private int uploadCyxj(FS.HISFC.Models.RADT.PatientInfo patient, ref string errMsg)
        {            
            //查询语句
            string selectSql = @"select akb020, aab299, yab600, akb026, akb021, ykc700, aab301, yab060, aac002, aac043, 
                                        aac044, yzy301, yzy302, yzy303, yzy304,  yzy305, yzy306, akc273, bz1, bz2, 
                                        bz3, aac003, aac004, pka017, pka032, pka035, yzy307,yzy308, yzy309, yzy310, 
                                        pka025,yzy311, drbz
                                   from his_snyd_cyxj_view
                                  where ykc700 = '{0}'
                                    and rownum = 1";

            //插入语句
            string insertSql = @"INSERT INTO HIS_SNYD_CYXJ (AKB020 ,AAB299 ,YAB600 ,AKB026 ,AKB021
                                                           ,YKC700 ,AAB301 ,YAB060 ,AAC002 ,AAC043
                                                           ,AAC044 ,YZY301 ,YZY302 ,YZY303 ,YZY304
                                                           ,YZY305 ,YZY306 ,AKC273 ,BZ1 ,BZ2
                                                           ,BZ3 ,aac003 ,aac004 ,pka017 ,pka032
                                                           ,pka035 ,yzy307 ,yzy308 ,yzy309 ,yzy310
                                                           ,pka025 ,yzy311 ,DRBZ)
                                                    VALUES ('{0}' ,'{1}' ,'{2}' ,'{3}' ,'{4}'
                                                           ,'{5}' ,'{6}' ,'{7}' ,'{8}' ,'{9}'
                                                           ,'{10}' ,'{11}' ,'{12}' ,'{13}' ,'{14}'
                                                           ,'{15}' ,'{16}' ,'{17}' ,'{18}' ,'{19}'
                                                           ,'{20}' ,'{21}' ,'{22}' ,'{23}' ,'{24}'
                                                           ,'{25}' ,'{26}' ,'{27}' ,'{28}' ,'{29}'
                                                           ,'{30}' ,'{31}' ,{32})";

            string select = string.Format(selectSql, patient.SIMainInfo.RegNo);

            string errMessage = string.Empty;
            int rtn = this.selectInsert(select, insertSql, ref errMessage);

            if (rtn < 0)
            {
                errMsg = "上传患者出院小结信息失败！患者姓名：" + patient.Name + "; \r\n";
                errMsg += errMessage;
                return -1;
            }

            return 1;
        }

        #endregion

        #region 取消医保系统上传信息

        /// <summary>
        /// 删除各类医保系统的上传信息
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private int deleteUpdateInfo(FS.HISFC.Models.RADT.PatientInfo patient, ref string errMsg)
        {
            if (this.deleteBasybrxx(patient, ref errMsg) < 0)
            {
                return -1;
            }

            if (this.deleteBasyzdxx(patient, ref errMsg) < 0)
            {
                return -1;
            }

            if (this.deleteBasyzdxxmx(patient, ref errMsg) < 0)
            {
                return -1;
            }

            if (this.deleteBasyssxx(patient, ref errMsg) < 0)
            {
                return -1;
            }

            if (this.deleteBasyssxxmx(patient, ref errMsg) < 0)
            {
                return -1;
            }

            if (this.deleteBasyfmxx(patient, ref errMsg) < 0)
            {
                return -1;
            }

            if (this.deleteBasyfmxxmx(patient, ref errMsg) < 0)
            {
                return -1;
            }

            if (this.deleteCyxj(patient, ref errMsg) < 0)
            {
                return -1;
            }

            if (this.setSiUploadFlag(patient, "0", ref errMsg) < 0)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 删除医保系统病案首页信息
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private int deleteBasybrxx(FS.HISFC.Models.RADT.PatientInfo patient, ref string errMsg)
        {
            //删除语句
            string deleteSql = @"delete from HIS_SNYD_BASYBRXX
                                  where ykc700 = '{0}'";

            deleteSql = string.Format(deleteSql, patient.SIMainInfo.RegNo);

            int rtn = 0;
            string errMessage = string.Empty;

            try
            {
                IBorn.SI.GuangZhou.Base.SIDataBase.ExecuteNonQuery(deleteSql);
            }
            catch(Exception ex)
            {
                rtn = -1;
                errMessage = ex.Message;
            }

            if (rtn < 0)
            {
                errMsg = "删除医保系统患者病案首页信息失败！患者姓名：" + patient.Name + ";\r\n";
                errMsg += errMessage;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 删除医保系统病案首页诊断信息
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private int deleteBasyzdxx(FS.HISFC.Models.RADT.PatientInfo patient, ref string errMsg)
        {
            //删除语句
            string deleteSql = @"delete from HIS_SNYD_BASYZDXX
                                  where ykc700 = '{0}'";

            int rtn = 0;
            string errMessage = string.Empty;

            try
            {
                deleteSql = string.Format(deleteSql, patient.SIMainInfo.RegNo);
                IBorn.SI.GuangZhou.Base.SIDataBase.ExecuteNonQuery(deleteSql);
            }
            catch (Exception ex)
            {
                rtn = -1;
                errMessage = ex.Message;
            }

            if (rtn < 0)
            {
                errMsg = "删除医保系统患者诊断信息失败！患者姓名：" + patient.Name + ";\r\n";
                errMsg += errMessage;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 删除医保系统病案首页诊断信息明细
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private int deleteBasyzdxxmx(FS.HISFC.Models.RADT.PatientInfo patient, ref string errMsg)
        {
            //删除语句
            string deleteSql = @"delete from HIS_SNYD_BASYZDXXMX
                                  where ykc700 = '{0}'";

            int rtn = 0;
            string errMessage = string.Empty;

            try
            {
                deleteSql = string.Format(deleteSql, patient.SIMainInfo.RegNo);
                IBorn.SI.GuangZhou.Base.SIDataBase.ExecuteNonQuery(deleteSql);
            }
            catch (Exception ex)
            {
                rtn = -1;
                errMessage = ex.Message;
            }

            if (rtn < 0)
            {
                errMsg = "删除医保系统患者诊断明细信息失败！患者姓名：" + patient.Name + ";\r\n";
                errMsg += errMessage;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 删除医保系统病案首页手术信息
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private int deleteBasyssxx(FS.HISFC.Models.RADT.PatientInfo patient, ref string errMsg)
        {
            //删除语句
            string deleteSql = @"delete from HIS_SNYD_BASYSSXX
                                  where ykc700 = '{0}'";

            int rtn = 0;
            string errMessage = string.Empty;

            try
            {
                deleteSql = string.Format(deleteSql, patient.SIMainInfo.RegNo);
                IBorn.SI.GuangZhou.Base.SIDataBase.ExecuteNonQuery(deleteSql);
            }
            catch (Exception ex)
            {
                rtn = -1;
                errMessage = ex.Message;
            }

            if (rtn < 0)
            {
                errMsg = "删除医保系统患者手术信息失败！患者姓名：" + patient.Name + ";\r\n";
                errMsg += errMessage;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 删除医保系统病案首页手术信息明细
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private int deleteBasyssxxmx(FS.HISFC.Models.RADT.PatientInfo patient, ref string errMsg)
        {
            //删除语句
            string deleteSql = @"delete from HIS_SNYD_BASYSSXXMX
                                  where ykc700 = '{0}'";

            int rtn = 0;
            string errMessage = string.Empty;

            try
            {
                deleteSql = string.Format(deleteSql, patient.SIMainInfo.RegNo);
                IBorn.SI.GuangZhou.Base.SIDataBase.ExecuteNonQuery(deleteSql);
            }
            catch (Exception ex)
            {
                rtn = -1;
                errMessage = ex.Message;
            }

            if (rtn < 0)
            {
                errMsg = "删除医保系统患者病案首页手术明细信息失败！患者姓名：" + patient.Name + ";\r\n";
                errMsg += errMessage;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 删除医保系统住院病人产科分娩婴儿信息
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private int deleteBasyfmxx(FS.HISFC.Models.RADT.PatientInfo patient, ref string errMsg)
        {
            //删除语句
            string deleteSql = @"delete from HIS_SNYD_BASYFMXX
                                  where ykc700 = '{0}'";

            int rtn = 0;
            string errMessage = string.Empty;

            try
            {
                deleteSql = string.Format(deleteSql, patient.SIMainInfo.RegNo);
                IBorn.SI.GuangZhou.Base.SIDataBase.ExecuteNonQuery(deleteSql);
            }
            catch (Exception ex)
            {
                rtn = -1;
                errMessage = ex.Message;
            }

            if (rtn < 0)
            {
                errMsg = "删除医保系统住院病人产科分娩婴儿信息失败！患者姓名：" + patient.Name + ";\r\n";
                errMsg += errMessage;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 删除医保系统住院病人产科分娩婴儿信息明细表
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private int deleteBasyfmxxmx(FS.HISFC.Models.RADT.PatientInfo patient, ref string errMsg)
        {
            //删除语句
            string deleteSql = @"delete from HIS_SNYD_BASYFMXXMX
                                  where ykc700 = '{0}'";

            int rtn = 0;
            string errMessage = string.Empty;

            try
            {
                deleteSql = string.Format(deleteSql, patient.SIMainInfo.RegNo);
                IBorn.SI.GuangZhou.Base.SIDataBase.ExecuteNonQuery(deleteSql);
            }
            catch (Exception ex)
            {
                rtn = -1;
                errMessage = ex.Message;
            }

            if (rtn < 0)
            {
                errMsg = "删除医保系统住院患者产科分娩婴儿明细信息失败！患者姓名：" + patient.Name + ";\r\n";
                errMsg += errMessage;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 删除医保系统出院小结/死亡记录/24小时入院记录/24小时内入院死亡记录/产科出院小结
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private int deleteCyxj(FS.HISFC.Models.RADT.PatientInfo patient, ref string errMsg)
        {
            //删除语句
            string deleteSql = @"delete from HIS_SNYD_CYXJ
                                  where ykc700 = '{0}'";

            int rtn = 0;
            string errMessage = string.Empty;

            try
            {
                deleteSql = string.Format(deleteSql, patient.SIMainInfo.RegNo);
                IBorn.SI.GuangZhou.Base.SIDataBase.ExecuteNonQuery(deleteSql);
            }
            catch (Exception ex)
            {
                rtn = -1;
                errMessage = ex.Message;
            }

            if (rtn < 0)
            {
                errMsg = "删除医保系统出院小结信息失败！患者姓名：" + patient.Name + ";\r\n";
                errMsg += errMessage;
                return -1;
            }

            return 1;
        }

        #endregion

        #region 辅助函数

        /// <summary>
        /// 通过查询语句查询并格式化结果插入
        /// </summary>
        /// <param name="selectSql"></param>
        /// <param name="insertSql"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private int selectInsert(string selectSql, string insertSql,ref string errMsg)
        {
            int rtn = this.inpatientManager.ExecQuery(selectSql);

            int insertCount = 0;

            if (rtn < 0)
            {
                errMsg = this.inpatientManager.Err;
                return -1;
            }
            try
            {
                while (this.inpatientManager.Reader.Read())
                {
                    int fieldCount = this.inpatientManager.Reader.FieldCount;
                    string[] insertParams = new string[fieldCount];

                    for (int i = 0; i < fieldCount; i++)
                    {
                        insertParams[i] = this.inpatientManager.Reader[i] == null ? "" : this.inpatientManager.Reader[i].ToString();
                    }

                    string insert = string.Format(insertSql, insertParams);

                    rtn = IBorn.SI.GuangZhou.Base.SIDataBase.ExecuteNonQuery(insert);

                    insertCount++;

                    if (rtn < 0)
                    {
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return -1;
            }
            finally
            {
                this.inpatientManager.Reader.Close();
            }

            return insertCount;
        }

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
            /// 结算时间
            /// </summary>
            JSSJ = 12

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
            /// 结算时间
            /// </summary>
            JSSJ = 11
        }

        #endregion 
    }
}
