using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;

namespace FS.HISFC.Components.Manager.Controls
{
    /// <summary>
    /// [��������: ����ά���Ӵ��ڣ������Զ�����ƴ����������]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2007-04-16]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucMaintenanceConstSub : FS.FrameWork.WinForms.Controls.ucMaintenance
    {
        public ucMaintenanceConstSub()
            :base("const")
        {
            InitializeComponent();
            //this.HideFilter();
        }
        private FS.HISFC.BizLogic.Manager.Spell spellManager = new FS.HISFC.BizLogic.Manager.Spell();

        protected override string SQL
        {
            get
            {
                return base.SQL + string.Format(" where TYPE='{0}' order by to_number(Pkg_Common.GetNumber(code)),Pkg_Common.GetStr(code)", ((Control)this).Text);
            }
        }

        protected override string GetDefaultValue(string fieldName)
        {
            if(fieldName=="TYPE")
            {
                return ((Control)this).Text;
            }else
            {
                return base.GetDefaultValue(fieldName);
            }
        }
        private FarPoint.Win.Spread.Column GetColumnByName(string name)
        {
            foreach (Column column in this.fpSpread1_Sheet1.Columns)
            {
                if (column.Label == name)
                    return column;
            }

            return null;
        }
        protected override void fpSpread1_Sheet1_CellChanged(object sender, FarPoint.Win.Spread.SheetViewEventArgs e)
        {
            base.fpSpread1_Sheet1_CellChanged(sender, e);

            //���û��װ��������ɣ���������
            if (!this.isDataLoaded)
                return;

            //����ı������ƣ���ƴ���롢������Զ������仯
            //{1B10BCB7-8133-4282-8479-9C41FE5A23FD} ��������ת��
            if (this.fpSpread1_Sheet1.Columns[e.Column].Label == FS.FrameWork.Management.Language.Msg("����"))
            {
                //{1B10BCB7-8133-4282-8479-9C41FE5A23FD} ��������ת��
                Column column = this.GetColumnByName( FS.FrameWork.Management.Language.Msg( "ƴ����" ) );
                if (column != null /*&& this.fpSpread1_Sheet1.Cells[e.Row,column.Index].Text.Length==0*/)
                {
                    this.fpSpread1_Sheet1.Cells[e.Row, column.Index].Text = FS.FrameWork.Public.String.GetSpell(this.fpSpread1_Sheet1.Cells[e.Row, e.Column].Text);
                }
                //{1B10BCB7-8133-4282-8479-9C41FE5A23FD} ��������ת��
                column = this.GetColumnByName( FS.FrameWork.Management.Language.Msg( "�����" ) );
                if(column != null)
                {
                    FS.HISFC.Models.Base.ISpell spCode = this.spellManager.Get(this.fpSpread1_Sheet1.Cells[e.Row, e.Column].Text);
                    if(spCode != null)
                        this.fpSpread1_Sheet1.Cells[e.Row, column.Index].Text = spCode.WBCode;
                }
            }
        }
    }
}
