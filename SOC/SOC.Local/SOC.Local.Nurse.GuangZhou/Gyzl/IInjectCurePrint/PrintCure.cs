using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.Local.Nurse.GuangZhou.Gyzl.IInjectCurePrint
{
    public class PrintCure : FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint
    {
        #region IInjectCurePrint 成员

        /// <summary>
        /// 医生帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper doctHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 科室帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 综合业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager inteManager = new FS.HISFC.BizProcess.Integrate.Manager();

     

        public void Init(System.Collections.ArrayList alPrintData)
        {
            ArrayList al = this.inteManager.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
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
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList detail in (alPrintData[1] as ArrayList))
            {
                FS.HISFC.Models.Registration.Register reg = alPrintData[0] as FS.HISFC.Models.Registration.Register;
                FS.HISFC.Models.Nurse.Inject inject = this.ChangeInject(reg,detail); 

                string key = inject.PrintNo;
                if (htInject.ContainsKey(key))
                {
                    List<FS.HISFC.Models.Nurse.Inject> injectList = htInject[key] as List<FS.HISFC.Models.Nurse.Inject>;
                    injectList.Add(inject);
                }
                else
                {
                    List<FS.HISFC.Models.Nurse.Inject> injectList = new List<FS.HISFC.Models.Nurse.Inject>();
                    htInject.Add(key, injectList);
                    injectList.Add(inject);
                }
            }
            //分别打印
            int pageCount = 1;
            int totPageCount = htInject.Count;
            //打印
            //FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            foreach (string key in htInject.Keys)
            {
                ucPrintCureNewControl ucPrint = new ucPrintCureNewControl();
                ucPrint.SetData(htInject[key] as List<FS.HISFC.Models.Nurse.Inject>, totPageCount, pageCount);
                pageCount++;
                //p.PrintPage(12, 1, ucPrint);
            }
        }

        private FS.HISFC.Models.Nurse.Inject ChangeInject(FS.HISFC.Models.Registration.Register reg, FS.HISFC.Models.Fee.Outpatient.FeeItemList detail)
        {
            #region 实体转化（门诊项目收费明细实体FeeItemList－->注射实体Inject）
            FS.HISFC.Models.Nurse.Inject info = new FS.HISFC.Models.Nurse.Inject();
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
          
            info.Booker.ID = FS.FrameWork.Management.Connection.Operator.ID;

            info.Item.ExecOper.ID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;

            string strOrder = "";
            info.InjectOrder = strOrder;
            info.Item.Days = detail.Days;
            return info;
        }

        #endregion
    }
}
