using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FarPoint.Win.Spread;

namespace Neusoft.UFC.Material.Base
{
    /// <summary>		
    /// ucMatKind��ժҪ˵����
    /// [��������: ���ʿ�Ŀά��]
    /// [�� �� ��: �]
    /// [����ʱ��: 2007-03-28]
    /// </summary>
    public partial class ucMatKind : UserControl
    {
        public ucMatKind()
        {
            InitializeComponent();
        }

        #region ����

        private DataView myDataView;

        private DataSet myDataSet = new DataSet();

        private string filePath = "\\MatKind.xml";

        private string myType = "";

        /// <summary>
        /// ���ʿ�Ŀ��
        /// </summary>
        private Neusoft.HISFC.Management.Material.Baseset baseset = new Neusoft.HISFC.Management.Material.Baseset();

        private string kindLevel;

        /// <summary>
        /// �ϼ���Ŀ����
        /// </summary>
        private string kindPreID;

        /// <summary>
        /// �༭״̬
        /// </summary>
        private string state;

        #endregion

        #region ����

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string KindLevel
        {
            get
            {
                return this.kindLevel;
            }
            set
            {
                this.kindLevel = value;
            }
        }

        /// <summary>
        /// �ϼ���Ŀ����
        /// </summary>
        public string KindPreID
        {
            get
            {
                return this.kindPreID;
            }
            set
            {
                this.kindPreID = value;
            }
        }

        /// <summary>
        /// �༭״̬
        /// </summary>
        public string State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }


        #endregion

        #region ����

        /// <summary>
        /// �����������е�������ʾ��fpKind_Sheet1��
        /// </summary>
        public void ShowData(string type)
        {
            //��ʼ��DataSet
            this.InitDataSet();
            //�������
            //			this.myDataSet.Tables[0].Rows.Clear();

            //ȡ��Ŀ��Ϣ
            ArrayList alObject = this.baseset.QueryKindAll();

            if (alObject == null)
            {
                MessageBox.Show(this.baseset.Err);
                return;
            }

            Neusoft.HISFC.Object.Material.MaterialKind metKind;

            for (int i = 0; i < alObject.Count; i++)
            {
                metKind = alObject[i] as Neusoft.HISFC.Object.Material.MaterialKind;
                this.myDataSet.Tables[0].Rows.Add(new Object[] {																	
																		//��Ŀ����
																		metKind.Kgrade,
				
																		//��Ŀ����
																		metKind.ID,

																		//�ϼ�����
																		metKind.SuperKind,

																		//��Ŀ����
																		metKind.Name,

																		//ƴ����
																		metKind.SpellCode.ToString(),

																		//�����
																		metKind.WBCode,

																		//��ĩ����ʶ
																		metKind.EndGrade,

																		//��Ҫ��Ƭ
																		metKind.IsCardNeed,//.ToString(),

																		//���ι���
																		metKind.IsBatch,//.ToString(),

																		//��Ч�ڹ���
																		metKind.IsValidcon,//.ToString(),

																		//�����Ŀ����
																		metKind.AccountCode.ToString(),

																		//�����Ŀ����
																		metKind.AccountName.ToString(),

																		//����Ա
																		//metKind.Oper.ID,

																		//��������
																		//metKind.OperateDate.ToString(),

																		//Ԥ�Ʋ�ֵ��
																		metKind.LeftRate.ToString(),

																		//�Ƿ�̶��ʲ�
																		metKind.IsFixedAssets,//.ToString(),

																		//�������
																		metKind.OrderNo.ToString(),																		

																		//��Ӧ�ɱ�������Ŀ���
																		metKind.StatCode,

																		//�Ƿ�Ӽ�����
																		metKind.IsAddFlag//.ToString()
																	});
            }

            //�ύDataSet�еı仯��
            this.myDataSet.Tables[0].AcceptChanges();
            this.fpKind_Sheet1.Columns[0].Visible = false;
            this.fpKind_Sheet1.Columns[2].Visible = false;

        }


