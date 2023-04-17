using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.InpatientFee.QiaoTou
{
    public partial class ucFinIpbInPatient : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        /// <summary>
        /// ��ͷסԺ����һ����{26AE821F-F32D-4ce6-B18E-1080B5D9E803}
        /// </summary>
        public ucFinIpbInPatient()
        {
            InitializeComponent();
            this.Load += new EventHandler(ucFinIpbInPatient_Load);
        }

        void ucFinIpbInPatient_Load(object sender, EventArgs e)
        {
            #region ����
            ArrayList alDept = new ArrayList();
            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
            alDept = deptMgr.GetDeptmentAll();
            ArrayList inpatientDept = new ArrayList();
            FS.HISFC.Models.Base.Department tempDeptAll =new FS.HISFC.Models.Base.Department ();
            tempDeptAll.ID = "ALL";
            tempDeptAll.Name = "ȫ��";
            inpatientDept.Add(tempDeptAll);
            foreach (FS.HISFC.Models.Base.Department dept in alDept)
            {
                if (dept.DeptType.ID.ToString() == "I")
                {
                    inpatientDept.Add(dept);
                }
            }
            this.cmbDept.AddItems(inpatientDept);
            this.cmbDept.SelectedIndex = 0;
            #endregion ����

            //#region סԺ״̬
            ////R-סԺ�Ǽ�  I-�������� B-��Ժ�Ǽ� O-��Ժ���� P-ԤԼ��Ժ,N-�޷���Ժ
            //ArrayList alInStata = new ArrayList();
            ////ȫ��
            //FS.HISFC.Models.Base.Const allinstate0 = new FS.HISFC.Models.Base.Const();
            //allinstate0.ID = "ALL";
            //allinstate0.Name = "ȫ��";
            //allinstate0.SpellCode = "QB";
            //alInStata.Add(allinstate0);
            ////סԺ�Ǽ�
            //FS.HISFC.Models.Base.Const allinstate1 = new FS.HISFC.Models.Base.Const();
            //allinstate1.ID = "R";
            //allinstate1.Name = "סԺ�Ǽ�";
            //allinstate1.SpellCode = "ZYDJ";
            //alInStata.Add(allinstate1);
            ////��������
            //FS.HISFC.Models.Base.Const allinstate2 = new FS.HISFC.Models.Base.Const();
            //allinstate2.ID = "I";
            //allinstate2.Name = "��������";
            //allinstate2.SpellCode = "BFJZ";
            //alInStata.Add(allinstate2);
            ////��Ժ�Ǽ�
            //FS.HISFC.Models.Base.Const allinstate3 = new FS.HISFC.Models.Base.Const();
            //allinstate3.ID = "B";
            //allinstate3.Name = "��Ժ�Ǽ�";
            //allinstate3.SpellCode = "CYDJ";
            //alInStata.Add(allinstate3);
            ////��Ժ����
            //FS.HISFC.Models.Base.Const allinstate4 = new FS.HISFC.Models.Base.Const();
            //allinstate4.ID = "O";
            //allinstate4.Name = "��Ժ����";
            //allinstate4.SpellCode = "CYJS";
            //alInStata.Add(allinstate4);
            ////ԤԼ��Ժ
            //FS.HISFC.Models.Base.Const allinstate5 = new FS.HISFC.Models.Base.Const();
            //allinstate5.ID = "P";
            //allinstate5.Name = "ԤԼ��Ժ";
            //allinstate5.SpellCode = "YYCY";
            //alInStata.Add(allinstate5);
            ////�޷���Ժ
            //FS.HISFC.Models.Base.Const allinstate6 = new FS.HISFC.Models.Base.Const();
            //allinstate6.ID = "N";
            //allinstate6.Name = "�޷���Ժ";
            //allinstate6.SpellCode = "WFTY";
            //alInStata.Add(allinstate6);
            //this.cmbInState.AddItems(alInStata);
            //this.cmbInState.SelectedIndex = 0;
            //#endregion סԺ״̬

            #region ����״̬
            ArrayList alFeeState = new ArrayList();
            FS.HISFC.Models.Base.Const tempFeeState1 = new FS.HISFC.Models.Base.Const();
            tempFeeState1.ID = "ALL";
            tempFeeState1.Name = "ȫ��";
            alFeeState.Add(tempFeeState1);
            FS.HISFC.Models.Base.Const tempFeeState2 = new FS.HISFC.Models.Base.Const ();
            tempFeeState2.ID = "WJS";
            tempFeeState2.Name = "δ����";
            alFeeState.Add(tempFeeState2);
            FS.HISFC.Models.Base.Const tempFeeState3 = new FS.HISFC.Models.Base.Const ();
            tempFeeState3.ID = "JS";
            tempFeeState3.Name = "�ѽ���";
            alFeeState.Add(tempFeeState3);
            this.cmbFeeState.AddItems(alFeeState);
            this.cmbFeeState.SelectedIndex = 0;
            #endregion ����״̬
        }
        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            string temp = "";
            //R-סԺ�Ǽ�  I-�������� B-��Ժ�Ǽ� O-��Ժ���� P-ԤԼ��Ժ,N-�޷���Ժ
            if (this.cmbFeeState.SelectedItem.Name.ToString() == "ȫ��")
            {
                temp = "'R','I','B','P','N','O'";

            }
            else if (this.cmbFeeState.SelectedItem.Name.ToString() == "�ѽ���")
            {
                temp = "'O'";
            }
            else
            {
                temp = "'R','I','B','P','N'";

            }
            return base.OnRetrieve(base.beginTime, base.endTime, this.cmbDept.SelectedItem.ID.ToString(), temp);//, temp);
        }
    }
}

