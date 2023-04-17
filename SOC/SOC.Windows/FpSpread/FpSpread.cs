using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using FS.FrameWork.WinForms.Controls;
using System.Windows.Forms;

namespace FS.SOC.Windows.Forms
{
    /// <summary>
    /// Spread<br></br>
    /// [功能描述: Spread控件，提供Schema保存和读取，提供标题名称检索列索引]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    [ToolboxBitmap(typeof(FarPoint.Win.Spread.FpSpread))]
    public partial class FpSpread : FarPoint.Win.Spread.FpSpread, INeuControl
    {
        public FpSpread()
        {
            InitializeComponent();
        }

        public FpSpread(IContainer container)
        {
            container.Add(this);

            InitializeComponent();


        }

        private string curSettingFileName = "";

        private StyleType styleType;

        /// <summary>
        /// 列别名对比，在配置文件中自定的列名称和程序原始定义名称对比
        /// </summary>
        private System.Collections.Hashtable hsColumnASNameCompare = new System.Collections.Hashtable();


        //public System.Collections.Hashtable HsColumnNameCompare
        //{
        //    get { return hsColumnNameCompare; }
        //    set { hsColumnNameCompare = value; }
        //}

        #region INeuControl 成员

        public StyleType Style
        {
            get
            {
                return this.styleType;
            }
            set
            {
                this.styleType = value;
                this.Width += 1;
                this.Width -= 1;
            }
        }

        #endregion

        #region 公开方法

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="excelSaveFlags">FarPoint.Excel.ExcelSaveFlag</param>
        /// <returns>0 取消 1导出成功</returns>
        public virtual bool ExportExcel(FarPoint.Win.Spread.Model.IncludeHeaders includeHeaders)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "(*.xls)|*.xls";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                return this.SaveExcel(dlg.FileName, includeHeaders);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 设置配置文件名称
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        public virtual void SetFileName(string fileName)
        {
            this.curSettingFileName = fileName;
        }
        
        /// <summary>
        /// 保存架构
        /// </summary>
        /// <param name="fileName">xml文件</param>
        /// <returns></returns>
        public virtual bool SaveSchema(string fileName)
        {
            return SaveSchema(null, fileName);
        }        

