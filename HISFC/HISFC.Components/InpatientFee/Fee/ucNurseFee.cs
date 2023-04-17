using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.InpatientFee.Fee
{
    /// <summary>
    /// ucNurseFee<br></br>
    /// [��������: סԺ��ʿ�շ�UC]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2006-11-06]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucNurseFee : ucCharge
    {
        /// <summary>
        /// ��ʿվ�շѹ��캯��
        /// </summary>
        public ucNurseFee()
        {
            InitializeComponent();
        }

        //#region ����

        ///// <summary>
        ///// ������Ŀ���
        ///// </summary>
        //protected FS.HISFC.Components.Common.Controls.EnumShowItemType itemKind = FS.HISFC.Components.Common.Controls.EnumShowItemType.Undrug;

        //#endregion

        //#region ����

        ///// <summary>
        ///// ������Ŀ���
        ///// </summary>
        //[Category("�ؼ�����"), Description("���ص���Ŀ��� All���� Undrug��ҩƷ drugҩƷ")]
        //public FS.HISFC.Components.Common.Controls.EnumShowItemType ItemKind 
        //{
        //    set 
        //    {
        //        this.itemKind = value;

        //        base.ucInpatientCharge.������Ŀ��� = this.itemKind;
        //    }
        //    get 
        //    {
        //        return this.ucInpatientCharge.������Ŀ���;
        //    }
        //}

        //#endregion

        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            //TreeView tvTemp = null;
            
            //if (sender != null)
            //{
            //    try
            //    {
            //        tvTemp = sender as TreeView;
            //    }
            //    catch (Exception e) 
            //    {
            //        MessageBox.Show(e.Message);

            //        return null;
            //    }
            //}
           
            //tvTemp.AfterCheck  += new TreeViewEventHandler(tvTemp_AfterCheck);
           
            //tvTemp.CheckBoxes = true;
            if (this.tv != null)
            {
                this.tv.CheckBoxes = true;
                this.tv.AfterCheck += new TreeViewEventHandler(tvTemp_AfterCheck);
            }

            //this.tv.Click += new System.EventHandler(tv_Click);

          
            return base.OnInit(sender, neuObject, param);
        }

        /// <summary>
        ///  ����
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected override int Save()
        {
            if (base.Save() == -1) 
            {
                return -1;
            }

            this.Clear();

            return 1;
        }

        /// <summary>
        /// ��շ���
        /// </summary>
        protected override void Clear()
        {
            if (this.tv != null) 
            {
                foreach (TreeNode node in this.tv.Nodes[0].Nodes) 
                {
                    node.Checked = false;
                }
            }
            
            base.Clear();
        }

        void tvTemp_AfterCheck(object sender, TreeViewEventArgs e)
        {
            base.patientList = this.GetSelectedTreeNodes();
            if (patientList.Count > 0)
            {
                if (base.patientInfo == null
                    || string.IsNullOrEmpty(base.patientInfo.ID)
                    || patientList.Count == 1)
                {
                    base.patientInfo = base.patientList[0] as FS.HISFC.Models.RADT.PatientInfo;

                    base.PatientInfo = base.radtIntegrate.GetPatientInfomation(base.patientInfo.ID);
                }
            }
            else
            {
                base.PatientInfo = null;
            }
        }

        //void tv_Click(object sender, System.EventArgs e)
        //{
        //    this.tv.Nodes[0].Checked = false;
        //    if (this.tv.SelectedNode == this.tv.Nodes[0])
        //    {
        //        return;
        //    }
        //    this.tv.SelectedNode.Checked = !this.tv.SelectedNode.Checked;
        //    //e.Checked = !e.Checked;

        //    if (!this.tv.SelectedNode.Checked)
        //    {
        //        base.Clear();

        //        return ;
        //    }

        //    base.Clear();

        //    base.patientInfo = this.tv.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;

        //    base.PatientInfo = base.radtIntegrate.GetPatientInfomation(base.patientInfo.ID);

        //    //return base.OnSetValue(neuObject, this.tv.SelectedNode.Checked);
        //}
        /// <summary>
        /// TreeView˫������Selected��Ӧ�¼�
        /// </summary>
        /// <param name="neuObject"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (this.tv == null)
            {
                return -1;
            }
            try
            {
                this.tv.AfterCheck -= new TreeViewEventHandler(tvTemp_AfterCheck);
                //this.tv.Nodes[0].Checked = false;
                foreach (TreeNode node in tv.Nodes)
                {
                    node.Checked = false;
                }

                e.Checked = !e.Checked;
                if (!e.Checked)
                {
                    base.Clear();
                    return -2;
                }
                base.Clear();
            }
            finally
            {
                this.tv.AfterCheck += new TreeViewEventHandler(tvTemp_AfterCheck);
            }

            //base.patientInfo = neuObject as FS.HISFC.Models.RADT.PatientInfo;

            //if (base.patientInfo != null)
            //{
            //    base.PatientInfo = base.radtIntegrate.GetPatientInfomation(base.patientInfo.ID);
            //}

            base.patientList = this.GetSelectedTreeNodes();
            if (patientList.Count > 0)
            {
                base.patientInfo = base.patientList[0] as FS.HISFC.Models.RADT.PatientInfo;

                base.PatientInfo = base.radtIntegrate.GetPatientInfomation(base.patientInfo.ID);
            }
            else
            {
                base.PatientInfo = null;
            }
            return base.OnSetValue(neuObject, e);
        }
        
       
        /// <summary>
        /// ��ѡ��Ӧ�¼�
        /// </summary>
        /// <param name="alValues"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValues(System.Collections.ArrayList alValues, object e)
        {   
            return base.OnSetValues(alValues, e);
        }
    }
}
