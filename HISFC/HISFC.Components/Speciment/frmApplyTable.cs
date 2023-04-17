using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using FS.HISFC.Models.Base;
using System.Collections;

namespace FS.HISFC.Components.Speciment
{
    public partial class frmApplyTable : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private ApplyTableManage applyTableManage;
        private SpecTypeManage specTypeManage;
        private OrgTypeManage orgTypeManage;
        private ApplyTable applyTable;
        private ApplyTable curApplyTable;
        private Employee loginPerson;
        private ArrayList arrSpecDemand; //��������걾��Ҫ��
        private ucSpecTypeApply ucSpecTypeApply;
        private UserApply userApply;// = new UserApply();
        private UserApplyManage userApplyManage;
        private DisTypeManage disTypeManage;

        private ApplyTable currentTable;
        private string curApplyNum;

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService;
        string upLoadFilePath;
        string title;
        private int applyNum = 0;

        #region ����������֯���걾���ʹ洢
        private ArrayList alDisType = new ArrayList();
        private Hashtable hsSpecType = new Hashtable();
        private ArrayList alOrgType = new ArrayList();
        private Hashtable hsDisType = new Hashtable();
        #endregion
        //������������
        public string ApplyOrImp;

        public int ApplyNum
        {
            get
            {
                return applyNum;
            }
            set
            {
                applyNum = value;
            }
        }
        /// <summary>
        /// �Ƿ��ѯ״̬trueʱ���ܶ��κ����ݲ��� 
        /// </summary>
        private string isState = "Operation";
        public string IsState
        {
            get
            {
                return this.isState;
            }
            set
            {
                this.isState = value;
                try
                {
                    if ((this.IsState == "Query") || (this.IsState == "Confirm"))
                    {
                        this.SetEnabledFlase();
                    }
                    if (this.IsState == "Confirm")
                    {
                        tabControl1.SelectedIndex = 1;
                    }
                }
                catch
                {
                }
            }
        }

        /*modify by ��� 2011-07-28*/
        /// <summary>
        /// �������
        /// </summary>
        private bool isDeptValid = true;
        [Category("�ؼ�����"), Description("false:�Ǳ걾�� true:�걾��")]
        public bool IsDeptValid
        {
            set
            {
                this.isDeptValid = value;
            }
            get
            {
                return this.isDeptValid;
            }
        }


        /// <summary>
        /// ���캯��
        /// </summary>
        public frmApplyTable()
        {
            InitializeComponent();
            #region ��'������Ϣ'���TabControl1���Ƴ�
            if (this.IsState == "Operation")
            {
                this.tabControl1.TabPages.RemoveAt(1);
            }
            else
            {
                this.tabControl1.Controls.Add(this.tabPage2);
            }
            #endregion
            applyTableManage = new ApplyTableManage();
            applyTable = new ApplyTable();
            arrSpecDemand = new ArrayList();
            ucSpecTypeApply = new ucSpecTypeApply();
            specTypeManage = new SpecTypeManage();
            orgTypeManage = new OrgTypeManage();
            userApply = new UserApply();
            userApplyManage = new UserApplyManage();
            toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
            upLoadFilePath = "";
            title = "�걾����";
            ApplyOrImp = "";
        }

        /// <summary>
        /// ��ҳ���ȡ��������Ϣ
        /// </summary>
        private void ApplyTableSet()
        {
            if (loginPerson != null)
                applyTable.ApplyUserId = loginPerson.ID;
            //������Ҳ�����޸İ�
            if (this.cmbAppOper.Tag != null)
            {
                if (!string.IsNullOrEmpty(this.cmbAppOper.Tag.ToString()))
                {
                    if (this.cmbAppOper.Tag.ToString() != loginPerson.ID)
                    {
                        applyTable.ApplyUserId = this.cmbAppOper.Tag.ToString();
                    }
                }
            }
            if (this.cmbOperName.Tag != null)
            {
                if (!string.IsNullOrEmpty(this.cmbOperName.Tag.ToString()))
                {
                    //if (this.cmbAppOper.Tag.ToString() != loginPerson.ID)
                    //{
                    //    applyTable.ApplyUserId = this.cmbAppOper.Tag.ToString();
                    //}
                    applyTable.ImpName = this.cmbOperName.Tag.ToString();
                }
            }
            //applyTable.ApplyUserName = txtApplyName.Text.TrimEnd().TrimStart();
            applyTable.ApplyUserName = this.cmbAppOper.Text.ToString().Trim();
            if (cmbApplyDept.Tag != null)
                applyTable.DeptId = cmbApplyDept.Tag.ToString();
            if (cmbApplyDept.Text != "")
                applyTable.DeptName = cmbApplyDept.Text;
            applyTable.SubjectName = txtSubName.Text;
            applyTable.ResPlan = txtResearchPlan.Text;
            applyTable.ResPlanAtt = upLoadFilePath;
            applyTable.FundStartTime = dtpFundStartTime.Value;
            applyTable.FundEndTime = dtpFundEndTime.Value;
            applyTable.FundName = txtFundName.Text;
            applyTable.FundId = txtFundID.Text;
            applyTable.ApplyEmail = txtApplyEmail.Text;
            applyTable.ApplyTel = txtApplyTel.Text;
            applyTable.OtherDemand = txtOtherDemand.Text;
            applyTable.ImpProcess = "U";
            applyTable.ImpEmail = txtOperEmail.Text;
            applyTable.ImpTel = txtOperTel.Text;
            //applyTable.ImpName = txtOperName.Text;
            applyTable.ImpName = cmbOperName.Text;
            //ִ��ҽ��������ID
            applyTable.ImpDocId = cmbOutputOperName.Tag.ToString();
            applyTable.OutPutOperDoc = cmbOutputOperName.Name;
            //��������걾��Ҫ��
            SpecDemandSet();
        }

