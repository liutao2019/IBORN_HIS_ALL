using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Integrate.Material
{
    /// <summary>
    /// Material<br></br>
    /// <Font color='#FF1111'>[��������: �����շѽӿڴ���]</Font><br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2010-07-06]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///		/>
    /// </summary>
    public class Material : FS.HISFC.BizProcess.Interface.Material.IMatFee
    {
        #region ˽�б���

        /// <summary>
        /// ���ýӿ�ʵ��
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Material.IMatFee matFeeAchieve = null;
        /// <summary>
        /// �Ƿ��ʼ�����ýӿ�
        /// 
        /// {C0E249B0-DE20-4d56-9C1E-9952858E32F1}
        /// </summary>
        private bool blnHasInitFeeAchieve = false;

        #endregion

        #region ˽�з���

        /// <summary>
        /// �����ӿ�ʵ��
        /// </summary>
        /// <returns></returns>
        private int CreatInstance()
        {
            
            if (this.matFeeAchieve != null)
            {
                return 1;
            }

            // {C0E249B0-DE20-4d56-9C1E-9952858E32F1}
            // ����ѳ�ʼ�������ٴ���
            if (!blnHasInitFeeAchieve)
            {
                this.matFeeAchieve = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Material.IMatFee)) as FS.HISFC.BizProcess.Interface.Material.IMatFee;
                blnHasInitFeeAchieve = true;
            }

            if (this.matFeeAchieve == null)
            {
                return -1;
            }
            return 1;
        }

        #endregion

        #region �ӿ�ʵ��

        public void SetTrans(System.Data.IDbTransaction trans)
        {
            if (this.CreatInstance() < 0)
            {
                return;
            }
            this.matFeeAchieve.SetTrans(trans);
        }

        public int DBErrCode
        {
            get
            {
                if (this.matFeeAchieve == null)
                {
                    return 1;
                }
                return this.matFeeAchieve.DBErrCode;
            }
            set
            {
                if (this.matFeeAchieve == null)
                {
                    return;
                }
                this.matFeeAchieve.DBErrCode = value;
            }
        }

        public string Err
        {
            get
            {
                if (this.matFeeAchieve == null)
                {
                    return "";
                }
                return this.matFeeAchieve.Err;
            }
            set
            {
                if (this.matFeeAchieve == null)
                {
                    return;
                }
                this.matFeeAchieve.Err = value;
            }
        }

        public string ErrCode
        {
            get
            {
                if (this.matFeeAchieve == null)
                {
                    return "";
                }
                return this.matFeeAchieve.ErrCode;
            }
            set
            {
                if (this.matFeeAchieve == null)
                {
                    return;
                }
                this.matFeeAchieve.ErrCode = value;
            }
        }

        public int ApplyMaterialFeeBack(List<FS.HISFC.Models.FeeStuff.Output> outputList, bool isCancelApply)
        {
            if (this.CreatInstance() < 0)
            {
                return 1;
            }
            return this.matFeeAchieve.ApplyMaterialFeeBack(outputList, isCancelApply);
        }

        public FS.HISFC.Models.FeeStuff.MaterialItem GetMetItem(string itemCode)
        {
            if (this.CreatInstance() < 0)
            {
                return new FS.HISFC.Models.FeeStuff.MaterialItem();
            }
            return this.matFeeAchieve.GetMetItem(itemCode);
        }

        public FS.HISFC.Models.FeeStuff.Output GetOutput(string outNo, string stockNo)
        {
            if (this.CreatInstance() < 0)
            {
                return new FS.HISFC.Models.FeeStuff.Output();
            }
            return this.matFeeAchieve.GetOutput(outNo, stockNo);
        }

        public int MaterialFeeOutput(System.Collections.ArrayList feeItemLists)
        {
            if (this.CreatInstance() < 0)
            {
                return 1;
            }
            return this.matFeeAchieve.MaterialFeeOutput(feeItemLists);
        }

        public int MaterialFeeOutputBack(System.Collections.ArrayList feeitemLists)
        {
            if (this.CreatInstance() < 0)
            {
                return 1;
            }
            return this.matFeeAchieve.MaterialFeeOutputBack(feeitemLists);
        }

        public int MaterialFeeOutputBack(FS.HISFC.Models.FeeStuff.Output backOutput)
        {
            if (this.CreatInstance() < 0)
            {
                return 1;
            }
            return this.matFeeAchieve.MaterialFeeOutputBack(backOutput);
        }

        public int MaterialFeeOutputBack(List<FS.HISFC.Models.Fee.ReturnApplyMet> returnApplyList)
        {
            if (this.CreatInstance() < 0)
            {
                return 1;
            }
            return this.matFeeAchieve.MaterialFeeOutputBack(returnApplyList);
        }

        public int MaterialFeeOutputBack(List<FS.HISFC.Models.FeeStuff.Output> outputList)
        {
            if (this.CreatInstance() < 0)
            {
                return 1;
            }
            return this.matFeeAchieve.MaterialFeeOutputBack(outputList);
        }

        public int MaterialOutpubBack(string recipeNO, int sequenceNO, decimal backQty, System.Data.IDbTransaction trans, ref List<FS.HISFC.Models.FeeStuff.Output> backOutList)
        {
            if (this.CreatInstance() < 0)
            {
                return 1;
            }
            return this.matFeeAchieve.MaterialOutpubBack(recipeNO, sequenceNO, backQty, trans, ref backOutList);
        }

        public int MaterialOutput(FS.HISFC.Models.Fee.FeeItemBase feeItem, System.Data.IDbTransaction trans, ref bool isCompare, ref List<FS.HISFC.Models.FeeStuff.Output> outListCollect)
        {
            if (this.CreatInstance() < 0)
            {
                return 1;
            }
            return this.matFeeAchieve.MaterialOutput(feeItem, trans, ref isCompare, ref outListCollect);
        }

        public int OutputByStore(FS.HISFC.Models.FeeStuff.StoreDetail storeDetail, decimal outQty)
        {
            if (this.CreatInstance() < 0)
            {
                return 1;
            }
            return this.matFeeAchieve.OutputByStore(storeDetail, outQty);
        }

        public List<FS.HISFC.Models.FeeStuff.Output> QueryOutput(FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            if (this.CreatInstance() < 0)
            {
                return new List<FS.HISFC.Models.FeeStuff.Output>();
            }
            return this.matFeeAchieve.QueryOutput(feeItemList);
        }

        public List<FS.HISFC.Models.FeeStuff.Output> QueryOutput(string outNo)
        {
            if (this.CreatInstance() < 0)
            {
                return new List<FS.HISFC.Models.FeeStuff.Output>();
            }
            return this.matFeeAchieve.QueryOutput(outNo);
        }

        public List<FS.HISFC.Models.FeeStuff.Output> QueryOutput(string outNo, string itemCode)
        {
            if (this.CreatInstance() < 0)
            {
                return new List<FS.HISFC.Models.FeeStuff.Output>();
            }
            return this.matFeeAchieve.QueryOutput(outNo, itemCode);
        }

        public List<FS.HISFC.Models.FeeStuff.MaterialItem> QueryStockHeadItemForFee(string storeDeptCode)
        {
            if (this.CreatInstance() < 0)
            {
                return new List<FS.HISFC.Models.FeeStuff.MaterialItem>();
            }
            return this.matFeeAchieve.QueryStockHeadItemForFee(storeDeptCode);
        }

        public List<FS.HISFC.Models.FeeStuff.StoreDetail> QueryUnCompareMaterialStoreDetail(string storeDeptCode)
        {
            if (this.CreatInstance() < 0)
            {
                return new List<FS.HISFC.Models.FeeStuff.StoreDetail>();
            }
            return this.matFeeAchieve.QueryUnCompareMaterialStoreDetail(storeDeptCode);
        }

        public List<FS.HISFC.Models.FeeStuff.StoreHead> QueryUnCompareMaterialStoreHead(string storeDeptCode)
        {
            if (this.CreatInstance() < 0)
            {
                return new List<FS.HISFC.Models.FeeStuff.StoreHead>();
            }
            return this.matFeeAchieve.QueryUnCompareMaterialStoreHead(storeDeptCode);
        }

        public int UpdateFeeRecipe(List<FS.HISFC.Models.FeeStuff.Output> outListCollect, string recipeNO, int sequenceNO)
        {
            if (this.CreatInstance() < 0)
            {
                return 1;
            }
            return this.matFeeAchieve.UpdateFeeRecipe(outListCollect, recipeNO, sequenceNO);
        }

        #endregion

        #region IMatFee ��Ա

        int FS.HISFC.BizProcess.Interface.Material.IMatFee.ApplyMaterialFeeBack(List<FS.HISFC.Models.FeeStuff.Output> outputList, bool isCancelApply)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.Material.IMatFee.DBErrCode
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        string FS.HISFC.BizProcess.Interface.Material.IMatFee.Err
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        string FS.HISFC.BizProcess.Interface.Material.IMatFee.ErrCode
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        FS.HISFC.Models.FeeStuff.MaterialItem FS.HISFC.BizProcess.Interface.Material.IMatFee.GetMetItem(string itemCode)
        {
            throw new NotImplementedException();
        }

        FS.HISFC.Models.FeeStuff.Output FS.HISFC.BizProcess.Interface.Material.IMatFee.GetOutput(string outNo, string stockNo)
        {
            throw new NotImplementedException();
        }

        int FS.HISFC.BizProcess.Interface.Material.IMatFee.MaterialFeeOutput(System.Collections.ArrayList feeItemLists)
        {
            throw new NotImplementedException();
        }

        int FS.HISFC.BizProcess.Interface.Material.IMatFee.MaterialFeeOutputBack(System.Collections.ArrayList feeitemLists)
        {
            throw new NotImplementedException();
        }

        int FS.HISFC.BizProcess.Interface.Material.IMatFee.MaterialFeeOutputBack(FS.HISFC.Models.FeeStuff.Output backOutput)
        {
            throw new NotImplementedException();
        }

        int FS.HISFC.BizProcess.Interface.Material.IMatFee.MaterialFeeOutputBack(List<FS.HISFC.Models.Fee.ReturnApplyMet> returnApplyList)
        {
            throw new NotImplementedException();
        }

        int FS.HISFC.BizProcess.Interface.Material.IMatFee.MaterialFeeOutputBack(List<FS.HISFC.Models.FeeStuff.Output> outputList)
        {
            throw new NotImplementedException();
        }

        int FS.HISFC.BizProcess.Interface.Material.IMatFee.MaterialOutpubBack(string recipeNO, int sequenceNO, decimal backQty, System.Data.IDbTransaction trans, ref List<FS.HISFC.Models.FeeStuff.Output> backOutList)
        {
            throw new NotImplementedException();
        }

        int FS.HISFC.BizProcess.Interface.Material.IMatFee.MaterialOutput(FS.HISFC.Models.Fee.FeeItemBase feeItem, System.Data.IDbTransaction trans, ref bool isCompare, ref List<FS.HISFC.Models.FeeStuff.Output> outListCollect)
        {
            throw new NotImplementedException();
        }

        int FS.HISFC.BizProcess.Interface.Material.IMatFee.OutputByStore(FS.HISFC.Models.FeeStuff.StoreDetail storeDetail, decimal outQty)
        {
            throw new NotImplementedException();
        }

        List<FS.HISFC.Models.FeeStuff.Output> FS.HISFC.BizProcess.Interface.Material.IMatFee.QueryOutput(FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            throw new NotImplementedException();
        }

        List<FS.HISFC.Models.FeeStuff.Output> FS.HISFC.BizProcess.Interface.Material.IMatFee.QueryOutput(string outNo)
        {
            throw new NotImplementedException();
        }

        List<FS.HISFC.Models.FeeStuff.Output> FS.HISFC.BizProcess.Interface.Material.IMatFee.QueryOutput(string outNo, string itemCode)
        {
            throw new NotImplementedException();
        }

        List<FS.HISFC.Models.FeeStuff.MaterialItem> FS.HISFC.BizProcess.Interface.Material.IMatFee.QueryStockHeadItemForFee(string storeDeptCode)
        {
            throw new NotImplementedException();
        }

        List<FS.HISFC.Models.FeeStuff.StoreDetail> FS.HISFC.BizProcess.Interface.Material.IMatFee.QueryUnCompareMaterialStoreDetail(string storeDeptCode)
        {
            throw new NotImplementedException();
        }

        List<FS.HISFC.Models.FeeStuff.StoreHead> FS.HISFC.BizProcess.Interface.Material.IMatFee.QueryUnCompareMaterialStoreHead(string storeDeptCode)
        {
            throw new NotImplementedException();
        }

        void FS.HISFC.BizProcess.Interface.Material.IMatFee.SetTrans(System.Data.IDbTransaction trans)
        {
            throw new NotImplementedException();
        }

        int FS.HISFC.BizProcess.Interface.Material.IMatFee.UpdateFeeRecipe(List<FS.HISFC.Models.FeeStuff.Output> outListCollect, string recipeNO, int sequenceNO)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
