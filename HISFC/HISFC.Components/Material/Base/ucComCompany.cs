using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;
using FarPoint.Win.Spread;
using FS.FrameWork.Management;
using FS.HISFC.Models.Material;

namespace FS.HISFC.Components.Material.Base
{
    /// <summary>		
    /// ucComCompany��ժҪ˵����<br></br>
    /// [��������: ������˾ά��]<br></br>
    /// [�� �� ��: ��־��]<br></br>
    /// [����ʱ��: 2007-11-26<br></br>
    /// 
    /// </summary>
    public partial class ucComCompany : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucComCompany()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// ��Ӧ����Ϣ��
        /// </summary>
        private FS.HISFC.BizLogic.Material.ComCompany comCompany = new FS.HISFC.BizLogic.Material.ComCompany();
       
        /// <summary>
        /// ���ݱ�
        /// </summary>
        private DataTable dt = new DataTable();

        /// <summary>
        /// ά���Ĺ�˾��� 
        /// </summary>
        private CompanyKind kind = CompanyKind.���ʳ���ʹ��;

        /// <summary>
        /// ά���Ĺ�˾���� 
        /// </summary>
        private CompanyType type = CompanyType.������˾;

        /// <summary>
        /// ά���Ĺ�˾��� 
        /// </summary>
        public string companyID;

        /// <summary>
        /// ������ά���ؼ�
        /// </summary>
        private ucComCompanyEdit ComEdit = null;
        private System.Windows.Forms.Form EditForm = null;

        /// <summary>
        /// ֻ������
        /// </summary>
        private bool isEditExpediency = true;
        #endregion

        #region ����

