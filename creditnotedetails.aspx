<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="creditnotedetails.aspx.cs" Inherits="advtech.Finance.Accounta.creditnotedetails" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        window.addEventListener('load', (event) => {
            var x = document.getElementById("Bd");
            x.style.display = "none";
        });
        window.addEventListener('load', (event) => {
            var x = document.getElementById("Pbutton");
            x.style.display = "none";
    </script>
    <script type="text/javascript">
            window.addEventListener('load', (event) => {
                var x = document.getElementById("Bd1");
                x.style.display = "none";
            });
            window.addEventListener('load', (event) => {
                var x = document.getElementById("Pbutton1");
                x.style.display = "none";
    </script>
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
        .water {
            content: 'Raksym Trading PLC';
            align-content: center;
            justify-content: center;
            opacity: 0.6;
            z-index: -1;
            transform: rotate(-35deg);
        }
    </style>
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" rel="stylesheet">
    <title>Credit Note Details</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid pr-3 pl-3">
        <div class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                        <h5 class="modal-title text-gray-900 font-weight-bold" id="H1">Cash From Customer</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">



                            <div class="col-md-12 ">
                                <div class="form-group">
                                    <label class="small text-gray-900">Customer Account</label>
                                    <asp:DropDownList ID="DropDownList1" runat="server" class="form-control form-control-sm">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-6 ">
                                <div class="form-group">
                                    <label class="small text-gray-900">Cash</label>


                                    <br />
                                    <div class="form-group mb-0">
                                        <div class="input-group input-group-alternative">

                                            <asp:TextBox ID="txtCash" class="form-control  form-control-sm" placeholder="Cash Payment" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-6 ">
                                <div class="form-group">
                                    <label class="small text-gray-900">Discount</label>


                                    <br />
                                    <asp:TextBox ID="txtDiscount" value="0" class="form-control  form-control-sm" placeholder="Discount" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12 ">
                                <div class="form-group">
                                    <label class="small text-gray-900">Deposit Account</label><span class="text-danger"> *Select Cash Account</span>
                                    <asp:DropDownList ID="DropDownList2" runat="server"
                                        class="form-control form-control-sm ">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-12 ">

                                <div class="custom-control mt-2  custom-checkbox ">
                                    <input type="checkbox" class="custom-control-input" checked="gf" id="CheckGene" runat="server" clientidmode="Static" />
                                    <label class="custom-control-label mt-1 small text-gray-900 " for="CheckGene">Generate invoice</label>
                                </div>
                            </div>

                        </div>



                        <div class="row mt-2">
                            <div class="col-12 ">
                                <div class="form-group">
                                    <label class="small text-gray-900">Reference</label>


                                    <br />
                                    <asp:TextBox ID="TextBox4" class="form-control" Style="border-color: #ff6a00" placeholder="References" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-12">
                                <label for="username" class="form-label small text-gray-900 font-weight-bold">FS no</label>
                                <div class="input-group has-validation">
                                    <asp:TextBox ID="txtFSNo" class="form-control form-control-sm" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">


                        <button class="btn btn-success" type="button" disabled id="Pbutton" style="display: none">
                            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                            Saving...
                        </button>
                        <div id="Bd">
                            <asp:Button ID="btnUpdate" class="btn btn-success" runat="server"
                                Text="SAVE" OnClick="Save" OnClientClick="myFunctionshop()" />
                        </div>
                    </div>


                </div>
            </div>
        </div>
        <div class="modal fade" id="exampleModalShopAreaChange" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="exampleModalShopAreaChange" aria-hidden="true">
            <div class="modal-dialog modal-sm" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h6 class="modal-title text-gray-900 font-weight-bolder" id="exampleModalLabel8shopM1">Write Off Credit</h6>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <p class="text-danger small">Write off is a process of removing the credit that can not be collected anymore</p>
                                <label class="text-gray-900 small">Amount to be WriteOff</label>
                                <asp:TextBox ID="txtWriteOffAmount" placeholder="Amount" runat="server" class=" form-control form-control-sm"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row mb-3">
                            <div class="col-md-12">

                                <label class="text-gray-900 small">Select Expense Account</label>
                                <asp:DropDownList ID="ddlExpense" class=" form-control form-control-sm" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="row">

                            <div class="col-md-12">

                                <center>
                                    <asp:Button ID="btnWriteOffAmount" runat="server" class="btn btn-sm btn-success w-100" Text="Save..." OnClick="WriteOff" />
                                </center>


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
        <div class="modal fade bd-example-modal-lg" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel2" aria-hidden="true">
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                        <h5 class="modal-title text-gray-900 font-weight-bold" id="H1">Bank Payment payment</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">

                            <div class="col-md-12 ">
                                <div class="form-group">
                                    <label class="small text-gray-900">Customer Account</label>
                                    <asp:DropDownList ID="DropDownList3" runat="server"
                                        class="form-control form-control-sm ">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-6 ">
                                <div class="form-group">
                                    <label class="small text-gray-900">Amount</label>


                                    <br />
                                    <div class="form-group mb-0">
                                        <div class="input-group input-group-alternative">

                                            <asp:TextBox ID="txtCash1" class="form-control  form-control-sm" placeholder="Cash Payment" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-6 ">
                                <div class="form-group">
                                    <label class="small text-gray-900">Discount</label>


                                    <br />
                                    <asp:TextBox ID="txtDiscount1" value="0" class="form-control  form-control-sm" placeholder="Discount" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12 ">
                                <div class="form-group">
                                    <label class="small text-gray-900">Deposit Account</label><span class="text-danger"> *Select Bank Account</span>
                                    <asp:DropDownList ID="DropDownList4" runat="server"
                                        class="form-control  form-control-sm">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-12 ">
                                <div class="form-group">
                                    <label class="small text-gray-900">Date</label>


                                    <br />
                                    <asp:TextBox ID="txtDate1" TextMode="Date" class="form-control form-control-sm" placeholder="Date" runat="server"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <div class="row mt-2">
                            <div class="col-12 ">
                                <div class="form-group">
                                    <label class="small text-gray-900">Reference</label>


                                    <br />
                                    <asp:TextBox ID="TextBox1" class="form-control form-control-sm" Style="border-color: #ff6a00" placeholder="References" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <label for="username" class="form-label small text-gray-900 font-weight-bold">FS no</label>
                                <div class="input-group has-validation">
                                    <asp:TextBox ID="txtFSNo1" class="form-control form-control-sm" runat="server"></asp:TextBox>
                                </div>

                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-12 ">
                                <div class="form-group">
                                    <label class="small text-gray-900">Voucher and Cheque number</label>


                                    <br />
                                    <asp:TextBox ID="TextBox5" TextMode="MultiLine" class="form-control form-control-sm" Style="border-color: #ff6a00" placeholder="Notes" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="custom-control mt-2  custom-checkbox ">
                            <input type="checkbox" class="custom-control-input" checked="gf" id="Checkbox1" runat="server" clientidmode="Static" />
                            <label class="custom-control-label mt-1 small text-gray-900 " for="Checkbox1">Generate invoice</label>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <center>
                            <button class="btn btn-success" type="button" disabled id="Pbutton1" style="display: none">
                                <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                Saving...
                            </button>
                            <div id="Bd1">
                                <asp:Button ID="Button1" class="btn btn-success" runat="server"
                                    Text="SAVE" OnClick="Save1" OnClientClick="myFunctionshop1()" />
                            </div>
                        </center>
                    </div>
                </div>
            </div>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <!-- Navbar -->



        <div class="bg-white rounded-lg mb-1 " style="height: 1200px">
            <div class="card-header bg-white ">


                <div class="row">
                    <div class="col-9 text-left ">

                        <a class="btn btn-light btn-circle" id="buttonback" runat="server" href="creditnote.aspx" data-toggle="tooltip" data-placement="bottom" title="Back to credit notes">

                            <span class="fa fa-arrow-left text-primary"></span>

                        </a>
                        <span class="badge badge-light mr-2" id="CRNo" runat="server" style="color: #ff00bb"></span>

                        <span id="status_indicator" runat="server"></span>
                        <a id="RedInv" data-toggle="tooltip" title="Initial Invoice Reference" target="_blank" runat="server"><span class="badge text-white mx-2" id="Ref" runat="server" style="background-color:#b14cb2"></span></a>
                        <span id="Notes" class="mx-2 badge badge-counter badge-info" data-toggle="tooltip" title="Credit Title" runat="server"></span>
                    </div>
                    <div class="col-3 text-right ">
                        <a class="btn btn-primary mr-2 btn-circle btn-sm" id="modalwriteoff" visible="false" runat="server" href="#" data-toggle="modal" data-target="#exampleModalShopAreaChange" title="Write off credit">

                            <span class="fa fa-trash text-white"></span>

                        </a>
                        <button type="button" id="paybutoon" runat="server" class="btn btn-danger btn-sm btn-circle dropdown-toggle dropdown-toggle-split" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            <span class="sr-only">Toggle Dropdown</span>
                        </button>
                        <div class="dropdown-menu">
                            <button type="button" runat="server" id="Sp2" class="btn btn-sm btn-danger" data-toggle="modal" data-target=".bd-example-modal-lg">
                                <div>
                                    <i class="fas fa-cash-register"></i>
                                    <span>Record Cash Payment </span>
                                </div>
                            </button>
                            <button type="button" runat="server" id="Button2" class="btn btn-sm btn-warning" data-toggle="modal" data-target="#exampleModal">
                                <div>
                                    <i class="fas fa-cash-register"></i>
                                    <span>Record Bank Payment </span>
                                </div>
                            </button>
                        </div>
                        <button name="b_print" onclick="printdiv('div_print');" type="button" title="Print" class="mx-2 btn btn-sm btn-success btn-circle" data-toggle="modal" data-target="#exampleModalCenter">
                            <div>
                                <i class="fas fa-print text-white font-weight-bold"></i>

                            </div>
                        </button>


                    </div>
                </div>

            </div>

            <div id="div_print">

                <asp:Label ID="lblError" runat="server"></asp:Label>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <div class="row mt-5">
                    <div class="col-1">
                    </div>
                    <div class="col-10 text-gray-800">
                        <div class="row border-bottom">
                            <div class="col-md-4 text-left">
                                <img class="" src="../../asset/Brand/gh.jpg" alt="" width="110" height="80">
                            </div>
                            <div class="col-md-8 text-right">

                                <h4 class="text-gray-900 font-weight-bold">CREDIT NOTE</h4>
                                <h5 class="text-gray-900 font-weight-bold">CN#-<span id="CN3" runat="server"></span></h5>
                                <h5 id="CreditTitle" class="text-gray-900 text-uppercase border-bottom border-top border-dark font-weight-bold" runat="server"></h5>
                            </div>

                        </div>
                        <div class="row ">
                            <div class="col-md-6 text-left">
                                <span translate="no" class="h5 text-gray-900 text-uppercase border-bottom font-weight-bold" id="oname" runat="server"></span>
                            </div>
                            <div class="col-md-6 text-right">
                                <span translate="no" class="small text-gray-900 font-weight-bold" id="datecurrent" runat="server"></span>
                            </div>
                        </div>
                        <div class="row  ">
                            <div class="col-md-12 text-left">
                                <span class=" h6 text-gray-900 " style="height: 100px">To: </span><span style="height: 100px" class="h6 mx-2 text-gray-900 font-weight-bold font-italic" id="Name" runat="server"></span>
                                [<span class="text-gray-900 small font-weight-bold mt-3">Shop No: </span><span class="text-gray-900 small mx-1 font-weight-bold mt-3" id="ShopNo" runat="server"></span>]
                            </div>
                        </div>
                        <p class="h6 mt-4  text-gray-900 mb-4" style="text-align: justify; text-indent: 0px; line-height: 23px;">
                            The customer from invoice provided, ETB <span class=" font-weight-bold" id="Total" runat="server"></span>
                            noticing his/her credit number CN# <span id="CN" class=" font-weight-bold" runat="server"></span> dating on <span id="dateofcredit" runat="server" class="font-weight-bold"></span>, 
                             he/she still have incomplete payment amount of ETB <span id="balance" runat="server" class="text-danger font-weight-bold"></span>. 
                             This balance aged <span id="lblAged" class="font-weight-bold" runat="server"></span>,
                             so that considering your excellent payment history,
                             we’re certain this is just an oversight on your part. 
                             I’m sure we will receive your check within the next few days.
                        </p>
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
                        <center>
                            <h1 class="water h1  font-weight-bolder" style="font-size: 60px" id="WaterMarkOname" runat="server"></h1>
                        </center>
                    </div>

                    <div class="col-1">
                    </div>

                </div>



            </div>
        </div>



    </div>
    <script>
                function myFunctionshop1() {
                    var x = document.getElementById("Bd1"); var y = document.getElementById("Pbutton1");
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
                function myFunctionshop() {
                    var x = document.getElementById("Bd"); var y = document.getElementById("Pbutton");
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
</asp:Content>
