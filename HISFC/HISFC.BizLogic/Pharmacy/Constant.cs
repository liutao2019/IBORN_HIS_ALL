using System;
using System.Collections;
using FS.HISFC.Models.Pharmacy;
using System.Collections.Generic;
using FS.FrameWork.Function;
using System.Data;

namespace FS.HISFC.BizLogic.Pharmacy
{
    /// <summary>
    /// [��������: ҩƷ�����г���ά��]<br></br>
    /// [�� �� ��: Cuip]<br></br>
    /// [����ʱ��: 2005-02]<br></br>
    /// <�޸ļ�¼>
    ///     1������ȡҩ�����ڲ����õĺ���
    /// </�޸ļ�¼>
    /// </summary>
    public class Constant : DataBase
    {
        public Constant()
        {

        }

        /// <summary>
        /// ȡϵͳҩƷ�����б�
        /// </summary>
        /// <returns>���󷵻�null����ȷ����Quality����</returns>
        public ArrayList QueryConstantQuality()
        {
            return FS.HISFC.Models.Pharmacy.DrugQualityEnumService.List();
        }

        #region ����ҩ������

        /// <summary>
        /// ��ѯ����ҩ������
        /// </summary>
        /// <returns>��������ҩ�������б�����</returns>
        public ArrayList QueryPhaFunction()
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.GetSQL("Pharmacy.Constant.GetComPhaFunction.all", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetComPhaFunction.all�ֶ�!";
                return null;
            }
            strSQL = string.Format(strSQL);
            //ִ��sql���
            this.ExecQuery(strSQL);
            ArrayList alist = new ArrayList();
            try
            {
                FS.HISFC.Models.Pharmacy.PhaFunction myFunction;
                while (this.Reader.Read())
                {

                    myFunction = new PhaFunction();
                    myFunction.ParentNode = this.Reader[0].ToString();							//0 ���ڵ�
                    myFunction.ID = this.Reader[1].ToString();								//1�ڵ�ID
                    myFunction.Name = this.Reader[2].ToString();								//2 �ڵ�����
                    myFunction.NodeKind = NConvert.ToInt32(this.Reader[3].ToString());            //3�ڵ�����
                    myFunction.GradeLevel = NConvert.ToInt32(this.Reader[4].ToString());        //4 ����
                    myFunction.SpellCode = this.Reader[5].ToString();							//5 ƴ����
                    myFunction.WBCode = this.Reader[6].ToString();							//6 �����
                    myFunction.SortID = NConvert.ToInt32(this.Reader[7].ToString());           //7 �Զ�����
                    myFunction.IsValid = NConvert.ToBoolean(this.Reader[8].ToString());							//8 ����
                    myFunction.Memo = this.Reader[11].ToString();								//11��ע      
                    this.ProgressBarValue++;
                    alist.Add(myFunction);
                }
            }
            catch (Exception ex)
            {
                this.Err = "ȡҩƷ����ҩ������ʧ��" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            //��������
            return alist;
        }

        /// <summary>
        /// ��ѯҩ������Ҷ�ӽڵ�����
        /// </summary>
        /// <returns>����ҩ������Ҷ�ӽڵ�����</returns>
        public ArrayList QueryPhaFunctionLeafage()
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.GetSQL("Pharmacy.Constant.GetComPhaFunction.all", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetComPhaFunction.all�ֶ�!";
                return null;
            }
            string strSQL1 = "";
            if (this.GetSQL("Pharmacy.Constant.GetComPhaFunction.Where.1", ref strSQL1) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetComPhaFunction.Where.1�ֶ�!";
                return null;
            }

            strSQL = strSQL + " " + strSQL1;

            strSQL = string.Format(strSQL);
            //ִ��sql���
            this.ExecQuery(strSQL);
            ArrayList alist = new ArrayList();
            try
            {
                FS.HISFC.Models.Pharmacy.PhaFunction myFunction;
                while (this.Reader.Read())
                {

                    myFunction = new PhaFunction();
                    myFunction.ParentNode = this.Reader[0].ToString();							//0 ���ڵ�
                    myFunction.ID = this.Reader[1].ToString();								//1�ڵ�ID
                    myFunction.Name = this.Reader[2].ToString();								//2 �ڵ�����
                    myFunction.NodeKind = NConvert.ToInt32(this.Reader[3].ToString());            //3�ڵ�����
                    myFunction.GradeLevel = NConvert.ToInt32(this.Reader[4].ToString());        //4 ����
                    myFunction.SpellCode = this.Reader[5].ToString();							//5 ƴ����
                    myFunction.WBCode = this.Reader[6].ToString();							//6 �����
                    myFunction.SortID = NConvert.ToInt32(this.Reader[7].ToString());           //7 �Զ�����
                    myFunction.IsValid = NConvert.ToBoolean(this.Reader[8].ToString());							//8 ����
                    myFunction.Memo = this.Reader[11].ToString();								//11��ע      
                    this.ProgressBarValue++;
                    alist.Add(myFunction);
                }
            }
            catch (Exception ex)
            {
                this.Err = "ȡҩƷ����ҩ������ʧ��" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            //��������
            return alist;
        }

        /// <summary>
        /// ���ݽڵ��Ų�ѯ����ҩ�����ó�������һ����¼
        /// </summary>
        /// <param name="nodecode"> ���нڵ�ı��</param>
        /// <returns>����arraylist ���� </returns>
        public ArrayList QueryFunctionByNode(string nodecode)
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.GetSQL("Pharmacy.Constant.GetComPhaFunction.ONE", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetComPhaFunction.ONE�ֶ�!";
                return null;
            }
            strSQL = string.Format(strSQL, nodecode);
            //ִ��sql���
            this.ExecQuery(strSQL);
            ArrayList alist = new ArrayList();
            try
            {
                FS.HISFC.Models.Pharmacy.PhaFunction myFunction;
                while (this.Reader.Read())
                {

                    myFunction = new PhaFunction();
                    myFunction.ParentNode = this.Reader[0].ToString();							//0 ���ڵ�
                    myFunction.ID = this.Reader[1].ToString();								//1�ڵ�ID
                    myFunction.Name = this.Reader[2].ToString();								//2 �ڵ�����
                    myFunction.NodeKind = NConvert.ToInt32(this.Reader[3].ToString());            //3�ڵ�����
                    myFunction.GradeLevel = NConvert.ToInt32(this.Reader[4].ToString());        //4 ����
                    myFunction.SpellCode = this.Reader[5].ToString();							//5 ƴ����
                    myFunction.WBCode = this.Reader[6].ToString();							//6 �����
                    myFunction.SortID = NConvert.ToInt32(this.Reader[7].ToString());           //7 �Զ�����
                    myFunction.IsValid = NConvert.ToBoolean(this.Reader[8].ToString());							//8 ����
                    myFunction.Memo = this.Reader[11].ToString();								//11��ע      
                    this.ProgressBarValue++;
                    alist.Add(myFunction);
                }
            }
            catch (Exception ex)
            {
                this.Err = "ȡҩƷҩ������ʧ��" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            //��������
            return alist;
        }

