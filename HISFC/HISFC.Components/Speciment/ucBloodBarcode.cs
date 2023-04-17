using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using System.Collections;

namespace FS.HISFC.Components.Speciment
{
    public partial class ucBloodBarcode : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        //ҵ���������
        private SpecSourceManage specSourceManage;
        private SpecSourcePlanManage specSourcePlanManage;    
        private SpecTypeManage specTypeManage;
        private SpecBarCodeManage barCodeManage;
        private DisTypeManage disTypeManage;
        private OperApplyManage operApplyMgr;
        private DiagnoseManage diagManage;

        //����
        private Dictionary<string, List<string>> dicBarCode;      
        //�����걾�м���
        private List<SpecBox> useFullBoxList;
        //���صı걾���Ϳؼ�
        private ucSpecSourceForBlood[] ucSourceForBlood;

        //����ԭ�걾�����뼰��װ��Ϣ
        private Dictionary<SpecSource, List<SpecSourcePlan>> dicSourcePlanList;
        private List<SubSpec> subList;
        private Dictionary<string, int> dicDiagToPos;

        private FS.HISFC.Models.Base.Employee loginPerson;
        private System.Data.IDbTransaction trans;
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService;
        private FS.HISFC.Models.Speciment.SpecType specType;

        /// <summary>
        /// ���͵����ϴ�ӡ���Ĳ���
        ///  Print2DBarCode(List<string> barCode, List<string> sequence, string host, List<string> disType, List<string> num)
        /// </summary>
        private List<string> barCodeList;
        private List<string> sequenceList;
        private List<string> disTypeList;
        private List<string> numList;
        private List<string> specTypeList;
 
        //��������ﲡ�˱���¼�����
        private bool needSaveDiagnose = false;
        private bool customDis = false;
        private bool enbleDis = true;

        public bool EnbleDis
        {
            get
            {
                return enbleDis;
            }
            set
            {
                enbleDis = value;
            }
        }

        public ucBloodBarcode()
        {
            InitializeComponent();        
            specSourcePlanManage = new SpecSourcePlanManage();
            specSourceManage = new SpecSourceManage();
            specType = new FS.HISFC.Models.Speciment.SpecType();
            specTypeManage = new SpecTypeManage();
            barCodeManage = new SpecBarCodeManage();
            diagManage = new DiagnoseManage();

            dicBarCode = new Dictionary<string, List<string>>();
            dicSourcePlanList = new Dictionary<SpecSource, List<SpecSourcePlan>>();
            disTypeManage = new DisTypeManage();
            operApplyMgr = new OperApplyManage();
            useFullBoxList = new List<SpecBox>();
            subList = new List<SubSpec>();
            loginPerson = new FS.HISFC.Models.Base.Employee();
            barCodeList = new List<string>();
            sequenceList = new List<string>();
            disTypeList = new List<string>();
            numList = new List<string>();
            specTypeList = new List<string>();
            dicDiagToPos = new Dictionary<string, int>();
            toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        }

        /// <summary>
        /// ������϶�Ӧ�Ĳ���
        /// </summary>
        private void SaveDiagToDis()
        {
            FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
            //type,cons.ID,cons.Name,cons.Memo,cons.SpellCode,cons.WBCode,cons.UserCode,cons.SortID,FS.NFC.Function.NConvert.ToInt32(cons.IsValid),this.Operator.ID);

            FS.HISFC.Models.Base.Const c = new FS.HISFC.Models.Base.Const();
            string type = "DiagnosebyNurse";
            c.ID = "";
            string id = "";
            disTypeManage.GetNextSequence(ref id);
            c.ID = id;

            c.Name = cmbDiagnose.Text;
            c.Memo = loginPerson.Dept.ID;
            c.UserCode = cmbDisType.Text.Trim();
            c.SortID = 1;
            c.IsValid = true;
            int inRes = conMgr.InsertItem(type, c);
        }

        private void ClearData()
        {
            foreach (KeyValuePair<SpecSource, List<SpecSourcePlan>> tmp in dicSourcePlanList)
            {
                tmp.Key.SpecId = 0;
            }
            useFullBoxList = new List<SpecBox>();
            subList = new List<SubSpec>();
            barCodeList = new List<string>();
            sequenceList = new List<string>();
            disTypeList = new List<string>();
            numList = new List<string>();
            specTypeList = new List<string>();
            txtBarCode.Focus();
        }

        /// <summary>
        /// �󶨲�������
        /// </summary>
        private void DisTypeBinding()
        {
            //Dictionary<int, string> dicDisType = disTypeManage.GetAllDisType();
            //if (dicDisType.Count > 0)
            //{
            //    BindingSource bs = new BindingSource();
            //    bs.DataSource = dicDisType;
            //    cmbDisType.DisplayMember = "Value";
            //    cmbDisType.ValueMember = "Key";
            //    cmbDisType.DataSource = bs;
            //}
            //cmbDisType.Text = "";
            ArrayList alDisType = disTypeManage.GetAllValidDisType();
            if (alDisType != null)
            {
                if (alDisType.Count > 0)
                {
                    cmbDisType.AddItems(alDisType);
                }
            }
            cmbDisType.Text = "";
        }

        private string GenerateSpecSql()
        {
            string sql = "SELECT distinct * FROM SPEC_SOURCE WHERE ORGORBLOOD = 'B' ";
            if (dtpStart.Value != null) sql += " and OPERTIME>=to_date('" + dtpStart.Value.Date.ToString() + "','yyyy-mm-dd hh24:mi:ss')";
            if (dtpEnd.Value != null)
                sql += " and OPERTIME<=to_date('" + dtpEnd.Value.AddDays(1.0).Date.ToString() + "','yyyy-mm-dd hh24:mi:ss')";
            sql += " order by OPERTIME desc";
            return sql;
        }

        private string GenerateApplySql()
        {
            string sql = "select distinct * from SPEC_OPERAPPLY where ORGORBLOOD='B'";
            if (txtBarCodeQ.Text.Trim() != "")
            {
                sql += " and Id =" + txtBarCodeQ.Text.Trim();
            }
            else
            {
                if (dtpStart.Value != null)
                    sql += " and OPERTIME>=to_date('" + dtpStart.Value.Date.ToString() + "','yyyy-mm-dd hh24:mi:ss')";
                if (dtpEnd.Value != null)
                    sql += " and OPERTIME<=to_date('" + dtpEnd.Value.AddDays(1.0).Date.ToString() + "','yyyy-mm-dd hh24:mi:ss')";
                if (cmbOperDept.Tag != null && cmbOperDept.Text != "") sql += " and OPERDEPTNAME = '" + cmbOperDept.Text.Trim() + "'";
            }
            return sql;
        }

