using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.Pharmacy.Maintenance
{
    /// <summary>
    /// [功能描述: 药品基本信息的扩展字段默认控件]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// 说明：修改时如果不确认能否通用则建议本地化实现接口
    /// </summary>
    public partial class ucItemExtend : UserControl, FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IItemExtendControl
    {
        public ucItemExtend()
        {
            InitializeComponent();
        }

        private string settingFile = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PharmacyItemExendControlSetting.xml";

        #region IItemExtendControl 获取设置扩展信息 成员

        public FS.HISFC.Models.Pharmacy.Item Get(ref string errInfo)
        {
            FS.HISFC.Models.Pharmacy.Item item = new FS.HISFC.Models.Pharmacy.Item();

            item.SpecialFlag = this.GetComboxValue(this.ncmbSpecialFlag);
            item.SpecialFlag1 = this.GetComboxValue(this.ncmbSpecialFlag1);
            item.SpecialFlag2 = this.GetComboxValue(this.ncmbSpecialFlag2);
            item.SpecialFlag3 = this.GetComboxValue(this.ncmbSpecialFlag3);
            item.SpecialFlag4 = this.GetComboxValue(this.ncmbSpecialFlag4);

            item.ExtendData1 = this.GetComboxValue(this.ncmbExtend1);
            item.ExtendData2 = this.GetComboxValue(this.ncmbExtend2);
            //item.ExtendData3 = this.ncmbExtend3.Tag.ToString();
            item.ExtendData4 = this.GetComboxValue(this.ncmbExtend4);

            item.ExtNumber1 = FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtExtendNum1.Text);
            item.ExtNumber2 = FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtExtendNum2.Text);
            item.RetailPrice2 = FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtExtendNum3.Text);

            return item;
        }

        public int Set(FS.HISFC.Models.Pharmacy.Item item, ref string errInfo)
        {
            this.SetComboxValue(this.ncmbSpecialFlag, item.SpecialFlag);
            this.SetComboxValue(this.ncmbSpecialFlag1, item.SpecialFlag1);
            this.SetComboxValue(this.ncmbSpecialFlag2, item.SpecialFlag2);
            this.SetComboxValue(this.ncmbSpecialFlag3, item.SpecialFlag3);
            this.SetComboxValue(this.ncmbSpecialFlag4, item.SpecialFlag4);

            this.SetComboxValue(this.ncmbExtend1, item.ExtendData1);
            this.SetComboxValue(this.ncmbExtend2, item.ExtendData2);
            this.SetComboxValue(this.ncmbExtend3, item.ExtendData3);
            this.SetComboxValue(this.ncmbExtend4, item.ExtendData4);

            this.ntxtExtendNum1.Text = item.ExtNumber1.ToString();
            this.ntxtExtendNum2.Text = item.ExtNumber2.ToString();

            this.ntxtExtendNum3.Text = item.RetailPrice2.ToString();

            return 1;
        }

        public int Save(FS.HISFC.Models.Pharmacy.Item item, ref string errInfo)
        {
            return 0;
        }

        #endregion

        #region IItemExtendControl 有效性检测 成员

        public int CheckValid()
        {
            decimal result = 0;
            if (!decimal.TryParse(this.ntxtExtendNum1.Text + "0", out result))
            {
                Function.ShowMessage(this.nlbExtendNum1.Text + "只能是数字类型，请更正", MessageBoxIcon.Information);
                return -1;
            }
            if (!decimal.TryParse(this.ntxtExtendNum2.Text + "0", out result))
            {
                Function.ShowMessage(this.nlbExtendNum2.Text + "只能是数字类型，请更正", MessageBoxIcon.Information);
                return -1;
            }
            if (!decimal.TryParse(this.ntxtExtendNum3.Text + "0", out result))
            {
                Function.ShowMessage(this.nlbRetailPrice2.Text + "只能是数字类型，请更正", MessageBoxIcon.Information);
                return -1;
            }
            return 1;
        }

        public int Clear()
        {
            this.ncmbSpecialFlag.Text = "";
            this.ncmbSpecialFlag1.Text = "";
            this.ncmbSpecialFlag2.Text = "";
            this.ncmbSpecialFlag3.Text = "";
            this.ncmbSpecialFlag4.Text = "";

            this.ncmbSpecialFlag.Tag = "";
            this.ncmbSpecialFlag1.Tag = "";
            this.ncmbSpecialFlag2.Tag = "";
            this.ncmbSpecialFlag3.Tag = "";
            this.ncmbSpecialFlag4.Tag = "";

            this.ncmbExtend1.Tag = "";
            this.ncmbExtend2.Tag = "";
            this.ncmbExtend3.Tag = "";
            this.ncmbExtend4.Tag = "";

            this.ntxtExtendNum1.Text = "";
            this.ntxtExtendNum2.Text = "";

            return 1;
        }

        #endregion

        #region IItemExtendControl 初始化 成员


        public int Init(ref string errInfo)
        {
            if (this.DesignMode)
            {
                return 0;
            }

            this.ngbCommont.Visible = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager;
            this.nlbSettingFile.Text = "配置文件：" + this.settingFile;
            this.ncbShowFields.CheckedChanged -= new EventHandler(ncbShowFields_CheckedChanged);
            this.ncbShowFields.CheckedChanged += new EventHandler(ncbShowFields_CheckedChanged);

            if (!System.IO.File.Exists(this.settingFile))
            {

                SOC.Public.XML.SettingFile.SaveSetting(this.settingFile, this.nlbSpecialFlag.Name, "Text", this.nlbSpecialFlag.Text);
                SOC.Public.XML.SettingFile.SaveSetting(this.settingFile, this.nlbSpecialFlag1.Name, "Text", this.nlbSpecialFlag1.Text);
                SOC.Public.XML.SettingFile.SaveSetting(this.settingFile, this.nlbSpecialFlag2.Name, "Text", this.nlbSpecialFlag2.Text);
                SOC.Public.XML.SettingFile.SaveSetting(this.settingFile, this.nlbSpecialFlag3.Name, "Text", this.nlbSpecialFlag3.Text);
                SOC.Public.XML.SettingFile.SaveSetting(this.settingFile, this.nlbSpecialFlag4.Name, "Text", this.nlbSpecialFlag4.Text);

                SOC.Public.XML.SettingFile.SaveSetting(this.settingFile, this.ncmbSpecialFlag.Name, "DataType", "Bool");
                SOC.Public.XML.SettingFile.SaveSetting(this.settingFile, this.ncmbSpecialFlag1.Name, "DataType", "Bool");
                SOC.Public.XML.SettingFile.SaveSetting(this.settingFile, this.ncmbSpecialFlag2.Name, "DataType", "Bool");
                SOC.Public.XML.SettingFile.SaveSetting(this.settingFile, this.ncmbSpecialFlag3.Name, "DataType", "Bool");
                SOC.Public.XML.SettingFile.SaveSetting(this.settingFile, this.ncmbSpecialFlag4.Name, "DataType", "Bool");

                SOC.Public.XML.SettingFile.SaveSetting(this.settingFile, this.ncmbExtend1.Name, "DataType", "");
                SOC.Public.XML.SettingFile.SaveSetting(this.settingFile, this.ncmbExtend1.Name, "DropDownStyle", "Simple");

                SOC.Public.XML.SettingFile.SaveSetting(this.settingFile, this.ncmbExtend2.Name, "DataType", "Constant");
                SOC.Public.XML.SettingFile.SaveSetting(this.settingFile, this.ncmbExtend2.Name, "Items", "BASEDRUGCODE");


                SOC.Public.XML.SettingFile.SaveSetting(this.settingFile, this.ncmbExtend3.Name, "DataType", "");
                SOC.Public.XML.SettingFile.SaveSetting(this.settingFile, this.ncmbExtend3.Name, "DropDownStyle", "Simple");


                SOC.Public.XML.SettingFile.SaveSetting(this.settingFile, this.ncmbExtend4.Name, "DataType", "");
                SOC.Public.XML.SettingFile.SaveSetting(this.settingFile, this.ncmbExtend4.Name, "DropDownStyle", "Simple");

                SOC.Public.XML.SettingFile.SaveSetting(this.settingFile, this.nlbExtend1.Name, "Text", this.nlbExtend1.Text);
                SOC.Public.XML.SettingFile.SaveSetting(this.settingFile, this.nlbExtend2.Name, "Text", this.nlbExtend2.Text);
                SOC.Public.XML.SettingFile.SaveSetting(this.settingFile, this.nlbExtend3.Name, "Text", this.nlbExtend3.Text);
                SOC.Public.XML.SettingFile.SaveSetting(this.settingFile, this.nlbExtend4.Name, "Text", this.nlbExtend4.Text);

                SOC.Public.XML.SettingFile.SaveSetting(this.settingFile, this.nlbExtendNum1.Name, "Text", this.nlbExtendNum1.Text);
                SOC.Public.XML.SettingFile.SaveSetting(this.settingFile, this.nlbExtendNum2.Name, "Text", this.nlbExtendNum2.Text);
                SOC.Public.XML.SettingFile.SaveSetting(this.settingFile, this.nlbRetailPrice2.Name, "Text", this.nlbRetailPrice2.Text);



            }

            this.SetLabel(this.nlbSpecialFlag);
            this.SetLabel(this.nlbSpecialFlag1);
            this.SetLabel(this.nlbSpecialFlag2);
            this.SetLabel(this.nlbSpecialFlag3);
            this.SetLabel(this.nlbSpecialFlag4);

            this.SetLabel(this.nlbExtend1);
            this.SetLabel(this.nlbExtend2);
            this.SetLabel(this.nlbExtend3);
            this.SetLabel(this.nlbExtend4);

            this.SetLabel(this.nlbExtendNum1);
            this.SetLabel(this.nlbExtendNum2);
            this.SetLabel(this.nlbRetailPrice2);

            this.SetComBox(this.ncmbSpecialFlag);
            this.SetComBox(this.ncmbSpecialFlag1);
            this.SetComBox(this.ncmbSpecialFlag2);
            this.SetComBox(this.ncmbSpecialFlag3);
            this.SetComBox(this.ncmbSpecialFlag4);

            this.SetComBox(this.ncmbExtend1);
            this.SetComBox(this.ncmbExtend2);
            this.SetComBox(this.ncmbExtend3);
            this.SetComBox(this.ncmbExtend4);

            return 1;
        }

        void ncbShowFields_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control gb in this.Controls)
            {
                foreach (Control c in gb.Controls)
                {
                    if (c.Name.Contains("Label") && c is Label)
                    {
                        c.Visible = this.ncbShowFields.Checked;
                    }
                }
            }
        }

        #endregion

        private int SetTextBox(FS.FrameWork.WinForms.Controls.NeuTextBox textBox)
        {
            textBox.Enabled = FS.FrameWork.Function.NConvert.ToBoolean(SOC.Public.XML.SettingFile.ReadSetting(this.settingFile, textBox.Name, "Enabled", textBox.Enabled.ToString()));
            return 1;
        }

        private int SetLabel(FS.FrameWork.WinForms.Controls.NeuLabel label)
        {
            label.Text = SOC.Public.XML.SettingFile.ReadSetting(this.settingFile, label.Name, "Text", label.Text);
            return 1;
        }

        private int SetComBox(FS.FrameWork.WinForms.Controls.NeuComboBox comboBox)
        {
            string dataType = SOC.Public.XML.SettingFile.ReadSetting(this.settingFile, comboBox.Name, "DataType", "Bool");
            if (dataType.ToLower() == "bool")
            {
                this.SetItems(comboBox);
            }
            else if (dataType.ToLower() == "items")
            {
                this.SetItems(comboBox);
            }
            else if (dataType.ToLower() == "constant")
            {
                this.SetConstantItems(comboBox);
            }

            string dropDownStyle = SOC.Public.XML.SettingFile.ReadSetting(this.settingFile, comboBox.Name, "DropDownStyle", "DropDown");
            if (dropDownStyle.ToLower() == "DropDownList".ToLower())
            {
                comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            else if (dropDownStyle.ToLower() == "Simple".ToLower())
            {
                comboBox.DropDownStyle = ComboBoxStyle.Simple;
            }
            else
            {
                comboBox.DropDownStyle = ComboBoxStyle.DropDown;
            }

            comboBox.Enabled = FS.FrameWork.Function.NConvert.ToBoolean(SOC.Public.XML.SettingFile.ReadSetting(this.settingFile, comboBox.Name, "Enabled", comboBox.Enabled.ToString()));
            return 1;
        }

        private string GetComboxValue(FS.FrameWork.WinForms.Controls.NeuComboBox comboBox)
        {
            if (comboBox.DropDownStyle == ComboBoxStyle.Simple)
            {
                return comboBox.Text;
            }
            if (comboBox.Tag == null)
            {
                return "";
            }
            return comboBox.Tag.ToString();
        }

        private void SetComboxValue(FS.FrameWork.WinForms.Controls.NeuComboBox comboBox, string value)
        {
            if (comboBox.DropDownStyle == ComboBoxStyle.Simple)
            {
                comboBox.Text = value;
            }
            else
            {
                comboBox.Tag = value;
            }
        }


        private int SetItems(FS.FrameWork.WinForms.Controls.NeuComboBox comboBox)
        {
            string items = SOC.Public.XML.SettingFile.ReadSetting(this.settingFile, comboBox.Name, "Items", "否,是");
            if (string.IsNullOrEmpty(items))
            {
                comboBox.Enabled = false;
            }
            int index = 0;
            System.Collections.ArrayList alState = new System.Collections.ArrayList();
            foreach (string item in items.Split(',', ' ', '|'))
            {
                FS.FrameWork.Models.NeuObject state = new FS.FrameWork.Models.NeuObject();
                state.ID = index.ToString();
                state.Name = item;
                alState.Add(state);
                index++;
            }
            comboBox.AddItems(alState);

            return 1;
        }

        private int SetConstantItems(FS.FrameWork.WinForms.Controls.NeuComboBox comboBox)
        {
            string constantType = SOC.Public.XML.SettingFile.ReadSetting(this.settingFile, comboBox.Name, "Items", "");
            if (string.IsNullOrEmpty(constantType))
            {
                comboBox.Enabled = false;
            }
            FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();

            comboBox.AddItems(constantMgr.GetList(constantType));

            return 1;
        }

    }
}
