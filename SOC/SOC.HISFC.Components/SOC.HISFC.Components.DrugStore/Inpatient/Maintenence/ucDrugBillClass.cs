using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Pharmacy;
using FS.FrameWork.Models;

namespace FS.SOC.HISFC.Components.DrugStore.Inpatient.Maintenence
{
    /// <summary>
    /// [�ؼ�����:ucDrugBill]<br></br>
    /// [��������: ��ҩ������]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11-10]<br></br>
    /// <�޸ļ�¼>
    ///     2011-03 ���� by cube
    /// </�޸ļ�¼>
    ///  />
    /// </summary>
    public partial class ucDrugBillClass : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDrugBillClass( )
        {
            InitializeComponent( );
        }

        #region ����

        //��ҩ������ʵ����
        private DrugBillClass drugBillClassInfo;
        //ҩ��������
        private FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore( ); 

        #endregion

        #region ����

        #region ��ҩ������

        /// <summary>
        /// �жϴ����ҩ����Ϣ�Ƿ���Ч
        /// </summary>
        /// <param name="drugBillClass">��ҩ����Ϣ</param>
        /// <returns>�ɹ�����True  ʧ�ܷ���False</returns>
        private bool IsDrugBillDataValid(DrugBillClass drugBillClass)
        {
            if (!FS.FrameWork.Public.String.ValidMaxLengh(drugBillClass.Memo, 150))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ע�ֶγ���"));
                return false;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(drugBillClass.Name, 30))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ҩ�����Ƴ���"));
                return false;
            }

            foreach (ListViewItem lv in this.lvPutDrugBill1.Items)
            {
                DrugBillClass tempDrugBill = lv.Tag as DrugBillClass;
                if (tempDrugBill == null)
                {
                    continue;
                }

                if (!FS.FrameWork.Public.String.ValidMaxLengh(tempDrugBill.Memo, 150))
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ע�ֶγ���"));
                    return false;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(tempDrugBill.Name, 30))
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ҩ�����Ƴ���"));
                    return false;
                }

                if (tempDrugBill.ID == drugBillClass.ID)
                {
                    continue;
                }                

                if (lv.Text == drugBillClass.Name)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg(drugBillClass.Name + "��ҩ�������ظ� ������ά��"));
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ��tabpage2����ȡ���ݣ�������myDrugBillClass�С�
        /// </summary>
        private DrugBillClass GetDrugBillItem( )
        {
            if( this.drugBillClassInfo == null )
            {
                drugBillClassInfo = new DrugBillClass( );
            }

            this.drugBillClassInfo.Name = this.txtName.Text;                       //��ҩ��������
            this.drugBillClassInfo.PrintType.ID = this.cbxPrinttype.Tag;           //��ӡ����
            this.drugBillClassInfo.DrugAttribute.ID = this.cbxPutType.Tag.ToString(); //��ҩ����
            this.drugBillClassInfo.IsValid = this.cbxIsValid.Checked;              //�Ƿ���Ч
            this.drugBillClassInfo.Memo = this.txtMark.Text;               //��ע
            return this.drugBillClassInfo;
        }

        /// <summary>
        /// ��myDrugBillClass��ȡ���ݣ���ʾ��tabpage2�С�
        /// </summary>
        private void SetDrugBillItem( DrugBillClass drugbill )
        {
            this.drugBillClassInfo = drugbill;

            this.txtName.Text = this.drugBillClassInfo.Name;          //��ҩ��������
            this.cbxPrinttype.Tag = this.drugBillClassInfo.PrintType.ID;  //��ӡ����
            this.cbxPutType.Tag = this.drugBillClassInfo.DrugAttribute.ID;   //��ҩ����
            this.cbxIsValid.Checked = this.drugBillClassInfo.IsValid;       //�Ƿ���Ч
            this.txtMark.Text = this.drugBillClassInfo.Memo;          //��ע
            //�����Ұ�ҩ�������޸�
            if( this.drugBillClassInfo.ID == "P" || this.drugBillClassInfo.ID == "R" )
            {
                this.lvPutDrugBill1.AllowEdit = false;
            }
            else
            {
                this.lvPutDrugBill1.AllowEdit = true;
            }
        }

        #endregion

        #region ��ʼ��

        private void Init()
        {
            //��ʼ����ӡ����
            this.cbxPrinttype.AddItems(BillPrintType.List());
            //��ʼ����ҩ����
            this.cbxPutType.AddItems(DrugAttribute.List());
            //����tabpage2
            this.neuTabControl1.TabPages.Remove(this.tabPage2);


            this.lvPutDrugBill1.CheckBoxes = false;
            this.lvPutDrugBill1.MultiSelect = false;

            FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();
            ArrayList drugBillClassList = new ArrayList();
            drugBillClassList = drugStoreMgr.QueryDrugBillClassList();
            if (drugBillClassList == null)
            {
                this.ShowMessage("��ȡ��ҩ�������б�������"+drugStoreMgr.Err, MessageBoxIcon.Error);
                return;
            }

            if (drugBillClassList.Count == 0)
            {
                object iBillClass = Components.DrugStore.Inpatient.Common.InterfaceManager.GetDrugBillClass();
                //û��ʵ�ֽӿڵ�ʹ�ú���Ĭ�ϵİ�ҩ��
                if (iBillClass == null)
                {
                    drugBillClassList = this.SaveDefaultBill();
                }
                //�Ѿ����ػ��˾�ʹ�ñ��ػ�����
                if (iBillClass is FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IDrugBillClass)
                {
                    List<FS.HISFC.Models.Pharmacy.DrugBillClass> listDrubBillClass = ((FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IDrugBillClass)iBillClass).GetList();
                    if (listDrubBillClass != null)
                    {
                        drugBillClassList.AddRange(listDrubBillClass);
                    }
                    else
                    {
                        this.ShowMessage("��ȡ���ػ��İ�ҩ�������б����˴���\n��֪ͨϵͳ����Ա�鿴�ӿ�FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IDrubBillClassʵ��", MessageBoxIcon.Error);
                    }
                }
                string errInfo = "";
                if (Function.SendBizMessage(drugBillClassList, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.DrugBillClass, ref errInfo) == -1)
                {
                    this.ShowMessage("������Ϣʧ�ܣ�����ϵͳ����Ա��ϵ���������" + errInfo, MessageBoxIcon.Error);
                }
            }
            this.lvPutDrugBill1.ShowDrugBillClassList(drugBillClassList);
        }
       

        #endregion

        #region ���ݲ���

        /// <summary>
        /// ���Ӱ�ҩ��
        /// </summary>
        private void AddDrugBill()
        {
            if (this.neuTabControl1.TabPages.ContainsKey(tabPage2.Name))
            {
                this.neuTabControl1.TabPages.Remove(this.tabPage2);
            }
            //����Ҫ����Ľڵ�
            DrugBillClass info = new DrugBillClass( );
            info.Name = "�½���ҩ��";
            info.IsValid = true;

            //����ϸ��Ϣ����ʾ�����ӵİ�ҩ��
            this.SetDrugBillItem( info );

            this.neuTabControl1.TabPages.Add( this.tabPage2 );
            this.neuTabControl1.SelectedIndex = 1;
        }

        /// <summary>
        /// �޸İ�ҩ�����
        /// </summary>
        private void ModifyDrugBill( )
        {
            if( this.lvPutDrugBill1.SelectedItems.Count > 0 )
            {
                if (this.drugBillClassInfo.ID == "" || this.drugBillClassInfo.ID == null)
                {
                    this.lvPutDrugBill1.ClearSelection();

                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ѡ��Ҫ�޸ĵİ�ҩ��"));
                    return;
                }

                //��ʾ��ҩ���༭��Ϣ
                this.neuTabControl1.TabPages.Add( this.tabPage2 );
                this.neuTabControl1.SelectedIndex = 1;
            }
            else
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ѡ��Ҫ�޸ĵİ�ҩ��"));
                //�������ð�ҩ������
                this.neuTabControl1.SelectedIndex = 1;
                this.drugBillClassInfo = new DrugBillClass( );

            }
        }

        /// <summary>
        /// ɾ��һ����ҩ����������
        /// </summary>
        private void DeleteDrugBill( )
        {
            //�ж��Ƿ�ѡ��һ����ҩ��
            if( this.lvPutDrugBill1.SelectedItems.Count > 0 )
            {
                //��ȡ��ǰ��ҩ����Ϣ
                this.GetDrugBillItem( );
            }
            else
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ѡ����Ҫɾ���İ�ҩ����" ));

                return;
            }

            if (this.drugBillClassInfo.ID == "P" || this.drugBillClassInfo.ID == "R")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ҩ������ҽ����ҩ��������ɾ��"),"",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            if( this.drugBillClassInfo.ID != "" )
            {
    
                //������ʾ����
                System.Windows.Forms.DialogResult result;
                result = MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ҩ��ɾ���󽫲��ɻָ�,��ȷ��Ҫɾ����" + drugBillClassInfo.Name + "����ҩ����" ), FS.FrameWork.Management.Language.Msg( "ɾ����ʾ") , System.Windows.Forms.MessageBoxButtons.OKCancel );
                if( result == DialogResult.Cancel ) return;

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm( FS.FrameWork.Management.Language.Msg( "����ɾ����ҩ��������ϸ��Ϣ�����Ե�..." ));
                Application.DoEvents( );

                //ɾ������
                int parmClass;
                //int parmList;

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction( FS.FrameWork.Management.Connection.Instance );

                drugStoreManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                //��ɾ����ҩ��������Ϣ
                //parmList = drugStoreManager.DeleteDrugBillList( this.drugBillClassInfo.ID );
                //��ɾ����ҩ����Ϣ
                parmClass = drugStoreManager.DeleteDrugBillClass( this.drugBillClassInfo.ID );

                if(parmClass == -1 )
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show( this.drugStoreManager.Err , FS.FrameWork.Management.Language.Msg( "FS.FrameWork.Management.Language.Msg( ��ʾ"));
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm( );
                    return;
                }
                else
                {
                    ArrayList alBillClass=new ArrayList();
                    alBillClass.Add(this.drugBillClassInfo);
                    string errInfo="";
                    if (Function.SendBizMessage(alBillClass, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Delete, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.DrugBillClass, ref errInfo) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("ɾ��ʧ�ܣ�"+errInfo));
                        return;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "ɾ���ɹ���") );
                    
                }
            }
            //ɾ���ڵ�
            this.lvPutDrugBill1.DeleteItem( this.lvPutDrugBill1.SelectedIndices[ 0 ] );
            this.lvPutDrugBill1.Focus( );
            this.neuTabControl1.SelectedIndex = 0;
            this.drugBillClassInfo = new DrugBillClass( );
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm( );
        }

        /// <summary>
        /// �����ҩ�����
        /// </summary>
        private void SaveDrugBill()
        {
            //��ȡ��ǰ��ҩ�����µı༭��Ϣ
            this.GetDrugBillItem();
            //��Ч���ж�
            if (!this.IsDrugBillDataValid(this.drugBillClassInfo))
            {
                return;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            drugStoreManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            bool isNewDrugBill = false;
            //�������
            if (this.drugBillClassInfo.ID == "")
            {
                isNewDrugBill = true;
            }

            int parm = drugStoreManager.SetDrugBillClass(this.drugBillClassInfo);
            if (parm == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(this.drugStoreManager.Err);
                return;
            }

            ArrayList alBillClass = new ArrayList();
            alBillClass.Add(this.drugBillClassInfo);
            string errInfo = "";
            FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType = FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Modify;
            if (isNewDrugBill)
            {
                operType = FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add;
            }
            if (Function.SendBizMessage(alBillClass, operType, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.DrugBillClass, ref errInfo) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ʧ�ܣ�" + errInfo));
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ɹ�"));

            if (isNewDrugBill)
            {
                this.lvPutDrugBill1.AddItem(this.drugBillClassInfo, true);

                this.drugBillClassInfo = new DrugBillClass();
            }
            else
            {
                //�ø��º�Ľڵ���Ϣ�޸�ListView�ж�Ӧ�Ľڵ�
                this.lvPutDrugBill1.ModifyItem(this.drugBillClassInfo, this.lvPutDrugBill1.SelectedIndices[0]);
            }

            this.neuTabControl1.SelectedIndex = 0;
        }      

        /// <summary>
        /// Ĭ�ϰ�ҩ������
        /// </summary>
        private ArrayList SaveDefaultBill()
        {
            ArrayList al = new ArrayList();

            FS.HISFC.Models.Pharmacy.DrugBillClass rDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            rDrugBill.ID = "R";
            rDrugBill.Name = "��ҩ��";                       //��ҩ��������
            rDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.D;           //��ӡ����
            rDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //��ҩ����
            rDrugBill.IsValid = true;              //�Ƿ���Ч
            rDrugBill.Memo = "ϵͳ��������İ�ҩ�������ܸ��ġ�ɾ��";               //��ע
            al.Add(rDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass pDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            pDrugBill.ID = "P";
            pDrugBill.Name = "��ҽ����ҩ��";                       //��ҩ��������
            pDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.D;           //��ӡ����
            pDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //��ҩ����
            pDrugBill.IsValid = true;              //�Ƿ���Ч
            pDrugBill.Memo = "ϵͳ��������İ�ҩ�������ܸ��ġ�ɾ��";               //��ע
            al.Add(pDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass cDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            cDrugBill.ID = "C";
            cDrugBill.Name = "��ҩ��ҩ��";                       //��ҩ��������
            cDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.D;           //��ӡ����
            cDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //��ҩ����
            cDrugBill.IsValid = true;              //�Ƿ���Ч
            cDrugBill.Memo = "ϵͳĬ�ϰ�ҩ��";               //��ע
            al.Add(cDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass oDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            oDrugBill.ID = "O";
            oDrugBill.Name = "��Ժ��ҩ��ҩ��";                       //��ҩ��������
            oDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.D;           //��ӡ����
            oDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //��ҩ����
            oDrugBill.IsValid = true;              //�Ƿ���Ч
            oDrugBill.Memo = "ϵͳĬ�ϰ�ҩ��";               //��ע
            al.Add(oDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass aDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            aDrugBill.ID = "A";
            aDrugBill.Name = "�ۺϰ�ҩ��";                       //��ҩ��������
            aDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.D;           //��ӡ����
            aDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //��ҩ����
            aDrugBill.IsValid = true;              //�Ƿ���Ч
            aDrugBill.Memo = "ϵͳ��������İ�ҩ�������ܸ��ġ�ɾ��";               //��ע
            al.Add(aDrugBill);


            FS.HISFC.Models.Pharmacy.DrugBillClass dcDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            dcDrugBill.ID = "DC";
            dcDrugBill.Name = "��ҩ����ҽ���ڷ���ҩ��";                       //��ҩ��������
            dcDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.D;           //��ӡ����
            dcDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //��ҩ����
            dcDrugBill.IsValid = true;              //�Ƿ���Ч
            dcDrugBill.Memo = "ϵͳĬ�ϵİ�ҩ��";               //��ע
            al.Add(dcDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass dlDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            dlDrugBill.ID = "DL";
            dlDrugBill.Name = "��ҩ��ʱҽ���ڷ���ҩ��";                       //��ҩ��������
            dlDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.D;           //��ӡ����
            dlDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //��ҩ����
            dlDrugBill.IsValid = true;              //�Ƿ���Ч
            dlDrugBill.Memo = "ϵͳĬ�ϵİ�ҩ��";               //��ע
            al.Add(dlDrugBill);


            FS.HISFC.Models.Pharmacy.DrugBillClass tcDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            tcDrugBill.ID = "TC";
            tcDrugBill.Name = "��ҩ����ҽ�������ҩ��";                       //��ҩ��������
            tcDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.T;           //��ӡ����
            tcDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //��ҩ����
            tcDrugBill.IsValid = true;              //�Ƿ���Ч
            tcDrugBill.Memo = "ϵͳĬ�ϵİ�ҩ��";               //��ע
            al.Add(tcDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass tlDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            tlDrugBill.ID = "TL";
            tlDrugBill.Name = "��ҩ��ʱҽ�������ҩ��";                       //��ҩ��������
            tlDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.T;           //��ӡ����
            tlDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //��ҩ����
            tlDrugBill.IsValid = true;              //�Ƿ���Ч
            tlDrugBill.Memo = "ϵͳĬ�ϵİ�ҩ��";               //��ע
            al.Add(tlDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass gDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            gDrugBill.ID = "G";
            gDrugBill.Name = "����Һ��ҩ��";                       //��ҩ��������
            gDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.T;           //��ӡ����
            gDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //��ҩ����
            gDrugBill.IsValid = true;              //�Ƿ���Ч
            gDrugBill.Memo = "ϵͳĬ�ϵİ�ҩ��";               //��ע
            al.Add(gDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass sDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            sDrugBill.ID = "S";
            sDrugBill.Name = "���龫��ҩ��";                       //��ҩ��������
            sDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.D;           //��ӡ����
            sDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //��ҩ����
            sDrugBill.IsValid = true;              //�Ƿ���Ч
            sDrugBill.Memo = "ϵͳĬ�ϵİ�ҩ��";               //��ע
            al.Add(sDrugBill);

            FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            foreach (FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass in al)
            {
                if (drugStoreManager.InsertOneDrugBillClass(drugBillClass) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return null;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            return al;
        }
        #endregion

        #endregion

        #region �¼�
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad( EventArgs e )
        {
            try
            {               
                this.Init();
            }
            catch { }

            base.OnLoad( e );
        }

        /// <summary>
        /// �����ҩ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click( object sender , EventArgs e )
        {
            this.SaveDrugBill( );
        }

        /// <summary>
        /// ѡ���ҩ����Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvPutDrugBill1_SelectedIndexChanged( object sender , EventArgs e )
        {
            if( this.lvPutDrugBill1.SelectedItems.Count > 0 )
            {
                //�����еķǵ�ǰ��ҩ��Ϊδѡ��״̬
                foreach( ListViewItem lvi in this.lvPutDrugBill1.CheckedItems )
                {
                    lvi.Checked = false;
                }
                this.lvPutDrugBill1.SelectedItems[ 0 ].Checked = true;
                //���õ�ǰ��ҩ����Ϣ
                this.SetDrugBillItem( this.lvPutDrugBill1.SelectedItems[ 0 ].Tag as DrugBillClass );
                if( this.drugBillClassInfo.ID != null )
                {
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(FS.FrameWork.Management.Language.Msg( "��������Ԥ����Ϣ..." ));
                    Application.DoEvents( );
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm( );
                }
            }
            else
            {
                //�������ð�ҩ������
                this.drugBillClassInfo = new DrugBillClass( );
            }
        }

        /// <summary>
        /// tabpage�л��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTabControl1_SelectedIndexChanged( object sender , EventArgs e )
        {
            if (this.neuTabControl1.SelectedIndex == 0)
            {
                this.neuTabControl1.TabPages.Remove(this.tabPage2);
            }
        }

        #endregion

        #region ��������Ϣ

        /// <summary>
        /// ���幤��������
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService( );
        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="NeuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit( object sender , object NeuObject , object param )
        {
            //���ӹ�����
            this.toolBarService.AddToolButton( "����" , "���Ӱ�ҩ��" , FS.FrameWork.WinForms.Classes.EnumImageList.T��� , true , false , null );
            this.toolBarService.AddToolButton( "�༭" , "�༭��ҩ��" , FS.FrameWork.WinForms.Classes.EnumImageList.X�޸� , true , false , null );
            this.toolBarService.AddToolButton( "ɾ��" , "ɾ����ҩ��" , FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ�� , true , false , null );
            return this.toolBarService;

            
        }

        /// <summary>
        /// ��������ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked( object sender , ToolStripItemClickedEventArgs e )
        {
            switch( e.ClickedItem.Text )
            {
                case "����":
                    this.AddDrugBill( );
                    break;
                case "�༭":
                    this.ModifyDrugBill( );
                    break;
                case "ɾ��":
                    this.DeleteDrugBill( );
                    break;
                default:
                    break;
            }

        }
        #endregion

        /// <summary>
        /// MessageBoxͳһ��ʽ
        /// </summary>
        /// <param name="text"></param>
        public void ShowMessage(string text, MessageBoxIcon messageBoxIcon)
        {
            string caption = "";
            switch (messageBoxIcon)
            {
                case MessageBoxIcon.Warning:
                    caption = "����>>";
                    break;
                case MessageBoxIcon.Error:
                    caption = "����>>";
                    break;
                default:
                    caption = "��ʾ>>";
                    break;
            }

            MessageBox.Show(this, text, caption, MessageBoxButtons.OK, messageBoxIcon);
        }
    }
}
