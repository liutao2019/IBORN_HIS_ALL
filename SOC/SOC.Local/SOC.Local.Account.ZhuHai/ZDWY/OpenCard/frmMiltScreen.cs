using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;

namespace FS.SOC.Local.Account.ZhuHai.ZDWY.OpenCard
{
    public partial class frmMiltScreen : Form, FS.HISFC.BizProcess.Interface.FeeInterface.IMultiScreen
    {
        public frmMiltScreen()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ��Ŀ��Ϣ
        /// </summary>
        private DataSet dsItem = null;

        /// <summary>
        /// ���ô����ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = null;

        /// <summary>
        /// ����ҩƷ����С�����б�
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper drugFeeCodeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        #endregion

        #region ����

        /// <summary>
        /// ����ҩƷ����С�����б�
        /// </summary>
        public FS.FrameWork.Public.ObjectHelper DrugFeeCodeHelper 
        {
            set 
            {
                this.drugFeeCodeHelper = value;
            }
        }

        /// <summary>
        /// ���ô����ӿڱ���
        /// </summary>
        public FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy MedcareInterfaceProxy 
        {
            set 
            {
                this.medcareInterfaceProxy = value;
            }
        }
        private bool isPreeFee = false;
        ///
        /// <summary>
        /// ҽ�������Ƿ�Ԥ����
        /// </summary>
        public bool IsPreFee
        {
            set
            {
                this.isPreeFee = value;
            }
            get
            {
                return this.isPreeFee;
            }
        }


        #endregion

        #region ����

        /// <summary>
        /// ��������ҩƷ����С�����б�
        /// </summary>
        /// <param name="drugFeeCodeHelper">����ҩƷ����С�����б�</param>
        public void SetFeeCodeIsDrugArrayListObj(FS.FrameWork.Public.ObjectHelper drugFeeCodeHelper) 
        {
            this.drugFeeCodeHelper = drugFeeCodeHelper;
        }

        /// <summary>
        /// ���ô����ӿڱ���
        /// </summary>
        /// <param name="medcareInterfaceProxy">�ӿڱ���</param>
        public void SetMedcareInterfaceProxy(FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy) 
        {
            this.medcareInterfaceProxy = medcareInterfaceProxy;
        }

        
        #endregion


        /// <summary>
        /// ������Ŀ��Ϣ
        /// </summary>
        /// <param name="dsItem">��Ŀ��Ϣ����</param>
        public void SetDataSet(DataSet dsItem) 
        {
            this.dsItem = dsItem;
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int Init() 
        {
            if (this.drugFeeCodeHelper != null && (this.drugFeeCodeHelper.ArrayObject == null || this.drugFeeCodeHelper.ArrayObject.Count == 0))
            {
                ArrayList drugFeeCodeList = this.managerIntegrate.GetConstantList("DrugMinFee");
                if (drugFeeCodeList == null)
                {
                    MessageBox.Show(Language.Msg("���ҩƷ��С�����б����!") + this.managerIntegrate.Err);

                    return -1;
                }
                
                this.drugFeeCodeHelper.ArrayObject = drugFeeCodeList;
            }

            return 1;
        }

        /// <summary>
        /// ���
        /// </summary>
        public void Clear() 
        {
            

            this.tbDrugCost.Text = "";
            
            this.tbRealOwnCost.Text = "";
            this.tbReturnCost.Text = "";
             
            this.tbTotCost.Text = "";
            this.tbPayCost.Text = "";
             
            this.tbDrugCost.Text = "";
            this.lblPaientInfo.Text = "��ע��˶Ի�����Ϣ��";//patient.Name + "  ���ڰ����۽��ѣ����Ժ�";
             
        }

        #region IMultiScreen ��Ա

        public System.Collections.Generic.List<Object> ListInfo
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                this.Clear();
                
                if (value != null)
                {
                    //������Ϣ�����ţ��շ�Աid���շ�Ա����
                    this.SetShowInfomation(value[0] as FS .HISFC .Models .RADT .PatientInfo , value[1] as string, value[2] as string, value[3] as string);
                    
                }
                
            }
        }

        public void SetShowInfomation(FS .HISFC .Models .RADT .PatientInfo a, string b, string c, string d)
        {
            if (c.ToString() == "")
            { 
                this.neuPanel4.Visible = true;
                this.neuPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
                this.neuLabel9.Visible = false;
                this.neuLabel10.Visible = false;


                this.lblPaientInfo.Text = "��ע��˶Ի�����Ϣ��";
                this.neuLabel7.Text = "������";
                this.tbRealOwnCost.Text = a.Name.ToString();
                this.neuLabel6.Text = "�Ա�";
                this.tbReturnCost.Text = a.Sex.ToString();
                this.neuLabel8.Text = "�������£�";
                this.tbTotCost.Text = a.Birthday.ToShortDateString();//a.Age.ToString();
                 
                this.tbPayCost.Text = a.Kin.RelationPhone.ToString();
                 
                //this.tbDrugSendInfo.Text = "���ţ�" + b.ToString();
            }
            else
            {//��ʾ��ʼ������
                this.neuPanel4.Visible = false;
                this.neuLabel9.Visible = true;
                this.neuLabel9.Text = c.ToString() + "Ϊ������";// d.ToString();
                this.neuLabel10.Visible = true;
            }

        }

        public void ScreenInvisible()
        {
            this.Visible = false;
        }

        public int ShowScreen()
        {
            this.Clear();

            if (Screen.AllScreens.Length > 1)
            {
                this.Show();

                //this.DesktopBounds = Screen.AllScreens[1].Bounds;
                //this.DesktopBounds = Screen.AllScreens[0].Bounds;
                if (Screen.AllScreens[0].Primary)
                {
                    this.DesktopBounds = Screen.AllScreens[1].Bounds;
                }
                else
                {
                    this.DesktopBounds = Screen.AllScreens[0].Bounds;
                }
            }
            return 0;
        }

        #endregion

        #region IMultiScreen ��Ա

        public int CloseScreen()
        {
            //this.Close();
            this.ScreenInvisible();
            return 0;
        }
        #endregion
    }
}
