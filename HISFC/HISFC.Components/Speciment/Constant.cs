using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Components.Speciment
{
    public class Constant
    {
        public static int BarCodeLength = 10;
        /// <summary>
        /// ��������
        /// </summary>
        public enum TumorType
        {
             ���� =1,
             ����=2,
             ����=3,
             ����,
             ��˨=5,
             ����=6,
             ����Ѫ,
             �ܰͽ� = 8
        }

        public enum TumorTypeCode
        {
            T=1,
            S=2,
            P=3,
            N,
            E=5,
            L=8
        }

        /// <summary>
        /// ��������
        /// </summary>
        public enum TumorPro
        {
            ԭ����=1,
            ������,
            ת�ư�,
            ����
        }

        /// <summary>
        /// ���ƽ׶�
        /// </summary>
        public enum GetPeriod
        {
            ����ǰ=1,
            ������,//2
            ����ǰ,//3
            ���ƺ�,//4
            ����ǰ,//5
            ���ƺ�,//6
            ��,//7
            ����//8
        }
    }
}
