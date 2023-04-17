using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
namespace FS.HISFC.Components.RADT.Controls
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
    public partial class ucPatientShiftOut : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucPatientShiftOut()
        {
            InitializeComponent();

            cmbNewDept.SelectedIndexChanged += new EventHandler(cmbNewDept_SelectedIndexChanged);

            this.btnBackDept.Click += new EventHandler(btnBackDept_Click);

            this.ncbBackDept.CheckedChanged += new EventHandler(ncbBackDept_CheckedChanged);

            this.ncbBackDept.Checked = false;

            this.btnBackDept.Visible = false;

            this.btnSave.Visible = true;

            this.cbxModefyNurse.CheckedChanged += new EventHandler(cbxModefyNurse_CheckedChanged);

        }


        /// <summary>
        /// ����ԭ�����ر���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ncbBackDept_CheckedChanged(object sender, EventArgs e)
        {
            this.cmbNewDept.Items.Clear();

            this.cmbNewNurse.Items.Clear();

            ArrayList allDept = new ArrayList();

            ArrayList allNurse = new ArrayList();

            ArrayList allShiftData = inpatient.QueryPatientShiftInfoNew(this.patientInfo.ID);

            if (this.ncbBackDept.Checked)
            {
                //���˳������б�,����comshiftdata��Ĵ���
                if (allShiftData == null || allShiftData.Count == 0)
                {
                }
                else
                {
                    foreach (FS.HISFC.Models.Invalid.CShiftData shiftInfo in allShiftData)
                    {
                        if (shiftInfo.ShitType == "RO")
                        {
                            //���ԭ�������²���
                            FS.FrameWork.Models.NeuObject deptInfoOld = (FS.FrameWork.Models.NeuObject)FS.SOC.HISFC.BizProcess.Cache.Common.GetDept(shiftInfo.OldDataCode);
                            FS.FrameWork.Models.NeuObject deptInfoNew = (FS.FrameWork.Models.NeuObject)FS.SOC.HISFC.BizProcess.Cache.Common.GetDept(shiftInfo.NewDataCode);
                            ArrayList nurseInfoOld = interMgr.QueryNurseStationByDept(deptInfoOld, "");
                            ArrayList nurseInfoNew = interMgr.QueryNurseStationByDept(deptInfoNew, "");
                            allDept.Add(deptInfoOld);
                            allDept.Add(deptInfoNew);
                            allNurse.AddRange(nurseInfoOld);
                            allNurse.AddRange(nurseInfoNew);
                        }
                    }
                }
            }
            else
            {
                allDept = manager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.I);
                allNurse = manager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.N);
            }
            this.cmbNewDept.AddItems(allDept);
            this.cmbNewNurse.AddItems(allNurse);
            //����ȫ������

            this.btnSave.Visible = !this.ncbBackDept.Checked;
            this.btnBackDept.Visible = this.ncbBackDept.Checked;
        }

        #region ����
        FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
        FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.BizLogic.RADT.InPatient inpatient = new FS.HISFC.BizLogic.RADT.InPatient();
        FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        //{62BB0773-5C87-4458-9EA9-A8196C5D3EF4}
        FS.HISFC.BizProcess.Integrate.Common.ControlParam ctlParamManage = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// �Ƿ���ȡ��ת�ƵĹ���
        /// </summary>
        private bool isCancel = false;

        /// <summary>
        /// �ӿ� ת�ơ�ת���ٻصȵط����ж�,�Ƿ����ִ����һ������
        /// </summary>
        FS.HISFC.BizProcess.Interface.IPatientShiftValid IPatientShiftValid = null;

        /// <summary>
        /// ����δ�շѵ��������뵥�Ƿ��������
        /// </summary>
        private CheckState isCanShiftWhenUnFeeUOApply = CheckState.Check;

        /// <summary>
        /// �Ƿ���Ը��Ĳ���
        /// </summary>
        public bool IsAbleChangeInpatientArea
        {
            get
            {
                return this.cbxModefyNurse.Enabled;
            }
            set
            {
                this.cbxModefyNurse.Enabled = value;
            }
        }


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

                this.panel1.Visible = !value;
                this.cbxModefyNurse.Visible = !value;
                this.cmbNewDept.Enabled = !value;
                this.cmbNewNurse.Enabled = !value;
            }
        }

        #region ����ʾ����

        /// <summary>
        /// �Ƿ�ʹ������ʾ
        /// </summary>
        private bool isUseNewMessage = true;

        /// <summary>
        /// �Ƿ�ʹ������ʾ
        /// </summary>
        [Category("ת������"), Description("�Ƿ�ʹ������ʾ��ʽ")]
        public bool IsUseNewMessage
        {
            set { this.isUseNewMessage = value; }
            get { return this.isUseNewMessage; }
        }

        #endregion

        /// <summary>
        /// ����δ�շѵ��������뵥�Ƿ��������
        /// </summary>
        [Category("ת��"), Description("����δ�շѵ��������뵥�Ƿ��������Ĭ��ΪУ�顣"), DefaultValue(CheckState.Check)]
        public CheckState IsCanShiftWhenUnFeeUOApply
        {
            get
            {
                return this.isCanShiftWhenUnFeeUOApply;
            }
            set
            {
                isCanShiftWhenUnFeeUOApply = value;
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
                ArrayList al = FS.HISFC.Models.Base.SexEnumService.List();

                //this.cmbNewDept.AddItems(manager.QueryDeptmentsInHos(true));
                this.cmbNewDept.AddItems(manager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.I));

                this.cmbNewNurse.AddItems(manager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.N));

                IPatientShiftValid = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IPatientShiftValid)) as FS.HISFC.BizProcess.Interface.IPatientShiftValid;
            }
            catch
            {
            }
        }

        /// <summary>
        /// ����ԭ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnBackDept_Click(object sender, EventArgs e)
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

            FS.FrameWork.Models.NeuObject dept = new FS.FrameWork.Models.NeuObject();
            dept.ID = this.cmbNewDept.Tag.ToString();
            dept.Name = this.cmbNewDept.Text;
            dept.Memo = this.txtNote.Text;


            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            FS.FrameWork.Models.NeuObject nurseCell = new FS.FrameWork.Models.NeuObject();
            nurseCell.ID = this.cmbNewNurse.Tag.ToString();
            nurseCell.Name = this.cmbNewNurse.Text;

            //��¼ת����Ҳ���,������Ϣ����
            FS.HISFC.Models.RADT.PatientInfo patientNew = this.patientInfo.Clone() as FS.HISFC.Models.RADT.PatientInfo;
            patientNew.PVisit.PatientLocation.NurseCell = nurseCell;
            patientNew.PVisit.PatientLocation.Dept = dept;

            if (!this.IsCancel)
            {
                //����ת��ǰ������Ϣ���
                if (this.CheckBackShiftOut(this.patientInfo, true) == -1)
                {
                    return;
                }
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            FS.HISFC.BizProcess.Integrate.RADT radt = new FS.HISFC.BizProcess.Integrate.RADT();

            if (radt.ShiftOut(this.patientInfo, dept, nurseCell, this.patientInfo.User03, this.isCancel) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
            }

            FS.FrameWork.Management.PublicTrans.Commit();

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
        /// ���ݿ����ҵ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbNewDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            //{62BB0773-5C87-4458-9EA9-A8196C5D3EF4}
            #region �ж�����ת�ƵĿ��Һ���������֮�䲻�ܻ�ת
            string noShiftDept = this.ctlParamManage.GetControlParam<string>("NOCHDT");

            //�ж�ѡ�п��ң����������ת�Ƶģ��Ͳ���ת
            if (!string.IsNullOrEmpty(this.cmbNewDept.Tag.ToString()) 
                && noShiftDept.Contains(this.cmbNewDept.Tag.ToString())
                && this.patientInfo.PVisit.PatientLocation.Dept.ID != this.cmbNewDept.Tag.ToString())
            {
                MessageBox.Show("������ӡ�" + this.patientInfo.PVisit.PatientLocation.Dept.Name + "��ת�Ƶ���" + this.cmbNewDept.Text + "��");
                this.cmbNewDept.Tag = this.patientInfo.PVisit.PatientLocation.Dept.ID;
                return;
            }

            //������������ת�Ƶģ��Ͳ���ת��������
            if (!string.IsNullOrEmpty(this.cmbNewDept.Tag.ToString())
                && noShiftDept.Contains(this.patientInfo.PVisit.PatientLocation.Dept.ID)
                && this.patientInfo.PVisit.PatientLocation.Dept.ID != this.cmbNewDept.Tag.ToString())
            {
                MessageBox.Show("������ӡ�" + this.patientInfo.PVisit.PatientLocation.Dept.Name + "��ת�Ƶ���" + this.cmbNewDept.Text + "��");
                this.cmbNewDept.Tag = this.patientInfo.PVisit.PatientLocation.Dept.ID;
                return;
            }

            #endregion

            //ArrayList alNurse = interMgr.QueryNurseStationByDept(new FS.FrameWork.Models.NeuObject(this.cmbNewDept.Tag.ToString(), "", ""));
            ArrayList alNurse = this.QueryNurseByDept(this.cmbNewDept.Tag.ToString());
            #region
            if (alNurse != null && alNurse.Count > 0)
            {
                this.cmbNewNurse.Items.Clear();
                this.cmbNewNurse.AddItems(alNurse);
                this.cmbNewNurse.Tag = (alNurse[0] as FS.FrameWork.Models.NeuObject).ID;
            }
            #endregion
        }


        /// <summary>
        /// ��������Ϣ��ʾ�ڿؼ���
        /// </summary>
        private void ShowPatientInfo()
        {
            this.txtPatientNo.Text = this.patientInfo.PID.PatientNO;		//���˺�
            this.txtPatientNo.Tag = this.patientInfo.ID;							//סԺ��ˮ��
            this.txtName.Text = this.patientInfo.Name;								//��������
            this.txtSex.Text = this.patientInfo.Sex.Name;					//�Ա�
            this.txtOldDept.Text = this.patientInfo.PVisit.PatientLocation.Dept.Name;//Դ��������
            this.cmbBedNo.Text = this.patientInfo.PVisit.PatientLocation.Bed.ID.Length > 4 ? this.patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) : "";	//����
            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            this.txtOldNurse.Text = this.patientInfo.PVisit.PatientLocation.NurseCell.Name;
            //���廼��Locationʵ��
            FS.HISFC.Models.RADT.Location newLocation = new FS.HISFC.Models.RADT.Location();
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
        /// ˢ��
        /// </summary>
        /// <param name="patientInfo"></param>
        public void RefreshList(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            try
            {
                //��������Ϣ��ʾ�ڿؼ���
                this.ShowPatientInfo();

                this.panel1.AutoScrollPosition = new Point(0, 0);
                this.CheckShiftOut(this.patientInfo, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.patientInfo = neuObject as FS.HISFC.Models.RADT.PatientInfo;
            RefreshList(this.patientInfo);
            return 0;
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
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

            FS.FrameWork.Models.NeuObject dept = new FS.FrameWork.Models.NeuObject();
            dept.ID = this.cmbNewDept.Tag.ToString();
            dept.Name = this.cmbNewDept.Text;
            dept.Memo = this.txtNote.Text;


            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            FS.FrameWork.Models.NeuObject nurseCell = new FS.FrameWork.Models.NeuObject();
            nurseCell.ID = this.cmbNewNurse.Tag.ToString();
            nurseCell.Name = this.cmbNewNurse.Text;

            //��¼ת����Ҳ���,������Ϣ����
            FS.HISFC.Models.RADT.PatientInfo patientNew = this.patientInfo.Clone() as FS.HISFC.Models.RADT.PatientInfo;
            patientNew.PVisit.PatientLocation.NurseCell = nurseCell;
            patientNew.PVisit.PatientLocation.Dept = dept;

            if (!this.IsCancel)
            {
                //ת��ǰ�Ի��߸�����Ϣ�ļ��
                if (this.CheckShiftOut(this.patientInfo, true) == -1)
                {
                    return;
                }
            }

            //�Ƿ�ѡ���ֹͣȫ������
            bool autoDC = false;
            if (!this.isAutoDcOrder && this.isUseShiftAutoDcOrder)
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

            //if (patientNew.IsBaby)// {7FFE7A7E-239D-4019-97B4-D3F80BB79713}
            //{
            //    DialogResult rev = MessageBox.Show("�Ƿ�ֻתӤ���������밴����ת�ƣ�", "ѯ��", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            //    if (rev == DialogResult.Cancel)
            //    {
            //        return;
            //    }
            //}

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            FS.HISFC.BizProcess.Integrate.RADT radt = new FS.HISFC.BizProcess.Integrate.RADT();

            if (radt.ShiftOut(this.patientInfo, dept, nurseCell, this.patientInfo.User03, this.isCancel) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
            }

            if (this.isUseShiftAutoDcOrder)
            {
                string errInfo = "";
                if (this.isAutoDcOrder || autoDC)
                {
                    if (this.AutoDcOrder(ref errInfo) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(errInfo);
                        return;
                    }
                }
            }


            FS.FrameWork.Management.PublicTrans.Commit();

            if (bSaveAndClose)
            {
                dialogResult = DialogResult.OK;
                this.FindForm().Close();
                return;
            }

            MessageBox.Show(radt.Err);

            base.OnRefreshTree();//ˢ����
        }



        #region ת����������

        /// <summary>
        /// �����˷������Ƿ�������ת������
        /// </summary>
        private CheckState isCanShiftWhenQuitFeeApplay = CheckState.Check;

        /// <summary>
        /// ���˷������Ƿ�����ת������
        /// </summary>
        [Category("ת������"), Description("�����˷������Ƿ�������ת������")]
        public CheckState IsCanShiftWhenQuitFeeApplay
        {
            get
            {
                return isCanShiftWhenQuitFeeApplay;
            }
            set
            {
                isCanShiftWhenQuitFeeApplay = value;
            }
        }

        /// <summary>
        /// ������ҩ�����Ƿ�������ת������
        /// </summary>
        private CheckState isCanShiftWhenQuitDrugApplay = CheckState.Check;

        /// <summary>
        /// ������ҩ�����Ƿ�������ת������
        /// </summary>
        [Category("ת������"), Description("������ҩ�����Ƿ�������ת������")]
        public CheckState IsCanShiftWhenQuitDrugApplay
        {
            get
            {
                return isCanShiftWhenQuitDrugApplay;
            }
            set
            {
                isCanShiftWhenQuitDrugApplay = value;
            }
        }

        /// <summary>
        /// ���ڷ�ҩ�����Ƿ�������ת������
        /// </summary>
        private CheckState isCanShiftWhenDrugApplay = CheckState.Check;

        /// <summary>
        /// ���ڷ�ҩ�����Ƿ�������ת������
        /// </summary>
        [Category("ת������"), Description("���ڷ�ҩ�����Ƿ�������ת������")]
        public CheckState IsCanShiftWhenDrugApplay
        {
            get
            {
                return this.isCanShiftWhenDrugApplay;
            }
            set
            {
                this.isCanShiftWhenDrugApplay = value;
            }
        }

        /// <summary>
        /// ����δȷ����Ŀ�Ƿ�������ת������
        /// </summary>
        private CheckState isCanShiftWhenUnConfirm = CheckState.Check;

        /// <summary>
        /// ����δȷ����Ŀ�Ƿ�������ת������
        /// </summary>
        [Category("ת������"), Description("����δȷ����Ŀ�Ƿ�������ת������")]
        public CheckState IsCanShiftWhenUnConfirm
        {
            get
            {
                return this.isCanShiftWhenUnConfirm;
            }
            set
            {
                this.isCanShiftWhenUnConfirm = value;
            }
        }

        /// <summary>
        /// δ����ת��ҽ���Ƿ�������ת������
        /// </summary>
        private CheckState isCanShiftWhenNoOutOrder = CheckState.No;

        /// <summary>
        /// δ����ת��ҽ���Ƿ�������ת������
        /// </summary>
        [Category("ת������"), Description("δ����ת��ҽ���Ƿ�������ת������")]
        public CheckState IsCanShiftWhenNoOutOrder
        {
            get
            {
                return this.isCanShiftWhenNoOutOrder;
            }
            set
            {
                this.isCanShiftWhenNoOutOrder = value;
            }
        }

        /// <summary>
        /// δȫ��ֹͣ�����Ƿ�������ת������
        /// </summary>
        private CheckState isCanShiftWhenNoDcOrder = CheckState.No;

        /// <summary>
        /// δȫ��ֹͣ�����Ƿ�������ת������
        /// </summary>
        [Category("ת������"), Description("δȫ��ֹͣ�����Ƿ�������ת������")]
        public CheckState IsCanShiftWhenNoDcOrder
        {
            get
            {
                return this.isCanShiftWhenNoDcOrder;
            }
            set
            {
                this.isCanShiftWhenNoDcOrder = value;
            }
        }

        /// <summary>
        /// ����δ���ҽ���Ƿ�������ת������
        /// </summary>
        private CheckState isCanShiftWhenUnConfirmOrder = CheckState.No;

        /// <summary>
        /// ����δ���ҽ���Ƿ�������ת������
        /// </summary>
        [Category("ת������"), Description("����δ���ҽ���Ƿ�������ת������")]
        public CheckState IsCanShiftWhenUnConfirmOrder
        {
            get
            {
                return this.isCanShiftWhenUnConfirmOrder;
            }
            set
            {
                this.isCanShiftWhenUnConfirmOrder = value;
            }
        }

        /// <summary>
        /// ����δ�շѵķ�ҩƷִ�е��Ƿ�������ת������
        /// </summary>
        private CheckState isCanShiftWhenNoFeeExecUndrugOrder = CheckState.Check;

        /// <summary>
        /// ����δ�շѵķ�ҩƷִ�е��Ƿ�������ת������
        /// </summary>
        [Category("ת������"), Description("����δ�շѵķ�ҩƷִ�е��Ƿ�������ת������")]
        public CheckState IsCanShiftWhenNoFeeExecUndrugOrder
        {
            get
            {
                return this.isCanShiftWhenNoFeeExecUndrugOrder;
            }
            set
            {
                isCanShiftWhenNoFeeExecUndrugOrder = value;
            }
        }

        /// <summary>
        /// ����δ��ɵĻ��������Ƿ��������ת������
        /// </summary>
        private CheckState isCanShiftWhenUnConsultation = CheckState.Check;

        /// <summary>
        /// ����δ��ɵĻ��������Ƿ��������ת������
        /// </summary>
        [Category("ת������"), Description("����δ��ɵĻ��������Ƿ��������ת������")]
        public CheckState IsCanShiftWhenUnConsultation
        {
            get
            {
                return this.isCanShiftWhenUnConsultation;
            }
            set
            {
                isCanShiftWhenUnConsultation = value;
            }
        }

        /// <summary>
        /// Ƿ���Ƿ��������ת������
        /// </summary>
        private CheckState isCanShiftWhenLackFee = CheckState.Check;

        /// <summary>
        /// Ƿ���Ƿ��������ת������
        /// </summary>
        [Category("ת������"), Description("Ƿ���Ƿ��������ת������")]
        public CheckState IsCanShiftWhenLackFee
        {
            get
            {
                return this.isCanShiftWhenLackFee;
            }
            set
            {
                isCanShiftWhenLackFee = value;
            }
        }

        #endregion

        /// <summary>
        /// �Ի��߽���ת���ж� by huangchw 2012-11-20
        /// </summary>
        /// <returns></returns>
        private int CheckShiftOut(FS.HISFC.Models.RADT.PatientInfo patient, bool isSave)
        {
            //��������ʾͳһ�ŵ�һ��

            //��Ҫ��ʾѡ��Ķ���
            string checkMessage = "";

            //��ʾ��ֹ�Ķ���
            string stopMessage = "";

            Classes.Function funMgr = new FS.HISFC.Components.RADT.Classes.Function();
            if (IPatientShiftValid != null)
            {
                bool bl = IPatientShiftValid.IsPatientShiftValid(patient, FS.HISFC.Models.Base.EnumPatientShiftValid.O, ref stopMessage);
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
             * 2���Ƿ���ת��ҽ��
             * 3���Ƿ���δ���ҽ��
             * 4���жϴ�λ���ͻ���ѵ���ȡ�Ƿ���ȷ
             * */


            #region 1�������˷����룬���������ת�ƵǼ�

            if (isCanShiftWhenQuitFeeApplay != CheckState.Yes)
            {
                string ReturnApplyItemInfo = FS.HISFC.Components.RADT.Classes.Function.CheckReturnApply(patient.ID);
                if (ReturnApplyItemInfo != null)
                {
                    string[] item = ReturnApplyItemInfo.Split('\r');
                    string tip = "";
                    for (int i = 0; i < item.Length; i++)
                    {
                        if (isUseNewMessage)
                        {
                            tip += item[i] + "\r";
                        }
                        else
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
                    }

                    if (isCanShiftWhenQuitFeeApplay == CheckState.Check)
                    {
                        checkMessage += "\r\n�����δȷ�ϵ��˷����룡\r\n" + tip;
                    }
                    else if (isCanShiftWhenQuitFeeApplay == CheckState.No)
                    {
                        //�����˷����벻������ת������
                        stopMessage += "\r\n����δȷ�ϵ��˷����룡\r\n" + ReturnApplyItemInfo;
                    }
                }
            }
            #endregion

            #region 2��������ҩ���룬��ʾ�Ƿ����

            if (isCanShiftWhenQuitDrugApplay != CheckState.Yes)
            {
                //���Ӳ�ѯ�����Ƿ���δ��˵���ҩ��¼,Ϊת�������ж���
                if (isUseNewMessage)
                {
                    string msg = FS.HISFC.Components.RADT.Classes.Function.CheckQuitDrugApplay(patient.ID);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        checkMessage += "\r\n\r\n�����δ��˵���ҩ������Ϣ��" + "\r" + msg;
                    }
                }
                else
                {
                    int returnValue = this.pharmacyIntegrate.QueryNoConfirmQuitApply(patient.ID);
                    if (returnValue == -1)
                    {
                        MessageBox.Show("��ѯ������ҩ������Ϣ����!" + this.pharmacyIntegrate.Err);

                        return -1;
                    }
                    if (returnValue > 0) //�����뵫��û�к�׼����ҩ��Ϣ
                    {
                        if (this.isCanShiftWhenQuitDrugApplay == CheckState.Check)
                        {
                            checkMessage += "\r\n\r\n�����δ��˵���ҩ������Ϣ��";
                        }
                        else if (this.isCanShiftWhenQuitDrugApplay == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n����δ��˵���ҩ������Ϣ��";
                        }
                    }
                }
            }

            #endregion

            #region 3���жϻ����Ǵ���δ��ҩ��ҩƷ ��ʾ�Ƿ����

            if (isCanShiftWhenDrugApplay != CheckState.Yes)
            {
                if (isUseNewMessage)
                {
                    string msg = FS.HISFC.Components.RADT.Classes.Function.CheckDrugApplayWithOutQuit(patient.ID);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        checkMessage += "\r\n\r\n�����δ��ҩ��ҩƷ��Ŀ��\r\n" + msg;
                    }
                }
                else
                {
                    string msg = FS.HISFC.Components.RADT.Classes.Function.CheckDrugApplay(patient.ID);
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

                        if (this.isCanShiftWhenDrugApplay == CheckState.Check)
                        {
                            checkMessage += "\r\n\r\n�����δ��ҩ��ҩƷ��Ŀ��\r\n" + tip;
                        }
                        else if (isCanShiftWhenDrugApplay == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n����δ��ҩ��ҩƷ��Ŀ��\r\n" + msg;
                        }
                    }
                }
            }
            #endregion

            #region 4������δ�ն�ȷ����Ŀ����ʾ�Ƿ����

            if (isCanShiftWhenUnConfirm != CheckState.Yes)
            {
                string UnConfirmItemInfo = FS.HISFC.Components.RADT.Classes.Function.CheckUnConfirm(patient.ID);
                if (UnConfirmItemInfo != null)
                {
                    string[] item = UnConfirmItemInfo.Split('\r');
                    string tip = "";
                    for (int i = 0; i < item.Length; i++)
                    {
                        if (isUseNewMessage)
                        {
                            tip += item[i] + "\r\n";
                        }
                        else
                        {
                            if (i <= 2)
                            {
                                tip += item[i] + "\r\n";

                                //{4660CE00-A79E-468a-8086-DE9C8D811779} ������ʾ��Ϣ����
                                if (tip.Length > 100)
                                {
                                    tip = tip.Substring(0, 100);
                                    tip += "   ��";
                                    break;
                                }
                                //{4660CE00-A79E-468a-8086-DE9C8D811779} ������ʾ��Ϣ����

                                if (i == item.Length - 1 || i == 2)
                                {
                                    tip += "   ��";
                                }
                            }
                        }
                    }

                    if (this.isCanShiftWhenUnConfirm == CheckState.Check)
                    {
                        //checkMessage += "\r\n\r\n�����δȷ���շѵ��ն���Ŀ��\r\n" + tip;
                        checkMessage += "\r\n\r\n�����δȷ���շѵ��ն���Ŀ��\r\n" + UnConfirmItemInfo;
                    }
                    else if (isCanShiftWhenUnConfirm == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n����δȷ���շѵ��ն���Ŀ��\r\n" + UnConfirmItemInfo;
                    }
                }
            }

            #endregion

            #region 5���ж��Ƿ���ת��ҽ��

            if (isCanShiftWhenNoOutOrder != CheckState.Yes)
            {
                FS.HISFC.Models.Order.Inpatient.Order inOrder = null;

                int rev = this.orderIntegrate.GetShiftOutOrderType(patientInfo.ID, ref inOrder);

                if (rev < 0)
                {
                    MessageBox.Show("��ѯת��ҽ������!\r\n" + orderIntegrate.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    return -1;
                }
                else if (rev == 0)
                {
                    if (isCanShiftWhenNoOutOrder == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n��δ����ת��ҽ����";
                    }
                    else if (isCanShiftWhenNoOutOrder == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n��δ����ת��ҽ����";
                    }
                }
            }

            #endregion

            #region 6���жϳ����Ƿ�ȫͣ

            if (isCanShiftWhenNoDcOrder != CheckState.Yes)
            {
                if (isUseNewMessage)
                {
                    string msg = FS.HISFC.Components.RADT.Classes.Function.CheckAllLongOrderUnStop(patient.ID);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        if (isCanShiftWhenNoDcOrder == CheckState.Check)
                        {
                            checkMessage += "\r\n\r\n�ﳤ��ҽ��û��ȫ��ֹͣ\r\n" + msg;
                        }
                        else if (isCanShiftWhenNoDcOrder == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n�ﳤ��ҽ��û��ȫ��ֹͣ\r\n" + msg;
                        }
                    }
                }
                else
                {
                    if (!funMgr.CheckIsAllLongOrderStop(patient.ID))
                    {
                        if (isCanShiftWhenNoDcOrder == CheckState.Check)
                        {
                            checkMessage += "\r\n\r\n��" + funMgr.Err;
                        }
                        else if (isCanShiftWhenNoDcOrder == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n" + funMgr.Err;
                        }
                    }
                }
            }

            #endregion

            #region 7���ж��Ƿ���δ���ҽ��


            if (isCanShiftWhenUnConfirmOrder != CheckState.Yes)
            {
                if (isUseNewMessage)
                {
                    string msg = FS.HISFC.Components.RADT.Classes.Function.CheckAllOrderUnConfirm(patient.ID);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        if (isCanShiftWhenUnConfirmOrder == CheckState.Check)
                        {
                            checkMessage += "\r\n\r\n�����δ���ҽ��\r\n" + msg;
                        }
                        else if (isCanShiftWhenUnConfirmOrder == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n�����δ���ҽ��\r\n" + msg;
                        }
                    }
                }
                else
                {
                    if (!funMgr.CheckIsAllOrderConfirm(patient.ID))
                    {
                        if (isCanShiftWhenUnConfirmOrder == CheckState.Check)
                        {
                            stopMessage += "\r\n\r\n��" + funMgr.Err;
                        }
                        else if (isCanShiftWhenUnConfirmOrder == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n" + funMgr.Err;
                        }
                    }
                }
            }

            #endregion

            #region 8���ж��Ƿ���δ�շѵķ�ҩƷҽ��ִ�е�

            if (isCanShiftWhenNoFeeExecUndrugOrder != CheckState.Yes)
            {
                string returnStr = FS.HISFC.Components.RADT.Classes.Function.CheckExecOrderCharge(patient.ID);
                if (returnStr != null)
                {
                    string[] strArray = returnStr.Split(new char[3] { '|', '|', '|' });

                    if (Convert.ToInt32(strArray[3]) > 0)
                    {
                        if (this.isCanShiftWhenNoFeeExecUndrugOrder == CheckState.Check)
                        {
                            checkMessage += "\r\n\r\n�����δ�շ���Ŀ��\r\n" + strArray[0];

                            if (!isUseNewMessage)
                            {
                                //{4660CE00-A79E-468a-8086-DE9C8D811779} ������ʾ��Ϣ����
                                if (checkMessage.Length > 250)
                                {
                                    checkMessage = checkMessage.Substring(0, 250);
                                    checkMessage += "   ��";
                                }
                            }
                            //{4660CE00-A79E-468a-8086-DE9C8D811779} ������ʾ��Ϣ����

                        }
                        else if (this.isCanShiftWhenNoFeeExecUndrugOrder == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n����δ�շ���Ŀ��\r\n" + strArray[0];
                        }
                    }
                }
            }

            #endregion

            #region 9��ת�����������Ϊ��

            //if (this.cmbZg.Tag == null || string.IsNullOrEmpty(cmbZg.Tag.ToString()))
            //{
            //    stopMessage += "\r\n\r\n������Ҫ��ת�����������Ϊ�գ�\r\n";
            //}
            #endregion

            #region 10������δ�շ��������뵥���������ת��
            // {a50189cc-d017-4e36-97f1-e011f4603858} ת�Ƽ����ж��������뵥������ת�ơ�2013-8-29 by xuc.
            if (this.isCanShiftWhenUnFeeUOApply != CheckState.Yes)
            {
                string sql = @"select  f.apply_date,f.pre_date,a.item_name,f.apply_dpcd from met_ops_apply f,met_ops_operationitem a
                                                        where a.operationno=f.operationno
                                                        and f.clinic_code = a.clinic_code  
                                                        and f.clinic_code='{0}'
                                                        and f.ynvalid='1'
                                                        and a.main_flag = '1'
                                                        and f.execstatus!='4'
                                                        and f.execstatus!='5'
                            order by a.sort_no";
                try
                {
                    DataSet dsTemp = null;
                    string msg = null;
                    int queryRev = funMgr.ExecQuery(string.Format(sql, patient.ID), ref dsTemp);

                    if (queryRev > 0)
                    {
                        if (dsTemp != null && dsTemp.Tables.Count > 0)
                        {
                            DataTable dtTemp = dsTemp.Tables[0];

                            string strApplyDate = string.Empty;
                            string strItemName = string.Empty;
                            string strPreDate = string.Empty;
                            string applyDpcd = string.Empty;
                            foreach (DataRow drRow in dtTemp.Rows)
                            {
                                strApplyDate = drRow["apply_date"].ToString();
                                strItemName = drRow["item_name"].ToString();
                                strPreDate = drRow["pre_date"].ToString();
                                applyDpcd = drRow["apply_dpcd"].ToString();

                                msg = msg + "����ʱ�䣺" + strApplyDate + "    " + "ԤԼʱ�䣺" + strPreDate + "\r\n" + "������ң�" + SOC.HISFC.BizProcess.Cache.Common.GetDept(applyDpcd) + "    �������ƣ�" + strItemName + "\r\n";
                            }

                            if (this.isCanShiftWhenUnFeeUOApply == CheckState.No)
                            {
                                // ���������������������ת�ơ�
                                stopMessage += "\r\n\r\n�����δ��ɵ��������뵥��\r\n" + msg;
                            }
                            else if (this.isCanShiftWhenUnFeeUOApply == CheckState.Check)
                            {
                                // ���������������������ת�ơ�
                                checkMessage += "\r\n\r\n�����δ��ɵ��������뵥��\r\n" + msg;
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("����δ��ɵ��������뵥");
                }
            }

            #endregion


            #region 11������δ��ɻ��������ת��

            if (isCanShiftWhenUnConsultation != CheckState.Yes)
            {
                string strSQL = @"select --f.state,--0:����δ���� 1:�ѽ��� 2:����� 3:�Ѿܾ�
                                   count(1)
                            from bhemr.CONS_CONSULTATION f
                            where f.state in('0','1')
                            and f.inpatient_id=(
                            select g.id from 
                            bhemr.pt_inpatient_cure g
                            where g.inpatient_code='{0}'
                            )";
                strSQL = string.Format(strSQL, this.patientInfo.ID);

                string errMsg = null;
                if (funMgr.ExecSqlReturnOne(strSQL) != "0")
                {
                    errMsg = "\r\n\r\n�����δ��ɵĻ������룡\n";
                }

                if (isCanShiftWhenUnConsultation == CheckState.Check)
                {
                    checkMessage += errMsg;
                }
                else if (isCanShiftWhenUnConsultation == CheckState.No)
                {
                    stopMessage += errMsg;
                }
            }
            #endregion

            #region Ƿ���ж�

            if (isCanShiftWhenLackFee != CheckState.Yes)
            {
                try
                {
                    if (patient.PVisit.MoneyAlert != 0 && patient.FT.LeftCost < this.patientInfo.PVisit.MoneyAlert)
                    {
                        if (isCanShiftWhenUnFeeUOApply == CheckState.Check)
                        {
                            checkMessage += "\r\n\r\n�Ѿ�Ƿ�ѣ�\r\n�� " + patient.FT.LeftCost.ToString() + "\r\n�����ߣ� " + patient.PVisit.MoneyAlert.ToString();
                        }
                        else if (isCanShiftWhenUnFeeUOApply == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n�Ѿ�Ƿ�ѣ�\r\n�� " + patient.FT.LeftCost.ToString() + "\r\n�����ߣ� " + patient.PVisit.MoneyAlert.ToString();
                        }
                    }
                }
                catch
                {
                }
            }
            #endregion

            if (!isSave)
            {
                this.label1.Text = stopMessage;
                return -1;
            }

            if (isUseNewMessage)
            {
                FS.HISFC.Components.RADT.Forms.frmMessageShow frmMessage = new FS.HISFC.Components.RADT.Forms.frmMessageShow();
                frmMessage.Clear();
                if (!string.IsNullOrEmpty(checkMessage))
                {
                    frmMessage.SetPatientInfo(patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "�� ���ߡ�" + patient.Name + "��");
                    frmMessage.SetTipMessage("������������δ����,�Ƿ��������ת�ƣ�");
                    frmMessage.SetMessage(checkMessage);
                    frmMessage.SetPerfectWidth();
                    if (frmMessage.ShowDialog() == DialogResult.No)
                    {
                        return -1;
                    }
                }

                if (!string.IsNullOrEmpty(stopMessage))
                {
                    frmMessage.SetPatientInfo(patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "�� ���ߡ�" + patient.Name + "��");
                    frmMessage.SetTipMessage("������������δ����,���ܼ�������ת�ƣ�");
                    frmMessage.SetMessage(stopMessage);
                    frmMessage.SetPerfectWidth();
                    frmMessage.HideNoButton();
                    frmMessage.ShowDialog();
                    return -1;
                }
            }
            else
            {
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
            }
            return 1;
        }

        /// <summary>
        /// ����ת��ԭ�����ж�
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="isSave"></param>
        /// <returns></returns>
        private int CheckBackShiftOut(FS.HISFC.Models.RADT.PatientInfo patient, bool isSave)
        {
            //���ڴ��ַ�ʽ���ж�ת�ƴ�������������ֻ�ж�ת�ƹ������Ƿ�����ҽ��
            string stopMessage = "";

            #region �ж�ҽ��
            string strOrderSql = @"select count(*)
  from met_ipm_order a,(select b.clinic_no,b.new_data_code,b.oper_date
          from com_shiftdata b
         where b.shift_type = 'RO'
           and b.clinic_no = '{0}'
           and rownum = 1
           order by b.oper_date desc) bb  where
           a.inpatient_no = '{0}' 
           and a.inpatient_no = bb.clinic_no(+)
           and a.dept_code = bb.new_data_code(+)
           and a.mo_date > bb.oper_date ";

            strOrderSql = string.Format(strOrderSql, patient.ID);

            int orderCount = FS.FrameWork.Function.NConvert.ToInt32(inpatient.ExecSqlReturnOne(strOrderSql, "0"));

            if (orderCount > 0)
            {
                stopMessage += "���ߣ�" + patient.Name + "��ת�ƺ��Ѿ�����" + orderCount + "��ҽ����\n" + "�޷�ֱ�ӷ���ԭ���ң�����ϵҽ������ת��ҽ����������ת�Ʋ�����";
                MessageBox.Show(stopMessage);
                return -1;
            }
            #endregion

            #region �жϷ��ã����������λ���ռƷѵ���Ҫ�ѻ��߷����˵�Ȼ�����ת��ԭ����
            string strFeeSql = @"select sum(c.pub_cost + c.pay_cost + c.own_cost) from fin_ipb_feeinfo c,(select b.clinic_no,b.new_data_code,b.oper_date
          from com_shiftdata b
         where b.shift_type = 'RO'
           and b.clinic_no = '{0}'
           and rownum = 1
           order by b.oper_date desc) bb  where
           c.inpatient_no = '{0}' 
           and c.recipe_deptcode = '{1}'
           and c.inpatient_no = bb.clinic_no(+)
           and c.recipe_deptcode = bb.new_data_code(+)
           and c.charge_date > bb.oper_date";

            strFeeSql = string.Format(strFeeSql, patient.ID, patient.PVisit.PatientLocation.Dept.ID);

            decimal feeCost = FS.FrameWork.Function.NConvert.ToDecimal(inpatient.ExecSqlReturnOne(strFeeSql, "0"));
            if (feeCost > 0)
            {
                stopMessage += "���ߣ�" + patient.Name + "��ת�ƺ��Ѿ�����" + feeCost + "Ԫ���ã�\n" + "ֱ�ӷ��ؿ���ǰ�����˵�������Ʋ������ռƷѵ���Ŀ��Ϣ��";
                MessageBox.Show(stopMessage);
                return -1;
            }

            #endregion

            return 1;
        }

        private FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();

        /// <summary>
        /// ת���Զ�ֹͣ������ֹͣҽ��
        /// </summary>
        private AotuDcDoct autoDcDoct = AotuDcDoct.ExecutOutDoct;

        /// <summary>
        /// ת���Զ�ֹͣ������ֹͣҽ��
        /// </summary>
        [Category("�ؼ�����"), Description("ת���Զ�ֹͣ������ֹͣҽ��")]
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
        [Category("ת������"), Description("�Ƿ�ʹ��ת���Զ�ֹͣҽ�����ܣ�ʹ�ú���뿪��ת��ҽ������ת�ƣ�")]
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
        /// ת���Զ�ֹͣȫ������
        /// </summary>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        private int AutoDcOrder(ref string errInfo)
        {
            //����ת��ҽ����ҽ��
            if (autoDcDoct == AotuDcDoct.ExecutOutDoct)
            {
                FS.HISFC.Models.Order.Inpatient.Order orderObj = null;
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

        public FS.FrameWork.Models.NeuObject Dept
        {
            get
            {
                return this.cmbNewDept.SelectedItem;
            }
        }

        public void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.InitControl();
            this.patientInfo = patientInfo.Clone();
            RefreshList(this.patientInfo);
        }

        public DialogResult ShowDialog()
        {
            bSaveAndClose = true;
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(this);
            return dialogResult;

        }

        private void cbxModefyNurse_CheckedChanged(object sender, EventArgs e)
        {
            cmbNewNurse.Enabled = cbxModefyNurse.Checked;
        }


        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                return new Type[] { typeof(FS.HISFC.BizProcess.Interface.ITransferDeptApplyable), 
                                    typeof(FS.HISFC.BizProcess.Interface.IPatientShiftValid) };
            }
        }

        #endregion
    }
}
