using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Neusoft.HISFC.Components.Order.Controls
{
    /// <summary>
    /// סԺҽ������ӡ
    /// </summary>
    public partial class ucOrderPrint : Neusoft.FrameWork.WinForms.Controls.ucBaseControl, Neusoft.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucOrderPrint()
        {
            InitializeComponent();
        }

        #region ����

        Neusoft.HISFC.Models.RADT.PatientInfo myPatientInfo = null;

        Neusoft.HISFC.BizProcess.Interface.IPrintOrder ip = null;//��ǰ�ӿ�

        #endregion

        #region ����

        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        protected Neusoft.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get
            {
                if (this.myPatientInfo == null)
                    this.myPatientInfo = new Neusoft.HISFC.Models.RADT.PatientInfo();
                return this.myPatientInfo;
            }
            set
            {
                this.myPatientInfo = value;
            }
        }
        #endregion

        #region ����

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.tv.CheckBoxes = false;

            this.myPatientInfo = neuObject as Neusoft.HISFC.Models.RADT.PatientInfo;
            if (myPatientInfo != null)
            {

                Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯִ�е���Ϣ...");

                if (this.Controls[0].Controls.Count == 0)
                {
                    object o = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Order.Controls.ucOrderPrint), typeof(Neusoft.HISFC.BizProcess.Interface.IPrintOrder));
                    if (o == null)
                    {
                        Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("��ά��HISFC.Components.Order.Controls.ucOrderPrint����ӿ�Neusoft.HISFC.BizProcess.Integrate.IPrintOrder��ʵ�����գ�");
                        return -1;
                    }
                    ip = o as Neusoft.HISFC.BizProcess.Interface.IPrintOrder;
                    Control c = ip as Control;
                    c.Dock = DockStyle.Fill;
                    this.Controls[0].Controls.Add(c);
                }
                else
                {
                    ip = this.Controls[0].Controls[0] as Neusoft.HISFC.BizProcess.Interface.IPrintOrder;
                }

                if (ip == null)
                {
                    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("ά����ʵ�����߱�Neusoft.HISFC.BizProcess.Integrate.IPrintOrder�ӿ�");
                    return -1;
                }

                ip.SetPatient(this.myPatientInfo);

                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            return base.OnSetValue(neuObject, e);
        }

        public override int Print(object sender, object neuObject)
        {
            ip.Print();
            return base.Print(sender, neuObject);
        }

        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] type = new Type[1];
                type[0] = typeof(Neusoft.HISFC.BizProcess.Interface.IPrintOrder);
                return type;
            }
        }

        #endregion
    }
}
