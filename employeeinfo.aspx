 <%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.master" AutoEventWireup="true" CodeBehind="employeeinfo.aspx.cs" Inherits="advtech.Finance.Accounta.employeeinfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Employee Details</title>
    <style>
                .dropdown-list-image1 {
  position: relative;
  height: 5rem;
  width: 5rem;
}
        .dropdown-list-image1 img {
  height: 5.4rem;
  width: 5.4rem;
}
        .dropdown-list-image1 .status-indicator1 {
  background-color: #eaecf4;
  height: 1.1rem;
  width: 1.1rem;
  border-radius: 100%;
  position: absolute;
  bottom: 0;
  right: 0;
  border: 0.125rem solid #fff;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="container-fluid pr-3 pl-3">
           <div class="row">
        <div class="col">
            <div class="bg-white rounded-lg mb-2">
                  <div class="row">

            <div class="col-xl-6 col-md-6 border-right">

                <div class="card-header bg-white  font-weight-bold">
                <div class=row>
                   <div class="col-md-6 text-left">
                     
                                                        <a class="btn btn-light btn-circle mr-2" id="buttonback"  runat="server" href="employeelist.aspx" data-toggle="tooltip" data-placement="bottom" title="Back to employee list page">
             
                    <span class="fa fa-arrow-left text-danger"></span>

                </a><span id="Span7" runat="server" class="badge badge-light small text-uppercase font-weight-bold text-gray-700"></span>
                </div>
                <div class="col-md-6 text-right">
             <a runat="server" data-toggle="tooltip" data-placement="bottom" title="Activate / Terminate Employee"  id="modalMain"  class="btn btn-light btn-circle mr-2" >
                    <div>
                      <i class="fas fa-pencil-alt text-danger"></i>
                      
                    </div>
                  </a>
                                 <a runat="server"   id="A1"  class="btn btn-light btn-circle mr-2" data-toggle="tooltip" data-placement="bottom" title="Change Portal Access">
                    <div>
                      <i class="fas fa-unlink text-danger"></i>
                      
                    </div>
                  </a>
                  </div>
                </div>

                </div>
                <div class="card-body">
                  <center>
                  <img class="rounded-circle" src="../../asset/Brand/pngtree-avatar.jpg" height="125" width="125" id="defaltprf" runat="server" />
                                        <div class="dropdown-list-image1 mr-1">
                                            <asp:Repeater ID="Repeater2" runat="server">
                         <ItemTemplate>
                                                               
<img class="rounded-circle" src="../../asset/userprofile/<%#Eval("FileName")%><%#Eval("Extension")%>"   height="70" width="70" />
                       
               </span>
                         </ItemTemplate>
                     </asp:Repeater>                 
                                            <div id="StatusIndicator" runat="server"></div>
                                    </div>
                  <br />
                  <div style="padding: 20px 0px 0px 0px">
                  <span id="name" runat="server" class="badge badge-light text-primary text-lg font-weight-bold"></span>
                  </div>
                  
                  </center>
                  <hr />
    
                  <div >
                  <span class="mb-2 font-weight-bolder text-xs text-uppercase text-gray-300 ">Basic Information</span>
         
                  
                  <div class=" mr-2 mb-2" >
                  <span><i class="fas fa-envelope-open-text text-center text-gray-900 "></i></span>  <span id="emailwork" class="small text-gray-800" runat="server"></span>
                  </div>
                   <div class=" mr-2 mb-2" >
                  <span><i class="fas fa-calendar-plus text-center text-gray-900 "></i></span>  <span id="datejoining" class="small text-gray-800" runat="server"></span>
                  </div>
                  
                                    
                                                    <div class=" mr-3">
                  <span><i class=" font-weight-bolder fas fa-bezier-curve text-center text-gray-900"></i></span>  <span id="department" class="small text-gray-800" runat=server title="Work Phone" ></span>
                  </div>
                      <hr />
                      <div class="row">
                     
                          <div class="col-xl-6 text-left">
<span class="small font-weight-bolder text-center text-gray-900">Status</span>
                          </div>
                          <div class="col-xl-6 text-right">
<span id="Statusof" class="small  text-gray-800" runat=server title="Status" ></span>
                          </div>
                    
                  
                      </div>
                                            <hr />
                      <div class="row">
                     
                          <div class="col-xl-6 text-left">
<span class="small font-weight-bolder text-center text-gray-900">Leave Days Left</span>
                          </div>
                          <div class="col-xl-6 text-right">
<span id="sPanLeaveDays" class="small font-weight-bolder text-center text-gray-900" runat=server title="Work Phone" ></span>
                          </div>
                    
                  
                      </div>
                  </div>
                </div>
             
       
            <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
              <div class="row align-items-center">
                <div class="col-12">
           
                  <h6 class="text-xs font-weight-bolder text-primary text-uppercase mb-1">Absent vs Leave Dys Left<span class="mx-2 text-xs text-gray-300 text-uppercase">Updated</span></h6>
                </div>
                <div class="col">
                  <ul class="nav nav-pills justify-content-end">

                  </ul>
                </div>
              </div>
            </div>
            <div class="card-body">

     <main role="main" id="main1" runat="server">
<center>
      <div class="starter-template">
          
        
        <p class="lead">
            <span class="fas fa-atom text-gray-400 fa-2x"></span></p>
      </div>
         <h6 class="text-gray-400 text-center small" style="font-weight: bold">Sorry!! Nothing to show here.</h6>
          </center>


    </main>
              <div class="chart">
                  <asp:Literal ID="Literal1" runat="server"></asp:Literal>   
                  </div>
                     <hr />
       
            </div>
         
            </div>

            <div class="col-xl-6 col-md-6">

                <!-- Card Header - Dropdown -->
                <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
                  <span id="Span6" runat="server" class="badge badge-light small text-uppercase font-weight-bold text-gray-700">Personal Information</span>
                  <div class="dropdown no-arrow">
                        <button class="btn btn-light btn-circle dropdown-toggle" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" >
             
                      <a class="nav-link btn btn-sm"  data-toggle="tooltip" data-placement="bottom" title="Options">
                              <div>
                      <i class="fas fa-caret-down text-danger"></i>
                      
                    </div>
                        </a>
  
                </button>
                    <div class="dropdown-menu  dropdown-menu-right shadow animated--fade-in" aria-labelledby="dropdownMenuLink">
                      <div class="dropdown-header">Option:</div>
                      
                        <a class="dropdown-item" id="A2" runat=server >Retirment Card</a>
                        <a class="dropdown-item border-top text-gray-900" id="A12" runat=server href="#" data-toggle="modal" data-target="#exampleModalMisc">Bind Emp. Info</a>

                        <a class="dropdown-item text-danger" id="A4" runat=server >Detail Report</a>
                    </div>
                  </div>
                </div>
                <!-- Card Body -->
          <div class=card-body>
             <div class="row">

            <!-- Earnings (Monthly) Card Example -->
            <div class="col-md-6 mb-1">
              <div >
                  <span class="mb-2 font-weight-bolder text-xs text-uppercase text-gray-300 ">Basic Information</span>
         
                  
                  
                  <div class=" mr-2 mb-2" >
                  <span><i class="text-center text-gray-900 text-xs ">Father's Name</i></span>  <span id="Span4" class="small text-gray-800" runat="server"></span>
                  </div>

                  <div class=" mr-2 mb-2" >
                  <span><i class=" text-center text-gray-900 text-xs ">Date of Birth</i></span>  <span id="Span5" class="small text-gray-800" runat="server"></span>
                  </div>

                   <div class=" mr-2 mb-2" >
                  <span><i class=" text-center text-gray-900 text-xs ">Contact Email</i></span>  <span id="Span2" class="small text-gray-800" runat="server"></span>
                  </div>
    
                     <div class=" mr-3 mb-2">
                  <span><i class="  text-center text-gray-900 text-xs">Residential Address</i></span>  <span id="Span3" class="small text-gray-800" runat=server title="Work Phone" ></span>
                  </div>
    <div class=" mr-2 mb-2" >
                  <span><i class=" text-center text-gray-900  text-xs">Mobile</i></span>  <span id="Span1" class="small text-gray-800" runat="server"></span>
                  </div>
                  </div>
            </div>

            <!-- Earnings (Monthly) Card Example -->
            <div class="col-md-6 mb-1">
              <div >
     <span class="mb-2 invisible">sd</span>
         
                  
                  
                  <div class=" mr-2 mb-2" >
                  <span><span class="text-center font-weight-bold text-xs  text-gray-900 "></span></span>  <span id="father" class="small font-weight-bold text-gray-800" runat="server"></span>
                  </div>

                  <div class=" mr-2 mb-2" >
                  <span><span class=" text-center font-weight-bold text-xs   text-gray-900 "></span></span>  <span id="birthdate" class="small font-weight-bold text-gray-800" runat="server"></span>
                  </div>

                   <div class=" mr-2 mb-2" >
                  <span><span class=" text-center font-weight-bold text-xs   text-gray-900 "></span></span>  <span id="contemail" class="small font-weight-bold text-gray-800" runat="server"></span>
                  </div>
    
                     <div class=" mr-3 mb-2">
                  <span><span class="  text-center font-weight-bold text-xs text-gray-900"></span></span>  <span id="addressres" class="small font-weight-bold text-gray-800" runat=server title="Work Phone" ></span>
                  </div>
    <div class=" mr-2 mb-2" >
                  <span><span class=" text-center font-weight-bold text-xs text-gray-900 "></span></span>  <span id="mobile" class="small font-weight-bold text-gray-800" runat="server"></span>
                  </div>
                  </div>
            </div>
             
            <!-- Earnings (Monthly) Card Example -->

          </div>
               </div>           


               <div class="card-header bg-white  d-flex flex-row align-items-center justify-content-between">
              <h6 class="m-0 font-weight-bold text-gray-800">Payment Mode</h6>
             <div class="row align-items-center">
             <div class=" col-12 text-right">

             </div>
             </div>
                </div>
               
                <!-- Card Content - Collapse -->
     
                  <div class="card-body">
                            <div class="row">

            <!-- Earnings (Monthly) Card Example -->
            <div class="col-md-6 mb-1">
<div >
                  <span class="mb-2 font-weight-bolder text-xs text-uppercase text-gray-400 ">Payment mode</span>
         

                  </div>
            </div>

            <!-- Earnings (Monthly) Card Example -->
            <div class="col-xl-6 col-md-6 mb-1">
              <div >

                  
                  
                  <div class=" mr-2 mb-2" >
                  <h6 id="payment" class="small font-weight-bold text-right text-gray-800" runat="server"></h6>
                  </div>

                  </div>
            </div>
             
            <!-- Earnings (Monthly) Card Example -->

          </div>
     

                  </div>
      
                <div class="card-header bg-white">
                    <h6 class="m-0 font-weight-bold text-gray-800">Salary Information</h6>
                </div>
              <asp:Repeater ID="Repeater1" runat="server" >
                         
                            <HeaderTemplate>
                        <div class="table-responsive text-gray-900">
                          <table class="table table-sm ">
                            <thead>
                              <tr>
                                  <th scope="col">Name</th>
                                <th scope="col">Salary</th>
                                <th scope="col">Tax</th>
  
                                <th scope="col">Deduction</th>  
                              </tr>
                            </thead>
                            <tbody>
                             </HeaderTemplate>
                            <ItemTemplate>
                              <tr>


                                <td class="text-left text-primary">
                                <%#Eval("Name")%>
                                    <asp:Label ID="lblCustomerId" runat="server" Text='<%#Eval("Name") %>' Visible="false" />
                                </td>
                             
                                                                    <td>
                                
                                                         <asp:Label ID="Label1" runat="server" Text='<%# Eval("Salary", "{0:N2}")%>' />
                    <asp:TextBox ID="TextBox1" class="form-control"   runat="server" Width="100" Text='<%# Eval("Salary", "{0:N2}")%>'
                        Visible="false" />
                                </td>
                                <td>
                                                         <asp:Label ID="Label2" runat="server" Text='<%# Convert.ToDouble(Eval("Tax", "{0:N2}"))*100 %>' />%
                    <asp:TextBox ID="TextBox2" class="form-control"   runat="server" Width="100" Text='<%# Eval("Tax")%>'
                        Visible="false" />
                                </td>

                             
                                
                                                                      <td>
                                
                                                         <asp:Label ID="Label3" runat="server" Text='<%# Eval("Deduction", "{0:N2}")%>' />
                    <asp:TextBox ID="TextBox3" class="form-control"   runat="server" Width="100" Text='<%# Eval("Deduction")%>'
                        Visible="false" />
                                </td>
                                                          <td class="text-right">
                      <asp:LinkButton ID="lnkEdit" class=" btn btn-circle btn-sm btn-primary" Text="Edit" runat="server" OnClick="OnEdit" />
                 <asp:LinkButton ID="lnkUpdate" Text="Save" runat="server" class=" btn btn-circle btn-sm  btn-warning" Visible="false" OnClick="OnUpdate" />                         
                    </td>
                                </td>
                              </tr>
                              </ItemTemplate>
                            <FooterTemplate>
                            </tbody>
                          </table>
                             </FooterTemplate>
                                                     
                        </asp:Repeater>
        
            </div>

          </div>
                </div>
        </div>
        </div>
 </div>

    <div class="modal fade" id="exampleModalMisc" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabelD1" aria-hidden="true">
  <div class="modal-dialog modal-sm" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h6 class="modal-title text-gray-900 font-weight-bolder" id="exampleModalLabelD1">Update Employee Info</h6>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
<h6 class="text-gray-900">Basic Info</h6>
          <hr />
        <div class="row mb-3">
            
            <div class="col-md-12 ">
                <label>Date of Joining</label>
                <asp:TextBox ID="txtDateofJoining"  class="form-control" TextMode="Date" placeholder="Date of Joining" runat="server"></asp:TextBox>

            </div></div>
              <div class="row mb-3">
            <div class="col-md-12 ">
                <asp:TextBox ID="txtWorkMail"  class="form-control" TextMode="Email"  placeholder="WorkMail" runat="server"></asp:TextBox>

            </div></div>
          <div class="row mb-2">
              <div class="col-md-12">
                                                                                         <asp:DropDownList ID="ddlPosition" runat="server" 
                                                    class="form-control " 
                                                   >
                                                   <asp:ListItem>-Select Position-</asp:ListItem>
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
          <div class="row mb-2">
              <div class="col-md-12">
                                                                    <asp:DropDownList ID="ddlDepartment" runat="server" class="form-control ">
                                                   
                                                   <asp:ListItem>-Select Department-</asp:ListItem>
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
          <h6 class="text-gray-900 mt-3">Personal Info</h6>
          <hr />
        <div class="row mb-3">
            
            <div class="col-md-12 ">

                <asp:TextBox ID="txtPersonalEmail"  class="form-control" TextMode="Email" placeholder="Personal Email" runat="server"></asp:TextBox>

            </div></div>
                  <div class="row mb-3">
            
            <div class="col-md-12 ">

                <asp:TextBox ID="txtMobile"  class="form-control"  placeholder="Mobile" runat="server"></asp:TextBox>

            </div></div>
                            <div class="row mb-3">
            
            <div class="col-md-12 ">

                <asp:TextBox ID="txtDateOfBirth"  class="form-control"  placeholder="Date of Birth" runat="server"></asp:TextBox>

            </div></div>
                                      <div class="row mb-3">
            
            <div class="col-md-12 ">

                <asp:TextBox ID="txtFatherName"  class="form-control"  placeholder="Father Name" runat="server"></asp:TextBox>

            </div></div>
                                                <div class="row mb-3">
            
            <div class="col-md-12 ">

                <asp:TextBox ID="txtResidentialAddress"  class="form-control"  placeholder="Residential Address" runat="server"></asp:TextBox>

            </div></div>
          <div class="row">
<div class="col-md-12">
             <asp:Button ID="btnEmpInfo" runat="server" class="btn btn-sm w-100 btn-success" Text="Save" OnClick="btnEmpInfo_Click"/>

            </div>
          </div>
            
        </div>
      
        <center>
      <div class="modal-footer">
       

       
       
      </div>

        </center>
    </div>
  </div>
</div>
</asp:Content>