        /// <summary>
        ///  ��ʼ��DataSet,����fpKind_Sheet1��
        /// </summary>
        private void InitDataSet()
        {
            this.myDataSet.Tables.Clear();
            this.myDataSet.Tables.Add();
            this.myDataView = new DataView(this.myDataSet.Tables[0]);
            this.myDataView.AllowEdit = true;
            this.myDataView.AllowNew = true;
            this.fpKind.DataSource = this.myDataView;


            //��������
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtBool = System.Type.GetType("System.Boolean");

            //��myDataTable�������
            this.myDataSet.Tables[0].Columns.AddRange(new DataColumn[] {
																			new DataColumn("��Ŀ����",   dtStr),
																			new DataColumn("��Ŀ����",   dtStr),
																			new DataColumn("�ϼ�����",   dtStr),
																			new DataColumn("��Ŀ����",   dtStr),
																			new DataColumn("ƴ����",   dtStr),
																			new DataColumn("�����",     dtStr),
																			new DataColumn("ĩ����ʶ",   dtBool),
																			new DataColumn("��Ҫ��Ƭ",   dtBool),
																			new DataColumn("���ι���",   dtBool),
																			new DataColumn("Ч�ڹ���",   dtBool),
																			new DataColumn("�����Ŀ����",   dtStr),
																			new DataColumn("�����Ŀ����",   dtStr),																			
																			new DataColumn("Ԥ�Ʋ�ֵ��",    dtStr),
																			new DataColumn("�̶��ʲ�",    dtBool),
																			new DataColumn("�������",    dtStr),																			
																			new DataColumn("�������",     dtStr),
																			new DataColumn("�Ӽ�����",   dtBool)																			
																		});


        }


        /// <summary>
        /// �������
        /// </summary>
        public void ClearData()
        {
            this.fpKind_Sheet1.Rows.Count = 0;
        }


        /// <summary>
        /// ɾ����¼
        /// </summary>
        public void DeleteData()
        {
            string kindID = "";

            kindID = this.fpKind_Sheet1.Cells[this.fpKind_Sheet1.ActiveRowIndex, 1].Value.ToString();

            int kindRowCount = this.baseset.GetKindRowCount(kindID);

            if (kindRowCount > 0)
            {
                MessageBox.Show("�˿�Ŀ�´�����Ʒ�ֵ���Ϣ������ɾ���ֵ���Ϣ��ִ�д˲���!", "ɾ����ʾ");
                return;
            }

            if(kindRowCount < 0)
            {
                MessageBox.Show("��ȡ�ÿ�Ŀ����Ŀ�ֵ�����������");
                return;
            }

            System.Windows.Forms.DialogResult dr;
            dr = MessageBox.Show("ȷ��Ҫɾ���˿�Ŀ��?", "��ʾ!", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No)
            {
                return;
            }

            Neusoft.NFC.Management.PublicTrans.BeginTransaction();

            //Neusoft.NFC.Management.Transaction t = new Neusoft.NFC.Management.Transaction(Neusoft.NFC.Management.Connection.Instance);
            //t.BeginTransaction();

            baseset.SetTrans(Neusoft.NFC.Management.PublicTrans.Trans);

            if (this.baseset.DeleteMetKind(kindID) == -1)
            {
                Neusoft.NFC.Management.PublicTrans.RollBack();
                MessageBox.Show(this.baseset.Err);
                return;
            }

            Neusoft.NFC.Management.PublicTrans.Commit();

            this.fpKind_Sheet1.Rows.Remove(this.fpKind_Sheet1.ActiveRowIndex, 1);

            MessageBox.Show("ɾ���ɹ���");

        }


