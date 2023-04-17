using System;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;
using FS.HISFC.Models.RADT;
using System.Collections.Generic;
using FS.HISFC.Models.Registration;
using FS.FrameWork.Function;
using FS.HISFC.Models.Base;

namespace FS.HISFC.BizProcess.Integrate.Pass
{
    /// <summary>
    /// ����Pass ��ժҪ˵����
    /// </summary>
    public class MKPass : FS.HISFC.BizProcess.Interface.Order.IReasonableMedicine
    {
        public MKPass()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

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
         *  1	סԺҽ������վ�����Զ����	1-�������ͨ��            0-����δ��������
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

        #region IReasonableMedicine ��Ա

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private string err = "";

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
        private bool passEnabled = false;

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
        /// ��ǰ������Ϣ
        /// </summary>
        private FS.HISFC.Models.RADT.Patient myPatient = null;

        /// <summary>
        /// ��ǰ����ҽ��
        /// </summary>
        private FS.FrameWork.Models.NeuObject myReciptDoct = null;

        /// <summary>
        /// ����վ���
        /// </summary>
        ServiceTypes stationType = ServiceTypes.C;

        /// <summary>
        /// ����վ���
        /// </summary>
        ServiceTypes FS.HISFC.BizProcess.Interface.Order.IReasonableMedicine.StationType
        {
            get
            {
                return stationType;
            }
            set
            {
                stationType = value;
            }
        }

        /// <summary>
        /// ������ҩ��ʼ��
        /// </summary>
        /// <param name="logEmpl"></param>
        /// <param name="logDept"></param>
        /// <param name="workStationType"></param>
        /// <returns></returns>
        public int PassInit(FS.FrameWork.Models.NeuObject logEmpl, FS.FrameWork.Models.NeuObject logDept, string workStationType)
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            bool isEnablePass = controlMgr.GetControlParam<bool>("200014", true, false);

            if (!isEnablePass)
            {
                this.passEnabled = false;
                return 0;
            }
            try
            {
                int rev = PassInit(logEmpl.ID + "/" + logEmpl.Name, logDept.ID + "/" + logDept.Name, 10);

                if (rev != 1)
                {
                    try
                    {
                        if (System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "MedicomSoftWare.exe"))
                        {
                            System.Diagnostics.Process.Start(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "MedicomSoftWare.exe");
                            DateTime time = DateTime.Now;
                            while (DateTime.Now < time.AddSeconds(3))
                            {
                            }
                        }
                        //rev = PassInit(logEmpl.ID + "/" + logEmpl.Name, logDept.ID + "/" + logDept.Name, 10);
                    }
                    catch
                    {
                        this.passEnabled = false;
                        return rev;
                    }
                }

                if (rev == 0)
                {
                    this.passEnabled = false;
                    this.err = "������ҩϵͳ ��ʼ��ʧ��! �������Ա��ϵ";
                    return -1;
                }
                if (PassGetState("0") != 0)
                {
                    //PassSetControlParam(1, 2, 0, 2, 1);
                    PassSetControlParam(1, 1, 0, 2, 1);  //�ڶ���������ָ���ǹ���ʷ/����״̬��1�û����룻2����ϵͳ���� 2014-04-26 gumzh�޸�
                    passEnabled = true;
                }
            }
            catch (DllNotFoundException)
            {
                try
                {
                    if (System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "MedicomSoftWare.exe"))
                    {
                        System.Diagnostics.Process.Start(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "MedicomSoftWare.exe");
                        DateTime time = DateTime.Now;
                        while (DateTime.Now < time.AddSeconds(3))
                        {
                        }
                    }
                    //int rev = PassInit(logEmpl.ID + "/" + logEmpl.Name, logDept.ID + "/" + logDept.Name, 10);

                    //if (rev > 0)
                    //{
                    //    return rev;
                    //}
                }
                catch
                {
                }

                //this.err = "δ�ҵ�������ҩϵͳ������������Dll ������ҩϵͳ���޷�����������\n�������ʣ�������Ϣ����ϵ��";
                this.passEnabled = false;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ������ҩ���ܳ�ʼ��ˢ��
        /// </summary>
        /// <returns></returns>
        public int PassRefresh()
        {
            return 1;
        }

