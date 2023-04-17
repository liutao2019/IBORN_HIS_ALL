using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Models;
using FS.HISFC.Models.Registration;
using System.Collections;

namespace FS.SOC.Local.Registration.SDFY
{
    public partial class IRegPrintDefault : UserControl, FS.HISFC.BizProcess.Interface.Registration.IRegPrint
    {
        public IRegPrintDefault()
        {
            InitializeComponent();
        }

        #region �����
        /// <summary>
        /// ȫ������
        /// </summary>
        private FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction();
        /// <summary>
        /// ������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager manageIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// �Һ�ҵ����
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
        #endregion

        #region ����

        private bool isPreview = false;
        /// <summary>
        /// �Ƿ�Ԥ��
        /// </summary>
        private bool IsPreview
        {
            get { return isPreview; }
            set { isPreview = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public System.Data.IDbTransaction Trans
        {
            get
            {
                return this.trans.Trans;
            }
            set
            {
                this.trans.Trans = value;
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ���ô�ӡֵ
        /// </summary>
        /// <param name="register">�Һ�ʵ��</param>
        /// <returns></returns>
        public int SetPrintValue(FS.HISFC.Models.Registration.Register register)
        {
            this.lblHosptialName.Text = manageIntegrate.GetHospitalName();
            //MessageBox.Show("���¼����ţ�"+register.PID.CardNO);
            try
            {
                //�����
                this.npbBarCode.Image = this.CreateBarCode(register.PID.CardNO);
                //����
                this.lblPatientName.Text = "������" + register.Name;
                //�Һſ���
                this.lblDeptName.Text = "�Һſ��ң�" + register.DoctorInfo.Templet.Dept.Name;
                //�Һ�ҽ��
                this.lblDoctName.Text = "�Һ�ҽ����" + register.DoctorInfo.Templet.Doct.Name;
                //�Ա�
                this.lblSex.Text = "�Ա�" + register.Sex.Name;
                //����
                this.lblAge.Text = "���䣺" + this.regMgr.GetAge(register.Birthday);
                //���֤����
                this.lblIDNo.Text = "���֤�ţ�" + register.IDCard;
                //��������
                this.lblPactName.Text = "�������ࣺ" + register.Pact.Name;
                //���
                this.lblSeeNo.Text = "�ŶӺ��룺" + register.DoctorInfo.SeeNO;
                //�Һ�ʱ��
                this.lblRegDate.Text = "�Һ�ʱ�䣺" + this.regMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch (Exception ex)
            {
                return -1;
            }
            return 0;
        }
        
        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Print print = null;
                try
                {
                    print = new FS.FrameWork.WinForms.Classes.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("��ʼ����ӡ��ʧ��!" + ex.Message);
                    return -1;
                }
                FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize("MZGH", 300, 300);
                print.SetPageSize(ps);
                print.PrintPage(0, 0, this);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public int Clear()
        {
            return 0;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="trans"></param>
        public void SetTrans(System.Data.IDbTransaction trans)
        {
            this.trans.Trans = trans;
        }

        /// <summary>
        /// Ԥ��
        /// </summary>
        /// <returns></returns>
        public int PrintView()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.PrintPreview(0, 0, this);
            return 0;
        }

        /// <summary>
        /// ���������뷽��
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private Image CreateBarCode(string code)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
            BarcodeLib.AlignmentPositions Align = BarcodeLib.AlignmentPositions.CENTER;
            b.IncludeLabel = true;
            b.Alignment = Align;
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, this.npbBarCode.Size.Width, this.npbBarCode.Height);
        }

        #region �����ӡ�ú��������ϣ�
        ///// <summary>
        ///// ����Ϊ��ӡģʽ
        ///// </summary>
        //public void SetToPrintMode()
        //{
        //    //��Ԥ������Ϊ���ɼ�
        //    SetLableVisible(false, lblPreview);
        //    foreach (Label lbl in lblPrint)
        //    {
        //        lbl.BorderStyle = BorderStyle.None;
        //        lbl.BackColor = SystemColors.ControlLightLight;
        //    }
        //}
        ///// <summary>
        ///// ����ΪԤ��ģʽ
        ///// </summary>
        //public void SetToPreviewMode()
        //{
        //    //��Ԥ������Ϊ�ɼ�
        //    SetLableVisible(true, lblPreview);
        //    foreach (Label lbl in lblPrint)
        //    {
        //        lbl.BorderStyle = BorderStyle.None;
        //        lbl.BackColor = SystemColors.ControlLightLight;
        //    }
        //}

        ///// <summary>
        ///// ��ӡ�õı�ǩ����
        ///// </summary>
        //public Collection<Label> lblPrint;
        ///// <summary>
        ///// Ԥ���õı�ǩ����
        ///// </summary>
        //public Collection<Label> lblPreview;

        ///// <summary>
        ///// ��ʼ���վ�
        ///// </summary>
        ///// <remarks>
        ///// �Ѵ�ӡ���Ԥ������ݣ�����ǩ��ֵ���ֿ�
        ///// </remarks>
        //private void InitReceipt(Control control)
        //{
        //    foreach (Control c in control.Controls)
        //    {
        //        if (c.GetType().FullName == "System.Windows.Forms.Label" ||
        //            c.GetType().FullName == "FS.FrameWork.WinForms.Controls.NeuLabel")
        //        {
        //            Label l = (Label)c;
        //            if (l.Tag != null)
        //            {
        //                if (l.Tag.ToString() == "print")
        //                {
        //                    if (!string.IsNullOrEmpty(l.Text) || l.Text == "ӡ")
        //                    {
        //                        l.Text = "";
        //                    }
        //                    lblPrint.Add(l);
        //                }
        //                else
        //                {
        //                    lblPreview.Add(l);
        //                }
        //            }
        //            else
        //            {
        //                lblPreview.Add(l);
        //            }
        //        }
        //    }
        //}


        ///// <summary>
        ///// ��ʼ���վ�
        ///// </summary>
        ///// <remarks>
        ///// �Ѵ�ӡ���Ԥ������ݣ�����ǩ��ֵ���ֿ�
        ///// </remarks>
        //private void InitReceipt()
        //{
        //    lblPreview = new Collection<Label>();
        //    lblPrint = new Collection<Label>();
        //    foreach (Control c in this.Controls)
        //    {
        //        if (c.GetType().FullName == "System.Windows.Forms.Label" ||
        //            c.GetType().FullName == "FS.FrameWork.WinForms.Controls.NeuLabel")
        //        {
        //            Label l = (Label)c;
        //            if (l.Tag != null)
        //            {
        //                if (l.Tag.ToString() == "print")
        //                {
        //                    #region ����ӡ�ֵĴ�ӡ��ֵ���
        //                    if (!string.IsNullOrEmpty(l.Text) && l.Text == "ӡ")
        //                    {
        //                        l.Text = "";
        //                    }
        //                    #endregion
        //                    lblPrint.Add(l);
        //                }
        //                else
        //                {
        //                    lblPreview.Add(l);
        //                }
        //            }
        //            else
        //            {
        //                lblPreview.Add(l);
        //            }
        //        }
        //    }
        //}
        ///// <summary>
        ///// ���ñ�ǩ���ϵĿɼ���
        ///// </summary>
        ///// <param name="v">�Ƿ�ɼ�</param>
        ///// <param name="l">��ǩ����</param>
        //private void SetLableVisible(bool v, Collection<Label> l)
        //{
        //    foreach (Label lbl in l)
        //    {
        //        lbl.Visible = v;
        //    }
        //}


        ///// <summary>
        ///// ���ô�ӡ���ϵ�ֵ
        ///// </summary>
        ///// <param name="t">ֵ����</param>
        ///// <param name="l">��ǩ����</param>
        //private void SetLableText(string[] t, Collection<Label> l)
        //{
        //    foreach (Label lbl in l)
        //    {
        //        lbl.Text = "";
        //    }
        //    if (t != null)
        //    {
        //        if (t.Length <= l.Count)
        //        {
        //            int i = 0;
        //            foreach (string s in t)
        //            {
        //                l[i].Text = s;
        //                i++;
        //            }
        //        }
        //        else
        //        {
        //            if (t.Length > l.Count)
        //            {
        //                int i = 0;
        //                foreach (Label lbl in l)
        //                {
        //                    lbl.Text = t[i];
        //                    i++;
        //                }
        //            }
        //        }
        //    }
        //}
        #endregion

        #endregion
    }
}
