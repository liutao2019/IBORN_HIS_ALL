//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.Data;
//using System.Text;
//using System.Windows.Forms;

//namespace FS.HISFC.Components.Manager.Items
//{
//    public partial class undrugztinfo : FS.FrameWork.WinForms.Controls.ucBaseControl
//    {
//        public delegate void myEventDelegate();
//        public DataView dataView = null;

//        private DataSet UndrugztinfoDataSet = null;
//        private DataView Undrugztinfodv = null;
//        private DataSet UndrugDataSet = null;
//        private ArrayList DepartMentList = null;
//        private ArrayList SystClassArrayList = null;
//        private DataSet UndrugztDataSet = null;
//        private DataView Undrugztdv = null;


//        public undrugztinfo()
//        {
//            InitializeComponent();
//        }

//        /// <summary>
//        /// ʵ������ҩƷ����
//        /// </summary>
//        /// <returns></returns>
//        private DataSet InitUndrugzt()
//        {
//            //��ҩƷ���ױ�
//            DataSet ds = null;
//            DataTable Table = null;
//            this.neuSpread1_Sheet1.Columns[0].Width = 60;
//            this.neuSpread1_Sheet1.Columns[1].Width = 180;
//            this.neuSpread1_Sheet1.Columns[2].Width = 60;
//            this.neuSpread1_Sheet1.Columns[3].Width = 50;
//            this.neuSpread1_Sheet1.Columns[4].Width = 50;
//            this.neuSpread1_Sheet1.Columns[5].Width = 50;
//            try
//            {
//                ds = new DataSet();
//                Table = new DataTable("��ҩƷ���ױ�");

//                DataColumn dataColumn1 = new DataColumn("���ױ���");
//                dataColumn1.DataType = typeof(System.String);
//                Table.Columns.Add(dataColumn1);

//                DataColumn dataColumn2 = new DataColumn("��������");
//                dataColumn2.DataType = typeof(System.String);
//                Table.Columns.Add(dataColumn2);

//                DataColumn dataColumn3 = new DataColumn("ϵͳ���");
//                dataColumn3.DataType = typeof(System.String);
//                Table.Columns.Add(dataColumn3);

//                DataColumn dataColumn4 = new DataColumn("ƴ����");
//                dataColumn4.DataType = typeof(System.String);
//                Table.Columns.Add(dataColumn4);

//                DataColumn dataColumn5 = new DataColumn("���");
//                dataColumn5.DataType = typeof(System.String);
//                Table.Columns.Add(dataColumn5);

//                DataColumn dataColumn6 = new DataColumn("������");
//                dataColumn6.DataType = typeof(System.String);
//                Table.Columns.Add(dataColumn6);

//                DataColumn dataColumn7 = new DataColumn("ִ�п��ұ���");
//                dataColumn7.DataType = typeof(System.String);
//                Table.Columns.Add(dataColumn7);

//                DataColumn dataColumn8 = new DataColumn("˳���");
//                dataColumn8.DataType = typeof(System.String);
//                Table.Columns.Add(dataColumn8);

//                DataColumn dataColumn9 = new DataColumn("ȷ�ϱ�־");
//                dataColumn9.DataType = typeof(System.String);
//                Table.Columns.Add(dataColumn9);

//                DataColumn dataColumn10 = new DataColumn("��Ч�Ա�־");
//                dataColumn10.DataType = typeof(System.String);
//                Table.Columns.Add(dataColumn10);

//                DataColumn dataColumn11 = new DataColumn("����������Ŀ");
//                dataColumn11.DataType = typeof(System.String);
//                Table.Columns.Add(dataColumn11);

//                ds.Tables.Add(Table);
//            }
//            catch (Exception ee)
//            {
//                string Error = ee.Message;
//                ds = null;
//            }
//            return ds;
//        }

//        /// <summary>
//        /// ��ʼ����ҩƷ�б�
//        /// </summary>
//        /// <returns></returns>
//        private DataSet InitUndrugDataSet()
//        {
//            DataSet ds = null;
//            try
//            {
//                ds = new DataSet();

