using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;
using FS.FrameWork.Function;

namespace FS.WinForms.Report.Pharmacy
{
    /// <summary>
    /// [��������: ҩ��������]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2007-10]<br></br>
    /// </summary>
    public partial class ucPhaInputSum : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ucPhaInputSum()
        {
            InitializeComponent();
        }



        #region ����

        /// <summary>
        /// ��ѯ��ʼʱ��
        /// </summary>
        public DateTime BeginTime
        {
            get
            {
                return NConvert.ToDateTime(this.dtpBeginTime.Text);
            }
        }

        /// <summary>
        /// ��ѯ��ֹʱ��
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                return NConvert.ToDateTime(this.dtpEndTime.Text);
            }
        }

        #endregion

        #region ������

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();

            return base.OnQuery(sender, neuObject);
        }

        #endregion

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        protected int Init()
        {
            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList alDept = deptManager.GetDeptmentAll();

            ArrayList alStockDept = new ArrayList();
            foreach (FS.HISFC.Models.Base.Department dept in alDept)
            {
                if (dept.DeptType.ID.ToString() == "PI")
                {
                    alStockDept.Add(dept);
                }
            }

            this.cmbStockDept.AddItems(alStockDept);

            this.dtpBeginTime.Value = deptManager.GetDateTimeFromSysDateTime().AddDays(-1);
            this.dtpEndTime.Value = deptManager.GetDateTimeFromSysDateTime();
            return 1;
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <returns></returns>

        protected int Query()
        {
            if (this.cmbStockDept.Tag == null || this.cmbStockDept.Tag.ToString() == "")
            {
                MessageBox.Show(Language.Msg("��ѡ���ѯҩ��"));
                return -1;
            }

            System.Data.DataSet ds = new DataSet();

            FS.FrameWork.Management.DataBaseManger dataManager = new DataBaseManger();
            if (dataManager.ExecQuery("Pharmacy.Report.InputSum", ref ds, this.cmbStockDept.Tag.ToString(), this.BeginTime.ToString(), this.EndTime.ToString()) == -1)
            {
                MessageBox.Show(Language.Msg("û�������Ϣ��") + dataManager.Err);
                return -1;
            }

            if (ds == null || ds.Tables.Count <= 0)
            {
                return 0;
            }
            this.fpSpread1_Sheet1.DataSource = ds;



            int iTotIndex = this.fpSpread1_Sheet1.RowCount;
            decimal sumNum4 = 0;
            decimal sumNum3 = 0;
            decimal sumNum2 = 0;
            decimal sumNum1 = 0;

       
            for (int i = 0; i < iTotIndex; i++)
            {
                sumNum1 = sumNum1 + NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, 1].Text);
                sumNum2 = sumNum2 + NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, 2].Text);
                sumNum3 = sumNum3 + NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, 3].Text);
                sumNum4 = sumNum4 + NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, 4].Text);
            }
            //this.fpSpread1_Sheet1.RowCount = iTotIndex + 1;
            this.fpSpread1_Sheet1.Rows.Add(iTotIndex, 1);
            this.fpSpread1_Sheet1.Cells[iTotIndex , 0].Text = "�ϼ�";
            this.fpSpread1_Sheet1.Cells[iTotIndex , 1].Text = sumNum1.ToString();
            this.fpSpread1_Sheet1.Cells[iTotIndex , 2].Text = sumNum2.ToString();
            this.fpSpread1_Sheet1.Cells[iTotIndex , 3].Text = sumNum3.ToString();
            this.fpSpread1_Sheet1.Cells[iTotIndex , 4].Text = sumNum4.ToString();          

            return 1;
        }
        /// <summary>
        /// ��ӡ
        /// </summary>
        public void Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.PrintPage(30, 10, this);

        }
        protected override int OnPrint(object sender, object neuObject)
        {
            this.Print();
            return base.OnPrint(sender, neuObject);
        }

        //public override int Export(object sender, object neuObject)
        //{
        //    if (this.fpSpread1.Export() == 1)
        //    {
        //        MessageBox.Show(Language.Msg("�����ɹ�"));
        //    }

        //    return 1;
        //}

        protected override void OnLoad(EventArgs e)
        {
            this.Init();

            base.OnLoad(e);
        }

        private void neuLabel2_Click(object sender, EventArgs e)
        {

        }
    }
}
