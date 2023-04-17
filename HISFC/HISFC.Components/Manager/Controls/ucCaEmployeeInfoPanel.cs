using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace Neusoft.HISFC.Components.Manager.Controls
{
    /// <summary>
    /// [��������: ��ԱCAǩ����Ϣ]<br></br>
    /// [�� �� ��: ��ѩ��]<br></br>
    /// [����ʱ��: 2014��12��18]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucCaEmployeeInfoPanel : UserControl
    {
        //��Ա������
        Neusoft.HISFC.BizLogic.Manager.Person personMgr = new Neusoft.HISFC.BizLogic.Manager.Person();
        //��Աʵ����
        Neusoft.HISFC.Models.Base.Employee employee = new Neusoft.HISFC.Models.Base.Employee();
        //����������
        Neusoft.HISFC.BizLogic.Manager.Constant consManager = new Neusoft.HISFC.BizLogic.Manager.Constant();

        public ucCaEmployeeInfoPanel()
        {
            InitializeComponent();
        }
        /// <summary>
        /// �вι��캯��
        /// </summary>
        /// <param name="empl"></param>
        public ucCaEmployeeInfoPanel(Neusoft.HISFC.Models.Base.Employee empl)
        {
            InitializeComponent();
            this.employee = empl;
            setInfoToControls();
        }

        /// <summary>
        /// ���ݲ������ͻ��ArrayList
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private ArrayList GetConstant(Neusoft.HISFC.Models.Base.EnumConstant type)
        {

            ArrayList constList = consManager.GetList(type);
            if (constList == null)
                throw new Neusoft.FrameWork.Exceptions.ReturnNullValueException();

            return constList;

        }

        /// <summary>
        /// ���ݴ��������Ϣ���UC
        /// </summary>
        private void setInfoToControls()
        {
            this.neuLabel1.Text = this.employee.ID + " " + this.employee.Name;//��Ա��������
            this.pbEmplNo.Image = this.CreateBarCode(this.employee.ID);//��Ա��������
            this.pbEmplSign.Image = Bytes2Image(personMgr.QueryEmplSignDataByEmplNo(this.employee.ID));//��Աǩ��
        }

        /// <summary>
        /// ��֤����
        /// </summary>
        /// <returns></returns>
        private bool ValueValidated()
        {
            //��Ա���벻��Ϊ��
            if (this.neuLabel1.Text.Trim() == "")
            {
                MessageBox.Show("��Ա���벻��Ϊ�գ�", "��ʾ", MessageBoxButtons.OK);
                return false;
            }            
            return true;
        }

        /// <summary>
        /// ���ؼ�����ת������Աʵ��
        /// </summary>
        private Neusoft.HISFC.Models.Base.Employee ConvertUcContextToObject()
        {
            Neusoft.HISFC.Models.Base.Employee employee = new Neusoft.HISFC.Models.Base.Employee();
            employee.User01 = this.neuLabel1.Text.Trim();//��Ա����

            return employee;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (Save() == 0)
                this.FindForm().DialogResult = DialogResult.OK;
        }
        /// <summary>
        /// ���淽��
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            //��֤�ؼ����ݷ���Ҫ��
            if (ValueValidated())
            {
                Neusoft.HISFC.Models.Base.Employee empl = ConvertUcContextToObject();
                if (empl == null) return -1;
                try
                {
                    Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

                    Neusoft.HISFC.BizLogic.Manager.Person perMgr = new Neusoft.HISFC.BizLogic.Manager.Person();

                    if (perMgr.Insert(empl) == -1)
                    {
                        if (perMgr.DBErrCode == 1)
                        {
                            if (perMgr.Update(empl) == -1 || perMgr.Update(empl) == 0)
                            {
                                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("������Աʧ�ܣ�");
                                return -1;
                            }
                            else
                            {
                                //Ƕ�������ϵͳ������ҵ��ģ�����Ϣ�����ŽӴ���
                                string errInfo = "";
                                ArrayList alInfo = new ArrayList();
                                alInfo.Add(empl);
                                int param = Function.SendBizMessage(alInfo, Neusoft.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Modify, Neusoft.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Employee, ref errInfo);

                                if (param == -1)
                                {
                                    Neusoft.FrameWork.Management.PublicTrans.RollBack(); ;
                                    Function.ShowMessage("��Ա���ʧ�ܣ�����ϵͳ����Ա���������Ϣ��" + errInfo, MessageBoxIcon.Error);
                                    return -1;
                                }

                            }
                        }
                        else
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("������Աʧ�ܣ�");
                            return -1;
                        }
                    }
                    else
                    {
                        //Ƕ�������ϵͳ������ҵ��ģ�����Ϣ�����ŽӴ���
                        string errInfo = "";
                        ArrayList alInfo = new ArrayList();
                        alInfo.Add(empl);
                        int param = Function.SendBizMessage(alInfo, Neusoft.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add, Neusoft.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Employee, ref errInfo);

                        if (param == -1)
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack(); ;
                            Function.ShowMessage("��Ա���ʧ�ܣ�����ϵͳ����Ա���������Ϣ��" + errInfo, MessageBoxIcon.Error);
                            return -1;
                        }

                    }
                    Neusoft.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show("����ɹ���");
                    return 0;

                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message);
                    return -1;
                }

            }
            else
            {
                return -1;
            }

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
            b.IncludeLabel = false;
            b.Alignment = Align;
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, 150, 50);
        }

        /// <summary>
        /// byte[]תImage
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Image Bytes2Image(byte[] imgData)
        {
            try
            {
                if (imgData == null) return null;
                System.IO.MemoryStream ms = new System.IO.MemoryStream(imgData);
                return System.Drawing.Image.FromStream(ms);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Imageתbyte[]
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        //public static byte[] Image2Bytes(Image img)
        //{
        //    try
        //    {
        //        if (img == null) return null;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
        private void btClose_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }
    }
}
