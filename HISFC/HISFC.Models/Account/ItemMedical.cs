using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Account
{
    public class ItemMedical : NeuObject
    {
        private string packageId = string.Empty;

        private string packageName = string.Empty;

        private decimal packageCost=0m;

        private string spellCode= string.Empty;

        private string inputCode = string.Empty;

        private string sortId = string.Empty;

        private string validState = string.Empty;

        private string memo = string.Empty;

        private OperEnvironment operEnvironment;
       	
        private OperEnvironment createEnvironment;

        public string PackageId
        {
            get { return packageId; }
            set { packageId = value; }
        }

        public string PackageName
        {
            get { return packageName; }
            set { packageName = value; }
        }

        public decimal PackageCost
        {
            get { return packageCost; }
            set { packageCost = value; }
        }


        public string SpellCode
        {
            get { return spellCode; }
            set { spellCode = value; }
        }

        public string InputCode
        {
            get { return inputCode; }
            set { inputCode = value; }
        }

        public string SortId
        {
            get { return sortId; }
            set { sortId = value; }
        }
        public string ValidState
        {
            get
            {
                return validState;
            }
            set
            {
                validState = value;
            }
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
    }
}
