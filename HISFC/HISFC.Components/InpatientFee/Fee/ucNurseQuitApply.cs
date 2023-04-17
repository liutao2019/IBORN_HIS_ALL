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
using FS.FrameWork.Function;

namespace FS.HISFC.Components.InpatientFee.Fee
{
    /// <summary>
    /// ucNurseQuitApply<br></br>
    /// [��������: סԺ��ʿ�˷�����UC]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2006-11-06]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucNurseQuitApply : ucQuitFee
    {
        /// <summary>
        /// ��ʿվ�˷�����
        /// </summary>
        public ucNurseQuitApply()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ������Ŀ�˷��Ƿ����ȫ��{F4912030-EF65-4099-880A-8A1792A3B449}
        /// </summary>
        protected bool isCombItemAllQuit = false;
        //{F4912030-EF65-4099-880A-8A1792A3B449}����

        /// <summary>
        /// ������Ŀ�˷��Ƿ����ȫ��{F4912030-EF65-4099-880A-8A1792A3B449}
        /// </summary>
        public bool IsCombItemAllQuit
        {
            get
            {
                return this.isCombItemAllQuit;
            }
            set
            {
                this.isCombItemAllQuit = value;
            }
        }//{F4912030-EF65-4099-880A-8A1792A3B449}����

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            //���ò���������סԺ��
            this.IsCanInputInpatientNO = false;
            //���ñ��淽ʽΪ�˷�����
            this.operation = Operations.Apply;

            this.itemType = ItemTypes.All;
            
            return base.OnInit(sender, neuObject, param);
        }

        /// <summary>
        /// ��ʾ���߻�����Ϣ
        /// </summary>
        protected override void SetPatientInfomation()
        {
            this.ucQueryPatientInfo.Text = this.patientInfo.PID.PatientNO;
            
            base.SetPatientInfomation();
        }

        /// <summary>
        /// ������ѡ��Ļ��߻�����Ϣ
        /// </summary>
        /// <param name="neuObject">���߻�����Ϣʵ��</param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            base.Clear();

            base.PatientInfo = neuObject as FS.HISFC.Models.RADT.PatientInfo;