        /// <summary>
        /// ͨ������Ĳ�ѯ�룬���������б�
        /// </summary>
        public void ChangeItem(string treeFilter)
        {
            if (this.myDataSet.Tables[0].Rows.Count == 0) return;

            try
            {
                string queryCode = "";

                queryCode = "%" + this.txtQueryCode.Text.Trim() + "%";

                string filter = "(ƴ���� LIKE '" + queryCode + "') OR " +
                    "(����� LIKE '" + queryCode + "') OR " +
                    "(��Ŀ���� LIKE '" + queryCode + "') ";

                //���ù�������
                if (treeFilter == "0")
                {
                    this.myDataView.RowFilter = filter;
                }
                else
                {
                    this.myDataView.RowFilter = "((�ϼ����� = '" + treeFilter + "')or" + "(��Ŀ����='" + treeFilter + "'))and (" + filter + ")";
                }
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
            this.fpKind.StopCellEditing();
            //��Ч���ж�
            if (valid())
            {
                return -1;
            };

            //�������ݿ⴦������
            Neusoft.NFC.Management.PublicTrans.BeginTransaction();

            //Neusoft.NFC.Management.Transaction t = new Neusoft.NFC.Management.Transaction(Neusoft.NFC.Management.Connection.Instance);
            //t.BeginTransaction();

            baseset.SetTrans(Neusoft.NFC.Management.PublicTrans.Trans);
            bool isUpdate = false; //�ж��Ƿ���»���ɾ��������

            //ȡ�޸ĺ����ӵ�����
            DataSet dataChanges = this.myDataSet.GetChanges(DataRowState.Modified | DataRowState.Added);
            if (dataChanges != null)
            {
                foreach (DataRow row in dataChanges.Tables[0].Rows)
                {
                    Neusoft.HISFC.Object.Material.MaterialKind metKind = new Neusoft.HISFC.Object.Material.MaterialKind();
                    //��Ŀ����
                    metKind.Kgrade = row["��Ŀ����"].ToString();

                    //��Ŀ����
                    metKind.ID = row["��Ŀ����"].ToString();

                    //�ϼ�����
                    metKind.SuperKind = row["�ϼ�����"].ToString();

                    //��Ŀ����
                    metKind.Name = row["��Ŀ����"].ToString();

                    //ƴ����
                    metKind.SpellCode = row["ƴ����"].ToString();

                    //�����
                    metKind.WBCode = row["�����"].ToString();

                    //��ĩ����ʶ
                    metKind.EndGrade = Neusoft.NFC.Function.NConvert.ToBoolean(row["ĩ����ʶ"].ToString());

                    //��Ҫ��Ƭ
                    metKind.IsCardNeed = Neusoft.NFC.Function.NConvert.ToBoolean(row["��Ҫ��Ƭ"].ToString());

                    //���ι���
                    metKind.IsBatch = Neusoft.NFC.Function.NConvert.ToBoolean(row["���ι���"].ToString());

                    //��Ч�ڹ���
                    metKind.IsValidcon = Neusoft.NFC.Function.NConvert.ToBoolean(row["Ч�ڹ���"].ToString());

                    //�����Ŀ����
                    metKind.AccountCode.ID = row["�����Ŀ����"].ToString();

                    //�����Ŀ����
                    metKind.AccountName.Name = row["�����Ŀ����"].ToString();

                    //					//����Ա
                    //					metKind.Oper.ID = row[10].ToString();
                    //
                    //					//��������
                    //					metKind.OperateDate = Convert.ToDateTime(row[11].ToString());

                    //Ԥ�Ʋ�ֵ��
                    metKind.LeftRate = Neusoft.NFC.Function.NConvert.ToDecimal(row["Ԥ�Ʋ�ֵ��"].ToString());

                    //�Ƿ�̶��ʲ�
                    metKind.IsFixedAssets = Neusoft.NFC.Function.NConvert.ToBoolean(row["�̶��ʲ�"].ToString());

                    //�������
                    metKind.OrderNo = Neusoft.NFC.Function.NConvert.ToInt32(row["�������"].ToString());

                    //��Ӧ�ɱ�������Ŀ���
                    metKind.StatCode = row["�������"].ToString();

                    //�Ƿ�Ӽ�����
                    metKind.IsAddFlag = Neusoft.NFC.Function.NConvert.ToBoolean(row["�Ӽ�����"].ToString());

                    //ִ�и��²������ȸ��£����û�гɹ������������
                    if (this.baseset.SetKind(metKind) == -1)
                    {
                        Neusoft.NFC.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.baseset.Err);

                        return 0;
                    }
                }
                dataChanges.AcceptChanges();
                isUpdate = true;
            }

            //ȡɾ��������
            //			dataChanges = this.myDataSet.GetChanges(DataRowState.Deleted);
            //			if(dataChanges != null)	 
            //			{
            //				dataChanges.RejectChanges();
            //				foreach(DataRow row in dataChanges.Tables[0].Rows) 
            //				{ 
            //					string metKindID   = row[0].ToString();        		
            //					//ִ��ɾ������
            //					if (this.baseset.DeleteMetKind(metKindID)==-1) 
            //					{
            //						Neusoft.NFC.Management.PublicTrans.RollBack();
            //						MessageBox.Show(this.baseset.Err );
            //						return 0;
            //					}
            //				}
            //				dataChanges.AcceptChanges();
            //				isUpdate = true;
            //			}
            Neusoft.NFC.Management.PublicTrans.Commit();

            //ˢ������
            this.ShowData(this.myType);

            if (isUpdate) MessageBox.Show("����ɹ���");
            return 1;
        }

