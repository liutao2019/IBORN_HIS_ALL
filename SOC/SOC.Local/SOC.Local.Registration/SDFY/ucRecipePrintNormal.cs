using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Registration.SDFY
{
    public partial class ucRecipePrintNormal : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.IRecipePrint
    {
        /// <summary>
        /// ������ӡ
        /// </summary>
        public ucRecipePrintNormal()
        {
            InitializeComponent();
        }

        #region ����

        private string myRecipeNO = "";

        /// <summary>
        /// �Һ���Ϣ
        /// </summary>
        private FS.HISFC.Models.Registration.Register myRegister = new FS.HISFC.Models.Registration.Register();

        /// <summary>
        /// �Һ�ҵ����
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();

        /// <summary>
        /// �ۺ�ҵ����
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        #endregion

        #region ����

        /// <summary>
        /// 
        /// </summary>
        public FS.HISFC.Models.Registration.Register PatientInfo
        {
            get
            {
                return this.myRegister;
            }
            set
            {
                this.myRegister = value;
            }
        }

        #endregion

        #region ˽�з���

        /// <summary>
        /// ���û��߻�����Ϣ
        /// </summary>
        private void SetPatient()
        {
            this.clear();

            if (this.myRegister == null)
            {
                return;
            }

            this.lblTitle.Text = this.interMgr.GetHospitalName() + " �� �� ��";

            if (this.myRegister.Pact.PayKind.ID == "01")
            {
                this.lblPact.Text = "��";
            }
            else if (this.myRegister.Pact.PayKind.ID == "02")
            {
                this.lblYB.Text = "��";
            }
            else if (this.myRegister.Pact.PayKind.ID == "03")
            {
                this.lblGF.Text = "��";
            }
            else
            {
                this.lblQT.Text = "��";
            }
            this.lblDept.Text = this.myRegister.DoctorInfo.Templet.Dept.Name;

            //˳�±��ػ����� �ҺŴ������ϴ�ӡ����ҽ�������Ϳ������  ygch  {10F6E92B-76C1-4c16-88D3-2E39E0E4F4FC}
            this.lblDoctName.Text += this.myRegister.DoctorInfo.Templet.Doct.Name;
            #region ����һ�������ɺͷ�������һ�����Ŷ���� ����м���� ˵������ʧ�� ���Ŷ���Ÿ�Ϊ��
            try
            {
                FS.HISFC.BizLogic.Nurse.Assign assginMgr = new FS.HISFC.BizLogic.Nurse.Assign();
                FS.HISFC.Models.Nurse.Assign assion = new FS.HISFC.Models.Nurse.Assign();
                assion = assginMgr.QueryByClinicID(this.myRegister.ID);

                FS.HISFC.BizLogic.Nurse.Queue queMgr = new FS.HISFC.BizLogic.Nurse.Queue();
                FS.HISFC.Models.Nurse.Queue queobj = new FS.HISFC.Models.Nurse.Queue();
                ArrayList queobjList = new ArrayList();
                queobjList = queMgr.QueryByQueueID(assion.Queue.ID);
                if (queobjList.Count != 0)
                {
                    queobj = queobjList[0] as FS.HISFC.Models.Nurse.Queue;
                }
                this.lblDoctSeeNO.Text += queobj.SRoom.Name + "-" + assion.SeeNO.ToString();
            }
            catch (Exception e)
            {
                this.lblDoctSeeNO.Text += "";
            }
            #endregion

            this.lblName.Text = this.myRegister.Name;
            this.lblCardNO.Text = this.myRegister.PID.CardNO;
            this.lblSex.Text = this.myRegister.Sex.Name;
            if (this.lblSex.Text == "��")
            {
                lblSex.Text = "��";
            }
            else
            {
                lblWoman.Text = "��";
            }
            this.lblAge.Text = this.regMgr.GetAge(this.myRegister.Birthday);

            #region ��ȷ���� 1��������ʾ�� ��  14��������ʾ������ 14��������ʾ��  ygch {45A98CF7-0239-4785-80FD-3DB8913E8F90}
            string agee = "";
            agee = this.regMgr.GetAge(this.myRegister.Birthday, true);

            agee = agee.Replace("_", "");
            int iyear = FS.FrameWork.Function.NConvert.ToInt32(agee.Substring(0, agee.IndexOf("��")));
            int iMonth = FS.FrameWork.Function.NConvert.ToInt32(agee.Substring(agee.IndexOf("��") + 1, agee.IndexOf("��") - agee.IndexOf("��") - 1));
            int iDay = FS.FrameWork.Function.NConvert.ToInt32(agee.Substring(agee.IndexOf("��") + 1, agee.IndexOf("��") - agee.IndexOf("��") - 1));
            if (iyear == 0)
            {
                agee = iMonth.ToString() + "��" + iDay.ToString() + "��";
            }
            else if (iyear >= 1 && iyear < 14)
            {
                agee = iyear.ToString() + "��" + iMonth.ToString() + "��";
            }
            else
            {
                agee = iyear.ToString() + "��";
            }

            this.lblAgeE.Text = "����:  " + agee;
            #endregion

            this.lblSICard.Text = this.myRegister.SIMainInfo.RegNo;
            this.lblAddress.Text = this.myRegister.AddressHome + "   " + this.myRegister.PhoneHome;
            this.neuPictureBox1.Image = FS.FrameWork.WinForms.Classes.CodePrint.GetCode39(this.myRegister.ID);
            this.lblRegisterID.Text = this.myRegister.ID;

            DateTime sysDate = this.regMgr.GetDateTimeFromSysDateTime();
            this.lblSeeDate.Text = sysDate.ToString("yyyy     MM    dd");

            this.lblSeeDoctor.Text = this.regMgr.Operator.Name;
        }

        #endregion

        #region IRecipePrint ��Ա

        /// <summary>
        /// ��ӡ������
        /// </summary>
        private string printer = "";

        public string Printer
        {
            get
            {
                return printer;
            }
            set
            {
                printer = value;
            }
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        public int PrintRecipe()
        {
            this.lblTitle.Text = this.interMgr.GetHospitalName() + "�� �� ��";

            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.IsDataAutoExtend = true;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            //ygch  ����ҺŴ�����ֱ�Ӵ�ӡ
            p.PrintPage(45,0,this);

            return 1;
        }

        /// <summary>
        /// ʵ�ִ�����ӡ����PrintRecipeView()
        /// </summary>
        /// <returns></returns>
        public int PrintRecipeView(ArrayList al)
        {
            return 1;
        }

        public void clear()
        {
            this.lblPact.Text = "";
            this.lblGF.Text = "";
            this.lblQT.Text = "";
            this.lblYB.Text = "";
            this.lblSex.Text = "";
            this.lblWoman.Text = "";
        }

        /// <summary>
        /// ���û�����Ϣ
        /// </summary>
        /// <param name="register"></param>
        public int SetPatientInfo(FS.HISFC.Models.Registration.Register register)
        {
            this.myRegister = register;
            this.SetPatient();
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        public string RecipeNO
        {
            get
            {
                return this.myRecipeNO;
            }
            set
            {
                this.myRecipeNO = value;
            }

        }

        #endregion
    }
}