//                DataTable Table = new DataTable("��ҩƷ�б�");

//                DataColumn dataColumn1 = new DataColumn("����"); //0
//                dataColumn1.DataType = typeof(System.String);
//                Table.Columns.Add(dataColumn1);


//                DataColumn dataColumn2 = new DataColumn("����"); //1
//                dataColumn2.DataType = typeof(System.String);
//                Table.Columns.Add(dataColumn2);

//                DataColumn dataColumn7 = new DataColumn("Ĭ�ϼ�");//2
//                dataColumn7.DataType = typeof(System.Decimal);
//                Table.Columns.Add(dataColumn7);

//                DataColumn dataColumn8 = new DataColumn("��ͯ��");//3
//                dataColumn8.DataType = typeof(System.Decimal);
//                Table.Columns.Add(dataColumn8);

//                DataColumn dataColumn9 = new DataColumn("�����");//4
//                dataColumn9.DataType = typeof(System.Decimal);
//                Table.Columns.Add(dataColumn9);


//                DataColumn dataColumn3 = new DataColumn("�Զ�����");//5
//                dataColumn3.DataType = typeof(System.String);
//                Table.Columns.Add(dataColumn3);

//                DataColumn dataColumn4 = new DataColumn("ƴ����");//6
//                dataColumn4.DataType = typeof(System.String);
//                Table.Columns.Add(dataColumn4);

//                DataColumn dataColumn5 = new DataColumn("�����");//7
//                dataColumn5.DataType = typeof(System.String);
//                Table.Columns.Add(dataColumn5);

//                DataColumn dataColumn6 = new DataColumn("���ʱ���");
//                dataColumn6.DataType = typeof(System.String);
//                Table.Columns.Add(dataColumn6);

//                ds.Tables.Add(Table);
//            }
//            catch (Exception ee)
//            {
//                string Error = ee.Message;
//                ds = null;
//            }
//            return ds;
//        }

//        /// <summary>
//        /// ��ҩƷ�б�
//        /// </summary>
//        /// <param name="List"></param>
//        /// <param name="Table"></param>
//        /// <param name="view"></param>
//        private void AddDataToUndrug(ArrayList List, DataTable Table, FarPoint.Win.Spread.SheetView view)
//        {
//            try
//            {
//                if (Table != null)
//                {
//                    Table.Clear();
//                }
//                if (List != null)
//                {
//                    foreach (FS.HISFC.Models.Fee.Item.Undrug info in List)
//                    {
//                        Table.Rows.Add(new object[] { info.Name, info.ID, info.Price, info.ChildPrice, info.SpecialPrice, info.UserCode, info.SpellCode, info.WBCode, info.GBCode });
//                    }
//                }
//            }
//            catch (Exception ee)
//            {
//                MessageBox.Show(ee.Message); //
//            }
//        }

//        /// <summary>
//        /// ����ҩƷ����
//        /// </summary>
//        /// <param name="List"></param>
//        /// <param name="Table"></param>
//        /// <param name="view"></param>
//        /// <returns></returns>
//        private bool AddDataToUndrugzt(ArrayList List, DataTable Table, FarPoint.Win.Spread.SheetView view)
//        {
//            bool Result = true;
//            try
//            {
//                if (Table != null)
//                {
//                    Table.Clear();
//                }
//                if (List != null)
//                {
//                    foreach (FS.HISFC.Models.Fee.Item.UndrugComb info in List)
//                    {
//                        //����һ��
//                        string valid = "";
//                        if (info.IsValid)
//                        {
//                            valid = "��Ч";
//                        }
//                        else if (info.IsValid)
//                        {
//                            valid = "��Ч";
//                        }
//                        else
//                        {
//                            valid = "����";
//                        }
//                        string confirmFlag = "";

