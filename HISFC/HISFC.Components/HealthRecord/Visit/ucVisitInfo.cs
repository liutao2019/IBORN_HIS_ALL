using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.BizLogic.HealthRecord.Visit;
using FS.HISFC.Components.HealthRecord.CaseFirstPage;
using System.Collections;

namespace FS.HISFC.Components.HealthRecord.Visit
{

    /// <summary>
    /// [��������: ����û�����Ϣ]<br></br>
    /// [������:   ���]<br></br>
    /// [����ʱ��: 2009-09-15]<br></br>
    /// <˵��>
    ///    
    /// </˵��>
    /// </summary>
    public partial class ucVisitInfo : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 
        /// </summary>
        public ucVisitInfo()
        {
            InitializeComponent();
        }

        #region ��������Ϣ

        /// <summary>
        /// ���幤��������
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #region ��ʼ��������
        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����", "����", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            toolBarService.AddToolButton("����б�", "����б�", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sˢ��, true, false, null);
            return toolBarService;
        }
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
                    DeleteRecord();
                    break;
                case "����б�":
                    RefreshVisitList();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #endregion

        #region ����

        /// <summary>
        /// ���ҵ����

        /// </summary>
        VisitRecord visitRecordManager = new VisitRecord();

        //��־ ��ʶ��ҽ��վ�û��ǲ�������
        private FS.HISFC.Models.HealthRecord.EnumServer.frmTypes frmType = FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC;

        HealthRecord.Search.ucPatientList ucPatient = new HealthRecord.Search.ucPatientList();

        

        #endregion

        #region �¼�

        private void ucVisitInfo_Load(object sender, EventArgs e)
        {
            ucPatient.Visible = false;
            this.ucPatient.SelectItem += new HealthRecord.Search.ucPatientList.ListShowdelegate(ucPatient_SelectItem);

            this.Controls.Add(this.ucPatient);

            LoadVisitList();

            SetVisitRecordStyle();
        }

        private void fp_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

            string txtPatientNO = this.fp_Sheet1.Cells[this.fp_Sheet1.ActiveRowIndex, 0].Value.ToString().Trim().PadLeft(10, '0');

            if (!this.ucPatient.Visible)
            {
                #region ��ѯ
                ArrayList list = null;
                list = ucPatient.Init(txtPatientNO, "2");
                if (list == null)
                {
                    MessageBox.Show("��ѯʧ��" + ucPatient.strErr);
                    return;
                }
                if (list.Count == 0)
                {
                    MessageBox.Show("û�в鵽��ز�����Ϣ,�Ƿ��ֹ�¼�벡��", "��ʾ");
                }
                else if (list.Count == 1) //ֻ��һ����Ϣ
                {
                    ucPatient.Visible = false;
                    FS.HISFC.Models.HealthRecord.Base obj = this.ucPatient.GetCaseInfo();
                    if (obj != null)
                    {
                        ShowCaseMainInfo(obj.PatientInfo.ID);   
                    }
                }
                else //������Ϣ 
                {
                    this.ucPatient.Location = new Point(Control.MousePosition.X, Control.MousePosition.Y);
                    this.ucPatient.BringToFront();
                    this.ucPatient.Visible = true;
                }
                #endregion
            }
            else
            {
                #region  ѡ��
                FS.HISFC.Models.HealthRecord.Base obj = this.ucPatient.GetCaseInfo();
                if (obj != null)
                {
                    ShowCaseMainInfo(obj.PatientInfo.ID);
                }
                this.ucPatient.Visible = false;
                #endregion
            }

        }

        void ucPatient_SelectItem(FS.HISFC.Models.HealthRecord.Base HealthRecord)
        {
            ShowCaseMainInfo(HealthRecord.PatientInfo.ID);
        }

        private void fp_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //��ǰѡ�����û��߲�����
            string cardNo = fp_Sheet1.Cells[e.Row, 1].Text.Trim();
          

            if (!string.IsNullOrEmpty(cardNo))
            {
                if (QueryVisitRecordByCardNo(cardNo) == -1)
                {
                    MessageBox.Show("��ϸ��Ϣ��ѯʧ��");
                }
            }
            

        }

        private void tsmDel_Click(object sender, EventArgs e)
        {
            DeleteRecord();
        }

        #endregion

        #region ����

        /// <summary>
        /// ���ش�����û��б�
        /// </summary>
        private void LoadVisitList()
        {
            DataSet ds = new DataSet();//����û����б�
            if (visitRecordManager.PatQuery(ref ds) > -1)
            {
                this.fp_Sheet1.DataSource = ds.Tables[0].DefaultView;
            }
        }


        /// <summary>
        /// ���ز�����Ϣ
        /// </summary>
        /// <param name="patientNo"></param>
        private void ShowCaseMainInfo(string patientNo)
        {
            ucCaseMainInfo ucDetail = new ucCaseMainInfo();

            ucDetail.IsVisitInput = true;//�����ʶΪ���¼��

            ucDetail.PatientNo = this.fp_Sheet1.Cells[this.fp_Sheet1.ActiveRowIndex, 0].Value.ToString().Trim().PadLeft(10, '0');
            ucDetail.CardNo = this.fp_Sheet1.Cells[this.fp_Sheet1.ActiveRowIndex, 1].Value.ToString().Trim();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ���ݣ����Ժ�...");
            Application.DoEvents();

            ucDetail.LoadInfo(patientNo, this.frmType);

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            DialogResult result = FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucDetail);
        }

        /// <summary>
        /// ��ȡ�����ϸ��Ϣ
        /// </summary>
        /// <param name="cardNo">������</param>
        /// <returns>�ɹ����� 0;ʧ�ܷ��� -1</returns>
        private int QueryVisitRecordByCardNo(string cardNo)
        {
            //�����ϸ�б�
            ArrayList list = new ArrayList();
            list = visitRecordManager.QueryByCardNo(cardNo);

            if (list == null)
            {
                return 0;
            }
            fpVisitRecord_Sheet1.Rows.Count = list.Count;
            for (int i = 0; i < list.Count; i++)
            {
                FS.HISFC.Models.HealthRecord.Visit.VisitRecord visitRecord
                    = list[i] as FS.HISFC.Models.HealthRecord.Visit.VisitRecord;


                if (visitRecord != null)
                {
                    this.fpVisitRecord_Sheet1.Cells[i, 1].Text = visitRecord.VisitOper.OperTime.ToShortDateString();//���ʱ��

                    this.fpVisitRecord_Sheet1.Cells[i, 2].Text = visitRecord.LinkWay.LinkMan.Name;//��ϵ��

                    this.fpVisitRecord_Sheet1.Cells[i, 3].Text = visitRecord.VisitType.Name;//��÷�ʽ

                    this.fpVisitRecord_Sheet1.Cells[i, 4].Text = visitRecord.VisitResult.Name;//��ý��

                    this.fpVisitRecord_Sheet1.Cells[i, 5].Text = visitRecord.Circs.Name;//һ���������

                    this.fpVisitRecord_Sheet1.Cells[i, 6].Text = visitRecord.Symptom.Name;//֢״����


                    this.fpVisitRecord_Sheet1.Cells[i, 7].Text = visitRecord.IsDead ? "��" : "��";//�Ƿ�����

                    this.fpVisitRecord_Sheet1.Cells[i, 8].Text = visitRecord.DeadTime.ToString();//����ʱ��

                    this.fpVisitRecord_Sheet1.Cells[i, 9].Text = visitRecord.DeadReason.Name;//����ԭ��


                    this.fpVisitRecord_Sheet1.Cells[i, 10].Text = visitRecord.IsRecrudesce ? "��" : "��";//�Ƿ񸴷�

                    this.fpVisitRecord_Sheet1.Cells[i, 11].Text = visitRecord.RecrudesceTime.ToString();//����ʱ��


                    this.fpVisitRecord_Sheet1.Cells[i, 12].Text = visitRecord.IsSequela ? "��" : "��";//�Ƿ��к���֢

                    this.fpVisitRecord_Sheet1.Cells[i, 13].Text = visitRecord.Sequela.Name;//����֢


                    this.fpVisitRecord_Sheet1.Cells[i, 14].Text = visitRecord.IsTransfer ? "��" : "��";//�Ƿ�ת��

                    this.fpVisitRecord_Sheet1.Cells[i, 15].Text = visitRecord.TransferPosition.Name;//����֢


                    this.fpVisitRecord_Sheet1.Rows[i].Tag = visitRecord;

                }
            }


            return 0;
        }

        /// <summary>
        /// ���������ϸ�б��ʽ
        /// </summary>
        private void SetVisitRecordStyle()
        {
            this.fpVisitRecord_Sheet1.Columns[0].Label = "ѡ��";
            this.fpVisitRecord_Sheet1.Columns[1].Label = "���ʱ��";
            this.fpVisitRecord_Sheet1.Columns[2].Label = "��ϵ��";
            this.fpVisitRecord_Sheet1.Columns[3].Label = "��÷�ʽ";
            this.fpVisitRecord_Sheet1.Columns[4].Label = "��ý��";
            this.fpVisitRecord_Sheet1.Columns[5].Label = "һ���������";
            this.fpVisitRecord_Sheet1.Columns[6].Label = "֢״����";
            this.fpVisitRecord_Sheet1.Columns[7].Label = "�Ƿ�����";
            this.fpVisitRecord_Sheet1.Columns[8].Label = "����ʱ��";
            this.fpVisitRecord_Sheet1.Columns[9].Label = "����ԭ��";
            this.fpVisitRecord_Sheet1.Columns[10].Label = "�Ƿ񸴷�";
            this.fpVisitRecord_Sheet1.Columns[11].Label = "����ʱ��";
            this.fpVisitRecord_Sheet1.Columns[12].Label = "�Ƿ��к���֢";
            this.fpVisitRecord_Sheet1.Columns[13].Label = "����֢";
            this.fpVisitRecord_Sheet1.Columns[14].Label = "�Ƿ�ת��";
            this.fpVisitRecord_Sheet1.Columns[15].Label = "ת�Ʋ�λ";

            this.fpVisitRecord_Sheet1.Columns[0].Width = 40;
            this.fpVisitRecord_Sheet1.Columns[1].Width = 60;
            this.fpVisitRecord_Sheet1.Columns[2].Width = 80;
            this.fpVisitRecord_Sheet1.Columns[3].Width = 80;
            this.fpVisitRecord_Sheet1.Columns[4].Width = 60;
            this.fpVisitRecord_Sheet1.Columns[5].Width = 80;
            this.fpVisitRecord_Sheet1.Columns[6].Width = 80;
            this.fpVisitRecord_Sheet1.Columns[7].Width = 60;
            this.fpVisitRecord_Sheet1.Columns[8].Width = 120;
            this.fpVisitRecord_Sheet1.Columns[9].Width = 80;
            this.fpVisitRecord_Sheet1.Columns[10].Width = 60;
            this.fpVisitRecord_Sheet1.Columns[11].Width = 120;
            this.fpVisitRecord_Sheet1.Columns[12].Width = 80;
            this.fpVisitRecord_Sheet1.Columns[13].Width = 80;
            this.fpVisitRecord_Sheet1.Columns[14].Width = 60;
            this.fpVisitRecord_Sheet1.Columns[15].Width = 80;


            this.fpVisitRecord_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpVisitRecord_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpVisitRecord_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpVisitRecord_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpVisitRecord_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpVisitRecord_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpVisitRecord_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpVisitRecord_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpVisitRecord_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpVisitRecord_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpVisitRecord_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpVisitRecord_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpVisitRecord_Sheet1.Columns.Get(12).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpVisitRecord_Sheet1.Columns.Get(13).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpVisitRecord_Sheet1.Columns.Get(14).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpVisitRecord_Sheet1.Columns.Get(15).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;


        }

        /// <summary>
        /// ɾ����ü�¼��ϸ
        /// </summary>
        /// <returns>�ɹ����� 0;ʧ�ܷ��� -1</returns>
        private int DeleteRecord()
        {
            if (MessageBox.Show("�Ƿ�����ѡ�����ʷ�����Ϣ?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                == DialogResult.Yes)
            {

                visitRecordManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                for (int i = 0; i < fpVisitRecord_Sheet1.Rows.Count; i++)
                {
                    if (this.fpVisitRecord_Sheet1.Cells[i, 0].Text == true.ToString())
                    {
                        FS.HISFC.Models.HealthRecord.Visit.VisitRecord visitRecord
                    = fpVisitRecord_Sheet1.Rows[i].Tag
                    as FS.HISFC.Models.HealthRecord.Visit.VisitRecord;//Tag�еĶ���
                        if (visitRecord != null)
                        {
                            if (visitRecordManager.DelVisitRecord(visitRecord.ID) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                //t.RollBack();
                                MessageBox.Show("ɾ����ϵ�˷�������:" + visitRecordManager.Err);
                                return -1;
                            }                       
                        }
                    }
                }

                FS.FrameWork.Management.PublicTrans.Commit();

                #region ���¼��������ϸ
                
               
                if (fp_Sheet1.ActiveRowIndex > -1)
                {
                    //��ǰѡ�����û��߲�����
                    string cardNo = fp_Sheet1.Cells[fp_Sheet1.ActiveRowIndex, 1].Text.Trim();


                    if (!string.IsNullOrEmpty(cardNo))
                    {
                        if (QueryVisitRecordByCardNo(cardNo) == -1)
                        {
                            MessageBox.Show("��ϸ��Ϣ��ѯʧ��");
                        }
                    }
                }

                #endregion

            }

            return 0;
        }

        /// <summary>
        /// ������û����б�
        /// </summary>
        private void RefreshVisitList()
        {
            if (MessageBox.Show("�Ƿ��������ɴ�����û��б�?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                == DialogResult.Yes)
            {
                    //�ȴ�����
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ���ݣ����Ժ�...");
                    Application.DoEvents();

                    if (visitRecordManager.RefreshVisitList() > -1)
                    {
                        LoadVisitList();
                    }
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            }
        }

        #endregion

      

       




    }
}
