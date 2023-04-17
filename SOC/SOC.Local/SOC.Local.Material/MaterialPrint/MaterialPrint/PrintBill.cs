using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOC.Local.MaterialPrint
{
    /// <summary>
    /// 单据打印信息
    /// </summary>
    public class PrintBill
    {
        private bool isNeedQuest = false;
        private bool isNeedPrint = true;
        private bool isNeedPreview = true;
        private bool isQuested = false;
        private string title = "";
        private string myDLLName = "";
        private string controlName = "";
        private int rowCount = 0;

        private SortType sortType = SortType.物理顺序;

        FS.HISFC.Models.Base.PageSize pageSize = new FS.HISFC.Models.Base.PageSize();

        /// <summary>
        /// 是否需要询问:“保存成功，是否打印单据”
        /// </summary>
        public bool IsNeedQuest
        {
            get { return isNeedQuest; }
            set { this.isNeedQuest = value; }
        }

        /// <summary>
        /// 是否需要打印单据
        /// </summary>
        public bool IsNeedPrint
        {
            get { return isNeedPrint; }
            set { this.isNeedPrint = value; }
        }

        /// <summary>
        /// 是否需要预览
        /// </summary>
        public bool IsNeedPreview
        {
            get { return this.isNeedPreview; }
            set { this.isNeedPreview = value; }
        }

        /// <summary>
        /// 是否已经询问
        /// </summary>
        public bool IsQuested
        {
            get { return this.isQuested; }
            set { this.isQuested = value; }
        }

        /// <summary>
        /// 纸张设置
        /// </summary>
        public FS.HISFC.Models.Base.PageSize PageSize
        {
            get { return this.pageSize; }
            set { this.pageSize = value; }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        /// <summary>
        /// 打印单据的名称
        /// </summary>
        public string ControlName
        {
            get { return controlName; }
            set { controlName = value; }
        }

        /// <summary>
        /// 程序集
        /// </summary>
        public string DLLName
        {
            get { return myDLLName; }
            set { this.myDLLName = value; }
        }

        /// <summary>
        /// 数据行数
        /// </summary>
        public int RowCount
        {
            get { return rowCount; }
            set { rowCount = value; }
        }

        /// <summary>
        /// 排序方式
        /// </summary>
        public SortType Sort
        {
            get { return this.sortType; }
            set { this.sortType = value; }
        }


        /// <summary>
        /// 本地设置的列
        /// </summary>
        public enum ColSet
        {
            二级权限代码,
            三级权限代码,
            操作类别,
            单据名称,
            提示,
            打印,
            预览,
            已经询问,
            排序方式,
            纸张名称,
            每页行数,
            程序集,
            控件,
            End
        }

        /// <summary>
        /// 排序方式
        /// </summary>
        public enum SortType
        {
            货位号,
            物理顺序,
            类别货位号,
        }

        #region 排序类
        internal class CompareByPlaceNO : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                string oX = "";
                string oY = "";
                if (x is FS.HISFC.Models.Pharmacy.Output && y is FS.HISFC.Models.Pharmacy.Output)
                {
                    oX = (x as FS.HISFC.Models.Pharmacy.Output).Clone().PlaceNO;
                    oY = (y as FS.HISFC.Models.Pharmacy.Output).Clone().PlaceNO;
                }
                else if (x is FS.HISFC.Models.Pharmacy.Check && y is FS.HISFC.Models.Pharmacy.Check)
                {
                    oX = (x as FS.HISFC.Models.Pharmacy.Check).Clone().PlaceNO;
                    oY = (y as FS.HISFC.Models.Pharmacy.Check).Clone().PlaceNO;
                }

                int nComp;

                if (oX == null)
                {
                    nComp = (oY != null) ? 1 : 0;
                }
                else if (oY == null)
                {
                    nComp = 1;
                }
                else
                {
                    nComp = string.Compare(oX.ToString(), oY.ToString());
                }

                return nComp;
            }

        }
        internal class CompareByTypePlaceNO : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                string oX = "";
                string oY = "";
                if (x is FS.HISFC.Models.Pharmacy.Output && y is FS.HISFC.Models.Pharmacy.Output)
                {
                    oX = (x as FS.HISFC.Models.Pharmacy.Output).Clone().Item.Type.ID + (x as FS.HISFC.Models.Pharmacy.Output).Clone().PlaceNO;
                    oY = (y as FS.HISFC.Models.Pharmacy.Output).Clone().Item.Type.ID + (y as FS.HISFC.Models.Pharmacy.Output).Clone().PlaceNO;
                }
                else if (x is FS.HISFC.Models.Pharmacy.Check && y is FS.HISFC.Models.Pharmacy.Check)
                {
                    oX = (x as FS.HISFC.Models.Pharmacy.Check).Clone().Item.Type.ID + (x as FS.HISFC.Models.Pharmacy.Check).Clone().PlaceNO;
                    oY = (y as FS.HISFC.Models.Pharmacy.Check).Clone().Item.Type.ID + (y as FS.HISFC.Models.Pharmacy.Check).Clone().PlaceNO;
                }

                int nComp;

                if (oX == null)
                {
                    nComp = (oY != null) ? 1 : 0;
                }
                else if (oY == null)
                {
                    nComp = 1;
                }
                else
                {
                    nComp = string.Compare(oX.ToString(), oY.ToString());
                }

                return nComp;
            }

        }
        internal class CompareByListNO : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                string oX = "";
                string oY = "";
                if (x is FS.HISFC.Models.Pharmacy.Pay && y is FS.HISFC.Models.Pharmacy.Pay)
                {
                    oX = (x as FS.HISFC.Models.Pharmacy.Pay).Clone().InListNO;
                    oY = (y as FS.HISFC.Models.Pharmacy.Pay).Clone().InListNO;
                }

                int nComp;

                if (oX == null)
                {
                    nComp = (oY != null) ? 1 : 0;
                }
                else if (oY == null)
                {
                    nComp = 1;
                }
                else
                {
                    nComp = string.Compare(oX.ToString(), oY.ToString());
                }

                return nComp;
            }

        }
        internal class CompareByBillNO : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                string oX = "";
                string oY = "";
                if (x is FS.FrameWork.Models.NeuObject && y is FS.FrameWork.Models.NeuObject)
                {
                    oX = ((FS.FrameWork.Models.NeuObject)x).ID;
                    oY = ((FS.FrameWork.Models.NeuObject)y).ID;
                }

                int nComp;

                if (oX == null)
                {
                    oX = "0";
                }
                if (oY == null)
                {
                    oY = "0";
                }

                decimal compare = FS.FrameWork.Function.NConvert.ToDecimal(oX) - FS.FrameWork.Function.NConvert.ToDecimal(oY);
                if (compare == 0)
                {
                    return 0;
                }
                else if (compare < 0)
                {
                    return -1;
                }
                return 1;
            }


        }
        internal class CompareByPayDate : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.Pay oXPay;
                FS.HISFC.Models.Pharmacy.Pay oYPay;
                string oX = "";
                string oY = "";
                if (x is FS.HISFC.Models.Pharmacy.Pay && y is FS.HISFC.Models.Pharmacy.Pay)
                {
                    oXPay = (x as FS.HISFC.Models.Pharmacy.Pay);
                    oYPay = (y as FS.HISFC.Models.Pharmacy.Pay);


                    if (oXPay.ExtendTime > new DateTime(2009, 7, 1, 0, 0, 0))
                    {
                        oX = oXPay.ExtendTime.ToString("yyyyMMdd");
                    }
                    else
                    {
                        oX = oXPay.PayOper.OperTime.ToString("yyyyMMdd");
                    }


                    if (oYPay.ExtendTime > new DateTime(2009, 7, 1, 0, 0, 0))
                    {
                        oY = oYPay.ExtendTime.ToString("yyyyMMdd");
                    }
                    else
                    {
                        oY = oYPay.PayOper.OperTime.ToString("yyyyMMdd");
                    }
                }

                int nComp;

                if (oX == null)
                {
                    nComp = (oY != null) ? 1 : 0;
                }
                else if (oY == null)
                {
                    nComp = 1;
                }
                else
                {
                    nComp = string.Compare(oX.ToString(), oY.ToString());
                }

                return nComp;
            }

        }

        #endregion

    }
}
