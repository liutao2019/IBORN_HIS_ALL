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
            #region �鿴��
            if (datafileInfo != null)
            {
                if (lastPatientID != curPatient.ID) //������
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
                        if (MessageBox.Show("�ò���ҳ�����������������ڱ༭���Ƿ�ת�����ڵĻ����ϣ�\r\n���ѡ��\"��\"���������ϱ༭�Ľ���ʧ��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
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
                        MessageBox.Show("�ò���ҳ������\"" + lockObject.Name + "\"�༭����ֻ������ò���ҳ��");
                        loader.ReadOnly = true;
                    }
                }
                else //����
                {

                    if (FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.SetEMRLocked(datafileInfo, curPatient, FS.FrameWork.Management.Connection.Operator, true) == -1)
                    {
                        MessageBox.Show("����ʧ�ܣ�");
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
                        MessageBox.Show("����ʧ�ܣ�");
                        return -1;
                    }
                }
            }
            return 0;
        }

    }
}
