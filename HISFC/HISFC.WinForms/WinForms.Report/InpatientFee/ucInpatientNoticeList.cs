using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.WinForms.Report.InpatientFee
{
    /// <summary>
    /// [��������: ��Ժ֪ͨ�� ]<br></br>
    /// [�� �� ��: ��ѩ��]<br></br>
    /// [����ʱ��: 2009-09-14]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucInpatientNoticeList : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.IPrintInHosNotice
    {
        public ucInpatientNoticeList()
        {
            InitializeComponent();
            
        }

        #region ����
        /// <summary>
        /// ��ӡ
        /// </summary>
        FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();

        FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();
        #endregion

        #region ����

        /// <summary>
        /// ���ô�ӡʱ�ؼ��Ŀɼ���
        /// </summary>
        /// <param name="isSee"></param>
        private void SetVisible(bool isSee)
        {
            foreach (Control c in this.neuPanel1.Controls)
            {
                if (c is FS.FrameWork.WinForms.Controls.NeuLabel && (!c.Name.StartsWith("lblPri") || c.Tag=="1"))
                {
                    c.Visible = isSee;
                }
            }
        }
        /// <summary>
        /// ������Ժ֪ͨ����ӡֵ
        /// </summary>
        /// <param name="prePatientInfo"></param>
        /// <returns></returns>
        public int SetValue(FS.HISFC.Models.RADT.PatientInfo prePatientInfo)
        {
            this.neuLabel466.Text = this.manager.GetHospitalName() + "��Ժ֪ͨ��";
            this.SetVisible(false);
            this.lblPriName.Text = prePatientInfo.Name;
            if (!string.IsNullOrEmpty(prePatientInfo.ID))
            {
                this.ID.Text = prePatientInfo.ID.Substring(4);
            }
            this.lblPriSex.Text = prePatientInfo.Sex.Name;
            this.lblPriAge.Text = inpatientManager.GetAge(prePatientInfo.Birthday);
            this.lblPriDeptName.Text = prePatientInfo.PVisit.PatientLocation.Dept.Name;
            this.ID.Text = prePatientInfo.PID.CardNO;
            //this.lblPriClinicDiag.Text = prePatientInfo.ClinicDiagnose;
            ////this.lblPriDoc.Text = prePatientInfo.PatientInfo.DoctorReceiver.Name;
            //if (prePatientInfo.PVisit.Circs.ID == "1")
            //{
            //    this.lblPriCommon.Text = "һ��";
            //    this.lblPriEmergency.Text = "";
            //    this.lblPriDanger.Text = "";
            //}
            //else if (prePatientInfo.PVisit.Circs.ID == "2")
            //{
            //    this.lblPriCommon.Text = "";
            //    this.lblPriEmergency.Text = "��";
            //    this.lblPriDanger.Text = "";
            //}
            //else
            //{
            //    this.lblPriCommon.Text = "";
            //    this.lblPriEmergency.Text = "";
            //    this.lblPriDanger.Text = "Σ";
            //}
            //this.lblName.Text = prePatientInfo.Name;//����
            //this.lblBirth.Text = prePatientInfo.Birthday.ToShortDateString();//��������
            //this.lblProfession.Text = prePatientInfo.Profession.Name;//ְҵ
            //this.lblPriPreCost.Text = prePatientInfo.FT.PrepayCost.ToString();//Ԥ����
                       
            if (prePatientInfo.PVisit.InTime>DateTime.MinValue)
            {
                this.lblPriYear.Text = (prePatientInfo.PVisit.InTime.Year.ToString()).Substring(2, 2); //��Ժ���
                this.lblPriMonth.Text = prePatientInfo.PVisit.InTime.Month.ToString();//��Ժ�·�
                this.lblPriDay.Text = prePatientInfo.PVisit.InTime.Day.ToString();//��Ժ����
            }
            else
            {
                this.lblPriYear.Text = (DateTime.Now.Year.ToString()).Substring(2, 2); //��Ժ���
                this.lblPriMonth.Text = DateTime.Now.Month.ToString();//��Ժ�·�
                this.lblPriDay.Text = DateTime.Now.Day.ToString();//��Ժ����
            }
           
            this.lblDocName.Text = "";//FS.NFC.Management.Connection.Operator.Name; //ҽ��ǩ��
            //this.lblID.Text = prePatientInfo.IDCard; //���֤
            //this.lblTelephone.Text = prePatientInfo.PhoneHome; //��ͥ�绰
            //this.lblWorkStationAndAddr.Text = prePatientInfo.CompanyName; //������λ

            FS.HISFC.BizLogic.Manager.Constant constant = new FS.HISFC.BizLogic.Manager.Constant();
            FS.FrameWork.Models.NeuObject birthArea = constant.GetConstant(FS.HISFC.Models.Base.EnumConstant.AREA,prePatientInfo.AreaCode);
            //if (!string.IsNullOrEmpty(birthArea.Name))
            //{
            //    this.lblBirthArea.Text = birthArea.Name; //������
            //}
           
            //this.lblPeople.Text = prePatientInfo.Nationality.Name; //����

            //if (string.IsNullOrEmpty(prePatientInfo.Nationality.Name))//�еĵط����������ֻ��idû������
            //{
            //    FS.FrameWork.Models.NeuObject nationality = constant.GetConstant(FS.HISFC.Models.Base.EnumConstant.NATION, prePatientInfo.Nationality.ID);
               
            //    this.lblPeople.Text = nationality.Name;
            //}

            FS.FrameWork.Models.NeuObject pact = constant.GetConstant("PACTUNIT", prePatientInfo.Pact.ID);

            this.lblPactName.Text = pact.Name; //�������

            //this.lblNational.Text = prePatientInfo.Country.Name;// ����
            //this.lblHomeZip.Text = prePatientInfo.HomeZip; //��ͥ�ʱ�
            //this.lblTel.Text = prePatientInfo.Kin.RelationPhone; //��ϵ�绰
            //this.relaName.Text = prePatientInfo.Kin.ID; //��ϵ������
            
            //this.neuLabel8.Text = pact.Name;

            //this.lblRelations.Text = prePatientInfo.Kin.Relation.Name; //�뻼�߹�ϵ

            //this.lblProvice.Text = prePatientInfo.AddressHome;

            //if (!string.IsNullOrEmpty(prePatientInfo.MaritalStatus.Name))
            //{
            //    this.isMarried.Text = prePatientInfo.MaritalStatus.Name;
            //}
            //else
            //{
            //    this.isMarried.Text = "δ֪";
            //}
            return 1;
        }
        
        public int Print()
        {
            this.SetVisible(true);
            p.PrintPage(100, 50, this.neuPanel1);
            return 1;
        }

        public int PrintView()
        {
            this.SetVisible(true);
            p.PrintPreview(100, 50, this.neuPanel1);
            return 1;
        }
        #endregion

        #region �¼�
        private void ucInpatientNoticeList_Load(object sender, EventArgs e)
        {
            //���ؼ�������ҽԺͼƬ
            
        }
        #endregion
    }
}
