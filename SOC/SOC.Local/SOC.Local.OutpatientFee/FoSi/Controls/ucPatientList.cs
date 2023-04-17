using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.BizProcess.Interface.Fee;
using SOC.Fee.DayBalance.AlterPayMode;

namespace FS.SOC.Local.OutpatientFee.FoSi.Controls
{
    public partial class ucPatientList : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientFeeList
    {
        

        #region 变量
        /// <summary>
        /// 收费发票列表
        /// </summary>
        DataTable dtInvoiceList = null;

        FS.HISFC.BizLogic.Fee.Outpatient outPatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        SOC.Local.OutpatientFee.FoSan.Interface.IOutPatientInvoiceShow outPatientInvoiceShow = null;
        #endregion


        #region 初始化
        public ucPatientList()
        {
            InitializeComponent();

            this.ReflashList();
        }

        /// <summary>
        /// 初始化数据源
        /// </summary>
        private void InitTable()
        {
            if (dtInvoiceList == null)
            {
                dtInvoiceList = new DataTable();
                DataColumn[] dcArr = new DataColumn[] { 
                    new DataColumn("patientName", typeof(string)), 
                    new DataColumn("totMoney", typeof(decimal)),
                    new DataColumn("operDate", typeof(string)),
                    new DataColumn("CardNO", typeof(object)),
                    new DataColumn("clinicNo", typeof(string)),
                    new DataColumn("type", typeof(string)),
                    new DataColumn("Tag", typeof(object))                
                };

                dtInvoiceList.Columns.AddRange(dcArr);
            }

            dtInvoiceList.DefaultView.Sort = "type, operDate ";

            this.neuSpread1_Sheet1.Columns[0].DataField = "patientName";
            this.neuSpread1_Sheet1.Columns[1].DataField = "totMoney";
            this.neuSpread1_Sheet1.Columns[2].DataField = "operDate";
            this.neuSpread1_Sheet1.Columns[3].DataField = "CardNO";

            this.neuSpread1_Sheet1.Columns[4].DataField = "clinicNo";
            this.neuSpread1_Sheet1.Columns[4].Visible = false;
            this.neuSpread1_Sheet1.Columns[5].DataField = "type";
            this.neuSpread1_Sheet1.Columns[5].Visible = false;
            this.neuSpread1_Sheet1.Columns[6].DataField = "Tag";
            this.neuSpread1_Sheet1.Columns[6].Visible = false;

            this.neuSpread1_Sheet1.AutoGenerateColumns = false;

            this.neuSpread1_Sheet1.DataSource = dtInvoiceList.DefaultView;
        }

        private void ucPatientList_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;

            this.ReflashList();
        }

        public void Clear()
        {
            InitTable();
            //dtInvoiceList.Clear();
            dtInvoiceList.AcceptChanges();
        }

