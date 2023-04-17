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
    public partial class frmOpsLEDShow : Form
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public frmOpsLEDShow()
        {
            InitializeComponent();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer2.Tick += new EventHandler(timer2_Tick);
            button1.Click +=new EventHandler(button1_Click);
        }

        int curPageNo = 0;

        int rowCount = 20;

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
            return 1;
        }

        private void QueryData()
        {
            this.Clear();
            string strSql = string.Empty;
            if(this.opsTableMgr.Sql.GetSql("Operator.Operator.LED.PatientShow",ref strSql) == -1)
            {
                MessageBox.Show("û���ҵ�SQL��䣺");
                return;
            }
            strSql = string.Format(strSql, DateTime.Now,((FS.HISFC.Models.Base.Employee)this.opsTableMgr.Operator).Dept.ID);
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
             this.lblWindow.Location = new Point((this.Width - this.lblWindow.Width) / 2,this.lblWindow.Location.Y);
             this.neuOperationSpread_����.Columns.Get(0).Width = 140F;
             this.neuOperationSpread_����.Columns.Get(1).Width = 278;
             this.neuOperationSpread_����.Columns.Get(2).Width = 85;
             this.neuOperationSpread_����.Columns.Get(3).Width = 126F;
             this.neuOperationSpread_����.Columns.Get(4).Width = 88F;
             this.neuOperationSpread_����.Columns.Get(5).Width = 223F;
             this.neuOperationSpread_����.Columns.Get(6).Width = 290F;
             if (this.neuOperationSpread_����.Rows.Count > 0)
             {
                 for (int index = 0; index < this.neuOperationSpread_����.Rows.Count; index++)
                 {
                     if (index >= rowCount)
                     {
                         this.neuOperationSpread_����.Rows[index].Visible = false;
                     }
                     this.neuOperationSpread_����.Rows[index].Height = 42F;
                     switch (this.neuOperationSpread_����.Cells[index, 6].Text)
                     {
                         case "����":
                             this.neuOperationSpread_����.Rows[index].BackColor = System.Drawing.Color.Blue;
                             break;
                         default:
                             break;
                     }
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
