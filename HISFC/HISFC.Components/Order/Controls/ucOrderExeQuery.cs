using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Order.Controls
{
    public partial class ucOrderExeQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucOrderExeQuery()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                this.neuSpread1.ActiveSheetChanged += new EventHandler(neuSpread1_ActiveSheetChanged);
                neuSpread2.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(neuSpread2_CellDoubleClick);
            }
        }

        /// <summary>
        /// ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Order.ChargeBill myCharegeBill = new FS.HISFC.BizLogic.Order.ChargeBill();

        /// <summary>
        /// ҽ��ִ�е�����
        /// </summary>
        private FS.HISFC.Models.Order.ExecOrder myExeOrder = null;

        private ArrayList alExeOrder = null;

        //���ݻ�����Ϣ��
        private FS.HISFC.Models.RADT.PatientInfo patient;
        /// <summary>
        /// ҳ�����ԣ����մ������Ļ�����Ϣ
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            get
            {
                return this.patient;
            }
            set
            {
                this.patient = value;
                if (patient != null)
                {
                    this.ShowData(this.patient.ID);
                }
            }

        }

        /// <summary>
        /// �Ƿ��Զ����� ��ʽ�ļ�
        /// </summary>
        private bool isAutoSaveXML = true;

        /// <summary>
        /// �Ƿ��Զ����� ��ʽ�ļ�
        /// </summary>
        [Description("�Ƿ��Զ����� ��ʽ�ļ�"), Category("����")]
        public bool IsAutoSaveXML
        {
            get
            {
                return isAutoSaveXML;
            }
            set
            {
                isAutoSaveXML = value;
            }
        }

        DataSet myDataSetDrug = new DataSet();
        DataSet myDataSetUndrug = new DataSet();

        DataView myDataViewDrug = new DataView();//ҩƷ����
        DataView myDataViewUndrug = new DataView();//��ҩƷ����

        string filterInput = "1=1";	//�������������
        string filterExec  = "1=1";
        string filterValid = "1=1";	//�Ƿ���Ч��������
        string filterType = "1=1";//ҽ������

        string drugQuery   = FS.FrameWork.WinForms.Classes.Function.SettingPath + @"\ucOrderExeQuery_Drug.xml";
        string undrugQuery = FS.FrameWork.WinForms.Classes.Function.SettingPath + @"\ucOrderExeQuery_UnDrug.xml";

        /// <summary>
        /// ���ݲ���ʵ��
        /// </summary>
        /// <param name="neuObject"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (this.tv.CheckBoxes == true)
            {
                this.tv.CheckBoxes = false;
            }
            this.tv.ExpandAll();
            this.Patient = neuObject as FS.HISFC.Models.RADT.PatientInfo;
            return base.OnSetValue(neuObject, e);
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.patient != null)
            {
                this.ShowData(this.patient.ID);
            }

            return base.OnQuery(sender, neuObject);
        }

        /// <summary>
        /// ����ҩƷִ�е���ʾ��ʽ
        /// </summary>
        protected void SetFormatForDrug()
        {
            if (System.IO.File.Exists(this.drugQuery))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuSpread1_Sheet1, this.drugQuery);
                //this.RefreshDrugBackColor();
            }
            else
            {
                #region ȱʡ��
                this.neuSpread1_Sheet1.Columns.Get(0).Label = "���";
                this.neuSpread1_Sheet1.Columns.Get(0).Width = 56F;
                FarPoint.Win.Spread.CellType.CheckBoxCellType cellcbkBJ = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                this.neuSpread1_Sheet1.Columns[0].CellType = cellcbkBJ;
                this.neuSpread1_Sheet1.Columns[0].Locked = false;
                this.neuSpread1_Sheet1.Columns.Get(1).Label = "����ҽ��";
                this.neuSpread1_Sheet1.Columns.Get(1).Width = 56F;
                this.neuSpread1_Sheet1.Columns.Get(2).Label = "ҽ������";
                this.neuSpread1_Sheet1.Columns.Get(2).Width = 56F;
                this.neuSpread1_Sheet1.Columns.Get(3).Label = "��Ч";
                this.neuSpread1_Sheet1.Columns.Get(3).Width = 35F;
                this.neuSpread1_Sheet1.Columns.Get(4).Label = "����״̬";
                this.neuSpread1_Sheet1.Columns.Get(4).Width = 59F;
                this.neuSpread1_Sheet1.Columns.Get(5).Label = "ҩƷ����";
                this.neuSpread1_Sheet1.Columns.Get(5).Width = 117F;
                this.neuSpread1_Sheet1.Columns.Get(6).Label = "���";
                this.neuSpread1_Sheet1.Columns.Get(6).Width = 71F;
                this.neuSpread1_Sheet1.Columns.Get(7).Label = "����";
                this.neuSpread1_Sheet1.Columns.Get(7).Width = 60F;
                this.neuSpread1_Sheet1.Columns.Get(8).Label = "��λ";
                this.neuSpread1_Sheet1.Columns.Get(8).Width = 35F;
                this.neuSpread1_Sheet1.Columns.Get(9).Label = "Ӧִ��ʱ��";
                this.neuSpread1_Sheet1.Columns.Get(9).Width = 110F;
                this.neuSpread1_Sheet1.Columns.Get(10).Label = "�ֽ�ʱ��";
                this.neuSpread1_Sheet1.Columns.Get(10).Width = 60F;
                this.neuSpread1_Sheet1.Columns.Get(11).Label = "���˱��";
                this.neuSpread1_Sheet1.Columns.Get(11).Width = 56F;
                this.neuSpread1_Sheet1.Columns.Get(12).Label = "����ʱ��";
                this.neuSpread1_Sheet1.Columns.Get(12).Width = 110F;
                this.neuSpread1_Sheet1.Columns.Get(13).Label = "��ҩʱ��";
                this.neuSpread1_Sheet1.Columns.Get(13).Width = 110F;
                this.neuSpread1_Sheet1.Columns.Get(14).Label = "ҽ��ʱ��";
                this.neuSpread1_Sheet1.Columns.Get(14).Width = 110F;
                this.neuSpread1_Sheet1.Columns.Get(15).Label = "ֹͣʱ��";
                this.neuSpread1_Sheet1.Columns.Get(15).Width = 110F;
                this.neuSpread1_Sheet1.Columns.Get(16).Label = "Ƶ��";
                this.neuSpread1_Sheet1.Columns.Get(16).Width = 47F;
                this.neuSpread1_Sheet1.Columns.Get(17).Label = "ÿ�μ���";
                this.neuSpread1_Sheet1.Columns.Get(17).Width = 56F;
                this.neuSpread1_Sheet1.Columns.Get(18).Label = "��λ";
                this.neuSpread1_Sheet1.Columns.Get(18).Width = 35F;
                this.neuSpread1_Sheet1.Columns.Get(19).Label = "��װ��";
                this.neuSpread1_Sheet1.Columns.Get(19).Width = 53F;
                this.neuSpread1_Sheet1.Columns.Get(20).Label = "����";
                this.neuSpread1_Sheet1.Columns.Get(20).Width = 45F;
                this.neuSpread1_Sheet1.Columns.Get(21).Label = "�÷�";
                this.neuSpread1_Sheet1.Columns.Get(21).Width = 54F;
                this.neuSpread1_Sheet1.Columns.Get(22).Label = "ȡҩҩ��";
                this.neuSpread1_Sheet1.Columns.Get(22).Width = 111F;
                this.neuSpread1_Sheet1.Columns.Get(23).Label = "ҽ��˵��";
                this.neuSpread1_Sheet1.Columns.Get(23).Width = 74F;
                this.neuSpread1_Sheet1.Columns.Get(24).Label = "��ע";
                this.neuSpread1_Sheet1.Columns.Get(24).Width = 51F;
                this.neuSpread1_Sheet1.Columns.Get(25).Label = "ҽ����";
                this.neuSpread1_Sheet1.Columns.Get(25).Width = 70F;
                this.neuSpread1_Sheet1.Columns.Get(26).Label = "��Ϻ�";
                this.neuSpread1_Sheet1.Columns.Get(26).Width = 67F;
                this.neuSpread1_Sheet1.Columns.Get(27).Label = "ִ�к�";
                this.neuSpread1_Sheet1.Columns.Get(27).Width = 69F;
                this.neuSpread1_Sheet1.Columns.Get(28).Label = "����";
                this.neuSpread1_Sheet1.Columns.Get(28).Width = 38F;
                this.neuSpread1_Sheet1.Columns.Get(29).Label = "ִ�п���";
                this.neuSpread1_Sheet1.Columns.Get(29).Width = 127F;
                this.neuSpread1_Sheet1.Columns.Get(30).Label = "������";
                this.neuSpread1_Sheet1.Columns.Get(30).Width = 45F;
                this.neuSpread1_Sheet1.Columns.Get(31).Label = "��ҩ����";
                this.neuSpread1_Sheet1.Columns.Get(31).Width = 104F;
                this.neuSpread1_Sheet1.Columns.Get(32).Label = "��ҩ��";
                this.neuSpread1_Sheet1.Columns.Get(32).Width = 45F;
                this.neuSpread1_Sheet1.Columns.Get(33).Label = "ֹͣ��";
                this.neuSpread1_Sheet1.Columns.Get(33).Width = 45F;
                this.neuSpread1_Sheet1.Columns.Get(34).Label = "������";
                this.neuSpread1_Sheet1.Columns.Get(34).Width = 65F;
                this.neuSpread1_Sheet1.Columns.Get(35).Label = "��������ˮ��";
                //this.neuSpread1_Sheet1.Columns.Get(35).Visible = false;
                this.neuSpread1_Sheet1.Columns.Get(36).Label = "���͵���ӡ���";
                //this.neuSpread1_Sheet1.Columns.Get(36).Visible = false;
                this.neuSpread1_Sheet1.Columns.Get(37).Label = "��ӡʱ��";
                //this.neuSpread1_Sheet1.Columns.Get(37).Visible = false;
                this.neuSpread1_Sheet1.Columns.Get(38).Label = "���ʹ���";
                //this.neuSpread1_Sheet1.Columns.Get(38).Visible = false;
                this.neuSpread1_Sheet1.Columns.Get(39).Label = "ҩƷ����";
                //this.neuSpread1_Sheet1.Columns.Get(39).Visible = false;
                this.neuSpread1_Sheet1.Columns.Get(40).Label = "סԺ����";
                //this.neuSpread1_Sheet1.Columns.Get(40).Visible = false;
                this.neuSpread1_Sheet1.Columns.Get(41).Label = "����վ";
                //this.neuSpread1_Sheet1.Columns.Get(41).Visible = false;
                this.neuSpread1_Sheet1.Columns.Get(42).Label = "��������";
                //this.neuSpread1_Sheet1.Columns.Get(42).Visible = false;
                this.neuSpread1_Sheet1.Columns.Get(43).Label = "ƴ����";
                //this.neuSpread1_Sheet1.Columns.Get(43).Visible = false;
                this.neuSpread1_Sheet1.Columns.Get(44).Label = "�����";
                //this.neuSpread1_Sheet1.Columns.Get(44).Visible = false;

                //this.RefreshDrugBackColor();

                this.neuSpread1_Sheet1.DefaultStyle.Locked = true;
                this.neuSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
                this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
                this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
                this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
                this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 30F;
                //this.neuSpread1_Sheet1.SetColumnAllowAutoSort(-1, true);
                #endregion
            }

            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ExtendedSelect;
        }

        /// <summary>
        /// ���÷�ҩƷִ�е���ʾ��ʽ
        /// </summary>
        protected void SetFormatForUnDrug()
        {
            if (System.IO.File.Exists(this.undrugQuery))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuSpread2_Sheet1, this.undrugQuery);
            }
            else
            {
                #region ȱʡ����

                this.neuSpread2_Sheet1.Columns.Get(0).Label = "����ҽ��";
                this.neuSpread2_Sheet1.Columns.Get(0).Width = 56F;
                this.neuSpread2_Sheet1.Columns.Get(1).Label = "ҽ������";
                this.neuSpread2_Sheet1.Columns.Get(1).Width = 56F;
                this.neuSpread2_Sheet1.Columns.Get(2).Label = "��Ч";
                this.neuSpread2_Sheet1.Columns.Get(2).Width = 35F;

                this.neuSpread2_Sheet1.Columns.Get(3).Label = "��Ŀ����";
                this.neuSpread2_Sheet1.Columns.Get(3).Width = 117F;
                this.neuSpread2_Sheet1.Columns.Get(4).Label = "����";
                this.neuSpread2_Sheet1.Columns.Get(4).Width = 60F;
                this.neuSpread2_Sheet1.Columns.Get(5).Label = "��λ";
                this.neuSpread2_Sheet1.Columns.Get(5).Width = 35F;
                this.neuSpread2_Sheet1.Columns.Get(6).Label = "Ӧִ��ʱ��";
                this.neuSpread2_Sheet1.Columns.Get(6).Width = 110F;
                this.neuSpread2_Sheet1.Columns.Get(7).Label = "�ֽ�ʱ��";
                this.neuSpread2_Sheet1.Columns.Get(7).Width = 60F;
                this.neuSpread2_Sheet1.Columns.Get(8).Label = "���˱��";
                this.neuSpread2_Sheet1.Columns.Get(8).Width = 56F;
                this.neuSpread2_Sheet1.Columns.Get(9).Label = "����ʱ��";
                this.neuSpread2_Sheet1.Columns.Get(9).Width = 110F;
                this.neuSpread2_Sheet1.Columns.Get(10).Label = "ҽ��ʱ��";
                this.neuSpread2_Sheet1.Columns.Get(10).Width = 110F;
                this.neuSpread2_Sheet1.Columns.Get(11).Label = "ֹͣʱ��";
                this.neuSpread2_Sheet1.Columns.Get(11).Width = 110F;
                this.neuSpread2_Sheet1.Columns.Get(12).Label = "Ƶ��";
                this.neuSpread2_Sheet1.Columns.Get(12).Width = 50F;
                this.neuSpread2_Sheet1.Columns.Get(13).Label = "ҽ��˵��";
                this.neuSpread2_Sheet1.Columns.Get(13).Width = 51F;

                this.neuSpread2_Sheet1.Columns.Get(14).Label = "��ע";
                this.neuSpread2_Sheet1.Columns.Get(14).Width = 51F;
                this.neuSpread2_Sheet1.Columns.Get(15).Label = "ҽ����";
                this.neuSpread2_Sheet1.Columns.Get(15).Width = 70F;

                this.neuSpread2_Sheet1.Columns.Get(16).Label = "��Ϻ�";
                this.neuSpread2_Sheet1.Columns.Get(16).Width = 67F;
                this.neuSpread2_Sheet1.Columns.Get(17).Label = "ִ�к�";
                this.neuSpread2_Sheet1.Columns.Get(17).Width = 69F;
                this.neuSpread2_Sheet1.Columns.Get(18).Label = "����";
                this.neuSpread2_Sheet1.Columns.Get(18).Width = 38F;
                this.neuSpread2_Sheet1.Columns.Get(19).Label = "ִ�п���";
                this.neuSpread2_Sheet1.Columns.Get(19).Width = 127F;
                this.neuSpread2_Sheet1.Columns.Get(20).Label = "������";
                this.neuSpread2_Sheet1.Columns.Get(20).Width = 45F;
                this.neuSpread2_Sheet1.Columns.Get(21).Label = "ֹͣ��";
                this.neuSpread2_Sheet1.Columns.Get(21).Width = 45F;
                this.neuSpread2_Sheet1.Columns.Get(22).Label = "������";
                this.neuSpread2_Sheet1.Columns.Get(22).Width = 65F;
                this.neuSpread2_Sheet1.Columns.Get(23).Label = "��������ˮ��";
                this.neuSpread2_Sheet1.Columns.Get(23).Visible = false;
                this.neuSpread2_Sheet1.Columns.Get(24).Label = "���͵���ӡ���";
                this.neuSpread2_Sheet1.Columns.Get(24).Visible = false;
                this.neuSpread2_Sheet1.Columns.Get(25).Label = "��ӡʱ��";
                this.neuSpread2_Sheet1.Columns.Get(25).Visible = false;
                this.neuSpread2_Sheet1.Columns.Get(26).Label = "��ҩƷ����";
                this.neuSpread2_Sheet1.Columns.Get(26).Visible = false;
                this.neuSpread2_Sheet1.Columns.Get(27).Label = "סԺ����";
                this.neuSpread2_Sheet1.Columns.Get(27).Visible = false;
                this.neuSpread2_Sheet1.Columns.Get(28).Label = "����վ";
                this.neuSpread2_Sheet1.Columns.Get(28).Visible = false;
                this.neuSpread2_Sheet1.Columns.Get(29).Label = "��������";
                this.neuSpread2_Sheet1.Columns.Get(29).Visible = false;
                this.neuSpread2_Sheet1.Columns.Get(30).Label = "ƴ����";
                this.neuSpread2_Sheet1.Columns.Get(30).Visible = false;
                this.neuSpread2_Sheet1.Columns.Get(31).Label = "�����";
                this.neuSpread2_Sheet1.Columns.Get(31).Visible = false;

                RefreshUndrugFlag();

                this.neuSpread2_Sheet1.DefaultStyle.Locked = true;
                this.neuSpread2_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
                this.neuSpread2_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
                this.neuSpread2_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
                this.neuSpread2_Sheet1.RowHeader.Columns.Default.Resizable = false;
                this.neuSpread2_Sheet1.RowHeader.Columns.Get(0).Width = 30F;
                //this.neuSpread1_Sheet1.SetColumnAllowAutoSort(-1, true);
                #endregion
            }
            this.neuSpread2_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ExtendedSelect;
        }

        protected void RefreshUndrugFlag()
        {
            //this.neuSpread2_Sheet1.Columns.Add(0, 1);//�������
            //this.neuSpread2_Sheet1.Columns.Get(0).Label = "���";
            //this.neuSpread2_Sheet1.Columns.Get(0).Width = 60F;
            //for (int i = 0; i < this.neuSpread2.Sheets[0].RowCount; i++)
            //{
                //int iFee = int.Parse(this.neuSpread2.Sheets[0].Cells[i, 19].Text);
                //if (iFee == 1)
                //{
                //    this.neuSpread2.Sheets[0].Cells[i, 0].Text = "���շ�";
                //}
                //else
                //{
                //    if (this.neuSpread2.Sheets[0].Cells[i, 24].Text == "1")
                //    {
                //        this.neuSpread2.Sheets[0].Cells[i, 0].Text = "���շ�";
                //    }
                //    else
                //    {
                //        this.neuSpread2.Sheets[0].Cells[i, 0].Text = "����δ�շ�";
                //    }
                //}
            //}
        }

        protected void RefreshDrugBackColor()
        {
            for (int i = 0; i < this.neuSpread1.Sheets[0].RowCount; i++)
            {
                string strValid = this.neuSpread1_Sheet1.Cells[i, 3].Text;
                this.neuSpread1_Sheet1.Rows[i].BackColor = Color.White;
                if (strValid == "��Ч")
                {
                    this.neuSpread1_Sheet1.Rows[i].BackColor = Color.FromArgb(255, 128, 128);
                }
            }
            for (int i = 0; i < this.neuSpread2.Sheets[0].RowCount; i++)
            {
                string strValid = this.neuSpread2.Sheets[0].Cells[i, 2].Text;
                this.neuSpread2_Sheet1.Rows[i].BackColor = Color.White;
                if (strValid == "��Ч")
                {
                    this.neuSpread2_Sheet1.Rows[i].BackColor = Color.FromArgb(255, 128, 128);
                }
            }
        }

        /// <summary>
        /// ��ʾ����
        /// </summary>
        public void ShowData(string inPatientNo)
        {
            //this.Patient = this.patientManager.PatientQuery(inPatientNo);
            if (this.Patient == null || this.Patient.ID == "")
            {
                //�������
                this.ClearData();
                return;
            }

            //��ʾҩƷִ�е���Ϣ
            if (this.Patient.ID == "") return;

            #region ȡҩƷִ�е�����
            this.myDataSetDrug = CacheManager.InOrderMgr.QueryExecDrugOrderByInpatientNo(this.Patient.ID);
            if (this.myDataSetDrug == null)
            {
                MessageBox.Show(CacheManager.InOrderMgr.Err);
                return;
            }

            //�������еı���ת��Ϊ����
            for (int i = 0; i < this.myDataSetDrug.Tables[0].Rows.Count; i++)
            {
                this.myDataSetDrug.Tables[0].Rows[i]["ȡҩҩ��"] = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(this.myDataSetDrug.Tables[0].Rows[i]["ȡҩҩ��"].ToString());
                this.myDataSetDrug.Tables[0].Rows[i]["ִ�п���"] = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(this.myDataSetDrug.Tables[0].Rows[i]["ִ�п���"].ToString());
                this.myDataSetDrug.Tables[0].Rows[i]["��ҩ����"] = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(this.myDataSetDrug.Tables[0].Rows[i]["��ҩ����"].ToString());

                //this.myDataSetDrug.Tables[0].Rows[i]["ҽ������"] = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(this.myDataSetDrug.Tables[0].Rows[i]["ҽ������"].ToString());
            }
            //��ȡ�õ�������ʾ���ؼ���
            this.myDataViewDrug = new DataView(this.myDataSetDrug.Tables[0]);
            this.neuSpread1_Sheet1.DataSource = this.myDataViewDrug;
            //������ʾ��ʽ
            this.SetFormatForDrug();
            #endregion

            #region ȡ��ҩƷִ�е�����
            this.myDataSetUndrug = CacheManager.InOrderMgr.QueryExecUndrugOrderByInpatientNo(this.Patient.ID);
            if (this.myDataSetUndrug == null)
            {
                MessageBox.Show(CacheManager.InOrderMgr.Err);
                return;
            }
            //�������еı���ת��Ϊ����
            for (int i = 0; i < this.myDataSetUndrug.Tables[0].Rows.Count; i++)
            {
                this.myDataSetUndrug.Tables[0].Rows[i]["ִ�п���"] = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(this.myDataSetUndrug.Tables[0].Rows[i]["ִ�п���"].ToString());

                //this.myDataSetUndrug.Tables[0].Rows[i]["ҽ������"] = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(this.myDataSetUndrug.Tables[0].Rows[i]["ҽ������"].ToString());
            }
            //��ȡ�õ�DataSet������ʾ�ؼ�
            this.myDataViewUndrug = new DataView(this.myDataSetUndrug.Tables[0]);
            this.neuSpread2_Sheet1.DataSource = this.myDataViewUndrug;
            //������ʾ��ʽ
            this.SetFormatForUnDrug();
            #endregion

            RefreshDrugBackColor();
        }

        /// <summary>
        /// �������
        /// </summary>
        public void ClearData()
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;
            this.neuSpread2_Sheet1.Rows.Count = 0;
        }



        /// <summary>
        /// ������Ч���
        /// </summary>
        /// <param name="validFlag"></param>
        private int SetValidFlag(bool validFlag)
        {
            string strValid = (validFlag ? "��Ч" : "��Ч");
            string strToDo = (validFlag ? "�ָ�" : "����");

            if (MessageBox.Show("ȷ��Ҫ����ѡ�м�¼Ϊ" + strValid + "ô��", "ѯ��", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Cancel)
            {
                return 1;
            }

            FS.HISFC.Models.Order.ExecOrder exeOrder = null;

            string feeItems = "";

            //�Ƿ���������ݣ����µ��˾���ʾ
            bool isDoSomeThing = false;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            if (this.neuTabControl1.SelectedTab == tabPage1)
            {
                for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
                {
                    if (neuSpread1_Sheet1.IsSelected(i, 0))
                    {
                        string execOrderID = this.neuSpread1_Sheet1.Cells[i, 27].Text.Trim();

                        exeOrder = CacheManager.InOrderMgr.QueryExecOrderByExecOrderID(execOrderID, "1");
                        if (exeOrder == null)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����ִ�е���¼����\r\n\r\n" + CacheManager.InOrderMgr.Err);
                            return -1;
                        }

                        int rev = 1;

                        if (validFlag)
                        {
                            if (exeOrder.IsValid)
                            {
                                MessageBox.Show("��¼��" + exeOrder.Order.Item.Name + "�� ʱ�䣺" + exeOrder.DateUse.ToString() + " �Ѿ�����Ч״̬��");
                                continue;
                            }
                            rev = CacheManager.InOrderMgr.UpdateExecValidFlag(execOrderID, true, "1");
                        }
                        else
                        {
                            if (!exeOrder.IsValid)
                            {
                                MessageBox.Show("��¼��" + exeOrder.Order.Item.Name + "�� ʱ�䣺" + exeOrder.DateUse.ToString() + " �Ѿ�����Ч״̬��");
                                continue;
                            }
                            rev = CacheManager.InOrderMgr.DcExecImmediate(exeOrder, CacheManager.InOrderMgr.Operator);
                        }

                        if (rev < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("��¼��" + exeOrder.Order.Item.Name + "�� ʱ�䣺" + exeOrder.DateUse.ToString() + " " + strToDo + "����\r\n" + exeOrder.Order.Item.Name + "\r\n\r\n" + CacheManager.InOrderMgr.Err);
                            return -1;
                        }
                        else if (rev == 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("��¼��" + exeOrder.Order.Item.Name + "�� ʱ�䣺" + exeOrder.DateUse.ToString() + " " + strToDo + "����\r\n" + exeOrder.Order.Item.Name + "\r\n\r\nû���ҵ���Ӧ��ִ����Ϣ��");
                            return -1;
                        }

                        if (!validFlag && this.neuSpread1_Sheet1.Cells[i, 11].Text.Trim() == "�Ѽ�")
                        {
                            feeItems += "\r\n" + exeOrder.DateUse.ToString("MM-dd HH:mm") + "��" + exeOrder.Order.Item.Name + "��";
                        }

                        isDoSomeThing = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < neuSpread2_Sheet1.RowCount; i++)
                {
                    if (neuSpread2_Sheet1.IsSelected(i, 0))
                    {
                        string execOrderID = this.neuSpread2_Sheet1.Cells[i, 17].Text.Trim();

                        exeOrder = CacheManager.InOrderMgr.QueryExecOrderByExecOrderID(execOrderID, "2");
                        if (exeOrder == null)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����ִ�е���¼����\r\n\r\n" + CacheManager.InOrderMgr.Err);
                            return -1;
                        }

                        int rev = 1;

                        if (validFlag)
                        {
                            if (exeOrder.IsValid)
                            {
                                MessageBox.Show("��¼��" + exeOrder.Order.Item.Name + "�� ʱ�䣺" + exeOrder.DateUse.ToString() + " �Ѿ�����Ч״̬��");
                                continue;
                            }
                            rev = CacheManager.InOrderMgr.UpdateExecValidFlag(execOrderID, false, "1");
                        }
                        else
                        {
                            if (!exeOrder.IsValid)
                            {
                                MessageBox.Show("��¼��" + exeOrder.Order.Item.Name + "�� ʱ�䣺" + exeOrder.DateUse.ToString() + " �Ѿ�����Ч״̬��");
                                continue;
                            }
                            rev = CacheManager.InOrderMgr.DcExecImmediate(exeOrder, CacheManager.InOrderMgr.Operator);
                        }

                        if (rev < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("��¼��" + exeOrder.Order.Item.Name + "�� ʱ�䣺" + exeOrder.DateUse.ToString() + " " + strToDo + "����\r\n" + exeOrder.Order.Item.Name + "\r\n\r\n" + CacheManager.InOrderMgr.Err);
                            return -1;
                        }
                        else if (rev == 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("��¼��" + exeOrder.Order.Item.Name + "�� ʱ�䣺" + exeOrder.DateUse.ToString() + " " + strToDo + "����\r\n" + exeOrder.Order.Item.Name + "\r\n\r\nû���ҵ���Ӧ��ִ����Ϣ��");
                            return -1;
                        }

                        if (!validFlag && this.neuSpread1_Sheet1.Cells[i, 11].Text.Trim() == "�Ѽ�")
                        {
                            feeItems += "\r\n" + exeOrder.DateUse.ToString("MM-dd HH:mm") + "��" + exeOrder.Order.Item.Name + "��";
                        }
                        isDoSomeThing = true;
                    }
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            if (!string.IsNullOrEmpty(feeItems))
            {
                FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "", feeItems + "\r\n\r\n�Ѿ��շѣ���ע���˷ѣ�", ToolTipIcon.Warning);
            }

            if (isDoSomeThing)
            {
                MessageBox.Show("�����ɹ���");
            }


            this.ShowData(this.patient.ID);

            return 1;
        }

        /// <summary>
        /// ������Ч���
        /// </summary>
        /// <param name="RowIndex"></param>
        /// <param name="flag">0 ������Ч��1 ������Ч</param>
        /// <param name="drugFlag">ҩƷ���</param>
        private void SetValidFlag(int RowIndex, string flag,string drugFlag)
        {
            DialogResult r;

            if (drugFlag == "1")
            {
                //if (this.neuSpread1_Sheet1.Cells[RowIndex, 3].Text.Trim() == "��Ч")
                //{
                //    return;
                //}

                //if (this.neuSpread1_Sheet1.Cells[RowIndex, 4].Text.Trim() != "δ����")
                //{
                //    MessageBox.Show("ֻ��δ���͵�ҩƷ�ſ��Բ�����");
                //    return;
                //}

                if (flag == "0")
                {
                    r = MessageBox.Show("ȷ��Ҫ�ָ�������¼����Ч����?", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                }
                else
                {
                    r = MessageBox.Show("ȷ��Ҫ���ϸ�����¼��?,�ò������ɳ���", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                }
                if (r == DialogResult.Cancel)
                {
                    return;
                }

                string execOrderID = this.neuSpread1_Sheet1.Cells[RowIndex, 27].Text.Trim();

                if (execOrderID == null || execOrderID.Length <= 0)
                {
                    MessageBox.Show("ִ����ˮ��Ϊ�գ�");
                    return;
                }
                if (flag == "1")
                {
                    if (this.neuSpread1_Sheet1.Cells[RowIndex, 3].Text.Trim() != "��Ч")
                    {
                        MessageBox.Show("������¼�Ѿ����ϣ�");
                        return;
                    }

                    this.myExeOrder = new FS.HISFC.Models.Order.ExecOrder();
                    FS.HISFC.Models.Pharmacy.Item objPharmacy = new FS.HISFC.Models.Pharmacy.Item();
                    objPharmacy.ID = this.neuSpread1_Sheet1.Cells[RowIndex, 39].Text;//ҩƷ����
                    objPharmacy.Name = this.neuSpread1_Sheet1.Cells[RowIndex, 5].Text;//ҩƷ����
                    objPharmacy.Specs = this.neuSpread1_Sheet1.Cells[RowIndex, 6].Text;//ҩƷ���
                    objPharmacy.Memo = this.neuSpread1_Sheet1.Cells[RowIndex, 22].Text;//ȡҩҩ��
                    this.myExeOrder.Order.Item = objPharmacy;//ִ�е���Ŀ
                    this.myExeOrder.Order.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[RowIndex, 7].Value);//ҩƷ����
                    this.myExeOrder.Order.Unit = this.neuSpread1_Sheet1.Cells[RowIndex, 8].Text;//ҩƷ��λ
                    this.myExeOrder.ID = execOrderID;

                    int _Ret = CacheManager.InOrderMgr.DcExecImmediate(this.myExeOrder, CacheManager.InOrderMgr.Operator);

                    //int _Ret = CacheManager.InOrderMgr.UpdateExecValidFlag(execOrderID, true, flag);

                    if (_Ret < 0)
                    {
                        MessageBox.Show("���ϼ�¼����\r\n" + CacheManager.InOrderMgr.Err);
                        return;
                    }
                    else if (_Ret == 0)
                    {
                        MessageBox.Show("���ϼ�¼����\r\nû���ҵ�����ִ����Ϣ��");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("���ϼ�¼�ɹ���");
                    }

                    if (this.neuSpread1_Sheet1.Cells[RowIndex, 11].Text.Trim() == "�Ѽ�")
                    {
                        MessageBox.Show("�ü�¼�Ѿ��շѣ����˷ѣ�");
                    }
                }
                else
                {
                    int _Ret = CacheManager.InOrderMgr.UpdateExecValidFlag(execOrderID, true, "1");

                    if (_Ret < 0)
                    {
                        MessageBox.Show("�ָ���¼����\r\n" + CacheManager.InOrderMgr.Err);
                        return;
                    }
                    else if (_Ret == 0)
                    {
                        MessageBox.Show("�ָ���¼����\r\nû���ҵ�����ִ����Ϣ��");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("�ָ���¼�ɹ���");
                    }
                }
            }
            else
            {
                if (flag == "0")
                {
                    r = MessageBox.Show("ȷ��Ҫ�ָ�������¼����Ч����?", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                }
                else
                {
                    //������ҽ��ȡ����ȷ�ϵ��ն���Ŀ��ҽ��
                    string execOrderId = this.neuSpread2_Sheet1.Cells[RowIndex, 17].Text.Trim();
                    this.myExeOrder = CacheManager.InOrderMgr.QueryExecOrderByExecOrderID(execOrderId, "2");
                    if (myExeOrder.Order.OrderType.Type == FS.HISFC.Models.Order.EnumType.SHORT
                        && myExeOrder.Order.Status == 2
                        && myExeOrder.Order.Item.IsNeedConfirm)
                    {
                        ArrayList execOrderList = CacheManager.InOrderMgr.QueryExecOrderByOrderNo(myExeOrder.Order.ID, myExeOrder.Order.Item.ID, "1");
                        if (execOrderList.Count > 0)
                        {
                            MessageBox.Show("[" + myExeOrder.Order.ExeDept.Name + "]�Ѿ���[" + myExeOrder.Order.Item.Name + "]�����շ�ȷ�ϣ�����[" + myExeOrder.Order.ExeDept.Name + "]��ϵ��" + "\n\n"
                                + "ȷ�ϻ����Ƿ��Ѿ�ִ��[" + myExeOrder.Order.Item.Name + "]������Ѿ�ִ�У������ҽ������������");
                            return;
                        }
                    }
                    r = MessageBox.Show("ȷ��Ҫ���ϸ�����¼��?,�ò������ɳ���", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                }
                if (r == DialogResult.Cancel)
                {
                    return;
                }

                string execOrderID = this.neuSpread2_Sheet1.Cells[RowIndex, 17].Text.Trim();
                this.myExeOrder = new FS.HISFC.Models.Order.ExecOrder();
                myExeOrder.ID = execOrderID;

                if (flag == "1")
                {
                    int _Ret = CacheManager.InOrderMgr.DcExecImmediate(this.myExeOrder, CacheManager.InOrderMgr.Operator);

                    //int _Ret = CacheManager.InOrderMgr.UpdateExecValidFlag(execOrderID, true, flag);

                    if (_Ret < 0)
                    {
                        MessageBox.Show("���ϼ�¼����\r\n" + CacheManager.InOrderMgr.Err);
                        return;
                    }
                    else if (_Ret == 0)
                    {
                        MessageBox.Show("���ϼ�¼����\r\nû���ҵ�����ִ����Ϣ��");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("���ϼ�¼�ɹ���");
                    } 
                    
                    if (this.neuSpread2_Sheet1.Cells[RowIndex, 8].Text.Trim() == "�Ѽ�")
                    {
                        MessageBox.Show("�ü�¼�Ѿ��շѣ����˷ѣ�");
                    }
                }
                else
                {
                    int _Ret = CacheManager.InOrderMgr.UpdateExecValidFlag(execOrderID, false, "1");

                    if (_Ret < 0)
                    {
                        MessageBox.Show("�ָ���¼����\r\n" + CacheManager.InOrderMgr.Err);
                        return;
                    }
                    else if (_Ret == 0)
                    {
                        MessageBox.Show("�ָ���¼����\r\nû���ҵ�����ִ����Ϣ��");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("�ָ���¼�ɹ���");
                    }
                }
            }
            this.ShowData(this.patient.ID);
        }

        /// <summary>
        /// ���ù�������,��������
        /// </summary>
        private void SetFilter()
        {
            //��������
            //ҩƷ
            if (this.myDataViewDrug.Table != null && this.myDataViewDrug.Table.Rows.Count > 0)
            {
                this.myDataViewDrug.RowFilter = this.filterInput + " AND " + this.filterValid + " AND " + this.filterExec + " AND " + this.filterType;
                this.SetFormatForDrug();
            }

            //��ҩƷ
            if (this.myDataViewUndrug.Table != null && this.myDataViewUndrug.Table.Rows.Count > 0)
            {
                this.myDataViewUndrug.RowFilter = this.filterInput + " AND " + this.filterValid + " AND " + this.filterType;
                this.SetFormatForUnDrug();
            }

            this.RefreshDrugBackColor();
        }

        /// <summary>
        /// ������״̬����ҽ��
        /// </summary>
        /// <param name="State"></param>
        public void Filter1(int State)
        {
            if (this.Patient == null) return;
            if (this.Patient.ID == "") return;

            //��ѯʱ����ܹ���
            switch (State)
            {
                case 0://ȫ��
                    this.filterExec = "1=1";
                    break;
                case 1://����
                    this.filterExec = "����״̬ = '�ѷ���'";//3
                    break;
                case 2://��Ч
                    this.filterExec = "����״̬ = 'δ����'";
                    break;
                case 3:
                    this.filterExec = "����״̬ = '�ѷ�ҩ'";
                    break;
                default:
                    this.filterExec = "1=1";
                    this.filterValid = "1=1";
                    this.filterType = "1=1";
                    break;
            }
            this.SetFilter();
        }

        /// <summary>
        /// ����ҽ����ʾ
        /// </summary>
        /// <param name="State"></param>
        public void Filter2(int State)
        {
            if (this.Patient == null) return;
            if (this.Patient.ID == "") return;
            //��ѯʱ����ܹ���
            switch (State)
            {
                case 0://ȫ��
                    this.filterValid = "1=1";
                    break;
                case 1://����
                    this.filterValid = "��Ч = '��Ч'";//3
                    break;
                case 2://��Ч
                    this.filterValid = "��Ч = '��Ч'";
                    break;
                default:
                    this.filterExec = "1=1";
                    this.filterValid = "1=1";
                    this.filterType = "1=1";
                    break;
            }
            this.SetFilter();
        }

        /// <summary>
        /// ����ҽ����ʾ
        /// </summary>
        /// <param name="State"></param>
        public void Filter3(int State)
        {
            if (this.Patient == null) return;
            if (this.Patient.ID == "") return;
            //��ѯʱ����ܹ���
            switch (State)
            {
                case 0://ȫ��
                    this.filterType = "1=1";
                    break;
                case 1://����ҽ��
                    this.filterType = "ҽ������ = '����ҽ��'";
                    break;
                case 2://��ʱҽ��
                    this.filterType = "ҽ������ = '��ʱҽ��'";
                    break;
                default:
                    this.filterExec = "1=1";
                    this.filterValid = "1=1";
                    this.filterType = "1=1";
                    break;
            }
            this.SetFilter();
        }

        /// <summary>
        /// load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucOrderExeQuery_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// ��Ч״̬
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter2(this.neuComboBox2.SelectedIndex);
        }

        /// <summary>
        /// ����״̬
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter1(this.neuComboBox1.SelectedIndex);
        }

        /// <summary>
        /// ����ƴ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTextBox1_TextChanged(object sender, EventArgs e)
        {
            //ȡ������
            string queryCode = this.neuTextBox1.Text.Trim().ToUpper() ;
            //if (this.chbMisty.Checked)
            //{
            //    queryCode = "%" + queryCode + "%";
            //}
            //else
            //{
                queryCode = queryCode + "%";
            //}

            //���ù�������
            this.filterInput = "((ƴ���� LIKE '%" + queryCode + "%') OR " +
                "(����� LIKE '%" + queryCode + "%') OR " +
                "(���� LIKE '%" + queryCode + "%') )";

            //����ҩƷ����
            this.SetFilter();
        }

        /// <summary>
        /// fp1����Ϊ xml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            if (this.isAutoSaveXML)
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.drugQuery);
            }
        }

        /// <summary>
        /// fp2����Ϊxml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread2_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            if (this.isAutoSaveXML)
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread2_Sheet1, this.undrugQuery);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void neuSpread1_ActiveSheetChanged(object sender, EventArgs e)
        {
            RefreshDrugBackColor();
        }

        /// <summary>
        /// ˫������ҩƷִ�е�Ϊ��Ч��ȡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.neuTabControl1.SelectedTab == this.tabPage1)
            {
                if (this.neuSpread1_Sheet1.ActiveRowIndex >= 0)
                {
                    string strValid = this.neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.ActiveRowIndex, 3].Text;

                    if (strValid == "��Ч")
                    {
                        this.SetValidFlag(this.neuSpread1_Sheet1.ActiveRowIndex, "0", "1");
                    }
                    else
                    {
                        this.SetValidFlag(this.neuSpread1_Sheet1.ActiveRowIndex, "1", "1");
                    }
                }
            }
            else
            {
                if (this.neuSpread2_Sheet1.ActiveRowIndex >= 0)
                {
                    string strValid = this.neuSpread2.Sheets[0].Cells[neuSpread2_Sheet1.ActiveRowIndex, 2].Text;

                    if (strValid == "��Ч")
                    {
                        this.SetValidFlag(this.neuSpread2_Sheet1.ActiveRowIndex, "0", "0");
                    }
                    else
                    {
                        this.SetValidFlag(this.neuSpread2_Sheet1.ActiveRowIndex, "1", "0");
                    }
                }
            }
        }

        /// <summary>
        /// ˫�����÷�ҩƷִ�е�Ϊ��Ч��ȡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void neuSpread2_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.neuSpread2_Sheet1.ActiveRowIndex >= 0)
            {
                string strValid = this.neuSpread2.Sheets[0].Cells[neuSpread2_Sheet1.ActiveRowIndex, 2].Text;

                if (strValid == "��Ч")
                {
                    this.SetValidFlag(this.neuSpread2_Sheet1.ActiveRowIndex, "0", "0");
                }
                else
                {
                    this.SetValidFlag(this.neuSpread2_Sheet1.ActiveRowIndex, "1", "0");
                }
            }
        }

        /// <summary>
        /// ҽ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter3(this.neuComboBox3.SelectedIndex);
        }

        /// <summary>
        /// ��ӡ����,ȡ�ӿڷ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            //���� �������� 2011-9-18 houwb
            return 1;

            if (this.GetItemInfo() == -1) 
                return -1;

            FS.HISFC.Components.Order.Classes.IOrderExeQuery printInterFace = null;

            printInterFace = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.Components.Order.Classes.IOrderExeQuery)) as FS.HISFC.Components.Order.Classes.IOrderExeQuery;
            if (printInterFace != null)
            {
                printInterFace.patientInfoObj = this.patient;
                if (printInterFace.SetValue(this.alExeOrder) == 1)
                {
                    printInterFace.Print();
                }
            }
            return base.OnPrint(sender, neuObject);
        }

        private int GetItemInfo()
        {
            if (this.neuTabControl1.SelectedTab == this.tabPage1)
            {
                Hashtable myhashtable = new Hashtable();
                for (int a = 0; a < this.neuSpread1_Sheet1.RowCount; a++)
                {
                    if (FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[a, 0].Value) == true)
                    {
                        string strDrugStoreName = this.neuSpread1_Sheet1.Cells[a, 21].Text;
                        if (!myhashtable.ContainsKey(strDrugStoreName))
                        {
                            myhashtable.Add(strDrugStoreName, strDrugStoreName);
                        }
                    }
                }
                if (myhashtable.Count > 1)
                {
                    System.Windows.Forms.MessageBox.Show("��ѡ���˶��ҩ����ҩƷ��������˶Դ�ӡҩ����");
                    return -1;
                }
                this.alExeOrder = new ArrayList();
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    if (FS.FrameWork.Function.NConvert.ToBoolean( this.neuSpread1_Sheet1.Cells[i, 0].Value) == true)
                    {
                        if (this.neuSpread1_Sheet1.Cells[i, 2].Text == "����ҽ��")
                        {
                            System.Windows.Forms.MessageBox.Show("���ڵڡ�" + (i + 1).ToString() + " ����ѡ���˳���ҽ��,��ѡ����ʱҽ����ӡҩ����");
                            return -1;

                        }
                        if (this.neuSpread1_Sheet1.Cells[i, 21].Text == this.neuSpread1_Sheet1.Cells[i, 21].Text)
                        {
                            
                        }
                        try
                        {
                            this.myExeOrder = new FS.HISFC.Models.Order.ExecOrder();
                            FS.HISFC.Models.Pharmacy.Item objPharmacy = new FS.HISFC.Models.Pharmacy.Item();
                            objPharmacy.ID    = this.neuSpread1_Sheet1.Cells[i, 39].Text;//ҩƷ����
                            objPharmacy.Name  = this.neuSpread1_Sheet1.Cells[i, 5].Text;//ҩƷ����
                            objPharmacy.Specs = this.neuSpread1_Sheet1.Cells[i, 6].Text;//ҩƷ���
                            objPharmacy.Memo  = this.neuSpread1_Sheet1.Cells[i, 21].Text;//ȡҩҩ��
                            this.myExeOrder.Order.Item = objPharmacy;//ִ�е���Ŀ
                            this.myExeOrder.Order.Qty  = FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, 7].Value);//ҩƷ����
                            this.myExeOrder.Order.Unit = this.neuSpread1_Sheet1.Cells[i, 8].Text;//ҩƷ��λ
                            this.alExeOrder.Add(this.myExeOrder);
                        }
                        catch(Exception ex)
                        {
                            return -1;
                        }
                        
                    }
                }
            }
            return 1;
        }

        #region addby xuewj 2009-8-24 �ָ��������������Ŀ���Ա�ʹ������������ҩ,����ִ�е� {01F18F48-887D-4d2a-A0F9-757B61A5B8A6}

        /// <summary>
        /// �ָ�ִ�е���¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVaildExecOrder_Click(object sender, EventArgs e)
        {
            this.SetValidFlag(true);
            //if (this.neuTabControl1.SelectedTab == this.tabPage1)
            //{
            //    if (this.neuSpread1_Sheet1.ActiveRowIndex >= 0 && this.neuSpread1_Sheet1.RowCount > 0)
            //    {
            //        this.SetValidFlag(this.neuSpread1_Sheet1.ActiveRowIndex, "0", "1");
            //    }
            //}
            //else if (this.neuTabControl1.SelectedTab == this.tabPage2)
            //{
            //    if (this.neuSpread2_Sheet1.ActiveRowIndex >= 0 && this.neuSpread2_Sheet1.RowCount > 0)
            //    {
            //        this.SetValidFlag(this.neuSpread2_Sheet1.ActiveRowIndex, "0", "0");
            //    }
            //}
        }

        /// <summary>
        /// ����Ϊ��Ч
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUNVaildExecOrder_Click(object sender, EventArgs e)
        {
            this.SetValidFlag(false);
            //if (this.neuTabControl1.SelectedTab == this.tabPage1)
            //{
            //    if (this.neuSpread1_Sheet1.ActiveRowIndex >= 0 && this.neuSpread1_Sheet1.RowCount > 0)
            //    {
            //        this.SetValidFlag(this.neuSpread1_Sheet1.ActiveRowIndex, "1", "1");
            //    }
            //}
            //else
            //{
            //    if (this.neuSpread2_Sheet1.ActiveRowIndex >= 0 && this.neuSpread2_Sheet1.RowCount > 0)
            //    {
            //        this.SetValidFlag(this.neuSpread2_Sheet1.ActiveRowIndex, "1", "0");
            //    }
            //}
        }

        #endregion

        private void neuLinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.neuTabControl1.SelectedTab == this.tabPage1)
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.drugQuery);
            }
            else
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread2_Sheet1, this.undrugQuery);
            }

            MessageBox.Show("��ʽ����ɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// ������
        /// </summary>
        //Enum EnumColumSet
        //{

        //}
    }
}
