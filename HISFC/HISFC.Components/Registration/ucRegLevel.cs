using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Registration
{
    /// <summary>
    /// [功能描述: 挂号级别维护]<br></br>
    /// [创 建 者: 黄小卫]<br></br>
    /// [创建时间: 2007-2-8]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucRegLevel:UserControl,FS.FrameWork.WinForms.Forms.IMaintenanceControlable
    {
        public ucRegLevel()
        {
            InitializeComponent();
        }

        #region 变量
        /// <summary>
        /// 挂号级别管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.RegLevel regLvlMgr = new FS.HISFC.BizLogic.Registration.RegLevel();

        #endregion

         #region IMaintenanceControlable 成员
        /// <summary>
        /// 添加行
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);
            int row = this.fpSpread1_Sheet1.RowCount - 1;

            this.fpSpread1_Sheet1.SetValue(row, 3, 0, false);

            this.fpSpread1.Focus();
            this.fpSpread1_Sheet1.SetActiveCell(row, 0);

            return 0;
        }

        public int Copy()
        {
            return 0;
        }

        public int Cut()
        {
            return 0;
        }

        /// <summary>
        /// 删除行
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;

            if (row < 0 || this.fpSpread1_Sheet1.RowCount == 0) return 0;

            if (MessageBox.Show(FS.FrameWork.Management.Language.Msg("是否删除该记录"), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.No)
                return 0;

            if (this.fpSpread1_Sheet1.Rows[row].Tag != null)
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(this.regLvlMgr.Connection);                 
                //SQLCA.BeginTransaction();

                try
                {
                    FS.HISFC.Models.Registration.RegLevel regLvl = (FS.HISFC.Models.Registration.RegLevel)this.fpSpread1_Sheet1.Rows[row].Tag;
                    this.regLvlMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                    if (this.regLvlMgr.Delete(regLvl.ID) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.regLvlMgr.Err, "提示");
                        return -1;
                    }

                    //嵌入对其他系统或其他业务模块的信息传送桥接处理
                    FS.HISFC.Models.Registration.RegLvlFee regLevelfee = new FS.HISFC.Models.Registration.RegLvlFee();
                    regLevelfee.RegLevel = regLvl;
                    regLevelfee.RegFee = 0;
                    regLevelfee.OwnDigFee = 0;  //预约挂号系统默认为0
                    string errInfo = "";
                    ArrayList alInfo = new ArrayList();
                    alInfo.Add(regLevelfee);
                    int param = Function.SendBizMessage(alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Delete, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.RegLevel, ref errInfo);

                    if (param == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack(); ;
                        Function.ShowMessage("挂号级别删除失败，请向系统管理员报告错误信息：" + errInfo, MessageBoxIcon.Error);
                        return -1;
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();
                }
                catch (Exception e)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(e.Message, "提示");
                    return -1;
                }
            }

            this.fpSpread1_Sheet1.Rows.Remove(row, 1);
            MessageBox.Show( FS.FrameWork.Management.Language.Msg( "删除成功" ), "提示" );
            
            return 0;
        }

        public int Export()
        {
            return 0;
        }

        public int Import()
        {
            return 0;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            FarPoint.Win.Spread.InputMap im;

            im = this.fpSpread1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            FarPoint.Win.Spread.CellType.NumberCellType number = new FarPoint.Win.Spread.CellType.NumberCellType();
            //number.MaximumValue = 1000; 
            //this.fpSpread1_Sheet1.Columns[3].CellType = number;


            return 0;
        }

        public bool IsDirty
        {
            get
            {
                return false;
            }
            set
            {
                //
            }
        }

        public int Modify()
        {
            return 0;
        }

        public int NextRow()
        {
            return 0;
        }

        public int Paste()
        {
            return 0;
        }

        public int PreRow()
        {
            return 0;
        }

        public int Print()
        {
            FS.FrameWork.WinForms.Classes.Print printer = new FS.FrameWork.WinForms.Classes.Print();
            printer.PrintPage(0, 0, this);

            return 0;
        }

        public int PrintConfig()
        {
            return 0;
        }

        public int PrintPreview()
        {
            return 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public int Query()
        {
            if (this.fpSpread1_Sheet1.RowCount > 0)
                this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);

            ArrayList al = this.regLvlMgr.Query();
            if (al == null)
            {
                MessageBox.Show("获取挂号级别出错!" + this.regLvlMgr.Err, "提示");
                return -1;
            }

            foreach (FS.HISFC.Models.Registration.RegLevel obj in al)
            {
                this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);

                int row = this.fpSpread1_Sheet1.RowCount - 1;
                this.fpSpread1_Sheet1.SetValue(row, 0, obj.ID, false);
                this.fpSpread1_Sheet1.SetValue(row, 1, obj.Name, false);
                this.fpSpread1_Sheet1.SetValue(row, 2, obj.UserCode, false);
                this.fpSpread1_Sheet1.SetValue(row, 3, obj.SortID, false);
                this.fpSpread1_Sheet1.SetValue(row, 4, !obj.IsValid, false);
                this.fpSpread1_Sheet1.SetValue(row, 5, obj.IsExpert, false);
                this.fpSpread1_Sheet1.SetValue(row, 6, obj.IsFaculty, false);
                this.fpSpread1_Sheet1.SetValue(row, 7, obj.IsSpecial, false);
                this.fpSpread1_Sheet1.SetValue(row, 8, obj.IsDefault, false);
                //{156C449B-60A9-4536-B4FB-D00BC6F476A1}
                this.fpSpread1_Sheet1.SetValue(row, 9, obj.IsEmergency, false); 


                this.fpSpread1_Sheet1.Rows[row].Tag = obj;
            }

            return 0;
        }

        public FS.FrameWork.WinForms.Forms.IMaintenanceForm QueryForm
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        /// <summary>
        /// 校验{2A9D4F15-E882-4e5c-9D53-63C6E3904A1B}
        /// </summary>
        /// <returns></returns>
        protected virtual int Valid()
        {
            //校验必须输入项
            int returnResult = this.ValidMustInput() ;
            if (returnResult < 0)
            {
                return -1;
            }

            //校验重复代码
            returnResult = this.ValidRepeatLevelCode();
            if (returnResult < 0)
            {
                return -1;
            }

            //校验重复挂号级别名称
            returnResult = this.ValidRepeatLevelName();
            {
                if (returnResult < 0)
                {
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// 校验是否重复编码{2A9D4F15-E882-4e5c-9D53-63C6E3904A1B}
        /// </summary>
        /// <returns></returns>
        protected virtual int ValidRepeatLevelCode()
        {
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                //编码
                string strLevelCode = this.fpSpread1_Sheet1.Cells[i, 0].Text.Trim();
                for (int j = i + 1; j < this.fpSpread1_Sheet1.RowCount; j++)
                {
                    string strLevelCodeNext = this.fpSpread1_Sheet1.Cells[j, 0].Text.Trim();
                    if (strLevelCode == strLevelCodeNext)
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("编码为" + strLevelCode + "的挂号级别已经存在"),
                            "提示", MessageBoxButtons.OK);
                        return -1;
                    }
                }
 
            }
            
            return 1;
        }

        /// <summary>
        /// 校验重复名称{2A9D4F15-E882-4e5c-9D53-63C6E3904A1B}
        /// </summary>
        /// <returns></returns>
        protected virtual int ValidRepeatLevelName()
        {
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                //编码
                string strLevelName = this.fpSpread1_Sheet1.Cells[i, 1].Text.Trim();
                for (int j = i + 1; j < this.fpSpread1_Sheet1.RowCount; j++)
                {
                    string strLevelNameNext = this.fpSpread1_Sheet1.Cells[j, 1].Text.Trim();
                    if (strLevelName == strLevelNameNext)
                    {
                       DialogResult d = MessageBox.Show(FS.FrameWork.Management.Language.Msg("挂号级别为" + strLevelName + "的挂号级别已经存在,是否继续"),
                            "提示", MessageBoxButtons.OKCancel);
                       if (d == DialogResult.Cancel)
                       {
                           return -1;
                       }
                       else
                       {
                           return 1;
                       }
                    }
                }

            }
            return 1;
        }



        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            this.fpSpread1.StopCellEditing();

            if (this.Valid() == -1) return -1;

            FS.HISFC.Models.Registration.RegLevel regLevel;
            DateTime operDate = this.regLvlMgr.GetDateTimeFromSysDateTime();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(this.regLvlMgr.Connection);
            //SQLCA.BeginTransaction();

            try
            {
                this.regLvlMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    //嵌入对其他系统或其他业务模块的信息传送桥接处理
                    FS.HISFC.Models.Registration.RegLvlFee regLevelfee = new FS.HISFC.Models.Registration.RegLvlFee();
                    string errInfo = "";
                    ArrayList alDelInfo = new ArrayList();//删除消息
                    ArrayList alAddinfo = new ArrayList();//增加信息
                    int param = 0;

                    if (this.fpSpread1_Sheet1.Rows[i].Tag != null)
                    {
                        regLevel = (FS.HISFC.Models.Registration.RegLevel)this.fpSpread1_Sheet1.Rows[i].Tag;
                        if (this.regLvlMgr.Delete(regLevel.ID) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.regLvlMgr.Err, "提示");
                            return -1;
                        }

                        regLevelfee.RegLevel = regLevel;
                        regLevelfee.RegFee = 0;
                        regLevelfee.OwnDigFee = 0;  //预约挂号系统默认为0

                        alDelInfo.Add(regLevelfee);
                        param = Function.SendBizMessage(alDelInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Delete, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.RegLevel, ref errInfo);

                        if (param == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            Function.ShowMessage("挂号级别维护失败，请向系统管理员报告错误信息：" + errInfo, MessageBoxIcon.Error);
                            return -1;
                        }

                    }
                    else
                    {
                        regLevel = new FS.HISFC.Models.Registration.RegLevel();
                    }

                    #region 赋值
                    regLevel.ID = this.fpSpread1_Sheet1.GetText(i, 0);//代码
                    regLevel.Name = this.fpSpread1_Sheet1.GetText(i, 1);//名称
                    regLevel.UserCode = this.fpSpread1_Sheet1.GetText(i, 2);//助记码
                    regLevel.SortID = int.Parse(this.fpSpread1_Sheet1.GetText(i, 3));//显示顺序
                    //是否停用
                    if (this.fpSpread1_Sheet1.GetText(i, 4).ToUpper() == "TRUE")
                    {
                        regLevel.IsValid = false;
                    }
                    else
                    {
                        regLevel.IsValid = true;
                    }

                    //是否专家号
                    if (this.fpSpread1_Sheet1.GetText(i, 5).ToUpper() == "TRUE")
                    {
                        regLevel.IsExpert = true;
                    }
                    else
                    {
                        regLevel.IsExpert = false;
                    }
                    //是否专科号
                    if (this.fpSpread1_Sheet1.GetText(i, 6).ToUpper() == "TRUE")
                    {
                        regLevel.IsFaculty = true;
                    }
                    else
                    {
                        regLevel.IsFaculty = false;
                    }
                    //是否特诊号
                    if (this.fpSpread1_Sheet1.GetText(i, 7).ToUpper() == "TRUE")
                    {
                        regLevel.IsSpecial = true;
                    }
                    else
                    {
                        regLevel.IsSpecial = false;
                    }
                    //是否默认项
                    if (this.fpSpread1_Sheet1.GetText(i, 8).ToUpper() == "TRUE")
                    {
                        regLevel.IsDefault = true;
                    }
                    else
                    {
                        regLevel.IsDefault = false;
                    }

                    //{156C449B-60A9-4536-B4FB-D00BC6F476A1}
                    //是否急诊号
                    if (this.fpSpread1_Sheet1.GetText(i, 9).ToUpper() == "TRUE")
                    {
                        regLevel.IsEmergency = true;
                    }
                    else
                    {
                        regLevel.IsEmergency = false;
                    }

                    regLevel.Oper.ID = this.regLvlMgr.Operator.ID;
                    regLevel.Oper.OperTime = operDate;
                    #endregion

                    if (this.regLvlMgr.Insert(regLevel) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.regLvlMgr.Err, "提示");
                        return -1;
                    }

                    //嵌入对其他系统或其他业务模块的信息传送桥接处理
                    regLevelfee.RegLevel = regLevel;
                    regLevelfee.RegFee = 0;
                    regLevelfee.OwnDigFee = 0;  //预约挂号系统默认为0

                    alAddinfo.Add(regLevelfee);
                    param = Function.SendBizMessage(alAddinfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.RegLevel, ref errInfo);

                    if (param == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack(); ;
                        Function.ShowMessage("挂号级别维护失败，请向系统管理员报告错误信息：" + errInfo, MessageBoxIcon.Error);
                        return -1;
                    }

                    this.fpSpread1_Sheet1.Rows[i].Tag = regLevel;
                }
                FS.FrameWork.Management.PublicTrans.Commit();

            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "提示");
                return -1;
            }

            MessageBox.Show( FS.FrameWork.Management.Language.Msg( "保存成功" ), "提示" );

            return 0;
        }

        /// <summary>
        /// 验证必须输入{2A9D4F15-E882-4e5c-9D53-63C6E3904A1B}
        /// </summary>
        /// <returns></returns>
        protected virtual int ValidMustInput()
        {
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                //判断挂号级别编码
                string strLevelCode = this.fpSpread1_Sheet1.GetText(i, 0).Trim();
                if ( string.IsNullOrEmpty(strLevelCode))
                {
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "请指定挂号级别代码" ), "提示" );
                    return -1;
                }

                //判断挂号级别代码长度
                if (FS.FrameWork.Public.String.ValidMaxLengh(strLevelCode,3) == false)
                {
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "挂号级别的输入码位数过长,编码为：" ) + strLevelCode );
                    return -1;
                }


                //判断挂号级别名称
                string strLevelName = this.fpSpread1_Sheet1.GetText(i, 1).Trim();
                if (string.IsNullOrEmpty(strLevelName))
                {
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "请指定挂号级别名称" ), "提示" );
                    return -1;
                }

                //判断挂号级别名称长度
                if (FS.FrameWork.Public.String.ValidMaxLengh(strLevelName, 20) == false)
                {
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "挂号级别的名称位数过长,已输入名称为：" ) + strLevelName );
                    return -1;
                }



                //判断挂号输入码
                string strInputCode = this.fpSpread1_Sheet1.GetText(i, 2).Trim();
                if (FS.FrameWork.Public.String.ValidMaxLengh(strInputCode, 8) == false)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("输入码位数过长"));
                    return -1;
                }


                //判断名称
                if (this.fpSpread1_Sheet1.GetText(i, 3).Trim() == "")
                {
                    this.fpSpread1_Sheet1.SetValue(i, 3, 0);
                }
                
            }

            return 0;
        }
        #endregion

        #region 事件
        /// <summary>
        /// 按键处理
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.fpSpread1_Sheet1.ActiveColumnIndex >= 0 || this.fpSpread1_Sheet1.ActiveColumnIndex <
                    this.fpSpread1_Sheet1.ColumnCount)
                {
                    this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex,
                        this.fpSpread1_Sheet1.ActiveColumnIndex + 1, false);
                }
            }

            return base.ProcessDialogKey(keyData);
        }        

        /// <summary>
        /// 切换cell事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            if (e.Column == 5)
            {
                if (this.fpSpread1_Sheet1.GetText(e.Row, e.Column).ToUpper() == "TRUE")
                {
                    this.fpSpread1_Sheet1.SetValue(e.Row, 6, false, false);
                    this.fpSpread1_Sheet1.SetValue(e.Row, 7, false, false);
                }
            }
            else if (e.Column == 6)
            {
                if (this.fpSpread1_Sheet1.GetText(e.Row, e.Column).ToUpper() == "TRUE")
                {
                    this.fpSpread1_Sheet1.SetValue(e.Row, 5, false, false);
                    this.fpSpread1_Sheet1.SetValue(e.Row, 7, false, false);
                }
            }
            else if (e.Column == 7)
            {
                if (this.fpSpread1_Sheet1.GetText(e.Row, e.Column).ToUpper() == "TRUE")
                {
                    this.fpSpread1_Sheet1.SetValue(e.Row, 5, false, false);
                    this.fpSpread1_Sheet1.SetValue(e.Row, 6, false, false);
                }
            }
        }
        #endregion
    }
}
