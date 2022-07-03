<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="creditnote.aspx.cs" Inherits="advtech.Finance.Accounta.creditnote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Credit Note</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" id="NoInvoiceDiv" runat="server" visible="false">
        <div class="text-center mt-5">
            <span class="fas fa-6x mb-2 fa-dollar-sign text-gray-300"></span>
            <h1>No Credit Created</h1>

        </div>
    </div>
    <!-- Navbar -->

    <div class="container-fluid pl-3 pr-3" id="container" runat="server">
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




        <div class="modal fade" id="exampleModal11" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel11" aria-hidden="true">
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

        <div class="row">
            <div class="col">

                <div class="bg-white rounded-lg">
                    <div class="card-header bg-white ">
                        <div class="row">
                            <div class="col-md-4 text-left">
                                <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown1"
                                    role="button" data-toggle="dropdown" aria-haspopup="true"
                                    aria-expanded="false">
                                    <span id="cashdrop" class="small" runat="server">CREDIT NOTES</span>
                                </a>
                                <div class="dropdown-menu dropdown-menu-left animated--fade-in"
                                    aria-labelledby="navbarDropdown1">
                                    <a class="dropdown-item" href="#">
                                        <asp:Button ID="btnBindFreeShops" OnClick="btnBindFreeShops_Click" class="btn  w-100 btn-sm btn-light" runat="server" Text="Bind Pending" />

                                    </a>
                                    <a class="dropdown-item" href="#">
                                        <asp:Button ID="btnWriteOff" OnClick="btnBindWrite_Click" class="btn  w-100 btn-sm btn-warning" runat="server" Text="Bind Write off" />

                                    </a>
                                    <a class="dropdown-item" href="#">
                                        <asp:Button ID="btnbindall" OnClick="btnbindall_Click" class="btn btn-sm btn-light  w-100" runat="server" Text="Bind All" />

                                    </a>
                                </div>
                            </div>
                            <div class="col-md-8 text-right">

                                <button runat="server" id="modalMain" type="button" class="btn btn-circle mx-1  btn-sm text-xs btn-danger" data-toggle="modal" data-target="#exampleModalLongService">
                                    <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Export as Excel">
                                        <div>
                                            <i class="fas fa-file-excel text-white"></i>

                                        </div>
                                    </a>
                                </button>
                                <button type="button" runat="server" id="Button1" class="border-primary mx-2 border-left border-top border-right border-bottom btn btn-sm btn-default btn-circle" data-toggle="modal" data-target="#exampleModal11">
                                    <a class="nav-link" data-toggle="tooltip" data-placement="bottom" title="Search By Amount by condition">
                                        <div>
                                            <i class="fas fa-search-location text-primary font-weight-bold"></i>
                                            <span></span>
                                        </div>
                                    </a>
                                </button>
                                <button type="button" runat="server" id="Sp2" class="border-primary border-left border-top border-right border-bottom btn btn-sm btn-default btn-circle" data-toggle="modal" data-target="#exampleModal">
                                    <div>
                                        <i class="fas fa-search text-primary font-weight-bold"></i>
                                        <span></span>
                                    </div>
                                </button>

                            </div>
                        </div>

                    </div>
                    <div id="con" runat="server">
                        <div class="card-body small text-gray-900">
                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">

                                <HeaderTemplate>
                                    <div class="table-responsive">
                                        <table class="table align-items-center table-sm ">
                                            <thead>
                                                <tr>

                                                    <th></th>
                                                    <th scope="col" class="text-gray-900">Date</th>
                                                    <th scope="col" class="text-gray-900">Credit# </th>
                                                    <th scope="col" class="text-gray-900">Customer </th>
                                                    <th scope="col" class="text-gray-900">Invoice Amount(VAT+) </th>
                                                    <th scope="col" class="text-gray-900">Credit Balance</th>
                                                    <th scope="col" class="text-gray-900">Ref#</th>
                                                    <th scope="col" class="text-gray-900">Due Date</th>
                                                    <th scope="col" class="text-gray-900">Status</th>



                                                </tr>
                                            </thead>
                                            <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Literal ID="LiteralDropdown" runat="server">


                                            </asp:Literal>


                                        </td>
                                        <td class="text-gray-900">
                                            <%# Eval("date", "{0: dd/MM/yyyy}")%>
                                        </td>
                                        <td class="text-primary">
                                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                            <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Eval("id")%>'></asp:Label>
                                        </td>

                                        <td class="text-gray-900">
                                            <asp:Label ID="lblCustomer" runat="server" Text='<%# Eval("customer")%>'></asp:Label>
                                        </td>
                                        <td class="text-gray-900">
                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("amount" , "{0:N2}")%>'></asp:Label>

                                        </td>
                                        <td class="text-gray-900">
                                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("balance" , "{0:N2}")%>'></asp:Label>

                                        </td>
                                        <td class="text-gray-900">
                                            <asp:Label ID="lblExp" runat="server" Text='<%# Eval("ref")%>'></asp:Label>
                                        </td>
                                        <td class="text-gray-900">
                                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("duedate","{0: MMMM dd, yyyy}")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label2" class="badge badge-danger" runat="server" Text="Pending"></asp:Label>
                                            <asp:Label ID="Label3" class="badge badge-success" runat="server" Text="Completed"></asp:Label>
                                            <asp:Label ID="Label_write_off" class="badge badge-warning" runat="server" Text="Writte Off"></asp:Label>
                                        </td>

                                    </tr>

                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
              </table>
                                </FooterTemplate>

                            </asp:Repeater>
                        </div>
                        <center>

                            <main role="main" id="mainb" class="mt-5" runat="server" visible="false">

                                <div class="starter-template">
                                    <center>


                                        <p class="lead">

                                            <i class="fas fa-donate text-gray-300  fa-5x"></i>

                                        </p>
                                        <h6 class="text-gray-700 h6 font-italic">No Credit Found</h6>
                                    </center>
                                </div>



                            </main>
                        </center>
                    </div>



                </div>
                <div class="card-footer bg-white py-4" id="buttondiv" runat="server">
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
        </div>
    </div>

</asp:Content>
