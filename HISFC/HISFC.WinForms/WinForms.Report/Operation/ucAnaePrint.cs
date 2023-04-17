using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Operation;
using FS.FrameWork.Models;

namespace FS.WinForms.Report.Operation
{
    public partial class ucAnaePrint : UserControl, FS.HISFC.BizProcess.Interface.Operation.IAnaeFormPrint
    {
        public ucAnaePrint()
        {
            InitializeComponent();
        }

        #region �ֶ�
        FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
        #endregion

      

        #region IReportPrinter ��Ա
                
        public int Print()
        {
            return this.print.PrintPreview(this);
        }

        public int PrintPreview()
        {
            return this.print.PrintPreview(this);
        }

        public int Export()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion


        #region IAnaeFormPrint ��Ա

        AnaeRecord FS.HISFC.BizProcess.Interface.Operation.IAnaeFormPrint.AnaeRecord
        {
            set
            {
                AnaeRecord anaeRecord = value;
                if (anaeRecord == null)
                {
                    return;
                }
                OperationAppllication thisOpsApp = anaeRecord.OperationApplication;          

                //�������
                NeuObject kind = Environment.GetPayKind(thisOpsApp.PatientInfo.Pact.PayKind.ID);
                if (kind == null)
                    this.lbPatientType.Text = thisOpsApp.PatientInfo.Pact.PayKind.ID;
                else
                    this.lbPatientType.Text = kind.Name;

                switch (thisOpsApp.TableType)
                {
                    case "1":
                        this.lbConsoleType.Text = "��̨";
                        break;
                    case "2":
                        this.lbConsoleType.Text = "��̨";
                        break;
                    case "3":
                        this.lbConsoleType.Text = "��̨";
                        break;
                }
                //�������
                switch (thisOpsApp.OperateKind)
                {
                    case "1":
                        this.lbOpsKind.Text = "����";
                        break;
                    case "2":
                        this.lbOpsKind.Text = "����";
                        break;
                    case "3":
                        this.lbOpsKind.Text = "��Ⱦ";
                        break;
                }
                
                //this.lbOpsDate.Text = anaeRecord.OpsDate.ToString();					//��������
                this.lbOpsRoom.Text = thisOpsApp.OperateRoom.Name;						//������
                this.lbInPatientNo.Text = thisOpsApp.PatientInfo.PID.ID.ToString();	//סԺ��
                this.lbName.Text = thisOpsApp.PatientInfo.Name;					//����
                this.lbSex.Text = thisOpsApp.PatientInfo.Sex.Name;				//�Ա�
                //����				
                int year = System.DateTime.Today.Year;//��ǰ��
                int birthYear = thisOpsApp.PatientInfo.Birthday.Year;//������
                int age = year - birthYear;
                this.lbAge.Text = age.ToString();

               

                //������Ŀ
                string strItemName = "";
                foreach (OperationInfo myOpsInfo in thisOpsApp.OperationInfos)
                {
                    if (myOpsInfo.IsMainFlag)
                    {
                        //�ҵ���������ֻ��ʾ������
                        strItemName = myOpsInfo.OperationItem.Name;
                        break;
                    }
                    //������ϸ�������Ϊһ���ַ���
                    if (strItemName != "")
                        strItemName = strItemName + " / ";
                    strItemName = strItemName + myOpsInfo.OperationItem.Name;
                }

                this.lbItemName.Text = strItemName;										//������Ŀ���������ƣ�
                this.lbAnaeType.Text = thisOpsApp.AnesType.Name;                        //����ʽ
               // this.lbAnaerName.Text = thisOpsApp                                     //������
                this.lbAnaeTime.Text = anaeRecord.AnaeDate.ToString();                  //����ʱ��
                if (anaeRecord.IsPACU == true)                                          //��/��PACU
                {
                    this.lbPACU.Text = "��";
                }
                else
                {
                    this.lbPACU.Text = "��";
                }
                this.lbInTime.Text = anaeRecord.InPacuDate.ToString();                  //����ʱ��
                this.lbInState.Text = anaeRecord.InPacuStatus.ToString();
                this.lbOutTime.Text = anaeRecord.OutPacuDate.ToString();
                this.lbOutState.Text = anaeRecord.OutPacuStatus.ToString();

                if (anaeRecord.IsDemulcent == true)                                          //��/�� ������ʹ
                {
                    this.lbIsDemulcent.Text = "��";
                }
                else
                {
                    this.lbIsDemulcent.Text = "��";
                }

                this.lbDemuKind.Text = anaeRecord.DemulcentType.Name.ToString();
                this.lbDemuModel.Text = anaeRecord.DemulcentModel.Name.ToString();
                this.lbDemuDays.Text = anaeRecord.DemulcentDays.ToString();
                this.lbPullOutDate.Text = anaeRecord.PullOutDate.ToString();
                this.lbPullOutOpcd.Text = anaeRecord.PullOutOperator.Name;
                this.lbDemuResult.Text = anaeRecord.DemulcentEffect.Name;

                this.lbAnaerName.Text = "";
                for (int i = 0; i < anaeRecord.OperationApplication.RoleAl.Count; i++)
                {              
                    this.lbAnaerName.Text += anaeRecord.OperationApplication.RoleAl[i].ToString() + " ";                    
                }   
            }            
        }

        #endregion
        }
}
