using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.RADT.Interface.Patient;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.HISFC.Models.Base;
using System.Collections;

namespace FS.SOC.Local.RADT.GuangZhou.GYSY.Base.Patient
{
    /// <summary>
    /// [功能描述: 患者基本信息控件]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
    public partial class ucPatient : FS.FrameWork.WinForms.Controls.ucBaseControl,IPatient
    {
        public ucPatient()
        {
            InitializeComponent();
        }

        private string fileName = FS.FrameWork.WinForms.Classes.Function.SettingPath + "\\SOC.Components.Radt.Patient.xml";

        #region IPatient 成员

        public FS.HISFC.Models.RADT.Patient GetPatient()
        {
            //刷新患者基本信息
            FS.HISFC.Models.RADT.Patient patient = new FS.HISFC.Models.RADT.Patient();

            return patient;
        }

        public int ShowPatient(FS.HISFC.Models.RADT.Patient patient)
        {
            return 1;
        }

        #endregion

        #region IInitialisable 成员

        public int Init()
        {
            //性别列表
            this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());
            this.cmbSex.Text = "男";

            //民族
            this.cmbNation.AddItems(CommonController.CreateInstance().QueryConstant(EnumConstant.NATION));
            this.cmbNation.Text = "汉族";

            //婚姻状态
            this.cmbMarry.AddItems(FS.HISFC.Models.RADT.MaritalStatusEnumService.List());

            //国家
            this.cmbCountry.AddItems(CommonController.CreateInstance().QueryConstant(EnumConstant.COUNTRY));
            this.cmbCountry.Text = "中国";

            //职业信息
            this.cmbProfession.AddItems(CommonController.CreateInstance().QueryConstant(EnumConstant.PROFESSION));

            //工作单位
            this.cmbWorkAddress.AddItems(CommonController.CreateInstance().QueryConstant(EnumConstant.WORKNAME));

            //联系人信息
            this.cmbRelation.AddItems(CommonController.CreateInstance().QueryConstant(EnumConstant.RELATIVE));

            //联系人地址信息
            this.cmbLinkAddress.AddItems(CommonController.CreateInstance().QueryConstant(EnumConstant.AREA));

            //家庭住址信息
            this.cmbHomeAddress.AddItems(CommonController.CreateInstance().QueryConstant(EnumConstant.AREA));

            //籍贯
            this.cmbDistrict.AddItems(CommonController.CreateInstance().QueryConstant(EnumConstant.DIST));

            //地区
            this.cmbArea.AddItems(CommonController.CreateInstance().QueryConstant(EnumConstant.AREA));

            //证件类型
            this.cmbCardType.AddItems(CommonController.CreateInstance().QueryConstant("IDCard"));

            FS.FrameWork.Management.ControlParam ctlParam = new FS.FrameWork.Management.ControlParam();

            return 1;
        }

        #endregion

        #region ILoadable 成员

        public new int Load()
        {
            Function.ReadConfig(this, this.fileName);
            return 1;
        }

        #endregion

        #region IClearable 成员

        public int Clear()
        {
            this.txtAge.ReadOnly = false;
            this.ckEncrypt.Checked = false;
            this.cmbCountry.Text = "中国";
            this.cmbSex.Text = "男";
            this.cmbSex.Tag = "M";
            this.cmbNation.Text = "汉族";
            this.cmbNation.Tag = "1";
            this.dtpBirthDay.Value = CommonController.CreateInstance().GetSystemTime();
            this.ckVip.Checked = false;

            this.pictureBox.Image = null;
            this.pictureBox.Tag = null;

            return 1;
        }

        #endregion

        #region IValidabe 成员

        public bool InputValid()
        {
            Function.ValidConfig(this);

            return true;
        }

        #endregion
    }
}
