<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="rentstatus1.aspx.cs" Inherits="advtech.Finance.Accounta.rentstatus1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <title>Rent Status</title>
    <script type="text/javascript">
        document.onclick = hideMenu;
        document.oncontextmenu = rightClick;

        function hideMenu() {
            document.getElementById("contextMenu")
                .style.display = "none"
        }

        function rightClick(e) {
            e.preventDefault();

            if (document.getElementById("contextMenu").style.display == "block") {
                hideMenu();
            } else {
                var menu = document.getElementById("contextMenu")
                menu.style.display = 'block';
                menu.style.left = e.pageX + "px";
                menu.style.top = e.pageY + "px";
            }
        }

    </script>
    <script type="text/javascript">
        window.addEventListener('load', (event) => {
            var x = document.getElementById("Pbutton1");
            x.style.display = "none";
        });
    </script>
    <script type="text/javascript">
        function myFunction() {
            /* Get the text field */
            var copyText = document.getElementById("myInput");

            copyText.select();
            copyText.setSelectionRange(0, 99999); /* For mobile devices */

            /* Copy the text inside the text field */
            navigator.clipboard.writeText(copyText.value);

            /* Alert the copied text */
            alert("Copied the text: " + copyText.value);
        }
    </script>
                    <script type="text/javascript">
                        window.addEventListener('load', (event) => {
                            var x = document.getElementById("myDIVSM");
                            x.style.display = "none";
                        });
                    </script>
                    <script type="text/javascript">
                        window.addEventListener('load', (event) => {
                            var x = document.getElementById("MyDivv");
                            x.style.display = "none";
                        });
                        window.addEventListener('load', (event) => {
                            var x = document.getElementById("MyDivvC");
                            x.style.display = "none";
                        });

                        window.addEventListener('load', (event) => {
                            var x = document.getElementById("MyDivvB");
                            x.style.display = "none";
                        });
                    </script>
    <style>
        .context-menu {
            position: absolute;
        }

        .menu {
            display: flex;
            flex-direction: column;
            background-color: #fff;
            border-radius: 10px;
            box-shadow: 0 10px 20px rgb(64 64 64 / 5%);
            padding: 10px 0;
        }

            .menu > li > a {
                font: inherit;
                border: 0;
                padding: 1px 30px 10px 15px;
                width: 100%;
                display: flex;
                align-items: center;
                position: relative;
                text-decoration: unset;
                color: #000;
                font-weight: 500;
                transition: 0.5s linear;
                -webkit-transition: 0.5s linear;
                -moz-transition: 0.5s linear;
                -ms-transition: 0.5s linear;
                -o-transition: 0.5s linear;
            }

                .menu > li > a:hover {
                    background: #f1f3f7;
                    color: #4b00ff;
                }

                .menu > li > a > i {
                    padding-right: 10px;
                }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <!-- Navbar -->

    <div class="container-fluid pl-3 pr-3">
              <div class="modal fade" id="EX" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle3" aria-hidden="true">
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
      <asp:Button ID="btnUncollected1"  class="btn btn-danger w-50" runat="server" Text="Export" OnClick="btnUncollected_Click1"/>
                          </center>

 
                      
                      </div>
                    </div>
                    </div>

    
  </div>

</div>
</div>
</div>




                <div class="modal fade" id="exampleModalSP" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabelSP" aria-hidden="true">
  <div class="modal-dialog modal-sm" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title text-gray-900" id="exampleModalLabelSP">Type Shop No.</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        <div class="row">
            <div class="col-md-7">
                <asp:TextBox ID="txtShopNo"  class="form-control mx-2" runat="server"></asp:TextBox>

            </div>

                        <div class="col-md-5">
                            <asp:Button ID="Button3" runat="server" class="btn btn-primary" Text="search.." OnClick="Button3_Click"  />

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
        <div class="modal fade" id="exampleModalLongBulkSMS" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle34" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title h5 font-weight-bold text-gray-900" id="exampleModalLongTitle34">Send Custom SMS</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
                       <div class="row">
                        <div class="col-12">
                      <div class="form-group">


    <asp:TextBox ID="txtBulkmessage" runat="server" TextMode="MultiLine" class="form-control " Height="150px" placeholder="Less than 100 character (do not contain spam)"></asp:TextBox>
                      
                      </div>
                    </div>
                    </div>


      <div class="modal-footer">
                        <div class="col-md-1">
                 <div id="myDIVSM" class="spinner-border text-danger spinner-border-sm  mr-4 mt-2" role="status">
                <span class="sr-only">Loading.ffrfyyrg..</span>
                   </div> 
                        </div>
      <center>