        /// <summary>
        /// �����������뵥
        /// </summary>
        private void LoadOperApply()
        {
            string sql = GenerateApplySql();
            ArrayList arrList = operApplyMgr.GetOperApplyBySql(sql);
            if (arrList != null || arrList.Count > 0)
            {
                tvSpec.ShowNodeToolTips = true;
                foreach (OperApply apply in arrList)
                {
                    //δȡ�걾
                    if (apply.HadCollect == "0" || apply.HadCollect == "4" || apply.HadCollect == "5" || apply.HadCollect == "6")
                    {
                        TreeNode tnNotGet = tvSpec.Nodes[2];
                        TreeNode tn = new TreeNode();
                        tn.Text = apply.PatientName + " ��" + apply.OperDeptName.ToString() + "�� ";
                        tn.ToolTipText = apply.OperApplyId.ToString().PadLeft(12, '0');

                        tn.Tag = apply;
                        tnNotGet.Nodes.Add(tn);
                    }

                    //ȡ����ȡ
                    if (apply.HadCollect == "3")
                    {
                        TreeNode tnNotGet = tvSpec.Nodes[3];
                        TreeNode tn = new TreeNode();
                        tn.Text = apply.PatientName + " ��" + apply.OperDeptName + "�� ";
                        tn.Tag = apply;
                        tn.ToolTipText = apply.OperApplyId.ToString().PadLeft(12, '0');
                        tnNotGet.Nodes.Add(tn);
                    }

                    //����
                    if (apply.HadCollect == "2")
                    {
                        TreeNode tnNotGet = tvSpec.Nodes[1];
                        TreeNode tn = new TreeNode();
                        tn.Text = apply.PatientName + " ��" + apply.OperDeptName + "�� ";
                        tn.ToolTipText = apply.OperApplyId.ToString().PadLeft(12, '0');
                        tn.Tag = apply;
                        tnNotGet.Nodes.Add(tn);
                    }
                }
            }
            tvSpec.Nodes[2].Text = "��ʿվδ��  (��:" + tvSpec.Nodes[2].Nodes.Count.ToString() + " ��)";
            tvSpec.Nodes[3].Text = "��ȡ��  (��:" + tvSpec.Nodes[3].Nodes.Count.ToString() + " ��)";
            tvSpec.Nodes[1].Text = "��ʿվ����  (��:" + tvSpec.Nodes[1].Nodes.Count.ToString() + " ��)";

        }

        /// <summary>
        /// ���������б�
        /// </summary>
        private void LoadSpecSource()
        {
            tvSpec.Nodes[0].Nodes.Clear();
            string sql = GenerateSpecSql();
            ArrayList arrList = specSourceManage.GetSpecSource(sql);
            if (arrList != null || arrList.Count > 0)
            {
                foreach (SpecSource source in arrList)
                {
                    TreeNode tn = tvSpec.Nodes[0];
                    TreeNode node = new TreeNode();
                    node.Text = source.HisBarCode + " ��" + source.SendTime.ToString() + "��";
                    node.Tag = source;
                    tn.Nodes.Add(node);
                }
            }
            tvSpec.Nodes[0].Text = "���ձ걾  (��:" + tvSpec.Nodes[0].Nodes.Count.ToString() + " ��)";
            if (tvSpec.Nodes[0].Nodes.Count > 0)
            {
                tvSpec.SelectedNode = tvSpec.Nodes[0].Nodes[0];
            }
        }

        /// <summary>
        /// ȡ������
        /// </summary>
        private void CancelSpec()
        {
            DialogResult d = MessageBox.Show("ȷ��ȡ��?", "ȡ��", MessageBoxButtons.YesNo);
            if (d == DialogResult.No)
            {
                return; 
            }
            TreeNode tn = tvSpec.SelectedNode;
            if (tn == null)
            {
                MessageBox.Show("��ѡ����Ҫȡ��������");
                return;
            }
            if (tn.Tag == null)
            {
                MessageBox.Show("��ȡ������Ϣʧ��");
                return;
            }

            try
            {
                OperApply operApply = tn.Tag as OperApply;
                if (operApplyMgr.UpdateColFlag(operApply.OperApplyId.ToString(), "3") == -1)
                {
                    MessageBox.Show("����ʧ��");
                    return;
                }
                MessageBox.Show("�����ɹ�");
            }
            catch
            {
                MessageBox.Show("���ͱ걾����ȡ����");
            }
        }

        /// <summary>
        /// ����ȡ���걾
        /// </summary>
        private void ReCancelSpec()
        {
            TreeNode tn = tvSpec.SelectedNode;
            if (tn == null)
            {
                MessageBox.Show("��ѡ����Ҫ����ȡ��������");
                return;
            }
            if (tn.Tag == null)
            {
                MessageBox.Show("��ȡ������Ϣʧ��");
                return;
            }

            try
            {
                OperApply operApply = tn.Tag as OperApply;
                if (operApplyMgr.UpdateColFlag(operApply.OperApplyId.ToString(), "0") == -1)
                {
                    MessageBox.Show("����ʧ��");
                    return;
                }
                MessageBox.Show("�����ɹ�");
            }
            catch
            {
                //MessageBox.Show("���ͱ걾����ȡ����");
            }
        }

        private void ReLoad(object sender, EventArgs e)
        {
            Query(null, null);
        }
        private void GenerateColForm()
        {
            FS.HISFC.Components.Speciment.Print.ucBColForm frmCol = new FS.HISFC.Components.Speciment.Print.ucBColForm();
            frmCol.OnReLoad += new Speciment.Print.ucBColForm.ReLoad(ReLoad);            
            frmCol.Show();
            //frmL.OnSetContainerId += new frmLocate.SetContainerId(OnSetContainerId);
           
        }

        /// <summary>
        /// ����Ѫ�걾����
        /// </summary>
        private void InitSpecType()
        {
            ArrayList arr = specTypeManage.GetSpecByOrgName("Ѫ");
            if (arr == null || arr.Count == 0)
            {
                return;
            }
            ArrayList temp = new ArrayList();
            //int count = arr.Count;
            for (int i = 0; i < arr.Count; i++)
            {
                FS.HISFC.Models.Speciment.SpecType st = arr[i] as FS.HISFC.Models.Speciment.SpecType;
                if (st.IsShow == "0")
                {
                    continue;
                }
                temp.Add(st);                
            }
            ucSourceForBlood = new ucSpecSourceForBlood[temp.Count];
            int index = 0;
            foreach (FS.HISFC.Models.Speciment.SpecType type in temp)
            {
                ucSourceForBlood[index] = new ucSpecSourceForBlood();
                ucSourceForBlood[index].SetSpecTypeName(type.SpecTypeID.ToString());               
                flpSubDetail.Controls.Add(ucSourceForBlood[index]);
                index++;
            } 
        }

