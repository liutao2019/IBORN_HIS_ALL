using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace UFC.Pharmacy.Base
{
    /// <summary>
    /// [��������: �������ҩƷά��]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-07]<br></br>
    /// </summary>
    public partial class ucManageDrugQuality : Neusoft.NFC.Interface.Controls.ucBaseControl,Neusoft.NFC.Interface.Forms.IMaintenanceControlable
    {
        public ucManageDrugQuality()
        {
            InitializeComponent();
        }

        #region IMaintenanceControlable ��Ա

        public int Add()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Copy()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Cut()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Delete()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Export()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Import()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Init()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public bool IsDirty
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public int Modify()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int NextRow()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Paste()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int PreRow()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Print()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int PrintConfig()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int PrintPreview()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Query()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public Neusoft.NFC.Interface.Forms.IMaintenanceForm QueryForm
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public int Save()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
