using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.HealthRecord.Search
{
    public partial class frmHistory : Form
    {
        public frmHistory()
        {
            InitializeComponent();
        }
        private void frmHistory_Load(object sender, System.EventArgs e)
        {
            this.fpSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            SetRowHeight();
            #region �����ʾ
            frm.Show();
            frm.Visible = false;
            #endregion
        }
        /// <summary>
        /// �趨���
        /// </summary>
        public void SetRowHeight()
        {
            //�����еĸ�ʽ
            FarPoint.Win.Spread.CellType.TextCellType cel = new FarPoint.Win.Spread.CellType.TextCellType();
            cel.WordWrap = true; //�Զ����� 
            this.fpSpread1_Sheet1.Columns[2].CellType = cel;
            for (int i = 0; i < this.fpSpread1_Sheet1.Rows.Count; i++)
            {
                this.fpSpread1_Sheet1.Rows[i].Height = 39;
            }
            fpSpread1_Sheet1.Columns[0].Width = 60;
            fpSpread1_Sheet1.Columns[1].Width = 50;
            fpSpread1_Sheet1.Columns[2].Width = 390;
            fpSpread1_Sheet1.Columns[3].Width = 50;
            fpSpread1_Sheet1.Columns[4].Width = 60;
            fpSpread1_Sheet1.Columns[5].Visible = false;
        }
        #region  ȫ�ֱ���
        private System.Data.DataTable dt = null;
        private FS.HISFC.BizLogic.HealthRecord.SearchManager sm = new FS.HISFC.BizLogic.HealthRecord.SearchManager();
        private frmShowResult frm = new frmShowResult();
        public bool BoolClose = false;
        #endregion
        /// <summary>
        /// ��ʼ����
        /// </summary>
        public void InitDateTable()
        {
            dt = new DataTable();
            Type strType = typeof(System.String);
            Type intType = typeof(System.Int32);
            Type dtType = typeof(System.DateTime);
            Type boolType = typeof(System.Boolean);

            dt.Columns.AddRange(new DataColumn[]{new DataColumn("����", strType),
													new DataColumn("������", strType),
													new DataColumn("��������", strType),
													new DataColumn("��¼��", strType),
													new DataColumn("����ҽ��", strType),
													new DataColumn("���",strType)});
            this.fpSpread1_Sheet1.DataSource = dt;
        }
        /// <summary>
        /// �������� 
        /// </summary>
        /// <param name="list"></param>
        /// <returns>������ -1 </returns>
        public int AddInfo(ArrayList list)
        {
            try
            {
                if (list == null)
                {
                    return -1;
                }
                if (list.Count == 0)
                {
                    this.dt.Clear();
                }
                FS.FrameWork.Models.NeuObject obj = null;
                for (int i = 0; i < list.Count; i = i + 5)
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = ((FS.FrameWork.Models.NeuObject)list[i]).ID;//����
                    obj.Name = ((FS.FrameWork.Models.NeuObject)list[i + 1]).ID;//������
                    obj.User01 = ((FS.FrameWork.Models.NeuObject)list[i + 2]).ID;//�������� 
                    obj.User02 = ((FS.FrameWork.Models.NeuObject)list[i + 3]).ID;//��¼��
                    obj.User03 = ((FS.FrameWork.Models.NeuObject)list[i + 4]).ID;//����ҽ��
                    string strSequence = ((FS.FrameWork.Models.NeuObject)list[i]).User02;
                    dt.Rows.Add(new object[] { obj.ID, obj.Name, obj.User01, obj.User02, obj.User03, strSequence });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            SetRowHeight();
            return 1;
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            try
            {
                if (this.fpSpread1_Sheet1.Rows.Count == 0)
                {
                    return;
                }
                string Sequence = this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 5].Text;
                string Result = sm.SelectResult(Sequence);
                if (Result != null)
                {
                    this.TopMost = false; //�����ڲ�������ǰ��
                    frm.LBSeacheInfo.Text = this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 2].Text;
                    frm.InpatientNoList = Result;
                    frm.Visible = true;
                    frm.TopMost = true; //��ʾ���ڴ�����ǰ��
                    frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                }
                else
                {
                    MessageBox.Show("��ѯ����" + sm.Err);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmHistory_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!BoolClose)
            {
                this.Visible = false;
                e.Cancel = true;
            }
        }
    }
}