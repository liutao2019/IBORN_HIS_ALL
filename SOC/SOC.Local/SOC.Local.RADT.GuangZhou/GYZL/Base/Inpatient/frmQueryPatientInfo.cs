using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.Local.RADT.GuangZhou.GYZL.Base.Inpatient
{
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
                        sqlWhereNew += " and name like '%" + this.txtName.Text + "%'";
                        sqlWhereOld += "and name like  '%" + this.txtName.Text + "%'";
                    }
                    else
                    {
                        sqlWhereNew += " and name = '" + this.txtName.Text + "'";
                        sqlWhereOld += "and name =  '" + this.txtName.Text + "'";
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
            if (alNewPatient != null && alNewPatient.Count > 0)
            {
                this.setValues(alNewPatient, this.sheetNewPatient);
                this.sheetNewPatient.ActiveRowIndex = 0;
                this.sheetNewPatient.AddSelection(0, 0, 1, 1);
                this.selectPatient = enumSelectPatient.NewPatient;
                this.lblNewPatient.Text = "��ϵͳ��ȡ:" + alNewPatient.Count.ToString();
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

            if (alOldPatient != null && alOldPatient.Count > 0)
            {
                this.setValues(alOldPatient, this.sheetOldPatient);

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
                foreach (FS.HISFC.Models.RADT.PatientInfo PatientInfo in alPatient)
                {
                    sheetView.Rows.Add(sheetView.RowCount, 1);
                    int row = sheetView.RowCount - 1;
                    sheetView.SetValue(row, (int)enuInfoColumn.Name, PatientInfo.Name);
                    sheetView.SetValue(row, (int)enuInfoColumn.Sex, PatientInfo.Sex.ID.ToString() == "M" ? "��" : "Ů");
                    sheetView.SetValue(row, (int)enuInfoColumn.InpatientNo, PatientInfo.PID.PatientNO.TrimStart('0'));
                    sheetView.SetValue(row, (int)enuInfoColumn.DeptName, PatientInfo.PVisit.PatientLocation.Dept.Name);
                    sheetView.SetValue(row, (int)enuInfoColumn.BirthDate, PatientInfo.Birthday.ToString("yyyy-MM-dd"));
                    sheetView.SetValue(row, (int)enuInfoColumn.WorkName, PatientInfo.CompanyName);
                    sheetView.SetValue(row, (int)enuInfoColumn.BirthArea, PatientInfo.AreaCode);
                    sheetView.SetValue(row, (int)enuInfoColumn.HomeArea, PatientInfo.AddressHome);
                    sheetView.SetValue(row, (int)enuInfoColumn.InDate, PatientInfo.PVisit.InTime.ToString("yyyy-MM-dd hh:m"));
                    sheetView.SetValue(row, (int)enuInfoColumn.OutDate, PatientInfo.PVisit.OutTime.ToString("yyyy-MM-dd hh:m"));
                    sheetView.SetValue(row, (int)enuInfoColumn.IdenNo, PatientInfo.IDCard);
                    sheetView.SetValue(row, (int)enuInfoColumn.LinkMan, PatientInfo.Kin.Name);
                    sheetView.SetValue(row, (int)enuInfoColumn.InTimes, PatientInfo.InTimes);
                    string strInState = "";
                    switch (PatientInfo.PVisit.InState.ID.ToString())
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

                    sheetView.Rows[row].Tag = PatientInfo;
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

            switch(queryType)
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
            this.clear();

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
                this.sheetNewPatient.ActiveRowIndex = 0;
                this.PatientInfo = this.sheetNewPatient.Rows[this.sheetNewPatient.ActiveRowIndex].Tag as FS.HISFC.Models.RADT.PatientInfo;
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
                this.sheetNewPatient.ActiveRowIndex = 0;
                this.PatientInfo = this.sheetOldPatient.Rows[this.sheetOldPatient.ActiveRowIndex].Tag as FS.HISFC.Models.RADT.PatientInfo;
                this.patientInfo.PID.PatientNO = this.patientInfo.PID.PatientNO.PadLeft(10, '0');
                this.patientInfo.PID.CardNO = Function.GetCardNOByPatientNO(string.Empty, this.patientInfo.PID.PatientNO);
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

        #region IQueryPatientInfo ��Ա

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

       public  bool IsSelect
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

        #region IQuery ��Ա


        public bool IsFiltTemPatient
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }

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
}