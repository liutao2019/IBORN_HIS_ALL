using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Preparation.Preparation
{
    /// <summary>
    /// <br></br>
    /// [��������: �Ƽ�����]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2008-10]<br></br>
    /// <˵��>
    /// 
    /// </˵��>
    /// </summary>
    public partial class ucConfect : ucPPRManager
    {
        public ucConfect()
        {
            InitializeComponent();
        }

        protected override bool DataValid()
        {           
            return base.DataValid();
        }

        protected override void SetFormat()
        {
            switch (this.SaveState)
            {
                case FS.HISFC.Models.Preparation.EnumState.Confect:

                    #region Fp��ʽ��

                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColAssayResult].Visible = false;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColBatchNO].Visible = false;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColValidDate].Visible = false;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColConfectQty].Visible = true;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColInQty].Visible = false;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPlanNum].Locked = true;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColCostPrice].Visible = false;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColConfectQty].Visible = false;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColAssayQty].Visible = false;

                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColDrugName].Width = 160;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColSpecs].Width = 90;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPackQty].Width = 75;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPackUnit].Width = 75;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPlanNum].Width = 90;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPlanUnit].Width = 60;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColMemo].Width = 180;

                    #endregion

                    break;
            }
        }
    }
}