        public void New()
        {
            try
            {
                string kindID = this.baseset.GetMaxKindID(this.KindPreID);

                ArrayList al = new ArrayList();

                if (this.KindPreID == "0")
                {
                    this.myDataSet.Tables[0].Rows.Add(new Object[] { "1", kindID.ToString(), "0", "", "", "", 1, 1, 1, 1, "", "", "", 1, "", "", 1 });
                }
                else
                {
                    al = this.baseset.QueryKindAllByID(this.KindPreID);
                    Neusoft.HISFC.Object.Material.MaterialKind metKind;
                    metKind = al[0] as Neusoft.HISFC.Object.Material.MaterialKind;
                    this.myDataSet.Tables[0].Rows.Add(new Object[] { (Convert.ToInt32(metKind.Kgrade) + 1).ToString(), kindID.ToString(), metKind.ID.ToString(), "", "", "", 1, 1, 1, 1, "", "", "", 1, "", "", 1 });
                }

                this.fpKind_Sheet1.ActiveRowIndex = this.fpKind_Sheet1.RowCount - 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }


        /// <summary>
        ///  ��Ч���ж� 
        /// </summary>	
        private bool valid()
        {
            for (int i = 0; i < this.fpKind_Sheet1.RowCount; i++)
            {

                if (this.fpKind_Sheet1.Cells[i, 1].Text == "" || this.fpKind_Sheet1.Cells[i, 1] == null)
                {
                    MessageBox.Show("��" + i.ToString() + "�п�Ŀ���Ʋ���Ϊ��");
                    return true;
                }

            }
            return false;
        }


        #endregion

        #region �¼�

        private void ucMatKind_Load(object sender, System.EventArgs e)
        {
            this.txtQueryCode.TextChanged += new EventHandler(txtQueryCode_TextChanged);
            this.txtQueryCode.KeyUp += new KeyEventHandler(txtQueryCode_KeyUp);
            //			this.InitDataSet();
            this.ShowData(this.myType);

            InputMap im;

            im = this.fpKind.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
        }


        private void txtQueryCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
                Neusoft.NFC.Interface.Classes.CustomerFp.SaveColumnProperty(this.fpKind_Sheet1, this.filePath);

        }


        private void txtQueryCode_TextChanged(object sender, EventArgs e)
        {
            this.ChangeItem("0");
        }


