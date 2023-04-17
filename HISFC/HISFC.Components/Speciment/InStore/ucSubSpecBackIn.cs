using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;

namespace FS.HISFC.Components.Speciment.InStore
{
    public partial class ucSubSpecBackIn :FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region ˽�б���
        private SpecTypeManage specTypeManage;
        private IceBoxManage iceBoxManage;
        private SpecBoxManage specBoxManage;
        private SubSpecManage subSpecManage;
        private BoxSpecManage boxSpecManage;
        private SubSpec subSpec;
        private SpecInOper specInOper;
        private FS.HISFC.Models.Base.Employee loginPerson;
        private string title;
        private string disTypeId;
        #endregion

        #region ���캯��
        public ucSubSpecBackIn()
        {
            InitializeComponent();
            specTypeManage = new SpecTypeManage();
            iceBoxManage = new IceBoxManage();
            specBoxManage = new SpecBoxManage();
            subSpecManage = new SubSpecManage();
            boxSpecManage = new BoxSpecManage();
            subSpec = new SubSpec();
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            specInOper = new SpecInOper();
            title = "�걾�ض�λ";
            disTypeId = "";
        }
        #endregion

        #region ˽�к���
        /// <summary>
        /// ��ʽ���걾��λ����Ϣ
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="boxBarCode">����������</param>
        /// <param name="specCol">�걾��</param>
        /// <param name="specRow">�걾��</param>
        /// <param name="desCap"></param>
        private void LocateInfo(ListBox lv, string boxBarCode, string specRow, string specCol, string desCap)
        {
            ///λ����Ϣ
            ///����ţ������������ǰ��λ
            string iceBoxId = boxBarCode.Substring(0, 3);
            string iceBox = iceBoxManage.GetIceBoxById(iceBoxId).IceBoxName;
            iceBox = "���䣺" + iceBox;
            string iceBoxLayer = boxBarCode.Substring(3, 2);
            iceBox += (" �� " + iceBoxLayer + " ��");
            lv.Items.Clear();
            if (desCap == "B")
            {
                lv.Items.Add("�ñ걾ֱ�Ӵ��ڱ�����,�걾��λ����Ϣ���£�");
                lv.Items.Add(iceBox);
                string row = boxBarCode.Substring(5, 2);
                string col = boxBarCode.Substring(7, 2);
                string layer = boxBarCode.Substring(9, 2);
                lv.Items.Add("�� " + row + " ��, �� " + col + " ��, �� " + layer + " ��");
                //lvOldLacate.Items.Add(
            }
            if (desCap == "J")
            {
                lv.Items.Add("�ñ걾���ڼ���,�걾��λ����Ϣ���£�");
                lv.Items.Add(iceBox);
                string shelfNum = boxBarCode.Substring(5, 2);
                lv.Items.Add("�� " + shelfNum + " ������");
                string inShelfNum = boxBarCode.Substring(7, 2);
                string row = boxBarCode.Substring(9, 2);
                string col = boxBarCode.Substring(11, 2);
                lv.Items.Add("�� " + row + " ��, �� " + col + " ��, �� " + inShelfNum + " ��");
            }
            lv.Items.Add("λ�ں����У� ��" + specRow + " ��, �� " + specCol + "��");
            
        }

