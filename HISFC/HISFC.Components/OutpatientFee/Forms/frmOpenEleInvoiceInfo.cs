using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.OutpatientFee.Forms//Neusoft.SOC.HISFC.InpatientFee.Components.Balance
{

    public delegate void delegateHanTianinrequestBillingNew(string req, string invoice);

    public partial class frmOpenEleInvoiceInfo : Form
    {
        public delegateHanTianinrequestBillingNew hanTianinrequestBillingNew;

        public string req = "";
        // decimal totalPrice = 0.0m;

        public frmOpenEleInvoiceInfo()
        {
            InitializeComponent();


            foreach (Control control in panel2.Controls)
            {
                if (control is TextBox)
                {
                    control.MouseLeave += txtBox_MouseLeave;
                }
            }

        }

        private void txtBox_MouseLeave(object sender, EventArgs e)
        {
            try
            {


                decimal totalPrice = 0.0m;

                foreach (Control control in panel2.Controls)
                {
                    if (control is TextBox)
                    {
                        totalPrice += Convert.ToDecimal(control.Text.ToString());
                    }
                }
                this.neuTextBox1.Text = totalPrice.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Hashtable SetOpenEleInfo(Hashtable hashTableIn)
        {
            decimal price = Convert.ToDecimal(hashTableIn["治疗费"] == null ? "0" : hashTableIn["治疗费"].ToString());
            decimal price1 = Convert.ToDecimal(hashTableIn["诊察费"] == null ? "0" : hashTableIn["诊察费"].ToString());
            decimal price2 = price + price1;

            this.tb1.Text = hashTableIn["姓名"] == null ? "" : hashTableIn["姓名"].ToString();
            this.tb2.Text = "";// hashTableIn["手机号"] == null ? "" : hashTableIn["手机号"].ToString();
            this.tb3.Text = hashTableIn["挂号费"] == null ? "0" : hashTableIn["挂号费"].ToString();
            this.tb4.Text = hashTableIn["诊查费"] == null ? "0" : hashTableIn["诊查费"].ToString();
            this.tb5.Text = hashTableIn["检查费"] == null ? "0" : hashTableIn["检查费"].ToString();
            this.tb6.Text = hashTableIn["检验费"] == null ? "0" : hashTableIn["检验费"].ToString();
            this.tb7.Text = hashTableIn["治疗费"] == null ? "0" : hashTableIn["治疗费"].ToString();
            this.tb8.Text = hashTableIn["手术费"] == null ? "0" : hashTableIn["手术费"].ToString();
            this.tb9.Text = hashTableIn["护理费"] == null ? "0" : hashTableIn["护理费"].ToString();
            this.tb10.Text = hashTableIn["材料费"] == null ? "0" : hashTableIn["材料费"].ToString();
            this.tb11.Text = hashTableIn["西药费"] == null ? "0" : hashTableIn["西药费"].ToString();
            this.tb12.Text = hashTableIn["中成药费"] == null ? "0" : hashTableIn["中成药费"].ToString();
            this.tb13.Text = hashTableIn["医疗费"] == null ? "0" : hashTableIn["医疗费"].ToString();
            this.tb14.Text = hashTableIn["会务费"] == null ? "0" : hashTableIn["会务费"].ToString();
            this.tb15.Text = hashTableIn["麻醉费"] == null ? "0" : hashTableIn["麻醉费"].ToString();
            this.tb16.Text = hashTableIn["床位费"] == null ? "0" : hashTableIn["床位费"].ToString();
            this.tb17.Text = hashTableIn["其他费"] == null ? "0" : hashTableIn["其他费"].ToString();
            this.tb18.Text = hashTableIn["中药费"] == null ? "0" : hashTableIn["中药费"].ToString();
            this.tb21.Text = hashTableIn["中药饮片费"] == null ? "0" : hashTableIn["中药饮片费"].ToString();
            this.tb22.Text = hashTableIn["卫生材料费"] == null ? "0" : hashTableIn["卫生材料费"].ToString();
            this.tb23.Text = hashTableIn["化验费"] == null ? "0" : hashTableIn["化验费"].ToString();
            this.tb24.Text = hashTableIn["一般诊疗费"] == null ? "0" : hashTableIn["一般诊疗费"].ToString();

            req = hashTableIn["req"].ToString();

            decimal totalPrice = 0.0m;

            foreach (Control control in panel2.Controls)
            {
                if (control is TextBox)
                {
                    totalPrice += Convert.ToDecimal(control.Text.ToString());
                }
            }
            this.neuTextBox1.Text = totalPrice.ToString();

            return new Hashtable();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string feeList = "";
            //feeList += string.Format(feeItem, totalprice, "1", totalprice, goodsname, totalprice);

            Hashtable hashTableIn = new Hashtable();
            hashTableIn["挂号费"] = this.tb3.Text.ToString();
            hashTableIn["诊查费"] = this.tb4.Text.ToString();
            hashTableIn["检查费"] = this.tb5.Text.ToString();
            hashTableIn["检验费"] = this.tb6.Text.ToString();
            hashTableIn["治疗费"] = this.tb7.Text.ToString();
            hashTableIn["手术费"] = this.tb8.Text.ToString();
            hashTableIn["护理费"] = this.tb9.Text.ToString();
            hashTableIn["材料费"] = this.tb10.Text.ToString();
            hashTableIn["西药费"] = this.tb11.Text.ToString();
            hashTableIn["中成药费"] = this.tb12.Text.ToString();
            hashTableIn["医疗费"] = this.tb13.Text.ToString();
            hashTableIn["会务费"] = this.tb14.Text.ToString();
            hashTableIn["麻醉费"] = this.tb15.Text.ToString();
            hashTableIn["床位费"] = this.tb16.Text.ToString();
            hashTableIn["其他费"] = this.tb17.Text.ToString();
            hashTableIn["中药费"] = this.tb18.Text.ToString();
            hashTableIn["中药饮片费"] = this.tb21.Text.ToString();
            hashTableIn["卫生材料费"] = this.tb22.Text.ToString();
            hashTableIn["化验费"] = this.tb23.Text.ToString();
            hashTableIn["一般诊疗费"] = this.tb24.Text.ToString();


            string feeItem = "<feeItem><taxExcludedAmount>{0}</taxExcludedAmount><num>{1}</num><price>{2}</price><goodsName>{3}</goodsName><taxIncludedAmount>{4}</taxIncludedAmount><goodsCode>{5}</goodsCode></feeItem>";

            int i = 0;
            foreach (DictionaryEntry dic in hashTableIn)
            {
                i++;
                if (dic.Value.ToString() != "0")
                    feeList += string.Format(feeItem, dic.Value.ToString(), "1", dic.Value.ToString(), dic.Key.ToString(), dic.Value.ToString(), "1000" + i.ToString());
            }

            string reqInfo = "<req>" + req + "<feeList>" + feeList + "</feeList></req>";

            MessageBox.Show(reqInfo);
            hanTianinrequestBillingNew(reqInfo, "20210825999");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
