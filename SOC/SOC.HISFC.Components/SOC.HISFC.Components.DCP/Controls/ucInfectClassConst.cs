using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.DCP.Controls
{
    /// <summary>
    /// [���������� ����ά�����ڱ�עһ���в����޸ģ�ֻ��ѡ����]
    /// [�� �� �� �� �Ծ�]
    /// [����ʱ�䣺 2008-09]
    /// <�޸ļ�¼>
    /// </�޸ļ�¼>
    /// </summary>
    public partial class ucInfectClassConst : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.FrameWork.WinForms.Forms.IMaintenanceControlable
    {
        public ucInfectClassConst()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// �����б�
        /// </summary>
        private DataTable dtConst=new DataTable();

        /// <summary>
        /// ��Ⱦ����������
        /// </summary>
        private ArrayList infectClassList = new ArrayList();

        /// <summary>
        /// ��������������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper additionHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ��Ա������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper employeeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ����������
        /// </summary>
        private FS.SOC.HISFC.BizProcess.DCP.Common commonProcess = new FS.SOC.HISFC.BizProcess.DCP.Common();

        /// <summary>
        /// ������ʾ����������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper AddtionReportMsgHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ƴ��������
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Spell spellLogicManagment = new FS.HISFC.BizLogic.Manager.Spell();

        private int columnCount;
        private bool isShowID = false;
        private string ConstType = "";
        private bool isSelectOne = false;
        #endregion

        #region ����

        /// <summary>
        /// ������ֵ
        /// </summary>
        private void SetFpValue()
        {
            this.neuSpread1_Sheet1.RowCount = 0;

            foreach (FS.HISFC.Models.Base.Const con in this.infectClassList)
            {
                this.neuSpread1_Sheet1.RowCount++;

                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)SetColName.ConNO].Value = con.ID;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)SetColName.ConName].Value = con.Name;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)SetColName.ConMemo].Value = con.Memo;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)SetColName.ConSpellCode].Value = con.SpellCode;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)SetColName.ConWBCode].Value = con.WBCode;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)SetColName.ConUserCode].Value = con.UserCode;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)SetColName.ConSortNO].Value = con.SortID;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)SetColName.ConValid].Value = con.IsValid;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)SetColName.ConOperName].Value = con.OperEnvironment.ID;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)SetColName.ConOperTime].Value = con.OperEnvironment.OperTime;
            }
        }

        /// <summary>
        /// ��������ӵ�dtConsts
        /// </summary>
        /// <param name="con">����ʵ��</param>
        public void AddDataToTable(FS.HISFC.Models.Base.Const con)
        {
            if (con == null)
            {
                return;
            }

            this.dtConst.Rows.Add(new object[]{
                con.ID,
                con.Name,
                con.Memo,
                con.SpellCode,
                con.WBCode,
                con.UserCode,
                con.SortID,
                con.IsValid,
                con.OperEnvironment.ID,
                con.OperEnvironment.OperTime});
        }

        /// <summary>
        /// ����������ӵ�ʵ��
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.Const AddData(DataRow dr)
        {
            if (dr == null)
            {
                return null;
            }
            
            FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();

            con.ID = dr[(int)SetColName.ConNO].ToString();
            con.Name = dr[(int)SetColName.ConName].ToString();
            con.Memo = dr[(int)SetColName.ConMemo].ToString();
            con.SpellCode = dr[(int)SetColName.ConSpellCode].ToString();
            con.WBCode = dr[(int)SetColName.ConWBCode].ToString();
            con.UserCode = dr[(int)SetColName.ConUserCode].ToString();
            con.SortID = FS.FrameWork.Function.NConvert.ToInt32(dr[(int)SetColName.ConSortNO]);
            con.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(dr[(int)SetColName.ConValid]);
            con.OperEnvironment.ID = this.employeeHelper.GetID(dr[(int)SetColName.ConOperName].ToString());
            con.OperEnvironment.OperTime =FS.FrameWork.Function.NConvert.ToDateTime( dr[(int)SetColName.ConOperTime]);

            return con;
        }

        /// <summary>
        /// ��ʼ��dtConst
        /// </summary>
        public void InitDataTable()
        {
            this.neuSpread1_Sheet1.DataAutoSizeColumns = false;

            //��������
            System.Type typeStr = System.Type.GetType("System.String");
            System.Type typeInt= System.Type.GetType("System.Int");
            System.Type typeDateTime= System.Type.GetType("System.DateTime");
            System.Type typeBoolean = System.Type.GetType("System.Boolean");

            //��ʼ����
            this.dtConst.Columns.AddRange(new DataColumn[]{
                                                        new DataColumn("����",typeStr),
                                                        new DataColumn("����",typeStr),
                                                        new DataColumn("��ע",typeStr),
                                                        new DataColumn("ƴ����",typeStr),
                                                        new DataColumn("�����",typeStr),
                                                        new DataColumn("������",typeStr),
                                                        new DataColumn("˳���",typeStr),
                                                        new DataColumn("�Ƿ���Ч",typeBoolean),
                                                        new DataColumn("����Ա",typeStr),
                                                        new DataColumn("����ʱ��",typeDateTime)
                                                        });
            this.dtConst.DefaultView.AllowNew = true;
            this.dtConst.DefaultView.AllowEdit = true;
            this.dtConst.DefaultView.AllowDelete = true;

            this.neuSpread1_Sheet1.DataSource = this.dtConst.DefaultView;
        }

        /// <summary>
        /// ��ʼ��Fp
        /// </summary>
        public void SetFormatFp()
        {
            this.neuSpread1_Sheet1.Columns[(int)SetColName.ConNO].Locked = true;
            this.neuSpread1_Sheet1.Columns[(int)SetColName.ConOperName].Locked = true;
            this.neuSpread1_Sheet1.Columns[(int)SetColName.ConOperTime].Locked = true;
        }

        /// <summary>
        /// �ж��Ƿ���Ч
        /// </summary>
        /// <param name="drAdd"></param>
        /// <returns></returns>
        public int IsValid()
        {
            DataTable dt = this.dtConst.GetChanges(DataRowState.Modified | DataRowState.Added);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (string.IsNullOrEmpty(dr["����"].ToString()))
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("���Ʋ���Ϊ��"));
                        return -1;
                    }
                }
            }

            dt = this.dtConst.GetChanges(DataRowState.Unchanged | DataRowState.Modified);
            DataTable dtAdd = this.dtConst.GetChanges(DataRowState.Added);
            if (dt != null && dtAdd != null)
            {
                foreach (DataRow drAdd in dtAdd.Rows)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["����"].ToString() == drAdd["����"].ToString())
                        {
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("�Ѵ���" + drAdd["����"].ToString() + "����"));
                            return -1;
                        }
                    }
                }
            }

            return 1;
        }

        #endregion

        #region �¼�

        protected virtual void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            try
            {
                if (e.Column == columnCount)
                {
                    if (isSelectOne)
                    {
                        FS.FrameWork.WinForms.Forms.frmEasyChoose frmPop = new FS.FrameWork.WinForms.Forms.frmEasyChoose(this.AddtionReportMsgHelper.ArrayObject);
                        frmPop.Text = "��ѡ����Ŀ";
                        frmPop.StartPosition = FormStartPosition.CenterScreen;
                        frmPop.SelectedItem += new FS.FrameWork.WinForms.Forms.SelectedItemHandler(frmPop_SelectedItem);
                        frmPop.ShowDialog();
                    }
                    else
                    {
                        System.Collections.Hashtable hs = new System.Collections.Hashtable();
                        string memo = this.dtConst.Rows[e.Row][columnCount].ToString();
                        if (!string.IsNullOrEmpty(memo))
                        {
                            foreach (string s in memo.Split(','))
                            {
                                if (hs.ContainsValue(s))
                                {
                                    continue;
                                }
                                else
                                {
                                    if (!isShowID)
                                    {
                                        hs.Add(this.AddtionReportMsgHelper.GetID(s), s);
                                    }
                                    else
                                    {
                                        hs.Add(s, s);
                                    }
                                }
                            }
                        }
                        ucMyReport ucMyReport = new ucMyReport(hs);
                        int i = this.AddtionReportMsgHelper.ArrayObject.Count / 14;
                        if (i > 1)
                        {
                            ucMyReport.Size = new Size(150 * i, 300);
                        }
                        ucMyReport.ConstList = this.AddtionReportMsgHelper.ArrayObject;
                        ucMyReport.enterDataBinding += new ucMyReport.DataBinding(ucMyReport_enterDataBinding);

                        FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucMyReport, FormBorderStyle.Fixed3D, FormWindowState.Normal);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg(ex.Message));
                this.neuSpread1_Sheet1.Cells[e.Row, e.Column].Value = "";
            }
        }

        private void frmPop_SelectedItem(FS.FrameWork.Models.NeuObject sender)
        {
            if (sender != null)
            {
                if (isShowID)
                {
                    this.neuSpread1_Sheet1.ActiveCell.Value = sender.ID;
                }
                else
                {
                    this.neuSpread1_Sheet1.ActiveCell.Value = sender.Name;
                }
            }
        }

        public void ucMyReport_enterDataBinding(System.Collections.Hashtable hsMemo)
        {
            string s="";
            foreach (System.Collections.DictionaryEntry de in hsMemo)
            {
                if (!isShowID)
                {
                    if (de.Value.ToString() != "")
                    {
                        s += "," + de.Value.ToString();
                    }
                }
                else
                {
                    if (de.Key.ToString() != "")
                    {
                        s += "," + de.Key.ToString();
                    }
                }
            }

            if(s.StartsWith(","))
            {
                s=s.Remove(0,1);
            }

            this.neuSpread1_Sheet1.ActiveCell.Value = s;
        }

        protected virtual void fpSpread1_Sheet1_CellChanged(object sender, FarPoint.Win.Spread.SheetViewEventArgs e)
        {           
            //����ı������ƣ���ƴ���롢������Զ������仯
            if (e.Column==(int)SetColName.ConName)
            {
                FarPoint.Win.Spread.Column column = this.neuSpread1_Sheet1.Columns[(int)SetColName.ConSpellCode];
                if (column != null /*&& this.fpSpread1_Sheet1.Cells[e.Row,column.Index].Text.Length==0*/)
                {
                    FS.HISFC.Models.Base.ISpell spCode = this.spellLogicManagment.Get(this.neuSpread1_Sheet1.Cells[e.Row, e.Column].Text);
                    if (spCode != null)
                        this.neuSpread1_Sheet1.Cells[e.Row, column.Index].Text = spCode.SpellCode;
                }

                column = this.neuSpread1_Sheet1.Columns[(int)SetColName.ConWBCode];
                if (column != null)
                {
                    FS.HISFC.Models.Base.ISpell spCode = this.spellLogicManagment.Get(this.neuSpread1_Sheet1.Cells[e.Row, e.Column].Text);
                    if (spCode != null)
                        this.neuSpread1_Sheet1.Cells[e.Row, column.Index].Text = spCode.WBCode;
                }
            }
        }

        private void neuTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (dtConst != null)
            {
                this.dtConst.DefaultView.RowFilter = string.Format("���� like '%{0}%' or ���� like '%{0}%' or ��ע like '%{0}%'  or ƴ���� like '%{0}%' or ����� like '%{0}%'", this.neuTextBox1.Text);
            }
        }

        #endregion

        #region IMaintenanceControlable ��Ա

        private bool isDirty = false;

        private FS.FrameWork.WinForms.Forms.IMaintenanceForm queryForm = null;

        /// <summary>
        /// ��ӷ���
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            if (this.dtConst == null)
            {
                return -1;
            }

            this.dtConst.Rows.InsertAt(this.dtConst.NewRow(), this.dtConst.Rows.Count);

            this.neuSpread1_Sheet1.ActiveRowIndex = this.neuSpread1_Sheet1.RowCount - 1;
            this.neuSpread1_Sheet1.ActiveColumnIndex = 0;
            this.neuSpread1_Sheet1.ActiveRow.Locked = false;
            this.neuSpread1_Sheet1.Columns[(int)SetColName.ConMemo].Locked = true;

            return 1;
        }

        public int Copy()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Cut()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            if (this.dtConst==null||this.dtConst.Rows.Count==0)
            {
                return -1;
            }

            if (this.neuSpread1_Sheet1.ActiveRow == null)
            {
                return -1;
            }

            this.dtConst.Rows.Remove(this.dtConst.Rows[this.neuSpread1_Sheet1.ActiveRowIndex]);
            //this.dtConst.AcceptChanges();

            return 1;
        }

        public int Export()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Import()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Init()
        {
            this.InitDataTable();

            string[] s = this.FocusedControl.Text.Split('|');
            string constType = "";
            columnCount = (int)SetColName.ConMemo;
            if (s.Length >= 2)
            {
                this.ConstType = s[0];
                string[] sTemp = s[1].Split(',');
                if (sTemp.Length == 4)
                {
                    constType = sTemp[0];
                    columnCount = FS.FrameWork.Function.NConvert.ToInt32(sTemp[1]);
                    isShowID = FS.FrameWork.Function.NConvert.ToBoolean(sTemp[2]);
                    isSelectOne = FS.FrameWork.Function.NConvert.ToBoolean(sTemp[3]);
                }
            }

            this.neuSpread1_Sheet1.Columns[columnCount].Locked = true;
            this.neuSpread1_Sheet1.Columns[columnCount].BackColor = Color.FromArgb(220, 220, 220);

            //��Ⱦ��������
            if (constType == "INFECTCLASS")
            {
                ArrayList alInfectClass = new ArrayList();

                alInfectClass.AddRange(commonProcess.QueryConstantList("INFECTCLASS"));
                if (alInfectClass == null)
                {
                    return -1;
                }

                ArrayList al = new ArrayList();
                foreach (FS.HISFC.Models.Base.Const infectclass in alInfectClass)
                {
                    ArrayList alItem = commonProcess.QueryConstantList(infectclass.ID);

                    al.AddRange(alItem);
                }
                this.AddtionReportMsgHelper.ArrayObject = al;
            }
            else
            {
                this.AddtionReportMsgHelper.ArrayObject = this.commonProcess.QueryConstantList(constType);
            }
            if (this.AddtionReportMsgHelper.ArrayObject == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ��Ⱦ��������ʾ����ʧ�ܣ�"));
                return -1;
            }


            return 1;
        }

        public bool IsDirty
        {
            get
            {
                return this.isDirty;
            }
            set
            {
                this.isDirty = value;
            }
        }

        public int Modify()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int NextRow()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Paste()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int PreRow()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Print()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int PrintConfig()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int PrintPreview()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <returns></returns>
        public int Query()
        {
            if (this.infectClassList == null)
            {
                return -1;
            }

            this.SetFormatFp();

            this.dtConst.Rows.Clear();
            this.additionHelper.ArrayObject = this.commonProcess.QueryConstantList(this.ConstType);
            if (this.additionHelper.ArrayObject == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ��Ⱦ��������������"));
                return -1;
            }

            this.infectClassList = this.additionHelper.ArrayObject;

            foreach (FS.HISFC.Models.Base.Const con in this.infectClassList)
            {
                this.AddDataToTable(con);
            }

            this.dtConst.AcceptChanges();

            return 1;
        }

        public FS.FrameWork.WinForms.Forms.IMaintenanceForm QueryForm
        {
            get
            {
                return this.queryForm;
            }
            set
            {
                this.queryForm = value;
            }
        }

        /// <summary>
        /// ���淽��
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            this.neuSpread1.StopCellEditing();

            foreach (DataRow dr in this.dtConst.Rows)
            {
                dr.EndEdit();
            }

            //�ж���Ч��
            if (this.IsValid() == -1)
            {
                return -1;
            }
            
            //��������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            FS.HISFC.BizLogic.Manager.Constant consManagment = new FS.HISFC.BizLogic.Manager.Constant();

            consManagment.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            DataTable dtChanges = this.dtConst.GetChanges( DataRowState.Modified | DataRowState.Added);
            
            if (dtChanges != null)
            {
                foreach (DataRow dr in dtChanges.Rows)
                {                    
                    FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();
                    con = this.AddData(dr);
                    int i = consManagment.UpdateItem(this.ConstType, con);
                    if ( i==0)
                    {
                        if (consManagment.InsertItem(this.ConstType, con) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ʧ��") + consManagment.Err);
                            return -1;
                        }
                    }
                    else if(i==-1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ʧ��") + consManagment.Err);
                        return -1;
                    }
                }
            }

            DataTable dtDelete = this.dtConst.GetChanges(DataRowState.Deleted);

            if (dtDelete != null)
            {
                dtDelete.RejectChanges();
                foreach (DataRow dr in dtDelete.Rows)
                {
                    if (consManagment.DelConstant(this.ConstType, dr[(int)SetColName.ConNO].ToString()) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("ɾ��ʧ��") + consManagment.Err);
                        return -1;
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            this.SetFormatFp();
            this.dtConst.AcceptChanges();
            MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ɹ�"));
            return 1;
        }

        #endregion

        
               
    }

    /// <summary>
    /// ��ö��
    /// </summary>
    public enum SetColName
    {
        /// <summary>
        /// ��������
        /// </summary>
        ConNO,
        /// <summary>
        /// ��������
        /// </summary>
        ConName,
        /// <summary>
        /// ������ע
        /// </summary>
        ConMemo,
        /// <summary>
        /// ƴ����
        /// </summary>
        ConSpellCode,
        /// <summary>
        /// �����
        /// </summary>
        ConWBCode,
        /// <summary>
        /// ������
        /// </summary>
        ConUserCode,
        /// <summary>
        /// ˳���
        /// </summary>
        ConSortNO,
        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        ConValid,
        /// <summary>
        /// ����Ա
        /// </summary>
        ConOperName,
        /// <summary>
        /// ����ʱ��
        /// </summary>
        ConOperTime,
    }
}
