using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Speciment
{
    public partial class frmUseAlone : Form
    {
        public delegate void UseProperty();
        public event UseProperty OnUseProperty;

        /// <summary>
        /// 管理业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 人员列表
        /// </summary>
        private ArrayList alEmpl = new ArrayList();

        private Dictionary<string, string> dicUsePro = new Dictionary<string, string>();

        public Dictionary<string, string> DicUsePro
        {
            get
            {
                return dicUsePro;
            }
            set
            {
                dicUsePro = value;
            }
        }

        private List<string> barCodeList = new List<string>();

        public List<string> BarCodeList
        {
            set
            {
                barCodeList = value;
                SetValue();
            }
        }

        public frmUseAlone()
        {
            InitializeComponent();
            this.alEmpl = this.managerIntegrate.QueryEmployeeAll();
        }

        private void SetValue()
        {
            neuSpread1_Sheet1.RowCount = barCodeList.Count;
            for (int i = 0; i < barCodeList.Count; i++)
            {               
                neuSpread1_Sheet1.Cells[i, 0].Text = barCodeList[i];
            }
        }

        public void GetUseProperty()
        {
            this.neuSpread1_Sheet1.RowCount = this.dicUsePro.Count;

            int i = 0;
            foreach (KeyValuePair<string, string> vp in dicUsePro)
            {
                neuSpread1_Sheet1.Cells[i, 0].Text = vp.Key.Trim();
                neuSpread1_Sheet1.Cells[i, 1].Text = vp.Value.Trim();
                i++;
            }
        }

        public void SetUseProperty()
        {
            for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
            {
                string s1= neuSpread1_Sheet1.Cells[i, 0].Text.Trim();
                string s2 = neuSpread1_Sheet1.Cells[i, 1].Text.Trim();
                dicUsePro[s1] = s2;
                //if (neuSpread1_Sheet1.Cells[i, 0].Text.Trim() == "")
                //{
                //    continue;
                //}
                //if (!dicUsePro.ContainsKey(neuSpread1_Sheet1.Cells[i, 0].Text))
                //{
                //    dicUsePro.Add(neuSpread1_Sheet1.Cells[i, 0].Text.Trim(), neuSpread1_Sheet1.Cells[i, 1].Text.Trim());
                //}
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            OnUseProperty();
            this.Close();
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //双击审核人弹出窗口
            if (this.neuSpread1_Sheet1.ActiveColumnIndex == 1)
            {
                this.PopSpeItem(e.Row);
            }
        }

        /// <summary>
        /// 项目选择框
        /// </summary>
        /// <param name="iIndex"></param>
        /// <returns></returns>
        public void PopSpeItem(int iIndex)
        {
            if ((this.alEmpl != null) && (this.alEmpl.Count > 0))
            {
                string[] label = { "代码", "名称" };
                float[] width = { 80F, 100F };
                bool[] visible = { true, true };
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.alEmpl, ref obj) == 1)
                {
                    this.neuSpread1_Sheet1.Cells[iIndex, 1].Text = obj.Name;
                    this.neuSpread1_Sheet1.Cells[iIndex, 1].Tag = obj.ID;
                }
            }
        }
    }
}