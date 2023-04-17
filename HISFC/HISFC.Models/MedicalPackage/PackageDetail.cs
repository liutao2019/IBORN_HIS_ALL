using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.MedicalPackage
{
    public class PackageDetail : FS.FrameWork.Models.NeuObject
    {
        /// <summary>
        /// 套餐编码
        /// </summary>
        private string packageID = string.Empty;

        /// <summary>
        /// 套餐内序列号
        /// </summary>
        private string sequenceNO = string.Empty;

        /// <summary>
        /// 单位
        /// </summary>
        private string unit = string.Empty;

        /// <summary>
        /// 0 - 最小单位，1 - 包装单位
        /// </summary>
        private string unitFlag = "0";

        /// <summary>
        /// 药品项目/非药品项目
        /// </summary>
        private FS.HISFC.Models.Base.Item item;

        /// <summary>
        /// 执行科室
        /// </summary>
        private FS.FrameWork.Models.NeuObject execDept = null;

        /// <summary>
        /// 创建人
        /// </summary>
        private OperEnvironment CreateInfo = new OperEnvironment();

        /// <summary>
        /// 修改人
        /// </summary>
        private OperEnvironment ModifyInfo = new OperEnvironment();

        /// <summary>
        /// 套餐编码
        /// </summary>
        public string PackageID
        {
            get { return this.packageID; }
            set { this.packageID = value; }
        }

        /// <summary>
        /// 套餐内序号
        /// </summary>
        public string SequenceNO
        {
            get { return this.sequenceNO; }
            set { this.sequenceNO = value; }
        }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit
        {
            get { return this.unit; }
            set { this.unit = value; }
        }

        /// <summary>
        /// 0 - 最小单位，1 - 包装单位
        /// </summary>
        public string UnitFlag
        {
            get { return this.unitFlag; }
            set { this.unitFlag = value; }
        }

        /// <summary>
        /// 药品项目/非药品项目
        /// </summary>
        public FS.HISFC.Models.Base.Item Item
        {
            get
            {
                if (item == null)
                {
                    item = new FS.HISFC.Models.Base.Item();
                }

                return this.item;
            }
            set
            {
                this.item = value;
            }
        }


        /// <summary>
        /// 执行科室
        /// </summary>
        public FS.FrameWork.Models.NeuObject ExecDept
        {
            get 
            {
                if (this.execDept == null)
                {
                    this.execDept = new FS.FrameWork.Models.NeuObject();
                }
                return this.execDept; 
            }
            set { this.execDept = value; }
        }


        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateOper
        {
            get
            {
                if (this.CreateInfo == null)
                {
                    this.CreateInfo = new OperEnvironment();
                }
                return this.CreateInfo.ID;
            }
            set
            {
                if (this.CreateInfo == null)
                {
                    this.CreateInfo = new OperEnvironment();
                }
                this.CreateInfo.ID = value;
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get
            {
                if (this.CreateInfo == null)
                {
                    this.CreateInfo = new OperEnvironment();
                }
                return this.CreateInfo.OperTime;
            }
            set
            {
                if (this.CreateInfo == null)
                {
                    this.CreateInfo = new OperEnvironment();
                }
                this.CreateInfo.OperTime = value;
            }
        }

        /// <summary>
        /// 修改人
        /// </summary>
        public string ModifyOper
        {
            get
            {
                if (this.ModifyInfo == null)
                {
                    this.ModifyInfo = new OperEnvironment();
                }
                return this.ModifyInfo.ID;
            }
            set
            {
                if (this.ModifyInfo == null)
                {
                    this.ModifyInfo = new OperEnvironment();
                }
                this.ModifyInfo.ID = value;
            }
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime
        {
            get
            {
                if (this.ModifyInfo == null)
                {
                    this.ModifyInfo = new OperEnvironment();
                }
                return this.ModifyInfo.OperTime;
            }
            set
            {
                if (this.ModifyInfo == null)
                {
                    this.ModifyInfo = new OperEnvironment();
                }
                this.ModifyInfo.OperTime = value;
            }
        }

        public new PackageDetail Clone()
        {
            PackageDetail packageDetail = new PackageDetail();
            packageDetail.PackageID = this.packageID;
            packageDetail.SequenceNO = this.SequenceNO;

            if (this.item is FS.HISFC.Models.Pharmacy.Item)
            {
                packageDetail.Item = new FS.HISFC.Models.Pharmacy.Item();
                (packageDetail.Item as FS.HISFC.Models.Pharmacy.Item).MinUnit = (this.item as FS.HISFC.Models.Pharmacy.Item).MinUnit;
                (packageDetail.Item as FS.HISFC.Models.Pharmacy.Item).PackUnit = (this.item as FS.HISFC.Models.Pharmacy.Item).PackUnit;
            }

            packageDetail.Item.ID = this.item.ID;
            packageDetail.Item.SysClass.ID = this.item.SysClass.ID;
            packageDetail.Item.Name = this.item.Name;
            packageDetail.Item.PackQty = this.item.PackQty;
            packageDetail.Item.Qty = this.item.Qty;
            packageDetail.Item.Price = this.item.Price;
            packageDetail.Item.PriceUnit = this.item.PriceUnit;
            packageDetail.Item.Specs = this.item.Specs;
            packageDetail.Item.ItemType = this.item.ItemType;
            packageDetail.unitFlag = this.unitFlag;
            packageDetail.Memo = this.Memo;
            packageDetail.unit = this.unit;
            packageDetail.ExecDept.ID = this.ExecDept.ID;
            packageDetail.ModifyOper = this.ModifyOper;
            packageDetail.ModifyTime = this.ModifyTime;
            packageDetail.CreateOper = this.CreateOper;
            packageDetail.CreateTime = this.CreateTime;
            return packageDetail;
        }
    }
}
