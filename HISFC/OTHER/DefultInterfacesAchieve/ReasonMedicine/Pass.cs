using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;
using System.Collections;
using Neusoft.HISFC.BizProcess.Interface.Order;
using Neusoft.HISFC.Models.RADT;
using Neusoft.HISFC.Models.Registration;
using Neusoft.FrameWork.Function;
using Neusoft.HISFC.Models.Base;

namespace Neusoft.DefultInterfacesAchieve
{
    /// <summary>
    /// ����������ҩ�ӿ�ʵ��
    /// </summary>
    public class Pass : IReasonableMedicine
    {
        #region �ӿڵ���

        /// <summary>
        /// ע���������PASS�ͻ��˹���Ӧ����ʽע���Ƿ�ɹ���
        /// </summary>
        /// <returns>0	ע��ɹ�������ʧ�ܣ�255  PASS��������������ʧ��</returns>
        [DllImport("ShellRunAs.dll")]
        public static extern int RegisterServer();

        /// <summary>
        /// ��ʼ��PASS�Ƿ���ȷ
        /// </summary>
        /// <param name="userInfo">�û�����/��¼�û�</param>
        /// <param name="deptInfo">���Ҵ���/������������</param>
        /// <param name="stationType">���봫ֵ��10--ָסԺҽ��վ������ҽ��վ����ʿվ��PIVA����ҩ���������ģ�20-ָ�ٴ�ҩѧ����վ</param>
        /// <returns>0 ��ʼ��PASSʧ�� 1��ʼ��PASS����ͨ��</returns>
        [DllImport("DIFPassDll.dll")]
        public static extern int PassInit(string userInfo, string deptInfo, int stationType);

        /// <summary>
        /// PASS����ģʽ����
        /// </summary>
        /// <param name="saveCheckResult">�Ƿ���вɼ���ȡֵ�� 0-���ɼ� 1-����ϵͳ����  �����봫ֵ��</param>
        /// <param name="allowAllegen">�Ƿ�����˹���ʷ/����״̬��ȡֵ��0-������1-���û����� 2-PASS���� 3-PASSǿ�ƹ������봫ֵ��</param>
        /// <param name="checkMode">���ģʽ��ȡֵ��0-��ͨģʽ 1-IVģʽ �����봫ֵ��</param>
        /// <param name="disqMode">����ҩ�о�ʱ�Ƿ���ҽ����Ϣ��ȡֵ��0-���� 1-Ҫ�� 2-��ʾ �����봫ֵ��</param>
        /// <param name="useDiposeIden">�Ƿ�ʹ�ô��������ȡֵ��0-��ʹ�ô��� 1-�������ã����봫ֵ��</param>
        /// <returns></returns>
        [DllImport("DIFPassDll.dll")]
        public static extern int PassSetControlParam(int saveCheckResult, int allowAllegen, int checkMode, int disqMode, int useDiposeIden);

        /// <summary>
        /// �����˻�����Ϣ
        /// </summary>
        /// <param name="PatientID">���˲�����ţ����봫ֵ��</param>
        /// <param name="VisitID">��ǰ������������봫ֵ��</param>
        /// <param name="Name">��������    �����봫ֵ��</param>
        /// <param name="Sex">�����Ա�    �����봫ֵ���磺�С�Ů������ֵ����δ֪</param>
        /// <param name="Birthday">��������    �����봫ֵ����ʽ��2005-09-20</param>
        /// <param name="Weight">����        �����Բ���ֵ����λ��KG</param>
        /// <param name="cHeight">���        �����Բ���ֵ����λ��CM</param>
        /// <param name="DepartMentName"> ҽ������ID/ҽ����������  �����Բ���ֵ��</param>
        /// <param name="Doctor">����ҽ��ID/����ҽ������ �����Բ���ֵ��</param>
        /// <param name="LeaveHospitalDate">��Ժ���� �����Բ���ֵ��</param>
        /// <returns></returns>
        [DllImport("DIFPassDll.dll")]
        public static extern int PassSetPatientInfo(string PatientID, string VisitID, string Name, string Sex, string Birthday, string Weight, string cHeight, string DepartMentName, string Doctor, string LeaveHospitalDate);

