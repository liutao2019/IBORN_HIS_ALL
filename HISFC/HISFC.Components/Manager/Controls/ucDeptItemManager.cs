using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;

namespace FS.HISFC.Components.Manager.Controls
{
    /// <summary>
    /// [��������: ���ҳ�����Ŀά��]<br></br>
    /// [�� �� ��: ��ΰ��]<br></br>
    /// [����ʱ��: 2006��10��27]<br></br>
    /// 
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucDeptItemManager : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private FS.HISFC.BizLogic.Manager.DeptItem diBusiness = new FS.HISFC.BizLogic.Manager.DeptItem();
        private System.Data.DataView dvPharmacy = new DataView();
        private System.Data.DataView dvUndrugItem = new DataView();
        private System.Data.DataView dvCombo = new DataView();
        private List<FS.HISFC.Models.Base.DeptItem> itemList = new List<FS.HISFC.Models.Base.DeptItem>();

        private FilterType filterType;

        private Dictionary<string, DataSet> dicDictionary = new Dictionary<string, DataSet>();

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("ɾ��", "", 0, true, false, null);
            return this.toolBarService;
        }
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "ɾ��":
                    this.DelItem();
                    break;
                default:
                    break;
            }
        }
        protected override int OnSave(object sender, object neuObject)
        {
            if( this.fpDeptItem_Sheet1.RowCount <= 0)
            {
                MessageBox.Show("û��Ҫ���������","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return -1;
            }

            for (int i = 0, j = this.fpDeptItem_Sheet1.RowCount; i < j; i++)
            {
                FS.HISFC.Models.Base.DeptItem deptItem = new FS.HISFC.Models.Base.DeptItem();

                //deptItem.Dept.ID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
                deptItem.Dept.ID = this.fpDeptItem_Sheet1.GetText(i, this.GetCloumn("���ұ��"));
                deptItem.ItemProperty.ID = this.fpDeptItem_Sheet1.GetText(i, this.GetCloumn("��Ŀ���"));
                deptItem.ItemProperty.Name = this.fpDeptItem_Sheet1.GetText(i, this.GetCloumn("��Ŀ����"));

                switch (this.fpDeptItem_Sheet1.GetText(i, this.GetCloumn("��λ��ʶ")).Trim())
                {
                    case "ҩƷ":
                        deptItem.UnitFlag = "0";
                        break;
                    case "0":
                        deptItem.UnitFlag = "0";
                        break;
                    case "��ҩƷ":
                        deptItem.UnitFlag = "1";
                        break;
                    case "1":
                        deptItem.UnitFlag = "1";
                        break;
                    case "�����Ŀ":
                        deptItem.UnitFlag = "2";
                        break;
                    case "2":
                        deptItem.UnitFlag = "2";
                        break;
                    default:
                        break;
                }

                deptItem.BookLocate = this.fpDeptItem_Sheet1.GetText(i, this.GetCloumn("ԤԼ��"));
                deptItem.BookTime = this.fpDeptItem_Sheet1.GetText(i, this.GetCloumn("ԤԼ�̶�ʱ��"));
                deptItem.ExecuteLocate = this.fpDeptItem_Sheet1.GetText(i, this.GetCloumn("ִ�еص�"));
                deptItem.ReportDate = this.fpDeptItem_Sheet1.GetText(i, this.GetCloumn("ȡ����ʱ��"));
                deptItem.HurtFlag = this.fpDeptItem_Sheet1.GetText(i, this.GetCloumn("�Ƿ��д�")).Trim().Equals("��") ? "0" : "1";
                deptItem.SelfBookFlag = this.fpDeptItem_Sheet1.GetText(i, this.GetCloumn("�Ƿ����ԤԼ")).Trim().Equals("��") ? "0" : "1";
                deptItem.ReasonableFlag = this.fpDeptItem_Sheet1.GetText(i, this.GetCloumn("֪��ͬ����")).Trim().Equals("��Ҫ") ? "0" : "1";
                deptItem.IsStat = this.fpDeptItem_Sheet1.GetText(i, this.GetCloumn("�Ƿ���Ҫͳ��")).Trim().Equals("��Ҫ") ? "0" : "1";
                deptItem.IsAutoBook = this.fpDeptItem_Sheet1.GetText(i, this.GetCloumn("�Ƿ��Զ�ԤԼ")).Trim().Equals("��Ҫ") ? "0" : "1";
                deptItem.Speciality = this.fpDeptItem_Sheet1.GetText(i, this.GetCloumn("����רҵ"));
                deptItem.ClinicMeaning = this.fpDeptItem_Sheet1.GetText(i, this.GetCloumn("�ٴ�����"));
                deptItem.SampleKind = this.fpDeptItem_Sheet1.GetText(i, this.GetCloumn("�걾"));
                deptItem.SampleWay = this.fpDeptItem_Sheet1.GetText(i, this.GetCloumn("��������"));
                deptItem.SampleUnit = this.fpDeptItem_Sheet1.GetText(i, this.GetCloumn("�걾��λ"));

                deptItem.SampleQty = FS.FrameWork.Function.NConvert.ToDecimal(this.fpDeptItem_Sheet1.GetText(i, this.GetCloumn("�걾��")));

                deptItem.SampleContainer = this.fpDeptItem_Sheet1.GetText(i, this.GetCloumn("�ٴ�����"));
                deptItem.Scope = this.fpDeptItem_Sheet1.GetText(i, this.GetCloumn("����ֵ��Χ"));
                deptItem.ItemTime = this.fpDeptItem_Sheet1.GetText(i, this.GetCloumn("��Ŀִ������ʱ��"));
                deptItem.Memo = this.fpDeptItem_Sheet1.GetText(i, this.GetCloumn("ע������"));
                //
                deptItem.CustomName = this.fpDeptItem_Sheet1.GetText(i, this.GetCloumn("��������"));

                if (deptItem.Dept.ID == "" || deptItem.Dept.Name == null)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                    //t.BeginTransaction();

                    this.diBusiness.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                    if (this.diBusiness.InsertItem(deptItem) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.diBusiness.Err, FS.FrameWork.Management.Language.Msg("��������ʧ��!"));
                        return -1;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                    //MessageBox.Show(FS.FrameWork.Management.Language.Msg("�������ݳɹ�"));
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                    //t.BeginTransaction();

                    this.diBusiness.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                    if (this.diBusiness.UpdateItem(deptItem) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.diBusiness.Err, FS.FrameWork.Management.Language.Msg("��������ʧ��!"));
                        return -1;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                }

                //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                //t.BeginTransaction();
                //this.diBusiness.SetTrans(t.Trans);
                //if (this.diBusiness.InsertItem(deptItem) == -1)
                //{
                //    if (this.diBusiness.UpdateItem(deptItem) == -1)
                //    {
                //        FS.FrameWork.Management.PublicTrans.RollBack();;
                //        MessageBox.Show(this.diBusiness.Err, FS.FrameWork.Management.Language.Msg("��������ʧ��!"));
                //        return -1;
                //    }
                //}
                //FS.FrameWork.Management.PublicTrans.Commit();;
            }

            MessageBox.Show(FS.FrameWork.Management.Language.Msg("�������ݳɹ�!"));
                
            this.Init();
            //this.FillPharmacy();
            //this.FillUndrugItem();
            //this.FillComboItem();
            return 1;
        }

        private void DelItem()
        {
            if (this.fpDeptItem_Sheet1.RowCount <= 0)
            {
                return;
            }
            int rowIndex = this.fpDeptItem_Sheet1.ActiveRowIndex;

            if (rowIndex >= 0)
            {
                DialogResult rs = MessageBox.Show("�Ƿ�ȷ��ɾ����Ŀ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rs == DialogResult.No)
                {
                    return;
                }
            }

            string deptID = this.fpDeptItem_Sheet1.GetText(rowIndex, 0);
            string itemID = this.fpDeptItem_Sheet1.GetText(rowIndex, 1);

            if (deptID == null || deptID == "")
            {
                this.fpDeptItem_Sheet1.RemoveRows(rowIndex, 1);
                return;
            }
            else
            {
                this.fpDeptItem_Sheet1.RemoveRows(rowIndex, 1);
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(this.diBusiness.Connection);
            //trans.BeginTransaction();
            this.diBusiness.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            try
            {
                if ( this.diBusiness.DeleteItem(deptID, itemID) == -1 )
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("ɾ������ʧ��!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch(Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(ex.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            this.Init();
        }

        #endregion

        public ucDeptItemManager()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                this.neuSpread1.ActiveSheetChanged += new EventHandler(neuSpread1_ActiveSheetChanged);

                neuSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);

                fpDeptItem.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpDeptItem_ColumnWidthChanged);
            }
        }

        string fpDeptPhaSetting = FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\DeptPhaItemSetting.xml";

        string fpDeptUndrugSetting = FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\DeptUndrugItemSetting.xml";

        string fpDeptCombSetting = FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\DeptCombItemSetting.xml";

        void fpDeptItem_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            if (fpDeptItem.ActiveSheetIndex == 0)
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpDeptItem.Sheets[0], fpDeptPhaSetting);
            }
            else if (fpDeptItem.ActiveSheetIndex == 1)
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpDeptItem.Sheets[1], fpDeptUndrugSetting);
            }
            else if (fpDeptItem.ActiveSheetIndex == 2)
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpDeptItem.Sheets[2], fpDeptCombSetting);
            }
        }

        string fpDicDeptPhaSetting = FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\DicDeptPhaItemSetting.xml";

        string fpDicDeptUndrugSetting = FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\DicDeptUndrugItemSetting.xml";

        string fpDicDeptCombSetting = FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\DicDeptCombItemSetting.xml";

        void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            if (neuSpread1.ActiveSheetIndex == 0)
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1.Sheets[0], fpDicDeptPhaSetting);
            }
            else if (neuSpread1.ActiveSheetIndex == 1)
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1.Sheets[1], fpDicDeptUndrugSetting);
            }
            else if (neuSpread1.ActiveSheetIndex == 2)
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1.Sheets[2], fpDicDeptCombSetting);
            }
        }

        void neuSpread1_ActiveSheetChanged(object sender, EventArgs e)
        {
            //ÿ�ζ����°󶨿��Ա����������£���֪���Բ��ԣ��ٶ���ͦ��
            switch (neuSpread1.ActiveSheetIndex)
            {
                case 0:
                    this.filterType = FilterType.P;
                    this.dvPharmacy = new DataView();
                    this.FillPharmacy();
                    this.tbFilterValue.Text = "";
                    this.tbFilterValue.Focus();
                    this.GetItems("0");
                    break;
                case 1:
                    this.filterType = FilterType.U;
                    this.dvUndrugItem = new DataView();
                    this.FillUndrugItem();
                    this.tbFilterValue.Text = "";
                    this.tbFilterValue.Focus();
                    this.GetItems("1");
                    break;
                case 2:
                    this.filterType = FilterType.C;
                    this.dvCombo = new DataView();
                    this.FillComboItem();
                    this.tbFilterValue.Text = "";
                    this.tbFilterValue.Focus();
                    this.GetItems("2");
                    break;
                default:
                    break;
            }
        }

        private void ucDeptItemManager_Load(object sender, EventArgs e)
        {
            /*
             *  ���������ԭ����Ҫ�õģ�����ʵ�����岢�����������أ�
             * 
             *  �����ڵ�ԭ�򣺿���ѡ�����еĽڵ�(����)����ά��һ�����ҵĳ�����Ŀ.
             * 
             *  �����ʹ�����������ôֻά����ǰ����Ա����½�Ŀ��ҵĳ�����Ŀ��
             * 
             *  ��ҵ�����������ұ��
             *  
             */
            this.npTree.Visible = false;
            this.tvPatientList1.Visible = false;
            /*******************************************************************/

            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ�������...", false);
                
                FillPharmacy();
                FillUndrugItem();
                FillComboItem();
                //{BF42D3A2-5342-40be-85EB-897DCBF9B794}
                //this.Init();

                this.neuSpread1.ActiveSheetIndex = 0;
                this.GetItems("0");

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            catch
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }

        /// <summary>
        /// ���ڳ�ʼ��
        /// </summary>
        /// <returns>1,�ɹ�; -1,ʧ��</returns>
        private int Init()
        {
            //{BF42D3A2-5342-40be-85EB-897DCBF9B794}
            List<FS.HISFC.Models.Base.DeptItem> lstItems = new List<FS.HISFC.Models.Base.DeptItem>();
            if (this.diBusiness.SelectItem(ref lstItems) == -1)
            {
                return -1;
            }
            this.InitFarPoint(lstItems);
            this.itemList = lstItems;

            this.GetItems(this.neuSpread1.ActiveSheetIndex.ToString());

            return 1;
        }
        private int GetItems(string UnitFlag)
        {
            List<FS.HISFC.Models.Base.DeptItem> lstItems = new List<FS.HISFC.Models.Base.DeptItem>();
            if (this.diBusiness.SelectItemByUint(ref lstItems,UnitFlag) == -1)
            {
                return -1;
            }
            this.InitFarPoint(lstItems);
            this.itemList = lstItems;

            if (UnitFlag == "0")
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpDeptItem.Sheets[0], fpDeptPhaSetting);
            }
            else if (UnitFlag == "1")
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpDeptItem.Sheets[1], fpDeptUndrugSetting);
            }
            else if (UnitFlag == "2")
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpDeptItem.Sheets[2], fpDeptCombSetting);
            }

            return 1; 
        }

        /// <summary>
        /// �ö����ʼ��FarPoint
        /// </summary>
        /// <param name="item"></param>
        private void InitFarPoint(List<FS.HISFC.Models.Base.DeptItem> lstItems)
        {
            if (this.fpDeptItem_Sheet1.RowCount > 0)
            {
                this.fpDeptItem_Sheet1.RemoveRows(0, this.fpDeptItem_Sheet1.RowCount);
            }
            this.fpDeptItem_Sheet1.Rows.Remove(0, this.fpDeptItem_Sheet1.Rows.Count);
            for (int i = 0, j = lstItems.Count; i < j; i++)
            {
                this.fpDeptItem_Sheet1.Rows.Add(i, 1);

                this.fpDeptItem_Sheet1.SetText(i, this.GetCloumn("���ұ��"), lstItems[i].Dept.ID);
                this.fpDeptItem_Sheet1.SetText(i, this.GetCloumn("��Ŀ���"), lstItems[i].ItemProperty.ID);
                this.fpDeptItem_Sheet1.SetText(i, this.GetCloumn("���ұ���"), lstItems[i].ItemProperty.GBCode);
                this.fpDeptItem_Sheet1.SetText(i, this.GetCloumn("��Ŀ����"), lstItems[i].ItemProperty.Name);
                switch (lstItems[i].UnitFlag.Trim())
                {
                    case "0":
                        this.fpDeptItem_Sheet1.SetText(i, this.GetCloumn("��λ��ʶ"), "ҩƷ");
                        break;
                    case "1":
                        this.fpDeptItem_Sheet1.SetText(i, this.GetCloumn("��λ��ʶ"), "��ҩƷ");
                        break;
                    case "2":
                        this.fpDeptItem_Sheet1.SetText(i, this.GetCloumn("��λ��ʶ"), "�����Ŀ");
                        break;
                    default:
                        break;
                }
                //this.fpDeptItem_Sheet1.SetText(i, this.GetCloumn("��λ��ʶ"), lstItems[i].UnitFlag);
                this.fpDeptItem_Sheet1.SetText(i, this.GetCloumn("ԤԼ��"), lstItems[i].BookLocate);
                this.fpDeptItem_Sheet1.SetText(i, this.GetCloumn("ԤԼ�̶�ʱ��"), lstItems[i].BookTime);
                this.fpDeptItem_Sheet1.SetText(i, this.GetCloumn("ִ�еص�"), lstItems[i].ExecuteLocate);
                this.fpDeptItem_Sheet1.SetText(i, this.GetCloumn("ȡ����ʱ��"), lstItems[i].ReportDate);
                this.fpDeptItem_Sheet1.SetText(i, this.GetCloumn("�Ƿ��д�"), lstItems[i].HurtFlag);
                this.fpDeptItem_Sheet1.SetText(i, this.GetCloumn("�Ƿ����ԤԼ"), lstItems[i].SelfBookFlag);
                this.fpDeptItem_Sheet1.SetText(i, this.GetCloumn("֪��ͬ����"), lstItems[i].ReasonableFlag);
                this.fpDeptItem_Sheet1.SetText(i, this.GetCloumn("����רҵ"), lstItems[i].Speciality);
                this.fpDeptItem_Sheet1.SetText(i, this.GetCloumn("�ٴ�����"), lstItems[i].ClinicMeaning);
                this.fpDeptItem_Sheet1.SetText(i, this.GetCloumn("�걾"), lstItems[i].SampleKind);
                this.fpDeptItem_Sheet1.SetText(i, this.GetCloumn("��������"), lstItems[i].SampleWay);
                this.fpDeptItem_Sheet1.SetText(i, this.GetCloumn("�걾��λ"), lstItems[i].SampleUnit);
                this.fpDeptItem_Sheet1.SetText(i, this.GetCloumn("�걾��"), lstItems[i].SampleQty.ToString());
                this.fpDeptItem_Sheet1.SetText(i, this.GetCloumn("����"), lstItems[i].SampleContainer);
                this.fpDeptItem_Sheet1.SetText(i, this.GetCloumn("����ֵ��Χ"), lstItems[i].Scope);
                this.fpDeptItem_Sheet1.SetText(i, this.GetCloumn("�Ƿ���Ҫͳ��"), lstItems[i].IsStat);
                this.fpDeptItem_Sheet1.SetText(i, this.GetCloumn("�Ƿ��Զ�ԤԼ"), lstItems[i].IsAutoBook);
                this.fpDeptItem_Sheet1.SetText(i, this.GetCloumn("��Ŀִ������ʱ��"), lstItems[i].ItemTime);
                this.fpDeptItem_Sheet1.SetText(i, this.GetCloumn("ע������"), lstItems[i].Memo);
                //
                this.fpDeptItem_Sheet1.SetText(i, this.GetCloumn("��������"), lstItems[i].CustomName);
            }
        }

        /// <summary>
        /// ��Ŀ�Ƿ����
        /// </summary>
        /// <param name="itemID">��Ŀ���</param>
        /// <param name="itemName">��Ŀ����</param>
        /// <returns>true: ����;  false:������</returns>
        private bool IsExist(string itemID, string itemName)
        {
            for (int i = 0, j = this.fpDeptItem_Sheet1.Rows.Count; i < j; i++)
            {
                if (this.fpDeptItem_Sheet1.GetText(i, this.GetCloumn("��Ŀ���")).Trim().Equals(itemID) &&
                    this.fpDeptItem_Sheet1.GetText(i, this.GetCloumn("��Ŀ����")).Trim().Equals(itemName))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// ����һ�����У�׼���������ݿ�
        /// </summary>
        /// <param name="itemID">��Ŀ���</param>
        /// <param name="gbCode">���ұ���</param>
        /// <param name="itemName">��Ŀ����</param>
        /// <param name="itemType">��λ��ʶ(1.ҩƷ   2.��ҩƷ  3.������Ŀ)</param>
        private void CreateLine(string itemID,string gbCode, string itemName, string itemType)
        {
            //����һ������
            int rowIndex = 0;
            this.fpDeptItem_Sheet1.Rows.Add(rowIndex, 1);

            //��0������ҵ���ת���õ���,����������Ϊ��ʱ����
            this.fpDeptItem_Sheet1.SetText(rowIndex, this.GetCloumn("��Ŀ���"), itemID);
            this.fpDeptItem_Sheet1.SetText(rowIndex, this.GetCloumn("���ұ���"), gbCode);
            this.fpDeptItem_Sheet1.SetText(rowIndex, this.GetCloumn("��Ŀ����"), itemName);

            this.fpDeptItem_Sheet1.SetText(rowIndex, this.GetCloumn("��������"), itemName);

            switch (itemType)
            {
                case "0":
                    this.fpDeptItem_Sheet1.SetText(rowIndex, this.GetCloumn("��λ��ʶ"), "ҩƷ");
                    break;
                case "1":
                    this.fpDeptItem_Sheet1.SetText(rowIndex, this.GetCloumn("��λ��ʶ"), "��ҩƷ");
                    break;
                case "2":
                    this.fpDeptItem_Sheet1.SetText(rowIndex, this.GetCloumn("��λ��ʶ"), "�����Ŀ");
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// ����FarPoint�е�һ��,����һ������
        /// </summary>
        /// <param name="row">������</param>
        /// <returns>����</returns>
        private FS.HISFC.Models.Base.DeptItem CreateDeptItem(int row)
        {
            FS.HISFC.Models.Base.DeptItem deptitem = new FS.HISFC.Models.Base.DeptItem();
            
            deptitem.Dept.ID = this.fpDeptItem_Sheet1.GetText(row, this.GetCloumn("���ұ��"));
            deptitem.ItemProperty.ID = this.fpDeptItem_Sheet1.GetText(row, this.GetCloumn("��Ŀ���"));
            deptitem.ItemProperty.GBCode = this.fpDeptItem_Sheet1.GetText(row, this.GetCloumn("���ұ���"));
            deptitem.ItemProperty.Name = this.fpDeptItem_Sheet1.GetText(row, this.GetCloumn("��Ŀ����"));
            //ϵͳ���:deptitem.ItemProperty.SysClass��ʱ�Ȳ�д
            switch (this.fpDeptItem_Sheet1.GetText(row, this.GetCloumn("��λ��ʶ")).Trim())
            {
                case "ҩƷ":
                    deptitem.UnitFlag = "0";
                    break;
                case "��ҩƷ":
                    deptitem.UnitFlag = "1";
                    break;
                case "�����Ŀ":
                    deptitem.UnitFlag = "2";
                    break;
                default:
                    break;
            }
        
            //deptitem.UnitFlag = this.fpDeptItem_Sheet1.GetText(row, this.GetCloumn("��λ��ʶ"));
            deptitem.BookLocate = this.fpDeptItem_Sheet1.GetText(row, this.GetCloumn("ԤԼ��"));
            deptitem.BookTime = this.fpDeptItem_Sheet1.GetText(row, this.GetCloumn("ԤԼ�̶�ʱ��"));
            deptitem.ExecuteLocate = this.fpDeptItem_Sheet1.GetText(row, this.GetCloumn("ִ�еص�"));
            deptitem.ReportDate = this.fpDeptItem_Sheet1.GetText(row, this.GetCloumn("ȡ����ʱ��"));
            deptitem.HurtFlag = this.fpDeptItem_Sheet1.GetText(row, this.GetCloumn("�Ƿ��д�"));
            deptitem.SelfBookFlag = this.fpDeptItem_Sheet1.GetText(row, this.GetCloumn("�Ƿ����ԤԼ"));
            deptitem.ReasonableFlag = this.fpDeptItem_Sheet1.GetText(row, this.GetCloumn("֪��ͬ����"));
            deptitem.Speciality = this.fpDeptItem_Sheet1.GetText(row, this.GetCloumn("����רҵ"));
            deptitem.ClinicMeaning = this.fpDeptItem_Sheet1.GetText(row, this.GetCloumn("�ٴ�����"));
            deptitem.SampleKind = this.fpDeptItem_Sheet1.GetText(row, this.GetCloumn("�걾"));
            deptitem.SampleWay = this.fpDeptItem_Sheet1.GetText(row, this.GetCloumn("��������"));
            deptitem.SampleUnit = this.fpDeptItem_Sheet1.GetText(row, this.GetCloumn("�걾��λ"));

            deptitem.SampleQty = FS.FrameWork.Function.NConvert.ToDecimal(this.fpDeptItem_Sheet1.GetText(row, this.GetCloumn("�걾��")));
            
            deptitem.SampleContainer = this.fpDeptItem_Sheet1.GetText(row, this.GetCloumn("����"));
            deptitem.Scope = this.fpDeptItem_Sheet1.GetText(row, this.GetCloumn("����ֵ��Χ"));
            deptitem.IsStat = this.fpDeptItem_Sheet1.GetText(row, this.GetCloumn("�Ƿ���Ҫͳ��"));
            deptitem.IsAutoBook = this.fpDeptItem_Sheet1.GetText(row, this.GetCloumn("�Ƿ��Զ�ԤԼ"));
            deptitem.ItemTime = this.fpDeptItem_Sheet1.GetText(row, this.GetCloumn("��Ŀִ������ʱ��"));
            deptitem.Memo = this.fpDeptItem_Sheet1.GetText(row, this.GetCloumn("ע������"));
            //
            deptitem.CustomName = this.fpDeptItem_Sheet1.GetText(row, this.GetCloumn("��������"));
            return deptitem;
        }

        /// <summary>
        /// ����ָ��������,�õ���Ӧ��λ������
        /// </summary>
        /// <param name="name">����</param>
        /// <returns>>=0:λ������,  -1:ʧ��</returns>
        private int GetCloumn(string name)
        {
            for (int i = 0; i < this.fpDeptItem_Sheet1.Columns.Count; i++)
            {
                if (name == this.fpDeptItem_Sheet1.Columns[i].Label)
                {
                    return i;
                }
            }

            return -1;
        }
        
        /// <summary>
        /// ���ҩƷ��Ϣ
        /// </summary>
        private void FillPharmacy()
        {
            DataSet ds = new DataSet();
            if (dicDictionary.ContainsKey("PhtItem"))
            {
                ds = dicDictionary["PhtItem"];
            }
            else
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯҩƷ�ֵ䣬���Ժ�>>");
                Application.DoEvents();
                if (this.diBusiness.QueryPhaItem(ref ds) == -1)
                {
                    MessageBox.Show("���ҩƷ��Ŀʧ��!");
                    return;
                }
                dicDictionary.Add("PhtItem", ds);

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            this.dvPharmacy.Table = ds.Tables[0];
            this.fpsPha.DataSource = this.dvPharmacy;

            //if (System.IO.File.Exists(this.fpDeptPhaSetting))
            //{
            FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuSpread1.Sheets[0], fpDicDeptPhaSetting);
            //}
        }

        /// <summary>
        /// ����ҩƷ��Ϣ
        /// </summary>
        private void FillUndrugItem()
        {
            DataSet ds = new DataSet();

            if (dicDictionary.ContainsKey("UndrugItem"))
            {
                ds = dicDictionary["UndrugItem"];
            }
            else
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ��ҩƷ�ֵ䣬���Ժ�>>");
                Application.DoEvents();
                if (this.diBusiness.QueryUndrugItem(ref ds) == -1)
                {
                    MessageBox.Show("��÷�ҩƷ��Ŀʧ��!");
                    return;
                }
                dicDictionary.Add("UndrugItem", ds);
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            this.dvUndrugItem.Table = ds.Tables[0];

            this.fpsUndrug.DataSource = this.dvUndrugItem;


            FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuSpread1.Sheets[1], fpDicDeptUndrugSetting);
        }

        /// <summary>
        /// ��������Ŀ��Ϣ
        /// </summary>
        private void FillComboItem()
        {
            DataSet ds = new DataSet();
            if (dicDictionary.ContainsKey("CombItem"))
            {
                ds = dicDictionary["CombItem"];
            }
            else
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ�����Ŀ�ֵ䣬���Ժ�>>");
                Application.DoEvents();
                if (this.diBusiness.QueryComboItem(ref ds) == -1)
                {
                    MessageBox.Show("��������Ŀʧ��!");
                    return;
                }
                dicDictionary.Add("CombItem", ds);
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            this.dvCombo.Table = ds.Tables[0];
            this.fpsCombo.DataSource = this.dvCombo;


            FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuSpread1.Sheets[2], fpDicDeptCombSetting);
        }

        /// <summary>
        /// ������(�������������)����Ҫ���ݿ����б�ά����Ŀʱ�ټ���
        /// </summary>
        private void FillDeptTree()
        {
            TreeNode root = new TreeNode("�����б�");
            root.Tag = null;
            this.tvPatientList1.Nodes.Add(root);

            // ����Ա
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                FS.HISFC.BizProcess.Integrate.Manager deptManager = new FS.HISFC.BizProcess.Integrate.Manager();
                System.Collections.ArrayList alDept = new System.Collections.ArrayList();
                alDept = deptManager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.C);
                if (alDept == null)
                {
                    return;
                }
                foreach (FS.FrameWork.Models.NeuObject obj in alDept)
                {
                    TreeNode node = new TreeNode();
                    node.Text = obj.Name;
                    node.Tag = obj;
                    root.Nodes.Add(node);
                }
            }
            else
            {
                FS.FrameWork.Models.NeuObject obj = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept;
                TreeNode node = new TreeNode();
                node.Text = obj.Name;
                node.Tag = obj;
                root.Nodes.Add(node);
            }

            this.tvPatientList1.ExpandAll();
        }

        private void tbFilterValue_KeyUp(object sender, KeyEventArgs e)
        {
            List<FS.HISFC.Models.Base.DeptItem> myItemList = new List<FS.HISFC.Models.Base.DeptItem>();
            switch (this.filterType)
            {
                case FilterType.P:
                    this.dvPharmacy.RowFilter = "��Ʒ��ƴ���� like '" + this.tbFilterValue.Text + "%'" + " or ��Ʒ������� like '" + this.tbFilterValue.Text + "%'" + " or ��Ʒ���Զ����� like '" + this.tbFilterValue.Text + "%'" + " or ͨ����ƴ���� like '" + this.tbFilterValue.Text + "%'" + " or ͨ��������� like '" + this.tbFilterValue.Text + "%'" + " or ͨ�����Զ����� like '" + this.tbFilterValue.Text + "%'" + " or ���ұ��� like '%" + this.tbFilterValue.Text + "%'";
                    this.dvPharmacy.RowStateFilter = DataViewRowState.CurrentRows;
                    break;
                case FilterType.U:
                    this.dvUndrugItem.RowFilter = "������ like '" + this.tbFilterValue.Text + "%'" + " or ƴ���� like '" + this.tbFilterValue.Text + "%'" + " or ��� like '" + this.tbFilterValue.Text + "%'" + " or ���ұ��� like '%" + this.tbFilterValue.Text + "%'";
                    this.dvUndrugItem.RowStateFilter = DataViewRowState.CurrentRows;
                    break;
                case FilterType.C:
                    this.dvCombo.RowFilter = "������ like '" + this.tbFilterValue.Text + "%'" + " or ƴ���� like '" + this.tbFilterValue.Text + "%'" + " or ��� like '" + this.tbFilterValue.Text + "%'" + " or ���׹��ұ��� like '%" + this.tbFilterValue.Text + "%'";
                    this.dvCombo.RowStateFilter = DataViewRowState.CurrentRows;
                    break;
                default:
                    break;
            }

            //�����޸ĵ�ʱ������������
            //myItemList = this.itemList.Where(item => (item.ItemProperty.GBCode.ToLower().Contains(this.tbFilterValue.Text.ToLower())
            //                                      || item.ItemProperty.SpellCode.ToLower().Contains(this.tbFilterValue.Text.ToLower())
            //                                      || item.ItemProperty.WBCode.ToLower().Contains(this.tbFilterValue.Text.ToLower()))).ToList();
            //this.InitFarPoint(myItemList);

            FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuSpread1.Sheets[0], fpDicDeptPhaSetting);
            FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuSpread1.Sheets[1], fpDicDeptUndrugSetting);
            FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuSpread1.Sheets[2], fpDicDeptCombSetting);
        }        

        private void neuSpread1_ActiveSheetChanging(object sender, FarPoint.Win.Spread.ActiveSheetChangingEventArgs e)
        {
            
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string itemID = this.neuSpread1.ActiveSheet.GetText(e.Row, 0);
            string gbCode = this.neuSpread1.ActiveSheet.GetText(e.Row, 1);
            string itemName = this.neuSpread1.ActiveSheet.GetText(e.Row, 2);
            string itemType = this.neuSpread1.ActiveSheetIndex.ToString();

            if (IsExist(itemID, itemName))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��Ŀ�Ѿ�����"));
                return;
            }
            this.CreateLine(itemID,gbCode, itemName, itemType);
        }

        private void fpDeptItem_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //
            // ���˫��������ͷ���򷵻�
            //
            if (e.ColumnHeader)
            {
                return;
            }
            FS.HISFC.Models.Base.DeptItem item = this.CreateDeptItem(e.Row);
            ucDeptItem deptItemWindow = new ucDeptItem();
            deptItemWindow.InsertSuccessed += new InsertSuccessedHandler(deptItemWindow_InsertSuccessed);

            //
            // �������ֵ�������Ǹ��»��ǲ���
            //
            deptItemWindow.Tag = this.fpDeptItem_Sheet1.GetText(e.Row, this.GetCloumn("���ұ��"));
            deptItemWindow.ShowWindow(item);

            FS.FrameWork.WinForms.Classes.Function.ShowControl(deptItemWindow);
        }

        /// <summary>
        /// �ڵ��������б���ɹ�ʱ���¼�������
        /// </summary>
        private void deptItemWindow_InsertSuccessed()
        {
            //this.fpDeptItem_Sheet1.Rows.Remove(0, this.fpDeptItem_Sheet1.Rows.Count);

            this.Init();
        }

        #region ��ȥ�õ�

        //private void tvPatientList1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        //{
        //    if (e.Node == null || e.Node.Tag == null)
        //    {
        //        MessageBox.Show("��ѡ��һ����Ч����!");
        //        return;
        //    }
        //    string deptID = ((FS.FrameWork.Models.NeuObject)e.Node.Tag).ID;
        //    this.Init(deptID);
        //}
        ///// <summary>
        ///// ���ڳ�ʼ��
        ///// </summary>
        ///// <returns>1,�ɹ�; -1,ʧ��</returns>
        //private int Init(string deptID)
        //{
        //    List<FS.HISFC.Models.Base.DeptItem> lstItems = new List<FS.HISFC.Models.Base.DeptItem>();
        //    if (this.diBusiness.SelectItem(ref lstItems, deptID) == -1)
        //    {
        //        return -1;
        //    }
        //    this.InitFarPoint(lstItems);

        //    this.neuSpread1.ActiveSheetIndex = 0;

        //    return 1;
        //}
        //private void neuComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    switch (this.neuComboBox1.Text.Trim())
        //    {
        //        case "ҩƷ��Ŀ":
        //            this.neuSpread1.ActiveSheetIndex = 0;
        //            this.filterType = FilterType.P;
        //            break;
        //        case "��ҩƷ��Ŀ":
        //            this.neuSpread1.ActiveSheetIndex = 1;
        //            this.filterType = FilterType.U;
        //            break;
        //        case "�����Ŀ":
        //            this.neuSpread1.ActiveSheetIndex = 2;
        //            this.filterType = FilterType.C;
        //            break;
        //        default:
        //            break;
        //    }
        //}

        ///// <summary>
        ///// ����һ������
        ///// </summary>
        ///// <param name="obj"></param>
        //private void CreateLine(FS.FrameWork.Models.NeuObject obj)
        //{
        //    //����һ������
        //    int rowIndex = this.fpDeptItem_Sheet1.Rows.Count;
        //    this.fpDeptItem_Sheet1.Rows.Add(rowIndex, 1);

        //    //��0������ҵ���ת���õ���,����������Ϊ��ʱ����
        //    this.fpDeptItem_Sheet1.SetText(rowIndex, this.GetCloumn("��Ŀ���"), obj.ID);
        //    this.fpDeptItem_Sheet1.SetText(rowIndex, this.GetCloumn("��Ŀ����"), obj.Name);
        //    //
        //    this.fpDeptItem_Sheet1.SetText(rowIndex, this.GetCloumn("��������"), obj.Name);
        //    this.fpDeptItem_Sheet1.ActiveRowIndex = rowIndex;
        //}

        ///// <summary>
        ///// ��Ŀ�Ƿ��Ѿ�����
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <returns>����:true;  ����:false</returns>
        //private bool IsExist(FS.FrameWork.Models.NeuObject obj)
        //{
        //    for (int i = 0, j = this.fpDeptItem_Sheet1.Rows.Count; i < j; i++)
        //    {
        //        if (this.fpDeptItem_Sheet1.GetText(i, this.GetCloumn("��Ŀ���")).Trim().Equals(obj.ID) &&
        //            this.fpDeptItem_Sheet1.GetText(i, this.GetCloumn("��Ŀ����")).Trim().Equals(obj.Name))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        ///// <summary>
        ///// ˫������ؼ�ʱ���¼��������
        ///// </summary>
        ///// <param name="sender"></param>
        //private void ucAllItems_SelectedItem(FS.FrameWork.Models.NeuObject sender)
        //{
        //    if (IsExist(sender))
        //    {
        //        MessageBox.Show(FS.FrameWork.Management.Language.Msg("��Ŀ�Ѿ�����"));
        //        return;
        //    }

        //    this.CreateLine(sender);
        //}
        #endregion

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Export(object sender, object neuObject)
        {
            return this.fpDeptItem.Export(); ;
        }
    }

    internal enum FilterType
    {
        /// <summary>
        /// ҩƷ
        /// </summary>
        P,
        /// <summary>
        /// ��ҩƷ
        /// </summary>
        U,
        /// <summary>
        /// �����Ŀ
        /// </summary>
        C
    }
}