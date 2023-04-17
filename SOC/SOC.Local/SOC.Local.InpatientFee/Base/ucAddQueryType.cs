using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.InpatientFee.Base
{
    public partial class ucAddQueryType : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucAddQueryType()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 项目查询类型维护管理类
        /// </summary>
        private FS.HISFC.BizLogic.Fee.FeeReport queryManager = new FS.HISFC.BizLogic.Fee.FeeReport();

        /// <summary>
        /// 查询类型帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper queryHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constmanager = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 窗口返回结果
        /// </summary>
        private bool diaglogResult = true;

        /// <summary>
        /// 窗口返回结果
        /// </summary>
        public bool DiaglogResult
        {
            get
            {
                return diaglogResult;
            }
            set
            {
                diaglogResult = value;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        private int Init()
        {
            ///加载查询类别常数
            ArrayList queryType = new ArrayList();
            queryType = this.constmanager.GetAllList("ITEMQUERY");
            if (queryType == null || queryType.Count <= 0)
            {
                return -1;
            }
            this.queryHelper.ArrayObject = queryType;

            return 1;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            FS.HISFC.BizLogic.Manager.Spell spellManager = new FS.HISFC.BizLogic.Manager.Spell();

            if (this.queryHelper.GetObjectFromName(this.txtName.Text) != null)
            {
                MessageBox.Show("名称重复!");
                this.txtName.Focus();
                return;
            }

            //int sortid = this.queryHelper.ArrayObject.Count;
            //string queryType = this.txtListType.Text.Trim();
            obj.ID = this.queryHelper.ArrayObject.Count.ToString();//sortid
            obj.Name = this.txtName.Text.Trim();//查询类型名称
            obj.Memo = this.txtMemo.Text.Trim();//备注
            obj.User01 = FS.FrameWork.Public.String.GetSpell(obj.Name);//拼音码
            FS.HISFC.Models.Base.ISpell spCode = spellManager.Get(obj.Name);
            obj.User02 = spCode.WBCode;//五笔码
            obj.User03 = this.txtHelpCode.Text.Trim();//助记码

            if (obj.Name == "")
            {
                MessageBox.Show("查询类型名称为空，请填写查询类型名称！");
                return;
            }

            if (this.queryManager.insertConstSub("ITEMQUERY", obj) == -1)
            {
                MessageBox.Show("维护查询类型出错:" + this.queryManager.Err);
                return;
            }

            MessageBox.Show("新增查询类型成功！");

            this.diaglogResult = true;
            this.FindForm().Close();
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.diaglogResult = false;
            this.FindForm().Close();
        }

        /// <summary>
        /// 自动生成拼音码，五笔码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            FS.HISFC.BizLogic.Manager.Spell spellManager = new FS.HISFC.BizLogic.Manager.Spell();
            this.txtSpellCode.Text = FS.FrameWork.Public.String.GetSpell(this.txtName.Text);//拼音码
            FS.HISFC.Models.Base.ISpell spCode = spellManager.Get(this.txtName.Text);
            this.txtWBCode.Text = spCode.WBCode;//五笔码
        }

        private void ucAddQueryType_Load(object sender, EventArgs e)
        {
            this.Init();
        }
    }
}
