using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.Manager.Controls
{
    /// <summary>
    /// [��������: Ƶ��ά��]<br></br>
    /// [�� �� ��: Ѧռ��]<br></br>
    /// [����ʱ��: 2006��12��18]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucFrequencyManager : FS.FrameWork.WinForms.Controls.ucBaseControl
    {   
        public ucFrequencyManager()
        {
            InitializeComponent();
        }
        #region ����

        //��֤����ѡ������
        int RowIndex;
        
        //���ұ���
        private string deptCode = "ROOT";
        //����Ƶ�ι�����
        private FS.HISFC.BizLogic.Manager.Frequency manager = new FS.HISFC.BizLogic.Manager.Frequency();
        FarPoint.Win.Spread.CellType.ComboBoxCellType comboBox;
        ArrayList alUsage;
        //ɾ���б�
        ArrayList delAl = new ArrayList();
        //�������ݼ�
        DataSet constantData = new DataSet();
        #endregion

        #region ����
        /// <summary>
        /// ���ұ���
        /// </summary>
        public string DeptCode
        {
            get 
            {
                return this.deptCode;
            }
            set
            {
                this.deptCode = value;
            }
        }
        #endregion

        #region ������

        #region ���幤����

        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        
        #endregion

        #region ��ʼ��������
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("���", "���", 0, true, false, null);
            toolBarService.AddToolButton("ɾ��", "ɾ��", 1, true, false, null);
            
            return toolBarService;
        }
        #endregion

        #region ��������ť��Ӧ���÷���
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            { 
                case "���":
                    Add();
                    break;
                case "ɾ��":
                    Del();
                    break;
                default:
                    break;
             
            }
            base.ToolStrip_ItemClicked(sender, e);
        }
        #endregion

        #region ��д��������ť����
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Save(object sender, object neuObject)
        {
            Save();
            return base.Save(sender, neuObject);
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Export(object sender, object neuObject)
        {
            ExportInfo();
            return base.Export(sender, neuObject);
        }
        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Print(object sender, object neuObject)
        {
            PrintInfo();
            return base.Print(sender, neuObject);
        }
        /// <summary>
        /// �˳�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Exit(object sender, object neuObject)
        {
            return base.Exit(sender, neuObject);
        }

        #endregion

        #endregion
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucFrequencyManager_Load(object sender, EventArgs e)
        {
            try
            {   
                //��ʼ��
                Initialize();   
                //����FarPoint��ʽ
                SetFarPointStyle();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }

        }

        #region ����
        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Initialize()
        {
           //��ʼ��comboxCellType
            //����������
            FS.HISFC.BizLogic.Manager.Constant constant = new FS.HISFC.BizLogic.Manager.Constant();
            alUsage = constant.GetList(FS.HISFC.Models.Base.EnumConstant.USAGE);
            string [] s=new string[alUsage.Count+1];
            s[0] = "ȫ��";            
            for (int i = 0; i < alUsage.Count; i++)
            {
                s[i + 1] = ((FS.FrameWork.Models.NeuObject)alUsage[i]).Name;
            }
            try
            {
                comboBox = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                comboBox.Items = s;

            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
            ;
            constantData = InitialDataSet();//��ʼ�����ݼ�
            //���ݿ��ұ���������ݵ����ݼ�
            LoadData(this.DeptCode); 
            this.neuSpread1_Sheet1.DataSource = constantData;
            this.neuSpread1_Sheet1.SelectionBackColor = Color.YellowGreen;
            this.neuSpread1_Sheet1.Columns[-1].AllowAutoSort = true;//�Ƿ��Զ�����
            this.neuSpread1_Sheet1.Columns[2].Visible = false;//�÷���������
            this.neuSpread1_Sheet1.Columns[3].CellType = comboBox;//�÷�����       
                
            //FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
            //t.SubEditor = new ucFrequencyTimeEdit();
            //t.Multiline = true;
            //t.WordWrap = true;
            //this.neuSpread1_Sheet1.Columns[4].CellType = t;
           
        }
        /// <summary>
        /// ����FarPoint��ʽ
        /// </summary>
        private void SetFarPointStyle()
        {
            FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuSpread1_Sheet1.Columns[0].CellType = t;
            t.MaxLength = 6;
            this.neuSpread1_Sheet1.Columns[0].Width = 50;

            FarPoint.Win.Spread.CellType.TextCellType t1 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuSpread1_Sheet1.Columns[1].CellType = t1;
            t1.MaxLength = 20;
            this.neuSpread1_Sheet1.Columns[1].Width = 80;

            this.neuSpread1_Sheet1.Columns[3].CellType = comboBox;
            this.neuSpread1_Sheet1.Columns[3].Width = 80;

            FarPoint.Win.Spread.CellType.TextCellType t2 = new FarPoint.Win.Spread.CellType.TextCellType();
            t2.SubEditor = new ucFrequencyTimeEdit();
            t2.Multiline = true;
            t2.WordWrap = true;
            t2.MaxLength = 2000;
            this.neuSpread1_Sheet1.Columns[4].CellType = t2;
            this.neuSpread1_Sheet1.Columns[4].Width = 800;

            FarPoint.Win.Spread.CellType.NumberCellType t3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.neuSpread1_Sheet1.Columns[5].CellType = t3;
            this.neuSpread1_Sheet1.Columns[5].Width = 80;         
        }

        /// <summary>
        /// ��ʼ�����ݼ�
        /// </summary>
        /// <returns></returns>
        private DataSet InitialDataSet()
        {
            try
            {   
                //�������ݼ�
                DataSet ds = new DataSet();
                //����DataTable
                DataTable dataTable = new DataTable("constant");
                //����DataTable��
                //����
                DataColumn dataColumn1 = new DataColumn("Code");
                dataColumn1.DataType = typeof(System.String);
                dataTable.Columns.Add(dataColumn1);
                //����
                DataColumn dataColumn2 = new DataColumn("Name");
                dataColumn2.DataType = typeof(System.String); ;
                dataTable.Columns.Add(dataColumn2);
                //ʹ�ñ���
                DataColumn dataColumn3 = new DataColumn("Usage");
                dataColumn3.DataType = typeof(System.String);
                dataTable.Columns.Add(dataColumn3);
                //ʹ������
                DataColumn dataColumn4 = new DataColumn("Usname");
                dataColumn4.DataType = typeof(System.String);
                dataTable.Columns.Add(dataColumn4);
                //ʱ���
                DataColumn dataColumn5 = new DataColumn("FrequencyTime");
                dataColumn5.DataType = typeof(System.String);
                dataTable.Columns.Add(dataColumn5);

                DataColumn dataColumn6 = new DataColumn("SortId");
                dataColumn6.DataType = typeof(System.Int32);
                dataTable.Columns.Add(dataColumn6);

                if (this.DeptCode != "ROOT")
                {
                    dataTable.Constraints.Add("PK_A", new DataColumn[] { dataColumn1, dataColumn3 }, true);
                }
                ds.Tables.Add(dataTable);
                return ds;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }
        
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="DeptCode"></param>
        private void LoadData(string DeptCode)
        {   
            //������ݼ�Ϊ�������
            if (constantData == null)
                constantData = InitialDataSet();
            DataTable table = constantData.Tables[0];
            //���DatatalbeΪ��
            if (table != null)
            {
                if (table.Rows.Count > 0)
                    table.Rows.Clear();
                ArrayList list = null;
                try
                {
                    list = manager.GetList(DeptCode);
                    if (list == null) return;
                    //��ÿ��ҵ�Ƶ��
                    if (list.Count > 0)
                    {
                        AddConstsToTable(list, table);
                        constantData.AcceptChanges();                        
                    }                    
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
            //FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
            //t.SubEditor = new ucFrequencyTimeEdit();
            //this.neuSpread1_Sheet1.Columns[4].CellType = t;

            //this.neuSpread1_Sheet1.Columns[3].CellType = comboBox;//�÷�Name
            this.neuSpread1_Sheet1.Columns[-1].AllowAutoSort = true;
            
        }

        /// <summary>
        /// �������ArrayList�е�ҽ��Ƶ����Ϣ��ӵ���DataTable
        /// </summary>
        /// <param name="list"></param>
        /// <param name="table"></param>
        private void AddConstsToTable(ArrayList list, DataTable table)
        {
            table.Clear();

            foreach (FS.HISFC.Models.Order.Frequency cons in list)
            {
                table.Rows.Add(new Object[] { cons.ID, cons.Name, cons.Usage.ID, cons.Usage.Name, cons.Time, cons.SortID });//,cons.OperatorCode ;
            }
        }

        /// <summary>
        /// ��ӷ���
        /// </summary>
        /// <returns></returns>
        private int Add()
        {
            try
            {
                int RowCount = this.neuSpread1_Sheet1.Rows.Count;
                this.neuSpread1_Sheet1.Rows.Add(RowCount, 1);
                this.neuSpread1_Sheet1.ActiveRowIndex = this.neuSpread1_Sheet1.Rows.Count;
                neuSpread1.ShowActiveCell(FarPoint.Win.Spread.VerticalPosition.Center, FarPoint.Win.Spread.HorizontalPosition.Center);
                this.neuSpread1_Sheet1.Cells[RowCount, 0].Locked = false;
                this.neuSpread1_Sheet1.Cells[RowCount, 3].Locked = false;
                this.neuSpread1_Sheet1.SetActiveCell(RowCount, 0);

			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
            return 0;
        }
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <returns></returns>
        private int Del()
        {
            if (this.constantData.Tables[0].Rows.Count <= 0)
                return 0;
            int index = this.neuSpread1_Sheet1.ActiveRowIndex;
            if (index < 0) return 0;

            //����ǰɾ����ת���ɶ���ʵ��
            FS.HISFC.Models.Order.Frequency frequency = GetObjFromRow(index);
            int returnvalue = manager.ExistFrequencyCounts(frequency);

            if (returnvalue >= 1)
            {
                MessageBox.Show("��Ƶ���Ѿ�ʹ��,����ɾ��");
                return -1;
            }


            if (MessageBox.Show("ȷ��ɾ��" + this.neuSpread1_Sheet1.Cells[index, 0].Text + "?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                ////����ǰɾ����ת���ɶ���ʵ��
                //FS.HISFC.Models.Order.Frequency frequency = GetObjFromRow(index);
                //if (frequency.ID.Trim() != "" && frequency.ID != string.Empty)
                //{
                //    int returnvalue = manager.ExistFrequencyCounts(frequency);
                    //if (returnvalue == 0)
                    //{
                delAl.Add(frequency);
                this.neuSpread1_Sheet1.Rows[index].Remove();
                    //}

                    //if (returnvalue >= 1)
                    //{
                    //    MessageBox.Show("��Ƶ���Ѿ�ʹ��,����ɾ��");
                    //    return -1;
                    //}


                //}
            }
            return 0;
        }
        /// <summary>
        /// ���ݴ����������ض�Ӧʵ��
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Order.Frequency GetObjFromRow(int i)
        {
            FS.HISFC.Models.Order.Frequency f = new FS.HISFC.Models.Order.Frequency();
            f.ID = this.neuSpread1_Sheet1.Cells[i, 0].Text;//Ƶ�α���
            f.Name = this.neuSpread1_Sheet1.Cells[i, 1].Text;//Ƶ������
            f.Usage.ID = this.neuSpread1_Sheet1.Cells[i, 2].Text;//ʹ�ñ���
            f.Usage.Name = this.neuSpread1_Sheet1.Cells[i, 3].Text;//ʹ������
            f.Time = this.neuSpread1_Sheet1.Cells[i, 4].Text;//ʹ��ʱ��
            f.Dept.ID = this.DeptCode;

            /*
             * [2007/01/31] ԭ��û�м��.
             * 
             * decimal a = Convert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, 5].Text);
             * f.SortID = (int)a;
             * 
             */
            //����Ϊ�ı���,�������������Ƿ�����,��ô��0����
            try
            {
                decimal a = Convert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, 5].Text);
                f.SortID = (int)a;
            }
            catch
            {
                f.SortID = 0;
            }

            return f;
        }      
        /// <summary>
        /// ����
        /// </summary>
        private void ExportInfo()
        {
            bool tr = false;
            string fileName = "";
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "excel|*.xls";
            saveFile.Title = "������Excel";

            saveFile.FileName = "Ƶ��ά�� " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString().Replace(':', '-');

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                if (saveFile.FileName.Trim() != "")
                {
                    fileName = saveFile.FileName;
                    tr = this.neuSpread1.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                }
                else
                {
                    MessageBox.Show("�ļ�������Ϊ��!");
                    return;
                }

                if (tr)
                {
                    MessageBox.Show("�����ɹ�!");
                }
                else
                {
                    MessageBox.Show("����ʧ��!");
                }
            }
        }
        
        /// <summary>
        /// ��ӡ
        /// </summary>
        private void PrintInfo()
        {
            FS.FrameWork.WinForms.Classes.Print pr = new FS.FrameWork.WinForms.Classes.Print();
            pr.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            //pr.ShowPrintPageDialog();
            pr.PrintPreview(this.neuPanel1);

          
    
        }

        /// <summary>
        /// ��õ�ǰ�������ж�Ӧ�Ķ���
        /// </summary>
        /// <returns></returns>
        private ArrayList AddRowDataToObj()
        {
            ArrayList al = new ArrayList();
            try
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    FS.HISFC.Models.Order.Frequency frequency = new FS.HISFC.Models.Order.Frequency();
                    frequency.ID = this.neuSpread1_Sheet1.Cells[i, 0].Text;
                    frequency.Name = this.neuSpread1_Sheet1.Cells[i, 1].Text;
                    frequency.Usage.ID = this.neuSpread1_Sheet1.Cells[i, 2].Text;
                    frequency.Usage.Name = this.neuSpread1_Sheet1.Cells[i, 3].Text;
                    frequency.Time = this.neuSpread1_Sheet1.Cells[i, 4].Text;
                    frequency.Dept.ID = this.DeptCode;

                    /* [2007/01/31] û�����ͼ��
                     * 
                     * decimal temp= Convert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, 5].Text);
                     * 
                     */
                    try
                    {
                        decimal temp = Convert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, 5].Text);
                        frequency.SortID = (int)temp;
                    }
                    catch
                    {
                        frequency.SortID = 0;
                    }
                   
                    al.Add(frequency);
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
            return al;
        
        }        

        /// <summary>
        /// ���淽��
        /// </summary>
        private int Save()
        {
            string msgErr = string.Empty;
            this.neuSpread1.StopCellEditing();
            if (ValidData() == -1)
            {
                this.neuSpread1_Sheet1.SetActiveCell(RowIndex, 0);
                return -1;
            }
             ArrayList al = AddRowDataToObj();
             if (al == null) return -1;
             //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(manager.Connection);
             try
              { //����ʼ

                  FS.FrameWork.Management.PublicTrans.BeginTransaction();

                 //trans.BeginTransaction();
                  manager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                 //ɾ��
                 foreach (FS.HISFC.Models.Order.Frequency f in delAl)
                 {
                     if (manager.Del(f) == -1)
                     {
                         msgErr = FS.FrameWork.Management.PublicTrans.Err;
                         FS.FrameWork.Management.PublicTrans.RollBack();
                         MessageBox.Show("ɾ��ʧ�ܣ�" + msgErr);
                         return -1;
                     }
                 }
                 //����޸�
                 foreach (FS.HISFC.Models.Order.Frequency fre in al)
                 {
                     if (manager.Set(fre) == -1)
                     {
                         msgErr = FS.FrameWork.Management.PublicTrans.Err;
                         FS.FrameWork.Management.PublicTrans.RollBack();
                         MessageBox.Show("����ʧ�ܣ�" + msgErr);
                         return -1;
                     }
                 }
               
                 FS.FrameWork.Management.PublicTrans.Commit();
                 for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                 {
                     if (!this.neuSpread1_Sheet1.Cells[i, 0].Locked)
                         this.neuSpread1_Sheet1.Cells[i, 0].Locked = true;
                     if (!this.neuSpread1_Sheet1.Cells[i, 3].Locked)
                         this.neuSpread1_Sheet1.Cells[i, 3].Locked = true;
                 }
		      }
		     catch(Exception a)
		     {
                 FS.FrameWork.Management.PublicTrans.RollBack();
			    MessageBox.Show("���ݱ���ʧ�ܣ�"+a.Message,"ʧ��",MessageBoxButtons.OK,MessageBoxIcon.Error);
			    return -1;
    			
		     }

             LoadData(this.DeptCode);
             SetFarPointStyle();
       
            MessageBox.Show("����ɹ�!");
            delAl.Clear();
            return 0;

         }

        /// <summary>
        /// ��֤
        /// </summary>
        /// <returns></returns>
        private int ValidData()
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                string temp = this.neuSpread1_Sheet1.GetText(i, 0).ToString();
                if (temp.Trim() == "")
                {
                    MessageBox.Show("��" + (i + 1).ToString() + "Ƶ�α��벻��Ϊ�գ�");
                    RowIndex = i;
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(temp,6))
                {
                    MessageBox.Show("��" + (i + 1).ToString() + "Ƶ�α��������");
                    RowIndex = i;
                    return -1;
                }

                /*
                 * [2007/01/31] ����ط�����Ҫ�����Ƶ�����Ƶļ��
                 *              ���ǲ�����Ա��û��˵
                 * 
                 */

                string temp2=this.neuSpread1_Sheet1.GetText(i,3).ToString();
                if (temp2.Trim() == "")
                {
                    MessageBox.Show("��" + (i + 1).ToString() + "�÷����Ʋ���Ϊ�գ�");
                    RowIndex = i;
                    return -1;
                }
                string temp3 = this.neuSpread1_Sheet1.GetText(i, 4).ToString();
                if (temp3.Trim() == "")
                {
                    MessageBox.Show("��" + (i + 1).ToString() + "ʱ��㲻��Ϊ�գ�");
                    RowIndex = i;
                    return -1;
                }

                if (!this.IsTime(temp3))
                {
                    MessageBox.Show("��" + (i + 1).ToString() + "ʱ��㲻����Ч��ʱ���ʽ��");
                    RowIndex = i;
                    return -1;
                }

                string temp4 = this.neuSpread1_Sheet1.GetText(i, 1).ToString();
                if (temp4.Trim() == "")
                {
                    MessageBox.Show("��"+(i + 1).ToString()+"�����Ʋ���Ϊ��");
                    RowIndex = i;
                    return -1;
                }
            }

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count - 1; i++)
                {
                    for (int k = i + 1; k < this.neuSpread1_Sheet1.Rows.Count; k++)
                    { 
                        if(this.neuSpread1_Sheet1.Cells[i,0].Text==this.neuSpread1_Sheet1.Cells[k,0].Text && 
                            this.neuSpread1_Sheet1.Cells[i,2].Text==this.neuSpread1_Sheet1.Cells[k,2].Text)
                        {
                            MessageBox.Show("��"+(i+1).ToString()+"��"+"��"+(k+1).ToString()+"�����ظ���");
                            return -1;
                        }
                    }
                }

            return 0;
        }
        /// <summary>
        /// ���ݴ������ƻ���÷�����
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        private string GetUsageCode(string Name)
        {
            if (Name == "ȫ��") return "All";
            for (int i = 0; i < alUsage.Count; i++)
            {
                if (((FS.FrameWork.Models.NeuObject)alUsage[i]).Name == Name)
                    return ((FS.FrameWork.Models.NeuObject)alUsage[i]).ID;
            }
            return "";
        }
        #endregion  
           
        #region �¼�
        private void neuSpread1_Sheet1_CellChanged(object sender, FarPoint.Win.Spread.SheetViewEventArgs e)
        {
            
            try
            {
                if (e.Column == 0)
                {
                    FS.HISFC.Models.Order.Frequency f = new FS.HISFC.Models.Order.Frequency();
                    try
                    {
                        f.ID = this.neuSpread1_Sheet1.Cells[e.Row, e.Column].Text;
                        if (f.Name != "") this.neuSpread1_Sheet1.Cells[e.Row, 1].Text = f.Name;
                    }
                    catch { }
                }
                else if (e.Column == 3)
                {
                    //�����÷�ID
                    this.neuSpread1_Sheet1.Cells[e.Row, e.Column - 1].Text = GetUsageCode(this.neuSpread1_Sheet1.Cells[e.Row, e.Column].Text);

                }
            }
            catch { }

        }
        #endregion

        //================================================================
        //�޸��ˣ�·־��
        //ʱ�䣺2007-4-10
        //Ŀ�ģ��ж�ʱ����Ƿ�����Ч��ʱ���ʽ

        #region ����ʱ��
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <param name="timeStr">ʱ���ַ���</param>
        /// <returns></returns>
        private bool IsTime(string timeStr)
        {
            bool bl = true;
            if (timeStr.IndexOf("-") < 0)
            {
                try
                {
                    Convert.ToDateTime(timeStr);
                    bl= true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                string tempStr;
                int index;
                while ((index = timeStr.IndexOf("-")) > 0)
                {
                    try
                    {
                        tempStr = timeStr.Substring(0, index);
                        Convert.ToDateTime(tempStr);
                        timeStr = timeStr.Remove(0, index + 1);
                        bl = true;
                    }
                    catch
                    {
                        return false;
                    }
                }
                try
                {
                    Convert.ToDateTime(timeStr);
                    bl=true;
                }
                catch
                {

                    return false;
                }
            }
            return bl;
        }
        #endregion
    }
       
}
