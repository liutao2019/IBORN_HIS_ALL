using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.SOC.Local.Order.OutPatientOrder.RecipePrint.FoSi
{
    /// <summary>
    /// 打印之前的处方预览，可以用于处方保存前的核对
    /// </summary>
    public partial class ucRecipePrintView : UserControl
    {
        public ucRecipePrintView()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                this.btConfirm.Click += new EventHandler(btConfirm_Click);
                btCancel.Click += new EventHandler(btCancel_Click);
                btPrint.Click += new EventHandler(btPrint_Click);
            }
        }

        /// <summary>
        /// 确认标记 0 取消；1 确认；2 打印
        /// </summary>
        int confirmCode = 1;

        /// <summary>
        /// 确认标记 0 取消；1 确认；2 打印
        /// </summary>
        public int ConfirmCode
        {
            get
            {
                return confirmCode;
            }
            set
            {
                confirmCode = value;
            }
        }

        void btPrint_Click(object sender, EventArgs e)
        {
            foreach (Control c in pnMain.Controls)
            {
                if (c.GetType() == typeof(ucRecipePrint))
                {
                    ((ucRecipePrint)c).PrintRecipe();
                }
            }

            confirmCode = 2;
            return;
        }

        void btCancel_Click(object sender, EventArgs e)
        {
            confirmCode = 0;
            return;
        }

        void btConfirm_Click(object sender, EventArgs e)
        {
            confirmCode = 1;
            return;
        }

        public int SetPrintInfo(ArrayList alRecipe)
        {
            if (alRecipe == null || alRecipe.Count == 0)
            {
                return 1;
            }

            ArrayList alTemp = new ArrayList();

            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in alRecipe)
            {
                if (order.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                {
                    alTemp.Add(order);
                }
            }
            alRecipe = alTemp;
            if (alRecipe.Count == 0)
            {
                return 1;
            }

            ucRecipePrint iRecipePrint = null;

            this.pnMain.Controls.Clear();

            iRecipePrint = new ucRecipePrint();
            Neusoft.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new Neusoft.HISFC.BizProcess.Integrate.Registration.Registration();

            Neusoft.HISFC.Models.Registration.Register regObj = regMgr.GetByClinic(((Neusoft.HISFC.Models.Order.OutPatient.Order)alRecipe[0]).Patient.ID);
            iRecipePrint.SetPatientInfo(regObj);
            iRecipePrint.MakaLabel(alRecipe);

            pnMain.Controls.Add(iRecipePrint);

            Neusoft.FrameWork.WinForms.Classes.Function.PopShowControl(this);
            return 1;
        }
    }
}
