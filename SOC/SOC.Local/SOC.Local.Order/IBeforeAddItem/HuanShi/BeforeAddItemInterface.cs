using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.Local.Order.IBeforeAddItem.HuanShi
{
    /// <summary>
    /// 开立项目前接口
    /// 院长助理王永江提出：当开立项目包含“输血”字样时，给出如下提示
    ///
    /** 内科输血指征

        （1）【红细胞】：血红蛋白<60克/升，或红细胞比积<0.30时输注。
        （2）急性失血超过30%血容量 
        （3）【血小板】：血小板计数<30×109升，或血小板功能低下且伴有出血表现时输注。 
        （4）【新鲜冰冻血浆】：用于各种原因引起的凝血因子缺乏并伴有出血表现时输注。
        （5）【冷沉淀】：用于严重肝功能不全、血友病出血补充凝血因子（特别是Ⅷ因子）的缺陷及严重肝病患者
        （6）【洗涤红细胞】：用于避免引起同种异型白细胞抗体和避免输入血浆中某些成分 
    */
    /// </summary>
    public class BeforeAddItemInterface : FS.HISFC.BizProcess.Interface.Order.IBeforeAddItem
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
        public int OnBeforeAddItemForInPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, ArrayList alOrder)
        {
            //return this.CheckOrder(patientInfo, reciptDept, reciptDoct, alOrder, FS.HISFC.Models.Base.ServiceTypes.I);
            return 1;
        }



        System.Windows.Forms.NotifyIcon notify = null;

        /// <summary>
        /// 药品管理
        /// </summary>
        FS.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.HISFC.BizLogic.Pharmacy.Item();

        FS.HISFC.BizLogic.Pharmacy.Constant phaConMgr = new FS.HISFC.BizLogic.Pharmacy.Constant();

        /// <summary>
        /// 取药科室列表
        /// </summary>
        List<FS.FrameWork.Models.NeuObject> alStockDept = null;

        /// <summary>
        /// 门诊开立判断
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int OnBeforeAddItemForOutPatient(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, ArrayList alOrder)
        {
            return this.CheckOrder(regObj, reciptDept, reciptDoct, alOrder, FS.HISFC.Models.Base.ServiceTypes.C);
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
        public int CheckOrder(FS.HISFC.Models.RADT.Patient regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, ArrayList alOrder, FS.HISFC.Models.Base.ServiceTypes type)
        {
            try
            {
                if (alOrder.Count >0)
                {
                    FS.HISFC.Models.Order.Order order = alOrder[0] as FS.HISFC.Models.Order.Order;

                    //如果输血器和静脉输血不提示的话 再打开
                    if (order.Item.Name.Contains("输血")
                        //&&order.Item.Name.Contains("静脉输血")
                        //&&order.Item.Name.Contains("输血器")
                        )
                    {
                        string warning = @"
输血指征
    （1）【红细胞】：血红蛋白<60克/升，或红细胞比积<0.30时输注。
    （2）急性失血超过30%血容量 
    （3）【血小板】：血小板计数<30×109升，或血小板功能低下且伴有出血表现时输注。 
    （4）【新鲜冰冻血浆】：用于各种原因引起的凝血因子缺乏并伴有出血表现时输注。
    （5）【冷沉淀】：用于严重肝功能不全、血友病出血补充凝血因子（特别是Ⅷ因子）的缺陷及严重肝病患者
    （6）【洗涤红细胞】：用于避免引起同种异型白细胞抗体和避免输入血浆中某些成分 ";

                        MessageBox.Show(warning, "输血提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                //err = ex.Message;
                //return -1;
            }

            return 1;
        }
    }
}
