using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// 医嘱执行档确认收费
    /// </summary>
    public partial class ucOrderExecChargeConfirm : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucOrderExecChargeConfirm()
        {
            InitializeComponent();
        }

        FS.HISFC.Models.RADT.PatientInfo myPatientInfo = null;
        
        private string LONGSETTINGFILENAME = FS.FrameWork.WinForms.Classes.Function.CurrentPath +
            FS.FrameWork.WinForms.Classes.Function.SettingPath + @"/Order/LongFeeOrderSetting.xml";
        private string SHORTSETTINGFILENAME = FS.FrameWork.WinForms.Classes.Function.CurrentPath +
            FS.FrameWork.WinForms.Classes.Function.SettingPath + "/Order/ShortFeeOrderSetting.xml"; 

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            TreeView tv = sender as TreeView;
            if (tv != null && this.tv.CheckBoxes == true)
            {
                tv.CheckBoxes = false;
                tv.ExpandAll();
            }
            return null;
        }

        protected override void OnLoad(EventArgs e)
        {
            this.fpLongExecOrder.ActiveSheet.RowCount = 0;
            fpLongExecOrder.ActiveSheet.ColumnCount = (int)LongOrderColumn.列数;
            this.fpShortExecOrder.ActiveSheet.RowCount = 0;
            fpShortExecOrder.ActiveSheet.ColumnCount = (int)LongOrderColumn.列数;

            fpShortExecOrder.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpShortExecOrder_ColumnWidthChanged);

            fpLongExecOrder.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpLongExecOrder_ColumnWidthChanged);

            fpLongExecOrder.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpLongExecOrder_ButtonClicked);

            fpShortExecOrder.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpShortExecOrder_ButtonClicked);
        
            fpLongExecOrder.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(fpLongExecOrderSelectAll);

            fpShortExecOrder.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(fpShortExecOrderSelectAll);
        }

        void fpLongExecOrderSelectAll(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader && e.Column == 0)
            {
                string select = "";
                for (int i = 0; i < fpLongExecOrder.ActiveSheet.RowCount; i++)
                {
                    select = this.fpLongExecOrder_Sheet1.GetValue(i, (int)LongOrderColumn.选择).ToString();
                    if (select.Equals("0"))
                    {
                        this.fpLongExecOrder_Sheet1.SetValue(i, (int)LongOrderColumn.选择, true);
                    }
                    else
                    {
                        this.fpLongExecOrder_Sheet1.SetValue(i, (int)LongOrderColumn.选择, false);
                    }
                }
            }
        }

        void fpShortExecOrderSelectAll(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader && e.Column == 0)
            {
                string select = "";
                for (int i = 0; i < fpShortExecOrder.ActiveSheet.RowCount; i++)
                {
                    select = this.fpShortExecOrder_Sheet1.GetValue(i, (int)LongOrderColumn.选择).ToString();
                    if (select.Equals("0"))
                    {
                        this.fpShortExecOrder_Sheet1.SetValue(i, (int)LongOrderColumn.选择, true);
                    }
                    else
                    {
                        this.fpShortExecOrder_Sheet1.SetValue(i, (int)LongOrderColumn.选择, false);
                    }
                }
            }
        }

        void fpShortExecOrder_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == (int)LongOrderColumn.选择)
            {
                for (int i = 0; i < fpShortExecOrder.ActiveSheet.RowCount; i++)
                {
                    if (i != e.Row
                        && fpShortExecOrder.ActiveSheet.Cells[i, (int)LongOrderColumn.组合号].Text == fpShortExecOrder.ActiveSheet.Cells[e.Row, (int)LongOrderColumn.组合号].Text)
                    {
                        fpShortExecOrder.ActiveSheet.Cells[i, (int)LongOrderColumn.选择].Value = fpShortExecOrder.ActiveSheet.Cells[e.Row, (int)LongOrderColumn.选择].Value;
                    }
                }
            }
        }

        void fpLongExecOrder_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == (int)LongOrderColumn.选择)
            {
                for (int i = 0; i < fpLongExecOrder.ActiveSheet.RowCount; i++)
                {
                    if (i != e.Row
                        && fpLongExecOrder.ActiveSheet.Cells[i, (int)LongOrderColumn.组合号].Text == fpLongExecOrder.ActiveSheet.Cells[e.Row, (int)LongOrderColumn.组合号].Text)
                    {
                        fpLongExecOrder.ActiveSheet.Cells[i, (int)LongOrderColumn.选择].Value = fpLongExecOrder.ActiveSheet.Cells[e.Row, (int)LongOrderColumn.选择].Value;
                    }
                }
            }
        }

        void fpLongExecOrder_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            try
            {
                fpLongExecOrder.SaveSchema(LONGSETTINGFILENAME);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void fpShortExecOrder_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            try
            {
                fpShortExecOrder.SaveSchema(SHORTSETTINGFILENAME);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (tv != null && this.tv.CheckBoxes == true)
            {
                tv.CheckBoxes = false;
                tv.ExpandAll();
            }
            this.myPatientInfo = neuObject as FS.HISFC.Models.RADT.PatientInfo;
            return ShowData();
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            return ShowData();
        }

        protected override int OnSave(object sender, object neuObject)
        {
            return this.Save();
            return base.OnSave(sender, neuObject);
        }

        private int ShowData()
        {
            if (myPatientInfo == null)
            {
                return -1;
            }

            this.lblPatientInfo.Text = myPatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "床 " + myPatientInfo.Name + " " + myPatientInfo.Sex.Name + " " + CacheManager.InOrderMgr.GetAge(myPatientInfo.Birthday) + " " + myPatientInfo.Pact.Name + " 总费用 " + myPatientInfo.FT.TotCost.ToString("F4").TrimEnd('0').TrimEnd('.') + "元 预交金 " + myPatientInfo.FT.PrepayCost.ToString("F4").TrimEnd('0').TrimEnd('.') + "元 余额 " + myPatientInfo.FT.LeftCost.ToString("F4").TrimEnd('0').TrimEnd('.') + "元";

            DataSet dsLongExecOrder = null;
            
            if (CacheManager.InOrderMgr.ExecChargeConfirm(myPatientInfo.ID, "LONG", ref dsLongExecOrder) == -1)
            {
                MessageBox.Show(CacheManager.InOrderMgr.Err);
                return -1;
            }
            fpLongExecOrder.ActiveSheet.DataSource = dsLongExecOrder;
            Classes.Function.DrawComboLeft(fpLongExecOrder.ActiveSheet, (int)LongOrderColumn.组合号, (int)LongOrderColumn.组合标记);

            DataSet dsShortExecOrder = null;

            if (CacheManager.InOrderMgr.ExecChargeConfirm(myPatientInfo.ID, "SHORT", ref dsShortExecOrder) == -1)
            {
                MessageBox.Show(CacheManager.InOrderMgr.Err);
                return -1;
            }
            fpShortExecOrder.ActiveSheet.DataSource = dsShortExecOrder;
            Classes.Function.DrawComboLeft(fpShortExecOrder.ActiveSheet, (int)LongOrderColumn.组合号, (int)LongOrderColumn.组合标记);

            if (System.IO.File.Exists(LONGSETTINGFILENAME))
            {
                fpLongExecOrder.ReadSchema(LONGSETTINGFILENAME);
            }
            if (System.IO.File.Exists(SHORTSETTINGFILENAME))
            {
                fpShortExecOrder.ReadSchema(SHORTSETTINGFILENAME);
            }

            return 1;
        }

        private int Save()
        {
            string errInfo = "";
            if (Classes.Function.CheckPatientState(myPatientInfo.ID, ref myPatientInfo, ref errInfo) == -1)
            {
                MessageBox.Show(errInfo);
                return -1;
            }

            if (CacheManager.FeeIntegrate.GetStopAccount(this.myPatientInfo.ID))
            {
                MessageBox.Show(Language.Msg("该患者处于封帐状态，不能进行收费！"));

                return -1;
            }

            if (CacheManager.FeeIntegrate.IsPatientLackFee(myPatientInfo))
            {
                MessageBox.Show("患者【" + myPatientInfo.Name + "】目前处于欠费状态，不能确认收费！");
                return -1;
            }
            string phaExecSeq = "";
            string undrugExecSeq = "";
            for (int i = 0; i < fpLongExecOrder.ActiveSheet.RowCount; i++)
            {
                if (fpLongExecOrder.ActiveSheet.Cells[i, (int)LongOrderColumn.选择].Text == "True")
                {
                    if (fpLongExecOrder.ActiveSheet.Cells[i, (int)LongOrderColumn.药品标记].Text == "1")
                    {
                        if (phaExecSeq == "")
                        {
                            phaExecSeq = "''";
                        }
                        phaExecSeq += ",'" + fpLongExecOrder.ActiveSheet.Cells[i, (int)LongOrderColumn.执行流水号].Text + "'";
                    }
                    else
                    {
                        if (undrugExecSeq == "")
                        {
                            undrugExecSeq = "''";
                        }
                        undrugExecSeq += ",'" + fpLongExecOrder.ActiveSheet.Cells[i, (int)LongOrderColumn.执行流水号].Text + "'";
                    }
                }
            }

            for (int i = 0; i < this.fpShortExecOrder.ActiveSheet.RowCount; i++)
            {
                if (fpShortExecOrder.ActiveSheet.Cells[i, (int)LongOrderColumn.选择].Text == "True")
                {
                    if (fpShortExecOrder.ActiveSheet.Cells[i, (int)LongOrderColumn.药品标记].Text == "1")
                    {
                        if (phaExecSeq == "")
                        {
                            phaExecSeq = "''";
                        }
                        phaExecSeq += ",'" + fpShortExecOrder.ActiveSheet.Cells[i, (int)LongOrderColumn.执行流水号].Text + "'";
                    }
                    else
                    {
                        if (undrugExecSeq == "")
                        {
                            undrugExecSeq = "''";
                        }
                        undrugExecSeq += ",'" + fpShortExecOrder.ActiveSheet.Cells[i, (int)LongOrderColumn.执行流水号].Text + "'";
                    }
                }
            }

            DateTime dtNow = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();

            List<FS.HISFC.Models.Order.ExecOrder> alPhaExecOrder = new List<FS.HISFC.Models.Order.ExecOrder>();
            List<FS.HISFC.Models.Order.ExecOrder> alUndrugExecOrder = new List<FS.HISFC.Models.Order.ExecOrder>();

            if (!string.IsNullOrEmpty(phaExecSeq))
            {
                ArrayList al = CacheManager.InOrderMgr.BetchQueryExecOrder("1", string.Format(@"where exec_sqn in ({0})", phaExecSeq));
                foreach (FS.HISFC.Models.Order.ExecOrder execOrder in al)
                {
                    alPhaExecOrder.Add(execOrder);
                }
            }

            if (!string.IsNullOrEmpty(undrugExecSeq))
            {
                ArrayList al = CacheManager.InOrderMgr.BetchQueryExecOrder("2", string.Format(@"where exec_sqn in ({0})", undrugExecSeq));
                foreach (FS.HISFC.Models.Order.ExecOrder execOrder in al)
                {
                    alUndrugExecOrder.Add(execOrder);
                }
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            CacheManager.OrderIntegrate.MessageType = FS.HISFC.Models.Base.MessType.Y;
            if (alPhaExecOrder.Count > 0)
            {
                if (CacheManager.OrderIntegrate.ComfirmExec(myPatientInfo, alPhaExecOrder, CacheManager.LogEmpl.Nurse.Clone().ID, dtNow, true, true, true) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(CacheManager.OrderIntegrate.Err);
                    return -1;
                }
            }

            if (alUndrugExecOrder.Count > 0)
            {
                if (CacheManager.OrderIntegrate.ComfirmExec(myPatientInfo, alUndrugExecOrder, CacheManager.LogEmpl.Nurse.Clone().ID, dtNow, false, true, true) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(CacheManager.OrderIntegrate.Err);
                    return -1;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("确认收费成功！");

            this.ShowData();

            return 1;
        }

        /// <summary>
        /// 长嘱列设置
        /// </summary>
        private enum LongOrderColumn
        {
            选择,
            执行流水号,
            药品标记,
            组合标记,
            项目编码,
            项目名称,
            组合号,
            数量,
            单位,
            用法,
            频次,
            使用时间,
            取药药房,
            列数
        }
    }
}
