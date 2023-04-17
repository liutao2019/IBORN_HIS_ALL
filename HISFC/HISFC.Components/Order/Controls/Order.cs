using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Collections;
namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// [功能描述: 医嘱控件]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2004-10-12]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public class Order
    {

        //public string LONGSETTINGFILENAME = FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath + "longordersetting.xml";
        //public string SHORTSETTINGFILENAME = FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath + "shortordersetting.xml";

        public DataSet dsAllLong = null;

        /// <summary>
        /// 缓存药品基本信息
        /// </summary>
        static Hashtable hsPhaItem = new Hashtable();

        /// <summary>
        /// 获取药品基本信息，不保证是最新信息
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public static FS.HISFC.Models.Pharmacy.Item GetPHAItem(string itemCode)
        {
            FS.HISFC.Models.Pharmacy.Item item = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(itemCode);
            if (item == null)
            {
                if (hsPhaItem.Contains(itemCode))
                {
                    item = hsPhaItem[itemCode] as FS.HISFC.Models.Pharmacy.Item;
                }
                else
                {
                    item = CacheManager.PhaIntegrate.GetItem(itemCode);
                    hsPhaItem.Add(itemCode, item);
                }
            }
            return item;
        }


        #region 初始化
        /// <summary>
        /// 设置DataSet
        /// </summary>
        /// <param name="dataSet"></param>
        [Obsolete("后面再处理这个吧", false)]
        public void SetDataSet(ref System.Data.DataSet dataSet)
        {
            try
            {

                Type dtStr = System.Type.GetType("System.String");
                Type dtDbl = typeof(System.Double);
                Type dtInt = typeof(System.Int32);
                Type dtBoolean = typeof(System.Boolean);
                Type dtDate = typeof(System.DateTime);

                DataTable table = new DataTable("Table");
                table.Columns.AddRange(new DataColumn[] {
															new DataColumn("!",dtStr),     //0
															new DataColumn("期效",dtStr),     //0
															new DataColumn("医嘱类型",dtStr),//1
															new DataColumn("医嘱流水号",dtStr),//2
															new DataColumn("医嘱状态",dtStr),//新开立，审核，执行
															new DataColumn("组合号",dtStr),//4
															new DataColumn("主药",dtStr),//5
                                                            
                                                            new DataColumn("组号",dtStr),
                                                            new DataColumn("开立时间",dtStr),
															new DataColumn("开立医生",dtStr),
															new DataColumn("顺序号",dtInt),//28
                                                            new DataColumn("医嘱名称",dtStr),//6
															new DataColumn("组合",dtStr),     //0
															new DataColumn("首日量",dtInt),//7
                                                            new DataColumn("每次用量",dtStr),//9
															new DataColumn("单位",dtStr),//10

                                                            new DataColumn("频次编码",dtStr),
															new DataColumn("频次名称",dtStr),
															new DataColumn("用法编码",dtStr),
															new DataColumn("用法名称",dtStr),//15

                                                            new DataColumn("总量",dtStr),//7
															new DataColumn("总量单位",dtStr),//8
															
															new DataColumn("付数",dtStr),//11

															new DataColumn("系统类别",dtStr),//5															
															
															new DataColumn("开始时间",dtStr),
                                                            new DataColumn("结束时间",dtStr),//25
															new DataColumn("停止时间",dtStr),//25
                                                            
															new DataColumn("执行科室编码",dtStr),
															new DataColumn("执行科室",dtStr),
															new DataColumn("加急",dtBoolean),
															new DataColumn("检查部位",dtStr),//31
															new DataColumn("样本类型",dtStr),//32
															new DataColumn("扣库科室编码",dtStr),//33
															new DataColumn("扣库科室",dtStr),//34
															new DataColumn("备注",dtStr),//20
															new DataColumn("录入人编码",dtStr),
															new DataColumn("录入人",dtStr),
															new DataColumn("开立科室",dtStr),

                                                            //new DataColumn("开立时间",dtStr),

															new DataColumn("停止人编码",dtStr),
															new DataColumn("停止人",dtStr),
                                                            new DataColumn("滴速",dtStr),
                                                            new DataColumn("国家医保代码",dtStr)
														});


                dataSet.Tables.Add(table);

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("SetDataSet" + ex.Message);
                return;
            }
        }


        public FarPoint.Win.Spread.FpSpread fpSpread1 = null;

        #endregion

        #region "对应"

        [Obsolete("", true)]
        public int[] iColumns;

        /// <summary>
        /// 存储列宽
        /// </summary>
        //[Obsolete("", true)]
        //public int[] iColumnWidth;

        /// <summary>
        /// 设置列属性
        /// </summary>
        [Obsolete("", true)]
        public void SetColumnProperty()
        {
            //if (System.IO.File.Exists(LONGSETTINGFILENAME))
            //{
            //    //if (iColumnWidth == null || iColumnWidth.Length <= 0)
            //    //{
            //        FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1.Sheets[0], LONGSETTINGFILENAME);
            //        FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1.Sheets[1], SHORTSETTINGFILENAME);

            //        //iColumnWidth = new int[40];
            //        //for (int i = 0; i < this.fpSpread1.Sheets[0].Columns.Count; i++)
            //        //{
            //        //    iColumnWidth[i] = (int)this.fpSpread1.Sheets[0].Columns[i].Width;
            //        //}
            //    //}
            //    //else
            //    //{
            //    //    for (int i = 0; i < this.fpSpread1.Sheets[0].Columns.Count; i++)
            //    //    {
            //    //        this.fpSpread1.Sheets[0].Columns[i].Width = iColumnWidth[i];
            //    //        this.fpSpread1.Sheets[1].Columns[i].Width = iColumnWidth[i];
            //    //    }
            //    //}
            //}
            //else
            //{
            //    FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1.Sheets[0], LONGSETTINGFILENAME);
            //    FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1.Sheets[1], SHORTSETTINGFILENAME);
            //}
        }
        [Obsolete("", true)]
        public void SetColumnWidth()
        {
            //this.iColumnWidth = new int[40];
            //this.iColumnWidth[0] = 56;
            //this.iColumnWidth[1] = 10;
            //this.iColumnWidth[2] = 56;
            //this.iColumnWidth[3] = 10;
            //this.iColumnWidth[4] = 10;
            //this.iColumnWidth[5] = 10;
            //this.iColumnWidth[6] = 10;
            //this.iColumnWidth[7] = 185;
            //this.iColumnWidth[8] = 15;
            //this.iColumnWidth[9] = 31;
            //this.iColumnWidth[10] = 31;
            //this.iColumnWidth[11] = 46;
            //this.iColumnWidth[12] = 31;
            //this.iColumnWidth[13] = 33;
            //this.iColumnWidth[14] = 33;
            //this.iColumnWidth[15] = 10;
            //this.iColumnWidth[16] = 10;
            //this.iColumnWidth[17] = 31;
            //this.iColumnWidth[18] = 76;//开始时间
            //this.iColumnWidth[19] = 76;//停止时间
            //this.iColumnWidth[20] = 56;//开立医生
            //this.iColumnWidth[21] = 10;//执行科室编码
            //this.iColumnWidth[22] = 56;//执行科室
            //this.iColumnWidth[23] = 19;//加急
            //this.iColumnWidth[24] = 56;//检查部位
            //this.iColumnWidth[26] = 56;//样本类型
            //this.iColumnWidth[27] = 10;//扣库科室编码
            //this.iColumnWidth[28] = 56;//扣库科室
            //this.iColumnWidth[29] = 56;
            //this.iColumnWidth[30] = 56;
            //this.iColumnWidth[31] = 56;
            //this.iColumnWidth[32] = 56;
            //this.iColumnWidth[33] = 56;
            //this.iColumnWidth[34] = 56;
            //this.iColumnWidth[35] = 56;
            //this.iColumnWidth[36] = 56;
            //this.iColumnWidth[37] = 10;
            //this.iColumnWidth[38] = 10;
            //this.iColumnWidth[39] = 10;
        }

        /// <summary>
        /// 通过列名获得列索引
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        [Obsolete("", true)]
        public int GetColumnIndexFromName(string Name)
        {
            for (int i = 0; i < dsAllLong.Tables[0].Columns.Count; i++)
            {
                if (dsAllLong.Tables[0].Columns[i].ColumnName == Name)
                    return i;
            }
            MessageBox.Show("缺少列" + Name);
            return -1;
        }
        [Obsolete("", true)]
        public void ColumnSet()
        {
            //iColumns = new int[40];
            //iColumns[0] = this.GetColumnIndexFromName("期效");     //Type
            //iColumns[1] = this.GetColumnIndexFromName("医嘱类型");//OrderType
            //iColumns[2] = this.GetColumnIndexFromName("医嘱流水号");//ID
            //iColumns[3] = this.GetColumnIndexFromName("医嘱状态");//新开立，审核，执行State
            //iColumns[4] = this.GetColumnIndexFromName("组合号");//4 ComboNo
            //iColumns[5] = this.GetColumnIndexFromName("主药");//5 MainDrug
            //iColumns[6] = this.GetColumnIndexFromName("医嘱名称");//6 Nameer	
            //iColumns[7] = this.GetColumnIndexFromName("总量");//7	Qty
            //iColumns[8] = this.GetColumnIndexFromName("总量单位");//8 PackUnit
            //iColumns[9] = this.GetColumnIndexFromName("每次用量");//9 DoseOnce
            //iColumns[10] = this.GetColumnIndexFromName("单位");//10 doseUnit
            //iColumns[11] = this.GetColumnIndexFromName("付数");//11 Fu
            //iColumns[12] = this.GetColumnIndexFromName("频次编码"); //FrequencyCode
            //iColumns[13] = this.GetColumnIndexFromName("频次名称"); //FrequecyName
            //iColumns[14] = this.GetColumnIndexFromName("用法编码"); //UsageCode
            //iColumns[15] = this.GetColumnIndexFromName("用法名称");//15
            //iColumns[16] = this.GetColumnIndexFromName("开始时间");
            //iColumns[17] = this.GetColumnIndexFromName("执行科室编码");
            //iColumns[18] = this.GetColumnIndexFromName("执行科室");
            //iColumns[19] = this.GetColumnIndexFromName("加急");
            //iColumns[20] = this.GetColumnIndexFromName("备注");//20
            //iColumns[21] = this.GetColumnIndexFromName("录入人编码");
            //iColumns[22] = this.GetColumnIndexFromName("录入人");
            //iColumns[23] = this.GetColumnIndexFromName("开立科室");
            //iColumns[25] = this.GetColumnIndexFromName("停止时间");//25
            //iColumns[26] = this.GetColumnIndexFromName("停止人编码");
            //iColumns[27] = this.GetColumnIndexFromName("停止人");
            //iColumns[28] = this.GetColumnIndexFromName("顺序号");//28
            //iColumns[24] = this.GetColumnIndexFromName("开立时间");
            //iColumns[29] = this.GetColumnIndexFromName("开立医生");
            //iColumns[30] = this.GetColumnIndexFromName("组合");
            //iColumns[31] = this.GetColumnIndexFromName("检查部位");
            //iColumns[32] = this.GetColumnIndexFromName("样本类型");
            //iColumns[33] = this.GetColumnIndexFromName("扣库科室编码");34
            //iColumns[34] = this.GetColumnIndexFromName("扣库科室");
            //iColumns[35] = this.GetColumnIndexFromName("!");

            //iColumns[36] = this.GetColumnIndexFromName("系统类别");
            //iColumns[37] = this.GetColumnIndexFromName("首日量");
            //iColumns[38] = this.GetColumnIndexFromName("组合");
            //iColumns[39] = this.GetColumnIndexFromName("组号");

        }

        [Obsolete("换做SetColumnNameNew", true)]
        public void SetColumnName(int k)
        {
            this.fpSpread1.Sheets[k].Columns.Count = 100;
            int i = 0;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("!");    //0
            this.fpSpread1.Sheets[k].Columns[i].Visible = false;
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("期效");     //0
            this.fpSpread1.Sheets[k].Columns[i].Visible = false;
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("医嘱类型");//1
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("医嘱流水号");//2
            this.fpSpread1.Sheets[k].Columns[i].Visible = false;
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("医嘱状态");//新开立，审核，执行
            this.fpSpread1.Sheets[k].Columns[i].Visible = false;
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("组合号");//4
            this.fpSpread1.Sheets[k].Columns[i].Visible = false;
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("主药");//5
            this.fpSpread1.Sheets[k].Columns[i].Visible = false;


            /*医嘱顺序显示调整*/

            i++;

            this.fpSpread1.Sheets[k].Columns[i].Label = ("组号");

            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("开立时间");
            this.fpSpread1.Sheets[k].Columns[i].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            i++;

            this.fpSpread1.Sheets[k].Columns[i].Label = ("开立医生");

            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("顺序号");//28

            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("医嘱名称");//6
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("组");    //0
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("首日量");//7

            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("每次量");//9
            this.fpSpread1.Sheets[k].Columns[i].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.fpSpread1.Sheets[k].Columns[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("单位");//10

            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("频次");
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("频次名称");
            this.fpSpread1.Sheets[k].Columns[i].Visible = false;
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("用法编码");
            this.fpSpread1.Sheets[k].Columns[i].Visible = false;
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("用法");//15


            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("总量");//7
            this.fpSpread1.Sheets[k].Columns[i].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.fpSpread1.Sheets[k].Columns[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("单位");//8

            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("付数");//11

            /*调整截止*/

            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("系统类别");//6

            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("开始时间");
            this.fpSpread1.Sheets[k].Columns[i].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("停止时间");//25
            this.fpSpread1.Sheets[k].Columns[i].CellType = new FarPoint.Win.Spread.CellType.TextCellType();


            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("执行科室编码");
            this.fpSpread1.Sheets[k].Columns[i].Visible = false;
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("执行科室");
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("急");
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("检查部位");
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("样本类型");
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("扣库科室编码");
            this.fpSpread1.Sheets[k].Columns[i].Visible = false;
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("扣库科室");
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("备注");//20
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("录入人编码");
            this.fpSpread1.Sheets[k].Columns[i].Visible = false;
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("录入人");
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("开立科室");
            i++;

            this.fpSpread1.Sheets[k].Columns[i].Label = ("停止人编码");
            this.fpSpread1.Sheets[k].Columns[i].Visible = false;
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("停止人");

            i++;
            this.fpSpread1.Sheets[k].Columns.Count = i;
        }

        #endregion

        #region 函数
        /// <summary>
        /// 保存格式
        /// </summary>
        //public void SaveGrid()
        //{
        //    try
        //    {
        //        FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1.Sheets[0], this.LONGSETTINGFILENAME);
        //        FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1.Sheets[1], this.SHORTSETTINGFILENAME);
        //        MessageBox.Show("显示格式保存成功！请重新登录后生效。");
        //    }
        //    catch { }
        //}

        #endregion

    }
}