        private void fpKind_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            if (e.Column == 3)
            {
                if (fpKind_Sheet1.Cells[e.Row, 3].Text.ToString() == "")
                    return;
                Neusoft.HISFC.Object.Base.Spell spCode = new Neusoft.HISFC.Object.Base.Spell();
                Neusoft.HISFC.Management.Manager.Spell mySpell = new Neusoft.HISFC.Management.Manager.Spell();

                spCode = (Neusoft.HISFC.Object.Base.Spell)mySpell.Get(fpKind_Sheet1.Cells[e.Row, 3].Text.ToString());

                if (spCode.SpellCode.Length > 10)
                    spCode.SpellCode = spCode.SpellCode.Substring(0, 10);
                if (spCode.WBCode.Length > 10)
                    spCode.WBCode = spCode.WBCode.Substring(0, 10);

                this.fpKind_Sheet1.Cells[e.Row, 4].Value = spCode.SpellCode;
                this.fpKind_Sheet1.Cells[e.Row, 5].Value = spCode.WBCode;
            }
        }


        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.fpKind.ContainsFocus)
            {
                if (keyData == Keys.Enter)
                {
                    if (this.fpKind_Sheet1.ActiveColumnIndex == 3)
                    {
                        Neusoft.HISFC.Object.Base.Spell spCode = new Neusoft.HISFC.Object.Base.Spell();
                        Neusoft.HISFC.Management.Manager.Spell mySpell = new Neusoft.HISFC.Management.Manager.Spell();

                        spCode = (Neusoft.HISFC.Object.Base.Spell)mySpell.Get(fpKind_Sheet1.Cells[this.fpKind_Sheet1.ActiveRowIndex, 3].Text.ToString());

                        if (spCode.SpellCode.Length > 10)
                            spCode.SpellCode = spCode.SpellCode.Substring(0, 10);
                        if (spCode.WBCode.Length > 10)
                            spCode.WBCode = spCode.WBCode.Substring(0, 10);

                        this.fpKind_Sheet1.Cells[this.fpKind_Sheet1.ActiveRowIndex, 4].Value = spCode.SpellCode;
                        this.fpKind_Sheet1.Cells[this.fpKind_Sheet1.ActiveRowIndex, 5].Value = spCode.WBCode;
                    }

                    this.fpKind_Sheet1.ActiveColumnIndex++;
                }

            }
            return base.ProcessDialogKey(keyData);
        }


        #endregion	
        
        #region IConstManager ��Ա
        /*
        public int Add()
        {
            // TODO:  ��� ucCompanyManager.Add ʵ��
            this.myDataView.AddNew();
            this.fpKind_Sheet1.ActiveRowIndex = this.fpKind_Sheet1.RowCount - 1;
            return 0;
        }

        public int Del()
        {
            // TODO:  ��� ucCompanyManager.Del ʵ��
            this.DeleteData();
            return 0;
        }

        public int Retrieve()
        {
            // TODO:  ��� ucCompanyManager.Retrieve ʵ��
            return 0;
        }

        public int Retrieve(string typeCode)
        {
            // TODO:  ��� ucCompanyManager.Pharmacy.IConstManager.Retrieve ʵ��
            if (typeCode == null) return 0;
            this.myType = typeCode;

            this.ShowData(typeCode);
            return 0;
        }

        public int Pre()
        {
            // TODO:  ��� ucCompanyManager.Pre ʵ��
            return 0;
        }

        public int Next()
        {
            // TODO:  ��� ucCompanyManager.Next ʵ��
            return 0;
        }

        public int Search()
        {
            // TODO:  ��� ucCompanyManager.Search ʵ��
            return 0;
        }

        public int Print()
        {
            // TODO:  ��� ucCompanyManager.Print ʵ��
            return 0;
        }

        public int Help()
        {
            // TODO:  ��� ucCompanyManager.Help ʵ��
            return 0;
        }

        public int Exit()
        {
            // TODO:  ��� ucCompanyManager.Exit ʵ��
            return 0;
        }

        public ToolBarButton AddButton
        {
            get
            {
                // TODO:  ��� ucCompanyManager.AddButton getter ʵ��
                return null;
            }
        }

        public ToolBarButton DelButton
        {
            get
            {
                // TODO:  ��� ucCompanyManager.DelButton getter ʵ��
                return null;
            }
        }

        public ToolBarButton SaveButton
        {
            get
            {
                // TODO:  ��� ucCompanyManager.SaveButton getter ʵ��
                return null;
            }
        }

        public ToolBarButton RetrieveButton
        {
            get
            {
                // TODO:  ��� ucCompanyManager.RetrieveButton getter ʵ��
                return null;
            }
        }

        public ToolBarButton SearchButton
        {
            get
            {
                // TODO:  ��� ucCompanyManager.SearchButton getter ʵ��
                return null;
            }
        }

        public ToolBarButton AuditingButton
        {
            get
            {
                // TODO:  ��� ucCompanyManager.AuditingButton getter ʵ��
                return null;
            }
        }

        public ToolBarButton PreButton
        {
            get
            {
                // TODO:  ��� ucCompanyManager.PreButton getter ʵ��
                return null;
            }
        }

        public ToolBarButton NextButton
        {
            get
            {
                // TODO:  ��� ucCompanyManager.NextButton getter ʵ��
                return null;
            }
        }

        public ToolBarButton PrintButton
        {
            get
            {
                // TODO:  ��� ucCompanyManager.PrintButton getter ʵ��
                return null;
            }
        }
        */
        #endregion
    }
}
