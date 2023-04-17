using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Models;
using FS.FrameWork.Function;

namespace FS.Report.Finance.FinOpb
{
    public partial class ucCollectDayBalance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCollectDayBalance()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// �ս᷽����
        /// </summary>

        Function.ClinicDayBalance clinicDayBalance = new FS.Report.Finance.FinOpb.Function.ClinicDayBalance();
        /// <summary>
        /// ��ǰ����Ա
        /// </summary>
        NeuObject currentOperator = new NeuObject();
        
        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        /// <summary>
        /// �ս����
        /// </summary>
        private string balanceNo = string.Empty;
        #endregion


        #region ����

        #region �������ս�����
        private void QueryDayBalanceRecord(string balanceNO)
        {
            // ����ֵ
            int intReturn = 0;
            // ��ѯ����ʼʱ��
            DateTime dtFrom = DateTime.MinValue;
            // ��ѯ�Ľ�ֹʱ��
            DateTime dtTo = DateTime.MinValue;
            // ��ѯ���ռ���ˮ��
            string sequence = "";

            //�������
            int count = this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Rows.Count;
            if (count > 0)
            {
                this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Rows.Remove(0, count);
            }
            
            DataSet ds=new DataSet();
            intReturn = this.clinicDayBalance.GetCollectDayBalanceData(balanceNO, ref ds);
            if (intReturn == -1)
            {
                MessageBox.Show(this.clinicDayBalance.Err);
                return;
            }
            //���ñ�����Ϣ
            //this.SetInfo(begin, end, 1);

            if (ds.Tables.Count == 0 || ds == null || ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("��ʱ�����û��Ҫ���ҵ����ݣ�");
                return;
            }
            SetOldFarPointData(ds.Tables[0]);
            ds.Dispose();
        }
        #endregion

        #region �������ս�Farpoint����
        private void SetOldFarPointData(DataTable table)
        {
            FarPoint.Win.Spread.SheetView sheet = this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1;
            int rowCount = sheet.Rows.Count;
            if (sheet.Rows.Count > 0)
            {
                sheet.Rows.Remove(0, rowCount - 1);
            }
            DataView dv = table.DefaultView;
            //������Ŀ��ϸ
            SetDetialed(sheet, dv);
            this.ucClinicDayBalanceReportNew1.SetFarPoint();
            this.SetInvoiced(sheet, dv);
            this.SetMoneyed(sheet, dv);
        }

        /// <summary>
        /// �������սᷢƱ��Ϣ
        /// </summary>
        /// <param name="sheet">FarPoint.Win.Spread.SheetView</param>
        /// <param name="dv">DataView</param>
        protected virtual void SetInvoiced(FarPoint.Win.Spread.SheetView sheet, DataView dv)
        {
            dv.RowFilter = "BALANCE_ITEM='5'";
            this.SetFarpointValue(sheet, dv);
        }

        protected virtual void SetMoneyed(FarPoint.Win.Spread.SheetView sheet, DataView dv)
        {
            dv.RowFilter = "BALANCE_ITEM='6'";
            this.SetFarpointValue(sheet, dv);
        }

        protected virtual void SetFarpointValue(FarPoint.Win.Spread.SheetView sheet, DataView dv)
        {
            if (dv.Count > 0)
            {
                string fieldStr = string.Empty;
                string tagStr = string.Empty;
                string field = string.Empty;
                int Index = 0;
                for (int k = 0; k < dv.Count; k++)
                {
                    fieldStr = dv[k]["sort_id"].ToString();
                    int index = fieldStr.IndexOf('��');
                    if (index == -1)
                    {
                        Index = fieldStr.IndexOf("|");
                        tagStr = fieldStr.Substring(0, Index);
                        field = fieldStr.Substring(Index + 1);
                        SetOneCellText(sheet, tagStr, dv[k][field].ToString());
                        if (dv[k][1].ToString() == "A023")
                        {
                            SetOneCellText(sheet, "A1000", NConvert.ToCapital(NConvert.ToDecimal(dv[k][field])));
                        }
                    }
                    else
                    {
                        string[] aField = fieldStr.Split('��');
                        if (aField.Length == 0) continue;
                        string s = aField[0];
                        Index = s.IndexOf("|");
                        tagStr = s.Substring(0, Index);
                        field = s.Substring(Index + 1);
                        SetOneCellText(sheet, tagStr, dv[k][field].ToString());
                    }
                }
            }
        }

        /// <summary>
        /// �������ս���Ŀ��ϸ
        /// </summary>
        /// <param name="sheet">FarPoint.Win.Spread.SheetView</param>
        /// <param name="dv">DataView</param>
        private void SetDetialed(FarPoint.Win.Spread.SheetView sheet, DataView dv)
        {
            #region ��ʾ��Ŀ����
            //��Ŀ����
            dv.RowFilter = "BALANCE_ITEM='4'";
            int count = dv.Count;
            decimal countMoney = 0;
            if (count > 0)
            {
                if (count % 2 == 0)
                {
                    sheet.Rows.Count = Convert.ToInt32(count / 2);
                }
                else
                {
                    sheet.Rows.Count = Convert.ToInt32(count / 2) + 1;
                }

                //��ʾ��Ŀ����
                for (int i = 0; i < count; i++)
                {
                    int index = Convert.ToInt32(i / 2);
                    int intMod = (i + 1) % 2;
                    if (intMod > 0)
                    {
                        sheet.Models.Span.Add(index, 0, 1, 2);
                        sheet.Cells[index, 0].Text = dv[i]["extent_field1"].ToString();
                        sheet.Cells[index, 2].Text = dv[i]["tot_cost"].ToString();
                    }
                    else
                    {
                        sheet.Models.Span.Add(index, 3, 1, 2);
                        sheet.Cells[index, 3].Text = dv[i]["extent_field1"].ToString();
                        sheet.Cells[index, 5].Text = dv[i]["tot_cost"].ToString();
                    }
                    countMoney += Convert.ToDecimal(dv[i][0]);

                }
                if (count % 2 > 0)
                {
                    sheet.Models.Span.Add(sheet.Rows.Count - 1, 3, 1, 2);
                }
                //��ʾ�ϼ�
                sheet.Rows.Count += 1;
                count = sheet.Rows.Count;
                sheet.Models.Span.Add(count - 1, 0, 1, 2);
                sheet.Cells[count - 1, 0].Text = "�ϼƣ�";
                sheet.Models.Span.Add(count - 1, 2, 1, 4);
                sheet.Cells[count - 1, 2].Text = countMoney.ToString();
            }
            #endregion
        }
        #endregion

