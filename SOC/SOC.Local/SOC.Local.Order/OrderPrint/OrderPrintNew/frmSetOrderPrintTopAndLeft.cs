using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Order.OrderPrint.OrderPrintNew
{
    /// <summary>
    /// 医嘱单打印边距设置
    /// </summary>
    public partial class frmSetOrderPrintTopAndLeft : Form
    {
        public int printLongTop = 8;

        public int printLongLeft = 64;

        public int printShortTop = 4;

        public int printShortLeft = 60;

        public int printLongPageY = 55;

        public int printShortPageY = 65;

        private string longNode = "LongOrderPrint";
        private string shortNode = "ShortOrderPrint";
        public bool IsNewOrderPrint
        {
            set
            {
                if (value)
                {
                    this.longNode = "LongOrderPrintNew";
                    shortNode = "ShortOrderPrintNew";
                }
                else
                {
                    this.longNode = "LongOrderPrint";
                    shortNode = "ShortOrderPrint";
                }
            }
        }

        private string orderPrintSetting = "orderPrintSetting.xml";

        public frmSetOrderPrintTopAndLeft()
        {
            InitializeComponent();
        }

        private void neuButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void neuButton1_Click(object sender, EventArgs e)
        {
            //取配置文件
            try
            {
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath + this.orderPrintSetting);

                doc.SelectSingleNode("OrderPrintSetting//"+longNode).Attributes["Top"].Value = this.neuNumericTextBox1.Text;
                doc.SelectSingleNode("OrderPrintSetting//"+longNode).Attributes["Left"].Value = this.neuNumericTextBox3.Text;
                doc.SelectSingleNode("OrderPrintSetting//"+longNode).Attributes["PageY"].Value = this.ntbLongPageY.Text;
                doc.SelectSingleNode("OrderPrintSetting//"+shortNode).Attributes["Top"].Value = this.neuNumericTextBox4.Text;
                doc.SelectSingleNode("OrderPrintSetting//"+shortNode).Attributes["Left"].Value = this.neuNumericTextBox2.Text;
                doc.SelectSingleNode("OrderPrintSetting//"+shortNode).Attributes["PageY"].Value = this.ntbShortPageY.Text;

                doc.Save(FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath + this.orderPrintSetting);

                this.printLongTop = FS.FrameWork.Function.NConvert.ToInt32(FS.FrameWork.Function.NConvert.ToDecimal(this.neuNumericTextBox1.Text) * 40);
                this.printLongLeft = FS.FrameWork.Function.NConvert.ToInt32(FS.FrameWork.Function.NConvert.ToDecimal(this.neuNumericTextBox3.Text) * 40);
                this.printLongPageY = FS.FrameWork.Function.NConvert.ToInt32(FS.FrameWork.Function.NConvert.ToDecimal(this.ntbLongPageY.Text) * 40);
                this.printShortTop = FS.FrameWork.Function.NConvert.ToInt32(FS.FrameWork.Function.NConvert.ToDecimal(this.neuNumericTextBox4.Text) * 40);
                this.printShortLeft = FS.FrameWork.Function.NConvert.ToInt32(FS.FrameWork.Function.NConvert.ToDecimal(this.neuNumericTextBox2.Text) * 40);
                this.printShortPageY = FS.FrameWork.Function.NConvert.ToInt32(FS.FrameWork.Function.NConvert.ToDecimal(this.ntbShortPageY.Text) * 40);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                this.Close();
            }
        }

        private void frmSetOrderPrintTopAndLeft_Load(object sender, EventArgs e)
        {
            #region 创建配置文件

            if (System.IO.Directory.Exists(FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath) == false)
            {
                System.IO.Directory.CreateDirectory(FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath);
            }

            if (System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath + this.orderPrintSetting) == false)
            {
                try
                {
                    System.IO.FileStream f = System.IO.File.Create(FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath + this.orderPrintSetting);
                    f.Close();
                    System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                    System.Xml.XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                    xmlDoc.AppendChild(xmlDeclaration);
                    System.Xml.XmlElement xmlElementParent = xmlDoc.CreateElement("OrderPrintSetting");

                    System.Xml.XmlElement xmlElementLong = xmlDoc.CreateElement(this.longNode);
                    System.Xml.XmlAttribute attribute = xmlDoc.CreateAttribute("Top");
                    attribute.Value = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Function.NConvert.ToDecimal(this.printLongTop) / 40).ToString();
                    xmlElementLong.Attributes.Append(attribute);
                    attribute = xmlDoc.CreateAttribute("Left");
                    attribute.Value = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Function.NConvert.ToDecimal(this.printLongLeft) / 40).ToString();
                    xmlElementLong.Attributes.Append(attribute);
                    attribute = xmlDoc.CreateAttribute("PageY");
                    attribute.Value = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Function.NConvert.ToDecimal(this.printLongPageY) / 40).ToString();
                    xmlElementLong.Attributes.Append(attribute);

                    System.Xml.XmlElement xmlElementShort = xmlDoc.CreateElement(this.shortNode);
                    attribute = xmlDoc.CreateAttribute("Top");
                    attribute.Value = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Function.NConvert.ToDecimal(this.printShortTop) / 40).ToString();
                    xmlElementShort.Attributes.Append(attribute);
                    attribute = xmlDoc.CreateAttribute("Left");
                    attribute.Value = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Function.NConvert.ToDecimal(this.printShortLeft) / 40).ToString();
                    xmlElementShort.Attributes.Append(attribute);
                    attribute = xmlDoc.CreateAttribute("PageY");
                    attribute.Value = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Function.NConvert.ToDecimal(this.printShortPageY) / 40).ToString();
                    xmlElementLong.Attributes.Append(attribute);

                    xmlElementParent.AppendChild(xmlElementLong);
                    xmlElementParent.AppendChild(xmlElementShort);
                    xmlDoc.AppendChild(xmlElementParent);

                    System.Xml.XmlTextWriter xmlWriter = new System.Xml.XmlTextWriter(FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath + this.orderPrintSetting, Encoding.UTF8);
                    xmlWriter.Formatting = System.Xml.Formatting.Indented;
                    xmlWriter.IndentChar = '\t';
                    xmlDoc.WriteContentTo(xmlWriter);
                    xmlWriter.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            #endregion

            //取配置文件
            try
            {
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath + this.orderPrintSetting);

                this.neuNumericTextBox1.Text = doc.SelectSingleNode("OrderPrintSetting//"+longNode).Attributes["Top"].Value;
                this.neuNumericTextBox3.Text = doc.SelectSingleNode("OrderPrintSetting//"+longNode).Attributes["Left"].Value;
                this.ntbLongPageY.Text = doc.SelectSingleNode("OrderPrintSetting//"+longNode).Attributes["PageY"].Value;
                this.neuNumericTextBox4.Text =doc.SelectSingleNode("OrderPrintSetting//"+shortNode).Attributes["Top"].Value;
                this.neuNumericTextBox2.Text = doc.SelectSingleNode("OrderPrintSetting//"+shortNode).Attributes["Left"].Value;
                this.ntbShortPageY.Text = doc.SelectSingleNode("OrderPrintSetting//"+shortNode).Attributes["PageY"].Value;

                this.printLongTop = FS.FrameWork.Function.NConvert.ToInt32(FS.FrameWork.Function.NConvert.ToDecimal(this.neuNumericTextBox1.Text) * 40);
                this.printLongLeft = FS.FrameWork.Function.NConvert.ToInt32(FS.FrameWork.Function.NConvert.ToDecimal(this.neuNumericTextBox3.Text) * 40);
                this.printLongPageY = FS.FrameWork.Function.NConvert.ToInt32(FS.FrameWork.Function.NConvert.ToDecimal(this.ntbLongPageY.Text) * 40);
                this.printShortTop = FS.FrameWork.Function.NConvert.ToInt32(FS.FrameWork.Function.NConvert.ToDecimal(this.neuNumericTextBox4.Text) * 40);
                this.printShortLeft = FS.FrameWork.Function.NConvert.ToInt32(FS.FrameWork.Function.NConvert.ToDecimal(this.neuNumericTextBox2.Text) * 40);
                this.printShortPageY = FS.FrameWork.Function.NConvert.ToInt32(FS.FrameWork.Function.NConvert.ToDecimal(this.ntbShortPageY.Text) * 40);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}