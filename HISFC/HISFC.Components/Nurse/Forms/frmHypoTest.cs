using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Nurse.Forms
{
    public partial class frmHypoTest : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmHypoTest()
        {
            InitializeComponent();
        }

        #region MyRegion
        private bool isEditMode = false;
        
        #endregion


        private ArrayList alOrderList = null;

        /// <summary>
        /// ����Ƥ�Ե�ҽ����Ϣ
        /// </summary>
        public ArrayList AlOrderList
        {
            get { return alOrderList; }
            set { alOrderList = value; }
        }
        
        public int GetHypotestValue()
        {
            if (!this.rb2.Checked && !this.rb4.Checked)
            {
                MessageBox.Show("��ѡ��Ƥ�Խ��");
                return  -1;
            }
            //if (this.rb1.Checked)
            //{
            //    return 1;
            //}
            //else 
            if (this.rb2.Checked)
            {
                return 4;
            }
            //else if (this.rb3.Checked)//{BCF43AF9-C17E-43e3-8E21-E273CE96975D}
            //{
            //    return 2;
            //}
            else if (this.rb4.Checked)
            {
                return 3;
            }
            else
            {
                return 4;
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            int hyTestValue = this.GetHypotestValue();
            if (hyTestValue == -1)
            {
                return;
            }

            ArrayList alOrderListClone = new ArrayList();

            foreach (FS.HISFC.Models.Order.OutPatient.Order var in this.alOrderList)
            {
                alOrderListClone.Add(var.Clone());
            }

            foreach (FS.HISFC.Models.Order.OutPatient.Order var in alOrderListClone)
            {
                if (var.HypoTest == FS.HISFC.Models.Order.EnumHypoTest.FreeHypoTest) //����ҪƤ��
                {
                    continue;
                }

                int returnVlue = orderIntegrate.UpdateOrderHyTest(hyTestValue.ToString(), var.ID.ToString());

               
                if (returnVlue < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ҽ����Ƥ����Ϣ����!"  + orderIntegrate.Err);
                    return; 
                }

                var.HypoTest = (FS.HISFC.Models.Order.EnumHypoTest)hyTestValue;

            }


            foreach (FS.HISFC.Models.Order.OutPatient.Order var in this.alOrderList)
            {

                foreach (FS.HISFC.Models.Order.OutPatient.Order varClone in alOrderListClone)
                {
                    if (var.ID == varClone.ID)
                    {
                        var.HypoTest = varClone.HypoTest;
                    }
                }
            }


            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("�޸ĳɹ�");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}