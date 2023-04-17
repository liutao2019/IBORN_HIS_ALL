using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.WinForms.Report.HealthRecord
{
    /// <summary>
    /// DL.HealthRecord.ucDLCaseBackPrint
    /// [��������: ������ҳ�����ӡ�ؼ� ]<br></br>
    /// [�� �� ��: ��־��]<br></br>
    /// [����ʱ��: 2008-09-03]<br></br>
    /// <�޸ļ�¼
    ///		�޸���='ţ��Ԫ'
    ///		�޸�ʱ��='2009-11-19'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucZhongRiCaseBackPrint : UserControl, FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack
    {
        public ucZhongRiCaseBackPrint()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ��ӡҵ���
        /// </summary>
        FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();

        //����ҵ���
        FS.HISFC.BizLogic.HealthRecord.Fee feeManager = new FS.HISFC.BizLogic.HealthRecord.Fee();

        //����ҵ���
        FS.HISFC.BizLogic.HealthRecord.Operation operationManager = new FS.HISFC.BizLogic.HealthRecord.Operation();

        //Ӥ��ҵ���
        FS.HISFC.BizLogic.HealthRecord.Baby ba = new FS.HISFC.BizLogic.HealthRecord.Baby();                   

        /// <summary>
        /// ��������
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// ���ת����
        /// </summary>
        FS.HISFC.BizProcess.Integrate.RADT radtIntergate = new FS.HISFC.BizProcess.Integrate.RADT();

        FS.HISFC.BizProcess.Integrate.Manager managerIntergate = new FS.HISFC.BizProcess.Integrate.Manager();

        //private FS.HISFC.Models.RADT.Location thirDept = null;
        /// <summary>
        /// ת�ƹ���
        /// </summary>
        FS.HISFC.BizLogic.HealthRecord.DeptShift deptChange = new FS.HISFC.BizLogic.HealthRecord.DeptShift();
           
        /// <summary>
        /// ����
        /// </summary>
        Hashtable hashICUdept = new Hashtable();

        Hashtable hashFeeControl = new Hashtable();
        Hashtable hashLableControl = new Hashtable();
        #endregion

        #region ����

        

        public int Print()
        {
            //this.SetVisible(false);
            p.PrintPage(0, 0, this.neuPanel1);
            return 1;
        }

        public int PrintPreview()
        {
            //this.SetVisible(true);
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            return p.PrintPreview(20, 10, this);
        }

        #endregion

        #region ˽�з���

        /// <summary>
        /// ���ô�ӡʱ�ؼ��Ŀɼ���
        /// </summary>
        /// <param name="isSee"></param>
        private void SetVisible(bool isSee) 
        {
            foreach (Control c in this.neuPanel1.Controls)
            {
                if (c is FS.FrameWork.WinForms.Controls.NeuLabel && !c.Name.StartsWith("lblPri"))
                {
                    c.Visible = isSee;
                }
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public void Reset()
        {
            foreach (Control c in this.neuPanel1.Controls)
            {
                if (c is FS.FrameWork.WinForms.Controls.NeuLabel && c.Name.StartsWith("lblPri"))
                {
                    FS.FrameWork.WinForms.Controls.NeuLabel lbl = c as FS.FrameWork.WinForms.Controls.NeuLabel;
                    lbl.Text = " ";
                }
            }

        }

        /// <summary>
        /// �����ÿؼ���ӵĹ�ϣ��
        /// </summary>
        private void SetHashControl()
        {
            this.hashFeeControl.Clear();
            foreach (Control var in this.neuPanel1.Controls)
            {
                if (var.Name.Contains("fee") || var.Name.Contains("lbl"))
                {
                    this.hashFeeControl.Add(var.Name,var);
                }
                
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controlName"></param>
        /// <returns></returns>
        private Control GetControlByControlName(string controlName)
        {
            if (this.hashFeeControl.Contains(controlName))
            {
                return this.hashFeeControl[controlName] as Control;
            }
            else
            {
                return null;
            }
        }

        #endregion

        private void ucDLCaseBackPrint_Load(object sender, EventArgs e)
        {
            this.Reset();
            this.SetHashControl();
        }

        private void neuLabel10_Click(object sender, EventArgs e)
        {

        }

        private void neuLabel11_Click(object sender, EventArgs e)
        {

        }

        private void neuLabel32_Click(object sender, EventArgs e)
        {

        }

        #region HealthRecordInterface ��Ա

        //public void ControlValue(FS.HISFC.Models.HealthRecord.Base obj)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}
        /// <summary>
        /// ���ò�������ֵ
        /// </summary>
        void FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack.ControlValue(FS.HISFC.Models.HealthRecord.Base obj)
        {
            FS.HISFC.Models.HealthRecord.Base healthReord = obj as FS.HISFC.Models.HealthRecord.Base;

            this.SetHashControl();

            // add by lk 2008-09-12 Ӥ����Ϣ��ֵ
            //#region Ӥ����Ϣ
            ////��ѯ��������������  �����һ��̥����Ӥ����Ϣ�Ͳ�Ҫ��ʾ
            //ArrayList list = ba.QueryBabyByInpatientNo(healthReord.PatientInfo.ID);
            //FS.HISFC.Models.HealthRecord.Baby babyinfo = null;
            //if (list.Count > 0)
            //{
            //    for (int j = 0; j < list.Count; j++)
            //    {
            //        babyinfo = list[j] as FS.HISFC.Models.HealthRecord.Baby;
            //        if (j == 0)
            //        {
            //            this.age.Text = babyinfo.Age.ToString(); //���� ��
            //            this.outBodyWeight.Text = babyinfo.Weight.ToString();//����ʱ����
            //            this.inBodyWeight.Text = babyinfo.WeightInHospital.ToString();//תԺʱ����

            //        }
            //        else if (j == 1)
            //        {
            //            this.age1.Text = babyinfo.Age.ToString(); //���� ��
            //            this.outBodyWeight1.Text = babyinfo.Weight.ToString();//����ʱ����
            //            this.inBodyWeight1.Text = babyinfo.WeightInHospital.ToString();//תԺʱ����

            //            this.txtage1.Visible = true;
            //            this.txtageunit1.Visible = true;
            //            this.txtweightunit1.Visible = true;
            //            this.txtweightunit2.Visible = true;
            //            this.age1.Visible = true;
            //            this.outBodyWeight1.Visible = true;
            //            this.inBodyWeight1.Visible = true;
            //            this.txtoutbaby1.Visible = true;
            //            this.txtinbaby1.Visible = true;
            //            this.neuLabel140.Visible = true;
            //            this.neuLabel47.Visible = true;
            //            this.neuLabel37.Visible = true;

            //        }
            //        else if (j == 2)
            //        {
            //            this.age2.Text = babyinfo.Age.ToString(); //���� ��
            //            this.outBodyWeigh2.Text = babyinfo.Weight.ToString();//����ʱ����

            //            this.inBodyWeight2.Text = babyinfo.WeightInHospital.ToString();//תԺʱ����

            //            this.txtageunit2.Visible = true;
            //            this.txtweightunit3.Visible = true;
            //            this.txtweightunit4.Visible = true;
            //            this.age2.Visible = true;
            //            this.outBodyWeigh2.Visible = true;
            //            this.inBodyWeight2.Visible = true;
            //            this.txtoutbaby2.Visible = true;
            //            this.txtinbaby2.Visible = true;
            //            this.neuLabel148.Visible = true;
            //            this.neuLabel166.Visible = true;
            //            this.neuLabel156.Visible = true;


            //        }
            //        else
            //        {
            //            continue;
            //        }
            //    }
            //}
            //#endregion

            #region ������Ϣ

            ArrayList alOpr = operationManager.QueryOperationByInpatientNo(healthReord.PatientInfo.ID);

            int i = 1;

            foreach (object opr in alOpr)
            {
                FS.HISFC.Models.HealthRecord.OperationDetail opration = opr as FS.HISFC.Models.HealthRecord.OperationDetail;
                switch (i)
                {
                    case 1:
                        //����
                        this.lblPriShoushuChaozuoBianma1.Text = opration.OperationInfo.ID;
                        //����
                        this.lblPriChaozuoRiqi1.Text = opration.OperationDate.ToShortDateString();
                        //����
                        this.lblPriChaozuoMingchen1.Text = opration.OperationInfo.Name;
                        //����
                        this.lblPriChaozuoSuzhe1.Text = opration.FirDoctInfo.Name;
                        //һ��
                        this.lblPriChaozuoYizu1.Text = opration.SecDoctInfo.Name;
                        //����
                        this.lblPriChaozuoErzu1.Text = opration.ThrDoctInfo.Name;
                        //����ʽ
                        this.lblPriChaozuoMazui1.Text = con.GetConstant(FS.HISFC.Models.Base.EnumConstant.ANESTYPE, opration.MarcKind).Name;
                        //����ҽʦ
                        this.lblPriChaozuoMazuiYishi1.Text = opration.NarcDoctInfo.Name;
                        //�п����ϵȼ�
                        this.lblPriQiekouYuheDengji1.Text = con.GetConstant(FS.HISFC.Models.Base.EnumConstant.INCITYPE, opration.NickKind).Name + "/" + con.GetConstant("CICATYPE", opration.CicaKind);
                        i++;
                        break;
                    case 4:
                        //����
                        this.lblPriShoushuChaozuoBianma4.Text = opration.OperationInfo.ID;
                        //����
                        this.lblPriChaozuoRiqi4.Text = opration.OperationDate.ToShortDateString();
                        //����
                        this.lblPriChaozuoMingchen4.Text = opration.OperationInfo.Name;
                        //����
                        this.lblPriChaozuoSuzhe4.Text = opration.FirDoctInfo.Name;
                        //һ��
                        this.lblPriChaozuoYizu4.Text = opration.SecDoctInfo.Name;
                        //����
                        this.lblPriChaozuoErzu4.Text = opration.ThrDoctInfo.Name;
                        //����ʽ
                        this.lblPriChaozuoMazui4.Text = con.GetConstant(FS.HISFC.Models.Base.EnumConstant.ANESTYPE, opration.MarcKind).Name;
                        //����ҽʦ
                        this.lblPriChaozuoMazuiYishi4.Text = opration.NarcDoctInfo.Name;
                        //�п����ϵȼ�
                        this.lblPriQiekouYuheDengji4.Text = con.GetConstant(FS.HISFC.Models.Base.EnumConstant.INCITYPE, opration.NickKind).Name + "/" + con.GetConstant("CICATYPE", opration.CicaKind);
                        i++;
                        break;
                    case 2:
                        //����
                        this.lblPriShoushuChaozuoBianma2.Text = opration.OperationInfo.ID;
                        //����
                        this.lblPriChaozuoRiqi2.Text = opration.OperationDate.ToShortDateString();
                        //����
                        this.lblPriChaozuoMingchen2.Text = opration.OperationInfo.Name;
                        //����
                        this.lblPriChaozuoSuzhe2.Text = opration.FirDoctInfo.Name;
                        //һ��
                        this.lblPriChaozuoYizu2.Text = opration.SecDoctInfo.Name;
                        //����
                        this.lblPriChaozuoErzu2.Text = opration.ThrDoctInfo.Name;
                        //����ʽ
                        this.lblPriChaozuoMazui2.Text = con.GetConstant(FS.HISFC.Models.Base.EnumConstant.ANESTYPE, opration.MarcKind).Name;
                        //����ҽʦ
                        this.lblPriChaozuoMazuiYishi2.Text = opration.NarcDoctInfo.Name;
                        //�п����ϵȼ�
                        this.lblPriQiekouYuheDengji2.Text = con.GetConstant(FS.HISFC.Models.Base.EnumConstant.INCITYPE, opration.NickKind).Name + "/" + con.GetConstant("CICATYPE", opration.CicaKind);
                        i++;
                        break;
                    case 3:
                        //����
                        this.lblPriShoushuChaozuoBianma3.Text = opration.OperationInfo.ID;
                        //����
                        this.lblPriChaozuoRiqi3.Text = opration.OperationDate.ToShortDateString();
                        //����
                        this.lblPriChaozuoMingchen3.Text = opration.OperationInfo.Name;
                        //����
                        this.lblPriChaozuoSuzhe3.Text = opration.FirDoctInfo.Name;
                        //һ��
                        this.lblPriChaozuoYizu3.Text = opration.SecDoctInfo.Name;
                        //����
                        this.lblPriChaozuoErzu3.Text = opration.ThrDoctInfo.Name;
                        //����ʽ
                        this.lblPriChaozuoMazui3.Text = con.GetConstant(FS.HISFC.Models.Base.EnumConstant.ANESTYPE, opration.MarcKind).Name;
                        //����ҽʦ
                        this.lblPriChaozuoMazuiYishi3.Text = opration.NarcDoctInfo.Name;
                        //�п����ϵȼ�
                        this.lblPriQiekouYuheDengji3.Text = con.GetConstant(FS.HISFC.Models.Base.EnumConstant.INCITYPE, opration.NickKind).Name + "/" + con.GetConstant("CICATYPE", opration.CicaKind);
                        i++;
                        break;
                    default:
                        break;
                }
                if (i > 4)
                {
                    break;
                }
            }

            #endregion

            #region ������Ϣ

            if (healthReord.DeadDate != DateTime.MinValue)
            {

            }

            #endregion

            #region ������Ϣ

            //Modify by lk 2008-09-12 ����ͳ�ƴ�����룬��ʾ���  ��ʱ��Ҳ���԰�ͳ�ƴ�������Ҳ ����
            ArrayList alFee = feeManager.QueryFeeInfoState(healthReord.PatientInfo.ID);
            decimal totFee = 0.0M;
            foreach (FS.HISFC.Models.RADT.Patient FeeObj in alFee)
            {
                ////FS.HISFC.Models.RADT.Patient info = fee as FS.HISFC.Models.RADT.Patient;
                //switch (patientinfo.ID)
                //{
                //    case "01":
                //        this.fee01.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "02":
                //        this.fee02.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "03":
                //        this.fee03.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "04":
                //        this.fee04.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "05":
                //        this.fee05.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "06":
                //        this.fee06.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "07":
                //        this.fee07.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "08":
                //        this.fee08.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "09":
                //        this.fee09.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "10":
                //        this.fee10.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "11":
                //        this.fee11.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "12":
                //        this.fee12.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "13":
                //        this.fee13.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "14":
                //        this.fee14.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "15":
                //        this.fee15.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "16":
                //        this.fee16.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "17":
                //        this.fee17.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "18":
                //        this.fee18.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "19":
                //        this.fee19.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "20":
                //        this.fee20.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "21":
                //        this.fee21.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "22":
                //        this.fee22.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "23":
                //        this.fee23.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "24":
                //        this.fee24.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "25":
                //        this.fee25.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "26":
                //        this.fee26.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "27":
                //        this.fee27.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "28":
                //        this.fee28.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "29":
                //        this.fee29.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    case "30":
                //        this.fee30.Text = patientinfo.User01;
                //        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.User01);
                //        break;
                //    default:
                //        break;
                //}



                //this.lblPriZhuyuanFeiyongZongji.Text = FS.FrameWork.Public.String.FormatNumberReturnString(totFee, 2);

                Control control = this.GetControlByControlName("fee" + FeeObj.DIST);
                control.Text = FeeObj.User01;
                Control control1 = this.GetControlByControlName("lbl" + FeeObj.DIST);
                if (FeeObj.DIST == "17" || FeeObj.DIST == "18" || FeeObj.DIST == "19")
                {
                }
                else
                {
                //    control.Text = FeeObj.AreaCode;
                //}
                //totFee += FS.FrameWork.Function.NConvert.ToDecimal(FeeObj.User01);
                    control1.Text = FeeObj.AreaCode;
                    control.Text = FeeObj.IDCard;
                }
                totFee += FS.FrameWork.Function.NConvert.ToDecimal(FeeObj.IDCard);
            }
            this.lblPriZhuyuanFeiyongZongji.Text = FS.FrameWork.Public.String.FormatNumberReturnString(totFee, 2);

            #endregion

            #region ʬ�죬���������Ƿ�Ϊ��һ��

            if (healthReord.CadaverCheck == "1")
            {
                this.lblPriShijian.Text = "1";
            }
            else
            {
                this.lblPriShijian.Text = "2";
            }
            if (healthReord.YnFirst == "1")
            {
                this.lblPriDiyili.Text = "1";
            }
            else
            {
                this.lblPriDiyili.Text = "2";
            }
            #endregion

            #region ����,ʾ�̲���

            if (healthReord.VisiStat == "1")
            {
                this.lblPriSuiZhen.Text = "1";
            }
            else
            {
                this.lblPriSuiZhen.Text = "2";
            }

            //����������
            this.lblPriSuizhenQixianNian.Text = healthReord.VisiPeriodYear;
            this.lblPriSuizhenQixianYue.Text = healthReord.VisiPeriodMonth;
            this.lblPriSuizhenQixianZhou.Text = healthReord.VisiPeriodWeek;

            //ʾ�̲���
            if (healthReord.TechSerc == "1")
            {
                this.lblPriShijiaoBingli.Text = "1";
            }
            else
            {
                this.lblPriShijiaoBingli.Text = "2";
            }

            #endregion

            #region Ѫ�͡���ѪƷ��

            //Ѫ�Ͳ��Ǵӳ����л�ȡ
            switch (healthReord.PatientInfo.BloodType.ID.ToString())
            {
                case "A":
                    this.lblPriXuexing.Text = "1";
                    break;
                case "B":
                    this.lblPriXuexing.Text = "2";
                    break;
                case "AB":
                    this.lblPriXuexing.Text = "3";
                    break;
                case "O":
                    this.lblPriXuexing.Text = "4";
                    break;
                case "U":
                    this.lblPriXuexing.Text = "5";
                    break;
                default:
                    this.lblPriXuexing.Text = "5";
                    break;
            }

            this.lblPriXuexing.Text = healthReord.PatientInfo.BloodType.ID.ToString();

            this.lblPriRH.Text = healthReord.RhBlood;

            this.lblPriShuxueFanying.Text = healthReord.ReactionBlood;

            //��ѪƷ��
            this.lblPriShuxuePinzhongHongxibao.Text = healthReord.BloodRed;
            this.lblPriShuxuePinzhongQuanxue.Text = healthReord.BloodWhole;
            this.lblPriShuxuePinzhongXuejiang.Text = healthReord.BloodPlasma;
            this.lblPriShuxuePinzhongXuexiaoban.Text = healthReord.BloodPlatelet;
            this.lblPriShuxuePinzhongQita.Text = healthReord.BloodOther;

            #endregion
            //#region ��֢��Ϣ
            //////FS.HISFC.BizProcess.RADT.InPatient( public ArrayList GetPatientRADTInfo(string patientNo))
            ////FS.HISFC.Models.Invalid.CShiftData myCShiftDate = new FS.HISFC.Models.Invalid.CShiftData();
            //ArrayList alShiftData = new ArrayList();
            //////��ȡ����ת����Ϣ
            //////alShiftData = radtIntergate.GetPatientRADTInfo(healthReord.PatientInfo.ID);
            ////�Ӳ�����ȡת����Ϣ
            //alShiftData = deptChange.QueryChangeDeptFromShiftApply(healthReord.PatientInfo.ID, "2");

            ////ArrayList deptList = managerIntergate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.I);

            //string inDate = "";
            //string outDate = "";
            //FS.HISFC.Models.RADT.Location changeDept = null;
            //FS.HISFC.Models.RADT.Location changeDeptTemp = null;

            //////��ICU������Ϣ����ϣ��
            ////for (int k = 0; k < deptList.Count; i++)
            ////{
            ////    dept = deptList[0] as FS.HISFC.Models.Base.Department;
            ////    if (dept.SpecialFlag != 3 || dept.SpecialFlag != 4)//����ICU CCU  coutinue
            ////    {
            ////        continue;
            ////    }                        
            ////}
            ////hashICUdept
            ////if (alShiftData != null && alShiftData.Count > 0)
            ////{
            ////for (int p = 0; p < alShiftData.Count - 1; p++)
            ////{
            ////    changeDept = alShiftData[p] as FS.HISFC.Models.RADT.Location;
            ////    if (changeDept.Dept.User01 != "3" && changeDept.Dept.User01 != "4" && p > 3)
            ////    {
            ////        continue;
            ////    }
            ////    if (p == 0)
            ////    {
            ////        inDate = healthReord.PatientInfo.PVisit.InTime.ToString();//ת������
            ////        if (alShiftData.Count > 1)
            ////        {
            ////            changeDeptTemp = alShiftData[1] as FS.HISFC.Models.RADT.Location;
            ////            outDate = changeDeptTemp.User01;//ת��ʱ��
            ////        }
            ////        else
            ////        {
            ////            outDate = obj.PatientInfo.PVisit.OutTime.ToString();
            ////        }
            ////    }
            ////    else if (p < alShiftData.Count)
            ////    {
            ////        inDate = changeDept.User01;//ת��ʱ��
            ////        changeDeptTemp = alShiftData[p + 1] as FS.HISFC.Models.RADT.Location;
            ////        outDate = changeDeptTemp.User01;//ת��ʱ��
            ////    }
            ////    else if (p == alShiftData.Count)
            ////    {
            ////        inDate = changeDept.User01;//ת��ʱ��
            ////        outDate = obj.PatientInfo.PVisit.OutTime.ToString();//ת��ʱ��
            ////    }

            //    //inDate = changeDept.User01;//ת��ʱ��
            //    //FS.HISFC.Models.Base.Department dept = null;

            //    //switch (p)
            //    //{
            //    //    case 0:
            //    //        Jianhu1.Text = changeDept.Dept.Name;
            //    //        Jinru1.Text = inDate;
            //    //        tuichu1.Text = outDate.ToString();
            //    //        break;
            //    //    case 1:
            //    //        Jianhu2.Text = changeDept.Dept.Name;
            //    //        Jinru2.Text = inDate;
            //    //        tuichu2.Text = outDate.ToString();
            //    //        break;
            //    //    case 2:
            //    //        Jianhu3.Text = changeDept.Dept.Name;
            //    //        Jinru3.Text = inDate;
            //    //        tuichu3.Text = outDate.ToString();
            //    //        break;
            //    //    case 3:
            //    //        Jianhu4.Text = changeDept.Dept.Name;
            //    //        Jinru4.Text = inDate;
            //    //        tuichu4.Text = outDate;
            //    //        break;
            //    //    default:
            //    //        break;

            //    //}

            ////}

            ////}
            //#endregion

            #region ���� add by lk 2008-09-12
            //this.useHourBox.Text = healthReord.ApneaUseTime.ToString();//������ʹ��ʱ��
            //this.HosHour.Text = healthReord.PreComaHour.ToString();//����ʱ��Сʱ
            //this.HosMinute.Text = healthReord.PreComaMin.ToString();//����ʱ�� ����
            //this.inHosHour.Text = healthReord.SithComaHour.ToString();//��Ժ�����ʱ�� Сʱ
            //this.inHosMinute.Text = healthReord.SithComaMin.ToString();//��Ժ�����ʱ�� ����
            //this.outHosMethod.Text = healthReord.LeaveHospital;//��Ժ��ʽ
            //this.HosName.Text = healthReord.TransferHospital;//ת��ҽԺ����

            //this.SuperNus.Text = healthReord.SuperNus.ToString();//�ؼ�����
            //this.INus.Text = healthReord.IINus.ToString();//һ������
            //this.IINus.Text = healthReord.IINus.ToString();//��������
            //this.IIINus.Text = healthReord.IIINus.ToString();//��������
            //this.ICU.Text = healthReord.StrictNuss.ToString();//��֢�໤
            //this.CCU.Text = healthReord.SpecalNus.ToString();//���⻤��


            #endregion
        }

        #endregion

        #region IReportPrinter ��Ա

        public int Export()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        private void lbl02_Click(object sender, EventArgs e)
        {

        }
    }
}
