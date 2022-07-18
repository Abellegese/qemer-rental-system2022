<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.master" AutoEventWireup="true" CodeBehind="orgprofileview.aspx.cs" Inherits="advtech.Finance.Accounta.orgprofileview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Company Profile</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid pl-3 pr-3">

        <div class="row">
            <div class="col">

                <div class="row">
                    <div class="col-lg-4">
                    </div>
                    <div class="col-lg-4">

                        <!-- Default Card Example -->
                        <div class="bg-white p-5 rounded-lg shadow">
                            <div class="card-header bg-white  font-weight-bold">
                                <div class="row">
                                    <div class="col-md-6 text-left">
                                        <span class=" text-xs font-weight-bold">Company profile</span>

                                    </div>
                                    <div class="col-md-6 text-right">
                                        <div class="dropdown no-arrow" id="Button11" runat="server">
                                            <button class="btn btn-light btn-circle dropdown-toggle" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">

                                                <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Options">
                                                    <div>
                                                        <i class="fas fa-caret-down text-danger"></i>

                                                    </div>
                                                </a>

                                            </button>
                                            <div class="dropdown-menu  dropdown-menu-right shadow animated--fade-in" aria-labelledby="dropdownMenuLink">
                                                <div class="dropdown-header">Option:</div>

                                                <a class="dropdown-item" href="#" id="A2" data-toggle="modal" data-target="#exampleModalT" runat="server">Update info</a>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="card-body">
                                <center>

                                    <img src="../../asset/Brand/gh.jpg" alt="" height="100" width="100">
                                    <br />
                                    <div style="padding: 20px 0px 0px 0px">
                                        <span id="company" runat="server" class="badge text-uppercase badge-danger text-lg font-weight-bold text-white"></span>
                                    </div>

                                </center>
                                <br />
                                <br />
                                <div>
                                    <span class="font-weight-bold text-gray-900 text-uppercase">basic info</span>
                                    <hr />

                                    <div class=" mr-2">
                                        <span data-toggle="tooltip" data-placement="top" title="Address"><i class="fas fa-address-card text-white btn-circle btn-sm mr-2  " style="background-color: #ff6a00"></i></span><span id="address" class="text-gray-900 small" runat="server" title="Biling Address"></span>
                                    </div>

                                    <div class=" mr-2" style="padding: 10px 0px 0px 0px">

                                        <span data-toggle="tooltip" data-placement="top" title="City"><i class="fas fa-city text-white btn-circle btn-sm mr-2  " style="background-color: #ff6a00"></i></span><span id="city" runat="server" class="text-gray-900 small" title="Shipping Address"></span>
                                    </div>
                                    <div class=" mr-2" style="padding: 10px 0px 0px 0px">
                                        <span data-toggle="tooltip" data-placement="top" title="Email"><i class="fas fas fa-envelope text-white btn-circle btn-sm mr-2  " style="background-color: #ff6a00"></i></span><span id="email" class="text-gray-900 small" runat="server" title="Email Address bg-white"></span>
                                    </div>
                                    <div class=" mr-2" style="padding: 10px 0px 0px 0px">
                                        <span data-toggle="tooltip" data-placement="top" title="Mobile"><i class="fas fa-mobile text-white btn-circle btn-sm mr-2  " style="background-color: #ff6a00"></i></span><span id="mobile" class="text-gray-900 small" runat="server" title="Mobile Number"></span>
                                    </div>
                                    <div class=" mr-2" style="padding: 10px 0px 0px 0px">
                                        <span data-toggle="tooltip" data-placement="top" title="Vat Reg Number"><i class="fas fa-hashtag text-white btn-circle btn-sm mr-2  " style="background-color: #ff6a00"></i></span><span id="VatRegNumber" class="text-gray-900 small" runat="server" title="Vat Reg Number"></span>
                                    </div>

                                </div>
                            </div>
                        </div>



                    </div>
                    <div class="col-lg-4">
                    </div>


                </div>

            </div>
        </div>
    </div>
    <div class="modal fade" id="exampleModalT" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabelT" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title text-gray-900 small text-uppercase" id="exampleModalLabelT"><span class="fas fa-city mr-2" style="color: #d46fe8"></span>Bind Company Info</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row mb-3">
                        <div class="col-md-12 ">
                            <asp:TextBox ID="txtMobile" data-toggle="tooltip" title="Update Mobile" class="form-control form-control-sm" placeholder="Contact" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-md-12 ">
                            <asp:TextBox ID="txtEmail" class="form-control  form-control-sm" data-toggle="tooltip" title="Update Email" placeholder="Email" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-12 ">
                            <asp:TextBox ID="txtTIN" class="form-control  form-control-sm" data-toggle="tooltip" title="Update TIN" placeholder="TIN#" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-12 ">
                            <asp:TextBox ID="txtVatRegNumber" class="form-control  form-control-sm" data-toggle="tooltip" title="Update Vat Reg Number" placeholder="Vat Reg Number" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-12 ">
                            <asp:TextBox ID="txtAddress" class="form-control  form-control-sm" data-toggle="tooltip" title="Update Address" placeholder="Company Address" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Button ID="Button18" runat="server" class="btn btn-sm text-white w-100" Style="background-color: #d46fe8" Text="Save" OnClick="Button18_Click" />

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
</asp:Content>
