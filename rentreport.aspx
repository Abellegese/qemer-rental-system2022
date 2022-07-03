<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="rentreport.aspx.cs" Inherits="advtech.Finance.Accounta.rentreport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Rent Report</title>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container-fluid pr-3 pl-3">
      <!-- Table -->
            <div class="row">
                <div class="col">
        <div class="bg-white rounded-lg mb-1 ">
            <div class="card-header bg-white ">
                
                
                   <div class="row">
                 <div class="col-7 text-left ">
                  <a class="btn btn-light btn-circle mr-2" id="buttonback" href="rentstatus1.aspx" runat="server"  data-toggle="tooltip" data-placement="bottom" title="Back to Home">
             
                    <span class="fa fa-arrow-left text-danger"></span>
  
                </a>
                     <a class="btn-circle border-bottom border-top border-right border-danger border-left border- mr-2" id="A1" runat="server">

                         <span id="counter" runat="server">0</span>

                     </a>
                     <span class="badge badge-danger">Unpaid Customer(s)</span>
                </div>
                 <div class="col-5 text-right ">
                              <a class="btn btn-sm btn-circle mr-2" style="background-color:#a538ba" id="buttonback2" href="NoticeLetter.aspx" runat="server"  data-toggle="tooltip" data-placement="bottom" title="Generate Notice Letter">
             
                    <span class="fas fa-book-open text-white"></span>
  
                </a>
                <button name="b_print" onclick="printdiv('div_print');" type="button" title="Print" class="btn btn-circle btn-sm btn-danger" data-toggle="modal" data-target="#exampleModalCenter" >
                    <div>
                      <i class="fas fa-print text-white font-weight-bold"></i>
                   
                    </div>
                  </button>
      

                </div>
                   </div>

            </div>

              <div id="div_print"> 
            <div class="card-header text-black bg-white font-weight-bold">
                <center>
                    <h5 id="oname" runat="server" class="mb-1 text-gray-900 font-weight-bold "></h5>
<h5 class="mb-1 font-weight-bold text-gray-900 ">Current Period Rent</h5>
                   <span class="mb-2 text-gray-900 font-weight-bold ">As of<span class="h6 mx-1 mb-2 text-gray-900 font-weight-bold " id="mont" runat="server"></span></span>
                </center>
              
              <asp:Label ID="lblMsg" runat="server" ></asp:Label>
                            <div class="row align-items-center">
                                
                <div class="col-12 text-right">
                </div>
              </div>
            </div>
                      
              <div class="row">
      <div class="col-1">

      </div>
                   <div class="col-10">
     
<asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                       
                <HeaderTemplate>

              <table class="table small align-items-center table-sm">
                <thead >
                  <tr>
                
                  <th scope="col"  class="text-gray-900">Customer</th>
                    <th scope="col"  class="text-gray-900">Shop No.</th>
                   
                    <th scope="col"  class="text-gray-900">Rate(Monthly)</th>                      

                    <th scope="col"   class="text-gray-900 border-right  border-right">VAT(15%)</th>  
                    

                      <th scope="col"  class="text-gray-900">Due Date</th>
                      <th scope="col"  >Status</th>            
 <th scope="col"  class="text-gray-900 text-right border-right">Total</th>
             
                  </tr>
                </thead>
                <tbody>
                                                                                        </HeaderTemplate>
                <ItemTemplate>
                  <tr>


                    <td class="text-primary text-left">
                    <a  title="Show customer details" data-toggle="tooltip" data-placement="bottom" href="CustomerDetails.aspx?ref2=<%# Eval("customer")%>""> CN#-<asp:Label ID="Label2" runat="server" Text=<%# Eval("customer")%>></asp:Label></a>
                    </td>
                     <td class="text-gray-900">
                    <%# Eval("shopno")%>
                    </td>

                    <td class="text-gray-900">
                        <asp:Label ID="lblPrice" runat="server" Text=<%# Eval("price", "{0:N2}")%>></asp:Label>
                    
                    </td> <asp:Label ID="lblServiceCharge" Visible="false" runat="server" Text=<%# Eval("servicecharge", "{0:N2}")%>></asp:Label>                                             
                                     
                    <td class="text-gray-900 border-right ">
                        <asp:Label ID="lblVAT" runat="server"> </asp:Label>

                    </td>
    

                     <td class="text-gray-900">
                         <asp:Label ID="lblDueDate" class=" font-weight-bold" runat="server" Text=<%# Eval("duedate","{0: dd/MM/yyyy}")%>></asp:Label>
                    
                    </td>
                      <td >
                       <asp:Label ID="lblDueStatus" class=" font-weight-bold badge badge-warning" runat="server"></asp:Label>
                       <asp:Label ID="lblFine" class=" font-weight-bold badge badge-success" runat="server"></asp:Label>
                          <asp:Label ID="lblpassed" class=" font-weight-bold badge badge-danger" runat="server"></asp:Label>
                    </td>
                                              <td class="text-gray-900 border-right text-right">
                          <asp:Label ID="lblTotal" runat="server" Text=<%# Eval("currentperiodue", "{0:N2}")%>></asp:Label>
                    
                    </td>              
                  </tr>
                  
                                                                            </ItemTemplate>
                <FooterTemplate>
                </tbody>
              </table>
            
                                                                 
                        </FooterTemplate>                              
            </asp:Repeater>
                           <div class="row border-top" id="VatTable" runat="server">
                               <div class="col-7">

                               </div>
                             <div class="col-5">
                                                                             <div class="form-group">
                                                <table class="table table-sm table-bordered ">
                                                    <tbody>
                               
                          
                                                             <tr>
                                                            <td><span style="margin: 7px 5px 5px 5px; padding: 5px" class="m-0 font-weight-bold text-gray-900 text-right ">Grand Total:</span></td>
                                                            <td class="text-right"> <span id="txtTotal" class="text-gray-900 font-weight-bold" runat=server></span></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
 
                                                 



                                             </div>
                               </div>
                           </div>
                  
                                           <center>
                                                                  <main class="mt-4" role="main" id="main2" runat="server">

      <div class="starter-template">
          
        
        <p class="lead">
            <span class="fas fa-donate text-gray-400 fa-4x"></span></p>
      </div>
         <span class="text-gray-400 text-center small" style="font-weight: bold">No Proccessed Rent Created Yet!!.</span>
          


    </main>
            </center>           
              
  
           

 
            </div>
                        <div class="col-1">

      </div>
                  
                  
              </div>   </div>
<div class="card-footer bg-white">
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
            
       
     
 </div>
           </div>
            </div>

        </div>
</asp:Content>
