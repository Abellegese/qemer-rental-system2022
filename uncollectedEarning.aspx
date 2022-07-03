<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="uncollectedEarning.aspx.cs" Inherits="advtech.Finance.Accounta.uncollectedEarning" %>
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
<title>Unearned Revenue</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid pr-3 pl-3">
        <div class="modal fade bd-example-modal-lg" id="daterange" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
        <h5 class="modal-title" id="H1">Filter By Date Range</h5>
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
            <asp:TextBox ID="txtDateform" class="form-control " TextMode="Date" runat="server"></asp:TextBox>
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
            <asp:TextBox ID="txtDateto" class="form-control " TextMode="Date"  runat="server"></asp:TextBox>
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
                <div class="modal fade" id="exampleModal11" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel11" aria-hidden="true">
  <div class="modal-dialog modal-sm" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <span class="modal-title small text-gray-900" id="exampleModalLabel11">Filter by balance(condtion)</span>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        <div class="row mb-3">
            <div class="col-md-12">
                  <div class="custom-control custom-radio custom-control-inline">
                                               
  <input type="radio" id="greater" name="customRadioInline1" class="custom-control-input" checked="true" runat=server clientidmode="Static"/>
  <label class="custom-control-label text-gray-900  " for="greater">ETB ></label>
</div>
                    <div class="custom-control custom-radio custom-control-inline">
  <input type="radio" id="less" name="customRadioInline1" class="custom-control-input" runat=server clientidmode="Static"/>
  <label class="custom-control-label font-weight-200  text-gray-900 " for="less"><</label>
</div>

            </div>
</div>
                    <div class="row">
                        <div class="col-md-12 mb-3">
                            <asp:TextBox ID="txtFilteredAmount" runat="server" class="form-control"></asp:TextBox>


            </div>
        </div>
          <div class="row">
                        <div class="col-md-5">
                            <asp:Button ID="btnAmountCondition" runat="server" class="btn btn-primary" Text="search.." OnClick="btnAmountCondition_Click" />

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
      <!-- Table -->
      <div class="row">
        <div class="col">
                       <div class="bg-white rounded-lg mb-1 ">
            <div class="card-header bg-white ">
                
                
                   <div class="row">
                 <div class="col-4 text-left ">
                           <a href="Home.aspx" class="btn btn-light btn-circle">
                  <i class="fas fa-arrow-left  font-weight-bold text-primary"></i>
              </a>

      

                </div>
                 <div class="col-8 text-right ">
                                               <button type="button"   runat=server id="Button3" class="mx-1  btn btn-sm btn-circle"  style="background-color: #d46fe8"  data-toggle="modal" data-target="#daterange" >
                    <a class="nav-link"  data-toggle="tooltip" data-placement="top" title="Search By Date Range">
                      <div>                       
                      <i class="fas fa-search text-white font-weight-bold"></i>
                      <span></span>  
                       </div>
                        </a>
                     </button>
                                                                              <button type="button"  runat=server id="Button1" style="background-color: #d46fe8"  class="btn btn-sm btn-circle mr-1" data-toggle="modal" data-target="#exampleModal11" >
                    <a class="nav-link"  data-toggle="tooltip" data-placement="bottom" title="Search By Amount by condition">
                      <div>                       
                      <i class="fas fa-search-location text-white font-weight-bold"></i>
                      <span></span>  
                       </div>
                        </a>
                  </button>
                    <button type="button" runat="server" id="Sp2" class="btn btn-sm btn-circle" style="background-color: #d46fe8" data-toggle="modal" data-target="#exampleModal">
                    <div>
                      <i class="fas fa-search text-white font-weight-bold"></i>
                   <span></span>
                    </div>
                  </button>
                <button name="b_print" onclick="printdiv('div_print');" type="button" title="Print" class="mx-1  btn btn-sm  btn-circle" style="background-color: #d46fe8" data-toggle="modal" data-target="#exampleModalCenter">
                    <div>
                      <i class="fas fa-print text-white font-weight-bold"></i>
                   
                    </div>
                  </button>
      

                </div>
                   </div>

            </div>
           
            <div id="div_print">

              <asp:Label ID="lblMsg" runat="server" ></asp:Label>

                          <div class="row mt-5">
                              <div class="col-2">

                              </div>

                                <div class="col-8 small">
                                    <div class="row " style="height:90px">
                    <div class="col-md-4 text-left">
                                 <img class="" src="../../asset/Brand/gh.jpg" alt="" width="110" height="80"><br />
                              <h5 id="oname" runat="server" class="mb-1 border-top border-dark text-gray-900 font-weight-bold "></h5>
                    </div>
                <div class="col-md-8 text-right">

            <h4 class="text-gray-900 font-weight-bold">CREDIT REPORT</h4>
                     <span id="datFrom1" runat="server" class="mb-1 text-gray-900 font-weight-bold"></span><span id="tomiddle" class="mb-1 mr-2 ml-2 text-gray-900 font-weight-bold" runat="server">To</span><span id="datTo" class="mb-1 text-gray-900 font-weight-bold" runat="server"></span>
                   <span class="mb-2 text-gray-900 font-weight-bold "><span class="h6 mx-1 mb-2 text-gray-900 font-weight-bold " id="mont" runat="server"></span></span>
              </div>
                
                </div>
                                    <br />
                                    <br />
                                    
<asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand" OnItemDataBound="Repeater1_ItemDataBound">
                         
                <HeaderTemplate>
            <div class="table-responsive">
              <table class="table align-items-center table-sm ">
                <thead >
                  <tr>
                
                 
            <th scope="col" class="text-gray-900"><asp:LinkButton ID="LinkButton1" runat="server" CommandName="customer">CUSTOMER</asp:LinkButton></th>
                        
                    
                    <th scope="col" class=" text-gray-900"><asp:LinkButton ID="LinkButton3" runat="server" CommandName="date">DATE</asp:LinkButton></th>
 <th><asp:LinkButton ID="LinkButton4" class="text-gray-900" runat="server" CommandName="date">AGED</asp:LinkButton></th>
                      <th>MOBILE</th>
                      <th scope="col" class="text-right"><asp:LinkButton ID="LinkButton2" runat="server" class="text-danger" CommandName="balance">AMOUNT</asp:LinkButton></th>
             
                  </tr>
                </thead>
                <tbody>
                                                                                        </HeaderTemplate>
                <ItemTemplate>
                  <tr>
                                        <td>
                    <span class="text-primary mr-1">CN#<%# Eval("id")%></span><span><asp:Label ID="Label2" runat="server" Text=<%# Eval("customer")%>></asp:Label></span>
                    </td>


                    <td >
                    <asp:Label ID="lblDueDate" runat="server" Text=<%# Eval("date","{0:MMMM dd, yyyy}")%>></asp:Label>
                    
                    </td>
                      <td>
                      <asp:Label ID="lblAged" runat="server"></asp:Label></td>
                                            <td>
                      <asp:Label ID="lblPhone" runat="server"></asp:Label></td>                    
                      <td class="text-right">
                    <%# Eval("balance","{0:N2}")%>
                    </td>
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
                              <div class="row" id="TotalRow" runat="server">
               <div class="col-md-7 text-left">

                                             </div>

                <div class="col-md-5 ">
                                            <div class="form-group">
                                                <table class="table table-sm table-bordered ">
                                                    <tbody>
               
                                                             <tr>
                                                            <td><span style="margin: 7px 5px 5px 5px; padding: 5px" class="m-0 font-weight-bold text-right text-gray-900 ">Grand Total:</span></td>
                                                            <td class="text-right"> <span id="Total" class="text-gray-900 font-weight-bold text-gray-900" runat=server></span></td>
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
                                                                                                                              <center>
                                        <h1 class="water h1  font-weight-bolder" style="font-size: 60px">Raksym Trading PLC</h1>
                                                         </center>   
                       </div>

                          </div>



            </div>
                  </div>     
  
            </div>
          </div>
            </div>
</asp:Content>
