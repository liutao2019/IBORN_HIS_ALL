using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
namespace Neusoft.HISFC.Components.RADT.Controls
{
    /// <summary>
    /// [��������: ת�����룬ȡ���ؼ�]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2006-11-30]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucPatientShiftOut : Neusoft.FrameWork.WinForms.Controls.ucBaseControl,Neusoft.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucPatientShiftOut()
        {
            InitializeComponent();

            cmbNewDept.SelectedIndexChanged += new EventHandler(cmbNewDept_SelectedIndexChanged);
        }

        #region ����
        Neusoft.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Pharmacy();
        Neusoft.HISFC.BizProcess.Integrate.Manager manager = new Neusoft.HISFC.BizProcess.Integrate.Manager();
        Neusoft.HISFC.BizLogic.RADT.InPatient inpatient = new Neusoft.HISFC.BizLogic.RADT.InPatient();
        Neusoft.HISFC.Models.RADT.PatientInfo patientInfo = null;
        private bool isCancel = false;

        /// <summary>
        /// �ӿ� ��Ժ����Ժ�ٻصȵط����ж�,�Ƿ����ִ����һ������
        /// </summary>
        Neusoft.HISFC.BizProcess.Interface.IPatientShiftValid IPatientShiftValid = null;
        
        #endregion

        #region ����
        /// <summary>
        /// �Ƿ�ȡ������
        /// </summary>
        public bool IsCancel
        {
            get
            {
                return this.isCancel;
            }
            set
            {
                this.isCancel = value;
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        private void InitControl()
        {           
            try
            {
                ArrayList al = Neusoft.HISFC.Models.Base.SexEnumService.List();
                this.cmbNewDept.AddItems(manager.QueryDeptmentsInHos(true));
                // this.cmbNewDept.Text = "";
                //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
                this.cmbNewNurse.AddItems(manager.GetDepartment(Neusoft.HISFC.Models.Base.EnumDepartmentType.N));
                IPatientShiftValid = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.IPatientShiftValid)) as Neusoft.HISFC.BizProcess.Interface.IPatientShiftValid;
            }
            catch { }

        }

