using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SOC.Local.Order.CircuitControl.NanZhuang
{
    /// <summary>
    /// [��������: ��ƿ���ؼ�]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///  />
    /// </summary>
    public partial class ucCircuitCardControl : Neusoft.WinForms.Report.Common.ucQueryBaseForDataWindow, Neusoft.HISFC.BizProcess.Interface.IPrintTransFusion
    {
        /// <summary>
        /// 
        /// </summary>
        public ucCircuitCardControl()
        {
            InitializeComponent();
        }
        Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
        Neusoft.HISFC.BizLogic.Order.Order orderManager = new Neusoft.HISFC.BizLogic.Order.Order();
        Neusoft.HISFC.BizProcess.Integrate.RADT inpatientManager = new Neusoft.HISFC.BizProcess.Integrate.RADT();
        Neusoft.HISFC.Models.RADT.PatientInfo patient = null;
        ArrayList curValues = null; //��ǰ��ʾ������

        /// <summary>
        /// 
        /// </summary>
        protected string inpatientNo;

        /// <summary>
        /// 
        /// </summary>
        bool isPrint = false;
        List<Neusoft.HISFC.Models.RADT.PatientInfo> myPatients = null;
        DateTime dt1;
        DateTime dt2;
        string usCode;
        string execID = "ALL";
        ArrayList al;

        /// <summary>
        /// �Ƿ������ӡδִ�У��Ʒѣ�ҽ��
        /// </summary>
        int isShowNoExecOrder = -1;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patients"></param>
        /// <param name="usageCode"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="isPrinted"></param>
        public void Query(List<Neusoft.HISFC.Models.RADT.PatientInfo> patients, string usageCode, DateTime dtBegin, DateTime dtEnd, bool isPrinted)
        {
            if (isShowNoExecOrder == -1)
            {
                Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();
                isShowNoExecOrder = controlMgr.GetControlParam<int>("HNZY31", true, 0);
            }

            this.myPatients = patients;
            this.usCode = usageCode;
            this.dt1 = dtBegin;
            this.dt2 = dtEnd;
            this.IsPrint = isPrinted;
            this.OnRetrieve(new object[1]);

        }

        /// <summary>
        /// 
        /// </summary>
        public void PrintSet()
        {
            print.ShowPrintPageDialog();
            this.Print();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Print()
        {
            try
            {
                ArrayList al = new ArrayList();// this.orderManager.QueryOrderCircult(inpatientNo, this.dt1, this.dt2, this.usCode, this.IsPrint);
                this.execID = "";

                for (int i = 1; i <= this.dwMain.RowCount; i++)
                {
                    string check = this.dwMain.GetItemString(i, "flag");

                    if (check == "1")
                    {
                        string execSqn = this.dwMain.GetItemString(i, "exec_sqn");

                        al.Add(execSqn);

                        this.execID += "'"+execSqn + "',";
                    }
                }

                if (this.execID.Length > 0)
                {
                    this.execID = this.execID.Substring(0, this.execID.Length - 2);
                    this.execID = this.execID.Substring(1);

                }

                this.OnRetrieve(new object[1]);

                Neusoft.HISFC.BizLogic.Manager.PageSize pgMgr = new Neusoft.HISFC.BizLogic.Manager.PageSize();
                Neusoft.HISFC.Models.Base.PageSize pSize = pgMgr.GetPageSize("Nurse3");
                if (pSize != null)
                {
                    dwMain.Modify("DataWindow.Print.Paper.Size=256");
                    //�˴�����letterֽΪ��216*279 ����Ϊ850*1100
                    //dwMain.Modify("DataWindow.Print.CustomPage.Length=1100");
                    //dwMain.Modify("DataWindow.Print.CustomPage.Width=425");
                    //�˴�����letterֽΪ��216*279
                    dwMain.Modify("DataWindow.Print.CustomPage.Length=140");
                    dwMain.Modify("DataWindow.Print.CustomPage.Width=216");
                }
                else
                {
                    //255�����ء�256������
                    dwMain.Modify("DataWindow.Print.Paper.Size=255");
                    dwMain.Modify("DataWindow.Print.CustomPage.Length=" + pSize.Height);
                    dwMain.Modify("DataWindow.Print.CustomPage.Width=" + pSize.Width);
                }

                dwMain.Modify("flag.visible = false");

                //���ݴ��ڴ�ӡ
                //dwMain.Print(true, true);
                dwMain.Print();

                dwMain.Modify("flag.visible = true");

                #region �����Ѿ���ӡ���
                Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

                this.orderManager.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);

                inpatientNo = "";
                for (int i = 0; i <= this.myPatients.Count - 1; i++)
                {
                    string pNo = this.myPatients[i].ID;
                    inpatientNo += pNo + "','";
                }

                if (al == null || al.Count == 0)
                {
                    return;
                }
                foreach (string execSqn in al)
                {
                    if (this.orderManager.UpdateCircultPrinted(execSqn) == -1)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���´�ӡ���ʧ��!" + orderManager.Err);
                        return;
                    }
                }
                Neusoft.FrameWork.Management.PublicTrans.Commit();

                this.execID = "ALL";

                this.OnRetrieve(new object[1]);

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        protected override int OnRetrieve(params object[] objects)
        {
            inpatientNo = "";
            List<string> listInpatientNo = new List<string>();

            for (int i = 0; i <= this.myPatients.Count - 1; i++)
            {
                string pNo = this.myPatients[i].ID;
                inpatientNo += pNo + ",";
                listInpatientNo.Add(this.myPatients[i].ID);
            }
            inpatientNo = inpatientNo.Substring(0, inpatientNo.Length - 1);
            try
            {
                Neusoft.HISFC.BizProcess.Integrate.Manager managermgr = new Neusoft.HISFC.BizProcess.Integrate.Manager();

                string hosname = managermgr.GetHospitalName();

                dwMain.Modify("t_1.text = '" + hosname + "סԺ������Һ��'");
            }
            catch { }

            string[] myPatientNO = listInpatientNo.ToArray(); 

            //user01Ϊ�Ƿ������� 1Ϊ����ҽ����0��
            //user02Ϊ��ҽ�����ͣ�0Ϊȫ����1Ϊ������2Ϊ����
            base.OnRetrieve(myPatientNO, this.usCode, this.dt1, this.dt2, 
                Neusoft.FrameWork.Function.NConvert.ToInt32(this.IsPrint).ToString(), 
                myPatients[0].User01, myPatients[0].User02,this.execID);

            //���˽̵������datawindow���ʹ���˷���������һ���������
            this.dwMain.CalculateGroups();

            #region ����Ϻ�
            if (this.dwMain.RowCount < 1)//���û��ҽ������
            {
                return 1;
            }
            string curComboID = "";
            //ȡ��Ϻţ�ע��datawindow�Ǵ�1��ʼ���͡�net��һ��
            string tmpComboID = this.dwMain.GetItemString(1, 11) + this.dwMain.GetItemDateTime(1, 12).ToString("yyyyMMddHHmm");
            for (int i = 2; i <= this.dwMain.RowCount; i++)
            {
                curComboID = this.dwMain.GetItemString(i, 11) + this.dwMain.GetItemDateTime(i, 12).ToString("yyyyMMddHHmm");
                if (tmpComboID == curComboID)
                {
                    ///��Һ�� ��Һ���÷�ͬһ��Ĳ�����ʾ����÷�
                    if (this.dwMain.GetItemString(i, "met_ipm_execdrug_use_name").Equals(this.dwMain.GetItemString(i - 1, "met_ipm_execdrug_use_name")))
                    {
                        this.dwMain.SetItemString(i, "met_ipm_execdrug_use_name", "");
                    }
                    //��Ϻ���ȣ������һ��û�б�־˵������ϵĵ�һ��
                    if (string.IsNullOrEmpty(this.dwMain.GetItemString(i - 1, 17).Trim()))
                    {
                        //��ϵ�һ����ֵ
                        this.dwMain.SetItemString(i - 1, "comb_text", "��");
                        //��������һ��
                        if (i == this.dwMain.RowCount)
                            this.dwMain.SetItemString(i, "comb_text", "��");
                        else
                            this.dwMain.SetItemString(i, "comb_text", "��");//���ﲻ���Ƿ���һ�����һ�������һ������ϺŲ���ʱ������
                    }
                    else
                    {
                        //��������һ�Щ���
                        if (i == this.dwMain.RowCount)
                            this.dwMain.SetItemString(i, "comb_text", "��");
                        else
                            this.dwMain.SetItemString(i, "comb_text", "��");
                    }
                }
                else
                {
                    //��ϺŲ��ȣ���ʱ��ı�����Ϻ����ʱ���õ�"��"����"��"��Ϊ"��"
                    if (!string.IsNullOrEmpty(this.dwMain.GetItemString(i - 1, "comb_text").Trim()))
                    {
                        //����һ������һ������
                        if (this.dwMain.GetItemString(i - 1, "comb_text") == "��" || this.dwMain.GetItemString(i - 1, "comb_text") == "��")
                            this.dwMain.SetItemString(i - 1, "comb_text", "��");
                    }
                }
                tmpComboID = curComboID;
            }
            #endregion


            for (int i = 1; i <= this.dwMain.RowCount; i++)
            {
                //����������ʾ
                try
                {
                    string birthday = this.dwMain.GetItemString(i, "txt_birthday");
                    this.dwMain.SetItemString(i, "txt_age", this.orderManager.GetAge(Neusoft.FrameWork.Function.NConvert.ToDateTime(birthday)));
                }
                catch
                {
                }
            }
            return 1;
        }

        #region IPrintTransFusion ��Ա


        public void Query(List<Neusoft.HISFC.Models.RADT.PatientInfo> patients, string usagecode, DateTime dtTime, bool isPrinted)
        {
            return;
        }

        #endregion

        #region IPrintTransFusion ��Ա


        public void Query(List<Neusoft.HISFC.Models.RADT.PatientInfo> patients, string usagecode, DateTime dtBegin, DateTime dtEnd, bool isPrinted, string orderType)
        {
            return;
        }

        public void Query(List<Neusoft.HISFC.Models.RADT.PatientInfo> patients, string usagecode, DateTime dtBegin, DateTime dtEnd, bool isPrinted, bool isFirst)
        {
            return;
        }

        public void Query(List<Neusoft.HISFC.Models.RADT.PatientInfo> patients, string usageCode, DateTime dtBegin, DateTime dtEnd, bool isPrinted, string orderType, bool isFirst)
        {
            return;
        }
        #endregion

        #region IPrintTransFusion ��Ա


        public void SetSpeOrderType(string speStr)
        {
            return;
        }

        #endregion

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            string flag = "";

            if (this.chkAll.Checked)
            {
                flag = "1";
            }
            else
            {
                flag = "0";
            }

            for (int i = 1; i <= this.dwMain.RowCount; i++)
            {
                this.dwMain.SetItemString(i,"flag",flag);
                this.dwMain.Refresh();
            }
        }

        private void dwMain_ItemChanged(object sender, Sybase.DataWindow.ItemChangedEventArgs e)
        {
            if (e.ColumnName == "flag")
            {
                string curComboID = "";
                //ȡ��Ϻţ�ע��datawindow�Ǵ�1��ʼ���͡�net��һ��
                string tmpComboID = this.dwMain.GetItemString(e.RowNumber, 11) + this.dwMain.GetItemDateTime(e.RowNumber, 12).ToString("yyyyMMddHHmm");

                for (int i = 1; i <= this.dwMain.RowCount; i++)
                {
                    if (i == e.RowNumber)
                        continue;

                    curComboID = this.dwMain.GetItemString(i, 11) + this.dwMain.GetItemDateTime(i, 12).ToString("yyyyMMddHHmm");

                    if (tmpComboID == curComboID)
                    {
                        this.dwMain.SetItemString(i, "flag", e.Data);
                    }
                }
            }
        }

    }
}

