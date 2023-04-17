using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;

namespace FS.HISFC.WinForms.DrugStore
{
    /// <summary>
    /// [�ؼ�����: frmDrugBillApprove]<br></br>
    /// [��������: ��ҩ����׼<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11-22]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='������' 
    ///		�޸�ʱ��='2007-03-dd' 
    ///		�޸�Ŀ��='�����ˢ���б�'
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class frmDrugBillApprove : FS.FrameWork.WinForms.Forms.BaseStatusBar, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public frmDrugBillApprove( )
        {
            InitializeComponent( );
            
            this.ucApproveList1.SelectBillEvent += new FS.HISFC.Components.DrugStore.Inpatient.ucApproveList.SelectBillHandler( ucApproveList1_SelectBillEvent );
            this.ucApproveList1.RootNodeCheckedEvent += new EventHandler(ucApproveList1_RootNodeCheckedEvent);
            this.ucDrugBillApprove1.SaveFinished += new EventHandler(ucDrugBillApprove1_SaveFinished);
        }           

        /// <summary>
        /// ��������
        /// </summary>
        private FS.FrameWork.Models.NeuObject operDept = null;

        /// <summary>
        /// ��������
        /// </summary>
        public FS.FrameWork.Models.NeuObject OperDept
        {
            get
            {
                return this.operDept;
            }
            set
            {
                this.operDept = value;

                this.ucApproveList1.OperDept = value;
                this.ucDrugBillApprove1.OperDept = value;
            }
        }

        #region ����

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void InitData()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڼ��ذ�ҩ���ؼ�..."));
            Application.DoEvents();

            #region �����ȡ��ǩ��ʽ

            object interfacePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint)) as FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint;
            if (interfacePrint != null)
            {
                this.ucDrugBillApprove1.AddDrugBill(interfacePrint, true);
            }
            else
            {
                //object[] o = new object[] { };

                //try
                //{
                //    //�����ǩ��ӡ�ӿ�ʵ����
                //    FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                //    string billValue = ctrlIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Inpatient_Print_Bill, true, "Report.DrugStore.ucDrugBill");

                //    System.Runtime.Remoting.ObjectHandle objHandel = System.Activator.CreateInstance("Report", billValue, false, System.Reflection.BindingFlags.CreateInstance, null, o, null, null, null);
                //    object oLabel = objHandel.Unwrap();

                //    this.ucDrugBillApprove1.AddDrugBill(oLabel, true);
                //}
                //catch (System.TypeLoadException ex)
                //{
                //    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                //    MessageBox.Show(Language.Msg("��ҩ�������ռ���Ч\n" + ex.Message));
                //    return;
                //}

                MessageBox.Show("δ����סԺ��ҩ����ʵ��,�޷����а�ҩ����ӡ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            #endregion

            FS.HISFC.Components.DrugStore.Function.IsApproveInitPrintInterface = true;

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        /// <summary>
        /// ��ʾ�ϲ�����
        /// </summary>
        private void ShowMergeData( )
        {
            string drugBillCodes = "";
            bool isNeedApprove = false;
            //��ȡ��ҩ���б��У���ѡ�еİ�ҩ�����,�������Ƿ���Զ԰�ҩ�����к�׼
            isNeedApprove = this.ucApproveList1.GetCheckBill( ref drugBillCodes );
            if( drugBillCodes != null && drugBillCodes != "''")
            {
                this.ucDrugBillApprove1.ShowData( drugBillCodes , isNeedApprove );
            }
        }
        
        #endregion

        #region �¼�

        /// <summary>
        /// ������أ����ݳ�ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmDrugBillApprove_Load( object sender , EventArgs e )
        {
            this.WindowState = FormWindowState.Maximized;

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.InitData();

                ////�Ƿ���в��ֺ�׼ 
                //this.ucDrugBillApprove1.IsPartialApprove = true;

                //ˢ�µ�ǰ��ҩ���б�
                this.ucApproveList1.RefreshBill();
            }
        }

        /// <summary>
        /// ��ҩ���б�ѡ���¼�(������ҩ��������ʾ)
        /// </summary>
        /// <param name="drugBillClass"></param>
        private void ucApproveList1_SelectBillEvent( FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass )
        {
            if( drugBillClass != null )
            {
                //this.ucDrugBillApprove1.ShowData( drugBillClass.DrugBillNO.ToString() ,true );
                if (!drugBillClass.Name.Contains("����"))
                {
                    drugBillClass.Name += "  ����";//{DE81F86D-5AD1-41d6-9F2D-A60D4D19AEFF}
                }
                this.ucDrugBillApprove1.ShowData(drugBillClass);
            }
        }

        private void ucApproveList1_RootNodeCheckedEvent(object sender, EventArgs e)
        {
            if ((bool)sender)
            {
                this.ShowMergeData();
            }
            else
            {
                this.ucDrugBillApprove1.Clear();
            }
        }   

        private void ucDrugBillApprove1_SaveFinished(object sender, EventArgs e)
        {
            this.ucApproveList1.RefreshBill();
        }

        /// <summary>
        /// ��������ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStrip1_ItemClicked( object sender , ToolStripItemClickedEventArgs e )
        {
            switch( e.ClickedItem.Name )
            {
                //ˢ��
                case "tsbRefresh":
                    //ˢ�µ�ǰ��ҩ���б�
                    this.ucApproveList1.RefreshBill( );
                    //��հ�ҩ����ʾ�е�����
                    this.ucDrugBillApprove1.Clear( );
                    break;
                //�ϲ�
                case "tsbMerge": 
                    //��ʾ�ϲ�����
                    this.ShowMergeData( );
                    break;
                //��ӡ
                case "tsbPrint":
                    this.ucDrugBillApprove1.Print( );
                    break;
                //�˳�
                case "tsbExit": 
                    this.Close( );
                    break;

            }
        }

        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] printType = new Type[1];
                printType[0] = typeof(FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint);

                return printType;
            }
        }

        #endregion

 
    }
}