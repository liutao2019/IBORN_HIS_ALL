using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// [��������: ��������]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���='����'
    ///		�޸�ʱ��='2007-8-25'
    ///		�޸�Ŀ��='����Ƿ���Կ���ҽ�����Ƿ�Ժ����﹦��'
    ///		�޸�����='�Ի���ҽʦ�ܷ���ҽ�����п���'
    ///  />
    /// </summary>
    public partial class ucConsultation : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 
        /// </summary>
        public ucConsultation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ϊҽ���������û���ؼ��������Զ��ر�
        /// </summary>
        /// <param name="order"></param>
        public ucConsultation(FS.HISFC.Models.Order.Order order)
        {
            // �õ����� Windows.Forms ���������������ġ�
            InitializeComponent();

            if (order != null)
            {
                this.InpatientNo = order.Patient.ID;
                this.NewOne();

            }
            bSaveAndClose = true;
            // TODO: �� InitializeComponent ���ú�����κγ�ʼ��

        }

        #region ����

        FS.HISFC.BizLogic.Order.Consultation manager = new FS.HISFC.BizLogic.Order.Consultation();

        protected bool bSaveAndClose = false;

        protected bool IsSave = false;

        FS.HISFC.Models.RADT.PatientInfo patient = null;

        /// <summary>
        /// ����
        /// </summary>
        FS.HISFC.Models.Order.Consultation consultation = new FS.HISFC.Models.Order.Consultation();

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void init()
        {
            //this.NewOne();
            this.Clear();
            this.DisplayPatientInfo(this.patient);
            
            //ArrayList alDept = CacheManager.InterMgr.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.I);
            
            //alDept.AddRange(CacheManager.InterMgr.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.C));

            //alDept.AddRange(CacheManager.InterMgr.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.T));

            //deptHelper.ArrayObject = alDept;
           
            //personHelper.ArrayObject = CacheManager.InterMgr.QueryEmployeeAll();

            RefreshList();
        }

        private void RefreshList()
        {

            ArrayList al = null;
            try
            {
                al = manager.QueryConsulation(this.inpatientNo);//����б�
            }
            catch (Exception ex)
            {
                MessageBox.Show("RefreshList" + ex.Message); 
                return;
            }
            this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpSpread1_Sheet1.RowCount = 0;

            if (al == null || al.Count == 0) return;

            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Order.Consultation obj = al[i] as FS.HISFC.Models.Order.Consultation;
                    this.fpSpread1_Sheet1.Rows.Add(0, 1);
                    this.fpSpread1_Sheet1.Cells[0, 0].Value = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(obj.DeptConsultation.ID);//����
                    try
                    {
                        this.fpSpread1_Sheet1.Cells[0, 1].Value = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(obj.DoctorConsultation.ID);//��Ա
                    }
                    catch { }
                    if (this.fpSpread1_Sheet1.Cells[0, 0].Text == "")//����û�ҵ�
                    {
                        this.fpSpread1_Sheet1.Cells[0, 0].Value = obj.DeptConsultation.ID;//����
                    }
                    if (this.fpSpread1_Sheet1.Cells[0, 1].Text == "")//��Աû�ҵ�
                    {
                        this.fpSpread1_Sheet1.Cells[0, 1].Value = obj.DoctorConsultation.ID;//��Ա
                    }
                    this.fpSpread1_Sheet1.Cells[0, 2].Value = obj.Doctor.Name;//personHelper.GetName(obj.Doctor.ID);//������
                    fpSpread1_Sheet1.Cells[0, 3].Value = obj.ApplyTime;//��������

                    this.fpSpread1_Sheet1.Cells[0, 4].Value = obj.Name;//ԭ��
                    this.fpSpread1_Sheet1.Cells[0, 5].Value = obj.Result;//���
                    if (obj.State == 1)
                    {
                        this.fpSpread1_Sheet1.Cells[0, 6].Value = "����";//״̬
                    }
                    else if (obj.State == 2)
                    {
                        this.fpSpread1_Sheet1.Cells[0, 6].Value = "ȷ��";//״̬
                        this.fpSpread1_Sheet1.Rows[0].BackColor = Color.FromArgb(255, 225, 225);//ȷ��״̬
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[0, 6].Value = "δ֪" + obj.State.ToString();//״̬
                    }

                    if (obj.IsCreateOrder)
                    {
                        this.fpSpread1_Sheet1.Cells[0, 7].Value = "����ҽ��";
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[0, 7].Value = "������ҽ��";
                    }
                    if (obj.Type == FS.HISFC.Models.Order.EnumConsultationType.Hos)//Ժ�����
                    {
                        this.fpSpread1_Sheet1.Rows[0].BackColor = Color.FromArgb(255, 200, 200);//Ժ�����
                    }
                    this.fpSpread1_Sheet1.Cells[0, 8].Value = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(obj.DoctorConfirm.ID);//�����
                    this.fpSpread1_Sheet1.Rows[0].Tag = obj;
                }
            for (int i = 0; i < this.fpSpread1_Sheet1.Rows.Count; i++)
            {
                this.fpSpread1_Sheet1.RowHeader.Rows[i].Label = "����" + (i + 1).ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                this.cmbDept.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbDept_KeyPress);
                this.cmbDoctor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbDept_KeyPress);
                this.dtConsultation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbDept_KeyPress);
                this.dtBegin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbDept_KeyPress);
                this.dtEnd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbDept_KeyPress);
                this.dtPreConsultation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbDept_KeyPress);
                this.rtbSource.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbDept_KeyPress);
                txtSource.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbDept_KeyPress);

                this.ucUserText1.Visible = false;
                try
                {
                    this.cmbDept.SelectedIndexChanged += new EventHandler(cmbDept_SelectedIndexChanged);

                    ArrayList alDept = CacheManager.InterMgr.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.I);

                    alDept.AddRange(CacheManager.InterMgr.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.C));

                    alDept.AddRange(CacheManager.InterMgr.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.T));

                    alDept.AddRange(CacheManager.InterMgr.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.OP));

                    this.cmbDept.AddItems(alDept);

                    this.cmbAppDept.AddItems(cmbDept.alItems);

                    this.cmbAppDept.Text = ((FS.HISFC.Models.Base.Employee)(FS.FrameWork.Management.Connection.Operator)).Dept.Name;
                    this.cmbDoctor.AddItems(SOC.HISFC.BizProcess.Cache.Common.GetDept());

                    this.lblDoc.Text = FS.FrameWork.Management.Connection.Operator.Name;//����Ա
                    NewOne();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("OnLoad" + ex.Message);
                }
                try
                {
                    components = new Container();
                    components.Add(this.rtbSource);
                    components.Add(this.txtSource);
                    components.Add(this.rtbResult);
                    components.Add(txtResult);
                    this.ucUserText1.SetControl(this.components);
                    //{004900EE-5FD7-4f40-883E-B4E445795FB9}ҽԺ����
                    this.lblHosName.Text = this.manager.Hospital.Name;
                }
                catch { }
                base.OnLoad(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("OnLoad" + ex.Message);
            }
        }

        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        /// <param name="P"></param>
        public void DisplayPatientInfo(FS.HISFC.Models.RADT.PatientInfo P)
        {
            //Add By Zuowy
            if (P == null)
            {
                return;
            }

            this.label12.Text = P.Name;
            this.label14.Text = P.Sex.Name;
            this.label16.Text = P.Age;

            if (P.PVisit.PatientLocation.Bed.ID.Length > 3)
            {
                this.lblBedID.Text = P.PVisit.PatientLocation.Bed.ID.Substring(4) + "��";
            }
            else
            {
                this.lblBedID.Text = P.PVisit.PatientLocation.Bed.ID + "��";
            }
            this.neuLblInpaientNo.Text = P.PID.PatientNO.TrimStart('0');
            this.lblDept.Text = P.PVisit.PatientLocation.Dept.Name;
            this.cmbDept.Visible = true;
            this.cmbDoctor.Visible = true;

            List<FS.HISFC.Models.Order.Inpatient.Order> orderList = new List<FS.HISFC.Models.Order.Inpatient.Order>();
            ArrayList alOrder = CacheManager.InOrderMgr.QueryOrder(this.inpatientNo);
            foreach (FS.HISFC.Models.Order.Inpatient.Order orderObj in alOrder)
            {
                if (("0".Contains(orderObj.Status.ToString()) || "1".Contains(orderObj.Status.ToString())) && !orderObj.OrderType.IsDecompose//���������˵�
                    && "MC".Contains(orderObj.Item.SysClass.ID.ToString()))
                {
                    orderList.Add(orderObj);
                }
            }
            if (orderList.Count > 0)
            {
                cmbDept.Tag = orderList.OrderByDescending(t => t.MOTime).FirstOrDefault().ExeDept.ID;
            }
        }

        /// <summary>
        /// �½���һ��
        /// </summary>
        protected virtual void NewOne()
        {
            if (this.neuButton2.Text == "�޸�(&M)")
            {
                this.neuButton2.Text = "����(&S)";
            }
            FS.HISFC.Models.RADT.PatientInfo p = CacheManager.RadtIntegrate.GetPatientInfomation(this.inpatientNo);
            if (p == null) return;
            
            consultation = new FS.HISFC.Models.Order.Consultation();
            consultation.PatientNo = p.PID.PatientNO;
            consultation.InpatientNo = p.ID;
            //����Ĭ��������Һ�����ҽʦ
            consultation.Dept = p.PVisit.PatientLocation.Dept.Clone();
            consultation.Doctor.ID = FS.FrameWork.Management.Connection.Operator.ID;

            consultation.NurseStation = p.PVisit.PatientLocation.NurseCell.Clone();
            consultation.State = 1;//����

            this.dtPreConsultation.Value = manager.GetDateTimeFromSysDateTime();
            this.dtBegin.Value = this.dtPreConsultation.Value;  //Ĭ��ֵ
            this.dtEnd.Value = this.dtBegin.Value.AddDays(1);  //Ĭ��ֵ ���������ڶ�һ��
            this.chkOuthos.Checked = false;
            this.chkCreateOrder.Checked = true;
            //�¼ӵĳ�ʼ������
            this.cmbAppDept.Text = ((FS.HISFC.Models.Base.Employee)(FS.FrameWork.Management.Connection.Operator)).Dept.Name;
            this.lblDoc.Text = FS.FrameWork.Management.Connection.Operator.Name;

            if (this.IsSave == false)
            {
                this.rtbSource.Text = "�����ݰ�����Ҫ��ʷ�����ƾ�������������ﲡ���֢״���������������������������Լ������������ɺ�Ŀ�ĵȣ�";
                this.rtbResult.Text = "�����ݰ����������ר�Ʋ�ʷ����������������������ۣ��������ƽ���ȣ�";
                this.txtSource.Text = "�����ݰ�����Ҫ��ʷ�����ƾ�������������ﲡ���֢״���������������������������Լ������������ɺ�Ŀ�ĵȣ�";
                this.txtResult.Text = "�����ݰ����������ר�Ʋ�ʷ����������������������ۣ��������ƽ���ȣ�";

                this.cmbDept.Tag = "";
                this.cmbDept.Text = "";
                this.cmbDoctor.Tag = "";
                this.cmbDoctor.Text = "";
            }

        }

        protected virtual int Valid()
        {
            if (this.bIsApply)//����
            {
                if (this.inpatientNo == "" || this.patient == null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ѡ���ߣ�"));
                    return -1;
                }
                if (this.cmbDept.Tag == null || this.cmbDept.Tag.ToString() == "")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("����д������ң�"));
                    this.cmbDept.Focus();
                    return -1;
                }
                if (this.rtbSource.Text == "")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("����д����ժҪ��"));
                    this.rtbSource.Focus();
                    return -1;
                }
                if (this.txtSource.Text == "")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("����д����ժҪ��"));
                    this.txtSource.Focus();
                    return -1;
                }

                if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtSource.Text, 1000) == false)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ժҪ��д���ܳ���1000�ַ�!"), "��ʾ");
                    return -1;
                }
                if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtResult.Text, 1000) == false)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("���������д���ܳ���1000�ַ�!"), "��ʾ");
                    return -1;
                }
                if (this.dtEnd.Value < this.dtBegin.Value)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����������ڲ���С�ڴ�����ʼ����!"), "��ʾ");
                    return -1;
                }
                if (this.dtBegin.Value.ToShortDateString() == this.dtEnd.Value.ToShortDateString())
                {
                    DialogResult r = MessageBox.Show(FS.FrameWork.Management.Language.Msg("��Ȩ������������Ȩ��ʼ������ͬ��\n�Ƿ�ֻ�����ҽ���ڵ������Ȩ���"), "��ʾ", MessageBoxButtons.OKCancel);
                    if (r == DialogResult.Cancel)
                        return -1;
                }
            }
            else//ȷ��
            {
                if (this.cmbDoctor.Tag == null || this.cmbDoctor.Tag.ToString() == "")
                {
                    MessageBox.Show("����д����ר�ң�");
                    this.cmbDoctor.Focus();
                    return -1;
                }

                if (this.dtConsultation.Value == DateTime.MinValue)
                {
                    MessageBox.Show("����д�������ڣ�");
                    this.dtConsultation.Focus();
                    return -1;
                }
            }
            return 0;
        }

        /// <summary>
        /// �½����뵥ʱ,�ж���û����Ч�����뵥����
        /// </summary>
        public void Save()
        {
            ArrayList al = null;
            try
            {
                al = manager.QueryConsulation(this.inpatientNo);//����б�
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            if (Valid() == -1) return;
            //if (this.dtEnd.Value <= manager.GetDateTimeFromSysDateTime())
            //{
            //    MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ʧ��!�����뵥�Ѿ�ʧЧ!"), "��ʾ");
            //    return;
            //}
            if (this.bIsApply)
            {

                if (al != null || al.Count != 0)
                {
                    for (int i = 0; i < al.Count; i++)
                    {
                        FS.HISFC.Models.Order.Consultation obj = al[i] as FS.HISFC.Models.Order.Consultation;
                        //���������Ѿ����ڵ���Ч�����뵥��Ϣ,��,����ʧ��/��,����ɹ�
                        if ((obj.PatientNo == this.patient.PID.PatientNO) && (obj.Doctor.ID == FS.FrameWork.Management.Connection.Operator.ID) &&
                            (obj.DoctorConsultation.ID == this.cmbDoctor.Tag.ToString()) && (obj.DeptConsultation.ID == this.cmbDept.Tag.ToString()) && 
                            (this.dtPreConsultation.Value < obj.EndTime) && (obj.EndTime >= manager.GetDateTimeFromSysDateTime()))
                        {
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ʧ��!�Ѿ�������Ч�Ļ������뵥,\n�����ظ�����!"), "��ʾ");
                            return;
                        }
                    }
                }
                consultation.State = 1;
                consultation.Doctor.ID = FS.FrameWork.Management.Connection.Operator.ID;
                consultation.Doctor.Name = FS.FrameWork.Management.Connection.Operator.Name;
            }
            else
            {
                //if (al != null || al.Count != 0)
                //{
                //    for (int i = 0; i < al.Count; i++)
                //    {
                //        FS.HISFC.Models.Order.Consultation obj = al[i] as FS.HISFC.Models.Order.Consultation;
                //        if ((obj.PatientNo == this.inpatientNo.Substring(4, 10)) && (obj.Doctor.ID == FS.FrameWork.Management.Connection.Operator.ID) &&
                //            (obj.DoctorConsultation.ID == this.cmbDoctor.Tag.ToString()) &&
                //            (this.dtPreConsultation.Value < obj.EndTime) && (obj.EndTime >= manager.GetDateTimeFromSysDateTime()))
                //        {
                //            MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ʧ��!��ѡ����Ҫ��˵����뵥!"), "��ʾ");
                //            return;
                //        }
                //    }
                //}
                if (this.isAllowMultiCnslt == false && this.cmbDoctor.Tag.ToString() != FS.FrameWork.Management.Connection.Operator.ID)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ʧ��!����Ȩ�޸ĸû�����Ϣ"), "��ʾ");
                    return;
                }
                consultation.State = 2;
                consultation.DoctorConfirm.ID = FS.FrameWork.Management.Connection.Operator.ID;
                consultation.DoctorConfirm.Name = FS.FrameWork.Management.Connection.Operator.Name;
            }
            consultation.Name = this.txtSource.Text;
            consultation.Result = this.txtResult.Text;

            consultation.ConsultationTime = this.dtConsultation.Value;
            
            consultation.BeginTime = this.dtBegin.Value.Date;
            consultation.EndTime = new DateTime(this.dtEnd.Value.Year, this.dtEnd.Value.Month, this.dtEnd.Value.Day,
                 23, 59, 59);
            consultation.PreConsultationTime = this.dtPreConsultation.Value;
            consultation.DeptConsultation.ID = this.cmbDept.Tag.ToString();
            consultation.DeptConsultation.Name = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(consultation.DeptConsultation.ID);
            consultation.DoctorConsultation.ID = this.cmbDoctor.Tag.ToString();
            consultation.DoctorConsultation.Name = this.cmbDoctor.Text;
            consultation.ApplyTime = this.dtPreConsultation.Value;

            if (this.chkCreateOrder.Checked)
            {
                consultation.IsCreateOrder = true;
            }
            else
            {
                consultation.IsCreateOrder = false;
            }

            if (this.chkEmergency.Checked)
            {
                consultation.IsEmergency = true;
            }
            else
            {
                consultation.IsEmergency = false;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(manager.Connection);
            //t.BeginTransaction();
            manager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            if (consultation.ID == "")
            {
                if (manager.InsertConsultation(consultation) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                    MessageBox.Show(manager.Err);
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.Commit();
                    this.IsSave = true;
                    init();
                    MessageBox.Show("����ɹ�!");
                    this.Clear();//�����������ղ���
                    if (bSaveAndClose)
                        this.FindForm().Close();
                }
            }
            else
            {
                if (manager.UpdateConsultation(consultation) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                    MessageBox.Show(manager.Err);
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.Commit();
                    this.IsSave = true;
                    init();
                    MessageBox.Show("����ɹ�!");
                    this.Clear();
                    if (bSaveAndClose)
                        this.FindForm().Close();
                }
            }
            this.RefreshList();
        }

        /// <summary>
        /// [��������: ��ղ���]<br></br>
        /// [�� �� ��: ]<br></br>
        /// [����ʱ��: ]<br></br>
        /// <�޸ļ�¼
        ///		�޸���='����'
        ///		�޸�ʱ��='2007-8-25'
        ///		�޸�Ŀ��=''
        ///		�޸�����=''
        ///  />
        /// </summary>
        public void Clear()
        {
            if (this.neuButton2.Text == "�޸�(&M)")
            {
                this.neuButton2.Text = "����(&S)";
            }
            this.IsSave = false;
            this.NewOne();
            //this.label12.Text = "δ֪";
            //this.label14.Text = "δ֪";
            //this.label16.Text = "δ֪";
            //this.lblBedID.Text = "δ֪";
            //this.cmbAppDept.Tag = "";
            //this.cmbDept.Tag = "";

            //this.lblBedNo0.Text = "";
            //this.lblConDept.Text = "";
            //this.lblConDoc.Text = "";
            //this.lblName.Text = "";
            //this.lblDept.Text = "";
            //this.lblPatientNo0.Text = "";

            //������
            this.cmbDept.Tag = "";
            this.cmbDoctor.Tag = "";
            this.dtPreConsultation.Value = manager.GetDateTimeFromSysDateTime();
            this.dtBegin.Value = this.dtPreConsultation.Value;  //Ĭ��ֵ
            this.dtEnd.Value = this.dtBegin.Value.AddDays(1);  //Ĭ��ֵ ���������ڶ�һ��
            this.chkOuthos.Checked = false;
            this.chkCreateOrder.Checked = true;

            this.cmbAppDept.Text = ((FS.HISFC.Models.Base.Employee)(FS.FrameWork.Management.Connection.Operator)).Dept.Name;
            this.lblDoc.Text = FS.FrameWork.Management.Connection.Operator.Name;
        }
        /// <summary>
        /// ����Ч�Ļ������뵥�����޸ĺ���˱���
        /// </summary>
        public void SaveUpdate()
        {
            if (Valid() == -1) return;
            if (this.dtEnd.Value <= manager.GetDateTimeFromSysDateTime())
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�޸�ʧ��!�����뵥�Ѿ�ʧЧ!"), "��ʾ");
                this.neuButton2.Text = "����(&S)";
                return;
            }
            else
            {
                if (this.bIsApply)
                {

                    consultation.State = 1;
                    consultation.Doctor.ID = FS.FrameWork.Management.Connection.Operator.ID;
                    consultation.Doctor.Name = FS.FrameWork.Management.Connection.Operator.Name;
                }
                else
                {
                    if (this.isAllowMultiCnslt == false && this.cmbDoctor.Tag.ToString() != FS.FrameWork.Management.Connection.Operator.ID)
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("�޸�ʧ��!����Ȩ�޸ĸû�����Ϣ"), "��ʾ");
                        this.neuButton2.Text = "�޸�(&M)";
                        return;
                    }
                    if (this.cmbDept.Tag.ToString() != ((FS.HISFC.Models.Base.Employee)CacheManager.InOrderMgr.Operator).Dept.ID)
                    {
                        
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("�޸�ʧ��!����Ȩ�޸ĸû�����Ϣ"), "��ʾ");
                        this.neuButton2.Text = "�޸�(&M)";
                        return;
                    }
                    consultation.State = 2;
                    consultation.DoctorConfirm.ID = FS.FrameWork.Management.Connection.Operator.ID;
                    consultation.DoctorConfirm.Name = FS.FrameWork.Management.Connection.Operator.Name;
                }
                consultation.Name = this.txtSource.Text;
                consultation.Result = this.txtResult.Text;
                consultation.ConsultationTime = this.dtConsultation.Value;
                consultation.BeginTime = this.dtBegin.Value.Date;
                consultation.EndTime = new DateTime(this.dtEnd.Value.Year, this.dtEnd.Value.Month, this.dtEnd.Value.Day,
                     23, 59, 59);
                consultation.PreConsultationTime = this.dtPreConsultation.Value;
                consultation.DeptConsultation.ID = this.cmbDept.Tag.ToString();
                consultation.DeptConsultation.Name = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(consultation.DeptConsultation.ID);
                consultation.DoctorConsultation.ID = this.cmbDoctor.Tag.ToString();
                consultation.ApplyTime = manager.GetDateTimeFromSysDateTime();

                if (this.chkCreateOrder.Checked)
                {
                    consultation.IsCreateOrder = true;
                }
                else
                {
                    consultation.IsCreateOrder = false;
                }

                if (this.chkEmergency.Checked)
                {
                    consultation.IsEmergency = true;
                }
                else
                {
                    consultation.IsEmergency = false;
                }

            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(manager.Connection);
            //t.BeginTransaction();
            manager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            if (consultation.ID == "")
            {
                if (manager.InsertConsultation(consultation) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                    MessageBox.Show(manager.Err);
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.Commit();
                    this.IsSave = true;
                    init();
                    MessageBox.Show("�޸ĳɹ�!");
                    this.neuButton2.Text = "����(&S)";
                    this.Clear();
                    if (bSaveAndClose)
                        this.FindForm().Close();
                }
            }
            else
            {
                if (manager.UpdateConsultation(consultation) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                    MessageBox.Show(manager.Err);
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.Commit();
                    this.IsSave = true;
                    init();
                    MessageBox.Show("�޸ĳɹ�!");
                    this.neuButton2.Text = "����(&S)";
                    this.Clear();
                    if (bSaveAndClose)
                        this.FindForm().Close();
                }
            }
            this.RefreshList();
        }

        private void Print()
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            
            //���ػ�������
            if (this.dtConsultation.Checked == false)
            {
                this.dtConsultation.Visible = false;
            } 

            this.rtbSource.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            
            this.panelFun.BorderStyle = BorderStyle.None;

            p.PrintPreview(20, 5, this.panelFun);

            this.txtSource.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;

            this.dtConsultation.Visible = true;
        }

        private void SetValue()
        {
            if (this.fpSpread1_Sheet1.ActiveRow.Tag == null) return;
            this.DisplayPatientInfo(this.patient);
            consultation = this.fpSpread1_Sheet1.ActiveRow.Tag as FS.HISFC.Models.Order.Consultation;
            
            if (this.consultation.Type == FS.HISFC.Models.Order.EnumConsultationType.Hos)
            {
                #region Ժ�����
                //frmConsultationOuthos f = new frmConsultationOuthos();
                //f.Init(consultation);
                //f.ShowDialog();
                //this.init();
                #endregion
            }
            else
            {
                #region Ժ�ڻ���
                this.bIsApply = this.bisApply;
                this.txtSource.Text = consultation.Name;
                this.txtResult.Text = consultation.Result;
                
                this.dtConsultation.Value = consultation.ConsultationTime;
                this.dtConsultation.Checked = false;

                this.dtBegin.Value = consultation.BeginTime;
                this.dtEnd.Value = consultation.EndTime;
                this.dtPreConsultation.Value = consultation.PreConsultationTime;
                this.cmbDept.Tag = consultation.DeptConsultation.ID;

                consultation.DeptConsultation.Name = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(consultation.DeptConsultation.ID);

                //#region  2009-07-22 ����������û��ָ������ҽ��ʱ����д���������ҽ��Ĭ��Ϊ����ҽ�� {53F962A7-44DC-4607-A240-5B21A1AC6E14} By Chenfan
                //#region  2012-09-27 ����������û��ָ������ҽ��ʱ����ʾΪ��
                //#region  2012-09-28 ����������û��ָ������ҽ��ʱ����ʾΪ��ǰ��¼��ҽ��
                #region  2012-10-24 ����������û��ָ������ҽ��ʱ��ʾΪ�ա�����ʱ�Զ����ϵ�ǰ��¼ҽ��
                if (consultation.DoctorConsultation.ID == null || consultation.DoctorConsultation.ID == string.Empty)
                {
                    if (this.txtResult.Enabled == false)//�������
                    {
                        this.cmbDoctor.Text = "";
                    }
                    else  //���ڻ���
                    {
                        this.cmbDoctor.Tag = FS.FrameWork.Management.Connection.Operator.ID;
                        consultation.DoctorConsultation.Name = ((FS.HISFC.Models.Base.Employee)(FS.FrameWork.Management.Connection.Operator)).Name;
                        this.cmbDoctor.Text = consultation.DoctorConsultation.Name;
                        this.dtConsultation.Enabled = true;
                        this.dtConsultation.Checked = false;
                    }
                }
                else //�Ѿ�ȷ��
                {
                    consultation.DoctorConsultation.Name = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(consultation.DoctorConsultation.ID);
                    this.cmbDoctor.Tag = consultation.DoctorConsultation.ID;
                    this.cmbDoctor.Text = consultation.DoctorConsultation.Name;
                    this.dtConsultation.Enabled = true;
                    this.dtConsultation.Checked = false;
                }
                #endregion
                
                this.cmbAppDept.Tag = consultation.Dept.ID;
                consultation.Dept.Name = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(consultation.Dept.ID);
                this.lblDoc.Text = consultation.Doctor.Name;
                if (consultation.IsCreateOrder)
                {
                    this.chkCreateOrder.Checked = true;
                    this.chkOuthos.Checked = false;
                }
                else
                {
                    this.chkCreateOrder.Checked = false;
                }
                if (consultation.IsEmergency)
                {
                    this.chkEmergency.Checked = true;
                    this.chkCommon.Checked = false;
                }
                else
                {
                    this.chkEmergency.Checked = false;
                    this.chkCommon.Checked = true;
                }
                if (this.bIsApply)
                {

                }
                else
                {
                    //this.cmbApplyDoctor.Text = consultation.DoctorConfirm.Name;

                }
                this.neuButton2.Text = "�޸�(&M)";
                #endregion
            }

        }


        #endregion

        #region ����

        protected string inpatientNo;

        /// <summary>
        /// סԺ��ˮ��
        /// </summary>
        public string InpatientNo
        {
            set
            {
                if (value == null || value == "") return;
                this.inpatientNo = value;
                patient = CacheManager.RadtIntegrate.GetPatientInfomation(this.inpatientNo);
                init();
            }
        }

        protected bool bisApply = true;
        /// <summary>
        /// ���뵥״̬
        /// </summary>
        protected bool bIsApply
        {
            get
            {
                return this.bisApply;
            }
            set
            {
                this.bisApply = value;
                if (consultation.State == 1)
                    this.lblState.Text = "״̬���������";
                else
                    this.lblState.Text = "״̬���Ѿ�ȷ��";

                this.dtPreConsultation.Enabled = value;
                this.cmbDept.Enabled = value;
                this.cmbDoctor.Enabled = value;
                this.chkEmergency.Enabled = value;
                this.txtSource.Enabled = value;
                this.dtConsultation.Enabled = true;
                this.dtBegin.Enabled = value;
                this.dtEnd.Enabled = value;
                this.cmbAppDept.Enabled = value;
                this.dtConsultation.Enabled = value;
                this.chkCommon.Enabled = value;
                this.chkCreateOrder.Enabled = value;
                this.chkOuthos.Enabled = value;
                this.txtResult.Enabled = !value;
                this.neuButton1.Enabled = value;
            }
        }

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        [Browsable(false)]
        public bool IsApply
        {
            get
            {
                return this.bIsApply;
            }
            set
            {
                this.bIsApply = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        [Description("��ʾ�ı���")]
        public string Title
        {
            set
            {
                this.lblTitle.Text = value;
            }
            get
            {
                return this.lblTitle.Text;
            }
        }

        //��ҽ������
        private bool isAllowMultiCnslt = true;

        [Description("�Ƿ������ҽ������(Ĭ������)")]
        public bool IsAllowMultiCnslt
        {
            set
            {
                this.isAllowMultiCnslt = value;
            }
            get
            {
                return this.isAllowMultiCnslt;
            }
        }


        #endregion

        #region �¼�
        
        //���ұ仯������Ա���ű仯
        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.cmbDoctor.alItems = null;
                this.cmbDoctor.Text = "";
                this.cmbDoctor.Tag = "";
                ArrayList emps = CacheManager.InterMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D ,this.cmbDept.Tag.ToString());
                if (emps == null) emps = new ArrayList();
                this.cmbDoctor.AddItems(emps);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnClear_Click(object sender, System.EventArgs e)
        {
            this.Clear();
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            if (cmbAppDept.Text == cmbDept.Text)
            {
                MessageBox.Show("����ҽ������������������Ҳ�����ͬ,������ѡ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.neuButton2.Text== "�޸�(&M)")
            {
                this.SaveUpdate();
            }
            else
            {
                this.Save();
            }

        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            try
            {
                SetValue();
            }
            catch { }
        }
        
        private void fpSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            try
            {
                SetValue();
            }
            catch { }
        }

        private void cmbDept_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{tab}");
                e.Handled = true;
            }
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            if (this.neuButton2.Text == "�޸�(&M)")
            {
                this.neuButton2.Text = "����(&S)";
            }
            if (consultation == null || consultation.ID == "")
            {
                MessageBox.Show("��ѡ��һ�����ﵥ�ٽ���ɾ��������");
                return;
            }
            if (consultation.ID == "")
            {
                this.NewOne();
            }
            else if (consultation.State == 2)
            {
                MessageBox.Show("�û����Ѿ���Ч�޷�ɾ����");
            }
            else
            {
                //.
                if (consultation.Doctor.ID != FS.FrameWork.Management.Connection.Operator.ID)
                {
                    MessageBox.Show("�����ǻ��������ˣ�����ɾ�������룡");
                    return;
                }
                else
                {
                    //.
                    if (MessageBox.Show("ȷ��Ҫɾ���û����¼��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        FS.FrameWork.Management.PublicTrans.BeginTransaction();
                        //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(manager.Connection);
                        //t.BeginTransaction();
                        manager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        if (manager.DeleteConsulation(consultation.ID) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show(manager.Err);
                        }
                        FS.FrameWork.Management.PublicTrans.Commit();
                        MessageBox.Show("ɾ���ɹ�!");
                    }
                    else
                    {
                        return;
                    }
                }
            }
            this.init();
        }
        /// <summary>
        /// ��ӡ �޸� by zuowy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, System.EventArgs e)
        {
            //
           
            try
            { 
                SetValue();
            }
            catch { }
            Print();
            if (this.neuButton2.Text == "�޸�(&M)")
            {
                this.neuButton2.Text = "����(&S)";
            }
           
        }

        private void chkTemplet_CheckedChanged(object sender, System.EventArgs e)
        {
            if (chkTemplet.Checked == true)
                ucUserText1.Visible = true;
            else
                ucUserText1.Visible = false;
        }

        private void chkEmergency_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.chkEmergency.Checked == true)
                this.chkCommon.Checked = false;
            else
                this.chkCommon.Checked = true;
        }

        private void chkCommon_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.chkCommon.Checked == true)
                this.chkEmergency.Checked = false;
            else
                this.chkEmergency.Checked = true;
        }

        private void chkOuthos_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkOuthos.Checked == true)
                this.chkCreateOrder.Checked = false;
        }


        private void chkCreateOrder_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkCreateOrder.Checked == true)
                this.chkOuthos.Checked = false;
        }

        #endregion

        #region ��д����

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            try
            {
                this.init();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return base.OnInit(sender, neuObject, param);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="neuObject"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            FS.FrameWork.Models.NeuObject obj = neuObject as FS.FrameWork.Models.NeuObject;
            if(obj == null) return -1;
            if (e.Parent.Tag.ToString() == "ConsultationPatient")
            {
                this.bIsApply = false;
            }
            else
            {
                this.bIsApply = true;
            }

            this.InpatientNo = obj.ID;
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            this.Print();
            return 0;
        }

        #endregion

        #region ��Ժ��ӡ���ﵥ

        private void ucQueryInpatientNO_myEvent()
        {
            this.Clear();
            if (this.ucQueryInpatientNO.InpatientNo == null || this.ucQueryInpatientNO.InpatientNo.Trim() == string.Empty)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("û�и�סԺ��,����֤������!") + "\r\n" + this.ucQueryInpatientNO.Err);
                this.ucQueryInpatientNO.Focus();
                this.ucQueryInpatientNO.TextBox.SelectAll();

                return;
            }

            this.IsApply = false;
            this.InpatientNo = ucQueryInpatientNO.InpatientNo;
        }

        private void cbxOutPrint_CheckedChanged(object sender, EventArgs e)
        {
            this.ucQueryInpatientNO.Enabled = this.cbxOutPrint.Checked;
            this.lblPatientInfo.Enabled = this.cbxOutPrint.Checked;
        }

        #endregion

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PrintNotice_Click(object sender, EventArgs e)
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            
            if (this.patient == null)
            {
                 MessageBox.Show("û��ѡ���ߡ�");
                 return;
            }

            ucConsultationNotice cn  = new ucConsultationNotice();

            if (this.dtConsultation.Checked == false)
            {
                this.consultation.Memo = "no";
            }

            cn.SetValues(this.patient, this.consultation);
            
            p.PrintPreview(20, 5, cn);
        }

    }
}
