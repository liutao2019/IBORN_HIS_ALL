using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Factory
{
    /// <summary>
    /// 
    /// </summary>
    public class Function
    {

        private Function()
        {

        }

        public static void BeginTransaction()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
        }

        public static void Commit()
        {
            FS.FrameWork.Management.PublicTrans.Commit();
        }

        public static void RollBack()
        {
            FS.FrameWork.Management.PublicTrans.RollBack();
        }

        static EPRBase EPRmanager = null;
        static ManagerBase ManManager = null;
        static OrderBase OrderManager = null;
        static RADTBase RadtManager = null;

        /// <summary>
        /// ��ǰ���Ӳ���ҵ���
        /// </summary>
        public static EPRBase IntegrateEPR
        {
            get
            {
                if(EPRmanager == null)
                     EPRmanager = new EPRManagement();

                 return EPRmanager;
            }
        }

        /// <summary>
        /// ϵͳ����
        /// </summary>
        public static ManagerBase IntegrateManager
        {
            get
            {
                if (ManManager == null)
                    ManManager = new ManagerManagement();

                return ManManager;
            }
        }

        /// <summary>
        /// ҽ������
        /// </summary>
        public static OrderBase IntegrateOrder
        {
            get
            {
                if (OrderManager == null)
                    OrderManager = new OrderManagement();

                return OrderManager;
            }
        }

        /// <summary>
        /// ���ת����
        /// </summary>
        public static RADTBase IntegrateRADT
        {
            get
            {
                if (RadtManager == null)
                    RadtManager = new RADTManagement();

                return RadtManager;
            }
        }

    }

    public class EPRManagement : EPRBase
    {

    }

    public class ManagerManagement : ManagerBase
    {
         
    }

    public class OrderManagement : OrderBase
    {

    }

    public class RADTManagement : RADTBase
    {

    }
    
}
