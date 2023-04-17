using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace UFC.HealthRecord
{
    public partial class TumourCard : UserControl
    {
        public TumourCard()
        {
            InitializeComponent();
        }

        #region  ȫ�ֱ���
        //��ǰ������б�
        private Neusoft.NFC.Interface.Controls.PopUpListBox listBoxActive = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        //��ǰ��ؼ�
        private System.Windows.Forms.Control contralActive = new Control();
        private DataTable dtTumour = new DataTable("����");
        private Neusoft.NFC.Public.ObjectHelper diagnoseTypeHelper = new Neusoft.NFC.Public.ObjectHelper();
        //�����ļ���·�� 
        private string filePath = Application.StartupPath + "\\profile\\ucTumourCard1.xml";
        //���˻�����Ϣ��
        private Neusoft.HISFC.Object.RADT.PatientInfo patientInfo = new Neusoft.HISFC.Object.RADT.PatientInfo();
        //��λ�б�
        Neusoft.NFC.Public.ObjectHelper UnitListHelper = new Neusoft.NFC.Public.ObjectHelper();
        //�Ƴ��б�
        Neusoft.NFC.Public.ObjectHelper PeriodListHelper = new Neusoft.NFC.Public.ObjectHelper();
        //����б�
        Neusoft.NFC.Public.ObjectHelper ResultListHelper = new Neusoft.NFC.Public.ObjectHelper();
        //���Ʒ�ʽ 
        private Neusoft.NFC.Interface.Controls.PopUpListBox RmodeidListBox = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        private Neusoft.NFC.Public.ObjectHelper RmodeidTypeHelper = new Neusoft.NFC.Public.ObjectHelper();

        //���Ƴ�ʽ 
        private Neusoft.NFC.Interface.Controls.PopUpListBox RprocessidListBox = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        private Neusoft.NFC.Public.ObjectHelper RprocessidTypeHelper = new Neusoft.NFC.Public.ObjectHelper();

        //����װ��
        private Neusoft.NFC.Interface.Controls.PopUpListBox RdeviceidListBox = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        private Neusoft.NFC.Public.ObjectHelper RdeviceidTypeHelper = new Neusoft.NFC.Public.ObjectHelper();

        //���Ʒ�ʽ
        private Neusoft.NFC.Interface.Controls.PopUpListBox CmodeidListBox = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        private Neusoft.NFC.Public.ObjectHelper CmodeidTypeHelper = new Neusoft.NFC.Public.ObjectHelper();

        //���Ʒ���
        private Neusoft.NFC.Interface.Controls.PopUpListBox CmethodListBox = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        private Neusoft.NFC.Public.ObjectHelper CmethodTypeHelper = new Neusoft.NFC.Public.ObjectHelper();
        //��ǰѡ�е���Ϣ
        private Neusoft.NFC.Object.NeuObject selectObj;
        #endregion

        #region ���������������
        #region  ��������¼�
        #region ���Ʒ�ʽ
        private void Rmodeid_Enter(object sender, System.EventArgs e)
        {
            if (Rmodeid.ReadOnly)
            {
                return;
            }
            contralActive = this.Rmodeid;
            listBoxActive = RmodeidListBox;
            ListBoxActiveVisible(true);
        }

        private void Rmodeid_TextChanged(object sender, System.EventArgs e)
        {
            ListFilter();
        }

        private void Rmodeid_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Rprocessid.Focus();
            }
            else if (e.KeyData == Keys.Up)
            {
                listBoxActive.PriorRow();
            }
            else if (e.KeyData == Keys.Down)
            {
                listBoxActive.NextRow();
            }
        }
        #endregion

        #region  ���Ƴ�ʽ
        private void Rprocessid_TextChanged(object sender, System.EventArgs e)
        {
            ListFilter();
        }

        private void Rprocessid_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Rdeviceid.Focus();
            }
            else if (e.KeyData == Keys.Up)
            {
                listBoxActive.PriorRow();
            }
            else if (e.KeyData == Keys.Down)
            {
                listBoxActive.NextRow();
            }
        }

        private void Rprocessid_Enter(object sender, System.EventArgs e)
        {
            if (Rprocessid.ReadOnly)
            {
                return;
            }
            contralActive = this.Rprocessid;
            listBoxActive = RprocessidListBox;
            ListBoxActiveVisible(true);
        }
        #endregion

        #region  ����װ��
        private void Rdeviceid_TextChanged(object sender, System.EventArgs e)
        {
            ListFilter();
        }

        private void Rdeviceid_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                gy1.Focus();
            }
            else if (e.KeyData == Keys.Up)
            {
                listBoxActive.PriorRow();
            }
            else if (e.KeyData == Keys.Down)
            {
                listBoxActive.NextRow();
            }
        }

        private void Rdeviceid_Enter(object sender, System.EventArgs e)
        {
            if (Rdeviceid.ReadOnly)
            {
                return;
            }
            contralActive = this.Rdeviceid;
            listBoxActive = RdeviceidListBox;
            ListBoxActiveVisible(true);
        }
        #endregion

        #region  ���Ʒ�ʽ
        private void Cmodeid_TextChanged(object sender, System.EventArgs e)
        {
            ListFilter();
        }

        private void Cmodeid_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Cmethod.Focus();
            }
            else if (e.KeyData == Keys.Up)
            {
                listBoxActive.PriorRow();
            }
            else if (e.KeyData == Keys.Down)
            {
                listBoxActive.NextRow();
            }
        }

        private void Cmodeid_Enter(object sender, System.EventArgs e)
        {
            if (Cmodeid.ReadOnly)
            {
                return;
            }
            contralActive = this.Cmodeid;
            listBoxActive = CmodeidListBox;
            ListBoxActiveVisible(true);
        }
        #endregion

        #region ���Ʒ���
        private void Cmethod_Enter(object sender, System.EventArgs e)
        {
            if (Cmethod.ReadOnly)
            {
                return;
            }
            contralActive = this.Cmethod;
            listBoxActive = CmethodListBox;
            ListBoxActiveVisible(true);
        }

        private void Cmethod_TextChanged(object sender, System.EventArgs e)
        {
            ListFilter();
        }

        private void Cmethod_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (fpEnter1_Sheet1.Rows.Count > 0)
                {
                    this.fpEnter1_Sheet1.SetActiveCell(0, 0);
                }
            }
            else if (e.KeyData == Keys.Up)
            {
                listBoxActive.PriorRow();
            }
            else if (e.KeyData == Keys.Down)
            {
                listBoxActive.NextRow();
            }
        }
        #endregion
        #endregion
        #region keypress�¼�
        private void gy1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar < 48 || e.KeyChar > 58)
            {
                e.Handled = true;
            }
        }
        #endregion
        #region �س��¼�
        private void gy1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.time1.Focus();
            }
        }

        private void time1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.day1.Focus();
            }
        }

        private void day1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.begin_date1.Focus();
            }
        }

        private void begin_date1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.end_date1.Focus();
            }
        }

        private void end_date1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.gy2.Focus();
            }
        }

        private void gy2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.time2.Focus();
            }
        }

        private void time2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.day2.Focus();
            }
        }

        private void day2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.begin_date2.Focus();
            }
        }

        private void begin_date2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.end_date2.Focus();
            }
        }

        private void end_date2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.gy3.Focus();
            }
        }

        private void gy3_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.time3.Focus();
            }
        }

        private void time3_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.day3.Focus();
            }
        }

        private void day3_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.begin_date3.Focus();
            }
        }

        private void begin_date3_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.end_date3.Focus();
            }
        }

        private void end_date3_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.Cmodeid.Focus();
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
                Neusoft.HISFC.Management.HealthRecord.Tumour da = new Neusoft.HISFC.Management.HealthRecord.Tumour();
                //���Ʒ�ʽ 
                //ArrayList RmodeidList = da.GetRmodeidList();
                //InitList(RmodeidListBox, RmodeidList);
                //RmodeidTypeHelper.ArrayObject = RmodeidList;

                //���Ƴ�ʽ 
                //ArrayList RprocessidList = da.GetRprocessidList();
                //InitList(RprocessidListBox, RprocessidList);
                //RprocessidTypeHelper.ArrayObject = RprocessidList;

                //����װ��
                //ArrayList RdeviceidList = da.GetRdeviceidList();
                //InitList(RdeviceidListBox, RdeviceidList);
                //RdeviceidTypeHelper.ArrayObject = RdeviceidList;

                //���Ʒ�ʽ
                //ArrayList CmodeidList = da.GetCmodeidList();
                //InitList(CmodeidListBox, CmodeidList);
                //CmodeidTypeHelper.ArrayObject = CmodeidList;

                //���Ʒ���
                //ArrayList CmethodList = da.GetCmethodList();
                //InitList(CmethodListBox, CmethodList);
                //CmethodTypeHelper.ArrayObject = CmethodList;

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
        private int InitList(Neusoft.NFC.Interface.Controls.PopUpListBox listBox, ArrayList list)
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
                listBox.SelectItem += new Neusoft.NFC.Interface.Controls.PopUpListBox.MyDelegate(ListBox_SelectItem);
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
            #region �����е������б��� ���ɼ�
            RmodeidListBox.Visible = false;
            RprocessidListBox.Visible = false;
            RdeviceidListBox.Visible = false;
            CmodeidListBox.Visible = false;
            CmethodListBox.Visible = false;
            #endregion
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
        private int ConvertInfoToPanel(Neusoft.HISFC.Object.HealthRecord.Tumour info)
        {
            try
            {
                Rmodeid.Tag = info.Rmodeid;//���Ʒ�ʽ
                Rmodeid.Text = RmodeidTypeHelper.GetName(info.Rmodeid);//���Ʒ�ʽ

                Rprocessid.Tag = info.Rprocessid;//���Ƴ�ʽ
                Rprocessid.Text = RprocessidTypeHelper.GetName(info.Rprocessid);//���Ƴ�ʽ

                Rdeviceid.Tag = info.Rdeviceid;//����װ��
                Rdeviceid.Text = RdeviceidTypeHelper.GetName(info.Rdeviceid);//����װ��
                gy1.Text = info.Gy1.ToString();//ԭ�������
                time1.Text = info.Time1.ToString();//ԭ������
                day1.Text = info.Day1.ToString();
                if (info.BeginDate1 != System.DateTime.MinValue)
                {
                    begin_date1.Value = info.BeginDate1;
                }
                else
                {
                    begin_date1.Value = System.DateTime.Now;
                }
                if (info.EndDate1 != System.DateTime.MinValue)
                {
                    end_date1.Value = info.EndDate1;
                }
                else
                {
                    end_date1.Value = System.DateTime.Now;
                }
                gy2.Text = info.Gy2.ToString(); //�����ܰͽ�
                time2.Text = info.Time2.ToString();
                day2.Text = info.Day2.ToString();
                if (info.BeginDate2 != System.DateTime.MinValue)
                {
                    begin_date2.Value = info.BeginDate2;
                }
                else
                {
                    begin_date2.Value = System.DateTime.Now;
                }
                if (info.EndDate2 != System.DateTime.MinValue)
                {
                    end_date2.Value = info.EndDate2;
                }
                else
                {
                    end_date2.Value = System.DateTime.Now;
                }
                gy3.Text = info.Gy3.ToString();//ת�������
                time3.Text = info.Time3.ToString();
                day3.Text = info.Day3.ToString();
                if (info.BeginDate3 != System.DateTime.MinValue)
                {
                    begin_date3.Value = info.BeginDate3;
                }
                else
                {
                    begin_date3.Value = System.DateTime.Now;
                }
                if (info.EndDate3 != System.DateTime.MinValue)
                {
                    end_date3.Value = info.EndDate3;
                }
                else
                {
                    end_date3.Value = System.DateTime.Now;
                }
                Cmodeid.Tag = info.Cmodeid;//���Ʒ�ʽ
                Cmodeid.Text = CmodeidTypeHelper.GetName(info.Cmodeid);//���Ʒ�ʽ
                Cmethod.Tag = info.Cmethod;//���Ʒ���
                Cmethod.Text = CmethodTypeHelper.GetName(info.Cmethod);//���Ʒ���
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
        public int ValueTumourSate(Neusoft.HISFC.Object.HealthRecord.Tumour info)
        {
            if (info == null)
            {
                MessageBox.Show("������ϢΪ��");
                return -1;
            }
            if (info.Rmodeid.Length > 2)
            {
                MessageBox.Show("���Ʒ�ʽ �������");
                return -1;
            }
            if (info.Rprocessid.Length > 2)
            {
                MessageBox.Show("���Ƴ�ʽ �������");
                return -1;
            }
            if (info.Rdeviceid.Length > 2)
            {
                MessageBox.Show("����װ�� �������");
                return -1;
            }
            //���Ʒ�ʽ    
            if (info.Cmodeid.Length > 2)
            {
                MessageBox.Show("����װ�� �������");
                return -1;
            }
            //���Ʒ��� 
            if (info.Cmethod.Length > 2)
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
            if (info.Rmodeid != "" || info.Rmodeid != null || info.Rprocessid != "" || info.Rprocessid != null || info.Rdeviceid != "" || info.Rdeviceid != null || info.Cmodeid != "" || info.Cmodeid != null || info.Cmethod != "" || info.Cmethod != null)
            {
                return 2;
            }
            return 1;
        }
        /// <summary>
        /// �������ϵ����� �ռ���ʵ���� 
        /// </summary>
        /// <returns></returns>
        public Neusoft.HISFC.Object.HealthRecord.Tumour GetTumourInfo()
        {
            Neusoft.HISFC.Object.HealthRecord.Tumour info = new Neusoft.HISFC.Object.HealthRecord.Tumour();

            try
            {
                info.InpatientNo = this.patientInfo.ID; //סԺ��ˮ��
                if (Rmodeid.Tag != null)
                {
                    info.Rmodeid = Rmodeid.Tag.ToString();//���Ʒ�ʽ
                }
                if (Rprocessid.Tag != null)
                {
                    info.Rprocessid = Rprocessid.Tag.ToString();//���Ƴ�ʽ
                }
                if (Rdeviceid.Tag != null)
                {
                    info.Rdeviceid = Rdeviceid.Tag.ToString();//����װ��
                }
                info.Gy1 = Neusoft.NFC.Function.NConvert.ToDecimal(gy1.Text);//ԭ�������
                info.Time1 = Neusoft.NFC.Function.NConvert.ToDecimal(time1.Text);//ԭ������
                info.Day1 = Neusoft.NFC.Function.NConvert.ToDecimal(day1.Text);
                info.BeginDate1 = begin_date1.Value;
                info.EndDate1 = end_date1.Value;
                info.Gy2 = Neusoft.NFC.Function.NConvert.ToDecimal(gy2.Text); //�����ܰͽ�
                info.Time2 = Neusoft.NFC.Function.NConvert.ToDecimal(time2.Text);
                info.Day2 = Neusoft.NFC.Function.NConvert.ToDecimal(day2.Text);
                info.BeginDate2 = begin_date2.Value;
                info.EndDate2 = end_date2.Value;
                info.Gy3 = Neusoft.NFC.Function.NConvert.ToDecimal(gy3.Text);//ת�������
                info.Time3 = Neusoft.NFC.Function.NConvert.ToDecimal(time3.Text);
                info.Day3 = Neusoft.NFC.Function.NConvert.ToDecimal(day3.Text);
                info.BeginDate3 = begin_date3.Value;
                info.EndDate3 = end_date3.Value;
                if (Cmodeid.Tag != null)
                {
                    info.Cmodeid = Cmodeid.Tag.ToString();//���Ʒ�ʽ
                }
                if (Cmethod.Tag != null)
                {
                    info.Cmethod = Cmethod.Tag.ToString();//���Ʒ���
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
            Rmodeid.ReadOnly = type;//���Ʒ�ʽ
            Rprocessid.ReadOnly = type;//���Ƴ�ʽ
            Rdeviceid.ReadOnly = type;//����װ��
            gy1.ReadOnly = type;//ԭ�������
            time1.ReadOnly = type;//ԭ������
            day1.ReadOnly = type;
            begin_date1.Enabled = !type;
            end_date1.Enabled = !type;
            gy2.ReadOnly = type; //�����ܰͽ�
            time2.ReadOnly = type;
            day2.ReadOnly = type;
            begin_date2.Enabled = !type;
            end_date2.Enabled = !type;
            gy3.ReadOnly = type;//ת�������
            time3.ReadOnly = type;
            day3.ReadOnly = type;
            begin_date3.Enabled = !type;
            end_date3.Enabled = !type;
            Cmodeid.ReadOnly = type;//���Ʒ�ʽ
            Cmethod.ReadOnly = type;//���Ʒ���
        }
        private void ClearTumourInfo()
        {
            Rmodeid.Text = "";//���Ʒ�ʽ
            Rmodeid.Tag = null;
            Rprocessid.Text = "";//���Ƴ�ʽ
            Rprocessid.Tag = null;
            Rdeviceid.Text = "";//����װ��
            Rdeviceid.Tag = null;
            gy1.Text = "";//ԭ�������
            time1.Text = "";//ԭ������
            day1.Text = "";
            begin_date1.Value = System.DateTime.Now;
            end_date1.Value = System.DateTime.Now;
            gy2.Text = ""; //�����ܰͽ�
            time2.Text = "";
            day2.Text = "";
            begin_date2.Value = System.DateTime.Now;
            end_date2.Value = System.DateTime.Now;
            gy3.Text = "";//ת�������
            time3.Text = "";
            day3.Text = "";
            begin_date3.Value = System.DateTime.Now;
            end_date3.Value = System.DateTime.Now;
            Cmodeid.Tag = null;//���Ʒ�ʽ
            Cmodeid.Text = "";//���Ʒ�ʽ
            Cmethod.Tag = null;//���Ʒ���
            Cmethod.Text = "";//���Ʒ���
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
            this.fpEnter1_Sheet1.Columns[0].Width = 63; //ʱ��
            this.fpEnter1_Sheet1.Columns[1].Width = 129;//ҩ������
            this.fpEnter1_Sheet1.Columns[5].Width = 60;//����
            this.fpEnter1_Sheet1.Columns[2].Width = 40; //��λ
            this.fpEnter1_Sheet1.Columns[3].Width = 40; //�Ƴ�
            this.fpEnter1_Sheet1.Columns[4].Width = 80; //���
            this.fpEnter1_Sheet1.Columns[6].Width = 100; //ҩƷ����
            this.fpEnter1_Sheet1.Columns[6].Locked = true;//ҩƷ����
            this.fpEnter1_Sheet1.Columns[7].Visible = false; //���
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
            foreach (Neusoft.HISFC.Object.HealthRecord.TumourDetail obj in list)
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
                else if (obj.DrugInfo.Name.Length > 50)
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
            foreach (Neusoft.HISFC.Object.HealthRecord.TumourDetail info in list)
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
        /// creator:zhangjunyi@Neusoft.com
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
														   new DataColumn("ʱ��", dtType),	//0
														   new DataColumn("ҩ������", strType),	 //1
														   new DataColumn("��λ", strType),//2
														   new DataColumn("�Ƴ�", strType),//3
														   new DataColumn("���", strType),//4
														   new DataColumn("����", strType),//5
														   new DataColumn("ҩƷ����", strType),//6
														   new DataColumn("���", intType)});//7
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
                Neusoft.HISFC.Object.HealthRecord.TumourDetail info = null;
                foreach (DataRow row in tempTable.Rows)
                {
                    info = new Neusoft.HISFC.Object.HealthRecord.TumourDetail();
                    info.InpatientNO = this.patientInfo.ID;
                    if (row["ʱ��"] != DBNull.Value)
                    {
                        info.CureDate = Neusoft.NFC.Function.NConvert.ToDateTime(row["ʱ��"].ToString());
                    }
                    if (row["ҩ������"] != DBNull.Value)
                    {
                        info.DrugInfo.Name = row["ҩ������"].ToString();//1
                    }
                    if (row["����"] != DBNull.Value)
                    {
                        info.Qty = Neusoft.NFC.Function.NConvert.ToDecimal(row["����"]);//1
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
                        info.HappenNO = Neusoft.NFC.Function.NConvert.ToInt32(row["���"]);//1
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
            if (fpEnter1_Sheet1.Rows.Count > 0)
            {
                this.fpEnter1_Sheet1.Rows.Remove(fpEnter1_Sheet1.ActiveRowIndex, 1);
            }
            if (fpEnter1_Sheet1.Rows.Count == 0)
            {
                this.fpEnter1.SetAllListBoxUnvisible();
            }
            return 1;
        }
        /// <summary>
        /// ɾ���հ׵���
        /// </summary>
        /// <returns></returns>
        public int deleteRow()
        {
            if (fpEnter1_Sheet1.Rows.Count == 1)
            {
                //��һ�б���Ϊ�� 
                if (fpEnter1_Sheet1.Cells[0, 1].Text == "")
                {
                    fpEnter1_Sheet1.Rows.Remove(0, 1);
                }
            }
            return 1;
        }
        /// <summary>
        /// ��ѯ����ʾ����
        /// </summary>
        /// <returns>������ ��1 ���� 0 �������в���1  </returns>
        public int LoadInfo(Neusoft.HISFC.Object.RADT.PatientInfo patient, Neusoft.HISFC.Object.HealthRecord.EnumServer.frmTypes Type)
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
            Neusoft.HISFC.Management.HealthRecord.Tumour tumour = new Neusoft.HISFC.Management.HealthRecord.Tumour();
            //��ѯ��������������
            ArrayList list = tumour.QueryTumourDetail(patient.ID);
            AddInfoToTable(list);
            Neusoft.HISFC.Object.HealthRecord.Tumour obj = tumour.GetTumour(patient.ID);
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
        private void SetRow(Neusoft.HISFC.Object.HealthRecord.TumourDetail info, DataRow row)
        {
            row["ʱ��"] = info.CureDate;//0
            row["ҩ������"] = info.DrugInfo.Name;//1
            row["����"] = info.Qty;//2
            row["��λ"] = UnitListHelper.GetName(info.Unit);            //3
            row["�Ƴ�"] = PeriodListHelper.GetName(info.Period);//4
            row["���"] = ResultListHelper.GetName(info.Result);//5
            row["ҩƷ����"] = info.DrugInfo.ID;//6
            row["���"] = info.HappenNO;//7
        }
        private enum Col
        {
            colTime = 0,//ʱ��
            DrugName = 1,//ҩƷ����
            Unit = 2,//��λ
            Preiod = 3,//�Ƴ�
            Result = 4,//���
            Qty = 5,//����
            DrugCode = 6 //ҩƷ����

        }
        /// <summary>
        /// �����������б�
        /// </summary>
        private void initList()
        {
            try
            {
                Neusoft.HISFC.Management.HealthRecord.Tumour da = new Neusoft.HISFC.Management.HealthRecord.Tumour();
                Neusoft.HISFC.Management.Manager.Constant con = new Neusoft.HISFC.Management.Manager.Constant();
                Neusoft.HISFC.Integrate.Pharmacy item = new Neusoft.HISFC.Integrate.Pharmacy();
                fpEnter1.SetWidthAndHeight(200, 200);
                fpEnter1.SelectNone = true;
                //ҩƷ��Ϣ
                ArrayList druglist = new ArrayList();
                //List<Neusoft.HISFC.Object.Pharmacy.Item> druglist = item.QueryItemAvailableList(true);
                //this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, (int)Col.DrugName, druglist);
                //this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, (int)Col.DrugCode, druglist);
                //ҩƷ��Ϣ����ʾID��
                this.fpEnter1.SetIDVisiable(this.fpEnter1_Sheet1, (int)Col.DrugName, false);
                this.fpEnter1.SetIDVisiable(this.fpEnter1_Sheet1, (int)Col.DrugCode, false);
                //��λ�б�
                ArrayList UnitList = con.GetList(Neusoft.HISFC.Object.Base.EnumConstant.DOSEUNIT);
                this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, (int)Col.Unit, UnitList);
                UnitListHelper.ArrayObject = UnitList;

                //�Ƴ��б� 
                //ArrayList PeriodList = da.GetPeriodList(); ;
                //this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, (int)Col.Preiod, PeriodList);
                //PeriodListHelper.ArrayObject = PeriodList;

                //j����б�
                //ArrayList ResultList = da.GetResultList();
                //this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, (int)Col.Result, ResultList);
                //ResultListHelper.ArrayObject = ResultList;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ucTumourCard_Load(object sender, System.EventArgs e)
        {
            //������Ӧ�����¼�
            fpEnter1.KeyEnter += new Neusoft.NFC.Interface.Controls.NeuFpEnter.keyDown(fpEnter1_KeyEnter);
            fpEnter1.SetItem += new Neusoft.NFC.Interface.Controls.NeuFpEnter.setItem(fpEnter1_SetItem);
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
                    row["ʱ��"] = System.DateTime.Now;
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
        /// ѡ��ѡ�е���
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private int fpEnter1_SetItem(Neusoft.NFC.Object.NeuObject obj)
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
                Neusoft.NFC.Interface.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)Col.DrugName);
                //��ȡѡ�е���Ϣ
                Neusoft.NFC.Object.NeuObject item = null;
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
                Neusoft.NFC.Interface.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)Col.DrugName);
                //��ȡѡ�е���Ϣ
                Neusoft.NFC.Object.NeuObject item = null;
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
                Neusoft.NFC.Interface.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)Col.Unit);
                //��ȡѡ�е���Ϣ
                Neusoft.NFC.Object.NeuObject item = null;
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
                Neusoft.NFC.Interface.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)Col.Preiod);
                //��ȡѡ�е���Ϣ
                Neusoft.NFC.Object.NeuObject item = null;
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
                Neusoft.NFC.Interface.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)Col.Result);
                //��ȡѡ�е���Ϣ
                Neusoft.NFC.Object.NeuObject item = null;
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
            //			Neusoft.UFC.Common.Controls.ucSetColumn uc = new Neusoft.UFC.Common.Controls.ucSetColumn();
            //			uc.FilePath = this.filePath;
            //			uc.GoDisplay += new Neusoft.UFC.Common.Controls.ucSetColumn.DisplayNow(uc_GoDisplay);
            //			Neusoft.NFC.Interface.Classes.Function.PopShowControl(uc);
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
    }
}
