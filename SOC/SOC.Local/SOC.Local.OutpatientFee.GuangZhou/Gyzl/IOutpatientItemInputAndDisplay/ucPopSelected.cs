using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using FarPoint.Win.Spread;
using FS.FrameWork.Models;
using FS.FrameWork.Function;

namespace FS.SOC.Local.OutpatientFee.GuangZhou.Gyzl.IOutpatientItemInputAndDisplay
{
    /// <summary>
    /// ucPopSelected<br></br>
    /// [��������: ���ﵯ��ѡ����ĿUC]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2006-2-5]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucPopSelected : Form, FS.HISFC.BizProcess.Integrate.FeeInterface.IChooseItemForOutpatient
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ucPopSelected()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// �Ƿ�ѡ����Ŀ��ʱ���жϿ��
        /// </summary>
        protected bool isJudgeStore = false;

        /// <summary>
        /// ������Ŀ��DataSet
        /// </summary>
        protected DataSet dsAllItem = new DataSet();

        /// <summary>
        /// ������ַ���
        /// </summary>
        protected string inputChar = string.Empty;

        /// <summary>
        /// ��ѯ��ʽ,Ĭ��ƴ��
        /// </summary>
        protected FS.HISFC.Models.Base.InputTypes inputType = FS.HISFC.Models.Base.InputTypes.Spell;

        /// <summary>
        /// ѡ����Ŀ�ķ�ʽ,����Ĭ��Ϊ����س������
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.FeeInterface.ChooseItemTypes chooseItemType = FS.HISFC.BizProcess.Integrate.FeeInterface.ChooseItemTypes.ItemInputEnd;

        /// <summary>
        /// ԭʼ��Ŀ�б�(����֮ǰ��)
        /// </summary>
        private DataView preDvItem;
        /// <summary>
        /// ԭʼ��ѯ�ַ���
        /// </summary>
        string rowFilter = "";
        /// <summary>
        /// ��ѯ��
        /// </summary>
        private string queryText;
        /// <summary>
        /// �Ƿ�ģ����ѯ
        /// </summary>
        private bool queryLike = false;
        /// <summary>
        /// ��Ŀ���
        /// </summary>
        private FS.HISFC.Models.Base.ItemTypes itemType = FS.HISFC.Models.Base.ItemTypes.All;
        /// <summary>
        /// ��Ŀ��DataTable
        /// </summary>
        private DataTable dtItem = null;
        /// <summary>
        /// ��Ӧ��Ŀ��DataTable ����ͼ
        /// </summary>
        private DataView dvItem = null;
        /// <summary>
        /// �����ļ�·��
        /// </summary>
        private string filePath = Application.StartupPath + @".\profile\clinicItemList.xml";
        /// <summary>
        /// Ĭ��ÿҳ��ʾ9����¼���Ժ������������
        /// </summary>
        private int itemCount = 9;
        /// <summary>
        /// ��ǰҳ���һ��;
        /// </summary>
        private int nowCount = 0;
        /// <summary>
        /// ��ѡ��һ����Ŀ��ʱ���Ƿ�رմ���
        /// </summary>
        private bool selectAndClose = false;
        /// <summary>
        /// ��ǰѡ������Ŀʵ��
        /// </summary>
        private FS.HISFC.Models.Base.Item nowItem = null;
    
        /// <summary>
        /// ��ѡ����Ŀ�󴥷�
        /// </summary>
        public event FS.HISFC.BizProcess.Integrate.FeeInterface.WhenGetItem SelectedItem;
        /// <summary>
        /// ��ǰ���Ҵ���
        /// </summary>
        private string deptCode = "";
        /// <summary>
        /// ������˺�ĵ���ĿfpSheet
        /// </summary>
        private FarPoint.Win.Spread.SheetView fpSheetData = new FarPoint.Win.Spread.SheetView();

        /// <summary>
        /// סԺ����ҵ���
        /// </summary>
        FS.HISFC.BizLogic.Fee.InPatient myInpatient = new FS.HISFC.BizLogic.Fee.InPatient();
        /// <summary>
        /// ҽ���ӿ�ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Interface myInterface = new FS.HISFC.BizLogic.Fee.Interface();

        /// <summary>
        /// ҩƷҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        private bool isSelectItem = false;

        private string inputCode = "";
        private string queryType = "1";//Ĭ�Ϻ�ģ��
        /// <summary>
        /// �������
        /// </summary>
        private FS.HISFC.Models.Base.ItemKind itemKind = FS.HISFC.Models.Base.ItemKind.All;

        #endregion

        #region ����

        /// <summary>
        /// �����б�����
        /// </summary>
        public FS.HISFC.Models.Base.ItemKind ItemKind
        {
            get
            {
                return this.itemKind;
            }
            set
            {
                this.itemKind = value;
            }
        }

        /// <summary>
        /// ��ѯ��Ŀ�����
        /// </summary>
        public FS.HISFC.Models.Base.ItemTypes ItemType
        {
            get
            {
                return itemType;
            }
            set
            {
                itemType = value;
            }
        }

        /// <summary>
        /// ģ����ѯ��ʽ
        /// </summary>
        public FS.HISFC.Models.Base.InputTypes InputType
        {
            get
            {
                return inputType;
            }
            set
            {
                inputType = value;
            }
        }

        /// <summary>
        /// �������ȼ�
        /// </summary>
        public string InputPrev
        {
            get
            {
                if (this.cmbPrev.Tag != null)
                {
                    return this.cmbPrev.Tag.ToString();
                }
                else
                {
                    return "SYS_CLASS ASC";
                }
            }
            set 
            {
                
            }
        }

        /// <summary>
        /// �Ƿ���Чѡ����Ŀ
        /// </summary>
        public bool IsSelectItem
        {
            get
            {
                return isSelectItem;
            }
            set
            {
                this.isSelectItem = value;
            }
        }

