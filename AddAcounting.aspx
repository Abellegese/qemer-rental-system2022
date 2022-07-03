<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.master" AutoEventWireup="true" CodeBehind="AddAcounting.aspx.cs" Inherits="advtech.Finance.Accounta.AddAcounting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Manage Bank</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <!-- Navbar -->

        <div class="container-fluid pl-3 pr-3">
            <!-- Table -->
            <div class="row">
                <div class="col">
                    <div class="bg-white rounded-lg">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
                            <h6 class="m-0 text-primary font-weight-bold">Bank Account</h6>
                            <div class="row align-items-center">

                                <div class="col-12 text-right">

                                    <button runat="server" id="modalMain" type="button" class="btn btn-sm text-xs btn-danger" data-toggle="modal" data-target="#exampleModalLong2">
                                        <div>
                                            <i class="ni ni-check-bold"></i>
                                            <span>Add Account</span>
                                        </div>
                                    </button>

                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="table-responsive">
                                <asp:Repeater ID="Repeater1" runat="server">

                                    <HeaderTemplate>

                                        <table class="table align-items-center table-sm">
                                            <thead>
                                                <tr>


                                                    <th scope="col" class="text-gray-900 small">Account Name</th>
                                                    <th scope="col" class="text-gray-900 small">Account Type </th>

                                                    <th scope="col" class="text-gray-900 small">Acc. Code</th>
                                                    <th scope="col" class="text-gray-900 small">Currency</th>
                                                    <th scope="col" class="text-gray-900 small">Account Number</th>
                                                    <th scope="col" class="text-gray-900 small">Bank Name</th>
                                                    <th scope="col" class="text-gray-900 small">Description</th>
                                                    <th scope="col" class="text-gray-900 small">Precedence</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>


                                            <td>

                                                <a class="dropdown-item text-primary" title="Show the details" href="bankstatment.aspx?ref2=<%# Eval("AccountName")%>"><%#Eval("AccountName")%></a>
                                            </td>
                                            <td class="text-gray-900 small">
                                                <%# Eval("AccountType")%>
                                            </td>

                                            <td class="text-gray-900 small">
                                                <%# Eval("AccountCode")%>
                                            </td>
                                            <td class="text-gray-900 small">
                                                <%# Eval("Currency")%>
                                            </td>
                                            <td class="text-primary small">
                                                <%# Eval("AccountNumber")%>
                                            </td>
                                            <td class="text-gray-900 small">
                                                <%# Eval("BankName")%>
                                            </td>
                                            <tdv>
                                                <%# Eval("Discrption")%>
                                            </tdv>
                                            <td class="text-gray-900 small">
                                                <%# Eval("primary1")%>
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
                    </div>
                    <div class="card-footer bg-white py-4">
                        <nav aria-label="...">
                        </nav>
                    </div>
                </div>
            </div>
        </div>
        <!-- Dark table -->
        <div class="row mt-5">
        </div>
    </div>
    <div class="modal fade" id="exampleModalLong2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title text-gray-900 font-weight-bold" id="exampleModalLongTitle">New Account</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">


                                <asp:TextBox ID="txtAccountName" runat="server" class="form-control form-control-sm" placeholder="Account Name"></asp:TextBox>

                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">


                                <asp:TextBox ID="txtAccountCode" runat="server" class="form-control form-control-sm" placeholder="Account code"></asp:TextBox>

                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">


                                <asp:DropDownList ID="DropDownList1" runat="server" class="form-control form-control-sm" placeholder="">
                                    <asp:ListItem>ETB</asp:ListItem>
                                    <asp:ListItem>USD</asp:ListItem>


                                </asp:DropDownList>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">


                                <asp:TextBox ID="txtAccountNumber" runat="server" class="form-control form-control-sm" placeholder="Account Number"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">


                                <asp:TextBox ID="txtBankName" runat="server" class="form-control form-control-sm" placeholder="Bank Name"></asp:TextBox>

                            </div>
                        </div>
                    </div>

                    <div class="form-group mb-0">

                        <div class="input-group input-group-alternative">
                            <asp:TextBox ID="txtopening" class="form-control " placeholder="Opening Balance" Style="border-color: #ff0000" runat="server"></asp:TextBox>
                            <div class="input-group-prepend">
                                <span class="input-group-text">ETB</span>
                            </div>

                        </div>
                    </div>
                    <br />
                    <div class="form-group">
                        <label class="font-weight-bold text-gray-900 small">Date of Opening<span class="font-weight-bold text-danger">*</span></label>


                        <div class="input-group input-group-alternative">
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="fas fa-calendar-day"></i></span>
                            </div>
                            <asp:TextBox ID="txtOrderDate" class="form-control " runat="server" TextMode="Date"></asp:TextBox>
                        </div>


                    </div>

                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">


                                <asp:TextBox ID="txtRemark" runat="server" class="form-control form-control-sm" placeholder="Description Max 500 char" Height="100px" TextMode="MultiLine"></asp:TextBox>

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
</asp:Content>

