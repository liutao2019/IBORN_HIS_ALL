using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using System.Collections;

namespace FS.HISFC.Components.Speciment
{
    public partial class ucSpecTypeApply : UserControl
    {
        private DisTypeManage disTypeManage;
        private SpecTypeManage specTypeManage;
        private OrgTypeManage orgTypeManage;
        public ApplySpecDemand applySpecDemand;
       

        public ucSpecTypeApply()
        {
            InitializeComponent();
            disTypeManage = new DisTypeManage();
            specTypeManage = new SpecTypeManage();
            orgTypeManage = new OrgTypeManage();
            applySpecDemand = new ApplySpecDemand();      
        }

        public ApplySpecDemand SpecDemand
        {
            get
            {
                ApplySpecSet();
                return applySpecDemand;
            }
            set
            {
                applySpecDemand = value;
            }
        }

        /// <summary>
        /// 绑定病种类型
        /// </summary>
        private void DisTypeBinding()
        {
            Dictionary<int, string> dicDisType = disTypeManage.GetAllDisType();
            if (dicDisType.Count > 0)
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = dicDisType;
                cmbDisType.DisplayMember = "Value";
                cmbDisType.ValueMember = "Key";
                cmbDisType.DataSource = bs;
            }
        }

        /// <summary>
        /// 组织类型绑定
        /// </summary>
        private void OrgTypeBinding()
        {
            Dictionary<int, string> orgTypeDic = new Dictionary<int, string>();
            orgTypeDic = orgTypeManage.GetAllOrgType();
            if (orgTypeDic.Count > 0)
            {
                //orgTypeDic.Add(-1,"");
                BindingSource bsTmp = new BindingSource();
                bsTmp.DataSource = orgTypeDic;
                cmbOrgType.ValueMember = "Key";
                cmbOrgType.DisplayMember = "Value";
                cmbOrgType.DataSource = bsTmp;
                cmbOrgType.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 根据组织类型ID获取标本类型
        /// </summary>
        /// <param name="orgTypeID"></param>
        private void SpecTypeBinding(string orgTypeID)
        {
            cmbSpecType.DataSource = null;
            Dictionary<int, string> specTypeDic = new Dictionary<int, string>();
            specTypeDic = specTypeManage.GetSpecTypeByOrgID(orgTypeID);
            if (specTypeDic.Count > 0)
            {
                //specTypeDic.Add(-1, "");
                BindingSource bsTmp = new BindingSource();
                bsTmp.DataSource = specTypeDic;
                cmbSpecType.ValueMember = "Key";
                cmbSpecType.DisplayMember = "Value";
                cmbSpecType.DataSource = bsTmp;
                cmbSpecType.SelectedIndex = 0;
            }
        }

        private void ucSpecTypeApply_Load(object sender, EventArgs e)
        {
            DisTypeBinding();
            OrgTypeBinding();            
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void cmbOrgType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string orgTypeName = cmbOrgType.Text;
            int orgId = orgTypeManage.SelectOrgByName(orgTypeName);
            if (orgId > 0)
            {
                SpecTypeBinding(orgId.ToString());                
            }
            if (orgTypeName.Contains("组织"))
            {
                flpTumorType.Controls.Clear();
                RadioButton rbt1 = new RadioButton();
                rbt1.Text = "肿物";
                rbt1.Name = "rbtTumor";
                rbt1.Checked = true;
                rbt1.AutoSize = true;
                flpTumorType.Controls.Add(rbt1);
                RadioButton rbt2 = new RadioButton();
                rbt2.Text = "子瘤";
                rbt2.Name = "rbtSub";
                rbt2.AutoSize = true;
                flpTumorType.Controls.Add(rbt2);
                RadioButton rbt3 = new RadioButton();
                rbt3.Text = "癌旁";
                rbt3.Name = "rbtSide";
                rbt3.AutoSize = true;
                flpTumorType.Controls.Add(rbt3);
                RadioButton rbt4 = new RadioButton();
                rbt4.Text = "正常、切端";
                rbt4.Name = "rbtNormal";
                rbt4.AutoSize = true;
                flpTumorType.Controls.Add(rbt4);
                RadioButton rbt5 = new RadioButton();
                rbt5.Text = "癌栓";
                rbt5.Name = "rbtShuan";
                rbt5.AutoSize = true;
                flpTumorType.Controls.Add(rbt5);
                RadioButton rbt6 = new RadioButton();
                rbt6.Text = "淋巴结";
                rbt6.Name = "rbtLinBa";
                rbt6.AutoSize = true;
                flpTumorType.Controls.Add(rbt6);
                RadioButton rbt7 = new RadioButton();
                rbt7.Text = "转移癌";
                rbt7.Name = "rbtTransfer";
                rbt7.AutoSize = true;
                flpTumorType.Controls.Add(rbt7);
            }
            if(orgTypeName.Contains("血"))
            {
                flpTumorType.Controls.Clear();
                RadioButton rbt1 = new RadioButton();
                rbt1.Text = "癌变";
                rbt1.Name = "rbtIsCancer";
                rbt1.Checked = true;
                flpTumorType.Controls.Add(rbt1);
                RadioButton rbt2 = new RadioButton();
                rbt2.Text = "正常";
                rbt2.Name = "rbtNoCancer";
                flpTumorType.Controls.Add(rbt2);
            }           
        }

        private void ApplySpecSet()
        {
            applySpecDemand.currentCount = Convert.ToInt32(this.nudCount.Value);
            applySpecDemand.orgType = cmbOrgType.SelectedValue == null ? "" : cmbOrgType.SelectedValue.ToString();
            applySpecDemand.specType = cmbSpecType.SelectedValue == null ? "" : cmbSpecType.SelectedValue.ToString();
            applySpecDemand.disType = cmbDisType.SelectedValue == null ? "" : cmbDisType.SelectedValue.ToString();
            foreach (Control c in flpTumorType.Controls)
            {
                RadioButton rbt = c as RadioButton;
                if (rbt.Checked)
                {
                    switch (rbt.Name)
                    {
                        case "rbtIsCancer":
                            applySpecDemand.tumorType = "6";
                            break;
                        case "rbtNoCancer":
                            applySpecDemand.tumorType = "7";
                            break;
                        case "rbtTumor":
                            applySpecDemand.tumorType = "1";
                            break;
                        case "rbtSub":
                            applySpecDemand.tumorType = "2";
                            break;
                        case "rbtSide":
                            applySpecDemand.tumorType = "3";
                            break;
                        case "rbtNormal":
                            applySpecDemand.tumorType = "4";
                            break;
                        case "rbtShuan":
                            applySpecDemand.tumorType = "5";
                            break;
                        case "rbtLinBa":
                            applySpecDemand.tumorType = "8";
                            break;
                        default:
                            applySpecDemand.tumorType = "0";
                            break;
                    }
                    break;
                }
            }            
        }
    
    }

    public class ApplySpecDemand
    {
        public string orgType = "";
        public string specType = "";
        public string disType = "";
        public int currentCount = 0;
        public string tumorType = "";
        public int applySpecDemandId = 0;
 
    }
}
