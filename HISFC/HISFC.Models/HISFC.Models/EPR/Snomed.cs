using System;
using System.Collections;
namespace FS.HISFC.Models.EPR
{

   

    #region SNOMED
    /// <summary>
    /// SNOPMED
    /// </summary>
    [Serializable]
    public class SNOMED : FS.FrameWork.Models.NeuObject, FS.HISFC.Models.Base.ISpell, FS.HISFC.Models.Base.ISort
    {
        public SNOMED()
        {

        }
        /// <summary>
        /// ��������
        /// </summary>
        public string ParentCode = "";
        /// <summary>
        /// Ӣ������
        /// </summary>
        public string EnglishName = "";

        /// <summary>
        /// ��ʽ����
        /// </summary>
        public string SNOPCode = "";

        /// <summary>
        /// ��ϱ���
        /// </summary>
        public string DiagnoseCode = "";

        #region ISpellCode ��Ա
        private string wbcode = "";
        public string WBCode
        {
            get
            {
                // TODO:  ��� SysClass.WB_Code getter ʵ��
                return wbcode;
            }
            set
            {
                // TODO:  ��� SysClass.WB_Code setter ʵ��
                wbcode = value;
            }
        }
        private string spellcode = "";
        public string SpellCode
        {
            get
            {
                // TODO:  ��� SysClass.Spell_Code getter ʵ��
                return spellcode;
            }
            set
            {
                // TODO:  ��� SysClass.Spell_Code setter ʵ��
                spellcode = value;
            }
        }
        private string usercode = "";
        public string UserCode
        {
            get
            {
                // TODO:  ��� SysClass.User_Code getter ʵ��
                return usercode;
            }
            set
            {
                // TODO:  ��� SysClass.User_Code setter ʵ��
                usercode = value;
            }
        }

        #endregion

        #region ISort ��Ա
        private int sortid = 0;
        public int SortID
        {
            get
            {
                // TODO:  ��� SysClass.SortID getter ʵ��
                return sortid;
            }
            set
            {
                // TODO:  ��� SysClass.SortID setter ʵ��
                sortid = value;
            }
        }

        #endregion


        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new SNOMED Clone()
        {
            return base.Clone() as SNOMED;
        }
    }
    #endregion

}