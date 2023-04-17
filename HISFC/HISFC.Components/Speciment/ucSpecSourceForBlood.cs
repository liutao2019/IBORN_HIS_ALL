using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;

namespace FS.HISFC.Components.Speciment
{
    public partial class ucSpecSourceForBlood : UserControl
    {
        private SpecSourcePlanManage specPlanManage;
        private SpecTypeManage specTypeManage;
        private SubSpecManage subSpecManage;
        private SpecBoxManage specBoxManage;
        private SpecSourcePlan specPlan;
        private SpecBarCodeManage barCodeManage;
        private FS.HISFC.Models.Base.Employee loginPerson;
        private System.Data.IDbTransaction trans;
        private string disTypeId;
        private string flag;
        private int specId;
        private int barCodeCount;
        private string disTypeName;
        //��ǰ�������ԣ����޸�Or�½�
        private string curOperPro;
        private List<SpecBox> curUseBoxList = new List<SpecBox>();
        private bool inited = false;


        private List<string> barCodeList;
        private List<string> sequenceList;
        private List<string> disTypeList;
        private List<string> numList;
        private List<string> specTypeList;

        //���к�
        private string sequence;
        //�Ƿ���Ҫ���µ�ǰʹ�õ����к�
        private bool needUpdateSeq = false;

        #region ����
        /// <summary>
        /// �����־Ϊ�գ�����ر걾���ͣ����򲻼���
        /// </summary>
        public string Flag
        {
            get
            {
                return flag;
            }
            set
            {
                flag = value;
            }
        }

        /// <summary>
        /// ��ǰʹ�õı걾��
        /// </summary>
        public List<SpecBox> UseFullBoxList
        {
            get
            {
                return curUseBoxList;
            }
            set
            {
                curUseBoxList = value;
            }
        }

        /// <summary>
        /// �����ı걾Id
        /// </summary>
        public int SpecId
        {
            get
            {
                return specId;
            }
            set
            {
                specId = value;
            }
        }

        /// <summary>
        /// ��ǰ�ؼ��Ĳ������ԣ��޸�Or�½�
        /// M���޸ģ�N���½�
        /// </summary>
        public string CurOperPro
        {
            get
            {
                return curOperPro;
            }
            set
            {
                curOperPro = value;
            }
        }

        /// <summary>
        /// ��ǰ����
        /// </summary>
        public string DisTypeId
        {
            get
            {
                return disTypeId;
            }
            set
            {
                disTypeId = value;
            }
        }

        public string DisTypeName
        {
            get
            {
                return disTypeName;
            }
            set
            {
                disTypeName = value;
            }
        }

        public SpecSourcePlan SpecSourcePlan
        {
            get
            {
                return specPlan;
            }
            set
            {
                specPlan = value;
            }
        }

        public string Seq
        {
            get
            {
                try
                {
                    return lsbBarCode.Items[0].ToString();
                }
                catch
                {
                    return "";
                }
            }
        }
        #endregion

        public ucSpecSourceForBlood()
        {
            InitializeComponent();
            specPlan = new SpecSourcePlan();
            specPlanManage = new SpecSourcePlanManage();
            specTypeManage = new SpecTypeManage();
            subSpecManage = new SubSpecManage();
            specBoxManage = new SpecBoxManage();
            barCodeManage = new SpecBarCodeManage();
            loginPerson = new FS.HISFC.Models.Base.Employee();
            barCodeList = new List<string>();
            sequenceList = new List<string>();
            disTypeList = new List<string>();
            specTypeList = new List<string>();
            numList = new List<string>();
        }