        /// <summary>
        /// 保存单个sheet架构
        /// </summary>
        /// <param name="fileName">xml文件</param>
        /// <returns></returns>
        public virtual bool SaveSchema(FarPoint.Win.Spread.SheetView sheet, string fileName)
        {
            this.SetFileName(fileName);
            bool isFirstCreate = false;
            if (!System.IO.File.Exists(fileName))
            {
                isFirstCreate = true;
                SOC.Public.XML.File.CreateXmlFile(fileName, "1.0", "Spread");
            }

            //sheet索引，指示程序处理到了哪个sheet
            int sheetIndex = 1;

            System.Xml.XmlDocument XmlDocument = new System.Xml.XmlDocument();

            //加载xml文件
            XmlDocument.Load(fileName);

            //获取Spread根节点
            System.Xml.XmlElement spreadNode = XmlDocument.DocumentElement;

            string sheetName = "";

            foreach (FarPoint.Win.Spread.SheetView sv in this.Sheets)
            {
                if (sheet != null && sheet != sv)
                {
                    continue;
                }

                if (sheet == null)
                {
                    //获取sheet的节点
                    sheetName = sv.SheetName;
                }
                else
                {
                    sheetName = "NormalSheet";
                }
                if (string.IsNullOrEmpty(sheetName))
                {
                    sheetName = "SheetName" + sheetIndex.ToString();
                }

                System.Xml.XmlElement sheetNode = this.GetXmlElement(XmlDocument, spreadNode, sheetName);
                sheetNode.SetAttribute("Locked", (sv.DefaultStyle.Locked ? "True" : "False"));
                sheetNode.SetAttribute("HorizontalAlignment", sv.DefaultStyle.HorizontalAlignment.ToString());
                sheetNode.SetAttribute("VerticalAlignment", sv.DefaultStyle.VerticalAlignment.ToString());
                sheetNode.SetAttribute("ZoomFactor", sv.ZoomFactor.ToString());


                #region 行头设置

                System.Xml.XmlElement RowHeaderNode = this.GetXmlElement(XmlDocument, sheetNode, "RowHeader");

                RowHeaderNode.SetAttribute("Visible", (sv.RowHeader.Visible ? "True" : "False"));
                if (sv.RowHeader.DefaultStyle.Font != null)
                {
                    RowHeaderNode.SetAttribute("FontName", sv.RowHeader.DefaultStyle.Font.FontFamily.Name);
                    RowHeaderNode.SetAttribute("FontSize", sv.RowHeader.DefaultStyle.Font.Size.ToString());
                    RowHeaderNode.SetAttribute("FontStyle", sv.RowHeader.DefaultStyle.Font.Style.ToString());
                }

                #endregion
                System.Xml.XmlElement RowDefaultNode = this.GetXmlElement(XmlDocument, sheetNode, "RowsDefault");
                RowDefaultNode.SetAttribute("Height", sv.Rows.Default.Height.ToString());
                RowDefaultNode.SetAttribute("VerticalAlignment", sv.Rows.Default.VerticalAlignment.ToString());
                RowDefaultNode.SetAttribute("HorizontalAlignment", sv.Rows.Default.HorizontalAlignment.ToString());

                #region 列、列头设置
                System.Xml.XmlElement ColumnHeaderRowsNode = this.GetXmlElement(XmlDocument, sheetNode, "ColumnHeaderRows");
                ColumnHeaderRowsNode.SetAttribute("Count", sv.ColumnHeader.RowCount.ToString());
                ColumnHeaderRowsNode.SetAttribute("Height", sv.ColumnHeader.Rows.Default.Height.ToString());

                System.Xml.XmlElement ColumnHeaderDefaultNode = this.GetXmlElement(XmlDocument, sheetNode, "ColumnHeaderDefault");
                ColumnHeaderDefaultNode.SetAttribute("VerticalAlignment", sv.ColumnHeader.DefaultStyle.VerticalAlignment.ToString());
                ColumnHeaderDefaultNode.SetAttribute("HorizontalAlignment", sv.ColumnHeader.DefaultStyle.HorizontalAlignment.ToString());
                if (sv.ColumnHeader.DefaultStyle.Font != null)
                {
                    ColumnHeaderDefaultNode.SetAttribute("FontName", sv.ColumnHeader.DefaultStyle.Font.FontFamily.Name);
                    ColumnHeaderDefaultNode.SetAttribute("FontSize", sv.ColumnHeader.DefaultStyle.Font.Size.ToString());
                    ColumnHeaderDefaultNode.SetAttribute("FontStyle", sv.ColumnHeader.DefaultStyle.Font.Style.ToString());
                }


                System.Xml.XmlElement ColumnNode = this.GetXmlElement(XmlDocument, sheetNode, "Column");
                ColumnNode.SetAttribute("Count", sv.ColumnCount.ToString());

                System.Xml.XmlElement ColumnHeaderCellNode = this.GetXmlElement(XmlDocument, sheetNode, "ColumnHeaderCell");
                for (int colIndex = 0; colIndex < sv.ColumnCount; colIndex++)
                {
                    System.Xml.XmlElement cNode = this.GetXmlElement(XmlDocument, ColumnNode, "Column" + colIndex.ToString());
                    cNode.SetAttribute("Index", colIndex.ToString());
                    cNode.SetAttribute("Width", sv.Columns[colIndex].Width.ToString());
                    cNode.SetAttribute("Visible", (sv.Columns[colIndex].Visible ? "True" : "False"));

                    cNode.SetAttribute("HorizontalAlignment", sv.Columns[colIndex].HorizontalAlignment.ToString());
                    cNode.SetAttribute("VerticalAlignment", sv.Columns[colIndex].VerticalAlignment.ToString());

                    if (sv.Columns[colIndex].Font != null)
                    {
                        cNode.SetAttribute("FontName", sv.Columns[colIndex].Font.FontFamily.Name);
                        cNode.SetAttribute("FontSize", sv.Columns[colIndex].Font.Size.ToString());
                        cNode.SetAttribute("FontStyle", sv.Columns[colIndex].Font.Style.ToString());
                    }

                    bool readOnly = true;
                    if (sv.Columns[colIndex].CellType is FarPoint.Win.Spread.CellType.TextCellType)
                    {
                        readOnly = ((FarPoint.Win.Spread.CellType.TextCellType)sv.Columns[colIndex].CellType).ReadOnly;
                        cNode.SetAttribute("CellType", "TextCellType");
                        cNode.SetAttribute("WordWrap", ((FarPoint.Win.Spread.CellType.TextCellType)sv.Columns[colIndex].CellType).WordWrap ? "True" : "False");
                    }

                    if (sv.Columns[colIndex].CellType is FarPoint.Win.Spread.CellType.CheckBoxCellType)
                    {
                        cNode.SetAttribute("CellType", "CheckBoxCellType");
                    }

                    if (sv.Columns[colIndex].CellType is FarPoint.Win.Spread.CellType.NumberCellType)
                    {
                        cNode.SetAttribute("CellType", "NumberCellType");
                        FarPoint.Win.Spread.CellType.NumberCellType n = sv.Columns[colIndex].CellType as FarPoint.Win.Spread.CellType.NumberCellType;
                        readOnly = n.ReadOnly;
                        cNode.SetAttribute("DecimalPlaces", n.DecimalPlaces.ToString());

                    }
                    if (sv.Columns[colIndex].CellType is FarPoint.Win.Spread.CellType.CurrencyCellType)
                    {
                        cNode.SetAttribute("CellType", "CurrencyCellType");
                        FarPoint.Win.Spread.CellType.CurrencyCellType c = sv.Columns[colIndex].CellType as FarPoint.Win.Spread.CellType.CurrencyCellType;
                        readOnly = c.ReadOnly;
                        cNode.SetAttribute("DecimalPlaces", c.DecimalPlaces.ToString());
                    }

                    cNode.SetAttribute("ReadOnly", (readOnly ? "True" : "False"));
                    cNode.SetAttribute("AllowAutoSort", (sv.Columns[colIndex].AllowAutoSort ? "True" : "False"));


                    for (int rowIndex = 0; rowIndex < sv.ColumnHeader.RowCount; rowIndex++)
                    {
                        System.Xml.XmlElement CellNode = this.GetXmlElement(XmlDocument, ColumnHeaderCellNode, "Row" + rowIndex.ToString() + "Col" + colIndex.ToString());
                        //保证原始的列头名称和数据源一致，这样才能保证程序操作farpoint的cell和自定义列名称无关
                        //启用别名显示到Farpoint列不影响取值赋值
                        string text = "";
                        string textAS = "";
                        if (sv.DataSource is System.Data.DataSet)
                        {
                            text = ((System.Data.DataSet)sv.DataSource).Tables[0].Columns[colIndex].ColumnName;
                        }
                        else if (sv.DataSource is System.Data.DataTable)
                        {
                            text = ((System.Data.DataTable)sv.DataSource).Columns[colIndex].ColumnName;
                        }
                        else if (sv.DataSource is System.Data.DataView)
                        {
                            text = ((System.Data.DataView)sv.DataSource).Table.Columns[colIndex].ColumnName;
                        }

                        //没有数据源时获取fp列头名称，这个可能是文件被更改，读取到更改后的文件内容后被删除，如此原始的列头名称则可能找不到，这个需要重置
                        if (string.IsNullOrEmpty(sv.ColumnHeader.Cells[rowIndex, colIndex].Text) && sv.ColumnHeader.RowCount == 1)
                        {
                            if (isFirstCreate && text == "")
                            {
                                text = sv.Columns[colIndex].Label;                                
                            }
                            textAS = sv.Columns[colIndex].Label;
                        }
                        else
                        {
                            //保留原始的列名称,防止自定义列名称覆盖后程序找不到原始列
                            if (isFirstCreate && text == "")
                            {
                                text = sv.ColumnHeader.Cells[rowIndex, colIndex].Text;
                            }
                            textAS = sv.ColumnHeader.Cells[rowIndex, colIndex].Text;
                        }

                        CellNode.SetAttribute("Text", text);
                        CellNode.SetAttribute("TextAS", textAS);

                        CellNode.SetAttribute("RowIndex", rowIndex.ToString());
                        CellNode.SetAttribute("ColumnIndex", colIndex.ToString());

                        CellNode.SetAttribute("RowSpan", sv.ColumnHeader.Cells[rowIndex, colIndex].RowSpan.ToString());
                        CellNode.SetAttribute("ColumnSpan", sv.ColumnHeader.Cells[rowIndex, colIndex].ColumnSpan.ToString());
                    }
                }
                #endregion

                //sheet索引，指示程序处理到了哪个sheet
                sheetIndex++;
            }

            //保存到xml文件
            XmlDocument.Save(fileName);

            return true;

            #region 旧的作废

            /*
             * 
            if (!System.IO.File.Exists(fileName))
            {
                SOC.Public.XML.File.CreateXmlFile(fileName, "1.0", "Spread");
            }

            //sheet索引，指示程序处理到了哪个sheet
            int sheetIndex = 1;

            System.Xml.XmlDocument XmlDocument = new System.Xml.XmlDocument();

            //加载xml文件
            XmlDocument.Load(fileName);

            //获取Spread根节点
            System.Xml.XmlElement spreadNode = XmlDocument.DocumentElement;

            //获取sheet的节点
            string sheetName = "sheet";
            if (string.IsNullOrEmpty(sheetName))
            {
                sheetName = "SheetName" + sheetIndex.ToString();
            }

            System.Xml.XmlElement sheetNode = this.GetXmlElement(XmlDocument, spreadNode, sheetName);
            sheetNode.SetAttribute("Locked", (sheet.DefaultStyle.Locked ? "True" : "False"));
            sheetNode.SetAttribute("HorizontalAlignment", sheet.DefaultStyle.HorizontalAlignment.ToString());
            sheetNode.SetAttribute("VerticalAlignment", sheet.DefaultStyle.VerticalAlignment.ToString());

            #region 行头设置

            System.Xml.XmlElement RowHeaderNode = this.GetXmlElement(XmlDocument, sheetNode, "RowHeader");

            RowHeaderNode.SetAttribute("Visible", (sheet.RowHeader.Visible ? "True" : "False"));
            if (sheet.RowHeader.DefaultStyle.Font != null)
            {
                RowHeaderNode.SetAttribute("FontName", sheet.RowHeader.DefaultStyle.Font.FontFamily.Name);
                RowHeaderNode.SetAttribute("FontSize", sheet.RowHeader.DefaultStyle.Font.Size.ToString());
                RowHeaderNode.SetAttribute("FontStyle", sheet.RowHeader.DefaultStyle.Font.Style.ToString());
            }

            #endregion
            System.Xml.XmlElement RowDefaultNode = this.GetXmlElement(XmlDocument, sheetNode, "RowsDefault");
            RowDefaultNode.SetAttribute("Height", sheet.Rows.Default.Height.ToString());
            RowDefaultNode.SetAttribute("VerticalAlignment", sheet.Rows.Default.VerticalAlignment.ToString());
            RowDefaultNode.SetAttribute("HorizontalAlignment", sheet.Rows.Default.HorizontalAlignment.ToString());

            #region 列、列头设置
            System.Xml.XmlElement ColumnHeaderRowsNode = this.GetXmlElement(XmlDocument, sheetNode, "ColumnHeaderRows");
            ColumnHeaderRowsNode.SetAttribute("Count", sheet.ColumnHeader.RowCount.ToString());
            ColumnHeaderRowsNode.SetAttribute("Height", sheet.ColumnHeader.Rows.Default.Height.ToString());

            System.Xml.XmlElement ColumnHeaderDefaultNode = this.GetXmlElement(XmlDocument, sheetNode, "ColumnHeaderDefault");
            ColumnHeaderDefaultNode.SetAttribute("VerticalAlignment", sheet.ColumnHeader.DefaultStyle.VerticalAlignment.ToString());
            ColumnHeaderDefaultNode.SetAttribute("HorizontalAlignment", sheet.ColumnHeader.DefaultStyle.HorizontalAlignment.ToString());
            if (sheet.ColumnHeader.DefaultStyle.Font != null)
            {
                ColumnHeaderDefaultNode.SetAttribute("FontName", sheet.ColumnHeader.DefaultStyle.Font.FontFamily.Name);
                ColumnHeaderDefaultNode.SetAttribute("FontSize", sheet.ColumnHeader.DefaultStyle.Font.Size.ToString());
                ColumnHeaderDefaultNode.SetAttribute("FontStyle", sheet.ColumnHeader.DefaultStyle.Font.Style.ToString());
            }


            System.Xml.XmlElement ColumnNode = this.GetXmlElement(XmlDocument, sheetNode, "Column");
            ColumnNode.SetAttribute("Count", sheet.ColumnCount.ToString());

            System.Xml.XmlElement ColumnHeaderCellNode = this.GetXmlElement(XmlDocument, sheetNode, "ColumnHeaderCell");
            for (int colIndex = 0; colIndex < sheet.ColumnCount; colIndex++)
            {
                System.Xml.XmlElement cNode = this.GetXmlElement(XmlDocument, ColumnNode, "Column" + colIndex.ToString());
                cNode.SetAttribute("Index", colIndex.ToString());
                cNode.SetAttribute("Width", sheet.Columns[colIndex].Width.ToString());
                cNode.SetAttribute("Visible", (sheet.Columns[colIndex].Visible ? "True" : "False"));
                cNode.SetAttribute("sort", (sheet.Columns[colIndex].Visible ? "True" : "False"));
                cNode.SetAttribute("ZoomFactor", sheet.ZoomFactor.ToString());
                cNode.SetAttribute("HorizontalAlignment", sheet.Columns[colIndex].HorizontalAlignment.ToString());
                cNode.SetAttribute("VerticalAlignment", sheet.Columns[colIndex].VerticalAlignment.ToString());

                if (sheet.Columns[colIndex].Font != null)
                {
                    cNode.SetAttribute("FontName", sheet.Columns[colIndex].Font.FontFamily.Name);
                    cNode.SetAttribute("FontSize", sheet.Columns[colIndex].Font.Size.ToString());
                    cNode.SetAttribute("FontStyle", sheet.Columns[colIndex].Font.Style.ToString());
                }

                bool readOnly = true;
                if (sheet.Columns[colIndex].CellType is FarPoint.Win.Spread.CellType.TextCellType)
                {
                    readOnly = ((FarPoint.Win.Spread.CellType.TextCellType)sheet.Columns[colIndex].CellType).ReadOnly;
                    cNode.SetAttribute("CellType", "TextCellType");
                }

                if (sheet.Columns[colIndex].CellType is FarPoint.Win.Spread.CellType.CheckBoxCellType)
                {
                    cNode.SetAttribute("CellType", "CheckBoxCellType");
                }

                if (sheet.Columns[colIndex].CellType is FarPoint.Win.Spread.CellType.NumberCellType)
                {
                    cNode.SetAttribute("CellType", "NumberCellType");
                    FarPoint.Win.Spread.CellType.NumberCellType n = sheet.Columns[colIndex].CellType as FarPoint.Win.Spread.CellType.NumberCellType;
                    readOnly = n.ReadOnly;
                    cNode.SetAttribute("DecimalPlaces", n.DecimalPlaces.ToString());

                }
                if (sheet.Columns[colIndex].CellType is FarPoint.Win.Spread.CellType.CurrencyCellType)
                {
                    cNode.SetAttribute("CellType", "CurrencyCellType");
                    FarPoint.Win.Spread.CellType.CurrencyCellType c = sheet.Columns[colIndex].CellType as FarPoint.Win.Spread.CellType.CurrencyCellType;
                    readOnly = c.ReadOnly;
                    cNode.SetAttribute("DecimalPlaces", c.DecimalPlaces.ToString());
                }

                cNode.SetAttribute("ReadOnly", (readOnly ? "True" : "False"));


                for (int rowIndex = 0; rowIndex < sheet.ColumnHeader.RowCount; rowIndex++)
                {
                    System.Xml.XmlElement CellNode = this.GetXmlElement(XmlDocument, ColumnHeaderCellNode, "Row" + rowIndex.ToString() + "Col" + colIndex.ToString());
                    if (string.IsNullOrEmpty(sheet.ColumnHeader.Cells[rowIndex, colIndex].Text) && sheet.ColumnHeader.RowCount == 1)
                    {
                        CellNode.SetAttribute("Text", sheet.Columns[colIndex].Label);
                    }
                    else
                    {
                        CellNode.SetAttribute("Text", sheet.ColumnHeader.Cells[rowIndex, colIndex].Text);
                    }

                    CellNode.SetAttribute("RowIndex", rowIndex.ToString());
                    CellNode.SetAttribute("ColumnIndex", colIndex.ToString());

                    CellNode.SetAttribute("RowSpan", sheet.ColumnHeader.Cells[rowIndex, colIndex].RowSpan.ToString());
                    CellNode.SetAttribute("ColumnSpan", sheet.ColumnHeader.Cells[rowIndex, colIndex].ColumnSpan.ToString());
                }
            }
            #endregion

            //保存到xml文件
            XmlDocument.Save(fileName);

            return true;
            */
            #endregion
        }

