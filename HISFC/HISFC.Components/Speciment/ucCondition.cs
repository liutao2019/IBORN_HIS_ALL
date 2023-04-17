using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace UFC.Speciment
{
    public partial class ucCondition : UserControl
    {
        public ucCondition()
        {
            InitializeComponent();
        }

        public void FieldBinding(Dictionary<string, string> filed)
        {
            if (filed.Count > 0)
            {
                BindingSource bsTmp = new BindingSource();
                bsTmp.DataSource = filed;
                cmbField.DataSource = bsTmp; 
                cmbField.DisplayMember = "Key";
                cmbField.ValueMember = "Value";                
            }
            //if (dicType.Count > 0)
            //{
            //    BindingSource bs = new BindingSource();
            //    bs.DataSource = dicType;
            //    cmbDisType.DisplayMember = "Value";
            //    cmbDisType.ValueMember = "Key";
            //    cmbDisType.DataSource = bs;
            //}
        }

        private void ucCondition_Load(object sender, EventArgs e)
        {

        }

        private void cmbField_SelectedValueChanged(object sender, EventArgs e)
        {
            cmbOperator.Items.Clear();
            string[] oper = new string[] { "<", "=", ">", "<=", ">=","!=" };
            string[] stringOper = new string[] { "like", "=" };
            string type = cmbField.SelectedValue.ToString();
            switch (type)
            {
                case "INTEGER":
                case "TIMESTAMP":                   
                    foreach (string op in oper)
                    {                        
                        cmbOperator.Items.Add(op);
                    }
                    break;
                default:
                    foreach (string op in stringOper)
                    {                        
                        cmbOperator.Items.Add(op);
                    }
                    break;
            }
        }
    }
}
