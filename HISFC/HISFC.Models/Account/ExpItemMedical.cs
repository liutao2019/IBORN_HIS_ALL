using FS.FrameWork.Models;
using FS.HISFC.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.Models.Account
{
    public class ExpItemMedical : NeuObject
    {

        private string clinicCode = string.Empty;

        public string ClinicCode
        {
            get { return clinicCode; }
            set { clinicCode = value; }
        }

        private string cardNo = string.Empty;

        public string CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }

        private string patientName = string.Empty;

        public string PatientName
        {
            get { return patientName; }
            set { patientName = value; }
        }

        private string itemCode = string.Empty;

        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }


        private string itemName = string.Empty;

        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        private string itemSubcode = string.Empty;

        public string ItemSubcode
        {
            get { return itemSubcode; }
            set { itemSubcode = value; }
        }

        private string itemSubname = string.Empty;

        public string ItemSubname
        {
            get { return itemSubname; }
            set { itemSubname = value; }
        }

        private decimal unitPrice = 0.0m;
        public decimal UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; }
        }


        private int qty = 0;

        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }

        private int rtnQty = 0;
        public int RtnQty
        {
            get { return rtnQty; }
            set { rtnQty = value; }
        }

        private int confirmQty = 0;

        public int ConfirmQty
        {
            get { return confirmQty; }
            set { confirmQty = value; }
        }

        private decimal totPrice = 0.0m;

        public decimal TotPrice
        {
            get { return totPrice; }
            set { totPrice = value; }
        }

        private string memo = string.Empty;

        public string Memo
        {
            get { return memo; }
            set { memo = value; }
        }

        private string packageId = string.Empty;

        public string PackageId
        {
            get { return packageId; }
            set { packageId = value; }
        }

        private string packageName = string.Empty;

        public string PackageName
        {
            get { return packageName; }
            set { packageName = value; }
        }



        private string itemMedicalHeadNo  = string.Empty;

        public string ItemMedicalHeadNo
        {
            get { return itemMedicalHeadNo; }
            set { itemMedicalHeadNo = value; }
        }

        private string cancelFlag=  string.Empty;
        public string CancelFlag
        {
            get { return cancelFlag; }
            set { cancelFlag = value; }
        }

        private OperEnvironment cancelEnvironment;
        public OperEnvironment CancelEnvironment
        {
            get
            {
                if (cancelEnvironment == null)
                {
                    cancelEnvironment = new OperEnvironment();
                }
                return this.cancelEnvironment;
            }
            set
            {
                this.operEnvironment = value;
            }
        }




        private OperEnvironment operEnvironment;

        private OperEnvironment createEnvironment;

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
    }
}