//                        if (info.IsNeedConfirm)
//                        {
//                            confirmFlag = "��Ҫȷ��";
//                        }
//                        else
//                        {
//                            confirmFlag = "����Ҫȷ��";
//                        }
//                        string SpellFlag = "";
//                        if (info.User01 == "1")
//                        {
//                            SpellFlag = "��";
//                        }
//                        else if (info.User01 == "0")
//                        {
//                            SpellFlag = "��";
//                        }
//                        Table.Rows.Add(new object[] { info.ID, info.Name, GetSysClassName(info.SysClass.Name, SystClassArrayList), info.SpellCode, info.WBCode, info.UserCode, GetDepartMentName(info.ExecDept, DepartMentList), info.SortID, confirmFlag, valid, SpellFlag });
//                    }
//                    Table.AcceptChanges();
//                    //
//                    //����
//                    LockfpSpread1();
//                }

//            }
//            catch (Exception ee)
//            {
//                string Error = ee.Message;
//            }

//            return Result;
//        }

//        /// <summary>
//        /// ��ҩƷ������ϸ��
//        /// </summary>
//        /// <returns></returns>
//        private DataSet InitUndrugztinfo()
//        {
//            DataSet ds = null;
//            try
//            {
//                ds = new DataSet();
//                DataTable Table = new DataTable("��ҩƷ������ϸ��");

//                DataColumn dataColumn1 = new DataColumn("���ױ���");
//                dataColumn1.DataType = typeof(System.String);
//                Table.Columns.Add(dataColumn1);

//                DataColumn dataColumn2 = new DataColumn("��ҩƷ����");
//                dataColumn2.DataType = typeof(System.String);
//                Table.Columns.Add(dataColumn2);

//                DataColumn dataColumn3 = new DataColumn("��ҩƷ����");
//                dataColumn3.DataType = typeof(System.String);
//                Table.Columns.Add(dataColumn3);

//                DataColumn dataColumn8 = new DataColumn("��Ч");
//                dataColumn8.DataType = typeof(System.Boolean);
//                Table.Columns.Add(dataColumn8);

//                DataColumn dataColumn4 = new DataColumn("˳���");
//                dataColumn4.DataType = typeof(System.Int32);
//                Table.Columns.Add(dataColumn4);

//                DataColumn dataColumn5 = new DataColumn("ƴ����");
//                dataColumn5.DataType = typeof(System.String);
//                Table.Columns.Add(dataColumn5);

//                DataColumn dataColumn6 = new DataColumn("�����");
//                dataColumn6.DataType = typeof(System.String);
//                Table.Columns.Add(dataColumn6);

//                DataColumn dataColumn7 = new DataColumn("������");
//                dataColumn7.DataType = typeof(System.String);
//                Table.Columns.Add(dataColumn7);

//                DataColumn dataColumn9 = new DataColumn("����");
//                dataColumn9.DataType = typeof(System.Decimal);
//                Table.Columns.Add(dataColumn9);


//                ds.Tables.Add(Table);
//            }
//            catch (Exception ee)
//            {
//                string Error = ee.Message;
//                ds = null;
//            }
//            return ds;
//        }

//        /// <summary>
//        /// ����ҩƷ������ϸ��
//        /// </summary>
//        /// <param name="List"></param>
//        /// <param name="Table"></param>
//        /// <param name="view"></param>
//        /// <returns></returns>
//        private bool AddDataToUndrugztinfo(ArrayList List, DataTable Table, FarPoint.Win.Spread.SheetView view)
//        {
//            bool Result = false;
//            if (Table != null)
//            {
//                Table.Clear();
//            }

//            if (List != null)
//            {

//                foreach (FS.HISFC.Models.Fee.Item.UndrugComb info in List)
//                {
//                    bool Value = true;
//                    if (info.User01 == "1") //��Ч
//                    {
//                        Value = false;
//                    }
//                    Table.Rows.Add(new object[] { info.ID, info.itemName, info.itemCode, Value, info.SortID, info.SpellCode, info.WbCode, info.InputCode, info.Qty });
//                }
//                LockfpSpread2();
//                Result = true;
//                Table.AcceptChanges();
//            }
//            return Result;
//        }