        /// <summary>
        /// �Ƿ��Զ��γ�������
        /// </summary>
        private void AutoGenerateBarCode()
        {

            int count = Convert.ToInt32(nudCount.Value);
            int barCount = lsbBarCode.Items.Count;
            if (chkAutoGen.Checked && chkName.Checked)
            {
                if (lsbBarCode.Items.Count == count)
                {
                    return;
                }
                if (count < barCount)
                {
                    for (int i = 0; i < barCount - count; i++)
                    {
                        lsbBarCode.Items.RemoveAt(lsbBarCode.Items.Count - 1 );
                    }
                    return;
                }
                if (count > barCount)
                {
                    int num = 0;
                    string barCodePre = "";
                    sequence = "";
                    //���������
                    if (lsbBarCode.Items.Count > 0)
                    {
                        foreach (string s in lsbBarCode.Items)
                        {
                            int lastNum = Convert.ToInt32(s[s.Length - 1].ToString());
                            if (lastNum > num)
                            {
                                num = lastNum;
                            }
                            barCodePre = s.Substring(0, s.Length - 1);
                            string firstLabel = s.Substring(0, 1);
                            //�����ֿ�ͷ
                            if (System.Text.RegularExpressions.Regex.Match(firstLabel, "^\\d+$").Success)
                            {
                                sequence = s.Substring(0, 6);
                            }
                            //��������ĸ��ͷ
                            if (System.Text.RegularExpressions.Regex.Match(firstLabel, "^[a-zA-Z]+$").Success)
                            {
                                sequence = s.Substring(1, 6);
                            }
                        }                      
                    }
                    else
                    {            
                        //�鿴ͬһԴ�����������걾��û�����ݣ�����������������ǵ����к�
                        if (SpecId > 0)
                        {
                            ArrayList arr = subSpecManage.GetSubSpecBySpecId(SpecId.ToString());
                            if (arr != null && arr.Count >0)
                            {
                                try
                                {
                                    SubSpec sub = arr[0] as SubSpec;
                                    string firstLabel = sub.SubBarCode.Substring(0, 1);
                                    //�����ֿ�ͷ
                                    if (System.Text.RegularExpressions.Regex.Match(firstLabel, "^\\d+$").Success)
                                    {
                                        sequence = sub.SubBarCode.Substring(0, 6);
                                    }
                                    //��������ĸ��ͷ
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
                        barCodePre = sequence.PadLeft(6, '0') + tmpSubBarCode.DisAbrre + tmpSubBarCode.SpecTypeAbrre;
                        //needUpdateSeq = true;
                    }
                    num++;
                    for (int i = 0; i < count - barCount; i++)
                    {
                        string barCode = barCodePre + (num + i).ToString();
                        lsbBarCode.Items.Add(barCode);
                        barCodeList.Add(barCode);
                        numList.Add((num + i).ToString());
                        disTypeList.Add(this.disTypeName);
                        sequenceList.Add(barCode.Substring(0, 6));
                    }

                }
            }
            else
            {
                lsbBarCode.Items.Clear();
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private void AddBarCode()
        {
            string barCode = txtBarCode.Text.Trim();
            int barCodeCount = lsbBarCode.Items.Count;
            if (barCode.Length >= 6 && barCodeCount < Convert.ToInt32(nudCount.Value) && chkName.Checked && !chkAutoGen.Checked)
            {
                //��ֹ�����ظ���������
                int index = 0;
                foreach (string s in lsbBarCode.Items)
                {
                    if (s == barCode)
                        break;
                    index++;
                }
                if (barCode.Length > 6)
                {
                    if (index <= lsbBarCode.Items.Count)
                    {
                        lsbBarCode.Items.Add(barCode);
                        sequenceList.Add("");
                    }
                }
                if (barCode.Length == 6)
                {
                    SpecBarCode bar = barCodeManage.GetSpecBarCode(disTypeName, chkName.Text);
                    if (bar != null)
                    {
                        barCode = bar.DisAbrre + barCode.PadLeft(6, '0') + bar.SpecTypeAbrre + barCodeCount.ToString();
                        sequenceList.Add(barCode.Substring(2,6));
                    }
                }
                lsbBarCode.Items.Add(barCode);
                barCodeList.Add(barCode);
                numList.Add(barCodeCount.ToString());
                disTypeList.Add(disTypeName);
                barCodeCount++;
                txtBarCode.Text = "";
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="curTtrans"></param>
        public void SetTrans(System.Data.IDbTransaction curTrans)
        {
            this.trans = curTrans;
            specTypeManage.SetTrans(this.trans);
            specPlanManage.SetTrans(this.trans);
            subSpecManage.SetTrans(this.trans);
            specBoxManage.SetTrans(this.trans);
            barCodeManage.SetTrans(this.trans);
        }

        public SpecSourcePlan GetSpecSourePlan()
        {
            //specPlan = new SpecSourcePlan();            
            specPlan.Capacity = nudCapcity.Value;
            specPlan.Count = Convert.ToInt32(nudCount.Value);
            specPlan.ForSelfUse = Convert.ToInt32(nudUse.Value);
            specPlan.Unit = "֧";
            specPlan.StoreCount = specPlan.Count;
            //if(chkOnlyDept.Checked)
            //    specPlan.LimitUse = '2';
            //else 
            //    specPlan.LimitUse = '0';
            //specPlan.Comment = txtComment.Text;
            if (chkOnlySelf.Checked)
            {
                specPlan.LimitUse = txtName.Text.Trim();
            }
            foreach (string s in lsbBarCode.Items)
            {
                specPlan.SubSpecCodeList.Add(s);
                barCodeCount++;
            }
            return specPlan;
        }

        public void SetSpecSourcePlan(SpecSourcePlan plan)
        {
            lsbBarCode.Items.Clear();
            if (plan.PlanID <= 0)
            {
                chkAutoGen.Checked = false;
                chkName.Checked = false;
                return;
            }
            //specPlan = new SpecSourcePlan();   
            if (plan.Capacity > 0)
            {
                nudCapcity.Value = plan.Capacity;
            }
            if (plan.Count > 0)
            {
                nudCount.Value = plan.Count;
            }
            nudUse.Value = plan.ForSelfUse;
            lsbBarCode.Items.Clear();
            foreach (string s in plan.SubSpecCodeList)
            {
                lsbBarCode.Items.Add(s);
            }
            chkAutoGen.Checked = false;
            if (chkName.Tag.ToString() == plan.SpecType.SpecTypeID.ToString())
            {
                chkName.Checked = true;
                this.Tag = plan;
            }
            if (plan.LimitUse.Trim() != "")
            {
                chkOnlySelf.Checked = true;
                txtName.Text = plan.LimitUse;
            }
        }

        /// <summary>
        /// ���ñ걾���ͼ�ID
        /// </summary>
        /// <param name="specTypeId">�걾����Id</param>
        public void SetSpecTypeName(string specTypeId)
        {
            FS.HISFC.Models.Speciment.SpecType st = specTypeManage.GetSpecTypeById(specTypeId);
            try
            {
                if (st != null)
                {
                    if (st.IsShow == "0" || st.DefaultCnt ==0)
                    {
                        chkName.Checked = false;
                    }
                    chkName.Text = st.SpecTypeName;
                    chkName.Tag = st.SpecTypeID;
                    nudCount.Value = st.DefaultCnt;
                    nudCapcity.Value = Convert.ToDecimal(st.Ext1);
                    inited = true;
                }
            }
            catch
            { }
        }

        /// <summary>
        /// �걾�ض�λ
        /// </summary>
        /// <param name="subSpec">��װ�걾</param>
        /// <returns></returns>
        private int SubSpecReLocate(ref SubSpec subSpec)
        {
            SpecInOper specInOper = new SpecInOper();
            specInOper.Trans = trans;
            specInOper.SetTrans();
            specInOper.SubSpec = subSpec;
            specInOper.DisTypeId = this.disTypeId;
            specInOper.SpecTypeId = subSpec.SpecTypeId.ToString();

            int result = specInOper.LocateSubSpec();
            if (result == -2)
            {
                MessageBox.Show("�˲�������û�п�ʹ�õı걾��!", "�걾��λ");
                return -1;
            }
            if (result <= 0)
            {
                return -1;
            }
            subSpec = specInOper.SubSpec;

            if (subSpecManage.UpdateSubSpec(subSpec) <= 0)
            {
                return -1;
            }

            if (specInOper.InOperInit() == -1)
            {
                return -1;
            }

            if (specInOper.FullSpecBox != null && specInOper.FullSpecBox.BoxId > 0)
            {
                this.UseFullBoxList.Add(specInOper.FullSpecBox);
            }
            return 1;
        }

        private bool BarCodeCountValidate()
        {
            if (lsbBarCode.Items.Count < Convert.ToInt32(nudCount.Value))
            {
                DialogResult ds = MessageBox.Show("�������������㣬���ǿ�ƽ��У���������Ϣ���󣡼�����", "��������", MessageBoxButtons.YesNo);
                if (ds == DialogResult.No)
                {
                    return false;
                }
            }
            return true;
        }

        private int UpdateBarCode(int subSpecId, int index)
        {
            string barCode = "";
            if (lsbBarCode.Items.Count > index)
            {
                barCode = lsbBarCode.Items[index] == null ? "" : (lsbBarCode.Items[index]).ToString();
            }
            decimal capcacity = nudCapcity.Value;
            string sql = " UPDATE SPEC_SUBSPEC SET SPEC_SUBSPEC.SUBBARCODE='" + barCode + "',SPEC_SUBSPEC.SPECCAP =" + capcacity.ToString() + " WHERE SUBSPECID = " + subSpecId.ToString();
            return subSpecManage.UpdateSubSpec(sql);
        }

        /// <summary>
        /// ����洢�걾����Ϣ
        /// </summary>
        /// <param name="isHis">�걾�Ƿ���Դ��His</param>
        /// <param name="tumorType">�걾�İ�������</param>
        /// <returns></returns>
        public int SaveSourcePlan(char isHis, string tumorType)
        {
            if (!chkName.Checked)
            {
                return 0;
            }
            if (Convert.ToInt32(nudCount.Value) <= 0)
            {
                return 0;
            }
            specPlan.Capacity = nudCapcity.Value;
            specPlan.StoreCount = Convert.ToInt32(nudCount.Value);
            specPlan.Count = Convert.ToInt32(nudCount.Value);
            specPlan.ForSelfUse = Convert.ToInt32(nudUse.Value);
            if (chkOnlySelf.Checked)
            {
                specPlan.LimitUse = txtName.Text;
            }
            string planId = specPlanManage.GetNextSequence();
            specPlan.PlanID = Convert.ToInt32(planId);
            specPlan.SpecID = Convert.ToInt32(specId);
            specPlan.IsHis = isHis;
            specPlan.SpecType.SpecTypeID = Convert.ToInt32(chkName.Tag.ToString());
            specPlan.TumorType = tumorType;
            for (int i = 0; i < specPlan.Count; i++)
            {
                if (i < lsbBarCode.Items.Count)
                {
                    specPlan.SubSpecCodeList.Add(lsbBarCode.Items[i].ToString());
                }
            }
            if (specPlanManage.InsertSourcePlan(specPlan) <= 0)
            {
                return -1;
            }
            //���´˴�ʹ�õ����
            if (needUpdateSeq)
            {
                sequence = (Convert.ToInt32(sequence)+1).ToString();
                if (barCodeManage.UpdateMaxSeqByDisAndType(DisTypeName,"1",sequence) <= 0)
                {
                    return -1;
                }
                needUpdateSeq = false;
            }

            SpecInOper specInOper = new SpecInOper();
            specInOper.DisTypeId = disTypeId.ToString();
            specInOper.LoginPerson = loginPerson;
            specInOper.SpecTypeId = specPlan.SpecType.SpecTypeID.ToString();
            specInOper.Trans = trans;
            specInOper.SetTrans();

            List<SubSpec> subList = new List<SubSpec>();
            int subResult = specInOper.SaveSubSpec(specPlan, ref subList);
            if (subResult == -2)
            {
                MessageBox.Show(chkName.Text + "���͵ı걾�ޱ걾�д��", "�걾����");
                return -1;
            }
            if (subResult <= 0)
            {
                return -1;
            }
            this.UseFullBoxList = specInOper.UseFullBoxList;
            return 1;

        }

        /// <summary>
        ///  ���·�װ�걾
        /// </summary>
        /// <param name="isHis">�Ƿ���Դ��His</param>
        /// <param name="tumorType">�걾�İ�������</param>
        /// <returns></returns>
        public int UpdateSourcePlan(char isHis, string tumorType)
        {           
            specPlan = this.Tag as SpecSourcePlan;
            if (specPlan == null)
            {
                return 0;
            }
            if (specPlan.PlanID <= 0 && !chkName.Checked)
            {
                return 0;
            }

            if (specPlan.SpecID <= 0 && chkName.Checked)
            {
                return this.SaveSourcePlan(isHis, tumorType);
            }

            #region  �����Ѵ��ڵı걾
            if (specPlan.PlanID > 0)
            {
                specPlan.IsHis = isHis;
                specPlan.TumorType = tumorType;
                ArrayList arrSubSpec = subSpecManage.GetSubSpecByStoreId(specPlan.PlanID.ToString());
                int oriCount = specPlan.Count;
                if (arrSubSpec == null)
                {
                    return -1;
                }
                #region  ��Ҫɾ���ñ걾
                if (!chkName.Checked || oriCount == 0)
                {
                    specPlan.Capacity = 0;
                    specPlan.Count = 0;
                    specPlan.ForSelfUse = 0;
                    specPlan.StoreCount = 0;
                    specPlan.SpecID = 0;
                    foreach (SubSpec spec in arrSubSpec)
                    {
                        SpecBox useBox = specBoxManage.GetBoxById(spec.BoxId.ToString());
                        int occupyCount = useBox.OccupyCount - 1;
                        if (occupyCount >= 0)
                        {
                            if (specBoxManage.UpdateOccupyCount(occupyCount.ToString(), useBox.BoxId.ToString()) <= 0)
                            {
                                return -1;
                            }
                        }
                        if (useBox.IsOccupy == '1')
                        {
                            if (specBoxManage.UpdateSotreFlag("0", useBox.BoxId.ToString()) <= 0)
                            {
                                return -1;
                            }
                        }

                        SubSpec tmpReSpec = new SubSpec();
                        tmpReSpec.StoreID = 0;
                        tmpReSpec = spec.Clone();
                        tmpReSpec.BoxEndCol = 0;
                        tmpReSpec.BoxEndRow = 0;
                        tmpReSpec.BoxId = 0;
                        tmpReSpec.BoxStartCol = 0;
                        tmpReSpec.BoxStartRow = 0;
                        tmpReSpec.SpecCap = 0.0M;
                        tmpReSpec.SpecCount = 0;
                        tmpReSpec.SpecId = 0;
                        tmpReSpec.Comment = "�걾ɾ��";
                        if (subSpecManage.UpdateSubSpec(tmpReSpec) <= 0)
                        {
                            return -1;
                        }
                    }
                }
                #endregion

                if (chkName.Checked)
                {
                    int count = Convert.ToInt32(nudCount.Value);
                    GetSpecSourePlan();

                    //����������,����������
                    #region
                    if (count == oriCount)
                    {
                        if (!BarCodeCountValidate())
                        {
                            return -1;
                        }
                        int index = 0;
                        foreach (SubSpec spec in arrSubSpec)
                        {
                            if (UpdateBarCode(spec.SubSpecId, index) <= 0)
                            {
                                return -1;
                            }
                            if (spec.BoxId <= 0 && spec.SpecId > 0 && spec.StoreID > 0)
                            {
                                SubSpec tmpSpec = spec;
                                if (SubSpecReLocate(ref tmpSpec) == -1)
                                {
                                    return -1;
                                }
                            }
                            index++;
                        }
                    }
                    #endregion
                    //������ٷ�װ�걾
                    #region
                    if (oriCount > count && count != 0)
                    {

                        DialogResult dialog = MessageBox.Show("ȷ��Ҫ���� " + chkName.Text + " ���͵ı걾��", "����", MessageBoxButtons.YesNo);
                        if (dialog == DialogResult.No)
                        {
                            return -1;
                        }
                        if (!BarCodeCountValidate())
                        {
                            return -1;
                        }
                        int cutNum = oriCount - count;

                        for (int i = 0; i < cutNum; i++)
                        {
                            SubSpec tmpCutSpec = new SubSpec();
                            tmpCutSpec = arrSubSpec[arrSubSpec.Count - 1] as SubSpec;
                            SpecBox useBox = specBoxManage.GetBoxById(tmpCutSpec.BoxId.ToString());
                            int occupyCount = useBox.OccupyCount - 1;
                            if (occupyCount >= 0)
                            {
                                if (specBoxManage.UpdateOccupyCount(occupyCount.ToString(), useBox.BoxId.ToString()) <= 0)
                                {
                                    return -1;
                                }
                            }
                            if (useBox.IsOccupy == '1')
                            {
                                if (specBoxManage.UpdateSotreFlag("0", useBox.BoxId.ToString()) <= 0)
                                {
                                    return -1;
                                }
                            }
                            //����֮����Ȼ����λ�ã�ֻ�Ǳ걾������������Ϣɾ��
                            //tmpCutSpec.BoxEndCol = 0;
                            //tmpCutSpec.BoxEndRow = 0;
                            //tmpCutSpec.BoxId = 0;
                            //tmpCutSpec.BoxStartCol = 0;
                            //tmpCutSpec.BoxStartRow = 0;
                            tmpCutSpec.SpecCap = 0.0M;
                            tmpCutSpec.SpecCount = 0;
                            tmpCutSpec.SubBarCode = "";
                            tmpCutSpec.StoreID = 0;
                            tmpCutSpec.SpecId = 0;
                            tmpCutSpec.Comment = "���ٷ�װ�걾";
                            if (subSpecManage.UpdateSubSpec(tmpCutSpec) <= 0)
                            {
                                return -1;
                            }
                            arrSubSpec.RemoveAt(arrSubSpec.Count - 1);
                        }
                        //��������
                        int index = 0;
                        foreach (SubSpec spec in arrSubSpec)
                        {
                            if (UpdateBarCode(spec.SubSpecId, index) <= 0)
                            {
                                return -1;
                            }
                            if (spec.BoxId <= 0 && spec.SpecId > 0 && spec.StoreID > 0)
                            {
                                SubSpec tmpSpec = spec;
                                if (SubSpecReLocate(ref tmpSpec) == -1)
                                {
                                    return -1;
                                }
                            }
                            index++;
                        }
                    }
                    #endregion
                    //������ӷ�װ�걾
                    #region
                    if (count > oriCount)
                    {
                        int addNum = count - oriCount;
                        if (!BarCodeCountValidate())
                        {
                            return -1;
                        }
                        SpecSourcePlan tmpPlan1 = specPlan.Clone();
                        tmpPlan1.Count = count - oriCount;


                        SpecInOper specInOper = new SpecInOper();
                        specInOper.DisTypeId = disTypeId.ToString();
                        specInOper.LoginPerson = loginPerson;
                        specInOper.SpecTypeId = tmpPlan1.SpecType.SpecTypeID.ToString();
                        specInOper.Trans = trans;
                        specInOper.SetTrans();

                        List<SubSpec> subList = new List<SubSpec>();
                        int subResult = specInOper.SaveSubSpec(tmpPlan1, ref subList);
                        if (subResult == -2)
                        {
                            MessageBox.Show("�˱걾�����ޱ걾�д��", "�걾����");
                            return -1;
                        }
                        if (subResult <= 0)
                        {
                            return -1;
                        }
                        this.UseFullBoxList = specInOper.UseFullBoxList;
                        //���»�ȡ�걾�б�
                        arrSubSpec = subSpecManage.GetSubSpecByStoreId(specPlan.PlanID.ToString());
                        //��������
                        int index = 0;
                        foreach (SubSpec spec in arrSubSpec)
                        {
                            if (UpdateBarCode(spec.SubSpecId, index) <= 0)
                            {
                                return -1;
                            }
                            if (spec.BoxId <= 0 && spec.SpecId > 0 && spec.StoreID > 0)
                            {
                                SubSpec tmpSpec = spec;
                                if (SubSpecReLocate(ref tmpSpec) == -1)
                                {
                                    return -1;
                                }
                            }
                            index++;
                        }
                    }
                    #endregion

                }
                if (specPlanManage.UpdateSpecPlan(specPlan) <= 0)
                {
                    return -1;
                }
                //���´˴�ʹ�õ����
                if (needUpdateSeq)
                {
                    sequence = (Convert.ToInt32(sequence) + 1).ToString();
                    if (barCodeManage.UpdateBarCode(disTypeName, chkName.Text, sequence) <= 0)
                    {
                        return -1;
                    }
                    needUpdateSeq = false;
                }
            }
            #endregion

            return 1;

        }

        private void ucSpecSourceForBlood_Load(object sender, EventArgs e)
        {
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            grpType.Text = chkName.Text;
        }

        private void chkAutoGen_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoGen.Checked)
            {
                AutoGenerateBarCode();
            }
        }

        private void txtBarCode_TextChanged(object sender, EventArgs e)
        {
            AddBarCode();
        }

        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtBarCode.Text.Length <= 6)
                {
                    txtBarCode.Text =  txtBarCode.Text.PadLeft(6, '0');
                }
                AddBarCode();
            }
        }

        private void nudCount_ValueChanged(object sender, EventArgs e)
        {

            if (inited)
            {
                int oldCount = lsbBarCode.Items.Count;
                int newCount = Convert.ToInt32(nudCount.Value);
                int diff = newCount - oldCount;
                //if (diff > 0 && chkAutoGen.Checked)
                //{
                //    this.AutoGenerateBarCode();
                //}
                if (diff < 0)
                {
                    diff = Math.Abs(diff);
                    for (int i = 0; i < diff; i++)
                    {
                        lsbBarCode.Items.RemoveAt(lsbBarCode.Items.Count-1);
                    }
                }
            }
        }

        private void lsbBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                lsbBarCode.Items.Remove(lsbBarCode.SelectedItem);
                barCodeCount--;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (barCodeList.Count <= 0)
            {
                if (lsbBarCode.SelectedItems.Count <= 0)
                {
                    MessageBox.Show("û���µ����룬�����ӡ��ѡ��");
                    return; 
                }
                foreach (string s in lsbBarCode.SelectedItems)
                {
                    barCodeList.Add(s);
                    numList.Add(s.Substring(s.Length - 1, 1));
                    disTypeList.Add(disTypeName);
                    specTypeList.Add(chkName.Text);
                    string sequence = "";
                    if (s.Length == 9)
                    {
                        sequence = s.Substring(1, 6);
                    }
                    if (s.Length == 11)
                    {
                        sequence = s.Substring(0, 6);
                    }
                    sequenceList.Add(sequence);
                }
            }
            if (PrintLabel.Print2DBarCode(barCodeList, sequenceList, specTypeList, disTypeList, numList) == -1)
            {
                MessageBox.Show("��ӡʧ��");
                return;
            }
            barCodeList = new List<string>();
            sequenceList = new List<string>();
            disTypeList = new List<string>();
            numList = new List<string>();
        }
    }
}
