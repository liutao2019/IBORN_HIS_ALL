using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace Neusoft.SOC.Local.RADT.GuangZhou.GYZL.IOutPrint
{
    /// <summary>
    /// ��Ժ֪ͨ����ӡ
    /// </summary>
    public partial class ucOutPrint : UserControl,Neusoft.HISFC.BizProcess.Interface.IPrintInHosNotice
    {

        Neusoft.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new Neusoft.HISFC.BizLogic.HealthRecord.Diagnose();
        Neusoft.HISFC.BizLogic.Manager.Department deptMgr = new Neusoft.HISFC.BizLogic.Manager.Department();

        Neusoft.HISFC.BizProcess.Integrate.Manager managerBizProcess = new Neusoft.HISFC.BizProcess.Integrate.Manager();

        //Neusoft.HISFC.BizLogic.RADT.InPatient inpatientRadt = new Neusoft.HISFC.BizLogic.RADT.InPatient();
        Neusoft.HISFC.Models.RADT.PatientInfo patient = new Neusoft.HISFC.Models.RADT.PatientInfo();

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

        public int SetValue(Neusoft.HISFC.Models.RADT.PatientInfo patient)
        {
            
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
                System.IO.MemoryStream me = new System.IO.MemoryStream(((Neusoft.HISFC.Models.Base.Hospital)deptMgr.Hospital).HosLogoImage);
                this.picbLogo.Image = Image.FromStream(me);
            }
            catch
            {
            }

            string sysday = System.DateTime.Now.ToString("d");//��ȡϵͳ����
            string syshm = System.DateTime.Now.ToString("t");//��ȡϵͳʱ��
           // this.lblBlood.Text = patient.BloodType.Name ;//Ѫ�� 
            this.lblOutStat.Text = managerBizProcess.GetConstansObj(Neusoft.HISFC.Models.Base.EnumConstant.ZG.ToString(), patient.PVisit.ZG.ID).Name;//��Ժ���
            this.lblOutDate1.Text = patient.PVisit.OutTime.ToShortDateString();//��Ժ����
            //this.lblSystemDay1.Text = patient.PVisit.OutTime.ToShortDateString();//��Ժ����
            this.lblBedNo1.Text = patient.PVisit.PatientLocation.Bed.ID;//����
            this.lblName1.Text = patient.Name;//���� 
            this.lblSex.Text = patient.Sex.ToString();//�Ա�
            this.lblPatientNo1.Text = patient.PID.PatientNO;//סԺ�� 
            this.lblDept.Text = patient.PVisit.PatientLocation.NurseCell.Name + ":" + Neusoft.FrameWork.Management.Connection.Operator.Name; ;//����:����Ա
            this.lblDial.Text = patient.MainDiagnose;//סԺ��������
            this.lblSystemDate1.Text = sysday + " " + syshm;  //ϵͳʱ��
            this.lblSystemDay.Text = sysday;//ϵͳ����
            this.lblSystemMin.Text = syshm;//ϵͳʱ��
            this.lblWjMoney.Text = Neusoft.FrameWork.Public.String.ToSimpleString(patient.FT.TotCost);//δ���ܽ��
            this.lblAjMoney.Text = Neusoft.FrameWork.Public.String.ToSimpleString(patient.FT.PrepayCost + patient.FT.BalancedPrepayCost);//Ԥ�����ܽ��
            this.lblOperName.Text = Neusoft.FrameWork.Management.Connection.Operator.Name;
            //ҽ�λ�������
            this.lblPact.Text = patient.Pact.ToString();  
            switch (patient.Pact.ToString())
            { 
                case "����ҽ��":
                case "ҽ����ְ":
                case "ҽ����ְ":
                case "�������գ�ҽ����":
                case "ҽ����������":
                case "ҽ������":
                case "ҽ��-ѧ��ǰ��ͯ":
                case "ҽ��-�������":
                case "ҽ��-��Уѧ��":
                case "ҽ��-δ������":
                case "ҽ��-��ҵ��Ա":
                case "ҽ��-����ͳ��":
                case "��������":
                    this.lblPactNotice.Text = "����ͨ    ����������    ��ICU    ���ھ�    ���ۿ�    ���ǿ�ָ������   ����������";
                    break;
                case "����ҽ��":
                    this.lblPactNotice.Text = "����ͨ      ����������      ���ǿ�ָ������";
                    break;
                default:
                    this.lblPactNotice.Text = "����ͨ      ������ ";
                    break;
            
            }
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
            diagManager.ExecQuery(tempsql,ref ds1);
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {

                    this.lblPMoney.Text = ds1.Tables[0].Rows[i][0].ToString() + "Ԫ";
                    this.lblPCZMoney.Text = ds1.Tables[0].Rows[i][1].ToString() + "Ԫ";
                    this.lblPCCMoney.Text = ds1.Tables[0].Rows[i][2].ToString() + "Ԫ";
                }
            }

            
            //ȡ��Ժ��Ժ��ҩ����
            DataSet ds2 = new DataSet();
            diagManager.ExecQuery(tempsql2, ref ds2);
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
            diagManager.ExecQuery(tempsql3, ref ds3);
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
            //ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(patient.ID, Neusoft.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            //foreach (Neusoft.HISFC.Models.HealthRecord.Diagnose diag in al)
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
            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            Neusoft.HISFC.Components.Common.Classes.Function.GetPageSize("outcard", ref print);
            print.IsLandScape = true;
            //��ӱ��� zhao.chf
            //print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.Border;
            print.PrintPage(0, 0, this);
            return 1;
        }
        public int PrintView()
        {
            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            Neusoft.HISFC.Components.Common.Classes.Function.GetPageSize("outcard", ref print);
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
