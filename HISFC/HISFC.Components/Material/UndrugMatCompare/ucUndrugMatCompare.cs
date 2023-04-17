using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using System.Collections;
using FS.HISFC.BizLogic.Material;
using FS.FrameWork.Function;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Material.UndrugMatCompare
{
    public partial class ucUndrugMatCompare : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucUndrugMatCompare()
        {
            InitializeComponent();
        }


        #region ����
        /// <summary>
        /// ���ʿ�Ŀ�滻
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper itemKindObjHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ���������滻
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper operHelper = new FS.FrameWork.Public.ObjectHelper();


        /// <summary>
        /// ���ʲֿ⡢��Ŀ����
        /// </summary>
        private FS.HISFC.BizLogic.Material.Baseset basesetManager = new FS.HISFC.BizLogic.Material.Baseset();
        /// <summary>
        /// ��ҩƷ��Ŀҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeItem = new FS.HISFC.BizProcess.Integrate.Fee();
        /// <summary>
        /// ��ҩƷ���ʱȽ�
        /// </summary>
        private FS.HISFC.BizLogic.Material.UndrugMatCompare undrugMatManager = new FS.HISFC.BizLogic.Material.UndrugMatCompare();
        /// <summary>
        /// ����ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Material.MetItem metitem = new FS.HISFC.BizLogic.Material.MetItem();
        /// <summary>
        /// ����ID,NAME
        /// </summary>
        private Hashtable htExecDept = new Hashtable();
        /// <summary>
        /// ��С���ô���
        /// </summary>
        private Hashtable htFeeType = new Hashtable();

        /// <summary>
        /// ���ݱ�
        /// </summary>
        private DataTable dt_mat = new DataTable();
        private DataTable dt_undrug = new DataTable();
        private DataTable dt_compare = new DataTable();

        private FS.FrameWork.Public.ObjectHelper applicabilityAreaHelp = new FS.FrameWork.Public.ObjectHelper();

        private FS.FrameWork.Public.ObjectHelper produceHelper = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// ��ϣ��
        /// </summary>
        private Hashtable htMat = new Hashtable();
        private Hashtable htUndrug = new Hashtable();

        /// <summary>
        /// ���ݹ���
        /// </summary>
        private DataView dv_mat;
        private DataView dv_undrug;
        private DataView dv_compare;

        /// <summary>
        /// ί��
        /// </summary>
        public delegate void SaveInput(ArrayList arrayList);
        /// <summary>
        /// ����ά���ؼ�
        /// </summary>




        //private ucUndrugMatManager frm = new ucUndrugMatManager();



        #endregion

        #region ��ʼ�������ݱ����

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            this.txtFiler3.Focus();
            #region Fp���λس���
            InputMap im;

            im = this.fpCompare.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
            //this.cmbLeach.AddItems(this.comCompany.GetList(FS.HISFC.Models.Base.EnumConstant.DOSAGEFORM));
            #endregion
            ArrayList alItemKind = new ArrayList();
            alItemKind = this.basesetManager.QueryKind();

            itemKindObjHelper.ArrayObject = alItemKind;
            //��ò���Ա����
            FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
            ArrayList personAl = personManager.GetEmployeeAll();
            if (personAl == null)
            {
                MessageBox.Show("��ȡȫ����Ա�б����!" + personManager.Err);
                return 0;
            }
            this.operHelper.ArrayObject = personAl;


            this.InitCompareDataSet();
            this.ShowCompareData();

            this.InitMatDataSet();
            this.ShowMatData();

            this.InitUndrugDataSet();
            this.ShowUndrugData();

            return 1;

        }

        #region ������ʾ
        /// <summary>
        ///  ��ʼ��DataSet
        /// </summary>
        private void InitMatDataSet()
        {

            this.fpMat_Sheet1.DataAutoSizeColumns = true;


            //��������
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtBool = System.Type.GetType("System.Boolean");

            //��dt�������
            this.dt_mat.Columns.AddRange(new DataColumn[] {
												new DataColumn("��Ʒ����", dtStr),	 //0														   
												new DataColumn("��Ʒ��Ŀ", dtStr),  //1
												new DataColumn("��Ʒ����", dtStr),     //2                                
												new DataColumn("ƴ����", dtStr),     //3
												new DataColumn("�����", dtStr),     //4
												new DataColumn("�Զ�����", dtStr),    //5
												new DataColumn("���ұ���", dtStr),    //6
												new DataColumn("���", dtStr),         //7
												new DataColumn("��λ", dtStr),          //8
												new DataColumn("����", dtStr),           //9
												new DataColumn("�����շѱ�־",dtStr),     //10
												new DataColumn("������Ϣ", dtStr),        //11
												new DataColumn("ҽ����Ŀ����", dtStr),        //12
												new DataColumn("ҽ����Ŀ����", dtStr),     //13
												new DataColumn("��ҩƷ����", dtStr),        //14
												new DataColumn("��ҩƷ����", dtStr),       //15
												new DataColumn("ͣ�ñ��", dtStr),       //16
												new DataColumn("�����־", dtStr),        //17
												new DataColumn("��������", dtStr),        //18
												new DataColumn("������˾", dtStr),        //19
												new DataColumn("���ô���", dtStr),        //20
												new DataColumn("ͳ�ƴ���", dtStr),          //21
                                                new DataColumn("��Ŀ����",dtStr),          //22
												new DataColumn("��װ��λ",dtStr),          //23
												new DataColumn("��װ����",dtStr),          //24
												new DataColumn("��װ�۸�",dtStr),          //25
												new DataColumn("�Ӽ۹���",dtStr),         //26
												new DataColumn("�ֿ�����",dtStr),        //27
												new DataColumn("��Դ",dtStr),             //28
												new DataColumn("��;",dtStr)				//29						
														});

            this.dv_mat = new DataView(this.dt_mat);
            this.dv_mat.AllowEdit = true;
            this.dv_mat.AllowNew = true;
            this.fpMat.DataSource = this.dv_mat;

            this.fpMat_Sheet1.Columns[0].Visible = false;
            this.fpMat_Sheet1.Columns[3].Visible = false;
            this.fpMat_Sheet1.Columns[4].Visible = false;
            this.fpMat_Sheet1.Columns[5].Visible = false;
            this.fpMat_Sheet1.Columns[6].Visible = false;
            this.fpMat_Sheet1.Columns[12].Visible = false;
            this.fpMat_Sheet1.Columns[14].Visible = false;
            this.fpMat_Sheet1.Columns[15].Visible = false;
            this.fpMat_Sheet1.Columns[16].Visible = false;
            this.fpMat_Sheet1.Columns[17].Visible = false;
            this.fpMat_Sheet1.Columns[20].Visible = false;
            this.fpMat_Sheet1.Columns[21].Visible = false;
            this.fpMat_Sheet1.Columns[22].Visible = false;
        }

        /// <summary>
        /// �����������е�������ʾ��fpMat_sheet1��
        /// </summary>
        public void ShowMatData()
        {
            //this.ClearData();

            //ȡ��Ŀ��Ϣ
            List<FS.HISFC.Models.Material.MaterialItem> matlist = this.metitem.QueryMetItemByValidFlag(true);
            if (matlist == null)
            {
                MessageBox.Show(this.metitem.Err);
                return;
            }
            //ѭ��������Ʒ������Ϣ
            for (int i = 0; i < matlist.Count; i++)
            {
                FS.HISFC.Models.Material.MaterialItem myItem = matlist[i];

                DataRow dr_mat = dt_mat.NewRow();

                try
                {
                    //�����ݲ��뵽DataTable��
                    this.SetMatRow(dr_mat, myItem);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("{0}", ex.Message);
                    return;
                }

                dt_mat.Rows.Add(dr_mat);

                htMat.Add(myItem.ID, myItem);

            }





            #region ����
            //foreach (FS.HISFC.Models.Material.MaterialItem myItem in al)
            //{
            //    this.dt.Rows.Add(new Object[] {			
            //                                                        //��Ʒ����																																

            //                                                         materialItem.ID, 

            //                                                         //��Ʒ��Ŀ
            //                                                           this.itemKindObjHelper.GetName(myItem.MaterialKind.ID.ToString()),

            //                                                            //��Ʒ����
            //                                                            myItem.Name,

            //                                                            //��Ŀ����
            //                                                            metKind.Name,

            //                                                            //ƴ����
            //                                                            metKind.SpellCode.ToString(),

            //                                                            //�����
            //                                                            metKind.WBCode,

            //                                                            //��ĩ����ʶ
            //                                                            metKind.EndGrade,

            //                                                            //��Ҫ��Ƭ
            //                                                            metKind.IsCardNeed,//.ToString(),

            //                                                            //���ι���
            //                                                            metKind.IsBatch,//.ToString(),

            //                                                            //��Ч�ڹ���
            //                                                            metKind.IsValidcon,//.ToString(),

            //                                                            //�����Ŀ����
            //                                                            metKind.AccountCode.ToString(),

            //                                                            //�����Ŀ����
            //                                                            metKind.AccountName.ToString(),

            //                                                            //����Ա
            //                                                            //metKind.Oper.ID,

            //                                                            //��������
            //                                                            //metKind.OperateDate.ToString(),

            //                                                            //Ԥ�Ʋ�ֵ��
            //                                                            metKind.LeftRate.ToString(),

            //                                                            //�Ƿ�̶��ʲ�
            //                                                            metKind.IsFixedAssets,//.ToString(),

            //                                                            //�������
            //                                                            metKind.OrderNo.ToString(),																		

            //                                                            //��Ӧ�ɱ�������Ŀ���
            //                                                            metKind.StatCode,

            //                                                            //�Ƿ�Ӽ�����
            //                                                            metKind.IsAddFlag//.ToString()
            //                                                        });
            //}
            #endregion
            //�ύDataSet�еı仯��
            this.dt_mat.AcceptChanges();


        }
        /// <summary>
        /// �����ݲ��뵽DataTable��
        /// </summary>

        private DataRow SetMatRow(DataRow dr_mat, FS.HISFC.Models.Material.MaterialItem myItem)
        {
            dr_mat["��Ʒ����"] = myItem.ID;
            dr_mat["��Ʒ��Ŀ"] = this.itemKindObjHelper.GetName(myItem.MaterialKind.ID.ToString());
            dr_mat["��Ʒ����"] = myItem.Name;
            dr_mat["ƴ����"] = myItem.SpellCode;
            dr_mat["�����"] = myItem.WBCode;
            dr_mat["�Զ�����"] = myItem.UserCode;
            dr_mat["���ұ���"] = myItem.GbCode;
            dr_mat["���"] = myItem.Specs;
            dr_mat["��λ"] = myItem.MinUnit;
            dr_mat["����"] = myItem.UnitPrice.ToString();
            if (myItem.FinanceState)
            {
                dr_mat["�����շѱ�־"] = "��";
            }
            else
            {
                dr_mat["�����շѱ�־"] = "��";
            }
            dr_mat["������Ϣ"] = myItem.ApproveInfo;
            dr_mat["ҽ����Ŀ����"] = myItem.Compare.ID;
            dr_mat["ҽ����Ŀ����"] = myItem.Compare.Name;
            dr_mat["��ҩƷ����"] = myItem.UndrugInfo.ID;
            dr_mat["��ҩƷ����"] = myItem.UndrugInfo.Name;
            if (myItem.ValidState)
            {
                dr_mat["ͣ�ñ��"] = "ʹ��";
            }
            else
            {
                dr_mat["ͣ�ñ��"] = "ͣ��";
            }
            dr_mat["�����־"] = myItem.SpecialFlag;
            dr_mat["��������"] = this.produceHelper.GetName(myItem.Factory.ID.ToString());
            dr_mat["������˾"] = this.produceHelper.GetName(myItem.Company.ID.ToString());
            dr_mat["���ô���"] = myItem.MinFee.ID;
            dr_mat["ͳ�ƴ���"] = myItem.StatInfo.ID;
            dr_mat["��Ŀ����"] = myItem.MaterialKind.ID;
            dr_mat["��װ��λ"] = myItem.PackUnit;
            dr_mat["��װ����"] = myItem.PackQty;
            dr_mat["��װ�۸�"] = myItem.PackPrice;
            dr_mat["�Ӽ۹���"] = myItem.AddRule;
            dr_mat["�ֿ�����"] = itemKindObjHelper.GetName(myItem.StorageInfo.ID);
            dr_mat["��Դ"] = myItem.InSource;
            dr_mat["��;"] = myItem.Usage;
            return dr_mat;

        }


        #endregion

        #region ��ҩƷ��ʾ


        /// <summary>
        ///  ��ʼ��dt_undrug
        /// </summary>
        private void InitUndrugDataSet()
        {

            this.fpUndrug_Sheet1.DataAutoSizeColumns = true;


            //��������
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtBool = System.Type.GetType("System.Boolean");

            //��dt�������
            this.dt_undrug.Columns.AddRange(new DataColumn[] { 
                                                               new DataColumn("��Ŀ���", typeof(string)),                    //0 
                                                               new DataColumn("��Ŀ����", typeof(string)),                    //1 
                                                               new DataColumn("ϵͳ���", typeof(string)),                    //2 
                                                               new DataColumn("���ô���", typeof(string)),                    //3 
                                                               new DataColumn("������", typeof(string)),                      //4 
                                                               new DataColumn("ƴ����", typeof(string)),                      //5 
                                                               new DataColumn("�����", typeof(string)),                      //6 
                                                               new DataColumn("���ұ���", typeof(string)),                    //7 
                                                               new DataColumn("���ʱ���", typeof(string)),                    //8 
                                                               new DataColumn("Ĭ�ϼ�", typeof(string)),                      //9 
                                                               new DataColumn("��λ", typeof(string)),                        //10
                                                               new DataColumn("����ӳɱ���", typeof(string)),                //11
                                                               new DataColumn("�ƻ��������", typeof(string)),                //12
                                                               new DataColumn("�ض�������Ŀ", typeof(string)),                //13
                                                               new DataColumn("�������־", typeof(string)),                  //14
                                                               new DataColumn("ȷ�ϱ�־", typeof(string)),                    //15
                                                               new DataColumn("��Ч�Ա�ʶ", typeof(string)),                  //16
                                                               new DataColumn("���", typeof(string)),                        //17
                                                               new DataColumn("ִ�п���", typeof(string)),                    //18
                                                               new DataColumn("�豸���", typeof(string)),                    //19
                                                               new DataColumn("�걾", typeof(string)),                        //20
                                                               new DataColumn("��������", typeof(string)),                    //21
                                                               new DataColumn("��������", typeof(string)),                    //22
                                                               new DataColumn("������ģ", typeof(string)),                    //23
                                                               new DataColumn("�Ƿ����", typeof(string)),                    //24
                                                               new DataColumn("��ע", typeof(string)),                        //25
                                                               new DataColumn("��ͯ��", typeof(string)),                      //26
                                                               new DataColumn("�����", typeof(string)),                      //27
                                                               new DataColumn("ʡ����", typeof(string)),                      //28
                                                               new DataColumn("������", typeof(string)),                      //29
                                                               new DataColumn("�Է���Ŀ", typeof(string)),                    //30
                                                               new DataColumn("�����ʶ1", typeof(string)),                   //31
                                                               new DataColumn("�����ʶ2", typeof(string)),                   //32
                                                               new DataColumn("����1", typeof(string)),                       //33
                                                               new DataColumn("����2", typeof(string)),                       //34
                                                               new DataColumn("��������", typeof(string)),                    //35
                                                               new DataColumn("ר������", typeof(string)),                    //36
                                                               new DataColumn("��ʷ�����", typeof(string)),                  //37
                                                               new DataColumn("���Ҫ��", typeof(string)),                    //38
                                                               new DataColumn("ע������", typeof(string)),                    //39
                                                               new DataColumn("֪��ͬ����", typeof(string)),                  //40
                                                               new DataColumn("������뵥����", typeof(string)),              //41
                                                               new DataColumn("�Ƿ���ҪԤԼ", typeof(string)),                //42
                                                               new DataColumn("��Ŀ��Χ", typeof(string)),                    //43
                                                               new DataColumn("��ĿԼ��", typeof(string)),                    //44
                                                               new DataColumn("��λ��ʶ", typeof(string)),                    //45
                                                               new DataColumn("���÷�Χ",typeof(string))                      //46
                                                  });

            this.dv_undrug = new DataView(this.dt_undrug);
            this.dv_undrug.AllowEdit = true;
            this.dv_undrug.AllowNew = true;
            this.fpUndrug.DataSource = this.dv_undrug;

            this.fpUndrug_Sheet1.Columns[0].Visible = false;
            this.fpUndrug_Sheet1.Columns[3].Visible = false;
            this.fpUndrug_Sheet1.Columns[4].Visible = false;
            this.fpUndrug_Sheet1.Columns[5].Visible = false;
            this.fpUndrug_Sheet1.Columns[6].Visible = false;
            this.fpUndrug_Sheet1.Columns[7].Visible = false;
            this.fpUndrug_Sheet1.Columns[8].Visible = false;
            this.fpUndrug_Sheet1.Columns[11].Visible = false;
            this.fpUndrug_Sheet1.Columns[12].Visible = false;
            this.fpUndrug_Sheet1.Columns[13].Visible = false;
            this.fpUndrug_Sheet1.Columns[15].Visible = false;
            this.fpUndrug_Sheet1.Columns[19].Visible = false;
            this.fpUndrug_Sheet1.Columns[20].Visible = false;
            this.fpUndrug_Sheet1.Columns[21].Visible = false;
            this.fpUndrug_Sheet1.Columns[22].Visible = false;
            this.fpUndrug_Sheet1.Columns[23].Visible = false;
            this.fpUndrug_Sheet1.Columns[24].Visible = false;
            this.fpUndrug_Sheet1.Columns[30].Visible = false;
            this.fpUndrug_Sheet1.Columns[31].Visible = false;
            this.fpUndrug_Sheet1.Columns[32].Visible = false;
            this.fpUndrug_Sheet1.Columns[33].Visible = false;
            this.fpUndrug_Sheet1.Columns[34].Visible = false;
            this.fpUndrug_Sheet1.Columns[35].Visible = false;
            this.fpUndrug_Sheet1.Columns[36].Visible = false;
            this.fpUndrug_Sheet1.Columns[37].Visible = false;
            this.fpUndrug_Sheet1.Columns[38].Visible = false;
            this.fpUndrug_Sheet1.Columns[40].Visible = false;
            this.fpUndrug_Sheet1.Columns[41].Visible = false;
            this.fpUndrug_Sheet1.Columns[42].Visible = false;
            this.fpUndrug_Sheet1.Columns[43].Visible = false;
            this.fpUndrug_Sheet1.Columns[44].Visible = false;
            this.fpUndrug_Sheet1.Columns[45].Visible = false;

        }

        /// <summary>
        /// �����������е�������ʾ��fpMat_sheet1��
        /// </summary>
        public void ShowUndrugData()
        {
            //this.ClearData();

            //ȡ��Ŀ��Ϣ
            List<FS.HISFC.Models.Fee.Item.Undrug> itemList = this.feeItem.QueryAllItemsList();
            if (itemList == null)
            {
                MessageBox.Show(this.feeItem.Err);
                return;
            }
            //ѭ��������Ʒ������Ϣ
            for (int i = 0; i < itemList.Count; i++)
            {
                FS.HISFC.Models.Fee.Item.Undrug undrug = itemList[i];

                DataRow dr_undrug = dt_undrug.NewRow();

                try
                {
                    //�����ݲ��뵽DataTable��
                    this.SetUndrugRow(dr_undrug, undrug);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("{0}", ex.Message);
                    return;
                }

                dt_undrug.Rows.Add(dr_undrug);
                htUndrug.Add(undrug.ID, undrug);

            }

            //�ύDataSet�еı仯��
            this.dt_undrug.AcceptChanges();


        }
        /// <summary>
        /// �����ݲ��뵽DataTable��
        /// </summary>

        private DataRow SetUndrugRow(DataRow dr_undrug, FS.HISFC.Models.Fee.Item.Undrug undrug)
        {
            dr_undrug["��Ŀ���"] = undrug.ID;
            dr_undrug["��Ŀ����"] = undrug.Name;

            dr_undrug["ϵͳ���"] = undrug.SysClass.Name;
            try
            {
                dr_undrug["ִ�п���"] = undrug.ExecDept == "" ? "" : this.htExecDept[undrug.ExecDept].ToString();
            }
            catch
            {
                dr_undrug["ִ�п���"] = "";
            }

            try
            {
                dr_undrug["���ô���"] = this.htFeeType[undrug.MinFee.ID].ToString();//
            }
            catch
            {
                dr_undrug["���ô���"] = "";
            }

            dr_undrug["������"] = undrug.UserCode;
            dr_undrug["ƴ����"] = undrug.SpellCode;
            dr_undrug["�����"] = undrug.WBCode;
            dr_undrug["���ұ���"] = undrug.GBCode;
            dr_undrug["���ʱ���"] = undrug.NationCode;
            dr_undrug["Ĭ�ϼ�"] = undrug.Price;
            dr_undrug["��λ"] = undrug.PriceUnit;
            dr_undrug["����ӳɱ���"] = undrug.FTRate.EMCRate.ToString();
            dr_undrug["�ƻ��������"] = undrug.IsFamilyPlanning ? "��" : "��";

            //���Ҳ������,�Ժ�������
            dr_undrug["�ض�������Ŀ"] = undrug.User01;

            //û��ת��������ֻ����ʾ1��2
            switch (undrug.Grade)
            {
                case "1":
                    dr_undrug["�������־"] = "��";
                    break;
                case "2":
                    dr_undrug["�������־"] = "��";
                    break;
                case "3":
                    dr_undrug["�������־"] = "��";
                    break;
                default:
                    dr_undrug["�������־"] = "";
                    break;
            }
            //dr["�������־"] = undrug.Grade;

            dr_undrug["ȷ�ϱ�־"] = undrug.IsNeedConfirm ? "��Ҫ" : "����Ҫ";
            switch (undrug.ValidState)
            {
                case "0":
                    dr_undrug["��Ч�Ա�ʶ"] = "ͣ��";
                    break;
                case "1":
                    dr_undrug["��Ч�Ա�ʶ"] = "����";
                    break;
                case "2":
                    dr_undrug["��Ч�Ա�ʶ"] = "����";
                    break;
                default:
                    dr_undrug["��Ч�Ա�ʶ"] = "";
                    break;
            }
            dr_undrug["���"] = undrug.Specs;
            dr_undrug["�豸���"] = undrug.MachineNO;
            dr_undrug["�걾"] = undrug.CheckBody;
            dr_undrug["��������"] = undrug.OperationInfo.ID;
            dr_undrug["��������"] = undrug.OperationType.ID;
            dr_undrug["������ģ"] = undrug.OperationScale.ID;
            dr_undrug["�Ƿ����"] = undrug.IsCompareToMaterial ? "��" : "û��";
            dr_undrug["��ע"] = undrug.Memo;
            dr_undrug["��ͯ��"] = undrug.ChildPrice.ToString();
            dr_undrug["�����"] = undrug.SpecialPrice.ToString();
            switch (undrug.SpecialFlag)
            {
                case "0":
                    dr_undrug["ʡ����"] = "������";
                    break;
                case "1":
                    dr_undrug["ʡ����"] = "����";
                    break;
                default:
                    dr_undrug["ʡ����"] = "";
                    break;
            }
            switch (undrug.SpecialFlag1)
            {
                case "0":
                    dr_undrug["������"] = "������";
                    break;
                case "1":
                    dr_undrug["������"] = "����";
                    break;
                default:
                    dr_undrug["������"] = "";
                    break;
            }
            switch (undrug.SpecialFlag2)
            {
                case "0":
                    dr_undrug["�Է���Ŀ"] = "����";
                    break;
                case "1":
                    dr_undrug["�Է���Ŀ"] = "��";
                    break;
                default:
                    dr_undrug["�Է���Ŀ"] = "";
                    break;
            }
            switch (undrug.SpecialFlag3)
            {
                case "0":
                    dr_undrug["�����ʶ1"] = "����";
                    break;
                case "1":
                    dr_undrug["�����ʶ1"] = "��";
                    break;
                default:
                    dr_undrug["�����ʶ1"] = "";
                    break;
            }
            switch (undrug.SpecialFlag4)
            {
                case "0":
                    dr_undrug["�����ʶ2"] = "����";
                    break;
                case "1":
                    dr_undrug["�����ʶ2"] = "��";
                    break;
                default:
                    dr_undrug["�����ʶ2"] = "";
                    break;
            }

            //��û����,���ƻ�������
            dr_undrug["����1"] = undrug.User02;

            //��û����,���ƻ�������
            dr_undrug["����2"] = undrug.User03;

            dr_undrug["��������"] = undrug.DiseaseType.ID;
            dr_undrug["ר������"] = undrug.SpecialDept.ID;
            dr_undrug["��ʷ�����"] = undrug.MedicalRecord;
            dr_undrug["���Ҫ��"] = undrug.CheckRequest;
            dr_undrug["ע������"] = undrug.Notice;
            dr_undrug["֪��ͬ����"] = undrug.IsConsent ? "��Ҫ" : "����Ҫ";
            dr_undrug["������뵥����"] = undrug.CheckApplyDept;
            dr_undrug["�Ƿ���ҪԤԼ"] = undrug.IsNeedBespeak ? "��Ҫ" : "����Ҫ";
            dr_undrug["��Ŀ��Χ"] = undrug.ItemArea;
            dr_undrug["��ĿԼ��"] = undrug.ItemException;
            dr_undrug["���÷�Χ"] = this.applicabilityAreaHelp.GetName(undrug.ApplicabilityArea);
            /*��λ��ʶ(0,��ϸ; 1,����)[2007/01/01  xuweizhe]*/
            dr_undrug["��λ��ʶ"] = undrug.UnitFlag;
            return dr_undrug;

        }








        #endregion

        #region �Ƚϱ���ʾ
        /// <summary>
        ///  ��ʼ��DataSet
        /// </summary>
        private void InitCompareDataSet()
        {
            this.fpCompare_Sheet1.DataAutoSizeColumns = true;


            //��������
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtBool = System.Type.GetType("System.Boolean");

            //��dt�������
            this.dt_compare.Columns.AddRange(new DataColumn[] {
												new DataColumn("���ձ��", dtStr),				//0											   
												new DataColumn("�������", dtStr),              //1
												new DataColumn("��ҩƷ����", dtStr),            //2                      
												new DataColumn("��ҩƷ����", dtStr),            //3
												new DataColumn("��ҩƷƴ����", dtStr),          //4
												new DataColumn("��ҩƷ���", dtStr),            //5
												new DataColumn("��ҩƷ�Զ�����", dtStr),        //6
												new DataColumn("��ҩƷ���ұ���", dtStr),        //7
												new DataColumn("���ʱ���", dtStr),              //8
												new DataColumn("��������", dtStr),              //9
												new DataColumn("����ƴ����",dtStr),             //10
												new DataColumn("���������", dtStr),            //11
												new DataColumn("�����Զ�����", dtStr),          //12
												new DataColumn("���ʹ��ұ���", dtStr),          //13
												new DataColumn("����Ա", dtStr),                //14
												new DataColumn("��������", dtStr),     		    //15				
														});

            //this.fpCompare_Sheet1.Rows[0].ForeColor = Color.Red
            this.fpCompare_Sheet1.Columns[0].Visible = false;
            this.fpCompare_Sheet1.Columns[1].Visible = false;
            this.fpCompare_Sheet1.Columns[2].Visible = false;
            this.fpCompare_Sheet1.Columns[4].Visible = false;
            this.fpCompare_Sheet1.Columns[5].Visible = false;
            this.fpCompare_Sheet1.Columns[6].Visible = false;
            this.fpCompare_Sheet1.Columns[7].Visible = false;
            this.fpCompare_Sheet1.Columns[8].Visible = false;
            this.fpCompare_Sheet1.Columns[10].Visible = false;
            this.fpCompare_Sheet1.Columns[11].Visible = false;
            this.fpCompare_Sheet1.Columns[12].Visible = false;
            this.fpCompare_Sheet1.Columns[13].Visible = false;
        }

        /// <summary>
        /// �����������е�������ʾ��fpMat_sheet1��
        /// </summary>
        public void ShowCompareData()
        {

            this.dt_compare.Rows.Clear();

            //ȡ��Ŀ��Ϣ
            ArrayList al = this.undrugMatManager.UndrugMatCompareInfo();

            if (al == null)
            {
                MessageBox.Show(this.undrugMatManager.Err);
                return;
            }
            //ѭ��������Ʒ������Ϣ
            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Material.UndrugMatCompare undrugmat = (FS.HISFC.Models.Material.UndrugMatCompare)al[i];

                DataRow dr_compare = dt_compare.NewRow();

                try
                {
                    //�����ݲ��뵽DataTable��
                    this.SetCompareRow(dr_compare, undrugmat);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("{0}", ex.Message);
                    return;
                }

                dt_compare.Rows.Add(dr_compare);

            }

            //�ύDataSet�еı仯��
            this.dt_compare.AcceptChanges();
            this.dv_compare = new DataView(this.dt_compare);
            this.dv_compare.AllowEdit = true;
            this.dv_compare.AllowNew = true;
            this.fpCompare.DataSource = this.dv_compare;


        }
        /// <summary>
        /// �����ݲ��뵽DataTable��
        /// </summary>
        private DataRow SetCompareRow(DataRow dr_compare, FS.HISFC.Models.Material.UndrugMatCompare umc)
        {


            dr_compare["���ձ��"] = umc.ID;
            dr_compare["�������"] = umc.Compare_NO.ToString();
            dr_compare["��ҩƷ����"] = umc.Undrug.ID;
            dr_compare["��ҩƷ����"] = umc.Undrug.Name;
            dr_compare["��ҩƷƴ����"] = umc.Undrug.SpellCode;
            dr_compare["��ҩƷ���"] = umc.Undrug.WBCode;
            dr_compare["��ҩƷ�Զ�����"] = umc.Undrug.UserCode;
            dr_compare["��ҩƷ���ұ���"] = umc.Undrug.GBCode;
            dr_compare["���ʱ���"] = umc.MatItem.ID;
            dr_compare["��������"] = umc.MatItem.Name;
            dr_compare["����ƴ����"] = umc.MatItem.SpellCode;
            dr_compare["���������"] = umc.MatItem.WBCode;
            dr_compare["�����Զ�����"] = umc.MatItem.UserCode;
            dr_compare["���ʹ��ұ���"] = umc.MatItem.GbCode;
            dr_compare["����Ա"] = this.operHelper.GetName(umc.Oper.ID);
            dr_compare["��������"] = umc.Oper.OperTime;
            return dr_compare;

        }
        #endregion


        #endregion

        #region ˽�з���

        private void frm_MyInput(ArrayList arrayList)
        {
            for (int i = 0; i < arrayList.Count; i++)
            {
                this.fpCompare_Sheet1.Cells[this.fpCompare_Sheet1.ActiveRowIndex, i].Value = arrayList[i] as string;

            }
        }





        private bool IsExist(object undrugID, object matItemID)
        {

            //���巵�ص�BOOLֵ
            bool result = false;
            ArrayList al = new ArrayList();
            al = this.undrugMatManager.UndrugMatCompareInfo();
            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Material.UndrugMatCompare undrugmat = (FS.HISFC.Models.Material.UndrugMatCompare)al[i];

                if (string.Equals(undrugmat.Undrug.ID, undrugID))
                {
                    if (string.Equals(undrugmat.MatItem.ID, matItemID))
                    {

                        result = true;
                        break;

                    }
                }
            }


            for (int i = 0; i < this.fpCompare_Sheet1.RowCount && !result; i++)
            {
                if (string.Equals(this.fpCompare_Sheet1.Cells[i, 2].Value, undrugID))
                {
                    if (string.Equals(this.fpCompare_Sheet1.Cells[i, 8].Value, matItemID))
                    {
                        result = true;
                        break;
                    }

                }


            }
            return result;
        }



        /// <summary>
        /// ���ձ�����ʾѡ�е�����
        /// </summary>
        private void GetUndrugMat(FS.HISFC.Models.Material.MaterialItem matItem, FS.HISFC.Models.Fee.Item.Undrug undrug)
        {
            FS.HISFC.Models.Material.UndrugMatCompare umc = new FS.HISFC.Models.Material.UndrugMatCompare();
            this.fpCompare_Sheet1.Columns[0].Visible = false;
            this.fpCompare_Sheet1.Columns[1].Visible = false;
            if (this.IsExist(undrug.ID, matItem.ID))
            {
                MessageBox.Show("��ӵ����ݶ��ձ������У���������ӣ��������������ˢ�£�");
                return;
            }
            umc.Undrug.ID = undrug.ID;
            umc.Undrug.Name = undrug.Name;
            umc.Undrug.SpellCode = undrug.SpellCode;
            umc.Undrug.WBCode = undrug.WBCode;
            umc.Undrug.UserCode = undrug.UserCode;
            umc.Undrug.GBCode = undrug.GBCode;
            umc.MatItem.ID = matItem.ID;
            umc.MatItem.Name = matItem.Name;
            umc.MatItem.SpellCode = matItem.SpellCode;
            umc.MatItem.WBCode = matItem.WBCode;
            umc.MatItem.UserCode = matItem.UserCode;
            umc.MatItem.GbCode = matItem.GbCode;
            umc.Oper.ID = this.operHelper.GetID(this.undrugMatManager.Operator.Name);
            umc.Oper.OperTime = System.DateTime.Now;

            DataRow dr_compare = dt_compare.NewRow();

            try
            {
                //�����ݲ��뵽DataTable��
                this.SetCompareRow(dr_compare, umc);
            }
            catch (Exception ex)
            {
                MessageBox.Show("{0}", ex.Message);
                return;
            }

            dt_compare.Rows.Add(dr_compare);
            //�ύDataSet�еı仯��
            //this.dt_compare.AcceptChanges();
        }

        /// <summary>
        /// ��������
        /// </summary>
        private int Save()
        {
            this.fpCompare.StopCellEditing();

            foreach (DataRow dr in this.dt_compare.Rows)
            {
                dr.EndEdit();
            }

            //�������ݿ⴦������


            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.undrugMatManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            bool isUpdate = false; //�ж��Ƿ���»���ɾ��������

            //ȡ�޸ĺ����ӵ�����

            DataTable dataChanges = this.dt_compare.GetChanges(DataRowState.Modified | DataRowState.Added);

            if (dataChanges != null)
            {
                foreach (DataRow row in dataChanges.Rows)
                {
                    FS.HISFC.Models.Material.UndrugMatCompare undrugMat = this.GetDataFromTable(row);

                    //ִ�и��²������ȸ��£����û�гɹ������������

                    if (this.undrugMatManager.SetUndrugMat(undrugMat) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("������ձ���Ϣ��������!" + this.undrugMatManager.Err));
                        return -1;
                    }
                }
                dataChanges.AcceptChanges();

                isUpdate = true;
            }

            //ȡɾ��������
            dataChanges = this.dt_compare.GetChanges(DataRowState.Deleted);
            if (dataChanges != null)
            {
                dataChanges.RejectChanges();
                foreach (DataRow row in dataChanges.Rows)
                {
                    string UndrugMatID = row["���ձ��"].ToString();        //��˾����		
                    //ִ��ɾ������
                    if (this.undrugMatManager.Delete(UndrugMatID) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("ɾ�����ձ���" + row["��ҩƷ����"].ToString() + "��������" + this.undrugMatManager.Err));
                        return -1;
                    }
                }
                dataChanges.AcceptChanges();

                isUpdate = true;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            if (isUpdate)
            {
                MessageBox.Show(Language.Msg("����ɹ���"));
            }
            else
            {
                MessageBox.Show("û���κ��޸ģ����豣�棡");
                return -1;
            }

            //ˢ������
            this.ShowCompareData();

            return 1;
        }

        private FS.HISFC.Models.Material.UndrugMatCompare GetDataFromTable(DataRow row)
        {
            FS.HISFC.Models.Material.UndrugMatCompare undrugmat = new FS.HISFC.Models.Material.UndrugMatCompare();
            undrugmat.ID = row["���ձ��"].ToString();
            undrugmat.Compare_NO = NConvert.ToInt32(row["�������"]);

            undrugmat.Undrug.ID = row["��ҩƷ����"].ToString();
            undrugmat.Undrug.Name = row["��ҩƷ����"].ToString();
            undrugmat.Undrug.SpellCode = row["��ҩƷƴ����"].ToString();
            undrugmat.Undrug.WBCode = row["��ҩƷ���"].ToString();
            undrugmat.Undrug.UserCode = row["��ҩƷ�Զ�����"].ToString();
            undrugmat.Undrug.GBCode = row["��ҩƷ���ұ���"].ToString();
            undrugmat.MatItem.ID = row["���ʱ���"].ToString();
            undrugmat.MatItem.Name = row["��������"].ToString();
            undrugmat.MatItem.SpellCode = row["����ƴ����"].ToString();
            undrugmat.MatItem.WBCode = row["���������"].ToString();
            undrugmat.MatItem.UserCode = row["�����Զ�����"].ToString();
            undrugmat.MatItem.GbCode = row["���ʹ��ұ���"].ToString();
            undrugmat.Oper.Name = row["����Ա"].ToString();
            return undrugmat;
        }


        /// <summary>
        /// ɾ��һ����¼

        /// </summary>
        private void DeleteViewData()
        {
            if (this.fpCompare_Sheet1.Rows.Count <= 0)
            {
                return;
            }
            string undrugMatID = this.fpCompare_Sheet1.Cells[this.fpCompare_Sheet1.ActiveRowIndex, 0].Text;
            if ((undrugMatID == null) || (undrugMatID.Equals("")))
            {
                //�ڿؼ���ɾ���˼�¼

                this.fpCompare_Sheet1.Rows.Remove(this.fpCompare_Sheet1.ActiveRowIndex, 1);
            }
            else
            {
                if (MessageBox.Show(Language.Msg("ȷ�Ͻ��ڡ�" + ((int)(this.fpCompare_Sheet1.ActiveRowIndex + 1)).ToString() + "�����ձ���Ϣ������"), "ȷ�϶��ձ���Ϣ����", MessageBoxButtons.YesNo) == DialogResult.No)
                {

                    return;
                }
                //this.fpCompare_Sheet1.Rows[this.fpCompare_Sheet1.ActiveRowIndex].BackColor = Color.Red;
                this.fpCompare_Sheet1.Rows.Remove(this.fpCompare_Sheet1.ActiveRowIndex, 1);
            }
            return;
        }




        private void SetUndrugMat()
        {
            //int key = FS.FrameWork.Function.NConvert.ToInt32(this.fpMat_Sheet1.Cells[this.fpMat_Sheet1.ActiveRowIndex, 0].Value.ToString());
            FS.HISFC.Models.Material.MaterialItem matItem = new FS.HISFC.Models.Material.MaterialItem();
            //key = FS.FrameWork.Function.NConvert.ToInt32(this.fpUndrug_Sheet1.Cells[this.fpUndrug_Sheet1.ActiveRowIndex, 0].Value.ToString());
            FS.HISFC.Models.Fee.Item.Undrug undrug = new FS.HISFC.Models.Fee.Item.Undrug();

            undrug.ID = this.fpUndrug_Sheet1.Cells[this.fpUndrug_Sheet1.ActiveRowIndex, 0].Value.ToString();
            undrug.Name = this.fpUndrug_Sheet1.Cells[this.fpUndrug_Sheet1.ActiveRowIndex, 1].Value.ToString();
            undrug.SpellCode = this.fpUndrug_Sheet1.Cells[this.fpUndrug_Sheet1.ActiveRowIndex, 5].Value.ToString();
            undrug.WBCode = this.fpUndrug_Sheet1.Cells[this.fpUndrug_Sheet1.ActiveRowIndex, 6].Value.ToString();
            undrug.UserCode = this.fpUndrug_Sheet1.Cells[this.fpUndrug_Sheet1.ActiveRowIndex, 4].Value.ToString();
            undrug.GBCode = this.fpUndrug_Sheet1.Cells[this.fpUndrug_Sheet1.ActiveRowIndex, 7].Value.ToString();
            matItem.ID = this.fpMat_Sheet1.Cells[this.fpMat_Sheet1.ActiveRowIndex, 0].Value.ToString();
            matItem.Name = this.fpMat_Sheet1.Cells[this.fpMat_Sheet1.ActiveRowIndex, 2].Value.ToString();
            matItem.SpellCode = this.fpMat_Sheet1.Cells[this.fpMat_Sheet1.ActiveRowIndex, 3].Value.ToString();
            matItem.WBCode = this.fpMat_Sheet1.Cells[this.fpMat_Sheet1.ActiveRowIndex, 4].Value.ToString();
            matItem.UserCode = this.fpMat_Sheet1.Cells[this.fpMat_Sheet1.ActiveRowIndex, 5].Value.ToString();
            matItem.GbCode = this.fpMat_Sheet1.Cells[this.fpMat_Sheet1.ActiveRowIndex, 6].Value.ToString();
            this.GetUndrugMat(matItem, undrug);


        }

        /// <summary>
        /// ͨ������Ĳ�ѯ�룬���������б�

        /// </summary>
        private void Filter(DataTable dt, string filter, DataView dv)
        {
            if (dt.Rows.Count == 0) return;

            try
            {



                //���ù�������
                dv.RowFilter = filter;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg(ex.Message));
            }
        }




        #endregion

        #region ������



        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("ɾ��", "ɾ�����ձ���Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            toolBarService.AddToolButton("ȷ��", "���ձ�Ƚϸ���", FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            toolBarService.AddToolButton("ˢ��", "ˢ�¶��ձ���Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.Sˢ��, true, false, null);



            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "ɾ��")
            {
                this.txtFiler3.Focus();
                this.DeleteViewData();
            }
            if (e.ClickedItem.Text == "ȷ��")
            {
                this.SetUndrugMat();
            }
            if (e.ClickedItem.Text == "ˢ��")
            {
                //this.fpUndrug_Sheet1.Rows[0].BackColor=this.fpUndrug_Sheet1.SelectionBackColor ;


                //this.fpUndrug_Sheet1.ActiveRowIndex = 0;
                //this.fpUndrug_Sheet1.SetActiveCell(0, 0);
                this.txtFiler3.Focus();
                //this.fpUndrug_Sheet1.SetActiveCell(0,0);

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڽ������ݳ�ʼ��...���Ժ�");
                Application.DoEvents();
                this.ShowCompareData();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }

            base.ToolStrip_ItemClicked(sender, e);
        }


        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();

            return 1;
        }






        #endregion

        #region �¼�
        private void ucUndrugMatCompare_Load(object sender, EventArgs e)
        {


            if (!this.DesignMode)
            {
                #region ���Ȩ��
                //FS.FrameWork.Models.NeuObject myPrivDept = new FS.FrameWork.Models.NeuObject();
                //int parma = FS.HISFC.Components.Common.Classes.Function.ChoosePivDept("0920", ref myPrivDept);

                //if (parma == -1)            //��Ȩ��
                //{
                //    MessageBox.Show(NFC.Management.Language.Msg("���޴˴��ڲ���Ȩ��"));
                //    return;
                //}
                //else if (parma == 0)       //�û�ѡ��ȡ��
                //{
                //    return;
                //}

                //this.workDept = myPrivDept;

                //base.OnStatusBarInfo(null, "�������ң� " + myPrivDept.Name);
                #endregion
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڽ������ݳ�ʼ��...���Ժ�");
                Application.DoEvents();


                this.Init();
                //this.InitFp();
                //this.InitList();

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();


            }

            this.fpUndrug_Sheet1.AddSelection(0, 0, 1, 46);
            this.fpMat_Sheet1.AddSelection(0, 0, 1, 30);
            this.txtFiler3.Focus();


        }

        private void txtFiler1_TextChanged(object sender, EventArgs e)
        {
            string queryCode = "";

            queryCode = "%" + this.txtFiler1.Text.Trim() + "%";
            string filter = "(��ҩƷƴ���� LIKE '" + queryCode + "') OR " +
                            "(��ҩƷ��� LIKE '" + queryCode + "') OR " +
                            "(��ҩƷ�Զ����� LIKE '" + queryCode + "') OR " +
                            "(����ƴ���� LIKE '" + queryCode + "') OR " +
                            "(��������� LIKE '" + queryCode + "') OR " +
                            "(�����Զ����� LIKE '" + queryCode + "') ";

            this.Filter(this.dt_compare, filter, this.dv_compare);
        }


        private void txtFiler2_TextChanged(object sender, EventArgs e)
        {
            //this.Filter(this.dt_mat, this.txtFiler2.Text.Trim());
            string queryCode = "";

            queryCode = "%" + this.txtFiler2.Text.Trim() + "%";
            string filter = "(ƴ���� LIKE '" + queryCode + "') OR " +
                            "(����� LIKE '" + queryCode + "') OR " +
                            "(�Զ����� LIKE '" + queryCode + "') ";

            this.Filter(this.dt_mat, filter, this.dv_mat);

        }

        private void txtFiler3_TextChanged(object sender, EventArgs e)
        {
            string queryCode = "";

            queryCode = "%" + this.txtFiler3.Text.Trim() + "%";
            string filter = "(ƴ���� LIKE '" + queryCode + "') OR " +
                            "(����� LIKE '" + queryCode + "') OR " +
                            "(������ LIKE '" + queryCode + "') ";

            this.Filter(this.dt_undrug, filter, this.dv_undrug);


        }

        /// <summary>
        /// ���ձ������޸�
        /// </summary>
        //private void fpCompare_CellDoubleClick(object sender, CellClickEventArgs e)
        //{
        //    ArrayList al = new ArrayList();
        //    for (int i = 0; i < this.fpCompare_Sheet1.ColumnCount; i++)
        //    {
        //        al.Add(this.fpCompare_Sheet1.Cells[this.fpCompare_Sheet1.ActiveRowIndex, i].Value);

        //    }

        //    al.Add(this.undrugMatManager.Operator.Name);
        //    this.frm.frmInit(al);
        //    this.frm.MyInput += new ucUndrugMatManager.SaveInput(frm_MyInput);

        //    this.frm.ShowDialog();
        //}


        private void txtFiler3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.txtFiler2.Focus();
            }
        }

        private void txtFiler2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.txtFiler1.Focus();
            }
        }

        #endregion

    }
}