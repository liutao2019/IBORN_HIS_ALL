using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    public partial class ucUndrugDictionary : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucUndrugDictionary()
        {
            InitializeComponent();
        }

        #region ����

        private List<FS.HISFC.Models.Fee.Item.Undrug> undrugList = new List<FS.HISFC.Models.Fee.Item.Undrug>();

        private DataSet dsUndrug = new DataSet();
        private DataTable dtUndrug = new DataTable();
        private DataView dvUndrug = new DataView();
        private string filterInput = "";
        private string mainSettingFilePath = FS.FrameWork.WinForms.Classes.Function.SettingPath + "UndrugDictionary.xml";
        #endregion

        #region ˽�з���

        /// <summary>
        /// ���ã���
        /// </summary>
        private void InitFrp()
        {
            dsUndrug = new DataSet();
            dtUndrug = new DataTable();
            dvUndrug = new DataView();
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڳ�ʼ��������Ժ�.....");
            if (File.Exists(this.mainSettingFilePath))
            {
                
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(this.mainSettingFilePath, this.dtUndrug, ref this.dvUndrug, this.fpSpread1_Sheet1);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1_Sheet1, this.mainSettingFilePath);
            }
            else
            {
                this.dtUndrug.Columns.AddRange(new DataColumn[] 
                {
                    new DataColumn("��ҩƷ����", typeof(string)),
                    new DataColumn("��ҩƷ����", typeof(string)),
                    new DataColumn("������", typeof(string)),
                    new DataColumn("���ʱ���", typeof(string)),
                    new DataColumn("ϵͳ���", typeof(string)),
                    new DataColumn("��С������", typeof(string)),
                    new DataColumn("ƴ����", typeof(string)),
                    new DataColumn("�����", typeof(string)),
                    new DataColumn("������", typeof(string)),
                    new DataColumn("�Ƽ۵�λ", typeof(string)),
                    new DataColumn("��Ч�Ա�־", typeof(string)),
                    new DataColumn("���", typeof(string)),
                    new DataColumn("ִ�п���", typeof(string)),
                    new DataColumn("Ĭ�ϼ�鲿λ", typeof(string)),
                    new DataColumn("�۸�", typeof(decimal)),
                    new DataColumn("�����", typeof(decimal)),
                    new DataColumn("��ͯ��", typeof(decimal)),
                    new DataColumn("ȷ�ϱ�־", typeof(bool)),
                    
                    new DataColumn("ͨ����", typeof(string)),
                    new DataColumn("ͨ����ƴ����", typeof(string)),
                    new DataColumn("ͨ���������", typeof(string)),
                    new DataColumn("ͨ�����Զ�����", typeof(string))
                    
                });

                this.dvUndrug = new DataView(this.dtUndrug);

                this.fpSpread1.DataSource = this.dvUndrug;
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        /// <summary>
        /// ���ط�ҩƷ��Ϣ
        /// </summary>
        private void QueryUndrug()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ��ط�ҩƷ��Ϣ�����Ժ�.....");
            Application.DoEvents();
            undrugList = CacheManager.FeeIntegrate.QueryAllItemsList();
            
            this.dtUndrug.Clear();
            foreach (FS.HISFC.Models.Fee.Item.Undrug undrug in undrugList)
            {
                DataRow row = this.dtUndrug.NewRow();

                row["��ҩƷ����"] = undrug.ID;
                row["��ҩƷ����"] = undrug.Name;
                row["������"] = undrug.GBCode;
                row["���ʱ���"] = undrug.NationCode;
                row["ϵͳ���"] = undrug.SysClass.Name;
                row["��С������"] = undrug.MinFee.Name;
                row["ƴ����"] = undrug.SpellCode;
                row["�����"] = undrug.WBCode;
                row["������"] = undrug.UserCode;
                row["�Ƽ۵�λ"] = undrug.PriceUnit;

                if (undrug.ValidState == "1")
                {
                    row["��Ч�Ա�־"] = "��Ч";
                }
                else
                {
                    row["��Ч�Ա�־"] = "��Ч";
                }

                row["���"] = undrug.Specs;
                if (string.IsNullOrEmpty(undrug.ExecDept) || undrug.ExecDept == "ALL")
                {
                    row["ִ�п���"] = "ȫ��";
                }
                else
                {
                    string allExecDept = string.Empty;
                    string[] allDept = undrug.ExecDept.Split('|');
                    for (int i = 0; i < allDept.Length; i++)
                    {
                        allExecDept += FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(allDept[i]) + "|";
                    }
                    row["ִ�п���"] = allExecDept.TrimEnd('|');
                }
                row["Ĭ�ϼ�鲿λ"] = undrug.CheckBody;
                row["�۸�"] = undrug.Price;
                row["�����"] = undrug.SpecialPrice;
                row["��ͯ��"] = undrug.ChildPrice;
                row["ȷ�ϱ�־"] = undrug.IsNeedConfirm;


                row["ͨ����"] = undrug.NameCollection.OtherName;
                row["ͨ����ƴ����"] = undrug.NameCollection.OtherSpell.SpellCode;
                row["ͨ���������"] = undrug.NameCollection.OtherSpell.WBCode;
                row["ͨ�����Զ�����"] = undrug.NameCollection.OtherSpell.UserCode;

                this.dtUndrug.Rows.Add(row);
            }
            this.dtUndrug.AcceptChanges();

            for (int row = 0; row < fpSpread1_Sheet1.RowCount; row++)
            {
                fpSpread1_Sheet1.Rows[row].BackColor = Color.White;
                if (fpSpread1_Sheet1.Cells[row, 10].Text == "��Ч")
                {
                    fpSpread1_Sheet1.Rows[row].BackColor = Color.Red;
                }
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }
                
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            if (!this.DesignMode)
            {
                this.InitFrp();
                this.QueryUndrug();

                try
                {
                    if (System.IO.File.Exists(this.mainSettingFilePath))
                    {
                        FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1_Sheet1, this.mainSettingFilePath);
                    }
                    else
                    {
                        FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1_Sheet1, this.mainSettingFilePath);
                    }


                }
                catch (Exception ex)
                {
                    //MessageBox.Show("��ȡ������ʾ�����ļ�ʱ����! ���˳�����" + ex.Message);
                    //GC.Collect();
                    //return;
                }
            }
            return base.OnInit(sender, neuObject, param);
        }

        #endregion
                
        #region �¼�
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string queryCode = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtFilter.Text);

            queryCode = FS.FrameWork.Public.String.TakeOffSpecialChar(queryCode, "'", "%");
            
            queryCode = queryCode + "%";
            this.filterInput = "((ƴ���� LIKE '" + queryCode + "') OR " +
                "(����� LIKE '" + queryCode + "') OR " +
                "(������ LIKE '" + queryCode + "') OR " +
                "(������ LIKE '" + queryCode + "') OR " +

                "(ͨ���� LIKE '" + queryCode + "') OR " +
                "(ͨ����ƴ���� LIKE '" + queryCode + "') OR " +
                "(ͨ��������� LIKE '" + queryCode + "') OR " +
                "(ͨ�����Զ����� LIKE '" + queryCode + "') OR " +

                "(��ҩƷ���� LIKE '" + queryCode + "') OR " +
                "(��ҩƷ���� LIKE '" + queryCode + "') )";

            this.dvUndrug.RowFilter = filterInput;
            if (System.IO.File.Exists(this.mainSettingFilePath))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1_Sheet1, this.mainSettingFilePath);
            }

            for (int row = 0; row < fpSpread1_Sheet1.RowCount; row++)
            {
                fpSpread1_Sheet1.Rows[row].BackColor = Color.White;
                if (fpSpread1_Sheet1.Cells[row, 10].Text == "��Ч")
                {
                    fpSpread1_Sheet1.Rows[row].BackColor = Color.Red;
                }
            }
        }

        private void linkLblSet_Click(object sender, EventArgs e)
        {
            FS.HISFC.Components.Common.Controls.ucSetColumn uc = new FS.HISFC.Components.Common.Controls.ucSetColumn();
            uc.FilePath = this.mainSettingFilePath;
            uc.SetDataTable(this.mainSettingFilePath, this.fpSpread1_Sheet1);
            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��ʾ����";
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
            uc.DisplayEvent += new EventHandler(ucSetColumn_DisplayEvent);
            this.ucSetColumn_DisplayEvent(null, null);
        }

        private void ucSetColumn_DisplayEvent(object sender, EventArgs e)
        {
            this.InitFrp();
            this.QueryUndrug();
        }
        
        #endregion

        private void fpSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1_Sheet1, this.mainSettingFilePath);
        }
    }
}

