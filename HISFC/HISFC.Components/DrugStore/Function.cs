using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FS.HISFC.Models.Pharmacy;
using FS.FrameWork.Models;

namespace FS.HISFC.Components.DrugStore
{
    /// <summary>
    /// [功能描述: 药房业务操作类]<br></br>
    /// [创 建 者: 梁俊泽]<br></br>
    /// [创建时间: 2006-11]<br></br>
    /// <说明  
    ///		业务操作类  科室使用操作员登陆科室(先不使用权限科室)
    ///  />
    /// <修改说明>
    ///    1.配置中心收费(根据用法附加材料费收取) by Sunjh 2010-10-26 {74D77EE3-F04E-4d94-A2A3-24902B93C619} 
    /// </修改说明>
    /// </summary>
    public class Function : FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public Function( )
        {

        }

        #region 静态量

        /// <summary>
        /// 摆药单打印控件
        /// </summary>
        internal static FS.FrameWork.WinForms.Controls.ucBaseControl ucDrugBill = null;

        /// <summary>
        /// 摆药单打印接口
        /// </summary>
        public static FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint IDrugPrint = null;        

        /// <summary>
        /// 是否已使用摆药单核准初始化
        /// </summary>
        public static bool IsApproveInitPrintInterface = false;

        /// <summary>
        /// 配置标签打印
        /// </summary>
        public static FS.HISFC.BizProcess.Interface.Pharmacy.ICompoundPrint ICompoundPrint = null;

        #endregion

        #region 住院发药保存

