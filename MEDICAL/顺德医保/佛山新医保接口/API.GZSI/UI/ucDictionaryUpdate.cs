using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace API.GZSI.UI
{
    /// <summary>
    /// 4.8.3	码表服务接口，获取码表界面
    /// </summary>
    public partial class ucDictionaryUpdate : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        


        public ucDictionaryUpdate()
        {
            InitializeComponent();
        }

        #region 菜单

        FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolbarService.AddToolButton("下载码表", "下载并更新本地码表", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D导入, true, false, null);
            return toolbarService;

        }

        /// <summary>
        /// 菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "下载码表":
                    //this.DownLoadDictionary();
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// 下载并更新码表
        /// </summary>
        //private void DownLoadDictionary()
        //{
        //    if (this.cmbCodeType.SelectedItem == null)
        //    {
        //        FS.FrameWork.WinForms.Classes.Function.Msg("请选择代码类别！", 111);
        //        return;
        //    }
        //    this.fpSpread1_Sheet1.RowCount = 0;

        //    string codeType = this.cmbCodeType.SelectedItem.ID;

        //    Models.Request.RequestBizh120205 req = new API.GZSI.Models.Request.RequestBizh120205();
        //    req.aaa100 = codeType;
        //    //req.akb020 = Models.UserInfo.Instance.userId;
        //    Models.Response.ResponseBase rb = null;
        //    string msg = string.Empty;
        //    if (req.Call(out rb, out msg))
        //    {
        //        Models.Response.ResponseBizh120205 res = rb as Models.Response.ResponseBizh120205;
        //        if (res == null)
        //        {
        //            FS.FrameWork.WinForms.Classes.Function.Msg("返回实体转换错误！", 111);
        //            return;
        //        }
        //        Common.Function.ShowOutDateToFarpoint<Models.CodeInfo>(this.fpSpread1_Sheet1, res.codeinfo);
               
        //        // 保存本地，增量保存
        //        SaveCode(res);

        //    }
        //    else
        //    {
        //        FS.FrameWork.WinForms.Classes.Function.Msg(msg, 111);
        //    }
        //}


        #endregion 菜单


        /// <summary>
        /// 保存本地，增量保存
        /// </summary>
        /// <param name="res"></param>
        //private void SaveCode(Models.Response.ResponseBizh120205 res)
        //{
        //    if (res == null || res.codeinfo == null)
        //    {
        //        return;
        //    }
        //    if (this.cmbCodeType.SelectedItem == null)
        //    {
        //        return;
        //    }

        //    FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();

        //    string codeType = Common.Constants.GDSZSI_CODE_PREFIX + this.cmbCodeType.SelectedItem.ID;
        //     ArrayList alConstList = new ArrayList();
        //    if (!this.cbAddSave.Checked)
        //    {
        //        // 非增量下载时清空本地字典
        //        if (constMgr.DelConstant(codeType) == -1)
        //        {
        //            FS.FrameWork.WinForms.Classes.Function.Msg("保存码表到本地失败！" + constMgr.Err, 111);
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        // 增量下载时获取本地字典
        //        alConstList = constMgr.GetAllList(codeType);
        //    }
            
            
        //    int nSaveCount = 0;

        //    foreach (var item in res.codeinfo)
        //    {
        //        if (IsSave(alConstList, item))
        //        {
        //            FS.HISFC.Models.Base.Const c = new FS.HISFC.Models.Base.Const();
        //            c.ID = item.aaa102;
        //            c.Name = item.aaa103;
        //            c.Memo = item.aaa027;
        //            c.UserCode = item.aaa104;
        //            c.IsValid = true;
        //            //c.SpellCode = 
        //            if (constMgr.InsertItem(codeType, c) == -1)
        //            {
        //                FS.FrameWork.WinForms.Classes.Function.Msg("保存码表到本地失败！" + constMgr.Err,111);
        //                return;
        //            }

        //            nSaveCount++;
        //        }
        //    }

        //    FS.FrameWork.WinForms.Classes.Function.Msg("下载码表成功！新增" + nSaveCount.ToString() + "条。", 111);
        //}

        /// <summary>
        /// 是否保存本地
        /// </summary>
        /// <param name="al">本地字典</param>
        /// <param name="item">下载字典项</param>
        /// <returns>ture保存;false不保存</returns>
        //private bool IsSave(ArrayList al, API.GZSI.Models.CodeInfo item)
        //{
        //    //if (al == null || al.Count == 0 || item == null)
        //    //    return false;
        //    for (int i = 0; i < al.Count; i++)
        //    {
        //        FS.HISFC.Models.Base.Const c = al[i] as FS.HISFC.Models.Base.Const;
        //        if (c == null)
        //            continue;
        //        if (c.ID == item.aaa102)
        //        {
        //            return false;
        //        }
        //    }

        //    return true;
        //}

        /// <summary>
        /// 选择代码类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCodeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDictionary();
        }

        /// <summary>
        /// 加载本地字典
        /// </summary>
        private void LoadDictionary()
        {
            if (this.cmbCodeType.SelectedItem == null)
            {
                return;
            }

            string codeType = Common.Constants.GZSI_CODE_PREFIX + this.cmbCodeType.SelectedItem.ID;

            ArrayList al = new FS.HISFC.BizLogic.Manager.Constant().GetAllList(codeType);
            if (al==null || al.Count==0)
            {
                this.fpSpread1_Sheet1.RowCount = 0;
                return;
            }
            List<Models.CodeInfo> ls = new List<API.GZSI.Models.CodeInfo>();
            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Base.Const c = al[i] as FS.HISFC.Models.Base.Const;
                if (c == null)
                    continue;
                Models.CodeInfo code = new API.GZSI.Models.CodeInfo();
                code.aaa027 = c.Memo;
                code.aaa100 = this.cmbCodeType.SelectedItem.ID;
                code.aaa101 = this.cmbCodeType.SelectedItem.Name;
                code.aaa102 = c.ID;
                code.aaa103 = c.Name;
                code.aaa104 = c.UserCode;
                ls.Add(code);
            }

            Common.Function.ShowOutDateToFarpoint<Models.CodeInfo>(this.fpSpread1_Sheet1, ls);
        }

        private void ucDictionaryUpdate_Load(object sender, EventArgs e)
        {
            this.cmbCodeType.AddItems(new FS.HISFC.BizLogic.Manager.Constant().GetAllList(Common.Constants.GZSI_CODELIST));
        }
    }
}
