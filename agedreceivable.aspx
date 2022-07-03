<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="agedreceivable.aspx.cs" Inherits="advtech.Finance.Accounta.agedreceivable" %>
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
    <title>Aged Receivable Report</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid pr-3 pl-3">
      <!-- Table -->
        
         <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-sm" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title text-gray-900" id="exampleModalLabel">Type Customer Name</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        <div class="row">
            <div class="col-md-7">
                <asp:TextBox ID="txtCustomerName1"  class="form-control mx-2" runat="server"></asp:TextBox>

            </div>

                        <div class="col-md-5">
                            <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="search.." OnClick="Button2_Click"/>

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
                                        <div class="bg-white rounded-lg mb-1 ">
            <div class="card-header bg-white ">
                
                
                   <div class="row">
                 <div class="col-7 text-left ">
                           <a href="Home.aspx" class="btn btn-light btn-circle">
                  <i class="fas fa-arrow-left  font-weight-bold text-primary"></i>
              </a>

      

                </div>
                 <div class="col-5 text-right ">

                                                             <button type="button"  runat=server id="Sp2" class=" btn btn-sm btn-warning btn-circle mr-2" data-toggle="modal" data-target="#exampleModal" >
                    <div>
                      <i class="fas fa-user-check text-white font-weight-bold"></i>
                   <span></span>
                    </div>
                  </button>
                                       <a class="btn btn-circle btn-primary btn-sm mr-2  " href="LedgerDetail.aspx?led=Accounts Receivable"  data-toggle="tooltip" data-placement="bottom" title="See Receivable Ledger">
            
                               <span id="Span2" runat="server"  class=" fas fa-dollar-sign"></span>
                            </a>
                <button name="b_print" onclick="printdiv('div_print');" type="button" title="Print" class="btn btn-sm btn-success btn-circle" data-toggle="modal" data-target="#exampleModalCenter" >
                    <div>
                      <i class="fas fa-print text-white font-weight-bold"></i>
                   
                    </div>
                  </button>
      

                </div>
                   </div>

            </div>
           
        <div id="div_print"> 

            
              <div class="row small mt-5">
                  <div class="col-2">

                  </div>
                   <div class="col-8">
                                                                                               <div class="row " style="height:90px">
                    <div class="col-md-6 text-left">
                                 <img class="" src="../../asset/Brand/gh.jpg" alt="" width="110" height="80">
                        <br />
                              <h5 id="oname" runat="server" class="mb-1 border-top border-dark text-gray-900 font-weight-bold "></h5>
                    </div>
                <div class="col-md-6 text-right ">

            <h4 class="text-gray-900 font-weight-bold">AGED RECEIVABLE</h4>

                   <span class="mb-2 text-gray-900 font-weight-bold "><span class="h6 mx-1 mb-2 text-gray-900 font-weight-bold " id="mont" runat="server"></span></span>
              </div>
                
                </div>
                                    <br />
                                    <br />
                                           <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                         
                <HeaderTemplate>
          
<table class="table align-items-center table-sm">
                <thead>
                  <tr class="mx-2">

                  <th >Customer</th>
                      <th class="text-right text-gray-900 border-left border-right" title="In this column you can track the opening balance of the customer. If they paid in credit note module it will reduce to zero. Until it is paid it will be recorded in one of three columns depending on its age started from the recorded date in the system. It won't summed in the total since it is added to one of the three columns." data-toggle="tooltip" data-placement="bottom">Opening</th>
                    
                    <th class="text-right text-gray-900">1-30 Days</th>
                      <th class="text-right text-gray-900">31-60 Days</th>
                      <th class="text-right text-gray-900 border-right">61-90 Days</th>
                      <th class="text-right text-gray-900 border-right"> > 90 Days</th>
                      <th class="text-right text-gray-900 text-danger border-right">TOTAL</th>
                  </tr>

                </thead>
                <tbody>
                    
                                                                                        </HeaderTemplate>
                <ItemTemplate>
                  <tr class="mx-2">
                  <tr >
                     <td class="text-primary font-weight-normal">                   
                    <a href="CustomerStatement.aspx?ref=<%# Eval("customer")%>"><asp:Label ID="lblCustomer" runat="server" Text=<%# Eval("customer")%>></asp:Label></a>
                    </td>
                                                <td class="text-right  border-left border-right text-gray-900" >
                          <asp:Label ID="lblOpening" runat="server" Text="0.00"></asp:Label>
                       </td>
        
                          <td class="text-right text-gray-900">
                          <asp:Label ID="lblone" runat="server" Text="0.00"></asp:Label>
                       </td>
                         <td class="text-right text-gray-900">
                          <asp:Label ID="lbltwo" runat="server" Text="0.00"></asp:Label>
                       </td>
                     
                        <td class="text-right text-gray-900 border-right">
                          <asp:Label ID="lblthree" runat="server" Text="0.00"></asp:Label>
                       </td>                         
                      <td class="text-right border-right text-gray-900">
                          <asp:Label ID="lblfour" runat="server" Text="0.00"></asp:Label>
                       </td>
                         <td class="text-right border-right text-gray-900">
                          <asp:Label ID="lblTotal" runat="server" Text="0.00"></asp:Label>
                       </td>

                  </tr>
                                                                            </ItemTemplate>
                <FooterTemplate>
                </tbody>
              </table>
                                                                  </FooterTemplate>
                                                     
            </asp:Repeater>
                       <div class="row border-top">
                       <div class="col-md-7">

                       </div>
                          <div class="col-md-5">
                                                                          <div class="form-group">
                                                <table class="table table-sm table-bordered ">
                                                    <tbody>
                                                        <tr>
                                                            <td><span style="margin: 7px 5px 5px 5px; padding: 5px" class="m-0 font-weight-bold text-right text-gray-900">Grand Total</span></td>
                                                            <td class="text-right"><span id="GrandTotal" class="text-gray-900 font-weight-bold text-gray-900" runat=server></span></td>
                                                        </tr>
         
                                                    </tbody>
                                                </table>
 
                                                 



                                             </div>
                       </div>
                   </div>
              <div class="row mt-lg-5">
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
</asp:Content>
