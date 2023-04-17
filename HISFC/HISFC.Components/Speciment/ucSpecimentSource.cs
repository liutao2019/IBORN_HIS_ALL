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
using FS.HISFC.Models.Base;

namespace FS.HISFC.Components.Speciment
{
    public partial class ucSpecimentSource : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region 私有变量
        private ucSpecSourceForBlood ucSpecSource;
        private ucSpecSourceForBlood[] ucSpecSourceInFlp;  
        private ucDiagnose ucDiaInFlp;
        private SpecDiagnose diagInfo;    
        private ArrayList alDepts;
        private SpecSource specSource;
        private DisTypeManage disTypeManage;
        private SpecSourcePlanManage specSourcePlanManage;
        private SubSpecManage subSpecManage;
        private SpecSourceManage specSourceMange;
        private DiagnoseManage diagManage;
        private PatientManage specPatientManage;
        private OperApplyManage operApplyManage;
        private FS.HISFC.Models.Base.Employee loginPerson;
        private ucPatientInfo ucPatient;
        private SpecPatient specPatient; 
        private string title;
        private int specId = 0;
        private string oper = "";
        private string patientId="";
        private string tumorPro="";//血标本性质      
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService;
        #endregion

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
        /// 构造函数
        /// </summary>
        public ucSpecimentSource()
        {
            InitializeComponent();
            ucSpecSource = new ucSpecSourceForBlood();
            specSource = new SpecSource();
            disTypeManage = new DisTypeManage();
            specSourcePlanManage = new SpecSourcePlanManage();
            specSourceMange = new SpecSourceManage();
            ucPatient = new ucPatientInfo();   
            specPatient = new SpecPatient();
            diagManage = new DiagnoseManage();// = new BaseManage();
            specPatientManage = new PatientManage();
            subSpecManage = new SubSpecManage();
            operApplyManage = new OperApplyManage();
            ucDiaInFlp = new ucDiagnose();
            alDepts = new ArrayList();
            toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
            title = "血标本录入";
        }

