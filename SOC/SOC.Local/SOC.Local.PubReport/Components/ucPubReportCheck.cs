using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.PubReport.Components
{
    public partial class ucPubReportCheck : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPubReportCheck()
        {
            InitializeComponent();
            this.Load += new EventHandler(ucPubReportCheck_Load);
            ucPubReportObj1.KeyDown += new KeyEventHandler(ucPubReportObj1_KeyDown);
            ucPubReportObj1.btnNext.Click += new EventHandler(btnNext_Click);
            ucPubReportObj1.btnPre.Click += new EventHandler(btnPre_Click);
        }

        void btnPre_Click(object sender, EventArgs e)
        {
            MoveDownData();
            int activeRow = this.fpSpread1_Sheet1.ActiveRowIndex;
            SOC.Local.PubReport.Models.PubReport obj = ucPubReportObj1.ReadFromPanel();
            WriteToFp(this.fpSpread1_Sheet1.ActiveRowIndex, obj);
            if (activeRow > 0)
            {
                activeRow--;
            }
            this.fpSpread1_Sheet1.SetActiveCell(activeRow, 0);
            obj = this.fpSpread1_Sheet1.ActiveRow.Tag as SOC.Local.PubReport.Models.PubReport;
            if (obj == null)
            {
                return;
            }
            ucPubReportObj1.WriteToPanel(obj);
            ucPubReportObj1.Check(obj);
        }

        void btnNext_Click(object sender, EventArgs e)
        {
            int activeRow = this.fpSpread1_Sheet1.ActiveRowIndex;
            MoveDownData();
            SOC.Local.PubReport.Models.PubReport obj = ucPubReportObj1.ReadFromPanel();
            WriteToFp(this.fpSpread1_Sheet1.ActiveRowIndex, obj);
            if (activeRow < this.fpSpread1_Sheet1.Rows.Count - 1)
            {
                activeRow++;
            }
            this.fpSpread1_Sheet1.SetActiveCell(activeRow, 0);
            obj = this.fpSpread1_Sheet1.ActiveRow.Tag as SOC.Local.PubReport.Models.PubReport;
            if (obj == null)
            {
                return;
            }
            ucPubReportObj1.WriteToPanel(obj);
            ucPubReportObj1.Check(obj);
        }

       
        #region 变量
        public DateTime Begin
        {
            get
            {
                return this.dtBegin.Value;
            }
        }
        public DateTime End
        {
            get
            {
                return this.dtEnd.Value;
            }
        }


        //DataTable dsInvoice = null;
        ArrayList alNeedDelete = new ArrayList();
        private SOC.Local.PubReport.BizLogic.PubReport pubMgr = new SOC.Local.PubReport.BizLogic.PubReport();
        private SOC.Local.PubReport.BizProcess.PubReport IntergrateMgr = new SOC.Local.PubReport.BizProcess.PubReport();
        Hashtable ItemRecord = new Hashtable();

        /// <summary>
        /// 门诊/住院
        /// </summary>
        public enum PatientType
        {
            /// <summary>
            /// 门诊
            /// </summary>
            C,
            /// <summary>
            /// 住院
            /// </summary>
            I
        }

        bool isModify = false;
        /// <summary>
        /// 窗口功能(true为修改,false为复核)
        /// </summary>
        public bool IsModify
        {
            get
            {
                return isModify;
            }
            set
            {
                isModify = value;
            }
        }

        PatientType patientType = PatientType.C;

        /// <summary>
        /// 患者类型
        /// </summary>
        public PatientType MyPatientType
        {
            get
            {
                return patientType;
            }
            set
            {
                this.patientType = value;
            }
        }


        #endregion

        #region 用户事件q`1

        class pubSortbySortid : System.Collections.IComparer
        {

            #region IComparer 成员

            public pubSortbySortid()
            {
            }

            public int Compare(object x, object y)
            {
                if (x == null || y == null)
                {
                    return 0;
                }
                if (x is SOC.Local.PubReport.Models.PubReport && y is SOC.Local.PubReport.Models.PubReport)
                {
                    SOC.Local.PubReport.Models.PubReport px = x as SOC.Local.PubReport.Models.PubReport;
                    SOC.Local.PubReport.Models.PubReport py = y as SOC.Local.PubReport.Models.PubReport;
                    int r = px.SortId.CompareTo(py.SortId);

                    return r;

                }
                else
                {
                    return 0;
                }
            }

            #endregion
        }

        class pubSort : System.Collections.IComparer
        {

            #region IComparer 成员

            public pubSort()
            {}

            public int Compare(object x, object y)
            {
                if(x == null || y == null)
                {
                    return 0;
                }
                if (x is SOC.Local.PubReport.Models.PubReport && y is SOC.Local.PubReport.Models.PubReport)
                {
                    SOC.Local.PubReport.Models.PubReport px = x as SOC.Local.PubReport.Models.PubReport;
                    SOC.Local.PubReport.Models.PubReport py = y as SOC.Local.PubReport.Models.PubReport;
                    int r = px.OperCode.CompareTo(py.OperCode);
                    if (r == 0)
                    {
                        return px.ID.CompareTo(py.ID);
                    }
                    else
                    {
                        return r;
                    }

                }
                else
                {
                    return 0;
                }
            }

            #endregion
        }

        private void Query()
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("数据查询中，请稍后.......");
                Application.DoEvents();

                this.fpSpread1_Sheet1.Rows.Count = 0;
               // this.dsInvoice.Clear();
                this.ItemRecord.Clear();
                ArrayList alPubReport = new ArrayList();
                this.alNeedDelete = new ArrayList();
                for (int i = 0; i < this.tvList.Nodes.Count; i++)
                {
                    for (int j = 0; j < this.tvList.Nodes[i].Nodes.Count; j++)
                    {
                        for (int k = 0; k < this.tvList.Nodes[i].Nodes[j].Nodes.Count; k++)
                        {
                            ArrayList al = new ArrayList();
                            if (this.tvList.Nodes[i].Nodes[j].Nodes[k].Checked)
                            {
                                string PactHead = this.tvList.Nodes[i].Nodes[j].Tag.ToString();
                                //if(UserCode=="38") UserCode="03";//换章号处理
                                string UserCode = this.tvList.Nodes[i].Nodes[j].Nodes[k].Tag.ToString();
                                //al = this.pubMgr.GetGfDetail(UserCode, PactHead, Begin, End);

                                if (IsModify)
                                {
                                    if (this.patientType == PatientType.C)
                                    {
                                        al = pubMgr.QueryPubFeeInfoByOperAndPactheadStaticMonth(UserCode, PactHead, this.GetStaticMonth(), "1");
                                    }
                                    else
                                    {
                                        al = pubMgr.QueryPubFeeInfoByOperAndPactheadStaticMonth(UserCode, PactHead, this.GetStaticMonth(), "2");
                                    }
                                }
                                else
                                {
                                    if (this.patientType == PatientType.C)
                                    {
                                        al = pubMgr.QueryClinicPubFeeInfoByOperAndPactheadHistory(UserCode, PactHead, Begin, End, "1", IsModify);
                                    }
                                    else
                                    {
                                        al = pubMgr.QueryPubFeeInfoByOperAndPactheadHistory(UserCode, PactHead, Begin, End, "2", IsModify);
                                    }
                                }
                                alPubReport.AddRange(al);
                            }
                        }
                    }
                }
                System.Collections.IComparer sort;
                if (this.IsModify)
                {
                    sort = new pubSortbySortid();
                }
                else
                {
                    sort = new pubSort();
                }
                
                alPubReport.Sort(sort);
                foreach (SOC.Local.PubReport.Models.PubReport obj in alPubReport)
                {
                    if (!IsModify)//复核时默认不选
                    {
                        obj.IsValid = "0";
                    }
                }
                     
                this.WriteToFp(alPubReport); //加进汇总dataset	
                //SetItemRecord(ItemRecord, al);//记录变换信息
                //this.fpSpread1_Sheet1.DataSource = dsInvoice;
                this.setStatuebar();
                this.SetFormat();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            catch (Exception ex)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(ex.Message);
            }

        }

        private void Print()
        {


        }
       
        private void Export()
        {
            try
            {
                string fileName = "";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".xls";
                dlg.Filter = "Microsoft Excel 工作薄 (*.xls)|*.*";
                dlg.FileName = "012GY" + this.dtEnd.Value.Year.ToString() + this.dtEnd.Value.Month.ToString().PadLeft(2, '0') + "01";
                DialogResult result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    fileName = dlg.FileName;
                    this.fpSpread1.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

         /// <summary>
        /// 从表格中读取公费汇总信息
        /// </summary>
        /// <param name="i">fp的行</param>
        /// <param name="pubObj"></param>
        /// <returns></returns>
        private int GetPubObjFromFp(int i, ref SOC.Local.PubReport.Models.PubReport pubObj,ref string err)
        {
            try
            {
                pubObj = this.fpSpread1_Sheet1.Rows[i].Tag as SOC.Local.PubReport.Models.PubReport;
                //pubObj.ID = this.fpSpread1_Sheet1.Cells[i, (int)Col.ID].Text;
                //pubObj.User01 = this.fpSpread1_Sheet1.Cells[i, (int)Col.流水号].Text;
                //pubObj.ID = this.fpSpread1_Sheet1.Cells[i, (int)Col.发票号].Text;
                pubObj.Pact.Memo = this.fpSpread1_Sheet1.Cells[i, (int)Col.字头].Text;
                pubObj.MCardNo = this.fpSpread1_Sheet1.Cells[i, (int)Col.医疗证号].Text;
                pubObj.Name = this.fpSpread1_Sheet1.Cells[i, (int)Col.姓名].Text;
                pubObj.Pact.Name = this.fpSpread1_Sheet1.Cells[i, (int)Col.结算方式].Text;
                pubObj.Pub_Cost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)Col.记帐金额].Value);
                pubObj.Tot_Cost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)Col.总金额].Value);
                pubObj.OperCode = this.fpSpread1_Sheet1.Cells[i, (int)Col.工号].Text;
                pubObj.YaoPin = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)Col.药品].Value);
                pubObj.ChengYao = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)Col.成药].Value);
                pubObj.CaoYao = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)Col.草药].Value);
                pubObj.JianCha = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)Col.检查].Value);
                pubObj.TeJian = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)Col.特殊检查].Value);
                pubObj.ZhiLiao = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)Col.治疗].Value);
                pubObj.ShouShu = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)Col.手术].Value);
                pubObj.CT = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)Col.CT].Value);
                pubObj.MR = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)Col.MR].Value);
                pubObj.ZhenJin = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)Col.诊金].Value);
                pubObj.ShuYang = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)Col.输氧].Value);
                pubObj.ShuXue = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)Col.输血].Value);
                pubObj.HuaYan = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)Col.化验].Value);
                pubObj.Bed_Fee= FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)Col.床位].Value);
                pubObj.JianHu= FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)Col.监护].Value);
                pubObj.Begin = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.Cells[i, (int)Col.入院日期].Value);
                pubObj.End = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.Cells[i, (int)Col.出院日期].Value);
                pubObj.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.Cells[i, (int)Col.结算日期].Value);
                pubObj.WorkName = this.fpSpread1_Sheet1.Cells[i, (int)Col.单位名称].Text;
                pubObj.WorkCode = this.fpSpread1_Sheet1.Cells[i, (int)Col.单位代号].Text;
                pubObj.PatientNO = this.fpSpread1_Sheet1.Cells[i, (int)Col.住院号].Text;
                pubObj.Amount = FS.FrameWork.Function.NConvert.ToInt32(this.fpSpread1_Sheet1.Cells[i,(int)Col.天数].Value);


                if (this.fpSpread1_Sheet1.Cells[i, 0].Text.ToUpper() == "TRUE")
                {

                    pubObj.IsValid = "1";
                    pubObj.Static_Month = GetStaticMonth();
                }
                else
                {
                    pubObj.IsValid = "0";
                    pubObj.Static_Month = DateTime.MinValue;
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return -1;
            }
            return 1;
        }

        DateTime GetStaticMonth()
        {
            DateTime dt = this.txtStaticMonth.GetStaticMonth();
            if (dt == DateTime.MinValue)
            {
                MessageBox.Show(this.txtStaticMonth.Err);
            }
            return dt;
        }

        private void GethorPubReportFromInvoice()
        {
            string feeType = "";
            if (this.patientType == PatientType.C)
            {
                feeType = "1";
            }
            else
            {
                feeType = "2";
            }
            int count = this.pubMgr.GetPubReportCount(this.dtBegin.Value, this.dtEnd.Value, feeType);
            if (count != 0)
            {
                MessageBox.Show("这个时间段内已经采集过数据了,不能重复采集");
                return;
            }
            ArrayList al = new ArrayList();
            if (this.patientType == PatientType.C)
            {
                al = this.IntergrateMgr.QueryPubFeeInfoClinic(this.dtBegin.Value,this.dtEnd.Value,true);
            }
            else
            {
                al = this.IntergrateMgr.QueryPubFeeInfoInpatient(this.dtBegin.Value, this.dtEnd.Value);
            }

            if (al == null)
            {
                MessageBox.Show("没有数据可以采集");
                return;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.pubMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            foreach (SOC.Local.PubReport.Models.PubReport pubObj in al)
            {
                if (pubObj.Seq == 0)
                {
                    pubObj.Seq = pubMgr.GetPubReortSeq(pubObj.ID);
                }
                if (this.pubMgr.InsertPubReport(pubObj) == -1)
                {
                     FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("数据保存失败 " + this.pubMgr.Err);
                    return;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("记账单采集完毕");

        }

        public FS.FrameWork.Models.NeuObject GetStaticTimeObj()
        {
            FS.FrameWork.Models.NeuObject sObj = new FS.FrameWork.Models.NeuObject();
            DateTime staticMonth = GetStaticMonth();
            sObj.ID = staticMonth.Year.ToString();
            sObj.Memo = staticMonth.Month.ToString().PadLeft(2,'0');
            sObj.User01 = this.dtBegin.Value.ToString();
            sObj.User02 = this.dtEnd.Value.ToString();
            return sObj;
        }

        private void Save()
        {

            if (this.GetStaticMonth() == DateTime.MinValue)
            {
                return;
            }
            DateTime staticMonth = GetStaticMonth();
            if (this.pubMgr.MonthLocked(staticMonth) == true)
            {
                MessageBox.Show(staticMonth.Year + "年" + staticMonth.Month + "月已经封账,需要修改,请先解封" );
                return;
            }

            //////FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(this.pubMgr.Connect);
            //////trans.BeginTransaction();
            //////this.pubMgr.SetTrans(trans.Trans);

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.pubMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            

            //获取一个对账单号码
            //string BillNo = this.pubMgr.GetSeq("SEQ_GFHZ_BILLNO").PadLeft(10,'0');
            FS.FrameWork.Models.NeuObject sObj = this.GetStaticTimeObj();
            int state = 0;
            state = this.pubMgr.UpdateStaticTime(sObj) ;
            if(state == 0)
            {
                state = this.pubMgr.InsertStaticTime(sObj);
            }
            if (state == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("更新统计时间错误");
                return;
            }
            foreach (SOC.Local.PubReport.Models.PubReport dObj in alNeedDelete)
            {
                if (pubMgr.DeletePubReport(dObj.ID, dObj.Seq) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("删除合并数据失败");
                    return;
                }
            }
            try
            {
                //更新记帐单对应明细
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {

                    SOC.Local.PubReport.Models.PubReport pubObj = this.fpSpread1_Sheet1.Rows[i].Tag as SOC.Local.PubReport.Models.PubReport;
                    if (pubObj == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("从表格的第" + i.ToString() + "行读取公费汇总信息出错");
                        return;
                    }
                    if (pubObj.Static_Month != DateTime.MinValue && pubObj.Static_Month != staticMonth && this.pubMgr.MonthLocked(pubObj.Static_Month))
                    {
                         FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("第"+ (i+1).ToString() + "行已经在" + pubObj.Static_Month.Year.ToString() + "年" + pubObj.Static_Month.Month.ToString() + "月的报表中,不能再修改");
                        return;
                    }

                    string errMsg = "";
                    if (this.GetPubObjFromFp(i, ref pubObj, ref errMsg) == -1)
                    {
                         FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("从表格的第" + i.ToString() + "行读取公费汇总信息出错 " + errMsg);
                        return;
                    }
                    if (pubObj.Pact.ID == "" || pubObj.MCardNo == "")
                    {
                         FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("数据保存不成功:Pactcode/McardNo is Empty.");
                        return;
                    }

                    //this.pubMgr.UpdateBillNO(BillID,BillNo,"1");
                    if (pubObj.Seq == 0)
                    {
                        pubObj.Seq = pubMgr.GetPubReortSeq(pubObj.ID);
                    }
                    if ((pubObj.SortId == null || pubObj.SortId.Trim() == "")&&pubObj.IsValid == "1")
                    {
                        pubObj.SortId = pubMgr.GetSeq("SEQ_GFHZ_BILLNO");
                    }
                    if ((pubObj.SortId != null && pubObj.SortId.Trim() != "") && pubObj.IsValid == "0") //不复核的则清掉顺序号,等下次复核时再从新生成顺序号
                    {
                        pubObj.SortId = "";
                    }
                    int iReturn;
                    iReturn = this.pubMgr.UpdatePubReport(pubObj);
                    if (iReturn == 0)
                    {
                        iReturn = this.pubMgr.InsertPubReport(pubObj);
                    }
                    if (iReturn == -1)
                    {
                         FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("数据保存失败 " + this.pubMgr.Err);
                        return;
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("数据保存完毕");
                this.fpSpread1_Sheet1.Rows.Count = 0;
                this.refresh();
                #region
                //				//根据对帐单 号码打印对账单
                //				this.PrintByBillCode(BillNo);
                //
                //				this.QueryByTime();
                //				//将选择记录清零
                //				this.SetTxtNum(0);
                #endregion
            }
            catch (Exception ex)
            {
                 FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("数据保存不成功" + ex.Message);
            }
        }

        private void refresh()
        {
            ArrayList al = new ArrayList();
            if (IsModify)
            {
                if (this.MyPatientType == PatientType.C)
                {
                    al = pubMgr.GetAllOper(this.GetStaticMonth().ToString(), "1");

                    this.InitTreeView(tvList, al, "门诊日结");
                }
                else
                {
                    al = pubMgr.GetAllOper(this.GetStaticMonth().ToString(), "2");
                    this.InitTreeView(tvList, al, "住院结算");
                }
            }
            else
            {
                if (this.MyPatientType == PatientType.C)
                {
                    al = pubMgr.GetAllOper(dtBegin.Value.ToString(), dtEnd.Value.ToString(), "1", IsModify);

                    this.InitTreeView(tvList, al, "门诊日结");
                }
                else
                {
                    al = pubMgr.GetAllOper(dtBegin.Value.ToString(), dtEnd.Value.ToString(), "2", IsModify);
                    this.InitTreeView(tvList, al, "住院结算");
                }
            }
        }

        private void Add()
        {
            this.fpSpread1_Sheet1.AddRows(this.fpSpread1_Sheet1.RowCount, 1);
            SOC.Local.PubReport.Models.PubReport obj = new SOC.Local.PubReport.Models.PubReport();
            //obj.OperDate = this.dtBegin.Value;
            if (this.patientType == PatientType.C)
            {
                obj.Fee_Type = "1";
            }
            else
            {
                obj.Fee_Type = "2";
            }
            this.WriteToFp(this.fpSpread1_Sheet1.RowCount-1,obj);
        }

        private void Remove()
        {
            DialogResult r = MessageBox.Show("确定删除该记录", "提示", MessageBoxButtons.YesNo);
            if (r == DialogResult.No)
            {
                return;
            }
            SOC.Local.PubReport.Models.PubReport obj = new SOC.Local.PubReport.Models.PubReport();
            string err = "";
            GetPubObjFromFp(this.fpSpread1_Sheet1.ActiveRowIndex, ref obj, ref err);
            if (obj.Seq != 0)
            {
                if (this.pubMgr.DeletePubReport(obj.ID, obj.Seq) == -1)
                {
                    MessageBox.Show(this.pubMgr.Err + " 删除失败");
                    return;
                }
            }
            this.fpSpread1_Sheet1.RemoveRows(this.fpSpread1_Sheet1.ActiveRowIndex, 1);
           
        }


        /// <summary>
        /// 拆分
        /// </summary>
        private void UnPackage()
        {
            this.fpSpread1_Sheet1.AddRows(this.fpSpread1_Sheet1.ActiveRowIndex + 1, 1);
            this.fpSpread1_Sheet1.ActiveRow.BackColor = Color.White;
            this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.ActiveRowIndex + 1].BackColor = Color.White;
            SOC.Local.PubReport.Models.PubReport lastObj = this.fpSpread1_Sheet1.ActiveRow.Tag as SOC.Local.PubReport.Models.PubReport;
            SOC.Local.PubReport.Models.PubReport newObj = lastObj.Clone();
            newObj.Pub_Cost = 0;
            newObj.Tot_Cost = 0;
            newObj.YaoPin = 0;
            newObj.ChengYao = 0;
            newObj.CaoYao = 0;
            newObj.JianCha = 0;
            newObj.ZhiLiao = 0;
            newObj.ShouShu = 0;
            newObj.CT = 0;
            newObj.MR = 0;
            newObj.ZhenJin = 0;
            newObj.ShuYang = 0;
            newObj.TeZhi = 0;
            newObj.HuaYan = 0;
            newObj.Bed_Fee = 0;
            #region [2010-01-28]zhaozf 修改公医报表添加
            newObj.OverLimitDrugFee = 0;
            newObj.CancerDrugFee = 0;
            newObj.BedFee_JianHu = 0;
            newObj.BedFee_CengLiu = 0;
            newObj.CompanyPay = 0;
            newObj.SelfPay = 0;
            newObj.TotalFee = 0;
            #endregion
            newObj.Seq++;
            WriteToFp(this.fpSpread1_Sheet1.ActiveRowIndex + 1, newObj);
            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 0].Tag = this.fpSpread1_Sheet1.ActiveRowIndex + 1;  //拆分后的兄弟项
            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex + 1, 0].Tag = this.fpSpread1_Sheet1.ActiveRowIndex;  //拆分后的兄弟项

        }

        /// <summary>
        /// 组合
        /// </summary>
        private void Combo()
        {
            SOC.Local.PubReport.Models.PubReport obj = new SOC.Local.PubReport.Models.PubReport();
           

            List<int> list = new List<int>();

            for (int j = 0; j < this.fpSpread1_Sheet1.RowCount; j++)
            {
                if (this.fpSpread1_Sheet1.IsSelected(j, 0))
                {
                    list.Add(j);
                }
            }

            list.Sort();

           for (int i = list.Count -1; i >= 0; i-- )
           {
               SOC.Local.PubReport.Models.PubReport newObj = this.fpSpread1_Sheet1.Rows[list[i]].Tag as SOC.Local.PubReport.Models.PubReport;
               if (i == list.Count - 1)
               {
                   obj = newObj;
               }
               else
               {
                   obj.Pub_Cost = obj.Pub_Cost + newObj.Pub_Cost;
                   obj.Tot_Cost = obj.Tot_Cost + newObj.Tot_Cost;
                   obj.YaoPin = obj.YaoPin + newObj.YaoPin;
                   obj.ChengYao = obj.ChengYao + newObj.ChengYao;
                   obj.CaoYao = obj.CaoYao + newObj.CaoYao;
                   obj.JianCha = obj.JianCha + newObj.JianCha;
                   obj.ZhiLiao = obj.ZhiLiao + newObj.ZhiLiao;
                   obj.ShouShu = obj.ShouShu + newObj.ShouShu;
                   obj.CT = obj.CT + newObj.CT;
                   obj.MR = obj.MR + newObj.MR;
                   obj.ZhenJin = obj.ZhenJin + newObj.ZhenJin;
                   obj.ShuYang = obj.ShuYang + newObj.ShuYang;
                   //obj.JieSheng = obj.JieSheng + newObj.JieSheng;
                   obj.HuaYan = obj.HuaYan + newObj.HuaYan;
                   obj.Bed_Fee = obj.Bed_Fee + newObj.Bed_Fee;
                   obj.JianHu = obj.JianHu + newObj.JianHu;
                   obj.TeJian = obj.TeJian = newObj.TeJian;
                   #region [2010-01-28]zhaozf 修改公医报表添加
                   obj.OverLimitDrugFee = obj.OverLimitDrugFee + newObj.OverLimitDrugFee;
                   obj.CancerDrugFee = obj.CancerDrugFee + newObj.CancerDrugFee;
                   obj.BedFee_JianHu = obj.BedFee_JianHu + newObj.BedFee_JianHu;
                   obj.BedFee_CengLiu = obj.BedFee_CengLiu + newObj.BedFee_CengLiu;
                   obj.CompanyPay = obj.CompanyPay + newObj.CompanyPay;
                   obj.SelfPay = obj.SelfPay + newObj.SelfPay;
                   obj.TotalFee = obj.TotalFee + newObj.TotalFee;
                   #endregion

                   
               }
               if (i != 0)
               {
                   SOC.Local.PubReport.Models.PubReport deleteObj = ((SOC.Local.PubReport.Models.PubReport)this.fpSpread1_Sheet1.Rows[list[i]].Tag).Clone();
                   this.fpSpread1_Sheet1.RemoveRows(list[i], 1);
                   alNeedDelete.Add(deleteObj);
               }
               else
               {
                   WriteToFp(list[i], obj);
               }
           }
           
        }


        /// <summary>
        /// 全选
        /// </summary>
        private void DOSelectAll(bool selected)
        { 
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
			{
                this.fpSpread1_Sheet1.Cells[i, 0].Value = selected;
			}
            
        }

        private void WriteToFp(ArrayList alPubReport)
        {
            try
            {
                if (alPubReport == null)
                    return;
                int i = this.fpSpread1_Sheet1.Rows.Count;
                string lastOper = "";
                foreach (SOC.Local.PubReport.Models.PubReport obj in alPubReport)
                {
                    this.fpSpread1_Sheet1.Rows.Add(i,1);
                    WriteToFp(i, obj);
                    if (lastOper != obj.OperCode)
                    {
                        lastOper = obj.OperCode;
                        ChangeColor();
                    }
                    this.fpSpread1_Sheet1.Rows[i].BackColor = RowColor;
                    i++;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void WriteToFp(int i,SOC.Local.PubReport.Models.PubReport obj)
        {
            try
            {

                if (obj.IsValid == "1")
                {
                    this.fpSpread1_Sheet1.Cells[i, (int)Col.选择].Value = true;
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[i, (int)Col.选择].Value = false;
                }
                this.fpSpread1_Sheet1.Cells[i, (int)Col.ID].Text = obj.ID;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.流水号].Text = obj.User01;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.发票号].Text = obj.ID;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.字头].Text = obj.Pact.Memo;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.医疗证号].Text = obj.MCardNo;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.姓名].Text = obj.Name;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.结算方式].Text = obj.Pact.Name;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.记帐金额].Value = obj.Pub_Cost;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.总金额].Value = obj.Tot_Cost;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.工号].Value = obj.OperCode;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.药品].Value = obj.YaoPin;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.成药].Value = obj.ChengYao;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.草药].Value = obj.CaoYao;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.检查].Value = obj.JianCha;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.特殊检查].Value = obj.TeJian;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.治疗].Value = obj.ZhiLiao;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.手术].Value = obj.ShouShu;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.CT].Value = obj.CT;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.MR].Value = obj.MR;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.诊金].Value = obj.ZhenJin;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.输氧].Value = obj.ShuYang;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.输血].Value = obj.ShuXue;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.化验].Value = obj.HuaYan;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.床位].Value = obj.Bed_Fee;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.监护].Value = obj.JianHu;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.特殊药品].Value = obj.SpDrugFeeTot;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.特殊治疗].Value = obj.TeZhi;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.特殊治疗比例].Value = obj.TeZhiRate;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.特药比例].Value = obj.TeYaoRate;

                this.fpSpread1_Sheet1.Cells[i, (int)Col.入院日期].Value = obj.Begin;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.出院日期].Value = obj.End;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.结算日期].Value = obj.OperDate;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.单位名称].Value = obj.WorkName;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.单位代号].Value = obj.WorkCode;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.住院号].Value = obj.PatientNO;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.天数].Value = obj.Amount;

                #region [2010-01-27]zhaozf 修改公医报表添加
                this.fpSpread1_Sheet1.Cells[i, (int)Col.药费超标金额].Value = obj.OverLimitDrugFee;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.审批肿瘤药费].Value = obj.CancerDrugFee;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.监护床位费].Value = obj.BedFee_JianHu;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.层流床位费].Value = obj.BedFee_CengLiu;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.单位支付金额].Value = obj.CompanyPay;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.自费金额].Value = obj.SelfPay;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.医疗费总金额].Value = obj.TotalFee;
                #endregion


                this.fpSpread1_Sheet1.Rows[i].Tag = obj;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void SetItemRecord(Hashtable TmpItemRecord, ArrayList ds)
        {
            try
            {
                if (ds == null)
                {
                    return;
                }
                if (ds.Count == 0)
                    return;
                TmpItemRecord.Add(TmpItemRecord.Count, ds.Count);//把变换信息记录进hashtable里面
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 查找选中记录数
        /// </summary>
        private int SelectInvioceNum()
        {
            int num = 0;
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                if (this.fpSpread1_Sheet1.Cells[i, 0].Text.ToUpper() == "TRUE")
                {
                    num++;
                }
            }
            return num;
        }

        int iColor = 0;
        System.Drawing.Color RowColor = System.Drawing.Color.YellowGreen;
        private void ChangeColor()
        {
            iColor = (iColor + 1) % 5;
            RowColor = SelectColor(iColor);
        }

        /// <summary>
        /// 返回系统颜色
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private System.Drawing.Color SelectColor(int number)
        {
            System.Drawing.Color BlackColor = new Color();
            switch (number)
            {
                case 0:
                    BlackColor = System.Drawing.Color.YellowGreen;
                    break;
                case 1:
                    BlackColor = System.Drawing.Color.Bisque;
                    break;
                case 2:
                    BlackColor = System.Drawing.Color.PowderBlue;
                    break;
                case 3:
                    BlackColor = System.Drawing.Color.Pink;
                    break;
                case 4:
                    BlackColor = System.Drawing.Color.LightSteelBlue;
                    break;

            }
            return BlackColor;
        }

        private void setStatuebar()
        {
            string select = this.SelectInvioceNum().ToString();
            this.statusBarPanel1.Text = "当前选择记录数:" + select;
        }

        #endregion

        #region 界面事件

        private void InitDateTime()
        {

            try
            {
                FS.FrameWork.Models.NeuObject staticTime = this.pubMgr.GetLastStaticTime();
                this.txtStaticMonth.Text = staticTime.ID + staticTime.Memo;
                DateTime begin = FS.FrameWork.Function.NConvert.ToDateTime(staticTime.User01);
                DateTime end = FS.FrameWork.Function.NConvert.ToDateTime(staticTime.User02);
                this.dtBegin.Value = begin;
                this.dtEnd.Value = end;
            }
            catch
            {
            }

        }

        private void SetFormat()
        {
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 40F;
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 50F;
            this.fpSpread1_Sheet1.Columns.Get(2).Width = 80F;
            this.fpSpread1_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet1.Columns.Get(3).Width = 85F;
            this.fpSpread1_Sheet1.Columns.Get(4).Width = 40F;
            this.fpSpread1_Sheet1.Columns.Get(5).Width = 60F;
            this.fpSpread1_Sheet1.Columns.Get(6).Width = 80F;
            this.fpSpread1_Sheet1.Columns[(int)Col.ID].Visible = false;
            this.fpSpread1_Sheet1.Columns[(int)Col.流水号].Visible = false;
            this.fpSpread1_Sheet1.Columns[(int)Col.结算方式].Width = 100F;
            int BeginRows = 0;
            int EndRows = -1;
            //for (int i = 0; i < ItemRecord.Count; i++)
            //{
            //    BeginRows = EndRows + 1;
            //    EndRows = BeginRows + FS.FrameWork.Function.NConvert.ToInt32(ItemRecord[i].ToString()) - 1;
            //    this.fpSpread1_Sheet1.Rows.Get(BeginRows, EndRows).BackColor = SelectColor(i % 5);
            //}

        }

        private void InitFp()
        {
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkCT = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCT = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCT = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.DateTimeCellType datetimeCT = new FarPoint.Win.Spread.CellType.DateTimeCellType();

            this.fpSpread1_Sheet1.Columns[(int)Col.选择].CellType = checkCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.ID].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.流水号].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.发票号].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.字头].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.医疗证号].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.姓名].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.结算方式].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.记帐金额].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.总金额].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.天数].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.工号].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.药品].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.成药].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.草药].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.检查].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.特殊检查].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.治疗].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.手术].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.CT].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.MR].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.诊金].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.输氧].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.输血].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.床位].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.化验].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.监护].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.出院日期].CellType = datetimeCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.入院日期].CellType = datetimeCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.结算日期].CellType = datetimeCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.单位名称].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.单位代号].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.特殊药品].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.特药比例].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.特殊治疗].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.特殊治疗比例].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.住院号].CellType = textCT;
            #region [2010-01-27]zhaozf 修改公医报表添加
            this.fpSpread1_Sheet1.Columns[(int)Col.药费超标金额].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.审批肿瘤药费].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.监护床位费].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.层流床位费].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.单位支付金额].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.自费金额].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.医疗费总金额].CellType = textCT;
            #endregion


            this.fpSpread1_Sheet1.Columns[(int)Col.选择].Label = Col.选择.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.ID].Label = Col.ID.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.流水号].Label = Col.流水号.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.发票号].Label = Col.发票号.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.字头].Label = Col.字头.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.医疗证号].Label = Col.医疗证号.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.姓名].Label = Col.姓名.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.结算方式].Label = Col.结算方式.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.记帐金额].Label = Col.记帐金额.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.总金额].Label = Col.总金额.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.工号].Label = Col.工号.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.天数].Label = Col.天数.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.药品].Label = Col.药品.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.成药].Label = Col.成药.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.草药].Label = Col.草药.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.检查].Label = Col.检查.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.特殊检查].Label = Col.特殊检查.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.治疗].Label = Col.治疗.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.手术].Label = Col.手术.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.CT].Label = Col.CT.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.MR].Label = Col.MR.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.诊金].Label = Col.诊金.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.输氧].Label = Col.输氧.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.输血].Label = Col.输血.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.化验].Label = Col.化验.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.床位].Label = Col.床位.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.监护].Label = Col.监护.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.入院日期].Label = Col.入院日期.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.出院日期].Label = Col.出院日期.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.结算日期].Label = Col.结算日期.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.单位名称].Label = Col.单位名称.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.单位代号].Label = Col.单位代号.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.特殊药品].Label = Col.特殊药品.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.特药比例].Label = Col.特药比例.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.特殊治疗].Label = Col.特殊治疗.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.特殊治疗比例].Label = Col.特殊治疗比例.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.住院号].Label = Col.住院号.ToString();
            #region [2010-01-27]zhaozf 修改公医报表添加
            this.fpSpread1_Sheet1.Columns[(int)Col.药费超标金额].Label = Col.药费超标金额.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.审批肿瘤药费].Label = Col.审批肿瘤药费.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.监护床位费].Label = Col.监护床位费.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.层流床位费].Label = Col.层流床位费.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.单位支付金额].Label = Col.单位支付金额.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.自费金额].Label = Col.自费金额.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.医疗费总金额].Label = Col.医疗费总金额.ToString();
            #endregion

            this.fpSpread1_Sheet1.Columns.Count = (int)Col.医疗费总金额 + 1;

            this.fpSpread1_Sheet1.Columns[(int)Col.选择].Locked = false;
            this.fpSpread1_Sheet1.Columns[(int)Col.ID].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.流水号].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.发票号].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.字头].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.医疗证号].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.姓名].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.结算方式].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.记帐金额].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.总金额].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.工号].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.天数].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.药品].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.成药].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.草药].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.检查].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.特殊检查].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.治疗].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.手术].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.CT].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.MR].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.诊金].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.输氧].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.输血].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.床位].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.监护].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.化验].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.入院日期].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.出院日期].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.结算日期].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.单位代号].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.单位名称].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.特殊药品].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.特殊治疗].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.特殊治疗比例].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.特药比例].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.住院号].Locked = true;
            #region [2010-01-27]zhaozf 修改公医报表添加
            this.fpSpread1_Sheet1.Columns[(int)Col.药费超标金额].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.审批肿瘤药费].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.监护床位费].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.层流床位费].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.单位支付金额].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.自费金额].Locked = true;
            this.fpSpread1_Sheet1.Columns[(int)Col.医疗费总金额].Locked = true;
            #endregion

            this.fpSpread1_Sheet1.Rows.Count = 0;
            this.SetFormat();
            #region
            /*
            dsInvoice.Columns.AddRange(new DataColumn[] {  new DataColumn("选择",Bool),														  
															new DataColumn("ID",str),
															new DataColumn("流水号",str),
															new DataColumn("发票号",str),
															new DataColumn("字头",str),
															new DataColumn("医疗证号",str),
															new DataColumn("姓名",str),	
															new DataColumn("结算方式",str),	
															//new DataColumn("小单号",Int),														  														  
															new DataColumn("记帐金额",dec),
															new DataColumn("总金额",dec),
															new DataColumn("工号",str),
															new DataColumn("药品",dec),
															new DataColumn("成药",dec),
															new DataColumn("草药",dec),
															new DataColumn("检查",dec),
															new DataColumn("治疗",dec),
															new DataColumn("手术",dec),
															new DataColumn("CT",dec),
															new DataColumn("MR",dec),
															new DataColumn("诊金",dec),
															new DataColumn("高氧",dec),
															new DataColumn("接生",dec),
																														
														});
            */

            //			dsInvoice.Columns.AddRange(new DataColumn[] {  new DataColumn("选择",Bool),														  
            //															new DataColumn("ID",str),
            //															new DataColumn("SJ",str),
            //															new DataColumn("BH",str),
            //															new DataColumn("JZDH",Int),														  														  
            //															new DataColumn("JZ",dec),
            //															new DataColumn("ZJ",dec),
            //															new DataColumn("GH",str),
            //															new DataColumn("F1",dec),
            //															new DataColumn("F2",dec),
            //															new DataColumn("F3",dec),
            //															new DataColumn("F4",dec),
            //															new DataColumn("F5",dec),
            //															new DataColumn("F6",dec),
            //															new DataColumn("F7",dec),
            //															new DataColumn("F8",dec),
            //															new DataColumn("F9",dec),
            //															new DataColumn("F10",dec),
            //															new DataColumn("F11",dec)
            //															
            //														});	
            #endregion
        }

        /// <summary>
        /// 公费复核，树的初试化与查询
        /// </summary>
        /// <param name="TView">树</param>
        /// <param name="NodePactHead">收费员信息</param>
        /// <param name="RootNameText">父节点名称</param>
        private void InitTreeView(System.Windows.Forms.TreeView TView, ArrayList NodePactHead, string RootNameText)
        {
            //ArrayList NodePactHead 为从数据库查询出字头，填充数组,作为参数传递进来	
            TView.Nodes.Clear();
            TreeNode RootNode = new TreeNode(RootNameText);
            RootNode.ImageIndex = 0;
            RootNode.Tag = "ROOT";
            TView.Nodes.Add(RootNode);

            //for each 生成第一级节点
            #region 一级说明
            //pinfo.ID = this.Reader[0].ToString();//章号
            //pinfo.User01= this.Reader[1].ToString();//工号
            //pinfo.Name = this.Reader[2].ToString();//姓名
            //pinfo.User02 = this.Reader[3].ToString();//条数	
            #endregion
            foreach (FS.FrameWork.Models.NeuObject pinfo in NodePactHead)
            {
                TreeNode tempNode;
                tempNode = new TreeNode();
                tempNode.Text = pinfo.ID + " " + pinfo.Name + "(共" + pinfo.User02 + "张)";
                tempNode.Tag = pinfo.ID;//章号
                tempNode.ImageIndex = 5;
                tempNode.SelectedImageIndex = 4;
                RootNode.Nodes.Add(tempNode);//挂到TreeView RootNode节点下


                //生成第二级结点:按照章号分类
                #region 二级说明
                //pinfo.ID = this.Reader[0].ToString();//章号
                //pinfo.Name = this.Reader[1].ToString();//姓名
                //pinfo.User01= this.Reader[2].ToString();//字头
                //pinfo.User02 = this.Reader[3].ToString();//条数		
                #endregion
                ArrayList alOper = new ArrayList();
                if (isModify)
                {
                    if (this.patientType == PatientType.C)
                    {
                        alOper = this.pubMgr.GetInvoiceSumByUserCode(this.GetStaticMonth().ToString(), pinfo.ID, "1", IsModify);
                    }
                    else
                    {
                        alOper = this.pubMgr.GetInvoiceSumByUserCode(this.GetStaticMonth().ToString(), pinfo.ID, "2", IsModify);
                    }
                }
                else
                {
                    if (this.patientType == PatientType.C)
                    {
                        alOper = this.pubMgr.GetInvoiceSumByUserCode(this.Begin.ToString(), this.End.ToString(), pinfo.ID, "1", IsModify);
                    }
                    else
                    {
                        alOper = this.pubMgr.GetInvoiceSumByUserCode(this.Begin.ToString(), this.End.ToString(), pinfo.ID, "2", IsModify);
                    }
                }
                foreach (FS.FrameWork.Models.NeuObject Info_PactHead in alOper)
                {
                    TreeNode SubtempNode;
                    SubtempNode = new TreeNode();
                    SubtempNode.Text = Info_PactHead.ID+" "+Info_PactHead.Name + "(共" + Info_PactHead.User02 + "张)";//字头
                    SubtempNode.Tag = Info_PactHead.ID;//字头
                    SubtempNode.ImageIndex = 1;
                    SubtempNode.SelectedImageIndex = 2;
                    tempNode.Nodes.Add(SubtempNode);//挂到TreeView tempNode节点下
                }


            }
            RootNode.Expand();
        }

        #region 响应工具栏

        protected FS.FrameWork.WinForms.Forms.ToolBarService toolbar = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return base.OnQuery(sender, neuObject);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return base.OnSave(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.Print();
            return base.OnPrint(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            this.Export();
            return base.Export(sender, neuObject);
        }

        protected override void OnRefresh()
        {
            this.refresh();
            base.OnRefresh();
        }
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolbar.AddToolButton("刷新", "刷新", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S刷新, true, false, null);
            toolbar.AddToolButton("合并", "合并", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z暂存, true, false, null);
            toolbar.AddToolButton("拆分", "拆分", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T添加, true, false, null);
            toolbar.AddToolButton("修改", "修改", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            toolbar.AddToolButton("采集", "采集", (int)FS.FrameWork.WinForms.Classes.EnumImageList.F复制, true, false, null);
            toolbar.AddToolButton("增加", "增加", (int)FS.FrameWork.WinForms.Classes.EnumImageList.F复制, true, false, null);
            toolbar.AddToolButton("删除", "删除", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除 , true, false, null);
            toolbar.AddToolButton("全选", "全选", (int)FS.FrameWork.WinForms.Classes.EnumImageList.F复制, true, false, null);
            toolbar.AddToolButton("取消全选", "取消全选", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            
            return this.toolbar;
        }


        #endregion

        void ucPubReportCheck_Load(object sender, EventArgs e)
        {
            this.InitDateTime();
            ArrayList al = new ArrayList();
            this.refresh();//初始化树
            this.InitFp();
        }


        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "刷新")
            {
                this.refresh();
            }
            else if (e.ClickedItem.Text == "拆分")
            {
                this.UnPackage();
            }
            else if (e.ClickedItem.Text == "合并")
            {
                this.Combo();
            }
            else if (e.ClickedItem.Text == "修改")
            {
                this.Modify();
            }
            else if (e.ClickedItem.Text == "采集")
            {
                this.GethorPubReportFromInvoice();
            }
            else if (e.ClickedItem.Text == "增加")
            {
                this.Add();
            }
            else if (e.ClickedItem.Text == "删除")
            {
                this.Remove();
            }
            else if (e.ClickedItem.Text == "全选")
            {
                this.DOSelectAll(true);
            }
            else if (e.ClickedItem.Text == "取消全选")
            {
                this.DOSelectAll(false);
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        frmPubReportObj ucPubReportObj1 = new frmPubReportObj();
        private void Modify()
        {
            //frmPubReportObj ucPubReportObj1 = new frmPubReportObj();
            SOC.Local.PubReport.Models.PubReport obj = this.fpSpread1_Sheet1.ActiveRow.Tag as SOC.Local.PubReport.Models.PubReport;
            if (obj == null)
            {
                return;
            }
            ucPubReportObj1.StaticMonth = txtStaticMonth.GetStaticMonth();
            ucPubReportObj1.WriteToPanel(obj);
            if (obj.ID != "")
            {
                ucPubReportObj1.Check(obj);
            }
            ucPubReportObj1.ShowDialog();
            if (ucPubReportObj1.dialogResult == DialogResult.Yes)
            {
                MoveDownData();
                obj = ucPubReportObj1.ReadFromPanel();
                WriteToFp(this.fpSpread1_Sheet1.ActiveRowIndex, obj);
            }
        }

        private SOC.Local.PubReport.Models.PubReport TotalNoChange(SOC.Local.PubReport.Models.PubReport broObj,SOC.Local.PubReport.Models.PubReport oldObj, SOC.Local.PubReport.Models.PubReport newObj)
        {
            SOC.Local.PubReport.Models.PubReport obj = new SOC.Local.PubReport.Models.PubReport();
            obj = broObj.Clone();
            obj.Pub_Cost = broObj.Pub_Cost + oldObj.Pub_Cost - newObj.Pub_Cost;
            obj.Tot_Cost = broObj.Tot_Cost + oldObj.Tot_Cost - newObj.Tot_Cost;
            obj.YaoPin = broObj.YaoPin + oldObj.YaoPin - newObj.YaoPin;
            obj.ChengYao = broObj.ChengYao + oldObj.ChengYao - newObj.ChengYao;
            obj.CaoYao = broObj.CaoYao + oldObj.CaoYao - newObj.CaoYao;
            obj.JianCha = broObj.JianCha + oldObj.JianCha - newObj.JianCha;
            obj.ZhiLiao = broObj.ZhiLiao + oldObj.ZhiLiao - newObj.ZhiLiao;
            obj.ShouShu = broObj.ShouShu + oldObj.ShouShu - newObj.ShouShu;
            obj.CT = broObj.CT + oldObj.CT - newObj.CT;
            obj.MR = broObj.MR + oldObj.MR - newObj.MR;
            obj.ZhenJin = broObj.ZhenJin + oldObj.ZhenJin - newObj.ZhenJin;
            obj.ShuYang = broObj.ShuYang + oldObj.ShuYang - newObj.ShuYang;
            obj.TeZhi = broObj.TeZhi + oldObj.TeZhi - newObj.TeZhi;
            obj.HuaYan = broObj.HuaYan + oldObj.HuaYan - newObj.HuaYan;
            obj.Bed_Fee = broObj.Bed_Fee + oldObj.Bed_Fee - newObj.Bed_Fee;
            obj.JianHu = broObj.JianHu + oldObj.JianHu - newObj.JianHu;
            obj.ShuXue = broObj.ShuXue + oldObj.ShuXue - newObj.ShuXue;
            
            return obj;
        }

        private void tvList_AfterCheck(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            TreeNode tempNode = new TreeNode();
            //如果是选中
            if (e.Node.Checked == true)
            {
                for (int i = 0; i < e.Node.Nodes.Count; i++)
                    e.Node.Nodes[i].Checked = true;
            }
            //没有选中
            else
            {
                for (int j = 0; j < e.Node.Nodes.Count; j++)
                    e.Node.Nodes[j].Checked = false;
            }
        }

        void MoveDownData()
        {
            if (this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 0].Tag != null )
            {
                DialogResult r = MessageBox.Show("是否自动调整同发票的记账单金额使总金额保持不变？", "提示", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {
                    int brotherRow = (int)this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 0].Tag;
                    SOC.Local.PubReport.Models.PubReport broObj = this.fpSpread1_Sheet1.Rows[brotherRow].Tag as SOC.Local.PubReport.Models.PubReport;
                    SOC.Local.PubReport.Models.PubReport objOld = this.fpSpread1_Sheet1.ActiveRow.Tag as SOC.Local.PubReport.Models.PubReport;  //修改前
                    SOC.Local.PubReport.Models.PubReport objNew = ucPubReportObj1.ReadFromPanel(); //修改后
                    SOC.Local.PubReport.Models.PubReport broObjNew = TotalNoChange(broObj, objOld, objNew);  //兄弟项
                    WriteToFp(this.fpSpread1_Sheet1.ActiveRowIndex, objNew);
                    WriteToFp(brotherRow, broObjNew);
                    return;
                }
            }
        }

        void ucPubReportObj1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageDown)
            {
                int activeRow = this.fpSpread1_Sheet1.ActiveRowIndex;
                MoveDownData();
                SOC.Local.PubReport.Models.PubReport obj = ucPubReportObj1.ReadFromPanel();
                WriteToFp(this.fpSpread1_Sheet1.ActiveRowIndex, obj);
                if (activeRow < this.fpSpread1_Sheet1.Rows.Count - 1)
                {
                    activeRow++;
                }
                this.fpSpread1_Sheet1.SetActiveCell(activeRow, 0);
                obj = this.fpSpread1_Sheet1.ActiveRow.Tag as SOC.Local.PubReport.Models.PubReport;
                if (obj == null)
                {
                    return;
                }
                ucPubReportObj1.WriteToPanel(obj);
                ucPubReportObj1.Check(obj);
            }
            if (e.KeyCode == Keys.PageUp)
            {
                MoveDownData();
                int activeRow = this.fpSpread1_Sheet1.ActiveRowIndex;
                SOC.Local.PubReport.Models.PubReport obj = ucPubReportObj1.ReadFromPanel();
                WriteToFp(this.fpSpread1_Sheet1.ActiveRowIndex, obj);
                if (activeRow > 0)
                {
                    activeRow--;
                }
                this.fpSpread1_Sheet1.SetActiveCell(activeRow, 0);
                obj = this.fpSpread1_Sheet1.ActiveRow.Tag as SOC.Local.PubReport.Models.PubReport;
                if (obj == null)
                {
                    return;
                }
                ucPubReportObj1.WriteToPanel(obj);
                ucPubReportObj1.Check(obj);
            }
            if (e.KeyCode == Keys.Up)
            {
                btnPre_Click(sender, e);
            }
            if (e.KeyCode == Keys.Down)
            {
                btnNext_Click(sender, e);
            }

        }

        #endregion

        enum Col
        {
            选择,
            ID,
            流水号,
            发票号,
            字头,
            医疗证号,
            姓名,
            结算方式,
            记帐金额,
            总金额,
            工号,
            天数,
            药品,
            成药,
            草药,
            检查,
            特殊检查,
            治疗,
            手术,
            CT,
            MR,
            诊金,
            输氧,
            输血,
            化验,
            床位,
            监护,
            特殊药品,
            特药比例,
            特殊治疗,
            特殊治疗比例,
            入院日期,
            出院日期,
            结算日期,
            单位名称,
            单位代号,
            住院号,

            #region [2010-01-27]zhaozf 修改公医报表添加
            药费超标金额,
            审批肿瘤药费,
            监护床位费,
            层流床位费,
            单位支付金额,
            自费金额,
            医疗费总金额
            #endregion

        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            Modify();
        }

        private void txtInvNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.fpSpread1_Sheet1.RowCount = 0;
                string McardNo = this.txtInvNo.Text;
                 ArrayList al  = new ArrayList();
                 this.alNeedDelete = new ArrayList();
                 string feeType = "";
                 if (this.patientType == PatientType.C)
                 {
                     feeType = "1";
                 }
                 else
                 {
                     feeType = "2";
                 }
                if (isModify)
                {
                   al = this.pubMgr.QueryPubFeeInfoByMcardNo(McardNo, GetStaticMonth(),feeType);
                }
                else
                {
                    al = this.pubMgr.QueryPubFeeInfoByMcardNo(McardNo, dtBegin.Value, dtEnd.Value,feeType);
                }
                this.WriteToFp(al); //加进汇总dataset	
                this.setStatuebar();
                this.SetFormat();
                

            }
        }

    }
}