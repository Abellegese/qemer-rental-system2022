<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="advtech.Finance.Accounta.Account" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Charts of account</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <!-- Navbar -->
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title text-gray-900" id="exampleModalLabel">Import Excel File</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">

                    <center>
                        <asp:FileUpload ID="FileUpload1" class="form-control mb-3" runat="server" />
                        <asp:Button ID="Button1" runat="server" class="btn btn-warning btn-sm " Text="Import Excel" OnClick="Button1_Click" />


                    </center>
                </div>

                <center>
                    <div class="modal-footer">
                    </div>

                </center>
            </div>
        </div>
    </div>
    <!-- Brand -->

    <div class="container-fluid pl-3 pr-3">
        <!-- Table -->
        <div class="row">




            <div class="col">

                <div class="bg-white rounded-lg mb-4">
                    <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
                        <h5 class="m-0 font-weight-bold text-primary h6 text-uppercase">Charts of Account</h5>
                        <div class="row align-items-center">

                            <div class="col-12 text-right">

                                <div class="dropdown no-arrow">

                                    <a class="dropdown-toggle" class="btn btn-info btn-sm" href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                        <i class="fas fa-ellipsis-v fa-sm fa-fw text-gray-400"></i>
                                    </a>
                                    <div class="dropdown-menu dropdown-menu-right shadow animated--fade-in" aria-labelledby="dropdownMenuLink">
                                        <div class="dropdown-header">Option:</div>
                                        <a class="dropdown-item" href="AccountAdd.aspx">Add account</a>


                                    </div>
                                </div>



                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-3">
                        </div>
                        <div class="col-6 small">
                            <asp:Repeater ID="Repeater1" runat="server">

                                <HeaderTemplate>
                                    <div class="table-responsive">
                                        <table class="table align-items-center table-sm  ">
                                            <thead>
                                                <tr>

                                                    <th scope="col">Account Name</th>


                                                    <th scope="col" class="text-right">Account Type </th>

                                                </tr>
                                            </thead>
                                            <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>

                                        <td class="text-primary">
                                            <%# Eval("Name")%>
                                        </td>


                                        <td class="text-gray-900 text-right">
                                            <%# Eval("AccountType")%>
                                        </td>



                                    </tr>

                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
              </table>
                                </FooterTemplate>

                            </asp:Repeater>
                        </div>
                        <div class="col-3">
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

                                <asp:Button ID="btnNext" class="btn btn-primary btn-sm btn-circle mx-2" runat="server" Text=">" OnClick="btnNext_Click" />

                            </li>

                        </ul>
                    </nav>
                </div>
            </div>
        </div>
    </div>
    <!-- Dark table -->



    <div class="modal fade" id="exampleModalCenter2" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="H1">Register Account</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">


                        <div class="col-12 " style="font-weight: bold">
                            <div class="form-group">
                                <label>Account type</label>



                                <asp:TextBox ID="txtAccounttype" class="form-control " placeholder="Account Name" runat="server"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-12 " style="font-weight: bold">
                            <div class="form-group">
                                <label>Account No.</label>



                                <asp:TextBox ID="txtAccountNo" class="form-control " runat="server"></asp:TextBox>
                                <asp:LinkButton
                                    ID="LinkButton1" runat="server"></asp:LinkButton>


                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 " style="font-weight: bold">
                            <div class="form-group">
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 " style="font-weight: bold">
                            <div class="form-group">
                                <label style="font-weight: bold">Dr./Cr.</label>

                                <asp:RadioButton ID="RadioButton5" runat="server" Text="Dr." ForeColor="#6666FF" GroupName="AT1" />
                                <asp:RadioButton ID="RadioButton6" runat="server" Text="Cr." ForeColor="#6666FF" GroupName="AT1" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12  " item="item" style="font-weight: bold">
                            <div class="form-group">
                                Account Division:<asp:DropDownList ID="DropDownList1" runat="server" OnTextChanged="Change" AutoPostBack="true"
                                    class="form-control ">
                                    <asp:ListItem>Main Account</asp:ListItem>
                                    <asp:ListItem>Sub Account</asp:ListItem>

                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                    </div>
                    <div class="row">

                        <div class="col-12  " style="font-weight: bold">
                            <div class="form-group">
                                Account Category:<asp:DropDownList ID="DropDownList2" runat="server"
                                    class="form-control ">
                                    <asp:ListItem>Income Statement</asp:ListItem>
                                    <asp:ListItem>Balance Sheet</asp:ListItem>
                                    <asp:ListItem>Cash Flow</asp:ListItem>

                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 " style="font-weight: bold">
                            <div class="form-group">
                                <label style="font-weight: bold">Operation type:</label>



                                <asp:RadioButton ID="RadioButton1" Text="Income" GroupName="AT" runat="server" EnableTheming="True" ForeColor="#6666FF" Checked="True" />

                                <asp:RadioButton ID="RadioButton2" runat="server" Text="Deposit" ForeColor="#6666FF" GroupName="AT" />
                                <asp:RadioButton ID="RadioButton3" runat="server" Text="Payment" ForeColor="#6666FF" GroupName="AT" />
                                <asp:RadioButton ID="RadioButton4" runat="server" Text="None" ForeColor="#6666FF" GroupName="AT" />
                            </div>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-12  ">
                            <div class="form-group">
                                Parent Account In F/S:<asp:DropDownList ID="DropDownList3" runat="server"
                                    class="form-control ">
                                    <asp:ListItem>Income Statement</asp:ListItem>
                                    <asp:ListItem>Balance Sheet</asp:ListItem>
                                    <asp:ListItem>Cash Flow</asp:ListItem>

                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-12  ">
                            <div class="form-group">
                                Account type:<asp:DropDownList ID="DropDownList4" runat="server"
                                    class="form-control ">
                                    <asp:ListItem>Asset</asp:ListItem>
                                    <asp:ListItem>Liabilty</asp:ListItem>
                                    <asp:ListItem>Owner's Equity</asp:ListItem>

                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <center>
                        <button type="button" class="btn btn-secondary btn-sm" data-dismiss="modal">Close</button>
                        <asp:Button ID="btnUpdate2" class="btn btn-default btn-sm" runat="server"
                            Text="Save changes" OnClick="btnUpdate2_Click" />
                </div>
                </center>
            </div>
        </div>
    </div>
</asp:Content>

