using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Manager.Forms
{
    /// <summary>
    /// ��λά��
    /// </summary>
    public partial class frmBatchAddBed : Form
    {
        //����վ���  ������ʱ��
        protected string bedRoomNO = string.Empty;
        public string BedRoomNO
        {
            set
            {
                bedRoomNO = value;
            }
        }

        //����վ���  ������ʱ��
        protected string nurseStation = string.Empty;
        public string NurseStation
        {
            set
            {
                nurseStation = value;
            }
        }

        public frmBatchAddBed(bool isUpdate)
        {
            InitializeComponent();
            if (isUpdate)
            {
                txtBedNo.Enabled = false;
                this.cmbNurse.Enabled = false;
            }
            this.isUpdate = isUpdate;
            this.Init();
        }

        protected void Init()
        {
            FS.HISFC.BizLogic.Manager.Department Dept = new FS.HISFC.BizLogic.Manager.Department();
            FS.HISFC.BizLogic.Manager.Constant content = new FS.HISFC.BizLogic.Manager.Constant();
            this.cmbNurse.AddItems(Dept.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.N));//��ʿվ�б�
            this.cmdBedGrade.AddItems(content.GetList(FS.HISFC.Models.Base.EnumConstant.BEDGRADE));//��λ�ȼ�
            this.cmbBedWeave.AddItems(FS.HISFC.Models.Base.BedRankEnumService.List());//��λ����
            this.cmbBedStatus.AddItems(FS.HISFC.Models.Base.BedStatusEnumService.List());//��λ״̬
            this.tbCount.Text = "1";
        }
        protected bool isUpdate = false;
        public string Err = "";
        FS.HISFC.BizLogic.Manager.Bed bed = new FS.HISFC.BizLogic.Manager.Bed();
        protected int CheckValid()
        {
            if (this.cmbNurse.SelectedItem == null)
            {
                this.Err = "����վ�Ų�����";
                return -1;
            }
            if (this.txtBedNo.Text == "")
            {
                this.Err = "����Ϊ�գ�����д��";
                return -1;
            }
            if (txtBedNo.Enabled)
            {
                if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtBedNo.Text, 6))
                {
                    this.Err = "���Ź�������������д��";
                    return -1;
                }
            }
            
            if (txtBedNo.Text != "")
            {
                for (int i = 0; i < int.Parse(tbCount.Text.Trim()) - 1; i++)
                {
                    int bedNo;
                    bedNo = int.Parse(txtBedNo.Text)  + i;
                    int temp = bed.IsExistBedNo(this.cmbNurse.SelectedItem.ID + bedNo);
                    if (temp == 0)
                    {
                        //û��
                    }
                    else if (temp == 1)
                    {
                        this.Err = "�Ѿ����ڴ�λ�� " + bedNo + "���޸ģ�";
                        txtBedNo.Focus();
                        return -1;
                    }
                }
            }

            if (this.txtWardNo.Text == "")
            {
                this.Err = "������Ϊ�գ�����д��";
                return -1;
            }
            if (this.cmdBedGrade.Text == "")
            {
                this.Err = "��λ�ȼ�Ϊ�գ���ѡ��";
                return -1;
            }
            if (this.cmbBedWeave.Text == "")
            {
                this.Err = "��λ����Ϊ�գ���ѡ��";
                return -1;
            }
            if (this.cmbBedStatus.Text == "")
            {
                this.Err = "��λ״̬Ϊ�գ���ѡ��";
                return -1;
            }
            
            if(!FS.FrameWork.Public.String.ValidMaxLengh(this.txtPhone.Text,14))
            {
                this.Err = "��λ�绰�Ϊ14λ,����������";
                return -1;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtWardNo.Text, 10))
            {
                this.Err = "���ҺŹ���,����������";
                return -1;
            }
            return 0;
        }

        public void SetBedInfo(FS.HISFC.Models.Base.Bed bedInfo)
        {
            if (bedInfo != null)
            {

                this.cmbNurse.Tag = bedInfo.NurseStation.ID;//��ʿվ���
                this.txtWardNo.Text = bedInfo.SickRoom.ID;//������
                this.txtBedNo.Text = bedInfo.ID;//������
                this.cmdBedGrade.Tag = bedInfo.BedGrade.ID.ToString();//�����ȼ�
                this.cmdBedGrade.Text = bedInfo.BedGrade.Name;
                this.cmbBedStatus.Tag = bedInfo.Status.ID.ToString();//����״̬
                this.cmbBedStatus.Text = bedInfo.Status.Name;
                this.cmbBedWeave.Tag = bedInfo.BedRankEnumService.ID.ToString();//��������
                this.cmbBedWeave.Text = bedInfo.BedRankEnumService.Name;
                this.txtPhone.Text = bedInfo.Phone;//�绰
                this.txtSort.Text = bedInfo.SortID.ToString();//˳���
                this.txtOwn.Text = bedInfo.OwnerPc.Trim();//����
                if (isUpdate)
                {
                    if (bedInfo.Status.ID.ToString() == "O" ||
                        bedInfo.Status.ID.ToString() == "R" ||
                        bedInfo.Status.ID.ToString() == "W") //ռ�ô�λ�����޸�״̬
                    {
                        this.cmbBedStatus.Enabled = false;
                    }
                }
            }
        }
        FS.HISFC.Models.Base.Bed BedInfo = null;
        public void GetBedInfo(string bedNo)
        {
            if (BedInfo == null)
            {
                BedInfo = new FS.HISFC.Models.Base.Bed();
            }
            if (BedInfo.InpatientNO == "" || BedInfo.InpatientNO == null)
            {
                BedInfo.InpatientNO = "N";
            }
            BedInfo.NurseStation.ID = cmbNurse.Tag.ToString();//��ʿվ���
            
            BedInfo.SickRoom.ID = this.txtWardNo.Text.Trim();//������

            BedInfo.ID = bedNo.Trim();//������
            BedInfo.BedGrade.ID = this.cmdBedGrade.Tag.ToString();//�����ȼ�
            BedInfo.Status.ID = this.cmbBedStatus.Tag.ToString();//����״̬
            BedInfo.BedRankEnumService.ID = this.cmbBedWeave.Tag.ToString();//��������
            BedInfo.Phone = txtPhone.Text.Trim();//�绰
            BedInfo.SortID = int.Parse(this.txtSort.Text);//˳���
            BedInfo.OwnerPc = this.txtOwn.Text.Trim();//����
            //{AF7C0F4A-2521-460a-A3F9-A30D7A4EB942}  
            BedInfo.IsValid = true;


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckValid() != -1)
                {
                    
                    int iParm=0;

                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                    bed.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    
                    for (int i = 0; i <= int.Parse(tbCount.Text.Trim()) - 1; i++)
                    {
                        int bedNo;
                        string newbedNo = "";
                        bedNo = int.Parse(txtBedNo.Text) + i;
                        if (txtBedNo.Text.Length > bedNo.ToString().Length)
                        {
                            newbedNo = bedNo.ToString().PadLeft(txtBedNo.Text.Length , '0');
                        }
                        else
                        {
                            newbedNo = bedNo.ToString();
                        }
                        this.GetBedInfo(newbedNo);
                        //{6A55FE10-D8BA-40da-AFFE-B3020AC26716}
                        BedInfo.SortID = int.Parse(txtSort.Text)+i;

                        if (isUpdate)
                        {
                            iParm = bed.UpdateBedInfo(BedInfo);
                            //Ƕ�������ϵͳ������ҵ��ģ�����Ϣ�����ŽӴ���
                            string errInfo = "";
                            ArrayList alInfo = new ArrayList();
                            alInfo.Add(BedInfo);
                            int param = Function.SendBizMessage(alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Modify, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Bed, ref errInfo);

                            if (param == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack(); ;
                                Function.ShowMessage("��λ���ʧ�ܣ�����ϵͳ����Ա���������Ϣ��" + errInfo, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        else
                        {
                            iParm = bed.CreatBedInfo(BedInfo);
                            //Ƕ�������ϵͳ������ҵ��ģ�����Ϣ�����ŽӴ���
                            string errInfo = "";
                            ArrayList alInfo = new ArrayList();
                            alInfo.Add(BedInfo);
                            int param = Function.SendBizMessage(alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Bed, ref errInfo);

                            if (param == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack(); ;
                                Function.ShowMessage("��λ���ʧ�ܣ�����ϵͳ����Ա���������Ϣ��" + errInfo, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    //{619F3CBF-7954-4d5e-B815-C66987E15C60}  ��λ������У��
                    if (Components.Manager.Classes.Function.BedVerify() == false)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }

                    if (iParm <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();;
                        MessageBox.Show(this.bed.Err);
                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.Commit();;
                        MessageBox.Show("����ɹ���");
                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show(Err);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                System.Windows.Forms.SendKeys.Send("{tab}");
            }
        }

        private void frmBedManager_Load(object sender, EventArgs e)
        {
            if (!isUpdate)
            {
                this.cmbNurse.Tag = this.nurseStation;
                this.txtWardNo.Text = this.bedRoomNO;
            }
        }
    }
}