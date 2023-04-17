using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Windows;

namespace FS.SOC.Local.Order.OutPatientOrder.GYZL
{
    class PacsInterface : FS.HISFC.BizProcess.Interface.Common.IPacs
    {
        #region 变量属性
        /// <summary>
        /// 错误信息
        /// </summary>
        string errInfo = string.Empty;
        /// <summary>
        /// 错误编码
        /// </summary>
        string errCode = string.Empty;

        string oprationMode = string.Empty;

        string pacsViewType = string.Empty;

        string strMsg = string.Empty;

        FS.HISFC.Models.RADT.Patient curPatient;

        FS.HISFC.Models.RADT.Patient CurPatient
        {
            get
            {
                return this.curPatient;
            }
            set
            {
                this.curPatient = value;
            }
        }

        FS.HISFC.Models.Order.Order curOrder;

        FS.HISFC.Models.Order.Order CurOrder
        {
            get
            {
                return this.curOrder;
            }
            set
            {
                this.curOrder = value;
            }
        }
        #endregion

        #region 业务层
        /// <summary>
        /// 控制参数
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        #endregion

        #region IPacs成员
        // 摘要:
        //     错误编码
        public string ErrCode
        {
            get
            {
                return this.errCode;
            }
            set
            {
                this.errCode = value;
            }
        }
        //
        // 摘要:
        //     错误信息
        public string ErrMsg
        {
            get
            {
                return this.errInfo;
            }
            set
            {
                this.errInfo = value;
            }
        }
        //
        // 摘要:
        //     调用模式(1:门诊2:住院)
        public string OprationMode
        {
            get
            {
                return this.oprationMode;
            }
            set
            {
                this.oprationMode = value;
            }
        }
        //
        // 摘要:
        //     pacs结果察看类型1：图像2：报告
        public string PacsViewType
        {
            get
            {
                return this.pacsViewType;
            }
            set
            {
                this.pacsViewType = value;
            }
        }

        // 摘要:
        //     检查医嘱项目是否可以开立
        //
        // 参数:
        //   Order:
        public bool CheckOrder(FS.HISFC.Models.Order.Order Order)
        {
            return true;
        }
        //
        // 摘要:
        //     提交
        public int Commit()
        {
            return 1;
        }
        //
        // 摘要:
        //     数据库连接
        //
        // 返回结果:
        //     成功 1 失败 -1
        public int Connect()
        {
            return 1;
        }
        // 摘要:
        //     数据库关闭
        //
        // 返回结果:
        //     成功 1 失败 -1
        public int Disconnect()
        {
            return 1;
        }
        //
        // 摘要:
        //     检验结果是否已经生成
        //
        // 参数:
        //   id:
        public bool IsReportValid(string id)
        {
            return true;
        }
        //
        // 摘要:
        //     下组合医嘱
        //
        // 参数:
        //   OrderList:
        public int PlaceOrder(List<FS.HISFC.Models.Order.Order> OrderList)
        {
            return 1;
        }
        //
        // 摘要:
        //     下医嘱
        //
        // 参数:
        //   Order:
        public int PlaceOrder(FS.HISFC.Models.Order.Order Order)
        {
            return 1;
        }
        //
        // 摘要:
        //     查询检查结果
        //
        // 参数:
        //   PatientNo:
        public string[] QueryResult()
        {
            string[] parm = {"0","1"};
            return parm;
        }
        //
        // 摘要:
        //     回滚
        public int Rollback()
        {
            return 1;
        }

        /// <summary>
        /// 设置患者信息
        /// </summary>
        /// <param name="Patient"></param>
        /// <returns></returns>
        public int SetPatient(FS.HISFC.Models.RADT.Patient Patient)
        {
            this.CurPatient = Patient;
            return 1;
        }

        public void SetTrans(IDbTransaction t)
        {
            return;
        }

        public void ShowForm()
        {
            return;
        }

