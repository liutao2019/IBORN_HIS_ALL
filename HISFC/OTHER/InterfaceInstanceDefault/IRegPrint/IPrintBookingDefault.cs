using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace InterfaceInstanceDefault.IRegPrint
{
    public partial class IPrintBookingDefault :FS.FrameWork.WinForms.Controls.ucBaseControl,  FS.HISFC.BizProcess.Interface.Registration.IBookingRegisterBill
    {
        public IPrintBookingDefault()
        {
            InitializeComponent();
        }

        #region 变量
        /// <summary>
        /// 性别帮助类
        /// </summary>
        FS.FrameWork.Public.ObjectHelper sexHelper = new FS.FrameWork.Public.ObjectHelper();
        #endregion

        #region IBookingRegisterBill 成员

        public int Print(FS.HISFC.Models.Registration.Booking booking)
        {
            this.lblBirthDay.Text = booking.Birthday.ToShortDateString();
            this.lblDept.Text = booking.DoctorInfo.Templet.Dept.Name;
            this.lblDoct.Text = booking.DoctorInfo.Templet.Doct.Name;
            this.lblName.Text = booking.Name;
            this.lblSex.Text = this.sexHelper.GetName( booking.Sex.ID.ToString());
            this.lblOrder.Text = booking.ID;
            this.lblPredate.Text = booking.DoctorInfo.SeeDate.ToString();
            this.lblOper.Text = booking.Oper.ID;
            this.lblOperDateTime.Text = booking.Oper.OperTime.ToString();

            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.PrintPage(0, 0, this.pnPrint);
            return 1;
        }

        #endregion

        private int InitControl()
        {
            ArrayList alSex = FS.HISFC.Models.Base.SexEnumService.List();
            this.sexHelper.ArrayObject = alSex;
            return 1;
        }

        protected override void OnLoad(EventArgs e)
        {
            this.InitControl();
            base.OnLoad(e);
        }

    }
}