        /// <summary>
        /// �걾�ض�λ
        /// </summary>
        private void SpecReLocate()
        {
            if (subSpec.Status == "1" || subSpec.Status == "3")
            {
                MessageBox.Show("�˱걾�Ѿ����ڿ��У�", title);
                return;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            subSpecManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            iceBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            specBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            boxSpecManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);         
            int result = -1;
            try
            {
                subSpec.Comment = "�걾�������";
                specInOper.Trans = FS.FrameWork.Management.PublicTrans.Trans;
                specInOper.SetTrans();
                specInOper.SubSpec = subSpec;
                result = specInOper.InOper();
                if (result <= -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ʧ�ܣ�", title);
                    return;
                }
                if (subSpec.BoxEndRow == 0 || subSpec.BoxEndCol==0)
                {
                    DialogResult diagResult = MessageBox.Show("ԭλ�ñ�ռ��, �Ƿ���������λ�ã�", title, MessageBoxButtons.YesNo);
                    if (diagResult == DialogResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    DialogResult diagResult = MessageBox.Show("�Ƿ�Ż�ԭλ�ã�", title, MessageBoxButtons.YesNo);
                    if (diagResult == DialogResult.Yes)
                    {
                        DateTime dtReturn = DateTime.Now;
                        string updateSql = "UPDATE SPEC_SUBSPEC" +
                                     " SET LASTRETURNTIME = to_date('" + dtReturn.ToString() + "','yyyy-mm-dd hh24:mi:ss'), STATUS = '3', ISRETURNABLE =  1" +
                                     " WHERE SUBSPECID=" + subSpec.SubSpecId.ToString();
                        result = subSpecManage.UpdateSubSpec(updateSql);
                        if (result == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����ʧ�ܣ�", title);
                        }
                        FS.FrameWork.Management.PublicTrans.Commit();
                        return;
                    }
                    //���¾ɱ걾�е�ռ������
                    if (diagResult == DialogResult.No)
                    {
                        SpecBox specBox = specBoxManage.GetBoxById(subSpec.BoxId.ToString());
                        //���±걾�еĵ�������־
                        result = specBoxManage.UpdateOccupy(subSpec.BoxId.ToString(), "0");
                        if (result == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("���±걾��ʧ�ܣ�", title);
                        }
                        int occupyCount = specBox.OccupyCount - 1;
                        if (occupyCount <= 0)
                            occupyCount = 0;
                        result = specBoxManage.UpdateOccupyCount(occupyCount.ToString(), subSpec.BoxId.ToString());
                        if (result == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("���±걾��ʧ�ܣ�", title);
                        }
                        string locateSql = "UPDATE SPEC_SUBSPEC SET BOXID=0 ,BOXSTARTROW= 0,BOXSTARTCOL=0, BOXENDROW= 0,BOXENDCOL=0"+
                                     " WHERE SPEC_SUBSPEC.SUBBARCODE= '" + txtSpecBarCode.Text.Trim() + "'";
                        //�ÿ�ԭ��λ����Ϣ
                        result = subSpecManage.UpdateSubSpec(locateSql);
                        if (result == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("�ÿ�λ��ʧ�ܣ�", title);
                        }
                    }
                }

                ArrayList arrBox = specBoxManage.GetLastLocation(disTypeId, cmbSpecType.SelectedValue.ToString()); ;
                if (arrBox == null || arrBox.Count <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�˱걾����, û�п�ʹ�õı걾��!", title);
                    return ;
                }

                SpecBox currentBox = new SpecBox();
                int boxCount = 0;
                foreach (SpecBox b in arrBox)
                {
                    if (b.BoxId > 0)
                    {
                        currentBox = b;
                        break;
                    }
                    boxCount++;
                }
                if (boxCount == arrBox.Count)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�˱걾����, û�п�ʹ�õı걾��!", title);
                    return ;
                }

                //SpecBox currentBox = specBoxManage.GetLastLocation(disTypeId, cmbSpecType.SelectedValue.ToString());
                //if (currentBox.BoxId <= 0)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show("�˱걾����, û�п�ʹ�õı걾��!", title);                    
                //    return;
                //}
                subSpec.BoxId = currentBox.BoxId;
                BoxSpec boxSpec = boxSpecManage.GetSpecByBoxId(currentBox.BoxId.ToString());
                int maxRow = boxSpec.Row;
                int maxCol = boxSpec.Col;
                //���ұ걾λ��
                SubSpec lastSubSpec = subSpecManage.ScanSpecBox(subSpec.BoxId.ToString(), boxSpec);
                int currentEndRow = lastSubSpec.BoxEndRow;
                int currentEndCol = lastSubSpec.BoxEndCol;
                if (currentEndCol < maxCol)
                {
                    subSpec.BoxStartCol = currentEndCol + 1;
                    subSpec.BoxEndCol = currentEndCol + 1;
                    subSpec.BoxEndRow = currentEndRow;
                    subSpec.BoxStartRow = subSpec.BoxEndRow;
                }
                if (currentEndCol == maxCol && currentEndRow < maxRow)
                {
                    subSpec.BoxEndCol = 1;
                    subSpec.BoxStartCol = 1;
                    subSpec.BoxStartRow = currentEndRow + 1;
                    subSpec.BoxEndRow = currentEndRow + 1;
                }
                string sql = "UPDATE SPEC_SUBSPEC SET SPECCAP = " + nudSpecCap.Value.ToString() + ", SPECTYPEID = " + cmbSpecType.SelectedValue.ToString() +
                             " , LASTRETURNTIME = to_date('" + DateTime.Now.ToString() + "','yyyy-mm-dd hh24:mi:ss'), " +
                             " STATUS = '3', BOXID=" + subSpec.BoxId.ToString() + ",BOXSTARTROW=" + subSpec.BoxStartRow.ToString() + ",BOXSTARTCOL=" + subSpec.BoxStartCol.ToString() +
                             ",BOXENDROW=" + subSpec.BoxEndRow.ToString() + ",BOXENDCOL=" + subSpec.BoxEndCol.ToString() + ",COMMENT= '" + txtComment.Text +"'"+
                             " WHERE SPEC_SUBSPEC.SUBBARCODE= '" + txtSpecBarCode.Text.Trim()+"'";
                //���±걾��λ����Ϣ
                result = subSpecManage.UpdateSubSpec(sql);
                if (result == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("������λ��ʧ�ܣ�", title);
                }
                //���µ�ǰ���ӵ�ռ����
                result = specBoxManage.UpdateOccupyCount((currentBox.OccupyCount + 1).ToString(), currentBox.BoxId.ToString());
                if (result == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���±걾��ʧ�ܣ�", title);
                }
                //�����ǰ����������ʾ���,������
                if (currentBox.OccupyCount == currentBox.Capacity)
                {
                    specBoxManage.UpdateOccupy(currentBox.BoxId.ToString(), "1");
                    DialogResult diagResult = MessageBox.Show("�ñ걾���������Ƿ����?", "�걾�����", MessageBoxButtons.YesNo);
                    if (diagResult == DialogResult.Yes)
                    {
                        result = specBoxManage.UpdateSotreFlag("1", currentBox.BoxId.ToString());
                        if (result == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("���±걾��ʧ�ܣ�", title);
                        }
                    }                    
                }
                LocateInfo(lvNewLocate, currentBox.BoxBarCode, subSpec.BoxEndRow.ToString(), subSpec.BoxEndCol.ToString(), currentBox.DesCapType.ToString());
                lvNewLocate.Visible = true;
                label8.Visible = true;
                //��ǰ�걾Ϊ�����ڿ�״̬
                subSpec.Status = "3";
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("�����ɹ���", title);
                if (currentBox.OccupyCount == currentBox.Capacity)
                {
                    MessageBox.Show("��ǰ�걾��������������µı걾�У�", title);
                    //��ʾ�û�����µı걾��
                    FS.HISFC.Components.Speciment.Setting.frmSpecBox newSpecBox = new FS.HISFC.Components.Speciment.Setting.frmSpecBox();
                    if (currentBox.DesCapType == 'B')
                        newSpecBox.CurLayerId = currentBox.DesCapID;
                    else
                        newSpecBox.CurShelfId = currentBox.DesCapID;
                    newSpecBox.Show();
                }                
            }
            catch
            {
                MessageBox.Show("������λ��ʧ�ܣ�", title);          
                FS.FrameWork.Management.PublicTrans.RollBack();
            }
        }

