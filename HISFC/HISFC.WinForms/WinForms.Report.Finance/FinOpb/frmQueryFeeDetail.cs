using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.Report.Finance.FinOpb
{
    public partial class frmQueryFeeDetail : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public frmQueryFeeDetail()
        {
            InitializeComponent();
            this.SetFormat();
        }

        enum EnumCol1Set
        { 
            发票号,
            病历卡号,
            挂号日期,
            患者姓名,
            结算类别,
            合同单位,
            个人编号,
            可报销金额,
            不可报销金额,
            自付金额,
            总金额,
            实付金额,
            结算人,
            结算时间,
            是否体检,
            发票状态,
            作废发票号,
            作废操作员,
            作废时间,
            是否核查,
            核查人,
            核查日期,
            是否已经日结,
            日结人,
            日结时间,
            发票序号,
            自费记帐特殊,
            交易类型        
        }

        enum EnumCol2Set
        { 
            发票号,
            交易类型,
            发票内流水号,
            科目名称,
            可报销金额,
            不可报销金额,
            自付金额,
            总金额,
            操作时间,
            操作员,
            是否已经日结,
            日结人,
            日结时间,
            发票状态
        }

        enum EnumCol3Set
        {
            发票号,
            项目代码,
            项目名称,
            单价,
            数量,
            付数,
            报销金额,
            自付金额,
            现金金额,
            优惠金额,
            发票科目,
            开方医师,
            开方医师科室,
            划价人,
            划价时间,
            收费员,
            收费日期,
            发票内流水号,
            发票状态,
            自费记帐特殊,
            交易类型,
            最小费用,
            系统类别,
            处方号,
            流水号,
            是否药品,
            可退数量,
            包装数量,
            药品性质,
            规格,
            剂型,
            是否自制药,
            频次,
            用法,
            院注次数,
            执行科室,
            医保中心项目代码,
            项目等级,
            新项目比例,
            原项目比例,
            是否主药,
            组合号,
            复合项目代码,
            复合项目名称,
            医技终端确认,
            终端确认人,
            终端确认科室,
            终端确认时间,
            确认数量,
            门诊号,
            病历卡号,
            挂号日期,
            挂号科室
        }

        private int decimals = 2;
        FS.SOC.Public.FarPoint.ColumnSet colSet  = new FS.SOC.Public.FarPoint.ColumnSet();
        FS.SOC.Public.FarPoint.ColumnSet colSet1 = new FS.SOC.Public.FarPoint.ColumnSet();
        FS.SOC.Public.FarPoint.ColumnSet colSet2 = new FS.SOC.Public.FarPoint.ColumnSet();

        private void SetFormat()
        {
            this.colSet.AddColumn(
            new FS.SOC.Public.FarPoint.Column[]{
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.发票号.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.病历卡号.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.挂号日期.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.患者姓名.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.结算类别.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.合同单位.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.个人编号.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.可报销金额.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.不可报销金额.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.自付金额.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.总金额.ToString(),80f,true,false,null),   
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.实付金额.ToString(),80f,true,false,null),  
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.结算人.ToString(),80f,true,false,null),                
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.结算时间.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.是否体检.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.发票状态.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.作废发票号.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.作废操作员.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.作废时间.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.是否核查.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.核查人.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.核查日期.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.是否已经日结.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.日结人.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.日结时间.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.发票序号.ToString(),80f,true,false,null),        
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.自费记帐特殊.ToString(),80f,true,false,null),
             new FS.SOC.Public.FarPoint.Column(EnumCol1Set.交易类型.ToString(),80f,true,false,null),

         }  
                );

           this.colSet1.AddColumn(new FS.SOC.Public.FarPoint.Column[]{
           new FS.SOC.Public.FarPoint.Column(EnumCol2Set.发票号.ToString(),80f,true,false,null),
           new FS.SOC.Public.FarPoint.Column(EnumCol2Set.交易类型.ToString(),80f,true,false,null),
           new FS.SOC.Public.FarPoint.Column(EnumCol2Set.发票内流水号.ToString(),80f,true,false,null),
           new FS.SOC.Public.FarPoint.Column(EnumCol2Set.科目名称.ToString(),80f,true,false,null),
           new FS.SOC.Public.FarPoint.Column(EnumCol2Set.可报销金额.ToString(),80f,true,false,null),
           new FS.SOC.Public.FarPoint.Column(EnumCol2Set.不可报销金额.ToString(),80f,true,false,null),
           new FS.SOC.Public.FarPoint.Column(EnumCol2Set.自付金额.ToString(),80f,true,false,null),
           new FS.SOC.Public.FarPoint.Column(EnumCol2Set.总金额.ToString(),80f,true,false,null),
           new FS.SOC.Public.FarPoint.Column(EnumCol2Set.操作时间.ToString(),80f,true,false,null),
           new FS.SOC.Public.FarPoint.Column(EnumCol2Set.操作员.ToString(),80f,true,false,null),
           new FS.SOC.Public.FarPoint.Column(EnumCol2Set.是否已经日结.ToString(),80f,true,false,null),
           new FS.SOC.Public.FarPoint.Column(EnumCol2Set.日结人.ToString(),80f,true,false,null),
           new FS.SOC.Public.FarPoint.Column(EnumCol2Set.日结时间.ToString(),80f,true,false,null),
           new FS.SOC.Public.FarPoint.Column(EnumCol2Set.发票状态.ToString(),80f,true,false,null)            
            }
            );
            
          this.colSet2.AddColumn(
          new FS.SOC.Public.FarPoint.Column[]{
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.发票号.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.项目代码.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.项目名称.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.单价.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.数量.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.付数.ToString(),80f,true,false,null),           
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.报销金额.ToString(),80f,true,false,null),     
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.自付金额.ToString(),80f,true,false,null),     
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.现金金额.ToString(),80f,true,false,null),     
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.优惠金额.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.发票科目.ToString(),80f,true,false,null),     
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.开方医师.ToString(),80f,true,false,null),     
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.开方医师科室.ToString(),80f,true,false,null),    
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.划价人.ToString(),80f,true,false,null),            new FS.SOC.Public.FarPoint.Column(EnumCol3Set.划价时间.ToString(),80f,true,false,null),    
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.收费员.ToString(),80f,true,false,null),            new FS.SOC.Public.FarPoint.Column(EnumCol3Set.收费日期.ToString(),80f,true,false,null),    
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.发票内流水号.ToString(),80f,true,false,null),    
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.发票状态.ToString(),80f,true,false,null),    
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.自费记帐特殊.ToString(),80f,true,false,null),    
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.交易类型.ToString(),80f,true,false,null),    
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.发票状态.ToString(),80f,true,false,null),    
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.系统类别.ToString(),80f,true,false,null),    
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.处方号.ToString(),80f,true,false,null),           
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.流水号.ToString(),80f,true,false,null),             
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.是否药品.ToString(),80f,true,false,null),                 
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.可退数量.ToString(),80f,true,false,null),    
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.包装数量.ToString(),80f,true,false,null),   
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.药品性质.ToString(),80f,true,false,null),   
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.规格.ToString(),80f,true,false,null),   
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.剂型.ToString(),80f,true,false,null),   
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.是否自制药.ToString(),80f,true,false,null),   
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.频次.ToString(),80f,true,false,null),   
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.用法.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.院注次数.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.执行科室.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.医保中心项目代码.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.项目等级.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.新项目比例.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.原项目比例.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.是否主药.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.组合号.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.复合项目代码.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.医技终端确认.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.终端确认人.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.终端确认科室.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.终端确认时间.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.确认数量.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.门诊号.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.病历卡号.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.挂号日期.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.挂号科室.ToString(),80f,true,false,null),            
                }                
                );
            this.fpSpread1_Sheet1.Columns.Count =this.colSet.Count;
            this.fpSpread1_Sheet2.Columns.Count =this.colSet1.Count;
            this.fpSpread1_Sheet3.Columns.Count =this.colSet2.Count;

            if(System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FeeDetailInvoice.xml"))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.fpSpread1_Sheet1,FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FeeDetailInvoice.xml");
                for(int colIndex = 0; colIndex < this.fpSpread1_Sheet1.Columns.Count;colIndex++)
                {
                this.fpSpread1_Sheet1.ColumnHeader.Cells[0, colIndex].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
               if(this.fpSpread1_Sheet1.Columns[colIndex].CellType is FarPoint.Win.Spread.CellType.TextCellType)
               {
                FarPoint.Win.Spread.CellType.TextCellType t = ( FarPoint.Win.Spread.CellType.TextCellType)this.fpSpread1_Sheet1.Columns[colIndex].CellType;
                   t.ReadOnly = true;
               
               }
               else if(this.fpSpread1_Sheet1.Columns[colIndex].CellType == null)
               {
                    FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                     t.ReadOnly = true;
                     this.fpSpread1_Sheet1.Columns[colIndex].CellType = t;
               }

              }
            }
        else
            {
                FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                t.ReadOnly =true;
                for(int colIndex = 0; colIndex<this.fpSpread1_Sheet1.Columns.Count;colIndex++)
                {
                    this.fpSpread1_Sheet1.ColumnHeader.Cells[0, colIndex].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    FS.SOC.Public.FarPoint.Column c = this.colSet.GetColumn(colIndex);
                    this.fpSpread1_Sheet1.Columns[colIndex].Label = c.Name;
                    this.fpSpread1_Sheet1.Columns[colIndex].Width = c.Width;
                    this.fpSpread1_Sheet1.Columns[colIndex].Visible = c.Visible;
                    this.fpSpread1_Sheet1.Columns[colIndex].Locked = c.Locked;
             if(colIndex != (int)EnumCol1Set.可报销金额 && colIndex != (int)EnumCol1Set.不可报销金额 && colIndex != (int)EnumCol1Set.自付金额 && colIndex != (int)EnumCol1Set.总金额 && colIndex != (int)EnumCol1Set.实付金额)
             {
                this.fpSpread1_Sheet1.Columns[colIndex].CellType = t;
             }   
            
           }         
         }

            if (System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FeeDetailInvoiceDetail.xml"))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.fpSpread1_Sheet2, FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FeeDetailInvoiceDetail.xml");
                for (int colIndex = 0; colIndex < this.fpSpread1_Sheet2.Columns.Count; colIndex++)
                {
                    this.fpSpread1_Sheet2.ColumnHeader.Cells[0, colIndex].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    if (this.fpSpread1_Sheet2.Columns[colIndex].CellType is FarPoint.Win.Spread.CellType.TextCellType)
                    {
                        FarPoint.Win.Spread.CellType.TextCellType t = (FarPoint.Win.Spread.CellType.TextCellType)this.fpSpread1_Sheet2.Columns[colIndex].CellType;
                        t.ReadOnly = true;

                    }
                    else if (this.fpSpread1_Sheet2.Columns[colIndex].CellType == null)
                    {
                        FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                        t.ReadOnly = true;
                        this.fpSpread1_Sheet2.Columns[colIndex].CellType = t;
                    }

                }
            }
            else
            {
                FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                t.ReadOnly = true;
                for (int colIndex = 0; colIndex < this.fpSpread1_Sheet2.Columns.Count; colIndex++)
                {
                    this.fpSpread1_Sheet2.ColumnHeader.Cells[0, colIndex].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    FS.SOC.Public.FarPoint.Column c = this.colSet1.GetColumn(colIndex);
                    this.fpSpread1_Sheet2.Columns[colIndex].Label = c.Name;
                    this.fpSpread1_Sheet2.Columns[colIndex].Width = c.Width;
                    this.fpSpread1_Sheet2.Columns[colIndex].Visible = c.Visible;
                    this.fpSpread1_Sheet2.Columns[colIndex].Locked = c.Locked;
                    if (colIndex != (int)EnumCol2Set.可报销金额 && colIndex != (int)EnumCol1Set.不可报销金额 && colIndex != (int)EnumCol1Set.自付金额 && colIndex != (int)EnumCol1Set.总金额 )
                    {
                        this.fpSpread1_Sheet2.Columns[colIndex].CellType = t;
                    }

                }
            }

            if (System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FeeDetail1.xml"))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.fpSpread1_Sheet3, FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FeeDetail1.xml");
                for (int colIndex = 0; colIndex < this.fpSpread1_Sheet3.Columns.Count; colIndex++)
                {
                    this.fpSpread1_Sheet3.ColumnHeader.Cells[0, colIndex].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    if (this.fpSpread1_Sheet3.Columns[colIndex].CellType is FarPoint.Win.Spread.CellType.TextCellType)
                    {
                        FarPoint.Win.Spread.CellType.TextCellType t = (FarPoint.Win.Spread.CellType.TextCellType)this.fpSpread1_Sheet3.Columns[colIndex].CellType;
                        t.ReadOnly = true;

                    }
                    else if (this.fpSpread1_Sheet3.Columns[colIndex].CellType == null)
                    {
                        FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                        t.ReadOnly = true;
                        this.fpSpread1_Sheet3.Columns[colIndex].CellType = t;
                    }

                }
            }
            else
            {
                FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                t.ReadOnly = true;
                for (int colIndex = 0; colIndex < this.fpSpread1_Sheet3.Columns.Count; colIndex++)
                {
                    this.fpSpread1_Sheet3.ColumnHeader.Cells[0, colIndex].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    FS.SOC.Public.FarPoint.Column c = this.colSet2.GetColumn(colIndex);
                    this.fpSpread1_Sheet3.Columns[colIndex].Label = c.Name;
                    this.fpSpread1_Sheet3.Columns[colIndex].Width = c.Width;
                    this.fpSpread1_Sheet3.Columns[colIndex].Visible = c.Visible;
                    this.fpSpread1_Sheet3.Columns[colIndex].Locked = c.Locked;
                    if (colIndex != (int)EnumCol3Set.单价 && colIndex != (int)EnumCol3Set.数量 && colIndex != (int)EnumCol3Set.付数 && colIndex != (int)EnumCol3Set.报销金额 && colIndex != (int)EnumCol3Set.自付金额 && colIndex != (int)EnumCol3Set.现金金额 && colIndex != (int)EnumCol3Set.优惠金额)
                    {
                        this.fpSpread1_Sheet3.Columns[colIndex].CellType = t;
                    }

                }
            }
        
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numberCellType.DecimalPlaces = this.decimals;
            numberCellType.ReadOnly = true;
            this.fpSpread1_Sheet1.Columns[(int)EnumCol1Set.可报销金额].CellType = numberCellType;
            this.fpSpread1_Sheet1.Columns[(int)EnumCol1Set.不可报销金额].CellType = numberCellType;
            this.fpSpread1_Sheet1.Columns[(int)EnumCol1Set.自付金额].CellType = numberCellType;
            this.fpSpread1_Sheet1.Columns[(int)EnumCol1Set.总金额].CellType = numberCellType;
            this.fpSpread1_Sheet1.Columns[(int)EnumCol1Set.实付金额].CellType = numberCellType;
            this.fpSpread1_Sheet2.Columns[(int)EnumCol2Set.可报销金额].CellType = numberCellType;
            this.fpSpread1_Sheet2.Columns[(int)EnumCol2Set.自付金额].CellType = numberCellType;
            this.fpSpread1_Sheet2.Columns[(int)EnumCol2Set.总金额].CellType = numberCellType;
            this.fpSpread1_Sheet3.Columns[(int)EnumCol3Set.单价].CellType = numberCellType;
            this.fpSpread1_Sheet3.Columns[(int)EnumCol3Set.数量].CellType = numberCellType;
            this.fpSpread1_Sheet3.Columns[(int)EnumCol3Set.付数].CellType = numberCellType;
            this.fpSpread1_Sheet3.Columns[(int)EnumCol3Set.报销金额].CellType = numberCellType;
            this.fpSpread1_Sheet3.Columns[(int)EnumCol3Set.自付金额].CellType = numberCellType;
            this.fpSpread1_Sheet3.Columns[(int)EnumCol3Set.现金金额].CellType = numberCellType;
            this.fpSpread1_Sheet3.Columns[(int)EnumCol3Set.优惠金额].CellType = numberCellType;
            this.fpSpread1.ColumnWidthChanged -= new  FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpSpread1_ColumnWidthChanged);
            this.fpSpread1.ColumnWidthChanged += new  FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpSpread1_ColumnWidthChanged);
        }
        

        #region 变量定义
        /// <summary>
        /// 查询类型
        /// </summary>
        int intQueryType = 1;

        FS.FrameWork.WinForms.Forms.frmWait frmWait = new FS.FrameWork.WinForms.Forms.frmWait();

        #endregion

        #region 函数

        #region 切换检索方式
        /// <summary>
        /// 切换检索方式
        /// </summary>
        public void ChangeQueryType()
        {
            // 转换查询类型
            if (intQueryType == 3)
            {
                intQueryType = 1;
            }
            else
            {
                intQueryType++;
            }

            // 设置查询类型显示文本
            switch (this.intQueryType)
            {
                case 1:
                    this.labQueryName.Text = "发票号(F2切换)";
                    break;
                case 2:
                    this.labQueryName.Text = "病历号(F2切换)";
                    break;
                case 3:
                    this.labQueryName.Text = "姓  名(F2切换)";
                    break;
            }

            // 设置焦点到输入框
            this.tbInput.Focus();
            this.tbInput.SelectAll();

            // 设置时间复选框
            if (this.intQueryType != 1)
            {
                this.cbDataDate.Enabled = true;
            }
            else
            {
                this.cbDataDate.Enabled = false;
            }
        }
        #endregion

        #region 获取检索码(1：成功/-1：失败)
        /// <summary>
        /// 获取检索码(1：成功/-1：失败)
        /// </summary>
        /// <param name="strCode">返回的检索码</param>
        /// <returns>1：成功/-1：失败</returns>
        public int GetInput(ref string strCode)
        {
            strCode = FS.FrameWork.Public.String.TakeOffSpecialChar(this.tbInput.Text.Trim());

            // 判断合法性
            if (this.intQueryType == 2)
            {
                try
                {
                    long.Parse(strCode);
                }
                catch
                {
                    MessageBox.Show("输入的病历卡号必须是数字形式,请尝试用发票号查询");
                    this.tbInput.Text = "";
                    this.tbInput.Focus();
                    return -1;
                }
            }

            #region {571171C3-2CF8-4edc-9403-0E5E2B424A26}
            // 判断合法性
            if (this.intQueryType == 3)
            {
                strCode = FS.FrameWork.Public.String.TakeOffSpecialChar(strCode);
                if (strCode == "" || strCode == null) { return  -1; }
            }
            #endregion
            // 填充号码
            switch (this.intQueryType)
            {
                case 1:
                    // 按照发票号查询：12位
                    strCode = strCode.PadLeft(12, '0');
                    break;
                case 2:
                    // 病历卡号：10位
                    strCode = strCode.PadLeft(10, '0');
                    break;
            }
            this.tbInput.Text = strCode;

            return 1;
        }
        #endregion

        #region 获取查询日期
        /// <summary>
        /// 获取查询日期
        /// </summary>
        /// <param name="dtFrom">返回的起始日期</param>
        /// <param name="dtTo">返回的截止日期</param>
        public void GetQueryDate(ref DateTime dtFrom, ref DateTime dtTo)
        {
            // 如果不按照发票号查询，才可以带时间参数，因为发票号是唯一的
            if (this.intQueryType != 1)
            {
                // 如果时间选项选中，日期有效选择日期
                if (this.cbDataDate.Checked)
                {
                    dtFrom = this.dtpFromDate.Value;
                    dtTo = this.dtpDateTo.Value;

                    dtFrom = new DateTime(dtFrom.Year, dtFrom.Month, dtFrom.Day);
                    dtTo = new DateTime(dtTo.Year, dtTo.Month, dtTo.Day);
                }
                else
                {
                    // 否则，起始日期为最小日期，截止日期为最大日期
                    dtFrom = new DateTime(2000, 11, 11, 11, 11, 11);
                    dtTo = new DateTime(2020, 11, 11, 11, 11, 11);
                }
            }
        }
        #endregion

        #region 检索发票基本信息
        /// <summary>
        /// QueryInvoiceInformation
        /// </summary>
        public void QueryInvoiceInformation()
        {
            this.frmWait.Show();
            int intReturn = 0;
            // 根据不同的查询类别，执行不同的查询
            switch (this.intQueryType)
            {
                case 1:
                    intReturn = this.QueryInvoiceInfromationByInvoiceNo();
                    break;
                case 2:
                    intReturn = this.QueryInvoiceInfromationByCardNo();
                    break;
                case 3:
                    intReturn = this.QueryInvoiceInfromationByName();
                    break;
            }

            this.SetFormat();

            if (intReturn == -1)
            {
                this.frmWait.Hide();
                return;
            }

            // 显示第一页
            this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet1;

            this.frmWait.Hide();
        }
        #endregion

        #region 按发票号检索发票基本信息
        /// <summary>
        /// 按发票号检索发票基本信息
        /// </summary>
        public int QueryInvoiceInfromationByInvoiceNo()
        {
            // 变量定义
            int intReturn = 0;
            string strCode = "";
            System.Data.DataSet dsResult1 = new DataSet();
            System.Data.DataSet dsResult2 = new DataSet();
            System.Data.DataSet dsResult3 = new DataSet();
            FS.HISFC.BizLogic.Fee.Outpatient outpatient = new FS.HISFC.BizLogic.Fee.Outpatient();

            // 获取检索码
            intReturn = this.GetInput(ref strCode);
            if (intReturn == -1)
            {
                this.frmWait.Hide();
                return -1;
            }

            // 执行查询
            intReturn = outpatient.QueryBalancesByInvoiceNO(strCode, ref dsResult1);
            if (-1 == intReturn)
            {
                MessageBox.Show("获取发票基本信息失败" + outpatient.Err);
                this.frmWait.Hide();
                return -1;
            }
            this.fpSpread1_Sheet1.DataSource = dsResult1;

            // 按发票号查询同时查询发票明细和费用明细
            intReturn = outpatient.QueryBalanceListsByInvoiceNO(strCode, ref dsResult2);
            if (-1 == intReturn)
            {
                MessageBox.Show("获取发票明细失败" + outpatient.Err);
                this.frmWait.Hide();
                return -1;
            }
            this.fpSpread1_Sheet2.DataSource = dsResult2;

            intReturn = outpatient.QueryFeeItemListsByInvoiceNO(strCode, ref dsResult3);
            if (-1 == intReturn)
            {
                MessageBox.Show("获取费用明细失败" + outpatient.Err);
                this.frmWait.Hide();
                return -1;
            }
            this.fpSpread1_Sheet3.DataSource = dsResult3;
            //if (this.fpSpread1_Sheet3.RowCount > 0)
            //{
            //    this.SetSheet3DisplayData();
            //}
            return 1;
        }
        #endregion

        #region 按病历号检索发票基本信息
        /// <summary>
        /// 按病历号检索发票基本信息
        /// </summary>
        public int QueryInvoiceInfromationByCardNo()
        {
            // 变量定义
            int intReturn = 0;
            string strCode = "";
            DateTime dtFrom = DateTime.MinValue;
            DateTime dtTo = DateTime.MaxValue;
            System.Data.DataSet dsResult = new DataSet();
            FS.HISFC.BizLogic.Fee.Outpatient outpatient = new FS.HISFC.BizLogic.Fee.Outpatient();

            // 获取检索码
            intReturn = this.GetInput(ref strCode);
            if (intReturn == -1)
            {
                this.frmWait.Hide();
                return -1;
            }

            // 获取时间范围
            this.GetQueryDate(ref dtFrom, ref dtTo);

            // 执行查询
            intReturn = outpatient.QueryBalancesByCardNO(strCode, dtFrom, dtTo, ref dsResult);
            if (-1 == intReturn)
            {
                this.frmWait.Hide();
                MessageBox.Show("获取发票基本信息失败" + outpatient.Err);
                return -1;
            }

            // 设置查询结果
            this.fpSpread1_Sheet1.DataSource = dsResult;
            this.fpSpread1_Sheet2.DataSource = null;
            this.fpSpread1_Sheet3.DataSource = null;

            return 1;
        }
        #endregion

        #region 按姓名检索发票基本信息
        /// <summary>
        /// 按姓名检索发票基本信息
        /// </summary>
        public int QueryInvoiceInfromationByName()
        {

            // 变量定义
            int intReturn = 0;
            string strCode = "";
            DateTime dtFrom = DateTime.MinValue;
            DateTime dtTo = DateTime.MaxValue;
            System.Data.DataSet dsResult = new DataSet();
            FS.HISFC.BizLogic.Fee.Outpatient outpatient = new FS.HISFC.BizLogic.Fee.Outpatient();

            // 获取检索码
            intReturn = this.GetInput(ref strCode);
            if (intReturn == -1)
            {
                this.frmWait.Hide();
                return -1;
            }

            // 获取时间范围
            this.GetQueryDate(ref dtFrom, ref dtTo);

            // 执行查询
            intReturn = outpatient.QueryBalancesByPatientName(strCode, dtFrom, dtTo, ref dsResult);
            if (-1 == intReturn)
            {
                this.frmWait.Hide();
                MessageBox.Show("获取发票基本信息失败" + outpatient.Err);
                return -1;
            }

            // 设置查询结果
            this.fpSpread1_Sheet1.DataSource = dsResult;
            this.fpSpread1_Sheet2.DataSource = null;
            this.fpSpread1_Sheet3.DataSource = null;

            return 1;
        }
        #endregion

        //#region 设置列的显示宽度
        ///// <summary>
        ///// 设置列的显示宽度
        ///// </summary>
        ///// <param name="intSheet">SHEET索引</param>
        //public void SetSheetWidth(int intSheet)
        //{
        //    // 设置列宽度
        //    for (int i = 0; i < this.fpSpread1.Sheets[intSheet].Columns.Count; i++)
        //    {
        //        this.fpSpread1.Sheets[intSheet].Columns[i].Width = 80;
        //    }

        //    // 设置特殊字段的值：结算类别5、医疗类别8、结算人14、是否体检16、发票状态17、作废操作员19、是否核查21、核查人22、是否已经日结24、日结人25、自费日结记帐28
        //}
        //#endregion

        //#region 设置第三页特殊字段的值
        ///// <summary>
        ///// 
        ///// </summary>
        //public void SetSheet3DisplayData()
        //{
        //    for (int i = 0; i < this.fpSpread1_Sheet3.RowCount; i++)
        //    {
        //        // 变量定义
        //        FS.HISFC.Models.Base.SysClassEnumService enuSysClass = new FS.HISFC.Models.Base.SysClassEnumService();

        //        // 系统类别7
        //        if (this.fpSpread1_Sheet3.Cells[i, 22].Text != "" || this.fpSpread1_Sheet3.Cells[i, 22].Text != null)
        //        {
        //            enuSysClass.ID = this.fpSpread1_Sheet3.Cells[i, 22].Text;
        //            this.fpSpread1_Sheet3.Cells[i, 22].Text = enuSysClass.Name;
        //        }
        //    }
        //}
        //#endregion

        #region 打印
        /// <summary>
        /// 打印
        /// </summary>
        public void PringSheet()
        {
            if (MessageBox.Show("是否打印当前结果页", "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }
            this.fpSpread1.PrintSheet(this.fpSpread1.ActiveSheetIndex);
        }
        #endregion

        #region 导出
        /// <summary>
        /// 导出
        /// </summary>
        public void ExportSheet()
        {
            DialogResult drExport = new DialogResult();
            drExport = this.saveFileDialog1.ShowDialog();
            if (drExport == DialogResult.OK)
            {
                this.fpSpread1.SaveExcel(this.saveFileDialog1.FileName);
            }
        }
        #endregion

        #endregion

        #region 事件

        #region 窗口按键事件
        /// <summary>
        /// 窗口按键事件
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F2)
            {
                // 切换查询方式
                this.ChangeQueryType();
                return true;
            }
            else if (keyData == Keys.F3)
            {
                // 切换数据时间复选框的状态
                this.cbDataDate.Checked = !this.cbDataDate.Checked;
                this.tbInput.Focus();
                return true;
            }
            else if (keyData == Keys.F4)
            {
                // 发票查询
                this.QueryInvoiceInformation();
                return true;
            }
            else if (keyData == Keys.F6)
            {
                // 打印
                this.PringSheet();
                return true;
            }
            else if (keyData == Keys.F7)
            {
                // 导出
                this.ExportSheet();
                return true;
            }
            else if (keyData == Keys.F1)
            {
                // 帮助
                return true;
            }
            else if (keyData == Keys.F12)
            {
                // 退出
                this.FindForm().Close();
                return true;
            }
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.A.GetHashCode())
            {
                // 切换焦点到输入框
                this.tbInput.Focus();
                this.tbInput.SelectAll();
            }
            return base.ProcessDialogKey(keyData);
        }

        #endregion

        #region 日期复选框选中状态改变事件
        /// <summary>
        /// 日期复选框选中状态改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbDataDate_CheckedChanged(object sender, System.EventArgs e)
        {
            this.dtpFromDate.Enabled = this.cbDataDate.Checked;
            this.dtpDateTo.Enabled = this.cbDataDate.Checked;
            if (this.cbDataDate.Checked)
            {
                this.dtpFromDate.Focus();
            }
            else
            {
                this.tbInput.Focus();
            }
        }

        #endregion

        #region 在检索码输入框回车事件
        /// <summary>
        /// 在检索码输入框回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbInput_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.tbInput.Text == "")
                {
                    return;
                }
                this.QueryInvoiceInformation();
            }
        }

        #endregion
        
         void  fpSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
           FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1_Sheet1, FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FeeDetailInvoice.xml");
           FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1_Sheet2, FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FeeDetailInvoiceDetail.xml");
           FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1_Sheet3, FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FeeDetail1.xml");
        }

        #region 双击表格第一页，根据发票号查询发票明细和费用明细
        /// <summary>
        /// 双击表格第一页，根据发票号查询发票明细和费用明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.fpSpread1.ActiveSheet == this.fpSpread1_Sheet1)
            {
                if (this.fpSpread1_Sheet1.RowCount > 0)
                {
                    this.frmWait.Show();
                    // 返回值
                    int intReturn = 0;
                    // 声明变量 = 所选行的发票号
                    string strCode = this.fpSpread1_Sheet1.Cells[e.Row, 0].Text;
                    // 返回的数据源
                    System.Data.DataSet dsResult1 = new DataSet();
                    System.Data.DataSet dsResult2 = new DataSet();
                    // 业务层
                    FS.HISFC.BizLogic.Fee.Outpatient outpatient = new FS.HISFC.BizLogic.Fee.Outpatient();

                    // 查询患者发票明细
                    intReturn = outpatient.QueryBalanceListsByInvoiceNO(strCode, ref dsResult1);
                    if (-1 == intReturn)
                    {
                        this.frmWait.Hide();
                        MessageBox.Show("获取发票明细失败" + outpatient.Err);
                        return;
                    }
                    this.fpSpread1_Sheet2.DataSource = dsResult1;
                    this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet2;
                    // 查询患者费用明细
                    intReturn = outpatient.QueryFeeItemListsByInvoiceNO(strCode, ref dsResult2);
                    if (-1 == intReturn)
                    {
                        this.frmWait.Hide();
                        MessageBox.Show("获取费用明细失败" + outpatient.Err);
                        return;
                    }
                    this.fpSpread1_Sheet3.DataSource = dsResult2;
                    this.SetFormat();
                    //if (this.fpSpread1_Sheet3.RowCount > 0)
                    //{
                    //    this.SetSheet3DisplayData();
                    //}

                    this.frmWait.Hide();
                }
            }
        }

        #endregion

        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("导出", "导出查询信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.J借出, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "导出") 
            {
                this.ExportSheet();
            }
            if (e.ClickedItem.Text == "查询") 
            {
                this.QueryInvoiceInformation();
            }

            if (e.ClickedItem.Text == "打印") 
            {
                this.PringSheet();
            }
            
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryInvoiceInformation();
            
            return base.OnQuery(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            this.ExportSheet();
            return base.Export(sender, neuObject);
        }

        //#endregion

        #region 窗口加载
        /// <summary>
        /// 窗口加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmQueryFeeDetail_Load(object sender, System.EventArgs e)
        {
            FS.HISFC.BizLogic.Fee.Outpatient function = new FS.HISFC.BizLogic.Fee.Outpatient();
            this.dtpFromDate.Value = function.GetDateTimeFromSysDateTime();
            this.dtpDateTo.Value = function.GetDateTimeFromSysDateTime();
            frmWait.Tip = "正在查询数据数据，请等待......";
        }

        #endregion

        #endregion
    }
}