        /// <summary>
        /// ���ݿ����ҵ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbNewDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            Neusoft.HISFC.BizProcess.Integrate.Manager interMgr=new Neusoft.HISFC.BizProcess.Integrate.Manager();
            ArrayList alNurse = interMgr.QueryNurseStationByDept(new Neusoft.FrameWork.Models.NeuObject(this.cmbNewDept.Tag.ToString(), "", ""));
            if (alNurse != null && alNurse.Count > 0)
            {
                this.cmbNewNurse.Tag = (alNurse[0] as Neusoft.FrameWork.Models.NeuObject).ID;
            }
            else
            {
                alNurse = interMgr.QueryDepartment(this.cmbNewDept.Tag.ToString());
                if (alNurse != null && alNurse.Count > 0)
                {
                    this.cmbNewNurse.Tag = (alNurse[0] as Neusoft.FrameWork.Models.NeuObject).ID;
                }
            }
        }


        /// <summary>
        /// ��������Ϣ��ʾ�ڿؼ���
        /// </summary>
        private void ShowPatientInfo()
        {
            this.txtPatientNo.Text = this.patientInfo.PID.PatientNO;		//סԺ��
            this.txtPatientNo.Tag = this.patientInfo.ID;							//סԺ��ˮ��
            this.txtName.Text = this.patientInfo.Name;								//��������
            this.txtSex.Text = this.patientInfo.Sex.Name;					//�Ա�
            this.txtOldDept.Text = this.patientInfo.PVisit.PatientLocation.Dept.Name;//Դ��������
            this.cmbBedNo.Text = this.patientInfo.PVisit.PatientLocation.Bed.ID.Length > 4 ? this.patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) : "";	//����
            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            this.txtOldNurse.Text = this.patientInfo.PVisit.PatientLocation.NurseCell.Name;
            //���廼��Locationʵ��
            Neusoft.HISFC.Models.RADT.Location newLocation = new Neusoft.HISFC.Models.RADT.Location();
            //ȡ����ת��������Ϣ
            newLocation = this.inpatient.QueryShiftNewLocation(this.patientInfo.ID, this.patientInfo.PVisit.PatientLocation.Dept.ID);
            this.patientInfo.User03 = newLocation.User03;
            if (this.patientInfo.User03 == null)
                this.patientInfo.User03 = "1";//����

            if (newLocation == null)
            {
                MessageBox.Show(this.inpatient.Err);
                return;
            }
            
            this.cmbNewDept.Tag = newLocation.Dept.ID;	//�¿�������
            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            this.cmbNewNurse.Tag = newLocation.NurseCell.ID;
            this.txtNote.Text = newLocation.Memo;		//��ע
            //���û��ת������,������¿��ұ���
            if (newLocation.Dept.ID == "")
            {
                this.cmbNewDept.Text = null;
            }

            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            if (newLocation.NurseCell.ID == "")
            {
                this.cmbNewNurse.Text = null;
            }

            if (this.patientInfo.User03 != null && this.patientInfo.User03 == "0")
                this.label8.Visible = true;
            else
                this.label8.Visible = false;
        }


        /// <summary>
        /// ����
        /// </summary>
        public void ClearPatintInfo()
        {
            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            this.cmbNewNurse.Tag = "";
            this.cmbNewNurse.Text = "";
            this.cmbNewDept.Text = "";
            this.cmbNewDept.Tag = "";
        }


        /// <summary>
        /// ˢ��
        /// </summary>
        /// <param name="patientInfo"></param>
        public void RefreshList(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            try
            {
                //��������Ϣ��ʾ�ڿؼ���
                this.ShowPatientInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }        

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.patientInfo = neuObject as Neusoft.HISFC.Models.RADT.PatientInfo;
            RefreshList(this.patientInfo);
            return 0;
        }

        protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.InitControl();
            return null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.cmbNewDept.Tag == null || this.cmbNewDept.Tag.ToString() == "")
            {
                MessageBox.Show("��ѡ��Ҫת��Ŀ���!");
                return;
            }

            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            if (this.cmbNewNurse.Tag == null || this.cmbNewNurse.Tag.ToString() == "")
            {
                MessageBox.Show("��ѡ��Ҫת��Ĳ���!");
                return;
            }

            Neusoft.FrameWork.Models.NeuObject dept = new Neusoft.FrameWork.Models.NeuObject();
            dept.ID = this.cmbNewDept.Tag.ToString();
            dept.Name = this.cmbNewDept.Text;
            dept.Memo = this.txtNote.Text;
            
            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            Neusoft.FrameWork.Models.NeuObject nurseCell = new Neusoft.FrameWork.Models.NeuObject();
            nurseCell.ID = this.cmbNewNurse.Tag.ToString();
            nurseCell.Name = this.cmbNewNurse.Text;

            if (!this.IsCancel)
            {
                //ת��ǰ�Ի��߸�����Ϣ�ļ��
                if (this.CheckShiftOut(this.patientInfo) == -1)
                {
                    return;
                }
            }

            //�Ƿ�ѡ���ֹͣȫ������
            bool autoDC = false;
            int rtn = this.orderIntegrate.IsOwnOrders(patientInfo.ID);
            if (rtn == -1) //����
            {
                MessageBox.Show("��ѯҽ������");
                return;
            }

            if (!this.isAutoDcOrder && this.isUseShiftAutoDcOrder && rtn == 1)
            {
                DialogResult rev = MessageBox.Show("�Ƿ�ֹͣȫ��������", "ѯ��", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (rev == DialogResult.Cancel)
                {
                    return;
                }
                else if (rev == DialogResult.Yes)
                {
                    autoDC = true;
                }
            }

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            Neusoft.HISFC.BizProcess.Integrate.RADT radt = new Neusoft.HISFC.BizProcess.Integrate.RADT();

            if (radt.ShiftOut(this.patientInfo, dept,nurseCell ,this.patientInfo.User03, this.isCancel) == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
            }

            if (this.isUseShiftAutoDcOrder)
            {
                string errInfo = "";
                if (this.isAutoDcOrder || autoDC)
                {
                    if (this.AutoDcOrder(ref errInfo) == -1)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(errInfo);
                        return;
                    }
                }
            }

            Neusoft.FrameWork.Management.PublicTrans.Commit();
            if (bSaveAndClose)
            {
                dialogResult = DialogResult.OK;
                this.FindForm().Close();
                return;
            }

            MessageBox.Show(radt.Err);
            
            base.OnRefreshTree();//ˢ����
        }

        /// <summary>
        /// �Ի��߽���ת���ж� by huangchw 2012-11-20
        /// </summary>
        /// <returns></returns>
        private int CheckShiftOut(Neusoft.HISFC.Models.RADT.PatientInfo patient)
        { 
            //��������ʾͳһ�ŵ�һ��

            //��Ҫ��ʾѡ��Ķ���
            string checkMessage = "";

            //��ʾ��ֹ�Ķ���
            string stopMessage = "";

            Classes.Function funMgr = new Neusoft.HISFC.Components.RADT.Classes.Function();
            if (IPatientShiftValid != null)
            {
                bool bl = IPatientShiftValid.IsPatientShiftValid(patient, Neusoft.HISFC.Models.Base.EnumPatientShiftValid.O, ref stopMessage);
                if (!bl)
                {
                    if (!string.IsNullOrEmpty(stopMessage))
                    {
                        MessageBox.Show(stopMessage);
                    }
                    return -1;
                }
            }


            //ע�ⲻҪ��ҵ��㵯��MessageBox������

            /*
             * һ������
             *  1�������˷����룬���������ת�ƵǼ�
             * ����ҩƷ
             *  1��������ҩ���룬���������ת�ƵǼ�
             *  2�����ڷ�ҩ���룬��ʾ�Ƿ����ת�ƵǼ�
             * �����ն�ȷ��
             *  1������δȷ����Ŀ�����������ʾ�Ƿ��������ת�ƵǼ�
             * 
             * ����������������ýӿڱ��ػ�ʵ��
             * 1���Ƿ���ȫͣ
             * 2���Ƿ�����Ժҽ��
             * 3���Ƿ���δ���ҽ��
             * 4���жϴ�λ���ͻ���ѵ���ȡ�Ƿ���ȷ
             * */


            #region 1�������˷����룬���������ת�ƵǼ�

            string ReturnApplyItemInfo = Neusoft.HISFC.Components.RADT.Classes.Function.CheckReturnApply(patient.ID);
            if (ReturnApplyItemInfo != null)
            {
                string[] item = ReturnApplyItemInfo.Split('\r');
                string tip = "";
                for (int i = 0; i < item.Length; i++)
                {
                    if (i <= 2)
                    {
                        tip += item[i] + "\r";
                        if (i == item.Length - 1 || i == 2)
                        {
                            tip += "  ��";
                        }
                    }
                }

                checkMessage += "\r\n�����δȷ�ϵ��˷����룡\r\n" + tip;
            }
            #endregion

            #region 2��������ҩ���룬��ʾ�Ƿ����

            int returnValue = this.pharmacyIntegrate.QueryNoConfirmQuitApply(patient.ID);
            if (returnValue == -1)
            {
                MessageBox.Show("��ѯ������ҩ������Ϣ����!" + this.pharmacyIntegrate.Err);

                return -1;
            }
            if (returnValue > 0) //�����뵫��û�к�׼����ҩ��Ϣ
            {
                checkMessage += "\r\n\r\n�����δ��˵���ҩ������Ϣ��";
            }

            #endregion

            #region 3���жϻ����Ǵ���δ��ҩ��ҩƷ ��ʾ�Ƿ����

            string msg = Neusoft.HISFC.Components.RADT.Classes.Function.CheckDrugApplay(patient.ID);
            if (msg != null)
            {
                string[] item = msg.Split('\r');
                string tip = "";
                for (int i = 0; i < item.Length; i++)
                {
                    if (i <= 2)
                    {
                        tip += item[i] + "\r";
                        if (i == item.Length - 1 || i == 2)
                        {
                            tip += "   ��";
                        }
                    }
                }

                checkMessage += "\r\n\r\n�����δ��ҩ��ҩƷ��Ŀ��\r\n" + tip;
                
            }
            #endregion

            #region 4������δ�ն�ȷ����Ŀ����ʾ�Ƿ����

            string UnConfirmItemInfo = Neusoft.HISFC.Components.RADT.Classes.Function.CheckUnConfirm(patient.ID);
            if (UnConfirmItemInfo != null)
            {
                string[] item = UnConfirmItemInfo.Split('\r');
                string tip = "";
                for (int i = 0; i < item.Length; i++)
                {
                    if (i <= 2)
                    {
                        tip += item[i] + "\r";
                        if (i == item.Length - 1 || i == 2)
                        {
                            tip += "   ��";
                        }
                    }
                }

                checkMessage += "\r\n\r\n�����δȷ���շѵ��ն���Ŀ��\r\n" + tip;
            }

            #endregion

            #region 5���ж��Ƿ���ת��ҽ��

            Neusoft.HISFC.Models.Order.Inpatient.Order inOrder = null;

            int rev = this.orderIntegrate.GetShiftOutOrderType(patientInfo.ID, ref inOrder);
            int rtn = this.orderIntegrate.IsOwnOrders(patientInfo.ID);

            if (rev < 0 || rtn == -1) //����ת�Ƴ�����ȫ��ҽ���ٴ���return
            {
                MessageBox.Show("��ѯת��ҽ������!\r\n" + orderIntegrate.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return -1;
            }
            else if (rev == 0 && rtn == 1)//�ѿ�����ҽ����û��ת��ҽ��
            {
                stopMessage += "\r\n\r\n��δ����ת��ҽ����";
            }

            #endregion

            #region 6���жϳ����Ƿ�ȫͣ

            if (!funMgr.CheckIsAllLongOrderStop(patient.ID))
            {
                stopMessage += "\r\n\r\n��" + funMgr.Err;
            }

            #endregion

            #region 7���ж��Ƿ���δ���ҽ��

            if (!funMgr.CheckIsAllOrderConfirm(patient.ID))
            {
                stopMessage += "\r\n\r\n��" + funMgr.Err;
            }

            #endregion

            #region 8���ж��Ƿ���δ�շѵķ�ҩƷҽ��ִ�е�

            string returnStr = Neusoft.HISFC.Components.RADT.Classes.Function.CheckExecOrderCharge(patient.ID);
            if (returnStr != null)
            {
                string[] strArray = returnStr.Split(new char[3] { '|', '|', '|' });

                if (Convert.ToInt32(strArray[3]) > 0)
                {
                    checkMessage += "\r\n\r\n�����δ�շ���Ŀ��\r\n" + strArray[0];
                }
            }

            #endregion

            #region 9����Ժ���������Ϊ��

            //if (this.cmbZg.Tag == null || string.IsNullOrEmpty(cmbZg.Tag.ToString()))
            //{
            //    stopMessage += "\r\n\r\n������Ҫ�󣬳�Ժ���������Ϊ�գ�\r\n";
            //}
            #endregion

            #region 10������δ�շ��������뵥���������ת��

            //            if (isCanOutWhenUnFeeUOApply != CheckState.Yes)
            //            {
            //                string sql = @"select count(*) from met_ops_apply f
            //                                                        where f.clinic_code='{0}'
            //                                                        and f.ynvalid='1'
            //                                                            and f.execstatus!='4'
            //                                                            and f.execstatus!='5'";

            //                string rev = judgeMgr.ExecSqlReturnOne(string.Format(sql, patient.ID));
            //                try
            //                {
            //                    if (Neusoft.FrameWork.Function.NConvert.ToInt32(rev) > 0)
            //                    {
            //                        if (isCanOutWhenUnFeeUOApply == CheckState.Check)
            //                        {
            //                            checkMessage += "\r\n\r\n����δ��ɵ��������뵥��";
            //                        }
            //                        else if (isCanOutWhenUnFeeUOApply == CheckState.No)
            //                        {
            //                            stopMessage += "\r\n\r\n����δ��ɵ��������뵥��";
            //                        }
            //                    }
            //                }
            //                catch
            //                {
            //                }
            //            }

            #endregion

            #region Ƿ���ж�

            //if (isCanOutWhenLackFee != CheckState.Yes)
            //{
            //    try
            //    {
            //        if (patient.PVisit.MoneyAlert != 0 && patient.FT.LeftCost < this.patient.PVisit.MoneyAlert)
            //        {
            //            if (isCanOutWhenUnFeeUOApply == CheckState.Check)
            //            {
            //                checkMessage += "\r\n\r\n�Ѿ�Ƿ�ѣ�\r\n�� " + patient.FT.LeftCost.ToString() + "\r\n�����ߣ� " + patient.PVisit.MoneyAlert.ToString();
            //            }
            //            else if (isCanOutWhenUnFeeUOApply == CheckState.No)
            //            {
            //                stopMessage += "\r\n\r\n�Ѿ�Ƿ�ѣ�\r\n�� " + patient.FT.LeftCost.ToString() + "\r\n�����ߣ� " + patient.PVisit.MoneyAlert.ToString();
            //            }
            //        }
            //    }
            //    catch
            //    {
            //    }
            //}
            #endregion

            if (!string.IsNullOrEmpty(checkMessage))
            {
                if (MessageBox.Show(patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "�� ���ߡ�" + patient.Name + "��\r\n������������δ����,�Ƿ��������ת�ƣ�\r\n\r\n" + checkMessage, "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return -1;
                }
            }

            if (!string.IsNullOrEmpty(stopMessage))
            {
                MessageBox.Show(patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "�� ���ߡ�" + patient.Name + "��\r\n������������δ����,���ܼ�������ת�ƣ�\r\n\r\n" + stopMessage, "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return -1;
            }
            return 1;
        }

        private Neusoft.HISFC.BizProcess.Integrate.Order orderIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Order();

        /// <summary>
        /// ��Ժ�Զ�ֹͣ������ֹͣҽ��
        /// </summary>
        private AotuDcDoct autoDcDoct = AotuDcDoct.ExecutOutDoct;

        /// <summary>
        /// ��Ժ�Զ�ֹͣ������ֹͣҽ��
        /// </summary>
        [Category("�ؼ�����"), Description("��Ժ�Զ�ֹͣ������ֹͣҽ��")]
        public AotuDcDoct AutoDcDoct
        {
            get
            {
                return autoDcDoct;
            }
            set
            {
                autoDcDoct = value;
            }
        }

        /// <summary>
        /// �Ƿ�ʹ��ת���Զ�ֹͣҽ������
        /// </summary>
        private bool isUseShiftAutoDcOrder = false;

        /// <summary>
        /// �Ƿ�ʹ��ת���Զ�ֹͣҽ������
        /// </summary>
        [Category("��Ժ����"), Description("�Ƿ�ʹ��ת���Զ�ֹͣҽ�����ܣ�ʹ�ú���뿪��ת��ҽ������ת�ƣ�")]
        public bool IsUseShiftAutoDcOrder
        {
            get
            {
                return isUseShiftAutoDcOrder;
            }
            set
            {
                isUseShiftAutoDcOrder = value;
            }
        }

        /// <summary>
        /// �Ƿ�ת���Զ�ֹͣҽ��
        /// </summary>
        private bool isAutoDcOrder = false;

        /// <summary>
        /// �Ƿ�ת���Զ�ֹͣҽ��
        /// </summary>
        public bool IsAutoDcOrder
        {
            get
            {
                return isAutoDcOrder;
            }
            set
            {
                isAutoDcOrder = value;
            }
        }

        /// <summary>
        /// ��Ժ�Զ�ֹͣȫ������
        /// </summary>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        private int AutoDcOrder(ref string errInfo)
        {
            //������Ժҽ����ҽ��
            if (autoDcDoct == AotuDcDoct.ExecutOutDoct)
            {
                Neusoft.HISFC.Models.Order.Inpatient.Order orderObj = null;
                int rev = this.orderIntegrate.GetShiftOutOrderType(patientInfo.ID, ref orderObj);
                if (rev == -1)
                {
                    errInfo = orderIntegrate.Err;
                    return -1;
                }
                else if (rev == 0)
                {
                    errInfo = "���ߡ�" + patientInfo.Name + "����δ����ת��ҽ����";
                    return -1;
                }

                if (orderObj == null || orderObj.ReciptDoctor == null || string.IsNullOrEmpty(orderObj.ReciptDoctor.ID))
                {
                    errInfo = "���ߡ�" + patientInfo.Name + "����δ����ת��ҽ����";
                    return -1;
                }

                if (this.orderIntegrate.AutoDcOrder(patientInfo.ID, orderObj.ReciptDoctor, this.inpatient.Operator, "", "ת���Զ�ֹͣ") == -1)
                {
                    errInfo = this.orderIntegrate.Err;
                    return -1;
                }
            }
            //����ҽ��
            else if (autoDcDoct == AotuDcDoct.ExecutOutDoct)
            {
                if (this.patientInfo.PVisit.AttendingDirector == null ||
                    string.IsNullOrEmpty(patientInfo.PVisit.AttendingDirector.ID))
                {
                    errInfo = "���ߡ�" + patientInfo.Name + "��û��ά������ҽʦ�������Զ�ֹͣҽ����";
                    return -1;
                }

                if (this.orderIntegrate.AutoDcOrder(patientInfo.ID, patientInfo.PVisit.AttendingDirector, this.inpatient.Operator, "", "ת���Զ�ֹͣ") == -1)
                {
                    errInfo = this.orderIntegrate.Err;
                    return -1;
                }
            }
            //�ܴ�ҽ��
            else if (autoDcDoct == AotuDcDoct.ExecutOutDoct)
            {
                if (this.orderIntegrate.AutoDcOrder(patientInfo.ID, patientInfo.PVisit.AdmittingDoctor, this.inpatient.Operator, "", "ת���Զ�ֹͣ") == -1)
                {
                    errInfo = this.orderIntegrate.Err;
                    return -1;
                }
            }

            return 1;
        }

        #endregion

        #region ITransferDeptApplyable ��Ա
        bool bSaveAndClose = false;
        DialogResult dialogResult = DialogResult.None;

        public Neusoft.FrameWork.Models.NeuObject Dept
        {
            get
            {
                return this.cmbNewDept.SelectedItem;
            }
        }

        public void SetPatientInfo(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.InitControl();
            this.patientInfo = patientInfo.Clone();
            RefreshList(this.patientInfo);
           
        }

        public DialogResult ShowDialog()
        {
            bSaveAndClose = true;
            Neusoft.FrameWork.WinForms.Classes.Function.PopShowControl(this);
            return dialogResult;
            
        }
        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get 
            {
                return new Type[] { typeof(Neusoft.HISFC.BizProcess.Interface.ITransferDeptApplyable), 
                                    typeof(Neusoft.HISFC.BizProcess.Interface.IPatientShiftValid) };
            }
        }

        #endregion

        private void cbxModefyNurse_CheckedChanged(object sender, EventArgs e)
        {
            cmbNewNurse.Enabled = cbxModefyNurse.Checked;
        }
    }
}
