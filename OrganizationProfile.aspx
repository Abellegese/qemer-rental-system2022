<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.master" AutoEventWireup="true" CodeBehind="OrganizationProfile.aspx.cs" Inherits="advtech.Finance.Accounta.OrganizationProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>Organization</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="main-content">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

<div class="container-fluid mt--7">
      <div class="row">
        <div class="col">
        <div id="div_print"> 

                       <div class="card shadow-none mb-4">
                <div class="card-header bg-white">
                <h6 class="m-0 font-weight-bold text-uppercase "> Organization Profile</h6>
                
                </div>
                <div class="card-body">
                <div class="row">
                                            <div class="col-md-12 ">
                                            <div class="form-group ">
                                             <label>Organzation Name</label>
   <asp:TextBox ID="txtOname"  class="form-control " BackColor="White" placeholder="Organzation Name" runat="server"></asp:TextBox>
                </div>
                 </div>
                 </div>
                 <div class="row">
                                                                                                                                        <div class="col-md-5 ">
                                            <div class="form-group">
                                                <label>Attach Logo</label>
    
                                            
                                                     <asp:FileUpload ID="FileUpload1" class="form-control"  runat="server"></asp:FileUpload>
 
                                            </div>
                                        </div>
                                        </div>

                                                        <div class="row">
                                            <div class="col-md-12 ">
                                            <div class="form-group ">
                                             <label>Organzation Address</label>
   <asp:TextBox ID="txtAddress"  class="form-control " BackColor="White" placeholder="Organzation Adress" runat="server"></asp:TextBox>
                </div>
                 </div>
                 </div>
                  <div class="row">
                                              <div class="col-md-4 ">
                                            <div class="form-group">

                                                <label>City</label>
    
                                            
                                                 <br />
  <asp:TextBox ID="txtCity"  class="form-control" placeholder="City" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        

                                                                                      <div class="col-md-4 ">
                                            <div class="form-group">

                                                <label>Email</label>
    
                                            
                                                 <br />
  <asp:TextBox ID="txtEmail"   class="form-control" placeholder="Email" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                 </div>
                                                                       
                                        
                                        <div class="row">   
                                                                                                                          <div class="col-4 ">
                                            <div class="form-group">
                                                <label>Fax</label>
    
                                            

<asp:TextBox ID="txtFax" class="form-control " placeholder="Fax" runat="server"></asp:TextBox>
              </div>
              


                                            </div>
                                        
                                                                                                                        <div class="col-4 ">
                                            <div class="form-group">
                                                <label>Contact</label>
    
                                            
                                                 <br />
  <asp:TextBox ID="txtContact"  class="form-control" placeholder="Contact" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                        </div> 
                                        <hr />
                                                                                                <div class="row">
                                            <div class="col-md-12 ">
                                            <div class="form-group ">
                                             <label>Business Location</label>
   <asp:TextBox ID="txtBussinessLocation"  class="form-control" placeholder="Bussness Location" runat="server"></asp:TextBox>
                </div>
                 </div>
                 </div>
                                 <div class="row">
                                            <div class="col-md-6 ">
                                            <div class="form-group ">
                                             <label>Country</label>
<asp:TextBox ID="txtCountry"  class="form-control" placeholder="Country" runat="server"></asp:TextBox>
                </div>
                 </div>
                                                             <div class="col-md-6 ">
                                            <div class="form-group">
                                             <label>Business Type</label>
                                                                       <asp:DropDownList ID="DropDownList1" runat="server" 
                                                    class="form-control " 
                                                   >
                   <asp:ListItem>Manufacturing</asp:ListItem>
                   <asp:ListItem>Corporation</asp:ListItem>
                   <asp:ListItem>Marchandise</asp:ListItem>
                   <asp:ListItem>Service Company</asp:ListItem>
                                                    
                                                </asp:DropDownList>
                </div>
                 </div>
                 </div>
                                            
                                        </div>                                             
 
                </div>
                </div>
                       
        
          
              <center>                 
                  <asp:Button ID="Button1" runat="server" class="btn btn-success w-25" Text="Save" 
                     onclick="Button1_Click" />

              </center>

              </div>
              </div>

              </div>
</asp:Content>

