using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Report.Logistics.Pharmacy
{
    /// <summary>
    /// [�ؼ�����:ҩƷ�Զ����ѯ]
    /// <�޸ļ�¼>
    ///    1.������µĴ�ӡ��ʽ by Sunjh 2009-3-13 {699DBE34-5DEA-4ba8-AFDD-A04364CFC8AD}
    ///    2.��ֲ����5.0 yangw 2010-05-18 {85997F7C-0E19-46e8-B552-2A60009747B4}
    /// </�޸ļ�¼>
    /// </summary>
    public partial class ucCustomQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCustomQuery()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ƽ��������ʱ�������뾫��
        /// </summary>
        private int decimalValue = 2;

        /// <summary>
        /// ƽ��������ʱ�������뾫��
        /// </summary>
        [Description("ƽ��������ʱ�������뾫��"), Category("����"), Browsable(true)]
        public int DecimalValue
        {
            get { return decimalValue; }
            set { decimalValue = value; }
        }

        /// <summary>
        /// SQL�����֤ʱʱ����
        /// </summary>
        private int daysSpan = 100;

        /// <summary>
        /// SQL�����֤ʱʱ����
        /// </summary>
        [Description("SQL�����֤ʱʱ����"), Category("����"), Browsable(true)]
        public int DaysSpan
        {
            get
            {
                return daysSpan;
            }
            set
            {
                if (value < 1)
                {
                    value = 1;
                }
                daysSpan = value;
            }
        }

        /// <summary>
        /// SQL�����֤ʱʱ�������ֵ
        /// </summary>
        private int maxDaysSpan = 700;

        /// <summary>
        /// SQL�����֤ʱʱ�������ֵ
        /// </summary>
        [Description("SQL�����֤ʱʱ�������ֵ���������ֵʱֹͣ��֤"), Category("����"), Browsable(true)]
        public int MaxDaysSpan
        {
            get { return maxDaysSpan; }
            set { maxDaysSpan = value; }
        }

        #region ����
        private FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
        private FS.HISFC.Components.Common.Controls.ucPrivePowerReport myReport = new FS.HISFC.Components.Common.Controls.ucPrivePowerReport();
        private FS.FrameWork.Models.NeuObject myPreDefine = new FS.FrameWork.Models.NeuObject();
        private Report.Logistics.Pharmacy.tvCustomQuery  tvCustomQuery;
        
        
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        private string operCode = string.Empty;

        
        #endregion

        #region ����

        /// <summary>
        /// ���ÿ���ȫ���鿴�Ĺ���
        /// </summary>
        [Category("�ؼ�����"), Description("��ѯȨ��ȫ�еĹ���")]
        public string OperCode
        {
            get
            {
                return operCode;
            }
            set
            {
                operCode = value;
            }
        }
        #endregion 
        
        #region ����
        /// <summary>
        /// ���ñ���ؼ�����
        /// </summary>
        private void SetReportControlPerporty()
        {
            this.myReport.lbAdditionTitleMid.Text = "";
            this.myReport.lbAdditionTitleRight.Text = "";
            this.myReport.Dock = DockStyle.Fill;
            this.myReport.IsDeptAsCondition = false;
            this.myReport.QueryDataWhenInit = false;
            this.myReport.FilerType = FS.HISFC.Components.Common.Controls.ucPrivePowerReport.EnumFilterType.���ܹ���;
        }

        /// <summary>
        /// ��ȡ������͵���
        /// </summary>
        /// <param name="filters">�����ַ���</param>
        /// <param name="sumCols">������</param>
        /// <returns>-1ʧ��</returns>
        private int GetFilterAndSumCol(ref string filters, ref string sumCols)
        {
            int colIndex = -1;
            sumCols = "";
            filters = "";
            try
            {
                for (int rowIndex = 0; rowIndex < this.neuFpEnter1_Sheet1.RowCount; rowIndex++)
                {
                    //����û��ѡ�е��ֶ�
                    if (!FS.FrameWork.Function.NConvert.ToBoolean(neuFpEnter1_Sheet1.Cells[rowIndex, (int)ColSet.ѡ��].Value))
                    {
                        continue;
                    }

                    colIndex++;

                    string customField = neuFpEnter1_Sheet1.Cells[rowIndex, (int)ColSet.�ֶα���].Text;

                    //�ϼ�
                    if (FS.FrameWork.Function.NConvert.ToBoolean(neuFpEnter1_Sheet1.Cells[rowIndex, (int)ColSet.��Ӻϼ�].Value))
                    {
                        sumCols += colIndex.ToString() + ",";
                    }

                    //����
                    if (FS.FrameWork.Function.NConvert.ToBoolean(neuFpEnter1_Sheet1.Cells[rowIndex, (int)ColSet.����].Value))
                    {
                        if (string.IsNullOrEmpty(customField))
                        {
                            filters += neuFpEnter1_Sheet1.Cells[rowIndex, (int)ColSet.�ֶ�����].Text + ",";
                        }
                        else
                        {
                            filters += customField + ",";
                        }
                    }
                }
            }
            catch
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ���SQl��Ч��
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        private bool CheckSQL(string sql, ref string errInfo)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("SQL��֤��...");
            Application.DoEvents();

            if (!string.IsNullOrEmpty(sql) && sql.IndexOf("usercode") != -1)
            {
                sql = sql.Replace("usercode", dbMgr.Operator.ID);
            }


            System.Data.DataSet ds = new DataSet();
            string s = sql;
            int times = 0;
            if (s.IndexOf("{0}") != -1)
            {
                #region ����ʱ�����
                while (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    s = sql;

                    s = string.Format(s, System.DateTime.Now.AddDays(-(DaysSpan + DaysSpan * times)).ToString(), System.DateTime.Now.ToString());

                    if (this.dbMgr.ExecQuery(s, ref ds) == -1)
                    {
                        errInfo = dbMgr.Err;
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                        errInfo = "SQL��Ч\n---------------------\n"
                     + dbMgr.Err
                     + "\n---------------------\n"
                     + s;

                        return false;
                    }
                    if (times > 1)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        System.Windows.Forms.DialogResult dr;
                        dr = System.Windows.Forms.MessageBox.Show(
                            FS.FrameWork.Management.Language.Msg("������֤�������Ч�����Ǹ������Ķ������ " + (DaysSpan + DaysSpan * times).ToString() + " ����û�п��Բ�ѯ�����ݣ��Ƿ�����ʱ�䷶Χ��"),
                            FS.FrameWork.Management.Language.Msg("��ȷ��"),
                            System.Windows.Forms.MessageBoxButtons.YesNo,
                            System.Windows.Forms.MessageBoxIcon.Question,
                            System.Windows.Forms.MessageBoxDefaultButton.Button2);
                        if (dr != DialogResult.Yes)
                        {
                            errInfo = "SQL��֤ȡ��";
                            return false;
                        }
                        FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("SQL��֤��...");
                    }
                    if (DaysSpan + DaysSpan * times > MaxDaysSpan)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        errInfo = MaxDaysSpan.ToString() + "����û�пɲ�ѯ���ݣ��Ѿ��������ʱ���������鶨���Ƿ���ȷ��";
                        return false;
                    }

                    times++;
                }
                #endregion
            }
            else
            {
                #region ����ʱ�����
                if (this.dbMgr.ExecQuery(s, ref ds) == -1)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                    errInfo = "SQL��Ч\n---------------------\n"
                    + dbMgr.Err
                    + "\n---------------------\n"
                    + s;

                    return false;
                }
                #endregion
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            errInfo = "SQL��֤�ɹ�\n---------------------\n" + s;
            return true;
        }

        /// <summary>
        /// ��ȡSQL���
        /// </summary>
        /// <returns></returns>
        private FS.FrameWork.Models.NeuObject GetSQL()
        {
            //û������ʱ����
            if (myPreDefine == null || this.neuFpEnter1_Sheet1.RowCount == 0)
            {
                return null;
            }

            string sql = "SELECT {0} \nFROM   " + neuFpEnter1_Sheet1.Cells[0, (int)ColSet.��ͼ].Text + "\n";

            //����ʾ�ظ�������
            if (this.ckDistinct.Checked)
            {
                sql = "SELECT DISTINCT {0} \nFROM   " + neuFpEnter1_Sheet1.Cells[0, (int)ColSet.��ͼ].Text + "\n";
            }

            string groupFields = "";                //��¼������ֶΣ����Ÿ���
            string fieldAndcustomFields = "";       //�ֶκ��ֶα�����
            string sumCols = "";                    //��͵���
            string sortCols = "";                   //�������
            string filters = "";                    //���˵���
            string customWheres = "";               //�������ʽ �Զ����where����
            int colIndex = -1;                      //��������������ͣ�����
            bool isNeedGroup = false;               //�Ƿ���Ҫ����

            for (int rowIndex = 0; rowIndex < this.neuFpEnter1_Sheet1.RowCount; rowIndex++)
            {
                //�Զ���Where����
                string customWhere = neuFpEnter1_Sheet1.Cells[rowIndex, (int)ColSet.�������ʽ].Text;
                if (!string.IsNullOrEmpty(customWhere))
                {
                    customWheres += "\nAND    " + customWhere;
                }

                //����û��ѡ�е��ֶ�
                if (!FS.FrameWork.Function.NConvert.ToBoolean(neuFpEnter1_Sheet1.Cells[rowIndex, (int)ColSet.ѡ��].Value))
                {
                    continue;
                }

                //�����������¼ �û�ѡ��һ�У��ֶ�ûѡ��ʱ������
                colIndex++;

                //�ֶα���
                string customField = neuFpEnter1_Sheet1.Cells[rowIndex, (int)ColSet.�ֶα���].Text;

                //�ֶ�
                string field = neuFpEnter1_Sheet1.Cells[rowIndex, (int)ColSet.��ͼ].Text + "." + neuFpEnter1_Sheet1.Cells[rowIndex, (int)ColSet.�ֶ�����].Text;

                //���ʽ �ñ��ʽ�滻�ֶΣ������滻�ֶα���
                string f = neuFpEnter1_Sheet1.Cells[rowIndex, (int)ColSet.�ֶα��ʽ].Text;
                if (!string.IsNullOrEmpty(f))
                {
                    field = f;
                }

                //�������\ƽ������͵��ֶβ���group by �У���������group by
                if (FS.FrameWork.Function.NConvert.ToBoolean(neuFpEnter1_Sheet1.Cells[rowIndex, (int)ColSet.ƽ����].Value))
                {
                    isNeedGroup = true;
                    field = "ROUND(AVG(" + field + "), " + DecimalValue.ToString() + ")";
                }
                else
                {
                    if (!FS.FrameWork.Function.NConvert.ToBoolean(neuFpEnter1_Sheet1.Cells[rowIndex, (int)ColSet.�������].Value))
                    {
                        //�Զ����˷����ֶΣ������Զ�������ֶ�
                        string customGroup = neuFpEnter1_Sheet1.Cells[rowIndex, (int)ColSet.�����ֶ�].Text;
                        if (string.IsNullOrEmpty(customGroup))
                        {
                            groupFields +=
                            neuFpEnter1_Sheet1.Cells[rowIndex, (int)ColSet.��ͼ].Text
                            + "."
                            + neuFpEnter1_Sheet1.Cells[rowIndex, (int)ColSet.�ֶ�����].Text;
                        }
                        else
                        {
                            groupFields += customGroup;
                        }
                        groupFields += ",\n       ";
                    }
                    else
                    {
                        isNeedGroup = true;
                        field = "SUM(" + field + ")";
                    }
                }

                fieldAndcustomFields += (colIndex == 0 ? "" : "       ")
                + field
                + " " + customField
                + ",\n";

                //�ϼ�
                if (FS.FrameWork.Function.NConvert.ToBoolean(neuFpEnter1_Sheet1.Cells[rowIndex, (int)ColSet.��Ӻϼ�].Value))
                {
                    sumCols += colIndex.ToString() + ",";
                }

                //����
                if (FS.FrameWork.Function.NConvert.ToBoolean(neuFpEnter1_Sheet1.Cells[rowIndex, (int)ColSet.����].Value))
                {
                    sortCols += colIndex.ToString() + ",";
                }

                //����
                if (FS.FrameWork.Function.NConvert.ToBoolean(neuFpEnter1_Sheet1.Cells[rowIndex, (int)ColSet.����].Value))
                {
                    if (string.IsNullOrEmpty(customField))
                    {
                        filters += neuFpEnter1_Sheet1.Cells[rowIndex, (int)ColSet.�ֶ�����].Text + ",";
                    }
                    else
                    {
                        filters += customField + ",";
                    }
                }

            }

            //û��ѡ���ֶΣ����ѯȫ��
            if (string.IsNullOrEmpty(fieldAndcustomFields))
            {
                for (int row = 0; row < this.neuFpEnter1_Sheet1.RowCount; row++)
                {
                    this.neuFpEnter1_Sheet1.Cells[row, (int)ColSet.ѡ��].Value = true;
                }
                sql = "SELECT * FROM " + neuFpEnter1_Sheet1.Cells[0, (int)ColSet.��ͼ].Text + "\n";
            }
            else
            {
                sql = string.Format(sql, fieldAndcustomFields.TrimEnd(',', '\n'));
            }

            //����
            if (!string.IsNullOrEmpty(groupFields) && isNeedGroup)
            {
                groupFields = "\nGROUP  BY " + groupFields;
                groupFields = groupFields.TrimEnd(',', '\n', ' ');
            }
            else
            {
                groupFields = "";
            }

            //ǿ��where����
            string sysWhere = this.myPreDefine.User01;
            if (!string.IsNullOrEmpty(sysWhere))
            {
                if (sysWhere.IndexOf("Local.CustomQuery") != -1)
                {
                    //�����ǿ��where��sql�����滻,���»�ȡ
                    string view = "" + neuFpEnter1_Sheet1.Cells[0, (int)ColSet.��ͼ].Text + "";
                    string getViewSql = "select sql_where from com_customquery_viewinfo where view_name = '{0}'";
                    getViewSql = string.Format(getViewSql, view.ToLower());
                    sysWhere = dbMgr.ExecSqlReturnOne(getViewSql);
                    if (sysWhere == "-1")
                    {
                        getViewSql = string.Format(getViewSql, view);
                        sysWhere = dbMgr.ExecSqlReturnOne(getViewSql);
                        if (sysWhere == "-1")
                        {
                            this.richTextBox1.SuperText = "��ȡǿ��������������\n" + dbMgr.Err;
                            return null;
                        }
                    }
                }

            }
            if (string.IsNullOrEmpty(sysWhere))
            {
                sysWhere = "\nWHERE  1=1 ";
            }

            //����Ȩ��
            if (!string.IsNullOrEmpty(this.myPreDefine.User02))
            {
                // sysWhere = sysWhere.Replace("usercode", this.dbMgr.Operator.ID);
            }

            //sql���
            string errInfo = "";
            string checkedSql = sql + sysWhere;
            checkedSql += customWheres + groupFields;
            if (!this.CheckSQL(checkedSql, ref errInfo))
            {
                this.richTextBox1.SuperText = errInfo;
                this.neuTabControl1.SelectedIndex = 2;
                return null;
            }
            this.richTextBox1.SuperText = errInfo;

            sql += sysWhere + customWheres;//ǿ��where

            sql += groupFields;//����

            FS.FrameWork.Models.NeuObject o = new FS.FrameWork.Models.NeuObject();

            o.ID = this.GetSQLID();
            o.Memo = this.myPreDefine.Name;
            o.User03 = this.myPreDefine.Name;
            o.User01 = "";
            o.User02 = "�Զ����ѯ";
            o.Name = sql;

            if (string.IsNullOrEmpty(filters))
            {
                this.myReport.FilerType = FS.HISFC.Components.Common.Controls.ucPrivePowerReport.EnumFilterType.������;
            }
            else
            {
                this.myReport.FilerType = FS.HISFC.Components.Common.Controls.ucPrivePowerReport.EnumFilterType.���ܹ���;
            }


            //��ѯ����������Ը�ֵ
            this.myReport.SQL = o.Name;
            this.myReport.Filters = filters;
            this.myReport.SumColIndexs = sumCols;
            this.myReport.SortColIndexs = sortCols;
            this.myReport.RightAdditionTitle = "";
            this.myReport.SQLIndexs = o.ID;

            this.myPreDefine.User03 = o.Name;

            return o;

        }

        /// <summary>
        /// ��ȡSQL�������
        /// </summary>
        /// <returns></returns>
        private string GetSQLID()
        {
            if (string.IsNullOrEmpty(this.myPreDefine.ID) && !string.IsNullOrEmpty(this.myPreDefine.User01))
            {
                return this.myPreDefine.User01;
            }
            string SQLID = "Local.CustomQuery." + this.dbMgr.Operator.ID + "." + this.GetID();
            return SQLID;
        }

        /// <summary>
        /// ��ȡ��ˮ��
        /// </summary>
        /// <returns></returns>
        private string GetID()
        {
            return
               System.DateTime.Now.Year.ToString()
               + System.DateTime.Now.Month.ToString().PadLeft(2, '0')
               + System.DateTime.Now.Day.ToString().PadLeft(2, '0')
               + System.DateTime.Now.Hour.ToString().PadLeft(2, '0')
               + System.DateTime.Now.Minute.ToString().PadLeft(2, '0')
               + System.DateTime.Now.Second.ToString().PadLeft(2, '0')
               + System.DateTime.Now.Millisecond.ToString().PadLeft(3, '0');
        }
        /// <summary>
        ///����һ��SQL��Ϣ 
        /// </summary>
        /// <param name="sqlManagerObject"></param>
        private int SaveSqlInfo(FS.FrameWork.Models.NeuObject sqlObject, ref string errInfo)
        {
            int iReturn = 0;
            string strSql = "insert into COM_SQL(ID,MEMO,TYPE,SPELL_CODE,MODUAL,INPUT,OUTPUT) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
            try
            {
                strSql = string.Format(strSql, sqlObject.ID, "�û�����", this.dbMgr.Operator.ID, sqlObject.User01, sqlObject.User02, "", "");
                //strSql = string.Format(strSql, sqlObject.ID, sqlObject.Memo, sqlObject.User03, sqlObject.User01, sqlObject.User02, "", "");
                iReturn = this.dbMgr.ExecNoQuery(strSql);

            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = e.Message;
                return -1;
            }

            string StrSql = "";
            StrSql = "Update COM_SQL set  NAME=:a where id='{0}'";

            try
            {
                StrSql = string.Format(StrSql, sqlObject.ID);

                if (sqlObject.Name != "")
                {
                    if (this.dbMgr.InputLong(StrSql, sqlObject.Name) == -1)
                    {
                        errInfo = dbMgr.Err;
                        return -1;
                    }
                }
                else
                {
                    errInfo = "SQLΪ��";
                    return -1;
                }
            }
            catch (Exception e)
            {
                errInfo = e.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ���涨����Ϣ
        /// </summary>
        /// <param name="sqlObject"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        private int SaveDefineInfo(string sqlID, string defineXML, ref string errInfo)
        {
            int iReturn = 0;
            string strSql = "";
            string shareType = "Owner";
            if (this.rbDept.Checked)
            {
                shareType = "Dept";
            }
            if (this.rbAll.Checked)
            {
                shareType = "All";
            }
            if (dbMgr.Sql.GetSql("Local.CustomQuery.InsertDefineInfo", ref strSql) == -1)
            {
                errInfo = dbMgr.Err;
                return -1;
            }
            try
            {

                //����
                this.tvCustomQuery.SelectedNode.EndEdit(false);
                strSql = string.Format(strSql, this.myPreDefine.ID,
                    dbMgr.Operator.ID,
                    this.tvCustomQuery.SelectedNode.Parent.Text,
                    this.tvCustomQuery.SelectedNode.Text,
                    sqlID,
                    dbMgr.Operator.ID,
                    shareType);
                iReturn = this.dbMgr.ExecNoQuery(strSql);

                //����
                if (iReturn == -1)
                {
                    if (dbMgr.Sql.GetSql("Local.CustomQuery.UpdateDefineInfo", ref strSql) == -1)
                    {
                        errInfo = dbMgr.Err;
                        return -1;
                    }

                    strSql = string.Format(strSql, sqlID, this.tvCustomQuery.SelectedNode.Parent.Text,
                        this.tvCustomQuery.SelectedNode.Text, dbMgr.Operator.ID, shareType);

                    iReturn = this.dbMgr.ExecNoQuery(strSql);
                    if (iReturn != 1)
                    {
                        errInfo = dbMgr.Err;
                        return -1;
                    }
                }
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = e.Message;
                return -1;
            }

            //����long�ֶ�
            string StrSql = "";
            StrSql = "Update com_customquery_defineinfo set  define_xml =:a where sql_id='{0}'";

            try
            {
                StrSql = string.Format(StrSql, sqlID);

                if (defineXML != "")
                {
                    if (this.dbMgr.InputLong(StrSql, defineXML) == -1)
                    {
                        errInfo = dbMgr.Err;
                        return -1;
                    }
                }
            }
            catch (Exception e)
            {
                errInfo = e.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ɾ��һ����Ϣ
        /// </summary>
        /// <param name="sqlID"></param>
        /// <param name="id"></param>
        private int DeleteSqlInfo(string sqlID)
        {
            string strSql = "delete from COM_SQL where ID='{0}'";
            try
            {
                strSql = string.Format(strSql, sqlID);
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                if (dbMgr.ExecNoQuery(strSql) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.ShowMessageBox("ɾ��SQL��������" + dbMgr.Err, "����");
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.Commit();

            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.ShowMessageBox("ɾ��SQL��������" + e.Message, "����");

                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ��ʾ�û�����Ĳ�ѯ��Ϣ
        /// </summary>
        /// <param name="obj"></param>
        private void ShowDefine(FS.FrameWork.Models.NeuObject obj)
        {

            if (obj == null)
            {
                return;
            }

            string sql = "";

            System.Data.DataSet ds = new DataSet();
            if (string.IsNullOrEmpty(this.myPreDefine.ID))
            {
                this.neuTabControl1.SelectedIndex = 1;
                try
                {
                    //string path = Application.StartupPath + "\\Profile\\CustomQuery\\" + this.dbMgr.Operator.ID;
                    //string fileName = path
                    //        + "\\"
                    //        + obj.User01.Substring(obj.User01.LastIndexOf('.') + 1, obj.User01.Length - obj.User01.LastIndexOf('.') - 1)
                    //        + ".xml";

                    ////�ȴӱ��ض�ȡ
                    //if (System.IO.File.Exists(fileName))
                    //{
                    //    ds.ReadXml(fileName);
                    //    this.neuFpEnter1_Sheet1.DataSource = ds;
                    //    return;
                    //}
                    //else
                    //{
                    //���ص�����
                    //if (!System.IO.Directory.Exists(path))
                    //{
                    //    System.IO.Directory.CreateDirectory(path);
                    //}

                    sql = @"select define_xml from com_customquery_defineinfo where sql_id = '{0}'";
                    sql = string.Format(sql, obj.User01);
                    string xml = dbMgr.ExecSqlReturnOne(sql);
                    if (xml == "-1")
                    {
                        this.ShowMessageBox("��ȡ�Զ���xml��������" + dbMgr.Err, "����");
                        return;
                    }

                    //System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName, false);
                    //sw.Write(xml);
                    //sw.Close();
                    //ds.ReadXml(fileName);

                    System.IO.StringReader sr = new System.IO.StringReader(xml);
                    ds.ReadXml(sr);

                    this.neuFpEnter1_Sheet1.DataSource = ds;
                    return;
                    //}
                }
                catch (Exception ex)
                {
                    this.ShowMessageBox(ex.Message, "����");
                    return;
                }
            }
            else
            {
                this.neuTabControl1.SelectedIndex = 0;
            }

            sql = @"select distinct
                            table_name ��ͼ,
                            column_name �ֶ�����,
                              '' ѡ��,
                              '' �ֶα���,
                              '' ����,
                              '' ��Ӻϼ�,
                              '' ����,
                              '' �����ֶ�,
                              '' �������,
                              '' ƽ����,
                              '' �ֶα��ʽ,
                              '' �������ʽ,
                              '' ��ע,
                              '' ˳���
                            from all_col_comments
                            where table_name = '{0}'";
            sql = string.Format(sql, obj.ID.ToUpper());
            if (this.dbMgr.ExecQuery(sql, ref ds) == -1)
            {
                MessageBox.Show("" + this.dbMgr.Err);
                return;
            }

            this.neuFpEnter1_Sheet1.DataSource = ds;
        }

        /// <summary>
        /// ����Farpoint
        /// </summary>
        private void SetFP()
        {
            if (System.IO.File.Exists(Application.StartupPath + "\\Profile\\CustomQuery\\PreDefine.xml"))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuFpEnter1_Sheet1, Application.StartupPath + "\\Profile\\CustomQuery\\PreDefine.xml");
            }
            else
            {
                FarPoint.Win.Spread.CellType.CheckBoxCellType c = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                this.neuFpEnter1_Sheet1.Columns[(int)ColSet.����].CellType = c;
                this.neuFpEnter1_Sheet1.Columns[(int)ColSet.��Ӻϼ�].CellType = c;
                this.neuFpEnter1_Sheet1.Columns[(int)ColSet.����].CellType = c;
                this.neuFpEnter1_Sheet1.Columns[(int)ColSet.�������].CellType = c;
                this.neuFpEnter1_Sheet1.Columns[(int)ColSet.ѡ��].CellType = c;
                this.neuFpEnter1_Sheet1.Columns[(int)ColSet.ƽ����].CellType = c;


            }

            FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
            t.ReadOnly = true;
            this.neuFpEnter1_Sheet1.Columns[(int)ColSet.��ͼ].CellType = t;
            this.neuFpEnter1_Sheet1.Columns[(int)ColSet.�ֶ�����].CellType = t;

            this.neuFpEnter1_Sheet1.Columns[(int)ColSet.�ֶ�����].ShowSortIndicator = true;
            this.neuFpEnter1_Sheet1.Columns[(int)ColSet.��ע].ShowSortIndicator = true;
            this.neuFpEnter1_Sheet1.Columns[(int)ColSet.�ֶ�����].AllowAutoSort = true;
            this.neuFpEnter1_Sheet1.Columns[(int)ColSet.��ע].AllowAutoSort = true;

            this.neuFpEnter1_Sheet1.Columns[(int)ColSet.�����ֶ�].Width = 0F;
            this.neuFpEnter1_Sheet1.ColumnHeader.Rows[0].Height = 36f;

            //int sortColumnIndex = this.neuFpEnter1_Sheet1.Columns.Count;
            //this.neuFpEnter1_Sheet1.Columns.Add(sortColumnIndex, 1);
            //this.neuFpEnter1_Sheet1.Columns[sortColumnIndex].Label = "˳���";

            //for (int i = 0; i < this.neuFpEnter1_Sheet1.Rows.Count; i++)
            //{
            //    this.neuFpEnter1_Sheet1.Cells[i, sortColumnIndex].Text = (i + 1).ToString();
            //}

            this.neuFpEnter1_Sheet1.SortRows((int)ColSet.˳���, true, false);

        }

        /// <summary>
        /// ��ʾMessageBox
        /// </summary>
        /// <param name="text">����</param>
        /// <param name="caption">����</param>
        private void ShowMessageBox(string text, string caption)
        {
            MessageBox.Show(FS.FrameWork.Management.Language.Msg(text), FS.FrameWork.Management.Language.Msg(caption));
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        private int Save()
        {
            this.neuFpEnter1_Sheet1.SortRows((int)ColSet.˳���,true, false);

            //�Զ��屣��
            FS.FrameWork.Models.NeuObject o = this.GetSQL();
            if (o == null)
            {
                return -1;
            }
            string errInfo = "";

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.dbMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (this.SaveSqlInfo(o, ref errInfo) != -1)
            {
                try
                {
                    System.Data.DataSet ds = this.neuFpEnter1_Sheet1.DataSource as System.Data.DataSet;
                    if (ds != null)
                    {
                        System.IO.StringWriter sw = new System.IO.StringWriter();

                        ds.WriteXml(sw, XmlWriteMode.WriteSchema);
                        string xml = sw.ToString();
                        sw.Close();

                        if (this.SaveDefineInfo(o.ID, xml, ref errInfo) != -1)
                        {
                            this.myPreDefine.ID = "";
                            this.myPreDefine.User01 = o.ID;
                            this.myPreDefine.User03 = o.Name;
                        }
                        else
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            this.ShowMessageBox(errInfo, "����");
                            return -1;
                        }
                    }

                }
                catch (Exception ex)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.ShowMessageBox(ex.Message, "����");
                    return -1;
                }
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.ShowMessageBox(errInfo, "����");
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <returns></returns>
        private int Query()
        {
            if (this.tvCustomQuery == null)
            {
                MessageBox.Show( "��ѡ������ѯ����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return -1;
            }

            if (this.neuTabControl1.SelectedIndex == 0)
            {
                if (!string.IsNullOrEmpty(this.myPreDefine.ID))//Ԥ���壬�½�����ʱ
                {
                    //������sql��ֵ
                    if (this.GetSQL() == null)
                    {
                        return -1;
                    }
                    this.neuTabControl1.SelectedIndex = 1;
                }
                else
                {
                    if (this.neuFpEnter1_Sheet1.RowCount > 0)
                    {
                        if (this.GetSQL() == null)
                        {
                            return -1;
                        }
                        this.neuTabControl1.SelectedIndex = 1;
                    }
                }
            }
            else if (this.neuTabControl1.SelectedIndex == 2)
            {
                return -1;
            }
            else//�Ѿ�����
            {
                if (string.IsNullOrEmpty(this.myPreDefine.User03) && !string.IsNullOrEmpty(this.myPreDefine.User01))
                {
                    if (this.dbMgr.Sql.GetSql(this.myPreDefine.User01, ref this.myPreDefine.User03) == -1)
                    {
                        string sql = @"select name from com_sql where id = '{0}'";
                        sql = string.Format(sql, this.myPreDefine.User01);
                        this.myPreDefine.User03 = this.dbMgr.ExecSqlReturnOne(sql);
                    }
                }
                if (string.IsNullOrEmpty(myReport.Filters) || string.IsNullOrEmpty(myReport.SumColIndexs))
                {
                    string filters = "";
                    string sumCols = "";
                    this.GetFilterAndSumCol(ref filters, ref sumCols);
                    myReport.SumColIndexs = sumCols;
                    myReport.Filters = filters;
                }
                this.myReport.SQL = this.myPreDefine.User03;
                this.myReport.SQLIndexs = this.myPreDefine.User01;
            }

            //�����ļ�
            string fileName = Application.StartupPath
                            + "\\Profile\\CustomQuery\\"
                            + this.dbMgr.Operator.ID
                            + "\\"
                            + this.GetSQLID()
                            + ".xml";

            this.myReport.SettingFilePatch = fileName;

            //����Ѿ���ֵ����ͷ����
            if (!System.IO.File.Exists(fileName))
            {
                this.myReport.fpSpread1_Sheet1.RowCount = 0;
                this.myReport.fpSpread1_Sheet1.Columns.Count = 0;
                this.myReport.fpSpread1_Sheet1.DataSource = null;
            }


            this.myReport.lbMainTitle.Text = this.tvCustomQuery.SelectedNode.Text;

            if (!string.IsNullOrEmpty(myReport.SQL) && !string.IsNullOrEmpty(this.myPreDefine.User02) && myReport.SQL.IndexOf("usercode") != -1)
            {
                myReport.SQL = myReport.SQL.Replace("usercode", dbMgr.Operator.ID);
            }
            if (string.IsNullOrEmpty(this.myReport.SQL) || myReport.SQL == "-1")
            {
                this.ShowMessageBox("���岻��ȷ", "����");
                return -1;
            }

            if (this.myReport.QueryData() == -1)
            {
                return -1;
            }

            this.myReport.lbAdditionTitleMid.Text = "";
            this.myReport.lbAdditionTitleRight.Text = "";

            return 1;
        }

        /// <summary>
        /// ɾ�� ������ɾ�����˹���Ĳ�ѯ����ɾ��com_sql�е�sql
        /// </summary>
        /// <returns></returns>
        private int Delete(ref string errInfo)
        {
            string text = this.tvCustomQuery.SelectedNode.Text;
            System.Windows.Forms.DialogResult dr = MessageBox.Show(this, "��ȷʵҪɾ��" + text + "��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No)
            {
                return 1;
            }
            TreeNode node = this.tvCustomQuery.SelectedNode;

            string sql = "";
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.dbMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //ҵ����ڵ�ɾ��
            if (node.Parent == null)
            {
                if (node.Nodes.Count == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return 1;
                }
                sql = @"delete from com_customquery_defineinfo where view_name = '{0}' and owner = '{1}'";
                sql = string.Format(sql, this.myPreDefine.ID, dbMgr.Operator.ID);
                if (dbMgr.ExecQuery(sql) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = dbMgr.Err;
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.Commit();

                node.Nodes.Clear();
                return 1;
            }

            //�ļ���
            if (!string.IsNullOrEmpty(this.myPreDefine.ID))
            {
                //�½����ļ���ֱ��ɾ��������ɾ���ļ�������
                if (node.Nodes.Count > 0)
                {
                    sql = @"delete from com_customquery_defineinfo where view_name = '{0}' and owner = '{1}' and type = '{2}'";
                    sql = string.Format(sql, this.myPreDefine.ID, dbMgr.Operator.ID, this.myPreDefine.Name);
                    if (dbMgr.ExecQuery(sql) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errInfo = dbMgr.Err;
                        return -1;
                    }
                }
                node.Parent.Nodes.Remove(node);

            }
            else//������ѯ
            {
                //û�б����ֱ��ɾ��
                if (!string.IsNullOrEmpty(this.myPreDefine.User03))
                {
                    sql = @"delete from com_customquery_defineinfo where sql_id = '{0}' and owner = '{1}'";
                    sql = string.Format(sql, this.myPreDefine.User01, dbMgr.Operator.ID);
                    if (dbMgr.ExecQuery(sql) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errInfo = dbMgr.Err;
                        return -1;
                    }
                }
                node.Parent.Nodes.Remove(node);
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            return 1;
        }

        private void DeleteDefine()
        {
            string errInfo = "";
            if (this.Delete(ref errInfo) == -1)
            {
                this.ShowMessageBox(errInfo, "����");
            }
            else
            {
                this.ShowMessageBox("ɾ���ɹ���", "��ʾ");
            }
        }
        #endregion

        #region �¼�
        protected override void OnLoad(EventArgs e)
        {
            if (!(FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).IsManager && !this.OperCode.Contains(FS.FrameWork.Management.Connection.Operator.ID.ToString()))
            {//�Զ�����ϲ�ѯ����׼��ȥ�����������ò��������� 20091213
                this.neuTabControl1.TabPages.RemoveAt(0);
            }

            this.neuTabControl1.SelectedIndexChanged += new EventHandler( neuTabControl1_SelectedIndexChanged );

            this.neuFpEnter1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;

            this.toolBarService.SetToolButtonEnabled( "����", false );
            this.toolBarService.SetToolButtonEnabled( "��ѯ", true );

            base.OnLoad(e);
        }

        void neuTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.neuTabControl1.SelectedTab == this.tpSet)      //����
            {
                this.toolBarService.SetToolButtonEnabled( "����", true );
                this.toolBarService.SetToolButtonEnabled( "��ѯ", false );
            }
            else if (this.neuTabControl1.SelectedTab == this.tpQuery)   //��ѯ
            {                
                this.toolBarService.SetToolButtonEnabled( "����", false );
                this.toolBarService.SetToolButtonEnabled( "��ѯ", true );
            }
        }

        protected override int OnSave(object sender, object neuObject)
        {
            if (this.Save() == 1)
            {
                //��ֹ�ٻ�ȡsql���
                this.neuTabControl1.SelectedIndex = 1;

                //ɾ�������ļ�
                try
                {
                    if (System.IO.File.Exists(this.myReport.SettingFilePatch))
                    {
                        System.IO.File.Delete(this.myReport.SettingFilePatch);
                    }

                    ////��ѯ
                    this.Query();
                }
                catch
                { }

                //�Զ����������ļ�
                try
                {
                    FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(myReport.fpSpread1_Sheet1, myReport.SettingFilePatch);
                }
                catch
                {
                }

                this.ShowMessageBox(this.tvCustomQuery.SelectedNode.Text + "����ɹ�", "��ʾ");
            }
            return 1;
            //return base.OnSave(sender, neuObject);
        }
        protected override int OnQuery(object sender, object neuObject)
        {
            return this.Query();
        }
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (e == null || e.Tag == null)
            {
                return -1;
            }

            FS.FrameWork.Models.NeuObject obj = e.Tag as FS.FrameWork.Models.NeuObject;
            this.myPreDefine = obj;

            if (this.tvCustomQuery == null)
            {
                this.tvCustomQuery = this.tv as tvCustomQuery;
                this.tvCustomQuery.DeleteDefineHandler += new tvCustomQuery.DeleteDefine(this.DeleteDefine);
            }

            //�����Զ����ѯ��������
            if (e.ImageIndex < 4)
            {
                if (this.neuFpEnter1_Sheet1.DataSource != null && this.neuFpEnter1_Sheet1.DataSource.GetType() == typeof(System.Data.DataSet))
                {
                    (this.neuFpEnter1_Sheet1.DataSource as System.Data.DataSet).Clear();
                }
                this.neuFpEnter1_Sheet1.RowCount = 0;
                this.SetFP();
                this.neuTabControl1.SelectedIndex = 0;

                return 1;
            }

            //��ʾ����
            this.ShowDefine(obj);
            this.SetFP();

            //if (this.neuTabControl1.SelectedIndex == 1)
            //{
            //    this.Query();
            //}

            return base.OnSetValue(neuObject, e);
        }
        protected override int OnPrint(object sender, object neuObject)
        {
            //this.myReport.PrintData();
            //������һ�䣬ʹ����������ʵ�ִ�ӡ���� by Sunjh 2009-3-13 {699DBE34-5DEA-4ba8-AFDD-A04364CFC8AD}
            FS.FrameWork.WinForms.Classes.Print pp = new FS.FrameWork.WinForms.Classes.Print();
            pp.PrintPreview(this.myReport.neuGroupBox2);
            return base.OnPrint(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            this.myReport.ExportData();
            return base.Export(sender, neuObject);
        }
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.SetReportControlPerporty();
            this.tpQuery.Controls.Add(myReport);

            this.neuFpEnter1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(neuFpEnter1_CellDoubleClick);
            this.neuFpEnter1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuFpEnter1_ColumnWidthChanged);

            this.toolBarService.AddToolButton("ɾ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);

            return this.toolBarService;
        }

        void neuFpEnter1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
            {
                if (e.Column == (int)ColSet.ѡ�� && this.neuFpEnter1_Sheet1.RowCount > 0)
                {
                    bool isChoosed = FS.FrameWork.Function.NConvert.ToBoolean(this.neuFpEnter1_Sheet1.Cells[0, e.Column].Value);

                    for (int row = 0; row < this.neuFpEnter1_Sheet1.RowCount; row++)
                    {
                        this.neuFpEnter1_Sheet1.Cells[row, e.Column].Value = isChoosed;
                    }
                }
            }
        }
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "ɾ��")
            {
                DeleteDefine();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }
        void neuFpEnter1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuFpEnter1_Sheet1, Application.StartupPath + "\\Profile\\CustomQuery\\PreDefine.xml");
        }
        #endregion

        /// <summary>
        /// ��ö��
        /// </summary>
        enum ColSet
        {
            ��ͼ,
            �ֶ�����,
            ѡ��,
            �ֶα���,
            ����,
            ��Ӻϼ�,
            ����,
            �����ֶ�,
            �������,
            ƽ����,
            �ֶα��ʽ,
            �������ʽ,
            ��ע,
            ˳���
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int nowIndex = this.neuFpEnter1_Sheet1.ActiveRowIndex;
            if (nowIndex == 0)
            {
                return;
            }

            string currentSort = this.neuFpEnter1_Sheet1.Cells[nowIndex, (int)ColSet.˳���].Text;

            this.neuFpEnter1_Sheet1.Cells[nowIndex, (int)ColSet.˳���].Text = this.neuFpEnter1_Sheet1.Cells[nowIndex - 1, (int)ColSet.˳���].Text;
            this.neuFpEnter1_Sheet1.Cells[nowIndex, (int)ColSet.˳���].Text = currentSort;

            this.neuFpEnter1_Sheet1.SortRows((int)ColSet.˳���, true, false);
        }
    }
}
