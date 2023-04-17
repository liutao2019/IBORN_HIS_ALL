using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FoShanYDSI.Controls
{
    public partial class ucYDInBalanceInfo : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucYDInBalanceInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 公用业务实体
        /// </summary>
        FoShanYDSI.Business.Common.CommonService comService = new FoShanYDSI.Business.Common.CommonService();

        FoShanYDSIDatabase dbmanager = new FoShanYDSIDatabase();
        FoShanYDSI.FoShanYDSIDatabase SIMgr = new FoShanYDSIDatabase();
        FoShanYDSI.Funtion ydFuntionMgr = new Funtion();

        public void SetValues(FS.HISFC.Models.RADT.PatientInfo p, FoShanYDSI.Objects.SIPersonInfo ps)
        {
            string Insure = string.Empty;
            ArrayList al = this.comService.QueryTYPEFormComDictionary("YD_AREA_CODE");
            
            foreach (FS.FrameWork.Models.NeuObject obj in al)
            {
                if (obj.ID == ps.InsuredAreaCode)
                {
                    Insure = obj.Name;
                    break;
                }
            }
            string diagStr = string.Empty;
            //FS.FrameWork.Models.NeuObject diagn = new FS.FrameWork.Models.NeuObject();
            ArrayList alDia = new ArrayList();

            alDia = this.SIMgr.QueryICD();
            foreach (FS.FrameWork.Models.NeuObject diagn in alDia)
            {
                if (diagn.ID == ps.Diagn1)
                {
                    diagStr = diagn.Name;
                    break;
                }
            }

            FoShanYDSI.Objects.SIPersonInfo personInfo = new FoShanYDSI.Objects.SIPersonInfo();
            this.ydFuntionMgr.GetHosCodeAndRegionCode(ref personInfo);//调测用，先写死，后需对照

            TimeSpan ts = p.PVisit.OutTime.Date - p.PVisit.InTime.Date;
            this.neuSpread1_Sheet1.Cells["日期"].Text = "打印日期" + System.DateTime.Now.ToShortDateString();
            //this.neuSpread1_Sheet1.Cells["医疗保机构名称"].Text = ps.InsuredAreaCode+"市社会基金管理局";
            this.neuSpread1_Sheet1.Cells["医疗机构名称"].Text = FS.FrameWork.Management.Connection.Hospital.Name;
            this.neuSpread1_Sheet1.Cells["医疗机构编码"].Text = personInfo.HospitalCode;
            this.neuSpread1_Sheet1.Cells["医院等级"].Text = "二级甲等";
            this.neuSpread1_Sheet1.Cells["就诊序列号"].Text = ps.JSYWH;
            //this.neuSpread1_Sheet1.Cells["医疗机构编码"].Text = ps.ZZJGDM;
            this.neuSpread1_Sheet1.Cells["姓名"].Text = p.Name;
            this.neuSpread1_Sheet1.Cells["性别"].Text = p.Sex.Name;
            this.neuSpread1_Sheet1.Cells["出生年月"].Text = p.Birthday.ToString("yyyy-MM-dd");
            this.neuSpread1_Sheet1.Cells["异地就医申请号"].Text = ps.ClinicNo; //ps.YDJYBAH;
            this.neuSpread1_Sheet1.Cells["社会保障卡号"].Text = ps.MCardNo;
            this.neuSpread1_Sheet1.Cells["居民身份证号"].Text = ps.IdenNo;
            this.neuSpread1_Sheet1.Cells["参保地"].Text = Insure;//ps.InsuredCenterAreaCode.ToString();
            this.neuSpread1_Sheet1.Cells["就医地"].Text = "佛山市";
            this.neuSpread1_Sheet1.Cells["医疗保险类型"].Text = this.GetSiType(ps.xzlx); //ps.xzlx;
            this.neuSpread1_Sheet1.Cells["人员类别"].Text = this.GetPersonType(ps.PersonType);
            this.neuSpread1_Sheet1.Cells["单位名称"].Text = ps.CompanyName;
            this.neuSpread1_Sheet1.Cells["住院号"].Text = p.PID.PatientNO;
            this.neuSpread1_Sheet1.Cells["科别"].Text = p.PVisit.PatientLocation.Dept.Name;
            this.neuSpread1_Sheet1.Cells["入院时间"].Text = p.PVisit.InTime.ToString("yyyy-MM-dd");
            this.neuSpread1_Sheet1.Cells["出院时间"].Text = p.PVisit.OutTime.ToString("yyyy-MM-dd");
            //if (p.Diagnoses.Count > 0)
            //{
                this.neuSpread1_Sheet1.Cells["出院第一诊断"].Text = diagStr;//p.Diagnoses[0].ToString();
            //}
                this.neuSpread1_Sheet1.Cells["入院第一诊断"].Text = p.ClinicDiagnose;
            this.neuSpread1_Sheet1.Cells["就诊类别"].Text = p.Pact.Name;
            this.neuSpread1_Sheet1.Cells["结算时间"].Text = p.PVisit.OutTime.ToShortDateString();//p.BalanceDate.ToShortDateString();
            this.neuSpread1_Sheet1.Cells["住院天数"].Text = (ts.Days > 0) ? ts.Days.ToString() : "1";

            this.neuSpread1_Sheet1.Cells["医疗费用总金额"].Text = ps.tot_cost.ToString();
            this.neuSpread1_Sheet1.Cells["自费金额"].Text = ps.own_cost_part.ToString();
            this.neuSpread1_Sheet1.Cells["部分项目自付金额"].Text = ps.pay_cost_part.ToString();
            this.neuSpread1_Sheet1.Cells["超限额以上费用"].Text = ps.CBXXEZF_cost.ToString();
            this.neuSpread1_Sheet1.Cells["进入结算费用总金额"].Text = ps.pub_cost.ToString();
            this.neuSpread1_Sheet1.Cells["本次起付标准"].Text = ps.limit_cost.ToString();
            this.neuSpread1_Sheet1.Cells["基本统筹基金支付费用"].Text = ps.TCJJZF_cost.ToString();//ps.YBTCZF_cost.ToString();//
            this.neuSpread1_Sheet1.Cells["重大疾病/大病保险支付费用"].Text = ps.DBYLTCZF_cost.ToString();
            this.neuSpread1_Sheet1.Cells["补充/补助保险等支付费用"].Text = ps.QTBZZF_cost + "";
            this.neuSpread1_Sheet1.Cells["公务员补助支付费用"].Text = ps.GWYBZ_cost.ToString();
            //this.neuSpread1_Sheet1.Cells["记账费用"].Text = ps.DBYLTCZF_cost + ps.TCJJZF_cost + ps.DBBXYLTCLJ + ps.BCYLLJYZF_cost + ps.GWYBZ_cost + ps.GWYBZZF_cost + ps.GWYDB_cost + ps.QTZF + "";//ps.GWYBZ_cost.ToString();
            this.neuSpread1_Sheet1.Cells["医疗救助金额"].Text = ps.ykc641.ToString();
            this.neuSpread1_Sheet1.Cells["记账费用"].Text = ps.YBTCZF_cost.ToString();//ps.TCJJZF_cost + ps.DBYLTCZF_cost + ps.GWYBZ_cost + ps.QTBZZF_cost + ""; //ps.YBTCZF_cost + ps.QTBZZF_cost + ps.GWYBZ_cost + ""; //ps.DBYLTCZF_cost + ps.TCJJZF_cost + ps.GWYBZ_cost + ps.GWYBZZF_cost + ps.GWYDB_cost + ps.QTZF + "";//2016.4.29 lu.jsh代改
            this.neuSpread1_Sheet1.Cells["个人自负费用"].Text = (ps.GRZF_cost + ps.GZZF_cost) + "";

            this.neuSpread1_Sheet1.Cells["联系电话"].Text = p.Kin.RelationPhone;
            this.neuSpread1_Sheet1.Cells["救助对象类型"].Text = ps.JZDXLX == "1" ? "是" : "否";
            
            
            this.neuSpread1_Sheet1.Cells["本次住院总费用"].Text = "         总费用  " + ps.tot_cost + " 元； "
                + " 记账费用 " + this.neuSpread1_Sheet1.Cells["记账费用"].Text + " 元； 个人自负费用  " + (ps.GRZF_cost + ps.GZZF_cost).ToString() + " 元";

            this.neuSpread1_Sheet1.Cells["本社保年度累计支付"].Text = "已住院 " + ps.InTimes + " 次；"
                                                                + "  统筹累计已支付  " + ps.SumCostJBYL.ToString()
                                                                + "  元，重大疾病/大病保险累计支付  " + ps.DBYLTCZF_cost.ToString()
                                                                + "  元，补充医疗累计已支付  " + ps.BCYLLJYZF_cost.ToString()
                                                                + "  元，公务员补助累计已支付  " + ps.GWYBCLJYZF_cost.ToString()
                                                                + "  元，本年度医疗救助累计金额  " + ps.ykc753.ToString()
                                                                + "  元，其他累计支付  " + ps.qtlj.ToString() + " 元";

            //this.neuSpread1_Sheet1.Cells["医疗费用总金额2"].Text = "医疗费用总金额：人民币  " + FS.FrameWork.Public.String.LowerMoneyToUpper(ps.tot_cost) + "，小写：" + ps.tot_cost.ToString();

            //this.neuSpread1_Sheet1.Cells["结算序号"].Text = ps.JSYWH;//ps.ClinicNo;
            //this.neuSpread1_Sheet1.Cells["发票编号"].Text = p.ID;
            
            //this.neuSpread1_Sheet1.Cells["统筹累计已支付"].Text = "统筹累计已支付  " + ps.SumCostJBYL.ToString() +"   元，补充医疗累计已支付  "
            //                                                     + ps.BCYLLJYZF_cost.ToString() + "  元，公务员补助累计已支付  " + ps.GWYBCLJYZF_cost.ToString();
            //this.neuSpread1_Sheet1.Cells["医疗费用总金额2"].Text = "医疗费用总金额：人民币  " + FS.FrameWork.Public.String.LowerMoneyToUpper(ps.tot_cost) + "，小写：" + ps.tot_cost.ToString();
            //医保各项基金支付金额2 找不到，暂不处理
            //this.neuSpread1_Sheet1.Cells["医保各项基金支付金额2"].Text = "医疗费用总金额：人民币  " + FS.FrameWork.Public.String.LowerMoneyToUpper(ps.tot_cost) + "，小写：" + ps.tot_cost.ToString();
            //this.neuSpread1_Sheet1.Cells["个人支付金额合计2"].Text = "医疗费用总金额：人民币  " + FS.FrameWork.Public.String.LowerMoneyToUpper(ps.GRZF_cost) + "，小写：" + ps.GRZF_cost.ToString();
            //this.SetCost(p.ID);
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

        public void SetCost(string inpatienNo)
        {
            System.Collections.ArrayList al= this.dbmanager.GetYDFeeList(inpatienNo);
            foreach (FS.FrameWork.Models.NeuObject Obj in al)
            {
                if (Obj.Name == "床位费")
                {
                    this.neuSpread1_Sheet1.Cells["床位费1"].Text = Obj.Memo;
                }

                if (Obj.Name == "西药费")
                {
                    this.neuSpread1_Sheet1.Cells["西药费1"].Text = Obj.Memo;
                }

                if (Obj.Name == "中药费")
                {
                    this.neuSpread1_Sheet1.Cells["中药费1"].Text = Obj.Memo;
                }

                if (Obj.Name == "中成药")
                {
                    this.neuSpread1_Sheet1.Cells["中成药1"].Text = Obj.Memo;
                }

                if (Obj.Name == "中草药")
                {
                    this.neuSpread1_Sheet1.Cells["中草药1"].Text = Obj.Memo;
                }

                if (Obj.Name == "检查费")
                {
                    this.neuSpread1_Sheet1.Cells["检查费1"].Text = Obj.Memo;
                }

                if (Obj.Name == "治疗费")
                {
                    this.neuSpread1_Sheet1.Cells["治疗费1"].Text = Obj.Memo;
                }

                if (Obj.Name == "放射费")
                {
                    this.neuSpread1_Sheet1.Cells["放射费1"].Text = Obj.Memo;
                }

                if (Obj.Name == "治疗费")
                {
                    this.neuSpread1_Sheet1.Cells["治疗费1"].Text = Obj.Memo;
                }

                if (Obj.Name == "放射费")
                {
                    this.neuSpread1_Sheet1.Cells["放射费1"].Text = Obj.Memo;
                }

                if (Obj.Name == "手术费")
                {
                    this.neuSpread1_Sheet1.Cells["手术费1"].Text = Obj.Memo;
                }

                if (Obj.Name == "化验费" || Obj.Name == "检验费")
                {
                    this.neuSpread1_Sheet1.Cells["化验费1"].Text = Obj.Memo;
                }

                if (Obj.Name == "输血费")
                {
                    this.neuSpread1_Sheet1.Cells["输血费1"].Text = Obj.Memo;
                }

                if (Obj.Name == "输氧费")
                {
                    this.neuSpread1_Sheet1.Cells["输氧费1"].Text = Obj.Memo;
                }

                if (Obj.Name == "其它")
                {
                    this.neuSpread1_Sheet1.Cells["其它1"].Text = Obj.Memo;
                }

                if (Obj.Name == "麻醉费")
                {
                    this.neuSpread1_Sheet1.Cells["麻醉费1"].Text = Obj.Memo;
                }

                if (Obj.Name == "材料费")
                {
                    this.neuSpread1_Sheet1.Cells["材料费1"].Text = Obj.Memo;
                }

                if (Obj.Name == "特殊检查费")
                {
                    this.neuSpread1_Sheet1.Cells["特殊检查费1"].Text = Obj.Memo;
                }

                if (Obj.Name == "特殊治疗费")
                {
                    this.neuSpread1_Sheet1.Cells["特殊治疗费1"].Text = Obj.Memo;
                }

                if (Obj.Name == "诊查费")
                {
                    this.neuSpread1_Sheet1.Cells["诊疗费(诊查费)1"].Text = Obj.Memo;
                }

                if (Obj.Name == "护理费")
                {
                    this.neuSpread1_Sheet1.Cells["护理费1"].Text = Obj.Memo;
                }

                if (Obj.Name == "诊金")
                {
                    this.neuSpread1_Sheet1.Cells["诊金1"].Text = Obj.Memo;
                }

                if (Obj.Name == "检查费(CT)")
                {
                    this.neuSpread1_Sheet1.Cells["检查费(CT)1"].Text = Obj.Memo;
                }

                if (Obj.Name == "检查费(MRI)")
                {
                    this.neuSpread1_Sheet1.Cells["检查费(MRI)1"].Text = Obj.Memo;
                }

                if (Obj.Name == "检查费(其它)")
                {
                    this.neuSpread1_Sheet1.Cells["检查费(其它)1"].Text = Obj.Memo;
                }

                if (Obj.Name == "特需服务费")
                {
                    this.neuSpread1_Sheet1.Cells["特需服务费1"].Text = Obj.Memo;
                }

                if (Obj.Name == "杂费" || Obj.Name == "其他")
                {
                    this.neuSpread1_Sheet1.Cells["杂费1"].Text = Obj.Memo;
                }

                if (Obj.Name == "挂号费" )
                {
                    this.neuSpread1_Sheet1.Cells["挂号费1"].Text = Obj.Memo;
                }
            }
        }

         public int Print()
        {
            //FS.FrameWork.WinForms.Classes.Print printManeger = new FS.FrameWork.WinForms.Classes.Print();
            //printManeger.PrintPage(5, 5, this);
            this.CommonPrint(this.neuSpread1, 0);
            return 1;

        }


        /// <summary>
        /// 通用的打印表格方法
        /// </summary>
        /// <param name="fpview"></param>
        /// <param name="fp"></param>
        /// <param name="index"></param>
        private void CommonPrint(FarPoint.Win.Spread.FpSpread fp, int index)
        {
            try
            {

                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                FS.HISFC.Models.Base.PageSize ps = pgMgr.GetPageSize("YDInPatientZHJSD");  //住院佛山异地医保结算单
                if (ps == null)
                {
                    //默认大小
                    ps = new FS.HISFC.Models.Base.PageSize("YDInPatientZHJSD", 830, 1140);
                }
                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                print.SetPageSize(ps);

                if ((FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).IsManager)
                {
                    //print.PrintPreview(ps.Left, ps.Top, this);
                    print.PrintPage(ps.Left, ps.Top, this);
                }
                else
                {
                    print.PrintPage(ps.Left, ps.Top, this);
                }

                return;

                #region 废弃

                if (fp.Sheets[0].RowCount == 0)
                    return;

                FarPoint.Win.Spread.PrintInfo pi = new FarPoint.Win.Spread.PrintInfo();
                //DialogResult result = MessageBox.Show("是否要横向打印?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);//提示横向打印
                FarPoint.Win.Spread.StyleInfo style = new FarPoint.Win.Spread.StyleInfo();
                //style.Border = new FarPoint.Win.LineBorder(Color.Black, 1);
                style.BackColor = Color.White;
                fp.Sheets[0].ColumnHeader.DefaultStyle = style;
                fp.Sheets[0].RowHeader.DefaultStyle = style;

                //if (result == DialogResult.Yes)
                //{
                //    pi.Orientation = FarPoint.Win.Spread.PrintOrientation.Landscape;
                //}
                //else
                //{
                pi.Orientation = FarPoint.Win.Spread.PrintOrientation.Portrait;
                //}

                FarPoint.Win.Spread.PrintMargin pm = new FarPoint.Win.Spread.PrintMargin();
                pm.Left = 20;
                pm.Right = 20;
                pm.Top = 20;
                pm.Bottom = 20;
                pi.FirstPageNumber = 1;
                //pi.Footer = "当前第 /p 页 共 /pc 页";
                //pi.Margin = pm;


                //pi.RepeatRowStart = 0;
                //pi.RepeatRowEnd = 2;
                //pi.RowStart = 0;
                //pi.RowEnd = 19;
                //pi.PrintType = FarPoint.Win.Spread.PrintType.PageRange;
                //pi.PaperSize.Height = 159;//该方式会报错
                //pi.PaperSize.Width = 1080;


                pi.PageStart = 1;
                pi.Preview = false;
                pi.ShowBorder = false;
                pi.ShowColor = false;
                pi.ShowColumnHeaders = false;
                pi.ShowGrid = false;
                pi.ShowRowHeaders = false;//原来为true
                pi.ShowShadows = false;
                pi.ZoomFactor = 1;
                pi.ShowPrintDialog = false;
                pi.UseSmartPrint = true;

                fp.Sheets[0].PrintInfo = pi;
                fp.PrintSheet(index);

                #endregion

            }
            catch
            {
                MessageBox.Show("打印发生错误,请确认是否有连接好打印机");
            }
        }
    }
}

