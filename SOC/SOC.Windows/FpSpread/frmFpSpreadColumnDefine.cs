using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Windows.Forms
{
    public partial class frmFpSpreadColumnDefine : Form
    {
        /// <summary>
        /// 注意和fpspread一起使用
        /// </summary>
        /// <param name="settingFileName"></param>
        /// <param name="sheetName"></param>
        public frmFpSpreadColumnDefine(string settingFileName,string sheetName)
        {
            InitializeComponent();
            this.fpSpread1.Sheets[0].SheetName = sheetName;
            this.curSettingFileName = settingFileName;
            this.txtSettingFile.Text = settingFileName;
            this.fpSpread1.ReadSchema(settingFileName);
            this.SetColumnTextName();
        }

        private string curSettingFileName = "";


        private int GetColumnCountSetting()
        {

            //加载xml文件
            System.Xml.XmlNode columnNode = SOC.Public.XML.File.GetNode(this.curSettingFileName,"//Column");

            if (columnNode != null)
            {
                if (columnNode.Attributes.GetNamedItem("Count") != null)
                {
                    int colCount = 0;
                    if (int.TryParse(columnNode.Attributes["Count"].InnerText, out colCount))
                    {
                        return colCount;
                    }

                }
               
            }

            return -1;
        }

        private void SetColumnTextName()
        {
            int colCount = this.GetColumnCountSetting();
            if (colCount == -1)
            {
                return;
            }

            this.fpSpread1.Sheets[0].Rows[0].Label = "原名";
            this.fpSpread1.Sheets[0].Rows[1].Label = "别名";

            for (int index = 0; index < colCount; index++)
            {
                System.Xml.XmlNode columnNode = SOC.Public.XML.File.GetNode(this.curSettingFileName,"//Row0Col"+index.ToString());
                if (columnNode != null)
                {
                    try
                    {
                        this.fpSpread1.Sheets[0].Cells[0, index].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                        this.fpSpread1.Sheets[0].Cells[1, index].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                        this.fpSpread1.Sheets[0].Cells[0, index].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                        this.fpSpread1.Sheets[0].Cells[0, index].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                        this.fpSpread1.Sheets[0].Cells[1, index].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                        this.fpSpread1.Sheets[0].Cells[1, index].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                        if (((System.Xml.XmlElement)columnNode).HasAttribute("Text"))
                        {
                            this.fpSpread1.Sheets[0].Cells[0, index].Text = columnNode.Attributes["Text"].InnerText;
                        }
                        if (((System.Xml.XmlElement)columnNode).HasAttribute("TextAS"))
                        {
                            this.fpSpread1.Sheets[0].Cells[1, index].Text = columnNode.Attributes["TextAS"].InnerText;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                
                }
            }
        }

        private void SaveColumnSetting()
        {
            System.Xml.XmlDocument XmlDocument = new System.Xml.XmlDocument();

            //加载xml文件
            XmlDocument.Load(this.curSettingFileName);
            for (int index = 0; index < this.fpSpread1.Sheets[0].ColumnCount; index++)
            {
                System.Xml.XmlNode node = XmlDocument.SelectSingleNode("//Row0Col" + index.ToString());
                if (node != null)
                {
                    System.Xml.XmlElement xmlElement = (System.Xml.XmlElement)node;
                    xmlElement.SetAttribute("Text", this.fpSpread1.Sheets[0].Cells[0, index].Text);
                    xmlElement.SetAttribute("TextAS", this.fpSpread1.Sheets[0].Cells[1, index].Text);
                }
            }

            XmlDocument.Save(this.curSettingFileName);
        }


        private void btSave_Click(object sender, EventArgs e)
        {
            this.fpSpread1.SaveSchema(curSettingFileName);
            this.SaveColumnSetting();
            this.fpSpread1.ReadSchema(this.curSettingFileName);
            MessageBox.Show("操作完成！\n文件："+this.curSettingFileName+"\n注意：设置可能要在界面退出后重新打开才能生效，如果程序不正常请删除配置文件进行修复\n");

        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
