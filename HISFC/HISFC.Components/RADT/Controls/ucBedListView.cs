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
    /// [��������: �����б�]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2006-11-30]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    public partial class ucBedListView : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucBedListView()
        {
            InitializeComponent();
        }

        #region ����
        public event ListViewItemSelectionChangedEventHandler ListViewItemChanged;

        protected ArrayList alBeds = null;

        private bool IslvBedShow = false;

        /// <summary>
        /// ���������б�
        /// </summary>
        protected ArrayList AlBeds
        {
            get
            {
                {
                    //***************��ò����б�*************
                    FS.HISFC.Models.Base.Employee empl = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

                    //ȡ������վ���õĴ�λ�б�:��ʱȡ���Ǳ�����ȫ���Ĵ�λ�б�
                    alBeds = manager.QueryBedList(empl.Nurse.ID);
                }
                return alBeds;
            }
        }
        protected FS.HISFC.BizLogic.RADT.InPatient radtManager = new FS.HISFC.BizLogic.RADT.InPatient();
        FS.HISFC.BizProcess.Integrate.RADT radtIntergrate = new FS.HISFC.BizProcess.Integrate.RADT();
        FS.HISFC.BizLogic.Manager.Person personMgr = new FS.HISFC.BizLogic.Manager.Person();

        protected FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
        protected FS.HISFC.Components.Common.Controls.tvPatientList tv = null;//��ǰ������
        private string Err;
        System.Windows.Forms.ToolTip toolTip1 = new System.Windows.Forms.ToolTip();
        //{5F752A30-7971-4b65-A84B-D233EF2A4406}
        protected FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase healthMgr = new FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase();

        //{C29C2D37-D519-49af-AFEA-4981B1A013AE}
        /// <summary>
        /// ���䴲λ����
        /// </summary>
        /// <param name="bedNO"></param>
        public delegate void ArrangeBed(string bedNO);
        public event ArrangeBed arrangeBed;
        #endregion 

        #region ����

        /// <summary>
        /// �Ƿ�ֻ����մ��ɽ���
        /// </summary>
        private bool isOnlyEmptyBedCanReceive = false;

        /// <summary>
        /// �Ƿ�ֻ����մ��ɽ���
        /// </summary>
        public bool IsOnlyEmptyBedCanReceive
        {
            set { this.isOnlyEmptyBedCanReceive = value; }
            get { return this.isOnlyEmptyBedCanReceive; }
        }

        /// <summary>
        /// ������������ͣ�0 ȫ�������������1 ȫ�����������2 ҽ�������Ѳ����������Ĭ��1
        /// </summary>
        private int wapType = 1;

        /// <summary>
        /// ������������ͣ�0 ȫ�������������1 ȫ�����������2 ҽ�������Ѳ����������Ĭ��1
        /// </summary>
        public int WapType
        {
            get
            {
                return wapType;
            }
            set
            {
                wapType = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ��������
        /// </summary>
        protected bool bShowDept = false;

        /// <summary>
        /// �Ƿ���ʾ��������
        /// </summary>
        public bool IsShowDeptName
        {
            get
            {
                return bShowDept;
            }
            set
            {
                bShowDept = value;
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ˢ���б�
        /// </summary>
        public void RefreshView()
        {

            // {43D3B083-0C3D-459d-9E2E-007368A21857}
            this.lsvBedView.BeginUpdate();
            //��ʾ����
            this.PaintListView();

            if (this.myucPCList != null)
            {
                //this.Controls.Clear();
                this.myucPCList = null;

                //this.lsvBedView.Visible = IslvBedShow;
            }

            this.lsvBedView.Visible = IslvBedShow;
            this.ucPatientCardList.Visible = !IslvBedShow;

            //{5D00E993-7774-47d1-95AA-C0F356F10BFA}
            this.panel1.Controls.Clear();

            //this.groupBox1.Visible = !IslvBedShow;
            //this.groupBox1.Dock = DockStyle.Top;
            //this.Controls.Add(this.groupBox1);
            this.lsvBedView.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(this.lsvBedView);
            this.myucPCList.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(this.myucPCList);

            this.lsvBedView.EndUpdate();
        }


        /// <summary>
        /// ����ListView����
        /// </summary>
        private void CreateHeaders()
        {
            //
            ColumnHeader colHead;
            colHead = new ColumnHeader();
            colHead.Text = "��������";
            colHead.Width = 250;
            this.lsvBedView.Columns.Add(colHead);
            //
            colHead = new ColumnHeader();
            colHead.Text = "�Ա�";
            colHead.Width = 40;
            this.lsvBedView.Columns.Add(colHead);
            //
            colHead = new ColumnHeader();
            colHead.Text = "סԺ��";
            colHead.Width = 80;
            this.lsvBedView.Columns.Add(colHead);
            //
            colHead = new ColumnHeader();
            colHead.Text = "����";
            colHead.Width = 100;
            this.lsvBedView.Columns.Add(colHead);
            //
            colHead = new ColumnHeader();
            colHead.Text = "״̬";
            colHead.Width = 100;
            this.lsvBedView.Columns.Add(colHead);
            //
            colHead = new ColumnHeader();
            colHead.Text = "��λ��";
            colHead.Width = 100;
            this.lsvBedView.Columns.Add(colHead);
            //
            colHead = new ColumnHeader();
            colHead.Text = "��λ����";
            colHead.Width = 100;
            this.lsvBedView.Columns.Add(colHead);

            //
            colHead = new ColumnHeader();
            colHead.Text = "������";
            colHead.Width = 100;
            this.lsvBedView.Columns.Add(colHead);

            //
            colHead = new ColumnHeader();
            colHead.Text = "��ʳ";
            colHead.Width = 100;
            this.lsvBedView.Columns.Add(colHead);

        }

        int bedCount = 0;
        int babyCountALL = 0;
        int motherCount = 0;
        int patientNunm = 0;
        int extraBed = 0;
        
        /// <summary>
        /// ��ʾ����
        /// </summary>
        protected virtual void PaintListView()
        {
            //���
            this.lsvBedView.Items.Clear();

            try
            {
                bedCount = 0;
                babyCountALL = 0;
                motherCount = 0;
                patientNunm = 0;
                extraBed = 0;

                //ѭ������λ��ӵ��б���              
                foreach (FS.HISFC.Models.Base.Bed bed in this.AlBeds)
                {
                    bedCount++;
                    if (bed.BedRankEnumService.ID.ToString() == "A")
                    {
                        extraBed++;
                    }

                   ListViewItem lst = this.GetListViewInfo(bed);
                   if (lst != null)
                   {
                       this.lsvBedView.Items.Add(lst);
                   }
                }

                decimal rate = 0;
                try
                {
                   // rate = FS.FrameWork.Public.String.FormatNumber(patientNunm / FS.FrameWork.Function.NConvert.ToDecimal(bedCount) * 100, 2);
                    rate = FS.FrameWork.Public.String.FormatNumber(patientNunm / (FS.FrameWork.Function.NConvert.ToDecimal(bedCount) - FS.FrameWork.Function.NConvert.ToDecimal(extraBed)) * 100, 2);
                }
                catch
                {
                }

                this.lblBedSumInfo.Text ="����������" + bedCount.ToString() + "���Ӵ�����" + extraBed.ToString()+"��   ռ�ô�λ����" + patientNunm.ToString() + "   ��λռ���ʣ�" + rate.ToString() + "%";

                if (motherCount != 0)
                {
                    this.lblBedSumInfo.Text += "\r\n���� " + patientNunm.ToString() + "��Ӥ�� " + babyCountALL.ToString() + "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }


        /// <summary>
        /// ��ʾ������Ϣ
        /// ��˫����λ�б�ʱִ�д˷���
        /// </summary>
        private void ShowPatientInfo()
        {
            if (this.lsvBedView.SelectedItems.Count > 0)
            {
                FS.HISFC.Models.RADT.PatientInfo patient = this.lsvBedView.SelectedItems[0].Tag as FS.HISFC.Models.RADT.PatientInfo;
                if (patient != null && tv != null)
                {
                    //ȡ������Ϣ�������б��еĽڵ�λ��
                    TreeNode node = this.tv.FindNode(0, patient);
                    //�������ͽڵ�ѡ���ҵ��Ľڵ�
                    if (node != null) this.tv.SelectedNode = node;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public override FS.FrameWork.WinForms.Forms.ToolBarService Init(object sender, object neuObject, object param)
        {
            try
            {
                this.tv = sender as FS.HISFC.Components.Common.Controls.tvPatientList;
            }
            catch { }
            //����
            this.lsvBedView.Clear();
            //this.PaintListView();
            //������
            this.CreateHeaders();
            this.RefreshView();
            return null;
        }

        /// <summary>
        /// ���ݴ�λ��Ϣ����ListView�Ľڵ�
        /// </summary>
        /// <param name="bed"></param>
        /// <returns></returns>
        private ListViewItem GetListViewInfo(FS.HISFC.Models.Base.Bed bed)
        {
            System.Windows.Forms.ListViewItem lvi = new ListViewItem();

            //ȥ�����ŵ�ǰ��λ,ȡ������ַ��� 
            string tempBedNo = bed.ID.Length > 4 ? bed.ID.Substring(4) : bed.ID;

            //Get patientinfo
            FS.HISFC.Models.RADT.PatientInfo patient = null;
            //������ڻ���,�򽫻�����Ϣ������lvi��Tag������(����Ҳ����������)
            if (bed.InpatientNO.Trim() != "N" && bed.InpatientNO.Trim() !="")
            {
                //ȡ���߻�����Ϣ
                patient = this.radtManager.QueryPatientInfoByInpatientNO(bed.InpatientNO);
                if (patient == null || patient.ID == "")
                {
                    MessageBox.Show(bed.InpatientNO + FS.FrameWork.Management.Language.Msg("����û�ҵ���"));
                   
                }
                 try
                 {
                    //����ǰ���,�򽫴�λ�����˻���
                    if (bed.Status.ID.ToString() == FS.HISFC.Models.Base.EnumBedStatus.W.ToString())
                    {
                        bed.Memo = patient.Name;
                        lvi.Tag = bed;
                    }
                    else
                    {
                        patient.PVisit.PatientLocation.Bed = bed;
                        lvi.Tag = patient;
                    }
                    lvi.SubItems.Clear();
                    lvi.SubItems.Add(patient.Sex.Name);
                    lvi.SubItems.Add(patient.PID.PatientNO);
                    lvi.SubItems.Add(patient.PVisit.PatientLocation.Dept.Name);
                    lvi.SubItems.Add(bed.Status.Name);
                    lvi.SubItems.Add(bed.User03);
                    lvi.SubItems.Add(bed.BedGrade.Name);
                    lvi.SubItems.Add(patient.Disease.Tend.Name);
                    lvi.SubItems.Add(this.radtManager.GetFoodName(patient.ID));

                    //���ݲ����Ƿ���ʾ��������
                    if (bShowDept)
                    {
                        lvi.Text = bed.Dept.Name + "��" + tempBedNo + "��" + patient.Name;
                    }
                    else
                    {

                        lvi.Text = "��" + tempBedNo + "��\n" + patient.Name;
                        //{F0C48258-8EFB-4356-B730-E852EE4888A0}
                        //���ص���ʾ��ɫ����...
                        //if (patient.ExtendFlag1 == "1")
                        //{
                        //    lvi.BackColor = Color.Orange;//{68D58637-2A56-49a0-8F7C-56E35CE44D5B}
                        //}
                        //#region {68D58637-2A56-49a0-8F7C-56E35CE44D5B}
                        //else if (patient.ExtendFlag1 == "2")
                        //{
                        //    lvi.BackColor = Color.Red;
                        //}
                        //#endregion
                    }
                }
                catch { }
            }
            else
            {
                //����λ��Ϣ������lvi��Tag������
                lvi.Tag = bed;
                lvi.SubItems.Clear();
                lvi.SubItems.Add(bed.Sex.Name);
                lvi.SubItems.Add("");
                lvi.SubItems.Add(bed.NurseStation.Name);
                lvi.SubItems.Add(bed.Status.Name);
                lvi.SubItems.Add(bed.User03);
                lvi.SubItems.Add(bed.BedGrade.Name);
                lvi.Text = "��" + tempBedNo + "��";
            }


            //�����λ��ռ��(�л���,����,�Ҵ�,���)
            if (bed.InpatientNO != "N" && 
                bed.Status.ID.ToString() != "W" && 
                bed.Status.ID.ToString() != "R" 
                && bed.Status.ID.ToString() != "H")
            {
                lvi.ImageIndex = GetIconIndex(patient);// {297FED84-CB6C-41f0-86DD-51BF035C2D36}
                //if (lvi.ImageIndex == 7 )
                //{
                //    lvi.BackColor = Color.Red;
                //}
            }
            else
            {
                bool isHasBaby = false;
                if (patient == null)
                {
                    isHasBaby = false;
                }
                else
                {
                    isHasBaby = patient.IsHasBaby;
                }
                lvi.ImageIndex = GetIconIndex(bed.Sex.ID.ToString(), bed, isHasBaby);// {297FED84-CB6C-41f0-86DD-51BF035C2D36}
            }

            #region ����λʹ������Ϣ

            if (bed.InpatientNO != "N" && !bed.ID.Contains("MT"))//����
            {
                this.patientNunm++;
                //if (bed.Status.ID.ToString() == "O")
                //{
                    if (patient.IsHasBaby)
                    {
                        int babyCount = this.radtManager.IsMotherHasBabiesInHos(bed.InpatientNO);

                        if (babyCount > 0)
                        {
                            motherCount++;
                            babyCountALL = babyCountALL + babyCount;
                        }
                    }
                //}
            }
            #endregion

            return lvi;
        }


        /// <summary>
        /// ͨ����λ���Ժ�״̬����ͼƬindex
        /// </summary>
        /// <param name="property"></param>
        /// <param name="bed"></param>
        /// <returns></returns>
        private int GetIconIndex(string property, FS.HISFC.Models.Base.Bed bed,bool isHasBaby)
        {
            int n = 0;
            if (bed.IsPrepay)
            {//ԤԼ
                n = 12;
            }
            else
            {//������ԤԼ�Ĵ�
                switch (bed.Status.ID.ToString())
                {
                    case "C"://close
                        n = 3;
                        break;
                    case "H"://�Ҵ�
                        n = 11;
                        break;
                    case "O"://ռ��
                        //Occupied 
                        n = 4;
                        break;
                    case "U":
                        //Unoccupied 
                        n = 0;
                        break;
                    case "K":
                        n = 13;
                        //��Ⱦ��
                        break;
                    case "I":
                        n = 13;
                        //�����
                        break;
                    case "W"://����
                        n = 9;
                        break;
                    //�ż�
                    case "R":
                        n = 10;
                        break;
                    default:
                        n = 0;//{BE12C15F-4DA8-457c-ABFF-54731C044662}
                        break;

                }
            }
            if (n == 0)
            {
                switch (isHasBaby)// {297FED84-CB6C-41f0-86DD-51BF035C2D36}
                {
                    case false:
                        n = 0;
                        break;
                    case true:
                        n = 13;
                        break;
                    default:
                        n = 0;
                        break;
                }
            }
            if (n == 0)
            {
                switch (property)
                {
                    case "M":
                        n = 2;
                        break;
                    case "F":
                        n = 1;
                        break;
                    default:
                        n = 0;//{BE12C15F-4DA8-457c-ABFF-54731C044662}
                        break;
                }
            }
            return n;

        }


        /// <summary>
        /// ͨ�����߻������Է���ͼƬindex
        /// </summary>
        /// <param name="nursetype"></param>
        /// <returns></returns>
        private int GetIconIndex(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            //{36E3CA9D-FD23-42b5-802E-C365C04D93A0}
            string nursetype = "";
            nursetype = patient.Disease.Tend.Name;
            int n;

            if (patient.IsHasBaby)// {297FED84-CB6C-41f0-86DD-51BF035C2D36}
            {
                n = 13;
                return n;
            }
            if (nursetype.IndexOf("һ������") >= 0 || nursetype.IndexOf("1������") >= 0
                || nursetype.IndexOf("I������") >= 0 || nursetype.IndexOf("�񼶻���") >= 0)
            {
                n = 6;
            }
            else if (nursetype.IndexOf("��������") >= 0 || nursetype.IndexOf("2������") >= 0
                || nursetype.IndexOf("II������") >= 0 || nursetype.IndexOf("�򼶻���") >= 0)
            {
                n = 5;
            }
            else if (nursetype.IndexOf("��������") >= 0 || nursetype.IndexOf("3������") >= 0
                || nursetype.IndexOf("III������") >= 0 || nursetype.IndexOf("�󼶻���") >= 0)
            {
                n = 4;
            }
            else if (nursetype.IndexOf("��Σ") >= 0)
            {
                n = 7;
            }
            else if (nursetype.IndexOf("��֢") >= 0)
            {
                n = 8;
            }
            else if (nursetype.IndexOf("�ؼ�") >= 0)
            {
                n = 9;
            }
            else
            {
                n = 4;
            }

            return n;

        }


        
        /// <summary>
        /// �������ò˵�List����ʾ��ʽ
        /// </summary>
        /// <param name="flag"></param>
        private void MenuSetControl(bool flag)
        {
            //��λ�б�ɼ�ʱ,����ʾ"��Ƭ"�˵�;������ʾ"��Ƭ"�˵�.
            if (this.lsvBedView.Visible)
            {
                switch (this.lsvBedView.View)
                {
                    case View.LargeIcon:
                        this.mnuI_Large.Checked = flag;
                        break;
                    case View.SmallIcon:
                        this.mnuI_Small.Checked = flag;
                        break;
                    case View.List:
                        this.mnuI_List.Checked = flag;
                        break;
                    case View.Details:
                        this.mnuI_Detail.Checked = flag;
                        break;
                    default:
                        this.mnuI_Large.Checked = flag;
                        break;
                }
                this.mnuI_card.Checked = false;
                this.mnuPrintCard.Visible = false;
                this.mmuBedChange.Visible = false;// {407F4A63-CC38-4842-BFDA-995E1C3FC664}
            }
            else
            {
                this.mnuI_Large.Checked = false;
                this.mnuI_Small.Checked = false;
                this.mnuI_List.Checked = false;
                this.mnuI_Detail.Checked = false;
                this.mnuI_card.Checked = true;
                this.mnuPrintCard.Visible = true;
                this.mmuBedChange.Visible = true;// {407F4A63-CC38-4842-BFDA-995E1C3FC664}
            }

        }

        /// <summary>
        /// ����λ�˵��е�check������Ϊfalse
        /// </summary>
        private void Clearmenu()
        {
            for (int i = 0; i < this.mnuSet.Items.Count; i++)
            {
                ((ToolStripMenuItem) this.mnuSet.Items[i]).Checked = false;
            }
        }


        ///// <summary>
        ///// 
        ///// </summary>
        //private void ControlAdd()
        //{
        //    if (this.lsvBedView.SelectedItems[0].Tag.GetType().ToString() == "FS.HISFC.Models.RADT.PatientInfo")
        //    {
        //        FS.HISFC.Models.RADT.PatientInfo patient = (FS.HISFC.Models.RADT.PatientInfo)this.lsvBedView.SelectedItems[0].Tag;
        //        this.ucBedChange1.NurseCell = this.NurseCode;
        //        this.ucBedChange1.myPatientInfo = patient;

        //        FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.ucBedChange1);
        //        if (FS.FrameWork.WinForms.Classes.Function.PopForm.DialogResult == DialogResult.OK)
        //        {
        //            //ˢ�´�λ�б�
        //            this.RefreshView();
        //        }
        //    }

        //}


        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private int ChangeItems(int a, int b)
        {
            int parm;
            FS.HISFC.Models.RADT.PatientInfo obj_a, obj_b;

            //�����ͬһ����,�򷵻�
            if (a == b) return -1;

            //���˶Ե���λ
            if (this.lsvBedView.Items[a].Tag.GetType().ToString() == "FS.HISFC.Models.RADT.PatientInfo" && this.lsvBedView.Items[b].Tag.GetType().ToString() == "FS.HISFC.Models.RADT.PatientInfo")
            {
                obj_a = (FS.HISFC.Models.RADT.PatientInfo)this.lsvBedView.Items[a].Tag;
                obj_b = (FS.HISFC.Models.RADT.PatientInfo)this.lsvBedView.Items[b].Tag;

                if (obj_a.PVisit.PatientLocation.Bed.Status.ID.ToString() == "W" || obj_b.PVisit.PatientLocation.Bed.Status.ID.ToString() == "W")//{8ADDE7FC-0CF5-4e86-9B0A-41DFD058A400}
                {
                    MessageBox.Show("�������Ĵ�λ��һ״̬Ϊ���������ܵ�����", "��ʾ��");
                    return -1;
                }
                if (obj_b.PVisit.PatientLocation.Bed.Status.ID.ToString() == "C")//{8ADDE7FC-0CF5-4e86-9B0A-41DFD058A400}
                {
                    MessageBox.Show(obj_b.Name + "���ڱ�����������ת����", "��ʾ��");
                    return -1;
                }
                else if(obj_a.PVisit.PatientLocation.Bed.Status.ID.ToString() == "C")
                {
                    MessageBox.Show(obj_a.Name + "���ڱ�����������ת����", "��ʾ��");
                    return -1;
                }
                //
                if (MessageBox.Show("�Ƿ�Ԥ����" + obj_a.Name + "���롰" + obj_b.Name + "������", "��ʾ��", System.Windows.Forms.MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Cancel) return -1;
                //
                try
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    this.radtManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    //�����ߴ�λ�Ե�����
                    parm = this.radtManager.SwapPatientBed(obj_a, obj_b);
                    if (parm == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.Err = "����ʧ�ܣ�\n" + this.radtManager.Err;
                        return -1;
                    }
                    else if (parm == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.Err = "����ʧ��! \n������Ϣ�б䶯,��ˢ�µ�ǰ����";
                        return -1;
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();
                    base.OnRefreshTree();

                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    return -1;
                }

                return 1;

            }
            else if (this.lsvBedView.Items[a].Tag.GetType().ToString() == "FS.HISFC.Models.RADT.PatientInfo")
            {
                //����a����b��
                return (this.TransPatientToBed(a, b));

            }
            else if (this.lsvBedView.Items[b].Tag.GetType().ToString() == "FS.HISFC.Models.RADT.PatientInfo")
            {
                //����b����a��
                return (this.TransPatientToBed(b, a));
            }

            return 0;
        }


        /// <summary>
        /// ���˻�������
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private int TransPatientToBed(int a, int b)
        {
            int parm = 0;
            FS.HISFC.Models.RADT.Location obj_location = new FS.HISFC.Models.RADT.Location();
            FS.HISFC.Models.RADT.PatientInfo obj_a;

            //ȡListView�еĴ�λ�ͻ�����Ϣ
            obj_a = (FS.HISFC.Models.RADT.PatientInfo)this.lsvBedView.Items[a].Tag;
            obj_location.Bed = (FS.HISFC.Models.Base.Bed)this.lsvBedView.Items[b].Tag;

            //���ų�ȥǰ��λ
            string bedNo = obj_location.Bed.ID.Length > 4 ? obj_location.Bed.ID.Substring(4) : obj_location.Bed.ID;

            if (obj_location.Bed.Status.ID.ToString() == "W")
            {
                MessageBox.Show("����Ϊ [" + bedNo + " ]�Ĵ�λ״̬Ϊ����������ռ�ã�", "��ʾ��");
                return -1;
            }
            else if (obj_location.Bed.Status.ID.ToString() == "C")
            {
                MessageBox.Show("����Ϊ [" + bedNo + " ]�Ĵ�λ״̬Ϊ�رգ�����ռ�ã�", "��ʾ��");
                return -1;
            }
            else if (obj_location.Bed.IsPrepay)
            {
                MessageBox.Show("����Ϊ [" + bedNo + " ]�Ĵ�λ�Ѿ�ԤԼ������ռ�ã�", "��ʾ��");
                return -1;
            }
            else if (!obj_location.Bed.IsValid)
            {
                MessageBox.Show("����Ϊ [" + bedNo + " ]�Ĵ�λ�Ѿ�ͣ�ã�����ռ�ã�", "��ʾ��");
                return -1;
            }
            else if (((FS.HISFC.Models.Base.EnumServiceBase)(obj_a.PVisit.PatientLocation.Bed.Status)).ID.ToString() == "C")
            {
                MessageBox.Show(obj_a.Name+"���ڱ�����������ת��", "��ʾ��");
                return -1;
            }
            //
            if (MessageBox.Show("�Ƿ�Ԥ����" + obj_a.Name + "��ת�Ƶ�[" + bedNo + "]�Ŵ�", "��ʾ��", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No) return -1;
            //
            try
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                this.radtManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                //���˻�������
                parm = this.radtManager.TransferPatient(obj_a, obj_location);
                if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "����ʧ�ܣ�\n" + this.radtManager.Err;
                    return -1;
                }
                else if (parm == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "����ʧ��! \n������Ϣ�б䶯�����Ѿ���Ժ,��ˢ�µ�ǰ����";
                    return -1;
                }

                //Ӥ������һ�𻻴�
                if (!obj_a.PID.PatientNO.Contains("B"))// {CDB01BF4-B40F-4cdc-9F0D-23F074290136}
                {
                    ArrayList alBabys = new ArrayList();

                    alBabys = this.radtManager.QueryBabiesByMother(obj_a.ID);
                    {
                        foreach (FS.HISFC.Models.RADT.PatientInfo bInfo in alBabys)
                        {
                            parm = this.radtManager.TransferPatient(bInfo, obj_location);
                            if (parm == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                this.Err = "����ʧ�ܣ�\n" + this.radtManager.Err;
                                return -1;
                            }
                            else if (parm == 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                this.Err = "����ʧ��! \n������Ϣ�б䶯�����Ѿ���Ժ,��ˢ�µ�ǰ����";
                                return -1;
                            }
                        }
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                base.OnRefreshTree();
              
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return 1;
        }

        protected override void  OnLoad(EventArgs e)
        {

            //{56BEF09F-F403-4518-94D9-FBE52890EE2F}
            FS.HISFC.Models.Base.Employee curremployee = personMgr.GetEmployeeByCode(FS.FrameWork.Management.Connection.Operator.ID);

            FS.HISFC.Models.Base.Department currDept = (FS.HISFC.Models.Base.Department)(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept);
            string hospitalname = "";
            string hospitalybcode = "";
            if (currDept.HospitalName.Contains("˳��"))
            {
                hospitalname = "˳�°���������ҽԺ";
                hospitalybcode = "H44060600494";
            }
            else
            {
                hospitalname = "���ݰ���������ҽԺ";
                hospitalybcode = "H44010600124";
            }

            string gjcode = "";
            if (curremployee != null)
            {
                if (string.IsNullOrEmpty(curremployee.InterfaceCode))
                {
                    gjcode = curremployee.UserCode;
                }
                else
                {
                    gjcode = curremployee.InterfaceCode;
                }
            }

            base.OnStatusBarInfo(null, "�������ƣ�" + hospitalname + "  ����ҽ�����룺" + hospitalybcode + "  ҽ����ʿ���룺" + gjcode + "");


 	        base.OnLoad(e);
        }
        private int ai;
        private int bi;
        private void lsvBedView_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
        {
            lsvBedView.DoDragDrop(e.Item, DragDropEffects.Move);
            ListViewItem lvi = (ListViewItem)e.Item;
            ai = lvi.Index;
            //��������
            if (ChangeItems(ai, bi) == 1)
            {
                //ˢ�´�λ***�����
            }
        }
        private void lsvBedView_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            Point clientPoint = lsvBedView.PointToClient(new Point(e.X, e.Y));
            bi = this.lsvBedView.GetItemAt(clientPoint.X, clientPoint.Y).Index;
            if (ai != bi)
            {
                this.lsvBedView.Items[bi].Focused = true;
            }
        }

        private void lsvBedView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        //�Ҽ��˵�����
        private void lsvBedView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.lsvBedView.SelectedItems.Count == 0)
                {
                    this.lsvBedView.ContextMenuStrip = this.mnuSet;
                    this.MenuSetControl(true);
                }
                else if (this.lsvBedView.SelectedItems[0].Tag.GetType().ToString() == "FS.HISFC.Models.RADT.PatientInfo")
                {
                    this.lsvBedView.ContextMenuStrip = this.mnuBed;
                    Clearmenu();
                    
                }
                else if (this.lsvBedView.SelectedItems[0].Tag.GetType().ToString() == "FS.HISFC.Models.Base.Bed")
                {
                    if (((FS.HISFC.Models.Base.Bed)this.lsvBedView.SelectedItems[0].Tag).Status.ID.ToString() == "W")
                    {
                        this.lsvBedView.ContextMenuStrip = this.mnuDealBed;
                    }
                    else
                    {
                        //{C29C2D37-D519-49af-AFEA-4981B1A013AE}
                        //this.lsvBedView.ContextMenuStrip = null;
                        if (!this.isOnlyEmptyBedCanReceive)
                        {
                            this.lsvBedView.ContextMenuStrip = this.mnuReceive;
                        }
                        else if (((FS.HISFC.Models.Base.Bed)this.lsvBedView.SelectedItems[0].Tag).Status.ID.ToString() == "U")
                        {
                            this.lsvBedView.ContextMenuStrip = this.mnuReceive;
                        }
                    }
                }
                else
                {
                    this.lsvBedView.ContextMenuStrip = null;
                }
            }

        }


        private void lsvBedView_ItemActivate(object sender, System.EventArgs e)
        {
            //������ڵ�ʱ,�����λ������ռ��,����ʾ������Ϣ
            this.ShowPatientInfo();
        }
        
        protected override int  OnSetValue(object neuObject, TreeNode e)
        {
            if (e == null || e.Tag == null)
            {
                this.RefreshView();
                return 0;
            }
            if(e.Tag.GetType() == typeof(FS.HISFC.Models.RADT.PatientInfo))
            {
            }else if(e.Tag !=null)
            {
                switch(e.Tag.ToString())
                {
                    case  "In"://��������
                        this.RefreshView();
                        break;
                    default:
                        break;
                }
            }
 	        return 0;
        }
        #endregion

        #region �˵�
        private ucPatientCardList myucPCList = null;
        protected ucPatientCardList ucPatientCardList
        {
            get
            {
                if (this.myucPCList == null)
                {

                    this.myucPCList = new ucPatientCardList();
                    ArrayList al = new ArrayList();

                    if (this.alBeds == null) return null;
                    //ȡ������վ��λ��Ϣ
                    for (int i = 0; i < this.alBeds.Count; i++)
                    {
                        FS.HISFC.Models.Base.Bed bed = this.alBeds[i] as FS.HISFC.Models.Base.Bed;
                        FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();

                        //�����λ���л���,��ȡ�˻�����Ϣ
                        if (bed.InpatientNO != "N")
                        {
                            patient = this.radtManager.QueryPatientInfoByInpatientNO(bed.InpatientNO);
                            patient.Disease.Memo = this.radtManager.GetFoodName(bed.InpatientNO);
                            #region {5F752A30-7971-4b65-A84B-D233EF2A4406}
                            if (patient.Diagnoses==null)
                            {
                                patient.Diagnoses=new ArrayList();
                            }
                            ArrayList alDiagnoses = this.healthMgr.QueryDiagnoseNoOps(bed.InpatientNO);
                            if (alDiagnoses != null)
                            {
                                foreach (FS.HISFC.Models.HealthRecord.Diagnose diagnoseInfo in alDiagnoses)
                                {
                                    if (diagnoseInfo.DiagInfo.IsMain)
                                    {
                                        patient.Diagnoses.Add(diagnoseInfo.DiagInfo.ICD10.Name);
                                    }
                                }
                            }
                            #endregion
                            if (patient == null)
                            {
                                MessageBox.Show(this.radtManager.Err);
                                return null;
                            }
                        }
                        else
                        {
                            patient.PVisit.PatientLocation.Bed = bed;
                        }

                        al.Add(patient);
                    }
                    // {43D3B083-0C3D-459d-9E2E-007368A21857}
                    this.myucPCList.SetPatients(al);
                    this.groupBox1.Visible = true;
                    //this.lsvBedView.Visible = !this.myucPCList.Visible;
                    //if (IslvBedShow)
                    //{
                    //    this.lsvBedView.Dock = DockStyle.Fill;
                    //    this.Controls.Add(this.lsvBedView);
                    //}
                    //else
                    //{
                    //    this.myucPCList.Dock = DockStyle.Bottom;
                    //    this.Controls.Add(this.myucPCList);
                    //}

                    this.myucPCList.ContextMenuStrip = this.mnuSet;
                  
                 }
                 return this.myucPCList;
            }
            set
            {
                myucPCList = value;
            }
        }
        private void mnuI_Large_Click(object sender, EventArgs e)
        {
            this.lsvBedView.Visible = true;
            IslvBedShow = true;
            //{5D00E993-7774-47d1-95AA-C0F356F10BFA}
            //this.groupBox1.Visible = !IslvBedShow;
            if (this.myucPCList != null)
                this.myucPCList.Visible = false;
            this.MenuSetControl(false);
            this.lsvBedView.View = View.LargeIcon;
            this.MenuSetControl(true);
        }

        private void mnuI_Small_Click(object sender, EventArgs e)
        {
            this.lsvBedView.Visible = true;

            IslvBedShow = true;
            //{5D00E993-7774-47d1-95AA-C0F356F10BFA}
            //this.groupBox1.Visible = !IslvBedShow;
            if (this.myucPCList != null)
                this.myucPCList.Visible = false;
            this.MenuSetControl(false);
            this.lsvBedView.View = View.SmallIcon;
            this.MenuSetControl(true);
        }

        private void mnuI_List_Click(object sender, EventArgs e)
        {
            this.lsvBedView.Visible = true;
            IslvBedShow = true;
            //{5D00E993-7774-47d1-95AA-C0F356F10BFA}
            //this.groupBox1.Visible = !IslvBedShow;
            if (this.myucPCList != null)
                this.myucPCList.Visible = false;

            this.MenuSetControl(false);
            this.lsvBedView.View = View.List;
            this.MenuSetControl(true);
        }

        private void mnuI_Detail_Click(object sender, EventArgs e)
        {
         
            this.lsvBedView.Visible = true;
            IslvBedShow = true;
            //{5D00E993-7774-47d1-95AA-C0F356F10BFA}
            //this.groupBox1.Visible = !IslvBedShow;
            if (this.myucPCList != null)
                this.myucPCList.Visible = false;
            this.MenuSetControl(false);
            this.lsvBedView.View = View.Details;
            this.MenuSetControl(true);
        }

        private void mnuI_card_Click(object sender, EventArgs e)
        {
            IslvBedShow = false;
            this.lsvBedView.Visible = false;
            //{5D00E993-7774-47d1-95AA-C0F356F10BFA}
            //this.groupBox1.Visible = !IslvBedShow;
            this.ucPatientCardList.Visible = true;
            this.MenuSetControl(false);
            this.MenuSetControl(true);
            
        }

        private void mnuI_Info_Click(object sender, EventArgs e)
        {
            ShowPatientInfo();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuI_Wap_Click(object sender, EventArgs e)
        {
            if (wapType == 0)
            {
                MessageBox.Show("�����������\r\n������������ϵҽ��ơ�����������ƣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else if (wapType == 2)
            {
                FS.HISFC.Models.RADT.PatientInfo patient = (FS.HISFC.Models.RADT.PatientInfo)this.lsvBedView.SelectedItems[0].Tag;

                if (patient != null && (SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(patient.Pact.ID).PayKind.ID == "02" || SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(patient.Pact.ID).PayKind.ID == "03"))
                {
                    MessageBox.Show("��" + SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(patient.Pact.ID).Name + "�������������\r\n������������ϵҽ��ơ�����������ƣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }

            if (this.ucBedChange1 == null)
            {
                this.ucBedChange1 = new ucBedChange();
            }
            this.ucBedChange1.SetType = BedType.Wap;

            this.ControlAdd();
        }

        #region ��ʿվ���û��߲��ر��
        //{F0C48258-8EFB-4356-B730-E852EE4888A0}
        private void mnuI_Add_Click(object sender, EventArgs e)
        {
            FS.HISFC.BizLogic.RADT.InPatient patient = new FS.HISFC.BizLogic.RADT.InPatient();        
            try
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                patient.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                if (patient.UpdateBZ_Info(((HISFC.Models.RADT.PatientInfo)this.lsvBedView.SelectedItems[0].Tag).ID) != -1)
                {
                    this.lsvBedView.SelectedItems[0].BackColor = Color.Red;
                    FS.FrameWork.Management.PublicTrans.Commit();                    
                    this.Err = "���ò���Ϊ���أ�";
                    MessageBox.Show(this.Err);
                    //ˢ�´�λ�б�
                    this.RefreshView();
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "���²��˲���ʧ�ܣ�" + patient.Err;
                    MessageBox.Show(this.Err);
                }
            }
            catch { }
        }
        private void mnuI_Cancel_Click(object sender, EventArgs e)
        {
            FS.HISFC.BizLogic.RADT.InPatient patient = new FS.HISFC.BizLogic.RADT.InPatient();
            try
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                patient.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                if (patient.UpdatePT_Info(((HISFC.Models.RADT.PatientInfo)this.lsvBedView.SelectedItems[0].Tag).ID) != -1)
                {
                    FS.FrameWork.Management.PublicTrans.Commit();
                    this.Err = "���ò���Ϊ��ͨ��";
                    MessageBox.Show(this.Err);
                    //ˢ�´�λ�б�
                     this.RefreshView();
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "���²��˲���ʧ�ܣ�" + patient.Err;
                    MessageBox.Show(this.Err);
                }
            }
            catch { }
        }
        #endregion

        private void mnuI_Unwap_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();
            FS.HISFC.Models.Base.Bed objBed = new FS.HISFC.Models.Base.Bed();

            objBed = (FS.HISFC.Models.Base.Bed)this.lsvBedView.SelectedItems[0].Tag;

            patient.ID = objBed.InpatientNO;
            patient = this.radtManager.QueryPatientInfoByInpatientNO(patient.ID);
            if (patient == null)
            {
                MessageBox.Show(radtManager.Err);
                return;
            }
            try
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(this.radtManager.Connection);
                //SQLCA.BeginTransaction();

                this.radtManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                //�������
                //if (this.radtManager.UnWrapPatientBed(patient, objBed.ID, "2") == 0)
                if (radtIntergrate.UnWapBed(patient, objBed, "2") != -1)
                {
                    FS.FrameWork.Management.PublicTrans.Commit();
                    //���»��������λ����
                    objBed.InpatientNO = "N";
                    objBed.Status.ID = FS.HISFC.Models.Base.EnumBedStatus.U;
                    this.lsvBedView.Items[this.lsvBedView.SelectedIndices[0]] = this.GetListViewInfo(objBed);
                    //
                    this.Err = "����ɹ���";
                    MessageBox.Show(this.Err);
                    //ˢ�´�λ�б�
                    this.RefreshView();
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "����ʧ�ܣ�" + this.radtManager.Err;
                    MessageBox.Show(this.Err);
                }
            }
            catch { }
        }

        //{C29C2D37-D519-49af-AFEA-4981B1A013AE}
        private void mnuI_Arrange_Click(object sender, EventArgs e)
        {
            if (this.lsvBedView.SelectedItems.Count > 0)
            {
                if (tv != null)
                {
                    FS.HISFC.Models.Base.Bed currBed;
                    if (this.lsvBedView.SelectedItems.Count > 0)
                    {
                        currBed = this.lsvBedView.SelectedItems[0].Tag as FS.HISFC.Models.Base.Bed;

                        foreach (TreeNode tn in this.tv.Nodes)
                        {
                            if (tn.Tag.ToString() == EnumPatientType.Arrive.ToString()
                             || tn.Tag.ToString() == EnumPatientType.ShiftIn.ToString())
                            {
                                if (tn.Nodes.Count > 0)
                                {
                                    this.arrangeBed(currBed.ID);
                                    this.tv.SelectedNode = tn.FirstNode;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        ucBedChange ucBedChange1 = new ucBedChange();
        private void ControlAdd()
        {
            if (this.lsvBedView.SelectedItems[0].Tag.GetType()
                == typeof(FS.HISFC.Models.RADT.PatientInfo))
            {
                FS.HISFC.Models.RADT.PatientInfo patient = (FS.HISFC.Models.RADT.PatientInfo)this.lsvBedView.SelectedItems[0].Tag;

            
                this.ucBedChange1.PatientInfo = patient;

                FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.ucBedChange1);
                if (FS.FrameWork.WinForms.Classes.Function.PopForm.DialogResult == DialogResult.OK)
                {
                    //ˢ�´�λ�б�
                    this.RefreshView();
                }
            }
        }
        // {407F4A63-CC38-4842-BFDA-995E1C3FC664}
        private void BedChange(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            this.ucBedChange1.PatientInfo = patient;

            FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.ucBedChange1);
            if (FS.FrameWork.WinForms.Classes.Function.PopForm.DialogResult == DialogResult.OK)
            {
                //ˢ�´�λ�б�
                this.RefreshView();
            }
        }

        #endregion

        private void lsvBedView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if(this.ListViewItemChanged!=null)
                this.ListViewItemChanged(sender, e);
        }

        /// <summary>
        /// {8ADDE7FC-0CF5-4e86-9B0A-41DFD058A400}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnu_ChangeBed_Click(object sender, EventArgs e)
        {
            if (this.ucBedChange1 == null) this.ucBedChange1 = new ucBedChange();
            this.ucBedChange1.SetType = BedType.Change;

            this.ControlAdd();
            base.OnRefreshTree();
        }

        /// <summary>
        /// {46063507-0C5A-405d-BD9D-4762ADE8DE02}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuPrintCard_Click(object sender, EventArgs e)
        {
            this.ucPatientCardList.PrintCard();
        }
        // {407F4A63-CC38-4842-BFDA-995E1C3FC664}
        private void mnu_BedChange_Click(object sender, EventArgs e)
        {
            if (this.ucBedChange1 == null) this.ucBedChange1 = new ucBedChange();
            this.ucBedChange1.SetType = BedType.Change;
            FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();
            int i = this.ucPatientCardList.ChoosePatientInfo(ref patient);
            if (i < 0 || patient == null)
            {
                MessageBox.Show("����һλ����Ϊ��ɫ��Ƭ,�������л�����");
                return;
            }
            this.BedChange(patient);
            base.OnRefreshTree();
        }

    }
}