        /// <summary>
        /// 设置单元格格式
        /// </summary>
        /// <param name="sv"></param>
        /// <param name="XmlDocument"></param>
        /// <param name="parentElement"></param>
        /// <param name="columnCount"></param>
        /// <param name="isUserUnMainSet">是否处理其他配置：行高、cellType等，因为cellType不能保存完全设置</param>
        /// <returns></returns>
        private bool SetColumn(FarPoint.Win.Spread.SheetView sv, System.Xml.XmlDocument XmlDocument, System.Xml.XmlElement parentElement, int columnCount, bool isUserUnMainSet)
        {
            System.Xml.XmlNode XmlNode = parentElement.SelectSingleNode("Column");
            System.Xml.XmlNodeList XmlNodeList = XmlNode.ChildNodes;
            if (XmlNodeList.Count != columnCount)
            {
                throw new Exception("配置文件已经损坏：列的节点数不等于Colums的Count值");
            }

            for (int index = 0; index < XmlNodeList.Count; index++)
            {
                System.Xml.XmlNode xmlNode = XmlNodeList.Item(index);
                if (xmlNode != null)
                {
                    System.Xml.XmlElement e = (System.Xml.XmlElement)xmlNode;
                    if (!e.HasAttribute("Index"))
                    {
                        throw new Exception("配置文件已经损坏：Column没有Index");
                    }

                    string strIndex = e.Attributes["Index"].Value;

                    if ("Column" + strIndex != xmlNode.Name)
                    {
                        throw new Exception("配置文件已经损坏：Column.Index指示的列和节点名称不一致");
                    }
                    int outValue = 0;
                    if (!int.TryParse(strIndex, out outValue))
                    {
                        throw new Exception("配置文件已经损坏：Column.Index指示的列索引不是整数");
                    }
                    if (outValue < 0)
                    {
                        throw new Exception("配置文件已经损坏：Column.Index指示的列索引必须大于等于0");
                    }
                    this.SetValue(sv.Columns[outValue], XmlDocument, (System.Xml.XmlElement)XmlNode, xmlNode.Name, "Width");
                    this.SetValue(sv.Columns[outValue], XmlDocument, (System.Xml.XmlElement)XmlNode, xmlNode.Name, "Visible");

                    this.SetValue(sv.Columns[outValue], XmlDocument, (System.Xml.XmlElement)XmlNode, xmlNode.Name, "HorizontalAlignment");
                    this.SetValue(sv.Columns[outValue], XmlDocument, (System.Xml.XmlElement)XmlNode, xmlNode.Name, "VerticalAlignment");
                    this.SetValue(sv.Columns[outValue], XmlDocument, (System.Xml.XmlElement)XmlNode, xmlNode.Name, "CellType");
                        sv.Columns[outValue].CellType = this.GetCellType(sv.Columns[outValue].CellType, XmlDocument, (System.Xml.XmlElement)XmlNode, xmlNode.Name, sv.Columns[outValue].CellType);

                    this.SetValue(sv.Columns[outValue], XmlDocument, (System.Xml.XmlElement)XmlNode, xmlNode.Name, "AllowAutoSort");
                    sv.Columns[outValue].Font = this.GetFont(XmlDocument, (System.Xml.XmlElement)XmlNode, xmlNode.Name, sv.Columns[outValue].Font);
                }
            }
            return true;
        }

