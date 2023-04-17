using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using FS.HISFC.Models.RADT;

namespace FS.HISFC.BizProcess.Integrate
{
    /// <summary>
    /// [功能描述: 静态函数集合类]<br></br>
    /// [创 建 者: 梁俊泽]<br></br>
    /// [创建时间: 2007-04]<br></br>
    /// </summary>
    public class Function : IntegrateBase
    {
        public Function()
        {

        }

        #region 领药单打印

        /// <summary>
        /// 领药单打印接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.IPrintExecDrug IPrintConsume = null;

        /// <summary>
        /// 领药单接口打印
        /// </summary>
        /// <returns>成功返回1 失败返回－1</returns>
        private int InitConsumePrintInterface()
        {
            try
            {
                object[] o = new object[] { };
                //以后由维护界面获取类名称
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
        /// 打印
        /// </summary>
        /// <param name="alList">需打印函数</param>
        public void PrintDrugConsume(List<FS.HISFC.Models.Order.ExecOrder> alList)
        {
            PrintDrugConsume(new System.Collections.ArrayList(alList.ToArray()));
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="alList">需打印函数</param>
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

            #region IComparer 成员

            public int Compare(object x, object y)
            {
                string xSort = (x as FS.HISFC.Models.Order.ExecOrder).Order.StockDept.ID;
                string ySort = (y as FS.HISFC.Models.Order.ExecOrder).Order.StockDept.ID;

                return xSort.CompareTo(ySort);
            }

            #endregion
        }

        #endregion

        #region  项目变更记录    
   
         /// <summary>
        /// 变更信息保存
        /// </summary>
        /// <param name="isInsert">是否插入</param>
        /// <param name="isDel">是否删除</param>
        /// <param name="itemCode">项目编码 用于标志变更信息</param>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="originalObject">原数据</param>
        /// <param name="newObject">新数据</param>
        /// <returns>成功返回1 失败返回-1</returns>
        public int SaveChange<T>(bool isInsert, bool isDel, string itemCode, T originalObject, T newObject)
        {
            return SaveChange<T>(null,isInsert, isDel, itemCode, originalObject, newObject);
        }

        /// <summary>
        /// 变更信息保存
        /// </summary>
        /// <param name="shiftType"></param>
        /// <param name="isInsert">是否插入</param>
        /// <param name="isDel">是否删除</param>
        /// <param name="itemCode">项目编码 用于标志变更信息</param>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="originalObject">原数据</param>
        /// <param name="newObject">新数据</param>
        /// <returns>成功返回1 失败返回-1</returns>
        public int SaveChange<T>(string shiftType,bool isInsert,bool isDel,string itemCode,T originalObject, T newObject)
        {         
            FS.HISFC.BizLogic.Manager.ShiftData shiftManager = new FS.HISFC.BizLogic.Manager.ShiftData();

            #region 获取类型信息
            
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

            #region 插入/删除保存变更

            if (isInsert)           //新插入数据
            {
                if (shiftManager.SetShiftData(itemCode, itemType, new FS.FrameWork.Models.NeuObject(), new FS.FrameWork.Models.NeuObject(), "新建") == -1)
                {
                    System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("保存项目新建变更记录失败") + shiftManager.Err);
                    return -1;
                }
                return 1;
            }

            if (isDel)           //删除数据
            {
                if (shiftManager.SetShiftData(itemCode, itemType, new FS.FrameWork.Models.NeuObject(), new FS.FrameWork.Models.NeuObject(), "删除") == -1)
                {
                    System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("保存项目删除变更记录失败") + shiftManager.Err);
                    return -1;
                }
                return 1;
            }

            #endregion

            if (originalObject == null || newObject == null)
            {
                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("保存变更记录 传入参数错误 修改时原始值与新值不能为null"));
                return -1;
            }
                     
            //获取维护的需记录变更属性
            List<FS.HISFC.Models.Base.ShiftProperty> sihftList = shiftManager.QueryShiftProperty(t.ToString());
            if (sihftList == null)
            {
                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("获取需记录变更属性列表失败") + shiftManager.Err);
                return -1;
            }
         
            foreach (FS.HISFC.Models.Base.ShiftProperty sf in sihftList)
            {
                if (!sf.IsRecord)           //不对该属性变更进行记录
                {
                    continue;
                }
                //根据字段名称获取Propertyinfo
                System.Reflection.PropertyInfo rP = t.GetProperty(sf.Property.ID);
                //由实体内取出相应属性值
                object rO = rP.GetValue(originalObject, null);
                //由实体内取出相应属性值
                object rN = rP.GetValue(newObject, null);
                //是否发生变化判断
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
                            System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("保存变更记录失败 属性:") + sf.Property.ID + shiftManager.Err);
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
                            System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("保存变更记录失败 属性:") + sf.Property.ID + shiftManager.Err);
                            return -1;
                        }
                    }
                }
            }

            return 1;
        }

        #endregion

        #region 医院名称

        //        static string hosNameSelect = @"SELECT T.HOS_NAME,T.HOS_CODE,T.Mark 
        //										FROM  COM_HOSPITALINFO T
        //										WHERE  ROWNUM = 1";
        static string hosNameSelect = @"SELECT T.HOS_NAME,T.HOS_CODE,T.Mark 
										FROM  COM_HOSPITALINFO T";

        /// <summary>
        /// 医院名称
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
        /// 医院名称获取
        /// </summary>
        /// <returns>成功返回医院名称 失败返回空字符串</returns>
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
        /// 获取医院信息
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
        /// 根据精度计算小数位
        /// </summary>
        /// <param name="oldDecimal"></param>
        /// <returns></returns>
        public static decimal calculateDecimal(decimal oldDecimal)
        {
            int roundControl = 2;         // 0-保留证书，1-保留一位小数,2-保留两位小数,3-下取整,4-上取整
            decimal ShouldDecimal = oldDecimal;
            decimal RealDecimal = 0.0m;

            //处理四舍五入
            if (roundControl < 3)
            {
                //保留0,1,2位小数
                RealDecimal = Math.Round(ShouldDecimal, roundControl, MidpointRounding.AwayFromZero);
            }
            else if (roundControl == 3)
            {
                //下取整
                RealDecimal = Math.Floor(ShouldDecimal);
            }
            else
            {
                //上取整
                RealDecimal = Math.Ceiling(ShouldDecimal);
            }

            return RealDecimal;
        }


    }
}
