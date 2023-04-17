using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.HealthRecord.Search
{
    /// <summary>
    /// ucNeedCase<br></br>
    /// [��������: ���ﲡ����Ҫ�������˲�ѯ]<br></br>
    /// [�� �� ��: �ſ���]<br></br>
    /// [����ʱ��: 2007-05-9]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucNeedCase : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucNeedCase()
        {
            InitializeComponent();
        }

        #region ��������Ϣ
        private int timeNum = 5000;
        /// <summary>
        /// ���幤��������
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #region ��ʼ��������
        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            //toolBarService.AddToolButton("��ѯ", "��ѯ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            return toolBarService;
        }
        #endregion

        #region ���������Ӱ�ť�����¼�
        /// <summary>
        /// ���������Ӱ�ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                //case "��ѯ":
                //    Search();
                //    break;
                default:
                    break;
            }
        }
        #endregion

        #endregion

        protected override int OnQuery(object sender, object neuObject)
        {
            Search();
            return base.OnQuery(sender, neuObject);
        }
        /// <summary>
        /// ��ѯ
        /// </summary>
        private void Search()
        {
            FS.HISFC.BizLogic.HealthRecord.SearchManager searchMgr = new FS.HISFC.BizLogic.HealthRecord.SearchManager();
            System.Data.DataSet ds = new DataSet();
            if (searchMgr.QueryClinicPatientNeedCase(this.dtBegin.Value, this.dtEnd.Value, ref ds) < 0)
            {
                this.neuSpread1_Sheet1.RowCount = 0;
                MessageBox.Show("��ѯ����");
                return;
            }
            if (ds != null && ds.Tables.Count > 0)
            {
                this.neuSpread1_Sheet1.DataSource = ds;
            }
            else
            {
                this.neuSpread1_Sheet1.RowCount = 0;
            }
        }

        #region ʱ��ı�
        private void dtEnd_ValueChanged(object sender, EventArgs e)
        {
            if (dtEnd.Value < dtBegin.Value)
            {
                dtEnd.Value = dtBegin.Value;
            }
        }

        private void dtBegin_ValueChanged(object sender, EventArgs e)
        {
            if (dtEnd.Value < dtBegin.Value)
            {
                dtBegin.Value = dtEnd.Value;
            }
        }
        #endregion
        private enum EnumCols
        {
            Name, //����
            CardNO,//����
            Sex,//�Ա�
            Dept //���� 
        }
        private void ucNeedCase_Load(object sender, EventArgs e)
        {
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;

            this.timer1.Interval = 5000;
            FarPoint.Win.Spread.CellType.TextCellType txtCellType = new FarPoint.Win.Spread.CellType.TextCellType();
            txtCellType.ReadOnly = true;
            this.neuSpread1_Sheet1.Columns[(int)EnumCols.Name].CellType = txtCellType;
            this.neuSpread1_Sheet1.Columns[(int)EnumCols.CardNO].CellType = txtCellType;
            this.neuSpread1_Sheet1.Columns[(int)EnumCols.Sex].CellType = txtCellType;
            this.neuSpread1_Sheet1.Columns[(int)EnumCols.Dept].CellType = txtCellType; 
        }

        #region �Զ�ˢ��
        private void cbHand_CheckedChanged(object sender, EventArgs e)
        { 
            this.cbAuto.Checked = !cbHand.Checked; 
        }

        private void cbAuto_CheckedChanged(object sender, EventArgs e)
        {
            cbHand.Checked = !cbAuto.Checked;
            if (cbAuto.Checked)
            {
                txtTime.Enabled = true;
            }
            else
            {
                txtTime.Enabled = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(!cbAuto.Checked)
            {
                return ;
            } 
            Search();
        }
        #endregion 

        private void txtTime_TextChanged(object sender, EventArgs e)
        {
            if (FS.FrameWork.Function.NConvert.ToInt32(txtTime.Text) <=0)
            {
                return;
            }
            this.timer1.Interval = FS.FrameWork.Function.NConvert.ToInt32(txtTime.Text) * 1000;
            if (timer1.Interval < 5000)
            {
                timer1.Interval = 5000;
            }
            this.timer1.Enabled = cbAuto.Checked;
        }
    } 
}
