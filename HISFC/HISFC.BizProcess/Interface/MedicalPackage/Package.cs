using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.MedicalPackage
{
    /// <summary>
    /// IPackage<br></br>
    /// [功能描述: 套餐管理接口]
    /// [创建时间: 2017-03-04]
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='yyyy-mm-dd' 
    ///		修改目的=''
    ///		修改描述=''
    ///		/>
    /// </summary>
    public interface IPackage
    {
        /// <summary>
        /// 事务设置
        /// </summary>
        /// <param name="trans"></param>
        void SetTrans(System.Data.IDbTransaction trans);

        /// <summary>
        /// 错误信息
        /// </summary>
        string Err
        {
            get;
            set;
        }

        /// <summary>
        /// 错误码
        /// </summary>
        string ErrCode
        {
            get;
            set;
        }

        /// <summary>
        /// 数据库错误码
        /// </summary>
        int DBErrCode
        {
            get;
            set;
        }

        #region 套餐主表相关

        /// <summary>
        /// 通过编号获取套餐信息
        /// </summary>
        /// <param name="packageID"></param>
        /// <returns></returns>
        HISFC.Models.MedicalPackage.Package GetPackage(string packageID);

        /// <summary>
        /// 按类型获取套餐
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        ArrayList GetPackageListByType(string Type);

        /// <summary>
        /// 增加套餐
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        int AddPackage(HISFC.Models.MedicalPackage.Package package);

        /// <summary>
        /// 更新套餐
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        int UpdatePackage(HISFC.Models.MedicalPackage.Package package);

        /// <summary>
        /// 删除套餐
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        int DeletePackage(HISFC.Models.MedicalPackage.Package package);

        /// <summary>
        /// 设置套餐启用或禁用
        /// </summary>
        /// <param name="package"></param>
        /// <param name="valid"></param>
        /// <returns></returns>
        int SetPackageValid(HISFC.Models.MedicalPackage.Package package,bool valid);

        /// <summary>
        /// 根据套餐查询套餐总金额
        /// </summary>
        /// <param name="packageID"></param>
        /// <returns></returns>
        decimal QueryTotFeeByPackge(string packageID);

        /// <summary>
        /// 套餐转移，套餐主表
        /// </summary>
        /// <param name="PatientInfo"></param>
        /// <param name="PatientInfoNew"></param>
        /// <param name="invoiceInfo"></param>
        /// <param name="operCode"></param>
        /// <returns></returns>
        int ShiftPackageOwner(FS.HISFC.Models.RADT.PatientInfo PatientInfo, FS.HISFC.Models.RADT.PatientInfo PatientInfoNew, FS.HISFC.Models.MedicalPackage.Fee.Invoice invoiceInfo, string operCode);


        #endregion

        #region 套餐明细表相关

        /// <summary>
        /// 通过套餐编号和序号获取套餐信息
        /// </summary>
        /// <param name="packageID"></param>
        /// <param name="sequenceNO"></param>
        /// <returns></returns>
        HISFC.Models.MedicalPackage.PackageDetail GetPackageItem(string packageID,string sequenceNO);

        /// <summary>
        /// 根据套餐编号获取套餐明细
        /// </summary>
        /// <param name="PackageID"></param>
        /// <returns></returns>
        ArrayList GetPackageItemByPackageID(string PackageID);

        /// <summary>
        /// 增加套餐明细
        /// </summary>
        /// <param name="package"></param>
        /// <param name="packageDetail"></param>
        /// <returns></returns>
        int AddPackageDetail(HISFC.Models.MedicalPackage.Package package,HISFC.Models.MedicalPackage.PackageDetail packageDetail);

        /// <summary>
        /// 删除套餐明细
        /// </summary>
        /// <param name="package"></param>
        /// <param name="packageDetail"></param>
        /// <returns></returns>
        int DeletePackageDetail(HISFC.Models.MedicalPackage.Package package, HISFC.Models.MedicalPackage.PackageDetail packageDetail);

        /// <summary>
        /// 更新套餐明细
        /// </summary>
        /// <param name="packageDetail"></param>
        /// <returns></returns>
        int UpdatePackageDetail(HISFC.Models.MedicalPackage.PackageDetail packageDetail);

        /// <summary>
        /// 根据套餐编号删除套餐明细
        /// </summary>
        /// <param name="packageID"></param>
        /// <returns></returns>
        int DeletePackageDetail(string packageID);

        /// <summary>
        /// 套餐转移，套餐明细表{3599D82C-0E7E-4628-8FC6-DDAAA1CC6335}
        /// </summary>
        /// <param name="PatientInfo"></param>
        /// <param name="PatientInfoNew"></param>
        /// <param name="invoiceInfo"></param>
        /// <param name="operCode"></param>
        /// <returns></returns>
        int ShiftPackageDetailOwner(FS.HISFC.Models.RADT.PatientInfo PatientInfo, FS.HISFC.Models.RADT.PatientInfo PatientInfoNew, FS.HISFC.Models.MedicalPackage.Fee.Invoice invoiceInfo,string operCode);
        

        #endregion


    }
}