        /// <summary>
        /// ��ǰ���Ҵ���
        /// </summary>
        public string DeptCode
        {
            set
            {
                this.deptCode = value;
            }
            get 
            {
                return this.deptCode;
            }
        }

        /// <summary>
        /// ������˺�ĵ���ĿfpSheet
        /// </summary>
        public FarPoint.Win.Spread.SheetView FpSheetData
        {
            set
            {
                fpSheetData = value;
                if (this.preDvItem == null)
                {
                    this.preDvItem = ((DataView)this.fpSheetData.DataSource);
                }
                int iReturn = 0;
                nowCount = 0;
                btnNextPage.Enabled = false;
                btnPre.Enabled = false;
                //��ѯ�������Ŀ
                iReturn = Query();
                if (iReturn == -1)
                {
                    return;
                }
                if (iReturn > itemCount)// �����ѯ����Ŀ����9�У�����һҳ��ťΪ�����ã����򲻿���
                {
                    btnNextPage.Enabled = true;
                }
                else
                {
                    btnNextPage.Enabled = false;
                }
                this.fpSpread1.Focus();

            }
        }

        /// <summary>
        /// ��ѯ��
        /// </summary>
        public string QueryText
        {
            get
            {
                return queryText;
            }
            set
            {
                queryText = value;
                tbInput.Text = queryText;
                tbInput.Focus();
                tbInput.SelectAll();
            }
        }

        /// <summary>
        /// ÿ��ȡ��������¼ > 1
        /// </summary>
        public int ItemCount
        {
            get
            {
                return itemCount;
            }
            set
            {
                itemCount = value;
            }
        }

        /// <summary>
        /// ��ѡ��һ����Ŀ��ʱ���Ƿ�رմ���;
        /// </summary>
        public bool SelectAndClose
        {
            get
            {
                return selectAndClose;
            }
            set
            {
                selectAndClose = value;
            }
        }

        /// <summary>
        /// ��ǰѡ������Ŀ
        /// </summary>
        /// <returns></returns>
        public FS.HISFC.Models.Base.Item NowItem
        {
            get
            {
                return nowItem;
            }
            set
            {
                nowItem = value;
            }
        }

        /// <summary>
        /// ģ����ѯ
        /// </summary>
        public bool QueryLike
        {
            get
            {
                return this.queryLike;
            }
            set
            {
                this.queryLike = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ����ʾ����Ϣ
        /// </summary>
        private void InitColumn()
        {
            if (File.Exists(filePath))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(filePath, dtItem, ref dvItem, this.fpSpread1_Sheet1);
            }
            else
            {
                Type str = typeof(string);
                Type dec = typeof(decimal);
                Type bl = typeof(bool);

                dtItem.Columns.AddRange(new DataColumn[]{new DataColumn("���÷���", str),
															new DataColumn("����", str),
															new DataColumn("������", str),
															new DataColumn("Ӣ����", str),
															new DataColumn("���", str),
															new DataColumn("���", str),
															new DataColumn("����", str),
															new DataColumn("�Զ�����", str),
															new DataColumn("����", dec),
															new DataColumn("��λ", str),
															new DataColumn("�������", str),
															new DataColumn("ҽ�����", str),
															new DataColumn("�Ը�����", dec),
															new DataColumn("��Ŀ���", bl),
															new DataColumn("ִ�п���", str)});
                dvItem = new DataView(dtItem);
                this.fpSpread1_Sheet1.DataSource = dvItem;

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1_Sheet1, filePath);
            }
        }

        /// <summary>
        /// ��ʼ��Farpoint
        /// </summary>
        private void InitFp()
        {
            InputMap im;

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.PageDown, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.PageUp, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
        }