        /// <summary>
        /// 对用户确认的出库申请数组进行发药处理（打印摆药单）
        /// writed by cuipeng
        /// 2005-1
        /// 操作如下:
        /// 1、如果该记录未计费,则
        ///		确定摆药是否对药品数量取整（只对每次量取整），确定数量。
        ///		取最新的药品基本信息
        ///	2、更新医嘱执行档（摆药确认信息）
        ///	3、更新药嘱主档的最新的执行信息
        ///	4、如果摆药的同时需要出库则处理出库数据，否则，只确认不处理出库数据
        ///	5、更新出库申请表中的摆药信息
        ///	6、如果全部核准，则更新摆药通知信息。否则不更新摆药通知信息
        ///	摆药后产生的摆药单保存在drugMessage.DrugBillClass.Memo中
        /// </summary>
        /// <param name="arrayApplyOut">出库申请信息</param>
        /// <param name="drugMessage">摆药通知，用来更新摆药通知(摆药后产生的摆药单保存在drugMessage.DrugBillClass.Memo中)</param>
        /// <returns>1成功，-1失败</returns>
        internal static int DrugConfirm(ArrayList arrayApplyOut, FS.HISFC.Models.Pharmacy.DrugMessage drugMessage, FS.FrameWork.Models.NeuObject arkDept, FS.FrameWork.Models.NeuObject approveDept)
        {
            string noPrivPatient = "";
            if (JudgeInStatePatient(arrayApplyOut, null, ref noPrivPatient) == -1)
            {
                System.Windows.Forms.MessageBox.Show("判断患者状态信息发生错误");
                return -1;
            }
            if (noPrivPatient != "")
            {
                System.Windows.Forms.MessageBox.Show(noPrivPatient);
                return -1;
            }
            //对申请项目按照项目编码排序 减少资源死锁的发生几率 {1B35A424-0127-42ff-96A4-6835D5DB0151}
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
        /// 对用户确认的出库申请数组进行发药处理（打印摆药单）
        /// writed by cuipeng
        /// 2005-1
        /// 操作如下:
        /// 1、如果该记录未计费,则
        ///		确定摆药是否对药品数量取整（只对每次量取整），确定数量。
        ///		取最新的药品基本信息
        ///	2、更新医嘱执行档（摆药确认信息）
        ///	3、更新药嘱主档的最新的执行信息
        ///	4、如果摆药的同时需要出库则处理出库数据，否则，只确认不处理出库数据
        ///	5、更新出库申请表中的摆药信息
        ///	6、如果全部核准，则更新摆药通知信息。否则不更新摆药通知信息
        ///	摆药后产生的摆药单保存在drugMessage.DrugBillClass.Memo中
        /// </summary>
        /// <param name="arrayApplyOut">出库申请信息</param>
        /// <param name="drugMessage">摆药通知，用来更新摆药通知(摆药后产生的摆药单保存在drugMessage.DrugBillClass.Memo中)</param>
        /// <returns>1成功，-1失败</returns>
        internal static int DrugConfirm(ArrayList arrayApplyOut, FS.HISFC.Models.Pharmacy.DrugMessage drugMessage, FS.FrameWork.Models.NeuObject arkDept, FS.FrameWork.Models.NeuObject approveDept,System.Data.IDbTransaction trans)
        {
            //对申请项目按照项目编码排序 减少资源死锁的发生几率 {1B35A424-0127-42ff-96A4-6835D5DB0151}
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
        /// 对已打印的摆药单进行核准处理（摆药核准）
        /// writed by cuipeng
        /// 2005-1
        /// 操作如下：
        /// 1、如果需要在核准时出库，则进行出库处理。并取得applyOut.OutBillCode
        /// 2、如果该记录未收费，则处理费用信息，否则更新费用表中的发药状态和出库流水号
        /// 3、核准摆药单
        /// </summary>
        /// <param name="arrayApplyOut">出库申请信息</param>
        /// <param name="approveOperCode">核准人（摆药人）</param>
        /// <param name="deptCode">核准科室</param>
        /// <returns>1成功，-1失败</returns>
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
        /// 对已打印的摆药单进行核准处理（摆药核准）
        /// writed by cuipeng
        /// 2005-1
        /// 操作如下：
        /// 1、如果需要在核准时出库，则进行出库处理。并取得applyOut.OutBillCode
        /// 2、如果该记录未收费，则处理费用信息，否则更新费用表中的发药状态和出库流水号
        /// 3、核准摆药单
        /// </summary>
        /// <param name="arrayApplyOut">出库申请信息</param>
        /// <param name="approveOperCode">核准人（摆药人）</param>
        /// <param name="deptCode">核准科室</param>
        /// <returns>1成功，-1失败</returns>
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
        /// 对退药申请进行核准处理（退药核准）
        /// writed by cuipeng
        /// 2005-3
        /// 操作如下：
        /// 1、出库处理，返回出库流水号。
        /// 2、如果退药的同时退费,则处理费用信息
        /// 3、核准出库申请，将摆药状态由“0”改成ApplyState。
        /// 4、取费用信息
        /// 5、进行退费申请
        /// 6、如果全部核准，则更新摆药通知信息。否则不更新摆药通知信息
        /// 摆药后产生的摆药单保存在drugMessage.DrugBillClass.Memo中
        /// </summary>
        /// <param name="arrayApplyOut">出库申请信息</param>
        /// <param name="drugMessage">摆药通知，用来更新摆药通知(摆药后产生的摆药单保存在drugMessage.DrugBillClass.Memo中)</param>
        /// <returns>1成功，-1失败</returns>
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
        /// 根据药品申请，判断患者是否已出院，返回不允许继续进行摆药收费的患者信息
        /// </summary>
        /// <param name="arrayApplyOut">药品申请</param>
        /// <param name="noPrivPatient">不在院的患者信息</param>
        /// <returns>成功返回1 失败返回－1</returns>
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
                if (info.IsCharge)      //对已经收费的记录不进行判断处理
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
                                noPrivPatient += "，" + p.Name;
                            }
                        }
                    }

                    hsPatient.Add(info.PatientNO, null);
                }
            }

            if (noPrivPatient != "")
            {
                noPrivPatient += "已不在院，不能进行摆药扣费操作。";
            }

            return 1;
        }
        #endregion

        #region 门诊配/发药保存

        /// <summary>
        /// 门诊配药保存
        /// </summary>
        /// <param name="applyOutCollection">摆药申请信息</param>
        /// <param name="terminal">配药终端</param>
        /// <param name="drugedDept">配药科室信息</param>
        /// <param name="drugedOper">配药人员信息</param>
        /// <param name="isUpdateAdjustParam">是否更新处方调剂参数</param>
        /// <returns>配药确认成功返回1 失败返回-1</returns>
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
        /// 门诊发药保存
        /// </summary>
        /// <param name="applyOutCollection">摆药申请信息</param>
        /// <param name="terminal">发药终端</param>
        /// <param name="sendDept">发药科室信息(扣库科室)</param>
        /// <param name="sendOper">发药人员信息</param>
        /// <param name="isDirectSave">是否直接保存 (无配药流程)</param>
        /// <param name="isUpdateAdjustParam">是否更新处方调剂参数</param>
        /// <returns>发药确认成功返回1 失败返回-1</returns>
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
        /// 门诊还药操作 对已配药确认的数据 更新为未打印状态
        /// </summary>
        /// <param name="applyOutCollection">摆药申请信息</param>
        /// <param name="backOper">还药人员信息</param>
        /// <returns>成功返回1 失败返回-1</returns>
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

        #region 住院配置中心收费

        /// <summary>
        ///  配置中心收费
        /// </summary>
        /// <param name="arrayApplyOut">住院配置数据</param>
        /// <param name="execDept">执行科室</param>
        /// <returns>成功返回1 失败返回-1</returns>
        internal static int CompoundFee(ArrayList arrayApplyOut, FS.FrameWork.Models.NeuObject execDept, System.Data.IDbTransaction trans)
        {
            FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
            //{0C5037B6-06FB-4dd8-AED8-B7412D2A6576}  更改对返回值的判断 仅判断是否为-1
            if (pharmacyIntegrate.CompoundFee(arrayApplyOut, execDept, trans) == -1)
            {
                System.Windows.Forms.MessageBox.Show(pharmacyIntegrate.Err);
                return -1;
            }
            return 1;
        }

        /// <summary>
        ///  配置中心收费(根据用法附加材料费收取) by Sunjh 2010-10-26 {74D77EE3-F04E-4d94-A2A3-24902B93C619}
        /// </summary>
        /// <param name="arrayApplyOut">住院配置数据</param>
        /// <param name="execDept">执行科室</param>
        /// <returns>成功返回1 失败返回-1</returns>
        internal static int CompoundFeeByUsage(ArrayList arrayApplyOut, FS.FrameWork.Models.NeuObject execDept, System.Data.IDbTransaction trans)
        {
            FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
            //{0C5037B6-06FB-4dd8-AED8-B7412D2A6576}  更改对返回值的判断 仅判断是否为-1
            if (pharmacyIntegrate.CompoundFeeByUsage(arrayApplyOut, execDept, trans) == -1)
            {
                System.Windows.Forms.MessageBox.Show(pharmacyIntegrate.Err);
                return -1;
            }
            return 1;
        }

        #endregion

        /// <summary>
        /// 执行数据打印
        /// </summary>
        /// <param name="al">需打印数据</param>
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
        /// 摆药单打印
        /// </summary>
        /// <param name="alData">需打印数据</param>
        /// <returns>成功返回1 失败返回-1</returns>
        internal static int PrintBill( ArrayList alData , FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass )
        {
            ArrayList alClone = new ArrayList();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                alClone.Add(info.Clone());
            }

            if( Function.IDrugPrint == null )
            {
                System.Windows.Forms.MessageBox.Show( "未正确设置摆药单打印接口." );
                return -1;
            }
            Function.IDrugPrint.AddAllData(alClone, drugBillClass);
            Function.IDrugPrint.Print( );
            return 1;
        }

        /// <summary>
        /// 对传入的申请数据打印门诊标签
        /// </summary>
        /// <param name="alOutData">申请数据</param>
        /// <returns>成功返回1 出错返回-1</returns>
        internal static int PrintLabelForOutpatient( ArrayList alOutData )
        {
            if( Function.IDrugPrint == null )
            {
                System.Windows.Forms.MessageBox.Show( "未正确设置摆药单打印接口." );
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
                        #region 标签打印赋值
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
                            else			//不同处方号
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
                #region 标签打印赋值
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
                    else			//不同处方号
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
        /// 打印预览
        /// </summary>
        /// <param name="alOutData">申请数据</param>
        /// <returns>成功返回1 出错返回-1</returns>
        internal static int Preview( ArrayList alData , FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass )
        {
            ArrayList alClone = new ArrayList();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                alClone.Add(info.Clone());
            }


            if( Function.IDrugPrint == null )
            {
                System.Windows.Forms.MessageBox.Show( "未正确设置摆药单打印接口." );
                return -1;
            }
            Function.IDrugPrint.AddAllData(alClone, drugBillClass);
            Function.IDrugPrint.Preview( );
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            return 1;
        }

        /// <summary>
        /// 门诊终端选择业务
        /// </summary>
        /// <param name="stockDept">操作库房科室</param>
        /// <param name="terminalType">门诊终端类别 0 发药窗口 1 配药台</param>
        /// <param name="isShowMessageBox">对相应的提示信息是否采用MessageBox弹出显示</param>
        /// <returns>成功返回 门诊终端实体 失败返回null</returns>
        public static FS.HISFC.Models.Pharmacy.DrugTerminal TerminalSelect( string stockDept , FS.HISFC.Models.Pharmacy.EnumTerminalType terminalType , bool isShowMessageBox )
        {
            FS.HISFC.Models.Pharmacy.DrugTerminal terminal = new DrugTerminal( );
            FS.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore( );

            string strErr = "";
            bool isShowTerminalList = true;
            ArrayList alValues = FS.FrameWork.WinForms.Classes.Function.GetDefaultValue( "ClinicDrug" , "TerminalCode" , out strErr );
            if( alValues != null && alValues.Count > 0 && ( alValues[ 0 ] as string ) != "" )
            {
                //根据配置文件内编码确定终端
                terminal = drugStoreManager.GetDrugTerminalById( alValues[ 0 ] as string );
                if( terminal != null )
                {
                    if( terminal.IsClose && isShowMessageBox )
                        System.Windows.Forms.MessageBox.Show( FS.FrameWork.Management.Language.Msg( "配置文件内设置的终端" + terminal.Name + "已关闭" ) );

                    if( terminal.TerminalType == terminalType )
                        isShowTerminalList = false;
                }
            }

            if( isShowTerminalList )
            {
                #region 配置文件内编码无效 弹出列表供人员选择

                ArrayList al = drugStoreManager.QueryDrugTerminalByDeptCode( stockDept , terminalType.GetHashCode( ).ToString( ) );
                if( al == null && isShowMessageBox )
                {
                    System.Windows.Forms.MessageBox.Show( FS.FrameWork.Management.Language.Msg( "未获取终端列表" ) + drugStoreManager.Err );
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

            if( terminal != null && terminal.TerminalType == EnumTerminalType.配药台 )
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
        /// 设置处方打印接口
        /// </summary>
        /// <returns></returns>
        public static int InitLabelPrintInterface()
        {
            object[] o = new object[] { };

            try
            {
                System.Runtime.Remoting.ObjectHandle objHandel = System.Activator.CreateInstance("WinForms.Report", "FS.WinForms.Report.DrugStore.ucRecipeLabel", false, System.Reflection.BindingFlags.CreateInstance, null, o, null, null, null);
                object oLabel = objHandel.Unwrap();

                if (oLabel.GetType().GetInterface("IDrugPrint") == null)
                {
                    System.Windows.Forms.MessageBox.Show("不符合接口");
                    return -1;
                }
                FS.HISFC.Components.DrugStore.Function.IDrugPrint = oLabel as FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint;
            }
            catch (System.TypeLoadException ex)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("标签命名空间无效\n" + ex.Message));
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 设置配置标签打印接口
        /// </summary>
        /// <returns></returns>
        public static int InitCompoundPrintInterface()
        {
            object factoryInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.DrugStore.Function), typeof(FS.HISFC.BizProcess.Interface.Pharmacy.ICompoundPrint));
            
            FS.HISFC.Components.DrugStore.Function.ICompoundPrint = factoryInstance as  FS.HISFC.BizProcess.Interface.Pharmacy.ICompoundPrint;

            if (FS.HISFC.Components.DrugStore.Function.ICompoundPrint == null)
            {
                return -1;
            }

            return 1;
            
            //object[] o = new object[] { };

            //try
            //{
            //    //门诊标签打印接口实现类
            //    FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            //    string labelValue = ctrlIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Compound_Print_Label, true, "FS.Report.DrugStore.ucCompoundLabel");

            //    System.Runtime.Remoting.ObjectHandle objHandel = System.Activator.CreateInstance("Report", labelValue , false, System.Reflection.BindingFlags.CreateInstance, null, o, null, null, null);
            //    object oLabel = objHandel.Unwrap();

            //    if (oLabel.GetType().GetInterface("ICompoundPrint") == null)
            //    {
            //        System.Windows.Forms.MessageBox.Show("不符合接口");
            //        return -1;
            //    }

            //    FS.HISFC.Components.DrugStore.Function.ICompoundPrint = oLabel as FS.HISFC.BizProcess.Interface.Pharmacy.ICompoundPrint;
            //}
            //catch (System.TypeLoadException ex)
            //{
            //    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            //    System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("标签命名空间无效\n" + ex.Message));
            //    return -1;
            //}

            //return 1;
        }

        /// <summary>
        /// 调用接口打印制定数据
        /// </summary>
        /// <param name="alData">待打印数据</param>
        /// <param name="isPrintDetail">是否打印明细执行单</param>
        /// <param name="isPrintLabel">是否打印标签</param>
        /// <returns>成功返回1，失败返回－1</returns>
        internal static int PrintCompound(ArrayList alData, bool isPrintDetail, bool isPrintLabel)
        {
            if (Function.ICompoundPrint == null)
            {
                InitCompoundPrintInterface();
            }

            if (Function.ICompoundPrint == null)
            {
                return -1;
            }

            ArrayList alGroupApplyOut = new ArrayList();
            ArrayList alCombo = new ArrayList();
            string privCombo = "-1";

            #region 标签打印

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                if (privCombo == "-1" || (privCombo == info.CompoundGroup && info.CompoundGroup != ""))
                {
                    alCombo.Add(info.Clone());
                    privCombo = info.CompoundGroup;
                    continue;
                }
                else			//不同处方号
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
            //清空数据显示
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
        /// 显示提示信息
        /// </summary>
        /// <param name="errStr">提示信息</param>
        public static void ShowMsg(string strMsg)
        {
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg(strMsg));
        }

        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] printType = new Type[1];
                printType[0] = typeof(FS.HISFC.BizProcess.Interface.Pharmacy.ICompoundPrint);

                return printType;
            }
        }

        #endregion
    }

    /// <summary>
    /// 门诊功能模块说明
    /// </summary>
    public enum OutpatientFun
    {
        /// <summary>
        /// 配药
        /// </summary>
        Drug ,
        /// <summary>
        /// 发药
        /// </summary>
        Send ,
        /// <summary>
        /// 直接发药 无配药步骤
        /// </summary>
        DirectSend ,
        /// <summary>
        /// 还药
        /// </summary>
        Back
    }

    /// <summary>
    /// 门诊配发药窗口功能
    /// </summary>
    public enum OutpatientWinFun
    {
        配药 ,
        发药 ,
        直接发药 ,
        还药 ,
        其他药房配药 ,
        其他药房发药
    }

    /// <summary>
    /// 单据检索方式
    /// </summary>
    public enum OutpatientBillType
    {
        /// <summary>
        /// 处方号
        /// </summary>
        处方号 = 0 ,
        /// <summary>
        /// 发票号
        /// </summary>
        发票号 = 1 ,
        /// <summary>
        /// 病例卡号
        /// </summary>
        病例卡号 = 2
    }
}
