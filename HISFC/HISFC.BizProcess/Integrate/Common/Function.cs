using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using FS.HISFC.Models.RADT;

namespace FS.HISFC.BizProcess.Integrate
{
    /// <summary>
    /// [��������: ��̬����������]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-04]<br></br>
    /// </summary>
    public class Function : IntegrateBase
    {
        public Function()
        {

        }

        #region ��ҩ����ӡ

        /// <summary>
        /// ��ҩ����ӡ�ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.IPrintExecDrug IPrintConsume = null;

        /// <summary>
        /// ��ҩ���ӿڴ�ӡ
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        private int InitConsumePrintInterface()
        {
            try
            {
                object[] o = new object[] { };
                //�Ժ���ά�������ȡ������
                System.Runtime.Remoting.ObjectHandle objHandel = System.Activator.CreateInstance("Report", "Report.Order.ucDrugConsuming", false, System.Reflection.BindingFlags.CreateInstance, null, o, null, null, null);
                if (objHandel != null)
                {
                    object oLabel = objHandel.Unwrap();

                    IPrintConsume = oLabel as FS.HISFC.BizProcess.Interface.IPrintExecDrug;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg(ex.Message));
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="alList">���ӡ����</param>
        public void PrintDrugConsume(List<FS.HISFC.Models.Order.ExecOrder> alList)
        {
            PrintDrugConsume(new System.Collections.ArrayList(alList.ToArray()));
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="alList">���ӡ����</param>
        public void PrintDrugConsume(System.Collections.ArrayList alData)
        {
            if (IPrintConsume == null)
            {
                if (InitConsumePrintInterface() == -1)
                {
                    return;
                }
            }

            FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();

            FS.FrameWork.Models.NeuObject dept = ((FS.HISFC.Models.Base.Employee)dataManager.Operator).Dept;
            FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();
            oper.ID = dataManager.Operator.ID;
            oper.Name = dataManager.Operator.Name;

            SortStockDept sort = new SortStockDept();

            alData.Sort(sort);

            IPrintConsume.SetTitle(oper, dept);

            IPrintConsume.SetExecOrder(alData);

            IPrintConsume.Print();
        }

        private class SortStockDept : System.Collections.IComparer
        {
            public SortStockDept()
            {
 
            }

            #region IComparer ��Ա

            public int Compare(object x, object y)
            {
                string xSort = (x as FS.HISFC.Models.Order.ExecOrder).Order.StockDept.ID;
                string ySort = (y as FS.HISFC.Models.Order.ExecOrder).Order.StockDept.ID;

                return xSort.CompareTo(ySort);
            }

            #endregion
        }

        #endregion

        #region  ��Ŀ�����¼    
   
         /// <summary>
        /// �����Ϣ����
        /// </summary>
        /// <param name="isInsert">�Ƿ����</param>
        /// <param name="isDel">�Ƿ�ɾ��</param>
        /// <param name="itemCode">��Ŀ���� ���ڱ�־�����Ϣ</param>
        /// <typeparam name="T">����</typeparam>
        /// <param name="originalObject">ԭ����</param>
        /// <param name="newObject">������</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int SaveChange<T>(bool isInsert, bool isDel, string itemCode, T originalObject, T newObject)
        {
            return SaveChange<T>(null,isInsert, isDel, itemCode, originalObject, newObject);
        }

        /// <summary>
        /// �����Ϣ����
        /// </summary>
        /// <param name="shiftType"></param>
        /// <param name="isInsert">�Ƿ����</param>
        /// <param name="isDel">�Ƿ�ɾ��</param>
        /// <param name="itemCode">��Ŀ���� ���ڱ�־�����Ϣ</param>
        /// <typeparam name="T">����</typeparam>
        /// <param name="originalObject">ԭ����</param>
        /// <param name="newObject">������</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int SaveChange<T>(string shiftType,bool isInsert,bool isDel,string itemCode,T originalObject, T newObject)
        {         
            FS.HISFC.BizLogic.Manager.ShiftData shiftManager = new FS.HISFC.BizLogic.Manager.ShiftData();

            #region ��ȡ������Ϣ
            
            Type t = typeof(T);

            string itemType = "0";
            if (shiftType == null)
            {
                switch (t.ToString())
                {
                    case "FS.HISFC.Models.Pharmacy.Item":
                        itemType = "0";
                        break;
                    case "FS.HISFC.Models.Fee.Item":
                        itemType = "1";
                        break;
                    case "FS.HISFC.Models.RADT.Patient":
                        itemType = "2";
                        break;
                }
            }
            else
            {
                itemType = shiftType;
            }

            #endregion

            #region ����/ɾ��������

            if (isInsert)           //�²�������
            {
                if (shiftManager.SetShiftData(itemCode, itemType, new FS.FrameWork.Models.NeuObject(), new FS.FrameWork.Models.NeuObject(), "�½�") == -1)
                {
                    System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("������Ŀ�½������¼ʧ��") + shiftManager.Err);
                    return -1;
                }
                return 1;
            }

            if (isDel)           //ɾ������
            {
                if (shiftManager.SetShiftData(itemCode, itemType, new FS.FrameWork.Models.NeuObject(), new FS.FrameWork.Models.NeuObject(), "ɾ��") == -1)
                {
                    System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("������Ŀɾ�������¼ʧ��") + shiftManager.Err);
                    return -1;
                }
                return 1;
            }

            #endregion

            if (originalObject == null || newObject == null)
            {
                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("��������¼ ����������� �޸�ʱԭʼֵ����ֵ����Ϊnull"));
                return -1;
            }
                     
            //��ȡά�������¼�������
            List<FS.HISFC.Models.Base.ShiftProperty> sihftList = shiftManager.QueryShiftProperty(t.ToString());
            if (sihftList == null)
            {
                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ���¼��������б�ʧ��") + shiftManager.Err);
                return -1;
            }
         
            foreach (FS.HISFC.Models.Base.ShiftProperty sf in sihftList)
            {
                if (!sf.IsRecord)           //���Ը����Ա�����м�¼
                {
                    continue;
                }
                //�����ֶ����ƻ�ȡPropertyinfo
                System.Reflection.PropertyInfo rP = t.GetProperty(sf.Property.ID);
                //��ʵ����ȡ����Ӧ����ֵ
                object rO = rP.GetValue(originalObject, null);
                //��ʵ����ȡ����Ӧ����ֵ
                object rN = rP.GetValue(newObject, null);
                //�Ƿ����仯�ж�
                if (rO is FS.FrameWork.Models.NeuObject)
                {
                    FS.FrameWork.Models.NeuObject origNeu = rO as FS.FrameWork.Models.NeuObject;
                    FS.FrameWork.Models.NeuObject newNeu = rN as FS.FrameWork.Models.NeuObject;

                    if (origNeu == null)
                    {
                        origNeu = new FS.FrameWork.Models.NeuObject();
                    }
                    if (newNeu == null)
                    {
                        newNeu = new FS.FrameWork.Models.NeuObject();
                    }

                    if (origNeu.ID != newNeu.ID)
                    {
                        if (shiftManager.SetShiftData(itemCode, itemType,origNeu, newNeu,sf.ShiftCause) == -1)
                        {
                            System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("��������¼ʧ�� ����:") + sf.Property.ID + shiftManager.Err);
                            return -1;
                        }
                    }
                }
                else
                {
                    FS.FrameWork.Models.NeuObject origNeu = new FS.FrameWork.Models.NeuObject();
                    FS.FrameWork.Models.NeuObject newNeu = new FS.FrameWork.Models.NeuObject();
                    if (rO == null)
                    {
                        rO = string.Empty;
                    }
                    origNeu.ID = rO.ToString();
                    origNeu.Name = sf.Property.Name;

                    newNeu.ID = rN.ToString();
                    newNeu.Name = sf.Property.Name;

                    if (origNeu.ID != newNeu.ID)
                    {
                        if (shiftManager.SetShiftData(itemCode, itemType, origNeu, newNeu, sf.ShiftCause) == -1)
                        {
                            System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("��������¼ʧ�� ����:") + sf.Property.ID + shiftManager.Err);
                            return -1;
                        }
                    }
                }
            }

