using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.Report.Finance.FinIpb
{
    /// <summary>
    /// ucFinIpbPatientDetail<br></br>
    /// [��������: ��Ժ���˱���UC]<br></br>
    /// [�� �� ��: ���]<br></br>
    /// [����ʱ��: 2007-10-22]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucFinIpbOutPatientReport : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinIpbOutPatientReport()
        {
            InitializeComponent();
        }

        private string personCode = "ALL";
        private string personName = "ȫ��";
        private string pactCode = "ALL";
        private string balType = "ALL";
        ArrayList alPersonconstantList = new ArrayList();
        ArrayList alBalanceList = new ArrayList();
        ArrayList alPactList = new ArrayList();


        /// <summary>
        /// סԺ�շ�ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();

        //{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
        /// <summary>
        /// ��ͬ��λ
        /// </summary>
        FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();


        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }

            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value,this.personCode,this.pactCode,this.balType);
        }

        /// <summary>
        /// ��ʼ����ͬ��λ
        /// </summary>
        /// <returns>�ɹ�1 ʧ�� -1</returns>
        private int InitPact()
        {
            int findAll = 0;
            FS.FrameWork.Models.NeuObject objAll = new FS.FrameWork.Models.NeuObject();

            objAll.ID = "ALL";
            objAll.Name = "ȫ��";
            //{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
            //this.alPactList = this.consManager.GetList(FS.HISFC.Models.Base.EnumConstant.PACTUNIT);
            this.alPactList = this.pactManager.QueryPactUnitAll();
            if (alPactList == null)
            {
                MessageBox.Show(Language.Msg("���غ�ͬ��λ�б����!") + this.consManager.Err);

                return -1;
            }

            alPactList.Insert(0,objAll);

            findAll = alPactList.IndexOf(objAll);

            this.cboPactCode.AddItems(alPactList);

            if (findAll >= 0)
            {
                this.cboPactCode.SelectedIndex = findAll;
            }

            return 1;
        }


        private int InitBalanceType()
        {
            int findAll = 0;
            FS.FrameWork.Models.NeuObject objAll = new FS.FrameWork.Models.NeuObject();

            objAll.ID = "ALL";
            objAll.Name = "ȫ��";

            FS.FrameWork.Models.NeuObject objI = new FS.FrameWork.Models.NeuObject();

            objI.ID = "I";
            objI.Name = "��;����";

            FS.FrameWork.Models.NeuObject objO = new FS.FrameWork.Models.NeuObject();

            objO.ID = "O";
            objO.Name = "��Ժ����";

            FS.FrameWork.Models.NeuObject objQ = new FS.FrameWork.Models.NeuObject();

            objQ.ID = "Q";
            objQ.Name = "Ƿ�ѽ���";

            alBalanceList.Insert(0,objAll);
            alBalanceList.Insert(1, objI);
            alBalanceList.Insert(2, objO);
            alBalanceList.Insert(3, objQ);

            this.cboBalanceType.AddItems(alBalanceList);

            findAll = alBalanceList.IndexOf(objAll);

            if (findAll >= 0)
            {
                this.cboBalanceType.SelectedIndex = findAll;
            }

            return 1;
        }

        #region �¼�

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucFinIpbOutPatientReport_Load(object sender, EventArgs e)
        {
            DateTime nowTime = this.inpatientManager.GetDateTimeFromSysDateTime();

             

            this.dtpEndTime.Value = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, 23, 59, 59);
            this.dtpBeginTime.Value = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, 00, 00, 00);


            //�������
            FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
            alPersonconstantList = manager.QueryEmployeeAll();
            FS.HISFC.Models.Base.Employee allPerson = new FS.HISFC.Models.Base.Employee();
            allPerson.ID = "ALL";
            allPerson.Name = "ȫ��";
            allPerson.SpellCode = "QB";

            alPersonconstantList.Insert(0, allPerson);
            this.cboPersonCode.AddItems(alPersonconstantList);
            cboPersonCode.SelectedIndex = 0;

            this.InitPact();

            this.InitBalanceType();

        }



        private void cboPersonCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPersonCode.SelectedIndex >= 0)
            {
                personCode = ((FS.HISFC.Models.Base.Employee)alPersonconstantList[this.cboPersonCode.SelectedIndex]).ID.ToString();
                personName = ((FS.HISFC.Models.Base.Employee)alPersonconstantList[this.cboPersonCode.SelectedIndex]).Name.ToString();
            }
        }

        #endregion

        private void cboPactCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPactCode.SelectedIndex >= 0)
            {
                pactCode = ((FS.FrameWork.Models.NeuObject)this.alPactList[this.cboPactCode.SelectedIndex]).ID.ToString();
            }

        }

        private void cboBalanceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboBalanceType.SelectedIndex >= 0)
            {
                balType =((FS.FrameWork.Models.NeuObject)this.alBalanceList[this.cboBalanceType.SelectedIndex]).ID.ToString();
            }
        }
    }
}