<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.master" AutoEventWireup="true" CodeBehind="EmployeesInformation.aspx.cs" Inherits="advtech.Finance.Accounta.EmployeesInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Add Employee</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="main-content">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <!-- Navbar -->
       

    <div class="container-fluid mt--7">
      <!-- Table -->
      <div class="row">

        <div class="col">

          <div class="card shadow-none mb-4 w-auto ">
            <div class="card-header bg-white">
             <h4 class="m-0 font-weight-bold text-center text-gray-900">Add Employee</h4>
             <br />
             <br />
                            <div class="row align-items-center">
                                            <div class="col-xl-3 ">
                                           
                <a >
                  <div class="mr-3"> 
                  
                    <div id="Basic" runat="server" class="icon-circle bg-white border-dark border-left border-top border-right border-bottom">
                      <i id="BasicIcon" runat="server" class="text-gray-900  font-weight-bold" style="font-style: normal">1</i>

                    </div>                      
                    <br />
                    <span id="BasicSpan" runat=server class="text-gray-900 font-weight-bold">Basics</span>
                      
                  </div>
                  <div>

                  </div>
                </a>
         
                </div>

                <div class="col-xl-3 " style="padding-right: 0px; padding-left: 61px">

                <a>
                  <div class="mr-8">
                    <div id=Salary runat=server class="icon-circle  bg-white border-dark border-left border-top border-right border-bottom">
                      <i id="SalaryIcon" runat=server class="text-gray-900 font-weight-bold" style="font-style: normal">2</i>
                    </div>
                                        <br />
                                        <span id=SalarySpan runat=server class="text-gray-900 font-weight-bold text-left" style="padding-right: 50px">Salary Details</span>
                   
                  </div>
                  <div>

                  </div>
                </a>

                </div>

                                <div class="col-xl-3 " style="padding-right: 0px; padding-left: 80px">

                <a>
                  <div class="mr-8">
                    <div id="Personal" runat="server" class="icon-circle bg-white border-dark border-left border-top border-right border-bottom">
                      <i id="PersonalIcon" runat="server" class="text-gray-900 font-weight-bold" style="font-style: normal">3</i>
                    </div>
                                                            <br />
                                                            <span id="PersonalSpan" runat="server" class="text-gray-900 font-weight-bold text-left" style="padding-left: -22px">Personal Info</span>
                      
                  </div>
                  <div>

                  </div>
                </a>

                </div>
                                <div class="col-xl-3 " style="padding-right: 0px; padding-left: 100px">

                <a>
                  <div class="mr-8">
                    <div id="Payment" runat="server" class="icon-circle bg-white border-dark border-left border-top border-right border-bottom">
                      <i id="PaymentIcon" runat="server" class="text-gray-900 font-weight-bold" style="font-style: normal">4</i>

                    </div>
                                                            <br />
                                                             <span id="PaymentSpan" runat="server" class="text-gray-900 font-weight-bold text-left" style="padding-left: 0px; padding-right: 50px;">Payment Info</span>
                      
                  </div>
                  <div>

                  </div>
                </a>

                </div>
              </div>
            </div>
            <div class="card-body">
            <asp:Panel ID="Panel5" runat="server">
                                              <div class="alert alert-danger alert-with-icon" data-notify="container">
                                    <button type="button" aria-hidden="true" class="close">
                                        <i class="now-ui-icons ui-1_simple-remove"></i>
                                    </button>
                                    <span data-notify="icon" class="fas fa-info-circle"></span>
                                    <span id="Span" runat="server" data-notify="message"></span>
                                </div>
                                </asp:Panel>
                                <asp:Panel ID="Panel6" runat="server">
                                              <div class="alert alert-success alert-with-icon" data-notify="container">
                                    <button type="button" aria-hidden="true" class="close">
                                        <i class="now-ui-icons ui-1_simple-remove"></i>
                                    </button>
                                    <span data-notify="icon" class="fas fa-bell"></span>
                                                  <asp:Label ID="Label1" runat="server" ></asp:Label>
                                    <span id="Span1" runat="server" data-notify="message">You have successfully <b>added the employee</b></span>
                                </div>
                                </asp:Panel>
                <asp:Panel ID="Panel1" runat="server">
                
            
                            <div class="row">
                                            <div class="col-md-4 ">
                                            <div class="form-group has-error">
                                             <label class=font-weight-bold>Employee Full Name<span class=text-danger>*</span></label>
   <asp:TextBox ID="txtEmployeeFName"  class="form-control alert-danger" BackColor="White" placeholder="Full Name" runat="server"></asp:TextBox>
                </div>
                 </div>

                 </div>


                                                        <div class="row">
                                            <div class="col-md-6 ">
                                            <div class="form-group has-error">
                                             <label class=font-weight-bold>Employee ID<span class=text-danger>*</span></label>
   <asp:TextBox ID="txtEmployeeID"  class="form-control alert-danger" BackColor="White" placeholder="ID" runat="server"></asp:TextBox>
                </div>
                 </div>
                                                                              <div class="col-md-6 ">
                                            <div class="form-group has-error">
                                             <label class=font-weight-bold>Gender<span class=text-danger>*</span></label>
                                                                       <asp:DropDownList ID="ddlGender" runat="server" 
                                                    class="form-control " 
                                                   >
                                                   <asp:ListItem>-Select-</asp:ListItem>
                   <asp:ListItem>Male</asp:ListItem>
                   <asp:ListItem>Female</asp:ListItem>

                                                    
                                                </asp:DropDownList>
                </div>
                 </div>
                 </div>
                  <div class="row">
                                              <div class="col-md-6 ">
                                            <div class="form-group">

                                                <label class=font-weight-bold>Date of Joining<span class=text-danger>*</span></label>
    
                                            
                                                 <br />
  <asp:TextBox ID="txtDateofJoining"  class="form-control" TextMode=Date placeholder="Date" runat="server"></asp:TextBox>
                                            </div> 
                                                  </div>                                                                               
                                            <div class="col-md-6 ">
                                            <div class="form-group has-error">
                                             <label class=font-weight-bold>Position<span class=text-danger>*</span></label>
                                                                       <asp:DropDownList ID="ddlPosition" runat="server" 
                                                    class="form-control " 
                                                   >
                                                   <asp:ListItem>-Select-</asp:ListItem>
                   <asp:ListItem>Accountant</asp:ListItem>
                   <asp:ListItem>Finance Head</asp:ListItem>
                   <asp:ListItem>Manager</asp:ListItem>
                   <asp:ListItem>Store Keeper</asp:ListItem>
                    <asp:ListItem>Store Head</asp:ListItem>
                    <asp:ListItem>HR</asp:ListItem>
                   <asp:ListItem>Technical</asp:ListItem>
                    <asp:ListItem>Technical Head</asp:ListItem>
  
                                                    
                                                </asp:DropDownList>
                </div>
                                        </div>
                                        
                                        


                 </div>
     
                                 <div class="row">
                                            <div class="col-md-12 ">
                                            <div class="form-group has-error">
                                             <label class=font-weight-bold>Work Email<span class=text-danger>*</span></label>
   <asp:TextBox ID="txtWorkEmail"  class="form-control alert-danger" BackColor="White" placeholder="Work Email" runat="server"></asp:TextBox>
                                                
                </div>
                 </div>
                 </div>                          
                                        
                         <div class="row">
                                              <div class="col-md-6 ">
                                            <div class="form-group">

                                                <label class=font-weight-bold>Departement<span class=text-danger>*</span></label>
    
                                            
                                                 <br />
                                                  <asp:DropDownList ID="ddlDepartment" runat="server" class="form-control ">
                                                   
                                                   <asp:ListItem>-Select-</asp:ListItem>
                                                    <asp:ListItem>Finance</asp:ListItem>
                                                    <asp:ListItem>HR</asp:ListItem>
                                                    <asp:ListItem>Marketing</asp:ListItem>
                                                      <asp:ListItem>Managment</asp:ListItem>
                                                    <asp:ListItem>Store</asp:ListItem>
                                                    <asp:ListItem>Technical</asp:ListItem>
                                                    <asp:ListItem>Other</asp:ListItem>
                                                                           
                                                   </asp:DropDownList>
                                            </div> 
                                                  </div>                                                                               
                                            <div class="col-md-6 ">
                                            <div class="form-group">

                                                <label class=font-weight-bold>Work Location<span class=text-danger>*</span></label>
    
                                            
                                                 <br />
  <asp:TextBox ID="txtWorkLocation"   class="form-control" placeholder="Work Location" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        
                                        


                 </div>
                    <div class="row">
                                          <div class="col-md-12 ">
                                            <div class="form-group">
                <div class="custom-control custom-checkbox font-weight-300">
  <input type="checkbox" class="custom-control-input" id="Checkbox1" runat="server" clientidmode="Static" />
  <label class="custom-control-label " for="Checkbox1">Allow portal access</label>