<buttom runat="server" id="Button7" type="button" class="btn btn-link mr-2 btn-sm text-xs" data-toggle="modal" data-target="#exampleModalSample" >
                      <a class="nav-link btn btn-sm"  data-toggle="tooltip" data-placement="bottom" title="See Sample">
                              <div>
                 See Sample SMS
                      
                    </div>
                        </a>
                  </buttom>


       <asp:Button ID="btnBulkSMS"  class="btn btn-danger" runat="server" Text="Send" OnClick="btnBulkSMS_Click" OnClientClick="myFunctionshopSM()"/>
      </center>
      </div>
    
  </div>

</div>
</div>
</div>
        <div class="modal fade" id="exampleModalLongDueDate" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle311" aria-hidden="true">
  <div class="modal-dialog modal-sm" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title h6  text-gray-900" id="exampleModalLongTitle311">Update Due Date</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
                       <div class="row mb-3">
                        <div class="col-12">
                      <div class="form-group">
                          <asp:TextBox ID="txtDueDate" TextMode="Date" class="form-control" runat="server"></asp:TextBox>
                          </div>
                            </div>
                            </div>

                                 <div class="row">
                        <div class="col-12">
                      <div class="form-group">

                <asp:Button ID="btnDueDateUpdate"  class="btn btn-danger btn-sm w-100" runat="server"  Text="Save" OnClick="btnDueDateUpdate_Click"/>
                      
                      </div>
                    </div>
                    </div>

    
  </div>

</div>
</div>
</div>
        <div class="modal fade" id="exampleModalLongService1" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle31" aria-hidden="true">
  <div class="modal-dialog modal-sm" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title h6  text-gray-900" id="exampleModalLongTitle31">bulk cash Payment</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
          <div class="text-center mb-2" id="MyDivvC">
  <div class="spinner-border  spinner-border-sm" role="status">
    <span class="sr-only">Loading...</span>
  </div>
</div>

                                 <div class="row">
                        <div class="col-12">
                      <div class="form-group">


       <asp:Button ID="btnBulkCashPayment"  class="btn btn-danger btn-sm w-100" runat="server" OnClientClick=" myFunctionshopC()" Text="Save" OnClick="btnBulkCashPayment_Click"/>
                      
                      </div>
                    </div>
                    </div>

    
  </div>

</div>
</div>
</div>
        <div class="modal fade" id="exampleModalLongService2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle32" aria-hidden="true">
  <div class="modal-dialog modal-sm" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title h6 font-weight-bold text-gray-900" id="exampleModalLongTitle32">bulk bank payment</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
          <div class="text-center mb-2" id="MyDivvB">
  <div class="spinner-border  spinner-border-sm" role="status">
    <span class="sr-only">Loading...</span>
  </div>
</div>
                    <div class="row mb-3">
              <div class="col-md-12 col-sm-4">
                
          <div class="form-group mb-0">
             <label class=font-weight-bold>Bank Account<span class=text-danger>*</span></label>
            <div class="input-group input-group-alternative">
                <asp:DropDownList ID="DropDownList1" class="form-control form-control-sm" runat="server"></asp:DropDownList>

              
            </div>
          </div>
                                                           <div class="custom-control mt-2  custom-checkbox ">
                 <input type="checkbox" class="custom-control-input" id="CheckGene" checked="true" runat="server" clientidmode="Static"/>
  <label class="custom-control-label mt-1 small text-gray-900 " for="CheckGene">Generate invoice</label>
                     </div>        
            </div>
          </div>
                                 <div class="row">
                        <div class="col-12">
                      <div class="form-group">


       <asp:Button ID="btnBankPayment"  class="btn btn-sm btn-danger w-100" runat="server" OnClientClick=" myFunctionshopB()" Text="Save" OnClick="btnBankPayment_Click"/>
                      
                      </div>
                    </div>
                    </div>

    
  </div>

