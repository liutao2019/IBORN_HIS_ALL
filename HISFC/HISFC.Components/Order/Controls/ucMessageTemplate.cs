using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Order.Controls
{
    public partial class ucMessageTemplate : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region 变量{D37652D3-1DB3-4f8c-AFE6-BE21625F082C}
        private DataTable dtTemplate = new DataTable();
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        private DataView dvTemplate = new DataView();
        FS.HISFC.BizLogic.Order.MessageTemplateLogic templatelogic = new FS.HISFC.BizLogic.Order.MessageTemplateLogic();
        #endregion
        public ucMessageTemplate()
        {
            InitializeComponent();
            Init();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.fpSpread1_Sheet1.RowCount > 0)
            {
                this.ShowTemplateSet(e.Row);
            }
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "新增":
                    this.AddTemplate();
                    QueryMsgTemplagte();
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            this.dtTemplate.Reset();
            this.dtTemplate.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("流水号",typeof(string)),
                    new DataColumn("模板类型",typeof(string)),
                    new DataColumn("模板主题",typeof(string)),
                    new DataColumn("模板内容",typeof(string)),
                    new DataColumn("排序号",typeof(string)),
                    new DataColumn("操作人",typeof(string)),
                    new DataColumn("操作时间",typeof(DateTime)),
                    new DataColumn("创建人",typeof(string)),
                    new DataColumn("创建时间",typeof(DateTime)),
                    new DataColumn("是否启用",typeof(string))
                });

            this.dvTemplate = new DataView(this.dtTemplate);
            this.fpSpread1_Sheet1.DataSource = this.dvTemplate;
            this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;

            QueryMsgTemplagte();
        }
        /// <summary>
        /// 设置选中行的数据到界面
        /// </summary>
        /// <param name="row">行数</param>
        private void ShowTemplateSet(int row)
        {
            FS.HISFC.Models.Order.MessageTemplate msg = null ;
           ArrayList almsg = this.templatelogic.QueryeTemplateByid(dtTemplate.Rows[row]["流水号"].ToString());
           if (almsg.Count > 0)
           {
               msg = almsg[0] as FS.HISFC.Models.Order.MessageTemplate;
           }
           // msg = fpSpread1_Sheet1.Rows[row].Tag as FS.HISFC.Models.Order.MessageTemplate;
            if (msg != null)
            {
                FS.HISFC.Components.Order.Forms.frmMessageTemplateSet frm = new FS.HISFC.Components.Order.Forms.frmMessageTemplateSet();
                frm.SetItem(msg);
                frm.ShowDialog();
            }
            QueryMsgTemplagte();
        }

        private void AddTemplate()
        {
            FS.HISFC.Components.Order.Forms.frmMessageTemplateSet frm = new FS.HISFC.Components.Order.Forms.frmMessageTemplateSet();
            frm.ShowDialog();
        }
        #region 变量



        /// <summary>
        /// 查询数据
        /// </summary>
        private int  QueryMsgTemplagte()
        {
            try
            {
                this.fpSpread1_Sheet1.RowCount = 0;
                this.dtTemplate.Clear();
                System.Collections.ArrayList altemplate = new System.Collections.ArrayList();
                altemplate = this.templatelogic.QueryMsgTemplateAll();
                if (altemplate != null && altemplate.Count > 0)
                {
                    this.AddTemplateToFrp(altemplate);
                }
            }
            catch (Exception ex)
            {

                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 添加数据到表格
        /// </summary>
        /// <param name="al"></param>
        private void AddTemplateToFrp(ArrayList al)
        {
           
            foreach (FS.HISFC.Models.Order.MessageTemplate obj in al)
            {
                DataRow row =this.dtTemplate.NewRow();

                   row["流水号"]= obj.MessageTemplateId;
                   row["模板类型"]=obj.MsgTemplateType;
                   row["模板主题"]=obj.MsgTemplateTitle;
                   row["模板内容"]=obj.MsgTemplateContent;
                   row["排序号"]=obj.Sortid;
                   row["操作人"]=obj.Opername;
                   row["操作时间"]=obj.Opertime;
                   row["创建人"]=obj.Createname;
                   row["创建时间"]=obj.Createtime;
                   if (obj.State == "1")
                   {
                       row["是否启用"] = "启用";
                   }
                   else
                   {
                       row["是否启用"] = "作废";
                   }
                this.dtTemplate.Rows.Add(row);
            }
        }

        protected override int OnQuery(object sender, object neuObject)
        {
          return  QueryMsgTemplagte();
            //return base.Query(sender, neuObject);
        }

        public override FS.FrameWork.WinForms.Forms.ToolBarService Init(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("新增", "新增模板", FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);
            return toolBarService;
        }
        #endregion
    }
}
