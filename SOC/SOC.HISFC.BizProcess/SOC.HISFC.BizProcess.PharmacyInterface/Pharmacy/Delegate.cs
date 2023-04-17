using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy
{
    public class Delegate
    {
        /// <summary>
        /// 数据选择录入信息完成代理（函数指针声明）
        /// </summary>
        public delegate void ChooseCompletedHander();

        /// <summary>
        /// 提供业务类设置入库界面供货公司\药品来源科室的代理（函数指针声明）
        /// </summary>
        public delegate void SetFromDeptHander(FS.FrameWork.Models.NeuObject fromDept);

        /// <summary>
        /// 提供业务类设置出库库界面目标科室的代理（函数指针声明）
        /// </summary>
        public delegate void SetTargetDeptHander(FS.FrameWork.Models.NeuObject targetDept);

        /// <summary>
        /// 过滤文本发生变化
        /// </summary>
        public delegate void FilterTextChangeHander();

        /// <summary>
        /// 一般入库\特殊入库录入信息完成代理（函数指针声明）
        /// </summary>
        public delegate void InputCompletedHander();

        /// <summary>
        /// 过滤方式设置完成
        /// </summary>
        public delegate void CheckFilterSetCompletedHander(CheckFilterSetting checkFilterSetting);
    }
}
