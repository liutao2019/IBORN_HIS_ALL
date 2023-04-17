using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.RADT.Controls
{
    /// <summary>
    /// {9A2D53D3-25BE-4630-A547-A121C71FB1C5}
    /// [��������: ת�������룬ȡ���ؼ�]<br></br>
    /// [�� �� ��: Sunm]<br></br>
    /// [����ʱ��: 2009-07-09]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    /// </summary>
    public partial class ucPatientShiftNurseCell : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        /// <summary>
        /// ����
        /// </summary>
        public ucPatientShiftNurseCell()
        {
            InitializeComponent();
            cmbNewDept.SelectedIndexChanged += new EventHandler(cmbNewDept_SelectedIndexChanged);
        }

        #region ����

        /// <summary>
        /// ���Ҳ���ҵ����
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ����ҵ����
        /// </summary>
        FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// ������Ϣ
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        FS.HISFC.BizProcess.Integrate.Common.ControlParam ctlParamManage = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        //��Ҫ��ʾѡ��Ķ���
        string checkMessage = "";

        //��ʾ��ֹ�Ķ���
        string stopMessage = "";
        #region addby xuewj IADT�ӿ�

        FS.HISFC.BizProcess.Interface.IHE.IADT adt = null;

        #endregion


        #endregion

        #region ����

        private bool isCancel = false;
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


        private CheckState isCanWhenUnConfirmOrder = CheckState.Check;
        /// <summary>
        /// ����δ��˵�ҽ���Ƿ��������ת����
        /// </summary>
        public CheckState IsCanWhenUnConfirmOrder
        {
            get
            {
                return this.isCanWhenUnConfirmOrder;
            }
            set
            {
                this.isCanWhenUnConfirmOrder = value;
            }
        }
        #endregion

        /// <summary>
        /// �Ƿ���ʾ
        /// </summary>
        private bool isShowShiftNurse = false;
        public bool IsShowShiftNurse
        {
            get
            {
                return this.isShowShiftNurse;
            }
            set
            {
                this.isShowShiftNurse = value;
            }
        }
        #region ����

        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        private void InitControl()
        {

            try
            {
                //{4C8D1E46-9C15-47dc-89B2-2CE58B934E17}
                //ArrayList al = new ArrayList();
                //al = manager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.N);
                //this.cmbNewDept.AddItems(al);
            }
            catch { }

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
            this.txtOldDept.Text = this.patientInfo.PVisit.PatientLocation.NurseCell.Name;//Դ��������
            this.cmbBedNo.Text = this.patientInfo.PVisit.PatientLocation.Bed.ID.Length > 4 ? this.patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) : "";	//����
            //���廼��Locationʵ��
            FS.HISFC.Models.RADT.Location newLocation = new FS.HISFC.Models.RADT.Location();
            //ȡ����ת��������Ϣ
            newLocation = this.inpatientManager.QueryShiftNewLocation(this.patientInfo.ID, this.patientInfo.PVisit.PatientLocation.Dept.ID);
            this.patientInfo.User03 = newLocation.User03;
            if (this.patientInfo.User03 == null)
                this.patientInfo.User03 = "1";//����

            if (this.isCanWhenUnConfirmOrder != CheckState.Yes)
            {
                string msg = FS.HISFC.Components.RADT.Classes.Function.CheckAllOrderUnConfirm(patientInfo.ID);
                if (!string.IsNullOrEmpty(msg))
                {
                    if (isCanWhenUnConfirmOrder == CheckState.Check)// {06A6BD92-97A6-4f01-8B6E-533B35CBF2DD}
                    {
                        checkMessage += "\r\n\r\n�����δ���ҽ��\r\n" + msg;
                    }
                    else if (isCanWhenUnConfirmOrder == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n�����δ���ҽ��\r\n" + msg;
                    }
                }

            }
            this.label1.Text = stopMessage;
            if (newLocation == null)
            {
                MessageBox.Show(this.inpatientManager.Err);
                return;
            }

            this.cmbNewDept.Tag = newLocation.NurseCell.ID;	//�¿�������
            this.txtNote.Text = newLocation.Memo;		//��ע
            //���û��ת������,������¿��ұ���
            if (newLocation.NurseCell.ID == "")
            {
                this.cmbNewDept.Text = null;
            }
            if (this.patientInfo.User03 != null && this.patientInfo.User03 == "0")
                this.label8.Visible = true;
            else
                this.label8.Visible = false;

            //{62BB0773-5C87-4458-9EA9-A8196C5D3EF4}
            ArrayList alNurse = this.QueryNurseByDept(this.patientInfo.PVisit.PatientLocation.Dept.ID);
            this.cmbNewDept.Items.Clear();
            this.cmbNewDept.AddItems(alNurse);
        }

        /// <summary>
        /// {62BB0773-5C87-4458-9EA9-A8196C5D3EF4}
        /// ���ݿ����Ҷ�Ӧ�Ĳ���
        /// </summary>
        /// <param name="deptStatCode"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        private ArrayList QueryNurseByDept(string deptCode)
        {
            FS.HISFC.BizLogic.Manager.DepartmentStatManager deptStatMgr = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();

            ArrayList alNurse = new ArrayList();
            ArrayList al = deptStatMgr.LoadByParent("01", deptCode);
            if (al == null || al.Count == 0)
            {
                al = deptStatMgr.LoadByChildren("01", deptCode);
                if (al != null)
                {
                    foreach (FS.HISFC.Models.Base.DepartmentStat deptStat in al)
                    {
                        alNurse.Add(new FS.FrameWork.Models.NeuObject(deptStat.PardepCode, deptStat.PardepName, ""));
                    }
                }
            }
            else
            {
                foreach (FS.HISFC.Models.Base.DepartmentStat deptStat in al)
                {
                    alNurse.Add(new FS.FrameWork.Models.NeuObject(deptStat.DeptCode, deptStat.DeptName, ""));
                }
            }
            return alNurse;
        }

        /// <summary>
        /// ����
        /// </summary>
        public void ClearPatintInfo()
        {
            this.cmbNewDept.Text = "";
            this.cmbNewDept.Tag = "";
        }

        /// <summary>
        /// ˢ��
        /// </summary>
        /// <param name="patientInfo"></param>
        public void RefreshList(FS.HISFC.Models.RADT.PatientInfo patientInfo)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="neuObject"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.patientInfo = neuObject as FS.HISFC.Models.RADT.PatientInfo;
            RefreshList(this.patientInfo);
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            //{4C8D1E46-9C15-47dc-89B2-2CE58B934E17}
            //this.InitControl();
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.cmbNewDept.Tag == null || this.cmbNewDept.Tag.ToString() == "")
            {
                MessageBox.Show("��ѡ��Ҫת��Ĳ���!");
                return;
            }
            //{3D9A4F05-98F9-450b-B0EE-CCEBCED15C6F}
            if (this.patientInfo.IsBaby)
            {
                DialogResult result = MessageBox.Show("�Ƿ������ת����ʱ��ֹ����ת�Ʊ���������Ҫת�ƣ���ѡ��ĸ��ת��!","��˶ԣ�",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {

                }
                else
                {
                    return;
                }
            }
            // {7FFE7A7E-239D-4019-97B4-D3F80BB79713}
            if (!this.patientInfo.IsBaby && this.patientInfo.IsHasBaby)
            {
                ArrayList alBabys = this.inpatientManager.QueryBabiesByMother(this.patientInfo.ID);
                if (alBabys.Count > 0)
                {
                    foreach (FS.HISFC.Models.RADT.PatientInfo obj in alBabys)
                    {
                        FS.HISFC.Models.RADT.PatientInfo babyInfo = new FS.HISFC.Models.RADT.PatientInfo();
                        babyInfo = this.inpatientManager.QueryPatientInfoByInpatientNO(obj.ID);
                        if (babyInfo.PVisit.PatientLocation.NurseCell.ID == this.patientInfo.PVisit.PatientLocation.NurseCell.ID)
                        {
                            DialogResult rev = MessageBox.Show("�û�����Ӥ����ͬһ���������Ƿ�һ�����ת������", "ѯ��", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            if (rev == DialogResult.Cancel)
                            {
                                return;
                            }
                            else if (rev == DialogResult.Yes)
                            {
                                this.patientInfo.User01 = "1";

                            }
                            else
                            {
                                this.patientInfo.User01 = "0";
                            }

                        }
                    }
                }
            }
            FS.HISFC.Components.RADT.Forms.frmMessageShow frmMessage = new FS.HISFC.Components.RADT.Forms.frmMessageShow();
            frmMessage.Clear();
            if (!string.IsNullOrEmpty(checkMessage))// {06A6BD92-97A6-4f01-8B6E-533B35CBF2DD}
            {
                frmMessage.SetPatientInfo(patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "�� ���ߡ�" + patientInfo.Name + "��");
                frmMessage.SetTipMessage("������������δ����,�Ƿ��������ת�ƣ�");
                frmMessage.SetMessage(checkMessage);
                frmMessage.SetPerfectWidth();
                if (frmMessage.ShowDialog() == DialogResult.No)
                {
                    return;
                }
            }

            if (!string.IsNullOrEmpty(stopMessage))
            {
                frmMessage.SetPatientInfo(patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "�� ���ߡ�" + patientInfo.Name + "��");
                frmMessage.SetTipMessage("������������δ����,���ܼ�������ת�ƣ�");
                frmMessage.SetMessage(stopMessage);
                frmMessage.SetPerfectWidth();
                frmMessage.HideNoButton();
                frmMessage.ShowDialog();
                return;
            }
            FS.FrameWork.Models.NeuObject nurseCell = new FS.FrameWork.Models.NeuObject();

            nurseCell.ID = this.cmbNewDept.Tag.ToString();
            nurseCell.Name = this.cmbNewDept.Text;
            nurseCell.Memo = this.txtNote.Text;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
                        
            FS.HISFC.BizProcess.Integrate.RADT radt = new FS.HISFC.BizProcess.Integrate.RADT();

            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            if (radt.ShiftOut(this.patientInfo, this.patientInfo.PVisit.PatientLocation.Dept,nurseCell, this.patientInfo.User03, this.isCancel) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
            }
            else
            {
                #region addby xuewj 
                if (this.adt == null)
                {
                    this.adt = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IHE.IADT)) as FS.HISFC.BizProcess.Interface.IHE.IADT;
                }
                if (this.adt != null && patientInfo != null)
                {
                    this.adt.CancelTransferPatient(patientInfo);
                }
                #endregion
                FS.FrameWork.Management.PublicTrans.Commit();
                
            }
            MessageBox.Show(radt.Err);

            base.OnRefreshTree();//ˢ����
        }

        /// <summary>
        /// ���ݿ����ҵ�����{D2B432AC-723A-4a54-88CA-690507CEC1B9}
        /// �ֵ���{F41B3E83-2136-4939-921F-8339C96C181E}
        /// select t.rowid,t.* from com_controlargument t where t.control_code  like '%NOCHAR%'; 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbNewDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(this.cmbNewDept.Tag.ToString()))
            {
                return;
            }
            #region �ж�����ת�ƵĲ�������������֮�䲻�ܻ�ת
            string noShiftDept = this.ctlParamManage.GetControlParam<string>("NOCHAR");


            //���ѡ�еĺ���ס�Ĳ������������Եģ�����Ի�ת
            if (noShiftDept.Contains(this.cmbNewDept.Tag.ToString())
                && noShiftDept.Contains(this.patientInfo.PVisit.PatientLocation.NurseCell.ID))
            {
                return;
            }

            //��������������ԵĲ��ţ�����Ի�ת
            if (!noShiftDept.Contains(this.cmbNewDept.Tag.ToString())
                && !noShiftDept.Contains(this.patientInfo.PVisit.PatientLocation.NurseCell.ID))
            {
                return;
            }

            MessageBox.Show("������ӡ�" + this.patientInfo.PVisit.PatientLocation.NurseCell.Name + "��ת��������" + this.cmbNewDept.Text + "��");
            this.cmbNewDept.Tag = this.patientInfo.PVisit.PatientLocation.NurseCell.ID;
            return;
            /*

            //�ж�ѡ�п��ң����������ת�Ƶģ��Ͳ���ת�����ǵ�ǰ����Ҳ�����Ʋ��������Ʋ���֮����Ի�ת
            if (!string.IsNullOrEmpty(this.cmbNewDept.Tag.ToString())
                && noShiftDept.Contains(this.cmbNewDept.Tag.ToString())
                && this.patientInfo.PVisit.PatientLocation.NurseCell.ID != this.cmbNewDept.Tag.ToString()
                && !noShiftDept.Contains(this.patientInfo.PVisit.PatientLocation.NurseCell.ID))
            {
                MessageBox.Show("������ӡ�" + this.patientInfo.PVisit.PatientLocation.NurseCell.Name + "��ת��������" + this.cmbNewDept.Text + "��");
                this.cmbNewDept.Tag = this.patientInfo.PVisit.PatientLocation.NurseCell.ID;
                return;
            }

            //������������ת�Ƶģ��Ͳ���ת��������
            if (!string.IsNullOrEmpty(this.cmbNewDept.Tag.ToString())
                && !noShiftDept.Contains(this.patientInfo.PVisit.PatientLocation.NurseCell.ID)
                //&& this.patientInfo.PVisit.PatientLocation.NurseCell.ID != this.cmbNewDept.Tag.ToString()
                )
            {
                MessageBox.Show("������ӡ�" + this.patientInfo.PVisit.PatientLocation.NurseCell.Name + "��ת��������" + this.cmbNewDept.Text + "��");
                this.cmbNewDept.Tag = this.patientInfo.PVisit.PatientLocation.NurseCell.ID;
                return;
            }*/

            #endregion

        }
        #endregion

        #region IInterfaceContainer ��Ա

        /// <summary>
        /// �ӿ�����
        /// </summary>
        public Type[] InterfaceTypes
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        #endregion
    }
}
