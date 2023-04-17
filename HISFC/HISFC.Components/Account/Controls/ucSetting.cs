using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.BizProcess.Integrate;
using System.Collections;

namespace FS.HISFC.Components.Account.Controls
{
    public partial class ucSetting : UserControl, FS.HISFC.BizProcess.Interface.Common.IControlParamMaint
    {
        public ucSetting()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// ������Ϣ
        /// </summary>
        private string errText = string.Empty;
        /// <summary>
        /// �Ƿ��޸�
        /// </summary>
        private bool isModify = false;
        /// <summary>
        /// �Ƿ���ʾ��ť
        /// </summary>
        private bool isShowOwnButtons = true;

        /// <summary>
        /// ���Ʋ���ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        /// <summary>
        /// �ʻ�����
        /// </summary>
        AccountConstant accountConstant = new AccountConstant();

        /// <summary>
        /// ������ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public string Description
        {
            get
            {
                return "�����ʻ���������";
            }
            set
            {

            }
        }
        /// <summary>
        /// ������ʾ
        /// </summary>
        public string ErrText
        {
            get
            {
                return errText;
            }
            set
            {
                errText = value;
            }
        }

        /// <summary>
        /// �Ƿ��޸�
        /// </summary>
        public bool IsModify
        {
            get
            {
                return isModify;
            }
            set
            {
                isModify = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ��ť
        /// </summary>
        public bool IsShowOwnButtons
        {
            get
            {
                return isShowOwnButtons;
            }
            set
            {
                isShowOwnButtons = value;
            }
        }
        #endregion 

        #region ����

        public int Apply()
        {
            return 1;
        }
 
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            #region ����
            this.ckbIsAcceptCardFee.Checked = this.controlParamIntegrate.GetControlParam<bool>(AccountConstant.IsAcceptCardFee, true, false);
            this.txtAcceptCardFee.Text = this.controlParamIntegrate.GetControlParam<string>(AccountConstant.AcceptCardFee, true, "0");
            //this.ckCase.Checked = this.controlParamIntegrate.GetControlParam<bool>(AccountConstant.BulidCardIsCreateCaseInfo, true, true);
            #endregion 

            #region ����
            this.ckbisChangeCardFee.Checked = this.controlParamIntegrate.GetControlParam<bool>(AccountConstant.IsAcceptChangeCardFee, true, false);
            this.txtChangeCardFee.Text = this.controlParamIntegrate.GetControlParam<string>(AccountConstant.AcceptChangeCardFee, true, "0");
            #endregion
            this.Focus();
            return 1;
        }

        /// <summary>
        /// �ӽ����ȡ�Ŀ��Ʋ���ֵ
        /// </summary>
        /// <returns>�ӽ����ȡ�Ŀ��Ʋ���ֵ����</returns>
        public ArrayList GetAllControl()
        {
            ArrayList allControlValues = new ArrayList(); //���еĿ����༯��

            FS.HISFC.Models.Base.ControlParam tempControlObj = null;//��ʱ������ʵ��;

            string tempControlValue = null;// �ӽ����ȡ�Ŀ��Ʋ���ֵ
                        
   

            #region ����
            #region �Ƿ���ȡ���ɱ�����
            if (this.ckbIsAcceptCardFee.Checked == true)
            {
                tempControlValue = "1";//��ȡ
            }
            else
            {
                tempControlValue = "0";//����ȡ
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = AccountConstant.IsAcceptCardFee;
            tempControlObj.Name = accountConstant.GetParamDescription(AccountConstant.IsAcceptCardFee);
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible= true;

            allControlValues.Add(tempControlObj.Clone());
            #endregion

            #region  ��ȡ���
            tempControlValue = this.txtAcceptCardFee.Text;
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = AccountConstant.AcceptCardFee;
            tempControlObj.Name = accountConstant.GetParamDescription(AccountConstant.AcceptCardFee);
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());
            #endregion

            #region ������ͬʱ�Ƿ���������Ϣ
            //if (this.ckCase.Checked == true)
            //{
            //    tempControlValue = "1";//����������Ϣ
            //}
            //else
            //{
            //    tempControlValue = "0";//������������Ϣ
            //}
            //tempControlObj = new FS.HISFC.Models.Base.Controler();
            //tempControlObj.ID = AccountConstant.BulidCardIsCreateCaseInfo;
            //tempControlObj.Name = accountConstant.GetParamDescription(AccountConstant.BulidCardIsCreateCaseInfo);
            //tempControlObj.ControlerValue = tempControlValue;
            //tempControlObj.IsVisible = true;

            //allControlValues.Add(tempControlObj.Clone());
            #endregion
            #endregion

            #region ����
            #region �Ƿ���ȡ
            if (this.ckbisChangeCardFee.Checked == true)
            {
                tempControlValue = "1";//��ȡ
            }
            else
            {
                tempControlValue = "0";//����ȡ
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = AccountConstant.IsAcceptChangeCardFee;
            tempControlObj.Name = accountConstant.GetParamDescription(AccountConstant.IsAcceptChangeCardFee);
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());
            #endregion

            #region  ��ȡ���
            tempControlValue = this.txtChangeCardFee.Text;
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = AccountConstant.AcceptChangeCardFee;
            tempControlObj.Name = accountConstant.GetParamDescription(AccountConstant.AcceptChangeCardFee);
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());
            #endregion

            #endregion

            return allControlValues;
        }
      
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            ArrayList allControlsValues = GetAllControl();
            if (allControlsValues == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();

                return -1;
            }

            this.managerIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            foreach (FS.HISFC.Models.Base.ControlParam c in allControlsValues)
            {
                int iReturn = this.managerIntegrate.InsertControlerInfo(c);
                if (iReturn == -1)
                {
                    //�����ظ���˵���Ѿ����ڲ���ֵ,��ôֱ�Ӹ���
                    if (managerIntegrate.DBErrCode == 1)
                    {
                        iReturn = this.managerIntegrate.UpdateControlerInfo(c);
                        if (iReturn <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("���¿��Ʋ���[" + c.Name + "]ʧ��! ���Ʋ���ֵ:" + c.ID + "\n������Ϣ:" + this.managerIntegrate.Err);

                            return -1;
                        }
                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("������Ʋ���[" + c.Name + "]ʧ��! ���Ʋ���ֵ:" + c.ID + "\n������Ϣ:" + this.managerIntegrate.Err);

                        return -1;
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("����ɹ�!");

            return 1;
        }

        #endregion

        #region �¼�
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        private void ucSetting_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void ckbIsAcceptCardFee_CheckedChanged(object sender, EventArgs e)
        {
            if (!ckbIsAcceptCardFee.Checked)
            {
                this.txtAcceptCardFee.Text = "0";
                this.txtAcceptCardFee.ReadOnly = true;
            }
            else
            {
                this.txtAcceptCardFee.ReadOnly = false;
            }
        }

        private void ckbisChangeCardFee_CheckedChanged(object sender, EventArgs e)
        {
            if (!ckbisChangeCardFee.Checked)
            {
                this.txtChangeCardFee.Text = "0";
                this.txtChangeCardFee.ReadOnly = true;
            }
            else
            {
                this.txtChangeCardFee.ReadOnly = false;
            }
        }
        #endregion

    }
}
