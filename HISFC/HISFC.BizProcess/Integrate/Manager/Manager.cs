using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.BizLogic.Manager;
using System.Collections;
using System.Text.RegularExpressions;
namespace FS.HISFC.BizProcess.Integrate
{
    /// <summary>
    /// [��������: ���ϵĹ�����]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class Manager : IntegrateBase
    {
        public  Manager()
        {
            
        }

        protected FS.HISFC.BizLogic.Manager.Constant managerConstant = new FS.HISFC.BizLogic.Manager.Constant();
        protected FS.HISFC.BizLogic.Manager.Department managerDepartment = new Department();
        protected FS.HISFC.BizLogic.Manager.Person manangerPerson = new Person();
        protected FS.HISFC.BizLogic.Manager.OrderType orderType = new OrderType( );
        protected FS.HISFC.BizLogic.Manager.Frequency managerFrequency = new Frequency();
        protected FS.HISFC.BizLogic.Manager.Bed managerBed = new Bed();
        protected FS.FrameWork.Management.ControlParam controler = new FS.FrameWork.Management.ControlParam();
        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Manager.ComGroupTail comGroupDetailManager = new ComGroupTail();
        /// <summary>
        /// ��ͬ��λ��ϵҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Manager.PactStatRelation pactStatRelationManager = new PactStatRelation();
        /// <summary>
        /// ��ͬ��λ����
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.PactUnitInfo pactUnitInfoManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
        /// <summary>
        /// ���
        /// </summary>
        protected FS.HISFC.BizLogic.Registration.Noon noonManager = new FS.HISFC.BizLogic.Registration.Noon();
        /// <summary>
        /// �Һ�ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Registration.Register regManager = new FS.HISFC.BizLogic.Registration.Register();
        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Nurse.Assign assignManager = new FS.HISFC.BizLogic.Nurse.Assign();
        protected FS.HISFC.BizLogic.Nurse.Room roomManager = new FS.HISFC.BizLogic.Nurse.Room();
        protected FS.HISFC.BizLogic.Nurse.Seat seatManager = new FS.HISFC.BizLogic.Nurse.Seat();
        #region addby xuewj 2010-11-2 ���ӽкŰ�ť{5A8B39E0-76A8-4e68-AF14-E2E0F45617D1}
        protected FS.HISFC.BizLogic.Nurse.Queue queueManager = new FS.HISFC.BizLogic.Nurse.Queue(); 
        #endregion
        protected FS.HISFC.BizLogic.Manager.UserPowerDetailManager userPowerDetailManager = new UserPowerDetailManager();

        protected FS.HISFC.BizLogic.Manager.DepartmentStatManager departStatManager = new DepartmentStatManager();

        //protected static FS.HISFC.BizLogic.Fee.UndrugComb undrugztManager = new FS.HISFC.BizLogic.Fee.UndrugComb();

        protected FS.HISFC.BizLogic.Fee.UndrugPackAge undrugPackageManager = new FS.HISFC.BizLogic.Fee.UndrugPackAge();

        /// <summary>
        /// סԺҵ��
        /// </summary>
        protected FS.HISFC.BizLogic.RADT.InPatient managerInpatient = new FS.HISFC.BizLogic.RADT.InPatient();
        /// <summary>
        /// �û��ı�
        /// </summary>
        protected FS.HISFC.BizLogic.Manager.UserText userTextManager = new UserText();
        protected FS.HISFC.BizLogic.Manager.Spell spellManager = new Spell();
        /// <summary>
        /// ����Trans
        /// </summary>
        /// <param name="trans"></param>
        public override void SetTrans(System.Data.IDbTransaction trans)
        {
            this.trans = trans;

            managerConstant.SetTrans(trans);
            managerDepartment.SetTrans(trans);
            manangerPerson.SetTrans(trans);
            orderType.SetTrans( trans );
            managerFrequency.SetTrans(trans);
            managerBed.SetTrans(trans);
            controler.SetTrans(trans);
            pactStatRelationManager.SetTrans(trans);
            comGroupDetailManager.SetTrans(trans);
            assignManager.SetTrans(trans);
            managerInpatient.SetTrans(trans);
            userTextManager.SetTrans(trans);
            spellManager.SetTrans(trans);
            undrugPackageManager.SetTrans(trans);
            userPowerDetailManager.SetTrans(trans);
        }

        #region ��ͬ��λ��ϵ

        /// <summary>
        /// ͨ����ͬ��λ�����ú�ͬ��λ��ϵ
        /// </summary>
        /// <param name="pactCode">��ͬ��λ����</param>
        /// <returns>�ɹ� : ���غ�ͬ��λ��ϵ���� ʧ�� null</returns>
        public ArrayList QueryRelationsByPactCode(string pactCode) 
        {
            return pactStatRelationManager.GetRelationByPactCode(pactCode);
        }
        /// <summary>
        /// ������к�ͬ��λ��Ϣ
        /// </summary>
        /// <returns>�ɹ�: ��ͬ��λ���� ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
        public ArrayList QueryPactUnitAll()
        {
            this.SetDB(pactUnitInfoManager);
            return pactUnitInfoManager.QueryPactUnitAll();
        }
        /// <summary>
        /// ��������ͬ��λ��Ϣ
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryPactUnitOutPatient()
        {
            this.SetDB(pactUnitInfoManager);
            return pactUnitInfoManager.QueryPactUnitOutPatient();
        }
        /// <summary>
        /// ���סԺ��ͬ��λ��Ϣ
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryPactUnitInPatient()
        {
            this.SetDB(pactUnitInfoManager);
            return pactUnitInfoManager.QueryPactUnitInPatient();
        }
        /// <summary>
        /// ���ݼ���ģ����ѯȡ��ͬ��λ��Ϣ
        /// </summary>
        /// <param name="shortName">����</param>
        /// <returns>�ɹ�: ��ͬ��λ���� ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
        public ArrayList QueryPactUnitByShortNameLiked(string shortName)
        {
            this.SetDB(pactUnitInfoManager);
            return pactUnitInfoManager.QueryPactUnitByShortNameLiked(shortName);
        }
        /// <summary>
        /// ���ݽ������ȡ��ͬ��λ
        /// </summary>
        /// <param name="payKindCode">����������</param>
        /// <returns>�ɹ�: ��ͬ��λ���� ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
        public ArrayList QueryPactUnitByPayKindCode(string payKindCode)
        {
            this.SetDB(pactUnitInfoManager);
            return pactUnitInfoManager.QueryPactUnitByPayKindCode(payKindCode);
        }
        /// <summary>
		/// ���ݺ�ͬ�����ѯ
		/// </summary>
        /// <param name="pactCode">��ͬ��λ����</param>
		/// <returns>�ɹ� ��ͬ��λʵ�� ʧ�� Null</returns>
        public FS.HISFC.Models.Base.PactInfo GetPactUnitInfoByPactCode(string pactCode)
        {
            this.SetDB(pactUnitInfoManager);
            return pactUnitInfoManager.GetPactUnitInfoByPactCode(pactCode);
        }
        //�޸Ľ�IsDrug(�Ƿ�ҩƷ)��Bool��Ϊö��EnumItemType���� Drug:ҩƷ Undrug:��ҩƷ MatItem����
        /// <summary>
		/// ���ݺ�ͬ��λ����Ŀ����õ���Ŀ�۸�
		/// </summary>
		/// <param name="patient"></param>
		/// <param name="IsDrug"></param>
		/// <param name="ItemID"></param>
		/// <param name="Price"></param>
		/// <returns></returns>
        [Obsolete("ͣ�ô˷�������ΪIntergrate.Fee����", true)]
        public int GetPrice(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Base.EnumItemType IsDrug, string ItemID, ref decimal Price)
        {
            this.SetDB(pactUnitInfoManager);

            return -1;// pactUnitInfoManager.GetPrice(patient, IsDrug, ItemID, ref Price);
        }
        #endregion

        #region ����

        

        /// <summary>
        /// ��ó���
        /// </summary>
        /// <returns></returns>
        public  ArrayList GetConstantList(FS.HISFC.Models.Base.EnumConstant constant)
        {
            this.SetDB(managerConstant);
            return managerConstant.GetList(constant);
        }

        /// <summary>
        /// ��ȡ�˻����� 
        ///{F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        /// <returns></returns>
        public ArrayList GetAccountTypeList()
        {
            return this.GetAccountTypeList("ALL");
        }

        /// <summary>
        /// �������ͻ�ȡ�˻�����
        /// {ECECDF2F-BA74-4615-A240-C442BE0A0074}
        /// </summary>
        /// <returns></returns>
        public ArrayList GetAccountTypeList(string Type)
        {
            this.SetDB(managerConstant);
            return managerConstant.GetAccountTypeList(Type);
        }


        /// <summary>
        /// ��������ó����б�
        /// </summary>
        /// <param name="type">�������</param>
        /// <returns></returns>
        public ArrayList GetConstantList(string type) 
        {
            this.SetDB(managerConstant);
            return managerConstant.GetList(type);
        }

        /// <summary>
		/// ��ó����е�һ��ʵ��
		/// </summary>
		/// <param name="type"></param>
		/// <param name="ID"></param>
		/// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetConstant(string type, string ID)
        {
            this.SetDB(managerConstant);
            return managerConstant.GetConstant(type, ID);
        }
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="constant"></param>
        /// <returns></returns>
        public ArrayList QueryConstantList(string constant)
        {
            this.SetDB(managerConstant);
            return managerConstant.GetList(constant);
        }

        /// <summary>
        /// ���һ������ʵ��
        /// </summary>
        /// <param name="type">��������</param>
        /// <param name="ID">��Ŀ����</param>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetConstansObj(string type, string ID) 
        {
            this.SetDB(managerConstant);

            return managerConstant.GetConstant(type, ID);
        }

        /// <summary>
        /// ���볣����Ϣ
        /// </summary>
        /// <param name="type">�������</param>
        /// <param name="constant">����ʵ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int InsertConstant(string type, FS.HISFC.Models.Base.Const constant) 
        {
            this.SetDB(managerConstant);

            return managerConstant.InsertItem(type, constant);
        }

        /// <summary>
        /// ���³�����Ϣ
        /// </summary>
        /// <param name="type">�������</param>
        /// <param name="constant">����ʵ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int UpdateConstant(string type, FS.HISFC.Models.Base.Const constant)
        {
            this.SetDB(managerConstant);

            return managerConstant.UpdateItem(type, constant);
        }

        #endregion

        #region ����
        /// <summary>
        /// ���ݴ���������ͻ�ÿ����б�
        /// </summary>
        /// <param name="type">������</param>
        /// <returns></returns>
        public ArrayList GetDeptmentByType(string type)
        {
            this.SetDB(managerDepartment);
            return managerDepartment.GetDeptmentByType(type);
        }
        /// <summary>
        /// ��ÿ����б�
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ArrayList GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType type)
        {
            this.SetDB(managerDepartment);
            return managerDepartment.GetDeptment(type);
        }
        public ArrayList GetDeptmentIn(FS.HISFC.Models.Base.EnumDepartmentType Type)
        {
            SetDB(managerDepartment);
            return managerDepartment.GetDeptmentIn(Type);
        }

