<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.master" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="advtech.Finance.Accounta.CustomerUtil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Customer</title>
    <link href="modals.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <!-- Navbar -->
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
                            <asp:TextBox ID="txtCustomerName1" class="form-control mx-2" runat="server"></asp:TextBox>

                        </div>

                        <div class="col-md-5">
                            <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="search.." OnClick="Button2_Click1" />

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
    <div class="container-fluid pl-3 pr-3">
        <!-- Table -->
        <div class="row">
            <div class="col">

                <div class="bg-white rounded-lg ">
                    <div class="row ">

                        <div class="col-xl-4 col-md-6 ">
                            <div class="card-header-pills">
                                <div class="text-center text-xs mt-2 font-weight-bolder text-primary text-uppercase">Total Active Customer</div>

                            </div>


                            <div class="card-body ">
                                <div class="row no-gutters align-items-center">
                                    <div class="col mr-2">
                                        <div class="h6 font-weight-bold text-gray-700 text-center text-uppercase"><span id="Span1" class="mx-2 fas fa-user-check fa-2x text-warning" runat="server"></span></div>
                                        <h5 class="mt-2 text-gray-900 font-weight-light text-center" id="H2" runat="server"></h5>


                                    </div>

                                    <div class="col-auto">
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="col-xl-4 col-md-6 ">
                            <div class="card-header-pills">
                                <div class="text-center text-xs mt-2 font-weight-bolder text-primary text-uppercase"># of Free Shops</div>

                            </div>


                            <div class="card-body ">
                                <div class="row no-gutters align-items-center">
                                    <div class="col mr-2">
                                        <div class="h6 font-weight-bold text-gray-700 text-center text-uppercase"><span id="Span3" class="mx-2 fas fa-house-user fa-2x text-danger" runat="server"></span></div>
                                        <h5 class="mt-2 text-gray-900 font-weight-light text-center" id="H3" runat="server"></h5>


                                    </div>

                                    <div class="col-auto">
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="col-xl-4 col-md-6 ">
                            <div class="card-header-pills">
                                <div class="text-center text-xs mt-2 font-weight-bolder text-primary text-uppercase">Total Receivable</div>

                            </div>


                            <div class="card-body ">
                                <div class="row no-gutters align-items-center">
                                    <div class="col mr-2">
                                        <div class="h6 font-weight-bold text-gray-700 text-center text-uppercase"><span id="Span4" class="mx-2 fas fa-donate fa-2x text-success" runat="server"></span></div>
                                        <h5 class="mt-2 text-gray-900 font-weight-light text-center" id="H4" runat="server"></h5>


                                    </div>

                                    <div class="col-auto">
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    <div class="card-header  bg-white py-3 d-flex flex-row align-items-center justify-content-between border-top">
                        <h5 class="m-0 font-weight-light text-gray-500 text-uppercase">Customers</h5>
                        <div class="row align-items-center">

                            <div class="col-12 text-right">
                                <button runat="server" id="Button3" type="button" class="btn btn-circle mr-2 btn-sm text-xs btn-warning" data-toggle="modal" data-target="#exampleModalLongService">
                                    <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Update Service Charge">
                                        <div>
                                            <i class="fas fa-donate text-white"></i>

                                        </div>
                                    </a>
                                </button>
                                <button runat="server" id="modalMain" type="button" class="btn btn-circle mr-2 btn-sm text-xs btn-danger" data-toggle="modal" data-target="#exampleModalLong2">
                                    <div>
                                        <i class="fas fa-plus"></i>

                                    </div>
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
                    <div class="card-body small">


                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">

                            <HeaderTemplate>
                                <div class="table-responsive">
                                    <table class="table align-items-center table-sm ">
                                        <thead>
                                            <tr>


                                                <th scope="col">Name</th>
                                                <th scope="col">Company Name</th>

                                                <th scope="col">Email</th>
                                                <th scope="col">Work Phone</th>
                                                <th scope="col">Shop No.</th>
                                                <th scope="col">Status</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>


                                    <td class="text-left text-primary">                    
                                        <a title="Show the details" href="CustomerDetails.aspx?ref2=<%#Eval("FllName")%>""><%#Eval("FllName")%></a></td>

                                    <td>
                                        <%# Eval("CompanyName")%>
                                    </td>
                                    <td>
                                        <%# Eval("CustomerEmail")%>
                                    </td>
                                    <td>
                                        <%# Eval("WorkPhone")%>
                                    </td>
                                    <td>
                                        <%# Eval("shop")%>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("Status")%>'></asp:Label></td>





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
            </div>
        </div>
    </div>
    <div class="modal fade" id="exampleModalLongService" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle3" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title h5 font-weight-bold text-gray-900" id="exampleModalLongTitle3">New Service Charge</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">
                                <label>Service Charge</label>

                                <asp:TextBox ID="txtServiceChargeUpdate" runat="server" class="form-control " placeholder="Service Charge"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">


                                <div class="custom-control custom-checkbox font-weight-300">
                                    <input type="checkbox" class="custom-control-input" id="Checkbox12" runat="server" clientidmode="Static" />
                                    <label class="custom-control-label text-gray-900 " for="Checkbox12">Include SMS Notification</label>
                                </div>

                            </div>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <center>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>

                            <asp:Button ID="btnSrviceChargeUpdate" class="btn btn-danger" runat="server" Text="Save" OnClick="btnSrviceChargeUpdate_Click" />
                        </center>
                    </div>

                </div>

            </div>
        </div>
    </div>
    <!-- Dark table -->
    <div class="modal fade" id="exampleModalLong2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title h5 font-weight-bold text-gray-900" id="exampleModalLongTitle">New Customer</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">
                                <label class="text-gray-900 small">Buissness Type</label>

                                <asp:TextBox ID="txtBuisnesstype" runat="server" class="form-control form-control-sm" placeholder="Service,Manufacturing..." value="-"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">
                                <label class="text-gray-900 small">Customer Name <span class="text-danger">{english}</span></label>

                                <asp:TextBox ID="txtCustomerName" runat="server" class="form-control  form-control-sm" placeholder="Customer Name"></asp:TextBox>

                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">
                                <label class="text-gray-900 small">የደንበኛው ስም በአማርኛ  <span class="text-danger mr-2">{ለደብዳቤ አገልግሎት}</span></label>

                                <asp:TextBox ID="txtCustomerNameAmharic" runat="server" class="form-control  form-control-sm" placeholder="የደንበኛው ስም {አማርኛ}"></asp:TextBox>

                            </div>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">
                                <label class="text-gray-900 small">Company Name</label>

                                <asp:TextBox ID="txtCompanyName" runat="server" class="form-control form-control-sm " placeholder="Company Name" value="-"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">
                                <label class="text-gray-900 small">Address</label>

                                <asp:TextBox ID="txtAddress" runat="server" class="form-control form-control-sm" placeholder="Address"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">
                                <label class="text-gray-900 small">TIN Number</label>

                                <asp:TextBox ID="txtTIN" runat="server" class="form-control form-control-sm" placeholder="TIN Number" Style="border-color: #ff0000"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">
                                <label class="text-gray-900 small">VAT Reg. Number</label>

                                <asp:TextBox ID="txtVatRegNumber" runat="server" class="form-control form-control-sm" placeholder="VAT Reg. Number" Style="border-color: #ff0000"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">
                                <label class="text-gray-900 small">Email</label>

                                <asp:TextBox ID="txtEmail" runat="server" class="form-control  form-control-sm" placeholder="Email" value="-"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-6">
                            <div class="form-group">
                                <label class="text-gray-900 small">Work Phone</label>

                                <asp:TextBox ID="txtWorkPhone" runat="server" class="form-control  form-control-sm" placeholder="Work Phone" value="-"></asp:TextBox>

                            </div>
                        </div>

                        <div class="col-6">
                            <div class="form-group">
                                <label class="text-gray-900 small">Mobile</label>

                                <asp:TextBox ID="txtMobile" runat="server" class="form-control  form-control-sm" placeholder="Mobile" value="-"></asp:TextBox>

                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">
                                <label class="text-gray-900 small">Website</label>

                                <asp:TextBox ID="txtWebsite" runat="server" class="form-control  form-control-sm" placeholder="Website" value="-"></asp:TextBox>

                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">
                                <label class="text-gray-900 small">Credit Limit</label>

                                <asp:TextBox ID="txtCreditLimit" runat="server" class="form-control form-control-sm " value="4000"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">
                                <label class="text-gray-900 small">Contigency</label>

                                <asp:TextBox ID="txtContigency" runat="server" class="form-control  form-control-sm" value="0"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">
                                <label class="text-gray-900 small">Service Charge</label>

                                <asp:TextBox ID="txtServiceCharge" runat="server" class="form-control  form-control-sm" value="500"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                    <hr />
                    <h6 class="text-gray-900 font-weight-bold">Guarantor Information</h6>
                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">
                                <label class="text-gray-900 small">Guarantor Full Name</label>

                                <asp:TextBox ID="txtGurentorName" runat="server" class="form-control form-control-sm "></asp:TextBox>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">
                                <label class="text-gray-900 small">Residential/Work Address</label>

                                <asp:TextBox ID="txtGAddress" runat="server" class="form-control form-control-sm"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">
                                <label class="text-gray-900 small">Contact</label>

                                <asp:TextBox ID="txtContactGurentor" runat="server" class="form-control  form-control-sm"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">
                                <label class="text-gray-900 small">ID Card Image Front</label>

                                <asp:FileUpload ID="FileUpload1" class="form-control form-control-sm" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">
                                <label class="text-gray-900 small">ID Card Image Back</label>

                                <asp:FileUpload ID="FileUpload2" class="form-control form-control-sm" runat="server" />

                            </div>
                        </div>
                    </div>

                    <div class="row border-top">
                        <div class="col-12">
                            <div class="form-group">
                                <label class="text-gray-900 small">Guarantor ID Only Front(If any)</label>

                                <asp:FileUpload ID="FileUpload3" class="form-control form-control-sm" runat="server" />

                            </div>
                        </div>
                    </div>
                    <hr />
                    <div class="form-group mb-2">
                        <label class="font-weight-bold">Shop held by the cust.<span class="text-danger">*</span></label>
                        <div class="input-group input-group-alternative">
                            <asp:DropDownList ID="ddlshop" class="form-control  form-control-sm" runat="server">
                            </asp:DropDownList>


                        </div>
                    </div>
                    <div class="form-group mb-2">
                        <label class="font-weight-bold">Payment Terms<span class="text-danger">*</span></label>
                        <div class="input-group input-group-alternative">
                            <asp:DropDownList ID="ddlterms" class="form-control  form-control-sm" runat="server">
                                <asp:ListItem>Every Three Month</asp:ListItem>
                                <asp:ListItem>Every Six Month</asp:ListItem>
                                <asp:ListItem>Monthly</asp:ListItem>
                                <asp:ListItem>Yearly</asp:ListItem>
                            </asp:DropDownList>


                        </div>
                    </div>

                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">
                                <label>Contact Person</label>

                                <asp:TextBox ID="txtContactPerson" runat="server" class="form-control form-control-sm " placeholder="Contact Person" value="Ayisha Awol"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 ">
                            <div class="form-group">
                                <label class="font-weight-bold">Opening Balance<span class="text-danger">*</span></label>
                                <br />
                                <div class="form-group mb-0">
                                    <div class="input-group input-group-alternative">
                                        <asp:TextBox ID="txtOpeningBalance" class="form-control  form-control-sm" value="0" runat="server"></asp:TextBox>
                                        <div class="input-group-prepend form-control-sm">
                                            <span class="input-group-text">ETB</span>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 ">
                            <div class="form-group">
                                <label class="font-weight-bold">The Coming Due Date<span class="text-danger">*</span></label>


                                <br />
                                <div class="form-group mb-0">
                                    <div class="input-group input-group-alternative">
                                        <asp:TextBox ID="txtDueDate" class="form-control  form-control-sm" TextMode="Date" runat="server"></asp:TextBox>
                                        <div class="input-group-prepend">
                                            <span class="input-group-text"><i class=" fas fa-calendar"></i></span>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 ">
                            <div class="form-group">
                                <label class="font-weight-bold">The Coming Agreement Renewal Date<span class="text-danger">*</span></label>


                                <br />
                                <div class="form-group mb-0">
                                    <div class="input-group input-group-alternative">
                                        <asp:TextBox ID="txtAgreement" class="form-control  form-control-sm" TextMode="Date" runat="server"></asp:TextBox>
                                        <div class="input-group-prepend">
                                            <span class="input-group-text"><i class=" fas fa-calendar"></i></span>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 ">
                            <div class="form-group">
                                <label class="font-weight-bold">Date of Joining<span class="text-danger">*</span></label>


                                <br />
                                <div class="form-group mb-0">
                                    <div class="input-group input-group-alternative">
                                        <asp:TextBox ID="txtDatejoinig" class="form-control  form-control-sm" TextMode="Date" runat="server"></asp:TextBox>
                                        <div class="input-group-prepend">
                                            <span class="input-group-text"><i class=" fas fa-calendar"></i></span>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                <input type="checkbox" class="custom-control-input" id="Checkbox2" runat="server" clientidmode="Static" />
                                <label class="custom-control-label small text-primary " for="Checkbox2">Allow Portal Access</label>
                            </div>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <center>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>

                            <asp:Button ID="Button1" class="btn btn-danger" runat="server" Text="Save" OnClick="save" />
                        </center>
                    </div>

                </div>

            </div>
        </div>
    </div>
</asp:Content>