        #region 私有函数：初始化界面

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
            if (dtpStart.Value != null)
                sql += " and OPERTIME>=to_date('" + dtpStart.Value.Date.ToString() + "','yyyy-mm-dd hh24:mi:ss')";
            if (dtpEnd.Value != null)
                sql += " and OPERTIME<=to_date('" + dtpEnd.Value.AddDays(1.0).Date.ToString() + "','yyyy-mm-dd hh24:mi:ss')";
            if (cmbOperDept.Tag != null && cmbOperDept.Text != "") sql += " and OPERDEPTNAME = '" + cmbOperDept.Text.Trim() + "'";
            return sql;
        }

        /// <summary>
        /// 加载手术申请单
        /// </summary>
        private void LoadOperApply()
        {
            string sql = GenerateApplySql();
            ArrayList arrList = operApplyManage.GetOperApplyBySql(sql);
            if (arrList != null && arrList.Count > 0)
            {
                foreach (OperApply apply in arrList)
                {
                    //未取标本
                    if (apply.HadCollect == "0" || apply.HadCollect == "4" || apply.HadCollect == "5" || apply.HadCollect == "6")
                    {
                        TreeNode tnNotGet = tvSpec.Nodes[2];
                        TreeNode tn = new TreeNode();
                        tn.Text = apply.PatientName + " 【" + apply.OperDeptName.ToString() + "】 ";
                        tn.Tag = apply;
                        tnNotGet.Nodes.Add(tn);
                    }

                    //取消不取
                    if (apply.HadCollect == "3")
                    {
                        TreeNode tnNotGet = tvSpec.Nodes[3];
                        TreeNode tn = new TreeNode();
                        tn.Text = apply.PatientName + " 【" + apply.OperDeptName + "】 ";
                        tn.Tag = apply;
                        tnNotGet.Nodes.Add(tn);
                    }

                    //已送
                    if (apply.HadCollect == "2")
                    {
                        TreeNode tnNotGet = tvSpec.Nodes[1];
                        TreeNode tn = new TreeNode();
                        tn.Text = apply.PatientName + " 【" + apply.OperDeptName + "】 ";
                        tn.Tag = apply;
                        tnNotGet.Nodes.Add(tn);
                    }
                }
            }
            tvSpec.Nodes[2].Text = "护士站未送  (共:" + tvSpec.Nodes[2].Nodes.Count.ToString() + " 例)";
            tvSpec.Nodes[3].Text = "已取消  (共:" + tvSpec.Nodes[3].Nodes.Count.ToString() + " 例)";
            tvSpec.Nodes[1].Text = "护士站已送  (共:" + tvSpec.Nodes[1].Nodes.Count.ToString() + " 例)";

        }

        /// <summary>
        /// 加载样本列表
        /// </summary>
        private void LoadSpecSource()
        {
            tvSpec.Nodes[0].Nodes.Clear();
            string sql = GenerateSpecSql();
            ArrayList arrList = specSourceMange.GetSpecSource(sql);
            if (arrList != null && arrList.Count > 0)
            {
                foreach (SpecSource source in arrList)
                {
                    TreeNode tn = tvSpec.Nodes[0];
                    TreeNode node = new TreeNode();
                    node.Text = source.HisBarCode + " 【" + source.SendTime.ToString() + "】";
                    node.Tag = source;
                    tn.Nodes.Add(node);
                }
            }
            tvSpec.Nodes[0].Text = "已收标本  (共:" + tvSpec.Nodes[0].Nodes.Count.ToString() + " 例)";
            if (tvSpec.Nodes[0].Nodes.Count > 0)
            {
                tvSpec.SelectedNode = tvSpec.Nodes[0].Nodes[0];
            }
        }

        /// <summary>
        /// 绑定病种类型
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

        /// <summary>
        /// 获取登录用户的ID
        /// </summary>
        private void GetLogEmpInfo()
        {
            loginPerson = null;
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            txtOperDoc.Text = loginPerson.Name;
            Dictionary<string, string> dicEMp = new Dictionary<string, string>();
            Dictionary<string, string> dicDept = new Dictionary<string, string>();
            dicEMp.Add(loginPerson.ID, loginPerson.Name);
            dicDept.Add(loginPerson.Dept.ID, loginPerson.Dept.Name);
            BindingSource bs = new BindingSource();
            BindingSource bsDept = new BindingSource();
            bsDept.DataSource = dicDept;
            bs.DataSource = dicEMp;
        } 

        /// <summary>
        /// 初始化当前的分装标本信息
        /// </summary>
        private void InitSpecSourcePlan()
        {
            flpBlood.Controls.Clear();
            SpecTypeManage specTypeManage = new SpecTypeManage();
            ArrayList arrTemp = new ArrayList();
            arrTemp = specTypeManage.GetSpecByOrgName("血");  
            if (arrTemp == null || arrTemp.Count == 0)
            {
                return;
            }
            ucSpecSourceInFlp = new ucSpecSourceForBlood[arrTemp.Count];

            int i = 0;
            foreach (FS.HISFC.Models.Speciment.SpecType s in arrTemp)
            {
                ucSpecSource = new ucSpecSourceForBlood();
                //GroupBox grp = ucSpecSource.Controls[0] as GroupBox;
                //foreach (Control c in grp.Controls)
                //{
                //    if (c.Name == "chkName")
                //    {
                //        CheckBox chk = c as CheckBox;
                //        chk.Text = s.SpecTypeName;
                //        chk.Tag = s.SpecTypeID;
                //    }
                //}
                ucSpecSourceInFlp[i] = ucSpecSource;
                ucSpecSourceInFlp[i].SetSpecTypeName(s.SpecTypeID.ToString());
                ucSpecSourceInFlp[i].Name = "ucSpecSource" + i.ToString();              
                flpBlood.Controls.Add(ucSpecSourceInFlp[i]);            
                i++;               
            }        
        }

        /// <summary>
        /// 更具PatientId获取病人信息
        /// </summary>
        /// <param name="patientId"></param>
        private void GetPatientInfo(string patientId)
        {
            try
            {
                ArrayList arrPatient = specPatientManage.GetPatientInfo("select * from spec_patient where patientid =" + patientId);
                if (arrPatient != null && arrPatient.Count > 0)
                {
                    foreach (SpecPatient s in arrPatient)
                    {
                        specPatient = new SpecPatient();
                        specPatient = s;
                        specPatient.InHosNum = specSource.InPatientNo;
                        ucPatient.IsExisted = true;
                        break;
                    }
                }
                else
                {
                    specPatient = new SpecPatient();
                    //specPatient.PatientID = 0;
                    ucPatient.IsExisted = false;
                }
                ucPatient.SpecPatient = specPatient;
                ucPatient.GetPatient();
            }
            catch
            {
            }

        }

        /// <summary>
        /// 设置分装标本的病种
        /// </summary>
        private void SetSubSourceDis()
        {
            foreach (ucSpecSourceForBlood tmpUc in ucSpecSourceInFlp)
            {                
                tmpUc.DisTypeName = cmbDisType.Text.TrimEnd().TrimStart();
            }
        }

        /// <summary>
        /// 获取分装标本存储信息
        /// </summary>
        /// <param name="specId"></param>
        private void GetSavePlan(string specId)
        {
            //foreach (ucSpecSourceForBlood tmpUc in ucSpecSourceInFlp)
            //{
            //    SpecSourcePlan tmp = new SpecSourcePlan();
            //    tmp = new SpecSourcePlan();
            //    tmpUc.Tag = tmp;
            //    tmpUc.SetSpecSourcePlan(tmp);
            //    tmpUc.Tag = tmp;
            //    tmpUc.DisTypeName = cmbDisType.Text.TrimEnd().TrimStart();
            //}
            string sql = "select distinct * from spec_source_store where specid=" + specId;
            ArrayList arrSpecSourcePlan = specSourcePlanManage.GetSourcePlan(sql);
          
            if (arrSpecSourcePlan == null)
            {

                arrSpecSourcePlan = new ArrayList();
            }
            try
            {
                int i = 0;
                foreach (SpecSourcePlan plan in arrSpecSourcePlan)
                {
                    plan.SubSpecCodeList.Clear();
                    ArrayList arrSubSpec = subSpecManage.GetSubSpecByStoreId(plan.PlanID.ToString());
                    if (arrSubSpec != null)
                    {
                        foreach (SubSpec s in arrSubSpec)
                        {
                            plan.SubSpecCodeList.Add(s.SubBarCode);
                        }
                    }                     
                    for (int k= 0; k < ucSpecSourceInFlp.Length; k++)
                    {
                        ucSpecSourceInFlp[k].DisTypeName = cmbDisType.Text;
                        foreach (Control cp in ucSpecSourceInFlp[k].Controls[0].Controls)
                        {
                            if (cp.Name == "chkName")
                            {
                                CheckBox ck = cp as CheckBox;
                                if (ck.Tag.ToString() == plan.SpecType.SpecTypeID.ToString())
                                {
                                    ucSpecSourceInFlp[k].SetSpecSourcePlan(plan);
                                    break;
                                }
                            }
                        }
                      
                        //ucSpecSourceInFlp[i].Tag = plan;
                    }
                    i++;
                     
                }
                foreach (ucSpecSourceForBlood tmpUc in ucSpecSourceInFlp)
                {
                    tmpUc.DisTypeName = cmbDisType.Text;
                    tmpUc.DisTypeId = cmbDisType.Tag.ToString();
                    tmpUc.SpecId = this.SpecId;
                    SpecSourcePlan tmp = tmpUc.Tag as SpecSourcePlan;
                    if (tmp == null)
                    {
                        tmp = new SpecSourcePlan();
                        tmpUc.Tag = tmp;
                        tmpUc.SetSpecSourcePlan(tmp);
                    }
                }
            }
            catch
            {

            }
        }

        //获取病例诊断信息
        private void GetBaseInfo(string specId)
        {
            string sql = "select * from spec_diagnose where specid=" + specId;
            ArrayList arr = new ArrayList();
            arr = diagManage.GetBaseInfo(sql);
            diagInfo = new SpecDiagnose();
            if (arr != null && arr.Count > 0)
            {
                foreach (SpecDiagnose s in arr)
                {
                    diagInfo = s;
                    break;
                }
            }
            //specSource.InDeptNo = diagInfo.inDeptNo;
            //specSource.MedGrp = diagInfo.medGrp;
            ucDiaInFlp.GetDiagnoseInfo(diagInfo);

        }

        /// <summary>
        /// 获取标本源信息
        /// </summary>
        private void GetSpecSourceInfo(string specSourceID, string specBarCode, SpecSource source)
        {
            ArrayList arrSpecSource = new ArrayList();
            if (source != null)
            {
                specSource = new SpecSource();
                specSource = source;
            }
            if (specSourceID != "")
            {
                arrSpecSource = specSourceMange.GetSpecSource(" select * from spec_source where specid = " + txtSpecCode.Text.Trim());
            }
            if (specBarCode != "")
            {
                arrSpecSource = specSourceMange.GetSpecSource(" select * from spec_source where HISBARCODE = '" + txtBarCode.Text.Trim().PadLeft(12,'0') + "'");
            }
            foreach (SpecSource spec in arrSpecSource)
            {
                specSource = new SpecSource();
                specSource = spec;
                break;
            }
            if (specSource.DiseaseType.DisTypeID != 0)
            {
                cmbDisType.Tag = specSource.DiseaseType.DisTypeID.ToString();
            }
            chkIsHis.Checked = specSource.IsHis == '1' ? true : false;
            specId = specSource.SpecId;
            txtFlowCode.Text = specSource.HisBarCode;
            txtSpecCode.Text = specSource.SpecId.ToString();
            cmbDept.Tag = specSource.DeptNo;
            cmbSendDoc.Tag = specSource.SendDoctor.ID;
            cmbOperDoc.Tag = specSource.SendDoctor.ID;
            txtOperDoc.Text = specSource.OperEmp.Name;
            dtpOperTime.Value = specSource.OperTime;
            dtpSendDate.Value = specSource.SendTime;
            txtComment.Text = specSource.Commet;
            txtOperPos.Text = specSource.OperPosName;
            nudCapcity.Value = specSource.AnticolBldCapcacity;
            nudCount.Value = specSource.AnticolBldCount;
            nudNoCapacity.Value = specSource.NonAntiBldCapcacity;
            nudNoCount.Value = specSource.NonantiBldCount;

            char[] tumorPor = specSource.TumorPor.ToCharArray();
            foreach (char p in tumorPor)
            {
                Constant.TumorPro pro = (Constant.TumorPro)Convert.ToInt32(p.ToString());
                if (pro == Constant.TumorPro.复发癌)
                {
                    chkSecond.Checked = true;
                }
                if (pro == Constant.TumorPro.原发癌)
                {
                    chkFirst.Checked = true;
                }
                if (pro == Constant.TumorPro.转移癌)
                {
                    chkTransfer.Checked = true;
                }
            }
            char[] tumorType = specSource.GetSpecPeriod.ToCharArray();
            foreach (char t in tumorType)
            {
                Constant.GetPeriod period = (Constant.GetPeriod)Convert.ToInt32(t.ToString());
                if (period == Constant.GetPeriod.放疗后)
                {
                    chkRad.Checked = true;
                }
                if (period == Constant.GetPeriod.化疗后)
                {
                    chkMed.Checked = true;
                }
                if (period == Constant.GetPeriod.其它)
                {
                    chkOther.Checked = true;
                }
                if (period == Constant.GetPeriod.无)
                {
                    chkNone.Checked = true;
                }
            }
            txtMedComment.Text = specSource.MedComment;
            GetPatientInfo(specSource.Patient.PatientID.ToString());
            GetSavePlan(txtSpecCode.Text.Trim());
            GetBaseInfo(txtSpecCode.Text.Trim());
        }

        private void InitOtherInfo()
        {
            //初始化送存部门
            FS.HISFC.BizLogic.Manager.DepartmentStatManager manager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            //alDepts = manager.GetMultiDept(loginPerson.ID);
            alDepts = manager.LoadAll();
            this.cmbDept.AddItems(alDepts);
            cmbOperDept.AddItems(alDepts);
            cmbInDept.AddItems(alDepts);
            this.cmbDept.Tag = loginPerson.Dept.ID;

            //初始化送存医生
            FS.HISFC.BizLogic.Manager.Person personList = new FS.HISFC.BizLogic.Manager.Person();
            ArrayList arrPerson = personList.GetEmployeeAll();// GetEmployee(FS.HISFC.Object.Base.EnumEmployeeType.D);
          
            cmbSendDoc.AddItems(arrPerson);
            cmbOperDoc.AddItems(arrPerson);            
            
            cmbOperDept.AddItems(alDepts);
            //加载病人信息控件
            this.flpPatientInfo.Controls.Add(ucPatient);            
            flpDiag.Controls.Add(ucDiaInFlp);
        }

        private int ValidateData()
        {
            if (txtSpecCode.Text.Trim() == "")
            {
                MessageBox.Show("标本号不能为空", title);
                return 0;
            }
            try
            {
                Convert.ToInt32(txtSpecCode.Text.Trim());
            }
            catch
            {
                MessageBox.Show("标本号必须是数字", title);
                return 0;
            }
            if (cmbSendDoc.Text.Trim() == "")
            {
                MessageBox.Show("请选择送存医生", title);
                return 0;
            }
            if (txtSpecCode.Text.Trim() == "")
            {
                MessageBox.Show("标本号不能为空", title);
                return 0;
            }
            if (cmbDept.Text.Trim() == "")
            {
                MessageBox.Show("请选择科室", title);
                return 0;
            }
            tumorPro = "";
            if (chkFirst.Checked) tumorPro = "1";
            if (chkSecond.Checked) tumorPro += "2";
            if (chkTransfer.Checked) tumorPro += "3";
            if (tumorPro != null && tumorPro.Length > 2)
            {
                MessageBox.Show("标本属性不能同时设置2个以上！", title);
                return 0;
            }
            return 1;
        }

        /// <summary>
        ///　原标本设置
        /// </summary>
        private void SpecSourceSet()
        {
            if (oper == "" && !ucPatient.IsExisted)
            {
                specSource.Patient.PatientID = Convert.ToInt32(patientId);
            }
            if (ucPatient.IsExisted)
                specSource.Patient.PatientID = ucPatient.SpecPatient.PatientID;// specPatient.PatientID;
            specSource.SpecId = Convert.ToInt32(txtSpecCode.Text.Trim()); 
            specSource.HisBarCode = txtFlowCode.Text.Trim();
            Employee operDoc = new Employee();
            operDoc.Name = txtOperDoc.Text;
            specSource.OperEmp = operDoc;
            Employee sendDoctor = new Employee();
            if (cmbSendDoc.Tag != null) sendDoctor.ID = cmbSendDoc.Tag.ToString();
            specSource.SendDoctor = sendDoctor;
            specSource.OperTime = dtpOperTime.Value;
            specSource.OrgOrBoold = "B";
            specSource.AnticolBldCapcacity = nudCapcity.Value;
            specSource.AnticolBldCount = Convert.ToInt32(nudCount.Value);
            specSource.NonAntiBldCapcacity = nudNoCapacity.Value;
            specSource.NonantiBldCount = Convert.ToInt32(nudNoCount.Value);
            //Get PatientNo by Blood HisbarCode
            specSource.InPatientNo = specPatient.InHosNum;
            //Get CardNo by Blood HisbarCode
            specSource.CardNo = specPatient.CardNo;
            specSource.Commet += txtComment.Text + "  ";
            if (cmbDept.Tag != null) specSource.DeptNo = cmbDept.Tag.ToString();
            DiseaseType disType = new DiseaseType();
            if (oper != "M")
            {
                disType.DisTypeID = Convert.ToInt32(cmbDisType.Tag.ToString());
                specSource.DiseaseType = disType;
            }            
            specSource.ICCardNO = "";
            specSource.SendTime = dtpSendDate.Value;

            if (chkIsHis.Checked)
                specSource.IsHis = '1';
            else specSource.IsHis = '0';

            //设置病人治疗信息
            string period = "";
            if (chkMed.Checked) period = Convert.ToInt32(Constant.GetPeriod.化疗后).ToString();
            if (chkRad.Checked) period += Convert.ToInt32(Constant.GetPeriod.放疗后).ToString();// "4";            
            if (chkNone.Checked) period += Convert.ToInt32(Constant.GetPeriod.无).ToString();// "8";
            if (chkOther.Checked) period = Convert.ToInt32(Constant.GetPeriod.其它).ToString(); //"7";
            specSource.GetSpecPeriod = period;
            //specSource.RadScheme = ucCureFunInFlp.CureInfo.radScheme;
            //specSource.MedScheme = ucCureFunInFlp.CureInfo.medScheme;
            specSource.OperPosName = txtOperPos.Text;
            specSource.MedComment = txtMedComment.Text;

            //设置病人诊断信息    
            if (cmbDept.Tag != null && cmbDept.Text.Trim() != "")
            {
                specSource.InDeptNo = cmbDept.Tag.ToString();
            }
            if (cmbGrp.Tag != null && cmbGrp.Text.Trim() != "")
            {
                specSource.MedGrp = cmbGrp.Tag.ToString();
            }
            specSource.TumorPor = tumorPro;
            if (cmbOperDoc.Tag != null && cmbOperDoc.Text.Trim() != "")
            {
                specSource.MediDoc.MainDoc.ID = cmbOperDoc.Tag.ToString();
                specSource.MediDoc.MainDoc.Name = cmbOperDoc.Text;
            }  
        }

        #endregion

        # region 保存从页面上读取的实体信息
        /// <summary>
        /// 保存计划取的标本实体
        /// </summary>
        /// <returns></returns>       
        private int SaveSpecSourcePlan()
        {
            int result = 0;
            foreach (ucSpecSourceForBlood c in ucSpecSourceInFlp)
            {
                GroupBox grp = c.Controls[0] as GroupBox;

                c.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                if (oper != "M")
                {
                    c.DisTypeId = cmbDisType.Tag.ToString();
                }
                char isHis = chkIsHis.Checked ? '1' : '0';
                string tumorType = chkIsCancer.Checked ? Convert.ToInt32(Constant.TumorType.肿物).ToString() : Convert.ToInt32(Constant.TumorType.正常.ToString()).ToString();
                c.SpecId = Convert.ToInt32(txtSpecCode.Text.Trim());

                if (oper == "")
                {
                    result = c.SaveSourcePlan(isHis, tumorType);
                }
                if (oper == "M")
                {
                    result = c.UpdateSourcePlan(isHis, tumorType);
                }
                if (result <= -1)
                {
                    MessageBox.Show("更新分装标本失败！", title);                  
                }
            }
            return result;
        }

        /// <summary>
        /// 保存原标本
        /// </summary>
        private int SaveSpecSource()
        {
            SpecSourceSet();
            int sourceResult = -1;
            for (int i = 0; i < flpBlood.Controls.Count; i++)
            {
                try
                {
                    string seq = (flpBlood.Controls[0] as ucSpecSourceForBlood).Seq.Substring(0,6);
                    if (seq != null && seq != string.Empty)
                    {
                        specSource.SpecNo = seq;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                catch
                {
                    continue;
                }
            }

            if (specSource.SpecNo.Trim() == string.Empty)
            {
                MessageBox.Show("获取标本号失败！");
                return -1;
            }

            if (oper == "")
            {
                specSource.ColDoctor.ID = loginPerson.ID;
                specSource.OperEmp.Name = loginPerson.Name;
                sourceResult = specSourceMange.InsertSpecSource(specSource);
            }
            if (oper == "M")
            {
                sourceResult = specSourceMange.UpdateSpecSource(specSource);
            }
            if (sourceResult <= -1)
            {
                MessageBox.Show("更新原标本失败!", title);
            }
            return sourceResult;  
        }

        /// <summary>
        /// 保存病人信息
        /// </summary>
        private int SavePatientInfo()
        {
            //在ucPatientInfo用户控件获取病人信息
            specPatient = ucPatient.SetPatient();          
            int patientResult = -1;
            if (!ucPatient.IsExisted)
            {
                specPatientManage.GetNextSequence(ref patientId);
                specPatient.PatientID = Convert.ToInt32(patientId);
                patientResult = specPatientManage.InsertPatient(specPatient);
                if (patientResult == 1)
                    ucPatient.IsExisted = true;
                return patientResult;
            }
            //如果标本库中已经存在该病人的信息，则根据页面设置的信息 更新
            if (ucPatient.IsExisted)
            {
                if (specPatient.PatientID <= 0 && patientId != "")
                {
                    specPatient.PatientID = Convert.ToInt32(patientId);
                }
                //this.specPatient = ucPatient.SpecPatient;
                patientResult = specPatientManage.UpdatePatient(specPatient);
                return patientResult;
            }
            if (patientResult == -1)
            {
                MessageBox.Show("病人信息插入失败！", title);
            }
            return patientResult;
        }

        /// <summary>
        /// 保存诊断信息
        /// </summary>
        /// <returns></returns>
        private int SaveDiagnoseInfo()
        {
           //是否录入了诊断
            if (ucDiaInFlp.IsInputDiagnose())
            {
                diagInfo = new SpecDiagnose();
                diagInfo = ucDiaInFlp.SpecDiag;
                diagInfo.SpecSource.SpecId = Convert.ToInt32(txtSpecCode.Text.Trim());

                //设置病人诊断信息                  
                int result = -1;
                string sql = "select * from spec_diagnose where specid=" + txtSpecCode.Text.Trim();
                ArrayList arr = diagManage.GetBaseInfo(sql); 
                if (oper == "" || arr.Count <= 0)
                {
                    string sequence = "";
                    diagManage.GetNextSequence(ref sequence);                 
                    diagInfo.BaseID = Convert.ToInt32(sequence);
                    result = diagManage.InsertDiagnose(diagInfo);
                }
                else
                {
                    result = diagManage.UpdateDiagnose(diagInfo);
                }               
                int updateResult = specSourceMange.UpdateInBase(txtSpecCode.Text.Trim());
                if (result <= -1)
                {
                    MessageBox.Show("诊断信息插入失败！", title);
                }
                return result;
                
               // 要设置 diagInfo.Main_DiagState;
            } 
               
            return 0;
        }
        #endregion

        private void ResetForm()
        {
            //flpPatientInfo.Controls.Clear();
            //ucPatient = new ucPatientInfo();
            ////加载新的病人信息
            //flpPatientInfo.Controls.Add(ucPatient);
            ucPatient.ClearControlContent();
            InitSpecSourcePlan();
            ucDiaInFlp.ClearContent();
            chkFirst.Checked = false;
            chkSecond.Checked = false;
            chkTransfer.Checked = false;
            chkMed.Checked = false;
            chkRad.Checked = false;
            chkOther.Checked = false;
            chkNone.Checked = false;
            txtOperPos.Text = "";
            txtComment.Text = "";
            txtOperDoc.Text = loginPerson.Name;
            txtFlowCode.Text = "";
            cmbSendDoc.Tag = "";
            cmbSendDoc.Text = "";
            cmbOperDoc.Tag = "";
            cmbOperDoc.Text = "";
            txtMedComment.Text = "";
            //flpDiagInfo.Controls.Clear();
            //ucDiaInFlp = new ucDiagnose();
            //flpDiagInfo.Controls.Add(ucDiaInFlp);

        }

        private void PrintLocation()
        {
            ArrayList arrSubSpec = new ArrayList();
            arrSubSpec = subSpecManage.GetSubSpecBySpecId(txtSpecCode.Text.Trim());
            if (arrSubSpec == null)
            {
                return;
            }
            List<SubSpec> subSpecList = new List<SubSpec>();
            foreach (SubSpec s in arrSubSpec)
                subSpecList.Add(s);
            if (subSpecList.Count > 0)
            {
                SpecOutOper oper = new SpecOutOper();
                oper.PrintOutSpec(subSpecList, null);
            }
        }

        /// <summary>
        /// 取消样本
        /// </summary>
        private void CancelSpec()
        {
            TreeNode tn = tvSpec.SelectedNode;
            if (tn == null)
            {
                MessageBox.Show("请选择需要取消的样本");
                return;
            }
            if (tn.Tag == null)
            {
                MessageBox.Show("获取样本信息失败");
                return;
            }

            try
            {
                OperApply operApply = tn.Tag as OperApply;
                if (operApplyManage.UpdateColFlag(operApply.OperApplyId.ToString(), "3") == -1)
                {
                    MessageBox.Show("操作失败");
                    return;
                }
                MessageBox.Show("操作成功");
            }
            catch
            {
                MessageBox.Show("已送标本不能取消！");
            }
        }

        private void barCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtBarCode.Text.Trim() != "")
                {
                    ResetForm();
                    GetSpecSourceInfo("", "barCode",null);
                    oper = "M";
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucSpecimentSource_Load(object sender, EventArgs e)
        {
            grp.Dock = DockStyle.Top;
            DisTypeBinding();
            GetLogEmpInfo();
            InitSpecSourcePlan();
            InitOtherInfo();
            if (SpecId > 0)
            {
                try
                {
                    txtSpecCode.Text = specId.ToString();
                    GetSpecSourceInfo("SpecId", "",null);
                    btnSave.Visible = true;
                    btnNew.Visible = false;
                    btnPrint.Visible = true;
                    oper = "M";
                    return;
                }
                catch
                {

                }
            }
            LoadSpecSource();
            LoadOperApply();
        }

        /// <summary>
        /// 保存标本信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Save(object sender, object neuObject)
        {
            int result = this.ValidateData();
            if (oper == "" && specSourceMange.ChkSpecExsit(specId.ToString()))
            {
                MessageBox.Show("原标本号存在，请重新设置！", title);
                return -1;
            }
            if (result == 0)
                return -1;

            FS.FrameWork.Management.PublicTrans.BeginTransaction(); 
         
            try
            {
                specSourceMange.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);              
                diagManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                specPatientManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                int patientInsert = SavePatientInfo();
                int soureceInsert = SaveSpecSource();
                int planInsert = SaveSpecSourcePlan();
                int baseInsert = SaveDiagnoseInfo();

                if (patientInsert == -1 || soureceInsert == -1 || planInsert == -1 || baseInsert == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("操作失败！", title);
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.Commit();

                MessageBox.Show("操作成功！", title);                   
                //当前标本盒已满
                
                //ucPatient.IsExisted = false;
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(ex.Message, title);
                return -1;
            }
            foreach (Control c in flpBlood.Controls)
            {
                ucSpecSourceForBlood bld = c as ucSpecSourceForBlood;
                if (bld.UseFullBoxList != null)
                {
                    foreach (SpecBox box in bld.UseFullBoxList)
                    {
                        MessageBox.Show(box.BoxBarCode + " 标本盒已满，请添加新的标本盒！", title);
                        //提示用户添加新的标本盒
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
                    bld.UseFullBoxList = null;
                }
            }
            oper = "M";
            txtSpecCode.Enabled = false;
            return base.Save(sender, neuObject);
        }

        /// <summary>
        /// 科室下拉列表Change事件，送存医生随着科室选择而变动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {

            //FS.HISFC.Management.Manager.Person personList = new FS.HISFC.Management.Manager.Person();
            //ArrayList arrPerson = personList.GetEmployee(EnumEmployeeType.D, cmbDept.Tag.ToString());
            //cmbSendDoc.AddItems(arrPerson);
        }

        private void txtFile_Click(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            specSource = new SpecSource();
            specPatient = new SpecPatient();
            ucPatient.IsExisted = false;
            string sequence = "";
            specSourceMange.GetNextSequence(ref sequence);
            specId = Convert.ToInt32(sequence);
            txtSpecCode.Text = sequence;
            ResetForm();
            oper = "";
            SetSubSourceDis();
        }

        private void txtSpecCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                try
                {
                    specId = Convert.ToInt32(txtSpecCode.Text.Trim());
                }
                catch
                {
                    MessageBox.Show("标本号必须为数字！", title);
                    return;
                }

                try
                {
                    ResetForm();
                    GetSpecSourceInfo("specId", "",null);
                }
                catch
                {

                }
            }
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("查询列表", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);
            this.toolBarService.AddToolButton("修改", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            this.toolBarService.AddToolButton("打印位置信息", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton("取消样本", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);
            this.toolBarService.AddToolButton("新建", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);

            return this.toolBarService;
            //return base.OnInit(sender, neuObject, param);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "查询列表":
                    this.QueryList();
                    break;
                case "修改":
                    txtSpecCode.Enabled = true;
                    oper = "M";
                    break;
                case "打印位置信息":
                    PrintLocation();
                    break;
                case "取消样本":
                    CancelSpec();
                    Query(null, null);
                    break;
                case "新建":
                    this.ResetForm();
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Save(sender, null);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.PrintLocation();
        }

        public override int Query(object sender, object neuObject)
        {
            try
            {
                if (txtBarCode.Text.Trim() != "")
                {
                    ResetForm();
                    GetSpecSourceInfo("", "barCode", null);
                    oper = "M";
                }
                else
                {
                    if (cmbDisType.Tag == null)
                    {
                        MessageBox.Show("请选择病种");
                        return -1;
                    }
                    else
                    {
                        string tmpId = specSourceMange.ExecSqlReturnOne("select SPECID from SPEC_SOURCE where ORGORBLOOD = 'B' and SPEC_NO = '" + txtSpecNo.Text.PadLeft(6, '0') + "' and DISEASETYPEID = " + cmbDisType.Tag.ToString());
                        if (tmpId == "-1")
                            return -1;
                        txtSpecCode.Text = tmpId;
                        this.specId = Convert.ToInt32(tmpId);
                        this.txtSpecCode_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
                    }
                }
            }
            catch
            {
            }
            return base.Query(sender, neuObject);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        private void QueryList()
        {
            for (int i = 0; i < tvSpec.Nodes.Count; i++)
            {
                tvSpec.Nodes[i].Nodes.Clear();
            }
            LoadSpecSource();
            LoadOperApply();
        }

        private void tvSpec_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                this.ResetForm();
                TreeNode node = tvSpec.SelectedNode;
                object tag = node.Tag;
                if (tag != null)
                {
                    if (tag.GetType().ToString().Contains("SpecSource"))
                    {
                        SpecSource spec = new SpecSource();
                        spec = tag as SpecSource;
                        if (spec == null)
                        {
                            return;
                        }
                        oper = "M";
                        GetSpecSourceInfo("", "", spec);
                        if (spec.InPatientNo != "")
                        {
                            ucPatient.GetPatient(spec.InPatientNo);
                        }
                    }
                }
            }
            catch
            {
 
            }
        }

        private void cmbDisType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (oper == "M")
            {
                SetSubSourceDis();
            }
            if (ucSpecSourceInFlp != null)
            {
                for (int i = 0; i < ucSpecSourceInFlp.Length; i++)
                {
                    ucSpecSourceInFlp[i].DisTypeName = cmbDisType.Text;
                    ucSpecSourceInFlp[i].DisTypeId = cmbDisType.Tag.ToString();
                    //ucSpecSourceInFlp[i].Tag = plan;
                    //}
                }
            }

             
        }

        private void txtSpecNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmbDisType.SelectedValue == null)
                {
                    MessageBox.Show("请选择病种");
                    return;
                }
                else
                {
                    string tmpId = specSourceMange.ExecSqlReturnOne("select SPECID from SPEC_SOURCE where ORGORBLOOD = 'B' and SPEC_NO = '" + txtSpecNo.Text.PadLeft(6, '0') + "' and DISEASETYPEID = " + cmbDisType.SelectedValue.ToString());
                    if (tmpId == "-1")
                        return;
                    txtSpecCode.Text = tmpId;
                    this.specId = Convert.ToInt32(tmpId);
                    this.txtSpecCode_KeyDown(sender, e);
                }
            }
        }
    }
}
