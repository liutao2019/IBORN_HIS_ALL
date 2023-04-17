using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Nurse.ZhuHai.ZDWY
{
    /// <summary>
    /// 注射患者列表
    /// </summary>
    public partial class tvInjectPatientList : FS.FrameWork.WinForms.Controls.NeuTreeView
    {
        public tvInjectPatientList()
        {
            InitializeComponent();
        }

        public tvInjectPatientList(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();

        public int Refresh(DateTime dtBegin, DateTime dtEnd)
        {
            string whereSQL = @"where fin_opr_register.clinic_code in (
                                    select m.clinic_code
                                      from fin_opb_feedetail m
                                     where m.pay_flag = '1'
                                       and m.cancel_flag = '1'
                                       and m.FEE_DATE >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
                                       and m.FEE_DATE < to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                                       and m.inject_number>confirm_inject
                                       and m.inject_number>0)
                                       order by fin_opr_register.reg_date";
            whereSQL = string.Format(whereSQL, dtBegin.ToString(), dtEnd.ToString());
            ArrayList alInjectReg = regMgr.QueryRegListBase(whereSQL);

            this.Nodes.Clear();

            TreeNode rootNode = new TreeNode("患者列表(" + alInjectReg.Count + ")");

            this.Nodes.Add(rootNode);

            foreach (FS.HISFC.Models.Registration.Register regObj in alInjectReg)
            {
                TreeNode node = new TreeNode();

                node.Text = regObj.Name + " [" + regObj.DoctorInfo.SeeDate + "]";
                switch (regObj.Sex.ID.ToString())
                {
                    case "F":
                        node.ImageIndex = 6;	//成年女
                        break;
                    case "M":
                        node.ImageIndex = 5;	//成年男
                        break;
                    default:
                        node.ImageIndex = 5;
                        break;
                }
                node.Tag = regObj;

                rootNode.Nodes.Add(node);
            }

            rootNode.ExpandAll();

            return 1;
        }
    }
}
