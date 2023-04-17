using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FarPoint.Win.Spread;
using FS.FrameWork.Function;
using FS.FrameWork.Management;
using FS.FrameWork.WinForms.Forms;

namespace FS.HISFC.Components.Material.Base
{
    /// <summary>		
    /// ucMatStorage��ժҪ˵��
    /// [��������: ���ʲֿ�ά��]
    /// [�� �� ��: �]
    /// [����ʱ��: 2007-03-28]
    /// 
    /// [�� �� ��:��ά]
    ///	 [�޸�ʱ��:2007-11-27]
    ///	 [�޸�Ŀ��:�����µ�ҵ��]
    ///	 [�޸�����:ʵ�����ֶα��]
    /// </summary>
    public partial class ucMatStorage : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucMatStorage()
        {
            InitializeComponent();
        }

        #region ������

        /// <summary>
        /// �����б���
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();

        /// <summary>
        /// ���ʿ�Ŀ��
        /// </summary>
        private FS.HISFC.BizLogic.Material.Baseset basesetManager = new FS.HISFC.BizLogic.Material.Baseset();

        /// <summary>
        /// �滻��������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        #endregion

        #region ����

        private DataView dv;

        private DataTable dt;

        private ArrayList alDept;

        /// <summary>
        /// ��ά���Ŀ��ҿ���б�
        /// </summary>
        private System.Collections.Hashtable hsphaDept = new Hashtable();
        #endregion

        #region ����

        #region ���ݳ�ʼ��

        /// <summary>
        /// ������ʾ��ʽ
        /// </summary>
        private void SetCellType()
        {
            this.fpStorage_Sheet1.Columns[0].Locked = true;

            this.fpStorage_Sheet1.Columns[14].Visible = false;
            this.fpStorage_Sheet1.Columns[15].Visible = false;

            this.fpStorage_Sheet1.Columns[0].Width = 80F;
            this.fpStorage_Sheet1.Columns[1].Width = 80F;
            this.fpStorage_Sheet1.Columns[2].Width = 80F;
            this.fpStorage_Sheet1.Columns[3].Width = 80F;
            this.fpStorage_Sheet1.Columns[4].Width = 80F;
            this.fpStorage_Sheet1.Columns[5].Width = 80F;
            this.fpStorage_Sheet1.Columns[6].Width = 80F;
            this.fpStorage_Sheet1.Columns[7].Width = 80F;
            this.fpStorage_Sheet1.Columns[8].Width = 80F;
            this.fpStorage_Sheet1.Columns[9].Width = 80F;
            this.fpStorage_Sheet1.Columns[10].Width = 80F;
            this.fpStorage_Sheet1.Columns[11].Width = 80F;
            this.fpStorage_Sheet1.Columns[12].Width = 80F;
            this.fpStorage_Sheet1.Columns[13].Width = 80F;
            this.fpStorage_Sheet1.Columns[14].Width = 80F;
            this.fpStorage_Sheet1.Columns[15].Width = 80F;
        }

        protected void AddDataToTable(ArrayList alData, bool isAddKey)
        {
            //���ʲֿ�ʵ��
            FS.HISFC.Models.Material.MaterialStorage storage;

            for (int i = 0; i < alData.Count; i++)
            {
                storage = alData[i] as FS.HISFC.Models.Material.MaterialStorage;

                if (storage.Name == null)
                {
                    storage.Name = deptHelper.GetName(storage.ID);
                }
                if (isAddKey)
                {
                    this.hsphaDept.Add(storage.ID, null);
                }

                this.dt.Rows.Add(new Object[] {		
					                                                storage.ID,	                                  //0�ֿ����				
																	storage.Name,                             //1�ֿ����� 
																	storage.SpellCode,                       //2ƴ����
																	storage.WBCode,                         //3�����																	 
																	storage.OutStartNO,                     //4���ⵥ��ʼ��
																	storage.InStartNO,                       //5��ⵥ��ʼ��																	
																	storage.PlanStartNO,                    //6���뵥��ʼ��
																	storage.IsWithFix,                        //7���޹̶��ʲ�
																	storage.IsStorage,                       //8�Ƿ��ǲֿ�
																	storage.IsStoreManage,               //9�Ƿ������
																	storage.IsBatchManage,               //10�Ƿ��������
																	storage.MaxDays,
                                                                    storage.MinDays,
                                                                    storage.ReferenceDays,
                                                                    storage.Oper.ID,                          //14����Ա
                                                                    storage.Oper.OperTime                //15��������
                                                              });
            }
        }

        /// <summary>
        /// �����������е�������ʾ��fpStorage_Sheet1��
        /// </summary>
        public void ShowData()
        {
            //���farpoint�е�����
            this.ClearData();

            //ȡ��ֿ���Ϣ
            ArrayList alObject = this.basesetManager.GetStorageInfo();
            if (alObject == null)
            {
                MessageBox.Show(this.basesetManager.Err);
                return;
            }

            this.hsphaDept.Clear();

            this.AddDataToTable(alObject, true);

            this.dt.AcceptChanges();
        }