            return base.OnSetValue(neuObject, e);
        }

        protected override int SaveApply()
        {
            if (base.SaveApply() == 1) 
            {
                base.ClearItemList();
                
                this.Retrive(false);
            }

            return 1;
        }

        /// <summary>
        /// ��д�����ȡ���˷Ѳ���,����Ϊȡ���˷�������Ϣ
        /// </summary>
        /// <returns></returns>
        protected override int CancelQuitOperation()
        {
            if (this.fpQuit.ActiveSheet.RowCount == 0) 
            {
                return -1;
            }
            //��ǰѡ�е���
            int selectedIndex = this.fpQuit.ActiveSheet.ActiveRowIndex;

            if(this.fpQuit.ActiveSheet.Rows[selectedIndex].Tag == null)
            {
                return -1;
            }

            //��õ�ǰѡ����Ŀ
            FeeItemList feeItemList = this.fpQuit.ActiveSheet.Rows[selectedIndex].Tag as FeeItemList;

            //�ж��Ƿ��Ѿ�ȷ���˷�,����˷�,��ô������ȡ��
            if (feeItemList.Item.User01 == "1") 
            {
                MessageBox.Show(Language.Msg("����ҩ����ҩ��������ҩȷ�� �޷���������"));

                return -1;
            }
            //�������Ŀ�Ǳ���δ�ύ���ݿ�ļ�¼,��ôֱ�ӵ��û����ȡ������.
            if (feeItemList.Memo != "OLD")
            {
                return base.CancelQuitOperation();
            }
            else//ȡ������ĿΪ�ύ���ݿ�ļ�¼,��ô��Ҫɾ�����ݿ��¼,�жϲ����Ȳ���. 
            {
                //�жϵ�ǰ�˷��������,�Ƿ�Ϊ������¼���˷��������,��ʱ����û��д.����Ϊʲôû��д,��God.

                DialogResult result = MessageBox.Show(Language.Msg("ȷ��ȡ�������˷�������Ϣ"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RightAlign);

                if (result == DialogResult.No)
                {
                    return -1;
                }

                //Transaction t = new Transaction(this.inpatientManager.Connection);
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                this.inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.phamarcyIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.returnApplyManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                //���ɲ����˷�������ȡ���˷��������� �ж��˷�����ȡ������
                if (feeItemList.User01 == "0")
                {
                    //�ж������Ƿ��ѱ��˷�ȷ��
                    bool isStill = false;
                    ArrayList tempAl = null;

                    tempAl = this.returnApplyManager.QueryDrugReturnApplys(feeItemList.Patient.ID, false, true);

                    foreach (FS.HISFC.Models.Fee.ReturnApply info in tempAl)
                    {
                        if (info.RecipeNO == feeItemList.RecipeNO && info.SequenceNO == feeItemList.SequenceNO)
                        {
                            isStill = true;

                            break;
                        }
                    }

                    if (isStill)
                    {  //�����ѱ�ȷ��
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("�������ѱ��˷�ȷ�ϣ��޷���������"));

                        return -1;
                    }
                }

                //if (feeItemList.Item.IsPharmacy)
                if (feeItemList.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    //����ҩƷ��ϸ���еĿ�����������ֹ����
                    int returnValue = this.inpatientManager.UpdateNoBackQtyForDrug(feeItemList.RecipeNO, feeItemList.SequenceNO, -feeItemList.Item.Qty, feeItemList.BalanceState);
                    if (returnValue == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("����ҩƷ������������!") + this.inpatientManager.Err);

                        return -1;
                    }
                    else if (returnValue == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("���ݷ����䶯!��ˢ�´���"));

                        return -1;
                    }

                    //ȡ����ҩ���룬���жϲ�������
                    //���ݲ�ͬ������Դ���в�ͬ����
                    //��������ҩȷ�� ���ϳ�������
                    if (feeItemList.PayType == FS.HISFC.Models.Base.PayTypes.PhaConfim)
                    {
                        returnValue = this.phamarcyIntegrate.CancelApplyOut(feeItemList.RecipeNO, feeItemList.SequenceNO);
                        if (returnValue == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg("ȡ���˷��������!") + this.phamarcyIntegrate.Err);

                            return -1;
                        }
                        else if (returnValue == 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg("���ݷ����䶯!��ˢ�´���"));

                            return -1;
                        }
                    }
                    else//û�з�ҩȷ��
                    {
                        //�ָ����������¼Ϊ��Ч   ����� 
                        returnValue = this.phamarcyIntegrate.UndoCancelApplyOut(feeItemList.RecipeNO, feeItemList.SequenceNO);
                        if (returnValue == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.phamarcyIntegrate.Err);

                            return -1;
                        }
                        else if (returnValue == 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg("�������ѱ�ȡ�����޷���������"));

                            return -1;
                        }

                        //ȡ���˷����� ��״̬Ϊ2 ɾ���˷���������
                        //���Բ����
                        if (this.returnApplyManager.CancelReturnApply(feeItemList.User03, this.returnApplyManager.Operator.ID) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.returnApplyManager.Err);

                            return -1;
                        }
                    }
                }
                else
                {//���·�ҩƷ��ϸ���еĿ�����������ֹ����
                    int returnValue = this.inpatientManager.UpdateNoBackQtyForUndrug(feeItemList.RecipeNO, feeItemList.SequenceNO, -feeItemList.Item.Qty, feeItemList.BalanceState);
                    if (returnValue == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.inpatientManager.Err);

                        return -1;
                    }
                    else if (returnValue == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("���ݷ����䶯!��ˢ�´���"));

                        return -1;
                    }

                    //ȡ���˷����� ��״̬Ϊ2 ɾ���˷���������
                    //���Բ����
                    if (this.returnApplyManager.CancelReturnApply(feeItemList.User03, this.returnApplyManager.Operator.ID) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.returnApplyManager.Err);

                        return -1;
                    }
                }

                FS.FrameWork.Management.PublicTrans.Commit();

                MessageBox.Show(Language.Msg("�˷�����ȡ���ɹ�!"));

                base.ClearItemList();

                this.Retrive(false);
            }

            return 1;
        }

        /// <summary>
        /// ��ѯ���˷Ѻ��Ѿ�������Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected override int Retrive(bool isRetrieveReturnApply)
        {
            DateTime beginTime = this.dtpBeginTime.Value;
            DateTime endTime = this.dtpEndTime.Value;

            if (this.patientInfo == null) 
            {
                MessageBox.Show(Language.Msg("��ѡ��Ҫ�˷�����Ļ���"));

                return -1;
            }

            this.RetrieveReturnApplyDrug(this.patientInfo.ID, beginTime, endTime);

            this.RetrieveReturnApplyUndrug(this.patientInfo.ID, beginTime, endTime);
            
            return base.Retrive(false);
        }

        /// <summary>
        /// ��ȡҩƷ�˷�������Ϣ
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        private void RetrieveReturnApplyDrug(string inpatientNO, DateTime beginTime, DateTime endTime)
        {

            this.fpQuit_SheetDrug.RowCount = 0;
            
            ArrayList applyReturnFromApplyOut = new ArrayList();
            ArrayList applyReturnsUnQuit = new ArrayList();//δ������˷�������Ϣ
            ArrayList confirmDrugList = new ArrayList();//��׼ҩƷ��Ϣ
            //��ȡҩƷ�˷�������Ϣ  ������Ч����ҩ�����¼ �ɳ���������ȡ��ҩ���� ����״̬����Ч��
            //�ò��ֻ�ȡ�Ķ��ѷ�ҩ��ҩƷ���˷����� ��ApplyOut�ڼ�����״̬Ϊ0(����)�ļ�¼ 
            //			al = this.drugItem.GetDrugReturn(this.PatientInfo.PVisit.PatientLocation.Dept.ID,"AAAA",this.PatientInfo.ID);
            //applyReturnFromApplyOut = this.drugItem.GetDrugReturn("AAAA", "AAAA", this.PatientInfo.ID);
            if (applyReturnFromApplyOut == null)
                return;
            //��ȡδ��ҩ(ҩ��δ������ҩȷ��)����ҩ���� �Դ˲����˷����� ����ʱ��ֱ���ó���������ڳ��������¼Ϊ��Ч
            //�ò��ֻ�ȡ�Ķ�δ��ҩ��ҩƷ���˷����� ��CancelItem�ڼ����ó� 
            //��ȡ�û���δȷ�� δ��ҩ���˷������¼
            applyReturnsUnQuit = this.returnApplyManager.QueryDrugReturnApplys(this.patientInfo.ID, false, false);
            if (applyReturnsUnQuit != null)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut applyOut = null;
                foreach (FS.HISFC.Models.Fee.ReturnApply applyReturn in applyReturnsUnQuit)
                {
                    applyOut = new FS.HISFC.Models.Pharmacy.ApplyOut();
                    applyOut.ID = applyReturn.ID;								//������ˮ��
                    applyOut.BillNO = applyReturn.ApplyBillNO;					    //���뵥�ݺ�
                    applyOut.RecipeNO = applyReturn.RecipeNO;					//������
                    applyOut.SequenceNO = applyReturn.SequenceNO;				//��������Ŀ��ˮ��
                    applyOut.ApplyDept.ID = applyReturn.RecipeOper.Dept.ID;				//�������
                    applyOut.Item.Name = applyReturn.Item.Name;					//��Ŀ����
                    applyOut.Item.ID = applyReturn.Item.ID;						//��Ŀ����
                    applyOut.Item.Specs = applyReturn.Item.Specs;				//���
                    applyOut.Item.Price = applyReturn.Item.Price;				//���ۼ�  ����С��λ��������ۼ�
                    applyOut.Operation.ApplyQty = applyReturn.Item.Qty;				//������ҩ���������Ը��������������
                    applyOut.Item.PackQty = applyReturn.Item.PackQty;
                    applyOut.Days = applyReturn.Days;							//����
                    applyOut.Item.MinUnit = applyReturn.Item.PriceUnit;			//�Ƽ۵�λ
                    applyOut.User01 = "0";										//��־�������ɲ����˷�������� ��applyReturnʵ���ȡ
                    
                    applyReturnFromApplyOut.Add(applyOut);
                }
            }
            

            #region ��ȡҩ������ҩȷ�� סԺ����δ�˷�ȷ�ϵ÷������¼
            //�ò��ֻ�ȡ�Ķ��ѷ�ҩ��ҩƷ�˷����� ҩ����������ҩȷ�� סԺ����δ���˷�ȷ�ϵ����� ��CancelItem�ڼ���
            confirmDrugList = this.returnApplyManager.QueryDrugReturnApplys(this.patientInfo.ID, false, true);
            if (confirmDrugList != null)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut applyOut;
                foreach (FS.HISFC.Models.Fee.ReturnApply applyReturn in confirmDrugList)
                {
                    applyOut = new FS.HISFC.Models.Pharmacy.ApplyOut();
                    applyOut.ID = applyReturn.ID;								//������ˮ��
                    applyOut.BillNO = applyReturn.ApplyBillNO;					//���뵥�ݺ�
                    applyOut.RecipeNO = applyReturn.RecipeNO;					//������
                    applyOut.SequenceNO = applyReturn.SequenceNO;				//��������Ŀ��ˮ��
                    applyOut.ApplyDept.ID = applyReturn.RecipeOper.Dept.ID;				//�������
                    applyOut.Item.Name = applyReturn.Item.Name;					//��Ŀ����
                    applyOut.Item.ID = applyReturn.Item.ID;						//��Ŀ����
                    applyOut.Item.Specs = applyReturn.Item.Specs;				//���
                    applyOut.Item.Price = applyReturn.Item.Price;				//���ۼ�  ����С��λ��������ۼ�
                    applyOut.Operation.ApplyQty = applyReturn.Item.Qty;				//������ҩ���������Ը��������������
                    applyOut.Item.PackQty = applyReturn.Item.PackQty;
                    applyOut.Days = applyReturn.Days;							//����
                    applyOut.Item.MinUnit = applyReturn.Item.PriceUnit;			//�Ƽ۵�λ
                    applyOut.User01 = "0";										//��־�������ɲ����˷�������� ��applyReturnʵ���ȡ
                    applyOut.User02 = "1";										//��־������ ҩ������ҩȷ�� ��סԺ����δ�˷�ȷ��
                   
                    applyReturnFromApplyOut.Add(applyOut);
                }
            }
            #endregion

            for (int i = 0; i < applyReturnFromApplyOut.Count; i++)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut applyOut = new FS.HISFC.Models.Pharmacy.ApplyOut();
                applyOut = applyReturnFromApplyOut[i] as FS.HISFC.Models.Pharmacy.ApplyOut;
                if (applyOut == null)
                    return;

                //ȡ������Ϣ
                FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList;
                //feeItemList = this.inpatientManager.GetItemListByRecipeNO(applyOut.RecipeNO, applyOut.SequenceNO, true);
                feeItemList = this.inpatientManager.GetItemListByRecipeNO(applyOut.RecipeNO, applyOut.SequenceNO, HISFC.Models.Base.EnumItemType.Drug);
                if (feeItemList == null)
                {
                    MessageBox.Show("��ȡδ��׼�˷�������Ϣ�ķ�����ϸ����");
                    return;
                }
                //��ʱ�洢�������
                feeItemList.ExecOper.Dept.User03 = applyOut.ApplyDept.ID;

                //��ȡҽ������
                //string DoctName = "";
                //DoctName = this.personHelp.GetName(feeItemList.FeeInfo.ReciptDoct.ID);
                //feeItemList.FeeInfo.ReciptDoct.Name = DoctName;

                //��ȡ��С��������--Add By Maokb
                //string MinFee = "";
                //MinFee = this.minfeeHelp.GetName(feeItemList.Item.MinFee.ID);
                //feeItemList.Item.MinFee.Name = MinFee;
                //��ȡִ�п�������--Add By Maokb
                //string DeptName = "";
                //DeptName = this.deptHelp.GetName(feeItemList.FeeInfo.ExeDept.ID);
                //feeItemList.FeeInfo.ExeDept.Name = DeptName;

               
               
                this.fpQuit_SheetDrug.Rows.Add(this.fpQuit_SheetDrug.RowCount, 1);

                int index = this.fpQuit_SheetDrug.Rows.Count - 1;
                
                applyOut.Item.PackQty = feeItemList.Item.PackQty;
                if (applyOut.Item.PackQty == 0)
                {
                    applyOut.Item.PackQty = 1;
                }
                if (feeItemList.Item.PackQty == 0)
                {
                    feeItemList.Item.PackQty = 1;
                }
                if (applyOut.Days == 0)
                {
                    applyOut.Days = 1;
                }
                decimal iNum = 0;
                decimal iCost = 0; ;
                if (applyOut.User01 == "0")			//���������ɲ����˷�������ȡ ����applyReturnʵ���ȡ����ת��ΪapplyOutʵ��
                {
                    iNum = applyOut.Operation.ApplyQty;						//������ҩ���������Ը��������������
                    iCost = feeItemList.Item.Price * applyOut.Operation.ApplyQty / feeItemList.Item.PackQty;	//�ܽ��
                }
                else							//���������ɳ���������ȡ  
                {
                    iNum = FS.FrameWork.Public.String.FormatNumber(applyOut.Operation.ApplyQty * applyOut.Days, 4);				//�˷�����
                    iCost = applyOut.Operation.ApplyQty * applyOut.Item.Price / applyOut.Item.PackQty;		//�ܽ��
                }

                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.ItemName, applyOut.Item.Name);
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.Specs, applyOut.Item.Specs);
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.Price, applyOut.Item.Price);
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.Qty, iNum);
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.Unit, feeItemList.Item.PriceUnit);
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.Cost, iCost);
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.FeeDate, feeItemList.FeeOper.OperTime);
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.IsConfirm, feeItemList.IsConfirmed);
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.IsApply, false);

                feeItemList.User02 = applyOut.BillNO; //�˷����뵥�ݺ�
                feeItemList.Item.Qty = iNum;
                feeItemList.FT.TotCost = iCost;
                feeItemList.User03 = applyOut.ID;				//�˷�������ˮ��
                feeItemList.User01 = applyOut.User01;			// "0" ���������ɲ����˷�������ȡ ���� ���������ɳ���������ȡ
                feeItemList.Item.User01 = applyOut.User02;		//"1" ��־����������ҩȷ�� ����δ�˷�ȷ��
                //���������Ƿ�Ϊ�ѱ�������˷�����
                feeItemList.Memo = "OLD";

                this.fpQuit_SheetDrug.Rows[index].Tag = feeItemList;
            }
        }

        private void RetrieveReturnApplyUndrug(string inpatientNO, DateTime beginTime, DateTime endTime)
        {
            this.fpQuit_SheetUndrug.Rows.Count = 0;

            ArrayList returnApplys = new ArrayList();
            //��ȡʱ��η�Χ�ڵļ�¼
            returnApplys = this.returnApplyManager.QueryReturnApplys(this.patientInfo.ID, false, false);
            if (returnApplys == null)
            {
                return;
            }

            foreach (FS.HISFC.Models.Fee.ReturnApply returnApply in returnApplys) 
            {
                FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList = null;

                //feeItemList = this.inpatientManager.GetItemListByRecipeNO(returnApply.RecipeNO, returnApply.SequenceNO, false);
                feeItemList = this.inpatientManager.GetItemListByRecipeNO(returnApply.RecipeNO, returnApply.SequenceNO, HISFC.Models.Base.EnumItemType.UnDrug);
                if (feeItemList == null) 
                {
                    MessageBox.Show("�����Ŀ��Ϣ����!" + this.inpatientManager.Err);

                    return;
                }

                this.fpQuit_SheetUndrug.Rows.Add(this.fpQuit_SheetUndrug.RowCount, 1);

                int index = this.fpQuit_SheetUndrug.Rows.Count - 1;

                returnApply.Item.PackQty = feeItemList.Item.PackQty;
                if (returnApply.Item.PackQty == 0)
                {
                    returnApply.Item.PackQty = 1;
                }
                if (feeItemList.Item.PackQty == 0)
                {
                    feeItemList.Item.PackQty = 1;
                }
                if (returnApply.Days == 0)
                {
                    returnApply.Days = 1;
                }

                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.ItemName, returnApply.Item.Name);
                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.FeeName, feeItemList.Item.MinFee.ID);
                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.Price, feeItemList.Item.Price);
                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.Qty, returnApply.Item.Qty);
                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.Unit, feeItemList.Item.PriceUnit);
                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.Cost, feeItemList.Item.Price * returnApply.Item.Qty);
                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.ExecDept, feeItemList.FeeOper.Dept.ID);
                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.IsConfirm, feeItemList.IsConfirmed);
                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.IsApply, false);

                feeItemList.User02 = returnApply.ApplyBillNO; //�˷����뵥�ݺ�
                feeItemList.Item.Qty = returnApply.Item.Qty;
                feeItemList.FT.TotCost = feeItemList.Item.Price * returnApply.Item.Qty;
                feeItemList.User03 = returnApply.ID;				//�˷�������ˮ��
                feeItemList.User01 = "0";			// "0" ���������ɲ����˷�������ȡ ���� ���������ɳ���������ȡ
                //���������Ƿ�Ϊ�ѱ�������˷�����
                feeItemList.Memo = "OLD";

                this.fpQuit_SheetUndrug.Rows[index].Tag = feeItemList;
            }  
        }
        private void RetrieveReturnApplyMaterial(string inpatientNO, DateTime beginTime, DateTime endTime)
        { 

        }
    }
}
