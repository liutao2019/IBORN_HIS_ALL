using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using FS.FrameWork.Management;

namespace FS.SOC.Local.Material.Base
{
    /// <summary>
    /// Function<br></br>
    /// <Font color='#FF1111'>[功能描述: 常用函数</Font><br></br>
    /// [创 建 者: 耿晓雷]<br></br>
    /// [创建时间: 2010-5-26]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='yyyy-mm-dd' 
    ///		修改目的=''
    ///		修改描述=''
    ///		/>
    /// </summary>
    public static class Function
    {
        #region 私有变量

        /// <summary>
        /// 金额小数点后保留位数
        /// </summary>{FAD6EDE8-5EFC-4340-A262-984A2368B74A}
        private static int moneyDecimalPlaces = -1;

        /// <summary>
        /// 数量小数点后保留位数
        /// </summary>
        /// {259AEE90-1E75-468d-9331-940367CD54F4}
        private static int numDecimalPlaces = -1;

        /// <summary>
        /// 出库时领用人是否为必填项
        /// </summary>
        ///{AC3793C9-D570-4688-B042-1F4791DD1CCE}
        private static string isNeedGetOper = null;

        /// <summary>
        /// 入库时是否判断注册证
        /// </summary>
        /// {00943189-3EE4-47ab-A1A2-48A88E9720B2}
        private static string isCheckRegistration = null;
        #endregion

        #region 属性

        /// <summary>
        /// 金额小数点后保留位数
        /// </summary>
        /// {FAD6EDE8-5EFC-4340-A262-984A2368B74A}
        public static int MoneyDecimalPlaces
        {
            get
            {
                if (moneyDecimalPlaces == -1)
                {
                    moneyDecimalPlaces = FS.FrameWork.Function.NConvert.ToInt32(FS.HISFC.BizProcess.Material.Manager.MatControlArgument.GetControlValue(FS.HISFC.BizProcess.Material.Manager.EnumMatControlArgument.小数点后保留位数));
                }
                return moneyDecimalPlaces;
            }
        }

        /// <summary>
        /// 数量小数点后保留位数
        /// </summary>
        /// {259AEE90-1E75-468d-9331-940367CD54F4}
        public static int NumDecimalPlaces
        {
            get
            {
                if (numDecimalPlaces == -1)
                {
                    numDecimalPlaces = FS.FrameWork.Function.NConvert.ToInt32(FS.HISFC.BizProcess.Material.Manager.MatControlArgument.GetControlValue(FS.HISFC.BizProcess.Material.Manager.EnumMatControlArgument.小数点后保留位数));
                }
                return numDecimalPlaces;
            }
        }

        /// <summary>
        /// 出库时领用人是否为必填项
        /// </summary>
        ///{AC3793C9-D570-4688-B042-1F4791DD1CCE}
        public static string IsNeedGetOper
        {
            get
            {
                //if (string.IsNullOrEmpty(isNeedGetOper))
                //{
                //    isNeedGetOper = FS.HISFC.BizProcess.Material.Manager.MatControlArgument.GetControlValue(FS.HISFC.BizProcess.Material.Manager.EnumMatControlArgument.出库时领用人是否为必填项);
                //}
                //if (string.IsNullOrEmpty(isNeedGetOper))
                //{
                //    isNeedGetOper = "0";
                //}
                return isNeedGetOper;
            }
        }

        /// <summary>
        /// 入库时是否判断注册证
        /// </summary>
        /// {00943189-3EE4-47ab-A1A2-48A88E9720B2}
        public static string IsCheckRegistration
        {
            get
            {
                if (string.IsNullOrEmpty(isCheckRegistration))
                {
                    isCheckRegistration = FS.HISFC.BizProcess.Material.Manager.MatControlArgument.GetControlValue(FS.HISFC.BizProcess.Material.Manager.EnumMatControlArgument.入库是否对注册证进行判断);
                }
                if (string.IsNullOrEmpty(isCheckRegistration))
                {
                    isCheckRegistration = "0";
                }
                return isCheckRegistration;
            }
        }

        #endregion

        #region 公开方法