        /// <summary>
        /// ��ѯ��������Ҷ�ӽڵ�
        /// </summary>
        /// <returns>Ҷ�ӽڵ�����</returns>
        public ArrayList QueryPhaFunctionNodeName()
        {
            string strSQL = "";
            //ȡ���²�����SQL���
            if (this.GetSQL("Pharmacy.Constant.GetComPhaFunction.GETLASTNODENAME", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetComPhaFunction.GETLASTNODENAME�ֶ�!";
                return null;
            }
            strSQL = string.Format(strSQL);
            this.ExecQuery(strSQL);//�滻SQL����еĲ�����
            ArrayList alist = new ArrayList();
            try
            {
                FS.HISFC.Models.Pharmacy.PhaFunction myFunction;
                while (this.Reader.Read())
                {

                    myFunction = new PhaFunction();
                    myFunction.ParentNode = this.Reader[0].ToString();							//0 ���ڵ�
                    myFunction.ID = this.Reader[1].ToString();								//1�ڵ�ID
                    myFunction.Name = this.Reader[2].ToString();								//2 �ڵ���   
                    this.ProgressBarValue++;
                    alist.Add(myFunction);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Pharmacy.Constant.GetComPhaFunction.GETLASTNODENAME:" + ex.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            //��������
            return alist;
        }

        /// <summary>
        /// ���ݽڵ��Ų�ѯҩ�����ó�������һ����¼ 
        /// </summary>
        /// <param name="Pnodecode">���нڵ�ı��</param>
        /// <returns>����Ҷ�ӽڵ�����</returns>
        public ArrayList QueryFunctionByParentNode(string Pnodecode)
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.GetSQL("Pharmacy.Constant.GetComPhaFunction.BYPARENTNODE", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetComPhaFunction.BYPARENTNODE �ֶ�!";
                return null;
            }
            strSQL = string.Format(strSQL, Pnodecode);
            //ִ��sql���
            this.ExecQuery(strSQL);
            ArrayList alist = new ArrayList();
            try
            {
                FS.HISFC.Models.Pharmacy.PhaFunction myFunction;
                while (this.Reader.Read())
                {

                    myFunction = new PhaFunction();
                    myFunction.ParentNode = this.Reader[0].ToString();							//0 ���ڵ�
                    myFunction.ID = this.Reader[1].ToString();								//1�ڵ�ID
                    myFunction.Name = this.Reader[2].ToString();								//2 �ڵ�����
                    myFunction.NodeKind = NConvert.ToInt32(this.Reader[3].ToString());            //3�ڵ�����
                    myFunction.GradeLevel = NConvert.ToInt32(this.Reader[4].ToString());        //4 ����
                    myFunction.SpellCode = this.Reader[5].ToString();							//5 ƴ����
                    myFunction.WBCode = this.Reader[6].ToString();							//6 �����
                    myFunction.SortID = NConvert.ToInt32(this.Reader[7].ToString());           //7 �Զ�����
                    myFunction.IsValid = NConvert.ToBoolean(this.Reader[8].ToString());							//8 ����
                    myFunction.Memo = this.Reader[11].ToString();								//11��ע      
                    this.ProgressBarValue++;
                    alist.Add(myFunction);
                }
            }
            catch (Exception ex)
            {
                this.Err = "ȡҩƷҩ������ֵʧ��" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            //��������
            return alist;
        }

        /// <summary>
        /// ���ݲ�ͬ�ȼ���ȡҩ��������Ϣ   {6E41A9CD-AEDC-4aae-8E46-1F312F0FA4C6}
        /// </summary>
        /// <param name="functionLevel">ҩ�����õȼ�</param>
        /// <returns>�ɹ�����ҩ������</returns>
        public List<FS.HISFC.Models.Pharmacy.PhaFunction> QueryPhaFunctionByLevel(int functionLevel)
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.GetSQL( "Pharmacy.Constant.GetComPhaFunction", ref strSQL ) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetComPhaFunction �ֶ�!";
                return null;
            }
            string strWhere = "";
            //ȡWHERE���
            if (this.GetSQL( "Pharmacy.Constant.GetComPhaFunction.ByLevel", ref strWhere ) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetComPhaFunction.ByLevel�ֶ�!";
                return null;
            }

            strSQL = string.Format( strSQL + strWhere, functionLevel );

            //��������
            return this.ExecSqlForFunctionData( strSQL );
        }

        /// <summary>
        /// ���½ڵ����
        /// </summary>
        ///<param name="NodeCode"> �ڵ����</param>
        /// <param name="NodeKind">�ڵ����</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int UpdateFunctionnNodekind(string NodeCode, int NodeKind)
        {
            string strSQL = "";
            //ȡ���²�����SQL���
            if (this.GetSQL("Pharmacy.Constant.GetComPhaFunction.UPDATEPARENTNODEKIND", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetComPhaFunction.UPDATEPARENTNODEKIND�ֶ�!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, NodeCode, NodeKind);            //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Pharmacy.Constant.GetComPhaFunction.UPDATEPARENTNODEKIND:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ���½ڵ�����
        /// </summary>
        ///<param name="NodeCode">�ڵ����</param>
        /// <param name="NodeName">�ڵ�����</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int UpdateFunctionnNodeName(string NodeCode, string NodeName)
        {
            string strSQL = "";
            //ȡ���²�����SQL���
            if (this.GetSQL("Pharmacy.Constant.GetComPhaFunction.UPDATENODENAME", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetComPhaFunction.UPDATENODENAME�ֶ�!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, NodeCode, NodeName);            //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Pharmacy.Constant.GetComPhaFunction.UPDATENODENAME:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ��������ҩ�����ó�������һ����¼
        /// </summary>
        /// <param name="FunConstant">����ҩ��������չ������</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int UpdateFunction(FS.HISFC.Models.Pharmacy.PhaFunction FunConstant)
        {
            string strSQL = "";
            //ȡ���²�����SQL���
            if (this.GetSQL("Pharmacy.Constant.GetComPhaFunction.UPDATE", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetComPhaFunction.UPDATE�ֶ�!";
                return -1;
            }
            try
            {
                string[] strParm = myGetFunConstant(FunConstant);     //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);            //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Pharmacy.Constant.GetComPhaFunction.UPDATE:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ��������ҩ�����ó�������һ����¼
        /// </summary>
        /// <param name="FunConstant">����ҩ��������չ������</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int InsertFunction(FS.HISFC.Models.Pharmacy.PhaFunction FunConstant)
        {
            string strSQL = "";
            //ȡ���²�����SQL���
            if (this.GetSQL("Pharmacy.Constant.GetComPhaFunction.INSERT", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetComPhaFunction.INSERT�ֶ�!";
                return -1;
            }
            try
            {
                string[] strParm = myGetFunConstant(FunConstant);     //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);            //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Pharmacy.Constant.GetComPhaFunction.INSERT:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ɾ������ҩ�����ó�������һ����¼
        /// </summary>
        /// <param name="ID">����ҩ�����ýڵ�����</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int DeleteFunction(string ID)
        {
            string strSQL = "";
            //ȡɾ��������SQL���
            if (this.GetSQL("Pharmacy.Constant.GetComPhaFunction.DELETE", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetComPhaFunction.DELETE�ֶ�!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, ID);    //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Pharmacy.Constant.GetComPhaFunction.DELETE:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ִ��Sql����ȡ ҩ��������Ϣ����   {6E41A9CD-AEDC-4aae-8E46-1F312F0FA4C6}
        /// </summary>
        /// <param name="strSql">Sql���</param>
        /// <returns>�ɹ�����ҩ��������Ϣ���� ʧ�ܷ���null</returns>
        private List<FS.HISFC.Models.Pharmacy.PhaFunction> ExecSqlForFunctionData(string strSql)
        {
            //ִ��sql���
            if (this.ExecQuery( strSql ) == -1)
            {
                return null;
            }
            List<FS.HISFC.Models.Pharmacy.PhaFunction> alist = new List<FS.HISFC.Models.Pharmacy.PhaFunction>();
            try
            {
                FS.HISFC.Models.Pharmacy.PhaFunction myFunction;
                while (this.Reader.Read())
                {
                    myFunction = new PhaFunction();

                    myFunction.ParentNode = this.Reader[0].ToString();							//0 ���ڵ�
                    myFunction.ID = this.Reader[1].ToString();								    //1�ڵ�ID
                    myFunction.Name = this.Reader[2].ToString();								//2 �ڵ�����
                    myFunction.NodeKind = NConvert.ToInt32( this.Reader[3].ToString() );          //3�ڵ�����
                    myFunction.GradeLevel = NConvert.ToInt32( this.Reader[4].ToString() );        //4 ����
                    myFunction.SpellCode = this.Reader[5].ToString();							//5 ƴ����
                    myFunction.WBCode = this.Reader[6].ToString();							    //6 �����
                    myFunction.SortID = NConvert.ToInt32( this.Reader[7].ToString() );            //7 �Զ�����
                    myFunction.IsValid = NConvert.ToBoolean( this.Reader[8].ToString() );			//8 ����
                    myFunction.Memo = this.Reader[11].ToString();								//11��ע   

                    alist.Add( myFunction );
                }
            }
            catch (Exception ex)
            {
                this.Err = "ȡҩƷҩ������ֵʧ��" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return alist;
        }

        /// <summary>
        /// ���update����insert����ҩ�����ñ�Ĵ����������
        /// </summary>
        /// <param name="FunConstant">����ҩ�����ó���ʵ��</param>
        /// <returns>�ַ�������</returns>
        /// 
        public int SetFunction(FS.HISFC.Models.Pharmacy.PhaFunction FunConstant, string ifInUpDel)
        {
            int parm = 0;
            //ִ�и��²���
            if (ifInUpDel == "UPDATE")
            {
                parm = this.UpdateFunction(FunConstant);   //���²��� 
                this.UpdateFunctionnNodekind(FunConstant.ParentNode, 1);
            }
            if (ifInUpDel == "DELETE")
            {
                string pnode;
                FS.HISFC.Models.Pharmacy.PhaFunction functin = (FS.HISFC.Models.Pharmacy.PhaFunction)this.QueryFunctionByNode(FunConstant.ID)[0];//�ҳ����ڵ�
                pnode = functin.ParentNode;
                int i;
                i = this.QueryFunctionByParentNode(pnode).Count;
                if (i >= 2)//���ڵ���2��ɾ�����нڵ�
                {
                    this.UpdateFunctionnNodekind(FunConstant.ParentNode, 1);
                }
                else
                {
                    this.UpdateFunctionnNodekind(FunConstant.ParentNode, 0);//<2��û�нڵ�
                }
                parm = this.DeleteFunction(FunConstant.ID);//ɾ������

            }
            if (ifInUpDel == "INSERT")
            {
                parm = this.InsertFunction(FunConstant);    //�������
                this.UpdateFunctionnNodekind(FunConstant.ParentNode, 1);
            }

            return parm;
        }

        /// <summary>
        /// ����ʵ�����ȡ��������
        /// </summary>
        /// <param name="FunConstant"></param>
        /// <returns></returns>
        private string[] myGetFunConstant(FS.HISFC.Models.Pharmacy.PhaFunction FunConstant)
        {
            string[] strParm ={   
								 FunConstant.ID,						 //0 �ڵ����
								 FunConstant.ParentNode,                 //1 ���ڵ�
								 FunConstant.Name,                       //2 �ڵ�����
								 FunConstant.NodeKind.ToString(),        //3 nodekind (δ��)
								 FunConstant.GradeLevel.ToString(),       //4 �ڵ㼶�� gradecode(δ��)
								 FunConstant.SpellCode,                 //5 ���
								 FunConstant.WBCode,                    //6ƴ��
								 FunConstant.SortID.ToString(),          //7����
								 NConvert.ToInt32(FunConstant.IsValid).ToString(),                 //8��Ч��־
								 this.Operator.ID,		                 //9 ����Ա
								 FunConstant.Oper.OperTime.ToString(),        //10����ʱ��
								 FunConstant.Memo                        //��ע
							 };
            return strParm;
        }
        
        /// <summary>
        /// ��ȡҩ����������ҩƷ�б� by Sunjh 2009-6-2 {B11FB211-56F7-418e-A415-4B07617A0510}
        /// </summary>
        /// <param name="functionID">ҩ�����ô���</param>
        /// <returns>�ɹ� ҩƷ�б� ʧ�� null</returns>
        public List<FS.HISFC.Models.Pharmacy.Item> QueryItemListByFunctionID(string functionID, int functionLevl)
        {
            string sqlStr = "";

            if (this.GetSQL("Pharmacy.Constant.QueryItemListByFunction", ref sqlStr) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.QueryItemListByFunction�ֶ�!";
                return null;
            }

            sqlStr = string.Format(sqlStr, functionID, functionLevl);

            return this.myGetItemFunction(sqlStr);
        }

        /// <summary>
        /// ȡҩƷ���ֻ�����Ϣ�б� by Sunjh 2009-6-2 {B11FB211-56F7-418e-A415-4B07617A0510}
        /// </summary>
        /// <param name="SQLString">SQL���</param>
        /// <returns>�ɹ�����ҩƷ�������� ʧ�ܷ���null</returns>
        private List<FS.HISFC.Models.Pharmacy.Item> myGetItemFunction(string sqlStr)
        {
            List<FS.HISFC.Models.Pharmacy.Item> al = new List<FS.HISFC.Models.Pharmacy.Item>();
            if (this.ExecQuery(sqlStr) == -1)
            {
                return null;
            }

            try
            {
                FS.HISFC.Models.Pharmacy.Item Item; //���������е�ҩƷ��Ϣ��

                while (this.Reader.Read())
                {
                    Item = new FS.HISFC.Models.Pharmacy.Item();

                    Item.ID = this.Reader[0].ToString();                                  //0  ҩƷ����
                    Item.Name = this.Reader[1].ToString();                                //1  ��Ʒ����
                    Item.NameCollection.RegularName = this.Reader[2].ToString();         //9  ҩƷͨ����
                    Item.PackQty = NConvert.ToDecimal(this.Reader[3].ToString());         //5  ��װ����
                    Item.Specs = this.Reader[4].ToString();                               //6  ���
                    Item.SysClass.ID = this.Reader[5].ToString();                         //7  ϵͳ������
                    Item.MinFee.ID = this.Reader[6].ToString();                           //8  ��С���ô���
                    Item.PackUnit = this.Reader[7].ToString();                           //21 ��װ��λ
                    Item.MinUnit = this.Reader[8].ToString();                            //22 ��С��λ
                    Item.Type.ID = this.Reader[9].ToString();                            //26 ҩƷ������
                    Item.Quality.ID = this.Reader[10].ToString();                         //27 ҩƷ���ʱ���
                    Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[11].ToString());    //28 ���ۼ�
                    Item.ValidState = (FS.HISFC.Models.Base.EnumValidState)(NConvert.ToInt32(this.Reader[12]));
                    Item.UserCode = this.Reader[13].ToString();                           //4  �Զ�����
                    Item.NameCollection.EnglishName = this.Reader[14].ToString();        //16 Ӣ����Ʒ��        
                    Item.PhyFunction1.ID = this.Reader[15].ToString();
                    Item.PhyFunction2.ID = this.Reader[16].ToString();
                    Item.PhyFunction3.ID = this.Reader[17].ToString();


                    al.Add(Item);
                }
            }
            catch (Exception ex)
            {
                this.Err = "���ҩƷ������Ϣʱ��ִ��SQL������" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return al;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        #endregion

        #region ������˾

          /// <summary>
        /// �������Ͳ�ѯҩƷ��˾���������һ��߹�����˾��
        /// </summary>
        /// <param name="type">���ͣ�0�������ң�1������˾</param>
        /// <param name="isValid">�Ƿ��������Ч��˾</param>
        /// <returns>���󷵻�null</returns>
        public ArrayList QueryCompany(string type,bool isValid)
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.GetSQL("Pharmacy.Constant.GetCompany", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetCompany�ֶ�!";
                return null;
            }

            if (isValid)
            {
                strSQL = string.Format(strSQL, type, "1");
            }
            else
            {
                strSQL = string.Format(strSQL, type, "A");
            }

            //ִ��sql���
            this.ExecQuery(strSQL);
            ArrayList al = new ArrayList();
            try
            {
                FS.HISFC.Models.Pharmacy.Company company;
                while (this.Reader.Read())
                {
                    company = new Company();
                    company.ID = this.Reader[0].ToString();             //0 ��˾����
                    company.Name = this.Reader[1].ToString();             //1 ��˾����
                    company.RelationCollection.Address = this.Reader[2].ToString();         //2 ��ַ
                    company.RelationCollection.Relative = this.Reader[3].ToString();        //3 ��ϵ��ʽ
                    company.GMPInfo = this.Reader[4].ToString();         //4 GMP��Ϣ
                    company.GSPInfo = this.Reader[5].ToString();         //5 GSP��Ϣ
                    company.SpellCode = this.Reader[6].ToString();       //6 ƴ����
                    company.WBCode = this.Reader[7].ToString();          //7 �����
                    company.UserCode = this.Reader[8].ToString();        //8 �Զ�����
                    company.Type = this.Reader[9].ToString();            //9 ����
                    company.OpenBank = this.Reader[10].ToString();       //10 ��������
                    company.OpenAccounts = this.Reader[11].ToString();   //11 �����ʺ�
                    company.ActualRate = NConvert.ToDecimal(this.Reader[12].ToString());//12 �Ӽ���
                    company.Memo = this.Reader[13].ToString();           //13��ע      
                    company.IsValid = NConvert.ToBoolean(this.Reader[16].ToString());
                    this.ProgressBarValue++;
                    al.Add(company);
                }
            }
            catch (Exception ex)
            {
                this.Err = "ȡҩƷ��˾ʱ����" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            //��������
            return al;
        }

        /// <summary>
        /// �������Ͳ�ѯҩƷ��˾���������һ��߹�����˾��
        /// </summary>
        /// <param name="type">���ͣ�0�������ң�1������˾</param>
        /// <returns>���󷵻�null</returns>
        public ArrayList QueryCompany(string type)
        {
           return this.QueryCompany(type, true);
        }


        /// <summary>
        /// ���ݹ�����˾�����ȡ������˾ʵ��
        /// </summary>
        /// <param name="companyID">������˾����</param>
        /// <returns>�ɹ����ع�����˾ʵ�� ʧ�ܷ���null</returns>
        public Company QueryCompanyByCompanyID(string companyID)
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.GetSQL("Pharmacy.Constant.GetCompanyByID", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetCompanyByID�ֶ�!";
                return null;
            }

            strSQL = string.Format(strSQL, companyID);
            //ִ��sql���
            this.ExecQuery(strSQL);
            try
            {
                FS.HISFC.Models.Pharmacy.Company company = new Company();
                if (this.Reader.Read())
                {
                    company.ID = this.Reader[0].ToString();             //0 ��˾����
                    company.Name = this.Reader[1].ToString();             //1 ��˾����
                    company.RelationCollection.Address = this.Reader[2].ToString();         //2 ��ַ
                    company.RelationCollection.Relative = this.Reader[3].ToString();        //3 ��ϵ��ʽ
                    company.GMPInfo = this.Reader[4].ToString();         //4 GMP��Ϣ
                    company.GSPInfo = this.Reader[5].ToString();         //5 GSP��Ϣ
                    company.SpellCode = this.Reader[6].ToString();       //6 ƴ����
                    company.WBCode = this.Reader[7].ToString();          //7 �����
                    company.UserCode = this.Reader[8].ToString();        //8 �Զ�����
                    company.Type = this.Reader[9].ToString();            //9 ����
                    company.OpenBank = this.Reader[10].ToString();       //10 ��������
                    company.OpenAccounts = this.Reader[11].ToString();   //11 �����ʺ�
                    company.ActualRate = NConvert.ToDecimal(this.Reader[12].ToString());//12 �Ӽ���
                    company.Memo = this.Reader[13].ToString();           //13��ע   
                    company.IsValid = NConvert.ToBoolean(this.Reader[16].ToString());
                }

                if (company.Name == "")
                {
                    this.Err = "������˾������ ���룺" + companyID;
                    return null;
                }

                return company;
            }
            catch (Exception ex)
            {
                this.Err = "ȡҩƷ��˾ʱ����" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

        }


        /// <summary>
        /// ���¹�˾��Ϣ���Թ�˾����Ϊ����
        /// </summary>
        /// <param name="company">��˾��Ϣ</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int UpdateCompany(FS.HISFC.Models.Pharmacy.Company company)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Constant.UpdateCompany", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.UpdateCompany�ֶ�!";
                return -1;
            }

            try
            {
                string[] strParm = myGetParmItem(company);  //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);       //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }

            //ִ��sql���
            return this.ExecNoQuery(strSQL);
        }


        /// <summary>
        /// ��˾���в���һ����¼����˾�������oracle�е����к�
        /// </summary>
        /// <param name="company">��˾��Ϣ</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int InsertCompany(FS.HISFC.Models.Pharmacy.Company company)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Constant.InsertCompany", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.InsertCompany�ֶ�!";
                return -1;
            }

            try
            {
                //ȡ��ˮ��
                company.ID = this.GetSequence("Pharmacy.Constant.GetNewCompanyID");
                if (company.ID == null) return -1;

                string[] strParm = myGetParmItem(company);  //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);    //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ��˾���в���һ����¼����˾�������oracle�е����к�
        /// </summary>
        /// <param name="company">��˾��Ϣ</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int InsertCompany(FS.HISFC.Models.Pharmacy.Company company,out string companyID)
        {
            string strSQL = "";
            companyID = "";
            if (this.GetSQL("Pharmacy.Constant.InsertCompany", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.InsertCompany�ֶ�!";
                return -1;
            }

            try
            {
                //ȡ��ˮ��
                company.ID = this.GetSequence("Pharmacy.Constant.GetNewCompanyID");                
                if (company.ID == null) return -1;
                companyID = company.ID;

                string[] strParm = myGetParmItem(company);  //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);    //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }


        /// <summary>
        /// ���湫˾���ݣ�����ִ�и��²��������û���ҵ����Ը��µ����ݣ������һ���¼�¼
        /// </summary>
        /// <param name="company">��˾ʵ��</param>
        /// <returns>1�ɹ� -1ʧ��</returns>
        public int SetCompany(FS.HISFC.Models.Pharmacy.Company company)
        {
            int parm;
            //ִ�и��²���
            parm = UpdateCompany(company);

            //���û���ҵ����Ը��µ����ݣ������һ���¼�¼
            if (parm == 0)
            {
                parm = InsertCompany(company);
            }
            return parm;
        }

        /// <summary>
        /// ɾ����˾��Ϣ
        /// </summary>
        /// <param name="ID">ҩƷ����</param>
        /// <returns>0û��ɾ�� 1�ɹ� -1ʧ��</returns>
        public int DeleteCompany(string ID)
        {
            string strSQL = ""; //����ҩƷ����ɾ��ĳһҩƷ��Ϣ��DELETE���
            if (this.GetSQL("Pharmacy.Constant.DeleteCompany", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.DeleteCompany�ֶ�!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, ID);
            }
            catch
            {
                this.Err = "����������ԣ�Pharmacy.Constant.DeleteCompany";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }


        /// <summary>
        /// ���update����insert��˾��Ĵ����������
        /// </summary>
        /// <param name="company">��˾��Ϣ</param>
        /// <returns>��������</returns>
        private string[] myGetParmItem(FS.HISFC.Models.Pharmacy.Company company)
        {

            string[] strParm ={   
								 company.ID,          //0 ��˾����
								 company.Name,        //1 ��˾����
								 company.RelationCollection.Address,     //2 ��ַ
								 company.RelationCollection.Relative,    //3 ��ϵ��ʽ
								 company.GMPInfo,     //4 GMP��Ϣ
								 company.GSPInfo,     //5 GSP��Ϣ
								 company.SpellCode,   //6 ƴ����
								 company.WBCode,      //7 �����
								 company.UserCode,    //8 �Զ�����
								 company.Type,        //9 ����
								 company.OpenBank,    //10 ��������
								 company.OpenAccounts,//11 �����ʺ�
								 company.ActualRate.ToString(),  //12 �Ӽ���
								 company.Memo,        //13 ��ע
								 this.Operator.ID,     //14 ����Ա
                                 NConvert.ToInt32(company.IsValid).ToString()
							 };
            return strParm;
        }

        #endregion

        #region ȡҩ����
        /// <summary>
        /// ����ҩ��ҩ�������ȡҩ��������
        /// </summary>
        /// <param name="ID">���ұ���</param>
        /// <returns>neuObject���飬ȡҩ���ұ�ţ�ȡҩ�������ƣ���ע������Ա������ʱ��</returns>
        public ArrayList QueryReciveDrugDept(string ID)
        {
            string strSQL = "";  //ȡĳһȡҩ�������ƻ�ÿ���ȡҩ��ҩ�������б��SQL���
            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.GetDrugRoomCode", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetDrugRoomCode�ֶ�!";
                return null;
            }

            strSQL = string.Format(strSQL, ID);
            //����SQL���ȡ���鲢��������
            ArrayList arrayObject = new ArrayList();

            this.ProgressBarText = "���ڼ���ȡҩ����������Ϣ...";
            this.ProgressBarValue = 0;

            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "����ȡҩ���������б�ʱ����" + this.Err;
                return null;
            }
            try
            {
                //	{ȡҩ���ұ��,ȡҩ��������,����Ա���,����Ա����,��������,��ע,rowid}
                //FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Base.Spell obj = new FS.HISFC.Models.Base.Spell();
                    //obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = this.Reader[0].ToString();					//ȡҩ���ұ��
                    obj.Name = this.Reader[1].ToString();				//ȡҩ��������
                    obj.Memo = this.Reader[2].ToString();				//��ע
                    obj.User01 = this.Reader[3].ToString();				//��ʼʱ��
                    obj.User02 = this.Reader[4].ToString();				//����ʱ��
                    obj.User03 = this.Reader[5].ToString();				//ҩƷ����
                    if (Reader.FieldCount > 6)
                    {
                        obj.SpellCode = this.Reader[6].ToString();				//ƴ����
                    }
                    if (Reader.FieldCount > 7)
                    {
                        obj.WBCode = this.Reader[7].ToString();				//�����
                    }
                    this.ProgressBarValue++;
                    arrayObject.Add(obj);
                }
            }//�׳�����
            catch (Exception ex)
            {
                this.Err = "ȡҩҩ�������б�ʱ��ִ��SQL������myGetDrugRoomCode" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            this.ProgressBarValue = -1;
            return arrayObject;
        }


        /// <summary>
        /// ���ݲ������롢ҩƷ���ͻ�ȡ��ҩ����
        /// </summary>
        /// <param name="roomCode">ȡҩ����</param>
        /// <param name="drugType">ҩƷ����</param>
        /// <returns>�ɹ�����ȡҩ��������(ID ���� Name ����) ʧ�ܷ���null</returns>
        public ArrayList QueryReciveDrugDept(string roomCode, string drugType)
        {
            string strSQL = "";
            ArrayList arrayObject = new ArrayList();
            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.GetDeptCode", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetDeptCode�ֶ�!";
                return null;
            }

            strSQL = string.Format(strSQL, roomCode, drugType);
            //����SQL���ȡ���鲢��������

            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "��ȡ��ҩҩ������" + this.Err;
                return null;
            }
            try
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                while (this.Reader.Read())
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = this.Reader[0].ToString();					//ȡҩ���ұ��
                    obj.Name = this.Reader[1].ToString();				//ȡҩ��������
                    arrayObject.Add(obj);
                }
            }//�׳�����
            catch (Exception ex)
            {
                this.Err = "ȡҩҩ�������б�ʱ��ִ��SQL������myGetDrugRoomCode" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return arrayObject;
        }

        /// <summary>
        /// ���ݲ������롢ҩƷ���ͻ�ȡ��ҩ����
        /// </summary>
        /// <param name="deptCode">ȡҩ����</param>
        /// <param name="drugType">ҩƷ����</param>
        /// <returns>�ɹ�����ȡҩ������Ϣ(ID ���� Name ����) ʧ�ܷ���null</returns>
        public List<FS.FrameWork.Models.NeuObject> GetRecipeDrugDept(string deptCode, string drugType)
        {
            string strSQL = "";
            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.GetDeptCode", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetDeptCode�ֶ�!";
                return null;
            }

            strSQL = string.Format(strSQL, deptCode, drugType);
            //����SQL���ȡ���鲢��������

            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "��ȡ��ҩҩ������" + this.Err;
                return null;
            }

            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            List<FS.FrameWork.Models.NeuObject> alStockDept = new List<FS.FrameWork.Models.NeuObject>();
            try
            {                
                while (this.Reader.Read())
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = this.Reader[0].ToString();					//��ҩ���ұ��
                    obj.Name = this.Reader[1].ToString();				//��ҩ��������

                    alStockDept.Add(obj);
                }
            }//�׳�����
            catch (Exception ex)
            {
                this.Err = "ȡҩҩ�������б�ʱ��ִ��SQL������myGetDrugRoomCode" + ex.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return alStockDept;
        }


        /// <summary>
        /// �����ύɸѡ�����롢���¡�ɾ����
        /// </summary>
        /// <param name="drugRoomList">�����б�</param>
        /// <param name="i">������־��0����1ɾ��2����</param>
        [System.Obsolete("�������ú���",true)]
        public void DrugRoomControl(ArrayList drugRoomList, int i)
        {
            try
            {
                switch (i)
                {
                    case 0:
                        foreach (FS.FrameWork.Models.NeuObject obj in drugRoomList)
                        {
                            this.InsertDrugRoom(obj);			//��������
                        }
                        break;
                    case 1:
                        foreach (FS.FrameWork.Models.NeuObject obj in drugRoomList)
                        {
                            this.DelSpeDrugRoom(obj.User03);	//ɾ������
                        }
                        break;
                    case 2:
                        foreach (FS.FrameWork.Models.NeuObject obj in drugRoomList)
                        {
                            this.UpdateDrugRoom(obj);			//��������
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                this.Err = "���ݱ������" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
            }
        }


        /// <summary>
        /// ����ȡҩ���ұ��
        /// </summary>
        /// <param name="obj">����ʵ��</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int InsertDrugRoom(FS.FrameWork.Models.NeuObject obj)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Constant.InsertDrugRoom", ref strSQL) == -1) return -1;
            try
            {
                //
                if (obj.ID == null) return -1;
                //�滻SQL����еĲ�����
                strSQL = string.Format(strSQL,
                    obj.ID,				//ҩ��ҩ�����
                    obj.Name,			//ȡҩ���ұ���
                    this.Operator.ID,	//����Ա
                    obj.Memo,			//��ע
                    obj.User03,			//ҩƷ����
                    obj.User01,			//��ʼʱ��
                    obj.User02);		//����ʱ��
            }
            catch (Exception ex)
            {
                this.Err = "��������ʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }


        /// <summary>
        /// ɾ��ָ��ȡҩ���ұ��
        /// </summary>
        /// <param name="rowid">rowid</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        [System.Obsolete("�������ú���", true)]
        public int DelSpeDrugRoom(string rowid)
        {
            string strSQL = "";
            //����rowidɾ��ĳһ��ȡҩ���ҵ�DELETE���
            if (this.GetSQL("Pharmacy.Constant.DelSpeDrugRoom", ref strSQL) == -1)
            {
                this.Err = "�Ҳ���SQL��䣡Pharmacy.Constant.DelSpeDrugRoom";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, rowid);
            }
            catch
            {
                this.Err = "����ɾ������Pharmacy.Constant.DelSpeDrugRoom";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }


        /// <summary>
        /// ɾ��ĳҩ��/ҩ��������ȡҩ����
        /// </summary>
        /// <param name="ID">ȡҩ���ұ��</param>
        /// <returns>0û��ɾ�� 1�ɹ� -1ʧ��</returns>
        public int DelAllDrugRoom(string ID)
        {
            string strSQL = "";
            //����ҩ��/ҩ����ɾ��ȡҩ���ҵ�DELETE���
            if (this.GetSQL("Pharmacy.Constant.DelAllDrugRoom", ref strSQL) == -1)
            {
                this.Err = "�Ҳ���SQL��䣡Pharmacy.Constant.DelAllDrugRoom";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, ID);
            }
            catch
            {
                this.Err = "����ɾ������Pharmacy.Constant.DelAllDrugRoom";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }


        /// <summary>
        /// ����ȡҩ������Ϣ
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        [System.Obsolete("�������ú���", true)]
        public int UpdateDrugRoom(FS.FrameWork.Models.NeuObject obj)
        {
            string strSQL = "";
            //����ȡҩ������Ϣ
            if (this.GetSQL("Pharmacy.Constant.UpdateDrugRoom", ref strSQL) == -1)
            {
                this.Err = "�Ҳ���SQL��䣡Pharmacy.Constant.UpdateDrugRoom";
                return -1;
            }
            if (obj.ID == null) return -1;
            string[] strParm = { obj.User03, obj.ID, obj.Name, this.Operator.ID, obj.Memo, obj.User01, obj.User02 };  //ȡ�����б�
            try
            {
                strSQL = string.Format(strSQL, strParm);        //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "���³����¼��SQl������ֵʱ����Pharmacy.Constant.UpdateDrugRoom" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }
        #endregion

        #region ���ҳ���  {59C9BD46-05E6-43f6-82F3-C0E3B53155CB} ��������ⵥ��ά��

        /// <summary>
        /// ���ݿ��ұ���ȡһ�����ҳ�����Ϣ
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <returns>���ҳ���</returns>
        public FS.HISFC.Models.Pharmacy.DeptConstant QueryDeptConstant(string deptCode)
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.GetSQL("Pharmacy.Constant.GetDeptConstant", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetDeptConstant�ֶ�!";
                return null;
            }

            string strWhere = "";
            //ȡWHERE���
            if (this.GetSQL("Pharmacy.Constant.GetDeptConstant.Where", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetDeptConstant.Where�ֶ�!";
                return null;
            }

            //��ʽ��SQL���
            try
            {
                strSQL += " " + strWhere;
                strSQL = string.Format(strSQL, deptCode);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Pharmacy.Constant.GetDeptConstant.Where:" + ex.Message;
                return null;
            }

            //ȡ���ҳ���
            ArrayList al = this.myGetDeptConstant(strSQL);
            if (al == null)
                return null;

            if (al.Count == 0)
                return new FS.HISFC.Models.Pharmacy.DeptConstant();

            return al[0] as FS.HISFC.Models.Pharmacy.DeptConstant;
        }

        /// <summary>
        /// ȡ���ҳ����б�
        /// </summary>
        /// <returns>���ҳ������飬������null</returns>
        public ArrayList QueryDeptConstantList()
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.GetSQL("Pharmacy.Constant.GetDeptConstant", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetDeptConstant�ֶ�!";
                return null;
            }

            //ȡ���ҳ�������
            return this.myGetDeptConstant(strSQL);
        }

        /// <summary>
        /// �Ƿ����Ź�����
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public bool IsManageBatch(string deptCode)
        {
            //ȡ���ҳ���ʵ��
            FS.HISFC.Models.Pharmacy.DeptConstant constant = QueryDeptConstant(deptCode);
            if (constant == null)
                //������false
                return false;
            else
                //�����Ƿ����Ź�����
                return constant.IsBatch;
        }

        /// <summary>
        /// �Ƿ����Ź�����
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public bool IsManageStore(string deptCode)
        {
            //ȡ���ҳ���ʵ��
            FS.HISFC.Models.Pharmacy.DeptConstant constant = QueryDeptConstant(deptCode);
            if (constant == null)
                //������false
                return false;
            else
                //�����Ƿ����Ź�����
                return constant.IsStore;
        }

        /// <summary>
        /// ����ҳ������в���һ����¼
        /// </summary>
        /// <param name="DeptConstant">������չ������</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int InsertDeptConstant(FS.HISFC.Models.Pharmacy.DeptConstant DeptConstant)
        {
            string strSQL = "";
            //ȡ���������SQL���
            if (this.GetSQL("Pharmacy.Constant.InsertDeptConstant", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.InsertDeptConstant�ֶ�!";
                return -1;
            }
            try
            {
                //ȡ��ˮ��
                ///DeptConstant.ID = this.GetSequence("Pharmacy.Constant.GetConstantID");
                //if (DeptConstant.ID == null) return -1;

                string[] strParm = myGetParmDeptConstant(DeptConstant);     //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);            //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Pharmacy.Constant.InsertDeptConstant:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ���¿��ҳ�������һ����¼
        /// </summary>
        /// <param name="DeptConstant">������չ������</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int UpdateDeptConstant(FS.HISFC.Models.Pharmacy.DeptConstant DeptConstant)
        {
            string strSQL = "";
            //ȡ���²�����SQL���
            if (this.GetSQL("Pharmacy.Constant.UpdateDeptConstant", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.UpdateDeptConstant�ֶ�!";
                return -1;
            }
            try
            {
                string[] strParm = myGetParmDeptConstant(DeptConstant);     //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);            //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Pharmacy.Constant.UpdateDeptConstant:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ɾ�����ҳ�������һ����¼
        /// </summary>
        /// <param name="ID">��ˮ��</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int DeleteDeptConstant(string ID)
        {
            string strSQL = "";
            //ȡɾ��������SQL���
            if (this.GetSQL("Pharmacy.Constant.DeleteDeptConstant", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.DeleteDeptConstant�ֶ�!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, ID);    //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Pharmacy.Constant.DeleteDeptConstant:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ������Ա���Ա䶯���ݣ�����ִ�и��²��������û���ҵ����Ը��µ����ݣ������һ���¼�¼
        /// </summary>
        /// <param name="DeptConstant">���ҳ���ʵ��</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int SetDeptConstant(FS.HISFC.Models.Pharmacy.DeptConstant DeptConstant)
        {
            int parm;
            //ִ�и��²���
            parm = UpdateDeptConstant(DeptConstant);

            //���û���ҵ����Ը��µ����ݣ������һ���¼�¼
            if (parm == 0)
            {
                parm = InsertDeptConstant(DeptConstant);
            }
            return parm;
        }

        /// <summary>
        /// ȡ���ҳ����б�������һ�����߶���
        /// ˽�з����������������е���
        /// </summary>
        /// <param name="SQLString">SQL���</param>
        /// <returns>���ҳ�����Ϣ��������</returns>
        private ArrayList myGetDeptConstant(string SQLString)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Pharmacy.DeptConstant DeptConstant; //���ҳ���ʵ��

            //ִ�в�ѯ���
            if (this.ExecQuery(SQLString) == -1)
            {
                this.Err = "��ÿ��ҳ���ʱ��ִ��SQL������" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    //ȡ��ѯ����еļ�¼
                    DeptConstant = new FS.HISFC.Models.Pharmacy.DeptConstant();

                    DeptConstant.ID = this.Reader[0].ToString(); //0 ���ű���
                    DeptConstant.Name = this.Reader[1].ToString(); //1 ��������

                    DeptConstant.StoreMaxDays = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[2].ToString());   //2 �ⷿ��߿����(��)
                    DeptConstant.StoreMinDays = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[3].ToString());   //3 �ⷿ��Ϳ����(��)
                    DeptConstant.ReferenceDays = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[4].ToString());   //4 �ⷿ��Ϳ����(��)
                    DeptConstant.IsBatch = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[5].ToString()); //5 �Ƿ����Ź���ҩƷ
                    DeptConstant.IsStore = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[6].ToString()); //6 �Ƿ����ҩƷ���
                    DeptConstant.UnitFlag = this.Reader[7].ToString(); //7 ������ʱĬ�ϵĵ�λ��1��װ��λ��0��С��λ
                    DeptConstant.User01 = this.Reader[8].ToString(); //8 ����Ա����
                    DeptConstant.User02 = this.Reader[9].ToString(); //9 ����ʱ��
                    DeptConstant.IsArk = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[10]);

                    DeptConstant.InListNO = this.Reader[11].ToString();         //8 ��ⵥ�ݺ�
                    DeptConstant.OutListNO = this.Reader[12].ToString();        //9 ���ⵥ�ݺ�

                    al.Add(DeptConstant);
                }
            }//�׳�����
            catch (Exception ex)
            {
                this.Err = "��ÿ��ҳ�����Ϣʱ����" + ex.Message;
                this.ErrCode = "-1";
                this.Reader.Close();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// ���update����insert���ҳ�����Ĵ����������
        /// </summary>
        /// <param name="DeptConstant">���ҳ���ʵ��</param>
        /// <returns>�ַ�������</returns>
        private string[] myGetParmDeptConstant(FS.HISFC.Models.Pharmacy.DeptConstant DeptConstant)
        {
            string[] strParm ={   
								 DeptConstant.ID,						 //0 ���ұ���
								 DeptConstant.StoreMaxDays.ToString(),   //1 �ⷿ��߿����(��)
								 DeptConstant.StoreMinDays.ToString(),   //2 �ⷿ��Ϳ����(��)
								 DeptConstant.ReferenceDays.ToString(),  //3 �ο�����
								 FS.FrameWork.Function.NConvert.ToInt32(DeptConstant.IsBatch).ToString(), //4 �Ƿ����Ź���ҩƷ
								 FS.FrameWork.Function.NConvert.ToInt32(DeptConstant.IsStore).ToString(), //5 �Ƿ����ҩƷ���
								 DeptConstant.UnitFlag,					//6 ������ʱĬ�ϵĵ�λ��1��װ��λ��0��С��λ
								 this.Operator.ID,						//7 ����Ա����׼�����ϣ�
                                 NConvert.ToInt32(DeptConstant.IsArk).ToString(),
                                 DeptConstant.InListNO,                 //9 ��ⵥ�ݺ�
                                 DeptConstant.OutListNO                 //10 ���ⵥ�ݺ�
							 };
            return strParm;
        }


        #endregion

        #region ҩƷģ��

        /// <summary>
        /// ����ʵ����Ϣ��ȡInsert��Update����������
        /// </summary>
        /// <param name="drugStencil">ģ��ʵ����Ϣ</param>
        /// <returns>�ɹ����ز������� ʧ�ܷ���null</returns>
        private string[] myGetDrugStencilParam(FS.HISFC.Models.Pharmacy.DrugStencil drugStencil)
        {
            string stencilType = drugStencil.OpenType.ID;
            if (drugStencil.OpenType.ID == FS.HISFC.Models.Pharmacy.EnumDrugStencil.Custom.ToString())
            {
                stencilType = drugStencil.Stencil.Memo;
            }
            string[] strParm = new string[]{
												drugStencil.Dept.ID,
												stencilType,
												drugStencil.Stencil.ID,
												drugStencil.Stencil.Name,
												drugStencil.Item.ID,
												drugStencil.Item.Name,
												drugStencil.Item.Specs,
												drugStencil.SortNO.ToString(),
												this.Operator.ID,
												drugStencil.Extend
										   };
            return strParm;
        }

        /// <summary>
        /// ִ��Sql��� ��ȡʵ����Ϣ����
        /// </summary>
        /// <param name="strSql">��ִ�е�Sql���</param>
        /// <returns>�ɹ�����ʵ����Ϣ���� ʧ�ܷ���null</returns>
        private ArrayList myGetDrugStencil(string strSql)
        {
            ArrayList al = new ArrayList();
            DrugStencil drugStencil = new DrugStencil();

            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "ִ��" + strSql + "��������" + this.Err;
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    drugStencil = new DrugStencil();
                    drugStencil.Dept.ID = this.Reader[0].ToString();

                    string stencilType = this.Reader[1].ToString();
                    switch (stencilType)
                    {
                        case "Plan":
                            drugStencil.OpenType.ID = stencilType;
                            break;
                        case "Check":
                            drugStencil.OpenType.ID = stencilType;
                            break;
                        case "Apply":
                            drugStencil.OpenType.ID = stencilType;
                            break;
                        default:
                            drugStencil.OpenType.ID = FS.HISFC.Models.Pharmacy.EnumDrugStencil.Custom.ToString(); ;
                            drugStencil.Stencil.Memo = stencilType;
                            break;

                    }
                    drugStencil.Stencil.ID = this.Reader[2].ToString();
                    drugStencil.Stencil.Name = this.Reader[3].ToString();
                    drugStencil.Item.ID = this.Reader[4].ToString();
                    drugStencil.Item.Name = this.Reader[5].ToString();
                    drugStencil.Item.Specs = this.Reader[6].ToString();
                    drugStencil.SortNO = NConvert.ToInt32(this.Reader[7].ToString());
                    drugStencil.Oper.ID = this.Reader[8].ToString();
                    drugStencil.Oper.OperTime = NConvert.ToDateTime(this.Reader[9].ToString());
                    drugStencil.Extend = this.Reader[10].ToString();

                    al.Add(drugStencil);
                }
            }
            catch
            {
                this.Err = "��Reader�ڶ�ȡ���ݷ����쳣";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// ִ��Sql��� ��ȡʵ����Ϣ����
        /// </summary>
        /// <param name="strSql">��ִ�е�Sql���</param>
        /// <returns>�ɹ�����ʵ����Ϣ���� ʧ�ܷ���null</returns>
        private ArrayList myGetDrugStencilList(string strSql)
        {
            ArrayList al = new ArrayList();
            DrugStencil drugStencil = new DrugStencil();

            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "ִ��" + strSql + "��������" + this.Err;
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    drugStencil = new DrugStencil();
                    drugStencil.Dept.ID = this.Reader[0].ToString();

                    string stencilType = this.Reader[1].ToString();
                    switch (stencilType)
                    {
                        case "Plan":
                            drugStencil.OpenType.ID = stencilType;
                            break;
                        case "Check":
                            drugStencil.OpenType.ID = stencilType;
                            break;
                        case "Apply":
                            drugStencil.OpenType.ID = stencilType;
                            break;
                        default:
                            drugStencil.OpenType.ID = FS.HISFC.Models.Pharmacy.EnumDrugStencil.Custom.ToString();
                            drugStencil.Stencil.Memo = stencilType;
                            break;

                    }
                    //drugStencil.OpenType.ID = this.Reader[1].ToString();
                    drugStencil.Stencil.ID = this.Reader[2].ToString();
                    drugStencil.Stencil.Name = this.Reader[3].ToString();

                    al.Add(drugStencil);
                }
            }
            catch
            {
                this.Err = "��Reader�ڶ�ȡ���ݷ����쳣";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// ��ȡ��ģ����
        /// </summary>
        /// <returns>�ɹ�������ģ����ˮ�� ʧ�ܷ���null</returns>
        public string GetNewStencilNO()
        {
            return this.GetSequence("Pharmacy.Constant.GetNewCompanyID");
        }

        /// <summary>
        /// ���ݿ��ұ����ȡ����ģ����Ϣ
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="openType">ģ������</param>
        /// <returns>�ɹ����ؿ���ģ����Ϣ</returns>
        public ArrayList QueryDrugStencilList(string deptCode, FS.HISFC.Models.Pharmacy.EnumDrugStencil openType)
        {
            string strSQL = "";  //ȡĳһȡҩ�������ƻ�ÿ���ȡҩ��ҩ�������б��SQL���
            string strWhere = "";
            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.GetDrugOpenList", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetDrugOpenList�ֶ�!";
                return null;
            }
            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.GetDrugOpenList.Type", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetDrugOpenList.Type�ֶ�!";
                return null;
            }

            strSQL = string.Format(strSQL + strWhere, deptCode, openType.ToString());

            return this.myGetDrugStencilList(strSQL);
        }

        /// <summary>
        /// ���ݿ��ұ����ȡ����ģ����Ϣ
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="openType">ģ������</param>
        /// <returns>�ɹ����ؿ���ģ����Ϣ</returns>
        public ArrayList QueryDrugStencilList(string deptCode, string openType)
        {
            string strSQL = "";  //ȡĳһȡҩ�������ƻ�ÿ���ȡҩ��ҩ�������б��SQL���
            string strWhere = "";
            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.GetDrugOpenList", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetDrugOpenList�ֶ�!";
                return null;
            }
            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.GetDrugOpenList.Type", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetDrugOpenList.Type�ֶ�!";
                return null;
            }

            strSQL = string.Format(strSQL + strWhere, deptCode, openType);

            return this.myGetDrugStencilList(strSQL);
        }

        /// <summary>
        /// ���ݿ��ұ����ȡ����ģ����Ϣ
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <returns>�ɹ����ؿ���ģ����Ϣ</returns>
        public ArrayList QueryDrugStencilList(string deptCode)
        {
            string strSQL = "";  //ȡĳһȡҩ�������ƻ�ÿ���ȡҩ��ҩ�������б��SQL���
            string strWhere = "";
            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.GetDrugOpenList", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetDrugOpenList�ֶ�!";
                return null;
            }
            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.GetDrugOpenList.Type", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetDrugOpenList.Type�ֶ�!";
                return null;
            }

            strSQL = string.Format(strSQL + strWhere, deptCode, "AAAA");

            return this.myGetDrugStencilList(strSQL);
        }

        /// <summary>
        /// ����ģ������ȡģ����ϸ
        /// </summary>
        /// <param name="stencilCode">ģ�����</param>
        /// <returns>�ɹ�����ģ����ϸ��Ϣ</returns>
        public ArrayList QueryDrugStencil(string stencilCode)
        {
            string strSQL = "";  //ȡĳһȡҩ�������ƻ�ÿ���ȡҩ��ҩ�������б��SQL���
            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.GetDrugOpenDetail", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetDrugOpenDetail�ֶ�!";
                return null;
            }

            strSQL = string.Format(strSQL, stencilCode);

            return this.myGetDrugStencil(strSQL);
        }

        /// <summary>
        /// ģ��ɾ��  ɾ������ģ��
        /// </summary>
        /// <param name="stencilCode">ģ�����</param>
        /// <returns>�ɹ�������Ӱ������  ʧ�ܷ���-1</returns>
        public int DelDrugStencil(string stencilCode)
        {
            string strSQL = "";  //ȡĳһȡҩ�������ƻ�ÿ���ȡҩ��ҩ�������б��SQL���
            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.DelDrugOpen.Stencil", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.DelDrugOpen.Stencil�ֶ�!";
                return -1;
            }

            strSQL = string.Format(strSQL, stencilCode);

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ģ��ɾ��
        /// </summary>
        /// <param name="stencilCode">ģ�����</param>
        /// <param name="drugCode">ҩƷ����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int DelDrugStencil(string stencilCode, string drugCode)
        {
            string strSQL = "";  //ȡĳһȡҩ�������ƻ�ÿ���ȡҩ��ҩ�������б��SQL���
            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.DelDrugOpen.Detail", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.DelDrugOpen.Detail�ֶ�!";
                return -1;
            }

            strSQL = string.Format(strSQL, stencilCode, drugCode);

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="drugStencil">ҩƷģ����Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int InsertDrugStencil(FS.HISFC.Models.Pharmacy.DrugStencil drugStencil)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Constant.InsertDrugOpen", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = myGetDrugStencilParam(drugStencil);  //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);    //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="drugStencil">ҩƷģ����Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int UpdateDrugStencil(FS.HISFC.Models.Pharmacy.DrugStencil drugStencil)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Constant.UpdateDrugOpen", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = myGetDrugStencilParam(drugStencil);  //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);    //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ����ģ����� �޸�ģ������
        /// </summary>
        /// <param name="stencilCode">ģ�����</param>
        /// <param name="stencilName">ģ������</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int UpdateDrugStencilName(string stencilCode, string stencilName)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Constant.UpdateDrugOpenStencilName", ref strSQL) == -1) return -1;
            try
            {
                strSQL = string.Format(strSQL, stencilCode, stencilName);    //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="drugStencil">ҩƷģ����Ϣ</param>
        /// <returns></returns>
        public int SetDrugStencil(FS.HISFC.Models.Pharmacy.DrugStencil drugStencil)
        {
            int parm = 0;
            parm = this.UpdateDrugStencil(drugStencil);
            if (parm == -1)
                return -1;
            if (parm == 0)
            {
                parm = this.InsertDrugStencil(drugStencil);
            }
            return parm;
        }
        #endregion

        #region �½����ݴ���

        /// <summary>
        /// �½��¼ɾ��
        /// </summary>
        /// <param name="dtBegin">�½��¼��ʼʱ��</param>
        /// <param name="dtEnd">�½��¼��ֹʱ��</param>
        /// <returns>�ɹ�ɾ������1 ʧ�ܷ��أ�1 �޲�����¼����0</returns>
        public int DelMonthStore(DateTime dtBegin,DateTime dtEnd)
        {
            string strHeadSql = "";         //ɾ���½������Ϣ
            //ȡSQL���
            if (this.GetSQL("Pharmacy.MonthStore.DelMonthStore.Head", ref strHeadSql) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.MonthStore.DelMonthStore.Head�ֶ�!";
                return -1;
            }

            strHeadSql = string.Format(strHeadSql, dtBegin, dtEnd);

            int parm = this.ExecNoQuery(strHeadSql);
            if (parm == -1)
            {
                this.Err = "ִ��Sql��䷢������ " + this.Err;
                return -1;
            }

            string strDetailSql = "";       //ɾ���½���ϸ��Ϣ
            //ȡSQL���
            if (this.GetSQL("Pharmacy.MonthStore.DelMonthStore.Detail", ref strDetailSql) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.MonthStore.DelMonthStore.Detail�ֶ�!";
                return -1;
            }

            strDetailSql = string.Format(strDetailSql, dtBegin, dtEnd);

            parm = this.ExecNoQuery(strDetailSql);

            return parm;
        }

        /// <summary>
        /// �½������Ϣ��ȡ
        /// </summary>
        /// <param name="drugDeptCode">�ⷿ����</param>
        /// <param name="dsHead">�½����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1 �����ݷ���0</returns>
        public int QueryMonthStoreHead(string drugDeptCode,ref System.Data.DataSet dsHead)
        {
            string strSql = "";
            //ȡSQL���
            if (this.GetSQL("Pharmacy.MonthStore.StoreHead", ref strSql) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.MonthStore.StoreHead�ֶΣ�";
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, drugDeptCode);
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����Pharmacy.MonthStore.StoreHead" + ex.Message;
                this.WriteErr();
                return -1;
            }

            //����SQL���ȡ��ѯ���
            if (this.ExecQuery(strSql, ref dsHead) == -1)
            {
                return -1;
            }
            if (dsHead == null)
            {
                return 1;
            }

            if (dsHead.Tables.Count == 0)
            {
                return 0;
            }

            return 1;
        }

        /// <summary>
        /// �½���ϸ��Ϣ��ȡ
        /// </summary>
        /// <param name="drugDeptCode">�ⷿ����</param>
        /// <param name="dsDetail">�½���ϸ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1 �����ݷ���0</returns>
        public int QueryMonthStoreDetail(string drugDeptCode, DateTime dtBegin,DateTime dtEnd,ref System.Data.DataSet dsDetail)
        {
            string strSql = "";
            //ȡSQL���
            if (this.GetSQL("Pharmacy.MonthStore.StoreDetail", ref strSql) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.MonthStore.StoreDetail�ֶΣ�";
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, drugDeptCode,dtBegin.ToString(),dtEnd.ToString());
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����Pharmacy.MonthStore.StoreDetail" + ex.Message;
                this.WriteErr();
                return -1;
            }

            //����SQL���ȡ��ѯ���
            if (this.ExecQuery(strSql, ref dsDetail) == -1)
            {
                return -1;
            }
            if (dsDetail == null)
            {
                return 1;
            }

            if (dsDetail.Tables.Count == 0)
            {
                return 0;
            }

            return 1;
        }

        #endregion

        #region ����ҩƷά��

        /// <summary>
        /// ����ʵ����Ϣ��ȡInsert��Update����������
        /// </summary>
        /// <param name="drugSpe">����ҩƷ��Ϣ</param>
        /// <returns>�ɹ����ز������� ʧ�ܷ���null</returns>
        private string[] GetDrugSpecialParam(FS.HISFC.Models.Pharmacy.DrugSpecial drugSpe)
        {
            string[] strParam = new string[] { 
                                                ((int)drugSpe.SpeType).ToString(),
                                                drugSpe.SpeItem.ID,
                                                drugSpe.SpeItem.Name,
                                                drugSpe.Item.ID,
                                                drugSpe.Item.Name,
                                                drugSpe.Item.Specs,
                                                drugSpe.Extend,
                                                drugSpe.Memo,
                                                drugSpe.Oper.ID,
                                                drugSpe.Oper.OperTime.ToString()                                                            
                                             };

            return strParam;
        }

        /// <summary>
        /// ִ��Sql ��ȡDrugSpecialʵ����Ϣ
        /// </summary>
        /// <param name="strSql">��ִ�е�Sql</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        private List<FS.HISFC.Models.Pharmacy.DrugSpecial> ExecSqlForDrugSpecial(string strSql)
        {
            List<FS.HISFC.Models.Pharmacy.DrugSpecial> al = new List<DrugSpecial>();
            DrugStencil drugStencil = new DrugStencil();

            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "ִ��" + strSql + "��������" + this.Err;
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    DrugSpecial drugSpe = new DrugSpecial();

                    drugSpe.SpeType = (EnumDrugSpecialType)NConvert.ToInt32(this.Reader[0]);
                    drugSpe.SpeItem.ID = this.Reader[1].ToString();
                    drugSpe.SpeItem.Name = this.Reader[2].ToString();
                    drugSpe.Item.ID = this.Reader[3].ToString();
                    drugSpe.Item.Name = this.Reader[4].ToString();
                    drugSpe.Item.Specs = this.Reader[5].ToString();
                    drugSpe.Extend = this.Reader[6].ToString();
                    drugSpe.Memo = this.Reader[7].ToString();
                    drugSpe.Oper.ID = this.Reader[8].ToString();
                    drugSpe.Oper.OperTime = NConvert.ToDateTime(this.Reader[9]);

                    al.Add(drugSpe);
                }
            }
            catch
            {
                this.Err = "��Reader�ڶ�ȡ���ݷ����쳣";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// ��������ҩƷ��Ϣ
        /// </summary>
        /// <param name="drugSpe">����ҩƷ��Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int InsertDrugSpecial(FS.HISFC.Models.Pharmacy.DrugSpecial drugSpe)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Constant.InsertDrugSpecial", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = this.GetDrugSpecialParam(drugSpe);   //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);                //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ����ҩƷ��Ϣɾ��
        /// </summary>
        /// <param name="drugSpe">����ҩƷ��Ϣ</param>
        /// <returns>�ɹ�����ɾ����¼�� ʧ�ܷ���-1</returns>
        public int DelDrugSpecial(FS.HISFC.Models.Pharmacy.DrugSpecial drugSpe)
        {
            string strSQL = "";  
            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.DelDrugSpecial.Detail", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.DelDrugSpecial.Detail�ֶ�!";
                return -1;
            }

            strSQL = string.Format(strSQL,((int)drugSpe.SpeType).ToString(),drugSpe.SpeItem.ID,drugSpe.Item.ID);

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ��������ɾ��ҩƷ������Ϣ
        /// </summary>
        /// <param name="speType">��������</param>
        /// <returns>�ɹ�����Ӱ���¼�� ʧ�ܷ���-1</returns>
        public int DelDrugSpecial(FS.HISFC.Models.Pharmacy.EnumDrugSpecialType speType)
        {
            string strSQL = "";  
            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.DelDrugSpecial.Type", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.DelDrugSpecial.Type�ֶ�!";
                return -1;
            }

            strSQL = string.Format(strSQL, ((int)speType).ToString());

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ��ȡ��������ҩƷ�б�
        /// </summary>
        /// <returns>�ɹ�������������ҩƷ ʧ�ܷ���null</returns>
        public List<FS.HISFC.Models.Pharmacy.DrugSpecial> QueryDrugSpecialList()
        {
            string strSQL = "";  

            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.QueryDrugSpecialList", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.QueryDrugSpecialList�ֶ�!";
                return null;
            }

            return this.ExecSqlForDrugSpecial(strSQL);
        }

        /// <summary>
        /// �������ͻ�ȡ��������ҩƷ
        /// </summary>
        /// <param name="speType">����ҩƷ����</param>
        /// <returns>�ɹ�������������ҩƷ ʧ�ܷ���null</returns>
        public List<FS.HISFC.Models.Pharmacy.DrugSpecial> QueryDrugSpecialList(EnumDrugSpecialType speType)
        {
            string strSQL = "";

            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.QueryDrugSpecialList", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.QueryDrugSpecialList�ֶ�!";
                return null;
            }

            //ȡwhere��� 
            string strWhere = "";
            if (this.GetSQL("Pharmacy.Constant.QueryDrugSpecialList.Type", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.QueryDrugSpecialList.Type�ֶ�!";
                return null;
            }

            strSQL = string.Format(strSQL + strWhere, ((int)speType).ToString());

            return this.ExecSqlForDrugSpecial(strSQL);
        }

        #endregion

        #region ��������ҩά��

        /// <summary>
        /// ��ȡInsert��Update����
        /// </summary>
        /// <param name="deptRadix">��������ҩ��Ϣ</param>
        /// <returns>�ɹ����ز������� ʧ�ܷ���null</returns>
        private string[] GetParamForDeptRadix(FS.HISFC.Models.Pharmacy.Common.DeptRadix deptRadix)
        {
            string[] strParam = new string[]{
                                                deptRadix.StockDept.ID,
                                                deptRadix.Dept.ID,
                                                deptRadix.Item.ID,
                                                deptRadix.Item.Name,
                                                deptRadix.Item.Specs,
                                                deptRadix.Item.PackUnit,
                                                deptRadix.Item.PackQty.ToString(),
                                                deptRadix.Item.MinUnit,
                                                deptRadix.RadixQty.ToString(),          //ҩƷ����
                                                deptRadix.SurplusQty.ToString(),        //ӯ������
                                                deptRadix.ExpendQty.ToString(),         //������
                                                deptRadix.SupplyQty.ToString(),         //������
                                                deptRadix.Memo,
                                                deptRadix.BeginDate.ToString(),
                                                deptRadix.EndDate.ToString(),
                                                deptRadix.Oper.ID,
                                                deptRadix.Oper.OperTime.ToString(),
                                                deptRadix.DeptType
                                            };

            return strParam;
        }

        /// <summary>
        /// ִ��Sql��ȡ��Ŀʵ��
        /// </summary>
        /// <param name="strSql">��ִ�е�Sql</param>
        /// <returns>�ɹ����ز������� ʧ�ܷ���null</returns>
        private List<FS.HISFC.Models.Pharmacy.Common.DeptRadix> ExecSqlForDeptRadix(string strSql)
        {
            List<FS.HISFC.Models.Pharmacy.Common.DeptRadix> al = new List<FS.HISFC.Models.Pharmacy.Common.DeptRadix>();
            FS.HISFC.Models.Pharmacy.Common.DeptRadix drugStencil = new FS.HISFC.Models.Pharmacy.Common.DeptRadix();

            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "ִ��" + strSql + "��������" + this.Err;
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Pharmacy.Common.DeptRadix deptRadix = new FS.HISFC.Models.Pharmacy.Common.DeptRadix();

                    deptRadix.StockDept.ID = this.Reader[0].ToString();
                    deptRadix.Dept.ID = this.Reader[1].ToString();
                    deptRadix.Item.ID = this.Reader[2].ToString();
                    deptRadix.Item.Name = this.Reader[3].ToString();
                    deptRadix.Item.Specs = this.Reader[4].ToString();
                    deptRadix.Item.PackUnit = this.Reader[5].ToString();
                    deptRadix.Item.PackQty = NConvert.ToDecimal(this.Reader[6].ToString());
                    deptRadix.Item.MinUnit = this.Reader[7].ToString();
                    deptRadix.RadixQty = NConvert.ToDecimal(this.Reader[8].ToString());
                    deptRadix.SurplusQty = NConvert.ToDecimal(this.Reader[9]);
                    deptRadix.ExpendQty = NConvert.ToDecimal(this.Reader[10]);
                    deptRadix.SupplyQty = NConvert.ToDecimal(this.Reader[11]);
                    deptRadix.Memo = this.Reader[12].ToString();
                    deptRadix.BeginDate = NConvert.ToDateTime(this.Reader[13]);
                    deptRadix.EndDate = NConvert.ToDateTime(this.Reader[14]);
                    deptRadix.Oper.ID = this.Reader[15].ToString();
                    deptRadix.Oper.OperTime = NConvert.ToDateTime(this.Reader[16]);
                    deptRadix.DeptType = this.Reader[17].ToString();

                    al.Add(deptRadix);
                }
            }
            catch
            {
                this.Err = "��Reader�ڶ�ȡ���ݷ����쳣";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// ���벡��ҩƷ������Ϣ
        /// </summary>
        /// <param name="deptRadix">����ҩƷ������Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int InsertDeptRadix(FS.HISFC.Models.Pharmacy.Common.DeptRadix deptRadix)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Constant.InsertDeptRadix", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = this.GetParamForDeptRadix(deptRadix);   //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);                //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ����ҩƷ������Ϣɾ��
        /// </summary>
        /// <param name="drugDeptCode">������</param>
        /// <param name="deptCode">����</param>
        /// <param name="drugCode">ҩƷ����</param>
        /// <returns>�ɹ�����ɾ����¼�� ʧ�ܷ���-1</returns>
        public int DelDeptRadix(string drugDeptCode,string deptCode,string drugCode,DateTime beginDate)
        {
            string strSQL = "";
            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.DelDeptRadix.Detail", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.DelDeptRadix.Detail�ֶ�!";
                return -1;
            }

            strSQL = string.Format(strSQL, drugDeptCode,deptCode, drugCode,beginDate.ToString());

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ���տ���ɾ������ҩƷ������Ϣ
        /// </summary>
        /// <param name="drugDeptCode">������</param>
        /// <param name="deptCode">����</param>
        /// <returns>�ɹ�����Ӱ���¼�� ʧ�ܷ���-1</returns>
        public int DelDeptRadix(string drugDeptCode,string deptCode,DateTime beginDate)
        {
            string strSQL = "";
            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.DelDeptRadix.Dept", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.DelDeptRadix.Dept�ֶ�!";
                return -1;
            }

            strSQL = string.Format(strSQL, drugDeptCode,deptCode,beginDate.ToString());

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ���ݿ��ұ����ȡ��������ҩ��Ϣ
        /// </summary>
        /// <param name="drugDeptCode">������</param>
        /// <param name="deptCode">��������</param>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <returns>�ɹ����ز�������ҩ��Ϣ ʧ�ܷ���null</returns>
        public List<FS.HISFC.Models.Pharmacy.Common.DeptRadix> QueryDeptRadix(string drugDeptCode,string deptCode,DateTime beginDate)
        {
            string strSQL = "";

            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.QueryDeptRadix", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.QueryDeptRadix�ֶ�!";
                return null;
            }

            string strWhere = "";
            if (this.GetSQL("Pharmacy.Constant.QueryDeptRadix.Dept", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.QueryDeptRadix.Dept�ֶ�!";
                return null;
            }

            strSQL = strSQL + strWhere;
            strSQL = string.Format(strSQL,drugDeptCode,deptCode,beginDate.ToString());

            return this.ExecSqlForDeptRadix(strSQL);
        }

        /// <summary>
        /// ��ȡ�����˻���ҩƷ�Ĳ�����Ϣ
        /// </summary>
        /// <param name="drugDeptCode">������</param>
        /// <returns>�ɹ����ز������� ʧ�ܷ���null</returns>
        public List<FS.FrameWork.Models.NeuObject> QueryDeptRadixDeptList(string drugDeptCode,DateTime beginTime)
        {
            string strSQL = "";

            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.QueryDeptRadixList", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.QueryDeptRadixList�ֶ�!";
                return null;
            }

            strSQL = string.Format(strSQL, drugDeptCode,beginTime.ToString());

            List<FS.FrameWork.Models.NeuObject> al = new List<FS.FrameWork.Models.NeuObject>();
            FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();

            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "ִ��" + strSQL + "��������" + this.Err;
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    info = new FS.FrameWork.Models.NeuObject();

                    info.ID = this.Reader[0].ToString();
                    info.Name = this.Reader[1].ToString();

                    al.Add(info);
                }
            }
            catch
            {
                this.Err = "��Reader�ڶ�ȡ���ݷ����쳣";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// ��ȡ����ҩʱ����б���Ϣ
        /// </summary>
        /// <param name="drugDeptCode"></param>
        /// <returns>ID ʱ��� Name ��ʼʱ��</returns>
        public List<FS.FrameWork.Models.NeuObject> QueryDeptRadixDateList(string drugDeptCode)
        {
            string strSQL = "";

            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.QueryDeptRadixDateList", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.QueryDeptRadixDateList�ֶ�!";
                return null;
            }

            strSQL = string.Format(strSQL, drugDeptCode);

            List<FS.FrameWork.Models.NeuObject> al = new List<FS.FrameWork.Models.NeuObject>();
            FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();

            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "ִ��" + strSQL + "��������" + this.Err;
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    info = new FS.FrameWork.Models.NeuObject();

                    info.ID = this.Reader[0].ToString();
                    info.Name = this.Reader[1].ToString();

                    al.Add(info);
                }
            }
            catch
            {
                this.Err = "��Reader�ڶ�ȡ���ݷ����쳣";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }
        #endregion

        #region ҩƷ�Զ����½�����

        /// <summary>
        /// ����ʵ����Ϣ��ȡInsert��Update����������
        /// </summary>
        /// <param name="msCustom">ҩƷ�Զ����½���Ŀά����Ϣ</param>
        /// <returns>�ɹ����ز������� ʧ�ܷ���null</returns>
        private string[] GetMSCustomParam(FS.HISFC.Models.Pharmacy.MSCustom msCustom)
        {
            string[] strParam = new string[] {                                                
                                                msCustom.ID,
                                                msCustom.DeptType.ToString(),
                                                msCustom.CustomItem.ID,
                                                msCustom.CustomItem.Name,
                                                msCustom.ItemOrder.ToString(),
                                                FS.HISFC.Models.Base.EnumMSCustomTypeService.GetNameFromEnum(msCustom.CustomType),
                                                msCustom.TypeItem,
                                                ((int)msCustom.Trans).ToString(),
                                                msCustom.PriceType,
                                                msCustom.Oper.ID,
                                                msCustom.Oper.OperTime.ToString()                                                            
                                             };

            return strParam;
        }

        /// <summary>
        /// ִ��Sql ��ȡMSCustomʵ����Ϣ
        /// </summary>
        /// <param name="strSql">��ִ�е�Sql</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        private List<FS.HISFC.Models.Pharmacy.MSCustom> ExecSqlForMSCustom(string strSql)
        {
            List<FS.HISFC.Models.Pharmacy.MSCustom> al = new List<MSCustom>();
            MSCustom msCustom = new MSCustom();

            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "ִ��" + strSql + "��������" + this.Err;
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    msCustom = new MSCustom();

                    msCustom.ID = this.Reader[0].ToString();
                    msCustom.DeptType = (FS.HISFC.Models.Base.EnumDepartmentType)(Enum.Parse(typeof(FS.HISFC.Models.Base.EnumDepartmentType),this.Reader[1].ToString()));
                    msCustom.CustomItem.ID = this.Reader[2].ToString();
                    msCustom.CustomItem.Name = this.Reader[3].ToString();
                    msCustom.ItemOrder = NConvert.ToInt32(this.Reader[4].ToString());
                    msCustom.CustomType = FS.HISFC.Models.Base.EnumMSCustomTypeService.GetEnumFromName(this.Reader[5].ToString());
                    msCustom.TypeItem = this.Reader[6].ToString();
                    msCustom.Trans = (FS.HISFC.Models.Base.TransTypes)(Enum.Parse(typeof(FS.HISFC.Models.Base.TransTypes),this.Reader[7].ToString()));
                    msCustom.PriceType = this.Reader[8].ToString();
                    msCustom.Oper.ID = this.Reader[8].ToString();
                    msCustom.Oper.OperTime = NConvert.ToDateTime(this.Reader[9]);

                    al.Add(msCustom);
                }
            }
            catch
            {
                this.Err = "��Reader�ڶ�ȡ���ݷ����쳣";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// ����ҩƷ�Զ����½�������Ϣ
        /// </summary>
        /// <param name="msCustom">ҩƷ�Զ����½���Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int InsertMSCustom(FS.HISFC.Models.Pharmacy.MSCustom msCustom)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Constant.InsertMSCustom", ref strSQL) == -1) return -1;
            try
            {
                if (msCustom.ID == "" || msCustom.ID == null)
                {
                    msCustom.ID = this.GetSequence("Pharmacy.Constant.GetNewCompanyID");
                }
                string[] strParm = this.GetMSCustomParam(msCustom);   //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);                //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ҩƷ�Զ����½�������Ϣɾ��
        /// </summary>
        /// <param name="deptType">��������</param>
        /// <returns>�ɹ�����ɾ����¼�� ʧ�ܷ���-1</returns>
        public int DelMSCustom(FS.HISFC.Models.Base.EnumDepartmentType deptType)
        {
            string strSQL = "";
            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.DelMSCustom.Type", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.DelMSCustom.Type�ֶ�!";
                return -1;
            }

            strSQL = string.Format(strSQL, deptType.ToString());

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ҩƷ�Զ����½�������Ϣɾ��
        /// </summary>
        /// <param name="ID">������Ϣ��ˮ��</param>
        /// <returns>�ɹ�����Ӱ���¼�� ʧ�ܷ���-1</returns>
        public int DelMSCustom(string ID)
        {
            string strSQL = "";
            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.DelMSCustom.Detail", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.DelMSCustom.Detail�ֶ�!";
                return -1;
            }

            strSQL = string.Format(strSQL, ID);

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ���ݿ������ͻ�ȡҩƷ�Զ����½�������Ϣ
        /// </summary>
        /// <param name="deptType">��������</param>
        /// <returns>�ɹ����ز�������ҩ��Ϣ ʧ�ܷ���null</returns>
        public List<FS.HISFC.Models.Pharmacy.MSCustom> QueryMSCustom(FS.HISFC.Models.Base.EnumDepartmentType deptType)
        {
            string strSQL = "";

            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.QueryMSCustom", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.QueryMSCustom�ֶ�!";
                return null;
            }

            string strWhere = "";
            if (this.GetSQL("Pharmacy.Constant.QueryMSCustom.Type", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.QueryMSCustom.Type�ֶ�!";
                return null;
            }

            strSQL = strSQL + strWhere;
            strSQL = string.Format(strSQL, deptType.ToString());

            return this.ExecSqlForMSCustom(strSQL);
        }
       
        #endregion

        #region ҽ����������

        /// <summary>
        /// ����ʵ����Ϣ��ȡUpdete��Insert����������
        /// </summary>
        /// <param name="orderGroup">ҽ������������Ϣ</param>
        /// <returns>�ɹ����ز������� ʧ�ܷ���null</returns>
        private string[] GetOrderGroupParam(FS.HISFC.Models.Pharmacy.OrderGroup orderGroup)
        {
            orderGroup.BeginTime = new DateTime(2000, 12, 12, orderGroup.BeginTime.Hour, orderGroup.BeginTime.Minute, orderGroup.BeginTime.Second);
            orderGroup.EndTime = new DateTime(2000, 12, 12, orderGroup.EndTime.Hour, orderGroup.EndTime.Minute, orderGroup.EndTime.Second);

            string[] strParam = new string[] {                                                
                                                orderGroup.ID,
                                                orderGroup.BeginTime.ToString(),
                                                orderGroup.EndTime.ToString(),
                                                orderGroup.Oper.ID,
                                                orderGroup.Oper.OperTime.ToString()
                                             };

            return strParam;
        }

        /// <summary>
        /// ִ��Sql ��ȡOrderGroup��Ϣ
        /// </summary>
        /// <param name="strSql">��ִ�е�Sql</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        private List<FS.HISFC.Models.Pharmacy.OrderGroup> ExecSqlForOrderGroup(string strSql)
        {
            List<FS.HISFC.Models.Pharmacy.OrderGroup> al = new List<OrderGroup>();
            OrderGroup orderGroup = new OrderGroup();

            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "ִ��" + strSql + "��������" + this.Err;
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    orderGroup = new OrderGroup();

                    orderGroup.ID = this.Reader[0].ToString();
                    orderGroup.BeginTime = NConvert.ToDateTime(this.Reader[1].ToString());
                    orderGroup.EndTime = NConvert.ToDateTime(this.Reader[2].ToString());       
                    orderGroup.Oper.ID = this.Reader[3].ToString();
                    orderGroup.Oper.OperTime = NConvert.ToDateTime(this.Reader[4]);

                    al.Add(orderGroup);
                }
            }
            catch
            {
                this.Err = "��Reader�ڶ�ȡ���ݷ����쳣";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// ����ҽ������������Ϣ
        /// </summary>
        /// <param name="orderGroup">ҽ������������Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int InsertOrderGroup(FS.HISFC.Models.Pharmacy.OrderGroup orderGroup)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Constant.InsertOrderGroup", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = this.GetOrderGroupParam(orderGroup);   //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);                //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ɾ������ҽ������������Ϣ
        /// </summary>
        /// <returns></returns>
        public int DelOrderGroup()
        {
            string strSQL = "";
            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.DelOrderGroup", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.DelOrderGroup�ֶ�!";
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ɾ������ҽ������������Ϣ
        /// </summary>
        /// <returns></returns>
        public int DelOrderGroup(string groupCode,DateTime dtBegin,DateTime dtEnd)
        {
            string strSQL = "";
            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.DelOrderGroup.GroupCode", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.DelOrderGroup.GroupCode�ֶ�!";
                return -1;
            }

            dtBegin = new DateTime(2000, 12, 12, dtBegin.Hour, dtBegin.Minute, dtBegin.Second);
            dtEnd = new DateTime(2000, 12, 12, dtEnd.Hour, dtEnd.Minute, dtEnd.Second);

            strSQL = string.Format(strSQL, groupCode,dtBegin.ToString(),dtEnd.ToString());

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ��ȡ����ҽ������������Ϣ
        /// </summary>
        /// <returns></returns>
        public List<OrderGroup> QueryOrderGroup()
        {
            string strSQL = "";

            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.QueryOrderGroup", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.QueryOrderGroup�ֶ�!";
                return null;
            }

            return this.ExecSqlForOrderGroup(strSQL);
        }

        /// <summary>
        /// ����ҽ��ִ��ʱ���ȡҽ������
        /// </summary>
        /// <param name="execTime">ҽ��ִ��ʱ��</param>
        /// <returns>�ɹ�����ҽ������ ʧ�ܷ���null</returns>
        public string GetOrderGroup(DateTime execTime)
        {
            string strSQL = "";

            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.GetOrderGroup", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetOrderGroup�ֶ�!";
                return null;
            }

            execTime = new DateTime(2000, 12, 12, execTime.Hour, execTime.Minute, execTime.Second);

            strSQL = string.Format(strSQL, execTime.ToString());

            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "ִ��" + strSQL + "��������" + this.Err;
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    return this.Reader[0].ToString();
                }
            }
            catch
            {
                this.Err = "��Reader�ڶ�ȡ���ݷ����쳣";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return null;
        }

        #endregion

        #region ����ȡҩҩ������(ҩƷָ���������ҩ��)

        /// <summary>
        /// ��ȡ���롢���²���
        /// </summary>
        /// <param name="speLocation"></param>
        /// <returns></returns>
        private string[] GetDrugSpeLocationParams(FS.HISFC.Models.Pharmacy.DrugSpeLocation speLocation)
        {
            string[] strParams = new string[] { 
                                                    speLocation.ID,
                                                    speLocation.Item.ID,
                                                    speLocation.Item.Name,
                                                    speLocation.Item.Specs,
                                                    speLocation.RoomDept.ID,
                                                    speLocation.Oper.ID,
                                                    speLocation.Oper.OperTime.ToString()
                                                };

            return strParams;
        }

        /// <summary>
        /// ִ��Sql����ȡ����
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        private List<FS.HISFC.Models.Pharmacy.DrugSpeLocation> ExecSqlForSpeLocation(string strSql)
        {
            List<FS.HISFC.Models.Pharmacy.DrugSpeLocation> al = new List<DrugSpeLocation>();

            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "ִ��" + strSql + "��������" + this.Err;
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    DrugSpeLocation speLocation = new DrugSpeLocation();

                    speLocation.ID = this.Reader[0].ToString();
                    speLocation.Item.ID = this.Reader[1].ToString();
                    speLocation.Item.Name = this.Reader[2].ToString();
                    speLocation.Item.Specs = this.Reader[3].ToString();
                    speLocation.RoomDept.ID = this.Reader[4].ToString();
                    speLocation.Oper.ID = this.Reader[5].ToString();
                    speLocation.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6].ToString());

                    al.Add(speLocation);
                }
            }
            catch
            {
                this.Err = "��Reader�ڶ�ȡ���ݷ����쳣";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// ������ҩƷ��Ϣ
        /// </summary>
        /// <param name="speLocation">��ҩƷ��Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int InsertDrugSpeLocation(FS.HISFC.Models.Pharmacy.DrugSpeLocation speLocation)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Constant.InsertDrugSpeLocation", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = this.GetDrugSpeLocationParams(speLocation);   //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);                //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ��ҩƷ��Ϣɾ��
        /// </summary>
        /// <param name="speLocation">��ҩƷ��Ϣ</param>
        /// <returns>�ɹ�����ɾ����¼�� ʧ�ܷ���-1</returns>
        public int DelDrugSpeLocation(FS.HISFC.Models.Pharmacy.DrugSpeLocation speLocation)
        {
            string strSQL = "";
            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.DelDrugSpeLocation", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.DelDrugSpeLocation�ֶ�!";
                return -1;
            }

            strSQL = string.Format(strSQL, speLocation.ID,speLocation.Item.ID,speLocation.RoomDept.ID);

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <returns></returns>
        public List<FS.HISFC.Models.Pharmacy.DrugSpeLocation> QueryDrugSpeLocation()
        {
            string strSQL = "";

            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.QueryDrugSpeLocation", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.QueryDrugSpeLocation�ֶ�!";
                return null;
            }

            return this.ExecSqlForSpeLocation(strSQL);
        }
        #endregion

        #region ҩƷ����

        /// <summary>
        /// ����ʵ����Ϣ��ȡUpdete��Insert����������
        /// </summary>
        /// <param name="drugConstant">ҩƷ����������Ϣ</param>
        /// <returns>�ɹ����ز������� ʧ�ܷ���null</returns>
        private string[] GetDrugConstantParam(FS.HISFC.Models.Pharmacy.DrugConstant drugConstant)
        {
            string[] strParam = new string[] {                                                
                                                drugConstant.ConsType,
                                                drugConstant.Dept.ID,
                                                drugConstant.DrugType,
                                                drugConstant.Class2Priv.ID,
                                                drugConstant.Class3Priv.ID,
                                                drugConstant.Item.ID,
                                                drugConstant.Item.Name,
                                                NConvert.ToInt32(drugConstant.IsValid).ToString(),
                                                drugConstant.Memo,
                                                drugConstant.Oper.ID,
                                                drugConstant.Oper.OperTime.ToString()
                                             };

            return strParam;
        }

        /// <summary>
        /// ִ��Sql ��ȡOrderGroup��Ϣ
        /// </summary>
        /// <param name="strSql">��ִ�е�Sql</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        private List<FS.HISFC.Models.Pharmacy.DrugConstant> ExecSqlForDrugConstant(string strSql)
        {
            List<FS.HISFC.Models.Pharmacy.DrugConstant> al = new List<DrugConstant>();
            DrugConstant drugConstant = new DrugConstant();

            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "ִ��" + strSql + "��������" + this.Err;
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    drugConstant = new DrugConstant();

                    drugConstant.ConsType = this.Reader[0].ToString();
                    drugConstant.Dept.ID = this.Reader[1].ToString();
                    drugConstant.DrugType = this.Reader[2].ToString();
                    drugConstant.Class2Priv.ID = this.Reader[3].ToString();
                    drugConstant.Class3Priv.ID = this.Reader[4].ToString();
                    drugConstant.Item.ID = this.Reader[5].ToString();
                    drugConstant.Item.Name = this.Reader[6].ToString();
                    drugConstant.IsValid = NConvert.ToBoolean(this.Reader[7]);
                    drugConstant.Memo = this.Reader[8].ToString();
                    drugConstant.Oper.ID = this.Reader[9].ToString();
                    drugConstant.Oper.OperTime = NConvert.ToDateTime(this.Reader[10]);

                    al.Add(drugConstant);
                }
            }
            catch
            {
                this.Err = "��Reader�ڶ�ȡ���ݷ����쳣";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// ����ҩƷ����������Ϣ
        /// </summary>
        /// <param name="drugConstant">ҩƷ����������Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int InsertDrugConstant(FS.HISFC.Models.Pharmacy.DrugConstant drugConstant)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Constant.InsertDrugConstant", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = this.GetDrugConstantParam(drugConstant);   //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);                //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ɾ��ҩƷ����������Ϣ
        /// </summary>
        /// <param name="drugConstant">ҩƷ����������Ϣ</param>
        /// <returns>�ɹ�����ɾ������ ʧ�ܷ���-1</returns>
        public int DeleteDrugConstant(FS.HISFC.Models.Pharmacy.DrugConstant drugConstant)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Constant.DeleteDrugConstant", ref strSQL) == -1) return -1;
            try
            {
                strSQL = string.Format(strSQL, drugConstant.ConsType, drugConstant.Dept.ID, drugConstant.DrugType, drugConstant.Item.ID);                //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ɾ��ҩƷ������Ϣ
        /// </summary>
        /// <param name="consType">ҩƷ����������Ϣ</param>
        /// <returns>�ɹ�����ɾ������ ʧ�ܷ���-1</returns>
        public int DeleteDrugConstant(string consType)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Constant.DeleteDrugConstant.Type", ref strSQL) == -1) return -1;
            try
            {
                strSQL = string.Format(strSQL, consType);                //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ���ݳ�������ȡ������Ϣ
        /// </summary>
        /// <param name="consType">�������</param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Pharmacy.DrugConstant> QueryDrugConstant(string consType)
        {
            string strSQL = "";

            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.QueryDrugConstant", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.QueryDrugConstant�ֶ�!";
                return null;
            }

            string strWhere = "";
            if (this.GetSQL("Pharmacy.Constant.QueryDrugConstant.Type", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.QueryDrugConstant.Type�ֶ�!";
                return null;
            }

            strSQL = strSQL + strWhere;
            strSQL = string.Format(strSQL, consType);

            return this.ExecSqlForDrugConstant(strSQL);
        }

        /// <summary>
        /// ���ݳ������/��Ŀ�����ȡ������Ϣ
        /// </summary>
        /// <param name="consType">�������</param>
        /// <param name="itemCode">��Ŀ�������</param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Pharmacy.DrugConstant> QueryDrugConstant(string consType, string itemCode)
        {
            string strSQL = "";

            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.QueryDrugConstant", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.QueryDrugConstant�ֶ�!";
                return null;
            }

            string strWhere = "";
            if (this.GetSQL("Pharmacy.Constant.QueryDrugConstant.Item", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.QueryDrugConstant.Item�ֶ�!";
                return null;
            }

            strSQL = strSQL + strWhere;
            strSQL = string.Format(strSQL, consType, itemCode);

            return this.ExecSqlForDrugConstant(strSQL);
        }
        #endregion

        #region ����

        /// <summary>
        /// ȡҩƷ�����б�
        /// </summary>
        /// <returns>���󷵻�null����ȷ����Quality����</returns>
        [System.Obsolete("ϵͳ�ع����ϣ���QueryConstantQuality����", true)]
        public ArrayList GetConstantQuality()
        {
            return null;
        }
        /// <summary>
        /// ��ѯ����ҩ������
        /// </summary>
        /// <returns>arrayList</returns>
        [System.Obsolete("ϵͳ�ع����ϣ���QueryPhaFunction����", true)]
        public ArrayList GetPhaFunction()
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.GetSQL("Pharmacy.Constant.GetComPhaFunction.all", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetComPhaFunction.all�ֶ�!";
                return null;
            }
            strSQL = string.Format(strSQL);
            //ִ��sql���
            this.ExecQuery(strSQL);
            ArrayList alist = new ArrayList();
            try
            {
                FS.HISFC.Models.Pharmacy.PhaFunction myFunction;
                while (this.Reader.Read())
                {

                    myFunction = new PhaFunction();
                    myFunction.ParentNode = this.Reader[0].ToString();							//0 ���ڵ�
                    myFunction.ID = this.Reader[1].ToString();								//1�ڵ�ID
                    myFunction.Name = this.Reader[2].ToString();								//2 �ڵ�����
                    myFunction.NodeKind = NConvert.ToInt32(this.Reader[3].ToString());            //3�ڵ�����
                    myFunction.GradeLevel = NConvert.ToInt32(this.Reader[4].ToString());        //4 ����
                    myFunction.SpellCode = this.Reader[5].ToString();							//5 ƴ����
                    myFunction.WBCode = this.Reader[6].ToString();							//6 �����
                    myFunction.SortID = NConvert.ToInt32(this.Reader[7].ToString());           //7 �Զ�����
                    myFunction.IsValid = NConvert.ToBoolean(this.Reader[8].ToString());							//8 ����
                    myFunction.Memo = this.Reader[11].ToString();								//11��ע      
                    this.ProgressBarValue++;
                    alist.Add(myFunction);
                }
            }
            catch (Exception ex)
            {
                this.Err = "ȡҩƷ����ҩ������ʧ��" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            //��������
            return alist;
        }

        [System.Obsolete("ϵͳ�ع����ϣ���QueryPhaFunctionLeafage����", true)]
        public ArrayList GetPhaFunctionLeafage()
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.GetSQL("Pharmacy.Constant.GetComPhaFunction.all", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetComPhaFunction.all�ֶ�!";
                return null;
            }
            string strSQL1 = "";
            if (this.GetSQL("Pharmacy.Constant.GetComPhaFunction.Where.1", ref strSQL1) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetComPhaFunction.Where.1�ֶ�!";
                return null;
            }

            strSQL = strSQL + " " + strSQL1;

            strSQL = string.Format(strSQL);
            //ִ��sql���
            this.ExecQuery(strSQL);
            ArrayList alist = new ArrayList();
            try
            {
                FS.HISFC.Models.Pharmacy.PhaFunction myFunction;
                while (this.Reader.Read())
                {

                    myFunction = new PhaFunction();
                    myFunction.ParentNode = this.Reader[0].ToString();							//0 ���ڵ�
                    myFunction.ID = this.Reader[1].ToString();								//1�ڵ�ID
                    myFunction.Name = this.Reader[2].ToString();								//2 �ڵ�����
                    myFunction.NodeKind = NConvert.ToInt32(this.Reader[3].ToString());            //3�ڵ�����
                    myFunction.GradeLevel = NConvert.ToInt32(this.Reader[4].ToString());        //4 ����
                    myFunction.SpellCode = this.Reader[5].ToString();							//5 ƴ����
                    myFunction.WBCode = this.Reader[6].ToString();							//6 �����
                    myFunction.SortID = NConvert.ToInt32(this.Reader[7].ToString());           //7 �Զ�����
                    myFunction.IsValid = NConvert.ToBoolean(this.Reader[8].ToString());							//8 ����
                    myFunction.Memo = this.Reader[11].ToString();								//11��ע      
                    this.ProgressBarValue++;
                    alist.Add(myFunction);
                }
            }
            catch (Exception ex)
            {
                this.Err = "ȡҩƷ����ҩ������ʧ��" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            //��������
            return alist;
        }

        /// <summary>
        /// ȡ����ҩ�����ó�������һ����¼
        /// </summary>
        /// ����arraylist ���� nodecode �Ǳ��нڵ�ı��
        [System.Obsolete("ϵͳ�ع����ϣ���QueryFunctionByNode����", true)]
        public ArrayList GetFunctionByNode(string nodecode)
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.GetSQL("Pharmacy.Constant.GetComPhaFunction.ONE", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetComPhaFunction.ONE�ֶ�!";
                return null;
            }
            strSQL = string.Format(strSQL, nodecode);
            //ִ��sql���
            this.ExecQuery(strSQL);
            ArrayList alist = new ArrayList();
            try
            {
                FS.HISFC.Models.Pharmacy.PhaFunction myFunction;
                while (this.Reader.Read())
                {

                    myFunction = new PhaFunction();
                    myFunction.ParentNode = this.Reader[0].ToString();							//0 ���ڵ�
                    myFunction.ID = this.Reader[1].ToString();								//1�ڵ�ID
                    myFunction.Name = this.Reader[2].ToString();								//2 �ڵ�����
                    myFunction.NodeKind = NConvert.ToInt32(this.Reader[3].ToString());            //3�ڵ�����
                    myFunction.GradeLevel = NConvert.ToInt32(this.Reader[4].ToString());        //4 ����
                    myFunction.SpellCode = this.Reader[5].ToString();							//5 ƴ����
                    myFunction.WBCode = this.Reader[6].ToString();							//6 �����
                    myFunction.SortID = NConvert.ToInt32(this.Reader[7].ToString());           //7 �Զ�����
                    myFunction.IsValid = NConvert.ToBoolean(this.Reader[8].ToString());							//8 ����
                    myFunction.Memo = this.Reader[11].ToString();								//11��ע      
                    this.ProgressBarValue++;
                    alist.Add(myFunction);
                }
            }
            catch (Exception ex)
            {
                this.Err = "ȡҩƷҩ������ʧ��" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            //��������
            return alist;
        }
        /// <summary>
        /// ��ȡ��������Ҷ�ӽڵ�
        /// </summary>
        [System.Obsolete("ϵͳ�ع����ϣ���QueryPhaFunctionNodeName����", true)]
        public ArrayList GetPhaFunctionNodeName()
        {
            string strSQL = "";
            //ȡ���²�����SQL���
            if (this.GetSQL("Pharmacy.Constant.GetComPhaFunction.GETLASTNODENAME", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetComPhaFunction.GETLASTNODENAME�ֶ�!";
                return null;
            }
            strSQL = string.Format(strSQL);
            this.ExecQuery(strSQL);//�滻SQL����еĲ�����
            ArrayList alist = new ArrayList();
            try
            {
                FS.HISFC.Models.Pharmacy.PhaFunction myFunction;
                while (this.Reader.Read())
                {

                    myFunction = new PhaFunction();
                    myFunction.ParentNode = this.Reader[0].ToString();							//0 ���ڵ�
                    myFunction.ID = this.Reader[1].ToString();								//1�ڵ�ID
                    myFunction.Name = this.Reader[2].ToString();								//2 �ڵ���   
                    this.ProgressBarValue++;
                    alist.Add(myFunction);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Pharmacy.Constant.GetComPhaFunction.GETLASTNODENAME:" + ex.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            //��������
            return alist;
        }
        /// <summary>
        /// ȡҩ�����ó�������һ����¼ ͨ�� PARENTID ȡֵ
        /// </summary>
        /// ����arraylist ���� nodecode �Ǳ��нڵ�ı��
        [System.Obsolete("ϵͳ�ع����ϣ���QueryFunctionByParentNode����", true)]
        public ArrayList GetFunctionByParentNode(string Pnodecode)
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.GetSQL("Pharmacy.Constant.GetComPhaFunction.BYPARENTNODE", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetComPhaFunction.BYPARENTNODE �ֶ�!";
                return null;
            }
            strSQL = string.Format(strSQL, Pnodecode);
            //ִ��sql���
            this.ExecQuery(strSQL);
            ArrayList alist = new ArrayList();
            try
            {
                FS.HISFC.Models.Pharmacy.PhaFunction myFunction;
                while (this.Reader.Read())
                {

                    myFunction = new PhaFunction();
                    myFunction.ParentNode = this.Reader[0].ToString();							//0 ���ڵ�
                    myFunction.ID = this.Reader[1].ToString();								//1�ڵ�ID
                    myFunction.Name = this.Reader[2].ToString();								//2 �ڵ�����
                    myFunction.NodeKind = NConvert.ToInt32(this.Reader[3].ToString());            //3�ڵ�����
                    myFunction.GradeLevel = NConvert.ToInt32(this.Reader[4].ToString());        //4 ����
                    myFunction.SpellCode = this.Reader[5].ToString();							//5 ƴ����
                    myFunction.WBCode = this.Reader[6].ToString();							//6 �����
                    myFunction.SortID = NConvert.ToInt32(this.Reader[7].ToString());           //7 �Զ�����
                    myFunction.IsValid = NConvert.ToBoolean(this.Reader[8].ToString());							//8 ����
                    myFunction.Memo = this.Reader[11].ToString();								//11��ע      
                    this.ProgressBarValue++;
                    alist.Add(myFunction);
                }
            }
            catch (Exception ex)
            {
                this.Err = "ȡҩƷҩ������ֵʧ��" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            //��������
            return alist;
        }

        /// <summary>
        /// ҩƷ��˾���������һ��߹�����˾��
        /// </summary>
        /// <param name="type">���ͣ�0�������ң�1������˾</param>
        /// <returns>���󷵻�null</returns>
        [System.Obsolete("ϵͳ�ع����ϣ���QueryCompany����", true)]
        public ArrayList GetCompany(string type)
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.GetSQL("Pharmacy.Constant.GetCompany", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetCompany�ֶ�!";
                return null;
            }

            strSQL = string.Format(strSQL, type);
            //ִ��sql���
            this.ExecQuery(strSQL);
            ArrayList al = new ArrayList();
            try
            {
                FS.HISFC.Models.Pharmacy.Company company;
                while (this.Reader.Read())
                {
                    company = new Company();
                    company.ID = this.Reader[0].ToString();             //0 ��˾����
                    company.Name = this.Reader[1].ToString();             //1 ��˾����
                    company.RelationCollection.Address = this.Reader[2].ToString();         //2 ��ַ
                    company.RelationCollection.Relative = this.Reader[3].ToString();        //3 ��ϵ��ʽ
                    company.GMPInfo = this.Reader[4].ToString();         //4 GMP��Ϣ
                    company.GSPInfo = this.Reader[5].ToString();         //5 GSP��Ϣ
                    company.SpellCode = this.Reader[6].ToString();       //6 ƴ����
                    company.WBCode = this.Reader[7].ToString();          //7 �����
                    company.UserCode = this.Reader[8].ToString();        //8 �Զ�����
                    company.Type = this.Reader[9].ToString();            //9 ����
                    company.OpenBank = this.Reader[10].ToString();       //10 ��������
                    company.OpenAccounts = this.Reader[11].ToString();   //11 �����ʺ�
                    company.ActualRate = NConvert.ToDecimal(this.Reader[12].ToString());//12 �Ӽ���
                    company.Memo = this.Reader[13].ToString();           //13��ע      
                    this.ProgressBarValue++;
                    al.Add(company);
                }
            }
            catch (Exception ex)
            {
                this.Err = "ȡҩƷ��˾ʱ����" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            //��������
            return al;
        }
        /// <summary>
        /// ���ݹ�����˾�����ȡ������˾ʵ��
        /// </summary>
        /// <param name="companyID">������˾����</param>
        /// <returns>�ɹ����ع�����˾ʵ�� ʧ�ܷ���null</returns>
        [System.Obsolete("ϵͳ�ع����ϣ���QueryCompanyByCompanyID����", true)]
        public Company GetCompanyByID(string companyID)
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.GetSQL("Pharmacy.Constant.GetCompanyByID", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetCompanyByID�ֶ�!";
                return null;
            }

            strSQL = string.Format(strSQL, companyID);
            //ִ��sql���
            this.ExecQuery(strSQL);
            try
            {
                FS.HISFC.Models.Pharmacy.Company company = new Company();
                if (this.Reader.Read())
                {
                    company.ID = this.Reader[0].ToString();             //0 ��˾����
                    company.Name = this.Reader[1].ToString();             //1 ��˾����
                    company.RelationCollection.Address = this.Reader[2].ToString();         //2 ��ַ
                    company.RelationCollection.Relative = this.Reader[3].ToString();        //3 ��ϵ��ʽ
                    company.GMPInfo = this.Reader[4].ToString();         //4 GMP��Ϣ
                    company.GSPInfo = this.Reader[5].ToString();         //5 GSP��Ϣ
                    company.SpellCode = this.Reader[6].ToString();       //6 ƴ����
                    company.WBCode = this.Reader[7].ToString();          //7 �����
                    company.UserCode = this.Reader[8].ToString();        //8 �Զ�����
                    company.Type = this.Reader[9].ToString();            //9 ����
                    company.OpenBank = this.Reader[10].ToString();       //10 ��������
                    company.OpenAccounts = this.Reader[11].ToString();   //11 �����ʺ�
                    company.ActualRate = NConvert.ToDecimal(this.Reader[12].ToString());//12 �Ӽ���
                    company.Memo = this.Reader[13].ToString();           //13��ע   
                }

                if (company.Name == "")
                {
                    this.Err = "������˾������ ���룺" + companyID;
                    return null;
                }

                return company;
            }
            catch (Exception ex)
            {
                this.Err = "ȡҩƷ��˾ʱ����" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// ����ҩ��ҩ�����ƻ��ȡҩ��������
        /// </summary>
        /// <returns>neuObject���飬ȡҩ���ұ�ţ�ȡҩ�������ƣ���ע������Ա������ʱ��</returns>
        [System.Obsolete("ϵͳ�ع����ú���QueryReciveDrugDept���", true)]
        public ArrayList GetDrugRoomCode(string ID)
        {
            string strSQL = "";  //ȡĳһȡҩ�������ƻ�ÿ���ȡҩ��ҩ�������б��SQL���
            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.GetDrugRoomCode", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetDrugRoomCode�ֶ�!";
                return null;
            }

            strSQL = string.Format(strSQL, ID);
            //����SQL���ȡ���鲢��������
            ArrayList arrayObject = new ArrayList();

            this.ProgressBarText = "���ڼ���ȡҩ����������Ϣ...";
            this.ProgressBarValue = 0;

            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "ȡҩ���������б�ʱ����" + this.Err;
                return null;
            }
            try
            {
                //	{ȡҩ���ұ��,ȡҩ��������,����Ա���,����Ա����,��������,��ע,rowid}
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                while (this.Reader.Read())
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = this.Reader[0].ToString();					//ȡҩ���ұ��
                    obj.Name = this.Reader[1].ToString();				//ȡҩ��������
                    obj.Memo = this.Reader[2].ToString();				//��ע
                    obj.User01 = this.Reader[3].ToString();				//��ʼʱ��
                    obj.User02 = this.Reader[4].ToString();				//����ʱ��
                    obj.User03 = this.Reader[5].ToString();				//ҩƷ����
                    this.ProgressBarValue++;
                    arrayObject.Add(obj);
                }
            }//�׳�����
            catch (Exception ex)
            {
                this.Err = "ȡҩҩ�������б�ʱ��ִ��SQL������myGetDrugRoomCode" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            this.ProgressBarValue = -1;
            return arrayObject;
        }

        /// <summary>
        /// ���ݲ������롢ҩƷ���ͻ�ȡ��ҩ����
        /// </summary>
        /// <param name="roomCode">ȡҩ����</param>
        /// <param name="drugType">ҩƷ����</param>
        /// <returns>�ɹ�����ȡҩ��������(ID ���� Name ����) ʧ�ܷ���null</returns>
        [System.Obsolete("ϵͳ�ع����ú���QueryReciveDrugDept���", true)]
        public ArrayList GetDeptCode(string roomCode, string drugType)
        {
            string strSQL = "";
            ArrayList arrayObject = new ArrayList();
            //ȡSQL���
            if (this.GetSQL("Pharmacy.Constant.GetDeptCode", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetDeptCode�ֶ�!";
                return null;
            }

            strSQL = string.Format(strSQL, roomCode, drugType);
            //����SQL���ȡ���鲢��������

            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "��ȡ��ҩҩ������" + this.Err;
                return null;
            }
            try
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                while (this.Reader.Read())
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = this.Reader[0].ToString();					//ȡҩ���ұ��
                    obj.Name = this.Reader[1].ToString();				//ȡҩ��������
                    arrayObject.Add(obj);
                }
            }//�׳�����
            catch (Exception ex)
            {
                this.Err = "ȡҩҩ�������б�ʱ��ִ��SQL������myGetDrugRoomCode" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return arrayObject;
        }

        /// <summary>
        /// �����ύɸѡ�����롢���¡�ɾ����
        /// </summary>
        /// <param name="drugRoomList">�����б�</param>
        /// <param name="i">������־��0����1ɾ��2����</param>
        [System.Obsolete("ϵͳ�ع����ú���DrugRoomControl���", true)]
        public void drugRoomControl(ArrayList drugRoomList, int i)
        {
            try
            {
                switch (i)
                {
                    case 0:
                        foreach (FS.FrameWork.Models.NeuObject obj in drugRoomList)
                        {
                            this.InsertDrugRoom(obj);			//��������
                        }
                        break;
                    case 1:
                        foreach (FS.FrameWork.Models.NeuObject obj in drugRoomList)
                        {
                            this.DelSpeDrugRoom(obj.User03);	//ɾ������
                        }
                        break;
                    case 2:
                        foreach (FS.FrameWork.Models.NeuObject obj in drugRoomList)
                        {
                            this.UpdateDrugRoom(obj);			//��������
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                this.Err = "���ݱ������" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
            }
        }

        /// <summary>
        /// ���ݿ��ұ���ȡһ�����ҳ�����Ϣ
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <returns>���ҳ���</returns>
        [System.Obsolete("ϵͳ�ع����ú���QueryDeptConstant���", true)]
        public FS.HISFC.Models.Pharmacy.DeptConstant GetDeptConstant(string deptCode)
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.GetSQL("Pharmacy.Constant.GetDeptConstant", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetDeptConstant�ֶ�!";
                return null;
            }

            string strWhere = "";
            //ȡWHERE���
            if (this.GetSQL("Pharmacy.Constant.GetDeptConstant.Where", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetDeptConstant.Where�ֶ�!";
                return null;
            }

            //��ʽ��SQL���
            try
            {
                strSQL += " " + strWhere;
                strSQL = string.Format(strSQL, deptCode);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Pharmacy.Constant.GetDeptConstant.Where:" + ex.Message;
                return null;
            }

            //ȡ���ҳ���
            ArrayList al = this.myGetDeptConstant(strSQL);
            if (al == null)
                return null;

            if (al.Count == 0)
                return new FS.HISFC.Models.Pharmacy.DeptConstant();

            return al[0] as FS.HISFC.Models.Pharmacy.DeptConstant;
        }

        /// <summary>
        /// ȡ���ҳ����б�
        /// </summary>
        /// <returns>���ҳ������飬������null</returns>
        [System.Obsolete("ϵͳ�ع����ú���QueryDeptConstantList���", true)]
        public ArrayList GetDeptConstantList()
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.GetSQL("Pharmacy.Constant.GetDeptConstant", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Constant.GetDeptConstant�ֶ�!";
                return null;
            }

            //ȡ���ҳ�������
            return this.myGetDeptConstant(strSQL);
        }
        #endregion

        #region ҽ��Ȩ��

        /// <summary>
        /// ��ѯҽ��Ȩ�� by Sunjh 2009-6-5 {B0F08F1A-3E14-4e6a-8EFC-F10BC0AD3E35}
        /// </summary>
        /// <param name="levelCode">ְ������</param>
        /// <param name="popedonCode">Ȩ�޴���</param>
        /// <param name="popedomType">Ȩ������ 0ҩƷ���� 1ҩ������</param>
        /// <returns>-1ʧ�� 0��Ȩ�� ����0��Ȩ��</returns>
        public int QueryPopedom(string levelCode, string popedonCode, int popedomType)
        {
            int popedomReturn = -1;
            string sqlStr = "";

            if (this.GetSQL("Medical.Popedom.QueryByType", ref sqlStr) == -1)
            {
                this.Err = "û���ҵ�Medical.Popedom.QueryByType�ֶ�!";
                return -1;
            }

            if (popedomType == 0)
            {
                sqlStr = string.Format(sqlStr, levelCode, "ҩƷ����", popedonCode);
            }
            else if (popedomType == 1)
            {
                sqlStr = string.Format(sqlStr, levelCode, "ҩ������", popedonCode);
            }
            else
            {
                return -1;
            }                        

            if (this.ExecQuery(sqlStr) == -1)
            {
                this.Err = "��ȡȨ����Ϣʧ�ܣ�" + this.Err;
                return -1;
            }
            try
            {
                while (this.Reader.Read())
                {
                    popedomReturn = Convert.ToInt32(this.Reader[0].ToString());
                }
            }
            catch (Exception ex)
            {
                this.Err = "��ȡȨ����Ϣʱ,ִ��SQL������!" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return -1;
            }
            finally
            {
                this.Reader.Close();
            }

            return popedomReturn;
        }

        #endregion

        #region ȫԺ����ҩƷ by Sunjh 2010-10-1 {1A398A34-0718-47ed-AAE9-36336430265E}

        /// <summary>
        /// ��������ҩƷ�Ķ���ά����
        /// </summary>
        /// <param name="speType"></param>
        /// <returns></returns>
        public DataTable GetSpeDruCompareDepOrPerson(FS.HISFC.Models.Pharmacy.EnumDrugSpecialType speType)
        {
            string strSql = string.Empty;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            if (this.GetSQL("Pharmacy.Constant.GetSpeDruCompareDepOrPerson", ref strSql) == -1)
            {
                this.Err = "��ȡ����ΪPharmacy.Constant.GetSpeDruCompareDepOrPerson SQL����";
                return null;
            }
            //            strSql = @"select spe_code,spe_name,drug_code,drug_name,memo,
            //decode(valid,'0','��Ч','1','��Ч') from PHA_COM_SPEDRUG_PER_DEP
            //where spe_type = '{0}'";
            strSql = string.Format(strSql, (int)speType);
            if (ExecQuery(strSql, ref ds) == -1)
            {
                this.Err = "ִ������ΪPharmacy.Constant.GetSpeDruCompareDepOrPerson SQL������";
                return null;
            }
            dt = ds.Tables[0];
            return dt;
        }
        /// <summary>
        /// ���ݴ��������ɾ��fp�е���������ҩƷά����Ϣ
        /// </summary>
        /// <param name="speType"></param>
        public int DelSpeDrugDepOrPerCompare(FS.HISFC.Models.Pharmacy.EnumDrugSpecialType speType)
        {
            string strSql = string.Empty;
            if (this.GetSQL("Pharmacy.Constant.DelSpeDrugDepOrPerCompare", ref strSql) == -1)
            {
                this.Err = "��ȡ����ΪPharmacy.Constant.DelSpeDrugDepOrPerCompare SQL����";
                return -1;
            }
            //            strSql = @"delete PHA_COM_SPEDRUG_PER_DEP
            //where spe_type = '{0}'";
            strSql = string.Format(strSql, (int)speType);
            if (ExecNoQuery(strSql) == -1)
            {
                this.Err = "ִ������ΪPharmacy.Constant.DelSpeDrugDepOrPerCompare SQL������";
                return -1;
            }
            return 0;
        }
        /// <summary>
        /// ��������ҩƷ��ά����Ϣ
        /// </summary>
        /// <param name="deuSpe"></param>
        /// <returns></returns>
        public int InsertSpeDrugDepOrPerCompare(FS.HISFC.Models.Pharmacy.DrugSpecial drugSpe)
        {
            string strSql = string.Empty;
            if (this.GetSQL("Pharmacy.Constant.InsertSpeDrugDepOrPerCompare", ref strSql) == -1)
            {
                this.Err = "��ȡ����ΪPharmacy.Constant.InsertSpeDrugDepOrPerCompare SQL����";
                return -1;
            }
            //            strSql = @"insert into PHA_COM_SPEDRUG_PER_DEP 
            //values('{0}','{1}','{2}','{3}','{4}','{5}',to_date('{6}','yyyy-MM-dd hh24:mi:ss'),'{7}','{8}')";
            strSql = string.Format(strSql, (int)drugSpe.SpeType, drugSpe.ID, drugSpe.Name, drugSpe.Item.ID, drugSpe.Item.Name, drugSpe.Oper.ID, drugSpe.Oper.OperTime, drugSpe.User01, drugSpe.Memo);
            if (ExecNoQuery(strSql) == -1)
            {
                this.Err = "ִ������ΪPharmacy.Constant.InsertSpeDrugDepOrPerCompare SQL������";
                return -1;
            }
            return 0;
        }
        /// <summary>
        /// ���ݴ��������ɾ��һ������ҩƷά����Ϣ
        /// </summary>
        /// <returns></returns>
        public int DelSpeDrugCompare(FS.HISFC.Models.Pharmacy.DrugSpecial drugSpe)
        {
            string strSql = string.Empty;
            if (this.GetSQL("Pharmacy.Constant.DelSpeDrugCompare", ref strSql) == -1)
            {
                this.Err = "��ȡ����ΪPharmacy.Constant.DelSpeDrugCompare SQL����";
                return -1;
            }
            //            strSql = @"delete PHA_COM_SPEDRUG_PER_DEP
            //where spe_type = '{0}' and spe_code = '{1}' and drug_code= '{2}'";
            strSql = string.Format(strSql, drugSpe.User02, drugSpe.ID, drugSpe.Item.ID);
            if (ExecNoQuery(strSql) == -1)
            {
                this.Err = "ִ������ΪPharmacy.Constant.DelSpeDrugCompare SQL������";
                return -1;
            }
            return 0;
        }
        /// <summary>
        /// ��ȡ���е���ά��������ҩƷ�ı��������
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryAllSpeDrugCode()
        {
            string strSql = string.Empty;
            FS.HISFC.Models.Pharmacy.Item item;
            ArrayList al = new ArrayList();

            if (this.GetSQL("Pharmacy.Constant.QueryAllSpeDrugCode", ref strSql) == -1)
            {
                this.Err = "��ȡ����ΪPharmacy.Constant.QueryAllSpeDrugCode SQL����";
                return null;
            }
            //strSql = @"select * from pha_com_spedrug_maintenance";

            if (ExecQuery(strSql) == -1)
            {
                this.Err = "ִ������ΪPharmacy.Constant.QueryAllSpeDrugCode SQL������";
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    item = new FS.HISFC.Models.Pharmacy.Item();
                    item.ID = this.Reader[0].ToString();
                    item.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3]);//�����洢������
                    item.OnceDose = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7]);//�����洢׷����{07AA9FFA-BC72-4443-99F2-85541A03E198}

                    al.Add(item);
                }
                return al;
            }
            catch (Exception ex)
            {
                this.Err = "��ȡ����ΪSQL����" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }
        /// <summary>
        /// ��ȡ���е�����ҩƷ��ά���õĿ��Һ���Ա
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryAllSpeDrugPerAndDep()
        {
            string strSql = string.Empty;
            FS.HISFC.Models.Base.Employee empl;
            ArrayList al = new ArrayList();

            if (this.GetSQL("Pharmacy.Constant.QueryAllSpeDrugPerAndDep", ref strSql) == -1)
            {
                this.Err = "��ȡ����ΪPharmacy.Constant.QueryAllSpeDrugPerAndDep SQL����";
                return null;
            }
            //strSql = @"select * from pha_com_spedrug_per_dep";

            if (ExecQuery(strSql) == -1)
            {
                this.Err = "ִ������ΪPharmacy.Constant.QueryAllSpeDrugPerAndDep SQL������";
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    empl = new FS.HISFC.Models.Base.Employee();
                    empl.ID = this.Reader[1].ToString();
                    empl.User01 = this.Reader[3].ToString();

                    al.Add(empl);
                }
                return al;
            }
            catch (Exception ex)
            {
                this.Err = "��ȡ����ΪSQL����" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }


        #endregion

    }

}
