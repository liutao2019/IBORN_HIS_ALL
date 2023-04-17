using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.DrugStore.Compound
{
    /// <summary>
    /// [��������: ���ñ�ǩ��Ƭ]
    /// [�� �� ��: Sunjh]
    /// [����ʱ��: 2010-10-16]
    /// <˵��>
    ///     1����ֲ�����Һ��ǩ
    ///     2��Ϊ�����ӿ�Ƭ��ʽ��ʾ
    /// </˵��>
    /// </summary>
    public partial class ucCompoundLabel: FS.FrameWork.WinForms.Controls.ucBaseControl,FS.HISFC.BizProcess.Interface.Pharmacy.ICompoundPrint
    {
        public ucCompoundLabel()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// ����ҽ��������
        /// </summary>
        private decimal labelTotNum = 0;

        /// <summary>
        /// ��ǩ���
        /// </summary>
        private int iCount = 0;
        /// <summary>
        /// ͬ���ҳ��
        /// </summary>
        private string iPage = "";
        /// <summary>
        /// ������Ϣ��ʾ
        /// </summary>
        private System.Collections.Hashtable hsPatientInfo = new Hashtable();

        /// <summary>
        /// ���Ұ�����
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper deptHelper = null;
        /// <summary>
        /// ҩƷҵ������
        /// </summary>
        private FS.SOC.HISFC.BizLogic.Pharmacy.Item itemManger = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// סԺ���߹�����
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtIntrgrate = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// �Ƿ񲹴�
        /// </summary>
        private bool isReprint = false;
        /// <summary>
        /// �����嵥
        /// </summary>
        private ArrayList alCompoundListData = new ArrayList();

        /// <summary>
        /// ��Һ���κ� ��Һ���ļ��ݿ�Ƭ������ʽby Sunjh 2010-11-16 {5998231C-4049-4b99-89B1-62BF1A639F4B}
        /// </summary>
        private string groupNO = "";
        /// <summary>
        /// ҽ������
        /// </summary>
        private static List<FS.HISFC.Models.Pharmacy.OrderGroup> orderGroupList = null; 
        #endregion

        #region ����
        /// <summary>
        /// �Ƿ񲹴�
        /// </summary>
        public bool IsReprint
        {
            get
            {
                return isReprint;
            }
            set
            {
                isReprint = value;
            }
        }

        /// <summary>
        /// �Ƿ�Ϊ���ϱ�ǩ�ı��� True ���ϱ�ǩ���� False ��������
        /// </summary>
        public bool IsUnvalidTitle
        {
            set
            {
                if (value == false)
                {
                    this.lbTitle.Text = "��Һ��\r\n";
                }
                else
                {
                    this.lbTitle.Text = "(��)��Һ��\r\n";
                }
            }
        }
        /// <summary>
        /// �Ƿ�Ϊ�Ѵ�ӡ��ǩ�ı��� True  False ��������
        /// </summary>
        public bool IsPrintedTitle
        {
            set
            {
                if (value == false)
                {
                    this.lbTitle.Text = "��Һ��\r\n";
                }
                else
                {
                    this.lbTitle.Text = "(��)��Һ��\r\n";
                }
            }
        }

        /// <summary>
        /// ��Һ���κ� ��Һ���ļ��ݿ�Ƭ������ʽby Sunjh 2010-11-16 {5998231C-4049-4b99-89B1-62BF1A639F4B}
        /// </summary>
        public string GroupNO
        {
            get
            {
                return groupNO;
            }
            set
            {
                groupNO = value;
            }
        } 
        #endregion

        #region ����
        private static string GetCompoundGroup(DateTime useTime)
        {
            if (orderGroupList == null)
            {
                //FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
                FS.SOC.HISFC.BizLogic.Pharmacy.Compound itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Compound();
                orderGroupList = itemMgr.QueryOrderGroup();
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

        /// <summary>
        /// �������뷽��
        /// </summary>
        /// <param name="barCodeType">�������ͣ�ʹ�ó���BARCODETYPE</param>
        /// <param name="barCode">�����</param>
        /// <param name="foreColor">������ɫ</param>
        /// <param name="backColor">������ɫ</param>
        /// <param name="width">���</param>
        /// <param name="height">�߶�</param>
        /// <returns>����ͼƬ</returns>
        public System.Drawing.Image DrawingBarCode(string barCodeType, string barCode, System.Drawing.Color foreColor, System.Drawing.Color backColor, int width, int height)
        {
            //BarcodeLib.Barcode b = new BarcodeLib.Barcode(); //����������

            //b.IncludeLabel = true;//��ʾ�ַ�������,false����ʾ�ַ�������

            //BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;//ѡ������

            //if (barCodeType == "1")
            //{
            //    type = BarcodeLib.TYPE.CODE128;
            //}
            //else if (barCodeType == "2")
            //{
            //    type = BarcodeLib.TYPE.CODE39;
            //}


            //System.Drawing.Image barCodeImage = b.Encode(type, barCode, foreColor, backColor, width, height);

            return null;
        }
        
        #endregion

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
            return;
        }

        /// <summary>
        /// ��ӡҳ�渳ֵ
        /// </summary>
        /// <param name="alCombo"></param>
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

            foreach (ArrayList alGroup in alCombo)
            {
                this.neuSpread1_Sheet1.Rows.Count = 0;

                foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alGroup)
                {
                    #region ǰ5��
                    FS.HISFC.Models.RADT.PatientInfo myPatientInfo = GetInpatientInfo(info.PatientNO);
                    //��һ�У�ҳ��/����ҳ
                    //�ڶ��У�'��'+��ҩ���� +������ +��ǩ�� +������
                    //�����У���������+������ţ���λ��+סԺ��+���κ�
                    //�����У�����+�Ա�+����+Ƶ��+��Ƶ�εĵڼ���
                    //�����У���ҩ����ʱ��+ҽ������+�趨����
                    this.lblLine0.Text = (iCount + 1).ToString() + "/" + this.labelTotNum.ToString() + "ҳ";
                    string printType = string.Empty;
                    if (isReprint == true)
                    {
                        printType = "��";
                    }
                    else
                    {
                        printType = "";
                    }
                    this.lblLine1.Text = printType + info.DrugNO + "(" + info.User03 + ")" + " [" + iPage + "]";
                    this.lblLine2.Text = deptHelper.GetName(info.ApplyDept.ID) + " " + info.ApplyDept.ID;
                    #region ����
                    string bedNo = string.Empty;
                    switch (info.User01.Length)
                    {
                        case 5:
                            bedNo = info.User01.Substring(4, 1);
                            break;
                        case 6:
                            bedNo = info.User01.Substring(4, 2);
                            break;
                        case 7:
                            bedNo = info.User01.Substring(4, 3);
                            break;
                        case 8:
                            bedNo = info.User01.Substring(4, 4);
                            break;
                        case 9:
                            bedNo = info.User01.Substring(4, 5);
                            break;
                        case 10:
                            bedNo = info.User01.Substring(4, 6);
                            break;
                    }
                    #endregion
                    this.lblLine21.Text = bedNo + " " + info.PatientNO.Substring(4, 10) + " " ;
                    this.lbGroup.Text = "��" + info.CompoundGroup + "��";
                    #region Ƶ�εĵڼ���
                    string frequencyTime = string.Empty;
                    //ÿ��һ�εĲ���ʾ��Ƶ�εĵڼ���
                    if (info.Frequency.ID == "QD" || info.Frequency.ID == "QD(06)" || info.Frequency.ID == "QD(07)" || info.Frequency.ID == "QD(11)" || info.Frequency.ID == "QD(12)" || info.Frequency.ID == "QD(16)" || info.Frequency.ID == "QD(17)")
                    {
                        frequencyTime = "";
                    }
                    //ÿ�����ε�
                    else if (info.Frequency.ID == "BID" || info.Frequency.ID == "BID6" || info.Frequency.ID == "BID7" || info.Frequency.ID == "BID719" || info.Frequency.ID == "BID8" || info.Frequency.ID == "BID812" || info.Frequency.ID == "BIDCXY")
                    {
                        if (info.UseTime.TimeOfDay.ToString() == "06:00:00" || info.UseTime.TimeOfDay.ToString() == "07:00:00" || info.UseTime.TimeOfDay.ToString() == "08:00:00" || info.UseTime.TimeOfDay.ToString() == "09:00:00")
                        {
                            frequencyTime = "(1/2)";
                        }
                        else if (info.UseTime.TimeOfDay.ToString() == "12:00:00" || info.UseTime.TimeOfDay.ToString() == "14:00:00" || info.UseTime.TimeOfDay.ToString() == "15:00:00" || info.UseTime.TimeOfDay.ToString() == "16:00:00" || info.UseTime.TimeOfDay.ToString() == "17:00:00" || info.UseTime.TimeOfDay.ToString() == "18:00:00" || info.UseTime.TimeOfDay.ToString() == "19:00:00")
                        {
                            frequencyTime = "(2/2)";
                        }
                    }
                    //ÿ�����ε�
                    else if (info.Frequency.ID == "TID" || info.Frequency.ID == "TID3" || info.Frequency.ID == "TIDCQ")
                    {
                        if (info.UseTime.TimeOfDay.ToString() == "07:00:00" || info.UseTime.TimeOfDay.ToString() == "08:00:00")
                        {
                            frequencyTime = "(1/3)";
                        }
                        else if (info.UseTime.TimeOfDay.ToString() == "11:00:00" || info.UseTime.TimeOfDay.ToString() == "16:00:00")
                        {
                            frequencyTime = "(2/3)";
                        }
                        else if (info.UseTime.TimeOfDay.ToString() == "17:00:00" || info.UseTime.TimeOfDay.ToString() == "20:00:00")
                        {
                            frequencyTime = "(3/3)";
                        }
                    }
                    //��Сʱһ��
                    else if (info.Frequency.ID=="Q8H")
                    {
                        if (info.UseTime.TimeOfDay.ToString() == "00:00:00")
                        {
                            frequencyTime = "(1/3)";
                        }
                        else if (info.UseTime.TimeOfDay.ToString() == "08:00:00")
                        {
                            frequencyTime = "(2/3)";
                        }
                        else if (info.UseTime.TimeOfDay.ToString() == "16:00:00")
                        {
                            frequencyTime = "(3/3)";
                        }
                    }
                    //12Сʱһ��
                    else if (info.Frequency.ID == "Q12H")
                    {
                        if (info.UseTime.TimeOfDay.ToString() == "08:00:00")
                        {
                            frequencyTime = "(1/2)";
                        }
                        else if (info.UseTime.TimeOfDay.ToString() == "20:00:00")
                        {
                            frequencyTime = "(2/2)";
                        }
                    }
                    //ÿ���Ĵε�
                    else if (info.Frequency.ID == "QID" || info.Frequency.ID == "QID710" || info.Frequency.ID == "QID79" || info.Frequency.ID == "QIDCH2" || info.Frequency.ID == "QIDCXT")
                    {
                        if (info.UseTime.TimeOfDay.ToString() == "06:00:00" || info.UseTime.TimeOfDay.ToString() == "07:00:00" || info.UseTime.TimeOfDay.ToString() == "08:00:00")
                        {
                            frequencyTime = "(1/4)";
                        }
                        else if (info.UseTime.TimeOfDay.ToString() == "09:00:00" || info.UseTime.TimeOfDay.ToString() == "10:00:00" || info.UseTime.TimeOfDay.ToString() == "11:00:00" || info.UseTime.TimeOfDay.ToString() == "12:00:00")
                        {
                            frequencyTime = "(2/4)";
                        }
                        else if (info.UseTime.TimeOfDay.ToString() == "14:00:00" || info.UseTime.TimeOfDay.ToString() == "16:00:00")
                        {
                            frequencyTime = "(3/4)";
                        }
                        else
                        {
                            frequencyTime = "(4/4)";
                        }
                    }
                    else if (info.Frequency.ID == "QID711")
                    {
                        if (info.UseTime.TimeOfDay.ToString() == "07:00:00")
                        { 
                            frequencyTime = "(1/4)";
                        }
                        else if (info.UseTime.TimeOfDay.ToString() == "11:00:00")
                        {
                            frequencyTime = "(2/4)";
                        }
                        else if (info.UseTime.TimeOfDay.ToString() == "17:00:00")
                        {
                            frequencyTime = "(3/4)";
                        }
                        else
                        {
                            frequencyTime = "(4/4)";
                        }
                    }
                    //ÿ����ε�
                    else if (info.Frequency.ID == "5ID")
                    {
                        if (info.UseTime.TimeOfDay.ToString() == "00:00:00")
                        {
                            frequencyTime = "(1/5)";
                        }
                        else if (info.UseTime.TimeOfDay.ToString() == "08:00:00")
                        {
                            frequencyTime = "(2/5)";
                        }
                        else if (info.UseTime.TimeOfDay.ToString() == "12:00:00")
                        {
                            frequencyTime = "(3/5)";
                        }
                        else if (info.UseTime.TimeOfDay.ToString() == "16:00:00")
                        {
                            frequencyTime = "(4/5)";
                        }
                        else
                        {
                            frequencyTime = "(5/5)";
                        }
                    }
                    #endregion
                    this.lblLine3.Text = info.User02 + " " + myPatientInfo.Sex.Name + " " + FS.FrameWork.Public.String.GetAge(myPatientInfo.Birthday, this.itemManger.GetDateTimeFromSysDateTime()) + " " + info.Frequency.ID + frequencyTime;
                    #region ҽ������
                    string orderType = string.Empty;
                    if (info.OrderType.ID=="CZ")
                    {
                        orderType = "����";
                    }
                    else
                    {
                        orderType = "��ʱ";
                    }
                    #endregion
                    //this.lblLine4.Text = info.Operation.ApproveOper.OperTime.ToString("yyyy-MM-dd HH:mm") + " " + orderType + " ��" + info.User03 + "��";
                    DateTime dateNow = FS.FrameWork.Function.NConvert.ToDateTime(this.itemManger.GetSysDateTime());
                    this.lblLine4.Text = dateNow.ToString("yyyy-MM-dd HH:mm") + " " + orderType + " ��" + info.User03 + "��";
                    #endregion

                    #region ������ҩ��Ϣ
                    //this.lbDrugInfo.Text = string.Format("��ҩʱ�䣺{0}  �� {1} ��  �˵� {2} ��",info.UseTime.ToString("HH:mm:ss"),this.labelTotNum.ToString(), (iCount + 1).ToString());
                    string strDosage = string.Empty;
                    //���ܺ�ʹ�����ڲ��ã����£��õ�ʱ���  20100507
                    //strDosage = Function.DrugDosage.GetStaticDosage(info.Item.ID);
                    this.lblCmbo.Text = this.lblCmbo.Text + info.User03; //���
                    this.neuSpread1_Sheet1.Rows.Add(0, 2);
                    //����
                    this.neuSpread1_Sheet1.Cells[0, 0].Text = info.Item.Name + "[" + strDosage + info.Item.Specs + "]";
                    this.neuSpread1_Sheet1.Cells[0, 0].ColumnSpan = 2;
                    //�Ƿ���Ч��־
                    this.neuSpread1_Sheet1.Cells[0, 2].Text = (info.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid ? "��" : "x");
                    //��������||�÷�
                    this.neuSpread1_Sheet1.Cells[1, 0].Text = info.Item.BaseDose.ToString() + info.Item.DoseUnit + "      " + info.Usage.Name;
                    this.neuSpread1_Sheet1.Columns[0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                    this.neuSpread1_Sheet1.Cells[1, 0].ColumnSpan = 2;
                    ////�÷�
                    //this.neuSpread1_Sheet1.Cells[1, 1].Text = info.Usage.Name.ToString();
                    //this.neuSpread1_Sheet1.Columns[1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                    //ÿ������
                    this.neuSpread1_Sheet1.Cells[1, 2].Text = info.DoseOnce.ToString() + info.Item.DoseUnit;
                    this.neuSpread1_Sheet1.Columns[2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    //this.neuSpread1_Sheet1.Cells[0, 1].Text = info.Operation.ApplyQty.ToString();
                    //this.neuSpread1_Sheet1.Cells[0, 2].Text = info.Usage.Name + "[" + info.Usage.Memo+"]";
                    #endregion
                }
                this.lblEnd1.Text = "�󷽣�" + this.itemManger.Operator.Name + " ִ�л�ʿ�� ______";

                iCount++;

                if (iCount != (alCombo.Count+1))
                {
                    this.Print();   
                }
            }
            iCount = 0;
        }

        /// <summary>
        /// ��Ƭ���渳ֵ
        /// </summary>
        /// <param name="alGroup"></param>
        public void AddComboNonePrint(System.Collections.ArrayList alGroup)
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

            this.neuSpread1_Sheet1.Rows.Count = 0;

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alGroup)
            {
                #region ���û�����Ϣ
                this.GroupNO = info.CompoundGroup;
                //if (info.PatientNO.Substring(0, 1) == "Z")
                //{
                //    //��Һ���ļ��ݿ�Ƭ������ʽby Sunjh 2010-11-16 {5998231C-4049-4b99-89B1-62BF1A639F4B}
                //    this.GroupNO = info.CompoundGroup;
                //    this.lbPatientInfo.Text = info.CompoundGroup + "  " + info.UseTime.ToString("yyyy-MM-dd") + string.Format("    {0}   ��{1}�� �˵�{2}��", GetCompoundGroup(info.UseTime), this.labelTotNum.ToString(), iCount.ToString()); ;
                //}
                //else
                //{
                //    this.lbPatientInfo.Text = info.UseTime.ToString("yyyy-MM-dd HH;mm:ss") + string.Format("      ��{0}�� �˵�{1}��", this.labelTotNum.ToString(), iCount.ToString()); ;
                //}

                //if (this.hsPatientInfo.Contains(info.PatientNO))
                //{
                //    this.lbDrugInfo.Text = this.hsPatientInfo[info.PatientNO].ToString();
                //}
                //else
                //{
                //    if (info.PatientNO.Substring(0, 1) == "Z")
                //    {
                //        if (info.User01.Length > 3)
                //        {
                //            this.lbDrugInfo.Text = info.PatientNO.Substring(5) + "     " + info.User01.Substring(4) + "��  " + info.User02 + "  " + deptHelper.GetName(info.ApplyDept.ID);
                //        }
                //        else
                //        {
                //            this.lbDrugInfo.Text = info.PatientNO.Substring(5) + "     " + info.User01 + "��  " + info.User02 + "  " + deptHelper.GetName(info.ApplyDept.ID);
                //        }
                //    }
                //    else
                //    {
                //        this.lbDrugInfo.Text = info.PatientNO + "    " + info.User02 + "  " + deptHelper.GetName(info.ApplyDept.ID);
                //    }
                //    this.hsPatientInfo.Add(info.PatientNO, this.lbDrugInfo.Text);
                //}
                #endregion

                #region ǰ5��
                FS.HISFC.Models.RADT.PatientInfo myPatientInfo = GetInpatientInfo(info.PatientNO);
                //��һ�У�ҳ��/����ҳ
                //�ڶ��У�'��'+��ҩ���� +������ +��ǩ�� +������
                //�����У���������+������ţ���λ��+סԺ��+���κ�
                //�����У�����+�Ա�+����+Ƶ��+��Ƶ�εĵڼ���
                //�����У���ҩ����ʱ��+ҽ������+�趨����
                this.lblLine0.Text = (iCount).ToString() + "/" + this.labelTotNum.ToString() + "ҳ";
                string printType = string.Empty;
                if (isReprint == true)
                {
                    printType = "��";
                }
                else
                {
                    printType = "";
                }
                this.lblLine1.Text = printType + info.DrugNO + "(" + info.User03 + ")" + "[" + iPage + "]";
                this.lblLine2.Text = deptHelper.GetName(info.ApplyDept.ID).Replace("����", "");
                #region ����
                string bedNo = string.Empty;
                switch (info.User01.Length)
                {
                    case 5:
                        bedNo = info.User01.Substring(4, 1);
                        break;
                    case 6:
                        bedNo = info.User01.Substring(4, 2);
                        break;
                    case 7:
                        bedNo = info.User01.Substring(4, 3);
                        break;
                    case 8:
                        bedNo = info.User01.Substring(4, 4);
                        break;
                    case 9:
                        bedNo = info.User01.Substring(4, 5);
                        break;
                    case 10:
                        bedNo = info.User01.Substring(4, 6);
                        break;
                }
                #endregion
                this.lblLine21.Text = bedNo + "��  (" + myPatientInfo.ID + ")";
                this.lbGroup.Text = "��" + info.CompoundGroup + "��";
                #region Ƶ�εĵڼ���
                string frequencyTime = string.Empty;
                //ÿ��һ�εĲ���ʾ��Ƶ�εĵڼ���
                if (info.Frequency.ID == "QD" || info.Frequency.ID == "QD(06)" || info.Frequency.ID == "QD(07)" || info.Frequency.ID == "QD(11)" || info.Frequency.ID == "QD(12)" || info.Frequency.ID == "QD(16)" || info.Frequency.ID == "QD(17)")
                {
                    frequencyTime = "";
                }
                //ÿ�����ε�
                else if (info.Frequency.ID == "BID" || info.Frequency.ID == "BID6" || info.Frequency.ID == "BID7" || info.Frequency.ID == "BID719" || info.Frequency.ID == "BID8" || info.Frequency.ID == "BID812" || info.Frequency.ID == "BIDCXY")
                {
                    if (info.UseTime.TimeOfDay.ToString() == "06:00:00" || info.UseTime.TimeOfDay.ToString() == "07:00:00" || info.UseTime.TimeOfDay.ToString() == "08:00:00" || info.UseTime.TimeOfDay.ToString() == "09:00:00")
                    {
                        frequencyTime = "(1/2)";
                    }
                    else if (info.UseTime.TimeOfDay.ToString() == "12:00:00" || info.UseTime.TimeOfDay.ToString() == "14:00:00" || info.UseTime.TimeOfDay.ToString() == "15:00:00" || info.UseTime.TimeOfDay.ToString() == "16:00:00" 
                        || info.UseTime.TimeOfDay.ToString() == "17:00:00" || info.UseTime.TimeOfDay.ToString() == "18:00:00" || info.UseTime.TimeOfDay.ToString() == "19:00:00")
                    {
                        frequencyTime = "(2/2)";
                    }
                }
                //ÿ�����ε�
                else if (info.Frequency.ID == "TID" || info.Frequency.ID == "TID3" || info.Frequency.ID == "TIDCQ")
                {
                    if (info.UseTime.TimeOfDay.ToString() == "07:00:00" || info.UseTime.TimeOfDay.ToString() == "08:00:00")
                    {
                        frequencyTime = "(1/3)";
                    }
                    else if (info.UseTime.TimeOfDay.ToString() == "11:00:00" || info.UseTime.TimeOfDay.ToString() == "16:00:00")
                    {
                        frequencyTime = "(2/3)";
                    }
                    else if (info.UseTime.TimeOfDay.ToString() == "17:00:00" || info.UseTime.TimeOfDay.ToString() == "20:00:00")
                    {
                        frequencyTime = "(3/3)";
                    }
                }
                //��Сʱһ��
                else if (info.Frequency.ID == "Q8H")
                {
                    if (info.UseTime.TimeOfDay.ToString() == "00:00:00")
                    {
                        frequencyTime = "(3/3)";
                    }
                    else if (info.UseTime.TimeOfDay.ToString() == "08:00:00")
                    {
                        frequencyTime = "(1/3)";
                    }
                    else if (info.UseTime.TimeOfDay.ToString() == "16:00:00")
                    {
                        frequencyTime = "(2/3)";
                    }
                }
                //12Сʱһ��
                else if (info.Frequency.ID == "Q12H")
                {
                    if (info.UseTime.TimeOfDay.ToString() == "08:00:00")
                    {
                        frequencyTime = "(1/2)";
                    }
                    else if (info.UseTime.TimeOfDay.ToString() == "20:00:00")
                    {
                        frequencyTime = "(2/2)";
                    }
                }
                //ÿ���Ĵε�
                else if (info.Frequency.ID == "QID" || info.Frequency.ID == "QID710" || info.Frequency.ID == "QID79" || info.Frequency.ID == "QIDCH2" || info.Frequency.ID == "QIDCXT")
                {
                    if (info.UseTime.TimeOfDay.ToString() == "06:00:00" || info.UseTime.TimeOfDay.ToString() == "07:00:00" || info.UseTime.TimeOfDay.ToString() == "08:00:00")
                    {
                        frequencyTime = "(1/4)";
                    }
                    else if (info.UseTime.TimeOfDay.ToString() == "09:00:00" || info.UseTime.TimeOfDay.ToString() == "10:00:00" || info.UseTime.TimeOfDay.ToString() == "11:00:00" 
                        || info.UseTime.TimeOfDay.ToString() == "12:00:00")
                    {
                        frequencyTime = "(2/4)";
                    }
                    else if (info.UseTime.TimeOfDay.ToString() == "14:00:00" || info.UseTime.TimeOfDay.ToString() == "16:00:00")
                    {
                        frequencyTime = "(3/4)";
                    }
                    else
                    {
                        frequencyTime = "(4/4)";
                    }
                }
                else if (info.Frequency.ID == "QID711")
                {
                    if (info.UseTime.TimeOfDay.ToString() == "07:00:00")
                    {
                        frequencyTime = "(1/4)";
                    }
                    else if (info.UseTime.TimeOfDay.ToString() == "11:00:00")
                    {
                        frequencyTime = "(2/4)";
                    }
                    else if (info.UseTime.TimeOfDay.ToString() == "17:00:00")
                    {
                        frequencyTime = "(3/4)";
                    }
                    else
                    {
                        frequencyTime = "(4/4)";
                    }
                }
                //ÿ����ε�
                else if (info.Frequency.ID == "5ID")
                {
                    if (info.UseTime.TimeOfDay.ToString() == "00:00:00")
                    {
                        frequencyTime = "(1/5)";
                    }
                    else if (info.UseTime.TimeOfDay.ToString() == "08:00:00")
                    {
                        frequencyTime = "(2/5)";
                    }
                    else if (info.UseTime.TimeOfDay.ToString() == "12:00:00")
                    {
                        frequencyTime = "(3/5)";
                    }
                    else if (info.UseTime.TimeOfDay.ToString() == "16:00:00")
                    {
                        frequencyTime = "(4/5)";
                    }
                    else
                    {
                        frequencyTime = "(5/5)";
                    }
                }
                #endregion
                this.lblLine3.Text = info.User02 + " " + myPatientInfo.Sex.Name + " " + FS.FrameWork.Public.String.GetAge(myPatientInfo.Birthday, this.itemManger.GetDateTimeFromSysDateTime()) + " " + info.Frequency.ID + frequencyTime;
                #region ҽ������
                string orderType = string.Empty;
                if (info.OrderType.ID == "CZ")
                {
                    orderType = "����";
                }
                else
                {
                    orderType = "��ʱ";
                }
                #endregion 
                //this.lblLine4.Text = info.Operation.ApproveOper.OperTime.ToString("yyyy-MM-dd HH:mm") + " " + orderType + " ��" + info.User03 + "��";
                DateTime dateNow = FS.FrameWork.Function.NConvert.ToDateTime(this.itemManger.GetSysDateTime());
                this.lblLine4.Text = info.UseTime.ToString() + " " + orderType + " ��" + info.User03 + "��";
                #endregion

                #region ������ҩ��Ϣ
                //this.lbDrugInfo.Text = string.Format("��ҩʱ�䣺{0}  �� {1} ��  �˵� {2} ��",info.UseTime.ToString("HH:mm:ss"),this.labelTotNum.ToString(), (iCount + 1).ToString());
                string strDosage = string.Empty;
                //���ܺ�ʹ�����ڲ��ã����£��õ�ʱ���  20100507
                //strDosage = Function.DrugDosage.GetStaticDosage(info.Item.ID);
                this.lblCmbo.Text = this.lblCmbo.Text + info.User03; //���
                this.neuSpread1_Sheet1.Rows.Add(0, 2);
                //����
                this.neuSpread1_Sheet1.Cells[0, 0].Text = info.Item.Name;//+ "[" + strDosage + info.Item.Specs + "]";
                this.neuSpread1_Sheet1.Cells[0, 0].ColumnSpan = 2;
                //�Ƿ���Ч��־
                this.neuSpread1_Sheet1.Cells[1, 2].Text = (info.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid ? "��" : "x");
                //��������
                string str = "";
                if (info.Item.ShiftMark == "���")
                {
                    str = "[���]";
                }
                this.neuSpread1_Sheet1.Cells[1, 0].Text = info.Item.BaseDose.ToString() + info.Item.DoseUnit + str;
                this.neuSpread1_Sheet1.Columns[0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                //�÷�
                this.neuSpread1_Sheet1.Cells[0, 2].Text = info.Usage.Name;
                this.neuSpread1_Sheet1.Columns[1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                //ÿ������
                //if (info.DoseOnce % info.Item.BaseDose != 0)
                //{
                //    Font f = new Font("����", 12, FontStyle.Underline);

                //    this.neuSpread1_Sheet1.Cells[1, 1].Font = f;
                //}
                Font f = new Font("����", 12, FontStyle.Underline);
                this.neuSpread1_Sheet1.Cells[1, 1].Font = f;
                this.neuSpread1_Sheet1.Cells[1, 1].Text = info.DoseOnce.ToString() + info.Item.DoseUnit;
                this.neuSpread1_Sheet1.Columns[2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                
                #endregion
            }
            this.lblEnd1.Text = "�󷽣�" + this.itemManger.Operator.Name + " ��  ׼1�� ______";

            this.pbBarcode.Image = this.DrawingBarCode("1", this.groupNO, Color.Black, Color.White, pbBarcode.Width, pbBarcode.Height);
        }

        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <returns></returns>
        private FS.HISFC.Models.RADT.PatientInfo GetInpatientInfo(string inpatientNO)
        {
            if (hsPatientInfo.ContainsKey(inpatientNO))
            {
                return hsPatientInfo[inpatientNO] as FS.HISFC.Models.RADT.PatientInfo;
            }
            else
            {
                FS.HISFC.Models.RADT.PatientInfo patientInfo = this.radtIntrgrate.GetPatientInfoByPatientNO(inpatientNO);
                hsPatientInfo.Add(inpatientNO, patientInfo);
                return patientInfo;
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

        /// <summary>
        /// ��ǩ���
        /// </summary>
        public int ICount
        {
            get 
            {
                return iCount; 
            }
            set 
            { 
                iCount = value; 
            }
        }
        /// <summary>
        /// ͬ���ҳ��
        /// </summary>
        public string IPage
        {
            get
            {
                return iPage;
            }
            set
            {
                iPage = value;
            }
        }

        /// <summary>
        /// ��ӡԤ��
        /// </summary>
        /// <returns></returns>
        public int Prieview()
        {
            if (this.neuSpread1_Sheet1.Rows.Count > 0)
            {
                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

                FS.HISFC.Models.Base.PageSize pageSize = new FS.HISFC.Models.Base.PageSize();

                FS.HISFC.BizLogic.Manager.PageSize pageMgr = new FS.HISFC.BizLogic.Manager.PageSize();

                print.SetPageSize(pageSize);

                print.PrintPreview(0, 0, this);
            }

            return 1;
        }

        public int Print()
        {
            if (this.neuSpread1_Sheet1.Rows.Count > 0)
            {
                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize("compound", 0, 0);
                //�ϱ߾�
                ps.Top = 0;
                //��߾�
                ps.Left = 0;
                print.SetPageSize(ps);
                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                print.PrintPage(0, 0, this);
            }

            return 1;
        }

        #endregion

        #region �¼�
        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad(e);
        } 
        #endregion

    }
}
