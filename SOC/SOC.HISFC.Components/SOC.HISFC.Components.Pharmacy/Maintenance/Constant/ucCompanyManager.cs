using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using System.Collections;
using FarPoint.Win.Spread;

namespace FS.SOC.HISFC.Components.Pharmacy.Maintenance
{
    /// <summary>
    /// [��������: ҩƷ������˾����������ά��]<br></br>
    /// [�� �� ��: Liangjz]<br></br>
    /// [����ʱ��: 2007-07]<br></br>
    /// <�޸ļ�¼>
    ///    1.�������ҹ�����˾ά�����������ַ�У�� by Sunjh 2010-8-25 {90875342-BE12-41fb-8F26-4EFF1889E7B2}
    ///    2.�޸��ַ����˵����� by Sunjh 2010-9-2 {A03A105A-8D12-45c0-8066-90F159BEB4D8}
    ///    3.�����ⲿ��������ά������
    /// </�޸ļ�¼>
    /// </summary>
    public partial class ucCompanyManager : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCompanyManager()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// ���ݱ�
        /// </summary>
        private DataTable dt = new DataTable();

        /// <summary>
        /// ҩƷ������
        /// </summary>
        private FS.SOC.HISFC.BizLogic.Pharmacy.Constant phaConsManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Constant();

        /// <summary>
        /// ά���Ĺ�˾���� 
        /// </summary>
        private CompanyType type = CompanyType.��������;

        /// <summary>
        /// �ɴ洢�Ĺ�˾���������
        /// </summary>
        private int nameMaxLength = 100;

        /// <summary>
        /// ��ά���Ĺ�˾��ַ�����
        /// </summary>
        private int addressMaxLength = 200;

        /// <summary>
        /// ��ά���Ĺ�˾��ϵ��ʽ�����
        /// </summary>
        private int relativeMaxLength = 100;

        /// <summary>
        /// ��ά���Ĺ�˾GSP��Ϣ�����
        /// </summary>
        private int gspMaxLength = 100;

        /// <summary>
        /// ��ά���Ĺ�˾GMP��Ϣ�����
        /// </summary>
        private int gmpMaxLength = 200;

        /// <summary>
        /// ��ά���Ĺ�˾�������������
        /// </summary>
        private int bankMaxLength = 200;

        /// <summary>
        /// ��ά���Ĺ�˾�����ʺ������
        /// </summary>
        private int accountMaxLength = 100;

        /// <summary>
        /// ���������Ƿ����������Ŀ
        /// </summary>
        private bool isBankNeed = false;

        /// <summary>
        /// ��ϵ��ʽ�Ƿ����������Ŀ
        /// </summary>
        private bool isRelativeNeed = false;

        #endregion

        #region ����

        /// <summary>
        /// ά���Ĺ�˾����
        /// </summary>
        [Description("������ά���Ĺ�˾����"),Category("����")]
        public CompanyType Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;

