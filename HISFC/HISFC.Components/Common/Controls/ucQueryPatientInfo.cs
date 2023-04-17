using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace FS.HISFC.Components.Common.Controls
{
    public partial class ucQueryPatientInfo : UserControl
    {
        public ucQueryPatientInfo()
        {
            InitializeComponent();
        }

        #region 变量
        /// <summary>
        /// 入出转业务层
        /// </summary>
        private FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// 综合管理业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 证件类型
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper IdCardTypeHelp = null;

        private FS.HISFC.Models.RADT.PatientInfo patient = null;



        #endregion

        #region 属性

        /// <summary>
        /// 患者信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
            }
        }

        private string queryStr = string.Empty;

        /// <summary>
        /// {B6EFC117-AEFB-441b-9BB2-3B8A1108CD5A}
        /// 预先设置好检索条件
        /// </summary>
        public string QueryStr
        {
            get { return queryStr; }
            set 
            { 
                queryStr = value;
                this.txtName.Text = value;
            }
        }

        #endregion

        #region 方法
        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private bool Valid()
        {
            string name = this.txtName.Text.Trim();
            string idCardType = this.cmbIDCardType.Tag.ToString();
            string idCardNO = this.txtIDCardNO.Text.Trim();
            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(idCardType) && string.IsNullOrEmpty(idCardNO))
            {
                MessageBox.Show("请输入信息后查询！");
                this.cmbIDCardType.Focus();
                return false;
            }
            if (!string.IsNullOrEmpty(idCardType) && string.IsNullOrEmpty(idCardNO))
            {
                MessageBox.Show("请输入患者的证件号！");
                this.txtIDCardNO.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(idCardType) && !string.IsNullOrEmpty(idCardNO))
            {
                MessageBox.Show("请选择患者的证件类型！");
                this.cmbIDCardType.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 查询患者信息
        /// </summary>
        private void QueryPatient()
        {
            string name = this.txtName.Text.Trim();
            string idCardType = this.cmbIDCardType.Tag.ToString();
            string idCardNO = this.txtIDCardNO.Text.Trim();
            ArrayList al = null;
            if (string.IsNullOrEmpty(idCardType) && string.IsNullOrEmpty(idCardNO) && !string.IsNullOrEmpty(name))
            {
                //{2E859F53-1650-4087-8637-9378352DD13D}
                al = inpatientManager.QueryComPatientInfoByName(name);

                ArrayList al2 = inpatientManager.QueryPatientByPhone(name);
                if (al == null)
                {
                    al = al2;
                }
                else
                {
                    if (al2 != null)
                    {
                        al.AddRange(al2);
                    }
                }
            }

            else if (!string.IsNullOrEmpty(idCardType) && !string.IsNullOrEmpty(idCardNO) && string.IsNullOrEmpty(name))
            {
                al = inpatientManager.QueryComPatientInfoByIdenInfo(idCardType, idCardNO);
            }
            else if (!string.IsNullOrEmpty(idCardType) && !string.IsNullOrEmpty(idCardNO) && !string.IsNullOrEmpty(name))
            {
                al = inpatientManager.QueryComPatientInfoByIdenInfoAndName(idCardType, idCardNO, name);
            }
            else
            {
                return;
            }
            if (al == null)
            {
                MessageBox.Show(inpatientManager.Err);
                return;
            }
            int count = this.neuSpread1_Sheet1.Rows.Count;
            if (count > 0)
            {
                this.neuSpread1_Sheet1.Rows.Remove(0, count);
            }
            foreach (FS.HISFC.Models.RADT.PatientInfo p in al)
            {
                count = this.neuSpread1_Sheet1.Rows.Count;
                this.neuSpread1_Sheet1.Rows.Add(count, 1);
                this.neuSpread1_Sheet1.Cells[count, 0].Text = p.Name;
                this.neuSpread1_Sheet1.Cells[count, 1].Text = p.Sex.Name;
                this.neuSpread1_Sheet1.Cells[count, 2].Text = inpatientManager.GetAge(p.Birthday);
                this.neuSpread1_Sheet1.Cells[count, 3].Text = p.IDCardType.ID;
                this.neuSpread1_Sheet1.Cells[count, 4].Text = p.IDCard;
                this.neuSpread1_Sheet1.Cells[count, 5].Text = p.PhoneHome;
                this.neuSpread1_Sheet1.Rows[count].Tag = p;
            }
            if (this.neuSpread1_Sheet1.Rows.Count > 0)
            {
                this.neuSpread1.Focus();
                this.neuSpread1_Sheet1.ActiveRowIndex = 0;
            }


        }

        /// <summary>
        /// 选择患者
        /// </summary>
        private void GetPatient()
        {
            if (this.neuSpread1_Sheet1.Rows.Count == 0)
            {
                return;
            }
            int index = this.neuSpread1_Sheet1.ActiveRowIndex;
            if (this.neuSpread1_Sheet1.Rows[index].Tag != null)
            {
                patient = this.neuSpread1_Sheet1.Rows[index].Tag as FS.HISFC.Models.RADT.PatientInfo;
                this.FindForm().DialogResult = DialogResult.OK;
            }
        }

        #endregion

        #region 事件
        private void btQuery_Click(object sender, EventArgs e)
        {
            if (!Valid()) return;
            QueryPatient();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.FindForm().DialogResult = DialogResult.Cancel;
            }
            if (keyData == Keys.Enter)
            {
                if (this.neuSpread1.Focused)
                {
                    this.GetPatient();
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ucQueryPatientInfo_Load(object sender, EventArgs e)
        {
            ArrayList alIdCard = managerIntegrate.QueryConstantList("IDCard");
            if (alIdCard == null)
            {
                MessageBox.Show("查询证件类型失败！");
                return;
            }
            this.cmbIDCardType.AddItems(alIdCard);
            IdCardTypeHelp = new FS.FrameWork.Public.ObjectHelper(alIdCard);
            //this.ActiveControl = this.cmbIDCardType;
            this.ActiveControl = this.txtName;

            //{B6EFC117-AEFB-441b-9BB2-3B8A1108CD5A}
            if (!string.IsNullOrEmpty(this.txtName.Text))
            {
                QueryPatient();
            }
        }



        private void btOk_Click(object sender, EventArgs e)
        {
            GetPatient();
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            GetPatient();
        }

        private void cmbIDCardType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtIDCardNO.Focus();
                this.txtIDCardNO.SelectAll();
            }
        }

        private void txtIDCardNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtName.Focus();
                this.txtName.SelectAll();
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {               
                this.btOk.Focus();
                this.btQuery_Click(new object(), new EventArgs());
            }
        }
        #endregion



    }
}