//        /// <summary>
//        /// ����ϵͳ�������
//        /// </summary>
//        /// <returns></returns>
//        private string[] GetSysClass(ArrayList List)
//        {
//            //����ϵͳ�������
//            string[] Str = null;
//            try
//            {
//                if (List != null)
//                {
//                    int j = 0;
//                    for (int i = 0; i < List.Count; i++)
//                    {
//                        string StrTemp = ((FS.HISFC.Models.Base.EnumSysClass)List[i]).ID.ToString();
//                        if (StrTemp != "P" && StrTemp != "PCC" && StrTemp != "PCZ")
//                        {
//                            j++;
//                        }
//                    }
//                    if (j > 0)
//                    {
//                        Str = new string[j];
//                        int n = 0;
//                        for (int i = 0; i < List.Count; i++)
//                        {
//                            string StrTemp = ((FS.HISFC.Models.Base.EnumSysClass)List[i]).ID.ToString();
//                            if (StrTemp != "P" && StrTemp != "PCC" && StrTemp != "PCZ")
//                            {
//                                Str[n] = ((FS.HISFC.Models.Base.EnumSysClass)List[i]).Name;
//                                n++;
//                            }
//                        }
//                    }
//                }
//            }
//            catch (Exception ee)
//            {
//                string Error = ee.Message;
//            }
//            return Str;

//        }

//        /// <summary>
//        /// ���ݣɣĵõ�ϵͳ��������
//        /// </summary>
//        /// <param name="SysClassId"></param>
//        /// <param name="List"></param>
//        /// <returns></returns>
//        private string GetSysClassName(string SysClassId, ArrayList List)
//        {
//            if (List != null && SysClassId != "")
//            {
//                try
//                {
//                    foreach (FS.HISFC.Models.Base.EnumSysClass info in List)
//                    {
//                        if (SysClassId == Convert.ToString(((FS.HISFC.Models.Base.EnumSysClass)info).ID))
//                        {
//                            return ((FS.HISFC.Models.Base.EnumSysClass)info).Name;
//                        }
//                    }
//                }
//                catch (Exception ee)
//                {
//                    string Error = ee.Message;
//                }
//            }
//            return "";
//        }

//        /// <summary>
//        /// ����ϵͳ������Ƶõ�ϵͳ���ģɣ�
//        /// </summary>
//        /// <param name="SysClassName"></param>
//        /// <param name="List"></param>
//        /// <returns></returns>
//        private string GetSysClassId(string SysClassName, ArrayList List)
//        {
//            if (List != null && SysClassName != "")
//            {
//                try
//                {
//                    foreach (FS.HISFC.Models.Base.EnumSysClass info in List)
//                    {
//                        if (SysClassName == ((FS.HISFC.Models.Base.EnumSysClass)info).Name)
//                        {
//                            return Convert.ToString(((FS.HISFC.Models.Base.EnumSysClass)info).ID);
//                        }
//                    }
//                }
//                catch (Exception ee)
//                {
//                    string Error = ee.Message;
//                }
//            }

//            return "";
//        }
//        private string[] GetDepartMentStr(ArrayList List)
//        {
//            //���ؿ����б�
//            string[] Str = null;
//            try
//            {
//                if (List != null)
//                {
//                    Str = new string[List.Count];
//                    for (int i = 0; i < List.Count; i++)
//                    {
//                        Str[i] = ((FS.HISFC.Models.Base.Department)List[i]).DeptName;
//                    }

//                }
//            }
//            catch (Exception ee)
//            {
//                string Error = ee.Message;
//            }
//            return Str;
//        }
//        private string GetDepartMentName(string DepartmentCode, ArrayList List)
//        {
//            if (List != null && DepartmentCode != "")
//            {
//                try
//                {
//                    foreach (FS.HISFC.Models.Base.Department info in List)
//                    {
//                        if (DepartmentCode == info.DeptID)
//                        {
//                            //���ؿ������� ��Ӧ�ı���
//                            return info.DeptName;
//                        }
//                    }
//                }
//                catch (Exception ee)
//                {
//                    MessageBox.Show(ee.Message);
//                }
//            }
//            return "";
//        }
//        private string GetDepartMentId(string DepartmentName, ArrayList List)
//        {
//            if (List != null && DepartmentName != "")
//            {
//                try
//                {
//                    foreach (FS.HISFC.Models.Base.Department info in List)
//                    {
//                        if (DepartmentName == info.DeptName)
//                        {
//                            //���ر��� ��Ӧ�Ŀ�������
//                            return info.DeptID;
//                        }
//                    }
//                }
//                catch (Exception ee)
//                {
//                    string Error = ee.Message;
//                }
//            }
//            return "";
//        }

