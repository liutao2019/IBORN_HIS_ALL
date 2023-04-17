using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Order.OutPatient.Forms
{
    public partial class frmInputInjectNum : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmInputInjectNum()
        {
            InitializeComponent();
            //this.txtInJectNum.ReadOnly = true;
        }

        #region ����
        private bool isHaveSetted = false;
        private bool isSpring = false;
        /// <summary>
        /// ������Ҫ���ĵ�ҽ��
        /// </summary>
        protected FS.HISFC.Models.Order.OutPatient.Order order = null;
        #endregion

        #region ����
        /// <summary>
        /// ��û�����Ժע����
        /// </summary>
        public int InjectNum
        {
            get
            {
                return (int)this.txtInJectNum.Value;
            }
            set
            {
                if ((decimal)value >= 100)
                {
                    MessageBox.Show("Ժע�����������������룡");
                }
                else
                {
                    this.txtInJectNum.Value = (decimal)value;
                }
            }
        }


        /// <summary>
        /// ��ǰҽ��
        /// </summary>
        public FS.HISFC.Models.Order.OutPatient.Order Order
        {
            get
            {
                return this.order;
            }
            set
            {
                order = value;
            }
        }
        #endregion

        /// <summary>
        /// load
        /// </summary>xz
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmInputInjectNum_Load(object sender, System.EventArgs e)
        {
            if (this.order.ExtendFlag1 == null)
            {
                this.txtTimes.Text = "1";//Ĭ��
            }
            else
            {
                this.txtTimes.TextChanged -= new EventHandler(txtTimes_TextChanged);
                this.txtTimes.Text = "1";
                this.txtTimes.TextChanged += new EventHandler(txtTimes_TextChanged);

                if (this.order.ExtendFlag1.Length >= 1)
                {
                    decimal times = 1;

                    try
                    {
                        times = System.Convert.ToDecimal(this.order.ExtendFlag1.Substring(0, 1));
                        this.isHaveSetted = true;
                    }
                    catch
                    {
                        this.isHaveSetted = false;
                    }

                    this.txtTimes.Text = times.ToString();
                }
            }
            this.txtInJectNum.Focus();
            this.txtInJectNum.Select(0, this.txtInJectNum.Text.ToString().Length);
            this.lblDoseOnce.Text = "ÿ�� " + order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + order.DoseUnit;
            //{27DBE032-6896-4b8f-9CBC-EDC47F499B50} ����ҽ��ʱ��ʾԺע����
            this.SetInjectDays();
        }

        /// <summary>
        /// {27DBE032-6896-4b8f-9CBC-EDC47F499B50} ����ҽ��ʱ��ʾԺע����
        /// </summary>
        private void SetInjectDays()
        {
            //��������
            decimal qty = this.order.Item.Qty;
            //Ժע����
            decimal injectTimes = this.txtInJectNum.Value;
            //��������
            decimal baseDose = ((FS.HISFC.Models.Pharmacy.Item)this.order.Item).BaseDose;
            //ÿ�μ���
            decimal doseOnce = this.order.DoseOnce;
            //ÿ��Ƶ��
            int frequencyDayCount = (this.order.Frequency.Time.Split('-')).Length;
            //����ó�Ժע����
            int injectDays = (int)Math.Ceiling(injectTimes / frequencyDayCount);
            //���Ժע����
            if (doseOnce != 0)
            {
                int maxDays = (int)((qty * baseDose) / (doseOnce * frequencyDayCount));
            }

            this.lblInjectDays.Text = "Ժע������" + injectDays + "��";
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Save()
        {
            try
            {
                if (this.InjectNum < 0)
                {
                    MessageBox.Show("Ժע���������㣡");
                    this.txtInJectNum.Select(0, this.txtInJectNum.Text.Length);
                    this.txtInJectNum.Focus();
                    return;
                }
                if (this.InjectNum > 98)
                {
                    MessageBox.Show("Ժע��������");
                    this.txtInJectNum.Select(0, this.txtInJectNum.Text.Length);
                    this.txtInJectNum.Focus();
                    return;
                }
                order.InjectCount = this.InjectNum;
                order.NurseStation.User02 = "C";//�޸Ĺ�
                if (this.isHaveSetted)
                {
                    order.ExtendFlag1 = order.ExtendFlag1.Remove(0, 1);
                    order.ExtendFlag1 = this.txtTimes.Text + order.ExtendFlag1;
                }
                else
                {
                    if (this.txtTimes.Text.Trim() == "")
                    {
                        MessageBox.Show("ע��ƿ������Ϊ��!");
                        this.txtTimes.Focus();
                        return;
                    }
                    int price = 0;
                    try
                    {
                        price = FS.FrameWork.Function.NConvert.ToInt32(this.txtTimes.Text.Trim());
                    }
                    catch
                    {
                        MessageBox.Show("����ע��ƿ���ĸ�ʽ����ȷ�����������룡");
                        this.txtTimes.Focus();
                        return;
                    }
                    if (price <= 0)
                    {
                        MessageBox.Show("�����ע��ƿ������С�ڻ����0��");
                        this.txtTimes.Focus();
                        return;
                    }
                    if (price > 9)
                    {
                        MessageBox.Show("�����ע��ƿ�����ܴ���9��");
                        this.txtTimes.Focus();
                        return;
                    }
                    this.order.ExtendFlag1 = this.txtTimes.Text.Trim() + this.order.ExtendFlag1;
                }
            }
            catch
            {
                MessageBox.Show("���Ժע��������");
                this.Close();
            }
            this.Close();
        }

        /// <summary>
        /// ȷ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, System.EventArgs e)
        {
            this.Save();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                this.Save();   
            }
            else if (keyData == Keys.Escape)
            {
                this.Close();
            }
            //return true;
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// �¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuNumericUpDown1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.isSpring == false)
                {
                    this.txtInJectNum.Focus();
                    this.isSpring = true;
                }
                else
                {
                    this.btnOK_Click(null, null);
                }
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTimes_TextChanged(object sender, System.EventArgs e)
        {
            decimal times = 1;

            if (this.txtTimes.Text.Trim() == "")
            {
                return;
            }

            try
            {
                times = System.Convert.ToDecimal(this.txtTimes.Text.Trim());
            }
            catch
            {
                MessageBox.Show("������ĵ����ָ�ʽ����ȷ�����������룡");
                this.txtTimes.Focus();
                return;
            }
            if (times > 9)
            {
                MessageBox.Show("������ĵ����ֹ������������룡");
                this.txtTimes.Focus();
                return;
            }
        }

        /// <summary>
        /// {27DBE032-6896-4b8f-9CBC-EDC47F499B50} ����ҽ��ʱ��ʾԺע����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuNumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.SetInjectDays();
        }

        private void txtInJectNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnOK.Focus();
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

