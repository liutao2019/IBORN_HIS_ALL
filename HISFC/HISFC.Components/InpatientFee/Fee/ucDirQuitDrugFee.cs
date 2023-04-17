using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Fee.Inpatient;
using FS.FrameWork.Management;
using System.Collections;

namespace FS.HISFC.Components.InpatientFee.Fee
{
    /// <summary>
    /// ucDircQuitFee<br></br>
    /// [��������: סԺֱ����ҩUC]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-10]<br></br>
    /// <˵��>
    ///     1��������Ϊ��С�汾ֱ���˷�ʵ�ֶ����
    ///     2��ͬʱ����˷�����ҩ�������
    ///     3���ù��ܽ�����ҩƷ���˷ѻ���
    /// </˵��>
    /// </summary>
    public partial class ucDirQuitDrugFee : ucQuitFee
    {
        public ucDirQuitDrugFee()
        {
            InitializeComponent();
            this.fpQuit_SheetUndrug.Visible = false;
            this.fpUnQuit_SheetUndrug.Visible = false;
        }

        /// <summary>
        /// ����˷ѵ���Ŀ
        /// </summary>
        /// <returns>�ɹ� ������Ŀ���� ʧ�� null</returns>
        private List<FeeItemList> GetConfirmDrugItem()
        {
            List<FeeItemList> feeItemLists = new List<FeeItemList>();

            for (int i = 0; i < this.fpQuit_SheetDrug.Rows.Count; i++)
            {
                if (this.fpQuit_SheetDrug.Rows[i].Tag != null && this.fpQuit_SheetDrug.Rows[i].Tag is FeeItemList)
                {
                    FeeItemList feeItemList = this.fpQuit_SheetDrug.Rows[i].Tag as FeeItemList;
                    if (feeItemList.NoBackQty > 0)
                    {
                        feeItemList.Item.Qty = feeItemList.NoBackQty;
                        feeItemList.NoBackQty = 0;
                        feeItemList.FT.TotCost = feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty;
                        feeItemList.FT.OwnCost = feeItemList.FT.TotCost;
                        feeItemList.IsNeedUpdateNoBackQty = true;

                        feeItemLists.Add(feeItemList);
                    }
                }
            }

            return feeItemLists;
        }

        /// <summary>
        /// ֱ���˷�
        /// </summary>
        public override int Save()
        {
            if (this.patientInfo == null || string.IsNullOrEmpty(patientInfo.ID))
            {
                MessageBox.Show("�����뻼��סԺ�Ų��س�ȷ�ϣ�");
                this.ucQueryPatientInfo.Focus();
                return -1;
            }

            List<FeeItemList> quitItem = this.GetConfirmDrugItem();
            if (quitItem.Count == 0)
            {
                MessageBox.Show("û�п��˵ķ��ã�");
                return -1;
            }
            FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();
            DateTime sysTime = dataManager.GetDateTimeFromSysDateTime();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.phamarcyIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList info in quitItem)
            {
                //{26757C60-3E01-47a2-963F-93B0E26565A6}  �����˺�������˳��
                //��Ҫ��ȡ�����룬�ٽ����˷�
                if (info.PayType == FS.HISFC.Models.Base.PayTypes.Balanced)
                {
                    //ȡ����������
                    if (this.phamarcyIntegrate.CancelApplyOut(info.Clone()) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("ȡ��ҩƷ����ʧ�ܣ�") + this.phamarcyIntegrate.Err);
                        return -1;
                    }
                }
                //�˷Ѳ���
                if (this.feeIntegrate.QuitItem(this.patientInfo, info.Clone()) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("�˷�ʧ��!") + this.feeIntegrate.Err);
                    return -1;
                }

                //�˿����
                if (this.phamarcyIntegrate.OutputReturn(info, dataManager.Operator.ID, sysTime) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("�˷�ʱ�˿�ʧ��!") + this.phamarcyIntegrate.Err);
                    return -1;
                }               
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(Language.Msg("�˷ѳɹ�!"));
            return 1;
        }

        /// <summary>
        /// ��ȡҩƷ��Ϣ
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <returns>1:�ɹ� -1ʧ��</returns>
        protected override int RetriveDrug(DateTime beginTime, DateTime endTime)
        {
            #region δ��ҩ
            ArrayList drugList = inpatientManager.QueryMedItemListsCanQuit(this.patientInfo.ID, beginTime, endTime, "1");
            if (drugList == null)
            {
                MessageBox.Show(Language.Msg("���ҩƷ�б����!") + this.inpatientManager.Err);

                return -1;
            }
            else
            {
                this.SetDrugList(drugList);
            }
            #endregion

            #region �Ѱ�ҩ
            ArrayList drugListed = inpatientManager.QueryMedItemListsCanQuit(this.patientInfo.ID, beginTime, endTime, "2");
            if (drugListed == null)
            {
                MessageBox.Show(Language.Msg("���ҩƷ�б����!") + this.inpatientManager.Err);

                return -1;
            }
            else
            {
                this.SetDrugList(drugListed);
            }
            #endregion
            if (drugList == null && drugListed == null)
            {
                return -1;
            }
            return 1;
        }
       
    }        
}
