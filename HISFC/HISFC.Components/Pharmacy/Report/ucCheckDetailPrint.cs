using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Pharmacy.Report
{
    /// <summary>
    /// [�ؼ�����: �̵��浥��ѯ���ӡ]
    /// [�� �� ��: ��ú�]
    /// [����ʱ��: 2010-9-19]
    /// [{A7D5A68A-52DF-4682-8E64-2FBBEFC72A20}]
    /// <�޸ļ�¼>
    ///    1.ҩƷ�̵��ѯ����ӯ����������� by Sunjh 2010-9-26 {A9E9AF95-ADF7-455a-816E-BE2F89FCF8E1}
    /// </�޸ļ�¼>
    /// </summary>
    public partial class ucCheckDetailPrint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCheckDetailPrint()
        {
            InitializeComponent();
        }

        #region ����

        ItemCheck itemManager = new ItemCheck();

        private FS.FrameWork.Models.NeuObject currDept = new FS.FrameWork.Models.NeuObject();

        private string currOperName = "";

        DateTime tempDate = new DateTime();

        #endregion

        #region ����

        private void QueryCheckList()
        {
            this.tvCheckList.Nodes.Clear();
            this.tvCheckList.Nodes.Add("�̵㵥�б�");
            string checkState = "1";
            if (this.rbtNoneCommit.Checked)
            {
                checkState = "0";
                this.lblTitle.Text = "�̵㵥�����ʣ�";
            }
            else if (this.rbtCommit.Checked)
            {
                checkState = "1";
                this.lblTitle.Text = "�̵㵥����棩";
            }
            else if (this.rbtCancel.Checked)
            {
                checkState = "2";
                this.lblTitle.Text = "�̵㵥����⣩";
            }
            ArrayList alCheckList = this.itemManager.QueryCheckList(this.currDept.ID, this.dtpStart.Value.Date.ToString(), this.dtpEnd.Value.Date.AddDays(1).ToString(), checkState);
            for (int i = 0; i < alCheckList.Count; i++)
            {
                FS.FrameWork.Models.NeuObject tempObj = alCheckList[i] as FS.FrameWork.Models.NeuObject;
                TreeNode tn = new TreeNode();
                tn.Text = tempObj.ID;
                tn.ToolTipText = tempObj.Memo;
                this.tvCheckList.Nodes[0].Nodes.Add(tn);
            }
            this.tvCheckList.ExpandAll();
        }

        private void ShowData(string checkNO)
        {
            ArrayList alTemp = new ArrayList();
            decimal tFsQty = 0;
            decimal tFsCost = 0;
            decimal tCsQty = 0;
            decimal tCsCost = 0;
            decimal tPsQty = 0;
            decimal tPsCost = 0;

            this.fpCheckDetail.RowCount = 0;
            if (checkNO != "")
            {
                alTemp = itemManager.QueryCheckDetail(currDept.ID, checkNO);
                if (alTemp == null || alTemp.Count == 0)
                {
                    MessageBox.Show("û�в�ѯ������<" + checkNO + ">���̵���ϸ");

                    return;
                }
                for (int i = 0; i < alTemp.Count; i++)
                {
                    FS.HISFC.Models.Pharmacy.Check checkObj = alTemp[i] as FS.HISFC.Models.Pharmacy.Check;
                    this.fpCheckDetail.RowCount = i + 1;
                    this.fpCheckDetail.Cells[i, 0].Text = checkObj.Item.NameCollection.RegularName;
                    this.fpCheckDetail.Cells[i, 1].Text = checkObj.Item.Specs;
                    this.fpCheckDetail.Cells[i, 2].Text = checkObj.Item.PriceUnit;
                    this.fpCheckDetail.Cells[i, 3].Text = checkObj.Item.PriceCollection.RetailPrice.ToString("0.00");
                    this.fpCheckDetail.Cells[i, 4].Text = checkObj.FStoreQty.ToString();
                    this.fpCheckDetail.Cells[i, 5].Text = checkObj.StoreCost.ToString("0.00");
                    this.fpCheckDetail.Cells[i, 6].Text = checkObj.CStoreQty.ToString();
                    this.fpCheckDetail.Cells[i, 7].Text = checkObj.RetailCost.ToString("0.00");
                    if (checkObj.CStoreQty >= checkObj.FStoreQty)
                    {
                        this.fpCheckDetail.Cells[i, 8].Text = checkObj.PurchaseCost.ToString();
                        this.fpCheckDetail.Cells[i, 9].Value = checkObj.ProfitLossQty;
                        tPsQty += checkObj.PurchaseCost;
                        tPsCost += checkObj.ProfitLossQty;
                    }
                    else
                    {
                        this.fpCheckDetail.Cells[i, 8].Text = "-" + checkObj.PurchaseCost.ToString();                                                
                        this.fpCheckDetail.Cells[i, 9].Value = 0 - checkObj.ProfitLossQty;
                        tPsQty += -checkObj.PurchaseCost;
                        tPsCost += -checkObj.ProfitLossQty;
                    }
                    
                    tFsQty += checkObj.FStoreQty;
                    tFsCost += checkObj.StoreCost;
                    tCsQty += checkObj.CStoreQty;
                    tCsCost += checkObj.RetailCost;                    
                }
            }

            //дҳ��
            this.fpCheckDetail.RowCount += 1;
            this.fpCheckDetail.Cells[this.fpCheckDetail.RowCount - 1, 0].Text = "�ϼ�";
            this.fpCheckDetail.Cells[this.fpCheckDetail.RowCount - 1, 1].Text = "Ʒ����:" + Convert.ToString(this.fpCheckDetail.RowCount - 1);
            this.fpCheckDetail.Cells[this.fpCheckDetail.RowCount - 1, 4].Text = tFsQty.ToString("0.00");
            this.fpCheckDetail.Cells[this.fpCheckDetail.RowCount - 1, 5].Text = tFsCost.ToString("0.00");
            this.fpCheckDetail.Cells[this.fpCheckDetail.RowCount - 1, 6].Text = tCsQty.ToString("0.00");
            this.fpCheckDetail.Cells[this.fpCheckDetail.RowCount - 1, 7].Text = tCsCost.ToString("0.00");
            this.fpCheckDetail.Cells[this.fpCheckDetail.RowCount - 1, 8].Text = tPsQty.ToString("0.00");
            this.fpCheckDetail.Cells[this.fpCheckDetail.RowCount - 1, 9].Value = tPsCost;

            this.lbCount.Text = "Ʒ��������" + alTemp.Count.ToString();
            this.lbOper.Text = "�Ʊ��ˣ�" + currOperName;
            this.lbCurrTime.Text = "�Ƶ����ڣ�" + this.itemManager.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd");
        }

        #endregion

        #region �¼�

        public override int Query(object sender, object neuObject)
        {
            this.QueryCheckList();
            return base.Query(sender, neuObject);
        }

        public override int Print(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize("", 850, 1080);
            p.SetPageSize(ps);
            p.IsDataAutoExtend = false;
            p.IsHaveGrid = true;            
            p.PrintPreview(20, 0, this.neuPanel1);
            return base.Print(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            this.neuSpread1.Export();
            
            return base.Export(sender, neuObject);
        }

        protected override void OnLoad(EventArgs e)
        {
            this.dtpStart.Value = itemManager.GetDateTimeFromSysDateTime();
            this.dtpEnd.Value = itemManager.GetDateTimeFromSysDateTime();
            currDept = ((FS.HISFC.Models.Base.Employee)(FS.FrameWork.Management.Connection.Operator)).Dept;
            this.lbDept.Text = "ҩ�����ƣ�" + currDept.Name;
            currOperName = ((FS.HISFC.Models.Base.Employee)(FS.FrameWork.Management.Connection.Operator)).Name;
            base.OnLoad(e);
        }

        private void tvCheckList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.tvCheckList.SelectedNode != null)
            {
                if (this.tvCheckList.SelectedNode.Text != "�̵㵥�б�")
                {
                    this.lbDate.Text = "ִ��ʱ�䣺" + this.tvCheckList.SelectedNode.ToolTipText;
                    this.ShowData(this.tvCheckList.SelectedNode.Text);
                }                
            }
        }

        #endregion

        private void rbtNoneCommit_CheckedChanged(object sender, EventArgs e)
        {
            QueryCheckList();
        }

        string temptFsQty = "0.00";
        string temptFsCost = "0.00";
        string temptCsQty = "0.00";
        string temptCsCost = "0.00";
        string temptPsQty = "0.00";
        string temptPsCost = "0.00";
        
        private void neuSpread1_AutoSortingColumn(object sender, FarPoint.Win.Spread.AutoSortingColumnEventArgs e)
        {
            if (this.fpCheckDetail.Cells[this.fpCheckDetail.RowCount - 1, 0].Text == "�ϼ�")
            {
                temptFsQty = this.fpCheckDetail.Cells[this.fpCheckDetail.RowCount - 1, 4].Text;
                temptFsCost = this.fpCheckDetail.Cells[this.fpCheckDetail.RowCount - 1, 5].Text;
                temptCsQty = this.fpCheckDetail.Cells[this.fpCheckDetail.RowCount - 1, 6].Text;
                temptCsCost = this.fpCheckDetail.Cells[this.fpCheckDetail.RowCount - 1, 7].Text;
                temptPsQty = this.fpCheckDetail.Cells[this.fpCheckDetail.RowCount - 1, 8].Text;
                temptPsCost = this.fpCheckDetail.Cells[this.fpCheckDetail.RowCount - 1, 9].Text;

                this.fpCheckDetail.Rows.Remove( this.fpCheckDetail.Rows.Count - 1, 1 );
            }
        }

        private void neuSpread1_AutoSortedColumn(object sender, FarPoint.Win.Spread.AutoSortedColumnEventArgs e)
        {
            //дҳ��
            this.fpCheckDetail.RowCount += 1;
            this.fpCheckDetail.Cells[this.fpCheckDetail.RowCount - 1, 0].Text = "�ϼ�";
            this.fpCheckDetail.Cells[this.fpCheckDetail.RowCount - 1, 1].Text = "Ʒ����:" + Convert.ToString( this.fpCheckDetail.RowCount - 1 );

            this.fpCheckDetail.Cells[this.fpCheckDetail.RowCount - 1, 4].Text = temptFsQty;
            this.fpCheckDetail.Cells[this.fpCheckDetail.RowCount - 1, 5].Text = temptFsCost;
            this.fpCheckDetail.Cells[this.fpCheckDetail.RowCount - 1, 6].Text = temptCsQty;
            this.fpCheckDetail.Cells[this.fpCheckDetail.RowCount - 1, 7].Text = temptCsCost;
            this.fpCheckDetail.Cells[this.fpCheckDetail.RowCount - 1, 8].Text = temptPsQty;
            this.fpCheckDetail.Cells[this.fpCheckDetail.RowCount - 1, 9].Text = temptPsCost;
        }

    }

    public class ItemCheck : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// ���̵���Һͽ��ʱ���ѯ�̵㵥���б�
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public ArrayList QueryCheckList(string deptCode, string startDate, string endDate,string checkState)
        {
            string strSQL = string.Empty;
            if (checkState != "0")
            {
                strSQL = @"select t.check_code,t.coper_time from pha_com_checkstatic t where t.drug_dept_code='{0}' and t.check_state='{3}' 
                      and t.coper_time >= to_date('{1}','yyyy-MM-dd hh24:mi:ss') and t.coper_time < to_date('{2}','yyyy-MM-dd hh24:mi:ss') ";
            }
            else
            {
                strSQL = @"select t.check_code,t.coper_time from pha_com_checkstatic t where t.drug_dept_code='{0}' and t.check_state='{3}' 
                      and t.foper_time >= to_date('{1}','yyyy-MM-dd hh24:mi:ss') and t.foper_time < to_date('{2}','yyyy-MM-dd hh24:mi:ss') ";
            }
            strSQL = string.Format(strSQL, deptCode, startDate, endDate, checkState);

            ArrayList tempList = new ArrayList();
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "��ѯ�̵��б�ʧ�ܣ�ִ��SQL������" + this.Err;
                this.ErrCode = "-1";
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject tempObj = new FS.FrameWork.Models.NeuObject();
                    tempObj.ID = this.Reader[0].ToString();//�̵㵥��
                    tempObj.Memo = this.Reader[1].ToString();//���ʱ��

                    tempList.Add(tempObj);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��ѯ�̵��б�ʱ����" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return tempList;
        }

        /// <summary>
        ///  ���̵㵥�Ų�ѯ�̵���ϸ����
        /// </summary>
        /// <param name="checkNO"></param>
        /// <returns></returns>
        public ArrayList QueryCheckDetail(string deptCode, string checkNO)
        {
            string strSQL = @"SELECT t.drug_code as ҩƷ����,s.trade_name as ҩƷ����,t.specs as ���,
                   decode(s.split_type,'1',t.pack_unit,t.min_unit) as ��λ, 
                   decode(s.split_type,'1',t.retail_price,round(t.retail_price/t.pack_qty,2)) as ����, 
                   nvl(sum(decode(s.split_type,'1',round(t.fstore_num/t.pack_qty,4),t.fstore_num)),0) as ������,
                   nvl(round(sum(t.retail_price*(t.fstore_num/t.pack_qty)),2),0) as ������,
                   nvl(sum(decode(s.split_type,'1',round(t.cstore_num/t.pack_qty,4),t.cstore_num)),0) as ʵ����, 
                   nvl(round(sum(t.retail_price*(t.cstore_num/t.pack_qty)),2),0) as ʵ�̽��,
                   nvl(sum(decode(s.split_type,'1',round(t.profit_loss_num/t.pack_qty,4),t.profit_loss_num)),0) as ӯ������, 
                   nvl(round(sum(t.profit_loss_num*t.retail_price/t.pack_qty),2),0) as ӯ�����
                   FROM PHA_COM_CHECKDETAIL t,pha_com_baseinfo s    
                   WHERE t.drug_code=s.drug_code and t.drug_dept_code = '{0}' and t.CHECK_CODE = '{1}'
                   group by t.drug_code,s.trade_name,t.specs,s.split_type,t.pack_unit,t.min_unit,t.retail_price,t.pack_qty
                   ORDER BY s.trade_name";
            strSQL = string.Format(strSQL, deptCode, checkNO);

            ArrayList tempList = new ArrayList();
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "��ѯ�̵���ϸʧ�ܣ�ִ��SQL������" + this.Err;
                this.ErrCode = "-1";
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Pharmacy.Check checkObj = new FS.HISFC.Models.Pharmacy.Check();
                    checkObj.Item.ID = this.Reader[0].ToString();//ҩƷ����
                    checkObj.Item.NameCollection.RegularName = this.Reader[1].ToString();//ͨ����
                    checkObj.Item.Specs = this.Reader[2].ToString();//���
                    checkObj.Item.PriceUnit = this.Reader[3].ToString();//��λ
                    checkObj.Item.PriceCollection.RetailPrice = Convert.ToDecimal(this.Reader[4].ToString());//����
                    checkObj.FStoreQty = Convert.ToDecimal(this.Reader[5].ToString());//��������
                    checkObj.StoreCost = Convert.ToDecimal(this.Reader[6].ToString());//�������ʱ�ÿ�������������
                    checkObj.CStoreQty = Convert.ToDecimal(this.Reader[7].ToString());//ʵ������
                    checkObj.RetailCost = Convert.ToDecimal(this.Reader[8].ToString());//ʵ�̽���ʱ�����۽�����������
                    checkObj.PurchaseCost = Convert.ToDecimal(this.Reader[9].ToString());//ӯ����������ʱ�ù��������������
                    checkObj.ProfitLossQty = Convert.ToDecimal(this.Reader[10].ToString());//ӯ�����

                    tempList.Add(checkObj);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��ѯ�̵���ϸʱ����" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return tempList;
        }
    }
}
