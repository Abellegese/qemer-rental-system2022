<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="creditnoteopeningpaymentbank.aspx.cs" Inherits="advtech.Finance.Accounta.creditnoteopeningpaymentbank" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid mt--7">
      <div class="row">
        <div class="col">
        <div id="div_print"> 
      
              <div class="card mb-4">
                <div class="card-header bg-white">
                <span id="item" runat="server" class="h6 text-center m-0 font-weight-bold text-primary">Pay via Bank</span>
               
                </div>
                <div class="card-body">
                      <div class="row">
                          <div class="col-md-1">

                          </div>
                          <div class="col-md-10">
<div class="card border-left-primary " >
                <div class="card-body">
                           <div class="row">
            <div class="col-md-3 col-sm-4">
                
          <div class="form-group mb-0">
             <label class=font-weight-bold>Amount<span class=text-danger>*</span></label>
            <div class="input-group input-group-alternative">
           <asp:TextBox ID="txtqtyhand" class="form-control" runat="server"></asp:TextBox>
              <div class="input-group-prepend">
<label class="input-group-text">ETB</label>
              </div>
              
            </div>
          </div>
                          
            </div>

<div class="col-md-3 col-sm-4">
                
          <div class="form-group mb-0">
             <label class=font-weight-bold>Bank Account<span class=text-danger>*</span></label>
            <div class="input-group input-group-alternative">
                <asp:DropDownList ID="DropDownList1" class="form-control" runat="server"></asp:DropDownList>

              
            </div>
          </div>
                          
            </div>
                                           <div class="col-md-3 col-sm-4">
                
          <div class="form-group mb-0">
             <label class=font-weight-bold>cheque or voucher no.<span class=text-danger>*</span></label>
            <div class="input-group input-group-alternative">
           <asp:TextBox ID="txtReference" class="form-control" runat="server"></asp:TextBox>

              
            </div>
          </div>
                          
            </div>
                               <div class="col-md-3 col-sm-4">

                        <div class="form-group mb-0">
             <label class=font-weight-bold><span class=text-danger></span></label>
            <div class="input-group input-group-alternative">
             <asp:Button ID="Button3" runat="server" class="btn mt-2 btn-secondary" Text="Save" OnClick="Button3_Click"/>
                </div>
                            </div>
                          
            </div>
        </div>                         
                </div>
                              <center>
             <span id="infoicon" visible="false" runat="server"  class="fas fa-info-circle"></span><asp:Label ID="lblMsg" runat="server" Text="Label" Visible="false"></asp:Label>
                 </center>
    
              </div>
                          </div>
                            <div class="col-md-1">

                          </div>

                  </div>
              </div>
              </div>
              </div>
            </div>
          </div>

                            <div class="row mt-5">

        </div>
</asp:Content>
