using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace ProvinceAcrossSI.Controls
{
    public partial class ucProvinceAcrossBalanceBill : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucProvinceAcrossBalanceBill()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 公用业务实体
        /// </summary>
        ProvinceAcrossSI.Business.Common.CommonService comService = new ProvinceAcrossSI.Business.Common.CommonService();

        ProvinceAcrossSIDatabase dbmanager = new ProvinceAcrossSIDatabase();
        ProvinceAcrossSI.ProvinceAcrossSIDatabase SIMgr = new ProvinceAcrossSIDatabase();
        ProvinceAcrossSI.Funtion ydFuntionMgr = new Funtion();

        public void SetValues(FS.HISFC.Models.RADT.PatientInfo p, ProvinceAcrossSI.Objects.SIPersonInfo ps)
        {
            string Insure = string.Empty;
            ArrayList al = this.comService.QueryTYPEFormComDictionary("ProvinceCity");

            foreach (FS.FrameWork.Models.NeuObject obj in al)
            {
                if (obj.ID == ps.InsuredAreaCode)
                {
                    Insure = obj.Name;
                    break;
                }
            }
            if (string.IsNullOrEmpty(Insure))
            {
                Insure = ps.InsuredAreaCode;
            }
            //Insure = "天津市";
            string diagStr = string.Empty;
            string diagStr2 = string.Empty;
            //FS.FrameWork.Models.NeuObject diagn = new FS.FrameWork.Models.NeuObject();
            ArrayList alDia = new ArrayList();

            alDia = this.SIMgr.QueryICD(p.ID);
            if (alDia != null && alDia.Count > 0)
            {
                FS.FrameWork.Models.NeuObject diagn = alDia[0] as FS.FrameWork.Models.NeuObject;
                diagStr = diagn.Name;
                diagStr2 = diagn.User01;

            }
            //foreach (FS.FrameWork.Models.NeuObject diagn in alDia)
            //{
            //    if (diagn.ID == ps.Diagn1)
            //    {
            //        diagStr = diagn.Name;
            //        break;
            //    }
            //}

            //if (!string.IsNullOrEmpty(ps.Diagn2))
            //{
            //    foreach (FS.FrameWork.Models.NeuObject diagn in alDia)
            //    {
            //        if (diagn.ID == ps.Diagn2)
            //        {
            //            diagStr2 = diagn.Name;
            //            break;
            //        }
            //    }
            //}

            ProvinceAcrossSI.Objects.SIPersonInfo personInfo = new ProvinceAcrossSI.Objects.SIPersonInfo();
            this.ydFuntionMgr.GetHosCodeAndRegionCode(ref personInfo);//调测用，先写死，后需对照

            TimeSpan ts = p.PVisit.OutTime.Date - p.PVisit.InTime.Date;
            this.lblName.Text = ps.Name;
            this.lblSex.Text = p.Sex.Name;
            this.lbAge.Text = ps.Age.ToString();
            this.lblID.Text = ps.IdenNo;
            this.lblSSN.Text = ps.MCardNo;
            this.lblSAddress.Text = Insure;//ps.InsuredCenterAreaCode.ToString();
            this.neuLabel1.Text = string.Format("{0}跨省异地就医住院结算单", Insure);
            this.lblPact.Text = this.GetSiType(ps.xzlx); //ps.xzlx;
            this.lblCA.Text = "佛山市";
            this.lblHos.Text = FS.FrameWork.Management.Connection.Hospital.Name;
            this.lblHosDegree.Text = "二级甲等";
            this.lblInState.Text = "普通住院";
            this.lblSerialNo.Text = ps.ClinicNo;
            this.lblOutDept.Text = p.PVisit.PatientLocation.Dept.Name;
            this.lblMainDiag.Text = diagStr;
            this.lblSecDiag.Text = diagStr2;
            this.lblInDate.Text = p.PVisit.InTime.ToString("yyyy-MM-dd");
            this.lblOutDate.Text = p.PVisit.OutTime.ToString("yyyy-MM-dd");
            this.lbDays.Text = (ts.Days > 0) ? ts.Days.ToString() : "1";
            this.lblTotCost.Text = (ps.ake149 + ps.akb067).ToString();//ps.tot_cost.ToString();
            this.lblPubCost.Text = ps.ake171.ToString();//ps.YBTCZF_cost.ToString();
            this.lblOwnCost.Text = ps.own_cost_part.ToString();
            this.lblDegreeCost.Text = ps.limit_cost.ToString();
            this.lblPub1.Text = ps.ake149.ToString();
            this.lblPub2.Text = ps.TCJJZF_cost.ToString();
            this.lblPub3.Text = ps.ake038.ToString();
            this.lblPub4.Text = ps.GWYBZ_cost.ToString();
            this.lblPub5.Text = ps.ake026.ToString();
            this.lblPub6.Text = ps.ake181.ToString();
            this.lblPub7.Text = ps.ake032.ToString();
            this.lblPub8.Text = "0";
            this.lblPub9.Text = ps.ake173.ToString();
            this.lblOwnCash.Text = ps.akb067.ToString();
            this.lblPatientNO.Text = p.PID.ID;

            #region 作废
            //TimeSpan ts = p.PVisit.OutTime - p.PVisit.InTime;
            //this.neuSpread1_Sheet1.Cells["日期"].Text = "打印日期" + System.DateTime.Now.ToShortDateString();
            ////this.neuSpread1_Sheet1.Cells["医疗保机构名称"].Text = ps.InsuredAreaCode+"市社会基金管理局";
            
            //this.neuSpread1_Sheet1.Cells["医疗机构编码"].Text = personInfo.HospitalCode;

            
            ////this.neuSpread1_Sheet1.Cells["医疗机构编码"].Text = ps.ZZJGDM;

            //this.neuSpread1_Sheet1.Cells["异地就医申请号"].Text = ps.ClinicNo; //ps.YDJYBAH;

            //this.neuSpread1_Sheet1.Cells["人员类别"].Text = this.GetPersonType(ps.PersonType);
            //this.neuSpread1_Sheet1.Cells["单位名称"].Text = ps.CompanyName;
            //this.neuSpread1_Sheet1.Cells["住院号"].Text = p.PID.PatientNO;
            //this.neuSpread1_Sheet1.Cells["科别"].Text = p.PVisit.PatientLocation.Dept.Name;

            
            //this.neuSpread1_Sheet1.Cells["入院第一诊断"].Text = p.ClinicDiagnose;
            //this.neuSpread1_Sheet1.Cells["就诊类别"].Text = p.Pact.Name;
            //this.neuSpread1_Sheet1.Cells["结算时间"].Text = p.PVisit.OutTime.ToShortDateString();//p.BalanceDate.ToShortDateString();
            //this.neuSpread1_Sheet1.Cells["住院天数"].Text = (ts.Days > 0) ? ts.Days.ToString() : "1";

            
            //this.neuSpread1_Sheet1.Cells["自费金额"].Text = ps.own_cost_part.ToString();
            //this.neuSpread1_Sheet1.Cells["部分项目自付金额"].Text = ps.pay_cost_part.ToString();
            //this.neuSpread1_Sheet1.Cells["超限额以上费用"].Text = ps.CBXXEZF_cost.ToString();
            //this.neuSpread1_Sheet1.Cells["进入结算费用总金额"].Text = ps.pub_cost.ToString();
            
            
            //this.neuSpread1_Sheet1.Cells["重大疾病/大病保险支付费用"].Text = ps.DBYLTCZF_cost.ToString();
            //this.neuSpread1_Sheet1.Cells["公务员补助支付费用"].Text = ps.GWYBZ_cost.ToString();
            ////this.neuSpread1_Sheet1.Cells["记账费用"].Text = ps.DBYLTCZF_cost + ps.TCJJZF_cost + ps.DBBXYLTCLJ + ps.BCYLLJYZF_cost + ps.GWYBZ_cost + ps.GWYBZZF_cost + ps.GWYDB_cost + ps.QTZF + "";//ps.GWYBZ_cost.ToString();
            ////this.neuSpread1_Sheet1.Cells["记账费用"].Text = ps.YBTCZF_cost.ToString();//ps.TCJJZF_cost + ps.DBYLTCZF_cost + ps.GWYBZ_cost + ps.QTBZZF_cost + ""; //ps.YBTCZF_cost + ps.QTBZZF_cost + ps.GWYBZ_cost + ""; //ps.DBYLTCZF_cost + ps.TCJJZF_cost + ps.GWYBZ_cost + ps.GWYBZZF_cost + ps.GWYDB_cost + ps.QTZF + "";//2016.4.29 lu.jsh代改
            

            //this.neuSpread1_Sheet1.Cells["联系电话"].Text = p.Kin.RelationPhone;

            //this.neuSpread1_Sheet1.Cells["本次住院总费用"].Text = "         总费用  " + ps.tot_cost + " 元； "
            //    + " 记账费用 " + this.neuSpread1_Sheet1.Cells["记账费用"].Text + " 元； 个人自负费用  " + ps.GRZF_cost.ToString() + " 元";

            //this.neuSpread1_Sheet1.Cells["本社保年度累计支付"].Text = "已住院 " + ps.InTimes + " 次；"
            //                                                    + "  统筹累计已支付  " + ps.SumCostJBYL.ToString()
            //                                                    + "  元，重大疾病/大病保险累计支付  " + ps.DBYLTCZF_cost.ToString()
            //                                                    + "  元，补充医疗累计已支付  " + ps.BCYLLJYZF_cost.ToString()
            //                                                    + "  元，公务员补助累计已支付  " + ps.GWYBCLJYZF_cost.ToString() + " 元";
            #endregion 

        }

        private string GetSiType(string code)
        {
            ArrayList arrRet = new System.Collections.ArrayList();
            FS.HISFC.Models.Base.Item obj1 = new FS.HISFC.Models.Base.Item();
            obj1.ID = "310";
            obj1.Name = "城镇职工基本医疗保险";
            arrRet.Add(obj1);
            FS.HISFC.Models.Base.Item obj2 = new FS.HISFC.Models.Base.Item();
            obj2.ID = "360";
            obj2.Name = "老红军医疗保障";
            arrRet.Add(obj2);
            FS.HISFC.Models.Base.Item obj3 = new FS.HISFC.Models.Base.Item();
            obj3.ID = "320";
            obj3.Name = "公务员医疗补助";
            arrRet.Add(obj3);
            FS.HISFC.Models.Base.Item obj4 = new FS.HISFC.Models.Base.Item();
            obj4.ID = "370";
            obj4.Name = "企业补充医疗保险";
            arrRet.Add(obj4);
            FS.HISFC.Models.Base.Item obj5 = new FS.HISFC.Models.Base.Item();
            obj5.ID = "330";
            obj5.Name = "大额医疗费用补助";
            arrRet.Add(obj5);
            FS.HISFC.Models.Base.Item obj6 = new FS.HISFC.Models.Base.Item();
            obj6.ID = "380";
            obj6.Name = "新型农村合作医疗";
            arrRet.Add(obj6);
            FS.HISFC.Models.Base.Item obj7 = new FS.HISFC.Models.Base.Item();
            obj7.ID = "340";
            obj7.Name = "离休人员医疗保障";
            arrRet.Add(obj7);
            FS.HISFC.Models.Base.Item obj8 = new FS.HISFC.Models.Base.Item();
            obj8.ID = "390";
            obj8.Name = "城乡居民基本医疗保险";
            arrRet.Add(obj8);
            FS.HISFC.Models.Base.Item obj9 = new FS.HISFC.Models.Base.Item();
            obj9.ID = "350";
            obj9.Name = "一至六级残疾军人医疗补助";
            arrRet.Add(obj9);
            FS.HISFC.Models.Base.Item obj10 = new FS.HISFC.Models.Base.Item();
            obj10.ID = "391";
            obj10.Name = "城镇居民基本医疗保险";
            arrRet.Add(obj10);
            FS.HISFC.Models.Base.Item obj11 = new FS.HISFC.Models.Base.Item();
            obj11.ID = "410";
            obj11.Name = "工伤保险";
            arrRet.Add(obj11);
            FS.HISFC.Models.Base.Item obj12 = new FS.HISFC.Models.Base.Item();
            obj12.ID = "510";
            obj12.Name = "生育保险";
            arrRet.Add(obj12);

            string strRet = (from FS.HISFC.Models.Base.Item obj in arrRet
                             where obj.ID.Equals(code)
                             select obj.Name).FirstOrDefault();
            return strRet;
        }

        private string GetPersonType(string code)
        {

            ArrayList al = this.comService.QueryTYPEFormComDictionary("YD_PERSON_TYPE");

            foreach (FS.FrameWork.Models.NeuObject obj in al)
            {
                if (obj.ID == code)
                {
                    return obj.Name;
                }
            }
            return null;
        }

         private void Clear()
        {
            foreach (Control c in this.panAll.Controls)
            {
                if (c.Tag != null && c.Tag.ToString() == "NULL")
                {
                    c.Text = "";
                }
            }        
        }

        public int Print()
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();

            p.IsHaveGrid = false;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize("YDInPatientZHJSD", 830, 1140);// {80867F3F-AE91-4525-82AF-0618AB01C92B}
            p.SetPageSize(ps);

            p.PrintPage(15, 3, this);
            return 1;

        }
    }
}

