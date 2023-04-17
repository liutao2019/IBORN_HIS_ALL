using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using System.Collections;

namespace FS.HISFC.Components.Material
{
    /// <summary>
    /// [��������: ���ʵ��ݲ���]
    /// [�� �� ��: yuyun]
    /// [����ʱ��: 2008-8-1]
    /// </summary>
    public partial class ucListReprint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region ���췽��

        public ucListReprint()
        {
            InitializeComponent();
        }

        #endregion        

        #region ����

        /// <summary>
        /// ���ʿ��ҵ����
        /// </summary>
        private FS.HISFC.BizLogic.Material.Store matManager = new FS.HISFC.BizLogic.Material.Store();

        /// <summary>
        /// ���ʻ���ҵ����
        /// </summary>
        private FS.HISFC.BizLogic.Material.Baseset baseManager = new FS.HISFC.BizLogic.Material.Baseset();

        /// <summary>
        /// ������ⵥ��ӡ����
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Material.IBillPrint iInPrint = null;

        /// <summary>
        /// ��ⵥ��ӡʵ��
        /// </summary>
        protected object inputInstance;

        /// <summary>
        /// ���ⵥ��ӡʵ��
        /// </summary>
        protected object outputInstance;

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Init()
        {
            if (this.DesignMode)
            {
                return;
            }

            this.dtpBeginTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(matManager.GetSysDateTime()).AddDays(-3);
            this.dtpEndTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(matManager.GetSysDateTime());
            this.SetStorageList();
            this.cmbStorage.Text = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.Name;
            this.cmbStorage.Tag = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
            this.SetSheet();
        }