//        /// <summary>
//        /// ����������ϸ��
//        /// </summary>
//        /// <returns></returns>
//        private bool SaveUndrugztinfo()
//        {
//            //����������ϸ��
//            bool SaveSuccess = true;
//            try
//            {
//                //�����ӵ�
//                DataTable AddTable = UndrugztinfoDataSet.Tables[0].GetChanges(System.Data.DataRowState.Added);
//                //�޸ĵ�
//                DataTable ModTable = UndrugztinfoDataSet.Tables[0].GetChanges(System.Data.DataRowState.Modified);
//                //ɾ����
//                DataTable DelTable = UndrugztinfoDataSet.Tables[0].GetChanges(System.Data.DataRowState.Deleted);

//                FS.HISFC.BizProcess.Integrate.Fee undrugztinfo = new FS.HISFC.BizProcess.Integrate.Fee();

//                //FS.HISFC.BizLogic.Fee.undrugztinfo undrugztinfo = new FS.HISFC.Management.Fee.undrugztinfo();
//                FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(undrugztinfo.Connection);
//                trans.BeginTransaction();
//                undrugztinfo.SetTrans(trans.Trans);
//                if (AddTable != null)
//                {
//                    ArrayList List = GetUndruginfoChangeList(AddTable);
//                    foreach (FS.HISFC.Models.Fee.Item.UndrugComb info in List)
//                    {
//                        if (undrugztinfo.InsertUndrugComb(info) > 0)
//                        {
//                        }
//                        else
//                        {
//                            SaveSuccess = false;
//                            break;
//                        }
//                    }
//                }
//                if (ModTable != null && SaveSuccess)
//                {
//                    ArrayList List = GetUndruginfoChangeList(ModTable);
//                    foreach (FS.HISFC.Models.Fee.Item.UndrugComb info in List)
//                    {
//                        if (undrugztinfo.UpdateUndrugComb(info) > 0)
//                        {
//                        }
//                        else
//                        {
//                            SaveSuccess = false;
//                            break;
//                        }
//                    }
//                }
//                if (DelTable != null && SaveSuccess)
//                {
//                    DelTable.RejectChanges();
//                    ArrayList List = GetUndruginfoChangeList(DelTable);
//                    foreach (FS.HISFC.Object.Fee.Undrugztinfo info in List)
//                    {
//                        if (undrugztinfo.DeleteUndrugComb(info) > 0)
//                        {
//                        }
//                        else
//                        {
//                            SaveSuccess = false;
//                            break;
//                        }
//                    }
//                }
//                if (SaveSuccess)
//                {
//                    trans.Commit();
//                    UndrugztinfoDataSet.Tables[0].AcceptChanges();
//                }
//                else
//                {
//                    FS.FrameWork.Management.PublicTrans.RollBack();;
//                    MessageBox.Show(undrugztinfo.Err);
//                }
//            }
//            catch (Exception ee)
//            {
//                MessageBox.Show(ee.Message);
//                SaveSuccess = false;
//            }
//            return SaveSuccess;
//        }

//        private bool UndrugztChanged()
//        {
//            bool IsChange = false;
//            neuSpread1.StopCellEditing();
//            try
//            {
//                DataTable AddTable = UndrugztDataSet.Tables[0].GetChanges(System.Data.DataRowState.Added);
//                DataTable ModTable = UndrugztDataSet.Tables[0].GetChanges(System.Data.DataRowState.Modified);
//                DataTable DelTable = UndrugztDataSet.Tables[0].GetChanges(System.Data.DataRowState.Deleted);
//                if (AddTable != null || ModTable != null || DelTable != null)
//                {
//                    IsChange = true;
//                }
//            }
//            catch (Exception ee)
//            {
//                string Error = ee.Message;
//            }
//            return IsChange;
//        }

