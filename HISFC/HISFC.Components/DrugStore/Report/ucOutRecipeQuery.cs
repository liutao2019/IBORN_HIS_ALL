using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.DrugStore.Report
{
    /// <summary>
    /// [��������: ���ﴦ����ѯ]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-03]<br></br>
    /// <�޸ļ�¼ 
    ///		 ��ʵ�� �����ǩ�Ĳ��� �ɴ˴����
    ///  />
    /// </summary>
    public partial class ucOutRecipeQuery : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucOutRecipeQuery()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// ҩƷ������
        /// </summary>
        FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
        
        /// <summary>
        /// ҩƷ������
        /// </summary>
        FS.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();
               
        /// <summary>
        /// ��������
        /// </summary>
        private FS.FrameWork.Models.NeuObject operDept = null;

        /// <summary>
        /// ҩƷ����
        /// </summary>
        private ArrayList drugCollectioon = null;

        /// <summary>
        /// ��Ա���
        /// </summary>
        private ArrayList personList = null;

        /// <summary>
        /// �Ƿ�ȫ������
        /// </summary>
        private bool isSendAllData = false;

        /// <summary>
        /// ��ӡ�ؼ��ӿ���
        /// </summary>
        FS.HISFC.BizProcess.Interface.Pharmacy.IOutpatientPrintFactory factory = null;

        #endregion

        #region ���������

        /// <summary>
        /// ��Ա������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper personHelper = null;


        /// <summary>
        /// ҽ��������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper doctorHelper = null;

        /// <summary>
        /// ��ҩ�ն˰�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper drugTerminalHelper = null;

        /// <summary>
        /// ��ҩ�ն˰�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper sendTerminalHelper = null;

        #endregion

        #region ��ϸ��ѯSql���  {0BD715F2-C09F-4b75-84D4-E9B45DAAEA96}

        /// <summary>
        /// ��ϸ��Ϣ��ѯ  ģ����ѯ
        /// </summary>
        string strDetailSql2 = @"
select t.recipe_no as ������,
       t.valid_state as ��Ч�Ա���,
       t.trade_name || '[' || t.specs || ']' as ��Ʒ��,
       decode(substr((t.dose_once || t.dose_unit),0,1),'.','0' || t.dose_once || t.dose_unit,t.dose_once || t.dose_unit) as ÿ����,
       (select name from com_dictionary where code = t.usage_code and type = 'USAGE' )as �÷�,
       t.dfq_freq as Ƶ��,
       decode(substr((t.apply_num || t.min_unit),0,1),'.','0' || t.apply_num || t.min_unit,t.apply_num || t.min_unit) as ����,
       round(t.retail_price / decode(t.pack_qty,0,1,t.pack_qty),4) as ���ۼ�,
       round(t.apply_num * t.days / decode(t.pack_qty,0,1,t.pack_qty) * t.retail_price,2) as ���,
       t.days as ����,
       decode(t.valid_state,'1','��Ч','��Ч') as ��Ч��
from    pha_com_applyout t
where   t.recipe_no in (
select  pha_sto_recipe.recipe_no
from    pha_sto_recipe
where   pha_sto_recipe.drug_dept_code = '{4}'
and     pha_sto_recipe.fee_date >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
and     pha_sto_recipe.fee_date < to_date('{1}','yyyy-mm-dd hh24:mi:ss')
and     (('{3}' = 'A') or ('{3}' = '0' and pha_sto_recipe.card_no like '{2}')
        or ('{3}' = '1' and pha_sto_recipe.invoice_no like '{2}')
        or ('{3}' = '2' and pha_sto_recipe.patient_name like  '{2}')
        or ('{3}' = '3' and pha_sto_recipe.recipe_no like '{2}'))
)";

        /// <summary>
        /// ��ϸ��Ϣ��ѯ ƥ���ѯ
        /// </summary>
        string strDetailSql1 = @"
select t.recipe_no as ������,
       t.valid_state as ��Ч�Ա���,
       t.trade_name || '[' || t.specs || ']' as ��Ʒ��,
       decode(substr((t.dose_once || t.dose_unit),0,1),'.','0' || t.dose_once || t.dose_unit,t.dose_once || t.dose_unit) as ÿ����,
       (select name from com_dictionary where code = t.usage_code and type = 'USAGE' )as �÷�,
       t.dfq_freq as Ƶ��,
       decode(substr((t.apply_num || t.min_unit),0,1),'.','0' || t.apply_num || t.min_unit,t.apply_num || t.min_unit) as ����,
       round(t.retail_price / decode(t.pack_qty,0,1,t.pack_qty),4) as ���ۼ�,
       round(t.apply_num * t.days / decode(t.pack_qty,0,1,t.pack_qty) * t.retail_price,2) as ���,
       t.days as ����,
       decode(t.valid_state,'1','��Ч','��Ч') as ��Ч��
from    pha_com_applyout t
where   t.recipe_no in (
select  pha_sto_recipe.recipe_no
from    pha_sto_recipe
where  pha_sto_recipe.drug_dept_code='{4}'
and    pha_sto_recipe.fee_date >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
and     pha_sto_recipe.fee_date < to_date('{1}','yyyy-mm-dd hh24:mi:ss')
and     (('{3}' = 'A') or ('{3}' = '0' and pha_sto_recipe.card_no = '{2}')
        or ('{3}' = '1' and pha_sto_recipe.invoice_no = '{2}')
        or ('{3}' = '2' and pha_sto_recipe.patient_name =  '{2}')
        or ('{3}' = '3' and pha_sto_recipe.recipe_no = '{2}')
        or ('{3}' = '4' and pha_sto_recipe.doct_code = '{2}'))
)";
        #endregion


        #region ��������Ϣ

        /// <summary>
        /// ���幤��������
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
       
        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="NeuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object NeuObject, object param)
        {
            //���ӹ�����
            this.toolBarService.AddToolButton("�ϲ�", "�ϲ�������ʾ", 0, true, false, null);
            this.toolBarService.AddToolButton("չ��", "չ����ʾ������ϸ", 0, true, false, null);
            return this.toolBarService;
        }
 
        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryData();

            return 1;
        }

        public override int Export(object sender, object neuObject)
        {
            this.Export();

            return base.Export(sender, neuObject);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "�ϲ�")
            {
                this.ExpandFp(false);
            }
            if (e.ClickedItem.Text == "չ��")
            {
                this.ExpandFp(true);
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

        #region ���ݳ�ʼ��

        /// <summary>
        /// ���ݳ�ʼ��
        /// </summary>
        protected void DataInit()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڼ��ػ�����ѯ����,���Ժ�...."));
            Application.DoEvents();

            this.dtEnd.Value = this.itemManager.GetDateTimeFromSysDateTime();
            this.dtBegin.Value = this.dtEnd.Value.AddDays(-1);

            #region ���ز�ѯ���

            ArrayList al = new ArrayList();
            FS.FrameWork.Models.NeuObject info1 = new FS.FrameWork.Models.NeuObject();
            info1.ID = "A";
            info1.Name = "ȫ��";
            al.Add(info1);
            FS.FrameWork.Models.NeuObject info2 = new FS.FrameWork.Models.NeuObject();
            info2.ID = "0";
            info2.Name = "��������";
            al.Add(info2);
            FS.FrameWork.Models.NeuObject info3 = new FS.FrameWork.Models.NeuObject();
            info3.ID = "1";
            info3.Name = "��Ʊ��";
            al.Add(info3);
            FS.FrameWork.Models.NeuObject info4 = new FS.FrameWork.Models.NeuObject();
            info4.ID = "2";
            info4.Name = "����";
            al.Add(info4);
            FS.FrameWork.Models.NeuObject info5 = new FS.FrameWork.Models.NeuObject();
            info5.ID = "3";
            info5.Name = "������";
            al.Add(info5);
            FS.FrameWork.Models.NeuObject info6 = new FS.FrameWork.Models.NeuObject();
            info6.ID = "D";
            info6.Name = "ҩƷ";
            al.Add(info6);
            FS.FrameWork.Models.NeuObject info7 = new FS.FrameWork.Models.NeuObject();
            info7.ID = "4";
            info7.Name = "ҽ������";
            al.Add(info7);
            this.cmbQueryType.DataSource = al;
            this.cmbQueryType.DisplayMember = "Name";
            this.cmbQueryType.ValueMember = "ID";

            #endregion

            #region ������Ա

            FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
            //{07972AB4-B44B-43e2-AE2C-478A19E8EBC1}ֻ����ҽ��20120510modify
            ArrayList personAl = personManager.GetEmployeeAll();
            ArrayList doctorAl = personManager.GetEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            //endmodify
            if (personAl == null)
            {
                Function.ShowMsg("��ȡ��Ա�б�ʧ��" + personManager.Err);
                return;
            }
            this.personList = doctorAl;
            if (this.personHelper == null)
            {
                this.personHelper = new FS.FrameWork.Public.ObjectHelper(personAl);
            }
            if (this.doctorHelper == null)
            {
                this.doctorHelper = new FS.FrameWork.Public.ObjectHelper(doctorAl);
            }
            #endregion

            #region ����ҩƷ
            List<FS.HISFC.Models.Pharmacy.Item> itemList = this.itemManager.QueryItemList(true);
            if (itemList == null)
            {
                Function.ShowMsg("��ȡҩƷ�б�ʧ��" + this.itemManager.Err);
                return;
            }

            foreach (FS.HISFC.Models.Pharmacy.Item item in itemList)
            {
                item.Memo = item.Specs;
            }

            this.drugCollectioon = new ArrayList(itemList.ToArray());

            #endregion

            #region ���������ն��б�
            ArrayList alDrugTerminal = this.drugStoreManager.QueryDrugTerminalByDeptCode(this.operDept.ID, "1");
            if (alDrugTerminal != null)
            {
                this.drugTerminalHelper = new FS.FrameWork.Public.ObjectHelper(alDrugTerminal);
            }
            ArrayList alSendTerminal = this.drugStoreManager.QueryDrugTerminalByDeptCode(this.operDept.ID, "0");
            if (alSendTerminal != null)
            {
                this.sendTerminalHelper = new FS.FrameWork.Public.ObjectHelper(alSendTerminal);
            }
            #endregion

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        #endregion

        /// <summary>
        /// ����FarPoint
        /// </summary>
        private void SetFP()
        {
            if (this.cmbQueryType.SelectedValue != null && this.cmbQueryType.SelectedValue.ToString() == "D")
            {
                FarPoint.Win.Spread.CellType.NumberCellType numCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
                numCellType.DecimalPlaces = 4;

                this.neuSpread1_Sheet1.Columns[0].Visible = false;			//ҩƷ����
                this.neuSpread1_Sheet1.Columns[1].Width = 184F;			    //����[���]
                this.neuSpread1_Sheet1.Columns[2].Width = 90F;				//���� ��λ

                this.neuSpread1_Sheet1.Columns[3].Visible = false;
                this.neuSpread1_Sheet1.Columns[4].Visible = false;
                this.neuSpread1_Sheet1.Columns[5].Visible = false;
            }
            else
            {
                this.neuSpread1_Sheet1.Columns[0].Visible = true;
                this.neuSpread1_Sheet1.Columns[0].Width = 50F;		//������
                this.neuSpread1_Sheet1.Columns[1].Width = 60F;		//����
                this.neuSpread1_Sheet1.Columns[2].Width = 35F;		//�Ա�
                this.neuSpread1_Sheet1.Columns[3].Width = 35F;		//����
                this.neuSpread1_Sheet1.Columns[3].Visible = true;
                this.neuSpread1_Sheet1.Columns[4].Width = 70F;		//������
                this.neuSpread1_Sheet1.Columns[4].Visible = true;
                this.neuSpread1_Sheet1.Columns[5].Width = 80F;		//��Ʊ��
                this.neuSpread1_Sheet1.Columns[5].Visible = true;
                this.neuSpread1_Sheet1.Columns[6].Width = 70F;		//��ҩ̨
                this.neuSpread1_Sheet1.Columns[7].Width = 50F;		//��ҩ��
                this.neuSpread1_Sheet1.Columns[8].Width = 120F;		//��ҩʱ��
                this.neuSpread1_Sheet1.Columns[9].Width = 70F;		//��ҩ̨
                this.neuSpread1_Sheet1.Columns[10].Width = 50F;		//��ҩ��
                this.neuSpread1_Sheet1.Columns[11].Width = 120F;	//��ҩʱ��
                this.neuSpread1_Sheet1.Columns[12].Width = 60F;		//����ҽ��
            }

            //��ֹ�¼��ظ����
            this.neuSpread1.ChildViewCreated -= new FarPoint.Win.Spread.ChildViewCreatedEventHandler(fpSpread1_ChildViewCreated);
            this.neuSpread1.ChildViewCreated += new FarPoint.Win.Spread.ChildViewCreatedEventHandler(fpSpread1_ChildViewCreated);
        }

        public override int Print(object sender, object neuObject)
        {
            FS.HISFC.BizLogic.Pharmacy.DrugStore drugManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();
            FS.HISFC.Models.Pharmacy.DrugRecipe info = drugManager.GetDrugRecipe(this.operDept.ID, this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRow.Index, 0].Text);
            string detailState = "0";

            if (info.RecipeState == "0" || info.RecipeState == "1")
            {
                detailState = "0";
            }
            else if (info.RecipeState == "2")
            {
                detailState = "1";
            }
            else
            {
                detailState = "2";
            }

            ArrayList alInfo = new ArrayList();
            alInfo = this.itemManager.QueryApplyOutListForClinic(this.operDept.ID, "M1", detailState, info.RecipeNO);
            if (alInfo == null)
            {
                MessageBox.Show(itemManager.Err);
                return -1;
            }

            //{9FA792B0-A60F-48d8-A3F5-1C52450C44A5}  ��ȡ��ӡ����
            FS.HISFC.Models.Pharmacy.DrugTerminal terminal = drugManager.GetDrugTerminal(info.DrugTerminal.ID);
            if (terminal == null)
            {
                MessageBox.Show("��ȡ��ҩ�ն���Ϣ��������" + drugManager.Err);
                return -1;
            }
            if (terminal.TerimalPrintType == FS.HISFC.Models.Pharmacy.EnumClinicPrintType.��ǩ)
            {
                this.isSendAllData = false;
            }
            else
            {
                this.isSendAllData = true;
            }

            if (this.factory == null)
            {
                MessageBox.Show("δ������ҩ̨���򵥾ݷ�ʽ���޷����д�ӡ","",MessageBoxButtons.OK,MessageBoxIcon.Information);               
                return -1;
            }

            FS.HISFC.Components.DrugStore.Function.IDrugPrint = factory.GetInstance(terminal);

            if (FS.HISFC.Components.DrugStore.Function.IDrugPrint == null)
            {
                MessageBox.Show("δ���õ�ǰѡ����ҩ̨�Ĵ�ӡ��ʽ���޷����д�ӡ" ,"",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return -1;
            }

            //{F1C6EB10-A23D-4249-BD0C-47329421B78B} ��ҩ���ںŸ�ֵ
            string sendWindow = "";
            FS.HISFC.Models.Pharmacy.DrugTerminal sendTerminal = drugManager.GetDrugTerminal(info.SendTerminal.ID);
            if (sendTerminal != null)
            {
                sendWindow = sendTerminal.Name;
            }
            ////{9FA792B0-A60F-48d8-A3F5-1C52450C44A5}  ��ȡ��ӡ����

            Print(info, alInfo,sendWindow);

            return base.Print(sender, neuObject);
        }

        /// <summary>
        /// ִ�д�ӡ
        /// </summary>
        /// <param name="al">��ӡ����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        internal int Print(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, ArrayList al,string sendWindow)
        {
            //һ��ֻ��ӡһ�������ŵ�
            //�����ʱ������Ϻš�Ժע��Ƿ��� ���ڴ�ӡ
            //applyOut.User01 ��ҩ���ں� applyOut.User02 Ժע����

            if (al.Count <= 0)
            {
                return 1;
            }

            FS.HISFC.Models.Registration.Register patientInfo = null;		//������Ϣ

            #region ������Ϣ��ȡ

            //��ȡ������Ϣ
            FS.HISFC.BizProcess.Integrate.Registration.Registration regManager = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
            patientInfo = regManager.GetByClinic(drugRecipe.ClinicNO);

            #endregion

            #region  ��ӡ��ʽһ ȫ�����ݴ��䷽ʽ

            if (this.isSendAllData)
            {
                patientInfo.User01 = drugRecipe.FeeOper.OperTime.ToString();

                patientInfo.DoctorInfo.Templet.Doct.Name = this.personHelper.GetName(drugRecipe.Doct.ID);

                Function.IDrugPrint.OutpatientInfo = patientInfo;

                Function.IDrugPrint.AddAllData(al);
                Function.IDrugPrint.Print();

                return 1;
            }

            #endregion

            #region ��ȡ��ǩ��ҳ��
            string privCombo = "";												//�ϴ�ҽ����Ϻ�
            int iRecipeTotNum = 0;												//�������ӡ��ǩ��ҳ��
            string recipeNo = "";		//������
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut temp in al)
            {
                temp.SendWindow = sendWindow;
                if (privCombo == temp.CombNO && temp.CombNO != "")
                {
                    continue;
                }
                else
                {
                    iRecipeTotNum = iRecipeTotNum + 1;
                    privCombo = temp.CombNO;
                }

                recipeNo = temp.RecipeNO;
            }
            #endregion

            Function.IDrugPrint.LabelTotNum = iRecipeTotNum;
            Function.IDrugPrint.DrugTotNum = al.Count;
            if (patientInfo != null)
            {
                patientInfo.User02 = al.Count.ToString();
                patientInfo.User01 = drugRecipe.FeeOper.OperTime.ToString();

                patientInfo.DoctorInfo.Templet.Doct.Name = this.personHelper.GetName(drugRecipe.Doct.ID);

                patientInfo.User03 = drugRecipe.RecipeNO;

                Function.IDrugPrint.OutpatientInfo = patientInfo;
            }

            privCombo = "-1";
            ArrayList alCombo = new ArrayList();

            #region ��ӡ��ʽ��  ���ݷ��鴫��

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in al)
            {
                info.SendWindow = sendWindow;
                if (privCombo == "-1" || (privCombo == info.CombNO && info.CombNO != ""))
                {
                    alCombo.Add(info);
                    privCombo = info.CombNO;
                    continue;
                }
                else			//��ͬ������
                {
                    if (alCombo.Count == 1)
                        Function.IDrugPrint.AddSingle(alCombo[0] as FS.HISFC.Models.Pharmacy.ApplyOut);
                    else
                        Function.IDrugPrint.AddCombo(alCombo);
                    Function.IDrugPrint.Print();

                    privCombo = info.CombNO;
                    alCombo = new ArrayList();

                    alCombo.Add(info);
                }
            }
            if (alCombo.Count == 0)
            {
                return 1;
            }
            if (alCombo.Count > 1)
            {
                Function.IDrugPrint.AddCombo(alCombo);
            }
            else
            {
                Function.IDrugPrint.AddSingle(alCombo[0] as FS.HISFC.Models.Pharmacy.ApplyOut);
            }

            Function.IDrugPrint.Print();

            #endregion

            return 1;
        }

        protected virtual bool IsValid()
        {
            DateTime dt1 = FS.FrameWork.Function.NConvert.ToDateTime(this.dtBegin.Text);
            DateTime dt2 = FS.FrameWork.Function.NConvert.ToDateTime(this.dtEnd.Text);
            if (dt1 >= dt2)
            {
                MessageBox.Show(Language.Msg("��ѯ ��ʼʱ��Ӧ������ֹʱ��"));
                return false;
            }

            return true;
        }

        /// <summary>
        /// ���ݲ�ѯ
        /// </summary>
        protected void QueryData()
        {
            if (!this.IsValid())
            {
                return;
            }
            
            try
            {
                this.neuSpread1_Sheet1.DataSource = null;
                this.neuSpread1_Sheet1.Rows.Count = 0;

                DataSet dsData = new DataSet();

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ ���Ժ�...");
                Application.DoEvents();

                if (this.cmbQueryType.SelectedValue.ToString() == "D")
                {
                    if (this.txtQueryData.Text == "")
                        this.txtQueryData.Tag = "";
                    if (this.GetDrugDataSet(ref dsData, this.txtQueryData.Tag) == 1)
                    {
                        this.neuSpread1_Sheet1.DataSource = dsData;

                        this.SetFP();
                        this.ExpandFp(true);
                    }
                }
               // else if (this.cmbQueryType.SelectedValue.ToString() == "4")
               //{
               //    if (this.txtQueryData.Text == "")
               //        this.txtQueryData.Tag = "";
               //    //if (this.GetDrugDataSet(ref dsData, this.txtQueryData.Tag) == 1)
               //    //{
               //    //    this.neuSpread1_Sheet1.DataSource = dsData;
               //    //    this.SetFP();
               //    //    this.ExpandFp(true);
               //    //}

                
               // }
                else
                {
                    if (this.GetDataSet(ref dsData) == 1)
                    {
                        this.neuSpread1_Sheet1.DataSource = dsData;

                        this.SetFP();
                        this.ExpandFp(true);
                    }
                }

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ��ȡSql����
        /// </summary>
        /// <param name="isHead">�Ƿ�ͷ��Ϣ��ѯ</param>
        /// <param name="strIndex">Sql�������</param>
        private void GetSqlIndex(bool isHead, ref string strIndex)
        {
            if (isHead)
            {
                if (this.ckBlurry.Checked)
                    strIndex = "Pharmacy.DrugStore.RecipeQuery.Head.2";
                else
                    strIndex = "Pharmacy.DrugStore.RecipeQuery.Head.1";
            }
            else
            {
                strIndex = "Pharmacy.DrugStore.RecipeQuery.Detail";
            }
        }

        /// <summary>
        /// ��ȡ����ѯ����
        /// </summary>
        /// <param name="queryData"></param>
        private void GetQueryData(ref string queryData)
        {
            switch (this.cmbQueryType.SelectedValue.ToString())
            {
                case "A":		//ȫ��
                    queryData = "A";
                    break;
                case "0":		//��������
                    queryData = this.txtQueryData.Text.PadLeft(10, '0');
                    break;
                case "1":		//��Ʊ��
                    queryData = this.txtQueryData.Text.PadLeft(12, '0');
                    break;
                case "2":		//����
                case "3":		//������
                    queryData = this.txtQueryData.Text;
                    break;
                case "4":
                    queryData = this.txtQueryData.Tag.ToString();
                    break;
            }
        }

        /// <summary>
        /// ִ��Sql��� ��ȡ����ͷ��Ϣ
        /// </summary>
        /// <param name="dsData">��ѯ���DataSet</param>
        /// <returns></returns>
        private int GetDataSet(ref DataSet dsData)
        {
            dsData = new DataSet();
            DataTable dtHead = new DataTable("Head");
            DataTable dtDetail = new DataTable("Detail");
            DataSet dsHead = new DataSet();

            DateTime dt1 = FS.FrameWork.Function.NConvert.ToDateTime(this.dtBegin.Text);
            DateTime dt2 = FS.FrameWork.Function.NConvert.ToDateTime(this.dtEnd.Text);

            string strIndex = "";
            string strQueryData = "";

            this.GetSqlIndex(true, ref strIndex);
            this.GetQueryData(ref strQueryData);
            if (this.ckBlurry.Checked)
            {
                strQueryData = "%" + strQueryData + "%";
            }
            //��ȡͷ��Ϣ
            this.drugStoreManager.ExecQuery(strIndex, ref dsHead, dt1.ToString(), dt2.ToString(), strQueryData, this.cmbQueryType.SelectedValue.ToString(), this.operDept.ID);
            if (dsHead == null)
            {
                MessageBox.Show("���ݼ��ش���" + this.drugStoreManager.Err);
                return -1;
            }
            //��ȡ��ϸ��Ϣ   {0BD715F2-C09F-4b75-84D4-E9B45DAAEA96}
            string execDetailSql = "";
            if (this.ckBlurry.Checked)
            {
                execDetailSql = string.Format(this.strDetailSql2, dt1.ToString(), dt2.ToString(), strQueryData, this.cmbQueryType.SelectedValue.ToString(), this.operDept.ID);
            }
            else
            {
                execDetailSql = string.Format(this.strDetailSql1, dt1.ToString(), dt2.ToString(), strQueryData, this.cmbQueryType.SelectedValue.ToString(), this.operDept.ID);
            }

            DataSet dsTotDetail = new DataSet();
            this.drugStoreManager.ExecQuery(execDetailSql, ref dsTotDetail);
            if (dsTotDetail == null)
            {
                MessageBox.Show("���ݼ��ش���" + this.drugStoreManager.Err);
                return -1;
            }

            if (dsHead != null && dsHead.Tables.Count > 0 && dsHead.Tables[0].Rows.Count > 0)
            {
                dtHead = dsHead.Tables[0];
                dtHead.TableName = "Head";
                dsHead.Tables.Remove(dsHead.Tables[0]);
                string str = "";
                for (int i = 0; i < dtHead.Rows.Count; i++)
                {
                    DataSet dsDetail = new DataSet();
                    str = dtHead.Rows[i]["������"].ToString();

                    #region ������ʾ �ɱ���ת��Ϊ����
                    dtHead.Rows[i]["����"] = this.drugStoreManager.GetAge(FS.FrameWork.Function.NConvert.ToDateTime(dtHead.Rows[i]["����"].ToString()));
                    if (this.personHelper != null)
                    {
                        if (dtHead.Rows[i]["��ҩ��"] != null && dtHead.Rows[i]["��ҩ��"].ToString() != "")
                        {
                            dtHead.Rows[i]["��ҩ��"] = this.personHelper.GetName(dtHead.Rows[i]["��ҩ��"].ToString());
                        }
                        if (dtHead.Rows[i]["��ҩ��"] != null && dtHead.Rows[i]["��ҩ��"].ToString() != "")
                        {
                            dtHead.Rows[i]["��ҩ��"] = this.personHelper.GetName(dtHead.Rows[i]["��ҩ��"].ToString());
                        }
                        if (dtHead.Rows[i]["����ҽ��"] != null && dtHead.Rows[i]["����ҽ��"].ToString() != "")
                        {
                            dtHead.Rows[i]["����ҽ��"] = this.personHelper.GetName(dtHead.Rows[i]["����ҽ��"].ToString());
                        }
                    }
                    if (this.drugTerminalHelper != null)
                    {
                        if (dtHead.Rows[i]["��ҩ̨"] != null && dtHead.Rows[i]["��ҩ̨"].ToString() != "")
                        {
                            if (this.drugTerminalHelper.GetObjectFromID(dtHead.Rows[i]["��ҩ̨"].ToString()) != null)
                            {
                                dtHead.Rows[i]["��ҩ̨"] = this.drugTerminalHelper.GetName(dtHead.Rows[i]["��ҩ̨"].ToString());
                            }
                            else
                            {
                                dtHead.Rows[i]["��ҩ̨"] = this.drugTerminalHelper.GetName(dtHead.Rows[i]["��ҩ̨"].ToString());
                                //dtHead.Rows[i]["��ҩ̨"] = "��ɾ��̨";
                            }
                        }
                    }
                    if (this.sendTerminalHelper != null)
                    {
                        if (dtHead.Rows[i]["��ҩ̨"] != null && dtHead.Rows[i]["��ҩ̨"].ToString() != "")
                        {
                            if (this.sendTerminalHelper.GetObjectFromID(dtHead.Rows[i]["��ҩ̨"].ToString()) != null)
                            {
                                dtHead.Rows[i]["��ҩ̨"] = this.sendTerminalHelper.GetName(dtHead.Rows[i]["��ҩ̨"].ToString());
                            }
                            else
                            {
                                dtHead.Rows[i]["��ҩ̨"] = "��ɾ����";
                            }
                        }
                    }

                    if (dtHead.Rows[i]["�Ա�"] != null)
                    {
                        switch (dtHead.Rows[i]["�Ա�"].ToString())
                        {
                            case "M":
                                dtHead.Rows[i]["�Ա�"] = "��";
                                break;
                            case "F":
                                dtHead.Rows[i]["�Ա�"] = "Ů";
                                break;
                            case "U":
                                dtHead.Rows[i]["�Ա�"] = "δ֪";
                                break;

                        }
                    }
                    #endregion

                    #region ��/��ҩ������ʾ

                    if (FS.FrameWork.Function.NConvert.ToDateTime(dtHead.Rows[i]["��ҩʱ��"]) == System.DateTime.MinValue)
                    {
                        dtHead.Rows[i]["��ҩʱ��"] = "";
                    }
                    if (FS.FrameWork.Function.NConvert.ToDateTime(dtHead.Rows[i]["��ҩʱ��"]) == System.DateTime.MinValue)
                    {
                        dtHead.Rows[i]["��ҩʱ��"] = "";
                    }

                    #endregion


                    //{0BD715F2-C09F-4b75-84D4-E9B45DAAEA96}  ����ѭ���ڵ����ݻ�ȡ
                    //this.GetSqlIndex(false, ref strIndex);
                    //this.itemManager.ExecQuery(strIndex, ref dsDetail, str);

                    //if (dsDetail != null && dsDetail.Tables.Count > 0 && dsDetail.Tables[0].Rows.Count > 0)
                    //{
                    //    if (i == 0)
                    //    {
                    //        dtDetail = dsDetail.Tables[0];
                    //        dtDetail.TableName = "Detail";
                    //        dsDetail.Tables.Remove(dsDetail.Tables[0]);
                    //    }
                    //    else
                    //    {
                    //        for (int j = 0; j < dsDetail.Tables[0].Rows.Count; j++)
                    //        {
                    //            dtDetail.ImportRow(dsDetail.Tables[0].Rows[j]);
                    //        }
                    //    }
                    //}
                }

                dsData.Tables.Add(dtHead);

                //{0BD715F2-C09F-4b75-84D4-E9B45DAAEA96}
                dtDetail = dsTotDetail.Tables[0];
                dtDetail.TableName = "Detail";
                dsTotDetail.Tables.RemoveAt(0);
                dsData.Tables.Add(dtDetail);

                try
                {
                    dsData.Relations.Add(dtHead.Columns["������"], dtDetail.Columns["������"]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString() + "\n" + ex.Message);
                }
            }
            else
            {
                return 0;
            }

            return 1;
        }

        /// <summary>
        /// ִ��Sql��� ��ȡ������ϸ��Ϣ
        /// </summary>
        /// <param name="dsData">������ϸ��Ϣ</param>
        /// <param name="queryData"></param>
        /// <returns></returns>
        private int GetDrugDataSet(ref DataSet dsData, object queryData)
        {
            string drugCode = "";
            if (queryData == null || queryData.ToString() == "")
            {
                MessageBox.Show(Language.Msg("��ѡ���ѯҩƷ"));
            }
            else
            {
                drugCode = queryData.ToString();
            }

            dsData = new DataSet();
            DataTable dtHead = new DataTable("Head");
            DataTable dtDetail = new DataTable("Detail");
            DataSet dsHead = new DataSet();
            string str = "";

            DateTime dt1 = FS.FrameWork.Function.NConvert.ToDateTime(this.dtBegin.Text);
            DateTime dt2 = FS.FrameWork.Function.NConvert.ToDateTime(this.dtEnd.Text);

            this.drugStoreManager.ExecQuery("Pharmacy.DrugStore.Recipe.DrugHead", ref dsHead, dt1.ToString(), dt2.ToString(), ((FS.HISFC.Models.Base.Employee)this.itemManager.Operator).Dept.ID, drugCode);

            if (dsHead != null && dsHead.Tables.Count > 0 && dsHead.Tables[0].Rows.Count > 0)
            {
                dtHead = dsHead.Tables[0];
                dtHead.TableName = "Head";
                dsHead.Tables.Remove(dsHead.Tables[0]);
                for (int i = 0; i < dtHead.Rows.Count; i++)
                {
                    DataSet dsDetail = new DataSet();
                    str = dtHead.Rows[i]["ҩƷ����"].ToString();
                    this.itemManager.ExecQuery("Pharmacy.DrugStore.Recipe.DrugDetail", ref dsDetail, dt1.ToString(), dt2.ToString(), ((FS.HISFC.Models.Base.Employee)this.itemManager.Operator).Dept.ID, str);
                    if (dsDetail != null && dsDetail.Tables.Count > 0 && dsDetail.Tables[0].Rows.Count > 0)
                    {
                        if (i == 0)
                        {
                            dtDetail = dsDetail.Tables[0];
                            dtDetail.TableName = "Detail";
                            dsDetail.Tables.Remove(dsDetail.Tables[0]);
                        }
                        else
                        {
                            for (int j = 0; j < dsDetail.Tables[0].Rows.Count; j++)
                            {
                                dtDetail.ImportRow(dsDetail.Tables[0].Rows[j]);
                            }
                        }
                    }
                }

                dsData.Tables.Add(dtHead);

                dsData.Tables.Add(dtDetail);

                for (int i = 0; i < dtDetail.Rows.Count; i++)
                {
                    dtDetail.Rows[i]["����"] = this.itemManager.GetAge(FS.FrameWork.Function.NConvert.ToDateTime(dtDetail.Rows[i]["����"].ToString()));

                    #region ��/��ҩ������ʾ

                    if (FS.FrameWork.Function.NConvert.ToDateTime(dtDetail.Rows[i]["��ҩʱ��"]) == System.DateTime.MinValue)
                    {
                        dtDetail.Rows[i]["��ҩʱ��"] = "";
                    }
                    if (FS.FrameWork.Function.NConvert.ToDateTime(dtDetail.Rows[i]["��ҩʱ��"]) == System.DateTime.MinValue)
                    {
                        dtDetail.Rows[i]["��ҩʱ��"] = "";
                    }

                    if (dtDetail.Rows[i]["����ҽ��"] != null && dtDetail.Rows[i]["����ҽ��"].ToString() != "")
                    {
                        dtDetail.Rows[i]["����ҽ��"] = this.personHelper.GetName(dtDetail.Rows[i]["����ҽ��"].ToString());
                    }

                    #endregion
                }

                try
                {
                    dsData.Relations.Add(dtHead.Columns["ҩƷ����"], dtDetail.Columns["ҩƷ����"]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString() + "\n" + ex.Message);
                }
            }
            else
            {
                return 0;
            }

            return 1;
        }

        /// <summary>
        /// Fp��ʾչ��
        /// </summary>
        /// <param name="isExpand"></param>
        private void ExpandFp(bool isExpand)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                this.neuSpread1_Sheet1.ExpandRow(i, isExpand);
            }
            if (isExpand)
            {
                this.btnExpand.Text = "ȫ���ϲ�";
            }
            else
            {
                this.btnExpand.Text = "ȫ��չ��";
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Export()
        {
            if (this.neuSpread1.Export() == 1)
            {
                MessageBox.Show(Language.Msg("�����ɹ�"));
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                this.operDept = ((FS.HISFC.Models.Base.Employee)this.drugStoreManager.Operator).Dept;

                this.DataInit();

                //{9FA792B0-A60F-48d8-A3F5-1C52450C44A5} ��ȡ��ӡ���� ȡ��ԭ���� ���ڱ���ӿ�ά���ڶ�ʵ�ֽ�������
                object factoryInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Pharmacy.IOutpatientPrintFactory)) as FS.HISFC.BizProcess.Interface.Pharmacy.IOutpatientPrintFactory;
                if (factoryInstance != null)
                {
                    this.factory = factoryInstance as FS.HISFC.BizProcess.Interface.Pharmacy.IOutpatientPrintFactory;                  
                }

                this.cmbQueryType.DropDownStyle = ComboBoxStyle.DropDownList;
                this.cmbQueryType.SelectedIndexChanged += new EventHandler(cmbQueryType_SelectedIndexChanged);
            }
            catch
            { }

            base.OnLoad(e);
        }

        void cmbQueryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbQueryType.Text == "��Ʊ��" || this.cmbQueryType.Text == "��������")
            {
                this.ckBlurry.Enabled = false;
            }
            else
            {
                this.ckBlurry.Enabled = true;
            }
        }

        private void fpSpread1_ChildViewCreated(object sender, FarPoint.Win.Spread.ChildViewCreatedEventArgs e)
        {
            e.SheetView.DataAutoCellTypes = false;
            e.SheetView.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            e.SheetView.DefaultStyle.BackColor = System.Drawing.Color.White;
            e.SheetView.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            e.SheetView.DataAutoSizeColumns = false;

            if (e.SheetView.Columns.Count > 7)
            {
                FarPoint.Win.Spread.CellType.NumberCellType numCell = new FarPoint.Win.Spread.CellType.NumberCellType();
                numCell.DecimalPlaces = 4;
                e.SheetView.Columns[7].CellType = numCell;
            }

            if (this.cmbQueryType.SelectedValue != null && this.cmbQueryType.SelectedValue.ToString() == "D")
            {
                e.SheetView.Columns[0].Visible = false;
                e.SheetView.Columns[0].Width = 50F;		//ҩƷ����
                e.SheetView.Columns[1].Visible = true;
                e.SheetView.Columns[1].Width = 60F;		//����
                e.SheetView.Columns[2].Width = 35F;		//�Ա�
                e.SheetView.Columns[3].Width = 35F;		//����
                e.SheetView.Columns[4].Width = 70F;		//������
                e.SheetView.Columns[5].Width = 80F;		//��Ʊ��
                e.SheetView.Columns[6].Width = 60F;		//��ҩ����
                e.SheetView.Columns[7].Width = 50F;		//��ҩ��
                e.SheetView.Columns[7].Visible = true;
                e.SheetView.Columns[8].Width = 120F;	//��ҩʱ��
                e.SheetView.Columns[9].Width = 60F;		//��ҩ����
                e.SheetView.Columns[10].Width = 50F;	//��ҩ��
                e.SheetView.Columns[11].Width = 120F;	//��ҩʱ��
                e.SheetView.Columns[12].Width = 60F;	//����ҽ��

                e.SheetView.Columns[13].Visible = false;

                for (int i = 0; i < e.SheetView.Rows.Count; i++)
                {
                    if (e.SheetView.Cells[i, 13].Text == "0")
                    {
                        e.SheetView.Rows[i].ForeColor = System.Drawing.Color.Red;
                    }
                }

            }
            else
            {
                #region ��ͨ��ʽ��
                e.SheetView.Columns[0].Visible = false;			//������
                e.SheetView.Columns[1].Visible = false;			//��Ч�Դ���
                e.SheetView.Columns[2].Width = 184F;			//����[���]
                e.SheetView.Columns[3].Width = 80F;				//ÿ���� ��λ
                e.SheetView.Columns[4].Width = 60F;				//�÷�
                e.SheetView.Columns[5].Width = 60F;				//Ƶ��
                e.SheetView.Columns[6].Width = 80F;				//���� ��λ
                e.SheetView.Columns[7].Width = 70F;				//���ۼ�
                e.SheetView.Columns[8].Width = 100F;				//��Ч��

                for (int i = 0; i < e.SheetView.Rows.Count; i++)
                {
                    if (e.SheetView.Cells[i, 1].Text == "0")
                    {
                        e.SheetView.Rows[i].ForeColor = System.Drawing.Color.Red;
                    }
                }

                #region ���Ӻϼ�
                try
                {
                    int iIndex = e.SheetView.Rows.Count;
                    e.SheetView.Rows.Add(iIndex, 1);
                    e.SheetView.Cells[iIndex, 0].Text = e.SheetView.Cells[0, 0].Text;
                    e.SheetView.Cells[iIndex, 2].Text = "�ϼ�";
                    e.SheetView.Cells[iIndex, 8].Formula = "SUM(I1:I" + iIndex.ToString() + ")";
                }
                catch { }
                #endregion

                #endregion
            }
        }

        private void btnExpand_Click(object sender, System.EventArgs e)
        {
            if (this.btnExpand.Text == "ȫ���ϲ�")
            {
                this.ExpandFp(false);
            }
            else
            {
                this.ExpandFp(true);
            }
        }

        private void txtQueryData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.QueryData();
            }
            if (e.KeyCode == Keys.Space && this.cmbQueryType.SelectedValue != null && this.cmbQueryType.SelectedValue.ToString() == "D" && this.drugCollectioon != null)
            {
                FS.FrameWork.Models.NeuObject drugInfo = new FS.FrameWork.Models.NeuObject();
                if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.drugCollectioon, new string[]{"����","��Ʒ����","���"},new bool[] {false,true,true,false,false,false,false,false,false},new int[]{100,160,80},ref drugInfo) == 0)
                {
                    return;
                }
                else
                {
                    this.txtQueryData.Text = drugInfo.Name;
                    this.txtQueryData.Tag = drugInfo.ID;

                    this.QueryData();
                }
            }
                //��ׯ����
            else if (e.KeyCode == Keys.Space && this.cmbQueryType.SelectedValue != null && this.cmbQueryType.SelectedValue.ToString() == "4" && this.personHelper!= null)
            {
                FS.FrameWork.Models.NeuObject personInfo = new FS.FrameWork.Models.NeuObject();
                if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.personList, new string[] { "����", "��Ա����", "" }, new bool[] { false, true, true, false, false, false, false, false, false }, new int[] { 100, 160, 80 }, ref personInfo) == 0)
                {
                    return;
                }
                else
                {
                    this.txtQueryData.Text = personInfo.Name;
                    this.txtQueryData.Tag = personInfo.ID;

                    this.QueryData();
                }
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Space && this.cmbQueryType.Focused)
            {
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] printType = new Type[1];
                printType[0] = typeof(FS.HISFC.BizProcess.Interface.Pharmacy.IOutpatientPrintFactory);

                return printType;
            }
        }

        #endregion
    }
}
