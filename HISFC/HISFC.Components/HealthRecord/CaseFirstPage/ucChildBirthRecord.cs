using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
{
    public partial class ucChildBirthRecord : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucChildBirthRecord()
        {
            InitializeComponent();
        }

        #region ����

        FS.FrameWork.Public.ObjectHelper objSex = new FS.FrameWork.Public.ObjectHelper();
        FS.FrameWork.Public.ObjectHelper womenKind = new FS.FrameWork.Public.ObjectHelper();
        FS.FrameWork.Public.ObjectHelper familyPlanning = new FS.FrameWork.Public.ObjectHelper();
        FS.FrameWork.Public.ObjectHelper breakLevel = new FS.FrameWork.Public.ObjectHelper();

        FS.HISFC.BizProcess.Integrate.Manager constant = new FS.HISFC.BizProcess.Integrate.Manager();

        FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
        FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        FS.HISFC.BizLogic.HealthRecord.ChildbirthRecord record = new FS.HISFC.BizLogic.HealthRecord.ChildbirthRecord();

        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get
            {
                return patientInfo;
            }
            set
            {
                this.patientInfo = value;
                Init();
                this.SetpatientInfo(patientInfo);
                QueryData();
                
            }
        }


        #endregion

        #region ����


        protected void Init()
        {
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            //�����Ա������б�
            FarPoint.Win.Spread.CellType.ComboBoxCellType celType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            objSex.ArrayObject = FS.HISFC.Models.Base.SexEnumService.List();
            string[] str = new string[objSex.ArrayObject.Count];
            for (int i = 0; i < objSex.ArrayObject.Count; i++)
            {
                str[i] = this.objSex.ArrayObject[i].ToString();
            }
            celType.Items = str;
            this.neuSpread1_Sheet1.Columns[7].CellType = celType;

            //���ɼƻ����������б�

            FarPoint.Win.Spread.CellType.ComboBoxCellType celType1 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            this.familyPlanning.ArrayObject = this.constant.GetConstantList("FAMILYPLANNING");
            string[] str1 = new string[this.familyPlanning.ArrayObject.Count];
            for (int i = 0; i < this.familyPlanning.ArrayObject.Count; i++)
            {
                str1[i] = this.familyPlanning.ArrayObject[i].ToString();
            }
            celType1.Items = str1;
            this.neuSpread1_Sheet1.Columns[2].CellType = celType1;

            //���ɲ������������б�

            FarPoint.Win.Spread.CellType.ComboBoxCellType celType2 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            this.womenKind.ArrayObject = this.constant.GetConstantList("WOMENKIND");
            string[] str2 = new string[this.womenKind.ArrayObject.Count];
            for (int i = 0; i < this.womenKind.ArrayObject.Count; i++)
            {
                str2[i] = this.womenKind.ArrayObject[i].ToString();
            }
            celType2.Items = str2;
            this.neuSpread1_Sheet1.Columns[4].CellType = celType2;

            //�������ѳ̶������б�

            FarPoint.Win.Spread.CellType.ComboBoxCellType celType3 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            this.breakLevel.ArrayObject = this.constant.GetConstantList("BREAKLEVEL");
            string[] str3 = new string[this.breakLevel.ArrayObject.Count];
            for (int i = 0; i < this.breakLevel.ArrayObject.Count; i++)
            {
                str3[i] = this.breakLevel.ArrayObject[i].ToString();
            }
            celType3.Items = str3;
            this.neuSpread1_Sheet1.Columns[6].CellType = celType3;
        }

        /// <summary>
        /// ������ʾ������Ϣ
        /// </summary>
        /// <param name="patientInfo">������Ϣʵ��</param>
        protected virtual void SetpatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.txtName.Text = patientInfo.Name;
            this.txtDept.Text = patientInfo.PVisit.PatientLocation.Dept.Name;
            this.txtNurseStation.Text = patientInfo.PVisit.PatientLocation.NurseCell.Name;
            this.txtDoctor.Text = patientInfo.PVisit.AdmittingDoctor.Name;
            this.txtBirthday.Text = patientInfo.Birthday.ToShortDateString();
            this.txtBedNo.Text = patientInfo.PVisit.PatientLocation.Bed.ID;
            this.txtDateIn.Text = patientInfo.PVisit.InTime.ToShortDateString();
            this.txtPact.Text = patientInfo.Pact.Name;
            this.ucQueryInpatientNo1.Text = this.patientInfo.ID.Substring(4,10);
        }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        private void Clear()
        {
            this.ucQueryInpatientNo1.Text = "";
            this.txtName.Text = "";
            this.txtDept.Text = "";
            this.txtNurseStation.Text = "";
            this.txtDoctor.Text = "";
            this.txtBirthday.Text = "";
            this.txtBedNo.Text = "";
            this.txtDateIn.Text = "";
            this.txtPact.Text = "";
            this.neuSpread1_Sheet1.Rows.Count = 0;
        }
      /// <summary>
      /// ��ѯ���߷����¼
      /// </summary>
        private void QueryData()
        {
            //{6DE13BEE-0219-427b-A798-5F0309EDAA00}
            this.neuSpread1_Sheet1.RowCount = 0;

            List<FS.HISFC.Models.HealthRecord.ChildbirthRecord> alRecord = new List<FS.HISFC.Models.HealthRecord.ChildbirthRecord>();
            if (this.patientInfo.ID == "" || this.patientInfo.ID == null)
            {
                return;
            }
            alRecord = this.record.QueryChildbirthRecord(this.patientInfo.ID);
            for (int i = 0; i < alRecord.Count; i++)
            {
                //this.neuSpread1_Sheet1.Rows.Count = ;
                this.neuSpread1_Sheet1.Rows.Add(i, 1);
                this.neuSpread1_Sheet1.Cells[i, 0].Text = alRecord[i].IsNormalChildbirth ? "��" : "��";
                this.neuSpread1_Sheet1.Cells[i, 1].Text = alRecord[i].IsDystocia ? "��" : "��";
                this.neuSpread1_Sheet1.Cells[i, 2].Text = this.familyPlanning.GetName(alRecord[i].FamilyPlanning.ID);
                this.neuSpread1_Sheet1.Cells[i, 3].Text = alRecord[i].IsPerineumBreak ? "��" : "��";
                this.neuSpread1_Sheet1.Cells[i, 4].Text = this.womenKind.GetName(alRecord[i].WomenKind.ID);
                this.neuSpread1_Sheet1.Cells[i, 5].Text = alRecord[i].IsBreak ? "��" : "��";
                this.neuSpread1_Sheet1.Cells[i, 6].Text = this.breakLevel.GetName(alRecord[i].BreakLevel.ID);
                this.neuSpread1_Sheet1.Cells[i, 7].Text = this.objSex.GetName(alRecord[i].BabySex.ToString());
                this.neuSpread1_Sheet1.Cells[i, 8].Value = alRecord[i].BabyWeight;
            }
        }

        /// <summary>
        /// ���ӷ����¼
        /// </summary>
        private void AddRow()
        {
            if (this.patientInfo.ID == "" || this.patientInfo.ID ==null )
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��������ȷ��סԺ�ţ����س���ȷ�Ϻ������ӣ�"));
                return;
            }

            int rowCount = this.neuSpread1_Sheet1.Rows.Count;

            if (rowCount >= 1)
            {
                if (string.IsNullOrEmpty( this.neuSpread1_Sheet1.Cells[rowCount - 1, 0].Text ) == true)
                {
                    MessageBox.Show("����ѡ���������");
                    return;
                }
            }

            this.neuSpread1_Sheet1.Rows.Add( rowCount, 1 );

            //{EFDD586D-A0B1-4bbf-85D1-641247533CDF}
            this.neuSpread1_Sheet1.ActiveRowIndex = rowCount;
        }

        /// <summary>
        /// ɾ����
        /// </summary>
        private void DeleteRow()
        {
            if (this.neuSpread1_Sheet1.RowCount > 0 && this.neuSpread1_Sheet1.ActiveRowIndex >= 0)
            {
                this.neuSpread1.ActiveSheet.Rows.Remove(this.neuSpread1.ActiveSheet.ActiveRowIndex, 1);
                return;
            }
        }

        /// <summary>
        /// ����Ƿ��п�������
        /// {7A631C83-427E-4dd0-9281-BF468CE113A9}
        /// </summary>
        /// <returns></returns>
        private bool CheckHaveNewData()
        {
            bool isHave = false;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                int j = 0;
                if (string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, 0].Text))
                {
                    j++;
                }
                if (string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, 1].Text))
                {
                    j++;
                }
                if (string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, 2].Text))
                {
                    j++;
                }
                if (string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, 3].Text))
                {
                    j++;
                }
                if (string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, 4].Text))
                {
                    j++;
                }
                if (string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, 5].Text))
                {
                    j++;
                }
                if (string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, 6].Text))
                {
                    j++;
                }
                if (string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, 7].Text))
                {
                    j++;
                }
                if (string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, 8].Text))
                {
                    j++;
                }

                if (j >= 9)
                {
                    isHave = true;
                }

                if (isHave)
                {
                    break;
                } 
            }
            return isHave;
        }

        /// <summary>
        /// ��������
        /// </summary>
        private void SaveData()
        {
            if (this.patientInfo.ID == "" || this.patientInfo.ID == null)
            {
                return;
            }

            #region {7A631C83-427E-4dd0-9281-BF468CE113A9}
            if (this.CheckHaveNewData())
            {
                MessageBox.Show("�������������Ŀ��У�");
                return;
            } 
            #endregion

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.record.SetTrans( FS.FrameWork.Management.PublicTrans.Trans );

            try
            {
                //���ԭ������
                if (this.record.DeleteAllByInpatientNO( this.patientInfo.ID ) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show( this, "���ݱ������" + this.record.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Information );
                    return;
                }

                //װ��������
                FS.HISFC.Models.HealthRecord.ChildbirthRecord obj = new FS.HISFC.Models.HealthRecord.ChildbirthRecord();
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {

                    obj.Patient = this.patientInfo;
                    obj.IsNormalChildbirth = this.neuSpread1_Sheet1.Cells[i, 0].Text == "��" ? true : false;
                    obj.IsDystocia = this.neuSpread1_Sheet1.Cells[i, 1].Text == "��" ? true : false;
                    obj.FamilyPlanning.ID = this.familyPlanning.GetID( this.neuSpread1_Sheet1.Cells[i, 2].Text.ToString() );
                    obj.IsPerineumBreak = this.neuSpread1_Sheet1.Cells[i, 3].Text == "��" ? true : false;
                    obj.WomenKind.ID = this.womenKind.GetID( this.neuSpread1_Sheet1.Cells[i, 4].Text.ToString() );
                    obj.IsBreak = this.neuSpread1_Sheet1.Cells[i, 5].Text == "��" ? true : false;
                    obj.BreakLevel.ID = this.breakLevel.GetID( this.neuSpread1_Sheet1.Cells[i, 6].Text.ToString() );

                    if (this.neuSpread1_Sheet1.Cells[i, 7].Value != null)
                    {
                        switch (this.neuSpread1_Sheet1.Cells[i, 7].Value.ToString())
                        {
                            //{0E9F748A-DE93-4642-9A4F-075DE33CCEF6}

                            case "ȫ��":
                                obj.BabySex = FS.HISFC.Models.Base.EnumSex.A;
                                break;
                            case "Ů":
                                obj.BabySex = FS.HISFC.Models.Base.EnumSex.F;
                                break;
                            case "��":
                                obj.BabySex = FS.HISFC.Models.Base.EnumSex.M;
                                break;
                            case "����":
                                obj.BabySex = FS.HISFC.Models.Base.EnumSex.O;
                                break;
                            case "δ֪":
                                obj.BabySex = FS.HISFC.Models.Base.EnumSex.U;
                                break;
                        }
                    }
                    obj.BabyWeight = FS.FrameWork.Function.NConvert.ToDecimal( this.neuSpread1_Sheet1.Cells[i, 8].Value );

                    //���ݸ���
                    if (this.record.Insert( obj ) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        if (this.record.DBErrCode == 1)
                        {
                            this.neuSpread1_Sheet1.ActiveRowIndex = i;
                            MessageBox.Show( "�����ظ�,������ͬ�ļ�¼����.��ά����ͬ�ļ�¼.", "������ʾ" );
                        }
                        else
                        {
                            this.neuSpread1_Sheet1.ActiveRowIndex = i;
                            MessageBox.Show( this, "���ݱ������" + this.record.Err, "�������", MessageBoxButtons.OK, MessageBoxIcon.Information );
                        }
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show( e.Message );
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show( "����ɹ���" );
        }

        #endregion

        #region �¼�
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucChildBirthRecord_Load(object sender, EventArgs e)
        {
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            //�����Ա������б�
            FarPoint.Win.Spread.CellType.ComboBoxCellType celType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            objSex.ArrayObject = FS.HISFC.Models.Base.SexEnumService.List();
            string[] str = new string[objSex.ArrayObject.Count];
            for (int i = 0; i < objSex.ArrayObject.Count; i++)
            {
                str[i] = this.objSex.ArrayObject[i].ToString();
            }
            celType.Items = str;
            this.neuSpread1_Sheet1.Columns[7].CellType = celType;

            //���ɼƻ����������б�
            
            FarPoint.Win.Spread.CellType.ComboBoxCellType celType1 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            this.familyPlanning.ArrayObject = this.constant.GetConstantList("FAMILYPLANNING");
            string[] str1 = new string[this.familyPlanning.ArrayObject.Count];
            for (int i = 0; i < this.familyPlanning.ArrayObject.Count; i++)
            {
                str1[i] = this.familyPlanning.ArrayObject[i].ToString();
            }
            celType1.Items = str1;
            this.neuSpread1_Sheet1.Columns[2].CellType = celType1;

            //���ɲ������������б�

            FarPoint.Win.Spread.CellType.ComboBoxCellType celType2 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            this.womenKind.ArrayObject = this.constant.GetConstantList("WOMENKIND");
            string[] str2 = new string[this.womenKind.ArrayObject.Count];
            for (int i = 0; i < this.womenKind.ArrayObject.Count; i++)
            {
                str2[i] = this.womenKind.ArrayObject[i].ToString();
            }
            celType2.Items = str2;
            this.neuSpread1_Sheet1.Columns[4].CellType = celType2;

            //�������ѳ̶������б�

            FarPoint.Win.Spread.CellType.ComboBoxCellType celType3 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            this.breakLevel.ArrayObject = this.constant.GetConstantList("BREAKLEVEL");
            string[] str3 = new string[this.breakLevel.ArrayObject.Count];
            for (int i = 0; i < this.breakLevel.ArrayObject.Count; i++)
            {
                str3[i] = this.breakLevel.ArrayObject[i].ToString();
            }
            celType3.Items = str3;
            this.neuSpread1_Sheet1.Columns[6].CellType = celType3;
        }

        /// <summary>
        /// ��ѯ�¼�
        /// </summary>
        private void ucQueryInpatientNo1_myEvent()
        {
            this.Clear();
            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo.Trim() == "")
            {
                if (this.ucQueryInpatientNo1.Err == "")
                {
                    this.ucQueryInpatientNo1.Err = "�˻��߲���Ժ";
                }
                FS.FrameWork.WinForms.Classes.Function.Msg(this.ucQueryInpatientNo1.Err, 211);

                this.ucQueryInpatientNo1.Focus();
                return;
            }
            this.patientInfo = this.radtIntegrate.GetPatientInfomation(this.ucQueryInpatientNo1.InpatientNo);
            //������ʾ������Ϣ
            this.SetpatientInfo(this.patientInfo);
            //��ʾ�������
            this.QueryData();
        }

        #endregion 

        #region ��������Ϣ

        /// <summary>
        /// ���幤��������
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="NeuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object NeuObject, object param)
        {
            //���ӹ�����
            this.toolBarService.AddToolButton("����", "����", FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            this.toolBarService.AddToolButton("ɾ��", "ɾ��", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            return this.toolBarService;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveData();
            return base.OnSave(sender, neuObject);
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryData();
            return base.OnQuery(sender, neuObject);
        }

        /// <summary>
        /// ��������ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "����":
                    this.AddRow();
                    break;
                case "ɾ��":
                    this.DeleteRow();
                    break;
            }

        }

        #endregion

        private void neuButton1_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void neuButton2_Click(object sender, EventArgs e)
        {
            this.AddRow();
        }

        private void neuButton3_Click(object sender, EventArgs e)
        {
            this.DeleteRow();
        }

   



    }
}
