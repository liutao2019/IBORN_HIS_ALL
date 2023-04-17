using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using API.GZSI.Models;
using API.GZSI.Business;

namespace API.GZSI.UI
{
    /// <summary>
    /// 4.8.3	码表服务接口，获取码表界面
    /// </summary>
    public partial class ucDictionaryUpdate : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 常数管理类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 公共返回值
        /// </summary>
        int returnvalue = 0;

        /// <summary>
        /// 交易流水号
        /// </summary>
        string SerialNumber = string.Empty;

        /// <summary>
        /// 交易版本号
        /// </summary>
        string strTransVersion = string.Empty;

        /// <summary>
        /// 交易验证码
        /// </summary>
        string strVerifyCode = string.Empty;

        public ucDictionaryUpdate()
        {
            InitializeComponent();
        }

        private void ucDictionaryUpdate_Load(object sender, EventArgs e)
        {
            this.cmbCodeType.AddItems(new FS.HISFC.BizLogic.Manager.Constant().GetAllList(Common.Constants.GZSI_CODELIST));
        }

        #region 菜单

        FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolbarService.AddToolButton("下载字典", "下载医保字典表", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D导入, true, false, null);
            toolbarService.AddToolButton("导入字典", "更新本地字典表", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
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
                case "下载字典":
                    this.DownLoadDictionary();
                    break;
                case "导入字典":
                    this.UpdateDictionary();
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

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
            if (al == null || al.Count == 0)
            {
                this.fpSpread1_Sheet1.RowCount = 0;
                return;
            }
            List<Models.CodeInfo> ls = new List<API.GZSI.Models.CodeInfo>();
            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Base.Const cst = al[i] as FS.HISFC.Models.Base.Const;
                if (cst == null)
                    continue;
                Models.CodeInfo code = new API.GZSI.Models.CodeInfo();
                code.dic_type_code = codeType;
                code.place_dic_val_code = cst.ID;
                code.place_dic_val_name = cst.Name;
                code.prnt_dic_val_code = cst.Memo;
                code.admdvs = cst.UserCode;
                ls.Add(code);
            }

