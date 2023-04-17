using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Pharmacy.Base
{
    /// <summary>
    /// [��������: ���ⵥλά��]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-04]<br></br>
    /// <˵��>
    ///     ������
    /// </˵��>
    /// </summary>
    public partial class ucSpeUnitManager : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucSpeUnitManager()
        {
            InitializeComponent();
        }

        public delegate void SaveDataHander(object data);

        public event SaveDataHander SaveDataEvent;

        private string drugInfo = "ҩƷ���ƣ�{0} ���{1} ��װ��λ��{2} ��װ������{3} ��С��λ��{4}";

        #region �����

        /// <summary>
        /// ���Ԫ��
        /// </summary>
        FarPoint.Win.Spread.CellType.ComboBoxCellType cmbTypeCellType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();

        /// <summary>
        /// ��λ��λ��
        /// </summary>
        FarPoint.Win.Spread.CellType.ComboBoxCellType cmbUnitCellType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();

        /// <summary>
        /// �洢��ά����ҩƷ
        /// </summary>
        private System.Collections.Hashtable hsItem = new Hashtable();

        /// <summary>
        /// ���β���ҩƷ��Ϣ
        /// </summary>
        private FS.HISFC.Models.Pharmacy.Item tempItem;

        /// <summary>
        /// ҩƷ������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// ҩƷ������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper drugDataHelper;

        /// <summary>
        /// ��������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper unitTypeHelper;

        /// <summary>
        /// ҩƷ��Ϣ
        /// </summary>
        private ArrayList alDrugData = new ArrayList();

        #endregion

        #region ����

        /// <summary>
        /// ���β���ҩƷ��Ϣ
        /// </summary>
        public FS.HISFC.Models.Pharmacy.Item Item
        {
            set
            {
                this.tempItem = value;
                if (value != null)
                {
                    this.lbDrugInfo.Text = string.Format(drugInfo, value.Name, value.Specs, value.PackUnit, value.PackQty.ToString(), value.MinUnit);
                    string[] strUnits = { value.MinUnit, value.PackUnit };

                    FarPoint.Win.Spread.CellType.ComboBoxCellType cmbBaseUnitCellType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                    cmbBaseUnitCellType.Items = strUnits;
                    //������λ ��С��λ/��װ��λ
                    this.neuSpread1_Sheet1.Columns[3].CellType = cmbBaseUnitCellType;
                }
                else
                {
                    this.lbDrugInfo.Text = this.drugInfo;
                    this.Clear();
                }
            }
        }

        /// <summary>
        /// �洢��ά����ҩƷ
        /// </summary>
        public System.Collections.Hashtable HsItem
        {
            get
            {
                return this.hsItem;
            }
            set
            {
                this.hsItem = value;
            }
        }

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void Init()
        {
            FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();

            #region �������

            ArrayList alUnitType = constMgr.GetList("SpeUnitType");
            if (alUnitType == null)
            {
                MessageBox.Show(Language.Msg("��ȡ������б�������" + constMgr.Err));
                return;
            }
            if (alUnitType.Count == 0)
            {
                #region �������� δ�ҵ�����ʱ��ʱʹ�ø�����

                FS.FrameWork.Models.NeuObject info1 = new FS.FrameWork.Models.NeuObject();
                info1.ID = "Clinic";
                info1.Name = "���﷢ҩ";
                alUnitType.Add(info1);

                FS.FrameWork.Models.NeuObject info2 = new FS.FrameWork.Models.NeuObject();
                info2.ID = "Inpatient";
                info2.Name = "סԺ��ҩ";
                alUnitType.Add(info2);

                FS.FrameWork.Models.NeuObject info3 = new FS.FrameWork.Models.NeuObject();
                info3.ID = "InOut";
                info3.Name = "���װ�����";
                alUnitType.Add(info3);

                #endregion
            }

            string[] strUnitType = new string[alUnitType.Count];
            int iUnit = 0;
            foreach (FS.FrameWork.Models.NeuObject unitType in alUnitType)
            {
                strUnitType[iUnit] = unitType.Name;
                iUnit++;
            }            

            this.unitTypeHelper = new FS.FrameWork.Public.ObjectHelper(alUnitType);

            this.cmbUnitCellType.Items = strUnitType;

            FarPoint.Win.Spread.CellType.ComboBoxCellType splitTypeCell = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            splitTypeCell.Items = strUnitType;

            this.neuSpread1_Sheet1.Columns[0].CellType = splitTypeCell;

            #endregion

            #region ��ȡ��λ����

            ArrayList alPackUnit = constMgr.GetList(FS.HISFC.Models.Base.EnumConstant.PACKUNIT);
            if (alPackUnit == null)
            {
                MessageBox.Show(Language.Msg("��ȡ��װ��λ��������" + constMgr.Err));
                return;
            }
            ArrayList alMinUnit = constMgr.GetList(FS.HISFC.Models.Base.EnumConstant.MINUNIT);
            if (alMinUnit == null)
            {
                MessageBox.Show(Language.Msg("��ȡ��С��λ��������" + constMgr.Err));
                return;
            }

            alPackUnit.AddRange(alMinUnit);

            string[] strUnit = new string[alPackUnit.Count];
            int i = 0;
            foreach (FS.HISFC.Models.Base.Const cons in alPackUnit)
            {
                strUnit[i] = cons.Name;
                i++;
            }

            this.cmbUnitCellType.Items = strUnit;
            //���ⵥλ
            this.neuSpread1_Sheet1.Columns[1].CellType = this.cmbUnitCellType;
            this.neuSpread1_Sheet1.Columns[3].CellType = this.cmbUnitCellType;

            #endregion

            #region ��ȡҩƷ��Ϣ

            List<FS.HISFC.Models.Pharmacy.Item> itemCollection = this.itemManager.QueryItemList(true);
            if (itemCollection == null)
            {
                MessageBox.Show(Language.Msg("��ȡҩƷ��Ϣ��������" + this.itemManager.Err));
                return;
            }

            this.alDrugData = new ArrayList(itemCollection.ToArray());

            foreach (FS.HISFC.Models.Pharmacy.Item info in alDrugData)
            {
                info.Memo = info.Specs;
            }

            this.drugDataHelper = new FS.FrameWork.Public.ObjectHelper(this.alDrugData);

            #endregion
        }

        #endregion

        /// <summary>
        /// ����
        /// </summary>
        private void Clear()
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;

                this.lbDrugInfo.Text = this.drugInfo;
        }

        /// <summary>
        /// ���ý���
        /// </summary>
        private void SetFocus()
        {
            if (this.neuSpread1_Sheet1.Rows.Count == 0)
            {
                this.neuSpread1_Sheet1.Rows.Count = 1;
            }
            
            this.neuSpread1_Sheet1.ActiveColumnIndex = 0;
        }

        /// <summary>
        /// �ر�
        /// </summary>
        private void Close()
        {
            if (this.ParentForm != null)
            {
                this.ParentForm.Close();
            }
        }

        /// <summary>
        /// ��Ч��
        /// </summary>
        /// <returns>������������True ����False</returns>
        private bool Valid()
        {
            if (this.tempItem == null)
            {
                MessageBox.Show(Language.Msg("������ά����ҩƷ"));
                return false;
            }
            FS.HISFC.Models.Pharmacy.DrugSpeUnit info;
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                info = this.GetSpeUnit(i);
                if (info == null)
                {
                    MessageBox.Show(Language.Msg("��ѡ�����"));
                    return false;
                }
                //{170D63B3-8252-4488-8675-B006283D9E41}
                if (string.IsNullOrEmpty(info.UnitType.Name))
                {
                    MessageBox.Show(Language.Msg("��ѡ�����"));
                    return false;
                }
                if (string.IsNullOrEmpty(info.Unit))
                {
                    MessageBox.Show(Language.Msg("��ѡ��λ"));
                    return false;
                }
                //{170D63B3-8252-4488-8675-B006283D9E41}
                if (info.Unit == this.tempItem.PackUnit || info.Unit == this.tempItem.MinUnit)
                {
                    MessageBox.Show(Language.Msg("���ⵥλ���ܵ��ڰ�װ��λ����С��λ"));
                    return false;
                }
                if (this.neuSpread1_Sheet1.Cells[i, 2].Text == "")
                {
                    MessageBox.Show(Language.Msg("��ά����������"));
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ����ҩƷ������Ϣ�������ⵥλ��Ϣ
        /// </summary>
        /// <param name="item">ҩƷ������Ϣ</param>
        public void SetSpeUnit(FS.HISFC.Models.Pharmacy.Item item)
        {
            this.Item = item;

            ArrayList al = this.itemManager.QuerySpeUnit(item.ID);
            if (al == null)
            {
                MessageBox.Show(Language.Msg("��ȡ��ά�������ⵥλ��Ϣ��������" + this.itemManager.Err));
                return;
            }
            if (al.Count > 0)
            {
                FS.HISFC.Models.Pharmacy.DrugSpeUnit info;
                this.neuSpread1_Sheet1.Rows.Count = al.Count;
                for (int i = 0; i < al.Count; i++)
                {
                    info = al[i] as FS.HISFC.Models.Pharmacy.DrugSpeUnit;

                    this.neuSpread1_Sheet1.Cells[i, 0].Text = info.UnitType.Name;
                    this.neuSpread1_Sheet1.Cells[i, 1].Text = info.Unit;

                    if (info.UnitFlag == "0")
                    {
                        this.neuSpread1_Sheet1.Cells[i, 3].Text = info.Item.MinUnit;
                        this.neuSpread1_Sheet1.Cells[i, 2].Text = info.Qty.ToString();
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells[i, 3].Text = info.Item.PackUnit;
                        this.neuSpread1_Sheet1.Cells[i, 2].Text = (info.Qty / info.Item.PackQty).ToString();
                    }

                    this.neuSpread1_Sheet1.Rows[i].Tag = info;
                }

            }

        }

        /// <summary>
        /// ����
        /// </summary>
        public void SaveSpeUnit()
        {
            if (!this.Valid())
            {
                return;
            }

            DateTime sysDate = this.itemManager.GetDateTimeFromSysDateTime();

            //��������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();
            this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (this.itemManager.DeleteSpeUnit(this.tempItem.ID) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg("ɾ����ҩƷ��ά�������ⵥλ��Ϣʱ��������" + this.itemManager.Err));
                return;
            }

            FS.HISFC.Models.Pharmacy.DrugSpeUnit info;
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                info = this.GetSpeUnit(i);

                decimal num = FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, 2].Text);
                string unit = this.neuSpread1_Sheet1.Cells[i, 3].Text;

                info.Item = this.tempItem;

                if (unit == this.tempItem.MinUnit)
                {
                    info.UnitFlag = "0";
                    info.Qty = num;
                }
                else
                {
                    info.UnitFlag = "1";
                    info.Qty = num * this.tempItem.PackQty;
                }

                info.Oper.ID = this.itemManager.Operator.ID;
                info.Oper.OperTime = sysDate;

                if (this.itemManager.InsertSpeUnit(info) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();

                    if (this.itemManager.DBErrCode == 1)
                    {
                        MessageBox.Show("�����ظ����� �����¼����ٽ��б������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(Language.Msg("����ʧ��" + this.itemManager.Err));
                    }
                    return;
                }
            }

            if (!this.hsItem.ContainsKey(this.tempItem.ID))
            {
                this.hsItem.Add(this.tempItem.ID, this.tempItem);
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            if (this.SaveDataEvent != null)
            {
                this.SaveDataEvent(this.tempItem);
            }

            MessageBox.Show(Language.Msg("����ɹ�"));

            if (!this.chkContinue.Checked)
            {
                this.Close();
            }

            this.Item = null;
        }

        /// <summary>
        /// �С�����ת
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        private int JumpColumn()
        {
            int i = this.neuSpread1_Sheet1.ActiveColumnIndex;

            if (this.neuSpread1_Sheet1.ActiveColumnIndex == 3)
            {
                if (this.neuSpread1_Sheet1.ActiveRowIndex == this.neuSpread1_Sheet1.Rows.Count - 1)
                {
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);
                    this.neuSpread1_Sheet1.ActiveRowIndex++;
                }
                else
                {
                    this.neuSpread1_Sheet1.ActiveRowIndex++;
                }

                this.neuSpread1_Sheet1.ActiveColumnIndex = 0;
            }
            else
            {
                this.neuSpread1_Sheet1.ActiveColumnIndex++;
            }
            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Add()
        {
            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);

            this.neuSpread1_Sheet1.ActiveRowIndex = this.neuSpread1_Sheet1.Rows.Count - 1;
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        private void Del()
        {
            int i = this.neuSpread1_Sheet1.ActiveRowIndex;

            FS.HISFC.Models.Pharmacy.DrugSpeUnit info;
            if (this.neuSpread1_Sheet1.Rows[i].Tag == null)
            {
                this.neuSpread1_Sheet1.Rows.Remove(i, 1);
            }
            else
            {
                info = this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.DrugSpeUnit;
                if (info == null)
                {
                    this.neuSpread1_Sheet1.Rows.Remove(i, 1);
                }

                else
                {
                    //��������
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                    //t.BeginTransaction();

                    this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                    if (this.itemManager.DeleteSpeUnit(info) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("ɾ��ʧ��"));
                        return;
                    }
                    this.neuSpread1_Sheet1.Rows.Remove(i, 1);

                    FS.FrameWork.Management.PublicTrans.Commit();
                }
            }
        }

        /// <summary>
        /// ����ҩƷ
        /// </summary>
        private void AddItem()
        {
            FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.alDrugData, ref info) == 0)
            {
                return;
            }
            else
            {
                FS.HISFC.Models.Pharmacy.Item item = info as FS.HISFC.Models.Pharmacy.Item;

                if (this.hsItem.ContainsKey(item.ID))
                {
                    MessageBox.Show(Language.Msg("��ҩƷ��ά�����༶��λ"));
                    return;
                }               

                this.Clear();

                this.Item = item;

                this.SetFocus();
            }
        }

        /// <summary>
        /// ��ȡ���ⵥλ��Ϣ
        /// </summary>
        /// <param name="iRowIndex">ָ��������</param>
        /// <returns></returns>
        private FS.HISFC.Models.Pharmacy.DrugSpeUnit GetSpeUnit(int iRowIndex)
        {
            FS.HISFC.Models.Pharmacy.DrugSpeUnit info = null;
            if (this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag != null)
            {
                info = this.neuSpread1_Sheet1.Rows[iRowIndex].Tag as FS.HISFC.Models.Pharmacy.DrugSpeUnit;
            }

            if (info == null)
            {
                info = new FS.HISFC.Models.Pharmacy.DrugSpeUnit();
            }
            //���
            info.UnitType.Name = this.neuSpread1_Sheet1.Cells[iRowIndex, 0].Text;       
            info.UnitType.ID = this.unitTypeHelper.GetID(info.UnitType.Name);
            //��λ
            info.Unit = this.neuSpread1_Sheet1.Cells[iRowIndex, 1].Text;
            //��������
            info.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[iRowIndex, 2].Text);

            return info;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveSpeUnit();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Add();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            this.Del();
        }

        private void lnbDrug_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.AddItem();
        }
    }
}
