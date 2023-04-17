using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.Nurse.Controls.SDFY
{
    /// <summary>
    /// [��������: Ӥ���Ǽ����]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2006-11-30]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucBabyInfo : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// ����
        /// </summary>
        public ucBabyInfo()
        {
            InitializeComponent();
        }

        #region ����
        BabyManager babyManager = new BabyManager();
        FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();
        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.Models.RADT.PatientInfo MomInfo = null;
        FS.HISFC.Models.RADT.PatientInfo BabyInfo = null;
        bool isNew = false;
        private string InpatientNo = "";

        #region {FEA519C4-2379-40a9-8271-829A76E04EF6}

        private int babyNo = 0;

        #endregion

        #endregion

        #region ����
        [Category("����"), Description("�Ƿ����¼��סԺ��"), DefaultValue(false)]
        public bool IsinputEnable
        {//{3E2B1A30-6689-4ea7-9946-DC60E1886D4E} Ӥ���Ǽǿ���ͨ���úŵ���ʹ�� 20100919
            get
            {
                return this.gpbPatientNO.Visible;
            }
            set
            {
                this.gpbPatientNO.Visible = value;
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        private void InitControl()
        {
            this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());				//ȡ�Ա��б�
            this.cmbBlood.AddItems(FS.HISFC.Models.RADT.BloodTypeEnumService.List());	//ȡѪ���б�
            this.cmbBlood.IsListOnly = true;
            this.cmbSex.IsListOnly= true;
            this.dtBirthday.Value = this.inpatientManager.GetDateTimeFromSysDateTime();		//Ĭ��Ӥ����������Ϊϵͳʱ��
            this.dtOperatedate.Value = dtBirthday.Value;	//Ĭ�ϲ�������Ϊϵͳʱ��
            ClearInfo();
            this.ucQueryInpatientNo.Focus();
        }
 
        /// <summary>
        /// ���û�����Ϣ���ؼ�
        /// </summary>
        /// <param name="patientInfo"></param>
        private void ShowBabyInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            //���û�л�����Ϣ,������
            if (patientInfo == null)
            {
                this.ClearInfo();
                return;
            }
            this.txtInpatientNo.Text = patientInfo.PID.ID;	//סԺ��
            this.txtInpatientNo.Tag = patientInfo.User01;			//�������
            this.txtName.Text = patientInfo.Name;					//����
            this.cmbSex.SelectedIndexChanged -= new EventHandler(this.cmbSex_SelectedIndexChanged);
            this.cmbSex.Tag = patientInfo.Sex.ID;			//�Ա�
            this.cmbSex.SelectedIndexChanged += new EventHandler(this.cmbSex_SelectedIndexChanged);
            this.cmbBlood.Tag = patientInfo.BloodType.ID;	//Ѫ��
            this.txtHeight.Text = patientInfo.Height;		//����
            this.txtWeight.Text = patientInfo.Weight;			//����
            this.dtBirthday.Value = patientInfo.Birthday;		//��������
            this.dtOperatedate.Text = patientInfo.User03;			//�Ǽ�����
            this.InpatientNo = patientInfo.ID; //סԺ��ˮ��

            if (patientInfo.PVisit.User01 == "1")   //ĸ���Ƿ��Ҹο�ԭ����
            { this.neuCkbIsMomHBAP.Checked = true; }
            else
            { this.neuCkbIsMomHBAP.Checked = false; }
            if (patientInfo.PVisit.User02 == "1")   //Ӥ���Ƿ��Ѿ�ע���Ч�����򵰰�
            { this.neuCkbIsImmu.Checked = true; }
            else
            { this.neuCkbIsImmu.Checked = false; }
        }

        /// <summary>
        /// ��տؼ�
        /// </summary>
        private void ClearInfo()
        {
            
            this.txtInpatientNo.Text = "";	//�������
            this.txtInpatientNo.Tag = "";	//
            this.txtName.Text = "";		//����
            //this.cmbSex.SelectedIndexChanged -= new EventHandler(this.cmbSex_SelectedIndexChanged);
            //this.cmbSex.Tag = "M";		//Ĭ���Ա�Ϊ����
            //this.cmbSex.SelectedIndexChanged += new EventHandler(this.cmbSex_SelectedIndexChanged);
            this.cmbBlood.Tag = "";	//Ѫ��Ϊ��
            this.cmbBlood.Text = "";	//Ѫ��Ϊ��
        
            try
            {
                this.cmbDept.Text = this.MomInfo.PVisit.PatientLocation.Dept.Name;	//Ӥ������=ĸ�׿���
                this.cmbDept.Tag = this.MomInfo.PVisit.PatientLocation.Dept.ID;	//Ӥ������=ĸ�׿���
                
            }
            catch { }
            this.txtHeight.Text = "";   //����
            this.txtWeight.Text = "";		//����
            this.dtBirthday.Value = this.inpatientManager.GetDateTimeFromSysDateTime();		//��������Ĭ�ϵ���
            this.dtOperatedate.Value = this.inpatientManager.GetDateTimeFromSysDateTime();	//����ʱ��

            this.neuCkbIsMomHBAP.Checked = false; //ĸ���Ƿ��Ҹο�ԭ����
            this.neuCkbIsImmu.Checked = false;    //Ӥ���Ƿ��Ѿ�ע���Ч�����򵰰�

            this.BabyInfo = null;
                        
        }


        /// <summary>
        /// ȡӤ����Ϣ
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.RADT.PatientInfo GetBabyInfo()
        {			//�������
            if (this.BabyInfo == null)
            {
                //���������Ӥ����û����������,������Ӥ������,��ȡ��Ӥ�������
                this.BabyInfo = new FS.HISFC.Models.RADT.PatientInfo();
                //���Ҵ�λ��Ϣ
                this.BabyInfo.PVisit.PatientLocation = this.MomInfo.PVisit.PatientLocation.Clone();
                //ȡ�ؼ���סԺҽ��
                this.BabyInfo.PVisit.AdmittingDoctor = this.MomInfo.PVisit.AdmittingDoctor.Clone();
                //ȡ�ؼ�������ҽ��
                this.BabyInfo.PVisit.AttendingDoctor = this.MomInfo.PVisit.AttendingDoctor.Clone();
                //ȡ�ؼ�������ҽ��
                this.BabyInfo.PVisit.ConsultingDoctor = this.MomInfo.PVisit.ConsultingDoctor.Clone();
                //ȡ�ؼ������λ�ʿ
                this.BabyInfo.PVisit.AdmittingNurse = this.MomInfo.PVisit.AdmittingNurse.Clone();
                //��Ժ���
                this.BabyInfo.PVisit.InSource = this.MomInfo.PVisit.InSource.Clone();
                //��Ժ;��
                this.BabyInfo.PVisit.Circs = this.MomInfo.PVisit.Circs.Clone();
                //��Ժ��Դ
                this.BabyInfo.PVisit.AdmitSource = this.MomInfo.PVisit.AdmitSource.Clone();


                //��Ӥ��
                isNew = true;

                //ȡӤ��������
                string happenNo = this.txtInpatientNo.Tag.ToString();
                happenNo = this.inpatientManager.GetMaxBabyNO(this.MomInfo.ID);
                if (happenNo == "-1")
                {
                    MessageBox.Show("ȡӤ��������ʱ����:" + this.inpatientManager.Err, "������ʾ");
                    return null;
                }

                //��1�õ���ǰӤ�����
                happenNo = (FS.FrameWork.Function.NConvert.ToInt32(happenNo) + 1).ToString();

                this.BabyInfo.User01 = happenNo; //��User01������Ӥ�����
                //ȡӤ����
                if (this.txtName.Text == "")
                {
                    //����Ŀǰ���ֺ�Ӥ���Ա�����Ӥ������
                    this.BabyInfo.Name = CreatBabyName(this.MomInfo.Name, this.cmbSex.Tag.ToString(), int.Parse(happenNo));
                }
                else
                {
                    this.BabyInfo.Name = this.txtName.Text;
                }


                //��Ժ���ڵ���ϵͳ��ǰʱ��
                this.BabyInfo.PVisit.InTime = this.inpatientManager.GetDateTimeFromSysDateTime();

                //����סԺ��
                this.BabyInfo.PID.ID = "B" + happenNo + MomInfo.PID.PatientNO.Substring(2);

                //����סԺ��ˮ��
                //this.BabyInfo.ID = MomInfo.ID.Substring(0, 4) + "B" + happenNo + MomInfo.PID.PatientNO.Substring(2);
                this.BabyInfo.ID = this.inpatientManager.GetNewInpatientNO();
                //�������￨��
                this.BabyInfo.PID.CardNO = "TB" + happenNo + MomInfo.PID.PatientNO.Substring(3);

                this.BabyInfo.Pact.PayKind.ID = "01";			//�Է�
                this.BabyInfo.Pact.ID = "1";		//�Է�
                this.BabyInfo.Pact.Name = "�ԷѶ�ͯ";//�ԷѶ�ͯ
                this.BabyInfo.PVisit.InState.ID = "R";		//��Ժ�Ǽ�
            }
            else
            {
                //�޸�Ӥ����Ϣ
                isNew = false;
                //������ѵǼǵ�Ӥ��,��ʾ�û�����Ӥ������
                if (this.txtName.Text == "")
                {
                    MessageBox.Show("������Ӥ������", "��ʾ");
                    return null;
                }

                this.BabyInfo.Name = this.txtName.Text;									//Ӥ������
            }


            this.BabyInfo.Sex.ID = this.cmbSex.Tag.ToString();				//�Ա�
            this.BabyInfo.BloodType.ID = this.cmbBlood.Tag.ToString();	//Ѫ��
            this.BabyInfo.Height = this.txtHeight.Text;						//����
            this.BabyInfo.Weight = this.txtWeight.Text;						//����
            this.BabyInfo.Birthday = this.dtBirthday.Value;					//��������

            //�ж������ĳ���
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.BabyInfo.Name, 20) == false)
            {
                txtName.Text = this.BabyInfo.Name;
                MessageBox.Show("���ƹ������������룡");
                return null;
            }

            //�����סԺ��ˮ��
            this.BabyInfo.PID.MotherInpatientNO = this.MomInfo.ID;

            return this.BabyInfo;
        }

        int BabyNum = 0;
        /// <summary>
        /// ����Ӥ������
        /// </summary>
        /// <param name="MumName">��������</param>
        /// <param name="SexId">�Ա�</param>
        /// <param name="HappenNo">�������</param>
        /// <returns></returns>
        private string CreatBabyName(string MumName, string SexId, int HappenNo)
        {
            string BabyName;
            //{FEA519C4-2379-40a9-8271-829A76E04EF6}��������
            //BabyNum++;

            switch (HappenNo)
            {
                case 1:
                    BabyName = "��";
                    break;
                case 2:
                    BabyName = "��";
                    break;
                case 3:
                    BabyName = "��";
                    break;
                case 4:
                    BabyName = "��";
                    break;
                default:
                    BabyName = "";
                    break;
            }

            #region {FEA519C4-2379-40a9-8271-829A76E04EF6}

            if (SexId == FS.HISFC.Models.Base.EnumSex.M.ToString())
            {

                return MumName + "֮" + BabyName + "��";
            }
            else
            {
                return MumName + "֮" + BabyName + "Ů";
            }

            #endregion
        }


        /// <summary>
        /// ˢ������
        /// </summary>
        /// <param name="inPatientNo"></param>
        public virtual void RefreshList(string inPatientNo)
        {
            //����ؼ�
            ClearInfo();
            //��ʾӤ���б�
            ShowTreeView();
        }


        /// <summary>
        /// ��ʾӤ���б�
        /// </summary>
        private void ShowTreeView()
        {
            ArrayList al;
            al = new ArrayList();

            //���ڵ�
            al.Add("Ӥ���б�");

            //ȡӤ���б�
            al = this.inpatientManager.QueryBabiesByMother(this.MomInfo.ID);
            if (al == null)
            {
                MessageBox.Show(inpatientManager.Err);
                return;
            }

            //����Ӥ�����Ա�,����ÿ���Ա��������.��ȥӤ����סԺ�����е���Ϣ
          
            BabyNum = 0;
            foreach (FS.HISFC.Models.RADT.PatientInfo baby in al)
            {
               
                BabyNum++;

              
            }


            //��ʾ�����Ϳؼ���
            this.tvPatientList1.BeginUpdate();
            this.tvPatientList1.SetPatient( al);
            this.tvPatientList1.EndUpdate();

            //չ���ڵ�,��ʾ���ڵ�
            this.tvPatientList1.ExpandAll();
            this.tvPatientList1.SelectedNode = this.tvPatientList1.Nodes[0];

        }

        #endregion

        #region �¼�
        private void btAdd_Click(object sender, System.EventArgs e)
        {
            this.ClearInfo();
            #region {FEA519C4-2379-40a9-8271-829A76E04EF6}
            
            this.babyNo = this.BabyNum + 1;

            this.txtName.Text = this.CreatBabyName(this.MomInfo.Name, this.cmbSex.Tag.ToString(), babyNo);

            #endregion
        }


        private void btCancel_Click(object sender, System.EventArgs e)
        {
            if (this.BabyInfo == null || this.txtInpatientNo.Text == "")
            {
                MessageBox.Show("��ѡ��Ԥȡ����Ӥ����", "��ʾ");
                return;
            }

            try
            {
                //string sPatientNo = this.MomInfo.PID.PatientNO;
                //sPatientNo = "B" + this.txtInpatientNo.Tag.ToString() + sPatientNo.Substring(2);
                //sPatientNo = MomInfo.ID.Substring(0, 4) + sPatientNo;
                string sPatientNo = this.BabyInfo.ID;

                FS.HISFC.Models.RADT.PatientInfo p = this.inpatientManager.QueryPatientInfoByInpatientNO(sPatientNo);
                if ((p.FT.TotCost + p.FT.BalancedCost) > 0)
                {
                    MessageBox.Show("��Ӥ���Ѿ��������ã�����ȡ����");
                    return;
                }

                #region {23EE5EA6-27CB-49c9-810A-310A1515D85E}
                if (p.FT.PrepayCost > 0)
                {
                    MessageBox.Show("��Ӥ��Ԥ��������0������ȡ����");
                    return;
                }
                #endregion

                //ȡ��Ӥ��

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(this.inpatientManager.Connection);
                //t.BeginTransaction();

                this.inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if (this.inpatientManager.DiscardBabyRegister(this.BabyInfo.ID) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("ȡ��ʧ�ܣ�" + this.inpatientManager.Err);
                }
                else
                {
                    FS.HISFC.Models.RADT.InStateEnumService status = new FS.HISFC.Models.RADT.InStateEnumService();
                    status.ID = "C";
                    p.ID = sPatientNo;
                    if (this.inpatientManager.UpdatePatientStatus(p,status) == -1)
                    {//����ΪסԺ
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("ȡ���Ǽ�ʧ�ܣ�" + this.inpatientManager.Err);
                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.Commit();
                        //����Ӥ�����ڵĽڵ�,��ɾ���˽ڵ�
                        if (this.tv != null)
                        {//{3E2B1A30-6689-4ea7-9946-DC60E1886D4E} Ӥ���Ǽǿ���ͨ���úŵ���ʹ�� 20100919
                            TreeNode node = this.tv.FindNode(0, this.BabyInfo);
                            if (node != null) node.Remove();
                        }

                        //ˢ��Ӥ���б�
                        RefreshList(this.MomInfo.ID);
                        this.BabyInfo = null;
                        MessageBox.Show("ȡ���Ǽǳɹ���", "��ʾ");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private FS.HISFC.Components.Common.Controls.tvPatientList tv = null;

        private void btSave_Click(object sender, System.EventArgs e)
        {
            //ȡ�������������µ���Ϣ,�����жϲ���
            FS.HISFC.Models.RADT.PatientInfo patient = this.inpatientManager.QueryPatientInfoByInpatientNO(this.MomInfo.ID);
            if (patient == null)
            {
                MessageBox.Show(this.inpatientManager.Err);
                return;
            }
            //��������Ѳ�����Ժ״̬,����������
            if (patient.PVisit.InState.ID.ToString() != this.MomInfo.PVisit.InState.ID.ToString())
            {
                MessageBox.Show("������Ϣ�ѷ����仯,��ˢ�µ�ǰ����", "��ʾ");
                return;
            }


            //����ֻ��ĸ�ײ�����Ӥ���Ǽ�
            //if (this.MomInfo.Sex.ID.ToString() != "F" || this.MomInfo.ID.Substring(4, 1) == "B")
            if (this.MomInfo.Sex.ID.ToString() != "F" || this.MomInfo.PID.PatientNO.StartsWith("B"))
            {
                MessageBox.Show("ֻ��ĸ�ײ�����Ӥ���Ǽǣ�", "��ʾ");
                return;
            }

            if (cmbSex.Text == "")
            {
                MessageBox.Show("��ѡ���Ա�", "��ʾ");
                return;
            }

            //ȡ�ؼ�����д��Ӥ����Ϣ
            //FS.HISFC.Models.RADT.PatientInfo  objBaby = new FS.HISFC.Models.RADT.PatientInfo();
            this.GetBabyInfo();

            if (FS.FrameWork.Public.String.ValidMaxLengh(this.BabyInfo.Name, 20) == false)
            {
                MessageBox.Show("���Ʋ��ܳ���10�����ֻ���20��Ӣ���ַ�,���������룡", "���ƹ���");
                return;
            }
            ////�ж������Ƿ�Ϊ����
            //for (int i = 0, j = this.txtHeight.Text.Length; i < j; i++)
            //{
            //    if (!char.IsDigit(this.txtHeight.Text, i))
            //    {
            //        //����˵���ǵڼ����ַ�������
            //        MessageBox.Show("���߱���������", "��ʾ", MessageBoxButtons.OK);
            //        return;
            //    }
            //}
            ////�ж������Ƿ�Ϊ����
            //for (int i = 0, j = this.txtWeight.Text.Length; i < j; i++)
            //{
            //    if (!char.IsDigit(this.txtWeight.Text, i))
            //    {
            //        //����˵���ǵڼ����ַ�������
            //        MessageBox.Show("���ر���������", "��ʾ", MessageBoxButtons.OK);
            //        return;
            //    }
            //}
            //�Ա�Ϊ��ʱ

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(this.inpatientManager.Connection);
            //t.BeginTransaction();

            this.inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.babyManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                this.InpatientNo = this.BabyInfo.ID;
                //������µǼǵ�Ӥ��,��Ǽ�Ӥ�����ͻ���סԺ������Ϣ,�������Ӥ�����ͻ���סԺ����
                if (this.isNew)
                {
                    //�Ǽ�Ӥ����
                    if (this.inpatientManager.InsertNewBabyInfo(this.BabyInfo) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ʧ�ܣ�" + this.inpatientManager.Err, "��ʾ");
                        return;
                    }

                    //�Ǽǻ�������
                    if (this.inpatientManager.InsertPatient(this.BabyInfo) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ʧ�ܣ�" + this.inpatientManager.Err, "��ʾ");
                        return;
                    }

                    //���±����¼����
                    if (this.inpatientManager.SetShiftData(this.BabyInfo.ID, FS.HISFC.Models.Base.EnumShiftType.B, "��Ժ�Ǽ�",
                        this.BabyInfo.PVisit.PatientLocation.Dept, this.BabyInfo.PVisit.PatientLocation.Dept, true) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ʧ�ܣ�" + this.inpatientManager.Err, "��ʾ");
                        return;
                    }

                    //���±����¼
                    this.BabyInfo.PVisit.PatientLocation.Bed.Name = this.BabyInfo.PVisit.PatientLocation.Bed.ID;
                    if (this.inpatientManager.SetShiftData(this.BabyInfo.ID, FS.HISFC.Models.Base.EnumShiftType.K, "����",
                        this.BabyInfo.PVisit.PatientLocation.NurseCell, this.BabyInfo.PVisit.PatientLocation.Bed, true) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ʧ�ܣ�" + this.inpatientManager.Err, "��ʾ");
                        return;
                    }

                    //���²������,Ӥ�����Ǽǲ���
                    if (this.inpatientManager.UpdateCaseSend(this.BabyInfo.ID, false) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ʧ�ܣ�" + this.inpatientManager.Err, "��ʾ");
                        return;
                    }

                    //����ĸ���Ƿ���Ӥ�����
                    if (this.inpatientManager.UpdateMumBabyFlag(this.MomInfo.ID, true) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ʧ�ܣ�" + this.inpatientManager.Err, "��ʾ");
                        return;
                    }

                    //�Ǽ�Ӥ��סԺ�����е���Ժ״̬
                    FS.HISFC.Models.RADT.InStateEnumService status = new FS.HISFC.Models.RADT.InStateEnumService();
                    status.ID = "I"; //סԺ�Ǽ�
                    if (this.inpatientManager.UpdatePatientStatus(this.BabyInfo, status) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ʧ�ܣ�" + this.inpatientManager.Err, "��ʾ");
                        return;
                    }

                    //����Ӥ���ǼǱ��е��ֶ� ĸ���Ƿ��Ҹο�ԭ���� �� Ӥ���Ƿ��Ѿ�ע���Ч�����򵰰�
                    string IsMomHBAP = "0";
                    string IsImmu = "0";
                    if (this.neuCkbIsMomHBAP.Checked == true)
                    {
                        IsMomHBAP = "1";
                    }
                    if (this.neuCkbIsImmu.Checked == true)
                    {
                        IsImmu = "1";
                    }
                    if (this.babyManager.InsertBabyInfoExtend(this.BabyInfo, IsMomHBAP, IsImmu)==-1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ʧ�ܣ�" + this.babyManager.Err, "��ʾ");
                        return;
                    }
                }
                else
                {
                    //���»���סԺ����(����������ͬʱ,�����Ӥ����)
                    if (this.inpatientManager.UpdatePatient(this.BabyInfo) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ʧ�ܣ�" + this.inpatientManager.Err, "��ʾ");
                        return;
                    }

                    //����Ӥ���ǼǱ��е��ֶ� ĸ���Ƿ��Ҹο�ԭ���� �� Ӥ���Ƿ��Ѿ�ע���Ч�����򵰰� 
                    string IsMomHBAP = "0";
                    string IsImmu = "0";
                    if (this.neuCkbIsMomHBAP.Checked == true)
                    {
                        IsMomHBAP = "1";
                    }
                    if (this.neuCkbIsImmu.Checked == true)
                    {
                        IsImmu = "1";
                    }
                    if (this.babyManager.UpdateBabyInfo(IsMomHBAP, IsImmu, this.BabyInfo) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ʧ�ܣ�" + this.babyManager.Err, "��ʾ");
                        return;
                    }
                }

                //�ύ���ݿ�
                FS.FrameWork.Management.PublicTrans.Commit();
                //������µǼ�Ӥ��,���ڻ����б��в���һ���½ڵ�,������½ڵ�
                if (this.tv != null)
                {//{3E2B1A30-6689-4ea7-9946-DC60E1886D4E} Ӥ���Ǽǿ���ͨ���úŵ���ʹ�� 20100919
                    if (this.isNew)
                    {
                        this.tv.AddTreeNode(0, this.BabyInfo);
                    }
                    else
                    {
                        //����Ӥ�����ڵĽڵ�,���޸Ĵ˽ڵ�
                        TreeNode node = this.tv.FindNode(0, this.BabyInfo);
                        if (node != null) this.tv.ModifiyNode(node, this.BabyInfo);
                    }
                }
                MessageBox.Show("����ɹ���");
                
                //ˢ��Ӥ���б�
                RefreshList(this.MomInfo.ID);

                ShowBabyInfo(this.BabyInfo);
                base.OnRefreshTree();
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(ex.Message);
                return;
            }
        }


        private void tvPatientList1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            //ȡ�ڵ��ϵ���Ϣ
            this.BabyInfo = e.Node.Tag as FS.HISFC.Models.RADT.PatientInfo;

            //���ݻ�ȡ����Ӥ��סԺ�Ż�ȡ����������չ�ֶ�
            FS.FrameWork.Models.NeuObject baby = new FS.FrameWork.Models.NeuObject();
            if (this.BabyInfo != null)
            {
                baby = this.babyManager.GetBabyInfoExtend(this.BabyInfo.ID);
                if (baby == null)
                {
                    MessageBox.Show("��ȡӤ����Ϣʧ�ܣ�" + this.babyManager.Err);
                }
                else
                {
                    this.BabyInfo.PVisit.User01 = baby.User01;
                    this.BabyInfo.PVisit.User02 = baby.User02; //ʹ��user01 user02��¼Ӥ����������չ�ֶ�
                }
            }
            //��Ӥ����Ϣ��ʾ�ڿؼ���
            this.ShowBabyInfo(this.BabyInfo);
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
            tv = sender as FS.HISFC.Components.Common.Controls.tvPatientList;
            this.InitControl();
            return null;
        }
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="neuObject"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (neuObject != null)
            {
                this.MomInfo = neuObject as FS.HISFC.Models.RADT.PatientInfo;
                this.BabyInfo = MomInfo.Clone();
                if (this.MomInfo.ID != null || this.MomInfo.ID != "")
                {
                    try
                    {
                        this.txtMomInfo.Text = "[" + MomInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "]" + MomInfo.Name;
                        RefreshList(MomInfo.ID);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            return 0;
        }

        #region {FEA519C4-2379-40a9-8271-829A76E04EF6}

        private void cmbSex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.MomInfo != null && this.MomInfo.ID != "")
            {
                this.babyNo = this.BabyNum + 1;
                this.txtName.Text = this.CreatBabyName(this.MomInfo.Name, this.cmbSex.Tag.ToString(), this.babyNo);
            }
        }
        #endregion

        //{3E2B1A30-6689-4ea7-9946-DC60E1886D4E} Ӥ���Ǽǿ���ͨ���úŵ���ʹ�� 20100919
        private void ucQueryInpatientNo_myEvent()
        {
            this.MomInfo = inpatientManager.QueryPatientInfoByInpatientNO(this.ucQueryInpatientNo.InpatientNo);
            this.BabyInfo = MomInfo.Clone();
            if (this.MomInfo.ID != null || this.MomInfo.ID != "")
            {
                try
                {
                    this.txtMomInfo.Text = "[" + MomInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "]" + MomInfo.Name;
                    RefreshList(MomInfo.ID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            this.setNextControlFocus();
        }

        /// <summary>
        /// ������һ���ؼ���ý���
        /// </summary>		
        private void setNextControlFocus()
        {
            System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void dtBirthday_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                this.setNextControlFocus();
            }
        }

        private void cmbSex_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                this.setNextControlFocus();
            }
        }
        #endregion  

        private void txtHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                this.setNextControlFocus();
            }
        }

        private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                this.setNextControlFocus();
            }
        }

    }
}