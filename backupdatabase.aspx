<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.master" AutoEventWireup="true" CodeBehind="backupdatabase.aspx.cs" Inherits="advtech.Finance.Accounta.backupdatabase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Create database backup</title>
    <link href="../../asset/css/starter-template.css" rel="stylesheet" />
                        <script type="text/javascript">

                    window.addEventListener('load', (event) => {
                        var x = document.getElementById("Pbutton");
                        x.style.display = "none";
                        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid ">
        <br />
        <br />
        <br />
    
        <main role="main" class="container">

      <div class="starter-template">
        <h2 style="font-weight: bold" class="text-gray-900">Welcome to Database Backup withward</h2>
        <p class="lead">Creating your backup periodially to avoid data loss</p>
          <br />


          <center>
                   <button class="w-25  btn btn-lg btn-danger" type="button" disabled id="Pbutton" style="display:none">
  <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
  Creating Backup...
</button>
          </center>

          <asp:Button ID="btnCreateDatabase"  class="w-25  btn btn-lg btn-danger" runat="server" Text="Create database backup" OnClick="btnCreateDatabase_Click" OnClientClick="myFunctionshop()" />

      </div>

    </main>
                  <script>
                        function myFunctionshop() {
                            var y = document.getElementById("<%=btnCreateDatabase.ClientID %>"); var x = document.getElementById("Pbutton");
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
