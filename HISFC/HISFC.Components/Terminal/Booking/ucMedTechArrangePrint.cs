using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.Terminal.Booking
{
    public partial class ucMedTechArrangePrint : System.Windows.Forms.UserControl, HISFC.Components.Terminal.Booking.IBookingPrint
    {
        public ucMedTechArrangePrint()
        {
            InitializeComponent();
        }
        private enum Cols
        {
            /// <summary>
            /// ��Ŀ����
            /// </summary>
            Name,
            /// <summary>
            /// ϵͳ���
            /// </summary>
            SysClass,
            /// <summary>
            /// ����
            /// </summary>
            Leixing,
            /// <summary>
            /// ִ�еص�
            /// </summary>
            ExeDept,
            /// <summary>
            /// �������
            /// </summary>
            CheckDate
        }

        #region IControlPrintable ��Ա

        //public int BeginHorizontalBlankWidth
        //{
        //    get
        //    {
        //        throw new Exception("The method or operation is not implemented.");
        //    }
        //    set
        //    {
        //        throw new Exception("The method or operation is not implemented.");
        //    }
        //}

        //public int BeginVerticalBlankHeight
        //{
        //    get
        //    {
        //        throw new Exception("The method or operation is not implemented.");
        //    }
        //    set
        //    {
        //        throw new Exception("The method or operation is not implemented.");
        //    }
        //}

        //public ArrayList Components
        //{
        //    get
        //    {
        //        throw new Exception("The method or operation is not implemented.");
        //    }
        //    set
        //    {
        //        throw new Exception("The method or operation is not implemented.");
        //    }
        //}

        //public Size ControlSize
        //{
        //    get { throw new Exception("The method or operation is not implemented."); }
        //}

        //public object ControlValue
        //{
        //    get
        //    {
        //        throw new Exception("The method or operation is not implemented.");
        //    }
        //    set
        //    {
        //        FS.FrameWork.Public.ObjectHelper NoonListHelper = new FS.FrameWork.Public.ObjectHelper();
        //        FS.HISFC.BizProcess.Integrate.Manager managerMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        //        ArrayList NoonList = managerMgr.GetConstantList("NOON");
        //        NoonListHelper.ArrayObject = NoonList;
        //        //FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();
        //        FS.HISFC.BizProcess.Integrate.Fee undrugztMgr = new FS.HISFC.BizProcess.Integrate.Fee();
        //        //FS.HISFC.BizLogic.Manager.Person ps = new FS.HISFC.BizLogic.Manager.Person();
        //        //FS.HISFC.Models.Base.Employee dd = managerMgr.GetPersonByID(terminalMgr.Operator.ID);
        //        //FS.HISFC.Models.Base.Department dep = managerMgr.GetDeptmentById(dd.Dept.ID);
        //        FS.HISFC.Models.Terminal.MedTechBookApply objRegister = value as FS.HISFC.Models.Terminal.MedTechBookApply;
        //        label4.Text = objRegister.MedTechBookInfo.BookID;// ԤԼ����
        //        label8.Text = objRegister.ItemList.Name; //����
        //        label10.Text = objRegister.MedTechBookInfo.BookTime.Year + "��" + objRegister.MedTechBookInfo.BookTime.Month + "��" + objRegister.MedTechBookInfo.BookTime.Day + "��" + NoonListHelper.GetName(objRegister.Noon.ID); //ִ��ʱ��
        //        label12.Text = objRegister.ItemList.ExecOper.Dept.Name;//ִ�еص� 
        //        label18.Text = "��Ŀ:" + objRegister.ItemList.Item.Name.PadLeft(20, ' '); //�����Ŀ

        //        #region  ��ѯ��Ϣ��ע������
        //        FS.HISFC.Models.Fee.Item.Undrug itemObj = undrugztMgr.GetUndrugByCode(objRegister.ItemList.Item.ID);
        //        if (itemObj != null && itemObj.Notice != "")
        //        {
        //            objRegister.Memo = itemObj.Notice;
        //        }

        //        #endregion

        //        richTextBox1.Text = objRegister.Memo;
        //        label11.Text = objRegister.ItemList.Patient.PID.CardNO;
        //    }
        //}

        //public int HorizontalBlankWidth
        //{
        //    get
        //    {
        //        throw new Exception("The method or operation is not implemented.");
        //    }
        //    set
        //    {
        //        throw new Exception("The method or operation is not implemented.");
        //    }
        //}

        //public int HorizontalNum
        //{
        //    get
        //    {
        //        throw new Exception("The method or operation is not implemented.");
        //    }
        //    set
        //    {
        //        throw new Exception("The method or operation is not implemented.");
        //    }
        //}

        //public bool IsCanExtend
        //{
        //    get
        //    {
        //        throw new Exception("The method or operation is not implemented.");
        //    }
        //    set
        //    {
        //        throw new Exception("The method or operation is not implemented.");
        //    }
        //}

        //public bool IsShowGrid
        //{
        //    get
        //    {
        //        throw new Exception("The method or operation is not implemented.");
        //    }
        //    set
        //    {
        //        throw new Exception("The method or operation is not implemented.");
        //    }
        //}

        //public int VerticalBlankHeight
        //{
        //    get
        //    {
        //        throw new Exception("The method or operation is not implemented.");
        //    }
        //    set
        //    {
        //        throw new Exception("The method or operation is not implemented.");
        //    }
        //}

        //public int VerticalNum
        //{
        //    get
        //    {
        //        throw new Exception("The method or operation is not implemented.");
        //    }
        //    set
        //    {
        //        throw new Exception("The method or operation is not implemented.");
        //    }
        //}

        #endregion

        #region IBookingPring ��Ա

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        void IBookingPrint.SetValue(FS.HISFC.Models.Terminal.MedTechBookApply obj)
        {
            FS.FrameWork.Public.ObjectHelper NoonListHelper = new FS.FrameWork.Public.ObjectHelper();
            FS.HISFC.BizProcess.Integrate.Manager managerMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            ArrayList NoonList = managerMgr.GetConstantList("NOON");
            NoonListHelper.ArrayObject = NoonList; 
            FS.HISFC.BizProcess.Integrate.Fee undrugztMgr = new FS.HISFC.BizProcess.Integrate.Fee();
            FS.HISFC.Models.Terminal.MedTechBookApply objRegister = obj as FS.HISFC.Models.Terminal.MedTechBookApply;
            label4.Text = objRegister.MedTechBookInfo.BookID;// ԤԼ����
            label8.Text = objRegister.ItemList.Patient.Name; //����
            label10.Text = objRegister.MedTechBookInfo.BookTime.Year + "��" + objRegister.MedTechBookInfo.BookTime.Month + "��" + objRegister.MedTechBookInfo.BookTime.Day + "��" + NoonListHelper.GetName(objRegister.Noon.ID); //ִ��ʱ��
            label12.Text = objRegister.ItemList.ExecOper.Dept.Name;//ִ�еص� 
            label18.Text = "��Ŀ:" + objRegister.ItemList.Item.Name.PadLeft(20, ' '); //�����Ŀ

            #region  ��ѯ��Ϣ��ע������
            FS.HISFC.Models.Fee.Item.Undrug itemObj = undrugztMgr.GetUndrugByCode(objRegister.ItemList.Item.ID);
            if (itemObj != null && itemObj.Notice != "")
            {
                objRegister.Memo = itemObj.Notice;
            }

            #endregion

            richTextBox1.Text = objRegister.Memo;
            label11.Text = objRegister.ItemList.Patient.PID.CardNO;
        }
        /// <summary>
        /// ���
        /// </summary>
        void IBookingPrint.Reset()
        {
            label8.Text = "";
            label10.Text = "";
            label12.Text = "";
            richTextBox1.Text = "";
        }

        #endregion

        #region IReportPrinter ��Ա

        int FS.FrameWork.WinForms.Forms.IReportPrinter.Export()
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            return p.PrintPage(20, 10, this);  
        }

        int FS.FrameWork.WinForms.Forms.IReportPrinter.Print()
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            return p.PrintPage(20, 10, this);    
        }

        int FS.FrameWork.WinForms.Forms.IReportPrinter.PrintPreview()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion 
 
    }
}
