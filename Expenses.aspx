<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="Expenses.aspx.cs" Inherits="advtech.Finance.Accounta.Expenses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Expenses</title>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid pl-3 pr-3" style="position: relative;">
        <div class="modal fade bd-example-modal-lg" tabindex="-1" id="exampleModal1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
                        <h5 class="modal-title text-gray-900 font-weight-bold" id="H1"><span class="fas text-white fa-hand-holding-usd mr-1 btn-sm btn-circle" style="background-color:#b9459e"></span>Add Expense</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">

                        <div class="row mb-3">
                            <div class="col-md-6">
                                <label>Select Expense</label>
                                <asp:DropDownList ID="ddlExpense" class="form-control form-control-sm" runat="server"></asp:DropDownList>
                            </div>

                            <div class="col-md-6">
                                <label>Select Vendor</label>
                                <asp:DropDownList ID="ddlVendor" class="form-control form-control-sm" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <asp:TextBox ID="txtAmount" class="form-control form-control-sm" placeholder="Amount" runat="server"></asp:TextBox>
                            </div>
                                                        <div class="col-md-6">
                                <asp:TextBox ID="txtReference" class="form-control form-control-sm" placeholder="Reference" runat="server"></asp:TextBox>
                            </div>

                        </div>
  
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <label>Select Cash Account <span class="text-danger font-weight-bolder">OR</span></label>
                                <asp:DropDownList ID="ddlCash" class="form-control form-control-sm" runat="server"></asp:DropDownList>

                            </div>

                            <div class="col-md-6">
                                <label>Select Bank Account</label>
                                <asp:DropDownList ID="ddlBank" class="form-control form-control-sm" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="row">

                            <div class="col-md-6">
                                <asp:FileUpload ID="FileUpload1" class="form-control form-control-sm" runat="server" />
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <center>

                            <button class="btn btn-sm text-white" style="background-color: #7813ad; display: none" type="button" disabled id="Pbutton">
                                <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                Saving...
                            </button>
                            <div id="Bd">
                            <asp:Button ID="btnUpdate" class="btn btn-sm text-white" runat="server" Style="background-color: #7813ad"
                                Text="Save Expense" OnClick="btnUpdate_Click" OnClientClick="myFunctionshop()" />
                                </div>
                    </div>
                    </center>
                </div>
            </div>
        </div>
        <div class="modal fade" id="exampleModal11" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel11" aria-hidden="true">
            <div class="modal-dialog modal-sm" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title text-gray-900 small font-weight-bold" id="exampleModalLabel11">Filter by amount(condition based)</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="custom-control custom-radio custom-control-inline">

                                    <input type="radio" id="greater" name="customRadioInline1" class="custom-control-input" checked="true" runat="server" clientidmode="Static" />
                                    <label class="custom-control-label text-gray-900  " for="greater">></label>
                                </div>
                                <div class="custom-control custom-radio custom-control-inline">
                                    <input type="radio" id="less" name="customRadioInline1" class="custom-control-input" runat="server" clientidmode="Static" />
                                    <label class="custom-control-label font-weight-200  text-gray-900 " for="less"><</label>
                                </div>
                                <div class="custom-control custom-radio custom-control-inline">
                                    <input type="radio" id="equal" name="customRadioInline1" class="custom-control-input" runat="server" clientidmode="Static" />
                                    <label class="custom-control-label font-weight-200  text-gray-900 " for="equal">=</label>
                                </div>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 mb-3">
                                <asp:TextBox ID="txtFilteredAmount" runat="server" class="form-control form-control-sm" placeholder="Amount"></asp:TextBox>


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
        <div class="modal fade bd-example-modal-lg" tabindex="-1" id="Datemodal" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
                        <h5 class="modal-title" id="H1">Filter Expense</h5>
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
                                    <label class="font-weight-bold">To<span class="text-danger">*</span></label>


                                    <br />
                                    <div class="form-group mb-0">
                                        <div class="input-group input-group-alternative">
                                            <asp:TextBox ID="txtDateto" class="form-control " TextMode="Date" runat="server"></asp:TextBox>
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
                            <asp:Button ID="Button3" class="btn btn-primary" runat="server"
                                Text="Search..." OnClick="Button3_Click" />
                    </div>
                    </center>
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
                                <h5 class="m-0 font-weight-bold h6 text-uppercase text-gray-900"><span class="fas fa-hashtag mr-2" style="color:#e4606d"></span>Expenses NOTES</h5>
                            </div>


                            <div class="col-7 text-right ">
                                <button type="button" runat="server" id="Button1" class="mx-1 btn btn-sm btn-circle" style="background-color:#711eaa" data-toggle="modal" data-target="#Datemodal">
                                    <a class="nav-link" data-toggle="tooltip" data-placement="bottom" title="Search By Date Range">
                                        <div>
                                            <i class="fas fa-search text-white font-weight-bold"></i>
                                            <span></span>
                                        </div>
                                    </a>
                                </button>
                                <button type="button" runat="server" id="Sp2" class=" btn btn-sm mr-1 btn-circle"  style="background-color:#711eaa" data-toggle="modal" data-target="#exampleModal11">
                                    <a class="nav-link" data-toggle="tooltip" data-placement="bottom" title="Search By Amount by condition">
                                        <div>
                                            <i class="fas fa-search-location text-white font-weight-bold"></i>
                                            <span></span>
                                        </div>
                                    </a>
                                </button>
                                <button name="b_print" onclick="printdiv('div_print');" type="button" title="Print"  style="background-color:#711eaa" class="btn btn-sm btn-circle" data-toggle="modal" data-target="#exampleModalCenter">
                                    <div>
                                        <i class="fas fa-print text-white font-weight-bold"></i>

                                    </div>
                                </button>
                                <button type="button" runat="server" id="Button4" class="mx-1 btn btn-sm btn-circle"  style="background-color:#711eaa" data-toggle="modal" data-target="#exampleModal1">
                                    <div>
                                        <i class="fas fa-plus text-white font-weight-bold"></i>
                                        <span></span>
                                    </div>
                                </button>





                            </div>
                        </div>





                        <div class="row g-0 border-top mt-3">
                            <div class="col-12 col-lg-5 col-xl-4 border-right">


                                <div class="chat-messages p-4">

                                    <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">

                                        <HeaderTemplate>

                                            <table class="table align-items-center table-sm ">
                                                <thead>
                                                    <tr>


                                                        <th scope="col" class="text-gray-900 font-weight-bold">
                                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="name">Expense</asp:LinkButton></th>

                                                        <th scope="col" class="text-right text-danger small">
                                                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="amount">Amount</asp:LinkButton></th>
                                                        <th scope="col" class="text-right text-danger small">
                                                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="date">Date</asp:LinkButton></th>




                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                            <tr>

                                                <td>
                                                    <a class=" text-primary" href="Expenses.aspx?ref2=<%# Eval("id")%>&&expname=<%# Eval("name")%>"><span><%# Eval("name")%></span></a>


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
                            <div class="col-12 col-lg-7 col-xl-8">
                                <div id="div_print">

                                    <div id="showdetail" runat="server">


                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="card-header text-left text-black bg-white font-weight-bold">

                                                    <h5 runat="server" class="mb-1 text-gray-600 font-weight-bold "></h5>
                                                    <h6 class="mb-1 h4 text-uppercase text-gray-900 font-weight-bold " id="ExpenseType" runat="server"><span class="small"><sup id="Status2" class="mx-2" runat="server"></sup></span></h6>
                                                    <span class="mb-2 text-gray-900 small font-weight-bold ">Vendor:</span><span class=" mx-1 mb-2 text-gray-900 small text-right  " id="vendor1" runat="server"></span><span class="mb-2 text-primary small "></span>
                                                    <h6><span class="mb-2 text-primary small font-weight-bold ">Addresses:</span><span class=" mx-1 mb-2 text-primary small text-right  " id="addressvendor" runat="server"></span></h6>
                                                    <h6><span class="mb-2 text-success small font-weight-bold ">Bill Date:</span><span class=" mx-1 mb-2 text-gray-900 small text-right  " id="BillDate1" runat="server"></span> </h6>

                                                    <div class="row align-items-center">

                                                        <div class="col-12 text-right">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-6">
                                                <div class="card-header text-right text-black bg-white font-weight-bold">

                                                    <h5 id="H3" runat="server" class="mb-1 text-gray-600 font-weight-bold "></h5>
                                                    <h6 class="mb-1 h4 text-uppercase text-gray-900 font-weight-bold " id="oname" runat="server"></h6>
                                                    <h6 class="mb-1 h6 text-uppercase text-gray-900 font-weight-bold " id="H2" runat="server">EXPENSE NOTE</h6>
                                                    <span class="mb-2 text-gray-900 small " id="addressname" runat="server"></span>

                                                </div>
                                            </div>
                                        </div>

                                        <div class="card-body">
                                            <table class="table align-items-center table-sm table-bordered ">

                                                <thead class=" thead-dark ">
                                                    <tr>


                                                        <th scope="col" class="text-left text-white">#</th>
                                                        <th scope="col" class="text-left text-white">Amount</th>
                                                        <th scope="col" class="text-right text-white">Bill Date</th>







                                                    </tr>
                                                </thead>

                                                <tbody>

                                                    <itemtemplate>
                                                    <tr>

                                                        <td class="text-left text-gray-900">
                                                            <span id="id" runat="server"></span>
                                                        </td>
                                                        <td class="text-left text-gray-900">ETB <span id="amount1" runat="server"></span>
                                                        </td>
                                                        <td class="text-right text-gray-900">
                                                            <span id="date1" runat="server"></span>
                                                        </td>


                                                    </tr>


                                                </tbody>
                                            </table>
                                            <div id="attachmnetdiv" runat="server">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <hr />
                                                        <span class="small text-gray-900 font-weight-bolder">Attachment</span>:<a id="attachlink" target="_parent" class="mx-2" runat="server"><span class="small text-primary" id="attachname" runat="server"></span></a>
                                                        <hr />

                                                    </div>
                                                </div>
                                            </div>
                                        </div>




                                    </div>
                                </div>
                                <br />
                                <br />
                                <br />
                                <div class="mt-lg-5" id="leaveempt" runat="server">
                                    <center>
                                        <span class="fas fa-trash-restore fa-9x text-gray-400"></span>
                                        <h6 class="text-gray-500 mt-2 font-weight-light h6">Select Expense to Show</h6>
                                    </center>
                                </div>


                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
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
</asp:Content>
