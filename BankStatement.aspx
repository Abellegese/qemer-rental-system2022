<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.master" AutoEventWireup="true" CodeBehind="BankStatement.aspx.cs" Inherits="advtech.Finance.Accounta.BankStatement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Bank Transaction</title>

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <!-- Navbar -->
    <div class="modal fade bd-example-modal-lg" id="SM" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-md">
    <div class="modal-content">
      <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
        <h5 class="modal-title text-gray-900 font-weight-bold" id="H1">Bind Summary</h5>
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
            <asp:TextBox ID="txtSmDateFrom" class="form-control form-control-sm " TextMode=Date  runat="server"></asp:TextBox>
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
            <asp:TextBox ID="txtSmDateTo" class="form-control form-control-sm " TextMode=Date  runat="server"></asp:TextBox>
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

                                            <asp:Button ID="btnSearchSummary" class="btn btn-sm btn-danger" OnClick="btnSearchSummary_Click"   runat="server"  
                                        Text="Bind"   />
                                        </div>
     </center>
      </div>
    </div>
  </div>
    <div class="container-fluid pl-3 pr-3">
      <!-- Table -->
      <div class="row">
              

               

        <div class="col">
        
          <div class="bg-white rounded-lg" >

              <div class="row mx-2 border-bottom">
                  <div class="col-xl-8 mb-2 mt-2 border-right border-bottom border-left border-danger ">
                      <div class="row">
                          <div class="col-10 text-left border-bottom border-danger">
                              <h6 class="font-weight-bold mx-1 text-uppercase mt-2" style="color:#ff6a00">Account Trends                                                                                                        
                                  <span id="datFrom" class="small mt-3 text-danger font-italic" runat="server"></span><span class="mt-3">-</span><span id="datTo" class="small mt-3 text-danger font-italic" runat="server"></span>
                              </h6>
                          </div>


                          <div class="col-2  text-right ">
                              <a href="#" data-toggle="modal" class="btn btn-sm btn-circle btn-warning" data-target="#SM"><span class="fas fa-calendar-check text-white"></span></a>

                          </div>
                      </div>
                      <div class="row border-top">
                          <div class="col-xl-12 mx-2 mt-2">
                              <main role="main" id="main" runat="server">

                                  <div class="starter-template">
                                      <center>


                                          <p class="lead">

                                              <i class="fas fa-chart-line text-gray-300  fa-3x"></i>

                                          </p>
                                          <h6 class="text-gray-700 small font-italic">Sorry!! Nothing to show here.</h6>
                                      </center>
                                  </div>



                              </main>
                              <asp:Literal ID="ltChart" runat="server"></asp:Literal>



                          </div>

                      </div>

                  </div>

                  <div class="col-xl-4 mb-2 mt-2 border-right border-bottom border-left border-warning">
                      <div class="row">
                          <div class="col-10 text-left">
                              <h6 class="font-weight-bold mx-1 text-uppercase mt-2" style="color: #5f1e9f">Bank account comparison </h6>
                          </div>


                          <div class="col-2  text-right ">
 
                          </div>
                      </div>
                      <div class="row border-top">
                          <div class="col-xl-12 mx-2 mt-2">
                              <main role="main" id="main1" runat="server">

                                  <div class="starter-template">
                                      <center>


                                          <p class="lead">

                                              <i class="fas fa-chart-line text-gray-300  fa-3x"></i>

                                          </p>
                                          <h6 class="text-gray-700 small font-italic">Sorry!! Nothing to show here.</h6>
                                      </center>
                                  </div>



                              </main>
                              <asp:Literal ID="ltrUnpaid" runat="server"></asp:Literal>



                          </div>

                      </div>
                      
                  </div>
                  
                  </div>
              
  
              <asp:Label ID="lblMsg" runat="server"></asp:Label>

            <div class="card-header bg-white ">
              <h6 class="m-0 font-weight-bold text-primary"></h6>
                            <div class="row">
                                <div class="col-6 text-left">
                                    <span id="cashdrop" class="small text-danger" runat="server">ACCOUNT BALANCE</span>
                                </div>
                <div class="col-6 text-right">

                  <button runat="server" id="modalMain"  type="button" class="btn btn-sm mr-2 btn-circle btn-success" data-toggle="modal" data-target="#exampleModalCenter" >
                    <div>
                      <i class="fas fa-plus"></i>
          
                    </div>
                  </button>
                  
                         <button runat="server" id="Button1"  type="button" class="btn btn-circle btn-sm btn-danger" data-toggle="modal" data-target="#exampleModalCenter2" >
                    <div>
                      <i class="fas fa-minus"></i>
                    
                    </div>
                  </button>
      


                </div>
              </div>
               
            </div> 
              <div class="card-body small text-gray-900">

                    <asp:Repeater ID="Repeater1" runat="server" >
                         
                <HeaderTemplate>
                    <div class="table-responsive">
                    <table class="table align-items-center table-sm " id="dataTable" width="100%" cellspacing="0">
                <thead >
                  <tr>
              <th scope="col" class="text-left">Account</th>

                    <th scope="col" class="text-gray-900">Cash In</th>
                   <th scope="col" class="text-gray-900">Cash Out</th>
                    <th scope="col" class="text-gray-900 text-right">Balance</th>


          
                   
                  </tr>
                </thead>
                <tbody>
                                                                                        </HeaderTemplate>
                <ItemTemplate>
                  <tr>
                     <td class="text-left text-gray-900">
                   
               <a title="Show the details" href="bankstatment.aspx?ref2=<%# Eval("account")%>"><%#Eval("account")%></a>
                    </td>


                                                            <td class="text-gray-900">
                    <%# Eval("cashin", "{0:N2}")%>
                    </td>
                                                                                  <td class="text-gray-900">
                    <%# Eval("cashout", "{0:N2}")%>
                    </td>
                                                            <td class="text-gray-900 text-right">
                    <%# Eval("balance", "{0:N2}")%>
                    </td>

                  </tr>
                                                                            </ItemTemplate>
                <FooterTemplate>
                </tbody>
              </table>
                    </div>
                                                                  </FooterTemplate>
                                                     
            </asp:Repeater>

                  </div>
   
              </div>
            </div>
                        
          </div>
        </div>

      <!-- Dark table -->


     <div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title text-gray-900 font-weight-bold" id="exampleModalLongTitle">Deposit</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
       <div class="row" >
                                      
  
                                            <div class="col-md-6 ">
                                            <div class="form-group">
     
  <asp:TextBox ID="txtvouch"  class="form-control form-control-sm " placeholder="Voucher number" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                                    <div class="col-md-6 ">
                                            <div class="form-group">

  <asp:TextBox ID="txtcheque"  class="form-control  form-control-sm " placeholder="Cheque number" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                            <div class="col-md-6 ">
                                            <div class="form-group">
            
                                      <asp:DropDownList ID="ddlbanknumber" data-toggle="popover" title="Select bank account" data-content="A bank account to deposit your cash" runat="server" class="form-control  form-control-sm ">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                 <div class="col-md-6 ">
                                            <div class="form-group">

  <asp:TextBox ID="txtCash" class="form-control  form-control-sm " placeholder="Cash" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
           
                                 <div class="col-md-12 ">
                                            <div class="form-group">
                            
  <asp:TextBox ID="txtremark" class="form-control  form-control-sm " TextMode="MultiLine" Height="100px" placeholder="Remark" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                                                                <div class="col-md-12 ">
                                            <div class="form-group">
                                      
  <asp:TextBox ID="txtDate" TextMode="Date" class="form-control  form-control-sm " placeholder="Date" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                                                                                                                                                        
                                    </div>
          <hr />
          <div class="row">
                                                       <div class="col-12 ">
                                            <div class="form-group">
                                                <label class="text-gray-900 small font-weight-bold">Select Decreased Account</label>
                                                 <br />
                                      <asp:DropDownList ID="ddlcreditaccount" runat="server" class="form-control form-control-sm">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
          </div>
      </div>
      <div class="modal-footer text-center">
      <center>
        <button type="button" class="btn btn-sm btn btn-secondary" data-dismiss="modal">Close</button>
                                            <asp:Button ID="Button3" class="btn btn-sm btn-success"   runat="server" 
                                        Text="Save changes" onclick="btnUpdate_Click"  />
                                        
     </center>

      </div>
      </div>
    </div>
  </div>


      <!-- Modal -->
      <div class="modal fade" id="exampleModalCenter2" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title text-gray-900 font-weight-bold" id="H1">Cash Out</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
       <div class="row" >
                                      
  
                                            <div class="col-6 ">
                                            <div class="form-group">
           
  <asp:TextBox ID="txtvouch1" TextMode="Number" class="form-control form-control-sm" placeholder="Voucher number" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                                                <div class="col-6 ">
                                            <div class="form-group">
                   
  <asp:TextBox ID="txtcheque1" class="form-control form-control-sm" placeholder="Cheque number" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                                       <div class="col-md-6 ">
                                            <div class="form-group">
                      
                                      <asp:DropDownList ID="DropDownList1" runat="server" class="form-control form-control-sm ">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        
                                                <div class="col-6 ">
                                            <div class="form-group">
                             
  <asp:TextBox ID="txtCash1" class="form-control form-control-sm" placeholder="Cash" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                                                               <div class="col-12 ">
                                            <div class="form-group">
                
  <asp:TextBox ID="txtremark1" class="form-control  form-control-sm" Height="100px" placeholder="Remark" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                                                                <div class="col-md-12 ">
                                            <div class="form-group">
                                                <label>Date</label>
    
                                            
                                                 <br />
  <asp:TextBox ID="txtDate1" TextMode="Date" class="form-control  form-control-sm" placeholder="Date" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                                                                                                           
                                    </div>
                                                              <hr />
          <div class="row">
                                                       <div class="col-12 ">
                                            <div class="form-group">
                                                                     <label class="text-gray-900 small font-weight-bold">Select Increased Account</label>
                                                 <br />
                                      <asp:DropDownList ID="DropDownList2" runat="server" class="form-control form-control-sm ">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
          </div>   
      </div>
      <div class="modal-footer">
      <center>
        <button type="button" class="btn btn-secondary btn-sm" data-dismiss="modal">Close</button>
                                            <asp:Button ID="Button5" class="btn btn-danger btn-sm"   runat="server" 
                                        Text="Save changes" onclick="btnUpdate_Click2"  />
                                        </div>
     </center>
      </div>
    </div>
  </div>


      <div class="modal fade" id="Div1" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="H2">Filter record</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
                            
                            <div class="row align-items-center">
                <div class="col-6">
                <label class="form-control-label" for="input-email">From:</label>
                <asp:TextBox ID="TextBox5" runat="server" class="form-control form-control-alternative" placeholder="Remark" TextMode="Date"></asp:TextBox>
              
                </div>
                                <div class="col-6">
                                <label class="form-control-label" for="input-email">TO:</label>
                <asp:TextBox ID="TextBox7" runat="server" class="form-control form-control-alternative" placeholder="Remark" TextMode="Date"></asp:TextBox>
                
                </div>

                      

                
          
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary btn-sm" data-dismiss="modal">Close</button>

                                      <asp:Button ID="Button7" runat="server" class="btn btn-sm btn-primary" 
                        Text="Search..."  Height="56" onclick="Button7_Click" />
      </div>
    </div>
  </div>
</div>
    </div>
</asp:Content>

