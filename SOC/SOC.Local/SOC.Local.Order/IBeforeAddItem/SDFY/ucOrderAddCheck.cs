using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SOC.Local.Order.IBeforeAddItem.SDFY
{
    /// <summary>
    /// 妇幼开立项目前 禁用药提示
    /// </summary>
    public class ucOrderAddCheck : Neusoft.HISFC.BizProcess.Interface.Order.IBeforeAddItem
    {
        #region IBeforeAddItem 成员

        /// <summary>
        /// 错误信息
        /// </summary>
        string err = "";

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrInfo
        {
            get
            {
                return err;
            }
            set
            {
                err = value;
            }
        }

        /// <summary>
        /// 住院开立判断
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int OnBeforeAddItemForInPatient(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, ArrayList alOrder)
        {
            return this.CheckOrder(patientInfo, reciptDept, reciptDoct, alOrder, Neusoft.HISFC.Models.Base.ServiceTypes.I);
        }

        /// <summary>
        /// 存放查询出来的药品扩展信息列表
        /// </summary>
        Hashtable hsPhaItemExendInfoList = new Hashtable();

        /// <summary>
        /// 禁用药品列表
        /// </summary>
        Hashtable hsDisablePha = null;


        System.Windows.Forms.NotifyIcon notify = null;

        /// <summary>
        /// 门诊开立判断
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int OnBeforeAddItemForOutPatient(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, ArrayList alOrder)
        {
            return this.CheckOrder(regObj, reciptDept, reciptDoct, alOrder, Neusoft.HISFC.Models.Base.ServiceTypes.C);
        }

        #endregion

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int CheckOrder(Neusoft.HISFC.Models.RADT.Patient regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, ArrayList alOrder, Neusoft.HISFC.Models.Base.ServiceTypes type)
        {
            try
            {
                if (hsDisablePha == null)
                {
                    Neusoft.HISFC.BizProcess.Integrate.Manager interMgr = new Neusoft.HISFC.BizProcess.Integrate.Manager();
                    ArrayList al = interMgr.GetConstantList("DisablePha");
                    if (al != null)
                    {
                        hsDisablePha = new Hashtable();

                        foreach (Neusoft.HISFC.Models.Base.Const con in al)
                        {
                            if (!hsDisablePha.Contains(con.ID))
                            {
                                hsDisablePha.Add(con.ID, con);
                            }
                        }
                    }
                }

                if (hsDisablePha == null || hsDisablePha.Count == 0)
                {
                    return 1;
                }

                Neusoft.HISFC.Models.Pharmacy.Item phaItem = null;
                Neusoft.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Pharmacy();
                Neusoft.HISFC.Models.Base.Const conObj = null;

                Hashtable hsWarn = new Hashtable();

                foreach (Neusoft.HISFC.Models.Order.Order order in alOrder)
                {
                    if (order.Item.ItemType != Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        continue;
                    }

                    if (!hsPhaItemExendInfoList.Contains(order.Item.ID))
                    {
                        phaItem = phaIntegrate.GetItem(order.Item.ID);
                    }
                    else
                    {
                        phaItem = hsPhaItemExendInfoList[order.Item.ID] as Neusoft.HISFC.Models.Pharmacy.Item;
                    }

                    if (phaItem != null)
                    {
                        conObj = hsDisablePha[phaItem.ExtendData1] as Neusoft.HISFC.Models.Base.Const;
                        if (conObj != null)
                        {
                            if (!hsWarn.Contains(conObj.Name))
                            {
                                ArrayList al = new ArrayList();
                                al.Add(phaItem.Name);
                                hsWarn.Add(conObj.Name, al);
                            }
                            else
                            {
                                if (!(hsWarn[conObj.Name] as ArrayList).Contains(phaItem.Name))
                                {
                                    (hsWarn[conObj.Name] as ArrayList).Add(phaItem.Name);
                                }
                            }

                            //可以参考人员类别等，限制不允许开立
                            //err = "该药品属于【" + conObj.Name + "】，请注意！";
                            //return -1;
                        }
                    }
                }

                if (hsWarn.Count > 0)
                {
                    if (notify == null)
                    {
                        notify = new System.Windows.Forms.NotifyIcon();
                        notify.Icon = Neusoft.SOC.Local.Order.Properties.Resources.HIS;
                    }

                    string warn = "";
                    foreach (string key in hsWarn.Keys)
                    {
                        warn += "【" + key + "】：\r\n";
                        foreach (string strWarn in (ArrayList)hsWarn[key])
                        {
                            warn += strWarn + "\r\n";
                        }
                    }

                    notify.Visible = true;
                    notify.ShowBalloonTip(4, "禁用药警告", warn, System.Windows.Forms.ToolTipIcon.Warning);

                    System.Windows.Forms.MessageBox.Show(warn, "禁用药警告", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return -1;
            }

            return 1;
        }
    }
}
