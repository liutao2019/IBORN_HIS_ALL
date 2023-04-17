using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.Local.RADT.ZhuHai.ZDWY.Base.Inpatient
{
    /// <summary>
    /// �����ˣ�����
    /// 
    /// �޸��ˣ�FZC
    /// �޸����ݣ������޸����������ע��
    /// �޸����ڣ�2014-10-02 
    /// PS��UI��������û�¼��İ�ť ��֪����ɶ�� ��������
    /// </summary>
    public partial class frmQueryPatientInfoNew : FS.FrameWork.WinForms.Forms.BaseForm, FS.SOC.HISFC.RADT.Interface.Patient.IQuery
    {
        public frmQueryPatientInfoNew()
        {
            InitializeComponent();
            this.tcMain.SelectedTab = this.tbpageName;
        }

        #region ����

        /// <summary>
        /// ѡ�еĻ�����Ϣ
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// �������Ļ�����Ϣ
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfoCompare = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// ��ϵͳ��ѯ����
        /// </summary>
        string sqlWhereNew = string.Empty;

        /// <summary>
        /// ��ϵͳ��ѯ����
        /// </summary>
        string sqlWhereOld = string.Empty;

        /// <summary>
        /// �Ƿ�ѡ�л���
        /// </summary>
        public bool isSelect = false;

        /// <summary>
        /// �Ƿ���ϵͳ����
        /// </summary>
        public bool isNewPerson = true;


        /// <summary>
        /// �Ƿ������ʷ��ʱסԺ�ŵĻ���
        /// </summary>
        private bool isFiltTemPatient = true;
        /// <summary>
        /// �Ƿ������ʷ��ʱסԺ�ŵĻ���
        /// </summary>
        public bool IsFiltTemPatient
        {
            get { return isFiltTemPatient; }
            set { isFiltTemPatient = value; }
        }

        #endregion

        #region ����

        private int Init()
        {
            return 1;
        }

        /// <summary>
        /// ��ö����ѯ����
        /// </summary>
        /// <returns></returns>
        private int getSqlWhere()
        {
            //�������޷���Ժ����Ϣ
            sqlWhereNew = "where 1=1 and in_state <> 'N' ";
            sqlWhereOld = "where 1=1 ";

            try
            {
                #region ѡ���������Ա�Tabҳ

                if (this.tcMain.SelectedTab == this.tbpageName)
                {
                    #region ����
                    if (this.rbtLike.Checked)
                    {
                        if (!string.IsNullOrEmpty(this.txtName.Text))
                        {
                            sqlWhereNew += " and name like '%" + this.txtName.Text + "%'";
                            sqlWhereOld += "and name like  '%" + this.txtName.Text + "%'";
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.txtName.Text))
                        {
                            sqlWhereNew += " and name = '" + this.txtName.Text + "'";
                            sqlWhereOld += "and name =  '" + this.txtName.Text + "'";
                        }
                    }

                    #endregion
                    #region ���֤��// {249D5ADE-8BE6-4c90-A8D1-F99540D24A64}
                    if (this.rbtLike.Checked)
                    {
                        if (!string.IsNullOrEmpty(this.txtCardID.Text))
                        {
                            sqlWhereNew += " and idenno like '%" + this.txtCardID.Text + "%'";
                            sqlWhereOld += "and idenno like  '%" + this.txtCardID.Text + "%'";
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.txtCardID.Text))
                        {
                            sqlWhereNew += " and idenno = '" + this.txtCardID.Text + "'";
                            sqlWhereOld += "and idenno =  '" + this.txtCardID.Text + "'";
                        }
                    }

                    #endregion

                    #region �Ա�

                    if (this.rbSexM.Checked == true)
                    {
                        sqlWhereNew += " and sex_code = 'M'";
                        sqlWhereOld += " and sex_code = 'M'";
                    }
                    else if (this.rbSexF.Checked == true)
                    {
                        sqlWhereNew += " and sex_code = 'F'";
                        sqlWhereOld += " and sex_code = 'F'";
                    }
                    else if (this.rbDimness.Checked == true)
                    {
                        sqlWhereNew += " and sex_code like '%%'";
                        sqlWhereOld += " and sex_code like '%%'";
                    }

                    #endregion
                }
                #endregion

                #region ѡ��סԺ��Tabҳ

                else if (this.tcMain.SelectedTab == this.tbpagePatientNo)
                {
                    if (string.IsNullOrEmpty(this.txtPatientNo.Text))
                    {
                        MessageBox.Show("δ����סԺ��!", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return -1;
                    }

                    if (this.rbtLike.Checked)
                    {
                        sqlWhereNew += " and patient_no like '%" + this.txtPatientNo.Text.PadLeft(10, '0') + "%'";
                        sqlWhereOld += " and card_no like '%" + this.txtPatientNo.Text.Trim() + "%'";
                    }
                    else
                    {
                        sqlWhereNew += " and patient_no = '" + this.txtPatientNo.Text.PadLeft(10, '0') + "'";
                        sqlWhereOld += " and card_no  ='" + this.txtPatientNo.Text.Trim() + "'";
                    }

                    sqlWhereNew += "and INPATIENT_NO Not LIKE '%B%' ";
                    sqlWhereOld += "and card_no Not LIKE '%*%' ";
                }
                #endregion
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ���
        /// </summary>
        private void clear()
        {
            //this.tcMain.SelectedTab = this.tbpageName;
            this.isSelect = false;
            this.isNewPerson = true;
            this.rbtSame.Checked = true;

            this.lblNewPatient.Text = "ϵͳ��ȡ:0";
            this.sheetNewPatient.RowCount = 0;
        }

        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        /// <returns></returns>
        private int queryPatient()
        {
            ArrayList alNewPatient = Function.QueryPatientInfo(this.sqlWhereNew);
            //{67F0DA44-EB32-4556-96C5-88B60C59E157}
            //��ϵͳ�����ǴӲ���ϵͳ�в������ÿ�����ߡ���Ժ���������ļ�¼
            //��ÿ���������һ�ε���Ժ��¼סԺ��
            ArrayList alOldPatient = Function.QueryOldSysPatientInfo(sqlWhereOld);

            //��ͬ��סԺ��ֻ��ʾ����һ�μ�¼
            Hashtable htTempPatient = new Hashtable();
            ArrayList alPatient;

            //HISϵͳ����
            foreach (FS.HISFC.Models.RADT.PatientInfo p in alNewPatient)
            {
                if (this.isFiltTemPatient)
                {
                    if (!Regex.IsMatch(p.PID.PatientNO,@"^\d+$"))
                    {
                        continue;
                    }
                }
                if (htTempPatient.ContainsKey(p.PID.PatientNO.TrimStart('0')))
                {
                    FS.HISFC.Models.RADT.PatientInfo temp = htTempPatient[p.PID.PatientNO.TrimStart('0')] as FS.HISFC.Models.RADT.PatientInfo;
                    if (temp != null && temp.InTimes < p.InTimes)
                    {
                        htTempPatient.Remove(p.PID.PatientNO.TrimStart('0'));
                        htTempPatient.Add(p.PID.PatientNO.TrimStart('0'), p);
                    }
                }
                else
                {
                    htTempPatient.Add(p.PID.PatientNO.TrimStart('0'), p);
                }
            }

            //����ϵͳ�еĻ�����Ϣ
            foreach (FS.HISFC.Models.RADT.PatientInfo p in alOldPatient)
            {
                if (this.isFiltTemPatient)
                {
                    if (!Regex.IsMatch(p.PID.PatientNO, @"^\d+$"))
                    {
                        continue;
                    }
                }
                if (htTempPatient.ContainsKey(p.PID.PatientNO.TrimStart('0')))
                {
                    FS.HISFC.Models.RADT.PatientInfo temp = htTempPatient[p.PID.PatientNO.TrimStart('0')] as FS.HISFC.Models.RADT.PatientInfo;
                    if (temp != null && temp.InTimes < p.InTimes)
                    {
                        htTempPatient.Remove(p.PID.PatientNO.TrimStart('0'));
                        htTempPatient.Add(p.PID.PatientNO.TrimStart('0'), p);
                    }
                }
                else
                {
                    htTempPatient.Add(p.PID.PatientNO.TrimStart('0'), p);
                }
            }

            alPatient = new ArrayList(htTempPatient.Values);
            if (alPatient != null && alPatient.Count > 0)
            {
                this.patientInfo = alPatient[0] as FS.HISFC.Models.RADT.PatientInfo;
                this.setValues(alPatient, this.sheetNewPatient);
                this.sheetNewPatient.ActiveRowIndex = 0;
                this.sheetNewPatient.AddSelection(0, 0, 1, 1);
                this.lblNewPatient.Text = "ϵͳ��ȡ:" + alPatient.Count.ToString();
            }
            else if (alPatient != null && alPatient.Count == 0)
            {
                return 0;
            }

            return 1;
        }

        /// <summary>
        /// ��ѯ��ϵͳ������Ϣ
        /// </summary>
        /// <param name="queryType"></param>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        private int queryOldPatient()
        {
            ArrayList alOldPatient = Function.QueryOldSysPatientInfo(sqlWhereOld);
            ArrayList newOldPatient = new ArrayList();
            foreach (FS.HISFC.Models.RADT.PatientInfo p in alOldPatient)
            {

                if (this.isFiltTemPatient)
                {
                    if (!Regex.IsMatch(p.PID.PatientNO, @"^\d+$"))
                    {
                        continue;
                    }
                }

                newOldPatient.Add(p);
            }

            return 1;
        }

        /// <summary>
        /// �����б�ֵ
        /// </summary>
        /// <param name="alPatient"></param>
        /// <param name="sheetView"></param>
        private void setValues(ArrayList alPatient, FarPoint.Win.Spread.SheetView sheetView)
        {
            try
            {
                foreach (FS.HISFC.Models.RADT.PatientInfo p in alPatient)
                {
                    sheetView.Rows.Add(sheetView.RowCount, 1);
                    int row = sheetView.RowCount - 1;
                    sheetView.SetValue(row, (int)enuInfoColumnNew.Name, p.Name);
                    sheetView.SetValue(row, (int)enuInfoColumnNew.Sex, p.Sex.ID.ToString() == "M" ? "��" : "Ů");
                    sheetView.SetValue(row, (int)enuInfoColumnNew.InpatientNo, p.PID.PatientNO.TrimStart('0'));
                    sheetView.SetValue(row, (int)enuInfoColumnNew.DeptName, p.PVisit.PatientLocation.Dept.Name);
                    sheetView.SetValue(row, (int)enuInfoColumnNew.BirthDate, p.Birthday.ToString("yyyy-MM-dd"));
                    sheetView.SetValue(row, (int)enuInfoColumnNew.WorkName, p.CompanyName);
                    sheetView.SetValue(row, (int)enuInfoColumnNew.BirthArea, p.AreaCode);
                    sheetView.SetValue(row, (int)enuInfoColumnNew.HomeArea, p.AddressHome);
                    //{70C44B29-EC2A-4bc2-BFFE-ED55DC9C7560}��ʱ�����Ϊ24Сʱ��
                    sheetView.SetValue(row, (int)enuInfoColumnNew.InDate, p.PVisit.InTime.ToString("yyyy-MM-dd HH:m"));
                    sheetView.SetValue(row, (int)enuInfoColumnNew.OutDate, p.PVisit.OutTime.ToString("yyyy-MM-dd HH:m"));
                    sheetView.SetValue(row, (int)enuInfoColumnNew.IdenNo, p.IDCard);
                    sheetView.SetValue(row, (int)enuInfoColumnNew.HomePhone, p.PhoneHome);
                    sheetView.SetValue(row, (int)enuInfoColumnNew.LinkMan, p.Kin.Name);
                    sheetView.SetValue(row, (int)enuInfoColumnNew.InTimes, p.InTimes);
                    string strInState = "";
                    switch (p.PVisit.InState.ID.ToString())
                    {
                        case "R":
                            strInState = "��Ժ";
                            break;
                        case "I":
                            strInState = "��Ժ";
                            break;
                        case "B":
                            strInState = "��Ժ�Ǽ�";
                            break;
                        case "O":
                            strInState = "�����Ժ";
                            break;
                        case "P":
                            strInState = "ԤԼ��Ժ";
                            break;
                        case "N":
                            strInState = "�޷���Ժ";
                            break;
                        case "C":
                            strInState = "ȡ��";
                            break;
                    }
                    sheetView.SetValue(row, (int)enuInfoColumnNew.InState, strInState);

                    sheetView.Rows[row].Tag = p;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
        }

        /// <summary>
        /// ��������ѯ���߻�����Ϣ
        /// </summary>
        /// <param name="queryType"></param>
        private int query(FS.SOC.HISFC.RADT.Interface.Patient.EnumQueryType queryType)
        {
            this.clear();

           
            switch (queryType)
            {
                case FS.SOC.HISFC.RADT.Interface.Patient.EnumQueryType.PatientNo:
                    this.tcMain.SelectedTab = this.tbpagePatientNo;
                    break;
                case FS.SOC.HISFC.RADT.Interface.Patient.EnumQueryType.NameSex:
                    this.tcMain.SelectedTab = this.tbpageName;
                    break;
            }

            //��ȡ��ϲ�ѯ����
            if (this.getSqlWhere() == -1)
            {
                return -1;
            }

            #region ԭ�Ⱥ�������������⣬��ѯ�¾ɻ��ߺϳ�һ������
            //{67F0DA44-EB32-4556-96C5-88B60C59E157}
            //if (this.queryOldPatient() == -1)
            //{
            //    return -1;
            //}

            //int returnInt = -1;
            //returnInt = this.queryNewPatient();
            #endregion 

            int rtn = -1;
            rtn = this.queryPatient();

            this.btSelect.Focus();
            this.btSelect.Select();

            return rtn;
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private int query(TabPage page)
        {
            //��ѯǰ������������ û�� ע��
            //this.clear();
            if (page.Text == "סԺ��")
            {
                this.isFiltTemPatient = false;
            }
            this.lblNewPatient.Text = "ϵͳ��ȡ:0";
            this.sheetNewPatient.RowCount = 0;
            //��ȡ��ϲ�ѯ����
            if (this.getSqlWhere() == -1)
            {
                return -1;
            }

            #region ԭ�Ⱥ�������������⣬��ѯ�¾ɻ��ߺϳ�һ������
            //{67F0DA44-EB32-4556-96C5-88B60C59E157}
            //if (this.queryOldPatient() == -1)
            //{
            //    return -1;
            //}

            //int returnInt = -1;
            //returnInt = this.queryNewPatient();
            #endregion 

            if (this.queryPatient() == -1)
            {
                return -1;
            }

            return 1;
        }

        #endregion

        #region �¼�

        /// <summary>
        /// ��ʼ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbtSearch_Click(object sender, EventArgs e)
        {
            this.query(this.tcMain.SelectedTab);
        }

        /// <summary>
        /// ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSelect_Click(object sender, EventArgs e)
        {

            //{51C02BB2-BBC3-45c2-B985-5E14ECEB0943}
            if (this.PatientInfo != null && !string.IsNullOrEmpty(this.PatientInfo.PID.CardNO))
            {
                FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
                FS.HISFC.Models.RADT.PatientInfo tmp = radtIntegrate.QueryComPatientInfo(this.PatientInfo.PID.CardNO);

                if (tmp != null && tmp.Memo == "���ϻ���")
                {
                    MessageBox.Show("�û��������Ѿ����ϣ�");
                    return;
                }
            }

            if (this.PatientInfo != null && this.patientInfoCompare != null )
            {
                string errMsg = string.Empty;

                if (this.patientInfoCompare.Name != this.PatientInfo.Name)
                {
                    errMsg += "�� ��ѡ���סԺ��¼�뵱ǰ���ߵġ�����������ͬ!\r\n";
                }

                if (this.patientInfoCompare.Birthday != this.PatientInfo.Birthday)
                {
                    errMsg += "�� ��ѡ���סԺ��¼�뵱ǰ���ߵġ����ա�����ͬ!\r\n";
                }

                if (string.IsNullOrEmpty(this.PatientInfo.IDCard))
                {
                    this.PatientInfo.IDCard = "";
                }

                if (string.IsNullOrEmpty(this.patientInfoCompare.IDCard))
                {
                    this.patientInfoCompare.IDCard = "";
                }

                if (this.patientInfoCompare.IDCard != this.PatientInfo.IDCard )
                {
                    errMsg += "�� ��ѡ���סԺ��¼�뵱ǰ���ߵġ����֤���롿����ͬ!\r\n";
                }

                if (this.patientInfoCompare.PhoneHome != this.PatientInfo.PhoneHome)
                {
                    errMsg += "�� ��ѡ���סԺ��¼�뵱ǰ���ߵġ��绰���롿����ͬ!\r\n";
                }

                if (!string.IsNullOrEmpty(errMsg))
                {
                    errMsg += "\r\n�����ȷ�ϣ�\r\n";
                    errMsg += "\r\nȷ��ѡ��������ȷ����������ѡ��������ȡ����";

                    if (MessageBox.Show(errMsg, "����", MessageBoxButtons.OKCancel,MessageBoxIcon.Information) != DialogResult.OK)
                    {
                        return;
                    }
                }

            }

            this.isSelect = true;
            this.Close();

            //frmPatientInfo frmPatientInfo = new frmPatientInfo();
            //frmPatientInfo.Init(this.patientInfo);
            //frmPatientInfo.ShowDialog(this);
            //frmPatientInfo.BringToFront();
            //if (frmPatientInfo.IsOK == "0")
            //{
            //    return;
            //}
            //else if (frmPatientInfo.IsOK == "1")
            //{
            //    this.isSelect = true;
            //    this.Close();

            //    return;
            //}
            //else if (frmPatientInfo.IsOK == "2")
            //{
            //    this.isSelect = false;
            //    this.Close();
            //    return;
            //}
        }

        private void btNoSelect_Click(object sender, EventArgs e)
        {
            this.isSelect = false;
            this.Close();
        }

        private void frmQueryPatientInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.isSelect = false;
                this.Close();
            }
        }

        #endregion

        #region  ���⹫�з���  //IQueryPatientInfo ��Ա

        /// <summary>
        /// ��ǰ������Ϣ
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get
            {
                return patientInfo;
            }
            set
            {
                patientInfo = value;
                patientInfoCompare = value.Clone();

                if (patientInfo != null)
                {
                    //����
                    this.txtName.Text = patientInfo.Name;
                    this.txtCardID.Text = patientInfo.IDCard;
                    //�Ա�
                    switch (patientInfo.Sex.Name.Trim())
                    {
                        case "��":
                            this.rbSexM.Checked = true;
                            break;
                        case "Ů":
                            this.rbSexF.Checked = true;
                            break;
                        default:
                            this.rbDimness.Checked = true;
                            break;
                    }
                    //סԺ��
                    this.txtPatientNo.Text = patientInfo.PID.PatientNO;
                }
            }
        }

        public bool IsSelect
        {
            get
            {
                return isSelect;
            }
        }

        public bool IsOldSystem
        {
            get
            {
                return !isNewPerson;
            }
        }

        public int Query(FS.SOC.HISFC.RADT.Interface.Patient.EnumQueryType enumQueryType)
        {
            return this.query(enumQueryType);
        }

        public new void Show(IWin32Window owner)
        {
            this.ShowDialog(owner);
        }

        public void Clear()
        {
            this.clear();
        }

        #endregion

        private void spNewPatientInfo_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.patientInfo = this.sheetNewPatient.Rows[e.Row].Tag as FS.HISFC.Models.RADT.PatientInfo;
        
        }

    }

    #region ö��


    /// <summary>
    /// ������Ϣö����
    /// </summary>
    public enum enuInfoColumnNew
    {
        /// <summary>
        /// ����
        /// </summary>
        Name = 1,

        /// <summary>
        /// �Ա�
        /// </summary>
        Sex,

        /// <summary>
        /// סԺ��
        /// </summary>
        InpatientNo,

        /// <summary>
        /// ��������
        /// </summary>
        BirthDate,

        /// <summary>
        /// ��ǰ����
        /// </summary>
        DeptName,

        /// <summary>
        /// ��־
        /// </summary>
        InState,

        /// <summary>
        /// ���֤��
        /// </summary>
        IdenNo,

        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        HomePhone,

        /// <summary>
        /// ������
        /// </summary>
        BirthArea,

        /// <summary>
        /// ��ͥ��ַ
        /// </summary>
        HomeArea,

        /// <summary>
        /// ��Ժ����
        /// </summary>
        InDate,

        /// <summary>
        /// ��Ժ����
        /// </summary>
        OutDate,

        /// <summary>
        /// ��λ����ַ
        /// </summary>
        WorkName,

        /// <summary>
        /// ��ϵ��
        /// </summary>
        LinkMan,

        /// <summary>
        /// סԺ����
        /// </summary>
        InTimes
    }

    #endregion
}