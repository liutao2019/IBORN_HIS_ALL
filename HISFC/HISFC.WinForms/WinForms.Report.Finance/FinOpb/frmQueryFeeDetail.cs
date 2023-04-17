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
            ��Ʊ��,
            ��������,
            �Һ�����,
            ��������,
            �������,
            ��ͬ��λ,
            ���˱��,
            �ɱ������,
            ���ɱ������,
            �Ը����,
            �ܽ��,
            ʵ�����,
            ������,
            ����ʱ��,
            �Ƿ����,
            ��Ʊ״̬,
            ���Ϸ�Ʊ��,
            ���ϲ���Ա,
            ����ʱ��,
            �Ƿ�˲�,
            �˲���,
            �˲�����,
            �Ƿ��Ѿ��ս�,
            �ս���,
            �ս�ʱ��,
            ��Ʊ���,
            �ԷѼ�������,
            ��������        
        }

        enum EnumCol2Set
        { 
            ��Ʊ��,
            ��������,
            ��Ʊ����ˮ��,
            ��Ŀ����,
            �ɱ������,
            ���ɱ������,
            �Ը����,
            �ܽ��,
            ����ʱ��,
            ����Ա,
            �Ƿ��Ѿ��ս�,
            �ս���,
            �ս�ʱ��,
            ��Ʊ״̬
        }

        enum EnumCol3Set
        {
            ��Ʊ��,
            ��Ŀ����,
            ��Ŀ����,
            ����,
            ����,
            ����,
            �������,
            �Ը����,
            �ֽ���,
            �Żݽ��,
            ��Ʊ��Ŀ,
            ����ҽʦ,
            ����ҽʦ����,
            ������,
            ����ʱ��,
            �շ�Ա,
            �շ�����,
            ��Ʊ����ˮ��,
            ��Ʊ״̬,
            �ԷѼ�������,
            ��������,
            ��С����,
            ϵͳ���,
            ������,
            ��ˮ��,
            �Ƿ�ҩƷ,
            ��������,
            ��װ����,
            ҩƷ����,
            ���,
            ����,
            �Ƿ�����ҩ,
            Ƶ��,
            �÷�,
            Ժע����,
            ִ�п���,
            ҽ��������Ŀ����,
            ��Ŀ�ȼ�,
            ����Ŀ����,
            ԭ��Ŀ����,
            �Ƿ���ҩ,
            ��Ϻ�,
            ������Ŀ����,
            ������Ŀ����,
            ҽ���ն�ȷ��,
            �ն�ȷ����,
            �ն�ȷ�Ͽ���,
            �ն�ȷ��ʱ��,
            ȷ������,
            �����,
            ��������,
            �Һ�����,
            �Һſ���
        }

        private int decimals = 2;
        FS.SOC.Public.FarPoint.ColumnSet colSet  = new FS.SOC.Public.FarPoint.ColumnSet();
        FS.SOC.Public.FarPoint.ColumnSet colSet1 = new FS.SOC.Public.FarPoint.ColumnSet();
        FS.SOC.Public.FarPoint.ColumnSet colSet2 = new FS.SOC.Public.FarPoint.ColumnSet();

        private void SetFormat()
        {
            this.colSet.AddColumn(
            new FS.SOC.Public.FarPoint.Column[]{
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.��Ʊ��.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.��������.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.�Һ�����.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.��������.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.�������.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.��ͬ��λ.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.���˱��.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.�ɱ������.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.���ɱ������.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.�Ը����.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.�ܽ��.ToString(),80f,true,false,null),   
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.ʵ�����.ToString(),80f,true,false,null),  
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.������.ToString(),80f,true,false,null),                
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.����ʱ��.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.�Ƿ����.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.��Ʊ״̬.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.���Ϸ�Ʊ��.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.���ϲ���Ա.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.����ʱ��.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.�Ƿ�˲�.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.�˲���.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.�˲�����.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.�Ƿ��Ѿ��ս�.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.�ս���.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.�ս�ʱ��.ToString(),80f,true,false,null),
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.��Ʊ���.ToString(),80f,true,false,null),        
            new FS.SOC.Public.FarPoint.Column(EnumCol1Set.�ԷѼ�������.ToString(),80f,true,false,null),
             new FS.SOC.Public.FarPoint.Column(EnumCol1Set.��������.ToString(),80f,true,false,null),

         }  
                );

           this.colSet1.AddColumn(new FS.SOC.Public.FarPoint.Column[]{
           new FS.SOC.Public.FarPoint.Column(EnumCol2Set.��Ʊ��.ToString(),80f,true,false,null),
           new FS.SOC.Public.FarPoint.Column(EnumCol2Set.��������.ToString(),80f,true,false,null),
           new FS.SOC.Public.FarPoint.Column(EnumCol2Set.��Ʊ����ˮ��.ToString(),80f,true,false,null),
           new FS.SOC.Public.FarPoint.Column(EnumCol2Set.��Ŀ����.ToString(),80f,true,false,null),
           new FS.SOC.Public.FarPoint.Column(EnumCol2Set.�ɱ������.ToString(),80f,true,false,null),
           new FS.SOC.Public.FarPoint.Column(EnumCol2Set.���ɱ������.ToString(),80f,true,false,null),
           new FS.SOC.Public.FarPoint.Column(EnumCol2Set.�Ը����.ToString(),80f,true,false,null),
           new FS.SOC.Public.FarPoint.Column(EnumCol2Set.�ܽ��.ToString(),80f,true,false,null),
           new FS.SOC.Public.FarPoint.Column(EnumCol2Set.����ʱ��.ToString(),80f,true,false,null),
           new FS.SOC.Public.FarPoint.Column(EnumCol2Set.����Ա.ToString(),80f,true,false,null),
           new FS.SOC.Public.FarPoint.Column(EnumCol2Set.�Ƿ��Ѿ��ս�.ToString(),80f,true,false,null),
           new FS.SOC.Public.FarPoint.Column(EnumCol2Set.�ս���.ToString(),80f,true,false,null),
           new FS.SOC.Public.FarPoint.Column(EnumCol2Set.�ս�ʱ��.ToString(),80f,true,false,null),
           new FS.SOC.Public.FarPoint.Column(EnumCol2Set.��Ʊ״̬.ToString(),80f,true,false,null)            
            }
            );
            
          this.colSet2.AddColumn(
          new FS.SOC.Public.FarPoint.Column[]{
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.��Ʊ��.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.��Ŀ����.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.��Ŀ����.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.����.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.����.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.����.ToString(),80f,true,false,null),           
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.�������.ToString(),80f,true,false,null),     
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.�Ը����.ToString(),80f,true,false,null),     
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.�ֽ���.ToString(),80f,true,false,null),     
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.�Żݽ��.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.��Ʊ��Ŀ.ToString(),80f,true,false,null),     
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.����ҽʦ.ToString(),80f,true,false,null),     
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.����ҽʦ����.ToString(),80f,true,false,null),    
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.������.ToString(),80f,true,false,null),            new FS.SOC.Public.FarPoint.Column(EnumCol3Set.����ʱ��.ToString(),80f,true,false,null),    
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.�շ�Ա.ToString(),80f,true,false,null),            new FS.SOC.Public.FarPoint.Column(EnumCol3Set.�շ�����.ToString(),80f,true,false,null),    
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.��Ʊ����ˮ��.ToString(),80f,true,false,null),    
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.��Ʊ״̬.ToString(),80f,true,false,null),    
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.�ԷѼ�������.ToString(),80f,true,false,null),    
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.��������.ToString(),80f,true,false,null),    
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.��Ʊ״̬.ToString(),80f,true,false,null),    
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.ϵͳ���.ToString(),80f,true,false,null),    
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.������.ToString(),80f,true,false,null),           
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.��ˮ��.ToString(),80f,true,false,null),             
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.�Ƿ�ҩƷ.ToString(),80f,true,false,null),                 
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.��������.ToString(),80f,true,false,null),    
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.��װ����.ToString(),80f,true,false,null),   
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.ҩƷ����.ToString(),80f,true,false,null),   
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.���.ToString(),80f,true,false,null),   
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.����.ToString(),80f,true,false,null),   
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.�Ƿ�����ҩ.ToString(),80f,true,false,null),   
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.Ƶ��.ToString(),80f,true,false,null),   
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.�÷�.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.Ժע����.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.ִ�п���.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.ҽ��������Ŀ����.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.��Ŀ�ȼ�.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.����Ŀ����.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.ԭ��Ŀ����.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.�Ƿ���ҩ.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.��Ϻ�.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.������Ŀ����.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.ҽ���ն�ȷ��.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.�ն�ȷ����.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.�ն�ȷ�Ͽ���.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.�ն�ȷ��ʱ��.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.ȷ������.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.�����.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.��������.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.�Һ�����.ToString(),80f,true,false,null),
          new FS.SOC.Public.FarPoint.Column(EnumCol3Set.�Һſ���.ToString(),80f,true,false,null),            
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
             if(colIndex != (int)EnumCol1Set.�ɱ������ && colIndex != (int)EnumCol1Set.���ɱ������ && colIndex != (int)EnumCol1Set.�Ը���� && colIndex != (int)EnumCol1Set.�ܽ�� && colIndex != (int)EnumCol1Set.ʵ�����)
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
                    if (colIndex != (int)EnumCol2Set.�ɱ������ && colIndex != (int)EnumCol1Set.���ɱ������ && colIndex != (int)EnumCol1Set.�Ը���� && colIndex != (int)EnumCol1Set.�ܽ�� )
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
                    if (colIndex != (int)EnumCol3Set.���� && colIndex != (int)EnumCol3Set.���� && colIndex != (int)EnumCol3Set.���� && colIndex != (int)EnumCol3Set.������� && colIndex != (int)EnumCol3Set.�Ը���� && colIndex != (int)EnumCol3Set.�ֽ��� && colIndex != (int)EnumCol3Set.�Żݽ��)
                    {
                        this.fpSpread1_Sheet3.Columns[colIndex].CellType = t;
                    }

                }
            }
        
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numberCellType.DecimalPlaces = this.decimals;
            numberCellType.ReadOnly = true;
            this.fpSpread1_Sheet1.Columns[(int)EnumCol1Set.�ɱ������].CellType = numberCellType;
            this.fpSpread1_Sheet1.Columns[(int)EnumCol1Set.���ɱ������].CellType = numberCellType;
            this.fpSpread1_Sheet1.Columns[(int)EnumCol1Set.�Ը����].CellType = numberCellType;
            this.fpSpread1_Sheet1.Columns[(int)EnumCol1Set.�ܽ��].CellType = numberCellType;
            this.fpSpread1_Sheet1.Columns[(int)EnumCol1Set.ʵ�����].CellType = numberCellType;
            this.fpSpread1_Sheet2.Columns[(int)EnumCol2Set.�ɱ������].CellType = numberCellType;
            this.fpSpread1_Sheet2.Columns[(int)EnumCol2Set.�Ը����].CellType = numberCellType;
            this.fpSpread1_Sheet2.Columns[(int)EnumCol2Set.�ܽ��].CellType = numberCellType;
            this.fpSpread1_Sheet3.Columns[(int)EnumCol3Set.����].CellType = numberCellType;
            this.fpSpread1_Sheet3.Columns[(int)EnumCol3Set.����].CellType = numberCellType;
            this.fpSpread1_Sheet3.Columns[(int)EnumCol3Set.����].CellType = numberCellType;
            this.fpSpread1_Sheet3.Columns[(int)EnumCol3Set.�������].CellType = numberCellType;
            this.fpSpread1_Sheet3.Columns[(int)EnumCol3Set.�Ը����].CellType = numberCellType;
            this.fpSpread1_Sheet3.Columns[(int)EnumCol3Set.�ֽ���].CellType = numberCellType;
            this.fpSpread1_Sheet3.Columns[(int)EnumCol3Set.�Żݽ��].CellType = numberCellType;
            this.fpSpread1.ColumnWidthChanged -= new  FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpSpread1_ColumnWidthChanged);
            this.fpSpread1.ColumnWidthChanged += new  FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpSpread1_ColumnWidthChanged);
        }
        

        #region ��������
        /// <summary>
        /// ��ѯ����
        /// </summary>
        int intQueryType = 1;

        FS.FrameWork.WinForms.Forms.frmWait frmWait = new FS.FrameWork.WinForms.Forms.frmWait();

        #endregion

        #region ����

        #region �л�������ʽ
        /// <summary>
        /// �л�������ʽ
        /// </summary>
        public void ChangeQueryType()
        {
            // ת����ѯ����
            if (intQueryType == 3)
            {
                intQueryType = 1;
            }
            else
            {
                intQueryType++;
            }

            // ���ò�ѯ������ʾ�ı�
            switch (this.intQueryType)
            {
                case 1:
                    this.labQueryName.Text = "��Ʊ��(F2�л�)";
                    break;
                case 2:
                    this.labQueryName.Text = "������(F2�л�)";
                    break;
                case 3:
                    this.labQueryName.Text = "��  ��(F2�л�)";
                    break;
            }

            // ���ý��㵽�����
            this.tbInput.Focus();
            this.tbInput.SelectAll();

            // ����ʱ�临ѡ��
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

        #region ��ȡ������(1���ɹ�/-1��ʧ��)
        /// <summary>
        /// ��ȡ������(1���ɹ�/-1��ʧ��)
        /// </summary>
        /// <param name="strCode">���صļ�����</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int GetInput(ref string strCode)
        {
            strCode = FS.FrameWork.Public.String.TakeOffSpecialChar(this.tbInput.Text.Trim());

            // �жϺϷ���
            if (this.intQueryType == 2)
            {
                try
                {
                    long.Parse(strCode);
                }
                catch
                {
                    MessageBox.Show("����Ĳ������ű�����������ʽ,�볢���÷�Ʊ�Ų�ѯ");
                    this.tbInput.Text = "";
                    this.tbInput.Focus();
                    return -1;
                }
            }

            #region {571171C3-2CF8-4edc-9403-0E5E2B424A26}
            // �жϺϷ���
            if (this.intQueryType == 3)
            {
                strCode = FS.FrameWork.Public.String.TakeOffSpecialChar(strCode);
                if (strCode == "" || strCode == null) { return  -1; }
            }
            #endregion
            // ������
            switch (this.intQueryType)
            {
                case 1:
                    // ���շ�Ʊ�Ų�ѯ��12λ
                    strCode = strCode.PadLeft(12, '0');
                    break;
                case 2:
                    // �������ţ�10λ
                    strCode = strCode.PadLeft(10, '0');
                    break;
            }
            this.tbInput.Text = strCode;

            return 1;
        }
        #endregion

        #region ��ȡ��ѯ����
        /// <summary>
        /// ��ȡ��ѯ����
        /// </summary>
        /// <param name="dtFrom">���ص���ʼ����</param>
        /// <param name="dtTo">���صĽ�ֹ����</param>
        public void GetQueryDate(ref DateTime dtFrom, ref DateTime dtTo)
        {
            // ��������շ�Ʊ�Ų�ѯ���ſ��Դ�ʱ���������Ϊ��Ʊ����Ψһ��
            if (this.intQueryType != 1)
            {
                // ���ʱ��ѡ��ѡ�У�������Чѡ������
                if (this.cbDataDate.Checked)
                {
                    dtFrom = this.dtpFromDate.Value;
                    dtTo = this.dtpDateTo.Value;

                    dtFrom = new DateTime(dtFrom.Year, dtFrom.Month, dtFrom.Day);
                    dtTo = new DateTime(dtTo.Year, dtTo.Month, dtTo.Day);
                }
                else
                {
                    // ������ʼ����Ϊ��С���ڣ���ֹ����Ϊ�������
                    dtFrom = new DateTime(2000, 11, 11, 11, 11, 11);
                    dtTo = new DateTime(2020, 11, 11, 11, 11, 11);
                }
            }
        }
        #endregion

        #region ������Ʊ������Ϣ
        /// <summary>
        /// QueryInvoiceInformation
        /// </summary>
        public void QueryInvoiceInformation()
        {
            this.frmWait.Show();
            int intReturn = 0;
            // ���ݲ�ͬ�Ĳ�ѯ���ִ�в�ͬ�Ĳ�ѯ
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

            // ��ʾ��һҳ
            this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet1;

            this.frmWait.Hide();
        }
        #endregion

        #region ����Ʊ�ż�����Ʊ������Ϣ
        /// <summary>
        /// ����Ʊ�ż�����Ʊ������Ϣ
        /// </summary>
        public int QueryInvoiceInfromationByInvoiceNo()
        {
            // ��������
            int intReturn = 0;
            string strCode = "";
            System.Data.DataSet dsResult1 = new DataSet();
            System.Data.DataSet dsResult2 = new DataSet();
            System.Data.DataSet dsResult3 = new DataSet();
            FS.HISFC.BizLogic.Fee.Outpatient outpatient = new FS.HISFC.BizLogic.Fee.Outpatient();

            // ��ȡ������
            intReturn = this.GetInput(ref strCode);
            if (intReturn == -1)
            {
                this.frmWait.Hide();
                return -1;
            }

            // ִ�в�ѯ
            intReturn = outpatient.QueryBalancesByInvoiceNO(strCode, ref dsResult1);
            if (-1 == intReturn)
            {
                MessageBox.Show("��ȡ��Ʊ������Ϣʧ��" + outpatient.Err);
                this.frmWait.Hide();
                return -1;
            }
            this.fpSpread1_Sheet1.DataSource = dsResult1;

            // ����Ʊ�Ų�ѯͬʱ��ѯ��Ʊ��ϸ�ͷ�����ϸ
            intReturn = outpatient.QueryBalanceListsByInvoiceNO(strCode, ref dsResult2);
            if (-1 == intReturn)
            {
                MessageBox.Show("��ȡ��Ʊ��ϸʧ��" + outpatient.Err);
                this.frmWait.Hide();
                return -1;
            }
            this.fpSpread1_Sheet2.DataSource = dsResult2;

            intReturn = outpatient.QueryFeeItemListsByInvoiceNO(strCode, ref dsResult3);
            if (-1 == intReturn)
            {
                MessageBox.Show("��ȡ������ϸʧ��" + outpatient.Err);
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

        #region �������ż�����Ʊ������Ϣ
        /// <summary>
        /// �������ż�����Ʊ������Ϣ
        /// </summary>
        public int QueryInvoiceInfromationByCardNo()
        {
            // ��������
            int intReturn = 0;
            string strCode = "";
            DateTime dtFrom = DateTime.MinValue;
            DateTime dtTo = DateTime.MaxValue;
            System.Data.DataSet dsResult = new DataSet();
            FS.HISFC.BizLogic.Fee.Outpatient outpatient = new FS.HISFC.BizLogic.Fee.Outpatient();

            // ��ȡ������
            intReturn = this.GetInput(ref strCode);
            if (intReturn == -1)
            {
                this.frmWait.Hide();
                return -1;
            }

            // ��ȡʱ�䷶Χ
            this.GetQueryDate(ref dtFrom, ref dtTo);

            // ִ�в�ѯ
            intReturn = outpatient.QueryBalancesByCardNO(strCode, dtFrom, dtTo, ref dsResult);
            if (-1 == intReturn)
            {
                this.frmWait.Hide();
                MessageBox.Show("��ȡ��Ʊ������Ϣʧ��" + outpatient.Err);
                return -1;
            }

            // ���ò�ѯ���
            this.fpSpread1_Sheet1.DataSource = dsResult;
            this.fpSpread1_Sheet2.DataSource = null;
            this.fpSpread1_Sheet3.DataSource = null;

            return 1;
        }
        #endregion

        #region ������������Ʊ������Ϣ
        /// <summary>
        /// ������������Ʊ������Ϣ
        /// </summary>
        public int QueryInvoiceInfromationByName()
        {

            // ��������
            int intReturn = 0;
            string strCode = "";
            DateTime dtFrom = DateTime.MinValue;
            DateTime dtTo = DateTime.MaxValue;
            System.Data.DataSet dsResult = new DataSet();
            FS.HISFC.BizLogic.Fee.Outpatient outpatient = new FS.HISFC.BizLogic.Fee.Outpatient();

            // ��ȡ������
            intReturn = this.GetInput(ref strCode);
            if (intReturn == -1)
            {
                this.frmWait.Hide();
                return -1;
            }

            // ��ȡʱ�䷶Χ
            this.GetQueryDate(ref dtFrom, ref dtTo);

            // ִ�в�ѯ
            intReturn = outpatient.QueryBalancesByPatientName(strCode, dtFrom, dtTo, ref dsResult);
            if (-1 == intReturn)
            {
                this.frmWait.Hide();
                MessageBox.Show("��ȡ��Ʊ������Ϣʧ��" + outpatient.Err);
                return -1;
            }

            // ���ò�ѯ���
            this.fpSpread1_Sheet1.DataSource = dsResult;
            this.fpSpread1_Sheet2.DataSource = null;
            this.fpSpread1_Sheet3.DataSource = null;

            return 1;
        }
        #endregion

        //#region �����е���ʾ���
        ///// <summary>
        ///// �����е���ʾ���
        ///// </summary>
        ///// <param name="intSheet">SHEET����</param>
        //public void SetSheetWidth(int intSheet)
        //{
        //    // �����п��
        //    for (int i = 0; i < this.fpSpread1.Sheets[intSheet].Columns.Count; i++)
        //    {
        //        this.fpSpread1.Sheets[intSheet].Columns[i].Width = 80;
        //    }

        //    // ���������ֶε�ֵ���������5��ҽ�����8��������14���Ƿ����16����Ʊ״̬17�����ϲ���Ա19���Ƿ�˲�21���˲���22���Ƿ��Ѿ��ս�24���ս���25���Է��ս����28
        //}
        //#endregion

        //#region ���õ���ҳ�����ֶε�ֵ
        ///// <summary>
        ///// 
        ///// </summary>
        //public void SetSheet3DisplayData()
        //{
        //    for (int i = 0; i < this.fpSpread1_Sheet3.RowCount; i++)
        //    {
        //        // ��������
        //        FS.HISFC.Models.Base.SysClassEnumService enuSysClass = new FS.HISFC.Models.Base.SysClassEnumService();

        //        // ϵͳ���7
        //        if (this.fpSpread1_Sheet3.Cells[i, 22].Text != "" || this.fpSpread1_Sheet3.Cells[i, 22].Text != null)
        //        {
        //            enuSysClass.ID = this.fpSpread1_Sheet3.Cells[i, 22].Text;
        //            this.fpSpread1_Sheet3.Cells[i, 22].Text = enuSysClass.Name;
        //        }
        //    }
        //}
        //#endregion

        #region ��ӡ
        /// <summary>
        /// ��ӡ
        /// </summary>
        public void PringSheet()
        {
            if (MessageBox.Show("�Ƿ��ӡ��ǰ���ҳ", "��ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }
            this.fpSpread1.PrintSheet(this.fpSpread1.ActiveSheetIndex);
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
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

        #region �¼�

        #region ���ڰ����¼�
        /// <summary>
        /// ���ڰ����¼�
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F2)
            {
                // �л���ѯ��ʽ
                this.ChangeQueryType();
                return true;
            }
            else if (keyData == Keys.F3)
            {
                // �л�����ʱ�临ѡ���״̬
                this.cbDataDate.Checked = !this.cbDataDate.Checked;
                this.tbInput.Focus();
                return true;
            }
            else if (keyData == Keys.F4)
            {
                // ��Ʊ��ѯ
                this.QueryInvoiceInformation();
                return true;
            }
            else if (keyData == Keys.F6)
            {
                // ��ӡ
                this.PringSheet();
                return true;
            }
            else if (keyData == Keys.F7)
            {
                // ����
                this.ExportSheet();
                return true;
            }
            else if (keyData == Keys.F1)
            {
                // ����
                return true;
            }
            else if (keyData == Keys.F12)
            {
                // �˳�
                this.FindForm().Close();
                return true;
            }
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.A.GetHashCode())
            {
                // �л����㵽�����
                this.tbInput.Focus();
                this.tbInput.SelectAll();
            }
            return base.ProcessDialogKey(keyData);
        }

        #endregion

        #region ���ڸ�ѡ��ѡ��״̬�ı��¼�
        /// <summary>
        /// ���ڸ�ѡ��ѡ��״̬�ı��¼�
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

        #region �ڼ����������س��¼�
        /// <summary>
        /// �ڼ����������س��¼�
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

        #region ˫������һҳ�����ݷ�Ʊ�Ų�ѯ��Ʊ��ϸ�ͷ�����ϸ
        /// <summary>
        /// ˫������һҳ�����ݷ�Ʊ�Ų�ѯ��Ʊ��ϸ�ͷ�����ϸ
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
                    // ����ֵ
                    int intReturn = 0;
                    // �������� = ��ѡ�еķ�Ʊ��
                    string strCode = this.fpSpread1_Sheet1.Cells[e.Row, 0].Text;
                    // ���ص�����Դ
                    System.Data.DataSet dsResult1 = new DataSet();
                    System.Data.DataSet dsResult2 = new DataSet();
                    // ҵ���
                    FS.HISFC.BizLogic.Fee.Outpatient outpatient = new FS.HISFC.BizLogic.Fee.Outpatient();

                    // ��ѯ���߷�Ʊ��ϸ
                    intReturn = outpatient.QueryBalanceListsByInvoiceNO(strCode, ref dsResult1);
                    if (-1 == intReturn)
                    {
                        this.frmWait.Hide();
                        MessageBox.Show("��ȡ��Ʊ��ϸʧ��" + outpatient.Err);
                        return;
                    }
                    this.fpSpread1_Sheet2.DataSource = dsResult1;
                    this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet2;
                    // ��ѯ���߷�����ϸ
                    intReturn = outpatient.QueryFeeItemListsByInvoiceNO(strCode, ref dsResult2);
                    if (-1 == intReturn)
                    {
                        this.frmWait.Hide();
                        MessageBox.Show("��ȡ������ϸʧ��" + outpatient.Err);
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
            toolBarService.AddToolButton("����", "������ѯ��Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.J���, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "����") 
            {
                this.ExportSheet();
            }
            if (e.ClickedItem.Text == "��ѯ") 
            {
                this.QueryInvoiceInformation();
            }

            if (e.ClickedItem.Text == "��ӡ") 
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

        #region ���ڼ���
        /// <summary>
        /// ���ڼ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmQueryFeeDetail_Load(object sender, System.EventArgs e)
        {
            FS.HISFC.BizLogic.Fee.Outpatient function = new FS.HISFC.BizLogic.Fee.Outpatient();
            this.dtpFromDate.Value = function.GetDateTimeFromSysDateTime();
            this.dtpDateTo.Value = function.GetDateTimeFromSysDateTime();
            frmWait.Tip = "���ڲ�ѯ�������ݣ���ȴ�......";
        }

        #endregion

        #endregion
    }
}