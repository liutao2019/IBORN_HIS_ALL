using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SOC.Local.Operation
{
    /// <summary>
    /// beijiao LED
    /// </summary>
    public partial class frmOpsLEDShowByDoc : Form
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public frmOpsLEDShowByDoc()
        {
            InitializeComponent();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer2.Tick += new EventHandler(timer2_Tick);
            button1.Click +=new EventHandler(button1_Click);
        }

        int curPageNo = 0;

        int rowCount = 8;

        void timer2_Tick(object sender, EventArgs e)
        {
            if (this.neuOperationSpread_����.Rows.Count > 0)
            {
                for (int rowIndex = 0; rowIndex < this.neuOperationSpread_����.RowCount; rowIndex++)
                {
                    if (rowIndex >= curPageNo * rowCount
                        && rowIndex < (curPageNo + 1) * rowCount)
                    {
                        neuOperationSpread_����.Rows[rowIndex].Visible = true;
                    }
                    else
                    {
                        neuOperationSpread_����.Rows[rowIndex].Visible = false;
                    }
                }

                curPageNo += 1;

                if (curPageNo >= Math.Ceiling((decimal)neuOperationSpread_����.RowCount / rowCount))
                {
                    curPageNo = 0;
                }
            }
        }

        public delegate void CancelButtonClickHandler();
        public CancelButtonClickHandler CancelButtonClickEvent;

        private int timeInterval = 30000;
        public int TimeInterval
        {
            get { return timeInterval; }
            set 
            { 
                timeInterval = value;
                timer1.Interval = value;
            }
        }

        private int timeInterval1 = 15000;
        public int TimeInterval1
        {
            get { return timeInterval1; }
            set
            {
                timeInterval1 = value;
                timer2.Interval = value;
            }
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            QueryData();
        }

        private FS.HISFC.BizLogic.Operation.OpsTableManage opsTableMgr = new FS.HISFC.BizLogic.Operation.OpsTableManage();

        public int Show()
        {
            this.QueryData();
            this.FindForm().Show();
            this.FindForm().TopMost = false;
            return 1;
        }

        private void QueryData()
        {
            this.Clear();
            string strSql = string.Empty;
            if (this.opsTableMgr.Sql.GetSql("Operator.Operator.LED.DocShow", ref strSql) == -1)
            {
                MessageBox.Show("û���ҵ�SQL��䣺");
                return;
            }
            strSql = string.Format(strSql, ((FS.HISFC.Models.Base.Employee)this.opsTableMgr.Operator).Dept.ID, DateTime.Now.ToShortDateString(), (DateTime.Now.AddDays(1)).ToShortDateString());
            DataSet ds = new DataSet();
            this.opsTableMgr.ExecQuery(strSql, ref ds);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                this.neuOperationSpread_����.DataSource = dt;
            }
            this.SetFarpointFormat();
        }

        private void Clear()
        {
            this.neuOperationSpread_����.Rows.Count = 0;
        }

        private void SetFarpointFormat()
        {
            this.nlbDate.Text = "���ڣ�" + DateTime.Now.ToShortDateString();
            this.lblWindow.Location = new Point((1600 - this.lblWindow.Width) / 2, this.lblWindow.Location.Y);
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            textCellType1.WordWrap = true;
            this.neuOperationSpread_����.Columns.Get(0).Width = 62F;//������
            this.neuOperationSpread_����.Columns.Get(1).Width = 35F;//����̨
            this.neuOperationSpread_����.Columns.Get(2).Width = 153F;//����
            this.neuOperationSpread_����.Columns.Get(3).Width = 50F;//����
            this.neuOperationSpread_����.Columns.Get(4).Width = 115F;//����
            this.neuOperationSpread_����.Columns.Get(5).Width = 32F;//�Ա�
            this.neuOperationSpread_����.Columns.Get(6).Width = 116F;//סԺ��
            this.neuOperationSpread_����.Columns.Get(7).Width = 77F;//����
            this.neuOperationSpread_����.Columns.Get(8).Width = 171F;//��ǰ���
            this.neuOperationSpread_����.Columns.Get(9).Width = 211F;//��������
            this.neuOperationSpread_����.Columns.Get(10).Width = 0F;//��Ⱦ����
            this.neuOperationSpread_����.Columns.Get(11).Width = 107F;//����ҽ��
            this.neuOperationSpread_����.Columns.Get(12).Width = 0F;//һ��
            this.neuOperationSpread_����.Columns.Get(13).Width = 107F;//����ҽ��
            this.neuOperationSpread_����.Columns.Get(14).Width = 0F;//����һ��
            this.neuOperationSpread_����.Columns.Get(15).Width = 107F;//����ʽ
            this.neuOperationSpread_����.Columns.Get(16).Width = 107F;//ϴ�ֻ�ʿ
            this.neuOperationSpread_����.Columns.Get(17).Width = 107F;//Ѳ�ػ�ʿ
            this.neuOperationSpread_����.Columns.Get(18).Width = 0F;//����˵��

            this.neuOperationSpread_����.Columns.Get(1).CellType = textCellType1;
            this.neuOperationSpread_����.Columns.Get(2).CellType = textCellType1;
            this.neuOperationSpread_����.Columns.Get(3).CellType = textCellType1;
            this.neuOperationSpread_����.Columns.Get(4).CellType = textCellType1;
            this.neuOperationSpread_����.Columns.Get(5).CellType = textCellType1;
            this.neuOperationSpread_����.Columns.Get(6).CellType = textCellType1;
            this.neuOperationSpread_����.Columns.Get(7).CellType = textCellType1;
            this.neuOperationSpread_����.Columns.Get(8).CellType = textCellType1;
            this.neuOperationSpread_����.Columns.Get(9).CellType = textCellType1;
            this.neuOperationSpread_����.Columns.Get(10).CellType = textCellType1;
            this.neuOperationSpread_����.Columns.Get(11).CellType = textCellType1;
            this.neuOperationSpread_����.Columns.Get(12).CellType = textCellType1;
            this.neuOperationSpread_����.Columns.Get(13).CellType = textCellType1;
            this.neuOperationSpread_����.Columns.Get(14).CellType = textCellType1;
            this.neuOperationSpread_����.Columns.Get(15).CellType = textCellType1;
            this.neuOperationSpread_����.Columns.Get(16).CellType = textCellType1;
            this.neuOperationSpread_����.Columns.Get(17).CellType = textCellType1;
            this.neuOperationSpread_����.Columns.Get(18).CellType = textCellType1;
            if (this.neuOperationSpread_����.Rows.Count > 0)
            {
                for (int rowIndex = 0; rowIndex < this.neuOperationSpread_����.Rows.Count; rowIndex++)
                {
                    if (rowIndex >= rowCount)
                    {
                        this.neuOperationSpread_����.Rows[rowIndex].Visible = false;
                    }
                    this.neuOperationSpread_����.Rows[rowIndex].Height = 110F;
                }
            }
        }

        public int Close()
        {
            this.FindForm().Hide();
            return 1;
        }

        private void frmDisplay_Load(object sender, EventArgs e)
        {
            FS.HISFC.BizProcess.Integrate.Manager controlMgr = new FS.HISFC.BizProcess.Integrate.Manager();

            #region �趨��ʾ��С

            if (Screen.AllScreens.Length > 1)
            {
                if (Screen.AllScreens[0].Primary)
                {
                    this.DesktopBounds = Screen.AllScreens[1].Bounds;
                }
                else
                {
                    this.DesktopBounds = Screen.AllScreens[0].Bounds;
                }
            }
            else
            {
                if (FS.FrameWork.WinForms.Classes.Function.IsManager())
                {
                    this.DesktopBounds = Screen.AllScreens[0].Bounds;
                }
            }
            #endregion
        }

        private void frmDisplay_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.CancelButtonClickEvent();
        }
    }
}
