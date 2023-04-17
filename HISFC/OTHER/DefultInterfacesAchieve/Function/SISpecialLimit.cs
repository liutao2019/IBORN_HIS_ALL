using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FS.FrameWork.Models;
using FS.HISFC.Models.Fee;
using FS.HISFC.Models.RADT;
using FS.FrameWork.Function;
using FS.HISFC.Models.Base;

namespace FS.DefultInterfacesAchieve.Function
{
    public class SISpecialLimit : FS.FrameWork.Management.Database
    {

        #region ˽�з���

        /// <summary>
        /// ���µ������
        /// </summary>
        /// <param name="sqlIndex">SQL�������</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�: >= 1 ʧ�� -1 û�и��µ����� 0</returns>
        private int UpdateSingleTable(string sqlIndex, params string[] args)
        {
            string sql = string.Empty;//Update���

            //���Where���
            if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + sqlIndex + "��SQL���";

                return -1;
            }

            return this.ExecNoQuery(sql, args);
        }

        /// <summary>
        /// ͨ��ҩƷʵ����ʵ����������
        /// </summary>
        /// <param name="patient">��Ա������Ϣ</param>
        /// <param name="medItemList">ҩƷ���û�����Ϣ</param>
        /// <returns>ҩƷʵ����ʵ����������</returns>
        private string[] GetMedItemListParams(PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList medItemList)
        {
            medItemList.Patient = patient.Clone();

            string[] args = 
				{
					medItemList.RecipeNO,
					medItemList.SequenceNO.ToString(),
					((int)medItemList.TransType).ToString(),
					medItemList.Patient.ID,
					medItemList.Patient.Name,
					medItemList.Patient.Pact.PayKind.ID,
					medItemList.Patient.Pact.ID,
					((FS.HISFC.Models.RADT.PatientInfo)medItemList.Patient).PVisit.PatientLocation.Dept.ID,
					((FS.HISFC.Models.RADT.PatientInfo)medItemList.Patient).PVisit.PatientLocation.NurseCell.ID,
					medItemList.RecipeOper.Dept.ID,
					medItemList.ExecOper.Dept.ID,
					medItemList.StockOper.Dept.ID,
					medItemList.RecipeOper.ID,
					medItemList.Item.ID,
					medItemList.Item.MinFee.ID,
					medItemList.Compare.CenterItem.ID,
					medItemList.Item.Name,
					medItemList.Item.Specs,
					((FS.HISFC.Models.Pharmacy.Item)medItemList.Item).Type.ID,
					((FS.HISFC.Models.Pharmacy.Item)medItemList.Item).Quality.ID,
					NConvert.ToInt32(((FS.HISFC.Models.Pharmacy.Item)medItemList.Item).Product.IsSelfMade).ToString(),
					medItemList.Item.Price.ToString(),
					medItemList.Item.PriceUnit,
					medItemList.Item.PackQty.ToString(),
					medItemList.Item.Qty.ToString(),
					medItemList.Days.ToString(),
					medItemList.FT.TotCost.ToString(),
					medItemList.FT.OwnCost.ToString(),
					medItemList.FT.PayCost.ToString(),
					medItemList.FT.PubCost.ToString(),
					medItemList.FT.RebateCost.ToString(),
                    ((int)medItemList.PayType).ToString(),
                    NConvert.ToInt32(medItemList.IsBaby).ToString(),
                    NConvert.ToInt32(medItemList.IsEmergency).ToString(),
                    ((FS.HISFC.Models.Order.Inpatient.Order)medItemList.Order).OrderType.ID,
					medItemList.Invoice.ID,
					medItemList.BalanceNO.ToString(),
					medItemList.ApproveNO,
					medItemList.ChargeOper.ID,
					medItemList.ChargeOper.OperTime.ToString(),
					medItemList.ExecOper.ID,
					medItemList.ExecOper.OperTime.ToString(),
					medItemList.AuditingNO,
					medItemList.Order.ID,
					medItemList.ExecOrder.ID,
					medItemList.FeeOper.ID,
                    medItemList.FeeOper.OperTime.ToString(),
                    medItemList.SendSequence.ToString(),
                    medItemList.UpdateSequence.ToString(),
					medItemList.NoBackQty.ToString(),
					medItemList.BalanceState,
					medItemList.FTRate.OwnRate.ToString(),	
					medItemList.FeeOper.Dept.ID,		
					medItemList.Item.SpecialFlag,	
					medItemList.Item.SpecialFlag1,
					medItemList.Item.SpecialFlag2,
                    medItemList.ExtCode,
                    medItemList.ExtOper.ID,
                    medItemList.ExtOper.OperTime.ToString(),
                    medItemList.Item.Memo
				};

            return args;
        }
        /// <summary>
        /// ���sql���������
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        protected string getOutpatOrderInfo(string sql, FS.HISFC.Models.Order.OutPatient.Order order)
        {
            #region sql
            //   0--������� ,1 --��Ŀ��ˮ��,2 --�����,3   --������ ,4    --�Һ�����
            //   5 --�Һſ���,6   --��Ŀ����,7   --��Ŀ����, 8  --���, 9  --1ҩƷ��2��ҩƷ
            //   10   --ϵͳ���,   --��С���ô���,   --����,   --��������,   --����
            //    --��װ����,   --�Ƽ۵�λ,   --�Էѽ��0,   --�Ը����0,   --�������0
            //   --��������,   --����ҩ,   --ҩƷ���ʣ���ҩ����ҩ,   --ÿ������
            //     --ÿ��������λ,   --���ʹ���,   --Ƶ��,   --Ƶ������,   --ʹ�÷���
            //     --�÷�����,   --�÷�Ӣ����д,   --ִ�п��Ҵ���,   --ִ�п�������
            //      --��ҩ��־,   --��Ϻ�,   --1����ҪƤ��/2��ҪƤ�ԣ�δ��/3Ƥ����/4Ƥ����
            //     --Ժ��ע�����,   --��ע,   --����ҽ��,   --����ҽ������,   --ҽ������
            //     --����ʱ��,   --����״̬,1������2�շѣ�3ȷ�ϣ�4����,   --������,   --����ʱ��
            //        --�Ӽ����0��ͨ/1�Ӽ�,   --��������,   --����,   --���뵥��
            //     --0���Ǹ���/1�Ǹ���,   --�Ƿ���Ҫȷ�ϣ�1��Ҫ��0����Ҫ,   --ȷ����
            //        --ȷ�Ͽ���,   --ȷ��ʱ��,   --0δ�շ�/1�շ�,   --�շ�Ա
            //       --�շ�ʱ��,   --������,    --��������ˮ��,     --��ҩҩ����    
            //      --������λ�Ƿ�����С��λ 1 �� 0 ���ǣ�      --ҽ�����ͣ�Ŀǰû�У�
            #endregion

            if (order.Item.ItemType == EnumItemType.Drug)//ҩƷ
            {
                FS.HISFC.Models.Pharmacy.Item pItem = order.Item as FS.HISFC.Models.Pharmacy.Item;
                System.Object[] s = {order.SeeNO ,FS.FrameWork.Function.NConvert.ToInt32(order.ID),order.Patient.ID,order.Patient.PID.CardNO,order.RegTime,
										order.InDept.ID,pItem.ID,pItem.Name,pItem.Specs,"1",
										order.Item.SysClass.ID,order.Item.MinFee.ID,order.Item.Price,order.Qty,order.HerbalQty,
										pItem.PackQty,pItem.PriceUnit,order.FT.OwnCost ,order.FT.PayCost,order.FT.PubCost,
										pItem.BaseDose,FS.FrameWork.Function.NConvert.ToInt32(pItem.Product.IsSelfMade),pItem.Quality.ID,order.DoseOnce,
										order.DoseUnit,pItem.DosageForm.ID,order.Frequency.ID,order.Frequency.Name,order.Usage.ID,
										order.Usage.Name,order.Usage.Memo,order.ExeDept.ID,order.ExeDept.Name,
										FS.FrameWork.Function.NConvert.ToInt32(order.Combo.IsMainDrug),order.Combo.ID,order.HypoTest,
										order.InjectCount,order.Memo,order.ReciptDoctor.ID,order.ReciptDoctor.Name,order.ReciptDept.ID,
										order.MOTime,order.Status,order.DCOper.ID,order.DCOper.OperTime,
										FS.FrameWork.Function.NConvert.ToInt32(order.IsEmergency),order.Sample.Name,order.CheckPartRecord,order.ID,
										FS.FrameWork.Function.NConvert.ToInt32(order.IsSubtbl),FS.FrameWork.Function.NConvert.ToInt32(order.IsNeedConfirm),order.ConfirmOper.ID,
										order.ConfirmOper.Dept.ID,order.ConfirmOper.OperTime,FS.FrameWork.Function.NConvert.ToInt32(order.IsHaveCharged),order.ChargeOper.ID,
										order.ChargeOper.OperTime,order.ReciptNO,order.SequenceNO,
                                        order.StockDept.ID,order.NurseStation.User03,"",
                                        order.NurseStation.User01,order.ExtendFlag1,
										order.ReciptSequence,order.NurseStation.Memo,order.SortID,order.Item.Memo};

                try
                {
                    string sReturn = string.Format(sql, s);
                    return sReturn;
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                    return null;
                }
            }
            else//��ҩƷ
            {
                FS.HISFC.Models.Fee.Item.Undrug pItem = order.Item as FS.HISFC.Models.Fee.Item.Undrug;
                System.Object[] s = {order.SeeNO,FS.FrameWork.Function.NConvert.ToInt32(order.ID),order.Patient.ID,order.Patient.PID.CardNO,order.RegTime,
										order.InDept.ID,pItem.ID,pItem.Name,pItem.Specs,"2",
										order.Item.SysClass.ID,order.Item.MinFee.ID,order.Item.Price,order.Qty,order.HerbalQty,
										pItem.PackQty,pItem.PriceUnit,order.FT.OwnCost ,order.FT.PayCost,order.FT.PubCost,
										"0",0,"",order.DoseOnce,
										order.DoseUnit,"",order.Frequency.ID,order.Frequency.Name,order.Usage.ID,
										order.Usage.Name,order.Usage.Memo,order.ExeDept.ID,order.ExeDept.Name,
										FS.FrameWork.Function.NConvert.ToInt32(order.Combo.IsMainDrug),order.Combo.ID,order.HypoTest,
										order.InjectCount,order.Memo,order.ReciptDoctor.ID,order.ReciptDoctor.Name,order.ReciptDept.ID,
										order.MOTime,order.Status,order.DCOper.ID,order.DCOper.OperTime,
										FS.FrameWork.Function.NConvert.ToInt32(order.IsEmergency),order.Sample.Name,order.CheckPartRecord,order.ID,
										FS.FrameWork.Function.NConvert.ToInt32(order.IsSubtbl),FS.FrameWork.Function.NConvert.ToInt32(order.IsNeedConfirm),order.ConfirmOper.ID,
										order.ConfirmOper.Dept.ID,order.ConfirmOper.OperTime,FS.FrameWork.Function.NConvert.ToInt32(order.IsHaveCharged),order.ChargeOper.ID,
										order.ChargeOper.OperTime,order.ReciptNO,order.SequenceNO,
                                        order.StockDept.ID,order.NurseStation.User03,"",
                                        order.NurseStation.User01,order.ExtendFlag1,
										order.ReciptSequence,order.NurseStation.Memo,order.SortID,order.Item.Memo};
                try
                {
                    string sReturn = string.Format(sql, s);
                    return sReturn;
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                    return null;
                }

            }

        }
        /// <summary>
        /// ���ҽ����Ϣ
        /// </summary>
        /// <param name="sqlOrder"></param>
        /// <param name="Order"></param>
        /// <returns></returns>
        private string getOrderInfo(string sqlOrder, FS.HISFC.Models.Order.Inpatient.Order Order)
        {
            #region "�ӿ�˵��"
            //0 IDҽ����ˮ��
            //������Ϣ����  
            //			1 סԺ��ˮ��   2סԺ������     3���߿���id      4���߻���id
            //ҽ��������Ϣ
            // ������Ŀ��Ϣ
            //	       5��Ŀ���      6��Ŀ����       7��Ŀ����      8��Ŀ������,    9��Ŀƴ���� 
            //	       10��Ŀ������ 11��Ŀ�������  12ҩƷ���     13ҩƷ��������  14������λ       
            //         15��С��λ     16��װ����,     17���ʹ���     18ҩƷ���  ,   19ҩƷ����
            //         20���ۼ�       21�÷�����      22�÷�����     23�÷�Ӣ����д  24Ƶ�δ���  
            //         25Ƶ������     26ÿ�μ���      27��Ŀ����     28�Ƽ۵�λ      29ʹ������			  
            // ����ҽ������
            //		   30ҽ�������� 31ҽ���������  32ҽ���Ƿ�ֽ�:1����/2��ʱ     33�Ƿ�Ʒ� 
            //		   34ҩ���Ƿ���ҩ 35��ӡִ�е�    36�Ƿ���Ҫȷ��  
            // ����ִ�����
            //		   37����ҽʦId   38����ҽʦname  39��ʼʱ��      40����ʱ��     41��������
            //		   42����ʱ��     43¼����Ա����  44¼����Ա����  45�����ID     46���ʱ��       
            //		   47DCԭ�����   48DCԭ������    49DCҽʦ����    50DCҽʦ����   51Dcʱ��
            //         52ִ����Ա���� 53ִ��ʱ��      54ִ�п��Ҵ���  55ִ�п������� 
            //		   56���ηֽ�ʱ�� 57�´ηֽ�ʱ��
            // ����ҽ������
            //		   58�Ƿ�Ӥ����1��/2��          59�������  	  60������     61��ҩ��� 
            //		   62�Ƿ񸽲�'1'  63�Ƿ��������  64ҽ��״̬      65�ۿ���     66ִ�б��1δִ��/2��ִ��/3DCִ�� 
            //		   67ҽ��˵��                     68�Ӽ����:1��ͨ/2�Ӽ�         69�������
            //         70��鲿λ��ע                 71��ע          72����,          73 ������������,
            //         74 ȡҩҩ������ 
            #endregion
            string strItemType = "";
            if (Order.CurMOTime == DateTime.MinValue)
            {
                Order.CurMOTime = Order.BeginTime;
            }
            if (Order.NextMOTime == DateTime.MinValue)
            {
                Order.NextMOTime = Order.BeginTime;
            }
            //�ж�ҩƷ/��ҩƷ

            if (Order.Item.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))
            {
                FS.HISFC.Models.Pharmacy.Item objPharmacy;
                objPharmacy = (FS.HISFC.Models.Pharmacy.Item)Order.Item;
                strItemType = "1";
                try
                {
                    System.Object[] s ={Order.ID,Order.Patient.ID,Order.Patient.PID.PatientNO,Order.Patient.PVisit.PatientLocation.Dept.ID,Order.Patient.PVisit.PatientLocation.NurseCell.ID,
										  strItemType,Order.Item.ID,Order.Item.Name,Order.Item.UserCode,Order.Item.SpellCode,
										  Order.Item.SysClass.ID.ToString(),Order.Item.SysClass.Name,objPharmacy.Specs,objPharmacy.BaseDose,objPharmacy.DoseUnit,objPharmacy.MinUnit,objPharmacy.PackQty,
										  objPharmacy.DosageForm.ID,objPharmacy.Type.ID,objPharmacy.Quality.ID,objPharmacy.PriceCollection.RetailPrice,
										  Order.Usage.ID,Order.Usage.Name,Order.Usage.Memo,Order.Frequency.ID,Order.Frequency.Name,
										  Order.DoseOnce,Order.Qty,Order.Unit,Order.HerbalQty.ToString(),
										  Order.OrderType.ID,Order.OrderType.Name,FS.FrameWork.Function.NConvert.ToInt32(Order.OrderType.IsDecompose),FS.FrameWork.Function.NConvert.ToInt32(Order.OrderType.IsCharge),
										  FS.FrameWork.Function.NConvert.ToInt32(Order.OrderType.IsNeedPharmacy),FS.FrameWork.Function.NConvert.ToInt32(Order.OrderType.IsPrint),FS.FrameWork.Function.NConvert.ToInt32(Order.OrderType.IsConfirm),
										  Order.ReciptDoctor.ID,Order.ReciptDoctor.Name,Order.BeginTime,Order.EndTime,Order.ReciptDept.ID,
										  Order.MOTime,Order.Oper.ID,Order.Oper.Name,Order.Nurse.ID,Order.ConfirmTime,
										  Order.DcReason.ID,Order.DcReason.Name,Order.DCOper.ID,Order.DCOper.Name,Order.DCOper.OperTime,
										  Order.ExecOper.ID,Order.ExecOper.OperTime,Order.ExeDept.ID,Order.ExeDept.Name,
										  Order.CurMOTime,Order.NextMOTime,
										  FS.FrameWork.Function.NConvert.ToInt32(Order.IsBaby),Order.BabyNO,Order.Combo.ID,FS.FrameWork.Function.NConvert.ToInt32(Order.Combo.IsMainDrug),
										  FS.FrameWork.Function.NConvert.ToInt32(Order.IsSubtbl),FS.FrameWork.Function.NConvert.ToInt32(Order.IsHaveSubtbl),Order.Status,FS.FrameWork.Function.NConvert.ToInt32(Order.IsStock),Order.ExecStatus,
										  Order.Note,FS.FrameWork.Function.NConvert.ToInt32(Order.IsEmergency),Order.SortID,Order.Memo,Order.CheckPartRecord,FS.FrameWork.Function.NConvert.ToInt32(Order.Reorder),Order.Sample.Name,Order.StockDept.ID,
										  objPharmacy.IsAllergy==true ?"2":"1",FS.FrameWork.Function.NConvert.ToInt32(Order.IsPermission),Order.Package.ID,Order.Package.Name,Order.ExtendFlag1,Order.ExtendFlag2,Order.ReTidyInfo,
                                          Order.Frequency.Time,Order.ExecDose,Order.Item.Memo};//�¼�����Ƶ��

                    string myErr = "";
                    if (FS.FrameWork.Public.String.CheckObject(out myErr, s) == -1)
                    {
                        this.Err = myErr;
                        this.WriteErr();
                        return null;
                    }
                    sqlOrder = string.Format(sqlOrder, s);
                }
                catch (Exception ex)
                {
                    this.Err = "����ֵʱ�����" + ex.Message;
                    this.WriteErr();
                    return null;
                }
            }
            else if (Order.Item.GetType() == typeof(FS.HISFC.Models.Fee.Item.Undrug))
            {
                FS.HISFC.Models.Fee.Item.Undrug objAssets;
                objAssets = (FS.HISFC.Models.Fee.Item.Undrug)Order.Item;
                strItemType = "2";

                try
                {
                    string[] s ={Order.ID,Order.Patient.ID,Order.Patient.PID.PatientNO,Order.Patient.PVisit.PatientLocation.Dept.ID,Order.Patient.PVisit.PatientLocation.NurseCell.ID,
								   strItemType,Order.Item.ID,Order.Item.Name,Order.Item.UserCode,Order.Item.SpellCode,
								   Order.Item.SysClass.ID.ToString(),Order.Item.SysClass.Name,objAssets.Specs,"0","","","0","","","",objAssets.Price.ToString(),
								   Order.Usage.ID,Order.Usage.Name,Order.Usage.Memo,Order.Frequency.ID,Order.Frequency.Name,
								   Order.DoseOnce.ToString(),Order.Qty.ToString(),Order.Unit,Order.HerbalQty.ToString(),
								   Order.OrderType.ID,Order.OrderType.Name,FS.FrameWork.Function.NConvert.ToInt32(Order.OrderType.IsDecompose).ToString(),FS.FrameWork.Function.NConvert.ToInt32(Order.OrderType.IsCharge).ToString(),
								   FS.FrameWork.Function.NConvert.ToInt32(Order.OrderType.IsNeedPharmacy).ToString(),FS.FrameWork.Function.NConvert.ToInt32(Order.OrderType.IsPrint).ToString(),FS.FrameWork.Function.NConvert.ToInt32(Order.OrderType.IsConfirm).ToString(),
								   Order.ReciptDoctor.ID,Order.ReciptDoctor.Name,Order.BeginTime.ToString(),Order.EndTime.ToString(),Order.ReciptDept.ID,
								   Order.MOTime.ToString(),Order.Oper.ID,Order.Oper.Name,Order.Nurse.ID,Order.ConfirmTime.ToString(),
								   Order.DcReason.ID,Order.DcReason.Name,Order.DCOper.ID,Order.DCOper.Name,Order.DCOper.OperTime.ToString(),
								   Order.ExecOper.ID,Order.ExecOper.OperTime.ToString(),Order.ExeDept.ID,Order.ExeDept.Name,
								   Order.CurMOTime.ToString(),Order.NextMOTime.ToString(),
								   FS.FrameWork.Function.NConvert.ToInt32(Order.IsBaby).ToString(),Order.BabyNO.ToString(),Order.Combo.ID,FS.FrameWork.Function.NConvert.ToInt32(Order.Combo.IsMainDrug).ToString(),
								   FS.FrameWork.Function.NConvert.ToInt32(Order.IsSubtbl).ToString(),FS.FrameWork.Function.NConvert.ToInt32(Order.IsHaveSubtbl).ToString(),Order.Status.ToString(),FS.FrameWork.Function.NConvert.ToInt32(Order.IsStock).ToString(),Order.ExecStatus.ToString(),
								   Order.Note,FS.FrameWork.Function.NConvert.ToInt32(Order.IsEmergency).ToString(),Order.SortID.ToString(),Order.Memo,Order.CheckPartRecord,FS.FrameWork.Function.NConvert.ToInt32(Order.Reorder).ToString(),Order.Sample.Name,Order.StockDept.ID,
								   "",FS.FrameWork.Function.NConvert.ToInt32(Order.IsPermission).ToString(),Order.Package.ID,Order.Package.Name,Order.ExtendFlag1,Order.ExtendFlag2,Order.ReTidyInfo,
                                   Order.Frequency.Time,Order.ExecDose,Order.Item.Memo};//�¼�����Ƶ��
                    sqlOrder = string.Format(sqlOrder, s);
                }
                catch (Exception ex)
                {
                    this.Err = "����ֵʱ�����" + ex.Message;
                    this.WriteErr();
                    return null;
                }
            }
            else
            {
                this.Err = "��Ŀ���ͳ���";
                return null;
            }
            return sqlOrder;
        }

        /// <summary>
        /// ���insert��Ĵ����������update
        /// </summary>
        /// <param name="feeItemList">������ϸʵ��</param>
        /// <returns>�ַ�������</returns>
        private string[] GetOutFeeItemListParams(FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList)
        {
            string[] args = new string[77];

            args[0] = feeItemList.RecipeNO;//RECIPE_NO,	--		������							0
            args[1] = feeItemList.SequenceNO.ToString();	  //SEQUENCE_NO;	--		��������Ŀ��ˮ��				1
            args[2] = ((int)feeItemList.TransType).ToString();//TRANS_TYPE;	--		��������;1�����ף�2������		2
            args[3] = feeItemList.Patient.ID;//CLINIC_CODE;	--		�����								3	
            args[4] = feeItemList.Patient.PID.CardNO;//CARD_NO;	--		��������									4		
            args[5] = ((FS.HISFC.Models.Registration.Register)feeItemList.Patient).DoctorInfo.SeeDate.ToString();//REG_DATE;	--		�Һ�����						5	
            args[6] = ((FS.HISFC.Models.Registration.Register)feeItemList.Patient).DoctorInfo.Templet.Dept.ID;//REG_DPCD;	--		�Һſ���							6	
            args[7] = feeItemList.RecipeOper.ID;//DOCT_CODE;	--		����ҽʦ							7
            args[8] = feeItemList.RecipeOper.Dept.ID;//DOCT_DEPT;	--		����ҽʦ���ڿ���				8
            args[9] = feeItemList.Item.ID;//ITEM_CODE;	--		��Ŀ����									9.
            args[10] = feeItemList.Item.Name;//ITEM_NAME;	--		��Ŀ����									10
            args[11] = NConvert.ToInt32(feeItemList.Item.ItemType == EnumItemType.Drug).ToString();//DRUG_FLAG;	--		1ҩƷ/0��Ҫ					11
            args[12] = feeItemList.Item.Specs;//SPECS;		--		���										12
            if (feeItemList.Item.ItemType == EnumItemType.Drug)
            {
                args[13] = NConvert.ToInt32(((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).Product.IsSelfMade).ToString();//SELF_MADE;	--		����ҩ��־					13
                args[14] = ((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).Quality.ID;//DRUG_QUALITY;	--		ҩƷ���ʣ���ҩ����ҩ		14	
                args[15] = ((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).DosageForm.ID;//DOSE_MODEL_CODE;--		����							15.
            }
            args[16] = feeItemList.Item.MinFee.ID;//FEE_CODE;	--		��С���ô���							16	
            args[17] = feeItemList.Item.SysClass.ID.ToString();//CLASS_CODE;	--		ϵͳ���				17	
            args[18] = feeItemList.Item.Price.ToString();//UNIT_PRICE;	--		����							18	
            args[19] = feeItemList.Item.Qty.ToString();//QTY;		--		����								19	
            args[20] = feeItemList.Days.ToString();//DAYS;		--		��ҩ�ĸ���������ҩƷΪ1			20	
            args[21] = feeItemList.Order.Frequency.ID;//FREQUENCY_CODE;	--		Ƶ�δ���						21	
            args[22] = feeItemList.Order.Usage.ID;//USAGE_CODE;	--		�÷�����							22	
            args[23] = feeItemList.Order.Usage.Name;//USE_NAME;	--		�÷�����							23
            args[24] = feeItemList.InjectCount.ToString();//INJECT_NUMBER;	--		Ժ��ע�����		24	
            args[25] = NConvert.ToInt32(feeItemList.IsUrgent).ToString();//EMC_FLAG;	--		�Ӽ����:1�Ӽ�/0��ͨ			25	
            args[26] = feeItemList.Order.Sample.ID;//LAB_TYPE;	--		��������							26	
            args[27] = feeItemList.Order.CheckPartRecord;//CHECK_BODY;	--		����								27	
            args[28] = feeItemList.Order.DoseOnce.ToString();//DOSE_ONCE;	--		ÿ������					28
            args[29] = feeItemList.Order.DoseUnit;//DOSE_UNIT;	--		ÿ��������λ							29
            if (feeItemList.Item.ItemType == EnumItemType.Drug)
            {
                args[30] = ((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).BaseDose.ToString();//BASE_DOSE;	--		��������					30
            }
            args[31] = feeItemList.Item.PackQty.ToString();//PACK_QTY;	--		��װ����						31	
            args[32] = feeItemList.Item.PriceUnit;//PRICE_UNIT;	--		�Ƽ۵�λ							32	
            args[33] = feeItemList.FT.PubCost.ToString();//PUB_COST;	--		�ɱ�Ч���				33	
            args[34] = feeItemList.FT.PayCost.ToString();//PAY_COST;	--		�Ը����				34	
            args[35] = feeItemList.FT.OwnCost.ToString();//OWN_COST;	--		�ֽ���				35	
            args[36] = feeItemList.ExecOper.Dept.ID;//EXEC_DPCD;	--		ִ�п��Ҵ���					36
            args[37] = feeItemList.ExecOper.Dept.Name;//EXEC_DPNM;	--		ִ�п�������					37
            args[38] = feeItemList.Compare.CenterItem.ID;//CENTER_CODE;	--		ҽ��������Ŀ����				38	
            args[39] = feeItemList.Compare.CenterItem.ItemGrade;//ITEM_GRADE;	--		��Ŀ�ȼ�1����2����3����		39	
            args[40] = NConvert.ToInt32(feeItemList.Order.Combo.IsMainDrug).ToString();//MAIN_DRUG;	--		��ҩ��־					40
            args[41] = feeItemList.Order.Combo.ID;//COMB_NO;	--		��Ϻ�										41	
            args[42] = feeItemList.ChargeOper.ID;//OPER_CODE;	--		������							42
            args[43] = feeItemList.ChargeOper.OperTime.ToString();//OPER_DATE;	--		����ʱ��					43
            args[44] = ((int)feeItemList.PayType).ToString();// //PAY_FLAG;	--		�շѱ�־��1δ�շѣ�2�շ�	44	
            args[45] = ((int)feeItemList.CancelType).ToString();
            args[46] = feeItemList.FeeOper.ID;//FEE_CPCD;	--		�շ�Ա����							46	
            args[47] = feeItemList.FeeOper.OperTime.ToString();//FEE_DATE;	--		�շ�����					47	
            args[48] = feeItemList.Invoice.ID;//INVOICE_NO;	--		Ʊ�ݺ�								48	
            args[49] = "";//INVO_CODE;	--		��Ʊ��Ŀ����				49
            args[50] = "";//INVO_SEQUENCE;	--		��Ʊ����ˮ��		50
            args[51] = NConvert.ToInt32(feeItemList.IsConfirmed).ToString();//CONFIRM_FLAG;	--		1δȷ��/2ȷ��				51		
            args[52] = feeItemList.ConfirmOper.ID;//CONFIRM_CODE;	--		ȷ����						52		
            args[53] = feeItemList.ConfirmOper.Dept.ID;//CONFIRM_DEPT;	--		ȷ�Ͽ���					53	
            args[54] = feeItemList.ConfirmOper.OperTime.ToString();//CONFIRM_DATE;	--		ȷ��ʱ��				54	
            args[55] = feeItemList.FT.RebateCost.ToString();// ECO_COST -- �Żݽ�� 55
            args[56] = feeItemList.InvoiceCombNO;//��Ʊ��ţ�һ�ν���������ŷ�Ʊ��combNo  56
            args[57] = feeItemList.NewItemRate.ToString();//����Ŀ����  57
            args[58] = feeItemList.OrgItemRate.ToString();//ԭ��Ŀ����  58 
            args[59] = feeItemList.ItemRateFlag;//��չ��־ ������Ŀ��־ 1�Է� 2 ���� 3 ����  59
            args[60] = feeItemList.UndrugComb.ID;
            args[61] = feeItemList.UndrugComb.Name;
            args[62] = feeItemList.Item.SpecialFlag1;
            args[63] = feeItemList.Item.SpecialFlag2;
            args[64] = feeItemList.FeePack;
            args[65] = feeItemList.NoBackQty.ToString();
            args[66] = feeItemList.ConfirmedQty.ToString();
            args[67] = feeItemList.ConfirmedInjectCount.ToString();
            args[68] = feeItemList.Order.ID;
            args[69] = feeItemList.RecipeSequence;
            args[70] = feeItemList.SpecialPrice.ToString();
            args[71] = feeItemList.FT.ExcessCost.ToString();
            args[72] = feeItemList.FT.DrugOwnCost.ToString();
            args[73] = feeItemList.FTSource;
            args[74] = NConvert.ToInt32(feeItemList.Item.IsMaterial).ToString();
            args[75] = NConvert.ToInt32(feeItemList.IsAccounted).ToString();
            args[76] = feeItemList.Item.Memo;

            return args;
        }

        #endregion

        #region ���з���

        /// <summary>
        /// ��ӻ�����Ŀ����-����ҩƷ������ϸ����Ϣ
        /// </summary>
        /// <param name="patient">������Ϣ</param>
        /// <param name="medItemList">ҩƷ������Ŀ��Ϣ</param>
        /// <param name="Insurance">ҽ������Ŀ�����Ϣ</param>
        /// <returns>�ɹ�: 1 ʧ�� : -1 û�в�������: 0</returns>
        public int InsertMedItemList(PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList medItemList)
        {
            if (medItemList.Patient.Pact.ID == null || medItemList.Patient.Pact.ID.Trim() == string.Empty)
            {
                medItemList.Patient.Pact.ID = patient.Pact.ID;
            }

            if (medItemList.Patient.Pact.PayKind.ID == null || medItemList.Patient.Pact.PayKind.ID.Trim() == string.Empty)
            {
                medItemList.Patient.Pact.PayKind.ID = patient.Pact.PayKind.ID;
            }

            return this.UpdateSingleTable("SI.AddSpecialLimitDrug.1", this.GetMedItemListParams(patient, medItemList));
        }

        /// <summary>
        /// ɾ��һ������֢סԺ��ϸ
        /// </summary>
        /// <param name="medItemList"></param>
        /// <returns></returns>
        public int DeleteMedItemList(FS.HISFC.Models.Fee.Inpatient.FeeItemList medItemList)
        {
            string sql = string.Empty;//Update���

            //���Where���
            if (this.Sql.GetSql("SI.DeleteItemPracticableSymptom.1", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ: ��SQL���";

                return -1;
            }
            try
            {
                sql = string.Format(sql, medItemList.RecipeNO, medItemList.SequenceNO, ((int)medItemList.TransType).ToString());
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
            
            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// ���סԺ��Ŀ�Ƿ������Ӧ֢
        /// </summary>
        /// <param name="medItemList"></param>
        /// <returns></returns>
        public bool CheckItemPracticableSymptom(FS.HISFC.Models.Fee.Inpatient.FeeItemList medItemList)
        {
            string sql = string.Empty;//Update���

            //���Where���
            if (this.Sql.GetSql("SI.CheckItemPracticableSymptom.1", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ: ��SQL���";

                return false;
            }
            try
            {
                sql = string.Format(sql, medItemList.RecipeNO, medItemList.SequenceNO, ((int)medItemList.TransType).ToString());
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return false;
            }
            string myresult = this.ExecSqlReturnOne(sql);

            return FS.FrameWork.Function.NConvert.ToBoolean(myresult);
        }

        /// <summary>
        /// ���סԺҽ���Ƿ������Ӧ֢
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public bool CheckOrderPracticableSymptom(string orderID)
        {
            string sql = string.Empty;//Update���

            //���Where���
            if (this.Sql.GetSql("SI.CheckOrderPracticableSymptom.1", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ: ��SQL���";

                return false;
            }
            try
            {
                sql = string.Format(sql, orderID);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return false;
            }
            string myresult = this.ExecSqlReturnOne(sql);

            return FS.FrameWork.Function.NConvert.ToBoolean(myresult);
        }
        /// <summary>
        /// �������ҽ���Ƿ������Ӧ֢SeeNO
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public bool CheckClinicOrderPracticableSymptom(string orderID)
        {
            string sql = string.Empty;//Update���

            //���Where���
            if (this.Sql.GetSql("SI.CheckClinicOrderPracticableSymptom.1", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ: ��SQL���";

                return false;
            }
            try
            {
                sql = string.Format(sql, orderID);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return false;
            }
            string myresult = this.ExecSqlReturnOne(sql);

            return FS.FrameWork.Function.NConvert.ToBoolean(myresult);
        }

        /// <summary>
        /// ������ҽ��(������ҽ����¼)
        /// </summary>
        /// <param name="order"></param>
        /// <returns>0 success -1 fail</returns>
        public int InsertOrder(FS.HISFC.Models.Order.Inpatient.Order order)
        {
            #region ������ҽ��
            //������ҽ��
            //Order.Order.CreateOrder.1
            //���룺71
            //			//������0 
            #endregion

            string strSql = "";

            if (this.Sql.GetSql("SI.Order.Order.CreateOrder.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            strSql = getOrderInfo(strSql, order);
            if (strSql == null) return -1;

            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ����������ҽ��(������ҽ����¼)
        /// </summary>
        /// <param name="order"></param>
        /// <returns>0 success -1 fail</returns>
        public int InsertOutpatOrder(FS.HISFC.Models.Order.OutPatient.Order order)
        {
            #region ������ҽ��
            //������ҽ��
            //Order.Order.CreateOrder.1
            //���룺71
            //			//������0 
            #endregion

            string strSql = "";

            if (this.Sql.GetSql("SI.Order.Order.CreateOrder.OutPatient.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            strSql = getOutpatOrderInfo(strSql, order);
            if (strSql == null) return -1;

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ɾ��һ��סԺ����֢ҽ��
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int DeleteOrder(FS.HISFC.Models.Order.Inpatient.Order order)
        {
            string sql = string.Empty;//Update���

            //���Where���
            if (this.Sql.GetSql("SI.Order.Order.DeleteOrder.1", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ: ��SQL���";

                return -1;
            }
            try
            {
                sql = string.Format(sql, order.ID);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }
        /// <summary>
        /// ɾ��һ����������֢ҽ��
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int DeleteOutpatOrder(FS.HISFC.Models.Order.OutPatient.Order order)
        {
            string sql = string.Empty;//Update���

            //���Where���
            if (this.Sql.GetSql("SI.Order.Order.DeleteOrder.OutPatient.1", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ: ��SQL���";

                return -1;
            }
            try
            {
                sql = string.Format(sql, order.SeeNO, order.ID);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }


        /// <summary>
        /// �������������ϸ
        /// </summary>
        /// <param name="feeItemList">������ϸʵ��</param>
        /// <returns>�ɹ�: 1 ʧ��: -1 û�в������ݷ��� 0</returns>
        public int InsertFeeItemList(FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList)
        {
            return this.UpdateSingleTable("SI.Fee.Item.GetFeeItemDetail.Insert", this.GetOutFeeItemListParams(feeItemList));
        }

        /// <summary>
        /// ���סԺ��Ŀ�Ƿ������Ӧ֢
        /// </summary>
        /// <param name="medItemList"></param>
        /// <returns></returns>
        public bool CheckClinicItemPracticableSymptom(FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList)
        {
            string sql = string.Empty;//Update���

            //���Where���
            if (this.Sql.GetSql("SI.CheckClinicItemPracticableSymptom.1", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ: ��SQL���";

                return false;
            }
            try
            {
                sql = string.Format(sql, feeItemList.RecipeNO, feeItemList.SequenceNO, ((int)feeItemList.TransType).ToString(), feeItemList.Order.ID, feeItemList.InvoiceCombNO);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return false;
            }
            string myresult = this.ExecSqlReturnOne(sql);

            return FS.FrameWork.Function.NConvert.ToBoolean(myresult);
        }

        /// <summary>
        /// ɾ��һ������֢������ϸ
        /// </summary>
        /// <param name="feeItemList"></param>
        /// <returns></returns>
        public int DeleteFeeItemList(FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList)
        {
            string sql = string.Empty;//Update���

            //���Where���
            if (this.Sql.GetSql("SI.Fee.Item.GetFeeItemDetail.Delete", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ: ��SQL���";

                return -1;
            }
            try
            {
                sql = string.Format(sql, feeItemList.RecipeNO, feeItemList.SequenceNO, ((int)feeItemList.TransType).ToString(), feeItemList.Order.ID, feeItemList.InvoiceCombNO);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        #endregion

    }
}