</div>
                                                </div>
                                              </div>
                    </div>
            <hr />
                                 

                                                                                                                 <div class="col-12 " style="font-weight: bold">
                                            <div class="form-group">
                                   <asp:LinkButton ID="LinkButton1" class="btn btn-primary"  runat="server" onclick="LinkButton1_Click" 
                                                   >Save and Continue</asp:LinkButton>                                   <asp:LinkButton ID="LinkButton2" class="btn btn-secondary"  runat="server" 
                                                   >Cancel</asp:LinkButton>

                                   </div>
                                   </div>
                                                                                                                                  <div class="col-1 " style="font-weight: bold">
                                            <div class="form-group">


                                   </div>
                                   </div>
                    
                </asp:Panel>
<asp:Panel ID="Panel2" runat="server">
<div class="row">
                                            <div class="col-md-6 ">
                                            <div class="form-group">
                                                <label class=font-weight-bold>Basic Salary<span class=text-danger>*</span></label>
    
                                            
                                                 <br />
                                                           <div class="form-group mb-0">
            <div class="input-group input-group-alternative">
            <asp:TextBox ID="txtBasic" class="form-control "  runat="server"></asp:TextBox>
              <div class="input-group-prepend">
                <span class="input-group-text">ETB</span>
              </div>
              
            </div>
          </div>

                                            </div>
                                        </div>
                                        </div>

                                                    <hr />
                                 

                                                                                                                 <div class="col-12 " style="font-weight: bold">
                                            <div class="form-group">
                                   <asp:LinkButton ID="LinkButton3" class="btn btn-primary"  runat="server" onclick="LinkButton2_Click" 
                                                   >Save and Continue</asp:LinkButton>                                   <asp:LinkButton ID="LinkButton4" class="btn btn-secondary"  runat="server" 
                                                   >Cancel</asp:LinkButton>

                                   </div>
                                   </div>
                    </asp:Panel>
                    <asp:Panel ID="Panel3" runat="server">
                
            
                            <div class="row">
                                                                        <div class="col-md-4 ">
                                            <div class="form-group">
                                                <label class="font-weight-bold">Personal Email Address<span class="font-weight-bold text-danger">*</span></label>
    
                                            
                                                 <br />
                                                           <div class="form-group mb-0">
            <div class="input-group input-group-alternative">
              <div class="input-group-prepend">
                <span class="input-group-text"><i class="fas fa-mail-bulk"></i></span>
              </div>
              <asp:TextBox ID="txtPersonalEmail" class="form-control " runat="server"></asp:TextBox>
            </div>
          </div>

                                            </div>
                                        </div>


                                                                                  <div class="col-md-4 ">
                                            <div class="form-group">
                                                 <label class=font-weight-bold>Mobile Number<span class=text-danger>*</span></label>
    
                                            
                                                 <br />
                                                           <div class="form-group mb-0">
            <div class="input-group input-group-alternative">
              <div class="input-group-prepend">
                <span class="input-group-text"><i class="fas fa-mobile"></i></span>
              </div>
              <asp:TextBox ID="txtMobileNumber" class="form-control "  runat="server"></asp:TextBox>
            </div>
          </div>

                                            </div>
                                        </div>
                                                                                                        <div class="col-md-4 ">
                                            <div class="form-group">
                                                <label class="font-weight-bold">Current Leave Days<span class="font-weight-bold text-danger">*</span></label>
    
                                            
                                                 <br />
                                                           <div class="form-group mb-0">
            <div class="input-group input-group-alternative">
              <div class="input-group-prepend">
                <span class="input-group-text"><i class="fas fa-mail-bulk"></i></span>
              </div>
              <asp:TextBox ID="txtLeavedays" class="form-control " value="0" runat="server"></asp:TextBox>
            </div>
          </div>

                                            </div>
                                        </div>
       
                 </div>
                 

                                                        <div class="row">
                                            <div class="col-md-6 ">
                                            <div class="form-group">
                                             <label class=font-weight-bold>Date of Birth<span class=text-danger>*</span></label>
   <asp:TextBox ID="txtDateofBirth" TextMode=Date  class="form-control alert-danger" BackColor="White"  runat="server" 
                                                    ></asp:TextBox>
                </div>
                 </div>
                                                  <div class="col-md-6 ">
                                            <div class="form-group">

                                                <label class=font-weight-bold><span class=text-danger></span></label>
    
                                            
 
                                            </div>
                                        </div>
                 </div>
                  <div class="row">
                                              <div class="col-md-12 ">
                                            <div class="form-group">

                                                <label class=font-weight-bold>Father's Name<span class=text-danger>*</span></label>
    
                                            
                                                 <br />
  <asp:TextBox ID="txtFathersName"  class="form-control"  runat="server"></asp:TextBox>
                                            </div> 
                                                  </div>                                                                               

                                        
                                        


                 </div>
                        
                                 <div class="row">
                                            <div class="col-md-12 ">
                                            <div class="form-group has-error">
                                             <label class=font-weight-bold>Residential Address<span class=text-danger>*</span></label>
   <asp:TextBox ID="txtResidentialAdress"  class="form-control alert-danger" BackColor="White" placeholder="Address"  runat="server"></asp:TextBox>
                </div>
                 </div>
                 </div>                          

            <hr />
                                                   <div class="row">
                                                                        <div class="col-md-6 ">
                                            <div class="form-group">
                                                           <div class="form-group mb-0">
            <div class="input-group input-group-alternative">
              <div class="input-group-prepend">
                <span class="input-group-text"><i class="fas fa-street-view"></i></span>
              </div>
              <asp:TextBox ID="txtStreet" class="form-control " placeholder="Street" runat="server"></asp:TextBox>
            </div>
          </div>

                                            </div>
                                        </div>

                                                                                  <div class="col-md-6 ">
                                            <div class="form-group">
               
                                                           <div class="form-group mb-0">
            <div class="input-group input-group-alternative">
              <div class="input-group-prepend">
                <span class="input-group-text"><i class="fas fa-city"></i></span>
              </div>
              <asp:TextBox ID="txtCity" class="form-control " placeholder="City"  runat="server"></asp:TextBox>
            </div>
          </div>

                                            </div>
                                        </div>
       
                 </div> 
                 <hr />        

                                                                                                                 <div class="col-12 " style="font-weight: bold">
                                            <div class="form-group">
                                   <asp:LinkButton ID="LinkButton5" class="btn btn-primary"  runat="server" onclick="LinkButton3_Click" 
                                                   >Save and Continue</asp:LinkButton>                                   <asp:LinkButton ID="LinkButton6" class="btn btn-secondary"  runat="server" 
                                                   >Cancel</asp:LinkButton>

                                   </div>
                                   </div>
                                                                                                                                  <div class="col-1 " style="font-weight: bold">
                                            <div class="form-group">


                                   </div>
                                   </div>
                    
                </asp:Panel>
                <asp:Panel ID="Panel4" runat="server">
