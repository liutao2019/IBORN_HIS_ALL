using System;
using System.Windows.Forms;
namespace FS.HISFC.Components.EPR.Query
{
	/// <summary>
	/// Function 的摘要说明。
	/// </summary>
	public  class Function
	{
		protected Function()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		private static frmSelectTree formSelectTree = null;
		/// <summary>
		/// 选择的节点窗口
		/// </summary>
		public static frmSelectTree FormSelectTree
		{
			get
			{
				if(Function.formSelectTree ==null)
					Function.formSelectTree = new frmSelectTree();
				return Function.formSelectTree;
			}
			set
			{
				if(Function.formSelectTree ==null)
					Function.formSelectTree = new frmSelectTree();
				Function.formSelectTree = value;
			}
		}

        public static void ViewEMR(FS.HISFC.Models.RADT.PatientInfo patientInfo,string eprId)
        {
            
            FS.HISFC.Models.File.DataFileInfo dt = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.GetFile(eprId);
            if (dt == null)
            {
                Panel p = new Panel();
                p.Size = new System.Drawing.Size(800, 1000);
                p.Visible = true;
                Common.Classes.Function.EMRShow(p,patientInfo,"0",false);
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(p);
            }
            else
            {

                TemplateDesignerApplication.ucLoader loader = new TemplateDesignerApplication.ucLoader();
                string[] param ={ FS.FrameWork.Management.Connection.Operator.ID, dt.Index1 };
                loader.ISql = Common.Classes.Function.ISql;
                loader.ISql.SetParam(param);
                loader.ISql.RefreshVariant();
                string fileName = "";//TemplateDesignerHost.Function.LoadFileCheckFile(dt, false);
                loader.Init(dt, fileName, param);
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(loader);
            }

        }


        public static void EditEMR(FS.HISFC.Models.RADT.PatientInfo patientInfo, Form sender)
        {
            Form f = sender.MdiParent;
            if (f == null)
            {
                return;
            }
            //Interface.IEMRable myEMR = null;

            //frmPatientListSet form = new frmPatientListSet();
            //myEMR = getInstance(form.GetEmr()) as Interface.IEMRable;
            //myEMR.

            FS.HISFC.Components.EPR.frmEPRMain fEPR = null; // = new frmEPRMain();
            //EPR.frmEPR fEPR = null;
            for (int i = 0; i < f.MdiChildren.Length; i++)
            {
                if (typeof(FS.HISFC.Components.EPR.frmEPRMain) == f.MdiChildren[i].GetType())
                {
                    fEPR = f.MdiChildren[i] as FS.HISFC.Components.EPR.frmEPRMain;
                    break;
                }
            }
            if (fEPR == null)
            {
                fEPR = new FS.HISFC.Components.EPR.frmEPRMain();
                fEPR.MdiParent = f;
                fEPR.Show();
            }
            fEPR.control.SetValue(patientInfo, null);
            fEPR.Activate();

            //fEPR.set(patientInfo);

        }



	}
}