        /// <summary>
        /// ���ָ���е���Ŀ��Ϣ
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetItem(int row)
        {
            if (row > this.fpSpread1_Sheet1.Rows.Count - 1)
            {
                return "";
            }
            else
            {
                if (this.fpSpread1_Sheet1.Rows[row].Tag == null || this.fpSpread1_Sheet1.Rows[row].Tag.ToString() == "")
                {
                    return "";
                }
                else
                {
                    return ((NeuObject)this.fpSpread1_Sheet1.Rows[row].Tag).Name;
                }
            }
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int Query()
        {
            dtItem.Rows.Clear();
            string feeCode = "";//��С���ô���
            string itemCode = "";//��Ŀ����
            string drugFlag = "";//��Ŀ��� 0 ��ҩƷ 1 ҩƷ 2 �����Ŀ
            string itemName = "";//��Ŀ����

            FS.HISFC.Models.Pharmacy.Storage stoItem = null;
            if (fpSheetData.Rows.Count == 1)
            {
                if (isJudgeStore)
                {
                    drugFlag = this.fpSheetData.Cells[0, 0].Text;
                    string exeDeptCode = this.fpSheetData.Cells[0, 22].Text;
                    if (drugFlag == "1")//���ҩƷ���
                    {
                        itemName = this.fpSheetData.Cells[0, 4].Text.ToString();
                        itemCode = this.fpSheetData.Cells[0, 3].Text;
                        decimal storeSum = 0;

                        stoItem = this.pharmacyIntegrate.GetStockInfoByDrugCode(this.fpSheetData.Cells[0, 22].Text, itemCode);
                        if (stoItem != null)
                        {
                            if (stoItem.IsStop)
                            {
                                MessageBox.Show(itemName + "[ҩƷȱҩ]!");
                                isSelectItem = false;

                                return -1;
                            }
                        }

                        int iReturn = this.pharmacyIntegrate.GetStorageNum(exeDeptCode, itemCode, out storeSum);

                        if (iReturn <= 0)
                        {
                            MessageBox.Show("���ҿ��ʧ��!");
                            isSelectItem = false;

                            return -1;
                        }
                        if (storeSum <= 0)
                        {
                            MessageBox.Show(itemName + "��治��!");
                            isSelectItem = false;

                            return -1;
                        }
                    }
                }
                isSelectItem = true;
                //{40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
                SelectedItem(fpSheetData.Cells[0, 3].Text, this.fpSheetData.Cells[0, 0].Text, fpSheetData.Cells[0, 22].Text, NConvert.ToDecimal(this.fpSheetData.Cells[0, 16].Text));

                return 0;
            }
            int i = 0;//��ǰȡ�õ�����

            decimal price = 0;
            decimal packQty = 0;

            for (int j = nowCount; j < fpSheetData.Rows.Count; j++)
            {
                i++;
                if (i > itemCount)
                {
                    break;
                }
                itemCode = this.fpSheetData.Cells[j, 3].Text;
                feeCode = this.fpSheetData.Cells[j, 2].Text;
                drugFlag = this.fpSheetData.Cells[j, 0].Text;

                DataRow row = dtItem.NewRow();

                if (drugFlag == "1")//ҩƷˢ�¿��
                {
                    stoItem = this.pharmacyIntegrate.GetStockInfoByDrugCode(this.fpSheetData.Cells[j, 22].Text, itemCode);
                    if (stoItem != null)
                    {
                        if (stoItem.IsStop)
                        {
                            row["���"] = "1";
                        }
                         else
                        {
                            row["���"] = FS.FrameWork.Public.String.FormatNumber( stoItem.StoreQty/stoItem.Item.PackQty,2);
                        }
                    }
                    else
                    {
                        row["���"] = this.fpSheetData.Cells[j, 21].Text;
                    }
                    //liu.xq20071009��ʾ���
                }
                if (feeCode != "")
                {
                    feeCode = myInpatient.GetComDictionaryNameByID("MINFEE", feeCode);
                }

                row["���÷���"] = feeCode;
                row["����"] = itemCode;
                row["������"] = this.fpSheetData.Cells[j, 4].Text.ToString();
                row["Ӣ����"] = this.fpSheetData.Cells[j, 5].Text.ToString();
                row["���"] = this.fpSheetData.Cells[j, 7].Text;
                row["����"] = this.fpSheetData.Cells[j, 8].Text;
                row["�Զ�����"] = this.fpSheetData.Cells[j, 11].Text;

                FS.HISFC.Models.Base.Department dept = new FS.HISFC.Models.Base.Department();

                dept = this.managerIntegrate.GetDepartment(this.fpSheetData.Cells[j, 22].Text);
                string deptName = "";
                if (dept != null)
                {
                    deptName = dept.Name;
                }
                else
                {
                    deptName = this.fpSheetData.Cells[j, 22].Text;
                }
                row["ִ�п���"] = deptName;

                price = NConvert.ToDecimal(this.fpSheetData.Cells[j, 16].Text);
                packQty = NConvert.ToDecimal(this.fpSheetData.Cells[j, 6].Text);
                if (packQty == 0)
                {
                    packQty = 1;
                }

                row["����"] = price;
                row["��λ"] = this.fpSheetData.Cells[j, 19].Text;

                dtItem.Rows.Add(row);
                //Ĭ������Ϊ��ɫ
                this.fpSpread1_Sheet1.Cells[i - 1, 2].ForeColor = Color.Black;
                //�����ҩƷ���ҿ�治�㣬����Ϊ��ɫ
                if (drugFlag == "1")
                {
                    try
                    {
                        if (row["���"].ToString() == "1")
                        {
                            this.fpSpread1_Sheet1.Rows[i - 1].ForeColor = Color.Red;
                            row["������"] = row["������"].ToString() + "{ȱҩ}";
                        }
                        else
                        {
                            this.fpSpread1_Sheet1.Rows[i - 1].ForeColor = Color.Black;
                            row["������"] = row["������"].ToString();
                        }
                    }
                    catch { }
                }
                NeuObject obj = new NeuObject();
                obj.ID = row["����"].ToString();
                obj.Name = this.fpSheetData.Cells[j, 22].Text;
                obj.Memo = drugFlag;
                obj.User01 = this.fpSheetData.Cells[j, 34].Text;

                if (row["���"].ToString() == "1")
                {
                    obj.User01 = "1";
                }
                else
                {
                    obj.User01 = "0";
                }

                this.fpSpread1_Sheet1.Rows[i - 1].Tag = obj;
                this.fpSpread1_Sheet1.Rows[i - 1].Label = "F" + i.ToString();
            }

            FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1_Sheet1, filePath);

            if (this.fpSpread1_Sheet1.RowCount > 0)
            {
                this.fpSpread1_Sheet1.ActiveRowIndex = 0;
                this.fpSpread1_Sheet1.SetActiveCell(0, 0, false);
            }

            return fpSheetData.Rows.Count - nowCount;
        }

