using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;

namespace FS.SOC.Local.OutpatientFee.QiaoTou
{
    public partial class frmRegiserMultiScreen : Form, FS.HISFC.BizProcess.Interface.FeeInterface.IMultiScreen
    {
        public frmRegiserMultiScreen()
        {
            InitializeComponent();
        }

        #region ����
        private FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
        #endregion

        #region ����


        #endregion

        #region ����

        #endregion

        /// <summary>
        /// ���
        /// </summary>
        public void Clear() 
        {
            this.lblName.Text = "";
            this.lblSex.Text = "";
            this.lblBirthday.Text = "";
            //this.lblIdenNo.Text = "";
            //this.lblPhoneNo.Text = "";
            //this.lblAdress.Text = "";

            //this.lblPact.Text = "";
            //this.lblRegLevl.Text = "";
            //this.lblDept.Text = "";
            //this.lblDoct.Text = "";
            this.lblFee.Text = "0Ԫ";
        }

        #region IMultiScreen ��Ա

        public System.Collections.Generic.List<object> ListInfo
        {
            get
            {
                return null;
            }
            set
            {
                this.Clear();
                if (value != null)
                {
                    ////////// this.SetInfomation(
                    //////////value[0] as FS.HISFC.Models.Registration.Register
                    //////////     , value[1] as FS.HISFC.Models.Base.FT
                    //////////         , value[2] as ArrayList
                    ////////// , value[3] as ArrayList
                    ////////// );
                    //this.SetShowInfomation(value[0] as FS.HISFC.Models.RADT.PatientInfo, value[1] as string, value[2] as string, value[3] as string);
                    this.SetShowInformation(value[0] as FS.HISFC.Models.Registration.Register, value[1] as string, value[2] as string, value[3] as string, value[4] as string, value[5] as FS.HISFC.Models.Base.Employee);
                } 
            }
        }

        private void SetShowInformation(FS.HISFC.Models.Registration.Register register, string p, string p_3, string p_4, string p_5, FS.HISFC.Models.Base.Employee feePerson)
        {
            if (feePerson != null)
            {
                this.neuPanel4.Visible = false;
                this.neuLabel10.Visible = true;
                this.neuLabel10.Text = feePerson.ID +"Ϊ������" ;
                this.neuLabel11.Visible = true;
            }
            else if (register !=null)
            {
                this.neuPanel4.Visible = true;
                this.neuPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
                this.neuLabel10.Visible = false;
                this.neuLabel11.Visible = false;

                this.lblName.Text = register.Name.ToString();
                this.lblSex.Text = register.Sex.ToString();
                this.lblBirthday.Text = register.Birthday.ToShortDateString()==null?"":register .Birthday .ToShortDateString();
                //this.lblIdenNo.Text = register.IDCard==null?"":register .IDCard .ToString ();
                //this.lblAdress.Text = register.AddressHome==null?"":register .AddressHome .ToString ();
                //this.lblPhoneNo.Text = register.PhoneHome== null ? "" : register.PhoneHome.ToString();


                FS.HISFC.Models.Base.PactInfo pact = manager.GetPactUnitInfoByPactCode(register.Pact.ID);
                //this.lblPact.Text = pact.Name;
                //this.lblRegLevl.Text = p.ToString();//"�Һż���";
                //this.lblDept.Text = p_3.ToString(); //"�Һſ���";
                //this.lblDoct.Text = p_4.ToString(); //"�Һ�ҽ��";
                this.lblFee.Text = p_5.ToString()+"Ԫ"; //"����" + p.ToString();
                //this.lblFee.Text = register.OwnCost.ToString ();
            }
             
        }
            

        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="otherInfomations">��������˳������:0 �ܷ��� 1 �Һŷ� 2 ��� 3�������� 4����</param>
        /// <returns></returns>
        private void ShowInfo(FS.HISFC.Models.Registration.Register regObj, params string[] otherInfomations)
        {
            if (regObj == null)
            {
                return;
            }
            try
            {
                this.lblName.Text = regObj.Name;
                this.lblSex.Text = regObj.Sex.Name;
                this.lblBirthday.Text = regObj.Birthday.ToString("yyyy-MM-dd");
                //this.lblIdenNo.Text = regObj.IDCard;
                //this.lblPhoneNo.Text = regObj.PhoneHome;
                //this.lblAdress.Text = regObj.AddressHome;

                //this.lblPact.Text = regObj.Pact.Name;
                //this.lblRegLevl.Text = regObj.DoctorInfo.Templet.RegLevel.Name;
                //this.lblDept.Text = regObj.DoctorInfo.Templet.Dept.Name;
                //this.lblDoct.Text = regObj.DoctorInfo.Templet.Doct.Name;
                this.lblFee.Text = otherInfomations[0] + "Ԫ";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public int ShowScreen()
        {
            this.Clear();

            if (Screen.AllScreens.Length > 1)
            {
                this.Show();

                if (Screen.AllScreens[0].Primary)
                {
                    this.DesktopBounds = Screen.AllScreens[1].Bounds;
                }
                else
                {
                    this.DesktopBounds = Screen.AllScreens[0].Bounds;
                }
            }
            return 0;
        }

        public int CloseScreen()
        {
            //this.Close();
            this.ScreenInvisible();
            return 0;
        }

        public void ScreenInvisible()
        {
            this.Visible = false;
        }
        #endregion

        void frmRegiserMultiScreen_Load(object sender, System.EventArgs e)
        {
            try
            {
                this.neuPanel3.BackgroundImage = System.Drawing.Image.FromFile(Application.StartupPath + "\\Setting\\backgroundimage\\����Һű���.jpg");
            }
            catch
            {
            }
        }
    }
}
