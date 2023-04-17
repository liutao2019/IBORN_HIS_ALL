using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections;
using Neusoft.HISFC.Object.Pharmacy;

namespace Neusoft.UFC.DrugStore.Outpatient
{
    [DefaultPropertyAttribute( "����" )]
    public class DrugTerminalClass
    {

        #region ���캯��

        /// <summary>
        /// ���������Ĺ��캯��
        /// </summary>
        public DrugTerminalClass( )
        {
        }
        /// <summary>
        /// �������Ĺ��캯��
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="terminalType">�ն�����</param>
        public DrugTerminalClass( string deptCode , string terminalType )
        {
            //��ȡ�ն��б�
            Neusoft.HISFC.Management.Pharmacy.DrugStore drugStore = new Neusoft.HISFC.Management.Pharmacy.DrugStore( );
            ArrayList al = drugStore.QueryDrugTerminalByDeptCode( deptCode , terminalType );
            string[ ] temp = new string[ al.Count + 1 ];

            temp[ 0 ] = "�����";

            for( int i = 1 ; i < al.Count ; i++ )
            {
                Neusoft.HISFC.Object.Pharmacy.DrugTerminal info = al[ i ] as Neusoft.HISFC.Object.Pharmacy.DrugTerminal;
                temp[ i ] = "<" + info.ID + ">" + info.Name;
            }

            ReplaceConverter.EnumString = temp;

            //��ȡ��ҩ�����б�
            ArrayList tempAl = drugStore.QueryDrugTerminalByDeptCode( deptCode , "0" );
            string[ ] tempStr = new string[ tempAl.Count ];

            for( int i = 0 ; i < tempAl.Count ; i++ )
            {
                Neusoft.HISFC.Object.Pharmacy.DrugTerminal info = tempAl[ i ] as Neusoft.HISFC.Object.Pharmacy.DrugTerminal;
                tempStr[ i ] = "<" + info.ID + ">" + info.Name;
            }

            SendWindowConverter.EnumString = tempStr;

        }

        #endregion

        #region ����

        private string name = ""; //�ն�����
        private EnumTerminalType enumType = EnumTerminalType.��ҩ����; //�ն�����
        private EnumTerminalProperty enumProperty = EnumTerminalProperty.��ͨ; //�ն�����
        private string replaceName = "";					//����ն�
        private string isClose = "��";						//�Ƿ�ر�
        private string isAutoPrint = "��";					//�Ƿ��Զ���ӡ
        private decimal refreshInterval1 = 10;				//����ˢ�¼��
        private decimal refreshInterval2 = 10;				//��ӡ/��ʾ ˢ�¼��
        private int alertNum = 25;						    //������
        private int showNum = 5;						    //��ʾ����
        private string sendWindow = "";					    //��ҩ����(ֻ������ҩ̨)
        private string mark = "";						    //��ע

        #endregion

        #region ����

        [CategoryAttribute( "������Ϣ" ) , DescriptionAttribute( "��ҩ̨/��ҩ��������" )]
        public string ����
        {
            get { return name; }
            set { name = value; }
        }

        [CategoryAttribute( "������Ϣ" ) ,
        DescriptionAttribute( "�ն���� ��ҩ����/��ҩ̨" ) ,
        ReadOnlyAttribute( true )]
        public EnumTerminalType ���
        {
            get { return enumType; }
            set { enumType = value; }
        }


        [CategoryAttribute( "������Ϣ" ) ,
        DescriptionAttribute( "�ն����� ��ͨ�����⡢ר��" ) ,
        DefaultValueAttribute( EnumTerminalProperty.��ͨ )]
        public EnumTerminalProperty ����
        {
            get { return enumProperty; }
            set { enumProperty = value; }
        }


        [CategoryAttribute( "ʹ����Ϣ" ) ,
        DescriptionAttribute( "���ն˹ر�ʱ������ն�" ) ,
        TypeConverter( typeof( ReplaceConverter ) )
        ]
        public string ����ն�
        {
            get { return replaceName; }
            set { replaceName = value; }
        }


        [CategoryAttribute( "ʹ����Ϣ" ) ,
        DescriptionAttribute( "�Ƿ����ø��ն�" ) ,
        DefaultValueAttribute( "��" ) ,
        TypeConverter( typeof( IsTure ) )
        ]
        public string �Ƿ�ر�
        {
            get { return isClose; }
            set { isClose = value; }
        }


        [CategoryAttribute( "ʹ����Ϣ" ) ,
        DescriptionAttribute( "����ҩ̨��ҩʱ�Ƿ��Զ���ӡ��ҩ��ǩ,�Է�ҩ���ڸò���������" ) ,
        DefaultValueAttribute( "��" ) ,
        TypeConverter( typeof( IsTure ) )
        ]
        public string �Ƿ��Զ���ӡ
        {
            get { return isAutoPrint; }
            set { isAutoPrint = value; }
        }


        [CategoryAttribute( "ʹ����Ϣ" ) ,
        DescriptionAttribute( "�ڵ��Խ���ˢ�»����б��ʱ���� ����ҩ̨Ϊ��ǩ��ӡ��� " ) ,
        DefaultValueAttribute( 10.0 )]
        public decimal ����ˢ�¼��
        {
            get { return refreshInterval1; }
            set { refreshInterval1 = value; }
        }


