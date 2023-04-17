using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.WinForms.Report.FinIpb
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucCheckPrint : System.Windows.Forms.Form, FS.HISFC.BizProcess.Interface.Common.ICheckPrint
    {
        /// <summary>
        /// ҽ����鵥
        /// </summary>
        public ucCheckPrint()
        {
            InitializeComponent();
        }

        FS.HISFC.BizProcess.Integrate.Manager mgrIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        #region ICheckPrint ��Ա

        /// <summary>
        /// ������ 
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="orderList"></param>
        public void ControlValue(FS.HISFC.Models.Registration.Register patient, List<FS.HISFC.Models.Order.OutPatient.Order> orderList)
        {
            if (patient.Pact.Name != null)
                this.neuSpread1_Sheet1.Cells[2, 9].Value = patient.Pact.Name.ToString();//�������
            if (patient.Name != null)
                this.neuSpread1_Sheet1.Cells[3, 1].Value = patient.Name.ToString();//����
            if (patient.Sex != null)
                this.neuSpread1_Sheet1.Cells[3, 3].Value = patient.Sex.ToString();//�Ա�
            if (patient.Age != null)
                this.neuSpread1_Sheet1.Cells[3, 5].Value = patient.Age.ToString();//����
        }
        /// <summary>
        /// סԺ���
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="orderList"></param>
        public void ControlValue(FS.HISFC.Models.RADT.Patient patient, List<FS.HISFC.Models.Order.Inpatient.Order> orderList)
        {

            this.neuSpread1_Sheet1.Cells[0, 0].Value = this.mgrIntegrate.GetHospitalName() + orderList[0].Item.Name.ToString() + "������뵥";

            if (orderList[0].IsEmergency)
            {
                this.neuSpread1_Sheet1.Cells[0, 9].Value = "��  ��";//�Ӽ�
                this.neuSpread1_Sheet1.Cells.Get(0, 9).ForeColor = System.Drawing.Color.Red;
            }
            if (orderList[0].BeginTime!=null)
                this.neuSpread1_Sheet1.Cells[2, 1].Value = orderList[0].BeginTime.ToString();//��������
            if (patient.PID.PatientNO!=null)
                this.neuSpread1_Sheet1.Cells[2, 5].Value = patient.PID.PatientNO.ToString();//סԺ��
            if (patient.Pact.Name!=null)
                this.neuSpread1_Sheet1.Cells[2, 9].Value = patient.Pact.Name.ToString();//�������
            if (patient.Name!=null)
                this.neuSpread1_Sheet1.Cells[3, 1].Value = patient.Name.ToString();//����
            if (patient.Sex!=null)
                this.neuSpread1_Sheet1.Cells[3, 3].Value = patient.Sex.ToString();//�Ա�
            if (patient.Age!=null)
                this.neuSpread1_Sheet1.Cells[3, 5].Value = patient.Age.ToString();//����
            if (orderList[0].NurseStation.Name!=null)
                this.neuSpread1_Sheet1.Cells[3, 7].Value = orderList[0].NurseStation.Name.ToString();//����
            if (orderList[0].Patient.PVisit.PatientLocation.Bed.ID!= null)
                this.neuSpread1_Sheet1.Cells[3, 9].Value = orderList[0].Patient.PVisit.PatientLocation.Bed.ID.ToString();//����         

            FS.HISFC.Models.Fee.Item.Undrug undrug = new FS.HISFC.Models.Fee.Item.Undrug();

            FS.HISFC.BizLogic.Fee.Item item = new FS.HISFC.BizLogic.Fee.Item();

            undrug = item.GetValidItemByUndrugCode(orderList[0].Item.ID.ToString());

            if (undrug.CheckRequest != null)
                this.neuSpread1_Sheet1.Cells[4, 1].Value = undrug.CheckRequest.ToString();//��鲿λ/Ҫ�� 

            #region  ���(����ժ��ucDiagnosis FillList())
            FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBaseMC diagnoseMgrMc = new FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBaseMC();
            FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

            FS.HISFC.Models.HealthRecord.Diagnose diagns = null;
            FS.HISFC.Models.Base.Spell dsType = null;
            FS.HISFC.Models.Base.Employee emp = null;

            //ArrayList diagnoseList = diagnoseMgrMc.QueryDiagnoseBoth(orderList[0].Patient.PVisit.PatientLocation.Bed.InpatientNO.ToString());
            ArrayList diagnoseList = diagnoseMgrMc.QueryDiagnoseBoth(orderList[0].Patient.ID);
            ArrayList dsTypeList = FS.HISFC.Models.HealthRecord.DiagnoseType.SpellList();
            ArrayList drList = managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            String strDsType = "";
            String strDrName = "";
            string diag = string.Empty;
            if (diagnoseList != null)
            {
                for (int i = 0; i < diagnoseList.Count; i++)
                {
                    diagns = (FS.HISFC.Models.HealthRecord.Diagnose)diagnoseList[i];
                    for (int j = 0; j < dsTypeList.Count; j++)
                    {
                        dsType = (FS.HISFC.Models.Base.Spell)dsTypeList[j];
                        if (dsType.ID.ToString() == diagns.DiagInfo.DiagType.ID)
                        {
                            strDsType = dsType.Name;//�������
                            break;
                        }
                    }
                    //�������ҽ������
                    for (int j = 0; j < drList.Count; j++)
                    {
                        emp = (FS.HISFC.Models.Base.Employee)drList[j];
                        if (emp.ID.ToString() == diagns.DiagInfo.Doctor.ID)
                        {
                            strDrName = emp.Name;
                            break;
                        }
                    }
                    if (i == 0)
                        diag = "[" + strDsType + "-" + diagns.DiagInfo.ICD10.Name.ToString() + "-" + strDrName + "]";
                    else
                        diag = diag + "[" + strDsType + "-" + diagns.DiagInfo.ICD10.Name.ToString() + "-" + strDrName + "]";

                }
            }
            this.neuSpread1_Sheet1.Cells[9, 1].Value = diag;
            #endregion

            if (orderList[0].ReciptDoctor.Name != null)
                this.neuSpread1_Sheet1.Cells[19, 1].Value = orderList[0].Memo.ToString();//��ע
            if (orderList[0].Memo != null)
                this.neuSpread1_Sheet1.Cells[25, 9].Value = orderList[0].ReciptDoctor.Name.ToString();//ҽʦ

            if (undrug.Notice != null)
                this.neuSpread1_Sheet1.Cells[22, 1].Value = undrug.Notice.ToString();//ע������          

        }
        /// <summary>
        /// �������
        /// </summary>
        public void Reset()
        {
            this.neuSpread1_Sheet1.Cells[0, 9].Value = string.Empty;
            this.neuSpread1_Sheet1.Cells[2, 1].Value = string.Empty;
            this.neuSpread1_Sheet1.Cells[2, 5].Value = string.Empty;
            this.neuSpread1_Sheet1.Cells[2, 9].Value = string.Empty;
            this.neuSpread1_Sheet1.Cells[3, 1].Value = string.Empty;
            this.neuSpread1_Sheet1.Cells[3, 3].Value = string.Empty;
            this.neuSpread1_Sheet1.Cells[3, 5].Value = string.Empty;
            this.neuSpread1_Sheet1.Cells[3, 7].Value = string.Empty;
            this.neuSpread1_Sheet1.Cells[4, 1].Value = string.Empty;
            this.neuSpread1_Sheet1.Cells[9, 1].Value = string.Empty;
            this.neuSpread1_Sheet1.Cells[11, 1].Value = string.Empty;
            this.neuSpread1_Sheet1.Cells[16, 1].Value = string.Empty;
            this.neuSpread1_Sheet1.Cells[19, 1].Value = string.Empty;
            this.neuSpread1_Sheet1.Cells[22, 1].Value = string.Empty;
            this.neuSpread1_Sheet1.Cells[25, 1].Value = string.Empty;
        }
        /// <summary>
        /// ��ʾ����
        /// </summary>
        public void Show()
        {
            //FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��Ŀ��鵥";
            //FS.FrameWork.WinForms.Classes.Function.PopShowControl(this);
            base.ShowDialog();
        }

        #endregion

        #region IReportPrinter ��Ա

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Export()
        {
            //throw new Exception("The method or operation is not implemented.");
            return 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int  Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            //print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.SetPageSize(new System.Drawing.Printing.PaperSize("A4", 800, 860));
            //print.PrintPreview(27, 73, this.neuPanel1);
            print.PrintPage(32, 64, this.neuPanel1);
            return 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int PrintPreview()
        {
            //throw new Exception("The method or operation is not implemented.");
            return 1;
        }

        #endregion

        #region �¼�

        /// <summary>
        /// ��ӡ��ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>      
        private void neuButton1_Click_1(object sender, EventArgs e)
        {
            this.Print();
        }     
        #endregion


    }
}
