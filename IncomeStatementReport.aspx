<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.master" AutoEventWireup="true" CodeBehind="IncomeStatementReport.aspx.cs" Inherits="advtech.Finance.Accounta.IncomeStatementReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
    <title>Income Statement</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="container-fluid ">
    <div class="modal fade" id="exampleModal2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel2" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title text-gray-900 font-weight-bold" id="exampleModalLabel2"><span id="numebr" class="invisible" runat="server"></span>Send Report PDF - Email</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
               <div class="form-group">
            <label for="recipient-name" class="col-form-label" >Email:</label>
              <asp:TextBox ID="txtEmail" class="form-control form-control-sm" runat="server" style="border-color:#00ff21"></asp:TextBox>
          </div>
          <div class="form-group">
            <label for="recipient-name" class="col-form-label" >Subject:</label>
              <asp:TextBox ID="txtSubjuct" class="form-control form-control-sm" runat="server" style="border-color:#00ff21"></asp:TextBox>
          </div>
          <div class="form-group">
            <label for="message-text" class="col-form-label">Message:</label>
              <asp:TextBox ID="txtBody" runat="server" CssClass="form-control form-control-sm" TextMode="MultiLine" placeholder="Discussion"></asp:TextBox>

          </div>

      </div>
      <div class="modal-footer">
          <button type="button" class="btn btn-secondary btn-sm" data-dismiss="modal">Close</button>
          <asp:Button ID="btnSendMessage" class="btn btn-primary btn-sm" OnClick="btnSendMessage_Click" runat="server" Text="Send Email" />
        
  
      </div>
    </div>
  </div>
</div>
    <div class="modal fade" id="exampleModalLongService" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle3" aria-hidden="true">
  <div class="modal-dialog modal-sm" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title h6 font-weight-bold text-gray-900" id="exampleModalLongTitle3">Export as Excel</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">

                                           <div class="row">
                        <div class="col-12">
                      <div class="form-group">
                          </div>

                            </div>
                                               </div>
                                 <div class="row">
                        <div class="col-12">
                      <div class="form-group">
                          <center>
      <asp:Button ID="btnUncollected"  class="btn btn-danger w-50" runat="server" Text="Export" OnClick="btnUncollected_Click"/>
                          </center>

 
                      
                      </div>
                    </div>
                    </div>

    
  </div>

</div>
</div>
</div>
             <div class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
        <h5 class="modal-title" id="H1">Filter Record</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
                                    <div class="row" >

                                              <div class="col-6">
                                            <div class="form-group">
                                                <label class=font-weight-bold>From<span class=text-danger>*</span></label>
    
                                            
                                                 <br />
                                                           <div class="form-group mb-0">
            <div class="input-group input-group-alternative">
            <asp:TextBox ID="txtDatefrom" class="form-control " TextMode=Date  runat="server"></asp:TextBox>

              
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
            <asp:TextBox ID="txtDateto" class="form-control " TextMode=Date  runat="server"></asp:TextBox>

            </div>
          </div>
          </div>
          </div>

                                                 </div>  
      </div>
      <div class="modal-footer">
      <center>
        <button type="button" class="btn btn-secondary btn-sm " data-dismiss="modal">Close</button>
                                            <asp:Button ID="btnUpdate"   class="btn btn-sm btn-primary" Text="Search" OnClick="btnUpdate_Click"   runat="server" />
                                        </div>
     </center>
      </div>
    </div>
  </div>
                                        <div class="card shadow-none mb-1 card">
            <div class="card-header bg-white ">
                
                
                   <div class="row">
                 <div class="col-5 text-left ">
                           <a href="Home.aspx" class="btn btn-default btn-sm">
                  <i class="fas fa-arrow-left  font-weight-bold text-primary"></i><span id="Span1" class="font-weight-bold text-primary mx-3" runat="server"></span>
              </a>

      

                </div>
                 <div class="col-7 text-right ">
                                                                                              <button runat="server" id="Button1" type="button" class="btn  mr-2 btn-sm  btn-light" data-toggle="modal" data-target="#exampleModal2" >
                                        <a class="btn-light btn btn-sm"  data-toggle="tooltip" data-placement="bottom" title="Send Email">
                              <div>
                      <i class="fas fa-envelope text-gray-900"></i>
                      
                    </div>
