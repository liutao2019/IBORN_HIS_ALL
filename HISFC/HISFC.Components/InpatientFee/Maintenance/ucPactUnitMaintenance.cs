using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace FS.HISFC.Components.InpatientFee.Maintenance
{
    public partial class ucPactUnitMaintenance : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucPactUnitMaintenance()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ��ͬ��λҵ���
        /// </summary>
        FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ��ͬ��λ��ϸ����ҵ���
        /// </summary>
        FS.HISFC.BizLogic.Fee.PactUnitItemRate pactUnitDetail = new FS.HISFC.BizLogic.Fee.PactUnitItemRate();

        /// <summary>
        /// ��ͬ��λ������Ϣ
        /// </summary>
        DataTable dtMain = new DataTable();

        /// <summary>
        /// ��ͬ��λ������Ϣ��ͼ
        /// </summary>
        DataView dvMain = new DataView();

        /// <summary>
        /// ��С����
        /// </summary>
        DataView dvFee = new DataView();
        DataTable dtFee = new DataTable();

        /// <summary>
        /// ��ϸ
        /// </summary>
        DataView dvDetail = new DataView();
        DataTable dtDetail = new DataTable();

        /// <summary>
        /// ��ͬ��λ������Ϣ����
        /// </summary>
        private string mainSettingFilePath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + @".\PactUnitMaintenance.xml";

        /// <summary>
        /// toolbarservice
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        /// <summary>
        /// ҽ������Dll·��
        /// </summary>
        protected string DllPath = Application.StartupPath + @"\Plugins\SI\";

        private bool isShowDllColumn = true;

        private FS.HISFC.BizProcess.Interface.FeeInterface.IValidPactItemChoose iValidPactItemChoose = null;
        #endregion

        #region  ����
        [Category("����"), Description("�Ƿ���ʾά��ҽ������DLL��")]
        public bool IsShowDllColumn
        {
            get
            {
                return isShowDllColumn;
            }
            set
            {
                isShowDllColumn = value;
            }
        }

        #endregion

        #region ˽�з���

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڳ�ʼ��,��ȴ�...");
            Application.DoEvents();
            //��ʼ����ͬ��λ��Ҫ��Ϣ
            if (InitDataTableMain() == -1)
            {
                return -1;
            }
            InitMainData();
            SetFrpColumnType();
            //��ʼ����С����
            InitDataTableFee();
            InitDetail();
            InitInterFace();
            //��ʼ����Ŀѡ���б�ucInputItem
            this.ucInputItem1.Init();
            this.ucInputItem1.SelectedItem += new FS.FrameWork.WinForms.Forms.SelectedItemHandler(ucInputItem1_SelectedItem);

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            return 1;
        }

        /// <summary>
        /// ��ʼ��ά���ĺ�ͬ��λ��Ϣ
        /// </summary>
        /// <returns>�ɹ�1 ʧ�� -1</returns>
        private int InitMainData()
        {
            ArrayList pactList = this.pactManager.QueryPactUnitAll();
            this.dtMain.Clear();
            foreach (FS.HISFC.Models.Base.PactInfo pactInfo in pactList)
            {
                DataRow row = this.dtMain.NewRow();

                row["��λ����"] = pactInfo.ID;
                row["��λ����"] = pactInfo.Name;
                row["�������"] = this.GetPayKindName(pactInfo.PayKind.ID);
                row["�۸���ʽ"] = pactInfo.PriceForm;

                row["ϵͳ���"] = pactInfo.PactSystemType;

                row["���ѱ���"] = pactInfo.Rate.PubRate;
                row["�Ը�����"] = pactInfo.Rate.PayRate;
                row["�Էѱ���"] = pactInfo.Rate.OwnRate;
                row["�Żݱ���"] = pactInfo.Rate.RebateRate;
                row["Ƿ�ѱ���"] = pactInfo.Rate.ArrearageRate;
                row["Ӥ����־"] = pactInfo.Rate.IsBabyShared;
                row["�Ƿ���"] = pactInfo.IsInControl;
                if (pactInfo.ItemType == "0")
                {
                    row["��־"] = "ȫ��";
                }
                else if (pactInfo.ItemType == "1")
                {
                    row["��־"] = "ҩƷ";
                }
                else if (pactInfo.ItemType == "2")
                {
                    row["��־"] = "��ҩƷ";
                }
                row["��ҽ��֤"] = pactInfo.IsNeedMCard;
                row["���޶�"] = pactInfo.DayQuota;
                row["���޶�"] = pactInfo.MonthQuota;
                row["���޶�"] = pactInfo.YearQuota;
                row["һ���޶�"] = pactInfo.OnceQuota;
                row["��λ����"] = pactInfo.BedQuota;
                row["�յ�����"] = pactInfo.AirConditionQuota;
                row["���"] = pactInfo.ShortName;
                row["���"] = pactInfo.SortID;
                row["�����㷨DLL"] = pactInfo.PactDllName;
                row["�����㷨DLL����"] = pactInfo.PactDllDescription;

                this.dtMain.Rows.Add(row);
            }

            this.dtMain.AcceptChanges();

            return 1;

        }

        /// <summary>
        /// ��ʼ����ͬ��λ��Ҫ��Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int InitDataTableMain()
        {
            if (File.Exists(this.mainSettingFilePath))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(this.mainSettingFilePath, this.dtMain, ref this.dvMain, this.fpMain_Sheet1);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpMain_Sheet1, this.mainSettingFilePath);
            }
            else
            {
                this.dtMain.Columns.AddRange(new DataColumn[] 
                {
                    new DataColumn("��λ����", typeof(string)),
                    new DataColumn("��λ����", typeof(string)),
                    new DataColumn("�������", typeof(string)),
                    new DataColumn("�۸���ʽ", typeof(string)),
                    new DataColumn("ϵͳ���", typeof(string)),
                    new DataColumn("���ѱ���", typeof(decimal)),
                    new DataColumn("�Ը�����", typeof(decimal)),
                    new DataColumn("�Էѱ���", typeof(decimal)),
                    new DataColumn("�Żݱ���", typeof(decimal)),
                    new DataColumn("Ƿ�ѱ���", typeof(decimal)),
                    new DataColumn("Ӥ����־", typeof(bool)),
                    new DataColumn("�Ƿ���", typeof(bool)),
                    new DataColumn("��־", typeof(string)),
                    new DataColumn("��ҽ��֤", typeof(bool)),
                    new DataColumn("���޶�", typeof(decimal)),
                    new DataColumn("���޶�", typeof(decimal)),
                    new DataColumn("���޶�", typeof(decimal)),
                    new DataColumn("һ���޶�", typeof(decimal)),
                    new DataColumn("��λ����", typeof(decimal)),
                    new DataColumn("�յ�����", typeof(decimal)),
                    new DataColumn("���", typeof(string)),
                    new DataColumn("���", typeof(int)),
                    new DataColumn("�����㷨DLL",typeof(string)),
                    new DataColumn("�����㷨DLL����",typeof(string))
                });

                this.dvMain = new DataView(this.dtMain);

                this.fpMain_Sheet1.DataSource = this.dvMain;

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpMain_Sheet1, this.mainSettingFilePath);
            }

            return 1;
        }

        /// <summary>
        /// ��ʼ����С����
        /// </summary>
        private void InitDataTableFee()
        {
            this.dtFee.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("��С���ô���",typeof(string)),
                    new DataColumn("��С��������",typeof(string))
                });
            this.dvFee = new DataView(this.dtFee);
            this.fpFeeCode_Sheet1.DataSource = this.dvFee;

            ArrayList feeCode = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.MINFEE);

            foreach (FS.HISFC.Models.Base.Const feeCodeCon in feeCode)
            {
                this.dtFee.Rows.Add(new object[]
                {
                    feeCodeCon.ID,
                    feeCodeCon.Name
                });
            }

        }

        /// <summary>
        /// ��ʼ����ϸ��
        /// </summary>
        private void InitDetail()
        {
            this.dtDetail.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("��λ����",typeof(string)),
                    new DataColumn("��Ŀ����",typeof(string)),
                    new DataColumn("��Ŀ����",typeof(string)),
                    new DataColumn("���",typeof(string)),
                    new DataColumn("���ѱ���",typeof(decimal)),
                    new DataColumn("�Էѱ���",typeof(decimal)),
                    new DataColumn("�Ը�����",typeof(decimal)),
                    new DataColumn("�Żݱ���",typeof(decimal)),//{65168F4D-B9D9-4386-BD0F-9DB780E74D60}
                    new DataColumn("Ƿ�ѱ���",typeof(decimal)),
                    new DataColumn("�޶�",typeof(decimal)),//add xf
                    new DataColumn("ƴ����",typeof(string)),
                    new DataColumn("�����",typeof(string)),
                    new DataColumn("������",typeof(string)),
                });
            this.dvDetail = new DataView(this.dtDetail);
            this.fpDetail_Sheet1.DataSource = this.dvDetail;
            //////this.fpDetail_Sheet1.Columns[9].Visible = false;
            //////this.fpDetail_Sheet1.Columns[10].Visible = false;
            //////this.fpDetail_Sheet1.Columns[11].Visible = false;
            this.fpDetail_Sheet1.Columns[10].Visible = false;
            this.fpDetail_Sheet1.Columns[11].Visible = false;
            this.fpDetail_Sheet1.Columns[12].Visible = false;
        }

        private void InitInterFace()
        {
            if (iValidPactItemChoose == null)
            {
                iValidPactItemChoose = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IValidPactItemChoose)) as FS.HISFC.BizProcess.Interface.FeeInterface.IValidPactItemChoose;
            }
        }

        /// <summary>
        /// �ӳ���������Ӻ�ͬ��λ
        /// </summary>
        private void AddPactUnitByCon()
        {

            #region ���ٴ������ֵ�ȡ�б� {16C790A2-6158-487b-8AC5-2F6B4683CAE4} xuc
            //ArrayList mpactList = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PACTUNIT);
            //DataTable dtMainCopy = this.dtMain;

            //foreach (FS.HISFC.Models.Base.Const pactCon in mpactList)
            //{
            //    string t = string.Format("��λ���� = '{0}'", pactCon.ID);
            //    DataRow[] rows = dtMainCopy.Select(t);
            //    if (rows == null || rows.GetLength(0) == 0)
            //    {
            //        this.dtMain.Rows.Add(new object[]
            //        {
            //            pactCon.ID,
            //            pactCon.Name,
            //            "",
            //            "Ĭ�ϼ�",
            //            0,
            //            0,
            //            0,
            //            0,
            //            0,
            //            false,
            //            false,
            //            "ȫ��",
            //            false,
            //            0,
            //            0,
            //            0,
            //            0,
            //            0,
            //            0,
            //            "",
            //            0
            //        });

            //    }
            //}
            #endregion
            DataTable dtMainCopy = this.dtMain;
            this.dtMain.Rows.Add(new object[]
                    {
                        "",
                        "",
                        "",
                        "Ĭ�ϼ�",
                        "ȫԺ",
                        0,
                        0,
                        0,
                        0,
                        0,
                        false,
                        false,
                        "ȫ��",
                        false,
                        0,
                        0,
                        0,
                        0,
                        0,
                        0,
                        "",
                        0
                    });

            //dtMainCopy.AcceptChanges();
            this.SetFrpColumnType();
            this.fpMain_Sheet1.Cells[this.dtMain.Rows.Count - 1, 0].Locked = false;
            this.fpMain_Sheet1.Cells[this.dtMain.Rows.Count - 1, 1].Locked = false;
            this.fpMain_Sheet1.SetActiveCell(this.dtMain.Rows.Count, 0);
            this.fpMain.ShowActiveCell(FarPoint.Win.Spread.VerticalPosition.Bottom, FarPoint.Win.Spread.HorizontalPosition.Left);
        }

        /// <summary>
        /// ��ѯ��ϸ����
        /// </summary>
        private void QueryPactUnitInfoDetail(string pactCode, int index)
        {
            ArrayList pactUnitDetailAList = this.pactUnitDetail.GetPactUnitItemRate(pactCode, index);

            this.dtDetail.Clear();

            foreach (FS.HISFC.Models.Base.PactItemRate pactItemRate in pactUnitDetailAList)
            {
                dtDetail.Rows.Add(new object[]
                {
                    pactItemRate.ID,
                    pactItemRate.PactItem.Name,
                    pactItemRate.PactItem.ID,
                    pactItemRate.ItemType,
                    pactItemRate.Rate.PubRate,
                    pactItemRate.Rate.OwnRate,
                    pactItemRate.Rate.PayRate,
                    pactItemRate.Rate.RebateRate,//{65168F4D-B9D9-4386-BD0F-9DB780E74D60}
                    pactItemRate.Rate.ArrearageRate,
                    pactItemRate.Rate.Quota,//add xf �޶�
                    pactItemRate.PactItem.User01,
                    pactItemRate.PactItem.User02,
                    pactItemRate.PactItem.User03
                });
            }
            this.dtDetail.AcceptChanges();
        }

        /// <summary>
        /// ͨ��paykindcode��ѯpaykindname
        /// </summary>
        /// <param name="payKindCode"></param>
        /// <returns></returns>
        private string GetPayKindName(string payKindCode)
        {
            ArrayList paykind = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYKIND);
            string payKindName = string.Empty;
            foreach (FS.HISFC.Models.Base.Const paykindCon in paykind)
            {
                if (paykindCon.ID.Trim() == payKindCode.Trim())
                {
                    payKindName = paykindCon.Name;
                    break;
                }
            }
            return payKindName;
        }

        /// <summary>
        /// ͨ��paykindname��ѯpaykindcode
        /// </summary>
        /// <param name="payKindName"></param>
        /// <returns></returns>
        private string GetPayKindCode(string payKindName)
        {
            ArrayList paykind = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYKIND);
            string payKindCode = string.Empty;
            foreach (FS.HISFC.Models.Base.Const paykindCon in paykind)
            {
                if (paykindCon.Name.Trim() == payKindName.Trim())
                {
                    payKindCode = paykindCon.ID;
                    break;
                }
            }
            return payKindCode;
        }

        /// <summary>
        /// ����farpoint�е���������
        /// </summary>
        protected virtual void SetFrpColumnType()
        {
            //�������
            FarPoint.Win.Spread.CellType.ComboBoxCellType cbxPayKind = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            ArrayList paykind = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYKIND);
            string[] paykindname = new string[paykind.Count];
            int i = 0;
            foreach (FS.HISFC.Models.Base.Const paykindCon in paykind)
            {
                paykindname[i] = paykindCon.Name;

                ++i;
            }
            cbxPayKind.Items = paykindname;
            this.fpMain_Sheet1.Columns[2].CellType = cbxPayKind;
            //�۸���ʽ
            FarPoint.Win.Spread.CellType.ComboBoxCellType cbxPriceForm = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            //{B9303CFE-755D-4585-B5EE-8C1901F79450}maokb���ӹ����
            cbxPriceForm.Items = new string[] { "Ĭ�ϼ�", "�����", "��ͯ��" ,"�����"};
            this.fpMain_Sheet1.Columns[3].CellType = cbxPriceForm;

            // ��ͬ��λϵͳ���
            FarPoint.Win.Spread.CellType.ComboBoxCellType cbxPactSystemType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            cbxPactSystemType.Items = new string[] { "ȫԺ", "����", "סԺ", "ϵͳ" };
            this.fpMain_Sheet1.Columns[4].CellType = cbxPactSystemType;

            //��־
            FarPoint.Win.Spread.CellType.ComboBoxCellType cbxItemType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            cbxItemType.Items = new string[] { "ȫ��", "ҩƷ", "��ҩƷ" };
            this.fpMain_Sheet1.Columns[12].CellType = cbxItemType;
            //�趨ֻ�е������ڵ�ǰ��Ԫ���ϵ�ʱ�������ʾ�����б�
            this.fpMain.ButtonDrawMode = FarPoint.Win.Spread.ButtonDrawModes.CurrentCell;
            this.fpMain_Sheet1.Columns[0].Locked = true;
            this.fpMain_Sheet1.Columns[1].Locked = true;
            this.fpMain_Sheet1.Columns[21].Visible = false;

            #region �㷨Dll
            //�㷨Dll
            string[] dllitems = this.GetDllName();
            FarPoint.Win.Spread.CellType.ComboBoxCellType cbxDllName = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            if (dllitems != null)
                cbxDllName.Items = dllitems;
            this.fpMain_Sheet1.Columns[22].CellType = cbxDllName;
            this.fpMain_Sheet1.Columns[23].Locked = true;
            if (!isShowDllColumn)
            {
                this.fpMain_Sheet1.Columns[22].Visible = false;
                this.fpMain_Sheet1.Columns[23].Visible = false;
            }
            #endregion
        }

        #region ����ҽ������DLL ·־�� 2007-7-6

        /// <summary>
        /// ͨ�������������ҽ������DLL
        /// </summary>
        /// <returns></returns>
        protected virtual string[] GetDllName()
        {
            string[] sPath = Directory.GetFiles(DllPath);
            if (sPath.Length == 0)
                return null;
            List<string> list = new List<string>();
            foreach (string path in sPath)
            {
                FileInfo fi = new FileInfo(path);
                if (fi.Extension.ToLower() == ".dll")
                {
                    FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare im = GetDllInterface(path);
                    if (im == null)
                        continue;
                    list.Add(fi.Name);
                }
            }
            if (list.Count == 0)
                return null;
            return list.ToArray();
        }

        /// <summary>
        /// ����Dll����dll����
        /// </summary>
        /// <param name="path">dll·��</param>
        /// <returns></returns>
        protected virtual FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare GetDllInterface(string path)
        {
            try
            {
                System.Reflection.Assembly assmbly = System.Reflection.Assembly.LoadFile(path);
                Type[] t = assmbly.GetTypes();
                if (t == null)
                    return null;
                foreach (Type type in t)
                {
                    if (type.GetInterface("IMedcare") != null)
                    {
                        return (FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare)System.Activator.CreateInstance(type);
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        #endregion


        /// <summary>
        /// У���ͬ��λ�Ƿ�仯
        /// </summary>
        /// <returns></returns>
        private bool IsMainDataChange()
        {
            DataTable dtMainAdd = this.dtMain.GetChanges(System.Data.DataRowState.Added);
            DataTable dtMainMod = this.dtMain.GetChanges(System.Data.DataRowState.Modified);
            if (dtMainAdd != null || dtMainMod != null)
            {
                DialogResult result;
                result = MessageBox.Show("�Ƿ񱣴�ղŸĶ��������ݣ�", "����", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    return true;
                }
                else
                {
                    this.InitMainData();
                    this.SetFrpColumnType();
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// У����ϸ�����Ƿ��б仯
        /// </summary>
        private bool IsDetailDataChange()
        {
            DataTable dtDetailAdd = this.dtDetail.GetChanges(System.Data.DataRowState.Added);
            DataTable dtDetailDel = this.dtDetail.GetChanges(System.Data.DataRowState.Deleted);
            DataTable dtDetailMod = this.dtDetail.GetChanges(System.Data.DataRowState.Modified);

            if (dtDetailAdd != null || dtDetailDel != null || dtDetailMod != null)
            {
                DialogResult result;
                result = MessageBox.Show("�Ƿ񱣴�ղŸĶ��������ݣ�", "����", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ���ѡ�е������Ƿ��Ѿ�ά��
        /// </summary>
        /// <returns></returns>
        private bool IsExistData(string itemCode)
        {
            int rowCount = 0;
            rowCount = this.fpDetail_Sheet1.RowCount;
            bool isExist = true;
            if (rowCount > 0)
            {
                string tmpCode = string.Empty;
                for (int i = 0; i < rowCount; i++)
                {
                    tmpCode = fpDetail_Sheet1.GetText(i, 2).ToString().Trim();
                    if (tmpCode == itemCode)
                    {
                        isExist = true;
                        break;
                    }
                    else
                    {
                        isExist = false;
                    }
                }
            }
            else
            {
                isExist = false;
            }
            return isExist;
        }

        /// <summary>
        /// ɾ����ϸ����
        /// </summary>
        private void DeleteDetail()
        {
            DialogResult result;
            //ɾ����ͬ��λά����Ϣ {16C790A2-6158-487b-8AC5-2F6B4683CAE4} xuc
            result = MessageBox.Show("�Ƿ�Ҫɾ����һ������,ɾ����ͬ��λ��Ϣͬʱ��ɾ��ά������ϸ��Ϣ��", "��ʾ", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (this.neuTabControl1.SelectedIndex == 1)
                {
                    if (this.fpDetail_Sheet1.Rows.Count > 0)
                    {
                        this.fpDetail_Sheet1.Rows.Remove(this.fpDetail_Sheet1.ActiveRowIndex, 1);
                    }
                }
                else
                {
                    //ɾ����ͬ��λά����Ϣ {16C790A2-6158-487b-8AC5-2F6B4683CAE4} xuc
                    if (this.fpMain_Sheet1.Rows.Count > 0)
                    {
                        this.fpMain_Sheet1.Rows.Remove(this.fpMain_Sheet1.ActiveRowIndex, 1);
                    }

                    DataTable dtDeletePact = this.dtMain.GetChanges(System.Data.DataRowState.Deleted);
                    if (dtDeletePact != null)
                    {
                        dtDeletePact.RejectChanges();

                        //����
                        FS.FrameWork.Management.PublicTrans.BeginTransaction();
                        this.pactUnitDetail.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        this.pactManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                        foreach (DataRow row in dtDeletePact.Rows)
                        {
                            FS.HISFC.Models.Base.PactInfo pactInfo = new FS.HISFC.Models.Base.PactInfo();
                            #region
                            pactInfo.ID = row["��λ����"].ToString();
                            pactInfo.Name = row["��λ����"].ToString();
                            pactInfo.PayKind.ID = row["�������"].ToString();
                            pactInfo.PriceForm = row["�۸���ʽ"].ToString();
                            pactInfo.Rate.PubRate = FS.FrameWork.Function.NConvert.ToDecimal(row["���ѱ���"].ToString());
                            pactInfo.Rate.PayRate = FS.FrameWork.Function.NConvert.ToDecimal(row["�Ը�����"].ToString());
                            pactInfo.Rate.OwnRate = FS.FrameWork.Function.NConvert.ToDecimal(row["�Էѱ���"].ToString());
                            pactInfo.Rate.RebateRate = FS.FrameWork.Function.NConvert.ToDecimal(row["�Żݱ���"].ToString());
                            pactInfo.Rate.ArrearageRate = FS.FrameWork.Function.NConvert.ToDecimal(row["Ƿ�ѱ���"].ToString());
                            pactInfo.Rate.IsBabyShared = (bool)row["Ӥ����־"];
                            pactInfo.IsInControl = (bool)row["�Ƿ���"];
                            if (row["��־"].ToString() == "ȫ��")
                            {
                                pactInfo.ItemType = "0";
                            }
                            else if (row["��־"].ToString() == "ҩƷ")
                            {
                                pactInfo.ItemType = "1";
                            }
                            else if (row["��־"].ToString() == "��ҩƷ")
                            {
                                pactInfo.ItemType = "2";
                            }
                            pactInfo.IsNeedMCard = (bool)row["��ҽ��֤"];
                            pactInfo.DayQuota = FS.FrameWork.Function.NConvert.ToDecimal(row["���޶�"].ToString());
                            pactInfo.MonthQuota = FS.FrameWork.Function.NConvert.ToDecimal(row["���޶�"].ToString());
                            pactInfo.YearQuota = FS.FrameWork.Function.NConvert.ToDecimal(row["���޶�"].ToString());
                            pactInfo.OnceQuota = FS.FrameWork.Function.NConvert.ToDecimal(row["һ���޶�"].ToString());
                            pactInfo.BedQuota = FS.FrameWork.Function.NConvert.ToDecimal(row["��λ����"].ToString());
                            pactInfo.AirConditionQuota = FS.FrameWork.Function.NConvert.ToDecimal(row["�յ�����"].ToString());
                            pactInfo.ShortName = row["���"].ToString();
                            pactInfo.SortID = FS.FrameWork.Function.NConvert.ToInt32(row["���"].ToString());
                            pactInfo.PactDllName = row["�����㷨DLL"].ToString();
                            pactInfo.PactDllDescription = row["�����㷨DLL����"].ToString();
                            #endregion
                            if (this.pactManager.DeletePactUnitInfo(pactInfo) <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("ɾ����ͬ��λ��Ϣ����\n" + this.pactManager.Err, "��ʾ");
                                InitMainData();
                                return;
                            }

                            ArrayList pactUnitDetailAListFee = this.pactUnitDetail.GetPactUnitItemRate(pactInfo.ID, 0);
                            ArrayList pactUnitDetailAListItem = this.pactUnitDetail.GetPactUnitItemRate(pactInfo.ID, 1);

                            if (pactUnitDetailAListFee != null)
                            {
                                if (pactUnitDetailAListItem != null)
                                {
                                    pactUnitDetailAListFee.AddRange(pactUnitDetailAListItem.ToArray());
                                }
                                foreach (FS.HISFC.Models.Base.PactItemRate pactItemRate in pactUnitDetailAListFee)
                                {
                                    if (this.pactUnitDetail.DeletePactUnitItemRate(pactItemRate) <= 0)
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        MessageBox.Show("ɾ����ͬ��λ��ϸ��Ϣ����\n" + this.pactUnitDetail.Err, "��ʾ");
                                        InitMainData();
                                        return;
                                    }
                                }
                            }
                        }

                        this.dtMain.AcceptChanges();
                        FS.FrameWork.Management.PublicTrans.Commit();
                        MessageBox.Show("ɾ����ͬ��λ��Ϣ�ɹ���", "��ʾ");
                        InitMainData();
                    }



                }
            }
        }

        /// <summary>
        /// ucinputitem�ؼ����ص�item��ӵ���ϸ�б���
        /// </summary>
        /// <param name="sender"></param>
        void ucInputItem1_SelectedItem(FS.FrameWork.Models.NeuObject sender)
        {
            FS.HISFC.Models.Base.Item item = sender as FS.HISFC.Models.Base.Item;
            string pactCode = string.Empty;
            pactCode = this.fpMain_Sheet1.GetText(this.fpMain_Sheet1.ActiveRowIndex, 0).ToString().Trim();

            //��ͬ��λ�Ż���Ŀά����֤
            if (iValidPactItemChoose != null)
            {
                string errText = string.Empty;
                bool bValue = iValidPactItemChoose.ValidPactItemChoose(pactCode, item, ref errText);
                if (!bValue)
                {
                    MessageBox.Show(errText);
                    return;
                }
            }

            if (this.IsExistData(item.ID))
            {
                MessageBox.Show("����Ŀ������ϸ�б���", "��ʾ");
                return;
            }
            else
            {
                if (item.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))
                {
                    dtDetail.Rows.Add(new object[]
                    {
                        pactCode,
                        item.Name,
                        item.ID,
                        "1",
                        0,
                        0,
                        0,
                        0,
                        0,
                        0,//add xf �޶�
                        item.SpellCode,
                        item.WBCode,
                        item.UserCode
                    });
                }
                else
                {
                    dtDetail.Rows.Add(new object[]
                    {
                        pactCode,
                        item.Name,
                        item.ID,
                        "2",
                        0,
                        0,
                        0,
                        0,
                        0,
                        0,//add xf �޶�
                        item.SpellCode,
                        item.WBCode,
                        item.UserCode
                    });
                }
            }
        }

        /// <summary>
        /// �����ͬ��λ��ϸ
        /// </summary>
        private void SavePactUnitDetail()
        {
            //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.pactUnitDetail.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            int sqlResult = 0;
            string errorText = string.Empty;
            this.fpDetail.StopCellEditing();
            //insert
            DataTable dtDetailAdd = this.dtDetail.GetChanges(System.Data.DataRowState.Added);

            if (dtDetailAdd != null)
            {
                foreach (DataRow rowAdd in dtDetailAdd.Rows)
                {
                    FS.HISFC.Models.Base.PactItemRate pactItemRate = new FS.HISFC.Models.Base.PactItemRate();

                    pactItemRate.ID = rowAdd["��λ����"].ToString().Trim();
                    pactItemRate.PactItem.Name = rowAdd["��Ŀ����"].ToString().Trim();
                    pactItemRate.PactItem.ID = rowAdd["��Ŀ����"].ToString().Trim();
                    pactItemRate.ItemType = rowAdd["���"].ToString().Trim();
                    pactItemRate.Rate.PubRate = Convert.ToDecimal(rowAdd["���ѱ���"].ToString().Trim());
                    pactItemRate.Rate.OwnRate = Convert.ToDecimal(rowAdd["�Էѱ���"].ToString().Trim());
                    pactItemRate.Rate.PayRate = Convert.ToDecimal(rowAdd["�Ը�����"].ToString().Trim());
                    pactItemRate.Rate.RebateRate = Convert.ToDecimal(rowAdd["�Żݱ���"].ToString().Trim());//{65168F4D-B9D9-4386-BD0F-9DB780E74D60}
                    pactItemRate.Rate.ArrearageRate = Convert.ToDecimal(rowAdd["Ƿ�ѱ���"].ToString().Trim());
                    pactItemRate.Rate.Quota = Convert.ToDecimal(rowAdd["�޶�"].ToString().Trim());//add xf �޶�


                    rowAdd.AcceptChanges();
                    try
                    {
                        sqlResult = this.pactUnitDetail.InsertPactUnitItemRate(pactItemRate);
                        if (sqlResult == -1)
                        {
                            break;
                        }
                    }
                    catch (Exception ee)
                    {
                        errorText = ee.Message;
                    }
                    pactItemRate = null;
                }
            }
            //delete
            DataTable dtDetailDel = this.dtDetail.GetChanges(System.Data.DataRowState.Deleted);

            if (dtDetailDel != null)
            {
                dtDetailDel.RejectChanges();
                foreach (DataRow rowDel in dtDetailDel.Rows)
                {
                    FS.HISFC.Models.Base.PactItemRate pactItemRate = new FS.HISFC.Models.Base.PactItemRate();

                    pactItemRate.ID = rowDel["��λ����"].ToString().Trim();
                    pactItemRate.PactItem.Name = rowDel["��Ŀ����"].ToString().Trim();
                    pactItemRate.PactItem.ID = rowDel["��Ŀ����"].ToString().Trim();
                    pactItemRate.ItemType = rowDel["���"].ToString().Trim();
                    pactItemRate.Rate.PubRate = Convert.ToDecimal(rowDel["���ѱ���"].ToString().Trim());
                    pactItemRate.Rate.OwnRate = Convert.ToDecimal(rowDel["�Էѱ���"].ToString().Trim());
                    pactItemRate.Rate.PayRate = Convert.ToDecimal(rowDel["�Ը�����"].ToString().Trim());
                    pactItemRate.Rate.RebateRate = Convert.ToDecimal(rowDel["�Żݱ���"].ToString().Trim());//{65168F4D-B9D9-4386-BD0F-9DB780E74D60}
                    pactItemRate.Rate.ArrearageRate = Convert.ToDecimal(rowDel["Ƿ�ѱ���"].ToString().Trim());
                    pactItemRate.Rate.Quota = Convert.ToDecimal(rowDel["�޶�"].ToString().Trim());//add xf �޶�

                    rowDel.AcceptChanges();
                    try
                    {
                        sqlResult = this.pactUnitDetail.DeletePactUnitItemRate(pactItemRate);
                        if (sqlResult == -1)
                        {
                            break;
                        }
                    }
                    catch (Exception ee)
                    {
                        errorText = ee.Message;
                    }
                    pactItemRate = null;
                }
            }
            #region ���θ��£���farpointд
            //#region//update
            //DataTable dtDetailMod = this.dtDetail.GetChanges(System.Data.DataRowState.Modified);
            //if (dtDetailMod != null)
            //{
            //    foreach (DataRow rowMod in dtDetailMod.Rows)
            //    {
            //        FS.HISFC.Models.Base.PactItemRate pactItemRate = new FS.HISFC.Models.Base.PactItemRate();

            //        pactItemRate.ID = rowMod["��λ����"].ToString().Trim();
            //        pactItemRate.PactItem.Name = rowMod["����/��Ŀ����"].ToString().Trim();
            //        pactItemRate.PactItem.ID = rowMod["���ô���"].ToString().Trim();
            //        pactItemRate.ItemType = rowMod["���"].ToString().Trim();
            //        pactItemRate.Rate.PubRate = Convert.ToDecimal(rowMod["���ѱ���"].ToString().Trim());
            //        pactItemRate.Rate.OwnRate = Convert.ToDecimal(rowMod["�Էѱ���"].ToString().Trim());
            //        pactItemRate.Rate.PayRate = Convert.ToDecimal(rowMod["�Ը�����"].ToString().Trim());
            //        pactItemRate.Rate.DerateRate = Convert.ToDecimal(rowMod["�������"].ToString().Trim());
            //        pactItemRate.Rate.ArrearageRate = Convert.ToDecimal(rowMod["Ƿ�ѱ���"].ToString().Trim());

            //        rowMod.AcceptChanges();
            //        try
            //        {
            //            sqlResult = this.pactUnitDetail.UpdatePactUnitItemRate(pactItemRate);
            //            if (sqlResult == -1)
            //            {
            //                break;
            //            }
            //        }
            //        catch (Exception ee)
            //        {
            //            errorText = ee.Message;
            //        }
            //        pactItemRate = null;
            //    }
            //}
            //#endregion
            #endregion

            #region ��farpointʵ�ָ���
            FS.HISFC.Models.Base.PactItemRate pactItemRateUpdate = new FS.HISFC.Models.Base.PactItemRate();
            for (int i = 0; i < this.fpDetail_Sheet1.Rows.Count; i++)
            {
                pactItemRateUpdate.ID = this.fpDetail_Sheet1.Cells[i, 0].Value.ToString().Trim();//��λ����
                pactItemRateUpdate.PactItem.Name = this.fpDetail_Sheet1.Cells[i, 1].Value.ToString().Trim();//����/��Ŀ����
                pactItemRateUpdate.PactItem.ID = this.fpDetail_Sheet1.Cells[i, 2].Value.ToString().Trim();//���ô���
                pactItemRateUpdate.ItemType = this.fpDetail_Sheet1.Cells[i, 3].Value.ToString().Trim();//���
                pactItemRateUpdate.Rate.PubRate = Convert.ToDecimal(this.fpDetail_Sheet1.Cells[i, 4].Value.ToString().Trim());//���ѱ���
                pactItemRateUpdate.Rate.OwnRate = Convert.ToDecimal(this.fpDetail_Sheet1.Cells[i, 5].Value.ToString().Trim());//�Էѱ���
                pactItemRateUpdate.Rate.PayRate = Convert.ToDecimal(this.fpDetail_Sheet1.Cells[i, 6].Value.ToString().Trim());//�Ը�����
                pactItemRateUpdate.Rate.RebateRate = Convert.ToDecimal(this.fpDetail_Sheet1.Cells[i, 7].Value.ToString().Trim());//�Żݱ���{65168F4D-B9D9-4386-BD0F-9DB780E74D60}
                pactItemRateUpdate.Rate.ArrearageRate = Convert.ToDecimal(this.fpDetail_Sheet1.Cells[i, 8].Value.ToString().Trim());//Ƿ�ѱ���
                pactItemRateUpdate.Rate.Quota = Convert.ToDecimal(this.fpDetail_Sheet1.Cells[i, 9].Value.ToString().Trim());//add xf �޶�
                try
                {
                    sqlResult = this.pactUnitDetail.UpdatePactUnitItemRate(pactItemRateUpdate);
                    if (sqlResult == 0)
                    {
                        sqlResult = this.pactUnitDetail.InsertPactUnitItemRate(pactItemRateUpdate);
                        if (sqlResult == -1)
                        {
                            break;
                        }
                    }
                }
                catch (Exception ee)
                {
                    errorText = ee.Message;
                }
            }
            #endregion
            if (sqlResult == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("��������ʧ�ܣ�" + errorText);
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.Commit();
                this.dtDetail.AcceptChanges();
                MessageBox.Show("����ɹ���");
            }
        }

        private bool Valid()
        {
            for (int i = 0; i < this.fpMain_Sheet1.Rows.Count; i++)
            {
                if (this.fpMain_Sheet1.Cells[i, 2].Text.Trim() == string.Empty)
                {
                    MessageBox.Show("��" + (i + 1).ToString() + "���������Ϊ�գ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// �����ͬ��λ
        /// </summary>
        private void SavePactUnit()
        {
            if (!Valid())
                return;
            //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.pactManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            int sqlResult = 0;
            string errorText = string.Empty;
            this.fpMain.StopCellEditing();
            for (int i = 0; i < dtMain.Rows.Count; i++)
            {
                this.dtMain.Rows[i].EndEdit();
            }
            //insert
            DataTable dtMainAdd = this.dtMain.GetChanges(System.Data.DataRowState.Added);
            if (dtMainAdd != null)
            {
                foreach (DataRow rowAdd in dtMainAdd.Rows)
                {
                    FS.HISFC.Models.Base.PactInfo pactInfoAdd = new FS.HISFC.Models.Base.PactInfo();

                    pactInfoAdd.ID = rowAdd["��λ����"].ToString().Trim();
                    pactInfoAdd.Name = rowAdd["��λ����"].ToString().Trim();
                    pactInfoAdd.PayKind.ID = this.GetPayKindCode(rowAdd["�������"].ToString().Trim());
                    if (rowAdd["�۸���ʽ"].ToString().Trim() == "Ĭ�ϼ�")
                    {
                        pactInfoAdd.PriceForm = "0";
                    }
                    else if (rowAdd["�۸���ʽ"].ToString().Trim() == "�����")
                    {
                        pactInfoAdd.PriceForm = "1";
                    }
                    else if (rowAdd["�۸���ʽ"].ToString().Trim() == "��ͯ��")
                    {
                        pactInfoAdd.PriceForm = "2";
                    }
                    //{B9303CFE-755D-4585-B5EE-8C1901F79450}maokb���ӹ����
                    else if (rowAdd["�۸���ʽ"].ToString().Trim() == "�����")
                    {
                        pactInfoAdd.PriceForm = "3";
                    }
                    else
                    {
                        pactInfoAdd.PriceForm = "0";
                    }

                    switch (rowAdd["ϵͳ���"].ToString().Trim())
                    {
                        case "����":
                            pactInfoAdd.PactSystemType = "1";
                            break;

                        case "סԺ":
                            pactInfoAdd.PactSystemType = "2";
                            break;

                        case "ϵͳ":
                            pactInfoAdd.PactSystemType = "3";
                            break;

                        default:
                            pactInfoAdd.PactSystemType = "0";
                            break;
                    }


                    //pactInfoAdd.PriceForm = rowAdd["�۸���ʽ"].ToString().Trim();
                    pactInfoAdd.Rate.PubRate = Convert.ToDecimal(rowAdd["���ѱ���"].ToString().Trim());
                    pactInfoAdd.Rate.PayRate = Convert.ToDecimal(rowAdd["�Ը�����"].ToString().Trim());
                    pactInfoAdd.Rate.OwnRate = Convert.ToDecimal(rowAdd["�Էѱ���"].ToString().Trim());
                    pactInfoAdd.Rate.RebateRate = Convert.ToDecimal(rowAdd["�Żݱ���"].ToString().Trim());
                    pactInfoAdd.Rate.ArrearageRate = Convert.ToDecimal(rowAdd["Ƿ�ѱ���"].ToString().Trim());
                    pactInfoAdd.Rate.IsBabyShared = Convert.ToBoolean(rowAdd["Ӥ����־"]);
                    pactInfoAdd.IsInControl = Convert.ToBoolean(rowAdd["�Ƿ���"]);
                    if (rowAdd["��־"].ToString().Trim() == "ȫ��")
                    {
                        pactInfoAdd.ItemType = "0";
                    }
                    else if (rowAdd["��־"].ToString().Trim() == "ҩƷ")
                    {
                        pactInfoAdd.ItemType = "1";
                    }
                    else if (rowAdd["��־"].ToString().Trim() == "��ҩƷ")
                    {
                        pactInfoAdd.ItemType = "2";
                    }
                    else
                    {
                        pactInfoAdd.ItemType = "0";
                    }
                    pactInfoAdd.IsNeedMCard = Convert.ToBoolean(rowAdd["��ҽ��֤"]);
                    pactInfoAdd.DayQuota = Convert.ToDecimal(rowAdd["���޶�"].ToString().Trim());
                    pactInfoAdd.MonthQuota = Convert.ToDecimal(rowAdd["���޶�"].ToString().Trim());
                    pactInfoAdd.YearQuota = Convert.ToDecimal(rowAdd["���޶�"].ToString().Trim());
                    pactInfoAdd.OnceQuota = Convert.ToDecimal(rowAdd["һ���޶�"].ToString().Trim());
                    pactInfoAdd.BedQuota = Convert.ToDecimal(rowAdd["��λ����"].ToString().Trim());
                    pactInfoAdd.AirConditionQuota = Convert.ToDecimal(rowAdd["�յ�����"].ToString().Trim());
                    pactInfoAdd.ShortName = rowAdd["���"].ToString().Trim();
                    pactInfoAdd.SortID = Convert.ToInt32(rowAdd["���"].ToString().Trim());
                    pactInfoAdd.PactDllName = rowAdd["�����㷨DLL"].ToString().Trim();
                    pactInfoAdd.PactDllDescription = rowAdd["�����㷨DLL����"].ToString().Trim();

                    rowAdd.AcceptChanges();
                    try
                    {
                        sqlResult = this.pactManager.InsertPactUnitInfo(pactInfoAdd);
                        if (sqlResult == -1)
                        {
                            break;
                        }
                    }
                    catch (Exception ee)
                    {
                        errorText = ee.Message;
                    }
                    pactInfoAdd = null;
                }
            }
            //update
            DataTable dtMainMod = this.dtMain.GetChanges(System.Data.DataRowState.Modified);
            if (dtMainMod != null)
            {
                foreach (DataRow rowMod in dtMainMod.Rows)
                {
                    FS.HISFC.Models.Base.PactInfo pactInfoMod = new FS.HISFC.Models.Base.PactInfo();

                    pactInfoMod.ID = rowMod["��λ����"].ToString().Trim();
                    pactInfoMod.Name = rowMod["��λ����"].ToString().Trim();
                    pactInfoMod.PayKind.ID = this.GetPayKindCode(rowMod["�������"].ToString().Trim());
                    if (rowMod["�۸���ʽ"].ToString().Trim() == "Ĭ�ϼ�")
                    {
                        pactInfoMod.PriceForm = "0";
                    }
                    else if (rowMod["�۸���ʽ"].ToString().Trim() == "�����")
                    {
                        pactInfoMod.PriceForm = "1";
                    }
                    else if (rowMod["�۸���ʽ"].ToString().Trim() == "��ͯ��")
                    {
                        pactInfoMod.PriceForm = "2";
                    }
                    //{B9303CFE-755D-4585-B5EE-8C1901F79450}maokb���ӹ����
                    else if(rowMod["�۸���ʽ"].ToString().Trim() == "�����")
                    {
                        pactInfoMod.PriceForm = "3";
                    }
                   else
                    {
                        pactInfoMod.PriceForm = "0";
                    }

                    switch (rowMod["ϵͳ���"].ToString().Trim())
                    {
                        case "����":
                            pactInfoMod.PactSystemType = "1";
                            break;

                        case "סԺ":
                            pactInfoMod.PactSystemType = "2";
                            break;

                        case "ϵͳ":
                            pactInfoMod.PactSystemType = "3";
                            break;

                        default:
                            pactInfoMod.PactSystemType = "0";
                            break;
                    }


                    //pactInfoMod.PriceForm = rowMod["�۸���ʽ"].ToString().Trim();
                    pactInfoMod.Rate.PubRate = Convert.ToDecimal(rowMod["���ѱ���"].ToString().Trim());
                    pactInfoMod.Rate.PayRate = Convert.ToDecimal(rowMod["�Ը�����"].ToString().Trim());
                    pactInfoMod.Rate.OwnRate = Convert.ToDecimal(rowMod["�Էѱ���"].ToString().Trim());
                    pactInfoMod.Rate.RebateRate = Convert.ToDecimal(rowMod["�Żݱ���"].ToString().Trim());
                    pactInfoMod.Rate.ArrearageRate = Convert.ToDecimal(rowMod["Ƿ�ѱ���"].ToString().Trim());
                    pactInfoMod.Rate.IsBabyShared = Convert.ToBoolean(rowMod["Ӥ����־"]);
                    pactInfoMod.IsInControl = Convert.ToBoolean(rowMod["�Ƿ���"]);
                    if (rowMod["��־"].ToString().Trim() == "ȫ��")
                    {
                        pactInfoMod.ItemType = "0";
                    }
                    else if (rowMod["��־"].ToString().Trim() == "ҩƷ")
                    {
                        pactInfoMod.ItemType = "1";
                    }
                    else if (rowMod["��־"].ToString().Trim() == "��ҩƷ")
                    {
                        pactInfoMod.ItemType = "2";
                    }
                    else
                    {
                        pactInfoMod.ItemType = "0";
                    }
                    pactInfoMod.IsNeedMCard = Convert.ToBoolean(rowMod["��ҽ��֤"]);
                    pactInfoMod.DayQuota = Convert.ToDecimal(rowMod["���޶�"].ToString().Trim());
                    pactInfoMod.MonthQuota = Convert.ToDecimal(rowMod["���޶�"].ToString().Trim());
                    pactInfoMod.YearQuota = Convert.ToDecimal(rowMod["���޶�"].ToString().Trim());
                    pactInfoMod.OnceQuota = Convert.ToDecimal(rowMod["һ���޶�"].ToString().Trim());
                    pactInfoMod.BedQuota = Convert.ToDecimal(rowMod["��λ����"].ToString().Trim());
                    pactInfoMod.AirConditionQuota = Convert.ToDecimal(rowMod["�յ�����"].ToString().Trim());
                    pactInfoMod.ShortName = rowMod["���"].ToString().Trim();
                    pactInfoMod.SortID = Convert.ToInt32(rowMod["���"].ToString().Trim());
                    pactInfoMod.PactDllName = rowMod["�����㷨DLL"].ToString().Trim();
                    pactInfoMod.PactDllDescription = rowMod["�����㷨DLL����"].ToString().Trim();
                    rowMod.AcceptChanges();
                    try
                    {
                        sqlResult = this.pactManager.UpdatePactUnitInfo(pactInfoMod);
                        if (sqlResult == -1)
                        {
                            break;
                        }
                    }
                    catch (Exception ee)
                    {
                        errorText = ee.Message;
                    }
                    pactInfoMod = null;
                }
            }

            if (sqlResult == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("��������ʧ�ܣ�" + errorText);
                this.SetFrpColumnType();
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.Commit();
                this.dtMain.AcceptChanges();
                //ɾ����ͬ��λά����Ϣ {16C790A2-6158-487b-8AC5-2F6B4683CAE4} xuc
                this.InitMainData();
                this.SetFrpColumnType();
                MessageBox.Show("����ɹ���");
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private void SaveData()
        {
            if (this.neuTabControl1.SelectedIndex == 0)
            {
                this.SavePactUnit();
            }
            else
            {
                this.SavePactUnitDetail();
            }
        }

        /// <summary>
        /// ���toolbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("���", "�ӳ����������Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            toolBarService.AddToolButton("ɾ��", "ɾ��ѡ����ϸ��Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);

            return this.toolBarService;
        }

        /// <summary>
        /// ����toolbarservice��save��ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveData();
            return base.OnSave(sender, neuObject);
        }

        #endregion

        #region �¼�
        private void ucPactUnitMaintenance_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void neuTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.IsMainDataChange())
            {
                this.SavePactUnit();
            }
            else
            {
                this.dtMain.RejectChanges();
            }
            if (this.IsDetailDataChange())
            {
                this.SavePactUnitDetail();
            }

            int row = this.fpMain_Sheet1.ActiveRowIndex;
            string pactCode = this.fpMain_Sheet1.GetText(row, 0).ToString().Trim();
            this.QueryPactUnitInfoDetail(pactCode, this.neuTabControl2.SelectedIndex);
        }

        private void neuTabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.IsDetailDataChange())
            {
                this.SavePactUnitDetail();
            }
            int row = this.fpMain_Sheet1.ActiveRowIndex;
            string pactCode = this.fpMain_Sheet1.GetText(row, 0).ToString().Trim();
            this.QueryPactUnitInfoDetail(pactCode, this.neuTabControl2.SelectedIndex);
        }

        private void fpFeeCode_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string pactCode = string.Empty;
            string itemCode = string.Empty;
            string itemName = string.Empty;
            pactCode = this.fpMain_Sheet1.GetText(this.fpMain_Sheet1.ActiveRowIndex, 0).ToString().Trim();
            itemCode = this.fpFeeCode_Sheet1.GetText(this.fpFeeCode_Sheet1.ActiveRowIndex, 0).ToString().Trim();
            itemName = this.fpFeeCode_Sheet1.GetText(this.fpFeeCode_Sheet1.ActiveRowIndex, 1).ToString().Trim();
            if (this.IsExistData(itemCode))
            {
                MessageBox.Show("�˷���������ϸ�б���", "��ʾ");
                return;
            }
            else
            {
                dtDetail.Rows.Add(new object[]
                {
                    pactCode,
                    itemName,
                    itemCode,
                    "0",
                    0,
                    0,
                    0,
                    0,
                    0,
                    0//add xf �޶�
                });
            }
        }

        private void fpMain_EditModeOff(object sender, EventArgs e)
        {
            if (this.fpMain_Sheet1.ActiveColumnIndex == 22)
            {
                string spath = this.DllPath + "\\" + this.fpMain_Sheet1.ActiveCell.Text.Trim();
                if (spath != string.Empty)
                {
                    FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare im = this.GetDllInterface(spath);
                    if (im != null)
                        this.fpMain_Sheet1.Cells[this.fpMain_Sheet1.ActiveRowIndex, 23].Text = im.Description;
                }
            }
        }
        #endregion

        #region ���з���

        /// <summary>
        /// toolbarclick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "���":
                    this.AddPactUnitByCon();
                    break;
                case "ɾ��":
                    this.DeleteDetail();
                    break;

            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion



        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get 
            {
                Type[] t = new Type[1];
                t[0] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IValidPactItemChoose);
                return t;
            }
        }

        #endregion

        private void tbQueryValue_TextChanged(object sender, EventArgs e)
        {
            string QueryValue = "";
            if (!this.tbQueryValue.Text.Equals("") && this.tbQueryValue.Text != null)
            
                QueryValue=this.tbQueryValue.Text.Trim().ToUpper();
                string con = " ƴ���� like '%" + QueryValue + "%'";
                con += " or ����� like '%" + QueryValue + "%'";
                con += " or ������ like '%" + QueryValue + "%'";
                con += " or ��Ŀ���� like '%" + QueryValue + "%'";
                con += " or ��Ŀ���� like '%" + QueryValue + "%'";
                this.dvDetail.RowFilter = con;
            }
        }
}