        /// <summary>
        /// ��̬����FarPoint��
        /// </summary>
        private void GenerateColumn()
        {
            neuSpread1_Sheet1.ColumnCount = flpSubDetail.Controls.Count + 2;
            int index = 1;
           
            foreach (Control c in flpSubDetail.Controls)
            {
                ucSpecSourceForBlood subBlood = c as ucSpecSourceForBlood;
                GroupBox grp = subBlood.Controls[0] as GroupBox;
                int count = 0;
                string planInfo = "";
                foreach (Control cSub in grp.Controls)
                {
                    if (cSub.Name == "chkName")
                    {
                        CheckBox chkName = cSub as CheckBox;
                        neuSpread1_Sheet1.Columns[index].Label = chkName.Text;
                        planInfo += chkName.Text;
                    }
                    if (cSub.Name == "nudCount")
                    {
                        NumericUpDown nud = cSub as NumericUpDown;
                        count = Convert.ToInt32(nud.Value);
                        neuSpread1_Sheet1.Columns[index].Width = 90 * count;
                    }
                }
                FarPoint.Win.Spread.CellType.TextCellType cellType = new FarPoint.Win.Spread.CellType.TextCellType();
                neuSpread1_Sheet1.Columns[index].CellType = cellType;
                neuSpread1_Sheet1.Columns[index].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                neuSpread1_Sheet1.Columns[index].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                index++;
            }
        }