        /// <summary>
        /// ��ȡ�Һſ����б�
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryRegDepartment()
        {
            this.SetDB(managerDepartment);
            return managerDepartment.GetRegDepartment();
        }
        /// <summary>
        /// ͨ�����ұ����ÿ�����Ϣ
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <returns>�ɹ�: ������Ϣ ʧ��: null</returns>
        public FS.HISFC.Models.Base.Department GetDepartment(string deptCode) 
        {
            this.SetDB(managerDepartment);

            return managerDepartment.GetDeptmentById(deptCode);
        }

        /// <summary>
        /// ���ȫ�������б�
        /// </summary>
        /// <returns></returns>
        public ArrayList GetDepartment()
        {
            this.SetDB(managerDepartment);
            return managerDepartment.GetDeptmentAll();
        }

        /// <summary>
        /// �����Ժ�����б�
        /// </summary>
        /// <param name="isInHos"></param>
        /// <returns></returns>
        public ArrayList QueryDeptmentsInHos(bool isInHos) 
        {
            this.SetDB(managerDepartment);

            return managerDepartment.GetInHosDepartment(isInHos);
        }

        /// <summary>
		///  
		/// ����������õĿ���
		/// </summary>
		/// <returns></returns>
        public ArrayList GetDeptmentAllValid()
        {
            this.SetDB(managerDepartment);

            return managerDepartment.GetDeptmentAll();
        }
        /// <summary>
        /// ��ѯ���������Ŀ���
        /// </summary>
        /// <param name="nurseCode"></param>
        /// <returns></returns>
        public ArrayList QueryDepartment(string nurseCode)
        {
            this.SetDB(managerDepartment);
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = nurseCode;
            return managerDepartment.GetDeptFromNurseStation(obj);
        }

