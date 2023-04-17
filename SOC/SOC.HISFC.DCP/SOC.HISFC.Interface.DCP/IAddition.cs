using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.BizProcess.DCPInterface
{
    /// <summary>
    /// [功能说明: 附卡接口，用于读取、写入、增加、删除、修改以及验证附卡信息]
    /// [添 加 人:   zj]
    /// [添加日期: 2008-08-25]
    /// </summary>
    public interface IAddition
    {
        /// <summary>
        /// 患者编号
        /// </summary>
        string PatientNO
        {
            get;
            set;
        }

        /// <summary>
        /// 患者姓名
        /// </summary>
        string PatientName
        {
            get;
            set;
        }

        /// <summary>
        /// 附卡实体
        /// </summary>
        FS.HISFC.DCP.Object.CommonReport Report
        {
            get;
            set;
        }
        /// <summary>
        /// 验证附卡信息的完整性
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <returns>-1 不完整  1 完整</returns>
        int IsValid(ref string msg);

        /// <summary>
        /// 获取附卡信息
        /// </summary>
        /// <returns>附卡信息实体</returns>
        FS.HISFC.DCP.Object.AdditionReport GetAdditionInfo(Control container);

        /// <summary>
        /// 根据报卡NO获取附卡信息
        /// </summary>
        /// <param name="reportNO">报卡NO</param>
        /// <returns>附卡信息实体</returns>
        FS.HISFC.DCP.Object.AdditionReport GetAdditionInfo(string reportNO);

        /// <summary>
        /// 写入附卡信息
        /// </summary>
        /// <param name="addition">附卡信息实体</param>
        void SetAdditionInfo(FS.HISFC.DCP.Object.AdditionReport addition, Control container);

        /// <summary>
        /// 修改附卡信息
        /// </summary>
        /// <param name="addition">附卡信息实体</param>
        /// <param name="t">事务</param>
        int UpdateAdditionInfo(FS.HISFC.DCP.Object.AdditionReport addition);

        /// <summary>
        /// 插入附卡信息
        /// </summary>
        /// <param name="addition">附卡信息实体</param>
        /// <param name="t">事务</param>
        int InsertAdditionInfo(FS.HISFC.DCP.Object.AdditionReport addition);

        /// <summary>
        /// 删除附卡信息
        /// </summary>
        /// <param name="addition">附卡信息实体</param>
        /// <param name="t">事务</param>
        int DeleteAdditionInfo();

    }
}