        /// <summary>
        /// �Զ�����������
        /// </summary>
        private void GenerateBarCode(SpecSource source)
        {
            dicSourcePlanList = new Dictionary<SpecSource, List<SpecSourcePlan>>();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            specSourceManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            specSourcePlanManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            barCodeManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            diagManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            #region �γ�����
            try
            {
                //��ǰ��װ�걾�����к�
                string sequence = "";
                string curSourceCode = source.HisBarCode;
                //�������µ��������ζ��û�б���
                //saved = false;
                int rowIndex = neuSpread1_Sheet1.ActiveCell.Row.Index;
                neuSpread1_Sheet1.Rows.Add(rowIndex + 1, 1);
                neuSpread1_Sheet1.Cells[rowIndex, 0].Text = curSourceCode;
                if (!dicBarCode.ContainsKey(curSourceCode))
                {
                    dicBarCode.Add(curSourceCode, new List<string>());
                }

                //���õ�ǰ�걾Դ��Ϣ
                if (source == null)
                {
                    source = new SpecSource();
                }
                dicSourcePlanList.Add(source, new List<SpecSourcePlan>());
                int column = 1;

                //Ҫ���ɵı걾�����б�
                //List<string> specTypeList = new List<string>();
                //��ǰ�Ĳ���
                string disType = "";

                foreach (Control c in flpSubDetail.Controls)
                {
                    //��ȡ��ǰ�걾Դ�ķ�װ��Ϣ
                    ucSpecSourceForBlood subBlood = c as ucSpecSourceForBlood;
                    GroupBox grp = subBlood.Controls[0] as GroupBox;
                    int count = 0;
                    //ָ���ض���ʹ��
                    bool specialUse = false;
                    //�ض��˵�����
                    string specialName = "";
                    //�Ƿ���Ҫ��ǰ���͵ı걾
                    bool curChecked = false;
                    //�Ƿ��Զ���������
                    bool autoGenCode = true;
                    //���ɵ�������
                    string codes = "";
                    string specType = "";

                    List<string> tmpBarCode = new List<string>();
                    SpecSourcePlan tmpPlan = new SpecSourcePlan();
                    foreach (Control cSub in grp.Controls)
                    {
                        if (cSub.Name == "nudCount")
                        {
                            NumericUpDown nud = cSub as NumericUpDown;
                            count = Convert.ToInt32(nud.Value);
                            tmpPlan.Count = count;
                            tmpPlan.StoreCount = count;
                        }
                        if (cSub.Name == "chkName")
                        {
                            CheckBox chk = cSub as CheckBox;
                            curChecked = chk.Checked ? true : false;
                            tmpPlan.SpecType.SpecTypeID = Convert.ToInt32(cSub.Tag.ToString());
                            if (!curChecked)
                            {
                                break;
                            }
                            specType = chk.Text.Trim();
                        }
                        if (cSub.Name == "nudCapcity")
                        {
                            NumericUpDown nudCap = cSub as NumericUpDown;
                            tmpPlan.Capacity = nudCap.Value;
                        }
                        if (cSub.Name == "nudUse")
                        {
                            NumericUpDown nudUse = cSub as NumericUpDown;
                            tmpPlan.ForSelfUse = Convert.ToInt32(nudUse.Value);
                        }
                        if (cSub.Name == "chkOnlySelf")
                        {
                            CheckBox chkCheck = cSub as CheckBox;
                            specialUse = chkCheck.Checked ? true : false;
                        }
                        if (cSub.Name == "txtName")
                        {
                            TextBox txt = cSub as TextBox;
                            specialName = txt.Text;
                        }
                        if (cSub.Name == "chkAutoGen")
                        {
                            CheckBox chkAuto = cSub as CheckBox;
                            autoGenCode = chkAuto.Checked;
                        }
                    }
                    if (!curChecked)
                    {
                        column++;
                        continue;
                    }
                    if (specialUse && specialName.Trim() != "")
                    {
                        tmpPlan.LimitUse = specialName;
                    }
                    //string codes = "";
                    disType = cmbDisType.Text.TrimEnd().TrimStart();

                    //ȡѪ�걾��������к�
                    sequence = barCodeManage.GetMaxSeqByDisAndType(disType, "1");
                    //��װ�걾�����                   
                    if (autoGenCode)
                    {
                        SpecBarCode tmpSubBarCode = barCodeManage.GetSpecBarCode(disType, specType);
                        if (tmpSubBarCode == null || tmpSubBarCode.Sequence == "")
                        {
                            ClearData();
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show("��������ʧ�ܣ��鲻������Ϊ" + disType + " �걾����Ϊ" + specType + " ����Ϣ");
                            return;
                        }
                        if (sequence == null || sequence == "")
                        {
                            ClearData();
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show("���ұ걾���к�ʧ��");
                            return;
                        }
                        string barCodePre = tmpSubBarCode.Sequence.PadLeft(6, '0') + tmpSubBarCode.DisAbrre + tmpSubBarCode.SpecTypeAbrre;
                        for (int i = 0; i < count; i++)
                        {
                            string barCode = barCodePre + (i + 1).ToString();
                            codes += barCode;

                            if (i < count - 1)
                            {
                                codes += ",";
                            }
                            dicBarCode[curSourceCode].Add(barCode);
                            tmpPlan.SubSpecCodeList.Add(barCode);
                            barCodeList.Add(barCode);
                            sequenceList.Add(sequence);
                            numList.Add((i + 1).ToString());
                            disTypeList.Add(disType);
                            specTypeList.Add(specType);
                        }
                    }
                    if (!autoGenCode)
                    {
                        foreach (Control cSub in grp.Controls)
                        {
                            if (cSub.Name == "lsbBarCode")
                            {
                                ListBox lb = cSub as ListBox;
                                foreach (string s in lb.Items)
                                {
                                    tmpBarCode.Add(s);
                                }
                            }
                        }
                        tmpPlan.SubSpecCodeList = tmpBarCode;
                        for (int i = 0; i < tmpBarCode.Count; i++)
                        {
                            codes += tmpBarCode[i];//
                            if (i < tmpBarCode.Count - 1)
                                codes += ",";
                            barCodeList.Add(tmpBarCode[i]);
                            sequenceList.Add("");
                            numList.Add("");
                            disTypeList.Add(disType);
                        }
                    }
                    neuSpread1_Sheet1.Cells[rowIndex, column].Text = codes;
                    dicSourcePlanList[source].Add(tmpPlan);
                    column++;
                }

                if (barCodeManage.UpdateMaxSeqByDisAndType(disType, "1", (Convert.ToInt32(sequence) + 1).ToString()) <= 0)
                {
                    ClearData();
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show("�����������ʧ��");
                    return;
                }
            #endregion

            #region ����Դ�걾����װ�걾
                if (dicSourcePlanList.Count <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                    return;
                }

                SpecInOper specInOper = new SpecInOper();
                //subList.Clear();
                foreach (KeyValuePair<SpecSource, List<SpecSourcePlan>> tmp in dicSourcePlanList)
                {
                    if (tmp.Key.SpecId > 0)
                    {
                        if (specSourceManage.UpdateSpecSource(tmp.Key) <= 0)
                        {
                            MessageBox.Show("����ʧ�ܣ�", "��������");
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            return;

                        }
                    }
                    if (tmp.Key.SpecId <= 0)
                    {
                        string seqSource = "";
                        specSourceManage.GetNextSequence(ref seqSource);
                        tmp.Key.SpecId = Convert.ToInt32(seqSource);
                        tmp.Key.SpecNo = sequence.PadLeft(6,'0');
                        if (specSourceManage.InsertSpecSource(tmp.Key) <= 0)
                        {
                            ClearData();
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            //foreach (KeyValuePair<SpecSource, List<SpecSourcePlan>> tmp1 in dicSourcePlanList)
                            //{
                            //    tmp1.Key.SpecId = 0;
                            //}
                            MessageBox.Show("����ʧ�ܣ�", "��������");
                            return;

                        }
                        if(needSaveDiagnose)
                        {
                            tmp.Key.IsInBase = '1';
                            SpecDiagnose diag = new SpecDiagnose();
                            string seq = "";
                            diagManage.GetNextSequence(ref seq);
                            diag.BaseID = Convert.ToInt32(seq);
                            diag.Diag.IcdName = cmbDiagnose.Text;
                            diag.InBaseTime = DateTime.Now;
                            diag.SpecSource.SpecId = tmp.Key.SpecId;
                            if (diagManage.InsertDiagnose(diag) == -1)
                            {
                                ClearData();
                                FS.FrameWork.Management.PublicTrans.RollBack();;
                                //foreach (KeyValuePair<SpecSource, List<SpecSourcePlan>> tmp1 in dicSourcePlanList)
                                //{
                                //    tmp1.Key.SpecId = 0;
                                //}
                                MessageBox.Show("���������Ϣʧ�ܣ�", "��������");
                                return;
                            }
                        }
                    }
                    foreach (SpecSourcePlan plan in tmp.Value)
                    {
                        plan.SpecID = tmp.Key.SpecId;
                        plan.TumorType = Convert.ToInt32(Constant.TumorType.����).ToString();
                        string planSequence = "";
                        planSequence = specSourcePlanManage.GetNextSequence();
                        plan.PlanID = Convert.ToInt32(planSequence);
                        if (specSourcePlanManage.InsertSourcePlan(plan) <= 0)
                        {
                            ClearData();
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show("����ʧ�ܣ�", "��������");
                            //foreach (KeyValuePair<SpecSource, List<SpecSourcePlan>> tmp1 in dicSourcePlanList)
                            //{
                            //    tmp1.Key.SpecId = 0;
                            //}
                            return;
                        }
                        specInOper.DisTypeId = tmp.Key.DiseaseType.DisTypeID.ToString();
                        specInOper.LoginPerson = loginPerson;
                        specInOper.Trans = trans;
                        specInOper.SpecTypeId = plan.SpecType.SpecTypeID.ToString();
                        specInOper.SetTrans();

                        int saveResult = specInOper.SaveSubSpec(plan, ref subList);
                        if (saveResult == -1)
                        //if (SaveSubSpec(plan,tmp.Key.DiseaseType.DisTypeID.ToString()) == -1)
                        {
                            ClearData();
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show("����ʧ�ܣ�", "��������");
                            //foreach (KeyValuePair<SpecSource, List<SpecSourcePlan>> tmp1 in dicSourcePlanList)
                            //{
                            //    tmp1.Key.SpecId = 0;
                            //}
                            return;
                        }
                        if (saveResult == -2)
                        {
                            ClearData();
                            //foreach (KeyValuePair<SpecSource, List<SpecSourcePlan>> tmp1 in dicSourcePlanList)
                            //{
                            //    tmp1.Key.SpecId = 0;
                            //}
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show("���ұ걾��ʧ�ܣ�", "��������");
                            return;
                        } 
                    }
                }
                //��ֹһ����ǩ����2����ͬ�����кš�
                if (sequenceList.Count > 0)
                {
                    string seq = sequenceList[0];
                    //string seq1 = "";
                    for (int i = 1; i < sequenceList.Count; i++)
                    {
                        if (sequenceList[i] != seq)
                        {
                            ClearData();
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show("���ɱ�ǩ����");
                            return ;
                        }
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                dicSourcePlanList = new Dictionary<SpecSource, List<SpecSourcePlan>>();
                //saved = true;
                this.useFullBoxList = specInOper.UseFullBoxList;
                MessageBox.Show("����ɹ���", "��������");
                chkCli.Checked = false;
                dtpColTime.Focus();
                //nudHour.Focus();
                //nudHour.Select(0, 2);
                nudCount.Value = 1;
                nudNoCount.Value = 1;
                neuSpread1_Sheet1.Rows.Count = 0;//.Clear();
                neuSpread1_Sheet1.Rows.Add(0, 1);
                //��ոñ�־
                //return 1;
            }

            catch
            {
                ClearData();
                //foreach (KeyValuePair<SpecSource, List<SpecSourcePlan>> tmp in dicSourcePlanList)
                //{
                //    tmp.Key.SpecId = 0;
                //}
                FS.FrameWork.Management.PublicTrans.RollBack();;
                MessageBox.Show("����ʧ�ܣ�", "��������");
                return;
            }
                #endregion

            #region ����

            //����
            //if (SaveSource() == -1)
            //{
            //    barCodeList = new List<string>();
            //    sequenceList = new List<string>();
            //    disTypeList = new List<string>();
            //    numList = new List<string>();
            //    List<string> tmpList = new List<string>();
            //    foreach (string type in specTypeList)
            //    {
            //        tmpList.Add(type);
            //        if (tmpList.Contains(type))
            //        {
            //            continue;
            //        }
            //        SpecBarCode tmp = barCodeManage.GetSpecBarCode(disType, type);
            //        if (tmp != null && tmp.Sequence.Trim() != "")
            //        {
            //            int seq = Convert.ToInt32(tmp.Sequence);
            //            seq = seq - 1;                      
            //            int s = barCodeManage.UpdateBarCode(disType, type, seq.ToString());
            //        }
            //    }
            //    return;
            //}
            #endregion

            #region  ��ӡ��ǩ

            DialogResult dr = MessageBox.Show("�Ƿ��ӡ��ǩ?", "��ǩ��ӡ", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                int result = PrintLabel.Print2DBarCode(barCodeList, sequenceList, specTypeList, disTypeList, numList);
                if (result == -2)
                {
                    MessageBox.Show("���Ӵ�ӡ��ʧ��");

                }
                if (result == -1)
                {
                    MessageBox.Show("��ӡʧ��");
                }
            }
            barCodeList = new List<string>();
            sequenceList = new List<string>();
            specTypeList = new List<string>();
            disTypeList = new List<string>();
            numList = new List<string>();
            foreach (SpecBox box in this.useFullBoxList)
            {
                FS.HISFC.Models.Speciment.SpecType tmp = specTypeManage.GetSpecTypeByBoxId(box.BoxId.ToString());
                string strHint = box.BoxBarCode;
                if (tmp != null)
                {
                    strHint += "  " + tmp.SpecTypeName;
                }
                strHint += " �걾��������������µı걾�У�";
                MessageBox.Show(strHint);
                //��ʾ�û�����µı걾��
                FS.HISFC.Components.Speciment.Setting.frmSpecBox newSpecBox = new FS.HISFC.Components.Speciment.Setting.frmSpecBox();
                if (box.DesCapType == 'B')
                    newSpecBox.CurLayerId = box.DesCapID;
                else
                    newSpecBox.CurShelfId = box.DesCapID;
                newSpecBox.DisTypeId = box.DiseaseType.DisTypeID;
                newSpecBox.OrgOrBlood = box.OrgOrBlood;
                newSpecBox.SpecTypeId = box.SpecTypeID;
                newSpecBox.Show();
            }
            #endregion

        }

        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="inHosNum">סԺ��ˮ��</param>
        /// <returns>���˵�ID</returns>
        private int GetPatientInfo(string inHosNum)
        {
            FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            FS.HISFC.BizLogic.RADT.InPatient patientMgr = new FS.HISFC.BizLogic.RADT.InPatient();
            string cardNo = "";
            if (inHosNum.Contains("C") || inHosNum.Contains("M")|| chkCli.Checked)
            {
                patientInfo = patientMgr.QueryComPatientInfo(inHosNum);
                cardNo = inHosNum;
                needSaveDiagnose = true;
            }
            else
            {
                patientInfo = patientMgr.QueryComPatientInfo(inHosNum.Substring(4));
                cardNo = inHosNum.Substring(4);
            }
            if (patientInfo == null)
            {
                MessageBox.Show("��ȡ������Ϣʧ�ܣ�");
                return -1;
            }
            //���ݲ����Ų�ѯ�걾�ⲡ����Ϣ��������ڸ��£������ڲ���
            PatientManage pm = new PatientManage();
            SpecPatient specPatient = new SpecPatient();           
            string sql = "select distinct * from SPEC_PATIENT where CARD_NO = '" + cardNo + "'";
            sql += " union select distinct * from SPEC_PATIENT where IC_CARDNO = '" + patientInfo.Card.ICCard.ID + "'";
            ArrayList arr = pm.GetPatientInfo(sql);

            //��ȡ������Ϣ��������Ϣ
            specPatient.Address = patientInfo.AddressHome;
            specPatient.Birthday = patientInfo.Birthday;
            specPatient.BloodType = patientInfo.BloodType.ID.ToString();
            specPatient.IcCardNo = patientInfo.Card.ICCard.ID;
            specPatient.ContactNum = patientInfo.PhoneBusiness;
            specPatient.Gender = Convert.ToChar(patientInfo.Sex.ID.ToString());
            specPatient.Home = patientInfo.DIST;
            specPatient.HomePhoneNum = patientInfo.PhoneHome;
            specPatient.IdCardNo = patientInfo.IDCard;
            specPatient.IsMarried = patientInfo.MaritalStatus.ID.ToString();
            specPatient.CardNo = inHosNum.Substring(4);
            switch (patientInfo.MaritalStatus.ID.ToString())
            {
                case "W":
                    specPatient.IsMarried = "4";
                    break;
                case "A":
                    specPatient.IsMarried = "5";
                    break;
                case "R":
                    specPatient.IsMarried = "5";
                    break;
                case "D":
                    specPatient.IsMarried = "3";
                    break;
                case "M":
                    specPatient.IsMarried = "2";
                    break;
                case "S":
                    specPatient.IsMarried = "1";
                    break;
            }
            specPatient.Nation = patientInfo.Nationality.ID;
            specPatient.Nationality = patientInfo.Country.ID;
            specPatient.PatientName = patientInfo.Name;
            if (arr != null && arr.Count > 0)
            {
                SpecPatient tmp = arr[0] as SpecPatient;
                specPatient.PatientID = tmp.PatientID;
                pm.UpdatePatient(specPatient);
                return specPatient.PatientID;
            }
            else
            {
                string sequence = "";
                specPatient.CardNo = cardNo;
                pm.GetNextSequence(ref sequence);
                if (sequence == "")
                {
                    MessageBox.Show("��ȡ��������ʧ�ܣ�");
                    return -1;
                }
                specPatient.PatientID = Convert.ToInt32(sequence);
                if (pm.InsertPatient(specPatient) <= 0)
                {
                    MessageBox.Show("���²�����Ϣʧ�ܣ�");
                    return -1;
                }
                return specPatient.PatientID;
            }       
          
        }

        /// <summary>
        /// ��SPEC_OPERAPPLY�л�ȡ�ɼ�����Ϣ������������
        /// </summary>
        /// <param name="barCode"></param>
        private void GetApplyInfoAndSubBarCode(string barCode)
        {
            try
            {   
                //id��������ֵ
                int id = Convert.ToInt32(barCode);
                FS.HISFC.Models.Speciment.OperApply operApply = operApplyMgr.GetById(id.ToString(),"B");

                if (operApply == null || operApply.OperApplyId <= 0)
                {
                    MessageBox.Show("��ȡ��Ϣʧ�ܣ������Ƿ���ڸ�����ţ�");
                    txtBarCode.Text = "";
                    cmbDisType.Text = "";
                    txtBarCode.Focus();
                    return;
                }
                if (operApply.HadCollect == "3")
                {
                    txtBarCode.Text = "";
                    MessageBox.Show("��������ȡ��");
                    return;
                }

                int patientId = GetPatientInfo(operApply.InHosNum);
                if (patientId == -1)
                {
                    return;
                }

                txtInHosNum.Text = operApply.InHosNum;
                txtName.Text = operApply.PatientName;
                if (!txtInHosNum.Text.Trim().Contains("C") && !txtInHosNum.Text.Trim().Contains("M") && !customDis)
                {
                    cmbDiagnose.Text = operApply.MainDiaName;
                }

                if (dicDiagToPos.ContainsKey(operApply.MainDiaName) && !customDis)
                {
                    cmbDisType.Tag = dicDiagToPos[operApply.MainDiaName];

                    //cmbDisType.Text = ((KeyValuePair<int, string>)(cmbDisType.SelectedItem)).Value;
                }

                if (cmbDisType.Tag == null || cmbDisType.Text == "")
                {
                    MessageBox.Show("��ѡ����ϣ����ֽ��Զ�����!", "�����ӡ");
                    cmbDiagnose.Focus();
                    //cmbDisType.Focus();
                    return;
                }
                //����ǹ��裬Ĭ�Ϸ�װѪ������ϸ��
                if (cmbDisType.Text.Contains("����"))
                {
                    nudNoCount.Value = 0;
                }
                if (needSaveDiagnose && cmbDiagnose.Text.Trim() == "")
                {
                    MessageBox.Show("���ﲡ�˱���¼�����!");
                    cmbDiagnose.Focus();
                    return;
                }

                DialogResult confirm = MessageBox.Show("��Ϣ�˶����", "��ӡ", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.No)
                {
                    return;
                }

                ArrayList arr = specSourceManage.GetSpecSource("select distinct * from spec_source where hisbarcode = '" + barCode + "'");
                if (arr != null && arr.Count > 0)
                {
                    MessageBox.Show("�˱걾�Ѿ���װ��");
                    txtBarCode.Focus();
                    return;
                }
                SpecSource tmpSource = new SpecSource();
                tmpSource.AnticolBldCapcacity = nudCapcity.Value;
                tmpSource.AnticolBldCount = Convert.ToInt32(nudCount.Value);
                tmpSource.NonAntiBldCapcacity = nudNoCapacity.Value;
                tmpSource.NonantiBldCount = Convert.ToInt32(nudNoCount.Value);
                tmpSource.Patient.PatientID = patientId;
                tmpSource.HisBarCode = barCode;
                tmpSource.InPatientNo = operApply.InHosNum;
                tmpSource.IsHis = '1';
                tmpSource.MediDoc = operApply.MediDoc.Clone();
                tmpSource.DeptNo = operApply.OperDeptId;
                tmpSource.SendDoctor = operApply.MediDoc.MainDoc.Clone();
                tmpSource.OrgOrBoold = "B";
                tmpSource.DiseaseType.DisTypeID = Convert.ToInt32(cmbDisType.Tag.ToString());
                tmpSource.GetSpecPeriod = operApply.GetPeriod;
                tmpSource.TumorPor = operApply.TumorPor;
                tmpSource.OperPosCode = operApply.OperPosId;
                tmpSource.OperPosName = operApply.OperPosName;
                tmpSource.OperApplyId = operApply.OperId;
                tmpSource.ColDoctor.ID = loginPerson.ID;
                tmpSource.OperEmp.Name = loginPerson.Name;
                tmpSource.SendTime = dtpColTime.Value; //new DateTime(dtpColTime.Value.Year,dtpColTime.Value.Month,dtpColTime.Value.Day,Convert.ToInt32(nudHour.Value.ToString()),Convert.ToInt32(nudMin.Value.ToString()),0);
                tmpSource.Ext2 = cmbDiagnose.Text.Trim();
                if (grpReason.Visible)
                {
                    tmpSource.Commet += grpReason.Text + " ";
                    if (rbtBreak.Checked)
                    {
                        tmpSource.Commet += rbtBreak.Text;
                    }
                    if (rbtNotGet.Checked)
                    {
                        tmpSource.Commet += rbtNotGet.Text;
                    }
                    if (rbtOther.Checked)
                    {
                        tmpSource.Commet += rbtOther.Text;
                    }
                }
                operApply.HadCollect = "1";
                operApply.GetOperInfoTime = DateTime.Now;
                if (operApplyMgr.UpdateOperApply(operApply) == -1)
                {
                    MessageBox.Show("�����ռ���־ʧ��");
                    return;
                }
                GenerateBarCode(tmpSource);                
                txtBarCode.Text = "";
                cmbDisType.Text = "";
                cmbDiagnose.Text = "";
                grpReason.Visible = false;
                nudCount.Value = 1;
                nudNoCount.Value = 1;
            }
            catch
            {
                MessageBox.Show("��ȡ��Ϣʧ��");
                return;
            }
        } 

        /// <summary>
        /// �����װ�걾����װ�걾��Ϣ
        /// </summary>
        /// <returns></returns>
        private int SaveSource()
        {
            if (dicSourcePlanList.Count <= 0)
            {                
                return 0;
            }

            try
            {
                //�����ϲ���û��ά�������Զ�����
                string curDiag = cmbDiagnose.Text.TrimEnd().TrimStart();
                if (curDiag != "")
                {
                    if (!dicDiagToPos.ContainsKey(curDiag))
                    {
                        this.SaveDiagToDis();
                        dicDiagToPos = disTypeManage.GetDiagToDis();
                    }
                }
            }
            catch
            { }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            specSourceManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            specSourcePlanManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                SpecInOper specInOper = new SpecInOper();
                //subList.Clear();
                foreach (KeyValuePair<SpecSource, List<SpecSourcePlan>> tmp in dicSourcePlanList)
                {
                    if (tmp.Key.SpecId > 0)
                    {
                        if (specSourceManage.UpdateSpecSource(tmp.Key) <= 0)
                        {
                            MessageBox.Show("����ʧ�ܣ�", "��������");
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            return -1;

                        }
                    }
                    if (tmp.Key.SpecId <= 0)
                    {
                        string sequence = "";
                        specSourceManage.GetNextSequence(ref sequence);
                        tmp.Key.SpecId = Convert.ToInt32(sequence);
                        if (specSourceManage.InsertSpecSource(tmp.Key) <= 0)
                        {
                            ClearData();
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            //foreach (KeyValuePair<SpecSource, List<SpecSourcePlan>> tmp1 in dicSourcePlanList)
                            //{
                            //    tmp1.Key.SpecId = 0;
                            //}
                            MessageBox.Show("����ʧ�ܣ�", "��������");
                            return -1;

                        }
                    }                    
                    foreach (SpecSourcePlan plan in tmp.Value)
                    {
                        plan.SpecID = tmp.Key.SpecId;
                        plan.TumorType = Convert.ToInt32(Constant.TumorType.����).ToString();
                        string planSequence = "";
                        planSequence = specSourcePlanManage.GetNextSequence();
                        plan.PlanID = Convert.ToInt32(planSequence);
                        if (specSourcePlanManage.InsertSourcePlan(plan) <= 0)
                        {
                            ClearData();
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show("����ʧ�ܣ�", "��������");
                            //foreach (KeyValuePair<SpecSource, List<SpecSourcePlan>> tmp1 in dicSourcePlanList)
                            //{
                            //    tmp1.Key.SpecId = 0;
                            //}
                            return -1; 
                        }                        
                        specInOper.DisTypeId = tmp.Key.DiseaseType.DisTypeID.ToString();
                        specInOper.LoginPerson = loginPerson;
                        specInOper.Trans = trans;
                        specInOper.SpecTypeId = plan.SpecType.SpecTypeID.ToString();
                        specInOper.SetTrans();

                        int saveResult = specInOper.SaveSubSpec(plan, ref subList);
                        if (saveResult == -1)
                        //if (SaveSubSpec(plan,tmp.Key.DiseaseType.DisTypeID.ToString()) == -1)
                        {                            
                            ClearData();
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show("����ʧ�ܣ�", "��������");
                            //foreach (KeyValuePair<SpecSource, List<SpecSourcePlan>> tmp1 in dicSourcePlanList)
                            //{
                            //    tmp1.Key.SpecId = 0;
                            //}
                            return -1; 
                        }
                        if (saveResult == -2)
                        {
                            ClearData();
                            //foreach (KeyValuePair<SpecSource, List<SpecSourcePlan>> tmp1 in dicSourcePlanList)
                            //{
                            //    tmp1.Key.SpecId = 0;
                            //}                            
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show("���ұ걾��ʧ�ܣ�", "��������");
                            return -1; 
                        }
                    }
                }         
                FS.FrameWork.Management.PublicTrans.Commit();                
                dicSourcePlanList = new Dictionary<SpecSource, List<SpecSourcePlan>>();
                //saved = true;
                this.useFullBoxList = specInOper.UseFullBoxList;
                MessageBox.Show("����ɹ���", "��������");                
                neuSpread1_Sheet1.Rows.Count = 0;//.Clear();
                neuSpread1_Sheet1.Rows.Add(0, 1);
                dtpColTime.Focus();
                //��ոñ�־
                needSaveDiagnose = false;
                customDis = false;
                cmbDiagnose.Text = "";
                return 1;
            }
            catch
            {
                ClearData();
                //foreach (KeyValuePair<SpecSource, List<SpecSourcePlan>> tmp in dicSourcePlanList)
                //{
                //    tmp.Key.SpecId = 0;
                //}

                FS.FrameWork.Management.PublicTrans.RollBack();;
                MessageBox.Show("����ʧ�ܣ�", "��������");
                return -1;
            }
           
        }

        private void txtBarCode_TextChanged(object sender, EventArgs e)
        {
            string barCode = txtBarCode.Text.Trim();
            
            if (barCode.Length == 12)
            {
                //txtBarCode_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                //GetApplyInfoAndSubBarCode(barCode.PadLeft(12, '0'));
            }
        }

        private void ucBloodBarcode_Load(object sender, EventArgs e)
        {
            //loginPerson = FS.NFC.Management.Connection.Operator as FS.HISFC.Object.Base.Employee;
            //FS.HISFC.Management.Manager.DepartmentStatManager manager = new FS.HISFC.Management.Manager.DepartmentStatManager();
            ////alDepts = manager.GetMultiDept(loginPerson.ID);
            //ArrayList alDepts = new ArrayList();
            //alDepts = manager.LoadAll();           
            //cmbOperDept.AddItems(alDepts);
            //LoadOperApply();
            //LoadSpecSource();
            //InitSpecType();
            //GenerateColumn();
            //DisTypeBinding();
            //dicDiagToPos = disTypeManage.GetDiagToDis();
            ////������Ժ���
            //FS.HISFC.Management.Manager.Constant conMgr = new FS.HISFC.Management.Manager.Constant();
            //ArrayList arrDiagnoseList = conMgr.GetList("DiagnosebyNurse");
            //if (arrDiagnoseList != null && arrDiagnoseList.Count > 0)
            //{
            //    cmbDiagnose.AddItems(arrDiagnoseList);
            //}
            //dtpColTime.Focus();
        }

        protected override void OnLoad(EventArgs e)
        {
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            FS.HISFC.BizLogic.Manager.Department manager = new FS.HISFC.BizLogic.Manager.Department();
            //alDepts = manager.GetMultiDept(loginPerson.ID);
            ArrayList alDepts = new ArrayList();
            alDepts = manager.GetDeptmentByType("I");//.GetMultiDept(loginPerson.ID);
            cmbOperDept.AddItems(alDepts);
            LoadOperApply();
            LoadSpecSource();
            InitSpecType();
            GenerateColumn();
            DisTypeBinding();
            dicDiagToPos = disTypeManage.GetDiagToDis();
            //������Ժ���
            FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
            ArrayList arrDiagnoseList = conMgr.GetList("DiagnosebyNurse");
            if (arrDiagnoseList != null && arrDiagnoseList.Count > 0)
            {
                cmbDiagnose.AddItems(arrDiagnoseList);
            }
            base.OnLoad(e);
            dtpColTime.Focus();

        }

        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Enter && txtBarCode.Text.Trim() != "")
            {
                cmbDisType.Text = "";
                //dtpColTime.Focus();
                string barCode = txtBarCode.Text.Trim();
                try
                {
                    needSaveDiagnose = false;
                    customDis = false;
                    int id = Convert.ToInt32(barCode);
                    GetApplyInfoAndSubBarCode(id.ToString().PadLeft(12, '0'));
                }
                catch
                {
                    MessageBox.Show("��ȡ����ʧ��");
                    return;
                }
            }
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("�ɼ�������", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X�½�, true, false, null);
            //this.toolBarService.AddToolButton("��ӡλ����Ϣ", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.A��ӡ, true, false, null);
            this.toolBarService.AddToolButton("ȡ������", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);
            this.toolBarService.AddToolButton("������Ϣ��ѯ", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ, true, false, null);

