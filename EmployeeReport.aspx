<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="EmployeeReport.aspx.cs" Inherits="advtech.Finance.Accounta.EmployeeReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
            <script language="javascript">
    function printdiv(printpage) {
        var headstr = "<html><head><title></title></head><body>";
        var footstr = "</body>";
        var newstr = document.all.item(printpage).innerHTML;
        var oldstr = document.body.innerHTML;
        document.body.innerHTML = headstr + newstr + footstr;
        window.print();
        document.body.innerHTML = oldstr;
        return false;
    }
            </script>
<title>Employee Profile</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid pl-3 pr-3">

    <!-- Navbar -->

    <!-- End Navbar -->
    <!-- Header -->

    
      <!-- Table -->
           
      <div class="row">
        <div class="col-12">
            <div class="card mb-2 shadow-none ">
            <div class="card-header bg-white ">
                <div class="row">
                <div class="col-4 text-left">
                  <a class="btn btn-light btn-circle mr-2" id="buttonback" href="employeelist.aspx" runat="server"  data-toggle="tooltip" data-placement="bottom" title="Back to customer details">
             
                    <span class="fa fa-arrow-left text-danger"></span>
  
                </a>
                </div>
            <div class="col-8 text-right">

                <button name="b_print" onclick="printdiv('div_print');" type="button" title="Print" class="border-primary border-left border-top border-right border-bottom btn btn-sm btn-default btn-circle" data-toggle="modal" data-target="#exampleModalCenter" >
                    <div>
                      <i class="fas fa-print text-primary font-weight-bold"></i>
                   
                    </div>
                  </button>

                   </div>
                </div>

            </div>
            
         <div id="div_print"> 
                      
         <div class="row" id="div4" runat="server">
                    <div class="col-2">

                    </div>
                    <div class="col-8">
    
                                 
                    <div class="card-header text-black bg-white font-weight-bold">


          
                <div class="row border-bottom" style="height:90px">
                <div class="col-md-12 text-left">
       
    
            <h4 class="text-gray-900 font-weight-bold">EMPLOYEE PROFILE</h4>
              </div>
                
                </div>
               <div class="row border-bottom border-dark border-top mb-5">               
                   <div class="col-md-6 text-left">
                   <span translate="no" class="small text-gray-900 font-weight-bold">February 13, 2022</span>
                    </div>
                <div class="col-md-6 text-right">
                    <span translate="no" class="h5 text-gray-900 font-weight-bold">RAKSYM TRADING PLC</span>
                    </div>

                   </div>

              <div class="row mt-5  ">
                <div class="col-md-3 text-left">
                    <div class="card-body border-dark border-bottom border-right border-top border-left" style="height:160px">
                        <center>
                            <h6 class="text-gray-900 mt-xl-5">4 x 4</h6>
                            <h6 class="mt-1 text-gray-900 ">Images</h6>
                        </center>
                    </div>
                    
                    </div>
                    <div class="col-md-8 text-left">
                        <div class="row">
                            <div class="col-md-4">
                                                      <h6 class="text-gray-600 border-bottom font-weight-bold">Name</h6>
                         <h6 class="text-gray-600 border-bottom font-weight-bold">Hired Date</h6>
                         <h6 class="text-gray-600 border-bottom font-weight-bold">Email</h6>
                         <h6 class="text-gray-600 border-bottom font-weight-bold">Address</h6>
                         <h6 class="text-gray-600 border-bottom font-weight-bold">Contact</h6>
                         <h6 class="text-gray-600 border-bottom font-weight-bold">Position</h6>
                          <h6 class="text-gray-600 border-bottom font-weight-bold">Department</h6>
                          </div>
                          <div class="col-md-8 text-right">
                          <h6 class="text-gray-900 border-bottom font-weight-bold" id="Name" runat="server"></h6>
                         <h6 class="text-gray-900 border-bottom font-weight-bold" id="DateofJoining" runat="server">-</h6>
                         <h6 class="text-gray-900 border-bottom font-weight-bold" id="Email" runat="server">-</h6>
                         <h6 class="text-gray-900 border-bottom font-weight-bold" id="Address"  runat="server">-</h6>
                         <h6 class="text-gray-900 border-bottom font-weight-bold" id="Contact" runat="server">-</h6>
                         <h6 class="text-gray-900 border-bottom font-weight-bold" id="Position"  runat="server">-</h6>
                              <h6 class="text-gray-900 border-bottom font-weight-bold" id="Department"  runat="server">-</h6>
                            </div>
                        </div>

                    </div>
                    </div>
                        <br />
                        
                        <div class="border-bottom border-top border-dark">
                            <span class="h6 text-gray-900 font-weight-bold">Salary Details</span>
                        </div>
                        <div class="row mt-5">
                            <div class="col-md-12">
                                <asp:Repeater ID="Repeater1" runat="server" >
                         
                            <HeaderTemplate>
                        <div class="table-responsive text-gray-900">
                          <table class="table table-sm small text-gray-900 ">
                            <thead>
                              <tr>
          
                                <th scope="col">Salary</th>
                                <th scope="col">Tax</th>
  
                                <th scope="col"  class="text-right">Deduction</th>  
                              </tr>
                            </thead>
                            <tbody>
                             </HeaderTemplate>
                            <ItemTemplate>
                              <tr>


    
                             
                                                                    <td>
                                
                                                         <asp:Label ID="Label1" runat="server" Text='<%# Eval("Salary", "{0:N2}")%>' />

                                </td>
                                <td>
                                                         <asp:Label ID="Label2" runat="server" Text='<%# Convert.ToDouble(Eval("Tax", "{0:N2}"))*100 %>' />%

                                </td>

                             
                                
                                                                      <td class="text-right">
                                
                                                         <asp:Label ID="Label3" runat="server" Text='<%# Eval("Deduction", "{0:N2}")%>' />

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
     
                                <div class="col-2">

                                </div>
                            </div>

                          </div>

                        </div>

                          </div>
                          </div>
                          </div>
</asp:Content>
