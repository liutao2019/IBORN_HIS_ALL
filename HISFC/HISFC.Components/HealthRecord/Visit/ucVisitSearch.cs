using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.HealthRecord.EnumServer;
using System.Collections;

namespace FS.HISFC.Components.HealthRecord.Visit
{
    /// <summary>
    /// [��������: ��ü�¼��ѯ]<br></br>
    /// [������:   ���]<br></br>
    /// [����ʱ��: 2009-09-27]<br></br>
    /// <˵��>
    ///    
    /// </˵��>
    /// </summary>
    public partial class ucVisitSearch : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 
        /// </summary>
        public ucVisitSearch()
        {
            InitializeComponent();
           
        }

        #region ����

        //�������
        FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();

        //����ȫ�ֱ��� DataView  DataSet ������������
        private DataView dvICD = new DataView();
        private DataView dvOutPatientInfo = new DataView();

        private DataSet ds = new DataSet();
        DataSet dsOPI = new DataSet();

        //Visitҵ���
        private FS.HISFC.BizLogic.HealthRecord.Visit.Visit myICD = new FS.HISFC.BizLogic.HealthRecord.Visit.Visit();

        //ȫ�ֱ������洢���ص����� ICD10 ICD9 ����ICD ��Ϣ
        private ICDTypes type;

        //��ü�¼ҵ����
        FS.HISFC.BizLogic.HealthRecord.Visit.VisitRecord visitRecordManager 
            = new FS.HISFC.BizLogic.HealthRecord.Visit.VisitRecord();

        //��û�����Ϣ������
        FS.HISFC.BizLogic.HealthRecord.Visit.LinkWay linkWayManager
            = new FS.HISFC.BizLogic.HealthRecord.Visit.LinkWay();

        #endregion

        #region �������¼�

        public override int Export(object sender, object neuObject)
        {
            Export();
            return base.Export(sender, neuObject);
        }

        #endregion

        #region �¼�

        private void ucVisitSearch_Load(object sender, EventArgs e)
        {
            #region ����ѡ���

            //��÷�ʽ
            this.cmbLinkType.AddItems(con.GetList("CASE06"));
            //һ�����
            cmbCircs.AddItems(con.GetList("CASE07"));

            //��ý��
            cmbResult.AddItems(con.GetList("CASE14"));

            #endregion

            if (this.Tag == null)
            {
                return;
            }
            if (this.Tag.ToString() == "ICD10")
            {
                type = ICDTypes.ICD10;
            }
            else if (this.Tag.ToString() == "ICD9")
            {
                type = ICDTypes.ICD9;
            }
            else //if (this.Tag.ToString() == "ICDOperation")
            {
                type = ICDTypes.ICDOperation;
            }

            LoadInfo();

            LoadOutPatientInfo();

            tvIcdList.AfterSelect += tv_AfterSelect;
        }


        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(tvIcdList.SelectedNode.Index!=-1)
            {
                if (tvIcdList.SelectedNode.Parent==null)
                {
                    LoadOutPatientInfo();
                }
                else
                {
                    QueryOPInfoByIcd(tvIcdList.SelectedNode.Text.Trim());
                }
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string filter = GetFilterStr();
            if (!string.IsNullOrEmpty(filter))
            {
                this.dvOutPatientInfo.RowFilter = GetFilterStr();
            }
            else
            {
                LoadOutPatientInfo();
            }
            SetFPStyle();
        }

        private void fpVisitInfo_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //��ǰѡ�����û��߲�����
            string cardNo = fpVisitInfo_Sheet1.Cells[e.Row, 1].Text.Trim();

            string patientNo = fpVisitInfo_Sheet1.Cells[e.Row, 0].Text.Trim();