</div>
</div>
</div>
        <div class="modal fade" id="exampleModalLongService" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle3" aria-hidden="true">
  <div class="modal-dialog modal-sm" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title h6 font-weight-bold text-gray-900" id="exampleModalLongTitle3">Mark as Uncollected</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
          <div class="text-center mb-2" id="MyDivv">
  <div class="spinner-border  spinner-border-sm" role="status">
    <span class="sr-only">Loading...</span>
  </div>
</div>
                                           <div class="row">
                        <div class="col-12">
                      <div class="form-group">
                          </div>
                            <p class="small text-danger">You are about to provide full credit. Make sure before saving. And check "Enable Credit Limit is Off."</p>
                            </div>
                                               </div>
                                 <div class="row">
                        <div class="col-12">
                      <div class="form-group">


       <asp:Button ID="btnUncollected"  class="btn btn-danger w-100" runat="server" OnClientClick=" myFunctionshop2()" Text="Save" OnClick="btnUncollected_Click"/>
                      
                      </div>
                    </div>
                    </div>

    
  </div>

</div>
</div>
</div>
                <div class="modal fade" id="exampleModalAM" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel11" aria-hidden="true">
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
                                        <div class="custom-control custom-radio custom-control-inline">
  <input type="radio" id="equal" name="customRadioInline1" class="custom-control-input" runat=server clientidmode="Static"/>
  <label class="custom-control-label font-weight-200  text-gray-900 " for="equal">=</label>
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
        <div class="modal fade" id="exampleModalAG" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitleAG" aria-hidden="true">
  <div class="modal-dialog modal-sm" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title h6  text-gray-900" id="exampleModalLongTitleAG">Update Agreement Date</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
                       <div class="row mb-3">
                        <div class="col-12">
                      <div class="form-group">
                          <asp:TextBox ID="txtAgreeDate" TextMode="Date" class="form-control" runat="server"></asp:TextBox>
                          </div>
                            </div>
                            </div>

                                 <div class="row">
                        <div class="col-12">
                      <div class="form-group">

                <asp:Button ID="btnAgreeUpdate"  class="btn btn-danger btn-sm w-100" runat="server"  Text="Save" OnClick="btnAgreeUpdate_Click"/>
                      
                      </div>
                    </div>
                    </div>

    
  </div>

