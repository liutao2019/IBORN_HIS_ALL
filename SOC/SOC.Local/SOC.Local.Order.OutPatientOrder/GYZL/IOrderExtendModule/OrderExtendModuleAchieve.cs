using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using FS.FrameWork.Models;

namespace FS.SOC.Local.Order.OutPatientOrder.GYZL.IOrderExtendModule
{
    class OrderExtendModuleAchieve:FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOrderExtendModule
    {
        private frmAnesthesiaManager frmAnesthesiaManager = new frmAnesthesiaManager();

        FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint iAnesthesia = null;

        private FS.HISFC.Models.Base.Employee oper = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
        #region IOrderExtendModule 成员

        /// <summary>
        /// 只能在开立模式下使用
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="regObj"></param>
        /// <param name="alOutOrders"></param>
        /// <returns></returns>
        public int DoOrderExtend1(System.Windows.Forms.IWin32Window owner, FS.HISFC.Models.Registration.Register regObj, System.Collections.ArrayList alOutOrders)
        {
            //FS.HISFC.Models.RADT.Patient patient = regObj;
            //if (this.frmAnesthesiaManager == null || this.frmAnesthesiaManager.IsDisposed)
            //{
            //    this.frmAnesthesiaManager = new frmAnesthesiaManager();
            //}
            //this.frmAnesthesiaManager.Init(patient);
            //this.frmAnesthesiaManager.Show(owner);
            if (iAnesthesia == null)
            {
                iAnesthesia = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.Local.Order.OutPatientOrder.GYZL.IOrderExtendModule.OrderExtendModuleAchieve), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 9) as FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
            }
            if (iAnesthesia != null)
            {
                ArrayList alTmp = new ArrayList();
                iAnesthesia.PrintOutPatientOrderBill(regObj, null, null, alTmp, false);
            }
            return 1;
        }

        /// <summary>
        /// 任何时候都可以使用
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="regObj"></param>
        /// <param name="alOutOrders"></param>
        /// <returns></returns>
        public int DoOrderExtend2(System.Windows.Forms.IWin32Window owner, FS.HISFC.Models.Registration.Register regObj, System.Collections.ArrayList alOutOrders)
        {
            FS.HISFC.Models.RADT.Patient patient = regObj;
            //this.frmAnesthesiaManager.Init(patient);
            //this.frmAnesthesiaManager.Show(owner);
            FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
           
            //List<FS.HISFC.Models.Account.AccountCard> list = accountManager.GetMarkList(string cardNO)
            // FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
            ArrayList accountList = accountManager.GetMarkByCardNo(regObj.PID.CardNO, "Health_CARD", "1");
      
            FS.HISFC.BizLogic.HealthCard.HealthCard healthCard = new FS.HISFC.BizLogic.HealthCard.HealthCard();
            FS.HISFC.BizLogic.HealthCard.HealthCardManager healthCardManager = new FS.HISFC.BizLogic.HealthCard.HealthCardManager();
            if (accountList.Count > 0)
            {
                FS.FrameWork.Models.NeuObject accountCard = accountList[0] as FS.FrameWork.Models.NeuObject;
                healthCard.UnitCode = "455350760";
                healthCard.DepartmentCode = "0001";
                healthCard.UserName = "455350760_001";
                healthCard.Password = "888888";
                healthCard.CardNumber = accountCard.Name;
                healthCard.CardType = "0";

            }
            else
            {
                healthCard.UnitCode = "455350760";
                healthCard.DepartmentCode = "0001";
                healthCard.UserName = "455350760_001";
                healthCard.Password = "888888";
                healthCard.CardNumber = regObj.PID.CardNO;
                healthCard.CardType = "0";//regObj.Card.CardType.ID;
            }

            string staffNo = "0001";
            string name = "测试";
            string businesType = "01";
            string businessSerialNO = regObj.ID;

            int existResult = healthCardManager.PatientExist(healthCard, staffNo, name, businesType, businessSerialNO);
            //存在合法数据
           // if (existResult >= 4 && existResult <= 7)
           // {
               healthCardManager.PopHealthRecord(healthCard,staffNo, name, businesType, businessSerialNO);
           // }
          //  else
           // {
          //      MessageBox.Show(healthCardManager.Err, "提示");
           // }
            return 1;
        }

        /// <summary>
        /// 只能在非开立模式下使用
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="regObj"></param>
        /// <param name="alOutOrders"></param>
        /// <returns></returns>
        public int DoOrderExtend3(System.Windows.Forms.IWin32Window owner, FS.HISFC.Models.Registration.Register regObj, System.Collections.ArrayList alOutOrders)
        {
            throw new NotImplementedException();
        }

        private string err;
        public string Err
        {
            get
            {
                return this.err;
            }
            set
            {
                this.err = value;
            }
        }

        #endregion
    }
}
