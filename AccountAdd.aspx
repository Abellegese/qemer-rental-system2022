<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.master" AutoEventWireup="true" CodeBehind="AccountAdd.aspx.cs" Inherits="advtech.Finance.Accounta.AccountAdd" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>Register Account</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">    


<div class="main-content">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<asp:UpdatePanel ID="upSetSession" runat="server">
<ContentTemplate>
    <!-- Navbar -->

    <div class="container mt--7">
      <!-- Table -->
      <div class="row justify-content-center">
        
          <div class="card mb-4   ">
            <div class="card-header bg-white py-3">
         
          <h6 class="m-0 font-weight-bold text-primary">Register Account</h6>
 
            </div>
            <div class="card-body">

     
       <div class="row" >
           <asp:Label ID="lblMsg" runat="server"></asp:Label>
  
                <div class="col-12 " style="font-weight: bold">
                <div class="form-group">
                    <label>Account name</label>
    
                                            
                                                
  <asp:TextBox ID="txtAccounttype"  class="form-control alert-danger "  ValidationGroup="A" BackColor="White" ForeColor="#999999" placeholder="Account Name" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

 </div>
                                    <div class="row" >                                                                       
                                    <div class="col-12 " style="font-weight: bold">
                                            <div class="form-group">
                                                <label>Account No.</label>
                                      
  <asp:TextBox ID="txtAccountNo" class="form-control alert-danger "  ValidationGroup="A" BackColor="White" ForeColor="#999999" title="Asset:1-001, Liability:2-001, Equity:3-001, Revenue:4-001, Expense:5-001, Other:6-001, you should leave the gap for the future addition of account"  runat="server"></asp:TextBox>

                                            </div>
                                        </div>
                                        </div>
 
                    <div class="row" > 
                                                                                                                                                                 
                          <div class="col-12  " >
                                            <div class="form-group">
                                            
                                              Account type:<asp:DropDownList ID="DropDownList4" runat="server" 
                                         class="form-control alert-danger "  ValidationGroup="A" BackColor="White" ForeColor="#999999">
                     <asp:ListItem>-Select-</asp:ListItem>
                   <asp:ListItem>Cash</asp:ListItem>
                   <asp:ListItem>Accounts Receivable</asp:ListItem>
                   <asp:ListItem>Inventory</asp:ListItem>
                   <asp:ListItem>Other Current Assets</asp:ListItem>
                   <asp:ListItem>Fixed Assets</asp:ListItem>
                   <asp:ListItem>Other Assets</asp:ListItem>
                    
                    <asp:ListItem>Accounts Payable</asp:ListItem>
                   <asp:ListItem>Other Current Liabilities</asp:ListItem>
                   <asp:ListItem>Long Term Liabilities</asp:ListItem>
                   <asp:ListItem>Equity</asp:ListItem>

                   <asp:ListItem>Income</asp:ListItem>
                   <asp:ListItem>Cost of Sales</asp:ListItem>
                   <asp:ListItem>Expenses</asp:ListItem>
                   <asp:ListItem>Accumulated Depreciation</asp:ListItem>
                     
                   </asp:DropDownList>
                </div>
                 </div>
                 </div>      
                
                       <div class="row" >                                                                       
                    <div class="col-12">
                               <div class="form-group mb-0">
             <label class=font-weight-bold>Opening Balance<span class=text-danger>*</span></label>
            <div class="input-group input-group-alternative">
            <asp:TextBox ID="txtOpeningbalance" class="form-control" runat="server"></asp:TextBox>
              <div class="input-group-prepend">
                <span class="input-group-text">ETB</span>
              </div>
              
            </div>
          </div>
                        </div>
                                        </div>
                       <div class="row" >                                                                       
                                    <div class="col-12 " style="font-weight: bold">
                                            <div class="form-group">
                                                <label>Date of The balance saved in the software</label>
                                      
  <asp:TextBox ID="txtDate" class="form-control alert-danger " TextMode=Date  ValidationGroup="A" BackColor="White" ForeColor="#999999" runat="server" ></asp:TextBox>

                                            </div>
                                        </div>
                                        </div>

                                       <div class=row>
                                            <div class="col-12 ">
                                            <div class="form-group">
                                                <label>Remark</label>

                                                 <br />
  <asp:TextBox ID="TextBox4" TextMode="MultiLine" class="form-control" placeholder="Explanation" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                                 </div>                                                                                                                        
                                   
      </div>


                <div class="text-center">
                <asp:Button ID="Button1" class="btn btn-primary my-4" runat="server" Text="Save" 
                        onclick="Button1_Click1" />
                </div>
           
            </div>
          </div>
        </div>    
        </ContentTemplate>
     </asp:UpdatePanel> 
      </div>
      

</asp:Content>