        /// <summary>
        /// ���뵱ǰ������ҩ��Ϣ�ӿ�
        /// �����ǰ�����ж�����ҩҽ��ʱ��ѭ�����롣�����ҽ��Ϊ��ҩҽ�������ڹ���վ����Ϊ10��ʱ�����ʱ(�磺סԺҽ��վ������ҽ��վ)����ҩ�������ڿ��Բ��ô�ֵ��Ĭ��Ϊ���죻�����ڹ���վ����Ϊ20�ع������ʱ(�磺�ٴ�ҩѧ�������ѯͳ��)����ҩ�������ڱ��봫ֵ��
        /// </summary>
        /// <param name="OrderUniqueCode">ҽ��Ψһ�루���봫ֵ��</param>
        /// <param name="DrugCode">ҩƷ����  �����봫ֵ��</param>
        /// <param name="DrugName">ҩƷ����  �����봫ֵ��</param>
        /// <param name="SingleDose">ÿ������  �����봫ֵ��</param>
        /// <param name="DoseUnit">������λ  �����봫ֵ��</param>
        /// <param name="Frequency">��ҩƵ��(��/��)�����봫ֵ��</param>
        /// <param name="StartOrderDate">��ҩ��ʼ���ڣ���ʽ��yyyy-mm-dd  �����봫ֵ��</param>
        /// <param name="StopOrderDate">��ҩ�������ڣ���ʽ��yyyy-mm-dd  �����Բ���ֵ����Ĭ��ֵΪ����</param>
        /// <param name="RouteName">��ҩ;���������� �����봫ֵ��</param>
        /// <param name="GroupTag">����ҽ����־  �����봫ֵ��</param>
        /// <param name="OrderType">�Ƿ�Ϊ��ʱҽ�� 1-����ʱҽ�� 0��� ����ҽ�� �����봫ֵ��</param>
        /// <param name="OrderDoctor">����ҽ��ID/����ҽ������ �����봫ֵ��</param>
        /// <returns></returns>
        [DllImport("DIFPassDll.dll")]
        public static extern int PassSetRecipeInfo(string OrderUniqueCode, string DrugCode, string DrugName, string SingleDose, string DoseUnit, string Frequency, string StartOrderDate, string StopOrderDate, string RouteName, string GroupTag, string OrderType, string OrderDoctor);

        /// <summary>
        /// �����˹���ʷ��Ϣ��HISϵͳ���������뵽PASSϵͳ�������ʱ�����øýӿ�
        /// </summary>
        /// <param name="allergenIndex">����ԭ��ҽ���е�˳���ţ����봫ֵ��</param>
        /// <param name="allergenCode">����ԭ���루���봫ֵ��</param>
        /// <param name="allergenDesc">����ԭ���ƣ����봫ֵ��</param>
        /// <param name="allergenType">����ԭ���ͣ����봫ֵ��ȡֵ����(�жϲ��ִ�Сд)��
        ///     AllergenGroup  PASS������
        ///     USER_Drug   �û�ҩƷ
        ///     DrugName     PASSҩ������</param>
        /// <param name="reaction">����֢״  �����Բ���ֵ��</param>
        /// <returns></returns>
        [DllImport("DIFPassDll.dll")]
        public static extern int PassSetAllergenInfo(string allergenIndex, string allergenCode, string allergenDesc, string allergenType, string reaction);

        /// <summary>
        /// ���벡��״̬
        /// </summary>
        /// <param name="medCondIndex">ҽ��������ҽ���е�˳���ţ����봫ֵ��</param>
        /// <param name="medCondCode">ҽ���������루���봫ֵ��</param>
        /// <param name="medCondDesc">ҽ���������ƣ����봫ֵ��</param>
        /// <param name="medCondType">ҽ���������ͣ����봫ֵ����ֵ����(�жϲ��ִ�Сд)
        /// USER_MedCond	�û�ҽ������
        /// ICD				ICD-9CM����</param>
        /// <param name="startDate">��ʼ����  ��ʽ��yyyy-mm-dd�����Բ���ֵ��</param>
        /// <param name="endDate">��������  ��ʽ��yyyy-mm-dd�����Բ���ֵ��</param>
        /// <returns></returns>
        [DllImport("DIFPassDll.dll")]
        public static extern int PassSetMedCond(string medCondIndex, string medCondCode, string medCondDesc, string medCondType, string startDate, string endDate);

        /// <summary>
        /// ���뵱ǰҩƷ��ѯ��Ϣ
        /// </summary>
        /// <param name="drugCode">ҩƷ���루���봫ֵ��</param>
        /// <param name="drugName">ҩƷ���ƣ����봫ֵ��</param>
        /// <param name="doseUnit">������λ�����봫ֵ��</param>
        /// <param name="routeName">��ҩ;���������ƣ����봫ֵ��</param>
        /// <returns></returns>
        [DllImport("DIFPassDll.dll")]
        public static extern int PassSetQueryDrug(string drugCode, string drugName, string doseUnit, string routeName);