            Common.Function.ShowOutDateToFarpoint<Models.CodeInfo>(this.fpSpread1_Sheet1, ls);
        }
        
        /// <summary>
        /// 下载字典
        /// </summary>
        private void DownLoadDictionary()
        {
            CommonService1901 commonService1901 = new CommonService1901();
            Models.Request.RequestGzsiModel1901 requestGzsiModel1901 = new API.GZSI.Models.Request.RequestGzsiModel1901();
            Models.Response.ResponseGzsiModel1901 responseGzsiModel1901 = new API.GZSI.Models.Response.ResponseGzsiModel1901();
            Models.Request.RequestGzsiModel1901.Data data1901 = new API.GZSI.Models.Request.RequestGzsiModel1901.Data();
            //API.GZSI.Models.Request.RequestGzsiModel1101Data
            data1901.admdvs = "440100";
            requestGzsiModel1901.data = new API.GZSI.Models.Request.RequestGzsiModel1901.Data();
            requestGzsiModel1901.data = data1901;
            returnvalue = commonService1901.CallService(requestGzsiModel1901, ref responseGzsiModel1901, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue == -1)
            {
                MessageBox.Show("下载字典出错：" + commonService1901.ErrorMsg);
                return;
            }

            CommonService9102 commonService9102 = new CommonService9102();
            Models.Request.RequestGzsiModel9102 requestGzsiModel9102 = new API.GZSI.Models.Request.RequestGzsiModel9102();
            Models.Response.ResponseGzsiModel9102 responseGzsiModel9102 = new API.GZSI.Models.Response.ResponseGzsiModel9102();
            Models.Request.RequestGzsiModel9102.FsDownloadIn data9102 = new API.GZSI.Models.Request.RequestGzsiModel9102.FsDownloadIn();
            data9102.file_qury_no = responseGzsiModel1901.output.fileinfo.file_qury_no;
            data9102.filename = responseGzsiModel1901.output.fileinfo.filename;
            requestGzsiModel9102.fsDownloadIn = new API.GZSI.Models.Request.RequestGzsiModel9102.FsDownloadIn();
            requestGzsiModel9102.fsDownloadIn = data9102;
            returnvalue = commonService9102.CallService(requestGzsiModel9102, ref responseGzsiModel9102, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue == -1)
            {
                MessageBox.Show("下载字典出错：" + commonService9102.ErrorMsg);
                return;
            }

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = dialog.SelectedPath + "\\" + data9102.filename;
                System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.OpenOrCreate);
                //fs.Write(responseGzsiModel9102.output, 0, responseGzsiModel9102.output.Length);
                fs.Close();
            }
        }

        /// <summary>
        /// 更新字典
        /// </summary>
        /// <param name="res"></param>
        private void UpdateDictionary()
        {
            if (this.cmbCodeType.SelectedItem == null)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("请选择需要导入的代码类别！", 111);
                return;
            }

            Dictionary<string,List<API.GZSI.Models.CodeInfo>> dictionaryList = new Dictionary<string,List<CodeInfo>>();

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;   //是否允许多选
            dialog.Title = "请选择要处理的文件";  //窗口title
            dialog.Filter = "文本文件(*.txt)|*.*";   //可选择的文件类型
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (System.IO.StreamReader sReader = new System.IO.StreamReader(dialog.FileName, Encoding.UTF8))
                {
                    string aLine = string.Empty;

                    while (true)
                    {
                        aLine = sReader.ReadLine();
                        if (aLine != null)
                        {
                            string[] value = aLine.Split();

                            //不合格的数据
                            if (value.Count() < 7)
                            {
                                continue;
                            }

                            API.GZSI.Models.CodeInfo codeInfo = new CodeInfo();
                            codeInfo.dic_type_code = value[0].ToLower();
                            codeInfo.place_dic_val_code = value[1];
                            codeInfo.place_dic_val_name = value[2];
                            codeInfo.prnt_dic_val_code = value[3];
                            codeInfo.admdvs = value[4];
                            codeInfo.seq = value[5];
                            codeInfo.ver = value[6];

                            if (dictionaryList.Keys.Contains(codeInfo.dic_type_code))
                            {
                                List<API.GZSI.Models.CodeInfo> codeInfoList = dictionaryList[codeInfo.dic_type_code] as List<API.GZSI.Models.CodeInfo>;
                                codeInfoList.Add(codeInfo);
                            }
                            else
                            {
                                List<API.GZSI.Models.CodeInfo> codeInfoList = new List<CodeInfo>();
                                codeInfoList.Add(codeInfo);
                                dictionaryList.Add(codeInfo.dic_type_code, codeInfoList);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            List<API.GZSI.Models.CodeInfo> currentCodeInfoList = new List<CodeInfo>();
            
            if(dictionaryList.Keys.Contains(cmbCodeType.SelectedItem.ID))
            {
                currentCodeInfoList = dictionaryList[cmbCodeType.SelectedItem.ID] as List<API.GZSI.Models.CodeInfo>;
            }

            if (currentCodeInfoList.Count == 0)
            {
                if (MessageBox.Show("导入文件中无该字典信息，点击【是】将会删除系统中该字典现有数据，是否继续！", "提示", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }
            }

            string codeType = Common.Constants.GZSI_CODE_PREFIX + this.cmbCodeType.SelectedItem.ID;
            ArrayList alConstList = new ArrayList();
            if (!this.cbAddSave.Checked)
            {
                // 非增量下载时清空本地字典
                if (constMgr.DelConstant(codeType) == -1)
                {
                    FS.FrameWork.WinForms.Classes.Function.Msg("保存码表到本地失败！" + constMgr.Err, 111);
                    return;
                }
            }
            else
            {
                // 增量下载时获取本地字典
                alConstList = constMgr.GetAllList(codeType);
            }

            int nSaveCount = 0;

            foreach (API.GZSI.Models.CodeInfo codeInfo in currentCodeInfoList)
            {
                if (IsSave(alConstList, codeInfo))
                {
                    FS.HISFC.Models.Base.Const cst = new FS.HISFC.Models.Base.Const();
                    cst.ID = codeInfo.place_dic_val_code;
                    cst.Name = codeInfo.place_dic_val_name;
                    cst.Memo = codeInfo.prnt_dic_val_code;
                    cst.UserCode = codeInfo.admdvs;
                    cst.IsValid = true;
                    if (constMgr.InsertItem(codeType, cst) == -1)
                    {
                        FS.FrameWork.WinForms.Classes.Function.Msg("保存码表到本地失败！" + constMgr.Err, 111);
                        return;
                    }

                    nSaveCount++;
                }
            }

            LoadDictionary();

            FS.FrameWork.WinForms.Classes.Function.Msg("下载码表成功！新增" + nSaveCount.ToString() + "条", 111);

        }

        /// <summary>
        /// 查询字典
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.LoadDictionary();
            return base.Query(sender, neuObject);
        }

        /// <summary>
        /// 是否保存本地
        /// </summary>
        /// <param name="al">本地字典</param>
        /// <param name="item">下载字典项</param>
        /// <returns>ture保存;false不保存</returns>
        private bool IsSave(ArrayList al, API.GZSI.Models.CodeInfo codeInfo)
        {
            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Base.Const c = al[i] as FS.HISFC.Models.Base.Const;
                if (c.ID == codeInfo.place_dic_val_code)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
