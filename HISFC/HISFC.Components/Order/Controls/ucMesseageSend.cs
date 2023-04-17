using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Order.Controls
{
    public partial class ucMesseageSend : FS.FrameWork.WinForms.Controls.ucBaseControl
    {//{2E47C0BD-CD18-4ce8-B244-02DCE3B30DB6}
        public ucMesseageSend()
        {
            InitializeComponent();
        }

        //public ucMesseageSend(FS.HISFC.Models.RADT.PatientInfo patient)
        //{
        //    InitializeComponent();
        //    this.patient = patient;
        //    Init();
        //}
        #region 变量
        FS.HISFC.Models.Order.Message msg = new FS.HISFC.Models.Order.Message();
        FS.HISFC.BizLogic.Order.MessageLogic mgslogic = new FS.HISFC.BizLogic.Order.MessageLogic();
        public FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();
        FS.HISFC.BizLogic.Order.MessageTemplateLogic templatelogic = new FS.HISFC.BizLogic.Order.MessageTemplateLogic(); //{D37652D3-1DB3-4f8c-AFE6-BE21625F082C}
        #endregion

        public void Init()
        {
            txtname.Text = patient.Name;
            txtcard.Text = patient.PID.CardNO.ToString();
            textphone.Text = patient.PhoneHome;
            System.Collections.ArrayList al = new System.Collections.ArrayList(); //{D37652D3-1DB3-4f8c-AFE6-BE21625F082C}
            al = templatelogic.QueryMsgTemplateAll();
            foreach (var o in al)
            {

                cbTemplate.Items.Add((o as FS.HISFC.Models.Order.MessageTemplate).MsgTemplateTitle);
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            MessageBoxButtons messButton = MessageBoxButtons.YesNo;
            DialogResult dr = MessageBox.Show("确定要发送吗?", "发送短信", messButton);
            if (dr == DialogResult.Yes)//如果点击“确定”按钮
            {
                bool rtn = false;
                FS.HISFC.Models.Order.Message msg = new FS.HISFC.Models.Order.Message();
                msg.Message_id = mgslogic.GetNewMessageID();
                msg.Card_NO = txtcard.Text;
                msg.Name = txtname.Text;
                msg.Content = txtExecBillName.Text;
                msg.OperCode = FS.FrameWork.Management.Connection.Operator.ID;
                msg.OperName = FS.FrameWork.Management.Connection.Operator.Name;
                msg.OperTime = DateTime.Now;
                msg.Phone = textphone.Text;
                if (textphone.Text == "")
                {
                    MessageBox.Show("找不到联系电话");
                    return;
                }
                string rtnMsg = string.Empty;
                rtnMsg = this.DoSendMsg(msg.Phone, msg.Content);
                //短信记录发送
                try
                {
                    Dictionary<string, string> resDit = FS.HISFC.Components.Order.Classes.JsonUntity.DeserializeStringToDictionary<string, string>(rtnMsg);
                    if (resDit["IsSuccess"].ToUpper() == "TRUE")
                    {
                        string sendresult_tmp = resDit["MainData"];
                        Newtonsoft.Json.Linq.JObject jobject = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(sendresult_tmp);
                        msg.ExtField01 = jobject["smsId"].ToString();
                        msg.ExtField02 = jobject["customSmsId"].ToString();
                        msg.SendResult = "发送成功：" + rtnMsg;
                        MessageBox.Show("发送成功");
                        if (this.ParentForm != null)
                        {
                            this.ParentForm.Close();
                        }
                        
                    }
                    else
                    {
                        msg.SendResult = "发送失败：" + resDit["Message"];
                        MessageBox.Show(msg.SendResult);
                    }
                }
                catch
                {
                    msg.SendResult = "发送失败！" + rtnMsg;
                    MessageBox.Show(msg.SendResult);
                }

                //发送记录插入
                try
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    int i = mgslogic.InsertMessage(msg);
                    if (i > 0)
                    {
                        FS.FrameWork.Management.PublicTrans.Commit();
                    }
                    else
                    {
                        MessageBox.Show("短信记录插入失败");
                        txtExecBillName.Text = "";
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
                catch (Exception ex)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(ex.Message);

                }
            }
        }


      
        //短信发送，调用crm短信发送接口
        public string    DoSendMsg(string mobile,string content)
        {
            #region 发送短信 //{D37652D3-1DB3-4f8c-AFE6-BE21625F082C}
            string req = @"<?xml version='1.0' encoding='UTF-8'?>
                            <relay>
                                <req>　
                                <hospitalid>{0}</hospitalid>
                                <mobile>{1}</mobile>
                                <content>{2}</content>
                                </req>
                                <method>{3}</method>
                            </relay>";
//            string req = @"<?xml version='1.0' encoding='UTF-8'?>
//                            <req>　
//                            <hospitalid>{0}</hospitalid>
//                            <mobile>{1}</mobile>
//                            <content>{2}</content>
//                            </req>";
            try
            {
                FS.HISFC.Models.Base.Employee empl = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                FS.HISFC.Models.Base.Department dept = empl.Dept as FS.HISFC.Models.Base.Department;

                string hospitalCode = string.Empty;

                if (dept.HospitalName.Contains("顺德"))
                {
                    hospitalCode = "IBORNSD";
                }
                else
                {
                    hospitalCode = "IBORNGZ";
                }

                string url = FS.HISFC.BizProcess.Integrate.WSHelper.GetUrl(hospitalCode, FS.HISFC.BizProcess.Integrate.URLTYPE.HIS);

                FS.HISFC.BizProcess.Integrate.IbornMobileService server = new FS.HISFC.BizProcess.Integrate.IbornMobileService();
                server.Url = url;
                string res = server.crmRelay(string.Format(req,hospitalCode,mobile,content, "HISMessageSend"));
                return res;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("发送短信异常！" + e.Message);
                return "";
            }
            #endregion
            return "";
        }

        private void cbTemplate_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbTemplate.Text != "")
            {
              System.Collections.ArrayList al = templatelogic.QueryeTemplateByTitle(cbTemplate.Text);
                if(al.Count>0)
                {
                    txtExecBillName.Text = (al[0] as FS.HISFC.Models.Order.MessageTemplate).MsgTemplateContent;
                }
            }
        }
    }
}