        /// <summary>
        /// PASSϵͳ�����Ƿ���Ч��
        /// ��ˢ�½��桢�л����ˡ������Ҽ��˵�����֮����Ҫˢ�²�ѯ����ʱ�������ݴ˺����ķ���ֵ��������Ӧ�ؼ�Enabled����
        /// </summary>
        /// <param name="queryItemNo">��ѯ����Ŀ��ʶ����Ŀ��ţ����д���/���Ĳ��֣���ʾ���ϱ�ʶ������������ѯ����Ŀ��״̬(��Ŀ��ʶ�����ִ�Сд)�����±�</param>
        /// <returns></returns>
        [DllImport("DIFPassDll.dll")]
        public static extern int PassGetState(string queryItemNo);
        /*
        ��Ŀ���	��Ŀ��ʶ	��Ŀ����	˵��
            0	PASSENABLE	PASS�����Ƿ����	����������״̬

            11	SYS-SET	ϵͳ��������	������ҩ��������
            12	DISQUISITION	��ҩ�о�	
            13	MATCH-DRUG	ҩƷ�����Ϣ��ѯ	
            14	MATCH-ROUTE	ҩƷ��ҩ;����Ϣ��ѯ	

            24	AlleyEnable	����״̬/����ʷ����	����ʷ/����״̬����

            101	CPRRes/CPR	�ٴ���ҩָ�ϲ�ѯ	һ���˵���ѯ״̬
            102	Directions	ҩƷ˵�����ѯ	
            103	CPERes/CPE	������ҩ������ѯ	
            104	CheckRes/CHECKINFO	ҩ�����ֵ��ѯ	
            105	HisDrugInfo	ҽԺҩƷ��Ϣ��ѯ	
            106	MEDInfo	ҩ����Ϣ��ѯ����	
            107	Chp	�й�ҩ��	
            501	DISPOSE	�����������	
            220	LMIM	�ٴ�������Ϣ�ο���ѯ	

            201	DDIM	ҩ����ҩ���໥���ò�ѯ	�����˵���ѯ״̬(ר����Ϣ)
            202	DFIM	ҩ����ʳ���໥���ò�ѯ	
            203	MatchRes/IV	����ע������������ѯ	
            204	TriessRes/IVM	����ע������������ѯ	
            205	DDCM	����֢��ѯ	
            206	SIDE	�����ò�ѯ	
            207	GERI	��������ҩ�����ѯ	
            208	PEDI	��ͯ��ҩ�����ѯ	
            209	PREG	��������ҩ�����ѯ	
            210	LACT	��������ҩ�����ѯ	

            301	HELP	Passϵͳ����	������ҩ����ϵͳ״̬
         * */

        /// <summary>
        /// PASS���ܵ���
        /// </summary>
        /// <param name="commandNo">CommandNo����ţ���ʾ����PASSϵͳ���������</param>
        /// <returns></returns>
        [DllImport("DIFPassDll.dll")]
        public static extern int PassDoCommand(int commandNo);
        /*
         * �����
         * 
         * 1	סԺҽ������վ�����Զ����	1-�������ͨ��            0-����δ��������
            2	סԺҽ������վ�ύ�Զ����	1-�������ͨ��            0-����δ��������
            33	����ҽ������վ�����Զ����	1-�������ͨ��            0-����δ��������
            34	����ҽ������վ�ύ�Զ����	1-�������ͨ��            0-����δ��������
            3	�ֹ����	��������
            4	�ٴ�ҩѧ���������	��������
            5	�ٴ�ҩѧ�ಡ�����	��������
            6	��ҩ����	��������
            7	�ֶ����,���Խ������	��������
         * 
            ��ѯ��
         * 
            13	ҩƷ�����Ϣ��ѯ	��������
            14	ҩƷ��ҩ;����Ϣ��ѯ	��������
            101	�ٴ���ҩָ�ϲ�ѯ	��������
            102	ҩƷ˵�����ѯ	��������
            103	������ҩ������ѯ	��������
            104	ҩ�����ֵ��ѯ	��������
            105	ҽԺҩƷ��Ϣ��ѯ	��������
            106	ҩ����Ϣ��ѯ����	��������
            107	�й�ҩ��	��������
            201	ҩ����ҩ���໥���ò�ѯ	��������
            202	ҩ����ʳ���໥���ò�ѯ	��������
            203	����ע������������ѯ	��������
            204	����ע������������ѯ	��������
            205	����֢��ѯ	��������
            206	�����ò�ѯ	��������
            207	��������ҩ�����ѯ	��������
            208	��ͯ��ҩ�����ѯ	��������
            209	��������ҩ�����ѯ	��������
            210	��������ҩ�����ѯ	��������
            220	�ٴ�������Ϣ�ο���ѯ	��������
         * 
         * ���ڿ�����
         * 
            401	��ʾ��������	��������
            402	�ر����и�������	��������
            403	��ʾ��ҩ���һ�������������ʾ����	��������
         * 
         * �ۺ���
         * 
            11	ϵͳ��������	��������
            12	��ҩ�о�	��������
            21	����״̬/����ʷ����(ֻ��)	��������
            22	����״̬/����ʷ����(�༭)	���˹���ʷ/����״̬�Ƿ����˱仯��2-�����˱仯��1-δ�����仯��<=0-���ֳ������
            23	����״̬/����ʷ����(ǿ��)	���˹���ʷ/����״̬�Ƿ����˱仯��2-�����˱仯��1-δ�����仯��<=0-���ֳ������

         * */

