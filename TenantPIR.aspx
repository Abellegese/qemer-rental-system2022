<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="TenantPIR.aspx.cs" Inherits="advtech.Finance.Accounta.TenantPIR" %>
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
        <style>
        .water{
            content:'Raksym Trading PLC';
            align-content:center;
            justify-content:center;
            opacity:0.2;
            z-index:-1;
            transform:rotate(-45deg);
        }
    </style>
    <title>Tenant PIR</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid pr-3 pl-3 ">
      <!-- Table -->

        
          <div class="bg-white rounded-lg mb-4">

            <div class="card-header text-black bg-white font-weight-bold">
                <div class="row mb-3 ">
                 <div class="col-7 text-left ">
                                    <a class="btn btn-light btn-circle mr-2" id="buttonback" runat="server" href="Customer.aspx" data-toggle="tooltip" data-placement="bottom" title="Back to customer page">
             
                    <span class="fa fa-arrow-left text-danger"></span>
  
                </a>
                </div>
                 <div class="col-5 text-right ">
            
            <button type="button" name="b_print" onclick="printdiv('div_print');" data-toggle="tooltip" data-placement="bottom" title="Print" class="btn btn-circle btn-light date">
                                         <span class="fa fa-print"></span>
                                        </button>
  
                </div>
                   </div>
              

              <asp:Label ID="lblMsg" runat="server" ></asp:Label>
                            <div class="row align-items-center border-top border-bottom mb-4">
                                
                <div class="col-12 text-right">
                </div>
              </div>
              <div id="div_print"> 
              <div class="row mb-3">
                  <div class="col-2">

                  </div>
                  <div class="col-8 ">

                       <div class="row border-bottom mt-3">                        
                                              <div class="col-md-6 text-left">
               
                                     <img class="rounded-circle" src="../../asset/Brand/images22.png" alt="" height="120" width="130">
                                                  <br />
                                                  <span class="fas fa-user-check text-gray-400 mr-2"></span><span class="mx-3 h6 mt-2 text-gray-900  font-weight-bold mb-1" id="fullname" runat="server"></span>[<span class="text-gray-900 font-weight-bold mb-1 h6 font-italic" id="shopno" runat="server"></span>]
                                                  <span class="small  text-gray-900" id="Status1" runat="server"></span>
               
                        </div>                       
                                              <div class="col-md-6 text-right">
                         <img class="mb-2" src="../../asset/Brand/gh.jpg" alt="" width="110" height="80">
                            <h6 translate="no" class="h5 text-gray-900 border-top border-dark font-weight-bold ">RAKSYM TRADING PLC</h6>
                        </div>


                    </div>                  
                      <span class="fas fa-info mt-4 btn-circle mr-2 btn-md border-bottom border-right border-left border-top border-danger fa-1x text-danger"></span>
                      <span class="h6 font-weight-bold border-top border-bottom text-danger">GENERAL INFO</span> 
                      <div class="row mt-4">
                        
                                      
       
                          <div class="col-md-6 text-left">
            

                       
                           
                              <span class="fas fa-bezier-curve mr-2 text-gray-400"></span><span class="small text-gray-900 mr-4"">Buissness Type:</span><span class="small text-gray-900" id="Bussnesstype" runat="server"></span>
                            <br />
                              <span class="fas fa-building mr-2 text-gray-400 mt-3 text-center"></span><span class="small  text-gray-900 mr-4"">Company Name:</span><span class="small  text-gray-900" id="comanyname" runat="server"></span>
                             <br /><span class="fas fa-mail-bulk mr-2 text-gray-400 mt-3 text-center"></span><span class="small text-gray-900 mr-4"">Email Address:</span><span class="small text-gray-900" id="emailaddress" runat="server"></span>
                              
                            <br /><span class="fas fa-phone mr-2 text-gray-400 mt-3 text-center"></span><span class="small  text-gray-900 mr-4"">Work Phone:</span><span class="small  text-gray-900" id="workphone" runat="server"></span>
                            <br /><span class="fas fa-mobile mr-2 text-gray-400 mt-3 text-center"></span><span class="small text-gray-900 mr-4"">Mobile:</span> <span class="small text-gray-900" id="mobile" runat="server"></span>
                            <br /><span class="fas fa-link mr-2 text-gray-400 mt-3 text-center"></span><span class="small  text-gray-900 mr-4"">Websites:</span><span class="small  text-gray-900" id="website" runat="server"></span>
                                                        <br /><span class="fas fa-address-book mr-2 text-gray-400 mt-3 text-center"></span><span class="small  text-gray-900 mr-4"">Address:</span><span class="small  text-gray-900" id="custAddress" runat="server"></span>
                            <br /><span class="fas fa-hashtag mr-2 text-gray-400 mt-3 text-center"></span><span class="small  text-gray-900 mr-4"">TIN#:</span><span class="small  text-gray-900" id="TINNumber" runat="server"></span>



                          </div>

                          <div class="col-md-6 text-left border-left">

            <span class="fas fa-tag mr-2 text-gray-400 mt-3 text-center"></span><span class="small  text-gray-900 mr-4"">Billing Terms:</span><span class="small  text-gray-900" id="billingterms" runat="server"></span>
          <br />
                            <span class="fas fa-location-arrow mr-2 text-gray-400 mt-3 text-center"></span><span class="small  text-gray-900 mr-4"">Location:</span><span class="small  text-gray-900" id="location" runat="server"></span>
                            <br /><span class="fas fa-map-marked mr-2 text-gray-400 mt-3 text-center"></span><span class="small  text-gray-900 mr-4"">Area(m<sup>2</sup>):</span><span class="small  text-gray-900" id="area" runat="server"></span>
                            <br /><span class="fas fa-dollar-sign mr-2 text-gray-400 mt-3 text-center"></span><span class="small  text-gray-900 mr-4"">Price(ETB/m<sup>2</sup>*Monthly):</span><span class="small  text-gray-900" id="price" runat="server"></span>
                              
                            <br /><span class="fas fa-calendar-check mr-2 text-gray-400 mt-3 text-center"></span><span class="small text-gray-900 mr-4"">Date of Joining:</span><span class="small text-gray-900" id="dateofj" runat="server"></span>
                            <br /><span class="fas fa-calendar-minus mr-2 text-gray-400 mt-3 text-center"></span><span class="small  text-gray-900 mr-4"">Agreement Date:</span><span class="small  text-gray-900" id="agrredate" runat="server"></span>
                               <br /><span class="fas fa-hand-holding-usd mr-2 text-gray-400 mt-3 text-center"></span><span class="small  text-gray-900 mr-4"">Contingency:</span><span class="small  text-gray-900" id="contingency" runat="server"></span>
                            
                          


                          </div>
                      </div>
                                            <span class="fas fa-chart-line mt-4 btn-circle mr-2 btn-md border-bottom border-right border-left border-top border-danger fa-1x text-danger"></span>
                      <span class="h6 font-weight-bold border-top border-bottom">TRANSACTION AND GURANTOR INFO</span> 
                      <div class="row">
                          <div class="col-md-6">
                              <span class="fas fa-location-arrow mr-2 text-gray-400 mt-3 text-center"></span><span class="small  text-gray-900 mr-4"">INVOICE:</span><span class="small  text-gray-900" id="paidinv" runat="server"></span>
                            <br /><span class="fas fa-map-marked mr-2 text-gray-400 mt-3 text-center"></span><span class="small  text-gray-900 mr-4"">PAYMENT:</span><span class="small  text-gray-900" id="unpaidinv" runat="server"></span>
                            <br /><span class="fas fa-hand-holding-usd mr-2 text-gray-400 mt-3 text-center"></span><span class="small  text-gray-900 mr-4"">BALANCE:</span><span class="small  text-gray-900" id="BalancePay" runat="server"></span>

                          </div>
                          <div class="col-md-6 border-left">
                                                        
                            <span class="small  text-gray-900 mr-4"">Gurantor Full Name:</span><span class="small  text-gray-900" id="gurantorfulln" runat="server"></span>
                            <br /><span class="fas fa-address-book mr-2 text-gray-400 mt-3 text-center"></span><span class="small  text-gray-900 mr-4"">Address:</span><span class="small  text-gray-900" id="addressgurantor" runat="server"></span>
                            <br /><span class="fas fa-mobile-alt mr-2 text-gray-400 mt-3 text-center"></span><span class="small  text-gray-900 mr-4"">Contact:</span><span class="small  text-gray-900" id="contact3" runat="server"></span>

                          </div>
                      </div>
                      <span class="fas mt-4 mb-4 fa-money-bill-wave btn-circle mr-2 btn-md border-bottom border-right border-left border-top border-danger fa-1x text-danger"></span>
                      <span class="h6 font-weight-bold">CREDIT INFO</span>
                      <div class="row">
                          <div class="col-12">
                              <div id="CreditDiv" runat="server">

                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound"  >
                         
                  
                <HeaderTemplate>
                
              <table class="table align-items-center table-sm small  ">

                <thead class=" thead-dark">
                  <tr>
                
              
                    <th scope="col" class="text-left ">Date</th>
                    <th scope="col" class="text-left">CN#</th>
                    <th scope="col" class="text-left">Amount</th>
                    <th scope="col" class="text-right">Days Overdue</th>

                  </tr>
                </thead>

                <tbody>
                 </HeaderTemplate>
                <ItemTemplate>
                  <tr >


                    <td class="text-left text-gray-900">
        <asp:Label ID="lblDueDate" runat="server" Text=<%# Eval("date","{0:MMMM dd, yyyy}")%>></asp:Label>
                    
                    </td>
                                        <td class="text-left text-gray-900">
                    <%# Eval("id")%>
                    </td>

                                        <td class="text-left text-gray-900">
                    <%# Eval("balance", "{0:N2}")%>
                    </td>
                     <td class="text-right">
                      <asp:Label ID="lblAged" runat="server"></asp:Label></td>


                  </tr>
                 </ItemTemplate>
                <FooterTemplate>
                </tbody>
              </table>
            
              <hr class="text-gray-700 font-weight-bold" />
                  </FooterTemplate>
                                                     
           </asp:Repeater>
                              </div>
                              <div id="CreditDivNone" runat="server">
                                  <center>
                                  <span class="fas fa-dollar-sign text-gray-400 fa-2x"></span>
                                  <br />
                                  <span  class="text-gray-400 small mt-1">No Credits</span>
                                  </center>
                              </div>
                          </div>
                      </div>
                       <span class="fas mt-4 mb-4 fa-exclamation-circle btn-circle mr-2 btn-md border-bottom border-right border-left border-top border-warning fa-1x text-warning"></span>
                      <span class="h6 font-weight-bold">DELINQUENCY</span>
                      <div class="row">
                          <div class="col-12">
                                              <div class="card-body">
                  <div class="row no-gutters ">

                    <div class="col mr-2">
  
                                <div class="small mb-0 font-weight-light  text-gray-800"><span id="Span7" runat="server"></span>                                                                      
                 </div></div></div>
                                   <div class="row">
                                       <div class="col-12">
                                       <div id="DelinquanceyDiv" runat="server">
                                         <asp:Repeater ID="Repeater4" runat="server">
                  <ItemTemplate>
           <div class="col-9">
