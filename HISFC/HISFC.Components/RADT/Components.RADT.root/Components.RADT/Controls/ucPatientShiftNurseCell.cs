using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.HISFC.Components.RADT.Controls
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
    public partial class ucPatientShiftNurseCell : Neusoft.FrameWork.WinForms.Controls.ucBaseControl, Neusoft.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        /// <summary>
        /// ����
        /// </summary>
        public ucPatientShiftNurseCell()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ���Ҳ���ҵ����
        /// </summary>
        Neusoft.HISFC.BizProcess.Integrate.Manager manager = new Neusoft.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ����ҵ����
        /// </summary>
        Neusoft.HISFC.BizLogic.RADT.InPatient inpatientManager = new Neusoft.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// ������Ϣ
        /// </summary>
        Neusoft.HISFC.Models.RADT.PatientInfo patientInfo = null;

        #region addby xuewj IADT�ӿ�

        Neusoft.HISFC.BizProcess.Interface.IHE.IADT adt = null;

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
                ArrayList al = new ArrayList();
                al = manager.GetDepartment(Neusoft.HISFC.Models.Base.EnumDepartmentType.N);
                this.cmbNewDept.AddItems(al);
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
            Neusoft.HISFC.Models.RADT.Location newLocation = new Neusoft.HISFC.Models.RADT.Location();
            //ȡ����ת��������Ϣ
            newLocation = this.inpatientManager.QueryShiftNewLocation(this.patientInfo.ID, this.patientInfo.PVisit.PatientLocation.Dept.ID);
            this.patientInfo.User03 = newLocation.User03;
            if (this.patientInfo.User03 == null)
                this.patientInfo.User03 = "1";//����

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="neuObject"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.patientInfo = neuObject as Neusoft.HISFC.Models.RADT.PatientInfo;
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
        protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.InitControl();
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
            
            Neusoft.FrameWork.Models.NeuObject nurseCell = new Neusoft.FrameWork.Models.NeuObject();

            nurseCell.ID = this.cmbNewDept.Tag.ToString();
            nurseCell.Name = this.cmbNewDept.Text;
            nurseCell.Memo = this.txtNote.Text;

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
                        
            Neusoft.HISFC.BizProcess.Integrate.RADT radt = new Neusoft.HISFC.BizProcess.Integrate.RADT();

            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            if (radt.ShiftOut(this.patientInfo, this.patientInfo.PVisit.PatientLocation.Dept,nurseCell, this.patientInfo.User03, this.isCancel) == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
            }
            else
            {
                #region addby xuewj 
                if (this.adt == null)
                {
                    this.adt = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.IHE.IADT)) as Neusoft.HISFC.BizProcess.Interface.IHE.IADT;
                }
                if (this.adt != null && patientInfo != null)
                {
                    this.adt.CancelTransferPatient(patientInfo);
                }
                #endregion
                Neusoft.FrameWork.Management.PublicTrans.Commit();
                
            }
            MessageBox.Show(radt.Err);

            base.OnRefreshTree();//ˢ����
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