        /// <summary>
        /// ��ȡPASS�������ʾ����
        /// Ƕ��ʱ�û����Ը�����鷵�ؾ���ֵ�����ж�ҽ���Ƿ���Ҫ������ύ���ƣ�ͬʱ�����Խ��þ���ֵ���浽HISϵͳ���ݿ��У�������������վ�鿴�ȡ����һ����ҩҽ������PASS��鷢�ֿ��ܴ��ڶ��Ǳ����ҩ���⣬ϵͳ�������о�ʾ������ߵľ�ʾɫ��ʾ����ҽ�� ����Ҫע����ǣ���鷵�ؾ���ֵ������ߴ��������أ����Ǿ�ʾ������ߵľ�ʾɫ�����������
        /// </summary>
        /// <param name="drugUniqeCode">ҽ��Ψһ����</param>
        /// <returns></returns>
        [DllImport("DIFPassDll.dll")]
        public static extern int PassGetWarn(string drugUniqeCode);
        /*
         * ��鷵�ؾ���ֵ	��ʾɫ	˵��
            -3	�޵�	��ʾPASS���ִ���δ�������
            -2	�޵�	��ʾ��ҩƷ�ڴ�������ʱ������
            -1	�޵�	��ʾδ����PASSϵͳ�ļ��
            0	����	PASS���δ��ʾ�����ҩ����
            1	�Ƶ�	Σ���ϵͻ��в���ȷ���ʶȹ�ע
            2	���	���Ƽ��������Σ�����߶ȹ�ע
            4	�ȵ�	���û���һ��Σ�����ϸ߶ȹ�ע
            3	�ڵ�	���Խ��ɡ������������Σ�������ع�ע
         * */

        /// <summary>
        /// ����ҩƷ��������λ��
        /// ���뵱ǰҩƷ����������ʾλ��ʱ�����ñ��ӿڣ����ǵ��ñ��ӿ�֮ǰ���ȵ���PassSetQueryDrug() ���������뵱ǰҩƷ��ѯ��Ϣ
        /// </summary>
        /// <param name="left">���Ͻ�x</param>
        /// <param name="top">���Ͻ�y</param>
        /// <param name="right">���½�x</param>
        /// <param name="bottom">���½�y</param>
        /// <returns></returns>
        [DllImport("DIFPassDll.dll")]
        public static extern int PassSetFloatWinPos(int left, int top, int right, int bottom);

        /// <summary>
        /// ��ʾ���渡�����ڻ�ҩ����
        /// </summary>
        /// <param name="drugUniqueCode">ҽ��Ψһ����</param>
        /// <returns></returns>
        [DllImport("DIFPassDll.dll")]
        public static extern int PassSetWarnDrug(string drugUniqueCode);

        /// <summary>
        /// PASS�˳�
        /// </summary>
        /// <returns></returns>
        [DllImport("DIFPassDll.dll")]
        public static extern int PassQuit();

        #endregion

        #region ����

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private string err = "";

        /// <summary>
        /// �Ƿ����ú�����ҩ���
        /// </summary>
        private bool passEnabled = false;

        /// <summary>
        /// 0 ��ʼ��PASSʧ�� 1��ʼ��PASS����ͨ��
        /// </summary>
        private int stationType = 0;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string Err
        {
            get
            {
                return this.err;
            }
            set
            {
                this.err = value;
            }
        }

        /// <summary>
        /// �Ƿ����ú�����ҩ���
        /// </summary>
        public bool PassEnabled
        {
            get
            {
                return this.passEnabled;
            }
            set
            {
                this.passEnabled = value;
            }
        }

        /// <summary>
        /// 0 ��ʼ��PASSʧ�� 1��ʼ��PASS����ͨ��
        /// </summary>
        public int StationType
        {
            get
            {
                return this.stationType;
            }
            set
            {
                this.stationType = value;
            }
        }

        /// <summary>
        /// �ݴ�ҩƷ������Ϣ
        /// </summary>
        Neusoft.HISFC.Models.Pharmacy.Item phaItem = null;

        #endregion

        #region ҵ��㺯��

        /// <summary>
        /// ҩƷҵ��
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Pharmacy();

        #endregion

        #region ����

        /// <summary>
        /// PASS���ܵ���
        /// </summary>
        /// <param name="commandType">PASS���������</param>
        /// <returns></returns>
        public int DoCommand(int commandType)
        {
            //�ж������Ƿ����
            if (this.PassEnabled && (PassGetState("0") != 0))
            {
                return PassDoCommand(commandType);
            }
            return -4;
        }

        /// <summary>
        /// PASSϵͳ�����Ƿ���Ч�Լ���
        /// </summary>
        /// <param name="queryItemNo"></param>
        /// <returns></returns>
        public int PassGetStateIn(string queryItemNo)
        {
            return PassGetState(queryItemNo);
        }

        /// <summary>
        /// ��ȡPASS�������ʾ����
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public int PassGetWarnFlag(string orderId)
        {
            if (this.PassEnabled && (PassGetState("0") != 0))
            {
                return PassGetWarn(orderId);
            }
            return -4;
        }