                this.SetCellType();
            }
        }

        /// <summary>
        /// �ɴ洢�Ĺ�˾��ַ�����(�������ַ�)
        /// </summary>
        [Description("�ɴ洢�Ĺ�˾��ַ�����(�������ַ�)"), Category("��Ч��У��"), DefaultValue(100)]
        public int NameMaxLength
        {
            get
            {
                return this.nameMaxLength;
            }
            set
            {
                this.nameMaxLength = value;
            }
        }

        /// <summary>
        /// ��ά���Ĺ�˾��ַ�����(�������ַ�)
        /// </summary>
        [Description("��ά���Ĺ�˾��ַ�����(�������ַ�)"), Category("��Ч��У��"), DefaultValue(200)]
        public int AddressMaxLength
        {
            get
            {
                return this.addressMaxLength;
            }
            set
            {
                this.addressMaxLength = value;
            }
        }

        /// <summary>
        /// ��ά���Ĺ�˾��ϵ��ʽ�����(�������ַ�)
        /// </summary>
        [Description("��ά���Ĺ�˾��ϵ��ʽ�����(�������ַ�)"), Category("��Ч��У��"), DefaultValue(100)]
        public int RelativeMaxLength
        {
            get
            {
                return this.relativeMaxLength;
            }
            set
            {
                this.relativeMaxLength = value;
            }
        }

        /// <summary>
        /// ��ά���Ĺ�˾GSP��Ϣ�����(�������ַ�)
        /// </summary>
        [Description("��ά���Ĺ�˾GSP��Ϣ�����(�������ַ�)"), Category("��Ч��У��"), DefaultValue(100)]
        public int GSPMaxLength
        {
            get
            {
                return this.gspMaxLength;
            }
            set
            {
                this.gspMaxLength = value;
            }
        }

        /// <summary>
        /// ��ά���Ĺ�˾GMP��Ϣ�����(�������ַ�)
        /// </summary>
        [Description("��ά���Ĺ�˾GMP��Ϣ�����(�������ַ�)"), Category("��Ч��У��"), DefaultValue(200)]
        public int GMPMaxLength
        {
            get
            {
                return this.gmpMaxLength;
            }
            set
            {
                this.gmpMaxLength = value;
            }
        }

        /// <summary>
        /// ��ά���Ĺ�˾�������������(�������ַ�)
        /// </summary>
        [Description("��ά���Ĺ�˾�������������(�������ַ�)"), Category("��Ч��У��"), DefaultValue(200)]
        public int BankMaxLength
        {
            get
            {
                return this.bankMaxLength;
            }
            set
            {
                this.bankMaxLength = value;
            }
        }

        /// <summary>
        /// ��ά���Ĺ�˾�����ʺ������(�������ַ�)
        /// </summary>
        [Description("��ά���Ĺ�˾�����ʺ������(�������ַ�)"), Category("��Ч��У��"), DefaultValue(100)]
        public int AccountMaxLength
        {
            get
            {
                return this.accountMaxLength;
            }
            set
            {
                this.accountMaxLength = value;
            }
        }

        /// <summary>
        /// ���������Ƿ����������Ŀ
        /// </summary>
        [Description("���������Ƿ����������Ŀ"), Category("��Ч��У��"), DefaultValue(false)]
        public bool IsBankNeed
        {
            get
            {
                return this.isBankNeed;
            }
            set
            {
                this.isBankNeed = value;
            }
        }

        /// <summary>
        /// ��ϵ��ʽ�Ƿ����������Ŀ
        /// </summary>
        [Description("��ϵ��ʽ�Ƿ����������Ŀ"), Category("��Ч��У��"), DefaultValue(false)]
        public bool IsRelativeNeed
        {
            get
            {
                return this.isRelativeNeed;
            }
            set
            {
                this.isRelativeNeed = value;
            }
        }

        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            #region {9768C6B1-5F8C-484c-AFBC-0B2D8CC55400}
            toolBarService.AddToolButton("����", "����", FS.FrameWork.WinForms.Classes.EnumImageList.A����, true, false, null); 
            #endregion
           
            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "����")
            {
                this.AddData();
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();

            return 1;
        }

        public override int Export(object sender, object neuObject)
        {
            if (this.neuSpread1_Sheet1.Rows.Count > 0)
            {
                if (this.neuSpread1.Export() == 1)
                {
                    MessageBox.Show(Language.Msg("�����ɹ�"));
                }
            }

            return 1;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.ShowData(this.type);

            return base.OnQuery(sender, neuObject);
        }

        #endregion

        #region ��ʼ�������ݱ����

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            InputMap im;

            im = this.neuSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            this.InitDataSet();

            return 1;
        }

        /// <summary>
        ///  ��ʼ��DataSet
        /// </summary>
        private void InitDataSet()
        {
            this.neuSpread1_Sheet1.DataAutoSizeColumns = false;           

            //��������
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtBol = System.Type.GetType("System.Boolean");

            //��myDataTable�������
            this.dt.Columns.AddRange(new DataColumn[] {														
														new DataColumn("��λ����",   dtStr),														
													    new DataColumn("��ϵ��ʽ",   dtStr),
                                                        new DataColumn("��������",   dtStr),
														new DataColumn("�����ʺ�",   dtStr),
                                                        new DataColumn("�Ӽ���",     dtDec),														                                                        
														new DataColumn("ƴ����",     dtStr),
														new DataColumn("�����",     dtStr),
														new DataColumn("�Զ�����",   dtStr),
                                                        new DataColumn("��Ч",       dtBol),
                                                        new DataColumn("GSP",        dtStr),
                                                        new DataColumn("GMP",        dtStr),
                                                        new DataColumn("��ַ",       dtStr),
                										new DataColumn("��ע",       dtStr),
                                                        new DataColumn("��˾����",   dtStr),
														new DataColumn("��˾����",   dtStr)
											        });

            this.neuSpread1_Sheet1.DataSource = this.dt.DefaultView;
        }

        /// <summary>
        /// �����ݱ��ڼ�������
        /// </summary>
        /// <param name="company"></param>
        private void AddDataToTable(FS.HISFC.Models.Pharmacy.Company company)
        {
            this.dt.Rows.Add(new object[] {
					                                company.Name,					                                
				                                    company.RelationCollection.Relative,
                                                    company.OpenBank,
                                                    company.OpenAccounts,
                                                    company.ActualRate,
                                                    company.SpellCode,
                                                    company.WBCode,
                                                    company.UserCode,
                                                    company.IsValid,
                                                    company.GSPInfo,
                                                    company.GMPInfo,
                                                    company.RelationCollection.Address,
                                                    company.Memo,
                                                    company.ID,
                                                    company.Type
											        });
        }

        /// <summary>
        /// �����ݱ��ڻ�ȡ����
        /// </summary>
        /// <param name="row">���ȡ���ݵ����ݱ���</param>
        /// <returns></returns>
        private FS.HISFC.Models.Pharmacy.Company GetDataFromTable(DataRow row)
        {
            FS.HISFC.Models.Pharmacy.Company company = new FS.HISFC.Models.Pharmacy.Company();

            company.ID = row["��˾����"].ToString();                            //��˾����
            company.Name = row["��λ����"].ToString();                          //��˾����
            company.RelationCollection.Address = row["��ַ"].ToString();        //��˾��ַ
            company.RelationCollection.Relative = row["��ϵ��ʽ"].ToString();   //��ϵ��ʽ
            company.GMPInfo = row["GMP"].ToString();                            //GMP��Ϣ
            company.GSPInfo = row["GSP"].ToString();                            //GSP��Ϣ
            company.SpellCode = row["ƴ����"].ToString();                       //ƴ����
            company.WBCode = row["�����"].ToString();                          //�����
            company.UserCode = row["�Զ�����"].ToString();                      //�Զ�����
            company.Type = ((int)this.type).ToString();                         //��˾����
            company.OpenBank = row["��������"].ToString();                      //��������
            company.OpenAccounts = row["�����ʺ�"].ToString();                  //�����ʺ�
            company.ActualRate = FS.FrameWork.Function.NConvert.ToDecimal(row["�Ӽ���"]);    //�Ӽ���
            company.Memo = row["��ע"].ToString();                              //��ע	
            company.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(row["��Ч"]);         //��Ч��

            return company;
        }

        #endregion

        #region Fp��CellType��ʽ��

        /// <summary>
        /// ������ʾ��ʽ
        /// </summary>
        private void SetCellType()
        {
            if (this.type == CompanyType.������˾)
            {
                this.SetCompany();
            }
            else if (this.type == CompanyType.��������)
            {
                    this.SetProducer();
            }
            else if (this.type == CompanyType.�ⲿ����)
            {
                this.SetUnit();
            }
            else
            {
                MessageBox.Show(Language.Msg("���빫˾���ʹ���"));
            }
        }

        /// <summary>
        /// �����������ҵ���ʾ��ʽ
        /// </summary>
        private void SetProducer()
        {
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColGMP].Visible = true;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColGSP].Visible = false;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColGrade].Visible = false;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColID].Visible = true;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColID].Locked = true;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColType].Visible = false;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColGMP].Width = 60;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColGSP].Width = 60;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColID].Width = 60F;
        }

        /// <summary>
        /// ���ù�����˾����ʾ��ʽ
        /// </summary>
        private void SetCompany()
        {
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColGSP].Visible = true;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColGMP].Visible = false;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColGrade].Visible = false;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColID].Visible = true;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColID].Locked = true;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColType].Visible = false;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColGMP].Width = 60;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColGSP].Width = 60;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColID].Width = 60F;
        }
        /// <summary>
        /// ����Ժ�����
        /// </summary>
        private void SetUnit()
        {
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColBank].Visible = false;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColAccount].Visible = false;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColGrade].Visible = false;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColGSP].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColGMP].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColID].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColType].Visible = false;

        }
        #endregion

        /// <summary>
        /// �����������е�������ʾFp��
        /// </summary>
        public void ShowData(CompanyType type)
        {
            //�������
            this.dt.Rows.Clear();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڼ�������,���Ժ�..."));
            Application.DoEvents();

            //ȡ��˾��¼
            ArrayList alCompany = this.phaConsManager.QueryCompany(((int)type).ToString(),false);
            if (alCompany == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(Language.Msg("���ع�˾���ݷ�������" + this.phaConsManager.Err));
                return;
            }

            FS.HISFC.Models.Pharmacy.Company company;

            for (int i = 0; i < alCompany.Count; i++)
            {
                company = alCompany[i] as FS.HISFC.Models.Pharmacy.Company;

                this.AddDataToTable(company);  
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            //�ύDataTable�еı仯��
            this.dt.AcceptChanges();

            this.SetCellType();
        }

        /// <summary>
        /// �������
        /// </summary>
        public void ClearData()
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;
        }

        /// <summary>
        /// ��������
        /// </summary>
        public void AddData()
        {
            this.dt.DefaultView.RowFilter = "";

            this.neuSpread1_Sheet1.ActiveRowIndex = this.neuSpread1_Sheet1.RowCount - 1;
            this.neuSpread1_Sheet1.ActiveColumnIndex = (int)ColumnSet.ColName;          

            this.dt.Rows.Add(this.dt.NewRow());
        }

        /// <summary>
        /// ɾ��һ����˾��¼
        /// </summary>
        public void DeleteData()
        {
            this.neuSpread1_Sheet1.Rows.Remove(this.neuSpread1_Sheet1.ActiveRowIndex, 1);
        }

        /// <summary>
        /// ͨ������Ĳ�ѯ�룬���������б�
        /// </summary>
        private void ChangeItem()
        {
            if (this.dt.Rows.Count == 0) return;

            try
            {
                string queryCode = "";

                queryCode = "%" + this.txtFilter.Text.Trim() + "%";

                string filter = "(ƴ���� LIKE '" + queryCode + "') OR " +
                    "(����� LIKE '" + queryCode + "') OR " +
                    "(�Զ����� LIKE '" + queryCode + "') OR " +
                    "(��λ���� LIKE '" + queryCode + "') ";

                //���ù�������
                this.dt.DefaultView.RowFilter = filter;

                this.SetCellType();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg(ex.Message));
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public int Save()
        {
            this.neuSpread1.StopCellEditing();

            foreach (DataRow dr in this.dt.Rows)
            {
                dr.EndEdit();
            }

            //��Ч���ж�
            if (!Valid())
            {
                return -1;
            };

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڱ���...");
            Application.DoEvents();

            bool isUpdate = false; //�ж��Ƿ���»���ɾ��������

            //ȡ�޸ĺ����ӵ�����
            DataTable dataChanges = this.dt.GetChanges(DataRowState.Modified | DataRowState.Added);
            string errInfo = "";
            ArrayList alCompany = new ArrayList();

            if (dataChanges != null)
            {
                foreach (DataRow row in dataChanges.Rows)
                {

                    //�������ݿ⴦������
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    FS.HISFC.Models.Pharmacy.Company company = this.GetDataFromTable(row);

                    //ִ�и��²������ȸ��£����û�гɹ������������
                    if (this.phaConsManager.SetCompany(company) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("���湫˾��Ϣ��������!" + this.phaConsManager.Err, MessageBoxIcon.Error);
                        return -1;
                    }
                    FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType = FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Modify;

                    if (string.IsNullOrEmpty(company.ID))
                    {
                        operType = FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add;
                    }

                    alCompany.Add(company);
                    if (Function.SendBizMessage(alCompany, operType, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.DrugCompany, ref errInfo) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("���湩����˾" + row["��λ����"].ToString() + "ʧ�ܣ�����ϵͳ����Ա��ϵ���������" + errInfo, MessageBoxIcon.Error);
                        return -1;
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();

                }
                dataChanges.AcceptChanges();

                isUpdate = true;
            }

            //ȡɾ��������
            dataChanges = this.dt.GetChanges(DataRowState.Deleted);
            alCompany.Clear();
            if (dataChanges != null)
            {
                dataChanges.RejectChanges();
                foreach (DataRow row in dataChanges.Rows)
                {
                    //�������ݿ⴦������
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    string companyID = row["��˾����"].ToString();        //��˾����		
                    //ִ��ɾ������
                    if (this.phaConsManager.DeleteCompany(companyID) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("ɾ��������˾" + row["��λ����"].ToString() + "��������" + this.phaConsManager.Err));
                        return -1;
                    }
                    alCompany.Add(this.GetDataFromTable(row));
                    if (Function.SendBizMessage(alCompany,FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Delete, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.DrugCompany, ref errInfo) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("ɾ��������˾" + row["��λ����"].ToString() + "ʧ�ܣ�����ϵͳ����Ա��ϵ���������" + errInfo, MessageBoxIcon.Error);
                        return -1;
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();
                }
                dataChanges.AcceptChanges();

                isUpdate = true;
            }


            if (isUpdate)
            {
                Function.ShowMessage("����ɹ���", MessageBoxIcon.None);
            }
            else
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return 1;
            }

            if (this.type == CompanyType.������˾)
            {
                SOC.HISFC.BizProcess.Cache.Pharmacy.ClearCompanyCache();
            }
            else if (type == CompanyType.��������)
            {
                SOC.HISFC.BizProcess.Cache.Pharmacy.ClearProducerCache();
            }
            else if(type== CompanyType.�ⲿ����)
            {
                SOC.HISFC.BizProcess.Cache.Pharmacy.ClearUnitCache();
            }

            //ˢ������
            this.ShowData(this.type);

            return 1;
        }

        /// <summary>
        ///  ��Ч���ж�
        /// </summary>
        private bool Valid()
        {
            int i = 1;

            //{2876B618-36A2-4faf-8DF1-7728D400ACA9} �ظ���˾�����ж�
            System.Collections.Generic.Dictionary<string,int> companyNameIndexDictionary = new Dictionary<string,int>();

            foreach (DataRow dr in this.dt.Rows)
            {
                if (dr["��λ����"].ToString() == "")
                {
                    MessageBox.Show(Language.Msg("��" + i.ToString() + "�е�λ���Ʋ���Ϊ��"));//{52A48F33-7979-447d-9925-1B1B3389929A}
                    return false;
                }
                //{2876B618-36A2-4faf-8DF1-7728D400ACA9} �ظ���˾�����ж�
                if (companyNameIndexDictionary.ContainsKey(dr["��λ����"].ToString()) == true)
                {
                    companyNameIndexDictionary[dr["��λ����"].ToString()] = companyNameIndexDictionary[dr["��λ����"].ToString()] + 1;
                }
                else
                {
                    companyNameIndexDictionary.Add(dr["��λ����"].ToString(), 1);
                }

                #region ��Ч��У��

                //�������ҹ�����˾ά�����������ַ�У�� by Sunjh 2010-8-25 {90875342-BE12-41fb-8F26-4EFF1889E7B2}
                string mStr = dr["��λ����"].ToString() + dr["��ϵ��ʽ"].ToString();

                if (this.isBankNeed)
                {
                    if (dr["��������"].ToString() == "")
                    {
                        MessageBox.Show(Language.Msg(dr["��λ����"].ToString() +" �������в���Ϊ��"));
                        return false;
                    }

                    if (dr["�����ʺ�"].ToString() == "")
                    {
                        MessageBox.Show(Language.Msg(dr["��λ����"].ToString() + " �����ʺŲ���Ϊ��"));
                        return false;
                    }
                    //�������ҹ�����˾ά�����������ַ�У�� by Sunjh 2010-8-25 {90875342-BE12-41fb-8F26-4EFF1889E7B2}
                    mStr = mStr + dr["��������"].ToString() + dr["�����˺�"].ToString();
                }

                if (this.isRelativeNeed)
                {
                    if (dr["��ϵ��ʽ"].ToString() == "")
                    {
                        MessageBox.Show(Language.Msg(dr["��λ����"].ToString() + " ��ϵ��ʽ����Ϊ��"));
                        return false;
                    }                    
                }

                //�������ҹ�����˾ά�����������ַ�У�� by Sunjh 2010-8-25 {90875342-BE12-41fb-8F26-4EFF1889E7B2}
                mStr = mStr + dr["��ַ"].ToString() + dr["��ע"].ToString() + dr["�Զ�����"].ToString() + dr["GSP"].ToString();

                if (!FS.FrameWork.Public.String.ValidMaxLengh(dr["��λ����"].ToString(), this.nameMaxLength))
                {
                    MessageBox.Show(Language.Msg(dr["��λ����"].ToString() + " ��λ���Ƴ���"));
                    return false;
                }

                if (!FS.FrameWork.Public.String.ValidMaxLengh(dr["��ַ"].ToString(), this.addressMaxLength))
                {
                    MessageBox.Show(Language.Msg(dr["��λ����"].ToString() + " ��λ��ַ����"));
                    return false;
                }

                if (!FS.FrameWork.Public.String.ValidMaxLengh(dr["��ϵ��ʽ"].ToString(), this.relativeMaxLength))
                {
                    MessageBox.Show(Language.Msg(dr["��λ����"].ToString() + " ��λ��ϵ��ʽ����"));
                    return false;
                }

                if (!FS.FrameWork.Public.String.ValidMaxLengh(dr["GMP"].ToString(), this.gmpMaxLength))
                {
                    MessageBox.Show(Language.Msg(dr["��λ����"].ToString() + " ��λGMP����"));
                    return false;
                }

                if (!FS.FrameWork.Public.String.ValidMaxLengh(dr["GSP"].ToString(), this.gspMaxLength))
                {
                    MessageBox.Show(Language.Msg(dr["��λ����"].ToString() + " ��λGSP����"));
                    return false;
                }

                if (!FS.FrameWork.Public.String.ValidMaxLengh(dr["��������"].ToString(), this.bankMaxLength))
                {
                    MessageBox.Show(Language.Msg(dr["��λ����"].ToString() + " ��λ�������г���"));
                    return false;
                }

                if (!FS.FrameWork.Public.String.ValidMaxLengh(dr["�����ʺ�"].ToString(), this.accountMaxLength))
                {
                    MessageBox.Show(Language.Msg(dr["��λ����"].ToString() + " ��λ�����ʺų���"));
                    return false;
                }

                //�������ҹ�����˾ά�����������ַ�У�� by Sunjh 2010-8-25 {90875342-BE12-41fb-8F26-4EFF1889E7B2}
                //{4AB4AC9F-23E0-4742-96DF-4446815E644C}
                string QueueName = FS.FrameWork.Public.String.TakeOffSpecialChar(mStr,new string[]{"'"});
                //�޸��ַ����˵����� by Sunjh 2010-9-2 {A03A105A-8D12-45c0-8066-90F159BEB4D8}
                if (QueueName != mStr)
                {
                    MessageBox.Show("�������������ַ�!" + mStr, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                #endregion

                i++;
            }
            //{2876B618-36A2-4faf-8DF1-7728D400ACA9} �ظ���˾�����ж�
            foreach (string key in companyNameIndexDictionary.Keys)
            {
                if (companyNameIndexDictionary[key] > 1)
                {
                    MessageBox.Show(key + " ��λ���ƴ����ظ� ��˶�����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);//{52A48F33-7979-447d-9925-1B1B3389929A}
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// ��ȡָ���еĹ�˾����ƴ����/�������Ϣ
        /// </summary>
        /// <param name="iRow">ָ��������</param>
        /// <returns></returns>
        private int GetSpell(int iRow)
        {
            if (this.neuSpread1_Sheet1.Cells[iRow, (int)ColumnSet.ColName].Text.ToString() == "")
            {
                return 1;
            }

            FS.HISFC.Models.Base.Spell spCode = new FS.HISFC.Models.Base.Spell();
            FS.HISFC.BizLogic.Manager.Spell spellManager = new FS.HISFC.BizLogic.Manager.Spell();

            spCode = (FS.HISFC.Models.Base.Spell)spellManager.Get(this.neuSpread1_Sheet1.Cells[iRow, (int)ColumnSet.ColName].Text.ToString());

            if (spCode != null && spCode.SpellCode != null)
            {
                if (spCode.SpellCode.Length > 10)
                    spCode.SpellCode = spCode.SpellCode.Substring(0, 10);
                if (spCode.WBCode.Length > 10)
                    spCode.WBCode = spCode.WBCode.Substring(0, 10);

                this.neuSpread1_Sheet1.Cells[iRow, (int)ColumnSet.ColSpell].Value = spCode.SpellCode;
                this.neuSpread1_Sheet1.Cells[iRow, (int)ColumnSet.ColWB].Value = spCode.WBCode;
            }

            return 1;
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                this.Init();

                this.ShowData(this.type);
            }
            catch { }

            base.OnLoad(e);
        }
        
        private void txtQueryCode_TextChanged(object sender, EventArgs e)
        {
            this.ChangeItem();
        }

        private void fpCompany_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            if (e.Column == 1)
            {
                this.GetSpell(e.Row);
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.neuSpread1.ContainsFocus)
            {
                if (keyData == Keys.Enter)
                {
                    if (this.neuSpread1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColName)
                    {
                        this.GetSpell(this.neuSpread1_Sheet1.ActiveRowIndex);
                    }

                    this.neuSpread1_Sheet1.ActiveColumnIndex++;
                }

            }
            return base.ProcessDialogKey(keyData);
        }                     

        #region ö����

        /// <summary>
        /// ά����˾����
        /// </summary>
        public enum CompanyType
        {
            ��������,
            ������˾,
            �ⲿ����,
        }

        /// <summary>
        /// ������
        /// </summary>
        private enum ColumnSet
        {
            /// <summary>
            /// ��λ����
            /// </summary>
            ColName,           
            /// <summary>
            /// ��ϵ��ʽ
            /// </summary>
            ColPhone,
            /// <summary>
            /// ��������
            /// </summary>
            ColBank,
            /// <summary>
            /// �ʺ�
            /// </summary>
            ColAccount,
            /// <summary>
            /// �Ӽ���
            /// </summary>
            ColGrade,           
            /// <summary>
            /// ƴ����
            /// </summary>
            ColSpell,
            /// <summary>
            /// �����
            /// </summary>
            ColWB,
            /// <summary>
            /// �Զ�����
            /// </summary>
            ColUserCode,
            /// <summary>
            /// ��Ч
            /// </summary>
            ColValid,
            /// <summary>
            /// GSP
            /// </summary>
            ColGSP,
            /// <summary>
            /// GMP
            /// </summary>
            ColGMP,
            /// <summary>
            /// ��ַ
            /// </summary>
            ColAddress,
            /// <summary>
            /// ��ע
            /// </summary>
            ColMemo,
            /// <summary>
            /// ����
            /// </summary>
            ColID,
            /// <summary>
            /// ����
            /// </summary>
            ColType

        }

        #endregion

    }
}
