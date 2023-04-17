using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore
{
    /// <summary>
    /// [功能描述: 门诊药房LED显示接口]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-1]<br></br>
    /// </summary>
    public interface IOutpatientLED
    {
        /// <summary>
        /// 自动刷新显示数据，从发药窗口的树列表中取数据
        /// </summary>
        /// <param name="listDrugRecipe">处方调剂信息</param>
        /// <param name="operBusying">主窗口操作是否正忙</param>
        /// <returns>-1失败</returns>
        int AutoShowData(List<FS.HISFC.Models.Pharmacy.DrugRecipe> listDrugRecipe, bool operBusying);

        /// <summary>
        /// 选中处方后点击按钮调用，从发药窗口的树列表中取数据
        /// </summary>
        /// <param name="listDrugRecipe">未发药保存的处方</param>
        /// <param name="selectedDrugRecipe">当前选中的处方</param>
        /// <returns>-1失败</returns>
        int ShowDataAfterSelect(List<FS.HISFC.Models.Pharmacy.DrugRecipe> listDrugRecipe, FS.HISFC.Models.Pharmacy.DrugRecipe selectedDrugRecipe);

        /// <summary>
        /// 在保存后调用，从发药窗口的树列表中取数据
        /// </summary>
        /// <param name="listDrugRecipe">未发药保存的处方</param>
        /// <param name="savingDrugRecipe">当前保存的处方</param>
        /// <returns>-1失败</returns>
        int ShowDataAfterSave(List<FS.HISFC.Models.Pharmacy.DrugRecipe> listDrugRecipe, FS.HISFC.Models.Pharmacy.DrugRecipe savedDrugRecipe);

        /// <summary>
        /// 实现大屏设置：暂停，开关等
        /// </summary>
        /// <returns></returns>
        int SetLED();

        /// <summary>
        /// 过号
        /// </summary>
        /// <param name="listDrugRecipe"></param>
        /// <param name="selectedDrugRecipe"></param>
        /// <returns></returns>
        int OverNO(List<FS.HISFC.Models.Pharmacy.DrugRecipe> listDrugRecipe, FS.HISFC.Models.Pharmacy.DrugRecipe selectedDrugRecipe);
    }
}