        /// <summary>
        /// ά���Ĺ�˾����
        /// </summary>
        [Description("������ά���Ĺ�˾����"), Category("����")]
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

        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����", "����������", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            toolBarService.AddToolButton("ɾ��", "ɾ�������̣��������Ч!", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);            
            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "����")
            {
                this.Add();
            }
            else if (e.ClickedItem.Text == "ɾ��")
            {
                this.DeleteData();
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
            if (this.fpCompany_Sheet1.Rows.Count > 0)
            {
                if (this.fpCompany.Export() == 1)
                {
                    MessageBox.Show("�����ɹ�");
                }
            }

            return 1;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.ShowData(this.type,this.kind);

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

            im = this.fpCompany.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
            //this.cmbLeach.AddItems(this.comCompany.GetList(FS.HISFC.Models.Base.EnumConstant.DOSAGEFORM));
            this.InitDataSet();

            return 1;
        }

        /// <summary>
        ///  ��ʼ��DataSet
        /// </summary>
        private void InitDataSet()
        {
            this.fpCompany_Sheet1.DataAutoSizeColumns = false;

            //��������
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtBol = System.Type.GetType("System.Boolean");

            //��myDataTable�������
            this.dt.Columns.AddRange(new DataColumn[] {	    
                                                            new DataColumn( "��˾����",     dtStr),
                                                            new DataColumn( "��˾����",     dtStr),
                                                            new DataColumn( "��˾��ַ",     dtStr),
                                                            new DataColumn( "��˾�绰",     dtStr),
                                                            new DataColumn( "GMP��Ϣ",      dtStr),
                                                            new DataColumn( "GSP��Ϣ",      dtStr),
                                                            new DataColumn( "ƴ����",       dtStr),
                                                            new DataColumn( "�����",       dtStr),
                                                            new DataColumn( "�Զ�����",     dtStr),
                                                            new DataColumn( "����",         dtStr),
                                                            new DataColumn( "��������",     dtStr),
                                                            new DataColumn( "�����ʺ�",     dtStr),
                                                            new DataColumn( "�Ӽ���",       dtStr),
                                                            new DataColumn( "��ע",         dtStr),
                                                            new DataColumn( "�Ƿ���Ч",     dtStr),
                                                            new DataColumn( "ִ����Ч��",   dtStr),
                                                            new DataColumn( "��Ӫ���֤��Ч��",dtStr),
                                                            new DataColumn( "˰��Ǽ�֤��Ч��",dtStr),
                                                            new DataColumn( "��֯��������֤��Ч��",dtStr)
											        });

            this.fpCompany_Sheet1.DataSource = this.dt.DefaultView;

            DataColumn[] keys = new DataColumn[1];
            keys[0] = dt.Columns["��˾����"];
            dt.PrimaryKey = keys;

        }

        /// <summary>
        /// �����ݱ��ڼ�������
        /// </summary>
        /// <param name="company"></param>
        private void AddDataToTable(FS.HISFC.Models.Material.MaterialCompany  company)
        {
            this.dt.Rows.Add(new object[] {           
                                              company.ID,           //��˾����
						                      company.Name ,        //��˾����
						                      company.Address ,     //��˾��ַ
						                      company.TelCode,      //��˾�绰
						                      company.GMPInfo,      //GMP��Ϣ
						                      company.GSPInfo ,     //GSP��Ϣ
						                      company.SpellCode ,   //ƴ����
						                      company.WBCode ,      //�����
						                      company.UserCode ,    //�Զ�����
						                      company.Type ,        //����
						                      company.OpenBank ,    //��������
						                      company.OpenAccounts ,//�����ʺ�
                                              company.ActualRate.ToString() ,//�Ӽ���
						                      company.Memo,         //��ע
                                              company.IsValid==true?"1":"0",//��Ч  
                                              company.BusinessDate.ToString(),
                                              company.ManageDate.ToString(),
                                              company.DutyDate.ToString(),
                                              company.OrgDate.ToString()
  
                                #region   ����
                                              //company.Oper.ID.ToString(),
                                              //company.OperTime.ToString(),
                                              //company.Extend1.ToString(),
                                              //company.Extend2.ToString(),
                                              //company.BusinessDate.ToString(),
                                              //company.ManageDate.ToString(),
                                              //company.DutyDate.ToString(),
                                              //company.OrgDate.ToString()
						                      //company.Kind ,        //��˾����
						                      //company.Coporation,  //��˾����
						                      //company.FaxCode,     //��˾����
						                      //company.NetAddress , //��˾��ַ
						                      //company.EMail ,      //��˾����
						                      //company.LinkMan,    //��ϵ��
						                      //company.LinkMail ,  //��ϵ������
						                      //company.LinkTel,    //��ϵ�˵绰
						                      //company.ISOInfo,    //ISO��Ϣ
                                #endregion             					                  
											 });

        }

        /// <summary>
        /// �����ݱ��ڻ�ȡ����
        /// </summary>
        /// <param name="row">���ȡ���ݵ����ݱ���</param>
        /// <returns></returns>
        private FS.HISFC.Models.Material.MaterialCompany GetDataFromTable(DataRow row)
        {
            FS.HISFC.Models.Material.MaterialCompany company = new FS.HISFC.Models.Material.MaterialCompany();
            company.ID = row["��˾����"].ToString();                            //��˾����
            company.Name = row["��˾����"].ToString();                          //��˾����
            company.Address = row["��˾��ַ"].ToString();                           //��˾��ַ
            company.TelCode = row["��˾�绰"].ToString();                       //��ϵ��ʽ
            company.GMPInfo = row["GMP��Ϣ"].ToString();                            //GMP��Ϣ
            company.GSPInfo = row["GSP��Ϣ"].ToString();                            //GSP��Ϣ
            company.SpellCode = row["ƴ����"].ToString();                       //ƴ����
            company.WBCode = row["�����"].ToString();                          //�����
            company.UserCode = row["�Զ�����"].ToString();                      //�Զ�����
            company.Type = ((int)this.type).ToString();                         //��˾����
            company.OpenBank = row["��������"].ToString();                      //��������
            company.OpenAccounts = row["�����ʺ�"].ToString();                  //�����ʺ�
            company.ActualRate = FS.FrameWork.Function.NConvert.ToDecimal(row["�Ӽ���"]);    //�Ӽ���
            company.Memo = row["��ע"].ToString();                              //��ע	
            company.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(row["�Ƿ���Ч"]);         //��Ч��
            company.BusinessDate = FS.FrameWork.Function.NConvert.ToDateTime(row["ִ����Ч��"]);
            company.ManageDate = FS.FrameWork.Function.NConvert.ToDateTime(row["��Ӫ���֤��Ч��"]);
            company.DutyDate = FS.FrameWork.Function.NConvert.ToDateTime(row["˰��Ǽ�֤��Ч��"]);
            company.OrgDate = FS.FrameWork.Function.NConvert.ToDateTime(row["��֯��������֤��Ч��"]);
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
                {
                    this.SetProducer();
                }
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
            this.fpCompany_Sheet1.Columns[(int)ColumnSet.ColGMP].Visible = true;

            this.fpCompany_Sheet1.Columns[(int)ColumnSet.ColGMP].Width = 120; 
        }

        /// <summary>
        /// ���ù�����˾����ʾ��ʽ
        /// </summary>
        private void SetCompany()
        {
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.fpCompany_Sheet1.Columns.Get(14).CellType = checkBoxCellType1;
            this.fpCompany_Sheet1.Columns.Get(0).Visible = false;
            this.fpCompany_Sheet1.Columns.Get(15).Visible = false;
            this.fpCompany_Sheet1.Columns.Get(16).Visible = false;
            this.fpCompany_Sheet1.Columns.Get(17).Visible = false;
            this.fpCompany_Sheet1.Columns.Get(18).Visible = false;
            this.fpCompany_Sheet1.Columns.Get(14).Visible = false;
            this.fpCompany_Sheet1.Columns.Get(9).Visible = false;

        }

        #endregion

        #region ����
        
        /// <summary>
        /// �����������е�������ʾFp��
        /// </summary>
        public void ShowData(CompanyType type,CompanyKind kind)
        {
            //�������
            this.dt.Rows.Clear();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڼ�������,���Ժ�..."));
            Application.DoEvents();

            //ȡ��˾��¼
            ArrayList alCompany = this.comCompany.QueryCompanyAppr(((int)type).ToString(), ((int)kind).ToString());
            if (alCompany == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(Language.Msg("���ع�˾���ݷ�������" + this.comCompany.Err));
                return;
            }

            FS.HISFC.Models.Material.MaterialCompany company;

            for (int i = 0; i < alCompany.Count; i++)
            {
                company = alCompany[i] as FS.HISFC.Models.Material.MaterialCompany;

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
            this.fpCompany_Sheet1.Rows.Count = 0;
        }

        /// <summary>
        /// �������
        /// </summary>
        public void Add()
        {
            MaterialCompany company = null;
            company = new MaterialCompany();
            company.ID = companyID;

            this.ShowMaintenanceForm("I", company, true);
        }

        /// <summary>
        /// ɾ��һ����˾��¼
        /// </summary>
        public void DeleteData()
        {
            this.fpCompany_Sheet1.Rows.Remove(this.fpCompany_Sheet1.ActiveRowIndex, 1);
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
                queryCode = "%" + this.txtQueryCode.Text.Trim() + "%";

                string filter = "(ƴ���� LIKE '" + queryCode + "') OR " +
                    "(����� LIKE '" + queryCode + "') OR " +
                    "(�Զ����� LIKE '" + queryCode + "') OR " +
                    "(��˾���� LIKE '" + queryCode + "') ";

                //���ù�������
                this.dt.DefaultView.RowFilter = filter;
                //this.SetCellType();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public int Save()
        {
            this.fpCompany.StopCellEditing();

            foreach (DataRow dr in this.dt.Rows)
            {
                dr.EndEdit();
            }

            //�������ݿ⴦������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.comCompany.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            bool isUpdate = false; //�ж��Ƿ���»���ɾ��������

            //ȡ�޸ĺ����ӵ�����
            DataTable dataChanges = this.dt.GetChanges(DataRowState.Deleted);
            if (dataChanges != null)
            {
                dataChanges.RejectChanges();
                foreach (DataRow row in dataChanges.Rows)
                {
                    string companyID = row["��˾����"].ToString();        //��˾����		
                    //ִ��ɾ������
                    if (this.comCompany.DeleteCompany(companyID) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("ɾ��������˾" + row["��˾����"].ToString() + "��������" + this.comCompany.Err));
                        return -1;
                    }
                }
                dataChanges.AcceptChanges();

                isUpdate = true;
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            if (isUpdate)
            {
                MessageBox.Show(Language.Msg("����ɹ���"));
            }

            //ˢ������
            this.ShowData(this.type,this.kind);

            return 1;

        }

        /// <summary>
        /// ��ȡָ���еĹ�˾����ƴ����/�������Ϣ
        /// </summary>
        /// <param name="iRow">ָ��������</param>
        /// <returns></returns>
        private int GetSpell(int iRow)
        {
            if (this.fpCompany_Sheet1.Cells[iRow, (int)ColumnSet.ColName].Text.ToString() == "")
            {
                return 1;
            }

            FS.HISFC.Models.Base.Spell spCode = new FS.HISFC.Models.Base.Spell();
            FS.HISFC.BizLogic.Manager.Spell spellManager = new FS.HISFC.BizLogic.Manager.Spell();

            spCode = (FS.HISFC.Models.Base.Spell)spellManager.Get(this.fpCompany_Sheet1.Cells[iRow, (int)ColumnSet.ColName].Text.ToString());

            if (spCode != null && spCode.SpellCode != null)
            {
                if (spCode.SpellCode.Length > 10)
                    spCode.SpellCode = spCode.SpellCode.Substring(0, 10);
                if (spCode.WBCode.Length > 10)
                    spCode.WBCode = spCode.WBCode.Substring(0, 10);

                this.fpCompany_Sheet1.Cells[iRow, (int)ColumnSet.ColSpell].Value = spCode.SpellCode;
                this.fpCompany_Sheet1.Cells[iRow, (int)ColumnSet.ColWB].Value = spCode.WBCode;
            }

            return 1;
        }

        /// <summary>
        /// �ؼ���������ʾһ������
        /// </summary>
        /// <param name="obj"></param>
        public void AddNewRow(FS.HISFC.Models.Material.MaterialCompany obj)
        {
            DataRow newRow = dt.NewRow();

            this.SetRow(newRow, obj);

            dt.Rows.Add(newRow);
        }

        /// <summary>
        /// ��DataSet�в�������
        /// </summary>
        /// <param name="row"></param>
        /// <param name="myItem"></param>
        /// <returns></returns>
        private DataRow SetRow(DataRow row, FS.HISFC.Models.Material.MaterialCompany company)
        {
            row["��˾����"] = company.ID ;                              //��˾����
            row["��˾����"] = company.Name;                             //��˾����
            row["��˾��ַ"] = company.Address;                          //��˾��ַ
            row["��˾�绰"] = company.TelCode;                          //��ϵ��ʽ
            row["GMP��Ϣ"] = company.GMPInfo;                           //GMP��Ϣ
            row["GSP��Ϣ"] = company.GSPInfo;                           //GSP��Ϣ
            row["ƴ����"] = company.SpellCode;                          //ƴ����
            row["�����"] = company.WBCode;                             //�����
            row["�Զ�����"] = company.UserCode;                         //�Զ�����
            row["��������"] = company.OpenBank;                         //��������
            row["�����ʺ�"] = company.OpenAccounts;                     //�����ʺ�
            row["�Ӽ���"] = company.ActualRate;                         //�Ӽ���
            row["��ע"] = company.Memo;                                 //��ע	
            row["�Ƿ���Ч"] = company.IsValid;                          //��Ч��
            row["ִ����Ч��"]= company.BusinessDate;
            row["��Ӫ���֤��Ч��"] = company.ManageDate;
            row["˰��Ǽ�֤��Ч��"] = company.DutyDate;
            row["��֯��������֤��Ч��"] = company.OrgDate ;
            return row;

        }

        /// <summary>
        /// �޸�����
        /// </summary>
        public void Modify()
        {
            if (this.fpCompany_Sheet1.Rows.Count == 0)
                return;

            DataRow findRow;

            MaterialCompany myCompany = null;
            myCompany = this.comCompany.QueryCompanyByCompanyID(this.fpCompany_Sheet1.Cells[this.fpCompany_Sheet1.ActiveRowIndex, this.dt.Columns.IndexOf("��˾����")].Value.ToString(),"A","A");
  
            this.ShowMaintenanceForm("U", myCompany, true);
            findRow = dt.Rows.Find(myCompany.ID.ToString());
            if (myCompany.ID.ToString() != null)
            {
                //���ݱ���ȡȫ����Ϣ����ʾ���б���
                myCompany = comCompany.QueryCompanyByCompanyID(myCompany.ID.ToString(),"A","A");
                this.SetRow(findRow, myCompany);
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public void Copy()
        {
            if (this.fpCompany_Sheet1.Rows.Count == 0)
                return;

            MaterialCompany company = null;
            company = this.comCompany.QueryCompanyByCompanyID(this.fpCompany_Sheet1.Cells[this.fpCompany_Sheet1.ActiveRowIndex, this.dt.Columns.IndexOf("��˾����")].Value.ToString(),"A","A");

            company.ID = "";

            this.ShowMaintenanceForm("I", company, true);
        }


        #endregion

        #region �¼�
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                this.Init();

                this.ShowData(this.type,this.kind);
            }
            catch { }

            base.OnLoad(e);
        }

        private void txtQueryCode_TextChanged(object sender, EventArgs e)
        {
            this.ChangeItem();
            this.SetCellType();

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
            if (this.fpCompany.ContainsFocus)
            {
                if (keyData == Keys.Enter)
                {
                    if (this.fpCompany_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColName)
                    {
                        this.GetSpell(this.fpCompany_Sheet1.ActiveRowIndex);
                    }

                    this.fpCompany_Sheet1.ActiveColumnIndex++;
                }

            }
            return base.ProcessDialogKey(keyData);
        }

        private void cmbLeach_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //DateTime dtSys = this.comCompany.GetDateTimeFromSysDateTime();

            DateTime dtSys = DateTime.Now.Date; //��ʱ����
            if (this.cmbLeach.SelectedIndex == 0)
            {
                if (this.dt.Rows.Count == 0) return;

                try
                {
                    string queryCode = "";
                    queryCode = "ִ����Ч�� <" + "#" + dtSys + "#";

                    //���ù�������
                    this.dt.DefaultView.RowFilter = queryCode;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (this.cmbLeach.SelectedIndex == 1)
            {
                if (this.dt.Rows.Count == 0) return;

                try
                {
                    string queryCode = "";
                    queryCode = "��Ӫ���֤��Ч�� <" + "#" + dtSys + "#";

                    //���ù�������
                    this.dt.DefaultView.RowFilter = queryCode;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (this.cmbLeach.SelectedIndex == 2)
            {
                if (this.dt.Rows.Count == 0) return;

                try
                {
                    string queryCode = "";
                    queryCode = "˰��Ǽ�֤��Ч�� <" + "#" + dtSys + "#";

                    //���ù�������
                    this.dt.DefaultView.RowFilter = queryCode;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (this.cmbLeach.SelectedIndex == 3)
            {
                if (this.dt.Rows.Count == 0) return;

                try
                {
                    string queryCode = "";
                    queryCode = "��֯��������֤��Ч�� <" + "#" + dtSys + "#";

                    //���ù�������
                    this.dt.DefaultView.RowFilter = queryCode;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            this.SetCellType();

        }

        private void ucComCompanyEdit_MyInput(FS.HISFC.Models.Material.MaterialCompany company)
        {
            this.AddNewRow(company);

        }

        private void fpMaterialQuery_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.isEditExpediency)	//ӵ���޸�Ȩ��
            {
                this.Modify();
            }
        }

        //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
        private void cmbLeach_TextChanged(object sender, EventArgs e)
        {
            if (this.cmbLeach.Text == "")
            {
                this.dt.DefaultView.RowFilter = "1=1";
            }
        }

        #endregion

        #region ö����
        /// <summary>
        /// ά����˾���
        /// </summary>
        public enum CompanyKind
        {
            ҩ��ʹ��,
            ���ʳ���ʹ��
        }

        /// <summary>
        /// ά����˾����
        /// </summary>
        public enum CompanyType
        {
            ��������,
            ������˾
        }

        /// <summary>
        /// ������
        /// </summary>
        private enum ColumnSet
        {
            /// <summary>
            /// ��λ����
            /// </summary>
            ColID, 
            /// <summary>
            /// ��λ����
            /// </summary>
            ColName,
            /// <summary>
            /// ����
            /// </summary>
            ColCoporation,
            /// <summary>
            /// ��ַ
            /// </summary>
            ColAddress,
            /// <summary>
            /// ��ϵ��ʽ
            /// </summary>
            ColPhone,
            /// <summary>
            /// ����
            /// </summary>
            ColFaxCode,
            /// <summary>
            /// ��ַ
            /// </summary>
            ColNetAddress,
            /// <summary>
            /// ��˾����
            /// </summary>
            ColEMail,
            /// <summary>
            /// ��ϵ��
            /// </summary>
            ColLinkMan,
            /// <summary>
            /// ��ϵ�˵绰
            /// </summary>
            ColLinkTel,
            /// <summary>
            /// ��ϵ������
            /// </summary>
            CollinkMail,
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
            /// ��ע
            /// </summary>
            ColMemo,
            /// <summary>
            /// ����
            /// </summary>
            ColType

        }

        #endregion

        #region ά����������

        /// <summary>
        /// ά���������� ��̳���Material.Base.ucComCompanyEdit
        /// </summary>
        public Material.Base.ucComCompanyEdit ComCompanyEditPop
        {
            set
            {
                if (value != null && value as Material.Base.ucComCompanyEdit == null)
                {
                    System.Windows.Forms.MessageBox.Show("��ά���ؼ���̳���Material.Base.ucComCompanyEdit");
                }
                else
                {
                    this.ComEdit = value as Material.Base.ucComCompanyEdit;

                    this.ComEdit.MyInput -= new ucComCompanyEdit.SaveInput(ucComCompanyEdit_MyInput);
                    this.ComEdit.MyInput += new ucComCompanyEdit.SaveInput(ucComCompanyEdit_MyInput);
                }
            }
        }
        /// <summary>
        /// ����ά������
        /// </summary>
        private void InitMaintenanceForm()
        {
            if (this.ComEdit == null)
            {
                this.ComEdit = new Material.Base.ucComCompanyEdit();
                this.ComEdit.MyInput -= new Material.Base.ucComCompanyEdit.SaveInput(ucComCompanyEdit_MyInput);
                this.ComEdit.MyInput += new Material.Base.ucComCompanyEdit.SaveInput(ucComCompanyEdit_MyInput);
            }
            if (this.EditForm == null)
            {
                this.EditForm = new Form();
                this.EditForm.Width = this.ComEdit.Width + 10;
                this.EditForm.Height = this.ComEdit.Height + 25;
                this.EditForm.Text = "��Ʒ��ϸ��Ϣά��";
                this.EditForm.StartPosition = FormStartPosition.CenterScreen;
                this.EditForm.ShowInTaskbar = false;
                this.EditForm.HelpButton = false;
                this.EditForm.MaximizeBox = false;
                this.EditForm.MinimizeBox = false;
                this.EditForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            }


            this.ComEdit.Dock = DockStyle.Fill;
            this.EditForm.Controls.Add(this.ComEdit);
        }
        /// <summary>
        /// ά��������ʾ
        /// </summary>
        private void ShowMaintenanceForm(string inputType, FS.HISFC.Models.Material.MaterialCompany company, bool isShow)
        {
            if (this.EditForm == null || this.ComEdit == null)
                this.InitMaintenanceForm();

            this.ComEdit.InputType = inputType;
            this.ComEdit.Company = company;
            this.ComEdit.ReadOnly = !this.isEditExpediency;
            this.ComEdit.Type = this.type;

            if (isShow)
            {
                this.EditForm.ShowDialog();
            }
        }

        #endregion

        

    }
}



