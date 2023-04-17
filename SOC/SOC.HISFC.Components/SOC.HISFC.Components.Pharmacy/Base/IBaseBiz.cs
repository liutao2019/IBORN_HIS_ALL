using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.Components.Pharmacy.Base
{
    /// <summary>
    /// [功能描述: 入出库业务接口]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-3]<br></br>
    /// </summary>
    public interface IBaseBiz
    {

        void Dispose();

        /// <summary>
        /// 权限类别
        /// </summary>
        FS.FrameWork.Models.NeuObject PriveType
        {
            get;
            set;
        }

        /// <summary>
        /// 数据录入控件
        /// </summary>
        System.Windows.Forms.UserControl InputInfoControl
        {
            get;
        }

        /// <summary>
        /// 库存科室（当前做入出库的库房）
        /// </summary>
        int SetStockDept(FS.FrameWork.Models.NeuObject stockDept);

        /// <summary>
        /// 目标科室（出库领药科室等）
        /// </summary>
        int SetTargetDept(FS.FrameWork.Models.NeuObject targetDept);

        /// <summary>
        /// 来源（入库供货单位、公司、科室等）
        /// </summary>
        int SetFromDept(FS.FrameWork.Models.NeuObject fromDept);

        /// <summary>
        /// 领药人
        /// </summary>
        /// <param name="getPerson"></param>
        /// <returns></returns>
        int SetGetPerson(FS.FrameWork.Models.NeuObject getPerson);

        /// <summary>
        /// 工具栏按钮事件
        /// </summary>
        /// <param name="text">按钮文本</param>
        /// <returns></returns>
        int ToolbarAfterClick(string text);

        /// <summary>
        /// 初始化设置
        /// </summary>
        /// <returns></returns>
        int Init(SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataChooseList IDataChooseList, SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataDetail IDataDetail, System.Windows.Forms.ToolStrip toolStrip);

        /// <summary>
        /// 清除数据
        /// </summary>
        /// <returns></returns>
        int Clear();

        /// <summary>
        /// 获取入库业务的来源科室
        /// 一般包含院内科室和院外机构、单位等
        /// </summary>
        /// <returns></returns>
        ArrayList GetFromDeptArray(ref string fromDeptTypeName);

        /// <summary>
        /// 获取出库业务的出库科室科室
        /// 一般包含院内科室和院外机构、单位等
        /// </summary>
        /// <returns></returns>
        ArrayList GetTargetDeptArray();

        /// <summary>
        /// 设置入库界面的供货公司、来源科室
        /// </summary>
        FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.SetFromDeptHander SetFromDeptEven
        {
            get;
            set;
        }

        /// <summary>
        /// 设置出库界面的目标科室
        /// </summary>
        FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.SetTargetDeptHander SetTargetDeptEven
        {
            get;
            set;
        }

        bool ProcessDialogKey(System.Windows.Forms.Keys keyData);        
    }
}