        /// <summary>
        /// ��ʾ���渡�����ڻ�ҩ����
        /// </summary>
        /// <param name="orderId">ҽ��Ψһ����</param>
        /// <param name="flag">��ʱ����</param>
        /// <returns></returns>
        public int PassGetWarnInfo(string orderId, string flag)
        {
            if (this.PassEnabled && (PassGetState("0") != 0))
            {
                PassSetWarnDrug(orderId);
                if (flag == "0")
                {
                    PassDoCommand(0x193);
                }
                else
                {
                    PassDoCommand(6);
                }
            }
            return 1;
        }

        /// <summary>
        /// PASS��ʼ��
        /// </summary>
        /// <param name="userID">�û�ID</param>
        /// <param name="userName">�û���</param>
        /// <param name="deptID">����ID</param>
        /// <param name="deptName">��������</param>
        /// <param name="stationType">����վ����(10-ҽ������վ 20-ҩѧ����վ)</param>
        /// <returns></returns>
        public int PassInit(string userID, string userName, string deptID, string deptName, int stationType)
        {
            int rev = this.PassInit(userID, userName, deptID, deptName, stationType, false);

            if (rev != 1)
            {
                try
                {
                    if (System.IO.File.Exists(Neusoft.FrameWork.WinForms.Classes.Function.CurrentPath + "/MedcomPassClient.exe"))
                    {
                        System.Diagnostics.Process.Start(Neusoft.FrameWork.WinForms.Classes.Function.CurrentPath + "/MedcomPassClient.exe");
                    }
                }
                catch
                {
                    return rev;
                }
                rev = this.PassInit(userID, userName, deptID, deptName, stationType, true);
            }
            return rev;
        }

