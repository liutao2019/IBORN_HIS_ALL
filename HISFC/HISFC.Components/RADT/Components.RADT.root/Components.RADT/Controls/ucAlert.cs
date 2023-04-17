using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Neusoft.FrameWork.Models;
using Neusoft.FrameWork.Function;
using System.Collections;

namespace Neusoft.HISFC.Components.RADT.Controls
{
    public partial class ucAlert : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucAlert()
        {
            InitializeComponent();
        }

        #region ����

        private NeuObject NurseCode = null;

        private string Err=string.Empty;
        Neusoft.HISFC.BizLogic.RADT.InPatient Patient = new Neusoft.HISFC.BizLogic.RADT.InPatient();
        Neusoft.HISFC.BizProcess.Integrate.Manager managerIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Manager();
        private Neusoft.HISFC.Models.RADT.PatientInfo mypatientinfo = new Neusoft.HISFC.Models.RADT.PatientInfo();
        Neusoft.HISFC.BizProcess.Interface.FeeInterface.IMoneyAlert IAlterPrint = null;
     
        Neusoft.HISFC.BizProcess.Integrate.Fee FeeInterate = new  Neusoft.HISFC.BizProcess.Integrate.Fee();
    
        /// <summary>
        /// ���Ʋ���ҵ����
        /// </summary>
        Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParmMgr = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// �߿������뷽ʽ
        /// 0����¼��
        /// 1����ӡ�߿ʱÿ�˵��������
        /// 2������������
        /// </summary>
        private string repayType = "0";

        #endregion

        #region ����
        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        public void initControl()
        {
            this.cmbType.SelectedIndex = 0;
            this.txtAlert.Text = "0";

            #region �����ȡǷ�Ѵ�ӡ��
            object[] o = new object[] { };
            try
            {
                //��ⱨ��
                Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();

                string billValue = ctrlIntegrate.GetControlParam<string>(Neusoft.HISFC.BizProcess.Integrate.Const.RADT_MoneyAlter, true, "Neusoft.WinForms.Report.InpatientFee.ucPatientMoneyAlter");

                System.Runtime.Remoting.ObjectHandle objHande = System.Activator.CreateInstance("WinForms.Report", billValue, false, System.Reflection.BindingFlags.CreateInstance, null, o, null, null, null);

                object oLabel = objHande.Unwrap();

                IAlterPrint = oLabel as Neusoft.HISFC.BizProcess.Interface.FeeInterface.IMoneyAlert;
            }
            catch (System.TypeLoadException ex)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show( "�����ռ���Ч\n" + ex.Message );
                return;
            }
            #endregion

            #region {80C40729-D5C1-42ce-96C3-7CF09E562BA7}
            this.repayType = this.ctrlParmMgr.GetControlParam<string>("200309", true, "0");

