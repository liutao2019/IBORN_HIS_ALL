using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.HealthRecord.UploadGuangDong
{
    /// <summary>
    /// [��������: �ϴ� ҽ�����﹤����־��tWorkLog�� ���﹤����־�����ֿƣ���tEmergeLogNoKs�� ר�����ﲡ������tSpecialLog��]<br></br>
    /// ��ݸ��sql ��  Report.DoctWordStat    ������  tWorkLog  ���� 0  ������ר�ҿ��Ҷ��� CASEWORKLOGSPDOC   ��ͨ���Ҷ��� ��CASETWORKLOG 
    /// [�� �� ��:��������]<br></br>
    /// [����ʱ��: 2011-07-22]<br></br>
    public partial class ucWorkLogUpLoad : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucWorkLogUpLoad()
        {
            InitializeComponent();
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
        }
        FS.HISFC.BizLogic.HealthRecord.Base baseMgr = new FS.HISFC.BizLogic.HealthRecord.Base();
         
        List<FS.FrameWork.Models.NeuObject> deptList = null;
        List<FS.FrameWork.Models.NeuObject> zkDeptList = null;
        List<FS.FrameWork.Models.NeuObject> docList = null;

        private bool isMut = true;

        /// <summary>
        /// �Ƿ��Ժ��
        /// </summary>
        [Category("��Ժ��"), Description("�Ƿ��Ժ��")]
        public bool IsMut
        {
            get { return this.isMut; }
            set { this.isMut = value; }
        }
        /// <summary>
        /// ��ѯ�ϴ�����־�Ƿ�Ϊ���յ�
        /// </summary>
        bool issingleday;

        System.Collections.ArrayList alUpload = null;
        DateTime begin;
        DateTime end;
        string Type = string.Empty;
        
        private FS.FrameWork.WinForms.Forms.ToolBarService tool = new FS.FrameWork.WinForms.Forms.ToolBarService();

        private void ucUpLoad_Load(object sender, EventArgs e)
        {
            FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface upLoadInterFace = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface();
            DateTime today = this.baseMgr.GetDateTimeFromSysDateTime();
            this.neuDateTimePickerFrom.Value = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);
            this.neuDateTimePickerTo.Value = new DateTime(today.Year, today.Month, today.Day, 23, 59, 59);
            this.neuSpread1_Sheet1.ColumnHeader.Rows[0].Height = 30f;
           
            //����3.0�Ŀ��ұ����� �� ҽ�������ݣ����ڻ�ȡͳһ�ţ�
            deptList = upLoadInterFace.GetDeptCodeByCode();//��ͨ����

            zkDeptList = upLoadInterFace.GetZkDeptCodeByCode();// ר�ƿ���

            docList = upLoadInterFace.GetDocCodeByCode();//ҽ��

        }

        /// <summary>
        /// ��ʼ������Ӱ�ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit( object sender, object neuObject, object param )
        {
            this.tool.AddToolButton("ҽ�����﹤����־", "ҽ�����﹤����־",FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ, true, false, null);
            this.tool.AddToolButton("���﹤����־", "���﹤����־", FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ, true, false, null);
            this.tool.AddToolButton("ר�����ﲡ����", "ר�����ﲡ����", FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ, true, false, null);
            this.tool.AddToolButton("�ٴ�·��ͳ��", "�ٴ�·��ͳ��", FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ, true, false, null);
            this.tool.AddToolButton("סԺ������־", "סԺ������־", FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ, true, false, null);

            this.tool.AddToolButton("�ϴ�", "�ϴ�", FS.FrameWork.WinForms.Classes.EnumImageList.J���, true, false, null);

            return tool;
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            //MessageBox.Show ("aa");

            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;

            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            switch (Type)
            {
                case "1":
                    p.PrintPreview(250, 20, this.neuSpread1);
                    break;

                case "2":
                    p.PrintPreview(100,20,this.neuSpread1);
                    break;

                case "3":
                    p.PrintPreview(80,10,this.neuSpread1);
                    break;
            }
            //p.PrintPreview(this.neuPanel2);
            return base.OnPrint(sender, neuObject);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Export(object sender, object neuObject)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "(Excel�ļ�)|*.xls";
            sf.FileName = a.Text;
            if (DialogResult.OK == sf.ShowDialog())
            {
                this.neuSpread1.SaveExcel(sf.FileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
            }
            return base.Export(sender, neuObject);
        }

        /// <summary>
        /// �Զ��幦�ܰ�ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked( object sender, ToolStripItemClickedEventArgs e )
        {
            int days=0;
            switch (e.ClickedItem.Text)
            {
                case "ҽ�����﹤����־":
                    days=this.MutDateTime();
                    if (days == 1)//һ��
                    {
                        Type = "tWorkLog";
                        this.SetMSpecial(Type);
                        this.a.Text = "ҽ�����﹤����־";
                    }
                    else
                    {
                        Type = "tWorkLog";
                        this.SetMutUpload(Type, days);
                        this.a.Text = "ҽ�����﹤����־";
                    }
                    break;
                case "���﹤����־":
                    days=this.MutDateTime();
                    if (days == 1)//һ��
                    {
                        Type = "tEmergeLog";
                        this.SetMSpecial(Type);
                        this.a.Text = "���﹤����־";
                    }
                    else
                    {
                        Type = "tEmergeLog";
                        this.SetMutUpload(Type, days);
                        this.a.Text = "���﹤����־";
                    }
                    break;
                case "ר�����ﲡ����":
                    days=this.MutDateTime();
                    if (days == 1)//һ��
                    {
                        Type = "tSpecialLog";
                        this.SetMSpecial(Type);
                        this.a.Text = "ר�����ﲡ����";
                    }
                    else
                    {
                        Type = "tSpecialLog";
                        this.SetMutUpload(Type, days);
                        this.a.Text = "ר�����ﲡ����";
                    }
                    break;
                case "סԺ������־":
                    days=this.MutDateTime();
                    if (days == 1)//һ��
                    {
                        Type = "TZyWardWorklog";
                        this.SetMSpecial(Type);
                        this.a.Text = "סԺ������־";
                    }
                    else //����
                    {
                        Type = "TZyWardWorklog";
                        this.SetMutUpload(Type, days);
                        this.a.Text = "סԺ������־";
                    }
                    break;
                case "�ϴ�":
                    this.SetEntity();
                    break;
                case "�ٴ�·��ͳ��":
                    Type = "clinicPath";
                    this.SetMSpecial(Type);
                    this.a.Text = "�ٴ�·��ͳ��";
                    break;

            }
            base.ToolStrip_ItemClicked( sender, e );
        }
        /// <summary>
        /// �ж�ʱ����
        /// 
        /// </summary>
        /// <returns></returns>
        private int MutDateTime()
        {
            int ret;
            TimeSpan ts = neuDateTimePickerTo.Value.Date - neuDateTimePickerFrom.Value.Date;
            if (ts.Days > 0)
            {
                ret = ts.Days +1;
                MessageBox.Show("ѡ��ʱ���ȴ���1�죬���潫����ʾ���ݣ���ʱ��������ϴ�");
            }
            else if (ts.Days == 0)
            {
                 ret = 1;
            }
            else
            {
                MessageBox.Show("����ʱ�䲻��С�ڿ�ʼʱ��");
                ret = 0;
            }
            return ret;
        }
        System.Data.DataSet dsMSpecil = null;

        System.Data.DataView drView = null;

        /// <summary>
        /// ��ѯ���ݷ���
        /// </summary>
        /// <param name="Type"></param>
        private void SetMSpecial(string Type)
        {
            FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface upLoadInterFace = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface();
            FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.WorklogFuntion hh = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.WorklogFuntion();
            dsMSpecil = new DataSet();
            this.neuSpread1_Sheet1.DataSource = "";
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.ClearColumnHeaderSpanCells();
            if (neuSpread1_Sheet1.RowCount > 0)
            {
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.Rows.Count);
            }
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ���ݣ��������ĵȴ�...");
            Application.DoEvents();
           
            //�ϴ���ʱ��ֻ����ͬһ��
            if (neuDateTimePickerFrom.Value.Date == neuDateTimePickerTo.Value.Date)
            {
                issingleday = true;
            }
            else { issingleday = false; }

            //��ݸ��Ժ����ʹ�þ�̬������ȡ���ݰ�
            string Sql = string.Empty;
            string execError = string.Empty;
//            if (isMut)
//            {
//                if (Type == "clinicPath")
//                {
//                    Sql = @"SELECT (SELECT hos_name FROM COM_HOSPITALINFO )  AS Ժ������ ,count(*)  AS ����  FROM MET_CAS_BASE 
//                            WHERE OUT_DATE BETWEEN '{0}' AND '{1}'  AND ect_numb='1' ";
//                    Sql = string.Format(Sql, neuDateTimePickerFrom.Value.Date, neuDateTimePickerTo.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59));
//                }
//                else
//                {
//                    Sql = hh.GetWorkLogUploadSql(Type, neuDateTimePickerFrom.Value, neuDateTimePickerTo.Value);
//                }

//                if (Sql != string.Empty)
//                {
//                    FS.UFC.Common.Classes.Function.ExecSQL(Sql, ref execError, ref dsMSpecil);
//                }
//            }
//            else
//            {
                if (hh.GetMSpecial(ref dsMSpecil, Type, neuDateTimePickerFrom.Value, neuDateTimePickerTo.Value) == -1)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("���MSpecial����" + hh.Err);
                    return;
                }
            //}
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            drView = new System.Data.DataView(dsMSpecil.Tables[0]);
            this.neuSpread1.DataSource = drView;
            this.setFP();

            //this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
        }

        /// <summary>
        /// �����ϴ�
        /// </summary>
        /// <param name="Type"></param>
        private void SetMutUpload(string Type, int days)
        {
            FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface upLoadInterFace = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface();
            FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.WorklogFuntion hh = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.WorklogFuntion();
            dsMSpecil = new DataSet();
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ���ݣ��������ĵȴ�...");
            Application.DoEvents();

            string Sql = string.Empty;
            string execError = string.Empty;

            DateTime dtBegin = neuDateTimePickerFrom.Value.Date;
            alUpload = new System.Collections.ArrayList();

            for (int day = 0; day < days; day++)
            {
                if (day > 0)
                {
                    dtBegin = dtBegin.AddDays(1);
                }
                if (hh.GetMSpecial(ref dsMSpecil, Type, dtBegin, dtBegin) == -1)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("���MSpecial����" + hh.Err);
                    return;
                }
                if (dsMSpecil == null || dsMSpecil.Tables.Count == 0 || dsMSpecil.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("δ��ȡ�����ݣ�" + hh.Err);
                    return;
                }
                FS.HISFC.Models.HealthRecord.Base b = new FS.HISFC.Models.HealthRecord.Base();
                switch (Type)
                {
                    case "tWorkLog":
                        foreach (DataRow rows in dsMSpecil.Tables[0].Rows)
                        {
                            b = new FS.HISFC.Models.HealthRecord.Base();
                            b.PatientInfo.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(rows[0].ToString());//����
                            b.InDept.ID = rows[1].ToString();//ͳһ�ƺ�
                            b.InDept.Name = rows[2].ToString();//��������
                            b.PatientInfo.DoctorReceiver.ID = rows[3].ToString();//ͳһҽ������
                            b.PatientInfo.DoctorReceiver.Name = rows[4].ToString();//ҽ������
                            b.PatientInfo.DoctorReceiver.Memo = rows[5].ToString();//ҽ��ְ��
                            b.OutDept.ID = rows[6].ToString();//ͳһר�ƺ�
                            b.OutDept.Name = rows[7].ToString();//ר�ƺ�
                            //b.OutDept.ID = "chengym";//ͳһר�ƺ� ������
                            b.PatientInfo.Memo = rows[8].ToString();//��ʱ
                            b.PatientInfo.User01 = rows[9].ToString();//�����˴�
                            b.PatientInfo.User02 = rows[10].ToString();//ר������
                            b.PatientInfo.User03 = rows[11].ToString();//ר������
                            b.PatientInfo.UserCode = rows[12].ToString();//�������
                            b.PatientInfo.WBCode = rows[13].ToString();//��������
                            b.PatientInfo.PVisit.User01 = rows[14].ToString();
                            b.PatientInfo.PVisit.User02 = rows[15].ToString();
                            b.PatientInfo.PVisit.OutTime = this.baseMgr.GetDateTimeFromSysDateTime();
                            alUpload.Add(b);
                        }
                        //MessageBox.Show("����ʵ��ɹ�tWorkLog");
                        break;
                    case "tEmergeLog":
                        foreach (DataRow rows in dsMSpecil.Tables[0].Rows)
                        {
                            b = new FS.HISFC.Models.HealthRecord.Base();
                            b.PatientInfo.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(rows[0].ToString());//����
                            b.InDept.ID = rows[1].ToString();//ͳһ�ƺ�
                            b.InDept.Name = rows[2].ToString();//��������
                            b.PatientInfo.ID = rows[3].ToString();//ҽ������
                            //b.PatientInfo.ID = "100000"; //������
                            b.PatientInfo.Memo = rows[4].ToString();//��ʱ
                            b.PatientInfo.User01 = rows[5].ToString();//�����˴�
                            b.PatientInfo.User02 = rows[6].ToString();//��������
                            b.PatientInfo.User03 = rows[7].ToString();//��������
                            b.PatientInfo.UserCode = rows[8].ToString();//���ȳɹ�����
                            b.PatientInfo.WBCode = rows[9].ToString();//��������
                            b.PatientInfo.PVisit.User01 = rows[10].ToString();//��������
                            b.PatientInfo.PVisit.OutTime = this.baseMgr.GetDateTimeFromSysDateTime();//��������
                            alUpload.Add(b);
                        }
                        //MessageBox.Show("����ʵ��ɹ�tEmergeLogNoKs");
                        break;
                    case "tSpecialLog":
                        foreach (DataRow rows in dsMSpecil.Tables[0].Rows)
                        {
                            b = new FS.HISFC.Models.HealthRecord.Base();

                            b.PatientInfo.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(rows[0].ToString());//����
                            b.InDept.ID = rows[1].ToString();//ͳһ�ƺ�
                            b.InDept.Name = rows[2].ToString();//��������
                            b.OutDept.ID = rows[3].ToString();//ͳһר�ƺ�
                            b.OutDept.Name = rows[4].ToString();//ר�ƺ�
                            //b.OutDept.ID = "chengym";//ͳһר�ƺ� ������
                            b.PatientInfo.ID = rows[5].ToString();//��������
                            b.PatientInfo.PVisit.OutTime = this.baseMgr.GetDateTimeFromSysDateTime();//��������
                            alUpload.Add(b);
                        }
                        break;
                    case "TZyWardWorklog":
                        FS.HISFC.Models.HealthRecord.Case.PatientMove patientMove = null;

                        foreach (DataRow rows in dsMSpecil.Tables[0].Rows)
                        {
                            patientMove = new FS.HISFC.Models.HealthRecord.Case.PatientMove();
                            patientMove.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(rows[0].ToString());
                            patientMove.DeptCode = rows[1].ToString();
                            patientMove.OperDept = rows[2].ToString();
                            patientMove.BedNum = FS.FrameWork.Function.NConvert.ToInt32(rows[3].ToString());

                            patientMove.OriginalNum = FS.FrameWork.Function.NConvert.ToInt32(rows[5].ToString());
                            patientMove.InNum = FS.FrameWork.Function.NConvert.ToInt32(rows[6].ToString());
                            patientMove.OtherDeptIn = FS.FrameWork.Function.NConvert.ToInt32(rows[7].ToString());
                            patientMove.OtherRegionIn = FS.FrameWork.Function.NConvert.ToInt32(rows[8].ToString());
                            patientMove.OutNum = FS.FrameWork.Function.NConvert.ToInt32(rows[9].ToString());
                            patientMove.DeadNum = FS.FrameWork.Function.NConvert.ToInt32(rows[10].ToString());
                            patientMove.ToOtherDept = FS.FrameWork.Function.NConvert.ToInt32(rows[11].ToString());
                            patientMove.ToOtherRegion = FS.FrameWork.Function.NConvert.ToInt32(rows[12].ToString());
                            patientMove.PatientNum = FS.FrameWork.Function.NConvert.ToInt32(rows[13].ToString());
                            patientMove.AccompanyNum = FS.FrameWork.Function.NConvert.ToInt32(rows[14].ToString());
                            patientMove.BeduseNum = FS.FrameWork.Function.NConvert.ToInt32(rows[15].ToString());
                            patientMove.Memo = rows[16].ToString();
                            alUpload.Add(patientMove);
                        }
                        break;
                    default:
                        break;
                }
            }
            try
            {
                upLoadInterFace = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface();
            }
            catch (Exception ex)
            {
                MessageBox.Show("����ʧ��!" + ex.Message);
                //return -1;
            }
            int deleteNum = 0;
            int intReturn = 0;
            bool isExist = false;
            try
            {
                switch (Type)
                {
                    case "tWorkLog":
                        intReturn = upLoadInterFace.InserttWorkLog(alUpload, begin.ToShortDateString(), end.ToShortDateString(), ref isExist);
                        break;
                    case "tEmergeLog":
                        intReturn = upLoadInterFace.InserttEmergeLog(alUpload, begin.ToShortDateString(), end.ToShortDateString(), ref isExist);
                        break;
                    case "tSpecialLog":
                        intReturn = upLoadInterFace.InserttSpecialLog(alUpload, begin.ToShortDateString(), end.ToShortDateString(), ref isExist);
                        break;
                    case "TZyWardWorklog":
                        intReturn = upLoadInterFace.InsertTZyWardWorklog(alUpload, begin.ToShortDateString(), end.ToShortDateString(), ref deleteNum);
                        break;
                }
                if (intReturn == -1)
                {
                    upLoadInterFace.Rollback();
                    MessageBox.Show(upLoadInterFace.Err + ",�ϴ�����");
                    return;
                }
                else
                {
                    if (isExist)
                    {
                        if (DialogResult.No == MessageBox.Show("�Ѿ�����ؼ�¼��Ҫ������", "����",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1))
                        {
                            upLoadInterFace.Rollback();
                            MessageBox.Show("ȡ���ϴ�");
                            return;
                        }
                    }
                    upLoadInterFace.Commit();
                }
                MessageBox.Show("�ϴ��ɹ�" + intReturn.ToString() + "����¼��");
            }
            catch (Exception ex)
            {
                upLoadInterFace.Rollback();
                MessageBox.Show(ex.Message);
                //return -1;
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }
        private void setFP()
        {
            switch (Type)
            {
                case "tWorkLog":
                    this.neuSpread1_Sheet1.Columns.Get(1).AllowAutoSort = true;
                    this.neuSpread1_Sheet1.Columns.Get(2).AllowAutoSort = true;
                    this.neuSpread1_Sheet1.Columns.Get(4).AllowAutoSort = true;
                    this.neuSpread1_Sheet1.Columns.Get(9).AllowAutoSort = true;

                    this.neuSpread1_Sheet1.Columns.Get(0).Width = 85;
                    this.neuSpread1_Sheet1.Columns.Get(1).Width = 50;
                    this.neuSpread1_Sheet1.Columns.Get(2).Width = 80;
                    this.neuSpread1_Sheet1.Columns.Get(3).Width = 50;
                    this.neuSpread1_Sheet1.Columns.Get(4).Width = 80;
                    this.neuSpread1_Sheet1.Columns.Get(5).Width = 40;
                    this.neuSpread1_Sheet1.Columns.Get(6).Width = 40;
                    this.neuSpread1_Sheet1.Columns.Get(7).Width = 80;
                    this.neuSpread1_Sheet1.Columns.Get(8).Width = 60;
                    this.neuSpread1_Sheet1.Columns.Get(9).Width = 70;
                    this.neuSpread1_Sheet1.Columns.Get(10).Width = 30;
                    this.neuSpread1_Sheet1.Columns.Get(11).Width = 30;
                    this.neuSpread1_Sheet1.Columns.Get(12).Width = 30;
                    this.neuSpread1_Sheet1.Columns.Get(13).Width = 30;
                    this.neuSpread1_Sheet1.Columns.Get(14).Width = 30;
                    this.neuSpread1_Sheet1.Columns.Get(15).Width = 80;
                    break;
                case "tEmergeLogNoKs":

                    this.neuSpread1_Sheet1.Columns.Get(0).Width = 85;
                    this.neuSpread1_Sheet1.Columns.Get(1).Width = 50;
                    this.neuSpread1_Sheet1.Columns.Get(2).Width = 80;
                    this.neuSpread1_Sheet1.Columns.Get(3).Width = 50;
                    this.neuSpread1_Sheet1.Columns.Get(4).Width = 80;
                    this.neuSpread1_Sheet1.Columns.Get(5).Width = 40;
                    this.neuSpread1_Sheet1.Columns.Get(6).Width = 40;
                    this.neuSpread1_Sheet1.Columns.Get(7).Width = 80;
                    this.neuSpread1_Sheet1.Columns.Get(8).Width = 60;
                    this.neuSpread1_Sheet1.Columns.Get(9).Width = 100;

                    break;
                case "tSpecialLog":

                    this.neuSpread1_Sheet1.Columns.Get(0).Width = 85;
                    this.neuSpread1_Sheet1.Columns.Get(1).Width = 80;
                    this.neuSpread1_Sheet1.Columns.Get(2).Width = 80;
                    this.neuSpread1_Sheet1.Columns.Get(3).Width = 80;
                    this.neuSpread1_Sheet1.Columns.Get(4).Width = 60;
                    this.neuSpread1_Sheet1.Columns.Get(5).Width = 60;
                    this.neuSpread1_Sheet1.Columns.Get(6).Width = 85;

                    break;
            
            }

        }

        /// <summary>
        /// �ϴ�����
        /// </summary>
        private void SetEntity()
        {
            FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface upLoadInterFace = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface();
            FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.WorklogFuntion hh = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.WorklogFuntion();
            begin = this.neuDateTimePickerFrom.Value;
            end = this.neuDateTimePickerTo.Value;
            if (!issingleday)
            {
                MessageBox.Show("�����ϴ�����־����1�죬������ѡ�����ڲ�ѯ���ٳ����ϴ���", "����");
                return;
                //if (MessageBox.Show("�����ϴ�����־����1�죬����������ظ����Ƿ�����ϴ���", "����", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                //{
                //    return;
                //}
            }
            alUpload = new System.Collections.ArrayList();
            FS.HISFC.Models.HealthRecord.Base b = new FS.HISFC.Models.HealthRecord.Base(); 
            switch (Type)
            {
                case "tWorkLog":
                    for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                    {
                        b = new FS.HISFC.Models.HealthRecord.Base();
                        b.PatientInfo.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.Cells[i, 0].Text);//����
                        foreach(FS.FrameWork.Models.NeuObject obj in deptList)
                        {
                            if (obj.Memo == this.neuSpread1_Sheet1.Cells[i, 1].Text.Trim())
                            {
                                b.InDept.ID = obj.ID;//ͳһ�ƺ�
                                b.InDept.Name = obj.Name;//��������
                                break;
                            }
                        }
                        foreach (FS.FrameWork.Models.NeuObject info in docList)
                        {
                            if (info.Memo == this.neuSpread1_Sheet1.Cells[i, 3].Text.Trim())
                            {
                                b.PatientInfo.DoctorReceiver.ID = info.ID;//ͳһҽ������
                                b.PatientInfo.DoctorReceiver.Name = info.Name;//ҽ������
                                break;
                            }
                        }
                        b.PatientInfo.DoctorReceiver.Memo = this.neuSpread1_Sheet1.Cells[i, 5].Text;//ҽ��ְ��
                        if (this.neuSpread1_Sheet1.Cells[i, 6].Text.Trim() == "")
                        {
                            b.OutDept.ID = "";//ͳһר�ƺ�
                            b.OutDept.Name = "";//ר�ƺ�
                        }
                        else
                        {
                            foreach (FS.FrameWork.Models.NeuObject objInfo in zkDeptList)
                            {
                                if (objInfo.Memo == this.neuSpread1_Sheet1.Cells[i, 6].Text.Trim())
                                {
                                    b.OutDept.ID = objInfo.ID;//ͳһר�ƺ�
                                    b.OutDept.Name = objInfo.Name;//ר�ƺ�
                                    break;
                                }
                            }
                        }
                        //b.OutDept.ID = "chengym";//ͳһר�ƺ� ������
                        b.PatientInfo.Memo = this.neuSpread1_Sheet1.Cells[i, 8].Text;//��ʱ
                        b.PatientInfo.User01 = this.neuSpread1_Sheet1.Cells[i, 9].Text;//�����˴�
                        b.PatientInfo.User02 = this.neuSpread1_Sheet1.Cells[i, 10].Text;//ר������
                        b.PatientInfo.User03 = this.neuSpread1_Sheet1.Cells[i, 11].Text;//ר������
                        b.PatientInfo.UserCode = this.neuSpread1_Sheet1.Cells[i, 12].Text;//�������
                        b.PatientInfo.WBCode = this.neuSpread1_Sheet1.Cells[i, 13].Text;//��������
                        b.PatientInfo.PVisit.User01 = this.neuSpread1_Sheet1.Cells[i, 14].Text;
                        b.PatientInfo.PVisit.User02 = this.neuSpread1_Sheet1.Cells[i, 15].Text;
                        b.PatientInfo.PVisit.OutTime = this.baseMgr.GetDateTimeFromSysDateTime();
                        alUpload.Add(b);
                    }
                    //MessageBox.Show("����ʵ��ɹ�tWorkLog");
                    break;
                case "tEmergeLog":
                    for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                    {
                        b = new FS.HISFC.Models.HealthRecord.Base();
                        b.PatientInfo.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.Cells[i, 0].Text);//����
                        b.InDept.ID = this.neuSpread1_Sheet1.Cells[i, 1].Text;//ͳһ�ƺ�
                        b.InDept.Name = this.neuSpread1_Sheet1.Cells[i, 2].Text;//��������
                        b.PatientInfo.ID = this.neuSpread1_Sheet1.Cells[i, 3].Text;//ҽ������
                        //b.PatientInfo.ID = "100000"; //������
                        b.PatientInfo.Memo = this.neuSpread1_Sheet1.Cells[i, 4].Text;//��ʱ
                        b.PatientInfo.User01 = this.neuSpread1_Sheet1.Cells[i, 5].Text;//�����˴�
                        b.PatientInfo.User02 = this.neuSpread1_Sheet1.Cells[i, 6].Text;//��������
                        b.PatientInfo.User03 = this.neuSpread1_Sheet1.Cells[i, 7].Text;//��������
                        b.PatientInfo.UserCode = this.neuSpread1_Sheet1.Cells[i, 8].Text;//���ȳɹ�����
                        b.PatientInfo.WBCode = this.neuSpread1_Sheet1.Cells[i, 9].Text;//��������
                        b.PatientInfo.PVisit.User01 = this.neuSpread1_Sheet1.Cells[i, 10].Text;//��������
                        b.PatientInfo.PVisit.OutTime = this.baseMgr.GetDateTimeFromSysDateTime();//��������
                        alUpload.Add(b);
                    }
                    //MessageBox.Show("����ʵ��ɹ�tEmergeLogNoKs");
                    break;
                case "tSpecialLog":
                    for (int i = 0; i < this.neuSpread1_Sheet1.RowCount;i++ )
                    {
                        b = new FS.HISFC.Models.HealthRecord.Base();

                        b.PatientInfo.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.Cells[i, 0].Text);//����
                        foreach (FS.FrameWork.Models.NeuObject obj in deptList)
                        {
                            if (obj.Memo == this.neuSpread1_Sheet1.Cells[i, 1].Text.Trim())
                            {
                                b.InDept.ID = obj.ID;//ͳһ�ƺ�
                                b.InDept.Name = obj.Name;//��������
                                break;
                            }
                        }
                        if (this.neuSpread1_Sheet1.Cells[i, 3].Text.Trim() == "")
                        {
                            b.OutDept.ID = "";//ͳһר�ƺ�
                            b.OutDept.Name = "";//ר�ƺ�
                        }
                        else
                        {
                            foreach (FS.FrameWork.Models.NeuObject objInfo in zkDeptList)
                            {
                                if (objInfo.Memo == this.neuSpread1_Sheet1.Cells[i, 3].Text.Trim())
                                {
                                    b.OutDept.ID = objInfo.ID;//ͳһר�ƺ�
                                    b.OutDept.Name = objInfo.Name;//ר�ƺ�
                                    break;
                                }
                            }
                        }
                        //b.OutDept.ID = "chengym";//ͳһר�ƺ� ������
                        b.PatientInfo.ID = this.neuSpread1_Sheet1.Cells[i, 5].Text;//��������
                        b.PatientInfo.PVisit.OutTime = this.baseMgr.GetDateTimeFromSysDateTime();//��������
                        alUpload.Add(b);
                    }
                    //MessageBox.Show( "����ʵ��ɹ�tSpecialLog" );
                    break;
                case "TZyWardWorklog":
                    FS.HISFC.Models.HealthRecord.Case.PatientMove patientMove = null;
                    for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                    {
                        patientMove = new FS.HISFC.Models.HealthRecord.Case.PatientMove();
                        patientMove.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.Cells[i, 0].Text);
                        patientMove.DeptCode = this.neuSpread1_Sheet1.Cells[i, 1].Text;
                        patientMove.OperDept = this.neuSpread1_Sheet1.Cells[i, 2].Text;
                        patientMove.BedNum = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 3].Text);

                        patientMove.OriginalNum = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 5].Text);
                        patientMove.InNum = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 6].Text);
                        patientMove.OtherDeptIn = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 7].Text);
                        patientMove.OtherRegionIn = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 8].Text);
                        patientMove.OutNum = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 9].Text);
                        patientMove.DeadNum = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 10].Text);
                        patientMove.ToOtherDept = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 11].Text);
                        patientMove.ToOtherRegion = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 12].Text);
                        patientMove.PatientNum = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 13].Text);
                        patientMove.AccompanyNum = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 14].Text);
                        patientMove.BeduseNum = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 15].Text);
                        patientMove.Memo = this.neuSpread1_Sheet1.Cells[i, 16].Text;
                        alUpload.Add(patientMove);
                    }
                    break;
                default:
                    break;
            }

            try
            {
                upLoadInterFace = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface();
            }
            catch (Exception ex)
            {
                MessageBox.Show("����ʧ��!" + ex.Message);
                //return -1;
            }
            int deleteNum = 0;
            int intReturn=0;
            bool isExist = false;
            try
            {
                switch (Type)
                {
                    case "tWorkLog":
                        intReturn = upLoadInterFace.InserttWorkLog(alUpload, begin.ToShortDateString(), end.ToShortDateString(), ref isExist);
                        break;
                    case "tEmergeLog":
                        intReturn = upLoadInterFace.InserttEmergeLog(alUpload, begin.ToShortDateString(), end.ToShortDateString(), ref isExist);
                        break;
                    case "tSpecialLog":
                        intReturn = upLoadInterFace.InserttSpecialLog(alUpload, begin.ToShortDateString(), end.ToShortDateString(), ref isExist);
                        break;
                    case "TZyWardWorklog":
                        intReturn = upLoadInterFace.InsertTZyWardWorklog(alUpload, begin.ToShortDateString(), end.ToShortDateString(), ref deleteNum);
                        break;
                }
                if (intReturn == -1)
                {
                    upLoadInterFace.Rollback();
                    MessageBox.Show(upLoadInterFace.Err + ",�ϴ�����");
                    return ;
                }
                else
                {
                    if (isExist)
                    {
                        if (DialogResult.No == MessageBox.Show("�Ѿ�����ؼ�¼��Ҫ������", "����",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1))
                        {
                            upLoadInterFace.Rollback();
                            MessageBox.Show("ȡ���ϴ�");
                            return ;
                        }
                    }
                    upLoadInterFace.Commit();
                }

                //if (intReturn<upLoadInterFace.Count )
                //{
                //    upLoadInterFace.Rollback();
                //}
                //else
                //{
                //    upLoadInterFace.Commit();
                //    
                //}
                MessageBox.Show("�ϴ��ɹ�" + intReturn.ToString() + "����¼��");
                //return intReturn;
            }
            catch (Exception ex)
            {
                upLoadInterFace.Rollback();
                MessageBox.Show(ex.Message);
                //return -1;
            }
            //MessageBox.Show("haolema?"+intReturn.ToString());
        }        

    }
}
