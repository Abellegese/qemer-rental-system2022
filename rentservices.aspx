<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="rentservices.aspx.cs" Inherits="advtech.Finance.Accounta.rentservices" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Start Services</title>
    <link href="../../asset/css/starter-template.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <main role="main" class="container">
            <br />
            <br />
      <div class="starter-template">
        <h3 style="font-weight: bold" class="text-gray-900 mb-2">QemerBook Rent Services Wizard</h3>
        <p class="lead small">Tracking your customer receivable is alot easier than ever here in QemerBook. Start your service.</p>
        <br />
      <a class="w-auto  btn btn-danger" href="rentstatus.aspx?createservice=true">Start Tracking</a>
      </div>

    </main>
</asp:Content>
