using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
namespace FS.HISFC.Components.EPR
{
    public class Lock
    {

        ArrayList al = new ArrayList();
        private string lastPatientID = "";
        public  void BeforOpen(TemplateDesignerApplication.ucDataFileLoader loader,FS.HISFC.Models.RADT.PatientInfo curPatient,FS.HISFC.Models.File.DataFileInfo datafileInfo)
        {
            #region 查看锁
            if (datafileInfo != null)
            {
                if (lastPatientID != curPatient.ID) //换人了
                {
                    lastPatientID = curPatient.ID;
                    al = new ArrayList();
                }

                //FS.HISFC.Management.EPR.EMR emr = new FS.HISFC.Management.EPR.EMR();
                FS.FrameWork.Models.NeuObject lockObject = new FS.FrameWork.Models.NeuObject();
                if (FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.IsEMRLocked(curPatient.ID, datafileInfo.ID, ref lockObject))
                {
                    if (FS.FrameWork.Management.Connection.Operator.ID == lockObject.ID)
                    {
                        if (MessageBox.Show("该病例页您在其他机器上正在编辑，是否转到现在的机器上，\r\n如果选择\"是\"其他机器上编辑的将丢失！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            al.Add(datafileInfo.Clone());
                            loader.ReadOnly = false;
                        }
                        else
                        {
                            loader.ReadOnly = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("该病例页正在由\"" + lockObject.Name + "\"编辑，您只能浏览该病例页！");
                        loader.ReadOnly = true;
                    }
                }
                else //加锁
                {

                    if (FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.SetEMRLocked(datafileInfo, curPatient, FS.FrameWork.Management.Connection.Operator, true) == -1)
                    {
                        MessageBox.Show("加锁失败！");
                        return;
                    }
                    al.Add(datafileInfo.Clone());
                    loader.ReadOnly = false;
                }
            }
            #endregion
           

        }

        public  int UnLock(TemplateDesignerApplication.ucDataFileLoader loader, FS.HISFC.Models.RADT.PatientInfo curPatient)
        {
            //FS.HISFC.Management.EPR.EMR emr = new FS.HISFC.Management.EPR.EMR();
            {
                foreach (FS.HISFC.Models.File.DataFileInfo datafileInfo in al)
                {
                    if (FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.SetEMRLocked(datafileInfo, curPatient, FS.FrameWork.Management.Connection.Operator, false) == -1)
                    {
                        MessageBox.Show("解锁失败！");
                        return -1;
                    }
                }
            }
            return 0;
        }

    }
}
