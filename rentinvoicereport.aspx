<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="rentinvoicereport.aspx.cs" Inherits="advtech.Finance.Accounta.rentinvoicereport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Invoice</title>
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
            var x = document.getElementById("myDIV5f");
            x.style.display = "none";
        });
    </script>
    <style>
        .water {
            opacity: 0.2;
            z-index: -1;
            transform: rotate(-45deg);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" id="CCF" runat="server" visible="false">
        <div class="text-center mt-5">
            <span class="fas fa-6x mb-2 fa-hand-holding-usd"></span>
            <h1>Invoice Couldn't be Found</h1>
            <p class="lead">Enter a correct invoice number and try again.</p>
            <p class="text-danger">eg: start with " i- " and add your invoce number or reference in top-bar search</p>
        </div>
    </div>
    <div class="container" id="NoInvoiceDiv" runat="server" visible="false">
        <div class="text-center mt-5">
            <span class="fas fa-6x mb-2 fa-hand-holding-usd text-gray-300"></span>
            <h1>No Invoice Created</h1>

        </div>
    </div>
    <div class="container-fluid pr-3 pl-3" id="container" runat="server">
        <!-- Table -->


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
                                        <asp:Button ID="btnUncollected" class="btn btn-danger w-50" runat="server" Text="Export" OnClick="btnUncollected_Click" />
                                    </center>



                                </div>
                            </div>
                        </div>


                    </div>

                </div>
            </div>
        </div>
        <div class="modal fade" id="exampleModalLongServicesss" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle3" aria-hidden="true">
            <div class="modal-dialog modal-sm" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title h6 font-weight-bold text-gray-900" id="exampleModalLongTitle3">Send Invoice</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">

                        <div class="row">
                            <div class="col-12">
                                <div class="form-group">
                                    <asp:TextBox ID="txtEmail" class="form-control form-control-sm" runat="server" Style="border-color: #00ff21" placeholder="Email adress..."></asp:TextBox>
                                </div>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <div class="form-group">
                                    <center>
                                        <asp:Button ID="btnSendEmail" class="btn btn-danger w-50" runat="server" Text="Send" OnClick="btnSendEmail_Click" />
                                    </center>



                                </div>
                            </div>
                        </div>


                    </div>

                </div>
            </div>
        </div>
        <div class="modal fade" id="exampleModal5" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabe5l" aria-hidden="true">
            <div class="modal-dialog modal-sm" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title text-gray-900 small font-weight-bold" id="exampleModalLabel5">Duplicate Invoice</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">


                            <div class="col-md-12">
                                <asp:Button ID="btnDuplicateInvoice" runat="server" class="btn btn-sm btn-success w-100" Text="Duplicate" OnClick="btnDuplicateInvoice_Click" />

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
                        <h5 class="modal-title" id="H1">Filter Record</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">

                            <div class="col-6">
                                <div class="form-group">
                                    <label class="font-weight-bold">From<span class="text-danger">*</span></label>


                                    <br />
                                    <div class="form-group mb-0">
                                        <div class="input-group input-group-alternative">
                                            <asp:TextBox ID="txtDatefrom" class="form-control " TextMode="Date" runat="server"></asp:TextBox>


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
                                            <asp:TextBox ID="txtDateto" class="form-control " TextMode="Date" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <center>
                            <button type="button" class="btn btn-secondary btn-sm " data-dismiss="modal">Close</button>
                            <asp:Button ID="btnUpdate" OnClick="btnUpdate_Click" class="btn btn-sm btn-primary" Text="Search" runat="server" />
                        </center>
                    </div>

                </div>
            </div>
        </div>
        <div class="modal fade" id="exampleModalDelete" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-sm" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title text-gray-900 h6" id="exampleModalLabel"><span class="fas fa-edit btn btn-circle btn-sm text-white mr-2" style="background-color: #9d469d"></span>Delete Invoice</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">

                            <div class="col-md-12">
                                <asp:Button ID="btnDelete1" runat="server" class="btn btn-sm text-white w-100" Style="background-color: #9d469d" Text="Remove..." OnClick="btnDelete_Click" />
                                <div class="custom-control mb-2 mt-2 mx-2 custom-checkbox font-weight-300">
                                    <input type="checkbox" class="custom-control-input" id="deleteCheck" runat="server" clientidmode="Static" />
                                    <label class="custom-control-label small mt-1 text-gray-900" for="deleteCheck">Adjust Ledger, Customer Statement & credit note</label>
                                </div>
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
        <div class="modal fade" id="exampleModalEdit" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-sm" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title text-gray-900 font-weight-bold h6" id="exampleModalLabel"><span class="fas fa-edit mr-2" style="color: #9d469d"></span>Edit Invoice</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-9">
                                <asp:TextBox ID="txtEditAmount" class="form-control form-control-sm mx-2" data-toggle="tooltip" data-placement="top" title="Don't Include Service Charge" placeholder="Total Amount {Service charge excluded}" runat="server"></asp:TextBox>
                                <div class="custom-control mb-2 mt-2 mx-2 custom-checkbox font-weight-300">
                                    <input type="checkbox" class="custom-control-input" id="Checkbox2" runat="server" clientidmode="Static" />
                                    <label class="custom-control-label small mt-1 text-gray-900" for="Checkbox2">Adjust Ledger, Customer Statement & credit note</label>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <asp:Button ID="btnEditInsert" runat="server" class="btn btn-sm text-white" Style="background-color: #9d469d" Text="Edit" OnClick="btnEdit_Click" />

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
                                <asp:TextBox ID="txtCustomerName" class="form-control mx-2" runat="server"></asp:TextBox>

                            </div>

                            <div class="col-md-5">
                                <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="search.." OnClick="Button2_Click" />

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
        <div class="modal fade" id="ModalDeleteAll" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-sm" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title text-gray-900 h6" id="exampleModalLabel"><span class="fas fa-edit btn btn-circle btn-sm text-white mr-2" style="background-color: #9d469d"></span>Delete All Invoice</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">

                            <div class="col-md-12">
                                <asp:Button ID="vtnDeleteAll" runat="server" class="btn btn-sm text-white w-100" Style="background-color: #9d469d" Text="Remove All Invoice..." OnClick="vtnDeleteAll_Click" />

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
            <div class="card-header bg-white " id="LeftDiv" runat="server">

                <div class="row align-items-center">
                    <div class="col-5">
                        <a class="btn btn-light btn-circle mr-2" id="buttonback" href="rentinvoicereport.aspx" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back to Home">

                            <span class="fa fa-arrow-left text-danger"></span>

                        </a>
                        <span class="badge badge-danger mr-2" id="InvoiceBadge" runat="server"></span>
                        <a id="Link" runat="server">
                            <span class="badge badge-success " id="PaymensStatus" runat="server"></span>
                        </a>
                        <button type="button" runat="server" id="Spvb" class="mr-2 btn btn-sm btn-default btn-circle" data-toggle="modal" data-target="#ModalHeader">
                            <div>
                                <i class="fas fa-cog fa-2x font-weight-bold" style="color: #b868bb;" data-toggle="tooltip" title="Customize Invoice"></i>
                                <span></span>
                            </div>
                        </button>
                        <button type="button" runat="server" id="btnDeleteAll"  class="mr-2 btn btn-sm btn-danger btn-circle" data-toggle="modal" data-target="#ModalDeleteAll">
                            <div>
                                <i class="fas fa-trash fa-1x font-weight-bold text-white" data-toggle="tooltip" title="Delete All Invoice"></i>
                                <span></span>
                            </div>
                        </button>
                    </div>
                    <div class="col-7 text-right" id="RightDiv" runat="server">

                        <button runat="server" id="btnEdit" type="button" visible="false" class="btn btn-circle mx-1  btn-sm text-xs" style="background-color: #9d469d;" data-toggle="modal" data-target="#exampleModalEdit">
                            <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Edit invoice">
                                <div>
                                    <i class="fas fa-edit text-white"></i>

                                </div>
                            </a>
                        </button>
                        <button runat="server" id="btnDelete" type="button" visible="false" class="btn btn-circle mx-1  btn-sm text-xs btn-danger" data-toggle="modal" data-target="#exampleModalDelete">
                            <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Delete invoice">
                                <div>
                                    <i class="fas fa-trash text-white"></i>

                                </div>
                            </a>
                        </button>
                        <button runat="server" id="modalMain" type="button" class="btn btn-circle mx-1  btn-sm text-xs btn-secondary" data-toggle="modal" data-target="#exampleModalLongService">
                            <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Export as Excel">
                                <div>
                                    <i class="fas fa-file-excel text-white"></i>

                                </div>
                            </a>
                        </button>
                        <button type="button" runat="server" id="Button5" visible="false" class=" mx-1 btn btn-sm btn-success btn-circle" data-toggle="modal" data-target="#exampleModalSMS">
                            <a class="nav-link" data-toggle="tooltip" data-placement="bottom" title="Print using POS">
                                <div>
                                    <i class="fas fa-print text-white font-weight-bold"></i>
                                    <span></span>
                                </div>
                            </a>
                        </button>


                        <button type="button" runat="server" id="Button4" visible="false" class=" mx-1 btn btn-sm btn-danger btn-circle" data-toggle="modal" data-target="#exampleModalLongServicesss">
                            <a class="nav-link" data-toggle="tooltip" data-placement="bottom" title="Send Invoice">
                                <div>
                                    <i class="fas fa-envelope text-white font-weight-bold"></i>
                                    <span></span>
                                </div>
                            </a>
                        </button>
                        <button type="button" runat="server" id="Button3" visible="false" class=" mx-1 btn btn-sm btn-danger btn-circle" data-toggle="modal" data-target="#exampleModal5">
                            <a class="nav-link" data-toggle="tooltip" data-placement="bottom" title="Duplicate Invoice">
                                <div>
                                    <i class="fas fa-copy text-white font-weight-bold"></i>
                                    <span></span>
                                </div>
                            </a>
                        </button>
                        <button type="button" runat="server" id="Button1" class=" mx-1  btn btn-sm btn-success btn-circle" data-toggle="modal" data-target=".bd-example-modal-lg">
                            <a class="nav-link" data-toggle="tooltip" data-placement="bottom" title="Search Cash Receipt by Date range">
                                <div>
                                    <i class="fas fa-search text-white font-weight-bold"></i>
                                    <span></span>
                                </div>
                            </a>
                        </button>
                        <button type="button" runat="server" id="Sp2" class="mx-1  btn btn-sm btn-warning btn-circle" data-toggle="modal" data-target="#exampleModal">
                            <a class="nav-link" data-toggle="tooltip" data-placement="bottom" title="Search cash receipt by Customer Name">
                                <div>
                                    <i class="fas fa-search-location text-white font-weight-bold"></i>
                                    <span></span>
                                </div>
                            </a>
                        </button>
                        <button name="b_print" onclick="printdiv('div_print');" type="button" title="Print" class="mx-1  btn btn-sm btn-danger btn-circle" data-toggle="modal" data-target="#exampleModalCenter">
                            <div>
                                <i class="fas fa-print text-white font-weight-bold"></i>

                            </div>
                        </button>

                    </div>
                </div>

            </div>
            <div id="div_print">

                <div class="bg-white rounded-lg mb-4 " id="BodyDiv" runat="server">



                    <asp:Label ID="lblMsg" runat="server"></asp:Label>


                    <div class="row mt-1" id="MarginDiv" runat="server">
                        <div class="col-1" id="left">
                        </div>
                        <div class="col-10 mt-3   text-gray-900" id="middle">
                            <div id="con1" runat="server">
                                <div class="row mx-2 mr-2">
                                    <div class="col-md-6  text-left">
                                        <img class="" src="../../asset/Brand/gh.jpg" alt="" id="LogoImage" runat="server" />
                                        <div class="row">
                                            <div class="col-md-12 border-top border-dark">
                                                <span class="text-gray-900  text-uppercase font-weight-bold" id="HeaderRaks" runat="server">Raksym trading plc</span>
                                                <br />
                                                <div id="Body1" runat="server">
                                                    <span class="fas fa-address-book mb-2 text-gray-500 mr-1"></span><span class="  text-uppercase  font-weight-bold text-gray-900 mt-1" id="CompAddress" runat="server"></span>
                                                    <br />
                                                    <span class="fas fa-phone mb-2 text-gray-500 mr-1"></span><span class="  text-uppercase   font-weight-bold text-gray-900 mt-1" id="Contact" runat="server"></span>
                                                    <br />
                                                    <span id="VT" runat="server" class="border-top mb-2 mt-1">VENDOR TIN<span class="fas border-top m-1 fa-hashtag text-gray-300 ml-1"></span><span id="VendorTIN" runat="server"></span></span>
                                                    <br />
                                                    <span class="  text-gray-900 border-top border-bottom font-weight-bold" id="RefTag" runat="server"></span>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mt-1  ">
                                            <div class="col-md-12 text-left">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6 text-right">

                                        <span class=" text-uppercase text-gray-900 font-weight-bold" id="HeaderInv" runat="server">INVOICE</span><br />
                                        <span id="invocenumber" runat="server"></span>
                                        <div id="Body2" runat="server">
                                            <span id="BindShop" runat="server" visible="false">
                                                <span class="text-gray-900 " style="height: 100px">To: </span><span style="height: 100px" class=" mx-2 text-gray-900 font-weight-bold font-italic" id="Name" runat="server"></span>
                                                [<span class="text-gray-900 text-uppercase font-weight-bold mt-3">Shop No: </span><span class="text-gray-900 text-uppercase mx-1 font-weight-bold mt-3" id="ShopNo" runat="server"></span>]
                                            </span>
                                            <br />
                                            <span id="Addressbar" visible="false" runat="server">ADDRESS: <span id="Address" class="  text-uppercase" contenteditable="true" runat="server"></span></span>
                                            <h4><span class="text-uppercase text-gray-900 font-weight-bold" id="InvNoBinding" runat="server"></span><span id="Span2" runat="server"></span></h4>
                                            <h4><span class="text-uppercase text-gray-900 font-weight-bold" id="FSno" contenteditable="true" runat="server"></span><span id="Span3" runat="server"></span></h4>
                                            <h6 title="print date" id="printdate" runat="server"></h6>
                                            <span id="datFrom1" runat="server" class="mb-1 border-bottom border-top text-gray-900 font-weight-bold"></span><span id="tomiddle" class="mb-1 mr-2 ml-2 border-bottom border-top text-gray-900 font-weight-bold" runat="server">To</span><span id="datTo" class="mb-1 text-gray-900 border-bottom border-top font-weight-bold" runat="server"></span>

                                            <span id="CustomerTIN" visible="false" runat="server" width="200px">CUSTOMER TIN<span class="fas fa-hashtag text-gray-300 ml-1"></span><span id="TINNUMBER" width="200px" contenteditable="true" runat="server" class="ml-1"></span></span>
                                            <br />
                                            <span id="PayMode" visible="true" runat="server" class="mt-2 "><i class=" fas fa-dollar-sign text-dark "></i><span class="mx-1"><span class="font-weight-bold   text-uppercase">Payment Mode:</span> <span id="PaymentMode" class="  text-uppercase" runat="server"></span></span></span>

                                        </div>
                                    </div>
                                </div>
                                <div id="conw" runat="server">
                                    <div class="card-body border-none">
                                        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">

                                            <HeaderTemplate>

                                                <table class="table align-items-center table-sm table-bordered">
                                                    <thead class="thead-dark ">
                                                        <tr>
                                                            <th scope="col" class="">#</th>
                                                            <th scope="col" class="">Customer</th>
                                                            <th scope="col" class="text-left text-white ">
                                                                <asp:LinkButton ID="LinkButton3" CssClass="text-white" runat="server" CommandName="date">Invoice Date</asp:LinkButton></th>
                                                            <th scope="col" class="">Pre-Tax</th>
                                                            <th scope="col" class="">VAT(15%)</th>

                                                            <th scope="col" class=" text-right">Amount</th>

                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>

                                                    <td class="text-primary text-left">
                                                        <%# Eval("id")%>
                                                        <asp:Label ID="lblSC" runat="server" Visible="false" Text='<%# Eval("vatfree")%>'></asp:Label>

                                                    </td>

                                                    <td class="text-primary text-left">
                                                        <a href="rentinvoicereport.aspx?id= <%# Eval("id2")%>&&cust=<%# Eval("customer")%>&&paymentmode=<%# Eval("payment_mode")%>" data-toggle="tooltip" data-placement="top" title="<%# Eval("customer")%> details">
                                                            <asp:Label ID="lblCust" runat="server" Text='<%# Eval("customer")%>'></asp:Label></a>

                                                    </td>
                                                    <td class="text-gray-900 text-left">
                                                        <%# Eval("date", "{0: MMMM dd,yyyy}")%>
                       
                    
                                                    </td>
                                                    <td class="text-gray-900">
                                                        <asp:Label ID="lblcatminus" Visible="true" runat="server" Text='<%# ((Convert.ToDouble(Eval("paid"))-Convert.ToDouble(Eval("vatfree")))/1.15).ToString("#,##0.00") %>'></asp:Label>

                                                    </td>
                                                    <td class="text-gray-900">
                                                        <asp:Label ID="lblVAT" runat="server" Text='<%# (Convert.ToDouble(Eval("paid"))-Convert.ToDouble(Eval("vatfree"))-((Convert.ToDouble(Eval("paid"))-Convert.ToDouble(Eval("vatfree")))/1.15)).ToString("#,##0.00") %>'></asp:Label>
                                                    </td>



                                                    <td class="text-gray-900 text-right">
                                                        <asp:Label ID="lblvatplus" Visible="true" runat="server" Text='<%# (Convert.ToDouble(Eval("paid"))-Convert.ToDouble(Eval("vatfree"))).ToString("#,##0.00")%>'></asp:Label>

                                                    </td>
                                                </tr>

                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
              </table>
                                            </FooterTemplate>
                                        </asp:Repeater>

                                        <div class="row" id="TotalRow" runat="server">
                                            <div class="col-md-8 text-left">
                                                <p class="small text-danger">* If you have any disagreement with the invoice please contact us</p>
                                                <div class="row">
                                                    <div class="col-md-12 text-left">
                                                        <span id="CreditDiv" runat="server" class="fas fa-arrow-circle-right text-gray-400 mr-2"></span><span id="CreditDiv2" runat="server">CREDIT BALANCE: [<span id="credittotal" class="font-weight-bold" runat="server"></span>]</span>
                                                    </div>

                                                </div>
                                                <center>
                                                    <span class="mx-5 invisible">1</span><h5 id="RaksTDiv" runat="server" class="water   font-weight-bolder mx-lg-5" style="padding-right: 0px; padding-left: 300px; font-size: 70px;" visible="false">ATTACHMENT</h5>
                                                </center>


                                            </div>

                                            <div class="col-md-4 mt-1">
                                                <div class="form-group">
                                                    <table class="table table-sm table-bordered  ">
                                                        <tbody>
                                                            <tr>
                                                                <td><span style="margin: 7px 5px 5px 5px; padding: 5px" class="m-0 font-weight-bold text-right text-gray-900">Sub-Total:</span></td>
                                                                <td class="text-right"><span id="VATfree" class="text-gray-900 font-weight-bold text-gray-900" runat="server"></span></td>
                                                            </tr>
                                                            <tr>
                                                                <td><span style="margin: 7px 5px 5px 5px; padding: 5px" class="m-0 font-weight-bold text-right text-gray-900 ">VAT(15%):</span></td>
                                                                <td class="text-right"><span id="VAT" class="text-gray-900 font-weight-bold text-gray-900" runat="server"></span></td>
                                                            </tr>

                                                            <tr>
                                                                <td><span style="margin: 7px 5px 5px 5px; padding: 5px" class="m-0 font-weight-bold border border-bottom text-right text-gray-900 ">Grand Total:</span></td>
                                                                <td class="text-right"><span id="Total" class="text-gray-900 font-weight-bold text-gray-900" runat="server"></span></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>

                                                </div>

                                            </div>
                                        </div>
                                        <div class="row " id="NotesDiv" runat="server" visible="false">
                                            <div class="col-md-7 text-left font-italic font-weight-bold">
                                                <span class="border-bottom mb-2">Approved By:_________________________</span><br />
                                                <br />
                                                <span>Signature_________________________</span>
                                            </div>
                                            <div class="col-md-5 text-right font-italic font-weight-bold">
                                                <span class="border-bottom mb-2 text-right">Prepared By:_________________________</span><br />
                                                <br />
                                                <span class=" text-right">Signature_________________________</span>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <main class="mt-4" role="main" id="main2" runat="server">
                                <center>
                                    <div class="starter-template">


                                        <p class="lead">
                                            <span class="fas fa-hand-holding-usd text-gray-400 fa-6x"></span>
                                        </p>
                                    </div>
                                    <h6 class="text-gray-400 text-center h6 mt-2">No Invoice found.</h6>
                                </center>


                            </main>
                            <div class="card-footer bg-white py-4">
                                <nav aria-label="...">
                                    <ul class="pagination justify-content-end mb-0">
                                        <br />
                                        <td>
                                            <asp:Label ID="Label1" runat="server" class="m-1 text-primary"></asp:Label></td>
                                        <br />
                                        <li class="page-item active">

                                            <asp:Button ID="btnPrevious" class="btn btn-sm btn-primary btn-circle" runat="server" Text="<" OnClick="btnPrevious_Click" />

                                        </li>
                                        <li class="page-item active">

                                            <asp:Button ID="btnNext" class="btn btn-sm btn-primary btn-circle mx-2" runat="server" Text=">" OnClick="btnNext_Click" />

                                        </li>

                                    </ul>
                                </nav>
                            </div>

                        </div>
                        <div class="col-1" id="right">
                        </div>
                        <div class="mt-2 mb-2 border-top border-dark">
                        </div>
                    </div>

                    <div class="row " id="DuplicateRow" runat="server">
                        <div class="col-1">
                        </div>
                        <div class="col-10 text-gray-900">

                            <div class="row mx-2 mr-2">
                                <div class="col-md-6  text-left">
                                    <img class="" src="../../asset/Brand/gh.jpg" alt="" id="LogoImage1" runat="server">

                                    <div class="row">
                                        <div class="col-md-12 border-top border-dark">
                                            <h5 class="text-gray-900  text-uppercase font-weight-bold" id="HeaderRaksDup" runat="server">Raksym trading plc</h5>
                                            <div id="body1dup" runat="server">
                                                <span class="fas fa-address-book text-gray-500 mr-1"></span><span class="  text-uppercase  font-weight-bold text-gray-900 mt-1" id="Ad2" runat="server"></span>
                                                <br />
                                                <span class="fas fa-phone text-gray-500 mr-1"></span><span class="  text-uppercase   font-weight-bold text-gray-900 mt-1" id="Ct2" runat="server"></span>
                                                <br />
                                                <span class="border-top mt-1 mb-1">VENDOR TIN<span class="fas border-top m-1 fa-hashtag text-gray-300 ml-1"></span><span id="DupVendorPIN" runat="server"></span></span>
                                                <br />

                                                <h6><span class="  text-gray-900 font-weight-bold" id="Ref2" runat="server"></span></h6>


                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <div class="col-md-6 text-right">
                                    <h2><span class="h1 text-uppercase text-gray-900 font-weight-bold" id="HeaderInvDup" runat="server">INVOICE</span><span id="Span5" runat="server"></span></h2>
                                    <div id="body2dup" runat="server">
                                        <div class="row mt-3  ">
                                            <div class="col-md-12 text-right">
                                                <span class=" text-uppercase text-gray-900 " style="height: 100px">To: </span><span style="height: 100px" class="text-uppercase mx-2 text-gray-900 font-weight-bold font-italic" id="Name1" runat="server"></span>
                                                [<span class="text-gray-900 text-uppercase font-weight-bold mt-3">Shop No: </span><span class="text-gray-900 text-uppercase mx-1 font-weight-bold mt-3" id="shopno1" runat="server"></span>]
                                            <br />
                                                <span>ADDRESS: <span id="DupAddress" class="  text-uppercase" contenteditable="true" runat="server"></span></span>
                                            </div>


                                        </div>
                                        <h4><span class="h5 text-uppercase text-gray-900 font-weight-bold" id="InVNo" runat="server"></span><span id="Span7" runat="server"></span></h4>
                                        <h4><span class="h5 text-uppercase text-gray-900 font-weight-bold" id="FSno1" contenteditable="true" runat="server"></span><span id="Span32" runat="server"></span></h4>
                                        <h6 title="print date"></h6>
                                        <span>CUSTOMER TIN<span class="fas fa-hashtag text-gray-300 ml-1"></span><span id="DupCustomerPIN" width="200px" contenteditable="true" runat="server" class="ml-1"></span></span>
                                        <br />
                                        <span id="PayMode1" visible="false" runat="server" class="mt-2 "><i class=" fas fa-dollar-sign text-dark "></i><span class="mx-1"><span class="font-weight-bold">Payment Mode:</span> <span id="PaymentMode1" runat="server"></span></span></span>

                                    </div>
                                </div>
                            </div>

                            <div class="card-body " id="conx" runat="server">
                                <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound" OnItemCommand="Repeater1_ItemCommand">

                                    <HeaderTemplate>

                                        <table class="table align-items-center table-bordered table-sm">
                                            <thead class="thead-dark ">
                                                <tr>
                                                    <th scope="col" class="">#</th>
                                                    <th scope="col" class="">Customer</th>
                                                    <th scope="col" class="text-left text-white ">
                                                        <asp:LinkButton ID="LinkButton3" CssClass="text-white" runat="server" CommandName="date">Invoice Date</asp:LinkButton></th>
                                                    <th scope="col" class="">Pre-Tax</th>
                                                    <th scope="col" class="">VAT(15%)</th>

                                                    <th scope="col" class=" text-right">Amount</th>

                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>

                                            <td class="text-primary text-left">
                                                <%# Eval("id")%>
                    
                                            </td>

                                            <td class="text-primary text-left">
                                                <a href="rentinvoicereport.aspx?id= <%# Eval("id2")%>&&cust=<%# Eval("customer")%>" data-toggle="tooltip" data-placement="top" title="<%# Eval("customer")%> details">
                                                    <asp:Label ID="lblCust" runat="server" Text='<%# Eval("customer")%>'></asp:Label></a>

                                            </td>
                                            <td class="text-gray-900 text-left">
                                                <%# Eval("date", "{0: MMMM dd,yyyy}")%>
                       
                    
                                            </td>
                                            <td class="text-gray-900">
                                                <asp:Label ID="lblcatminus" Visible="true" runat="server"></asp:Label>

                                            </td>
                                            <td class="text-gray-900">
                                                <asp:Label ID="lblVAT" runat="server" Text="Label"></asp:Label>
                                                <asp:Label ID="lblSC" runat="server" Visible="false" Text='<%# Eval("vatfree")%>'></asp:Label>
                                            </td>



                                            <td class="text-gray-900 text-right">
                                                <asp:Label ID="lblvatplus" Visible="true" runat="server" Text='<%# Eval("paid")%>'></asp:Label>

                                            </td>
                                        </tr>

                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
              </table>
                                    </FooterTemplate>
                                </asp:Repeater>


                                <div class="row" id="Div1" runat="server">
                                    <div class="col-md-8 text-left">
                                        <p class="small text-danger">* If you have any disagreement with the invoice please contact us</p>
                                        <center>
                                            <h5 id="RaksTDiv2" runat="server" class="water   font-weight-bolder mx-lg-5" style="padding-right: 0px; padding-left: 300px; font-size: 70px;">ATTACHMENT</h5>
                                        </center>
                                    </div>

                                    <div class="col-md-4 ">
                                        <div class="form-group">
                                            <table class="table table-sm table-bordered  ">
                                                <tbody>
                                                    <tr>
                                                        <td><span style="margin: 7px 5px 5px 5px; padding: 5px" class="m-0 font-weight-bold text-right text-gray-900">Sub-Total:</span></td>
                                                        <td class="text-right"><span id="DupVatFree" class="text-gray-900 font-weight-bold text-gray-900" runat="server"></span></td>
                                                    </tr>
                                                    <tr>
                                                        <td><span style="margin: 7px 5px 5px 5px; padding: 5px" class="m-0 font-weight-bold text-right text-gray-900 ">VAT(15%):</span></td>
                                                        <td class="text-right"><span id="DupVAT" class="text-gray-900 font-weight-bold text-gray-900" runat="server"></span></td>
                                                    </tr>

                                                    <tr>
                                                        <td><span style="margin: 7px 5px 5px 5px; padding: 5px" class="m-0 font-weight-bold text-right text-gray-900 ">Grand Total:</span></td>
                                                        <td class="text-right"><span id="DupGrandTotal" class="text-gray-900 font-weight-bold text-gray-900" runat="server"></span></td>
                                                    </tr>
                                                </tbody>
                                            </table>

                                        </div>

                                    </div>
                                </div>
                                <div class="row border-top" id="Div2" runat="server" visible="true">
                                    <div class="col-md-7 text-left font-italic font-weight-bold">
                                        <span class="border-bottom mb-2">Approved By:_________________________</span><br />
                                        <br />
                                        <span>Signature_________________________</span>
                                    </div>
                                    <div class="col-md-5 text-right font-italic font-weight-bold">
                                        <span class="border-bottom mb-2 text-right">Prepared By:_________________________</span><br />
                                        <br />
                                        <span class=" text-right">Signature_________________________</span>
                                    </div>
                                </div>
                            </div>



                        </div>

                        <div class="col-1">
                        </div>
                    </div>
                </div>


                <div class="modal fade" id="exampleModalSMS" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel8shopSMS" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content rounded-4 shadow">
                            <div class="modal-body p-4 text-center">
                                <h5 class="mb-0 text-gray-900 font-weight-bold">Print using POS</h5>
                                <hr />
                                <div id="installedPrinters">
                                    <label for="installedPrinterName" class="text-danger small mb-2">Select POS Printer:</label>
                                    <select name="installedPrinterName" class=" form-control form-control-sm" id="installedPrinterName"></select>
                                </div>

                            </div>
                            <div class="modal-footer flex-nowrap p-0">

                                <button class="btn btn-lg font-weight-bold  btn-link fs-6 text-decoration-none col-6 m-0 rounded-0 border-right" onclick="print2();">Yes, Print</button>

                                <button type="button" class="btn btn-lg btn-link fs-6 text-decoration-none col-6 m-0 rounded-0" data-dismiss="modal">No thanks</button>
                            </div>
                        </div>
                    </div>

                </div>

                <div class="modal fade" id="ModalHeader" tabindex="-1" role="dialog" aria-labelledby="MHeader" aria-hidden="true">
                    <div class="modal-dialog modal-md" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h6 class="modal-title text-gray-900 h6 text-uppercase font-weight-bold" id="MHeader"><span class="fas fa-edit text-gray-300 mr-2 text-uppercase"></span>Edit HEADer and footer Content</h6>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="card-body border-left border-primary">
                                    <div class="row mb-3">
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtHeadingEdit" class="form-control form-control-sm" Height="40px" runat="server" Text="RAKSYM TRADING PLC [INVOICE]" ReadOnly="true"></asp:TextBox>
                                            <hr />

                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtInvoiceName" data-toggle="tooltip" title="HEADING NAME" class="form-control form-control-sm" Style="border-color: #b868bb" runat="server" ReadOnly="false"></asp:TextBox>
                                            <hr />

                                        </div>
                                    </div>
                                    <div class="row">

                                        <div class="col-4">
                                            <div class="form-group">

                                                <div class="input-group input-group-alternative">

                                                    <div class="form-group mb-0">
                                                        <div class="input-group input-group-alternative input-group-sm">
                                                            <asp:TextBox ID="txtFontsize" class="form-control form-control-sm" data-toggle="tooltip" Style="border-color: #ff0000;" data-placement="bottom" title="font size" TextMode="Number" Height="25px" Width="70px" runat="server" Text="12"></asp:TextBox>

                                                            <div class="input-group-prepend " style="height: 25px">
                                                                <span class="input-group-text ">px</span>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-4">
                                            <div class="form-group">

                                                <div class="input-group input-group-alternative">

                                                    <div class="form-group mb-0">
                                                        <div class="input-group input-group-alternative input-group-sm">
                                                            <asp:TextBox ID="txtLineHeight" class="form-control form-control-sm" data-toggle="tooltip" Style="border-color: #ff0000;" data-placement="bottom" title="line height" TextMode="Number" Height="25px" Width="70px" runat="server" Text="12"></asp:TextBox>

                                                            <div class="input-group-prepend " style="height: 25px">
                                                                <span class="input-group-text ">px</span>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-4">
                                            <div class="form-group">

                                                <div class="input-group input-group-alternative">

                                                    <div class="form-group mb-0">
                                                        <div class="input-group input-group-alternative input-group-sm">
                                                            <asp:TextBox ID="txtMarigin" class="form-control form-control-sm" data-toggle="tooltip" Style="border-color: #ff0000;" data-placement="bottom" title="Marigin" TextMode="Number" Height="25px" Width="70px" runat="server" Text="12"></asp:TextBox>

                                                            <div class="input-group-prepend " style="height: 25px">
                                                                <span class="input-group-text ">px</span>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                        </div>

                                    </div>
                                </div>
                                <div class="card-body border-left border-danger">
                                    <h6 class="text-gray-900 small mb-3">Edit Body Section</h6>
                                    <div class="row">
                                        <div class="col-6">
                                            <div class="form-group">

                                                <div class="input-group input-group-alternative">

                                                    <div class="form-group mb-0">
                                                        <div class="input-group input-group-alternative input-group-sm">
                                                            <asp:TextBox ID="txtBodyFontSize" class="form-control form-control-sm" data-toggle="tooltip" Style="border-color: #ff0000;" data-placement="bottom" title="font size" TextMode="Number" Height="25px" Width="90px" runat="server" Text="12"></asp:TextBox>

                                                            <div class="input-group-prepend " style="height: 25px">
                                                                <span class="input-group-text ">px</span>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                        </div>

                                    </div>
                                </div>
                                <div class="card-body border-left border-danger">
                                    <h6 class="text-gray-900 small mb-3">Logo Visibility</h6>
                                    <div class="row">
                                        <div class="col-1">
                                            <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                                <input type="checkbox" class="custom-control-input" id="logoCheck" runat="server" clientidmode="Static" />
                                                <label class="custom-control-label small text-danger font-weight-bolder" for="logoCheck">Visible</label>
                                            </div>

                                        </div>
                                        <div class="col-5 mx-5">
                                            <div class="form-group">

                                                <div class="input-group input-group-alternative">

                                                    <div class="form-group mb-0">
                                                        <div class="input-group input-group-alternative input-group-sm">
                                                            <asp:TextBox ID="txtLogoSize" class="form-control form-control-sm" data-toggle="tooltip" Style="border-color: #ff0000;" data-placement="bottom" title="Logo size" TextMode="Number" Height="25px" Width="70px" runat="server" Text="12"></asp:TextBox>

                                                            <div class="input-group-prepend " style="height: 25px">
                                                                <span class="input-group-text ">px</span>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                        </div>

                                    </div>
                                </div>
                                <div class="card-body border-left border-secondary">

                                    <div class="row">
                                        <div class="col-5">
                                            <h6 class="text-gray-900 small mb-3">Credit balance Visibility</h6>
                                            <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                                <input type="checkbox" class="custom-control-input" id="creditCheck" runat="server" clientidmode="Static" />
                                                <label class="custom-control-label small text-danger font-weight-bolder" for="creditCheck">Visible</label>
                                            </div>

                                        </div>
                                        <div class="col-5">
                                            <h6 class="text-gray-900 small mb-3">Watermark Visibility</h6>
                                            <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                                <input type="checkbox" class="custom-control-input" id="waterCheck" runat="server" clientidmode="Static" />
                                                <label class="custom-control-label small text-danger font-weight-bolder" for="waterCheck">Visible</label>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                            </div>
                            <center>
                                <div class="modal-footer">
                                    <div id="myDIV5f" class="spinner-border text-danger spinner-border-sm  mr-4 mt-2" role="status">
                                        <span class="sr-only">Loading.ffrfyyrg..</span>
                                    </div>
                                    <asp:Button ID="btnCustomizeSave" runat="server" class="btn btn-sm btn-success " OnClick="btnCustomizeSave_Click" OnClientClick="myFunctionshop1()" Text="Save Changes" />


                                </div>

                            </center>
                        </div>
                    </div>
                </div>



            </div>
        </div>
        <script type="text/javascript">

            function HideORshowDiv() {
                var left = document.getElementById('left');
                var middle = document.getElementById('middle');
                var right = document.getElementById('right');
                if (left.className == "col-2") {
                    left.className = "col-1";
                    right.className = "co-10 mt-3 small text-gray-900";
                    middle.className = "col-1";
                }
                else {
                    left.className = "col-2";
                    right.className = "co-8 mt-3 small text-gray-900";
                    middle.className = "col-2";
                }
            }
        </script>
        <script src="../../asset/js/JSPrintManager.js"></script>
        <script src="../../asset/js/bluebird.min.js"></script>
        <script src="../../asset/js/jquery-3.2.1.slim.min.js"></script>
        <script>
            function myFunctionshop1() {
                var x = document.getElementById("myDIV5f");
                if (x.style.display === "none") {
                    x.style.display = "block";
                } else {
                    x.style.display = "none";
                }
            }
        </script>
        <script>

            //WebSocket settings
            //JSPM.JSPrintManager.auto_reconnect = true;
            // JSPM.JSPrintManager.start();
            //JSPM.JSPrintManager.WS.onStatusChanged = function () {
            // if (jspmWSStatus()) {
            //get client installed printers
            //  JSPM.JSPrintManager.getPrinters().then(function (myPrinters) {
            //   var options = '';
            //  for (var i = 0; i < myPrinters.length; i++) {
            //     options += '<option>' + myPrinters[i] + '</option>';
            // }
            // $('#installedPrinterName').html(options);
            //});
            //}
            //};

            //Check JSPM WebSocket status
            //function jspmWSStatus() {
            //if (JSPM.JSPrintManager.websocket_status == JSPM.WSStatus.Open)
            //return true;
            //else if (JSPM.JSPrintManager.websocket_status == JSPM.WSStatus.Closed) {
            //alert('JSPrintManager (JSPM) is not installed or not running! Download JSPM Client App from http://192.168.1.254:9300/Finance/Accounta/app/jspm4-4.0.22.512-win.exe');
            //return false;
            //}
            //else if (JSPM.JSPrintManager.websocket_status == JSPM.WSStatus.Blocked) {
            //alert('JSPM has blocked this website!');
            //return false;
            //}
            //}

            //Do printing...
            function print2(o) {
                var tot = document.getElementById("<%=Total.ClientID %>");
                var pre = document.getElementById("<%=VATfree.ClientID %>");
                var Vat = document.getElementById("<%=VAT.ClientID %>");
                var inv = document.getElementById("<%=InvNoBinding.ClientID %>");
                var contact = document.getElementById("<%=Contact.ClientID %>");
                var Tin = document.getElementById("<%=VendorTIN.ClientID %>");
                var FSNo = document.getElementById("<%=FSno.ClientID %>");
                var ref = document.getElementById("<%=RefTag.ClientID %>");

                if (jspmWSStatus()) {
                    //Create a ClientPrintJob
                    var cpj = new JSPM.ClientPrintJob();
                    //Set Printer type (Refer to the help, there many of them!)
                    if ($('#useDefaultPrinter').prop('checked')) {
                        cpj.clientPrinter = new JSPM.DefaultPrinter();
                    } else {
                        cpj.clientPrinter = new JSPM.InstalledPrinter($('#installedPrinterName').val());
                    }
                    //Set content to print...
                    //Create ESP/POS commands for sample label
                    var esc = '\x1B'; //ESC byte in hex notation
                    var newLine = '\x0A'; //LF byte in hex notation
                    var cmds = esc + "@"; //Initializes the printer (ESC @)
                    cmds += esc + '!' + '\x38';
                    cmds += '   TIN: ' + Tin.innerHTML; //text to print
                    cmds += newLine;
                    cmds += esc + '!' + '\x38';
                    cmds += '   RAKSYM TRADING PLC'; //text to print
                    cmds += newLine;
                    cmds += esc + "E";
                    cmds += '   Bole \k 03 \H. No 088'; //text to print
                    cmds += newLine;
                    cmds += contact.innerHTML; //text to print
                    cmds += newLine + newLine;
                    cmds += FSNo.innerHTML; //text to print
                    cmds += newLine;
                    cmds += 'date:' + date.innerHTML;
                    cmds += newLine;
                    cmds += newLine;
                    cmds += esc + '!' + '\x00';
                    cmds += ref.innerHTML;
                    cmds += newLine;
                    cmds += '**Cash Sales Invoice**';
                    cmds += newLine;
                    cmds += inv.innerHTML;
                    cmds += newLine;
                    cmds += esc + '##############' + '\x00'; //Character font A selected (ESC ! 0)
                    cmds += newLine;
                    cmds += '1x' + pre.innerHTML + " =";
                    cmds += newLine;
                    cmds += 'TXBL1      *' + pre.innerHTML;
                    cmds += newLine;
                    cmds += 'TAX1 15%   *' + Vat.innerHTML;
                    cmds += newLine;
                    cmds += esc + '!' + '\x38';
                    cmds += 'TOTAL:     *' + tot.innerHTML;
                    cmds += newLine;
                    cmds += '------------------';
                    cmds += newLine;
                    cmds += 'CASH:      *' + tot.innerHTML;
                    cmds += newLine;
                    cmds += 'ITEM#';
                    cmds += newLine + newLine;
                    cmds += '#####################';
                    cmds += newLine;
                    cmds += '    THANK YOU!!!';
                    cmds += newLine;
                    cmds += esc + '!' + '\x00'; //Character font A selected (ESC ! 0)

                    cpj.printerCommands = cmds;
                    //Send print job to printer!
                    cpj.sendToClient();
                }
            }

        </script>

    </div>
</asp:Content>
