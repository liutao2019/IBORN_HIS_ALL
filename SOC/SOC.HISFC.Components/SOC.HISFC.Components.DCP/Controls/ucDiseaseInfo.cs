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
    /// [��������: ������Ϣuc]<br></br>
    /// [�� �� ��: zj]<br></br>
    /// [����ʱ��: 2008-09-17]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
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

        #region �����

        /// <summary>
        /// ѡ��ڵ�ί��
        /// </summary>
        /// <param name="patient"></param>
        public delegate void AddAddtion(bool isNeed,ArrayList al);

        /// <summary>
        /// ��ʾ�¼�
        /// </summary>
        public event AddAddtion AdditionEvent;

        /// <summary>
        /// �������� ����
        /// </summary>
        private Dictionary<FS.SOC.HISFC.Components.DCP.Classes.EnumAdditionReportMsg, Hashtable> diseaseDictionary = new Dictionary<Classes.EnumAdditionReportMsg, Hashtable>();

        private Hashtable diseaseHt = new Hashtable();

        /// <summary>
        /// ���д�Ⱦ�������ڵ�������
        /// </summary>
        private ArrayList alInfectItem = new ArrayList();

        /// <summary>
        /// ���д�Ⱦ������������
        /// </summary>
        private ArrayList alinfection = new ArrayList();

        /// <summary>
        /// ��Ҫ�����ļ���
        /// </summary>
        private System.Collections.Hashtable hshNeedAdd;

        /// <summary>
        /// ��Ⱦ��������[���ұ���]�����ѡ��Ⱦ��ʱ�Ƿ�ѡ��������
        /// </summary>
        private System.Collections.Hashtable hshInfectClass;

        /// <summary>
        /// ��Ҫ�����Բ����ļ���
        /// </summary>
        private System.Collections.Hashtable hshNeedSexReport;

        /// <summary>
        /// ��Ҫ��Ѫ�ͼ�
        /// </summary>
        private System.Collections.Hashtable hshNeedCheckedBlood;

        /// <summary>
        /// ��Ҫ������������
        /// </summary>
        private System.Collections.Hashtable hshNeedCaseTwo;

        /// <summary>
        /// ��Ҫ�绰����ļ���
        /// </summary>
        private System.Collections.Hashtable hshNeedTelInfect;

        /// <summary>
        /// ��Ҫ��˲�ת�ﵥ�ļ���
        /// </summary>
        private System.Collections.Hashtable hshNeedBill;

        /// <summary>
        /// ��Ҫ��ע�ļ���
        /// </summary>
        private System.Collections.Hashtable hshNeedMemo;

        /// <summary>
        /// ���������˷�
        /// </summary>
        private System.Collections.Hashtable hshLitteChild;

        /// <summary>
        /// ����ְҵΪѧ��[Ӧ��ʾ��дѧУ����֮��]
        /// </summary>
        private System.Collections.Hashtable hshStudent;

        /// <summary>
        /// ��Ҫ�������Ƶ��Բ�
        /// </summary>
        private System.Collections.Hashtable hshSexNeedGradeTwo;

        /// <summary>
        /// ��Ҫ��������Ⱥ����
        /// </summary>
        private System.Collections.Hashtable hshPatientTyepNeedDesc;

        /// <summary>
        /// ���������
        /// </summary>
        private System.Collections.Hashtable hsInfomationQuery;

        /// <summary>
        /// �����͸��׸����ļ�������
        /// </summary>
        private System.Collections.Hashtable hsNeedHepatitisBReport;


        /// <summary>
        /// ����������
        /// </summary>
        private FS.SOC.HISFC.BizProcess.DCP.Common commonProcess = new FS.SOC.HISFC.BizProcess.DCP.Common();

        private ucOtherDisease otherDisease = null;
        private ucVenerealDisease venerealDisease = null;
        private ucHepatitisB ucHepatitisB = null;

        private bool isShow = false;
        #endregion

        #region ����

        public bool InfectionClassEnable
        {
            get 
            {
                return this.cmbInfectionClass.Enabled;
            }
        }

        private string infectCode = "";
        /// <summary>
        /// ָ����������
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

                    //��������
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

        #region ����

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns>-1 ʧ�� 1 �ɹ�</returns>
        public override int Init(DateTime sysdate)
        {
            base.Init(sysdate);//�ȳ�ʼ������ķ�����

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
        /// ��ʼ����Ⱦ����������
        /// </summary>
        /// <returns>-1 ʧ�� 1�ɹ�</returns>
        private int InitInfect()
        {
            //List<FS.HISFC.Models.Base.Const> listInfectClass = Classes.Function<FS.HISFC.Models.Base.Const>.ConvertToList(this.commonProcess.QueryConstantList("INFECTCLASS"));
            //if (listInfectClass == null)
            //{
            //    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ�����������"));
            //    return -1;
            //}

            //ArrayList al=new ArrayList();
            ////���ؼ������ೣ��
            //foreach (FS.HISFC.Models.Base.Const con in listInfectClass)
            //{
            //    List<FS.HISFC.Models.Base.Const> listInfect = Classes.Function<FS.HISFC.Models.Base.Const>.ConvertToList(this.commonProcess.QueryConstantList(con.ID));
            //    if (listInfect == null)
            //    {
            //        MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ"+con.Name+"����"));
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

            //    #region ���ظ�����ʾ����

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
        /// ��ʼ����������1
        /// </summary>
        /// <returns>-1 ʧ�� 1�ɹ�</returns>
        private int InitInfectOne()
        {
            ArrayList alInfectOne = this.commonProcess.QueryConstantList("CASECLASS");
            if (alInfectOne == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ��������1����"));
                return -1;
            }
            this.cmbCaseClassOne.AddItems(alInfectOne);
            return 1;
        }

        /// <summary>
        /// ��ʼ����������2
        /// </summary>
        /// <returns>-1 ʧ�� 1�ɹ�</returns>
        private int InitInfectTwo()
        {
            ArrayList altwo = new ArrayList();
            FS.HISFC.Models.Base.Const obone = new FS.HISFC.Models.Base.Const();

            //altwo.Add(obj);
            FS.HISFC.Models.Base.Const obthree = new FS.HISFC.Models.Base.Const();
            obthree.ID = "2";
            obthree.Name = "δ����";
            altwo.Add(obthree);

            obone.ID = "0";
            obone.Name = "����";
            altwo.Add(obone);

            FS.HISFC.Models.Base.Const obtwo = new FS.HISFC.Models.Base.Const();
            obtwo.ID = "1";
            obtwo.Name = "����";
            altwo.Add(obtwo);

            this.cmbCaseClaseTwo.AddItems(altwo);
            return 1;
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        private int InitInfections()
        {

            //��Ⱦ��������
            ArrayList alInfectClass = new ArrayList();

            alInfectClass.AddRange(commonProcess.QueryConstantList("INFECTCLASS"));
            if (alInfectClass==null)
            {
                return -1;
            }

            //��Ҫ�����Ĵ�Ⱦ��
            this.hshNeedAdd = new Hashtable();
            //����
            this.hshInfectClass = new Hashtable();
            //��Ҫ���Բ����ļ���
            this.hshNeedSexReport = new Hashtable();
            //��Ҫ��Ѫ�ͼ�ļ���

            this.hshNeedCheckedBlood = new Hashtable();
            //��Ҫ���������ļ���
            this.hshNeedCaseTwo = new Hashtable();
            //��Ҫ�绰����ļ���
            this.hshNeedTelInfect = new Hashtable();
            //��Ҫ��д��˲�ת�ﵥ�ļ���
            this.hshNeedBill = new Hashtable();
            //���������˷�
            this.hshLitteChild = new Hashtable();
            //��Ҫ�������Ƶ��Բ�
            this.hshSexNeedGradeTwo = new Hashtable();
            //��Ҫ��Ⱥ��������
            this.hshPatientTyepNeedDesc = new Hashtable();
            //��Ҫ��ע
            this.hshNeedMemo = new Hashtable();
            //���������
            this.hsInfomationQuery = new Hashtable();
            //��Ҫ�Ҹθ����ļ���
            this.hsNeedHepatitisBReport = new Hashtable();

            //�������ͻ�ȡ��Ⱦ��

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
                    o.Name = "--��ѡ��--";
                    al.Insert(0, o);
                    index++;
                }
                alItem = commonProcess.QueryConstantList(infectclass.ID);

                al.AddRange(alItem);
                alInfectItem.AddRange(alItem);

                hshInfectClass.Add(infectclass.ID, null);
                foreach (FS.HISFC.Models.Base.Const infect in al)
                {
                    //���ƹ�����ά���ڱ�ע��ڴ˽���
                    if (infect.Name.IndexOf("��ע", 0) != -1)
                    {
                        infect.Name = infect.Memo;
                        infect.Memo = "";
                    }
                    if (infect.Memo.IndexOf("�踽��", 0) != -1)
                    {
                        hshNeedAdd.Add(infect.ID, null);
                    }
                    if (infect.Memo.IndexOf("���Բ�����", 0) != -1)
                    {
                        hshNeedSexReport.Add(infect.ID, null);
                    }
                    if (infect.Memo.IndexOf("�豸ע") != -1)
                    {
                        hshNeedMemo.Add(infect.ID, null);
                    }
                    //�Բ���������
                    if (infect.Memo.IndexOf("��������", 0) != -1)
                    {
                        hshSexNeedGradeTwo.Add(infect.ID, null);
                    }
                    if (infect.Memo.IndexOf("���Ѫ�ͼ�", 0) != -1)
                    {
                        hshNeedCheckedBlood.Add(infect.ID, null);
                    }
                    //������������
                    if (infect.Memo.IndexOf("��������", 0) != -1)
                    {
                        hshNeedCaseTwo.Add(infect.ID, null);
                    }
                    //�绰֪ͨ
                    if (infect.Memo.IndexOf("��绰֪ͨ", 0) != -1)
                    {
                        hshNeedTelInfect.Add(infect.ID, null);
                    }
                    //��˲�ת�ﵥ
                    if (infect.Memo.IndexOf("��ת�ﵥ", 0) != -1)
                    {
                        hshNeedBill.Add(infect.ID, null);
                    }
                    if (infect.Memo.IndexOf("���������˷�", 0) != -1 || infect.Name.IndexOf("���������˷�", 0) != -1)
                    {
                        hshLitteChild.Add(infect.ID, null);
                    }

                    if (infect.Memo.IndexOf("���������", 0) != -1 )
                    {
                        hsInfomationQuery.Add(infect.ID, null);
                    }
                    if (infect.Memo.IndexOf("���Ҹθ���", 0) != -1)
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
        /// ��Ӹ�����ʾ����
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
        /// ��������Ϣ
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
        /// ȡ������Ϣ
        /// </summary>
        /// <returns></returns>
        public override int GetValue(ref FS.HISFC.DCP.Object.CommonReport report)
        {
            try
            {
                if (this.cmbInfectionClass.Tag == null || this.cmbInfectionClass.Tag.ToString()=="")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ѡ�񡶼������ơ�"));
                    this.cmbInfectionClass.Select();
                    this.cmbInfectionClass.Focus();
                    return -1;
                }
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                if (this.GetDisease(ref obj) == -1)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ѡ�񡶼������ơ�"));
                    this.cmbInfectionClass.Select();
                    this.cmbInfectionClass.Focus();
                    return -1;
                }
                report.Disease = obj;


                if (this.cmbCaseClassOne.Tag == null||this.cmbCaseClassOne.Tag.ToString()=="")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ѡ�񡶲������ࡷ"));
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
                //    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ѡ�񡶼������ơ�"));
                //    this.cmbInfectionClass.Select();
                //    this.cmbInfectionClass.Focus();
                //    return -1;
                //}
                //report.Disease = this.cmbInfectionClass.SelectedItem;

                //�����߼�����
                if (this.dtInfectionDate.Value.Date>this.dtDiaDate.Value.Date)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ѡ�񷢲�����\nע�⣺�������ڲ������������"));
                    this.dtInfectionDate.Select();
                    this.dtInfectionDate.Focus();
                    return -1;
                }
                if (this.dtDiaDate.Value.Date > this.sysdate.Date)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("������ڳ����˽���"));
                    this.dtInfectionDate.Select();
                    this.dtInfectionDate.Focus();
                    return -1;
                }
                if (this.dtDiaDate.Value.Date<this.dtInfectionDate.Value.Date)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�������Ӧ���ڷ�������"));
                    this.dtInfectionDate.Select();
                    this.dtInfectionDate.Focus();
                    return -1;
                }
                //��������
                if (this.dtDeadDate.Checked)
                {
                    if (this.dtDiaDate.Value.Date< this.dtDeadDate.Value.Date)
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("�������Ӧ������������"));
                        this.dtDeadDate.Select();
                        this.dtDeadDate.Focus();
                        return -1;
                    }
                    if (this.dtDeadDate.Value.Date<this.dtInfectionDate.Value.Date)
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("��������Ӧ���ڷ�������"));
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
                        this.MyMessageBox("����д����˲�ת�ﵥ��\n��˲�����ת����˲�������������", "��ʾ>>");
                        isShow = true;
                    }
                    if (report.Memo != string.Empty)
                    {
                        if (report.Memo.IndexOf("��ת��") == -1)
                        {
                            report.Memo = "��ת��\\\\" + report.Memo;
                        }
                    }
                    else
                    {
                        report.Memo = "��ת��\\\\";
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
        /// ��ȡ������Ϣ
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
        /// <param name="message">��ʾ��Ϣ</param>
        /// <param name="type">err���� ����������</param>
        private void MyMessageBox(string message, string type)
        {
            switch (type)
            {
                case "err":
                    MessageBox.Show(message, "��ʾ", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    break;
                default:
                    MessageBox.Show(message, type, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    break;
            }
        }

        /// <summary>
        /// ���ݼ����������Ƿ���Ҫ��Ӹ���
        /// </summary>
        /// <param name="diseaseCode"></param>
        public void IsNeedAddition(string infectCode)
        {
            string msg = "";
            ArrayList al = new ArrayList();
            //�踽��
            if (hshNeedAdd.Contains(infectCode))
            {
                if (otherDisease == null)
                {
                    otherDisease = new ucOtherDisease();
                }
                otherDisease.Clear();
                al.Add(otherDisease);
            }

            //���Բ�����
            if (hshNeedSexReport.Contains(infectCode))
            {
                if (venerealDisease == null)
                {
                    venerealDisease = new ucVenerealDisease();
                }
                venerealDisease.Clear();
                al.Add(venerealDisease);
            }
            //���Ҹθ���
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
                msg = @"���������������������Ժר������ﲻ�����Ϊ���������ĵķ��ײ��������ϱ�Ϊ������ԭ����ס���
����ԭ����׶��壺

�ٷ��ȣ�Ҹ�����¡�38�棩
�ھ��з��׵�Ӱ��ѧ����

�۷������ڰ�ϸ���������ͻ����������ܰ�ϸ�������������

�ܾ��淶����ҩ������3~5�죨�����л�ҽѧ�������ѧ�ֻ�䲼��2006�桰��������Է�����Ϻ�����ָ�ϡ����������2�������������Ը��ƻ�ʽ����Լ���

";

            }

            else if (diseaseId == "3003")
            {
                msg = @"�������¼��Գڻ�����ԣ�AFP���������壬����б��档

AFP���壺����15�����³��ּ��Գڻ������֢״�Ĳ��������κ������ٴ����Ϊ���ҵĲ�������Ϊ���Գڻ�����ԣ�AFP��������

AFP���������Ҫ�㣺�����𲡡������������������½����췴���������ʧ��

������AFP�����������¼�����

��1����������ף�

��2�����ְ����ۺ�������Ⱦ�Զ෢���񾭸����ף�GBS����
��3������Լ����ס������ס��Լ����ס������񾭸������ף�
��4�����񾭲���ҩ���Զ��񾭲����ж���������Ķ��񾭲���ԭ�����Զ��񾭲�����

��5���񾭸��ף�
��6�����������ף������μ�ҩ��ע������������ף���
��7�������ף�
��8���񾭴��ף�
��9����������ԣ������ͼ�����ԡ��߼�����ԡ�����������ԣ���

��10������������ȫ������֢���������ж��ԡ�ԭ�����Լ�������

��11�����Զ෢�Լ��ף�
��12���ⶾ�ж���
��13����̱֫����̱�͵�̱��ԭ��������

��14��������֫����ԡ�

";
            }

            if (msg != "")
            {
                this.MyMessageBox(msg, "��ʾ");
            }
        }

        #endregion

        #region �¼�

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
                this.MyMessageBox("���ڱ�ע����д��������", "��ʾ>>");
            }

            if (this.hsInfomationQuery.Contains(strtempid))
            {
                this.MyMessageBox("����д"+this.cmbInfectionClass.Text+"���������", "��ʾ>>");
            }

            this.IsNeedAddition(strtempid);

            this.ShowMessageAfterSelect(strtempid);

            //��������
            this.cmbCaseClaseTwo.Enabled = this.hshNeedCaseTwo.Contains(strtempid);
            this.cmbCaseClaseTwo.TabStop = this.cmbCaseClaseTwo.Enabled;
        }

        /// <summary>
        /// ѡ�񼲲�
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

                //��������
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
