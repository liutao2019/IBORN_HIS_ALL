using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Preparation
{
    /// <summary>
    /// <br></br>
    /// [��������: �Ƽ���װ]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2008-10]<br></br>
    /// <˵��>
    /// 
    /// </˵��>
    /// </summary>
    public partial class ucDivision : ucPPRManager
    {
        public ucDivision()
        {
            InitializeComponent();
        }

        protected override bool DataValid()
        {
            for (int i = 0; i < this.fsDrug_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.Preparation.Preparation info = this.GetDrugFromFp(i);
                if (info.ConfectQty == 0)
                {
                    MessageBox.Show(info.Drug.Name + "  ���Ʒ������Ϊ ��", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (info.ConfectQty > info.PlanQty)
                {
                    MessageBox.Show(info.Drug.Name + "  ���Ʒ�����ܴ��� �ƻ�������", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            return base.DataValid();
        }
    }
}
