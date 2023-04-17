using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace GZSI.Controls
{
    /// <summary>
    /// 添加/修改医保待遇窗体 by huangchw 2012-12-06
    /// </summary>
    public partial class frmInsuranceManager : Form
    {
        public frmInsuranceManager(Hashtable htPactDescript)
        {
            InitializeComponent();
            this.htPactDescript = htPactDescript;
        }

        private Type type = typeof(ucInsuranceManager.InsuranceTreatmentKind);

        private Hashtable htPactDescript;

        private FS.HISFC.Models.SIInterface.Insurance oldObj;

        private FS.HISFC.BizLogic.Fee.InPatient bizInpatient = new FS.HISFC.BizLogic.Fee.InPatient();

        private static ArrayList alKind;

        private int Add()
        {
            FS.HISFC.Models.SIInterface.Insurance newObj = new FS.HISFC.Models.SIInterface.Insurance();
          

            foreach (DictionaryEntry item in htPactDescript)
            {
                if (item.Value.ToString().Equals(this.cmbPact.Text))
                {
                    newObj.PactInfo.ID = item.Key.ToString();
                    break;
                }
            }

            newObj.Kind.ID = Enum.Format(this.type, Enum.Parse(this.type, this.cmbKind.Text), "d");
            newObj.PartId = this.txtPartID.Text;
            newObj.Rate = FS.FrameWork.Function.NConvert.ToDecimal(this.txtRate.Text);
            newObj.BeginCost = FS.FrameWork.Function.NConvert.ToDecimal(this.txtBeginCost.Text); 
            newObj.EndCost = FS.FrameWork.Function.NConvert.ToDecimal(this.txtEndCost.Text); 
            newObj.Memo = this.txtMemo.Text;

            newObj.OperCode.ID = ((FS.HISFC.Models.Base.Employee)(FS.FrameWork.Management.Connection.Operator)).ID;
            newObj.OperDate = this.bizInpatient.GetDateTimeFromSysDateTime();

            if (this.bizInpatient.InsertInsuranceTreatment(newObj) == -1)
            {
                MessageBox.Show("添加出错，请联系管理员");
                return -1;
            }
            return 1;
        }

        private int Modify()
        {
            FS.HISFC.Models.SIInterface.Insurance newObj = new FS.HISFC.Models.SIInterface.Insurance();
            
            //这三个字段暂时不提供修改
            newObj.PactInfo.ID = this.oldObj.PactInfo.ID;
            newObj.Kind.ID = this.oldObj.Kind.ID;
            newObj.PartId = this.oldObj.PartId;

            newObj.Rate = FS.FrameWork.Function.NConvert.ToDecimal(this.txtRate.Text);
            newObj.BeginCost = FS.FrameWork.Function.NConvert.ToDecimal(this.txtBeginCost.Text);
            newObj.EndCost = FS.FrameWork.Function.NConvert.ToDecimal(this.txtEndCost.Text); 
            newObj.Memo = this.txtMemo.Text;

            newObj.OperCode.ID = ((FS.HISFC.Models.Base.Employee)(FS.FrameWork.Management.Connection.Operator)).ID;
            newObj.OperDate = this.bizInpatient.GetDateTimeFromSysDateTime();
            
            if (bizInpatient.UpdateInsuranceTreatment(this.oldObj, newObj) == -1)
            {
                MessageBox.Show("修改出错，请联系管理员");
                return -1;
            }
            return 1;
        }


        internal void Init(string type)
        {
            if (type.ToLower().Equals("add"))
            {
                this.Text = "添加医保待遇";
            }

            foreach (DictionaryEntry item in htPactDescript)
            {
                string str = item.Value.ToString();
                this.cmbPact.Items.Add(str);
            }
            this.cmbPact.SelectedIndex = 0;


            if (alKind == null || alKind.Count == 0)
            {
                alKind = new ArrayList();
                foreach (string str in Enum.GetNames(typeof(ucInsuranceManager.InsuranceTreatmentKind)))
                {
                    alKind.Add(str);
                }
            }

            for (int i = 0; i < alKind.Count; i++)
            {
                this.cmbKind.Items.Add(alKind[i].ToString());
            }
            this.cmbKind.SelectedIndex = 0;

        }

        internal void Init(string type, FS.HISFC.Models.SIInterface.Insurance oldObj)
        {
            if (type.ToLower().Equals("modify"))
            {
                this.Text = "修改医保待遇";
                this.oldObj = oldObj;

                //名字
                foreach (DictionaryEntry item in htPactDescript)
                {
                    if (oldObj.PactInfo.ID.ToString().Equals(item.Key.ToString()))
                    {
                        this.cmbPact.Items.Add(item.Value.ToString());
                    }
                }
                this.cmbPact.SelectedIndex = 0;

                this.cmbKind.Items.Add((ucInsuranceManager.InsuranceTreatmentKind)Int32.Parse(this.oldObj.Kind.ID) );
                this.cmbKind.SelectedIndex = 0;

                this.txtPartID.Text = this.oldObj.PartId;
                this.txtRate.Text = this.oldObj.Rate.ToString();
                this.txtBeginCost.Text = this.oldObj.BeginCost.ToString();
                this.txtEndCost.Text = this.oldObj.EndCost.ToString();
                this.txtMemo.Text = this.oldObj.Memo;

                //这三个字段不提供修改
                this.cmbPact.Enabled = false;
                this.cmbKind.Enabled = false;
                this.txtPartID.Enabled = false;

            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtPartID.Text.Equals(""))
                {
                    MessageBox.Show("请输入分段序号.");
                    return;
                }
                if (!(int.Parse(this.txtPartID.Text) > 0))
                {
                    MessageBox.Show("分段序号只能是大于0的整数.");
                    return;
                }

                if (this.txtRate.Text.Equals(""))
                {
                    MessageBox.Show("请输入比例.");
                    return;
                }
                if (!(decimal.Parse(this.txtRate.Text) >= 0))
                {
                    MessageBox.Show("比例只能是>=0的数.");
                    return;
                }

                if (this.txtBeginCost.Text.Equals(""))
                {
                    MessageBox.Show("请输入区间开始.");
                    return;
                }
                if (!(decimal.Parse(this.txtBeginCost.Text) >= 0))
                {
                    MessageBox.Show("区间开始只能是>=0的数.");
                    return;
                }

                if (this.txtEndCost.Text.Equals(""))
                {
                    MessageBox.Show("请输入区间结束.");
                    return;
                }
                if (!(decimal.Parse(this.txtEndCost.Text) >= 0))
                {
                    MessageBox.Show("区间结束只能是>=0的数.");
                    return;
                }

                if (this.txtMemo.Text.Length > 50)
                {
                    MessageBox.Show("备注过长.");
                    return;
                }  
            }
            catch
            {
                MessageBox.Show("非法数据.");
                return;
            }
                

            if (this.Text.Equals("添加医保待遇"))
            {
                this.Add();
            }
            else if (this.Text.Equals("修改医保待遇"))
            {
                this.Modify();
            }

            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
