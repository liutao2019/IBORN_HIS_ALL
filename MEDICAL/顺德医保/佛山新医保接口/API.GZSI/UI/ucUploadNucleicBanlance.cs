using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using API.GZSI.Models;
using API.GZSI.Business;

namespace API.GZSI.UI
{
    public partial class ucUploadNucleicBanlance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 核酸批量上传
        /// </summary>
        public ucUploadNucleicBanlance()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 综合业务
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        private Process Process = new Process();
        /// <summary>
        /// 人员信息管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Person personMgr = new FS.HISFC.BizLogic.Manager.Person();

        /// <summary>
        /// 科室管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();

        /// <summary>
        /// 非药品管理类
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Item undrugManager = new FS.HISFC.BizLogic.Fee.Item();
        /// <summary>
        /// 参数管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam cpMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 医保数据操作
        /// </summary>
        private LocalManager localMgr = new LocalManager();

        /// <summary>
        /// Excel导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInputExcel_Click(object sender, EventArgs e)
        {
            string filePath = "";
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择Excel文件";
            dialog.Filter = "Excel(*.xls)|*.xls|Excel(*.xlsx)|*.xlsx";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filePath = dialog.FileName;
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application(); ;
                Microsoft.Office.Interop.Excel.Sheets sheets;
                object oMissiong = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Excel.Workbook workbook = null;
                DataTable dt = new DataTable();
                bool hasTitle = false;//是否需要第一列头，false不需要，true需要
                try
                {
                    if (app == null)
                    {
                        MessageBox.Show("Excel文件信息为空！");
                        return;
                    }
                    workbook = app.Workbooks.Open(filePath, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong,
                        oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong);
                    sheets = workbook.Worksheets;

                    //将数据读入到DataTable中
                    Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets.get_Item(1);//读取第一张表  
                    if (worksheet == null)
                    {
                        MessageBox.Show("Excel文件信息为空！");
                        return;
                    }

                    int iRowCount = worksheet.UsedRange.Rows.Count;
                    int iColCount = worksheet.UsedRange.Columns.Count;

                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在导入，请稍后！");
                    //生成列头
                    for (int i = 0; i < iColCount; i++)
                    {
                        var name = "column" + i;
                        if (hasTitle)
                        {
                            var txt = ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, i + 1]).Text.ToString();
                            if (!string.IsNullOrEmpty(txt)) name = txt;
                        }
                        while (dt.Columns.Contains(name)) name = name + "_1";//重复行名称会报错。
                        dt.Columns.Add(new DataColumn(name, typeof(string)));
                    }
                    //生成行数据
                    Microsoft.Office.Interop.Excel.Range range;
                    int rowIdx = hasTitle ? 1 : 2;
                    for (int iRow = rowIdx; iRow <= iRowCount; iRow++)
                    {
                        DataRow dr = dt.NewRow();
                        for (int iCol = 1; iCol <= iColCount; iCol++)
                        {
                            range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[iRow, iCol];
                            dr[iCol - 1] = (range.Value2 == null) ? "" : range.Text.ToString();
                        }
                        dt.Rows.Add(dr);
                    }
                    if (dt == null || dt.Rows.Count <= 0)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("获取Excel文件信息为空！");
                        return;
                    }
                    if (InsertBaseInfo(dt) < 0)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        return;
                    }
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("导入成功！");
                    //return dt;
                }
                catch (Exception ex)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("获取Excel异常：" + ex.Message);
                    return;
                }
                finally
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    workbook.Close(false, oMissiong, oMissiong);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                    workbook = null;
                    app.Workbooks.Close();
                    app.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                    app = null;
                }



                //string fileType = System.IO.Path.GetExtension(filePath);
                //if (string.IsNullOrEmpty(fileType))
                //{
                //    MessageBox.Show("请选择正确的Excel文件！");
                //    return;
                //}
                //bool hasTitle = false;
                //using (DataSet ds = new DataSet())
                //{
                //    string strCon = string.Format("Provider=Microsoft.Jet.OLEDB.{0}.0;" +
                //                    "Extended Properties=\"Excel {1}.0;HDR={2};IMEX=1;\";" +
                //                    "data source={3};",
                //                    (fileType == ".xls" ? 4 : 12), (fileType == ".xls" ? 8 : 12), (hasTitle ? "Yes" : "NO"), filePath);
                //    string strCom = " SELECT * FROM [Sheet1$]";
                //    using (System.Data.OleDb.OleDbConnection myConn = new System.Data.OleDb.OleDbConnection(strCon))
                //    using (System.Data.OleDb.OleDbDataAdapter myCommand = new System.Data.OleDb.OleDbDataAdapter(strCom, myConn))
                //    {
                //        myConn.Open();
                //        myCommand.Fill(ds);
                //    }
                //    if (ds == null || ds.Tables.Count <= 0)
                //    {
                //        MessageBox.Show("获取Excel文件信息失败！");
                //        return;
                //    }
                //   DataTable dt =  ds.Tables[0];
                //   if (InsertBaseInfo(dt) < 0)
                //   {
                //       return;
                //   }
                //}
            }
            else
            {
                MessageBox.Show("请选择正确的Excel文件！！");
                return;
            }
        }
        /// <summary>
        /// 本地导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btInputLocal_Click(object sender, EventArgs e)
        {
            frmQueryBaseInfoLocal frmQueryBaseInfoLocal = new frmQueryBaseInfoLocal();
            frmQueryBaseInfoLocal.ShowDialog(this);
        }

        private int InsertBaseInfo(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
            {
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.localMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            int index = 2;

            foreach (DataRow dRow in dt.Rows)
            {
                //排除表头不规范的问题
                string pattern = "[\u4e00-\u9fbb]";
                if (!string.IsNullOrEmpty(dRow[1].ToString()) && Regex.IsMatch(dRow[1].ToString(), pattern))
                {
                    continue;
                }

                #region 必填
                if (string.IsNullOrEmpty(dRow[1].ToString()))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("第" + index.ToString() + "行，证件号码必填！");
                    return -1;
                }
                if (string.IsNullOrEmpty(dRow[0].ToString()))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("第" + index.ToString() + "行，证件号码：" + dRow[1].ToString() + "，证件类型必填！");
                    return -1;
                }
                if (string.IsNullOrEmpty(dRow[2].ToString()))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("第" + index.ToString() + "行，证件号码：" + dRow[1].ToString() + "，姓名必填！");
                    return -1;
                }
                if (string.IsNullOrEmpty(dRow[3].ToString()))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("第" + index.ToString() + "行，证件号码：" + dRow[1].ToString() + "，就诊日期必填！");
                    return -1;
                }
                if (string.IsNullOrEmpty(dRow[8].ToString()))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("第" + index.ToString() + "行，证件号码：" + dRow[1].ToString() + "，项目信息必填！");
                    return -1;
                }

                if (string.IsNullOrEmpty(dRow[9].ToString()))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("第" + index.ToString() + "行，证件号码：" + dRow[1].ToString() + "，项目信息必填！");
                    return -1;
                }
                index++;
                #endregion

                FS.HISFC.Models.Registration.Register r = new FS.HISFC.Models.Registration.Register();
                r.IDCardType.ID = dRow[0].ToString();
                r.IDCard = dRow[1].ToString();
                r.Name = dRow[2].ToString();
                r.DoctorInfo.SeeDate = Convert.ToDateTime(dRow[3].ToString());
                r.InTimes = Convert.ToInt32(dRow[4].ToString());
                r.OwnCost = Convert.ToDecimal(dRow[5].ToString());
                r.PubCost = Convert.ToDecimal(dRow[6].ToString());
                r.PatientType = dRow[7].ToString();
                r.Insurance.Name = dRow[8].ToString();
                r.Insurance.Memo = "0";
                r.DoctorInfo.Templet.Doct.ID = dRow[9].ToString();
                r.ID = "";
                if (this.localMgr.InsertHSBaseInfo(r) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("导入失败！");
                    return -1;
                }
                //
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

        private void btUploadBalance_Click(object sender, EventArgs e)
        {
            if (this.tabMain.SelectedIndex != 0)
            {
                MessageBox.Show("请选择待结算界面！");
                return;
            }
            //提示
            string strTips = "是否确定上传结算！";
            if (MessageBox.Show(strTips, "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }

            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("无需要结算信息!");
                return;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在批量结算!请稍后!");

            for (int k = 0; k < this.neuSpread1_Sheet1.Rows.Count; k++)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(k, this.neuSpread1_Sheet1.Rows.Count);

                bool isChoose = FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[k, 0].Value);
                if (isChoose)
                {
                    DataRow dRow = this.neuSpread1_Sheet1.Cells[k, 0].Tag as DataRow;
                    if (dRow == null)
                    {
                        continue;
                    }
                    FS.HISFC.Models.Registration.Register r = new FS.HISFC.Models.Registration.Register();
                    r.Pact.ID = "9999";
                    r.Pact.PayKind.ID = "02";
                    r.ID = dRow["ID"].ToString();
                    r.Name = dRow["NAME"].ToString();
                    r.IDCardType.ID = dRow["CARD_TYPE"].ToString();
                    r.SIMainInfo.Mdtrt_cert_type = dRow["CARD_TYPE"].ToString();
                    r.IDCard = dRow["IDNO"].ToString();
                    r.Pact.PayKind.ID = "02";
                    r.Pact.PayKind.User01 = "XGJC002";
                    r.Pact.PayKind.User02 = "核酸检测";
                    r.SIMainInfo.Med_type = "11";
                    r.DoctorInfo.SeeDate = FS.FrameWork.Function.NConvert.ToDateTime(dRow["SEE_DATE"].ToString());
                    r.DoctorInfo.Templet.Begin = r.DoctorInfo.SeeDate;
                    r.DoctorInfo.Templet.End = r.DoctorInfo.SeeDate;
                    r.DoctorInfo.Templet.Doct.ID = dRow["SEE_DOCT_CODE"].ToString();

                    string err = "";
                    ArrayList alFee = new ArrayList();
                    alFee = GetFeeDetail(r, dRow["ITEMINFO"].ToString(), ref err);

                    if (alFee == null || alFee.Count <= 0)
                    {
                        if (this.localMgr.UpdateHSBalanceState(dRow["ID"].ToString(), "0", err) < 0)
                        {
                            MessageBox.Show("更新状态失败！");
                            return;
                        }
                        continue;
                    }
                    if (UploadBalance(r, alFee, ref err) < 0)
                    {
                        if (this.localMgr.UpdateHSBalanceState(dRow["ID"].ToString(), "0", err) < 0)
                        {
                            MessageBox.Show("更新状态失败！");
                            return;
                        }
                        continue;
                    }
                }
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show("操作成功！");
            btQuery_Click(null, null);
        }

        /// <summary>
        /// 获取项目费用
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <returns></returns>
        private ArrayList GetFeeDetail(FS.HISFC.Models.Registration.Register r, string itemInfo, ref string err)
        {

            ArrayList alNucleicItem = new ArrayList();//核酸项目
            #region 组装费用
            if (string.IsNullOrEmpty(itemInfo))
            {
                if (alNucleicItem == null || alNucleicItem.Count <= 0)
                {
                    alNucleicItem = this.localMgr.GetNucleicItem();
                }
            }
            else
            {
                string[] itemList = itemInfo.Split('|');
                if (itemList.Length > 0)
                {
                    foreach (string itemInfoTemp in itemList)
                    {
                        string[] itemInfoList = itemInfoTemp.Split('&');
                        if (itemInfoList.Length > 1)
                        {
                            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                            obj.ID = itemInfoList[0];
                            obj.User01 = itemInfoList[1];
                            alNucleicItem.Add(obj);
                        }
                    }

                }
            }
            if (alNucleicItem == null || alNucleicItem.Count <= 0)
            {
                err = "请先维护收费项目，字典表TYPE：NucleicItem！";
                return null;
            }

            ArrayList alFee = new ArrayList();

            //当前操作人
            //FS.HISFC.Models.Base.Employee recipeDoct = personMgr.GetPersonByID(personMgr.Operator.ID);
            FS.HISFC.Models.Base.Employee recipeDoct = personMgr.GetPersonByID(r.DoctorInfo.Templet.Doct.ID);
            if (recipeDoct == null || string.IsNullOrEmpty(recipeDoct.ID))
            {
                err = "查找医生信息【" + personMgr.Operator.ID + "】失败!";
                return null;
            }

            //科室信息
            string deptCode = "";
            FS.FrameWork.Models.NeuObject objTemp = alNucleicItem[0] as FS.FrameWork.Models.NeuObject;
            if (objTemp != null)
            {
                deptCode = objTemp.Memo;
            }
            if (string.IsNullOrEmpty(deptCode))
            {
                deptCode = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
            }
            FS.HISFC.Models.Base.Department recipeDept = deptMgr.GetDeptmentById(deptCode);
            if (recipeDept == null || string.IsNullOrEmpty(recipeDept.ID))
            {
                err = "查找科室信息【" + deptCode + "】失败!";
                return null;
            }

            string itemCodeCompareType = cpMgr.GetControlParam<string>("FSMZ16", false, "0");
            DateTime dtNow = this.deptMgr.GetDateTimeFromSysDateTime();

            r.DoctorInfo.Templet.Doct = recipeDoct;
            r.DoctorInfo.Templet.Dept = recipeDept;
            if (r.DoctorInfo.SeeDate >= DateTime.MaxValue || r.DoctorInfo.SeeDate <= DateTime.MinValue)
            {
                r.DoctorInfo.SeeDate = dtNow;
                r.DoctorInfo.Templet.Begin = dtNow;
                r.DoctorInfo.Templet.End = dtNow;
            }
            foreach (FS.FrameWork.Models.NeuObject obj in alNucleicItem)
            {
                decimal totCost = 0m;
                if (!string.IsNullOrEmpty(obj.User01))
                {
                    totCost = decimal.Parse(obj.User01);
                }
                FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();
                FS.HISFC.Models.Fee.Item.Undrug undrugItem = undrugManager.GetItemByUndrugCode(obj.ID);
                if (undrugItem == null || string.IsNullOrEmpty(undrugItem.ID))
                {
                    err = "查找物价项目【" + obj.ID + "】失败!";
                    return null;
                }
                if (!undrugItem.IsValid)
                {
                    err = "物价项目【" + obj.ID + "-" + undrugItem.Name + "】无效!";
                    return null;
                }
                if (undrugItem.UnitFlag == "1")
                {
                    err = "物价项目【" + obj.ID + "-" + undrugItem.Name + "】为复合项目!请联系信息科重新维护!";
                    return null;
                }
                feeItem.Item = undrugItem;
                if (totCost > 0)
                {
                    feeItem.Item.Price = totCost;
                }
                feeItem.Item.Qty = 1;
                feeItem.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                feeItem.IsGroup = false;

                feeItem.Patient.ID = "";//门诊流水号
                feeItem.Patient.PID.CardNO = "";//门诊卡号 
                feeItem.Order.ID = string.Empty;   //医嘱号为空

                FS.HISFC.Models.Base.Employee chargeOper = recipeDoct;

                feeItem.ChargeOper.ID = chargeOper.ID;  //划价
                feeItem.Order.Combo.ID = "";   //组合号

                feeItem.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(feeItem.Item.Qty * feeItem.Item.Price, 2);
                feeItem.FT.TotCost = feeItem.FT.OwnCost;
                feeItem.FT.PayCost = 0;
                feeItem.FT.PubCost = 0;
                feeItem.FT.RebateCost = 0;
                feeItem.NoBackQty = feeItem.Item.Qty;

                feeItem.FeePack = "1";
                feeItem.Days = 1;//天数

                //开方科室信息
                feeItem.RecipeOper.Dept = recipeDept;
                feeItem.ExecOper.Dept = recipeDept;  //执行科室

                //开方医生信息
                feeItem.RecipeOper.ID = recipeDoct.ID;
                feeItem.RecipeOper.Name = recipeDoct.Name;


                feeItem.Order.Item.ItemType = undrugItem.ItemType;//是否药品
                feeItem.PayType = FS.HISFC.Models.Base.PayTypes.Charged;//划价状态

                ((FS.HISFC.Models.Registration.Register)feeItem.Patient).DoctorInfo.SeeDate = r.DoctorInfo.SeeDate;
                ((FS.HISFC.Models.Registration.Register)feeItem.Patient).DoctorInfo.Templet.Dept = feeItem.RecipeOper.Dept;//登记科室
                feeItem.TransType = FS.HISFC.Models.Base.TransTypes.Positive;//交易类型

                //划价来源：体检
                feeItem.FTSource = "4";

                //体检费用明细主键
                feeItem.MedicalGroupCode.ID = "";
                #region 编码转换

                string medins_list_codg = "";
                if (itemCodeCompareType == "0")//国标码处理
                {
                    if (string.IsNullOrEmpty(feeItem.Item.GBCode) || feeItem.Item.GBCode == "0")
                    {
                        if (Process.DealFeeItemList(feeItem) == -1)
                        {
                            err = Process.ErrMsg;
                            return null;
                        }
                    }

                    if (string.IsNullOrEmpty(feeItem.Item.GBCode) || feeItem.Item.GBCode == "0")
                    {
                        err = "项目【" + feeItem.Item.Name + "】没有维护医保对照码(国标码为空)，请先进行维护！";
                        return null;
                    }

                    medins_list_codg = feeItem.Item.GBCode;
                }
                else //自定义码处理
                {

                    if (string.IsNullOrEmpty(feeItem.Item.UserCode) || feeItem.Item.UserCode == "0")
                    {
                        if (Process.DealFeeItemList(feeItem) == -1)
                        {
                            err = Process.ErrMsg;
                            return null;
                        }
                    }
                    if (string.IsNullOrEmpty(feeItem.Item.UserCode) || feeItem.Item.UserCode == "0")
                    {
                        err = "项目【" + feeItem.Item.Name + "】没有维护医保对照码(自定义码为空)，请先进行维护！";
                        return null;
                    }
                    medins_list_codg = feeItem.Item.UserCode;

                }

                string med_list_codg = localMgr.GetCenterCode(medins_list_codg, r.IDCard, r.Birthday);
                if (med_list_codg == "-1" || string.IsNullOrEmpty(med_list_codg))
                {
                    err = "项目【" + feeItem.Item.Name + "】没有维护国家标准码对照码，请先进行维护！";
                    return null;
                }

                feeItem.Item.UserCode = medins_list_codg;

                if (!string.IsNullOrEmpty(medins_list_codg) && medins_list_codg != "-1")
                {
                    feeItem.Item.GBCode = med_list_codg;
                }
                else
                {
                    feeItem.Item.GBCode = medins_list_codg;// string.IsNullOrEmpty(feeItem.Compare.SpellCode.UserCode) ? feeItem.Item.UserCode : feeItem.Compare.SpellCode.UserCode; ;//医药机构目录编码 Y
                }
                #endregion
                alFee.Add(feeItem);
            }
            #endregion

            return alFee;
        }

        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        private int UploadBalance(FS.HISFC.Models.Registration.Register r, ArrayList alFee, ref string Err)
        {
            if (alFee == null || alFee.Count <= 0)
            {
                Err = "请先维护收费项目，字典表TYPE：NucleicItem！！！";
                return -1;
            }
            #region 校验人员信息
            if (this.QueryCanMedicare(r, ref Err) < 0)
            {
                Err = "校验患者信息失败！" + Err;
                return -1;

            }
            #endregion

            #region 挂号

            OutPatient2201 outPatient2201 = new OutPatient2201();
            Models.Request.RequestGzsiModel2201 RequestGzsiModel2201 = new Models.Request.RequestGzsiModel2201();
            Models.Response.ResponseGzsiModel2201 responseGzsiModel2201 = new Models.Response.ResponseGzsiModel2201();
            Models.Request.RequestGzsiModel2201.Mdtrtinfo data2201 = new Models.Request.RequestGzsiModel2201.Mdtrtinfo();
            data2201.psn_no = r.SIMainInfo.Psn_no;//人员编号
            data2201.insutype = r.SIMainInfo.Insutype;//险种类型
            data2201.mdtrt_cert_type = r.SIMainInfo.Mdtrt_cert_type;//就诊凭证类型
            data2201.mdtrt_cert_no = r.SIMainInfo.Certno;//就诊凭证编号
            if (r.DoctorInfo.Templet.Begin <= DateTime.MinValue || r.DoctorInfo.Templet.Begin >= DateTime.MaxValue)
            {

                if (r.DoctorInfo.SeeDate <= DateTime.MinValue || r.DoctorInfo.SeeDate >= DateTime.MaxValue)
                {
                    data2201.begntime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//开始时间 
                }
                else
                {
                    data2201.begntime = r.DoctorInfo.SeeDate.ToString("yyyy-MM-dd HH:mm:ss");//开始时间 
                }
            }
            else
            {
                data2201.begntime = r.DoctorInfo.Templet.Begin.ToString("yyyy-MM-dd HH:mm:ss");//开始时间 
            }
            string cardNo = this.localMgr.GetCardNoByIDNO(r.SIMainInfo.Certno);
            if (string.IsNullOrEmpty(cardNo))
            {
                cardNo = r.ID;
            }
            data2201.ipt_otp_no = r.ID;//住院/门诊号
            data2201.dept_code = r.DoctorInfo.Templet.Dept.ID;//科室编码  
            data2201.dept_name = r.DoctorInfo.Templet.Dept.Name;//科室名称  
            data2201.atddr_no = this.localMgr.GetGDDoct(r.DoctorInfo.Templet.Doct.ID);//医师编码
            data2201.dr_name = r.DoctorInfo.Templet.Doct.Name;//医师姓名
            if (string.IsNullOrEmpty(data2201.atddr_no))
            {
                MessageBox.Show("医生" + data2201.dr_name + "没有维护国家编码，请维护！");
                return -1;
            }
            data2201.caty = this.localMgr.GetGDDept(r.DoctorInfo.Templet.Dept.ID);//科别
            RequestGzsiModel2201.data = data2201;
            if (outPatient2201.CallService(RequestGzsiModel2201, ref responseGzsiModel2201) < 0)
            {
                Err = "挂号失败！" + outPatient2201.ErrorMsg;
                return -1;
            }

            if (responseGzsiModel2201.infcode != "0")
            {
                Err = "挂号失败！！" + responseGzsiModel2201.err_msg;
                return -1;
            }
            //就诊ID 赋值
            r.SIMainInfo.Mdtrt_id = responseGzsiModel2201.output.data.mdtrt_id;
            #endregion

            #region 门诊就诊信息上传
            #region 2203
            OutPatient2203 outPatient2203 = new OutPatient2203();
            Models.Request.RequestGzsiModel2203 RequestGzsiModel2203 = new Models.Request.RequestGzsiModel2203();
            Models.Response.ResponseGzsiModel2203 responseGzsiModel2203 = new Models.Response.ResponseGzsiModel2203();
            List<Models.Request.RequestGzsiModel2203.Diseinfo> diseinfoList2203 = new List<Models.Request.RequestGzsiModel2203.Diseinfo>();
            RequestGzsiModel2203.diseinfo = new List<Models.Request.RequestGzsiModel2203.Diseinfo>();

            #region 诊断信息判断
            Models.Request.RequestGzsiModel2203.Diseinfo diseinfo2203 = new Models.Request.RequestGzsiModel2203.Diseinfo();

            diseinfo2203.diag_type = "1"; //"1";//诊断类别  1 西医诊断
            diseinfo2203.diag_srt_no = "1";//诊断排序号 
            diseinfo2203.diag_code = "Z03.800";
            diseinfo2203.diag_name = "可疑疾病和情况的观察，其他的";
            diseinfo2203.diag_dept = r.DoctorInfo.Templet.Dept.ID;//诊断科室 
            diseinfo2203.dise_dor_no = this.localMgr.GetGDDoct(r.DoctorInfo.Templet.Doct.ID);//诊断医生编码 
            diseinfo2203.dise_dor_name = r.DoctorInfo.Templet.Doct.Name;//诊断医生姓名 
            if (string.IsNullOrEmpty(diseinfo2203.dise_dor_no))
            {
                MessageBox.Show("医生" + diseinfo2203.dise_dor_name + "没有维护国家编码，请维护！");
                return -1;
            }
            diseinfo2203.diag_time = r.DoctorInfo.Templet.Begin.ToString("yyyy-MM-dd HH:mm:ss");// DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//诊断时间 
            diseinfo2203.vali_flag = "1";//有效标志
            RequestGzsiModel2203.diseinfo.Add(diseinfo2203);

            #endregion 诊断信息判断
            Models.Request.RequestGzsiModel2203.Mdtrtinfo mdtrtinfo2203 = new Models.Request.RequestGzsiModel2203.Mdtrtinfo();

            mdtrtinfo2203.mdtrt_id = r.SIMainInfo.Mdtrt_id;//就诊 ID 
            mdtrtinfo2203.psn_no = r.SIMainInfo.Psn_no;//人员编号 
            mdtrtinfo2203.med_type = r.SIMainInfo.Med_type;//医疗类别 
            mdtrtinfo2203.begntime = r.DoctorInfo.Templet.Begin.ToString("yyyy-MM-dd HH:mm:ss");//开始时间 
            mdtrtinfo2203.main_cond_dscr = "";//主要病情描述 
            mdtrtinfo2203.dise_codg = r.Pact.PayKind.User01;// fp.dise_codg;//病种编码 
            mdtrtinfo2203.dise_name = r.Pact.PayKind.User02;//fp.dise_name;//病种名称 
            mdtrtinfo2203.birctrl_type = "";//计划生育手术类别 
            mdtrtinfo2203.birctrl_matn_date = "";//计划生育手术或生育日期 
            RequestGzsiModel2203.mdtrtinfo = new Models.Request.RequestGzsiModel2203.Mdtrtinfo();
            RequestGzsiModel2203.mdtrtinfo = mdtrtinfo2203;
            if (outPatient2203.CallService(RequestGzsiModel2203, ref responseGzsiModel2203) == -1)
            {
                Err = "就诊信息上传失败！！" + outPatient2203.ErrorMsg;
                Cancel(r.SIMainInfo.Mdtrt_id, r.SIMainInfo.Setl_id, r.SIMainInfo.Psn_no, ref Err);
                return -1;
            }
            if (responseGzsiModel2203.infcode != "0")
            {
                Err = "就诊信息上传失败！！" + responseGzsiModel2203.err_msg;
                Cancel(r.SIMainInfo.Mdtrt_id, r.SIMainInfo.Setl_id, r.SIMainInfo.Psn_no, ref Err);
                return -1;
            }
            #endregion

            #region 生育门诊用2203A
            #endregion
            #endregion

            #region 上传费用

            #region 上传明细

            OutPatient2204 outPatient2204 = new OutPatient2204();
            Models.Request.RequestGzsiModel2204 RequestGzsiModel2204 = new Models.Request.RequestGzsiModel2204();
            Models.Response.ResponseGzsiModel2204 responseGzsiModel2204 = new Models.Response.ResponseGzsiModel2204();
            RequestGzsiModel2204.feedetail = new List<Models.Request.RequestGzsiModel2204.Feedetail>();
            string medfee_sumamt = string.Empty;
            int i = 0;
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in alFee)
            {
                if (feeItem == null)
                {
                    continue;
                }

                decimal unitPrice = FS.FrameWork.Public.String.FormatNumber(System.Math.Abs(feeItem.FT.TotCost / feeItem.Item.Qty), 4);
                //decimal unitPrice = feeItem.Item.Price;
                decimal Count = feeItem.Item.Qty;
                //FS.HISFC.Models.Base.Employee doctor = interMgr.GetEmployeeInfo(feeItem.RecipeOper.ID);//获取开立医生信息
                Models.Request.RequestGzsiModel2204.Feedetail feedetail2204 = new Models.Request.RequestGzsiModel2204.Feedetail();
                feedetail2204.feedetl_sn = r.SIMainInfo.Mdtrt_id + (i + 1).ToString().PadLeft(5, '0');
                feedetail2204.psn_no = r.SIMainInfo.Psn_no;//人员编号 Y
                feedetail2204.mdtrt_id = r.SIMainInfo.Mdtrt_id;//就诊ID Y
                feedetail2204.chrg_bchno = r.SIMainInfo.Mdtrt_id;//收费批次号 
                feedetail2204.dise_codg = r.Pact.PayKind.User01;//fp.dise_codg;//病种编号 
                feedetail2204.rxno = r.SIMainInfo.Mdtrt_id;//处方号 
                feedetail2204.rx_circ_flag = "1";//外购处方标志 Y
                feedetail2204.fee_ocur_time = r.DoctorInfo.Templet.Begin.ToString("yyyy-MM-dd HH:mm:ss");//DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//费用发生日期 Y
                feedetail2204.cnt = Count.ToString("F4");//数量 Y
                feedetail2204.pric = unitPrice.ToString("F4");//单价 Y
                feedetail2204.det_item_fee_sumamt = feeItem.FT.TotCost.ToString("F2");//明细项目费用总额 Y
                feedetail2204.sin_dos_dscr = "";//单次剂量描述 
                feedetail2204.used_frqu_dscr = "";//使用频次描述 
                feedetail2204.prd_days = "";//用药周期天数 
                feedetail2204.medc_way_dscr = "";//用药途径描述 
                feedetail2204.medins_list_codg = feeItem.Item.UserCode;//HIS目录编码
                feedetail2204.med_list_codg = feeItem.Item.GBCode;//国家标准编码
                feedetail2204.bilg_dept_codg = feeItem.RecipeOper.Dept.ID;//开单科室编码 Y
                feedetail2204.bilg_dept_name = feeItem.RecipeOper.Dept.Name;//开单科室名称 Y
                feedetail2204.bilg_dr_codg = this.localMgr.GetGDDoct(feeItem.RecipeOper.ID);//开单医生编码 Y
                feedetail2204.bilg_dr_name = feeItem.RecipeOper.Name;//开单医生姓名 Y
                if (string.IsNullOrEmpty(feedetail2204.bilg_dr_codg))
                {
                    MessageBox.Show("医生" + feedetail2204.bilg_dr_name + "没有维护国家编码，请维护！");
                    return -1;
                }
                feedetail2204.acord_dept_codg = "";//受单科室编码 
                feedetail2204.acord_dept_name = "";//受单科室名称 
                feedetail2204.orders_dr_code = this.localMgr.GetGDDoct(feeItem.RecipeOper.ID);//受单医生编码    同开单医生
                feedetail2204.orders_dr_name = feeItem.RecipeOper.Name;//受单医生姓名 
                feedetail2204.hosp_appr_flag = "1";//feeItem.RangeFlag != "1" ? "0" : "1"; ;//医院审批标志   “0”或“2”时，明细按照自费处理 “1”时，明细按纳入报销处理。
                feedetail2204.tcmdrug_used_way = "";//中药使用方式 
                feedetail2204.etip_flag = "";//外检标志 
                feedetail2204.etip_hosp_code = "";//外检医院编码 
                feedetail2204.dscg_tkdrug_flag = "";//出院带药标志 
                feedetail2204.matn_fee_flag = "";//生育费用标志 
                //feedetail2204.unchk_flag = "";//不进行审核标志 
                //feedetail2204.unchk_memo = "";//不进行审核说明 

                RequestGzsiModel2204.feedetail.Add(feedetail2204);
                i++;
            }
            if (outPatient2204.CallService(RequestGzsiModel2204, ref responseGzsiModel2204) < 0)
            {
                Err = "门诊费用明细信息上传失败！" + outPatient2204.ErrorMsg;
                Cancel(r.SIMainInfo.Mdtrt_id, r.SIMainInfo.Setl_id, r.SIMainInfo.Psn_no, ref Err);
                return -1;
            }
            if (responseGzsiModel2204.infcode != "0")
            {
                Err = "门诊费用明细信息上传失败！！" + responseGzsiModel2204.err_msg;
                Cancel(r.SIMainInfo.Mdtrt_id, r.SIMainInfo.Setl_id, r.SIMainInfo.Psn_no, ref Err);
                return -1;
            }
            if (responseGzsiModel2204.output == null || responseGzsiModel2204.output.result.Count <= 0)
            {
                Err = "门诊费用明细信息上传失败！！出参为空！" + responseGzsiModel2204.err_msg;
                Cancel(r.SIMainInfo.Mdtrt_id, r.SIMainInfo.Setl_id, r.SIMainInfo.Psn_no, ref Err);
                return -1;
            }

            decimal totCostTemp = 0;
            foreach (Models.Response.ResponseGzsiModel2204.Result res in responseGzsiModel2204.output.result)
            {
                totCostTemp += decimal.Parse(res.det_item_fee_sumamt);
            }
            medfee_sumamt = totCostTemp.ToString();
            #endregion
            #endregion

            #region 预结算
            OutPatient2206 outPatient2206 = new OutPatient2206();
            Models.Request.RequestGzsiModel2206 RequestGzsiModel2206 = new Models.Request.RequestGzsiModel2206();
            Models.Response.ResponseGzsiModel2206 responseGzsiModel2206 = new Models.Response.ResponseGzsiModel2206();
            Models.Request.RequestGzsiModel2206.Mdtrtinfo data2206 = new Models.Request.RequestGzsiModel2206.Mdtrtinfo();
            data2206.psn_no = r.SIMainInfo.Psn_no;//人员编号 Y
            data2206.mdtrt_cert_type = r.SIMainInfo.Mdtrt_cert_type;//就诊凭证类型 Y
            data2206.mdtrt_cert_no = r.SIMainInfo.Certno;//就诊凭证编号 
            data2206.med_type = r.SIMainInfo.Med_type;//医疗类别 Y
            data2206.medfee_sumamt = medfee_sumamt;//医疗费总额 Y
            data2206.psn_setlway = "01";//个人结算方式 Y
            data2206.mdtrt_id = r.SIMainInfo.Mdtrt_id;//就诊ID Y
            data2206.chrg_bchno = r.SIMainInfo.Mdtrt_id;//收费批次号 Y （医保说取费用上传的费用明细流水号）
            data2206.acct_used_flag = "1";//个人账户使用标志 Y
            data2206.insutype = r.SIMainInfo.Insutype;//险种类型 Y
                                                      //data2206.mdtrt_mode = "0";//就诊方式 Y  0(线下就诊)
            RequestGzsiModel2206.data = new Models.Request.RequestGzsiModel2206.Mdtrtinfo();
            RequestGzsiModel2206.data = data2206;
            if (outPatient2206.CallService(RequestGzsiModel2206, ref responseGzsiModel2206) == -1)
            {
                Err = "预结算失败！" + outPatient2206.ErrorMsg;
                Cancel(r.SIMainInfo.Mdtrt_id, r.SIMainInfo.Setl_id, r.SIMainInfo.Psn_no, ref Err);
                return -1;
            }
            if (responseGzsiModel2206.infcode != "0")
            {
                Err = "预结算失败！！" + responseGzsiModel2206.err_msg;
                Cancel(r.SIMainInfo.Mdtrt_id, r.SIMainInfo.Setl_id, r.SIMainInfo.Psn_no, ref Err);
                return -1;
            }

            decimal totCost = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2206.output.setlinfo.medfee_sumamt);
            decimal pubCost = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2206.output.setlinfo.fund_pay_sumamt);
            decimal ownCost = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2206.output.setlinfo.psn_part_amt);

            r.SIMainInfo.Medfee_sumamt = totCost;
            #endregion

            #region 确费结算
            OutPatient2207 outPatient2207 = new OutPatient2207();
            Models.Request.RequestGzsiModel2207 RequestGzsiModel2207 = new Models.Request.RequestGzsiModel2207();
            Models.Response.ResponseGzsiModel2207 responseGzsiModel2207 = new Models.Response.ResponseGzsiModel2207();
            Models.Request.RequestGzsiModel2207.Mdtrtinfo data2207 = new Models.Request.RequestGzsiModel2207.Mdtrtinfo();
            data2207.psn_no = r.SIMainInfo.Psn_no;//人员编号 Y
            data2207.mdtrt_cert_type = r.SIMainInfo.Mdtrt_cert_type;//就诊凭证类型 Y
            data2207.mdtrt_cert_no = r.SIMainInfo.Certno;//就诊凭证编号 
            data2207.medfee_sumamt = r.SIMainInfo.Medfee_sumamt.ToString();//医疗费总额 Y
            data2207.mdtrt_id = r.SIMainInfo.Mdtrt_id;//就诊ID Y
            data2207.chrg_bchno = r.SIMainInfo.Mdtrt_id;//收费批次号 Y
            data2207.insutype = r.SIMainInfo.Insutype;//险种类型 
            data2207.med_type = r.SIMainInfo.Psn_type;//医疗类别 Y
            data2207.acct_used_flag = "1";//个人账户使用标志 Y 
            data2207.psn_setlway = "01";//个人结算方式  Y 
            data2207.invono = r.SIMainInfo.InvoiceNo;//发票号 
            //data2207.mdtrt_mode = "0";//就诊方式 Y 0(线下就诊)
            RequestGzsiModel2207.data = data2207;
            if (outPatient2207.CallService(RequestGzsiModel2207, ref responseGzsiModel2207) == -1)
            {
                Err = "确费结算失败！" + outPatient2207.ErrorMsg;
                Cancel(r.SIMainInfo.Mdtrt_id, r.SIMainInfo.Setl_id, r.SIMainInfo.Psn_no, ref Err);
                return -1;
            }
            if (responseGzsiModel2207.infcode != "0")
            {
                Err = "确费结算失败！" + responseGzsiModel2207.err_msg;
                Cancel(r.SIMainInfo.Mdtrt_id, r.SIMainInfo.Setl_id, r.SIMainInfo.Psn_no, ref Err);
                return -1;
            }
            #region 插入到本地
            if (this.localMgr.InsertHSBalanceInfo(r, responseGzsiModel2207) < 0)
            {
                Err = "插入本地记录失败！" + responseGzsiModel2207.err_msg;
                Cancel(r.SIMainInfo.Mdtrt_id, responseGzsiModel2207.output.setlinfo.setl_id, r.SIMainInfo.Psn_no, ref Err);
                return -1;
            }

            if (this.localMgr.UpdateHSBalanceState(r.ID, "1", "") < 0)
            {
                Err = "更新状态失败！" + localMgr.Err;
                Cancel(r.SIMainInfo.Mdtrt_id, responseGzsiModel2207.output.setlinfo.setl_id, r.SIMainInfo.Psn_no, ref Err);
                return -1;
            }
            #endregion
            #endregion
            return 1;
        }
        private int QueryCanMedicare(FS.HISFC.Models.Registration.Register r, ref string ErrMsg)
        {
            ErrMsg = "";
            if (r == null)
            {
                ErrMsg = "挂号信息为空！";
                return -1;
            }
            if (string.IsNullOrEmpty(r.PID.CardNO) && string.IsNullOrEmpty(r.IDCard))
            {
                ErrMsg = "门诊卡号或证件号为空！";
                return -1;
            }
            if (string.IsNullOrEmpty(r.IDCard))
            {
                FS.HISFC.Models.Base.Const obj = new FS.HISFC.Models.Base.Const();
                obj = this.localMgr.QueryIDNO(r.PID.CardNO);
                if (obj != null)
                {
                    r.IDCard = obj.Memo;
                }
            }
            string dise_codg = string.Empty;//病种编码 // 
            string dise_name = string.Empty;//病种名称//
            string medType = "11";
            string Insutype = "";
            string paykindCode = "";
            medType = "11";
            r.Pact.PayKind.ID = "02";
            r.SIMainInfo.Med_type = medType;
            if (string.IsNullOrEmpty(r.SIMainInfo.Mdtrt_cert_type))
            {
                r.SIMainInfo.Mdtrt_cert_type = "02";
            }

            #region 人员信息获取
            Patient1101 queryPersonDetail1101 = new Patient1101();
            Models.Request.RequestGzsiModel1101 inParam = new Models.Request.RequestGzsiModel1101();
            Models.Response.ResponseGzsiModel1101 responseGzsiModel1101 = new Models.Response.ResponseGzsiModel1101();
            Models.Request.RequestGzsiModel1101.Data bb = new Models.Request.RequestGzsiModel1101.Data();
            bb.mdtrt_cert_type = r.SIMainInfo.Mdtrt_cert_type;//就诊类型
            bb.mdtrt_cert_no = r.IDCard;//卡号
            bb.card_sn = "";
            bb.psn_cert_type = "";// mdtrt_cert_type == "02" ? "1" : "99";//就诊类型
            bb.psn_name = r.Name;
            bb.certno = r.IDCard;//卡号
            inParam.data = bb;
            if (queryPersonDetail1101.CallService(inParam, ref responseGzsiModel1101) < 0)
            {
                ErrMsg = "获取人员信息失败！" + queryPersonDetail1101.ErrorMsg;
                return -1;
            }
            if (responseGzsiModel1101.infcode != "0")
            {
                ErrMsg = "获取人员信息失败！" + responseGzsiModel1101.err_msg;
                return -1;
            }
            if (responseGzsiModel1101 == null || responseGzsiModel1101.output == null)
            {
                ErrMsg = "获取人员信息失败!" + responseGzsiModel1101.err_msg;
                return -1;
            }
            List<Models.Baseinfo> baseinfo = new List<Models.Baseinfo>();
            baseinfo.Add(responseGzsiModel1101.output.baseinfo);

            List<Models.Insuinfo> insuinfo = responseGzsiModel1101.output.insuinfo;

            if (baseinfo != null && baseinfo.Count > 0)
            {
                Models.Baseinfo baseinfoTemp = baseinfo[0] as Models.Baseinfo;

                r.SIMainInfo.Psn_no = baseinfoTemp.psn_no;
                r.SIMainInfo.Certno = baseinfoTemp.certno;
            }
            else
            {
                ErrMsg = "获取人员基本信息为空!" + responseGzsiModel1101.err_msg;
                return -1;
            }

            if (insuinfo != null && insuinfo.Count > 0)
            {
                Hashtable insuinfoYXHS = new Hashtable();

                foreach (Models.Insuinfo insuinfoTemp in insuinfo)
                {
                    //if (!insuinfoYXHS.ContainsKey(insuinfoTemp.insutype) && insuinfoTemp.insuplc_admdvs.Substring(0, 4) == "4406")//核酸不判断参保地   只有佛山的可以  {F34E39A1-5C89-406e-84C3-E476F8328C03}
                    //{
                    //    insuinfoYXHS.Add(insuinfoTemp.insutype, insuinfoTemp);
                    //}
                    //佛山只报这几个险种，其他都不管了
                    if (!insuinfoYXHS.ContainsKey(insuinfoTemp.insutype) && (insuinfoTemp.insutype == "310" || insuinfoTemp.insutype == "390" || insuinfoTemp.insutype == "340"))
                    {
                        insuinfoYXHS.Add(insuinfoTemp.insutype, insuinfoTemp);
                    }
                }
                if (insuinfoYXHS == null || insuinfoYXHS.Count <= 0)
                {
                    ErrMsg = "参保人佛山险种信息为空!";
                    return -1;

                }
                #region 人员的险种、参保地、类型确定

                //如果险种没有正常缴费那么取有正常缴费的险种结算,默认第一个
                if (string.IsNullOrEmpty(Insutype))
                {
                    if (insuinfoYXHS.ContainsKey("340"))//查询患者是否是离休患者，如果是则不查询缴费信息
                    {
                        Models.Insuinfo insuinfoTemp = insuinfoYXHS["340"] as Models.Insuinfo;
                        if (insuinfoTemp != null)
                        {
                            Insutype = "340";
                            r.SIMainInfo.Psn_type = insuinfoTemp.psn_type;
                            r.SIMainInfo.Insutype = insuinfoTemp.insutype;
                            r.SIMainInfo.Insuplc_admdvs = insuinfoTemp.insuplc_admdvs;
                        }
                    }
                }


                #endregion

                #region 查询险种缴费情况

                //无语了，险种停止时间不准确，要看上个月缴费的情况，如果上个月正常缴费，这个月险种可以正常享受待遇

                Hashtable insuinfoHS = new Hashtable();
                Hashtable insuinfoFOSHAN = new Hashtable();
                string poolarea_no12 = "";//去年12月份的参保地，医保要求如果今年转了参保地，没有定点备案的话，需要查去年12月份的参保地
                if (Insutype != "340")
                {
                    Patient90100 inPatient90100 = new Patient90100();
                    Models.Request.RequestGzsiModel90100 RequestGzsiModel90100 = new Models.Request.RequestGzsiModel90100();
                    Models.Response.ResponseGzsiModel90100 responseGzsiModel90100 = new Models.Response.ResponseGzsiModel90100();
                    Models.Request.RequestGzsiModel90100.Data data90100 = new Models.Request.RequestGzsiModel90100.Data();
                    data90100.psn_no = responseGzsiModel1101.output.baseinfo.psn_no;
                    RequestGzsiModel90100.data = data90100;

                    if (inPatient90100.CallService(RequestGzsiModel90100, ref responseGzsiModel90100) < 0)
                    {
                        ErrMsg = "缴费信息查询失败!" + inPatient90100.ErrorMsg;
                        return -1;
                    }

                    if (responseGzsiModel90100.infcode != "0")
                    {
                        ErrMsg = "缴费信息查询失败!" + responseGzsiModel90100.err_msg;
                        return -1;
                    }

                    if (responseGzsiModel90100.output == null || responseGzsiModel90100.output.Count <= 0)
                    {
                        ErrMsg = "未找到参保人的缴费信息!" + responseGzsiModel90100.err_msg;
                        return -1;
                    }
                    string insuinfoGQXZ = "";//到账日期为本月，归属月为上月的险种

                    foreach (Models.Response.ResponseGzsiModel90100.Output insuinfoTemp in responseGzsiModel90100.output)
                    { 
                        if (insuinfoTemp.poolarea_no.Substring(0, 4) != "4406")//排除非佛山的，非佛山不上传核酸
                        {
                            continue;
                        }
                        if (string.IsNullOrEmpty(insuinfoTemp.clct_time))//未到账
                        {
                            continue;
                        }
                        //到账年份去年之前的过滤
                        if (int.Parse(DateTime.Parse(insuinfoTemp.clct_time).ToString("yyyy")) < int.Parse(DateTime.Now.AddYears(-1).ToString("yyyy")))
                        {
                            continue;
                        }
                        if (insuinfoTemp.accrym_end == (DateTime.Now.AddYears(-1).ToString("yyyy") + "12")
                            && (insuinfoTemp.poolarea_no.Substring(0, 4) == "4406"))//核酸五区不区分
                        {
                            poolarea_no12 = insuinfoTemp.poolarea_no;
                        }
                        DateTime regDate = DateTime.Now;
                        if (!(r.DoctorInfo.Templet.Begin >= DateTime.MaxValue || r.DoctorInfo.Templet.Begin <= DateTime.MaxValue))
                        {
                            regDate = r.DoctorInfo.Templet.Begin;
                        }
                        if (int.Parse(insuinfoTemp.accrym_end) >= int.Parse(regDate.AddMonths(-1).ToString("yyyyMM"))
                            && (insuinfoTemp.insutype == "390" || insuinfoTemp.insutype == "310" || insuinfoTemp.insutype == "340") //佛山险种只有 310  390  340
                            )
                        {
                            if (!insuinfoHS.ContainsKey(insuinfoTemp.insutype))
                            {
                                if (string.IsNullOrEmpty(insuinfoTemp.clct_time))//未到账
                                {
                                    continue;
                                }
                                if (int.Parse(DateTime.Parse(insuinfoTemp.clct_time).ToString("yyyyMM")) >= int.Parse(DateTime.Now.ToString("yyyyMM")))
                                {
                                    //这个月到账的不允许报销
                                    //不知道为什么有些患者的到账日期是这个月，但日期却是上个月的，这种不管了
                                    insuinfoGQXZ = insuinfoTemp.insutype;
                                    continue;
                                }
                                if (!insuinfoFOSHAN.ContainsKey(insuinfoTemp.insutype) && (insuinfoTemp.poolarea_no.Substring(0, 4) == "4406"))
                                {
                                    insuinfoFOSHAN.Add(insuinfoTemp.insutype, insuinfoTemp);
                                }
                                if (!insuinfoHS.ContainsKey(insuinfoTemp.insutype))//非佛山险种不能享受
                                {
                                    insuinfoHS.Add(insuinfoTemp.insutype, insuinfoTemp);
                                }

                            }
                        }
                    }
                    if (insuinfoFOSHAN == null || insuinfoFOSHAN.Count <= 0)
                    {
                        ErrMsg = "没有找到参保人【佛山】参保的缴费信息!";
                        return -1;
                    }
                    if ((insuinfoHS == null || insuinfoHS.Count <= 0) && !string.IsNullOrEmpty(insuinfoGQXZ))
                    {
                        Models.Response.ResponseGzsiModel90100.Output insuinfoTemp = new Models.Response.ResponseGzsiModel90100.Output();
                        insuinfoTemp.insutype = insuinfoGQXZ;
                        insuinfoHS.Add(insuinfoGQXZ, insuinfoTemp);
                    }
                }
                else
                {
                    Models.Response.ResponseGzsiModel90100.Output insuinfoTemp = new Models.Response.ResponseGzsiModel90100.Output();
                    insuinfoTemp.insutype = "340";
                    insuinfoHS.Add("340", insuinfoTemp);
                }
                if (insuinfoHS == null || insuinfoHS.Count <= 0)
                {
                    ErrMsg = "没有找到参保人上月缴费到账的信息，请确定参保人的医保缴费信息!";
                    return -1;
                }

                if (insuinfoHS.ContainsKey("340"))//优先职工
                {
                    Models.Response.ResponseGzsiModel90100.Output insuinfoTemp = insuinfoHS["340"] as Models.Response.ResponseGzsiModel90100.Output;
                    if (insuinfoTemp != null)
                    {
                        Insutype = "340";
                    }
                }
                else if (insuinfoHS.ContainsKey("310"))//
                {
                    Models.Response.ResponseGzsiModel90100.Output insuinfoTemp = insuinfoHS["310"] as Models.Response.ResponseGzsiModel90100.Output;
                    if (insuinfoTemp != null)
                    {
                        Insutype = "310";
                    }
                }
                else if (insuinfoHS.ContainsKey("390"))//
                {
                    Models.Response.ResponseGzsiModel90100.Output insuinfoTemp = insuinfoHS["390"] as Models.Response.ResponseGzsiModel90100.Output;
                    if (insuinfoTemp != null)
                    {
                        Insutype = "390";
                    }
                }
                else//其他默认第一个
                {
                    foreach (string key in insuinfoHS.Keys)
                    {
                        Models.Response.ResponseGzsiModel90100.Output insuinfoTemp = insuinfoHS[key] as Models.Response.ResponseGzsiModel90100.Output;
                        if (insuinfoTemp != null)
                        {
                            Insutype = key;
                        }
                        break;
                    }
                }

                if (string.IsNullOrEmpty(Insutype))
                {
                    ErrMsg = "[" + Models.Config.HospitalCode + "]当前机构备案险种信息为空！";
                    return -1;
                }

                Models.Insuinfo insuinfoTemp1 = null;
                if (insuinfoFOSHAN != null && insuinfoFOSHAN.Count > 0)//优先佛山的
                {
                    insuinfoTemp1 = insuinfoFOSHAN[Insutype] as Models.Insuinfo;
                }
                if (insuinfoTemp1 == null)
                {
                    insuinfoTemp1 = insuinfoYXHS[Insutype] as Models.Insuinfo;
                }
                if (insuinfoTemp1 != null)
                {
                    r.SIMainInfo.Psn_type = insuinfoTemp1.psn_type;
                    r.SIMainInfo.Insutype = insuinfoTemp1.insutype;
                    if (!string.IsNullOrEmpty(poolarea_no12) && insuinfoTemp1.insuplc_admdvs.Substring(0, 4) != "4406")
                    {
                        r.SIMainInfo.Insuplc_admdvs = poolarea_no12;
                    }
                    else
                    {
                        r.SIMainInfo.Insuplc_admdvs = insuinfoTemp1.insuplc_admdvs;
                    }

                }
                else
                {
                    ErrMsg = "[" + Models.Config.HospitalCode + "]佛山险种信息为空！";
                    return -1;
                }
                #endregion
            }
            else
            {
                ErrMsg = "获取人员参保信息为空!" + responseGzsiModel1101.err_msg;
                return -1;
            }
            #endregion

            #region 根据参保地再查询下人员信息

            //QueryPersonDetail1101 queryPersonDetail1101Temp = new QueryPersonDetail1101();
            //Models.Request.RequestGdsiModel1101 inParamTemp = new FS.HIT.Plugins.SI.GDSI.Models.Request.RequestGdsiModel1101();
            //Models.Response.ResponseGdsiModel1101 responseGdsiModel1101Temp = new FS.HIT.Plugins.SI.GDSI.Models.Response.ResponseGdsiModel1101();
            //Models.Request.RequestGdsiModel1101.Data bbTemp = new FS.HIT.Plugins.SI.GDSI.Models.Request.RequestGdsiModel1101.Data();
            //bbTemp.mdtrt_cert_type = r.SIMainInfo.Mdtrt_cert_type;//就诊类型
            //bbTemp.mdtrt_cert_no = r.IDCard;//卡号
            //bbTemp.card_sn = "";
            //bbTemp.psn_cert_type = "";// mdtrt_cert_type == "02" ? "1" : "99";//就诊类型
            //bbTemp.psn_name = r.Name;
            //bbTemp.certno = r.IDCard;//卡号
            //inParam.data = bbTemp;
            //if (queryPersonDetail1101Temp.CallService(r.SIMainInfo.Insuplc_admdvs, inParamTemp, ref responseGdsiModel1101Temp) < 0)
            //{
            //    ErrMsg = "获取人员信息失败！" + queryPersonDetail1101Temp.ErrorMsg;
            //    return -1;
            //}
            #endregion

            return 1;
        }


        private int Cancel(string Mdtrt_id, string Setl_id, string Psn_no, ref string err)
        {
            //string Insuplc_admdvs = this.localMgr.GetInsuplcAdmdvs(Mdtrt_id, Setl_id);
            if (!string.IsNullOrEmpty(Setl_id))
            {
                #region 退费
                OutPatient2208 outPatient2208 = new OutPatient2208();
                Models.Request.RequestGzsiModel2208 RequestGzsiModel2208 = new Models.Request.RequestGzsiModel2208();
                Models.Response.ResponseGzsiModel2208 responseGzsiModel2208 = new Models.Response.ResponseGzsiModel2208();
                Models.Request.RequestGzsiModel2208.Mdtrtinfo data2208 = new Models.Request.RequestGzsiModel2208.Mdtrtinfo();
                data2208.setl_id = Setl_id;
                data2208.mdtrt_id = Mdtrt_id;
                data2208.psn_no = Psn_no;
                RequestGzsiModel2208.data = data2208;
                if (outPatient2208.CallService(RequestGzsiModel2208, ref responseGzsiModel2208) < 0)
                {
                    err = "退费失败！" + outPatient2208.ErrorMsg;
                    return -1;
                }
                if (responseGzsiModel2208.infcode != "0")
                {
                    err = "退费失败！！" + responseGzsiModel2208.err_msg;
                    return -1;
                }
                #endregion

                //return 1;
            }

            if (!string.IsNullOrEmpty(Mdtrt_id))
            {
                OutPatient2202 outPatient2202 = new OutPatient2202();
                Models.Request.RequestGzsiModel2202 RequestGzsiModel2202 = new Models.Request.RequestGzsiModel2202();
                Models.Response.ResponseGzsiModel2202 responseGzsiModel2202 = new Models.Response.ResponseGzsiModel2202();
                Models.Request.RequestGzsiModel2202.Mdtrtinfo data2202 = new Models.Request.RequestGzsiModel2202.Mdtrtinfo();
                data2202.mdtrt_id = Mdtrt_id;//就诊ID 202011031615
                data2202.psn_no = Psn_no;//人员编号 1000753288
                data2202.ipt_otp_no = Mdtrt_id;//住院/门诊号  0000735959
                RequestGzsiModel2202.data = new Models.Request.RequestGzsiModel2202.Mdtrtinfo();
                RequestGzsiModel2202.data = data2202;
                if (outPatient2202.CallService(RequestGzsiModel2202, ref responseGzsiModel2202) < 0)
                {
                    if (!string.IsNullOrEmpty(responseGzsiModel2202.err_msg))
                    {
                        err = "退号失败！" + responseGzsiModel2202.err_msg;
                    }
                    return -1;
                }
            }
            return 1;
        }

        private void btQuery_Click(object sender, EventArgs e)
        {
            if (this.tabMain.SelectedIndex == 0)
            {
                this.neuSpread1_Sheet1.RowCount = 0;
                DataTable dt = new DataTable();
                dt = this.localMgr.QueryALLNeedBalanceBaseInfo(this.dtBeginTime.Value.Date.ToString(), this.dtEndTime.Value.Date.AddDays(1).AddSeconds(-1).ToString());
                if (dt == null || dt.Rows.Count <= 0)
                {
                    MessageBox.Show("没有待结算信息！");
                    return;
                }
                SetNeedBaseInfo(dt);
            }
            else
            {
                this.neuSpread1_Sheet2.RowCount = 0;
                DataTable dt = new DataTable();
                dt = this.localMgr.QueryALLBalanceInfo(this.dtBeginTime.Value.Date.ToString(), this.dtEndTime.Value.Date.AddDays(1).AddSeconds(-1).ToString());
                if (dt == null || dt.Rows.Count <= 0)
                {
                    MessageBox.Show("没有已结算信息！");
                    return;
                }

                SetBalanceInfo(dt);
            }

        }

        private void SetNeedBaseInfo(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
            {
                return;
            }
            this.neuSpread1_Sheet1.RowCount = 0;
            int rowIndex = this.neuSpread1_Sheet1.Rows.Count;
            this.cbCheckAll.Checked = true;
            foreach (DataRow dRow in dt.Rows)
            {
                this.neuSpread1_Sheet1.Rows.Add(rowIndex, 1);
                this.neuSpread1_Sheet1.Cells[rowIndex, 0].Value = true;//FS.FrameWork.Function.NConvert.ToBoolean("1");
                this.neuSpread1_Sheet1.Cells[rowIndex, 1].Value = dRow["ID"].ToString();
                this.neuSpread1_Sheet1.Cells[rowIndex, 2].Value = dRow["CARD_TYPE"].ToString();
                this.neuSpread1_Sheet1.Cells[rowIndex, 3].Value = dRow["IDNO"].ToString();
                this.neuSpread1_Sheet1.Cells[rowIndex, 4].Value = dRow["NAME"].ToString();
                this.neuSpread1_Sheet1.Cells[rowIndex, 5].Value = dRow["SEE_DATE"].ToString();
                this.neuSpread1_Sheet1.Cells[rowIndex, 6].Value = dRow["CHECK_NUM"].ToString();
                this.neuSpread1_Sheet1.Cells[rowIndex, 7].Value = dRow["PRICE"].ToString();
                this.neuSpread1_Sheet1.Cells[rowIndex, 8].Value = dRow["TOT_COST"].ToString();
                this.neuSpread1_Sheet1.Cells[rowIndex, 9].Value = dRow["PATIENT_TYPE"].ToString();
                this.neuSpread1_Sheet1.Cells[rowIndex, 10].Value = dRow["ITEMINFO"].ToString();
                string type = "";
                if (dRow["PATIENTY_TYPE"].ToString() == "0")
                {
                    type = "Excel";
                }
                else if (dRow["PATIENTY_TYPE"].ToString() == "1")
                {
                    type = "门诊";
                }
                else if (dRow["PATIENTY_TYPE"].ToString() == "2")
                {
                    type = "住院";
                }
                else
                {
                    type = "其他";
                }
                this.neuSpread1_Sheet1.Cells[rowIndex, 11].Value = type;
                this.neuSpread1_Sheet1.Cells[rowIndex, 12].Value = dRow["SEE_DOCT_CODE"].ToString();
                this.neuSpread1_Sheet1.Cells[rowIndex, 13].Value = dRow["ERR"].ToString();

                this.neuSpread1_Sheet1.Cells[rowIndex, 0].Tag = dRow;
            }
        }
        private void SetBalanceInfo(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
            {
                return;
            }
            this.neuSpread1_Sheet2.RowCount = 0;
            int rowIndex = this.neuSpread1_Sheet2.Rows.Count;
            foreach (DataRow dRow in dt.Rows)
            {
                this.neuSpread1_Sheet2.Rows.Add(rowIndex, 1);
                this.neuSpread1_Sheet2.Cells[rowIndex, 0].Value = true;//FS.FrameWork.Function.NConvert.ToBoolean("1");
                this.neuSpread1_Sheet2.Cells[rowIndex, 1].Value = dRow["ID"].ToString();
                this.neuSpread1_Sheet2.Cells[rowIndex, 2].Value = dRow["CARD_TYPE"].ToString();
                this.neuSpread1_Sheet2.Cells[rowIndex, 3].Value = dRow["IDNO"].ToString();
                this.neuSpread1_Sheet2.Cells[rowIndex, 4].Value = dRow["NAME"].ToString();
                this.neuSpread1_Sheet2.Cells[rowIndex, 5].Value = dRow["SEE_DATE"].ToString();
                this.neuSpread1_Sheet2.Cells[rowIndex, 6].Value = dRow["CHECK_NUM"].ToString();
                this.neuSpread1_Sheet2.Cells[rowIndex, 7].Value = dRow["PATIENT_TYPE"].ToString();
                this.neuSpread1_Sheet2.Cells[rowIndex, 8].Value = dRow["MEDFEE_SUMAMT"].ToString();
                this.neuSpread1_Sheet2.Cells[rowIndex, 9].Value = dRow["FUND_PAY_SUMAMT"].ToString();
                this.neuSpread1_Sheet2.Cells[rowIndex, 10].Value = dRow["PSN_PART_AMT"].ToString();
                this.neuSpread1_Sheet2.Cells[rowIndex, 11].Value = dRow["MDTRT_ID"].ToString();
                this.neuSpread1_Sheet2.Cells[rowIndex, 12].Value = dRow["SETL_ID"].ToString();
                this.neuSpread1_Sheet2.Cells[rowIndex, 13].Value = dRow["ERR"].ToString();
                this.neuSpread1_Sheet2.Cells[rowIndex, 0].Tag = dRow;
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            if (this.tabMain.SelectedIndex == 0)
            {
                MessageBox.Show("请选择已结算界面！");
                return;
            }

            if (this.neuSpread1_Sheet2.Rows.Count <= 0)
            {
                MessageBox.Show("无需要撤销结算信息!");
                return;
            }
            //提示
            string strTips = "是否确定撤销结算！";
            if (MessageBox.Show(strTips, "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在撤销!请稍后!");

            for (int k = 0; k < this.neuSpread1_Sheet2.Rows.Count; k++)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(k, this.neuSpread1_Sheet2.Rows.Count);

                bool isChoose = FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet2.Cells[k, 0].Value);
                if (isChoose)
                {
                    DataRow dRow = this.neuSpread1_Sheet2.Cells[k, 0].Tag as DataRow;
                    if (dRow != null)
                    {
                        string err = "";
                        if (this.Cancel(dRow["MDTRT_ID"].ToString(), dRow["SETL_ID"].ToString(), dRow["PSN_NO"].ToString(), ref err) < 0)
                        {
                            if (this.localMgr.UpdateHSBalanceState(dRow["ID"].ToString(), "1", err) < 0)
                            {
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                MessageBox.Show("更新状态失败！");
                                return;
                            }
                        }
                        else
                        {
                            if (this.localMgr.UpdateHSBalanceState(dRow["ID"].ToString(), "0", "") < 0)
                            {
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                MessageBox.Show("更新状态失败！");
                                return;
                            }
                            if (this.localMgr.UpdateHSBalanceValidFlag(dRow["ID"].ToString(), dRow["MDTRT_ID"].ToString(), "0") < 0)
                            {
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                MessageBox.Show("更新状态失败！");
                                return;
                            }
                        }
                    }
                }
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show("操作完成！");
            btQuery_Click(null, null);

        }

        private void cbCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.tabMain.SelectedIndex == 0)
            {
                #region 待上传

                if (this.neuSpread1_Sheet1.Rows.Count > 0)
                {
                    for (int k = 0; k < this.neuSpread1_Sheet1.Rows.Count; k++)
                    {
                        if (this.cbCheckAll.Checked)
                        {
                            this.neuSpread1_Sheet1.Cells[k, 0].Value = true;
                        }
                        else
                        {
                            this.neuSpread1_Sheet1.Cells[k, 0].Value = false;
                        }
                    }
                }

                #endregion
            }
            else
            {
                #region 已上传

                if (this.neuSpread1_Sheet2.Rows.Count > 0)
                {
                    for (int k = 0; k < this.neuSpread1_Sheet2.Rows.Count; k++)
                    {
                        if (this.cbCheckAll.Checked)
                        {
                            this.neuSpread1_Sheet2.Cells[k, 0].Value = true;
                        }
                        else
                        {
                            this.neuSpread1_Sheet2.Cells[k, 0].Value = false;
                        }
                    }
                }

                #endregion
            }

        }


        List<FS.FrameWork.WinForms.Controls.NeuSpread> lstSpread = null;
        private void btOutPutExcel_Click(object sender, EventArgs e)
        {
            if (this.tabMain.SelectedIndex == 0)
            {
                if (this.neuSpread1_Sheet1.Rows.Count <= 0)
                {
                    MessageBox.Show("没有可导出的数据！");
                    return;
                }

                //提示
                string strTips = "将导出当前查询的全部待结算信息，是否导出信息？";
                if (MessageBox.Show(strTips, "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    return;
                }

                lstSpread = new List<FS.FrameWork.WinForms.Controls.NeuSpread>();
                lstSpread.Add(this.neuSpread1);
                if (lstSpread != null && lstSpread.Count > 0)
                {
                    if (lstSpread[0].Export() > 0)
                    {
                        MessageBox.Show("导出成功！导出的Excel将被保护，取消保护请点：审阅-撤销保护工作表，取消后方可修改！");
                    }
                    else
                    {
                        MessageBox.Show("导出失败！");
                    }
                }
            }
            else
            {
                if (this.neuSpread1_Sheet2.Rows.Count <= 0)
                {
                    MessageBox.Show("没有可导出的数据！");
                    return;
                }
                //提示
                string strTips = "将导出当前查询的全部已结算信息，是否导出信息？";
                if (MessageBox.Show(strTips, "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    return;
                }

                lstSpread = new List<FS.FrameWork.WinForms.Controls.NeuSpread>();
                lstSpread.Add(this.neuSpread2);
                if (lstSpread != null && lstSpread.Count > 0)
                {
                    if (lstSpread[0].Export() > 0)
                    {
                        MessageBox.Show("导出成功！导出的Excel将被保护，取消保护请点：审阅-撤销保护工作表，取消后方可修改！");
                    }
                    else
                    {
                        MessageBox.Show("导出失败！");
                    }
                }
            }

        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            if (this.tabMain.SelectedIndex != 0)
            {
                MessageBox.Show("请选择待结算界面！");
                return;
            }

            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("无需要删除信息!");
                return;
            }
            //提示
            string strTips = "是否确定删除导入的信息！";
            if (MessageBox.Show(strTips, "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在删除!请稍后!");

            for (int k = 0; k < this.neuSpread1_Sheet1.Rows.Count; k++)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(k, this.neuSpread1_Sheet1.Rows.Count);

                bool isChoose = FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[k, 0].Value);
                if (isChoose)
                {
                    DataRow dRow = this.neuSpread1_Sheet1.Cells[k, 0].Tag as DataRow;
                    if (dRow != null)
                    {
                        if (!this.localMgr.GetHSIsFee(dRow["ID"].ToString()))
                        {
                            MessageBox.Show("编号：" + dRow["ID"].ToString() + "，已经结算，无法删除！");
                            continue;
                        }
                        if (this.localMgr.DeleteHSBaseInfo(dRow["ID"].ToString()) < 0)
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show("删除信息失败！");
                            return;
                        }
                        //fin_syb_hsbalanceinfo也要删除
                        if (this.localMgr.DeleteHSBalanceinfo(dRow["ID"].ToString()) < 0)
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show("删除信息失败！");
                            return;
                        }
                    }
                }
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show("操作完成！");
            btQuery_Click(null, null);

        }

    }
}