        public void ReflashList()
        {
            Clear();

            ArrayList arlInvoiceList = outPatientManager.QueryBalanceListsByOper(outPatientManager.Operator.ID);

            AddItemsRef(arlInvoiceList);
            this.setColor();

            SetActiveRow();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="balance"></param>
        /// <param name="blnDeleteRegInfo">是否删除挂号记录</param>
        private void AddItem(FS.HISFC.Models.Fee.Outpatient.Balance balance, bool blnDeleteRegInfo)
        {
            if (balance == null)
            {
                return;
            }

            DataRow drTemp = null;

            if (blnDeleteRegInfo)
            {
                DataRow[] drTempArr = null;
                drTempArr = dtInvoiceList.Select("type = '2' and clinicNo = '" + balance.Patient.ID + "'");
                if (drTempArr != null && drTempArr.Length > 0)
                {
                    foreach (DataRow dr in drTempArr)
                    {
                        dr.Delete();
                    }
                }
            }

            decimal decTemp = 0;
            decimal.TryParse(balance.User01, out decTemp);
            if (balance.TransType == FS.HISFC.Models.Base.TransTypes.Negative)
            {
                decTemp = -1 * decTemp;
            }

            drTemp = dtInvoiceList.NewRow();
            drTemp["patientName"] = balance.Patient.Name;
            drTemp["totMoney"] = balance.FT.PayCost + balance.FT.OwnCost - decTemp;
            drTemp["operDate"] = balance.BalanceOper.OperTime.ToString("yyyy-MM-dd HH:mm:ss");
            drTemp["CardNO"] = balance.Patient.PID.CardNO;

            drTemp["clinicNo"] = balance.Invoice.ID;

            drTemp["type"] = "1"; // 发票信息
            drTemp["Tag"] = balance;
            dtInvoiceList.Rows.Add(drTemp);
        }

        private void AddItem(FS.HISFC.Models.Registration.Register register)
        {
            if (register == null)
            {
                return;
            }

            DataRow drTemp = null;
            DataRow[] drTempArr = null;
            drTempArr = dtInvoiceList.Select("type = '2' and clinicNo = '" + register.ID + "'");

            if (drTempArr != null && drTempArr.Length > 0)
            {
                if (drTempArr.Length == 1)
                {
                    drTemp = drTempArr[0];
                }
                else
                {
                    foreach (DataRow dr in drTempArr)
                    {
                        dr.Delete();
                    }
                }
            }

            if (drTemp == null)
            {
                drTemp = dtInvoiceList.NewRow();

                drTemp["patientName"] = register.Name;
                drTemp["totMoney"] = 0;
                drTemp["operDate"] = "";
                drTemp["CardNO"] = register.PID.CardNO;
                drTemp["clinicNo"] = register.ID;
                drTemp["type"] = "2"; // 未收费显示挂号信息
                drTemp["Tag"] = register;

                dtInvoiceList.Rows.Add(drTemp);
            }
            else
            {
                drTemp["patientName"] = register.Name;
                drTemp["totMoney"] = 0;
                drTemp["operDate"] = "";
                drTemp["CardNO"] = register.PID.CardNO;
                drTemp["clinicNo"] = register.ID;
                drTemp["type"] = "2"; // 未收费显示挂号信息
                drTemp["Tag"] = register;
            }
        }

        #endregion


        #region IOutpatientFeeList 成员


        public event FS.HISFC.BizProcess.Integrate.FeeInterface.DelegatePatientSelected evnPatientSelected;

        public void AddItems(ArrayList arlInvoiceList)
        {
            if (arlInvoiceList == null || arlInvoiceList.Count <= 0)
                return;

            if (dtInvoiceList == null)
            {
                InitTable();
            }

            FS.HISFC.Models.Fee.Outpatient.Balance balance = null;
            FS.HISFC.Models.Registration.Register register = null;

            for (int idx = 0; idx < arlInvoiceList.Count; idx++)
            {
                if (arlInvoiceList[idx] is FS.HISFC.Models.Fee.Outpatient.Balance)
                {
                    balance = arlInvoiceList[idx] as FS.HISFC.Models.Fee.Outpatient.Balance;

                    this.AddItem(balance, true);
                }
                else if (arlInvoiceList[idx] is FS.HISFC.Models.Registration.Register)
                {
                    register = arlInvoiceList[idx] as FS.HISFC.Models.Registration.Register;

                    this.AddItem(register);
                }
            }
            //进行退费操作后列表不自动增加在列表，所以有退费操作后进行一次刷新,否则只刷新颜色
            if (arlInvoiceList.Count == 1 && dtInvoiceList.Rows.Count != this.GetRowCount(outPatientManager.Operator.ID) && dtInvoiceList.Rows.Count!=1)
            {
                this.ReflashList();
            }
            else
            {
                this.setColor();
            }
            dtInvoiceList.AcceptChanges();

            SetActiveRow();
        }

        public void AddItemsRef(ArrayList arlInvoiceList)
        {
            if (arlInvoiceList == null || arlInvoiceList.Count <= 0)
                return;

            if (dtInvoiceList == null)
            {
                InitTable();
            }
            else
            {
                DataRow drTemp = null;
                int idx = 0;
                while (idx < dtInvoiceList.Rows.Count)
                {
                    drTemp = dtInvoiceList.Rows[idx];
                    if (drTemp["type"].ToString().Trim() == "1")
                    {
                        drTemp.Delete();
                        dtInvoiceList.AcceptChanges();
                    }
                    else
                    {
                        idx++;
                    }
                }
                //for (int idx = 0; idx < dtInvoiceList.Rows.Count; idx++)
                //{
                //    drTemp = dtInvoiceList.Rows[idx];
                //    if (drTemp["type"].ToString().Trim() == "1")
                //    {
                //        drTemp.Delete();
                //        idx--;
                //    }
                //}
            }
            

            FS.HISFC.Models.Fee.Outpatient.Balance balance = null;
            FS.HISFC.Models.Registration.Register register = null;

            for (int idx = 0; idx < arlInvoiceList.Count; idx++)
            {
                if (arlInvoiceList[idx] is FS.HISFC.Models.Fee.Outpatient.Balance)
                {
                    balance = arlInvoiceList[idx] as FS.HISFC.Models.Fee.Outpatient.Balance;

                    this.AddItem(balance, false);
                }
                else if (arlInvoiceList[idx] is FS.HISFC.Models.Registration.Register)
                {
                    register = arlInvoiceList[idx] as FS.HISFC.Models.Registration.Register;

                    this.AddItem(register);
                }
            }
            dtInvoiceList.AcceptChanges();
        }

        #endregion

        private void btnReFresh_Click(object sender, EventArgs e)
        {
            this.ReflashList();
            //this.setColor();
        }

        private void neuSpread1_Paint(object sender, PaintEventArgs e)
        {
            this.neuSpread1_Sheet1.Columns[0].Width = 80;
            this.neuSpread1_Sheet1.Columns[1].Width = 60;
            this.neuSpread1_Sheet1.Columns[2].Width = 120;
            this.neuSpread1_Sheet1.Columns[3].Width = 90;

            //this.neuSpread1.Paint -= new PaintEventHandler(neuSpread1_Paint);

            //this.setColor();

            //this.neuSpread1.Paint += new PaintEventHandler(neuSpread1_Paint);

        }

        private void SetActiveRow()
        {
            int iRowCount = this.neuSpread1_Sheet1.RowCount;
            if (iRowCount > 0)
            {
                this.neuSpread1_Sheet1.ActiveRowIndex = iRowCount - 1;
            }

            this.neuSpread1.SetViewportTopRow(0, this.neuSpread1_Sheet1.ActiveRowIndex);
        }

        private void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Row < 0 || e.Row > this.neuSpread1_Sheet1.RowCount)
            {
                return;
            }

