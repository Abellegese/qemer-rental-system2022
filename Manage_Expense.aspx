<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="Manage_Expense.aspx.cs" Inherits="advtech.Finance.Accounta.Manage_Expense" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
           <div class="col">
               <div class="card shadow-none">
             <div class="row">
                 
                <div class="col-xl-5 col-md-6 border-right">

                <div class="card-header bg-white  font-weight-bold">
              <img src="../../asset/plus-circle-dotted.svg" />
          <span class="text-gray-800 mx-2">Add Expense</span>
                </div>
                <div class="card-body">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    <div class="form-group mb-2">
             <label class=font-weight-bold>Expense Name<span class=text-danger>*</span></label>
            <div class="input-group input-group-alternative">
            <asp:TextBox ID="txtName" class="form-control " runat="server"></asp:TextBox>
              <div class="input-group-prepend">
                <span class="input-group-text"></span>
              </div>
              
            </div>
          </div> 
     


                                                            <div class="form-group mb-2">
             <label class=font-weight-bold>Expense Account<span class=text-danger>*</span></label>
            <div class="input-group input-group-alternative">
                <asp:DropDownList ID="ddlExpenseAccount" class="form-control " runat="server">

                </asp:DropDownList>
     
              
            </div>
          </div>


                    <div class="form-group mb-2">
             <label class=font-weight-bold>Notes<span class=text-danger>*</span></label>
            <div class="input-group input-group-alternative">
            <asp:TextBox ID="txtNotes" class="form-control " runat="server"></asp:TextBox>
              <div class="input-group-prepend">
                <span class="input-group-text"></span>
              </div>
              
            </div>
          </div> 

     <hr />
        <div class="form-group mb-2">


    <asp:Button ID="Button3" class="btn btn-sm btn-success"   runat="server" 
    Text="Save" OnClick="Button3_Click" />
          </div> 


                </div>
           
                  </div>
                 <div class="col-xl-7 col-md-6">
      
                <!-- Card Header - Dropdown -->
                <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
                  <h6 class="m-0 font-weight-bold text-gray-800">Expense Types</h6>
                  
                </div>
                <!-- Card Body -->
          <div class=card-body>
             <div class="row">

            <!-- Earnings (Monthly) Card Example -->
            <div class="col-md-12 mb-1">

               <asp:Repeater ID="Repeater1" runat="server" >
                         
                            <HeaderTemplate>
                        <div class="table-responsive">
                          <table class="table table-sm table-bordered ">
                            <thead>
                              <tr>
                                <th scope="col">Expense Name</th>
                                <th scope="col">Ledger Account</th>
                                <th scope="col">Notes</th>

                              </tr>
                            </thead>
                            <tbody>
                             </HeaderTemplate>
                            <ItemTemplate>
                              <tr>


                                <td class="text-left text-primary">
                                <%#Eval("name")%>
                                </td>
                                 <td>
                                <%# Eval("account")%>
                                </td>
                                 <td class="text-cneter">
                                <%# Eval("Notes")%>
                                </td>
                              </tr>
                              </ItemTemplate>
                            <FooterTemplate>
                            </tbody>
                          </table>
                             </FooterTemplate>
                                                     
                        </asp:Repeater>
              
            </div>
             
            <!-- Earnings (Monthly) Card Example -->

          </div>
               </div>           

            
                 </div>
          </div>
               </div>

        </div>
        </div>
 </div>
</asp:Content>
