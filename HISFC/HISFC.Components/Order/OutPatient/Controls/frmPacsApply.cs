using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    public partial class frmPacsApply : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmPacsApply()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// 检查目的1
        /// </summary>
        public Dictionary<string, string> PurPoses = new Dictionary<string,string>();

        /// <summary>
        /// 医嘱扩展信息管理{97B9173B-834D-49a1-936D-E4D04F98E4BA}
        /// </summary>
        FS.HISFC.BizLogic.Order.OutPatient.OrderExtend orderExtMgr = new FS.HISFC.BizLogic.Order.OutPatient.OrderExtend();

        /// <summary>
        /// 设置显示信息
        /// </summary>
        /// <param name="DicPacsExetDepts"></param>
        public void setInfo(string PatientID, Dictionary<string, string> DicPacsExetDepts, ArrayList allOrder)
        {

            try
            {
                List<string> Keys = new List<string>();

                foreach (var key in DicPacsExetDepts.Keys)
                {
                    Keys.Add(key);
                }

                #region 一个项目
                if (DicPacsExetDepts.Count == 1)
                {
                    this.Height = 180;

                    this.lblDept1.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(Keys[0]);

                    string strItem = string.Empty;
                    string message1 = string.Empty;
                    for (int i = 0; i < allOrder.Count; i++)
                    {
                        FS.HISFC.Models.Order.OutPatient.Order Order = allOrder[i] as FS.HISFC.Models.Order.OutPatient.Order;
                        if (Order.ExeDept.Name == this.lblDept1.Text && Order.Item.SysClass.ID.ToString() == "UC")
                        {
                            strItem += Order.Item.Name + "、";

                            if (string.IsNullOrEmpty(this.rchText1.Text))
                            {
                                FS.HISFC.Models.Order.OutPatient.OrderExtend orderExtObj = orderExtMgr.QueryByClinicCodOrderID(PatientID, Order.ID);

                                if (orderExtObj != null)
                                {
                                    if (!string.IsNullOrEmpty(orderExtObj.Extend1))
                                    {
                                        this.rchText1.Text = orderExtObj.Extend1;
                                    }
                                }
                            }
                        }
                    }
                    this.lblItem1.Text = strItem.Substring(0, strItem.Length - 1);

                }
                #endregion 
                #region 两个项目
                else if (DicPacsExetDepts.Keys.Count == 2)
                {
                    this.Height = 290;

                    this.lblDept1.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(Keys[0]);

                    string strItem1 = string.Empty;
                    for (int i = 0; i < allOrder.Count; i++)
                    {
                        FS.HISFC.Models.Order.OutPatient.Order Order = allOrder[i] as FS.HISFC.Models.Order.OutPatient.Order;

                        if (Order.ExeDept.Name == this.lblDept1.Text && Order.Item.SysClass.ID.ToString() == "UC")
                        {
                            strItem1 += Order.Item.Name + "、";

                            if (string.IsNullOrEmpty(this.rchText1.Text))
                            {
                                FS.HISFC.Models.Order.OutPatient.OrderExtend orderExtObj = orderExtMgr.QueryByClinicCodOrderID(PatientID, Order.ID);

                                if (orderExtObj != null)
                                {
                                    if (!string.IsNullOrEmpty(orderExtObj.Extend1))
                                    {
                                        this.rchText1.Text = orderExtObj.Extend1;
                                    }
                                }
                            }
                        }

                        
                    }
                    this.lblItem1.Text = strItem1.Substring(0, strItem1.Length - 1);

                    this.lblDept2.Visible = true;
                    this.lblItem2.Visible = true;
                    this.rchText2.Visible = true;
                    this.lblDept2.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(Keys[1]);
                    string strItem2 = string.Empty;
                    for (int i = 0; i < allOrder.Count; i++)
                    {
                        FS.HISFC.Models.Order.OutPatient.Order Order = allOrder[i] as FS.HISFC.Models.Order.OutPatient.Order;
                        if (Order.ExeDept.Name == this.lblDept2.Text && Order.Item.SysClass.ID.ToString() == "UC")
                        {
                            strItem2 += Order.Item.Name + "、";

                            if (string.IsNullOrEmpty(this.rchText2.Text))
                            {
                                FS.HISFC.Models.Order.OutPatient.OrderExtend orderExtObj = orderExtMgr.QueryByClinicCodOrderID(PatientID, Order.ID);

                                if (orderExtObj != null)
                                {
                                    if (!string.IsNullOrEmpty(orderExtObj.Extend1))
                                    {
                                        this.rchText2.Text = orderExtObj.Extend1;
                                    }
                                }
                            }
                        }                    
                    }
                    this.lblItem2.Text = strItem2.Substring(0, strItem2.Length - 1);
                }
                #endregion
                #region 三个项目
                else if (DicPacsExetDepts.Keys.Count == 3)
                {
                    this.Height = 400;

                    this.lblDept1.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(Keys[0]);

                    string strItem1 = string.Empty;
                    for (int i = 0; i < allOrder.Count; i++)
                    {
                        FS.HISFC.Models.Order.OutPatient.Order Order = allOrder[i] as FS.HISFC.Models.Order.OutPatient.Order;

                        if (Order.ExeDept.Name == this.lblDept1.Text && Order.Item.SysClass.ID.ToString() == "UC")
                        {
                            strItem1 += Order.Item.Name + "、";

                            if (string.IsNullOrEmpty(this.rchText1.Text))
                            {
                                FS.HISFC.Models.Order.OutPatient.OrderExtend orderExtObj = orderExtMgr.QueryByClinicCodOrderID(PatientID, Order.ID);
                                if (orderExtObj != null)
                                {
                                    if (!string.IsNullOrEmpty(orderExtObj.Extend1))
                                    {
                                        this.rchText1.Text = orderExtObj.Extend1;
                                    }
                                }
                            }
                        }

                        
                    }
                    this.lblItem1.Text = strItem1.Substring(0, strItem1.Length - 1);

                    this.lblDept2.Visible = true;
                    this.lblItem2.Visible = true;
                    this.rchText2.Visible = true;

                    this.lblDept2.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(Keys[1]);
                    string strItem2 = string.Empty;
                    for (int i = 0; i < allOrder.Count; i++)
                    {
                        FS.HISFC.Models.Order.OutPatient.Order Order = allOrder[i] as FS.HISFC.Models.Order.OutPatient.Order;

                         if (Order.ExeDept.Name == this.lblDept2.Text && Order.Item.SysClass.ID.ToString() == "UC")
                         {
                             strItem2 += Order.Item.Name + "、";

                             if (string.IsNullOrEmpty(this.rchText2.Text))
                            {
                                FS.HISFC.Models.Order.OutPatient.OrderExtend orderExtObj = orderExtMgr.QueryByClinicCodOrderID(PatientID, Order.ID);

                                if (orderExtObj != null)
                                {
                                    if (!string.IsNullOrEmpty(orderExtObj.Extend1))
                                    {
                                        this.rchText2.Text = orderExtObj.Extend1;
                                    }
                                }
                            }
                         }
                       
                    }
                    this.lblItem2.Text = strItem2.Substring(0, strItem2.Length - 1);

                    this.lblDept3.Visible = true;
                    this.lblItem3.Visible = true;
                    this.rchText3.Visible = true;

                    this.lblDept3.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(Keys[2]);
                    string strItem3 = string.Empty;
                    for (int i = 0; i < allOrder.Count; i++)
                    {
                        FS.HISFC.Models.Order.OutPatient.Order Order = allOrder[i] as FS.HISFC.Models.Order.OutPatient.Order;

                        if (Order.ExeDept.Name == this.lblDept3.Text && Order.Item.SysClass.ID.ToString() == "UC")
                        {
                            strItem3 += Order.Item.Name + "、";

                            if (string.IsNullOrEmpty(this.rchText3.Text))
                            {
                                FS.HISFC.Models.Order.OutPatient.OrderExtend orderExtObj = orderExtMgr.QueryByClinicCodOrderID(PatientID, Order.ID);

                                if (orderExtObj != null)
                                {
                                    if (!string.IsNullOrEmpty(orderExtObj.Extend1))
                                    {
                                        this.rchText3.Text = orderExtObj.Extend1;
                                    }
                                }
                            }
                        }          
                    }
                    this.lblItem3.Text = strItem3.Substring(0, strItem3.Length - 1);
                }

                #endregion

                #region 四个项目
                else if (DicPacsExetDepts.Keys.Count == 4)
                {
                    this.Height = 500;

                    this.lblDept1.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(Keys[0]);

                    string strItem1 = string.Empty;
                    for (int i = 0; i < allOrder.Count; i++)
                    {
                        FS.HISFC.Models.Order.OutPatient.Order Order = allOrder[i] as FS.HISFC.Models.Order.OutPatient.Order;

                        if (Order.ExeDept.Name == this.lblDept1.Text && Order.Item.SysClass.ID.ToString() == "UC")
                        {
                            strItem1 += Order.Item.Name + "、";

                            if (string.IsNullOrEmpty(this.rchText1.Text))
                            {
                                FS.HISFC.Models.Order.OutPatient.OrderExtend orderExtObj = orderExtMgr.QueryByClinicCodOrderID(PatientID, Order.ID);
                                if (orderExtObj != null)
                                {
                                    if (!string.IsNullOrEmpty(orderExtObj.Extend1))
                                    {
                                        this.rchText1.Text = orderExtObj.Extend1;
                                    }
                                }
                            }
                        }


                    }
                    this.lblItem1.Text = strItem1.Substring(0, strItem1.Length - 1);

                    this.lblDept2.Visible = true;
                    this.lblItem2.Visible = true;
                    this.rchText2.Visible = true;

                    this.lblDept2.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(Keys[1]);
                    string strItem2 = string.Empty;
                    for (int i = 0; i < allOrder.Count; i++)
                    {
                        FS.HISFC.Models.Order.OutPatient.Order Order = allOrder[i] as FS.HISFC.Models.Order.OutPatient.Order;

                        if (Order.ExeDept.Name == this.lblDept2.Text && Order.Item.SysClass.ID.ToString() == "UC")
                        {
                            strItem2 += Order.Item.Name + "、";

                            if (string.IsNullOrEmpty(this.rchText2.Text))
                            {
                                FS.HISFC.Models.Order.OutPatient.OrderExtend orderExtObj = orderExtMgr.QueryByClinicCodOrderID(PatientID, Order.ID);

                                if (orderExtObj != null)
                                {
                                    if (!string.IsNullOrEmpty(orderExtObj.Extend1))
                                    {
                                        this.rchText2.Text = orderExtObj.Extend1;
                                    }
                                }
                            }
                        }

                    }
                    this.lblItem2.Text = strItem2.Substring(0, strItem2.Length - 1);

                    this.lblDept3.Visible = true;
                    this.lblItem3.Visible = true;
                    this.rchText3.Visible = true;

                    this.lblDept3.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(Keys[2]);
                    string strItem3 = string.Empty;
                    for (int i = 0; i < allOrder.Count; i++)
                    {
                        FS.HISFC.Models.Order.OutPatient.Order Order = allOrder[i] as FS.HISFC.Models.Order.OutPatient.Order;

                        if (Order.ExeDept.Name == this.lblDept3.Text && Order.Item.SysClass.ID.ToString() == "UC")
                        {
                            strItem3 += Order.Item.Name + "、";

                            if (string.IsNullOrEmpty(this.rchText3.Text))
                            {
                                FS.HISFC.Models.Order.OutPatient.OrderExtend orderExtObj = orderExtMgr.QueryByClinicCodOrderID(PatientID, Order.ID);

                                if (orderExtObj != null)
                                {
                                    if (!string.IsNullOrEmpty(orderExtObj.Extend1))
                                    {
                                        this.rchText3.Text = orderExtObj.Extend1;
                                    }
                                }
                            }
                        }
                    }
                    this.lblItem3.Text = strItem3.Substring(0, strItem3.Length - 1);

                    this.lblDept4.Visible = true;
                    this.lblItem4.Visible = true;
                    this.rchText4.Visible = true;

                    this.lblDept4.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(Keys[3]);
                    string strItem4 = string.Empty;
                    for (int i = 0; i < allOrder.Count; i++)
                    {
                        FS.HISFC.Models.Order.OutPatient.Order Order = allOrder[i] as FS.HISFC.Models.Order.OutPatient.Order;

                        if (Order.ExeDept.Name == this.lblDept4.Text && Order.Item.SysClass.ID.ToString() == "UC")
                        {
                            strItem4 += Order.Item.Name + "、";

                            if (string.IsNullOrEmpty(this.rchText4.Text))
                            {
                                FS.HISFC.Models.Order.OutPatient.OrderExtend orderExtObj = orderExtMgr.QueryByClinicCodOrderID(PatientID, Order.ID);

                                if (orderExtObj != null)
                                {
                                    if (!string.IsNullOrEmpty(orderExtObj.Extend1))
                                    {
                                        this.rchText4.Text = orderExtObj.Extend1;
                                    }
                                }
                            }
                        }
                    }
                    this.lblItem4.Text = strItem4.Substring(0, strItem4.Length - 1);
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }


        void btSavet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.rchText1.Text))
            {
                MessageBox.Show("保存失败，检查目的不可为空！");
                return;
            }
            if (this.PurPoses.ContainsKey(this.lblDept1.Text))
            {
                this.PurPoses.Remove(this.lblDept1.Text);
            }
            this.PurPoses.Add(this.lblDept1.Text, this.rchText1.Text);
            if (this.rchText2.Visible == true)
            {
                if (string.IsNullOrEmpty(this.rchText2.Text))
                {
                    MessageBox.Show("保存失败，检查目的不可为空！");
                    return;
                }
                if (this.PurPoses.ContainsKey(this.lblDept2.Text))
                {
                    this.PurPoses.Remove(this.lblDept2.Text);
                }
                this.PurPoses.Add(this.lblDept2.Text, this.rchText2.Text);
            }
            if (this.rchText3.Visible == true)
            {
                if (string.IsNullOrEmpty(this.rchText3.Text))
                {
                    MessageBox.Show("保存失败，检查目的不可为空！");
                    return;
                }
                if (this.PurPoses.ContainsKey(this.lblDept3.Text))
                {
                    this.PurPoses.Remove(this.lblDept3.Text);
                }
                this.PurPoses.Add(this.lblDept3.Text, this.rchText3.Text);
            }
            if (this.rchText4.Visible == true)
            {
                if (string.IsNullOrEmpty(this.rchText4.Text))
                {
                    MessageBox.Show("保存失败，检查目的不可为空！");
                    return;
                }
                if (this.PurPoses.ContainsKey(this.lblDept4.Text))
                {
                    this.PurPoses.Remove(this.lblDept4.Text);
                }
                this.PurPoses.Add(this.lblDept4.Text, this.rchText4.Text);
            }
          
            this.Hide();
        }

        private void btSavet_Click(object sender, FormClosedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.rchText1.Text))
            {
                MessageBox.Show("保存失败，检查目的不可为空！");
                return;
            }
            this.PurPoses.Add(this.lblDept1.Text, this.rchText1.Text);
            if (this.rchText2.Visible == true)
            {
                if (string.IsNullOrEmpty(this.rchText2.Text))
                {
                    MessageBox.Show("保存失败，检查目的不可为空！");
                    return;
                }
                this.PurPoses.Add(this.lblDept2.Text, this.rchText2.Text);
            }
            if (this.rchText3.Visible == true)
            {
                if (string.IsNullOrEmpty(this.rchText3.Text))
                {
                    MessageBox.Show("保存失败，检查目的不可为空！");
                    return;
                }
                this.PurPoses.Add(this.lblDept3.Text, this.rchText3.Text);
            }
            if (this.rchText4.Visible == true)
            {
                if (string.IsNullOrEmpty(this.rchText4.Text))
                {
                    MessageBox.Show("保存失败，检查目的不可为空！");
                    return;
                }
                this.PurPoses.Add(this.lblDept4.Text, this.rchText4.Text);
            }

            this.Hide();
        }
    }
}
