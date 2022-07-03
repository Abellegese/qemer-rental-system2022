<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.master" AutoEventWireup="true" CodeBehind="Activity.aspx.cs" Inherits="advtech.Finance.Accounta.Activity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Activity Logs</title>

    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <!-- Navbar -->
        <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-sm" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title text-gray-900" id="exampleModalLabel">Search by module</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        <div class="row">
            <div class="col-md-7">
                <asp:DropDownList ID="DropDownList1" class="form-control" runat="server">

                    <asp:ListItem>Payment paid</asp:ListItem>
                    <asp:ListItem>Payment Received</asp:ListItem>
                    <asp:ListItem>Credit Issued</asp:ListItem>
                    <asp:ListItem>Quantity updated</asp:ListItem>

                </asp:DropDownList>

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
 <div class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
        <h5 class="modal-title" id="H1">Filter Activity</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
                                    <div class="row" >
                                      


                                                                                                        <div class="col-6 ">
                                            <div class="form-group">
                                                <label class=font-weight-bold>From<span class=text-danger>*</span></label>
    
                                            
                                                 <br />
                                                           <div class="form-group mb-0">
            <div class="input-group input-group-alternative">
            <asp:TextBox ID="txtDateform" class="form-control " TextMode="DateTimeLocal" runat="server"></asp:TextBox>
              <div class="input-group-prepend">
                <span class="input-group-text"><i class=" fas fa-calendar"></i></span>
              </div>
              
            </div>
          </div>
          </div>
          </div>
                                                                                                                  <div class="col-6">
                                            <div class="form-group">
                                                <label class=font-weight-bold>To<span class=text-danger>*</span></label>
    
                                            
                                                 <br />
                                                           <div class="form-group mb-0">
            <div class="input-group input-group-alternative">
            <asp:TextBox ID="txtDateto" class="form-control " TextMode="DateTimeLocal"  runat="server"></asp:TextBox>
              <div class="input-group-prepend">
                <span class="input-group-text"><i class=" fas fa-calendar"></i></span>
              </div>
              
            </div>
          </div>
          </div>
          </div>

                                                 </div>  
      </div>
      <div class="modal-footer">
      <center>
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                            <asp:Button ID="btnUpdate" class="btn btn-primary" OnClick="btnUpdate_Click"   runat="server"  
                                        Text="Search..."   />
                                        </div>
     </center>
      </div>
    </div>
  </div>
    <div class="container-fluid pr-3 pl-3">
      <!-- Table -->
      <div class="row">
        <div class="col">

          <div class="bg-white rounded-lg mb-4 w-auto">
            <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
              <h5 class="m-0 font-weight-bold text-primary">Activity</h5>
                            <div class="row align-items-center">

                <div class="col-12 text-right">

                   <button type="button"   runat=server id="Button1" class="mx-1 border-primary border-left border-top border-right border-bottom btn btn-sm btn-default btn-circle"  data-toggle="modal" data-target=".bd-example-modal-lg" >
                    <a class="nav-link"  data-toggle="tooltip" data-placement="top" title="Search By Date Range">
                      <div>                       
                      <i class="fas fa-search text-primary font-weight-bold"></i>
                      <span></span>  
                       </div>
                        </a>
                     </button>
                     <button type="button"  runat=server id="Sp2" class="border-primary border-left border-top border-right border-bottom btn btn-sm btn-default btn-circle" data-toggle="modal" data-target="#exampleModal" >
                    <a class="nav-link"  data-toggle="tooltip" data-placement="top" title="Search By Activity Module">
                      <div>                       
                      <i class="fas fa-search-location text-primary font-weight-bold"></i>
                      <span></span>  
                       </div>
                        </a>
                  </button>
                </div>
              </div>
            </div>
                    <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                         
                <HeaderTemplate>
            <div class="table-responsive">
              <table class="table align-items-center table-sm small ">
                <thead class="thead-white">
                  <tr>
                
                 
                    <th scope="col">                     <asp:LinkButton ID="LinkButton1" runat="server" CommandName="time">TIME</asp:LinkButton></th>
</th>
                    <th scope="col" class="text-danger">ACTIVITY DETAIS</th>
                    <th scope="col">DESCRIPTION</th>
 


             
                  </tr>
                </thead>
                <tbody>
                                                                                        </HeaderTemplate>
                <ItemTemplate>
                  <tr>
                                        <td>
                    <%# Eval("Time")%>
                    </td>
                                                           <td >
                                                           <span>
<a title="Show the details" class=text-primary href="<%# Eval("Query")%>"><%# Eval("Module")%></a>
                                                           </span>
                    <span>
                    <h6>
                    <asp:Label ID="Label2" runat="server" Text=<%# Eval("ModuleDescription")%>></asp:Label>
                    </h6>
                    
                    </span>
                    <span>
                    <h6>
                    Issue: <asp:Label ID="Label3" runat="server" class="text-primary font-weight-bold" Text=<%# Eval("ForPerson")%>></asp:Label>
                    </h6>
                    
                    </span>
                    
                    </td>


                    <td>
                    <%# Eval("What")%>
                    <h6 class=" text-xs text-gray-900  font-weight-bold">
                    by <%# Eval("Who")%>
                    </h6>
                    
                    </td>
                  </tr>
                  
                                                                            </ItemTemplate>
                <FooterTemplate>
                </tbody>
              </table>
                                                                  </FooterTemplate>
                                                     
            </asp:Repeater>

            </div>
                        <div class="card-footer bg-white py-4">
            <nav aria-label="...">
                <ul class="pagination justify-content-end mb-0">
                             <br /> 
                   <td>  <asp:label id="Label1" runat="server" class="m-1 small text-primary"></asp:label></td>
                   <br /> 
                  <li class="page-item active">

                  <asp:Button ID="btnPrevious" class="btn btn-primary btn-sm btn-circle" runat="server" Text="<"  onclick="btnPrevious_Click"/>
                    
                  </li>

             
                                    <li class="page-item active">

                  <asp:Button ID="btnNext" class="btn btn-sm btn-primary btn-circle mx-2" runat="server" Text=">" onclick="btnNext_Click"/>
                    
                  </li>

                </ul>
              </nav>
            </div>
            <div class="card-footer py-4">
              <nav aria-label="...">
                
              </nav>
            </div>
          </div>
        </div>
      </div>
      <!-- Dark table -->
      <div class="row mt-5">

      </div>
</asp:Content>

