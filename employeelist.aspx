<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.master" AutoEventWireup="true" CodeBehind="employeelist.aspx.cs" Inherits="advtech.Finance.Accounta.employeelist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Employee List</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid pl-3 pr-3">
<div class="row">
              

               
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-sm" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title text-gray-900" id="exampleModalLabel">Type Employee Name</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        <div class="row">
            <div class="col-md-7">
                <asp:TextBox ID="txtCustomerName"  class="form-control mx-2" runat="server"></asp:TextBox>

            </div>

                        <div class="col-md-5">
                            <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="search.." OnClick="Button2_Click"  />

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
        <div class="col">
          <div class="card shadow-none ">
            <div class="card-header  bg-white py-3 d-flex flex-row align-items-center justify-content-between">
              <h6 class="m-0 font-weight-bold text-ark text-uppercase">Employees</h6>
                            <div class="row align-items-center">

                <div class="col-12 text-right">

                   <a runat="server" id="modalMain" href="EmployeesInformation.aspx" class="btn mx-2 btn-sm btn-circle text-xs btn-danger"  >
                    <div>
                      <i class="fas fa-plus"></i>

                    </div>
                  </a>
                                        <button type="button"  runat=server id="Sp2" class="border-primary border-left border-top border-right border-bottom btn btn-sm btn-default btn-circle" data-toggle="modal" data-target="#exampleModal" >
                    <div>
                      <i class="fas fa-search text-primary font-weight-bold"></i>
                   <span></span>
                    </div>
                  </button>
                </div>
              </div>
            </div>
              <div class="card-body small">
<asp:Repeater ID="Repeater1" runat="server" >
                         
                <HeaderTemplate>
            <div class="table-responsive">
              <table class="table align-items-center table-sm ">
                <thead>
                  <tr>
                
                 
                    <th scope="col" class="text-gray-900">Name</th>
                    <th scope="col" class="text-gray-900">Date of Joining</th>
            
                    <th scope="col" class="text-gray-900">Work Email</th>
                                        <th scope="col" class="text-gray-900 text-right">Department</th>   
                                            

                  </tr>
                </thead>
                <tbody>
                                                                                        </HeaderTemplate>
                <ItemTemplate>
                  <tr>


                    <td class="text-left text-primary">
                     <a title="Show the details" href="employeeinfo.aspx?fname=<%#Eval("FullName")%>"><%#Eval("FullName")%></a>
                   

                    </td>

                                        <td class="text-gray-900">
                    <%# Eval("DateofJoining","{0: MMMM dd, yyyy}")%>
                    </td>
                    <td class="text-gray-900">
                    <%# Eval("WorkEmail")%>
                    </td>
                                        <td class="text-gray-900 text-right">
                    <%# Eval("Department")%>
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
            <div class="card-footer bg-white py-4">
              <nav aria-label="...">
                
              </nav>
            </div>
          



    
            </div>
              </div>
    </div>
              
</asp:Content>
