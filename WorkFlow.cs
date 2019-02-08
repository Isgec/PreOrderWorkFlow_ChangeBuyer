using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PreOrderWorkflow_ChangeBuyer
{
    public class WorkFlow
    {
        public static string Con = ConfigurationManager.AppSettings["Connection"];
        public static string Con129 = ConfigurationManager.AppSettings["ConnectionLive"];

        #region Properties
        public int WFID { get; set; }
        public int WFIDHistoryId { get; set; }
        public int Parent_WFID { get; set; }
        public int SLNO_WFID { get; set; }
        public string Project { get; set; }
        public string Element { get; set; }
        public string SpecificationNo { get; set; }
        public string Buyer { get; set; }
        public string Manager { get; set; }
        public string WF_Status { get; set; }
        public string UserId { get; set; }
        public string Supplier { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }

        public string EmailSubject { get; set; }
        public string Notes { get; set; }

        public string AttachmentId { get; set; }
        public string AthhFile { get; set; }
        public string IndexValue { get; set; }
        public string AttachmentHandle { get; set; }
        public string NotesHandle { get; set; }
        public string PurposeCode { get; set; }
        public string FileName { get; set; }
        public string LibraryCode { get; set; }
        public string AttachedBy { get; set; }
        public string RandomNo { get; set; }
        public string PMDLdocDesc { get; set; }
        public string SearchWFID { get; set; }
        public string SearchWFProject { get; set; }
        public string SearchWFStatus { get; set; }
        public string SearchWFDate { get; set; }
        #endregion

        #region Methods
        public DataTable GetUser(string UserID)
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "select CardNo,EmployeeName  from HRM_Employees where EmployeeName Like '" + UserID + "%' or CardNo Like '" + UserID + "%'").Tables[0];
        }
        public int UpdateBuyer_Manager()
        {
            return SqlHelper.ExecuteNonQuery(Con, CommandType.Text, "Update WF1_PreOrder set [Buyer]='" + Buyer + "',[Manager]='" + Manager + "',DateTime=getDate()  Where [WFID]='" + WFID + "'");
        }
        public DataTable GetHRM_Employees()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "select CardNo,EmployeeName from HRM_Employees order by EmployeeName asc").Tables[0];
        }
        public DataTable GetUserName(string UserCode)
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, "select CardNo,EmployeeName  from HRM_Employees where CardNo= '" + UserCode + "'").Tables[0];
        }
        public DataTable GetWFById()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"select [WFID]
                                      ,[Parent_WFID]
                                      ,[Project]
                                      ,[Element]
                                      ,[SpecificationNo]
                                      ,[PMDLDocNo]
                                      ,(Select EmployeeName FROM HRM_Employees where CardNo=Buyer) as BuyerName
                                      ,[Buyer]
                                      ,(Select EmployeeName FROM HRM_Employees where CardNo=Manager) as ManagerName
                                      ,[Manager]
                                      ,[WF_Status]
                                      ,[UserId]
                                      ,[DateTime]
                                      ,[Supplier]
                                      ,[SupplierName]
                                      ,[SupplierCode]
                                      ,[EmailSubject]
                                      ,EmployeeName,RandomNo
                                       from [WF1_PreOrder] WF
                                INNER JOIN HRM_Employees E on E.CardNo=WF.UserId
                                WHERE WFID='" + WFID + "'").Tables[0]; //
        }
        public DataTable InserPreOrderHistory()
        {
            List<SqlParameter> sqlParamater = new List<SqlParameter>();
            sqlParamater.Add(new SqlParameter("@WFID", WFID));
            sqlParamater.Add(new SqlParameter("@Parent_WFID", Parent_WFID));
            sqlParamater.Add(new SqlParameter("@Project", Project));
            sqlParamater.Add(new SqlParameter("@Element", Element));
            sqlParamater.Add(new SqlParameter("@SpecificationNo", SpecificationNo));
            sqlParamater.Add(new SqlParameter("@PMDLDocNo", PMDLdocDesc));
            sqlParamater.Add(new SqlParameter("@Buyer", Buyer));
            sqlParamater.Add(new SqlParameter("@Manager", Manager));
            sqlParamater.Add(new SqlParameter("@WF_Status", WF_Status));
            sqlParamater.Add(new SqlParameter("@UserId", UserId));
            sqlParamater.Add(new SqlParameter("@Supplier", Supplier));
            sqlParamater.Add(new SqlParameter("@SupplierName", SupplierName));
            sqlParamater.Add(new SqlParameter("@Notes", Notes));
            // return SqlHelper.ExecuteDataset(Con, CommandType.StoredProcedure, "sp_Insert_PreOrder_Workflow_History", sqlParamater.ToArray()).Tables[0];
            return SqlHelper.ExecuteDataset(Con, CommandType.StoredProcedure, "sp_Insert_PreOrder_Workflow_History_Test", sqlParamater.ToArray()).Tables[0];
        }
        public DataTable GetHistory()
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"SELECT [WF_HistoryID]
                                      ,[WFID]
                                      ,[WFID_SlNo]
                                      ,[Parent_WFID]
                                      ,[Project]
                                      ,[Element]
                                      ,[SpecificationNo]
                                      ,[PMDLDocNo]
                                      ,[Buyer]
                                      ,(Select EmployeeName FROM HRM_Employees where CardNo=Buyer) as BuyerName
                                      ,[Manager]
                                      ,(Select EmployeeName FROM HRM_Employees where CardNo=Manager) as ManagerName
                                      ,[WF_Status]
                                      ,[UserId]
                                      ,[DateTime]
                                      ,[Supplier]
                                      ,[SupplierName]
                                      ,[Notes]
                                  FROM [WF1_PreOrder_History] WF
                                  INNER JOIN HRM_Employees E on E.CardNo=WF.UserId
                                WHERE  WFID='" + WFID + "'").Tables[0]; //
        }

        public DataTable GetWFBY_Status()
        {
            string WFbyStatus = @"select [WFID],[Parent_WFID],[Project] ,[Element],[SpecificationNo]
              ,[PMDLDocNo],(Select EmployeeName FROM HRM_Employees where CardNo=Buyer) as BuyerName
              ,[Buyer],(Select EmployeeName FROM HRM_Employees where CardNo=Manager) as ManagerName
              ,[Manager],[WF_Status] ,[UserId],[DateTime],[Supplier],[SupplierName] ,EmployeeName
                from [WF1_PreOrder] WF INNER JOIN HRM_Employees E on E.CardNo=WF.UserId
                WHERE Wf_Status not in('" + WF_Status + "')  order by WFID desc";


            return SqlHelper.ExecuteDataset(Con, CommandType.Text, WFbyStatus).Tables[0];

        }
        public DataTable GetWFBY_HeaderFilter()
        {
            //string WFbyStatus = @"select [WFID],[Parent_WFID],[Project] ,[Element],[SpecificationNo]
            //  ,[PMDLDocNo],(Select EmployeeName FROM HRM_Employees where CardNo=Buyer) as BuyerName
            //  ,[Buyer],(Select EmployeeName FROM HRM_Employees where CardNo=Manager) as ManagerName
            //  ,[Manager],[WF_Status] ,[UserId],[DateTime],[Supplier],[SupplierName] ,EmployeeName
            //    from [WF1_PreOrder] WF INNER JOIN HRM_Employees E on E.CardNo=WF.UserId
            //    WHERE Wf_Status in('" + WF_Status + "')  and WF.Buyer='" + UserId + "' order by WFID desc";

            string Qryemp = "  select [WFID],[Parent_WFID],[Project] ,[Element],[SpecificationNo] ";
            Qryemp += " ,[PMDLDocNo],(Select EmployeeName FROM HRM_Employees where CardNo=Buyer) as BuyerName ";
            Qryemp += " ,[Buyer],(Select EmployeeName FROM HRM_Employees where CardNo=Manager) as ManagerName ";
            Qryemp += " ,[Manager],[WF_Status] ,[UserId],[DateTime],[Supplier],[SupplierName] ,EmployeeName ";
            Qryemp += " from [WF1_PreOrder] WF INNER JOIN HRM_Employees E on E.CardNo=WF.UserId where ";
            if (SearchWFID != "")
            {
                Qryemp += " Convert(varchar(10), WFID) like '%" + SearchWFID + "%' and";
            }
            if (SearchWFProject != "")
            {
                Qryemp += " Project like '%" + SearchWFProject + "%' and";
            }
            if (SearchWFDate != "")
            {
                Qryemp += " Convert(varchar(10), DateTime,103) like '%" + SearchWFDate + "%' and";
            }
            Qryemp += " Wf_Status in('" + WF_Status + "')  and WF.Buyer='" + UserId + "'";
            Qryemp += " Wf_Status not in('" + WF_Status + "')  order by WFID desc";
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, Qryemp).Tables[0];

            //return SqlHelper.ExecuteDataset(Con, CommandType.Text, @"select [WFID]
            //                          ,[Parent_WFID]
            //                          ,[Project]
            //                          ,[Element]
            //                          ,[SpecificationNo]
            //                          ,[PMDLDocNo]
            //                          ,(Select EmployeeName FROM HRM_Employees where CardNo=Buyer) as BuyerName
            //                          ,[Buyer]
            //                          ,[WF_Status]
            //                          ,[UserId]
            //                          ,[DateTime]
            //                          ,[Supplier]
            //                          ,[SupplierName]
            //                          ,EmployeeName
            //                           from [WF1_PreOrder] WF
            //                    INNER JOIN HRM_Employees E on E.CardNo=WF.UserId
            //                    WHERE Wf_Status in(" + WF_Status + ")  and WF.Buyer='" + UserId + "' order by WFID desc").Tables[0]; //
        }
        public DataTable GetWFBY_Status_Project()
        {
            string WFbyStatus = @"select [WFID],[Parent_WFID],[Project] ,[Element],[SpecificationNo]
              ,[PMDLDocNo],(Select EmployeeName FROM HRM_Employees where CardNo=Buyer) as BuyerName
              ,[Buyer],(Select EmployeeName FROM HRM_Employees where CardNo=Manager) as ManagerName
              ,[Manager],[WF_Status] ,[UserId],[DateTime],[Supplier],[SupplierName] ,EmployeeName
                from [WF1_PreOrder] WF INNER JOIN HRM_Employees E on E.CardNo=WF.UserId
                WHERE Wf_Status not in('" + WF_Status + "') and WF.Project='" + Project + "'order by WFID desc";


            return SqlHelper.ExecuteDataset(Con, CommandType.Text, WFbyStatus).Tables[0];

        }
        public DataTable GetProject()
        {
               string sGetProject = @"select distinct Project from [WF1_PreOrder] order by Project";
               return SqlHelper.ExecuteDataset(Con, CommandType.Text, sGetProject).Tables[0];
        }
        #endregion
    }
}