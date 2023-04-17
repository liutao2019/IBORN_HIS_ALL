using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Manager.Controls
{
    /// <summary>
    /// [��������: �����ѯ�ؼ�]<br></br>
    /// [�� �� ��: dorian]<br0511-03]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��='�̳��Ի���ucMaintenance��ʵ��Spell����'
    ///		�޸�����='{B30340F5-ACAA-4546-B3EB-2E9A52F42F52}'
    ///  />
    /// </summary>
    public partial class ucMaintenance : FS.FrameWork.WinForms.Controls.ucMaintenance
    {
        public ucMaintenance()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.Manager.Spell spellManager = new FS.HISFC.BizLogic.Manager.Spell();

        protected override void fpSpread1_EditModeOff(object sender, EventArgs e)
        {
            base.fpSpread1_EditModeOff(sender, e);

            int columnIndex = this.fpSpread1_Sheet1.ActiveColumnIndex;
            int rowIndex = this.fpSpread1_Sheet1.ActiveRowIndex;

            if (this.fpSpread1_Sheet1.Columns[columnIndex].Label == "����")
            {
                string words = this.fpSpread1_Sheet1.Cells[rowIndex, columnIndex].Text;
                FS.HISFC.Models.Base.ISpell spellInfo = spellManager.Get(words);

                int spellColumn = this.GetColumnIndexFromLabel("ƴ����");
                if (spellColumn != -1)
                {
                    this.fpSpread1_Sheet1.Cells[rowIndex, spellColumn].Text = spellInfo.SpellCode;
                }
                int wbColumn = this.GetColumnIndexFromLabel("�����");
                if (wbColumn != -1)
                {
                    this.fpSpread1_Sheet1.Cells[rowIndex, wbColumn].Text = spellInfo.WBCode;
                }


            }
        }

        /// <summary>
        /// ������������������
        /// </summary>
        /// <param name="label"></param>
        /// <returns>�粻������Ӧ�У��򷵻�-1</returns>
        private int GetColumnIndexFromLabel(string label)
        {
            for (int i = 0; i < this.fpSpread1_Sheet1.Columns.Count; i++)
            {
                if (this.fpSpread1_Sheet1.Columns[i].Label == label)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
