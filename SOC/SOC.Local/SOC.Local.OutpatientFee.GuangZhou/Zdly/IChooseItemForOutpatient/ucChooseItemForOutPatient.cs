using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
namespace FS.SOC.Local.OutpatientFee.GuangZhou.Zdly.IChooseItemForOutpatient
{
    /// <summary>
    /// �����շ���Ŀ�б�
    /// </summary>
    public partial class ucChooseItemForOutPatient : UserControl, FS.HISFC.BizProcess.Integrate.FeeInterface.IChooseItemForOutpatient
    {
        public ucChooseItemForOutPatient()
        {
            InitializeComponent();

            this.ckCustomCode.CheckedChanged += new EventHandler(ckCustomCode_CheckedChanged);
            this.ckSpellCode.CheckedChanged += new EventHandler(ckCustomCode_CheckedChanged);
            this.ckWBCode.CheckedChanged += new EventHandler(ckCustomCode_CheckedChanged);
            this.cbFilter.CheckedChanged += new EventHandler(ckCustomCode_CheckedChanged);
        }

        void ckCustomCode_CheckedChanged(object sender, EventArgs e)
        {

            string fileName = FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSettingOutpatientFee.xml";
            if (((Control)sender).Name == this.ckCustomCode.Name && this.ckCustomCode.Checked)
            {
                SOC.Public.XML.SettingFile.SaveSetting(fileName, "����" + FS.FrameWork.Management.Connection.Operator.Name, "currentmodel", "2");
            }
            else if (((Control)sender).Name == this.ckSpellCode.Name && this.ckSpellCode.Checked)
            {
                SOC.Public.XML.SettingFile.SaveSetting(fileName, "����" + FS.FrameWork.Management.Connection.Operator.Name, "currentmodel", "0");
            }
            else if (((Control)sender).Name == this.ckWBCode.Name && this.ckWBCode.Checked)
            {
                SOC.Public.XML.SettingFile.SaveSetting(fileName, "����" + FS.FrameWork.Management.Connection.Operator.Name, "currentmodel", "1");
            }
            else if (((Control)sender).Name == this.cbFilter.Name)
            {
                SOC.Public.XML.SettingFile.SaveSetting(fileName, "ģ����ѯ" + FS.FrameWork.Management.Connection.Operator.Name, "currentmodel", this.cbFilter.Checked ? "1" : "0");
            }

            this.QuerySortString();
        }

        #region ˽�г�Ա
        FS.HISFC.BizProcess.Integrate.FeeInterface.ChooseItemTypes chooseItemType = FS.HISFC.BizProcess.Integrate.FeeInterface.ChooseItemTypes.ItemChanging;
        string deptCode = ""; 
        System.Data.DataTable dtItem = null;
        FS.FrameWork.Public.ObjectHelper minFeeHelp = new FS.FrameWork.Public.ObjectHelper();
        FS.FrameWork.Public.ObjectHelper deptHelp = new FS.FrameWork.Public.ObjectHelper();
        FS.HISFC.Models.Base.ItemKind itemKind = FS.HISFC.Models.Base.ItemKind.All;
        System.Data.DataView dvItem = null;

        FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrage = new FS.HISFC.BizProcess.Integrate.Pharmacy();
        bool isSelectAndClose = true;
        /// <summary>
        /// ������ַ���
        /// </summary>
        protected string inputChar = string.Empty;

        /// <summary>
        /// ��ѯ��ʽ,Ĭ��ƴ��
        /// </summary>
        protected FS.HISFC.Models.Base.InputTypes inputType = FS.HISFC.Models.Base.InputTypes.Spell;

        //{ED51E97B-B752-4c32-BD93-F80209A24879}���������������

        /// <summary>
        /// ����doc
        /// </summary>
        static XmlDocument xSortDoc = null;
        /// <summary>
        /// ������
        /// </summary>
        string strSortString = "";
        /// <summary>
        /// �����ļ�·��
        /// </summary>
        private string filePath = Application.StartupPath + @".\profile\�����շ���Ŀ¼��ѡ��.xml";

