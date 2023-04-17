using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore
{
    /// <summary>
    /// [功能描述: 住院药房摆药单标准控件接口]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// </summary>
    public interface IInpatientBill
    {

        /// <summary>
        /// 显示数据
        /// </summary>
        /// <param name="alData">applyout出库申请实体数组</param>
        /// <param name="drugBillClass">摆药单分类</param>
        /// <param name="stockDept">实发科室</param>
        /// <returns>-1发生错误 0没有处理，1处理成功</returns>
        void ShowData(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept);

        /// <summary>
        /// 打印
        /// </summary>
        void Print();

        /// <summary>
        /// System.Windows.Forms.DockStyle
        /// 补打的时候需要返回控件，然后设置DockStyle属性
        /// </summary>
        System.Windows.Forms.DockStyle WinDockStyle
        { get; set; }

        /// <summary>
        /// 住院药房单据类型
        /// </summary>
        InpatientBillType InpatientBillType
        { get;}
    }
}
