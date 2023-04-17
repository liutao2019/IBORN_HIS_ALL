using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace FS.HISFC.Components.OutpatientFee.Controls
{
    public partial class ucCardInfo : UserControl
    {
        public ucCardInfo()
        {
            InitializeComponent();
        }
        ///<summary>
        ///恢复html中的特殊字符{6ABA909B-8693-46d5-B636-8C30797BAE8E}
        ///</summary>
        ///<paramname="theString">需要恢复的文本。</param>
        ///<returns>恢复好的文本。</returns>
        private string HtmlDiscode(string theString)
        {
            //{7EE78666-6558-4321-990F-4BD1CB2EFA2D}
            theString = NoHTML(theString);

            theString = theString.Replace("&gt;", ">");
            theString = theString.Replace("&lt;", "<");
            theString = theString.Replace("&nbsp;", " ");
            theString = theString.Replace("&quot;", "\"");
            theString = theString.Replace("&#39;", "\'");
            theString = theString.Replace("<br/>", "\n");
            theString = theString.Replace("&middot;", ".");

            //
            //{6950D976-2FA6-408f-AAFC-902DBD3CA238}添加卡券，电话号码
            theString = theString.Replace("&amp;", "&");
            theString = theString.Replace("&nbsp;", " ");



            return theString;
        }
        //{7EE78666-6558-4321-990F-4BD1CB2EFA2D}
        public string NoHTML(string Htmlstring)
        {
            //删除脚本  
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML  
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([/r/n])[/s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "/", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "/xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "/xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "/xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "/xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(/d+);", "", RegexOptions.IgnoreCase);
            //替换掉 < 和 > 标记
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("/r/n", "");

            Htmlstring.Replace("'", "(quot)");
            //返回去掉html标记的字符串
            return Htmlstring;
        }
        public void setTicketInfo(FS.HISFC.Models.Ticker.CardTicker ticketInfo)
        {
            this.clearTicketInfo();
            this.lbTicketName.Text = "卡券名称:" + ticketInfo.Title;
            this.lbShareWay.Text = "分享形式:" + ticketInfo.ShareType + "[0-不可分享，1-可裂变，2-邀请]";
            this.lbTicketType.Text = "卡券类型:" + ticketInfo.Type;
            this.lbGetPerson.Text = "领取人:" + ticketInfo.ReceivePersonName;
            this.lbTicketChannel.Text = "卡券渠道:" + ticketInfo.ChannelName;
            this.lbStartTime.Text = "开始时间:" + ticketInfo.StartTime.ToString();
            this.lbEndTime.Text = "结束时间:" + ticketInfo.EndTime.ToString();
            this.lbLowestCost.Text = "最低消费:" + ticketInfo.ConditionMoney.ToString();
            this.lbTicketValue.Text = "卡券金额:" + ticketInfo.CardMoney.ToString();
            this.lbTicketDisccount.Text = "卡券折扣:" + ticketInfo.Discount;
            this.lbGiftName.Text = "礼品名称:" + ticketInfo.GiftName;
            this.lbIsIncludeRegFee.Text = "是否包含挂号费:" + ticketInfo.RegisFee + "[0-不包含，1-包含]";
            this.lbUseArea.Text = "使用院区:" + ticketInfo.HospitalArea;
            this.lbState.Text = "使用状态:" + ticketInfo.CouponState + "[0-未使用，1-已使用，2-不可使用]";
            this.lbCreateTime.Text = "创建时间:" + ticketInfo.CreateTime.ToString();
            this.lbTicketContent.Text = "卡券详情:" + HtmlDiscode(ticketInfo.Content);
            this.lbTicketContent.Tag = "卡券详情:" + ticketInfo.Content;

            //{6950D976-2FA6-408f-AAFC-902DBD3CA238}添加卡券，电话号码
            this.lbcard.Text = "卡券号:" + ticketInfo.ID;
            this.lbphone.Text = "领用人电话号码:" + ticketInfo.ReceiveMobile;
            //{7EE78666-6558-4321-990F-4BD1CB2EFA2D}
            this.lbTicketChannel.Text = TextNoHTML(this.lbTicketChannel.Text);

            //this.lbTicketContent.Text = this.lbTicketContent.Text.Replace("<p>", "\n").Replace("</p>", "").Replace("</span>", "").Replace("<span style=\"font-size: 13.3333px;\">", "").Replace("<p style=\"font-size: 13.3333px;\">", "");

        }
        private void clearTicketInfo()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Label)
                {
                    ((Label)control).Text = "";
                }
            }

          
        }
        //{6950D976-2FA6-408f-AAFC-902DBD3CA238}添加卡券，电话号码
        public string TextNoHTML(string Htmlstring)
        {
            //删除脚本   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([/r/n])[/s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "/", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "/xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "/xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "/xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "/xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(/d+);", "", RegexOptions.IgnoreCase);
            //替换掉 < 和 > 标记
            Htmlstring = Htmlstring.Replace("<", "");
            Htmlstring = Htmlstring.Replace(">", "");
            Htmlstring = Htmlstring.Replace("\r\n", "");
            Htmlstring = Htmlstring.Replace("\r", "");
            Htmlstring = Htmlstring.Replace("\n", "");

            //返回去掉html标记的字符串
            return Htmlstring;
        }

    }
}
