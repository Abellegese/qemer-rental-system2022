<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="markasreceivable.aspx.cs" Inherits="advtech.Finance.Accounta.markasreceivable" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<title>Full Credit</title>
    <style>Edit in JSFiddle
Result
HTML
CSS
Resources
/*
*
* ==========================================
* CUSTOM UTIL CLASSES
* ==========================================
*
*/

.rounded-lg {
  border-radius: 1rem !important;
}

.text-small {
  font-size: 0.9rem !important;
}

.custom-separator {
  width: 5rem;
  height: 6px;
  border-radius: 1rem;
}

.text-uppercase {
  letter-spacing: 0.2em;
}

/*
*
* ==========================================
* FOR DEMO PURPOSES
* ==========================================
*
*/

body {
  background: #00B4DB;
  background: -webkit-linear-gradient(to right, #0083B0, #00B4DB);
  background: linear-gradient(to right, #0083B0, #00B4DB);
  color: #514B64;
  min-height: 100vh;
}</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid">

    <div class="row text-center align-items-end">
      <!-- Pricing Table-->
      <div class="col-lg-4 mb-5 mb-lg-0">
        
      </div>
      <!-- END -->


      <!-- Pricing Table-->
      <div class="col-lg-4 mb-5 mb-lg-0">
        <div class="bg-white p-5 rounded-lg shadow">
          <h1 class="h6 text-uppercase font-weight-bold mb-4">Customer Credit Review</h1>
          <h2 class="h2 font-weight-bold" id="Span9" runat="server">ETB0.00<span class="text-small font-weight-normal ml-2">/ total</span></h2>

          <div class="custom-separator my-4 mx-auto bg-primary"></div>

          <ul class="list-unstyled my-5 text-small text-left font-weight-normal">
            <li class="mb-3">
              <i class="fa fa-check mr-2 text-primary"></i> Number of credit<span class="badge ml-3 h4 badge-danger badge-pill " id="NumberofCredit" runat="server">0</span> </li>
            <li class="mb-3">
              <i class="fa fa-check mr-2 text-primary"></i> Write off <span id="TotalWriteOff" class="mx-2" runat="server"></span></li>
          </ul>
               <asp:Button ID="Button1" runat="server" class="btn btn-danger btn-block p-2 shadow rounded-pill" Text="Proceed Credit" OnClick="Button1_Click" />

        </div>
      </div>
      <!-- END -->


      <!-- Pricing Table-->
      <div class="col-lg-4">
        
      </div>
      <!-- END -->

    </div>

        </div>
</asp:Content>
