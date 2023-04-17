using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.Local.Nurse.ZhuHai.ZDWY.IInjectBottleLabel
{
    public class PrintBottleLabel : FS.SOC.HISFC.BizProcess.NurseInterface.OutPatient.IInjectBottleLabel
    {
        /// <summary>
        /// 综合业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager inteManager = new FS.HISFC.BizProcess.Integrate.Manager();

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

            info.Item.Order.DoctorDept.Name = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(detail.RecipeOper.Dept.ID);
            info.Item.Order.DoctorDept.ID = detail.RecipeOper.Dept.ID;

            info.Item.Order.Doctor.Name = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(detail.RecipeOper.ID);
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

        #region IInjectBottleLabel 成员

        bool isReprint = false;

        public bool IsReprint
        {
            set
            {
                isReprint = value;
            }
        }

        public int Print(ArrayList alPrintData)
        {
            //用来将要分开打的数据分开
            Hashtable htInject = new Hashtable();

            if (alPrintData.Count > 0)
            {
                if (alPrintData[0] is FS.HISFC.Models.Nurse.Inject)
                {
                    foreach (FS.HISFC.Models.Nurse.Inject detail in alPrintData)
                    {
                        FS.HISFC.Models.Registration.Register reg = regInfo;

                        //FS.HISFC.Models.Nurse.Inject inject = this.ChangeInject(reg, detail);
                        FS.HISFC.Models.Nurse.Inject inject = detail;

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

                }

                else if (alPrintData[0] is FS.HISFC.Models.Fee.Outpatient.FeeItemList)
                {
                    foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList detail in alPrintData)
                    {
                        FS.HISFC.Models.Registration.Register reg = regInfo;
                        FS.HISFC.Models.Nurse.Inject inject = this.ChangeInject(reg, detail);

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
                }
                else
                {
                    MessageBox.Show("类型匹配出错！\r\n" + inteManager.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            //分别打印
            int pageCount = 1;
            int totPageCount = htInject.Count;
            //打印
            //FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            foreach (string key in htInject.Keys)
            {
                ucPrintBottleLabel ucPrint = new ucPrintBottleLabel();
                ucPrint.IsReprint = isReprint;
                ucPrint.SetData(htInject[key] as List<FS.HISFC.Models.Nurse.Inject>, totPageCount, pageCount);
                pageCount++;
                //p.PrintPage(12, 1, ucPrint);
            }
            return 1;
        }

        FS.HISFC.Models.Registration.Register regInfo = null;

        public FS.HISFC.Models.Registration.Register RegInfo
        {
            set
            {
                regInfo = value;
            }
        }

        #endregion
    }
}
