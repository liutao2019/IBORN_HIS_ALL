using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace Neusoft.HISFC.Components.Manager.Controls
{
    /// <summary>
    /// [功能描述: 人员CA签名信息]<br></br>
    /// [创 建 者: 李雪龙]<br></br>
    /// [创建时间: 2014－12－18]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucCaEmployeeInfoPanel : UserControl
    {
        //人员管理类
        Neusoft.HISFC.BizLogic.Manager.Person personMgr = new Neusoft.HISFC.BizLogic.Manager.Person();
        //人员实体类
        Neusoft.HISFC.Models.Base.Employee employee = new Neusoft.HISFC.Models.Base.Employee();
        //常数管理类
        Neusoft.HISFC.BizLogic.Manager.Constant consManager = new Neusoft.HISFC.BizLogic.Manager.Constant();

        public ucCaEmployeeInfoPanel()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 有参构造函数
        /// </summary>
        /// <param name="empl"></param>
        public ucCaEmployeeInfoPanel(Neusoft.HISFC.Models.Base.Employee empl)
        {
            InitializeComponent();
            this.employee = empl;
            setInfoToControls();
        }

        /// <summary>
        /// 根据参数类型获得ArrayList
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private ArrayList GetConstant(Neusoft.HISFC.Models.Base.EnumConstant type)
        {

            ArrayList constList = consManager.GetList(type);
            if (constList == null)
                throw new Neusoft.FrameWork.Exceptions.ReturnNullValueException();

            return constList;

        }

        /// <summary>
        /// 根据传入对象信息填充UC
        /// </summary>
        private void setInfoToControls()
        {
            this.neuLabel1.Text = this.employee.ID + " " + this.employee.Name;//人员编码姓名
            this.pbEmplNo.Image = this.CreateBarCode(this.employee.ID);//人员编码条码
            this.pbEmplSign.Image = Bytes2Image(personMgr.QueryEmplSignDataByEmplNo(this.employee.ID));//人员签名
        }

        /// <summary>
        /// 验证方法
        /// </summary>
        /// <returns></returns>
        private bool ValueValidated()
        {
            //人员代码不能为空
            if (this.neuLabel1.Text.Trim() == "")
            {
                MessageBox.Show("人员代码不能为空！", "提示", MessageBoxButtons.OK);
                return false;
            }            
            return true;
        }

        /// <summary>
        /// 将控件内容转换成人员实体
        /// </summary>
        private Neusoft.HISFC.Models.Base.Employee ConvertUcContextToObject()
        {
            Neusoft.HISFC.Models.Base.Employee employee = new Neusoft.HISFC.Models.Base.Employee();
            employee.User01 = this.neuLabel1.Text.Trim();//人员编码

            return employee;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (Save() == 0)
                this.FindForm().DialogResult = DialogResult.OK;
        }
        /// <summary>
        /// 保存方法
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            //验证控件内容符合要求
            if (ValueValidated())
            {
                Neusoft.HISFC.Models.Base.Employee empl = ConvertUcContextToObject();
                if (empl == null) return -1;
                try
                {
                    Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

                    Neusoft.HISFC.BizLogic.Manager.Person perMgr = new Neusoft.HISFC.BizLogic.Manager.Person();

                    if (perMgr.Insert(empl) == -1)
                    {
                        if (perMgr.DBErrCode == 1)
                        {
                            if (perMgr.Update(empl) == -1 || perMgr.Update(empl) == 0)
                            {
                                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("更新人员失败！");
                                return -1;
                            }
                            else
                            {
                                //嵌入对其他系统或其他业务模块的信息传送桥接处理
                                string errInfo = "";
                                ArrayList alInfo = new ArrayList();
                                alInfo.Add(empl);
                                int param = Function.SendBizMessage(alInfo, Neusoft.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Modify, Neusoft.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Employee, ref errInfo);

                                if (param == -1)
                                {
                                    Neusoft.FrameWork.Management.PublicTrans.RollBack(); ;
                                    Function.ShowMessage("人员添加失败，请向系统管理员报告错误信息：" + errInfo, MessageBoxIcon.Error);
                                    return -1;
                                }

                            }
                        }
                        else
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("插入人员失败！");
                            return -1;
                        }
                    }
                    else
                    {
                        //嵌入对其他系统或其他业务模块的信息传送桥接处理
                        string errInfo = "";
                        ArrayList alInfo = new ArrayList();
                        alInfo.Add(empl);
                        int param = Function.SendBizMessage(alInfo, Neusoft.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add, Neusoft.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Employee, ref errInfo);

                        if (param == -1)
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack(); ;
                            Function.ShowMessage("人员添加失败，请向系统管理员报告错误信息：" + errInfo, MessageBoxIcon.Error);
                            return -1;
                        }

                    }
                    Neusoft.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show("保存成功！");
                    return 0;

                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message);
                    return -1;
                }

            }
            else
            {
                return -1;
            }

        }

        /// <summary>
        /// 生成条形码方法
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private Image CreateBarCode(string code)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
            BarcodeLib.AlignmentPositions Align = BarcodeLib.AlignmentPositions.CENTER;
            b.IncludeLabel = false;
            b.Alignment = Align;
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, 150, 50);
        }

        /// <summary>
        /// byte[]转Image
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Image Bytes2Image(byte[] imgData)
        {
            try
            {
                if (imgData == null) return null;
                System.IO.MemoryStream ms = new System.IO.MemoryStream(imgData);
                return System.Drawing.Image.FromStream(ms);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Image转byte[]
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        //public static byte[] Image2Bytes(Image img)
        //{
        //    try
        //    {
        //        if (img == null) return null;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
        private void btClose_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }
    }
}
