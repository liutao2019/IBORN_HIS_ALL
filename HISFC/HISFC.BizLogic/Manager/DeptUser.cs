using System;
using System.Collections;
namespace FS.HISFC.BizLogic.Manager
{
	/// <summary>
	/// DeptUser ��ժҪ˵����
	/// </summary>
    //public class DeptUser:DataBase
    //{
    //    public DeptUser()
    //    {
    //        //
    //        // TODO: �ڴ˴���ӹ��캯���߼�
    //        //
    //    }
    //    /// <summary>
    //    /// ��ȡ���пƳ��� 
    //    /// </summary>
    //    /// <returns></returns>
    //    public ArrayList GetDeptUserAll()
    //    {
    //        string strSql = "";
    //        ArrayList al = new ArrayList();
    //        if (this.GetSQL("Manager.DeptUser.GetDeptUserAll",ref strSql)==-1)return null;
    //        this.ExecQuery(strSql);
    //        //0���Ʋ�������1���Ʋ�������2���Ʋ���ֵ3��ʾ���
    //        while (this.Reader.Read())
    //        {
    //            FS.HISFC.Models.PhysicalExam.DeptUse Control = new FS.HISFC.Models.PhysicalExam.DeptUse();
    //            try
    //            {
    //                //					Control.ID = this.Reader[0].ToString();
    //                //					Control.Name= this.Reader[1].ToString();
    //                //					Control.ControlValue=this.Reader[3].ToString();
    //                //					Control.VisibleFlag=this.Reader[4].ToString();
    //            }
    //            catch(Exception ex)
    //            {
    //                this.Err="��ѯ������Ϣ��ֵ����!"+ex.Message;
    //                this.ErrCode=ex.Message;
    //                return null;
    //            }
				
    //            al.Add(Control);

    //        }
    //        this.Reader.Close();

    //        return al;
    //    }
    //    /// <summary>
    //    /// ��ȡ���еĿƳ��� 
    //    /// </summary>
    //    /// <param name="deptCode"></param>
    //    /// <returns></returns>
    //    public ArrayList  GetDeptUse(string deptCode)
    //    {
    //        string strSql = "";
    //        if (this.GetSQL("Manager.DeptUser.GetDeptUse",ref strSql)==-1) return null;
    //        strSql = string.Format(strSql,deptCode);
    //        try
    //        {				
    //            return myGetDeptUse(strSql);
    //        }
    //        catch(Exception ee)
    //        {
    //            this.Err = "Manager.DeptUser.GetDeptUse"+ee.Message;
    //            this.ErrCode=ee.Message;
    //            WriteErr();
    //            return null;
    //        }
    //    }
    //    /// <summary>
    //    /// ˽�г�Ա ��ȡ�Ƴ���
    //    /// </summary>
    //    /// <param name="strSql"></param>
    //    /// <returns></returns>
    //    private ArrayList myGetDeptUse(string strSql)
    //    {
    //        try
    //        {
    //            ArrayList list = new  ArrayList();
    //            FS.HISFC.Models.PhysicalExam.DeptUse info = null;
    //            this.ExecQuery(strSql);
    //            while(this.Reader.Read())
    //            {
    //                info = new FS.HISFC.Models.PhysicalExam.DeptUse();
    //                info.item.Name = this.Reader[0].ToString(); //��Ŀ����
    //                info.ExecDeptInfo.Name = this.Reader[1].ToString(); //ִ�п���
    //                info.Memo = this.Reader[2].ToString(); //ע������
    //                info.item.ID = this.Reader[3].ToString();//��Ŀ����
    //                info.ExecDeptInfo.ID = this.Reader[4].ToString(); //ִ�п���
    //                info.DeptInfo.ID = this.Reader[5].ToString(); //���� 
    //                info.UnitFlag = this.Reader[6].ToString(); //��λ��ʶ
    //                info.item.SysClass.ID = this.Reader[7].ToString();//ϵͳ���
    //                list.Add(info);
    //            }
    //            return list;
    //        }
    //        catch(Exception ex)
    //        {
    //            this.Err  = ex.Message;
    //            return null;
    //        }
    //    }
    //    /// <summary>
    //    /// ��ȡĳ���Ƴ���
    //    /// </summary>
    //    /// <param name="ItemCode"></param>
    //    /// <returns></returns>
    //    public ArrayList GetDeptUser(string ItemCode)
    //    {
    //        ArrayList list = new  ArrayList();
    //        return list;
    //    }
    //    /// <summary>
    //    /// ��ȡ���еĿƳ��� 
    //    /// </summary>
    //    /// <param name="ds"></param>
    //    /// <param name="deptCode"></param>
    //    /// <returns></returns>
    //    public int GetDeptUse(ref System.Data.DataSet ds ,string deptCode)
    //    {
    //        string strSql = "";
    //        if (this.GetSQL("Manager.DeptUser.GetDeptUse",ref strSql)==-1) return -1;
    //        strSql = string.Format(strSql,deptCode);
    //        try
    //        {				
    //            if(this.ExecQuery(strSql)==-1)return -1;
    //            this.ExecQuery(strSql,ref ds);
    //            return 1;
    //        }
    //        catch(Exception ee)
    //        {
    //            this.Err = "Manager.DeptUser.GetDeptUse"+ee.Message;
    //            this.ErrCode=ee.Message;
    //            WriteErr();
    //            return -1;
    //        }
    //    }
    //    public int AddOrUpdateDeptUse(FS.HISFC.Models.PhysicalExamination.DeptUse item)
    //    {
    //        return 1;
    //    }
    //    /// <summary>
    //    /// ���ӿƳ���
    //    /// </summary>
    //    /// <param name="item"></param>
    //    /// <returns></returns>
    //    public int AddDeptUse(FS.HISFC.Models.PhysicalExamination.DeptUse item)
    //    {
    //        string sql = "";
    //        if(this.GetSQL("Manager.DeptUser.AddDeptUse",ref sql)== -1)
    //            return -1;
    //        try 
    //        {
    //            sql=string.Format(sql,ItemParam(item));
    //        }
    //        catch(Exception ex) 
    //        {
    //            this.ErrCode=ex.Message;
    //            this.Err="�ӿڴ���"+ex.Message;
    //            this.WriteErr();
    //            return -1;
    //        }

