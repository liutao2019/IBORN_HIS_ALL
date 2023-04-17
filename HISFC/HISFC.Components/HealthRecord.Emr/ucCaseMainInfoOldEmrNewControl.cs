using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.HealthRecord.Emr
{
    public partial class ucCaseMainInfoOldEmrNewControl : FS.HISFC.Components.HealthRecord.CaseFirstPage.ucCaseMainInfo
        ,FS.FrameWork.WinForms.Forms.IInterfaceContainer,FS.Emr.RecordTpl.Controls.External.Interfaces.IInpatientRecordOperation,
        FS.Emr.RecordTpl.Controls.External.Interfaces.IThirdControl, FS.Emr.Print.IPrintable,
        FS.Emr.RecordTpl.Controls.External.Interfaces.IValueType,
        FS.Emr.RecordTpl.Controls.External.PropertyObjects.IImplementProperty
    {
        public ucCaseMainInfoOldEmrNewControl()
        {
            InitializeComponent();
        }


        #region IThirdControl 成员

        public string GetCName()
        {
            return "旧版首页4.5控件";
        }

        #endregion

        #region IInpatientRecordOperation 成员

        public void Init(long patientID)
        {
            string hisInpatientNo = FS.Emr.Patient.IBll.Facade.PatientFacadeFactory.CreateIprFacade().GetInpatientInfoByInpatientID(patientID).HisInpatientNo;
            base.LoadInfo(hisInpatientNo, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
        }

        public new void Load(long patientID, long recordID)
        {
            string hisInpatientNo = FS.Emr.Patient.IBll.Facade.PatientFacadeFactory.CreateIprFacade().GetInpatientInfoByInpatientID(patientID).HisInpatientNo;
            base.LoadInfo(hisInpatientNo, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
        }

        public void Refresh(long patientID, long recordID)
        {
            ;
        }

        public void Save(long inpatientID, long recordID)
        {
            //base.EmrSave(null, null);
            base.Save(null,null);
        }

        public event EventHandler ValueChange;

        public bool Verify(long inpatientID, long recordID, out string error)
        {
            error = string.Empty;
            return true;
        }

        #endregion

        #region IPrintable 成员

        public bool Print(FS.Emr.Print.PrintPaintEventArgs args)
        {
            base.PrintInterface();
            return false;
        }

        #endregion

        #region IValueType 成员


        public string GetFullName()
        {
            return "FS.HISFC.Components.HealthRecord.CaseFirstPage.ucCaseMainInfoEmrNewControl";
        }

        #endregion

        #region IImplementProperty 成员
        private FS.Emr.RecordTpl.Controls.External.PropertyObjects.Property property;
        public FS.Emr.RecordTpl.Controls.External.PropertyObjects.Property Property
        {
            get
            {
                if (property == null)
                {
                    property = new FS.Emr.RecordTpl.Controls.External.PropertyObjects.Property(this);
                }
                return property;
            }
            set
            {
                property = value;
            }
        }

        #endregion
    }
}
