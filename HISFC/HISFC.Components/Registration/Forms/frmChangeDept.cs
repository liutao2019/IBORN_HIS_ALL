using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Registration.Forms
{
    public partial class frmChangeDept : Form
    {
        public event EventHandler ChangeDeptEvent;
        public frmChangeDept()
        {
            InitializeComponent();
            if (this.DesignMode) return;
            this.Init();
        }
        #region ����
        /// <summary>
        /// �Һ�ʵ��
        /// </summary>
        private FS.HISFC.Models.Registration.Register myRegObj = new FS.HISFC.Models.Registration.Register();

        private FS.HISFC.BizProcess.Integrate.Manager managerMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        private FS.FrameWork.Management.ControlParam conMgr = new FS.FrameWork.Management.ControlParam();

        ArrayList alDeptOrDoct = new ArrayList();

        bool isUpdateRegDt = false;

        #endregion

        #region ����
        /// <summary>
        /// �Һ�ʵ��
        /// </summary>
        public FS.HISFC.Models.Registration.Register MyRegObj
        {
            set
            {
                this.myRegObj = value;
                this.setValue();
            }
            get
            {
                return this.myRegObj;
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            //��ʼ������
            ArrayList al = new ArrayList();
            al = this.managerMgr.QueryRegDepartment();
            if (al == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ѯ����ʧ��") + this.managerMgr.Err);
                return -1;
            }

            this.cmbDept.AddItems(al);
            //��ʼ��ҽ��
            al = new ArrayList();
            al = this.managerMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (al == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ѯҽ��ʧ��") + this.managerMgr.Err);
                return -1;
            }
            this.cmbDoct.AddItems(al);

            isUpdateRegDt = FS.FrameWork.Function.NConvert.ToBoolean(this.conMgr.QueryControlerInfo("400031"));

          

            return 1;
        }

        /// <summary>
        /// ���渳ֵ
        /// </summary>
        private void setValue()
        {
            this.txtCurrentDept.Text = this.myRegObj.DoctorInfo.Templet.Dept.Name;
            this.txtCurrentDoct.Text = this.myRegObj.DoctorInfo.Templet.Doct.Name;
            this.cmbDoct.Tag = this.myRegObj.DoctorInfo.Templet.Doct.ID;
            this.cmbDept.Tag = this.myRegObj.DoctorInfo.Templet.Dept.ID;
        }

        private int Save()
        {
            int returnValue = this.valid();
            if (returnValue < 0)
            {
                return -1;
            }
            FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            FS.FrameWork.Models.NeuObject myObj = new FS.FrameWork.Models.NeuObject();
            FS.FrameWork.Models.NeuObject myDeptObj = new FS.FrameWork.Models.NeuObject();

            for (int i = 0; i < this.cmbDoct.alItems.Count; i++)
            {
                FS.FrameWork.Models.NeuObject obj = this.cmbDoct.alItems[i] as FS.FrameWork.Models.NeuObject;
                if (obj.ID == this.cmbDoct.Tag.ToString())
                {
                    myObj.ID = obj.ID;
                    myObj.Name = obj.Name;
                    break;
                }

            }

            if (this.cmbDept.SelectedItem != null)
            {
                myDeptObj = this.cmbDept.SelectedItem as FS.FrameWork.Models.NeuObject;
            }

            //if (this.cmbDoct.SelectedItem != null)
            //{
            //    myObj = this.cmbDoct.SelectedItem as FS.FrameWork.Models.NeuObject;
            //}
            //else
            //{
            //    myObj.ID = "";
            //    myObj.Name = "";
            //}

            if (isUpdateRegDt)
            {
                this.myRegObj.DoctorInfo.SeeDate = regMgr.GetDateTimeFromSysDateTime();
            }
            returnValue = regMgr.UpdateDeptAndDoct(this.myRegObj.ID, this.cmbDept.Tag.ToString(), this.cmbDept.Text,
                myObj.ID, myObj.Name,this.myRegObj.DoctorInfo.SeeDate.ToString());
            if (returnValue < 0)
            {
                MessageBox.Show("����ʧ�ܣ�" + regMgr.Err);
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }

            returnValue=regMgr.CancelTriage(this.myRegObj.ID);
            if (returnValue < 0)
            {
                MessageBox.Show("ȡ������ʧ�ܣ�" + regMgr.Err);
                FS.FrameWork.Management.PublicTrans.RollBack();
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show( FS.FrameWork.Management.Language.Msg( "���Ƴɹ�" ) );

            this.alDeptOrDoct.Add(myDeptObj);
            this.alDeptOrDoct.Add(myObj);
            return 1;

        }

        private int valid()
        {
            if (this.cmbDept.Tag == null || this.cmbDept.SelectedItem == null)
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ѡ�����" ) );
                this.cmbDept.Focus();
                return -1;
            }
            return 1;
        }

        private void frmChangeDept_Load(object sender, EventArgs e)
        {
            //this.Init();
        }

        private void neuButton1_Click(object sender, EventArgs e)
        {
            int returnValue = this.Save();
            if (returnValue < 0)
            {
                return;
            }
            else
            {
                this.neuButton1.DialogResult = DialogResult.OK;
                if (ChangeDeptEvent != null)
                {
                    this.ChangeDeptEvent(alDeptOrDoct, null);
                }
                this.FindForm().Close();
            }
        }

    }
}