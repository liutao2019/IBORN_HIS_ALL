//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.Data;
//using System.Text;
//using System.Windows.Forms;

//namespace FS.HISFC.Components.Manager.Items
//{
//    public partial class ucUndrugztManager : UserControl
//    {
//        #region ȫ�ֱ���

//        public delegate void SaveHandle(FS.HISFC.Object.Fee.Undrugzt obj);
//        public event SaveHandle SaveInfo;
//        //private FS.Common.Class.EditTypes editType; //��ʶ

//        #endregion

//        public ucUndrugztManager()
//        {
//            InitializeComponent();
//        }

//        /// <summary>
//        /// ��ʼ�������б�
//        /// </summary>
//        public void InitList()
//        {
//            FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();
//            ArrayList al = dept.GetDeptmentAllOrderByDeptType();
//            //ϵͳ���
//            this.cbSysClass.AddItems(FS.HISFC.Models.Base.SysClassEnumService.List());
//            this.cbDept.AddItems(al);
//        }
//        //public FS.Common.Class.EditTypes EditType
//        //{
//        //    get
//        //    {
//        //        return editType;
//        //    }
//        //    set
//        //    {
//        //        editType = value;
//        //    }

//        //}

//        /// <summary>
//        /// ������е���Ϣ
//        /// </summary>
//        private void ClearInfo()
//        {
//            this.tbZTName.Text = ""; //��������
//            this.tbSpell.Text = "";//ƴ����
//            this.tbWB.Text = "";//�����
//            this.tbInput.Text = "";//������
//            this.cbSysClass.Tag = null;//ϵͳ���
//            this.cbSysClass.Text = "";
//            this.cbDept.Tag = null;//ִ�п���
//            this.cbDept.Text = "";
//            this.Mark4.Text = "";//��鵥����
//            this.tbSortID.Text = ""; //���
//            this.ckbConfirm.Checked = false;//ȷ�ϱ�־
//            this.ckbValid.Checked = false;//��Ч��־
//            this.cbSpellItem.Checked = false;//������Ŀ��־
//        }
//        /// <summary>
//        /// ��������
//        /// </summary>
//        public void SetValue(FS.HISFC.Object.Fee.Undrugzt info)
//        {
//            ztName.Tag = info.ID;//���ױ���
//            ztName.Text = info.Name; //��������
//            txSpellCode.Text = info.spellCode;//ƴ����
//            txtWB.Text = info.wbCode;//�����
//            txtInput.Text = info.inputCode;//������
//            comSys.Tag = info.sysClass;//ϵͳ���
//            comDept.Tag = info.deptCode;//ִ�п���
//            Mark4.Text = info.Mark4;//��鵥����
//            textBox5.Text = info.sortId.ToString(); //���
//            cbEnter.Checked = FS.neuFC.Function.NConvert.ToBoolean(info.confirmFlag);//ȷ�ϱ�־
//            cbValid.Checked = !FS.neuFC.Function.NConvert.ToBoolean(info.validState);//��Ч��־
//            if (info.User01 == "1")
//            {
//                cbSpellItem.Checked = true;//������Ŀ��־
//            }
//            else
//            {
//                cbSpellItem.Checked = false;
//            }
//            Mark1.Text = info.Mark1;
//            Mark2.Text = info.Mark2;
//            Mark3.Text = info.Mark3;
//        }
//        /// <summary>
//        /// ��ȡ����
//        /// </summary>
//        private FS.HISFC.Models.Fee.Undrugztinfo GetValue()
//        {
//            FS.HISFC.Object.Fee.Undrugzt info = new FS.HISFC.Object.Fee.Undrugzt();
//            try
//            {
//                if (ztName.Tag != null)
//                {
//                    info.ID = ztName.Tag.ToString();
//                }
//                info.Name = ztName.Text; //��������
//                info.spellCode = txSpellCode.Text;//ƴ����
//                info.wbCode = txtWB.Text;//�����
//                info.inputCode = txtInput.Text;//������
//                if (comSys.Tag != null)
//                {
//                    info.sysClass = comSys.Tag.ToString();//ϵͳ���
//                }
//                if (comDept.Tag != null)
//                {
//                    info.deptCode = comDept.Tag.ToString();//ִ�п���
//                }
//                info.Mark4 = Mark4.Text;//��鵥����
//                info.sortId = FS.neuFC.Function.NConvert.ToInt32(textBox5.Text); //���
//                if (cbEnter.Checked)
//                {
//                    info.confirmFlag = "1";//ȷ�ϱ�־
//                }
//                else
//                {
//                    info.confirmFlag = "0";//ȷ�ϱ�־
//                }
//                if (cbValid.Checked)
//                {
//                    info.validState = "0";//��Ч��־
//                }
//                else
//                {
//                    info.validState = "1";//��Ч��־
//                }
//                if (cbSpellItem.Checked)
//                {
//                    info.User01 = "1";//������Ŀ��־
//                }
//                else
//                {
//                    info.User01 = "0";//������Ŀ��־
//                }
//                info.Mark1 = Mark1.Text;
//                info.Mark2 = Mark2.Text;
//                info.Mark3 = Mark3.Text;
//                return info;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message);
//                return null;
//            }
//        }
//    }
//}