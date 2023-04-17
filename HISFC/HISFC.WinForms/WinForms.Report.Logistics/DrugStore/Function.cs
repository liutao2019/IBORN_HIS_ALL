using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FS.HISFC.Models.Pharmacy;
using FS.FrameWork.Models;

namespace FS.Report.Logistics.DrugStore
{
    /// <summary>
    /// [��������: ҩ��ҵ�������]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11]<br></br>
    /// <˵��  
    ///		ҵ�������  ����ʹ�ò���Ա��½����(�Ȳ�ʹ��Ȩ�޿���)
    ///  />
    /// </summary>
    public class Function
    {
        public Function( )
        {

        }

        #region ��̬��

        /// <summary>
        /// ��ҩ����ӡ�ؼ�
        /// </summary>
        internal static FS.FrameWork.WinForms.Controls.ucBaseControl ucDrugBill = null;

        /// <summary>
        /// ��ҩ����ӡ�ӿ�
        /// </summary>
        public static FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint IDrugPrint = null;        

        /// <summary>
        /// �Ƿ���ʹ�ð�ҩ����׼��ʼ��
        /// </summary>
        public static bool IsApproveInitPrintInterface = false;

        /// <summary>
        /// ���ñ�ǩ��ӡ
        /// </summary>
        public static FS.HISFC.BizProcess.Interface.Pharmacy.ICompoundPrint ICompoundPrint = null;

        #endregion

        #region סԺ��ҩ����

        /// <summary>
        /// ���û�ȷ�ϵĳ�������������з�ҩ������ӡ��ҩ����
        /// writed by cuipeng
        /// 2005-1
        /// ��������:
        /// 1������ü�¼δ�Ʒ�,��
        ///		ȷ����ҩ�Ƿ��ҩƷ����ȡ����ֻ��ÿ����ȡ������ȷ��������
        ///		ȡ���µ�ҩƷ������Ϣ
        ///	2������ҽ��ִ�е�����ҩȷ����Ϣ��
        ///	3������ҩ�����������µ�ִ����Ϣ
        ///	4�������ҩ��ͬʱ��Ҫ��������������ݣ�����ֻȷ�ϲ������������
        ///	5�����³���������еİ�ҩ��Ϣ
        ///	6�����ȫ����׼������°�ҩ֪ͨ��Ϣ�����򲻸��°�ҩ֪ͨ��Ϣ
        ///	��ҩ������İ�ҩ��������drugMessage.DrugBillClass.Memo��
        /// </summary>
        /// <param name="arrayApplyOut">����������Ϣ</param>
        /// <param name="drugMessage">��ҩ֪ͨ���������°�ҩ֪ͨ(��ҩ������İ�ҩ��������drugMessage.DrugBillClass.Memo��)</param>
        /// <returns>1�ɹ���-1ʧ��</returns>
        internal static int DrugConfirm(ArrayList arrayApplyOut, FS.HISFC.Models.Pharmacy.DrugMessage drugMessage, FS.FrameWork.Models.NeuObject arkDept, FS.FrameWork.Models.NeuObject approveDept)
        {
            string noPrivPatient = "";
            if (JudgeInStatePatient(arrayApplyOut, null, ref noPrivPatient) == -1)
            {
                System.Windows.Forms.MessageBox.Show("�жϻ���״̬��Ϣ��������");
                return -1;
            }
            if (noPrivPatient != "")
            {
                System.Windows.Forms.MessageBox.Show(noPrivPatient);
                return -1;
            }
            //��������Ŀ������Ŀ�������� ������Դ�����ķ������� {1B35A424-0127-42ff-96A4-6835D5DB0151}
            FS.HISFC.BizProcess.Integrate.PharmacyMethod.SortApplyOutByItemCode(ref arrayApplyOut);

            FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
            if (pharmacyIntegrate.InpatientDrugConfirm(arrayApplyOut, drugMessage, arkDept, approveDept) != 1)
            {
                System.Windows.Forms.MessageBox.Show(pharmacyIntegrate.Err);
                return -1;
            }
            return 1;
        }