        /// <summary>
        /// ���ñ걾��Ϣ
        /// </summary>
        private void SpecInfo()
        {
            try
            {
                string barCode = txtSpecBarCode.Text.Trim();
                #region sql���
                string sql = " select DISTINCT SPEC_SUBSPEC.STATUS, SPEC_SUBSPEC.SUBSPECID, SPEC_SUBSPEC.SPECTYPEID, SPEC_DISEASETYPE.DISEASENAME,SPEC_SOURCE_STORE.TUMORTYPE,SPEC_SOURCE.TUMORPOR,\n" +
                             " SPEC_SUBSPEC.SPECCAP, SPEC_SUBSPEC.SPECCOUNT,SPEC_SUBSPEC.LASTRETURNTIME,SPEC_SOURCE.DISEASETYPEID,\n" +
                             " (SELECT COUNT(*) FROM SPEC_OUT WHERE SPEC_OUT.SUBSPECID=SPEC_SUBSPEC.SUBSPECID) OUTCOUNT,SPEC_SUBSPEC.BOXID,\n" +
                             " SPEC_SUBSPEC.COMMENT,SPEC_BOX.BOXBARCODE,SPEC_SUBSPEC.BOXENDROW,SPEC_SUBSPEC.BOXENDCOL,DESCAPTYPE\n" +
                             " from SPEC_SUBSPEC LEFT JOIN SPEC_SOURCE_STORE ON SPEC_SUBSPEC.STOREID = SPEC_SOURCE_STORE.SOTREID \n" +
                             " LEFT JOIN SPEC_SOURCE ON SPEC_SUBSPEC.SPECID = SPEC_SOURCE.SPECID \n" +
                             " LEFT JOIN SPEC_DISEASETYPE ON SPEC_SOURCE.DISEASETYPEID = SPEC_DISEASETYPE.DISEASETYPEID \n" +
                             " LEFT JOIN SPEC_OUT ON SPEC_OUT.SUBSPECID = SPEC_SUBSPEC.SUBSPECID \n" +
                             " LEFT JOIN SPEC_BOX ON SPEC_SUBSPEC.BOXID = SPEC_BOX.BOXID \n" +
                             " WHERE SPEC_SUBSPEC.SUBBARCODE = '" + barCode + "'";
                #endregion
                DataSet ds = new DataSet();
                specTypeManage.ExecQuery(sql, ref ds);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    subSpec.Status = dt.Rows[0]["STATUS"].ToString();
                    subSpec.BoxEndCol = Convert.ToInt32(dt.Rows[0]["BOXENDCOL"].ToString());
                    subSpec.BoxEndRow = Convert.ToInt32(dt.Rows[0]["BOXENDROW"].ToString());
                    subSpec.Comment = dt.Rows[0]["COMMENT"].ToString();
                    subSpec.LastReturnTime = Convert.ToDateTime(dt.Rows[0]["LASTRETURNTIME"].ToString());
                    subSpec.SpecCap = Convert.ToDecimal(dt.Rows[0]["SPECCAP"].ToString());
                    subSpec.SpecCount = Convert.ToInt32(dt.Rows[0]["SPECCOUNT"].ToString());
                    subSpec.SpecTypeId = Convert.ToInt32(dt.Rows[0]["SPECTYPEID"].ToString());
                    subSpec.SubSpecId = Convert.ToInt32(dt.Rows[0]["SUBSPECID"].ToString());
                    subSpec.BoxId = Convert.ToInt32(dt.Rows[0]["BOXID"].ToString());
                    disTypeId = dt.Rows[0]["DISEASETYPEID"].ToString();
                    txtComment.Text = subSpec.Comment;
                    txtDisType.Text = dt.Rows[0]["DISEASENAME"].ToString();
                    txtLastReturn.Text = subSpec.LastReturnTime.ToString("yyyy-MM-dd");
                    txtOutCount.Text = dt.Rows[0]["OUTCOUNT"].ToString();
                    #region ������
                    if (dt.Rows[0]["TUMORPOR"].ToString() != "")
                    {
                        char[] tumorPor = dt.Rows[0]["TUMORPOR"].ToString().ToCharArray();
                        Constant.TumorPro TumorPro = Constant.TumorPro.ԭ����;
                        foreach (char t in tumorPor)
                        {
                            TumorPro = (Constant.TumorPro)(Convert.ToInt32(t.ToString()));
                            txtTumorPro.Text = "";
                            switch (TumorPro)
                            {
                                //�걾����1.ԭ���� 2.������ 3.ת�ư�
                                case Constant.TumorPro.ԭ����:
                                    txtTumorPro.Text += "ԭ����";
                                    break;
                                case Constant.TumorPro.������:
                                    txtTumorPro.Text += "������";
                                    break;
                                case Constant.TumorPro.ת�ư�:
                                    txtTumorPro.Text += "ת�ư�";
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    #endregion
                    #region ������
                    if (dt.Rows[0]["TUMORTYPE"].ToString().Trim() != "")
                    {
                        //1.���� 2.���� 3.���� 4�����硢�ж� 5.��˨ 6, ��(ѪҺ), 7 ����(ѪҺ)
                        char[] tumorType = dt.Rows[0]["TUMORTYPE"].ToString().ToCharArray();
                        foreach (char t in tumorType)
                        {
                            txtTumorType.Text = "";
                            Constant.TumorType TumorType = (Constant.TumorType)(Convert.ToInt32(t.ToString()));
                            switch (TumorType)
                            {
                                case Constant.TumorType.����:
                                    txtTumorType.Text += "����";
                                    break;
                                case Constant.TumorType.����:
                                    txtTumorType.Text += "����";
                                    break;
                                //case Constant.TumorType.����:
                                //    txtTumorType.Text += "��";
                                //    break;
                                case Constant.TumorType.����:
                                    txtTumorType.Text += "����";
                                    break;
                                case Constant.TumorType.��˨:
                                    txtTumorType.Text += "��˨";
                                    break;
                                //case Constant.TumorType.�ж�:
                                //    txtTumorType.Text += "�ж�";
                                //    break;
                                case Constant.TumorType.����:
                                    txtTumorType.Text += "����";
                                    break;
                                case Constant.TumorType.�ܰͽ�:
                                    txtTumorType.Text += "�ܰͽ�";
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    #endregion
                    nudSpecCap.Value = Convert.ToDecimal(dt.Rows[0]["SPECCAP"].ToString());
                    nudSpecCount.Value = Convert.ToDecimal(dt.Rows[0]["SPECCOUNT"].ToString());
                    cmbSpecType.SelectedValue = subSpec.SpecTypeId;
                    string boxBarCode = dt.Rows[0]["BOXBARCODE"].ToString();
                    if (subSpec.BoxEndCol == 0 || subSpec.BoxEndRow == 0)
                    {
                        lvOldLacate.Items.Add("ԭλ�ñ�ռ�ã�");
                    }
                    #region �걾��ԭλ����Ϣ
                    ///λ����Ϣ
                    LocateInfo(lvOldLacate, boxBarCode, dt.Rows[0]["BOXENDROW"].ToString(), dt.Rows[0]["BOXENDCOL"].ToString(), dt.Rows[0]["DESCAPTYPE"].ToString());                   
                    #endregion
                }

            }
            catch
            {
 
            }
        }

        /// <summary>
        /// �걾���Ͱ�
        /// </summary>
        private void SpecTypeBinding()
        {
            string sql = "select * from SPEC_TYPE";
            ArrayList arrSpecType = specTypeManage.GetSpecType(sql);
            cmbSpecType.ValueMember = "SpecTypeID";  
            cmbSpecType.DisplayMember = "SpecTypeName";
            cmbSpecType.DataSource = arrSpecType;
            //specTypeManage
        }
        #endregion

        #region �¼�
        private void ucSubSpecBackIn_Load(object sender, EventArgs e)
        {
            SpecTypeBinding();
        }

        private void txtSpecBarCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSpecBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                lvOldLacate.Items.Clear();
                lvNewLocate.Items.Clear();
                SpecInfo();
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            SpecReLocate();
        }
        #endregion
    }
}
