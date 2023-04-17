using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.HealthRecord.EnumServer;
namespace FS.HISFC.Components.HealthRecord.Search
{
    public partial class ucCaseQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCaseQuery()
        {
            InitializeComponent();
        }

        #region  ȫ�ֱ���
        private FS.HISFC.BizLogic.HealthRecord.SearchManager SearchMana = new FS.HISFC.BizLogic.HealthRecord.SearchManager();
        private ArrayList deptList = null;
        //������ 
        private string QueryOper = "";
        //�������� 
        private string QueryItem = "";
        //����ҽ��
        private string QueryDoc = "";
        private string frmSequenceNo = "-1";
        #endregion

        #region ��������Ϣ

        /// <summary>
        /// ���幤��������
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #region ��ʼ��������
        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("��ѯ", "��ѯ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ, true, false, null); 
            return toolBarService;
        }
        #endregion

        #region ���������Ӱ�ť�����¼�
        /// <summary>
        /// ���������Ӱ�ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "��ѯ":
                    this.SearchInfo();
                    break; 
                default:
                    break;
            }
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.SearchInfo();
            return base.OnPrint(sender, neuObject);
        }

        #endregion

        #endregion

        #region  ���ƺ���
        #region �����¼�
        /// <summary>
        /// ʱ�临ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbTime_CheckedChanged(object sender, System.EventArgs e)
        {
            if (tbTime.Checked)
            {
                this.DTBeginTime.Enabled = true;
                this.DTEndTime.Enabled = true;
            }
            else
            {
                this.DTBeginTime.Enabled = false;
                this.DTEndTime.Enabled = false;
            }
        }
        /// <summary>
        /// ��ϸ�ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void diagNose_CheckedChanged(object sender, System.EventArgs e)
        {
            if (diagNose.Checked)
            {
                cbClinicDiag.Enabled = true;
                cbInHosDiag.Enabled = true;
                cbSameIN.Enabled = true;
                cbIcd1.Enabled = true;
                cbMainDiag1.Enabled = true;
                cbAccord1.Enabled = true;
                COMOper1.Enabled = true;
                COMoutState1.Enabled = true;
                SingleDisease1.Enabled = true;
                cbMainDiag2.Enabled = true;
                cbAccord2.Enabled = true;
                COMOper2.Enabled = true;
                COMoutState2.Enabled = true;
                cbIcd.Enabled = true;
                cbMainDiag3.Enabled = true;
                cbAccord3.Enabled = true;
                COMOper3.Enabled = true;
                COMoutState3.Enabled = true;
                SingleDisease2.Enabled = true;
                cbIcd2.Enabled = true;
            }
            else
            {
                cbClinicDiag.Enabled = false;
                cbClinicDiag.Checked = false;
                TXClinicDiag.Enabled = false;
                cbInHosDiag.Enabled = false;
                cbInHosDiag.Checked = false;
                TXInhisDiag.Enabled = false;
                cbSameIN.Enabled = false;
                cbSameIN.Checked = false;
                cbIcd1.Checked = false;
                cbIcd1.Enabled = false;
                TXIcdDiag.Enabled = false;
                cbMainDiag1.Enabled = false;
                cbMainDiag1.Checked = false;
                cbAccord1.Enabled = false;
                cbAccord1.Checked = false;
                COMOper1.Enabled = false;
                COMOper1.Enabled = false;
                COMoutState1.Enabled = false;
                SingleDisease1.Enabled = false;
                SingleDisease1.Checked = false;
                TXIcdDiag2.Enabled = false;
                cbMainDiag2.Enabled = false;
                cbMainDiag2.Checked = false;
                cbAccord2.Enabled = false;
                cbAccord2.Checked = false;
                COMOper2.Enabled = false;
                COMOper2.Enabled = false;
                COMoutState2.Enabled = false;
                cbIcd.Enabled = false;
                cbIcd.Checked = false;
                TXIcdDiag3.Enabled = false;
                cbMainDiag3.Enabled = false;
                cbMainDiag3.Checked = false;
                cbAccord3.Enabled = false;
                cbAccord3.Checked = false;
                COMOper3.Enabled = false;
                COMOper3.Enabled = false;
                COMoutState3.Enabled = false;
                SingleDisease2.Enabled = false;
                SingleDisease2.Checked = false;
                cbIcd2.Enabled = false;
                cbIcd2.Checked = false;
            }
        }

        private void cbClinicDiag_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbClinicDiag.Checked)
            {
                TXClinicDiag.Enabled = true;
            }
            else
            {
                TXClinicDiag.Enabled = false;
            }
        }

        private void cbInHosDiag_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbInHosDiag.Checked)
            {
                TXInhisDiag.Enabled = true;
            }
            else
            {
                TXInhisDiag.Enabled = false;
            }
        }

        private void cbIcd1_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbIcd1.Checked)
            {
                TXIcdDiag.Enabled = true;
                cbMainDiag1.Enabled = true;
                cbAccord1.Enabled = true;
                COMOper1.Enabled = true;
                COMoutState1.Enabled = true;
                SingleDisease1.Enabled = true;
                cbIcd2.Enabled = true;
                cbIcd.Enabled = true;
            }
            else
            {
                this.TXIcdDiag.Enabled = false;
                cbMainDiag1.Enabled = false;
                cbAccord1.Enabled = false;
                COMOper1.Enabled = false;
                COMoutState1.Enabled = false;
                SingleDisease1.Enabled = false;
                cbIcd2.Checked = false;
                cbIcd.Checked = false;
                cbIcd2.Enabled = false;
                cbIcd.Enabled = false;
            }
        }

        private void cbIcd2_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbIcd2.Checked)
            {
                TXIcdDiag2.Enabled = true;
                cbMainDiag2.Enabled = true;
                cbAccord2.Enabled = true;
                COMOper2.Enabled = true;
                COMoutState2.Enabled = true;
            }
            else
            {
                TXIcdDiag2.Enabled = false;
                cbMainDiag2.Enabled = false;
                cbAccord2.Enabled = false;
                COMOper2.Enabled = false;
                COMoutState2.Enabled = false;
            }
        }

        private void cbIcd_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbIcd.Checked)
            {
                TXIcdDiag3.Enabled = true;
                cbMainDiag3.Enabled = true;
                cbAccord3.Enabled = true;
                COMOper3.Enabled = true;
                COMoutState3.Enabled = true;
                SingleDisease2.Enabled = true;
            }
            else
            {
                TXIcdDiag3.Enabled = false;
                cbMainDiag3.Enabled = false;
                cbAccord3.Enabled = false;
                COMOper3.Enabled = false;
                COMoutState3.Enabled = false;
                SingleDisease2.Enabled = false;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Operation_CheckedChanged(object sender, System.EventArgs e)
        {
            if (Operation.Checked)
            {
                cbOperation1.Enabled = true;
                Operator1.Enabled = true;
                cbAnesthetist1.Enabled = true;
                SingleOper1.Enabled = true;
                cbOperation2.Enabled = true;
                Operator2.Enabled = true;
                cbAnesthetist2.Enabled = true;
                cbOperation3.Enabled = true;
                Operator3.Enabled = true;
                cbAnesthetist3.Enabled = true;
                SingleOper2.Enabled = true;
                checkBox1.Enabled = true;
            }
            else
            {
                cbOperation1.Enabled = false;
                cbOperation1.Checked = false;
                TXOperation1.Enabled = false;
                Operator1.Enabled = false;
                Operator1.Checked = false;
                COMOperator1.Enabled = false;
                cbAnesthetist1.Enabled = false;
                cbAnesthetist1.Checked = false;
                COMAnesthetist1.Enabled = false;
                SingleOper1.Enabled = false;
                SingleOper1.Checked = false;
                cbOperation2.Enabled = false;
                cbOperation2.Checked = false;
                TXOperation2.Enabled = false;
                Operator2.Enabled = false;
                Operator2.Checked = false;
                COMOperator2.Enabled = false;
                cbAnesthetist2.Enabled = false;
                cbAnesthetist2.Checked = false;
                COMAnesthetist2.Enabled = false;
                cbOperation3.Enabled = false;
                cbOperation3.Checked = false;
                TXOperation3.Enabled = false;
                Operator3.Enabled = false;
                Operator3.Checked = false;
                COMOperator3.Enabled = false;
                cbAnesthetist3.Enabled = false;
                cbAnesthetist3.Checked = false;
                COMAnesthetist3.Enabled = false;
                SingleOper2.Enabled = false;
                SingleOper2.Checked = false;
                checkBox1.Enabled = false;
                COMHocusType.Enabled = false;
                checkBox1.Checked = false;
            }

        }
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbOperation1_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbOperation1.Checked)
            {
                TXOperation1.Enabled = true;
                Operator1.Enabled = true;
                cbAnesthetist1.Enabled = true;
                SingleOper1.Enabled = true;
                checkBox1.Enabled = true;
            }
            else
            {
                TXOperation1.Enabled = false;
                Operator1.Checked = false;
                Operator1.Enabled = false;
                cbAnesthetist1.Checked = false;
                cbAnesthetist1.Enabled = false;
                checkBox1.Checked = false;
                checkBox1.Enabled = false;
                SingleOper1.Checked = false;
                SingleOper1.Enabled = false;
            }
        }

        private void Operator1_CheckedChanged(object sender, System.EventArgs e)
        {
            //����
            if (Operator1.Checked)
            {
                COMOperator1.Enabled = true;
            }
            else
            {
                COMOperator1.Enabled = false;
            }
        }

        private void cbAnesthetist1_CheckedChanged(object sender, System.EventArgs e)
        {
            //����ʦ
            if (cbAnesthetist1.Checked)
            {
                COMAnesthetist1.Enabled = true;
            }
            else
            {
                COMAnesthetist1.Enabled = false;
            }
        }

        private void cbOperation2_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbOperation2.Checked)
            {
                TXOperation2.Enabled = true;
                Operator2.Enabled = true;
                cbAnesthetist2.Enabled = true;
            }
            else
            {
                TXOperation2.Enabled = false;
                Operator2.Checked = false;
                Operator2.Enabled = false;
                cbAnesthetist2.Checked = false;
                cbAnesthetist2.Enabled = false;
            }
        }

        private void Operator2_CheckedChanged(object sender, System.EventArgs e)
        {
            if (Operator2.Checked)
            {
                COMOperator2.Enabled = true;
            }
            else
            {
                COMOperator2.Enabled = false;
            }
        }

        private void cbAnesthetist2_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbAnesthetist2.Checked)
            {
                COMAnesthetist2.Enabled = true;
            }
            else
            {
                COMAnesthetist2.Enabled = false;
            }
        }

        private void cbOperation3_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbOperation3.Checked)
            {
                TXOperation3.Enabled = true;
                Operator3.Enabled = true;
                cbAnesthetist3.Enabled = true;
                SingleOper2.Enabled = true;
            }
            else
            {
                TXOperation3.Enabled = false;
                Operator3.Checked = false;
                Operator3.Enabled = false;
                cbAnesthetist3.Checked = false;
                cbAnesthetist3.Enabled = false;
                SingleOper2.Checked = false;
                SingleOper2.Enabled = false;
            }
        }

        private void Operator3_CheckedChanged(object sender, System.EventArgs e)
        {
            if (Operator3.Checked)
            {
                COMOperator3.Enabled = true;
            }
            else
            {
                COMOperator3.Enabled = false;
            }
        }

        private void cbAnesthetist3_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbAnesthetist3.Checked)
            {
                COMAnesthetist3.Enabled = true;
            }
            else
            {
                COMAnesthetist3.Enabled = false;
            }
        } 
        private void cbTo_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbTo.Checked)
            {
                DToutDate.Enabled = true;
            }
            else
            {
                DToutDate.Enabled = false;
            }
        }

        private void tbTime_CheckedChanged_1(object sender, System.EventArgs e)
        {
            if (tbTime.Checked)
            {
                DTBeginTime.Enabled = true;
                DTEndTime.Enabled = true;
            }
            else
            {
                DTBeginTime.Enabled = false;
                DTEndTime.Enabled = false;
            }
        }
        /// <summary>
        /// ������Ŀ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbSpecalItem_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbSpecalItem.Checked)
            {
                cbSpecalItem2.Checked = false;
                CHSyndrome1.Enabled = true;
                CHInfection.Enabled = true;
                CHCT.Enabled = true;
                CHUFCT.Enabled = true;
                CHMR.Enabled = true;
                CHXG.Enabled = true;
                CHBC.Enabled = true;
            }
            else if (!cbSpecalItem2.Checked && !cbSpecalItem.Checked)
            {
                CHSyndrome1.Enabled = false;
                CHSyndrome1.Checked = false;
                CHInfection.Enabled = false;
                CHInfection.Checked = false;
                CHCT.Enabled = false;
                CHCT.Checked = false;
                CHUFCT.Enabled = false;
                CHUFCT.Checked = false;
                CHMR.Enabled = false;
                CHMR.Checked = false;
                CHXG.Enabled = false;
                CHXG.Checked = false;
                CHBC.Enabled = false;
                CHBC.Checked = false;
            }
        }
        /// <summary>
        /// ������Ŀ�ų�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbSpecalItem2_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbSpecalItem2.Checked)
            {
                cbSpecalItem.Checked = false;
                CHSyndrome1.Enabled = true;
                CHInfection.Enabled = true;
                CHCT.Enabled = true;
                CHUFCT.Enabled = true;
                CHMR.Enabled = true;
                CHXG.Enabled = true;
                CHBC.Enabled = true;
            }
            else if (!cbSpecalItem2.Checked && !cbSpecalItem.Checked)
            {
                CHSyndrome1.Enabled = false;
                CHInfection.Enabled = false;
                CHCT.Enabled = false;
                CHUFCT.Enabled = false;
                CHMR.Enabled = false;
                CHXG.Enabled = false;
                CHBC.Enabled = false;
            }
        }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox38_CheckedChanged(object sender, System.EventArgs e)
        {
            if (checkBox38.Checked)
            {
                checkBox39.Enabled = true; //����
                Sex.Enabled = true;//�Ա�
                cbAge.Enabled = true;//����
                cbHomeZip.Enabled = true; //��������
                cbDeptMent.Enabled = true;//�Ʊ�
                cbComeFrom.Enabled = true;//��Ժ��Դ
                cbInState.Enabled = true;//��Ժ���
                cbWorkZip.Enabled = true; //��������
                cbInhospital.Enabled = true;//סԺ����
                cbDoctor.Enabled = true;//ǩ��ҽ��
            }
            else
            {
                checkBox39.Enabled = false; //����
                checkBox39.Checked = false;
                Sex.Enabled = false;//�Ա�
                Sex.Checked = false;
                cbAge.Enabled = false;//����
                cbAge.Checked = false;
                cbHomeZip.Enabled = false; //��������
                cbHomeZip.Checked = false;
                cbDeptMent.Enabled = false;//�Ʊ�
                cbDeptMent.Checked = false;
                cbComeFrom.Enabled = false;//��Ժ��Դ
                cbComeFrom.Checked = false;
                cbInState.Enabled = false;//��Ժ���
                cbInState.Checked = false;
                cbWorkZip.Enabled = false; //��������
                cbWorkZip.Checked = false;
                cbInhospital.Enabled = false;//סԺ����
                cbInhospital.Checked = false;
                cbDoctor.Enabled = false;//ǩ��ҽ��
                cbDoctor.Checked = false;
                ComInNum.Enabled = false;
                COMSexType.Enabled = false;
                TXAge.Enabled = false;
                COMAgeType.Enabled = false;
                COMInAvenue.Enabled = false;
                COMDepartment.Enabled = false;
                TXHomeZip.Enabled = false;
                COMInState.Enabled = false;
                TXWorkZip.Enabled = false;
                COMDoctor.Enabled = false;
                DTInDate.Enabled = false;
                DToutDate.Enabled = false;
                btDept.Enabled = false;
            }
        }

        private void Sex_CheckedChanged(object sender, System.EventArgs e)
        {
            if (Sex.Checked)
            {
                COMSexType.Enabled = true;
            }
            else
            {
                COMSexType.Enabled = false;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox39_CheckedChanged(object sender, System.EventArgs e)
        {
            if (checkBox39.Checked)
            {
                ComInNum.Enabled = true;
            }
            else
            {
                ComInNum.Enabled = false;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbAge_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbAge.Checked)
            {
                TXAge.Enabled = true;
                COMAgeType.Enabled = true;
            }
            else
            {
                TXAge.Enabled = false;
                COMAgeType.Enabled = false;
            }
        }
        /// <summary>
        /// ��ͥ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbHomeZip_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbHomeZip.Checked)
            {
                TXHomeZip.Enabled = true;
            }
            else
            {
                TXHomeZip.Enabled = false;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbDeptMent_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbDeptMent.Checked)
            {
                COMDepartment.Enabled = true;
                btDept.Enabled = true;
            }
            else
            {
                COMDepartment.Enabled = false;
                btDept.Enabled = false;
            }
        }
        /// <summary>
        /// ��Դ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbComeFrom_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbComeFrom.Checked)
            {
                COMInAvenue.Enabled = true;
            }
            else
            {
                COMInAvenue.Enabled = false;
            }
        }
        /// <summary>
        /// ����ʽ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            if (checkBox1.Checked)
            {
                COMHocusType.Enabled = true;
            }
            else
            {
                COMHocusType.Enabled = false;
            }
        }
        /// <summary>
        /// ��Ժ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbInState_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbInState.Checked)
            {
                COMInState.Enabled = true;
            }
            else
            {
                COMInState.Enabled = false;
            }
        }
        /// <summary>
        /// ������ַ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbWorkZip_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbWorkZip.Checked)
            {
                TXWorkZip.Enabled = true;
            }
            else
            {
                TXWorkZip.Enabled = false;
            }
        }
        /// <summary>
        /// סԺ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbInhospital_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbInhospital.Checked)
            {
                DTInDate.Enabled = true;
                cbTo.Enabled = true;
            }
            else
            {
                DTInDate.Enabled = false;
                cbTo.Checked = false;
                cbTo.Enabled = false;
            }
        }
        /// <summary>
        /// ǩ��ҽ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbDoctor_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbDoctor.Checked)
            {
                COMDoctor.Enabled = true;
            }
            else
            {
                COMDoctor.Enabled = false;
            }
        }
        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbSearch_Click(object sender, System.EventArgs e)
        {
        }
        #endregion
        /// <summary>
        /// ������ʷ��¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbhistory_Click(object sender, System.EventArgs e)
        {
        }
        /// <summary>
        /// �ر�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbClose_Click(object sender, System.EventArgs e)
        {
            //�ر�
            this.FindForm().Close();
        }

        /// <summary>
        /// �������ݵ���Ч��
        /// </summary>
        /// <returns></returns>
        private int ValueState()
        {
            //			if(diagNose.Checked)
            //			{
            //				if(TXClinicDiag.Enabled &&TXClinicDiag.Text !="")
            //				{
            //					 
            //				}
            //			}
            //			if(Operation.Checked)
            //			{
            //			}
            //			if(cbSpecalItem.Checked ||cbSpecalItem2.Checked)
            //			{
            //			}
            //			if(checkBox38.Checked)
            //			{
            //			}
            return 1;
        }

        /// <summary>
        /// �������е�����д��������
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private int SetContralInfo(ArrayList list)
        {
            foreach (FS.FrameWork.Models.NeuObject obj in list)
            {
                for (int i = this.panel1.Controls.Count - 1; i >= 0; i--)
                {
                    if (panel1.Controls[i].Name == obj.Name)
                    {
                        panel1.Controls[i].Enabled = true;
                        switch (obj.User01)
                        {
                            case "ComboBox":
                                panel1.Controls[i].Tag = obj.ID;
                                break;
                            case "DateTime":
                                ((FS.FrameWork.WinForms.Controls.NeuDateTimePicker)panel1.Controls[i]).Value = FS.FrameWork.Function.NConvert.ToDateTime(obj.ID);
                                break;
                            case "TextBox":
                                panel1.Controls[i].Text = obj.ID;
                                break;
                            case "CheckBox":
                                if (obj.ID == "1")
                                {
                                    ((FS.FrameWork.WinForms.Controls.NeuCheckBox)panel1.Controls[i]).Checked = true;
                                }
                                break;
                        }
                    }
                }
            }
            return 1;
        }
        private int Reset()
        {
            return 1;
        }
        private void DTInDate_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar < (char)48 || e.KeyChar > (char)57)
            {
                DTInDate.Text = "";
                MessageBox.Show("ֻ����������");
            }

        }

        private void DToutDate_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar < 48 || e.KeyChar > 57)
            {
                DTInDate.Text = "";
                MessageBox.Show("ֻ����������");
            }
        }

        #region  ��Ժʱ��enable�¼�
        private void DTEndTime_EnabledChanged(object sender, System.EventArgs e)
        {
            if (DTEndTime.Enabled)
            {
                if (!tbTime.Checked)
                {
                    tbTime.Checked = true;
                }
            }
        }
        private void DTBeginTime_EnabledChanged(object sender, System.EventArgs e)
        {
            if (DTBeginTime.Enabled)
            {
                if (!tbTime.Checked)
                {
                    tbTime.Checked = true;
                }
            }
        }
        #endregion
        #region ��ϵ�enable�¼�
        private void TXClinicDiag_EnabledChanged(object sender, System.EventArgs e)
        {
            if (TXClinicDiag.Enabled)
            {
                if (!cbClinicDiag.Checked)
                {
                    cbClinicDiag.Checked = true;
                }
                if (!diagNose.Checked)
                {
                    diagNose.Checked = true;
                }
            }
        }

        private void TXIcdDiag_EnabledChanged(object sender, System.EventArgs e)
        {
            if (TXIcdDiag.Enabled)
            {
                if (!cbIcd1.Checked)
                {
                    cbIcd1.Checked = true;
                }
                if (!diagNose.Checked)
                {
                    diagNose.Checked = true;
                }
            }
        }

        private void TXIcdDiag2_EnabledChanged(object sender, System.EventArgs e)
        {
            if (TXIcdDiag2.Enabled)
            {
                if (!cbIcd2.Checked)
                {
                    cbIcd2.Checked = true;
                }
                if (!diagNose.Checked)
                {
                    diagNose.Checked = true;
                }
            }
        }

        private void TXIcdDiag3_EnabledChanged(object sender, System.EventArgs e)
        {
            if (TXIcdDiag3.Enabled)
            {
                if (!cbIcd.Checked)
                {
                    cbIcd.Checked = true;
                }
                if (!diagNose.Checked)
                {
                    diagNose.Checked = true;
                }
            }
        }

        private void TXInhisDiag_EnabledChanged(object sender, System.EventArgs e)
        {
            if (TXInhisDiag.Enabled)
            {
                if (!cbInHosDiag.Checked)
                {
                    cbInHosDiag.Checked = true;
                }
                if (!diagNose.Checked)
                {
                    diagNose.Checked = true;
                }
            }
        }
        #endregion
        #region ������enable�¼�
        private void TXOperation1_EnabledChanged(object sender, System.EventArgs e)
        {
            if (TXOperation1.Enabled)
            {
                if (!cbOperation1.Checked)
                {
                    cbOperation1.Checked = true;
                }
                if (!Operation.Checked)
                {
                    Operation.Checked = true;
                }
            }
        }

        private void COMOperator1_EnabledChanged(object sender, System.EventArgs e)
        {
            if (COMOperator1.Enabled)
            {
                if (!Operator1.Checked)
                {
                    Operator1.Checked = true;
                }
                if (!Operation.Checked)
                {
                    Operation.Checked = true;
                }
            }
        }

        private void COMAnesthetist1_EnabledChanged(object sender, System.EventArgs e)
        {
            if (COMAnesthetist1.Enabled)
            {
                if (!cbAnesthetist1.Checked)
                {
                    cbAnesthetist1.Checked = true;
                }
                if (!Operation.Checked)
                {
                    Operation.Checked = true;
                }
            }

        }

        private void COMHocusType_EnabledChanged(object sender, System.EventArgs e)
        {
            if (COMHocusType.Enabled)
            {
                if (!checkBox1.Checked)
                {
                    checkBox1.Checked = true;
                }
                if (!Operation.Checked)
                {
                    Operation.Checked = true;
                }
            }
        }

        private void TXOperation2_EnabledChanged(object sender, System.EventArgs e)
        {
            if (TXOperation2.Enabled)
            {
                if (!cbOperation2.Checked)
                {
                    cbOperation2.Checked = true;
                }
                if (!Operation.Checked)
                {
                    Operation.Checked = true;
                }
            }
        }

        private void COMOperator2_EnabledChanged(object sender, System.EventArgs e)
        {
            if (COMOperator2.Enabled)
            {
                if (!Operator2.Checked)
                {
                    Operator2.Checked = true;
                }
                if (!Operation.Checked)
                {
                    Operation.Checked = true;
                }
            }
        }

        private void COMAnesthetist2_EnabledChanged(object sender, System.EventArgs e)
        {
            if (COMAnesthetist2.Enabled)
            {
                if (!cbAnesthetist2.Checked)
                {
                    cbAnesthetist2.Checked = true;
                }
                if (!Operation.Checked)
                {
                    Operation.Checked = true;
                }
            }
        }

        private void TXOperation3_EnabledChanged(object sender, System.EventArgs e)
        {
            if (TXOperation3.Enabled)
            {
                if (!cbOperation3.Checked)
                {
                    cbOperation3.Checked = true;
                }
                if (!Operation.Checked)
                {
                    Operation.Checked = true;
                }
            }

        }

        private void COMOperator3_EnabledChanged(object sender, System.EventArgs e)
        {
            if (COMOperator3.Enabled)
            {
                if (!Operator3.Checked)
                {
                    Operator3.Checked = true;
                }
                if (!Operation.Checked)
                {
                    Operation.Checked = true;
                }
            }

        }

        private void COMAnesthetist3_EnabledChanged(object sender, System.EventArgs e)
        {
            if (COMAnesthetist3.Enabled)
            {
                if (!cbAnesthetist3.Checked)
                {
                    cbAnesthetist3.Checked = true;
                }
                if (!Operation.Checked)
                {
                    Operation.Checked = true;
                }
            }
        }
        #endregion
        #region ������Ϣ
        private void TXInNum_EnabledChanged(object sender, System.EventArgs e)
        {
            if (ComInNum.Enabled)
            {
                if (!checkBox39.Checked)
                {
                    checkBox39.Checked = true;
                }
                if (!checkBox38.Checked)
                {
                    checkBox38.Checked = true;
                }
            }
        }

        private void COMSexType_EnabledChanged(object sender, System.EventArgs e)
        {
            if (COMSexType.Enabled)
            {
                if (!Sex.Checked)
                {
                    Sex.Checked = true;
                }
                if (!checkBox38.Checked)
                {
                    checkBox38.Checked = true;
                }
            }
        }

        private void TXAge_EnabledChanged(object sender, System.EventArgs e)
        {
            if (TXAge.Enabled)
            {
                if (!cbAge.Checked)
                {
                    cbAge.Checked = true;
                }
                if (!checkBox38.Checked)
                {
                    checkBox38.Checked = true;
                }
            }
        }

        private void COMAgeType_EnabledChanged(object sender, System.EventArgs e)
        {

        }

        private void TXHomeZip_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {

        }

        private void TXHomeZip_EnabledChanged(object sender, System.EventArgs e)
        {
            if (TXHomeZip.Enabled)
            {
                if (!cbHomeZip.Checked)
                {
                    cbHomeZip.Checked = true;
                }
                if (!checkBox38.Checked)
                {
                    checkBox38.Checked = true;
                }
            }
        }

        private void COMDepartment_EnabledChanged(object sender, System.EventArgs e)
        {
            if (COMDepartment.Enabled)
            {
                if (!cbDeptMent.Checked)
                {
                    cbDeptMent.Checked = true;
                }
                if (!checkBox38.Checked)
                {
                    checkBox38.Checked = true;
                }
            }
        }

        private void COMInAvenue_EnabledChanged(object sender, System.EventArgs e)
        {
            if (COMInAvenue.Enabled)
            {
                if (!cbComeFrom.Checked)
                {
                    cbComeFrom.Checked = true;
                }
                if (!checkBox38.Checked)
                {
                    checkBox38.Checked = true;
                }
            }
        }

        private void COMInState_EnabledChanged(object sender, System.EventArgs e)
        {
            if (COMInState.Enabled)
            {
                if (!cbInState.Checked)
                {
                    cbInState.Checked = true;
                }
                if (!checkBox38.Checked)
                {
                    checkBox38.Checked = true;
                }
            }
        }

        private void TXWorkZip_EnabledChanged(object sender, System.EventArgs e)
        {
            if (TXWorkZip.Enabled)
            {
                if (!cbWorkZip.Checked)
                {
                    cbWorkZip.Checked = true;
                }
                if (!checkBox38.Checked)
                {
                    checkBox38.Checked = true;
                }
            }
        }

        private void COMDoctor_EnabledChanged(object sender, System.EventArgs e)
        {
            if (COMDoctor.Enabled)
            {
                if (!cbDoctor.Checked)
                {
                    cbDoctor.Checked = true;
                }
                if (!checkBox38.Checked)
                {
                    checkBox38.Checked = true;
                }
            }
        }

        private void DTInDate_EnabledChanged(object sender, System.EventArgs e)
        {
            //			if(DTInDate.Enabled)
            //			{
            //				if(!cbInhospital.Checked)
            //				{
            //					cbInhospital.Checked = true;
            //				}
            //				if(!checkBox38.Checked)
            //				{
            //					checkBox38.Checked = true;
            //				}
            //			}
        }

        private void DToutDate_EnabledChanged(object sender, System.EventArgs e)
        {
            //			if(DToutDate.Enabled)
            //			{
            //				if(!cbInhospital.Checked)
            //				{
            //					cbInhospital.Checked = true;
            //				}
            //				if(!checkBox38.Checked)
            //				{
            //					checkBox38.Checked = true;
            //				}
            //			}
        }

        private void cbInWeek_EnabledChanged(object sender, System.EventArgs e)
        {
        }

        #endregion
        #endregion��

        #region ˽�к���
        /// <summary>
        /// �������ϵ����ݱ��浽ʵ����
        /// </summary>
        /// <param name="contralList"> ��ѯ��������</param>
        /// <param name="InpatientNoList">סԺ��ˮ�ż���</param>
        /// <returns>-1 ������� ��-2 û���������� 1 ����</returns>
        private int GetContralInfo(ref ArrayList contralList, ref ArrayList InpatientNoList)
        {
            //������ 
            QueryOper = "";
            //�������� 
            QueryItem = "";
            //����ҽ��
            QueryDoc = "";
            //������� 
            string strDiagNose = "";
            //��Ժ���
            string strInDiagNose = "";
            //��Ժ���
            string OutDiagNose = "";
            //������Ժ���
            string OutDiagNoseIntercurrent = "";
            //�ų� ��Ժ���
            string OutDiagNoseExclude = "";
            //����
            string strOperation = "";
            //��������
            string strOperationIntercurrent = "";
            //�ų�����
            string strOperationExclude = "";
            //����ʽ 
            string strNarcKind = "";
            //������Ϣ��
            string strBase = "";
            //������Ŀ  1; ������Ŀ �ų� 2
            int SpecalItem = 0;
            if (cbSpecalItem.Checked) //������Ŀ
            {
                SpecalItem = 1;
            }
            else if (cbSpecalItem2.Checked) //������Ŀ�ų�
            {
                SpecalItem = 2;
            }
            //��ʼʱ��
            string strBegin = "";
            //����ʱ��
            string strEnd = "";
            FS.FrameWork.Models.NeuObject info = null;
            //����ҽ��
            if (COMSeDoc.Text != "")
            {
                info = new FS.FrameWork.Models.NeuObject();
                info.ID = COMSeDoc.Text;
                info.Name = "COMSeDoc";
                info.User01 = "1";
                QueryDoc = COMSeDoc.Text;
                contralList.Add(info);
            }
            #region ���
            if (diagNose.Checked) //���
            {
                #region ������� ��Ժ���
                if (TXClinicDiag.Enabled) //�������
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = TXClinicDiag.Text;
                    info.Name = "TXClinicDiag";
                    info.User01 = "1";
                    if (QueryItem != "")
                    {
                        QueryItem += "; �������:" + info.ID;
                    }
                    else
                    {
                        QueryItem += " �������:" + info.ID;
                    }
                    contralList.Add(info);

                    string str1 = "";
                    string[] temp = SplitString(info.ID, ref str1);
                    strDiagNose = DepartWhere(temp, str1, " dia.ICD_CODE ", "AND");
                    strDiagNose = "(" + strDiagNose + " and  dia.DIAG_KIND = '10' )";
                }
                if (TXInhisDiag.Enabled) //��Ժ���
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = TXInhisDiag.Text;
                    info.Name = "TXInhisDiag";
                    info.User01 = "1";
                    if (QueryItem != "")
                    {
                        QueryItem += ";��Ժ���:" + info.ID;
                    }
                    else
                    {
                        QueryItem += "��Ժ���:" + info.ID;
                    }
                    contralList.Add(info);

                    string str1 = "";
                    string[] temp = SplitString(info.ID, ref str1);
                    strInDiagNose = DepartWhere(temp, str1, " dia.ICD_CODE ", "AND");
                    strInDiagNose = "(" + strInDiagNose + " and  dia.DIAG_KIND = '11' )";
                }
                if (cbSameIN.Checked) //ͬһ����Ժ
                {
                }
                #endregion
                #region ��Ҫ���
                if (TXIcdDiag.Enabled)
                {
                    //icd-10 
                    if (TXIcdDiag.Text != "")
                    {
                        info = new FS.FrameWork.Models.NeuObject();
                        info.ID = TXIcdDiag.Text;
                        info.Name = "TXIcdDiag";
                        info.User01 = "1";
                        if (QueryItem != "")
                        {
                            QueryItem += ";icd-10 :" + info.ID;
                        }
                        else
                        {
                            QueryItem += "icd-10 :" + info.ID;
                        }

                        contralList.Add(info);

                        string str1 = "";
                        string[] temp = SplitString(info.ID, ref str1);
                        OutDiagNose = DepartWhere(temp, str1, " dia.ICD_CODE ", "AND");
                        OutDiagNose = OutDiagNose + " and  dia.DIAG_KIND != '10' and dia.DIAG_KIND != '11'  ";
                    }

                    if (cbMainDiag1.Checked) //�����
                    {
                        info = new FS.FrameWork.Models.NeuObject();
                        info.ID = "1";
                        info.Name = "cbMainDiag1";
                        info.User01 = "1";
                        if (QueryItem != "")
                        {
                            QueryItem += ";�����";
                        }
                        else
                        {
                            QueryItem += "�����";
                        }

                        contralList.Add(info);
                        if (OutDiagNose != "")
                        {
                            OutDiagNose += " and dia.DIAG_KIND = '1' ";
                        }
                        else
                        {
                            OutDiagNose += "  dia.DIAG_KIND = '1' ";
                        }

                    }
                    if (cbAccord1.Checked) //�������
                    {
                        info = new FS.FrameWork.Models.NeuObject();
                        info.ID = "1";
                        info.Name = "cbAccord1";
                        info.User01 = "1";
                        if (QueryItem != "")
                        {
                            QueryItem += ";�������";
                        }
                        else
                        {
                            QueryItem += " �������";
                        }

                        contralList.Add(info);

                        if (OutDiagNose != "")
                        {
                            OutDiagNose += "and dia.CL_PA = '1'";
                        }
                        else
                        {
                            OutDiagNose += " dia.CL_PA = '1'";
                        }

                    }
                    if (COMOper1.Enabled) //�Ƿ�������
                    {
                        if (COMOper1.Text.IndexOf(',') > 0)
                        {
                            info = new FS.FrameWork.Models.NeuObject();
                            info.ID = COMOper1.Text;
                            info.Name = "COMOper1";
                            info.User01 = "1";
                            if (QueryItem != "")
                            {
                                QueryItem += ";�Ƿ�������";
                            }
                            else
                            {
                                QueryItem += "�Ƿ�������";
                            }
                            contralList.Add(info);

                            string str2 = "";
                            string str = "";
                            string[] temp2 = SplitString(info.ID, ref str2);
                            str = DepartWhere(temp2, str2, " dia.OPERATION_FLAG ", "AND");
                            if (OutDiagNose != "")
                            {
                                OutDiagNose += " and " + str;
                            }
                            else
                            {
                                OutDiagNose += str;
                            }
                        }
                        else if (COMOper1.Tag != null && COMOper1.Text != "")
                        {
                            info = new FS.FrameWork.Models.NeuObject();
                            info.ID = COMOper1.Tag.ToString();
                            info.Name = "COMOper1";
                            info.User01 = "1";
                            if (QueryItem != "")
                            {
                                QueryItem += "�Ƿ�������";
                            }
                            else
                            {
                                QueryItem += "�Ƿ�������";
                            }

                            contralList.Add(info);

                            if (OutDiagNose != "")
                            {
                                OutDiagNose += "and dia.OPERATION_FLAG = '" + info.ID + "'";
                            }
                            else
                            {
                                OutDiagNose += " dia.OPERATION_FLAG = '" + info.ID + "'";

                            }
                        }
                    }
                    if (COMoutState1.Enabled) //��Ժ���
                    {
                        if (COMoutState1.Text.IndexOf(',') > 0)
                        {
                            info = new FS.FrameWork.Models.NeuObject();
                            info.ID = COMoutState1.Text;
                            info.Name = "COMoutState1";
                            info.User01 = "1";
                            if (QueryItem != "")
                            {
                                QueryItem += ";��Ժ��� :" + info.ID;
                            }
                            else
                            {
                                QueryItem += "��Ժ��� :" + info.ID;
                            }

                            contralList.Add(info);

                            string str3 = "";
                            string str = "";
                            string[] temp3 = SplitString(info.ID, ref str3);
                            str = DepartWhere(temp3, str3, " dia.DIAG_OUTSTATE ", "AND");
                            if (OutDiagNose != "")
                            {
                                OutDiagNose += " and " + str;
                            }
                            else
                            {
                                OutDiagNose += str;
                            }
                        }
                        else if (COMoutState1.Tag != null && COMoutState1.Text != "")
                        {
                            info = new FS.FrameWork.Models.NeuObject();
                            info.ID = COMoutState1.Tag.ToString();
                            info.Name = "COMoutState1";
                            info.User01 = "1";
                            if (QueryItem != "")
                            {
                                QueryItem += "" + info.User01 + ":" + info.ID;
                            }
                            else
                            {
                                QueryItem += info.User01 + ":" + info.ID;
                            }

                            contralList.Add(info);
                            if (OutDiagNose != "")
                            {
                                OutDiagNose += " and dia.DIAG_OUTSTATE  = '" + info.ID + "' ";
                            }
                            else
                            {
                                OutDiagNose += " dia.DIAG_OUTSTATE  = '" + info.ID + "' ";

                            }

                        }
                    }
                    if (SingleDisease1.Checked) //�����  û�д���
                    {
                        info = new FS.FrameWork.Models.NeuObject();
                        info.ID = "1";
                        info.Name = "SingleDisease1";
                        info.User01 = "1";
                        if (QueryItem != "")
                        {
                            QueryItem += ";�����";
                        }
                        else
                        {
                            QueryItem += "�����";
                        }

                        contralList.Add(info);

                    }
                    if (OutDiagNose != "")
                    {
                        OutDiagNose = " ( " + OutDiagNose + " ) ";
                    }
                }
                #endregion
                #region �������
                if (TXIcdDiag2.Enabled)
                {
                    if (TXIcdDiag2.Text != "")
                    {
                        info = new FS.FrameWork.Models.NeuObject();
                        info.ID = TXIcdDiag2.Text;
                        info.Name = "TXIcdDiag2";
                        info.User01 = "1";
                        if (QueryItem != "")
                        {
                            QueryItem += ";�������:" + info.ID;
                        }
                        else
                        {
                            QueryItem += "�������:" + info.ID;
                        }

                        contralList.Add(info);

                        string str1 = "";
                        string[] temp = SplitString(info.ID, ref str1);
                        OutDiagNoseIntercurrent = DepartWhere(temp, str1, " diag.ICD_CODE ", "AND");
                        OutDiagNoseIntercurrent = OutDiagNoseIntercurrent + " and  diag.DIAG_KIND != '10' and diag.DIAG_KIND != '11'  ";
                    }
                    if (cbMainDiag2.Checked) //�����
                    {
                        info = new FS.FrameWork.Models.NeuObject();
                        info.ID = "1";
                        info.Name = "cbMainDiag2";
                        info.User01 = "";
                        if (QueryItem != "")
                        {
                            QueryItem += ";�����";
                        }
                        else
                        {
                            QueryItem += "�����";
                        }

                        contralList.Add(info);
                        if (OutDiagNoseIntercurrent != "")
                        {
                            OutDiagNoseIntercurrent += " and diag.DIAG_KIND = '1' ";
                        }
                        else
                        {
                            OutDiagNoseIntercurrent += " diag.DIAG_KIND = '1' ";
                        }
                    }
                    if (cbAccord2.Checked) //�������
                    {
                        info = new FS.FrameWork.Models.NeuObject();
                        info.ID = "1";
                        info.Name = "cbAccord2";
                        info.User01 = "";
                        if (QueryItem != "")
                        {
                            QueryItem += ";�������";
                        }
                        else
                        {
                            QueryItem += "�������";
                        }

                        contralList.Add(info);
                        if (OutDiagNoseIntercurrent != "")
                        {
                            OutDiagNoseIntercurrent += "and diag.CL_PA = '1'";
                        }
                        else
                        {
                            OutDiagNoseIntercurrent += " diag.CL_PA = '1'";
                        }

                    }
                    if (COMOper2.Enabled) //�Ƿ�������
                    {
                        if (COMOper2.Text.IndexOf(',') > 0)
                        {
                            info = new FS.FrameWork.Models.NeuObject();
                            info.ID = COMOper2.Text;
                            info.Name = "COMOper2";
                            info.User01 = "1";
                            contralList.Add(info);
                            if (QueryItem != "")
                            {
                                QueryItem += "�Ƿ�������:" + info.ID;
                            }
                            else
                            {
                                QueryItem += "�Ƿ�������:" + info.ID;
                            }


                            string str4 = "";
                            string str = "";
                            string[] temp4 = SplitString(info.ID, ref str4);
                            str = DepartWhere(temp4, str4, " diag.OPERATION_FLAG ", "AND");
                            if (OutDiagNoseIntercurrent != "")
                            {
                                OutDiagNoseIntercurrent += " and " + str;
                            }
                            else
                            {
                                OutDiagNoseIntercurrent += str;
                            }
                        }
                        else if (COMOper2.Tag != null && COMOper2.Text != "")
                        {
                            info = new FS.FrameWork.Models.NeuObject();
                            info.ID = COMOper2.Tag.ToString();
                            info.Name = "COMOper2";
                            info.User01 = "1";
                            contralList.Add(info);
                            if (QueryItem != "")
                            {
                                QueryItem += "�Ƿ�������:" + info.ID;
                            }
                            else
                            {
                                QueryItem += "�Ƿ�������:" + info.ID;
                            }
                            if (OutDiagNoseIntercurrent != "")
                            {
                                OutDiagNoseIntercurrent += "and diag.OPERATION_FLAG = '" + info.ID + "'";
                            }
                            else
                            {
                                OutDiagNoseIntercurrent += " diag.OPERATION_FLAG = '" + info.ID + "'";
                            }

                        }

                    }
                    if (COMoutState2.Enabled) //��Ժ���
                    {
                        if (COMoutState2.Text.IndexOf(',') > 0)
                        {
                            info = new FS.FrameWork.Models.NeuObject();
                            info.ID = COMoutState2.Text;
                            info.Name = "COMoutState2";
                            info.User01 = "1";
                            contralList.Add(info);
                            if (QueryItem != "")
                            {
                                QueryItem += ";��Ժ���:" + info.ID;
                            }
                            else
                            {
                                QueryItem += "��Ժ���:" + info.ID;
                            }


                            string str1 = "";
                            string str = "";
                            string[] temp = SplitString(info.ID, ref str1);
                            str = DepartWhere(temp, str1, " diag.DIAG_OUTSTATE ", "AND");
                            if (OutDiagNoseIntercurrent != "")
                            {
                                OutDiagNoseIntercurrent += " and " + str;
                            }
                            else
                            {
                                OutDiagNoseIntercurrent += str;
                            }
                        }
                        else if (COMoutState2.Tag != null && COMoutState2.Text != "")
                        {
                            info = new FS.FrameWork.Models.NeuObject();
                            info.ID = COMoutState2.Tag.ToString();
                            info.Name = "COMoutState2";
                            info.User01 = "1";
                            contralList.Add(info);
                            if (QueryItem != "")
                            {
                                QueryItem += ";��Ժ���:" + info.ID;
                            }
                            else
                            {
                                QueryItem += "��Ժ���:" + info.ID;
                            }

                            if (OutDiagNoseIntercurrent != "")
                            {
                                OutDiagNoseIntercurrent += " and diag.DIAG_OUTSTATE  = '" + info.ID + "' ";
                            }
                            else
                            {
                                OutDiagNoseIntercurrent += " diag.DIAG_OUTSTATE  = '" + info.ID + "' ";
                            }

                        }
                    }
                    if (OutDiagNoseIntercurrent != "")
                    {
                        OutDiagNoseIntercurrent = " ( " + OutDiagNoseIntercurrent + " ) ";
                    }
                }
                #endregion
                #region  �ų�ICD���
                if (TXIcdDiag3.Enabled)
                {
                    if (TXIcdDiag3.Text != "")
                    {
                        info = new FS.FrameWork.Models.NeuObject();
                        info.ID = TXIcdDiag3.Text;
                        info.Name = "TXIcdDiag3";
                        info.User01 = "1";
                        contralList.Add(info);
                        if (QueryItem != "")
                        {
                            QueryItem += ";�ų�ICD���:" + info.ID;
                        }
                        else
                        {
                            QueryItem += "�ų�ICD���:" + info.ID;
                        }


                        string str1 = "";
                        string[] temp = SplitString(info.ID, ref str1);
                        OutDiagNoseExclude = DepartWhere(temp, str1, " dia.ICD_CODE ", "DEL");
                        OutDiagNoseExclude = OutDiagNoseExclude + " and  dia.DIAG_KIND != '10' and dia.DIAG_KIND != '11'  ";
                    }
                    if (cbMainDiag3.Checked) //�����
                    {
                        info = new FS.FrameWork.Models.NeuObject();
                        info.ID = "1";
                        info.Name = "cbMainDiag3";
                        info.User01 = "1";
                        contralList.Add(info);
                        if (QueryItem != "")
                        {
                            QueryItem += ";�����";
                        }
                        else
                        {
                            QueryItem += "�����";
                        }

                        if (OutDiagNoseExclude != "")
                        {
                            OutDiagNoseExclude += " and dia.DIAG_KIND = '1' ";
                        }
                        else
                        {
                            OutDiagNoseExclude += " dia.DIAG_KIND = '1' ";
                        }
                    }
                    if (cbAccord1.Checked) //�������
                    {
                        info = new FS.FrameWork.Models.NeuObject();
                        info.ID = "1";
                        info.Name = "cbAccord1";
                        info.User01 = "1";
                        contralList.Add(info);
                        if (QueryItem != "")
                        {
                            QueryItem += ";�������";
                        }
                        else
                        {
                            QueryItem += " �������";
                        }

                        if (OutDiagNoseExclude != "")
                        {
                            OutDiagNoseExclude += "and dia.CL_PA = '1'";
                        }
                        else
                        {
                            OutDiagNoseExclude += " dia.CL_PA = '1'";
                        }
                    }
                    if (COMOper3.Enabled) //�Ƿ�������
                    {
                        if (COMOper3.Text.IndexOf(',') > 0)
                        {
                            info = new FS.FrameWork.Models.NeuObject();
                            info.ID = COMOper3.Text;
                            info.Name = "COMOper3";
                            info.User01 = "1";
                            contralList.Add(info);
                            if (QueryItem != "")
                            {
                                QueryItem += ";�Ƿ�������:" + info.ID;
                            }
                            else
                            {
                                QueryItem += " �Ƿ�������:" + info.ID;
                            }


                            string str1 = "";
                            string str = "";
                            string[] temp = SplitString(info.ID, ref str1);
                            str = DepartWhere(temp, str1, " dia.OPERATION_FLAG ", "AND");
                            if (OutDiagNoseExclude != "")
                            {
                                OutDiagNoseExclude += "and " + str;
                            }
                            else
                            {
                                OutDiagNoseExclude += str;
                            }
                        }
                        else if (COMOper3.Tag != null && COMOper3.Text != "")
                        {
                            info = new FS.FrameWork.Models.NeuObject();
                            info.ID = COMOper3.Tag.ToString();
                            info.Name = "COMOper3";
                            info.User01 = "1";
                            contralList.Add(info);
                            if (QueryItem != "")
                            {
                                QueryItem += ";�Ƿ�������:" + info.ID;
                            }
                            else
                            {
                                QueryItem += "�Ƿ�������:" + info.ID;
                            }

                            if (OutDiagNoseExclude != "")
                            {
                                OutDiagNoseExclude += "and dia.OPERATION_FLAG = '" + info.ID + "'";
                            }
                            else
                            {
                                OutDiagNoseExclude += " dia.OPERATION_FLAG = '" + info.ID + "'";
                            }
                        }
                    }
                    if (COMoutState3.Enabled) //��Ժ���
                    {
                        if (COMoutState3.Text.IndexOf(',') > 0)
                        {
                            info = new FS.FrameWork.Models.NeuObject();
                            info.ID = COMoutState3.Text;
                            info.Name = "COMoutState3";
                            info.User01 = "1";
                            contralList.Add(info);
                            if (QueryItem != "")
                            {
                                QueryItem += ";��Ժ���:" + info.ID;
                            }
                            else
                            {
                                QueryItem += "��Ժ���:" + info.ID;
                            }


                            string str1 = "";
                            string str = "";
                            string[] temp = SplitString(info.ID, ref str1);
                            OutDiagNoseExclude = DepartWhere(temp, str1, " dia.DIAG_OUTSTATE ", "AND");
                            if (OutDiagNoseExclude != "")
                            {
                                OutDiagNoseExclude += " and " + str;
                            }
                            else
                            {
                                OutDiagNoseExclude += str;
                            }
                        }
                        else if (COMoutState3.Tag != null && COMoutState3.Text != "")
                        {
                            info = new FS.FrameWork.Models.NeuObject();
                            info.ID = COMoutState3.Tag.ToString();
                            info.Name = "COMoutState3";
                            info.User01 = "1";
                            contralList.Add(info);
                            if (QueryItem != "")
                            {
                                QueryItem += ";��Ժ���:" + info.ID;
                            }
                            else
                            {
                                QueryItem += "��Ժ���:" + info.ID;
                            }

                            if (OutDiagNoseExclude != "")
                            {
                                OutDiagNoseExclude += " and dia.DIAG_OUTSTATE  = '" + info.ID + "' ";
                            }
                            else
                            {
                                OutDiagNoseExclude += " dia.DIAG_OUTSTATE  = '" + info.ID + "' ";
                            }

                        }

                    }
                    if (SingleDisease2.Checked) //����� û�д���
                    {
                        info = new FS.FrameWork.Models.NeuObject();
                        info.ID = "1";
                        info.Name = "SingleDisease2";
                        info.User01 = "1";
                        contralList.Add(info);
                        if (QueryItem != "")
                        {
                            QueryItem += ";" + info.User01;
                        }
                        else
                        {
                            QueryItem += info.User01;
                        }

                    }
                    if (OutDiagNoseExclude != "")
                    {
                        OutDiagNoseExclude = " ( " + OutDiagNoseExclude + " ) ";
                    }
                }
                #endregion
            }
            #endregion
            #region ����
            if (Operation.Checked)
            {
                #region ����
                if (TXOperation1.Enabled) //����
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = TXOperation1.Text;
                    info.Name = "TXOperation1";
                    info.User01 = "1";
                    contralList.Add(info);
                    if (QueryItem != "")
                    {
                        QueryItem += ";����:" + info.ID;
                    }
                    else
                    {
                        QueryItem += "����:" + info.ID;
                    }


                    string str1 = "";
                    string[] temp = SplitString(info.ID, ref str1);
                    strOperation = DepartWhere(temp, str1, " op.OPERATION_CODE  ", "AND");
                }
                if (COMOperator1.Enabled && COMOperator1.Tag != null)
                {
                    //���ߡ�
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = COMOperator1.Tag.ToString();
                    info.Name = "COMOperator1";
                    info.User01 = "1";
                    contralList.Add(info);
                    if (QueryItem != "")
                    {
                        QueryItem += ";����:" + info.ID;
                    }
                    else
                    {
                        QueryItem += "����:" + info.ID;
                    }

                    if (strOperation != "")
                    {
                        strOperation += " and ( op.FIR_DOCD  = '" + info.ID + "' or  op.FIR_DCODE2  = '" + info.ID + "') ";
                    }
                    else
                    {
                        strOperation += "( op.FIR_DOCD  = '" + info.ID + "' or  op.FIR_DCODE2  = '" + info.ID + "') ";
                    }
                }
                if (COMAnesthetist1.Enabled && COMAnesthetist1.Tag != null)
                {
                    //����ʦ 
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = COMAnesthetist1.Tag.ToString();
                    info.Name = "COMAnesthetist1";
                    info.User01 = "1";
                    contralList.Add(info);
                    if (QueryItem != "")
                    {
                        QueryItem += ";����ʦ:" + info.ID;
                    }
                    else
                    {
                        QueryItem += "����ʦ:" + info.ID;
                    }

                    if (strOperation != "")
                    {
                        strOperation += " and  op.NARC_DOCD  = '" + info.ID + "' ";
                    }
                    else
                    {
                        strOperation += "op.NARC_DOCD  = '" + info.ID + "' ";
                    }
                }
                if (SingleOper1.Checked) //û�д���
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = "1";
                    info.Name = "SingleOper1";
                    info.User01 = "1";
                    contralList.Add(info);
                    if (QueryItem != "")
                    {
                        QueryItem += ";������";
                    }
                    else
                    {
                        QueryItem += "������";
                    }


                }
                if (strOperation != "")
                {
                    strOperation = "(" + strOperation + ")";
                }
                #endregion
                #region  ��������
                if (TXOperation2.Enabled) //����
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = TXOperation2.Text;
                    info.Name = "TXOperation2";
                    info.User01 = "1";
                    contralList.Add(info);
                    if (QueryItem != "")
                    {
                        QueryItem += "��������:" + info.ID;
                    }
                    else
                    {
                        QueryItem += "��������:" + info.ID;
                    }


                    string str1 = "";
                    string[] temp = SplitString(info.ID, ref str1);
                    strOperationIntercurrent = DepartWhere(temp, str1, " op.OPERATION_CODE  ", "AND");
                }
                if (COMOperator2.Enabled && COMOperator2.Tag != null && COMOperator2.Text != "")
                {
                    //���ߡ�
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = COMOperator2.Tag.ToString();
                    info.Name = "COMOperator2";
                    info.User01 = "1";
                    contralList.Add(info);
                    if (QueryItem != "")
                    {
                        QueryItem += ";����:" + info.ID;
                    }
                    else
                    {
                        QueryItem += "����:" + info.ID;
                    }

                    if (strOperationIntercurrent != "")
                    {
                        strOperationIntercurrent += " and ( op.FIR_DOCD  = '" + info.ID + "' or  op.FIR_DCODE2  = '" + info.ID + "') ";
                    }
                    else
                    {
                        strOperationIntercurrent += " ( op.FIR_DOCD  = '" + info.ID + "' or  op.FIR_DCODE2  = '" + info.ID + "') ";
                    }
                }
                if (COMAnesthetist2.Enabled && COMAnesthetist2.Tag != null && COMAnesthetist2.Text != "")
                {
                    //����
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = COMAnesthetist2.Tag.ToString();
                    info.Name = "COMAnesthetist2";
                    info.User01 = "1";
                    contralList.Add(info);
                    if (QueryItem != "")
                    {
                        QueryItem += ";����ʦ:" + info.ID;
                    }
                    else
                    {
                        QueryItem += "����ʦ:" + info.ID;
                    }


                    if (strOperationIntercurrent != "")
                    {
                        strOperationIntercurrent += " and  op.NARC_DOCD  = '" + info.ID + "' ";
                    }
                    else
                    {
                        strOperationIntercurrent += "  op.NARC_DOCD  = '" + info.ID + "' ";
                    }
                }
                if (strOperationIntercurrent != "")
                {
                    strOperationIntercurrent = "(" + strOperationIntercurrent + ")";
                }
                #endregion
                #region �ų�����
                if (TXOperation3.Enabled) //����
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = TXOperation3.Text;
                    info.Name = "TXOperation3";
                    info.User01 = "1";
                    contralList.Add(info);
                    if (QueryItem != "")
                    {
                        QueryItem += ";�ų�����:" + info.ID;
                    }
                    else
                    {
                        QueryItem += "�ų�����:" + info.ID;
                    }


                    string str1 = "";
                    string[] temp = SplitString(info.ID, ref str1);
                    strOperationExclude = DepartWhere(temp, str1, " op.OPERATION_CODE  ", "DEL");
                }
                if (COMOperator3.Enabled && COMOperator3.Tag != null && COMOperator3.Text != "")
                {
                    //���ߡ�
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = COMOperator3.Tag.ToString();
                    info.Name = "COMOperator3";
                    info.User01 = "1";
                    contralList.Add(info);
                    if (QueryItem != "")
                    {
                        QueryItem += ";����:" + info.ID;
                    }
                    else
                    {
                        QueryItem += "����:" + info.ID;
                    }

                    if (strOperationExclude != "")
                    {
                        strOperationExclude += " and ( op.FIR_DOCD  = '" + info.ID + "' or  op.FIR_DCODE2  = '" + info.ID + "') ";
                    }
                    else
                    {
                        strOperationExclude += "  ( op.FIR_DOCD  = '" + info.ID + "' or  op.FIR_DCODE2  = '" + info.ID + "') ";
                    }
                }
                if (COMAnesthetist3.Enabled && COMAnesthetist3.Tag != null && COMAnesthetist3.Text != "")
                {
                    //����
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = COMAnesthetist3.Tag.ToString();
                    info.Name = "COMAnesthetist3";
                    info.User01 = "1";
                    contralList.Add(info);
                    if (QueryItem != "")
                    {
                        QueryItem += ";����ʦ:" + info.ID;
                    }
                    else
                    {
                        QueryItem += "����ʦ:" + info.ID;
                    }

                    if (strOperationExclude != "")
                    {
                        strOperationExclude += " and  op.NARC_DOCD  = '" + info.ID + "' ";
                    }
                    else
                    {
                        strOperationExclude += "  op.NARC_DOCD  = '" + info.ID + "' ";
                    }
                }
                if (SingleOper2.Checked)
                {
                    //������
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = "1";
                    info.Name = "SingleOper2";
                    info.User01 = "1";
                    contralList.Add(info);
                    if (QueryItem != "")
                    {
                        QueryItem += ";������";
                    }
                    else
                    {
                        QueryItem += "������";
                    }

                }
                if (strOperationExclude != "")
                {
                    strOperationExclude = "(" + strOperationExclude + ")";
                }
                #endregion
                #region ����ʽ
                if (COMHocusType.Enabled)
                {
                    if (COMHocusType.Text.IndexOf(',') > 0)
                    {
                        //����ʽ
                        info = new FS.FrameWork.Models.NeuObject();
                        info.ID = COMHocusType.Text;
                        info.Name = "COMHocusType";
                        info.User01 = "1";
                        contralList.Add(info);
                        if (QueryItem != "")
                        {
                            QueryItem += ";����ʽ:" + info.ID;
                        }
                        else
                        {
                            QueryItem += "����ʽ:" + info.ID;
                        }

                        string str1 = "";
                        string[] temp = SplitString(info.ID, ref str1);
                        strNarcKind = DepartWhere(temp, str1, " op.NARC_KIND  ", "AND");
                    }
                    else if (COMHocusType.Tag != null && COMHocusType.Text != "")
                    {
                        //����ʽ
                        info = new FS.FrameWork.Models.NeuObject();
                        info.ID = COMHocusType.Tag.ToString();
                        info.Name = "COMHocusType";
                        info.User01 = "1";
                        contralList.Add(info);
                        if (QueryItem != "")
                        {
                            QueryItem += ";����ʽ:" + info.ID;
                        }
                        else
                        {
                            QueryItem += "����ʽ:" + info.ID;
                        }

                        strNarcKind += " op.NARC_KIND  = '" + info.ID + "' ";
                    }

                }
                #endregion
            }
            #endregion
            #region ������Ϣ
            if (checkBox38.Checked)
            {
                if (ComInNum.Enabled)
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = ComInNum.Text;
                    info.Name = "ComInNum";
                    info.User01 = "1";
                    contralList.Add(info);
                    if (QueryItem != "")
                    {
                        QueryItem += ";��Ժ����:" + info.ID;
                    }
                    else
                    {
                        QueryItem += "��Ժ����:" + info.ID;
                    }


                    if (strBase == "")
                    {
                        strBase += " ba.IN_TIMES = '" + info.ID + "' ";
                    }
                    else
                    {
                        strBase += " and  ba.IN_TIMES = '" + info.ID + "' ";
                    }
                }
                if (COMSexType.Enabled && COMSexType.Tag != null && COMSexType.Text != "")
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = COMSexType.Tag.ToString();
                    info.Name = "COMSexType";
                    info.User01 = "1";
                    contralList.Add(info);
                    if (QueryItem != "")
                    {
                        QueryItem += ";�Ա�:" + info.ID;
                    }
                    else
                    {
                        QueryItem += "�Ա�:" + info.ID;
                    }

                    if (strBase == "")
                    {
                        strBase += " ba.SEX_CODE = '" + info.ID + "' ";
                    }
                    else
                    {
                        strBase += " and  ba.SEX_CODE = '" + info.ID + "' ";
                    }
                }
                if (TXAge.Enabled)
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = TXAge.Text;
                    info.Name = "TXAge";
                    info.User01 = "1";
                    contralList.Add(info);
                    if (QueryItem != "")
                    {
                        QueryItem += ";����:" + info.ID;
                    }
                    else
                    {
                        QueryItem += "����:" + info.ID;
                    }

                    if (strBase == "")
                    {
                        strBase += " ba.AGE = '" + info.ID + "' ";
                    }
                    else
                    {
                        strBase += " and  ba.AGE = '" + info.ID + "' ";
                    }
                }
                if (COMAgeType.Enabled)
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = COMSexType.Tag.ToString();
                    info.Name = "COMSexType";
                    info.User01 = "1";
                    contralList.Add(info);
                    if (QueryItem != "")
                    {
                        QueryItem += ";���䵥λ:" + info.ID;
                    }
                    else
                    {
                        QueryItem += "���䵥λ:" + info.ID;
                    }

                    if (strBase == "")
                    {
                        strBase += " ba.AGE_UNIT = '" + info.ID + "' ";
                    }
                    else
                    {
                        strBase += " and  ba.AGE_UNIT = '" + info.ID + "' ";
                    }
                }
                if (TXHomeZip.Enabled)
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = TXHomeZip.Text;
                    info.Name = "TXHomeZip";
                    info.User01 = "1";
                    contralList.Add(info);
                    if (QueryItem != "")
                    {
                        QueryItem += ";��ͥ�ʱ�:" + info.ID;
                    }
                    else
                    {
                        QueryItem += "��ͥ�ʱ�:" + info.ID;
                    }

                    if (strBase == "")
                    {
                        strBase += " ba.HOME_ZIP = '" + info.ID + "' ";
                    }
                    else
                    {
                        strBase += " and  ba.HOME_ZIP = '" + info.ID + "' ";
                    }
                }
                if (COMDepartment.Enabled)
                {
                    if (COMDepartment.Text.IndexOf(',') > 0)
                    {
                        info = new FS.FrameWork.Models.NeuObject();
                        info.ID = COMDepartment.Text;
                        info.Name = "COMDepartment";
                        info.User01 = "1";
                        contralList.Add(info);
                        if (QueryItem != "")
                        {
                            QueryItem += ";��Ժ�Ʊ�:" + info.ID;
                        }
                        else
                        {
                            QueryItem += "��Ժ�Ʊ�:" + info.ID;
                        }

                        string str1 = "";
                        string str = "";
                        string[] temp = SplitString(info.ID, ref str1);
                        str = DepartWhere(temp, str1, " ba.DEPT_CODE ", "AND");
                        if (strBase == "")
                        {
                            strBase = str;
                        }
                        else
                        {
                            strBase += " and " + str;
                        }
                    }
                    else if (COMDepartment.Tag != null && COMDepartment.Text != "")
                    {
                        info = new FS.FrameWork.Models.NeuObject();
                        info.ID = COMDepartment.Tag.ToString();
                        info.Name = "COMDepartment";
                        info.User01 = "1";
                        contralList.Add(info);
                        if (QueryItem != "")
                        {
                            QueryItem += ";��Ժ�Ʊ�:" + info.ID;
                        }
                        else
                        {
                            QueryItem += "��Ժ�Ʊ�:" + info.ID;
                        }

                        if (strBase == "")
                        {
                            strBase += " ba.DEPT_CODE = '" + info.ID + "' ";
                        }
                        else
                        {
                            strBase += " and  ba.DEPT_CODE = '" + info.ID + "' ";
                        }
                    }
                }
                if (COMInAvenue.Enabled && COMInAvenue.Tag != null)
                {
                    if (COMInAvenue.Text.IndexOf(',') > 0)
                    {
                        info = new FS.FrameWork.Models.NeuObject();
                        info.ID = COMInAvenue.Text;
                        info.Name = "COMInAvenue";
                        info.User01 = "1";
                        contralList.Add(info);
                        if (QueryItem != "")
                        {
                            QueryItem += ";��Դ:" + info.ID;
                        }
                        else
                        {
                            QueryItem += "��Դ:" + info.ID;
                        }

                        string str1 = "";
                        string str = "";
                        string[] temp = SplitString(info.ID, ref str1);
                        str = DepartWhere(temp, str1, " ba.IN_AVENUE ", "AND");
                        if (strBase != "")
                        {
                            strBase += " and " + str;
                        }
                        else
                        {
                            strBase = str;
                        }
                    }
                    else if (COMInAvenue.Tag != null && COMInAvenue.Text != "")
                    {
                        info = new FS.FrameWork.Models.NeuObject();
                        info.ID = COMInAvenue.Tag.ToString();
                        info.Name = "COMInAvenue";
                        info.User01 = "1";
                        contralList.Add(info);
                        if (QueryItem != "")
                        {
                            QueryItem += ";��Դ:" + info.ID;
                        }
                        else
                        {
                            QueryItem += "��Դ:" + info.ID;
                        }

                        if (strBase == "")
                        {
                            strBase += " ba.IN_AVENUE = '" + info.ID + "' ";
                        }
                        else
                        {
                            strBase += " and  ba.IN_AVENUE = '" + info.ID + "' ";
                        }
                    }
                }
                if (COMInState.Enabled)
                {
                    if (COMInState.Text.IndexOf(',') > 0)
                    {
                        info = new FS.FrameWork.Models.NeuObject();
                        info.ID = COMInState.Text;
                        info.Name = "COMInState";
                        info.User01 = "1";
                        contralList.Add(info);
                        if (QueryItem != "")
                        {
                            QueryItem += ";��Ժ���:" + info.ID;
                        }
                        else
                        {
                            QueryItem += "��Ժ���:" + info.ID;
                        }

                        string str1 = "";
                        string str = "";
                        string[] temp = SplitString(info.ID, ref str1);
                        str = DepartWhere(temp, str1, " ba.IN_CIRCS ", "AND");
                        if (strBase != "")
                        {
                            strBase += " and " + str;
                        }
                        else
                        {
                            strBase = str;
                        }
                    }
                    else if (COMInState.Tag != null && COMInState.Text != "")
                    {
                        info = new FS.FrameWork.Models.NeuObject();
                        info.ID = COMInState.Tag.ToString();
                        info.Name = "COMInState";
                        info.User01 = "1";
                        contralList.Add(info);
                        if (QueryItem != "")
                        {
                            QueryItem += ";��Ժ���:" + info.ID;
                        }
                        else
                        {
                            QueryItem += "��Ժ���:" + info.ID;
                        }

                        if (strBase == "")
                        {
                            strBase += " ba.IN_CIRCS = '" + info.ID + "' ";
                        }
                        else
                        {
                            strBase += " and  ba.IN_CIRCS = '" + info.ID + "' ";
                        }
                    }
                }
                if (TXWorkZip.Enabled)
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = TXWorkZip.Text;
                    info.Name = "TXWorkZip";
                    info.User01 = "1";
                    contralList.Add(info);
                    if (QueryItem != "")
                    {
                        QueryItem += ";������λ�ʱ�:" + info.ID;
                    }
                    else
                    {
                        QueryItem += "������λ�ʱ�:" + info.ID;
                    }

                    if (strBase == "")
                    {
                        strBase += " ba.WORK_ZIP  = '" + info.ID + "' ";
                    }
                    else
                    {
                        strBase += " and  ba.WORK_ZIP  = '" + info.ID + "' ";
                    }
                }
                if (COMDoctor.Enabled && COMDoctor.Tag != null && COMDoctor.Text != "")
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = COMDoctor.Tag.ToString();
                    info.Name = "COMDoctor";
                    info.User01 = "1";
                    contralList.Add(info);
                    if (QueryItem != "")
                    {
                        QueryItem += ";ǩ��ҽʦ:" + info.ID;
                    }
                    else
                    {
                        QueryItem += "ǩ��ҽʦ:" + info.ID;
                    }


                    string strtempSql = " ba.HOUSE_DOC_CODE = '{0}' or ba.CHARGE_DOC_CODE  = '{0}'  or ba.CHIEF_DOC_CODE = '{0}'  or ba.DEPT_CHIEF_DOCD = '{0}'  or ba.REFRESHER_DOCD = '{0}' or ba.GRA_DOC_CODE   = '{0}' or ba.PRA_DOC_CODE   = '{0}' ";
                    strtempSql = string.Format(strtempSql, info.ID);
                    if (strBase == "")
                    {
                        strBase += strtempSql;
                    }
                    else
                    {
                        strBase += " and " + strtempSql;
                    }
                }
                if (DTInDate.Enabled && DTInDate.Text != "")
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = DTInDate.Text;
                    info.Name = "DTInDate";
                    info.User01 = "1";
                    contralList.Add(info);
                    if (QueryItem != "")
                    {
                        QueryItem += ";����סԺ����:" + info.ID;
                    }
                    else
                    {
                        QueryItem += "����סԺ����:" + info.ID;
                    }

                    string strtempSql = " ba.PI_DAYS  >= {0}";
                    strtempSql = string.Format(strtempSql, info.ID);
                    if (strBase == "")
                    {
                        strBase += strtempSql;
                    }
                    else
                    {
                        strBase += " and " + strtempSql;
                    }
                }
                if (DToutDate.Enabled && DToutDate.Text != "")
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = DToutDate.Text;
                    info.Name = "DToutDate";
                    info.User01 = "1";
                    contralList.Add(info);
                    if (QueryItem != "")
                    {
                        QueryItem += ";���סԺ����:" + info.ID;
                    }
                    else
                    {
                        QueryItem += "���סԺ����:" + info.ID;
                    }


                    string strtempSql = " ba.PI_DAYS  <= {0}";
                    strtempSql = string.Format(strtempSql, info.ID);
                    if (strBase == "")
                    {
                        strBase += strtempSql;
                    }
                    else
                    {
                        strBase += " and " + strtempSql;
                    }
                }
                if (CHInfection.Checked)
                {
                    string strTemp = "";
                    if (strBase == "")
                    {
                        strBase += strTemp;
                    }
                    else
                    {
                        strBase += " and " + strTemp;
                    }
                }
            }
            #endregion
            #region  ������Ŀ
            if (cbSpecalItem.Checked || cbSpecalItem2.Checked)
            {
                if (cbSpecalItem.Checked)
                {
                    QueryItem += ";������Ŀ" + ":";
                }
                else if (cbSpecalItem2.Checked)
                {
                    QueryItem += ";������Ŀ(�ų�)" + ":";
                }
                if (CHSyndrome1.Checked)
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = "1";
                    info.Name = "CHSyndrome1";
                    info.User01 = "1";
                    contralList.Add(info);
                    QueryItem += "����֢";
                    if (strBase != "")
                    {
                        if (cbSpecalItem.Checked)
                        {
                            strBase += "  ba.DIAG_SYNDROME  = '1' ";
                        }
                        else
                        {
                            strBase += "  ba.DIAG_SYNDROME  = '0' ";
                        }
                    }
                    else
                    {
                        if (cbSpecalItem.Checked)
                        {
                            strBase += " ba.DIAG_SYNDROME  = '1' ";
                        }
                        else
                        {
                            strBase += "  ba.DIAG_SYNDROME  = '0' ";
                        }
                    }
                }
                if (CHInfection.Checked) //Ժ�ڸ�Ⱦ���� 
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = "1";
                    info.Name = "CHInfection";
                    info.User01 = "1";
                    contralList.Add(info);
                    QueryItem += ";Ժ�ڸ�Ⱦ";
                    if (strBase != "")
                    {
                        if (cbSpecalItem.Checked)
                        {
                            strBase += " and ba.INFECTION_NUM >= 0 ";
                        }
                        else
                        {
                            strBase += " and ba.INFECTION_NUM  = 0 ";
                        }
                    }
                    else
                    {
                        if (cbSpecalItem.Checked)
                        {
                            strBase += " ba.INFECTION_NUM  >= 0 ";
                        }
                        else
                        {
                            strBase += "  ba.INFECTION_NUM  = 0 ";
                        }
                    }
                }
                if (CHCT.Checked)
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = "1";
                    info.Name = "CHCT";
                    info.User01 = "";
                    contralList.Add(info);
                    QueryItem += ";CT";
                    #region CT
                    if (SpecalItem == 1) //���������Ŀ
                    {
                        if (strBase == "")
                        {
                            strBase += " ba.CT_NUMB !=''";
                        }
                        else
                        {
                            strBase += " and ba.CT_NUMB !='' ";
                        }
                    }
                    else //���������Ŀ�ų�
                    {
                        if (strBase == "")
                        {
                            strBase += " ba.CT_NUMB =''";
                        }
                        else
                        {
                            strBase += " and ba.CT_NUMB ='' ";
                        }
                    }
                    #endregion
                }
                if (CHUFCT.Checked)
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = "1";
                    info.Name = "CHUFCT";
                    info.User01 = "1";
                    contralList.Add(info);
                    QueryItem += ";UFCT";
                    #region UFCT
                    if (SpecalItem == 1) //���������Ŀ
                    {
                        if (strBase == "")
                        {
                            strBase += " ba.PATH_NUMB !=''";
                        }
                        else
                        {
                            strBase += " and ba.PATH_NUMB !='' ";
                        }
                    }
                    else //���������Ŀ�ų�
                    {
                        if (strBase == "")
                        {
                            strBase += " ba.PATH_NUMB =''";
                        }
                        else
                        {
                            strBase += " and ba.PATH_NUMB ='' ";
                        }
                    }
                    #endregion
                }
                if (CHMR.Checked)
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = "1";
                    info.Name = "CHMR";
                    info.User01 = "1";
                    contralList.Add(info);
                    QueryItem += ";MR";
                    #region MR
                    if (SpecalItem == 1) //���������Ŀ
                    {
                        if (strBase == "")
                        {
                            strBase += " ba.MR_TIMES !=''";
                        }
                        else
                        {
                            strBase += " and ba.MR_TIMES !='' ";
                        }
                    }
                    else //���������Ŀ�ų�
                    {
                        if (strBase == "")
                        {
                            strBase += " ba.MR_TIMES =''";
                        }
                        else
                        {
                            strBase += " and ba.MR_TIMES ='' ";
                        }
                    }
                    #endregion
                }
                if (CHXG.Checked)
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = "1";
                    info.Name = "CHXG";
                    info.User01 = "1";
                    contralList.Add(info);
                    QueryItem += ";X��";
                    #region X��
                    if (SpecalItem == 1) //���������Ŀ
                    {
                        if (strBase == "")
                        {
                            strBase += " ba.X_NUMB !=''";
                        }
                        else
                        {
                            strBase += " and ba.X_NUMB !='' ";
                        }
                    }
                    else //���������Ŀ�ų�
                    {
                        if (strBase == "")
                        {
                            strBase += " ba.X_NUMB =''";
                        }
                        else
                        {
                            strBase += " and ba.X_NUMB ='' ";
                        }
                    }
                    #endregion
                }
                if (CHBC.Checked)
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = "1";
                    info.Name = "CHBC";
                    info.User01 = "1";
                    contralList.Add(info);
                    QueryItem += ";B��";
                    #region B��
                    if (SpecalItem == 1) //���������Ŀ
                    {
                        if (strBase == "")
                        {
                            strBase += " ba.DSA_NUMB !=''";
                        }
                        else
                        {
                            strBase += " and ba.DSA_NUMB !='' ";
                        }
                    }
                    else //���������Ŀ�ų�
                    {
                        if (strBase == "")
                        {
                            strBase += " ba.DSA_NUMB =''";
                        }
                        else
                        {
                            strBase += " and ba.DSA_NUMB ='' ";
                        }
                    }
                    #endregion
                }

            }
            #endregion
            #region ʱ������
            strBegin = DTBeginTime.Value.ToString();
            strEnd = DTEndTime.Value.ToString();
            #endregion
            string diagFlag = GetDiagFlag(strDiagNose, strInDiagNose, OutDiagNose, OutDiagNoseIntercurrent, OutDiagNoseExclude);
            string operFlag = GetOperFlag(strOperation, strOperationIntercurrent, strOperationExclude, strNarcKind);
            //����Ա 
            info = new FS.FrameWork.Models.NeuObject();
            info.ID = SearchMana.Operator.Name;
            info.Name = "OPERNAME";
            info.User01 = "1";
            contralList.Add(info);
            QueryOper = info.ID;
            if (contralList.Count == 1)
            {
                //ȱ�ٲ�ѯ���� 
                return -2;
            }
            InpatientNoList = GetInpatientNO(strBase, diagFlag, operFlag, strBegin, strEnd);
            return 1;
        }
        /// <summary>
        /// ��ѯסԺ��ˮ�� 
        /// </summary>
        /// <param name="strBase"></param>
        /// <param name="diagFlag"></param>
        /// <param name="operFlag"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        private ArrayList GetInpatientNO(string strBase, string diagFlag, string operFlag, string BeginDate, string EndTime)
        {
            ArrayList arrlist = new ArrayList();
            //סԺ��������
            string BaseInpatient = "";
            //��ϱ�����
            string DiagINpatientNo = "";
            string TimeFlag1 = ""; //ʱ������
            string TimeFlag2 = ""; //ʱ������
            string TimeFlag3 = ""; //ʱ������
            #region ʱ������
            if (BeginDate != "")
            {
                TimeFlag1 = " OUT_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')";
                TimeFlag1 = string.Format(TimeFlag1, BeginDate);

                TimeFlag2 = " dia.OUT_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')";
                TimeFlag2 = string.Format(TimeFlag2, BeginDate);

                TimeFlag3 = " op.OUT_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')";
                TimeFlag3 = string.Format(TimeFlag3, BeginDate);
            }
            if (TimeFlag1 != "")
            {
                TimeFlag1 += "and OUT_DATE <= to_date('{0}','yyyy-mm-dd hh24:mi:ss')";
                TimeFlag1 = string.Format(TimeFlag1, EndTime);

                TimeFlag2 += "and dia.OUT_DATE <= to_date('{0}','yyyy-mm-dd hh24:mi:ss')";
                TimeFlag2 = string.Format(TimeFlag2, EndTime);

                TimeFlag3 += "and op.OUT_DATE <= to_date('{0}','yyyy-mm-dd hh24:mi:ss')";
                TimeFlag3 = string.Format(TimeFlag3, EndTime);
            }
            else
            {
                TimeFlag1 += " OUT_DATE <= to_date('{0}','yyyy-mm-dd hh24:mi:ss')";
                TimeFlag1 = string.Format(TimeFlag1, EndTime);

                TimeFlag2 += " OUT_DATE <= to_date('{0}','yyyy-mm-dd hh24:mi:ss')";
                TimeFlag2 = string.Format(TimeFlag2, EndTime);

                TimeFlag3 += " OUT_DATE <= to_date('{0}','yyyy-mm-dd hh24:mi:ss')";
                TimeFlag3 = string.Format(TimeFlag3, EndTime);
            }
            #endregion
            #region ��ѯ����

            if (string.IsNullOrEmpty(strBase))
            {
                strBase = " 1=1 ";
            }

            if (strBase != "" || (strBase == "" && diagFlag == "" && operFlag == "" && TimeFlag1 != ""))
            {
                string strSql1 = SearchMana.GetSelectSql(TablesName.BASE);
                strSql1 += " and " + strBase;
                if (TimeFlag1 != "")
                {
                    strSql1 += " and " + TimeFlag1;
                }
                //�������в�ѯ���������ļ�¼ 
                ArrayList list1 = SearchMana.GetInpatientNoList(strSql1, TablesName.BASE);

                #region  ��ͬһ����Ժ
                if (!cbSameIN.Checked) //��ͬһ����Ժ
                {
                    //��ȡ�ַ��� �ö��Ÿ��� 
                    BaseInpatient = ChangeInfo(list1);

                    string strSqlSub = SearchMana.GetSelectSql(TablesName.BASESUB);
                    if (strSqlSub == null)
                    {
                        MessageBox.Show("��ȡSQL���ʧ��");
                        return null;
                    }
                    strSqlSub = string.Format(strSqlSub, BaseInpatient);
                    //�������в�ѯ���������ļ�¼ 
                    list1 = SearchMana.GetInpatientNoList(strSql1, TablesName.BASESUB);
                }
                #endregion

                if ((diagFlag == "" && operFlag == "") || (list1.Count == 0)) //��Ҫ����Ϻ����� ֱ�ӷ���  ����û�з���������
                {
                    return list1;
                }
                else
                {
                    if (list1 == null)
                    {
                        return null;
                    }
                    BaseInpatient = "";
                    foreach (FS.FrameWork.Models.NeuObject obj in list1)
                    {
                        if (BaseInpatient == "")
                        {
                            BaseInpatient = "'" + obj.ID + "'";
                        }
                        else
                        {
                            BaseInpatient += ",'" + obj.ID + "'";
                        }
                    }
                }
            }
            #endregion
            #region ��ѯ��ϱ�
            if (diagFlag != "")
            {
                string strSql2 = SearchMana.GetSelectSql(TablesName.DIAG);
                if (BaseInpatient != "")
                {
                    strSql2 += " and " + diagFlag + "and dia.INPATIENT_NO in (" + BaseInpatient + ")";
                }
                else
                {
                    strSql2 += " and " + diagFlag;
                }
                if (TimeFlag2 != "")
                {
                    strSql2 += " and " + TimeFlag2;
                }
                if (SingleDisease2.Checked || SingleDisease2.Checked)
                {
                    string SingleSql = SearchMana.GetSelectSql(TablesName.DIAGSINGLE);
                    strSql2 = SingleSql + " and op.inpatient_no in ( " + strSql2 + " )";
                }
                ArrayList list2 = SearchMana.GetInpatientNoList(strSql2, TablesName.DIAG);

                #region  ��ͬһ����Ժ
                if (!cbSameIN.Checked) //��ͬһ����Ժ
                {
                    //��ȡ�ַ��� �ö��Ÿ��� 
                    DiagINpatientNo = ChangeInfo(list2);

                    string strSqlSub = SearchMana.GetSelectSql(TablesName.BASESUB);
                    if (strSqlSub == null)
                    {
                        MessageBox.Show("��ȡSQL���ʧ��");
                        return null;
                    }
                    if (strSqlSub != "")
                    {

                        strSqlSub = string.Format(strSqlSub, DiagINpatientNo);
                        //�������в�ѯ���������ļ�¼ 
                        list2 = SearchMana.GetInpatientNoList(strSqlSub, TablesName.BASESUB);
                    }
                    else
                    {
                        list2 = new ArrayList();
                    }
                }
                #endregion

                if (list2.Count == 0 || operFlag == "")
                {
                    return list2;
                }
                else
                {
                    if (list2 == null)
                    {
                        return null;
                    }
                    DiagINpatientNo = "";
                    foreach (FS.FrameWork.Models.NeuObject obj in list2)
                    {
                        if (DiagINpatientNo == "")
                        {
                            DiagINpatientNo = "'" + obj.ID + "'";
                        }
                        else
                        {
                            DiagINpatientNo += ",'" + obj.ID + "'";
                        }
                    }
                }
            }
            #endregion
            #region ��ѯ������
            if (operFlag != "")
            {
                string strSql3 = SearchMana.GetSelectSql(TablesName.OPERATION);
                if (DiagINpatientNo != "")
                {
                    strSql3 += " and " + operFlag + "and op.INPATIENT_NO in (" + DiagINpatientNo + ")";
                }
                else if (BaseInpatient != "")
                {
                    strSql3 += " and " + operFlag + "and op.INPATIENT_NO in (" + BaseInpatient + ")";
                }
                else
                {
                    strSql3 += " and " + operFlag;
                }
                if (TimeFlag3 != "")
                {
                    strSql3 += " and " + TimeFlag3;
                }
                if (SingleOper2.Checked || SingleOper2.Checked)
                {
                    string SingleSql = SearchMana.GetSelectSql(TablesName.OPERATIONSINGLE);
                    strSql3 = SingleSql + " and op.inpatient_no in (" + strSql3 + ")";
                }
                ArrayList list3 = SearchMana.GetInpatientNoList(strSql3, TablesName.DIAG);
                #region  ��ͬһ����Ժ
                if (!cbSameIN.Checked) //��ͬһ����Ժ
                {
                    //��ȡ�ַ��� �ö��Ÿ��� 
                    string tempStr = ChangeInfo(list3);

                    string strSqlSub = SearchMana.GetSelectSql(TablesName.BASESUB);
                    if (strSqlSub == null)
                    {
                        MessageBox.Show("��ȡSQL���ʧ��");
                        return null;
                    }
                    strSqlSub = string.Format(strSqlSub, tempStr);
                    //�������в�ѯ���������ļ�¼ 
                    list3 = SearchMana.GetInpatientNoList(strSqlSub, TablesName.BASESUB);
                }
                #endregion
                return list3;
            }
            #endregion
            return arrlist;
        }
        private string ChangeInfo(ArrayList list)
        {
            if (list == null)
            {
                MessageBox.Show("ת��������Ϊ��");
                return null;
            }
            string strReturn = "";
            foreach (FS.FrameWork.Models.NeuObject obj in list)
            {
                if (strReturn == "")
                {
                    strReturn = "'" + obj.ID + "'";
                }
                else
                {
                    strReturn += ",'" + obj.ID + "'";
                }
            }
            return strReturn;
        }
        /// <summary>
        /// �������յ�SQL����
        /// </summary>
        /// <param name="list"></param>
        /// <param name="strSql"></param>
        /// <returns></returns>
        private int CreateSqlWhere(ArrayList list, ref string strSql)
        {
            //			//�ۺ�where���
            //			string strWhere = "";
            //			//���յģӣѣ���䡡
            //			string strSql = "";
            //������� 
            string strDiagNose = "";
            //��Ժ���
            string strInDiagNose = "";
            //��Ժ���
            string OutDiagNose = "";
            //������Ժ���
            string OutDiagNoseIntercurrent = "";
            //�ų� ��Ժ���
            string OutDiagNoseExclude = "";
            //����
            string strOperation = "";
            //��������
            string strOperationIntercurrent = "";
            //�ų�����
            string strOperationExclude = "";
            //������Ϣ��
            string strBase = "";
            //��ѯ��ʱ��� 
            string OutDateSql = "";
            //������Ŀ  1; ������Ŀ �ų� 2 
            int SpecalItem = IsExistItem(list);
            string NarcKind = "";
            foreach (FS.FrameWork.Models.NeuObject obj in list)
            {
                #region ���
                #region �������
                if (obj.Name == "TXClinicDiag") //�������
                {
                    string str1 = "";
                    string[] temp = SplitString(obj.ID, ref str1);
                    strDiagNose = DepartWhere(temp, str1, " dia.ICD_CODE ", "AND");
                    if (strDiagNose != "" && strDiagNose != null)
                    {
                        strDiagNose = " ( " + strDiagNose + " and dia.DIAG_KIND = '10' " + " ) ";
                    }
                }
                #endregion
                #region ��Ժ���
                else if (obj.Name == "TXInhisDiag") //��Ժ���
                {
                    string str1 = "";
                    string[] temp = SplitString(obj.ID, ref str1);
                    strInDiagNose = DepartWhere(temp, str1, " dia.ICD_CODE ", "AND");
                    if (strInDiagNose != "" && strInDiagNose != null)
                    {
                        strInDiagNose = " ( " + strInDiagNose + " and dia.DIAG_KIND = '11' " + " ) ";
                    }
                }
                #endregion
                #region  ��Ժ���
                else if (obj.Name == "TXIcdDiag")
                {
                    string str1 = "";
                    string[] temp = SplitString(obj.ID, ref str1);
                    OutDiagNose = DepartWhere(temp, str1, " dia.ICD_CODE ", "AND");
                }
                else if (obj.Name == "cbMainDiag1")
                {
                    OutDiagNose += " and dia.DIAG_KIND = '1' ";
                }
                else if (obj.Name == "cbAccord1")
                {
                    OutDiagNose += "and dia.CL_PA = '1'";
                }
                else if (obj.Name == "COMOper1")
                {
                    OutDiagNose += " and dia.OPERATION_FLAG = '1' ";
                }
                else if (obj.Name == "COMoutState1")
                {
                    OutDiagNose += " and dia.DIAG_OUTSTATE  = '" + obj.ID + "' ";
                }
                #endregion
                #region  ������Ժ���
                else if (obj.Name == "TXIcdDiag2")
                {
                    string str1 = "";
                    string[] temp = SplitString(obj.ID, ref str1);
                    OutDiagNoseIntercurrent = DepartWhere(temp, str1, " dia.ICD_CODE ", "AND");
                }
                else if (obj.Name == "cbMainDiag2")
                {
                    OutDiagNoseIntercurrent += " and dia.DIAG_KIND = '1' ";
                }
                else if (obj.Name == "cbAccord2")
                {
                    OutDiagNoseIntercurrent += "and dia.CL_PA = '1'";
                }
                else if (obj.Name == "COMOper2")
                {
                    OutDiagNoseIntercurrent += " and dia.OPERATION_FLAG = '1' ";
                }
                else if (obj.Name == "COMoutState2")
                {
                    OutDiagNoseIntercurrent += " and dia.DIAG_OUTSTATE  = '" + obj.ID + "' ";
                }
                #endregion
                #region �ų� ��Ժ���
                else if (obj.Name == "TXIcdDiag3")
                {
                    string str1 = "";
                    string[] temp = SplitString(obj.ID, ref str1);
                    OutDiagNoseExclude = DepartWhere(temp, str1, " dia.ICD_CODE ", "DEL");
                }
                else if (obj.Name == "cbMainDiag3")
                {
                    OutDiagNoseExclude += " and dia.DIAG_KIND = '1' ";
                }
                else if (obj.Name == "cbAccord3")
                {
                    OutDiagNoseExclude += "and dia.CL_PA = '1'";
                }
                else if (obj.Name == "COMOper3")
                {
                    OutDiagNoseExclude += " and dia.OPERATION_FLAG = '1' ";
                }
                else if (obj.Name == "COMoutState3")
                {
                    OutDiagNoseExclude += " and dia.DIAG_OUTSTATE  = '" + obj.ID + "' ";
                }
                #endregion
                else if (obj.Name == "CHSyndrome1")
                {
                    #region  �������
                    if (SpecalItem == 0)
                    {
                        MessageBox.Show("��ȡ��Ϣ����,����ȷ����С��Ŀ������С��Ŀ�ų�");
                        return -1;
                    }
                    else if (SpecalItem == 2) //���������Ŀ�ų�
                    {
                        if (strBase == "")
                        {
                            OutDiagNose += " dia.DIAG_KIND !='3'";
                        }
                        else
                        {
                            OutDiagNose += " and dia.CT_NUMB !='3' ";
                        }
                    }
                    else if (SpecalItem == 1) //���������Ŀ
                    {
                        if (strBase == "")
                        {
                            OutDiagNose += " dia.CT_NUMB ='3'";
                        }
                        else
                        {
                            OutDiagNose += " and dia.CT_NUMB ='3' ";
                        }
                    }
                    #endregion
                }
                else if (obj.Name == "CHInfection")
                {
                    #region  �������
                    if (SpecalItem == 0)
                    {
                        MessageBox.Show("��ȡ��Ϣ����,����ȷ����С��Ŀ������С��Ŀ�ų�");
                        return -1;
                    }
                    else if (SpecalItem == 2) //���������Ŀ�ų�
                    {
                        if (strBase == "")
                        {
                            OutDiagNose += " dia.DIAG_KIND !='4'";
                        }
                        else
                        {
                            OutDiagNose += " and dia.CT_NUMB !='4' ";
                        }
                    }
                    else if (SpecalItem == 1) //���������Ŀ
                    {
                        if (strBase == "")
                        {
                            OutDiagNose += " dia.CT_NUMB ='4'";
                        }
                        else
                        {
                            OutDiagNose += " and dia.CT_NUMB ='4' ";
                        }
                    }
                    #endregion
                }
                #endregion
                #region ����
                #region ����
                else if (obj.Name == "TXOperation1")
                {
                    string str1 = "";
                    string[] temp = SplitString(obj.ID, ref str1);
                    strOperation = DepartWhere(temp, str1, " op.OPERATION_CODE  ", "AND");
                }
                else if (obj.Name == "COMOperator1")
                {
                    strOperation += " and ( op.FIR_DOCD  = '" + obj.ID + "' or  op.FIR_DCODE2  = '" + obj.ID + "') ";
                }
                else if (obj.Name == "COMAnesthetist1")
                {
                    strOperation += " and  op.NARC_DOCD  = '" + obj.ID + "' ";
                }
                else if (obj.Name == "COMHocusType")
                {
                    strOperation += " and  op.NARC_KIND  = '" + obj.ID + "' ";
                }
                #endregion
                #region  ��������
                else if (obj.Name == "TXOperation2")
                {
                    string str1 = "";
                    string[] temp = SplitString(obj.ID, ref str1);
                    strOperationIntercurrent = DepartWhere(temp, str1, " op.OPERATION_CODE  ", "AND");
                }
                else if (obj.Name == "COMOperator2")
                {
                    strOperationIntercurrent += " and ( op.FIR_DOCD  = '" + obj.ID + "' or  op.FIR_DCODE2  = '" + obj.ID + "') ";
                }
                else if (obj.Name == "COMAnesthetist2")
                {
                    strOperationIntercurrent += " and  op.NARC_DOCD  = '" + obj.ID + "' ";
                }
                #endregion
                #region  �ų�����
                else if (obj.Name == "TXOperation3")
                {
                    string str1 = "";
                    string[] temp = SplitString(obj.ID, ref str1);
                    strOperationExclude = DepartWhere(temp, str1, " op.OPERATION_CODE  ", "DEL");
                }
                else if (obj.Name == "COMOperator3")
                {
                    strOperationExclude += " and ( op.FIR_DOCD  = '" + obj.ID + "' or  op.FIR_DCODE2  = '" + obj.ID + "') ";
                }
                else if (obj.Name == "COMAnesthetist3")
                {
                    strOperationExclude += " and  op.NARC_DOCD  = '" + obj.ID + "' ";
                }
                else if (obj.Name == "COMHocusType")
                {
                    strOperation += " and  op.NARC_KIND  = '" + obj.ID + "' ";
                }
                #endregion
                #endregion
                #region ������Ϣ��
                else if (obj.Name == "ComInNum")
                {
                    if (strBase == "")
                    {
                        strBase += " ba.IN_TIMES = '" + obj.ID + "' ";
                    }
                    else
                    {
                        strBase += " and  ba.IN_TIMES = '" + obj.ID + "' ";
                    }
                }
                else if (obj.Name == "COMSexType")
                {
                    if (strBase == "")
                    {
                        strBase += " ba.SEX_CODE = '" + obj.ID + "' ";
                    }
                    else
                    {
                        strBase += " and  ba.SEX_CODE = '" + obj.ID + "' ";
                    }
                }
                else if (obj.Name == "TXAge")
                {
                    if (strBase == "")
                    {
                        strBase += " ba.AGE = '" + obj.ID + "' ";
                    }
                    else
                    {
                        strBase += " and  ba.AGE = '" + obj.ID + "' ";
                    }
                }
                else if (obj.Name == "COMAgeType")
                {
                    if (strBase == "")
                    {
                        strBase += " ba.AGE_UNIT = '" + obj.ID + "' ";
                    }
                    else
                    {
                        strBase += " and  ba.AGE_UNIT = '" + obj.ID + "' ";
                    }
                }
                else if (obj.Name == "TXHomeZip")
                {
                    if (strBase == "")
                    {
                        strBase += " ba.HOME_ZIP = '" + obj.ID + "' ";
                    }
                    else
                    {
                        strBase += " and  ba.HOME_ZIP = '" + obj.ID + "' ";
                    }
                }
                else if (obj.Name == "COMDepartment")
                {
                    if (strBase == "")
                    {
                        strBase += " ba.DEPT_CODE = '" + obj.ID + "' ";
                    }
                    else
                    {
                        strBase += " and  ba.DEPT_CODE = '" + obj.ID + "' ";
                    }
                }
                else if (obj.Name == "COMInAvenue")
                {
                    if (strBase == "")
                    {
                        strBase += " ba.IN_AVENUE = '" + obj.ID + "' ";
                    }
                    else
                    {
                        strBase += " and  ba.IN_AVENUE = '" + obj.ID + "' ";
                    }
                }
                else if (obj.Name == "COMInState")
                {
                    if (strBase == "")
                    {
                        strBase += " ba.IN_CIRCS = '" + obj.ID + "' ";
                    }
                    else
                    {
                        strBase += " and  ba.IN_CIRCS = '" + obj.ID + "' ";
                    }
                }
                else if (obj.Name == "TXWorkZip")
                {
                    if (strBase == "")
                    {
                        strBase += " ba.LINKMA_NAME = '" + obj.ID + "' ";
                    }
                    else
                    {
                        strBase += " and  ba.LINKMA_NAME = '" + obj.ID + "' ";
                    }
                }
                else if (obj.Name == "COMDoctor")
                {
                    string strtempSql = " ba.HOUSE_DOC_CODE = '{0}' or ba.CHARGE_DOC_CODE  = '{0}'  or ba.CHIEF_DOC_CODE = '{0}'  or ba.DEPT_CHIEF_DOCD = '{0}'  or ba.REFRESHER_DOCD = '{0}' or ba.GRA_DOC_CODE   = '{0}' or ba.PRA_DOC_CODE   = '{0}' ";
                    strtempSql = string.Format(strtempSql, obj.ID);
                    if (strBase == "")
                    {
                        strBase += strtempSql;
                    }
                    else
                    {
                        strBase += " and " + strtempSql;
                    }
                }
                #region ������Ŀ
                else if (obj.Name == "CHCT")
                {
                    #region CT
                    if (SpecalItem == 0)
                    {
                        MessageBox.Show("��ȡ��Ϣ����,����ȷ����С��Ŀ������С��Ŀ�ų�");
                        return -1;
                    }
                    else if (SpecalItem == 1) //���������Ŀ
                    {
                        if (strBase == "")
                        {
                            strBase += " ba.CT_NUMB !=''";
                        }
                        else
                        {
                            strBase += " and ba.CT_NUMB !='' ";
                        }
                    }
                    else //���������Ŀ�ų�
                    {
                        if (strBase == "")
                        {
                            strBase += " ba.CT_NUMB =''";
                        }
                        else
                        {
                            strBase += " and ba.CT_NUMB ='' ";
                        }
                    }
                    #endregion
                }
                else if (obj.Name == "CHUFCT")
                {
                    #region UFCT
                    if (SpecalItem == 0)
                    {
                        MessageBox.Show("��ȡ��Ϣ����,����ȷ����С��Ŀ������С��Ŀ�ų�");
                        return -1;
                    }
                    else if (SpecalItem == 1) //���������Ŀ
                    {
                        if (strBase == "")
                        {
                            strBase += " ba.PATH_NUMB !=''";
                        }
                        else
                        {
                            strBase += " and ba.PATH_NUMB !='' ";
                        }
                    }
                    else //���������Ŀ�ų�
                    {
                        if (strBase == "")
                        {
                            strBase += " ba.PATH_NUMB =''";
                        }
                        else
                        {
                            strBase += " and ba.PATH_NUMB ='' ";
                        }
                    }
                    #endregion
                }
                else if (obj.Name == "CHMR")
                {
                    #region MR
                    if (SpecalItem == 0)
                    {
                        MessageBox.Show("��ȡ��Ϣ����,����ȷ����С��Ŀ������С��Ŀ�ų�");
                        return -1;
                    }
                    else if (SpecalItem == 1) //���������Ŀ
                    {
                        if (strBase == "")
                        {
                            strBase += " ba.MR_TIMES !=''";
                        }
                        else
                        {
                            strBase += " and ba.MR_TIMES !='' ";
                        }
                    }
                    else //���������Ŀ�ų�
                    {
                        if (strBase == "")
                        {
                            strBase += " ba.MR_TIMES =''";
                        }
                        else
                        {
                            strBase += " and ba.MR_TIMES ='' ";
                        }
                    }
                    #endregion
                }
                else if (obj.Name == "CHXG")
                {
                    #region X��
                    if (SpecalItem == 0)
                    {
                        MessageBox.Show("��ȡ��Ϣ����,����ȷ����С��Ŀ������С��Ŀ�ų�");
                        return -1;
                    }
                    else if (SpecalItem == 1) //���������Ŀ
                    {
                        if (strBase == "")
                        {
                            strBase += " ba.X_NUMB !=''";
                        }
                        else
                        {
                            strBase += " and ba.X_NUMB !='' ";
                        }
                    }
                    else //���������Ŀ�ų�
                    {
                        if (strBase == "")
                        {
                            strBase += " ba.X_NUMB =''";
                        }
                        else
                        {
                            strBase += " and ba.X_NUMB ='' ";
                        }
                    }
                    #endregion
                }
                else if (obj.Name == "CHBC") //B��
                {
                    #region X��
                    if (SpecalItem == 0)
                    {
                        MessageBox.Show("��ȡ��Ϣ����,����ȷ����С��Ŀ������С��Ŀ�ų�");
                        return -1;
                    }
                    else if (SpecalItem == 1) //���������Ŀ
                    {
                        if (strBase == "")
                        {
                            strBase += " ba.DSA_NUMB !=''";
                        }
                        else
                        {
                            strBase += " and ba.DSA_NUMB !='' ";
                        }
                    }
                    else //���������Ŀ�ų�
                    {
                        if (strBase == "")
                        {
                            strBase += " ba.DSA_NUMB =''";
                        }
                        else
                        {
                            strBase += " and ba.DSA_NUMB ='' ";
                        }
                    }
                    #endregion
                }
                #endregion

                #endregion
            }
            string diagFlag = GetDiagFlag(strDiagNose, strInDiagNose, OutDiagNose, OutDiagNoseIntercurrent, OutDiagNoseExclude);
            string operFlag = GetOperFlag(strOperation, strOperationIntercurrent, strOperationExclude, NarcKind);
            //����������� 
            OutDateSql = OutDate(diagFlag, operFlag, strBase, list);
            #region  ������յ�where����
            if (strBase == "" && OutDateSql == "" && diagFlag == "" && operFlag == "")
            {
                MessageBox.Show("��ѡ���ѯ������");
                return -1;
            }
            else if (strBase != "" && diagFlag == "" && operFlag == "")
            {
                //��ѯ������ҳ 
                strSql = SearchMana.GetSelectSql(TablesName.BASE);
                if (strSql != "" && strSql != null)
                {
                    strSql = strSql + " and  " + strBase;
                }
                else
                {
                    MessageBox.Show("��ȡSQL���ʧ��" + SearchMana.Err);
                    return -1;
                }
            }
            else if (strBase == "" && diagFlag != "" && operFlag == "")
            {
                //��ѯ��ϱ�
                strSql = SearchMana.GetSelectSql(TablesName.DIAG);
                if (strSql != "" && strSql != null)
                {
                    strSql = strSql + " and  " + diagFlag;
                }
                else
                {
                    MessageBox.Show("��ȡSQL���ʧ��" + SearchMana.Err);
                    return -1;
                }
            }
            else if (strBase == "" && diagFlag == "" && operFlag != "")
            {
                //��ѯ������
                strSql = SearchMana.GetSelectSql(TablesName.OPERATION);
                if (strSql != "" && strSql != null)
                {
                    strSql = strSql + " and " + operFlag;
                }
                else
                {
                    MessageBox.Show("��ȡSQL���ʧ��" + SearchMana.Err);
                    return -1;
                }
            }
            else if (strBase != "" && diagFlag != "" && operFlag == "")
            {
                //��ѯ�������ϱ�
                strSql = SearchMana.GetSelectSql(TablesName.BASEANDDIAG);
                if (strSql != "" && strSql != null)
                {
                    strSql = strSql + " and " + strBase + " and " + diagFlag;
                }
                else
                {
                    MessageBox.Show("��ȡSQL���ʧ��" + SearchMana.Err);
                    return -1;
                }
            }
            else if (strBase != "" && diagFlag == "" && operFlag != "")
            {
                //��ѯ�����������
                strSql = SearchMana.GetSelectSql(TablesName.BASEANDOPERATION);
                if (strSql != "" && strSql != null)
                {
                    strSql = strSql + " and " + strBase + " and " + operFlag;
                }
                else
                {
                    MessageBox.Show("��ȡSQL���ʧ��" + SearchMana.Err);
                    return -1;
                }
            }
            else if (strBase == "" && diagFlag != "" && operFlag != "")
            {
                //��ѯ��ϱ��������
                strSql = SearchMana.GetSelectSql(TablesName.DIAGANDOPERATION);
                if (strSql != "" && strSql != null)
                {
                    strSql = strSql + " and " + diagFlag + " and " + operFlag;
                }
                else
                {
                    MessageBox.Show("��ȡSQL���ʧ��" + SearchMana.Err);
                    return -1;
                }
            }
            else if (strBase != "" && diagFlag != "" && operFlag != "")
            {
                //��ѯ��ϱ�������� ��������
                strSql = SearchMana.GetSelectSql(TablesName.BASEANDDIAGANDOPERATION);
                if (strSql != "" && strSql != null)
                {
                    strSql = strSql + " and " + strBase + " and " + diagFlag + " and " + operFlag;
                }
                else
                {
                    MessageBox.Show("��ȡSQL���ʧ��" + SearchMana.Err);
                    return -1;
                }
            }
            else if (strBase == "" && diagFlag == "" && operFlag == "" && OutDateSql != "")
            {
                //��ѯ������ҳ 
                strSql = SearchMana.GetSelectSql(TablesName.BASE);
                if (strSql != "" && strSql != null)
                {
                }
                else
                {
                    MessageBox.Show("��ȡSQL���ʧ��" + SearchMana.Err);
                    return -1;
                }
            }

            strSql = strSql + " and " + OutDateSql;
            #endregion
            return 1;
        }
        /// <summary>
        /// ����ѡ��  ��ѡ 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDept_Click(object sender, System.EventArgs e)
        {
            string Result = "";
            if (deptList == null)
            {
                deptList = new ArrayList();
            }
            ArrayList templist = null;
            templist = FS.FrameWork.WinForms.Classes.Function.ChooseMultiObject(deptList);
            if (templist != null)
            {
                foreach (FS.HISFC.Models.Base.Department obj in templist)
                {
                    if (Result != "")
                    {
                        Result = Result + "," + obj.ID;
                    }
                    else
                    {
                        Result = Result + obj.ID;
                    }
                }
            }
            COMDepartment.Text = Result;
        }
        private void ucCaseQuery_Load(object sender, System.EventArgs e)
        {
            InitInfo();
        }
        /// <summary>
        /// ����������亯��  
        /// </summary>
        /// <param name="diagflag"></param>
        /// <param name="operflag"></param>
        /// <param name="baseflag"></param>
        /// <param name="arrList"></param>
        /// <returns></returns>
        private string OutDate(string diagflag, string operflag, string baseflag, ArrayList arrList)
        {
            string strBegin = "";
            string strEnd = "";
            #region  ��ȡ����
            foreach (FS.FrameWork.Models.NeuObject obj in arrList)
            {
                if (obj.Name == "DTBeginTime") //��ʼʱ��  
                {
                    if (baseflag != "")
                    {
                        strBegin = " ba.OUT_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')";
                    }
                    else if (diagflag != "")
                    {
                        strBegin = " dia.OUT_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')";
                    }
                    else if (operflag != "")
                    {
                        strBegin = " op.OUT_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')";
                    }
                    else
                    {
                        strBegin = " ba.OUT_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')";
                    }
                    if (strBegin != "")
                    {
                        strBegin = string.Format(strBegin, obj.ID);
                    }
                }
                else if (obj.Name == "DTEndTime") //����ʱ��
                {
                    if (baseflag != "")
                    {
                        strEnd = " ba.OUT_DATE <= to_date('{0}','yyyy-mm-dd hh24:mi:ss')";
                    }
                    else if (diagflag != "")
                    {
                        strEnd = " dia.OUT_DATE <= to_date('{0}','yyyy-mm-dd hh24:mi:ss')";
                    }
                    else if (operflag != "")
                    {
                        strEnd = " op.OUT_DATE <= to_date('{0}','yyyy-mm-dd hh24:mi:ss')";
                    }
                    else
                    {
                        strEnd = " ba.OUT_DATE <= to_date('{0}','yyyy-mm-dd hh24:mi:ss')";
                    }
                    if (strEnd != "")
                    {
                        strEnd = string.Format(strEnd, obj.ID);
                    }
                }

            }
            #endregion
            if (strEnd != "" && strBegin != "")
            {
                strBegin = strBegin + " and " + strEnd;
                return strBegin;
            }
            else if (strEnd == "" && strBegin != "")
            {
                return strBegin;
            }
            else if (strEnd != "" && strBegin == "")
            {
                return strEnd;
            }
            return "";

        }
        private int InitInfo()
        {
            try
            {
                FS.HISFC.BizLogic.Manager.Person person = new FS.HISFC.BizLogic.Manager.Person();
                //��ȡҽ���б�
                ArrayList DoctorList = person.GetEmployeeAll();
                COMSeDoc.AppendItems(DoctorList);
                COMDoctor.AppendItems(DoctorList);
                COMOperator1.AppendItems(DoctorList);//������
                COMOperator2.AppendItems(DoctorList);//������
                COMOperator3.AppendItems(DoctorList);//������
                COMAnesthetist1.AppendItems(DoctorList);//����ʦ
                COMAnesthetist2.AppendItems(DoctorList);//����ʦ
                COMAnesthetist3.AppendItems(DoctorList);//����ʦ
                //���������б�
                FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();
                deptList = dept.GetInHosDepartment();
                COMDepartment.AppendItems(deptList);
                //�������
                FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
                //��Ժ���
                ArrayList CircsList = con.GetList(FS.HISFC.Models.Base.EnumConstant.INCIRCS);
                COMInState.AppendItems(CircsList);

                FS.HISFC.BizLogic.HealthRecord.Base baseDml = new FS.HISFC.BizLogic.HealthRecord.Base();
                ArrayList InAvenuelist = con.GetAllList(FS.HISFC.Models.Base.EnumConstant.INAVENUE);
                COMInAvenue.AddItems(InAvenuelist);

                ArrayList list = FS.HISFC.Models.Base.SexEnumService.List();
                COMSexType.AppendItems(list);
                //�ӳ������л�ȡ��������
                ArrayList AnestypeList = con.GetList("ANESTYPE");
                COMHocusType.AppendItems(AnestypeList);
                //��Ժ����б�
                ArrayList diagOutStateList = con.GetList(FS.HISFC.Models.Base.EnumConstant.ZG);
                COMoutState1.AppendItems(diagOutStateList); //ת��
                COMoutState2.AppendItems(diagOutStateList);//ת��
                COMoutState3.AppendItems(diagOutStateList);//ת��
                FS.HISFC.BizLogic.HealthRecord.Diagnose dia = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
                ArrayList OpList = con.GetList(FS.HISFC.Models.Base.EnumConstant.OPERATIONTYPE);// dia.GetDiagOperType();
                COMOper1.AppendItems(OpList);
                COMOper2.AppendItems(OpList);
                COMOper3.AppendItems(OpList);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 1;
        }
        private int SearchInfo()
        {

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(SearchMana.Connection);
            try
            {
                //���ȴӽ����ϻ�ȡ���� ,�ж����ݵ��߼�
                //Ȼ���ѯ
                //���Ҫ�󱣴���
                //��ѯ�ӣѣ����
                ArrayList infoList = new ArrayList();
                ArrayList InpatientNoList = new ArrayList();
                #region ��ѯ ��������������� סԺ��ˮ��
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ���ݣ����Ժ�...");
                //�ӽ����ϻ�ȡ����
                int i = GetContralInfo(ref infoList, ref InpatientNoList);

                if (i == -1)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("��ȡ��ѯ��������");
                    return -1;
                }
                if (i == -2)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("����д��ѯ����");
                    return -1;
                }
                //�������
                frmSequenceNo = SearchMana.GetSequence("Case.SearchManager.GetSequence1");
                string SysTime = SearchMana.GetSysDate();
                //��ʼ����

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                //t.BeginTransaction();

                SearchMana.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                //��ȡ��ѯ��������ء�סԺ��ˮ�š���
                if (InpatientNoList == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("��ѯ����: " + SearchMana.Err);
                    return -1;
                }
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                if (InpatientNoList.Count == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("û�в鵽��ؼ�¼");
                    return -1;
                }

                #endregion
                #region �����ѯ����
                #region ������ϸ
                foreach (FS.FrameWork.Models.NeuObject obj in infoList)
                {
                    obj.User02 = frmSequenceNo; //���
                    obj.User03 = SysTime; //ʱ��
                    if (SearchMana.InsertContralDetail(obj) <= 0)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�����ѯ����ʧ��:" + SearchMana.Err);
                        return -1;
                    }
                }
                #endregion
                #region ����ϲ���Ľ��
                ArrayList HisList = GetItemList(SysTime, InpatientNoList.Count.ToString());
                foreach (FS.FrameWork.Models.NeuObject obj in HisList)
                {
                    if (SearchMana.InsertContralDetail(obj) <= 0)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�����ѯ����ʧ��:" + SearchMana.Err);
                        return -1;
                    }
                }
                #endregion
                //�����ѯ��¼
                int TotalNum = InpatientNoList.Count;
                string HappenNO = SearchMana.GetSequence("Case.SearchManager.GetHappenNo");
                FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
                info.ID = frmSequenceNo; //���\
                info.Name = HappenNO; //�������
                info.User02 = SysTime; //ʱ��
                info.User01 = ""; //��ʼ��
                foreach (FS.FrameWork.Models.NeuObject obj in InpatientNoList)
                {
                    if (info.User01.Length + 17 <= 4000)
                    {
                        if (info.User01 == "")
                        {
                            info.User01 = obj.ID;
                        }
                        else
                        {
                            info.User01 = info.User01 + "," + obj.ID;
                        }
                    }
                    else
                    {
                        info.User01 = "'" + info.User01 + "'";
                        //�����ѯ�����Ľ��
                        if (SearchMana.InsertResult(info) <= 0)
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("�����ѯ���ʧ��" + SearchMana.Err);
                            return -1;
                        }
                        info.User01 = "";
                    }
                }
                //����¼���ĺ�û�� 4000���ַ����Ȼ� ��4000ȡģ������ʱ ������������
                if (info.User01.Length > 0)
                {
                    //�����ѯ�����Ľ��
                    if (SearchMana.InsertResult(info) <= 0)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�����ѯ���ʧ��" + SearchMana.Err);
                        return -1;
                    }
                }
                #endregion
                #region ����ɹ�
                FS.FrameWork.Management.PublicTrans.Commit();
                #endregion

                #region ��ʾ���
                System.Windows.Forms.DialogResult Result = MessageBox.Show("�鵽" + InpatientNoList.Count.ToString() + "����¼.�Ƿ����ڲ쿴", "��ʾ", MessageBoxButtons.YesNo);
                if (Result == DialogResult.Yes)
                {
                    frmShowResult frm = new frmShowResult();
                    frm.InpatientNoList = GetInpatienNoStr(InpatientNoList);
                    frm.TopMost = false;
                    frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                    frm.ShowDialog();
                }
                #endregion

            }
            catch (Exception ex)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// ��ȡסԺ��ˮ���ַ������ö��ź͵����Ÿ���
        /// </summary>
        /// <param name="tempList"></param>
        /// <returns></returns>
        private string GetInpatienNoStr(ArrayList tempList)
        {
            if (tempList == null)
            {
                return "";
            }
            string InpatientList = "";
            foreach (FS.FrameWork.Models.NeuObject obj in tempList)
            {
                if (InpatientList != "")
                {
                    InpatientList = InpatientList + " , '" + obj.ID + "'";
                }
                else
                {
                    InpatientList = InpatientList + "'" + obj.ID + "'";
                }
            }
            return InpatientList;
        }
        private ArrayList GetItemList(string strTime, string SumNun)
        {
            //���ﱣ������� ������ʾ��ʷ��ѯ 
            ArrayList list = new ArrayList();
            if (QueryOper == "" && QueryItem == "" && QueryDoc == "" && SumNun == "0")
            {
                return list;
            }
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.User02 = frmSequenceNo; //��� 
            obj.ID = strTime; //ֵ
            obj.Name = "����"; //��ʾ����
            obj.User01 = "2"; // 1 ��ϸ ��2 ����ʷ��ѯ
            obj.User03 = strTime; //����ʱ�� 
            list.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.User02 = frmSequenceNo; //��� 
            obj.ID = SearchMana.Operator.Name; //ֵ
            obj.Name = "������"; //��ʾ����
            obj.User01 = "2"; // 1 ��ϸ ��2 ����ʷ��ѯ
            obj.User03 = strTime; //����ʱ�� 
            list.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.User02 = frmSequenceNo; //��� 
            obj.ID = QueryItem; //ֵ
            obj.Name = "��������"; //��ʾ����
            obj.User01 = "2"; // 1 ��ϸ ��2 ����ʷ��ѯ
            obj.User03 = strTime; //����ʱ�� 
            list.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.User02 = frmSequenceNo; //��� 
            obj.ID = SumNun.ToString(); //ֵ
            obj.Name = "��¼��"; //��ʾ����
            obj.User01 = "2"; // 1 ��ϸ ��2 ����ʷ��ѯ
            obj.User03 = strTime; //����ʱ�� 
            list.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.User02 = frmSequenceNo; //��� 
            obj.ID = QueryDoc; //ֵ
            obj.Name = "����ҽ��"; //��ʾ����
            obj.User01 = "2"; // 1 ��ϸ ��2 ����ʷ��ѯ
            obj.User03 = strTime; //����ʱ�� 
            list.Add(obj);

            return list;
        }
        /// <summary>
        /// �ж���������Ŀ����������Ŀ�ų�
        /// </summary>
        /// <returns></returns>
        private int IsExistItem(ArrayList tempList)
        {
            int m = 0;
            foreach (FS.FrameWork.Models.NeuObject obj in tempList)
            {
                if (obj.Name == "cbSpecalItem")
                {
                    m = 1;
                    break;
                }
                else if (obj.Name == "cbSpecalItem2")
                {
                    m = 2;
                    break;
                }
            }
            return m;
        }


        private string GetOperFlag(string opFlag, string opInter, string opExclude, string NarcKind)
        {
            string OperationStr = "";
            OperationStr = opFlag;
            if (OperationStr != "" && opInter != "")
            {
                OperationStr = OperationStr + " and " + opInter;
            }
            else if (OperationStr == "" && opInter != "")
            {
                OperationStr = opInter;
            }

            if (OperationStr != "" && opExclude != "")
            {
                OperationStr = OperationStr + " and " + opExclude;
            }
            else if (OperationStr == "" && opExclude != "")
            {
                OperationStr = opExclude;
            }

            if (OperationStr != "" && NarcKind != "")
            {
                OperationStr = OperationStr + " and " + NarcKind;
            }
            else
            {
                OperationStr = NarcKind;
            }
            return OperationStr;
        }
        /// <summary>
        /// ��֯��ϵ�SQL
        /// </summary>
        /// <param name="cliDiagFlag"></param>
        /// <param name="InDiagFlag"></param>
        /// <param name="OutDiag"></param>
        /// <param name="OutDiagNoseInter"></param>
        /// <param name="OutDiagNose"></param>
        /// <returns></returns>
        private string GetDiagFlag(string cliDiagFlag, string InDiagFlag, string OutDiag, string OutDiagNoseInter, string OutDiagNose)
        {
            string DiagStr = "";
            DiagStr = cliDiagFlag;
            if (DiagStr != "" && InDiagFlag != "")
            {
                DiagStr = DiagStr + " and  " + InDiagFlag;
            }
            else if (DiagStr == "" && InDiagFlag != "")
            {
                DiagStr = InDiagFlag;
            }

            if (DiagStr != "" && OutDiag != "")
            {
                DiagStr = DiagStr + " and " + OutDiag;
            }
            else if (DiagStr == "" && OutDiag != "")
            {
                DiagStr = OutDiag;
            }

            if (DiagStr != "" && OutDiagNoseInter != "")
            {
                DiagStr = DiagStr + " and " + OutDiagNoseInter;
            }
            else if (DiagStr == "" && OutDiagNoseInter != "")
            {
                DiagStr = OutDiagNoseInter;
            }

            if (DiagStr != "" && OutDiagNose != "")
            {
                DiagStr = DiagStr + " and " + OutDiagNose;
            }
            else if (DiagStr == "" && OutDiagNose != "")
            {
                DiagStr = OutDiagNoseInter;
            }
            return DiagStr;
        }

        /// <summary>
        /// �ֽ� "~" �� "," �����ַ�������
        /// </summary>
        /// <param name="StringStr">��Ҫ�����ַ�����</param>
        /// <param name="strFlag">�ָ�����</param>
        /// <param name="columns">��Ӧ���е��ֶ�</param>
        /// <param name="flagCompare">DEL ���ų�ĳЩ��  AND ���߻� ����</param>
        /// <returns> ������Ϻõ��ַ���</returns>
        private string DepartWhere(string[] StringStr, string strFlag, string columns, string flagCompare)
        {
            //�����ж��ַ������ÿ�����Ƿ��С�~���� ��������� ���������ٲ�� �����û�� ���� flagCompare��

            //���طֽ��� �ַ���  
            string SplitStr = "";
            string strCompare = "";

            if (strFlag == ",") //���� �� 
            {
                #region �ö��Ÿ��� �������� ~��
                for (int i = 0; i < StringStr.Length; i++)
                {
                    if (StringStr[i].IndexOf('~') > 0) //����� "~"�� ������"~"
                    {
                        string[] detailStr = StringStr[i].Split('~');
                        if (SplitStr != "")
                        {
                            if (flagCompare == "AND")
                            {
                                SplitStr = SplitStr + "( " + columns + " >= '" + detailStr[0].ToString() + "'" + " and " + columns + " <= '" + detailStr[1].ToString() + "'" + ")";
                            }
                            else if (flagCompare == "DEL")
                            {
                                SplitStr = SplitStr + " ( " + columns + " <= '" + detailStr[0].ToString() + "'" + " and " + columns + " >= '" + detailStr[1].ToString() + "'" + ")";
                            }
                            if (i < StringStr.Length - 1)
                            {
                                SplitStr += " and ";
                            }
                            continue;
                        }
                        else
                        {
                            if (flagCompare == "AND")
                            {
                                SplitStr = SplitStr + " ( " + columns + " >= '" + detailStr[0].ToString() + "'" + " and " + columns + " <= '" + detailStr[1].ToString() + "'" + ")";
                            }
                            else if (flagCompare == "DEL")
                            {
                                SplitStr = SplitStr + " ( " + columns + " <= '" + detailStr[0].ToString() + "'" + " and " + columns + " >= '" + detailStr[1].ToString() + "'" + ")";
                            }
                            if (i < StringStr.Length - 1)
                            {
                                SplitStr += " and ";
                            }
                            continue;
                        }
                    }
                    if (flagCompare == "AND")
                    {
                        strCompare = " = ";
                    }
                    else if (flagCompare == "DEL")
                    {
                        strCompare = " != ";
                    }

                    SplitStr = SplitStr + columns + strCompare + " '" + StringStr[i].ToString() + "'";
                    if (i < StringStr.Length - 1)
                    {
                        SplitStr += " or ";
                    }
                }
                #endregion
            }
            else if (strFlag == "~") //�������
            {
                #region ��~�Ÿ���
                if (flagCompare == "AND")
                {
                    SplitStr = columns + " >= '" + StringStr[0].ToString() + "'" + " and " + columns + " <= '" + StringStr[1].ToString() + "'";
                }
                else if (flagCompare == "DEL")
                {
                    SplitStr = columns + " <= '" + StringStr[0].ToString() + "'" + " and " + columns + " >= '" + StringStr[1].ToString() + "'";
                }
                #endregion
            }
            else if (strFlag == "") //ֻ��һ���ʱ�� 
            {
                if (flagCompare == "AND")
                {
                    SplitStr = columns + " = '" + StringStr[0].ToString() + "'";
                }
                else if (flagCompare == "DEL")
                {
                    SplitStr = columns + " != '" + StringStr[0].ToString() + "'";
                }
            }
            if (strFlag != "") //����������� ��������������
            {
                SplitStr = "(" + SplitStr + ")";
            }
            return SplitStr;
        }
        /// <summary>
        /// �����ַ��� 
        /// </summary>
        /// <param name="Str"></param>
        /// <param name="flagStr"></param>
        /// <returns></returns>
        private string[] SplitString(string Str, ref string flagStr)
        {
            string[] temStr;
            if (Str.IndexOf(',') > 0)
            {
                temStr = Str.Split(',');
                flagStr = ",";
            }
            else if (Str.IndexOf('~') > 0)
            {
                temStr = Str.Split('~');
                flagStr = "~";
            }
            else
            {
                temStr = new string[1];
                temStr[0] = Str;
                flagStr = "";
            }
            return temStr;
        }

        /// <summary>
        /// �ر�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbClose_Click_1(object sender, System.EventArgs e)
        {
            this.FindForm().Close();
        }
        /// <summary>
        /// ��ʷ���ڡ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbhistory_Click_1(object sender, System.EventArgs e)
        {

        }��
        #endregion
       
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                System.Windows.Forms.SendKeys.Send("{tab}");
            }
            return base.ProcessDialogKey(keyData);
        }��
    }
}
