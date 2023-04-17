using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
{
    /// <summary>
    /// ucTumourCard<br></br>
    /// [��������: ����������Ϣ¼��]<br></br>
    /// [�� �� ��: �ſ���]<br></br>
    /// [����ʱ��: 2007-04-20]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucTumourCard : UserControl
    {
        public ucTumourCard()
        {
            InitializeComponent();
        }

        #region  ȫ�ֱ���
        //��ǰ������б�
        private FS.FrameWork.WinForms.Controls.PopUpListBox listBoxActive = new FS.FrameWork.WinForms.Controls.PopUpListBox();
        //��ǰ��ؼ�
        private System.Windows.Forms.Control contralActive = new Control();
        private DataTable dtTumour = new DataTable("����");
        private FS.FrameWork.Public.ObjectHelper diagnoseTypeHelper = new FS.FrameWork.Public.ObjectHelper();
        //�����ļ���·�� 
        private string filePath = Application.StartupPath + "\\profile\\ucTumourCard1.xml";
        //���˻�����Ϣ��
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
        //��λ�б�
        FS.FrameWork.Public.ObjectHelper UnitListHelper = new FS.FrameWork.Public.ObjectHelper();
        //�Ƴ��б�
        FS.FrameWork.Public.ObjectHelper PeriodListHelper = new FS.FrameWork.Public.ObjectHelper();
        //����б�
        FS.FrameWork.Public.ObjectHelper ResultListHelper = new FS.FrameWork.Public.ObjectHelper();
        //����б�
        FS.FrameWork.Public.ObjectHelper TumStageListHelper = new FS.FrameWork.Public.ObjectHelper();
        //��ǰѡ�е���Ϣ
        private FS.FrameWork.Models.NeuObject selectObj;
        
        //����ҩ��
        static  List<FS.HISFC.Models.Pharmacy.Item> druglist = new List<FS.HISFC.Models.Pharmacy.Item>();
        #endregion

        /// <summary>
        /// ������Ϣ
        /// </summary>
        [System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            get
            {
                return patientInfo;
            }
            set
            {
                patientInfo = value;
            }
        }

        private string isHaveTum = string.Empty;
        /// <summary>
        /// ȷ���Ƿ���ڸ�Ӥ����Ϣ
        /// </summary>
        public string IsHavedTum
        {
            get
            {
                if (this.cmbIsHaveTum.Tag == null)
                {
                    this.isHaveTum = string.Empty;
                }
                else
                {
                    this.isHaveTum = this.cmbIsHaveTum.Tag.ToString();
                }
                return this.isHaveTum;
            }
            set
            {
                this.isHaveTum = value;
                this.cmbIsHaveTum.Tag = this.isHaveTum;
            }
        }
        #region ���������������
        
        #region �س��¼�
        private void gy1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txttime1.Focus();
            }
        }

        private void time1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtday1.Focus();
            }
        }

        private void day1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dtbegindate1.Focus();
            }
        }

        private void begin_date1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dtenddate1.Focus();
            }
        }

        private void end_date1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtgy2.Focus();
            }
        }

        private void gy2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txttime2.Focus();
            }
        }

        private void time2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtday2.Focus();
            }
        }

        private void day2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dtbegindate2.Focus();
            }
        }

        private void begin_date2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dtenddate2.Focus();
            }
        }

        private void end_date2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtgy3.Focus();
            }
        }

        private void gy3_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txttime3.Focus();
            }
        }

        private void time3_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtday3.Focus();
            }
        }

        private void day3_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dtbegindate3.Focus();
            }
        }

        private void begin_date3_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dtenddate3.Focus();
            }
        }

        private void end_date3_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtCmodeid.Focus();
            }
        }
        #endregion
        /// <summary>
        /// �����������б�
        /// </summary>
        private void initList2()
        {
            try
            {
                FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
                //������������
                ArrayList TumourTypeList = con.GetList("CASETUMOURTYPE");
                this.txtTumourType.AddItems(TumourTypeList);
                //����
                ArrayList TumourStageList = con.GetList("CASETUMOURSTAGE");
                this.txtTumourStage.AddItems(TumourStageList);
                TumStageListHelper.ArrayObject = TumourStageList;
                //��λ
                ArrayList PositionList = con.GetList("CASEPOSITION");
                this.txtPosition.AddItems(PositionList);
                //���Ʒ�ʽ 
                ArrayList RmodeidList = con.GetList(FS.HISFC.Models.Base.EnumConstant.RADIATETYPE);
                this.txtRmodeid.AddItems(RmodeidList);

                //���Ƴ�ʽ 
                ArrayList RprocessidList = con.GetList(FS.HISFC.Models.Base.EnumConstant.RADIATEPERIOD);
                this.txtRprocessid.AddItems(RprocessidList);

                //����װ��
                ArrayList RdeviceidList = con.GetList(FS.HISFC.Models.Base.EnumConstant.RADIATEDEVICE);
                this.txtRdeviceid.AddItems(RdeviceidList);

                //���Ʒ�ʽ
                ArrayList CmodeidList = con.GetList(FS.HISFC.Models.Base.EnumConstant.CHEMOTHERAPY);
                this.txtCmodeid.AddItems(CmodeidList);

                //���Ʒ���
                ArrayList CmethodList = con.GetList(FS.HISFC.Models.Base.EnumConstant.CHEMOTHERAPYWAY);
                this.txtCmethod.AddItems(CmethodList);
                //�ӳ������л�ȡ�Ƿ���Ҫ����������ѡ��
                ArrayList listHavedTum = con.GetList("CASEHAVEDTUM");
                if (listHavedTum != null && listHavedTum.Count > 0)
                {
                    this.label31.Visible = true;
                    this.cmbIsHaveTum.Visible = true;
                }
                else
                {
                    this.label31.Visible = false;
                    this.cmbIsHaveTum.Visible = false;
                }
                ArrayList Havedlist = con.GetList("CASENOTORHAVED");
                if (Havedlist != null)
                {
                    this.cmbIsHaveTum.AddItems(Havedlist);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// ���������б�ĸ�ʽ
        /// </summary>
        /// <param name="listBox"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private int InitList(FS.FrameWork.WinForms.Controls.PopUpListBox listBox, ArrayList list)
        {
            if (list == null)
            {
                return -1;
            }
            try
            {
                //�����б�
                listBox.AddItems(list);
                listBox.Visible = false;
                Controls.Add(listBox);
                //����
                listBox.Hide();
                //���ñ߿�
                listBox.BorderStyle = BorderStyle.FixedSingle;
                listBox.BringToFront();
                //�����¼�
                listBox.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(ListBox_SelectItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 0;
        }
        /// <summary>
        /// ѡ����Ŀ�б� ����
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int ListBox_SelectItem(Keys key)
        {
            GetSelectItem();
            return 0;
        }
        /// <summary>
        /// ��ȡѡ�е���
        /// </summary>
        /// <returns></returns>
        private int GetSelectItem()
        {
            int rtn = listBoxActive.GetSelectedItem(out selectObj);
            if (selectObj == null)
            {
                return -1;
            }
            if (selectObj.ID != "")
            {
                this.contralActive.Tag = selectObj.ID;
                this.contralActive.Text = selectObj.Name;
            }
            else
            {
                this.contralActive.Tag = null;
            }
            this.listBoxActive.Visible = false;
            return rtn;
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (listBoxActive != null) //�������б�� 
                {
                    if (listBoxActive.Visible == true)
                    {
                        GetSelectItem();
                    }
                }
                else if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.Qty) //û�������б��  ע��Ҫ��else ����ᷢ��һ�������������
                {
                    if (fpEnter1_Sheet1.ActiveRowIndex < fpEnter1_Sheet1.Rows.Count - 1)
                    {
                        this.fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex + 1, 0);
                    }
                    else
                    {
                        this.AddRow();
                    }
                }
            }
            if (keyData == Keys.Escape)
            {
                listBoxActive.Visible = false;
            }
            return base.ProcessDialogKey(keyData);
        }
        /// <summary>
        /// ����ICDList�Ŀɼ���
        /// </summary>
        /// <param name="result"></param>
        private void ListBoxActiveVisible(bool result)
        {
            if (result)
            {
                //				int i = contral.Top +contral.Height + ICDListBox.Height;
                //				if(i <= this.Height)
                //					ICDListBox.Location=new System.Drawing.Point(contral.Left, i - ICDListBox.Height);				
                //				else
                //					ICDListBox.Location=new System.Drawing.Point(contral.Left, contral.Top - ICDListBox.Height);
                listBoxActive.Location = new System.Drawing.Point(contralActive.Location.X, contralActive.Location.Y + contralActive.Height + 2);
                listBoxActive.SelectNone = true;
                listBoxActive.Width = 100;
            }
            else
            {

            }
            listBoxActive.BringToFront();
            try
            {
                if (contralActive.Text != "")
                {
                    listBoxActive.Filter(contralActive.Text);
                }
                else
                {
                    listBoxActive.Filter(contralActive.Text);
                    listBoxActive.SelectedIndex = -1;
                }
            }
            catch { }
            listBoxActive.Visible = result;
        }
        /// <summary>
        /// �������б��textBox �ڸĶ�ʱɸѡ����
        /// </summary>
        private void ListFilter()
        {
            try
            {
                listBoxActive.Filter(contralActive.Text);
            }
            catch { }
        }
        /// <summary>
        /// ��ʵ���е�������ʾ�������� 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private int ConvertInfoToPanel(FS.HISFC.Models.HealthRecord.Tumour info)
        {
            try
            {
                txtTumourType.Tag = info.Tumour_Type;//������������
                txtTumourT.Text = info.Tumour_T;//T
                txtTumourN.Text = info.Tumour_N;//N
                txtTumourM.Text = info.Tumour_M;//M
                if (info.Tumour_Stage==null || TumStageListHelper.GetName(info.Tumour_Stage) == null
                    || TumStageListHelper.GetName(info.Tumour_Stage) == "")
                {
                    txtTumourStage.Text = info.Tumour_Stage;//����
                }
                else
                {
                    txtTumourStage.Tag = info.Tumour_Stage;//����
                }
                txtRmodeid.Tag = info.Rmodeid;//���Ʒ�ʽ
                txtRprocessid.Tag = info.Rprocessid;//���Ƴ�ʽ
                txtRdeviceid.Tag = info.Rdeviceid;//����װ��
                txtgy1.Text = info.Gy1.ToString();//ԭ�������
                txttime1.Text = info.Time1.ToString();//ԭ������
                txtday1.Text = info.Day1.ToString();
                if (info.BeginDate1 != System.DateTime.MinValue)
                {
                    dtbegindate1.Value = info.BeginDate1;
                }
                else
                {
                    dtbegindate1.Value = System.DateTime.Now;
                }
                if (info.EndDate1 != System.DateTime.MinValue)
                {
                    dtenddate1.Value = info.EndDate1;
                }
                else
                {
                    dtenddate1.Value = System.DateTime.Now;
                }
                txtgy2.Text = info.Gy2.ToString(); //�����ܰͽ�
                txttime2.Text = info.Time2.ToString();
                txtday2.Text = info.Day2.ToString();
                if (info.BeginDate2 != System.DateTime.MinValue)
                {
                    dtbegindate2.Value = info.BeginDate2;
                }
                else
                {
                    dtbegindate2.Value = System.DateTime.Now;
                }
                if (info.EndDate2 != System.DateTime.MinValue)
                {
                    dtenddate2.Value = info.EndDate2;
                }
                else
                {
                    dtenddate2.Value = System.DateTime.Now;
                }
                txtPosition.Text = info.Position;//ת����λ��
                txtgy3.Text = info.Gy3.ToString();//ת�������
                txttime3.Text = info.Time3.ToString();
                txtday3.Text = info.Day3.ToString();
                if (info.BeginDate3 != System.DateTime.MinValue)
                {
                    dtbegindate3.Value = info.BeginDate3;
                }
                else
                {
                    dtbegindate3.Value = System.DateTime.Now;
                }
                if (info.EndDate3 != System.DateTime.MinValue)
                {
                    dtenddate3.Value = info.EndDate3;
                }
                else
                {
                    dtenddate3.Value = System.DateTime.Now;
                }
                txtCmodeid.Tag = info.Cmodeid;//���Ʒ�ʽ
                txtCmethod.Tag = info.Cmethod;//���Ʒ���
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// ����У��  У��ʧ�ܷ��� -1 �ɹ����� 1 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int ValueTumourSate(FS.HISFC.Models.HealthRecord.Tumour info)
        {
            if (info == null)
            {
                MessageBox.Show("������ϢΪ��");
                return -1;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(info.Rmodeid,20))
            {
                MessageBox.Show("���Ʒ�ʽ �������");
                return -1;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(info.Rprocessid, 20))
            {
                MessageBox.Show("���Ƴ�ʽ �������");
                return -1;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(info.Rdeviceid,20))
            {
                MessageBox.Show("����װ�� �������");
                return -1;
            }
            //���Ʒ�ʽ    
            if (!FS.FrameWork.Public.String.ValidMaxLengh(info.Cmodeid,20))
            {
                MessageBox.Show("����װ�� �������");
                return -1;
            }
            //���Ʒ��� 
            if (!FS.FrameWork.Public.String.ValidMaxLengh(info.Cmethod,20))
            {
                MessageBox.Show("����װ�� �������");
                return -1;
            }
            if (info.Gy1 > (decimal)9999.99)
            {
                MessageBox.Show("ԭ������� ����");
                return -1;
            }
            if (info.Gy1 < 0)
            {
                MessageBox.Show("ԭ������� ����С����");
                return -1;
            }
            if (info.Time1 < 0)
            {
                MessageBox.Show("ԭ������� ����С����");
                return -1;
            }
            if (info.Time1 > (decimal)9999.99)
            {
                MessageBox.Show("ԭ������� ����");
                return -1;
            }
            if (info.Day1 < 0)
            {
                MessageBox.Show("ԭ�������� ����С����");
                return -1;
            }
            if (info.Day1 > (decimal)9999.99)
            {
                MessageBox.Show("ԭ�������� ����");
                return -1;
            }
            if (info.Gy2 < 0)
            {
                MessageBox.Show("�����ܰͽ���� ����С����");
                return -1;
            }
            if (info.Gy2 > (decimal)9999.99)
            {
                MessageBox.Show("�����ܰͽ���� ����");
                return -1;
            }
            if (info.Time2 < 0)
            {
                MessageBox.Show("�����ܰͽ���� ����С����");
                return -1;
            }
            if (info.Time2 > (decimal)9999.99)
            {
                MessageBox.Show("�����ܰͽ���� ����");
                return -1;
            }
            if (info.Day2 < 0)
            {
                MessageBox.Show("�����ܰͽ����� ����С����");
                return -1;
            }

            if (info.Day2 > (decimal)9999.99)
            {
                MessageBox.Show("�����ܰͽ����� ����");
                return -1;
            }

            if (info.Gy3 < 0)
            {
                MessageBox.Show("ת����������� ����С����");
                return -1;
            }

            if (info.Gy3 > (decimal)9999.99)
            {
                MessageBox.Show("ת����������� ����");
                return -1;
            }

            if (info.Time3 < 0)
            {
                MessageBox.Show("ת����������� ����С����");
                return -1;
            }
            if (info.Time3 > (decimal)9999.99)
            {
                MessageBox.Show("ת����������� ����");
                return -1;
            }

            if (info.Day3 < 0)
            {
                MessageBox.Show("ת����������� ����С����");
                return -1;
            }

            if (info.Day3 > (decimal)9999.99)
            {
                MessageBox.Show("ת����������� ����");
                return -1;
            }
            if ((info.Tumour_Type!=null ||info.Tumour_Stage!=null || info.Rmodeid != null || info.Rprocessid != null || info.Rdeviceid != null || info.Cmodeid != null || info.Cmethod != null)
               && (info.Tumour_Type != "" || info.Tumour_Stage != "" || info.Rmodeid != "" || info.Rprocessid != "" || info.Rdeviceid != "" || info.Cmodeid != "" || info.Cmethod != "")
                )
            {
                return 2;
            }
            return 1;
        }
        /// <summary>
        /// �������ϵ����� �ռ���ʵ���� 
        /// </summary>
        /// <returns></returns>
        public FS.HISFC.Models.HealthRecord.Tumour GetTumourInfo()
        {
            FS.HISFC.Models.HealthRecord.Tumour info = new FS.HISFC.Models.HealthRecord.Tumour();

            try
            {
                info.InpatientNo = this.patientInfo.ID; //סԺ��ˮ��
                //������������
                if (txtTumourType.Tag != null)
                {
                    info.Tumour_Type = this.txtTumourType.Tag.ToString();
                }
                else
                {
                    info.Tumour_Type = string.Empty;
                }
                //T
                info.Tumour_T = this.txtTumourT.Text.Trim();
                //N
                info.Tumour_N = this.txtTumourN.Text.Trim();
                //M
                info.Tumour_M = this.txtTumourM.Text.Trim();
                //����
                if (this.txtTumourStage.Tag != null && this.txtTumourStage.Tag.ToString() != "" && this.txtTumourStage.Text.Trim()!="")
                {
                    info.Tumour_Stage = this.txtTumourStage.Tag.ToString();
                }
                else
                {
                    info.Tumour_Stage = this.txtTumourStage.Text;
                }

                if (txtRmodeid.Tag != null && txtRmodeid.Text.Trim()!="")
                {
                    info.Rmodeid = txtRmodeid.Tag.ToString();//���Ʒ�ʽ
                }
                else
                {
                    info.Rmodeid = string.Empty;
                }
                if (txtRprocessid.Tag != null && txtRprocessid.Text.Trim()!="")
                {
                    info.Rprocessid = txtRprocessid.Tag.ToString();//���Ƴ�ʽ
                }
                else
                {
                    info.Rprocessid = string.Empty;
                }
                if (txtRdeviceid.Tag != null && txtRdeviceid.Text.Trim()!="")
                {
                    info.Rdeviceid = txtRdeviceid.Tag.ToString();//����װ��
                }
                else
                {
                    info.Rdeviceid = string.Empty;
                }
                info.Gy1 = FS.FrameWork.Function.NConvert.ToDecimal(txtgy1.Text);//ԭ�������
                info.Time1 = FS.FrameWork.Function.NConvert.ToDecimal(txttime1.Text);//ԭ������
                info.Day1 = FS.FrameWork.Function.NConvert.ToDecimal(txtday1.Text);
                info.BeginDate1 = dtbegindate1.Value;
                info.EndDate1 = dtenddate1.Value;
                info.Gy2 = FS.FrameWork.Function.NConvert.ToDecimal(txtgy2.Text); //�����ܰͽ�
                info.Time2 = FS.FrameWork.Function.NConvert.ToDecimal(txttime2.Text);
                info.Day2 = FS.FrameWork.Function.NConvert.ToDecimal(txtday2.Text);
                info.BeginDate2 = dtbegindate2.Value;
                info.EndDate2 = dtenddate2.Value;
                info.Position = this.txtPosition.Text.Trim();//ת����λ��
                info.Gy3 = FS.FrameWork.Function.NConvert.ToDecimal(txtgy3.Text);//ת�������
                info.Time3 = FS.FrameWork.Function.NConvert.ToDecimal(txttime3.Text);
                info.Day3 = FS.FrameWork.Function.NConvert.ToDecimal(txtday3.Text);
                info.BeginDate3 = dtbegindate3.Value;
                info.EndDate3 = dtenddate3.Value;
                if (txtCmodeid.Tag != null && txtCmodeid.Text.Trim()!="")
                {
                    info.Cmodeid = txtCmodeid.Tag.ToString();//���Ʒ�ʽ
                }
                else
                {
                    info.Cmodeid = string.Empty;
                }
                if (txtCmethod.Tag != null && txtCmethod.Text.Trim()!="")
                {
                    info.Cmethod = txtCmethod.Tag.ToString();//���Ʒ���
                }
                else
                {
                    info.Cmethod = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return info;
        }
        /// <summary>
        /// ���� ֻ���� 
        /// </summary>
        /// <param name="type"></param>
        private void SetTumourReadOnly(bool type)
        {
            txtTumourType.ReadOnly = type;//������������
            txtTumourT.ReadOnly = type;//ԭ������
            txtTumourN.ReadOnly = type;//�ܰ�ת��
            txtTumourM.ReadOnly = type;//Զ��ת��
            txtTumourStage.ReadOnly = type;//����
            txtRmodeid.ReadOnly = type;//���Ʒ�ʽ
            txtRprocessid.ReadOnly = type;//���Ƴ�ʽ
            txtRdeviceid.ReadOnly = type;//����װ��
            txtgy1.ReadOnly = type;//ԭ�������
            txttime1.ReadOnly = type;//ԭ������
            txtday1.ReadOnly = type;
            dtbegindate1.Enabled = !type;
            dtenddate1.Enabled = !type;
            txtgy2.ReadOnly = type; //�����ܰͽ�
            txttime2.ReadOnly = type;
            txtday2.ReadOnly = type;
            dtbegindate2.Enabled = !type;
            dtenddate2.Enabled = !type;
            txtPosition.ReadOnly = type;//ת����λ��
            txtgy3.ReadOnly = type;//ת�������
            txttime3.ReadOnly = type;
            txtday3.ReadOnly = type;
            dtbegindate3.Enabled = !type;
            dtenddate3.Enabled = !type;
            txtCmodeid.ReadOnly = type;//���Ʒ�ʽ
            txtCmethod.ReadOnly = type;//���Ʒ���
            this.btAdd.Enabled = !type;
            this.btDelete.Enabled = !type;
        }
        /// <summary>
        /// ��տؼ�ֵ
        /// </summary>
        private void ClearTumourInfo()
        {
            txtTumourType.Tag = null;
            txtTumourT.Text = "";
            txtTumourN.Text = "";
            this.txtTumourM.Text = "";
            this.txtTumourStage.Tag = null;
            this.txtPosition.Tag = null;

            txtRmodeid.Text = "";//���Ʒ�ʽ
            txtRmodeid.Tag = null;
            txtRprocessid.Text = "";//���Ƴ�ʽ
            txtRprocessid.Tag = null;
            txtRdeviceid.Text = "";//����װ��
            txtRdeviceid.Tag = null;
            txtgy1.Text = "";//ԭ�������
            txttime1.Text = "";//ԭ������
            txtday1.Text = "";
            dtbegindate1.Value = System.DateTime.Now;
            dtenddate1.Value = System.DateTime.Now;
            txtgy2.Text = ""; //�����ܰͽ�
            txttime2.Text = "";
            txtday2.Text = "";
            dtbegindate2.Value = System.DateTime.Now;
            dtenddate2.Value = System.DateTime.Now;
            txtgy3.Text = "";//ת�������
            txttime3.Text = "";
            txtday3.Text = "";
            dtbegindate3.Value = System.DateTime.Now;
            dtenddate3.Value = System.DateTime.Now;
            txtCmodeid.Tag = null;//���Ʒ�ʽ
            txtCmodeid.Text = "";//���Ʒ�ʽ
            txtCmethod.Tag = null;//���Ʒ���
            txtCmethod.Text = "";//���Ʒ���
        }
        #endregion

        #region  ������ϸ��������
        /// <summary>
        /// ���û��Ԫ��
        /// </summary>
        public void SetActiveCells()
        {
            try
            {
                this.fpEnter1_Sheet1.SetActiveCell(0, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// �޶���Ŀ�Ⱥܿɼ��� 
        /// </summary>
        private void LockFpEnter()
        {
            this.fpEnter1_Sheet1.Columns[0].Width = 63; //��ʼ����
            this.fpEnter1_Sheet1.Columns[1].Width = 63;//��������
            this.fpEnter1_Sheet1.Columns[2].Width = 129;//ҩ������
            this.fpEnter1_Sheet1.Columns[6].Width = 60;//����
            this.fpEnter1_Sheet1.Columns[3].Width = 40; //��λ
            this.fpEnter1_Sheet1.Columns[4].Width = 40; //�Ƴ�
            this.fpEnter1_Sheet1.Columns[5].Width = 80; //���
            this.fpEnter1_Sheet1.Columns[7].Width = 100; //ҩƷ����
            this.fpEnter1_Sheet1.Columns[7].Locked = true;//ҩƷ����
            this.fpEnter1_Sheet1.Columns[8].Visible = false; //���
        }
        /// <summary>
        /// ���ԭ�е�����
        /// </summary>
        /// <returns></returns>
        public int ClearInfo()
        {
            if (this.dtTumour != null)
            {
                this.dtTumour.Clear();
                ClearTumourInfo();
                LockFpEnter();
            }
            else
            {
                MessageBox.Show("������Ϊnull");
            }
            return 1;
        }
        public int SetReadOnly(bool type)
        {
            if (type)
            {
                this.fpEnter1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
                SetTumourReadOnly(type);
            }
            else
            {
                this.fpEnter1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.Normal;
                SetTumourReadOnly(type);
            }
            return 0;
        }
        /// <summary>
        /// У�����ݵĺϷ��ԡ�
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int ValueState(ArrayList list)
        {
            if (list == null)
            {
                return -2;
            }
            foreach (FS.HISFC.Models.HealthRecord.TumourDetail obj in list)
            {
                if (obj.InpatientNO == "" || obj.InpatientNO == null)
                {
                    MessageBox.Show("������Ϣ סԺ��ˮ�Ų���Ϊ��");
                    return -1;
                }

                if (obj.InpatientNO.Length > 14)
                {
                    MessageBox.Show("������Ϣ סԺ��ˮ�Ź���");
                    return -1;
                }
                if (obj.DrugInfo.Name == null || obj.DrugInfo.Name == "")
                {
                    MessageBox.Show("������Ϣ ҩ�����Ʋ���Ϊ��");
                    return -1;
                }
                else if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DrugInfo.Name, 50))
                {
                    MessageBox.Show("������Ϣ ҩ�����ƹ���");
                    return -1;
                }
                if (obj.Qty == 0)
                {
                    MessageBox.Show("������Ϣ" + obj.DrugInfo.Name + " ��������Ϊ��");
                    return -1;
                }
                else if (obj.Qty > 10000)
                {
                    MessageBox.Show("������Ϣ" + obj.DrugInfo.Name + " ��������");
                    return -1;
                }
                else if (obj.Qty < 0)
                {
                    MessageBox.Show("������Ϣ" + obj.DrugInfo.Name + " ��������С����");
                    return -1;
                }

                if (obj.Unit == null || obj.Unit == "")
                {
                    MessageBox.Show("����д ������Ϣ" + obj.DrugInfo.Name + " �ĵ�λ");
                    return -1;
                }
                else if (obj.Unit.Length > 8)
                {
                    MessageBox.Show("������Ϣ" + obj.DrugInfo.Name + " ��λ���ȹ���");
                    return -1;
                }
            }
            return 0;
        }
        /// <summary>
        /// ����Ա����������޸�
        /// </summary>
        /// <returns></returns>
        public int fpEnterSaveChanges()
        {
            try
            {
                this.dtTumour.AcceptChanges();
                LockFpEnter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// ������������ݻ�д������
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int fpEnterSaveChanges(ArrayList list)
        {
            AddInfoToTable(list);
            dtTumour.AcceptChanges();
            LockFpEnter();
            return 0;
        }
        public int AddInfoToTable(ArrayList list)
        {
            if (this.dtTumour != null)
            {
                this.dtTumour.Clear();
                this.dtTumour.AcceptChanges();
            }
            if (list == null)
            {
                return -1;
            }

            //ѭ����������
            foreach (FS.HISFC.Models.HealthRecord.TumourDetail info in list)
            {
                DataRow row = dtTumour.NewRow();
                SetRow(info, row);
                dtTumour.Rows.Add(row);
            }
            //���ı�־
            dtTumour.AcceptChanges();
            LockFpEnter();
            return 0;
        }
        /// <summary>
        /// ���ص�ǰ��������
        /// </summary>
        /// <returns></returns>
        public int GetfpSpreadRowCount()
        {
            return fpEnter1_Sheet1.Rows.Count;
        }
        /// <summary>
        /// ���reset Ϊ�� ������������� ���������  Ϊ�� ֻ�Ǳ��浱ǰ����
        /// creator:zhangjunyi@FS.com
        /// </summary>
        /// <param name="reset"></param>
        /// <returns></returns>
        public bool Reset(bool reset)
        {
            if (reset)
            {
                //������� ������� 
                if (dtTumour != null)
                {
                    dtTumour.Clear();
                    dtTumour.AcceptChanges();
                }
            }
            else
            {
                //�������
                dtTumour.AcceptChanges();
            }
            return true;
        }
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void InitInfo()
        {
            try
            {
                //��ʼ����
                InitDateTable();
                //���������б�
                this.initList();
                //���������б�
                initList2();
                fpEnter1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
                LockFpEnter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void InitDateTable()
        {
            try
            {
                Type strType = typeof(System.String);
                Type intType = typeof(System.Int32);
                Type dtType = typeof(System.DateTime);
                Type boolType = typeof(System.Boolean);
                Type floatType = typeof(System.Single);

                dtTumour.Columns.AddRange(new DataColumn[]{
														   new DataColumn("��ʼ����", dtType),	//0
                                                           new DataColumn("��������", dtType),	//1
														   new DataColumn("ҩ������", strType),	 //2
														   new DataColumn("��λ", strType),//3
														   new DataColumn("�Ƴ�", strType),//4
														   new DataColumn("���", strType),//5
														   new DataColumn("����", strType),//6
														   new DataColumn("ҩƷ����", strType),//7
														   new DataColumn("���", intType)});//8
                //������Դ
                this.fpEnter1_Sheet1.DataSource = dtTumour;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public bool GetList(string strType, ArrayList list)
        {
            try
            {
                this.fpEnter1.StopCellEditing();
                foreach (DataRow dr in this.dtTumour.Rows)
                {
                    dr.EndEdit();
                }
                switch (strType)
                {
                    case "A":
                        //���ӵ�����
                        DataTable AddTable = this.dtTumour.GetChanges(DataRowState.Added);
                        GetChangeInfo(AddTable, list);
                        break;
                    case "M":
                        DataTable ModTable = this.dtTumour.GetChanges(DataRowState.Modified);
                        GetChangeInfo(ModTable, list);
                        break;
                    case "D":
                        DataTable DelTable = this.dtTumour.GetChanges(DataRowState.Deleted);
                        if (DelTable != null)
                        {
                            DelTable.RejectChanges();
                        }
                        GetChangeInfo(DelTable, list);
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// ��ȡ�޸Ĺ�����Ϣ
        /// </summary>
        /// <returns></returns>
        private bool GetChangeInfo(DataTable tempTable, ArrayList list)
        {
            if (tempTable == null)
            {
                return true;
            }
            try
            {
                FS.HISFC.Models.HealthRecord.TumourDetail info = null;
                foreach (DataRow row in tempTable.Rows)
                {
                    info = new FS.HISFC.Models.HealthRecord.TumourDetail();
                    info.InpatientNO = this.patientInfo.ID;
                    if (row["��ʼ����"] != DBNull.Value)
                    {
                        info.CureDate = FS.FrameWork.Function.NConvert.ToDateTime(row["��ʼ����"].ToString());
                    }
                    if (row["��������"] != DBNull.Value)
                    {
                        info.OperInfo.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(row["��������"].ToString());
                    }
                    if (row["ҩ������"] != DBNull.Value)
                    {
                        info.DrugInfo.Name = row["ҩ������"].ToString().Replace("'", "��"); ;//1
                    }
                    if (row["����"] != DBNull.Value)
                    {
                        info.Qty = FS.FrameWork.Function.NConvert.ToDecimal(row["����"]);//1
                    }
                    if (row["��λ"] != DBNull.Value)
                    {
                        info.Unit = this.UnitListHelper.GetID(row["��λ"].ToString());//2
                    }
                    if (row["�Ƴ�"] != DBNull.Value)
                    {
                        info.Period = this.PeriodListHelper.GetID(row["�Ƴ�"].ToString());//2
                    }
                    if (row["���"] != DBNull.Value)
                    {
                        info.Result = this.ResultListHelper.GetID(row["���"].ToString());//2
                    }
                    if (row["ҩƷ����"] != DBNull.Value)
                    {
                        info.DrugInfo.ID = row["ҩƷ����"].ToString();//2
                    }
                    if (row["���"] != DBNull.Value)
                    {
                        info.HappenNO = FS.FrameWork.Function.NConvert.ToInt32(row["���"]);//1
                    }
                    list.Add(info);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// ɾ����ǰ�� 
        /// </summary>
        /// <returns></returns>
        public int DeleteActiveRow()
        {
            this.fpEnter1.SetAllListBoxUnvisible();
            this.fpEnter1.EditModePermanent = false;
            this.fpEnter1.EditModeReplace = false;
            if (fpEnter1_Sheet1.Rows.Count > 0)
            {
                this.fpEnter1_Sheet1.Rows.Remove(fpEnter1_Sheet1.ActiveRowIndex, 1);
            }
            if (fpEnter1_Sheet1.Rows.Count == 0)
            {
                this.fpEnter1.SetAllListBoxUnvisible();
            } 
            this.fpEnter1.EditModePermanent = true;
            this.fpEnter1.EditModeReplace = true;
            return 1;
        }
        /// <summary>
        /// ɾ���հ׵���
        /// </summary>
        /// <returns></returns>
        public int deleteRow()
        {
            this.fpEnter1.SetAllListBoxUnvisible();
            this.fpEnter1.EditModePermanent = false;
            this.fpEnter1.EditModeReplace = false;
            if (fpEnter1_Sheet1.Rows.Count == 1)
            {
                //��һ�б���Ϊ�� 
                if (fpEnter1_Sheet1.Cells[0, 1].Text == "")
                {
                    fpEnter1_Sheet1.Rows.Remove(0, 1);
                }
            } 
            this.fpEnter1.EditModePermanent = true;
            this.fpEnter1.EditModeReplace = true;
            return 1;
        }
        /// <summary>
        /// ��ѯ����ʾ����
        /// </summary>
        /// <returns>������ ��1 ���� 0 �������в���1  </returns>
        public int LoadInfo(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes Type)
        {
            if (patient == null)
            {
                return -1;
            }
            patientInfo = patient;
            if (patientInfo.CaseState == "0")
            {
                //�������в���
                return 1;
            }
            FS.HISFC.BizLogic.HealthRecord.Tumour tumour = new FS.HISFC.BizLogic.HealthRecord.Tumour();
            //��ѯ��������������
            ArrayList list = tumour.QueryTumourDetail(patient.ID);
            AddInfoToTable(list);
            FS.HISFC.Models.HealthRecord.Tumour obj = tumour.GetTumour(patient.ID);
            if (obj == null)
            {
                MessageBox.Show("��ȡ������Ϣ����");
                return -1;
            }
            this.ConvertInfoToPanel(obj);
            return 0;

        }
        /// <summary>
        /// ��ֵ
        /// </summary>
        /// <param name="row"></param>
        /// <param name="info"></param>
        private void SetRow(FS.HISFC.Models.HealthRecord.TumourDetail info, DataRow row)
        {
            row["��ʼ����"] = info.CureDate;//0
            row["��������"] = info.OperInfo.OperTime;//1
            row["ҩ������"] = info.DrugInfo.Name.Replace("��", "'"); ;//2
            row["����"] = info.Qty;//3
            row["��λ"] = UnitListHelper.GetName(info.Unit);            //4
            row["�Ƴ�"] = PeriodListHelper.GetName(info.Period);//5
            row["���"] = ResultListHelper.GetName(info.Result);//6
            row["ҩƷ����"] = info.DrugInfo.ID;//7
            row["���"] = info.HappenNO;//8
        }
        private enum Col
        {
            colTime = 0,//��ʼ����
            colEndTime=1,//��������
            DrugName = 2,//ҩƷ����
            Unit = 3,//��λ
            Preiod = 4,//�Ƴ�
            Result = 5,//���
            Qty = 6,//����
            DrugCode = 7 //ҩƷ����

        }
        /// <summary>
        /// �����������б�
        /// </summary>
        private void initList()
        {
            try
            {
                FS.HISFC.BizLogic.HealthRecord.Tumour da = new FS.HISFC.BizLogic.HealthRecord.Tumour();
                FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
                FS.HISFC.BizProcess.Integrate.Pharmacy item = new FS.HISFC.BizProcess.Integrate.Pharmacy();
                fpEnter1.SetWidthAndHeight(200, 200);
                fpEnter1.SelectNone = true;
                //FS.HISFC.BizProcess.Integrate.Management.Pharmacy.Item itemMgr = new FS.HISFC.BizProcess.Pharmacy.Item();
                //ҩƷ��Ϣ
                if (druglist == null || druglist.Count == 0)
                {
                    druglist = item.QueryItemList(true);
                }
                ArrayList temp = new ArrayList(druglist.ToArray());
                this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, (int)Col.DrugName, temp);
                this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, (int)Col.DrugCode, temp);
                //ҩƷ��Ϣ����ʾID��
                this.fpEnter1.SetIDVisiable(this.fpEnter1_Sheet1, (int)Col.DrugName, false);
                this.fpEnter1.SetIDVisiable(this.fpEnter1_Sheet1, (int)Col.DrugCode, false);
                //��λ�б�
                ArrayList UnitList = con.GetList(FS.HISFC.Models.Base.EnumConstant.DOSEUNIT);
                this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, (int)Col.Unit, UnitList);
                UnitListHelper.ArrayObject = UnitList;

                //�Ƴ��б� 
                ArrayList PeriodList = con.GetList(FS.HISFC.Models.Base.EnumConstant.PERIODOFTREATMENT);// da.GetPeriodList();
                this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, (int)Col.Preiod, PeriodList);
                PeriodListHelper.ArrayObject = PeriodList;

                //j����б�
                ArrayList ResultList = con.GetList(FS.HISFC.Models.Base.EnumConstant.RADIATERESULT);// da.GetResultList();
                this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, (int)Col.Result, ResultList);
                ResultListHelper.ArrayObject = ResultList;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ucTumourCard_Load(object sender, System.EventArgs e)
        {
            //������Ӧ�����¼�
            fpEnter1.KeyEnter+=new FS.FrameWork.WinForms.Controls.NeuFpEnter.keyDown(fpEnter1_KeyEnter);
            fpEnter1.SetItem +=new FS.FrameWork.WinForms.Controls.NeuFpEnter.setItem(fpEnter1_SetItem);
            fpEnter1.ShowListWhenOfFocus = true;
            fpEnter1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
        }
        /// <summary>
        /// ������Ӧ����
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int fpEnter1_KeyEnter(Keys key)
        {
            if (key == Keys.Enter)
            {
                //				MessageBox.Show("Enter,�����Լ���Ӵ����¼�������������һcell");
                //�س�
                if (this.fpEnter1.ContainsFocus)
                {
                    int i = this.fpEnter1_Sheet1.ActiveColumnIndex;
                    if (i == (int)Col.DrugName || i == (int)Col.Unit || i == (int)Col.Preiod || i == (int)Col.Result || i == (int)Col.DrugCode)
                    {
                        ProcessDept();
                    }
                    else if (i == (int)Col.colTime)
                    {
                        this.fpEnter1_Sheet1.SetActiveCell(this.fpEnter1_Sheet1.ActiveRowIndex, 1);
                    }
                    else if (i == (int)Col.Qty)
                    {
                        if (fpEnter1_Sheet1.ActiveRowIndex < fpEnter1_Sheet1.Rows.Count - 1)
                        {
                            this.fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex + 1, 0);
                        }
                        else
                        {
                            this.AddRow();
                        }
                    }
                }
            }
            else if (key == Keys.Up)
            {
                //				MessageBox.Show("Up,�����Լ���Ӵ����¼��������������б�ʱ���������У���ʾ�����ؼ�ʱ���������ؼ������ƶ�");
            }
            else if (key == Keys.Down)
            {
                //				MessageBox.Show("Down�������Լ���Ӵ����¼��������������б�ʱ���������У���ʾ�����ؼ�ʱ���������ؼ������ƶ�");
            }
            else if (key == Keys.Escape)
            {
                //				MessageBox.Show("Escape,ȡ���б�ɼ�");
            }
            return 0;
        }
        /// <summary>
        /// ���һ����Ŀ
        /// </summary>
        /// <returns></returns>
        public int AddRow()
        {
            try
            {
                if (fpEnter1_Sheet1.Rows.Count < 1)
                {
                    //����һ�п�ֵ
                    DataRow row = dtTumour.NewRow();
                    row["���"] = 1;
                    row["��ʼ����"] = System.DateTime.Now;
                    row["��������"] = System.DateTime.Now;
                    row["����"] = 0;//2
                    dtTumour.Rows.Add(row);
                }
                else
                {
                    //����һ��
                    int j = fpEnter1_Sheet1.Rows.Count;
                    this.fpEnter1_Sheet1.Rows.Add(j, 1);
                    for (int i = 0; i < fpEnter1_Sheet1.Columns.Count; i++)
                    {
                        fpEnter1_Sheet1.Cells[j, i].Value = fpEnter1_Sheet1.Cells[j - 1, i].Value;
                    }
                }
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.Rows.Count, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }
        /// <summary>
        /// ���һ����Ŀ
        /// </summary>
        /// <returns></returns>
        public int InsertRow()
        {
            try
            {
                if (this.fpEnter1_Sheet1.RowCount == 0)
                {
                    this.AddRow();
                }
                else
                {
                    //����һ��
                    int actRow = fpEnter1_Sheet1.ActiveRowIndex + 1;
                    this.fpEnter1_Sheet1.Rows.Add(actRow, 1);
                    //for (int i = 0; i < fpEnter1_Sheet1.Columns.Count; i++)
                    //{
                    //    if (i == 0)
                    //    {
                    //        fpEnter1_Sheet1.Cells[actRow, i].Value = "�������";
                    //    }
                    //    else
                    //    {
                    //        fpEnter1_Sheet1.Cells[actRow, i].Value = fpEnter1_Sheet1.Cells[actRow - 1, i].Value;
                    //    }
                    //}
                    fpEnter1.Focus();
                    fpEnter1_Sheet1.SetActiveCell(actRow, 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }
        /// <summary>
        /// ѡ��ѡ�е���
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private int fpEnter1_SetItem(FS.FrameWork.Models.NeuObject obj)
        {
            this.ProcessDept();
            return 0;
        }
        /// <summary>
        /// ����س����� ������ȡ������
        /// </summary>
        /// <returns></returns>
        private int ProcessDept()
        {
            int CurrentRow = fpEnter1_Sheet1.ActiveRowIndex;
            if (CurrentRow < 0) return 0;

            if (fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.DrugName)
            {
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)Col.DrugName);
                //��ȡѡ�е���Ϣ
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                //ҩƷ����
                fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                //ҩƷ����
                fpEnter1_Sheet1.Cells[CurrentRow, (int)Col.DrugCode].Text = item.ID;
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.Unit);
                return 0;
            }
            else if (fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.DrugName)
            {
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)Col.DrugName);
                //��ȡѡ�е���Ϣ
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                //ҩƷ����
                fpEnter1_Sheet1.ActiveCell.Text = item.ID;
                //ҩƷ����
                fpEnter1_Sheet1.Cells[CurrentRow, (int)Col.DrugName].Text = item.Name;
                fpEnter1.Focus();
                //
                if (fpEnter1_Sheet1.ActiveRowIndex < fpEnter1_Sheet1.Rows.Count - 1)
                {
                    this.fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex + 1, 0);
                }
                //				else
                //				{
                //					this.AddRow();
                //				}
                return 0;
            }
            else if (fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.Unit)
            {
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)Col.Unit);
                //��ȡѡ�е���Ϣ
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                //ҩƷ������λ
                fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.Preiod);
                return 0;
            }
            else if (fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.Preiod)
            {
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)Col.Preiod);
                //��ȡѡ�е���Ϣ
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                // �Ƴ�
                fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.Result);
                return 0;
            }
            else if (fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.Result)
            {
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)Col.Result);
                //��ȡѡ�е���Ϣ
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                if (item == null) return -1;
                //j���
                fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.Qty);
                //
                //				if(fpEnter1_Sheet1.ActiveRowIndex < fpEnter1_Sheet1.Rows.Count -1)
                //				{
                //					this.fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex +1 ,0);
                //				}
                return 0;
            }

            return 0;
        }
        /// <summary>
        /// ��������ÿ�� ���Ƿ��ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem1_Click(object sender, System.EventArgs e)
        {
            //			FS.Common.Controls.ucSetCol uc = new FS.Common.Controls.ucSetCol();
            //			uc.FilePath = this.filePath;
            //			uc.GoDisplay += new FS.Common.Controls.ucSetCol.DisplayNow(uc_GoDisplay);
            //			FS.neuFC.Interface.Classes.Function.PopShowControl(uc);
        }
        /// <summary>
        /// ����fpSpread1_Sheet1�Ŀ�ȵ� ����󴥷����¼�
        /// </summary>
        private void uc_GoDisplay()
        {
        }

        private void menuItem2_Click(object sender, System.EventArgs e)
        {
            if (this.fpEnter1_Sheet1.Rows.Count > 0)
            {
                //ɾ����ǰ��
                this.fpEnter1_Sheet1.Rows.Remove(fpEnter1_Sheet1.ActiveRowIndex, 1);
            }

        }
        #endregion  

        private void btAdd_Click(object sender, EventArgs e)
        {
            //this.AddRow();
            this.InsertRow();
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            this.DeleteActiveRow();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.AddRow();
        }

    }
}