        [CategoryAttribute( "ʹ����Ϣ" ) ,
        DescriptionAttribute( "�Է�ҩ����Ϊ����Ļˢ�»����б�ʱ����" ) ,
        DefaultValueAttribute( 10.0 )]
        public decimal ��ʾˢ�¼��
        {
            get { return refreshInterval2; }
            set { refreshInterval2 = value; }
        }


        [CategoryAttribute( "ʹ����Ϣ" ) ,
        DescriptionAttribute( "����ҩ̨Ϊ����������ҩ̨�Ĵ���ҩ����������ֵ �Է�ҩ����Ϊ��ȡҩ���ߵľ���ֵ" ) ,
        DefaultValueAttribute( 25 )]
        public int ������
        {
            get { return alertNum; }
            set { alertNum = value; }
        }


        [CategoryAttribute( "ʹ����Ϣ" ) ,
        DescriptionAttribute( "ÿ�δ���Ļˢ����ʾ�Ļ�������" ) ,
        DefaultValueAttribute( 5 )]
        public int ��ʾ����
        {
            get { return showNum; }
            set { showNum = value; }
        }


        [CategoryAttribute( "����" ) ,
        DescriptionAttribute( "��ҩ̨��Ӧ�ķ�ҩ���ڡ��Է�ҩ���ڸò���������" ) ,
        TypeConverter( typeof( SendWindowConverter ) )
        ]
        public string ��ҩ����
        {
            get { return sendWindow; }
            set { sendWindow = value; }
        }


        [CategoryAttribute( "����" ) ,
        DescriptionAttribute( "����˵��" )]
        public string ��ע
        {
            get { return mark; }
            set { mark = value; }
        }


        #endregion


    }

    /// <summary>
    /// ��д���ࡢʵ�ֶ��Ƿ��б�ѡ��
    /// </summary>
    public class IsTure : StringConverter
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public IsTure( )
        {
        }

        private static string[ ] str = { "��" , "��" };
        public static string[ ] EnumString
        {
            get { return str; }
            set { str = value; }
        }

        /// <summary>
        /// �趨�ö���֧�ִ��б���ѡ��һ���׼ֵ
        /// </summary>
        /// <param name="context">Ҫ��ȡֵ�����</param>
        /// <returns>����True</returns>
        public override bool GetStandardValuesSupported( ITypeDescriptorContext context )
        {
            return true;
        }


        /// <summary>
        /// ��������б������
        /// </summary>
        /// <param name="context">Ҫ��ȡֵ�����</param>
        /// <returns></returns>
        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues( ITypeDescriptorContext context )
        {
            return new StandardValuesCollection( EnumString );
        }


        /// <summary>
        /// ����Ա�Ƿ���������������ڲ����ڵ�ֵ
        /// </summary>
        /// <param name="context">Ҫ��ȡֵ�����</param>
        /// <returns></returns>
        public override bool GetStandardValuesExclusive(  ITypeDescriptorContext context )
        {
            return true;
        }

    }


    /// <summary>
    /// ��д���ࡢʵ�ֶ�����ն���ʾ�б�ѡ��
    /// </summary>
    public class ReplaceConverter : StringConverter
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ReplaceConverter( )
        {
        }

        private static string[ ] str = { };
        public static string[ ] EnumString
        {
            get { return str; }
            set { str = value; }
        }


        /// <summary>
        /// �趨�ö���֧�ִ��б���ѡ��һ���׼ֵ
        /// </summary>
        /// <param name="context">Ҫ��ȡֵ�����</param>
        /// <returns>����True</returns>
        public override bool GetStandardValuesSupported( ITypeDescriptorContext context )
        {
            return true;
        }


        /// <summary>
        /// ��������б������
        /// </summary>
        /// <param name="context">Ҫ��ȡֵ�����</param>
        /// <returns></returns>
        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues( ITypeDescriptorContext context )
        {

            return new StandardValuesCollection( EnumString );
        }


        /// <summary>
        /// ����Ա�Ƿ���������������ڲ����ڵ�ֵ
        /// </summary>
        /// <param name="context">Ҫ��ȡֵ�����</param>
        /// <returns></returns>
        public override bool GetStandardValuesExclusive( ITypeDescriptorContext context )
        {
            return true;
        }

    }


    /// <summary>
    /// ��д���ࡢʵ�ֶԷ�ҩ������ʾ�б�ѡ��
    /// </summary>
    public class SendWindowConverter : StringConverter
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public SendWindowConverter( )
        {
        }

        private static string[ ] str = { };
        public static string[ ] EnumString
        {
            get { return str; }
            set { str = value; }
        }


        /// <summary>
        /// �趨�ö���֧�ִ��б���ѡ��һ���׼ֵ
        /// </summary>
        /// <param name="context">Ҫ��ȡֵ�����</param>
        /// <returns>����True</returns>
        public override bool GetStandardValuesSupported( ITypeDescriptorContext context )
        {
            return true;
        }


        /// <summary>
        /// ��������б������
        /// </summary>
        /// <param name="context">Ҫ��ȡֵ�����</param>
        /// <returns></returns>
        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues( ITypeDescriptorContext context )
        {
            return new StandardValuesCollection( EnumString );
        }


        /// <summary>
        /// ����Ա�Ƿ���������������ڲ����ڵ�ֵ
        /// </summary>
        /// <param name="context">Ҫ��ȡֵ�����</param>
        /// <returns></returns>
        public override bool GetStandardValuesExclusive( ITypeDescriptorContext context )
        {
            return true;
        }

    }

}
