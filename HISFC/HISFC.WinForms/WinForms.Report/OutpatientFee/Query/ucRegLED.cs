using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.OutpatientFee.Query
{
    public partial class ucRegLED : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.HISFC.BizProcess.Interface.Registration.IShowLED
    {
        public ucRegLED()
        {
            InitializeComponent();
        }
        #region ����
        private FS.HISFC.BizLogic.Registration.Schema schemaManager = new FS.HISFC.BizLogic.Registration.Schema();
        #endregion

        #region ����
        /// <summary>
        /// ҽ���������
        /// </summary>
        /// <returns></returns>
        public string Query()
        {
            DateTime currentDate = schemaManager.GetDateTimeFromSysDateTime();
            string strLED = string.Empty;
            DataSet ds = new DataSet();
            ds = this.schemaManager.QueryDoctForLED(currentDate.Date, currentDate);
            if (ds == null)
            {
                return null; 
            }

            this.neuSpread1_Sheet1.DataSource = ds;
            this.SetFPFormat();
            return strLED;
        }
        /// <summary>
        /// ����farpoint��ʾ��ʽ
        /// </summary>
        /// <returns></returns>
        public int SetFPFormat()
        {
            this.neuSpread1_Sheet1.Columns[0].Visible = false;

            this.neuSpread1_Sheet1.Columns[1].Label = "��������";
            this.neuSpread1_Sheet1.Columns[1].Width = 100;
            this.neuSpread1_Sheet1.Columns[2].Visible = false;

            this.neuSpread1_Sheet1.Columns[3].Label = "��������";
            this.neuSpread1_Sheet1.Columns[3].Width = 100;
            

            this.neuSpread1_Sheet1.Columns[4].Label = "���";
            this.neuSpread1_Sheet1.Columns[4].Width = 100;

            this.neuSpread1_Sheet1.Columns[5].Label = "��ʼʱ��";
            this.neuSpread1_Sheet1.Columns[5].Width = 100;

            this.neuSpread1_Sheet1.Columns[6].Label = "����ʱ��";
            this.neuSpread1_Sheet1.Columns[6].Width = 100;
            this.neuSpread1_Sheet1.Columns[7].Label = "�Һ��޶�";
            this.neuSpread1_Sheet1.Columns[7].Width = 100;
            this.neuSpread1_Sheet1.Columns[8].Label = "�ѹ�����";
            this.neuSpread1_Sheet1.Columns[8].Width = 100;
            this.neuSpread1_Sheet1.Columns[9].Label = "ԤԼ����";
            this.neuSpread1_Sheet1.Columns[9].Width = 100;
            this.neuSpread1_Sheet1.Columns[10].Label = "ԤԼ�ѹ�";
            this.neuSpread1_Sheet1.Columns[10].Width = 100;
            this.neuSpread1_Sheet1.Columns[11].Label = "����Һ��޶�";
            this.neuSpread1_Sheet1.Columns[11].Width = 100;
            this.neuSpread1_Sheet1.Columns[12].Label = "�����ѹ�����";
            this.neuSpread1_Sheet1.Columns[12].Width = 100;

            //this.neuSpread1_Sheet1.Columns[11].Visible = false;
             
            //this.neuSpread1_Sheet1.Columns[12].Visible = false;
             
            this.neuSpread1_Sheet1.Columns[13].Visible = false;

            return 1;
        }

        private void neuButton1_Click(object sender, EventArgs e)
        {
            this.Query();
            //this.SetFPFormat();
        }
        /// <summary>
        /// ����LED �ӿ� �����ʾ����LED
        /// </summary>
        public int CreateString()
        {
            return 1;
        }

        public void SetFresh (bool Valid,string strNum )
        {
            this.timer1.Enabled = Valid;
            timer1.Interval = 1000 * FS.FrameWork.Function.NConvert.ToInt32(strNum);

        }
        
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void Init()
        {
            //SetFresh(true,this.neuNumericTextBox1.Text);
            
            this.Query();
            this.OK_Click(null,null);
            this.FindForm().Text = "ҽ���������(LED��ʾ��ʹ��)";
        }
        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.PrintPage(0, 0,this.NpPrint );
             
            return base.OnPrint(sender, neuObject);
        }
        //����
        public override int Export(object sender, object neuObject)
        {

            SaveFileDialog op = new SaveFileDialog();

            op.Title = "��ѡ�񱣴��·��������";
            op.CheckFileExists = false;
            op.CheckPathExists = true;
            op.DefaultExt = "*.xls";
            op.Filter = "(*.xls)|*.xls";

            DialogResult result = op.ShowDialog();

            if (result == DialogResult.Cancel || op.FileName == string.Empty)
            {
                return -1;
            }

            string filePath = op.FileName;

            bool returnValue = this.neuSpread1.SaveExcel(filePath, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
            return base.Export(sender, neuObject);
        }
        #endregion
        #region �¼�
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucRegLED_Load(object sender, EventArgs e)
        {
            this.Init();
        }
       
        private void OK_Click(object sender, EventArgs e)
        {
            this.neuNumericTextBox1.Enabled = false;
            this.SetFresh(true, this.neuNumericTextBox1.Text);

        }

        private void ReSet_Click(object sender, EventArgs e)
        {
            this.neuNumericTextBox1.Enabled = true;
            this.SetFresh(false, this.neuNumericTextBox1.Text);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Query();
            
            
        }
        #endregion
    }
   
   
}
