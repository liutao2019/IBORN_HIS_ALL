using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.WinForms.Report.Order
{
    /// <summary>
    /// [功能描述: 执行单控件]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2004-10-12]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucExecBill :  Report.Common.ucQueryBaseForDataWindow,FS.HISFC.BizProcess.Interface.IPrintTransFusion
    {
        /// <summary>
        ///  治疗单
        /// </summary>
        public ucExecBill()
        {
            InitializeComponent();
        }

        #region IPrintTransFusion 成员
        FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
        FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
        FS.HISFC.BizLogic.Manager.Bed manager = new FS.HISFC.BizLogic.Manager.Bed();
        dsExecBill ds = new dsExecBill();
        
        ArrayList curValues = null; //当前显示的数据
        bool isPrint = false;
        List<FS.HISFC.Models.RADT.PatientInfo> myPatients = null;
        DateTime dt1;
        DateTime dt2;
        string usage = "";
        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
        {
            try
            {
                if (this.dwMain.RowCount <= 0)
                {
                    return;
                }

                FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                FS.HISFC.Models.Base.PageSize pSize = pgMgr.GetPageSize("Nurse3");
                if (pSize != null)
                {
                    //dwMain.Modify("DataWindow.Print.Paper.Size=256");
                    ////此处设置letter纸为：216*279 像素为850*1100
                    //dwMain.Modify("DataWindow.Print.CustomPage.Length=1100");
                    //dwMain.Modify("DataWindow.Print.CustomPage.Width=425");
                }

                //for (int i = 0; i < myPatients.Count; i++)
                //{
                //    this.dwMain.SetFilter("patient_no=" + myPatients[i].ID);
                //    this.dwMain.Filter();
                //    MessageBox.Show("过滤完毕" + this.dwMain.RowCount.ToString());
                //    if (this.dwMain.RowCount > 0)
                //    {
                //        this.dwMain.Print(true, true);
                //    }
                //}

                //this.dwMain.SetFilter("1=1");
                //this.dwMain.Filter();

                //数据窗口打印
                //dwMain.Print(true, true);
                dwMain.Print();

                #region 更新已经打印标记
                if (!this.checkBox1.Checked)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(orderManager.Connection);
                    //t.BeginTransaction();

                    this.orderManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    string itemType = null;
                    for (int i = 0; i < this.curValues.Count; i++)
                    {
                        FS.HISFC.Models.Order.ExecOrder exeord = curValues[i] as FS.HISFC.Models.Order.ExecOrder;
                        //if (exeord.Order.Item.IsPharmacy)
                        if (exeord.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            itemType = "1";
                        }
                        else
                        {
                            itemType = "2";
                        }
                        if (this.orderManager.UpdateExecOrderPrinted(exeord.ID, itemType) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新打印标记失败!" + orderManager.Err);
                            return;
                        }
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();

                    this.Query(myPatients, usage, dt1, dt2, isPrint);
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        public void PrintSet()
        {
            print.ShowPrintPageDialog();
            this.Print();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="patients"></param>
        /// <param name="usagecode"></param>
        /// <param name="dtTime"></param>
        /// <param name="isPrinted"></param>
        public void Query(List<FS.HISFC.Models.RADT.PatientInfo> patients, string usagecode, DateTime dtTime, bool isPrinted)
        {
            return;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="patients"></param>
        /// <param name="usageCode"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="isPrinted"></param>
        public void Query(List<FS.HISFC.Models.RADT.PatientInfo> patients, string usageCode, DateTime dtBegin, DateTime dtEnd, bool isPrinted)
        {
            //给患者列表赋值
            this.myPatients = patients;
            //更改治疗单标题
            this.dwMain.Modify("t_title.text= " + "'" + this.Tag.ToString() + "'");
            #region {0A2D3FF0-85B0-4322-8E0A-9E11D02344EF}
            try
            {
                FS.HISFC.BizProcess.Integrate.Manager managerMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                string hosname = managerMgr.GetHospitalName();
                this.dwMain.Modify("t_hospitalname.text= " + "'" + hosname + "'");
            }
            catch { }
            #endregion
            ArrayList alOrder = new ArrayList();
            ArrayList al = new ArrayList();
            string paramPatient = "";
            //获得in的患者id参数
            for (int i = 0; i < patients.Count; i++)
            {
                FS.HISFC.Models.RADT.PatientInfo p = patients[i] as FS.HISFC.Models.RADT.PatientInfo;
                paramPatient ="'" + p.ID + "'," +paramPatient;
                //获得护理分组
             
                p.PVisit.PatientLocation.Bed.Memo = manager.GetNurseTendGroupFromBed(p.PVisit.PatientLocation.Bed.ID);
                
                if (p.PVisit.PatientLocation.Bed == null)
                {
                    MessageBox.Show(manager.Err);
                    return ;
                }
            }

            if (paramPatient == "")
            {
                paramPatient = "''";
            }
            else
            {
                paramPatient = paramPatient.Substring(0, paramPatient.Length - 1);//去掉后面的逗号
            }

            
            alOrder = this.orderManager.QueryOrderExecBill(paramPatient, dtBegin, dtEnd,usageCode,isPrinted);//查询医嘱，传入参数

            this.curValues = alOrder;

            #region 将同一条医嘱合并在一起
            ArrayList alTemp = alOrder.Clone() as ArrayList;
            alOrder = new ArrayList();
            for (int k = 0; k < alTemp.Count; k++)
            {
                //if (isPrinted && ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).Order.BeginTime.Date < dtBegin.Date
                //    && ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse < new DateTime(dtBegin.Year, dtBegin.Month, dtBegin.Day, 12, 1, 0))
                //{
                //    continue;//去掉非首日量的，使用时间小于今天12点的时间
                //}

                bool isHave = false;

                TimeSpan span;
                string sMing = "";
                for (int j = 0; j < alOrder.Count; j++)
                {
                    sMing = "";
                    if (((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.ID == ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).Order.ID)
                    {
                        if (((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Memo.Length > 2 && 
                            ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Memo.Substring(0, 2) == "时间")
                        {
                            //特殊频次
                        }
                        else
                        {
                            isHave = true;//包含添加时间
                            span = new TimeSpan(((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Date.Ticks - dtBegin.Date.Ticks);
                            if (span.Days == 1) sMing = "明";
                            if (span.Days == 2) sMing = "后";
                            if (span.Days > 2) sMing = "[" + ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Day.ToString() + "日]";
                            ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Memo += "," + sMing + ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Hour.ToString().PadLeft(2, '0');
                            break;
                        }
                    }
                }
                if (!isHave)
                {
                    span = new TimeSpan(((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Date.Ticks - dtBegin.Date.Ticks);
                    if (span.Days == 1) sMing = "明";
                    if (span.Days == 2) sMing = "后";
                    if (span.Days > 2) sMing = "[" + ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Day.ToString() + "日]";
                    if (((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).Order.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)//临时医嘱暂时不显示执行时间 by zuowy
                        ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).Memo = sMing + ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Hour.ToString().PadLeft(2, '0');
                    alOrder.Add(alTemp[k]);
                }
          
                #endregion
              

            }
            string Combno = "";
            ArrayList alComb = new ArrayList();
            #region 至组合号
            for (int j = 0; j < alOrder.Count; j++)
            {
                FS.HISFC.Models.Order.ExecOrder obj;

                obj = (FS.HISFC.Models.Order.ExecOrder)alOrder[j];
                for (int kk = 0; kk < patients.Count; kk++)
                {
                    if (((FS.FrameWork.Models.NeuObject)patients[kk]).ID == obj.Order.Patient.ID)
                    {
                        obj.Order.Patient = ((FS.HISFC.Models.RADT.PatientInfo)patients[kk]).Clone();
                        break;
                    }
                }
                //判断组合
                if (obj.Order.Combo.ID != "0" && obj.Order.Combo.ID != "")
                {
                    if (Combno != obj.Order.Combo.ID + obj.DateUse.ToString())
                    {
                        //非组合
                        if (alComb.Count == 1) al.Add((FS.HISFC.Models.Order.ExecOrder)alComb[0]);
                        //组合
                        else if (alComb.Count > 1)
                        {
                            for (int n = 0; n < alComb.Count; n++)
                            {
                                FS.HISFC.Models.Order.ExecOrder objC;
                                objC = (FS.HISFC.Models.Order.ExecOrder)alComb[n];
                                if (n == 0) objC.Order.Combo.Memo = "┏";
                                else if (n == alComb.Count - 1) objC.Order.Combo.Memo = "┗";
                                else objC.Order.Combo.Memo = "┃";
                                al.Add(objC);
                            }
                        }
                        alComb = new ArrayList();
                        alComb.Add(obj);
                        Combno = obj.Order.Combo.ID + obj.DateUse.ToString();
                        if (j == alOrder.Count - 1) al.Add(obj);//最后一条
                    }
                    else
                    {
                        alComb.Add(obj);
                        if (j == alOrder.Count - 1)
                        {
                            for (int row = 0; row < alComb.Count; row++)
                            {
                                FS.HISFC.Models.Order.ExecOrder exe = alComb[row] as FS.HISFC.Models.Order.ExecOrder;
                                if (alComb.Count == 1)
                                {
                                    al.Add(exe);
                                    break;
                                }
                                if (row == 0) exe.Order.Combo.Memo = "┏";
                                else if (row == alComb.Count - 1) exe.Order.Combo.Memo = "┗";
                                else exe.Order.Combo.Memo = "┃";
                                al.Add(exe);
                            }
                        }
                    }
                }
                else al.Add(obj);
            }
            #endregion
            if (this.dwMain.RowCount > 0)
            {
                this.dwMain.Reset();
            }
            this.SetValues(al);
            this.dwMain.CalculateGroups();
            return;

        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="alValues"></param>
        protected void SetValues(ArrayList alValues)
        {
           // curValues = alValues;
            if (alValues != null)
            {
                if (this.ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ds.Tables[0].Rows.Clear();
                    }
                }
                this.AddConstsToTable(alValues, ds.Tables[0]);
                
                this.dwMain.BindAdoDataTable(ds.Tables[0]);
                this.dwMain.CalculateGroups();
                //this.dwMain.Retrieve(ds.Tables[0]);
            }
            
        }
   
        private void AddConstsToTable(ArrayList list, DataTable table)
        {
            if (table.Rows.Count > 0)
            {
                table.Rows.Clear();
            }
            #region {85F71883-49E9-4a10-BDA9-203F3700343B}
            string strDate = this.dt1.DayOfWeek + "," + this.dt1.ToShortDateString();
            string strDate2 = this.dtpBeginTime.Value.ToString("yy-MM-dd") + "至" + this.dtpEndTime.Value.ToString("yy-MM-dd");
            //暂时借用user03存储药品性质，age存储查询时间段
            #endregion
            foreach (FS.HISFC.Models.Order.ExecOrder objExc in list)
            {

                //if (!objExc.Order.Item.IsPharmacy)
                if (objExc.Order.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    objExc.Order.DoseOnce = 0;
                    objExc.Order.DoseUnit = "";
                }

                try
                {

                    //if (objExc.Order.Item.IsPharmacy == false)
                    if (objExc.Order.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        if (objExc.Order.OrderType.Type == FS.HISFC.Models.Order.EnumType.SHORT)
                        {
                            if (objExc.Order.IsEmergency == true)
                            {
                                if (objExc.Order.Note != "")
                                    objExc.Memo = "加急[" + objExc.Order.Note + "]";
                                else
                                    objExc.Memo = "加急";
                            }
                            else
                            {
                                if (objExc.Order.Note != "")
                                    objExc.Memo = objExc.Order.Note;
                                else
                                    objExc.Memo += "";
                            }
                        }
                        else
                        {
                            objExc.Order.DoseOnce = objExc.Order.Qty;
                            objExc.Order.DoseUnit = objExc.Order.Unit;
                            objExc.Memo += objExc.Order.Note;
                        }
         
                        if (objExc.Order.Memo == objExc.Memo) objExc.Memo = "";

                        if (objExc.Order.OrderType.ID == "BL")//补录医嘱,备注显示补录字样。
                            objExc.Order.Memo = objExc.Order.Memo + "[补录]";
                        FS.HISFC.Models.Fee.Item.Undrug objAssets = objExc.Order.Item as FS.HISFC.Models.Fee.Item.Undrug;
                        table.Rows.Add(new Object[]{objExc.Order.Patient.PID.PatientNO,
													   objExc.Order.Patient.Name,
													   (objExc.Order.Patient.PVisit.PatientLocation.Bed.ID),
													   objExc.Order.Patient.PVisit.PatientLocation.Bed.Memo,
													   objExc.Order.OrderType.ID,
													   FS.FrameWork.Function.NConvert.ToInt32(objExc.Order.OrderType.IsDecompose),
													   objExc.Order.Item.ID,
													   objExc.Order.Item.Name,
													   objExc.Order.DoseOnce,
													   objExc.Order.DoseUnit,
													   objExc.Order.Usage.Name,
													   objExc.Order.Combo.Memo,
													   strDate,
													   objExc.Order.Frequency.ID,
													   objExc.Order.Qty,
													   objExc.Order.Unit,
													   objExc.Order.Combo.ID,
													   FS.FrameWork.Function.NConvert.ToInt32(objExc.Order.Combo.IsMainDrug),
													   FS.FrameWork.Function.NConvert.ToInt32(objExc.Order.OrderType.IsCharge),
													   objExc.Order.ReciptNO,
													   objExc.Order.BeginTime,
													   "", //收费标志
													   objExc.Order.Memo.TrimStart(), //备注
													   objExc.Memo.TrimStart(),
													   "",
													   "",
							                           FS.FrameWork.Function.NConvert.ToInt32(objExc.IsCharge)                                                
                                                       ,""//execorderid
                                                       ,""//itemtype
                                                       ,objExc.Order.Patient.Sex.Name//sex
                                                       ,strDate2//objExc.Order.Patient.Age//age
                                                       //,8m
                                                       ,FS.FrameWork.Public.String.FormatNumber(
                                                               FS.FrameWork.Function.NConvert.ToDecimal(
                                                               objAssets.Qty * (objAssets.Price )
                                                               ),
                                                               4
                                                               )//tot_cost
												   });
                    }
                    else
                    {
                        if (objExc.Order.OrderType.Type == FS.HISFC.Models.Order.EnumType.SHORT)
                        {
                            if (objExc.Order.Note != "")
                                objExc.Memo = objExc.Order.Note;
                            else
                                objExc.Memo += "";
                        }
                        else
                            objExc.Memo += objExc.Order.Note;
    
                        if (objExc.Order.Memo == objExc.Memo) objExc.Memo = "";
                        if (objExc.Order.Memo != "" && objExc.Order.Memo.IndexOf("需皮试") != -1)
                        {
                            try
                            {
                                //int hypotest = this.Order.QueryOrderHypotest(objExc.Order.ID);
                                //switch (hypotest)
                                //{
                                //    case 2:
                                //        objExc.Order.Memo = "需皮试";
                                //        break;
                                //    case 3:
                                //        objExc.Order.Memo = "阳性";
                                //        break;
                                //    case 4:
                                //        objExc.Order.Memo = "阴性";
                                //        break;
                                //    default:
                                //        objExc.Order.Memo = "需皮试";
                                //        break;
                                //}
                            }
                            catch
                            {
                                MessageBox.Show("获得皮试信息出错！", "Note");
                            }

                        }
                        if (objExc.Order.OrderType.ID == "BL")//补录医嘱,备注显示补录字样。
                            objExc.Order.Memo = objExc.Order.Memo + "[补录]";
                        FS.HISFC.Models.Pharmacy.Item objPharmacy = objExc.Order.Item as FS.HISFC.Models.Pharmacy.Item ;
                        table.Rows.Add(new Object[]{objExc.Order.Patient.PID.PatientNO,
													   objExc.Order.Patient.Name,
													   (objExc.Order.Patient.PVisit.PatientLocation.Bed.ID),
													   objExc.Order.Patient.PVisit.PatientLocation.Bed.Memo,
													   objExc.Order.OrderType.ID,
													   FS.FrameWork.Function.NConvert.ToInt32(objExc.Order.OrderType.IsDecompose),
													   objExc.Order.Item.ID,
													   objExc.Order.Item.Name,
													   objExc.Order.DoseOnce,
													   objExc.Order.DoseUnit,
													   objExc.Order.Usage.Name,
													   objExc.Order.Combo.Memo,
													   strDate,
													   objExc.Order.Frequency.ID,
													   objExc.Order.Qty,
													   objExc.Order.Unit,
													   objExc.Order.Combo.ID,
													   FS.FrameWork.Function.NConvert.ToInt32(objExc.Order.Combo.IsMainDrug),
													   FS.FrameWork.Function.NConvert.ToInt32(objExc.Order.OrderType.IsCharge),
													   objExc.Order.ReciptNO,
													   objExc.Order.BeginTime,
													   "", //收费标志
													   objExc.Order.Memo.TrimStart(), //备注
													   objExc.Memo.TrimStart(),
													   "【"+objExc.Order.Item.Specs+"】",
													   objPharmacy.Quality.ID,
							                           FS.FrameWork.Function.NConvert.ToInt32(objExc.IsCharge)
                                                       ,""//execorderid
                                                       ,""//itemtype
                                                       ,objExc.Order.Patient.Sex.Name//sex
                                                       ,strDate2//objExc.Order.Patient.Age//age
                                                       //,8m
                                                       ,FS.FrameWork.Public.String.FormatNumber(
                                                               FS.FrameWork.Function.NConvert.ToDecimal(
                                                               objPharmacy.Qty*(objPharmacy.Price / objPharmacy.PackQty)
                                                               ),
                                                               4
                                                               )//tot_cost
												   });
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            }
        }

        private void ucExecBill_Load(object sender, EventArgs e)
        {
            if (base.GetQueryTime() == -1)
            {
                return;
            }
            else
            {
                this.dt1 = this.dtpBeginTime.Value;
                this.dt2 = this.dtpEndTime.Value;
            }
        }


        #region IPrintTransFusion 成员


        public void Query(List<FS.HISFC.Models.RADT.PatientInfo> patients, string usagecode, DateTime dtBegin, DateTime dtEnd, bool isPrinted, string orderType)
        {
            return;
        }

        public void Query(List<FS.HISFC.Models.RADT.PatientInfo> patients, string usagecode, DateTime dtBegin, DateTime dtEnd, bool isPrinted, bool isFirst)
        {
            return;
        }

        public void Query(List<FS.HISFC.Models.RADT.PatientInfo> patients, string usagecode, DateTime dtBegin, DateTime dtEnd, bool isPrinted,string orderType, bool isFirst)
        {
            return;
        }
        #endregion

        #region IPrintTransFusion 成员


        public void SetSpeOrderType(string speStr)
        {
            return;
        }

        #endregion

        #region IPrintTransFusion 成员

        public bool DCIsPrint
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool NoFeeIsPrint
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool QuitFeeIsPrint
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
