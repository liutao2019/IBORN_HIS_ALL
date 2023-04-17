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
    /// [��������: ���з���]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucDrugSend : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDrugSend()
        {
            InitializeComponent();
        }

        #region ����
        
        private FS.FrameWork.Public.ObjectHelper usageHelper;
        private FS.FrameWork.Public.ObjectHelper dosageHelper;
        int sendFlag = 2;//1 ���з��� 2 ��ʱ����
       
        #endregion

        #region ����
        protected void InitControl()
        {
            this.fpOrderExecBrowser1.IsShowRowHeader = false;
            try
            {
                this.cmbDept.AddItems(CacheManager.InterMgr.QueryDepartment(CacheManager.LogEmpl.Nurse.ID));
            }
            catch { return; }
            if (this.cmbDept.Items.Count > 0) this.cmbDept.SelectedIndex = 0;
            //���Ƽ��з��Ͳ���ѡ�񲿷�,ֻ��ѡ��ȫ��,��ֹѡ���˲���ҩƷ,ʣ���ҩƷ���ܼ��з��͵�����
            this.fpOrderExecBrowser1.fpSpread.Sheets[0].Columns[1].Locked = true;
            
        }
        
        /// <summary>
        /// ���²�ѯ��ʾ
        /// </summary>
        /// <returns></returns>
        protected int RefreshQuery()
        {
            if (this.cmbDept.Tag == null || this.cmbDept.Tag.ToString() == "")
            {
                MessageBox.Show("����ѡ����ң�");
                return 0;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ�����Ժ�...");
            Application.DoEvents();
            ArrayList alOrders = null;

            this.fpOrderExecBrowser1.fpSpread.Sheets[0].RowCount = 0;

            alOrders = CacheManager.InOrderMgr.QureyExecOrderNeedSendDrug(this.cmbDept.Tag.ToString());//��ѯδ��ҩ��
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
                //"ÿ������ȡ������Ϣ ֻ��¼�������������� ���� ��ԭ���ҷֽ����ת�Ƶ�ʱ��ҩӦ�÷��͵�ԭ���� ��ȡ���Һ��͵��¿���ȥ��"
                if (pid != order.Order.Patient.ID)
                {
                    pid = order.Order.Patient.ID;
                    p = CacheManager.RadtIntegrate.GetPatientInfomation(pid);
                    if (p == null)
                    {
                        MessageBox.Show("��û���סԺ�ų���!\n" + CacheManager.RadtIntegrate.Err);
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
                { //��ʾ�Ѿ��շѵ�
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
            bool b = true;//ȫѡ
            for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows.Count; i++)
            {
                this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, this.fpOrderExecBrowser1.ColumnIndexSelection].Value = b;
            }
            return 0;
        }
      
        /// <summary>
        /// ����ҽ��
        /// </summary>
        /// <returns></returns>
        public int ComfirmExec()
        {
            
            FS.HISFC.Models.Order.ExecOrder order = null;
            usageHelper = new FS.FrameWork.Public.ObjectHelper(CacheManager.GetConList(FS.HISFC.Models.Base.EnumConstant.USAGE));
            dosageHelper = new FS.FrameWork.Public.ObjectHelper(CacheManager.GetConList(FS.HISFC.Models.Base.EnumConstant.DOSAGEFORM));
           
            #region ���
            List<FS.HISFC.Models.Order.ExecOrder> alExecOrder = new List<FS.HISFC.Models.Order.ExecOrder>();
            for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].RowCount; i++)
            {
                if (this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, this.fpOrderExecBrowser1.ColumnIndexSelection].Text.ToUpper() == "TRUE")
                {
                    order = this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].Tag as FS.HISFC.Models.Order.ExecOrder;
                    if (order == null)
                    {
                        
                        MessageBox.Show("û��ѯ��ҽ����");
                        return -1;
                    }
                    try
                    {
                        order.Order.Usage.Name = usageHelper.GetName(order.Order.Usage.ID);
                    }
                    catch
                    {
                        MessageBox.Show("����÷�����"+order.Order.Usage.ID);
                        return -1;
                    }
                    try
                    {
                        ((FS.HISFC.Models.Pharmacy.Item)order.Order.Item).DosageForm.Name = dosageHelper.GetName(((FS.HISFC.Models.Pharmacy.Item)order.Order.Item).DosageForm.ID);
                    }
                    catch
                    {
                        MessageBox.Show("���ҩƷ���ͳ���" + ((FS.HISFC.Models.Pharmacy.Item)order.Order.Item).DosageForm.ID);
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

        #region �¼�

        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            bool b = false;
            if (this.chkAll.Checked) //ȫѡ
            {
                b = true;
            }
            else//ȡ��
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
                        this.lblSendInfo.Text = "�����ˣ�" + obj.OperEnvironment.ID + "   ����ʱ�䣺" + dateTimeTmp;
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
                if (MessageBox.Show("�Ƿ��з���!", "ȷ��", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    sendFlag = 1;//0 ������ 1 ���з��� 2 ��ʱ����
                    if (ComfirmExec() == -1) return;
                    if (Classes.Function.HaveDruged(this.cmbDept.Tag.ToString()) == -1) return;
                }
            }
            else
            {
                this.sendFlag = 2;//��ʱ����
                ComfirmExec();
            }
        }

        private void lnkDefault_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("�Ƿ�ָ����з�ҩ��ǣ�ȷ�Ϻ�ɽ��н���Ķ��μ��з��ͣ�", "��ʾ", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                  if (Classes.Function.NotHaveDruged(this.cmbDept.Tag.ToString()) == 1)
                  {
                      this.btnSend.Enabled = true;
                      this.lnkDefault.Visible = false;
                      MessageBox.Show("�ָ��ɹ���");
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

        #region ��д
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
