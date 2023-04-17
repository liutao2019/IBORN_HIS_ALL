using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace FS.SOC.Local.OutpatientFee.ShenZhen.BinHai.IMultiScreen
{
    public class MultiScreen:FS.HISFC.BizProcess.Interface.FeeInterface.IMultiScreen
    {
        /// <summary>
        /// 显示患者信息
        /// </summary>
        private void ShowPatientInfo(FS.HISFC.Models.Registration.Register register)
        {
            TDKJ_BJ_IV.ShowInfo("&C12姓名：" + register.Name + "$");
        }

        /// <summary>
        /// 显示应收金额
        /// </summary>
        /// <param name="ownCost"></param>
        private void ShowFTInfo(FS.HISFC.Models.Base.FT ft)
        {
            TDKJ_BJ_IV.ShowInfo("&C22应收：" + (ft.RealCost - ft.ReturnCost).ToString("F2") + "$");//ft.OwnCost

            TDKJ_BJ_IV.ShowInfo("&C32预收：" + ft.RealCost.ToString("F2") + "$");

            TDKJ_BJ_IV.ShowInfo("&C42找零：" + ft.ReturnCost.ToString("F2") + "$");

            TDKJ_BJ_IV.ShowInfo("&C52发药窗口：" + ft.User01 + "$");
        }

        /// <summary>
        /// 发音
        /// </summary>
        /// <param name="ownCost"></param>
        private void SoundFTInfo(FS.HISFC.Models.Base.FT ft)
        {
            //string j = (ft.RealCost - ft.ReturnCost).ToString("F2") + "J";
            //TDKJ_BJ_IV.SayMoney(j);

            string y = ft.RealCost.ToString("F2") + "Y";
            TDKJ_BJ_IV.SayMoney(y);

            string z = ft.ReturnCost.ToString("F2") + "Z";
            TDKJ_BJ_IV.SayMoney(z);
        }

        /// <summary>
        /// 播报请您付款XXX元，并在屏幕上显示
        /// </summary>
        private void SoundFKFtInfo(FS.HISFC.Models.Base.FT ft)
        {
            string j = (ft.RealCost - ft.ReturnCost).ToString("F2") + "J";
            TDKJ_BJ_IV.SayMoney(j);
        }

        /// <summary>
        /// 显示医院
        /// </summary>
        private void ShowHospital() 
        {
            TDKJ_BJ_IV.ShowHospital();
        }

        /// <summary>
        /// 显示欢迎信息
        /// </summary>
        private void ShowWelCome()
        {
            TDKJ_BJ_IV.ShowWelCome();
        }

        /// <summary>
        /// 清屏
        /// </summary>
        private void ShowClear()
        {
            TDKJ_BJ_IV.ShowInfo(TDKJ_BJ_IV.ClearScreen);
            //TDKJ_BJ_IV.ShowInfo("&C11姓名：$");
            //TDKJ_BJ_IV.ShowInfo("&C21应收：$");
            //TDKJ_BJ_IV.ShowInfo("&C31预收：$");
            //TDKJ_BJ_IV.ShowInfo("&C41找零：$");
            //TDKJ_BJ_IV.ShowInfo("&C51发药窗口：$");
        }

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="?"></param>
        private void ShowInfo(List<object> list)
        {
            if (list == null || list.Count == 0)
            {
                //this.ShowClear();
                return;
            }

            object o1 = list[0];

            //显示患者信息
            if (o1 is FS.HISFC.Models.Registration.Register)
            {
                //清屏
                this.ShowClear();
                FS.HISFC.Models.Registration.Register register = o1 as FS.HISFC.Models.Registration.Register;
                //有患者信息
                this.ShowPatientInfo(register);

                //金额信息
                object o2 = list[1];
                if (o2 is FS.HISFC.Models.Base.FT)
                {
                    FS.HISFC.Models.Base.FT ft = o2 as FS.HISFC.Models.Base.FT;
                    this.ShowFTInfo(ft);
                    if (list[4] != null)
                    {
                        object o5 = list[4];
                        string[] strr = o5 as string[];
                        // otherInfomations[0] = "1" 看诊医生科室变更
                        // otherInfomations[0] = "2" 合同单位变更
                        // otherInfomations[0] = "3" 收费前显示
                        // otherInfomations[0] = "4" 收费后清空
                        if (strr[0] == "4")//收费之后才进行叫号
                        {
                            if (ft.Memo == "Call")
                            {
                                if (ft.TotCost == 0)
                                {
                                    this.SoundFKFtInfo(ft);
                                }
                                else
                                {
                                    this.SoundFTInfo(ft);
                                }
                                ft.Memo = "";
                            }
                            else
                            {
                                if ((ft.RealCost - ft.ReturnCost) != 0)//医保的实付会为零，就不叫了
                                {
                                    this.SoundFTInfo(ft);
                                }
                            }
          
                        }
                        if (strr[0] == "3")//收费之前进行叫号
                        {
                            if (ft.Memo == "Call")//如果为Call 就是叫号
                            {
                                this.SoundFKFtInfo(ft);
                                ft.Memo = "";
                            }
                        }
                    }
                    
                }
            }
            else//患者信息都没有，直接清屏，显示欢迎信息
            {
                this.ShowClear();
                this.ShowHospital();
                this.ShowWelCome();
            }

        }

        #region IMultiScreen 成员

        public int CloseScreen()
        {
            return 1;
        }

        public List<object> ListInfo
        {
            get
            {
                return null;
            }
            set
            {
                this.ShowInfo(value);
            }
        }

        public int ShowScreen()
        {
            this.ShowWelCome();
            return 1;
        }

        #endregion

        
    }
}