        /// <summary>
        /// ��С���ù���
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int QueryByFeeCode()
        {
            dtItem.Rows.Clear();
            string feeCode = "";//��С���ô���
            string itemCode = "";//��Ŀ����
            string drugFlag = "";//��Ŀ��� 0 ��ҩƷ 1 ҩƷ 2 �����Ŀ

            FS.HISFC.Models.Pharmacy.Storage stoItem = null;

            int i = 0;//��ǰȡ�õ�����

            decimal price = 0;
            decimal packQty = 0;

            for (int j = nowCount; j < fpSheetData.Rows.Count; j++)
            {
                i++;
                if (i > itemCount)
                {
                    break;
                }
                itemCode = this.fpSheetData.Cells[j, 3].Text;
                feeCode = this.fpSheetData.Cells[j, 2].Text;
                drugFlag = this.fpSheetData.Cells[j, 0].Text;

                DataRow row = dtItem.NewRow();

                if (drugFlag == "1")//ҩƷˢ�¿��
                {
                    stoItem = this.pharmacyIntegrate.GetStockInfoByDrugCode(this.fpSheetData.Cells[j, 22].Text, itemCode);

                    if (stoItem != null)
                    {
                        if (stoItem.IsStop)
                        {
                            row["���"] = "1";
                        }
                        else
                        {
                            row["���"] = "0";
                        }
                    }
                }
                if (feeCode != "")
                {
                    feeCode = this.myInpatient.GetComDictionaryNameByID("MINFEE", feeCode);
                }

                row["���÷���"] = feeCode;
                row["����"] = itemCode;
                row["������"] = this.fpSheetData.Cells[j, 4].Text.ToString();
                row["Ӣ����"] = this.fpSheetData.Cells[j, 5].Text.ToString();
                row["���"] = this.fpSheetData.Cells[j, 7].Text;
                row["����"] = this.fpSheetData.Cells[j, 8].Text;
                row["�Զ�����"] = this.fpSheetData.Cells[j, 11].Text;

                FS.HISFC.Models.Base.Department dept = new FS.HISFC.Models.Base.Department();
                dept = this.managerIntegrate.GetDepartment(this.fpSheetData.Cells[j, 22].Text);
                string deptName = "";
                if (dept != null)
                {
                    deptName = dept.Name;
                }
                else
                {
                    deptName = this.fpSheetData.Cells[j, 22].Text;
                }
                row["ִ�п���"] = deptName;

                price = NConvert.ToDecimal(this.fpSheetData.Cells[j, 16].Text);
                packQty = NConvert.ToDecimal(this.fpSheetData.Cells[j, 6].Text);
                if (packQty == 0)
                {
                    packQty = 1;
                }

                row["����"] = price;
                row["��λ"] = this.fpSheetData.Cells[j, 19].Text;

                dtItem.Rows.Add(row);
                //Ĭ������Ϊ��ɫ
                this.fpSpread1_Sheet1.Cells[i - 1, 2].ForeColor = Color.Black;
                //�����ҩƷ���ҿ�治�㣬����Ϊ��ɫ
                if (drugFlag == "1")
                {
                    try
                    {
                        if (row["���"].ToString() == "1")
                        {
                            this.fpSpread1_Sheet1.Rows[i - 1].ForeColor = Color.Red;
                            row["������"] = row["������"].ToString() + "{ȱҩ}";
                        }
                        else
                        {
                            this.fpSpread1_Sheet1.Rows[i - 1].ForeColor = Color.Black;
                            row["������"] = row["������"].ToString();
                        }
                    }
                    catch { }
                }
                NeuObject obj = new NeuObject();
                obj.ID = row["����"].ToString();
                obj.Name = this.fpSheetData.Cells[j, 22].Text;
                obj.Memo = drugFlag;

                if (row["���"].ToString() == "1")
                {
                    obj.User01 = "1";
                }
                else
                {
                    obj.User01 = "0";
                }

                this.fpSpread1_Sheet1.Rows[i - 1].Tag = obj;
                this.fpSpread1_Sheet1.Rows[i - 1].Label = "F" + i.ToString();
            }

            FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1_Sheet1, filePath);

            if (this.fpSpread1_Sheet1.RowCount > 0)
            {
                this.fpSpread1_Sheet1.ActiveRowIndex = 0;
                this.fpSpread1_Sheet1.SetActiveCell(0, 0, false);
            }