        /// <summary>
        /// ���ҽṹ��Ϣ��ʾ
        /// </summary>
        public void ShowDeptStruct()
        {
            FS.HISFC.BizLogic.Manager.DepartmentStatManager deptStatManager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            ArrayList alDeptStat = deptStatManager.LoadDepartmentStat("05");
            if (alDeptStat == null)
            {
                MessageBox.Show(Language.Msg("��ȡ���ҽڵ���Ϣʧ��"));
                return;
            }

            ArrayList al = new ArrayList();

            foreach (FS.HISFC.Models.Base.DepartmentStat deptStat in alDeptStat)
            {
                if (this.hsphaDept.ContainsKey(deptStat.DeptCode))
                {
                    continue;
                }

                if (deptStat.DeptCode.Substring(0, 1) == "S")
                {
                    continue;
                }

                FS.HISFC.Models.Material.MaterialStorage storage = new FS.HISFC.Models.Material.MaterialStorage();

                storage.ID = deptStat.DeptCode;
                storage.Name = this.deptHelper.GetName(deptStat.DeptCode);
                storage.Name = storage.Name;
                storage.ID = deptStat.DeptCode;
                storage.SpellCode = deptStat.SpellCode;
                storage.WBCode = deptStat.WBCode;

                al.Add(storage);
            }

            this.AddDataToTable(al, true);
        }

        /// <summary>
        ///  ��ʼ��DataSet,����fpStorage_Sheet1��
        /// </summary>
        private void InitDataSet()
        {
            //��������
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtInt = System.Type.GetType("System.Int32");
            System.Type dtBool = System.Type.GetType("System.Boolean");
            System.Type dtDate = System.Type.GetType("System.DateTime");

            //����CellType
            this.dt = new DataTable();

            //��myDataTable�������
            this.dt.Columns.AddRange(new DataColumn[] {
                                                                            new DataColumn("�ֿ����",   dtStr),
																			new DataColumn("�ֿ�����",   dtStr),
																			new DataColumn("ƴ����",   dtStr),
																			new DataColumn("�����",    dtStr),																			
																			new DataColumn("���ⵥ��ʼ��",   dtInt),
																			new DataColumn("��ⵥ��ʼ��",   dtInt),
																			new DataColumn("���뵥��ʼ��",    dtInt),
																			new DataColumn("���޹̶��ʲ�",   dtBool),																			
																			new DataColumn("�Ƿ��ǲֿ�",   dtBool),
																			new DataColumn("�Ƿ������",   dtBool),
																			new DataColumn("�Ƿ��������",   dtBool),
                                                                            new DataColumn("�����������",  dtInt),
                                                                            new DataColumn("�����������",  dtInt),
                                                                            new DataColumn("���ο�����",  dtInt),
                                                                            new DataColumn("����Ա",   dtStr),
                                                                            new DataColumn("��������",dtDate)						
																		});

            alDept = deptManager.GetDeptmentAllOrderByDeptType();
            this.deptHelper = new FS.FrameWork.Public.ObjectHelper(alDept);

            this.dv = new DataView(this.dt);
            this.fpStorage.DataSource = this.dv;

            this.SetCellType();
        }

        #endregion

        /// <summary>
        /// �������
        /// </summary>
        public void ClearData()
        {
            this.dt.Rows.Clear();

            this.fpStorage_Sheet1.Rows.Count = 0;
        }

