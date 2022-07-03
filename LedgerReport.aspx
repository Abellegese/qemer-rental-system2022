<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.master" AutoEventWireup="true" CodeBehind="LedgerReport.aspx.cs" Inherits="advtech.Finance.Accounta.LedgerReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="javascript">
    function printdiv(printpage) {
        var headstr = "<html><head><title></title></head><body>";
        var footstr = "</body>";
        var newstr = document.all.item(printpage).innerHTML;
        var oldstr = document.body.innerHTML;
        document.body.innerHTML = headstr + newstr + footstr;
        window.print();
        document.body.innerHTML = oldstr;
        return false;
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="container-fluid ">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <!-- Navbar -->

    <!-- End Navbar -->
    <!-- Header -->

    
      <!-- Table -->
      <div class="row">
     

               

        <div class="col">
                 <input name="b_print" type="button" class="btn btn-default"   onclick="printdiv('div_print');" value=" Print " />
        <div id="div_print"> 
                        <div class="card mb-4 ">

                                <div class="card-header text-black bg-white font-weight-bold">
                <center>
                  <h4 class="m-0 font-weight-bold ">General Ledger</h4>
                  <br />
                <h6 class="m-0 font-weight-bold ">As of Dec 31, 2020</h6>
                </center>
                </div>
                <div class="card-body ">

                    <asp:Repeater ID="Repeater1" runat="server" >
                         
                <HeaderTemplate>
            <div class="table-responsive">
<table class="table align-items-center table-sm  " id="dataTable" width="100%" cellspacing="0">
                <thead >
                  <tr>
                
                  <th class="text-left text-uppercase">Account Name</th>
                 

                    <th class="text-right text-uppercase">Debit</th>
                    <th class="text-right text-uppercase">Credit</th>
                    <th class="text-right text-uppercase" >Balance</th>

                   
                  </tr>
                </thead>

                <tbody>
                                                                                        </HeaderTemplate>
                <ItemTemplate>
                  <tr>

                                        <td class="text-primary font-weight-normal">
                                       <%# Eval("Account")%>
                   
                    </td>



                                               <td class="text-right text-uppercase">
                                                   <asp:Label ID="Label4" runat="server" Text=<%# Eval("Debit", "{0:N2}")%>></asp:Label>
                    
                    </td>

                                        <td class="text-right text-uppercase">
                                            <asp:Label ID="Label5" runat="server" Text=<%# Eval("Credit", "{0:N2}")%>></asp:Label>
                    
                    </td>
                                                            <td class="text-right text-uppercase">
                                                                <asp:Label ID="Label2" runat="server" Text=<%# Eval("Balance", "{0:N2}")%>></asp:Label>
                    
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

              </div>
              </div>
              </div>
</asp:Content>

