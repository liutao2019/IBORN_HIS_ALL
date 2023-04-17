using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Common.Controls
{
    public partial class ucDrugLimitMaintenance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDrugLimitMaintenance()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ҩƷҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item pharmacyManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// ҽ��ҩƷ����ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Order.SpecialLimit specialLimitManager = new FS.HISFC.BizLogic.Order.SpecialLimit();

        /// <summary>
        /// ·��
        /// </summary>
        private string filePath = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\OrderDrugLimit.xml";

        private DataTable dtOrderDrugLimit = new DataTable();

        private DataView dvOrderDrugLimit = new DataView();

        private ArrayList alDrugLimit = new ArrayList();

        /// <summary>
        /// ����Ա
        /// </summary>
        private FS.HISFC.Models.Base.Employee oper = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

        /// <summary>
        /// ToolBarService
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Init()
        {
            this.ucDrugList1.ChooseDataEvent += new FS.HISFC.Components.Common.Controls.ucDrugList.ChooseDataHandler(ucDrugList1_ChooseDataEvent);
            this.ucDrugList1.ShowPharmacyList();
            this.InitFP();
            this.QueryAll();
        }

        /// <summary>
        /// ��ʼ��FP
        /// </summary>
        private void InitFP()
        {
            this.dtOrderDrugLimit.Reset();
            if (System.IO.File.Exists(this.filePath))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(this.filePath, dtOrderDrugLimit, ref dvOrderDrugLimit, this.fpOrderDrugLimit_Sheet1);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpOrderDrugLimit_Sheet1, this.filePath);
            }
            else
            {
                this.dtOrderDrugLimit.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("ҩƷ����",typeof(string)),
                    new DataColumn("ҩƷ����",typeof(string)),
                    new DataColumn("�Ƿ����ϼ�ҽ�����",typeof(bool)),
                    new DataColumn("�Ƿ����ֹ�����",typeof(bool)),
                    new DataColumn("��Ч���",typeof(bool)),
                    new DataColumn("��ע",typeof(string)),
                    new DataColumn("����Ա",typeof(string)),
                    new DataColumn("����ʱ��",typeof(DateTime))
                });

                this.dvOrderDrugLimit = new DataView(this.dtOrderDrugLimit);

                this.fpOrderDrugLimit_Sheet1.DataSource = this.dvOrderDrugLimit;

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpOrderDrugLimit_Sheet1, this.filePath);
            }
        }

        /// <summary>
        /// ��ѯȫ������
        /// </summary>
        private void QueryAll()
        {
            this.dtOrderDrugLimit.Clear();
            this.alDrugLimit = this.specialLimitManager.QueryPharmacyLimit();
            if (alDrugLimit != null || this.alDrugLimit.Count > 0)
            {
                foreach (FS.HISFC.Models.Order.PharmacyLimit obj in alDrugLimit)
                {

                    FS.HISFC.Models.Pharmacy.Item item = null;
                    item = this.pharmacyManager.GetItem(obj.ID);

                    DataRow row = this.dtOrderDrugLimit.NewRow();
                    row["ҩƷ����"] = obj.ID;
                    row["ҩƷ����"] = item.Name;
                    row["�Ƿ����ϼ�ҽ�����"] = obj.IsLeaderCheck;
                    row["�Ƿ����ֹ�����"] = obj.IsNeedRecipe;
                    row["��Ч���"] = obj.IsValid;
                    row["��ע"] = obj.Remark;
                    row["����Ա"] = obj.Oper.ID;
                    row["����ʱ��"] = obj.Oper.OperTime;

                    this.dtOrderDrugLimit.Rows.Add(row);
                }
            }
            this.dtOrderDrugLimit.AcceptChanges();
        }

        /// <summary>
        /// ��ҩƷ������
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private int AddDrugToFp(FS.HISFC.Models.Pharmacy.Item item)
        {
            
            try
            {
                DataRow row = this.dtOrderDrugLimit.NewRow();
                row["ҩƷ����"] = item.ID;
                row["ҩƷ����"] = item.Name;
                row["�Ƿ����ϼ�ҽ�����"] = false;
                row["�Ƿ����ֹ�����"] = false;
                row["��Ч���"] = false;
                row["��ע"] = "";
                row["����Ա"] = this.oper.ID;
                row["����ʱ��"] = DateTime.Now;

                this.dtOrderDrugLimit.Rows.Add(row);
            }
            catch 
            {
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        private int SaveData()
        {
            this.fpOrderDrugLimit.StopCellEditing();
            foreach (System.Data.DataRow row in dtOrderDrugLimit.Rows)
            {
                row.EndEdit();
            }
                        
            DataTable dtSave = this.dtOrderDrugLimit.GetChanges();
            DataTable ss = this.dtOrderDrugLimit.GetChanges(System.Data.DataRowState.Modified);
            if (dtSave != null)
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(this.specialLimitManager.Connection);
                //t.BeginTransaction();
                this.specialLimitManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                
                foreach (DataRow row in dtSave.Rows )
                {
                    FS.HISFC.Models.Order.PharmacyLimit obj = new FS.HISFC.Models.Order.PharmacyLimit();

                    obj.ID = row["ҩƷ����"].ToString().Trim();
                    obj.Name = row["ҩƷ����"].ToString().Trim();
                    obj.IsLeaderCheck = FS.FrameWork.Function.NConvert.ToBoolean(row["�Ƿ����ϼ�ҽ�����"]);
                    obj.IsNeedRecipe = FS.FrameWork.Function.NConvert.ToBoolean(row["�Ƿ����ֹ�����"]);
                    obj.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(row["��Ч���"]);
                    obj.Remark = row["��ע"].ToString().Trim();
                    obj.Oper.ID = oper.ID;
                    obj.Oper.OperTime = this.specialLimitManager.GetDateTimeFromSysDateTime();

                    int iReturn = 0;
                    iReturn = this.specialLimitManager.UpdateSpecialLimit(obj);
                    if (iReturn == 0)
                    {
                        if (this.specialLimitManager.InsertSpecialLimit(obj) < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ҽ��ҩƷ������Ϣ����!") + this.specialLimitManager.Err);
                            return -1;
                        }
                    }
                    else if (iReturn < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ҽ��ҩƷ������Ϣ����!") + this.specialLimitManager.Err);
                        return -1;
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                this.dtOrderDrugLimit.AcceptChanges();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ɹ�"));
            }
            
            return 0;
        }

        private void ColumnSet()
        {
            FS.HISFC.Components.Common.Controls.ucSetColumn uc = new FS.HISFC.Components.Common.Controls.ucSetColumn();
            uc.FilePath = this.filePath;
            uc.SetColVisible(true, true, false, false);
            uc.SetDataTable(this.filePath, this.fpOrderDrugLimit.Sheets[0]);
            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��ʾ����";
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
            uc.DisplayEvent += new EventHandler(ucSetColumn_DisplayEvent);
            this.ucSetColumn_DisplayEvent(null, null);
        }

        private void ucSetColumn_DisplayEvent(object sender, EventArgs e)
        {

        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveData();
            this.QueryAll();
            return base.OnSave(sender, neuObject);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryAll();
            return base.OnQuery(sender, neuObject);
        }

        public override FS.FrameWork.WinForms.Forms.ToolBarService Init(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("������", "���������", FS.FrameWork.WinForms.Classes.EnumImageList.S����, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "������":
                    this.ColumnSet();
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

        private void ucDrugList1_ChooseDataEvent(FarPoint.Win.Spread.SheetView sv,int activeRow)
        {
            if (activeRow < 0)
            {
                return;
            }
            string drugCode = sv.Cells[activeRow, 0].Text;

            FS.HISFC.Models.Pharmacy.Item item = this.pharmacyManager.GetItem(drugCode);
            if (item == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg(this.pharmacyManager.Err));
            }
            this.AddDrugToFp(item);
        }

        private void ucDrugLimitMaintenance_Load(object sender, EventArgs e)
        {
            this.Init();
        }
    }
}