//        private bool UndrugztinfoChanged()
//        {
//            bool IsChange = false;
//            neuSpread2.StopCellEditing();
//            try
//            {
//                DataTable AddTable = UndrugztinfoDataSet.Tables[0].GetChanges(System.Data.DataRowState.Added);
//                DataTable ModTable = UndrugztinfoDataSet.Tables[0].GetChanges(System.Data.DataRowState.Modified);
//                DataTable DelTable = UndrugztinfoDataSet.Tables[0].GetChanges(System.Data.DataRowState.Deleted);
//                if (AddTable != null || ModTable != null || DelTable != null)
//                {
//                    IsChange = true;
//                }
//            }
//            catch (Exception ee)
//            {
//                string Error = ee.Message;
//            }
//            return IsChange;
//        }

//        private ArrayList GetUndrugChangeList(DataTable Table, ref bool Result)
//        {
//            ArrayList List = null;
//            try
//            {
//                if (Table != null)
//                {
//                    List = new ArrayList();
//                    FS.HISFC.Models.Fee.Item.UndrugComb info = null;
//                    foreach (DataRow row in Table.Rows)
//                    {
//                        info = new FS.HISFC.Models.Fee.Item.UndrugComb();
//                        info.ID = row["���ױ���"].ToString();

//                        info.Name = row["��������"].ToString();

//                        info.SysClass = GetSysClassId(row["ϵͳ���"].ToString(), SystClassArrayList);

//                        info.SpellCode = row["ƴ����"].ToString();

//                        info.WBCode = row["���"].ToString();

//                        info.UserCode = row["������"].ToString();

//                        info.ExecDept = GetDepartMentId(row["ִ�п��ұ���"].ToString(), DepartMentList);
//                        if (row["˳���"] != DBNull.Value)
//                        {
//                            info.SortID = Convert.ToInt32(row["˳���"]);
//                        }
//                        else
//                        {
//                            MessageBox.Show("������˳���");
//                            Result = false;
//                        }
//                        if (row["ȷ�ϱ�־"].ToString() == "��Ҫȷ��")
//                        {
//                            info.IsNeedConfirm = true;
//                        }
//                        else
//                        {
//                            info.IsNeedConfirm = false;
//                        }
//                        if (row["��Ч�Ա�־"] != DBNull.Value)
//                        {
//                            if (row["��Ч�Ա�־"].ToString() == "��Ч")
//                            {
//                                info.ValidState = "0";
//                            }
//                            else if (row["��Ч�Ա�־"].ToString() == "��Ч")
//                            {
//                                info.ValidState = "1";
//                            }
//                            else if (row["��Ч�Ա�־"].ToString() == "����")
//                            {
//                                info.ValidState = "2";
//                            }
//                            else
//                            {
//                                info.ValidState = "";
//                            }
//                        }
//                        else
//                        {
//                            MessageBox.Show("��ѡ����Ч�Ա�ʶ");
//                            Result = false;
//                            return null;
//                        }
//                        if (row["����������Ŀ"] != DBNull.Value)
//                        {
//                            if (row["����������Ŀ"].ToString() == "��")
//                            {
//                                info.User02 = "1";
//                            }
//                            else if (row["����������Ŀ"].ToString() == "��")
//                            {
//                                info.User02 = "0";
//                            }
//                        }
//                        List.Add(info);
//                        info = null;
//                    }
//                }
//            }
//            catch (Exception ee)
//            {
//                MessageBox.Show(ee.Message);
//                Result = false;
//                return null;
//            }
//            return List;
//        }

