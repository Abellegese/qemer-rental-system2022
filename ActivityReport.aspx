<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="ActivityReport.aspx.cs" Inherits="advtech.Finance.Accounta.ActivityReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Activity Reports</title>
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
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title text-gray-900" id="exampleModalLabel">Search by module</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-7">
                            <asp:DropDownList ID="DropDownList1" class="form-control" runat="server">

                                <asp:ListItem>Payment paid</asp:ListItem>
                                <asp:ListItem>Payment Received</asp:ListItem>
                                <asp:ListItem>Credit Issued</asp:ListItem>

                            </asp:DropDownList>

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
    <div class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
                    <h5 class="modal-title" id="H1">Filter Activity</h5>
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
                                        <asp:TextBox ID="txtDateform" class="form-control " TextMode="DateTimeLocal" runat="server"></asp:TextBox>
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
                                        <asp:TextBox ID="txtDateto" class="form-control " TextMode="DateTimeLocal" runat="server"></asp:TextBox>
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
    <div class="container-fluid pr-3 pl-3">
        <!-- Table -->
        <div class="row">
            <div class="col">
                <div class="bg-white rounded-lg mb-1">
                    <div class="card-header bg-white ">


                        <div class="row">
                            <div class="col-4 text-left ">
                                <a href="Home.aspx" class="btn btn-light btn-circle">
                                    <i class="fas fa-arrow-left text-danger"></i>
                                </a>



                            </div>
                            <div class="col-8 text-right ">
                                <button type="button" runat="server" id="Button1" class="mx-1  btn btn-sm btn-circle" style="background-color: #d46fe8" data-toggle="modal" data-target=".bd-example-modal-lg">
                                    <a class="nav-link" data-toggle="tooltip" data-placement="top" title="Search By Date Range">
                                        <div>
                                            <i class="fas fa-search text-white font-weight-bold"></i>
                                            <span></span>
                                        </div>
                                    </a>
                                </button>
                                <button type="button" runat="server" id="Sp2" class="mx-1 btn btn-sm  btn-circle" style="background-color: #d46fe8" data-toggle="modal" data-target="#exampleModal">
                                    <a class="nav-link" data-toggle="tooltip" data-placement="top" title="Search By Activity Module">
                                        <div>
                                            <i class="fas fa-search-location text-white font-weight-bold"></i>
                                            <span></span>
                                        </div>
                                    </a>
                                </button>
                                <button name="b_print" onclick="printdiv('div_print');" type="button" title="Print" class="mx-1 btn btn-sm  btn-circle" style="background-color: #d46fe8" data-toggle="modal" data-target="#exampleModalCenter">
                                    <div>
                                        <i class="fas fa-print text-white font-weight-bold"></i>

                                    </div>
                                </button>


                            </div>
                        </div>

                    </div>

                    <div id="div_print">


                        <div class="card-header text-black bg-white font-weight-bold">
                            <center>
                                <h5 id="oname" runat="server" class="mb-1 text-uppercase text-gray-900 font-weight-bold "></h5>
                                <h5 class="mb-1 h4 text-uppercase text-gray-600 ">Activity report</h5>
                                <span class="mb-2 text-gray-900 font-weight-bold ">As of<span class="h6 mx-1 mb-2 text-gray-900 font-weight-bold " id="mont" runat="server"></span></span>
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
                                <asp:Repeater ID="Repeater1" runat="server">

                                    <HeaderTemplate>
                                        <div class="table-responsive">
                                            <table class="table align-items-center table-sm ">
                                                <thead>
                                                    <tr>


                                                        <th scope="col">TIME</th>
                                                        <th scope="col" class="text-danger">ACTIVITY DETAIS</th>
                                                        <th scope="col">DESCRIPTION</th>




                                                    </tr>
                                                </thead>
                                                <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <%# Eval("Time")%>
                                            </td>
                                            <td>
                                                <span>
                                                    <a title="Show the details" class="text-primary" href="<%# Eval("Query")%>"><%# Eval("Module")%></a>
                                                </span>
                                                <span>
                                                    <h6>
                                                        <asp:Label ID="Label2" class="small" runat="server" Text='<%# Eval("ModuleDescription")%>'></asp:Label>
                                                    </h6>

                                                </span>
                                                <span>
                                                    <h6>Issue:
                                                        <asp:Label ID="Label3" runat="server" class="text-primary font-weight-bold" Text='<%# Eval("ForPerson")%>'></asp:Label>
                                                    </h6>

                                                </span>

                                            </td>


                                            <td>
                                                <%# Eval("What")%>
                                                <h6 class=" text-xs text-gray-900  font-weight-bold">by <%# Eval("Who")%>
                                                </h6>

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
