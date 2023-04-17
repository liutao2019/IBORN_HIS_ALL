using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Manager.Forms
{
    public partial class ucMaterialItems : Form, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucMaterialItems()
        {
            InitializeComponent();
            InitInterface();
            this.Init();
        }

        FS.HISFC.BizProcess.Integrate.Material.MaterialInterface materialInterface = null;

        DataView dv = null;
        bool isAddMaterial = false;
        Items.ucHandleItems ucHandle = null;
        string FileDress = string.Empty;

        private void InitInterface()
        {
            if (this.materialInterface == null)
            {
                this.materialInterface = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Integrate.Material.MaterialInterface)) as FS.HISFC.BizProcess.Integrate.Material.MaterialInterface;
            }
        }

        private void Init()
        {
            isAddMaterial = false;
            if (this.materialInterface != null)
            {
                FileDress = FS.FrameWork.WinForms.Classes.Function.SettingPath + "MaterialToUndrug.xml";
                DataSet dt = this.materialInterface.MaterialBaseInfo();
                if (dt != null && dt.Tables[0].Rows.Count != 0)
                {
                    dv = new DataView(dt.Tables[0]);
                    this.neuSpread1_Sheet1.DataSource = dv;
                }
                if (System.IO.File.Exists(FileDress))
                {
                    FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuSpread1_Sheet1,FileDress);
                }
            }
        }        

        private void btOK_Click(object sender, EventArgs e)
        {            
            FS.HISFC.Models.Fee.Item.Undrug item = new FS.HISFC.Models.Fee.Item.Undrug();
            int index = this.neuSpread1_Sheet1.ActiveRowIndex;
            item.Name = this.neuSpread1_Sheet1.Cells[index, 3].Text;//名称
            item.Price = FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[index, 17].Text);//默认价
            item.SpecialPrice = item.Price;//特诊价
            item.ChildPrice = item.Price;//儿童价
            item.PriceUnit = this.neuSpread1_Sheet1.Cells[index, 18].Text;//单位
            item.SpellCode = this.neuSpread1_Sheet1.Cells[index, 4].Text;//拼音码
            item.WBCode = this.neuSpread1_Sheet1.Cells[index, 5].Text;//五笔码

            ucHandle = new Items.ucHandleItems(true);
            ucHandle.UnDrugItem = item;
            this.isAddMaterial = true;
            this.Close();
            
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] t = new Type[1];
                t[0] = typeof(FS.HISFC.BizProcess.Integrate.Material.MaterialInterface);
                return t;
            }
        }

        #endregion

        private void ucMaterialItems_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isAddMaterial)
            {
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucHandle);
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string text=this.txtFilter.Text.Trim().ToUpper();
            string filter = "拼音码 like '%" + text + "%'" + " OR " + "五笔码 like '%" + text + "%'";
            this.dv.RowFilter = filter;
            if (System.IO.File.Exists(FileDress))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuSpread1_Sheet1, FileDress);
            }
        }
    }
}
