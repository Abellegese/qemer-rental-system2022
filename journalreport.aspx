<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="journalreport.aspx.cs" Inherits="advtech.Finance.Accounta.journalreport" %>
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
    <title>Journal Report</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid pl-3 pr-3">
      <!-- Table -->
      <div class="row">
        <div class="col">
                       <div class="bg-white rounded-lg mb-1 ">
            <div class="card-header bg-white ">
                
                
                   <div class="row">
                 <div class="col-4 text-left ">
                           <a href="Home.aspx" class="btn btn-default btn-sm">
                  <i class="fas fa-arrow-left  font-weight-bold text-primary"></i><span id="account" class="font-weight-bold text-primary mx-3" runat="server"></span>
              </a>

      

                </div>
                 <div class="col-8 text-right ">

                <button name="b_print" onclick="printdiv('div_print');" type="button" title="Print" class="mx-1 border-primary border-left border-top border-right border-bottom btn btn-sm btn-default btn-circle" data-toggle="modal" data-target="#exampleModalCenter" >
                    <div>
                      <i class="fas fa-print text-primary font-weight-bold"></i>
                   
                    </div>
                  </button>
      

                </div>
                   </div>

            </div>
           
            <div id="div_print">

                        <div class="card-header text-black bg-white font-weight-bold">
                <center>
                    <h5 id="oname" runat="server" class="mb-1 text-gray-900 font-weight-bold "></h5>
<h5 class="mb-1 h4 text-uppercase text-gray-600 ">Journal Report</h5>
                   <span class="mb-2 text-gray-900 font-weight-bold ">As of<span class="h6 mx-1 mb-2 text-gray-900 font-weight-bold " id="montvb" runat="server"></span></span>
                </center>
              
              <asp:Label ID="lblMsg" runat="server" ></asp:Label>
                            <div class="row align-items-center">
                                
                <div class="col-12 text-right">
                </div>
              </div>
            </div>
                          <div class="row">
                              <div class="col-2">

                              </div>
                                <div class="col-8 small">
<asp:Repeater ID="Repeater1" runat="server">
                         
                <HeaderTemplate>
            <div class="table-responsive">

<table class="table align-items-center table-sm " style="empty-cells: hide">
                <thead >
                  <tr>
                 <th scope="col" class="text-left" >Date </th>
                  <th scope="col" class="text-center" style="color: #FF9900">Account</th>
                  <th scope="col" class="text-center">Explanation</th>

                    <th scope="col">Debit</th>
                    <th scope="col" class=text-center>Credit</th>
                   
          
                   
                  </tr>
                </thead>
                <tbody>
                                                                                        </HeaderTemplate>
                <ItemTemplate>
                  <tr >                                        <td>
                      <asp:Label ID="Label7" runat="server" Text=<%# Eval("Date", "{0: MMM dd, yyyy}")%>></asp:Label>
                    
                    </td>
                            <th  style="color: #0000CC">
                            <a title="Show the details" href="LedgerDetail.aspx?led=<%# Eval("Account1")%>"" style="text-align: left;">
                                <asp:Label ID="Label6" title="Debited Account" runat="server" Text=<%# Eval("Account1")%> ForeColor="#009900"></asp:Label>
                                                </a>
                                                <a title="Show the details" href="LedgerDetail.aspx?led=<%# Eval("Account2")%>""  style="text-align: right;">
<asp:Label ID="Label5" title="Credited Account" runat="server" Text=<%# Eval("Account2")%> ForeColor="DarkRed"></asp:Label>
</a>

                    
                            </th>     
                                       

                    <td style="vertical-align: bottom" class="text-center">
                              <asp:Label ID="Label8" runat="server" Text=<%# Eval("Explanation")%>></asp:Label>
                    </td>
          
                                        
                   




                                               <th>
                                                   <asp:Label ID="Label2" runat="server" Text= <%# Eval("Debit", "{0:N2}")%>></asp:Label>
                   
                    </th>

                                        <th>
                                            <asp:Label ID="Label3" runat="server" Text=<%# Eval("Credit", "{0:N2}")%>></asp:Label>
                    
                    </th>
</tr>
                                                                            </ItemTemplate>
                <FooterTemplate>
                </tbody>
              </table>
                                                                  </FooterTemplate>
                                                     
            </asp:Repeater>
                              </div>
                                <div class="col-2">

                              </div>
                          </div>
                    

        
            </div>
                 </div>      

                        <div class="card-footer py-4">
              <nav aria-label="...">
                <ul class="pagination justify-content-end mb-0">
                        
                  <li class="page-item active">

                  <asp:Button ID="btnPrevious" class="page-link" runat="server" Text="<"  onclick="btnPrevious_Click"/>
                    
                  </li>

                  <br /> 
                   <td>  <asp:label id="Label1" runat="server" class="all"></asp:label></td>
                   <br /> 
                                    <li class="page-item active">

                  <asp:Button ID="btnNext" class="page-link" runat="server" Text=">" onclick="btnNext_Click"/>
                    
                  </li>

                </ul>
              </nav>
            </div>
            </div>
          </div>
            </div>
</asp:Content>
