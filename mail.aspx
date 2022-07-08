<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="mail.aspx.cs" Inherits="advtech.Finance.Accounta.mail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click"/>
    <asp:Button ID="Button3" runat="server" Text="Convert" OnClick="Button3_Click" />
    <asp:Button ID="Button2" runat="server" Text="Send SMS" class="btn btn-success" OnClick="Button2_Click" />
    <asp:Button ID="Button4" runat="server" Text="Amharic" class="btn btn-success" OnClick="Button4_Click" />
    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
    <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Update row id" />
    <span id="counter" runat="server" visible="false"></span>
    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">

        <HeaderTemplate>
            <div class="table-responsive small">
                <table class="table align-items-center table-bordered table-sm " id="dataTable" width="100%" cellspacing="0">
                    <thead>
                        <tr>

                            <th scope="col">Date</th>

                            <th class="text-right">Reference
                            </th>
                            <th class="text-right">Date Clear in Bank Rec

                            </th>
                            <th class="text-right">Number of Distributions

                            </th>
                            <th class="text-right">G/L Account

                            </th>
                            <th class="text-right">Description

                            </th>
                            <th class="text-right">Amount

                            </th>
                            <th class="text-right">Job ID

                            </th>
                            <th class="text-right">Used for Reimbursable Expenses

                            </th>
                            <th class="text-right">Transaction Period

                            </th>
                            <th class="text-right">Transaction Number

                            </th>
                            <th class="text-right">Consolidated Transaction

                            </th>
                            <th class="text-right">Recur Number

                            </th>
                            <th class="text-right">Recur Frequency

                            </th>
         
                        </tr>
                    </thead>
                    <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>

                <td class="text-primary"><%# Eval("Date")%></td>
                <td class="text-right text-gray-900"></td>
                <td class="text-right text-gray-900"></td>
                <td class="text-right text-gray-900">2</td>
                <td class="text-right text-gray-900">
                    <asp:Label ID="lblAccount" runat="server" Text='<%# Eval("Account")%>'></asp:Label>
                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("LedID")%>'></asp:Label>
                </td>
                <td class="text-right text-gray-900"><%# Eval("Explanation")%></td>
                <td class="text-right text-gray-900">
                    <asp:Label ID="lblAmount" runat="server"></asp:Label></td>
                <td class="text-right text-gray-900"></td>
                <td class="text-right text-gray-900">FALSE</td>
                <td class="text-right text-gray-900"></td>
                <td class="text-right text-gray-900"></td>
                <td class="text-right text-gray-900">FALSE</td>
                <td class="text-right text-gray-900">0</td>
                <td class="text-right text-gray-900">0</td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </tbody>
              </table>
                    </div>
        </FooterTemplate>

    </asp:Repeater>

</asp:Content>