            if (this.neuSpread1_Sheet1.Cells[e.Row, 6].Value == null)
            {
                return;
            }

            FS.HISFC.Models.Fee.Outpatient.Balance balance = this.neuSpread1_Sheet1.Cells[e.Row, 6].Value as FS.HISFC.Models.Fee.Outpatient.Balance;
            if (balance != null)
            {
                invoiceNo = balance.Invoice.ID;
            }
            

            FS.HISFC.Models.Registration.Register register = this.neuSpread1_Sheet1.Cells[e.Row, 6].Value as FS.HISFC.Models.Registration.Register;
            if (register == null)
            {
              
                return;
            }
           
            if (evnPatientSelected != null)
            {
                try
                {
                    evnPatientSelected(register);
                }
                catch(Exception objEx)
                {
                    MessageBox.Show(objEx.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

          
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Row < 0 || e.Row > this.neuSpread1_Sheet1.RowCount)
            {
                return;
            }

            if (this.neuSpread1_Sheet1.Cells[e.Row, 6].Value == null)
            {
                return;
            }

            FS.HISFC.Models.Fee.Outpatient.Balance balance = this.neuSpread1_Sheet1.Cells[e.Row, 6].Value as FS.HISFC.Models.Fee.Outpatient.Balance;
            if (balance == null)
            {
                return;
            }
            invoiceNo = balance.Invoice.ID;

            ArrayList alBalance = outPatientManager.QueryBalancesByInvoiceNO(balance.Invoice.ID);
            if (alBalance == null)
            {
                MessageBox.Show("查询发票信息失败！");
                return;
            }

            balance = null;
            foreach (FS.HISFC.Models.Fee.Outpatient.Balance invoice in alBalance)
            {
                if (invoice.TransType == FS.HISFC.Models.Base.TransTypes.Positive && invoice.CancelType == FS.HISFC.Models.Base.CancelTypes.Valid)
                {
                    balance = invoice;
                    break;
                }
            }

            if (balance == null)
            {
                MessageBox.Show("该发票已退费！");
                return;
            }

            this.outPatientInvoiceShow = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject<SOC.Local.OutpatientFee.FoSan.Interface.IOutPatientInvoiceShow>(this.GetType());

            if (outPatientInvoiceShow != null)
            {
                this.outPatientInvoiceShow.SetInvoiceInfo(balance);
                this.outPatientInvoiceShow.ShowDialog();
            }
            else
            {
                frmInvoiceInfo frm = new frmInvoiceInfo();
                frm.SetInvoiceInfo(balance);
                frm.ShowDialog();
            }

        }
        //根据收费方式显示不同的颜色
        private void setColor()
        {
            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                return ;
            }
            for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
            {
                FS.HISFC.Models.Fee.Outpatient.Balance balance = this.neuSpread1_Sheet1.Cells[i, 6].Value as FS.HISFC.Models.Fee.Outpatient.Balance;
                if (balance ==null )
                {
                    this.neuSpread1_Sheet1.Rows[i].ForeColor = Color.Black;
                    continue;
                }
                //如果是退费，显示红色
                if (((int)balance.TransType).ToString() == "2")
                {
                    this.neuSpread1_Sheet1.Rows[i].ForeColor = Color.Red;
                }
                else 
                { 
                    string bankName =this.GetBankByBalanceNo(balance.Invoice.ID);
                    if(bankName =="JH")
                    {
                        this.neuSpread1_Sheet1.Rows[i].ForeColor = Color.Green;
                    }
                    else if (bankName == "NH")
                    {
                       this.neuSpread1_Sheet1.Rows[i].ForeColor = Color.Purple;
                    }
                }
            }
            return;

        }
        /// <summary>
        ///  按发票号判断发票的支付方式
        /// </summary>
        /// <param name="balanceNo"> 发票号</param>
        /// <returns></returns>
        public string GetBankByBalanceNo(string BalanceNo)
        {
            string bankName = null;
            if (BalanceNo == null)
            {
                return null;
            }
            string sql = @"select
                           a.invoice_no ,
                           a.mode_code 
                           from fin_opb_paymode a
                           where a.balance_flag = '0'
                           and a.invoice_no ='{0}'";
            try
            {
                sql = string.Format(sql, BalanceNo);
            }
            catch (Exception ex)
            {
                //this.Err = ex.Message;
                return null;
            }
            DataSet dsResult = null;
            if (this.outPatientManager.ExecQuery(sql, ref dsResult) == -1)
            {
                //this.Err = "执行SQL语句失败！";
                return null;
            }
            DataTable dtResult = dsResult.Tables[0];
            foreach (DataRow dr in dtResult.Rows)
            {
                if (bankName == "NH" || bankName == "JH")
                {
                    return bankName;
                }
                bankName = dr["mode_code"].ToString().Trim();
            }
            return bankName;
        }
        /// <summary>
        ///  获取列表要显示的行数
        /// </summary>
        /// <param name="operid"> 操作员</param>
        /// <returns></returns>
        public int GetRowCount(string operid)
        {
            int rowCount = 0;
            string sql = @"  select
                             count(*)
                             from fin_opb_invoiceinfo m
                             where balance_flag = '0'  and oper_date > sysdate - 10  and oper_code = '{0}'";
            sql = string.Format(sql, operid);
            rowCount = FS.FrameWork.Function.NConvert.ToInt32(this.outPatientManager.ExecSqlReturnOne(sql));
            return rowCount;
        }


