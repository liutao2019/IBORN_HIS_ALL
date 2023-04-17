using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Components.PharmacyCommon
{
    /// <summary>
    /// [功能描述: 药品管理用函数集合]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// </summary>
    public class Function
    {
        #region 权限控制

        /// <summary>
        /// 判断是否有权限
        /// </summary>
        /// <param name="deptNO">权限科室</param>
        /// <param name="class2Code">二级权限</param>
        /// <param name="class3Code">三级权限</param>
        /// <returns>true有，false无</returns>
        public static bool JugePrive(string deptNO, string class2Code, string class3Code)
        {
            if (string.IsNullOrEmpty(deptNO) || string.IsNullOrEmpty(class2Code) || string.IsNullOrEmpty(class3Code))
            {
                return false;
            }
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager powerDetailManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            return powerDetailManager.JudgeUserPriv(powerDetailManager.Operator.ID, deptNO, class2Code, class3Code);
        }

        /// <summary>
        /// 判断是否有权限
        /// </summary>
        /// <param name="class2Code">二级权限</param>
        /// <param name="class3Code">三级权限</param>
        /// <returns>true有，false无</returns>
        public static bool JugePrive(string class2Code, string class3Code)
        {
            if (string.IsNullOrEmpty(class2Code) || string.IsNullOrEmpty(class3Code))
            {
                return false;
            }
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager powerDetailManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            List<FS.FrameWork.Models.NeuObject> listPrive = powerDetailManager.QueryUserPriv(powerDetailManager.Operator.ID, class2Code, class3Code);
            if (listPrive == null)
            {
                return false;
            }

            return listPrive.Count > 0;
        }

        /// <summary>
        /// 取当前操作员是否有某一权限。
        /// </summary>
        /// <param name="class2Code">二级权限编码</param>
        /// <returns>True 有权限, False 无权限</returns>
        public static bool JugePrive(string class2Code)
        {
            List<FS.FrameWork.Models.NeuObject> al = null;
            //权限管理类
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager privManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            //取操作员拥有权限的科室
            al = privManager.QueryUserPriv(privManager.Operator.ID, class2Code);

            if (al == null || al.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 获取当前所有权限科室集合
        /// </summary>
        /// <param name="class2Code">二级权限编码</param>
        /// <param name="class3Code">三级权限编码</param>
        /// <param name="isShowErrMsg">是否弹出错误信息</param>
        /// <returns>成功返回拥有权限科室列表 失败返回null</returns>
        public static List<FS.FrameWork.Models.NeuObject> QueryPrivList(string class2Code, string class3Code, bool isShowErrMsg)
        {
            List<FS.FrameWork.Models.NeuObject> al = null;
            //权限管理类
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager privManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            //取操作员拥有权限的科室
            if (class3Code == null || class3Code == "")
                al = privManager.QueryUserPriv(privManager.Operator.ID, class2Code);
            else
                al = privManager.QueryUserPriv(privManager.Operator.ID, class2Code, class3Code);

            if (al == null)
            {
                if (isShowErrMsg)
                {
                    System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg(privManager.Err));
                }
                return null;
            }
            if (al.Count == 0)
            {
                if (isShowErrMsg)
                {
                    System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("您没有此窗口的操作权限"));
                }
                return al;
            }

            //成功则权限科室数组
            return al;
        }

        /// <summary>
        /// 获取当前所有权限科室集合
        /// </summary>
        /// <param name="class2Code">二级权限编码</param>
        /// <param name="isShowErrMsg">是否弹出错误信息</param>
        /// <returns>成功返回拥有权限科室列表 失败返回null</returns>
        public static List<FS.FrameWork.Models.NeuObject> QueryPrivList(string class2Code, bool isShowErrMsg)
        {
            return Function.QueryPrivList(class2Code, null, isShowErrMsg);
        }

        /// <summary>
        /// 弹出窗口，选择权限科室，如果用户只有一个科室的权限，则可以直接登陆
        /// </summary>
        /// <param name="class2Code">二级权限编码</param>
        /// <param name="privDept">权限科室</param>
        /// <returns>1 返回正常权限科室 0 用户选择取消 －1 用户无权限</returns>
        public static int ChoosePriveDept(string class2Code, ref FS.FrameWork.Models.NeuObject privDept)
        {
            return ChoosePrivDept(class2Code, null, ref privDept);
        }

        /// <summary>
        /// 弹出窗口，选择权限科室，如果用户只有一个科室的权限，则可以直接登陆
        /// </summary>
        /// <param name="class2Code">二级权限编码</param>
        /// <param name="class3Code">三级权限编码</param>
        /// <param name="privDept">权限科室</param>
        /// <returns>1 返回正常权限科室 0 用户选择取消 －1 用户无权限</returns>
        public static int ChoosePrivDept(string class2Code, string class3Code, ref FS.FrameWork.Models.NeuObject privDept)
        {
            List<FS.FrameWork.Models.NeuObject> al = new List<FS.FrameWork.Models.NeuObject>();
            if (class3Code == null || class3Code == "")
                al = QueryPrivList(class2Code, true);
            else
                al = QueryPrivList(class2Code, class3Code, true);

            if (al == null || al.Count == 0)
            {
                return -1;
            }

            //如果用户只有一个科室的权限，则返回此科室
            if (al.Count == 1)
            {
                privDept = al[0] as FS.FrameWork.Models.NeuObject;
                return 1;
            }

            //弹出窗口，取权限科室
            FS.SOC.HISFC.Components.PharmacyCommon.frmChoosePrivDept formPrivDept = new FS.SOC.HISFC.Components.PharmacyCommon.frmChoosePrivDept();
            formPrivDept.SetPriv(al, true);
            System.Windows.Forms.DialogResult Result = formPrivDept.ShowDialog();

            //取窗口返回权限科室
            if (Result == System.Windows.Forms.DialogResult.OK)
            {
                privDept = formPrivDept.SelectData;
                return 1;
            }

            return 0;
        }

        #endregion

    }
}
