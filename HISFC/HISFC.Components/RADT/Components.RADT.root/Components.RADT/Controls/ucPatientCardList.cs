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
    public partial class ucPatientCardList : UserControl
    {
        /// <summary>
        /// [��������: ��λ��Ƭ]<br></br>
        /// [�� �� ��: wolf]<br></br>
        /// [����ʱ��: 2006-11-30]<br></br>
        /// <�޸ļ�¼
        ///		�޸���=''
        ///		�޸�ʱ��='yyyy-mm-dd'
        ///		�޸�Ŀ��=''
        ///		�޸�����=''
        ///  />
        /// </summary>
        public ucPatientCardList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ���û���
        /// </summary>
        /// <param name="al"></param>
        public virtual void SetPatients(ArrayList al)
        {
            this.Controls.Clear();
            Neusoft.FrameWork.WinForms.Classes.Function.AddControlToPanel(al, typeof(ucPatientCard), this, new System.Drawing.Size(791, 1150));   
            
        }

        /// <summary>
        /// {46063507-0C5A-405d-BD9D-4762ADE8DE02}
        /// </summary>
        public void PrintCard()
        {
            
            Neusoft.FrameWork.WinForms.Controls.NeuPanel np = new Neusoft.FrameWork.WinForms.Controls.NeuPanel();

            ArrayList al = new ArrayList();
            foreach ( Control c in this.Controls)
            {
                if (c.GetType() == typeof(ucPatientCard))
                {
                    ucPatientCard uc = c as ucPatientCard;
                    if (uc.BackColor == Color.Blue)
                    {
                        al.Add(uc.ControlValue);
                    }
                }
            }

            Neusoft.FrameWork.WinForms.Classes.Function.AddControlToPanel(al, typeof(ucPatientCard), np, new System.Drawing.Size(791, 1150));   

           
           

           

            Neusoft.FrameWork.WinForms.Classes.Print p = new Neusoft.FrameWork.WinForms.Classes.Print();
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

    }
}