</div>
</div>
</div>
      <!-- Table -->
      <div class="row">
        <div class="col"> 
            
            <div class="bg-white rounded-lg">    
            <div class="row">
            <div class="col-xl-12 col-md-6">
             
                                     
                  <div class="row">
            <div class="col-xl-3 col-md-6 border-right">
                  <div class="card-header-pills text-center">
                      <div class="text-center text-xs mt-2 font-weight-bolder badge badge-light  text-gray-700 text-uppercase"  title="Customers whose due date less or equal 15 days but not passed their due date!" data-toggle="tooltip" data-placement="bottom">Unpaid Rent<span id="notifcounter" runat="server" class="badge badge-danger badge-counter mx-2">#C-<span id="customerno" runat="server">0</span></span></div>
                     
                  </div> 
                
  
                <div class="card-body">
                  <div class="row no-gutters align-items-center">
                    <div class="col mr-2">
                                            <div class="h6 font-weight-bold text-gray-700 text-center text-uppercase"> <span id="Span2" class="mx-2 fas fa-hand-holding-usd btn-circle border-bottom  border-bottom-info fa-2x" style="color:#ff6a00" runat="server"></span></div>
           
                       <h5 class="text-gray-900 mt-2 text-center" id="cost" runat="server"></h5> 
                      
                    </div>
                      
                    <div class="col-auto">

                      
                    </div>

                  </div>
                    </div>
                </div>
                      <div class="col-xl-3 col-md-6 border-right">
                  <div class="card-header-pills text-center">
                      <div class="text-center text-xs mt-2 font-weight-bolder badge badge-light  text-gray-700  text-uppercase">Due Date Passed Amount</div>
                     
                  </div> 
  
  
                <div class="card-body ">
                  <div class="row no-gutters align-items-center">
                    <div class="col mr-2">
                     <div class="h6 font-weight-bold text-gray-700 text-center text-uppercase"> <span id="Span1" class="mx-2 fas fa-calendar fa-2x btn-circle border-bottom  border-bottom-info fa-2x" style="color: #ff6a00" runat="server"></span></div>
                        <h5 class="text-gray-900 mt-2 text-center" id="H1" runat="server"></h5> 
                        
                      
                    </div>
                      
                    <div class="col-auto">

                      
                    </div>

                  </div>
                    </div>
                </div>
                      <div class="col-xl-3 col-md-6 border-right text-white text-white">
                  <div class="card-header-pills text-white text-center">
                      <div class="text-center text-xs mt-2 font-weight-bolder  badge-light  text-gray-700  badge text-uppercase">Action</div>
                     
                  </div> 
      
  
                <div class="card-body ">
                  <div class="row no-gutters align-items-center">
                    <div class="col mr-2" id="automationdiv" runat="server">
                        <center>
                      <div class="h6 font-weight-bold text-gray-700 text-center text-uppercase"> <span id="Span6" class="mx-2 fas fa-exclamation-triangle fa-2x text-gray-400" runat="server"></span></div>
                        <h6 class="mt-2 small text-gray-400 text-center">Automation is On.</h6>
                        </center>


                        
                      
                    </div>
                      <div class="col mr-2" id="Div1" runat="server">
                                              <center>
                                                  <button class="btn btn-sm text-uppercase text-white" type="button" disabled id="Pbutton1" style="display: none; background-color: #d46fe8">
                                                      <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                                      Sending SMS
                                                  </button>
                                                  <div id="Bd1">
                                                  <asp:LinkButton ID="btnAlert" OnClientClick="myFunctionshop1vb()" Style="background-color: #d46fe8" OnClick="btnAlert_Click"  class="btn btn-sm text-uppercase text-white" runat="server" ><span class="fas fa-sms mr-2 text-white "></span>Alert Customer</asp:LinkButton>

                                                   </div>
                                                      </center>
                          </div>
                    <div class="col-auto">

                      
                    </div>

                  </div>
                    </div>
                </div>
                      <div class="col-xl-3 col-md-6 border-right text-center">
                  <div class="card-header-pills text-center">
                      <div class="text-center text-xs mt-2 font-weight-bolder badge badge-light  text-gray-700  text-uppercase">Total Receivable</div>
                     
                  </div> 
  
  
                <div class="card-body ">
                  <div class="row no-gutters align-items-center">
                    <div class="col mr-2">
                     <div class="h6 font-weight-bold text-gray-700 text-center text-uppercase"> <span id="Span4" class="mx-2 fas fa-donate fa-2x btn-circle border-bottom  border-bottom-info fa-2x" style="color: #ff6a00" runat="server"></span></div>
                        <h5  class="text-gray-900 mt-2 text-center" id="H4" runat="server"></h5> 
                        
                      
                    </div>
                      
                    <div class="col-auto">

                      
                    </div>

                  </div>
                    </div>
                </div>
                </div>
              

               

            <div class="card-header bg-white border-top ">
                <div class="row">
                    <div class="col-3 text-left">
                                                             <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown1"
                                                    role="button" data-toggle="dropdown" aria-haspopup="true"
                                                    aria-expanded="false">
                                                    <span id="cashdrop" class="small" runat="server">Rent Status</span>
                                                </a>
                                                <div class="dropdown-menu dropdown-menu-left animated--fade-in"
                                                    aria-labelledby="navbarDropdown1">
                                                    <a class="dropdown-item" href="#">
                                                        <asp:Button ID="btnPassed" class="btn btn-sm text-white w-100" style="background-color:#cc7df1" OnClick="btnPassed_Click" runat="server" Text="Bind Due passed Customer" />
                                                        
                                                       </a>
                                                      <a class="dropdown-item" href="#">
                                                          <asp:Button ID="btnUnpassed" class="btn text-white btn-sm w-100" Style="background-color: #cc7df1" OnClick="btnUnpassed_Click" runat="server" Text="Bind who approach the due date" />
                                                        
                                                       </a>
                                                      <a class="dropdown-item" href="#">
                                                        
                                                          <asp:Button ID="btnAll" class="btn text-white btn-sm w-100" Style="background-color: #cc7df1" OnClick="btnAll_Click" runat="server" Text="Bind All Customer" />
                                                       </a>

                                                </div>
                    </div>
                    <div class="col-9 text-right">

                                          <div class="dropdown no-arrow">
                                                        <button type="button" data-toggle="modal" data-target="#EX" class="btn mt-1 mr-2 mb-1 btn-circle btn-sm btn-warning"><span data-toggle="tooltip" title="Export as excel" class="fas fa-file-excel text-white"></span></button>


                                                                                                                              <button runat="server" id="Button6" type="button" class="btn btn-circle mr-2 btn-sm text-xs btn-primary" data-toggle="modal" data-target="#exampleModalLongBulkSMS" >
                      <a class="nav-link btn btn-sm"  data-toggle="tooltip" data-placement="bottom" title="Send SMS">
                              <div>
                      <i class="fas fa-mail-bulk text-white"></i>
                      
                    </div>
                        </a>
                  </button>
                              <button runat="server" id="Button5" type="button" class="btn btn-circle mr-2 btn-sm text-xs btn-secondary" data-toggle="modal" data-target="#exampleModalLongDueDate" >
                                        <a class="nav-link btn btn-sm"  data-toggle="tooltip" data-placement="bottom" title="Update Due Date">
                              <div>
                      <i class="fas fa-calendar-check text-white"></i>
                      
                    </div>
                        </a>
                  </button>
                        <button runat="server" id="Button4" type="button" class="btn btn-circle mr-2 btn-sm text-xs btn-success" data-toggle="modal" data-target="#exampleModalLongService1" >
                                        <a class="nav-link btn btn-sm"  data-toggle="tooltip" data-placement="bottom" title="Bulk Cash Payment">
                              <div>
                      <i class="fas fa-hand-holding-usd text-white"></i>
                      
                    </div>
                        </a>
                  </button>
                           <button runat="server" id="Button1" type="button" class="btn btn-circle mr-2 btn-sm text-xs btn-warning" data-toggle="modal" data-target="#exampleModalLongService2" >
                                             <a class="nav-link btn btn-sm"  data-toggle="tooltip" data-placement="bottom" title="Bulk Bank Payment">
                              <div>
                      <i class="fas fa-landmark text-white"></i>
                      
                    </div>
                        </a>
                  </button>
                  <button runat="server" id="modalMain" type="button" class="btn btn-circle mr-2 btn-sm text-xs btn-danger" data-toggle="modal" data-target="#exampleModalLongService" >
                                        <a class="nav-link btn btn-sm"  data-toggle="tooltip" data-placement="bottom" title="Mark as Uncollected">
                              <div>
                      <i class="fas fa-donate text-white"></i>
                      
                    </div>
                        </a>
                  </button>
                    <button type="button"  runat=server id="Sp2" class="btn btn-sm btn-primary btn-circle" data-toggle="modal" data-target="#exampleModal" >
                    <div>
                      <i class="fas fa-search text-white"></i>
                   <span></span>
                    </div>
                  </button>
        
                        <button class="btn btn-light btn-circle dropdown-toggle" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" >
             
                      <a class="nav-link btn btn-sm"  data-toggle="tooltip" data-placement="bottom" title="Options">
                              <div>
                      <i class="fas fa-caret-down text-danger"></i>
                      
                    </div>
                        </a>
  
                </button>
                    <div class="dropdown-menu  dropdown-menu-right shadow animated--fade-in" aria-labelledby="dropdownMenuLink">
                      <div class="dropdown-header">Option:</div>
                      <a class="dropdown-item" id="CustLink" runat=server href="rentreport.aspx">Generate Report</a>
                        <a class="dropdown-item" id="A6" runat=server href="uncollectedEarning.aspx">UnEarned Revenues</a>
                        <a class="dropdown-item" id="A7" runat=server href="uncollectedEarning.aspx"   data-toggle="modal" data-target="#exampleModalSP">Search by Shop No</a>
                        <a class="dropdown-item" id="A8" runat=server href="#" data-toggle="modal" data-target="#exampleModalAM">Search by Amount</a>
                         <a class="dropdown-item border-top text-danger" id="A1" runat=server href="#"   data-toggle="modal" data-target="#exampleModalAG">Update Agree. Date</a>

                    </div>
                                          </div>

            
                    </div>
                </div>

            </div>  
               
             <div class="card-body">
                 <div id="contextMenu" class="context-menu shadow-lg animated--grow-in" style="display: none"> 
        <ul class="menu"> 
