<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="addshop.aspx.cs" Inherits="advtech.Finance.Accounta.addshop" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Manage Shop</title>
    <script type="text/javascript">

        window.addEventListener('load', (event) => {
            var x = document.getElementById("Pbutton");
            x.style.display = "none";
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid  pr-3 pl-3 ">
        <div class="modal fade" id="exampleModalLongService" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle3" aria-hidden="true">
            <div class="modal-dialog modal-sm" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title h6 font-weight-bold text-gray-900" id="exampleModalLongTitle3">Delete Shop</h5>
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
                                        <asp:LinkButton ID="btnDelete" runat="server" class="btn btn-sm btn-danger w-50" OnClick="btndelete_Click"><span class="text-white fas fa-trash mr-2"></span>Delete</asp:LinkButton>

                                    </center>



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
        <div class="row">
            <div class="col">
                <div class="bg-white">
                    <div class="row">


                        <div class="col-4 border-right">

                                <div class="card-header bg-white  font-weight-bold">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    <img src="../../asset/plus-circle-dotted.svg" />
                                    <span class="text-gray-800 mx-2">Add Shops</span>
                                </div>
                                <div class="card-body">

                                    <div class="form-group mb-2">
                                        <label class="small text-gray-900">Shop No.<span class="text-danger">*</span></label>
                                        <div class="input-group input-group-alternative">
                                            <asp:TextBox ID="txtshopno" class="form-control form-control-sm" placeholder="shop number" runat="server"></asp:TextBox>
                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <div class="input-group-prepend ">
                                                        <span class="input-group-text ">SN</span>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="form-group mb-2">
                                        <label class="small text-gray-900">Location<span class="text-danger">*</span></label>
                                        <div class="input-group input-group-alternative">
                                            <asp:TextBox ID="txtlocation" class="form-control  form-control-sm" placeholder="location" runat="server"></asp:TextBox>


                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <div class="input-group-prepend ">
                                                        <span class="input-group-text ">LC</span>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-group mb-2">
                                        <label class="small text-gray-900">Area(m<sup>2</sup>)<span class="text-danger">*</span></label>
                                        <div class="input-group input-group-alternative">
                                            <asp:TextBox ID="txtarea" class="form-control  form-control-sm" placeholder="Area" runat="server"></asp:TextBox>
                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <div class="input-group-prepend ">
                                                        <span class="input-group-text ">AR</span>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="form-group mb-2">
                                        <p class="small text-danger">The price you enter here or when you update the shop price per area are or has to be VAT free... The software will calculate the VAT itself.</p>
                                        <label class="small text-gray-900">Rate(ETB/Sqm*Month)<span class="text-danger">*</span></label>
                                        <div class="input-group input-group-alternative">
                                            <asp:TextBox ID="txtrate" class="form-control  form-control-sm" runat="server" placeholder="rate"></asp:TextBox>
                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <div class="input-group-prepend ">
                                                        <span class="input-group-text ">RT</span>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-group mb-2">
                                        <label class="small text-gray-900">Shop description<span class="text-danger">*</span></label>
                                        <div class="input-group input-group-alternative">
                                            <asp:TextBox ID="txtDescription" class="form-control  form-control-sm" TextMode="MultiLine" Height="100px" runat="server" placeholder="Description"></asp:TextBox>

                                        </div>
                                    </div>

                                    <div class="form-group mb-2">
                                        <label class="small text-gray-900">Status<span class="text-danger">*</span></label>
                                        <div class="input-group input-group-alternative">
                                            <asp:DropDownList ID="ddlShopStatus" runat="server" class="form-control form-control-sm">
                                                <asp:ListItem>Free</asp:ListItem>
                                                <asp:ListItem>Occupied</asp:ListItem>
                                            </asp:DropDownList>



                                        </div>
                                    </div>
                                    <hr>
                                    <asp:Button ID="Button3" class="btn btn-sm btn-danger" runat="server"
                                        Text="Save shop" OnClick="Button3_Click" />
                                </div>
               
                        </div>



                        <div class="col-8 ">
        
                                <!-- Card Header - Dropdown -->
                                <div class="card-header bg-white ">
                                    <div class="row">
                                        <div class="col-xl-5">
                                            <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown1"
                                                role="button" data-toggle="dropdown" aria-haspopup="true"
                                                aria-expanded="false">
                                                <span id="cashdrop" class="small" runat="server">SHOPS</span>
                                            </a>
                                            <div class="dropdown-menu dropdown-menu-left animated--fade-in"
                                                aria-labelledby="navbarDropdown1">

                                                <a class="dropdown-item" href="#">
                                                    <asp:Button ID="btnBindFreeShops" OnClick="btnBindFreeShops_Click" class="btn w-100 btn-sm btn-success" runat="server" Text="Bind Free shops" />

                                                </a>

                                                <a class="dropdown-item" href="#">
                                                    <asp:Button ID="btnbindasuspended" OnClick="btnbindsus_Click" class="btn w-100 btn-sm btn-warning" runat="server" Text="Bind suspended" />

                                                </a>
                                                <a class="dropdown-item" href="#">
                                                    <asp:Button ID="btnbindall" OnClick="btnbindall_Click" class="btn w-100 btn-sm btn-light" runat="server" Text="Bind All" />

                                                </a>
                                            </div>
                                        </div>
                                        <div class="col-xl-7 text-right">
                                            <button runat="server" id="modalMain12" type="button" class="btn btn-circle mx-1  btn-sm text-xs btn-danger" data-toggle="modal" data-target="#exampleModalLongService">
                                                <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Delete Free and Suspended">
                                                    <div>
                                                        <i class="fas fa-trash text-white"></i>

                                                    </div>
                                                </a>
                                            </button>
                                            <button runat="server" id="modalMain" type="button" class="btn btn-circle mx-2  btn-sm text-xs btn-danger" data-toggle="modal" data-target="#exampleModalLongService">
                                                <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Export as Excel">
                                                    <div>
                                                        <i class="fas fa-file-excel text-white"></i>

                                                    </div>
                                                </a>
                                            </button>
                                            <button type="button" runat="server" id="Button1" class=" btn btn-sm btn-danger mr-2 btn-circle" data-toggle="modal" data-target="#exampleModal1">
                                                <div>
                                                    <i class="fas fa-dollar-sign text-white font-weight-bold" data-toggle="tooltip" data-placement="top" title="Update price"></i>
                                                    <span></span>
                                                </div>
                                            </button>
                                            <button type="button" runat="server" id="Sp2" class=" btn btn-sm btn-danger btn-circle" data-toggle="modal" data-target="#exampleModal">
                                                <div>
                                                    <i class="fas fa-search text-white font-weight-bold"></i>
                                                    <span></span>
                                                </div>
                                            </button>
                                        </div>
                                    </div>


                                </div>
                                <!-- Card Body -->
                                <div class="card-body small">


                                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">

                                        <headertemplate>
                                            <div class="table-responsive">
                                                <table class="table table-sm " id="shop">
                                                    <thead>
                                                        <tr>
                                                            <th class="text-center border-right border-left">
                                                                <asp:CheckBox ID="chkHeaders" runat="server" />
                                                            </th>
                                                            <th scope="col">Shop No.</th>

                                                            <th scope="col">Area(m<sup>2</sup>)</th>
                                                            <th scope="col">Rate(ETB/Sqm*Month)</th>
                                                            <th scope="col">Total Rate(ETB/Month)</th>
                                                            <th scope="col">Status</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                        </headertemplate>
                                        <itemtemplate>
                                            <tr>
                                                <th class="text-center  border-right border-left">
                                                    <asp:CheckBox ID="chkRows" runat="server" />
                                                </th>
                                                <td class="text-left text-primary">
                                                    <span class="fas fa-home text-white btn-circle btn-sm mx-2 mr-2 " style="background-color: #ff6a00"></span><a href="shop_details.aspx?ref2=<%#Eval("shopno")%>"><%#Eval("shopno")%></a>
                                                    <asp:Label ID="lblCustomerId" runat="server" Text='<%#Eval("shopno")%>' Visible="false" />
                                                </td>

                                                <td>
                                                    <asp:Label ID="Label2" runat="server" class="text-gray-900" Text='<%# Eval("area","{0:N2}")%>'></asp:Label>
                                                    <asp:TextBox ID="TextBox6" class="form-control" runat="server" Width="100" Text='<%# Eval("area","{0:N2}")%>'
                                                        Visible="false" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" class="text-gray-900" Text='<%# Eval("rate","{0:N2}")%>' />
                                                    <asp:TextBox ID="TextBox5" class="form-control" runat="server" Width="100" Text='<%# Eval("rate","{0:N2}")%>'
                                                        Visible="false" />
                                                </td>
                                                <td class="text-gray-900">
                                                    <%# Convert.ToDouble(Eval("monthlyprice")).ToString("#,##0.00")%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("status")%>'></asp:Label>
                                                </td>


                                            </tr>
                                        </itemtemplate>
                                        <footertemplate>
                                            </tbody>
                          </table>
                                        </footertemplate>
                                    </asp:Repeater>

                                </div>

                                <div class="card-footer bg-white py-4">
                                    <nav aria-label="...">
                                        <ul class="pagination justify-content-end mb-0 ">
                                            <br />
                                            <td>
                                                <asp:label id="Label1" runat="server" class="m-1 text-primary"></asp:label>
                                            </td>
                                            <br />
                                            <li class="page-item active">

                                                <asp:Button ID="btnPrevious" class="btn btn-sm btn-warning btn-circle" runat="server" Text="<" onclick="btnPrevious_Click" />

                                            </li>


                                            <li class="page-item active">

                                                <asp:Button ID="btnNext" class="btn btn-sm btn-warning btn-circle mx-2" runat="server" Text=">" onclick="btnNext_Click" />

                                            </li>

                                        </ul>
                                    </nav>
                                </div>
            
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <div id="con" runat="server" visible="false">
            <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">

                <headertemplate>
                    <div class="table-responsive">
                        <table class="table table-sm " id="shop">
                            <thead>
                                <tr>
                                    <th scope="col">Shop No.</th>

                                    <th scope="col">Area(m<sup>2</sup>)</th>
                                    <th scope="col">Rate(ETB/Sqm*Month)</th>
                                    <th scope="col">Total Rate(ETB/Month)</th>
                                    <th scope="col">Status</th>
                                </tr>
                            </thead>
                            <tbody>
                </headertemplate>
                <itemtemplate>
                    <tr>
                        <td class="text-left text-primary">
                            <a href="shop_details.aspx?ref2=<%#Eval("shopno")%>"><%#Eval("shopno")%></a>
                            <asp:Label ID="lblCustomerId" runat="server" Text='<%#Eval("shopno")%>' Visible="false" />
                        </td>

                        <td>
                            <asp:Label ID="Label2" runat="server" class="text-gray-900" Text='<%# Eval("area","{0:N2}")%>'></asp:Label>
                            <asp:TextBox ID="TextBox6" class="form-control" runat="server" Width="100" Text='<%# Eval("area","{0:N2}")%>'
                                Visible="false" />
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" class="text-gray-900" Text='<%# Eval("rate","{0:N2}")%>' />
                            <asp:TextBox ID="TextBox5" class="form-control" runat="server" Width="100" Text='<%# Eval("rate","{0:N2}")%>'
                                Visible="false" />
                        </td>
                        <td class="text-gray-900">
                            <%# Convert.ToDouble(Eval("monthlyprice")).ToString("#,##0.00")%>
                        </td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("status")%>'></asp:Label>
                        </td>


                    </tr>
                </itemtemplate>
                <footertemplate>
                    </tbody>
                          </table>
                </footertemplate>
            </asp:Repeater>


        </div>

        <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-sm" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title text-gray-900" id="exampleModalLabel">Type Shop No.</h5>
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
        <div class="modal fade" id="exampleModal1" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel1" aria-hidden="true">
            <div class="modal-dialog modal-sm" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title text-gray-900" id="exampleModalLabel1">Update Monthly Rate</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <asp:TextBox ID="txtNewRate" class="form-control form-control-sm" placeholder="Amount per area per month" style="border-color: #ff6a00" runat="server"></asp:TextBox>

                            </div>

                        </div>
                        <div class="row">


                            <div class="col-md-12">
                                <button class="btn btn-danger btn-sm w-100" type="button" disabled id="Pbutton" style="display: none">
                                    <span class="spinner-grow spinner-grow-sm" role="status" aria-hidden="true"></span>
                                    Updating...
                                </button>
                                <asp:Button ID="Button5" runat="server" class="btn btn-sm w-100 btn-danger" Text="Update" OnClick="Button5_Click" OnClientClick="myFunctionshop5()" />

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
        <script>
                                function myFunctionshop5() {
                                    var y = document.getElementById("<%=Button5.ClientID %>"); var x = document.getElementById("Pbutton");
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
</asp:Content>
