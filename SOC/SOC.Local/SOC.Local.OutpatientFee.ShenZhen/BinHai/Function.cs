using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace FS.SOC.Local.OutpatientFee.ShenZhen.BinHai
{
    public class Function
    {
        #region 静态变量

        /// <summary>
        /// 控制参数帮助类
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper controlerHelper = new FS.FrameWork.Public.ObjectHelper();

        #endregion

        #region 分发票

        /// <summary>
        /// 分票处理, 不在事务内
        /// </summary>
        /// <param name="cost">当前金额</param>
        /// <returns>处理后得金额</returns>
        public static decimal DealCent(decimal cost)
        {
            return DealCent(cost, null);
        }
        /// <summary>
        /// 分票处理 在事务内
        /// </summary>
        /// <param name="cost">当前金额</param>
        /// <param name="t">数据库连接</param>
        /// <returns>处理后得金额</returns>
        public static decimal DealCent(decimal cost, FS.FrameWork.Management.Transaction t)
        {
            FS.FrameWork.Management.ControlParam myCtrl = new FS.FrameWork.Management.ControlParam();
            if (t != null)
            {
                myCtrl.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            }
            string conValue = "0";//myCtrl.QueryControlerInfo(FS.HISFC.BizProcess.Integrate.Const.CENTRULE);
            if (controlerHelper == null || controlerHelper.ArrayObject == null || controlerHelper.ArrayObject.Count <= 0)
            {
                conValue = myCtrl.QueryControlerInfo(FS.HISFC.BizProcess.Integrate.Const.CENTRULE);
            }
            else
            {
                FS.FrameWork.Models.NeuObject obj = controlerHelper.GetObjectFromID(FS.HISFC.BizProcess.Integrate.Const.CENTRULE);

                if (obj == null)
                {
                    conValue = myCtrl.QueryControlerInfo(FS.HISFC.BizProcess.Integrate.Const.CENTRULE);
                }
                else
                {
                    conValue = ((FS.HISFC.Models.Base.ControlParam)obj).ControlerValue;
                }
            }
            if (conValue == null || conValue == "" || conValue == "-1")
            {
                conValue = "0";//默认不处理
            }
            decimal dealedCost = 0;

            switch (conValue)
            {
                case "0": //不处理
                    dealedCost = cost;
                    break;
                case "1": //四舍五入
                    dealedCost = FS.FrameWork.Public.String.FormatNumber(cost, 1);
                    break;
                case "2": //上取整
                    dealedCost = cost * 10;
                    if (dealedCost != Decimal.Truncate(dealedCost))
                    {
                        dealedCost = Decimal.Truncate(dealedCost) + 1;
                    }
                    dealedCost = dealedCost / 10;
                    break;
                case "3": //下取整
                    dealedCost = cost * 10;
                    dealedCost = Decimal.Truncate(dealedCost) / 10;
                    break;

            }
            return dealedCost;
        }

        #endregion


        #region 获取门诊收费日结管理类

        public static FS.SOC.HISFC.OutpatientFee.BizProcess.DayReport GetDayReportBizProcess()
        {
            return new FS.SOC.HISFC.OutpatientFee.BizProcess.DayReport();
        }

        #endregion

        /// <summary>
        /// 创建配置文件 feeSetting.xml 提供保存输入法
        /// </summary>
        /// <returns></returns>
        public static int CreateFeeSetting()
        {
            try
            {
                XmlDocument docXml = new XmlDocument();
                if (System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml"))
                {
                    System.IO.File.Delete(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
                }
                else
                {
                    System.IO.Directory.CreateDirectory(FS.FrameWork.WinForms.Classes.Function.SettingPath);
                }
                docXml.LoadXml("<setting>  </setting>");
                XmlNode root = docXml.DocumentElement;

                XmlElement elem1 = docXml.CreateElement("输入法");
                System.Xml.XmlComment xmlcomment;
                xmlcomment = docXml.CreateComment("查询方式0:拼音码 1:五笔码 2:自定义码 3:国标码 4:英文");
                elem1.SetAttribute("currentmodel", "0");
                root.AppendChild(xmlcomment);
                root.AppendChild(elem1);

                XmlElement elem2 = docXml.CreateElement("IME");
                System.Xml.XmlComment xmlcomment2;
                xmlcomment2 = docXml.CreateComment("当前默认输入法");
                elem2.SetAttribute("currentmodel", "紫光拼音输入法");
                root.AppendChild(xmlcomment2);
                root.AppendChild(elem2);

                docXml.Save(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 1;
        }
    }
}
