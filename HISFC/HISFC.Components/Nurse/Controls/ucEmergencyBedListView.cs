using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.Nurse.Controls
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
                    alBeds = manager.QueryBedList( empl.Dept.ID );
                }
                return alBeds;
            }
        }

        protected FS.HISFC.BizLogic.RADT.InPatient radtManager = new FS.HISFC.BizLogic.RADT.InPatient();
        protected FS.HISFC.BizLogic.RADT.OutPatient radtOutPatientManager = new FS.HISFC.BizLogic.RADT.OutPatient();
        protected FS.HISFC.BizLogic.Registration.Register regManager = new FS.HISFC.BizLogic.Registration.Register();

        protected FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();

        protected FS.HISFC.Components.Common.Controls.tvPatientList tv = null;//��ǰ������

        private string Err;
        System.Windows.Forms.ToolTip toolTip1 = new System.Windows.Forms.ToolTip();
        #endregion

        #region ����
        protected bool bShowDept = false;
        /// <summary>
        /// 
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
            this.lsvBedView.BeginUpdate();
            //��ʾ����
            this.PaintListView();
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
            colHead.Width = 150;
            this.lsvBedView.Columns.Add( colHead );
            //
            colHead = new ColumnHeader();
            colHead.Text = "�Ա�";
            colHead.Width = 40;
            this.lsvBedView.Columns.Add( colHead );
            //
            colHead = new ColumnHeader();
            colHead.Text = "������";
            colHead.Width = 80;
            this.lsvBedView.Columns.Add( colHead );
            //
            colHead = new ColumnHeader();
            colHead.Text = "����";
            colHead.Width = 100;
            this.lsvBedView.Columns.Add( colHead );
            //
            colHead = new ColumnHeader();
            colHead.Text = "״̬";
            colHead.Width = 100;
            this.lsvBedView.Columns.Add( colHead );

        }


        /// <summary>
        /// ��ʾ����
        /// </summary>
        protected virtual void PaintListView()
        {
            //���
            this.lsvBedView.Items.Clear();

            try
            {
                //ѭ������λ��ӵ��б���
                foreach (FS.HISFC.Models.Base.Bed bed in this.AlBeds)
                {
                    ListViewItem lst = this.GetListViewInfo( bed );
                    if (lst != null)
                        this.lsvBedView.Items.Add( lst );

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message );
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
                FS.HISFC.Models.Registration.Register patient = this.lsvBedView.SelectedItems[0].Tag as FS.HISFC.Models.Registration.Register;

                if (patient != null && tv != null)
                {
                    //ȡ������Ϣ�������б��еĽڵ�λ��
                    TreeNode node = this.tv.FindNode( 0, patient );
                    //�������ͽڵ�ѡ���ҵ��Ľڵ�
                    if (node != null)
                        this.tv.SelectedNode = node;
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
            catch
            {
            }
            //����
            this.lsvBedView.Clear();
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
            string tempBedNo = bed.ID.Length > 4 ? bed.ID.Substring( 4 ) : bed.ID;

            //Get patientinfo
            FS.HISFC.Models.Registration.Register patient = null;
            //������ڻ���,�򽫻�����Ϣ������lvi��Tag������(����Ҳ����������)
            if (bed.InpatientNO.Trim() != "N" && bed.InpatientNO.Trim() != "")
            {
                //ȡ���߻�����Ϣ
                patient = this.regManager.GetByClinic( bed.InpatientNO );
                if (patient == null || patient.ID == "")
                {
                    MessageBox.Show( bed.InpatientNO + FS.FrameWork.Management.Language.Msg( "����û�ҵ���" ) );

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
                    lvi.SubItems.Add( patient.Sex.Name );
                    lvi.SubItems.Add( patient.PID.PatientNO );
                    lvi.SubItems.Add( patient.PVisit.PatientLocation.Dept.Name );
                    lvi.SubItems.Add( bed.Status.Name );

                    //���ݲ����Ƿ���ʾ��������
                    if (bShowDept)
                    {
                        lvi.Text = bed.Dept.Name + "��" + tempBedNo + "��" + patient.Name;
                    }
                    else
                    {
                        lvi.Text = "��" + tempBedNo + "��\n" + patient.Name;
                    }
                }
                catch
                {
                }
            }
            else
            {
                //����λ��Ϣ������lvi��Tag������
                lvi.Tag = bed;
                lvi.SubItems.Clear();
                lvi.SubItems.Add( bed.Sex.Name );
                lvi.SubItems.Add( "" );
                lvi.SubItems.Add( bed.NurseStation.Name );
                lvi.SubItems.Add( bed.Status.Name );
                lvi.Text = "��" + tempBedNo + "��";
            }


            //�����λ��ռ��(�л���,����,�Ҵ�,���)
            if (bed.InpatientNO != "N" &&
                bed.Status.ID.ToString() != "W" &&
                bed.Status.ID.ToString() != "R"
                && bed.Status.ID.ToString() != "H")
            {
                //lvi.ImageIndex = GetIconIndex(patient.Disease.Tend.Name);
                lvi.ImageIndex = 4;
            }
            else
            {
                lvi.ImageIndex = GetIconIndex( bed.Sex.ID.ToString(), bed );
            }


            return lvi;
        }


        /// <summary>
        /// ͨ����λ���Ժ�״̬����ͼƬindex
        /// </summary>
        /// <param name="property"></param>
        /// <param name="bed"></param>
        /// <returns></returns>
        private int GetIconIndex(string property, FS.HISFC.Models.Base.Bed bed)
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
                        n = 0;
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
        private int GetIconIndex(string nursetype)
        {
            int n;
            switch (nursetype)
            {
                case "һ������":// 1 grade
                    n = 6;
                    break;
                case "��������"://2 grade
                    n = 5;
                    break;
                case "��������"://3 grade
                    n = 4;
                    break;
                case "��Σ": //��Σ
                    n = 7;
                    break;
                case "��֢"://��֢
                    n = 8;
                    break;
                default:
                    n = 4;
                    break;
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
            }
            else
            {
                this.mnuI_Large.Checked = false;
                this.mnuI_Small.Checked = false;
                this.mnuI_List.Checked = false;
                this.mnuI_Detail.Checked = false;
                this.mnuI_card.Checked = true;
            }

        }

        /// <summary>
        /// ����λ�˵��е�check������Ϊfalse
        /// </summary>
        private void Clearmenu()
        {
            for (int i = 0; i < this.mnuSet.Items.Count; i++)
            {
                ((ToolStripMenuItem)this.mnuSet.Items[i]).Checked = false;
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

        //        FS.FrameWork.Interface.Classes.Function.PopShowControl(this.ucBedChange1);
        //        if (FS.FrameWork.Interface.Classes.Function.PopForm.DialogResult == DialogResult.OK)
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
            /*int parm;
            FS.HISFC.Models.RADT.PatientInfo obj_a, obj_b;

            //�����ͬһ����,�򷵻�
            if (a == b) return -1;

            //���˶Ե���λ
            if (this.lsvBedView.Items[a].Tag.GetType().ToString() == "FS.HISFC.Models.RADT.PatientInfo" && this.lsvBedView.Items[b].Tag.GetType().ToString() == "FS.HISFC.Models.RADT.PatientInfo")
            {
                obj_a = (FS.HISFC.Models.RADT.PatientInfo)this.lsvBedView.Items[a].Tag;
                obj_b = (FS.HISFC.Models.RADT.PatientInfo)this.lsvBedView.Items[b].Tag;

                if (obj_a.PVisit.PatientLocation.Bed.Status.ID.ToString() == "W" || obj_a.PVisit.PatientLocation.Bed.Status.ID.ToString() == "W")
                {
                    MessageBox.Show("�������Ĵ�λ��һ״̬Ϊ���������ܵ�����", "��ʾ��");
                    return -1;
                }
                //
                if (MessageBox.Show("�Ƿ�Ԥ����" + obj_a.Name + "���롰" + obj_b.Name + "������", "��ʾ��", System.Windows.Forms.MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Cancel) return -1;
                //
                try
                {
                    FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                    SQLCA.BeginTransaction();
                    this.radtManager.SetTrans(SQLCA.Trans);
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

                    SQLCA.Commit();
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
            


            return 0;*/
            int parm;
            FS.HISFC.Models.Registration.Register obj_a, obj_b;

            //�����ͬһ����,�򷵻�
            if (a == b)
                return -1;

            //���˶Ե���λ
            if (this.lsvBedView.Items[a].Tag.GetType().ToString() == "FS.HISFC.Models.Registration.Register" && this.lsvBedView.Items[b].Tag.GetType().ToString() == "FS.HISFC.Models.Registration.Register")
            {
                obj_a = (FS.HISFC.Models.Registration.Register)this.lsvBedView.Items[a].Tag;
                obj_b = (FS.HISFC.Models.Registration.Register)this.lsvBedView.Items[b].Tag;

                if (obj_a.PVisit.PatientLocation.Bed.Status.ID.ToString() == "W" || obj_a.PVisit.PatientLocation.Bed.Status.ID.ToString() == "W")
                {
                    MessageBox.Show( "�������Ĵ�λ��һ״̬Ϊ���������ܵ�����", "��ʾ��" );
                    return -1;
                }
                //
                if (MessageBox.Show( "�Ƿ�Ԥ����" + obj_a.Name + "���롰" + obj_b.Name + "������", "��ʾ��", System.Windows.Forms.MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2 ) == System.Windows.Forms.DialogResult.Cancel)
                    return -1;
                //
                try
                {

                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                    //SQLCA.BeginTransaction();

                    this.radtOutPatientManager.SetTrans( FS.FrameWork.Management.PublicTrans.Trans );

                    //�����ߴ�λ�Ե�����
                    parm = 0;
                    parm = this.radtOutPatientManager.SwapPatientBed( obj_a, obj_b );
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
            else if (this.lsvBedView.Items[a].Tag.GetType().ToString() == "FS.HISFC.Models.Registration.Register")
            {
                //����a����b��
                return (this.TransPatientToBed( a, b ));

            }
            else if (this.lsvBedView.Items[b].Tag.GetType().ToString() == "FS.HISFC.Models.Registration.Register")
            {
                //����b����a��
                return (this.TransPatientToBed( b, a ));
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
            /*int parm = 0;
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
            //
            if (MessageBox.Show("�Ƿ�Ԥ����" + obj_a.Name + "��ת�Ƶ�[" + bedNo + "]�Ŵ�", "��ʾ��", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No) return -1;
            //
            try
            {
                FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(this.radtManager.Connection);
                SQLCA.BeginTransaction();
                this.radtManager.SetTrans(SQLCA.Trans);
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

                SQLCA.Commit();
                base.OnRefreshTree();
              
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return 1;*/
            int parm = 0;
            FS.HISFC.Models.RADT.Location obj_location = new FS.HISFC.Models.RADT.Location();
            FS.HISFC.Models.Registration.Register obj_a;

            //ȡListView�еĴ�λ�ͻ�����Ϣ
            obj_a = (FS.HISFC.Models.Registration.Register)this.lsvBedView.Items[a].Tag;
            obj_location.Bed = (FS.HISFC.Models.Base.Bed)this.lsvBedView.Items[b].Tag;

            //���ų�ȥǰ��λ
            string bedNo = obj_location.Bed.ID.Length > 4 ? obj_location.Bed.ID.Substring( 4 ) : obj_location.Bed.ID;

            if (obj_location.Bed.Status.ID.ToString() == "W")
            {
                MessageBox.Show( "����Ϊ [" + bedNo + " ]�Ĵ�λ״̬Ϊ����������ռ�ã�", "��ʾ��" );
                return -1;
            }
            else if (obj_location.Bed.Status.ID.ToString() == "C")
            {
                MessageBox.Show( "����Ϊ [" + bedNo + " ]�Ĵ�λ״̬Ϊ�رգ�����ռ�ã�", "��ʾ��" );
                return -1;
            }
            else if (obj_location.Bed.IsPrepay)
            {
                MessageBox.Show( "����Ϊ [" + bedNo + " ]�Ĵ�λ�Ѿ�ԤԼ������ռ�ã�", "��ʾ��" );
                return -1;
            }
            else if (!obj_location.Bed.IsValid)
            {
                MessageBox.Show( "����Ϊ [" + bedNo + " ]�Ĵ�λ�Ѿ�ͣ�ã�����ռ�ã�", "��ʾ��" );
                return -1;
            }
            //
            if (MessageBox.Show( "�Ƿ�Ԥ����" + obj_a.Name + "��ת�Ƶ�[" + bedNo + "]�Ŵ�", "��ʾ��", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2 ) == System.Windows.Forms.DialogResult.No)
                return -1;
            //
            try
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(this.radtManager.Connection);
                //SQLCA.BeginTransaction();

                this.radtOutPatientManager.SetTrans( FS.FrameWork.Management.PublicTrans.Trans );

                //���˻�������
                parm = this.radtOutPatientManager.TransferPatient( obj_a, obj_location );
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

        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad( e );
        }
        private int ai;
        private int bi;
        private void lsvBedView_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
        {
            lsvBedView.DoDragDrop( e.Item, DragDropEffects.Move );
            ListViewItem lvi = (ListViewItem)e.Item;
            ai = lvi.Index;
            //��������
            if (ChangeItems( ai, bi ) == 1)
            {
                //ˢ�´�λ***�����
            }
        }
        private void lsvBedView_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            Point clientPoint = lsvBedView.PointToClient( new Point( e.X, e.Y ) );
            bi = this.lsvBedView.GetItemAt( clientPoint.X, clientPoint.Y ).Index;
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
                    this.MenuSetControl( true );
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
                        this.lsvBedView.ContextMenuStrip = null;
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

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (e == null || e.Tag == null)
            {
                this.RefreshView();
                return 0;
            }
            if (e.Tag.GetType() == typeof( FS.HISFC.Models.RADT.PatientInfo ))
            {
            }
            else if (e.Tag != null)
            {
                switch (e.Tag.ToString())
                {
                    case "In"://��������
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
                    ArrayList al = new ArrayList();

                    if (this.alBeds == null)
                        return null;
                    //ȡ������վ��λ��Ϣ
                    for (int i = 0; i < this.alBeds.Count; i++)
                    {
                        FS.HISFC.Models.Base.Bed bed = this.alBeds[i] as FS.HISFC.Models.Base.Bed;
                        FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();

                        //�����λ���л���,��ȡ�˻�����Ϣ
                        if (bed.InpatientNO != "N")
                        {
                            patient = this.radtManager.QueryPatientInfoByInpatientNO( bed.InpatientNO );
                            patient.Disease.Memo = this.radtManager.GetFoodName( bed.InpatientNO );
                            if (patient == null)
                            {
                                MessageBox.Show( this.radtManager.Err );
                                return null;
                            }
                        }
                        else
                        {
                            patient.PVisit.PatientLocation.Bed = bed;
                        }

                        al.Add( patient );
                    }
                    this.myucPCList = new ucPatientCardList();
                    this.myucPCList.SetPatients( al );
                    this.myucPCList.Dock = DockStyle.Fill;
                    this.Controls.Add( this.myucPCList );
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


            if (this.myucPCList != null)
                this.myucPCList.Visible = false;
            this.MenuSetControl( false );
            this.lsvBedView.View = View.LargeIcon;
            this.MenuSetControl( true );
        }

        private void mnuI_Small_Click(object sender, EventArgs e)
        {
            this.lsvBedView.Visible = true;


            if (this.myucPCList != null)
                this.myucPCList.Visible = false;
            this.MenuSetControl( false );
            this.lsvBedView.View = View.SmallIcon;
            this.MenuSetControl( true );
        }

        private void mnuI_List_Click(object sender, EventArgs e)
        {
            this.lsvBedView.Visible = true;


            if (this.myucPCList != null)
                this.myucPCList.Visible = false;

            this.MenuSetControl( false );
            this.lsvBedView.View = View.List;
            this.MenuSetControl( true );
        }

        private void mnuI_Detail_Click(object sender, EventArgs e)
        {

            this.lsvBedView.Visible = true;
            if (this.myucPCList != null)
                this.myucPCList.Visible = false;
            this.MenuSetControl( false );
            this.lsvBedView.View = View.Details;
            this.MenuSetControl( true );
        }

        private void mnuI_card_Click(object sender, EventArgs e)
        {

            this.lsvBedView.Visible = false;
            this.ucPatientCardList.Visible = true;
            this.MenuSetControl( false );
            this.MenuSetControl( true );

        }

        private void mnuI_Info_Click(object sender, EventArgs e)
        {
            ShowPatientInfo();
        }

        private void mnuI_Wap_Click(object sender, EventArgs e)
        {
            if (this.ucBedChange1 == null)
                this.ucBedChange1 = new ucBedChange();
            this.ucBedChange1.SetType = BedType.Wap;

            this.ControlAdd();
        }

        private void mnuI_Unwap_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();
            FS.HISFC.Models.Base.Bed objBed = new FS.HISFC.Models.Base.Bed();

            objBed = (FS.HISFC.Models.Base.Bed)this.lsvBedView.SelectedItems[0].Tag;

            patient.ID = objBed.InpatientNO;
            try
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(this.radtManager.Connection);
                //SQLCA.BeginTransaction();

                this.radtManager.SetTrans( FS.FrameWork.Management.PublicTrans.Trans );

                //�������
                if (this.radtManager.UnWrapPatientBed( patient, objBed.ID, "2" ) == 0)
                {
                    FS.FrameWork.Management.PublicTrans.Commit();
                    //���»��������λ����
                    objBed.InpatientNO = "N";
                    objBed.Status.ID = FS.HISFC.Models.Base.EnumBedStatus.U;
                    this.lsvBedView.Items[this.lsvBedView.SelectedIndices[0]] = this.GetListViewInfo( objBed );
                    //
                    this.Err = "����ɹ���";
                    MessageBox.Show( this.Err );
                    //ˢ�´�λ�б�
                    this.RefreshView();
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "����ʧ�ܣ�" + this.radtManager.Err;
                    MessageBox.Show( this.Err );
                }
            }
            catch
            {
            }
        }

        ucBedChange ucBedChange1 = new ucBedChange();
        private void ControlAdd()
        {
            if (this.lsvBedView.SelectedItems[0].Tag.GetType()
                == typeof( FS.HISFC.Models.RADT.PatientInfo ))
            {
                FS.HISFC.Models.RADT.PatientInfo patient = (FS.HISFC.Models.RADT.PatientInfo)this.lsvBedView.SelectedItems[0].Tag;


                this.ucBedChange1.PatientInfo = patient;

                FS.FrameWork.WinForms.Classes.Function.PopShowControl( this.ucBedChange1 );
                if (FS.FrameWork.WinForms.Classes.Function.PopForm.DialogResult == DialogResult.OK)
                {
                    //ˢ�´�λ�б�
                    this.RefreshView();
                }
            }
        }

        #endregion

        private void lsvBedView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (this.ListViewItemChanged != null)
                this.ListViewItemChanged( sender, e );
        }

    }
}
