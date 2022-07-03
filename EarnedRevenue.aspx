<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="EarnedRevenue.aspx.cs" Inherits="advtech.Finance.Accounta.EarnedRevenue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Earned Revenue</title>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid pl-3 pr-3">
        <div class="modal fade bd-example-modal-lg" id="daterange" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
                        <h5 class="modal-title" id="H1">Filter By Date Range</h5>
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
                            <asp:Button ID="btnUpdate" class="btn btn-primary" OnClick="btnUpdate_Click" runat="server"
                                Text="Search..." />
                    </div>
                    </center>
                </div>
            </div>
        </div>
        <div class="modal fade" id="exampleModalLongService2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle32" aria-hidden="true">
            <div class="modal-dialog modal-sm" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title small font-weight-bold text-gray-900" id="exampleModalLongTitle32">Show all revenue collected lists</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">


                        <div class="row">
                            <div class="col-12">
                                <div class="form-group">


                                    <asp:Button ID="btnShowAll" class="btn btn-sm btn-danger w-100" runat="server" OnClientClick=" myFunctionshopB()" Text="Show all" OnClick="btnShowAll_Click" />

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
                        <h5 class="modal-title h6 font-weight-bold text-gray-900" id="exampleModalLongTitle3">Search by condition</h5>
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
                            <div class="col-12">
                                <div class="form-group">


                                    <asp:Button ID="btnCondition" class="btn btn-danger w-100" runat="server" OnClientClick=" myFunctionshop2()" Text="Search.." OnClick="btnCondition_Click" />

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
                <div class="bg-white mb-1 rounded-lg">
                    <div class="card-header bg-white ">


                        <div class="row">
                            <div class="col-4 text-left ">
                                <a href="Home.aspx" class="btn btn-default btn-sm">
                                    <i class="fas fa-arrow-left  font-weight-bold text-primary"></i><span id="account" class="font-weight-bold text-primary mx-3" runat="server"></span>
                                </a>



                            </div>
                            <div class="col-8 text-right ">
                                <button runat="server" id="Button2" type="button" class="btn btn-circle mr-2 btn-sm text-xs btn-warning" data-toggle="modal" data-target="#exampleModalLongService2">
                                    <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Show all">
                                        <div>
                                            <i class="fas fa-list-ol text-white"></i>

                                        </div>
                                    </a>
                                </button>
                                <button runat="server" id="modalMain" type="button" class="btn btn-circle mr-2 btn-sm text-xs btn-danger" data-toggle="modal" data-target="#exampleModalLongService">
                                    <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Search by conditions">
                                        <div>
                                            <i class="fas fa-greater-than-equal text-white"></i>

                                        </div>
                                    </a>
                                </button>
                                <button type="button" runat="server" id="Button1" class="mx-1 border-primary border-left border-top border-right border-bottom btn btn-sm btn-default btn-circle" data-toggle="modal" data-target="#daterange">
                                    <a class="nav-link" data-toggle="tooltip" data-placement="top" title="Search By Date Range">
                                        <div>
                                            <i class="fas fa-search text-primary font-weight-bold"></i>
                                            <span></span>
                                        </div>
                                    </a>
                                </button>

                                <button name="b_print" onclick="printdiv('div_print');" type="button" title="Print" class="mx-1 border-primary border-left border-top border-right border-bottom btn btn-sm btn-default btn-circle" data-toggle="modal" data-target="#exampleModalCenter">
                                    <div>
                                        <i class="fas fa-print text-primary font-weight-bold"></i>

                                    </div>
                                </button>


                            </div>
                        </div>

                    </div>

                    <div id="div_print">


                        <div class="row mt-5">
                            <div class="col-2">
                            </div>
                            <div class="col-8 small text-gray-900">
                                <div class="row " style="height: 90px">
                                    <div class="col-md-6 text-left">
                                        <img class="" src="../../asset/Brand/gh.jpg" alt="" width="110" height="80">
                                        <br />
                                        <h5 id="oname" runat="server" class="mb-1 border-top border-dark text-uppercase text-gray-900 font-weight-bold "></h5>
                                    </div>
                                    <div class="col-md-6 text-right ">

                                        <h4 class="text-gray-900 font-weight-bold">REVENUE REPORT</h4>
                                        <span id="datFrom1" runat="server" class="mb-1 text-gray-900 font-weight-bold"></span><span id="tomiddle" class="mb-1 mr-2 ml-2 text-gray-900 font-weight-bold" runat="server">To</span><span id="datTo" class="mb-1 text-gray-900 font-weight-bold" runat="server"></span>
                                        <span class="mb-2 text-gray-900 font-weight-bold "><span class="h6 mx-1 mb-2 text-gray-900 font-weight-bold " id="mont" runat="server"></span></span>
                                    </div>

                                </div>
                                <br />
                                <br />
                                <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">

                                    <HeaderTemplate>
                                        <div class="table-responsive">
                                            <table class="table align-items-center table-sm ">
                                                <thead>
                                                    <tr>


                                                        <th scope="col">
                                                            <asp:LinkButton ID="LinkButton1" runat="server" class="text-gray-900" CommandName="Customer">CUSTOMER</asp:LinkButton></th>

                                                        <th>REFERENCE</th>
                                                        <th scope="col" class="text-right">
                                                            <asp:LinkButton ID="LinkButton2" class="text-gray-900" runat="server" CommandName="Date">DATE</asp:LinkButton></th>
                                                        <th scope="col" class="text-right">
                                                            <asp:LinkButton ID="LinkButton3" class="text-danger" runat="server" CommandName="Payment">AMOUNT</asp:LinkButton></th>



                                                    </tr>
                                                </thead>
                                                <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="text-gray-900">
                                                <%# Eval("Customer")%>
                                            </td>

                                            <td><%# Eval("Trans")%></td>
                                            <td class="text-right text-gray-900">
                                                <%# Eval("Date","{0:MMMM dd, yyyy}")%>
                    
                                            </td>
                                            <td class="text-right text-gray-900">
                                                <%# Eval("Payment","{0:N2}")%>
                                            </td>
                                        </tr>

                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
              </table>
                                    </FooterTemplate>

                                </asp:Repeater>
                            </div>
                            <div class="col-2">
                            </div>
                            <div class="row" id="TotalRow" runat="server">
                                <div class="col-md-7 text-left">
                                </div>

                                <div class="col-md-5 ">
                                    <div class="form-group">
                                        <table class="table table-sm table-bordered ">
                                            <tbody>

                                                <tr>
                                                    <td><span style="margin: 7px 5px 5px 5px; padding: 5px" class="m-0 font-weight-bold text-right text-gray-900 ">Grand Total:</span></td>
                                                    <td class="text-right"><span id="Total" class="text-gray-900 font-weight-bold text-gray-900" runat="server"></span></td>
                                                </tr>
                                            </tbody>
                                        </table>





                                    </div>
                                </div>
                            </div>
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
                        </div>

                    </div>
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
        </div>
    </div>
</asp:Content>
