using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Account
{
    public class ItemMedicalDetail : NeuObject
    {
        private string packageId = string.Empty;

        private string medicalDetailId = string.Empty;

        private string sequenceNo = string.Empty;

        private string itemCode = string.Empty;

        private string itemName = string.Empty;

        private int itemNum = 0;

        private string itemSubcode = string.Empty;

        private string itemSubname = string.Empty;

        private decimal unitPrice = 0m;

        private string memo = string.Empty;

        private OperEnvironment operEnvironment;

        private OperEnvironment createEnvironment;

        private ItemMedical itemMediacl;
        public string PackageId
        {
            get { return packageId; }
            set { packageId = value; }
        }

        public string MedicalDetailId
        {
            get { return medicalDetailId; }
            set { medicalDetailId = value; }
        }

        public string SequenceNo
        {
            get { return sequenceNo; }
            set { sequenceNo = value; }
        }

        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }

        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        public int ItemNum
        {
            get { return itemNum; }
            set { itemNum = value; }
        }

        public string ItemSubcode
        {
            get { return itemSubcode; }
            set { itemSubcode = value; }
        }

        public string ItemSubname
        {
            get { return itemSubname; }
            set { itemSubname = value; }
        }

        public decimal UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; }
        }

        public string Memo
        {
            get { return memo; }
            set { memo = value; }
        }

        public OperEnvironment OperEnvironment
        {
            get
            {
                if (operEnvironment == null)
                {
                    operEnvironment = new OperEnvironment();
                }
                return this.operEnvironment;
            }
            set
            {
                this.operEnvironment = value;
            }
        }

        public OperEnvironment CreateEnvironment
        {
            get
            {
                if (createEnvironment == null)
                {
                    createEnvironment = new OperEnvironment();
                }
                return this.createEnvironment;
            }
            set
            {
                this.createEnvironment = value;
            }
        }

        public ItemMedical ItemMediacl
        {
            get
            {
                if (itemMediacl == null)
                {
                    itemMediacl = new ItemMedical();
                }
                return this.itemMediacl;
            }
            set
            {
                this.itemMediacl = value;
            }

        }
    }
}
