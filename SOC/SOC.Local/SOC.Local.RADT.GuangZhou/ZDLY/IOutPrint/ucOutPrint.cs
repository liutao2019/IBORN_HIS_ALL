using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace FS.SOC.Local.RADT.GuangZhou.ZDLY.IOutPrint
{
    /// <summary>
    /// ��Ժ֪ͨ����ӡ
    /// </summary>
    public partial class ucOutPrint : UserControl,FS.HISFC.BizProcess.Interface.IPrintInHosNotice
    {

        //FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

        FS.HISFC.BizProcess.Integrate.Manager managerBizProcess = new FS.HISFC.BizProcess.Integrate.Manager();

        FS.HISFC.BizLogic.RADT.InPatient inpatientRadt = new FS.HISFC.BizLogic.RADT.InPatient();
        FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();

        public ucOutPrint()
        {
            InitializeComponent();
        }



        //Ĭ�ϴ�ӡ������Ժ֪ͨ��
        string nameFlag = "0";

        /// <summary>
        /// ֪ͨ����ӡ����
        /// </summary>
        public string NameFlag
        {
            get
            {
                return nameFlag;
            }
            set 
            {
                nameFlag = value;
                //switch (nameFlag)
                //{
                //    case "0":
                //        this.neuLabel1.Text = "��Ժ";
                //        break;
                //    case "1":
                //        this.neuLabel1.Text = "תԺ";
                //        //this.Paint += new PaintEventHandler(MyDrawRectangle);
                //        //this.BorderStyle = BorderStyle.FixedSingle;
                //        this.AddPanelRect();
                //        break;
                //    case "2":
                //        this.neuLabel1.Text = "ת��";
                //        break;
                //    default:
                //        break;
                //}
                //this.neuLabel1.Text += "����֪ͨ��";

                

            }
        }

        public int SetValue(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            FS.HISFC.Models.RADT.PatientInfo pInfo = inpatientRadt.QueryPatientInfoByInpatientNO(patient.ID);
            if (pInfo != null)
            {
                patient = pInfo;
            }
            
            #region //��Ժ��ҩSQL���
            /// <summary>
            /// ��Ժ��ҩSQL���
            /// </summary>

            //��ȡ��Ժ��ҩ����
            string sql1 = @"select nvl(sum( case when r.class_code='P' then sum(w.tot_cost)else 0 end ),0)sum_p,
                             nvl(sum( case when r.class_code='PCZ' then sum(w.tot_cost)else 0 end ),0)sum_pcz,
                             nvl(sum( case when r.class_code='PCC' then sum(w.tot_cost)else 0 end ),0)sum_pcc      
                            from fin_ipb_medicinelist w , MET_IPM_ORDER r where w.inpatient_no=r.inpatient_no
                            and w.mo_order=r.mo_order and r.type_code='CD'and r.mo_stat in ('1','2') 
                             and w.inpatient_no='{0}'
                            group by r.class_code  ";
            //ȡ��Ժ��Ժ��ҩ����
            string sql2 = @"select r.use_days from MET_IPM_ORDER r 
                        where r.type_code='CD'and r.mo_stat in ('1','2')  and r.class_code='PCC'
                          and r.inpatient_no='{0}'
                          group by r.class_code ,use_days";
            //ȡ��Ժ��ҩ���ķ�
            string sql3 = @"select nvl(sum(t.tot_cost),0)tot_cost from met_ipm_order w,fin_ipb_itemlist t
        where w.subtbl_flag='1' and w.type_code='CD' and w.mo_order = t.mo_order and t.inpatient_no=w.inpatient_no
        and w.inpatient_no='{0}' ";
            #endregion


            try
            {
                System.IO.MemoryStream me = new System.IO.MemoryStream(((FS.HISFC.Models.Base.Hospital)inpatientRadt.Hospital).HosLogoImage);
                this.picbLogo.Image = Image.FromStream(me);
            }
            catch
            {
            }

            //string sysday = System.DateTime.Now.ToString("d");//��ȡϵͳ����
            //string syshm = System.DateTime.Now.ToString("t");//��ȡϵͳʱ��
           // this.lblBlood.Text = patient.BloodType.Name ;//Ѫ�� 
            this.lblOutStat.Text = managerBizProcess.GetConstansObj(FS.HISFC.Models.Base.EnumConstant.ZG.ToString(), patient.PVisit.ZG.ID).Name;//��Ժ���
            this.lblOutDate1.Text = patient.PVisit.OutTime.ToShortDateString();//��Ժ����
            //this.lblSystemDay1.Text = patient.PVisit.OutTime.ToShortDateString();//��Ժ����
            this.lblBedNo1.Text = patient.PVisit.PatientLocation.Bed.ID;//����
            this.lblName1.Text = patient.Name;//���� 
            this.lblSex.Text = patient.Sex.ToString();//�Ա�
            this.lblPatientNo1.Text = patient.PID.PatientNO;//סԺ�� 
            this.lblDept.Text = patient.PVisit.PatientLocation.NurseCell.Name + ":" + FS.FrameWork.Management.Connection.Operator.Name; ;//����:����Ա
            this.lblDial.Text = patient.MainDiagnose;//סԺ��������

            lblSystemDate1.Text = inpatientRadt.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm");

            lblSystemDay.Text = inpatientRadt.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss");

            //this.lblSystemDate1.Text = sysday + " " + syshm;  //ϵͳʱ��
            //this.lblSystemDay.Text = sysday;//ϵͳ����
            //this.lblSystemMin.Text = syshm;//ϵͳʱ��
            this.lblWjMoney.Text = FS.FrameWork.Public.String.ToSimpleString(patient.FT.TotCost);//δ���ܽ��
            this.lblAjMoney.Text = FS.FrameWork.Public.String.ToSimpleString(patient.FT.PrepayCost);//Ԥ�����ܽ��   + patient.FT.BalancedPrepayCost
            //ҽ�λ�������
            this.lblPact.Text = patient.Pact.ToString();  
            //����ҽ������
            string healthCareType = patient.PVisit.HealthCareType;
            if (!string.IsNullOrEmpty(healthCareType))
            {
                this.healthCareTypeLbl.Text = managerBizProcess.GetConstant("HealthCareType", healthCareType).ToString().Trim();
            }
            #region ҽ�����͸�ѡ�� ע��by zhaorong at 2013-7-25 �޸�Ϊֱ����ʾ�����ı�
            ////��ʼҽ������checkedΪfalse
            //this.cbNormal.Checked = false;
            //this.cbCancer.Checked = false;
            //this.cbICU.Checked = false;
            //this.cbEndoscope.Checked = false;
            //this.cb0phthalmology.Checked = false;
            //this.cbOrthopaedics.Checked = false;
            //this.cbBirthInsurance.Checked = false;
            //switch (patient.Pact.ToString())
            //{ 
            //    case "����ҽ��":
            //    case "ҽ����ְ":
            //    case "ҽ����ְ":
            //    case "�������գ�ҽ����":
            //    case "ҽ����������":
            //    case "ҽ������":
            //    case "ҽ��-ѧ��ǰ��ͯ":
            //    case "ҽ��-�������":
            //    case "ҽ��-��Уѧ��":
            //    case "ҽ��-δ������":
            //    case "ҽ��-��ҵ��Ա":
            //    case "ҽ��-����ͳ��":
            //    case "��������":
            //        //update by zhaorong start at 2013-7-24 ֪ͨ�����Զ���ӳҽ��ѡ���ҽ������
            //        //this.lblPactNotice.Text = "����ͨ    ����������    ��ICU    ���ھ�    ���ۿ�    ���ǿ�ָ������   ����������";
            //        /*
            //         * ҽ�����ͣ� 0��ͨ 1���� 2��������  3ICU  4�ھ�  5�ۿ� 6�ǿ�ָ������ 7��������
            //         */
            //        //��ͨ 
            //        this.cbNormal.Visible = true;
            //        if ("0".Equals(healthCareType)) this.cbNormal.Checked = true;
            //        //��������
            //        this.cbCancer.Visible = true;
            //        if ("2".Equals(healthCareType)) this.cbCancer.Checked = true;
            //        //ICU
            //        this.cbICU.Visible = true;
            //        if ("3".Equals(healthCareType)) this.cbICU.Checked = true;
            //        //�ھ�
            //        this.cbEndoscope.Visible = true;
            //        if ("4".Equals(healthCareType)) this.cbEndoscope.Checked = true;
            //        //�ۿ�
            //        this.cb0phthalmology.Visible = true;
            //        if ("5".Equals(healthCareType)) this.cb0phthalmology.Checked = true;
            //        //�ǿ�ָ������
            //        this.cbOrthopaedics.Visible = true;
            //        if ("6".Equals(healthCareType)) this.cbOrthopaedics.Checked = true;
            //        //��������
            //        this.cbBirthInsurance.Visible = true;
            //        if ("7".Equals(healthCareType)) this.cbBirthInsurance.Checked = true;
            //        break;
            //    case "����ҽ��":
            //        //this.lblPactNotice.Text = "����ͨ      ����������      ���ǿ�ָ������";
            //        //��ͨ
            //        this.cbNormal.Visible = true;
            //        if ("0".Equals(healthCareType)) this.cbNormal.Checked = true;
            //        //��������
            //        this.cbCancer.Visible = true;
            //        if ("2".Equals(healthCareType)) this.cbCancer.Checked = true;
            //        //ICUλ����ʾ�ǿ�ָ������
            //        this.cbICU.Visible = true;
            //        this.cbICU.Text = "�ǿ�ָ������";
            //        if ("7".Equals(healthCareType)) this.cbICU.Checked = true;
            //        break;
            //    default:
            //        //this.lblPactNotice.Text = "����ͨ      ������ ";
            //        //��ͨ
            //        this.cbNormal.Visible = true;
            //        if ("0".Equals(healthCareType)) this.cbNormal.Checked = true;
            //        //����
            //        this.cbCancer.Visible = true;
            //        this.cbCancer.Text = "����";
            //        if ("1".Equals(healthCareType)) this.cbCancer.Checked = true;
            //        break;
            //        //update by zhaorong end at 2013-7-24 ֪ͨ�����Զ���ӳҽ��ѡ���ҽ������
            //}
            #endregion
            //this.label8.Text = patient.Pact.ToString();

            //��Ժ���
            if (patient.PVisit.Circs.ID == "1")
            {
                this.lblInStat.Text = "һ��";

            }
            else if (patient.PVisit.Circs.ID == "2")
            {
                this.lblInStat.Text = "��";

            }
            else if (patient.PVisit.Circs.ID == "3")
            {
                this.lblInStat.Text = "Σ";

            }
            else
            {
                lblInStat.Text = "��";
            }
        

            string tempsql = string.Format(sql1, patient.ID);
            string tempsql2 = string.Format(sql2, patient.ID);
            string tempsql3 = string.Format(sql3, patient.ID);
            // ��ȡ��Ժ��ҩ����
            DataSet ds1=new DataSet();
            inpatientRadt.ExecQuery(tempsql,ref ds1);
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {

                    this.lblPMoney.Text = ds1.Tables[0].Rows[i][0].ToString() + "Ԫ";
                    this.lblPCZMoney.Text = ds1.Tables[0].Rows[i][1].ToString() + "Ԫ";
                    this.lblPCCMoney.Text = ds1.Tables[0].Rows[i][2].ToString() + "Ԫ";
                    this.lblTotal.Text = (Convert.ToDouble(ds1.Tables[0].Rows[i][0]) + Convert.ToDouble(ds1.Tables[0].Rows[i][1]) + Convert.ToDouble(ds1.Tables[0].Rows[i][2])).ToString() + "Ԫ";                     
                }
            }

            
            //ȡ��Ժ��Ժ��ҩ����
            DataSet ds2 = new DataSet();
            inpatientRadt.ExecQuery(tempsql2, ref ds2);
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                {

                    this.lblPCCjs.Text = ds2.Tables[0].Rows[i][0].ToString()+"��";

                }
            }
            else
            {
                this.lblPCCjs.Text = "0��";
            }
            
           //ȡ��Ժ��ҩ���ķ�
            DataSet ds3 = new DataSet();
            inpatientRadt.ExecQuery(tempsql3, ref ds3);
            if (ds3 != null && ds3.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
                {

                    this.lblZsf.Text = ds3.Tables[0].Rows[i][0].ToString() + "Ԫ";
                    
                }
            }
           


         
          



            ///ҽ��ҽ���ĳ�Ժ���
            //string strDial = string.Empty;
            //string strMainDial = string.Empty;
            //ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(patient.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            //foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
            //{
            //    if (diag.DiagInfo.DiagType.ID.Equals("14"))//��Ժ���
            //    {
            //        strDial += diag.DiagInfo.ICD10.Name + "��";
            //    }

            //    if (diag.DiagInfo.DiagType.ID.Equals("1"))//��Ժ���
            //    {
            //        strMainDial += diag.DiagInfo.ICD10.Name + "��";
            //    }
            //}

            //if (strDial.Length > 0)
            //{
            //    this.lblDial.Text = strDial.Substring(0, strDial.Length - 1); //��Ժ���
            //}
            //else if (strMainDial.Length > 0)
            //{
            //    this.lblDial.Text = strMainDial.Substring(0, strMainDial.Length - 1); 
            //}
            return 1;
        }

        public int Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            FS.HISFC.Components.Common.Classes.Function.GetPageSize("outcard", ref print);
            print.IsLandScape = true;
            //��ӱ��� zhao.chf
            //print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            print.PrintPage(0, 0, this);
            return 1;
        }
        public int PrintView()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            FS.HISFC.Components.Common.Classes.Function.GetPageSize("outcard", ref print);
            print.IsLandScape = true;
            print.PrintPreview(0, 0, this);
            return 1;
        }

        private void neuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void ucOutPrint_Load(object sender, EventArgs e)
        {


        }

        private void lblMaxDay_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void neuLabel8_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click_2(object sender, EventArgs e)
        {

        }

        private void label5_Click_3(object sender, EventArgs e)
        {

        }

        private void lblDial_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click_4(object sender, EventArgs e)
        {

        }

        private void lblName1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click_1(object sender, EventArgs e)
        {

        }

        private void neuLabel9_Click(object sender, EventArgs e)
        {

        }

        private void lbl_Click(object sender, EventArgs e)
        {

        }

        private void lblOperName_Click(object sender, EventArgs e)
        {

        }

     

    }
}
