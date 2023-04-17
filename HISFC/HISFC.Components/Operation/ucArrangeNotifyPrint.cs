using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Neusoft.NFC.Object;
using Neusoft.HISFC.Object.Operation;

namespace UFC.Operation
{
    /// <summary>
    /// [��������: ��������֪ͨ����ӡ�ؼ�]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2007-01-04]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucArrangeNotifyPrint : UserControl, Neusoft.HISFC.Integrate.Operation.IArrangeNotifyFormPrint
    {
        /// <summary>
        /// [��������: �������Ŵ�ӡ]<br></br>
        /// [�� �� ��: ����ȫ]<br></br>
        /// [����ʱ��: 2007-01-04]<br></br>
        /// <�޸ļ�¼
        ///		�޸���=''
        ///		�޸�ʱ��='yyyy-mm-dd'
        ///		�޸�Ŀ��=''
        ///		�޸�����=''
        ///  />
        /// </summary>
        public ucArrangeNotifyPrint()
        {
            InitializeComponent();
            if(!Environment.DesignMode)
            {
                this.Init();
            }
        }

#region �ֶ�

        Neusoft.HISFC.Management.Manager.Constant constManager = new Neusoft.HISFC.Management.Manager.Constant();
        Neusoft.NFC.Interface.Classes.Print print = new Neusoft.NFC.Interface.Classes.Print();

#endregion

#region ����

#endregion

#region ����

        private void Init()
        {
            print.ControlBorder = Neusoft.NFC.Interface.Classes.enuControlBorder.None;
        }


#endregion

        #region IApplicationFormPrint ��Ա

        /// <summary>
        /// �������뵥����
        /// </summary>
        public Neusoft.HISFC.Object.Operation.OperationAppllication OperationApplicationForm
        {
            set 
            {
                Neusoft.HISFC.Object.Operation.OperationAppllication thisOpsApp = value;
                if (thisOpsApp == null) return;
                
                NeuObject kind = this.constManager.GetConstant(Neusoft.HISFC.Object.Base.EnumConstant.PAYKIND, thisOpsApp.PatientInfo.Pact.PayKind.ID);
                if (kind == null)
                    this.lbPatientType.Text = thisOpsApp.PatientInfo.Pact.PayKind.ID;
                else
                    this.lbPatientType.Text = kind.Name;

                //��ʾ��������

                this.lbOpSpecial.Text = "��������:" + thisOpsApp.SpecialItem;

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

                this.lbOrder.Text = thisOpsApp.BloodUnit;									//̨��
                this.lbOpsDate.Text = thisOpsApp.PreDate.ToString();					//��������
                this.lbOpsRoom.Text = thisOpsApp.OperateRoom.Name;						//������
                this.lbInPatientNo.Text = thisOpsApp.PatientInfo.PID.PatientNO;	//סԺ��
                this.PatientNO.Text = thisOpsApp.PatientInfo.PID.PatientNO;
                this.lbName.Text = thisOpsApp.PatientInfo.Name;					//����
                this.PatientName.Text = thisOpsApp.PatientInfo.Name;
                this.lbSex.Text = thisOpsApp.PatientInfo.Sex.Name;				//�Ա�
                SexType.Text = thisOpsApp.PatientInfo.Sex.Name;
                //����				
                int li_thisYear = this.constManager.GetDateTimeFromSysDateTime().Year;//��ǰ��
                int li_BirYear = thisOpsApp.PatientInfo.Birthday.Year;//������
                int li_age = li_thisYear - li_BirYear;
                if (li_age == 0) li_age = 1;
                this.lbAge.Text = li_age.ToString();									//����
                Age.Text = this.lbAge.Text;
                this.deptName.Text = thisOpsApp.PatientInfo.PVisit.PatientLocation.Dept.Name;
                string BedNO = "";
                if (thisOpsApp.PatientInfo.PVisit.PatientLocation.Bed.ID.Length >= 7)
                {
                    BedNO = thisOpsApp.PatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4);
                }
                this.lbDept.Text = thisOpsApp.PatientInfo.PVisit.PatientLocation.Dept.Name + BedNO + "��";//����
                
                // TODO: ���δʵ��
                #region ���
                //��ǰ���
                //				string strDiagnoses = "";
                //int m = 0;
                //foreach (neusoft.HISFC.Object.Case.DiagnoseBase myDiagnose in thisOpsApp.DiagnoseAl)
                //{
                //    if (m == 0)
                //    {
                //        this.lbDiagnose.Text = myDiagnose.Name;
                //    }
                //    else if (m == 1)
                //    {
                //        this.lbDiagnose2.Text = myDiagnose.Name;
                //    }
                //    else if (m == 2)
                //    {
                //        this.lbDiagnose3.Text = myDiagnose.Name;
                //    }
                //    m++;
                //    //					//��ϸ������Ϊһ���ַ���
                //    //					if (strDiagnoses != "")
                //    //						strDiagnoses = strDiagnoses + " / ";
                //    //					strDiagnoses = strDiagnoses + myDiagnose.Name;
                //    //						this.lbDiagnose.Text = strDiagnoses;	
                //}
                ////��ǰ���
                ////������Ŀ
                ////				string strItemName = "";
                //int j = 0;
                //foreach (neusoft.HISFC.Object.Operator.OperateInfo myOpsInfo in thisOpsApp.OperateInfoAl)
                //{

                //    if (j == 0)
                //    {
                //        this.lbItemName.Text = myOpsInfo.OperateItem.Name;
                //        lbOperationName.Text = myOpsInfo.OperateItem.Name;
                //    }
                //    else if (j == 1)
                //    {
                //        this.lbItemName2.Text = myOpsInfo.OperateItem.Name;
                //    }
                //    else if (j == 2)
                //    {
                //        this.lbItemName3.Text = myOpsInfo.OperateItem.Name;
                //    }
                //    j++;
                //    //					if(myOpsInfo.bMainFlag)
                //    //					{
                //    //						//�ҵ���������ֻ��ʾ������
                //    //						strItemName = myOpsInfo.OperateItem.Name;
                //    //						break;
                //    //					}
                //    //					//������ϸ�������Ϊһ���ַ���
                //    //					if(strItemName != "")
                //    //						strItemName = strItemName + " / ";
                //    //					strItemName = strItemName + myOpsInfo.OperateItem.Name;
                //}
                #endregion

                //������Ŀ���������ƣ�

                NeuObject obj = new NeuObject();
                if (thisOpsApp.AnesType.ID != null && thisOpsApp.AnesType.ID != "")
                {
                    obj = this.constManager.GetConstant(Neusoft.HISFC.Object.Base.EnumConstant.ANESTYPE,
                        thisOpsApp.AnesType.ID);
                    if (obj != null)
                    {
                        this.lbAnaeType.Text = obj.Name;
                    }
                }

                this.lbOpsDoct.Text = thisOpsApp.OperationDoctor.Name;							//����ҽʦ

                for (int i = 0; i < thisOpsApp.HelperAl.Count; i++)
                {
                    obj = (NeuObject)(thisOpsApp.HelperAl[i]);
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
                this.lbOpsNote.Text = thisOpsApp.OpsNote;								//����ע������
                this.lbApplyDoct.Text = thisOpsApp.ApplyDoctor.Name;						//����ҽʦ

                if (thisOpsApp.RoleAl != null)
                {
                    foreach (Neusoft.HISFC.Object.Operation.ArrangeRole role in thisOpsApp.RoleAl)
                    {
                        if (role.RoleType.ID.ToString() == EnumOperationRole.WashingHandNurse.ToString())
                            //ϴ�ֻ�ʿ
                        {
                            if (lbIfQX2.Text == "")
                            {
                                this.lbIfQX2.Text = role.Name;
                            }
                            else
                            {
                                if (lbIfQX3.Text == "")
                                {
                                    lbIfQX3.Text = role.Name;
                                }
                                else
                                {
                                    lbIfQX.Text = role.Name;
                                }
                            }

                        }
                        else if (role.RoleType.ID.ToString() == EnumOperationRole.ItinerantNurse.ToString())
                            //Ѳ�ػ�ʿ
                        {
                            if (lbIfXH2.Text == "")
                            {
                                this.lbIfXH2.Text = role.Name;
                            }
                            else
                            {
                                if (this.lbIfXH3.Text == "")
                                {
                                    this.lbIfXH3.Text = role.Name;
                                }
                                else
                                {
                                    this.lbIfXH.Text = role.Name;
                                }
                            }
                        }
                    }
                }
                lbOpsNote.Text = thisOpsApp.ApplyNote;
                this.lbBedNo.Text = thisOpsApp.OpsTable.Name;//����
                //				neusoft.HISFC.Management.Operator.OpsTableManage roomMgr=new neusoft.HISFC.Management.Operator.OpsTableManage();
                //				if(thisOpsApp.RoomID!=null&&thisOpsApp.RoomID!="")
                //				{
                //					neusoft.HISFC.Object.Operator.OpsRoom room=roomMgr.GetRoomByID(thisOpsApp.RoomID);
                //					if(room!=null)
                //					{
                //						this.lbBedNo.Text=room.Name + thisOpsApp.OpsTable.Name;//����
                //						
                //					}
                //				}				
            }
        }


        #endregion

        #region IReportPrinter ��Ա

        public int Export()
        {
            return 0;
        }

        public int Print()
        {
            //this.print.PrintPage(0,0,this);
            return 0;
        }

        public int PrintPreview()
        {
            this.print.PrintPreview(this);
            return 0;
        }

        #endregion

        #region IArrangeFormPrint ��Ա

        /// <summary>
        /// �Ƿ��ӡ������̨�����
        /// </summary>
        public bool IsPrintExtendTable
        {
            set
            {
                label16.Visible = value;
                label15.Visible = value;
                label14.Visible = value;
                label3.Visible = value;
                label4.Visible = value;
                label5.Visible = value;
                label6.Visible = value;
                label7.Visible = value;
                label8.Visible = value;
                label9.Visible = value;
                label10.Visible = value;
                label11.Visible = value;
                label12.Visible = value;
                label13.Visible = value;
                PatientName.Visible = value;
                SexType.Visible = value;
                Age.Visible = value;
                deptName.Visible = value;
                PatientNO.Visible = value;
                lbOperationName.Visible = value;
            }
        }

        #endregion
    }
}
