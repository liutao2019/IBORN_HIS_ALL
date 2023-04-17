using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Material.Report
{
    public partial class ucApplyHistory : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucApplyHistory()
        {
            InitializeComponent();
        }
        
        private string Dept = "";
        private string Item = "";
        private bool IsInit = false;
        public string reportParm = "";	//����ͳ������
        public DateTime myBeginDate;	//��ʼʱ��
        public DateTime myEndDate;		//����ʱ��
        private string filePath = "";   //��ʽ�����ļ���ַ
        public DataSet myDataSet = new DataSet();
        public DataView myDataView;
        
        private FS.HISFC.BizLogic.Material.MetItem itemMgr = new FS.HISFC.BizLogic.Material.MetItem();

        /// <summary>
        /// ҳ�߾� Ĭ�� 50
        /// </summary>
        private int top = 10;

        /// <summary>
        /// ҳ�߾� Ĭ�� 50
        /// </summary>
        public int TableTop
        {
            set
            {
                this.top = value;
            }
        }

        /// <summary>
        /// ҳ�߾� Ĭ�� 50
        /// </summary>
        private int left = 10;

        /// <summary>
        /// ҳ�߾� Ĭ�� 50
        /// </summary>
        public int TableLeft
        {
            set
            {
                this.left = value;
            }
        }

        /// <summary>
        /// tabPage��ʼ��
        /// </summary>
        public virtual void InitTabPage()
        {
            //Ĭ�ϲ���ʾ�ڶ���tabҳ.�����Ҫ����overridate�˷���.
            this.tabControl1.TabPages.Remove(this.tabPage2);
        }

        /// <summary>
        /// �������ݵ�һ��tabҳ����ʾ��ʽ��ʾ��ʽ
        /// </summary>
        public virtual void SetFormat()
        {
            //������������ļ�������������ļ��е���ʽ������ΪDataSet��Ĭ����ʽ
            if (System.IO.File.Exists(this.filePath))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuSpread1_Sheet1, this.filePath);
            }
            else
            {
                this.neuSpread1_Sheet1.Columns[0].Width = 70F;//
                this.neuSpread1_Sheet1.Columns[1].Width = 140F;
                this.neuSpread1_Sheet1.Columns[2].Width = 70F;
                this.neuSpread1_Sheet1.Columns[3].Width = 60F;
                this.neuSpread1_Sheet1.Columns[4].Width = 60F;
                this.neuSpread1_Sheet1.Columns[5].Width = 50F;
                this.neuSpread1_Sheet1.Columns[6].Width = 70F;
                this.neuSpread1_Sheet1.Columns[7].Width = 70F;
            }
        }

        /// <summary>
        /// ��Ӻϼ���
        /// </summary>
        /// <param name="iTextIndex">"�ϼƣ�"��������</param>
        /// <param name="iIndex">�����ϼƵ�������</param>
        public void SetSum(int iTextIndex, params int[] iIndex)
        {
            int iRowIndex = this.neuSpread1_Sheet1.Rows.Count;
            this.neuSpread1_Sheet1.Rows.Add(iRowIndex, 1);
            this.neuSpread1_Sheet1.Cells[iRowIndex, iTextIndex].Text = "�ϼƣ�";
            if (iRowIndex == 0)
                return;
            for (int i = 0; i < iIndex.Length; i++)
            {
                if (iIndex[i] >= this.neuSpread1_Sheet1.Columns.Count)
                    continue;
                this.neuSpread1_Sheet1.Cells[iRowIndex, iIndex[i]].Formula = "SUM(" + (char)(65 + iIndex[i]) + "1:" + (char)(65 + iIndex[i]) + iRowIndex.ToString() + ")";
            }

        }

        private int iTextIndex = 0;

        private int[] iIndex = null;

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="type">��ʼ������</param>
        /// <param name="dept">���Ҵ���</param>
        /// <param name="item">���ʴ���</param>
        /// <param name="IsQuery">�Ƿ�ֱ�Ӳ�ѯ</param>
        public void Init(string type, FS.FrameWork.Models.NeuObject dept, FS.FrameWork.Models.NeuObject item, bool IsQuery)
        {
            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
            this.neuSpread1_Sheet1.SetColumnAllowAutoSort(-1, true);

            if (dept == null)
            {
                this.cmbDept.Enabled = true;
                ArrayList al = new ArrayList();
                al = deptMgr.GetDeptmentAll();

                if (al != null || al.Count > 0)
                {
                    this.cmbDept.ClearItems();
                    this.cmbDept.AddItems(al);
                }

            }
            else
            {
                ArrayList al = new ArrayList();
                al.Add(dept);
                this.cmbDept.AddItems(al);
                this.cmbDept.SelectedIndex = 0;
                this.cmbDept.Tag = dept.ID;
                this.cmbDept.Text = dept.Name;
                this.Dept = dept.ID;
                this.cmbDept.Visible = false;
                this.label10.Visible = false;
            }


            if (item == null)
            {
                this.cmbItem.Enabled = true;
                List<FS.HISFC.Models.Material.MaterialItem> alItem = new List<FS.HISFC.Models.Material.MaterialItem>();
                alItem = this.itemMgr.GetMetItemList();
                if (alItem != null && alItem.Count > 0)
                {
                    this.cmbItem.ClearItems();
                    this.cmbItem.AddItems(new ArrayList(alItem.ToArray()));
                }
            }
            else
            {
                ArrayList al = new ArrayList();
                al.Add(item);
                this.cmbItem.AddItems(al);
                this.cmbItem.SelectedIndex = 0;
                this.Item = item.ID;
                this.cmbItem.Text = item.Name;
                this.cmbItem.Visible = false;
                this.label10.Visible = false;
            }

            this.dtpBeginDate.Value = this.itemMgr.GetDateTimeFromSysDateTime().AddMonths(-1);
            this.dtpEndDate.Value = this.itemMgr.GetDateTimeFromSysDateTime();

            if (IsQuery)
            {
                this.Query();
            }
        }

        /// <summary>
        /// ������ѯ���
        /// </summary>
        public virtual void Query()
        {
            // TODO:  ��� ucReportBase.Query ʵ��

            this.myBeginDate = this.dtpBeginDate.Value;	//ȡ��ʼʱ��
            this.myEndDate = this.dtpEndDate.Value;	//ȡ����ʱ��

            //�ж�ʱ�����Ч��
            if (this.myEndDate.CompareTo(this.myBeginDate) < 0)
            {
                MessageBox.Show("��ֹʱ����������ʼʱ�䣡", "��ʾ");
                return;
            }

            this.lblTop.Text = "ͳ��ʱ��:" + this.myBeginDate.ToString() + " �� " + this.myEndDate.ToString();
            this.lblTop.Text = "ͳ�ƿ���:" + this.cmbDept.Text + "               ͳ������:"
                //+ this.myBeginDate.ToString() + " �� " + this.myEndDate.ToString();
                + System.DateTime.Now.ToString("yyyy-MM-dd");

            FS.HISFC.BizLogic.Manager.Report report = new FS.HISFC.BizLogic.Manager.Report();
            //�������ݣ�����dataset
            //this.myDataSet = report.Query(reportParm, this.myBeginDate, this.myEndDate);
            FS.HISFC.BizLogic.Manager.Person ps = new FS.HISFC.BizLogic.Manager.Person();
            FS.HISFC.Models.Base.Employee var = new FS.HISFC.Models.Base.Employee();// FS.FrameWork.WinForms.BaseVar();
            var = ps.GetPersonByID(ps.Operator.ID);
            this.myDataSet = new DataSet();

            try
            {
                if (this.cmbItem.Tag == null || this.cmbItem.Tag.ToString() == "" || this.cmbItem.SelectedIndex < 0)
                {
                    this.cmbItem.Tag = "ALL";
                }

                if (this.cmbDept.Tag == null || this.cmbItem.Tag == null)
                {
                    MessageBox.Show("����ѡ����������!");
                    return;
                }

                int parm = report.ExecQuery("Material.Report.GetApplyInfoHistory", ref this.myDataSet,
                    this.cmbDept.Tag.ToString(), this.cmbItem.Tag.ToString(),
                    this.dtpBeginDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    this.dtpEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                if (parm == -1)
                {
                    MessageBox.Show(report.Err);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //��ʽ�����ļ���ַ

            //��farpoint������Դ
            this.myDataView = new DataView(myDataSet.Tables[0]);
            this.neuSpread1_Sheet1.DataSource = this.myDataView;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.SetFormat();

            if (this.iIndex != null)
            {
                this.SetSum(this.iTextIndex, this.iIndex);
            }
        }

        public virtual void Print()
        {
            // TODO:  ��� ucReportBase.Print ʵ��

            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;

            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            FS.HISFC.Models.Base.PageSize page = new FS.HISFC.Models.Base.PageSize();
            page.Height = 1062;
            page.Width = 965;
            page.Name = "10x11";
            print.SetPageSize(page);

            print.PrintPreview(this.left, this.top, this.panelPrint);
        }

        public virtual void Export()
        {
            // TODO:  ��� ucReportBase.Export ʵ��
            try
            {
                string fileName = "";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".xls";
                dlg.Filter = "Microsoft Excel ������ (*.xls)|*.*";
                DialogResult result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    fileName = dlg.FileName;
                    this.neuSpread1.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public virtual void Import()
        {
            // TODO:  ��� ucReportBase.Import ʵ��
            //�ӿ���û�д�ӡ���ã�ֻ��ʹ�������
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            //ֽ������
            p.ShowPageSetup();
            p.IsHaveGrid = true;
            //��ӡ��ʼ��ֹҳ������
            p.ShowPrintPageDialog();
            System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("Letter", 813, 1064);
            p.SetPageSize(size);
            //��ӡԤ��
            p.PrintPreview(10, 60, this.panelPrint);

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //��ʼ��
            if (!this.IsInit)
            {
                this.Init(null, null, null, false);
                this.btnOK.Text = "��ѯ";
                this.IsInit = true;
                this.label10.Visible = true;
                this.label10.Visible = true;
                this.cmbDept.Visible = true;
                this.cmbItem.Visible = true;
            }
            else
            {
                this.Query();
            }
        }

        private void dtpBeginDate_ValueChanged(object sender, EventArgs e)
        {
            this.Query();
        }

        public virtual string Parm
        {
            get
            {
                // TODO:  ��� ucReportBase.Parm getter ʵ��
                return reportParm;
            }
            set
            {
                // TODO:  ��� ucReportBase.Parm setter ʵ��
                if (value.IndexOf("|") == -1 || value.IndexOf("|") == value.Length - 1)
                    this.reportParm = value;
                else
                {
                    this.iIndex = null;
                    if (value.IndexOf("+") == -1 || value.IndexOf("+") == value.Length - 1)
                    {
                        this.reportParm = value.Substring(0, value.IndexOf("|"));
                        this.lbTitle.Text = value.Substring(value.IndexOf("|") + 1, value.Length - value.IndexOf("|") - 1);
                    }
                    else
                    {
                        string str1 = value.Substring(0, value.IndexOf("+"));
                        this.reportParm = str1.Substring(0, str1.IndexOf("|"));
                        this.lbTitle.Text = str1.Substring(str1.IndexOf("|") + 1, str1.Length - str1.IndexOf("|") - 1);

                        string str2 = value.Substring(value.IndexOf("+") + 1);
                        str2 = str2.Trim(',');
                        string[] strIndex = str2.Split(',');
                        iIndex = new int[strIndex.Length - 1];
                        iTextIndex = FS.FrameWork.Function.NConvert.ToInt32(strIndex[0]);
                        int j = 1;
                        for (int i = 0; i < strIndex.Length - 1; i++)
                        {
                            if (j >= strIndex.Length)
                                break;
                            iIndex[i] = FS.FrameWork.Function.NConvert.ToInt32(strIndex[j]);
                            j++;
                        }

                    }
                }
                //��ѯ
                this.Query();
            }
        }

    }
}
