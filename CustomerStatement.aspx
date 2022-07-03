<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.master" AutoEventWireup="true" CodeBehind="CustomerStatement.aspx.cs" Inherits="advtech.Finance.Accounta.CustomerStatement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    <title>Customer statement</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid pl-3 pr-3">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <!-- Navbar -->

        <!-- End Navbar -->
        <!-- Header -->


        <!-- Table -->
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



                            <div class="col-6 ">
                                <div class="form-group">
                                    <label class="font-weight-bold">From<span class="text-danger">*</span></label>


                                    <br />
                                    <div class="form-group mb-0">
                                        <div class="input-group input-group-alternative">
                                            <asp:TextBox ID="txtDatefrom" class="form-control " TextMode="Date" runat="server"></asp:TextBox>
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
                                            <asp:TextBox ID="txtDateTo" class="form-control " TextMode="Date" runat="server"></asp:TextBox>
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
                            <asp:Button ID="btnUpdate" class="btn btn-primary" runat="server" OnClick="Save"
                                Text="Search..." />
                    </div>
                    </center>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <div class=" bg-white rounded-lg mb-2 ">
                    <div class="card-header bg-white ">
                        <div class="row">
                            <div class="col-4 text-left">
                                <a class="btn btn-light btn-circle mr-2" id="buttonback" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back to customer details">

                                    <span class="fa fa-arrow-left text-danger"></span>

                                </a>
                                <span class="badge text-white" id="CustomerID" runat="server" style="background-color: #8d4ac7"></span>
                            </div>
                            <div class="col-8 text-right">

                                <button type="button" runat="server" id="Sp2" class="mr-2 btn btn-sm btn-circle" style="background-color: #8d4ac7" data-toggle="modal" data-target=".bd-example-modal-lg">
                                    <div>
                                        <i class="fas fa-search text-white font-weight-bold"></i>
                                        <span></span>
                                    </div>
                                </button>



                                <button name="b_print" onclick="printdiv('div_print');" type="button" title="Print" class=" btn btn-sm btn btn-circle" style="background-color:#8d4ac7" data-toggle="modal" data-target="#exampleModalCenter">
                                    <div>
                                        <i class="fas fa-print text-white font-weight-bold"></i>

                                    </div>
                                </button>


                            </div>
                        </div>

                    </div>

                    <div id="div_print">

                        <div class="row mt-5">
                            <div class="col-1">
                            </div>
                            <div class="col-10">

                                <div class="card-header text-black bg-white font-weight-bold">
                                    <center>
                                    </center>
                                    <center>
                                        <div class="row border-bottom">
                                            <div class="col-md-4 text-left">
                                                <img class="" src="../../asset/Brand/gh.jpg" alt="" width="110">
                                            </div>
                                            <div class="col-md-8 text-right">


                                                <h4 class="text-gray-900 font-weight-bold">CUSTOMER STATEMENT</h4>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-7 text-left">
                                                <h5 class="m-0 font-weight-bold text-left border-bottom text-gray-900 text-uppercase " id="campName" runat="server"></h5>
                                                <span class="fas fa-phone text-gray-500 text-left mr-1"></span><span class="h6 small text-left font-weight-bold text-gray-900 mt-1" id="Contact" runat="server"></span>
                                                <br />
                                                <span class="fas fa-address-book text-left text-gray-500 mr-1"></span><span class="h6 text-left small font-weight-bold text-gray-900 mt-1" id="CompAddress" runat="server"></span>
                                                <h6 id="H2" class="m-0 font-weight-bold text-left mt-5 " runat="server">To: <span id="Sp" class="text-gray-900" runat="server"></span></h6>

                                            </div>
                                            <div class="col-md-5 ">


                                                <h6 class="text-right border-bottom border-top"><span id="asof" runat="server">As of <span id="dateasof" runat="server"></span></span><span id="Date1" visible="false" class="font-weight-bold" runat="server"></span><span class="font-weight-light" id="middleto" runat="server" visible="false">To</span> <span class="font-weight-bold" id="Span1" visible="false" runat="server"></span></h6>

                                                <h5 class="m-0 font-weight-bold text-gray-900 text-left" style="margin: 7px 5px 5px 5px; padding: 5px; background-color: #CCCCCC;">Account Summary<span id="Span5" class="text-gray-900 " runat="server"></span></h5>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <h6 class="m-0 font-weight-bold text-left" style="margin: 7px 5px 5px 5px; padding: 5px">Opening Balance   </h6>
                                                    </div>
                                                    <div class="col-md-6 text-right">
                                                        <span id="Ship" class="text-gray-900" runat="server"></span>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <h6 class="m-0 font-weight-bold text-left" style="margin: 7px 5px 5px 5px; padding: 5px">Invoiced Amount</h6>
                                                    </div>
                                                    <div class="col-md-6 text-right">
                                                        <span id="Ship2" class="text-gray-900" runat="server"></span>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <h6 class="m-0 font-weight-bold text-left" style="margin: 7px 5px 5px 5px; padding: 5px">Amount Piad</h6>
                                                    </div>
                                                    <div class="col-md-6 text-right">
                                                        <span id="Ship3" class="text-gray-900" runat="server"></span>
                                                    </div>
                                                </div>

                                                <hr class="border-black border-bottom border-top" />
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <h6 class="m-0 font-weight-bold text-left" style="margin: 7px 5px 5px 5px; padding: 5px">Balance Due</h6>
                                                    </div>
                                                    <div class="col-md-6 text-right">
                                                        <span id="Span6" class="text-gray-900" runat="server"></span>
                                                    </div>
                                                </div>


                                            </div>

                                        </div>

                                    </center>


                                </div>

                                <div class="card-body " style="border-top-width: 0px">

                                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">


                                        <HeaderTemplate>

                                            <div class="table-responsive">
                                                <table class="table align-items-center table-sm  ">

                                                    <thead class=" thead-dark">
                                                        <tr>


                                                            <th scope="col" class="text-left ">Date</th>
                                                            <th scope="col" class="text-left">Remark</th>
                                                            <th scope="col" class="text-left">Amount</th>
                                                            <th scope="col" class="text-left">Payment</th>
                                                            <th scope="col" class="text-right">Balance</th>





                                                        </tr>
                                                    </thead>

                                                    <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>


                                                <td class="text-left text-gray-900">
                                                    <%# Eval("date", "{0: dd/MM/yyyy}")%>
                    
                                                </td>
                                                <td class="text-left text-gray-900">
                                                    <%# Eval("Trans")%>
                                                </td>

                                                <td class="text-left text-gray-900">
                                                    <%# Eval("InvAmount", "{0:N2}")%>
                                                </td>
                                                <td class="text-left text-gray-900">
                                                    <%# Eval("Payment", "{0:N2}")%>
                                                </td>
                                                <td class="text-right text-gray-900">
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Balance", "{0:N2}")%>'></asp:Label>
                                                </td>


                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
              </table>
              <hr class="text-gray-700 font-weight-bold" />
                                        </FooterTemplate>

                                    </asp:Repeater>

                                </div>

                                <div class="card-footer bg-white">

                                    <nav aria-label="...">
                                        <ul class="pagination justify-content-end mb-0">
                                            <br />
                                            <td>
                                                <asp:Label ID="Label1" runat="server" class="m-1 small text-primary"></asp:Label></td>
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

                                <div class="row">
                                    <div class="col-md-11 text-left">
                                        <span class="small text-danger">If you have any disaggrement with the statment contact the admin.</span>

                                    </div>
                                    <div class="col-md-1 text-right">
                                    </div>

                                </div>

                            </div>
                            <div class="col-1">
                            </div>
                        </div>


                    </div>



                </div>

            </div>
        </div>
    </div>
</asp:Content>

