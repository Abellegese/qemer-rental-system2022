<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="OtherInvoices.aspx.cs" Inherits="advtech.Finance.Accounta.OtherInvoices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Other Invoice</title>
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
        $(document).ready(function () {
            //We are binding onchange event using jQuery built-in change event
            $('#ddlVendor').change(function () {
                //get selected value and check if subject is selected else show alert box
                var SelectedValue = $("#ddlVendor").val();
                if (SelectedValue > 0) {
                    //get selected text and set to label
                    let SelectedText = $("#ddlVendor option:selected").val();
                    $("#txtTIN").val(SelectedText);
                }
                else {
                    $("#txtTIN").val("");
                }
                if (SelectedText == "0") {
                    $("#txtCustomer").visible = true;
                }
            });
        });

    </script>
    <script type="text/javascript">

        window.addEventListener('load', (event) => {
            var x = document.getElementById("Pbutton");
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
            opacity: 0.2;
            z-index: -1;
            transform: rotate(-45deg);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid pl-3 pr-3" style="position: relative;">


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




        <div class="modal fade bd-example-modal-lg" tabindex="-1" id="exampleModal1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class=" modal-header">
                        <h6 class="modal-title text-gray-900 font-weight-bold" id="H1">
                            <span class="mr-2 fas fa-hand-holding-usd" style="color: #ff00bb"></span>Create Invoice                               
                        </h6>


                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                    <div class="modal-body">
                        <div class="row">
                            <div class="col-12">
                                <div class="custom-control mb-1  custom-checkbox ">
                                    <input type="checkbox" class="custom-control-input" id="Checkbox1" runat="server" clientidmode="Static" />
                                    <label class="custom-control-label mt-1 small text-gray-900 " for="Checkbox1">Is Bulk</label>
                                    <button type="button" id="AddButton" class="btn btn-sm btn-circle" style="background-color: #b868bb; display: none">
                                        <span class="fas fa-plus text-white" data-toggle="tooltip" data-placement="top" title="Add to cart"></span>
                                    </button>
                                </div>

                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <label class="small text-gray-900">Select Invoice type</label>
                                <asp:DropDownList ID="ddlExpense" ClientIDMode="Static" class="form-control  form-control-sm" runat="server"></asp:DropDownList>
                            </div>

                            <div class="col-md-6">
                                <label class="small text-gray-900">Select Customer</label>
                                <asp:DropDownList ID="ddlVendor" ClientIDMode="Static" class="form-control form-control-sm" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="row mb-2 border-left border-warning border-top border-bottom border-right">
                            <div class="col-sm-6 mb-2">
                                <label class="mb-2 small text-danger"><span class="fas fa-arrow-alt-circle-right mr-1"></span>If customer not exist</label>

                                <div class="input-group has-validation">
                                    <asp:TextBox ID="txtCustomer" class="form-control form-control-sm" Visible="true" runat="server" ClientIDMode="Static" placeholder="Customer Name"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-6 mb-2">
                                <label class="mb-2 small  text-success">.</label>

                                <div class="input-group has-validation">
                                    <asp:TextBox ID="txtAddress" class="form-control form-control-sm" Visible="true" runat="server" ClientIDMode="Static" placeholder="Address"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <p class="small text-danger mt-1 mb-2 "><span class="fas fa-arrow-alt-circle-right mr-1"></span>If amount is unit based, please enter the amount of the unit, eg. 5 hour. Else put vat included customer payment</p>
                                <asp:TextBox ID="txtAmount" ClientIDMode="Static" class="form-control form-control-sm" placeholder="Amount [VAT Included]" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label class="mb-2 small font-weight-bold">Reference</label>
                                <asp:TextBox ID="txtReference" class="form-control form-control-sm" placeholder="Reference" runat="server" Style="border-color: #ff6a00"></asp:TextBox>
                            </div>

                        </div>
                        <div class="row mb-2">
                            <div class="col-sm-6">
                                <label for="username" class="form-label small text-gray-900 font-weight-bold">FS#</label>
                                <div class="input-group has-validation">
                                    <asp:TextBox ID="txtFSNo" class="form-control form-control-sm" runat="server" placeholder="Provide FS#"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <label for="username" class="form-label small text-gray-900 font-weight-bold">TIN#</label>
                                <div class="input-group has-validation">
                                    <asp:TextBox ID="txtTIN" class="form-control form-control-sm" Style="color: #ff6a00; border-color: #ff6a00" runat="server" ClientIDMode="Static" placeholder="Provide TIN#"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <label class="small text-gray-900">Select Cash Account <span class="text-danger small font-weight-bolder">OR</span></label>
                                <asp:DropDownList ID="ddlCash" ClientIDMode="Static" class="form-control form-control-sm" runat="server"></asp:DropDownList>

                            </div>

                            <div class="col-md-6">
                                <label class="small text-gray-900">Select Bank Account</label>
                                <asp:DropDownList ID="ddlBank" ClientIDMode="Static" class="form-control form-control-sm" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="card-body">
                                <table id="myTableHeader" style="display: none" class="table table-sm table-bordered small text-gray-900">
                                    <thead>
                                        <th>Invoice Type</th>
                                        <th>Amount</th>
                                    </thead>
                                    <tr>
                                    </tr>
                                </table>
                                <table id="myTable" class="table table-sm table-bordered small text-gray-900">

                                    <tr>
                                    </tr>
                                </table>
                            </div>

                        </div>

                    </div>
                    <div class="modal-footer">
                        <center>
                            <button class="btn btn-danger btn-sm" type="button" disabled id="Pbutton" style="display: none">
                                <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                Creating Invoice...
                            </button>
                            <asp:Button ID="btnUpdate" class="btn btn-sm btn-danger" runat="server"
                                Text="Create Invoice" OnClick="btnUpdate_Click" OnClientClick="myFunctionshop()" />
                    </div>
                    </center>
     
                </div>
            </div>
        </div>

        <div class="modal fade" id="exampleModal11" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel11" aria-hidden="true">
            <div class="modal-dialog modal-sm" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <span class="modal-title small text-gray-900" id="exampleModalLabel11">Filter by amount(condition based)</span>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="custom-control custom-radio custom-control-inline">

                                    <input type="radio" id="greater" name="customRadioInline1" class="custom-control-input" checked="true" runat="server" clientidmode="Static" />
                                    <label class="custom-control-label text-gray-900  small" for="greater">Greater</label>
                                </div>
                                <div class="custom-control custom-radio custom-control-inline">
                                    <input type="radio" id="less" name="customRadioInline1" class="custom-control-input" runat="server" clientidmode="Static" />
                                    <label class="custom-control-label font-weight-200  text-gray-900 " for="less">Less</label>
                                </div>
                                <div class="custom-control custom-radio custom-control-inline">
                                    <input type="radio" id="equal" name="customRadioInline1" class="custom-control-input" runat="server" clientidmode="Static" />
                                    <label class="custom-control-label font-weight-200  text-gray-900 " for="equal">Equals</label>
                                </div>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 mb-3">
                                <asp:TextBox ID="txtFilteredAmount" placeholder="Amount" runat="server" class="form-control form-control-sm"></asp:TextBox>


                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-5">

                            </div>
                        </div>
                    </div>
                    <center>
                        <div class="modal-footer">
                            <asp:Button ID="btnAmountCondition" runat="server" class="btn btn-sm w-100 btn-danger" Text="search.." OnClick="btnAmountCondition_Click" />

                        </div>

                    </center>
                </div>
            </div>
        </div>
        <div class="modal fade bd-example-modal-lg" tabindex="-1" id="Datemodal" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
                        <h5 class="modal-title text-gray-900 font-weight-bold" id="H1"><span class="fas fa-calendar mr-2" style="color:#b868bb"></span>Filter Invoice</h5>
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
                                            <asp:TextBox ID="txtDateform" class="form-control form-control-sm" TextMode="Date" runat="server"></asp:TextBox>
                          

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
                                            <asp:TextBox ID="txtDateto" class="form-control form-control-sm " TextMode="Date" runat="server"></asp:TextBox>
                
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <center>

                            <asp:Button ID="Button3" class="btn btn-sm btn-danger" runat="server"
                                Text="Search..." OnClick="Button3_Click" />
                    </div>
                    </center>
     
                </div>
            </div>
        </div>

        <div class="modal fade" id="exampleModalDelete" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-sm" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title text-gray-900 h6" id="exampleModalLabel"><span class="fas fa-trash mr-2" style="color: #9d469d"></span>Delete Invoice</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">

                            <div class="col-md-12">
                                <asp:Button ID="btnDelete1" runat="server" class="btn btn-sm text-white w-100" Style="background-color: #9d469d" Text="Remove..." OnClick="btnDelete1_Click" />
                                <div class="custom-control mb-2 mt-2 mx-2 custom-checkbox font-weight-300">
                                    <input type="checkbox" class="custom-control-input" id="deleteCheck" runat="server" clientidmode="Static" />
                                    <label class="custom-control-label small mt-1 text-gray-900" for="deleteCheck">Adjust Ledger & Customer Statement</label>
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
                            <div class="col-md-12">
                                <p class="small text-danger mt-1 mb-2 "><span class="fas fa-arrow-alt-circle-right mr-1"></span>If amount is unit based, please enter the amount of the unit, eg. 5 hour. Else put vat included customer payment</p>

                                <asp:TextBox ID="txtAmountEdited" class="form-control mb-3 form-control-sm " placeholder="Amount" runat="server" Style="border-color: #9d469d"></asp:TextBox>

                            </div>
                            <div class="col-md-12">
                                <asp:TextBox ID="txtEditAmount" ReadOnly="true" class="form-control form-control-sm " placeholder="Total Amount" runat="server" Style="border-color: #9d469d"></asp:TextBox>
                                <div class="custom-control mb-2 mt-2 custom-checkbox font-weight-300">
                                    <input type="checkbox" class="custom-control-input" id="Checkbox2" runat="server" clientidmode="Static" />
                                    <label class="custom-control-label small mt-1 text-gray-900" for="Checkbox2">Adjust Ledger & Customer Statement</label>
                                </div>
                            </div>
                        </div>
                        <div class="row">


                            <div class="col-md-12">
                                <asp:Button ID="btnEditInsert" runat="server" class="btn btn-sm w-100 text-white" Style="background-color: #9d469d" Text="Edit" OnClick="btnEditInsert_Click" />

                            </div>
                        </div>

                        <center>
                            <div class="modal-footer">
                            </div>

                        </center>
                    </div>
                </div>
            </div>

        </div>
        <div class="row">
            <div class="col">
                <div class="bg-white rounded-lg" style="height: 1000px;">
                    <div class="card-header shadow-none bg-white ">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        <div class="row">

                            <div class="col-5 text-left ">
                                <a class="btn btn-circle btn-sm text-white mr-2" id="buttonback" style="background-color: #34ce57" href="OtherInvoices.aspx" visible="false" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back to Invoice">

                                    <span class="fa fa-arrow-left "></span>

                                </a>
                                <span class="badge mr-2 text-white" id="InvoiceBadge" style="background-color: #34ce57" visible="false" runat="server"></span>
                                <span class="fas fa-hashtag mr-2" style="color: #ff00bb" id="I1" runat="server"></span><span id="I2" runat="server" class="m-0 font-weight-bold h5 text-gray-900">Invoice</span>
                                <span class="badge mr-2 text-white" id="editSpan" style="background-color: #adc728" visible="false" runat="server"></span>
                                <span class="badge mr-2 text-white" id="editSpan2" style="background-color: #22e0ab" visible="false" runat="server">selected</span>
                                <span class="badge mr-2 text-white" id="DateRangerSpan" style="background-color: #7472f3" visible="false" runat="server"></span>

                            </div>


                            <div class="col-7 text-right ">






                                <div class="dropdown no-arrow">

                                    <button class="btn btn-sm btn-light btn-circle " type="button" onclick="HideORshowDiv()">
                                        <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Resize">
                                            <div>
                                                <i class="fas fa-arrows-alt-h text-danger" id="DivIcon"></i>

                                            </div>
                                        </a>
                                    </button>
                                    <button runat="server" id="modalMain" visible="false" type="button" class="btn btn-circle mx-1  btn-sm text-xs btn-danger" data-toggle="modal" data-target="#exampleModalLongService">
                                        <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Export as Excel">
                                            <div>
                                                <i class="fas fa-file-excel text-white"></i>

                                            </div>
                                        </a>
                                    </button>
                                    <button name="b_print" visible="false" onclick="printdiv('div_print');" id="btnMainPrint" runat="server" type="button" title="Print" class="mx-1 btn btn-sm btn-danger btn-circle" data-toggle="modal" data-target="#exampleModalCenter">
                                        <div>
                                            <i class="fas fa-print text-white font-weight-bold"></i>

                                        </div>
                                    </button>
                                    <button type="button" runat="server" id="btnPOS" visible="false" class=" mx-1 btn btn-sm btn-success btn-circle" data-toggle="modal" data-target="#exampleModalSMS">
                                        <a class="nav-link" data-toggle="tooltip" data-placement="bottom" title="Print using POS">
                                            <div>
                                                <i class="fas fa-print text-white font-weight-bold"></i>
                                                <span></span>
                                            </div>
                                        </a>
                                    </button>
                                    <button type="button" runat="server" id="Button4" class="mx-1 btn btn-sm btn-danger btn-circle" data-toggle="modal" data-target="#exampleModal1">
                                        <div data-toggle="tooltip" title="Create Invoice" data-placement="top">
                                            <i class="fas fa-plus text-white font-weight-bold"></i>
                                            <span></span>
                                        </div>
                                    </button>
                                    <button class="btn btn-light btn-circle dropdown-toggle" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">

                                        <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Options">
                                            <div>
                                                <i class="fas fa-caret-down text-danger"></i>

                                            </div>
                                        </a>

                                    </button>


                                    <div class="dropdown-menu  dropdown-menu-right shadow animated--fade-in" aria-labelledby="dropdownMenuLink">
                                        <div class="dropdown-header text-gray-900">Option:</div>
                                        <a class="dropdown-item  text-gray-700  text-danger" id="A3" target="_blank" href="OtherInvoicesCategory.aspx" runat="server"><span class="fas fa-location-arrow  mr-2" style="color: #ff00bb"></span>Manage Category</a>

                                        <a class="dropdown-item  text-gray-700  text-danger" href="#" id="A4" data-toggle="modal" data-target="#Datemodal" runat="server"><span class="fas fa-search-dollar  mr-2" style="color: #ff00bb"></span>Date Range Search</a>
                                        <a class="dropdown-item  text-gray-700  text-danger" href="#" id="A5" data-toggle="modal" data-target="#exampleModal11" runat="server"><span class="fas fa-search  mr-2" style="color: #ff00bb"></span>Condition based Search</a>
                                        <a class="dropdown-item  text-gray-700  text-danger" href="#" id="editTab" data-toggle="modal" data-target="#exampleModalEdit" runat="server" visible="false"><span class="fas fa-edit  mr-2" style="color: #ff00bb"></span>Edit Invoice</a>
                                        <a class="dropdown-item  text-gray-700  text-danger" href="#" id="deleteTab" data-toggle="modal" data-target="#exampleModalDelete" runat="server" visible="false"><span class="fas fa-trash-alt  mr-2" style="color: #ff00bb"></span>Delete Invoice</a>
                                        <a class="dropdown-item  text-gray-700  text-danger" href="#" id="A1" data-toggle="modal" data-target="#ModalHeader" runat="server"><span class="fas fa-bezier-curve  mr-2" style="color: #ff00bb"></span>Customize Invoice</a>

                                    </div>
                                </div>
                            </div>
                        </div>





                        <div class="row g-0 border-top mt-3">
                            <div class="col-4 border-right" id="leftDiv">


                                <div class="chat-messages ">

                                    <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">

                                        <HeaderTemplate>

                                            <table class="table align-items-center table-sm ">
                                                <thead>
                                                    <tr>
                                                        <th scope="col" class=" font-weight-bold small" style="color: #9d469d">FS#
                                                        </th>
                                                        <th scope="col" class=" font-weight-bold small" style="color: #9d469d">Customer
                                                        </th>



                                                        <th scope="col" class="text-right text-danger small" style="color: #9d469d">
                                                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="amount">Amount</asp:LinkButton></th>
                                                        <th scope="col" class="text-right text-danger small" style="color: #9d469d">
                                                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="date">Date</asp:LinkButton></th>




                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                            <tr>
                                                <td>
                                                    <a class=" text-primary small " href="OtherInvoices.aspx?fsno=<%# Eval("fsno")%>"><span>#00<%# Eval("fsno")%></span></a>
                                                </td>
                                                <td>
                                                    <a class=" text-primary small " href="OtherInvoices.aspx?fsno=<%# Eval("fsno")%>"><span><%# Eval("customer")%></span></a>
                                                </td>

                                                <td class="text-right">
                                                    <span id="Span1" class="mx-1 small text-gray-900" runat="server"><%# Eval("amount","{0:N2}")%></span>
                                                </td>

                                                <td class="text-right">
                                                    <span id="Label1" class="mx-1 small text-gray-900" runat="server"><%# Eval("date", "{0: dd/MM/yyyy}")%></span>
                                                </td>

                                            </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
              </table>
                                                                 
                                        </FooterTemplate>

                                    </asp:Repeater>
                                </div>
                                <div id="con" runat="server" visible="false">
                                    <asp:Repeater ID="Repeater2" runat="server">

                                        <HeaderTemplate>

                                            <table class="table align-items-center table-sm ">
                                                <thead>
                                                    <tr>
                                                        <th scope="col" class="text-gray-900 font-weight-bold small">Customer
                                                        </th>

                                                        <th scope="col" class="text-gray-900 font-weight-bold small">Type
                                                        </th>

                                                        <th scope="col" class="text-right text-danger small">Amount</th>
                                                        <th scope="col" class="text-right text-danger small">Date</th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                            <tr>
                                                <td>
                                                    <a class=" text-primary small " href="OtherInvoices.aspx?ref2=<%# Eval("id")%>&&expname=<%# Eval("incomename")%>"><span><%# Eval("customer")%></span></a>
                                                </td>
                                                <td>
                                                    <a class=" text-primary small" href="OtherInvoices.aspx?ref2=<%# Eval("id")%>&&expname=<%# Eval("incomename")%>"><span><%# Eval("incomename")%></span></a>


                                                </td>
                                                <td class="text-right">
                                                    <span id="Span1" class="mx-1 small text-gray-900" runat="server"><%# Eval("amount","{0:N2}")%></span>
                                                </td>

                                                <td class="text-right">
                                                    <span id="Label1" class="mx-1 small text-gray-900" runat="server"><%# Eval("date", "{0: dd/MM/yyyy}")%></span>
                                                </td>

                                            </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
              </table>
                                                                 
                                        </FooterTemplate>

                                    </asp:Repeater>
                                </div>

                                <hr class="d-block d-lg-none mt-1 mb-0">
                            </div>
                            <div class="col-8" id="rightDiv">

                                <div class="row">
                                    <div class="col-1">
                                    </div>
                                    <div class="col-10 mt-4">
                                        <div id="div_print">
                                            <div id="showdetail" class="border-bottom mb-2" runat="server">

                                                <div class="row ">
                                                    <div class="col-md-6">
                                                        <div class="card-header text-uppercase text-left text-black bg-white font-weight-bold">

                                                            <span runat="server" class="mb-1 border-bottom border-dark h3 text-gray-900 font-weight-bold " id="InvoiceName"></span>
                                                            <br />
                                                            <div id="BodyFontSizeDiv1" runat="server">
                                          
                                                                <span class="mb-2   font-weight-bold ">Customer:</span><span class=" mx-1 mb-2 text-gray-900  text-right  " id="vendor1" runat="server"></span><span class="mb-2 text-primary  "></span>
                                                                <br />
                                                                <span><span class="fas fa-address-book  mr-2"></span><span id="Address" class="text-gray-900" contenteditable="true" runat="server"></span></span>
                                                                <br />
                                                                <span width="200px" class="">TIN<span class="fas fa-hashtag text-gray-300 ml-1"></span><span id="TINNUMBER" width="200px" contenteditable="true" runat="server" class="ml-1 text-gray-900"></span></span>
                                                                <br />

                                                                <span class="   font-weight-bold mr-2">Ref#</span><span class=" text-gray-900  font-weight-bold " id="RefNum" runat="server"></span>
                                                                <br />
                                                                <span id="PayMode" contenteditable="true" visible="false" runat="server" class=" mt-3 border-top border-dark"><i class=" fas fa-dollar-sign text-dark "></i><span class="mx-1"><span class="font-weight-bold  text-gray-400">Payment Mode:</span> <span id="PaymentMode" runat="server"></span></span></span>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <div class="card-header text-uppercase text-right text-black bg-white font-weight-bold">
                                                            <img class="" src="../../asset/Brand/gh.jpg" alt="" id="logoElement" runat="server" width="110" height="80">

                                                            <br />
                                                            <span class="mb-1 h3 border-top border-dark  text-uppercase text-gray-900 font-weight-bold " id="oname" runat="server"></span>
                                                            <br />
                                                            <div id="BodyFontSizeDiv2" runat="server">
                                                                <span class="border-top mt-1 mb-1">VENDOR TIN<span class="fas border-top m-1 fa-hashtag text-gray-300 ml-1"></span><span id="VendorTIN" runat="server"></span></span>
                                                                <br />
                                                                <span class="fas fa-address-book text-gray-400 mr-2"></span><span class="mb-2 text-gray-900  " id="addressname" runat="server"></span>
                                                                <br />
                                             
                                                                <span><span class=" text-uppercase border-bottom text-gray-900 font-weight-bold" contenteditable="true" id="FSno" runat="server"></span><span id="Span3" runat="server"></span></span>
                                                                <br />
                                                                <span><span class="mb-2   font-weight-bold ">Date:</span><span class=" mx-1 mb-2 text-gray-900  text-right  " id="BillDate1" runat="server"></span> </span>

                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="card-body">
                                                    <div id="BodyFontSize3" runat="server">

                                                        <asp:Repeater ID="rptBindDetails" runat="server" OnItemDataBound="rptBindDetails_ItemDataBound">

                                                            <HeaderTemplate>

                                                                <table class="table align-items-center table-bordered table-sm text-gray-900 ">
                                                                    <thead class=" thead-dark">
                                                                        <tr>
                                                                          
                                                                            <th scope="col" class="text-left text-white" <%#ColumnvistoRepeater().Item1 %>>INV#</th>
                                                                            <th scope="col" class="text-left text-white" >Item</th>
                                                                            <th scope="col" class="text-left text-white" <%#ColumnvistoRepeater().Item2 %>>Unit</th>
                                                                            <th scope="col" class="text-left text-white" <%#ColumnvistoRepeater().Item3 %>>Rate</th>
                                                                            <th scope="col" class="text-left text-white" <%#ColumnvistoRepeater().Item4 %>>Qty.</th>
                                                                            <th scope="col" class="text-right text-white">Amount</th>
                                                                            <th scope="col" class="text-right text-white">VAT(15%)</th>
                                                                            <th scope="col" class="text-right text-white">Total</th>

                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>

                                                                <tr>
                                                                    <td <%#ColumnvistoRepeater().Item1 %>>
                                                                        <a class=" text-primary small " data-toggle="tooltip" title="Edit the Line" href="OtherInvoices.aspx?fsno=<%# Eval("fsno")%>&&id=<%# Eval("id")%>&&invtype=<%# Eval("incomename")%>&&edit=true&&customer=<%# Eval("customer")%>"><span><%# Eval("id")%></span></a>
                                                                    
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("incomename") %>
                                                                    </td>
                                                                    <td <%#ColumnvistoRepeater().Item2 %>>
                                                                        <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("unit")%>'></asp:Label>
                                                                    </td>
                                                                    <td <%#ColumnvistoRepeater().Item3 %>>
                                                                        <asp:Label ID="lblRate" runat="server" Text='<%# Eval("rate")%>'></asp:Label>
                                                                    </td>
                                                                    <td <%#ColumnvistoRepeater().Item4 %>>

                                                                        <asp:Label ID="lblQty" runat="server" Text='<%# Eval("qty")%>'></asp:Label>
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <%# (Convert.ToDouble(Eval("amount"))/1.15).ToString("#,##0.00") %>
                                                                        
                                                                    </td>

                                                                    <td class="text-right">
                                                                        <%# (Convert.ToDouble(Eval("amount"))-Convert.ToDouble(Eval("amount"))/1.15).ToString("#,##0.00") %>
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <%# (Convert.ToDouble(Eval("amount"))).ToString("#,##0.00")%>
                                                                  
                                                                    </td>
                                                                </tr>

                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </tbody>
              </table>
                                                                 
                                                            </FooterTemplate>

                                                        </asp:Repeater>

                                                    </div>
                                                    <div class="row" id="Div1" runat="server">
                                                        <div class="col-md-8 text-left">
                                                            <p class="small text-danger">* If you have any disagreement with the invoice please contact us</p>
                               
                                                            <p class="small text-gray-900 font-weight-bold border-top">* Invalid without Fiscal or Refund Receipt</p>
                                                            <span class="fas fa-phone text-gray-400 mr-2"></span><span class="mb-2 text-gray-900 small " id="phone" runat="server"></span>
                                                            <center>
                                                                <h5 id="RaksTDiv" runat="server" class="water   font-weight-bolder mx-lg-5" style="padding-right: 0px; padding-left: 300px; font-size: 70px;">ATTACHMENT</h5>
                                                            </center>
                                                        </div>

                                                        <div class="col-md-4 small">
                                                            <div class="form-group">
                                                                <table class="table table-sm table-bordered  ">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td><span class="m-0 font-weight-bold text-right text-gray-900">Sub-Total:</span></td>
                                                                            <td class="text-right"><span id="DupVatFree" class="text-gray-900 font-weight-bold text-gray-900" runat="server"></span></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span class="m-0 font-weight-bold text-right text-gray-900 ">VAT(15%):</span></td>
                                                                            <td class="text-right"><span id="DupVAT" class="text-gray-900 font-weight-bold text-gray-900" runat="server"></span></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><span class="m-0 font-weight-bold text-right text-gray-900 ">Grand Total:</span></td>
                                                                            <td class="text-right"><span id="DupGrandTotal" class="text-gray-900 font-weight-bold text-gray-900" runat="server"></span></td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>

                                                            </div>

                                                        </div>


                                                    </div>
                                                    <div class="row border-top" id="NotesDiv" runat="server" visible="true">
                                                        <div class="col-md-7 text-left font-italic font-weight-bold">
                                                            <span class="text-gray-900 mb-2">Approved By:_________________________</span><br />
                                                            <br />
                                                            <span class="text-gray-900 ">Signature_________________________</span>
                                                        </div>
                                                        <div class="col-md-5 text-right font-italic font-weight-bold">
                                                            <span class="text-gray-900 mb-2 text-right">Prepared By:_________________________</span><br />
                                                            <br />
                                                            <span class="text-gray-900  text-right">Signature_________________________</span>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>


                                        </div>
                                        <div class="col-1">
                                        </div>

                                    </div>
                                </div>
                                <br />
                                <br />
                                <br />
                                <div class="mt-lg-5" id="leaveempt" runat="server">
                                    <center>
                                        <span class="fas fa-hand-holding-usd fa-9x text-gray-400"></span>
                                        <h6 class="text-gray-300 mt-4 font-weight-bold small">Select Invoice to Show</h6>
                                    </center>
                                </div>


                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </div>
        <div class="modal fade" id="ModalHeader" tabindex="-1" role="dialog" aria-labelledby="MHeader" aria-hidden="true">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h6 class="modal-title text-gray-900 h6 text-uppercase font-weight-bold" id="MHeader"><span class="fas fa-edit text-gray-300 mr-2 text-uppercase"></span>Customize Invoice</h6>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="card-body border-left border-primary">

                            <div class="row mb-3">
                                <div class="col-8">
                                    <h6 class="text-gray-900 small mb-3">Header Name</h6>
                                    <div class="form-group">

                                        <div class="input-group input-group-alternative">

                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <asp:TextBox ID="txtInvoiceName" data-toggle="tooltip" title="HEADING NAME" class="form-control form-control-sm" Style="border-color: #b868bb" runat="server" ReadOnly="false"></asp:TextBox>
                                                    <hr />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <h6 class="text-gray-900 small mb-3">Font Size</h6>
                                    <div class="form-group">

                                        <div class="input-group input-group-alternative">

                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <asp:TextBox ID="txtFontsize" class="form-control form-control-sm" data-toggle="tooltip" Style="border-color: #ff0000;" data-placement="bottom" title="font size" TextMode="Number" Height="30px" Width="70px" runat="server" Text="12"></asp:TextBox>

                                                    <div class="input-group-prepend " style="height: 30px">
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

                            <div class="row">
                                <div class="col-4">
                                    <h6 class="text-gray-900 small mb-3">Logo Visibility</h6>
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="logoCheck" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger font-weight-bolder" for="logoCheck">Visible</label>
                                    </div>

                                </div>
                                <div class="col-4">
                                    <h6 class="text-gray-900 small mb-3">Watermark Visibility</h6>
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="attachCheck" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger font-weight-bolder" for="attachCheck">Visible</label>
                                    </div>

                                </div>
                                <div class="col-4">
                                    <h6 class="text-gray-900 small mb-3">Invoice Type Visibiity</h6>

                                    <div class="col-1">
                                        <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                            <input type="checkbox" class="custom-control-input" id="invCheck" runat="server" clientidmode="Static" />
                                            <label class="custom-control-label small text-danger font-weight-bolder" for="invCheck">Visible</label>
                                        </div>

                                    </div>

                                </div>

                            </div>
                        </div>

                        <div class="card-body border-left border-secondary">
                            <h6 class="text-gray-900 small font-weight-light text-uppercase">Column visibility</h6>
                            <div class="row">
                                <div class="col-12">
                                    <table class="table-bordered table-sm table">
                                        <thead>

                                            <th class="text-gray-900 small text-center">#</th>
                                            <th class="text-gray-900 small text-center">Unit</th>
                                            <th class="text-gray-900 small text-center">Rate</th>
                                            <th class="text-gray-900 small text-center">Qty</th>
                                        </thead>
                                        <tbody>
                                            <tr>

                                                <td class="text-center">
                                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                                        <input type="checkbox" class="custom-control-input" id="NumbCheck" runat="server" clientidmode="Static" />
                                                        <label class="custom-control-label " for="NumbCheck"></label>
                                                    </div>
                                                </td>
                                                <td class="text-center">
                                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                                        <input type="checkbox" class="custom-control-input" id="UnitCheck" runat="server" clientidmode="Static" />
                                                        <label class="custom-control-label " for="UnitCheck"></label>
                                                    </div>
                                                </td>

                                                <td class="text-center">
                                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                                        <input type="checkbox" class="custom-control-input" id="RateCheck" runat="server" clientidmode="Static" />
                                                        <label class="custom-control-label " for="RateCheck"></label>
                                                    </div>
                                                </td>


                                                <td class="text-center">
                                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                                        <input type="checkbox" class="custom-control-input" id="QtyCheck" runat="server" clientidmode="Static" />
                                                        <label class="custom-control-label " for="QtyCheck"></label>
                                                    </div>
                                                </td>
                                            </tr>

                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <center>
                        <div class="modal-footer">

                            <asp:Button ID="btnCustomizeSave" runat="server" class="btn btn-sm btn-success " OnClick="btnCustomizeSave_Click" OnClientClick="myFunctionshop1()" Text="Save Changes" />


                        </div>

                    </center>
                </div>
            </div>
        </div>
        <script>
            $('#Checkbox1').click(function () {

                $("#myTableHeader").toggle(this.checked);
                $("#AddButton").toggle(this.checked);
            });
        </script>
        <script>


</script>
        <script type="text/javascript">
            function HideORshowDiv() {
                var div = document.getElementById('rightDiv');
                var div2 = document.getElementById('leftDiv');
                var div3 = document.getElementById('DivIcon');
                if (div.className == "col-12") {

                    document.getElementById("rightDiv").style.transition = "all 0.1s";

                    div2.className = "col-4"; div.className = "col-8";
                    div3.className = "fas fa-arrows-alt-h  text-danger";
                    div2.style.display = "block";
                }
                else {
                    div.className = "col-12";

                    document.getElementById("leftDiv").style.transition = "all 0.5s";

                    div2.style.display = "none";
                    div3.className = "fas fa-compress  text-danger";
                }
            }
        </script>
        <style type="text/css">
            .chat-online {
                color: #34ce57
            }

            .chat-offline {
                color: #e4606d
            }

            .chat-messages {
                display: flex;
                flex-direction: column;
                max-height: 800px;
                overflow-y: scroll
            }

            .chat-message-left,
            .chat-message-right {
                display: flex;
                flex-shrink: 0
            }

            .chat-message-left {
                margin-right: auto
            }

            .chat-message-right {
                flex-direction: row-reverse;
                margin-left: auto
            }

            .py-3 {
                padding-top: 1rem !important;
                padding-bottom: 1rem !important;
            }

            .px-4 {
                padding-right: 1.5rem !important;
                padding-left: 1.5rem !important;
            }

            .flex-grow-0 {
                flex-grow: 0 !important;
            }

            .border-top {
                border-top: 1px solid #dee2e6 !important;
            }
        </style>
        <script>
            function myFunctionshop() {
                var y = document.getElementById("<%=btnUpdate.ClientID %>"); var x = document.getElementById("Pbutton");
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
        <script type="text/javascript" src="http://cdn.jsdelivr.net/json2/0.1/json2.js"></script>

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
        <script src="../../asset/js/JSPrintManager.js"></script>
        <script src="../../asset/js/bluebird.min.js"></script>
        <script>

</script>
        <script type="text/javascript">
            var invType, amount;
            function AddTable(invType, amount) {
                var table = document.getElementById("myTable");

                // Create an empty <tr> element and add it to the 1st position of the table:
                var row = table.insertRow(0);

                // Insert new cells (<td> elements) at the 1st and 2nd position of the "new" <tr> element:
                var cell1 = row.insertCell(0);
                var cell2 = row.insertCell(1);

                // Add some text to the new cells:
                cell1.innerHTML = invType;
                cell2.innerHTML = amount;
            }

            $(function () {
                $("[id*=AddButton]").bind("click", function () {

                    var infoJson = {};
                    infoJson.invoiceType = $("#ddlExpense option:selected").text();
                    infoJson.PermanentCustomer = $("#ddlVendor option:selected").text();
                    infoJson.Customer = $("[id*=txtCustomer]").val();
                    infoJson.Address = $("[id*=txtAddress]").val();
                    infoJson.Amount = $("[id*=txtAmount]").val();
                    infoJson.Reference = $("[id*=txtReference]").val();
                    infoJson.FS = $("[id*=txtFSNo]").val();
                    infoJson.TIN = $("[id*=txtTIN]").val();
                    infoJson.ddlCash = $("#ddlCash option:selected").text();
                    infoJson.ddlBank = $("#ddlBank option:selected").text();
                    if (infoJson.invoiceType == "-Select-" || infoJson.Amount == "" || infoJson.FS == "") {
                        alert("Please fill all the required inputs.");
                    }
                    else {
                        $.ajax({
                            type: "POST",
                            url: "OtherInvoices.aspx/SaveInvoice",
                            data: '{infoJson: ' + JSON.stringify(infoJson) + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {


                            }
                        });
                        AddTable(infoJson.invoiceType, infoJson.Amount);
                    }
                    return false;
                });
            });
        </script>
        <script>

            //WebSocket settings
            JSPM.JSPrintManager.auto_reconnect = true;
            JSPM.JSPrintManager.start();
            JSPM.JSPrintManager.WS.onStatusChanged = function () {
                if (jspmWSStatus()) {
                    //get client installed printers
                    JSPM.JSPrintManager.getPrinters().then(function (myPrinters) {
                        var options = '';
                        for (var i = 0; i < myPrinters.length; i++) {
                            options += '<option>' + myPrinters[i] + '</option>';
                        }
                        $('#installedPrinterName').html(options);
                    });
                }
            };

            //Check JSPM WebSocket status
            function jspmWSStatus() {
                if (JSPM.JSPrintManager.websocket_status == JSPM.WSStatus.Open)
                    return true;
                else if (JSPM.JSPrintManager.websocket_status == JSPM.WSStatus.Closed) {
                    alert('JSPrintManager (JSPM) is not installed or not running! Download JSPM Client App from http://192.168.1.254:9300/Finance/Accounta/app/jspm4-4.0.22.512-win.exe');
                    return false;
                }
                else if (JSPM.JSPrintManager.websocket_status == JSPM.WSStatus.Blocked) {
                    alert('JSPM has blocked this website!');
                    return false;
                }
            }

            //Do printing...
            function print2(o) {
                var tot = document.getElementById("<%=DupGrandTotal.ClientID %>");
                var pre = document.getElementById("<%=DupVatFree.ClientID %>");
                var Vat = document.getElementById("<%=DupVAT.ClientID %>");
   
                var date = document.getElementById("<%=BillDate1.ClientID %>");
                var contact = document.getElementById("<%=phone.ClientID %>");
                var Tin = document.getElementById("<%=VendorTIN.ClientID %>");
                var FSNo = document.getElementById("<%=FSno.ClientID %>");
                var ref = document.getElementById("<%=RefNum.ClientID %>");

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
