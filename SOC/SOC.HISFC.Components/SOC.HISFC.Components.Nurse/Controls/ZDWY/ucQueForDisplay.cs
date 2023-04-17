using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.Nurse.Controls.ZDWY
{
    public partial class ucQueForDisplay : UserControl
    {
        public ucQueForDisplay(bool isShowName)
        {
            InitializeComponent();
            isDiaplayName = isShowName;
        }

        private FS.HISFC.Models.Nurse.Queue queue = new FS.HISFC.Models.Nurse.Queue();

        private bool isDiaplayName = true;

        /// <summary>
        /// 设置界面字体
        /// </summary>
        /// <param name="fontSize"></param>
        public void SetFontSize(float titleFontSize,float fpFontSize,float bottomFontSize)
        {
            Font titleFont = new System.Drawing.Font("宋体", titleFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.Font = titleFont;
            this.lblRoom.Font = titleFont;

            Font fpFont = new System.Drawing.Font("宋体", fpFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));            
            this.neuSpread1_Sheet1.Rows.Default.Font = fpFont;
            this.neuSpread1_Sheet1.Rows.Default.Height = fpFont.Height + 2;

            Font bottomFont = new System.Drawing.Font("宋体", bottomFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWaitCount.Font = bottomFont;
            
        }

        public FS.HISFC.Models.Nurse.Queue Queue
        {
            get
            {
                return this.queue;
            }
            set
            {
                this.queue = value;
                FS.HISFC.Models.Base.Employee currOper = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                FS.HISFC.BizLogic.Nurse.Assign asignManager = new FS.HISFC.BizLogic.Nurse.Assign();
                this.lblName.Text = queue.Name;
                this.lblRoom.Text = queue.SRoom.Name;
                this.lblWaitCount.Text = "候诊人数：" + queue.WaitingCount.ToString();
                this.neuSpread1_Sheet1.RowCount = 0;
                ArrayList al = asignManager.Query(currOper.Dept.ID, queue.QueueDate.Date, queue.ID, FS.HISFC.Models.Nurse.EnuTriageStatus.In);
                this.AddItem(al, queue);
                al = asignManager.Query(currOper.Dept.ID, queue.QueueDate.Date, queue.ID, FS.HISFC.Models.Nurse.EnuTriageStatus.Triage);
                this.AddItem(al, queue);
                //this.lblName.Text = queue.Name + "（" + queue.Order.ToString() + "）";
            }
        }

        private void AddItem(ArrayList al, FS.HISFC.Models.Nurse.Queue obj)
        {
            if (al != null)
            {
                for (int i = 0; i < al.Count; i++)
                {
                    FS.HISFC.Models.Nurse.Assign assign = al[i] as FS.HISFC.Models.Nurse.Assign;
                    int row = this.neuSpread1_Sheet1.RowCount;
                    this.neuSpread1_Sheet1.AddRows(row, 1);
                    //this.neuSpread1_Sheet1.Cells[row, 0].Text = queue.Order.ToString() + "-" + assign.SeeNO.ToString();
                    this.neuSpread1_Sheet1.Cells[row, 0].Text = assign.SeeNO.ToString();
                    
                    //this.neuSpread1_Sheet1.Cells[row, 1].Text = assign.Register.Name;

                    if (isDiaplayName)
                    {
                        this.neuSpread1_Sheet1.Cells[row, 1].Text = assign.Register.Name;
                    }
                    else
                    {
                        string name = assign.Register.Name;
                        if (assign.Register.Name.Length > 0)
                        {
                            name = assign.Register.Name.Substring(0, 1);
                            for (int ii = 0; ii < assign.Register.Name.Length - 1; ii++)
                            {
                                name += "*";
                            }
                        }

                        this.neuSpread1_Sheet1.Cells[row, 1].Text = name;
                    }


                    if (assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.In)
                    {
                        this.neuSpread1_Sheet1.Cells[row, 2].Text = "请就诊";
                        this.neuSpread1_Sheet1.Cells[row, 0].ForeColor = Color.Lime;
                        this.neuSpread1_Sheet1.Cells[row, 1].ForeColor = Color.Lime;
                        this.neuSpread1_Sheet1.Cells[row, 2].ForeColor = Color.Lime;
                    }
                    else if (assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Triage)
                    {
                        this.neuSpread1_Sheet1.Cells[row, 2].Text = "请等候";
                        this.neuSpread1_Sheet1.Cells[row, 0].ForeColor = Color.Gold;
                        this.neuSpread1_Sheet1.Cells[row, 1].ForeColor = Color.Gold;
                        this.neuSpread1_Sheet1.Cells[row, 2].ForeColor = Color.Gold;
                    }
                }

            }

        }

    }
}
