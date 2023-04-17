using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.BizProcess.Integrate.MedicalPackage
{
    public class Package : IntegrateBase,FS.HISFC.BizProcess.Interface.MedicalPackage.IPackage
    {
        #region 逻辑管理类

        /// <summary>
        /// 套餐管理类
        /// </summary>
        private FS.HISFC.BizLogic.MedicalPackage.Package packageManger = new FS.HISFC.BizLogic.MedicalPackage.Package();

        /// <summary>
        /// 明细管理类
        /// </summary>
        private FS.HISFC.BizLogic.MedicalPackage.PackageDetail detailManager = new FS.HISFC.BizLogic.MedicalPackage.PackageDetail();

        /// <summary>
        /// 套餐管理类2
        /// </summary>
        private FS.HISFC.BizLogic.MedicalPackage.Fee.Package packageManger2 = new FS.HISFC.BizLogic.MedicalPackage.Fee.Package();

        /// <summary>
        /// 套餐管理类2
        /// </summary>
        private FS.HISFC.BizLogic.MedicalPackage.Fee.PackageDetail detailManager2 = new FS.HISFC.BizLogic.MedicalPackage.Fee.PackageDetail();

        /// <summary>
        /// 非药品
        /// </summary>
        FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();

        /// <summary>
        /// 药品
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Pharmacy itemIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        #endregion

        #region 接口函数实现

        /// <summary>
        /// 通过编号获取套餐信息
        /// </summary>
        /// <param name="packageID"></param>
        /// <returns></returns>
        public HISFC.Models.MedicalPackage.Package GetPackage(string packageID)
        {
            if (string.IsNullOrEmpty(packageID))
            {
                return null;
            }
            return this.packageManger.QueryPackageByID(packageID);
        }

        /// <summary>
        /// 按类型获取套餐
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public ArrayList GetPackageListByType(string Type)
        {
            if (string.IsNullOrEmpty(Type))
            {
                return null;
            }
            return this.packageManger.QueryPackageByType(Type);
        }

        ////{409E5137-13A9-4ee0-8AF7-01BCD3B09D36}
        public ArrayList QueryPackageByParentCode(string parentCode)
        {
            if (string.IsNullOrEmpty(parentCode))
            {
                return null;
            }
            return this.packageManger.QueryPackageByParentCode(parentCode);
        }

        public ArrayList QueryParentPackage(string parentCode)
        {
            if (string.IsNullOrEmpty(parentCode))
            {
                return null;
            }
            return this.packageManger.QueryParentPackage(parentCode);
        }

        /// <summary>
        /// 增加套餐
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public int AddPackage(HISFC.Models.MedicalPackage.Package package)
        {
            if (package == null)
            {
                return -1;
            }

            this.SetDB(this.packageManger);

            string packageID = string.Empty;
            packageID = this.packageManger.GetSequence("PackageManage.GetPackageID");

            if (packageID == null || string.IsNullOrEmpty(packageID))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.Err = this.packageManger.Err;
                return -1;
            }

            package.ID = packageID;
            package.CreateOper = FrameWork.Management.Connection.Operator.ID;
            package.CreateTime = this.GetDateTimeFromSysDateTime();
            package.ModifyOper = FrameWork.Management.Connection.Operator.ID;
            package.ModifyTime = this.GetDateTimeFromSysDateTime();

            int dbrtn = this.packageManger.Insert(package);

            if (dbrtn < 0)
            {
                this.Err = this.packageManger.Err;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 更新套餐
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public int UpdatePackage(HISFC.Models.MedicalPackage.Package package)
        {
            if (package == null || string.IsNullOrEmpty(package.ID))
            {
                return -1;
            }

            this.SetDB(this.packageManger);

            package.ModifyOper = FrameWork.Management.Connection.Operator.ID;
            package.ModifyTime = this.GetDateTimeFromSysDateTime();

            return this.packageManger.Update(package);

        }

        /// <summary>
        /// 删除套餐
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public int DeletePackage(HISFC.Models.MedicalPackage.Package package)
        {
            if (package == null || string.IsNullOrEmpty(package.ID))
            {
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            if (this.packageManger.Delete(package) == -1)
            {
                this.Err = this.packageManger.Err;
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }

            if (this.detailManager.DeleteByPackageID(package.ID) == -1)
            {
                this.Err = this.detailManager.Err;
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

        /// <summary>
        /// 设置套餐启用或禁用
        /// </summary>
        /// <param name="package"></param>
        /// <param name="valid"></param>
        /// <returns></returns>
        public int SetPackageValid(HISFC.Models.MedicalPackage.Package package, bool valid)
        {
            if (package == null || string.IsNullOrEmpty(package.ID))
            {
                return -1;
            }
            this.SetDB(this.packageManger);
            package.ModifyOper = FrameWork.Management.Connection.Operator.ID;
            package.ModifyTime = this.GetDateTimeFromSysDateTime();

            return this.packageManger.SetValidByID(package.ID, valid);
        }

        /// <summary>
        /// 通过套餐编号和序号获取套餐信息
        /// </summary>
        /// <param name="packageID"></param>
        /// <param name="sequenceNO"></param>
        /// <returns></returns>
        public HISFC.Models.MedicalPackage.PackageDetail GetPackageItem(string packageID, string sequenceNO)
        {
            if (string.IsNullOrEmpty(packageID) || string.IsNullOrEmpty(sequenceNO))
            {
                return null;
            }

            return this.detailManager.QueryPackageDetailByIDAndSeq(packageID, sequenceNO);
        }

        /// <summary>
        /// 根据套餐编号获取套餐明细
        /// </summary>
        /// <param name="PackageID"></param>
        /// <returns></returns>
        public ArrayList GetPackageItemByPackageID(string PackageID)
        {
            if (string.IsNullOrEmpty(PackageID))
            {
                return null;
            }

            return this.detailManager.QueryPackageDetailByPackageID(PackageID);
        }

        /// <summary>
        /// 增加套餐明细
        /// </summary>
        /// <param name="package"></param>
        /// <param name="packageDetail"></param>
        /// <returns></returns>
        public int AddPackageDetail(HISFC.Models.MedicalPackage.Package package, HISFC.Models.MedicalPackage.PackageDetail packageDetail)
        {
            if (package == null || string.IsNullOrEmpty(package.ID) || packageDetail == null)
            {
                return -1;
            }

            this.SetDB(this.detailManager);

            string seqno = string.Empty;
            seqno = this.detailManager.GetSequence("PackageDetailManage.GetSequenceNO");
            if (seqno == null || string.IsNullOrEmpty(seqno))
            {
                this.Err = this.detailManager.Err;
                return -1;
            }
            packageDetail.PackageID = package.ID;
            packageDetail.SequenceNO = seqno;
            packageDetail.ModifyOper = FrameWork.Management.Connection.Operator.ID;
            packageDetail.ModifyTime = this.GetDateTimeFromSysDateTime();
            packageDetail.CreateOper = FrameWork.Management.Connection.Operator.ID;
            packageDetail.CreateTime = this.GetDateTimeFromSysDateTime();

            int dbrtn = this.detailManager.Insert(packageDetail);

            if (dbrtn < 0)
            {
                this.Err = this.detailManager.Err;
                return -1;
            }

            return dbrtn;

        }

        /// <summary>
        /// 删除套餐明细
        /// </summary>
        /// <param name="package"></param>
        /// <param name="packageDetail"></param>
        /// <returns></returns>
        public int DeletePackageDetail(HISFC.Models.MedicalPackage.Package package, HISFC.Models.MedicalPackage.PackageDetail packageDetail)
        {
            if (package == null || 
                string.IsNullOrEmpty(package.ID) || 
                packageDetail == null || 
                string.IsNullOrEmpty(packageDetail.SequenceNO) ||
                packageDetail.PackageID != package.ID)
            {
                return -1;
            }

            return this.detailManager.Delete(packageDetail);
        }

        /// <summary>
        /// 更新套餐明细
        /// </summary>
        /// <param name="packageDetail"></param>
        /// <returns></returns>
        public int UpdatePackageDetail(HISFC.Models.MedicalPackage.PackageDetail packageDetail)
        {
            if (packageDetail == null ||
               string.IsNullOrEmpty(packageDetail.PackageID) ||
               string.IsNullOrEmpty(packageDetail.SequenceNO))
            {
                return -1;
            }

            this.SetDB(this.detailManager);
            packageDetail.ModifyOper = FrameWork.Management.Connection.Operator.ID;

            return this.detailManager.Update(packageDetail);
        }

        /// <summary>
        /// 根据套餐编号删除套餐明细
        /// </summary>
        /// <param name="packageID"></param>
        /// <returns></returns>
        public int DeletePackageDetail(string packageID)
        {
            if (string.IsNullOrEmpty(packageID))
            {
                return -1;
            }

            return this.detailManager.DeleteByPackageID(packageID);
        }

        /// <summary>
        /// 根据套餐查询套餐总金额
        /// </summary>
        /// <param name="packageID"></param>
        /// <returns></returns>
        public decimal QueryTotFeeByPackge(string packageID)
        {
            decimal TotFee = 0.0m;

            ArrayList detailList = this.GetPackageItemByPackageID(packageID);

            foreach (FS.HISFC.Models.MedicalPackage.PackageDetail detail in detailList)
            {
                decimal tmpQTY = detail.Item.Qty;
                if (detail.Item.SysClass.ID.ToString().Equals("P") ||
                    detail.Item.SysClass.ID.ToString().Equals("PCC") ||
                    detail.Item.SysClass.ID.ToString().Equals("PCZ"))
                {
                    detail.Item = itemIntegrate.GetItem(detail.Item.ID);
                }
                else
                {
                    detail.Item = itemMgr.GetUndrugByCode(detail.Item.ID);
                }

                detail.Item.Qty = tmpQTY;
                detail.Item.Price = Decimal.Parse(detail.Item.Price.ToString("F2"));

                if (detail.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    if (detail.UnitFlag.Equals("0"))
                    {
                        TotFee += Math.Round((detail.Item.Price / detail.Item.PackQty) * detail.Item.Qty, 2);
                    }
                    else
                    {
                        TotFee += detail.Item.Price * detail.Item.Qty;
                    }
                }
                else
                {
                    TotFee += detail.Item.Price * detail.Item.Qty;
                }
            }

            return TotFee;
        }

        //{3599D82C-0E7E-4628-8FC6-DDAAA1CC6335}
        public int ShiftPackageOwner(FS.HISFC.Models.RADT.PatientInfo PatientInfo, FS.HISFC.Models.RADT.PatientInfo PatientInfoNew, FS.HISFC.Models.MedicalPackage.Fee.Invoice invoiceInfo, string operCode)
        {
            if (PatientInfo == null || PatientInfoNew == null || invoiceInfo == null || operCode == null)
            {
                return 0;
            }
            //{0F7B065C-F9A2-48e7-A331-F9DC0EC64D3A}
            int i= this.packageManger2.ShiftPackageOwner(PatientInfo,PatientInfoNew,invoiceInfo,operCode);
            FS.HISFC.Models.Base.Employee employee= FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            string operName = employee.Name;
            int j=this.packageManger2.ShiftPackageOwnerOper(PatientInfo, PatientInfoNew, invoiceInfo, operCode, operName);

            return i;
        }

        public int ShiftPackageDetailOwner(FS.HISFC.Models.RADT.PatientInfo PatientInfo, FS.HISFC.Models.RADT.PatientInfo PatientInfoNew, FS.HISFC.Models.MedicalPackage.Fee.Invoice invoiceInfo, string operCode)
        {
            if (PatientInfo == null || PatientInfoNew == null || invoiceInfo == null || operCode == null)
            {
                return 0;
            }
            int i= this.detailManager2.ShiftPackageDetailOwner(PatientInfo, PatientInfoNew, invoiceInfo, operCode);
            FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            string operName = employee.Name;
            int j = this.detailManager2.ShiftPackageDetailOwnerOper(PatientInfo, PatientInfoNew, invoiceInfo, operCode, operName);

            return i;
        }

        #endregion

    }
}
