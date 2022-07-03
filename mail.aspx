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


</asp:Content>
