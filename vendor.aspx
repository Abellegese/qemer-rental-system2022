<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.master" AutoEventWireup="true" CodeBehind="vendor.aspx.cs" Inherits="advtech.Finance.Accounta.vendor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Vendor</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <!-- Navbar -->


        <!-- Table -->

        <div class="row">




            <div class="col">
                <div class="card ">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    <div class="card-header  bg-white py-3 d-flex flex-row align-items-center justify-content-between">
                        <h5 class="m-0 font-weight-bold text-primary">Vendor</h5>
                        <div class="row align-items-center">

                            <div class="col-12 text-right">

                                <button runat="server" id="modalMain" type="button" class="btn btn-sm text-xs btn-danger" data-toggle="modal" data-target="#exampleModalLong2">
                                    <div>
                                        <i class="fas fa-plus"></i>
                                        <span>Add vendor</span>
                                    </div>
                                </button>

                            </div>
                        </div>

                    </div>
                    <div class="card-body">
                        <asp:Repeater ID="Repeater1" runat="server">

                            <HeaderTemplate>
                                <div class="table-responsive">
                                    <table class="table align-items-center table-sm small ">
                                        <thead>
                                            <tr>


                                                <th scope="col" class="text-gray-900">Name</th>
                                                <th scope="col" class="text-gray-900">Company Name</th>

                                                <th scope="col" class="text-gray-900">Email</th>
                                                <th scope="col" class="text-gray-900">Work Phone</th>


                                            </tr>
                                        </thead>
                                        <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>


                                    <td class="text-left text-primary">
                                        <a title="Show the details" href="vendorDetails.aspx?ref2=<%#Eval("FllName")%>"><%#Eval("FllName")%></a>


                                    </td>

                                    <td>
                                        <%# Eval("CompanyName")%>
                                    </td>
                                    <td>
                                        <%# Eval("CustomerEmail")%>
                                    </td>
                                    <td>
                                        <%# Eval("WorkPhone")%>
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
                <div class="card-footer bg-white py-4">
                    <nav aria-label="...">
                    </nav>
                </div>




                <div class="modal fade" id="exampleModalLong2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLongTitle">Add the required input</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="form-group">
                                            <label>Custoner Type</label>
                                            <asp:RadioButton ID="RadioButton5" runat="server" Text="Business" ForeColor="#6666FF" Checked="true" GroupName="AT1" />
                                            <asp:RadioButton ID="RadioButton6" runat="server" Text="Individual" ForeColor="#6666FF" GroupName="AT1" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <div class="form-group">
                                            <label class="small text-gray-900">Primary Contact</label>

                                            <asp:TextBox ID="txtCustomerName" runat="server" class="form-control form-control-sm" placeholder="Vendor Name"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-12">
                                        <div class="form-group">
                                            <label class="small text-gray-900">Company Name</label>

                                            <asp:TextBox ID="txtCompanyName" runat="server" class="form-control  form-control-sm" placeholder="Company Name"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <div class="form-group">
                                            <label class="small text-gray-900">Vendor Email</label>

                                            <asp:TextBox ID="txtEmail" runat="server" class="form-control  form-control-sm" placeholder="Email"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-6">
                                        <div class="form-group">
                                            <label class="small text-gray-900">Work Phone</label>

                                            <asp:TextBox ID="txtWorkPhone" runat="server" class="form-control  form-control-sm" placeholder="Work Phone"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-6">
                                        <div class="form-group">
                                            <label class="small text-gray-900">Mobile</label>

                                            <asp:TextBox ID="txtMobile" runat="server" class="form-control  form-control-sm" placeholder="Mobile"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <div class="form-group">
                                            <label class="small text-gray-900">Website</label>

                                            <asp:TextBox ID="txtWebsite" runat="server" class="form-control  form-control-sm" placeholder="Website"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <div class="form-group">
                                            <label class="small text-gray-900">Credit Limit</label>

                                            <asp:TextBox ID="txtCreditLimit" runat="server" class="form-control  form-control-sm"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-6">
                                        <div class="form-group">
                                            <label class="small text-gray-900">Billing Address</label>

                                            <asp:TextBox ID="txtBillingAddress" runat="server" class="form-control  form-control-sm" placeholder="Billing Address"></asp:TextBox>

                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <div class="form-group">
                                            <label class="small text-gray-900">Payment Terms</label>

                                            <asp:TextBox ID="txtTerms" runat="server" class="form-control  form-control-sm" placeholder="Payment Terms"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <div class="form-group">
                                            <label class="small text-gray-900">Contact Person</label>

                                            <asp:TextBox ID="txtContactPerson" runat="server" class="form-control  form-control-sm" placeholder="Contact Person"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 ">
                                        <div class="form-group">
                                            <label class="small text-gray-900">Opening Balance<span class="text-danger">*</span></label>


                                            <br />
                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative">
                                                    <asp:TextBox ID="txtOpeningBalance" class="form-control " runat="server" placeholder="Opening Amount" Style="border-color: #ff0000"></asp:TextBox>
                                                    <div class="input-group-prepend">
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
                                            <label class="font-weight-bold small text-gray-900">Date of Opening Balance<span class="text-danger">*</span></label>


                                            <br />
                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative">
                                                    <asp:TextBox ID="txtDate" class="form-control " runat="server" TextMode="Date"></asp:TextBox>
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text"><i class=" fas fa-calendar"></i></span>
                                                    </div>

                                                </div>
                                            </div>
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
            </div>
        </div>


    </div>
</asp:Content>

