using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Factory
{
    /// <summary>
    /// 入出转相关
    /// </summary>
    public abstract class RADTBase:FactoryBase
    {
        public virtual FS.HISFC.Models.RADT.PatientInfo QueryPatientInfoByInpatientNO(string inpatientNo)
        {
            FS.HISFC.BizLogic.RADT.InPatient manager = new FS.HISFC.BizLogic.RADT.InPatient();
            this.SetDB(manager);
            return manager.QueryPatientInfoByInpatientNO(inpatientNo);
        }
  
        public virtual  System.Collections.ArrayList  QueryInpatientNOByPatientNO(string patientNo, bool tr)
        {
            FS.HISFC.BizLogic.RADT.InPatient manager = new FS.HISFC.BizLogic.RADT.InPatient();
            this.SetDB(manager);
            return manager.QueryInpatientNOByPatientNO(patientNo, tr);        
        }
        public virtual System.Collections.ArrayList QueryInpatientNOByBedNO(string bedNo)
        {
            FS.HISFC.BizLogic.RADT.InPatient manager = new FS.HISFC.BizLogic.RADT.InPatient();
            this.SetDB(manager);
            return manager.QueryInpatientNOByBedNO(bedNo);
        }
        public virtual System.Collections .ArrayList  PatientQueryByPcNoRetArray(string str,string str1)
        {
            FS.HISFC.BizLogic.RADT.InPatient manager = new FS.HISFC.BizLogic.RADT.InPatient();
            this.SetDB(manager);
            return manager.PatientQueryByPcNoRetArray(str,str1);
        }
        public virtual System.Collections.ArrayList QueryPatientByEmpl(string emplCode,string deptCode)
        {
            FS.HISFC.BizLogic.RADT.InPatient manager = new FS.HISFC.BizLogic.RADT.InPatient();
            this.SetDB(manager);
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = emplCode;
            return manager.QueryHouseDocPatient(obj, FS.HISFC.Models.Base.EnumInState.I,deptCode);
        }

        public virtual System.Collections.ArrayList QueryPatientByDept(string deptCode)
        {
            FS.HISFC.BizLogic.RADT.InPatient manager = new FS.HISFC.BizLogic.RADT.InPatient();
            this.SetDB(manager);
            FS.HISFC.Models.RADT.InStateEnumService instate = new FS.HISFC.Models.RADT.InStateEnumService();
            instate.ID = "I";
            return manager.PatientQuery(deptCode,instate);
        }

        public virtual System.Collections.ArrayList QueryPatientByDept(string deptCode, int days)
        {
            FS.HISFC.BizLogic.RADT.InPatient manager = new FS.HISFC.BizLogic.RADT.InPatient();
            this.SetDB(manager);
            return manager.PatientQuery(deptCode, days);
        }

        public virtual System.Collections.ArrayList QueryPatientByDept(string deptCode, FS.HISFC.Models.RADT.InStateEnumService state)
        {
            FS.HISFC.BizLogic.RADT.InPatient manager = new FS.HISFC.BizLogic.RADT.InPatient();
            this.SetDB(manager);
            return manager.PatientQuery(deptCode, state);
        }

        public virtual System.Collections.ArrayList PatientInfoGet(string strWhere)
        {
            FS.HISFC.BizLogic.RADT.InPatient manager = new FS.HISFC.BizLogic.RADT.InPatient();
            this.SetDB(manager);
            return manager.PatientInfoGet(strWhere);
        }



        public virtual System.Collections.ArrayList QuereyPatientByDateAndState(DateTime dt1, DateTime dt2, FS.HISFC.Models.Base.EnumInState state)
        {
            FS.HISFC.BizLogic.RADT.InPatient manager = new FS.HISFC.BizLogic.RADT.InPatient();
            this.SetDB(manager);
            return manager.QueryPatientInfoByTimeInState(dt1, dt2, state.ToString());

        }
        public virtual System.Collections.ArrayList QuereyPatientByDate(DateTime dt1, DateTime dt2)
        {
            FS.HISFC.BizLogic.RADT.InPatient manager = new FS.HISFC.BizLogic.RADT.InPatient();
            this.SetDB(manager);
            return manager.QueryPatient(dt1, dt2);

        }
    }
}
