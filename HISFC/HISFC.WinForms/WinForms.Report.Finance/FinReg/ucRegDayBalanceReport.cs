using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.Report.Finance.FinReg
{
    public partial class ucRegDayBalanceReport : UserControl
    {
        public ucRegDayBalanceReport()
        {
            InitializeComponent();
            this.InitUC();
        }
        #region
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void InitUC()
        {
            //����ҽԺ���� 
           ��
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                this.neuSpread1_Sheet1.RemoveRows(0, this.neuSpread1_Sheet1.RowCount);
            }
        }
        #endregion
        /// <summary>
        /// ����Ϣ��䵽farpoint��
        /// </summary>
        /// <param name="dayreport">�Һ��ս�ʵ��</param>
        /// <returns></returns>
        public int setFP(FS.HISFC.Models.Registration.DayReport dayreport)
        {
            int BackCount = 0;//�˷�����
            int Disvalid = 0;//��������
            //this.lblDayDate.Text = dayreport.BeginDate.ToString() + "----" + dayreport.EndDate.ToString();
            if (dayreport.Details.Count <= 0) return -1;
            for (int i = 0; i < dayreport.Details.Count; i++) 
            {
                this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
       
                this.neuSpread1_Sheet1.Cells[i,0].Text = dayreport.Details[i].BeginRecipeNo+"��"+dayreport.Details[i].EndRecipeNo;
                this.neuSpread1_Sheet1.Cells[i,1].Text = dayreport.Details[i].Count.ToString();
                if (dayreport.Details[i].Status == FS.HISFC.Models.Base.EnumRegisterStatus.Back)
                {
                    #region {3E1E803B-426C-4db6-9E28-C910544CC8B8} liuq ��Ϊ�Һ��˺Ŵ��¼�ķ�ʽ���ˣ��ս���Ҫ����Ӧ�䶯
                    //this.neuSpread1_Sheet1.Cells[i, 2].Text = (-dayreport.Details[i].OwnCost).ToString();
                    //this.neuSpread1_Sheet1.Cells[i, 3].Text = (-dayreport.Details[i].PayCost).ToString();
                    //this.neuSpread1_Sheet1.Cells[i, 4].Text = (-dayreport.Details[i].RegFee).ToString();
                    //this.neuSpread1_Sheet1.Cells[i, 5].Text = (-dayreport.Details[i].DigFee - dayreport.Details[i].ChkFee).ToString();
                    //this.neuSpread1_Sheet1.Cells[i, 6].Text = (-dayreport.Details[i].OthFee).ToString();
                    //BackCount += dayreport.Details[i].Count; 
                    this.neuSpread1_Sheet1.Cells[i, 2].Text = (dayreport.Details[i].OwnCost).ToString();
                    this.neuSpread1_Sheet1.Cells[i, 3].Text = (dayreport.Details[i].PayCost).ToString();
                    this.neuSpread1_Sheet1.Cells[i, 4].Text = (dayreport.Details[i].RegFee).ToString();
                    this.neuSpread1_Sheet1.Cells[i, 5].Text = (dayreport.Details[i].DigFee - dayreport.Details[i].ChkFee).ToString();
                    this.neuSpread1_Sheet1.Cells[i, 6].Text = (dayreport.Details[i].OthFee).ToString();
                    BackCount += dayreport.Details[i].Count; 
                    #endregion
                 
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[i, 2].Text = dayreport.Details[i].OwnCost.ToString();
                    this.neuSpread1_Sheet1.Cells[i, 3].Text = (dayreport.Details[i].PayCost).ToString();
                    this.neuSpread1_Sheet1.Cells[i, 4].Text = dayreport.Details[i].RegFee.ToString();
                    this.neuSpread1_Sheet1.Cells[i, 5].Text = (dayreport.Details[i].DigFee + dayreport.Details[i].ChkFee).ToString();
                    this.neuSpread1_Sheet1.Cells[i, 6].Text = dayreport.Details[i].OthFee.ToString();
                }
                this.neuSpread1_Sheet1.Cells[i, 7].Text = getStatus(dayreport.Details[i].Status);
                if (dayreport.Details[i].Status == FS.HISFC.Models.Base.EnumRegisterStatus.Cancel)
                {
                    Disvalid+=dayreport.Details[i].Count;
                }
            }
            //�ϼ�
            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount , 1);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "�ϼ�";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = dayreport.SumCount.ToString();
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = dayreport.SumOwnCost.ToString();
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = dayreport.SumPayCost.ToString();
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = dayreport.SumRegFee.ToString();
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = (dayreport.SumDigFee+dayreport.SumChkFee).ToString();
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 6].Text = dayreport.SumOthFee.ToString();
            //��д���
            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 8);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "ʵ�ս��(��д): " + FS.FrameWork.Function.NConvert.ToCapital(dayreport.SumOwnCost);
    
            
            // ����Ա��Ϣ
            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount,1);
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 2);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "�ɿ���: " + dayreport.Oper.Name ;
        
            
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 2, 1, 3);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = "�տ�Ա: " + dayreport.Oper.ID;
         
            
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 5, 1, 3);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = "��������: "+ Disvalid.ToString();
        
            
            //
            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 2);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "�����: " ;
       
            
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 2, 1, 3);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = "����Ա: ";
        
            
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 5, 1, 3);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = "�˷�����:" + BackCount.ToString();
         
            
            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 8);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "ͳ��ʱ��: " + dayreport.BeginDate.ToString() + "---" + dayreport.EndDate.ToString();
                return 1;
        }
        /// <summary>
        /// ���Һ��Ƿ���Чת���ɺ���
        /// </summary>
        /// <param name="status">�Һ�״̬</param>
        /// <returns></returns>
        private string getStatus(FS.HISFC.Models.Base.EnumRegisterStatus status)
        {
            if (status == FS.HISFC.Models.Base.EnumRegisterStatus.Valid)
            { return "����"; }
            else if (status == FS.HISFC.Models.Base.EnumRegisterStatus.Back)
            { return "�˺�"; }
            else if (status == FS.HISFC.Models.Base.EnumRegisterStatus.Cancel)
            { return "����"; }
            else
            { return "����"; }
        }
        private void FPClear()
        {
            if(this.neuSpread1_Sheet1.RowCount > 0 )
            {
                this.neuSpread1_Sheet1.RemoveRows(0, this.neuSpread1_Sheet1.RowCount);
            }
        }

    }
}
