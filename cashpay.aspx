<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="cashpay.aspx.cs" Inherits="advtech.Finance.Accounta.cashpay" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Create Invoice</title>
    <script type="text/javascript">

        window.addEventListener('load', (event) => {
            var x = document.getElementById("Pbutton");
            x.style.display = "none";
    </script>

    <style>
        .ddlborder {
            border-color: #ff6a00
        }
    </style>
    <link href="../../asset/form-validation.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="modal fade" id="exampleModalS" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title text-gray-900 small font-weight-bold" id="exampleModalLabel1">Put Customer TIN# and Address</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:TextBox ID="txtTIN" class="form-control form-control-sm mx-2" placeholder="TIN#" runat="server"></asp:TextBox>

                        </div>


                    </div>
                    <div class="row mt-2 mb-2">
                        <div class="col-md-12">
                            <asp:TextBox ID="txtCustAddress" class="form-control form-control-sm mx-2" placeholder="Address" runat="server"></asp:TextBox>

                        </div>


                    </div>
                    <div class="col-md-12">
                        <asp:Button ID="btnUpdateTIN" runat="server" class="btn w-100 btn-danger btn-sm" Text="Update" OnClick="btnUpdateTIN_Click" />

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
                    <h5 class="modal-title text-gray-900 small font-weight-bold" id="exampleModalLabel">Update Service Charge</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-7">
                            <asp:TextBox ID="txtServiceChargeUpdate" class="form-control form-control-sm mx-2" placeholder="Amount [Monthly]" runat="server"></asp:TextBox>

                        </div>

                        <div class="col-md-5">
                            <asp:Button ID="btnUpdateSC" runat="server" class="btn btn-danger btn-sm" Text="Update" OnClick="btnUpdateSC_Click" />

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
    <div class="container" id="CCF" runat="server" visible="false">
        <div class="text-center mt-5">
            <span class="fas fa-6x fa-user-alt-slash"></span>
            <h1>Customer Couldn't be Found</h1>
            <p class="lead">Enter a correct customer name and try again.</p>
            <p>eg: start with " c- " and add customer name in top-bar search</p>
        </div>
    </div>
    <div class="container" style="position: relative;" id="container" runat="server">
        <div aria-live="polite" id="Tast" runat="server" aria-atomic="true" style="position: relative;">
            <!-- Position it -->
            <div style="position: absolute; top: 0; right: 8;">
                <div role="alert" aria-live="assertive" aria-atomic="true" class="toast" data-autohide="false" style="width: 250px">
                    <div class="toast-header">
                        <span class="fas fa-edit text-gray-400 mr-2"></span>
                        <strong class="mr-auto">Notes</strong>
                        <small id="date" runat="server"></small>
                        <button type="button" class="ml-2 mb-1 close" data-dismiss="toast" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>


                    <div class="toast-body">
                        <center>
                            <div id="Div1" visible="false" runat="server">
                                <span class="fas fa-book-reader text-gray-400 fa-2x"></span>
                                <br />
                                <span class="text-gray-400 small mt-1">No Notes</span>
                            </div>

                        </center>
                        <span class="border-bottom mb-2 small font-italic text-danger" id="Notes" runat="server"></span>
                        <br />
                        <asp:Button ID="btnClear" CausesValidation="false" OnClick="btnClear_Click" Visible="false" class="btn mt-2 btn-sm btn-link" runat="server" Text="clear notes" />
                    </div>
                </div>
            </div>
        </div>
        <span id="infoicon" visible="false" runat="server" class="fas fa-info-circle"></span>
        <asp:Label ID="lblMsg" runat="server" Text="Label" Visible="false"></asp:Label>

        <main>

            <div class="py-4 text-center">
                <span class="fas fa-hand-holding-usd text-danger fa-3x mb-2 border-danger border-bottom border-top border-left border-right btn-lg btn-circle"></span>
                <h2 class="font-weight-bold text-gray-900">Create Invoice</h2>
                <span class="fas fa-user-cog text-gray-400 mr-2"></span><span class="text-xs text-gray-500" id="CustCreate" runat="server"></span><a href="#" data-toggle="modal" data-target="#exampleModalS"><span class="small text-danger ml-2" data-toggle="tooltip" title="provide a TIN#" id="tinSpan" runat="server">[TIN# <span class="ml-1" id="TINNumber" runat="server" data-toggle="tooltip" title="provide a TIN#"></span>]</span></a>
                <br />
                <span id="duedate2" runat="server"></span>
            </div>

            <div class="row g-2">
                <div class="col-md-5 col-lg-4 order-md-last">
                    <h4 class="d-flex justify-content-between align-items-center mb-3">
                        <span class="text-primary mx-1">Payment Info</span>
                        <span class="badge bg-danger text-white rounded-pill">3</span>
                    </h4>
                    <ul class="list-group mb-3 snall">
                        <li class="list-group-item d-flex justify-content-between lh-sm">
                            <div>
                                <h6 class="my-0 text-gray-900 font-weight-bold">Invoice Amount</h6>
                                <small class="text-muted">Total amount [SC: <a href="#" data-toggle="modal" data-target="#exampleModal"><span data-toggle="tooltip" title="Update Service Charge" data-placement="top" id="ServieCharge" runat="server" class="text-danger"></span></a>]</small>
                            </div>
                            <span class="small font-weight-bold text-gray-900" id="Span1" runat="server"></span>
                        </li>
                        <li class="list-group-item d-flex justify-content-between lh-sm">
                            <div>
                                <h6 class="my-0 text-success">Overpayment</h6>
                                <small class="text-muted">Total overpayment</small>
                            </div>
                            <span class="small font-weight-bold text-gray-900" id="OverPayAmount" runat="server">0.00</span>
                        </li>
                        <li class="list-group-item d-flex justify-content-between lh-sm">
                            <div>
                                <h6 class="my-0">Credit</h6>
                                <div class="dropdown no-arrow" data-toggle="tooltip" data-placement="bottom" title="See credit analytics">
                                    <a class="dropdown-toggle btn-sm text-danger font-weight-bolder" href="#" role="button" id="dropdownMenuLink2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span class="fas fa-arrow-right mr-1"></span>Total credit</a>
                                    <div class="dropdown-menu  dropdown-menu-left shadow animated--fade-in animated--fade-in" style="width: 600px;" aria-labelledby="dropdownMenuLink2">
                                        <div class=" text-gray-900 h6 text-uppercase text-warning font-weight-bold text-center">Customer Credit Analytics</div>
                                        <span id="Dec" runat="server" class="mx-bar2 small text-gray-bg-gray-600"></span>
                                        <div class="dropdown-divider"></div>
                                        <div class="row" id="AnalyticsDiv" runat="server">
                                            <div class="col-1">
                                            </div>
                                            <div class="col-10">

                                                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">

                                                    <HeaderTemplate>

                                                        <table class="table align-items-center table-sm ">
                                                            <thead>
                                                                <tr>

                                                                    <th scope="col" class="text-gray-900 text-left">Title</th>
                                                                    <th scope="col" class="text-gray-900">Credit# </th>
                                                                    <th scope="col" class="text-gray-900">Invoice Amount</th>
                                                                    <th scope="col" class="text-gray-900  text-right">Credit Balance</th>




                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="text-gray-900  text-left">
                                                                <%# Eval("Notes")%>
                                                            </td>
                                                            <td class="text-primary">
                                                                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                                                <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Eval("id")%>'></asp:Label>
                                                            </td>
                                                            <asp:Label ID="lblCustomer" Visible="false" runat="server" Text='<%# Eval("customer")%>'></asp:Label>

                                                            <td class="text-gray-900">
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("amount" , "{0:N2}")%>'></asp:Label>

                                                            </td>
                                                            <td class="text-gray-900 text-right">
                                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("balance" , "{0:N2}")%>'></asp:Label>

                                                            </td>


                                                        </tr>

                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </tbody>
              </table>
                                                    </FooterTemplate>

                                                </asp:Repeater>
                                                <div class="dropdown-header text-gray-400"><span class="fas fa-donate text-gray-400 mr-1"></span>Credit Pattern</div>
                                                <span id="Span2" runat="server" class="mx-bar2 small text-gray-bg-gray-600"></span>
                                                <div class="dropdown-divider"></div>
                                                <div class="chart-area" style="height: 200px">
                                                    <asp:Literal ID="ltrIncreaseTrends" runat="server"></asp:Literal>
                                                </div>
                                                <span class="fas fa-info-circle text-gray-500"></span><span class="mx-2 text-xs text-gray-600">Values are in thousands</span>


                                            </div>
                                            <div class="col-1">
                                            </div>
                                        </div>
                                        <main role="main" id="main2" runat="server">
                                            <script>
            
                                            </script>
                                            <div class="starter-template">
                                                <center>


                                                    <p class="lead">

                                                        <i class="fas fa-chart-line text-gray-300  fa-7x"></i>

                                                    </p>
                                                    <h6 class="text-gray-700 small font-italic">No Data.</h6>
                                                </center>
                                            </div>



                                        </main>
                                    </div>
                                </div>

                            </div>
                            <span class="small mx-1 font-weight-bold text-gray-900" id="CashPay1" runat="server"></span>
                        </li>

                        <li class="list-group-item d-flex justify-content-between">
                            <span class="font-weight-bold text-success text-uppercase small">Total Payment (ETB)</span>
                            <strong><a href="#" id="btnCopy"><span class="small mx-1 font-weight-bold text-gray-900" data-toggle="tooltip" title="Copy to amount" data-placement="bottom" id="ExpecAmount" runat="server"></span></a></strong>
                        </li>
                    </ul>
                </div>
                <div class="col-md-7 col-lg-8">


                    <div class="row g-3">
                        <div class="col-sm-6">
                            <label for="lastName" class="form-label small text-gray-900 font-weight-bold">Amount</label>
                            <div class="form-group mb-0">
                                <div class="input-group input-group-alternative input-group-sm">
                                    <div class="input-group-prepend ">
                                        <span class="input-group-text">ETB</span>
                                    </div>
                                    <asp:TextBox ID="txtqtyhand" ClientIDMode="Static" class="form-control form-control-sm" runat="server"></asp:TextBox>
                                </div>
                            </div>


                        </div>

                        <div class="col-sm-6">
                            <label for="username" class="form-label small text-gray-900 font-weight-bold">Reference</label>
                            <div class="input-group has-validation">
                                <asp:TextBox ID="txtReference" required="true" class="form-control form-control-sm" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-12 mb-2">
                            <div class="custom-control mt-2  custom-checkbox ">
                                <input type="checkbox" class="custom-control-input" id="Checkbox3" checked="true" runat="server" clientidmode="Static" />
                                <label class="custom-control-label mt-1 small text-gray-900 " for="Checkbox3">Deduct Service Charge</label>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <label for="username" class="form-label small text-gray-900 font-weight-bold">FS no</label>
                            <div class="input-group has-validation">
                                <asp:TextBox ID="txtFSNo" class="form-control ddlborder form-control-sm" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-12">
                            <label for="username" class="form-label small text-gray-900 font-weight-bold">Notes</label>
                            <asp:TextBox ID="txtNotes" Height="100px" runat="server" placeholder="Put notes you want to remember for the next invoice. [less 40 character]" class="form-control form-control-sm" TextMode="MultiLine"></asp:TextBox>
                        </div>

                        <div class="col-12 mt-2 mb-3">
                            <label for="username" class="form-label small text-gray-900 font-weight-bold">Account</label>

                            <asp:DropDownList ID="ddlCashorBank" OnTextChanged="ddlCashorBank_TextChanged" AutoPostBack="true" CssClass="form-control ddlborder form-control-sm" runat="server">
                                <asp:ListItem>Cash on Hand</asp:ListItem>
                                <asp:ListItem>Cash at Bank</asp:ListItem>

                            </asp:DropDownList>
                        </div>

                        <div class="col-12 mt-2 mb-3" id="BankDiv" runat="server" visible="false">
                            <label for="username" class="form-label small text-gray-900 font-weight-bold">Bank Account</label>

                            <asp:DropDownList ID="DropDownList1" CssClass="form-control ddlborder form-control-sm" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="col-12 mt-2 mb-3" id="ChequeDiv" runat="server" visible="false">


                            <asp:TextBox ID="txtVoucher" class="form-control form-control-sm ddlborder" placeholder="Put Voucher or Cheque Number" runat="server"></asp:TextBox>
                            <div class="col-12">
                                <div class="custom-control mt-2  custom-checkbox ">
                                    <input type="checkbox" class="custom-control-input" id="CheckGene" checked="true" runat="server" clientidmode="Static" />
                                    <label class="custom-control-label mt-1 small text-gray-900 " for="CheckGene">Generate invoice</label>
                                </div>
                            </div>
                        </div>


                            <div class="col-12">
                                <div class="custom-control mb-1  custom-checkbox ">
                                    <input type="checkbox" class="custom-control-input" id="Checkbox2" runat="server" clientidmode="Static" />
                                    <label class="custom-control-label mt-1 small text-gray-900 " for="Checkbox2">Bind Credit</label>
                                    <br />
                                    <div id="MergeDiv"  style="display: none">
                                        <input type="checkbox" class="custom-control-input mx-3"  id="Checkbox1" runat="server" clientidmode="Static" />
                                        <label id="CheckBoxLebel" class="custom-control-label mt-1 small text-gray-900 " for="Checkbox1">Merge Credit</label>
                                    </div>

                                </div>
   
                            </div>
                        <div class="col-sm-12" id="creditNotes" style="display:none">
                            <label for="username" class="form-label small text-gray-900 font-weight-bold">New Credit Title</label>
                            <div class="input-group has-validation">
                                <asp:TextBox ID="txtCreditTitle" placeholder="eg. Service Charge Credit" class="form-control ddlborder form-control-sm" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-12 mb-4" id="ExistingCredit" style="display: none">
                            <label for="username" class="form-label small text-gray-900 font-weight-bold">Existing Credit Title</label>
                            <div class="input-group has-validation">

                                <asp:DropDownList ID="ddlExistingCredit" ClientIDMode="Static"  CssClass="form-control ddlborder form-control-sm" runat="server">
                                   
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>
                    <center>
                        <button class="btn btn-danger btn-sm" type="button" disabled id="Pbutton" style="display: none">
                            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                            Creating Invoice...
                        </button>
                    </center>
                    <div id="Bd">
                        <asp:Button ID="Button3" runat="server" class="w-100 btn btn-danger btn-md mt-4" Text="Create Invoice" OnClick="Button3_Click" OnClientClick="myFunctionshop()" />

                    </div>
                    <script>
            function myFunctionshop() {
                var y = document.getElementById("<%=Button3.ClientID %>"); var x = document.getElementById("Pbutton");
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
                </div>
            </div>
        </main>

    </div>
    
    <script>
            $('#Checkbox2').click(function () {
                
                $("#creditModal").toggle(this.checked);
                $("#creditNotes").toggle(this.checked);
                $("#MergeDiv").toggle(this.checked);
            });
    </script>

    <script>
            $('#Checkbox1').click(function () {
                $("#ExistingCredit").toggle(this.checked);
                $("#creditNotes").toggle(this.unchecked);
            });
    </script>

    <script type="text/javascript">
            $(function () {
                $("[id*=btnCopy]").bind("click", function () {

                    var expectedAmount = document.getElementById("<%=ExpecAmount.ClientID %>");
                    $("#txtqtyhand").val(expectedAmount.innerHTML);
                });
            });
    </script>
    <script src="../../asset/form-validation.js"></script>
</asp:Content>
