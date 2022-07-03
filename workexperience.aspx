<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="workexperience.aspx.cs" Inherits="advtech.Finance.Accounta.workexperience" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <title>Retirement Card</title>
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid ">
      <!-- Table -->
                <div class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
        <h5 class="modal-title text-gray-900 font-weight-bolder" id="H1">Add Gratitude</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
                                    <div class="row" >

                                  <div class="col-12">
                                            <div class="form-group">

                                                           <div class="form-group mb-0">
            <div class="input-group input-group-alternative">
            <asp:TextBox ID="txtGratitude" class="form-control " TextMode="MultiLine" Height="360px" Placeholder="His/Her responsibilities included"  runat="server"></asp:TextBox>

            </div>
          </div>
          </div>
          </div>

                                                 </div>  
      </div>
      <div class="modal-footer">
      <center>
        <button type="button" class="btn btn-secondary btn-sm " data-dismiss="modal">Close</button>
                                            <asp:Button ID="btnUpdate" OnClick="btnUpdate_Click"   class="btn btn-sm btn-primary" Text="Save gratitudes"    runat="server" />
                                        </div>
     </center>
      </div>
    </div>
  </div>
           <div class="card shadow-none mb-1 card">
            <div class="card-header bg-white ">
                
                
                   <div class="row">
                 <div class="col-xl-3 text-left ">
                           <a id="link" runat="server" class="btn btn-default btn-sm">
                  <i class="fas fa-arrow-left  font-weight-bold text-primary"></i><span id="account" class="font-weight-bold text-primary" runat="server"></span>
              </a>

      

                </div>
                 <div class="col-xl-9 text-right ">

                    <button name="b_print" onclick="printdiv('div_print');" type="button" title="Print" class="mx-2 border-primary border-left border-top border-right border-bottom btn btn-sm btn-default btn-circle" data-toggle="modal" data-target="#exampleModalCenter" >
                    <div>
                      <i class="fas fa-print text-primary font-weight-bold"></i>
                   
                    </div>
                  </button>
                  <button type="button" runat=server id="Sp2" class="border-primary border-left border-top border-right border-bottom btn btn-sm btn-default btn-circle" data-toggle="modal" data-target=".bd-example-modal-lg" >
                    <div>
                      <i class="fas fa-plus text-primary font-weight-bold"></i>
                   <span></span>
                    </div>
                  </button>

                </div>
                   </div>

            </div>
            </div>
        <div id="div_print"> 
          <div class="card shadow-none mb-4">
            <div class="card-header mb-4 text-black bg-white font-weight-bold">
                <center>
                    <h5 id="oname" runat="server" class="mb-1 text-gray-700 font-weight-bold "></h5>
<h4 class="mb-1 font-weight-bold text-uppercase text-gray-900 ">Work Experience Card</h4>
                   <span class="mb-2 text-gray-700 font-weight-bold ">Date of Issuance:<span class="h6 mx-1 mb-2 text-gray-700 font-weight-bold " id="mont" runat="server"></span></span>
                </center>
              
              <asp:Label ID="lblMsg" runat="server" ></asp:Label>
                            <div class="row align-items-center">
                                
                <div class="col-12 text-right">
                </div>
              </div>
            </div>
              <div class="row">
                  <div class="col-xl-2">

                  </div>
                   <div class="col-xl-8 col-md-12">
               

<h4 class="text-center mb-2" style="text-decoration: underline">To Whom It May Concern:</h4>

<p class="text-gray-900 align-content-center mx-3 ml-3 my-3" style="text-align: justify">This letter ensures we were employed <span id="empname" runat="server" class="font-weight-bolder font-italic"></span> as <span class="font-weight-bold" id="position" runat="server"></span> with our company, <span class="text-uppercase font-weight-bolder h6" id="compName" runat="server"></span>, during the period beginning <span class="font-italic font-weight-bold" id="datebegining" runat="server"></span>, and ending <span class="font-italic font-weight-bold" id="dateending" runat="server"></span>.

During <span class="text-gray-900" id="gen1" runat="server"></span> time with <span class="text-uppercase font-weight-bolder h6" id="comp2" runat="server"></span>, <span class="text-gray-900" id="sr1" runat="server"></span> <span id="emp2" runat="server" class="font-weight-bold font-italic"></span> has remained dedicated and loyal to <span class="text-gray-900" id="gen2" runat="server"></span> work and responsibilities with our company. <span class="text-gray-900" id="gen3" runat="server"></span> responsibilities <span id="gratitude" runat="server" class="text-gray-900"></span> has done an exemplary job while in <span class="text-gray-900" id="gen4" runat="server"></span> role as an <span id="position2" runat="server" class="font-weight-bold"></span> at <span id="comp3" runat="server" class="text-uppercase font-weight-bold"></span>. <span class="text-gray-900" id="sir4" runat="server"></span> <span class="font-weight-bold font-italic" id="emp3" runat="server"></span> has always maintained a professional and courteous attitude and appearance while with our company.

<span class="text-gray-900" id="sir6" runat="server"></span> <span class="font-italic font-weight-bold" id="emp4" runat="server"></span>’s decision to end <span class="text-gray-900" id="gen6" runat="server"></span> employment with our company is solely <span class="text-gray-900" id="gen7" runat="server"></span> own decision, and we wish <span class="text-gray-900" id="gensg1" runat="server"></span> all the best in <span class="text-gray-900" id="gen9" runat="server"></span> future career opportunities.</p>
<p class="mt-2 text-gray-900 mx-3 ml-3 my-3">Please contact us for any additional information.</p>


<p class="text-gray-900 mx-3 ml-3 my-3">Sincerely,</p>
 <p><span  class="text-gray-900 mx-3 ml-3 my-3">General Manager: </span><span class="text-gray-900 font-weight-bold mx-3 ml-3 my-3" id="manager" runat="server"></span>_______________________</p>



<span id="comp5" runat="server" class="text-uppercase mx-3 ml-3 my-3 font-weight-bold"></span>, <span id="compaddress" runat="server" class="small text-gray-900 mx-3 ml-3 my-3"></span>                  
                       
              
  
           

 
            </div>
                  
                  
              </div>

            
          </div>
            </div>
        </div>
</asp:Content>
