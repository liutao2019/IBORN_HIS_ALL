using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Registration.Forms
{
    public partial class frmMiltScreen : Form, FS.HISFC.BizProcess.Interface.FeeInterface.IMultiScreen
    {
        public frmMiltScreen()
        {
            InitializeComponent();
        }

        #region ����

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
            this.lblIdenNo.Text = "";
            this.lblPhoneNo.Text = "";
            this.lblAdress.Text = "";

            this.lblPact.Text = "";
            this.lblRegLevl.Text = "";
            this.lblDept.Text = "";
            this.lblDoct.Text = "";
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
                this.lblIdenNo.Text = regObj.IDCard;
                this.lblPhoneNo.Text = regObj.PhoneHome;
                this.lblAdress.Text = regObj.AddressHome;

                this.lblPact.Text = regObj.Pact.Name;
                this.lblRegLevl.Text = regObj.DoctorInfo.Templet.RegLevel.Name;
                this.lblDept.Text = regObj.DoctorInfo.Templet.Dept.Name;
                this.lblDoct.Text = regObj.DoctorInfo.Templet.Doct.Name;
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
           // this.Close();
            this.Hide();
            return 0;
        }

        #endregion
    }
}