</a>
                  </button>
                                          <button runat="server" id="modalMain" type="button" class="btn btn-circle mr-2 btn-sm text-xs btn-danger" data-toggle="modal" data-target="#exampleModalLongService" >
                                        <a class="nav-link btn btn-sm"  data-toggle="tooltip" data-placement="bottom" title="Export as Excel">
                              <div>
                      <i class="fas fa-file-excel text-white"></i>
                      
                    </div>
                        </a>
                  </button>
                  <button name="b_print" onclick="printdiv('div_print');" type="button" title="Print" class="border-primary border-left border-top border-right border-bottom btn btn-sm btn-default btn-circle" data-toggle="modal" data-target="#exampleModalCenter" >
                    <div>
                      <i class="fas fa-print text-primary font-weight-bold"></i>
                   
                    </div>
                  </button>
                        <button type="button"       runat=server id="Sp2" class=" mx-2 border-primary border-left border-top border-right border-bottom btn btn-sm btn-default btn-circle" data-toggle="modal" data-target=".bd-example-modal-lg" >
                    <div>
                      <i class="fas fa-search text-primary font-weight-bold"></i>
                   <span></span>
                    </div>
                  </button>

                </div>
                   </div>

            </div>
            
        <div id="div_print">

            <div id="con" runat="server">


            <div class="card-header text-black bg-white font-weight-bold">
                <center>
                    <h5 id="oname" runat="server" class=" mb-1 font-weight-bold text-gray-900"></h5>
<h4 class="mb-1 font-weight-bold text-gray-900">Income Statement</h4>
                    <span id="datFrom1" runat="server" class="mb-1 text-gray-900 font-weight-bold"></span><span id="tomiddle" class="mb-1 mr-2 ml-2 text-gray-900 font-weight-bold" runat="server">To</span><span id="datTo" class="mb-1 text-gray-900 font-weight-bold" runat="server"></span>
                    <h6 class="mb-1 text-gray-900 font-weight-bold " id="mont" runat="server"></h6>

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
                     <div class="col-8">
                                             <asp:Repeater ID="Repeater1" runat="server" >
                         
                <HeaderTemplate>
           
<table class="table align-items-center table-sm">
                <thead>
                  <tr>
                  <th >ACCOUNT</th>
                    <th class="text-right">BALANCE</th>
                  </tr>
                    <tr>
                        <td class="font-weight-bold">Revenues</td>
                    </tr>
                </thead>
                <tbody>
                   </HeaderTemplate>
                <ItemTemplate>
                  <tr>
                                        <td class="text-primary font-weight-normal">
                    <asp:Label ID="Label1" runat="server" Text=<%# Eval("Account")%>></asp:Label>
                    </td>
                       <td class="text-right text-gray-900"><asp:Label ID="Label3" runat="server" Text=<%# Eval("Balance", "{0:N2}")%>></asp:Label></td>
                  </tr>
                     </ItemTemplate>
                <FooterTemplate>
                </tbody>
              </table>
               </FooterTemplate>             
            </asp:Repeater>
                                                <table class="table align-items-center table-sm">
                <thead>

                    <tr>
                        <td class="font-weight-bold small text-gray-900 text-uppercase">Total Revenues</td> 
                        <td>

                        </td>

                        <td>
        <h6 id="TotRevenue" class="text-gray-900 text-lg-right font-weight-bold" runat="server" Text="0.00"></h6></td>
                    </tr>

                </thead>
                <tbody>
                    <tr>
                        <td></td>
                    </tr>
     
<tr>



</tr>
                </tbody>
              </table>
                    <asp:Repeater ID="Repeater3" runat="server" >
                         
                <HeaderTemplate>
            
<table class="table align-items-center table-sm">
                <thead>
                    <tr>
                        <td class="font-weight-bold">Expense</td>
                    </tr>
                </thead>
                <tbody>
                 </HeaderTemplate>
                <ItemTemplate>
                  <tr>

                    <td class="text-primary font-weight-normal">
                    <asp:Label ID="Label4" runat="server" Text=<%# Eval("Account")%>></asp:Label>
                    </td>
                       <td class="text-right  text-gray-900">
                           <asp:Label ID="Label5" runat="server" Text=<%# Eval("Balance", "{0:N2}")%>></asp:Label></td>
                  </tr>
                   </ItemTemplate>
                <FooterTemplate>
                </tbody>
              </table>
               </FooterTemplate>
                                                     
            </asp:Repeater>
                                                <table class="table align-items-center table-sm">
                <thead>

                    <tr>
                        <td class="font-weight-bold text-gray-900 small text-uppercase">Total Expense</td> 
                        <td>

                        </td>

                        <td>
        <h6 id="TotExpense" class="text-gray-900 text-lg-right font-weight-bold" runat="server" Text="0.00"></h6></td>
                    </tr>

                </thead>
                <tbody>
                    <tr>
                        <td></td>
                    </tr>
     
<tr>



</tr>
                </tbody>
              </table>
                    <table class="table align-items-center table-sm">
                <thead>

                    <tr>
                        <td><span id="NetText"  runat="server"></span></td> 
                        <td>

                        </td>

                        <td>
        <h6 id="Label2" class="text-dark text-lg-right font-weight-bold" runat="server" Text="0.00"></h6></td>
                    </tr>

                </thead>
                <tbody>
                    <tr>
                        <td></td>
                    </tr>
     
<tr>



</tr>
                </tbody>
              </table>  

                  </div>
                    <div class="col-2">

                  </div>
              </div>
                   
      
            </div>
          
            </div>
</div>
          </div>

</asp:Content>

