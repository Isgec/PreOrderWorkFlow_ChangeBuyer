<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="ProjectInProcess.aspx.cs" Inherits="PreOrderWorkflow_ChangeBuyer.ProjectInProcess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .auto-style1 {
            display: block;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            margin-left: 43px;
        }
        .auto-style2 {
            position: relative;
            min-height: 1px;
            float: left;
            width: 8%;
            left: -9px;
            top: 0px;
            height: 28px;
            padding-left: 15px;
            padding-right: 15px;
        }
        .auto-style3 {
            font-weight: bold;
            font-size: 16px;
        }
        </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div style="text-align:center;" class="auto-style3">Change Buyer/Project Manager</div>
    <div class="container">
        <br />
            <div class="row mt">
           
                    <label class="auto-style2" style="margin-left:0px">Project</label>
                  
                                <asp:DropDownList runat="server" ID="ddlProject" CssClass="auto-style1"   OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" AutoPostBack="true" style="margin-right:0px; " Width="350px"/>
                      
                            <%--<div class="col-sm-6">
                                <asp:Label runat="server" ID="lblItem"></asp:Label></div>--%>
                
            </div>
        <div class="row">
            <div class="col-lg-12" style="text-align: end">
                <asp:Button ID="btn_refresh" runat="server" Text="Fetch All Enquiry" class="btn btn-default btn-sm" OnClick="btn_refresh_Click" />

            </div>
        </div>
        <div class="col-lg-12" style="background-color: #fff; min-height: 600px; font-size: 11px;margin-top:20px">
            <asp:GridView runat="server" ID="gvData" AutoGenerateColumns="false" Width="95%" CssClass="table table-bordered table-hover" HeaderStyle-BackColor="#e9e9e9" OnPageIndexChanging="gvData_PageIndexChanging" AllowPaging="True" >
                <Columns>
                    <asp:TemplateField HeaderText="WFID">
                         <HeaderTemplate>
                            <asp:Label runat="server" ID="lbl_headerwfid" Text="WFID"></asp:Label>
                            <asp:TextBox runat="server" ID="txt_searchwfid" Width="42px" AutoPostBack="true" Value=' <%#Eval("WFID") %>' OnTextChanged="txt_wfidonTextChange"></asp:TextBox>
                        </HeaderTemplate>
                        <ItemTemplate>
                              <%#Eval("WFID") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Project">
                         <HeaderTemplate>
                            <asp:Label runat="server" ID="lbl_headerproj" Text="Project"></asp:Label>
                            <asp:TextBox runat="server" ID="txt_searchproj" Width="80px" AutoPostBack="true" Value=' <%#Eval("WFID") %>' OnTextChanged="txt_wfidonTextChange"></asp:TextBox>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%#Eval("Project") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Element">
                        <ItemTemplate>
                            <%#Eval("Element") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Specification No">
                        <ItemTemplate>
                            <%#Eval("SpecificationNo") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PMDL Doc No">
                        <ItemTemplate>
                            <%#Eval("PMDLDocNo") %>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Buyer">
                        <ItemTemplate>
                            <%#Eval("BuyerName") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Project Manager">
                        <ItemTemplate>
                            <%#Eval("ManagerName") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                <%--    <asp:TemplateField HeaderText="Supplier">
                        <ItemTemplate>
                      <%#Eval("SupplierName") %>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="WF Status">
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lbl_headerstatus" Text="WF Status"></asp:Label>

                            <asp:DropDownList ID="ddl_srchstatus" Width="90px" runat="server" AutoPostBack="true" OnTextChanged="txt_wfidonTextChange">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="Enquiry in progress">Enquiry in progress</asp:ListItem>
                                <asp:ListItem Value="Technical Specification Released">Technical Specification Released</asp:ListItem>
                                <asp:ListItem Value="All Offer Received">All Offer Received</asp:ListItem>
                            </asp:DropDownList>
                        </HeaderTemplate>
                         <ItemTemplate>
                            <%#Eval("WF_Status") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="User">
                        <ItemTemplate>
                            <%#Eval("EmployeeName") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="DateTime">
                        <HeaderTemplate>
                             <asp:Label runat="server" ID="lbl_headerDate" Text="DateTime"></asp:Label>
                            <asp:TextBox ID="txtDate" runat="server" CssClass="txtDateCss" AutoPostBack="true" AutoComplete="off" OnTextChanged="txt_wfidonTextChange"></asp:TextBox>
                            <ajax:calendarextender id="txtDateCalendarExtender" runat="server" targetcontrolid="txtDate"
                                popupposition="BottomRight" format="dd/MM/yyyy">
                            </ajax:calendarextender>
                        </HeaderTemplate>
                         <ItemTemplate>
                            <%#Convert.ToDateTime(Eval("DateTime")).ToString("dd-MM-yyyy") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Change Buyer/PM" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton CssClass="btn-link" runat="server" ID="btnChangeBuyer" OnClick="btnChangeBuyer_Click" CommandArgument='<%#Eval("WFID") %>' Text='<i class="fa fa-mail-forward" style="font-size:16px"></i>' />
                        </ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <%--  <asp:TemplateField HeaderText="View Enquiry" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton CssClass="" runat="server" ID="btnView" OnClick="btnView_Click" CommandArgument='<%#Eval("WFID") %>' Text='<i class="fa fa-address-book" aria-hidden="true" style="font-size:16px"></i>'/>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <%--<asp:TemplateField HeaderText="Return" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton Font-Size="11" runat="server" ID="btnReturn" OnClick="btnReturn_Click" CommandArgument='<%#Eval("WFID") %>'  Text='<i class="fa fa-backward" aria-hidden="true" style="font-size:16px"></i>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                       <%--<asp:TemplateField HeaderText="Commercial offer Finalized" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton Font-Size="11" runat="server" ID="btnClosed" OnClick="btnClosed_Click" CommandArgument='<%#Eval("WFID") %>'  ForeColor="Red" Text='<i class="fa fa-unlock-alt" aria-hidden="true" style="font-size:16px"></i>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>

<HeaderStyle BackColor="#E9E9E9"></HeaderStyle>
                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="20" />

            </asp:GridView>
        </div>
    </div>
    
</asp:Content>