        /// <summary>
        /// ͨ������Ĳ�ѯ�룬���������б�
        /// </summary>
        private void ChangeItem()
        {
            if (this.dt.Rows.Count == 0)
            {
                return;
            }

            try
            {
                string queryCode = "";
                queryCode = "%" + this.txtQueryCode.Text.Trim() + "%";

                string filter = "(ƴ���� LIKE '" + queryCode + "') OR " +
                    "(����� LIKE '" + queryCode + "') OR " +
                    "(�ֿ����� LIKE '" + queryCode + "') ";

                //���ù�������
                this.dv.RowFilter = filter;
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
            this.fpStorage.StopCellEditing();
            //��Ч���ж�
            if (Valid())
            {
                return -1;
            };

            //�������ݿ⴦������

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            basesetManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            FS.HISFC.Models.Material.MaterialStorage storage = null;
            foreach (DataRow row in this.dt.Rows)
            {
                storage = new FS.HISFC.Models.Material.MaterialStorage();

                storage.ID = row["�ֿ����"].ToString();                    //0����
                storage.Name = row["�ֿ�����"].ToString();                  //1�ֿ�����
                storage.SpellCode = row["ƴ����"].ToString();              //2ƴ����
                storage.WBCode = row["�����"].ToString();                  //3�����
                storage.OutStartNO = FS.FrameWork.Function.NConvert.ToInt32(row["���ⵥ��ʼ��"]);        //4���ⵥ��ʼ��
                storage.InStartNO = FS.FrameWork.Function.NConvert.ToInt32(row["��ⵥ��ʼ��"]);         //5��ⵥ��ʼ��
                storage.PlanStartNO = FS.FrameWork.Function.NConvert.ToInt32(row["���뵥��ʼ��"]);        //6���뵥��ʼ��
                storage.IsWithFix = FS.FrameWork.Function.NConvert.ToBoolean(row["���޹̶��ʲ�"].ToString());      //7���޹̶��ʲ�
                storage.IsStorage = FS.FrameWork.Function.NConvert.ToBoolean(row["�Ƿ��ǲֿ�"].ToString());         //8�Ƿ��ǲֿ�
                storage.IsStoreManage = FS.FrameWork.Function.NConvert.ToBoolean(row["�Ƿ������"].ToString());   //9�Ƿ������
                storage.IsBatchManage = FS.FrameWork.Function.NConvert.ToBoolean(row["�Ƿ��������"].ToString());   //10�Ƿ��������
                storage.MaxDays = FS.FrameWork.Function.NConvert.ToInt32(row["�����������"]);
                storage.MinDays = FS.FrameWork.Function.NConvert.ToInt32(row["�����������"]);
                storage.ReferenceDays = FS.FrameWork.Function.NConvert.ToInt32(row["���ο�����"]);
                storage.Oper.ID = row["����Ա"].ToString();
                storage.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(row["��������"].ToString());

                //����ִ�и��²��������û�гɹ������������
                if (this.basesetManager.SetStorage(storage) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.basesetManager.Err);
                    return 0;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            //ˢ������
            this.ShowData();

            MessageBox.Show("����ɹ���");
            return 1;
        }

        /// <summary>
        ///  ��Ч���ж� 
        /// </summary>	
        private bool Valid()
        {
            for (int i = 0; i < this.fpStorage_Sheet1.RowCount; i++)
            {
                if (this.fpStorage_Sheet1.Cells[i, 1].Text == "" || this.fpStorage_Sheet1.Cells[i, 1] == null)
                {
                    MessageBox.Show("��" + (i+1).ToString() + "�����Ʋ���Ϊ��");
                    return true;
                }

            }
            return false;
        }

        #endregion

        #region �¼�
        private void ucMatStorage_Load(object sender, EventArgs e)
        {
            this.txtQueryCode.TextChanged += new EventHandler(txtQueryCode_TextChanged);

            this.InitDataSet();

            this.ShowData();

            this.ShowDeptStruct();

            InputMap im;
            im = this.fpStorage.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

        }

        private void txtQueryCode_TextChanged(object sender, EventArgs e)
        {
            this.ChangeItem();
        }

        private void fpStorage_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {

        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.fpStorage.ContainsFocus)
            {
                if (keyData == Keys.Enter)
                {
                    if (this.fpStorage_Sheet1.ActiveColumnIndex == 2)
                    {
                        FS.HISFC.Models.Base.Spell spCode = new FS.HISFC.Models.Base.Spell();
                        FS.HISFC.BizLogic.Manager.Spell mySpell = new FS.HISFC.BizLogic.Manager.Spell();

                        spCode = (FS.HISFC.Models.Base.Spell)mySpell.Get(fpStorage_Sheet1.Cells[this.fpStorage_Sheet1.ActiveRowIndex, 2].Text.ToString());

                        if (spCode.SpellCode.Length > 10)
                            spCode.SpellCode = spCode.SpellCode.Substring(0, 10);
                        if (spCode.WBCode.Length > 10)
                            spCode.WBCode = spCode.WBCode.Substring(0, 10);

                        this.fpStorage_Sheet1.Cells[this.fpStorage_Sheet1.ActiveRowIndex, 3].Value = spCode.SpellCode;
                        this.fpStorage_Sheet1.Cells[this.fpStorage_Sheet1.ActiveRowIndex, 4].Value = spCode.WBCode;
                    }

                    this.fpStorage_Sheet1.ActiveColumnIndex++;
                }
            }
            return base.ProcessDialogKey(keyData);
        }
        #endregion

        #region ��ʼ��������

        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        protected override ToolBarService OnInit(object sender, object NeuObject, object param)
        {
            toolBarService.AddToolButton("����", "���浱ǰ��Ʒ��Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);

            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "����":
                    this.Save();
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.ShowData();

            return base.OnQuery(sender, neuObject);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();

            return base.OnSave(sender, neuObject);
        }

        private void fpStorage_EditChange(object sender, EditorNotifyEventArgs e)
        {
            FS.FrameWork.Management.DataBaseManger data = new FS.FrameWork.Management.DataBaseManger();
            DateTime date = this.basesetManager.GetDateTimeFromSysDateTime();

            this.fpStorage_Sheet1.Cells[e.Row, 14].Text = ((FS.HISFC.Models.Base.Employee)data.Operator).ID;
            this.fpStorage_Sheet1.Cells[e.Row, 15].Text = date.ToString();
        }
        #endregion
    }
}
