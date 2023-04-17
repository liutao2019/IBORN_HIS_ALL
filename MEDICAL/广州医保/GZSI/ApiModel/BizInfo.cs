using System;
using System.Collections.Generic;
using System.Text;

namespace GZSI.ApiModel
{
    /// <summary>
    /// ����ҵ����Ϣ
    /// </summary>
    public class BizInfo
    {

        /// <summary>
        /// ��ҽ�ǼǺ�
        /// </summary>
        public string serial_no = string.Empty;
        /// <summary>
        /// ��Ա��Ϣ
        /// </summary>
        public PersonInfo personInfo = new PersonInfo();
        /// <summary>
        /// ҽԺ���
        /// </summary>
        public string hospital_id = string.Empty;
        /// <summary>
        /// ҵ������
        /// </summary>
        public string biz_type = string.Empty;
        /// <summary>
        /// ���ı���
        /// </summary>
        public string center_id = string.Empty;

        /// <summary>
        /// ҽ������(25λ)
        /// </summary>
        public string ic_no = string.Empty;


        /// <summary>
        /// �������
        /// </summary>
        public string treatment_type = string.Empty;


        /// <summary>
        /// ҵ��Ǽ�����(��ʽ��YYYY-MM-DD hh:mi:ss)
        /// </summary>
        public string reg_date = string.Empty;

        /// <summary>
        /// ��������(��ʽ��YYYY-MM-DD hh:mi:ss)
        /// </summary>
        public string diagnose_date = String.Empty;
        /// <summary>
        /// �Ǽ��˹���
        /// </summary>
        public string reg_staff = string.Empty;
        /// <summary>
        /// �Ǽ���
        /// </summary>
        public string reg_man = string.Empty;
        /// <summary>
        /// �ǼǱ�־
        /// </summary>
        public string reg_flag = string.Empty;
        /// <summary>
        /// ҵ��ʼʱ��(���ڸ�ʽYYYY-MM-DD)
        /// </summary>
        public string begin_date = string.Empty;
        /// <summary>
        /// ҵ��ʼ���
        /// </summary>
        public string reg_info = string.Empty;
        /// <summary>
        /// ��Ժ����
        /// </summary>
        public string in_dept = string.Empty;
        /// <summary>
        /// ��Ժ��������
        /// </summary>
        public string in_dept_name = string.Empty;
        /// <summary>
        /// ��Ժ����
        /// </summary>
        public string in_area = string.Empty;
        /// <summary>
        /// ��Ժ��������
        /// </summary>
        public string in_area_name = string.Empty;
        /// <summary>
        /// ��Ժ��λ��
        /// </summary>
        public string in_bed = string.Empty;

        /// <summary>
        /// ҽԺҵ���(�Һ�)
        /// </summary>
        public string patient_id = string.Empty;
        /// <summary>
        /// ��Ժ������ϣ�icd�룩
        /// </summary>
        public string in_disease = string.Empty;
        /// <summary>
        /// ��������
        /// </summary>
        public string disease = string.Empty;
        /// <summary>
        /// �ÿ���־
        /// </summary>
        public string ic_flag = string.Empty;
        /// <summary>
        /// ��ע
        /// </summary>
        public string remark = string.Empty;


        /// <summary>
        /// �ܷ���
        /// </summary>
        public decimal total_fee = 0m;

        /// <summary>
        /// ��������
        /// </summary>
        public string fee_batch = string.Empty;

        /// <summary>
        /// ����ƾ֤�ţ�������ҽƾ֤�ţ�
        /// </summary>
        public string injury_borth_sn = string.Empty;

        /// <summary>
        /// �����������
        /// </summary>
        public string serial_apply = string.Empty;


    }
}
