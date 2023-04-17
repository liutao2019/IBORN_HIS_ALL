using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections;

namespace Neusoft.HISFC.Components.RADT.Controls
{
    /// <summary>
    /// [��������: ��Ժ�����б�]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2008-09-3]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class tvOutHosList : Neusoft.HISFC.Components.Common.Controls.tvPatientList
    {
        #region ��ʼ��

        public tvOutHosList()
        {
            InitializeComponent();
        }

        public tvOutHosList(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #endregion      

        #region ����

        Neusoft.HISFC.BizProcess.Integrate.RADT manager = null;
        Neusoft.HISFC.BizLogic.RADT.InPatient radtManager = new Neusoft.HISFC.BizLogic.RADT.InPatient();
        Neusoft.HISFC.Models.Base.Employee employee = Neusoft.FrameWork.Management.Connection.Operator as Neusoft.HISFC.Models.Base.Employee;
        //��Ժ�ٻص���Ч����
        private int callBackVaildDays;
        public const string control_id = "ZY0001";
        #endregion

        #region ����

        /// <summary>
        /// ��ʼ�����Ʋ���,��ó�Ժ�ٻص���Ч����
        /// </summary>
        private void InitControlParam()
        {
            Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();
            this.callBackVaildDays = ctrlParamIntegrate.GetControlParam<int>(control_id, true, 1);
        }

        /// <summary>
        /// ˢ��
        /// </summary>
        public void Refresh(string deptCode, string beginTime, string endTime)
        {
            if(beginTime==null)
            {
                beginTime = System.DateTime.Now.AddDays(-3).ToShortDateString();
            }
            if(endTime==null)
            {
                endTime = System.DateTime.Now.AddDays(4).ToShortDateString();
            }
            this.BeginUpdate();
            this.Nodes.Clear();
            if (manager == null)
                manager = new Neusoft.HISFC.BizProcess.Integrate.RADT();


            ArrayList al = new ArrayList();//�����б�

            //��ʾ��Ժ�Ǽ�δ����Ч�ٻ��ڵĻ���
            al.Add("δ����Ч�ٻ��ڻ���|" + EnumPatientState.InVaildDayPatient.ToString());
            addPatientList(al,deptCode, beginTime, endTime, EnumPatientState.InVaildDayPatient);

            //��ʾ��Ժ�Ǽ��ѹ���Ч�ٻ��ڵĻ���
            al.Add("�ѹ���Ч�ٻ��ڻ���|" + EnumPatientState.OutVaildDayPatient.ToString());
            addPatientList(al,deptCode,beginTime, endTime, EnumPatientState.OutVaildDayPatient);

            //��ʾ���л����б�
            this.SetPatient(al);
            this.EndUpdate();

        }

        /// <summary>
        /// ���ݲ���վ�õ�����
        /// </summary>
        /// <param name="al"></param>
        private void addPatientList(ArrayList al,string deptCode, string beginTime, string endTime, EnumPatientState patientState)
        {
            ArrayList al1 = new ArrayList();
            InitControlParam();
            int myPatientState;
            if (patientState == EnumPatientState.InVaildDayPatient)
            {
                myPatientState = 0;
            }
            else if (patientState == EnumPatientState.OutVaildDayPatient)
            {
                myPatientState = 1;
            }
            else
            {
                myPatientState = 2;
            }
                al1 = this.manager.QueryOutHosPatient(deptCode, beginTime, endTime, callBackVaildDays, myPatientState);
                al.AddRange(al1);

        }
    }
        #endregion
    public enum EnumPatientState
    {
        InVaildDayPatient=0,//����Ч�ٻ����ڵĻ���
        OutVaildDayPatient=1,//������Ч�ٻ����ڵĻ���
        OutHos=2 //��Ժ�Ǽǻ���
 
    }
}
