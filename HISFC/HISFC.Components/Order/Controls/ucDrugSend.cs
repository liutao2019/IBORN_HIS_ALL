using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// [功能描述: 集中发送]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2004-10-12]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucDrugSend : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDrugSend()
        {
            InitializeComponent();
        }

        #region 变量
        
        private FS.FrameWork.Public.ObjectHelper usageHelper;
        private FS.FrameWork.Public.ObjectHelper dosageHelper;
        int sendFlag = 2;//1 集中发送 2 临时发送
       
        #endregion

        #region 函数
        protected void InitControl()
        {
            this.fpOrderExecBrowser1.IsShowRowHeader = false;
            try
            {
                this.cmbDept.AddItems(CacheManager.InterMgr.QueryDepartment(CacheManager.LogEmpl.Nurse.ID));
            }
            catch { return; }
            if (this.cmbDept.Items.Count > 0) this.cmbDept.SelectedIndex = 0;
            //控制集中发送不能选择部分,只能选择全部,防止选择了部分药品,剩余的药品不能集中发送的问题
            this.fpOrderExecBrowser1.fpSpread.Sheets[0].Columns[1].Locked = true;
            
        }
        
        /// <summary>
        /// 更新查询显示
        /// </summary>
        /// <returns></returns>
        protected int RefreshQuery()
        {
            if (this.cmbDept.Tag == null || this.cmbDept.Tag.ToString() == "")
            {
                MessageBox.Show("请先选择科室！");
                return 0;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询，请稍候...");
            Application.DoEvents();
            ArrayList alOrders = null;

            this.fpOrderExecBrowser1.fpSpread.Sheets[0].RowCount = 0;

            alOrders = CacheManager.InOrderMgr.QureyExecOrderNeedSendDrug(this.cmbDept.Tag.ToString());//查询未发药的
            if (alOrders == null)
            {
                MessageBox.Show(CacheManager.InOrderMgr.Err);
                return -1;
            }
            string pid = "";
            FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();
            for (int j = 0; j < alOrders.Count; j++)
            {
                FS.HISFC.Models.Order.ExecOrder order = alOrders[j] as FS.HISFC.Models.Order.ExecOrder;

                #region 
                //"每次重新取患者信息 只记录患者姓名、床号 否则 在原科室分解后发生转科的时候，药应该发送到原科室 ，取科室后发送到新科室去了"
                if (pid != order.Order.Patient.ID)
                {
                    pid = order.Order.Patient.ID;
                    p = CacheManager.RadtIntegrate.GetPatientInfomation(pid);
                    if (p == null)
                    {
                        MessageBox.Show("获得患者住院号出错!\n" + CacheManager.RadtIntegrate.Err);
                        return -1;
                    }
                    order.Order.Patient.Name = p.Name;
                    order.Order.Patient.PVisit.PatientLocation.Bed.ID = p.PVisit.PatientLocation.Bed.ID;
                }
                else
                {
                    order.Order.Patient.Name = p.Name;
                    order.Order.Patient.PVisit.PatientLocation.Bed.ID = p.PVisit.PatientLocation.Bed.ID;
                }
                #endregion

                if (order.IsCharge)
                { //显示已经收费的
                    if (this.rdoTemp.Checked)
                    {
                        if (order.Order.OrderType.Type == FS.HISFC.Models.Order.EnumType.SHORT)
                            this.fpOrderExecBrowser1.AddRow(order);
                    }
                    else
                    {
                        this.fpOrderExecBrowser1.AddRow(order);
                    }
                }

            }
            this.fpOrderExecBrowser1.RefreshComboNo();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            bool b = true;//全选
            for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows.Count; i++)
            {
                this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, this.fpOrderExecBrowser1.ColumnIndexSelection].Value = b;
            }
            return 0;
        }
      
        /// <summary>
        /// 发送医嘱
        /// </summary>
        /// <returns></returns>
        public int ComfirmExec()
        {
            
            FS.HISFC.Models.Order.ExecOrder order = null;
            usageHelper = new FS.FrameWork.Public.ObjectHelper(CacheManager.GetConList(FS.HISFC.Models.Base.EnumConstant.USAGE));
            dosageHelper = new FS.FrameWork.Public.ObjectHelper(CacheManager.GetConList(FS.HISFC.Models.Base.EnumConstant.DOSAGEFORM));
           
            #region 获得
            List<FS.HISFC.Models.Order.ExecOrder> alExecOrder = new List<FS.HISFC.Models.Order.ExecOrder>();
            for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].RowCount; i++)
            {
                if (this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, this.fpOrderExecBrowser1.ColumnIndexSelection].Text.ToUpper() == "TRUE")
                {
                    order = this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].Tag as FS.HISFC.Models.Order.ExecOrder;
                    if (order == null)
                    {
                        
                        MessageBox.Show("没查询到医嘱！");
                        return -1;
                    }
                    try
                    {
                        order.Order.Usage.Name = usageHelper.GetName(order.Order.Usage.ID);
                    }
                    catch
                    {
                        MessageBox.Show("获得用法出错！"+order.Order.Usage.ID);
                        return -1;
                    }
                    try
                    {
                        ((FS.HISFC.Models.Pharmacy.Item)order.Order.Item).DosageForm.Name = dosageHelper.GetName(((FS.HISFC.Models.Pharmacy.Item)order.Order.Item).DosageForm.ID);
                    }
                    catch
                    {
                        MessageBox.Show("获得药品剂型出错！" + ((FS.HISFC.Models.Pharmacy.Item)order.Order.Item).DosageForm.ID);
                        return -1;
                    }
                    alExecOrder.Add(order);
                }
            }
          
            #endregion
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(CacheManager.OrderMgr.Connection);
            //t.BeginTransaction();
            CacheManager.OrderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            if (CacheManager.OrderIntegrate.SendDrug(alExecOrder, sendFlag) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();;
                MessageBox.Show(CacheManager.OrderIntegrate.Err);
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            this.RefreshQuery();
            return 0;
        }

        #endregion

        #region 事件

        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            bool b = false;
            if (this.chkAll.Checked) //全选
            {
                b = true;
            }
            else//取消
            {
                b = false;
            }

            for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows.Count; i++)
            {
                this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, this.fpOrderExecBrowser1.ColumnIndexSelection].Value = b;
            }
        }

      
        private void cmbDept_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            
            this.RefreshQuery();
            if (this.cmbDept.Tag != null)
            {
                FS.HISFC.Models.Base.ExtendInfo obj = Classes.Function.IsDeptHaveDruged(this.cmbDept.Tag.ToString());
                if (obj == null)
                    this.btnSend.Enabled = true;
                else
                {
                    this.btnSend.Enabled = !FS.FrameWork.Function.NConvert.ToBoolean(obj.NumberProperty);
                    string dateTimeTmp = obj.DateProperty.Date == new DateTime(1, 1, 1).Date ? string.Empty : obj.DateProperty.ToString();
                    if (!(obj.OperEnvironment.ID == "" && dateTimeTmp == ""))
                    {
                        this.lblSendInfo.Text = "发送人：" + obj.OperEnvironment.ID + "   发送时间：" + dateTimeTmp;
                    }
                    else
                    {
                        this.lblSendInfo.Text = "";
                    }
                     
                }
            }
        }

       

        private void radioButton1_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.rdoTemp.Checked == true)
            {
                this.fpOrderExecBrowser1.fpSpread.Sheets[0].Columns[1].Locked = false;
                this.chkAll.Visible = true;
                this.RefreshQuery();
            }
        }

        private void radioButton2_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.rdoAllSend.Checked == true)
            {
                this.fpOrderExecBrowser1.fpSpread.Sheets[0].Columns[1].Locked = true;
                this.chkAll.Visible = false;
                this.RefreshQuery();
            }
        }
        private void neuButton1_Click(object sender, EventArgs e)
        {
            if (this.fpOrderExecBrowser1.fpSpread.Sheets[0].RowCount <= 0) return;
            if (this.cmbDept.Tag == null) return;
            if (this.rdoAllSend.Checked)
            {
                if (MessageBox.Show("是否集中发送!", "确认", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    sendFlag = 1;//0 待发送 1 集中发送 2 临时发送
                    if (ComfirmExec() == -1) return;
                    if (Classes.Function.HaveDruged(this.cmbDept.Tag.ToString()) == -1) return;
                }
            }
            else
            {
                this.sendFlag = 2;//临时发送
                ComfirmExec();
            }
        }

        private void lnkDefault_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("是否恢复集中发药标记！确认后可进行今天的二次集中发送！", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                  if (Classes.Function.NotHaveDruged(this.cmbDept.Tag.ToString()) == 1)
                  {
                      this.btnSend.Enabled = true;
                      this.lnkDefault.Visible = false;
                      MessageBox.Show("恢复成功！");
                  }
            }
            
        }
        int i = 0;
        private void lblSendInfo_Click(object sender, EventArgs e)
        {
            i++;
            if (i > 100)
                this.lnkDefault.Visible = true;
            else
                this.lnkDefault.Visible = false;
        }
        #endregion

        #region 重写
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.InitControl();
            
            return null;
        }
        protected override int OnSetValues(ArrayList alValues, object e)
        {
            return this.OnQuery(null, null);
        }
        protected override int OnQuery(object sender, object neuObject)
        {
            this.RefreshQuery();
           
            return 0;
        }
        #endregion
    }
}