//        /// <summary>
//        /// ��ȡ������ϸ��Ϣ
//        /// </summary>
//        /// <param name="Table"></param>
//        /// <returns></returns>
//        private ArrayList GetUndruginfoChangeList(DataTable Table)
//        {
//            ArrayList List = null;
//            try
//            {
//                List = new ArrayList();
//                if (Table != null)
//                {
//                    FS.HISFC.Models.Fee.Item.UndrugComb info = null;
//                    foreach (DataRow row in Table.Rows)
//                    {
//                        info = new FS.HISFC.Models.Fee.Item.UndrugComb();
//                        info.ID = row["���ױ���"].ToString();
//                        info.itemName = row["��ҩƷ����"].ToString();
//                        info.itemCode = row["��ҩƷ����"].ToString();
//                        info.SortID = Convert.ToInt32(row["˳���"]);
//                        info.SpellCode = row["ƴ����"].ToString();
//                        info.WBCode = row["�����"].ToString();
//                        info.UserCode = row["������"].ToString();
//                        string str = row["��Ч"].ToString();
//                        if (row["��Ч"].ToString() == "False")
//                        {
//                            info.User01 = "1";
//                        }
//                        else
//                        {
//                            info.User01 = "0";
//                        }
//                        //����
//                        info.Qty = FS.neuFC.Function.NConvert.ToDecimal(row["����"].ToString());
//                        List.Add(info);
//                        info = null;
//                    }
//                }
//            }
//            catch (Exception ee)
//            {
//                string Error = ee.Message;
//            }
//            return List;
//        }


//        public void ShowNewData()
//        {
//            try
//            {
//                int temp = neuSpread1_Sheet1.ActiveRowIndex;
//                string packageCode = "";
//                try
//                {
//                    FS.HISFC.BizProcess.Integrate.Fee undrugzt = new FS.HISFC.BizProcess.Integrate.Fee();
//                    packageCode = neuSpread1.Cells[temp, 0].Text;
//                    FS.HISFC.Models.Fee.Item.UndrugComb itemzt = Undrugszinfo.GetUndrugztinfo(packageCode);
//                    ArrayList UndrugztinfoList = new ArrayList();
//                    UndrugztinfoList.Add(itemzt);
//                    AddDataToUndrugztinfo(UndrugztinfoList, UndrugztinfoDataSet.Tables[0], fpSpread2_Sheet1);
//                }
//                catch (Exception ee)
//                {
//                    string Error = ee.Message;
//                }
//            }
//            catch (Exception ee)
//            {
//                string Error = ee.Message;
//            }
//        }

//        private void InsertInfo()
//        {
//            if (MessageBox.Show("���Ҫ���������", "�������", System.Windows.Forms.MessageBoxButtons.YesNo) == DialogResult.Yes)
//            {
//                AddUndrugzt();
//            }
//        }
//        private void DeleteInfo()
//        {
//            //ͣ�÷�ҩƷ������ϸ
//            try
//            {
//                if (neuSpread2_Sheet1.Rows.Count > 0)
//                {
//                    string Name = neuSpread2_Sheet1.Cells[neuSpread2_Sheet1.ActiveRowIndex, 1].Text;
//                    DialogResult result;
//                    result = MessageBox.Show("�Ƿ�Ҫͣ��" + Name, "ͣ��", MessageBoxButtons.YesNo);
//                    if (result == DialogResult.Yes)
//                    {
//                        neuSpread2_Sheet1.Cells[neuSpread2_Sheet1.ActiveRowIndex, 3].Value = false;
//                    }
//                }
//            }
//            catch (Exception ee)
//            {
//                MessageBox.Show(ee.Message);
//            }
//        }
//        private void RecoverInfo()
//        {
//            //ͣ�÷�ҩƷ������ϸ
//            try
//            {
//                if (neuSpread2_Sheet1.Rows.Count > 0)
//                {
//                    string Name = neuSpread2_Sheet1.Cells[neuSpread2_Sheet1.ActiveRowIndex, 1].Text;
//                    DialogResult result;
//                    result = MessageBox.Show("�Ƿ�Ҫ�ָ�" + Name, "�ָ�", MessageBoxButtons.YesNo);
//                    if (result == DialogResult.Yes)
//                    {
//                        neuSpread2_Sheet1.Cells[neuSpread2_Sheet1.ActiveRowIndex, 3].Value = true;
//                    }
//                }
//            }
//            catch (Exception ee)
//            {
//                MessageBox.Show(ee.Message);
//            }
//        }
//        private void PrintInfo()
//        {
//            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
//            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
//            p.PrintPreview(panel7);
//        }
//        private void ExportData()
//        {
//            string Result = "";
//            try
//            {
//                bool ret = false;
//                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
//                saveFileDialog1.Filter = "Excel |.xls";
//                saveFileDialog1.Title = "��������";
//                try
//                {
//                    saveFileDialog1.FileName = neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.ActiveRowIndex, 1].Text;
//                }
//                catch (Exception)
//                {
//                    saveFileDialog1.FileName = "";
//                }
//                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
//                {
//                    if (saveFileDialog1.FileName != "")
//                    {
//                        //��Excel ����ʽ��������
//                        UndrugztDataSet.Tables[0].AcceptChanges();
//                        fpSpread1.StopCellEditing();
//                        ret = fpSpread2.SaveExcel(saveFileDialog1.FileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
//                    }
//                    if (ret)
//                    {
//                        MessageBox.Show("�ɹ���������");
//                    }
//                }
//                else
//                {
//                    MessageBox.Show("������ȡ��");
//                }
//            }
//            catch (Exception ee)
//            {
//                Result = ee.Message;
//                MessageBox.Show(Result);
//            }

