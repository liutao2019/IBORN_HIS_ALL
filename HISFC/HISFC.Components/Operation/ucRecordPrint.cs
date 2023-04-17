using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Neusoft.HISFC.Object.Operation;
using Neusoft.NFC.Object;

namespace UFC.Operation
{
    /// <summary>
    /// [��������: �����ǼǴ�ӡ�ؼ�]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2007-01-08]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucRecordPrint : UserControl ,Neusoft.HISFC.Integrate.Operation.IRecordFormPrint
    {
        public ucRecordPrint()
        {
            InitializeComponent();
        }

#region �ֶ�
        Neusoft.NFC.Interface.Classes.Print print = new Neusoft.NFC.Interface.Classes.Print();
#endregion

#region IRecordFormPrint ��Ա

        public Neusoft.HISFC.Object.Operation.OperationRecord OperationRecord
        {
            set 
            {
                OperationRecord operationRecord = value;
                if (operationRecord == null) 
                    return;
                OperationAppllication thisOpsApp = operationRecord.OperationAppllication;
                
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

                this.lbOpsDate.Text = operationRecord.OpsDate.ToString();					//��������
                this.lbOpsRoom.Text = thisOpsApp.OperateRoom.Name;						//������
                this.lbInPatientNo.Text = thisOpsApp.PatientInfo.PID.ID.ToString();	//סԺ��
                this.lbName.Text = thisOpsApp.PatientInfo.Name;					//����
                this.lbSex.Text = thisOpsApp.PatientInfo.Sex.Name;				//�Ա�
                //����				
                int year = System.DateTime.Today.Year;//��ǰ��
                int birthYear = thisOpsApp.PatientInfo.Birthday.Year;//������
                int age = year - birthYear;
                this.lbAge.Text = age.ToString();									//����
                //����
                if (thisOpsApp.PatientInfo.PVisit.PatientLocation.Dept.ID != "")
                {
                    Neusoft.HISFC.Object.Base.Department dept = null;
                    Neusoft.HISFC.Management.Manager.Department deptMgr = new Neusoft.HISFC.Management.Manager.Department();
                    dept = deptMgr.GetDeptmentById(thisOpsApp.PatientInfo.PVisit.PatientLocation.Dept.ID);
                    if (dept != null) 
                        this.lbDept.Text = dept.Name;
                }

                // TODO: �����ǰ���
                //��ǰ���
                //string strDiagnoses = "";
                //foreach (neusoft.HISFC.Object.Case.DiagnoseBase myDiagnose in thisOpsApp.DiagnoseAl)
                //{
                //    //��ϸ������Ϊһ���ַ���
                //    if (strDiagnoses != "")
                //        strDiagnoses = strDiagnoses + " / ";
                //    strDiagnoses = strDiagnoses + myDiagnose.Name;
                //}
                //this.lbDiagnose.Text = strDiagnoses;									//��ǰ���
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
                this.lbAnaeType.Text = thisOpsApp.AnesType.Name;						//��������
                this.lbOpsDoct.Text = thisOpsApp.OperationDoctor.Name;							//����ҽʦ
                Neusoft.NFC.Object.NeuObject obj = new Neusoft.NFC.Object.NeuObject();
                for (int i = 0; i < thisOpsApp.HelperAl.Count; i++)
                {
                    obj = (Neusoft.NFC.Object.NeuObject)(thisOpsApp.HelperAl[i]);
                    switch (i)
                    {
                        case 0:
                            this.lbHelp1.Text = obj.Name;											//һ����
                            break;
                        case 1:
                            this.lbHelp2.Text = obj.Name;											//������
                            break;
                        case 2:
                            this.lbHelp3.Text = obj.Name;											//������
                            break;
                    }
                }

                this.lbRemark.Text = operationRecord.Memo;								//����˵��
                this.lbApplyDoct.Text = thisOpsApp.ApplyDoctor.Name;						//����ҽʦ
                this.lbBedNo.Text = thisOpsApp.PatientInfo.PVisit.PatientLocation.Bed.ID.ToString();//����

                string strAccoNurs = "";//��̨��ʿ
                string strPrepNurs = "";//Ѳ�ػ�ʿ
                foreach (ArrangeRole thisRole in thisOpsApp.RoleAl)
                {
                    if (thisRole.RoleType.ID.ToString() == EnumOperationRole.WashingHandNurse.ToString())//��̨��ʿ
                    {
                        if (strAccoNurs != "")
                            strAccoNurs = strAccoNurs + "/";
                        strAccoNurs = strAccoNurs + thisRole.Name;
                    }
                    if (thisRole.RoleType.ID.ToString() == EnumOperationRole.ItinerantNurse.ToString())//Ѳ�ػ�ʿ
                    {
                        if (strPrepNurs != "")
                            strPrepNurs = strPrepNurs + "/";
                        strPrepNurs = strPrepNurs + thisRole.Name;
                    }
                }
                this.lbAcco.Text = strAccoNurs;
                this.lbPrep.Text = strPrepNurs;
            }
        }

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
    }
}
