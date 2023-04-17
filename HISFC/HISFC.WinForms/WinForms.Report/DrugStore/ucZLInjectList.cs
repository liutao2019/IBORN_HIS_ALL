using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Function;

namespace FS.WinForms.Report.DrugStore
{
    /// <summary>
    /// ����ע���嵥 
    /// 
    /// <����˵��>
    ///     1������ע���嵥��ӡ  ����������Ŀ�����γ�
    /// </����˵��>
    /// </summary>
    public partial class ucZLInjectList : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucZLInjectList()
        {
            InitializeComponent();

            this.Init();
        }

        private static FS.FrameWork.Public.ObjectHelper usageHelper = new FS.FrameWork.Public.ObjectHelper();

        private void Init()
        {
            FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
            System.Collections.ArrayList alList = consManager.GetList(FS.HISFC.Models.Base.EnumConstant.USAGE);

            usageHelper = new FS.FrameWork.Public.ObjectHelper(alList);
        }

        public void AddAllData(System.Collections.ArrayList al)
        {
            if (al.Count <= 0)
            {
                return;
            }

            #region �г�ҩ����ҩ��ӡ

            try
            {
                try
                {
                    ComboSort comboSort = new ComboSort();
                    al.Sort(comboSort);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("��������������" + ex.Message);
                    return;
                }

                int iIndex = this.neuSpread1_Sheet1.Rows.Count;
                foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in al)
                {
                    this.neuSpread1_Sheet1.Rows.Add(iIndex, 1);
                    this.neuSpread1_Sheet1.Cells[iIndex, 0].Text = info.CombNO;     //��Ϻ�
                    this.neuSpread1_Sheet1.Cells[iIndex, 2].Text = string.Format("{0}��{1}[{2}]  {3}{4}/{5}",info.Item.Name,Function.DrugDosage.GetStaticDosage(info.Item.ID),info.Item.Specs,info.Operation.ApplyQty,info.Item.MinUnit,usageHelper.GetName(info.Usage.ID));              //ҩƷ��ҩ��Ϣ                    

                    this.lbInfo.Text = string.Format("������{0}        ���ڣ�{1}     �����ţ�{2}",info.User02,info.Operation.ApplyOper.OperTime.ToString("yyyy-MM-dd"),info.PatientNO);
                }

                int groupID = 0;
                string privCombo = "-1";
                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    if (privCombo != this.neuSpread1_Sheet1.Cells[i, 0].Text)
                    {
                        groupID++;
                        this.neuSpread1_Sheet1.Cells[i, 1].Text = groupID.ToString();
                        privCombo = this.neuSpread1_Sheet1.Cells[i, 0].Text;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells[i, 1].Text = groupID.ToString();
                    }
                }

                iIndex = this.neuSpread1_Sheet1.Rows.Count;
                this.neuSpread1_Sheet1.Rows.Add(iIndex, 2);
                iIndex = this.neuSpread1_Sheet1.Rows.Count - 1;
                this.neuSpread1_Sheet1.Cells[iIndex, 0].ColumnSpan = 7;
                this.neuSpread1_Sheet1.Cells[iIndex, 0].Text = "��ҩ��              �˶ԣ�                 ��ʿ�˶ԣ�";
                this.neuSpread1_Sheet1.Rows[iIndex].Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            #endregion
        }

        public void Preview()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Print()
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.IsResetPage = true;
            System.Drawing.Printing.PaperSize pageSize = this.getPaperSizeForInput();
            p.SetPageSize(pageSize);
            p.PrintPage(15, 10, this);
            //p.PrintPreview(15, 10, this.neuPanel1);
        }

        public void Clear()
        {
            this.lbInfo.Text = "";

            this.neuSpread1_Sheet1.Rows.Count = 0;
        }

        /// <summary>
        /// ��ⵥ��ֽ�Ÿ߶�����
        /// Ĭ�������������������ݵĸ߶�
        /// </summary>
        private System.Drawing.Printing.PaperSize getPaperSizeForInput()
        {
            System.Drawing.Printing.PaperSize paperSize = new System.Drawing.Printing.PaperSize();
            paperSize.PaperName = "rkd" + System.DateTime.Now.ToString();
            try
            {
                int width = 820;
                //int width = this.Width;
                int curHeight = this.Height;
                int addHeight = (this.neuSpread1_Sheet1.RowCount - 1) * (int)this.neuSpread1_Sheet1.Rows[0].Height;

                paperSize.Width = width;
                paperSize.Height = (addHeight + curHeight);
            }
            catch (Exception ex)
            {
                MessageBox.Show("��������ӡֽ�ų���>>" + ex.Message);
            }
            return paperSize;
        }

        protected class ComboSort : System.Collections.IComparer
        {
            public ComboSort() { }


            #region IComparer ��Ա

            public int Compare(object x, object y)
            {
                // TODO:  ��� FeeSort.Compare ʵ��
                FS.HISFC.Models.Pharmacy.ApplyOut obj1 = x as FS.HISFC.Models.Pharmacy.ApplyOut;
                FS.HISFC.Models.Pharmacy.ApplyOut obj2 = y as FS.HISFC.Models.Pharmacy.ApplyOut;
                if (obj1 == null || obj2 == null)
                    throw new Exception("�����ڱ���ΪPharmacy.ApplyOut����");
                int i1 = NConvert.ToInt32(obj1.CombNO);
                int i2 = NConvert.ToInt32(obj2.CombNO);
                return i1 - i2;
            }

            #endregion
        }
    }
}