<a class="dropdown-item " id="A5" runat=server href="#" data-toggle="modal" data-target="#exampleModalLongDueDate"><span class="text-warning fas fa-calendar-check mr-3"></span><span>Update Due Date</span></a>
<a class="dropdown-item" id="A2" runat=server href="#" data-toggle="modal" data-target="#exampleModalLongService1"><span class="text-warning fas fa-hand-holding-usd mr-2"></span><span>Bulk Cash Payment</span></a>
<a class="dropdown-item" id="A3" runat=server href="#" data-toggle="modal" data-target="#exampleModalLongService2"><span class="text-warning fas fa-landmark mr-3"></span><span>Bulk Bank Pay</span></a>
<a class="dropdown-item" id="A4" runat=server href="#" data-toggle="modal" data-target="#exampleModalLongBulkSMS"><span class="text-warning fas fa-mail-bulk mr-3"></span><span>Send SMS</span></a>

        </ul> 
    </div> 

<asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                       
                <HeaderTemplate>
             <div class="table-responsive"> 
              <table class="table small align-items-center table-sm" id="tblCustomerss">
                <thead >
                  <tr>
                 <th class="text-center border-right border-left"><asp:CheckBox ID="chkHeader3" runat="server" /></th>
                  <th scope="col"  class="text-gray-900">Customer</th>
                    <th scope="col"  class="text-gray-900">Shop No.</th>
                   
                    <th scope="col"  class="text-gray-900">Buissness/Company</th>
                    <th scope="col"  class="text-gray-900">Area(m<sup>2</sup>)</th>
                    <th scope="col"  class="text-gray-900">Rate(Monthly)</th>                      
                      <th scope="col"  class="text-gray-900 border-right">SC(Monthly)</th>
                      <th scope="col"  class="text-gray-900 border-right">Agr. Date</th>
                    <th scope="col"   class="text-gray-900 border-right  border-right" title="The VAT amount is calculated by adding the monthly rate by its payment period then taking 15% of that amount" data-toggle="tooltip" data-placement="bottom">VAT(15%)</th>  
                     <th scope="col"  class="text-gray-900 text-center border-right">Total</th>

                      <th scope="col"  class="text-gray-900">Due Date</th>
                      <th scope="col"  class="text-gray-900">Status</th>            

             
                  </tr>
                </thead>
                <tbody>
                                                                                        </HeaderTemplate>
                <ItemTemplate>
                  <tr>
                      <th class="text-center  border-right border-left"> <asp:CheckBox ID="chkRow3" runat="server"/></th>

                    <td class="text-primary text-left">
                    <a  title="Show customer details" data-toggle="tooltip" data-placement="bottom" href="CustomerDetails.aspx?ref2=<%# Eval("customer")%>""> CN#-<asp:Label ID="Label2" Style="color: #d46fe8" runat="server" Text='<%# Eval("customer")%>'></asp:Label></a>
                    </td>
                     <td class="text-gray-900">
                    <%# Eval("shopno")%>
                    </td>
                      <td class="text-gray-900">
                    <%# Eval("buisnesstype")%>
                    </td>
                     <td class="text-gray-900">
                    <%# Eval("area", "{0:N2}")%>
                    </td>
                    <td class="text-gray-900">
                        <asp:Label ID="lblPrice" runat="server" Text=<%# Eval("price", "{0:N2}")%>></asp:Label>
                    
                    </td>                                             <td class="text-gray-900 border-right">
                        <asp:Label ID="lblServiceCharge" runat="server" Text=<%# Eval("servicecharge", "{0:N2}")%>></asp:Label>
                    
                    </td>      
                      <td>
                          <asp:Label ID="Label3" class="text-danger" runat="server" ></asp:Label>
                      </td>
                    <td class="text-gray-900 border-right ">
                        <asp:Label ID="lblVAT" runat="server"> </asp:Label>

                    </td>
                      <td class="text-gray-900 border-right text-center">
                          <asp:Label ID="lblTotal" runat="server" Text=<%# Eval("currentperiodue", "{0:N2}")%>></asp:Label>
                    
                    </td>

                     <td class="text-gray-900">
                         <asp:Label ID="lblDueDate" class=" font-weight-bold" runat="server" Text=<%# Eval("duedate","{0: dd/MM/yyyy}")%>></asp:Label>
                    
                    </td>
                      <td>
                       <asp:Label ID="lblDueStatus" class=" font-weight-bold badge badge-warning" runat="server"></asp:Label>
                       <asp:Label ID="lblFine" class=" font-weight-bold badge badge-success" runat="server"></asp:Label>
                          <asp:Label ID="lblpassed" class=" font-weight-bold badge badge-danger" runat="server"></asp:Label>
                    </td>
                                          <td class="text-right">
                      <div class="dropdown mb-4">
                        <a class="btn btn-sm btn-icon-only text-light" href="#" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                          <i class="fas fa-ellipsis-v text-primary"></i>
                        </a>
                        <div class="dropdown-menu dropdown-menu-right shadow animated--fade-in dropdown-menu-arrow">
                            <a class="dropdown-item" href="cashpay.aspx?cust=<%# Eval("customer")%>"><span class="fas fa-hand-holding-usd mr-1" style="color: #d46fe8"></span><span class="mx-2 text-gray-500">Create Invoice</span></a>
                          <a class="dropdown-item" href="markasreceivable.aspx?cust=<%# Eval("customer")%>"><span class="fas fa-donate mr-1" style="color: #d46fe8"></span><span class="mx-2 text-gray-500">Mark As Receivable</span></a>
                          
         

                            <a class="dropdown-item" href="CustomerStatement.aspx?ref=<%# Eval("customer")%>"><span class="fas fa-user-edit mr-1" style="color: #d46fe8"></span><span class="mx-2 text-gray-500">Statement</span></a>
                        </div>
                      </div>
                    </td>



   




                  </tr>
                  
                                                                            </ItemTemplate>
                <FooterTemplate>
                </tbody>
              </table>
                    </div>
                                                                 
                        </FooterTemplate>
            

                                                     
            </asp:Repeater>
 

                     <div id="con" visible="false" class="border-top" runat="server">
<asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                       
                <HeaderTemplate>
             <div class="table-responsive"> 
              <table class="table small align-items-center table-sm" id="tblCustomerss">
                <thead >
                  <tr>

                  <th scope="col"  class="text-gray-900">Customer</th>
                    <th scope="col"  class="text-gray-900">Shop No.</th>
                   
                    <th scope="col"  class="text-gray-900">Buissness Type</th>
                    <th scope="col"  class="text-gray-900">Area(m<sup>2</sup>)</th>
                    <th scope="col"  class="text-gray-900">Rate(Monthly)</th>                      
                      <th scope="col"  class="text-gray-900 border-right">SC(Monthly)</th>
                      <th scope="col"  class="text-gray-900 border-right">Agr. Date</th>
                    <th scope="col"   class="text-gray-900 border-right  border-right" title="The VAT amount is calculated by adding the monthly rate by its payment period then taking 15% of that amount" data-toggle="tooltip" data-placement="bottom">VAT(15%)</th>  
                     <th scope="col"  class="text-gray-900 text-center border-right">Total</th>

                      <th scope="col"  class="text-gray-900">Due Date</th>
                      <th scope="col"  class="text-gray-900">Status</th>            

             
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
                    <%# Eval("buisnesstype")%>
                    </td>
                     <td class="text-gray-900">
                    <%# Eval("area", "{0:N2}")%>
                    </td>
                    <td class="text-gray-900">
                        <asp:Label ID="lblPrice" runat="server" Text=<%# Eval("price", "{0:N2}")%>></asp:Label>
                    
                    </td>                                             <td class="text-gray-900 border-right">
                        <asp:Label ID="lblServiceCharge" runat="server" Text=<%# Eval("servicecharge", "{0:N2}")%>></asp:Label>
                    
                    </td>      
                      <td>
                          <asp:Label ID="Label3" class="text-danger" runat="server" ></asp:Label>
                      </td>
                    <td class="text-gray-900 border-right ">
                        <asp:Label ID="lblVAT" runat="server"> </asp:Label>

                    </td>
                      <td class="text-gray-900 border-right text-center">
                          <asp:Label ID="lblTotal" runat="server" Text=<%# Eval("currentperiodue", "{0:N2}")%>></asp:Label>
                    
                    </td>

                     <td class="text-gray-900">
                         <asp:Label ID="lblDueDate" class=" font-weight-bold" runat="server" Text=<%# Eval("duedate","{0: dd/MM/yyyy}")%>></asp:Label>
                    
                    </td>
                      <td>
                       <asp:Label ID="lblDueStatus" class=" font-weight-bold badge badge-warning" runat="server"></asp:Label>
                       <asp:Label ID="lblFine" class=" font-weight-bold badge badge-success" runat="server"></asp:Label>
                          <asp:Label ID="lblpassed" class=" font-weight-bold badge badge-danger" runat="server"></asp:Label>
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
        </div>
      </div>
      <!-- Dark table -->
      <div class="row mt-5">

      </div>
      <!-- Footer -->
    <div class="modal fade" id="exampleModalSample" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel8shop" aria-hidden="true">
  <div class="modal-dialog modal-sm" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h6 class="modal-title text-gray-900 font-weight-bolder" id="exampleModalLabel8shop">Sample SMS</h6>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        <div class="row mb-3">
            <div class="col-md-12">
                <asp:TextBox ID="TextBox1" class="form-control" TextMode="MultiLine" runat="server"  height="150px" Text="Dear Customer, this is your last warning to pay the bill."></asp:TextBox>

            </div>
            </div>
                    <div class="row">
    
            <div class="col-md-11">

             

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
                <asp:TextBox ID="txtCustomerName"  class="form-control form-control-sm mx-2" runat="server"></asp:TextBox>

            </div>

                        <div class="col-md-5">
                            <asp:Button ID="Button2" runat="server" class="btn btn-sm btn-primary" Text="search.." OnClick="Button2_Click"  />

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
                    <script>
                        function myFunctionshopSM() {
                            var x = document.getElementById("myDIVSM");

                            if (x.style.display === "none") {
                                x.style.display = "block";
                            } else {
                                x.style.display = "none";
                            }

                        }
                    </script>
                <script>
                    function myFunctionshop2() {
                        var x = document.getElementById("MyDivv");

                        if (x.style.display === "none") {
                            x.style.display = "block";
                        } else {
                            x.style.display = "none";
                        }

                    }
                </script>
                    <script>
                        function myFunctionshopC() {
                            var x = document.getElementById("MyDivvC");

                            if (x.style.display === "none") {
                                x.style.display = "block";
                            } else {
                                x.style.display = "none";
                            }

                        }
                    </script>
                    <script>
                        function myFunctionshopB() {
                            var x = document.getElementById("MyDivvB");

                            if (x.style.display === "none") {
                                x.style.display = "block";
                            } else {
                                x.style.display = "none";
                            }

                        }
                    </script>
    <script>
        function myFunctionshop1vb() {
            var x = document.getElementById("Bd1"); var y = document.getElementById("Pbutton1");
            if (x.style.display == "none") {
                x.style.display = "block";
            } else {
                x.style.display = "none";
            }

            if (y.style.display === "none") {
                y.style.display = "block";
            } else {
                y.style.display = "none";
            }

        }
    </script>
</asp:Content>