            if (!string.IsNullOrEmpty(cardNo) || !string.IsNullOrEmpty(patientNo))
            {
                GetLinkWay(patientNo, cardNo);
            }
           
        }

        #endregion

        #region ����

        /// <summary>
        /// ����ICD����
        /// </summary>
        private void LoadInfo()
        {
            ds.Tables.Clear();
            try
            {
                //�����none ֱ�ӷ��� 
                if (ICDTypes.None == type)
                {
                    return;
                }
                //�ȴ�����
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ���ݣ����Ժ�...");
                Application.DoEvents();

                myICD.QueryVisitICD(type, ref ds); //��ѯ 
                if (ds.Tables.Count == 1)
                {
                    //DataColumn[] keys = new DataColumn[] { ds.Tables[0].Columns["sequence_no"] }; //z�������� 
                    //ds.Tables[0].PrimaryKey = keys;

                    this.dvICD = new DataView(ds.Tables[0]);
                }

                LoadICDTree(dvICD);
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// ���س�Ժ���������Ϣ
        /// </summary>
        private void LoadOutPatientInfo()
        {
            dsOPI.Tables.Clear();
            try
            {
                //�ȴ�����
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ���ݣ����Ժ�...");
                Application.DoEvents();

                visitRecordManager.OutPatientQuery(ref dsOPI); //��ѯ 
                if (dsOPI.Tables.Count == 1)
                {
                    //DataColumn[] keys = new DataColumn[] { ds.Tables[0].Columns["sequence_no"] }; //z�������� 
                    //ds.Tables[0].PrimaryKey = keys;

                    this.dvOutPatientInfo = new DataView(dsOPI.Tables[0]);
                    this.fpVisitInfo.DataSource = dvOutPatientInfo;
                    SetFPStyle();
                }

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            
        }

        /// <summary>
        /// ��ѡ���ICD��Χ��ѯ��Ժ������Ϣ
        /// </summary>
        /// <param name="icdRange">ICD��Χ</param>
        private void QueryOPInfoByIcd(string icdRange)
        {
            dsOPI.Tables.Clear();
            try
            {
                //�ȴ�����
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ���ݣ����Ժ�...");
                Application.DoEvents();

                visitRecordManager.OutPatientQueryByICD(ref dsOPI, icdRange); //��ѯ 
                if (dsOPI.Tables.Count == 1)
                {
                    //DataColumn[] keys = new DataColumn[] { ds.Tables[0].Columns["sequence_no"] }; //z�������� 
                    //ds.Tables[0].PrimaryKey = keys;

                    this.dvOutPatientInfo = new DataView(dsOPI.Tables[0]);
                    this.fpVisitInfo.DataSource = dvOutPatientInfo;
                    SetFPStyle();
                }
                else
                {
                    this.fpVisitInfo.DataSource = null;
                }

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// �������ICD��Χ��
        /// </summary>
        /// <param name="dv"></param>
        private void LoadICDTree(DataView dv)
        {
            tvIcdList.ImageList = this.tv.ImageList;
            this.tvIcdList.Nodes.Clear();
            System.Windows.Forms.TreeNode parentNode = new System.Windows.Forms.TreeNode("���ַ�Χ", 0, 0);
            this.tvIcdList.Nodes.Add(parentNode);

            DataTable dt = dv.ToTable(true, "ICDRANGE");//�ϲ��ظ���Χ

            foreach (DataRow dr in dt.Rows)
            {
                parentNode.Nodes.Add(dr["ICDRANGE"].ToString());
            }
            this.tvIcdList.ExpandAll();
        }

        /// <summary>
        /// ��ȡ�����ַ���
        /// </summary>
        /// <returns>�ɹ�����:�����ַ��� ; ʧ�ܷ���:null</returns>
        private string GetFilterStr()
        {
            string filter = string.Empty;

            if (chkState.Checked)
            {
                if (cmbState.SelectedIndex > -1)
                {
                    switch (cmbState.Text.Trim())
                    {
                        case "�����":
                            filter = "(CARD_NO is not null or PATIENT_NO is not null) and ���ʱ�� is null ";
                            break;
                        case "�����":
                            filter = "CARD_NO is  null and PATIENT_NO is null and ���ʱ�� is null ";
                            break;
                        case "�����":
                            filter = "  ���ʱ�� is not null  ";
                            break;
                    }
                }
            }
            if (chkLinkType.Checked && cmbLinkType.SelectedIndex > -1)
            {
                filter += "and ��÷�ʽ='" + cmbLinkType.Text.Trim() + "' ";
            }
            if (chkResult.Checked && cmbResult.SelectedIndex > -1)
            {
                filter += "and ��ý��='" + cmbResult.Text.Trim() + "' ";
            }
            if (chkCircs.Checked && cmbCircs.SelectedIndex > -1)
            {
                filter += "and һ�����='" + cmbCircs.Text.Trim() + "' ";
            }

            if (chkTime.Checked && dtpBegin.Value <= dtpEnd.Value)
            {
                filter += "and ���ʱ��>='"+dtpBegin.Value.ToShortDateString()
                    +"' and ���ʱ��<='"+dtpEnd.Value.ToShortDateString()+"' ";
            }


            if (filter.Length >= 3)
            {
                if (filter.Substring(0, 3) == "and")
                {
                    filter = filter.Substring(4);
                }
            }



            return filter;
        }

        /// <summary>
        /// ���û�����Ϣ�б��ʽ
        /// </summary>
        private void SetFPStyle()
        {
            fpVisitInfo_Sheet1.Columns.Get(0).Width = 70;
            fpVisitInfo_Sheet1.Columns.Get(1).Width = 70;
            fpVisitInfo_Sheet1.Columns.Get(2).Width = 60;
            fpVisitInfo_Sheet1.Columns.Get(3).Width = 60;
            fpVisitInfo_Sheet1.Columns.Get(4).Width = 60;
            fpVisitInfo_Sheet1.Columns.Get(5).Width = 60;
            fpVisitInfo_Sheet1.Columns.Get(6).Width = 60;
            fpVisitInfo_Sheet1.Columns.Get(7).Width = 60;
            fpVisitInfo_Sheet1.Columns.Get(8).Width = 80;
            fpVisitInfo_Sheet1.Columns.Get(9).Width = 80;
            fpVisitInfo_Sheet1.Columns.Get(10).Width = 80;
            fpVisitInfo_Sheet1.Columns.Get(11).Width = 60;
            fpVisitInfo_Sheet1.Columns.Get(12).Width = 60;
            fpVisitInfo_Sheet1.Columns.Get(13).Width = 60;
            fpVisitInfo_Sheet1.Columns.Get(14).Width = 60;
            fpVisitInfo_Sheet1.Columns.Get(15).Width = 60;

            foreach(FarPoint.Win.Spread.Column fpColumn in fpVisitInfo_Sheet1.Columns)
            {
                fpColumn.HorizontalAlignment =FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            }
        }

        /// <summary>
        /// �������� 
        /// </summary>
        private void Export()
        {
            bool ret = false;
            //��������
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "Excel|.xls";
                saveFileDialog1.FileName = "";

                saveFileDialog1.Title = "��������";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    //��Excel ����ʽ��������
                    ret = fpVisitInfo.SaveExcel(saveFileDialog1.FileName);
                    if (ret)
                    {
                        MessageBox.Show("�����ɹ���");
                    }
                }
            }
            catch (Exception ex)
            {
                //������
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ����סԺ�Ż����Ų�ѯ������ϵ���б�
        /// </summary>
        /// <param name="patientNo">סԺ��</param>
        /// <param name="cardNo">������</param>
        private void GetLinkWay(string patientNo, string cardNo)
        {
            fpLinkWay_Sheet1.RowCount = 0;

            ArrayList list = new ArrayList();

            list = linkWayManager.QueryLinkWay(patientNo, cardNo);
            if (list == null)
            {
                return;
            }
            fpLinkWay_Sheet1.Rows.Count = list.Count;
            for (int i = 0; i < list.Count; i++)
            {
                FS.HISFC.Models.HealthRecord.Visit.LinkWay linkWayObj
                    = list[i] as FS.HISFC.Models.HealthRecord.Visit.LinkWay;


                if (linkWayObj != null)
                {
                    this.fpLinkWay_Sheet1.Cells[i, 0].Text = linkWayObj.Name;//��ϵ��
                    this.fpLinkWay_Sheet1.Cells[i, 1].Text = linkWayObj.Memo;//�뻼�߹�ϵ
                    this.fpLinkWay_Sheet1.Cells[i, 2].Text = linkWayObj.Phone;//��ϵ�绰
                    this.fpLinkWay_Sheet1.Cells[i, 3].Text = linkWayObj.User01;//�绰״̬
                    this.fpLinkWay_Sheet1.Cells[i, 4].Text = linkWayObj.Address;//��ϵ��ַ
                    this.fpLinkWay_Sheet1.Cells[i, 5].Text = linkWayObj.Mail;//�����ʼ�

                    this.fpLinkWay_Sheet1.Rows[i].Tag = linkWayObj;

                }
            }

        }

        #endregion
    }
}
