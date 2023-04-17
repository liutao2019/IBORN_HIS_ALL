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
    public partial class frmQueryPatientInfo : FS.FrameWork.WinForms.Forms.BaseForm, FS.SOC.HISFC.RADT.Interface.Patient.IQuery
    {
        public frmQueryPatientInfo()
        {
            InitializeComponent();
            this.tcMain.SelectedTab = this.tbpageName;
        }

        #region ����

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

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

        /// <summary>
        /// ��ǰѡ��Ļ������
        /// </summary>
        private enumSelectPatient selectPatient = enumSelectPatient.NewPatient;

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
            this.cbxNewSys.Checked = true;
            this.cbxOldSys.Checked = true;
            this.rbtSame.Checked = true;

            this.lblNewPatient.Text = "��ϵͳ��ȡ:0";
            this.lblOldPatient.Text = "��ϵͳ��ȡ:0";
            this.sheetNewPatient.RowCount = 0;
            this.sheetOldPatient.RowCount = 0;
        }

        /// <summary>
        /// ��ѯ��ϵͳ������Ϣ
        /// </summary>
        /// <returns></returns>
        private int queryNewPatient()
        {
            ArrayList alNewPatient = Function.QueryPatientInfo(this.sqlWhereNew);

            //��ͬ��סԺ��ֻ��ʾ����һ�μ�¼
            Hashtable htTempPatient = new Hashtable();
            ArrayList alPatient;
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
            alPatient = new ArrayList(htTempPatient.Values);

            if (alPatient != null && alPatient.Count > 0)
            {
                this.setValues(alPatient, this.sheetNewPatient);
                this.sheetNewPatient.ActiveRowIndex = 0;
                this.sheetNewPatient.AddSelection(0, 0, 1, 1);
                this.selectPatient = enumSelectPatient.NewPatient;
                this.lblNewPatient.Text = "��ϵͳ��ȡ:" + alPatient.Count.ToString();
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
            if (newOldPatient != null && newOldPatient.Count > 0)
            {
                this.setValues(newOldPatient, this.sheetOldPatient);

                this.sheetOldPatient.ActiveRowIndex = 0;
                this.sheetOldPatient.AddSelection(0, 0, 1, 1);
                this.selectPatient = enumSelectPatient.OldPatient;
                this.lblOldPatient.Text = "��ϵͳ��ȡ:" + alOldPatient.Count.ToString();
                
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
                    sheetView.SetValue(row, (int)enuInfoColumn.Name, p.Name);
                    sheetView.SetValue(row, (int)enuInfoColumn.Sex, p.Sex.ID.ToString() == "M" ? "��" : "Ů");
                    sheetView.SetValue(row, (int)enuInfoColumn.InpatientNo, p.PID.PatientNO.TrimStart('0'));
                    sheetView.SetValue(row, (int)enuInfoColumn.DeptName, p.PVisit.PatientLocation.Dept.Name);
                    sheetView.SetValue(row, (int)enuInfoColumn.BirthDate, p.Birthday.ToString("yyyy-MM-dd"));
                    sheetView.SetValue(row, (int)enuInfoColumn.WorkName, p.CompanyName);
                    sheetView.SetValue(row, (int)enuInfoColumn.BirthArea, p.AreaCode);
                    sheetView.SetValue(row, (int)enuInfoColumn.HomeArea, p.AddressHome);
                    sheetView.SetValue(row, (int)enuInfoColumn.InDate, p.PVisit.InTime.ToString("yyyy-MM-dd hh:m"));
                    sheetView.SetValue(row, (int)enuInfoColumn.OutDate, p.PVisit.OutTime.ToString("yyyy-MM-dd hh:m"));
                    sheetView.SetValue(row, (int)enuInfoColumn.IdenNo, p.IDCard);
                    sheetView.SetValue(row, (int)enuInfoColumn.LinkMan, p.Kin.Name);
                    sheetView.SetValue(row, (int)enuInfoColumn.InTimes, p.InTimes);
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
                            strInState = "����";
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
                    sheetView.SetValue(row, (int)enuInfoColumn.InState, strInState);

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

            if (this.queryOldPatient() == -1)
            {
                return -1;
            }

            if (this.queryNewPatient() == -1)
            {
                return -1;
            }

            //���û�в鵽���߾ͷ���
            if (this.sheetNewPatient.RowCount <= 0 && this.sheetOldPatient.RowCount <= 0)
            {
                return -1;
            }
            else if ((this.sheetNewPatient.RowCount <= 0 && this.sheetOldPatient.RowCount > 0))
            {
                this.selectPatient = enumSelectPatient.OldPatient;
                this.sheetNewPatient.RemoveSelection(this.sheetNewPatient.ActiveRowIndex, 0, 1, 1);
            }
            else
            {
                this.selectPatient = enumSelectPatient.NewPatient;
                this.sheetOldPatient.RemoveSelection(this.sheetOldPatient.ActiveRowIndex, 0, 1, 1);
            }

            this.btSelect.Focus();
            this.btSelect.Select();

            return 1;
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
            this.lblNewPatient.Text = "��ϵͳ��ȡ:0";
            this.lblOldPatient.Text = "��ϵͳ��ȡ:0";
            this.sheetNewPatient.RowCount = 0;
            this.sheetOldPatient.RowCount = 0;
            //��ȡ��ϲ�ѯ����
            if (this.getSqlWhere() == -1)
            {
                return -1;
            }

            if (this.queryOldPatient() == -1)
            {
                return -1;
            }

            if (this.queryNewPatient() == -1)
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
            //ѡ����ϵͳ����
            if (this.selectPatient == enumSelectPatient.NewPatient && this.sheetNewPatient.Rows.Count > 0)
            {
                if (this.sheetNewPatient.Rows[this.sheetNewPatient.ActiveRowIndex].Tag == null)
                {
                    this.isSelect = false;
                    return;
                }
                this.isNewPerson = true;
                this.PatientInfo = (this.sheetNewPatient.Rows[this.sheetNewPatient.ActiveRowIndex].Tag as FS.HISFC.Models.RADT.PatientInfo).Clone();
            }
            //ѡ�о�ϵͳ����
            if (this.selectPatient == enumSelectPatient.OldPatient && this.sheetOldPatient.RowCount > 0)
            {
                if (this.sheetOldPatient.Rows[this.sheetOldPatient.ActiveRowIndex].Tag == null)
                {
                    this.isSelect = false;
                    return;
                }
                this.isNewPerson = false;
                this.PatientInfo = (this.sheetOldPatient.Rows[this.sheetOldPatient.ActiveRowIndex].Tag as FS.HISFC.Models.RADT.PatientInfo).Clone();
                this.patientInfo.PID.PatientNO = this.patientInfo.PID.PatientNO.PadLeft(10, '0');
                this.patientInfo.PID.CardNO = Function.GetCardNOByPatientNO(string.Empty, this.patientInfo.PID.PatientNO);
                //����������
                //if (string.IsNullOrEmpty(this.patientInfo.PID.CardNO) == true)
                //{
                //    //���ﲡ����Ϊ�ղ��Զ�����
                //    this.patientInfo.PID.CardNO = Function.GetCardNOByPatientNO(string.Empty, this.patientInfo.PID.PatientNO);
                //}
            }

            //if (this.patientInfo != null && string.IsNullOrEmpty(this.patientInfo.ID))
            //{
            //    this.isSelect = false;
            //    return;
            //}

            frmPatientInfo frmPatientInfo = new frmPatientInfo();
            frmPatientInfo.Init(this.patientInfo);
            frmPatientInfo.ShowDialog(this);
            frmPatientInfo.BringToFront();
            if (frmPatientInfo.IsOK == "0")
            {
                return;
            }
            else if (frmPatientInfo.IsOK == "1")
            {
                this.isSelect = true;
                this.Close();

                return;
            }
            else if (frmPatientInfo.IsOK == "2")
            {
                this.isSelect = false;
                this.Close();
                return;
            }
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

        private void spOldPatientInfo_KeyDown(object sender, KeyEventArgs e)
        {
            this.selectPatient = enumSelectPatient.OldPatient;
            this.sheetNewPatient.RemoveSelection(this.sheetNewPatient.ActiveRowIndex, 0, 1, 1);
        }

        private void spOldPatientInfo_MouseDown(object sender, MouseEventArgs e)
        {
            this.selectPatient = enumSelectPatient.OldPatient;
            this.sheetNewPatient.RemoveSelection(this.sheetNewPatient.ActiveRowIndex, 0, 1, 1);
        }

        private void spNewPatientInfo_KeyDown(object sender, KeyEventArgs e)
        {
            this.selectPatient = enumSelectPatient.NewPatient;
            this.spOldPatientInfo.ActiveSheet.ActiveRowIndex = -1;
        }

        private void spNewPatientInfo_MouseDown(object sender, MouseEventArgs e)
        {
            this.selectPatient = enumSelectPatient.NewPatient;
            this.sheetOldPatient.RemoveSelection(this.sheetOldPatient.ActiveRowIndex, 0, 1, 1);
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
    }

    #region ö��

    /// <summary>
    /// ѡ��Ļ������
    /// </summary>
    public enum enumSelectPatient
    {
        /// <summary>
        /// �»���
        /// </summary>
        NewPatient,

        /// <summary>
        /// �ɻ���
        /// </summary>
        OldPatient
    }

    /// <summary>
    /// ������Ϣö����
    /// </summary>
    public enum enuInfoColumn
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
        /// ��λ����ַ
        /// </summary>
        WorkName,

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
        /// ���֤��
        /// </summary>
        IdenNo,

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