        /// <summary>
        /// ��ȡ�걾����Ҫ���б�
        /// </summary>
        private void SpecDemandSet()
        {
            arrSpecDemand.Clear();
            applyTable.DiseaseType = "";
            applyTable.SpecDetAmout = "";
            applyTable.SpecType = "";
            applyTable.SpecIsCancer = "";
            applyTable.SpecAmout = 0;
            //foreach (Control c in flpSpecDemand.Controls)
            //{
            //    ucSpecTypeApply apply = c as ucSpecTypeApply;
            //    arrSpecDemand.Add(apply.SpecDemand);
            //}
            //int arrDemandIndex = 0;
            //foreach (ApplySpecDemand asd in arrSpecDemand)
            //{
            //    applyTable.DiseaseType += asd.disType;
            //    applyTable.SpecType += asd.specType;
            //    applyTable.SpecDetAmout += asd.currentCount.ToString();
            //    applyTable.SpecIsCancer += asd.tumorType;
            //    applyTable.SpecAmout += asd.currentCount;
            //    if (arrDemandIndex < arrSpecDemand.Count - 1)
            //    {
            //        applyTable.DiseaseType += ",";
            //        applyTable.SpecType += ",";
            //        applyTable.SpecDetAmout += ",";
            //        applyTable.SpecIsCancer += ",";
            //    }
            //    arrDemandIndex++;
            //}
            if (hsDisType.Count > 0)
            {
                int i = 0;
                foreach (string s in hsDisType.Keys)
                {
                    applyTable.DiseaseType += s;
                    if (i < hsDisType.Count - 1)
                    {
                        applyTable.DiseaseType += ",";
                    }
                    i++;
                }
            }
            hsSpecType.Clear();
            applyTable.SpecDetAmout = Convert.ToInt32(nudCount.Value).ToString();

            #region �����Ŀ�ѡ��
            if (cbRnaCancer.Checked)
            {
                hsSpecType.Add("RC", true);
            }
            if (cbDnaCancer.Checked)
            {
                hsSpecType.Add("DC", true);
            }
            if (cbDnaBesidCancer.Checked)
            {
                hsSpecType.Add("DBC", true);
            }
            if (cbRnaBesidCancer.Checked)
            {
                hsSpecType.Add("RBC", true);
            }
            if (cbSliceCancer.Checked)
            {
                hsSpecType.Add("SC", true);
            }
            if (cbSliceNormal.Checked)
            {
                hsSpecType.Add("SN", true);
            }
            if (cbSerumTumer.Checked)
            {
                hsSpecType.Add("ST", true);
            }
            if (cbSerumControl.Checked)
            {
                hsSpecType.Add("SL", true);
            }
            if (cbPlasmaTumors.Checked)
            {
                hsSpecType.Add("PT", true);
            }
            if (cbPlasmaControl.Checked)
            {
                hsSpecType.Add("PC", true);
            }
            if (cbLympTumor.Checked)
            {
                hsSpecType.Add("LT", true);
            }
            if (cbLympControl.Checked)
            {
                hsSpecType.Add("LC", true);
            }
            #endregion

            if (hsSpecType.Count > 0)
            {
                int j = 0;
                foreach (string s in hsSpecType.Keys)
                {
                    applyTable.SpecType += s;
                    if (j < hsSpecType.Count - 1)
                    {
                        applyTable.SpecType += ",";
                    }
                    j++;
                }
            }
        }

        /// <summary>
        /// ����������д����Ϣ���ܸ�д
        /// </summary>
        private void DiabledSomeControls()
        {
            //txtApplyEmail.Enabled = false;
            //txtApplyName.Enabled = false;
            //txtApplyTel.Enabled = false;
            //txtFundID.Enabled = false;
            //txtFundName.Enabled = false;
            //txtOtherDemand.Enabled = false;
            //txtPlanAtt.Enabled = false;
            //txtResearchPlan.Enabled = false;
            //cmbApplyDept.Enabled = false;
            //dtpApplyDate.Enabled = false;
            //dtpFundEndTime.Enabled = false;
            //dtpFundStartTime.Enabled = false;
            //txtSubName.Enabled = false;
            //btnBrowse.Enabled = false;
            //btnUpload.Text = "���ظ���";
            //btnSave.Visible = true;
            //rbtAgree.Visible = true;
            //rbtDisAgree.Visible = true;
        }

