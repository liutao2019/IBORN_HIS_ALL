using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace IBorn.SI.MedicalInsurance.FoShan.Proxy
{
    public class MedicalFactoryProxy
    {
        /// <summary>
        /// 合同单位编码
        /// </summary>
        private string medicalID = string.Empty;//合同单位编码

        FS.HISFC.BizLogic.Manager.Constant managerConstant = new FS.HISFC.BizLogic.Manager.Constant();

        FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        List<FS.FrameWork.Models.NeuObject> medicalTypeList = new List<FS.FrameWork.Models.NeuObject>();
        /// <summary>
        /// 医保待遇算法
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
       
        public MedicalFactoryProxy()
        {
        }

        private string errorMsg = string.Empty;
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg
        {
            get { return errorMsg; }
            set { errorMsg = value; }
        }


        #region IBalance 成员

        #region 住院


        public int CancelBalanceInPatient(FS.HISFC.Models.RADT.PatientInfo register)
        {
            try
            {
                if (register == null)
                {
                    this.errorMsg = "请选择患者再进行操作";
                    return -1;
                }

                //待遇算法
                long returnValue = this.medcareInterfaceProxy.SetPactCode(register.Pact.ID);

                if (returnValue != 1)
                {
                    this.errorMsg = "接口未实现！" + this.medcareInterfaceProxy.ErrMsg;
                    return -1;
                }

                this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                returnValue = this.medcareInterfaceProxy.Connect();

                ArrayList alFeeList = new ArrayList();
                FS.HISFC.Models.Fee.Inpatient.FeeItemList f = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();

                returnValue = this.medcareInterfaceProxy.DeleteUploadedFeeDetailInpatient(register, f);
                if (returnValue != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.errorMsg = "删除已上传数据失败！" + this.medcareInterfaceProxy.ErrMsg;

                    return -1;
                }

                returnValue = this.medcareInterfaceProxy.CancelBalanceInpatient(register, ref alFeeList);
                if (returnValue != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.errorMsg = "取消医保患者结算失败！" + this.medcareInterfaceProxy.ErrMsg;

                    return -1;
                }

                //-----------------待遇接口计算完毕.
                FS.FrameWork.Management.PublicTrans.Commit();
                this.medcareInterfaceProxy.Commit();
                this.medcareInterfaceProxy.Disconnect();

                return 1;
            }
            catch (Exception e)
            {
                this.errorMsg = e.Message;
                return -1;
            }
        }

        #endregion

        #endregion

        #region ICompare 成员


        #endregion




    }
}
