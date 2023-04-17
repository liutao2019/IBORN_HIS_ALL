using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.HealthRecord.Search
{
    public partial class ucCaseTrack : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCaseTrack()
        {
            InitializeComponent();
        }
        FS.HISFC.BizLogic.HealthRecord.Base baseMgr = new FS.HISFC.BizLogic.HealthRecord.Base();
        FS.HISFC.BizLogic.HealthRecord.CaseCard caseCard = new FS.HISFC.BizLogic.HealthRecord.CaseCard();
        FS.FrameWork.Public.ObjectHelper objSex = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            Query();
            return base.OnQuery(sender, neuObject);
        }
        /// <summary>
        /// ����Ա
        /// </summary>
        private enum EnumCols
        {
            OperTime, //����ʱ��
            InpatientNO,//סԺ��ˮ��
            Name,//����
            Sex,//�Ա�
            CaseState, //״̬
            LendTime, //����ʱ��
            ReturnTime // �黹ʱ��
        }
        /// <summary>
        /// ��ѯ
        /// </summary>
        private void Query()
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;

            string strCaseNO = txtCaseNO.Text.PadLeft(10, '0');
            ArrayList list = baseMgr.QueryCaseBaseInfoByCaseNO(strCaseNO);
            if (list == null)
            {
                MessageBox.Show("��ѯ����ʧ�� " + baseMgr.Err);
                return;
            } 

            if(list.Count == 0)
            {
                MessageBox.Show("û�в�����Ϣ");
                return;
            }
            #region ����������Ϣ
            foreach (FS.HISFC.Models.HealthRecord.Base obj in list)
            {
                this.neuSpread1_Sheet1.Rows.Add(0, 1);
                //����ʱ��
                this.neuSpread1_Sheet1.Cells[0, (int)EnumCols.OperTime].Text = obj.OperInfo.OperTime.ToShortDateString();
                //סԺ��ˮ��
                this.neuSpread1_Sheet1.Cells[0, (int)EnumCols.InpatientNO].Text = obj.PatientInfo.ID;
                //����
                this.neuSpread1_Sheet1.Cells[0, (int)EnumCols.Name].Text = obj.PatientInfo.Name;
                string SexID = "";

                if (obj.PatientInfo.Sex.ID != null)
                {
                    SexID = obj.PatientInfo.Sex.ID.ToString();
                }
                //�Ա�
                this.neuSpread1_Sheet1.Cells[0, (int)EnumCols.Sex].Text = objSex.GetName(SexID);
                if (obj.PatientInfo.CaseState == "1")
                {
                    //״̬
                    this.neuSpread1_Sheet1.Cells[0, (int)EnumCols.CaseState].Text = "û���γɲ���";
                }
                else if (obj.PatientInfo.CaseState == "2")
                {
                    //״̬
                    this.neuSpread1_Sheet1.Cells[0, (int)EnumCols.CaseState].Text = "ҽ��վ�γɲ���";
                }
                else if (obj.PatientInfo.CaseState == "3")
                {
                    //״̬
                    this.neuSpread1_Sheet1.Cells[0, (int)EnumCols.CaseState].Text = "�������γɲ���";
                }
                else if (obj.PatientInfo.CaseState == "4")
                {
                    //״̬
                    this.neuSpread1_Sheet1.Cells[0, (int)EnumCols.CaseState].Text = "�������";
                }
            }
            #endregion 

            ArrayList cardList = caseCard.QueryLendInfoByCaseNO(strCaseNO);
            if (cardList == null)
            {
                MessageBox.Show("��ѯ����������Ϣʧ�� " + baseMgr.Err);
                return;
            }
            if (cardList.Count == 0)
            {
                return;
            }
            #region ����������Ϣ
            foreach (FS.HISFC.Models.HealthRecord.Lend info in cardList)
            {
                this.neuSpread1_Sheet1.Rows.Add(0, 1);
                //����ʱ��
                this.neuSpread1_Sheet1.Cells[0, (int)EnumCols.OperTime].Text = info.OperInfo.OperTime.ToShortDateString();
                //סԺ��ˮ��
                this.neuSpread1_Sheet1.Cells[0, (int)EnumCols.InpatientNO].Text = info.CaseBase.PatientInfo.ID;
                //����
                this.neuSpread1_Sheet1.Cells[0, (int)EnumCols.Name].Text = info.CaseBase.PatientInfo.Name;
                string SexID = "";

                if (info.CaseBase.PatientInfo.Sex.ID != null)
                {
                    SexID = info.CaseBase.PatientInfo.Sex.ID.ToString();
                }
                //�Ա�
                this.neuSpread1_Sheet1.Cells[0, (int)EnumCols.Sex].Text = objSex.GetName(SexID);
                if (info.LendStus == "1")
                {
                    //״̬
                    this.neuSpread1_Sheet1.Cells[0, (int)EnumCols.CaseState].Text = "�������";
                }
                else if (info.LendStus == "2")
                {
                    //״̬
                    this.neuSpread1_Sheet1.Cells[0, (int)EnumCols.CaseState].Text = "���ĺ󷵻�";
                    //�黹����
                    this.neuSpread1_Sheet1.Cells[0, (int)EnumCols.ReturnTime].Text = info.ReturnDate.ToShortDateString();
                }
                //��������
                this.neuSpread1_Sheet1.Cells[0, (int)EnumCols.LendTime].Text = info.LendDate.ToShortDateString();
               
            }
            #endregion 

        }

        private void ucCaseTrack_Load(object sender, EventArgs e)
        {
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            objSex.ArrayObject = FS.HISFC.Models.Base.SexEnumService.List();
            FarPoint.Win.Spread.CellType.TextCellType txtCellType = new FarPoint.Win.Spread.CellType.TextCellType();
            txtCellType.ReadOnly = true;
            this.neuSpread1_Sheet1.Columns[(int)EnumCols.OperTime].CellType = txtCellType;
            this.neuSpread1_Sheet1.Columns[(int)EnumCols.InpatientNO].CellType = txtCellType;
            this.neuSpread1_Sheet1.Columns[(int)EnumCols.Name].CellType = txtCellType;
            this.neuSpread1_Sheet1.Columns[(int)EnumCols.Sex].CellType = txtCellType;
            this.neuSpread1_Sheet1.Columns[(int)EnumCols.CaseState].CellType = txtCellType;
            this.neuSpread1_Sheet1.Columns[(int)EnumCols.LendTime].CellType = txtCellType;
            this.neuSpread1_Sheet1.Columns[(int)EnumCols.ReturnTime].CellType = txtCellType;
        }

        private void txtCaseNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData.GetHashCode() == Keys.Enter.GetHashCode())
            {
                this.Query();
            }
        }
    }
}
