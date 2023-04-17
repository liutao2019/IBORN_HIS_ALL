using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Neusoft.NFC.Object;
using Neusoft.HISFC.Object.Operation;

namespace UFC.Operation
{
    /// <summary>
    /// [��������: �������ţ������Ŵ�ӡ��]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2007-01-05]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucArrangePrint : UserControl, Neusoft.HISFC.Integrate.Operation.IArrangePrint
    {
        public ucArrangePrint()
        {
            InitializeComponent();
            this.neuSpread1_Sheet1.ColumnHeader.Rows[0].Height = 28;
            this.neuSpread1_Sheet1.ColumnHeader.Rows[0].BackColor = Color.White;

        }

#region �ֶ�

        Neusoft.NFC.Interface.Classes.Print print = new Neusoft.NFC.Interface.Classes.Print();
        Neusoft.HISFC.Integrate.Operation.EnumArrangeType arrangeType = Neusoft.HISFC.Integrate.Operation.EnumArrangeType.Anaesthesia;
#endregion

#region ����

        public string Title
        {
            set
            {
                this.neuLabel1.Text = value;
            }
        }
        public DateTime Date
        {
            set
            {
                this.neuLabel2.Text = string.Concat("��ֹ", value.ToString("yyyy-MM-dd hh:mm:ss"));
            }
        }


    
#endregion

        #region IReportPrinter ��Ա

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int Export()
        {
            return 0;
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            return 0;
        }

        /// <summary>
        /// ��ӡԤ��
        /// </summary>
        /// <returns></returns>
        public int PrintPreview()
        {
            return this.print.PrintPreview(10,0,this);
        }

        #endregion


        #region IArrangePrint ��Ա

        /// <summary>
        /// ������뵥
        /// </summary>
        /// <param name="appliction"></param>
        public void AddAppliction(Neusoft.HISFC.Object.Operation.OperationAppllication appliction)
        {
            if (appliction == null)
                return;

            this.neuSpread1_Sheet1.RowCount += 1;
            int i = this.neuSpread1_Sheet1.RowCount - 1;
            this.neuSpread1_Sheet1.Rows[i].Height = 30;
            this.neuSpread1_Sheet1.Rows[i].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.neuSpread1_Sheet1.Cells[i, 0].Text = appliction.PatientInfo.PVisit.PatientLocation.Dept.Name;
            this.neuSpread1_Sheet1.Cells[i, 1].Text = appliction.PatientInfo.PVisit.PatientLocation.Bed.ID;
            this.neuSpread1_Sheet1.Cells[i, 2].Text = appliction.PatientInfo.Name;
            this.neuSpread1_Sheet1.Cells[i, 3].Text = appliction.PatientInfo.Sex.Name;
            this.neuSpread1_Sheet1.Cells[i, 4].Text = appliction.PatientInfo.Age;
            //��ǰ���
            if (appliction.DiagnoseAl.Count > 0)
                this.neuSpread1_Sheet1.Cells[i, 5].Text = (appliction.DiagnoseAl[0] as NeuObject).Name;
            //��������
            this.neuSpread1_Sheet1.Cells[i, 6].Text = appliction.MainOperationName;

            if (this.arrangeType == Neusoft.HISFC.Integrate.Operation.EnumArrangeType.Operation)
            {
                this.neuSpread1_Sheet1.Cells[i, 7].Text = appliction.OperationDoctor.Name;
                this.neuSpread1_Sheet1.Cells[i, 8].Text = appliction.OpsTable.ID;
                //ϴ�ֻ�ʿ
                string nurse = string.Empty;
                foreach (ArrangeRole role in appliction.RoleAl)
                {
                    if (role.RoleType.ID.ToString() == EnumOperationRole.WashingHandNurse.ToString())
                    {
                        nurse += role.Name + "\n";
                    }
                }
                this.neuSpread1_Sheet1.Cells[i, 9].Text = nurse;
                //Ѳ�ػ�ʿ
                nurse = string.Empty;
                foreach (ArrangeRole role in appliction.RoleAl)
                {
                    if (role.RoleType.ID.ToString() == EnumOperationRole.ItinerantNurse.ToString())
                    {
                        nurse += role.Name + "\n";
                    }
                }
                this.neuSpread1_Sheet1.Cells[i, 10].Text = nurse;
            }
            else
            {                
                //��������
                this.neuSpread1_Sheet1.Cells[i, 11].Text = appliction.AnesType.Name;
                //����
                string nurse = string.Empty;
                foreach (ArrangeRole role in appliction.RoleAl)
                {
                    if (role.RoleType.ID.ToString() == EnumOperationRole.Anaesthetist.ToString())
                    {
                        nurse += role.Name + "\n";
                    }
                }
                this.neuSpread1_Sheet1.Cells[i, 12].Text = nurse;
                this.neuSpread1_Sheet1.Cells[i, 13].Text = appliction.OpsTable.Name;   

            }
        }

        /// <summary>
        /// ���
        /// </summary>
        public void Reset()
        {
            this.neuSpread1_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// ��������
        /// </summary>
        public Neusoft.HISFC.Integrate.Operation.EnumArrangeType ArrangeType
        {
            get
            {
                return this.arrangeType;
            }
            set
            {
                this.arrangeType = value;
                if (this.arrangeType == Neusoft.HISFC.Integrate.Operation.EnumArrangeType.Operation)
                {
                    this.neuSpread1_Sheet1.Columns[7].Visible = true;
                    this.neuSpread1_Sheet1.Columns[8].Visible = true;
                    this.neuSpread1_Sheet1.Columns[9].Visible = true;
                    this.neuSpread1_Sheet1.Columns[10].Visible = true;
                    this.neuSpread1_Sheet1.Columns[11].Visible = false;
                    this.neuSpread1_Sheet1.Columns[12].Visible = false;
                    this.neuSpread1_Sheet1.Columns[13].Visible = false;
                    
                }else
                {
                    this.neuSpread1_Sheet1.Columns[7].Visible = false;
                    this.neuSpread1_Sheet1.Columns[8].Visible = false;
                    this.neuSpread1_Sheet1.Columns[9].Visible = false;
                    this.neuSpread1_Sheet1.Columns[10].Visible = false;
                    this.neuSpread1_Sheet1.Columns[11].Visible = true;                    
                    this.neuSpread1_Sheet1.Columns[12].Visible = true;
                    this.neuSpread1_Sheet1.Columns[13].Visible = true;
                }
            }
        }

        #endregion
    }
}