        /// <summary>
        /// 读取架构
        /// </summary>
        /// <param name="fileName">xml文件</param>
        /// <returns></returns>
        public virtual bool ReadSchema(string fileName)
        {
            return ReadSchema(null, fileName, true);
        }

        /// <summary>
        /// 格式化单个sheet
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="fileName">配置文件名称</param>
        /// <param name="isUserUnMainSet">是否处理其他配置：行高、cellType等，因为cellType不能保存完全设置</param>
        /// <returns></returns>
        public virtual bool ReadSchema(FarPoint.Win.Spread.SheetView sheet, string fileName,bool isUserUnMainSet)
        {
            this.SetFileName(fileName);

            if (!System.IO.File.Exists(fileName))
            {
                return false;
            }
            //sheet索引，指示程序处理到了哪个sheet
            int sheetIndex = 1;
            System.Xml.XmlDocument XmlDocument = new System.Xml.XmlDocument();

            //加载xml文件
            XmlDocument.Load(fileName);

            //获取Spread根节点
            System.Xml.XmlElement spreadNode = XmlDocument.DocumentElement;

            string sheetName="";

            foreach (FarPoint.Win.Spread.SheetView sv in this.Sheets)
            {
                if (sheet != null && sheet != sv)
                {
                    continue;
                }

                //sheet的节点
                if (sheet == null)
                {
                    sheetName = sv.SheetName;
                }
                else
                {
                    sheetName = "NormalSheet";
                }

                if (string.IsNullOrEmpty(sheetName))
                {
                    sheetName = "SheetName" + sheetIndex.ToString();
                }

                this.ResortDataColumns();

                this.SetValue(sv.DefaultStyle, XmlDocument, spreadNode, sheetName, "Locked");
                this.SetValue(sv.DefaultStyle, XmlDocument, spreadNode, sheetName, "HorizontalAlignment");
                this.SetValue(sv.DefaultStyle, XmlDocument, spreadNode, sheetName, "VerticalAlignment");
                this.SetValue(sv, XmlDocument, spreadNode, sheetName, "ZoomFactor");

                //深一级
                System.Xml.XmlNode sheetNode = spreadNode.SelectSingleNode(sheetName);

                if (sheetNode != null)
                {
                    this.SetValue(sv.RowHeader, XmlDocument, (System.Xml.XmlElement)sheetNode, "RowHeader", "Visible");
                    sv.RowHeader.DefaultStyle.Font = this.GetFont(XmlDocument, (System.Xml.XmlElement)sheetNode, "RowHeader", sv.RowHeader.DefaultStyle.Font);

                    if (isUserUnMainSet)
                    {
                        this.SetValue(sv.Rows.Default, XmlDocument, (System.Xml.XmlElement)sheetNode, "RowsDefault", "Height");
                        this.SetValue(sv.ColumnHeader.Rows.Default, XmlDocument, (System.Xml.XmlElement)sheetNode, "ColumnHeaderRows", "Height");
                    }
                    this.SetValue(sv.Rows.Default, XmlDocument, (System.Xml.XmlElement)sheetNode, "RowsDefault", "VerticalAlignment");
                    this.SetValue(sv.Rows.Default, XmlDocument, (System.Xml.XmlElement)sheetNode, "RowsDefault", "HorizontalAlignment");

                    this.SetValue(sv.ColumnHeader.Rows, XmlDocument, (System.Xml.XmlElement)sheetNode, "ColumnHeaderRows", "Count");

                    this.SetValue(sv.ColumnHeader.DefaultStyle, XmlDocument, (System.Xml.XmlElement)sheetNode, "ColumnHeaderDefault", "VerticalAlignment");
                    this.SetValue(sv.ColumnHeader.DefaultStyle, XmlDocument, (System.Xml.XmlElement)sheetNode, "ColumnHeaderDefault", "HorizontalAlignment");
                    sv.ColumnHeader.DefaultStyle.Font = this.GetFont(XmlDocument, (System.Xml.XmlElement)sheetNode, "ColumnHeaderDefault", sv.ColumnHeader.DefaultStyle.Font);

                    this.SetValue(sv.Columns, XmlDocument, (System.Xml.XmlElement)sheetNode, "Column", "Count");

                    this.SetColumnHeaderCell(sv, XmlDocument, (System.Xml.XmlElement)sheetNode, sv.ColumnCount * sv.ColumnHeader.RowCount);


                    //this.SetColumn(sv, XmlDocument, (System.Xml.XmlElement)sheetNode, sv.ColumnCount);
                    this.SetColumn(sv, XmlDocument, (System.Xml.XmlElement)sheetNode, sv.ColumnCount, isUserUnMainSet);
                }

                //sheet索引，指示程序处理到了哪个sheet
                sheetIndex++;
            }


            return true;


            #region 旧的作废
            /*
            if (!System.IO.File.Exists(fileName))
            {
                return false;
            }

            System.Xml.XmlDocument XmlDocument = new System.Xml.XmlDocument();

            //加载xml文件
            XmlDocument.Load(fileName);

            //获取Spread根节点
            System.Xml.XmlElement spreadNode = XmlDocument.DocumentElement;

            this.SetValue(sheet.DefaultStyle, XmlDocument, spreadNode, "sheet", "Locked");
            this.SetValue(sheet.DefaultStyle, XmlDocument, spreadNode, "sheet", "HorizontalAlignment");
            this.SetValue(sheet.DefaultStyle, XmlDocument, spreadNode, "sheet", "VerticalAlignment");

            //深一级
            System.Xml.XmlNode sheetNode = spreadNode.SelectSingleNode("sheet");

            if (sheetNode != null)
            {
                this.SetValue(sheet.RowHeader, XmlDocument, (System.Xml.XmlElement)sheetNode, "RowHeader", "Visible");
                sheet.RowHeader.DefaultStyle.Font = this.GetFont(XmlDocument, (System.Xml.XmlElement)sheetNode, "RowHeader", sheet.RowHeader.DefaultStyle.Font);

                if (isUserUnMainSet)
                {
                    this.SetValue(sheet.Rows.Default, XmlDocument, (System.Xml.XmlElement)sheetNode, "RowsDefault", "Height");
                }
                this.SetValue(sheet.Rows.Default, XmlDocument, (System.Xml.XmlElement)sheetNode, "RowsDefault", "VerticalAlignment");
                this.SetValue(sheet.Rows.Default, XmlDocument, (System.Xml.XmlElement)sheetNode, "RowsDefault", "HorizontalAlignment");

                this.SetValue(sheet.ColumnHeader.Rows, XmlDocument, (System.Xml.XmlElement)sheetNode, "ColumnHeaderRows", "Count");
                this.SetValue(sheet.ColumnHeader.Rows.Default, XmlDocument, (System.Xml.XmlElement)sheetNode, "ColumnHeaderRows", "Height");

                this.SetValue(sheet.ColumnHeader.DefaultStyle, XmlDocument, (System.Xml.XmlElement)sheetNode, "ColumnHeaderDefault", "VerticalAlignment");
                this.SetValue(sheet.ColumnHeader.DefaultStyle, XmlDocument, (System.Xml.XmlElement)sheetNode, "ColumnHeaderDefault", "HorizontalAlignment");
                sheet.ColumnHeader.DefaultStyle.Font = this.GetFont(XmlDocument, (System.Xml.XmlElement)sheetNode, "ColumnHeaderDefault", sheet.ColumnHeader.DefaultStyle.Font);

                this.SetValue(sheet.Columns, XmlDocument, (System.Xml.XmlElement)sheetNode, "Column", "Count");

                this.SetColumnHeaderCell(sheet, XmlDocument, (System.Xml.XmlElement)sheetNode, sheet.ColumnCount * sheet.ColumnHeader.RowCount);


                this.SetColumn(sheet, XmlDocument, (System.Xml.XmlElement)sheetNode, sheet.ColumnCount, isUserUnMainSet);

            }
            return true;
            */
            #endregion
        }

