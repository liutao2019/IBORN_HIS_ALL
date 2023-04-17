using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Registration.NewRegister
{
    public partial class FrmTag : Form
    {
        public FrmTag()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 套餐编号
        /// </summary>
        private string packageNames;

        public string PackageNames
        {
            get { return packageNames; }
            set
            {
                packageNames = value;
                string[] arr = packageNames.Split(',');
                if (arr != null && arr.Length > 0)
                {
                   // int i = 0;
                    int s = 3;

                    int x = 0;
                    int y = 0;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (x > s) 
                        { 
                            x = 0;
                            y += 1;
                        }
                        PictureBox px1 = new PictureBox();
                        px1.Location = new System.Drawing.Point(7 + 41 * x, 4+34*y);
                        //this.px1.Name = "px1";
                        px1.Size = new System.Drawing.Size(39, 34);
                        px1.TabIndex = 0;
                        px1.TabStop = false;
                        px1.Image = imageList1.Images[i];
                        px1.Tag = arr[i];
                        plTB.Controls.Add(px1);
                        x++;
                    }
                   // plTB.Height = y * 34 + 8;
                    this.Size = new Size(this.Size.Width, (y+1) * 34 + 10);
                    plTB.Dock = DockStyle.Fill;
                }

            }
        }

    }
}