        #region IOutpatientFeeList 成员


        public void GetSelectRowUpByCount(int iCount, out ArrayList arlFeeInfo)
        {
            arlFeeInfo = null;
            if (iCount <= 0)
            {
                return;
            }
            if (neuSpread1_Sheet1.RowCount <= 0)
            {
                return;
            }

            ArrayList arlRegTemp = new ArrayList();
            int iActive = this.neuSpread1_Sheet1.ActiveRow.Index;
            int idxTemp = 0;
            for (int idx = 0; idx < iCount; idx++)
            {
                idxTemp = iActive - idx;
                if (idxTemp < 0 || idxTemp >= this.neuSpread1_Sheet1.RowCount)
                {
                    MessageBox.Show("张数录入错误！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                FS.HISFC.Models.Registration.Register register = this.neuSpread1_Sheet1.Cells[idxTemp, 6].Value as FS.HISFC.Models.Registration.Register;

                if (register == null)
                {
                    FS.HISFC.Models.Fee.Outpatient.Balance balance = this.neuSpread1_Sheet1.Cells[idxTemp, 6].Value as FS.HISFC.Models.Fee.Outpatient.Balance;
                    if (balance == null)
                    {
                        MessageBox.Show("获取第 " + (idxTemp + 1).ToString() + " 行数据出错！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        arlRegTemp.Add(balance);
                    }
                }
                else
                {
                    arlRegTemp.Add(register);
                }
            }

            arlFeeInfo = arlRegTemp;
            return;
        }

        #endregion

        #region 打印用清单

        private void miPrintFeedetial_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1_Sheet1.ActiveRow == null)
            {
                MessageBox.Show("请选择收费记录!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int iActiveRow = this.neuSpread1_Sheet1.ActiveRow.Index;
            if (iActiveRow < 0 && iActiveRow >= this.neuSpread1_Sheet1.RowCount)
            {
                MessageBox.Show("请选择收费记录!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.neuSpread1_Sheet1.Cells[iActiveRow, 6].Value == null)
            {
                MessageBox.Show("请选择收费记录!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FS.HISFC.Models.Fee.Outpatient.Balance balance = this.neuSpread1_Sheet1.Cells[iActiveRow, 6].Value as FS.HISFC.Models.Fee.Outpatient.Balance;
            if (balance == null)
            {
                MessageBox.Show("请选择收费记录!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ArrayList alBalance = outPatientManager.QueryBalancesByInvoiceNO(balance.Invoice.ID);
            if (alBalance == null)
            {
                MessageBox.Show("查询发票信息失败！");
                return;
            }

            balance = null;
            foreach (FS.HISFC.Models.Fee.Outpatient.Balance invoice in alBalance)
            {
                if (invoice.TransType == FS.HISFC.Models.Base.TransTypes.Positive && invoice.CancelType == FS.HISFC.Models.Base.CancelTypes.Valid)
                {
                    balance = invoice;
                    break;
                }
            }

            if (balance == null)
            {
                MessageBox.Show("该发票已退费！");
                return;
            }

            this.PrintFeeList(balance);
        }

        private void PrintFeeList(FS.HISFC.Models.Fee.Outpatient.Balance invoice)
        {
            IPrintFeeList print = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(IPrintFeeList)) as IPrintFeeList;
            if (print != null)
            {
                if (print.SetValue(invoice))
                {
                    print.PrintFeeList();
                }
                else
                {
                    MessageBox.Show("设置打印信息失败!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region 重打发票

        private void miReprintInvoice_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1_Sheet1.ActiveRow == null)
            {
                MessageBox.Show("请选择收费记录!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int iActiveRow = this.neuSpread1_Sheet1.ActiveRow.Index;
            if (iActiveRow < 0 && iActiveRow >= this.neuSpread1_Sheet1.RowCount)
            {
                MessageBox.Show("请选择收费记录!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.neuSpread1_Sheet1.Cells[iActiveRow, 6].Value == null)
            {
                MessageBox.Show("请选择收费记录!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FS.HISFC.Models.Fee.Outpatient.Balance balance = this.neuSpread1_Sheet1.Cells[iActiveRow, 6].Value as FS.HISFC.Models.Fee.Outpatient.Balance;
            if (balance == null)
            {
                MessageBox.Show("请选择收费记录!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ArrayList alBalance = outPatientManager.QueryBalancesByInvoiceNO(balance.Invoice.ID);
            if (alBalance == null)
            {
                MessageBox.Show("查询发票信息失败！");
                return;
            }

            balance = null;
            foreach (FS.HISFC.Models.Fee.Outpatient.Balance invoice in alBalance)
            {
                if (invoice.TransType == FS.HISFC.Models.Base.TransTypes.Positive && invoice.CancelType == FS.HISFC.Models.Base.CancelTypes.Valid)
                {
                    balance = invoice;
                    break;
                }
            }

            if (balance == null)
            {
                MessageBox.Show("该发票已退费！");
                return;
            }

            this.ReprintInvoice(balance);
        }

        private void ReprintInvoice(FS.HISFC.Models.Fee.Outpatient.Balance invoice)
        {
            IReprintInvoice print = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(IReprintInvoice)) as IReprintInvoice;
            if (print != null)
            {
                if (print.SetValue(invoice))
                {
                    print.PrintInvoice();
                }
                else
                {
                    MessageBox.Show("设置打印信息失败!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region 补打发票

        private void miReprintInvoiceNoRacl_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1_Sheet1.ActiveRow == null)
            {
                MessageBox.Show("请选择收费记录!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int iActiveRow = this.neuSpread1_Sheet1.ActiveRow.Index;
            if (iActiveRow < 0 && iActiveRow >= this.neuSpread1_Sheet1.RowCount)
            {
                MessageBox.Show("请选择收费记录!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.neuSpread1_Sheet1.Cells[iActiveRow, 6].Value == null)
            {
                MessageBox.Show("请选择收费记录!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FS.HISFC.Models.Fee.Outpatient.Balance balance = this.neuSpread1_Sheet1.Cells[iActiveRow, 6].Value as FS.HISFC.Models.Fee.Outpatient.Balance;
            if (balance == null)
            {
                MessageBox.Show("请选择收费记录!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ArrayList alBalance = outPatientManager.QueryBalancesByInvoiceNO(balance.Invoice.ID);
            if (alBalance == null)
            {
                MessageBox.Show("查询发票信息失败！");
                return;
            }

            balance = null;
            foreach (FS.HISFC.Models.Fee.Outpatient.Balance invoice in alBalance)
            {
                if (invoice.TransType == FS.HISFC.Models.Base.TransTypes.Positive && invoice.CancelType == FS.HISFC.Models.Base.CancelTypes.Valid)
                {
                    balance = invoice;
                    break;
                }
            }

            if (balance == null)
            {
                MessageBox.Show("该发票已退费！");
                return;
            }

            this.ReprintInvoiceNotRollCode(balance);
        }

        private void ReprintInvoiceNotRollCode(FS.HISFC.Models.Fee.Outpatient.Balance invoice)
        {
            IReprintInvoice print = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(IReprintInvoice)) as IReprintInvoice;
            if (print != null)
            {
                if (print.SetValue(invoice))
                {
                    print.PrintInvoiceNotRollCode();
                }
                else
                {
                    MessageBox.Show("设置打印信息失败!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion
        //删除没用的列表数据
        private void miDeleteRow_Click(object sender, EventArgs e)
        {
            int currRow = this.neuSpread1_Sheet1.ActiveRowIndex;
            decimal  pay = decimal.Parse(this.neuSpread1_Sheet1.Cells[currRow, 1].Value.ToString());
            if (pay == 0)
            {
                this.neuSpread1_Sheet1.Rows.Remove(currRow, 1);
            }
            else
            {
                MessageBox.Show("只可以删除未收费信息，已收费或退费不可删除!");
            }
        }

        #region 修改支付方式
        private string invoiceNo = string.Empty;
        private Form form =null;         
        private ucAlterPayMode ucAlterPM = null;

        private void miModifyPayMode_Click(object sender, EventArgs e)
        {
            if (invoiceNo==string.Empty)
            {
             int actRow = this.neuSpread1_Sheet1.ActiveRow.Index;
             FS.HISFC.Models.Fee.Outpatient.Balance balance = this.neuSpread1_Sheet1.Cells[actRow, 6].Value as FS.HISFC.Models.Fee.Outpatient.Balance;
              if (balance == null)
              {
                return;
              }
            invoiceNo = balance.Invoice.ID;
            }
            form = new Form();
            form.Text = "修改患者支付方式";
            form.Width = 720;
            form.Height = 300;
            form.StartPosition = FormStartPosition.CenterParent;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            ucAlterPM =  new ucAlterPayMode();
            form.Load+=new EventHandler(form_Load);
            ucAlterPM.InvoiceNo = invoiceNo;
            ucAlterPM.ShowBtnSave = true;
            ucAlterPM.ShowChkTans = false;
            ucAlterPM.KindEnum = ucAlterPayMode.TransKind.ClinicFee;
            form.Controls.Add(ucAlterPM);
            ucAlterPM.Dock=DockStyle.Fill;
            form.ShowDialog();
            form = null;

        }
        private void form_Load(object sender, EventArgs e)
        {
            if (ucAlterPM.InvoiceNo!=null &&ucAlterPM.InvoiceNo!="")
            {
                ucAlterPM.txtInvoice_KeyDown(null, null);
            }
        }

        #endregion
    }
}
