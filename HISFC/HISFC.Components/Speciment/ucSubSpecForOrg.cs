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
    public partial class ucSubSpecForOrg : UserControl
    {
        #region ˽�б���
        private ucColForOrg ucSubOrgSource;
        private ucColForOrg[] ucSubOrgSourceInFlp;

        private List<SpecBox> curUseBoxList = new List<SpecBox>();

        private SpecSourcePlan specSourcePlan;
        private SubSpec subSpec;
        private FS.HISFC.Models.Base.Employee loginPerson;

        private SubSpecManage subSpecManage;
        private SpecSourcePlanManage specSourcePlanManage;
        private SpecTypeManage specTypeManage;
        private SpecBoxManage specBoxManage;

        private string flag;
        private int tumorType;
        private int specId;
        private string sideFrom = "";

        //��ǰ�������ԣ����޸�Or�½�
        private string curOperPro = "";
        private int disTypeId;
        private string tumorPor = "";
        private string tumorPos = "";
        private string transPos = "";
        private string tumorDet = "";

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
        /// ��������
        /// </summary>
        public int TumorType
        {
            get
            {
                return tumorType;
            }
            set
            {
                tumorType = value;
            }
        }

        /// <summary>
        /// ����Id
        /// </summary>
        public int DisTypeId
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

        /// <summary>
        /// ������
        /// </summary>
        public string TumorPor
        {
            get
            {
                return tumorPor;
            }
            set
            {
                tumorPor = value;
            }
        }

        /// <summary>
        /// ȡ��������ϸ˵��
        /// </summary>
        public string PosDet
        {
            set
            {
                tumorDet = value;
            }

        }

        /// <summary>
        /// ȡ�ԵĲ��
        /// </summary>
        public string SideFrom
        {
            set
            {
                sideFrom = value;
            }
        }

        /// <summary>
        /// ���ﲿλ
        /// </summary>
        public string TumorPos
        {
            set
            {
                tumorPos = value;
            }
        }

        public string TransPos
        {
            set
            {
                transPos = value;
            }
        }

        public ucColForOrg[] SubOrgSourceInFlp
        {
            set
            {
                ucSubOrgSourceInFlp = value;
            }
            get
            {
                return ucSubOrgSourceInFlp;
            }
        }

        /// <summary>
        /// ���ر걾��
        /// </summary>
        public string Seq
        {
            get
            {
 
                 foreach (ucColForOrg c in ucSubOrgSourceInFlp)
                {
                    if (c.Seq != null && c.Seq != string.Empty)
                    {
                        return c.Seq;
                    }
                    else
                    {
                        continue;
                    }
                }
                return string.Empty;
            }
        }

        #endregion

        public ucSubSpecForOrg()
        {
            InitializeComponent();
            specTypeManage = new SpecTypeManage();
            ucSubOrgSource = new ucColForOrg();
            specSourcePlan = new SpecSourcePlan();
            subSpec = new SubSpec();
            subSpecManage = new SubSpecManage();
            specSourcePlanManage = new SpecSourcePlanManage();
            specBoxManage = new SpecBoxManage();
            flag = "";
            tumorType = 0;
            //��ǰ�ĵķ�װ�걾���ڵ�ԭ�걾ID
            specId = 0;
            loginPerson = new FS.HISFC.Models.Base.Employee();
            FlpBinding();
        }

        private void RbtChanged(object sender, EventArgs e)
        {
            RadioButton rbt = sender as RadioButton;
            grp.Text = rbt.Text;
            if (ucSubOrgSourceInFlp != null)
            {
                SetTumorKind();
            }
        }

        private void SetTumorKind()
        {
            foreach (ucColForOrg org in ucSubOrgSourceInFlp)
            {
                if (rbtTumor.Checked) { org.TumorKind = "T"; org.TumorPos = rbtTumor.Text; }
                if (rbtSonTumor.Checked) { org.TumorKind = "S"; org.TumorPos = rbtSonTumor.Text; }
                if (rbtShuang.Checked) { org.TumorKind = "E"; org.TumorPos = rbtShuang.Text; }
                if (rbtNormal.Checked) { org.TumorKind = "N"; org.TumorPos = rbtNormal.Text; }
                if (rbtSide.Checked) { org.TumorKind = "P"; org.TumorPos = rbtSide.Text; }
                if (rbtLinBa.Checked) { org.TumorKind = "L"; org.TumorPos = rbtLinBa.Text; }
            }
        }

        /// <summary>
        /// ����flp�еĿؼ�
        /// </summary>
        private void FlpBinding()
        {
            ArrayList arrTmp = new ArrayList();
            arrTmp = specTypeManage.GetSpecByOrgName("��֯");
            ucSubOrgSourceInFlp = new ucColForOrg[arrTmp.Count];
            int i = 0;
            foreach (FS.HISFC.Models.Speciment.SpecType s in arrTmp)
            {
                ucSubOrgSource = new ucColForOrg();
                ucSubOrgSource.SetSpecType(s);
                ucSubOrgSourceInFlp[i] = ucSubOrgSource;
                ucSubOrgSourceInFlp[i].Name = "ucSubOrgSource" + i.ToString();
                flpPlan.Controls.Add(ucSubOrgSource);
                i++;
            }
            //foreach (ucColForOrg org in ucSubOrgSourceInFlp)
            //{
            //    flpPlan.Controls.Add(org);
            //}
        }

        private void SetPlanSoure(ref SpecSourcePlan s)
        {
            s.TumorPos = txtTumorPos.Text;
            int tumorType = 0;
            ///�걾ȡ�Ĳ�λ
            if (rbtTumor.Checked) tumorType = Convert.ToInt32(Constant.TumorType.����);
            if (rbtSonTumor.Checked) tumorType = Convert.ToInt32(Constant.TumorType.����);
            if (rbtShuang.Checked) tumorType = Convert.ToInt32(Constant.TumorType.��˨);
            if (rbtNormal.Checked) tumorType = Convert.ToInt32(Constant.TumorType.����);
            if (rbtSide.Checked) tumorType = Convert.ToInt32(Constant.TumorType.����);
            if (rbtLinBa.Checked) tumorType = Convert.ToInt32(Constant.TumorType.�ܰͽ�);

            s.TumorType = tumorType.ToString();

            /////��Ĥ�Ƿ�����
            //if (rbtYes.Checked) s.BaoMoEntire = '1';
            //if (rbtNo.Checked) s.BaoMoEntire = '0';

            /////�Ƿ�����֯
            //if (rbtPieceNo.Checked) s.BreakPiece = '0';
            //if (rbtPieceYes.Checked) s.BreakPiece = '1';
            s.Comment = txtComment.Text;
        }

        public void InitSpecPlan(ArrayList arrSpecPlan)
        {
            if (arrSpecPlan == null)
            {
                return;
            }
            foreach (SpecSourcePlan plan in arrSpecPlan)
            {
                for (int k = 0; k < ucSubOrgSourceInFlp.Length; k++)
                {
                    foreach (Control cp in ucSubOrgSourceInFlp[k].Controls[0].Controls)
                    {
                        if (cp.Name == "chkName")
                        {
                            CheckBox ck = cp as CheckBox;
                            if (ck.Tag.ToString() == plan.SpecType.SpecTypeID.ToString())
                            {
                                ucSubOrgSourceInFlp[k].SpecSourcePlan = plan;
                                ucSubOrgSourceInFlp[k].GetColOrgInfo(ck.Tag.ToString(), plan.Count, plan.LimitUse, plan.SubSpecCodeList);
                                //ucSubOrgSourceInFlp[k].SpecId = plan.SpecID;
                                this.specSourcePlan = plan;
                                this.specId = plan.SpecID;
                                break;
                            }
                        }
                        ucSubOrgSourceInFlp[k].SpecId = plan.SpecID;
                        if (rbtTumor.Checked) ucSubOrgSourceInFlp[k].TumorKind = "T";
                        if (rbtSonTumor.Checked) ucSubOrgSourceInFlp[k].TumorKind = "S";
                        if (rbtShuang.Checked) ucSubOrgSourceInFlp[k].TumorKind = "E";
                        if (rbtNormal.Checked) ucSubOrgSourceInFlp[k].TumorKind = "N";
                        if (rbtSide.Checked) ucSubOrgSourceInFlp[k].TumorKind = "P";
                        if (rbtLinBa.Checked) ucSubOrgSourceInFlp[k].TumorKind = "L";
                    }

                    //ucSpecSourceInFlp[i].Tag = plan;
                }
            }

            foreach (ucColForOrg cp in ucSubOrgSourceInFlp)
            {
                SpecSourcePlan tmp = cp.SpecSourcePlan;
                if (tmp == null || tmp.PlanID <= 0)
                {
                    tmp = new SpecSourcePlan();
                    cp.SpecSourcePlan = tmp;
                    cp.GetColOrgInfo(cp.Tag.ToString(), tmp.Count, tmp.LimitUse, tmp.SubSpecCodeList);
                }
            }

            //ucSpecSourceInFlp[i].Tag = plan;

        }

        private int UpdateSubSpec(ucColForOrg c, System.Data.IDbTransaction trans)
        {
            #region �����Ѿ����ڵı걾

            ucColForOrg ucOrg = c;
            SpecSourcePlan s = specSourcePlanManage.GetSourcePlan("SELECT * FROM SPEC_SOURCE_STORE WHERE SOTREID=" + c.SpecSourcePlan.PlanID.ToString())[0] as SpecSourcePlan;
            int oriCount = s.Count;
            //���·�װ�걾������
            this.SetPlanSoure(ref s);
            //int rt = specSourcePlanManage.UpdateSpecPlan(s);
            c.SpecSourcePlan = s;

            if (specSourcePlanManage.UpdateSpecPlan(c.SpecSourcePlan) <= 0)
            {
                return -1;
            }


            if (c.UpdateUserPro(trans) < 0)
            {
                MessageBox.Show("����ר������ʧ��!");
                return -1;
            }

            ArrayList arrSubSpec = new ArrayList();
            arrSubSpec = subSpecManage.GetSubSpecByStoreId(s.PlanID.ToString());
            if (arrSubSpec == null || arrSubSpec.Count <= 0)
            {
                MessageBox.Show("��ȡ��װ�걾ʧ�ܣ�", "����");
                return -1;
                //trans.RollBack();
                //return;
            }
            ControlCollection controls = (ucOrg.Controls[0] as GroupBox).Controls;
            foreach (Control cl in controls)
            {
                if (cl.Name.Contains("chkName"))
                {
                    CheckBox chk = cl as CheckBox;
                    string specTypeName = specTypeManage.GetSpecTypeById(s.SpecType.SpecTypeID.ToString()).SpecTypeName;
                    #region ����걾����ѡ��
                    if (chk.Checked)
                    {
                        s.LimitUse = c.SpecSourcePlan.LimitUse;
                        s.SubSpecCodeList = c.SpecSourcePlan.SubSpecCodeList;
                        s.Count = c.SpecSourcePlan.Count;

                        //����������,����������
                        #region
                        if (s.Count == oriCount)
                        {
                            int index = 0;
                            if (!ucOrg.BarCodeCountValidate())
                            {
                                return -1;
                            }
                            foreach (SubSpec spec in arrSubSpec)
                            {
                                int result = ucOrg.UpdateBarCode(spec.SubSpecId, index, trans);
                                if (result <= 0)
                                {
                                    MessageBox.Show("����ʧ�ܣ�", "����");
                                    return -1;
                                }
                                index++;
                            }
                        }
                        #endregion
                        //������ٷ�װ�걾
                        #region
                        if (oriCount > s.Count && s.Count != 0)
                        {

                            DialogResult dialog = MessageBox.Show("ȷ��Ҫ���� " + specTypeName + " ���͵ı걾��", "����", MessageBoxButtons.YesNo);
                            if (dialog == DialogResult.No)
                            {
                                return -1;
                            }
                            if (!ucOrg.BarCodeCountValidate())
                            {
                                return -1;
                            }
                            int cutNum = oriCount - s.Count;
                            //���¿��
                            s.StoreCount -= cutNum;
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
                                tmpCutSpec.BoxEndCol = 0;
                                tmpCutSpec.BoxEndRow = 0;
                                tmpCutSpec.BoxId = 0;
                                tmpCutSpec.BoxStartCol = 0;
                                tmpCutSpec.BoxStartRow = 0;
                                tmpCutSpec.SpecCap = 0.0M;
                                tmpCutSpec.SpecCount = 0;
                                tmpCutSpec.SubBarCode = "";
                                tmpCutSpec.StoreID = 0;
                                tmpCutSpec.Comment = "���ٷ�װ�걾";
                                tmpCutSpec.SpecId = 0;
                                if (subSpecManage.UpdateSubSpec(tmpCutSpec) <= 0)
                                {
                                    MessageBox.Show("����ʧ�ܣ�", "����");
                                    return -1;
                                }
                                arrSubSpec.RemoveAt(arrSubSpec.Count - 1);
                            }
                            //��������
                            int index = 0;
                            foreach (SubSpec spec in arrSubSpec)
                            {
                                int result = ucOrg.UpdateBarCode(spec.SubSpecId, index, trans);
                                if (result <= 0)
                                {
                                    MessageBox.Show("����ʧ�ܣ�", "����");
                                    return -1;
                                }
                                index++;
                            }
                        }
                        #endregion
                        //������ӷ�װ�걾
                        #region
                        if (s.Count > oriCount)
                        {
                            int addNum = s.Count - oriCount;
                            if (!ucOrg.BarCodeCountValidate())
                            {
                                return -1;
                            }
                            SpecSourcePlan tmpPlan1 = s.Clone();
                            tmpPlan1.Count = s.Count - oriCount;
                            SpecInOper specInOper = new SpecInOper();
                            specInOper.LoginPerson = loginPerson;
                            specInOper.SpecTypeId = tmpPlan1.SpecType.SpecTypeID.ToString();
                            specInOper.DisTypeId = disTypeId.ToString();
                            specInOper.Trans = trans;
                            specInOper.SetTrans();

                            List<SubSpec> subList = new List<SubSpec>();
                            //��Ҫ���ӵı걾��
                            //SpecSourcePlan tmpPlan = new SpecSourcePlan();
                            //tmpPlan = c.SpecSourcePlan.Clone();
                            //tmpPlan.Count = addNum;
                            int subResult = specInOper.SaveSubSpec(tmpPlan1, ref subList);
                            //���¿��
                            s.StoreCount += addNum;
                            if (subResult == -2)
                            {
                                MessageBox.Show("�˲��ֵ� " + chk.Text + " �����ޱ걾�д��", "�걾����");
                                return -1;
                            }
                            if (subResult <= 0)
                            {
                                MessageBox.Show("����ʧ�ܣ�", "����");
                                return -1;
                            }

                            this.UseFullBoxList = specInOper.UseFullBoxList;
                            //���»�ȡ�걾�б�
                            arrSubSpec = subSpecManage.GetSubSpecByStoreId(s.PlanID.ToString());
                            //��������
                            int index = 0;
                            foreach (SubSpec spec in arrSubSpec)
                            {
                                int result = ucOrg.UpdateBarCode(spec.SubSpecId, index, trans);
                                if (result <= 0)
                                {
                                    MessageBox.Show("����ʧ�ܣ�", "����");
                                    return -1;
                                }
                                index++;
                            }
                        }
                        #endregion
                    }
                    #endregion
                    if (!chk.Checked || (chk.Checked && s.Count == 0))
                    {
                        DialogResult dialog = MessageBox.Show("ȷ��ɾ�� " + specTypeName + " ���͵ı걾��", "����", MessageBoxButtons.YesNo);
                        if (dialog == DialogResult.No)
                        {
                            return -1;
                        }
                        s.Count = 0;
                        s.Capacity = 0;
                        s.StoreCount = 0;
                        s.SpecID = 0;
                        foreach (SubSpec spec in arrSubSpec)
                        {
                            SpecBox useBox = specBoxManage.GetBoxById(spec.BoxId.ToString());
                            int occupyCount = useBox.OccupyCount - 1;
                            if (occupyCount >= 0)
                            {
                                if (specBoxManage.UpdateOccupyCount(occupyCount.ToString(), useBox.BoxId.ToString()) <= 0)
                                {
                                    MessageBox.Show("����ʧ�ܣ�", "����");
                                    return -1;
                                }
                            }
                            if (useBox.IsOccupy == '1')
                            {
                                if (specBoxManage.UpdateSotreFlag("0", useBox.BoxId.ToString()) <= 0)
                                {
                                    MessageBox.Show("����ʧ�ܣ�", "����");
                                    return -1;
                                }
                            }
                            SubSpec tmpReSpec = spec.Clone();
                            tmpReSpec.SubBarCode = "";
                            tmpReSpec.Comment = "���ٷ�װ�걾";
                            tmpReSpec.StoreID = 0;
                            tmpReSpec.BoxEndCol = 0;
                            tmpReSpec.BoxEndRow = 0;
                            tmpReSpec.BoxId = 0;
                            tmpReSpec.BoxStartCol = 0;
                            tmpReSpec.BoxStartRow = 0;
                            tmpReSpec.SpecCap = 0.0M;
                            tmpReSpec.SpecCount = 0;
                            tmpReSpec.SpecId = 0;
                            if (subSpecManage.UpdateSubSpec(tmpReSpec) <= 0)
                            {
                                MessageBox.Show("����ʧ�ܣ�", "����");
                                return -1;
                            }
                        }
                    }
                }
            }
            s.BaoMoEntire = tumorDet;
            if (specSourcePlanManage.UpdateSpecPlan(s) <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("����ʧ�ܣ�", "����");
                //trans.RollBack();
                return -1;
            }
            //trans.Commit();
            // MessageBox.Show("���³ɹ���", "����");
            //��ǰ�걾������
            if (UseFullBoxList != null)
            {
                foreach (SpecBox box in UseFullBoxList)
                {
                    MessageBox.Show(box.BoxBarCode + " �걾��������������µı걾�У�", "��ӱ걾��");
                    //��ʾ�û�����µı걾��
                    FS.HISFC.Components.Speciment.Setting.frmSpecBox newSpecBox = new FS.HISFC.Components.Speciment.Setting.frmSpecBox();
                    if (box.DesCapType == 'B')
                        newSpecBox.CurLayerId = box.DesCapID;
                    else
                        newSpecBox.CurShelfId = box.DesCapID;
                    newSpecBox.Show();
                }
                UseFullBoxList = new List<SpecBox>();
            }
            return 1;
            #endregion
        }

        /// <summary>
        /// �����װ�걾���
        /// </summary>
        /// <param name="specId"></param>
        /// <param name="tumorPor"></param>
        /// <returns></returns>
        public int SaveSourcePlan(System.Data.IDbTransaction trans)
        {
            specSourcePlanManage.SetTrans(trans);
            specTypeManage.SetTrans(trans);
            subSpecManage.SetTrans(trans);
            //specSourcePlanManage.SetTrans(trans.Trans);
            //specTypeManage.SetTrans(trans.Trans);

            specBoxManage.SetTrans(trans);

            #region �����װ�걾
            foreach (ucColForOrg c in ucSubOrgSourceInFlp)
            {
                if (c.UpdateSeq(trans) <= 0)
                {
                    return -1;
                }
                ControlCollection controls = (c.Controls[0] as GroupBox).Controls;
                foreach (Control cl in controls)
                {
                    //grp.Text;                    

                    if (cl.Name.Contains("chkName"))
                    {
                        CheckBox chk = cl as CheckBox;
                        //���count��0������
                        if (c.SpecSourcePlan.Count <= 0)
                            continue;
                        c.SpecSourcePlan.SpecID = specId;
                        c.SpecSourcePlan.TumorPor = tumorPor;
                        c.SpecSourcePlan.TumorPos = tumorPos;
                        c.SpecSourcePlan.TransPos = transPos;
                        c.SpecSourcePlan.BaoMoEntire = tumorDet;
                        ///�걾ȡ�Ĳ�λ
                        if (rbtTumor.Checked) c.SpecSourcePlan.TumorType = Convert.ToInt32(Constant.TumorType.����).ToString();
                        if (rbtSonTumor.Checked) c.SpecSourcePlan.TumorType = Convert.ToInt32(Constant.TumorType.����).ToString();
                        if (rbtShuang.Checked) c.SpecSourcePlan.TumorType = Convert.ToInt32(Constant.TumorType.��˨).ToString();
                        if (rbtNormal.Checked) c.SpecSourcePlan.TumorType = Convert.ToInt32(Constant.TumorType.����).ToString();
                        if (rbtSide.Checked) c.SpecSourcePlan.TumorType = Convert.ToInt32(Constant.TumorType.����).ToString();
                        if (rbtLinBa.Checked) c.SpecSourcePlan.TumorType = Convert.ToInt32(Constant.TumorType.�ܰͽ�).ToString();

                        /////��Ĥ�Ƿ�����
                        //if (rbtYes.Checked) c.SpecSourcePlan.BaoMoEntire = '1';
                        //if (rbtNo.Checked) c.SpecSourcePlan.BaoMoEntire = '0';

                        ///�Ƿ�����֯
                        //if (rbtPieceNo.Checked) c.SpecSourcePlan.BreakPiece = "0";
                        //if (rbtPieceYes.Checked) c.SpecSourcePlan.BreakPiece = "1";
                        c.SpecSourcePlan.Comment = txtComment.Text;
                        int specTypeId = specTypeManage.GetSpecIDByName(chk.Text);
                        c.SpecSourcePlan.SpecType.SpecTypeID = specTypeId;
                        c.SpecSourcePlan.SideFrom = sideFrom;

                        if (!chk.Checked)
                        {
                            //���PlanId��0 ��ʾ ����
                            if (c.SpecSourcePlan.PlanID > 0)
                            {
                                if (UpdateSubSpec(c, trans) <= 0)
                                {
                                    return -1;
                                }
                            }
                        }
                        if (chk.Checked)
                        {
                            //���뵽Spec_Source_Store��

                            //���PlanId��0 ��ʾ ����
                            if (c.SpecSourcePlan.PlanID > 0)
                            {
                                if (UpdateSubSpec(c, trans) <= 0)
                                {
                                    return -1;
                                }
                            }

                            else
                            {
                                if (!c.BarCodeCountValidate())
                                {
                                    return -1;
                                }
                                string sequence = specSourcePlanManage.GetNextSequence();
                                c.SpecSourcePlan.PlanID = Convert.ToInt32(sequence);
                                if (specSourcePlanManage.InsertSourcePlan(c.SpecSourcePlan) == -1)
                                    return -1;

                                SpecInOper specInOper = new SpecInOper();
                                specInOper.S863 = c.C863;
                                specInOper.S115 = c.C115;
                                specInOper.DisTypeId = disTypeId.ToString();
                                specInOper.LoginPerson = loginPerson;
                                specInOper.SpecTypeId = specTypeId.ToString();
                                specInOper.OrgOrBld = "O";
                                specInOper.Trans = trans;
                                specInOper.SetTrans();
                                List<SubSpec> subList = new List<SubSpec>();
                                int subResult = specInOper.SaveSubSpec(c.SpecSourcePlan, ref subList);
                                if (subResult == -2)
                                {
                                    c.SpecSourcePlan.PlanID = 0;
                                    MessageBox.Show("�˲��ֵ� " + chk.Text + " �����ޱ걾�д��", "�걾����");
                                    return -2;
                                }
                                if (subResult <= 0)
                                {
                                    return -1;
                                }

                                if (c.UpdateUserPro(trans)<0)
                                {
                                    MessageBox.Show("����ר������ʧ�ܣ�");
                                    return -1;
                                }

                                this.UseFullBoxList.AddRange(specInOper.UseFullBoxList);// = specInOper.UseFullBoxList;
                            }
                        }
                    }
                }
            }
            #endregion
            

            return 1;
        }

        private void ucSubSpecForOrg_Load(object sender, EventArgs e)
        {
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            //FlpBinding();

            if (tumorType != 0)
            {
                Constant.TumorType type = (Constant.TumorType)tumorType;
                switch (type)
                {
                    case Constant.TumorType.����:
                        rbtSonTumor.Checked = true;
                        break;
                    case Constant.TumorType.��˨:
                        rbtShuang.Checked = true;
                        break;
                    case Constant.TumorType.����:
                        rbtNormal.Checked = true;
                        break;
                    case Constant.TumorType.����:
                        rbtSide.Checked = true;
                        break;
                    case Constant.TumorType.�ܰͽ�:
                        rbtLinBa.Checked = true;
                        break;
                    default:
                        rbtTumor.Checked = true;
                        break;
                }
            }
            SetTumorKind();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            btnDel.Parent.Parent.Parent.Parent.Dispose();
        }

        public void GetPlanSource(SpecSourcePlan s)
        {
            txtTumorPos.Text = s.TumorPos;
            char[] tumorType = s.TumorType.ToString().ToCharArray();

            foreach (char t in tumorType)
            {
                Constant.TumorType TumorType = (Constant.TumorType)(Convert.ToInt32(t.ToString()));
                switch (TumorType)
                {
                    case Constant.TumorType.����:
                        rbtTumor.Checked = true;
                        grp.Text = rbtTumor.Text;
                        break;
                    case Constant.TumorType.����:
                        rbtSonTumor.Checked = true;
                        grp.Text = rbtSonTumor.Text;
                        break;
                    case Constant.TumorType.����:
                        grp.Text = rbtSide.Text;
                        rbtSide.Checked = true;
                        break;
                    case Constant.TumorType.��˨:
                        grp.Text = rbtShuang.Text;
                        rbtShuang.Checked = true;
                        break;
                    case Constant.TumorType.����:
                        grp.Text = rbtNormal.Text;
                        rbtNormal.Checked = true;
                        break;
                    case Constant.TumorType.�ܰͽ�:
                        grp.Text = rbtLinBa.Text;
                        rbtLinBa.Checked = true;
                        break;
                    default:
                        break;
                }
            }
            //if (s.BaoMoEntire == '0')
            //    rbtNo.Checked = true;
            //else
            //    rbtYes.Checked = true;
            //if (s.BreakPiece == "0")
            //    rbtPieceNo.Checked = true;
            //else
            //    rbtPieceYes.Checked = true;
            txtComment.Text = s.Comment;

        }

        private void btnPrintLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
