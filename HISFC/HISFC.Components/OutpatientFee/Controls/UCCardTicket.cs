using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Web.Script.Serialization;
using FS.HISFC.Models.Ticker;
using FS.FrameWork.Management;
using GZ.Components.Bespeak;
using System.Collections;
using FS.HISFC.Models.RADT;
using System.Text.RegularExpressions;

namespace FS.HISFC.Components.OutpatientFee.Controls
{
    /// <summary>
    /// 卡券核销
    /// </summary>
    public partial class UCCardTicket : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private static string Adert = "";

        /// <summary>
        /// 患者基本信息
        /// </summary>
        private  FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        /// <summary>
        /// 患者基本信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get { return this.patientInfo; }
            set
            {
                this.patientInfo = value;
                this.SetPatientInfo();
                
            }
        }

        private CardTicker ticketInfo = null;//{6ABA909B-8693-46d5-B636-8C30797BAE8E}

        CardTicketNew ticketInfonew = null;//{6ABA909B-8693-46d5-B636-8C30797BAE8E}
        public CardTicker TicketInfo
        {
            get { return ticketInfo; }
            set 
            { 
                ticketInfo = value;
                this.setTicketInfo(ticketInfo);
            }
        }

        /// <summary>
        /// 当前院区
        /// </summary>
        private string hospitalInfo = string.Empty;

        /// <summary>
        /// 加密key
        /// </summary>
        private string encryptKey = string.Empty;

        /// <summary>
        /// web服务地址
        /// </summary>
        private string url = string.Empty;

        /// <summary>
        /// 业务层
        /// </summary>
        HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        public UCCardTicket()
        {
            InitializeComponent();
            initAESEncryptInfo();
        }

        private int initAESEncryptInfo()
        {
            try
            {
                HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;
                HISFC.Models.Base.Department dept = employee.Dept as HISFC.Models.Base.Department;

                FS.FrameWork.Models.NeuObject obj = managerIntegrate.GetConstansObj("MMWYTICKETKEY", dept.HospitalID);
                this.hospitalInfo = obj.Name;
                this.encryptKey = obj.Memo;
                
                FS.FrameWork.Models.NeuObject obj1 = managerIntegrate.GetConstansObj("MMWYTICKETURL", dept.HospitalID);
                this.url = obj1.Memo;

                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
                
        /// <summary>
        /// 工具栏
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        
        
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolbarService.AddToolButton("清屏", "清屏", FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            return toolbarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "清屏":
                    {
                        this.clearPatientInfo();
                        this.clearTicketInfo();
                        this.txtTicketNO.Focus();
                        break;
                    }
            }
        }

        private void btnWriteOff_Click(object sender, EventArgs e)
        {
            if (txtTicketNO.Text.Length > 15) //{6ABA909B-8693-46d5-B636-8C30797BAE8E}
            {
                if (PatientInfo == null || string.IsNullOrEmpty(PatientInfo.PID.CardNO))
                {
                    MessageBox.Show("患者信息为空！请先查找患者信息");
                    return;
                }
                if (this.ticketInfonew == null || string.IsNullOrEmpty(ticketInfonew.Id))
                {
                    MessageBox.Show("卡券信息为空");
                    return;
                }
                TickerServices.CardTicketService cs = new FS.HISFC.Components.OutpatientFee.TickerServices.CardTicketService();
                cs.Url = this.url;


                string result = cs.ExchangeNewCard(ticketInfonew.Id, Connection.Operator.Name + Connection.Operator.ID);
                System.Xml.XmlDocument document = new System.Xml.XmlDocument();
                document.LoadXml(result);
                System.Xml.XmlNode resultCodeNode = document.SelectSingleNode("/res/resultCode");
                if (resultCodeNode != null && resultCodeNode.InnerText == "0")
                {
                    //核销成功
                    TickerWriteOffRecord record = new TickerWriteOffRecord();
                    record.TickerNO = ticketInfonew.Id;
                    record.Card_NO = PatientInfo.PID.CardNO;
                    record.ConSutype = "门诊消费";
                    record.Oper_ID = Connection.Operator.ID;
                    record.Oper_Name = Connection.Operator.Name;
                    //{E40946A4-FEB0-4842-BEAB-472BD85F1829}
                    record.Title = ticketInfonew.Title;
                    record.Tcontent = HtmlDiscode(ticketInfonew.UseLimits);
                    record.ReceivePersonName = ticketInfonew.PatientName;
                    FS.HISFC.BizLogic.Fee.CardTickerBLL bll = new FS.HISFC.BizLogic.Fee.CardTickerBLL();
                    if (bll.Insert(record) > 0)
                    {
                        MessageBox.Show("卡券核销成功！");
                        this.txtTicketNO.Text = "";
                        this.clearTicketInfo();
                        this.clearPatientInfo();
                        this.txtTicketNO.Focus();
                        this.panel.Controls.Clear();
                    }
                    else
                    {
                        record.Tcontent = "";
                        bll.Insert(record);
                    }
                }
                else
                {
                    //{6950D976-2FA6-408f-AAFC-902DBD3CA238}核销日志
                    FS.HISFC.Components.OutpatientFee.Class.LogManager.Write("webservices核销失败" + result + DateTime.Now.ToString());
                    MessageBox.Show(result);
                }
            }
            else
            {
                try
                {
                    if (PatientInfo == null || string.IsNullOrEmpty(PatientInfo.PID.CardNO))
                    {
                        MessageBox.Show("患者信息为空！请先查找患者信息");
                        return;
                    }

                    if (this.TicketInfo == null || string.IsNullOrEmpty(ticketInfo.ID))
                    {
                        MessageBox.Show("卡券信息为空");
                        return;
                    }

                    if (!string.IsNullOrEmpty(ticketInfo.ReceiveCode) && ticketInfo.ReceiveCode != PatientInfo.CrmID)
                    {
                        if (MessageBox.Show("当前患者信息与卡券信息领取人不符合，是否继续", "提醒", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                        {
                            return;
                        }
                    }

                    if (string.IsNullOrEmpty(this.hospitalInfo) || string.IsNullOrEmpty(this.encryptKey))
                    {
                        MessageBox.Show("获取院区信息与加密码出错");
                        return;
                    }

                    string jmticker = AESEncrypt(ticketInfo.ID, this.encryptKey);
                    string data = "{\"channel\":\"" + this.hospitalInfo + "\",\"couponCode\":\"" + jmticker + "\"}";
                    TickerServices.CardTicketService cs = new FS.HISFC.Components.OutpatientFee.TickerServices.CardTicketService();
                    cs.Url = this.url;
                    string result = cs.QueryCard(data);
                    JavaScriptSerializer json = new JavaScriptSerializer();
                    ReturnData<CardTicker> rt = json.Deserialize<ReturnData<CardTicker>>(result);
                    if (rt.Status == "0")
                    {
                        CardTicker model = rt.Data;
                        string consuMoney = AESEncrypt(model.CardMoney.ToString(), this.encryptKey);
                        string writeOffMoney = AESEncrypt(model.CardMoney.ToString(), this.encryptKey);
                        string operName = AESEncrypt(Connection.Operator.Name, this.encryptKey);
                        string consuType = AESEncrypt("门诊消费", this.encryptKey);
                        string hdata = "{\"channel\":\"" + this.hospitalInfo + "\",\"couponCode\":\"" + jmticker + "\",\"consuMoney\":\"" + consuMoney + "\",\"writeOffMoney\":\"" + writeOffMoney + "\",\"operator\":\"" + operName + "\",\"consuType\":\"" + consuType + "\"}";
                        result = cs.ExchangeCard(hdata);
                        ReturnData<CardTicker> rt1 = json.Deserialize<ReturnData<CardTicker>>(result);
                        if (rt1.Status == "0")
                        {
                            TickerWriteOffRecord record = new TickerWriteOffRecord();
                            record.TickerNO = ticketInfo.ID;
                            record.Card_NO = PatientInfo.PID.CardNO;
                            record.ConSutype = "门诊消费";
                            record.Oper_ID = Connection.Operator.ID;
                            record.Oper_Name = Connection.Operator.Name;
                            //{E40946A4-FEB0-4842-BEAB-472BD85F1829}
                            record.Title = ticketInfo.Title;
                            record.Tcontent = HtmlDiscode(ticketInfo.Content);// lbTicketContent.Text.ToString(); //{7EE78666-6558-4321-990F-4BD1CB2EFA2D}
                            record.ReceivePersonName = ticketInfo.ReceivePersonName;
                            FS.HISFC.BizLogic.Fee.CardTickerBLL bll = new FS.HISFC.BizLogic.Fee.CardTickerBLL();
                            if (bll.Insert(record) > 0)
                            {
                                MessageBox.Show("卡券核销成功！");

                                this.clearTicketInfo();
                                this.clearPatientInfo();
                                this.txtTicketNO.Focus();
                                this.txtTicketNO.Text = "";
                            }
                            else
                            {
                                record.Tcontent = "";
                                bll.Insert(record);

                                ////{6950D976-2FA6-408f-AAFC-902DBD3CA238}核销日志
                                //FS.HISFC.Components.OutpatientFee.Class.LogManager.Write("本地数据库插入失败" + result + DateTime.Now.ToString());
                                //MessageBox.Show(bll.Err);
                            }
                        }
                        else
                        {
                            //{6950D976-2FA6-408f-AAFC-902DBD3CA238}核销日志
                            FS.HISFC.Components.OutpatientFee.Class.LogManager.Write("webservices核销失败" + result + DateTime.Now.ToString());
                            MessageBox.Show(rt1.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show(rt.Message);
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }

           

        }


        /// <summary>
        ///  根据条件查询人员信息 //{D7C6FBD0-6BE3-4e44-9DE5-6293AAE1037F} 修改综合查询
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private bool queryPatientList(string condition)
        {

            Form patientForm = new Form();
            ucPatientList patientList = new ucPatientList();
            patientForm.Size = patientList.Size;
            patientForm.Controls.Add(patientList);
            patientList.QueryCondition = condition;
            patientForm.StartPosition = FormStartPosition.Manual;
            patientForm.Location = new Point(PointToScreen(this.txtCardNO.Location).X, PointToScreen(this.txtCardNO.Location).Y + this.txtCardNO.Height * 2);
            patientForm.FormBorderStyle = FormBorderStyle.None;
            patientForm.ShowInTaskbar = false;
            patientList.patientInfo = patientInfoSet;

            if (patientList.patientList != null && patientList.patientList.Count > 0)
            {
                patientForm.ShowDialog();
                return true;
            }
            else
            {
                MessageBox.Show("没有该患者信息！");
                // tbName.Text = patient.Name;
            }

            return false;
        }

        /// <summary>
        ///根据选择的人员信息设置综合查询信息  //{D7C6FBD0-6BE3-4e44-9DE5-6293AAE1037F} 修改综合查询
        /// </summary>
        /// <param name="patient"></param>
        private void patientInfoSet(PatientInfo patient)
        {
            this.PatientInfo = patient;
            // SetPatientInfo();
        }

        /// <summary>
        /// 账户管理类
        /// </summary>
        FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();


        /// <summary>
        /// 性别
        /// </summary>
        private ArrayList sexList = null;

        /// <summary>
        /// 证件类型
        /// </summary>
        private ArrayList IDTypeList = null;

        /// <summary>
        /// 设置患者信息
        /// </summary>
        private void SetPatientInfo()
        {
            clearPatientInfo();
            if (this.PatientInfo == null || string.IsNullOrEmpty(this.PatientInfo.PID.CardNO))
            {
                this.tbMedicalNO.Text = string.Empty;
                this.tbName.Text = string.Empty;
                this.tbCardType.Text = string.Empty;
                this.tbIDNO.Text = string.Empty;
                this.tbSex.Text = string.Empty;
                this.tbAge.Text = string.Empty;
           
                this.tbPhone.Text = string.Empty;
              

                return;
            }


          //  System.Collections.Generic.List<FS.HISFC.Models.Account.AccountCard> cardList = accountMgr.GetMarkList(this.PatientInfo.PID.CardNO, "ALL", "1");
            //if (cardList == null || cardList.Count < 1)
            //{
            //    MessageBox.Show("病历号不存在！");
            //    return;
            //}
            //this.accountCardInfo = cardList[cardList.Count - 1];

            this.txtCardNO.Text = string.Empty;
            this.tbMedicalNO.Text = this.PatientInfo.PID.CardNO;
            this.tbName.Text = PatientInfo.Name;
            this.tbCardType.Text = this.QueryNameByIDFromDictionnary(this.IDTypeList, this.patientInfo.IDCardType.ID);
            this.tbIDNO.Text = this.patientInfo.IDCard;
            this.tbSex.Text = this.QueryNameByIDFromDictionnary(this.sexList, patientInfo.Sex.ID.ToString());
            this.tbAge.Text = this.accountMgr.GetAge(patientInfo.Birthday);
        
            this.tbPhone.Text = this.patientInfo.PhoneHome;
            
        }

        private void setTicketInfo(CardTicker ticketInfo)
        {
            this.clearTicketInfo();
            ucCardInfo uccard = new ucCardInfo();
            uccard.setTicketInfo(ticketInfo);
            this.panel.Controls.Clear();
            this.panel.Controls.Add(uccard);
            return;
            //  return;//{6ABA909B-8693-46d5-B636-8C30797BAE8E}

          //  this.lbTicketName.Text = "卡券名称:" + ticketInfo.Title;
          //  this.lbShareWay.Text = "分享形式:" + ticketInfo.ShareType + "[0-不可分享，1-可裂变，2-邀请]";
          //  this.lbTicketType.Text = "卡券类型:" + ticketInfo.Type;
          //  this.lbGetPerson.Text = "领取人:" + ticketInfo.ReceivePersonName;
          //  this.lbTicketChannel.Text = "卡券渠道:" + ticketInfo.ChannelName;
          //  this.lbStartTime.Text = "开始时间:" + ticketInfo.StartTime.ToString();
          //  this.lbEndTime.Text = "结束时间:" + ticketInfo.EndTime.ToString();
          //  this.lbLowestCost.Text = "最低消费:" + ticketInfo.ConditionMoney.ToString();
          //  this.lbTicketValue.Text = "卡券金额:" + ticketInfo.CardMoney.ToString();
          //  this.lbTicketDisccount.Text = "卡券折扣:" + ticketInfo.Discount;
          //  this.lbGiftName.Text = "礼品名称:" + ticketInfo.GiftName;
          //  this.lbIsIncludeRegFee.Text = "是否包含挂号费:" + ticketInfo.RegisFee + "[0-不包含，1-包含]";
          //  this.lbUseArea.Text = "使用院区:" + ticketInfo.HospitalArea;
          //  this.lbState.Text = "使用状态:" + ticketInfo.CouponState + "[0-未使用，1-已使用，2-不可使用]";
          //  this.lbCreateTime.Text = "创建时间:" + ticketInfo.CreateTime.ToString();
          //  this.lbTicketContent.Text = "卡券详情:" + HtmlDiscode(ticketInfo.Content);
          //  this.lbTicketContent.Tag = "卡券详情:" + ticketInfo.Content;

          //  //{6950D976-2FA6-408f-AAFC-902DBD3CA238}添加卡券，电话号码
          //  this.lbcard.Text = "卡券号:"+ticketInfo.ID ;
          //  this.lbphone.Text = "领用人电话号码:"+ticketInfo.ReceiveMobile;
          ////{7EE78666-6558-4321-990F-4BD1CB2EFA2D}
          //  this.lbTicketChannel.Text = TextNoHTML(this.lbTicketChannel.Text);

          //  //this.lbTicketContent.Text = this.lbTicketContent.Text.Replace("<p>", "\n").Replace("</p>", "").Replace("</span>", "").Replace("<span style=\"font-size: 13.3333px;\">", "").Replace("<p style=\"font-size: 13.3333px;\">", "");
            
        }

        private string QueryNameByIDFromDictionnary(ArrayList al, string ID)
        {
            try
            {
                foreach (FS.FrameWork.Models.NeuObject obj1 in al)
                {
                    if (obj1.ID == ID)
                    {
                        return obj1.Name;
                    }
                }
            }
            catch
            { }
            return string.Empty;
        }


        /// <summary>  
        /// AES加密(无向量)  
        /// </summary>  
        /// <param name="plainBytes">被加密的明文</param>  
        /// <param name="key">密钥</param>  
        /// <returns>密文</returns>  
        public string AESEncrypt(String Data, String Key)
        {
            MemoryStream mStream = new MemoryStream();
            RijndaelManaged aes = new RijndaelManaged();
            byte[] plainBytes = Encoding.UTF8.GetBytes(Data);
            Byte[] bKey = new Byte[16];
            Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(bKey.Length)), bKey, bKey.Length);
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            aes.KeySize = 128;
            aes.Key = bKey;
            CryptoStream cryptoStream = new CryptoStream(mStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
            try
            {
                cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                cryptoStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray(), 0, mStream.ToArray().Length);//mStream.ToArray();
            }
            finally
            {
                cryptoStream.Close();
                mStream.Close();
                aes.Clear();
            }
        }

        private void txtCardNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                queryPatientList(this.txtCardNO.Text);
            }
        }


        //{73D6B203-E34C-484b-98FE-9CE6519E0201}
        private void txtTicketNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.panel.Controls.Clear();
                //if (this.txtTicketNO.Text.Length > 15)
                //{
                    queryTicketInfonew(this.txtTicketNO.Text);
                //}
                //else
                //{
                //    queryTicketInfo(this.txtTicketNO.Text);
                //}
            }
        }


        private void queryTicketInfonew(string ticketNO)
        {
            this.clearTicketInfo();
            if (string.IsNullOrEmpty(ticketNO))//{6ABA909B-8693-46d5-B636-8C30797BAE8E}
            {
                return;
            }
            //HISFC.Components.OutpatientFee.WebReference.CardTicketService cs = new FS.HISFC.Components.OutpatientFee.WebReference.CardTicketService();
            //cs.Url ="http://192.168.35.42:8076/CardTicketService.asmx";
            TickerServices.CardTicketService cs = new FS.HISFC.Components.OutpatientFee.TickerServices.CardTicketService();
            cs.Url = this.url;
            string result = cs.QueryNewCard(ticketNO);
            System.Xml.XmlDocument document = new System.Xml.XmlDocument();
            document.LoadXml(result);
            System.Xml.XmlNode resultCodeNode = document.SelectSingleNode("/res/resultCode");
            if (resultCodeNode != null && resultCodeNode.InnerText == "0")
            {
                CardTicketNew card = new CardTicketNew();
                card.Id = ticketNO;
                System.Xml.XmlNode patientName = document.SelectSingleNode("/res/patientName");
                System.Xml.XmlNode title = document.SelectSingleNode("/res/title");
                System.Xml.XmlNode phone = document.SelectSingleNode("/res/phone");
                System.Xml.XmlNode gmtStart = document.SelectSingleNode("/res/gmtStart");
                System.Xml.XmlNode gmtExpiry = document.SelectSingleNode("/res/gmtExpiry");
                System.Xml.XmlNode limitArea = document.SelectSingleNode("/res/limitArea");
                System.Xml.XmlNode couponType = document.SelectSingleNode("/res/couponType");
                System.Xml.XmlNode couponBusiness = document.SelectSingleNode("/res/couponBusiness");
                System.Xml.XmlNode useLimits = document.SelectSingleNode("/res/useLimits");
                System.Xml.XmlNode advert = document.SelectSingleNode("/res/advert");
                System.Xml.XmlNode coverImg = document.SelectSingleNode("/res/coverImg");
                System.Xml.XmlNode writeOffMsgName = document.SelectSingleNode("/res/writeOffMsgName");
                System.Xml.XmlNode writeOffMsgPhone = document.SelectSingleNode("/res/writeOffMsgPhone");
                System.Xml.XmlNode gmtWriteOff = document.SelectSingleNode("/res/gmtWriteOff");

                card.PatientName =patientName==null?"": patientName.InnerText;
                card.Title = title == null ? "" : title.InnerText;
                card.Phone = phone == null ? "" : phone.InnerText;
                card.GmtStart = gmtStart == null ? "" : gmtStart.InnerText;
                card.GmtExpiry = gmtExpiry == null ? "" : gmtExpiry.InnerText;
                card.LimitArea = limitArea == null ? "" : limitArea.InnerText;
                card.CouponType = couponType == null ? "" : couponType.InnerText;
                card.UseLimits = useLimits == null ? "" : useLimits.InnerText;


                //{73D6B203-E34C-484b-98FE-9CE6519E0201}

                card.Advert = advert == null ? "" : advert.InnerText.Replace("&lt;p&gt;", "").Replace("&lt;/p&gt;", "").Replace("&middot;", "").Replace("&amp;nbsp;", "").Replace("&lt;br&gt;", "");


                card.WriteOffMsgName = writeOffMsgName == null ? "" : writeOffMsgName.InnerText;
                card.WriteOffMsgPhone = writeOffMsgPhone == null ? "" : writeOffMsgPhone.InnerText;
                card.GmtWriteOff = gmtWriteOff == null ? "" : gmtWriteOff.InnerText;
                ArrayList list = managerIntegrate.QueryComPatientInfoByphone(card.Phone);
             
                if (list.Count > 0)
                {
                    this.PatientInfo = list[0] as PatientInfo;
                }

                ucCardInfo2 uc2 = new ucCardInfo2();
                uc2.SetInfo(card);
                this.ticketInfonew = card;
                this.panel.Controls.Clear();
                this.panel.Controls.Add(uc2);
            }
            else
            {
                MessageBox.Show("从服务端返回的卡券信息转化错误！");
                this.panel.Controls.Clear();
            }
        }


        private void queryTicketInfo(string ticketNO)
        {
            this.clearTicketInfo();
            if (string.IsNullOrEmpty(ticketNO))
            {
                return;
            }

            if (string.IsNullOrEmpty(this.hospitalInfo) || string.IsNullOrEmpty(this.encryptKey))
            {
                MessageBox.Show("获取院区信息与加密码出错");
                return;
            }

            string jmticker = AESEncrypt(ticketNO, this.encryptKey);
            string data = "{\"channel\":\"" + this.hospitalInfo + "\",\"couponCode\":\"" + jmticker + "\"}";
            TickerServices.CardTicketService cs = new FS.HISFC.Components.OutpatientFee.TickerServices.CardTicketService();
            cs.Url = this.url;
            string result = cs.QueryCard(data);
            JavaScriptSerializer json = new JavaScriptSerializer();
            ReturnData<CardTicker> rt = json.Deserialize<ReturnData<CardTicker>>(result);

            if (rt != null && rt.Data != null)
            {
                this.TicketInfo = rt.Data;

                if (ticketInfo == null)
                {
                    MessageBox.Show("从服务端返回的卡券信息转化错误！");
                    this.clearTicketInfo();
                    return;
                }

                FS.HISFC.Models.RADT.PatientInfo patient = managerIntegrate.QueryComPatientInfoByCRMID(ticketInfo.ReceiveCode);
                if (patient == null || string.IsNullOrEmpty(patient.ID))
                {
                    MessageBox.Show("未查询到卡券所对应的患者信息，请手动检索患者信息！");
                }

                this.PatientInfo = patient;
            }
            else
            {
                MessageBox.Show(rt.Message);
            }
        }


        //{73D6B203-E34C-484b-98FE-9CE6519E0201}
        private void button1_Click(object sender, EventArgs e)
        {
            this.panel.Controls.Clear();
                //ucCardInfo2 uccard = new ucCardInfo2();
                //TickerServices.CardTicketService cs = new FS.HISFC.Components.OutpatientFee.TickerServices.CardTicketService();
                //cs.Url = this.url;
                //string result = cs.QueryNewCard(txtTicketNO.Text);
                //uccard.SetInfo(result);
                //this.panel.Controls.Clear();
                //this.panel.Controls.Add(uccard);{6ABA909B-8693-46d5-B636-8C30797BAE8E}
                queryTicketInfonew(this.txtTicketNO.Text);
  
        }
        //{7EE78666-6558-4321-990F-4BD1CB2EFA2D}
        public string NoHTML(string Htmlstring)
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
        ///<summary>
        ///恢复html中的特殊字符
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
            theString = theString.Replace( "&amp;","&");
            theString = theString.Replace("&nbsp;"," ");

             
          
            return theString;
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

        private void clearTicketInfo()
        {
            foreach (Control control in this.panel.Controls)
            {
                if (control is Label)
                {
                    ((Label)control).Text = "";
                }
            }

            // this.txtTicketNO.Text = "";{6ABA909B-8693-46d5-B636-8C30797BAE8E}
        }

        private void clearPatientInfo()
        {
            foreach (Control control in this.pnlTop.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Text = "";
                }
            }
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {

        }


        //{D9B4353D-B8EC-40b5-BE69-7BF2C97EA4CE}
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.txtTicketNO.Text != "")
            {
                //if (PatientInfo == null || string.IsNullOrEmpty(PatientInfo.PID.CardNO))
                //{
                //    MessageBox.Show("患者信息为空！请先查找患者信息");
                //    return;
                //}
                //if (this.ticketInfonew == null || string.IsNullOrEmpty(ticketInfonew.Id))
                //{
                //    MessageBox.Show("卡券信息为空");
                //    return;
                //}
                //HISFC.Components.OutpatientFee.WebReference1.WebService1 w = new FS.HISFC.Components.OutpatientFee.WebReference1.WebService1();
                //w.Url = this.url;

                //HISFC.Components.OutpatientFee.WebReference.CardTicketService cs = new FS.HISFC.Components.OutpatientFee.WebReference.CardTicketService();
                //cs.Url = "http://192.168.35.42:8076/CardTicketService.asmx";
                //string result = cs.ExchangeOldCard(this.txtTicketNO.Text);


                TickerServices.CardTicketService cs = new FS.HISFC.Components.OutpatientFee.TickerServices.CardTicketService();
                cs.Url = this.url;

                string result = cs.ExchangeOldCard(this.txtTicketNO.Text);

                System.Xml.XmlDocument document = new System.Xml.XmlDocument();
                document.LoadXml(result);
                System.Xml.XmlNode resultCodeNode = document.SelectSingleNode("/res/resultCode");
                if (resultCodeNode != null && resultCodeNode.InnerText == "0")
                {
                    //核销成功

                    //MessageBox.Show("卡券核销成功！");
                    //this.txtTicketNO.Text = "";
                    //this.clearTicketInfo();
                    //this.clearPatientInfo();
                    //this.txtTicketNO.Focus();
                    //this.panel.Controls.Clear();


                    // {92AA4986-2CBE-480b-B643-D75846648D5E}

                    //核销成功
                    TickerWriteOffRecord record = new TickerWriteOffRecord();
                    record.TickerNO = ticketInfonew.Id;
                    record.Card_NO = this.PatientInfo== null ? "-" : PatientInfo.PID.CardNO;
                    record.ConSutype = "门诊消费";
                    record.Oper_ID = Connection.Operator.ID;
                    record.Oper_Name = Connection.Operator.Name;
                   
                    record.Title = ticketInfonew.Title;
                    record.Tcontent = HtmlDiscode(ticketInfonew.Advert);
                    record.ReceivePersonName = ticketInfonew.PatientName;
                    FS.HISFC.BizLogic.Fee.CardTickerBLL bll = new FS.HISFC.BizLogic.Fee.CardTickerBLL();
                    if (bll.Insert(record) > 0)
                    {
                        MessageBox.Show("卡券核销成功！");
                        this.txtTicketNO.Text = "";
                        this.clearTicketInfo();
                        this.clearPatientInfo();
                        this.txtTicketNO.Focus();
                        this.panel.Controls.Clear();
                    }
                    else
                    {
                        record.Tcontent = "";
                        bll.Insert(record);
                    }



                }
                else
                {
                    //{6950D976-2FA6-408f-AAFC-902DBD3CA238}核销日志
                    FS.HISFC.Components.OutpatientFee.Class.LogManager.Write("webservices核销失败" + result + DateTime.Now.ToString());
                    MessageBox.Show("旧卡券核销失败");
                }
            }
            else
            {
                MessageBox.Show("卡券号不能为空");
            }
        }
    }
}
