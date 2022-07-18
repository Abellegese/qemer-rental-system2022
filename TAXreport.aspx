<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="TAXreport.aspx.cs" Inherits="advtech.Finance.Accounta.TAXreport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>VAT report</title>
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
    <div class="container-fluid pr-3 pl-3">
        <div class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
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
        <!-- Table -->
        <div class="row">
            <div class="col">
                <div class="bg-white rounded-lg  mb-1 ">
                    <div class="card-header bg-white ">


                        <div class="row">
                            <div class="col-4 text-left ">
                                <a href="Home.aspx" class="btn btn-default btn-sm">
                                    <i class="fas fa-arrow-left  font-weight-bold text-primary"></i><span id="account" class="font-weight-bold text-primary mx-3" runat="server"></span>
                                </a>



                            </div>
                            <div class="col-8 text-right ">
                                <button type="button" runat="server" id="Button1" class="mx-1 border-primary border-left border-top border-right border-bottom btn btn-sm btn-default btn-circle" data-toggle="modal" data-target=".bd-example-modal-lg">
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
                        <div class="card-header text-black bg-white font-weight-bold">
                            <center>
                                <h5 id="oname" runat="server" class="mb-1 text-gray-900 font-weight-bold "></h5>
                                <h5 class="mb-1 h4 text-uppercase text-gray-600 ">VAT Report</h5>
                                <span id="datFrom1" runat="server" class="mb-1 text-gray-900 font-weight-bold"></span><span id="tomiddle" class="mb-1 mr-2 ml-2 text-gray-900 font-weight-bold" runat="server">To</span><span id="datTo" class="mb-1 text-gray-900 font-weight-bold" runat="server"></span>
                                <span class="mb-2 text-gray-900 font-weight-bold "><span class="h6 mx-1 mb-2 text-gray-900 font-weight-bold " id="mont" runat="server"></span></span>
                            </center>

                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            <div class="row align-items-center">

                                <div class="col-12 text-right">
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                            </div>
                            <div class="col-8 small">
                                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">

                                    <HeaderTemplate>
                                        <div class="table-responsive">
                                            <table class="table align-items-center table-sm ">
                                                <thead>
                                                    <tr>
                                                        <th scope="col">CUSTOMER</th>
                                                        <th scope="col" class="text-right">DATE</th>
                                                        <th scope="col" class="text-right">FSNO/INV#</th>
                                                        <th scope="col" class="text-danger text-right">VAT AMOUNT(15%)</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="text-gray-900">
                                                <%# Eval("Customer")%>
                                            </td>
                                            <td class="text-right text-gray-900">
                                                <%# Eval("Date","{0:MMMM dd, yyyy}")%>
                                            </td>
                                            <td class="text-right text-gray-900">
                                                <%# Eval("fsno")%>/ <%# Eval("id2")%>
                                            </td>
                                            <asp:Label ID="lblPayment" runat="server" Visible="false" Text='<%# Eval("paid")%>'></asp:Label>
                                            <td class="text-gray-900 text-right">
                                                <asp:Label ID="lblVAT" runat="server"></asp:Label>
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
                        </div>
                    </div>

                </div>

            </div>
        </div>
    </div>
</asp:Content>
