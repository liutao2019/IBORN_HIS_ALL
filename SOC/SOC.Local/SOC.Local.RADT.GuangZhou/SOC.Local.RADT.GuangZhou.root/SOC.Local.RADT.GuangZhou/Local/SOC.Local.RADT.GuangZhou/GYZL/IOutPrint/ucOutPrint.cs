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
    /// 出院通知单打印
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



        //默认打印正常出院通知到
        string nameFlag = "0";

        /// <summary>
        /// 通知到打印类型
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
                //        this.neuLabel1.Text = "出院";
                //        break;
                //    case "1":
                //        this.neuLabel1.Text = "转院";
                //        //this.Paint += new PaintEventHandler(MyDrawRectangle);
                //        //this.BorderStyle = BorderStyle.FixedSingle;
                //        this.AddPanelRect();
                //        break;
                //    case "2":
                //        this.neuLabel1.Text = "转科";
                //        break;
                //    default:
                //        break;
                //}
                //this.neuLabel1.Text += "结算通知单";

                

            }
        }

        public int SetValue(Neusoft.HISFC.Models.RADT.PatientInfo patient)
        {
            
            #region //出院带药SQL语句
            /// <summary>
            /// 出院带药SQL语句
            /// </summary>

            //获取出院带药费用
            string sql1 = @"select nvl(sum( case when r.class_code='P' then sum(w.tot_cost)else 0 end ),0)sum_p,
                             nvl(sum( case when r.class_code='PCZ' then sum(w.tot_cost)else 0 end ),0)sum_pcz,
                             nvl(sum( case when r.class_code='PCC' then sum(w.tot_cost)else 0 end ),0)sum_pcc      
                            from fin_ipb_medicinelist w , MET_IPM_ORDER r where w.inpatient_no=r.inpatient_no
                            and w.mo_order=r.mo_order and r.type_code='CD'and r.mo_stat in ('1','2') 
                             and w.inpatient_no='{0}'
                            group by r.class_code  ";
            //取出院带院中药剂数
            string sql2 = @"select r.use_days from MET_IPM_ORDER r 
                        where r.type_code='CD'and r.mo_stat in ('1','2')  and r.class_code='PCC'
                          and r.inpatient_no='{0}'
                          group by r.class_code ,use_days";
            //取出院带药附材费
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

            string sysday = System.DateTime.Now.ToString("d");//获取系统日期
            string syshm = System.DateTime.Now.ToString("t");//获取系统时分
           // this.lblBlood.Text = patient.BloodType.Name ;//血型 
            this.lblOutStat.Text = managerBizProcess.GetConstansObj(Neusoft.HISFC.Models.Base.EnumConstant.ZG.ToString(), patient.PVisit.ZG.ID).Name;//出院情况
            this.lblOutDate1.Text = patient.PVisit.OutTime.ToShortDateString();//出院日期
            //this.lblSystemDay1.Text = patient.PVisit.OutTime.ToShortDateString();//出院日期
            this.lblBedNo1.Text = patient.PVisit.PatientLocation.Bed.ID;//床号
            this.lblName1.Text = patient.Name;//姓名 
            this.lblSex.Text = patient.Sex.ToString();//性别
            this.lblPatientNo1.Text = patient.PID.PatientNO;//住院号 
            this.lblDept.Text = patient.PVisit.PatientLocation.NurseCell.Name + ":" + Neusoft.FrameWork.Management.Connection.Operator.Name; ;//病区:操作员
            this.lblDial.Text = patient.MainDiagnose;//住院主表的诊断
            this.lblSystemDate1.Text = sysday + " " + syshm;  //系统时间
            this.lblSystemDay.Text = sysday;//系统日期
            this.lblSystemMin.Text = syshm;//系统时分
            this.lblWjMoney.Text = Neusoft.FrameWork.Public.String.ToSimpleString(patient.FT.TotCost);//未结总金额
            this.lblAjMoney.Text = Neusoft.FrameWork.Public.String.ToSimpleString(patient.FT.PrepayCost + patient.FT.BalancedPrepayCost);//预交金总金额
            this.lblOperName.Text = Neusoft.FrameWork.Management.Connection.Operator.Name;
            //医何患者类型
            this.lblPact.Text = patient.Pact.ToString();  
            switch (patient.Pact.ToString())
            { 
                case "广州医保":
                case "医保在职":
                case "医保离职":
                case "生育保险（医保）":
                case "医保（门慢）":
                case "医保门特":
                case "医保-学龄前儿童":
                case "医保-老年居民":
                case "医保-在校学生":
                case "医保-未成年人":
                case "医保-无业人员":
                case "医保-门诊统筹":
                case "生育保险":
                    this.lblPactNotice.Text = "□普通    □恶性肿瘤    □ICU    □内镜    □眼科    □骨科指定手术   □生育保险";
                    break;
                case "深圳医保":
                    this.lblPactNotice.Text = "□普通      □恶性肿瘤      □骨科指定手术";
                    break;
                default:
                    this.lblPactNotice.Text = "□普通      □肿瘤 ";
                    break;
            
            }
            //this.label8.Text = patient.Pact.ToString();

            //入院情况
            if (patient.PVisit.Circs.ID == "1")
            {
                this.lblInStat.Text = "一般";

            }
            else if (patient.PVisit.Circs.ID == "2")
            {
                this.lblInStat.Text = "急";

            }
            else if (patient.PVisit.Circs.ID == "3")
            {
                this.lblInStat.Text = "危";

            }
            else
            {
                lblInStat.Text = "无";
            }
        

            string tempsql = string.Format(sql1, patient.ID);
            string tempsql2 = string.Format(sql2, patient.ID);
            string tempsql3 = string.Format(sql3, patient.ID);
            // 获取出院带药费用
            DataSet ds1=new DataSet();
            diagManager.ExecQuery(tempsql,ref ds1);
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {

                    this.lblPMoney.Text = ds1.Tables[0].Rows[i][0].ToString() + "元";
                    this.lblPCZMoney.Text = ds1.Tables[0].Rows[i][1].ToString() + "元";
                    this.lblPCCMoney.Text = ds1.Tables[0].Rows[i][2].ToString() + "元";
                }
            }

            
            //取出院带院中药剂数
            DataSet ds2 = new DataSet();
            diagManager.ExecQuery(tempsql2, ref ds2);
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                {

                    this.lblPCCjs.Text = ds2.Tables[0].Rows[i][0].ToString()+"剂";

                }
            }
            else
            {
                this.lblPCCjs.Text = "0剂";
            }
            
           //取出院带药附材费
            DataSet ds3 = new DataSet();
            diagManager.ExecQuery(tempsql3, ref ds3);
            if (ds3 != null && ds3.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
                {

                    this.lblZsf.Text = ds3.Tables[0].Rows[i][0].ToString() + "元";
                    
                }
            }
           


         
          



            ///医生医嘱的出院诊断
            //string strDial = string.Empty;
            //string strMainDial = string.Empty;
            //ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(patient.ID, Neusoft.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            //foreach (Neusoft.HISFC.Models.HealthRecord.Diagnose diag in al)
            //{
            //    if (diag.DiagInfo.DiagType.ID.Equals("14"))//出院诊断
            //    {
            //        strDial += diag.DiagInfo.ICD10.Name + "、";
            //    }

            //    if (diag.DiagInfo.DiagType.ID.Equals("1"))//出院诊断
            //    {
            //        strMainDial += diag.DiagInfo.ICD10.Name + "、";
            //    }
            //}

            //if (strDial.Length > 0)
            //{
            //    this.lblDial.Text = strDial.Substring(0, strDial.Length - 1); //出院诊断
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
            //添加边线 zhao.chf
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