        /// <summary>
        /// ���û�ȷ�ϵĳ�������������з�ҩ������ӡ��ҩ����
        /// writed by cuipeng
        /// 2005-1
        /// ��������:
        /// 1������ü�¼δ�Ʒ�,��
        ///		ȷ����ҩ�Ƿ��ҩƷ����ȡ����ֻ��ÿ����ȡ������ȷ��������
        ///		ȡ���µ�ҩƷ������Ϣ
        ///	2������ҽ��ִ�е�����ҩȷ����Ϣ��
        ///	3������ҩ�����������µ�ִ����Ϣ
        ///	4�������ҩ��ͬʱ��Ҫ��������������ݣ�����ֻȷ�ϲ������������
        ///	5�����³���������еİ�ҩ��Ϣ
        ///	6�����ȫ����׼������°�ҩ֪ͨ��Ϣ�����򲻸��°�ҩ֪ͨ��Ϣ
        ///	��ҩ������İ�ҩ��������drugMessage.DrugBillClass.Memo��
        /// </summary>
        /// <param name="arrayApplyOut">����������Ϣ</param>
        /// <param name="drugMessage">��ҩ֪ͨ���������°�ҩ֪ͨ(��ҩ������İ�ҩ��������drugMessage.DrugBillClass.Memo��)</param>
        /// <returns>1�ɹ���-1ʧ��</returns>
        internal static int DrugConfirm(ArrayList arrayApplyOut, FS.HISFC.Models.Pharmacy.DrugMessage drugMessage, FS.FrameWork.Models.NeuObject arkDept, FS.FrameWork.Models.NeuObject approveDept,System.Data.IDbTransaction trans)
        {
            //��������Ŀ������Ŀ�������� ������Դ�����ķ������� {1B35A424-0127-42ff-96A4-6835D5DB0151}
            FS.HISFC.BizProcess.Integrate.PharmacyMethod.SortApplyOutByItemCode(ref arrayApplyOut);

            FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
            if (pharmacyIntegrate.InpatientDrugConfirm(arrayApplyOut, drugMessage, arkDept, approveDept,trans) != 1)
            {
                System.Windows.Forms.MessageBox.Show(pharmacyIntegrate.Err);
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ���Ѵ�ӡ�İ�ҩ�����к�׼������ҩ��׼��
        /// writed by cuipeng
        /// 2005-1
        /// �������£�
        /// 1�������Ҫ�ں�׼ʱ���⣬����г��⴦����ȡ��applyOut.OutBillCode
        /// 2������ü�¼δ�շѣ����������Ϣ��������·��ñ��еķ�ҩ״̬�ͳ�����ˮ��
        /// 3����׼��ҩ��
        /// </summary>
        /// <param name="arrayApplyOut">����������Ϣ</param>
        /// <param name="approveOperCode">��׼�ˣ���ҩ�ˣ�</param>
        /// <param name="deptCode">��׼����</param>
        /// <returns>1�ɹ���-1ʧ��</returns>
        internal static int DrugApprove( ArrayList arrayApplyOut , string approveOperCode , string deptCode )
        {
            FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy( );
            if( pharmacyIntegrate.InpatientDrugApprove( arrayApplyOut , approveOperCode , deptCode ) != 1 )
            {
                System.Windows.Forms.MessageBox.Show( pharmacyIntegrate.Err );
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ���Ѵ�ӡ�İ�ҩ�����к�׼������ҩ��׼��
        /// writed by cuipeng
        /// 2005-1
        /// �������£�
        /// 1�������Ҫ�ں�׼ʱ���⣬����г��⴦����ȡ��applyOut.OutBillCode
        /// 2������ü�¼δ�շѣ����������Ϣ��������·��ñ��еķ�ҩ״̬�ͳ�����ˮ��
        /// 3����׼��ҩ��
        /// </summary>
        /// <param name="arrayApplyOut">����������Ϣ</param>
        /// <param name="approveOperCode">��׼�ˣ���ҩ�ˣ�</param>
        /// <param name="deptCode">��׼����</param>
        /// <returns>1�ɹ���-1ʧ��</returns>
        internal static int DrugApprove(ArrayList arrayApplyOut, string approveOperCode, string deptCode,System.Data.IDbTransaction trans)
        {
            FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
            if (pharmacyIntegrate.InpatientDrugApprove(arrayApplyOut, approveOperCode, deptCode,trans) != 1)
            {
                System.Windows.Forms.MessageBox.Show(pharmacyIntegrate.Err);
                return -1;
            }
            return 1;
        }


        /// <summary>
        /// ����ҩ������к�׼������ҩ��׼��
        /// writed by cuipeng
        /// 2005-3
        /// �������£�
        /// 1�����⴦�����س�����ˮ�š�
        /// 2�������ҩ��ͬʱ�˷�,���������Ϣ
        /// 3����׼�������룬����ҩ״̬�ɡ�0���ĳ�ApplyState��
        /// 4��ȡ������Ϣ
        /// 5�������˷�����
        /// 6�����ȫ����׼������°�ҩ֪ͨ��Ϣ�����򲻸��°�ҩ֪ͨ��Ϣ
        /// ��ҩ������İ�ҩ��������drugMessage.DrugBillClass.Memo��
        /// </summary>
        /// <param name="arrayApplyOut">����������Ϣ</param>
        /// <param name="drugMessage">��ҩ֪ͨ���������°�ҩ֪ͨ(��ҩ������İ�ҩ��������drugMessage.DrugBillClass.Memo��)</param>
        /// <returns>1�ɹ���-1ʧ��</returns>
        internal static int DrugReturnConfirm( ArrayList arrayApplyOut , FS.HISFC.Models.Pharmacy.DrugMessage drugMessage,FS.FrameWork.Models.NeuObject arkDept,FS.FrameWork.Models.NeuObject approveDept )
        {
            FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy( );
            if( pharmacyIntegrate.InpatientDrugReturnConfirm( arrayApplyOut , drugMessage ,arkDept,approveDept) != 1 )
            {
                System.Windows.Forms.MessageBox.Show( pharmacyIntegrate.Err );
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ����ҩƷ���룬�жϻ����Ƿ��ѳ�Ժ�����ز�����������а�ҩ�շѵĻ�����Ϣ
        /// </summary>
        /// <param name="arrayApplyOut">ҩƷ����</param>
        /// <param name="noPrivPatient">����Ժ�Ļ�����Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        internal static int JudgeInStatePatient(ArrayList arrayApplyOut,System.Data.IDbTransaction trans,ref string noPrivPatient)
        {
            System.Collections.Hashtable hsPatient = new Hashtable();
            FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
            if (trans != null)
            {
                radtIntegrate.SetTrans(trans);
            }

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in arrayApplyOut)
            {
                if (info.IsCharge)      //���Ѿ��շѵļ�¼�������жϴ���
                {
                    continue;
                }
                if (hsPatient.ContainsKey(info.PatientNO))
                {
                    continue;
                }
                else
                {
                    FS.HISFC.Models.RADT.PatientInfo p = radtIntegrate.GetPatientInfomation(info.PatientNO);
                    if (p != null)
                    {
                        if (p.PVisit.InState.ID.ToString() != FS.HISFC.Models.Base.EnumInState.I.ToString())
                        {
                            if (noPrivPatient == "")
                            {
                                noPrivPatient = p.Name;
                            }
                            else
                            {
                                noPrivPatient += "��" + p.Name;
                            }
                        }
                    }

                    hsPatient.Add(info.PatientNO, null);
                }
            }

            if (noPrivPatient != "")
            {
                noPrivPatient += "�Ѳ���Ժ�����ܽ��а�ҩ�۷Ѳ�����";
            }

            return 1;
        }
        #endregion

        #region ������/��ҩ����

        /// <summary>
        /// ������ҩ����
        /// </summary>
        /// <param name="applyOutCollection">��ҩ������Ϣ</param>
        /// <param name="terminal">��ҩ�ն�</param>
        /// <param name="drugedDept">��ҩ������Ϣ</param>
        /// <param name="drugedOper">��ҩ��Ա��Ϣ</param>
        /// <param name="isUpdateAdjustParam">�Ƿ���´�����������</param>
        /// <returns>��ҩȷ�ϳɹ�����1 ʧ�ܷ���-1</returns>
        internal static int OutpatientDrug( List<ApplyOut> applyOutCollection , NeuObject terminal , NeuObject drugedDept , NeuObject drugedOper,bool isUpdateAdjustParam )
        {
            FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy( );
            if( pharmacyIntegrate.OutpatientDrug( applyOutCollection , terminal , drugedDept , drugedOper,isUpdateAdjustParam) != 1 )
            {
                System.Windows.Forms.MessageBox.Show( pharmacyIntegrate.Err );
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ���﷢ҩ����
        /// </summary>
        /// <param name="applyOutCollection">��ҩ������Ϣ</param>
        /// <param name="terminal">��ҩ�ն�</param>
        /// <param name="sendDept">��ҩ������Ϣ(�ۿ����)</param>
        /// <param name="sendOper">��ҩ��Ա��Ϣ</param>
        /// <param name="isDirectSave">�Ƿ�ֱ�ӱ��� (����ҩ����)</param>
        /// <param name="isUpdateAdjustParam">�Ƿ���´�����������</param>
        /// <returns>��ҩȷ�ϳɹ�����1 ʧ�ܷ���-1</returns>
        internal static int OutpatientSend(List<ApplyOut> applyOutCollection, NeuObject terminal, NeuObject sendDept, NeuObject sendOper, bool isDirectSave, bool isUpdateAdjustParam)
        {
            FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy( );
            if( pharmacyIntegrate.OutpatientSend( applyOutCollection , terminal , sendDept , sendOper , isDirectSave,isUpdateAdjustParam ) != 1 )
            {
                System.Windows.Forms.MessageBox.Show( pharmacyIntegrate.Err );
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ���ﻹҩ���� ������ҩȷ�ϵ����� ����Ϊδ��ӡ״̬
        /// </summary>
        /// <param name="applyOutCollection">��ҩ������Ϣ</param>
        /// <param name="backOper">��ҩ��Ա��Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        internal static int OutpatientBack( List<ApplyOut> applyOutCollection , NeuObject backOper )
        {
            FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy( );
            if( pharmacyIntegrate.OutpatientBack( applyOutCollection , backOper ) != 1 )
            {
                System.Windows.Forms.MessageBox.Show( pharmacyIntegrate.Err );
                return -1;
            }
            return 1;
        }

        #endregion

        #region סԺ���������շ�

        /// <summary>
        ///  ���������շ�
        /// </summary>
        /// <param name="arrayApplyOut">סԺ��������</param>
        /// <param name="execDept">ִ�п���</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        internal static int CompoundFee(ArrayList arrayApplyOut, FS.FrameWork.Models.NeuObject execDept, System.Data.IDbTransaction trans)
        {
            FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
            if (pharmacyIntegrate.CompoundFee(arrayApplyOut, execDept, trans) != 1)
            {
                System.Windows.Forms.MessageBox.Show(pharmacyIntegrate.Err);
                return -1;
            }
            return 1;
        }

        #endregion

        /// <summary>
        /// ִ�����ݴ�ӡ
        /// </summary>
        /// <param name="al">���ӡ����</param>
        internal static void Print( ArrayList al , FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass , bool isAutoPrint , bool isPrintLabel , bool isNeedPreview )
        {
            ArrayList alClone = new ArrayList();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in al)
            {
                alClone.Add(info.Clone());
            }

            if( !isAutoPrint )
            {
                if( isPrintLabel )
                {
                    Function.PrintLabelForOutpatient(alClone);
                }
                else
                {
                    if( isNeedPreview )
                        Function.Preview(alClone, drugBillClass);
                    else
                        Function.PrintBill(alClone, drugBillClass);
                }
               
            }
        }

        /// <summary>
        /// ��ҩ����ӡ
        /// </summary>
        /// <param name="alData">���ӡ����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        internal static int PrintBill( ArrayList alData , FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass )
        {
            ArrayList alClone = new ArrayList();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                alClone.Add(info.Clone());
            }

            if( Function.IDrugPrint == null )
            {
                System.Windows.Forms.MessageBox.Show( "δ��ȷ���ð�ҩ����ӡ�ӿ�." );
                return -1;
            }
            Function.IDrugPrint.AddAllData(alClone, drugBillClass);
            Function.IDrugPrint.Print( );
            return 1;
        }

        /// <summary>
        /// �Դ�����������ݴ�ӡ�����ǩ
        /// </summary>
        /// <param name="alOutData">��������</param>
        /// <returns>�ɹ�����1 ������-1</returns>
        internal static int PrintLabelForOutpatient( ArrayList alOutData )
        {
            if( Function.IDrugPrint == null )
            {
                System.Windows.Forms.MessageBox.Show( "δ��ȷ���ð�ҩ����ӡ�ӿ�." );
                return -1;
            }
            if( alOutData.Count <= 0 )
                return 1;

            string strPID = "";

            ArrayList al = new ArrayList( );
            FS.HISFC.Models.Registration.Register patiRegister = new FS.HISFC.Models.Registration.Register( );
            FS.HISFC.Models.RADT.PatientInfo patiPerson = new FS.HISFC.Models.RADT.PatientInfo( );

            FS.HISFC.BizProcess.Integrate.RADT radtManager = new FS.HISFC.BizProcess.Integrate.RADT( );
            FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger( );

            foreach( FS.HISFC.Models.Pharmacy.ApplyOut temp in alOutData )
            {
                temp.User01 = "";
                if( temp.PatientNO == strPID )
                {
                    al.Add( temp );
                }
                else
                {
                    if( al.Count > 0 )
                    {
                        #region ��ǩ��ӡ��ֵ
                        patiPerson = radtManager.GetPatientInfomation( strPID );
                        patiRegister.Name = patiPerson.Name;
                        patiRegister.Sex = patiPerson.Sex;
                        patiRegister.Age = dataManager.GetAge( patiPerson.Birthday );
                        patiRegister.User02 = al.Count.ToString( );

                        Function.IDrugPrint.OutpatientInfo = patiRegister;
                        Function.IDrugPrint.LabelTotNum = al.Count;
                        Function.IDrugPrint.DrugTotNum = al.Count;

                        string privCombo = "";

                        ArrayList alCombo = new ArrayList( );

                        foreach( FS.HISFC.Models.Pharmacy.ApplyOut info in al )
                        {
                            if( privCombo == "-1" || ( privCombo == info.CombNO && info.CombNO != "" ) )
                            {
                                alCombo.Add( info );
                                privCombo = info.CombNO;
                                continue;
                            }
                            else			//��ͬ������
                            {
                                if( alCombo.Count > 0 )
                                {
                                    if( alCombo.Count == 1 )
                                        Function.IDrugPrint.AddSingle( alCombo[ 0 ] as FS.HISFC.Models.Pharmacy.ApplyOut );
                                    else
                                        Function.IDrugPrint.AddCombo( alCombo );
                                    Function.IDrugPrint.Print( );
                                }

                                privCombo = info.CombNO;
                                alCombo = new ArrayList( );

                                alCombo.Add( info );
                            }
                        }
                        if( alCombo.Count == 0 )
                        {
                            return 1;
                        }
                        if( alCombo.Count > 1 )
                        {
                            Function.IDrugPrint.AddCombo( alCombo );
                        }
                        else
                        {
                            Function.IDrugPrint.AddSingle( alCombo[ 0 ] as FS.HISFC.Models.Pharmacy.ApplyOut );
                        }

                        Function.IDrugPrint.Print( );

                        #endregion
                    }

                    al = new ArrayList( );
                    al.Add( temp );
                    strPID = temp.PatientNO;
                }
            }

            if( al.Count > 0 )
            {
                #region ��ǩ��ӡ��ֵ
                patiPerson = radtManager.GetPatientInfomation( strPID );
                patiRegister.Name = patiPerson.Name;
                patiRegister.Sex = patiPerson.Sex;
                patiRegister.Age = dataManager.GetAge( patiPerson.Birthday );
                patiRegister.User02 = al.Count.ToString( );

                Function.IDrugPrint.OutpatientInfo = patiRegister;
                Function.IDrugPrint.LabelTotNum = al.Count;
                Function.IDrugPrint.DrugTotNum = al.Count;

                string privCombo = "-1";

                ArrayList alCombo = new ArrayList( );

                foreach( FS.HISFC.Models.Pharmacy.ApplyOut info in al )
                {
                    if( privCombo == "-1" || ( privCombo == info.CombNO && info.CombNO != "" ) )
                    {
                        alCombo.Add( info );
                        privCombo = info.CombNO;
                        continue;
                    }
                    else			//��ͬ������
                    {
                        if( alCombo.Count == 1 )
                            Function.IDrugPrint.AddSingle( alCombo[ 0 ] as FS.HISFC.Models.Pharmacy.ApplyOut );
                        else
                            Function.IDrugPrint.AddCombo( alCombo );
                        Function.IDrugPrint.Print( );

                        privCombo = info.CombNO;
                        alCombo = new ArrayList( );

                        alCombo.Add( info );
                    }
                }
                if( alCombo.Count == 0 )
                {
                    return 1;
                }
                if( alCombo.Count > 1 )
                {
                    Function.IDrugPrint.AddCombo( alCombo );
                }
                else
                {
                    Function.IDrugPrint.AddSingle( alCombo[ 0 ] as FS.HISFC.Models.Pharmacy.ApplyOut );
                }

                Function.IDrugPrint.Print( );

                #endregion
            }

            return 1;
        }

        /// <summary>
        /// ��ӡԤ��
        /// </summary>
        /// <param name="alOutData">��������</param>
        /// <returns>�ɹ�����1 ������-1</returns>
        internal static int Preview( ArrayList alData , FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass )
        {
            ArrayList alClone = new ArrayList();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                alClone.Add(info.Clone());
            }


            if( Function.IDrugPrint == null )
            {
                System.Windows.Forms.MessageBox.Show( "δ��ȷ���ð�ҩ����ӡ�ӿ�." );
                return -1;
            }
            Function.IDrugPrint.AddAllData(alClone, drugBillClass);
            Function.IDrugPrint.Preview( );
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            return 1;
        }

        /// <summary>
        /// �����ն�ѡ��ҵ��
        /// </summary>
        /// <param name="stockDept">�����ⷿ����</param>
        /// <param name="terminalType">�����ն���� 0 ��ҩ���� 1 ��ҩ̨</param>
        /// <param name="isShowMessageBox">����Ӧ����ʾ��Ϣ�Ƿ����MessageBox������ʾ</param>
        /// <returns>�ɹ����� �����ն�ʵ�� ʧ�ܷ���null</returns>
        public static FS.HISFC.Models.Pharmacy.DrugTerminal TerminalSelect( string stockDept , FS.HISFC.Models.Pharmacy.EnumTerminalType terminalType , bool isShowMessageBox )
        {
            FS.HISFC.Models.Pharmacy.DrugTerminal terminal = new DrugTerminal( );
            FS.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore( );

            string strErr = "";
            bool isShowTerminalList = true;
            ArrayList alValues = FS.FrameWork.WinForms.Classes.Function.GetDefaultValue( "ClinicDrug" , "TerminalCode" , out strErr );
            if( alValues != null && alValues.Count > 0 && ( alValues[ 0 ] as string ) != "" )
            {
                //���������ļ��ڱ���ȷ���ն�
                terminal = drugStoreManager.GetDrugTerminalById( alValues[ 0 ] as string );
                if( terminal != null )
                {
                    if( terminal.IsClose && isShowMessageBox )
                        System.Windows.Forms.MessageBox.Show( FS.FrameWork.Management.Language.Msg( "�����ļ������õ��ն�" + terminal.Name + "�ѹر�" ) );

                    if( terminal.TerminalType == terminalType )
                        isShowTerminalList = false;
                }
            }

            if( isShowTerminalList )
            {
                #region �����ļ��ڱ�����Ч �����б���Աѡ��

                ArrayList al = drugStoreManager.QueryDrugTerminalByDeptCode( stockDept , terminalType.GetHashCode( ).ToString( ) );
                if( al == null && isShowMessageBox )
                {
                    System.Windows.Forms.MessageBox.Show( FS.FrameWork.Management.Language.Msg( "δ��ȡ�ն��б�" ) + drugStoreManager.Err );
                    return null;
                }
                FS.FrameWork.Models.NeuObject tempTerminal = new NeuObject( );
                if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(al, null, new bool[] { true, true, false, false, false, false, false, false }, null, ref tempTerminal) == 0)
                //if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(al, ref tempTerminal) == 0)
                {
                    return null;
                }
                else
                {
                    terminal = tempTerminal as FS.HISFC.Models.Pharmacy.DrugTerminal;
                }

                #endregion
            }

            if( terminal != null && terminal.TerminalType == EnumTerminalType.��ҩ̨ )
            {
                FS.HISFC.Models.Pharmacy.DrugTerminal sendTerminal = drugStoreManager.GetDrugTerminalById( terminal.SendWindow.ID );
                if( sendTerminal != null )
                {
                    terminal.SendWindow.Name = sendTerminal.Name;
                }
            }

            return terminal;
        }

        /// <summary>
        /// ���ô�����ӡ�ӿ�
        /// </summary>
        /// <returns></returns>
        public static int InitLabelPrintInterface()
        {
            object[] o = new object[] { };

            try
            {
                System.Runtime.Remoting.ObjectHandle objHandel = System.Activator.CreateInstance("Report", "Report.DrugStore.ucRecipeLabel", false, System.Reflection.BindingFlags.CreateInstance, null, o, null, null, null);
                object oLabel = objHandel.Unwrap();

                if (oLabel.GetType().GetInterface("IDrugPrint") == null)
                {
                    System.Windows.Forms.MessageBox.Show("�����Ͻӿ�");
                    return -1;
                }

                FS.Report.Logistics.DrugStore.Function.IDrugPrint = oLabel as FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint;
                //FS.HISFC.Components.DrugStore.Function.IDrugPrint = oLabel as FS.HISFC.BizProcess.Integrate.PharmacyInterface.IDrugPrint;
            }
            catch (System.TypeLoadException ex)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ǩ�����ռ���Ч\n" + ex.Message));
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// �������ñ�ǩ��ӡ�ӿ�
        /// </summary>
        /// <returns></returns>
        public static int InitCompoundPrintInterface()
        {
            object[] o = new object[] { };

            try
            {
                //�����ǩ��ӡ�ӿ�ʵ����
                FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                string labelValue = "FS.Report.Logistics.ucCompoundLabel"; //ctrlIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Compound_Print_Label, true, "FS.Report.Logistics.ucCompoundLabel");

                System.Runtime.Remoting.ObjectHandle objHandel = System.Activator.CreateInstance("Report", labelValue , false, System.Reflection.BindingFlags.CreateInstance, null, o, null, null, null);
                object oLabel = objHandel.Unwrap();

                if (oLabel.GetType().GetInterface("ICompoundPrint") == null)
                {
                    System.Windows.Forms.MessageBox.Show("�����Ͻӿ�");
                    return -1;
                }
                FS.Report.Logistics.DrugStore.Function.ICompoundPrint = oLabel as FS.HISFC.BizProcess.Interface.Pharmacy.ICompoundPrint;
                //FS.HISFC.Components.DrugStore.Function.ICompoundPrint = oLabel as FS.HISFC.BizProcess.Integrate.PharmacyInterface.ICompoundPrint;
            }
            catch (System.TypeLoadException ex)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ǩ�����ռ���Ч\n" + ex.Message));
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ���ýӿڴ�ӡ�ƶ�����
        /// </summary>
        /// <param name="alData">����ӡ����</param>
        /// <param name="isPrintDetail">�Ƿ��ӡ��ϸִ�е�</param>
        /// <param name="isPrintLabel">�Ƿ��ӡ��ǩ</param>
        /// <returns>�ɹ�����1��ʧ�ܷ��أ�1</returns>
        internal static int PrintCompound(ArrayList alData, bool isPrintDetail, bool isPrintLabel)
        {
            if (Function.ICompoundPrint == null)
            {
                InitCompoundPrintInterface();
            }

            ArrayList alGroupApplyOut = new ArrayList();
            ArrayList alCombo = new ArrayList();
            string privCombo = "-1";

            #region ��ǩ��ӡ

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                if (privCombo == "-1" || (privCombo == info.CompoundGroup && info.CompoundGroup != ""))
                {
                    alCombo.Add(info.Clone());
                    privCombo = info.CompoundGroup;
                    continue;
                }
                else			//��ͬ������
                {
                    alGroupApplyOut.Add(alCombo);

                    privCombo = info.CompoundGroup;
                    alCombo = new ArrayList();

                    alCombo.Add(info.Clone());
                }
            }

            if (alCombo.Count > 0)
            {
                alGroupApplyOut.Add(alCombo);
            }

            #endregion

            Function.ICompoundPrint.LabelTotNum = alGroupApplyOut.Count;
            //���������ʾ
            Function.ICompoundPrint.Clear();
            if (isPrintLabel)
            {
                Function.ICompoundPrint.AddCombo(alGroupApplyOut);
            }
            if (isPrintDetail)
            {
                Function.ICompoundPrint.AddAllData(alData);
            }

            Function.ICompoundPrint.Print();

            return 1;
        }

        /// <summary>
        /// ��ʾ��ʾ��Ϣ
        /// </summary>
        /// <param name="errStr">��ʾ��Ϣ</param>
        public static void ShowMsg(string strMsg)
        {
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg(strMsg));
        }
    }

    /// <summary>
    /// ���﹦��ģ��˵��
    /// </summary>
    public enum OutpatientFun
    {
        /// <summary>
        /// ��ҩ
        /// </summary>
        Drug ,
        /// <summary>
        /// ��ҩ
        /// </summary>
        Send ,
        /// <summary>
        /// ֱ�ӷ�ҩ ����ҩ����
        /// </summary>
        DirectSend ,
        /// <summary>
        /// ��ҩ
        /// </summary>
        Back
    }

    /// <summary>
    /// �����䷢ҩ���ڹ���
    /// </summary>
    public enum OutpatientWinFun
    {
        ��ҩ ,
        ��ҩ ,
        ֱ�ӷ�ҩ ,
        ��ҩ ,
        ����ҩ����ҩ ,
        ����ҩ����ҩ
    }

    /// <summary>
    /// ���ݼ�����ʽ
    /// </summary>
    public enum OutpatientBillType
    {
        /// <summary>
        /// ������
        /// </summary>
        ������ = 0 ,
        /// <summary>
        /// ��Ʊ��
        /// </summary>
        ��Ʊ�� = 1 ,
        /// <summary>
        /// ��������
        /// </summary>
        �������� = 2
    }
}
