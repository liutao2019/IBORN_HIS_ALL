using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.HISFC.BizProcess.Integrate.Material
{
    /// <summary>
    /// [��������: �������ҵ����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-10]<br></br>
    /// </summary>
    public class Material1 : IntegrateBase, FS.HISFC.BizProcess.Interface.Material.IMatFee
    {
        static Material1()
        {

        }

        #region ��̬��

        /// <summary>
        /// ���ҵ�������
        /// </summary>
        //protected FS.HISFC.BizLogic.Material.Store storeManager = new FS.HISFC.BizLogic.Material.Store();

        /// <summary>
        /// ������Ŀ������
        /// </summary>
        //protected FS.HISFC.BizLogic.Material.MetItem itemManager = new FS.HISFC.BizLogic.Material.MetItem();

        /// <summary>
        /// Ȩ�޹�����
        /// </summary>
        FS.HISFC.BizLogic.Manager.PowerLevelManager powerLevelManager = new FS.HISFC.BizLogic.Manager.PowerLevelManager();

        #endregion

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="trans"></param>
        public override void SetTrans(System.Data.IDbTransaction trans)
        {
            //{6F1AD0FE-B6EE-446a-85B6-CEE1BC22C55D} integrate�������ʲ���
            //storeManager.SetTrans(trans);
            //itemManager.SetTrans(trans);

            this.trans = trans;
        }

        /// <summary>
        /// ���ʿۿ�
        /// </summary>
        /// <param name="feeItem"></param>
        /// <param name="isCompare"></param>
        /// <param name="outList"></param>
        /// <returns></returns>
        public int MaterialOutput(FS.HISFC.Models.Fee.FeeItemBase feeItem, System.Data.IDbTransaction trans, ref bool isCompare, ref List<FS.HISFC.Models.FeeStuff.Output> outListCollect)
        {
            return 1;
            /*{6F1AD0FE-B6EE-446a-85B6-CEE1BC22C55D} integrate�������ʲ���
            if (trans != null)
            {
                this.SetTrans(trans);
            }

            #region ���ʳ�����Ŀ����(������Ŀ/������Ŀ) ����ж�

            List<FS.HISFC.Models.FeeStuff.MaterialItem> compareMaterialCollect = null;
            if (feeItem.Item.IsMaterial)            //������ĿΪ������Ŀ ֱ�Ӹ���������Ŀ��ɿۿ�
            {
                compareMaterialCollect = new List<FS.HISFC.Models.FeeStuff.MaterialItem>();
                compareMaterialCollect.Add(feeItem.Item as FS.HISFC.Models.FeeStuff.MaterialItem);
                isCompare = false;
            }
            else                                   //������ĿΪ��ҩƷ��Ŀ ���ݶ���������Ŀ��ɿۿ�
            {
                compareMaterialCollect = itemManager.QueryCompareMaterial(feeItem.Item.ID);
                if (compareMaterialCollect == null)
                {
                    this.Err = itemManager.Err;
                    return -1;
                }
                if (compareMaterialCollect.Count == 0)      //��ҩƷ��Ŀ��δ���ж��� ��ֱ�ӷ���
                {
                    isCompare = false;
                    return 1;
                }
                isCompare = true;
            }

            #endregion

            decimal totQty = feeItem.Item.Qty;

            foreach (FS.HISFC.Models.FeeStuff.MaterialItem info in compareMaterialCollect)
            {
                if (totQty <= 0)
                {
                    break;
                }

                #region ��������Ŀ����ж�

                decimal qty;
                if (storeManager.GetStoreQty(feeItem.ExecOper.Dept.ID, feeItem.Item.ID, out qty) == -1)
                {
                    return -1;
                }
                if (qty <= 0)
                {
                    continue;
                }

                #endregion

                decimal outQty = totQty > qty ? qty : totQty;       //����ʵ�ʳ�����

                #region �γɳ���ʵ����Ϣ

                FS.HISFC.Models.FeeStuff.Output output = new FS.HISFC.Models.FeeStuff.Output();
                output.StoreBase.Item = info;
                output.StoreBase.PrivType = "Z1";
                output.StoreBase.SystemType = "Z1";
                output.StoreBase.StockDept = feeItem.ExecOper.Dept;
                output.StoreBase.TargetDept = feeItem.ExecOper.Dept;

                output.StoreBase.Quantity = outQty;
                output.StoreBase.Operation.Oper = feeItem.FeeOper;
                output.StoreBase.Operation.ExamOper = feeItem.FeeOper;
                output.GetPerson.ID = feeItem.Patient.ID;

                output.StoreBase.State = "2";
                #endregion

                #region ���ʳ��⴦��

                List<FS.HISFC.Models.FeeStuff.Output> tempOutList = new List<FS.HISFC.Models.FeeStuff.Output>();
                if (storeManager.Output(output, ref tempOutList) == -1)
                {
                    this.Err = storeManager.Err;
                    return -1;
                }
                //ʵ�ʳ�����Ϣ���� 
                outListCollect.AddRange(tempOutList);
                #endregion

                totQty = totQty - outQty;
            }

            if (totQty > 0)
            {
                this.Err = "������Ŀ��治��";
                return -1;
            }

            return 1;
            */
        }

        /// <summary>
        /// ������ʳ����¼ ������Ӧ�շѼ�¼�����š���������Ŀ��ˮ��
        /// </summary>
        /// <param name="outListCollect"></param>
        /// <param name="recipeNO"></param>
        /// <param name="sequenceNO"></param>
        /// <returns></returns>
        public int UpdateFeeRecipe(List<FS.HISFC.Models.FeeStuff.Output> outListCollect, string recipeNO, int sequenceNO)
        {
            return 1;
            //{6F1AD0FE-B6EE-446a-85B6-CEE1BC22C55D} integrate�������ʲ���
            //this.SetDB(storeManager);

            //return UpdateFeeRecipe(outListCollect, recipeNO, sequenceNO);
        }

        /// <summary>
        /// �����˿�
        /// </summary>
        /// <param name="recipeNO"></param>
        /// <param name="sequenceNO"></param>
        /// <param name="backQty"></param>
        /// <param name="backOutList"></param>
        /// <returns></returns>
        public int MaterialOutpubBack(string recipeNO, int sequenceNO, decimal backQty, System.Data.IDbTransaction trans, ref List<FS.HISFC.Models.FeeStuff.Output> backOutList)
        {
            return 1;
            /*{6F1AD0FE-B6EE-446a-85B6-CEE1BC22C55D} integrate�������ʲ���
            if (trans != null)
            {
                this.SetTrans(trans);
            }
            this.SetDB(storeManager);
            //���ݴ����š���Ŀ����ˮ�� ��ȡ���г����¼
            List<FS.HISFC.Models.FeeStuff.Output> outList = storeManager.QueryOutList(recipeNO, sequenceNO);
            if (outList == null)
            {
                return -1;
            }
            DateTime sysTime = storeManager.GetDateTimeFromSysDateTime();

            //���ݳ����¼��ϸ�����˿⴦��
            foreach (FS.HISFC.Models.FeeStuff.Output output in outList)
            {
                if (backQty <= 0)
                {
                    break;
                }

                //��ȡ����Ӧ�˿���
                decimal tempBackQty = backQty > output.StoreBase.Quantity ? output.StoreBase.Quantity : backQty;

                output.StoreBase.PrivType = "Z2";
                output.StoreBase.SystemType = "Z2";
                output.StoreBase.State = "2";
                output.StoreBase.Quantity = -tempBackQty;
                output.StoreBase.Operation.ExamQty = output.StoreBase.Quantity;
                output.StoreBase.Operation.ExamOper.ID = storeManager.Operator.ID;
                output.StoreBase.Operation.ExamOper.OperTime = sysTime;
                output.StoreBase.Operation.Oper = output.StoreBase.Operation.ExamOper;

                List<FS.HISFC.Models.FeeStuff.Output> tempBackOutList = new List<FS.HISFC.Models.FeeStuff.Output>();
                if (storeManager.OutputBack(output.Clone(), output.ID, output.StoreBase.SerialNO, ref tempBackOutList) == -1)
                {
                    return -1;
                }
                backOutList.AddRange(tempBackOutList);

                backQty = backQty - tempBackQty;
            }

            if (backQty > 0)
            {
                this.Err = "���˿���������ԭ�����������޷���ȷ����˿�";
                return -1;
            }
            return 1;
           */
        }

        /// <summary>
        /// ���ݿ����ұ����ȡ����δ����������Ŀ�����ϸ
        /// </summary>
        /// <param name="storeDeptCode"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.FeeStuff.StoreDetail> QueryUnCompareMaterialStoreDetail(string storeDeptCode)
        {
            return new List<FS.HISFC.Models.FeeStuff.StoreDetail>();
            //{6F1AD0FE-B6EE-446a-85B6-CEE1BC22C55D} integrate�������ʲ���
            //this.SetDB(storeManager);

            //return storeManager.QueryUnCompareMaterialStoreDetail(storeDeptCode);
        }

        /// <summary>
        /// ���ݿ����ұ����ȡ����δ����������Ŀ������
        /// </summary>
        /// <param name="storeDeptCode"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.FeeStuff.StoreHead> QueryUnCompareMaterialStoreHead(string storeDeptCode)
        {
            return new List<FS.HISFC.Models.FeeStuff.StoreHead>();
            //{6F1AD0FE-B6EE-446a-85B6-CEE1BC22C55D} integrate�������ʲ���
            //this.SetDB(storeManager);

            //return storeManager.QueryUnCompareMaterialStoreHead(storeDeptCode);
        }

        /// <summary>
        /// ͨ��������Ŀ��Ų�ѯ������Ŀ��Ϣ
        /// </summary>
        /// <param name="itemCode">������Ŀ����</param>
        /// <returns>������Ŀʵ��</returns>
        public FS.HISFC.Models.FeeStuff.MaterialItem GetMetItem(string itemCode)
        {
            //{6F1AD0FE-B6EE-446a-85B6-CEE1BC22C55D}
            return new FS.HISFC.Models.FeeStuff.MaterialItem();
            //this.SetDB(this.itemManager);
            //return this.itemManager.GetMetItemByMetID(itemCode);
        }

        ///// <summary>
        ///// ���ݿ�沿�����Ŀ����ȡ��������Ϣ
        ///// ������Ŀ��Ϣ,�������Ѷ��պͲ��շѵ�������Ŀ
        ///// </summary>
        ///// <param name="storeDeptCode">��沿��</param>
        ///// <returns>�ɹ����ؿ���������� ʧ�ܷ���null</returns>
        //public List<FS.HISFC.Models.Material.StoreHead> QueryStockHeadItemForFee(string storeDeptCode)
        //{
        //    this.SetDB(this.storeManager);
        //    this.SetDB(this.itemManager);
        //    List<FS.HISFC.Models.Material.StoreHead> storeHeadList = this.storeManager.QueryStockHead(storeDeptCode, "1", false);
        //    if (storeHeadList == null)
        //    {
        //        this.Err = this.storeManager.Err;
        //        return null;
        //    }
        //    foreach (FS.HISFC.Models.Material.StoreHead storeHead in storeHeadList)
        //    {
        //        storeHead.StoreBase.Item = this.itemManager.GetMetItemByMetID(storeHead.StoreBase.Item.ID);
        //        if (storeHead.StoreBase.Item == null)
        //        {
        //            this.Err = this.itemManager.Err;
        //            return null;
        //        }
        //    }
        //    return storeHeadList;
        //}

        #region ����

        /// <summary>
        /// ��ȡ�����¼
        /// </summary>
        /// <param name="outNo">������ˮ��</param>
        /// <param name="itemCode">���ʱ���</param>
        /// <returns>�����¼�б�</returns>
        public List<FS.HISFC.Models.FeeStuff.Output> QueryOutput(string outNo, string itemCode)
        {
            return new List<FS.HISFC.Models.FeeStuff.Output>();
            //{6F1AD0FE-B6EE-446a-85B6-CEE1BC22C55D} integrate�������ʲ���
            //this.SetDB(this.storeManager);
            //List<FS.HISFC.Models.Material.Output> outputAll = this.storeManager.QueryOutputDetailByID(outNo);
            //if (outputAll == null)
            //{
            //    this.Err = this.storeManager.Err;
            //    return null;
            //}
            //List<FS.HISFC.Models.Material.Output> outputList = new List<FS.HISFC.Models.Material.Output>();
            //foreach (FS.HISFC.Models.Material.Output tmpOutput in outputList)
            //{
            //    if (tmpOutput.StoreBase.Item.ID == itemCode)
            //    {
            //        outputList.Add(tmpOutput);
            //    }
            //}
            //return outputList;
        }

        /// <summary>
        /// ��ȡ�����¼
        /// </summary>
        /// <param name="outNo">������ˮ��</param>
        /// <param name="stockNo">������</param>
        /// <returns>�����¼</returns>
        public FS.HISFC.Models.FeeStuff.Output GetOutput(string outNo, string stockNo)
        {
            return new FS.HISFC.Models.FeeStuff.Output();
            //{6F1AD0FE-B6EE-446a-85B6-CEE1BC22C55D} integrate�������ʲ���
            //this.SetDB(this.storeManager);
            //return this.storeManager.GetOutputByOutNoAndStockNo(outNo, stockNo);
        }

        /// <summary>
        /// ��ȡ�����¼
        /// </summary>
        /// <param name="outNo">������ˮ��</param>
        /// <returns>�����¼�б�</returns>
        public List<FS.HISFC.Models.FeeStuff.Output> QueryOutput(string outNo)
        {
            return new List<FS.HISFC.Models.FeeStuff.Output>();
            //{6F1AD0FE-B6EE-446a-85B6-CEE1BC22C55D} integrate�������ʲ���
            //this.SetDB(this.storeManager);
            //List<FS.HISFC.Models.Material.Output> outputAll = this.storeManager.QueryOutputDetailByID(outNo);
            //if (outputAll == null)
            //{
            //    this.Err = this.storeManager.Err;
            //    return null;
            //}
            //foreach (FS.HISFC.Models.Material.Output output in outputAll)
            //{
            //    output.StoreBase.Item.Price = output.StoreBase.PriceCollection.RetailPrice;
            //}
            //return outputAll;
        }

        /// <summary>
        /// ��ȡ�����¼
        /// </summary>
        /// <param name="feeItemList">�շ���Ŀ</param>
        /// <returns>�����¼�б�</returns>
        public List<FS.HISFC.Models.FeeStuff.Output> QueryOutput(FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            if (feeItemList.Item.ItemType != EnumItemType.Drug)
            {
                if (!string.IsNullOrEmpty(feeItemList.UpdateSequence.ToString()) && feeItemList.UpdateSequence != 0)
                {
                    return QueryOutput(feeItemList.UpdateSequence.ToString());
                }
            }
            return null;
        }

        #endregion

        #region ���ʷ���

        /// <summary>
        /// �����շ�
        /// </summary>
        /// <param name="feeItemLists">�շ���Ŀ�б�</param>
        /// <returns>�ɹ���1��ʧ�ܣ�-1</returns>
        public int MaterialFeeOutput(ArrayList feeItemLists)
        {
            return 1;
            /*{6F1AD0FE-B6EE-446a-85B6-CEE1BC22C55D} integrate�������ʲ���
            this.SetDB(this.storeManager);
            this.SetDB(this.itemManager);
            this.SetDB(this.powerLevelManager);
            //�շ���Ŀѭ��
            //.Inpatient.FeeItemList
            foreach (FS.HISFC.Models.Fee.FeeItemBase feeItemList in feeItemLists)
            {
                #region ��ҩƷ
                if (feeItemList.Item.ItemType == EnumItemType.UnDrug)
                {
                    #region ��ȡ��ҩƷ���ʶ�����Ϣ
                    List<FS.HISFC.Models.Material.MaterialItem> matList = this.itemManager.QueryUndrugMatCompare(feeItemList.Item.ID);

                    if (matList == null)
                    {
                        this.Err = this.itemManager.Err;
                        return -1;
                    }

                    #endregion

                    #region ���ʳ���
                    if (matList.Count > 0)//�ж���
                    {

                        #region �ж������Ƿ�ȫ����ͣ��
                        bool hasValid = false;
                        foreach (FS.HISFC.Models.Material.MaterialItem tmpItem in matList)
                        {
                            if (tmpItem.ValidState)
                            {
                                hasValid = true;
                                break;
                            }
                        }
                        if (!hasValid)
                        {
                            this.Err = " " + feeItemList.Item.Name + "����Ӧ����ȫ���ѱ�ͣ��";
                            return -1;
                        }
                        #endregion

                        decimal leftQty = feeItemList.Item.Qty;
                        //��Ÿôζ�����Ҫ�ۿ������
                        List<FS.HISFC.Models.Material.MaterialItem> alFeeMat = new List<FS.HISFC.Models.Material.MaterialItem>();
                        //���ճ��������ʽ��пۿ�
                        foreach (FS.HISFC.Models.Material.MaterialItem matItem in matList)
                        {
                            //decimal totQty = this.storeManager.GetStoreQty(feeItemList.ExecOper.Dept.ID, matItem.ID);
                            //�ۿ����
                            decimal totQty = this.storeManager.GetStoreQty(feeItemList.StockOper.Dept.ID, matItem.ID);
                            //ȡ�������
                            if (totQty == -1)
                            {
                                this.Err = this.storeManager.Err;
                                return -1;
                            }
                            if (totQty >= leftQty)//�����湻
                            {
                                matItem.Qty = leftQty;
                                leftQty = 0;
                            }
                            else
                            {
                                matItem.Qty = totQty;
                                leftQty -= totQty;
                            }
                            alFeeMat.Add(matItem);
                        }
                        if (leftQty > 0)
                        {
                            this.Err = " " + feeItemList.Item.Name + "����Ӧ���ʿ�治��";
                            return -1;
                        }
                        string outNo = this.storeManager.GetNewOutputID();
                        this.serialNO = 0;
                        //���ó��⺯�������г������
                        if (this.OutputItemListForFee(alFeeMat, feeItemList.StockOper.Dept.ID, outNo) == -1)
                        {
                            return -1;
                        }
                        FS.HISFC.Models.Material.Output output;
                        feeItemList.UpdateSequence = FS.FrameWork.Function.NConvert.ToInt32(outNo);
                    }
                    #endregion
                }
                #endregion

                #region ����
                else if (feeItemList.Item.ItemType == EnumItemType.MatItem)
                {
                    #region �ж������Ƿ�ͣ��
                    FS.HISFC.Models.Material.MaterialItem matItem = this.itemManager.GetMetItemByMetID(feeItemList.Item.ID);
                    if (!matItem.ValidState)
                    {
                        this.Err = " �������ѱ�ͣ��";
                        return -1;
                    }
                    #endregion

                    #region �жϿ��
                    decimal totQty = this.storeManager.GetStoreQty(feeItemList.StockOper.Dept.ID, feeItemList.Item.ID, feeItemList.Item.Price);
                    if (totQty == -1)
                    {
                        this.Err = this.storeManager.Err;
                        return -1;
                    }
                    if (feeItemList.Item.Qty > totQty)
                    {
                        this.Err = " ����Ϊ��" + feeItemList.Item.Price + "������" + feeItemList.Item.Name + "��治��";
                        return -1;
                    }
                    #endregion

                    #region ���ʳ���
                    string outNo = this.storeManager.GetNewOutputID();
                    this.serialNO = 0;
                    FS.HISFC.Models.Material.StoreHead storeHead = this.storeManager.GetStoreHead(feeItemList.StockOper.Dept.ID, feeItemList.Item.ID, feeItemList.Item.Price);
                    if (storeHead == null)
                    {
                        this.Err = this.storeManager.Err;
                        return -1;
                    }
                    //���ó��⺯�������г������
                    if (this.OutputHeadForFee(storeHead, feeItemList.Item.Qty, outNo) == -1)
                    {
                        return -1;
                    }
                    feeItemList.UpdateSequence = FrameWork.Function.NConvert.ToInt32(outNo);
                    #endregion
                }
                #endregion
            }
            return 1;
            */
        }

        /// <summary>
        /// �����˷�ȷ��
        /// </summary>
        /// <param name="returnApplyList">�շ���Ŀ�����б�</param>
        /// <returns>�ɹ���1��ʧ�ܣ�-1</returns>
        public int MaterialFeeOutputBack(List<FS.HISFC.Models.Fee.ReturnApplyMet> returnApplyList)
        {
            //{6F1AD0FE-B6EE-446a-85B6-CEE1BC22C55D} integrate�������ʲ���
            //this.SetDB(this.storeManager);
            //this.SetDB(this.itemManager);
            //this.SetDB(this.powerLevelManager);
            ////�շ���Ŀѭ��
            //foreach (FS.HISFC.Models.Fee.ReturnApplyMet returnApply in returnApplyList)
            //{
            //    if (this.MaterialFeeOutputBack(returnApply) == -1)
            //    {
            //        return -1;
            //    }
            //}
            return 1;
        }

        /// <summary>
        /// �����˷�ȷ��
        /// </summary>
        /// <param name="outputList">�շ���Ŀ�����б�</param>
        /// <returns>�ɹ���1��ʧ�ܣ�-1</returns>
        public int MaterialFeeOutputBack(List<FS.HISFC.Models.FeeStuff.Output> outputList)
        {
            //{6F1AD0FE-B6EE-446a-85B6-CEE1BC22C55D} integrate�������ʲ���
            //this.SetDB(this.storeManager);
            //this.SetDB(this.itemManager);
            //this.SetDB(this.powerLevelManager);
            ////�շ���Ŀѭ��
            //foreach (FS.HISFC.Models.Material.Output output in outputList)
            //{
            //    if (this.MaterialFeeOutputBack(output) == -1)
            //    {
            //        return -1;
            //    }
            //}
            return 1;
        }

        /// <summary>
        /// �����˷�ȷ��
        /// </summary>
        /// <param name="outputList">�շ���Ŀ�����б�</param>
        /// <returns>�ɹ���1��ʧ�ܣ�-1</returns>
        public int MaterialFeeOutputBack(ArrayList feeitemLists)
        {
            return 1;
            /*{6F1AD0FE-B6EE-446a-85B6-CEE1BC22C55D} integrate�������ʲ���
            this.SetDB(this.storeManager);
            this.SetDB(this.itemManager);
            this.SetDB(this.powerLevelManager);
            //�շ���Ŀѭ��
            foreach (FS.HISFC.Models.Fee.FeeItemBase feeitemList in feeitemLists)
            {
                //{DA6C8ECB-B997-4ea1-A550-62BCDAA5645A} ��HIS453���ϣ���ֱ���շѵ�����
                #region ����
                if (feeitemList.Item.ItemType == EnumItemType.MatItem)
                {
                    //���ҿ����ϸ
                    List<FS.HISFC.Models.FeeStuff.Output> outputList = this.storeManager.QueryOutputDetailByID(feeitemList.UpdateSequence.ToString());
                    //�����ϸ����
                    OutputSortByStockNo outputSort = new OutputSortByStockNo();
                    outputList.Sort(outputSort);
                    //ʣ���˿�����������ѭ���𽥼���
                    decimal backQtyLeft = feeitemList.Item.Qty;
                    //ѭ���˿�
                    foreach (FS.HISFC.Models.Material.Output output in outputList)
                    {
                        //���˵���
                        if (output.StoreBase.Quantity - output.StoreBase.Returns >= backQtyLeft)
                        {
                            //output.StoreBase.Returns += backQtyLeft;
                            output.StoreBase.Quantity = -backQtyLeft;

                            backQtyLeft = 0;
                            string origOutputID = output.ID;
                            output.ID = this.storeManager.GetNewOutputID();
                            if (this.storeManager.OutputBack(output, origOutputID, false) == -1)
                            {
                                this.Err = this.storeManager.Err;
                                return -1;
                            }
                            break;
                        }
                        //������
                        else
                        {
                            //output.StoreBase.Returns = output.StoreBase.Quantity;
                            backQtyLeft = backQtyLeft - (output.StoreBase.Quantity - output.StoreBase.Returns);
                            output.StoreBase.Quantity = -(output.StoreBase.Quantity - output.StoreBase.Returns);
                            string origOutputID = output.ID;
                            output.ID = this.storeManager.GetNewOutputID();
                            if (this.storeManager.OutputBack(output, origOutputID, false) == -1)
                            {
                                this.Err = this.storeManager.Err;
                                return -1;
                            }
                        }
                    }
                    if (backQtyLeft > 0)
                    {
                        this.Err = "�˿�[" + feeitemList.Item.Name + "(" + feeitemList.Item.ID + ")" + "]ʱʧ��:δ�ҵ��㹻�ĳ�������";
                        return -1;
                    }
                }
                #endregion
                #region ��ҩƷ
                else
                {
                    List<FS.HISFC.Models.Material.Output> outputList = feeitemList.MateList;
                    foreach (FS.HISFC.Models.Material.Output output in outputList)
                    {
                        if (this.MaterialFeeOutputBack(output) == -1)
                        {
                            return -1;
                        }
                    }
                }
                #endregion
            }
            return 1;
            */
        }

        /// <summary>
        /// �����˷�����
        /// </summary>
        /// <param name="outputList">�����¼�б�</param>
        /// <param name="isCancelApply">true:ȡ�����룻false:��������</param>
        /// <returns>�ɹ���1��ʧ�ܣ�-1</returns>
        public int ApplyMaterialFeeBack(List<FS.HISFC.Models.FeeStuff.Output> outputList, bool isCancelApply)
        {
            //{6F1AD0FE-B6EE-446a-85B6-CEE1BC22C55D} integrate�������ʲ���
            //this.SetDB(this.storeManager);
            //foreach (FS.HISFC.Models.Material.Output tmpOutput in outputList)
            //{
            //    FS.HISFC.Models.Material.Output output = this.storeManager.GetOutputDetailByID(tmpOutput.ID, tmpOutput.StoreBase.StockNO);
            //    if (isCancelApply)
            //    {
            //        output.ReturnApplyNum -= tmpOutput.ReturnApplyNum;
            //    }
            //    else
            //    {
            //        output.ReturnApplyNum += tmpOutput.ReturnApplyNum;
            //    }
            //    if (this.storeManager.UpdateOutput(output) == -1)
            //    {
            //        this.Err = this.storeManager.Err;
            //        return -1;
            //    }
            //}
            return 1;
        }

        /// <summary>
        /// ���ݿ�沿�����Ŀ����ȡ��������Ϣ
        /// ������Ŀ��Ϣ,�������Ѷ��պͲ��շѵ�������Ŀ
        /// </summary>
        /// <param name="storeDeptCode">��沿��</param>
        /// <returns>�ɹ����ؿ���������� ʧ�ܷ���null</returns>
        public List<FS.HISFC.Models.FeeStuff.MaterialItem> QueryStockHeadItemForFee(string storeDeptCode)
        {
            return new List<FS.HISFC.Models.FeeStuff.MaterialItem>();
            //{6F1AD0FE-B6EE-446a-85B6-CEE1BC22C55D} integrate�������ʲ���
            //this.SetDB(this.storeManager);
            //this.SetDB(this.itemManager);
            //List<FS.HISFC.Models.Material.MaterialItem> itemList = new List<FS.HISFC.Models.Material.MaterialItem>();
            //List<FS.HISFC.Models.Material.StoreHead> storeHeadList = this.storeManager.QueryStockHead(storeDeptCode, "1", false);
            //if (storeHeadList == null)
            //{
            //    this.Err = this.storeManager.Err;
            //    return null;
            //}
            //foreach (FS.HISFC.Models.Material.StoreHead storeHead in storeHeadList)
            //{
            //    FS.HISFC.Models.Material.MaterialItem metItem = this.itemManager.GetMetItemByMetID(storeHead.StoreBase.Item.ID);
            //    if (metItem == null)
            //    {
            //        this.Err = this.itemManager.Err;
            //        return null;
            //    }
            //    metItem.Price = storeHead.StoreBase.AvgSalePrice;
            //    //{8F86BB0D-9BB4-4c63-965D-969F1FD6D6B2} ҽ�����İ����� by gengxl
            //    metItem.ItemType = EnumItemType.MatItem;
            //    (metItem as FS.HISFC.Models.Base.Item).Specs = metItem.Specs;

            //    itemList.Add(metItem);
            //}
            //return itemList;
        }

        
        ///// <summary>
        ///// ���ʷ��ýӿ�
        ///// </summary>
        ///// <param name="feeItemLists">�շ���Ŀ�б�</param>
        ///// <param name="TransTypes">��������</param>
        ///// <returns>�ɹ���1��ʧ�ܣ�-1</returns>
        //public int MaterialFee(ArrayList feeItemLists, TransTypes transType)
        //{
        //    this.SetDB(this.storeManager);
        //    this.SetDB(this.itemManager);
        //    this.SetDB(this.powerLevelManager);
        //    if (transType == TransTypes.Positive)//�շ�
        //    {
        //        return this.MaterialFeeOutput(feeItemLists);
        //    }
        //    else//�˷�
        //    {
        //        return -1;
        //       // return this.MaterialFeeOutputBack(feeItemLists);
        //    }
        //}

        #region �˷�

        ///// <summary>
        ///// �����˷�
        ///// </summary>
        ///// <param name="feeItemList">�շ���Ŀ</param>
        ///// <returns>�ɹ���1��ʧ�ܣ�-1</returns>
        //private int MaterialFeeOutputBack(FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        //{
        //    #region ȡ����Ӧ�ĳ���ʵ��
        //    //List<FS.HISFC.Models.Material.Output> outputList = this.storeManager.QueryOutputDetailByID(feeItemList.UpdateSequence.ToString());
        //    //FS.HISFC.Models.Material.Output output = null;
        //    //foreach (FS.HISFC.Models.Material.Output tmpOut in outputList)
        //    //{
        //    //    if (tmpOut.StoreBase.Item.ID == feeItemList.Item.ID //��Ʒ����
        //    //        && tmpOut.StoreBase.PriceCollection.RetailPrice == feeItemList.Item.Price) //���ۼ�
        //    //    {
        //    //        output = tmpOut;
        //    //        break;
        //    //    }
        //    //}


        //    FS.HISFC.Models.Material.Output output = this.storeManager.GetOutputByOutNoAndStockNo(feeItemList.UpdateSequence.ToString(), feeItemList.StockNo);
        //    if (output == null)
        //    {
        //        this.Err = "δ�ҵ����ʳ����¼" + this.storeManager.Err;
        //        return -1;
        //    }
        //    #endregion
        //    //output.StoreBase.Returns = feeItemList.Item.Qty;
        //    output.StoreBase.Returns += feeItemList.Item.Qty;
        //    output.ReturnApplyNum -= feeItemList.Item.Qty;
        //    string origOutputID = output.ID;
        //    output.ID = this.storeManager.GetNewOutputID();
        //    if (this.storeManager.OutputBack(output, origOutputID, false) == -1)
        //    {
        //        this.Err = this.storeManager.Err;
        //        return -1;
        //    }

        //    return 1;
        //}

        /// <summary>
        /// �����˷�
        /// </summary>
        /// <param name="returnApply">�շ���Ŀ</param>
        /// <returns>�ɹ���1��ʧ�ܣ�-1</returns>
        private int MaterialFeeOutputBack(FS.HISFC.Models.Fee.ReturnApplyMet returnApply)
        {
            //{6F1AD0FE-B6EE-446a-85B6-CEE1BC22C55D} integrate�������ʲ���
            //#region ȡ����Ӧ�ĳ���ʵ��

            //FS.HISFC.Models.Material.Output output = this.storeManager.GetOutputByOutNoAndStockNo(returnApply.OutNo, returnApply.StockNo);
            //if (output == null)
            //{
            //    this.Err = "δ�ҵ����ʳ����¼" + this.storeManager.Err;
            //    return -1;
            //}
            //#endregion
            ////output.StoreBase.Returns = feeItemList.Item.Qty;
            ////output.StoreBase.Returns += returnApply.Item.Qty;
            //output.StoreBase.Quantity = -returnApply.Item.Qty;
            //if (output.ReturnApplyNum > 0)//�����������Ŀ
            //{
            //    output.ReturnApplyNum = -returnApply.Item.Qty;
            //}
            //string origOutputID = output.ID;
            //output.ID = this.storeManager.GetNewOutputID();
            //if (this.storeManager.OutputBack(output, origOutputID, false) == -1)
            //{
            //    this.Err = this.storeManager.Err;
            //    return -1;
            //}

            return 1;
        }

        /// <summary>
        /// �����˷�
        /// </summary>
        /// <param name="backOutput">�շ���Ŀ</param>
        /// <returns>�ɹ���1��ʧ�ܣ�-1</returns>
        public int MaterialFeeOutputBack(FS.HISFC.Models.FeeStuff.Output backOutput)
        {
            //{6F1AD0FE-B6EE-446a-85B6-CEE1BC22C55D} integrate�������ʲ���
            //FS.HISFC.Models.Material.Output output = this.storeManager.GetOutputByOutNoAndStockNo(backOutput.ID, backOutput.StoreBase.StockNO);
            //if (output == null)
            //{
            //    this.Err = "δ�ҵ����ʳ����¼" + this.storeManager.Err;
            //    return -1;
            //}
            ////output.StoreBase.Returns += backOutput.StoreBase.Item.Qty;
            //output.StoreBase.Quantity = -backOutput.StoreBase.Item.Qty;
            //if (output.ReturnApplyNum > 0)//�����������Ŀ
            //{
            //    output.ReturnApplyNum = -backOutput.StoreBase.Item.Qty;
            //}
            //string origOutputID = output.ID;
            //output.ID = this.storeManager.GetNewOutputID();
            //if (this.storeManager.OutputBack(output, origOutputID, false) == -1)
            //{
            //    this.Err = this.storeManager.Err;
            //    return -1;
            //}

            return 1;
        }

        #endregion �˷�

        #region �շ�

        private int serialNO = 0;

        /// <summary>
        /// ���⣬��Կ�����
        /// </summary>
        /// <param name="storeHead">������ʵ��</param>
        /// <param name="outQty">������Ŀ</param>
        /// <param name="outNo">������ˮ��</param>
        /// <returns>1 �ɹ� -1 ʧ��</returns>
        private int OutputHeadForFee(FS.HISFC.Models.FeeStuff.StoreHead storeHead, decimal outQty, string outNo)
        {
            //{6F1AD0FE-B6EE-446a-85B6-CEE1BC22C55D} integrate�������ʲ���
            //List<FS.HISFC.Models.Material.StoreDetail> storeDetailList = this.storeManager.QueryStoreList(storeHead.StoreBase.StockDept.ID, storeHead.StoreBase.Item.ID, storeHead.StoreBase.AvgSalePrice, true);
            //return OutputDetailForFee(outQty, outNo, storeDetailList);
            return 1;
        }

        /// <summary>
        /// ����,���������Ŀ
        /// </summary>
        /// <param name="itemID">�������ʱ���</param>
        /// <param name="outDeptCode">�������</param>
        /// <param name="outQty">������Ŀ</param>
        /// <param name="outNo">���ⵥ��ˮ��</param>
        /// <returns>1 �ɹ� -1 ʧ��</returns>
        private int OutputItemForFee(string itemID, string outDeptCode, decimal outQty, string outNo)
        {
            return 1;
            //{6F1AD0FE-B6EE-446a-85B6-CEE1BC22C55D} integrate�������ʲ���
            //List<FS.HISFC.Models.Material.StoreDetail> storeDetailList = this.storeManager.QueryStoreList(outDeptCode, itemID, true);
            //return OutputDetailForFee(outQty, outNo, storeDetailList);
        }

        /// <summary>
        /// ���⣬��Կ����ϸ
        /// </summary>
        /// <param name="outQty">������Ŀ</param>
        /// <param name="outNo">���ⵥ��ˮ��</param>
        /// <param name="storeDetailList">�����ϸʵ���б�</param>
        /// <returns>1 �ɹ� -1 ʧ��</returns>
        private int OutputDetailForFee(decimal outQty, string outNo, List<FS.HISFC.Models.FeeStuff.StoreDetail> storeDetailList)
        {
            decimal leftQty = outQty;
            foreach (FS.HISFC.Models.FeeStuff.StoreDetail storeDetail in storeDetailList)
            {
                if (storeDetail.StoreBase.StoreQty >= leftQty)
                {
                    if (this.OutputForFee(storeDetail, leftQty, outNo) == -1)
                    {
                        return -1;
                    }
                    leftQty = 0;
                }
                else
                {
                    if (this.OutputForFee(storeDetail, storeDetail.StoreBase.StoreQty, outNo) == -1)
                    {
                        return -1;
                    }
                    leftQty -= storeDetail.StoreBase.StoreQty;
                }
            }
            if (leftQty > 0)
            {
                this.Err = "��治��";
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ���⣬���������Ŀ
        /// </summary>
        /// <param name="metItemList">������Ŀ�б�</param>
        /// <param name="outDeptCode">�������</param>
        /// <param name="outNo">���ⵥ��ˮ��</param>
        /// <returns>1 �ɹ� -1 ʧ��</returns>
        private int OutputItemListForFee(List<FS.HISFC.Models.FeeStuff.MaterialItem> metItemList, string outDeptCode, string outNo)
        {
            foreach (FS.HISFC.Models.FeeStuff.MaterialItem metItem in metItemList)
            {
                if (this.OutputItemForFee(metItem.ID, outDeptCode, metItem.Qty, outNo) == -1)
                {
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// ����,������ʿ�����
        /// </summary>
        /// <param name="itemID">�������ʱ���</param>
        /// <param name="outDeptCode">�������</param>
        /// <param name="outQty">������Ŀ</param>
        /// <param name="salePrice">���ۼ�</param>
        /// <param name="outNo">���ⵥ��ˮ��</param>
        /// <returns>1 �ɹ� -1 ʧ��</returns>
        private int OutputForFee(FS.HISFC.Models.FeeStuff.StoreDetail storeDetail, decimal outQty, string outNo)
        {
            return 1;
            /*{6F1AD0FE-B6EE-446a-85B6-CEE1BC22C55D} integrate�������ʲ���
            #region ��������ʵ��

            #region ȡ����Ȩ��
            ArrayList alPriv = this.powerLevelManager.LoadLevel3ByLevel2("0520");
            string class3MeanCode = "26";
            string class3Code = string.Empty;
            foreach (FS.HISFC.Models.Admin.PowerLevelClass3 priv3Obj in alPriv)
            {
                if (priv3Obj.Class3MeaningCode == class3MeanCode)
                {
                    class3Code = priv3Obj.Class3Code;
                    break;
                }
            }
            #endregion

            string outListNO = this.storeManager.GetOutListNO(storeDetail.StoreBase.StockDept.ID);
            DateTime sysTime = this.storeManager.GetDateTimeFromSysDateTime();

            FS.HISFC.Models.Material.MaterialItem item = this.itemManager.GetMetItemByMetID(storeDetail.StoreBase.Item.ID);
            if (item == null)
            {
                this.Err = this.itemManager.Err;
                return -1;
            }

            FS.HISFC.Models.Material.Output output = new FS.HISFC.Models.Material.Output();
            //��Ʒ��Ϣ
            output.ID = outNo;
            output.SequenceNO = ++this.serialNO;
            output.OutListNO = outListNO;
            output.StoreBase = storeDetail.StoreBase;
            output.StoreBase.Item = item;
            output.StoreBase.PrivType = class3Code;
            output.StoreBase.SystemType = class3MeanCode;
            output.StoreBase.Quantity = outQty;
            output.OutCost = outQty * storeDetail.StoreBase.PriceCollection.PurchasePrice;
            output.IsPrivate = false;
            output.OutTime = sysTime;
            output.StoreBase.Operation.Oper.ID = this.storeManager.Operator.ID;
            output.StoreBase.Operation.Oper.OperTime = sysTime;
            output.StoreBase.State = "2";
            output.StoreBase.Returns = 0;
            #endregion

            #region ����

            if (this.storeManager.Output(output, null, true) == -1)
            {
                this.Err = this.storeManager.Err;
                return -1;
            }
            return 1;

            #endregion
            */
        }

        #endregion �շ�

        #endregion ���ʷ���

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="storeDetail">�����ϸ��Ϣ</param>
        /// <param name="outQty">������</param>
        /// <returns>1 �ɹ� -1 ʧ��</returns>
        public int OutputByStore(FS.HISFC.Models.FeeStuff.StoreDetail storeDetail, decimal outQty)
        {
            return 1;
            /*{6F1AD0FE-B6EE-446a-85B6-CEE1BC22C55D} integrate�������ʲ���
            #region ��������ʵ��

            #region ȡ����Ȩ��
            ArrayList alPriv = this.powerLevelManager.LoadLevel3ByLevel2("0520");
            string class3MeanCode = "26";
            string class3Code = string.Empty;
            foreach (FS.HISFC.Models.Admin.PowerLevelClass3 priv3Obj in alPriv)
            {
                if (priv3Obj.Class3MeaningCode == class3MeanCode)
                {
                    class3Code = priv3Obj.Class3Code;
                    break;
                }
            }
            #endregion

            string outListNO = this.storeManager.GetOutListNO(storeDetail.StoreBase.StockDept.ID);
            DateTime sysTime = this.storeManager.GetDateTimeFromSysDateTime();

            FS.HISFC.Models.Material.MaterialItem item = this.itemManager.GetMetItemByMetID(storeDetail.StoreBase.Item.ID);
            if (item == null)
            {
                this.Err = this.itemManager.Err;
                return -1;
            }

            FS.HISFC.Models.Material.Output output = new FS.HISFC.Models.Material.Output();
            //��Ʒ��Ϣ
            output.ID = null;
            output.SequenceNO = ++this.serialNO;
            output.OutListNO = outListNO;
            output.StoreBase = storeDetail.StoreBase;
            output.StoreBase.Item = item;
            output.StoreBase.PrivType = class3Code;
            output.StoreBase.SystemType = class3MeanCode;
            output.StoreBase.Quantity = outQty;
            output.OutCost = outQty * storeDetail.StoreBase.PriceCollection.PurchasePrice;
            output.IsPrivate = false;
            output.OutTime = sysTime;
            output.StoreBase.Operation.Oper.ID = this.storeManager.Operator.ID;
            output.StoreBase.Operation.Oper.OperTime = sysTime;
            output.StoreBase.State = "2";
            output.StoreBase.Returns = 0;
            #endregion

            #region ����

            if (this.storeManager.Output(output, null, true) == -1)
            {
                this.Err = this.storeManager.Err;
                return -1;
            }
            return 1;

            #endregion
            */
        }

    }

    
    /// <summary>
    /// ���ʵ�ArrayList���տ���������
    /// {DA6C8ECB-B997-4ea1-A550-62BCDAA5645A} ��HIS453���ϣ���ֱ���շѵ�����
    /// </summary>
    internal class OutputSortByStockNo : IComparer<FS.HISFC.Models.FeeStuff.Output>
    {
        #region IComparer<Output> ��Ա

        public int Compare(FS.HISFC.Models.FeeStuff.Output x, FS.HISFC.Models.FeeStuff.Output y)
        {
            return FrameWork.Function.NConvert.ToInt32(x.StoreBase.StockNO) - FrameWork.Function.NConvert.ToInt32(y.StoreBase.StockNO);
        }

        #endregion
    }
}