        /// <summary>
        /// ��ѯ����ҩƷҪ����ʾ
        /// </summary>
        /// <param name="order"></param>
        /// <param name="LeftTop"></param>
        /// <param name="RightButton"></param>
        /// <param name="isFirst"></param>
        /// <returns></returns>
        public int PassShowSingleDrugInfo(FS.HISFC.Models.Order.Order order, System.Drawing.Point LeftTop, System.Drawing.Point RightButton, bool isFirst)
        {
            if (this.passEnabled && (PassGetState("0") != 0))
            {
                int rev = 0;
                //rev = PassSetPatientInfo(this.myPatient, this.myReciptDoct);
                rev = PassSetQueryDrug(order.Item.ID, order.Item.Name, order.DoseUnit, order.Usage.Name);
                rev = PassSetFloatWinPos(LeftTop.X, LeftTop.Y, RightButton.X, RightButton.Y);
                this.PassShowFloatWindow(true);
            }
            return 1;
        }

        /// <summary>
        /// ��ȡҩƷ������Ϣ
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public int PassShowWarnDrug(FS.HISFC.Models.Order.Order order)
        {
            if (this.passEnabled && (PassGetState("0") != 0))
            {
                try
                {
                    PassSetWarnDrug(order.ID);
                    PassDoCommand(403);
                }
                catch (Exception ex)
                {
                    err = ex.Message;
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// ҩƷ���
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="alOrder"></param>
        /// <param name="isSave">��Ϊ����ʱ�Զ������Ҽ��ֹ����</param>
        /// <returns></returns>
        public int PassDrugCheck(ArrayList alOrder, bool isSave)
        {
            int warn = 0;
            if (isSave)
            {
                if (this.stationType == ServiceTypes.C)
                {
                    //warn = PassDrugCheckBase(alOrder, 1);
                    //warn = PassDrugCheckBase(alOrder, 2);
                    warn = PassDrugCheckBase(alOrder, 33);
                }
                else
                {
                    warn = PassDrugCheckBase(alOrder, 33);
                }
            }
            else
            {
                warn = PassDrugCheckBase(alOrder, 1);
            }

            if (warn == 3)
            {
                if (MessageBox.Show("PASSϵͳ�������ںڵ���ҩ��\r\n�������桢ִ�д�����", "PASS��ȫ��ҩ�����ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// ҩƷ���ͨ�ú���
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="alOrder"></param>
        /// <param name="isSave">��Ϊ����ʱ�Զ������Ҽ��ֹ����</param>
        /// <param name="type">��ѯ���</param>
        /// <returns></returns>
        public int PassDrugCheckBase(ArrayList alOrder, int type)
        {
            /* 1��סԺҽ������վ�����Զ����
             * 33������ҽ������վ�����Զ����
             * 3���ֹ����,�ڵ�������˵��е�"���"����ʱ����
             * 12����ҩ�о�,�ڵ�������˵��е�"���"����ʱ����
             * 6����ҩ����,�ڵ�������˵��е�"��ҩ����"����ʱ����
             * */

            if (!this.passEnabled)
            {
                return 0;
            }
            if ((alOrder == null) || (alOrder.Count == 0))
            {
                return -1;
            }

            this.PassSetPatientInfo(myPatient, myReciptDoct);

            //0-��ɫ��1-��ɫ��2-��ɫ��3-��ɫ��4-��ɫ
            int warn = 1;

            foreach (FS.HISFC.Models.Order.Order order in alOrder)
            {
                if (order == null)
                {
                    this.err = "ִ��ҽ����ҩ���ʱ����! ��������ת������";
                    return -1;
                }
                if ((order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug) && (order.Status != 3))
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
            PassDoCommand(type);

            //��ҩ����
            if (type == 6)
            {
                PassSetWarnDrug((alOrder[0] as FS.HISFC.Models.Order.Order).ID);
            }
            //��鹦��
            else if (type == 1 || type == 2 || type == 3 || type == 33 || type == 34)
            {
                foreach (FS.HISFC.Models.Order.Order order in alOrder)
                {
                    if (order == null)
                    {
                        this.err = "ִ��ҽ����ҩ���ʱ����! ��������ת������";
                        return -1;
                    }
                    if ((order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug) && (order.Status != 3))
                    {
                        int rev = PassGetWarn(order.ID);
                        if (warn < rev)
                        {
                            warn = rev;
                        }
                    }
                }
            }

            return 1;
        }

        /// <summary>
        /// �˳�PASS
        /// </summary>
        /// <returns></returns>
        public int PassClose()
        {
            if (this.passEnabled && (PassGetState("0") != 0))
            {
                return PassQuit();
            }
            return -1;
        }

        /// <summary>
        /// ���ø��������Ƿ���ʾ
        /// </summary>
        /// <param name="isShow"></param>
        public int PassShowFloatWindow(bool isShow)
        {
            if (this.passEnabled && (PassGetState("0") != 0))
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
            return 1;
        }

        /// <summary>
        /// ��ѯ�˵�
        /// </summary>
        ArrayList alMenu = null;

        /// <summary>
        /// �������ƻ�ò�ѯ���ID
        /// </summary>
        /// <param name="queryTypeName"></param>
        /// <returns></returns>
        private int GetQueryTypeID(string queryTypeName)
        {
            if (alMenu == null || alMenu.Count == 0)
            {
                return 0;
            }
            foreach (TreeNode node in alMenu)
            {
                if (node.Tag == null)
                {
                    foreach (TreeNode secondNode in node.Nodes)
                    {
                        if (secondNode.Text.Trim() == queryTypeName.Trim())
                        {
                            return FS.FrameWork.Function.NConvert.ToInt32(secondNode.Tag);
                        }
                    }
                }

                if (node.Text.Trim() == queryTypeName.Trim())
                {
                    return FS.FrameWork.Function.NConvert.ToInt32(node.Tag);
                }
            }
            return 0;
        }

        /// <summary>
        /// ��ȡ��ѯ����
        /// </summary>
        /// <param name="order">ҽ��</param>
        /// <param name="queryType">��ѯ�������</param>
        /// <param name="alShowMenu">��ѯ�˵��б�</param>
        /// <returns></returns>
        public int PassShowOtherInfo(FS.HISFC.Models.Order.Order order, FS.FrameWork.Models.NeuObject queryType, ref ArrayList alShowMenu)
        {
            try
            {
                if (queryType != null)
                {
                    int queryTypeID = GetQueryTypeID(queryType.Name);
                    if (queryTypeID == 0)
                    {
                        return 1;
                    }
                    PassShowFloatWindow(false);
                    PassSetQueryDrug(order.Item.ID, order.Item.Name, order.DoseUnit, order.Usage.Name);
                    DoCommand(FS.FrameWork.Function.NConvert.ToInt32(queryTypeID));
                }
                else
                {
                    if (alMenu != null)
                    {
                        alShowMenu = alMenu;
                    }
                    else
                    {
                        TreeNode passTemp = new TreeNode();

                        #region һ���˵�
                        TreeNode pass22 = new TreeNode("����ʷ/����״̬");
                        pass22.Tag = 22;

                        TreeNode pass101 = new TreeNode("ҩ���ٴ���Ϣ�ο�");
                        pass101.Tag = 101;

                        TreeNode pass102 = new TreeNode("ҩƷ˵����");
                        pass102.Tag = 102;

                        TreeNode pass103 = new TreeNode("������ҩ����");
                        pass103.Tag = 103;

                        TreeNode pass104 = new TreeNode("ҩ�����ֵ");
                        pass104.Tag = 104;

                        TreeNode pass105 = new TreeNode("ҽԺҩƷ��Ϣ");
                        pass105.Tag = 105;

                        TreeNode pass106 = new TreeNode("ҽҩ��Ϣ����");
                        pass106.Tag = 106;

                        TreeNode pass107 = new TreeNode("�й�ҩ��");
                        pass107.Tag = 107;

                        TreeNode pass = new TreeNode("ר����Ϣ");

                        TreeNode pass13 = new TreeNode("ҩƷ�����Ϣ");
                        pass13.Tag = 13;

                        TreeNode pass14 = new TreeNode("��ҩ;�������Ϣ");
                        pass14.Tag = 14;

                        TreeNode pass11 = new TreeNode("ϵͳ����");
                        pass11.Tag = 11;

                        TreeNode pass12 = new TreeNode("��ҩ�о�");
                        pass12.Tag = 12;

                        TreeNode pass6 = new TreeNode("����");
                        pass6.Tag = 6;

                        TreeNode pass3 = new TreeNode("���");
                        pass3.Tag = 3;

                        TreeNode pass301 = new TreeNode("����");
                        pass301.Tag = 301;

                        alMenu = new ArrayList();
                        alMenu.Add(pass22);
                        alMenu.Add(passTemp);
                        alMenu.Add(pass101);
                        alMenu.Add(pass102);
                        alMenu.Add(pass103);
                        alMenu.Add(pass104);
                        alMenu.Add(pass105);
                        alMenu.Add(passTemp);
                        alMenu.Add(pass106);
                        alMenu.Add(passTemp);
                        alMenu.Add(pass107);

                        alMenu.Add(passTemp);
                        alMenu.Add(pass);
                        alMenu.Add(passTemp);

                        alMenu.Add(pass13);
                        alMenu.Add(pass14);
                        alMenu.Add(passTemp);
                        alMenu.Add(pass11);
                        alMenu.Add(passTemp);
                        alMenu.Add(pass12);
                        alMenu.Add(passTemp);
                        alMenu.Add(pass6);
                        alMenu.Add(pass3);
                        alMenu.Add(passTemp);
                        alMenu.Add(pass301);

                        #endregion

                        #region �����˵� ר����Ϣ

                        TreeNode pass201 = new TreeNode("ҩ��-ҩ���໥����");
                        pass201.Tag = 201;

                        TreeNode pass202 = new TreeNode("ҩ��-ʳ���໥����");
                        pass202.Tag = 202;

                        TreeNode pass203 = new TreeNode("����ע�����������");
                        pass203.Tag = 203;

                        TreeNode pass204 = new TreeNode("����ע�����������");
                        pass204.Tag = 204;

                        TreeNode pass205 = new TreeNode("����֢");
                        pass205.Tag = 205;

                        TreeNode pass206 = new TreeNode("������");
                        pass206.Tag = 206;

                        TreeNode pass207 = new TreeNode("��������ҩ");
                        pass207.Tag = 207;

                        TreeNode pass208 = new TreeNode("��ͯ��ҩ");
                        pass208.Tag = 208;

                        TreeNode pass209 = new TreeNode("��������ҩ");
                        pass209.Tag = 209;

                        TreeNode pass210 = new TreeNode("��������ҩ");
                        pass210.Tag = 210;

                        TreeNode pass220 = new TreeNode("�ٴ�������Ϣ�ο�");
                        pass220.Tag = 220;

                        pass.Nodes.Add(pass201);
                        pass.Nodes.Add(pass202);
                        pass.Nodes.Add(passTemp);
                        pass.Nodes.Add(pass203);
                        pass.Nodes.Add(pass204);
                        pass.Nodes.Add(passTemp);
                        pass.Nodes.Add(pass205);
                        pass.Nodes.Add(pass206);
                        pass.Nodes.Add(passTemp);
                        pass.Nodes.Add(pass207);
                        pass.Nodes.Add(pass208);
                        pass.Nodes.Add(pass209);
                        pass.Nodes.Add(pass210);
                        pass.Nodes.Add(pass220);

                        #endregion

                        foreach (TreeNode node in alMenu)
                        {
                            if (node == null)
                            {
                                continue;
                            }

                            if (node.Tag == null)
                            {
                                foreach (TreeNode secondNode in node.Nodes)
                                {
                                    if (node == null)
                                    {
                                        continue;
                                    }
                                    if (secondNode.Tag != null && PassGetState(secondNode.Tag.ToString()) == 0)
                                    {
                                        secondNode.Tag = "������";
                                    }
                                }
                            }
                            else
                            {
                                try
                                {
                                    if (node.Tag != null && PassGetState(node.Tag.ToString()) == 0)
                                    {
                                        node.Tag = "������";
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }

                        alShowMenu = alMenu;
                    }
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return -1;
            }

            return 1;
        }

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
            if (this.passEnabled && (PassGetState("0") != 0))
            {
                return PassDoCommand(commandType);
            }
            return -4;
        }

        /// <summary>
        /// ��ȡPASS�������ʾ����
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public int PassGetWarnFlag(string orderId)
        {
            if (this.passEnabled && (PassGetState("0") != 0))
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
            if (this.passEnabled && (PassGetState("0") != 0))
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
        /// ��Ӻ�����ҩ���������־
        /// </summary>
        /// <param name="iRow">������������</param>
        /// <param name="iSheet">������Sheet����</param>
        /// <param name="warnFlag">������־</param>
        public void AddWarnPicturn(int iRow, int iSheet, int warnFlag)
        {
            //string picturePath = Application.StartupPath + "\\pic";
            //switch (warnFlag)
            //{
            //    case 0:										//0 (��ɫ)������
            //        picturePath = picturePath + "\\warn0.gif";
            //        break;
            //    case 1:										//1 (��ɫ)Σ���ϵͻ��в���ȷ
            //        picturePath = picturePath + "\\warn1.gif";
            //        break;
            //    case 2:										//2 (��ɫ)���Ƽ��������Σ��
            //        picturePath = picturePath + "\\warn2.gif";
            //        break;
            //    case 3:										// 3 (��ɫ)���Խ��ɡ������������Σ��
            //        picturePath = picturePath + "\\warn3.gif";
            //        break;
            //    case 4:										//4 (��ɫ)���û���һ��Σ�� 
            //        picturePath = picturePath + "\\warn4.gif";
            //        break;
            //    default:
            //        break;
            //}
            //if (!System.IO.File.Exists(picturePath))
            //{
            //    return;
            //}
            //try
            //{
            //    FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
            //    FarPoint.Win.Picture pic = new FarPoint.Win.Picture();
            //    pic.Image = System.Drawing.Image.FromFile(picturePath, true);
            //    pic.TransparencyColor = System.Drawing.Color.Empty;
            //    t.BackgroundImage = pic;
            //    this.neuSpread1.Sheets[iSheet].Cells[iRow, GetColumnIndexFromName("��")].CellType = t;			//ҽ������
            //    this.neuSpread1.Sheets[iSheet].Cells[iRow, GetColumnIndexFromName("��")].Tag = "1";							//���������
            //    this.neuSpread1.Sheets[iSheet].Cells[iRow, GetColumnIndexFromName("��")].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("���ú�����ҩ�������ʾ�����г���!" + ex.Message);
            //}
        }

        /// <summary>
        /// ���벡����Ϣ
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="docID"></param>
        /// <param name="docName"></param>
        public int PassSetPatientInfo(Patient patientObj, FS.FrameWork.Models.NeuObject recipeDoct)
        {
            if (((patientObj != null) && this.passEnabled) && (PassGetState("0") != 0))
            {
                myPatient = patientObj;
                myReciptDoct = recipeDoct;

                string docID = recipeDoct.ID;
                string docName = recipeDoct.Name;

                if (stationType == ServiceTypes.I)
                {
                    #region סԺ������Ϣ

                    FS.HISFC.Models.RADT.PatientInfo patient = patientObj as FS.HISFC.Models.RADT.PatientInfo;

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
                    if ((patient.Sex.ID.ToString() == "F") || (patient.Sex.ID.ToString() == "M"))
                    {
                        sex = patient.Sex.Name;
                    }
                    else
                    {
                        sex = "δ֪";
                    }
                    string birthday = patient.Birthday.ToString("yyyy-MM-dd");
                    string weight = patient.Weight;
                    string height = patient.Height;
                    //string departMentName = patient.PVisit.PatientLocation.Dept.ID + "/" + patient.PVisit.PatientLocation.Dept.Name;
                    string departMentName = ((FS.HISFC.Models.Base.Employee)recipeDoct).Dept.ID + "/" + ((FS.HISFC.Models.Base.Employee)recipeDoct).Dept.Name;
                    string doctor = recipeDoct.ID + "/" + recipeDoct.Name;
                    string leaveHospitalDate = "";
                    PassDoCommand(402);
                    PassSetPatientInfo(patientNO, str2, name, sex, birthday, weight, height, departMentName, doctor, leaveHospitalDate);

                    #endregion
                }
                else
                {
                    #region ���ﻼ����Ϣ

                    FS.HISFC.Models.Registration.Register patient = patientObj as FS.HISFC.Models.Registration.Register;

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
                    if ((patient.Sex.ID.ToString() == "F") || (patient.Sex.ID.ToString() == "M"))
                    {
                        sex = patient.Sex.Name;
                    }
                    else
                    {
                        sex = "δ֪";
                    }
                    string birthday = patient.Birthday.ToString("yyyy-MM-dd");
                    string weight = patient.Weight;
                    string height = patient.Height;
                    //string departMentName = patient.PVisit.PatientLocation.Dept.ID + "/" + patient.PVisit.PatientLocation.Dept.Name;
                    string departMentName = ((FS.HISFC.Models.Base.Employee)recipeDoct).Dept.ID + "/" + ((FS.HISFC.Models.Base.Employee)recipeDoct).Dept.Name;
                    string doctor = recipeDoct.ID + "/" + recipeDoct.Name;
                    string leaveHospitalDate = "";
                    PassDoCommand(402);
                    PassSetPatientInfo(patientNO, str2, name, sex, birthday, weight, height, departMentName, doctor, leaveHospitalDate);

                    //���ù���ʷ
                    //PassSetAllergenInfo("0", "Y00000000002", "ע������ù����", "USER_Drug", "");
                    PassSetAllergenInfo(patientObj);

                    #endregion
                }
            }
            return 1;
        }

        /// <summary>
        /// ���봦����ҽ������Ϣ
        /// </summary>
        /// <param name="order"></param>
        public void PassSetRecipeInfo(FS.HISFC.Models.Order.Order orderObj)
        {
            if (this.passEnabled && ((orderObj != null)
                && (orderObj.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)))
            {
                if (stationType == ServiceTypes.I)
                {
                    #region סԺҽ��

                    FS.HISFC.Models.Order.Inpatient.Order order = orderObj as FS.HISFC.Models.Order.Inpatient.Order;

                    //string applyNO = order.ApplyNo;
                    string applyNO = order.ID; //ҽ��Ψһ���

                    string iD = order.Item.ID;//��Ŀ����

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
                            FS.HISFC.BizLogic.Manager.Frequency frequency = new FS.HISFC.BizLogic.Manager.Frequency();
                            order.Frequency = (FS.HISFC.Models.Order.Frequency)frequency.Get(order.Frequency, "ROOT");
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
                        //string orderDoctor = order.Doctor.ID + "/" + order.Doctor.Name;
                        string orderDoctor = order.ReciptDoctor.ID + "/" + order.ReciptDoctor.Name;
                        PassSetRecipeInfo(applyNO, iD, name, singleDose, doseUnit, str6, startOrderDate, stopOrderDate, routeName, groupTag, orderType, orderDoctor);
                    }
                    #endregion
                }
                else
                {
                    #region ���ﴦ��

                    FS.HISFC.Models.Order.OutPatient.Order order = orderObj as FS.HISFC.Models.Order.OutPatient.Order;

                    //string applyNO = order.ApplyNo;
                    string applyNO = order.ID; //ҽ��Ψһ���

                    //string iD = order.Item.UserCode;
                    string iD = order.Item.ID;  //��Ŀ����

                    string name = order.Item.Name;
                    string singleDose = order.DoseOnce.ToString();
                    string doseUnit = order.DoseUnit;
                    int length = 0;
                    string str7 = "";
                    if ((order.Frequency != null) && (order.Usage != null))
                    {
                        if (order.Frequency.Time == null)
                        {
                            FS.HISFC.BizLogic.Manager.Frequency frequency = new FS.HISFC.BizLogic.Manager.Frequency();
                            order.Frequency = (FS.HISFC.Models.Order.Frequency)frequency.Get(order.Frequency, "ROOT");
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
                        string orderType = "1";
                        //string orderDoctor = order.Doctor.ID + "/" + order.Doctor.Name;
                        string orderDoctor = order.ReciptDoctor.ID + "/" + order.ReciptDoctor.Name;

                        PassSetRecipeInfo(applyNO, iD, name, singleDose, doseUnit, str6, startOrderDate, stopOrderDate, routeName, groupTag, orderType, orderDoctor);
                    }

                    #endregion
                }
            }
        }

        /// <summary>
        /// ���û��ߵĹ���ʷ
        /// </summary>
        /// <param name="patientObj"></param>
        public void PassSetAllergenInfo(Patient patientObj)
        {
            if (((patientObj != null) && this.passEnabled) && (PassGetState("0") != 0))
            {
                if (stationType == ServiceTypes.C)
                {
                    #region �������ʷ

                    try
                    {
                        FS.HISFC.Models.Registration.Register patient = patientObj as FS.HISFC.Models.Registration.Register;
                        string patientKind = "1";
                        string cardNO = patient.PID.CardNO;
                        FS.HISFC.BizLogic.Order.Medical.AllergyManager allergyMgr = new FS.HISFC.BizLogic.Order.Medical.AllergyManager();
                        ArrayList al = allergyMgr.QueryAllergyInfo(cardNO, patientKind);
                        if (al != null && al.Count > 0)
                        {
                            int index = 0;
                            foreach (FS.HISFC.Models.Order.Medical.AllergyInfo allergyInfo in al)
                            {
                                if (allergyInfo.Allergen.ID.StartsWith("Y"))
                                {
                                    //����Ҫ����ͬ��ҩƷHIS���룻ҩƷHIS���ƣ���ԴHIS(USER_Drug)����ע
                                    PassSetAllergenInfo(index.ToString(), allergyInfo.Allergen.ID, allergyInfo.Allergen.Name, "USER_Drug", allergyInfo.Remark);
                                    index++;
                                }
                            }
                        }
                    }
                    catch (Exception ex) { }


                    #endregion
                }
            }
        }

        #endregion

        #region IReasonableMedicine ��Ա


        public int PassSetDiagnoses(ArrayList diagnoseList)
        {
            return 1;
        }

        #endregion
    }
}