            this.toolBarService.AddToolButton("����ȡ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.F����, true, false, null);

            return this.toolBarService;
            //return base.OnInit(sender, neuObject, param);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "�ɼ�������":

                    GenerateColForm();
                    break;
                case "ȡ������":
                    this.CancelSpec();
                    Query(null, null);
                    break;
                case "������Ϣ��ѯ":
                    ucPatInfoQuery ucPatQuery = new ucPatInfoQuery();
                    Size size = new Size();
                    size.Height = 800;
                    size.Width = 1200;
                    ucPatQuery.Size = size;
                    FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucPatQuery, FormBorderStyle.FixedSingle, FormWindowState.Normal);
                    break;
                case "����ȡ��":
                    this.ReCancelSpec();
                    Query(null, null);
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        public override int Save(object sender, object neuObject)
        {
            //this.SaveSource();
            return base.Save(sender, neuObject);
        }

        public override int Print(object sender, object neuObject)
        {
            //if (barCodeList.Count <= 0)
            //{
            //    MessageBox.Show("û����Ҫ��Ӧ������");
            //    return 0; 
            //}
            //if (!saved)
            //{
            //    DialogResult diagResult = MessageBox.Show("��ӡ֮ǰ�뱣����Ϣ�����δ���棬���������ظ���������!","�����ӡ", MessageBoxButtons.YesNo);
            //    if (diagResult == DialogResult.Yes)
            //    {
            //        Save(sender, neuObject);
            //    }                
            //}
            //int result = PrintLabel.Print2DBarCode(barCodeList, sequenceList, "", disTypeList, numList) ;
            //if (result == -2)
            //{
            //    MessageBox.Show("���Ӵ�ӡ��ʧ��");
            //    return -1;
            //}
            //if (result == -1)
            //{
            //    MessageBox.Show("��ӡʧ��");
            //    return -1;
            //}
            //barCodeList = new List<string>();
            //sequenceList = new List<string>();
            //disTypeList = new List<string>();
            //numList = new List<string>();
            //neuSpread1_Sheet1.Rows.Count = 0;//.Clear();
            //neuSpread1_Sheet1.Rows.Add(0, 1);

            SpecOutOper operOut = new SpecOutOper();             
            operOut.PrintTitle = "�걾λ����Ϣ";
            try
            {
                operOut.PrintOutSpec(subList, null);
                subList = new List<SubSpec>();
            }
            catch
            {
                MessageBox.Show("��ӡλ����Ϣʧ�ܣ�", "��������");                
                return -1;
            }
            return base.Print(sender, neuObject);
        }

