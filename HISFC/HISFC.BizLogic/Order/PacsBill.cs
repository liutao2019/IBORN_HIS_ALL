using System;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.HISFC.BizLogic.Order
{
	/// <summary>
	/// ��������
	/// written by zuowy 
	/// 2005-8-20
	/// </summary>
	public class PacsBill:FS.FrameWork.Management.Database 
	{
        public PacsBill()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        /// <summary>
        /// ����ʱ���ж�
        /// </summary>
        /// <param name="pacsbill"></param>
        public int SetPacsBill(FS.HISFC.Models.Order.PacsBill pacsbill)
        {
            int Parm;
            Parm = this.UpdatePacsBill(pacsbill);
            if (Parm == 0)
                Parm = this.SavePacsBill(pacsbill);
            return Parm;
        }
        /// <summary>
        /// ��ѯ��鵥��Ϣ
        /// </summary>
        /// <param name="combNo"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Order.PacsBill QueryPacsBill(string combNo)
        {
            # region ��ѯ��鵥��Ϣ
            // ��ѯ��鵥��Ϣ
            // Management.Order.SelectPacsBill
            // ���� 1 ���� 11
            # endregion
            string strSql = "";
            ArrayList al = null;
            if (this.Sql.GetCommonSql("Management.Order.QueryResourceByPacsBillNo", ref strSql) == -1)
            {
                this.Err = "û���ҵ�Management.Order.QueryResourceByPacsBillNo�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            strSql = string.Format(strSql, combNo);
            al = this.myPacsBillQuery(strSql);
            if (al == null || al.Count == 0) return null;
            return al[0] as FS.HISFC.Models.Order.PacsBill;
        }

        public FS.HISFC.Models.Order.PacsBill QueryPacsApply(string interfaceCode)
        {
            # region ��ѯ��鵥��Ϣ
            // ��ѯ��鵥��Ϣ
            // Management.Order.SelectPacsBill
            // ���� 1 ���� 11
            # endregion
            string strSql = "";
            ArrayList al = null;
            if (this.Sql.GetCommonSql("Management.Order.interfaceCode", ref strSql) == -1)
            {
                this.Err = "û���ҵ�Management.Order.interfaceCode�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            strSql = string.Format(strSql, interfaceCode);
            al = this.myPacsBillQuery(strSql);
            if (al == null || al.Count == 0) return null;
            return al[0] as FS.HISFC.Models.Order.PacsBill;
        }
        /// <summary>
        /// ���ݿ��Ҵ����������Ϣ
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public ArrayList QueryPacsBillByDept(string deptCode, DateTime dtBegin, DateTime dtEnd)
        {
            # region ��ѯ��鵥��Ϣ
            // ��ѯ��鵥��Ϣ
            // Management.Order.SelectPacsBill
            // ���� 1 ���� 11
            # endregion
            string strSql = "";
            ArrayList al = null;
            if (this.Sql.GetCommonSql("Management.Order.QueryResourceByDeptCode", ref strSql) == -1)
            {
                this.Err = "û���ҵ�Management.Order.QueryResourceByDeptCode�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            strSql = string.Format(strSql, deptCode, dtBegin, dtEnd);
            al = this.myPacsBillQuery(strSql);

            return al;
        }
        /// <summary>
        /// ���ݿ��Ų�������Ϣ
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public ArrayList QueryPacsBillByCardNo(string cardNo, DateTime dtBegin, DateTime dtEnd)
        {
            # region ��ѯ��鵥��Ϣ
            // ��ѯ��鵥��Ϣ
            // Management.Order.SelectPacsBill
            // ���� 1 ���� 11
            # endregion
            string strSql = "";
            ArrayList al = null;
            if (this.Sql.GetCommonSql("Management.Order.QueryPacsBillByCardNo", ref strSql) == -1)
            {
                this.Err = "û���ҵ�Management.Order.QueryPacsBillByCardNo�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            strSql = string.Format(strSql, cardNo, dtBegin, dtEnd);
            al = this.myPacsBillQuery(strSql);

            return al;
        }
        /// <summary>
        /// �����µļ�鵥
        /// </summary>
        /// <param name="PacsBill"></param>
        /// <returns></returns>
        public int SavePacsBill(FS.HISFC.Models.Order.PacsBill PacsBill)
        {
            // �����µļ�鵥
            // Management.Order.InsertPacsBill
            // ���� 12 ���� 0
            string strSql = "";
            if (this.Sql.GetCommonSql("Management.Order.InsertPacsBill", ref strSql) == -1) return -1;
            strSql = this.getPacsBillInfo(strSql, PacsBill);

            if (strSql == null)
            {
                this.Err = "��ʽ��Sql���ʱ����";
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ���¼�鵥��Ϣ
        /// </summary>
        /// <param name="pacsbill"></param>
        /// <returns></returns>
        public int UpdatePacsBill(FS.HISFC.Models.Order.PacsBill pacsbill)
        {
            // ���¼�鵥
            // Management.Order.UpdatePacsBill
            // ���� 12 ���� 0
            string strSql = "";
            if (this.Sql.GetCommonSql("Management.Order.UpdatePacsBill", ref strSql) == -1) return -1;
            strSql = this.getPacsBillInfo(strSql, pacsbill);

            if (strSql == null)
            {
                this.Err = "��ʽ��Sql���ʱ����";
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ���¼�鵥��Ϣ
        /// </summary>
        /// <param name="pacsbill"></param>
        /// <returns></returns>
        public int UpdatePacsBillState(FS.HISFC.Models.Order.PacsBill pacsbill)
        {
            // ���¼�鵥
            // Management.Order.UpdatePacsBill
            // ���� 12 ���� 0
            string strSql = "";
            if (this.Sql.GetCommonSql("Management.Order.UpdatePacsBillState", ref strSql) == -1) return -1;
            strSql = System.String.Format(strSql, pacsbill.ComboNO, this.Operator.ID);

            if (strSql == null)
            {
                this.Err = "��ʽ��Sql���ʱ����";
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ɾ��һ����¼
        /// </summary>
        /// <param name="PacsId"></param>
        /// <returns></returns>
        public int DeletePacsBill(string PacsId)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Management.Order.deletePacsBill", ref strSql) == -1)
                return -1;
            strSql = string.Format(strSql, PacsId);
            if (strSql == null)
                return -1;
            return this.ExecNoQuery(strSql);
        }
      
        public int DeletePacsApply(string interfaceCode)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Management.Order.deletePacsBill.1", ref strSql) == -1)
                return -1;
            strSql = string.Format(strSql, interfaceCode);
            if (strSql == null)
                return -1;
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ��ü�鵥��Ϣ
        /// </summary>
        /// <param name="pacsbill"></param>
        /// <returns></returns>
        public string getPacsBillInfo(string strSql, FS.HISFC.Models.Order.PacsBill pacsbill)
        {
            # region "�ӿ�˵��"
            // 0 ��鵥��       1 ��鵥����      2 סԺ��ˮ�� 3 ���ұ��� 
            // 4 ��������       5 ��鲿λ/Ŀ��   6 ��ʷ������
            // 7 ʵ���Ҽ���� 8 ע������        9 ��� 10 ��ע
            // 11 ����Ա        12 ��������
            # endregion
            int patientType = 0;
            int his_PatientType = 1;
            if (pacsbill.PatientType == FS.HISFC.Models.Order.PatientType.OutPatient)
            {
                patientType = 1;
                his_PatientType = 1;
            }
            else if (pacsbill.PatientType == FS.HISFC.Models.Order.PatientType.InPatient)
            {
                his_PatientType = 2;
            }
            try
            {
                System.Object[] s = {pacsbill.ComboNO,//��鵥��
										pacsbill.BillName,//��鵥����
										pacsbill.PatientNO,//סԺ��ˮ��
										pacsbill.Dept.ID,//���Ҵ���
										pacsbill.Dept.Name,//��������
										pacsbill.CheckOrder,//��鲿λ/Ŀ��
										pacsbill.IllHistory,//��ʷ��鼰����
										pacsbill.LisResult,//�����
										pacsbill.Caution,//ע������
										pacsbill.Diagnose1,//���1
										pacsbill.Memo,//��ע
										pacsbill.Oper.ID,//����Ա
										pacsbill.ApplyDate,//��������
					                    pacsbill.Doctor.ID,//����ҽʦ����
					                    pacsbill.Doctor.Name,//����ҽʦ����
					                    pacsbill.TotCost.ToString(),//��Ŀ���
					                    //pacsbill.PatientNo.Length>=7?pacsbill.PatientNo.Substring(pacsbill.PatientNo.Length - 7):pacsbill.PatientNo,//�ӿ�
										pacsbill.User01,
					                    patientType.ToString(),//�������
					                    pacsbill.Diagnose2,//���2
					                    pacsbill.Diagnose3,//���3
					                    pacsbill.MachineType,//�豸����
					                    pacsbill.CheckBody,//��鲿λ
					                    his_PatientType.ToString(),
					                    pacsbill.ItemCode,
										pacsbill.ExeDept,
					                    pacsbill.ClinicCode,
										pacsbill.PacsItem,
										pacsbill.SampleDate,
										pacsbill.LastMensesDate,
										pacsbill.IsMenopause == true?"1":"0",
										pacsbill.Exec_sqn,//ִ�е���ˮ��
					                    pacsbill.Antiviotic1,//������1
					                    pacsbill.Antiviotic2,//������2
					                    pacsbill.Temperature,//����
					                    pacsbill.SpecimenType//��������
									};
                string myErr = "";
                if (FS.FrameWork.Public.String.CheckObject(out myErr, s) == -1)
                {
                    this.Err = myErr;
                    this.WriteErr();
                    return null;
                }
                strSql = string.Format(strSql, s);
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����" + ex.Message;
                this.WriteErr();
                return null;
            }
            return strSql;
        }
        /// <summary>
        /// ��ü�鵥��Ϣʵ��
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public ArrayList myPacsBillQuery(string strSql)
        {
            ArrayList al = new ArrayList();

            if (this.ExecQuery(strSql) == -1) return null;
            FS.HISFC.Models.Order.PacsBill pacsbill;// = new FS.HISFC.Object.Order.PacsBill();
            try
            {
                while (this.Reader.Read())
                {
                    try
                    {
                        pacsbill = new FS.HISFC.Models.Order.PacsBill();
                        pacsbill.ComboNO = this.Reader[0].ToString();//��鵥��
                        pacsbill.BillName = this.Reader[1].ToString();//��鵥����
                        pacsbill.PatientNO = this.Reader[2].ToString();//סԺ��ˮ��
                        pacsbill.Dept.Name = this.Reader[3].ToString();//��������
                        pacsbill.CheckOrder = this.Reader[4].ToString();//��鲿λ/Ŀ��
                        pacsbill.IllHistory = this.Reader[5].ToString();//��ʷ��鼰����
                        pacsbill.LisResult = this.Reader[6].ToString();//�����
                        pacsbill.Caution = this.Reader[7].ToString();//ע������
                        pacsbill.Diagnose1 = this.Reader[8].ToString();//���1
                        pacsbill.Memo = this.Reader[9].ToString();//��ע
                        pacsbill.Doctor.Name = this.Reader[10].ToString();//����Ա
                        pacsbill.ApplyDate = this.Reader[11].ToString();//��������
                        pacsbill.User02 = this.Reader[12].ToString();//�Ƿ�ȷ��
                        pacsbill.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[13].ToString());//��Ч״̬
                        pacsbill.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[14].ToString());//���
                        pacsbill.Diagnose2 = this.Reader[15].ToString();//���2
                        pacsbill.Diagnose3 = this.Reader[16].ToString();//���3
                        pacsbill.MachineType = this.Reader[17].ToString();//�豸����
                        pacsbill.CheckBody = this.Reader[18].ToString();//��鲿λ
                        pacsbill.ItemCode = this.Reader[19].ToString();//��Ŀ����
                        pacsbill.Dept.ID = this.Reader[20].ToString();//���Ҵ���
                        pacsbill.ExeDept = this.Reader[21].ToString();//ִ�п���
                        pacsbill.ClinicCode = this.Reader[22].ToString();//��ˮ��
                        pacsbill.PacsItem = this.Reader[23].ToString();//ҽ����Ŀ
                        pacsbill.SampleDate = this.Reader[24].ToString();
                        pacsbill.LastMensesDate = this.Reader[25].ToString();
                        pacsbill.IsMenopause = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[26].ToString());
                        pacsbill.Exec_sqn = this.Reader[27].ToString();//ִ�е���ˮ��
                        pacsbill.Antiviotic1 = this.Reader[28].ToString();//������1
                        pacsbill.Antiviotic2 = this.Reader[29].ToString();//������2
                        pacsbill.Temperature = this.Reader[30].ToString();//����
                        pacsbill.SpecimenType = this.Reader[31].ToString();//��������
                    }
                    catch (Exception ex)
                    {
                        this.Err = "��ü�鵥��Ϣ����" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    al.Add(pacsbill);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��ü�鵥��Ϣ����" + ex.Message;
                this.WriteErr();
                return null;
            }
            this.Reader.Close();
            return al;
        }



	}
}
