using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Neusoft.UFC.Preparation.Dininfect
{
    /// <summary>
    /// <br></br>
    /// [��������: �Ƽ�ԭ��������(��Ӧ��)]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-09]<br></br>
    /// <˵��>
    ///    1���Ƽ����Ͽۿ�ʵ��
    ///    2���Բ�����ԭ�����Զ��γ�����ƻ�
    ///    3������������á����ӳ�Ʒ�����γɳ�Ʒ�������
    ///    4��������Ϣ�������ʳ������ݻ�ȡ
    /// </˵��>
    /// </summary>
    public partial class ucExpandManager : Neusoft.NFC.Interface.Controls.ucBaseControl
    {
        public ucExpandManager()
        {
            InitializeComponent();
        }

        #region ö��

        private enum ExpandColumnSet
        {
            /// <summary>
            /// ԭ������
            /// </summary>
            ColMaterialName,
            /// <summary>
            /// ���
            /// </summary>
            ColSpecs,
            /// <summary>
            /// ����
            /// </summary>
            ColPrice,
            /// <summary>
            /// ��׼������
            /// </summary>
            ColNormativeQty,
            /// <summary>
            /// ����������
            /// </summary>
            ColPlanExpand,
            /// <summary>
            /// �����
            /// </summary>
            ColStore,
            /// <summary>
            /// ʵ��������
            /// </summary>
            ColFactualExpand,
            /// <summary>
            /// ��ע
            /// </summary>
            ColMemo
        }

        #endregion
    }
}
