<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="OtherInvoicesCategory.aspx.cs" Inherits="advtech.Finance.Accounta.OtherInvoicesCategory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Invoice Category</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
           <div class="col">
               <div class="card shadow-none">
             <div class="row">
                 
                <div class="col-xl-5 col-md-6 border-right">

                <div class="card-header bg-white  font-weight-bold">
                                               <a class="btn btn-light btn-circle mr-2" id="buttonback" runat="server" href="OtherInvoices.aspx" data-toggle="tooltip" data-placement="bottom" title="Back to Invoice">
             
                    <span class="fa fa-arrow-left text-danger"></span>
  
                </a>
          <span class="text-gray-800 mx-2">Add Invoice Type</span>
                </div>
                <div class="card-body">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    <div class="form-group mb-2">
             <label class=font-weight-bold>Invoice Name<span class=text-danger>*</span></label>
            <div class="input-group input-group-alternative">
            <asp:TextBox ID="txtName" class="form-control " runat="server"></asp:TextBox>
              <div class="input-group-prepend">
                <span class="input-group-text"></span>
              </div>
              
            </div>
          </div> 

                         <div class="form-group mb-2">
             <label class=font-weight-bold>Rate<span class=text-danger>*</span></label>
            <div class="input-group input-group-alternative">
            <asp:TextBox ID="txtRate" class="form-control " value="1" runat="server"></asp:TextBox>
              <div class="input-group-prepend">
                <span class="input-group-text"></span>
              </div>
              
            </div>
          </div> 
                                        <div class="form-group mb-2">
             <label class=font-weight-bold>Unit<span class=text-danger>*</span></label>
            <div class="input-group input-group-alternative">
            <asp:TextBox ID="txtUnit" class="form-control " value="-" runat="server"></asp:TextBox>
              <div class="input-group-prepend">
                <span class="input-group-text"></span>
              </div>
              
            </div>
          </div> 


                                                            <div class="form-group mb-2">
             <label class=font-weight-bold>Income Account<span class=text-danger>*</span></label>
            <div class="input-group input-group-alternative">
                <asp:DropDownList ID="ddlIncomeAccount" class="form-control " runat="server">

                </asp:DropDownList>
     
              
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
                <div class="card-header bg-white">
                    <div class="row">
                        <div class="col-6 text-left">
        <span class="fas fa-hashtag mr-2" style="color:#ff00bb"></span><span class="m-0 font-weight-bold h5 text-gray-900">Category</span>

                        </div>
                        <div class="col-6 text-right">
                                                                         <button type="button"  runat=server id="Button4" class="mx-1 btn btn-sm btn-danger btn-circle" data-toggle="modal" data-target="#exampleModalUpdatePrice" >
                    <div data-toggle="tooltip" title="Update Rate" data-placement="top">
                      <i class="fas fa-cogs text-white font-weight-bold"></i>
                   <span></span>
                    </div>
                  </button>
                        </div>
                    </div>
                  
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
                                <th scope="col">Income Name</th>
                                
                                <th scope="col">Rate</th>
                                    <th scope="col">Unit</th>

                                  <th scope="col">Ledger Account</th>
                              </tr>
                            </thead>
                            <tbody>
                             </HeaderTemplate>
                            <ItemTemplate>
                              <tr>
                                <td class="text-left text-primary">
                                <%#Eval("incomename")%>
                                </td>
                                 <td>
                                <%# Eval("rate")%>
                                </td>
                                 <td class="text-cneter">
                                <%# Eval("unit")%>
                                </td>

                                 <td class="text-cneter">
                                <%# Eval("incomeaccount")%>
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
            <div class="modal fade" id="exampleModalUpdatePrice" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel8shopM" aria-hidden="true">
  <div class="modal-dialog modal-sm" role="document">
    <div class="modal-content">
      <div class="modal-header">
       <span class="fas fa-plus-circle mr-2"  style="color:#ff00bb"></span> <span class="h6 modal-title mb-1 text-gray-900 font-weight-bolder" id="exampleModalLabel8shopM">Update Rate</span>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
                    <div class="row mb-3">
    <div class="col-md-12">
        <label class="small" style="color:#ff00bb">Select Income type</label>
<asp:DropDownList ID="ddlExpense" class="form-control form-control-sm" runat="server"></asp:DropDownList>
    </div>
              </div>
        <div class="row mb-3">
            <div class="col-md-12">
                                <span class="fas fa-arrow-circle-right mb-2" style="color:#ff00bb"></span><span class="small text-danger mb-2 ml-2">If you don't want to use unit based payment put the rate to "1" and unit to "-"</span>

                <asp:TextBox ID="txtUpdatePrice" class="form-control mt-3 form-control-sm" style="border-color:#ff00bb" placeholder="New price" runat="server"></asp:TextBox>
            </div>
            </div>
                  <div class="row mb-3">
            <div class="col-md-12">
                <asp:TextBox ID="txtUnitUpdate" class="form-control form-control-sm" style="border-color:#ff00bb" placeholder="New Unit" runat="server"></asp:TextBox>
            </div>
            </div>

        
                    <div class="row">                        </div>
            <div class="col-md-12">

                <center>
                                                  <button class="btn btn-primary btn-sm w-100" type="button" disabled id="Pbutton35" style="display:none">
  <span class="spinner-grow spinner-grow-sm" role="status" aria-hidden="true"></span>
  Assigning...
</button>
<asp:Button ID="btnUpdatePrice" runat="server" class="btn btn-sm btn-primary w-100" Text="Update price" OnClick="btnUpdatePrice_Click" />
                </center>
             

            </div>
        </div>
                <center>
      <div class="modal-footer">
       

       
       
      </div>

        </center>
      </div>

    </div>
  </div>
        </div>
 </div>
</asp:Content>
