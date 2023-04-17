using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Order;
using FS.HISFC.Models.RADT;

namespace UFC.Lis
{
    /// <summary>
    /// [��������: ���鵥������]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2008-03]<br></br>
    /// </summary>
    public partial class ucLisApply : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucLisApply()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// ҵ�������
        /// </summary>
        private FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();

        /// <summary>
        /// ������Ϣ�б�
        /// </summary>
        protected ArrayList myPatients = null;

        /// <summary>
        /// ���Ұ�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptHelper = null;

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ񲹴�
        /// </summary>
        protected bool RePrint
        {
            get
            {
                return this.ckPrePrint.Checked;
            }
        }

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        protected DateTime BeginDate
        {
            get
            {
                return FS.FrameWork.Function.NConvert.ToDateTime(this.dtpBegin.Text);
            }
        }

        /// <summary>
        /// ��ֹʱ��
        /// </summary>
        protected DateTime EndDate
        {
            get
            {
                return FS.FrameWork.Function.NConvert.ToDateTime(this.dtpEnd.Text);
            }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public ArrayList Patients
        {
            get
            {
                return this.myPatients;
            }
            set
            {
                this.myPatients = value;
            }
        }

        #endregion        

        #region ���鵥��ӡ�ӿڱ���������

        /// <summary>
        /// ������ʾ�ؼ�
        /// </summary>
        protected FS.FrameWork.WinForms.Controls.ucBaseControl displayControl = new ucLisApplyControl();

        /// <summary>
        /// ���ݴ�ӡ�ؼ�
        /// </summary>
        protected FS.FrameWork.WinForms.Controls.ucBaseControl printControl = new ucPrintLisApply();

        /// <summary>
        /// Lis���ݴ���ӿ�
        /// </summary>
        protected ILisDB ILisDBInstance = null;
        #endregion
       
        /// <summary>
        /// ��ѯ
        /// </summary>
        protected virtual void Query()
        {
            if (this.ILisDBInstance != null)
            {
                if (this.ILisDBInstance.ConnectLisOnQuery() == -1)
                {
                    MessageBox.Show("����Lis���ݿ�ʧ��");
                }
            }

            ArrayList al = new ArrayList();
            if (myPatients == null)
            {
                return;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ���鵥��Ϣ...");
            Application.DoEvents();

            for (int i = 0; i < this.myPatients.Count; i++)
            {
                ArrayList alOrder = alOrder = orderManager.QueryOrderLisApplyBill(((FS.FrameWork.Models.NeuObject)this.myPatients[i]).ID, this.BeginDate, this.EndDate, this.RePrint);
                if (alOrder == null)
                {
                    MessageBox.Show(orderManager.Err);
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return;
                }
                //���������Ϣ ��ֵ
                FS.HISFC.Models.RADT.PatientInfo p = ((FS.HISFC.Models.RADT.PatientInfo)myPatients[i]).Clone();	
                string diagnose = this.GetDiagnose(p);

                string strDocName = "";
                string strDiff = "";
                string strItems = "";
                string strID = "";//Ϊ�˸��´�ӡ������

                FS.HISFC.Models.Order.ExecOrder exeTempOrder;		//��ʱ���� �洢ִ�е���Ϣ
                //p.User01 ����ID p.User02 ����  p.User03 ��Ŀ
                //p.PVisit.User01 ִ�п���  p.PVisit.User02 ҽ��  p.PVisit.User01 �ͼ����� p.PVisit.Memo ����
                for (int j = 0; j < alOrder.Count; j++)
                {
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(j);
                    Application.DoEvents();

                    exeTempOrder = alOrder[j] as FS.HISFC.Models.Order.ExecOrder;

                    if (strDiff != exeTempOrder.Order.Combo.ID + exeTempOrder.DateUse.ToString("YYYY-MM-DD HH:mm") + exeTempOrder.Order.Sample.Name)
                    {
                        #region ��ͬ�����Ŀ
                        //����ϴε���Ŀ
                        if (strItems != "")
                        {
                            p.User01 = strID;		        //�ϴ�����
                            p.User03 = strItems;

                            p.PVisit.User02 = strDocName;

                            al.Add(p.Clone());				//�ϴεļ��鵥
                        }

                        strDiff = exeTempOrder.Order.Combo.ID + exeTempOrder.DateUse.ToString("YYYY-MM-DD HH:mm") + exeTempOrder.Order.Sample.Name;
                        //���������Ϣ��ֵ
                        p = ((FS.HISFC.Models.RADT.PatientInfo)myPatients[i]).Clone();						//������һ��
                        p.Diagnoses = new ArrayList(new string[1] { diagnose });

                        strItems = exeTempOrder.Order.Item.Name;
                        strDocName = exeTempOrder.Order.ReciptDoctor.Name;

                        if (((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.IsEmergency == true)
                        {
                            p.ExtendFlag1 = ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.IsEmergency.ToString();//�Ӽ���־
                        }
                        if (this.RePrint)
                        {
                            p.ExtendFlag2 = "True";//�����־
                        }

                        p.User01 = ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).ID;
                        p.User02 = ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Sample.Name;         //��������
                        p.PVisit.User01 = ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.ExeDept.Name; //ִ�п���
                        p.PVisit.User03 = ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).DateUse.ToLongDateString();
                       
                        p.PVisit.Memo = ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Qty.ToString(); //����

                        strID = ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).ID;

                        #endregion
                    }
                    else//��ͬ��
                    {
                        strItems = strItems + "+" + ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Item.Name;

                        p.PVisit.Memo = ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Qty.ToString();//����                          

                        strID = strID + "," + ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).ID;
                    }
                }
                if (strItems != "")
                {
                    p.User01 = strID;
                    p.User03 = strItems;

                    p.PVisit.User02 = strDocName;

                    al.Add(p.Clone());//�ϴεļ��鵥
                }
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            this.AddControlData(al);
        }

        /// <summary>
        /// ���ݴ�ӡ
        /// </summary>
        /// <returns></returns>
        protected virtual int Print()
        {
            #region �������

            ArrayList al = new ArrayList(); //��ֵ����
            FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();//������

            #endregion

            foreach (Control c in this.panelContainer.Controls)
            {
                #region ��������

                //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(orderManager.Connection);
                //t.BeginTransaction();
                //orderManager.SetTrans(t.Trans);
                //itemManager.SetTrans(t.Trans);

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                orderManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                #endregion

                if (((ucLisApplyControl)c).IsSelected)//ѡ���ӡ�ļ��鵥
                {
                    #region ��ӡ״̬����

                    al.Add(((ucLisApplyControl)c).ControlValue);

                    //��ӿ��ƴ�ӡ����	//�ô�ӡ���	//�����������
                    FS.HISFC.Models.RADT.PatientInfo p = ((ucLisApplyControl)c).ControlValue as FS.HISFC.Models.RADT.PatientInfo;

                    List<FS.HISFC.Models.Order.ExecOrder> execList = null;
                    if (this.ILisDBInstance != null)
                    {
                        execList = new List<ExecOrder>();
                    }

                    try
                    {
                        string[] strExeOrderID = p.User01.Split(',');
                        for (int m = 0; m < strExeOrderID.Length; m++)
                        {
                            #region ���ILisDB�ӿ���ʵ�� ���ȡҽ��ִ�е���Ϣ

                            if (this.ILisDBInstance != null)
                            {
                                FS.HISFC.Models.Order.ExecOrder exeOrder = orderManager.QueryExecOrderByExecOrderID(strExeOrderID[m], "2");
                                if (exeOrder == null)
                                {
                                    //t.RollBack();
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("���ִ�е���Ϣ����!" + orderManager.Err);
                                    return -1;
                                }

                                execList.Add(exeOrder);
                            }

                            #endregion

                            #region ���±���ҽ����Ϣ
                            if (orderManager.UpdateExecOrderLisBarCode(strExeOrderID[m], "") == -1)
                            {
                                //t.RollBack();
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("�޷��������룡" + orderManager.Err);
                                return -1;
                            }
                            if (orderManager.UpdateExecOrderLisPrint(strExeOrderID[m]) == -1)//����Ѳ�ؿ���ӡ���
                            {
                                //t.RollBack();
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("�޷����´�ӡ��ǣ�" + orderManager.Err);
                                return -1;
                            }
                            #endregion
                        }

                        #region ���ILisDB�ӿ���ʵ�� ����Lis����

                        if (this.ILisDBInstance != null)
                        {
                            string err = "";
                            if (this.ILisDBInstance.TransDataToLisDB(p, execList,ref err) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(p.Name + " ���ߵ�Lis���ݴ���ʧ��!  " + err);
                                return -1;
                            }
                        }

                        #endregion 
                    }
                    catch (Exception ex)
                    {
                        //t.RollBack();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�޷���ӡ���룡" + ex.Message);
                        return -1;
                    }

                    #endregion
                }

                //t.Commit();
                FS.FrameWork.Management.PublicTrans.Commit();
            }

            #region ��ӡ�������뵥

            Panel panel = new Panel();
            panel.BackColor = Color.White;

            if (al.Count > 0) //��ӡ
            {
                ArrayList alNew = new ArrayList();
                foreach (FS.HISFC.Models.RADT.PatientInfo pa in al)//��ѯ��������ӡ���ż������뵥
                {
                    string strLisID = "";
                    for (int i = 0; i < FS.FrameWork.Function.NConvert.ToInt32(pa.PVisit.Memo); i++)
                    {
                        if (strLisID == "")
                        {
                            strLisID = pa.User01;
                        }

                        pa.User01 = strLisID + "-" + (i + 1).ToString();

                        alNew.Add(pa.Clone());
                    }
                }

                FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(alNew, this.printControl, panel, new System.Drawing.Size(800, 353), 1);

                FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
                try
                {
                    Control c = panel;

                    p.SetPageSize(new System.Drawing.Printing.PaperSize("", 800, 1000));
                    //FS.UFC.Common.Classes.Function.GetPageSize("jyd", ref p);

                    p.IsPrintBackImage = false;
                    p.PrintPreview(8, 1, c);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                this.Query();

                return 0;
            }
            else //ûѡ�񲻴�ӡ
            {
                return -1;
            }

            #endregion
        }

        /// <summary>
        /// ��ȡ���
        /// </summary>
        protected string GetDiagnose(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            FS.HISFC.BizLogic.HealthRecord.Diagnose diag = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            if (patientInfo.CaseState == "1" || patientInfo.CaseState == "2")
            {
                //��ҽ��վ¼�����Ϣ�в�ѯ
                //{014680EC-6381-408b-98FB-A549DAA49B82}
                //ArrayList diagList = diag.QueryCaseDiagnose(patientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
                ArrayList diagList = diag.QueryCaseDiagnose(patientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC,FS.HISFC.Models.Base.ServiceTypes.A);
                if (diagList != null && diagList.Count > 0)
                {
                    FS.HISFC.Models.HealthRecord.Diagnose obj = diagList[0] as FS.HISFC.Models.HealthRecord.Diagnose;
                    return obj.DiagInfo.ICD10.Name;
                }
            }

            return "";
        }

        /// <summary>
        /// ���ݸ�ֵ
        /// </summary>
        /// <param name="alValues">��ӡ����</param>
        protected void AddControlData(ArrayList alValues)
        {
            FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(alValues, this.displayControl, this.panelContainer, new System.Drawing.Size(800, 1200), 1);
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            tv = sender as TreeView;
            if (tv != null && tv.CheckBoxes == false)
                tv.CheckBoxes = true;

            return null;
        }

        protected override int OnSetValues(ArrayList alValues, object e)
        {
            this.Patients = alValues;

            return 1;
        }

        protected override int OnQuery(object sender, object NeuObject)
        {
            this.Query();

            return 1;
        }

        protected override int OnPrint(object sender, object NeuObject)
        {
            return this.Print();
        }

        private void ucLisApply_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.ILisDBInstance != null)
                {
                    if (this.ILisDBInstance.ConnectLisOnLoad() == -1)
                    {
                        MessageBox.Show("����Lis���ݿ�ʧ��");
                    }
                }

                this.ILisDBInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(UFC.Lis.ILisDB)) as UFC.Lis.ILisDB;       

                DateTime dt = this.orderManager.GetDateTimeFromSysDateTime();
                DateTime dt1 = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
                DateTime dt2 = new DateTime(dt.AddDays(1).Year, dt.AddDays(1).Month, dt.AddDays(1).Day, 12, 00, 00);
                this.dtpBegin.Value = dt1;
                this.dtpEnd.Value = dt2;
            }
            catch { }

            FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
            this.deptHelper = new FS.FrameWork.Public.ObjectHelper(managerIntegrate.GetDepartment());
        }

        private void ckCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.panelContainer.Controls.Count <= 1)
                return;
            if (this.ckCheckAll.Checked == true)
            {
                for (int i = 0; i < this.panelContainer.Controls.Count; i++)
                    ((ucLisApplyControl)(this.panelContainer.Controls[i])).IsSelected = true;
            }
            else
            {
                for (int i = 0; i < this.panelContainer.Controls.Count; i++)
                    ((ucLisApplyControl)(this.panelContainer.Controls[i])).IsSelected = false;
            }
        }

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] printType = new Type[1];
                printType[0] = typeof(UFC.Lis.ILisDB);

                return printType;
            }
        }

        #endregion
    }
}
