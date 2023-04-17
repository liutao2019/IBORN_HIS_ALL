using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.HealthRecord.CaseLend
{
    /// <summary>
    /// ucCardManage<br></br>
    /// [��������: �������Ŀ���Ϣ¼��]<br></br>
    /// [�� �� ��: �ſ���]<br></br>
    /// [����ʱ��: 2007-04-20]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucCardManage : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCardManage()
        {
            InitializeComponent();
        }

        #region ȫ�ֱ���
        private FS.HISFC.BizLogic.HealthRecord.CaseCard card = new FS.HISFC.BizLogic.HealthRecord.CaseCard();
        private System.Data.DataSet ds = new System.Data.DataSet();
        private frmCaseCard frm = new frmCaseCard(); //���Ŀ�ά������
        #endregion

        #region ���������Ӱ�ť�����¼�

        /// <summary>
        /// ���������Ӱ�ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "����":
                    this.AddInfo();
                    break;
                case "�޸�":
                    this.ModifyInfo();
                    break;��
                default:
                    break;
            }
        }
        #endregion

        #region ��ʼ��������
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����", "����", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            toolBarService.AddToolButton("�޸�", "�޸�", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sˢ��, true, false, null);
            //toolBarService.AddToolButton("ɾ��", "ɾ��", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            //toolBarService.AddToolButton("��ӡ", "��ӡ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            return toolBarService;
        }
        #endregion

        private void frmCardManage_Load(object sender, System.EventArgs e)
        {
            if (card.GetCardInfo(ref ds) != -1)
            {
                this.fpSpread1_Sheet1.DataSource = ds;
            }
            LockFp();
            FS.HISFC.BizLogic.Manager.Person person = new FS.HISFC.BizLogic.Manager.Person();
            //��ȡ��Ա�б�
            ArrayList DoctorList = person.GetEmployeeAll();
            frm.SetPersonList(DoctorList);

            FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();
            //��ȡ�����б�
            ArrayList deptList = dept.GetInHosDepartment();
            frm.SetDeptList(deptList);
            frm.SaveHandle +=new frmCaseCard.SaveInfo(frm_SaveHandle);
        }
        /// <summary>
        /// ���� ��Ԫ��Ŀ�� 
        /// </summary>
        private void LockFp()
        {
            this.fpSpread1_Sheet1.Columns[0].Width = 80;
            this.fpSpread1_Sheet1.Columns[1].Width = 80;
            this.fpSpread1_Sheet1.Columns[2].Width = 80;
            this.fpSpread1_Sheet1.Columns[3].Width = 80;
            this.fpSpread1_Sheet1.Columns[4].Width = 80;
            this.fpSpread1_Sheet1.Columns[5].Width = 80;
            this.fpSpread1_Sheet1.Columns[6].Width = 80;
            this.fpSpread1_Sheet1.Columns[7].Width = 80;
            this.fpSpread1_Sheet1.Columns[8].Width = 50;
            this.fpSpread1_Sheet1.Columns[9].Width = 80;
            this.fpSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
        }

        /// <summary>
        /// ���� 
        /// </summary>
        private void AddInfo()
        {
            frm.ClearInfo();
            frm.EditType = FS.HISFC.Models.HealthRecord.EnumServer.EditTypes.Add;
            frm.Text = "����";
            frm.Visible = true;
        }
        /// <summary>
        /// �޸� 
        /// </summary>
        private void ModifyInfo()
        {
            if (this.fpSpread1_Sheet1.Rows.Count == 0)
            {
                return;
            }
            string str = this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 0].Text;
            FS.HISFC.Models.HealthRecord.ReadCard info = card.GetCardInfo(str);
            if (info.CardID == null || info.CardID == "")
            {
                MessageBox.Show("��ѯ���ݿ�ʧ��");
                return;
            }
            frm.EditType = FS.HISFC.Models.HealthRecord.EnumServer.EditTypes.Modify;
            frm.SetInfo(info);
            frm.Text = "�޸�";
            frm.Visible = true;
        }
        /// <summary>
        /// ɾ�� 
        /// </summary>
        private void delete()
        {
            if (this.fpSpread1_Sheet1.Rows.Count == 0)
            {
                MessageBox.Show("û�п�ɾ��������");
                return;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        private void frm_SaveHandle(FS.HISFC.Models.HealthRecord.ReadCard obj)
        {
            if (frm.EditType == FS.HISFC.Models.HealthRecord.EnumServer.EditTypes.Add)
            {

                int i = this.fpSpread1_Sheet1.Rows.Count;
                this.fpSpread1_Sheet1.Rows.Add(i, 1);
                this.fpSpread1_Sheet1.Cells[i, 0].Text = obj.CardID; //����
                this.fpSpread1_Sheet1.Cells[i, 1].Text = obj.EmployeeInfo.ID; //Ա����
                this.fpSpread1_Sheet1.Cells[i, 2].Text = obj.EmployeeInfo.Name;//Ա������
                this.fpSpread1_Sheet1.Cells[i, 3].Text = obj.DeptInfo.ID;//���Ҵ���
                this.fpSpread1_Sheet1.Cells[i, 4].Text = obj.DeptInfo.Name;//��������
                this.fpSpread1_Sheet1.Cells[i, 5].Text = obj.User01;//����Ա
                this.fpSpread1_Sheet1.Cells[i, 6].Text = obj.EmployeeInfo.OperTime.ToString();//����ʱ��
                if (obj.ValidFlag == "1")
                {
                    this.fpSpread1_Sheet1.Cells[i, 7].Text = "��Ч";//��Ч
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[i, 7].Text = "��Ч";//��Ч
                }
                this.fpSpread1_Sheet1.Cells[i, 8].Text = obj.CancelOperInfo.Name;//������
                this.fpSpread1_Sheet1.Cells[i, 9].Text = obj.CancelDate.ToString();//����ʱ��
            }
            else
            {
                int i = fpSpread1_Sheet1.ActiveRowIndex;
                this.fpSpread1_Sheet1.Cells[i, 0].Text = obj.CardID; //����
                this.fpSpread1_Sheet1.Cells[i, 1].Text = obj.EmployeeInfo.ID; //Ա����
                this.fpSpread1_Sheet1.Cells[i, 2].Text = obj.EmployeeInfo.Name;//Ա������
                this.fpSpread1_Sheet1.Cells[i, 3].Text = obj.DeptInfo.ID;//���Ҵ���
                this.fpSpread1_Sheet1.Cells[i, 4].Text = obj.DeptInfo.Name;//��������
                this.fpSpread1_Sheet1.Cells[i, 5].Text = obj.User01;//����Ա
                this.fpSpread1_Sheet1.Cells[i, 6].Text = obj.EmployeeInfo.OperTime.ToString();//����ʱ��
                if (obj.ValidFlag == "1")
                {
                    this.fpSpread1_Sheet1.Cells[i, 7].Text = "��Ч";//��Ч
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[i, 7].Text = "��Ч";//��Ч
                }
                this.fpSpread1_Sheet1.Cells[i, 8].Text = obj.CancelOperInfo.Name;//������
                this.fpSpread1_Sheet1.Cells[i, 9].Text = obj.CancelDate.ToString();//����ʱ��
            }
        }
        /// <summary>
        /// ��ӡ
        /// </summary>
        private void PrintInfo()
        {
            try
            {

                FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
                p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
                p.PrintPreview(panel2);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            //if (keyData.GetHashCode() == Keys.F1.GetHashCode())
            //{
            //    this.AddInfo();
            //}
            //else if (keyData.GetHashCode() == Keys.F2.GetHashCode())
            //{
            //    this.ModifyInfo();
            //}
            //else if (keyData.GetHashCode() == Keys.F9.GetHashCode())
            //{
            //    this.PrintInfo();
            //}
            return base.ProcessDialogKey(keyData);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.PrintInfo();
            return base.OnPrint(sender, neuObject);
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //			this.ModifyInfo();
        }

    }
}