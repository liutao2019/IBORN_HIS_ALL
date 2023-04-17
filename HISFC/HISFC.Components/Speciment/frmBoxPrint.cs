using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using System.Collections;

namespace FS.HISFC.Components.Speciment
{
    public partial class frmBoxPrint : Form
    {
        //记录打印的标本盒个数
        private int count = 1;
        //要打印的标本盒列表
        private List<SpecBox> boxList = new List<SpecBox>();


        public List<SpecBox> BoxList
        {
            set
            {
                boxList = value;
            }
        }
        public frmBoxPrint()
        {
            InitializeComponent();
        }

        private string GetLocation(SpecBox box)
        {
            IceBoxManage iceBoxManage = new IceBoxManage(); 

            SpecTypeManage specTypeMange = new SpecTypeManage();
            DisTypeManage disTypeMange = new DisTypeManage();

            string boxId = Convert.ToInt32(box.BoxBarCode.Substring(2, 3)).ToString();
            IceBox iceBox = iceBoxManage.GetIceBoxById(boxId);
            if (iceBox == null || iceBox.IceBoxId <= 0)
            {
                return "";
            }
            DiseaseType disType = disTypeMange.GetDisTypeByBoxId(box.BoxId.ToString());
            if (disType == null || disType.DisTypeID <= 0)
            {
                return "";
            }
            FS.HISFC.Models.Speciment.SpecType specType = specTypeMange.GetSpecTypeByBoxId(box.BoxId.ToString());

            if (specType == null || specType.SpecTypeID <= 0)
            {
                return "";
            }
            string loc1 = box.BoxBarCode.Replace("C", "层").Replace("J", "架");
            string loc2 = iceBox.IceBoxName + loc1.Substring(5) + "-" + disType.DiseaseName + "-" + specType.SpecTypeName;
            return loc2; 
        }

        /// <summary>
        /// 获取标本盒中的第一个标本的序列号
        /// </summary>
        /// <param name="boxId"></param>
        /// <returns></returns>
        private string GetFirstSpecNumInBox(string boxId, ref DateTime dt)
        {
            SubSpec subSpec = new SubSpec();
            SubSpecManage specManage = new SubSpecManage();
            ArrayList arrSpec = specManage.GetSubSpecInOneBox(boxId);
            if (arrSpec == null || arrSpec.Count <= 0)
            {
                return "";
            }
            subSpec = arrSpec[0] as SubSpec;
             if (subSpec != null && subSpec.SpecId>0 && subSpec.SubBarCode!="")
            {
                //G000030P1
                if (subSpec.SubBarCode.Length == 9)
                {
                    return subSpec.SubBarCode.Substring(1, 6);
                }
                if (subSpec.SubBarCode.Length >= 11)
                {
                    return subSpec.SubBarCode.Substring(0, 6);
                }
                dt = subSpec.StoreTime;
            }
            return "";
        }

        /// <summary>
        /// 获取标本盒条形码
        /// </summary>
        /// <param name="barCode"></param>
        public void GetBarCode(SpecBox box)
        {
            try
            {
                if (count > 4)
                {
                    MessageBox.Show("最多只能打印4个标本盒的条码");
                    return;
                }
                string loc = GetLocation(box);
                if (loc == "")
                    return;
                string endMsg = PrintLabel.Generate2DBarCode(box.BoxBarCode);
                FlowLayoutPanel flpAdd = new FlowLayoutPanel();
                flpAdd.Size = flp1.Size;
                if (count == 1)
                {
                    flpAdd = flp1;
                }
                if (count > 1 && count <= 4)
                {
                    flp.Controls.Add(flpAdd);
                }
                DateTime dt = DateTime.Now;
                string seq = this.GetFirstSpecNumInBox(box.BoxId.ToString(), ref dt);
                if (seq == "")
                {
                    return;
                }
                for (int i = 0; i < 4; i++)
                {
                    Print.ucBoxLabel ucLabel = new FS.HISFC.Components.Speciment.Print.ucBoxLabel();
                    ucLabel.SetBarCode(loc, box.BoxBarCode, endMsg, seq, dt);
                    AddControl(ucLabel, "", flpAdd);
                }

                Button btn = new Button();
                Size size = new Size(75, 27);
                btn.Size = size;
                btn.Font = new Font("宋体", 12);
                btn.Text = "删除";
                AddControl(btn, "B", flpAdd);
                count++;
            }
            catch
            {
 
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;            　
            p.PrintPreview(0, 0, flp);
        }

        private void frmBoxPrint_Load(object sender, EventArgs e)
        {
            if (boxList.Count <= 0)
            {
                return;
            }
            foreach (SpecBox box in boxList)
            {
                GetBarCode(box);
            }

        }

        private void Del(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            count--;
            btn.Parent.Dispose();
        }
        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SpecBoxManage boxManage = new SpecBoxManage();
                SpecBox box = boxManage.GetBoxByBarCode(txtBarCode.Text.Trim());
                if (box == null || box.BoxId <= 0)
                {
                    box = boxManage.GetBoxById(txtBarCode.Text.Trim());
                }
                if (box == null || box.BoxId <= 0)
                {
                    MessageBox.Show("找不到标本盒");
                    return;
                }

                GetBarCode(box);                         
            }
        }

        private void AddControl(Control c , string type, FlowLayoutPanel flpControls)
        {
            if (type == "B")
            {
                Button btn = c as Button;
                c.Click += Del;               
            }
            flpControls.Controls.Add(c); 
        }
         
    }
}