        #region ��tag��ȡFarPoint��cell����
        /// <summary>
        /// ���õ���Cell��Text
        /// </summary>
        /// <param name="sheet">SheetView</param>
        /// <param name="tagStr">Cell��tag</param>
        /// <param name="strText">Ҫ��ʾ��Text</param>
        private void SetOneCellText(FarPoint.Win.Spread.SheetView sheet, string tagStr, string strText)
        {
            FarPoint.Win.Spread.Cell cell = sheet.GetCellFromTag(null, tagStr);
            if (cell != null)
            {
                FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                t.Multiline = true;
                t.WordWrap = true;
                cell.CellType = t;
                cell.Text += strText;
            }
        }

        private string GetOneCellText(FarPoint.Win.Spread.SheetView sheet, string tagStr)
        {
            FarPoint.Win.Spread.Cell cell = sheet.GetCellFromTag(null, tagStr);
            if (cell != null)
                return cell.Text;
            return string.Empty;
        }
        #endregion

        #region ����ToolBar����
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
     
            toolBarService.AddToolButton("����", "�����ս����ͳ��", (int)FS.FrameWork.WinForms.Classes.EnumImageList.H�ϲ�, true, false, null);
            #region {A233C411-4B52-4831-AF89-8D7C2CE8D09E} �ս���ܼӲ�����
            toolBarService.AddToolButton("����", "�����ѻ����ս����ͳ��", (int)FS.FrameWork.WinForms.Classes.EnumImageList.H�ϲ�, true, false, null); 
            #endregion

            toolBarService.AddToolButton("ȷ��", "������ܼ�¼", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            

            return toolBarService;
        }
        #endregion

        #region ����
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        protected virtual void GetCollectData()
        {
            ucCollectDayBalanceInfo c = new ucCollectDayBalanceInfo();
            DialogResult result = FS.FrameWork.WinForms.Classes.Function.PopShowControl(c);
            if (result == DialogResult.OK)
            {
                balanceNo = c.BalaceNO;
                this.QueryDayBalanceRecord(balanceNo);
            }
            else
            {
                balanceNo = string.Empty;
            }
        }

        #region {A233C411-4B52-4831-AF89-8D7C2CE8D09E} �ս���ܼӲ�����
        /// <summary>
        /// ��ȡ��������,������
        /// </summary>
        protected virtual void GetCheckedCollectData()
        {
            ucCollectDayBalanceInfo c = new ucCollectDayBalanceInfo();
            c.ckRePrint.Checked = true;
            DialogResult result = FS.FrameWork.WinForms.Classes.Function.PopShowControl(c);
            if (result == DialogResult.OK)
            {
                balanceNo = c.BalaceNO;
                this.QueryDayBalanceRecord(balanceNo);
                balanceNo = string.Empty;
            }
            else
            {
                balanceNo = string.Empty;
            }
        } 
        #endregion
        /// <summary>
        /// �����������
        /// </summary>
        protected virtual void SaveCollectData()
        {
            if (balanceNo == string.Empty)
            {
                #region {A233C411-4B52-4831-AF89-8D7C2CE8D09E} �ս���ܼӲ�����
                MessageBox.Show("���Ȼ������ݣ�"); 
                #endregion
                return;
            }
            DialogResult result = MessageBox.Show("ȷ��Ҫ�������ݣ�", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;
            DateTime operTime = clinicDayBalance.GetDateTimeFromSysDateTime();
            if (clinicDayBalance.SaveCollectData(currentOperator.ID, operTime, balanceNo) <1)
            {
                MessageBox.Show("����ɹ���");
                return ;
            }
            balanceNo = string.Empty;
            MessageBox.Show("����ɹ���");
        }
            
        #endregion
        #endregion

        #region �¼�
        protected override int OnQuery(object sender, object neuObject)
        {
            //this.QueryDayBalanceRecord();
            return base.OnQuery(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.PrintPage(0, 0, this.panelPrint);
            return base.OnPrint(sender, neuObject);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "����":
                    {
                        GetCollectData();
                        break;
                    }
                #region {A233C411-4B52-4831-AF89-8D7C2CE8D09E} �ս���ܼӲ�����
                case "����":
                    {
                        GetCheckedCollectData();
                        break;
                    } 
                #endregion
                case "ȷ��":
                    {
                        SaveCollectData();
                        break;
                    }
            }
            
            base.ToolStrip_ItemClicked(sender, e);
        }

        private void ucCollectDayBalance_Load(object sender, EventArgs e)
        {
            currentOperator = this.clinicDayBalance.Operator;
            //��ʾ�ڻ�������
            this.ucClinicDayBalanceReportNew1.IsCollectData = true;
            this.ucClinicDayBalanceReportNew1.InitUC("�����շ�Ա�ս���ܱ���");
        }

        #endregion

        
    }
}
