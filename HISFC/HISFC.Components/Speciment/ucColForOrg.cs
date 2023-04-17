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
    public partial class ucColForOrg : UserControl
    {
        private SpecSourcePlanManage specPlanManage;       
        private SpecTypeManage specTypeManage;
        private SubSpecManage subSpecManage;
        private SpecBarCodeManage barCodeManage;

        private SpecSource specSource;
        private SpecBarCode specBarCode;
        private SpecSourcePlan specPlan;

        private List<string> barCodeList = new List<string>();
        private List<string> sequenceList = new List<string>();
        private List<string> disTypeList = new List<string>();
        private List<string> numList = new List<string>();
        private List<string> tumorTypeList = new List<string>();

        private int barCodeCount;
        private string sequence = "";
        private string seqFromSource = "";
        private bool needUpdateSeq = false;

        private FS.FrameWork.Models.NeuObject project = new FS.FrameWork.Models.NeuObject();
        #region ����
        /// <summary>
        /// �������ʣ����� T������ N���ܰ� L 
        /// </summary>
        private string tumorKind = "";
        public string TumorKind
        {
            set
            {
                tumorKind = value;
            }
        }

        private string tumorName = "";
        public string TumorName
        {
            set
            {
                tumorName = value;
            }
        }

        /// <summary>
        /// �걾�Ĵ洢
        /// </summary>
        public SpecSourcePlan SpecSourcePlan
        {
            get
            {
                GetSpecSourePlan();
                return specPlan;
            }
            set
            {
                specPlan = value;
            }
        }

        /// <summary>
        /// ���ò�������
        /// </summary>
        private string disTypeName = "";
        public string DisTypeName
        {
            set
            {
                disTypeName = value;
            }
        }

        /// <summary>
        /// ԭ�걾ID
        /// </summary>
        private int specId = 0;
        public int SpecId
        {
            set
            {
                specId = value;
            }
        }

        /// <summary>
        /// ���һ�������ж����֯����Ҫ����������ԣ�Ҫ��һ��������ֻ����һ�����к�
        /// </summary>
        public string SeqFromSource
        {
            set
            {
                seqFromSource = value;
            }
        }

        /// <summary>
        /// �������ﲿλ����
        /// </summary>
        private string tumorPosCode = "";
        public string TumorPosCode
        {
            set
            {
                tumorPosCode = value;               
            }
        }

        private string tumorPos = "";
        public string TumorPos
        {
            set
            {
                tumorPos = value;
            }
        }

        /// <summary>
        /// ��ǰ�걾�����к�
        /// </summary>
        public string Seq
        {
            get
            {
                try
                {
                    return lsbBarCode.Items[0].ToString().Substring(0, 6);
                }
                catch
                {
                    return "";
                }
            }
        }
        public int C863
        {
            get
            {
                if (chk863.Checked)
                {
                    return Convert.ToInt32(nud863.Value);
                }
                else
                {
                    return 0;
                }
            }
        }

        public int C115
        {
            get
            {
                if (chk115.Checked)
                {
                    return Convert.ToInt32(nud115.Value);
                }
                else
                {
                    return 0;
                }
            }
        }

        private Dictionary<string, string> useAlone = new Dictionary<string, string>();
        public Dictionary<string, string> UseAlone
        {
            get
            {
                return useAlone;
            }
            set
            {
                useAlone = value;
            }
        }

        #endregion

        public ucColForOrg()
        {
            InitializeComponent();

            specPlanManage = new SpecSourcePlanManage();
            specTypeManage = new SpecTypeManage();
            subSpecManage = new SubSpecManage();
            barCodeManage = new SpecBarCodeManage();

            specSource = new SpecSource();
            specPlan = new SpecSourcePlan();
            specBarCode = new SpecBarCode();

            barCodeCount = 1;
            //ͨ��tag���ĵ�ǰ��Ϣ
            if (this.Tag == null)
                this.Tag = 0;
        }

        private void GetSpecSourePlan()
        {
            specPlan.Count = Convert.ToInt32(nudCount.Value);
            specPlan.Unit = "��";
            specPlan.StoreCount = specPlan.Count;
            if (chkOnlySelf.Checked)
                specPlan.LimitUse = txtDocName.Text;
            else
                specPlan.LimitUse = "";
            specPlan.SubSpecCodeList.Clear();
            foreach (string s in lsbBarCode.Items)
            {
                specPlan.SubSpecCodeList.Add(s);
            }
        }

        /// <summary>
        /// �鿴�Ƿ����barcode�ı걾
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        private bool IsExsited(string barCode)
        {
            SubSpec s = subSpecManage.GetSubSpecById("", barCode);
            if (s == null || s.SpecId == 0)
                return false;
            return true;
        }

        /// <summary>
        /// �Զ��γ�������
        /// </summary>
        private void AutoGenerateBarCode()
        {
            int count = Convert.ToInt32(nudCount.Value);
            bool containX = false;

            foreach (string s in lsbBarCode.Items)
            {
                string lastLabel = s.Substring(s.Length - 1, 1);
                if (lastLabel == "X")
                    containX = true;
            }

            int barCount = lsbBarCode.Items.Count;
            if (chkAutoGen.Checked && chkName.Checked)
            {
                if (lsbBarCode.Items.Count == count)
                {
                    //�����Ҫ��оƬ�������������������X�����ı�ǩ
                    if (chkChip.Checked && containX)
                    {
                        return;
                    }
                    //�����Ҫ��оƬ������������û�м�����X�����ı�ǩ
                    if (chkChip.Checked && !containX)
                    {
                        string tmp = lsbBarCode.Items[count - 1].ToString();
                        tmp = tmp.Substring(0, tmp.Length - 1);
                        lsbBarCode.Items[0] = tmp + "X";
                    }
                    return;
                }
                if (count < barCount)
                {
                    for (int i = 0; i < barCount - count; i++)
                    {
                        lsbBarCode.Items.RemoveAt(lsbBarCode.Items.Count - 1);
                    }
                    return;
                }
                if (count > barCount)
                {
                    int num = 0;
                    string barCodePre = "";
                    sequence = "";
                    if (seqFromSource != "")
                    {
                        sequence = seqFromSource;
                        needUpdateSeq = false;
                    }
                    //���������
                    if (lsbBarCode.Items.Count > 0 && sequence == "")
                    {
                        foreach (string s in lsbBarCode.Items)
                        {
                            string firstLabel = s.Substring(0, 1);
                            barCodePre = s.Substring(0, s.Length - 1);
                            if (System.Text.RegularExpressions.Regex.Match(s.Substring(s.Length - 1, 1), "^[a-zA-Z]+$").Success)
                            {
                                continue;
                            }
                            int lastNum = Convert.ToInt32(s[s.Length - 1].ToString());
                            if (lastNum > num)
                            {
                                num = lastNum;
                            }

                            if (System.Text.RegularExpressions.Regex.Match(firstLabel, "^\\d+$").Success)
                            {
                                sequence = s.Substring(0, 6);
                            }

                            if (System.Text.RegularExpressions.Regex.Match(firstLabel, "^[a-zA-Z]+$").Success)
                            {
                                sequence = s.Substring(1, 6);
                            }
                        }
                    }
                    else
                    {
                        if (specId > 0 && sequence == "")
                        {
                            ArrayList arr = subSpecManage.GetSubSpecBySpecId(specId.ToString());
                            if (arr != null && arr.Count > 0)
                            {
                                try
                                {
                                    SubSpec sub = new SubSpec();
                                    foreach (SubSpec s in arr)
                                    {
                                        if (s.SubBarCode.Trim() != "")
                                        {
                                            sub =s ; 
                                            break;
                                        }
                                    }
                                    string firstLabel = sub.SubBarCode.Substring(0, 1);
                                    if (System.Text.RegularExpressions.Regex.Match(firstLabel, "^\\d+$").Success)
                                    {
                                        sequence = sub.SubBarCode.Substring(0, 6);
                                    }

                                    if (System.Text.RegularExpressions.Regex.Match(firstLabel, "^[a-zA-Z]+$").Success)
                                    {
                                        sequence = sub.SubBarCode.Substring(1, 6);
                                    }
                                    needUpdateSeq = false;
                                }
                                catch
                                { }
                            }
                        }

                        SpecBarCode tmpSubBarCode = barCodeManage.GetSpecBarCode(disTypeName, chkName.Text);
                        if (tmpSubBarCode == null)
                        {
                            MessageBox.Show("��������ʧ�ܣ��鲻������Ϊ" + disTypeName + " �걾����Ϊ" + chkName.Text + " ����Ϣ");
                            return;
                        }
                        if (sequence == "")
                        {
                            sequence = tmpSubBarCode.Sequence;
                            needUpdateSeq = true;
                        }
                        barCodePre = sequence.PadLeft(6, '0') + tmpSubBarCode.DisAbrre + this.tumorPosCode + tumorKind + tmpSubBarCode.SpecTypeAbrre;
                        //needUpdateSeq = true;
                    }
                    num++;
                    if (chkChip.Checked && !containX)
                    {
                        string barCode = barCodePre + "X";
                        if (IsExsited(barCode))
                        {
                            MessageBox.Show("�����Ƿ����ͬ�����Եı걾");
                            return;
                        }
                        lsbBarCode.Items.Add(barCode);
                        barCodeList.Add(barCode);
                        numList.Add("X");
                        disTypeList.Add(this.disTypeName);
                        sequenceList.Add(Convert.ToInt32(sequence).ToString());
                        tumorTypeList.Add(tumorName);
                        barCount++;
                    }
                    for (int i = 0; i < count - barCount; i++)
                    {
                        string barCode = barCodePre + (num + i).ToString();
                        if (IsExsited(barCode))
                        {
                            MessageBox.Show("�����Ƿ����ͬ�����Եı걾");
                            return;
                        }
                        lsbBarCode.Items.Add(barCode);
                        barCodeList.Add(barCode);
                        numList.Add((num + i).ToString());
                        disTypeList.Add(this.disTypeName);
                        tumorTypeList.Add(tumorPos);
                        sequenceList.Add(Convert.ToInt32(sequence).ToString());
                    }
                }
            }
            else
            {
                lsbBarCode.Items.Clear();
            }
        }

        /// <summary>
        /// ����ҳ����Ϣ
        /// </summary>
        /// <param name="specTypeId">�걾����Id</param>
        /// <param name="count">�ñ걾����ID�洢������</param>
        /// <param name="limitUse">�Ƿ�����ʹ��</param>
        public void GetColOrgInfo(string specTypeId,int count,string limitUse,List<string> subSpec)
        {
            if (this.specPlan.PlanID <= 0)
            {
                chkAutoGen.Checked = false;
                chkName.Checked = false;
                return;
            }
            lsbBarCode.Items.Clear();
            subSpec.Remove("");
            specTypeId = chkName.Tag.ToString();
            chkName.Text = specTypeManage.GetSpecTypeById(specTypeId).SpecTypeName;
            chkName.Checked = true;
            nudCount.Value = count;
            if (limitUse.Trim() != "")
            {
                chkOnlySelf.Checked = true;
                txtDocName.Text = limitUse;
            }
            foreach (string s in subSpec)
                lsbBarCode.Items.Add(s);
            if (subSpec.Count > 1)
                barCodeCount = subSpec.Count;
        }

        /// <summary>
        /// ���������У��
        /// </summary>
        /// <returns></returns>
        public bool BarCodeCountValidate()
        {
            if (lsbBarCode.Items.Count < Convert.ToInt32(nudCount.Value))
            {
                DialogResult ds = MessageBox.Show("�������������㣬���ǿ�ƽ��У���������Ϣ���󣡼�����", "����", MessageBoxButtons.YesNo);
                if (ds == DialogResult.No)
                {
                    return false;
                }
            }
            if (nud863.Value > nudCount.Value)
            {
                MessageBox.Show("863�������ܴ�������!");
                return false;
            }
            return true;
        }

        /// <summary>
        /// ����sequence
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        public int UpdateSeq(System.Data.IDbTransaction trans)
        {
            barCodeManage.SetTrans(trans);
            if (needUpdateSeq)
            {
                if (barCodeManage.UpdateBarCode(disTypeName, chkName.Text, (Convert.ToInt32(sequence) + 1).ToString()) <= 0)
                {
                    return -1;
                }
            }
            needUpdateSeq = false;
            return 1;
        }

        public int UpdateBarCode(int subSpecId, int index, System.Data.IDbTransaction trans)
        {
            subSpecManage.SetTrans(trans);
            barCodeManage.SetTrans(trans);

            string barCode = "";
            if (lsbBarCode.Items.Count > index)
            {
                barCode = lsbBarCode.Items[index] == null ? "" : (lsbBarCode.Items[index]).ToString();
            }
            string sql = " UPDATE SPEC_SUBSPEC SET SPEC_SUBSPEC.SUBBARCODE='" + barCode + "' WHERE SUBSPECID = " + subSpecId.ToString();
            if (subSpecManage.UpdateSubSpec(sql) <= 0)
            {
                return -1;
            }
            if (needUpdateSeq)
            {
                if (barCodeManage.UpdateMaxSeqByDisAndType(disTypeName, "2", (Convert.ToInt32(sequence) + 1).ToString()) <= 0)
                {
                    return -1;
                }
            }
            needUpdateSeq = false;
            return 1;            
        }

        /// <summary>
        /// ��ȡ�ƻ�ȡ�ı걾�б�
        /// </summary>
        /// <returns>SpecSourcePlan �б�</returns>
        public void SetSpecType(FS.HISFC.Models.Speciment.SpecType st)
        {
            if (st == null)
            {
                return;
            }
            if (st.IsShow == "0" || st.DefaultCnt == 0)
            {
                chkName.Checked = false;
            }
            nudCount.Value = st.DefaultCnt;
            chkName.Text = st.SpecTypeName;
            chkName.Tag = st.SpecTypeID;
            //ArrayList arrPlan = new ArrayList();
            //arrPlan = specTypeManage.GetSpecByOrgName(orgName);
            ////foreach (FS.HISFC.Object.Speciment.SpecType s in arrSpecTypeList)
            ////{
            ////    specPlan = new SpecSourcePlan();
            ////    specPlan.SpecType = s;
            ////    arrPlan.Add(specPlan);
            ////}
            ////return arrPlan;
            //return arrPlan;
        }

        private void txtBarCode_TextChanged(object sender, EventArgs e)
        {
                     
        }

        private void lbBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                lsbBarCode.Items.Remove(lsbBarCode.SelectedItem);               
                barCodeCount--;
            }
        }

        private void ucColForOrg_Load(object sender, EventArgs e)
        {
            lsbBarCode.Items.Remove("");
            //ֻ���������ʾоƬ
            if (chkName.Tag != null && chkName.Tag.ToString() == "7")
            {
                chkChip.Visible = true;
            }
            else
            {
                chkChip.Visible = false;
            }
        }

        private void chkName_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkName.Checked)
            {
                txtBarCode.Enabled = false;
                lsbBarCode.Enabled = false;
                chk115.Checked = false;
                chk863.Checked = false;
                chk115.Enabled = false;
                chk863.Enabled = false;
            }
            if (chkName.Checked)
            {
                txtBarCode.Enabled = true;
                lsbBarCode.Enabled = true;
                chk863.Enabled = true;
                chk115.Enabled = true;
            }
        }

        private void chkGetCode_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoGen.Checked)
                {
                    //specBarCode = barCodeManage.GetSpecBarCode(disTypeName, chkName.Text);
                    AutoGenerateBarCode();
                }
            }
            catch
            { }
            if (!chkAutoGen.Checked)
            {
                //lbBarCode.Items.Clear();
            }
        }

        private void nudCount_ValueChanged(object sender, EventArgs e)
        {
            int oldCount = lsbBarCode.Items.Count;
            int newCount = Convert.ToInt32(nudCount.Value);
            int diff = newCount - oldCount;
            if (diff > 0 && chkAutoGen.Checked)
            {
                this.AutoGenerateBarCode();
            }
            if (diff < 0)
            {
                diff = Math.Abs(diff);
                for (int i = 0; i < diff; i++)
                {
                    lsbBarCode.Items.RemoveAt(lsbBarCode.Items.Count - 1);
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (lsbBarCode.Items.Count == 0)
                return;
            if (barCodeList.Count == 0)
            {
                List<string> barCode = new List<string>();
                foreach (string s in lsbBarCode.SelectedItems)
                {
                    barCode.Add(s);
                }
                PrintLabel.PrintBarCode(barCode);
            }
            else
            {
                PrintLabel.Print2DBarCodeOrg(barCodeList, sequenceList, tumorTypeList, disTypeList, numList);
            }
        }

        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string barCode = txtBarCode.Text.Trim();
                if (barCodeCount <= Convert.ToInt32(nudCount.Value) && chkName.Checked)
                {
                    //��ֹ�����ظ���������
                    int index = 0;
                    foreach (string s in lsbBarCode.Items)
                    {
                        if (s == barCode)
                            break;
                        index++;
                    }
                    if (index <= lsbBarCode.Items.Count)
                        lsbBarCode.Items.Add(barCode);
                    barCodeCount++;
                    txtBarCode.Text = "";
                }
            }
        }

        private void nud863_ValueChanged(object sender, EventArgs e)
        {
            if (nudCount.Value < nud863.Value)
            {
                MessageBox.Show("863��115�걾�������ܴ�������");
                NumericUpDown tmp = sender as NumericUpDown;
                tmp.Value = 1;
            }
        }

        private void chk115_CheckedChanged(object sender, EventArgs e)
        {
            if (chk115.Checked)
            {
                chk863.Checked = false;
            }
        }

        private void chk863_CheckedChanged(object sender, EventArgs e)
        {
            if (chk863.Checked)
            {
                chk115.Checked = false;
            }
        }

        private void btnSetUseAlone_Click(object sender, EventArgs e)
        {
            try
            {
                frmUseAlone frm = new frmUseAlone();

                if (this.useAlone.Count > 0)
                {
                    frm.DicUsePro = useAlone;
                    frm.GetUseProperty();
                }
                else if (specPlan.PlanID > 0)
                {
                    this.useAlone = this.subSpecManage.GetUseAlonePro(specPlan.PlanID.ToString());
                    frm.DicUsePro = useAlone;
                    frm.GetUseProperty();
                }
                else
                {
                    List<string> tmpList = new List<string>();
                    foreach (string s in lsbBarCode.Items)
                    {
                        tmpList.Add(s);
                    }
                    frm.BarCodeList = tmpList;

                 }

                frm.OnUseProperty += new frmUseAlone.UseProperty(frm.SetUseProperty);
                frm.ShowDialog();
                this.useAlone = frm.DicUsePro;
            }
            catch
            { }
        }

        public int UpdateUserPro(System.Data.IDbTransaction trans)
        {
            if (trans != null)
            {
                subSpecManage.SetTrans(trans);
            }
            return this.subSpecManage.UpdateUseAlonePro(useAlone);
        }

        private void btnProject_Click(object sender, EventArgs e)
        {
            Setting.frmSpecProject frmPjt = new Setting.frmSpecProject();
            frmPjt.StartPosition = FormStartPosition.CenterScreen;
            frmPjt.FrmType = "Input";
            if (chk863.Checked)
            {
                this.project.ID = "8";
                this.project.Name = nud863.Value.ToString();
            }
            if (chk115.Checked)
            {
                this.project.ID = "1";
                this.project.Name = nud115.Value.ToString();
            }
            frmPjt.Original = this.project;
            if (!((Form)frmPjt).Visible)
            {
                frmPjt.ShowDialog();
            }
            if (!string.IsNullOrEmpty(frmPjt.RtObj.ID))
            {
                this.project = frmPjt.RtObj;
                if (this.project.ID == "8")
                {
                    chk863.Checked = true;
                    nud863.Value = Convert.ToDecimal(this.project.Name);
                }
                if (this.project.ID == "1")
                {
                    chk115.Checked = true;
                    nud115.Value = Convert.ToDecimal(this.project.Name);
                }
            }
            else
            {
                chk863.Checked = false;
                chk115.Checked = false;
                this.project = frmPjt.RtObj;
            }
        }
    }
}
