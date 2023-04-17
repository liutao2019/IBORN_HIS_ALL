using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Neusoft.HISFC.Components.Nurse.Print
{
    public class PrintCure : Neusoft.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint
    {
        #region IInjectCurePrint 成员

        /// <summary>
        /// 医生帮助类
        /// </summary>
        private Neusoft.FrameWork.Public.ObjectHelper doctHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 科室帮助类
        /// </summary>
        private Neusoft.FrameWork.Public.ObjectHelper deptHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 综合业务层
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Manager inteManager = new Neusoft.HISFC.BizProcess.Integrate.Manager();



        public void Init(System.Collections.ArrayList alPrintData)
        {
            ArrayList al = this.inteManager.QueryEmployee(Neusoft.HISFC.Models.Base.EnumEmployeeType.D);
            if (al == null)
            {
                MessageBox.Show("查询医生列表出错！\r\n" + inteManager.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.doctHelper.ArrayObject = al;

            al = this.inteManager.GetDepartment();
            if (al == null)
            {
                MessageBox.Show("查询科室列表出错！\r\n" + inteManager.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.deptHelper.ArrayObject = al;

            //用来将要分开打的数据分开
            Hashtable htInject = new Hashtable();
            foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList detail in (alPrintData[1] as ArrayList))
            {
                Neusoft.HISFC.Models.Registration.Register reg = alPrintData[0] as Neusoft.HISFC.Models.Registration.Register;
                Neusoft.HISFC.Models.Nurse.Inject inject = this.ChangeInject(reg, detail);

                string key = inject.PrintNo;
                if (htInject.ContainsKey(key))
                {
                    List<Neusoft.HISFC.Models.Nurse.Inject> injectList = htInject[key] as List<Neusoft.HISFC.Models.Nurse.Inject>;
                    injectList.Add(inject);
                }
                else
                {
                    List<Neusoft.HISFC.Models.Nurse.Inject> injectList = new List<Neusoft.HISFC.Models.Nurse.Inject>();
                    htInject.Add(key, injectList);
                    injectList.Add(inject);
                }
            }
            //分别打印
            int pageCount = 1;
            int totPageCount = htInject.Count;
            //打印
            //Neusoft.FrameWork.WinForms.Classes.Print p = new Neusoft.FrameWork.WinForms.Classes.Print();
            foreach (string key in htInject.Keys)
            {
                ucPrintCureNewControl ucPrint = new ucPrintCureNewControl();
                ucPrint.SetData(htInject[key] as List<Neusoft.HISFC.Models.Nurse.Inject>, totPageCount, pageCount);
                pageCount++;
                //p.PrintPage(12, 1, ucPrint);
            }
        }

        private Neusoft.HISFC.Models.Nurse.Inject ChangeInject(Neusoft.HISFC.Models.Registration.Register reg, Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList detail)
        {
            #region 实体转化（门诊项目收费明细实体FeeItemList－->注射实体Inject）
            Neusoft.HISFC.Models.Nurse.Inject info = new Neusoft.HISFC.Models.Nurse.Inject();
            info.Patient = reg;
            info.Patient.ID = detail.Patient.ID;
            info.Patient.Name = reg.Name;
            info.Patient.Sex.ID = reg.Sex.ID;
            info.Patient.Birthday = reg.Birthday;
            info.Patient.PID.CardNO = reg.PID.CardNO;

            info.Item = detail;
            info.Item.ID = detail.Item.ID;
            info.Item.Name = detail.Item.Name;

            info.Item.InjectCount = detail.InjectCount;

            info.Item.Order.DoctorDept.Name = deptHelper.GetName(detail.RecipeOper.Dept.ID);
            info.Item.Order.DoctorDept.ID = detail.RecipeOper.Dept.ID;

            info.Item.Order.Doctor.Name = doctHelper.GetName(detail.RecipeOper.ID);
            info.Item.Order.Doctor.ID = detail.RecipeOper.ID;
            #endregion

            info.PrintNo = detail.User02;

            info.Booker.ID = Neusoft.FrameWork.Management.Connection.Operator.ID;

            info.Item.ExecOper.ID = ((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).Dept.ID;

            string strOrder = "";
            info.InjectOrder = strOrder;
            info.Item.Days = detail.Days;
            return info;
        }

        #endregion
    }
}
