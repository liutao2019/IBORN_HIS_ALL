using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace UFC.Speciment
{
    public partial class ucCureFun : UserControl
    {
        #region ˽�б���
        /// <summary>
        /// ������Ϣ
        /// </summary>
        private CureInfo cureInfo;
        #endregion

        #region ���캯��
        public ucCureFun()
        {
            InitializeComponent();
            cureInfo = new CureInfo();
        }
        #endregion

        #region ��������
        public CureInfo CureInfo
        {
            get
            {
                GetCureInfo();
                return cureInfo;
            }
            set
            {
                cureInfo = value;
            }
        }
        #endregion

        #region ���з���
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        public void GetCureInfo()
        {
            if(rbtAfterCure.Checked) cureInfo.getPeroid = "2";
            else cureInfo.getPeroid = "1";
            cureInfo.operPos = txtOperPos.Text;
            if(txtRadSch.Tag!=null) cureInfo.radScheme = txtRadSch.Tag.ToString();
            if(txtMedSch.Tag!=null) cureInfo.medScheme = txtMedSch.Tag.ToString();
            cureInfo.Comment = txtComment.Text;
        }
        #endregion

        #region �¼�
        private void ucCureFun_Load(object sender, EventArgs e)
        {
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            //���Ʒ�ʽ 
            ArrayList RmodeidList = con.GetList(FS.HISFC.Models.Base.EnumConstant.RADIATETYPE);
            txtRadSch.AddItems(RmodeidList);

            //���Ʒ�ʽ
            ArrayList CmodeidList = con.GetList(FS.HISFC.Models.Base.EnumConstant.CHEMOTHERAPY);

            txtMedSch.AddItems(CmodeidList);
        }
        #endregion
    }

    #region �ڲ�������
    /// <summary>
    /// ������Ϣ
    /// </summary>
    public class CureInfo
    {
        public string getPeroid;
        public string operPos;
        public string radScheme;
        public string medScheme;
        public string Comment;
    }
    #endregion
}
