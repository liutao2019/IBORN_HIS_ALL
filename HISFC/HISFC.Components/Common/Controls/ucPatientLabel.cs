using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Common.Controls
{
    public partial class ucPatientLabel : UserControl
    {
        /// <summary>
        /// his患者标签功能
        /// 通过连接中间服务器
        /// 最终为crm添加特定的患者标签
        /// 查看患者标签
        /// 2019-08-29 胡云贵
        /// {0F599816-C860-40e1-856A-EF5ACACBDA26}
        /// {9A0B93B1-AB81-47e9-BFE1-61A6DCE2C4A8}
        /// </summary>
        string crmId = "";
        string hisId = "";
        string hisurl = "";
        // 创建the ToolTip 标签气泡
        ToolTip toolTip1 = new ToolTip();
        //套餐气泡
        ToolTip taocanTip = new ToolTip();
        //投诉气泡
        ToolTip tousuTip = new ToolTip();
        //会员气泡
        ToolTip huiyuanTip = new ToolTip();
        //生日气泡
        ToolTip shengriTip = new ToolTip();

        string birthdayTipText = "";
        string tousuTipTip = "";
        string huiyuanTipText = "";

        private static FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();

        //当前标签list
        List<FS.HISFC.Models.Label.PatientLabel> currentLabelList = new List<FS.HISFC.Models.Label.PatientLabel>();

        //当前套餐
        List<string> packagelist = new List<string>();

        //代收套餐 {f60c6fff-8d03-4d28-92f3-8caa29a81a32}
        List<string> dspackagelist = new List<string>();

        public ucPatientLabel()
        {
            InitializeComponent();
            //this.Load += new EventHandler(shentiEnter);

        }

        /*
        public void ucPatientLabel_Load(object sender, EventArgs e)
        {
            //getUserLabelByHisCardNo();
        }
         * */


        //通过his卡号获取crmid
        public string getCrmIdByHisId(string hisno)
        {
            FS.HISFC.Components.Common.Report.DB db = new FS.HISFC.Components.Common.Report.DB();
            //string crmid = db.GetSequence("select crmid from com_patientinfo where card_no = '" + hisno + "'");
            string crmid = db.ExecSqlReturnOne("select crmid from com_patientinfo where card_no = '" + hisno + "'");
            //string crmid = db.GetSequence("select crmid from com_patientinfo where card_no = '" + hisno + "'");
            return crmid;
        }

        public string getHisServerUrl()
        {
            if (this.hisurl == null || hisurl == "")
            {
                FS.HISFC.Components.Common.Report.DB db = new FS.HISFC.Components.Common.Report.DB();
                string hisUrl = db.ExecSqlReturnOne("select MARK from com_dictionary where type = 'HISSERVERURL'");
                hisurl = hisUrl;
            }
            return hisurl;
        }

        //通过his卡号查询近一年购买套餐项目
        public List<string> getPackageByCardNO(string hiscardno)
        {
            List<string> lists = new List<string>();
            if (this.hisId != null && hisId != "")
            {

                DataTable dt = new DataTable();
                FS.HISFC.Components.Common.Report.DB db = new FS.HISFC.Components.Common.Report.DB();
                int res = db.QueryDataBySql(@"select distinct PARENTPACKAGENAME from exp_package 
                                    where card_no='{0}' 
                                      and delimiting_date between add_months(sysdate,-12)  and sysdate and cancel_flag='0'", ref dt, hiscardno);

                if (dt == null)
                {
                    //查不到表错误
                    return lists;
                }
                else if (dt.Rows.Count == 0)
                {
                    return lists;
                }

                //查询出套餐内容
                foreach (DataRow dr in dt.Rows)
                {
                    string name = dr["PARENTPACKAGENAME"].ToString();
                    lists.Add(name);
                }


            }
            return lists;
        }

        /// <summary>
        /// 是否七天内再次入院   {b79dd8d4-7fc0-4434-806c-00e8ff4d9423}
        /// </summary>
        /// <param name="hiscardno"></param>
        /// <returns></returns>
        private bool getIndateByCardNO(string hiscardno)
        {
            if (!string.IsNullOrEmpty(hiscardno))
            {
                string sql = @"select in_date from (select in_date from fin_ipr_inmaininfo where  card_no='{0}' and in_state !='N' order by in_date desc ) where rownum<3";

                DataTable dt = new DataTable();
                FS.HISFC.Components.Common.Report.DB db = new FS.HISFC.Components.Common.Report.DB();
                int res = db.QueryDataBySql(sql, ref dt, hiscardno);

                if (dt == null)
                {
                    //查不到表错误
                    return false;
                }
                else if (dt.Rows.Count == 0 || dt.Rows.Count < 2)
                {
                    return false;
                }

                DateTime timedate = DateTime.Parse(dt.Rows[0]["in_date"].ToString());

                DateTime nexttimedate = DateTime.Parse(dt.Rows[1]["in_date"].ToString());

                if (nexttimedate.AddDays(7) >= timedate)
                {
                    return true;
                }
                else
                {
                    return false;
                }

                
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// 代收  {f60c6fff-8d03-4d28-92f3-8caa29a81a32}
        /// </summary>
        /// <param name="hiscardno"></param>
        /// <returns></returns>
        public List<string> getDsPackageByCardNO(string hiscardno)
        {
            List<string> lists = new List<string>();
            if (this.hisId != null && hisId != "")
            {
                //{0e9be5d5-fe6d-4cf8-809f-3547292d1cc9} 同套餐未匹配报表逻辑
                string sql = @"   select t3.package_name packagename from exp_package t2
                               left join bd_com_package t3 on t2.package_id = t3.package_id
                               left join exp_packagedetail epd on t2.clinic_code=epd.clinic_code and t2.trans_type=epd.trans_type
                               where  t3.package_kind in ('21','27','34') and epd.RTN_QTY>0 and  t2.pay_flag=1 and t2.CANCEL_FLAG=0 and t2.card_no='{0}' ";

                DataTable dt = new DataTable();
                FS.HISFC.Components.Common.Report.DB db = new FS.HISFC.Components.Common.Report.DB();
                int res = db.QueryDataBySql(sql, ref dt, hiscardno);

                if (dt == null)
                {
                    //查不到表错误
                    return null;
                }
                else if (dt.Rows.Count == 0)
                {
                    return null;
                }

                //查询出套餐内容
                foreach (DataRow dr in dt.Rows)
                {
                    string name = dr["packagename"].ToString();
                    lists.Add(name);
                }


            }
            return lists;

        }



        //初始化标签信息
        public List<PatientLabel> getUserLabelByHisCardNo(string hisno)
        {
            #region 请求结果
            /*
             <res>
             * <resultCode>1</resultCode>
             * <resultDesc>查询成功</resultDesc>
             * <labelList>
                 * <patientid>1000005031</patientid>
                 * <labelType></labelType>
                 * <labelId>999</labelId>
                 * <labelContent>对服务不满意</labelContent>
                 * <memo>测试测试测试</memo>
             * </labelList>
             * <labelList>
                 * <patientid>1000005031</patientid>
                 * <labelType></labelType>
                 * <labelId>999</labelId>
                 * <labelContent>挑剔</labelContent>
                 * <memo></memo>
             * </labelList>
             * <labelList>
                 * <patientid>1000005031</patientid>
                 * <labelType></labelType>
                 * <labelId>999</labelId>
                 * <labelContent>测试标签</labelContent>
                 * <memo></memo>
             * </labelList>
             * </res>
             */
            #endregion
            clearContent();

            string reqFormat = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><req><patientId>{0}</patientId></req>";
            //string patientId = "1000005031";
            string patientId = getCrmIdByHisId(hisno);
            this.hisId = hisno;

            //积分模块是否启用控制参数
            //未启用时正常返回，不影响正常流程
            bool IsCouponModuleInUse = ctlMgr.QueryControlerInfo("CP0001") == "1";

            if (!IsCouponModuleInUse)
            {
                this.Visible = false;
                return null;
            }

            bool IsLabelModuleInUse = ctlMgr.QueryControlerInfo("CP0005") == "1";

            if (!IsLabelModuleInUse)
            {
                this.Visible = false;
                return null;
            }

            if (patientId == null || patientId == "" || patientId == "-1")
            {
                //MessageBox.Show("该患者未绑定crm系统，无法使用添加标签功能!", "ERROR");
                this.Visible = false;
                return null;
            }
            else
            {
                this.Visible = true;
            }

            string req = string.Format(reqFormat, patientId);

            string url = getHisServerUrl();
            //string resultXml = FS.HISFC.BizProcess.Integrate.WSHelper.InvokeWebService(url, "getCrmLabel", new string[] { req }) as string;
            //{91AB66D4-38D1-448f-B2AF-FA9D1F114A67}
            FS.HISFC.BizProcess.Integrate.IbornMobileService server = new FS.HISFC.BizProcess.Integrate.IbornMobileService();

            server.Url = url;
            string resultXml = server.getCrmLabel(req);

            //解析报文成为detail的标签list
            currentLabelList = getPatientLabelByXML(resultXml);

            //解析标签列表
            System.Xml.XmlDocument document = new System.Xml.XmlDocument();
            document.LoadXml(resultXml);
            System.Xml.XmlNodeList nodes = document.SelectNodes("/res/labelList");
            System.Xml.XmlNode ispackage = document.SelectSingleNode("/res/isPackage");
            if (ispackage.InnerText == "1")
                package.Visible = true;
            else
                package.Visible = false;

            //封装label标签list
            List<PatientLabel> labelList = new List<PatientLabel>();

            foreach (System.Xml.XmlNode node in nodes)
            {
                string patientId2 = node.SelectSingleNode("patientid").InnerText;
                string labelType = node.SelectSingleNode("labelType").InnerText;
                string labelId = node.SelectSingleNode("labelId").InnerText;
                string labelContent = node.SelectSingleNode("labelContent").InnerText;
                string memo = node.SelectSingleNode("memo").InnerText;

                PatientLabel label = new PatientLabel();
                label.PatientId = patientId;
                label.labelType = labelType;
                label.LabelId = labelId;
                label.LabelContent = labelContent;
                label.Memo = memo;
                labelList.Add(label);

                this.crmId = patientId;
            }

            //初始化标签类别数量
            int bodynum = 0;
            int backgroundnum = 0;
            int stylenum = 0;
            int interestnum = 0;

            foreach (PatientLabel label in labelList)
            {
                switch (label.LabelType)
                {
                    case "LABELTYPE1":
                        shenti.Text += label.LabelContent + "\n";
                        bodynum++;
                        break;
                    case "LABELTYPE2":
                        beijing.Text += label.LabelContent + "\n";
                        backgroundnum++;
                        break;
                    case "LABELTYPE3":
                        gexing.Text += label.LabelContent + "\n";
                        stylenum++;
                        break;
                    case "LABELTYPE4":
                        xingqu.Text += label.LabelContent + "\n";
                        interestnum++;
                        break;
                    default:
                        shenti.Text += label.LabelContent + "\n";
                        bodynum++;
                        break;
                }
            }

            body.Text += bodynum;
            background.Text += backgroundnum;
            style.Text += stylenum;
            interest.Text += interestnum;

            //初始化套餐可见性
            string isPackage = document.SelectSingleNode("/res/isPackage").InnerText;
            if (isPackage == "1")
            {
                package.Visible = true;
            }

            //初始化投诉可见性
            string isComplain = document.SelectSingleNode("/res/complain").InnerText;
            if (isComplain == "1")
            {
                complain.Visible = true;
                foreach (FS.HISFC.Models.Label.PatientLabel patientlabel in currentLabelList)
                {
                    if (patientlabel.LabelType2 == "LABELTYPE31" || patientlabel.LabelType2 == "LABELTYPE32")
                    {
                        //{929E868E-3D45-4a06-AB38-722E309BD3C2}
                        this.tousuTipTip += patientlabel.LabelContent + " " + patientlabel.LabelMemo + "\n";
                    }
                }
            }

            //初始化会员可见性
            string isLevel = document.SelectSingleNode("/res/level").InnerText;
            if (isLevel != "0")
            {
                member.Visible = true;
                //switch (isLevel)
                //{
                //    case "1":
                //        huiyuanTipText = "银牌会员";
                //        break;
                //    case "2":
                //        huiyuanTipText = "金牌会员";
                //        break;
                //    case "3":
                //        huiyuanTipText = "钻石会员";
                //        break;
                //}
                huiyuanTipText = "会员等级lv" + isLevel + "\n" + document.SelectSingleNode("/res/levelName").InnerText;
            }


            //初始化生日可见性
            string resBirthday = document.SelectSingleNode("/res/birthday").InnerText;
            DateTime dt = DateTime.ParseExact(resBirthday, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            int day = dt.Day;
            //今年的生日 闰年平年2月天数不一样 {cc48c8d9-1ded-4fe5-9934-9ab7e6eaa7a5} 
            if (dt.Month == 2)
            {
                if (day == 29 || day == 28)
                {
                    day = DateTime.DaysInMonth(DateTime.Now.Year, dt.Month);
                }
            }
            DateTime d = new DateTime(DateTime.Now.Year, dt.Month, day);

            birthdayTipText = d.ToString("yyyy年MM月dd日");
            string birthdayTip = "";
            string tousuTipTip = "";


            //TimeSpan ts = DateTime.Now.Subtract(dt);
            //今年生日-今天=差几天生日，差3天生日就生日提醒
            TimeSpan ts = d.Subtract(DateTime.Now);
            double days = ts.TotalDays;
            if (days < 3 && days >= 0)
            {
                birthday.Visible = true;
            }

            //初始化套餐信息
            this.packagelist = getPackageByCardNO(hisId);

            //代收
            this.dspackagelist = getDsPackageByCardNO(hisId);

            if (dspackagelist != null && dspackagelist.Count > 0)
            {
                era.Visible = true;
            }

            bool isweek = getIndateByCardNO(hisId);
            week.Visible = isweek;
           

            return null;
        }

        //清空标签内容
        private void clearContent()
        {
            //>>>>>>>>>>>>>>>>>>>>>>>>初始化时清空label信息
            body.Text = "身体";
            background.Text = "背景";
            style.Text = "个性";
            interest.Text = "兴趣";

            shenti.Text = "";
            beijing.Text = "";
            gexing.Text = "";
            xingqu.Text = "";
            //addBox.Text = "";

            package.Visible = false;
            member.Visible = false;
            birthday.Visible = false;
            //allergy.Visible = false;
            complain.Visible = false;
            era.Visible = false;
            week.Visible = false;
            birthdayTipText = "";
            tousuTipTip = "";
            huiyuanTipText = "";
            //<<<<<<<<<<<<<<<<<<<<<<<初始化label信息
        }

        //气泡信息列表
        private void shentiEnter(object sender, EventArgs e)
        {
            // 设置显示样式
            //toolTip1.AutoPopDelay = 5000;//提示信息的可见时间
            toolTip1.InitialDelay = 200;//事件触发多久后出现提示
            toolTip1.ReshowDelay = 500;//指针从一个控件移向另一个控件时，经过多久才会显示下一个提示框
            toolTip1.ShowAlways = true;//是否显示提示框
            //toolTip1.UseAnimation = true;   //动画效果
            //toolTip1.UseFading = true;      //淡入淡出效果
            toolTip1.IsBalloon = true;      //气球状外观
            //toolTip1.OwnerDraw = true;//由自己绘制
            //toolTip1.Draw += new DrawToolTipEventHandler(toolTip1_Draw);


            //  设置伴随的对象.
            toolTip1.SetToolTip(this.body, shenti.Text == "" ? "无标签" : shenti.Text);
            toolTip1.SetToolTip(this.background, beijing.Text == "" ? "无标签" : beijing.Text);
            toolTip1.SetToolTip(this.style, gexing.Text == "" ? "无标签" : gexing.Text);
            toolTip1.SetToolTip(this.interest, xingqu.Text == "" ? "无标签" : xingqu.Text);
        }

        /*
        private void addButton_Click(object sender, EventArgs e)
        {
            if (crmId == null || crmId == "")
            {
                MessageBox.Show("该患者未绑定crm系统，无法绑卡!", "ERROR");
                return;
            }


            //点击时获取当前操作编号
            HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;
            HISFC.Models.Base.Department dept = employee.Dept as HISFC.Models.Base.Department;

            string employeeId = employee.ID;
            string deptId = dept.ID;
            string employeeName = employee.Name;


            string req = @"<req>
                            <patientLabelId>{0}</patientLabelId>
                            <patientId>{1}</patientId>
                            <labelType>{2}</labelType>
                            <labelId>{3}</labelId>
                            <labelSpecies>{4}</labelSpecies>
                            <labelContent>{5}</labelContent>
                            <memo>{6}</memo>
                            <extFiled1>{7}</extFiled1>
                            <extFiled2>{8}</extFiled2>
                            <extFiled3>{9}</extFiled3>
                            <labelState>{10}</labelState>
                            <operDept>{11}</operDept>
                            <operCode>{12}</operCode>
                            <operName>{13}</operName>
                            <operTime>{14}</operTime>
                        </req>";
            string patientLabelId = "";
            string patientId = crmId;
            string labelType = "LABELTYPE1";
            string labelId = "999";
            string labelSpecies = "LABELTYPE11";
            //string labelContent = addBox.Text;
            string memo = "";
            string extFiled1 = "0";
            string extFiled2 = "0";
            string extFiled3 = "";
            string labelState = "1";
            string operDept = deptId;
            string operCode = employeeId;
            string operName = employeeName;
            string operTime = "";

            string request = string.Format(req, patientLabelId, patientId, labelType, labelId, labelSpecies, labelContent,
                memo, extFiled1, extFiled2, extFiled3, labelState, operDept, operCode, operName, operTime);

            string url = getHisServerUrl();
            string resultXml = FS.HISFC.BizProcess.Integrate.WSHelper.InvokeWebService(url, "postCrmLabel", new string[] { request }) as string;

            System.Xml.XmlDocument document = new System.Xml.XmlDocument();
            document.LoadXml(resultXml);
            System.Xml.XmlNode node = document.SelectSingleNode("/res/resultCode");
            if (node.InnerText == "0")
            {
                MessageBox.Show("添加标签成功!", "SUCCESS");
                getUserLabelByHisCardNo(hisId);
            }else
            {
                MessageBox.Show("添加标签失败!", "ERROR");
            }
        }
         * */

        private void body_Click(object sender, EventArgs e)
        {
            FS.HISFC.Components.Common.Forms.frmAddLabel frmAddLabel = new FS.HISFC.Components.Common.Forms.frmAddLabel("LABELTYPE1", crmId, hisurl, currentLabelList);
            frmAddLabel.StartPosition = FormStartPosition.CenterScreen;
            frmAddLabel.ShowDialog();
            getUserLabelByHisCardNo(hisId);
        }

        private void background_Click(object sender, EventArgs e)
        {
            FS.HISFC.Components.Common.Forms.frmAddLabel frmAddLabel = new FS.HISFC.Components.Common.Forms.frmAddLabel("LABELTYPE2", crmId, hisurl, currentLabelList);
            frmAddLabel.StartPosition = FormStartPosition.CenterScreen;
            frmAddLabel.ShowDialog();
            getUserLabelByHisCardNo(hisId);
        }

        private void style_Click(object sender, EventArgs e)
        {
            FS.HISFC.Components.Common.Forms.frmAddLabel frmAddLabel = new FS.HISFC.Components.Common.Forms.frmAddLabel("LABELTYPE3", crmId, hisurl, currentLabelList);
            frmAddLabel.StartPosition = FormStartPosition.CenterScreen;
            frmAddLabel.ShowDialog();
            getUserLabelByHisCardNo(hisId);
        }

        private void interest_Click(object sender, EventArgs e)
        {
            FS.HISFC.Components.Common.Forms.frmAddLabel frmAddLabel = new FS.HISFC.Components.Common.Forms.frmAddLabel("LABELTYPE4", crmId, hisurl, currentLabelList);
            frmAddLabel.StartPosition = FormStartPosition.CenterScreen;
            frmAddLabel.ShowDialog();
            getUserLabelByHisCardNo(hisId);
        }

        private void taocanenter(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 200;//事件触发多久后出现提示
            toolTip1.ReshowDelay = 500;//指针从一个控件移向另一个控件时，经过多久才会显示下一个提示框
            toolTip1.ShowAlways = true;//是否显示提示框
            toolTip1.IsBalloon = true;      //气球状外观

            string taocantips = "";
           
            foreach (string name in packagelist)
            {
                taocantips += name + "\n";
            }

            //  设置伴随的对象.
            taocanTip.SetToolTip(this.package, taocantips);
        }

        private void tousuenter(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 200;//事件触发多久后出现提示
            toolTip1.ReshowDelay = 500;//指针从一个控件移向另一个控件时，经过多久才会显示下一个提示框
            toolTip1.ShowAlways = true;//是否显示提示框
            toolTip1.IsBalloon = true;      //气球状外观

            //  设置伴随的对象.
            taocanTip.SetToolTip(this.complain, "该客户有投诉记录" + "\n" + tousuTipTip);
        }

        private void huiyuanenter(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 200;//事件触发多久后出现提示
            toolTip1.ReshowDelay = 500;//指针从一个控件移向另一个控件时，经过多久才会显示下一个提示框
            toolTip1.ShowAlways = true;//是否显示提示框
            toolTip1.IsBalloon = true;      //气球状外观

            //  设置伴随的对象.
            taocanTip.SetToolTip(this.member, huiyuanTipText);
        }

        private void shengrienter(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 200;//事件触发多久后出现提示
            toolTip1.ReshowDelay = 500;//指针从一个控件移向另一个控件时，经过多久才会显示下一个提示框
            toolTip1.ShowAlways = true;//是否显示提示框
            toolTip1.IsBalloon = true;      //气球状外观

            //  设置伴随的对象.
            taocanTip.SetToolTip(this.birthday, "该客户马上就要生日了" + birthdayTipText);
        }

        //自定义tip
        /*
        void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            throw new NotImplementedException();
            //e.Font = new Font("Microsoft Sans Serif", 8.25f);
            e.DrawBackground();
            Font f = new System.Drawing.Font("黑体", 100, FontStyle.Bold);

            e.Graphics.DrawString(e.ToolTipText, f, Brushes.Black, new PointF(0, 0));
        }*/

        private List<FS.HISFC.Models.Label.PatientLabel> getPatientLabelByXML(string xml)
        {
            List<FS.HISFC.Models.Label.PatientLabel> labels = new List<FS.HISFC.Models.Label.PatientLabel>();

            //解析标签列表
            System.Xml.XmlDocument document = new System.Xml.XmlDocument();
            document.LoadXml(xml);
            System.Xml.XmlNodeList nodes = document.SelectNodes("/res/labelList");

            //封装label标签list

            foreach (System.Xml.XmlNode node in nodes)
            {
                string patientId = node.SelectSingleNode("patientid").InnerText;
                string labelType = node.SelectSingleNode("labelType").InnerText;
                string labelId = node.SelectSingleNode("labelId").InnerText;
                string labelContent = node.SelectSingleNode("labelContent").InnerText;
                string memo = node.SelectSingleNode("memo").InnerText;
                string patientLabel_id = node.SelectSingleNode("patientLabel_id").InnerText;
                string oper_time = node.SelectSingleNode("oper_time").InnerText;
                string oper_name = node.SelectSingleNode("oper_name").InnerText;
                string oper_code = node.SelectSingleNode("oper_code").InnerText;
                string label_state = node.SelectSingleNode("label_state").InnerText;
                string label_species = node.SelectSingleNode("label_species").InnerText;

                FS.HISFC.Models.Label.PatientLabel label = new FS.HISFC.Models.Label.PatientLabel();
                label.PatientId = patientId;
                label.LabelType1 = labelType;
                label.LabelId = labelId;
                label.LabelContent = labelContent;
                label.LabelMemo = memo;
                label.PatientLabelId = patientLabel_id;
                label.LabelCreatetime = oper_time;
                label.CreaterName = oper_name;
                label.CreaterCode = oper_code;
                label.LabelStatus = label_state;
                label.LabelType2 = label_species;


                labels.Add(label);

                this.crmId = patientId;
            }
            return labels;
        }

        private void era_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 200;//事件触发多久后出现提示
            toolTip1.ReshowDelay = 500;//指针从一个控件移向另一个控件时，经过多久才会显示下一个提示框
            toolTip1.ShowAlways = true;//是否显示提示框
            toolTip1.IsBalloon = true;      //气球状外观

            string taocantips = "";
            foreach (string name in dspackagelist)
            {
                taocantips += name + "\n";
            }

            //  设置伴随的对象.
            taocanTip.SetToolTip(this.era, taocantips);
        }

        private void week_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 200;//事件触发多久后出现提示
            toolTip1.ReshowDelay = 500;//指针从一个控件移向另一个控件时，经过多久才会显示下一个提示框
            toolTip1.ShowAlways = true;//是否显示提示框
            toolTip1.IsBalloon = true;      //气球状外观

            string taocantips = "存在七天内再次入院";

            //  设置伴随的对象.
            taocanTip.SetToolTip(this.week, taocantips);
        }

    }

    public class PatientLabel
    {
        // "<patientid>{0}</patientid><labelType>{1}</labelType><labelId>{2}</labelId><labelContent>{3}</labelContent><memo>{4}</memo>";

        public string patientId;
        public string labelType;
        public string labelId;
        public string labelContent;
        public string memo;

        public string PatientId
        {
            get
            {
                return this.patientId;
            }
            set
            {
                this.patientId = value;
            }
        }

        public string LabelType
        {
            get
            {
                return this.labelType;
            }
            set
            {
                this.labelType = value;
            }
        }

        public string LabelId
        {
            get
            {
                return this.labelId;
            }
            set
            {
                this.labelId = value;
            }
        }

        public string LabelContent
        {
            get
            {
                return this.labelContent;
            }
            set
            {
                this.labelContent = value;
            }
        }

        public string Memo
        {
            get
            {
                return this.memo;
            }
            set
            {
                this.memo = value;
            }
        }
    }
}