<span class="small text-primary">#<%#Eval("id")%></span><span class="text-gray-900 mx-2 small"><%#Eval("delinquency")%></span>
           </div>
                                        



                    <div class="col-3 text-right">
                    <span class="text-xs fas fa-calendar-check text-gray-300"></span><span class="text-xs mx-1 text-primary"><%#Eval("datetime","{0: MMMM dd, yyyy}")%></span>
                      
                    </div><hr />
     
              </ItemTemplate>
                </asp:Repeater>
                                       </div>
                                <div id="DelinNone" runat="server">
                                  <center>
                                  <span class="fas fa-exclamation-circle text-gray-400 fa-2x"></span>
                                  <br />
                                  <span  class="text-gray-400 small mt-1">No Delinquency</span>
                                  </center>
                              </div></div>
                                   </div>                              
                      
                 

          </div>
                          </div>
                      </div>
              <div class="row mt-5">
                                             <div class="col-12 border-top border-bottom">
                               <div class="row">
                                   <div class="col-6 text-left">
                                     <span class="fas fa-address-book text-gray-400 mr-2"></span><span class="mb-2 text-gray-900 small " id="addressname" runat="server"></span>

                                   </div>
                                   <div class="col-6 text-right">
                                  <span class="fas fa-phone text-gray-400 mr-2"></span><span class="mb-2 text-gray-900 small " id="phone" runat="server"></span>


                                   </div>
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
    </asp:Content>