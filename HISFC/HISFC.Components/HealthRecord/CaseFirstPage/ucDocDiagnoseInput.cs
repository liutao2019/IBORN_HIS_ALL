using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
{
    /// <summary>
    /// ucCaseMainInfo<br></br>
    /// [��������: סԺҽ�����¼��]<br></br>
    /// [�� �� ��: dorian]<br></br>
    /// [����ʱ��: 2008-03]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucDocDiagnoseInput : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDocDiagnoseInput()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        #region ����

        #region {8BC09475-C1D9-4765-918B-299E21E04C74} ���¼������ҽ��վ������ҽ��վ������������

        /// <summary>
        /// �Ƿ��ǲ���¼�����  ҽ��վ¼����ϺͲ�����¼����ϴ�ı�ͬ
        /// </summary>
        public enum EnumDiagInput
        {
            /// <summary>
            /// ������
            /// </summary>
            Cas,

            /// <summary>
            /// סԺҽ��
            /// </summary>
            InpatientOrder,

            /// <summary>
            /// ����ҽ��
            /// </summary>
            OutPatientOrder
        }
        private EnumDiagInput enumdiaginput = EnumDiagInput.InpatientOrder;

        [Category("����ҽ��վ��סԺҽ��վ���ǲ�����¼���"), Description("������¼�������ҽ��¼���������ı�ͬ")]
        public EnumDiagInput Enumdiaginput
        {
            get
            {
                return enumdiaginput;
            }
            set
            {
                enumdiaginput = value;
                if (enumdiaginput == EnumDiagInput.Cas)
                {
                    this.ucDiagNoseInput1.IsCas = true;
                }
                else
                {
                    this.ucDiagNoseInput1.IsCas = false;
                }
            }
        }

        //�ж��Ƿ��Ǳ�Ŀ����ã�������޸Ĳ��������ҽ��¼��ģ����������¼���(���ղ����Ҳ�¼�����)
        public bool isList = false;
        #endregion

        #region {6EF7D73B-4350-4790-B98C-C0BD0098516E}
        /// <summary>
        /// ���ҳ�����ϱ�־
        /// </summary>
        private bool isUseDeptICD = false;

        /// <summary>
        /// ���ҳ�����ϱ�־
        /// </summary>
        [Category("���ҳ������"), Description("�Ƿ���ʹ�ÿ��ҳ������")]
        public bool IsUseDeptICD
        {
            get
            {
                return isUseDeptICD;
            }
            set
            {
                isUseDeptICD = value;
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// ����� סԺ��
        /// </summary>
        /// <param name="InpatientNo"></param>
        /// <returns></returns>
        public int LoadInfo(string InpatientNo)
        {
            if (InpatientNo == null || InpatientNo == "")
            {
                patientInfo = null;
                MessageBox.Show("�����סԺ��ˮ��Ϊ��");
                return -1;
            }

            FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
            FS.HISFC.BizProcess.Integrate.Registration.Registration registerIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

            //{8BC09475-C1D9-4765-918B-299E21E04C74} ���¼������ҽ��վ������ҽ��վ������������
            if (Enumdiaginput == EnumDiagInput.InpatientOrder)//סԺҽ��
            {
                //��סԺ�����в�Ѯ
                patientInfo = radtIntegrate.GetPatientInfomation(InpatientNo);
                if (patientInfo == null)
                {
                    FS.HISFC.Models.Registration.Register obj = registerIntegrate.GetByClinic(InpatientNo);
                    if (obj == null)
                    {
                        MessageBox.Show("��ѯ������Ϣ����");
                        return -1;
                    }
                    patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                    patientInfo.ID = obj.ID;
                    patientInfo.CaseState = "1";
                }
                //this.ucDiagNoseInput1.LoadInfo(patientInfo, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
                this.ucDiagNoseInput1.LoadInfo(patientInfo, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, enumdiaginput.ToString());

            }
            else if (Enumdiaginput == EnumDiagInput.OutPatientOrder)//����ҽ��
            {
                FS.HISFC.Models.Registration.Register obj = registerIntegrate.GetByClinic(InpatientNo);
                if (obj == null)
                {
                    MessageBox.Show("��ѯ������Ϣ����");
                    return -1;
                }
                patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                patientInfo.ID = obj.ID;
                patientInfo.PID.CardNO = obj.PID.CardNO;
                patientInfo.CaseState = "1";
                this.ucDiagNoseInput1.LoadInfo(patientInfo, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, enumdiaginput.ToString());

            }
            else if (Enumdiaginput == EnumDiagInput.Cas)
            {
                //��סԺ�����в�Ѯ
                patientInfo = radtIntegrate.GetPatientInfomation(InpatientNo);
                if (patientInfo == null)
                {
                    FS.HISFC.Models.Registration.Register obj = registerIntegrate.GetByClinic(InpatientNo);
                    if (obj == null)
                    {
                        MessageBox.Show("��ѯ������Ϣ����");
                        return -1;
                    }
                    patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                    patientInfo.ID = obj.ID;
                    patientInfo.CaseState = "1";
                }
                this.ucDiagNoseInput1.LoadInfo(patientInfo, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS, enumdiaginput.ToString());

            }

            this.ucDiagNoseInput1.fpEnterSaveChanges();
            if (this.ucDiagNoseInput1.GetfpSpreadRowCount() == 0)
            {
                this.ucDiagNoseInput1.AddRow();
            }
            return 1;
        }

        /// <summary>
        /// ��ʼ���� �Ͳ�������ѡ�� ��������ICD�� ��
        /// </summary>
        /// <returns></returns>
        public void InitInfo()
        {
            this.ucDiagNoseInput1.InitInfo();
        }

        /// <summary>
        /// ���� 
        /// </summary>
        /// <returns>1 ����ɹ� ,-1 ����ʧ��</returns>
        private int Save()
        {
            //modify chengym 2012-1-10
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            FS.HISFC.BizLogic.HealthRecord.Diagnose diagNose = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            diagNose.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            List<FS.HISFC.Models.HealthRecord.Diagnose> diagList = new List<FS.HISFC.Models.HealthRecord.Diagnose>();
            this.ucDiagNoseInput1.deleteRow();

            this.ucDiagNoseInput1.GetDiagnosInfo(diagList);
            if (this.ucDiagNoseInput1.ValueStateNew(diagList) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack(); //����У��ʧ��
                return -3;
            }
            if (diagList != null)
            {
                diagNose.DeleteDiagnoseAll(patientInfo.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, FS.HISFC.Models.Base.ServiceTypes.I);
                foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in diagList)
                {
                    if (diagNose.InsertDiagnose(obj) < 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���������Ϣʧ��" + diagNose.Err);
                        return -1;
                    }
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("����ɹ�");
            return 1;

            #region ����ԭ����

            //FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.HISFC.BizLogic.HealthRecord.Diagnose diagNose = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            ////FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(diagNose.Connection);
            ////trans.BeginTransaction();
            ////diagNose.SetTrans(trans.Trans);

            //ArrayList diagAdd = new ArrayList();
            //ArrayList diagMod = new ArrayList();
            //ArrayList diagDel = new ArrayList();

            //this.ucDiagNoseInput1.deleteRow();
            //this.ucDiagNoseInput1.GetList("A", diagAdd);
            //this.ucDiagNoseInput1.GetList("M", diagMod);
            //this.ucDiagNoseInput1.GetList("D", diagDel);

            ////{6873115C-BBAC-4de0-95BB-F905B766F5AA}
            //if (diagAdd.Count == 0 && diagDel.Count == 0 && diagMod.Count == 0)
            //{
            //    MessageBox.Show("���豣��");
            //    return -1;
            //}

            //if (this.ucDiagNoseInput1.ValueState(diagAdd) == -1 || this.ucDiagNoseInput1.ValueState(diagMod) == -1 || this.ucDiagNoseInput1.ValueState(diagDel) == -1)
            //{
            //    //trans.RollBack();
            //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    return -1;
            //}

            //if (diagDel != null)
            //{
            //    foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in diagDel)
            //    {
            //        //{8BC09475-C1D9-4765-918B-299E21E04C74} ���¼������ҽ��վ������ҽ��վ������������
            //        if (enumdiaginput == EnumDiagInput.Cas)
            //        {
            //            if (diagNose.DeleteDiagnoseSingle(obj.DiagInfo.Patient.ID, obj.DiagInfo.HappenNo, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, FS.HISFC.Models.Base.ServiceTypes.I) < 1)
            //            {
            //                //trans.RollBack();
            //                FS.FrameWork.Management.PublicTrans.RollBack();
            //                MessageBox.Show("���������Ϣʧ��" + diagNose.Err);
            //                return -1;
            //            }
            //        }
            //        else
            //        {
            //            if (diagNose.DeleteDiagnoseSingle(obj.DiagInfo.Patient.ID, obj.DiagInfo.HappenNo) < 1)
            //            {
            //                //trans.RollBack();
            //                FS.FrameWork.Management.PublicTrans.RollBack();
            //                MessageBox.Show("ɾ�������Ϣʧ��" + diagNose.Err);
            //                return -1;
            //            }

            //        }
            //    }
            //}
            //if (diagMod != null)
            //{
            //    foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in diagMod)
            //    {
            //                            //{8BC09475-C1D9-4765-918B-299E21E04C74} ���¼������ҽ��վ������ҽ��վ������������
            //        if (enumdiaginput == EnumDiagInput.Cas)
            //        {
            //            if (diagNose.UpdateDiagnose(obj) < 1)
            //            {
            //                if (diagNose.InsertDiagnose(obj) < 1)
            //                {
            //                    //trans.RollBack();
            //                    FS.FrameWork.Management.PublicTrans.RollBack();
            //                    MessageBox.Show("���������Ϣʧ��" + diagNose.Err);
            //                    return -1;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            if (diagNose.UpdatePatientDiagnose(obj) < 1)
            //            {
            //                if (diagNose.CreatePatientDiagnose(obj) < 1)
            //                {
            //                    //trans.RollBack();
            //                    FS.FrameWork.Management.PublicTrans.RollBack();
            //                    MessageBox.Show("���������Ϣʧ��" + diagNose.Err);
            //                    return -1;
            //                }
            //            }
            //        }
            //        string result = diagNose.IsInfect(obj.DiagInfo.ICD10.ID);
            //        if (result == "Error")
            //            MessageBox.Show("��ѯ�����Ϣ����", "��ʾ");
            //        if (result == "1")
            //        {
            //            MessageBox.Show("���:" + obj.DiagInfo.ICD10.Name + "Ϊ��Ⱦ����ϣ�����д��Ⱦ�����濨!", "��ʾ");
            //        }
            //    }
            //}
            //if (diagAdd != null)
            //{
            //    foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in diagAdd)
            //    {
            //                            //{8BC09475-C1D9-4765-918B-299E21E04C74} ���¼������ҽ��վ������ҽ��վ������������
            //        if (enumdiaginput == EnumDiagInput.Cas)
            //        {
            //            if (diagNose.InsertDiagnose(obj) < 1)
            //            {
            //                //trans.RollBack();
            //                FS.FrameWork.Management.PublicTrans.RollBack();
            //                MessageBox.Show("���������Ϣʧ��" + diagNose.Err);
            //                return -1;
            //            }
            //        }
            //        else
            //        {
            //            obj.DiagInfo.HappenNo = diagNose.GetNewDignoseNo();
            //            if (obj.DiagInfo.HappenNo < 0)
            //            {
            //                FS.FrameWork.Management.PublicTrans.RollBack();
            //                MessageBox.Show("ȡ�����ˮ��ʧ��" + diagNose.Err);
            //                return -1;

            //            }

            //            if (diagNose.CreatePatientDiagnose(obj) < 1)
            //            {
            //                //trans.RollBack();
            //                FS.FrameWork.Management.PublicTrans.RollBack();
            //                MessageBox.Show("���������Ϣʧ��" + diagNose.Err);
            //                return -1;
            //            }
            //        }
            //        string result = diagNose.IsInfect(obj.DiagInfo.ICD10.ID);
            //        if (result == "Error")
            //            MessageBox.Show("��ѯ�����Ϣ����", "��ʾ");
            //        if (result == "1")
            //        {
            //            MessageBox.Show("���:" + obj.DiagInfo.ICD10.Name + "Ϊ��Ⱦ����ϣ�����д��Ⱦ�����濨!", "��ʾ");
            //        }
            //    }
            //}

            //this.ucDiagNoseInput1.fpEnterSaveChanges();
            
            ////trans.Commit();
            //FS.FrameWork.Management.PublicTrans.Commit();

            //this.ucDiagNoseInput1.ClearInfo();

            ////{8BC09475-C1D9-4765-918B-299E21E04C74} ���¼������ҽ��վ������ҽ��վ������������
            ////this.ucDiagNoseInput1.LoadInfo(patientInfo, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            //if (Enumdiaginput == EnumDiagInput.InpatientOrder || Enumdiaginput == EnumDiagInput.OutPatientOrder)
            //{
            //    this.ucDiagNoseInput1.LoadInfo(patientInfo, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, enumdiaginput.ToString());
            //}
            //else if (Enumdiaginput == EnumDiagInput.Cas)
            //{
            //    if (isList)
            //    {
            //        //this.ucDiagNoseInput1.LoadInfo(patientInfo, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, enumdiaginput.ToString());
            //        LoadInfo(patientInfo.ID);
            //    }
            //    else
            //    {
            //        this.ucDiagNoseInput1.LoadInfo(patientInfo, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS, enumdiaginput.ToString());
            //    }

            //}

            //MessageBox.Show("����ɹ�");

            //return 1;
            #endregion
        }

        protected override int OnSave(object sender, object neuObject)
        {
            return this.Save();
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (neuObject.GetType() == typeof(FS.HISFC.Models.RADT.PatientInfo))
            {
                this.patientInfo = neuObject as FS.HISFC.Models.RADT.PatientInfo;

                this.LoadInfo(this.patientInfo.ID);
            }
            //{8BC09475-C1D9-4765-918B-299E21E04C74} ���¼������ҽ��վ������ҽ��վ������������
            if (neuObject.GetType() == typeof(FS.HISFC.Models.Registration.Register))
            {
                FS.HISFC.Models.Registration.Register objReg = neuObject as FS.HISFC.Models.Registration.Register;

                this.LoadInfo(objReg.ID);
            }

            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            this.Save();
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            //if (this.Tag != null)
            //{
                this.ucDiagNoseInput1.AddBlankRow(); //����һ��
            //}
            //else
            //{
            //    //����һ��
            //    this.ucDiagNoseInput1.AddRow();
            //}
        }

        private void btnDel_Click(object sender, System.EventArgs e)
        {
            this.ucDiagNoseInput1.DeleteActiveRow();//ɾ��һ�� 
        }

        private void btnPrint_Click(object sender, System.EventArgs e)
        {
            //HealthRecord.CaseFirstPage.uccase uc = new ucCaseInputForClinic();
            //FS.neuFC.Interface.Classes.Function.PopShowControl(uc);
        }

        private void ucDocDiagNoseInput_Load(object sender, System.EventArgs e)
        {
            #region {6EF7D73B-4350-4790-B98C-C0BD0098516E}
            this.ucDiagNoseInput1.IsUseDeptICD = this.isUseDeptICD;
            #endregion
            this.InitInfo();

            this.ucDiagNoseInput1.AddRow();
            this.ucDiagNoseInput1.Tag = "AddNew";
        }
    }
}