        #endregion   

        #region ˽�к��� 
        /// <summary>
        /// ��ʼ����ʾ����Ϣ
        /// </summary>
        private void InitDataTable()
        {
            this.fpSpread1_Sheet1.DataAutoSizeColumns = false;

            // ����
            if (System.IO.File.Exists(filePath))
            {
                dtItem = new DataTable("�б�");
                dvItem = new DataView(dtItem);
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(this.filePath, dtItem, ref dvItem, this.fpSpread1_Sheet1);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1_Sheet1, this.filePath);
            }
            else
            {
                Type str = typeof(string);
                Type dec = typeof(decimal);
                Type bl = typeof(bool);
                dtItem = new DataTable("�б�");
                
                dtItem.Columns.AddRange(new DataColumn[]{   new DataColumn("�Զ�����", str),
                                                        new DataColumn("��Ŀ����", str),
                                                        new DataColumn("�������", str),
														new DataColumn("���", str),
														new DataColumn("�۸�", str),
                                                        new DataColumn("���", str),
														new DataColumn("��λ", str), 
                                                        new DataColumn("ִ�п���", str), 
														new DataColumn("ƴ����", str),
                                                        new DataColumn("�����", str),
                                                        new DataColumn("����", str),
                                                        new DataColumn("�Ƿ�ҩƷ",str),
                                                        new DataColumn("ҽ���ȼ�",str),
                                                        new DataColumn("����", str),
                                                        new DataColumn("����ƴ����", str),
                                                        new DataColumn("���������", str),
                                                        new DataColumn("�����Զ�����", str),   //{E91E0D33-FCC6-4982-BA74-320A6E8A373C}
                                                            //{ED51E97B-B752-4c32-BD93-F80209A24879}���������������
                                                        new DataColumn("sort", dec)
                                                            //{ED51E97B-B752-4c32-BD93-F80209A24879}�������
															
                                                        });
                this.fpSpread1_Sheet1.RowCount = 0;
                dvItem = new DataView(dtItem);
                this.fpSpread1_Sheet1.DataSource = dvItem;

                //{ED51E97B-B752-4c32-BD93-F80209A24879}���������������
                this.fpSpread1_Sheet1.Columns[(int)Cols.Sort].Visible = false;
                //{ED51E97B-B752-4c32-BD93-F80209A24879}��������������

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1_Sheet1, filePath);
            }
        } 
        #endregion  

        #region IChooseItemForOutpatient ��Ա

        public FS.HISFC.BizProcess.Integrate.FeeInterface.ChooseItemTypes ChooseItemType
        {
            get
            {
                return chooseItemType;
            }
            set
            {
                //chooseItemType = value;
            }
        }

        public string DeptCode
        {
            get
            {
                return deptCode;
            }
            set
            {
                deptCode = value;
            }
        }

        public int GetSelectedItem(ref FS.HISFC.Models.Base.Item item)
        {
            if (fpSpread1_Sheet1.RowCount == 0)
            {
                return 1;
            }
            if (item == null)
            {
                return 1;
            }
            item.ID = this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.ItemCode].Text;
            string drugFlag = fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.DrugFlag].Text;
            if (drugFlag == "1")
            {
                //item.IsPharmacy = false;
                item.ItemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;
            }
            return 1;
        }

        public int GetSelectedItem()
        {
            if (fpSpread1_Sheet1.RowCount == 0)
            {
                return 1;
            }
            string itemCode = this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.ItemCode].Text;
            ////{ED51E97B-B752-4c32-BD93-F80209A24879}���������������
            //Common.Classes.Function.SetSortItemXML(itemCode);
            ////{ED51E97B-B752-4c32-BD93-F80209A24879}��������������
            string drugFlag = fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.DrugFlag].Text;
            string exeDept = deptHelp.GetID(this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.ExeDept].Text);
            //�����շ� {40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
            //SelectedItem(itemCode, drugFlag, exeDept);
            decimal price = FrameWork.Function.NConvert.ToDecimal(fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.UintPrice].Text);
            SelectedItem(itemCode, drugFlag, exeDept, price);

            if (isSelectAndClose)
            {
                this.Visible = false;
            }
            return 1;
        }

        public int Init()
        {
            this.fpSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            InitDataTable();
            #region ��С����
            FS.HISFC.BizProcess.Integrate.Manager managerMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            ArrayList minFeeList = managerMgr.GetConstantList(FS.HISFC.Models.Base.EnumConstant.MINFEE);
            if (minFeeList == null)
            {
                MessageBox.Show("��ȡ��С����ʧ�ܣ�");
                return -1;
            }
            minFeeHelp.ArrayObject = minFeeList;
            #endregion 

            #region ����
            ArrayList deptList = managerMgr.GetDeptmentAllValid();
            if (deptList == null)
            {
                MessageBox.Show("��ȡ�����б�ʧ��");
                return -1;
            }
            deptHelp.ArrayObject = deptList;
            #endregion 
            this.fpSpread1_Sheet1.Columns[(int)Cols.ItemCode].Visible = false;
            this.fpSpread1_Sheet1.Columns[(int)Cols.WBCode].Visible = false;
            this.fpSpread1_Sheet1.Columns[(int)Cols.DrugFlag].Visible = false;

            //����ҽ�������ʾ
            this.fpSpread1_Sheet1.Columns[(int)Cols.ItemGrade].Visible = false;

            ////{ED51E97B-B752-4c32-BD93-F80209A24879}���������������
            //xSortDoc = Common.Classes.Function.GetSortXML();

            //Common.Classes.Function.xmlDoc = xSortDoc;
            //{ED51E97B-B752-4c32-BD93-F80209A24879}��������������

            this.strSortString = QuerySortString();

            this.Visible = false;
            return 1;
        }

        private string QuerySortString()
        {
            this.ckCustomCode.CheckedChanged -= new EventHandler(ckCustomCode_CheckedChanged);
            this.ckSpellCode.CheckedChanged -= new EventHandler(ckCustomCode_CheckedChanged);
            this.ckWBCode.CheckedChanged -= new EventHandler(ckCustomCode_CheckedChanged);
            this.cbFilter.CheckedChanged -= new EventHandler(ckCustomCode_CheckedChanged);

            string strTemp = "";
            string fileName = FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSettingOutpatientFee.xml";

            if (!System.IO.File.Exists(fileName))
            {
                SOC.Public.XML.SettingFile.SaveSetting(fileName, "����" + FS.FrameWork.Management.Connection.Operator.Name, "currentmodel", "0");
                SOC.Public.XML.SettingFile.SaveSetting(fileName, "ģ����ѯ" + FS.FrameWork.Management.Connection.Operator.Name, "currentmodel", "0");

            }
            int iModel = 0;

            iModel = FS.FrameWork.Function.NConvert.ToInt32(SOC.Public.XML.SettingFile.ReadSetting(fileName, "ģ����ѯ" + FS.FrameWork.Management.Connection.Operator.Name, "currentmodel", "1"));
            if (iModel == 0)
            {
                cbFilter.Checked = false;
            }
            else
            {
                cbFilter.Checked = true;
            }


            iModel = FS.FrameWork.Function.NConvert.ToInt32(SOC.Public.XML.SettingFile.ReadSetting(fileName, "����" + FS.FrameWork.Management.Connection.Operator.Name, "currentmodel", "0"));

            ckSpellCode.Checked = false;
            ckWBCode.Checked = false;
            ckCustomCode.Checked = false;
            switch (iModel)
            {
                case 0://ƴ��
                    strTemp = "ƴ���� ASC";
                    ckSpellCode.Checked = true;
                    break;
                case 1://���
                    strTemp = "����� ASC";
                    ckWBCode.Checked = true;
                    break;
                case 2://�Զ���
                    strTemp = "�Զ����� ASC";
                    ckCustomCode.Checked = true;
                    break;
                default:
                    strTemp = "ƴ���� ASC";
                    ckSpellCode.Checked = true;
                    break;
            }

            if (string.IsNullOrEmpty(strTemp))
            {
                dvItem.Sort = "sort asc, ƴ���� ASC";
            }
            else
            {
                dvItem.Sort = "sort asc," + strTemp;
            }

            this.ckCustomCode.CheckedChanged += new EventHandler(ckCustomCode_CheckedChanged);
            this.ckSpellCode.CheckedChanged += new EventHandler(ckCustomCode_CheckedChanged);
            this.ckWBCode.CheckedChanged += new EventHandler(ckCustomCode_CheckedChanged);
            this.cbFilter.CheckedChanged += new EventHandler(ckCustomCode_CheckedChanged);


            return strTemp;
        }

        public string InputPrev
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

        public bool IsJudgeStore
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

        public bool IsQueryLike
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

        public bool IsSelectAndClose
        {
            get
            {
                return true;
            }
            
        }

        public bool IsSelectItem
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

        public int ItemCount
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

        public FS.HISFC.Models.Base.ItemKind ItemKind
        {
            get
            {
                return itemKind;
            }
            set
            {
                itemKind = value;
            }
        }

        public void NextPage()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void NextRow()
        {

            if (this.fpSpread1_Sheet1.RowCount == 0)
            {
                return;
            }
            int RowNum = fpSpread1_Sheet1.ActiveRowIndex;
            //if (RowNum > 9)
            //{
            //    fpSpread1.SetViewportTopRow(0, fpSpread1_Sheet1.ActiveRowIndex - 9);
            //}
            if (RowNum < this.fpSpread1_Sheet1.RowCount-1)
            {
                RowNum++;
                fpSpread1_Sheet1.ActiveRowIndex = RowNum;
                //fpSpread1_Sheet1.AddSelection(RowNum, 0, 1, 1);
                //fpSpread1_Sheet1.SetActiveCell(RowNum, 0);
                fpSpread1.ShowRow(0, RowNum, FarPoint.Win.Spread.VerticalPosition.Nearest);
            }

            this.fpSpread1_SelectionChanged(this, null);
        }

        public FS.HISFC.Models.Base.Item NowItem
        {
            get
            {
                //if (this.fpSpread1_Sheet1.RowCount == 0)
                //{
                //    return new FS.HISFC.Models.Base.Item(); ;
                //}

                //FS.HISFC.Models.Base.Item item = new FS.HISFC.Models.Base.Item();
                //item.ID = this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.ItemCode].Text;
                //item.Name = this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.ItemName].Text;
                //string DrugFlag = fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.DrugFlag].Text;
                //if (DrugFlag == "0")
                //{
                //    item.IsPharmacy = false;
                //}
                //else if (DrugFlag == "1")
                //{
                //    item.IsPharmacy = true;
                //}
                //return item;
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public object ObjectFilterObject
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

        public void PriorPage()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void PriorRow()
        {
            if (this.fpSpread1_Sheet1.RowCount == 0)
            {
                return;
            }
            int RowNum = fpSpread1_Sheet1.ActiveRowIndex;
            //if (RowNum > 9)
            //{
            //    fpSpread1.SetViewportTopRow(0, fpSpread1_Sheet1.ActiveRowIndex - 9);
            //}
            if (RowNum > 0 )
            {
                RowNum--;
                fpSpread1_Sheet1.ActiveRowIndex = RowNum;
                //fpSpread1_Sheet1.AddSelection(RowNum, 0, 1, 1);
                //fpSpread1_Sheet1.SetActiveCell(RowNum, 0);
                fpSpread1.ShowRow(0, RowNum, FarPoint.Win.Spread.VerticalPosition.Nearest);
            }

            this.fpSpread1_SelectionChanged(this, null);
        }

        public string QueryType
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

        public event FS.HISFC.BizProcess.Integrate.FeeInterface.WhenGetItem SelectedItem;

        public void SetDataSet(DataSet dsItem)
        {
            if (dsItem == null)
            {
                return;
            }
            if (dsItem.Tables.Count == 0)
            {
                return;
            }
            //{E91E0D33-FCC6-4982-BA74-320A6E8A373C}
            dtItem.Clear();
            FS.HISFC.Models.Base.Employee employee =FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

            foreach (DataRow row in dsItem.Tables[0].Rows)
            {
                if (itemKind == FS.HISFC.Models.Base.ItemKind.Pharmacy)
                {
                    if(row["DRUG_FLAG"].ToString() ==  "0")
                    continue;
                }
                if (itemKind == FS.HISFC.Models.Base.ItemKind.Undrug)
                {
                    if (row["DRUG_FLAG"].ToString() == "1")
                        continue;
                }

                DataRow IRow = dtItem.NewRow();
                IRow["��Ŀ����"] = row["ITEM_NAME"];
                IRow["�������"] = minFeeHelp.GetName(row["FEE_CODE"].ToString()); 
                IRow["���"] = row["SPECS"];
                if (Function.IsContainYKDept(employee.Dept.ID))//������˿�����
                {
                    IRow["�۸�"] = row["SP_PRICE"];//��ʾ�����
                }
                else
                {
                    IRow["�۸�"] = row["UNIT_PRICE"];//��ʾ�����۸�
                }
                IRow["��λ"] = row["PACK_UNIT"];
                IRow["���"] = row["NOW_STORE"];
                IRow["ִ�п���"] = deptHelp.GetName(row["EXE_DEPT"].ToString());
                IRow["ƴ����"] = row["SPELL_CODE"];
                IRow["�Զ�����"] = row["USER_CODE"];
                IRow["�����"] = row["WB_CODE"]; 
                IRow["����"] = row["ITEM_CODE"];
                IRow["�Ƿ�ҩƷ"] = row["DRUG_FLAG"];

                IRow["�����Զ�����"] = row["CUS_USER_CODE"];
                IRow["����"] = row["cus_name"];
                IRow["����ƴ����"] = row["cus_spell_code"];
                IRow["���������"] = row["cus_wb_code"];
                //{ED51E97B-B752-4c32-BD93-F80209A24879}���������������
                if (dsItem.Tables[0].Columns.Contains("SORT_ID"))
                {
                    IRow["sort"] = row["SORT_ID"];//Common.Classes.Function.GetSortValue(xSortDoc, row["ITEM_CODE"].ToString());
                }
                //{ED51E97B-B752-4c32-BD93-F80209A24879}��������������
                //{E91E0D33-FCC6-4982-BA74-320A6E8A373C}
                #region
                string strGrage = string.Empty;
                if (iGetSiItemGrade != null)
                {
                    if (this.regPatientInfo == null)
                    {
                        this.iGetSiItemGrade.GetSiItemGrade(row["ITEM_CODE"].ToString(), ref strGrage);
                    }
                    else
                    {
                        this.iGetSiItemGrade.GetSiItemGrade(this.regPatientInfo.Pact.ID, row["ITEM_CODE"].ToString(), ref strGrage);
                    }
                    strGrage = FS.HISFC.BizLogic.Fee.Interface.ShowItemGradeByCode(strGrage);

                }
                else
                {
                    strGrage = "�Է�";
                }
                IRow["ҽ���ȼ�"] = strGrage;
                #endregion
                dtItem.Rows.Add(IRow);
                //dtItem.Rows.Add(IRow);
            }

            this.fpSpread1_Sheet1.Columns[(int)Cols.NowStore].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.fpSpread1_Sheet1.Columns[(int)Cols.PackUnit].Visible = false;
            this.fpSpread1_Sheet1.Columns[(int)Cols.UintPrice].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
        }

        public void SetInputChar(object sender, string inputChar, FS.HISFC.Models.Base.InputTypes inputType)
        {
            if (!this.Visible)
            {
                this.Show();
            }

            this.Show();

            this.fpSpread1_SelectionChanged(this, null);

            this.BringToFront();

            this.SetFilter(inputChar);
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="inputChar">�����ַ���</param>
        private void SetFilter(string inputChar)
        {
             FS.FrameWork.Management.ControlParam controler = new FS.FrameWork.Management.ControlParam();

            string filterString = string.Empty;
            //��ͨ�������ת��
            if (inputChar.Length > 0)
            {
                inputChar = inputChar.Replace("]", "&#@&");
                inputChar = inputChar.Replace("[", "[[]");
                inputChar = inputChar.Replace("&#@&", "[]]");
                inputChar = inputChar.Replace("*", "[*]");
                inputChar = inputChar.Replace("%", "[%]");
            }

            if (inputChar == string.Empty)
            {
                filterString = "1 = 1";
            }
            else if(!cbFilter.Checked)
            {
                filterString = "(ƴ���� LIKE '" + inputChar + "%') OR " + "(��Ŀ���� LIKE '" + inputChar + "%') OR " + "(�Զ����� LIKE '" + inputChar + "%') " + " or (����� LIKE '" + inputChar + "%') " + " or (���� LIKE '" + inputChar + "%') ";
            }
            else if (cbFilter.Checked)
            {
                filterString = "(ƴ���� LIKE '%" + inputChar + "%') OR " + "(��Ŀ���� LIKE '%" + inputChar + "%') OR " + "(�Զ����� LIKE '%" + inputChar + "%') " + " or (����� LIKE '%" + inputChar + "%') " + " or (��������� LIKE '%" + inputChar + "%') " + " or (����ƴ���� LIKE '%" + inputChar + "%') " + " or (�����Զ����� LIKE '%" + inputChar + "%') ";
            }

            dvItem.RowFilter = filterString;


            //���õ�һ��Ϊ���.gmz(2011-07-28)
            this.fpSpread1_Sheet1.ActiveRowIndex = 0;
            this.fpSpread1_Sheet1.AddSelection(0, 0, 1, 1);
            this.fpSpread1.ShowRow(0, 0, FarPoint.Win.Spread.VerticalPosition.Nearest);

        }
        public void SetLocation(Point p)
        {
            this.Location = p;
        }

        #endregion

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData.GetHashCode() == Keys.Enter.GetHashCode())
            {
                if (this.fpSpread1_Sheet1.RowCount == 0)
                {
                    return false;
                }
                string itemCode = this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.ItemCode].Text;
                string drugFlag = fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.DrugFlag].Text;
                string exeDept = deptHelp.GetID(this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.ExeDept].Text);
                //�����շ�{40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
                //SelectedItem(itemCode, drugFlag, exeDept);
                decimal price = FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.UintPrice].Text);
                SelectedItem(itemCode, drugFlag, exeDept, price);

                if (isSelectAndClose)
                {
                    this.Visible = false;
                }
            }
            return base.ProcessDialogKey(keyData);
        }
        private enum Cols
        {
            UsageCode,//�Զ�����
            ItemName,//��Ŀ����
            FeeCode,//ϵͳ���
            Spaces, //���
            UintPrice,//�۸�
            NowStore,//�����
            PackUnit,//��λ 
            ExeDept,//ִ�п���
            SpellCode,//ƴ����
            WBCode,//�����
            ItemCode,//����
            DrugFlag,
            ItemGrade,  
            Sort,
            CusItemName,
            CusSpellCode,
            CusWBCode,
            CusUsageCode
        }

        #region IChooseItemForOutpatient ��Ա


        bool FS.HISFC.BizProcess.Integrate.FeeInterface.IChooseItemForOutpatient.IsSelectAndClose
        {
            get
            {
                return isSelectAndClose;
            }
            set
            {
                isSelectAndClose = value;
            }
        }

        #endregion

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string itemCode = this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.ItemCode].Text;
            string drugFlag = fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.DrugFlag].Text;
            string exeDept = deptHelp.GetID(this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.ExeDept].Text);
            //�����շ� {40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
            //SelectedItem(itemCode, drugFlag, exeDept);
            decimal price = FrameWork.Function.NConvert.ToDecimal(fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.UintPrice].Text);
            SelectedItem(itemCode, drugFlag, exeDept, price);

            if (isSelectAndClose)
            {
                this.Visible = false;
            }
        }

        private void fpSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            return;
        }

        //{E91E0D33-FCC6-4982-BA74-320A6E8A373C}
        #region ҽ������ʾ���
        FS.HISFC.Models.Registration.Register regPatientInfo = null;


        #region IChooseItemForOutpatient ��Ա


        /// <summary>
        /// ������Ϣ
        /// </summary>
        public FS.HISFC.Models.Registration.Register RegPatientInfo
        {
            get
            {
                return this.regPatientInfo;
            }
            set
            {
                this.regPatientInfo = value;
            }
        }

        /// <summary>
        /// ҽ���ȼ��ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.FeeInterface.IGetSiItemGrade iGetSiItemGrade = null;

        /// <summary>
        /// ҽ���ӿڱ���
        /// </summary>
        public FS.HISFC.BizProcess.Interface.FeeInterface.IGetSiItemGrade IGetSiItemGrade
        {
            get
            {
                return this.iGetSiItemGrade;
            }
            set
            {
                this.iGetSiItemGrade = value; ;
            }
        }

        #endregion

        //{32553EB6-EF4D-4c61-A63A-17B3C850FC51}
        private void fpSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;
            if (this.fpSpread1_Sheet1.RowCount == 0)
            {
                return;
            }

            string itemCode = this.fpSpread1_Sheet1.Cells[row, (int)Cols.ItemCode].Text;
            if (string.IsNullOrEmpty(itemCode))
            {
                return;
            }

           // GetDisplaySI(itemCode);

            string drugFlag = this.fpSpread1_Sheet1.Cells[row, (int)Cols.DrugFlag].Text;
            if (drugFlag.Trim() == "4")//Э������
            {
                this.neuGroupBox1.Visible = true;
                this.neuGroupBox1.Width = 233;

                List<FS.HISFC.Models.Pharmacy.Nostrum> drugCombList = this.pharmacyIntegrage.QueryNostrumDetail(itemCode);
                if (drugCombList == null)
                {
                    MessageBox.Show("���ҩƷЭ�鴦����ϸ����!" + this.pharmacyIntegrage.Err);

                    return;
                }

                this.neuSpread1_Sheet1.RowCount = 0;
                this.neuSpread1_Sheet1.RowCount = drugCombList.Count;

                for (int i = 0; i < drugCombList.Count; i++)
                {
                    FS.HISFC.Models.Pharmacy.Nostrum n = drugCombList[i];

                    this.neuSpread1_Sheet1.Cells[i, 0].Text = n.Item.Name;
                    this.neuSpread1_Sheet1.Cells[i, 1].Text = n.Item.Price.ToString();
                    this.neuSpread1_Sheet1.Cells[i, 2].Text = n.Qty.ToString();
                }
            }
            else
            {
                this.neuGroupBox1.Visible = false;
                this.neuGroupBox1.Width = 0;
            }
        }
        #endregion

        private void fpSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1_Sheet1, filePath);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