        /// <summary>
        /// ����2����ӡ�ӿ�ʵ����������Ҫѡ���ӡ��ⵥ���ǳ��ⵥ
        /// </summary>
        protected virtual void SetPrintObject()
        {
            object[] o = new object[] { };

            try
            {
                //����ӿ�---ModifyBy zj--�����û�����ô�ӡ�ӿ�����Ĭ�ϵ�ʵ��
                ////����ⱨ��
                //FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                //string billValue = "FS.DongGuan.Report.Material.ucMatInputBill";
                //string billValue1 = "FS.DongGuan.Report.Material.ucMatOutputBill";
                //System.Runtime.Remoting.ObjectHandle objHandel = System.Activator.CreateInstance("DongGuan.Report", billValue, false, System.Reflection.BindingFlags.CreateInstance, null, o, null, null, null);
                //System.Runtime.Remoting.ObjectHandle objHandel1 = System.Activator.CreateInstance("DongGuan.Report", billValue1, false, System.Reflection.BindingFlags.CreateInstance, null, o, null, null, null);

                //inputInstance = objHandel.Unwrap();
                //outputInstance = objHandel1.Unwrap();
                //---���ýӿڷ��䷽��
                //
                inputInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.Material.In.ucMatIn), typeof(FS.HISFC.BizProcess.Interface.Material.IBillPrint));
                if (inputInstance == null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("û������������ⵥ��ӡ�ӿڣ�����Ĭ��ʵ�֣�"));
                    string billValue = "FS.Report.Material.ucMatInputBill";
                    System.Runtime.Remoting.ObjectHandle objHandel = System.Activator.CreateInstance("Report", billValue, false, System.Reflection.BindingFlags.CreateInstance, null, o, null, null, null);
                    inputInstance = objHandel.Unwrap();
                }
                outputInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.Material.Out.ucMatOut), typeof(FS.HISFC.BizProcess.Interface.Material.IBillPrint));
                if (outputInstance == null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("û���������ʳ��ⵥ��ӡ�ӿڣ�����Ĭ��ʵ�֣�"));
                    string billValue = "FS.Report.Material.ucMatOutputBill";
                    System.Runtime.Remoting.ObjectHandle objHandel = System.Activator.CreateInstance("Report", billValue, false, System.Reflection.BindingFlags.CreateInstance, null, o, null, null, null);
                    outputInstance = objHandel.Unwrap();
                }
            }
            catch (System.TypeLoadException ex)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(Language.Msg("�����ռ���Ч\n" + ex.Message));

                return;
            }
        }

        /// <summary>
        /// ���ñ���ʽ
        /// </summary>
        private void SetSheet()
        {

            #region ���1��ʽ����

            this.neuSpread1_Sheet1.DefaultStyle.Locked = true;
            this.neuSpread1_Sheet1.RowCount = 0;

            this.neuSpread1_Sheet1.Columns[(int)ColSheet1.ListNO].Label = "����";
            this.neuSpread1_Sheet1.Columns[(int)ColSheet1.ListNO].Width = 120F;

            this.neuSpread1_Sheet1.Columns[(int)ColSheet1.InvoiceNO].Label = "��Ʊ��";
            this.neuSpread1_Sheet1.Columns[(int)ColSheet1.InvoiceNO].Width = 120F;

            this.neuSpread1_Sheet1.Columns[(int)ColSheet1.OperType].Label = "��������";
            this.neuSpread1_Sheet1.Columns[(int)ColSheet1.OperType].Width = 80F;

            this.neuSpread1_Sheet1.Columns[(int)ColSheet1.TargetDept].Label = "Ŀ�굥λ";
            this.neuSpread1_Sheet1.Columns[(int)ColSheet1.TargetDept].Width = 120F;

            //this.neuSpread1_Sheet1.Columns[(int)ColSheet1.Operator].Label = "������";
            //this.neuSpread1_Sheet1.Columns[(int)ColSheet1.Operator].Width = 80F;

            this.neuSpread1_Sheet1.Columns[(int)ColSheet1.OperDate].Label = "����ʱ��";
            this.neuSpread1_Sheet1.Columns[(int)ColSheet1.OperDate].Width = 120F;

            this.neuSpread1_Sheet1.Columns[(int)ColSheet1.OperDate + 1, this.neuSpread1_Sheet1.ColumnCount - 1].Visible = false;

            #endregion

            #region ���2��ʽ����

            this.neuSpread2_Sheet1.DefaultStyle.Locked = true;
            this.neuSpread2_Sheet1.RowCount = 0;

            this.neuSpread2_Sheet1.Columns[(int)ColSheet2.ItemCode].Label = "��Ŀ����";
            this.neuSpread2_Sheet1.Columns[(int)ColSheet2.ItemCode].Width = 120F;

            this.neuSpread2_Sheet1.Columns[(int)ColSheet2.ItemName].Label = "��Ŀ����";
            this.neuSpread2_Sheet1.Columns[(int)ColSheet2.ItemName].Width = 120F;

            this.neuSpread2_Sheet1.Columns[(int)ColSheet2.Specs].Label = "���";
            this.neuSpread2_Sheet1.Columns[(int)ColSheet2.Specs].Width = 80F;

            this.neuSpread2_Sheet1.Columns[(int)ColSheet2.Qty].Label = "����";
            this.neuSpread2_Sheet1.Columns[(int)ColSheet2.Qty].Width = 120F;

            this.neuSpread2_Sheet1.Columns[(int)ColSheet2.Unit].Label = "��λ";
            this.neuSpread2_Sheet1.Columns[(int)ColSheet2.Unit].Width = 60F;

            this.neuSpread2_Sheet1.Columns[(int)ColSheet2.Price].Label = "����";
            this.neuSpread2_Sheet1.Columns[(int)ColSheet2.Price].Width = 80F;

            this.neuSpread2_Sheet1.Columns[(int)ColSheet2.Cost].Label = "���";
            this.neuSpread2_Sheet1.Columns[(int)ColSheet2.Cost].Width = 80F;

            this.neuSpread2_Sheet1.Columns[(int)ColSheet2.Cost + 1, this.neuSpread2_Sheet1.ColumnCount - 1].Visible = false;

            #endregion
        }

        /// <summary>
        /// ���ؿⷿ�����б�
        /// </summary>
        private void SetStorageList()
        {
            ArrayList alStorage = this.baseManager.GetStorageInfo();

            if (alStorage == null || alStorage.Count <= 0)
            {
                MessageBox.Show("���ؿⷿ��Ϣʧ��" + baseManager.Err);

                return;
            }
            this.cmbStorage.AddItems(alStorage);
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            ArrayList alListInfo = new ArrayList();
            if (this.cmbStorage.Tag == null || string.IsNullOrEmpty(this.cmbStorage.Tag.ToString()))
            {
                MessageBox.Show("��ѡ��ⷿ��");

                return -1;
            }
            this.Clear();
            //�ж��ǲ�����ⵥ���ǳ��ⵥ
            if (this.rbtInputList.Checked)
            {
                alListInfo = this.QueryInputList(this.dtpBeginTime.Value, this.dtpEndTime.Value, this.cmbStorage.Tag.ToString());
            }
            else if (this.rbtOutputList.Checked)
            {
                alListInfo = this.QueryOutputList(this.dtpBeginTime.Value, this.dtpEndTime.Value, this.cmbStorage.Tag.ToString());
            }
            else
            {
                MessageBox.Show("��ѡ�񵥾����ͣ�");

                return -1;
            }

            if (alListInfo != null && alListInfo.Count > 0)
            {
                this.AddDataToSheet(alListInfo);
            }

            return base.OnQuery(sender, neuObject);
        }

        public override int Print(object sender, object neuObject)
        {
            if (this.neuSpread2_Sheet1.RowCount == 0)
            {
                MessageBox.Show("��˫��ѡ��Ҫ����ĵ���");

                return -1;
            }
            //�ж�����ⵥ�����ǳ��ⵥ����
            if (this.neuSpread2_Sheet1.Cells[0,(int)ColSheet2.ItemCode].Tag.ToString() == "input")
            {
                Function.IPrint = inputInstance as FS.HISFC.BizProcess.Interface.Material.IBillPrint;
                List<FS.HISFC.Models.Material.Input> list = new List<FS.HISFC.Models.Material.Input>();
                foreach (FarPoint.Win.Spread.Row r in neuSpread2_Sheet1.Rows)
                {
                    FS.HISFC.Models.Material.Input input = neuSpread2_Sheet1.Rows[r.Index].Tag as FS.HISFC.Models.Material.Input;
                    list.Add(input);
                }
                Function.IPrint.SetData(list);
            }
            else
            {
                Function.IPrint = outputInstance as FS.HISFC.BizProcess.Interface.Material.IBillPrint;
                List<FS.HISFC.Models.Material.Output> list = new List<FS.HISFC.Models.Material.Output>();
                foreach (FarPoint.Win.Spread.Row r in neuSpread2_Sheet1.Rows)
                {
                    FS.HISFC.Models.Material.Output output = neuSpread2_Sheet1.Rows[r.Index].Tag as FS.HISFC.Models.Material.Output;
                    list.Add(output);
                }
                Function.IPrint.SetData(list);
            }

            this.Clear();
            return 1;
            return base.Print(sender, neuObject);
        }

        /// <summary>
        /// �������������
        /// </summary>
        private void Clear()
        {
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread2_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// ����ѯ���ĵ��ݼ�������
        /// </summary>
        /// <param name="alListInfo"></param>
        private void AddDataToSheet(ArrayList alListInfo)
        {
            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
            FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
            foreach (object obj in alListInfo)
            {
                if (this.rbtInputList.Checked)
                {
                    FS.HISFC.Models.Material.Input input = obj as FS.HISFC.Models.Material.Input;

                    this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColSheet1.ListNO].Text = input.InListNO;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColSheet1.InvoiceNO].Text = input.InvoiceNO;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColSheet1.OperType].Text = input.StoreBase.Extend;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColSheet1.TargetDept].Text = input.StoreBase.TargetDept.ID;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColSheet1.OperDate].Text = input.StoreBase.Operation.Oper.OperTime.ToString();

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColSheet1.ListNO].Tag = cmbStorage.Tag.ToString();
                    this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Tag = "input";
                }
                else
                {
                    FS.HISFC.Models.Material.Output output = obj as FS.HISFC.Models.Material.Output;

                    this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColSheet1.ListNO].Text = output.OutListNO;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColSheet1.InvoiceNO].Text = "";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColSheet1.OperType].Text = output.StoreBase.Extend;

                    FS.HISFC.Models.Base.Department dept = deptManager.GetDeptmentById(output.StoreBase.TargetDept.ID);
                    if (dept != null)
                    {
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColSheet1.TargetDept].Text = dept.Name;
                    }
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColSheet1.OperDate].Text = output.StoreBase.Operation.Oper.OperTime.ToString();

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColSheet1.ListNO].Tag = cmbStorage.Tag.ToString();
                    this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Tag = "output";
                }
            }
        }

        /// <summary>
        /// ���ҳ��ⵥ
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="storageID">ִ�пⷿ</param>
        private ArrayList QueryOutputList(DateTime beginTime, DateTime endTime, string storageID)
        {
            ArrayList al = new ArrayList();
            if (this.matManager.QueryOutputListInfoByStorageAndDate(beginTime, endTime, storageID, ref al) == -1)
            {
                MessageBox.Show("���ҳ��ⵥ��Ϣʧ��" + matManager.Err);

                return null;
            }
            return al;
        }

        /// <summary>
        /// ������ⵥ
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="storageID">ִ�пⷿ</param>
        private ArrayList QueryInputList(DateTime beginTime, DateTime endTime, string storageID)
        {
            ArrayList al = new ArrayList();
            if (this.matManager.QueryInputListInfoByStorageAndDate(beginTime, endTime, storageID, ref al) == -1)
            {
                MessageBox.Show("������ⵥ��Ϣʧ��" + matManager.Err);

                return null;
            }
            return al;
        }

        /// <summary>
        /// ����ѯ���ĳ����¼���뵽�ڶ��������
        /// </summary>
        /// <param name="alInputInfo"></param>
        private void AddOutputToSheet2(List<FS.HISFC.Models.Material.Output> alInputInfo)
        {
            foreach (FS.HISFC.Models.Material.Output output in alInputInfo)
            {
                this.neuSpread2_Sheet1.AddRows(this.neuSpread2_Sheet1.RowCount, 1);
                this.neuSpread2_Sheet1.Cells[this.neuSpread2_Sheet1.RowCount - 1, (int)ColSheet2.ItemCode].Text = output.StoreBase.Item.ID;
                this.neuSpread2_Sheet1.Cells[this.neuSpread2_Sheet1.RowCount - 1, (int)ColSheet2.ItemName].Text = output.StoreBase.Item.Name;
                this.neuSpread2_Sheet1.Cells[this.neuSpread2_Sheet1.RowCount - 1, (int)ColSheet2.Specs].Text = output.StoreBase.Item.Specs;
                this.neuSpread2_Sheet1.Cells[this.neuSpread2_Sheet1.RowCount - 1, (int)ColSheet2.Qty].Text = output.StoreBase.Quantity.ToString();
                this.neuSpread2_Sheet1.Cells[this.neuSpread2_Sheet1.RowCount - 1, (int)ColSheet2.Unit].Text = output.StoreBase.Item.MinUnit;
                this.neuSpread2_Sheet1.Cells[this.neuSpread2_Sheet1.RowCount - 1, (int)ColSheet2.Price].Text = output.StoreBase.PriceCollection.PurchasePrice.ToString();

                decimal cost = output.StoreBase.PriceCollection.PurchasePrice * output.StoreBase.Quantity;
                this.neuSpread2_Sheet1.Cells[this.neuSpread2_Sheet1.RowCount - 1, (int)ColSheet2.Cost].Text = cost.ToString();

                this.neuSpread2_Sheet1.Cells[this.neuSpread2_Sheet1.RowCount - 1, (int)ColSheet2.ItemCode].Tag = "output";
                this.neuSpread2_Sheet1.Rows[this.neuSpread2_Sheet1.RowCount - 1].Tag = output;
            }
        }

        /// <summary>
        /// ����ѯ��������¼���뵽�ڶ��������
        /// </summary>
        /// <param name="alInputInfo"></param>
        private void AddInputToSheet2(ArrayList alInputInfo)
        {
            foreach (FS.HISFC.Models.Material.Input input in alInputInfo)
            {
                this.neuSpread2_Sheet1.AddRows(this.neuSpread2_Sheet1.RowCount, 1);
                this.neuSpread2_Sheet1.Cells[this.neuSpread2_Sheet1.RowCount - 1, (int)ColSheet2.ItemCode].Text = input.StoreBase.Item.ID;
                this.neuSpread2_Sheet1.Cells[this.neuSpread2_Sheet1.RowCount - 1, (int)ColSheet2.ItemName].Text = input.StoreBase.Item.Name;
                this.neuSpread2_Sheet1.Cells[this.neuSpread2_Sheet1.RowCount - 1, (int)ColSheet2.Specs].Text = input.StoreBase.Item.Specs;
                this.neuSpread2_Sheet1.Cells[this.neuSpread2_Sheet1.RowCount - 1, (int)ColSheet2.Qty].Text = input.StoreBase.Quantity.ToString();
                this.neuSpread2_Sheet1.Cells[this.neuSpread2_Sheet1.RowCount - 1, (int)ColSheet2.Unit].Text = input.StoreBase.Item.MinUnit;
                this.neuSpread2_Sheet1.Cells[this.neuSpread2_Sheet1.RowCount - 1, (int)ColSheet2.Price].Text = input.StoreBase.PriceCollection.PurchasePrice.ToString();
                
                decimal cost = input.StoreBase.PriceCollection.PurchasePrice * input.StoreBase.Quantity;
                this.neuSpread2_Sheet1.Cells[this.neuSpread2_Sheet1.RowCount - 1, (int)ColSheet2.Cost].Text = cost.ToString();

                this.neuSpread2_Sheet1.Cells[this.neuSpread2_Sheet1.RowCount - 1, (int)ColSheet2.ItemCode].Tag = "input";
                this.neuSpread2_Sheet1.Rows[this.neuSpread2_Sheet1.RowCount - 1].Tag = input;
            }
        }

        /// <summary>
        /// ���ݳ��ⵥ�š�������Ҳ�ѯ�����¼
        /// </summary>
        /// <param name="listNO"></param>
        /// <param name="deptID"></param>
        private List<FS.HISFC.Models.Material.Output> QueryOutputByListNO(string listNO, string deptID)
        {
            return matManager.QueryOutputByListNO(deptID, listNO);
        }

        /// <summary>
        /// ������ⵥ�š������Ҳ�ѯ����¼
        /// </summary>
        /// <param name="p"></param>
        /// <param name="p_2"></param>
        private ArrayList QueryInputByListNO(string listNO, string deptID)
        {
            return matManager.QueryInputDetailByListNO(deptID, listNO);
        } 

        #endregion

        #region �¼�

        private void ucListReprint_Load(object sender, EventArgs e)
        {
            this.Init();
            this.SetPrintObject();
        }        

        /// <summary>
        /// ˫���¼� ��ѯѡ���е�������¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.neuSpread2_Sheet1.RowCount = 0;
            ArrayList alInputInfo = new ArrayList();

            if (neuSpread1_Sheet1.Rows[neuSpread1_Sheet1.ActiveRowIndex].Tag.ToString() == "input")
            {
                alInputInfo = this.QueryInputByListNO(neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.ActiveRowIndex, (int)ColSheet1.ListNO].Text, neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.ActiveRowIndex, (int)ColSheet1.ListNO].Tag.ToString());
                if (alInputInfo != null && alInputInfo.Count > 0)
                {
                    this.AddInputToSheet2(alInputInfo);
                }
            }
            else
            {
                List<FS.HISFC.Models.Material.Output> outputList = new List<FS.HISFC.Models.Material.Output>();
                outputList = this.QueryOutputByListNO(neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.ActiveRowIndex, (int)ColSheet1.ListNO].Text, neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.ActiveRowIndex, (int)ColSheet1.ListNO].Tag.ToString());
                if (outputList != null && outputList.Count > 0)
                {
                    this.AddOutputToSheet2(outputList);
                }
            }
        } 
        #endregion

        #region ��ö��

        /// <summary>
        /// ������
        /// </summary>
        private enum ColSheet1
        {
            /// <summary>
            /// ����
            /// </summary>
            ListNO,
            /// <summary>
            /// ��Ʊ��
            /// </summary>
            InvoiceNO,
            /// <summary>
            /// ����
            /// </summary>
            OperType,
            /// <summary>
            /// Ŀ�굥λ
            /// </summary>
            TargetDept,
            ///// <summary>
            ///// ����Ա
            ///// </summary>
            //Operator,
            /// <summary>
            /// ����ʱ��
            /// </summary>
            OperDate
        }
        /// <summary>
        /// ������
        /// </summary>
        private enum ColSheet2
        {
            /// <summary>
            /// ��Ŀ����
            /// </summary>
            ItemCode,
            /// <summary>
            /// ��Ŀ����
            /// </summary>
            ItemName,
            /// <summary>
            /// ���
            /// </summary>
            Specs,
            /// <summary>
            /// ����
            /// </summary>
            Qty,
            /// <summary>
            /// ��λ
            /// </summary>
            Unit,
            /// <summary>
            /// ����
            /// </summary>
            Price,
            /// <summary>
            /// ���
            /// </summary>
            Cost,
        }

        #endregion

    }
}
