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
            FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(al, typeof(ucPatientCard), this, new System.Drawing.Size(1000, 2000));   
            
        }
    }
}
