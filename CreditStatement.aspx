<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="CreditStatement.aspx.cs" Inherits="advtech.Finance.Accounta.CreditStatement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    <style>
        .water{
            content:'Raksym Trading PLC';
            align-content:center;
            justify-content:center;
            opacity:0.7;
            z-index:1;
            transform:rotate(-45deg);
            position:relative;
        }
    </style>
<title>Credit statement</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid pl-3 pr-3" style="position:absolute;">

    <!-- Navbar -->

    <!-- End Navbar -->
    <!-- Header -->

    
      <!-- Table -->
           
      <div class="row">
        <div class="col-12">
            <div class="bg-white rounded-lg mb-2 ">
            <div class="card-header bg-white ">
                <div class="row">
                <div class="col-4 text-left">
                                                        <a class="btn btn-light btn-circle mr-2" id="buttonback" runat="server"  data-toggle="tooltip" data-placement="bottom" title="Back to customer details">
             
                    <span class="fa fa-arrow-left text-danger"></span>
  
                </a>
                </div>
            <div class="col-8 text-right">

                <button name="b_print" onclick="printdiv('div_print');" type="button" title="Print" class=" btn btn-sm btn-info btn-circle" data-toggle="modal" data-target="#exampleModalCenter" >
                    <div>
                      <i class="fas fa-print text-white font-weight-bold"></i>
                   
                    </div>
                  </button>

                   </div>
                </div>

            </div>
            
         <div id="div_print"> 
                      
         <div class="row" id="div4" runat="server">
                    <div class="col-1">

                    </div>
                    <div class="col-10">
    
                                 
                    <div class="card-header text-black bg-white font-weight-bold">


          
                <div class="row border-bottom" style="height:90px">
                    <div class="col-md-4 text-left">
                                 <img class="" src="../../asset/Brand/gh.jpg" alt="" width="110" height="80">
                    </div>
                <div class="col-md-8 text-right">

    
            <h4 class="text-gray-900 font-weight-bold">CREDIT STATEMENT</h4>
              </div>
                
                </div>

               <div class="row " style="height:60px">
                <div class="col-md-6 text-left">
                    <span translate="no" class="h5 text-gray-900 text-uppercase font-weight-bold" id="oname" runat="server"></span>
                    </div>
               <div class="col-md-6 text-right">
                   <span translate="no" class="small text-gray-900 font-weight-bold" id="datecurrent" runat="server"></span>
                    </div>
                   </div>

              <div class="row  ">
                <div class="col-md-12 text-left">
                    <span class=" h6 text-gray-900 "  style="height:100px">To [ለ]: </span><span style="height:100px" class="h6 mx-2 text-gray-900 font-weight-bold font-italic" id="Name" runat="server"></span>
                    [<span class="text-gray-900 small font-weight-bold mt-3">Shop No [የሱቅ ቁጥር]: </span><span class="text-gray-900 small mx-1 font-weight-bold mt-3" id="ShopNo" runat="server"></span>]
                    </div>
                    </div>
                </div>

                                 <div class="card-body " style="height:1300px">
                                           
     
                           
                  
                         <h6  class="text-gray-900 font-weight-bold text-left mb-3">ውድ ደንበኛ</h6>
                           
                               <p class="h5  text-gray-900 mb-4" style="text-align: justify; text-indent: 0px; line-height: 40px;">


                       ድርጅታችን ላይ ያለበዎት እዳ የክፍያ ቀኑን ስላለፈ ፤ ይህ እርስዎን ማስታወሻ ደብዳቤ ነው፡፡ ድርጅታችን ላይ የተመዘገበው የእዳ መጠን ብር  <span class="font-weight-bold h6 text-gray-900" id="TotalReceivable" runat="server"></span> እንዳለበዎት ያሳያል፡፡ ከታች ያለውን ዝርዝር በማጣቀስ፡፡

                        </p>
  
                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound"  >
                         
                  
                <HeaderTemplate>
                
              <table class="table align-items-center table-sm small  ">

                <thead class=" thead-dark">
                  <tr>
                
              
                    <th scope="col" class="text-left ">Date</th>
                    <th scope="col" class="text-left">CN#</th>
                    <th scope="col" class="text-left">Amount</th>
                    <th scope="col" class="text-right">Days Overdue</th>

                  </tr>
                </thead>

                <tbody>
                 </HeaderTemplate>
                <ItemTemplate>
                  <tr >


                    <td class="text-left text-gray-900">
        <asp:Label ID="lblDueDate" runat="server" Text=<%# Eval("date","{0:MMMM dd, yyyy}")%>></asp:Label>
                    
                    </td>
                                        <td class="text-left text-gray-900">
                    <%# Eval("id")%>
                    </td>

                                        <td class="text-left text-gray-900">
                    <%# Eval("balance", "{0:N2}")%>
                    </td>
                     <td class="text-right">
                      <asp:Label ID="lblAged" runat="server"></asp:Label></td>


                  </tr>
                 </ItemTemplate>
                <FooterTemplate>
                </tbody>
              </table>
            
              <hr class="text-gray-700 font-weight-bold" />
                  </FooterTemplate>
                                                     
           </asp:Repeater>
                                     <center>
                                         <h1 class="water h1  font-weight-bolder" style="font-size: 60px" id="WaterMarkOname" runat="server"></h1>
                                     </center>
                                <p class="h5  text-gray-900 mb-xl-5" style="text-align: justify; line-height: 23px; height:100px">እስካሁን ያለዎትን ጥሩ የክፍያ ሁኔታ አገናዝበን ፤ እርስዎም በርስዎ በኩል መርመረው በቶሎ እንዲያሳውቁን ስንል በትህትና እንጠይቃለን፡፡</p>
                                     
     
                                        <div class="row mt-xl-4">
                            <div class="col-md-12 text-right">
                                <h5 class="font-italic mb-2 text-gray-900 font-weight-bold">ከሰላምታ ጋር</h5>
                                <h5 class="font-italic text-gray-900 font-weight-bold">አስተዳደር ቢሮ</h5>
          
                            </div>


                        </div>
                                     <div class="row mt-lg-5">
                                         <div class="col-12 border-top border-bottom">
                                             <div class="row">
                                                 <div class="col-6 text-left">
                                                     <span class="fas fa-address-book text-gray-400 mr-2"></span><span class="mb-2 text-gray-900 small " id="addressname" runat="server"></span>

                                                 </div>
                                                 <div class="col-6 text-right">
                                                     <span class="fas fa-phone text-gray-400 mr-2"></span><span class="mb-2 text-gray-900 small " id="phone" runat="server"></span>


                                                 </div>
                                             </div>
                                         </div>
                                     </div>

                                 </div>

                                </div>
     
                                <div class="col-1">

                                </div>
                            </div>

                          </div>

                        </div>

                          </div>
                          </div>
                          </div>
</asp:Content>
