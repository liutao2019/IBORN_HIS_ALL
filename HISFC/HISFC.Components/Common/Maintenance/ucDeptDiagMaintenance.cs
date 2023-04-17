using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Common.Maintenance
{

    /// <summary>
    /// ���ҳ������ά��
    /// �����ˣ�����
    /// ����ʱ�䣺2010-05-20
    /// {6EF7D73B-4350-4790-B98C-C0BD0098516E}
    /// </summary>
    public partial class ucDeptDiagMaintenance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// ����
        /// </summary>
        public ucDeptDiagMaintenance()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// �Ƿ���ʾ��
        /// </summary>
        protected bool isShowTree = false;

        /// <summary>
        /// �Ƴ������ҵ����
        /// </summary>
        private FS.HISFC.BizLogic.Manager.DeptICD deptDiagManager = new FS.HISFC.BizLogic.Manager.DeptICD();

        /// <summary>
        /// icdҵ����
        /// </summary>
        private FS.HISFC.BizLogic.HealthRecord.ICD icdManager = new FS.HISFC.BizLogic.HealthRecord.ICD();

        /// <summary>
        /// ��ǰ����Ա
        /// </summary>
        private FS.HISFC.Models.Base.Employee currOper = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

        FS.FrameWork.WinForms.Forms.ToolBarService toolBar = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// ���ұ���
        /// </summary>
        protected string deptID = string.Empty;

        #endregion

        #region ����
        /// <summary>
        /// �Ƿ���ʾ��
        /// </summary>
        public bool IsShowTree
        {
            get 
            {
                return this.isShowTree;
            }
            set 
            {
                this.isShowTree = value;
            }
        }
        #endregion

        #region ����

        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        private void InitControl()
        {
            this.pnlTree.Visible = this.isShowTree;
            this.neuSplitter1.Visible = this.isShowTree;
            this.InitucDiagnose();

            this.neuFpEnter1_Sheet1.Columns[0].Visible = false;
            this.neuFpEnter1_Sheet1.Columns[6].Visible = false;
            
        }

        /// <summary>
        /// ��ʼ�����ѡ���б�
        /// </summary>
        private void InitucDiagnose()
        {
            this.ucDiagnose1.Visible = false;
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ��������Ϣ ���Ժ�.....");
            Application.DoEvents();
            this.ucDiagnose1.Init();
            this.ucDiagnose1.SelectItem += new FS.HISFC.Components.Common.Controls.ucDiagnose.MyDelegate(ucDiagnose1_SelectItem);
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        /// <summary>
        /// ��ʼ������Ŀǰ����
        /// </summary>
        private void InitTree()
        {

        }
        

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="deptID"></param>
        private void QueryDeptDiag(string deptID)
        {
            this.neuFpEnter1_Sheet1.RowCount = 0;

            ArrayList alDeptDiag = new ArrayList();

            alDeptDiag = this.icdManager.QueryDeptDiag(deptID);

            if (alDeptDiag != null)
            {
                foreach (FS.HISFC.Models.HealthRecord.ICD icd in alDeptDiag)
                {
                    this.neuFpEnter1_Sheet1.Rows.Add(this.neuFpEnter1_Sheet1.RowCount, 1);

                    this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.RowCount - 1, 0].Text = icd.KeyCode.ToString();
                    this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.RowCount - 1, 1].Text = icd.ID.ToString();
                    this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.RowCount - 1, 2].Text = icd.Name.ToString();
                    this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.RowCount - 1, 3].Text = icd.OperInfo.Name.ToString();
                    this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.RowCount - 1, 4].Text = icd.OperInfo.OperTime.ToString();
                    this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.RowCount - 1, 5].Value = icd.IsValid;

                }
            }
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="deptID"></param>
        /// <returns></returns>
        private int DeleteOneData(int rowIndex,string deptID)
        {
            if (rowIndex >= 0)
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                this.deptDiagManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                string icdIndex = this.neuFpEnter1_Sheet1.Cells[rowIndex, 0].Text;

                FS.FrameWork.Models.NeuObject objDeptDiag = new FS.FrameWork.Models.NeuObject();
                objDeptDiag.ID = deptID;
                objDeptDiag.Name=icdIndex;
                int iReturn = this.deptDiagManager.DeleteDeptDiag(objDeptDiag);

                if (iReturn < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("ɾ��ʧ�ܣ�"));
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.Commit();

                this.neuFpEnter1_Sheet1.RemoveRows(rowIndex, 1);
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// ���ر���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.deptDiagManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            bool isAddData = false;

            for (int i = 0; i < this.neuFpEnter1_Sheet1.RowCount; i++)
            {
                if (this.neuFpEnter1_Sheet1.Cells[i, 6].Text == "NEW")
                {
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    FS.HISFC.Models.HealthRecord.ICD icd = this.neuFpEnter1_Sheet1.Rows[i].Tag as FS.HISFC.Models.HealthRecord.ICD;
                    obj.ID = this.deptID;
                    obj.Name = icd.KeyCode;
                    obj.Memo = icd.Name;
                    obj.User01 = icd.SpellCode;
                    obj.User02 = this.currOper.ID;

                    int iReturn = this.deptDiagManager.InsertDeptDiag(obj);

                    if (iReturn < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ʧ�ܣ�" + this.deptDiagManager.Err));
                        return -1;
                    }

                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            for (int i = 0; i < this.neuFpEnter1_Sheet1.RowCount; i++)
            {
                if (this.neuFpEnter1_Sheet1.Cells[i, 6].Text == "NEW")
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 6].Text = "";
                    isAddData = true;
                }
            }
            if (isAddData)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ɹ���"));
            }
            else
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("û����Ҫ��������ݣ�"));
            }

            return base.OnSave(sender, neuObject);
        }

        /// <summary>
        /// ��ʼ��ToolBar
        /// </summary>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBar.AddToolButton("ɾ��", "ɾ������", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            return this.toolBar;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "ɾ��")
            {
                if (this.neuFpEnter1_Sheet1.RowCount <= 0) return;
                int row = this.neuFpEnter1_Sheet1.ActiveRowIndex;
                if (row < 0) return;
                DialogResult Result = MessageBox.Show("ȷ��ɾ�������ݣ�", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (Result == DialogResult.OK)
                {
                    this.DeleteOneData(row, this.deptID);
                }
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryDeptDiag(this.deptID);
            return base.OnQuery(sender, neuObject);
        }

        #endregion

        #region �¼�

        private int ucDiagnose1_SelectItem(Keys KeyData)
        {
            #region ѡ�����ʱ��֧��������{3198206F-57BD-4937-BAC2-790257626D6F}
            this.GetInfo();
            #endregion
            return 0;
        }
        #region ѡ�����ʱ��֧�������� {3198206F-57BD-4937-BAC2-790257626D6F}
        private int GetInfo()
        {
            try
            {
                FS.HISFC.Models.HealthRecord.ICD icd = null;
                if (this.ucDiagnose1.GetItem(ref icd) == -1)
                {
                    return -1;
                }
                if (icd != null)
                {
                    for (int i = 0; i < this.neuFpEnter1_Sheet1.RowCount; i++)
                    {
                        if (icd.ID == this.neuFpEnter1_Sheet1.Cells[i, 1].Text.Trim())
                        {
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("�б����Ѵ���" + icd.Name + "��"));
                            return -1;
                        }
                    }
                    this.txtInput.Tag = icd;

                    this.neuFpEnter1_Sheet1.Rows.Add(0, 1);
                    this.neuFpEnter1_Sheet1.Cells[0, 0].Text = icd.KeyCode.ToString();
                    this.neuFpEnter1_Sheet1.Cells[0, 1].Text = icd.ID.ToString();
                    this.neuFpEnter1_Sheet1.Cells[0, 2].Text = icd.Name.ToString();
                    this.neuFpEnter1_Sheet1.Cells[0, 3].Text = this.currOper.Name.ToString();
                    this.neuFpEnter1_Sheet1.Cells[0, 4].Text = DateTime.Now.ToString();
                    this.neuFpEnter1_Sheet1.Cells[0, 5].Value = icd.IsValid;
                    this.neuFpEnter1_Sheet1.Cells[0, 6].Text = "NEW";

                    this.neuFpEnter1_Sheet1.Rows[0].Tag = icd;
                }
                this.ucDiagnose1.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return 1;
        }
        #endregion

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Up)
            {
                this.ucDiagnose1.PriorRow();
            }
            if (e.KeyCode == Keys.Down)
            {
                this.ucDiagnose1.NextRow();
            }
            if (e.KeyCode == Keys.Enter)
            {
                FS.HISFC.Models.HealthRecord.ICD icd = new FS.HISFC.Models.HealthRecord.ICD();
                this.ucDiagnose1.GetItem(ref icd);
                if (icd != null)
                {
                    for (int i = 0; i < this.neuFpEnter1_Sheet1.RowCount; i++)
                    {
                        if (icd.ID == this.neuFpEnter1_Sheet1.Cells[i, 1].Text.Trim())
                        {
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("�б����Ѵ���" + icd.Name + "��"));
                            return;
                        }
                    }
                    this.txtInput.Tag = icd;

                    this.neuFpEnter1_Sheet1.Rows.Add(0, 1);
                    this.neuFpEnter1_Sheet1.Cells[0, 0].Text = icd.KeyCode.ToString();
                    this.neuFpEnter1_Sheet1.Cells[0, 1].Text = icd.ID.ToString();
                    this.neuFpEnter1_Sheet1.Cells[0, 2].Text = icd.Name.ToString();
                    this.neuFpEnter1_Sheet1.Cells[0, 3].Text = this.currOper.Name.ToString();
                    this.neuFpEnter1_Sheet1.Cells[0, 4].Text = DateTime.Now.ToString();
                    this.neuFpEnter1_Sheet1.Cells[0, 5].Value = icd.IsValid;
                    this.neuFpEnter1_Sheet1.Cells[0, 6].Text = "NEW";

                    this.neuFpEnter1_Sheet1.Rows[0].Tag = icd;
                }
                this.ucDiagnose1.Visible = false;

                e.Handled = true;
                
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.ucDiagnose1.Visible = false;
            }
        }

        private void txtInput_Enter(object sender, EventArgs e)
        {

        }

        private void txtInput_Leave(object sender, EventArgs e)
        {
            //this.ucDiagnose1.Visible = false;//{3198206F-57BD-4937-BAC2-790257626D6F}
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            this.ucDiagnose1.Visible = true;
            string strFilter = string.Empty;
            strFilter = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtInput.Text);

            this.ucDiagnose1.Filter(strFilter);
            if (this.txtInput.Text.Trim() == "")
            {
                this.ucDiagnose1.Visible = true;
            }
        }

        private void ucDeptDiagMaintenance_Load(object sender, EventArgs e)
        {
            this.InitControl();
            this.deptID = this.currOper.Dept.ID;
            this.QueryDeptDiag(this.deptID);
        }

        #endregion
                
    }
}