            return 1;
        }

        #endregion

        #region ҽԺ����

        //        static string hosNameSelect = @"SELECT T.HOS_NAME,T.HOS_CODE,T.Mark 
        //										FROM  COM_HOSPITALINFO T
        //										WHERE  ROWNUM = 1";
        static string hosNameSelect = @"SELECT T.HOS_NAME,T.HOS_CODE,T.Mark 
										FROM  COM_HOSPITALINFO T";

        /// <summary>
        /// ҽԺ����
        /// </summary>
        protected static string HosName = "-1";
        protected static string HosCode = "-1";
        protected static string HosMemo = "-1";

        public static string GetHosCode()
        {
             //{A6AEB319-8190-4188-BFCB-825C83A14C89}
            //GetHosName();
            //return HosCode;
            return FS.FrameWork.Management.Connection.Hospital.ID;

        }
        /// <summary>
        /// ҽԺ���ƻ�ȡ
        /// </summary>
        /// <returns>�ɹ�����ҽԺ���� ʧ�ܷ��ؿ��ַ���</returns>
        public static string GetHosName()
        {
            // {A6AEB319-8190-4188-BFCB-825C83A14C89}
            //if (HosName == "-1")
            //{
            //    FS.FrameWork.Management.DataBaseManger dataBase = new FS.FrameWork.Management.DataBaseManger();
            //if (dataBase.ExecQuery(Function.hosNameSelect) == -1)
            //{
            //    return HosCode;
            //}

            //    try
            //    {
            //        if (dataBase.Reader.Read())
            //        {
            //            HosName = dataBase.Reader[0].ToString();
            //            HosCode = dataBase.Reader[1].ToString();
            //            HosMemo = dataBase.Reader[2].ToString();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        return "";
            //    }
            //    finally
            //    {
            //        if (!dataBase.Reader.IsClosed)
            //        {
            //            dataBase.Reader.Close();
            //        }
            //    }
            //}

            //return HosName;
            return FS.FrameWork.Management.Connection.Hospital.Name;
        }

        /// <summary>
        /// ��ȡҽԺ��Ϣ
        /// </summary>
        /// <returns></returns>
        public static FS.FrameWork.Models.NeuObject GetHosInfo()
        {
            // {A6AEB319-8190-4188-BFCB-825C83A14C89}
            //GetHosName();
            //return new FS.FrameWork.Models.NeuObject(HosCode, HosName, HosMemo);
            return FS.FrameWork.Management.Connection.Hospital;
        }

        #endregion


        //{0FF4B806-1507-4cfa-A269-6FBA9B044473}
        /// <summary>
        /// ���ݾ��ȼ���С��λ
        /// </summary>
        /// <param name="oldDecimal"></param>
        /// <returns></returns>
        public static decimal calculateDecimal(decimal oldDecimal)
        {
            int roundControl = 2;         // 0-����֤�飬1-����һλС��,2-������λС��,3-��ȡ��,4-��ȡ��
            decimal ShouldDecimal = oldDecimal;
            decimal RealDecimal = 0.0m;

            //������������
            if (roundControl < 3)
            {
                //����0,1,2λС��
                RealDecimal = Math.Round(ShouldDecimal, roundControl, MidpointRounding.AwayFromZero);
            }
            else if (roundControl == 3)
            {
                //��ȡ��
                RealDecimal = Math.Floor(ShouldDecimal);
            }
            else
            {
                //��ȡ��
                RealDecimal = Math.Ceiling(ShouldDecimal);
            }

            return RealDecimal;
        }


    }
}
