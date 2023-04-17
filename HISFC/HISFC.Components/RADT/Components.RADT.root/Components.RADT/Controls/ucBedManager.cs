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
    /// [��������: ��ʿվ��λ����]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2006-11-30]<br></br>
    /// <�޸ļ�¼
    ///		�޸���='����'
    ///		�޸�ʱ��='2007-08-29'
    ///		�޸�Ŀ��='����λҽ��'
    ///		�޸�����='�޸�ԭ��Ĭ�ϵĴ�λҽ��(��סԺҽ��)'
    ///  />
    /// </summary>
    public partial class ucBedManager : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucBedManager()
        {
            InitializeComponent();
        }

        protected Neusoft.FrameWork.Models.NeuObject objNurse = null;
        private Neusoft.FrameWork.Models.NeuObject nurseCell
        {
            get
            {

                if(objNurse==null)
                    objNurse = ((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).Nurse.Clone();
                return objNurse;
            }
            set
            {
                objNurse = value;
            }
        }

        #region ����
        Neusoft.HISFC.BizProcess.Integrate.Manager manager = new Neusoft.HISFC.BizProcess.Integrate.Manager();
        Neusoft.HISFC.BizLogic.RADT.InPatient myInpatient = new Neusoft.HISFC.BizLogic.RADT.InPatient();
        int patientnum = 0;
        private DataSet constantData = new DataSet();
        private Neusoft.HISFC.Models.Base.Bed myBed = null;	//��ǰ�����Ĵ�λ��Ϣ
        string Err = "";
        #endregion

        #region ��λά������

        /// <summary>
        /// �Ѿ����ڵĴ����Ƿ������޸Ĵ���(ֻ��Կմ���
        /// </summary>
        private bool isAllowEditBedNo = false;

        /// <summary>
        /// �Ѿ����ڵĴ����Ƿ������޸Ĵ���
        /// </summary>
        [Category("��λά������"), Description("�Ѿ����ڵĴ����Ƿ������޸Ĵ���(ֻ��Կմ���")]
        public bool IsAllowEditBedNo
        {
            get
            {
                return isAllowEditBedNo;
            }
            set
            {
                isAllowEditBedNo = value;
                //txtBedNo.Enabled = value;
            }
        }

        /// <summary>
        /// �Ƿ������޸Ĵ�λ�ȼ�
        /// </summary>
        private bool isAllowEditBedLevel = false;

        /// <summary>
        /// �Ƿ������޸Ĵ�λ�ȼ�
        /// </summary>
        [Category("��λά������"), Description("�Ƿ������޸Ĵ�λ�ȼ�")]
        public bool IsAllowEditBedLevel
        {
            get
            {
                return isAllowEditBedLevel;
            }
            set
            {
                isAllowEditBedLevel = value;
                this.cmbGrade.Enabled = value;
            }
        }

        /// <summary>
        /// �Ƿ�����������д�λ�ȼ�
        /// </summary>
        private bool isAllowAddAllBedLevel = false;

        /// <summary>
        /// �Ƿ�����������д�λ�ȼ�
        /// </summary>
        [Category("��λά������"), Description("�Ƿ�����������д�λ�ȼ�")]
        public bool IsAllowAddAllBedLevel
        {
            get
            {
                return isAllowAddAllBedLevel;
            }
            set
            {
                isAllowAddAllBedLevel = value;
            }
        }

        /// <summary>
        /// �Ƿ�����������д�λ����
        /// </summary>
        private bool isAllBedWave = false;

        /// <summary>
        /// �Ƿ�����������д�λ����
        /// </summary>
        [Category("��λά������"), Description("�Ƿ����������д�λ���Ʋ���")]
        public bool IsAllBedWave
        {
            get
            {
                return this.isAllBedWave;
            }
            set
            {
                this.isAllBedWave = value;
                this.cmbWeave.Enabled = this.isAllBedWave;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Init()
        {
            try
            {
                if (this.nurseCell != null &&
                    this.nurseCell.ID != null &&
                    this.nurseCell.ID != "")
                {
                    //����ҽ��,��ʿ�б�

                    //���ݻ�ʿվ�õ�������Ϣ
                    ArrayList alDepts = manager.QueryDepartment(this.nurseCell.ID);
                    if (alDepts == null) return;

                    //ȡ�����е�ҽ���б�
                    ArrayList alDoc = new ArrayList();
                    for (int i = 0; i < alDepts.Count; i++)
                    {
                        alDoc.AddRange(manager.QueryEmployee(Neusoft.HISFC.Models.Base.EnumEmployeeType.D, ((Neusoft.FrameWork.Models.NeuObject)alDepts[i]).ID));
                    }

                    //��ȡ�õ�ҽ���б���ӵ�ҽ���ؼ���
                    this.cmbAdmittingDoctor.AddItems(alDoc);
                    this.cmbAttendingDoctor.AddItems(alDoc);
                    this.cmbConsultingDoctor.AddItems(alDoc);
                    this.cmbBedDoctor.AddItems(alDoc);

                    ArrayList alNurse = manager.QueryNurse(this.nurseCell.ID);
                    if (alNurse == null) return;

                    //��ȡ�õĻ�ʿ�б���ӵ�ҽ���ؼ���
                    this.cmbAdmittingNurse.AddItems(alNurse);

                    //���ش�λ�����б�
                    this.cmbWeave.AddItems(Neusoft.HISFC.Models.Base.BedRankEnumService.List());

                    if (isAllowAddAllBedLevel)
                    {
                        this.cmbGrade.AddItems(manager.GetConstantList(Neusoft.HISFC.Models.Base.EnumConstant.BEDGRADE));
                    }
                    else
                    {
                        ArrayList alBedLevel = manager.GetConstantList(Neusoft.HISFC.Models.Base.EnumConstant.BEDGRADE);
                        if (alBedLevel == null)
                        {
                            MessageBox.Show("��ѯ��λ�Ǽ��б����\r\n" + manager.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        ArrayList al = new ArrayList();
                        foreach (Neusoft.HISFC.Models.Base.Const conObj in alBedLevel)
                        {
                            if (conObj.Memo.Trim() != "����")
                            {
                                al.Add(conObj);
                            }
                        }

                        this.cmbGrade.AddItems(al);
                    }


                    this.cmbIsValid.AddItems(InitComboxList());
                    this.cmbIsprepay.AddItems(InitComboxList());


                    //�������
                    this.ClearInfo();

                    //��ʾ��λ�б�
                    this.ShowBedList();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ��ʼ��Combox�����б�
        /// </summary>
        /// <returns></returns>
        private ArrayList InitComboxList()
        {
            ArrayList list = new ArrayList();
            Neusoft.FrameWork.Models.NeuObject obj;
            obj = new Neusoft.FrameWork.Models.NeuObject();
            obj.ID = "True";
            obj.Name = "��";
            list.Add(obj);
            obj = new Neusoft.FrameWork.Models.NeuObject();
            obj.ID = "False";
            obj.Name = "��";
            list.Add(obj);
            return list;
        }


        /// <summary>
        /// ��ʾ��λ�б�
        /// </summary>
        public void ShowBedList()
        {
            //����
            this.ClearInfo();

            //ȡ��λ��Ϣ
            ArrayList al = this.manager.QueryBedList(this.nurseCell.ID);
            if (al == null)
            {
                MessageBox.Show(this.manager.Err, "��ʾ");
                return;
            }

            //��ʾ����������
            this.fpSpread1_Sheet1.RowCount = al.Count;

            //������ʾ����
            Neusoft.HISFC.Models.Base.Bed bed = null;
            this.patientnum = 0; //���ڼ���ռ�ô�λ��
            for (int i = 0; i < al.Count; i++)
            {
                bed = al[i] as Neusoft.HISFC.Models.Base.Bed;
                if (bed == null) return;
                //��ֵ
                this.fpSpread1_Sheet1.Cells[i, 0].Value = bed.ID.Substring(4);	//����
                this.fpSpread1_Sheet1.Cells[i, 1].Value = bed.SickRoom.ID;      //�����
                this.fpSpread1_Sheet1.Cells[i, 2].Value = bed.TendGroup;	    //������
                this.fpSpread1_Sheet1.Cells[i, 3].Value = bed.BedGrade.Name;    //��λ�ȼ�
                this.fpSpread1_Sheet1.Cells[i, 4].Value = bed.User03;			//��λ��
                this.fpSpread1_Sheet1.Cells[i, 5].Value = bed.BedRankEnumService.Name;		//��λ����
                this.fpSpread1_Sheet1.Cells[i, 6].Value = bed.Status.Name;	//��λ״̬

                if (bed.InpatientNO == "N")
                {
                    this.fpSpread1_Sheet1.Cells[i, 7].Value = "";
                }
                else
                {
                    Neusoft.HISFC.Models.RADT.PatientInfo info = this.myInpatient.QueryPatientInfoByInpatientNO(bed.InpatientNO);

                    this.fpSpread1_Sheet1.Cells[i, 7].Value = info.PID.PatientNO;
                }
                //this.fpSpread1_Sheet1.Cells[i, 6].Value = bed.InpatientNO== "N" ? "" : bed.InpatientNO.Substring(4);		//סԺ��
                this.fpSpread1_Sheet1.Cells[i, 8].Value = bed.Phone;				//��λ�绰
                this.fpSpread1_Sheet1.Cells[i, 9].Value = bed.OwnerPc;			//����
                this.fpSpread1_Sheet1.Cells[i, 10].Value = bed.IsValid;			//�Ƿ�ͣ��
                this.fpSpread1_Sheet1.Cells[i, 11].Value = bed.IsPrepay;			//�Ƿ�ԤԼ
                this.fpSpread1_Sheet1.Rows[i].Tag = bed;
                //���㴲λռ����(ռ��,�Ҵ�,����)
                if (bed.Status.ID.ToString() == "O" || bed.Status.ID.ToString() == "H" || bed.Status.ID.ToString() == "W")
                    this.patientnum++;
            }
            decimal rate;
            if (al.Count == 0)
            {
                rate = 0;
            }
            else
            {
                rate = Neusoft.FrameWork.Public.String.FormatNumber(this.patientnum / Neusoft.FrameWork.Function.NConvert.ToDecimal(al.Count) * 100, 2);
            }
            this.txtshow.Text = "ռ�ô�����" + patientnum.ToString() + "   ����������" + al.Count.ToString() + "   ռ���ʣ�" + rate.ToString() + "%";

            //ѡ�е�һ��
            if (this.fpSpread1_Sheet1.RowCount > 0)
                this.fpSpread1_Sheet1.ActiveRowIndex = 0;
            this.fpSpread1.Focus();
        }


        /// <summary>
        /// ��ʾҪ�޸ĵĴ�λ��Ϣ,�����Ƹ��ؼ��Ƿ����
        /// </summary>
        /// <param name="bed"></param>
        private void ShowInfoForModify(Neusoft.HISFC.Models.Base.Bed bed)
        {
            //ȡ��ǰ���е�����
            this.myBed = this.fpSpread1_Sheet1.ActiveRow.Tag as Neusoft.HISFC.Models.Base.Bed;
            if (this.myBed == null)
            {
                return;
            }

            //this.cmbStatus.Items.Clear();
            //�����ռ��O,���R,�Ҵ�H,����W,����ʾȫ���Ĵ�λ״̬.�����ƿؼ��Ƿ����
            if (this.myBed.Status.ID.ToString() == "O" ||
                this.myBed.Status.ID.ToString() == "R" ||
                this.myBed.Status.ID.ToString() == "H" ||
                this.myBed.Status.ID.ToString() == "W")
            {
                this.cmbStatus.AddItems(Neusoft.HISFC.Models.Base.BedStatusEnumService.List());
                this.cmbStatus.Enabled = false;	//��λ״̬
                this.cmbIsprepay.Enabled = false;	//�Ƿ�ԤԼ
                this.cmbIsValid.Enabled = false;	//�Ƿ���Ч
                if (this.isAllowEditBedLevel)
                {
                    this.cmbGrade.Enabled = true;	//��λ�ȼ�
                }
                else
                {
                    this.cmbGrade.Enabled = false;	//��λ�ȼ�
                }
                this.btnDelete.Enabled = false;	//������ɾ��
            }
            else
            {
                this.cmbStatus.AddItems(Neusoft.HISFC.Models.Base.BedStatusEnumService.UnoccupiedList());
                this.cmbStatus.Enabled = true;	//��λ״̬
                //this.cmbWeave.Enabled    = true;	//��λ����
                this.cmbIsprepay.Enabled = true;	//�Ƿ�ԤԼ
                this.cmbIsValid.Enabled = true;	//�Ƿ���Ч

                if (this.isAllowEditBedLevel)
                {
                    this.cmbGrade.Enabled = true;	//��λ�ȼ�
                }
                else
                {
                    this.cmbGrade.Enabled = false;	//��λ�ȼ�
                }
                if (this.myBed.BedRankEnumService.ID.ToString() == "A")
                    this.btnDelete.Enabled = true;	//�Ӵ�����ɾ��
                else
                    this.btnDelete.Enabled = false;	//������λ������ɾ��

            }

            //��ʾ��Ϣ
            this.txtBedNo.Text = this.myBed.ID.Substring(4);	//����
            this.txtBedNo.Tag = this.myBed.ID;					//����
            this.txtOwnPc.Text = this.myBed.OwnerPc;			//����
            this.txtPhone.Text = this.myBed.Phone;				//��λ�绰
            this.txtSortId.Text = this.myBed.SortID.ToString();	//����
            this.txtWardNo.Text = this.myBed.SickRoom.ID;			//����
            this.cmbGrade.Tag = this.myBed.BedGrade.ID;		//��λ�ȼ�
            this.cmbWeave.Tag = this.myBed.BedRankEnumService.ID;		//��λ����
            this.cmbStatus.Tag = this.myBed.Status.ID;		//��λ״̬
            this.cmbIsValid.Tag = this.myBed.IsValid.ToString();	//�Ƿ���Ч
            if (this.myBed.PrepayOutdate != DateTime.MinValue)
            {
                this.dtOutDate.Value = this.myBed.PrepayOutdate;	//ԤԼ��Ժʱ��(����Ҫ�û�ά��)
            }
            this.cmbIsprepay.Tag = this.myBed.IsPrepay.ToString();			//�Ƿ�ԤԼ
            this.cmbAdmittingDoctor.Tag = this.myBed.AdmittingDoctor.ID;	//סԺҽ��
            this.cmbAttendingDoctor.Tag = this.myBed.AttendingDoctor.ID;	//����ҽ��
            this.cmbConsultingDoctor.Tag = this.myBed.ConsultingDoctor.ID;	//����ҽ��
            this.cmbAdmittingNurse.Tag = this.myBed.AdmittingNurse.ID;		//���λ�ʿ
            this.txtTendGroup.Text = this.myBed.TendGroup;		//������
            //{255E2A05-2A48-4d50-B05E-0E0AA225B9B4}
            this.cmbBedDoctor.Tag = this.myBed.Doctor.ID;    //��λҽ��

            if (this.IsAllowEditBedNo && bed.InpatientNO.Trim() == "N")
            {
                this.txtBedNo.Enabled = true;
            }
            else
            {
                //�޸Ĵ�λ��Ϣʱ,�������޸Ĵ���
                this.txtBedNo.Enabled = false;
            }
        }


        /// <summary>
        /// ȡ�ؼ���ά���Ĵ�λ��Ϣ
        /// </summary>
        /// <returns></returns>
        private Neusoft.HISFC.Models.Base.Bed GetBedInfo()
        {
            //�����������λ,��ȡ������Ϣ
            if (this.myBed == null)
            {
                this.myBed = new Neusoft.HISFC.Models.Base.Bed();
                this.myBed.ID = this.txtBedNo.Text;
                this.myBed.InpatientNO= "N";
            }

            this.myBed.OwnerPc = this.txtOwnPc.Text;									//����
            this.myBed.Phone = this.txtPhone.Text;									//�绰
            this.myBed.SortID = Neusoft.FrameWork.Function.NConvert.ToInt32(this.txtSortId.Text);	//���(������)
            this.myBed.SickRoom.ID = this.txtWardNo.Text;									//����
            //this.myBed.Doctor.ID = this.cmbAdmittingDoctor.Tag.ToString();			//ҽ������
            this.myBed.BedGrade.ID = this.cmbGrade.Tag.ToString();						//��λ�ȼ�����
            this.myBed.BedRankEnumService.ID = this.cmbWeave.Tag.ToString();						//��λ���Ʊ���
            this.myBed.Status.ID = this.cmbStatus.Tag.ToString();					//��λ״̬����
            //�Ƿ���Ч:0��Ч,1��Ч---��ʵ�������෴
            this.myBed.IsValid = Neusoft.FrameWork.Function.NConvert.ToBoolean(this.cmbIsValid.Tag.ToString());
            this.myBed.IsPrepay = Neusoft.FrameWork.Function.NConvert.ToBoolean(this.cmbIsprepay.Tag.ToString());	//�Ƿ���Ч
            this.myBed.NurseStation.ID = this.nurseCell.ID;							//����վ����
            this.myBed.PrepayOutdate = this.dtOutDate.Value;							//ԤԼ��Ժʱ��
            this.myBed.AdmittingDoctor.ID = this.cmbAdmittingDoctor.Tag.ToString();		//סԺҽ��
            this.myBed.AttendingDoctor.ID = this.cmbAttendingDoctor.Tag.ToString();		//סԺҽ��
            this.myBed.ConsultingDoctor.ID = this.cmbConsultingDoctor.Tag.ToString();	//סԺҽ��
            this.myBed.AdmittingNurse.ID = this.cmbAdmittingNurse.Tag.ToString();		//סԺҽ��
            this.myBed.TendGroup = this.txtTendGroup.Text;								//������
           this.myBed.Doctor.ID=this.cmbBedDoctor.Tag.ToString()  ;    //��λҽ��
            //���ش�λ��Ϣ
            return this.myBed;
        }


        /// <summary>
        /// ����
        /// </summary>
        private void ClearInfo()
        {
            //��մ�λ��Ϣ
            this.myBed = null;
            this.cmbStatus.AddItems(Neusoft.HISFC.Models.Base.BedStatusEnumService.UnoccupiedList());
            this.cmbStatus.Enabled = true;

            if (this.IsAllowEditBedNo)
            {
                this.txtBedNo.Enabled = true;
            }
            else
            {
                this.txtBedNo.Enabled = false;
            }
            this.dtOutDate.Enabled = false;
            this.cmbIsprepay.Enabled = false;
            this.btnDelete.Enabled = true;

            this.txtBedNo.Text = "";
            this.txtBedNo.Tag = "";
            this.txtOwnPc.Text = "";
            this.txtPhone.Text = "";
            this.txtSortId.Text = "";
            this.txtWardNo.Text = "";
            this.cmbGrade.SelectedIndex = 0;
            this.cmbWeave.Tag = "A";
            //this.cmbWeave.SelectedIndex = 0;
            this.cmbStatus.Tag = "U";
            this.cmbIsValid.Tag = "False";
            this.dtOutDate.Text = "";
            this.cmbIsprepay.Tag = "False";
            this.cmbAdmittingDoctor.Tag = "";
            this.cmbAttendingDoctor.Tag = "";
            this.cmbConsultingDoctor.Tag = "";
            this.cmbAdmittingNurse.Tag = "";
            this.cmbBedDoctor.Tag = "";
        }


        /// <summary>
        /// ��������Ƿ���Ч
        /// </summary>
        /// <returns></returns>
        private int CheckValid()
        {
            if (this.txtBedNo.Text == "")
            {
                this.Err = "���Ų���Ϊ�գ�����д��";
                return -1;
            }
            if (this.txtBedNo.Text.Trim().Length>6)
            {
                this.Err = "���Ź�������������д��";
                return -1;
            }
            if (this.txtWardNo.Text.Trim().Length > 10)
            {
                this.Err = "����Ź�������������д��";
                return -1;
            }
            if (this.txtWardNo.Text == "")
            {
                this.Err = "�����Ų���Ϊ�գ�����д��";
                return -1;
            }
            if (this.cmbGrade.Text == "")
            {
                this.Err = "��λ�ȼ�Ϊ�գ���ѡ��";
                return -1;
            }
            if (this.cmbWeave.Text == "")
            {
                this.Err = "��λ����Ϊ�գ���ѡ��";
                return -1;
            }
            if (this.cmbStatus.Text == "")
            {
                this.Err = "��λ״̬Ϊ�գ���ѡ��";
                return -1;
            }
            return 1;
        }


        /// <summary>
        /// ����
        /// </summary>
        private void Save()
        {
            //���¼���Ƿ���Ч
            if (this.CheckValid() == -1)
            {
                MessageBox.Show(this.Err);
                return;
            }

            //ȡ�û�¼��Ĵ�λ��Ϣ
            this.GetBedInfo();

            //���洲λ��Ϣ
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

            //Neusoft.FrameWork.Management.Transaction trans = new Neusoft.FrameWork.Management.Transaction(Neusoft.FrameWork.Management.Connection.Instance);
            //trans.BeginTransaction();

            this.manager.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);

            int parm = this.manager.SetBed(this.myBed);
            if (parm != 1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                if (this.manager.DBErrCode == 1)
                    MessageBox.Show("����Ϊ��" + this.myBed.ID.Substring(4) + "���Ĵ�λ�Ѿ�����,��ά�������Ĵ�λ.");
                else
                    MessageBox.Show(this.manager.Err, "������ʾ");

                //����
                this.ClearInfo();
                return;
            }
            Neusoft.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("����ɹ�", "��ʾ");
            //ˢ�´�λ�б�
            this.ShowBedList();
        }

        #endregion

        #region �¼�

        private void fpSpread1_Click(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.fpSpread1_Sheet1.RowCount == 0 || this.fpSpread1_Sheet1.ActiveRowIndex < 0)
            {
                return;
            }

            //��ʾ��λ��Ϣ
            this.ShowInfoForModify(this.myBed);
        }


        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            //��մ�λ��Ϣ
            ClearInfo();
            this.txtBedNo.Enabled = true;
            this.txtBedNo.Focus();
            this.btnDelete.Enabled = false; //������λ������ɾ��

            /*
             * [2007/02/01] ������Ա�����,ԭ���� false ��,��֪����û��ʲô������,
             *              �Ժ�Ҫ�ľ͸�����ط�
             */
            this.cmbGrade.Enabled = true;
            this.cmbIsValid.Enabled = true;
            #region {5DF40042-300D-49b8-BB8D-4E4E906B7BAF}
            this.cmbWeave.Tag = Neusoft.HISFC.Models.Base.EnumBedRank.A.ToString();
            #endregion

        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            if (this.myBed == null)
            {
                MessageBox.Show("��ѡ��Ҫɾ���Ĵ�λ", "��ʾ");
                return;
            }

            //�����������λ,���������
            if (this.myBed.ID == "")
            {
                this.ClearInfo();
                return;
            }

            if (this.myBed.Status.ID.ToString() == "O" || 
                this.myBed.Status.ID.ToString() == "W" ||
                this.myBed.Status.ID.ToString() == "R" ||
                this.myBed.Status.ID.ToString() == "H")
            {
                MessageBox.Show("�ô��Ѿ���ռ�ã�����ɾ����", "��ʾ");
                return;
            }

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

            //Neusoft.FrameWork.Management.Transaction trans = new Neusoft.FrameWork.Management.Transaction(Neusoft.FrameWork.Management.Connection.Instance);
            //trans.BeginTransaction();

            this.manager.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);

            if (this.manager.DeleteBed(this.myBed.ID) >= 0)
            {
                Neusoft.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("ɾ���ɹ���", "��ʾ");
            }
            else
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("ɾ��ʧ�ܣ�" + this.manager.Err, "��ʾ");
            }

            this.ClearInfo();
            this.ShowBedList();
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            //����
            this.Save();
        }

        private void fpSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            if (this.fpSpread1_Sheet1.RowCount == 0 || this.fpSpread1_Sheet1.ActiveRowIndex < 0) return;

            //��ʾ��λ��Ϣ
            this.ShowInfoForModify(this.myBed);
        }

        #endregion

        #region ��д
        protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            return null;
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            base.OnLoad(e);
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (neuObject == null) return 0;
            if (neuObject.GetType() == typeof(Neusoft.HISFC.Models.Base.Department))
            {
                this.nurseCell = neuObject as Neusoft.FrameWork.Models.NeuObject;
                this.Init();
            }
            return 0;
        }
        #endregion
    }
}