            if (this.repayType == "2")
            {
                this.neuSpread1_Sheet1.Columns[11].Visible = true;
            }
            else
            {
                this.neuSpread1_Sheet1.Columns[11].Visible = false;
            }
            #endregion
        }

        /// <summary>
        /// ������ʾ������Ϣ
        /// </summary>
        /// <param name="myNurse"></param>
        public void RefreshList(string myNurse)
        {
            this.labDay.Text = "��ӡ����:" + this.Patient.GetSysDate();
            string alertText = "";
            string myWhere = "'" + myNurse + "'";
            ArrayList list = new ArrayList();
            if (string.IsNullOrEmpty(this.txtAlert.Text.Trim()))
            {
                MessageBox.Show("�������ѯ��");
                return ;
            }
            try
            {
                if (this.cmbType.Text == "��ָ����׼")
                {
                    list = this.Patient.GetAlertPerson(myWhere, NConvert.ToDecimal(this.txtAlert.Text));
                    alertText = this.txtAlert.Text.ToString();
                    
                    if (alertText[0] == '-')
                        alertText = alertText.Remove(0, 1);
                }
                else if (this.cmbType.Text == "������")
                {
                    list = this.Patient.GetAlertPercent(myWhere, NConvert.ToDecimal(this.txtAlert.Text) / 100);
                    alertText = this.txtAlert.Text.ToString();
                    if (alertText[0] == '-')
                        alertText = alertText.Remove(0, 1);

                }
                else if (this.cmbType.Text == "���������")
                {
                    list = this.Patient.GetAlertPerson(myWhere);
                    alertText = this.txtAlert.Text.ToString();
                    if (alertText[0] == '-')
                        alertText = alertText.Remove(0, 1);
                }
                string date = "";

                //������ݵ��ؼ���
                NeuObject obj = null;
                Neusoft.HISFC.Models.RADT.PatientInfo patient = null;
                if (list == null)
                {
                    list = new ArrayList();
                }
                
                this.neuSpread1_Sheet1.RowCount = list.Count;
                for (int i = 0; i < list.Count; i++)
                {
                    patient = list[i] as Neusoft.HISFC.Models.RADT.PatientInfo;
                    if (patient == null) return;
                    
                    obj = this.managerIntegrate.GetConstant(Neusoft.HISFC.Models.Base.EnumConstant.PAYKIND.ToString(),patient.Pact.PayKind.ID);
                    this.neuSpread1_Sheet1.Cells[i, 0].Value = true;
                    this.neuSpread1_Sheet1.Cells[i, 1].Value = patient.PVisit.PatientLocation.Bed.ID.Substring(4);
                    this.neuSpread1_Sheet1.Cells[i, 3].Value = patient.PID.ID;
                    this.neuSpread1_Sheet1.Cells[i, 2].Value = patient.Name;
                    this.neuSpread1_Sheet1.Cells[i, 4].Value = obj.Name;
                    this.neuSpread1_Sheet1.Cells[i, 5].Value = patient.Pact.Name;
                    this.neuSpread1_Sheet1.Cells[i, 6].Value = patient.FT.PrepayCost.ToString();
                    this.neuSpread1_Sheet1.Cells[i, 7].Value = patient.FT.TotCost.ToString();

                    this.neuSpread1_Sheet1.Cells[i, 8].Value = (patient.FT.PrepayCost - patient.FT.LeftCost).ToString();//�Ը����
                    this.neuSpread1_Sheet1.Cells[i, 9].Value = patient.FT.LeftCost.ToString();
                   // this.neuSpread1_Sheet1.Cells[i, 10].Value = patient.PVisit.MoneyAlert.ToString();//patient.PVisit.PatientLocation.Dept.Name; //λ�÷ŵ�����
                    date = patient.PVisit.InTime.Year.ToString().Substring(2, 2) + "-";
                    if (patient.PVisit.InTime.Month < 10)
                        date = date + "0" + patient.PVisit.InTime.Month.ToString() + "-";
                    else
                        date = date + patient.PVisit.InTime.Month.ToString() + "-";
                    if (patient.PVisit.InTime.Day < 10)
                        date = date + "0" + patient.PVisit.InTime.Day.ToString();
                    else
                        date = date + patient.PVisit.InTime.Day.ToString();


                    this.neuSpread1_Sheet1.Cells[i, 10].Value = date;

                    this.neuSpread1_Sheet1.Cells[i, 10].Value = patient.PVisit.MoneyAlert.ToString();//patient.PVisit.PatientLocation.Dept.Name;
                    this.neuSpread1_Sheet1.Rows[i].Tag = patient;

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
        }
        #endregion

        #region �¼�
        private void ucAlert_Load(object sender, EventArgs e)
        {
            try
            {
                NurseCode = (Patient.Operator as Neusoft.HISFC.Models.Base.Employee).Nurse;
                //���¼���ҽ�����
                //ArrayList alPatientInfo = this.Patient.GetAlertPerson("'"+NurseCode.ID+"'", 10000);
                //DateTime dtEnd=this.Patient.GetDateTimeFromSysDateTime();
                //foreach (Neusoft.HISFC.Models.RADT.PatientInfo patient in alPatientInfo)
                //{
                //    if (-1 == this.FeeInterate.ComputeSiFreeCost(patient, dtEnd))
                //    {
                //        continue;
                //    }
                //}

                initControl();
                RefreshList(NurseCode.ID);
            }
            catch { }
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbType.Text == "��ָ����׼")
            {
                this.labShow.Text = "ָ�꣺";
                this.labShow.Visible = true;
                this.txtAlert.Visible = true;
                this.labPercent.Visible = false;
                this.lblInfo.Text = "(������� < " + this.txtAlert.Text + ")";
            }
            else if (this.cmbType.Text == "������")
            {
                this.labShow.Text = "������";
                this.labShow.Visible = true;
                this.txtAlert.Visible = true;
                this.labPercent.Visible = true;
                this.lblInfo.Text = "(��� / Ԥ���� <= " + this.txtAlert.Text + "%)";
            }
            else if (this.cmbType.Text == "���������")
            {
                this.labShow.Visible = false;
                this.txtAlert.Visible = false;
                this.labPercent.Visible = false;
                this.lblInfo.Text = "(���������ͳ��)";
            }
        }

        private void txtAlert_TextChanged(object sender, EventArgs e)
        {
            if (this.cmbType.Text == "��ָ����׼")
            {
                this.labShow.Text = "ָ�꣺";
                this.labShow.Visible = true;
                this.txtAlert.Visible = true;
                this.labPercent.Visible = false;
                if (this.chkAll.Checked)
                {
                    this.lblInfo.Text = "";
                }
                else
                {
                    this.lblInfo.Text = "(������� < " + this.txtAlert.Text + ")";
                }
            }
            else if (this.cmbType.Text == "������")
            {
                this.labShow.Text = "������";
                this.labShow.Visible = true;
                this.txtAlert.Visible = true;
                this.labPercent.Visible = true;
                this.lblInfo.Text = "(��� / Ԥ���� <= " + this.txtAlert.Text + "%)";
            }
            else if (this.cmbType.Text == "���������")
            {
                this.labShow.Visible = false;
                this.txtAlert.Visible = false;
                this.labPercent.Visible = false;
                this.lblInfo.Text = "(���������ͳ��)";
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkAll.Checked)
                this.txtAlert.Text = "1000000";
            else
                this.txtAlert.Text = "0";

            this.RefreshList(NurseCode.ID);
        }

        

        private void btnCompute_Click(object sender, EventArgs e)
        {
            RefreshList(this.NurseCode.ID);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            //print.PrintPage(0, 0, this.neuPanel1);
            for(int i =0;i< this.neuSpread1_Sheet1.RowCount;i++)
            {
                if (this.neuSpread1_Sheet1.Cells[i, 0].Value.ToString() == "True")
                {
                    #region {80C40729-D5C1-42ce-96C3-7CF09E562BA7}
                    Neusoft.HISFC.Models.RADT.PatientInfo selectPatient = this.neuSpread1_Sheet1.Rows[i].Tag as Neusoft.HISFC.Models.RADT.PatientInfo;
                    if (selectPatient.Pact.PayKind.ID =="02")
                    {
                        selectPatient.FT.OwnCost = selectPatient.FT.PrepayCost - selectPatient.FT.LeftCost;
                        selectPatient.FT.PayCost = 0;
                    }
                    //��������ֶδ߿������뷽ʽ
                    selectPatient.PVisit.AdmittingDoctor.User02 = this.repayType;

                    if (this.repayType == "2")
                    {
                        //��������ֶδ洢����Ľ��
                        selectPatient.PVisit.AdmittingDoctor.User01 = this.neuSpread1_Sheet1.Cells[i, 11].Text;
                    }

                    this.IAlterPrint.PatientInfo = selectPatient;
                    #endregion
                    this.IAlterPrint.SetPatientInfo();
                }
            }
        }
        #endregion

        private void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader && e.Column == 0 )
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    this.neuSpread1_Sheet1.Cells[i, 0].Value = true;
                }
            }
        }

        /// <summary>
        /// {1C1BD0C8-A5E2-4da2-BE99-B31BB34226EA}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                this.neuSpread1_Sheet1.Cells[i, 0].Value = this.chkSelectAll.Checked;
                
            }
        }

        /// <summary>
        /// {80C40729-D5C1-42ce-96C3-7CF09E562BA7}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            this.neuSpread1_Sheet1.Columns[4].Visible = false;
            this.neuSpread1_Sheet1.Columns[0].Visible = false;
            this.neuSpread1_Sheet1.Columns[11].Visible = false;

            Neusoft.FrameWork.WinForms.Classes.Print p = new Neusoft.FrameWork.WinForms.Classes.Print();

            p.PrintPreview(this.neuPanel1);

            this.neuSpread1_Sheet1.Columns[4].Visible = true;
            this.neuSpread1_Sheet1.Columns[0].Visible = true;
            if (this.repayType == "2")
            {
                this.neuSpread1_Sheet1.Columns[11].Visible = true;
            }
            else
            {
                this.neuSpread1_Sheet1.Columns[11].Visible = false;
            }
            
        }

    }
}
