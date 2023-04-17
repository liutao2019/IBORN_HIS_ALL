using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.OutpatientFee.Forms
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

        /// <summary>
        /// ������ʾ��Ϣ
        /// </summary>
        /// <param name="patient">�ҺŻ�����Ϣ</param>
        /// <param name="ft">�����ķ��÷�����Ϣ</param>
        /// <param name="feeItemLists">������ϸ������Ϣ</param>
        /// <param name="diagLists">�����Ϣ</param>
        /// <param name="otherInfomations">������Ϣ</param>
        public void SetInfomation(FS.HISFC.Models.Registration.Register patient, FS.HISFC.Models.Base.FT ft, ArrayList feeItemLists, ArrayList diagLists,
            params string[] otherInfomations)
        {            
            if (this.medcareInterfaceProxy == null)
            {
                this.medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
            }
            
            if (this.medcareInterfaceProxy == null) 
            {
                return;
            }

            if (patient == null) 
            {
                this.lblPaientInfo.Text = "";
                return;
            }
            
            this.medcareInterfaceProxy.SetPactCode(patient.Pact.ID);
            // {293FDD11-FC10-4ceb-8E4C-1A4304F22592}
            this.medcareInterfaceProxy.IsLocalProcess = false;

            if (feeItemLists == null)// || feeItemLists.Count <= 0)
            {
                if (ft != null)
                {
                    if (patient.Pact.PayKind.ID == "01" || patient.Pact.PayKind.ID == "03")
                    {
                        this.tbDrugSendInfo.Text = ft.User01;
                        if (string.IsNullOrEmpty(tbDrugSendInfo.Text))
                        {
                            tbDrugSendInfo.Text = "ȡҩҩ��";
                        }
                        else
                        {
                            tbDrugSendInfo.Text = "�뵽  " + tbDrugSendInfo + "  ȡҩ";
                        }
                    }
                    this.tbRealOwnCost.Text = ft.RealCost.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.tbReturnCost.Text = ft.ReturnCost.ToString("F4").TrimEnd('0').TrimEnd('.');
                }
                //this.Clear();
                this.lblPaientInfo.Text = patient.Name + "  ���ڰ����۽��ѣ����Ժ�";
            }

            #region ����
            //{CC93C88A-9DD0-49fe-9DC0-B6DA445A7F30}���ݲ����жϡ��������Ƿ���ҽ�������ˣ�
            //Ҫ���ò������Ʋ����Էѻ��ߡ�--�޸�����ֱ�ʱ���ֵ�����
            if (false)
            {
                long returnValue = this.medcareInterfaceProxy.DeleteUploadedFeeDetailsOutpatient(patient, ref feeItemLists);
                if (returnValue == -1)
                {
                    MessageBox.Show(Language.Msg("�����ӿ��ϴ���ϸʧ��!") + this.medcareInterfaceProxy.ErrMsg);

                    return;
                }
                returnValue = this.medcareInterfaceProxy.UploadFeeDetailsOutpatient(patient, ref feeItemLists);
                if (returnValue == -1)
                {
                    MessageBox.Show(Language.Msg("�����ӿ��ϴ���ϸʧ��!") + this.medcareInterfaceProxy.ErrMsg);

                    return;
                }

                returnValue = this.medcareInterfaceProxy.PreBalanceOutpatient(patient, ref feeItemLists);
                if (returnValue == -1)
                {
                    MessageBox.Show(Language.Msg("�����ӿ�Ԥ�������ʧ��!") + this.medcareInterfaceProxy.ErrMsg);

                    return;
                }
                //����ϴ���ϸ,�����ٴ�
                this.medcareInterfaceProxy.Rollback();
            }
            #endregion

            decimal sumTotCost = 0, sumPayCost = 0, sumPubCost = 0, sumOwnCost = 0; decimal sumDrugCost = 0;

            if(feeItemLists != null)
            {
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeItemLists)
                {
                    if (f.IsAccounted)
                    {
                        if (f.FT.OwnCost > 0)
                        {
                            sumPayCost += f.FT.OwnCost;
                            sumOwnCost += 0;
                        }
                        else 
                        {
                            sumPayCost += f.FT.PayCost;
                        }
                        
                    }
                    else
                    {
                        sumTotCost += f.FT.TotCost;
                        sumPayCost += f.FT.PayCost;
                        //{C623A693-19A7-4378-859D-5C07CFF9BEB1}
                        sumPubCost += f.FT.PubCost + f.FT.RebateCost ;
                        sumOwnCost += f.FT.OwnCost - f.FT.RebateCost;
                    }
                    if (this.drugFeeCodeHelper.ArrayObject != null && this.drugFeeCodeHelper.ArrayObject.Count > 0 && this.drugFeeCodeHelper.GetObjectFromID(f.Item.MinFee.ID) != null)
                    {
                        sumDrugCost += f.FT.OwnCost + f.FT.PayCost + f.FT.PubCost;
                    }
                }
            
                if (patient.Pact.PayKind.ID == "01")
                {
                    this.tbOwnCost.Text = FS.FrameWork.Public.String.FormatNumber(sumOwnCost, 2).ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.tbTotCost.Text = FS.FrameWork.Public.String.FormatNumber(sumTotCost, 2).ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.tbPayCost.Text = sumPayCost.ToString("F4").TrimEnd('0').TrimEnd('.');

                    this.tbPubCost.Text = sumPubCost.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.tbDrugCost.Text = sumDrugCost.ToString("F4").TrimEnd('0').TrimEnd('.');

                    this.lblPayTitle.Visible = false;
                    this.tbPayCost.Visible = false;
                    this.lblPubTitle.Visible = false;
                    this.tbPubCost.Visible = false;
                }
                else if (patient.Pact.PayKind.ID == "03")
                {
                    this.tbTotCost.Text = FS.FrameWork.Public.String.FormatNumber(sumTotCost, 2).ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.tbOwnCost.Text = FS.FrameWork.Public.String.FormatNumber(sumOwnCost, 2).ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.tbPayCost.Text = FS.FrameWork.Public.String.FormatNumber(sumPayCost, 2).ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.tbPubCost.Text = FS.FrameWork.Public.String.FormatNumber(sumPubCost, 2).ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.tbDrugCost.Text = sumDrugCost.ToString("F4").TrimEnd('0').TrimEnd('.');

                    this.lblPayTitle.Visible = false;
                    this.tbPayCost.Visible = false;
                    this.lblPubTitle.Visible = true;
                    this.tbPubCost.Visible = true;
                    this.lblPubTitle.Text = "���ѣ�";
                }
                else if (patient.Pact.PayKind.ID == "02")
                {
                    this.tbOwnCost.Text = FS.FrameWork.Public.String.FormatNumber(patient.SIMainInfo.OwnCost, 2).ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.tbTotCost.Text = FS.FrameWork.Public.String.FormatNumber(patient.SIMainInfo.TotCost, 2).ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.tbPayCost.Text = "0.00";
                    this.tbPubCost.Text = FS.FrameWork.Public.String.FormatNumber(patient.SIMainInfo.PubCost, 2).ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.tbDrugCost.Text = sumDrugCost.ToString("F4").TrimEnd('0').TrimEnd('.');

                    this.lblPayTitle.Visible = true;
                    this.tbPayCost.Visible = true;
                    this.lblPubTitle.Visible = true;
                    this.tbPubCost.Visible = true;
                    this.lblPubTitle.Text = "ҽ����";
                }
            }

            if (ft != null)
            {
                this.tbDrugSendInfo.Text = ft.User01;

                if (string.IsNullOrEmpty(tbDrugSendInfo.Text))
                {
                    tbDrugSendInfo.Text = "ȡҩҩ��";
                }
                else
                {
                    tbDrugSendInfo.Text = "�뵽 " + tbDrugSendInfo.Text + " ȡҩ";
                }

                if (ft.RealCost == 0)
                {
                    ft.RealCost = FS.FrameWork.Function.NConvert.ToDecimal(this.tbOwnCost.Text);
                }

                this.tbRealOwnCost.Text = ft.RealCost.ToString("F4").TrimEnd('0').TrimEnd('.');
                this.tbReturnCost.Text = ft.ReturnCost.ToString("F4").TrimEnd('0').TrimEnd('.');
                Decimal ownpay = FS.FrameWork.Function.NConvert.ToDecimal(this.tbOwnCost.Text) + FS.FrameWork.Function.NConvert.ToDecimal(this.tbPayCost.Text);
                this.tbPayCost.Text = FS.FrameWork.Public.String.FormatNumber(patient.SIMainInfo.PayCost, 2).ToString("F4").TrimEnd('0').TrimEnd('.');
                this.lblPaientInfo.Text = patient.Name + "  Ӧ�ɽ��Ϊ " + ownpay.ToString("F4").TrimEnd('0').TrimEnd('.') + " Ԫ";
            }

            #endregion
        }

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
            this.tbDrugCost.Text = "0.00";
            this.tbDrugSendInfo.Text = "ȡҩҩ��";
            this.tbRealOwnCost.Text = "0.00";
            this.tbReturnCost.Text = "0.00";
            this.tbOwnCost.Text = "0.00";
            this.tbTotCost.Text = "0.00";
            this.tbPayCost.Text = "0.00";
            this.tbPubCost.Text = "0.00";
            this.tbDrugCost.Text = "0.00";
            //this.lblPaientInfo.Text = patient.Name + "  ���ڰ����۽��ѣ����Ժ�";
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
                 //this.SetInfomation(
                //FS.HISFC.Models.Registration.Register register
                //    ,FS.HISFC.Models.Base.FT ft
                //        , ArrayList feeItemLists
                //,ArrayList diagLists
                //, params string[] otherInfomations);
                if (value != null)
                {
                    this.SetInfomation(
                   value[0] as FS.HISFC.Models.Registration.Register
                        , value[1] as FS.HISFC.Models.Base.FT
                            , value[2] as ArrayList
                    , value[3] as ArrayList
                    );
                }
                else
                {
                    this.lblPaientInfo.Text = "";
                }
            }
        }

        public int ShowScreen()
        {
          //  this.Clear();

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
            this.Hide();
            return 0;
        }

        #endregion
    }
}
