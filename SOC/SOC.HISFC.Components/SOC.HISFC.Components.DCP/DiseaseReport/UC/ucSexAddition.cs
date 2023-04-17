using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.UFC.DCP.DiseaseReport.UC
{
    /// <summary>
    /// [功能描述： 性病附卡uc]
    /// [创 建 者： ZJ]
    /// [创建时间： 2008-08-25]
    /// </summary>
    public partial class ucSexAddition : ucBaseAddition
    {
        public ucSexAddition()
        {
            InitializeComponent();
            this.Init();
        }

        #region 初始化

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
            Neusoft.HISFC.DCP.BizProcess.Common commonProcess = new Neusoft.HISFC.DCP.BizProcess.Common();
            //婚姻状况
            ArrayList alMarry = commonProcess.QueryConstantList("MARRY_STATE");
            this.cmbMarry.AddItems(alMarry);

            //民族
            ArrayList alNation = commonProcess.QueryConstantList(Neusoft.HISFC.Models.Base.EnumConstant.NATION);
            this.cmbNation.AddItems(alNation);

            //文化程度
            ArrayList alEducation = commonProcess.QueryConstantList(Neusoft.HISFC.Models.Base.EnumConstant.EDUCATION);
            this.cmbEducation.AddItems(alEducation);

            //接触史
            ArrayList alTatch = commonProcess.QueryConstantList("Tatch");
            if (alTatch == null)
            {
                alTatch = new ArrayList();
            }
            this.cmbTatch.AddItems(alTatch);

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
                if (c.GetType() == typeof(Neusoft.FrameWork.WinForms.Controls.NeuComboBox))
                {
                    ((Neusoft.FrameWork.WinForms.Controls.NeuComboBox)c).DropDownStyle = ComboBoxStyle.DropDownList;
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
        public new int IsValid(ref string msg)
        {
            int ret=1;
            if (this.cmbMarry.SelectedValue == null &&this.cmbMarry.Tag.ToString() =="")
            {
                msg += "婚姻||";
                ret =-1;
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
            if (this.cmbTatch.SelectedValue == null && this.cmbTatch.Tag.ToString() == "")
            {
                msg += "接触史||";
                ret = -1;
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

        #endregion
    }
}