        private void nudCount_ValueChanged(object sender, EventArgs e)
        {

            try
            {
                if (Convert.ToInt32(nudCount.Value) == 0)
                {
                    MessageBox.Show("����дû���յ���ԭ��");
                    grpReason.Text = "δ�յ�" + "����Ѫ��ԭ��:";
                    grpReason.Visible = true;
                    rbtBreak.Focus();// txtReason.Focus();
                    //�������ѪΪ0ʱ �Զ���ѡ��Ѫ���Ͱ�ϸ���������걾�����˵��Ϊ�˲��ñ걾��λ�÷��ң��������۵ķ����ǲ�����û�� ��Ҫ���װ
                    if (!cmbDisType.Text.Contains("����"))
                    {
                        return;
                    }
                    foreach (Control c in flpSubDetail.Controls)
                    {
                        foreach (Control sc in c.Controls[0].Controls)
                        {
                            if (sc.Text == "Ѫ��" || sc.Text == "��ϸ��")
                            {
                                CheckBox chk = sc as CheckBox;
                                chk.Checked = false;
                            }
                        }
                    }

                }
                if (nudCount.Value >= 1)
                {
                    foreach (Control c in flpSubDetail.Controls)
                    {
                        foreach (Control sc in c.Controls[0].Controls)
                        {
                            if (sc.Text == "Ѫ��" || sc.Text == "��ϸ��")
                            {
                                CheckBox chk = sc as CheckBox;
                                chk.Checked = true;
                            }
                        }
                    }
                    grpReason.Visible = false;
                }
            }
            catch
            { }
        }