<div class="row text-xl">
                                            <div class="col-md-6 ">

            
            
              
                <span ><i class="fas fa-money-bill-wave"></i><span> Bank Transfer</span></span>
              
              
      

                                           
                                        </div>
                                                                                    <div class="col-md-6 text-right ">
                                      

                                            <div class="custom-control custom-radio custom-control-inline">
  <input type="radio" id="Radio1" checked=true name="customRadioInline1" class="custom-control-input" runat=server clientidmode="Static"/>
  <label class="custom-control-label" for="Radio1"></label>
</div>
                                        </div>
                                        </div>
                                        <hr />
                                        <div class="row text-xl">
                                            <div class="col-md-6 ">

            
            
              
                <span ><i class=" font-weight-bolder fas fa-money-check"></i><span>  Cheque</span></span>
              
                                                
      

                                           
                                        </div>
                                                                                    <div class="col-md-6 text-right ">
                                      

                                            <div class="custom-control custom-radio custom-control-inline">
  <input type="radio" id="Radio2"  name="customRadioInline1" class="custom-control-input" runat=server clientidmode="Static"/>
  <label class="custom-control-label" for="Radio2"></label>
</div>
                                        </div>
                                        </div>
                                        <hr />
                                                                                <div class="row text-xl">
                                            <div class="col-md-6 ">

            
            
              
                <span ><i class=" font-weight-bolder fas fa-list"></i><span> Cash</span></span>
              
              
      

                                           
                                        </div>
                                                                                    <div class="col-md-6 text-right ">
                                      

                                            <div class="custom-control custom-radio custom-control-inline">
  <input type="radio" id="Radio3"  name="customRadioInline1" class="custom-control-input" runat=server clientidmode="Static"/>
  <label class="custom-control-label" for="Radio3"></label>
</div>
                                        </div>
                                        </div>
                                        <hr />

                                 

                                                                                                                 <div class="col-12 " style="font-weight: bold">
                                            <div class="form-group">
                                                
                                   <asp:LinkButton ID="LinkButton7" class="btn btn-primary"  runat="server" onclick="LinkButton4_Click" 
                                                   >Save and Continue</asp:LinkButton>                                   <asp:LinkButton ID="LinkButton8" class="btn btn-secondary"  runat="server" 
                                                   >Cancel</asp:LinkButton>

                                   </div>
                                   </div>
                    </asp:Panel>
            </div>
<!-- End -->

          </div>
        </div>
      </div>
      <!-- Dark table -->
      
      <!-- Footer -->

    </div>


</asp:Content>

