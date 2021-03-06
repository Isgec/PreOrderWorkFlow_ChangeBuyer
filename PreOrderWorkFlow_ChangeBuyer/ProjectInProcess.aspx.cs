﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PreOrderWorkflow_ChangeBuyer
{
    public partial class ProjectInProcess : System.Web.UI.Page
    {
        WorkFlow objWorkFlow;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["u"] != null)
                {
                    GetProject();
                    GetData();
                }
                else
                {

                }
            }
        }
        private void GetProject()
        {
            WorkFlow objWorkFlow = new WorkFlow();
            DataTable dt = objWorkFlow.GetProject();
            ddlProject.DataSource = dt;
            ddlProject.DataValueField = "Project";
            ddlProject.DataTextField = "Project";
            ddlProject.DataBind();
            ddlProject.Items.Insert(0, "Select Project");
        }
        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            objWorkFlow = new WorkFlow();
            objWorkFlow.Project = ddlProject.SelectedValue;
           
                objWorkFlow.WF_Status = "Commercial offer Finalized";
                objWorkFlow.UserId = Request.QueryString["u"];
            if (objWorkFlow.Project != "Select Project")
            {
                DataTable dt = objWorkFlow.GetWFBY_Status_Project();
                gvData.DataSource = dt;
                gvData.DataBind();
            }
            else
            {
                GetData();
            }

            // }
        }
        private void GetData()
        {
            objWorkFlow = new WorkFlow();
            objWorkFlow.WF_Status = "Commercial offer Finalized";
            objWorkFlow.UserId = Request.QueryString["u"];
            DataTable dt = objWorkFlow.GetWFBY_Status();
            gvData.DataSource = dt;
            gvData.DataBind();
           
        }
        protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvData.PageIndex = e.NewPageIndex;
            GetData();
        }
        protected void btnChangeBuyer_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            objWorkFlow = new WorkFlow();
            objWorkFlow.WFID = Convert.ToInt32(btn.CommandArgument);
            Response.Redirect("ChangeBuyer.aspx?u=" + Request.QueryString["u"]+"&WFID="+btn.CommandArgument);
           
        }
        }
     
    }