    //        if(this.ExecNoQuery(sql) == -1) return -1;


    //        return 1;
    //    }

    //    /// <summary>
    //    /// ��ȡ����
    //    /// </summary>
    //    /// <param name="item"></param>
    //    /// <returns></returns>
    //    private string[] ItemParam(FS.HISFC.Models.PhysicalExamination.DeptUse item)
    //    {
    //        string []str = new string[]{
    //                                       item.DeptInfo.ID,
    //                                       item.item.ID,
    //                                       item.item.SysClass.ID.ToString(),
    //                                       item.item.Name,
    //                                       item.ExecDeptInfo.ID,
    //                                       item.UnitFlag,
    //                                       item.Memo,
    //                                       this.Operator.ID
    //                                   };
    //        return str;
    //    }
    //    /// <summary>
    //    /// ���¿Ƴ���
    //    /// </summary>
    //    /// <param name="item"></param>
    //    /// <returns></returns>
    //    public int UpdateDeptUse(FS.HISFC.Models.PhysicalExamination.DeptUse item)
    //    {
    //        string sql = "";
    //        if(this.GetSQL("Manager.DeptUser.UpdateDeptUse",ref sql)== -1)
    //            return -1;
    //        try 
    //        {
    //            sql=string.Format(sql,ItemParam(item));
    //        }
    //        catch(Exception ex) 
    //        {
    //            this.ErrCode=ex.Message;
    //            this.Err="�ӿڴ���"+ex.Message;
    //            this.WriteErr();
    //            return -1;
    //        }

    //        if(this.ExecNoQuery(sql) == -1) return -1;


    //        return 1;
    //    }
    //    /// <summary>
    //    /// ɾ��һ���Ƴ���
    //    /// </summary>
    //    /// <param name="item"></param>
    //    ///  
    //    /// <returns></returns>
    //    public int DeleteDeptUse(FS.HISFC.Models.PhysicalExamination.DeptUse item)
    //    {
    //        string sql = "";
    //        if(this.GetSQL("Manager.DeptUser.DeleteDeptUse",ref sql)== -1)
    //            return -1;
    //        try 
    //        {
    //            sql=string.Format(sql,ItemParam(item));
    //        }
    //        catch(Exception ex) 
    //        {
    //            this.ErrCode=ex.Message;
    //            this.Err="�ӿڴ���"+ex.Message;
    //            this.WriteErr();
    //            return -1;
    //        }

    //        return this.ExecNoQuery(sql);
    //    }
    //}
}
