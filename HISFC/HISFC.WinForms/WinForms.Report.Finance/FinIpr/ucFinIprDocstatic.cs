using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Report.Finance.FinIpr
{
    public partial class ucFinIprDocstatic :FSDataWindow.Controls.ucQueryBaseForDataWindow                                             
    {
        public ucFinIprDocstatic()
        {
            InitializeComponent();
        }

        string doctCode = "ALL";


        #region ��ʼ��
        protected override void OnLoad()
        {       
            
            FS.HISFC.BizProcess.Integrate.Manager doctMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            ArrayList al = new ArrayList();

            //ҽ��
            al = doctMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
  
            FS.HISFC.Models.Base.Employee top = new FS.HISFC.Models.Base.Employee();
            top.ID = "ALL";
            top.Name = "ȫ  ��";
            top.SpellCode = "QB";
            top.WBCode = "WU";
            this.cmbDoc.Items.Add(top);

            foreach (FS.HISFC.Models.Base.Employee con in al)
            {
                cmbDoc.Items.Add(con);
            }
            this.cmbDoc.alItems.Add(top);
            this.cmbDoc.alItems.AddRange(al);

            if (cmbDoc.Items.Count > 0)
            {
                cmbDoc.SelectedIndex = 0;
                doctCode = ((FS.HISFC.Models.Base.Employee)cmbDoc.Items[0]).ID;
   
            }
            base.OnLoad();
        }
        #endregion

        #region ����
        protected override int OnRetrieve(params object[] objects)
        {
          

            if (base.GetQueryTime() == -1)
            {
                return -1;
            }

            if (cmbDoc.Items.Count > 0)
            {
                doctCode = ((FS.HISFC.Models.Base.Employee)cmbDoc.Items[cmbDoc.SelectedIndex]).ID;
               
            }

            if (this.tcDocstatic.SelectedTab.Text == "סԺ�Ƽ�ҽ������")
            {
                this.MainDWDataObject = "d_fin_ipr_docstatic";
                dwcDocstatic1.DataWindowObject = "d_fin_ipr_docstatic";
                this.MainDWLabrary = "Report\\finipb.pbd;Report\\finipb.pbl";
                return this.dwcDocstatic1.Retrieve(this.beginTime, this.endTime,doctCode);

            }
            if (this.tcDocstatic.SelectedTab.Text == "סԺ�Ƽ�ҽ����ϸ����")
            {
                this.mainDWDataObject = "d_fin_ipr_docinfo";
                dwMain.DataWindowObject = "d_fin_ipr_docinfo";

                string filterString = "ALL";
                if (this.cmbDoc.Text != "ȫ  ��")
                { 
                    filterString = this.cmbDoc.Tag.ToString();
                }
                return this.dwcDocstatic2.Retrieve(this.beginTime, this.endTime,doctCode);
            }

            return 1;

        }
        #endregion 
    }
}



 