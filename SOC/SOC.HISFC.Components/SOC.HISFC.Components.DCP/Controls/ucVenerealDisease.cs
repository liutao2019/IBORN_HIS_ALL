using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.DCP.Controls
{
    /// <summary>
    /// 性病附卡
    /// </summary>
    public partial class ucVenerealDisease : ucBaseAddition
    {
        public ucVenerealDisease()
        {
            InitializeComponent();
            this.Init();
        }

        #region 初始化
        /// <summary>
        /// 接触史帮助类
        /// </summary>
        FS.FrameWork.Public.ObjectHelper techHelper = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// 初始化信息
        /// </summary>
        public void Init()
        {
            //下拉框
            InitCmb();
            InitCmbStyle();
        }

        /// <summary>
        /// 初始化下拉框
        /// </summary>
        public void InitCmb()
        {
            FS.SOC.HISFC.BizProcess.DCP.Common commonProcess = new FS.SOC.HISFC.BizProcess.DCP.Common();
            //婚姻状况
            ArrayList alMarry = commonProcess.QueryConstantList("MARRY_STATE");
            this.cmbMarry.AddItems(alMarry);

            //民族
            ArrayList alNation = commonProcess.QueryConstantList(FS.HISFC.Models.Base.EnumConstant.NATION);
            this.cmbNation.AddItems(alNation);

            //文化程度
            ArrayList alEducation = commonProcess.QueryConstantList(FS.HISFC.Models.Base.EnumConstant.EDUCATION);
            this.cmbEducation.AddItems(alEducation);

            //接触史
            ArrayList alTatch = commonProcess.QueryConstantList("Tatch");
            if (alTatch == null)
            {
                alTatch = new ArrayList();
            }
            this.cmbTatch.AddItems(alTatch);
            this.techHelper.ArrayObject = alTatch;
            //感染途径
            ArrayList alChannel = commonProcess.QueryConstantList("InfectChannel");
            if (alChannel == null)
            {
                alChannel = new ArrayList();
            }
            this.cmbChannel.AddItems(alChannel);

            //样本来源
            ArrayList alSampleSource = commonProcess.QueryConstantList("SampleSource");
            if (alSampleSource == null)
            {
                alSampleSource = new ArrayList();
            }
            this.cmbSampleSource.AddItems(alSampleSource);
        }

        /// <summary>
        /// 初始化CMB的格式
        /// </summary>
        public void InitCmbStyle()
        {
            foreach (Control c in this.gbSexAddition.Controls)
            {
                if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuComboBox))
                {
                    ((FS.FrameWork.WinForms.Controls.NeuComboBox)c).DropDownStyle = ComboBoxStyle.DropDownList;
                }
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 验证附卡信息是否完整
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <returns>-1 不完整，1 完整</returns>
        public override int IsValid(ref string msg)
        {
            int ret = 1;
            if (this.cmbMarry.SelectedValue == null && this.cmbMarry.Tag.ToString() == "")
            {
                msg += "婚姻||";
                ret = -1;
            }
            if (this.cmbNation.SelectedValue == null && this.cmbNation.Tag.ToString() == "")
            {
                msg += "民族||";
                ret = -1;
            }
            if (this.cmbEducation.SelectedValue == null && this.cmbEducation.Tag.ToString() == "")
            {
                msg += "文化程度||";
                ret = -1;
            }
            if (!this.panelTech.Visible && this.cmbTatch.SelectedValue == null && this.cmbTatch.Tag.ToString() == "")
            {
                msg += "接触史||";
                ret = -1;
            }
            if (this.panelTech.Visible &&
                (!this.checkBox1.Checked || !this.neuCheckBox1.Checked ||!this.neuCheckBox2.Checked ||
                !this.neuCheckBox3.Checked||!this.neuCheckBox4.Checked||!this.neuCheckBox5.Checked ||
                !this.neuCheckBox6.Checked||!this.neuCheckBox7.Checked||!this.neuCheckBox8.Checked ||
                !this.neuCheckBox9.Checked||!this.neuCheckBox10.Checked))
            {
            }
            if (this.cmbChannel.SelectedValue == null && this.cmbChannel.Tag.ToString() == "")
            {
                msg += "感染途径||";
                ret = -1;
            }
            if (this.cmbSampleSource.SelectedValue == null && this.cmbSampleSource.Tag.ToString() == "")
            {
                msg += "样本来源||";
                ret = -1;
            }
            if (this.txtTestUnit.Text == "" || this.txtTestUnit.Text == null)
            {
                msg += "确认（策略）检测单位||";
                ret = -1;
            }
            return ret;
        }

        public override void PrePrint()
        {
            this.gbSexAddition.BackColor = Color.White;
            this.BackColor = Color.White;
            //this.cl1.Visible = false;
            //this.cl2.Visible = false;
            //this.cl3.Visible = false;
            //this.cl4.Visible = false;
            //this.cl5.Visible = false;
            //this.cl6.Visible = false;
            //this.cl7.Visible = false;
            //this.cl8.Visible = false;
            //this.cl9.Visible = false;
            //this.cl10.Visible = false;
            base.PrePrint();
        }

        public override void Printed()
        {
            this.gbSexAddition.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.BackColor = System.Drawing.Color.FromArgb(158, 177, 201);
            //this.cl1.Visible = true;
            //this.cl2.Visible = true;
            //this.cl3.Visible = true;
            //this.cl4.Visible = true;
            //this.cl5.Visible = true;
            //this.cl6.Visible = true;
            //this.cl7.Visible = true;
            //this.cl8.Visible = true;
            //this.cl9.Visible = true;
            //this.cl10.Visible = true;
            base.Printed();
        }

        public override void Clear()
        {
            this.cmbChannel.Text = "";
            this.cmbChannel.Tag = "";
            this.cmbEducation.Text = "";
            this.cmbEducation.Tag = "";
            this.cmbMarry.Text = "";
            this.cmbMarry.Tag = "";
            this.cmbNation.Text = "";
            this.cmbNation.Tag = "";
            this.cmbSampleSource.Text = "";
            this.cmbSampleSource.Tag = "";
            this.cmbTatch.Text = "";
            this.cmbTatch.Tag = "";
            this.rbSexHisN.Checked = true;
            this.neuDateTimePicker1.Value = sysdate;
            this.txtTestUnit.Text = "";
            this.txtTechPerson.Text = "";
            label5.Visible = false;
            cl4.Visible = false;
            cmbTatch.Visible = false;
            this.panelTech.Visible = true;
            base.Clear();
        }
        #endregion

        private void cmbTatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbTatch.Tag != null && this.cmbTatch.Tag.ToString() != "")
            {
                label5.Visible = true;
                cl4.Visible = true;
                cmbTatch.Visible = true;
                this.panelTech.Visible = false;
                if (techHelper.GetName(this.cmbTatch.Tag.ToString()) == "注射毒品史")
                {
                    this.label6.Visible = true;
                    this.txtTechPerson.Visible = true;
                    this.label8.Visible = true;
                    label8.Text = "人与您共用过注射器?)";
                }
                else if (techHelper.GetName(this.cmbTatch.Tag.ToString()) == "非婚异性性接触史"
                    || techHelper.GetName(this.cmbTatch.Tag.ToString()) == "非婚异性接触史")
                {
                    this.label6.Visible = true;
                    this.txtTechPerson.Visible = true;
                    this.label8.Visible = true;
                    label8.Text = "人与您有过非婚性行为?)";
                }
                else if (techHelper.GetName(this.cmbTatch.Tag.ToString()) == "男男性行为史")
                {
                    this.label6.Visible = true;
                    this.txtTechPerson.Visible = true;
                    this.label8.Visible = true;
                    label8.Text = "人与您有过同性性行为?)";
                }
                else
                {
                    this.label6.Visible = false;
                    this.txtTechPerson.Visible = false;
                    this.label8.Visible = false;
                }
            }
            else
            {
                label5.Visible = false;
                cl4.Visible = false;
                cmbTatch.Visible = false;
                this.panelTech.Visible = true;
            }
        }
        
    }
}
