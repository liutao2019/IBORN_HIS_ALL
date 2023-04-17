using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.Report.Finance.FinOpb
{
    public partial class ucFinOpbDocWork : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinOpbDocWork()
        {
            InitializeComponent();
           
        }

        ArrayList alDoc = new ArrayList();
        private FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();

        #region 初始化
        protected override void OnLoad(EventArgs e)
        {
            InitDoc();
            base.OnLoad(e);
        }
        #endregion

        #region 初始化医生
        private void InitDoc()
        {
            
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "ALL";
            obj.Name = "全部";
            alDoc.Add(obj);

            ArrayList doc = manager.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            alDoc.AddRange(doc);
            this.cbDoc.AddItems(alDoc);
            this.cbDoc.SelectedIndex = 0;
            return;
        }

        #endregion

        #region 查询
        protected override int OnRetrieve(params object[] objects)
        {
            FS.HISFC.Models.Base.Employee employee = null;
            employee = (FS.HISFC.Models.Base.Employee)this.dataBaseManager.Operator;

            if (base.GetQueryTime() == -1)
            {
                return -1;
            }


            return base.OnRetrieve(base.beginTime, base.endTime, employee.Dept.ID.ToString());
        }

        #endregion

        #region 过滤
        private void cbDoc_SelectIndexChanged(object sender, EventArgs e)
        {
            string docName;

            if (cbDoc.SelectedIndex > -1)
            {
                docName = ((FS.FrameWork.Models.NeuObject)alDoc[cbDoc.SelectedIndex]).Name.ToString();
                DataView dv = this.dwMain.Dv;
                if (dv == null)
                {
                    return;
                }
                //this.dwMain.SetFilter("");
                //this.dwMain.Filter();
                dv.RowFilter = "";
                if (docName != "全部")
                {
               
                    //this.dwMain.SetFilter("开单医生 = '" + docName + "'");
                    //this.dwMain.Filter();
                    try
                    {
                        dv.RowFilter = "开单医生 = '" + docName + "'";
                    }
                    catch
                    {
                        MessageBox.Show("请输入正确信息，不许输入特殊字符");
                        return;
                    }
                }

            }
        }

        #endregion 

    }
}