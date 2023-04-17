using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections;
namespace Neusoft.HISFC.Components.RADT.Controls
{
    /// <summary>
    /// [��������: ��ʿվ�����б�]<br></br>
    /// ��������ר�ã��������ߡ������ﻼ�ߡ�ת�뻼�ߡ�ת�����ߡ���Ժ����
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2006-11-30]<br></br>
    /// <�޸ļ�¼
    ///		�޸���='����'
    ///		�޸�ʱ��='2008-09-3'
    ///		�޸�Ŀ��='���Ƴ�Ժ�ٻػ��ߵ���Ч����'
    ///		�޸�����='�ڳ�Ժ�����б���ֻ��ʾ����Ч�����ڵĻ����б���Ϣ'
    ///  />
    /// </summary>
    public partial class tvNursePatientList : Neusoft.HISFC.Components.Common.Controls.tvPatientList
    {
        #region ��ʼ��

        public tvNursePatientList()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                Init();
            }
        }

        public tvNursePatientList(string type)
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.pateintType = type;
                Init();
            }
        }

        public tvNursePatientList(IContainer container)
            : this()
        {
            container.Add(this);

            //InitializeComponent();
        }

        #endregion

        #region ����

        Neusoft.HISFC.BizProcess.Integrate.RADT radtIntegrate = null;
        Neusoft.HISFC.BizLogic.RADT.InPatient radtManager = new Neusoft.HISFC.BizLogic.RADT.InPatient();

        Neusoft.HISFC.BizProcess.Integrate.Manager interMgr = new Neusoft.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ���������Ŀ����б�
        /// </summary>
        private ArrayList alDepts = null;

        /// <summary>
        /// ��ǰ����Ա
        /// </summary>
        Neusoft.HISFC.Models.Base.Employee oper = Neusoft.FrameWork.Management.Connection.Operator as Neusoft.HISFC.Models.Base.Employee;

        /// <summary>
        /// �б���صĻ�������
        /// �洢ö�ٵ���ֵ����������|����
        /// </summary>
        private string pateintType = "ALL";

        /// <summary>
        /// �б���صĻ�������
        /// �洢ö�ٵ���ֵ����������|����
        /// </summary>
        public string PateintType
        {
            get
            {
                return pateintType;
            }
            set
            {
                pateintType = value;
            }
        }

        #endregion

        #region ��Ժ�ٻ��������� Ŀǰ����

        /// <summary>
        /// ��Ժ�ٻص���Ч����
        /// </summary>
        private int callBackVaildDays;

        /// <summary>
        /// ��Ժ�ٻ����� ���Ʋ���
        /// </summary>
        public const string control_id = "ZY0001";

        /// <summary>
        /// ��ʼ�����Ʋ���,��ó�Ժ�ٻص���Ч����
        /// </summary>
        private void InitControlParam()
        {
            Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();
            this.callBackVaildDays = ctrlParamIntegrate.GetControlParam<int>(control_id, false, 1);
        }

        #endregion

        /// <summary>
        /// ��ȡ���������Ŀ����б�
        /// </summary>
        /// <param name="nurseCode"></param>
        /// <returns></returns>
        private ArrayList GetDepts(string nurseCode)
        {
            if (alDepts == null)
            {
                alDepts = interMgr.QueryDepartment(nurseCode);

            }
            return alDepts;
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        private int Init()
        {
            this.ShowType = enuShowType.Bed;
            this.Direction = enuShowDirection.Ahead;

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
            {
                return -1;
            }

            InitControlParam();

            this.Refresh();
            return 1;
        }

        /// <summary>
        /// ˢ��
        /// </summary>
        public new void Refresh()
        {
            try
            {
                this.BeginUpdate();
                this.Nodes.Clear();

                if (radtIntegrate == null)
                {
                    radtIntegrate = new Neusoft.HISFC.BizProcess.Integrate.RADT();
                }

                ArrayList alPatientList = new ArrayList();//�����б�

                Dictionary<string, object> dicPaient = new Dictionary<string, object>();

                if (pateintType.ToUpper() == "ALL")
                {
                    ////������Ժ����
                    //alPatientList.AddRange(GetPatientList(EnumPatientType.In));

                    ////��ʾ������վ�����仼��
                    //alPatientList.AddRange(GetPatientList(EnumPatientType.Arrive));

                    ////��ʾת�뱾����վ�����仼��
                    //alPatientList.AddRange(GetPatientList(EnumPatientType.ShiftIn));

                    ////��ʾ������վת������Ļ���
                    //alPatientList.AddRange(GetPatientList(EnumPatientType.ShiftOut));

                    ////��ʾ������վ��Ժ�ǼǵĻ���
                    //alPatientList.AddRange(GetPatientList(EnumPatientType.Out));


                    GetPatientListNew(EnumPatientType.In, ref dicPaient);
                    GetPatientListNew(EnumPatientType.Arrive, ref dicPaient);
                    GetPatientListNew(EnumPatientType.ShiftIn, ref dicPaient);
                    GetPatientListNew(EnumPatientType.ShiftOut, ref dicPaient);
                    GetPatientListNew(EnumPatientType.Out, ref dicPaient);
                }
                else
                {
                    string[] types = pateintType.Split('|');

                    for (int i = 0; i < types.Length; i++)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(types[i])
                                && Enum.IsDefined(typeof(EnumPatientType), Neusoft.FrameWork.Function.NConvert.ToInt32(types[i])))
                            {
                                EnumPatientType paitentType = (EnumPatientType)Neusoft.FrameWork.Function.NConvert.ToInt32(types[i]);

                                //alPatientList.AddRange(GetPatientList(paitentType));
                                GetPatientListNew(paitentType, ref dicPaient);
                            }
                        }
                        catch
                        {
                        }
                    }
                }

                //��ʾ���л����б�
                //this.SetPatient(alPatientList);

                SetPatient(dicPaient);

                this.EndUpdate();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("ˢ�´�λ�б����\r\n" + ex.Message, "����", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ��ȡ����������
        /// </summary>
        /// <param name="al"></param>
        /// <param name="patientType"></param>
        private ArrayList GetPatientList(EnumPatientType patientType)
        {
            if (oper == null)
            {
                return null;
            }

            ArrayList alPatientList = new ArrayList();

            ArrayList al1 = new ArrayList();

            Neusoft.HISFC.Models.Base.EnumInState Status = Neusoft.HISFC.Models.Base.EnumInState.I;

            Neusoft.HISFC.Models.Base.Bed bedInfo = null;

            #region ������Ժ����
            if (patientType == EnumPatientType.In)
            {
                Status = Neusoft.HISFC.Models.Base.EnumInState.I;

                //��½�Ŀ����Ƿ��ǲ���
                //����һ�����Ҷ�Ӧ�������ʱ��employee.Nurse.ID����ǵ�½������Ϣ
                bool isNureseDept = true;

                al1 = this.radtManager.PatientQueryByNurseCell(oper.Nurse.ID, Status);

                ArrayList alDept = this.GetDepts(oper.Nurse.ID);

                //����һ�����Ҷ�Ӧ�������ʱ��employee.Nurse.ID����ǵ�½������Ϣ
                //��ʱ�����½���ң������ǲ��������ѯ��������
                if (al1.Count == 0)
                {
                    isNureseDept = false;
                    foreach (Neusoft.FrameWork.Models.NeuObject objdept in alDept)
                    {
                        al1.AddRange(this.radtManager.PatientQueryByNurseCell(objdept.ID, Status));
                    }
                }

                if (alDept == null || alDept.Count < 2)
                {
                    //alPatientList.AddRange(al1);

                    //���ӻ�������ʾ<������,�����б�>
                    Dictionary<string, ArrayList> dicNurseList = new Dictionary<string, ArrayList>();
                    foreach (Neusoft.HISFC.Models.RADT.PatientInfo pInfo in al1)
                    {
                        bedInfo = interMgr.GetBed(pInfo.PVisit.PatientLocation.Bed.ID);
                        string tendGroup = "";
                        if (bedInfo != null)
                        {
                            tendGroup = bedInfo.TendGroup;
                        }
                        if (dicNurseList.ContainsKey(tendGroup))
                        {
                            ArrayList alTend = dicNurseList[tendGroup];
                            alTend.Add(pInfo);
                            dicNurseList[tendGroup] = alTend;
                        }
                        else
                        {
                            ArrayList alTend = new ArrayList();
                            alTend.Add(pInfo);
                            dicNurseList.Add(tendGroup, alTend);
                        }
                    }

                    //û��ά��������ʱ������ʾ������
                    if (dicNurseList.Keys.Count == 1)
                    {
                        foreach (string key in dicNurseList.Keys)
                        {
                            alPatientList.Add("��������|" + EnumPatientType.In.ToString());
                            alPatientList.AddRange(dicNurseList[key]);
                        }
                    }
                    else
                    {
                        foreach (string key in dicNurseList.Keys)
                        {
                            if (string.IsNullOrEmpty(key))
                            {
                                alPatientList.Add("��" + "|" + EnumPatientType.In.ToString());
                            }
                            else
                            {
                                alPatientList.Add(key + "|" + EnumPatientType.In.ToString());
                            }
                            alPatientList.AddRange(dicNurseList[key]);
                        }
                    }
                }
                else
                {
                    Neusoft.FrameWork.Models.NeuObject objdept = null;
                    Neusoft.HISFC.Models.RADT.PatientInfo patientTemp = null;

                    for (int i = 0; i < alDept.Count; i++)
                    {
                        objdept = alDept[i] as Neusoft.FrameWork.Models.NeuObject;

                        alPatientList.Add(objdept.Name + "|" + EnumPatientType.In.ToString());

                        //���ӻ�������ʾ<������,�����б�>
                        Dictionary<string, ArrayList> dicNurseList = new Dictionary<string, ArrayList>();

                        for (int j = 0; j < al1.Count; j++)
                        {
                            patientTemp = al1[j] as Neusoft.HISFC.Models.RADT.PatientInfo;

                            if (isNureseDept)
                            {
                                if (patientTemp.PVisit.PatientLocation.Dept.ID.Trim() != objdept.ID.Trim())
                                {
                                    continue;
                                }
                                //if (patientTemp.PVisit.PatientLocation.Dept.ID.Trim() == objdept.ID.Trim())
                                //{
                                //    alPatientList.Add(patientTemp);
                                //}
                            }
                            else
                            {
                                if (patientTemp.PVisit.PatientLocation.NurseCell.ID.Trim() != objdept.ID.Trim())
                                {
                                    continue; ;
                                }
                                //if (patientTemp.PVisit.PatientLocation.NurseCell.ID.Trim() == objdept.ID.Trim())
                                //{
                                //    alPatientList.Add(patientTemp);
                                //}
                            }

                            bedInfo = interMgr.GetBed(patientTemp.PVisit.PatientLocation.Bed.ID);
                            string tendGroup = "";
                            if (bedInfo != null)
                            {
                                tendGroup = bedInfo.TendGroup;
                            }

                            if (dicNurseList.ContainsKey(tendGroup))
                            {
                                ArrayList alTend = dicNurseList[tendGroup];
                                alTend.Add(patientTemp);
                                dicNurseList[tendGroup] = alTend;
                            }
                            else
                            {
                                ArrayList alTend = new ArrayList();
                                alTend.Add(patientTemp);
                                dicNurseList.Add(tendGroup, alTend);
                            }
                        }

                        //û��ά��������ʱ������ʾ������
                        if (dicNurseList.Keys.Count == 1)
                        {
                            foreach (string key in dicNurseList.Keys)
                            {
                                if (string.IsNullOrEmpty(key))
                                {
                                    alPatientList.AddRange(dicNurseList[key]);
                                }
                            }
                        }
                        else
                        {
                            foreach (string key in dicNurseList.Keys)
                            {
                                if (string.IsNullOrEmpty(key))
                                {
                                    alPatientList.Add("��" + "|" + EnumPatientType.In.ToString());
                                }
                                else
                                {
                                    alPatientList.Add(key + "|" + EnumPatientType.In.ToString());
                                }
                                alPatientList.AddRange(dicNurseList[key]);
                            }
                        }
                    }
                }
            }
            #endregion

            #region �����ﻼ��

            else if (patientType == EnumPatientType.Arrive)
            {
                Status = Neusoft.HISFC.Models.Base.EnumInState.R;
                alPatientList.Add("�����ﻼ��|" + EnumPatientType.Arrive.ToString());
                al1 = this.radtIntegrate.QueryPatientByNurseCellAndState(oper.Nurse.ID, Status);//�����ҽ���
                alPatientList.AddRange(al1);
            }
            #endregion

            #region ת������

            else if (patientType == EnumPatientType.ShiftOut)
            {
                Status = Neusoft.HISFC.Models.Base.EnumInState.I;
                alPatientList.Add("ת������|" + EnumPatientType.ShiftOut.ToString());
                al1 = this.radtManager.QueryPatientShiftOutApplyByNurseCell(oper.Nurse.ID, "1");
                alPatientList.AddRange(al1);
            }
            #endregion

            #region ת�뻼��

            else if (patientType == EnumPatientType.ShiftIn)
            {
                Status = Neusoft.HISFC.Models.Base.EnumInState.I;
                alPatientList.Add("ת�뻼��|" + EnumPatientType.ShiftIn.ToString());
                al1 = this.radtManager.QueryPatientShiftInApplyByNurseCell(oper.Nurse.ID, "1");				//�����Ҳ�ת�������
                alPatientList.AddRange(al1);

            }
            #endregion

            #region ��Ժ�Ǽǻ���

            else if (patientType == EnumPatientType.Out)
            {
                Status = Neusoft.HISFC.Models.Base.EnumInState.B;
                alPatientList.Add("��Ժ�Ǽǻ���|" + EnumPatientType.Out.ToString());
                //���ݳ�Ժ�ٻص���Ч������ѯ��Ժ�Ǽǻ�����Ϣ
                al1 = this.radtManager.PatientQueryByNurseCellVaildDate(oper.Nurse.ID, Status, callBackVaildDays);
                alPatientList.AddRange(al1);
            }
            #endregion

            return alPatientList;
        }

        /// <summary>
        /// ��ȡ����������
        /// </summary>
        /// <param name="al"></param>
        /// <param name="patientType"></param>
        private int GetPatientListNew(EnumPatientType patientType, ref Dictionary<string, object> dicPatient)
        {
            if (oper == null)
            {
                return -1;
            }

            ArrayList alPatientList = new ArrayList();

            ArrayList al1 = new ArrayList();

            Neusoft.HISFC.Models.Base.Bed bedInfo = null;

            Neusoft.HISFC.Models.Base.EnumInState Status = Neusoft.HISFC.Models.Base.EnumInState.I;

            #region ������Ժ����

            if (patientType == EnumPatientType.In)
            {
                Status = Neusoft.HISFC.Models.Base.EnumInState.I;

                //��½�Ŀ����Ƿ��ǲ���
                //����һ�����Ҷ�Ӧ�������ʱ��employee.Nurse.ID����ǵ�½������Ϣ
                bool isNureseDept = true;

                al1 = this.radtManager.PatientQueryByNurseCell(oper.Nurse.ID, Status);

                ArrayList alDept = this.GetDepts(oper.Nurse.ID);

                //����һ�����Ҷ�Ӧ�������ʱ��employee.Nurse.ID����ǵ�½������Ϣ
                //��ʱ�����½���ң������ǲ��������ѯ��������
                if (al1.Count == 0)
                {
                    isNureseDept = false;
                    foreach (Neusoft.FrameWork.Models.NeuObject objdept in alDept)
                    {
                        al1.AddRange(this.radtManager.PatientQueryByNurseCell(objdept.ID, Status));
                    }
                }

                if (alDept == null || alDept.Count < 2)
                {
                    //alPatientList.Add("��������|" + EnumPatientType.In.ToString());

                    //���ӻ�������ʾ<������,�����б�>
                    Dictionary<string, ArrayList> dicNurseList = new Dictionary<string, ArrayList>();
                    foreach (Neusoft.HISFC.Models.RADT.PatientInfo pInfo in al1)
                    {
                        bedInfo = interMgr.GetBed(pInfo.PVisit.PatientLocation.Bed.ID);
                        string tendGroup = "";
                        if (bedInfo != null)
                        {
                            tendGroup = bedInfo.TendGroup + "|" + EnumPatientType.In.ToString();
                        }

                        if (dicNurseList.ContainsKey(tendGroup))
                        {
                            ArrayList alTend = dicNurseList[tendGroup];
                            alTend.Add(pInfo);
                            dicNurseList[tendGroup] = alTend;
                        }
                        else
                        {
                            ArrayList alTend = new ArrayList();
                            alTend.Add(pInfo);
                            dicNurseList.Add(tendGroup, alTend);
                        }
                    }

                    //û��ά��������ʱ������ʾ������
                    if (dicNurseList.Keys.Count == 1)
                    {
                        foreach (string key in dicNurseList.Keys)
                        {
                            //alPatientList.AddRange(dicNurseList[key]);
                            dicPatient.Add("��������" + "|" + EnumPatientType.In.ToString(), dicNurseList[key]);
                        }
                    }
                    else
                    {
                        dicPatient.Add("��������" + "|" + EnumPatientType.In.ToString(), dicNurseList);
                        //foreach (string key in dicNurseList.Keys)
                        //{
                        //    if (string.IsNullOrEmpty(key))
                        //    {
                        //        dicPatient.Add("δ����|TendGroup", dicNurseList[key]);
                        //    }
                        //    else
                        //    {
                        //        alPatientList.Add(key + "|" + EnumPatientType.In.ToString());
                        //        dicPatient.Add(key + "|TendGroup", dicNurseList[key]);
                        //    }
                        //}
                    }
                }
                else
                {
                    Neusoft.FrameWork.Models.NeuObject objdept = null;
                    Neusoft.HISFC.Models.RADT.PatientInfo patientTemp = null;

                    for (int i = 0; i < alDept.Count; i++)
                    {
                        objdept = alDept[i] as Neusoft.FrameWork.Models.NeuObject;

                        //alPatientList.Add(objdept.Name + "|" + EnumPatientType.In.ToString());

                        //���ӻ�������ʾ<������,�����б�>
                        Dictionary<string, ArrayList> dicNurseList = new Dictionary<string, ArrayList>();

                        for (int j = 0; j < al1.Count; j++)
                        {
                            patientTemp = al1[j] as Neusoft.HISFC.Models.RADT.PatientInfo;

                            if (isNureseDept)
                            {
                                if (patientTemp.PVisit.PatientLocation.Dept.ID.Trim() != objdept.ID.Trim())
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                if (patientTemp.PVisit.PatientLocation.NurseCell.ID.Trim() != objdept.ID.Trim())
                                {
                                    continue; ;
                                }
                            }

                            bedInfo = interMgr.GetBed(patientTemp.PVisit.PatientLocation.Bed.ID);
                            string tendGroup = "";
                            if (bedInfo != null)
                            {
                                tendGroup = bedInfo.TendGroup + "|" + EnumPatientType.In.ToString();
                            }

                            //string tendGroup = SOC.HISFC.BizProcess.Cache.Common.GetBedInfo(patientTemp.PVisit.PatientLocation.Bed.ID).TendGroup + "|" + EnumPatientType.In.ToString();
                            if (dicNurseList.ContainsKey(tendGroup))
                            {
                                ArrayList alTend = dicNurseList[tendGroup];
                                alTend.Add(patientTemp);
                                dicNurseList[tendGroup] = alTend;
                            }
                            else
                            {
                                ArrayList alTend = new ArrayList();
                                alTend.Add(patientTemp);
                                dicNurseList.Add(tendGroup, alTend);
                            }
                        }

                        //û��ά��������ʱ������ʾ������
                        if (dicNurseList.Keys.Count == 1)
                        {
                            foreach (string key in dicNurseList.Keys)
                            {
                                dicPatient.Add(objdept.Name + "|" + EnumPatientType.In.ToString(), dicNurseList[key]);
                            }
                        }
                        else
                        {
                            dicPatient.Add(objdept.Name + "|" + EnumPatientType.In.ToString(), dicNurseList);
                            //foreach (string key in dicNurseList.Keys)
                            //{
                            //    if (string.IsNullOrEmpty(key))
                            //    {
                            //        //alPatientList.Add("��" + "|" + EnumPatientType.In.ToString());
                            //        dicPatient.Add(objdept.Name + "|" + objdept.ID, dicNurseList[key]);
                            //    }
                            //    else
                            //    {
                            //        alPatientList.Add(key + "|" + EnumPatientType.In.ToString());
                            //        dicPatient.Add(objdept.Name + "|" + objdept.ID, dicNurseList[key]);
                            //    }
                            //    alPatientList.AddRange(dicNurseList[key]);
                            //}
                        }
                    }
                }
            }
            #endregion

            #region �����ﻼ��

            else if (patientType == EnumPatientType.Arrive)
            {
                Status = Neusoft.HISFC.Models.Base.EnumInState.R;
                //alPatientList.Add("�����ﻼ��|" + EnumPatientType.Arrive.ToString());
                al1 = this.radtIntegrate.QueryPatientByNurseCellAndState(oper.Nurse.ID, Status);//�����ҽ���
                //alPatientList.AddRange(al1);
                dicPatient.Add("�����ﻼ��|" + EnumPatientType.Arrive.ToString(), al1);
            }
            #endregion

            #region ת������

            else if (patientType == EnumPatientType.ShiftOut)
            {
                Status = Neusoft.HISFC.Models.Base.EnumInState.I;
                alPatientList.Add("ת������|" + EnumPatientType.ShiftOut.ToString());
                al1 = this.radtManager.QueryPatientShiftOutApplyByNurseCell(oper.Nurse.ID, "1");
                alPatientList.AddRange(al1);
                dicPatient.Add("ת������|" + EnumPatientType.ShiftOut.ToString(), al1);
            }
            #endregion

            #region ת�뻼��

            else if (patientType == EnumPatientType.ShiftIn)
            {
                Status = Neusoft.HISFC.Models.Base.EnumInState.I;
                alPatientList.Add("ת�뻼��|" + EnumPatientType.ShiftIn.ToString());
                al1 = this.radtManager.QueryPatientShiftInApplyByNurseCell(oper.Nurse.ID, "1");				//�����Ҳ�ת�������
                alPatientList.AddRange(al1);
                dicPatient.Add("ת�뻼��|" + EnumPatientType.ShiftIn.ToString(), al1);

            }
            #endregion

            #region ��Ժ�Ǽǻ���

            else if (patientType == EnumPatientType.Out)
            {
                Status = Neusoft.HISFC.Models.Base.EnumInState.B;
                alPatientList.Add("��Ժ�Ǽǻ���|" + EnumPatientType.Out.ToString());
                //���ݳ�Ժ�ٻص���Ч������ѯ��Ժ�Ǽǻ�����Ϣ
                al1 = this.radtManager.PatientQueryByNurseCellVaildDate(oper.Nurse.ID, Status, callBackVaildDays);
                alPatientList.AddRange(al1);
                dicPatient.Add("��Ժ�Ǽǻ���|" + EnumPatientType.Out.ToString(), al1);
            }
            #endregion

            return 1;
        }
    }

    /// <summary>
    /// �������
    /// </summary>
    public enum EnumPatientType
    {
        /// <summary>
        /// ������Ժ����
        /// </summary>
        In = 0,

        /// <summary>
        /// �����ﻼ��
        /// </summary>
        Arrive = 1,

        /// <summary>
        /// ��Ժ�Ǽǻ���
        /// </summary>
        Out = 2,

        /// <summary>
        /// ת�뻼��
        /// </summary>
        ShiftIn = 3,

        /// <summary>
        /// ת������
        /// </summary>
        ShiftOut = 4,

        /// <summary>
        /// �����б�
        /// </summary>
        DeptOrTend = 5
    }
}
