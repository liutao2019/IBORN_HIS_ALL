using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using ATL_COMLib;
using GDCAWSCOMLib;

namespace Neusoft.HISFC.Components.Manager.Controls
{
    /// <summary>
    /// [功能描述: CA签名样例]<br></br>
    /// [创 建 者: 李雪龙]<br></br>
    /// [创建时间: 2015－01－05]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucCaDemo : UserControl
    {
        //GDCACOM控件
        ATL_COMLib.GdcaClass GDCACom = new ATL_COMLib.GdcaClass();
        //GDCAWS控件
        GDCAWSCOMLib.WSClientComClass AGWWSClient = new WSClientComClass();
        //密码设备类型
        int deviceType;
        //COM控件返回的错误代码
	    int errorCode = -1;
        //密码设备PIN码 默认初始值123456
        string userPin;
        //读出的用户签名证书
        string readOutSignCert;
        //读出的用户加密证书
        string readOutEncCert;
        //证书ID
        string certId;
        //24位随机数
        string randReq;
        //身份签名数据
        string clientSignData;
        //Base64编码数据
        string base64Data;
        //传出加密数据
        string encOutData;
        //传出签名数据
        string signOutData;
        //传入加密数据
        string encInData;
        //传入签名数据
        string signInData;
        //网关的加密证书
        string serverEncrytCert;
        //网关的签名证书
        string serverSignCert;
        //原文数据
        string orgData;

        public ucCaDemo()
        {
            InitializeComponent();
            Initialize();
        }

        //初始化
        private void Initialize()
        {
            try
            {
                deviceType = GDCACom.GDCA_GetDevicType();   //获取客户端密码设备设备类型

                switch (deviceType)
                {
                    case -1:
                        MessageBox.Show("请插入证书硬件介质(USBKey)：" + deviceType);
                        break;
                    case -2:
                        MessageBox.Show("注册表错误,请导入正确的注册表文件: " + deviceType);
                        break;
                    case -3:
                        MessageBox.Show("有其他的USB设备: " + deviceType);
                        break;
                    default:
                        //设置密码设备类型，具体设置详见gdca_device.ini的配置信息，gdca_device.ini在安装包中有
                        GDCACom.GDCA_SetDeviceType(deviceType);
                        errorCode = GDCACom.GetError();	//检查函数是否成功运行，在函数运行结束后调用
                        if (errorCode != 0)
                        {
                            MessageBox.Show("设置密码设备类型出错：" + errorCode);
                        }

                        //初始化接口所需要的全局资源（内存、信号量等），如果初始化成功，返回值为0
                        GDCACom.GDCA_Initialize();
                        errorCode = GDCACom.GetError(); //检查函数是否成功运行，在函数运行结束后调用
                        if (errorCode != 0)
                        {
                            MessageBox.Show("初始化控件出错：" + errorCode);
                        }
                        break;
                    }
                }
            catch (Exception a)
            {
                MessageBox.Show("GDCA3.0控件初始化失败！.   error:" + a.Message);
                GDCACom.GDCA_Finalize();         /*释放密码设备接口资源*/
            }
        }

        //用户登录
        private void Login()
        {
            userPin = tbUserPin.Text;
            try
            {
                if (String.IsNullOrEmpty(userPin))
                {
                    MessageBox.Show("请输入PIN码");
                    tbUserPin.Focus();
                }

                GDCACom.GDCA_Login(2, userPin); //客户端密码设备登录
                errorCode = GDCACom.GetError();	//检查函数是否成功运行，在函数运行结束后调用
                if (errorCode != 0)
                {
                    MessageBox.Show("用户登陆失败：" + errorCode);
                    GDCACom.GDCA_Logout();     //用户退出
                }
                else
                {
                    btIsLogin.Text = "ON";
                }
            }
            catch (Exception a)
            {
                MessageBox.Show("用户登录出错！.   error:" + a.Message);
                GDCACom.GDCA_Logout();         //用户退出
            }
        }

        //用户退出
        private void Logout()
        {
            try
            {
                if (GDCACom.GDCA_isLogin(2) == 0)     //判断用户是否登陆
                {
                    GDCACom.GDCA_Logout();      //退出登录
                    errorCode = GDCACom.GetError();   //检查上一函数是否成功运行
                    if (errorCode != 0)              //退出登录失败
                    {
                        MessageBox.Show("退出登录失败:" + errorCode);
                    }
                    else
                    {
                        btIsLogin.Text = "OFF";
                    }
                }
            }
            catch (Exception a)
            {
                MessageBox.Show("GDCA3.0退出登录出错！.   error: " + a.Message);
            }
        }

        //读取证书
        private void ReadLabel()
        {
            try
            {
               //读取用户签名证书，保存在readOutSignCert中
		       readOutSignCert = GDCACom.GDCA_ReadLabel("LAB_USERCERT_SIG",7);
               errorCode = GDCACom.GetError(); //检查函数是否成功运行，在函数运行结束后调用
               //证书读取失败
               if (errorCode != 0)
               {
                   MessageBox.Show("读取用户签名证书失败：" + errorCode);
                   GDCACom.GDCA_Logout();        //退出登录
               }
               else
               {
                   tbReadOutSignCert.Text = readOutSignCert;
               }
            }
            catch (Exception a)
            {
                MessageBox.Show("读取用户签名证书出错！.   error: " + a.Message);
                GDCACom.GDCA_Logout();          //退出登录
            }
            try
            {
                //读取用户加密证书，保存在readOutEncCert中
                readOutEncCert = GDCACom.GDCA_ReadLabel("LAB_USERCERT_ENC", 8);
                errorCode = GDCACom.GetError();  //检查函数是否成功运行，在函数运行结束后调用
                //证书读取失败
                if (errorCode != 0)
                {
                    MessageBox.Show("读取用户加密证书失败：" + errorCode);
                    GDCACom.GDCA_Logout();          //退出登录
                }
                else
                {
                    tbReadOutEncCert.Text = readOutEncCert;
                }
            }
            catch (Exception a)
            {
                MessageBox.Show("读取用户加密证书出错！.   error: " + a.Message);
                GDCACom.GDCA_Logout();          //退出登录
            }
        }

        private void IsLogin()
        {
            GDCACom.GDCA_isLogin(2);//判断用户是否登陆
            errorCode = GDCACom.GetError();	//检查函数是否成功运行，在函数运行结束后调用
            if (errorCode != 0)
            {
                btIsLogin.Text = "OFF";
            }
            else
            {
                btIsLogin.Text = "ON";
            }
        }

        private void btIsLogin_Click(object sender, EventArgs e)
        {
            IsLogin();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            try
            {
                Logout();//用户退出
                GDCACom.GDCA_Finalize();         /*释放密码设备接口资源*/
                errorCode = GDCACom.GetError(); //检查函数是否成功运行，在函数运行结束后调用
                if (errorCode != 0)
                {
                    MessageBox.Show("释放接口资源失败！" + errorCode);
                }
                else
                {
                    this.FindForm().Close();//关闭窗口
                }
            }
            catch (Exception a)
            {
                MessageBox.Show("GDCA3.0接口资源释放出错！.   error: " + a.Message);
            }
        }

        //2.1身份认证 时间关系方法就不详细写了，每个函数都可独立丰富，trycatch是必须的。
        private void btLogin_Click(object sender, EventArgs e)
        {
            //登陆
            Login();
            //读取证书
            ReadLabel();
            //初始化WS
            AGWWSClient.Initialize("http://bswg.95105813.cn:8080/AGW/services/AGWService?wsdl");
            //获取证书ID
            certId = AGWWSClient.CheckCert("GDCATest", "CertCheck", tbReadOutSignCert.Text);
            tbCertId.Text = certId;
            //产生24位随机数
            randReq = AGWWSClient.GenRand(24);
            //对证书签名
            clientSignData = GDCACom.GDCA_OpkiSignData("LAB_USERCERT_SIG", 4, tbReadOutSignCert.Text, GDCACom.GDCA_Base64Encode(randReq), 32772, 0);
            //对传来的签名值进行签名验证
            AGWWSClient.PKCS1Verify("GDCATest", "VerifySign", tbReadOutSignCert.Text, randReq, clientSignData);
            errorCode = AGWWSClient.GetError();
            if (errorCode != 0)
            {
                MessageBox.Show("身份验证失败！" + errorCode);
            }
            else
            {
                MessageBox.Show("身份验证成功！" + errorCode);
                btSign.Enabled = true;
            }
        }

        private void neuButton1_Click(object sender, EventArgs e)
        {
            tbData.Text = "13|46977|0000203944|5016|3010|5016|14622482|105175|陈美竹|2015-01-08 11:46:06|0|0||0|LZ|临时医嘱|0|1|1|1|0|0|1|Y00000015376|氯化钾缓释片（48片）|P|西药|9008|5016|呼吸内科住院|0.5000|g|片|片|48|0.5g*48片/盒|02|P|O|10.340000|5404460|0|0|1|口服||BID|每日二次|1.0000|0|4.0000|1.00|2015-01-08 11:46:06|0001-01-01|105175|陈美竹|0|0001-01-01|||0001-01-01|||||||0|0001-01-01||0|2015-01-08 11:46:06|2015-01-08 11:46:06|||0|||0|0|0|2000001|||0||0||||||8:00-16:00||1|g||301040|-1|-1|DEPT5016|20";
        }

        //2.2数据加密/签名 时间关系方法就不详细写了，每个函数都可独立丰富，trycatch是必须的。
        private void btSign_Click(object sender, EventArgs e)
        {
            //对数据进行BASE64位编码，这步是加密前的必要步骤，因为加密的入参必须是BASE64位编码
            base64Data = GDCACom.GDCA_Base64Encode(tbData.Text);
            //获取网关加密证书
            serverEncrytCert = AGWWSClient.GetEncCert();
            //获取网关签名证书
            serverSignCert = AGWWSClient.GetSigCert();
            //获取网关的BASE64加密证书，对传入的BASE64已编码数据，根据26115（对应'GDCA_ALGO_3DES'，具体请查阅接口文档）算法进行数字信封（GDCA_OpkiSealEnvelope）加密，加密后的数据保存在encOutData中
            encOutData = GDCACom.GDCA_OpkiSealEnvelope(serverEncrytCert, base64Data, 26115);
            tbSeal.Text = encOutData;
            //"LAB_USERCERT_SIG"用户签名标签名称，标签类型为4，使用用户签名证书readOutSignCert，对传入的BASE64位已编码数据，根据32771（对应'GDCA_ALGO_MD5'，具体请查阅接口文档）算法进行数字签名（GDCA_OpkiSignData），签名后的数据保存在signOutData中
            signOutData = GDCACom.GDCA_OpkiSignData("LAB_USERCERT_SIG", 4, tbReadOutEncCert.Text, GDCACom.GDCA_Base64Encode(encOutData), 32772, 0);
            tbDataSign.Text = signOutData;
            //数字信封解密
            orgData = AGWWSClient.OpenEnvelope("GDCATest", "DecData", encOutData);
            //验签
            AGWWSClient.PKCS1Verify("GDCATest", "VerifySign", tbReadOutSignCert.Text, encOutData, signOutData);
            errorCode = AGWWSClient.GetError();
            if (errorCode != 0)
            {
                MessageBox.Show("服务器验签失败！" + errorCode);
            }
            else 
            {
                //对解密后的原文数据再进行服务器端的数字信封加密，需使用客户端加密证书
                encInData = AGWWSClient.SealEnvelope("GDCATest", "EncData", tbReadOutEncCert.Text, orgData);
                //在服务器端使用服务器端证书私钥进行签名，准备传回客户端验签
                signInData = AGWWSClient.PKCS1Sign("GDCATest", "SignData", serverSignCert, encInData);
                //客户端数字信封解密并将解密后的BASE64位编码数据转换为可读的明文
                tbOpen.Text = GDCACom.GDCA_Base64Decode(GDCACom.GDCA_OpkiOpenEnvelope("LAB_USERCERT_ENC", 5, encInData));
                //数字签名操作，进行客户端验签操作。对进行验签的原文数据进行BASE64位编码，该函数返回值encInData是已编码数据，这步是验签前的必要步骤，因为验签的入参必须是BASE64位编码，并且COM接口进行数字签名需要此步进行一次BASE64编码
                GDCACom.GDCA_OpkiVerifyData(serverSignCert, GDCACom.GDCA_Base64Encode(tbOpen.Text), signInData, 32772, 0);
                if (errorCode != 0)
                {
                    MessageBox.Show("客户端验签失败！" + errorCode);
                }
                else
                {
                    MessageBox.Show("客户端验签成功！" + errorCode);
                }
            }
        }
    }
}
