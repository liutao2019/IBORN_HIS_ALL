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
    /// [��������: CAǩ������]<br></br>
    /// [�� �� ��: ��ѩ��]<br></br>
    /// [����ʱ��: 2015��01��05]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucCaDemo : UserControl
    {
        //GDCACOM�ؼ�
        ATL_COMLib.GdcaClass GDCACom = new ATL_COMLib.GdcaClass();
        //GDCAWS�ؼ�
        GDCAWSCOMLib.WSClientComClass AGWWSClient = new WSClientComClass();
        //�����豸����
        int deviceType;
        //COM�ؼ����صĴ������
	    int errorCode = -1;
        //�����豸PIN�� Ĭ�ϳ�ʼֵ123456
        string userPin;
        //�������û�ǩ��֤��
        string readOutSignCert;
        //�������û�����֤��
        string readOutEncCert;
        //֤��ID
        string certId;
        //24λ�����
        string randReq;
        //���ǩ������
        string clientSignData;
        //Base64��������
        string base64Data;
        //������������
        string encOutData;
        //����ǩ������
        string signOutData;
        //�����������
        string encInData;
        //����ǩ������
        string signInData;
        //���صļ���֤��
        string serverEncrytCert;
        //���ص�ǩ��֤��
        string serverSignCert;
        //ԭ������
        string orgData;

        public ucCaDemo()
        {
            InitializeComponent();
            Initialize();
        }

        //��ʼ��
        private void Initialize()
        {
            try
            {
                deviceType = GDCACom.GDCA_GetDevicType();   //��ȡ�ͻ��������豸�豸����

                switch (deviceType)
                {
                    case -1:
                        MessageBox.Show("�����֤��Ӳ������(USBKey)��" + deviceType);
                        break;
                    case -2:
                        MessageBox.Show("ע������,�뵼����ȷ��ע����ļ�: " + deviceType);
                        break;
                    case -3:
                        MessageBox.Show("��������USB�豸: " + deviceType);
                        break;
                    default:
                        //���������豸���ͣ������������gdca_device.ini��������Ϣ��gdca_device.ini�ڰ�װ������
                        GDCACom.GDCA_SetDeviceType(deviceType);
                        errorCode = GDCACom.GetError();	//��麯���Ƿ�ɹ����У��ں������н��������
                        if (errorCode != 0)
                        {
                            MessageBox.Show("���������豸���ͳ���" + errorCode);
                        }

                        //��ʼ���ӿ�����Ҫ��ȫ����Դ���ڴ桢�ź����ȣ��������ʼ���ɹ�������ֵΪ0
                        GDCACom.GDCA_Initialize();
                        errorCode = GDCACom.GetError(); //��麯���Ƿ�ɹ����У��ں������н��������
                        if (errorCode != 0)
                        {
                            MessageBox.Show("��ʼ���ؼ�����" + errorCode);
                        }
                        break;
                    }
                }
            catch (Exception a)
            {
                MessageBox.Show("GDCA3.0�ؼ���ʼ��ʧ�ܣ�.   error:" + a.Message);
                GDCACom.GDCA_Finalize();         /*�ͷ������豸�ӿ���Դ*/
            }
        }

        //�û���¼
        private void Login()
        {
            userPin = tbUserPin.Text;
            try
            {
                if (String.IsNullOrEmpty(userPin))
                {
                    MessageBox.Show("������PIN��");
                    tbUserPin.Focus();
                }

                GDCACom.GDCA_Login(2, userPin); //�ͻ��������豸��¼
                errorCode = GDCACom.GetError();	//��麯���Ƿ�ɹ����У��ں������н��������
                if (errorCode != 0)
                {
                    MessageBox.Show("�û���½ʧ�ܣ�" + errorCode);
                    GDCACom.GDCA_Logout();     //�û��˳�
                }
                else
                {
                    btIsLogin.Text = "ON";
                }
            }
            catch (Exception a)
            {
                MessageBox.Show("�û���¼����.   error:" + a.Message);
                GDCACom.GDCA_Logout();         //�û��˳�
            }
        }

        //�û��˳�
        private void Logout()
        {
            try
            {
                if (GDCACom.GDCA_isLogin(2) == 0)     //�ж��û��Ƿ��½
                {
                    GDCACom.GDCA_Logout();      //�˳���¼
                    errorCode = GDCACom.GetError();   //�����һ�����Ƿ�ɹ�����
                    if (errorCode != 0)              //�˳���¼ʧ��
                    {
                        MessageBox.Show("�˳���¼ʧ��:" + errorCode);
                    }
                    else
                    {
                        btIsLogin.Text = "OFF";
                    }
                }
            }
            catch (Exception a)
            {
                MessageBox.Show("GDCA3.0�˳���¼����.   error: " + a.Message);
            }
        }

        //��ȡ֤��
        private void ReadLabel()
        {
            try
            {
               //��ȡ�û�ǩ��֤�飬������readOutSignCert��
		       readOutSignCert = GDCACom.GDCA_ReadLabel("LAB_USERCERT_SIG",7);
               errorCode = GDCACom.GetError(); //��麯���Ƿ�ɹ����У��ں������н��������
               //֤���ȡʧ��
               if (errorCode != 0)
               {
                   MessageBox.Show("��ȡ�û�ǩ��֤��ʧ�ܣ�" + errorCode);
                   GDCACom.GDCA_Logout();        //�˳���¼
               }
               else
               {
                   tbReadOutSignCert.Text = readOutSignCert;
               }
            }
            catch (Exception a)
            {
                MessageBox.Show("��ȡ�û�ǩ��֤�����.   error: " + a.Message);
                GDCACom.GDCA_Logout();          //�˳���¼
            }
            try
            {
                //��ȡ�û�����֤�飬������readOutEncCert��
                readOutEncCert = GDCACom.GDCA_ReadLabel("LAB_USERCERT_ENC", 8);
                errorCode = GDCACom.GetError();  //��麯���Ƿ�ɹ����У��ں������н��������
                //֤���ȡʧ��
                if (errorCode != 0)
                {
                    MessageBox.Show("��ȡ�û�����֤��ʧ�ܣ�" + errorCode);
                    GDCACom.GDCA_Logout();          //�˳���¼
                }
                else
                {
                    tbReadOutEncCert.Text = readOutEncCert;
                }
            }
            catch (Exception a)
            {
                MessageBox.Show("��ȡ�û�����֤�����.   error: " + a.Message);
                GDCACom.GDCA_Logout();          //�˳���¼
            }
        }

        private void IsLogin()
        {
            GDCACom.GDCA_isLogin(2);//�ж��û��Ƿ��½
            errorCode = GDCACom.GetError();	//��麯���Ƿ�ɹ����У��ں������н��������
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
                Logout();//�û��˳�
                GDCACom.GDCA_Finalize();         /*�ͷ������豸�ӿ���Դ*/
                errorCode = GDCACom.GetError(); //��麯���Ƿ�ɹ����У��ں������н��������
                if (errorCode != 0)
                {
                    MessageBox.Show("�ͷŽӿ���Դʧ�ܣ�" + errorCode);
                }
                else
                {
                    this.FindForm().Close();//�رմ���
                }
            }
            catch (Exception a)
            {
                MessageBox.Show("GDCA3.0�ӿ���Դ�ͷų���.   error: " + a.Message);
            }
        }

        //2.1�����֤ ʱ���ϵ�����Ͳ���ϸд�ˣ�ÿ���������ɶ����ḻ��trycatch�Ǳ���ġ�
        private void btLogin_Click(object sender, EventArgs e)
        {
            //��½
            Login();
            //��ȡ֤��
            ReadLabel();
            //��ʼ��WS
            AGWWSClient.Initialize("http://bswg.95105813.cn:8080/AGW/services/AGWService?wsdl");
            //��ȡ֤��ID
            certId = AGWWSClient.CheckCert("GDCATest", "CertCheck", tbReadOutSignCert.Text);
            tbCertId.Text = certId;
            //����24λ�����
            randReq = AGWWSClient.GenRand(24);
            //��֤��ǩ��
            clientSignData = GDCACom.GDCA_OpkiSignData("LAB_USERCERT_SIG", 4, tbReadOutSignCert.Text, GDCACom.GDCA_Base64Encode(randReq), 32772, 0);
            //�Դ�����ǩ��ֵ����ǩ����֤
            AGWWSClient.PKCS1Verify("GDCATest", "VerifySign", tbReadOutSignCert.Text, randReq, clientSignData);
            errorCode = AGWWSClient.GetError();
            if (errorCode != 0)
            {
                MessageBox.Show("�����֤ʧ�ܣ�" + errorCode);
            }
            else
            {
                MessageBox.Show("�����֤�ɹ���" + errorCode);
                btSign.Enabled = true;
            }
        }

        private void neuButton1_Click(object sender, EventArgs e)
        {
            tbData.Text = "13|46977|0000203944|5016|3010|5016|14622482|105175|������|2015-01-08 11:46:06|0|0||0|LZ|��ʱҽ��|0|1|1|1|0|0|1|Y00000015376|�Ȼ��ػ���Ƭ��48Ƭ��|P|��ҩ|9008|5016|�����ڿ�סԺ|0.5000|g|Ƭ|Ƭ|48|0.5g*48Ƭ/��|02|P|O|10.340000|5404460|0|0|1|�ڷ�||BID|ÿ�ն���|1.0000|0|4.0000|1.00|2015-01-08 11:46:06|0001-01-01|105175|������|0|0001-01-01|||0001-01-01|||||||0|0001-01-01||0|2015-01-08 11:46:06|2015-01-08 11:46:06|||0|||0|0|0|2000001|||0||0||||||8:00-16:00||1|g||301040|-1|-1|DEPT5016|20";
        }

        //2.2���ݼ���/ǩ�� ʱ���ϵ�����Ͳ���ϸд�ˣ�ÿ���������ɶ����ḻ��trycatch�Ǳ���ġ�
        private void btSign_Click(object sender, EventArgs e)
        {
            //�����ݽ���BASE64λ���룬�ⲽ�Ǽ���ǰ�ı�Ҫ���裬��Ϊ���ܵ���α�����BASE64λ����
            base64Data = GDCACom.GDCA_Base64Encode(tbData.Text);
            //��ȡ���ؼ���֤��
            serverEncrytCert = AGWWSClient.GetEncCert();
            //��ȡ����ǩ��֤��
            serverSignCert = AGWWSClient.GetSigCert();
            //��ȡ���ص�BASE64����֤�飬�Դ����BASE64�ѱ������ݣ�����26115����Ӧ'GDCA_ALGO_3DES'����������Ľӿ��ĵ����㷨���������ŷ⣨GDCA_OpkiSealEnvelope�����ܣ����ܺ�����ݱ�����encOutData��
            encOutData = GDCACom.GDCA_OpkiSealEnvelope(serverEncrytCert, base64Data, 26115);
            tbSeal.Text = encOutData;
            //"LAB_USERCERT_SIG"�û�ǩ����ǩ���ƣ���ǩ����Ϊ4��ʹ���û�ǩ��֤��readOutSignCert���Դ����BASE64λ�ѱ������ݣ�����32771����Ӧ'GDCA_ALGO_MD5'����������Ľӿ��ĵ����㷨��������ǩ����GDCA_OpkiSignData����ǩ��������ݱ�����signOutData��
            signOutData = GDCACom.GDCA_OpkiSignData("LAB_USERCERT_SIG", 4, tbReadOutEncCert.Text, GDCACom.GDCA_Base64Encode(encOutData), 32772, 0);
            tbDataSign.Text = signOutData;
            //�����ŷ����
            orgData = AGWWSClient.OpenEnvelope("GDCATest", "DecData", encOutData);
            //��ǩ
            AGWWSClient.PKCS1Verify("GDCATest", "VerifySign", tbReadOutSignCert.Text, encOutData, signOutData);
            errorCode = AGWWSClient.GetError();
            if (errorCode != 0)
            {
                MessageBox.Show("��������ǩʧ�ܣ�" + errorCode);
            }
            else 
            {
                //�Խ��ܺ��ԭ�������ٽ��з������˵������ŷ���ܣ���ʹ�ÿͻ��˼���֤��
                encInData = AGWWSClient.SealEnvelope("GDCATest", "EncData", tbReadOutEncCert.Text, orgData);
                //�ڷ�������ʹ�÷�������֤��˽Կ����ǩ����׼�����ؿͻ�����ǩ
                signInData = AGWWSClient.PKCS1Sign("GDCATest", "SignData", serverSignCert, encInData);
                //�ͻ��������ŷ���ܲ������ܺ��BASE64λ��������ת��Ϊ�ɶ�������
                tbOpen.Text = GDCACom.GDCA_Base64Decode(GDCACom.GDCA_OpkiOpenEnvelope("LAB_USERCERT_ENC", 5, encInData));
                //����ǩ�����������пͻ�����ǩ�������Խ�����ǩ��ԭ�����ݽ���BASE64λ���룬�ú�������ֵencInData���ѱ������ݣ��ⲽ����ǩǰ�ı�Ҫ���裬��Ϊ��ǩ����α�����BASE64λ���룬����COM�ӿڽ�������ǩ����Ҫ�˲�����һ��BASE64����
                GDCACom.GDCA_OpkiVerifyData(serverSignCert, GDCACom.GDCA_Base64Encode(tbOpen.Text), signInData, 32772, 0);
                if (errorCode != 0)
                {
                    MessageBox.Show("�ͻ�����ǩʧ�ܣ�" + errorCode);
                }
                else
                {
                    MessageBox.Show("�ͻ�����ǩ�ɹ���" + errorCode);
                }
            }
        }
    }
}
