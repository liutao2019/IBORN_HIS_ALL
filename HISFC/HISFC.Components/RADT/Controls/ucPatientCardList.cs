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
    public partial class ucPatientCardList : UserControl
    {
        /// <summary>
        /// [功能描述: 床位卡片]<br></br>
        /// [创 建 者: wolf]<br></br>
        /// [创建时间: 2006-11-30]<br></br>
        /// <修改记录
        ///		修改人=''
        ///		修改时间='yyyy-mm-dd'
        ///		修改目的=''
        ///		修改描述=''
        ///  />
        /// </summary>
        public ucPatientCardList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置患者
        /// </summary>
        /// <param name="al"></param>
        public virtual void SetPatients(ArrayList al)
        {
            this.Controls.Clear();// {43D3B083-0C3D-459d-9E2E-007368A21857}
            FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(al, typeof(ucPatientCardNew), this, new System.Drawing.Size(1250, 1150));
            // {297FED84-CB6C-41f0-86DD-51BF035C2D36}
        }

        /// <summary>
        /// {46063507-0C5A-405d-BD9D-4762ADE8DE02}  
        /// </summary>
        public void PrintCard()
        {
            
            FS.FrameWork.WinForms.Controls.NeuPanel np = new FS.FrameWork.WinForms.Controls.NeuPanel();

            ArrayList al = new ArrayList();
            foreach ( Control c in this.Controls)
            {
                if (c.GetType() == typeof(ucPatientCardNew))// {297FED84-CB6C-41f0-86DD-51BF035C2D36}
                {
                    ucPatientCardNew uc = c as ucPatientCardNew;// {297FED84-CB6C-41f0-86DD-51BF035C2D36}
                    if (uc.BackColor == Color.Blue)
                    {
                        al.Add(uc.ControlValue);
                    }
                }
            }

            FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(al, typeof(ucPatientCardNew), np, new System.Drawing.Size(791, 1150));   

           
           

           

            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            //p.SetPageSize(System.Drawing.Printing.PaperKind.A4);
            //Panel ppp = new Panel();
            //ppp.Size = new Size(791, 1200);
            //ppp.BackColor = Color.Blue;

            if (al.Count == 0)
            {

                p.PrintPreview(0, 0, this);
            }
            else
            {
                p.PrintPreview(0, 0, np);
            }
            np.Dispose();
        }
        /// <summary>
        /// // {407F4A63-CC38-4842-BFDA-995E1C3FC664}
        /// </summary>
        public int ChoosePatientInfo(ref FS.HISFC.Models.RADT.PatientInfo patient)
        {
            ArrayList al = new ArrayList();
            foreach (Control c in this.Controls)
            {
                if (c.GetType() == typeof(ucPatientCardNew))// {297FED84-CB6C-41f0-86DD-51BF035C2D36}
                {
                    ucPatientCardNew uc = c as ucPatientCardNew;// {297FED84-CB6C-41f0-86DD-51BF035C2D36}
                    if (uc.BackColor == Color.Blue)
                    {
                        al.Add(uc.ControlValue);
                    }
                }
            }
            if (al.Count > 1)
            {
                return -2;
            }
            else if(al.Count <= 0)
            {
                return -1;
            }
            else if (al.Count == 1)
            {
                patient = al[0] as FS.HISFC.Models.RADT.PatientInfo;
            }

            return 1;
        }
    }
}