        /// <summary>
        /// ��ѯ���������ķ������
        /// </summary>
        /// <param name="nurseCode"></param>
        /// <returns></returns>
        public ArrayList QueryDepartmentForArray(string nurseCode)
        {
            this.SetDB(managerDepartment);
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = nurseCode;
            return managerDepartment.GetDeptFromNurseStationForArray(obj);
        }

        #endregion

        #region ��Ա

        /// <summary>
        /// ��ȡӵ��ָ�����ҵ�¼Ȩ�޵���Ա�б�
        /// </summary>
        /// <param name="deptcode"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ArrayList GetEmployeeHasAccLoinByDept(string deptcode, FS.HISFC.Models.Base.EnumEmployeeType type)
        {
            this.SetDB(manangerPerson);
            return manangerPerson.GetEmployeeHasAccLoinByDept(deptcode, type);
        }


        /// <summary>
        /// ��ȡ������Ա�б� {0a849cd8-db12-48e0-97ff-0b34f287c0a0}
        /// </summary>
        /// <returns></returns>
        public ArrayList GetDeliverEmployee()
        {
            this.SetDB(manangerPerson);
            return manangerPerson.GetDeliverEmployee();
        
        }

        /// <summary>
        /// ������Ա���ͻ����Ա�б�
        /// </summary>
        /// <param name="emplType">��Ա����ö��</param>
        /// <returns>��Ա�б�</returns>
        public ArrayList QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType emplType) 
        {
            this.SetDB(manangerPerson);

            return manangerPerson.GetEmployee(emplType);
        }


           /// <summary>
        /// ������Ա���ͻ����Ա�б�,����������{BF4583B0-B5C7-490e-8AB3-1B6708E7A162}
        /// </summary>
        /// <param name="emplType">��Ա����ö��</param>
        /// <returns>��Ա�б�</returns>
        public ArrayList QueryEmployee4(FS.HISFC.Models.Base.EnumEmployeeType emplType) 
        {
            this.SetDB(manangerPerson);

            return manangerPerson.GetEmployee4(emplType);
        }
        /// <summary>
        /// ���ݿ��ұ���ȡ��Ա�б�
        /// </summary>
        /// <param name="deptID">���ұ���</param>
        /// <returns></returns>
        public ArrayList QueryEmployeeByDeptID(string deptID)
        {
            this.SetDB(manangerPerson);

            return manangerPerson.GetPersonsByDeptID(deptID);
             
        }
        /// <summary>
        /// �����Ա�б�
        /// </summary>
        /// <param name="emplType"></param>
        /// <param name="deptcode"></param>
        /// <returns></returns>
        public ArrayList QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType emplType,string deptcode)
        {
            this.SetDB(manangerPerson);

            return manangerPerson.GetEmployee(emplType,deptcode);
        }
        /// <summary>
        /// ����Ű�ר�ҵ���Ա�б�
        /// </summary>
        /// <param name="emplType"></param>
        /// <param name="deptcode"></param>
        /// <returns></returns>
        public ArrayList QueryEmployeeForScama(FS.HISFC.Models.Base.EnumEmployeeType emplType, string deptcode)
        {
            this.SetDB(manangerPerson);
            return manangerPerson.GetEmployeeForScama(emplType, deptcode);
 
        }
        /// <summary>
        /// ���ȫ����Ա�б�
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryEmployeeAll( )
        {
            this.SetDB( manangerPerson );

            return manangerPerson.GetEmployeeAll( );
        }

        /// <summary>
        /// ������ԱID��ȡ��Ա��Ϣ
        /// </summary>
        /// <param name="emplID">��Աid</param>
        /// <returns>��Ա��Ϣ</returns>
        public FS.HISFC.Models.Base.Employee  GetEmployeeInfo(string emplID)
        {
            this.SetDB(manangerPerson);
            return manangerPerson.GetPersonByID(emplID);
        }

        /// <summary>
        /// �������֤�Ż�ȡ��Ա��Ϣ
        /// </summary>
        /// <param name="idenNo">���֤��</param>
        /// <returns>��Ա��Ϣ</returns>
        public FS.HISFC.Models.Base.Employee GetEmployeeInfoByIendNo(string idenNo)
        {
            this.SetDB(manangerPerson);
            return manangerPerson.GetPersonByIdenNo(idenNo);
        }

        /// <summary>
        /// ��û�ʿ�б�
        /// </summary>
        /// <param name="nurseCode"></param>
        /// <returns></returns>
        public ArrayList QueryNurse(string nurseCode)
        {
            this.SetDB(manangerPerson);
            return manangerPerson.GetNurse(nurseCode);
        }

        /// <summary>
        /// ��÷ǻ�ʿ��Ա�б�
        /// </summary>
        /// <param name="deptID">���ұ���</param>
        /// <returns>��Ա�б�</returns>
        public ArrayList QueryEmployeeExceptNurse(string deptID)
        {
            this.SetDB( manangerPerson );

            return manangerPerson.GetAllButNurse( deptID );
        }
        #endregion

        #region ҽ������
        /// <summary>
        /// ��ȡҽ�������б�
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryOrderTypeList( )
        {
            this.SetDB( orderType );
            return orderType.GetList( );
        }
        #endregion

        #region ҽ��Ƶ��
        /// <summary>
        /// ��ѯҽ��Ƶ��
        /// </summary>
        /// <returns></returns>
        public ArrayList QuereyFrequencyList()
        {
            this.SetDB( managerFrequency );
            return managerFrequency.GetAll("Root");
        }

        /// <summary>
        /// �������Ƶ��
        /// </summary>
        /// <returns></returns>
        public FS.HISFC.Models.Order.Frequency QuereySpecialFrequencyList(string orderID,string comboNO)
        {
            this.SetDB(managerFrequency);
            return managerFrequency.GetDfqspecial(orderID, comboNO);
        }
        #endregion

        #region ����
        /// <summary>
        /// ��ò����б�
        /// </summary>
        /// <param name="nurseCode"></param>
        /// <returns></returns>
        public ArrayList QueryBedList(string nurseCode)
        {
            this.SetDB(managerBed);

            return managerBed.GetBedList(nurseCode);
        }

        /// <summary>
        /// ��ò����մ���Ϣ
        /// </summary>
        /// <param name="nurseCode"></param>
        /// <returns></returns>
        public ArrayList QueryUnoccupiedBed(string nurseCode)
        {
            this.SetDB(managerBed);

            return managerBed.GetUnoccupiedBed(nurseCode);
        }

        /// <summary>
        /// ��ò�����Ϣ
        /// </summary>
        /// <param name="bedNo"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.Bed GetBed(string bedNo)
        {
            this.SetDB(managerBed);

            return managerBed.GetBedInfo(bedNo);
        }

        /// <summary>
        /// ���ò�����Ϣ
        /// </summary>
        /// <param name="bed"></param>
        /// <returns></returns>
        public int SetBed(FS.HISFC.Models.Base.Bed bed)
        {
            this.SetDB(managerBed);

            return managerBed.SetBedInfo(bed);
        }

        /// <summary>
        /// ɾ��������Ϣ
        /// </summary>
        /// <param name="bedNo"></param>
        /// <returns></returns>
        public int DeleteBed(string bedNo)
        {
            this.SetDB(managerBed);

            return managerBed.DeleteBedInfo(bedNo);
        }


        /// <summary>
        /// ��û�����
        /// </summary>
        /// <param name="nurseCode"></param>
        /// <returns></returns>
        public ArrayList QueryBedNurseTendGroupList(string nurseCode)
        {
            this.SetDB(managerBed);

            return managerBed.GetBedNurseTendGroupList(nurseCode);
        }

        /// <summary>
        /// ���»�����
        /// </summary>
        /// <param name="bedNo"></param>
        /// <param name="nurseTendGroup"></param>
        /// <returns></returns>
        public int UpdateNurseTendGroup(string bedNo,string nurseTendGroup)
        {
            this.SetDB(managerBed);

            return managerBed.UpdateNurseTendGroup(bedNo, nurseTendGroup);
        }
        #endregion

        #region ����Controler

        /// <summary>
        /// ���ݿ������������������͵�ֵ
        /// </summary>
        /// <param name="ControlerCode"></param>
        /// <returns></returns>
        public string QueryControlerInfo(string ControlerCode)
        {
            this.SetDB(controler);
            return controler.QueryControlerInfo(ControlerCode);
        }

        /// <summary>
        /// ���ݿ������������������͵�ֵ
        /// </summary>
        /// <param name="ControlerKind"></param>
        /// <returns></returns>
        public ArrayList QueryControlerInfoByKind(string ControlerKind)
        {
            this.SetDB(controler);
            return controler.QueryControlInfoByKind(ControlerKind);
        }

        /// <summary>
        /// ���볣����Ϣ
        /// </summary>
        /// <param name="c">����ʵ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int InsertControlerInfo(FS.HISFC.Models.Base.ControlParam c) 
        {
            this.SetDB(controler);

            return controler.AddControlerInfo(c);
        }

        /// <summary>
        /// ���³�����Ϣ 
        /// </summary>
        /// <param name="c">����ʵ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int UpdateControlerInfo(FS.HISFC.Models.Base.ControlParam c)
        {
            this.SetDB(controler);

            return controler.UpdateControlerInfo(c);
        }



        #endregion

        #region ����

        /// <summary>
        /// ͨ�����ױ�����������ϸ��Ŀ����
        /// </summary>
        /// <param name="groupCode">���ױ���</param>
        /// <returns>�ɹ�������ϸ��Ŀ���� ʧ�� null </returns>
        public ArrayList QueryGroupDetailByGroupCode(string groupCode) 
        {
            this.SetDB(comGroupDetailManager);

            return comGroupDetailManager.GetComGroupTailByGroupID(groupCode);
        }

        public ArrayList GetValidGroupList(string deptID)
        {
            FS.HISFC.BizLogic.Manager.ComGroup groupManager = new ComGroup();
            this.SetDB( groupManager );

            return groupManager.GetValidGroupList( deptID );
        }

         /// <summary>
        /// ���ݿ��һ�ȡ��������{9F3CF1C0-AF96-4d17-96B1-6B34636A42A7}
        /// </summary>
        /// <param name="GroupKind">0 �����ã�1������,ALL ȫ��</param>
        /// <returns></returns>
        public ArrayList GetValidGroupListByRoot(string deptCode)
        {
            FS.HISFC.BizLogic.Manager.ComGroup groupManager = new ComGroup();
            this.SetDB(groupManager);

            return groupManager.GetValidGroupListByRoot(deptCode);
        }

         /// <summary>
        /// ��ȡ��������{9F3CF1C0-AF96-4d17-96B1-6B34636A42A7}
        /// </summary>
        /// <param name="GroupKind">0 �����ã�1������,ALL ȫ��</param>
        /// <returns></returns>
        public ArrayList GetGroupsByDeptParent(string GroupKind, string deptCode, string parentGroupID)
        {
            FS.HISFC.BizLogic.Manager.ComGroup groupManager = new ComGroup();
            this.SetDB(groupManager);

            return groupManager.GetGroupsByDeptParent(GroupKind, deptCode, parentGroupID);
        }
        #endregion

        #region ����

        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="roomID">��̨����</param>
        /// <param name="state">״̬ 1.���ﻼ��   2.���ﻼ��</param>
        /// <param name="doctID">ҽ������</param>
        /// <returns>����ʵ������</returns>
        public ArrayList QueryPatient(DateTime beginTime, DateTime endTime, string roomID, String state, string doctID)
        {
            this.SetDB(assignManager);
            return assignManager.QueryPatient(beginTime, endTime, roomID, state, doctID);
        }

          /// <summary>
        /// ����״̬��ѯ���ﻼ��
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="consoleID">��̨����</param>
        /// <param name="state">״̬ 1.���ﻼ��   2.���ﻼ��</param>
        /// <returns>ArrayList (����ʵ������)</returns>
        public ArrayList QueryAssignPatientByState(DateTime beginTime, DateTime endTime, string consoleID, String state, string doctID)
        {
            this.SetDB(assignManager);
            return assignManager.QueryAssignPatientByState(beginTime, endTime, consoleID, state, doctID);
        }

        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        /// <param name="deptID">���Ҵ���</param>
        /// <param name="roomID">��̨����</param>
        /// <returns>����ʵ������</returns>
        public ArrayList QueryPatient(string deptID, string roomID)
        {
            this.SetDB(assignManager);
            return assignManager.Query(deptID, roomID);
        }

        /// <summary>
        /// �������Һ����ȡ��̨
        /// </summary>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        public ArrayList QuerySeatByRoomNo(string roomNo)
        {
            this.SetDB(seatManager);
            return seatManager.QueryValid(roomNo);
        }

        /// <summary>
        /// ���ݿ��һ�ȡ�����б�
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public ArrayList QueryRoomByDeptID(string deptID)
        {
            this.SetDB(roomManager);
            return roomManager.GetRoomInfoByNurseNoValid(deptID);
        }

        /// <summary>
        /// ���ݿ��һ�ȡ����վ
        /// </summary>
        /// <param name="objDept"></param>
        /// <returns></returns>
        public ArrayList QueryNurseStationByDept(FS.FrameWork.Models.NeuObject objDept)
        {
            this.SetDB(managerDepartment);
            return managerDepartment.GetNurseStationFromDept(objDept);
        }
       /// <summary>
        /// ���ݸ��ݿ��ң��������ȡ����վ
       /// </summary>
       /// <param name="objDept">����</param>
       /// <param name="MyStatCode">������</param>
       /// <returns></returns>
        public ArrayList QueryNurseStationByDept(FS.FrameWork.Models.NeuObject objDept,string MyStatCode)
        {
            this.SetDB(managerDepartment);
            return managerDepartment.GetNurseStationFromDept(objDept, MyStatCode);
        }

        /// <summary>
        /// ���(����ҽ��ֱ�ӵ�����ר��)
        /// </summary>
        /// <param name="consoleCode">��̨����</param>
        /// <param name="clinicID">������ˮ��</param>
        /// <param name="outDate">�������</param>
        /// <param name="doctID">ҽ������</param>
        /// <returns></returns>
        public int UpdateAssign(string consoleCode, string clinicID, DateTime outDate, string doctID)
        {
            this.SetDB(assignManager);
            return assignManager.Update(consoleCode, clinicID, outDate, doctID);
        }

        /// <summary>
        /// ���(����ҽ������ҽ����ר��)
        /// </summary>
        /// <param name="consoleCode">��̨����</param>
        /// <param name="clinicID">������ˮ��</param>
        /// <param name="outDate">�������</param>
        /// <param name="doctID">ҽ������</param>
        /// <returns></returns>
        public int UpdateAssignSaved(string consoleCode, string clinicID, DateTime outDate, string doctID)
        {
            this.SetDB(assignManager);
            return assignManager.UpdateSaved(consoleCode, clinicID, outDate, doctID);
        }


        public ArrayList QueryFZDept()
        {
            this.SetDB(departStatManager);
            return departStatManager.LoadDepartmentStat("14");
        }

        /// <summary>
        /// ����������ˮ�ţ������־��ȡһ��Ψһ������Ϣ
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Nurse.Assign QueryAssignByClinicID(string clinicCode)
        {
            this.SetDB(assignManager);
            return assignManager.QueryByClinicID(clinicCode);
        }

        /// <summary>
        /// Ϊ�Һ�ʹ�õ��Զ�����
        /// </summary>
        /// <param name="register">�Һ�ʵ��</param>
        /// <param name="trigeWhereFlag">������0������1������</param>
        /// <param name="seeSequence"></param>
        /// <param name="isUseBookNum"></param>
        /// <returns></returns>
        public int TriageForRegistration(FS.HISFC.Models.Registration.Register register, string trigeWhereFlag, int seeSequence, bool isUseBookNum)
        {
            //1��׼������
            FS.HISFC.Models.Nurse.Assign assign = new FS.HISFC.Models.Nurse.Assign();
            FS.HISFC.Models.Nurse.Queue queue = new FS.HISFC.Models.Nurse.Queue();

            DateTime dtCurr = assignManager.GetDateTimeFromSysDateTime();

            string noonID = this.noonManager.getNoon(dtCurr).ID;

            string doctID = register.DoctorInfo.Templet.Doct.ID;

            int seeNO = 0;

            queue = this.queueManager.GetQueueByDoct(doctID, dtCurr.Date, noonID);

            if (queue == null || string.IsNullOrEmpty(queue.ID))
            {
                this.Err = "��ҽ��û�ж�Ӧ�ķ�����У�";
                return -1;
            }
            //seeNO = this.assignManager.Query(queue);

            //seeNO = seeNO + 1;

            seeNO = register.DoctorInfo.SeeNO;

            #region ��ֵ�����ɷ���ʵ��

            assign.Register = register;

            assign.Queue = queue;

            if (trigeWhereFlag == "0")
            {
                assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Triage;
            }
            else if (trigeWhereFlag == "1")
            {
                assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.In;
            }

            assign.TriageDept = queue.Dept.ID;
            assign.TirageTime = dtCurr;
            assign.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;
            assign.Oper.OperTime = dtCurr;

            assign.SeeNO = seeNO;

            if (isUseBookNum)
            {
                if (seeSequence == 0)
                {
                    assign.SeeNO = seeNO;
                }
                else
                {
                    assign.SeeNO = seeSequence;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(queue.Memo))
                {
                    string[] bookNum = queue.Memo.Split('|');

                    for (int i = 0; i < bookNum.Length; i++)
                    {
                        if (seeNO.ToString().Contains(bookNum[i]))
                        {
                            assign.SeeNO = seeNO + 1;
                        }
                    }
                }
            }
            #endregion
            //2�������������
            if (this.assignManager.Insert(assign) == -1)
            {
                this.Err = this.assignManager.Err;
                return -1;
            }

            //3�����¹Һ���Ϣ���÷����־

            if (this.regManager.Update(register.ID, FS.FrameWork.Management.Connection.Operator.ID, dtCurr) == -1)
            {
                this.Err = this.regManager.Err;
                return -1;
            }
            //4.������������1
            if (this.assignManager.UpdateQueue(assign.Queue.ID, "1") == -1)
            {
                this.Err = this.assignManager.Err;
                return -1;
            }

            return 1;
        }

        #region addby xuewj 2010-11-2 ���ӽкŰ�ť{5A8B39E0-76A8-4e68-AF14-E2E0F45617D1}
        /// <summary>
        /// ��ѯ��ǰʱ��,��ǰ�����е����Ƚ���ĺ�����Ϣ
        /// </summary>
        /// <param name="queueCode"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Nurse.Assign QueryWait(string queueCode, DateTime begin, DateTime end)
        {
            this.SetDB(assignManager);
            return assignManager.QueryWait(queueCode, begin, end);
        }

        /// <summary>
        /// ������̨������ڶ������ڲ�ѯ����ID
        /// </summary>
        ///  
        /// <param name="consoleCode">��̨</param>
        /// <param name="noonID">���</param>
        /// <param name="queueDate">����ʱ��</param>
        /// <returns>false��ȡsql��������̨�ѱ�ʹ�� true ��û�б�ʹ��</returns>
        public string QueryQueueID(string consoleCode, string noonID, string queueDate)
        {
            this.SetDB(queueManager);
            return queueManager.QueryQueueID(consoleCode, noonID, queueDate);
        }

        /// <summary>
        /// ������(���±�־������ʱ��)
        /// </summary>
        /// <param name="room"></param>
        /// <param name="inDate"></param>
        /// <returns></returns>
        public int Update(string clinicID, FS.FrameWork.Models.NeuObject room, FS.FrameWork.Models.NeuObject console, DateTime inDate)
        {
            this.SetDB(assignManager);
            return assignManager.Update(clinicID, room, console, inDate);
        }

        #endregion

        #endregion

        #region ������Ŀ

        /// <summary>
        /// ͨ��������Ŀ�����ѯ��ϸ��Ŀ
        /// </summary>
        /// <param name="combCode"></param>
        /// <returns></returns>
        [Obsolete("����,������Ŀ�ѹ鲢����ҩƷ", true)]
        public ArrayList QueryUndrugztDetailByCode(string combCode)
        {
            ArrayList list = new ArrayList();
            return list;
        }

        /// <summary>
        /// ͨ��������Ŀ�����ѯ��ϸ��Ŀ
        /// </summary>
        /// <param name="combCode"></param>
        /// <returns></returns>
        public ArrayList QueryUndrugPackageDetailByCode(string combCode)
        {
            this.SetDB(undrugPackageManager);
            return undrugPackageManager.QueryUndrugPackagesBypackageCode(combCode);
        }

        #endregion

        #region סԺ
        /// <summary>
        /// �����￨�Ų�ѯ����
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public ArrayList QueryPatientInfoByCardNO(string cardNO)
        {
            this.SetDB(managerInpatient);
            return managerInpatient.GetPatientInfoByCardNO(cardNO);
        }

        /// <summary>
		/// ���߻�����Ϣ��ѯ  com_patientinfo
		/// </summary>
		/// <param name="cardNO">����</param>
		/// <returns></returns>
        public FS.HISFC.Models.RADT.PatientInfo QueryComPatientInfo(string cardNO)
        {
            return managerInpatient.QueryComPatientInfo(cardNO);
        }

        /// <summary>
        /// ���߻�����Ϣ��ѯ  {F55EE363-24DB-4a01-B540-49761A5ADBC6}
        /// </summary>
        /// <param name="cardNO">CRMID</param>
        /// <returns></returns>
        public FS.HISFC.Models.RADT.PatientInfo QueryComPatientInfoByCRMID(string crmID)
        {
            return managerInpatient.QueryComPatientInfoByCRMID(crmID);
        }

        /// <summary>
        /// ���߻�����Ϣ��ѯ  {F55EE363-24DB-4a01-B540-49761A5ADBC6}{6ABA909B-8693-46d5-B636-8C30797BAE8E}
        /// </summary>
        /// <param name="cardNO">CRMID</param>
        /// <returns></returns>
        public ArrayList  QueryComPatientInfoByphone(string phone)
        {
            return managerInpatient.QueryPatientByPhone(phone);
        }

        /// <summary>
		/// ����ԤԼ��Ժ�Ǽǻ���-������Ϣ
		/// </summary>
		/// <param name="PatientInfo"></param>
		/// <returns>���� 0 �ɹ� С�� 0 ʧ��</returns>
        public int InsertPreInPatient(FS.HISFC.Models.RADT.PatientInfo PatientInfo)
        {
            this.SetDB(managerInpatient);
            return managerInpatient.InsertPreInPatient(PatientInfo);
        }

        /// <summary>
        /// ����ԤԼ��Ժ�Ǽǻ���-������Ϣ
        /// </summary>
        /// <param name="PatientInfo"></param>
        /// <returns>���� 0 �ɹ� С�� 0 ʧ��</returns>
        public int UpdatePreInPatientByHappenNo(FS.HISFC.Models.RADT.PatientInfo PatientInfo)
        {
            this.SetDB(managerInpatient);
            return managerInpatient.UpdatePreInPatientByHappenNo(PatientInfo);
        }

        /// <summary>
        /// ���߿���ԤԼ��Σ���������� ������Ÿ���ԤԼ״̬ 0 ΪԤԼ 1 Ϊ���� 2ת��Ժ
        /// </summary>
        /// <param name="CardNO">���￨��</param>
        /// <param name="State">״̬</param>
        /// <param name="HappenNO">�������</param>
        /// <returns></returns>
        public int UpdatePreInPatientState(string CardNO, string State, string HappenNO)
        {
            this.SetDB(managerInpatient);
            return managerInpatient.UpdatePreInPatientState(CardNO, State, HappenNO);
        }

        /// <summary>
		/// ��������Ż�õǼ�ʵ��
		/// </summary>
		/// <param name="strNo">�������</param>
		/// <param name="cardNO">����</param>
		/// <returns></returns>
        public FS.HISFC.Models.RADT.PatientInfo QueryPreInPatientInfoByCardNO(string strNo, string cardNO)
        {
            this.SetDB(managerInpatient);
            return managerInpatient.GetPreInPatientInfoByCardNO(strNo, cardNO);
        }

        /// <summary>
		/// ��ȡԤԼ�Ǽ���Ϣͨ��״̬��ԤԼʱ��
		/// </summary>
		/// <param name="State"></param>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <returns></returns>
        public ArrayList QueryPreInPatientInfoByDateAndState(string State, string Begin, string End)
        {
            this.SetDB(managerInpatient);
            return managerInpatient.GetPreInPatientInfoByDateAndState(State, Begin, End);
        }
        /// <summary>
        /// ��ȡԤԼ�Ǽ���Ϣͨ�����ź�ԤԼʱ��// {6BF1F99D-7307-4d05-B747-274D24174895}
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public ArrayList GetPrepayInByCardNoAndDate(string cardNo)
        {
            this.SetDB(managerInpatient);
            return managerInpatient.GetPrepayInByCardNoAndDate(cardNo);
        }
        #endregion

        #region ���ҽṹά��

        /// <summary>
        /// ����ͳ�Ʒ�����룬���ӿ��ұ�����ȡ�丸���ڵ������Ϣ��
        /// </summary>
        /// <param name="deptCode">���ұ���(���ӿ���)</param>
        /// <returns></returns>
        public ArrayList LoadPhaParentByChildren(string deptCode)
        {
            this.SetDB(departStatManager);

            return departStatManager.LoadByChildren("03", deptCode);
        }

        /// <summary>
        /// ����ͳ�Ʒ�����룬�������ұ�����ȡ�������¼��ڵ������Ϣ��
        /// </summary>
        /// <param name="statCode">ͳ�ƴ������</param>
        /// <param name="parDeptCode">�������ұ���</param>
        /// <param name="nodeKind">��������: 0��ʵ����, 1���ҷ���(�����), 2ȫ������</param>
        /// <returns></returns>
        public ArrayList LoadChildren(string statCode, string parDeptCode, int nodeKind)
        {
            this.SetDB(departStatManager);

            return departStatManager.LoadChildren(statCode, parDeptCode, nodeKind);
        }

        #endregion

        #region �û��ı�
        /// <summary>
        /// �����û��ı�
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public ArrayList GetList(string Code, int Type)
        {
            this.SetDB(userTextManager);
            return userTextManager.GetList(Code, Type);
        }
        /// <summary>
        /// ����ʹ��Ƶ��
        /// </summary>
        /// <param name="id"></param>
        /// <param name="operId"></param>
        /// <returns></returns>
        public int UpdateFrequency(string id, string operId)
        {
            this.SetDB(userTextManager);
            return userTextManager.UpdateFrequency(id, operId);
        }

        #endregion

        #region  ȡҽԺ����
        public string GetHospitalName()
        {
            this.SetDB(managerConstant);
            return managerConstant.GetHospitalName();
        }
        #endregion 

        #region ƴ������
        /// <summary>
		/// ȡһ�����ֵ�ƴ���루ȫƴ�� 
		/// </summary>
		/// <param name="word">һ������</param>
		/// <returns>null ������� </returns>
        public string GetSpellCode(string word)
        {
            this.SetDB(spellManager);
            return spellManager.GetSpellCode(word);
        }
        /// <summary>
        /// ����ַ���
        /// </summary>
        /// <param name="Words"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.ISpell Get(string Words)
        {
            this.SetDB(spellManager);
            return spellManager.Get(Words);
        }
        #endregion

        #region ��������ά��

        public ArrayList GetPrivInOutDeptList(string deptCode, string class2Priv)
        {
            FS.HISFC.BizLogic.Manager.PrivInOutDept privInOutManager = new FS.HISFC.BizLogic.Manager.PrivInOutDept();
            return privInOutManager.GetPrivInOutDeptList(deptCode, class2Priv);
        }

        #endregion

        //{7565C40F-3BD3-416a-B12B-BD12ABA51551}
         /// <summary>
        /// ������Ա���룬����Ȩ�ޱ���ȡ��Աӵ��Ȩ�޵Ĳ���
        /// </summary>
        /// <param name="userCode">����Ա����</param>
        /// <param name="class2Code">����Ȩ����</param>
        /// <returns>�ɹ����ؾ���Ȩ�޵Ŀ��Ҽ��� ʧ�ܷ���null</returns>        
        public List<FS.FrameWork.Models.NeuObject> QueryUserPriv(string userCode, string class2Code)
        {

            this.SetDB(this.userPowerDetailManager);
            return this.userPowerDetailManager.QueryUserPriv(userCode, class2Code);

        }

        #region Ȩ��

        protected FS.HISFC.BizLogic.Manager.UserPowerDetailManager powerDetailManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();

        public List<FS.FrameWork.Models.NeuObject> QueryUserPrivCollection(string userCode, string class2Code, string deptCode)
        {
            this.SetDB(powerDetailManager);

            return powerDetailManager.QueryUserPrivCollection(userCode, class2Code, deptCode);
        }

        #endregion

        //{3AEB5613-1CB0-4158-89E6-F82F0B643388}
        /// <summary>
        /// ������Ա�����ȡҽ������Ϣ
        /// </summary>
        /// <param name="emplCode"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Order.Inpatient.MedicalTeamForDoct> GetMedicalGroup(string deptCode, string emplCode)
        {
            FS.HISFC.BizLogic.Order.MedicalTeamForDoct medicalTeamForDoct = new FS.HISFC.BizLogic.Order.MedicalTeamForDoct();
            this.SetDB(medicalTeamForDoct);
            return medicalTeamForDoct.QueryQueryMedicalTeamForDoctInfo(deptCode,emplCode);
        }
    }
}
