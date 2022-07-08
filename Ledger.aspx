<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.master" AutoEventWireup="true" CodeBehind="Ledger.aspx.cs" Inherits="advtech.Finance.Accounta.Ledger" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>General Ledger</title>

    <script type="text/javascript">

        window.addEventListener('load', (event) => {
            var x = document.getElementById("InfoDiv");
            x.style.display = "none";
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <!-- Navbar -->

    <div class="container-fluid pl-3 pr-3">
        <div class="modal fade" id="ExportPeachtree" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle3" aria-hidden="true">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title h6 font-weight-bold text-gray-900" id="exampleModalLongTitle3"><span class="fas fa-download mr-2" style="color:#e61ce8"></span> CSV for Peachtree</h5>
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
                                            <asp:TextBox ID="txtCsvDateFrom" class="form-control form-control-sm " TextMode="Date" runat="server"></asp:TextBox>


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
                                            <asp:TextBox ID="txtCsvDateTo" class="form-control form-control-sm " TextMode="Date" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-12">
                                <div class="form-group">
                                    <center>
                                        <div id="InfoDiv" style="display: none" runat="server" ClientIDMode="Static">
                                            <span class="spinner-border spinner-border-sm" style="color:#ff2ccd;"></span>
                                            <br />
                                            <span class="text-xs text-gray-900">Processing...</span>
                                            <br />
                                            <span class="text-xs text-gray-900">This could take a few minute...</span>
                                        </div>
                                    </center>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <div class="form-group">
                                    <center>
                                        <asp:LinkButton ID="btnPeachtreeExport" OnClientClick="myFunctionshopSM();" OnClick="Button1_Click" class="btn text-white  btn-sm w-100" Style="background-color: #ff2ccd" runat="server"><span class="fas fa-download mr-2" style="color:#ffffff"  ></span>Convert & Export</asp:LinkButton>

                                    </center>



                                </div>
                            </div>
                        </div>


                    </div>

                </div>
            </div>
        </div>
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
                                        <asp:Button ID="btnUncollected" class="btn btn-danger w-50" runat="server" Text="Export" OnClick="btnUncollected_Click" />
                                    </center>



                                </div>
                            </div>
                        </div>


                    </div>

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
                            <asp:Button ID="btnBindBussinessSumary" class="btn btn-sm btn-danger" OnClientClick="myFunctionshop22();" Style="background-color: #ff2ccd" runat="server"
                                Text="Bind Summary" />
                    </div>
                    </center>
                </div>
            </div>
        </div>
        <!-- Table -->
        <div class="row">
            <div class="col">
                <div class="rounded-lg bg-white mb-1 ">
                    <div class="card-header bg-white ">
                        <div class="row">
                            <div class="col-6 text-left">
                                <span class="fas fa-hashtag mr-2" style="color: #ff00bb"></span><span class="m-0 font-weight-bold h5 text-gray-900">General Ledger</span>  <span class="fas fa-calendar text-white btn-circle btn-sm mx-2 " style="background-color: #ff6a00"></span><span id="SystemDate" runat="server" class="small text-gray-900 mx-1"></span>
                            </div>
                            <div class="col-6 text-right ">
                                <div class="dropdown no-arrow">
                                    <button type="button" data-toggle="modal" data-target="#ExportPeachtree" class="btn mt-1 mr-2 mb-1 btn-circle btn-sm btn-success"><span data-toggle="tooltip" title="Export for Peachtree Software" class="fas fa-download text-white"></span></button>

                                    <button type="button" data-toggle="modal" data-target="#EX" class="btn mt-1 mr-2 mb-1 btn-circle btn-sm btn-warning"><span data-toggle="tooltip" title="Export as excel" class="fas fa-file-excel text-white"></span></button>

                                    <button class="btn btn-light btn-circle btn-sm dropdown-toggle" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">

                                        <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Options">
                                            <div>
                                                <i class="fas fa-caret-down text-danger"></i>

                                            </div>
                                        </a>

                                    </button>
                                    <div class="dropdown-menu  dropdown-menu-right shadow animated--fade-in" aria-labelledby="dropdownMenuLink">
                                        <div class="dropdown-header">Option:</div>
                                        <a class="dropdown-item" id="CustLink" runat="server" href="Ledger.aspx?asset=true">Group Asset Acount</a>
                                        <a class="dropdown-item" id="A6" runat="server" href="Ledger.aspx?liability=true">Group Liability Account</a>

                                        <a class="dropdown-item" id="A2" runat="server" href="Ledger.aspx?income=true">Group Income Account</a>
                                        <a class="dropdown-item" id="A3" runat="server" href="Ledger.aspx?expense=true">Group Expense Account</a>
                                        <a class="dropdown-item" id="A1" runat="server" href="Ledger.aspx?accdep=true">Group Accumulated Depreciation</a>
                                        <a class="dropdown-item" id="A7" runat="server" href="Ledger.aspx">Show all</a>


                                        <a class="dropdown-item text-danger" href="#" id="A5" runat="server" data-toggle="modal" data-target="#exampleModal">Search Account</a>
                                    </div>
                                </div>




                            </div>
                        </div>



                    </div>

                    <div class="row">
                        <div class="col-xl-6 border-right">

                            <div id="con1" runat="server">

                                <div class="card-body">
                                    <asp:Repeater ID="Repeater1" runat="server">

                                        <HeaderTemplate>
                                            <div class="table-responsive small">
                                                <table class="table align-items-center table-sm " id="dataTable" width="100%" cellspacing="0">
                                                    <thead>
                                                        <tr>

                                                            <th scope="col">Account Name</th>

                                                            <th class="text-right">Balance</th>



                                                        </tr>
                                                    </thead>
                                                    <tfoot>
                                                        <tr>
                                                            <th scope="col">Account Name</th>

                                                            <th class="text-right">Balance</th>


                                                        </tr>
                                                    </tfoot>
                                                    <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>

                                                <td class="text-primary">
                                                    <a title="Show the details" href="Ledger_analysis_details.aspx?led=<%# Eval("Account")%>"><%# Eval("Account")%></a>

                                                </td>

                                                <td class="text-right text-gray-900">
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Balance", "{0:N2}")%>'></asp:Label>

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


                            <div class="card-footer bg-white py-4">
                                <nav aria-label="...">
                                    <ul class="pagination justify-content-end mb-0 small">
                                        <br />
                                        <td>
                                            <asp:Label ID="Label1" runat="server" class="m-1 text-primary"></asp:Label></td>
                                        <br />
                                        <li class="page-item active">

                                            <asp:Button ID="btnPrevious" class="btn btn-sm btn-warning btn-circle" runat="server" Text="<" OnClick="btnPrevious_Click" />

                                        </li>


                                        <li class="page-item active">

                                            <asp:Button ID="btnNext" class="btn btn-sm btn-warning btn-circle mx-2" runat="server" Text=">" OnClick="btnNext_Click" />

                                        </li>

                                    </ul>
                                </nav>
                            </div>


                        </div>
                        <div class="col-xl-6">


                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-6 border-right shadow-sm">
                                        <center>
                                            <span class="fas fa-balance-scale text-warning fa-3x border-bottom border-top border-right border-left border-warning btn-circle btn-lg"></span>
                                            <h6 class="text-uppercase small text-gray-900 mt-3">ASSET BALANCE</h6>
                                            <h6 id="AssetBalance" runat="server" class="text-uppercase small text-gray-900 mt-3 font-weight-bold">$500,000</h6>
                                        </center>
                                        <div class="row mt-5  ">
                                            <div class="col-md-1">
                                            </div>
                                            <div class="col-md-5">
                                                <h6 class="text-uppercase  font-weight-light small text-warning mt-5">Debit</h6>
                                                <h6 id="AssetDebit" runat="server" class="text-uppercase text-xs text-gray-900 mt-2 font-weight-bold">ETB 0.00</h6>
                                            </div>

                                            <div class="col-md-5 text-right">
                                                <h6 class="text-uppercase small font-weight-light text-warning mt-5">Credit</h6>
                                                <h6 id="AssetCredit" runat="server" class="text-uppercase text-xs text-gray-900 mt-2 font-weight-bold">$500,000</h6>
                                            </div>
                                            <div class="col-md-1">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 border-right shadow-sm">
                                        <center>
                                            <span class="fas fa-balance-scale-right text-danger fa-3x border-bottom border-top border-right border-left border-danger btn-circle btn-lg"></span>
                                            <h6 class="text-uppercase small text-gray-900 mt-3">LIABILITY BALANCE</h6>
                                            <h6 id="LiabilityBlance" runat="server" class="text-uppercase small text-gray-900 mt-3 font-weight-bold">$500,000</h6>
                                        </center>
                                        <div class="row mt-5 border-bottom ">
                                            <div class="col-md-1">
                                            </div>
                                            <div class="col-md-5">
                                                <h6 class="text-uppercase  font-weight-light small text-danger mt-5">CREDIT</h6>
                                                <h6 id="LiabilittCredit" runat="server" class="text-uppercase text-xs text-gray-900 mt-3 font-weight-bold">$500,000</h6>
                                            </div>

                                            <div class="col-md-5 text-right">
                                                <h6 class="text-uppercase small font-weight-light text-danger mt-5">DEBIT</h6>
                                                <h6 id="LiabilityDebit" runat="server" class="text-uppercase small text-gray-900 mt-3 font-weight-bold">$500,000</h6>
                                            </div>
                                            <div class="col-md-1">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12 border-right shadow-sm">
                                        <center>
                                            <span class="fas fa-coins text-success fa-3x border-bottom border-top border-right border-left border-success btn-circle btn-lg"></span>
                                            <h6 class="text-uppercase small text-gray-900 mt-3">Cash Balance</h6>
                                            <h6 id="CashBalance" runat="server" class="text-uppercase small text-gray-900 mt-3 font-weight-bold">0.00</h6>
                                        </center>
                                        <div class="row mt-5 border-bottom ">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                <h6 class="text-uppercase  font-weight-light small text-danger mt-4">Current Year</h6>
                                                <h6 id="CashCurrent" runat="server" class="text-uppercase small text-gray-900 mt-3 font-weight-bold">0.00</h6>
                                            </div>

                                            <div class="col-md-4 text-right">
                                                <h6 class="text-uppercase small font-weight-light text-danger mt-4">Last Year</h6>
                                                <h6 id="CashLast" runat="server" class="text-uppercase small text-gray-900 mt-3 font-weight-bold">0.00</h6>
                                            </div>
                                            <div class="col-md-2">
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-6 border-right shadow-sm">
                                        <center>
                                            <span class="fas fa-chart-line text-warning fa-3x border-bottom border-top border-right border-left border-warning btn-circle btn-lg"></span>
                                            <h6 class="text-uppercase small text-gray-900 mt-3">REVENUE BALANCE</h6>
                                            <h6 id="RevenueBalance" runat="server" class="text-uppercase small text-gray-900 mt-3 font-weight-bold">$500,000</h6>
                                        </center>
                                        <div class="row mt-5 border-bottom ">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                <h6 class="text-uppercase  font-weight-light small text-warning mt-5">Debit</h6>
                                                <h6 id="RevenueDebit" runat="server" class="text-uppercase text-xs text-gray-900 mt-2 font-weight-bold">ETB 0.00</h6>
                                            </div>

                                            <div class="col-md-4 text-right">
                                                <h6 class="text-uppercase small font-weight-light text-warning mt-5">Credit</h6>
                                                <h6 id="RevenueCredit" runat="server" class="text-uppercase text-xs text-gray-900 mt-2 font-weight-bold">$500,000</h6>
                                            </div>
                                            <div class="col-md-2">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 border-right shadow-sm">
                                        <center>
                                            <span class="fas fa-comment-dollar text-danger fa-3x border-bottom border-top border-right border-left border-danger btn-circle btn-lg"></span>
                                            <h6 class="text-uppercase small text-gray-900 mt-3">EXPENSE BALANCE</h6>
                                            <h6 id="ExpenseBalance" runat="server" class="text-uppercase small text-gray-900 mt-3 font-weight-bold">$500,000</h6>
                                        </center>
                                        <div class="row mt-5 border-bottom ">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-4">
                                                <h6 class="text-uppercase  font-weight-light small text-danger mt-5">CREDIT</h6>
                                                <h6 id="ExpenseDebit" runat="server" class="text-uppercase text-xs text-gray-900 mt-3 font-weight-bold">$500,000</h6>
                                            </div>

                                            <div class="col-md-4 text-right">
                                                <h6 class="text-uppercase small font-weight-light text-danger mt-5">DEBIT</h6>
                                                <h6 id="ExpenseCredit" runat="server" class="text-uppercase text-xs text-gray-900 mt-3 font-weight-bold">$500,000</h6>
                                            </div>
                                            <div class="col-md-2">
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

        <div id="CSVTable" runat="server" visible="false">
            <span id="counter" runat="server" ></span>
            <asp:Repeater ID="Repeater2" runat="server" >

                <HeaderTemplate>
                    <div class="table-responsive small">
                        <table class="table align-items-center table-bordered table-sm " id="dataTable" width="100%" cellspacing="0">
                            <thead>
                                <tr>

                                    <th scope="col">Date</th>

                                    <th class="text-right">Reference
                                    </th>
                                    <th class="text-right">Date Clear in Bank Rec

                                    </th>
                                    <th class="text-right">Number of Distributions

                                    </th>
                                    <th class="text-right">G/L Account

                                    </th>
                                    <th class="text-right">Description

                                    </th>
                                    <th class="text-right">Amount

                                    </th>
                                    <th class="text-right">Job ID

                                    </th>
                                    <th class="text-right">Used for Reimbursable Expenses

                                    </th>
                                    <th class="text-right">Transaction Period

                                    </th>
                                    <th class="text-right">Transaction Number

                                    </th>
                                    <th class="text-right">Consolidated Transaction

                                    </th>
                                    <th class="text-right">Recur Number

                                    </th>
                                    <th class="text-right">Recur Frequency

                                    </th>

                                </tr>
                            </thead>
                            <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>

                        <td class="text-primary"><%# Eval("Date")%></td>
                        <td class="text-right text-gray-900"></td>
                        <td class="text-right text-gray-900"></td>
                        <td class="text-right text-gray-900">2</td>
                        <td class="text-right text-gray-900">
                            <asp:Label ID="lblAccountNumber" runat="server"></asp:Label>
                            <asp:Label ID="lblAccount" Visible="false" runat="server" Text='<%# Eval("Account")%>'></asp:Label>
                            <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("LedID")%>'></asp:Label>
                        </td>
                        <td class="text-right text-gray-900"><%# Eval("Explanation")%></td>
                        <td class="text-right text-gray-900">
                            <asp:Label ID="lblAmount" runat="server"></asp:Label></td>
                        <td class="text-right text-gray-900"></td>
                        <td class="text-right text-gray-900">FALSE</td>
                        <td class="text-right text-gray-900"></td>
                        <td class="text-right text-gray-900"></td>
                        <td class="text-right text-gray-900">FALSE</td>
                        <td class="text-right text-gray-900">0</td>
                        <td class="text-right text-gray-900">0</td>
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
    <!-- Dark table -->

    <!-- Footer -->


    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title text-gray-900" id="exampleModalLabel">Type Account Name</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-7">
                            <asp:TextBox ID="txtAccountName" class="form-control mx-2" runat="server"></asp:TextBox>

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

    <div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLongTitle">Add Transaction</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-5 ">
                            <div class="form-group">
                                <label>Account</label>
                                <asp:DropDownList ID="ddl1" runat="server"
                                    class="form-control ">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-5 ">
                            <div class="form-group">
                                <label>Explanation</label>


                                <br />
                                <asp:TextBox ID="txtExplanation" class="form-control" placeholder="Explanation" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-5 ">
                            <div class="form-group">
                                <label>Ref.</label>


                                <br />
                                <asp:TextBox ID="txtRef" class="form-control " placeholder="Reference" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-5 ">
                            <div class="form-group">
                                <label>Debit</label>


                                <br />
                                <asp:TextBox ID="txtDebit" TextMode="Number" class="form-control " placeholder="Debit" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-5 ">
                            <div class="form-group">
                                <label>Credit</label>


                                <br />
                                <asp:TextBox ID="txtCredit" class="form-control" placeholder="Credit" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-5 ">
                            <div class="form-group">
                                <label>Date</label>


                                <br />
                                <asp:TextBox ID="txtDate" TextMode="Date" class="form-control " placeholder="Date" runat="server"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="modal-footer">
                    <center>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <asp:Button ID="Button3" class="btn btn-default btn-icon-split" runat="server"
                            Text="Save changes" OnClick="btnUpdate_Click" />
                </div>
                </center>
            </div>
        </div>
    </div>


    <!-- Modal -->
    <div class="modal fade" id="exampleModalCenter2" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="H1">Register Account</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">


                        <div class="col-12 " style="font-weight: bold">
                            <div class="form-group">
                                <label>Account type</label>



                                <asp:TextBox ID="txtAccounttype" class="form-control " placeholder="Account Name" runat="server"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-12 " style="font-weight: bold">
                            <div class="form-group">
                                <label>Account No.</label>



                                <asp:TextBox ID="txtAccountNo" class="form-control " runat="server"></asp:TextBox>
                                <asp:LinkButton
                                    ID="LinkButton1" runat="server"></asp:LinkButton>


                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 " style="font-weight: bold">
                            <div class="form-group">
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 " style="font-weight: bold">
                            <div class="form-group">
                                <label style="font-weight: bold">Dr./Cr.</label>

                                <asp:RadioButton ID="RadioButton5" runat="server" Text="Dr." ForeColor="#6666FF" GroupName="AT1" />
                                <asp:RadioButton ID="RadioButton6" runat="server" Text="Cr." ForeColor="#6666FF" GroupName="AT1" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12  " item="item" style="font-weight: bold">
                            <div class="form-group">
                                Account Division:<asp:DropDownList ID="DropDownList1" runat="server"
                                    class="form-control ">
                                    <asp:ListItem>Main Account</asp:ListItem>
                                    <asp:ListItem>Sub Account</asp:ListItem>

                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                    </div>
                    <div class="row">

                        <div class="col-12  " style="font-weight: bold">
                            <div class="form-group">
                                Account Category:<asp:DropDownList ID="DropDownList2" runat="server"
                                    class="form-control ">
                                    <asp:ListItem>Income Statement</asp:ListItem>
                                    <asp:ListItem>Balance Sheet</asp:ListItem>
                                    <asp:ListItem>Cash Flow</asp:ListItem>

                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 " style="font-weight: bold">
                            <div class="form-group">
                                <label style="font-weight: bold">Operation type:</label>



                                <asp:RadioButton ID="RadioButton1" Text="Income" GroupName="AT" runat="server" EnableTheming="True" ForeColor="#6666FF" Checked="True" />

                                <asp:RadioButton ID="RadioButton2" runat="server" Text="Deposit" ForeColor="#6666FF" GroupName="AT" />
                                <asp:RadioButton ID="RadioButton3" runat="server" Text="Payment" ForeColor="#6666FF" GroupName="AT" />
                                <asp:RadioButton ID="RadioButton4" runat="server" Text="None" ForeColor="#6666FF" GroupName="AT" />
                            </div>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-12  ">
                            <div class="form-group">
                                Parent Account In F/S:<asp:DropDownList ID="DropDownList3" runat="server"
                                    class="form-control ">
                                    <asp:ListItem>Income Statement</asp:ListItem>
                                    <asp:ListItem>Balance Sheet</asp:ListItem>
                                    <asp:ListItem>Cash Flow</asp:ListItem>

                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-12  ">
                            <div class="form-group">
                                Account type:<asp:DropDownList ID="DropDownList4" runat="server"
                                    class="form-control ">
                                    <asp:ListItem>Asset</asp:ListItem>
                                    <asp:ListItem>Liabilty</asp:ListItem>
                                    <asp:ListItem>Owner's Equity</asp:ListItem>

                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <center>
                        <button type="button" class="btn btn-secondary btn-sm" data-dismiss="modal">Close</button>
                        <asp:Button ID="btnUpdate2" class="btn btn-default btn-sm" runat="server"
                            Text="Save changes" OnClick="btnUpdate2_Click" />
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
                        <div class="col-md-5 ">
                            <div class="form-group">
                                <label class="form-control-label" for="input-email">Account type</label>
                                <asp:DropDownList ID="ddl2" runat="server"
                                    class="form-control form-control-alternative">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary btn-sm" data-dismiss="modal">Close</button>

                        <asp:Button ID="Button7" runat="server" class="btn btn-sm btn-primary"
                            Text="Search..." Height="56" OnClick="Button7_Click" />
                    </div>
                </div>
            </div>
        </div>
        <script>

            function myFunctionshopSM() {
                var x = document.getElementById("InfoDiv");

                if (x.style.display === "none") {
                    x.style.display = "block";
                    setTimeout(function () { x.style.display = "none";},1000)
                } 
            }
        </script>
    </div>
</asp:Content>