        /// <summary>
        /// ���������ID��ʼ�������
        /// </summary>
        private void InitApplyTable()
        {
            lsSpecOut.View = View.List;
            //DiabledSomeControls(); 
            curApplyTable = new ApplyTable();
            curApplyTable = applyTableManage.QueryApplyByID(ApplyNum.ToString());
            lblNum.Text = curApplyTable.ApplyId.ToString();
            this.cmbAppOper.Tag = curApplyTable.ApplyUserId;
            this.cmbAppOper.Text = curApplyTable.ApplyUserName;
            txtApplyName.Text = curApplyTable.ApplyUserName;
            if (string.IsNullOrEmpty(txtApplyName.Text))
            {
                txtApplyName.Text = this.cmbAppOper.Text;
            }
            cmbApplyDept.Text = curApplyTable.DeptName;
            txtSubName.Text = curApplyTable.SubjectName;
            txtResearchPlan.Text = curApplyTable.ResPlan;
            string fileName = curApplyTable.ResPlanAtt;
            txtPlanAtt.Text = fileName.Substring(fileName.LastIndexOf("/") + 1);
            dtpFundStartTime.Value = curApplyTable.FundStartTime;
            dtpFundEndTime.Value = curApplyTable.FundEndTime;
            txtFundName.Text = curApplyTable.FundName;
            txtFundID.Text = curApplyTable.FundId;
            txtApplyEmail.Text = curApplyTable.ApplyEmail;
            txtApplyTel.Text = curApplyTable.ApplyTel;
            txtOtherDemand.Text = curApplyTable.OtherDemand;
            if (curApplyTable.ImpResult == "0")
                rbtDisAgree.Checked = true;
            if (curApplyTable.ImpResult == "1")
                rbtAgree.Checked = true;
            string[] disTypeDemand = curApplyTable.DiseaseType.Split(',');
            string[] specTypeDemand = curApplyTable.SpecType.Split(',');
            string[] demandCount = curApplyTable.SpecDetAmout.Split(',');
            string[] isCancer = curApplyTable.SpecIsCancer.Split(',');
            int index = 0;
            this.tbDisTypeAll.Text = "";
            hsDisType.Clear();
            #region �鷳�ĸ�ֵ
            foreach (string s in disTypeDemand)
            {
                foreach (DiseaseType dis in alDisType)
                {
                    if (s == dis.DisTypeID.ToString())
                    {
                        if (!hsDisType.Contains(s))
                        {
                            hsDisType.Add(s, dis.DiseaseName);
                        }
                        this.tbDisTypeAll.Text += dis.DiseaseName + ",";
                    }
                }
            }
            #region ȫ����false
            cbIceOrg.Checked = false;
            cbPwOrg.Checked = false;
            cbSerum.Checked = false;
            cbPlasma.Checked = false;
            cbLymp.Checked = false;
            #endregion
            foreach (string sp in specTypeDemand)
            {
                switch (sp)
                {
                    case "RC":
                        cbRnaCancer.Checked = true;
                        break;
                    case "DC":
                        cbDnaCancer.Checked = true;
                        break;
                    case "DBC":
                        cbDnaBesidCancer.Checked = true;
                        break;
                    case "RBC":
                        cbRnaBesidCancer.Checked = true;
                        break;
                    case "SC":
                        cbSliceCancer.Checked = true;
                        break;
                    case "SN":
                        cbSliceNormal.Checked = true;
                        break;
                    case "ST":
                        cbSerumTumer.Checked = true;
                        break;
                    case "SL":
                        cbSerumControl.Checked = true;
                        break;
                    case "PT":
                        cbPlasmaTumors.Checked = true;
                        break;
                    case "PC":
                        cbPlasmaControl.Checked = true;
                        break;
                    case "LT":
                        cbLympTumor.Checked = true;
                        break;
                    case "LC":
                        cbLympControl.Checked = true;
                        break;
                    default:
                        break;
                }

            }
            #endregion
            this.tbDisTypeAll.Text = this.tbDisTypeAll.Text.TrimEnd(',');
            if (!string.IsNullOrEmpty(curApplyTable.SpecDetAmout))
            {
                this.nudCount.Value = Convert.ToDecimal(curApplyTable.SpecDetAmout);
            }
            #region ����
            /*
            ucSpecTypeApply[] ucSpecInit = new ucSpecTypeApply[disTypeDemand.Length];
            foreach (string s in specTypeDemand)
            {
                if (string.IsNullOrEmpty(s))
                {
                    continue;
                }
                int orgTypeId = specTypeManage.GetSpecTypeById(s).OrgType.OrgTypeID;
                ucSpecInit[index] = new ucSpecTypeApply();
                flpSpecDemand.Controls.Add(ucSpecInit[index]);
                foreach (Control c in ucSpecInit[index].Controls)
                {
                    switch (c.Name)
                    {
                        case "cmbOrgType":
                            ComboBox cmbOrg = c as ComboBox;
                            cmbOrg.SelectedValue = orgTypeId;
                            break;                            
                        case "cmbDisType":
                            ComboBox cmbDisType = c as ComboBox;
                            cmbDisType.SelectedValue = Convert.ToInt32(disTypeDemand[index]);
                            break;
                        case "cmbSpecType":
                            ComboBox cmb = c as ComboBox;
                            cmb.SelectedValue = Convert.ToInt32(s);
                            break;
                        case "nudCount":
                            NumericUpDown numCount = c as NumericUpDown;
                            numCount.Value = Convert.ToInt32(demandCount[index]);
                            break;
                        case "btnAdd":
                            Button btn = c as Button;
                            btn.Click += BtnAdd_Click;
                            break;
                        case "btnDel":
                            if (index==0)
                                c.Enabled = false;
                            //c.Enabled = false;
                            break;
                        case "flpTumorType":
                            #region ��ʼ����������
                            FlowLayoutPanel flp = c as FlowLayoutPanel;
                            //foreach (Control f in flp.Controls)
                            //    f.Enabled = false;
                            Constant.TumorType TumorType = (Constant.TumorType)(Convert.ToInt32(isCancer[index]));
                            switch (TumorType)
                            {
                                //case Constant.TumorType.����:
                                //    foreach (Control f in flp.Controls)
                                //    {
                                //        RadioButton tmp = f as RadioButton;
                                //        if (tmp.Name == "rbtIsCancer")
                                //            tmp.Checked = true;
                                //    }
                                //    break;
                                case Constant.TumorType.����:
                                    foreach (Control f in flp.Controls)
                                    {
                                        RadioButton tmp = f as RadioButton;
                                        if (tmp.Name == "rbtTumor")
                                            tmp.Checked = true;
                                    }
                                    break;
                                //case Constant.TumorType.����:
                                //    foreach (Control f in flp.Controls)
                                //    {
                                //        RadioButton tmp = f as RadioButton;
                                //        if (tmp.Name == "rbtNoCancer")
                                //            tmp.Checked = true;
                                //    }
                                //    break;
                                case Constant.TumorType.����:
                                    foreach (Control f in flp.Controls)
                                    {
                                        RadioButton tmp = f as RadioButton;
                                        if (tmp.Name == "rbtSub")
                                            tmp.Checked = true;
                                    }
                                    break;
                                case Constant.TumorType.����:
                                    foreach (Control f in flp.Controls)
                                    {
                                        RadioButton tmp = f as RadioButton;
                                        if (tmp.Name == "rbtSide")
                                            tmp.Checked = true;
                                    }
                                    break;
                                case Constant.TumorType.����:
                                    foreach (Control f in flp.Controls)
                                    {
                                        RadioButton tmp = f as RadioButton;
                                        if (tmp.Name == "rbtNormal")
                                            tmp.Checked = true;
                                    }
                                    break;
                                case Constant.TumorType.��˨:
                                    foreach (Control f in flp.Controls)
                                    {
                                        RadioButton tmp = f as RadioButton;
                                        if (tmp.Name == "rbtShuan")
                                            tmp.Checked = true;
                                    }
                                    break;
                                case Constant.TumorType.�ܰͽ�:
                                    foreach (Control f in flp.Controls)
                                    {
                                        RadioButton tmp = f as RadioButton;
                                        if (tmp.Name == "rbtLinBa")
                                            tmp.Checked = true;
                                    }
                                    break;
                                //case Constant.TumorType.ת����:
                                //    foreach (Control f in flp.Controls)
                                //    {
                                //        RadioButton tmp = f as RadioButton;
                                //        if (tmp.Name == "rbtTransfer")
                                //            tmp.Checked = true;
                                //    }
                                //    break;
                                default:
                                    break;
                            }
                            #endregion
                            break;
                        default:
                            break;

                    }
                }
                
                index++;

            }
            */
            #endregion
            //foreach (ucSpecTypeApply u in ucSpecInit)
            //{
            //    flpSpecDemand.Controls.Add(u);
            //}

            if (curApplyTable.ImpProcess == "O")
            {
                //ÿ��Ҫ���
                lsSpecOut.Clear();
                SpecOutManage outMgr = new SpecOutManage();
                DataSet ds = new DataSet();
                string sql = "select SUBSPECBARCODE from spec_out where RELATEID =" + curApplyTable.ApplyId.ToString();
                outMgr.ExecQuery(sql, ref ds);

                if (ds == null || ds.Tables.Count <= 0)
                {
                    MessageBox.Show("��ȡ����걾ʧ��");
                }
                else
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        try
                        {
                            lsSpecOut.Items.Add(r[0].ToString());
                        }
                        catch
                        {
                            continue;
                        }

                    }
                    lblCnt.Text = "�ܹ���" + lsSpecOut.Items.Count.ToString() + "��";
                }
            }
            else if (curApplyTable.ImpProcess == "U")
            {
                //ÿ��Ҫ���
                lsSpecOut.Clear();
                SpecOutManage outMgr = new SpecOutManage();
                DataSet ds = new DataSet();
                string sql = "select SUBSPECBARCODE from SPEC_APPLY_OUT where RELATEID =" + curApplyTable.ApplyId.ToString() + " and OPER <> 'Del'";
                outMgr.ExecQuery(sql, ref ds);

                if (ds == null || ds.Tables.Count <= 0)
                {
                    MessageBox.Show("��ȡ����걾ʧ��");
                }
                else
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        try
                        {
                            lsSpecOut.Items.Add(r[0].ToString());
                        }
                        catch
                        {
                            continue;
                        }

                    }
                    lblCnt.Text = "�ܹ���" + lsSpecOut.Items.Count.ToString() + "��";
                }
            }
            #region ������Ϣ
            //txtOperName.Text = curApplyTable.ImpName;
            cmbOperName.Text = curApplyTable.ImpName;
            txtOperEmail.Text = curApplyTable.ImpEmail;
            txtAcceptConfirm.Text = curApplyTable.AcceptConfirm;
            dtpAcceptDate.Value = curApplyTable.AcceptConfrimDate;
            txtDeptComment.Text = curApplyTable.DeptFromComm;
            dtpDeptComment.Value = curApplyTable.DeptFromDate;
            rbtAgree.Checked = curApplyTable.ImpResult == "1" ? true : false;
            rbtDisAgree.Checked = curApplyTable.ImpResult == "0" ? true : false;
            txtOperTel.Text = curApplyTable.ImpTel;
            //txtOutputOperName.Text = curApplyTable.OutPutOperDoc;
            cmbOutputOperName.Text = curApplyTable.OutPutOperDoc;
            txtOutput.Text = curApplyTable.OutPutResult;
            dtpOutputDate.Value = curApplyTable.OutPutTime;
            dtpSpecAdmDate.Value = curApplyTable.SepcAdmDate;
            txtSpecAdmComment.Text = curApplyTable.SpecAdmComment;
            #endregion
        }

        /// <summary>
        /// ��ȡ��¼�˵���Ϣ
        /// </summary>
        private void InitLoginPerson()
        {
            loginPerson = null;
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            FS.HISFC.BizLogic.Manager.DepartmentStatManager manager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            ArrayList alDepts;
            alDepts = manager.LoadAll();
            this.cmbApplyDept.AddItems(alDepts);

            if (loginPerson != null)
            {
                //alDepts = manager.GetMultiDept(loginPerson.ID);
                //this.cmbApplyDept.AddItems(alDepts);
                this.cmbApplyDept.Tag = loginPerson.Dept.ID;
                //#region ���ݵ�¼�˵Ŀ���ȷ��������Ϣ�Ƿ�ɼ����걾��ʱ�ɼ� �Ǳ걾�ⲻ�ɼ�
                //if (loginPerson.Dept == "")
                //{

                //}
                //else
                //{

                //}
                //#endregion
            }
            //else
            //{
            //    alDepts = manager.LoadAll();
            //    this.cmbApplyDept.AddItems(alDepts);
            //}
            FS.HISFC.BizProcess.Integrate.Manager dpMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            ArrayList alEmpl = new ArrayList();
            alEmpl = dpMgr.QueryEmployeeAll();
            if ((alEmpl != null) && (alEmpl.Count > 0))
            {
            }
            else
            {
                alEmpl.Add(loginPerson);
            }
            this.cmbAppOper.AddItems(alEmpl);
            this.cmbAppOper.Tag = loginPerson.ID;
            this.cmbAppOper.Text = loginPerson.Name;
            this.cmbOperName.AddItems(alEmpl);
            this.cmbOutputOperName.AddItems(alEmpl);
        }

        private void RecentlyApply()
        {
            #region ����
            /*
            string sql = " Select SPEC_APPLICATIONTABLE.APPLICATIONID ����ID, SUBJECTNAME ��������,FUNDNAME ��������,FUNDSTARTTIME ����ʼʱ��,FUNDENDTIME �������ʱ��,\n" +
                         " Case IMPPROCESS when 'O' then '��������' when 'U' then '������' end ��������,\n" +
                         " case IMPRESLUT when '1' then 'ͬ��' when '0' then '�ܾ�' end �������\n" +
                         " FROM SPEC_USERAPPLICATION RIGHT JOIN SPEC_APPLICATIONTABLE ON SPEC_USERAPPLICATION.APPLICATIONID = SPEC_APPLICATIONTABLE.APPLICATIONID\n" +
                         " WHERE SPEC_APPLICATIONTABLE.APPLICATIONID>0 and IMPPROCESS='U' ";
            //if (rbtMonth.Checked)
            //{
            //    sql += " AND APPLYTIME >= TIMESTAMP('" + DateTime.Now.AddMonths(-1).ToString() + "')";
            //}
            //if (rbtThreeMonths.Checked)
            //{
            //    sql += " AND APPLYTIME >= TIMESTAMP('" + DateTime.Now.AddMonths(-3).ToString() + "')";
            //}
            if (rbtAll.Checked)
            {
                sql += "";
            }
            DataSet dsTmp = new DataSet();
            applyTableManage.ExecQuery(sql, ref dsTmp);
            */
            #endregion
            /* ���� 2011-08-08 */
            #region ��ѡ���Ѿ�������ѯʱ���б����Ϊ�����������뵥�� ����Ϊ��δ�������뵥��
            if (chkImped.Checked)
            {
                groupBox3.Text = "���������뵥";
            }
            else
            {
                groupBox3.Text = "δ�������뵥";
            }
            #endregion
            /*modify by ��� 2011-07-28*/
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;


            //ERROR [42601] [IBM][DB2/NT] SQL0104N  �� "ME���������ʱ��FROM" �����ҵ��쳣��� "SPEC_APPLICATIONTABLE"��Ԥ�ڱ�ǿ��ܰ�����"FROM"��  SQLSTATE=42601
            string strQuery = "SELECT APPLICATIONID ����ID,APPLYUSERNAME ����������,DEPARTMENTNAME ��������,APPLYTIME ����ʱ��,SUBJECTNAME ��Ŀ����,FUNDNAME ��������," +
                              " FUNDSTARTTIME ����ʼʱ��,FUNDENDTIME���������ʱ��,(CASE IMPRESLUT WHEN '0' THEN '�ܾ�' WHEN '1' THEN 'ͬ��'��WHEN '' THEN '������' END) ������� ";
            strQuery += " FROM SPEC_APPLICATIONTABLE WHERE APPLICATIONID>0 ";

            /*modify by ��� 2011-07-28*/
            if (IsDeptValid == false)
            {
                strQuery += loginPerson.ID.ToString() == "" ? "" : " AND (APPLYUSERID like '%" + loginPerson.ID.ToString() + "%'";
                strQuery += loginPerson.Name.ToString() == "" ? "" : " OR APPLYUSERNAME LIKE '%" + loginPerson.Name.ToString() + "%'";
                strQuery += loginPerson.Name.ToString() == "" ? "" : " AND IMPLEMENTNAME LIKE '%" + cmbOperName.Text.Trim() + "%')";
            }

            strQuery += chkImped.Checked ? " AND IMPPROCESS like 'O%'" : " AND IMPPROCESS='U'";
            DataSet dsTmp = new DataSet();
            applyTableManage.ExecQuery(strQuery, ref dsTmp);
            //DataTable dTab = new DataTable();
            //dTab = dsTmp.Tables[0];
            if (dsTmp.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = dsTmp.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["����ʼʱ��"] != null)
                    {
                        dt.Rows[i]["����ʼʱ��"] = Convert.ToDateTime(dt.Rows[i]["����ʼʱ��"].ToString()).ToString("yyyy-MM-dd");
                    }
                    if (dt.Rows[i]["�������ʱ��"] != null)
                    {
                        dt.Rows[i]["�������ʱ��"] = Convert.ToDateTime(dt.Rows[i]["�������ʱ��"].ToString()).ToString("yyyy-MM-dd");
                    }
                }
                //neuSpread1_Sheet1.DataSource = null;
                dgvResult.DataSource = dt;
            }
            if (dgvResult.RowCount == 0)
            {
                panel2.Visible = false;
            }
            else if (this.IsState != "Query")
            {
                panel2.Visible = true;
            }
            //if (neuSpread1_Sheet1.RowCount == 0)
            //{
            //    panel2.Visible = false;
            //}
        }

        private void AddNew()
        {
            int sequence = 0;
            applyTable = new ApplyTable();
            sequence = applyTableManage.GetMaxApplyId();// applyTableManage.GetNextSequence(ref sequence);
            lblNum.Text = sequence.ToString();
            applyTable.ApplyId = sequence; //Convert.ToInt32(sequence);
            applyNum = 0;
            ApplyOrImp = "";
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("ȡ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);
            this.toolBarService.AddToolButton("��ӡ���뵥", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ�嵥, true, false, null);
            this.toolBarService.AddToolButton("��д", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S�ֶ�¼��, true, false, null);
            //this.toolBarService.AddToolButton("�½�", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.A�½�, true, false, null);

            return this.toolBarService;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.RecentlyApply();
            return base.OnQuery(sender, neuObject);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                // case "�޸�":

                //break;
                case "��ӡ���뵥":
                    if (applyNum < 1)
                        return;
                    FS.HISFC.Components.Speciment.Print.frmPrintApplyTable frmPrint = new FS.HISFC.Components.Speciment.Print.frmPrintApplyTable();
                    frmPrint.PrintApplication(applyNum);
                    frmPrint.Dispose();
                    break;
                case "ȡ��":
                    Cancel();
                    break;
                case "��д":
                    AddNew();
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        private void rbt_Changed(object sender, EventArgs e)
        {
            RecentlyApply();
        }

        /// <summary>
        /// ��ӱ걾����Ҫ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ucSpecTypeApply = new ucSpecTypeApply();
            foreach (Control c in ucSpecTypeApply.Controls)
            {
                if (c.Name == "btnAdd")
                {
                    Button btn = c as Button;
                    btn.Enabled = false;
                }
            }
            //flpSpecDemand.Controls.Add(ucSpecTypeApply);
        }

        private void frmApplyTable_Load(object sender, EventArgs e)
        {
            lblCnt.Text = "";
            RecentlyApply();
            try
            {
                InitLoginPerson();
                if (applyNum == 0)
                {
                    txtApplyName.Text = loginPerson.Name;
                    //flpSpecDemand.Controls.Add(ucSpecTypeApply);
                    foreach (Control c in ucSpecTypeApply.Controls)
                    {
                        if (c.Name == "btnDel")
                        {
                            Button btn = c as Button;
                            btn.Enabled = false;
                        }
                        if (c.Name == "btnAdd")
                        {
                            Button btn = c as Button;
                            btn.Click += BtnAdd_Click;
                        }
                    }
                    btnSave.Enabled = false;
                }
                else
                {
                    ApplyOrImp = "Imp";
                    InitApplyTable();
                    btnSubmit.Enabled = false;
                }
                this.DisTypeBinding();
            }
            catch
            {

            }
        }

        /// <summary>
        /// �󶨲�������
        /// </summary>
        private void DisTypeBinding()
        {
            disTypeManage = new DisTypeManage();
            Dictionary<int, string> dicDisType = disTypeManage.GetAllDisType();
            if (dicDisType.Count > 0)
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = dicDisType;
                cmbDisType.DisplayMember = "Value";
                cmbDisType.ValueMember = "Key";
                cmbDisType.DataSource = bs;
            }
            alDisType = disTypeManage.QueryAllDisType();

        }

        /// <summary>
        /// ���ÿؼ���Ч��������
        /// </summary>
        private void SetEnabledFlase()
        {
            this.btnSubmit.Enabled = false;
            panel2.Visible = false;
            if (this.IsState == "Query")
            {
                this.tabControl1.Controls.Add(this.tabPage2);
                this.btnSave.Enabled = false;
                this.tabControl1.TabPages.RemoveAt(1);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //    DialogResult diaRes = MessageBox.Show("����֮�󽫲��ܸ��ģ�", title, MessageBoxButtons.OKCancel);
            //    if (DialogResult.Cancel == diaRes)
            //    {
            //        return -1;
            //    }
            if (ApplyOrImp != "")
            {
                //curApplyTable.ImpDocId = loginPerson.ID;
                curApplyTable.ImpDocId = cmbOutputOperName.Tag.ToString();
                curApplyTable.ImpEmail = txtOperEmail.Text;
                curApplyTable.AcceptConfirm = txtAcceptConfirm.Text;
                curApplyTable.AcceptConfrimDate = dtpAcceptDate.Value;
                curApplyTable.DeptFromComm = txtDeptComment.Text;
                curApplyTable.DeptFromDate = dtpDeptComment.Value;
                curApplyTable.ImpProcess = "O";
                curApplyTable.ImpResult = rbtAgree.Checked ? "1" : "0";
                curApplyTable.ImpTel = txtOperTel.Text;
                //curApplyTable.OutPutOperDoc = txtOutputOperName.Text;
                curApplyTable.OutPutOperDoc = cmbOutputOperName.Text;
                curApplyTable.OutPutResult = txtOutput.Text;
                curApplyTable.OutPutTime = dtpOutputDate.Value;
                curApplyTable.SepcAdmDate = dtpSpecAdmDate.Value;
                curApplyTable.SpecAdmComment = txtSpecAdmComment.Text;
                //curApplyTable.ImpName = txtOperName.Text;
                curApplyTable.ImpName = cmbOperName.Text.ToString();
                if (applyTableManage.UpdateApplyTable(curApplyTable) <= 0)
                {
                    MessageBox.Show("����ʧ��!", title);
                }
                else
                {
                    MessageBox.Show("����ɹ�!", title);
                }
            }

        }

        private void neuSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            int rowIndex = neuSpread1_Sheet1.ActiveRowIndex;
            lsSpecOut.Clear();
            if (rowIndex >= 0 && neuSpread1_Sheet1.RowCount > 0)
            {
                btnSubmit.Enabled = true;
                btnSave.Enabled = false;
                //flpSpecDemand.Controls.Clear();
                applyNum = Convert.ToInt32(neuSpread1_Sheet1.Cells[rowIndex, 0].Text);
                //ApplyOrImp = "Imp";
                InitApplyTable();
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            ApplyTableSet();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {
                userApplyManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                applyTableManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                int result;
                //���Ҳ��������µ�����ID
                //if (applyNum <= 0)
                //{
                //    applyTable.ApplyId = applyTableManage.GetMaxApplyId();
                //    if (applyTable.ApplyId <= 0)
                //    {
                //        FS.FrameWork.Management.Public.FrameWork.Management.PublicTrans.RollBack();
                //        MessageBox.Show("�����Ų���Ϊ��!", title);
                //        return;
                //    }
                //    if (applyTable.ImpProcess == "O")
                //    {
                //        FS.FrameWork.Management.Public.FrameWork.Management.PublicTrans.RollBack();
                //        MessageBox.Show("�������ļ��������޸�!", title);
                //        return;
                //    }
                //    result = applyTableManage.InsertApplyTable(applyTable);
                //    userApply.ApplyId = applyTable.ApplyId;
                //    userApply.UserId = loginPerson.ID;
                //    userApply.Schedule = "��д���뵥";
                //    string sequence = "";
                //    userApplyManage.GetNextSequence(ref sequence);
                //    userApply.UserAppId = Convert.ToInt32(sequence);
                //    result = userApplyManage.InsertUserApply(userApply);
                //    if (result == -1)
                //    {
                //        MessageBox.Show("����ʧ��!", title);
                //        FS.FrameWork.Management.Public.FrameWork.Management.PublicTrans.RollBack();
                //        return;
                //    }
                //}
                if (applyNum > 0)
                {
                    if (curApplyTable.ImpResult == "0" || curApplyTable.ImpResult == "1")
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���뵥�Ѿ������������޸�!", title);
                        return;
                    }

                    applyTable.ApplyId = curApplyTable.ApplyId;
                    result = applyTableManage.UpdateApplyTable(applyTable);
                    if (userApplyManage.QueryUserApply(applyNum.ToString(), "��д���뵥") <= 0)
                    {
                        userApply.ApplyId = applyTable.ApplyId;
                        userApply.UserId = loginPerson.ID;
                        userApply.Schedule = "��д���뵥";
                        userApply.ScheduleId = "Q2";
                        userApply.CurDate = System.DateTime.Now;
                        userApply.OperId = loginPerson.ID;
                        userApply.OperName = loginPerson.Name;
                        string sequence = "";
                        userApplyManage.GetNextSequence(ref sequence);
                        userApply.UserAppId = Convert.ToInt32(sequence);
                        result = userApplyManage.InsertUserApply(userApply);
                        if (result == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����ʧ��!", title);
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("��ѡ����������뵥�Ž����޸�!", title);
                    return;
                }
                if (result == -1)
                {
                    MessageBox.Show("����ʧ��!", title);
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return;
                }

                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("���뵥���ύ����ȴ�����!", title);
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
            }
            RecentlyApply();
        }

        private void Cancel()
        {
            if (applyNum < 1)
            {
                return;
            }
            else
            {
                try
                {
                    ApplyTableSet();
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    userApplyManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    applyTableManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    int result;
                    //applyTableManage.ExecNoQuery("update SPEC_APPLICATIONTABLE set IMPPROCESS = 'C' where APPLICATIONID = " + applyNum.ToString());
                    if (curApplyTable.ImpResult == "0" || curApplyTable.ImpResult == "1" || curApplyTable.ImpResult == "2")
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���뵥�Ѿ������������޸�!", title);
                        return;
                    }

                    applyTable.ApplyId = applyNum;
                    applyTable.ImpProcess = "O";
                    applyTable.ImpResult = "2";
                    result = applyTableManage.UpdateApplyTable(applyTable);
                    if (result == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�������뵥ʧ��!", title);
                        return;
                    }
                    userApply.ApplyId = applyTable.ApplyId;
                    userApply.UserId = loginPerson.ID;
                    userApply.Schedule = "����";
                    userApply.ScheduleId = "Del";
                    userApply.CurDate = System.DateTime.Now;
                    userApply.OperId = loginPerson.ID;
                    userApply.OperName = loginPerson.Name;
                    string sequence = "";
                    userApplyManage.GetNextSequence(ref sequence);
                    userApply.UserAppId = Convert.ToInt32(sequence);
                    result = userApplyManage.InsertUserApply(userApply);
                    if (result == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�������ʧ��!", title);
                        return;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show("�ɹ��������뵥!", title);
                }
                catch
                {
                    MessageBox.Show("�쳣����!", title);
                    return;
                }
            }
        }

        private void dgvResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvResult.SelectedCells.Count > 0)
            {
                string type = dgvResult.SelectedCells[0].GetType().ToString();
                string text = dgvResult.SelectedCells[0].Value.ToString();
                //������һ��������ڵ�2����
                curApplyNum = dgvResult.CurrentRow.Cells[2].Value.ToString();
                currentTable = applyTableManage.QueryApplyByID(curApplyNum);
                //���뵥��׼
                List<string> specList = new List<string>();
                List<string> returnableList = new List<string>();
                if (currentTable.ImpResult == "1")
                {
                    string[] outSpecList = currentTable.SpecList.Split(',');
                    string[] backList = currentTable.IsImmdBackList.Split(',');
                    int count = backList.Length > outSpecList.Length ? outSpecList.Length : backList.Length;
                    for (int i = 0; i < count; i++)
                    {
                        if (outSpecList[i].Trim() != "")
                        {
                            specList.Add(outSpecList[i]);
                            //�ɻ��ı걾
                            if (backList[i].Trim() == "1")
                            {
                                returnableList.Add(outSpecList[i]);
                            }
                        }
                    }
                }
                if (type.Equals("System.Windows.Forms.DataGridViewButtonCell") && text == "�������")
                {
                    frmApplySchedule frmSdl = new frmApplySchedule();
                    //Size size = new Size();
                    //size.Height = 700;
                    //size.Width = 1000;
                    //frmSdl.Size = size;
                    frmSdl.ApplyId = curApplyNum;
                    FS.FrameWork.WinForms.Classes.Function.PopShowControl(frmSdl, FormBorderStyle.FixedSingle, FormWindowState.Normal);

                }
                if (type.Equals("System.Windows.Forms.DataGridViewButtonCell") && text.Contains("�鿴"))
                {
                    tabControl1.SelectTab("tpSpec");
                    tpSpec.Controls.Clear();
                    //currentTable = applyTableManage.QueryApplyByID(curApplyNum);
                    ucOutSpecList ucOutList = new ucOutSpecList(loginPerson);
                    ucOutList.Dock = DockStyle.Fill;
                    ucOutList.ApplyID = curApplyNum;
                    //���������������ʾ�Ѿ�����ı걾
                    if (currentTable.ImpProcess == "O")
                    {
                        string spec = "";
                        int index = 0;
                        if ((specList != null) && (specList.Count > 0))
                        {
                            foreach (string s2 in specList)
                            {
                                spec += s2;
                                if (index < specList.Count)
                                {
                                    spec += ",";
                                }
                                index++;
                            }
                            if (spec.Trim() == "")
                            {
                                return;
                            }
                        }
                        else
                        {
                            ucOutList.QuerySpecListByApplyNum(curApplyNum);
                        }
                        ucOutList.QueryImpedOutSpec(spec);
                    }
                    else
                    {
                        ucOutList.QuerySpecListByApplyNum(curApplyNum);
                    }
                    tpSpec.Controls.Add(ucOutList);
                    //QueryDemand(curApplyNum);
                }
            }
        }

        private void dgvResult_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvResult.SelectedCells.Count > 0)
            {
                applyNum = Convert.ToInt32(dgvResult.CurrentRow.Cells[2].Value.ToString());
                InitApplyTable();
                if (!string.IsNullOrEmpty(applyNum.ToString()))
                {
                    this.tabPage1.Text = "�������Ϣ[�����:" + applyNum.ToString() + "]";
                }
            }
        }

        private void btnAddNewDis_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbDisTypeAll.Text.Trim()))
            {
                //���Ϊ��˵�����ǲ�ѯ��Ӧ������Ա��洢��
                alDisType.Clear();
                hsDisType.Clear();
            }
            if (this.cmbDisType.Text == null)
            {
                MessageBox.Show("��ѡ���֣�", "������Ϣ");
                return;
            }
            else
            {
                string strDisType = string.Empty;
                string disTypeId = string.Empty;
                disTypeId = this.cmbDisType.SelectedValue.ToString();
                if (!string.IsNullOrEmpty(this.tbDisTypeAll.Text.Trim()))
                {
                    strDisType = this.tbDisTypeAll.Text.Trim();
                    if (hsDisType.Contains(disTypeId))
                    {
                        MessageBox.Show("����Ӹò��֣�", "������Ϣ");
                        return;
                    }
                    else
                    {
                        hsDisType.Add(disTypeId, this.cmbDisType.Text);
                        this.tbDisTypeAll.Text = strDisType + "," + this.cmbDisType.Text.ToString();
                    }
                }
                else
                {
                    if (hsDisType.Contains(disTypeId))
                    {
                        MessageBox.Show("����Ӹò��֣�", "������Ϣ");
                        return;
                    }
                    else
                    {
                        strDisType = this.cmbDisType.Text.ToString();
                        hsDisType.Add(disTypeId, strDisType);
                        this.tbDisTypeAll.Text = strDisType;
                    }
                }
            }
        }

        #region ���������걾���Ϳ�ѡ�����¼�
        private void cmbAppOper_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.txtOperName.Text = this.cmbAppOper.Text;
            this.cmbOperName.Text = this.cmbAppOper.Text;
        }

        private void cbIceOrg_CheckedChanged(object sender, EventArgs e)
        {
            if (cbIceOrg.Checked)
            {
                cbDna.Checked = true;
                cbRna.Checked = true;
            }
            else
            {
                cbDna.Checked = false;
                cbRna.Checked = false;
            }
        }

        private void cbDna_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbDna.Checked && !cbRna.Checked)
            {
                cbIceOrg.Checked = false;
                cbDnaCancer.Checked = false;
                cbDnaBesidCancer.Checked = false;
            }
            else if (cbDna.Checked && !cbRna.Checked)
            {
                cbIceOrg.Checked = true;
                cbRna.Checked = false;
                cbDnaCancer.Checked = true;
                cbDnaBesidCancer.Checked = true;
                cbRnaCancer.Checked = false;
                cbRnaBesidCancer.Checked = false;
            }
            else if (!cbDna.Checked && cbRna.Checked)
            {
                cbDnaCancer.Checked = false;
                cbDnaBesidCancer.Checked = false;
            }
            else
            {
                cbDnaCancer.Checked = true;
                cbDnaBesidCancer.Checked = true;
            }
        }

        private void cbRna_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbDna.Checked && !cbRna.Checked)
            {

                cbRnaCancer.Checked = false;
                cbRnaBesidCancer.Checked = false;
                cbIceOrg.Checked = false;
            }
            else if (cbDna.Checked && !cbRna.Checked)
            {
                cbRnaCancer.Checked = false;
                cbRnaBesidCancer.Checked = false;

            }
            else if (!cbDna.Checked && cbRna.Checked)
            {
                cbIceOrg.Checked = true;
                cbDna.Checked = false;
                cbRnaCancer.Checked = true;
                cbRnaBesidCancer.Checked = true;
            }
            else
            {
                cbRnaCancer.Checked = true;
                cbRnaBesidCancer.Checked = true;
            }
        }

        private void cbDnaCancer_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbDnaCancer.Checked && !cbDnaBesidCancer.Checked)
            {
                cbDna.Checked = false;
            }
            else if (cbDnaCancer.Checked && !cbDnaBesidCancer.Checked)
            {
                cbDna.Checked = true;
                cbDnaBesidCancer.Checked = false;
            }
        }

        private void cbDnaBesidCancer_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbDnaCancer.Checked && !cbDnaBesidCancer.Checked)
            {
                cbDna.Checked = false;
            }
            else if (!cbDnaCancer.Checked && cbDnaBesidCancer.Checked)
            {
                cbDna.Checked = true;
                cbDnaBesidCancer.Checked = false;
            }
        }

        private void cbRnaCancer_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbRnaCancer.Checked && !cbRnaBesidCancer.Checked)
            {
                cbRna.Checked = false;
            }
            else if (cbRnaCancer.Checked && !cbRnaBesidCancer.Checked)
            {
                cbRna.Checked = true;
                cbRnaBesidCancer.Checked = false;
            }
        }

        private void cbRnaBesidCancer_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbRnaCancer.Checked && !cbRnaBesidCancer.Checked)
            {
                cbRna.Checked = false;
            }
            else if (!cbRnaCancer.Checked && cbRnaBesidCancer.Checked)
            {
                cbRna.Checked = true;
                cbRnaCancer.Checked = false;
            }
        }

        private void cbPwOrg_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (cb.Checked)
            {
                cbSlice.Checked = true;
                cbSliceCancer.Checked = true;
                cbSliceNormal.Checked = true;
            }
            else
            {
                cbSlice.Checked = false;
                cbSliceCancer.Checked = false;
                cbSliceNormal.Checked = false;
            }
        }

        private void cbSlice_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (cb.Checked)
            {
                cbPwOrg.Checked = true;
                cbSliceCancer.Checked = true;
                cbSliceNormal.Checked = true;
            }
            else
            {
                cbPwOrg.Checked = false;
                cbSliceCancer.Checked = false;
                cbSliceNormal.Checked = false;
            }
        }

        private void cbSliceCancer_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (!cb.Checked && !cbSliceNormal.Checked)
            {
                cbPwOrg.Checked = false;
            }
            else if (cb.Checked && !cbSliceNormal.Checked)
            {
                cbPwOrg.Checked = true;
                cbSliceNormal.Checked = false;
            }
        }

        private void cbSliceNormal_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (!cb.Checked && !cbSliceCancer.Checked)
            {
                cbPwOrg.Checked = false;
            }
            else if (cb.Checked && !cbSliceCancer.Checked)
            {
                cbPwOrg.Checked = true;
                cbSliceCancer.Checked = false;
            }
        }

        private void cbSerum_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (cb.Checked)
            {
                cbSerumTumer.Checked = true;
                cbSerumControl.Checked = true;
            }
            else
            {
                cbSerumTumer.Checked = false;
                cbSerumControl.Checked = false;
            }
        }

        private void cbSerumTumer_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (!cb.Checked && !cbSerumControl.Checked)
            {
                cbSerum.Checked = false;
            }
            else if (cb.Checked && !cbSerumControl.Checked)
            {
                cbSerum.Checked = true;
                cbSerumControl.Checked = false;
            }
        }

        private void cbSerumControl_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (!cb.Checked && !cbSerumTumer.Checked)
            {
                cbSerum.Checked = false;
            }
            else if (cb.Checked && !cbSerumTumer.Checked)
            {
                cbSerum.Checked = true;
                cbSerumTumer.Checked = false;
            }
        }

        private void cbPlasma_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (cb.Checked)
            {
                cbPlasmaTumors.Checked = true;
                cbPlasmaControl.Checked = true;
            }
            else
            {
                cbPlasmaTumors.Checked = false;
                cbPlasmaControl.Checked = false;
            }
        }

        private void cbPlasmaTumors_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (!cb.Checked && !cbPlasmaControl.Checked)
            {
                cbPlasma.Checked = false;
            }
            else if (cb.Checked && !cbPlasmaControl.Checked)
            {
                cbPlasma.Checked = true;
                cbPlasmaControl.Checked = false;
            }
        }

        private void cbPlasmaControl_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (!cb.Checked && !cbPlasmaTumors.Checked)
            {
                cbPlasma.Checked = false;
            }
            else if (cb.Checked && !cbPlasmaTumors.Checked)
            {
                cbPlasma.Checked = true;
                cbPlasmaTumors.Checked = false;
            }
        }

        private void cbLymp_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (cb.Checked)
            {
                cbLympTumor.Checked = true;
                cbLympControl.Checked = true;
            }
            else
            {
                cbLympTumor.Checked = false;
                cbLympControl.Checked = false;
            }
        }

        private void cbLympTumor_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (!cb.Checked && !cbLympControl.Checked)
            {
                cbLymp.Checked = false;
            }
            else if (cb.Checked && !cbLympControl.Checked)
            {
                cbLymp.Checked = true;
                cbLympControl.Checked = false;
            }
        }

        private void cbLympControl_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (!cb.Checked && !cbLympTumor.Checked)
            {
                cbLymp.Checked = false;
            }
            else if (cb.Checked && !cbLympTumor.Checked)
            {
                cbLymp.Checked = true;
                cbLympTumor.Checked = false;
            }
        }
        #endregion
        //private void btnApply_Click(object sender, EventArgs e)
        //{
        //    string sequence = "";
        //    applyTable = new ApplyTable();
        //    applyTableManage.GetNextSequence(ref sequence);
        //    lblNum.Text = sequence;
        //    applyTable.ApplyId = Convert.ToInt32(sequence);
        //    applyNum = 0;
        //    ApplyOrImp = "";
        //}
    }
}