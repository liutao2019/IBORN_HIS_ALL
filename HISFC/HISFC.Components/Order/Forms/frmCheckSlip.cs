using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Order.Forms
{
    public partial class frmCheckSlip : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmCheckSlip()
        { 
            InitializeComponent();
        }
        public delegate void EventHandler(FS.HISFC.Models.Order.CheckSlip checkslip);
        public event EventHandler handler;
        private FS.HISFC.Models.Order.Inpatient.Order order = new FS.HISFC.Models.Order.Inpatient.Order();
        private FS.HISFC.Models.RADT.PatientInfo myPatientInfo = new FS.HISFC.Models.RADT.PatientInfo();
        private FS.FrameWork.Public.ObjectHelper areaHelper = new FS.FrameWork.Public.ObjectHelper();
        private FS.HISFC.Models.Order.CheckSlip checkslip = null;
        private FS.HISFC.BizLogic.Order.CheckSlip checkSlip = new FS.HISFC.BizLogic.Order.CheckSlip();

        ArrayList alChoose_zs = new ArrayList();//ת���������
        ArrayList alChoose_yxtz = new ArrayList();
        ArrayList alChoose_yxsy = new ArrayList();

        FS.FrameWork.Models.NeuObject obj = null;//���ص�ʵ�� 

        public FS.HISFC.Models.RADT.PatientInfo MyPatientInfo
        {
            get { return myPatientInfo; }
            set { myPatientInfo = value; }
        }
        public FS.HISFC.Models.Order.Inpatient.Order Order
        {
            get { return order; }
            set { order = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            Init();
            base.OnLoad(e);
        }

        protected void Init()
        {
            
            this.neuSex.Text = "";
            this.neuCardNo.Visible = false;
            this.neuLabel10.Visible = false;

            this.alChoose_zs = CacheManager.GetConList("ZS");
            this.alChoose_yxtz = CacheManager.GetConList("YXTZ");
            this.alChoose_yxsy = CacheManager.GetConList("YXSY");
            List<FS.HISFC.Models.Order.CheckSlip> patientList = new List<FS.HISFC.Models.Order.CheckSlip>();
            patientList = this.checkSlip.QueryPatineInfo(this.Order.Patient.ID.ToString());
            if (patientList.Count != 0)
            {
                this.neuPatientDept.Text = patientList[0].ExtFlag2.ToString();
                this.neuNurseCellName.Text = patientList[0].ExtFlag3.ToString();
                this.neuBedNo.Text = patientList[0].ExtFlag4.ToString();
            }
            this.neuSex.Text = this.MyPatientInfo.Sex.Name.ToString();
            this.neuAge.Text = this.MyPatientInfo.Age.ToString();
            this.neuInpatientNo.Text = this.Order.Patient.ID.ToString();
            this.neuName.Text = this.MyPatientInfo.Name.ToString();
            
            this.tbItem.Text = this.Order.Item.Name.ToString();
            this.neuDept.Text = this.Order.ReciptDept.Name.ToString();
            this.neuDoct.Text = this.Order.ReciptDoctor.Name.ToString();
            if (order.IsEmergency == true)
            {
                this.neuLabel10.Text = "�Ӽ�";
                this.neuLabel10.Visible = true;
            }
            //if (((FS.FrameWork.Models.NeuObject)(order)).ID == "")
            //{
            //    if (recientSlip(this.Order) == null)
            //    {
            //        this.neuAge.Text = this.MyPatientInfo.Age.ToString();
            //        this.neuSex.Text = this.MyPatientInfo.Sex.Name.ToString();
            //        //this.neuName.Text = this.Order.Patient.Name.ToString();
            //        this.neuName.Text = this.MyPatientInfo.Name.ToString();
            //        this.neuInpatientNo.Text = this.Order.Patient.ID.ToString();
            //        this.neuDept.Text = this.Order.ReciptDept.Name.ToString();
            //       // this.neuNurseCellName.Text = allDept.GetDeptFromNurseStation();
            //        this.neuItemNote.Text = this.Order.CheckPartRecord.ToString();
            //        this.tbItem.Text = this.Order.Item.Name.ToString();
            //        this.tbDignose.Text = this.checkSlip.QueryDiagName(this.Order.Patient.ID.ToString());                  
            //        this.neuDoct.Text = this.Order.ReciptDoctor.Name.ToString();
            //    }
                
            //}
            //else
            //{
                    List<FS.HISFC.Models.Order.CheckSlip> list = new List<FS.HISFC.Models.Order.CheckSlip>();
                    list = this.checkSlip.QuerySlip(this.checkSlip.QueryByMoOrder(((FS.FrameWork.Models.NeuObject)(order)).ID).ToString());
                    if (list.Count != 0)
                    {
                        //this.neuSex.Text = this.MyPatientInfo.Sex.Name.ToString();
                        //this.neuAge.Text = this.MyPatientInfo.Age.ToString();
                        //this.neuInpatientNo.Text = this.Order.Patient.ID.ToString();
                        //this.neuName.Text = this.Order.Patient.Name.ToString();
                        //this.neuName.Text = this.MyPatientInfo.Name.ToString();
                        this.neuDept.Text = areaHelper.GetName(list[0].Doct_dept.ToString());

                        this.neuItemNote.Text = list[0].ItemNote.ToString();
                        this.tbItem.Text = this.Order.Item.Name.ToString();
                        this.txtMain.Text = list[0].ZsInfo.ToString();
                        this.neuRichTextBox1.Text = list[0].YxtzInfo.ToString();
                        this.neuRichTextBox2.Text = list[0].YxsyInfo.ToString();
                        this.tbDignose.Text = list[0].DiagName.ToString();
                        this.tbNote.Text = list[0].Memo.ToString();
                    }
                    else
                    {
                        list.Clear();
                        list = this.checkSlip.QuerySlip(this.Order.ApplyNo);
                        if (list.Count != 0)
                        {
                            this.neuDept.Text = areaHelper.GetName(list[0].Doct_dept.ToString());

                            this.neuItemNote.Text = list[0].ItemNote.ToString();
                            this.tbItem.Text = this.Order.Item.Name.ToString();
                            this.txtMain.Text = list[0].ZsInfo.ToString();
                            this.neuRichTextBox1.Text = list[0].YxtzInfo.ToString();
                            this.neuRichTextBox2.Text = list[0].YxsyInfo.ToString();
                            this.tbDignose.Text = list[0].DiagName.ToString();
                            this.tbNote.Text = list[0].Memo.ToString();
                        }
                        else if (recientSlip(this.Order) == null)                        
                        {
                            
                        }
                    }
                }
            //}                

        protected List<FS.HISFC.Models.Order.CheckSlip> recientSlip(FS.HISFC.Models.Order.Order order)
        {
            List<FS.HISFC.Models.Order.CheckSlip> list = new List<FS.HISFC.Models.Order.CheckSlip>();
            list = this.checkSlip.QueryRecientSlip(order.Patient.ID.ToString());
            if (list.Count != 0)
            {
                //this.neuSex.Text = this.MyPatientInfo.Sex.Name.ToString();
                //this.neuAge.Text = this.MyPatientInfo.Age.ToString();
                //this.neuInpatientNo.Text = Order.Patient.ID.ToString();
                this.neuDept.Text = areaHelper.GetName(list[0].Doct_dept.ToString());
                //this.neuName.Text = this.Order.Patient.Name.ToString();
                this.neuName.Text = this.MyPatientInfo.Name.ToString();
               
                this.neuItemNote.Text = list[0].ItemNote.ToString();
                this.tbItem.Text = this.Order.Item.Name.ToString();
                this.txtMain.Text = list[0].ZsInfo.ToString();
                this.neuRichTextBox1.Text = list[0].YxtzInfo.ToString();
                this.neuRichTextBox2.Text = list[0].YxsyInfo.ToString();
                this.tbDignose.Text = list[0].DiagName.ToString();
                this.tbNote.Text = list[0].Memo.ToString();
            }
            else
            {
                return null;
            }
            return list;
        }
       

        protected FS.HISFC.Models.Order.CheckSlip getSlip()
        {
            this.checkslip = new FS.HISFC.Models.Order.CheckSlip();
            int i = this.checkSlip.QueryByMoOrder(((FS.FrameWork.Models.NeuObject)(order)).ID);
            if (i != -1 && i != 0)
            {
                this.getSlip();
                this.checkslip.CheckSlipNo = i.ToString();
            }            
            this.checkslip.InpatientNO = this.neuInpatientNo.Text;
            this.checkslip.Doct_dept = this.Order.ReciptDept.ID.ToString();
            this.checkslip.ZsInfo = this.txtMain.Text;
            this.checkslip.YxtzInfo = this.neuRichTextBox1.Text;
            this.checkslip.YxsyInfo = this.neuRichTextBox2.Text;
            this.checkslip.ItemNote = this.neuItemNote.Text;
            this.checkslip.DiagName = this.tbDignose.Text;
            this.checkslip.ExtFlag1 = this.tbItem.Text;
            this.checkslip.MoNote = this.tbNote.Text;
            this.checkslip.ApplyDate = this.checkSlip.GetDateTimeFromSysDateTime();
            this.checkslip.OperDate = this.checkSlip.GetDateTimeFromSysDateTime();                        
            return this.checkslip;
        }

        private void neuButton2_Click(object sender, EventArgs e)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.PrintPreview(0, 0, this.neuPanel1);
        }

        private void txtMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space)
            {
                return;
            }

            obj = new FS.FrameWork.Models.NeuObject();

            FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.alChoose_zs, ref obj);
            if (obj == null || obj.ID == "")
            {
                return;
            }

            this.txtMain.AppendText(obj.Name + "  ");
            this.txtMain.SelectionStart = this.txtMain.Text.Length;
            this.txtMain.ScrollToCaret();
            this.txtMain.Focus();
        }

        private void neuRichTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space)
            {
                return;
            }

            obj = new FS.FrameWork.Models.NeuObject();

            FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.alChoose_yxtz, ref obj);
            if (obj == null || obj.ID == "")
            {
                return;
            }

            this.neuRichTextBox1.AppendText(obj.Name + "  ");
            this.neuRichTextBox1.SelectionStart = this.txtMain.Text.Length;
            this.neuRichTextBox1.ScrollToCaret();
            this.neuRichTextBox1.Focus();
        }

        private void neuRichTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space)
            {
                return;
            }

            obj = new FS.FrameWork.Models.NeuObject();

            FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.alChoose_yxsy, ref obj);
            if (obj == null || obj.ID == "")
            {
                return;
            }

            this.neuRichTextBox2.AppendText(obj.Name + "  ");
            this.neuRichTextBox2.SelectionStart = this.txtMain.Text.Length;
            this.neuRichTextBox2.ScrollToCaret();
            this.neuRichTextBox2.Focus();
        }

        private void neuButton1_Click_1(object sender, EventArgs e)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            checkSlip.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            getSlip();
            if (this.checkslip.CheckSlipNo == null)
            {
                if (checkSlip.InsertCheckSlip(this.checkslip) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("������뵥����ʧ��");
                    return;
                    this.Close();
                }
            }
            else
            {
                if (checkSlip.UpdateCheckSlip(this.checkslip) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("������뵥����ʧ��");
                    return;
                    this.Close();
                }
            }
            MessageBox.Show("������뵥����ɹ�");
            FS.FrameWork.Management.PublicTrans.Commit();
            EventHandler eventhandler = handler;
            if (eventhandler != null)
            {
                eventhandler(this.checkslip);
            }
            this.Close();
        }

    }
}