        public int ShowResult(string orderID)
        {
            return 1;
        }

        /// <summary>
        /// 结果类型
        /// </summary>
        string resultType;

        public string ResultType
        {
            get
            {
                return resultType;
            }
            set
            {
                resultType = value;
            }
        }

        /// <summary>
        /// 显示PACS报告或结果
        /// </summary>
        /// <returns></returns>
        public int ShowResultByPatient()
        {
            string txtFile = FS.FrameWork.WinForms.Classes.Function.CurrentPath + this.GetLocalPath("txtFile");
            if (System.IO.File.Exists(txtFile) == false)
            {
                this.ErrMsg = "没有找到本地配置文件:" + txtFile + ",请通知信息科人员维护！";
                return -1;
            }
            else
            {
                try
                {
                    //this.strMsg = "<MML>" + "\r\n" +
                    //"<NAME>" + this.CurPatient.Name + "</NAME>" + "\r\n" +
                    //"<ZYH>" + this.CurPatient.PID.CardNO + "</ZYH>" + "\r\n" +
                    //"<SFZH>" + this.CurPatient.IDCard + "</SFZH>" + "\r\n" +
                    //"<JZH>" + this.CurPatient.ID + "</JZH>" + "\r\n" +
                    //"<SEX>" + this.CurPatient.Sex.Name + "</SEX>" + "\r\n" +
                    //"<BQDM>" + this.CurPatient.User01 + "</BQDM>" + "\r\n" +
                    //"<NL>" + this.CurPatient.Age + "</NL>" + "\r\n" +
                    //"<BIRTHDAY>" + this.CurPatient.Birthday.ToString() + "</BIRTHDAY>" + "\r\n" +
                    //"<DOCTORID>" + this.CurPatient.DoctorReceiver.ID + "</DOCTORID>" + "\r\n" +
                    //"<DOCTORNAME>" + this.CurPatient.DoctorReceiver.Name + "</DOCTORNAME>" + "\r\n" +
                    //"<PATIENTLOCALID>" + this.CurPatient.ID + "</PATIENTLOCALID>" + "\r\n" +
                    //"<MODALITY></MODALITY>" + "\r\n" +
                    //"<IMAGEQX>0</IMAGEQX>" + "\r\n" +
                    //"</MML>" + "\r\n";

                    string exePath = this.GetLocalPath("exeFile");

                    //暂时 存储一个病历号，后续电子申请单分类后再做完善lingk20120713
                    //格式:Dxxxxxx -Hxxxx –Rxxxxx -Ixxxxx –Mxxxx -Nxxxx -Sxxxx -Pxxxxx
                    // D后内容为病区码、H后为住院号、R后为检查单申请号(临床科室申请号)、I后为影像检查号、
                    //M后为检查设备名称、N后为患者姓名、S后为患者性别、P后为权限编码

                    this.strMsg = "echo off " + "\r\n" +
                                  exePath + " -h" + this.CurPatient.PID.CardNO + "\r\n" +
                                  "exit";

                    using (StreamWriter sw = File.CreateText(txtFile))
                    {
                        sw.WriteLine(strMsg);
                    }



                    System.Diagnostics.Process.Start(txtFile);
                }
                catch
                {
                    this.ErrMsg = "接口异常错误，请联系信息科管理员！";
                    return -1;
                }
            }
            return 1;
        }
        #endregion

        #region  方法
        /// <summary>
        /// 获取路径方法
        /// </summary>
        /// <param name="fileType">文件类型</param>
        /// <returns></returns>
        private string GetLocalPath(string fileType)
        {
            Common.ComFunc cf = new FS.SOC.Local.Order.OutPatientOrder.GYZL.Common.ComFunc();
            string erro = "出错";
            string imgpath =cf.GetHospitalLogo("Xml\\PACSCONN.xml", "PACSPath", fileType, erro);
            return imgpath;
        }
        #endregion
    }
}
