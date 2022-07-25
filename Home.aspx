<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="advtech.Finance.Accounta.Home" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Home</title>
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
    <script type="text/javascript">

        window.addEventListener('load', (event) => {
            var x = document.getElementById("Pbutton");
            x.style.display = "none";
    </script>

    <style>
        .vr {
            display: inline-block;
            position: center;
            width: 1px;
            min-height: 1em;
            background-color: black;
            opacity: 0.25;
        }
    </style>
    <script type="text/javascript">  
            function HandleIT() {
                var name = document.getElementById('<%=Label1.ClientID %>').value;


                PageMethods.GetData(name);

            }
    </script>
    <script type="text/javascript">
            window.addEventListener('load', (event) => {
                var x = document.getElementById("myDIVSM");
                x.style.display = "none";
            });
    </script>
    <script>
            function MarkasPaid() {
                PageMethods.MakeCustomerAsPaid(Success);
            }
            function Success(result) {
                alert("The customer successfully Marked as paid");
                window.location.reload();
            }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID='ScriptManager1' runat='server' EnablePageMethods='true' />
    <div class="container-fluid pl-3 pr-2" style="position: relative;">
        <div class="modal fade bd-example-modal-lg" id="InvoiceSummary" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
                        <h5 class="modal-title text-gray-900 font-weight-bold" id="H1"><span class="fas fa-calendar mr-2" style="color: #ff2ccd"></span>Fetch invoice data</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">



                            <div class="col-6 ">
                                <div class="form-group">
                                    <label class="font-weight-bold">From<span class="text-danger">*</span></label>


                                    <br />
                                    <div class="form-group mb-0">
                                        <div class="input-group input-group-alternative">
                                            <asp:TextBox ID="txtCHDateFrominv" class="form-control form-control-sm " TextMode="Date" runat="server"></asp:TextBox>


                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label class="font-weight-bold">To<span class="text-danger">*</span></label>


                                    <br />
                                    <div class="form-group mb-0">
                                        <div class="input-group input-group-alternative">
                                            <asp:TextBox ID="txtCHDateToinv" class="form-control form-control-sm " TextMode="Date" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <center>
                            <button class="btn btn-primary btn-sm w-100 " style="background-color: #ff00bb; display: none" type="button" disabled id="Pbutton">
                                <span class="spinner-grow spinner-grow-sm" role="status" aria-hidden="true"></span>
                                Binding Data
                            </button>
                            <asp:Button ID="btnInvSummary" class="btn btn-sm btn-danger" OnClientClick="myFunctionshop2222();" Style="background-color: #ff2ccd" OnClick="btnBindInvSumary_Click" runat="server"
                                Text="Bind Summary" />
                    </div>
                    </center>
                </div>
            </div>
        </div>

        <div class="modal fade bd-example-modal-lg" id="CashSummary" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
                        <h5 class="modal-title text-gray-900 font-weight-bold" id="H1"><span class="fas fa-calendar mr-2" style="color: #ff2ccd"></span>Fetch Cash data</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">



                            <div class="col-6 ">
                                <div class="form-group">
                                    <label class="font-weight-bold">From<span class="text-danger">*</span></label>


                                    <br />
                                    <div class="form-group mb-0">
                                        <div class="input-group input-group-alternative">
                                            <asp:TextBox ID="txtCHDateFromCash" class="form-control form-control-sm " TextMode="Date" runat="server"></asp:TextBox>


                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label class="font-weight-bold">To<span class="text-danger">*</span></label>


                                    <br />
                                    <div class="form-group mb-0">
                                        <div class="input-group input-group-alternative">
                                            <asp:TextBox ID="txtCHDateToCash" class="form-control form-control-sm " TextMode="Date" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <center>
                            <button class="btn btn-primary btn-sm w-100 " style="background-color: #ff00bb; display: none" type="button" disabled id="Pbutton">
                                <span class="spinner-grow spinner-grow-sm" role="status" aria-hidden="true"></span>
                                Binding Data
                            </button>
                            <asp:Button ID="btnCashSummary" class="btn btn-sm btn-danger" OnClientClick="myFunctionshop222();" Style="background-color: #ff2ccd" OnClick="btnBindCashSumary_Click" runat="server"
                                Text="Bind Summary" />
                    </div>
                    </center>
                </div>
            </div>
        </div>

        <div class="modal fade bd-example-modal-lg" id="SMChart" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
                        <h5 class="modal-title text-gray-900 font-weight-bold" id="H1"><span class="fas fa-calendar mr-2" style="color: #ff2ccd"></span>Fetch data</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">



                            <div class="col-6 ">
                                <div class="form-group">
                                    <label class="font-weight-bold">From<span class="text-danger">*</span></label>


                                    <br />
                                    <div class="form-group mb-0">
                                        <div class="input-group input-group-alternative">
                                            <asp:TextBox ID="txtCHDateFrom" class="form-control form-control-sm " TextMode="Date" runat="server"></asp:TextBox>


                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label class="font-weight-bold">To<span class="text-danger">*</span></label>


                                    <br />
                                    <div class="form-group mb-0">
                                        <div class="input-group input-group-alternative">
                                            <asp:TextBox ID="txtCHDateTo" class="form-control form-control-sm " TextMode="Date" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <center>
                            <button class="btn btn-primary btn-sm w-100 " style="background-color: #ff00bb; display: none" type="button" disabled id="Pbutton">
                                <span class="spinner-grow spinner-grow-sm" role="status" aria-hidden="true"></span>
                                Binding Data
                            </button>
                            <asp:Button ID="btnBindBussinessSumary" class="btn btn-sm btn-danger" OnClientClick="myFunctionshop22();" Style="background-color: #ff2ccd" OnClick="btnBindBussinessSumary_Click" runat="server"
                                Text="Bind Summary" />
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
                                <buttom runat="server" id="Button7" type="button" class="btn btn-link mr-2 btn-sm text-xs" data-toggle="modal" data-target="#exampleModalSample">
                                    <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="See Sample">
                                        <div>
                                            See Sample SMS
                      
                                        </div>
                                    </a>
                                </buttom>


                                <asp:Button ID="btnBulkSMS" class="btn btn-danger" runat="server" Text="Send" OnClick="btnBulkSMS_Click" OnClientClick="myFunctionshopSM()" />
                            </center>
                        </div>

                    </div>

                </div>
            </div>
        </div>
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
                                <asp:TextBox ID="TextBox1" class="form-control" TextMode="MultiLine" runat="server" Height="150px" Text="Dear Customer, this is your last warning to pay the bill."></asp:TextBox>

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
        <div class="bg-white rounded-lg" id="div_print">
            <div class="row mx-2 border-bottom">


                <div class="col-xl-4 mb-2 mt-2 border-right ">
                    <div class="row">
                        <div class="col-6 text-left">
                            <h6 class="font-weight-bold text-gray-900 mx-1 text-uppercase mt-2">RENT <span class="badge badge-primary badge-pill mr-1" id="UNPcounter" runat="server">0</span><span class="text-muted small text-uppercase badge badge-light">This period</span></h6>
                        </div>

                        <div class="col-4">
                            <span class="bg bg-light text-muted small mr-1" data-toggle="tooltip" data-placement="top" title="Number of customer whose agreement date passed">Agr. Passed</span><span class="badge badge-danger badge-pill  mt-2" id="AGRDcounter" runat="server">0</span>
                        </div>
                        <div class="col-2  text-right ">
                            <div class="dropdown no-arrow">
                                <button type="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="btn mt-1 mb-1 btn-circle btn-sm btn-light">
                                    <span data-toggle="tooltip" title="Action" class="fas fa-caret-down text-danger"></span>


                                </button>


                                <div class="dropdown-menu  dropdown-menu-right shadow animated--fade-in" aria-labelledby="dropdownMenuLink">
                                    <div class="dropdown-header text-gray-900">Option:</div>

                                    <a href="NoticeLetter.aspx" class="dropdown-item  text-gray-700  text-danger9" id="A1" runat="server"><span class="fas fa-exclamation-triangle  mr-2 text-gray-400  text-danger"></span>Generate Notice Letter</a>
                                    <a href="#" class="dropdown-item  text-gray-700  text-danger" id="A4" runat="server" data-toggle="modal" data-target="#exampleModalLongBulkSMS"><span class="fas fa-envelope  mr-2 text-gray-400  text-danger"></span>SMS Alert</a>
                                    <a href="rentreport.aspx" class="dropdown-item  text-gray-700  text-danger" runat="server"><span class="fas fa-user  mr-2 text-gray-400  text-danger"></span>Unpaid Rent Report</a>
                                    <hr />
                                    <a href="#" onclick="MarkasPaid();" class="dropdown-item  text-gray-700" runat="server"><span class="fas fa-check  mr-2 text-success"></span>Make Customer As Paid</a>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row border-top">
                        <div class="col-xl-6 mx-2">

                            <asp:Literal ID="ltrUnpaid" runat="server"></asp:Literal>



                        </div>

                        <div class="col-xl-5 mt-3 text-right">
                            <br />


                            <span><i class=" fas fa-dollar-sign mr-1 text-white btn-circle btn-sm " style="background-color: #10e469"></i></span><span class="text-gray-900 font-weight-bold h6" id="RentPaidAmount" runat="server">0.00</span>
                            <h6 class="text-muted small">Paid amount</h6>
                            <span><i class="fas fa-donate mr-1 text-white btn-circle btn-sm " style="background-color: #a20fb2"></i></span><span class="text-gray-900 font-weight-bold h6" id="RentUnpaidAmount" runat="server">0.00</span>
                            <h6 class="text-muted small">Unpaid amount</h6>
                            <div class="border-bottom mb-2"></div>
                            <span><i class="fas fa-percent mr-1 text-white btn-circle btn-sm " style="background-color: #ff6a00"></i></span><span class="text-gray-900 font-weight-bold h6" id="PercentageUnpaid" runat="server">0%</span>
                            <br />
                            <span class="text-muted small">% Unpaid amount</span><br />

                        </div>
                    </div>
                    <div class="card-body">

                        <div class="row no-gutters align-items-center">
                            <div class="col mr-2">
                                <div class="row">
                                    <div class="col-7">
                                        <div class="text-xs font-weight-bolder  text-uppercase mt-1" style="color: #10e469">Outstanding Receivable</div>
                                    </div>
                                    <div class="col-5 text-left" id="Receivablediv" runat="server" data-toggle="tooltip" data-placement="bottom" title="Credit imbalance found" visible="false">
                                        <a class=" btn-sm text-danger font-weight-bolder" href="#" role="button" id="dropdownMenuLink8880" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                            <span class="fas fa-exclamation-circle"></span>
                                        </a>
                                        <div class="dropdown-menu  dropdown-menu-left shadow animated--fade-in" aria-labelledby="dropdownMenuLink8880" style="width: 600px">
                                            <div class="dropdown-header  font-weight-bold text-danger text-center text-uppercase"><span class="fas fa-exclamation-circle text-danger mr-1 "></span>Credit Imbalance found</div>
                                            <hr />
                                            <div id="Imbalance_creditdiv" class=" mx-2" runat="server" visible="false">
                                                <div class="card-body  border-danger border-left">
                                                    <span class=" small" style="color: #a04ad4;">Credit imbalance amount of <b><span id="balance_left" runat="server"></span></b>found between customer statement and accounts receivable ledger { that may be caused by unchecking <b>bind credit checkbox</b> when creating invoice }</span>

                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                </div>




                                <div class="row align-content-center">
                                    <div class="col-8">
                                        <div class="h6 mb-0 font-weight-bold  text-gray-900"><a href="agedreceivable.aspx"><span id="Span9" class="text-gray-900 font-weight-bold h6" runat="server"></span></a></div>
                                    </div>
                                    <div class="col-1 text-left">
                                        <a class=" dropdown-toggle btn-sm text-warning font-weight-bolder" href="#" role="button" id="dropdownMenuLink2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></a>

                                        <div class="dropdown-menu  dropdown-menu-left shadow animated--fade-in text-center" aria-labelledby="dropdownMenuLink2">
                                            <div class="dropdown-header  font-weight-bold text-primary text-uppercase">Aged Receivable</div>
                                            <div class="dropdown-divider"></div>
                                            <a class="dropdown-item" href="#"><i class="fas fa-arrow-circle-right mx-3 text-primary"></i>AGED 1-30 DAYS<i class="invisible">gggggg</i><span class="text-uppercase font-weight-bolder text-black" id="one" runat="server">0.00</span></a>
                                            <div class="dropdown-divider"></div>
                                            <a class="dropdown-item" href="#"><i class="fas fa-arrow-circle-right mx-3 text-primary"></i>AGED 31-60 DAYS<i class="invisible">gggggg</i><span class="text-uppercase  font-weight-bolder text-black" id="two" runat="server">0.00</span></a>
                                            <div class="dropdown-divider"></div>
                                            <a class="dropdown-item" href="#"><i class="fas fa-arrow-circle-right mx-3 text-primary"></i>AGED  61-90 DAYS<i class="invisible">gggggg</i><span class="text-uppercase  font-weight-bolder text-black" id="three" runat="server">0.00</span></a>
                                            <div class="dropdown-divider"></div>
                                            <a class="dropdown-item" href="#"><i class="fas fa-arrow-circle-right mx-3 text-primary"></i>AGED  > 90 DAYS<i class="invisible">gggggg</i><span class="text-uppercase  font-weight-bolder text-black" id="four" runat="server">0.00</span></a>
                                            <div class="dropdown-divider"></div>
                                            <center>
                                                <a href="creditnote.aspx"><span class="fas fa-hand-holding-usd text-primary mr-2"></span><span class=" small text-primary mr-2">Receive Money</span></a>
                                                <div class="dropdown-divider"></div>
                                                <a href="agedreceivable.aspx"><span class="fas fa-calendar-check text-gray-500 mr-2"></span><span class=" small text-danger mr-2">Aged Report</span></a>
                                                <a href="#" data-toggle="modal" data-target="#ModalAnalysis"><span class="fas fa-chart-bar text-gray-500 mr-2"></span><span class=" small text-danger mr-2">Analysis</span></a>
                                            </center>

                                        </div>
                                    </div>

                                </div>

                            </div>

                            <div class="col-auto">

                                <i class="fas fa-hand-holding-usd text-gray-500 fa-2x "></i>

                            </div>

                        </div>
                    </div>
                </div>
                <div class="col-xl-4 mb-2 mt-2 border-right " style="color: #a04ad4">
                    <div class="row">
                        <div class="col-4 text-left">
                            <h6 class="font-weight-bold text-gray-900 text-uppercase mt-2">SHOP INFO</h6>
                        </div>
                        <div class="col-6">
                            <span class="bg bg-light text-muted small mx-1" data-toggle="tooltip" data-placement="top" title="Suspended shop">suspended shop</span><span class="badge badge-warning badge-pill  mt-2" id="SUScounter" runat="server">0</span>
                        </div>
                        <div class="col-2 text-right ">
                            <button type="button" data-toggle="modal" data-target="#ModalShop" class="btn mt-1 mb-1 btn-circle btn-sm btn-primary"><span data-toggle="tooltip" title="See customer credit and shop details" class="fas fa-info text-white"></span></button>
                        </div>
                    </div>
                    <div class="row border-top">
                        <div class="col-xl-6 mx-2">

                            <asp:Literal ID="Literal2" runat="server"></asp:Literal>



                        </div>

                        <div class="col-xl-5 mt-3 text-right">
                            <br />

                            <a href="#" data-toggle="modal" data-target="#ModalShop"><span data-toggle="modal" data-target="#ModalShop"><i class="fas mr-1 fa-home text-white btn-circle btn-sm " style="background-color: #a20fb2"></i></span><span data-toggle="modal" data-target="#ModalShop" class="text-gray-900 font-weight-bold h6" id="ShopFree" runat="server">0</span>
                                <h6 data-toggle="modal" data-target="#ModalShop" class="text-muted small">#Free shop(s)</h6>
                            </a>



                            <span><i class="fas fa-user mr-1 text-white btn-circle btn-sm " style="background-color: #10e469"></i></span><span class="text-gray-900 font-weight-bold h6" id="ShopCustomer" runat="server">0</span>
                            <h6 class="text-muted small">#Active Customer</h6>
                            <div class="border-bottom mb-2"></div>
                            <span><i class="fas fa-percent mr-1 text-white btn-circle btn-sm " style="background-color: #ff6a00"></i></span><span class="text-gray-900 font-weight-bold h6" id="ShopPercentage_Occupy" runat="server">0%</span>
                            <br />
                            <span class="text-muted small">% Occupied shop</span><br />

                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row no-gutters align-items-center">
                            <div class="col">
                                <div class="text-xs font-weight-bolder  text-uppercase text- mb-2" style="color: #a20fb2">Due Date passed Amount</div>
                                <div class="row align-items-center">
                                    <div class="col-8">
                                        <div class="h6 mb-0 font-weight-bold  text-gray-900"><a class="text-gray-900"><span id="Span2" runat="server">0.00</span></a></div>
                                    </div>
                                    <div class="col-4">
                                    </div>
                                </div>



                            </div>
                            <div class="col-auto">
                                <i class="fas fa-calendar-week text-gray-500 fa-2x "></i>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-4 mb-2 mt-2  ">
                    <div class="row">
                        <div class="col-11 text-left">
                            <h6 id="SummaryContent" runat="server" class="font-weight-bold text-gray-900 mx-3 text-uppercase mt-2" style="color: #62a9b6">BUSINESS INFO
                          <span id="SystemDate" runat="server" class="small text-gray-900 mx-1"></span>
                                <span id="datFrom1" runat="server" visible="false" class="mb-1 text-xs font-italic text-danger"></span><span id="tomiddle" visible="false" class="mb-1 mr-2 ml-2 mb-1 small font-italic text-danger" runat="server">-</span><span id="datTo" visible="false" class="mb-1 small font-italic text-danger" runat="server"></span>
                            </h6>



                        </div>
                        <div class="col-1 text-right ">
                            <button type="button" data-toggle="modal" data-target="#SMChart" class="btn mt-1 mb-1 btn-circle btn-sm btn-warning"><span data-toggle="tooltip" title="Bind data based on date range" class="fas fa-calendar-week text-white"></span></button>
                        </div>
                    </div>
                    <div class="row border-top">
                        <div class="col-xl-6 mx-2">

                            <asp:Literal ID="Literal3" runat="server"></asp:Literal>


                            <center>

                                <main role="main" id="mainb" runat="server" visible="false">

                                    <div class="starter-template">
                                        <center>


                                            <p class="lead">

                                                <i class="fas fa-chart-line text-gray-300  fa-7x"></i>

                                            </p>
                                            <h6 class="text-gray-700 small font-italic">No Data.</h6>
                                        </center>
                                    </div>



                                </main>
                            </center>

                        </div>

                        <div class="col-xl-5 mt-3 text-right">
                            <br />


                            <span><i class="fas fa-dollar-sign mr-1 text-white btn-circle btn-sm " style="background-color: #d5ff00"></i></span><span id="Revenues" runat="server" class="text-gray-900 font-weight-bold h6">0.00</span>
                            <h6 class="text-muted small">Revenue</h6>
                            <span><i class="fas fa-dollar-sign mr-1 text-white btn-circle btn-sm " style="background-color: #10e469"></i></span><span id="Expenses1" runat="server" class="text-gray-900 font-weight-bold h6">0.00</span>
                            <h6 class="text-muted small">Expense</h6>
                            <div class="border-bottom mb-2"></div>
                            <span><i class="fas fa-dollar-sign mr-1 text-white btn-circle btn-sm " style="background-color: #ff6a00"></i></span><a href="IncomeStatementReport.aspx"><span id="NetProfit" runat="server" class="text-gray-900 font-weight-bold h6">0.00</span></a>
                            <br />
                            <span class="text-muted small" id="NetText1" runat="server">Net Profit</span><br />

                        </div>
                    </div>
                    <div class="card-body">

                        <div class="row no-gutters align-items-center">
                            <div class="col mr-2">
                                <div class="text-xs font-weight-bolder text-uppercase mb-2" style="color: #d5ff00">Net Worth<span class="mx-2 text-xs text-gray-300">Balance</span></div>

                                <div class="row align-items-center">
                                    <div class="col-8">
                                        <div class="h6 mb-0 text-gray-900 font-weight-bold "><a class="text-gray-900" href="BalanceSheetReport.aspx"><span id="Span3" runat="server">0.00</span></a></div>
                                    </div>
                                    <div class="col-4">
                                        <a class="dropdown-toggle btn-sm text-warning font-weight-bolder" href="#" role="button" id="dropdownMenuLink888" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></a>
                                        <div class="dropdown-menu  dropdown-menu-right shadow animated--fade-in" aria-labelledby="dropdownMenuLink888" style="width: 400px">
                                            <div class="dropdown-header  font-weight-bold text-primary text-uppercase">Balance Sheet Summary <span class="small text-gray-900" id="BalanceSheetDate" runat="server">As of</span></div>

                                            <div class="dropdown-divider"></div>
                                            <div class="row  ml-3 mr-3 mb-4">

                                                <div class="col-md-6 text-left text-uppercase text-primary">Assets</div>
                                                <div class="col-md-6 text-right   text-uppercase text-primary"><span id="Asset2" runat="server"></span></div>
                                            </div>

                                            <div class="row  ml-3 mr-3 mb-4">
                                                <div class="col-md-6 text-left   text-uppercase text-danger">Liability</div>
                                                <div class="col-md-6 text-right   text-uppercase text-danger"><span id="Liability2" runat="server"></span></div>
                                            </div>
                                            <div class="dropdown-divider"></div>
                                            <div class="row ml-3 mr-3">
                                                <div class="col-md-4 text-left   text-uppercase text-gray-900" id="Div1" runat="server">Net Worth</div>
                                                <div class="col-md-8 text-right font-weight-bold   text-uppercase text-gray-900"><span id="NetWorth" runat="server" class="font-weight-bold "></span></div>
                                            </div>


                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-auto">
                                <i class="fas fa-chart-pie text-gray-500 fa-2x"></i>

                            </div>
                        </div>


                    </div>
                </div>
            </div>
            <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>


            <div class="row ">
                <div class="col-12">

                    <div class="row">
                        <div class="col-lg-6 mx-0 border-right">




                            <div class="card-header bg-white ">
                                <div class="row align-items-center">
                                    <div class="col-8">
                                        <div class="text-xs font-weight-bolder text-primary  text-uppercase mb-1">
                                            Cash Inflow and Outflow<span class="mx-2 text-xs text-gray-300" id="cashcurrentyear" runat="server"></span>
                                            <span id="datFromc" runat="server" visible="false" class="mb-1 small font-italic text-danger"></span><span id="tomiddlec" visible="false" class="mb-1 mr-2 ml-2 mb-1 small font-italic text-danger" runat="server">-</span><span id="datToc" visible="false" class="mb-1 small font-italic text-danger" runat="server"></span>
                                        </div>

                                    </div>
                                    <div class="col-4 text-right">
                                        <div class="nav-item dropdown">
                                            <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown1"
                                                role="button" data-toggle="dropdown" aria-haspopup="true"
                                                aria-expanded="false">
                                                <span id="cashdrop" class="small" runat="server"></span>
                                            </a>
                                            <div class="dropdown-menu dropdown-menu-right animated--fade-in"
                                                aria-labelledby="navbarDropdown1">
                                                <a class="dropdown-item" href="#">
                                                    <asp:Button ID="btnCashThisYear" OnClick="btnCashThisYear_Click" runat="server" class="btn btn-light btn-sm w-100" Text="This Year" /></a>
                                                <a class="dropdown-item" href="#">
                                                    <asp:Button ID="btnCashLastYear" OnClick="btnCashLastYear_Click" runat="server" class="btn btn-light btn-sm w-100" Text="Last Year" /></a>
                                                <a class="dropdown-item" href="#">
                                                    <button type="button" class="btn btn-danger btn-sm w-100" data-toggle="modal" data-target="#CashSummary">Customize</button></a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="progress" style="height: 15px">
                                    <div class="progress-bar bg-primary progress-bar-striped progress-bar-animated" role="progressbar" id="Atr2" runat="server" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"></div>
                                    <div class="progress-bar bg-warning progress-bar-striped progress-bar-animated" id="Atr3" runat="server" role="progressbar" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                </div>

                                <hr />
                                <div class="row align-content-md-center">
                                    <div class="col-md-3 col-sm-6">
                                        <h6 class="mb-2 font-weight-bold text-xs text-gray-900 text-uppercase">Inflow</h6>

                                        <span class="font-weight-bold text-xs text-uppercase text-primary" id="inflow" runat="server">0.00</span>
                                    </div>
                                    <div class="col-md-9 col-sm-6">
                                        <h6 class="mb-2 font-weight-bold text-xs text-uppercase text-gray-900 text-right">Outflow</h6>
                                        <h6 class="font-weight-bold text-xs text-uppercase text-right  text-warning" id="outflow" runat="server">0.00</h6>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class="col-lg-6 mb-2 ">

                            <div class="card-header bg-white ">
                                <div class="row align-items-center">
                                    <div class="col-6">
                                        <div class="text-xs font-weight-bolder text-primary text-uppercase mb-1 mr-1">
                                            Invoice Summary
                                      <span id="datFromi" runat="server" visible="false" class="mb-1 small font-italic text-danger"></span><span id="tomiddlei" visible="false" class="mb-1 mr-2 ml-2 mb-1 small font-italic text-danger" runat="server">-</span><span id="datToi" visible="false" class="mb-1 small font-italic text-danger" runat="server"></span>

                                        </div>
                                    </div>
                                    <div class="col-6 text-right">
                                        <div class="nav-item dropdown">
                                            <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown2"
                                                role="button" data-toggle="dropdown" aria-haspopup="true"
                                                aria-expanded="false">
                                                <span id="billsdrop" class="small" runat="server"></span>
                                            </a>
                                            <div class="dropdown-menu dropdown-menu-right animated--fade-in"
                                                aria-labelledby="navbarDropdown2">
                                                <a class="dropdown-item" href="#">
                                                    <asp:Button ID="btnBillThisYear" OnClick="btnBillThisYear_Click" runat="server" class="btn btn-light btn-sm w-100" Text="This Year" /></a>
                                                <a class="dropdown-item" href="#">
                                                    <asp:Button ID="btnBillLastYear" OnClick="btnBillLastYear_Click" runat="server" class="btn btn-light btn-sm w-100" Text="Last Year" /></a>
                                                <a class="dropdown-item" href="#">
                                                    <button type="button" class="btn btn-danger btn-sm w-100" data-toggle="modal" data-target="#InvoiceSummary">Customize</button></a>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col">
                                    </div>
                                </div>
                            </div>
                            <div class="card-body" height="96">

                                <div class="row border-bottom">


                                    <div class="col-md-8 text-left">
                                        <div class="text-xs font-weight-bolder text-primary text-uppercase mr-1 mb-1">
                                            Invoices

                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="h6  text-gray-900 font-weight-bold text-uppercase mb-1">ETB <span runat="server" class=" mx-1 h6 font-weight-light text-gray-900 text-uppercase mb-1" id="paidinv">0.00</span></div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="h6  text-gray-900 font-weight-bold text-uppercase mb-1">PAID:<span runat="server" class=" mx-1 h6 font-weight-light text-gray-900 text-uppercase mb-1" id="unpaidinv">0.00</span></div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-md-4 border-left">
                                        <div class="text-xs font-weight-bolder text-primary text-uppercase mb-1">Payment  Status<span class="mx-2 text-xs text-uppercase text-gray-300" runat="server" id="billcurrentyear2">Current Year</span></div>
                                        <div class="progress  mb-2" style="height: 15px;">
                                            <div class="progress-bar bg-success progress-bar-striped progress-bar-animated" id="paidbill" runat="server" role="progressbar" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                            <div class="progress-bar bg-danger progress-bar-striped progress-bar-animated" id="unpaidbill" runat="server" role="progressbar" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                        </div>


                                    </div>
                                </div>


                            </div>


                        </div>
                    </div>



                </div>

            </div>
            <div class="row">
                <div class="col-lg-12">

                    <div class="row">
                        <div class="col-lg-6 mx-0 mb-2 border-right">


                            <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
                                <div class="row align-items-center">
                                    <div class="col-12">
                                        <div class="text-xs font-weight-bolder text-primary text-uppercase mb-1">Tax & Depreciation<span class="mx-2 text-xs text-gray-300">Balance</span></div>

                                    </div>

                                </div>
                            </div>
                            <div class="card-body">


                                <div class="row align-content-md-center">
                                    <div class="col-md-6 col-sm-6">
                                        <span class="small text-danger">TAX</span>
                                        <a data-toggle="tooltip" data-placement="top" title="Tax from rents and salaries over the year.">

                                            <span id="Taxt" runat="server" class=" text-md-left badge-counter badge-pill badge badge-danger ">0.00</span>
                                        </a>
                                    </div>
                                    <div class="vr"></div>
                                    <div class="col-md-5 col-sm-6 text-right">

                                        <span class="small text-danger">Depreciation</span>
                                        <a data-toggle="tooltip" data-placement="top" title="Depreciation expenses from fixed assets such as furniture, land, buildings...">

                                            <span id="deprec" runat="server" class="text-md-left badge-counter badge-pill badge badge-success ">0.00</span>
                                        </a>

                                    </div>


                                </div>
                                <hr class="mb-2" />
                            </div>


                        </div>
                        <div class="col-lg-6 mx-0 mb-2 border-top">
                            <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
                                <div class="row align-items-center">
                                    <div class="col-12">
                                        <div class="text-xs font-weight-bolder text-primary text-uppercase mb-1">LIABILITY<span class="mx-2 text-xs text-gray-300">balance</span></div>

                                    </div>

                                </div>
                            </div>
                            <div class="card-body">

                                <div class="row">
                                    <div class="col-xl-6 col-md-6 mb-2" style="height: 70px">

                                        <div class="card-body">
                                            <div class="row no-gutters align-items-center">
                                                <div class="col mr-2">

                                                    <div class="row align-content-center">
                                                        <div class="col-8">
                                                            <div class="text-xs font-weight-bolder text-primary text-uppercase mb-2">Current Liablity</div>
                                                            <div class="h6 mb-0 font-weight-bold text-gray-800"><a class="text-gray-900 text-xs " tabindex="0" role="button" data-toggle="popover" data-trigger="focus" title="Current Liability" data-content="Current Liability is a payment that is expected to be paid within short period of time. CL include Accounts Payable, Sales Tax Payable, Income Tax Payable, etc..."><span id="Currentliability" runat="server"></span></a></div>
                                                        </div>


                                                    </div>

                                                </div>

                                                <div class="col-auto">

                                                    <i class="fas fa-chart-pie text-gray-500  "></i>

                                                </div>

                                            </div>
                                        </div>

                                    </div>
                                    <hr />
                                    <div class="col-xl-6 col-md-6 mb-2" style="height: 70px">


                                        <div class="card-body">
                                            <div class="row no-gutters align-items-center">
                                                <div class="col mr-2">

                                                    <div class="row align-content-center">
                                                        <div class="col-8">
                                                            <div class="text-xs font-weight-bolder text-primary text-uppercase mb-2">LTM</div>
                                                            <div class="h6 mb-0 font-weight-bold text-gray-800"><a class="text-gray-900 text-xs " tabindex="0" role="button" data-toggle="popover" data-trigger="focus" title="Long Term Liability" data-content="Long Term Liability is a payment that is expected to be paid over long period of time."><span id="LongTermliability" runat="server">0.00</span></a></div>
                                                        </div>


                                                    </div>

                                                </div>

                                                <div class="col-auto">

                                                    <i class="fas fa-balance-scale-right text-gray-500 "></i>

                                                </div>

                                            </div>
                                        </div>

                                    </div>

                                </div>


                            </div>

                        </div>
                    </div>

                </div>

            </div>

            <div class="row">
                <div class="col-12">

                    <div class="row">
                        <div class="col-lg-8 mx-0 mb-2 border-right">
                            <div class="card-header bg-white ">
                                <div class="row ">
                                    <div class="col-4">

                                        <h5 class="text-xs font-weight-bolder text-primary text-uppercase mb-1">Collection pattern<span class="mx-2 text-xs text-uppercase text-gray-300" runat="server" id="currentRevenueYear"></span></h5>

                                    </div>
                                    <div class="col-8 text-right">
                                        <div class="nav-item dropdown">
                                            <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown"
                                                role="button" data-toggle="dropdown" aria-haspopup="true"
                                                aria-expanded="false">
                                                <span id="incomedrop" class="small" runat="server"></span>
                                            </a>
                                            <div class="dropdown-menu dropdown-menu-right animated--fade-in"
                                                aria-labelledby="navbarDropdown">
                                                <a class="dropdown-item" href="#">
                                                    <asp:Button ID="btnincomeThisYear" OnClick="btnincomeThisYear_Click" runat="server" class="btn btn-light btn-sm w-100" Text="This Year" /></a>
                                                <a class="dropdown-item" href="#">
                                                    <asp:Button ID="btnincomeLastYear" OnClick="btnincomeLastYear_Click" runat="server" class="btn btn-light btn-sm w-100" Text="Last Year" /></a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card-body">

                                <main role="main" id="main" runat="server">

                                    <div class="starter-template">
                                        <center>

                                            <p class="lead text-primary">
                                                <span class="fas fa-trash-alt text-gray-300 fa-4x"></span>
                                            </p>

                                            <h6 class="text-gray-300 text-xs" style="font-weight: bold">Sorry!! Nothing to show here.</h6>
                                        </center>
                                    </div>

                                </main>
                                <div class="chart">
                                    <asp:Literal ID="ltChart" runat="server"></asp:Literal>
                                </div>
                                <hr />
                                <div style="padding-right: 0px; padding-left: 26px">
                                    <span><i id="infoicon" runat="server" class=" text-info fas fa-1x fa-info-circle"></i><span id="info" runat="server" class="text-xs font-weight-bold text-warning mx-1  mb-1">Values are in Million</span></span>
                                </div>
                            </div>

                        </div>

                        <div class="col-lg-4 mb-2">

                            <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
                                <div class="row align-items-center">
                                    <div class="col-12">

                                        <h6 class="text-xs font-weight-bolder text-primary text-uppercase mb-1">Top 5 Expenses<span class="mx-2 text-xs text-gray-300 text-uppercase">balance</span></h6>
                                    </div>
                                    <div class="col">
                                        <ul class="nav nav-pills justify-content-end">
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="card-body">

                                <main role="main" id="main1" runat="server">

                                    <div class="starter-template">
                                        <center>

                                            <p class="lead text-primary">
                                                <span class="fas fa-trash-alt text-gray-300 fa-4x"></span>
                                            </p>

                                            <h6 class="text-gray-300 text-xs" style="font-weight: bold">Sorry!! Nothing to show here.</h6>
                                        </center>
                                    </div>

                                </main>
                                <div class="chart">
                                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                </div>
                                <hr />
                                <div style="padding-right: 0px; padding-left: 26px">
                                    <span><i id="i1" runat="server" class=" text-info fas fa-1x fa-info-circle"></i><span id="Span4" runat="server" class="mx-2 text-xs font-weight-bold text-warning mb-1">Values are in Thousands</span></span>
                                </div>
                            </div>

                        </div>
                    </div>

                </div>


            </div>
        </div>




        <div class="row mt-5">
        </div>




    </div>

    <div class="modal fade" id="ModalAnalysis" tabindex="-1" role="dialog" aria-labelledby="ModalAnalysis" aria-hidden="true">
        <div class="modal-dialog modal-xl" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title font-weight-bold text-gray-900" id="exampleModalLabelG">AR-Graphical Analysis</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row mb-3">
                        <div class="col-md-6 ">
                            <main role="main" id="main2" runat="server">

                                <div class="starter-template">
                                    <center>

                                        <p class="lead text-primary">
                                            <span class="fas fa-chart-pie text-gray-300 fa-4x"></span>
                                        </p>

                                        <h6 class="text-gray-300 text-xs" style="font-weight: bold">Sorry!! Nothing to show here.</h6>
                                    </center>
                                </div>

                            </main>
                            <div class="chart-area">
                                <asp:Literal ID="ltrARPie" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="col-md-6 ">
                            <main role="main" id="main3" runat="server">

                                <div class="starter-template">
                                    <center>

                                        <p class="lead text-primary">
                                            <span class="fas fa-chart-bar text-gray-300 fa-4x"></span>
                                        </p>

                                        <h6 class="text-gray-300 text-xs" style="font-weight: bold">Sorry!! Nothing to show here.</h6>
                                    </center>
                                </div>

                            </main>
                            <div class="chart-area">
                                <asp:Literal ID="ltrARBar" runat="server"></asp:Literal>
                            </div>

                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <div class="modal fade" id="ModalShop" tabindex="-1" role="dialog" aria-labelledby="ModalAnalysis" aria-hidden="true">
        <div class="modal-dialog modal-xl" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <span class="fas fa-home text-white btn-circle btn-sm mx-2 mr-2 " style="background-color: #ff6a00"></span>
                    <h6 class="modal-title font-weight-bold mt-1 small text-uppercase text-danger" id="exampleModalLabelG">Shop & Customer</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row mb-3">
                        <div class="col-md-6 border-right ">
                            <span class="small text-gray-900 font-weight-bold " id="FreeShopSpan" runat="server">Free Shop<a href="addshop.aspx" data-toggle="tooltip" title="show details" data-placement="top"><span class="fas mx-2 fa-link text-danger"></span></a></span>
                            <div id="shopDiv" class="text-xs mt-3" runat="server">
                                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">

                                    <HeaderTemplate>

                                        <table class="table table-sm ">
                                            <thead>
                                                <tr>

                                                    <th scope="col">Shop No.</th>

                                                    <th scope="col">Area(m<sup>2</sup>)</th>
                                                    <th scope="col">Rate(ETB/Sqm*Month)</th>
                                                    <th scope="col">Total Rate(ETB/Month)</th>
                                                    <th scope="col">Status</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>

                                            <td class="text-left text-primary">
                                                <a href="shop_details.aspx?ref2=<%#Eval("shopno")%>"><%#Eval("shopno")%></a>
                                            </td>

                                            <td>
                                                <%# Eval("area","{0:N2}")%>
             
                                            </td>
                                            <td>
                                                <%# Eval("rate","{0:N2}")%>
            
                                            </td>
                                            <td class="text-gray-900">
                                                <%# Convert.ToDouble(Eval("monthlyprice")).ToString("#,##0.00")%>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("status")%>'></asp:Label>
                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                          </table>
                                    </FooterTemplate>

                                </asp:Repeater>
                            </div>
                            <main role="main" id="main4" runat="server">

                                <div class="starter-template">
                                    <center>

                                        <p class="lead text-primary">
                                            <span class="fas fa-home  fa-4x" style="color: #ff2cf4"></span>
                                        </p>

                                        <h6 class="text-gray-300 text-xs" style="font-weight: bold; color: #ff2ccd">Sorry!! No Free Shop Found.</h6>
                                    </center>
                                </div>

                            </main>

                        </div>
                        <div class="col-md-6 ">
                            <span class="small text-gray-900 font-weight-bold " id="CustomerWithCreditSpan" runat="server">Customer with credit <a href="creditnote.aspx" data-toggle="tooltip" title="show details" data-placement="top"><span class="fas mx-2 fa-link text-danger"></span></a></span>
                            <div id="DivCustomerDue" class="text-xs mt-3" runat="server">
                                <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">

                                    <HeaderTemplate>
                                        <div class="table-responsive">
                                            <table class="table align-items-center table-sm ">
                                                <thead>
                                                    <tr>


                                                        <th scope="col" class="text-gray-900">CUSTOMER</th>


                                                        <th scope="col" class=" text-gray-900">DATE</th>
                                                        <th>AGED</th>
                                                        <th>MOBILE</th>
                                                        <th scope="col" class="text-right">AMOUNT</th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <span class="text-primary mr-1"><a href="creditnotedetails.aspx?ref2=<%# Eval("id")%>&&cust=<%# Eval("customer")%>">CN#<%# Eval("id")%></a></span><span><asp:Label ID="Label2" runat="server" Text='<%# Eval("customer")%>'></asp:Label></span>
                                            </td>


                                            <td>
                                                <asp:Label ID="lblDueDate" runat="server" Text='<%# Eval("date","{0:MMMM dd, yyyy}")%>'></asp:Label>

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

                            <main role="main" id="main5" runat="server">

                                <div class="starter-template">
                                    <center>

                                        <p class="lead text-primary">
                                            <span class="fas fa-user-edit fa-4x" style="color: #ff2cf4"></span>
                                        </p>

                                        <h6 class="text-xs" style="font-weight: bold;color:#ff2ccd">Sorry!! Nothing to show here.</h6>
                                    </center>
                                </div>

                            </main>


                        </div>
                    </div>
                </div>

            </div>
        </div>
        <script>
            function myFunctionshop22() {
                var y = document.getElementById("<%=btnBindBussinessSumary.ClientID %>"); var x = document.getElementById("Pbutton");
                if (x.style.display === "none") {
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
        <script>
            function myFunctionshop222() {
                var y = document.getElementById("<%=btnCashSummary.ClientID %>"); var x = document.getElementById("Pbutton");
                if (x.style.display === "none") {
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
        <script>
            function myFunctionshop2222() {
                var y = document.getElementById("<%=btnInvSummary.ClientID %>"); var x = document.getElementById("Pbutton");
                if (x.style.display === "none") {
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
    </div>
</asp:Content>