//        }
//        private void ExistForm()
//        {
//            // ������µ����ݣ���ʾ����Ȼ�� �˳�
//            if (UndrugztChanged() || UndrugztinfoChanged())
//            {
//                DialogResult result;
//                result = MessageBox.Show("�Ƿ񱣴�ղŸĶ���������", "����", MessageBoxButtons.YesNo);
//                if (result == DialogResult.Yes)
//                {
//                    if (SaveUndrugztinfo())
//                    {
//                        MessageBox.Show("����ɹ���");
//                        this.FindForm().Close();
//                    }
//                    else
//                    {
//                        //���治�ɹ�
//                        MessageBox.Show("����ʧ��");
//                    }
//                }
//                else
//                {
//                    Form form = this.FindForm();
//                    form.Close();
//                }
//            }
//            else
//            {
//                Form form = this.FindForm();
//                form.Close();
//            }
//        }

//        /// <summary>
//        /// ��������
//        /// </summary>
//        /// <returns></returns>
//        public int AddUndrugzt()
//        {
//            frmUndrugztManager frm = new frmUndrugztManager();
//            frm.SaveInfo += new Fee.frmUndrugztManager.SaveHandle(frm_SaveInfo);
//            frm.EditType = FS.Common.Class.EditTypes.Add;
//            this.editType = FS.Common.Class.EditTypes.Add;
//            frm.ShowDialog();
//            return 0;
//        }
//        /// <summary>
//        /// �޸ĵ�ǰ��
//        /// </summary>
//        /// <returns></returns>
//        public int ModifyUndrugzt()
//        {
//            if (this.neuSpread1_Sheet1.RowCount == 0)
//            {
//                return -1;
//            }
//            frmUndrugztManager frm = new frmUndrugztManager();
//            frm.SaveInfo += new Fee.frmUndrugztManager.SaveHandle(frm_SaveInfo);
//            frm.EditType = FS.Common.Class.EditTypes.Modify;
//            this.editType = FS.Common.Class.EditTypes.Modify;
//            frm.InitList();
//            FS.HISFC.Management.Fee.undrugzt neuUndrugsz = new FS.HISFC.Management.Fee.undrugzt();
//            FS.HISFC.Object.Fee.Undrugzt obj = neuUndrugsz.GetSingleUndrugzt(this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 0].Text);
//            if (obj == null)
//            {
//                MessageBox.Show("��ȡ������Ϣʧ��");
//                return -1;
//            }
//            frm.SetValue(obj);
//            frm.ShowDialog();
//            return 0;
//        }


//    }
//}