        /// <summary>
        /// PASS��ʼ��
        /// </summary>
        /// <param name="userID">�û�ID</param>
        /// <param name="userName">�û���</param>
        /// <param name="deptID">����ID</param>
        /// <param name="deptName">��������</param>
        /// <param name="stationType">����վ����(10-ҽ������վ 20-ҩѧ����վ)</param>
        /// <param name="isJudgeLocalSetting">�Ƿ��жϱ���xml�����ļ���5.0 ���ã�</param>
        /// <returns></returns>
        public int PassInit(string userID, string userName, string deptID, string deptName, int stationType, bool isJudgeLocalSetting)
        {
            Exception exception;
            Neusoft.FrameWork.Management.ControlParam controler = new Neusoft.FrameWork.Management.ControlParam();

            bool isEnablePass = false;
            try
            {
                isEnablePass = NConvert.ToBoolean(controler.QueryControlerInfo("200014"));
            }
            catch (Exception exception1)
            {
                exception = exception1;
                this.err = exception.Message;
                this.PassEnabled = false;
                return -1;
            }
            if (!isEnablePass)
            {
                this.PassEnabled = false;
                return 0;
            }
            if (isJudgeLocalSetting)
            {
                try
                {
                    ArrayList defaultValue = Neusoft.FrameWork.WinForms.Classes.Function.GetDefaultValue("PassSetting", out this.err);
                    if ((defaultValue == null) || (defaultValue.Count == 0))
                    {
                        this.PassEnabled = false;
                    }
                    else if ((defaultValue[0] as string) == "1")
                    {
                        this.PassEnabled = true;
                    }
                    else
                    {
                        this.PassEnabled = false;
                    }
                }
                catch (Exception exception2)
                {
                    exception = exception2;
                    this.err = exception.Message;
                    this.PassEnabled = false;
                    return -1;
                }
                if (!this.PassEnabled)
                {
                    this.PassEnabled = false;
                    return 0;
                }
            }
            try
            {
                int rev = PassInit(userID + "/" + userName, deptID + "/" + deptName, stationType);

                if (rev != 1)
                {
                    try
                    {
                        if (System.IO.File.Exists(Neusoft.FrameWork.WinForms.Classes.Function.CurrentPath + "MedicomSoftWare.exe"))
                        {
                            System.Diagnostics.Process.Start(Neusoft.FrameWork.WinForms.Classes.Function.CurrentPath + "MedicomSoftWare.exe");
                            DateTime time = DateTime.Now;
                            while (DateTime.Now < time.AddSeconds(3))
                            {
                            }
                        }
                        rev = PassInit(userID + "/" + userName, deptID + "/" + deptName, stationType);
                    }
                    catch
                    {
                        this.PassEnabled = false;
                        return rev;
                    }
                }

                if (rev == 0)
                {
                    this.PassEnabled = false;
                    this.err = "������ҩϵͳ ��ʼ��ʧ��! �������Ա��ϵ";
                    return -1;
                }
                if (PassGetState("0") != 0)
                {
                    PassSetControlParam(1, 2, 0, 2, 1);
                    this.passEnabled = true;
                }
            }
            catch (DllNotFoundException)
            {
                try
                {
                    if (System.IO.File.Exists(Neusoft.FrameWork.WinForms.Classes.Function.CurrentPath + "MedicomSoftWare.exe"))
                    {
                        System.Diagnostics.Process.Start(Neusoft.FrameWork.WinForms.Classes.Function.CurrentPath + "MedicomSoftWare.exe");
                        DateTime time = DateTime.Now;
                        while (DateTime.Now < time.AddSeconds(3))
                        {
                        }
                    }
                    int rev = PassInit(userID + "/" + userName, deptID + "/" + deptName, stationType);

                    if (rev > 0)
                    {
                        return rev;
                    }
                }
                catch
                {
                }

                this.err = "δ�ҵ�������ҩϵͳ������������Dll ������ҩϵͳ���޷�����������\n                 �������Ա��ϵ";
                this.PassEnabled = false;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 7.2.8	���뵱ǰ��ѯҩƷ��Ϣ
        /// ����Ҫʵ��ҩ����Ϣ��ѯ�򸡶����ڹ��ܣ����øýӿ�
        /// </summary>
        /// <param name="drugCode"></param>
        /// <param name="drugName"></param>
        /// <param name="doseUnit"></param>
        /// <param name="useName"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        public void PassQueryDrug(string drugCode, string drugName, string doseUnit, string useName, int left, int top, int right, int bottom)
        {
            if (this.PassEnabled && (PassGetState("0") != 0))
            {
                int rev = 0;
                rev = PassSetQueryDrug(drugCode, drugName, doseUnit, useName);
                rev = PassSetFloatWinPos(left, top, right, bottom);
                //rev = PassSetFloatWinPos(10, 20, 300, 400);
                this.ShowFloatWin(true);
            }
        }

        /// <summary>
        /// �˳�PASS
        /// </summary>
        /// <returns></returns>
        public int PassQuitIn()
        {
            if (this.passEnabled && (PassGetState("0") != 0))
            {
                return PassQuit();
            }
            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patient">������Ϣ</param>
        /// <param name="alOrder">ҽ���б�</param>
        /// <param name="checkType">PASS���ܱ���</param>
        /// <returns></returns>
        public int PassSaveCheck(PatientInfo patient, List<Neusoft.HISFC.Models.Order.Inpatient.Order> alOrder, int checkType)
        {
            if (!this.PassEnabled)
            {
                return 0;
            }
            if ((alOrder == null) || (alOrder.Count == 0))
            {
                return -1;
            }

            foreach (Neusoft.HISFC.Models.Order.Inpatient.Order order in alOrder)
            {
                if (order == null)
                {
                    this.err = "ִ��ҽ����ҩ���ʱ����! ��������ת������";
                    return -1;
                }
                if ((order.Item.SysClass.ID.ToString().Substring(0, 1) == "P") && (order.Status != 3))
                {
                    try
                    {
                        this.PassSetRecipeInfo(order);
                    }
                    catch (Exception exception)
                    {
                        this.err = "������ҩ��鴫��ҽ�����ݹ����з�������! " + exception.Message;
                        return -1;
                    }
                }
            }
            //PASS���ܵ���
            return PassDoCommand(checkType);
            return 1;
        }

        /// <summary>
        /// ��ҩ��Ϣ���
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="alOrder"></param>
        /// <param name="checkType"></param>
        /// <returns></returns>
        public int PassSaveCheck(Register patient, List<Neusoft.HISFC.Models.Order.OutPatient.Order> alOrder, int checkType)
        {
            if (!this.PassEnabled)
            {
                return 0;
            }
            if ((alOrder == null) || (alOrder.Count == 0))
            {
                return -1;
            }

            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in alOrder)
            {
                if (order == null)
                {
                    this.err = "ִ��ҽ����ҩ���ʱ����! ��������ת������";
                    return -1;
                }
                if ((order.Item.ItemType == EnumItemType.Drug) && (order.Status != 3))
                {
                    try
                    {
                        this.PassSetRecipeInfo(order);
                    }
                    catch (Exception exception)
                    {
                        this.err = "������ҩ��鴫��ҽ�����ݹ����з�������! " + exception.Message;
                        return -1;
                    }
                }
            }
            int rev = PassDoCommand(checkType);

            return rev;
            return 1;
        }

        /// <summary>
        /// ���õ�ǰ��ҩ��Ϣ
        /// </summary>
        /// <param name="drugCode"></param>
        /// <param name="drugName"></param>
        /// <param name="doseUnit"></param>
        /// <param name="routeName"></param>
        /// <returns></returns>
        public int PassSetDrug(string drugCode, string drugName, string doseUnit, string routeName)
        {
            return PassSetQueryDrug(drugCode, drugName, doseUnit, routeName);
        }

        /// <summary>
        /// ����סԺ������Ϣ
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="docID"></param>
        /// <param name="docName"></param>
        public void PassSetPatientInfo(PatientInfo patient, string docID, string docName)
        {
            if (((patient != null) && this.PassEnabled) && (PassGetState("0") != 0))
            {
                string str2;
                string patientNO = patient.PID.PatientNO;
                try
                {
                    str2 = NConvert.ToInt32(patient.ID.Substring(2, 2)).ToString();
                }
                catch
                {
                    str2 = "1";
                }
                string name = patient.Name;
                string sex = "";
                if (patient.Sex.ID.ToString() == "F")
                {
                    sex = "��";
                }
                else if (patient.Sex.ID.ToString() == "M")
                {
                    sex = "Ů";
                }
                else
                {
                    sex = "δ֪";
                }
                string birthday = patient.Birthday.ToString("yyyy-MM-dd");
                string weight = patient.Weight;
                string height = patient.Height;
                string departMentName = patient.PVisit.PatientLocation.Dept.ID + "/" + patient.PVisit.PatientLocation.Dept.Name;
                string doctor = patient.PVisit.AdmittingDoctor.ID + "/" + patient.PVisit.AdmittingDoctor.Name;
                string leaveHospitalDate = "";
                PassDoCommand(0x192);
                PassSetPatientInfo(patientNO, str2, name, sex, birthday, weight, height, departMentName, doctor, leaveHospitalDate);
            }
        }

        /// <summary>
        /// �������ﲡ����Ϣ
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="docID"></param>
        /// <param name="docName"></param>
        public void PassSetPatientInfo(Register patient, string docID, string docName)
        {
            if (((patient != null) && this.PassEnabled) && (PassGetState("0") != 0))
            {
                string str2;
                string patientNO = patient.PID.CardNO;
                try
                {
                    str2 = NConvert.ToInt32(patient.ID.Substring(2, 2)).ToString();
                }
                catch
                {
                    str2 = "1";
                }
                string name = patient.Name;
                string sex = "";
                if (patient.Sex.ID.ToString() == "F")
                {
                    sex = "��";
                }
                else if (patient.Sex.ID.ToString() == "M")
                {
                    sex = "Ů";
                }
                else
                {
                    sex = "δ֪";
                }
                string birthday = patient.Birthday.ToString("yyyy-MM-dd");
                string weight = patient.Weight;
                string height = patient.Height;
                string departMentName = patient.DoctorInfo.Templet.Dept.ID + "/" + patient.DoctorInfo.Templet.Dept.Name;

                if (!string.IsNullOrEmpty(patient.SeeDoct.Dept.ID))
                {
                    departMentName = patient.SeeDoct.Dept.ID + "/" + patient.SeeDoct.Dept.Name;
                }

                //����/�Һ�ҽ��
                string doctor = patient.DoctorInfo.Templet.Doct.ID + "/" + patient.DoctorInfo.Templet.Doct.Name;

                if (!string.IsNullOrEmpty(patient.SeeDoct.ID))
                {
                    doctor = patient.SeeDoct.ID + "/" + patient.SeeDoct.Name;
                }

                string leaveHospitalDate = "";
                PassDoCommand(0x192);
                PassSetPatientInfo(patientNO, str2, name, sex, birthday, weight, height, departMentName, doctor, leaveHospitalDate);
            }
        }

        /// <summary>
        /// ����סԺҽ����Ϣ
        /// </summary>
        /// <param name="order"></param>
        public void PassSetRecipeInfo(Neusoft.HISFC.Models.Order.Inpatient.Order order)
        {
            if (this.PassEnabled && ((order != null) && (order.Item.ItemType.ToString() != ItemTypes.Undrug.ToString())))
            {
                string applyNO = order.ApplyNo;
                string iD = order.Item.UserCode;

                phaItem = this.phaIntegrate.GetItem(iD);
                if (phaItem != null)
                {
                    iD = phaItem.UserCode;
                }

                //string iD = "Y00000001077";
                string name = order.Item.Name;
                string singleDose = order.DoseOnce.ToString();
                string doseUnit = order.DoseUnit;
                int length = 0;
                string str7 = "";
                if ((order.Frequency != null) && (order.Usage != null))
                {
                    if (order.Frequency.Time == null)
                    {
                        Neusoft.HISFC.BizLogic.Manager.Frequency frequency = new Neusoft.HISFC.BizLogic.Manager.Frequency();
                        order.Frequency = (Neusoft.HISFC.Models.Order.Frequency)frequency.Get(order.Frequency, "ROOT");
                        if (order.Frequency == null)
                        {
                            return;
                        }
                    }
                    if (order.Frequency.Time == null)
                    {
                        length = 1;
                    }
                    else
                    {
                        length = order.Frequency.Times.Length;
                    }
                    if (order.Frequency.Days == null)
                    {
                        str7 = "1";
                    }
                    else
                    {
                        str7 = order.Frequency.Days[0];
                    }
                    string str6 = length.ToString() + "/" + str7.ToString();
                    string startOrderDate = order.BeginTime.ToString("yyyy-MM-dd");
                    string stopOrderDate = "";
                    string routeName = order.Usage.Name;
                    string groupTag = order.Combo.ID;
                    string orderType = order.OrderType.ID;
                    string orderDoctor = order.Doctor.ID + "/" + order.Doctor.Name;
                    PassSetRecipeInfo(applyNO, iD, name, singleDose, doseUnit, str6, startOrderDate, stopOrderDate, routeName, groupTag, orderType, orderDoctor);
                }
            }
        }

        /// <summary>
        /// �������ﴦ����Ϣ
        /// </summary>
        /// <param name="order"></param>
        public void PassSetRecipeInfo(Neusoft.HISFC.Models.Order.OutPatient.Order order)
        {
            if (this.PassEnabled && ((order != null) && (order.Item.ItemType == EnumItemType.Drug)))
            {
                //ҽ��Ψһ�루���봫ֵ��
                string applyNO = order.ApplyNo;

                // ҩƷ����  �����봫ֵ��
                string iD = order.Item.ID;

                //ҩƷ����  �����봫ֵ��
                string name = order.Item.Name;

                //ÿ������  �����봫ֵ��
                string singleDose = order.DoseOnce.ToString();

                //������λ  �����봫ֵ��
                string doseUnit = order.DoseUnit;

                #region ��ҩƵ��(��/��)�����봫ֵ��
                int length = 0;
                string str7 = "";
                string str6 = "";
                if ((order.Frequency != null) && (order.Usage != null))
                {
                    if (order.Frequency.Time == null)
                    {
                        Neusoft.HISFC.BizLogic.Manager.Frequency frequency = new Neusoft.HISFC.BizLogic.Manager.Frequency();
                        order.Frequency = (Neusoft.HISFC.Models.Order.Frequency)frequency.Get(order.Frequency, "ROOT");
                        if (order.Frequency == null)
                        {
                            return;
                        }
                    }
                    if (order.Frequency.Time == null)
                    {
                        length = 1;
                    }
                    else
                    {
                        length = order.Frequency.Times.Length;
                    }
                    if (order.Frequency.Days == null)
                    {
                        str7 = "1";
                    }
                    else
                    {
                        str7 = order.Frequency.Days[0];
                    }
                    str6 = length.ToString() + "/" + str7.ToString();
                }
                #endregion

                //��ҩ��ʼ���ڣ���ʽ��yyyy-mm-dd  �����봫ֵ��
                string startOrderDate = order.MOTime.ToString("yyyy-MM-dd");

                //��ҩ�������ڣ���ʽ��yyyy-mm-dd  �����Բ���ֵ����Ĭ��ֵΪ����
                string stopOrderDate = "";

                //��ҩ;���������� �����봫ֵ��
                string routeName = order.Usage.Name;

                //����ҽ����־  �����봫ֵ��
                string groupTag = order.Combo.ID;

                //�Ƿ�Ϊ��ʱҽ�� 1-����ʱҽ�� 0��� ����ҽ�� �����봫ֵ��
                string orderType = "1";

                //����ҽ��ID/����ҽ������ �����봫ֵ��
                string orderDoctor = order.ReciptDoctor.ID + "/" + order.ReciptDoctor.Name;

                PassSetRecipeInfo(applyNO, iD, name, singleDose, doseUnit, str6, startOrderDate, stopOrderDate, routeName, groupTag, orderType, orderDoctor);
            }
        }

        /// <summary>
        /// ���ø��������Ƿ���ʾ
        /// </summary>
        /// <param name="isShow"></param>
        public void ShowFloatWin(bool isShow)
        {
            if (this.PassEnabled && (PassGetState("0") != 0))
            {
                if (isShow)
                {
                    PassDoCommand(401);
                }
                else
                {
                    PassDoCommand(402);
                }
            }
        }

        #endregion

        #region IReasonableMedicine ��Ա


        public int PassClose()
        {
            return 1;
        }

        public int PassDrugCheck(ArrayList alOrder, bool isSave)
        {
            return 1;
        }

        public int PassInit(Neusoft.FrameWork.Models.NeuObject logEmpl, Neusoft.FrameWork.Models.NeuObject logDept, string workStationType)
        {
            return 1;
        }

        public int PassRefresh()
        {
            return 1;
        }

        public int PassSetPatientInfo(Patient patient, Neusoft.FrameWork.Models.NeuObject recipeDoct)
        {
            return 1;
        }

        public int PassShowFloatWindow(bool isShow)
        {
            return 1;
        }

        public int PassShowOtherInfo(Neusoft.HISFC.Models.Order.Order order, Neusoft.FrameWork.Models.NeuObject queryType, ref ArrayList alShowMemu)
        {
            return 1;
        }

        public int PassShowSingleDrugInfo(Neusoft.HISFC.Models.Order.Order order, System.Drawing.Point LeftTop, System.Drawing.Point RightButton, bool isFirst)
        {
            return 1;
        }

        public int PassShowWarnDrug(Neusoft.HISFC.Models.Order.Order order)
        {
            return 1;
        }

        ServiceTypes station = ServiceTypes.C;

        ServiceTypes IReasonableMedicine.StationType
        {
            get
            {
                return station;
            }
            set
            {
                station = value;
            }
        }

        #endregion
    }
}