        /// <summary>
        /// 查找列索引
        /// </summary>
        /// <param name="lableName"></param>
        /// <param name="sheet"></param>
        /// <returns></returns>
        public static int GetColIndex(string lableName, FarPoint.Win.Spread.SheetView sheet)
        {
            for (int i = 0; i < sheet.Columns.Count; i++)
            {
                if (sheet.Columns[i].Label == lableName)
                {
                    return i;
                }
            }
            //返回-1的时候fp会默认所有列，而不会抛异常
            return -2;
        }

        /// <summary>
        /// 获取显示数量字符串
        /// </summary>
        /// <param name="num"></param>
        /// <param name="packQty"></param>
        /// <param name="minUnit"></param>
        /// <param name="packUnit"></param>
        /// <returns></returns>
        public static string GetShowNumString(decimal num, decimal packQty, string minUnit, string packUnit)
        {
            int packNum = (int)Math.Floor(num / packQty);
            decimal minNum = num % packQty;
            string numString = "";
            string tailString = "";
            if (packNum > 0)
            {
                numString = packNum.ToString() + packUnit;
                tailString = "(共" + num + minUnit + ")";
            }
            numString += minNum.ToString() + minUnit + tailString;
            return numString;
        }

        /// <summary>
        /// 保存前检查字符串长度是否超过标准位数，是否含有特殊字符等
        /// </summary>
        /// {9FF9FEBB-0257-425a-97CA-720E586A5DFE}
        /// <param name="stringForCheck">被检查的字符串</param>
        /// <param name="length">标准位数</param>
        /// <param name="isAutoChange">是否将半角字符转换为全角字符</param> 
        /// <returns>true 通过检查 false 未通过</returns>
        public static bool StringCheckBeforeSave(ref string stringForCheck, int maxLength, bool isAotuChange, ref string msg)
        {
            if (string.IsNullOrEmpty(stringForCheck))
            {
                msg = FS.FrameWork.Management.Language.Msg("未输入任何内容");
                return false;
            }
            //其次判断是否超出长度
            if (System.Text.Encoding.Default.GetByteCount(stringForCheck) > maxLength)
            {
                msg = FS.FrameWork.Management.Language.Msg("中内容长度超过长度最大值：" + maxLength + "，请重新输入");
                return false;
            }

            //判断是否包含特殊字符
            char[] specialChar = { '\'', '&', '|', '\"' };
            if (stringForCheck.IndexOfAny(specialChar) >= 0)
            {
                //存在特殊字符
                if (isAotuChange)
                {
                    //如果设置自动替换的话
                    string newString = stringForCheck.Clone().ToString();

                    int index = newString.IndexOfAny(specialChar);
                    while (index != -1)
                    {
                        newString = newString.Replace(newString[index], Function.ToSBC(newString[index]));
                        index = newString.IndexOfAny(specialChar);
                    }
                    //转换完成后再判断是否超出长度
                    if (System.Text.Encoding.Default.GetByteCount(newString) > maxLength)
                    {
                        msg = FS.FrameWork.Management.Language.Msg("转换特殊字符后长度超过限制：" + maxLength + "，请重新输入");
                        return false;
                    }
                    stringForCheck = newString;
                }
                else
                {
                    msg = FS.FrameWork.Management.Language.Msg("中存在特殊字符，请修改。");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 判断字符串中是否包括中文字符
        /// </summary>
        /// {9FF9FEBB-0257-425a-97CA-720E586A5DFE}
        /// <param name="stringForCheck">被校验的字符串</param>
        /// <returns>默认返回false，如果存在中文字符，返回true</returns>
        public static bool IsContainChinese(string stringForCheck)
        {
            string cloneString = stringForCheck.Trim();
            Regex rx = new Regex("^[\u4e00-\u9fa5]$");
            for (int i = 0; i < stringForCheck.Length; i++)
            {
                if (rx.IsMatch(cloneString[i].ToString()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 半角转全角函数
        /// </summary>
        ///任意字符串
        ///全角字符串
        ///
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///
        /// <param name="input">任意字符串</param>
        /// <returns>转换后的字符串</returns>
        public static String ToSBC(String input)
        {
            // 半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new String(c);
        }

        /// <summary>
        /// 字符半角转全角
        /// </summary>
        /// {9FF9FEBB-0257-425a-97CA-720E586A5DFE}
        /// <param name="input">输入</param>
        /// <returns>输出</returns>
        public static char ToSBC(char input)
        {
            //半角转全角：
            if (input == 32)
            {
                input = (char)12288;
            }
            else
            {
                if (input < 127)
                {
                    input = (char)(input + 65248);
                }
            }
            return input;
        }

        /// <summary>
        /// 设置farpoint的列最佳宽度，如果最佳宽度大于最大显示宽度，则该列宽度为最大显示宽度
        /// </summary>
        /// <param name="sv">farpoint</param>
        /// <param name="maxWidth">最大显示宽度</param>
        public static void SetFpPerfectWidth(ref FarPoint.Win.Spread.SheetView sv, int maxWidth)
        {
            float tempWidth = 0;
            for (int i = 0; i < sv.ColumnCount; i++)
            {
                tempWidth = sv.Columns[i].GetPreferredWidth();
                if (tempWidth > maxWidth)
                {
                    tempWidth = maxWidth;

                    for (int j = 0; j < sv.RowCount; j++)
                    {
                        if (sv.Cells[j, i].Text.Trim().Length > maxWidth)
                        {
                            if (sv.Cells[j, i].GetType() == typeof(FarPoint.Win.Spread.CellType.TextCellType))
                            {
                                FarPoint.Win.Spread.CellType.TextCellType textCellType = new FarPoint.Win.Spread.CellType.TextCellType();
                                textCellType.Multiline = true;
                                textCellType.WordWrap = true;
                                sv.Cells[j, i].CellType = textCellType;
                            }
                            sv.Rows[j].Height = sv.Rows[j].GetPreferredHeight();
                        }
                    }
                }
                sv.Columns[i].Width = tempWidth;
            }
        }

        /// <summary>
        /// 获取输入字符的合法长度，如果超过，将自动截取
        /// </summary>
        /// <param name="text">输入的字符串</param>
        /// <param name="length">最大长度</param>
        /// <returns></returns>
        public static string GetValidLength(string text, int length)
        {
            string returnText = string.Empty;
            returnText = text.Trim();
            if (System.Text.Encoding.Default.GetByteCount(text.Trim()) > length)
            {
                byte[] bytes = System.Text.Encoding.Default.GetBytes(text.ToCharArray());
                returnText = System.Text.Encoding.Default.GetString(bytes, 0, length);
            }
            return returnText;
        }

        /// <summary>
        /// 调整DataTable列顺序
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        //{9EDC5FC9-13A4-4e95-BAF4-B8D92326501A}
        public static int AdjustColumnOrder(System.Data.DataTable dataTable, ref FarPoint.Win.Spread.SheetView sv)
        {
            for (int i = 0; i < sv.Columns.Count; i++)
            {
                string columnsName = sv.ColumnHeader.Columns[i].Label;
                if (dataTable.Columns.Contains(columnsName))
                {
                    dataTable.Columns[columnsName].SetOrdinal(i);
                }
                else
                {
                    continue;
                }
            }
            return 1;
        }

        /// <summary>
        /// 校验当前系统的语言版本与xml的语言版本是否相同，如果不同，则删除该xml
        /// </summary>
        /// {A982DC32-F168-4f37-AE8C-C352049AC606}
        /// <param name="xmlFilePath">xml路径</param>
        /// <returns>true 相同，false 不同</returns>
        public static bool CheckLanguageVersion(string xmlFilePath)
        {
            try
            {

                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(xmlFilePath);
                System.Xml.XmlNode languageNode = doc.SelectSingleNode("/Setting/LanguageVersion");
                System.Xml.XmlElement xe = (System.Xml.XmlElement)languageNode;
                string currentLanguage = Language.CurrentLanguage;
                if (string.IsNullOrEmpty(currentLanguage))
                {
                    currentLanguage = "Default Language";
                }
                if (xe.GetAttribute("language").Equals(currentLanguage))
                {
                    return true;
                }
                System.IO.File.Delete(xmlFilePath);
                return false;
            }
            catch
            {
                System.IO.File.Delete(xmlFilePath);
                return false;
            }
        }

        /// <summary>
        /// 为xml文件添加当前系统的语言版本
        /// </summary>
        /// {A982DC32-F168-4f37-AE8C-C352049AC606}
        /// <param name="xmlFilePath">xml路径</param>
        /// <returns>true 成功 false 失败</returns>
        public static bool AddLanguageVersion(string xmlFilePath)
        {
            try
            {
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(xmlFilePath);
                System.Xml.XmlNode root = doc.SelectSingleNode("Setting");
                System.Xml.XmlElement xe = doc.CreateElement("LanguageVersion");
                string currentLanguage = string.Empty;
                if (string.IsNullOrEmpty(Language.CurrentLanguage))
                {
                    currentLanguage = "Default Language";
                }
                else
                {
                    currentLanguage = Language.CurrentLanguage;
                }
                xe.SetAttribute("language", currentLanguage);
                root.AppendChild(xe);
                doc.Save(xmlFilePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static FS.HISFC.Models.Fee.Item.Undrug CovertMatBaseInfoToUndrugInfo(FS.HISFC.BizLogic.Material.Object.MatBase matBaseInfo)
        {
            FS.HISFC.Models.Fee.Item.Undrug undrug = new FS.HISFC.Models.Fee.Item.Undrug();
            undrug.ID = matBaseInfo.ID;
            undrug.Name = matBaseInfo.Name;
            undrug.SysClass.ID = "U";
            undrug.MinFee.ID = matBaseInfo.MinFee.ID;
            undrug.UserCode = matBaseInfo.UserCode;
            undrug.SpellCode = matBaseInfo.SpellCode;
            undrug.WBCode = matBaseInfo.WBCode;
            undrug.GBCode = matBaseInfo.GBCode;
            undrug.NationCode = matBaseInfo.UserCode;
            undrug.Price = matBaseInfo.SalePrice;
            undrug.PriceUnit = matBaseInfo.MinUnit;
            undrug.FTRate.EMCRate = 0;
            undrug.IsFamilyPlanning = false;
            undrug.Grade = "0";
            undrug.IsNeedConfirm = false;
            undrug.ValidState = "1";
            undrug.Specs = matBaseInfo.Specs;
            undrug.ExecDept = "";
            undrug.MachineNO = "";
            undrug.CheckBody = "";
            undrug.OperationInfo.ID = "";
            undrug.OperationType.ID = "";
            undrug.OperationScale.ID = "";
            undrug.IsCompareToMaterial = false;
            undrug.Memo = "";
            undrug.Oper.ID = matBaseInfo.Oper.ID;
            undrug.ChildPrice = matBaseInfo.SalePrice;
            undrug.SpecialPrice = matBaseInfo.SalePrice;
            undrug.SpecialFlag = "0";
            undrug.SpecialFlag1 = "0";
            undrug.SpecialFlag2 = "0";
            undrug.SpecialFlag3 = "0";
            undrug.SpecialFlag4 = "0";
            undrug.DiseaseType.ID = "";
            undrug.SpecialDept.ID = "";
            undrug.IsConsent = false;
            undrug.MedicalRecord = "";
            undrug.CheckRequest = matBaseInfo.ID;
            undrug.Notice = "";
            undrug.CheckApplyDept = "";
            undrug.IsNeedBespeak = false;
            undrug.ItemArea = "0";
            undrug.ItemException = "";
            undrug.UnitFlag = "0";
            undrug.ApplicabilityArea = "0";

            return undrug;
        }
        #endregion

        public static int DisplayToFp(FarPoint.Win.Spread.SheetView sv, DataTable dt, int beginRowIdx, int beginColumnIdx)
        {
            int dtRowIdx = 0;
            foreach (DataRow dr in dt.Rows)
            {
                dtRowIdx = dt.Rows.IndexOf(dr);
                int dtColumnIdx = 0;
                foreach (DataColumn dc in dt.Columns)
                {
                    dtColumnIdx = dt.Columns.IndexOf(dc);
                    switch (dc.DataType.ToString())
                    {
                        case "System.Decimal":
                            {
                                sv.Cells[dtRowIdx + beginRowIdx, dtColumnIdx + beginColumnIdx].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();
                                sv.Cells[dtRowIdx + beginRowIdx, dtColumnIdx + beginColumnIdx].Text = dr[dtColumnIdx].ToString();
                                break;
                            }
                        default:
                            {
                                sv.Cells[dtRowIdx + beginRowIdx, dtColumnIdx + beginColumnIdx].Text = dr[dtColumnIdx].ToString();
                                break;
                            }
                    }
                    //sv.Cells[dtRowIdx + beginRowIdx, dtColumnIdx + beginColumnIdx].Text = dr[dtColumnIdx].ToString();
                }
            }
            return 1;
        }
        
        public static int DisplayToFpReverse(FarPoint.Win.Spread.SheetView sv, DataTable dt, int beginRowIdx, int beginColumnIdx)
        {
            int dtRowIdx = 0;
            foreach (DataRow dr in dt.Rows)
            {
                dtRowIdx = dt.Rows.IndexOf(dr);
                int dtColumnIdx = 0;
                foreach (DataColumn dc in dt.Columns)
                {
                    dtColumnIdx = dt.Columns.IndexOf(dc);
                    sv.Cells[dtColumnIdx + beginRowIdx, dtRowIdx + beginColumnIdx].Text = dr[dtColumnIdx].ToString();
                }
            }
            return 1;
        }
        
        public static int DrawGridLine(FarPoint.Win.Spread.SheetView sv, int row, int column, int rowCount, int columnCount)
        {
            #region 画格
            FarPoint.Win.LineBorder lineBorderLTRB = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, true, true, true, true);
            int row2 = 0;
            int column2 = 0;
            if (row + rowCount - 1 >= row)
            {
                row2 = row + rowCount - 1;
            }
            else
            {
                row2 = row;
            }
            if (column + columnCount - 1 >= column)
            {
                column2 = column + columnCount - 1;
            }
            else
            {
                column2 = column;
            }
            if (rowCount > 0)
            {
                sv.Cells[row, column, row2, column2].Border = lineBorderLTRB;
            }
            return 1;
            //#region 先画左上
            //FarPoint.Win.LineBorder lineBorderLT = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, true, true, false, false);
            //SvMain.Cells[this.DataBeginRowIndex + 1, 0, this.DataBeginRowIndex + this.dataRowCount, dataDisplayColumns.Length - 1].Border = lineBorderLT;
            //#endregion
            //#region 再画最下面
            //FarPoint.Win.LineBorder lineBorderLTB = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, true, true, false, true);
            //SvMain.Cells[this.DataBeginRowIndex + this.dataRowCount, 0, this.DataBeginRowIndex + this.dataRowCount, dataDisplayColumns.Length - 1].Border = lineBorderLTB;
            //#endregion
            //#region 再画最右面
            //FarPoint.Win.LineBorder lineBorderLTR = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, true, true, true, false);
            //SvMain.Cells[this.DataBeginRowIndex + 1, dataDisplayColumns.Length - 1, this.DataBeginRowIndex + this.dataRowCount, dataDisplayColumns.Length - 1].Border = lineBorderLTR;
            //#endregion
            //#region 再画右下的一个格
            ////FarPoint.Win.LineBorder lineBorderLTRB = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, true, true, true, true);
            //SvMain.Cells[this.DataBeginRowIndex + this.dataRowCount, dataDisplayColumns.Length - 1, this.DataBeginRowIndex + this.dataRowCount, dataDisplayColumns.Length - 1].Border = lineBorderLTRB;
            //#endregion
            #endregion
        }
        
        /// <summary>
        /// 克隆Cell
        /// </summary>
        /// <param name="cellRes">结果cell</param>
        /// <param name="cell">原cell</param>
        public static void CloneSheetViewCell(FarPoint.Win.Spread.Cell cellRes, FarPoint.Win.Spread.Cell cell)
        {
            cellRes.Font = cell.Font;
            cellRes.ColumnSpan = cell.ColumnSpan;
            cellRes.RowSpan = cell.RowSpan;
            cellRes.Value = cell.Value;
            cellRes.HorizontalAlignment = cell.HorizontalAlignment;
            cellRes.Border = cell.Border;
            cellRes.CellType = cell.CellType;
            cellRes.VerticalAlignment = cell.VerticalAlignment;
        }

        public static void SortByCustomerCode(ref System.Collections.ArrayList alPrintData)
        {
            FS.SOC.Local.Material.Base.LocalSort.CompareByCustomerCode c = new LocalSort.CompareByCustomerCode();
            alPrintData.Sort(c);
        }
    }
}
