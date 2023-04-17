using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.WinForms.Report.DrugStore
{
    /// <summary>
    /// סԺ���ñ�ǩ��ӡ
    /// 
    /// <����˵��>
    ///     1�� ���ñ�ǩ��ӡ 
    ///     2���ڴ�ӡ�ñ�ǩ��ͬʱ ��ӡ�����嵥
    ///     3���õ��ݸ�������ҽԺ��Ŀͬʱ���
    /// </����˵��>
    /// </summary>
    public partial class ucCompoundLabel : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.HISFC.BizProcess.Interface.Pharmacy.ICompoundPrint
    {
        public ucCompoundLabel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ����ҽ��������
        /// </summary>
        private decimal labelTotNum = 0;

        /// <summary>
        /// ������Ϣ��ʾ
        /// </summary>
        private System.Collections.Hashtable hsPatientInfo = new Hashtable();

        /// <summary>
        /// ���Ұ�����
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper deptHelper = null;

        private bool isPrintDetailBill = false;

        /// <summary>
        /// ��ϸ����ӡ
        /// </summary>
        private ucDrugBillDetail ucDetail = null;

        /// <summary>
        /// �嵥
        /// </summary>
        private ucCompoundList ucList = null;

        /// <summary>
        /// �����嵥
        /// </summary>
        private ArrayList alCompoundListData = new ArrayList();

        /// <summary>
        /// ҽ������
        /// </summary>
        private static List<FS.HISFC.Models.Pharmacy.OrderGroup> orderGroupList = null;

        private static string GetCompoundGroup(DateTime useTime)
        {
            if (orderGroupList == null)
            {
                FS.HISFC.BizLogic.Pharmacy.Constant consManager = new FS.HISFC.BizLogic.Pharmacy.Constant();
                orderGroupList = consManager.QueryOrderGroup();
            }

            DateTime juegeTime = new DateTime(2000, 12, 12, useTime.Hour, useTime.Minute, useTime.Second);
            if (orderGroupList != null)
            {
                foreach (FS.HISFC.Models.Pharmacy.OrderGroup info in orderGroupList)
                {
                    if (juegeTime >= info.BeginTime && juegeTime <= info.EndTime)
                    {
                        return info.ID;
                    }
                }
            }

            return "";
        }

        #region ICompoundPrint ��Ա

        /// <summary>
        /// ��Ļ���
        /// </summary>
        public void Clear()
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;
        }

        public void AddAllData(System.Collections.ArrayList al)
        {
            //if (ucDetail == null)
            //{
            //    ucDetail = new ucDrugBillDetail();
            //}

            //FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            //drugBillClass.Name = "������ϸ��";

            //ucDetail.Clear();
            ////���Ƶ�ת�� Ϊɶҵ���Ͳ�ͳһ�أ�����
            //ArrayList alList = new ArrayList();
            //string compoundGroup = "";
            //foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in al)
            //{
            //    FS.HISFC.Models.Pharmacy.ApplyOut tempApply = info.Clone();

            //    string temp = tempApply.User01;
            //    tempApply.User01 = tempApply.User02;
            //    tempApply.User02 = temp;

            //    compoundGroup = GetCompoundGroup(tempApply.UseTime);

            //    alList.Add(tempApply);
            //}

            //ucDetail.IfBPrint = "�������Σ�" + compoundGroup;
            //ucDetail.ShowData(alList, drugBillClass);            

            if (this.ucList == null)
            {
                this.ucList = new ucCompoundList();
            }

            this.ucList.Clear();

            this.alCompoundListData = al;

            this.isPrintDetailBill = true;

        }

        public void AddCombo(System.Collections.ArrayList alCombo)
        {
            this.Clear();

            if (deptHelper == null)
            {
                FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
                ArrayList alDept = deptManager.GetDeptmentAll();
                if (alDept == null)
                {
                    MessageBox.Show("��ȡ���Ұ�������Ϣ��������");
                }
                deptHelper = new FS.FrameWork.Public.ObjectHelper(alDept);
            }

            this.hsPatientInfo.Clear();

            int iCount = 0;
            
            foreach (ArrayList alGroup in alCombo)
            {
                this.neuSpread1_Sheet1.Rows.Count = 0;

                foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alGroup)
                {
                    #region ���û�����Ϣ

                    if (info.PatientNO.Substring(0, 1) == "Z")
                    {
                        this.lbPatientInfo.Text = info.CompoundGroup + "  " + info.UseTime.ToString("yyyy-MM-dd") + string.Format("    {0}   ��{1}�� �˵�{2}��", GetCompoundGroup(info.UseTime), this.labelTotNum.ToString(), (iCount + 1).ToString()); ;
                    }
                    else
                    {
                        this.lbPatientInfo.Text = info.UseTime.ToString("yyyy-MM-dd HH;mm:ss") + string.Format("      ��{0}�� �˵�{1}��", this.labelTotNum.ToString(), (iCount + 1).ToString()); ;                    
                    }

                    if (this.hsPatientInfo.Contains(info.PatientNO))
                    {
                        this.lbDrugInfo.Text = this.hsPatientInfo[info.PatientNO].ToString();
                    }
                    else
                    {
                        if (info.PatientNO.Substring(0, 1) == "Z")
                        {
                            if (info.User01.Length > 3)
                            {
                                this.lbDrugInfo.Text = info.PatientNO.Substring(5) + "     " + info.User01.Substring(4) + "��  " + info.User02 + "  " + deptHelper.GetName(info.ApplyDept.ID);
                            }
                            else
                            {
                                this.lbDrugInfo.Text = info.PatientNO.Substring(5) + "     " + info.User01 + "��  " + info.User02 + "  " + deptHelper.GetName(info.ApplyDept.ID);
                            }
                        }
                        else
                        {
                            this.lbDrugInfo.Text = info.PatientNO + "    " + info.User02 + "  " + deptHelper.GetName(info.ApplyDept.ID);                           
                        }
                        this.hsPatientInfo.Add(info.PatientNO, this.lbDrugInfo.Text);
                    }

                    #endregion

                    #region ������ҩ��Ϣ

                    //this.lbDrugInfo.Text = string.Format("��ҩʱ�䣺{0}  �� {1} ��  �˵� {2} ��",info.UseTime.ToString("HH:mm:ss"),this.labelTotNum.ToString(), (iCount + 1).ToString());

                    #endregion

                    string strDosage = string.Empty;
                    //���ܺ�ʹ�����ڲ��ã����£��õ�ʱ���  20100507
                    //strDosage = Function.DrugDosage.GetStaticDosage(info.Item.ID);

                    this.neuSpread1_Sheet1.Rows.Add(0, 1);
                    this.neuSpread1_Sheet1.Cells[0, 0].Text = info.Item.Name + "[" + strDosage + info.Item.Specs + "]";
                    this.neuSpread1_Sheet1.Cells[0, 1].Text = info.Operation.ApplyQty.ToString();
                    this.neuSpread1_Sheet1.Cells[0, 2].Text = info.Usage.Name;
                }

                iCount++;

                if (iCount != alCombo.Count)
                {
                    this.Print();
                }
            }

        }

        public FS.HISFC.Models.RADT.PatientInfo InpatientInfo
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

        public decimal LabelTotNum
        {
            set 
            {
                this.labelTotNum = value;
            }
        }

        public int Prieview()
        {
            if (this.neuSpread1_Sheet1.Rows.Count > 0)
            {
                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

                FS.HISFC.Components.Common.Classes.Function.GetPageSize("compound", ref print);
                //print.SetPageSize(new System.Drawing.Printing.PaperSize("compound", 420, 320));

                print.PrintPreview(0, 45, this);
            }

            if (ucDetail != null)            
            {
                ucDetail.PrintPreview();
            }

            return 1;
        }

        public int Print()
        {
            if (this.neuSpread1_Sheet1.Rows.Count > 0)
            {
                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

                FS.HISFC.Components.Common.Classes.Function.GetPageSize("compound", ref print);

                //print.SetPageSize(new System.Drawing.Printing.PaperSize("compound", 420, 320));

                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                print.PrintPage(0, 45, this);
            }

            //if (ucDetail != null && this.isPrintDetailBill)
            //{
            //    ucDetail.PrintPreview();
            //    this.isPrintDetailBill = false;
            //}

            if (this.ucList != null && this.isPrintDetailBill)
            {
                this.ucList.ShowData(this.alCompoundListData,false);
                this.isPrintDetailBill = false;
            }

            return 1;
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            
            base.OnLoad(e);
        }
    }
}
