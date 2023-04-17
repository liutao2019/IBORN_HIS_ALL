using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.DCP.Controls
{
    /// <summary>
    /// ucDiseaseInfo<br></br>
    /// [功能描述: 疾病信息uc]<br></br>
    /// [创 建 者: zj]<br></br>
    /// [创建时间: 2008-09-17]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='' 
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucDiseaseInfo : ucBaseMainReport
    {
        public ucDiseaseInfo()
        {
            InitializeComponent();

            this.cmbInfectionClass.SelectedValueChanged += new EventHandler(cmbInfectionClass_SelectedValueChanged);
           // this.cmbInfectionClass.Enter += new EventHandler(cmbInfectionClass_Enter);
        }

        #region 域变量

        /// <summary>
        /// 选择节点委托
        /// </summary>
        /// <param name="patient"></param>
        public delegate void AddAddtion(bool isNeed,ArrayList al);

        /// <summary>
        /// 显示事件
        /// </summary>
        public event AddAddtion AdditionEvent;

        /// <summary>
        /// 疾病类型 分类
        /// </summary>
        private Dictionary<FS.SOC.HISFC.Components.DCP.Classes.EnumAdditionReportMsg, Hashtable> diseaseDictionary = new Dictionary<Classes.EnumAdditionReportMsg, Hashtable>();

        private Hashtable diseaseHt = new Hashtable();

        /// <summary>
        /// 所有传染病，用于弹出窗口
        /// </summary>
        private ArrayList alInfectItem = new ArrayList();

        /// <summary>
        /// 所有传染病，用于下拉
        /// </summary>
        private ArrayList alinfection = new ArrayList();

        /// <summary>
        /// 需要附卡的疾病
        /// </summary>
        private System.Collections.Hashtable hshNeedAdd;

        /// <summary>
        /// 传染病的类型[甲乙丙等]，检测选择传染病时是否选择了类型
        /// </summary>
        private System.Collections.Hashtable hshInfectClass;

        /// <summary>
        /// 需要报告性病卡的疾病
        /// </summary>
        private System.Collections.Hashtable hshNeedSexReport;

        /// <summary>
        /// 需要采血送检
        /// </summary>
        private System.Collections.Hashtable hshNeedCheckedBlood;

        /// <summary>
        /// 需要二级病例分类
        /// </summary>
        private System.Collections.Hashtable hshNeedCaseTwo;

        /// <summary>
        /// 需要电话报告的疾病
        /// </summary>
        private System.Collections.Hashtable hshNeedTelInfect;

        /// <summary>
        /// 需要结核病转诊单的疾病
        /// </summary>
        private System.Collections.Hashtable hshNeedBill;

        /// <summary>
        /// 需要备注的疾病
        /// </summary>
        private System.Collections.Hashtable hshNeedMemo;

        /// <summary>
        /// 新生儿破伤风
        /// </summary>
        private System.Collections.Hashtable hshLitteChild;

        /// <summary>
        /// 患者职业为学生[应提示填写学校机构之类]
        /// </summary>
        private System.Collections.Hashtable hshStudent;

        /// <summary>
        /// 需要二级名称的性病
        /// </summary>
        private System.Collections.Hashtable hshSexNeedGradeTwo;

        /// <summary>
        /// 需要描述的人群分类
        /// </summary>
        private System.Collections.Hashtable hshPatientTyepNeedDesc;

        /// <summary>
        /// 需个案调查
        /// </summary>
        private System.Collections.Hashtable hsInfomationQuery;

        /// <summary>
        /// 需乙型肝炎附卡的疾病类型
        /// </summary>
        private System.Collections.Hashtable hsNeedHepatitisBReport;


        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.SOC.HISFC.BizProcess.DCP.Common commonProcess = new FS.SOC.HISFC.BizProcess.DCP.Common();

        private ucOtherDisease otherDisease = null;
        private ucVenerealDisease venerealDisease = null;
        private ucHepatitisB ucHepatitisB = null;

        private bool isShow = false;
        #endregion

        #region 属性

        public bool InfectionClassEnable
        {
            get 
            {
                return this.cmbInfectionClass.Enabled;
            }
        }

        private string infectCode = "";
        /// <summary>
        /// 指定疾病编码
        /// </summary>
        public string InfectCode
        {
            get { return infectCode; }
            set
            {
                infectCode = value;

                this.cmbInfectionClass.ClearItems();
                string[] infectCodes = this.infectCode.Split(',');

                if (infectCodes != null && infectCodes.Length > 1)
                {
                    ArrayList alTmp = new ArrayList();
                    this.cmbInfectionClass.Enabled = true;
                    foreach (string code in infectCodes)
                    {
                        foreach (FS.HISFC.Models.Base.Const disease in this.alInfectItem)
                        {
                            if (code == disease.ID)
                            {
                                alTmp.Add(disease);
                                break;
                            }
                        }
                    }
                    this.cmbInfectionClass.AddItems(alTmp);
                 
                    //this.cmbInfectionClass.Tag = ob.ID;

                    //二级分类
                    //this.cmbCaseClaseTwo.Enabled = this.hshNeedCaseTwo.Contains(ob.ID);
                    //this.cmbInfectionClass.Enabled = true;
                }
                else
                {
                    this.cmbInfectionClass.AddItems(alInfectItem);
                    this.cmbInfectionClass.Tag = this.infectCode;
                }
            }
        }

        public Hashtable HshNeedTelInfect
        {
            get
            {
                return this.hshNeedTelInfect;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns>-1 失败 1 成功</returns>
        public override int Init(DateTime sysdate)
        {
            base.Init(sysdate);//先初始化基类的方法。

            this.dtDiaDate.Value = sysdate;
            this.dtInfectionDate.Value = sysdate;

            if (this.InitInfections()== -1)
            {
                return -1;
            }
            if (this.InitInfectOne() == -1)
            {
                return -1;
            }
            if (this.InitInfectTwo() == -1)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 初始化传染病疾病名称
        /// </summary>
        /// <returns>-1 失败 1成功</returns>
        private int InitInfect()
        {
            //List<FS.HISFC.Models.Base.Const> listInfectClass = Classes.Function<FS.HISFC.Models.Base.Const>.ConvertToList(this.commonProcess.QueryConstantList("INFECTCLASS"));
            //if (listInfectClass == null)
            //{
            //    MessageBox.Show(FS.FrameWork.Management.Language.Msg("获取疾病分类错误！"));
            //    return -1;
            //}

            //ArrayList al=new ArrayList();
            ////加载疾病分类常数
            //foreach (FS.HISFC.Models.Base.Const con in listInfectClass)
            //{
            //    List<FS.HISFC.Models.Base.Const> listInfect = Classes.Function<FS.HISFC.Models.Base.Const>.ConvertToList(this.commonProcess.QueryConstantList(con.ID));
            //    if (listInfect == null)
            //    {
            //        MessageBox.Show(FS.FrameWork.Management.Language.Msg("获取"+con.Name+"错误！"));
            //        return -1;
            //    }
            //    if (this.diseaseHt.ContainsKey(con))
            //    {
            //        List<FS.HISFC.Models.Base.Const> list = (List<FS.HISFC.Models.Base.Const>)this.diseaseHt[con];
            //        list.AddRange(listInfect);
            //        this.diseaseHt[con] = list;
            //    }
            //    else
            //    {
            //        this.diseaseHt.Add(con, listInfect);
            //    }

            //    al.AddRange(listInfect);

            //    #region 加载附卡提示常数

            //    foreach (FS.HISFC.Models.Base.Const conMemo in listInfect)
            //    {
            //        if (conMemo.Memo.IndexOf(Classes.EnumAdditionReportMsg.NeedSexReport.ToString()) > 0)
            //        {
            //            if (this.SetAddMsgConst(FS.SOC.HISFC.Components.DCP.myDiseaseReport.Classes.EnumAdditionReportMsg.NeedSexReport, conMemo) == 0)
            //            {
            //                continue;
            //            }
            //        }
            //        if (conMemo.Memo.IndexOf(Classes.EnumAdditionReportMsg.NeedAdditionReport.ToString()) > 0)
            //        {
            //            if (this.SetAddMsgConst(FS.SOC.HISFC.Components.DCP.myDiseaseReport.Classes.EnumAdditionReportMsg.NeedAdditionReport, conMemo) == 0)
            //            {
            //                continue;
            //            }
            //        }
            //        if (conMemo.Memo.IndexOf(Classes.EnumAdditionReportMsg.NeedPhoneNotice.ToString()) > 0)
            //        {
            //            if (this.SetAddMsgConst(FS.SOC.HISFC.Components.DCP.myDiseaseReport.Classes.EnumAdditionReportMsg.NeedPhoneNotice, conMemo) == 0)
            //            {
            //                continue;
            //            }
            //        }
            //        if (conMemo.Memo.IndexOf(Classes.EnumAdditionReportMsg.NeedWriteBill.ToString()) > 0)
            //        {
            //            if (this.SetAddMsgConst(FS.SOC.HISFC.Components.DCP.myDiseaseReport.Classes.EnumAdditionReportMsg.NeedWriteBill, conMemo) == 0)
            //            {
            //                continue;
            //            }
            //        }
            //    }

            //    #endregion
            //}

            //this.cmbInfectionClass.AddItems(al);

            return 1;


        }

        /// <summary>
        /// 初始化病例分类1
        /// </summary>
        /// <returns>-1 失败 1成功</returns>
        private int InitInfectOne()
        {
            ArrayList alInfectOne = this.commonProcess.QueryConstantList("CASECLASS");
            if (alInfectOne == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("获取病例分类1出错！"));
                return -1;
            }
            this.cmbCaseClassOne.AddItems(alInfectOne);
            return 1;
        }

        /// <summary>
        /// 初始化病例分类2
        /// </summary>
        /// <returns>-1 失败 1成功</returns>
        private int InitInfectTwo()
        {
            ArrayList altwo = new ArrayList();
            FS.HISFC.Models.Base.Const obone = new FS.HISFC.Models.Base.Const();

            //altwo.Add(obj);
            FS.HISFC.Models.Base.Const obthree = new FS.HISFC.Models.Base.Const();
            obthree.ID = "2";
            obthree.Name = "未分型";
            altwo.Add(obthree);

            obone.ID = "0";
            obone.Name = "急性";
            altwo.Add(obone);

            FS.HISFC.Models.Base.Const obtwo = new FS.HISFC.Models.Base.Const();
            obtwo.ID = "1";
            obtwo.Name = "慢性";
            altwo.Add(obtwo);

            this.cmbCaseClaseTwo.AddItems(altwo);
            return 1;
        }

        /// <summary>
        /// 初始化疾病
        /// </summary>
        private int InitInfections()
        {

            //传染病的类型
            ArrayList alInfectClass = new ArrayList();

            alInfectClass.AddRange(commonProcess.QueryConstantList("INFECTCLASS"));
            if (alInfectClass==null)
            {
                return -1;
            }

            //需要附卡的传染病
            this.hshNeedAdd = new Hashtable();
            //类型
            this.hshInfectClass = new Hashtable();
            //需要报性病卡的疾病
            this.hshNeedSexReport = new Hashtable();
            //需要采血送检的疾病

            this.hshNeedCheckedBlood = new Hashtable();
            //需要二级病例的疾病
            this.hshNeedCaseTwo = new Hashtable();
            //需要电话报告的疾病
            this.hshNeedTelInfect = new Hashtable();
            //需要填写结核病转诊单的疾病
            this.hshNeedBill = new Hashtable();
            //新生儿破伤风
            this.hshLitteChild = new Hashtable();
            //需要二级名称的性病
            this.hshSexNeedGradeTwo = new Hashtable();
            //需要人群分类描述
            this.hshPatientTyepNeedDesc = new Hashtable();
            //需要备注
            this.hshNeedMemo = new Hashtable();
            //需个案调查
            this.hsInfomationQuery = new Hashtable();
            //需要乙肝附卡的疾病
            this.hsNeedHepatitisBReport = new Hashtable();

            //根据类型获取传染病

            int index = 1;
            foreach (FS.HISFC.Models.Base.Const infectclass in alInfectClass)
            {
                ArrayList al = new ArrayList();
                ArrayList alItem = new ArrayList();


                infectclass.Name = "--" + infectclass.Name + "--";
                infectclass.Name = infectclass.Name.PadLeft(13, ' ');
                al.Add(infectclass);
                if (index == 1)
                {
                    FS.HISFC.Models.Base.Const o = new FS.HISFC.Models.Base.Const();
                    o.ID = "####";
                    o.Name = "--请选择--";
                    al.Insert(0, o);
                    index++;
                }
                alItem = commonProcess.QueryConstantList(infectclass.ID);

                al.AddRange(alItem);
                alInfectItem.AddRange(alItem);

                hshInfectClass.Add(infectclass.ID, null);
                foreach (FS.HISFC.Models.Base.Const infect in al)
                {
                    //名称过长，维护在备注里，在此交换
                    if (infect.Name.IndexOf("备注", 0) != -1)
                    {
                        infect.Name = infect.Memo;
                        infect.Memo = "";
                    }
                    if (infect.Memo.IndexOf("需附卡", 0) != -1)
                    {
                        hshNeedAdd.Add(infect.ID, null);
                    }
                    if (infect.Memo.IndexOf("需性病报告", 0) != -1)
                    {
                        hshNeedSexReport.Add(infect.ID, null);
                    }
                    if (infect.Memo.IndexOf("需备注") != -1)
                    {
                        hshNeedMemo.Add(infect.ID, null);
                    }
                    //性病二级名称
                    if (infect.Memo.IndexOf("二级名称", 0) != -1)
                    {
                        hshSexNeedGradeTwo.Add(infect.ID, null);
                    }
                    if (infect.Memo.IndexOf("需采血送检", 0) != -1)
                    {
                        hshNeedCheckedBlood.Add(infect.ID, null);
                    }
                    //二级病例分类
                    if (infect.Memo.IndexOf("病例分类", 0) != -1)
                    {
                        hshNeedCaseTwo.Add(infect.ID, null);
                    }
                    //电话通知
                    if (infect.Memo.IndexOf("需电话通知", 0) != -1)
                    {
                        hshNeedTelInfect.Add(infect.ID, null);
                    }
                    //结核病转诊单
                    if (infect.Memo.IndexOf("需转诊单", 0) != -1)
                    {
                        hshNeedBill.Add(infect.ID, null);
                    }
                    if (infect.Memo.IndexOf("新生儿破伤风", 0) != -1 || infect.Name.IndexOf("新生儿破伤风", 0) != -1)
                    {
                        hshLitteChild.Add(infect.ID, null);
                    }

                    if (infect.Memo.IndexOf("需个案调查", 0) != -1 )
                    {
                        hsInfomationQuery.Add(infect.ID, null);
                    }
                    if (infect.Memo.IndexOf("需乙肝附卡", 0) != -1)
                    {
                        hsNeedHepatitisBReport.Add(infect.ID, null);
                    }
                }
                alinfection.AddRange(al);
                FS.FrameWork.Models.NeuObject ob = new FS.FrameWork.Models.NeuObject();
                ob.ID = "####";
                ob.Name = "    ";
                alinfection.Add(ob);
            }
            this.cmbInfectionClass.AddItems(alinfection);
            if (!string.IsNullOrEmpty(this.infectCode))
            {
                this.cmbInfectionClass.Tag = this.infectCode;
            }

            return 1;
        }


        /// <summary>
        /// 添加附卡提示常数
        /// </summary>
        /// <returns></returns>
        public int SetAddMsgConst(FS.SOC.HISFC.Components.DCP.Classes.EnumAdditionReportMsg enumAddition,FS.HISFC.Models.Base.Const conMemo)
        {
            Hashtable hs = (Hashtable)this.diseaseDictionary[enumAddition];
            if (hs == null)
            {
                hs = new Hashtable();
            }
            else if(hs.ContainsKey(conMemo.ID))
            {
                return 0;
            }
            else
            {
                hs.Add(conMemo.ID, conMemo.Name);                
            }

            this.diseaseDictionary.Add(enumAddition, hs);

            return 1;
        }

        /// <summary>
        /// 赋疾病信息
        /// </summary>
        /// <returns></returns>
        public override int SetValue(FS.HISFC.DCP.Object.CommonReport report)
        {
            try
            {
                this.cmbInfectionClass.Tag = report.Disease.ID;
                this.cmbCaseClassOne.Tag = report.CaseClass1.ID;
                this.cmbCaseClaseTwo.Tag = report.CaseClass2;
                this.dtInfectionDate.Value = report.InfectDate;
                this.dtDiaDate.Value = report.DiagnosisTime;

                if (report.DeadDate > new DateTime(1753, 1, 1))
                {
                    this.dtDeadDate.Checked = true;
                    this.dtDeadDate.Value = report.DeadDate;
                }
                else
                {
                    this.dtDeadDate.Checked = false;
                }
                
                if (FS.FrameWork.Function.NConvert.ToBoolean(report.InfectOtherFlag))
                {
                    this.rdbInfectOtherYes.Checked = true;
                }
                else
                {
                    this.rdbInfectOtherYes.Checked = false;
                }
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg(e.Message));
                return -1;
            }
        }

        public override int SetValue(FS.HISFC.Models.RADT.Patient patient, FS.SOC.HISFC.DCP.Enum.PatientType patientType)
        {
            this.dtDeadDate.Checked = false;
            return base.SetValue(patient, patientType);
        }

        /// <summary>
        /// 取疾病信息
        /// </summary>
        /// <returns></returns>
        public override int GetValue(ref FS.HISFC.DCP.Object.CommonReport report)
        {
            try
            {
                if (this.cmbInfectionClass.Tag == null || this.cmbInfectionClass.Tag.ToString()=="")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择《疾病名称》"));
                    this.cmbInfectionClass.Select();
                    this.cmbInfectionClass.Focus();
                    return -1;
                }
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                if (this.GetDisease(ref obj) == -1)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择《疾病名称》"));
                    this.cmbInfectionClass.Select();
                    this.cmbInfectionClass.Focus();
                    return -1;
                }
                report.Disease = obj;


                if (this.cmbCaseClassOne.Tag == null||this.cmbCaseClassOne.Tag.ToString()=="")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择《病例分类》"));
                    this.cmbCaseClassOne.Select();
                    this.cmbCaseClassOne.Focus();
                    return -1;
                }
                report.CaseClass1 = this.cmbCaseClassOne.SelectedItem;

                if (this.cmbCaseClaseTwo.Tag != null)
                {
                    report.CaseClass2 = this.cmbCaseClaseTwo.Tag.ToString();
                }

                //if (this.cmbInfectionClass.Tag == null)
                //{
                //    MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择《疾病名称》"));
                //    this.cmbInfectionClass.Select();
                //    this.cmbInfectionClass.Focus();
                //    return -1;
                //}
                //report.Disease = this.cmbInfectionClass.SelectedItem;

                //日期逻辑处理
                if (this.dtInfectionDate.Value.Date>this.dtDiaDate.Value.Date)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择发病日期\n注意：发病日期不大于诊断日期"));
                    this.dtInfectionDate.Select();
                    this.dtInfectionDate.Focus();
                    return -1;
                }
                if (this.dtDiaDate.Value.Date > this.sysdate.Date)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("诊断日期超过了今天"));
                    this.dtInfectionDate.Select();
                    this.dtInfectionDate.Focus();
                    return -1;
                }
                if (this.dtDiaDate.Value.Date<this.dtInfectionDate.Value.Date)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("诊断日期应大于发病日期"));
                    this.dtInfectionDate.Select();
                    this.dtInfectionDate.Focus();
                    return -1;
                }
                //死亡日期
                if (this.dtDeadDate.Checked)
                {
                    if (this.dtDiaDate.Value.Date< this.dtDeadDate.Value.Date)
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("诊断日期应大于死亡日期"));
                        this.dtDeadDate.Select();
                        this.dtDeadDate.Focus();
                        return -1;
                    }
                    if (this.dtDeadDate.Value.Date<this.dtInfectionDate.Value.Date)
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("死亡日期应大于发病日期"));
                        this.dtDeadDate.Select();
                        this.dtDeadDate.Focus();
                        return -1;
                    }

                    report.DeadDate = this.dtDeadDate.Value;
                }
                report.DiagnosisTime = this.dtDiaDate.Value;
                report.InfectDate = this.dtInfectionDate.Value;

                if (this.rdbInfectOtherYes.Checked)
                {
                    report.InfectOtherFlag = "1";
                }
                else
                {
                    report.InfectOtherFlag = "0";
                }

                if (this.hshNeedBill.Contains(report.Disease.ID))
                {
                    if (!isShow)
                    {
                        this.MyMessageBox("请填写《结核病转诊单》\n结核病人请转至结核病防治中心治疗", "提示>>");
                        isShow = true;
                    }
                    if (report.Memo != string.Empty)
                    {
                        if (report.Memo.IndexOf("已转诊") == -1)
                        {
                            report.Memo = "已转诊\\\\" + report.Memo;
                        }
                    }
                    else
                    {
                        report.Memo = "已转诊\\\\";
                    }
                }

                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg(e.Message));
                return -1;
            }
        }

        /// <summary>
        /// 获取疾病信息
        /// </summary>
        /// <param name="disease"></param>
        private int GetDisease(ref FS.FrameWork.Models.NeuObject disease)
        {
            if (this.cmbInfectionClass.Tag.ToString() == "####"
                //|| hshInfectClass.Contains(this.cmbInfectionClass.SelectedValue.ToString())
                )
            {
                return -1;
            }
            // if (this.rdbInfectionClass.Checked)
            {
                disease.ID = this.cmbInfectionClass.Tag.ToString();
                string diseasename = (string)this.cmbInfectionClass.Text;
                if (diseasename != null && diseasename != "")
                {
                    disease.Name = diseasename;
                }
                else
                {
                    disease.Name = this.cmbInfectionClass.Text;
                }
            }
            disease.Memo = disease.ID.Substring(0, 1);
            return 0;
        }

        public override void Clear()
        {
            this.cmbCaseClaseTwo.Tag = "";
            this.cmbCaseClaseTwo.Text = "";
            this.cmbCaseClassOne.Tag = "";
            this.cmbCaseClassOne.Text = "";
            this.cmbInfectionClass.Tag = "";
            this.cmbInfectionClass.Text = "";
            this.rdbInfectionOtherNo.Checked = true;
            this.dtDiaDate.Value = DateTime.Now;
            this.dtInfectionDate.Value = DateTime.Now;
            this.dtDeadDate.Checked = false;

            isShow = false;
            base.Clear();
        }

        /// <summary>
        /// MessageBox
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <param name="type">err错误 其它作标题</param>
        private void MyMessageBox(string message, string type)
        {
            switch (type)
            {
                case "err":
                    MessageBox.Show(message, "提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    break;
                default:
                    MessageBox.Show(message, type, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    break;
            }
        }

        /// <summary>
        /// 根据疾病代码检查是否需要添加附卡
        /// </summary>
        /// <param name="diseaseCode"></param>
        public void IsNeedAddition(string infectCode)
        {
            string msg = "";
            ArrayList al = new ArrayList();
            //需附卡
            if (hshNeedAdd.Contains(infectCode))
            {
                if (otherDisease == null)
                {
                    otherDisease = new ucOtherDisease();
                }
                otherDisease.Clear();
                al.Add(otherDisease);
            }

            //需性病报卡
            if (hshNeedSexReport.Contains(infectCode))
            {
                if (venerealDisease == null)
                {
                    venerealDisease = new ucVenerealDisease();
                }
                venerealDisease.Clear();
                al.Add(venerealDisease);
            }
            //需乙肝附卡
            if (hsNeedHepatitisBReport.Contains(infectCode))
            {
                if (ucHepatitisB == null)
                {
                    ucHepatitisB = new ucHepatitisB();
                }
                ucHepatitisB.Clear();
                al.Add(ucHepatitisB);
            }

            if (this.AdditionEvent != null)
            {
                this.AdditionEvent(true, al);
            }
        }

        public override void PrePrint()
        {
            this.gbDiseaseInfo.BackColor = Color.White;
            this.BackColor = Color.White;
            if (!this.dtDeadDate.Checked)
            {
                this.dtDeadDate.Visible = false;
            }
            //this.cl1.Visible = false;
            //this.cl2.Visible = false;
            //this.cl3.Visible = false;
            //this.cl4.Visible = false;
            //this.cl5.Visible = false;
            //this.cl6.Visible = false;
            //this.cl7.Visible = false;
            base.PrePrint();
        }

        public override void Printed()
        {
            this.gbDiseaseInfo.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.BackColor = System.Drawing.Color.FromArgb(158, 177, 201);
            if (!this.dtDeadDate.Checked)
            {
                this.dtDeadDate.Visible = true;
            }
            //this.cl1.Visible = true;
            //this.cl2.Visible = true;
            //this.cl3.Visible = true;
            //this.cl4.Visible = true;
            //this.cl5.Visible = true;
            //this.cl6.Visible = true;
            //this.cl7.Visible = true;
            base.Printed();
        }

        private void ShowMessageAfterSelect(string diseaseId)
        {
            string msg = "";
            if (diseaseId == "3001")
            {
                msg = @"需符合以下四条，并经本院专家组会诊不能诊断为其它疾病的的肺炎病例方可上报为“不明原因肺炎”！
不明原因肺炎定义：

①发热（腋下体温≥38℃）
②具有肺炎的影像学特征

③发病早期白细胞总数降低或正常，或淋巴细胞分类计数减少

④经规范抗菌药物治疗3~5天（参照中华医学会呼吸病学分会颁布的2006版“社区获得性肺炎诊断和治疗指南”，详见附件2），病情无明显改善或呈进行性加重

";

            }

            else if (diseaseId == "3003")
            {
                msg = @"符合以下急性弛缓性麻痹（AFP）病例定义，需进行报告。

AFP定义：所有15岁以下出现急性弛缓性麻痹症状的病例，和任何年龄临床诊断为脊灰的病例均作为急性弛缓性麻痹（AFP）病例。

AFP病例的诊断要点：急性起病、肌张力减弱、肌力下降、腱反射减弱或消失。

常见的AFP病例包括以下疾病：

（1）脊髓灰质炎；

（2）格林巴利综合征（感染性多发性神经根神经炎，GBS）；
（3）横贯性脊髓炎、脊髓炎、脑脊髓炎、急性神经根脊髓炎；
（4）多神经病（药物性多神经病，有毒物质引起的多神经病、原因不明性多神经病）；

（5）神经根炎；
（6）外伤性神经炎（包括臀肌药物注射后引发的神经炎）；
（7）单神经炎；
（8）神经丛炎；
（9）周期性麻痹（包括低钾性麻痹、高钾性麻痹、正常钾性麻痹）；

（10）肌病（包括全身型重症肌无力、中毒性、原因不明性肌病）；

（11）急性多发性肌炎；
（12）肉毒中毒；
（13）四肢瘫、截瘫和单瘫（原因不明）；

（14）短暂性肢体麻痹。

";
            }

            if (msg != "")
            {
                this.MyMessageBox(msg, "提示");
            }
        }

        #endregion

        #region 事件

        protected void cmbInfectionClass_SelectedValueChanged(object sender, EventArgs e)
        {
            //xiwx
            if (this.cmbInfectionClass.Tag == null || this.cmbInfectionClass.Tag.ToString() == "####")
            {
                return;
            }

            string strtempid = this.cmbInfectionClass.Tag.ToString();

            if (this.AdditionEvent != null)
            {
                this.AdditionEvent(false, null);
            }

            if (this.hshNeedMemo.Contains(strtempid))
            {
                this.MyMessageBox("请在备注中填写疾病名称", "提示>>");
            }

            if (this.hsInfomationQuery.Contains(strtempid))
            {
                this.MyMessageBox("请填写"+this.cmbInfectionClass.Text+"个案调查表", "提示>>");
            }

            this.IsNeedAddition(strtempid);

            this.ShowMessageAfterSelect(strtempid);

            //二级分类
            this.cmbCaseClaseTwo.Enabled = this.hshNeedCaseTwo.Contains(strtempid);
            this.cmbCaseClaseTwo.TabStop = this.cmbCaseClaseTwo.Enabled;
        }

        /// <summary>
        /// 选择疾病
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbInfectionClass_Enter(object sender, EventArgs e)
        {
            if (this.infectCode == null || this.infectCode == "")
            {
                this.cmbInfectionClass.Enabled = true;
                return;
            }
            string[] infectCodes = this.infectCode.Split(',');

            if (infectCodes!=null&&infectCodes.Length > 1)
            {
                ArrayList alTmp = new ArrayList();
                this.cmbInfectionClass.Enabled = true;
                foreach (string code in infectCodes)
                {
                    foreach (FS.HISFC.Models.Base.Const disease in this.alInfectItem)
                    {
                        if (code == disease.ID)
                        {
                            alTmp.Add(disease);
                            break;
                        }
                    }
                }
                FS.FrameWork.Models.NeuObject ob = new FS.HISFC.Models.Base.Const();
                FS.FrameWork.WinForms.Classes.Function.ChooseItem(alTmp, ref ob);
                this.cmbInfectionClass.Tag = ob.ID;

                //二级分类
                this.cmbCaseClaseTwo.Enabled = this.hshNeedCaseTwo.Contains(ob.ID);
                this.cmbInfectionClass.Enabled = true;
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                SendKeys.Send("{Tab}");

                return true;
            }
            
            return base.ProcessDialogKey(keyData);
        }

        #endregion
    }
}
