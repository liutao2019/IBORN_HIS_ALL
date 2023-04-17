using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.Operation;

namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// [��������: �����ſؼ�]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2006-12-11]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucAnaesthesia : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucAnaesthesia()
        {
            InitializeComponent();
        }

        #region �ֶ�
        private ArrayList alApplys;

        //{4F4C0095-4E5A-4e48-AD22-D38A2894A31F}
        /// <summary>
        /// ���ҷ����ά������ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager deptStatMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        private FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();
        //{30819195-8798-446e-84C7-2B331A5ADDD3}feng.ch�Ƿ�ϲ�����������������
        /// <summary>
        /// ����ֱ��������ʱ�������������Ϣ
        /// ��ʼʱ��
        /// </summary>
        private DateTime beginTime = new DateTime();
        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime endTime = new DateTime();
        /// <summary>
        /// �Ƿ�ϲ�����������������
        /// </summary>
        private bool isJoinUc = false;
        /// <summary>
        /// ����ʵ��
        /// </summary>
        private List<OperationAppllication> apply = new List<OperationAppllication>();
        //{70B49442-8CA3-4fe7-95F0-D54E24CE2374}feng.ch
        /// <summary>
        /// �������Ƿ������������֮��
        /// </summary>
        private bool isMustAfterArrangement = true;
        #endregion
        #region ����
        //{30819195-8798-446e-84C7-2B331A5ADDD3}feng.ch
        public bool IsJoinUc
        {
            get
            {
                return isJoinUc;
            }
            set
            {
                isJoinUc = value;
            }
        }
        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        public DateTime BeginTime
        {
            get
            {
                return beginTime;
            }
            set
            {
                beginTime = value;
            }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
            }
        }
        public List<OperationAppllication> Apply
        {
            get
            {
                return apply;
            }
            set
            {
                apply = value;
            }
        }
        //{70B49442-8CA3-4fe7-95F0-D54E24CE2374}feng.ch
        /// <summary>
        /// �������Ƿ������������֮��
        /// </summary>
        [Category("�ؼ�����"), Description("�������Ƿ������������֮��")]
        public bool IsMustAfterArrangement
        {
            get
            {
                return isMustAfterArrangement;
            }
            set
            {
                isMustAfterArrangement = value;
            }
        }

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #endregion
        #region ����

        /// <summary>
        /// ˢ�����������б�
        /// </summary>
        /// <returns></returns>
        public int RefreshApplys()
        {
            this.ucAnaesthesiaSpread1.Reset();                    
            //��ʼʱ��
            DateTime beginTime = this.dateTimePicker1.Value.Date;
            //����ʱ��
            DateTime endTime = this.dateTimePicker1.Value.Date.AddDays(1);
            #region ʱ�丳ֵ
            //{30819195-8798-446e-84C7-2B331A5ADDD3}feng.ch
            if (this.isJoinUc)
            {
                beginTime = FS.FrameWork.Function.NConvert.ToDateTime(this.beginTime);
                endTime = FS.FrameWork.Function.NConvert.ToDateTime(this.endTime);
            }
            #endregion
            //FS.HISFC.Components.Interface.Classes.Function.ShowWaitForm("������������,���Ժ�...");
            Application.DoEvents();
            try
            {
                this.ucAnaesthesiaSpread1.Reset();
                //{70B49442-8CA3-4fe7-95F0-D54E24CE2374}feng.ch
                if (!this.isMustAfterArrangement)
                {
                    //�������Ҫ��Ҫ�������ź���������ŵĻ�ֱ��ȡ������Ϣ
                    alApplys = Environment.OperationManager.GetOpsAppList(beginTime, endTime,"1");
                }
                else
                {
                    //�������������Ժ��ȡ����������Ϣ
                    alApplys = Environment.OperationManager.GetOpsAppList(beginTime, endTime);
                }
                if (alApplys != null)
                {
                    foreach (OperationAppllication apply in alApplys)
                    {
                        //{25E1FC1A-66A0-4e40-9236-9CC6710A5704} �����������Ҷ�

                        #region ���������������ҹ�ϵ�����й��ˣ�ֻ�ܹ��˳������������Ӧ�������ҵ�����
                        ArrayList alAnesDepts = this.deptStatMgr.LoadChildren("10", apply.ExeDept.ID, 1);
                        if (alAnesDepts == null)
                        {
                            MessageBox.Show("���ҿ��Ҷ�Ӧ��ϵʱ����" + this.deptStatMgr.Err);
                            return -1;
                        }
                        if (alAnesDepts.Count == 0)
                        {
                            //FS.HISFC.BizProcess.Integrate.Manager depMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                            apply.ExeDept.Name = deptStatMgr.GetDepartment(apply.ExeDept.ID).Name;
                            MessageBox.Show("�������ң���" + apply.ExeDept.Name + "���Ҳ����������ҵĶ�Ӧ��ϵ�����ڿ��ҽṹ����ά����");
                            return -1;
                        }
                        foreach (FS.HISFC.Models.Base.DepartmentStat deptStat in alAnesDepts)
                        {
                            #region {2F58330D-0BEC-4a68-AE06-6C2868CFE545}
                            //{E4C275E8-6E12-4a42-A60A-0EB9A8CB52BD}
                            if (deptStat.DeptCode == (this.dataManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID)
                            {
                                this.ucAnaesthesiaSpread1.AddOperationApplication(apply);
                                break;
                            }
                            //{30819195-8798-446e-84C7-2B331A5ADDD3}feng.ch
                            if (this.isJoinUc)
                            {
                                if (deptStat.PardepCode == (this.dataManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID)
                                {
                                    this.apply.Add(apply);
                                    break;
                                }
                            }
                            #endregion
                        }
                        #endregion
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("��������������Ϣ����!" + e.Message, "��ʾ");
                return -1;
            }

            //FS.HISFC.Components.Interface.Classes.Function.HideWaitForm();
            //if (fpSpread1_Sheet1.RowCount > 0)
            //{
            //    FarPoint.Win.Spread.LeaveCellEventArgs e = new FarPoint.Win.Spread.LeaveCellEventArgs
            //        (new FarPoint.Win.Spread.SpreadView(fpSpread1), 0, 0, 0, (int)Cols.anaeType);
            //    fpSpread1_LeaveCell(fpSpread1, e);
            //fpSpread1.Focus();
            //fpSpread1_Sheet1.SetActiveCell(0, (int)Cols.anaeType, true);
            //}

            return 0;
        }
        #endregion

        #region �¼�

        protected override int OnQuery(object sender, object neuObject)
        {
            this.RefreshApplys();
            this.Fileter();
            
            return base.OnQuery(sender, neuObject);
        }

        private void Fileter() 
        {
            if (this.toolBarService.GetToolButton("δ����").CheckState == System.Windows.Forms.CheckState.Checked)
            {
                this.ucAnaesthesiaSpread1.Filter = ucAnaesthesiaSpread.EnumFilter.NotYet;
            }
            else
            {
                this.ucAnaesthesiaSpread1.Filter = ucAnaesthesiaSpread.EnumFilter.Already;
            }
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.ucAnaesthesiaSpread1.Save();
            this.Fileter();
            return base.OnSave(sender, neuObject);
        }
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("�Ѱ���", "�Ѱ���", FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ��Һ��, true, false, null);
            this.toolBarService.AddToolButton("δ����", "δ����", FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡִ�е�, true, false, null);
            this.toolBarService.AddToolButton("ȫѡ", "ȫѡ", FS.FrameWork.WinForms.Classes.EnumImageList.Qȫѡ, true, false, null);
            this.toolBarService.AddToolButton("ȫ��ѡ", "ȫ��ѡ", FS.FrameWork.WinForms.Classes.EnumImageList.Qȫ��ѡ, true, false, null);
            this.toolBarService.GetToolButton("δ����").CheckState = System.Windows.Forms.CheckState.Checked;
            return this.toolBarService;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.ucAnaesthesiaSpread1.Date = this.dateTimePicker1.Value;
            this.ucAnaesthesiaSpread1.Print();
            return base.OnPrint(sender, neuObject);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "�Ѱ���")
            {
                this.toolBarService.GetToolButton("�Ѱ���").CheckState = System.Windows.Forms.CheckState.Checked;
           
                this.toolBarService.GetToolButton("δ����").CheckState = System.Windows.Forms.CheckState.Unchecked;

                this.ucAnaesthesiaSpread1.Filter = ucAnaesthesiaSpread.EnumFilter.Already; 
            }
            else if (e.ClickedItem.Text == "δ����")
            {
                this.toolBarService.GetToolButton("δ����").CheckState = System.Windows.Forms.CheckState.Checked;
              
                this.toolBarService.GetToolButton("�Ѱ���").CheckState = System.Windows.Forms.CheckState.Unchecked;

                this.ucAnaesthesiaSpread1.Filter = ucAnaesthesiaSpread.EnumFilter.NotYet;
            }
            else if (e.ClickedItem.Text == "ȫѡ")
            {
                this.AllSelect(true);
            }
            else if (e.ClickedItem.Text == "ȫ��ѡ")
            {
                this.AllSelect(false);
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        public void AllSelect(bool isSelect)
        {
            this.ucAnaesthesiaSpread1.AllSelect(isSelect);
        }

        public override int Export(object sender, object neuObject)
        {
            return this.ucAnaesthesiaSpread1.Export();
        }
        #endregion

        private void ucAnaesthesiaSpread1_applictionSelected(object sender, OperationAppllication e)
        {
            if (e != null)
            {
                this.ucArrangementInfo1.OperationApplication = e;
            }
          
        }
    }
}