        private void nudNoCount_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(nudNoCount.Value) == 0)
                {
                    MessageBox.Show("����дû���յ���ԭ��");
                    grpReason.Text = "δ�յ�" + "�ǿ���Ѫ��ԭ��:";
                    grpReason.Visible = true;
                    rbtBreak.Focus();
                    //�������ѪΪ0ʱ �Զ���ѡ��Ѫ�壬�����걾�����˵��Ϊ�˲��ñ걾��λ�÷��ң��������۵ķ����ǲ�����û�� ��Ҫ���װ
                    //�����財�� 
                    if (!cmbDisType.Text.Contains("����"))
                    {
                        return;
                    }
                    foreach (Control c in flpSubDetail.Controls)
                    {
                        foreach (Control sc in c.Controls[0].Controls)
                        {
                            if (sc.Text == "Ѫ��" || sc.Text == "��ϸ��")
                            {
                                CheckBox chk = sc as CheckBox;
                                chk.Checked = false;
                            }
                        }
                    }

                }
                if (nudNoCount.Value >= 1)
                {
                    foreach (Control c in flpSubDetail.Controls)
                    {
                        foreach (Control sc in c.Controls[0].Controls)
                        {
                            if (sc.Text == "Ѫ��" || sc.Text == "��ϸ��")
                            {
                                CheckBox chk = sc as CheckBox;
                                chk.Checked = true;
                            }
                        }
                    }
                    grpReason.Visible = false;                    
                }
            }
            catch
            { }
        }

        private void cmbDisType_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Control c in flpSubDetail.Controls)
            {
                ucSpecSourceForBlood ub = c as ucSpecSourceForBlood;
                ub.DisTypeName = cmbDisType.Text;
                //ub.DisTypeId = cmbDisType.SelectedValue;
            }
        }

        private void cmbDisType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtBarCode.Text.Trim() != "")
                {
                    customDis = true;
                    string barCode = txtBarCode.Text.Trim();
                    try
                    {
                        needSaveDiagnose = false;
                        int id = Convert.ToInt32(barCode);
                        GetApplyInfoAndSubBarCode(id.ToString().PadLeft(12, '0'));
                    }
                    catch
                    {
                        MessageBox.Show("��ȡ����ʧ��");
                        return;
                    }
                }
                else
                {
                    txtBarCode.Focus();
                }
            }
        }

        public override int Exit(object sender, object neuObject)
        {
            //if (!saved && dicSourcePlanList!=null & dicSourcePlanList.Count>0)
            //{
            //    //DialogResult diagResult = MessageBox.Show("��Ϣû�б���", "�����ӡ", MessageBoxButtons.YesNo);
            //    //if (diagResult == DialogResult.Yes)
            //    //{
            //        Save(sender, neuObject);
            //    //}
            //}
            return base.Exit(sender, neuObject);
        }

        public override int Query(object sender, object neuObject)
        {
            for (int i = 0; i < tvSpec.Nodes.Count; i++)
            {
                tvSpec.Nodes[i].Nodes.Clear();
            }
            LoadSpecSource();
            LoadOperApply();
            return base.Query(sender, neuObject);
        }

        private void dtpColTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtBarCode.Text.Trim() != "")
            {
                string barCode = txtBarCode.Text.Trim();
                try
                {
                    needSaveDiagnose = false;
                    customDis = false;
                    int id = Convert.ToInt32(barCode);
                    GetApplyInfoAndSubBarCode(id.ToString().PadLeft(12, '0'));
                }
                catch
                {
                    MessageBox.Show("��ȡ����ʧ��");
                    return;
                }
            }
        }

        private void cmbDiagnose_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbDiagnose.Text.Trim() != "")
                {
                    customDis = true;
                }
                if (dicDiagToPos.ContainsKey(cmbDiagnose.Text) && customDis)
                {
                    cmbDisType.Tag = dicDiagToPos[cmbDiagnose.Text];

                    //cmbDisType.Text = dicDiagToPos[cmbDiagnose.Text];
                }
                else
                {
                    cmbDisType.Text = "";
                }
            }
            catch
            { }
        }

        private void dtpColTime_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBarCode.Focus();
            //    nudHour.Select(0,2);//.Select();
            //    nudHour.Focus();
            }
        }

        private void nudHour_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                nudMin.Select(0,2);
                nudMin.Focus();
            }
        }

        private void nudMin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBarCode.Focus();
            }
        }

        private void nudHour_ValueChanged(object sender, EventArgs e)
        {
            if(Convert.ToInt32(nudHour.Value.ToString())>=24)
            {              
                nudHour.Value = 23;
            }
            if (Convert.ToInt32(nudMin.Value.ToString()) >= 59)
            {               
                nudMin.Value = 59;
            }
        }
    }
}
