using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.DCP
{
    [System.Serializable]
    public class Patient : ProcessBase, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        #region �ӿڱ���

        private FS.SOC.HISFC.BizProcess.DCPInterface.IInpatient IInpatientInstance = null;

        private FS.SOC.HISFC.BizProcess.DCPInterface.IOutpatient IOutpatientInstance = null;

        #endregion

        /// <summary>
        /// ����סԺ������Ϣ��ȡ�ӿ�
        /// </summary>
        private void CreateInpatientInstance()
        {
            if (this.IInpatientInstance == null)
            {
                this.IInpatientInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(),
                    typeof(FS.SOC.HISFC.BizProcess.DCPInterface.IInpatient)) as FS.SOC.HISFC.BizProcess.DCPInterface.IInpatient;
            }

            if (this.IInpatientInstance == null)
            {
                throw new Exception(this.GetType().ToString() + "�еĽӿ�\n" + "FS.SOC.HISFC.BizProcess.DCPInterface.IInpatient" + "\nû��ά������,�������Ա��ϵ");
            }
        }

        private void CreateOutpatientInstance()
        {
            if (this.IOutpatientInstance == null)
            {
                this.IOutpatientInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(),
                    typeof(FS.SOC.HISFC.BizProcess.DCPInterface.IOutpatient)) as FS.SOC.HISFC.BizProcess.DCPInterface.IOutpatient;
            }

            if (this.IOutpatientInstance == null)
            {
                throw new Exception(this.GetType().ToString() + "�еĽӿ�\n" + "FS.SOC.HISFC.BizProcess.DCPInterface.IOutpatient" + "\nû��ά������,�������Ա��ϵ");
            }
        }


        #region ���ﻼ��

        /// <summary>
        /// ��ȡ���ﻼ��
        /// </summary>
        /// <param name="clinicNO"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Registration.Register GetByClinic(string clinicNO)
        {
            this.CreateOutpatientInstance();

            if (this.IOutpatientInstance != null)
            {
                return this.IOutpatientInstance.GetByClinic(clinicNO);
            }

            return null;
        }

        /// <summary>
        /// ��ȡ���ﻼ��
        /// </summary>
        /// <param name="patientName"></param>
        /// <returns></returns>
        public ArrayList QueryValidPatientsByName(string patientName)
        {
            this.CreateOutpatientInstance();

            if (this.IOutpatientInstance != null)
            {
                return this.IOutpatientInstance.QueryValidPatientsByName(patientName);
            }

            return null;
        }

        /// <summary>
        /// ��ȡ���ﻼ��
        /// </summary>
        /// <returns></returns>
        public ArrayList Query(string patientNO, DateTime date)
        {
            this.CreateOutpatientInstance();

            if (this.IOutpatientInstance != null)
            {
                return this.IOutpatientInstance.Query(patientNO,date);
            }

            return null;
        }
        /// <summary>
        /// ����ҽ����ѯ ���ﻼ��
        /// </summary>
        /// <param name="docID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isSee"></param>
        /// <returns></returns>
        public ArrayList QueryBySeeDoc(string docID, DateTime beginDate, DateTime endDate, bool isSee)
        {
            this.CreateOutpatientInstance();

            if (this.IOutpatientInstance != null)
            {
                return this.IOutpatientInstance.QueryBySeeDoc(docID, beginDate, endDate, isSee);
            }

            return null;
        }
        /// <summary>
        /// �ҺŲ�ѯ
        /// </summary>
        /// <param name="sql">����</param>
        /// <returns></returns>
        public ArrayList QueryRegister(string sql)
        {
            this.CreateOutpatientInstance();

            if (this.IOutpatientInstance != null)
            {
                return this.IOutpatientInstance.QueryRegister(sql);
            }

            return null;
        }
        #endregion

        #region סԺ����

        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="patientCardNO"></param>
        /// <returns></returns>
        public FS.HISFC.Models.RADT.Patient GetBasePatientInfomation(string patientCardNO)
        {
            this.CreateInpatientInstance();

            if (this.IInpatientInstance != null)
            {
                return this.IInpatientInstance.GetBasePatientInfomation(patientCardNO);
            }

            return null;
        }

        /// <summary>
        /// ��ȡסԺ������Ϣ
        /// </summary>
        /// <param name="patientNO"></param>
        /// <returns></returns>
        public ArrayList GetPatientInfoByPatientNOAll(string patientNO)
        {
            this.CreateInpatientInstance();

            if (this.IInpatientInstance != null)
            {
                return this.IInpatientInstance.GetPatientInfoByPatientNOAll(patientNO);
            }

            return null;
        }

        /// <summary>
        /// ��ȡסԺ������Ϣ
        /// </summary>
        /// <param name="patientName"></param>
        /// <returns></returns>
        public ArrayList GetPatientInfoByPatientName(string patientName)
        {
            this.CreateInpatientInstance();

            if (this.IInpatientInstance != null)
            {
                return this.IInpatientInstance.GetPatientInfoByPatientName(patientName);
            }

            return null;
        }

        /// <summary>
        /// ��ȡסԺ������Ϣ
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public ArrayList QueryPatientInfoBySqlWhere(string strWhere)
        {
            this.CreateInpatientInstance();

            if (this.IInpatientInstance != null)
            {
                return this.IInpatientInstance.QueryPatientInfoBySqlWhere(strWhere);
            }

            return null;
        }

        public FS.HISFC.Models.RADT.PatientInfo GetPatientInfomation(string inPatientNO)
        {
            this.CreateInpatientInstance();

            if (this.IInpatientInstance != null)
            {
                return this.IInpatientInstance.GetPatientInfomation(inPatientNO);
            }

            return null;
        }

        public ArrayList QueryPatientByDeptCode(string deptCode, FS.HISFC.Models.RADT.InStateEnumService instate)
        {
            this.CreateInpatientInstance();

            if (this.IInpatientInstance != null)
            {
                return this.IInpatientInstance.QueryPatientByDeptCode(deptCode, instate);
            }

            return null;
        }

        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] t = new Type[]{     
                    typeof(FS.SOC.HISFC.BizProcess.DCPInterface.IInpatient),
                    typeof(FS.SOC.HISFC.BizProcess.DCPInterface.IOutpatient)                                  
                                                };

                return t;
            }
        }

        #endregion
    }
}
