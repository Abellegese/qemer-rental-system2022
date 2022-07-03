<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.master" AutoEventWireup="true" CodeBehind="bankstatment.aspx.cs" Inherits="advtech.Finance.Accounta.bankstatment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" rel="stylesheet">
    <title>Bank Statement</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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



    <div class="modal fade" id="exampleModal9v" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel9v" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title font-weight-bolder text-gray-900" id="exampleModalLabel9v">Adjust Bank Account</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row mb-3">
                        <div class="col-md-12">

                            <asp:TextBox ID="txtAmount" style="border-color:#cb32d7" placeholder="Amount" class="form-control form-control-sm" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-12">

                            <asp:TextBox ID="txtRemark" Style="border-color: #cb32d7" TextMode="MultiLine" placeholder="Remark" Height="150px" class="form-control form-control-sm" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <center>
                                <asp:Button ID="Button14" runat="server" class="btn text-white btn-sm w-100" style="background-color:#cb32d7" OnClick="Button14_Click" Text="Adjust" />
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
    <div class="modal fade" id="exampleModal11C" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel11" aria-hidden="true">
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
                            <asp:TextBox ID="txtFilteredAmount" runat="server" class="form-control"></asp:TextBox>


                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-12">
                            <div class="custom-control custom-radio custom-control-inline">

                                <input type="radio" id="Credit" name="amount" class="custom-control-input" checked="true" runat="server" clientidmode="Static" />
                                <label class="custom-control-label text-gray-900  " for="Credit">IN</label>
                            </div>
                            <div class="custom-control custom-radio custom-control-inline">
                                <input type="radio" id="Debit" name="amount" class="custom-control-input" runat="server" clientidmode="Static" />
                                <label class="custom-control-label font-weight-200  text-gray-900 " for="Debit">OUT</label>
                            </div>
                            <div class="custom-control custom-radio custom-control-inline">
                                <input type="radio" id="Balance" name="amount" class="custom-control-input" runat="server" clientidmode="Static" />
                                <label class="custom-control-label font-weight-200  text-gray-900 " for="Balance">Balance</label>
                            </div>

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
    <div class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
                    <h5 class="modal-title" id="H1">Filter Transaction</h5>
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
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <asp:Button ID="btnUpdate" class="btn btn-danger btn-sm" OnClick="btnUpdate_Click" runat="server"
                            Text="Search..." />
                </div>
                </center>
            </div>
        </div>
    </div>
    <div class="container-fluid pr-3 pl-3">

        <div class="bg-white rounded-lg  mb-2">
            <div class="card-header bg-white ">
                <div class="row">
                    <div class="col-6 text-left">
                        <a class="btn btn-light btn-circle" id="buttonback" runat="server" href="BankStatement.aspx" data-toggle="tooltip" data-placement="bottom" title="Back to Manage Bank">

                            <span class="fa fa-arrow-left text-gray-900"></span>

                        </a>
                        <span class="badge badge-danger" id="Badge" runat="server"></span>
                        <button class="btn btn-circle btn-sm ml-2 mr-2 " style="background-color:#cb32d7" type="button" data-toggle="modal" data-target="#exampleModal9v">

                            <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Adjust Account">
                                <div>
                                    <i class="fas fa-adjust text-white"></i>

                                </div>
                            </a>

                        </button>
                        <button class="btn btn-light btn-sm btn-circle   border-bottom border-right border-top border-danger " type="button" data-toggle="modal" data-target="#exampleModal11C">

                            <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Search by condition">
                                <div>
                                    <i class="fas fa-greater-than-equal text-danger"></i>

                                </div>
                            </a>

                        </button>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                    <div class="col-6 text-right">

                        <button runat="server" id="modalMain" type="button" class="btn btn-circle mx-1  mr-2  btn-sm text-xs btn-danger" data-toggle="modal" data-target="#exampleModalLongService">
                            <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Export as Excel">
                                <div>
                                    <i class="fas fa-file-excel text-white"></i>

                                </div>
                            </a>
                        </button>

                        <button name="b_print" onclick="printdiv('div_print');" type="button" title="Print" class=" btn  mr-2 btn-sm btn-circle" style="background-color: #30569a" data-toggle="modal" data-target="#exampleModalCenter">
                            <div>
                                <i class="fas fa-print text-white"></i>

                            </div>
                        </button>
                        <button type="button" runat="server" id="Button1" class="mx-1 btn btn-sm btn-success  mr-2 btn-circle" data-toggle="modal" data-target=".bd-example-modal-lg">
                            <div>
                                <i class="fas fa-search text-white font-weight-bold"></i>
                                <span></span>
                            </div>
                        </button>

                    </div>
                </div>

            </div>

            <div class="row">

                <div class="col-2">
                </div>
                <div class="col-8">

                    <div id="div_print">

                        <div class="card-header mb-5 text-black bg-white font-weight-bold">
                            <div class="row" style="height: 90px">
                                <div class="col-md-6 text-left">
                                    <img class="" src="../../asset/Brand/gh.jpg" alt="" width="110" height="80">
                                    <h5 id="company1" runat="server" class="mb-1 text-gray-900 text-uppercase font-weight-bold " style="font-family: calibri"></h5>
                                    <span class="fas fa-address-book text-gray-400 mr-2"></span><span class="h6 small  text-gray-500 mt-1" id="CompAddress" runat="server"></span>
                                    <br />
                                    <span class="fas fa-phone text-gray-400 mr-2"></span><span class="h6 small text-gray-500 mt-1" id="Contact" runat="server"></span>

                                </div>

                                <div class="col-md-6 text-right">
                                    <h4 class="m-0  font-weight-bold text-uppercase  text-gray-900  mb-2">Bank Statement</h4>
                                    <h6><span class="h6 text-uppercase text-gray-900" id="H1" runat="server"></span></h6>
                                    <h6 class="h6 text-uppercase text-gray-600" visible="false" id="Span2" runat="server"></h6>
                                    <span id="datFrom1" runat="server" class="mb-1 text-gray-900 font-weight-bold"></span><span id="tomiddle" class="mb-1 mr-2 ml-2 text-gray-900 font-weight-bold" runat="server">To</span><span id="datTo" class="mb-1 text-gray-900 font-weight-bold" runat="server"></span>
                                    <h6 class="mb-2 text-gray-900 font-weight-bold "><span class="h6 mx-1 mb-2 text-gray-900 font-weight-bold " id="mont" runat="server"></span></h6>
                                </div>
                            </div>


                        </div>
                        <div id="con" runat="server">
                            <div class="card-body mt-5 small">

                                <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">


                                    <HeaderTemplate>

                                        <div class="table-responsive">
                                            <table class="table align-items-center table-sm">

                                                <thead class=" thead-white">
                                                    <tr>


                                                        <th scope="col" class="text-left ">Voucher#</th>
                                                        <th scope="col" class="text-left">Cheque#</th>
                                                        <th scope="col" class="text-left">In</th>
                                                        <th scope="col" class="text-left">Out</th>
                                                        <th scope="col" class="text-right">balance</th>
                                                        <th scope="col" class="text-center">Remark</th>
                                                        <th scope="col" class="text-right text-danger ">
                                                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="date">Date</asp:LinkButton></th>





                                                    </tr>
                                                </thead>

                                                <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>


                                            <td class="text-left text-gray-900">
                                                <%# Eval("voucher")%>
                    
                                            </td>
                                            <td class="text-left text-gray-900">
                                                <%# Eval("cheque")%>
                                            </td>
                                            <td class="text-left text-gray-900">
                                                <%# Eval("cashin", "{0:N2}")%>
                                            </td>
                                            <td class="text-left text-gray-900">
                                                <%# Eval("cashout", "{0:N2}")%>
                                            </td>
                                            <td class="text-right text-gray-900">
                                                <%# Eval("balance", "{0:N2}")%>
                                            </td>
                                            <td class="text-center text-gray-900">
                                                <%# Eval("remark")%>
                                            </td>
                                            <td class="text-right text-gray-900">
                                                <%# Eval("date", "{0: dd/MM/yyyy}")%>
                                            </td>


                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
              </table>
              <hr class="text-gray-700 font-weight-bold" />
                                    </FooterTemplate>

                                </asp:Repeater>
                                <div id="con2" runat="server" visible="false">
                                    <asp:Repeater ID="Repeater2" runat="server">


                                        <HeaderTemplate>

                                            <div class="table-responsive">
                                                <table class="table align-items-center table-sm">

                                                    <thead class=" thead-white">
                                                        <tr>


                                                            <th scope="col" class="text-left ">Voucher#</th>
                                                            <th scope="col" class="text-left">Cheque#</th>
                                                            <th scope="col" class="text-left">In</th>
                                                            <th scope="col" class="text-left">Out</th>
                                                            <th scope="col" class="text-right">balance</th>
                                                            <th scope="col" class="text-center">Remark</th>
                                                            <th scope="col" class="text-right text-danger ">
                                                              Date</th>





                                                        </tr>
                                                    </thead>

                                                    <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>


                                                <td class="text-left text-gray-900">
                                                    <%# Eval("voucher")%>
                    
                                                </td>
                                                <td class="text-left text-gray-900">
                                                    <%# Eval("cheque")%>
                                                </td>
                                                <td class="text-left text-gray-900">
                                                    <%# Eval("cashin", "{0:N2}")%>
                                                </td>
                                                <td class="text-left text-gray-900">
                                                    <%# Eval("cashout", "{0:N2}")%>
                                                </td>
                                                <td class="text-right text-gray-900">
                                                    <%# Eval("balance", "{0:N2}")%>
                                                </td>
                                                <td class="text-center text-gray-900">
                                                    <%# Eval("remark")%>
                                                </td>
                                                <td class="text-right text-gray-900">
                                                    <%# Eval("date", "{0: dd/MM/yyyy}")%>
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
                                

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-8 text-left" visible="false">
                            </div>


                            <div class="col-md-4 ">
                                <div class="form-group">

                                    <h6 class="m-0 font-weight-bold text-right text-danger" id="BalanceDiv" runat="server">Balance: <span id="sPan1" class="text-gray-900 font-weight-bold" runat="server"></span></h6>

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
        <div class="col-2">
        </div>

    </div>
</asp:Content>
