<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.master" AutoEventWireup="true" CodeBehind="LedgerDetail.aspx.cs" Inherits="advtech.Finance.Accounta.LedgerDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Ledger Detail</title>
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
            transform: rotate(-35deg);
        }
    </style>
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid pr-3 pl-3">
        <div class="modal fade" id="exampleModal11" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel11" aria-hidden="true">
            <div class="modal-dialog modal-sm" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <span class="modal-title small text-gray-900" id="exampleModalLabel11">Filter by amount(condtion)</span>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="custom-control custom-radio custom-control-inline">

                                    <input type="radio" id="greater" name="customRadioInline1" class="custom-control-input" checked="true" runat="server" clientidmode="Static" />
                                    <label class="custom-control-label text-gray-900  " for="greater">ETB ></label>
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
                                <asp:TextBox ID="txtFilteredAmount" runat="server" class="form-control form-control-sm" placeholder="type amount"></asp:TextBox>


                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <div class="custom-control custom-radio custom-control-inline">

                                    <input type="radio" id="Credit" name="amount" class="custom-control-input" checked="true" runat="server" clientidmode="Static" />
                                    <label class="custom-control-label text-gray-900  " for="Credit">Credit</label>
                                </div>
                                <div class="custom-control custom-radio custom-control-inline">
                                    <input type="radio" id="Debit" name="amount" class="custom-control-input" runat="server" clientidmode="Static" />
                                    <label class="custom-control-label font-weight-200  text-gray-900 " for="Debit">Debit</label>
                                </div>
                                <div class="custom-control custom-radio custom-control-inline">
                                    <input type="radio" id="Balance" name="amount" class="custom-control-input" runat="server" clientidmode="Static" />
                                    <label class="custom-control-label font-weight-200  text-gray-900 " for="Balance">Balance</label>
                                </div>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <center>
                                    <asp:Button ID="btnAmountCondition" runat="server" class="btn btn-sm btn-success" Text="search.." OnClick="btnAmountCondition_Click" />

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
        <div class="modal fade" id="exampleModal9v" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel9v" aria-hidden="true">
            <div class="modal-dialog modal-sm" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h6 class="modal-title font-weight-bolder text-gray-900" id="exampleModalLabel9v">Adjust Account</h6>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row mb-3">
                            <div class="col-md-12">

                                <asp:TextBox ID="txtAmount" placeholder="Amount" class="form-control form-control-sm" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">

                                <asp:TextBox ID="txtRemark" placeholder="Remark" TextMode="MultiLine" Height="100px" class="form-control form-control-sm" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <center>
                                    <asp:Button ID="Button14" runat="server" class="btn btn-sm btn-light" OnClick="Button14_Click" Text="Adjust" />
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
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <!-- Navbar -->
        <!-- Table -->
        <div class="row ">
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
            <div class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
                            <h5 class="modal-title text-gray-900 font-weight-bold" id="H1">Search Transactions</h5>
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
                                                <asp:TextBox ID="txtDateform" class="form-control form-control-sm " TextMode="Date" runat="server"></asp:TextBox>
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
                                                <asp:TextBox ID="txtDateto" class="form-control form-control-sm " TextMode="Date" runat="server"></asp:TextBox>
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
                                <asp:Button ID="btnUpdate" class="btn btn-primary" OnClick="btnUpdate_Click" runat="server"
                                    Text="Search..." />
                        </div>
                        </center>
                    </div>
                </div>
            </div>


            <div class="col">
                <div class="bg-white rounded-lg">
                    <div class="card-header bg-white">
                        <asp:Label ID="lblMsg1" runat="server"></asp:Label>
                        <div class="row">
                            <div class="col-xl-5 text-left">
                                <a class="btn btn-light btn-circle" id="buttonback" runat="server" href="Ledger.aspx" data-toggle="tooltip" data-placement="bottom" title="Back to Ledger">

                                    <span class="fa fa-arrow-left text-primary"></span>

                                </a>
                                <button class="btn btn-light btn-circle " type="button" data-toggle="modal" data-target="#exampleModal9v">

                                    <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Adjust Account">
                                        <div>
                                            <i class="fas fa-adjust text-danger"></i>

                                        </div>
                                    </a>

                                </button>
                            </div>
                            <div class="col-xl-7 text-right">
                                <button runat="server" id="modalMain" type="button" class="btn btn-circle  btn-sm text-xs btn-danger" data-toggle="modal" data-target="#exampleModalLongService">
                                    <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Export as Excel">
                                        <div>
                                            <i class="fas fa-file-excel text-white"></i>

                                        </div>
                                    </a>
                                </button>
                                <button type="button" runat="server" id="Button2" class="mx-2 btn btn-circle  btn-sm text-xs btn-danger" data-toggle="modal" data-target="#exampleModal11">
                                    <a class="nav-link" data-toggle="tooltip" data-placement="bottom" title="Search By Amount by condition">
                                        <div>
                                            <i class="fas fa-search-location text-white font-weight-bold"></i>
                                            <span></span>
                                        </div>
                                    </a>
                                </button>
                                <button name="b_print" onclick="printdiv('div_print');" type="button" title="Print" class="btn btn-circle mx-1  btn-sm text-xs btn-danger" data-toggle="modal" data-target="#exampleModalCenter">
                                    <div>
                                        <i class="fas fa-print text-white font-weight-bold"></i>

                                    </div>
                                </button>
                                <button type="button" runat="server" id="Button1" class="btn btn-circle  btn-sm text-xs btn-danger" data-toggle="modal" data-target=".bd-example-modal-lg">
                                    <div>
                                        <i class="fas fa-search text-white font-weight-bold"></i>
                                        <span></span>
                                    </div>
                                </button>
                            </div>
                        </div>
                    </div>



                    <asp:Label ID="lblMsg" runat="server"></asp:Label>

                    <div class="row mt-5">
                        <div class="col-2">
                        </div>
                        <div class="col-8 ">
                            <div id="div_print">
                                <div class="card-body">
                                <div class="row" style="height: 90px">
                                    <div class="col-md-6 text-left">
                                        <img class="" src="../../asset/Brand/gh.jpg" alt="" width="110" height="80">
                                        <h5 id="oname" runat="server" class="mb-1 text-gray-900 border-top border-dark text-uppercase font-weight-bold " style="font-family: calibri"></h5>
                                        <span class="fas fa-address-book text-gray-400 mr-2"></span><span class="h6 small  text-gray-500 mt-1" id="CompAddress" runat="server"></span>
                                        <br />
                                        <span class="fas fa-phone text-gray-400 mr-2"></span><span class="h6 small text-gray-500 mt-1" id="Contact" runat="server"></span>

                                    </div>
                                    <div class="col-md-6 text-right">


                                        <span class="text-gray-900 h5 text-uppercase font-weight-bold">Ledger Detail</span>
                                        <br />
                                        <span class="h6 text-uppercase text-gray-600" id="account" runat="server"></span>
                                        <br />
                                        <span id="asof" runat="server"><span class="mb-2 border-top border-bottom text-gray-900 font-weight-bold ">As of<span class="h6 mx-1 mb-2 text-gray-900 font-weight-bold " id="mont" runat="server"></span></span></span>
                                        <span id="between" class="text-gray-900 font-weight-bold  border-top border-bottom" runat="server" visible="false"><span id="datefrom" runat="server"></span><span class="mx-2 mr-2">To</span><span id="dateto1" runat="server"></span></span>
                                        <h5 class="m-0 font-weight-bold text-gray-900 text-right" style="margin: 7px 5px 5px 5px; padding: 5px; background-color: #CCCCCC;">Account Summary<span id="Span5" class="text-gray-900 " runat="server"></span></h5>
                                        <div class="row  border-bottom border-top ">
                                            <div class="col-md-6">
                                                <h6 class="m-0 font-weight-bold text-left text-danger" style="margin: 7px 5px 5px 5px; padding: 5px" id="H2" runat="server">Beg. Balance</h6>
                                            </div>
                                            <div class="col-md-6 text-right">
                                                <span id="OpeningBal" class="text-danger font-weight-bold" runat="server"></span>
                                            </div>
                                        </div>
                                        <div class="row  border-bottom border-top ">
                                            <div class="col-md-6">
                                                <h6 class="m-0 font-weight-bold text-gray-900 text-left" style="margin: 7px 5px 5px 5px; padding: 5px" id="deb" runat="server">DEBIT</h6>
                                            </div>
                                            <div class="col-md-6 text-right">
                                                <span id="TotDebitor" class="text-gray-900  font-weight-bold" runat="server"></span>
                                            </div>
                                        </div>
                                        <div class="row border-bottom border-top">
                                            <div class="col-md-6">
                                                <h6 class="m-0 font-weight-bold text-gray-900 text-left" style="margin: 7px 5px 5px 5px; padding: 5px" id="cre" runat="server">CREDIT</h6>
                                            </div>
                                            <div class="col-md-6 text-right">
                                                <span id="TotalCreditor" class="text-gray-900 font-weight-bold" runat="server"></span>
                                            </div>
                                        </div>


                                        <div class="row border-top border-bottom">
                                            <div class="col-md-6">
                                                <h6 class="m-0 font-weight-bold text-danger text-left" style="margin: 7px 5px 5px 5px; padding: 5px">Ending Balance</h6>
                                            </div>
                                            <div class="col-md-6 text-right">
                                                <span id="TotBala" class="text-danger font-weight-bold" runat="server"></span>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                    </div>
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <div id="con" runat="server">
                                    <div class="row">
                                        <div class="col-12">
                                             <div class="card-body">

                                             
                                            <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">

                                                <HeaderTemplate>
                                                    <div class="table-responsive text-gray-900">
                                                        <table class="table align-items-center table-sm  table-hover" id="dataTable" width="100%" cellspacing="8">
                                                            <thead class="">
                                                                <tr>





                                                                    <th scope="col" class="text-gray-900">Debit</th>
                                                                    <th scope="col" class="text-gray-900">Credit</th>
                                                                    <th scope="col" class="text-gray-900">Balance</th>
                                                                    <th scope="col" class="text-gray-900">Explanation</th>
                                                                    <th scope="col" class="text-right text-danger small">
                                                                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="date">Date</asp:LinkButton></th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>




                                                        <td class="text-gray-900">
                                                            <%# Eval("Debit", "{0:N2}")%>
                                                        </td>

                                                        <td class="text-gray-900">
                                                            <%# Eval("Credit", "{0:N2}")%>
                                                        </td>

                                                        <td class="text-gray-900">
                                                            <%# Eval("Balance", "{0:N2}")%>
                                                        </td>
                                                        <td class="text-gray-900">
                                                            <%# Eval("Explanation")%>
                                                        </td>
                                                        <td class="text-gray-900  text-right">
                                                            <%# Eval("Date", "{0: dd MMM, yyyy}")%>
                                                        </td>



                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </tbody>
              </table>
                                                </FooterTemplate>

                                            </asp:Repeater>

                                             </div>
                    <div class="card-footer bg-white py-4">
                    <nav aria-label="...">
                        <ul class="pagination justify-content-end mb-0">
                            <br />
                            <td>
                                <asp:Label ID="Label1" runat="server" class="m-1 text-primary"></asp:Label></td>
                            <br />
                            <li class="page-item active">

                                <asp:Button ID="btnPrevious" class="btn btn-primary btn-sm btn-circle" runat="server" Text="<" OnClick="btnPrevious_Click" />

                            </li>


                            <li class="page-item active">

                                <asp:Button ID="btnNext" class="btn btn-sm btn-primary btn-circle mx-2" runat="server" Text=">" OnClick="btnNext_Click" />

                            </li>

                        </ul>
                    </nav>
                </div>
                                        </div>
                                        <div id="con1" runat="server" visible="false">
                                            <asp:Repeater ID="Repeater2" runat="server">

                                                <HeaderTemplate>
                                                    <div class="table-responsive text-gray-900">
                                                        <table class="table align-items-center table-sm  table-hover" id="dataTable" width="100%" cellspacing="8">
                                                            <thead class="">
                                                                <tr>





                                                                    <th scope="col" class="text-gray-900">Debit</th>
                                                                    <th scope="col" class="text-gray-900">Credit</th>
                                                                    <th scope="col" class="text-gray-900">Balance</th>
                                                                    <th scope="col" class="text-gray-900">Explanation</th>
                                                                    <th scope="col" class="text-right text-danger small">Date</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>




                                                        <td class="text-gray-900">
                                                            <%# Eval("Debit", "{0:N2}")%>
                                                        </td>

                                                        <td class="text-gray-900">
                                                            <%# Eval("Credit", "{0:N2}")%>
                                                        </td>

                                                        <td class="text-gray-900">
                                                            <%# Eval("Balance", "{0:N2}")%>
                                                        </td>
                                                        <td class="text-gray-900">
                                                            <%# Eval("Explanation")%>
                                                        </td>
                                                        <td class="text-gray-900  text-right">
                                                            <%# Eval("Date", "{0: dd MMM, yyyy}")%>
                                                        </td>



                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </tbody>
                                                    </table>
                                                </FooterTemplate>

                                            </asp:Repeater>
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
    </div>
</asp:Content>