        /// <summary>
        /// 根据列名称设置cell的值
        /// </summary>
        /// <param name="sheetIndex">表索引</param>
        /// <param name="columnName">列名称</param>
        /// <param name="rowIndex">行索引</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public virtual bool SetCellValue(int sheetIndex, int rowIndex, string columnName, object value)
        {
            if (this.Sheets.Count <= sheetIndex)
            {
                throw new Exception("无效的sheet索引" + sheetIndex + "应介于0和" + (this.Sheets.Count - 1).ToString() + "之间");
            }
            if (this.Sheets[sheetIndex].RowCount <= rowIndex)
            {
                throw new Exception("无效的Row索引" + rowIndex + "应介于0和" + (this.Sheets[sheetIndex].RowCount - 1).ToString() + "之间");
            }
            if (hsColumnASNameCompare.Contains(columnName))
            {
                columnName = hsColumnASNameCompare[columnName].ToString();
            }
            for (int columnIndex = 0; columnIndex < this.Sheets[sheetIndex].ColumnCount; columnIndex++)
            {
                if (this.Sheets[sheetIndex].ColumnHeader.Cells[0, columnIndex].Text == columnName || this.Sheets[sheetIndex].Columns[columnIndex].Label == columnName)
                {
                    this.Sheets[sheetIndex].Cells[rowIndex, columnIndex].Value = value;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 根据列名称获取cell的值
        /// </summary>
        /// <param name="sheetIndex">表索引</param>
        /// <param name="columnName">列名称</param>
        /// <param name="rowIndex">行索引</param>
        /// <returns></returns>
        public virtual string GetCellText(int sheetIndex, int rowIndex, string columnName)
        {
            if (this.Sheets.Count <= sheetIndex)
            {
                throw new Exception("无效的sheet索引" + sheetIndex + "应介于0和" + (this.Sheets.Count - 1).ToString() + "之间");
            }
            if (this.Sheets[sheetIndex].RowCount <= rowIndex)
            {
                throw new Exception("无效的Row索引" + rowIndex + "应介于0和" + (this.Sheets[sheetIndex].RowCount - 1).ToString() + "之间");
            }
            if (hsColumnASNameCompare.Contains(columnName))
            {
                columnName = hsColumnASNameCompare[columnName].ToString();
            }
            for (int columnIndex = 0; columnIndex < this.Sheets[sheetIndex].ColumnCount; columnIndex++)
            {
                if (this.Sheets[sheetIndex].ColumnHeader.Cells[0, columnIndex].Text == columnName || this.Sheets[sheetIndex].Columns[columnIndex].Label == columnName)
                {
                    if (this.Sheets[sheetIndex].Cells[rowIndex, columnIndex].Value != null)
                    {
                        return this.Sheets[sheetIndex].Cells[rowIndex, columnIndex].Value.ToString();
                    }
                    return this.Sheets[sheetIndex].Cells[rowIndex, columnIndex].Text;
                }
            }

            return null;
        }

        /// <summary>
        /// 根据列名称获取列索引的值
        /// </summary>
        /// <param name="sheetIndex">表索引</param>
        /// <param name="columnName">列名称</param>
        /// <returns></returns>
        public virtual int GetColumnIndex(int sheetIndex, string columnName)
        {
            if (this.Sheets.Count <= sheetIndex)
            {
                throw new Exception("无效的sheet索引" + sheetIndex + "应介于0和" + (this.Sheets.Count - 1).ToString() + "之间");
            }
            if (hsColumnASNameCompare.Contains(columnName))
            {
                columnName = hsColumnASNameCompare[columnName].ToString();
            }
            for (int columnIndex = 0; columnIndex < this.Sheets[sheetIndex].ColumnCount; columnIndex++)
            {
                if (this.Sheets[sheetIndex].ColumnHeader.Cells[0, columnIndex].Text == columnName || this.Sheets[sheetIndex].Columns[columnIndex].Label == columnName)
                {
                    return columnIndex;
                }
            }

            return -1;
        }

        /// <summary>
        /// 根据列名称设置FarPoint的列宽
        /// </summary>
        /// <param name="sheetIndex">表索引</param>
        /// <param name="columnName">列名称</param>
        /// <param name="with">列宽</param>
        /// <returns></returns>
        public virtual bool SetColumnWith(int sheetIndex, string columnName, float with)
        {

            if (this.Sheets.Count <= sheetIndex)
            {
                throw new Exception("无效的sheet索引" + sheetIndex + "应介于0和" + (this.Sheets.Count - 1).ToString() + "之间");
            }
            if (hsColumnASNameCompare.Contains(columnName))
            {
                columnName = hsColumnASNameCompare[columnName].ToString();
            }
            for (int columnIndex = 0; columnIndex < this.Sheets[sheetIndex].ColumnCount; columnIndex++)
            {
                if (this.Sheets[sheetIndex].ColumnHeader.Cells[0, columnIndex].Text == columnName || this.Sheets[sheetIndex].Columns[columnIndex].Label == columnName)
                {
                    this.Sheets[sheetIndex].Columns[columnIndex].Width = with;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 根据列名称设置FarPoint的列CellType
        /// </summary>
        /// <param name="sheetIndex">表索引</param>
        /// <param name="columnName">列名称</param>
        /// <param name="cellType">列宽</param>
        /// <returns></returns>
        public virtual bool SetColumnCellType(int sheetIndex, string columnName, FarPoint.Win.Spread.CellType.BaseCellType cellType)
        {

            if (this.Sheets.Count <= sheetIndex)
            {
                throw new Exception("无效的sheet索引" + sheetIndex + "应介于0和" + (this.Sheets.Count - 1).ToString() + "之间");
            }

            for (int columnIndex = 0; columnIndex < this.Sheets[sheetIndex].ColumnCount; columnIndex++)
            {
                if (this.Sheets[sheetIndex].ColumnHeader.Cells[0, columnIndex].Text == columnName || this.Sheets[sheetIndex].Columns[columnIndex].Label == columnName)
                {
                    this.Sheets[sheetIndex].Columns[columnIndex].CellType = cellType;
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region 私有方法
        private string ReadXmlElementAttribute(System.Xml.XmlDocument XmlDocument, System.Xml.XmlElement parentElement, string elementName, string attributeName)
        {
            System.Xml.XmlNode selectNode = parentElement.SelectSingleNode(elementName);
            if (selectNode == null)
            {
                return "";
            }
            System.Xml.XmlElement e = (System.Xml.XmlElement)selectNode;
            if (e.HasAttribute(attributeName))
            {
                return (e.Attributes[attributeName]).Value;
            }

            return "";
        }

        private System.Xml.XmlElement GetXmlElement(System.Xml.XmlDocument XmlDocument, System.Xml.XmlElement parentElement, string elementName)
        {
            System.Xml.XmlNode selectNode = parentElement.SelectSingleNode(elementName);
            if (selectNode == null)
            {
                //sheet节点建立
                System.Xml.XmlElement XmlElement = XmlDocument.CreateElement(elementName);
                parentElement.AppendChild(XmlElement);

                selectNode = parentElement.SelectSingleNode(elementName);
            }

            return (System.Xml.XmlElement)selectNode;
        }

        private bool SetValue(object propertyCompoent, System.Xml.XmlDocument XmlDocument, System.Xml.XmlElement parentElement, string elementName, string attributeName)
        {
            if (propertyCompoent == null)
            {
                return false;
            }

            //从配置文件里读取属性的值
            string strXMLValue = ReadXmlElementAttribute(XmlDocument, parentElement, elementName, attributeName);

            if (!string.IsNullOrEmpty(strXMLValue))
            {
                //抽象化当前属性
                PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(propertyCompoent)[attributeName];

                //字符串表示的属性值转换成属性相对应的类型
                try
                {
                    object value = propertyDescriptor.Converter.ConvertFromInvariantString(strXMLValue);

                    //设置属性的值
                    propertyDescriptor.SetValue(propertyCompoent, value);
                }
                catch
                {
                    throw new Exception("配置文件已经损坏：" + attributeName + "属性不正确");
                }
            }
            return true;
        }

        private Font GetFont(System.Xml.XmlDocument XmlDocument, System.Xml.XmlElement parentElement, string elementName, Font defaultFont)
        {
            //字体的设置相当麻烦，多数情况下FarPoint的字体都是null，而字体的属性一般都是只读的
            Font font = new Font("宋体", 9f, FontStyle.Regular);
            if (defaultFont != null)
            {
                font = new Font(defaultFont.Name, defaultFont.Size, defaultFont.Style);
            }

            //从配置文件里读取属性的值
            string strXMLValue1 = ReadXmlElementAttribute(XmlDocument, parentElement, elementName, "FontName");
            string strXMLValue2 = ReadXmlElementAttribute(XmlDocument, parentElement, elementName, "FontSize");
            string strXMLValue3 = ReadXmlElementAttribute(XmlDocument, parentElement, elementName, "FontStyle");

            if (string.IsNullOrEmpty(strXMLValue1) && string.IsNullOrEmpty(strXMLValue2) && string.IsNullOrEmpty(strXMLValue3))
            {
                return defaultFont;
            }

            //抽象化当前font的属性
            PropertyDescriptor propertyDescriptor1 = TypeDescriptor.GetProperties(font)["Name"];
            PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(font)["Size"];
            PropertyDescriptor propertyDescriptor3 = TypeDescriptor.GetProperties(font)["Style"];

            object value1 = font.FontFamily.Name;
            object value2 = font.Size;
            object value3 = font.Style;
            if (!string.IsNullOrEmpty(strXMLValue1))
            {
                //字符串表示属性值转换成sheet的属性相对应的类型
                try
                {
                    value1 = propertyDescriptor1.Converter.ConvertFromInvariantString(strXMLValue1);
                }
                catch
                {
                    throw new Exception("配置文件已经损坏：字体名称设置不正确");
                }
            }
            if (!string.IsNullOrEmpty(strXMLValue2))
            {
                //字符串表示属性值转换成sheet的属性相对应的类型
                try
                {
                    value2 = propertyDescriptor2.Converter.ConvertFromInvariantString(strXMLValue2);
                }
                catch
                {
                    throw new Exception("配置文件已经损坏：字体大小设置不正确");
                }
            }

            if (!string.IsNullOrEmpty(strXMLValue3))
            {
                //字符串表示属性值转换成sheet的属性相对应的类型
                try
                {
                    value3 = propertyDescriptor3.Converter.ConvertFromInvariantString(strXMLValue3);
                }
                catch
                {
                    throw new Exception("配置文件已经损坏：字体类型设置不正确");
                }
            }

            if (value1 == null)
            {
                value1 = font.FontFamily.Name;
            }
            if (value2 == null)
            {
                value2 = font.Size;
            }
            if (value3 == null)
            {
                value3 = font.Style;
            }

            font = new Font(value1.ToString(), (float)value2, (FontStyle)value3);

            return font;
        }

        private bool SetColumnHeaderCell(FarPoint.Win.Spread.SheetView sv, System.Xml.XmlDocument XmlDocument, System.Xml.XmlElement parentElement, int columnCount)
        {
            System.Xml.XmlNode XmlNode = parentElement.SelectSingleNode("ColumnHeaderCell");
            System.Xml.XmlNodeList XmlNodeList = XmlNode.ChildNodes;
            if (XmlNodeList.Count != columnCount)
            {
                throw new Exception("配置文件已经损坏：ColumnHeader的列数不等于Sheet的列数");
            }
            this.hsColumnASNameCompare.Clear();
            for (int index = 0; index < XmlNodeList.Count; index++)
            {
                System.Xml.XmlNode xmlNode = XmlNodeList.Item(index);
                if (xmlNode != null)
                {
                    System.Xml.XmlElement e = (System.Xml.XmlElement)xmlNode;
                    if (!e.HasAttribute("RowIndex"))
                    {
                        throw new Exception("配置文件已经损坏：ColumnHeader的Cell没有RowIndex");
                    }
                    string rowIndex = e.Attributes["RowIndex"].Value;
                    int outValue1 = 0;
                    if (!int.TryParse(rowIndex, out outValue1))
                    {
                        throw new Exception("配置文件已经损坏：ColumnHeader.Cell.RowIndex不是整数");
                    }
                    if (outValue1 < 0)
                    {
                        throw new Exception("配置文件已经损坏：ColumnHeader.Cell.RowIndex表示的索引必须大于等于0");
                    }
                    if (!e.HasAttribute("ColumnIndex"))
                    {
                        throw new Exception("配置文件已经损坏：ColumnHeader的Cell没有ColumnIndex");
                    }
                    string columnIndex = e.Attributes["ColumnIndex"].Value;
                    if (!int.TryParse(columnIndex, out outValue1))
                    {
                        throw new Exception("配置文件已经损坏：ColumnHeader.Cell.ColumnIndex不是整数");
                    }
                    if (outValue1 < 0)
                    {
                        throw new Exception("配置文件已经损坏：ColumnHeader.Cell.ColumnIndex表示的索引必须大于等于0");
                    }
                    if ("Row" + rowIndex + "Col" + columnIndex != xmlNode.Name)
                    {
                        throw new Exception("配置文件已经损坏：ColumnHeader.Cell.RowIndex和ColumnIndex指示的Cell和节点名称不一致");
                    }
                    if (sv.ColumnHeader.RowCount > int.Parse(rowIndex) && sv.ColumnHeader.Columns.Count > int.Parse(columnIndex))
                    {
                        string text = e.Attributes["Text"].Value;
                        string textAS = "";
                        if (e.Attributes.GetNamedItem("TextAS") != null)
                        {
                            textAS = e.Attributes["TextAS"].Value; 
                        }
                        if (!string.IsNullOrEmpty(textAS))
                        {
                            sv.ColumnHeader.Cells[int.Parse(rowIndex), int.Parse(columnIndex)].Text = textAS;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(text))
                            {
                                sv.ColumnHeader.Cells[int.Parse(rowIndex), int.Parse(columnIndex)].Text = text;
                            }
                        }
                        if (sv.DataSource is System.Data.DataSet)
                        {
                            if (!((System.Data.DataSet)sv.DataSource).Tables[0].Columns.Contains(text))
                            {
                                throw new Exception("配置文件已经损坏：Text代表的列名称[" + text + "]在数据源中不存在");
                            }
                        }
                        else if (sv.DataSource is System.Data.DataTable)
                        {
                            if (!((System.Data.DataTable)sv.DataSource).Columns.Contains(text))
                            {
                                throw new Exception("配置文件已经损坏：Text代表的列名称[" + text + "]在数据源中不存在");
                            }
                        }
                        else if (sv.DataSource is System.Data.DataView)
                        {
                            if (!((System.Data.DataView)sv.DataSource).Table.Columns.Contains(text))
                            {
                                throw new Exception("配置文件已经损坏：Text代表的列名称[" + text + "]在数据源中不存在");
                            }
                        }
                        if (!hsColumnASNameCompare.Contains(text))
                        {
                            hsColumnASNameCompare.Add(text, textAS);
                        }

                        int outValue2 = 0;
                        if (e.HasAttribute("RowSpan"))
                        {
                            string rowSpan = e.Attributes["RowSpan"].Value;
                            if (!int.TryParse(rowSpan, out outValue2))
                            {
                                throw new Exception("配置文件已经损坏：ColumnHeader.Cell.RowSpan不是整数");
                            }
                            sv.ColumnHeader.Cells[int.Parse(rowIndex), int.Parse(columnIndex)].RowSpan = int.Parse(rowSpan);
                        }

                        if (e.HasAttribute("ColumnSpan"))
                        {
                            string columnSpan = e.Attributes["ColumnSpan"].Value;
                            if (!int.TryParse(columnSpan, out outValue2))
                            {
                                throw new Exception("配置文件已经损坏：ColumnHeader.Cell.ColumnSpan不是整数");
                            }
                            sv.ColumnHeader.Cells[int.Parse(rowIndex), int.Parse(columnIndex)].ColumnSpan = int.Parse(columnSpan);
                        }
                    }
                }
            }
            return true;
        }

        private bool SetColumn(FarPoint.Win.Spread.SheetView sv, System.Xml.XmlDocument XmlDocument, System.Xml.XmlElement parentElement, int columnCount)
        {
            System.Xml.XmlNode XmlNode = parentElement.SelectSingleNode("Column");
            System.Xml.XmlNodeList XmlNodeList = XmlNode.ChildNodes;
            if (XmlNodeList.Count != columnCount)
            {
                throw new Exception("配置文件已经损坏：列的节点数不等于Colums的Count值");
            }

            for (int index = 0; index < XmlNodeList.Count; index++)
            {
                System.Xml.XmlNode xmlNode = XmlNodeList.Item(index);
                if (xmlNode != null)
                {
                    System.Xml.XmlElement e = (System.Xml.XmlElement)xmlNode;
                    if (!e.HasAttribute("Index"))
                    {
                        throw new Exception("配置文件已经损坏：Column没有Index");
                    }

                    string strIndex = e.Attributes["Index"].Value;

                    if ("Column" + strIndex != xmlNode.Name)
                    {
                        throw new Exception("配置文件已经损坏：Column.Index指示的列和节点名称不一致");
                    }
                    int outValue = 0;
                    if (!int.TryParse(strIndex, out outValue))
                    {
                        throw new Exception("配置文件已经损坏：Column.Index指示的列索引不是整数");
                    }
                    if (outValue < 0)
                    {
                        throw new Exception("配置文件已经损坏：Column.Index指示的列索引必须大于等于0");
                    }
                    this.SetValue(sv.Columns[outValue], XmlDocument, (System.Xml.XmlElement)XmlNode, xmlNode.Name, "Width");
                    this.SetValue(sv.Columns[outValue], XmlDocument, (System.Xml.XmlElement)XmlNode, xmlNode.Name, "Visible");
                    this.SetValue(sv.Columns[outValue], XmlDocument, (System.Xml.XmlElement)XmlNode, xmlNode.Name, "HorizontalAlignment");
                    this.SetValue(sv.Columns[outValue], XmlDocument, (System.Xml.XmlElement)XmlNode, xmlNode.Name, "VerticalAlignment");
                    this.SetValue(sv.Columns[outValue], XmlDocument, (System.Xml.XmlElement)XmlNode, xmlNode.Name, "CellType");
                    sv.Columns[outValue].CellType = this.GetCellType(sv.Columns[outValue].CellType, XmlDocument, (System.Xml.XmlElement)XmlNode, xmlNode.Name, sv.Columns[outValue].CellType);
                    this.SetValue(sv.Columns[outValue], XmlDocument, (System.Xml.XmlElement)XmlNode, xmlNode.Name, "AllowAutoSort");
                    sv.Columns[outValue].Font = this.GetFont(XmlDocument, (System.Xml.XmlElement)XmlNode, xmlNode.Name, sv.Columns[outValue].Font);

                }
            }
            return true;
        }

        private FarPoint.Win.Spread.CellType.ICellType GetCellType(FarPoint.Win.Spread.CellType.ICellType cellType, System.Xml.XmlDocument XmlDocument, System.Xml.XmlElement parentElement, string elementName, FarPoint.Win.Spread.CellType.ICellType defaultCellType)
        {
            //从配置文件里读取属性的值
            string strXMLValue1 = ReadXmlElementAttribute(XmlDocument, parentElement, elementName, "CellType");
            string strXMLValue2 = ReadXmlElementAttribute(XmlDocument, parentElement, elementName, "ReadOnly");

            bool outValue1 = true;
            bool readOnly = true;

            if (bool.TryParse(strXMLValue2, out outValue1))
            {
                readOnly = outValue1;
            }

            if (string.IsNullOrEmpty(strXMLValue1))
            {
                return defaultCellType;
            }

            if (strXMLValue1 == "TextCellType")
            {
                FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                t.ReadOnly = readOnly;

                string wordWrap = ReadXmlElementAttribute(XmlDocument, parentElement, elementName, "WordWrap");
                if (bool.TryParse(wordWrap, out outValue1))
                {
                    t.WordWrap = outValue1;
                }

                return t;
            }

            if (strXMLValue1 == "NumberCellType")
            {
                string strDecimalPlaces = ReadXmlElementAttribute(XmlDocument, parentElement, elementName, "DecimalPlaces");
                FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
                int outValue2 = 0;
                if (int.TryParse(strDecimalPlaces, out outValue2))
                {
                    n.DecimalPlaces = outValue2;
                }
                n.ReadOnly = readOnly;
                return n;
            }
            if (strXMLValue1 == "CurrencyCellType")
            {
                string strDecimalPlaces = ReadXmlElementAttribute(XmlDocument, parentElement, elementName, "DecimalPlaces");
                FarPoint.Win.Spread.CellType.CurrencyCellType c = new FarPoint.Win.Spread.CellType.CurrencyCellType();
                int outValue3 = 0;
                if (int.TryParse(strDecimalPlaces, out outValue3))
                {
                    c.DecimalPlaces = outValue3;
                }
                c.ReadOnly = readOnly;
                return c;
            }

            if (strXMLValue1 == "CheckBoxCellType")
            {
                FarPoint.Win.Spread.CellType.CheckBoxCellType c = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                return c;
            }

            return defaultCellType;
        }

        #endregion

        #region 公开的设置皮肤的方法
        public int SetActiveSkin(int sheetIndex, EnumSkinType skinType)
        {
            if (skinType == EnumSkinType.简单一分线)
            {
                return this.SetSampleOneLine(sheetIndex);
            }

            return 0;
        }

        public enum EnumSkinType
        {
            简单一分线,
            自定义
        }

        private int SetSampleOneLine(int sheetIndex)
        {
            //设置FarPoint皮肤
            if (this.Sheets.Count <= sheetIndex)
            {
                throw new Exception("无效的SheetIndex");
            }
            this.Sheets[sheetIndex].ColumnHeader.HorizontalGridLine =
                new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, Color.White, Color.White, Color.Black, -1);
            this.Sheets[sheetIndex].ColumnHeader.VerticalGridLine =
                new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, Color.White, Color.White, Color.White, -1);
            this.Sheets[sheetIndex].HorizontalGridLine =
                new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, Color.White, Color.White, Color.White, -1);
            this.Sheets[sheetIndex].VerticalGridLine =
               new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, Color.White, Color.White, Color.White, -1);
            return 1;
        }
        #endregion


        #region 按键

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.Alt && e.Control && e.KeyCode == Keys.Q)
            {
                if (!System.IO.File.Exists(this.curSettingFileName))
                {
                    this.SaveSchema(this.curSettingFileName);
                }
                frmFpSpreadColumnDefine frmFpSpreadColumnDefine = new frmFpSpreadColumnDefine(this.curSettingFileName,this.ActiveSheet.SheetName);
                frmFpSpreadColumnDefine.ShowDialog();
                this.ReadSchema(this.curSettingFileName);
            }
            base.OnKeyUp(e);
        }

        /// <summary>
        /// 重新设置数据源列顺序
        /// </summary>
        private void ResortDataColumns()
        {
            int sheetIndex = 0;
            foreach (FarPoint.Win.Spread.SheetView sv in this.Sheets)
            {
                string sheetName = sv.SheetName;

                if (string.IsNullOrEmpty(sheetName))
                {
                    sheetName = "SheetName" + sheetIndex.ToString();
                }
                sheetIndex++;

                System.Data.DataTable dt = new System.Data.DataTable();
                if (sv.DataSource is System.Data.DataSet && ((System.Data.DataSet)sv.DataSource).Tables.Count == 1)
                {
                    dt = ((System.Data.DataSet)sv.DataSource).Tables[0];
                }
                else if (sv.DataSource is System.Data.DataTable)
                {
                    dt = (System.Data.DataTable)sv.DataSource;
                }
                else if (sv.DataSource is System.Data.DataView)
                {
                    dt = ((System.Data.DataView)sv.DataSource).Table;
                }

                for (int index = 0; index < dt.Columns.Count; index++)
                {
                    System.Xml.XmlNode node = SOC.Public.XML.File.GetNode(this.curSettingFileName, sheetName + "/ColumnHeaderCell/Row0Col" + index.ToString());
                    if (node != null)
                    {
                        if (node.Attributes.GetNamedItem("Text") == null)
                        {
                            throw new Exception("配置文件已经损坏：找不到节点Row0Col" + index.ToString() + "的属性Text");
                        }
                        dt.Columns[node.Attributes["Text"].InnerText].SetOrdinal(index);
                    }
                    else
                    {
                        throw new Exception("配置文件已经损坏：找不到节点Row0Col" + index.ToString());
                    }
                }
            }
        }

        #endregion
    }
}