            return fpSheetData.Rows.Count - nowCount + 1;
        }

        /// <summary>
        /// ��һҳ
        /// </summary>
        public void NextPage()
        {
            btnNextPage.Enabled = false;
            int iReturn = 0;
            nowCount += itemCount;
            //��ѯ����
            iReturn = Query();
            //��ѯ�������

            if (iReturn > itemCount) //˵������ҳ������Ŀ
            {
                btnNextPage.Enabled = true;
            }
            else //˵����ҳ������Ŀ
            {
                btnNextPage.Enabled = false;
            }

            btnPre.Enabled = true;
        }

        /// <summary>
        /// ��һҳ
        /// </summary>
        private void PrePage()
        {
            btnPre.Enabled = false;
            nowCount -= itemCount;
            if (nowCount < 0)
            {
                return;
            }
            if (nowCount == 0)
            {
                btnPre.Enabled = false;
            }
            else
            {
                btnPre.Enabled = true;
            }

            Query();
            btnNextPage.Enabled = true;
        }

        /// <summary>
        /// ����Fn���ܼ�ѡ����Ŀ
        /// </summary>
        /// <param name="i"></param>
        private void SelectItemKeys(int i)
        {
            string itemCode = GetItem(i);
            if (itemCode == "")
            {
                return;
            }
            NeuObject obj = ((NeuObject)this.fpSpread1_Sheet1.Rows[i].Tag);
            if (isJudgeStore)
            {

                if (obj.Memo == "1")//���ҩƷ���
                {
                    decimal storeSum = 0;
                    int iReturn = this.pharmacyIntegrate.GetStorageNum(obj.Name, obj.ID, out storeSum);
                    if (iReturn <= 0)
                    {
                        MessageBox.Show("���ҿ��ʧ��!");
                        isSelectItem = false;
                        return;
                    }
                    if (storeSum <= 0)
                    {
                        MessageBox.Show("��治��!");
                        isSelectItem = false;
                        return;
                    }
                    if (obj.User01 == "1")
                    {
                        MessageBox.Show("����Ŀ�Ѿ�ȱҩ,����ѡ��!");
                        isSelectItem = false;
                        return;
                    }
                }

            }
            isSelectItem = true;
            //{40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
            SelectedItem(obj.ID, obj.Memo, obj.Name, NConvert.ToDecimal(obj.User02));
            this.FindForm().Close();
        }

        /// <summary>
        /// ���ִ�п���
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetExeDeptCode(int row)
        {
            if (row > this.fpSpread1_Sheet1.Rows.Count - 1)
            {
                return "";
            }
            else
            {
                if (this.fpSpread1_Sheet1.Rows[row].Tag == null || this.fpSpread1_Sheet1.Rows[row].Tag.ToString() == "")
                {
                    return "";
                }
                else
                {

                    return ((NeuObject)this.fpSpread1_Sheet1.Rows[row].Tag).Name;
                }
            }
        }

        /// <summary>
        /// ��ʼ�����ȼ�
        /// </summary>
        public void InitPrev()
        {
            ArrayList alPrev = new ArrayList();

            NeuObject o2 = new NeuObject();

            o2.ID = "";

            o2.Name = "Ĭ��";

            alPrev.Add(o2);

            NeuObject o = new NeuObject();

            o.ID = "SYS_CLASS ASC";

            o.Name = "��ҩ����";

            alPrev.Add(o);

            NeuObject o1 = new NeuObject();

            o1.ID = "SYS_CLASS DESC";

            o1.Name = "��ҩ����";

            alPrev.Add(o1);

            this.cmbPrev.AddItems(alPrev);

            ArrayList alValues = FS.FrameWork.WinForms.Classes.Function.GetDefaultValue("INPUTPREV");

            if (alValues == null || alValues.Count <= 0)
            {
                this.cmbPrev.SelectedIndexChanged -= new EventHandler(cmbPrev_SelectedIndexChanged);
                this.cmbPrev.SelectedIndex = 0;
                this.cmbPrev.SelectedIndexChanged += new EventHandler(cmbPrev_SelectedIndexChanged);
            }
            else
            {
                this.cmbPrev.Tag = alValues[0].ToString();
                this.cmbPrev.Text = alValues[1].ToString();
            }
        }

        /// <summary>
        /// ��ù�������
        /// </summary>
        private void SetRowFilter()
        {
            string tagValue = this.cmbQueryType.Tag.ToString();

            switch (tagValue)
            {
                case "0":
                    rowFilter = "SPELL_CODE like '%" + inputCode + "'" +
                        " OR " + "WB_CODE like '%" + inputCode + "'" +
                        " OR " + "USER_CODE like '%" + inputCode.PadLeft(6, '0') + "'" +
                        " OR " + "ITEM_NAME like '%" + inputCode + "'" +
                        " OR " + "CUS_SPELL_CODE like '%" + inputCode + "'" +
                        " OR " + "CUS_WB_CODE like '%" + inputCode + "'" +
                        " OR " + "CUS_USER_CODE like '%" + inputCode + "'" +
                        " OR " + "CUS_NAME like '%" + inputCode + "'" +
                        " OR " + "OTHER_NAME like '%" + inputCode + "'" +
                        " OR " + "OTHER_SPELL like '%" + inputCode + "'" +
                        " OR " + "EN_NAME like '%" + inputCode + "'";
                    break;
                case "1":
                    rowFilter = "SPELL_CODE like '" + inputCode + "%'" +
                        " OR " + "WB_CODE like '" + inputCode + "%'" +
                        " OR " + "USER_CODE like '" + inputCode.PadLeft(6, '0') + "%'" +
                        " OR " + "ITEM_NAME like '" + inputCode + "%'" +
                        " OR " + "CUS_SPELL_CODE like '" + inputCode + "%'" +
                        " OR " + "CUS_WB_CODE like '" + inputCode + "%'" +
                        " OR " + "CUS_USER_CODE like '" + inputCode + "%'" +
                        " OR " + "CUS_NAME like '" + inputCode + "%'" +
                        " OR " + "OTHER_NAME like '%" + inputCode + "'" +
                        " OR " + "OTHER_SPELL like '%" + inputCode + "'" +
                        " OR " + "EN_NAME like '" + inputCode + "%'";
                    break;
                case "2":
                    rowFilter = "SPELL_CODE like '%" + inputCode + "%'" +
                        " OR " + "WB_CODE like '%" + inputCode + "%'" +
                        " OR " + "USER_CODE like '%" + inputCode.PadLeft(6, '0') + "%'" +
                        " OR " + "ITEM_NAME like '%" + inputCode + "%'" +
                        " OR " + "CUS_SPELL_CODE like '%" + inputCode + "%'" +
                        " OR " + "CUS_WB_CODE like '%" + inputCode + "%'" +
                        " OR " + "CUS_USER_CODE like '%" + inputCode + "%'" +
                        " OR " + "CUS_NAME like '%" + inputCode + "%'" +
                        " OR " + "OTHER_NAME like '%" + inputCode + "'" +
                        " OR " + "OTHER_SPELL like '%" + inputCode + "'" +
                        " OR " + "EN_NAME like '%" + inputCode + "%'";
                    break;
                case "3":
                    rowFilter = "SPELL_CODE like '" + inputCode + "'" +
                        " OR " + "WB_CODE like '" + inputCode + "'" +
                        " OR " + "USER_CODE like '" + inputCode.PadLeft(6, '0') + "'" +
                        " OR " + "ITEM_NAME like '" + inputCode + "'" +
                        " OR " + "CUS_SPELL_CODE like '" + inputCode + "'" +
                        " OR " + "CUS_WB_CODE like '" + inputCode + "'" +
                        " OR " + "CUS_USER_CODE like '" + inputCode + "'" +
                        " OR " + "CUS_NAME like '" + inputCode + "'" +
                        " OR " + "OTHER_NAME like '%" + inputCode + "'" +
                        " OR " + "OTHER_SPELL like '%" + inputCode + "'" +
                        " OR " + "EN_NAME like '" + inputCode + "'";
                    break;
                default:
                    rowFilter = "SPELL_CODE like '" + inputCode + "%'" +
                        " OR " + "WB_CODE like '" + inputCode + "%'" +
                        " OR " + "USER_CODE like '" + inputCode.PadLeft(6, '0') + "%'" +
                        " OR " + "ITEM_NAME like '" + inputCode + "%'" +
                        " OR " + "CUS_SPELL_CODE like '" + inputCode + "%'" +
                        " OR " + "CUS_WB_CODE like '" + inputCode + "%'" +
                        " OR " + "CUS_USER_CODE like '" + inputCode + "%'" +
                        " OR " + "CUS_NAME like '" + inputCode + "%'" +
                        " OR " + "OTHER_NAME like '%" + inputCode + "'" +
                        " OR " + "OTHER_SPELL like '%" + inputCode + "'" +
                        " OR " + "EN_NAME like '" + inputCode + "%'";
                    break;
            }
        }

        /// <summary>
        /// ��ʾҽ����Ϣ
        /// </summary>
        private void DisplayCompareInfo()
        {
            if (this.fpSpread1_Sheet1.ActiveRowIndex < 0)
            {
                return;
            }

            if (this.fpSpread1_Sheet1.Rows.Count <= 0)
            {
                return;
            }

            NeuObject obj = this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.ActiveRowIndex].Tag as NeuObject;

            if (obj == null)
            {
                return;
            }

            DataRow findRow;
            DataRow[] rowFinds = this.preDvItem.Table.Select("ITEM_CODE = " + "'" + obj.ID + "'");
            FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();
            feeItem.ID = obj.ID;
            //ҽ����Ϣ
            try
            {
                myInterface.GetCompareSingleItem("2", feeItem.ID, ref feeItem.Compare);
            }
            catch { }

            if (rowFinds != null && rowFinds.Length > 0)
            {
                findRow = rowFinds[0];
                string siInfo = "";

                if (feeItem.Compare != null)
                {
                    if (feeItem.Compare.CenterItem.ItemGrade == "1")
                    {
                        siInfo += "ҽ�����:����" + System.Convert.ToString(feeItem.Compare.CenterItem.Rate * 100) + "%";
                    }
                    else if (feeItem.Compare.CenterItem.ItemGrade == "2")
                    {
                        siInfo += "ҽ�����:����" + System.Convert.ToString(feeItem.Compare.CenterItem.Rate * 100) + "%";
                    }
                    else
                    {
                        siInfo += "ҽ�����:�Է�" + System.Convert.ToString(feeItem.Compare.CenterItem.Rate * 100) + "%";
                    }
                }
                this.label5.Text = siInfo + "\n" + "ͨ����:" + findRow["cus_name"].ToString() + " Ӣ����:" + findRow["en_name"].ToString().ToLower() +
                    "����:" + findRow["OTHER_NAME"].ToString() + "\n" +
                    "���:" + findRow["SPECS"].ToString() + " ����:" + findRow["DOSE_CODE"].ToString();
            }
        }


        #endregion

        #region IChooseItemForOutpatient ��Ա

        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int Init()
        {
            dtItem = new DataTable();
            //��ʼ������Ϣ
            InitColumn();
            tbInput.Focus();
            tbInput.SelectAll();
            this.InitFp();

            ArrayList alMinFee = new ArrayList();
            FS.HISFC.Models.Base.Const cnst = new FS.HISFC.Models.Base.Const();
            cnst.ID = "";
            cnst.Name = "ȫ��";
            alMinFee.Add(cnst);
            ArrayList al = this.managerIntegrate.GetConstantList("MINFEE");
            if (al == null)
            {
                MessageBox.Show("�����С����������");
            }
            else
            {
                alMinFee.AddRange(al);
                this.cmbMinFee.AddItems(alMinFee);
            }

            this.chooseItemType = FS.HISFC.BizProcess.Integrate.FeeInterface.ChooseItemTypes.ItemInputEnd;

            return 1;
        }

        /// <summary>
        /// ���ѡ�е���Ŀ
        /// </summary>
        /// <param name="item">ѡ�е���Ŀ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int GetSelectedItem(ref FS.HISFC.Models.Base.Item item)
        {
            item = this.nowItem.Clone();

            return 1;
        }

        /// <summary>
        /// �Ƿ�ѡ����Ŀ��ʱ���жϿ��
        /// </summary>
        public bool IsJudgeStore
        {
            get
            {
                return this.isJudgeStore;
            }
            set
            {
                this.isJudgeStore = value;
            }
        }

        /// <summary>
        /// ����������Ŀ��DataSet
        /// </summary>
        /// <param name="dsItem">������Ŀ��DataSet</param>
        public void SetDataSet(DataSet dsItem)
        {
            this.dsAllItem = dsItem;
        }

        /// <summary>
        /// ���ô���Ĺ����ַ���,�Ͳ�ѯ��ʽ
        /// </summary>
        /// <param name="inputChar">����Ĺ����ַ���</param>
        /// <param name="inputType">��ѯ��ʽ</param>
        public void SetInputChar(object sender, string inputChar, FS.HISFC.Models.Base.InputTypes inputType)
        {
            this.inputChar = inputChar;
            this.inputType = inputType;
        }

        /// <summary>
        /// ѡ����Ŀ�ķ�ʽ,����Ĭ��Ϊ����س������
        /// </summary>
        public FS.HISFC.BizProcess.Integrate.FeeInterface.ChooseItemTypes ChooseItemType
        {
            get
            {
                return this.chooseItemType;
            }
            set
            {
                this.chooseItemType = value;
            }
        }

        /// <summary>
        /// �Ƿ�ģ����ѯ
        /// </summary>
        public bool IsQueryLike
        {
            get
            {
                return this.queryLike;
            }
            set
            {
                this.queryLike = value;
            }
        }

        /// <summary>
        /// �Ƿ�ѡ����Ŀ��رմ���
        /// </summary>
        public bool IsSelectAndClose
        {
            get
            {
                return this.selectAndClose;
            }
            set
            {
                this.selectAndClose = value;
            }
        }

        /// <summary>
        /// ��ѯ��ʽ
        /// </summary>
        public string QueryType 
        {
            get 
            {
                return this.queryType;
            }
            set 
            {
                this.queryType = value;
            }
        }

        /// <summary>
        /// ���˺����,������FarPoint
        /// </summary>
        public object ObjectFilterObject
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                if (value == null)
                {
                    return;
                }

                fpSheetData = value as FarPoint.Win.Spread.SheetView;
                if (this.preDvItem == null)
                {
                    this.preDvItem = ((DataView)this.fpSheetData.DataSource);
                }
                int iReturn = 0;
                nowCount = 0;
                btnNextPage.Enabled = false;
                btnPre.Enabled = false;
                //��ѯ�������Ŀ
                iReturn = Query();
                if (iReturn == -1)
                {
                    return;
                }
                if (iReturn > itemCount)// �����ѯ����Ŀ����9�У�����һҳ��ťΪ�����ã����򲻿���
                {
                    btnNextPage.Enabled = true;
                }
                else
                {
                    btnNextPage.Enabled = false;
                }
                this.fpSpread1.Focus();
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="p">��ǰ����</param>
        public void SetLocation(Point p)
        {
            this.Location = p;
        }

        /// <summary>
        /// ��һ��
        /// </summary>
        public void NextRow()
        {

        }


        /// <summary>
        /// ��һ��
        /// </summary>
        public void PriorRow()
        {

        }

        /// <summary>
        /// ��һҳ
        /// </summary>
        public void PriorPage()
        {

        }

        /// <summary>
        /// ѡ��ǰ��Ŀ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ��-1</returns>
        public int GetSelectedItem()
        {
            return 1;
        }


        #endregion

        private void cmbPrev_SelectedIndexChanged(object sender, EventArgs e)
        {
            NeuObject o = this.cmbPrev.SelectedItem;

            if (o != null)
            {
                FS.FrameWork.WinForms.Classes.Function.SaveDefaultValue("INPUTPREV", o.ID, o.Name);
            }
            else
            {
                return;
            }

            this.fpSpread1.Focus();
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            if (btnNextPage.Enabled == false)
            {
                return;
            }

            NextPage();
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            //if (btnNextPage.Enabled == false)
            //{
            //    return;
            //}
            this.PrePage();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            int iReturn = 0;
            nowCount = 0;
            btnNextPage.Enabled = false;
            btnPre.Enabled = false;
            iReturn = Query();
            if (iReturn == -1)
            {
                return;
            }
            if (iReturn > itemCount)
            {
                btnNextPage.Enabled = true;
            }
            else
            {
                btnNextPage.Enabled = false;
            }
        }

        private void fpSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1_Sheet1, filePath);
        }

        private void tbInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender == null)
            {
                return;
            }
            if (e.KeyCode == Keys.Up)
            {
                this.fpSpread1.SetViewportTopRow(0, this.fpSpread1_Sheet1.ActiveRowIndex - 5);
                this.fpSpread1_Sheet1.ActiveRowIndex--;
                ////{0CD66D53-785C-4ba5-840B-885F01A31A42}
                //this.fpSpread1_Sheet1.AddSelection(this.fpSpread1_Sheet1.ActiveRowIndex, 0, 1, 0);
                this.fpSpread1_Sheet1.AddSelection(this.fpSpread1_Sheet1.ActiveRowIndex, 1, 1, 1);
            }
            if (e.KeyCode == Keys.Down)
            {
                this.fpSpread1.SetViewportTopRow(0, this.fpSpread1_Sheet1.ActiveRowIndex - 4);
                this.fpSpread1_Sheet1.ActiveRowIndex++;
                ////{0CD66D53-785C-4ba5-840B-885F01A31A42}
                //this.fpSpread1_Sheet1.AddSelection(this.fpSpread1_Sheet1.ActiveRowIndex, 0, 1, 0);
                this.fpSpread1_Sheet1.AddSelection(this.fpSpread1_Sheet1.ActiveRowIndex, 1, 1, 1);
            }
            if (e.KeyCode == Keys.Enter)
            {
                FS.HISFC.Models.Base.Item obj = null;

                if (obj != null)
                {
                    //MessageBox.Show(obj.Name + obj.IsPharmacy.ToString());
                    MessageBox.Show(obj.Name + obj.ItemType.ToString());
                }
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {

            if (keyData == Keys.Escape)
            {
                this.isSelectItem = false;
                this.FindForm().Close();
            }
            if (keyData == Keys.F1)
            {
                SelectItemKeys(0);
            }
            if (keyData == Keys.F2)
            {
                SelectItemKeys(1);
            }
            if (keyData == Keys.F3)
            {
                SelectItemKeys(2);
            }
            if (keyData == Keys.F4)
            {
                SelectItemKeys(3);
            }
            if (keyData == Keys.F5)
            {
                SelectItemKeys(4);
            }
            if (keyData == Keys.F6)
            {
                SelectItemKeys(5);
            }
            if (keyData == Keys.F7)
            {
                SelectItemKeys(6);
            }
            if (keyData == Keys.F8)
            {
                SelectItemKeys(7);
            }
            if (keyData == Keys.F9)
            {
                SelectItemKeys(8);
            }
            if (keyData == Keys.F10)
            {
                this.cmbMinFee.SelectedIndex = (this.cmbMinFee.SelectedIndex + 1) % this.cmbMinFee.Items.Count;
                return true;
            }
            if (keyData == Keys.F11)
            {

            }
            if (keyData == Keys.F12)
            {
                this.cmbQueryType.SelectedIndex = (this.cmbQueryType.SelectedIndex + 1) % this.cmbQueryType.Items.Count;
            }
            if (keyData == Keys.PageDown)
            {
                if (btnNextPage.Enabled == false)
                {
                    return false;
                }
                NextPage();
            }
            if (keyData == Keys.PageUp)
            {
                if (btnPre.Enabled == false)
                {
                    return false;
                }
                PrePage();
            }
            if (keyData == Keys.N)
            {
                if (btnNextPage.Enabled == false)
                {
                    return false;
                }
                NextPage();
            }
            if (keyData == Keys.P)
            {
                if (btnPre.Enabled == false)
                {
                    return false;
                }
                PrePage();
            }
            if (keyData == Keys.Back)
            {
                if (this.cmbMinFee.SelectedIndex >= 1)
                {
                    this.cmbMinFee.SelectedIndex = (this.cmbMinFee.SelectedIndex - 1) % this.cmbMinFee.Items.Count;
                }
            }

            return base.ProcessDialogKey(keyData);
        }

        private void fpSpread1_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender == null)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (this.fpSpread1_Sheet1.RowCount > 0)
                {
                    int row = this.fpSpread1_Sheet1.ActiveRowIndex;
                    NeuObject obj = ((NeuObject)this.fpSpread1_Sheet1.Rows[row].Tag);
                    if (isJudgeStore)
                    {

                        if (obj.Memo == "1")//���ҩƷ���
                        {
                            decimal storeSum = 0;
                            int iReturn = this.pharmacyIntegrate.GetStorageNum(obj.Name, obj.ID, out storeSum);
                            if (iReturn <= 0)
                            {
                                MessageBox.Show("���ҿ��ʧ��!");
                                isSelectItem = false;
                                return;
                            }
                            if (storeSum <= 0)
                            {
                                MessageBox.Show("��治��!");
                                isSelectItem = false;
                                return;
                            }
                            if (obj.User01 == "1")
                            {
                                MessageBox.Show("����Ŀ�Ѿ�ȱҩ,����ѡ��!");
                                isSelectItem = false;
                                return;
                            }
                        }
                    }
                    isSelectItem = true;
                    //{40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
                    SelectedItem(obj.ID, obj.Memo, obj.Name, NConvert.ToDecimal(obj.User02));
                    this.FindForm().Close();
                }
            }
        }

        private void fpSpread1_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            if (sender == null)
            {
                return;
            }
            if (this.fpSpread1_Sheet1.RowCount > 0)
            {
                NeuObject obj = ((NeuObject)this.fpSpread1_Sheet1.Rows[e.Row].Tag);
                if (isJudgeStore)
                {

                    if (obj.Memo == "1")//���ҩƷ���
                    {
                        decimal storeSum = 0;
                        int iReturn = this.pharmacyIntegrate.GetStorageNum(obj.Name, obj.ID, out storeSum);
                        if (iReturn <= 0)
                        {
                            MessageBox.Show("���ҿ��ʧ��!");
                            return;
                        }
                        if (storeSum <= 0)
                        {
                            MessageBox.Show("��治��!");
                            return;
                        }
                        if (obj.User01 == "1")
                        {
                            MessageBox.Show("����Ŀ�Ѿ�ȱҩ,����ѡ��!");
                            isSelectItem = false;
                            return;
                        }
                    }
                }
                isSelectItem = true;
                //{40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
                SelectedItem(obj.ID, obj.Memo, obj.Name, NConvert.ToDecimal(obj.User02));
                this.FindForm().Close();
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            FS.HISFC.Components.Common.Controls.ucSetColumn ucSet = new FS.HISFC.Components.Common.Controls.ucSetColumn();
            ucSet.FilePath = this.filePath;
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucSet);
        }

        private void cmbMinFee_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strFilter = "";

            if (this.cmbMinFee.Tag != null)
            {
                strFilter = this.cmbMinFee.Tag.ToString();
            }

            ((DataView)this.fpSheetData.DataSource).RowFilter = "(" + rowFilter + ")" + " and " + "FEE_CODE like '%" + strFilter + "%'";

            int iReturn = 0;
            nowCount = 0;
            btnNextPage.Enabled = false;
            btnPre.Enabled = false;
            //��ѯ�������Ŀ
            iReturn = QueryByFeeCode();
            if (iReturn == -1)
            {
                return;
            }
            if (iReturn > itemCount)// �����ѯ����Ŀ����9�У�����һҳ��ťΪ�����ã����򲻿���
            {
                btnNextPage.Enabled = true;
            }
            else
            {
                btnNextPage.Enabled = false;
            }

            this.fpSpread1.Focus();
        }

        private void cmbQueryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetRowFilter();

            this.cmbMinFee_SelectedIndexChanged(null, null);

            this.fpSpread1.Focus();
        }

        private void fpSpread1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.fpSpread1_Sheet1.ActiveRowIndex < 0)
            {
                return;
            }

            if (this.fpSpread1_Sheet1.Rows.Count <= 0)
            {
                return;
            }

            NeuObject obj = this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.ActiveRowIndex].Tag as NeuObject;

            if (obj == null)
            {
                return;
            }

            DataRow findRow;
            DataRow[] rowFinds = this.preDvItem.Table.Select("ITEM_CODE = " + "'" + obj.ID + "'");
            FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();
            feeItem.ID = obj.ID;
            //ҽ����Ϣ
            try
            {
                myInterface.GetCompareSingleItem("2", feeItem.ID, ref feeItem.Compare);
            }
            catch { }

            if (rowFinds != null && rowFinds.Length > 0)
            {
                findRow = rowFinds[0];
                string siInfo = "";

                if (feeItem.Compare != null)
                {
                    if (feeItem.Compare.CenterItem.ItemGrade == "1")
                    {
                        siInfo += "ҽ�����:����" + System.Convert.ToString(feeItem.Compare.CenterItem.Rate * 100) + "%";
                    }
                    else if (feeItem.Compare.CenterItem.ItemGrade == "2")
                    {
                        siInfo += "ҽ�����:����" + System.Convert.ToString(feeItem.Compare.CenterItem.Rate * 100) + "%";
                    }
                    else
                    {
                        siInfo += "ҽ�����:�Է�" + System.Convert.ToString(feeItem.Compare.CenterItem.Rate * 100) + "%";
                    }
                }
                this.label5.Text = siInfo + "\n" + "ͨ����:" + findRow["cus_name"].ToString() + " Ӣ����:" + findRow["en_name"].ToString().ToLower() +
                    "����:" + findRow["OTHER_NAME"].ToString() + "\n" +
                    "���:" + findRow["SPECS"].ToString() + " ����:" + findRow["DOSE_CODE"].ToString();
            }
        }

        private void ucPopSelected_Load(object sender, EventArgs e)
        {
            this.fpSpread1.Select();
            this.fpSpread1.Focus();
        }

        #region IChooseItemForOutpatient ��Ա


        public FS.HISFC.BizProcess.Interface.FeeInterface.IGetSiItemGrade IGetSiItemGrade
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public FS.HISFC.Models.Registration.Register RegPatientInfo
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        #endregion
    }
}
