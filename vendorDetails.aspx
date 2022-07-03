<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.master" AutoEventWireup="true" CodeBehind="vendorDetails.aspx.cs" Inherits="advtech.Finance.Accounta.vendorDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>Vendor Details</title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="container-fluid pl-3 pr-3">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
      <div class="row">
        <div class="col">
            <div class="bg-white rounded-lg">
<div class="row">

            <div class="col-lg-4 border-right">

                <div class="card-header bg-white  font-weight-bold">
                <div class=row>
                                <div class="col-md-6 text-left">
                                                             <span class=" text-xs font-weight-bold">                                    
                                    <a class="btn btn-light btn-circle mr-2" id="buttonback" runat="server" href="vendor.aspx" data-toggle="tooltip" data-placement="bottom" title="Back to vendor page">
             
                    <span class="fa fa-arrow-left text-danger"></span>
  
                </a>Personal Information</span>
                    
                </div>
                <div class="col-md-6 text-right">
                  </div>
                </div>

                </div>
                <div class="card-body">
                  <center>
                  
                  <img class="rounded-circle" src="../../asset/Brand/depositphotos.jpg" alt="" height="120" width="120">
                  <br />
                  <div style="padding: 20px 0px 0px 0px">
                  <span id="Span" runat="server" class="badge badge-light text-lg font-weight-bold text-gray-700"></span>
                  </div>
                  
                  </center>
                  <br />
                  <br />
                  <div >
                  <span class="font-weight-bold">ADSRESS</span>
                  <hr />
                  
                  <div class=" mr-2" >
                  <span><i class=" font-weight-bolder fas fa-address-book"></i></span>  <span id="billing" runat=server title="Biling Address" ></span>
                  </div>

                <div class=" mr-2" style="padding: 10px 0px 0px 0px">
                  <span><i class=" font-weight-bolder  fas fa-envelope"></i></span>  <span id="email" runat=server title="Email Address bg-white" ></span>
                  </div>
                                  <div class=" mr-2" style="padding: 10px 0px 0px 0px">
                  <span><i class=" font-weight-bolder  fas fa-mobile"></i>  </span><span id="mobile" runat=server title="Mobile Number" class="bg-white" ></span>
                  </div>
                                                    <div class=" mr-2" style="padding: 10px 0px 0px 0px">
                  <span><i class=" font-weight-bolder  fas fa-link"></i></span>  <a target="_blank" id="L" runat=server></a>
                  </div>
                                                    <div class=" mr-2" style="padding: 10px 0px 0px 0px">
                  <span><i class=" font-weight-bolder  fas fa-mobile-alt"></i></span>  <span id="work" runat=server title="Work Phone" ></span>
                  </div>
                  <hr />
                <span class="font-weight-bold">OTHER DETAILS</span>
                     <hr />                                                 
                     <div class=" mr-2" >
                  <span>Company Name<b>:</b>  </span>  <b class=text-right><span class="text-right" id="Span1" runat=server></span></b>
                  </div>
                                   <div class="mr-2">
                  <span>Credit Limit<b>:</b>  </span>  <b><span class="text-right text-lg" id="Span2" runat=server></span>  
                                                                   </b>
                  </div>
                  </div>
                </div>
       
            </div>

            <div class="col-lg-8">

                <!-- Card Header - Dropdown -->
                <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
                  <span class="m-0 small font-weight-bold text-primary text-uppercase">Payable and Credit Limit</span>

                </div>
                <!-- Card Body -->
          <div class=card-body>
                            <div class="row">

            <!-- Earnings (Monthly) Card Example -->
            <div class="col-xl-6 col-md-6 mb-1 border-right">

                <div class="card-body">
                  <div class="row no-gutters align-items-center">
                    <div class="col mr-2">
                      <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">Amount Due</div>
                      <div class="h5 mb-0 font-weight-light  text-gray-800"><span id=Span9 runat=server></span></div>
                        <div class="dropdown w-100 ">
                    <a class="dropdown-toggle btn-sm text-warning font-weight-bolder"  href="#" role="button" id="dropdownMenuLink2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                      
                    </a>
                    <div class="dropdown-menu  dropdown-menu-left shadow animated--fade-in w-100 text-center" aria-labelledby="dropdownMenuLink2">
                      <div class="dropdown-header  font-weight-bolder text-warning text-uppercase">Aged Receivable</div>
                        <div class="dropdown-divider"></div>
                       <table class="text-center dropdown-item">
                           <tbody>
                           <tr>
                            <td>
                               <i class="fas fa-arrow-circle-left text-success"></i>1-30 DAYS
                            </td>
                              <td class="text-right mx-4 font-weight-bolder">
                                 <span id="one" runat="server" class="text-right" style="margin-left: 30px"><i class="fas fa-dollar-sign text-success"></i>0.00</span>
                            </td>
                        </tr>
                           </tbody>
                        
                       </table>
                      
                      <div class="dropdown-divider"></div>
                      <table class="dropdown-item">
                           <tbody>
                           <tr>
                            <td>
                               <i class="fas fa-arrow-circle-right text-warning"></i>31-60 DAYS
                            </td>
                            <td>

                            </td>
                              <td class="text-right mx-4 font-weight-bolder">
                                <span id="two" runat="server" class="text-right" style="margin-left: 30px"><i class="fas fa-dollar-sign text-warning"></i>0.00</span>
                            </td>
                        </tr>
                           </tbody>
                        
                       </table>
                      <div class="dropdown-divider"></div>
                      <table class="dropdown-item">
                           <tbody>
                           <tr>
                            <td>
                                <i class="fas fa-arrow-circle-right text-danger"></i> <b>></b> 61 DAYS
                            </td>
                            <td>

                            </td>
                              <td class="text-right mx-4 font-weight-bolder">
                                <span id="three" runat="server" class="text-right" style="margin-left: 30px"><i class="fas fa-dollar-sign text-danger"></i>0.00</span>
                            </td>
                        </tr>
                           </tbody>
                        
                       </table>
                    </div>
                  </div>
                    </div>
                    <div class="col-auto">
                    <i class="fas fa-dollar-sign fa-2x text-gray-300"></i>
                      
                    </div>
                  </div>
                </div>

            </div>

            <!-- Earnings (Monthly) Card Example -->
            <div class="col-xl-6 col-md-6 mb-1">
 
                <div class="card-body">
                  <div class="row no-gutters align-items-center">
                    <div class="col mr-2">
                      <div class="text-xs font-weight-bold text-success text-uppercase mb-1">Unused Credit</div>
                      <div class="h5 mb-0 font-weight-light  text-gray-800"><span id="Credit" runat="server"></span></div>
                    </div>
                    <div class="col-auto">
                      <i class="fas fa-money-bill fa-2x text-gray-300"></i>
                    </div>
                  </div>
                </div>
      
            </div>
            

             
            <!-- Earnings (Monthly) Card Example -->

          </div>
               </div>           

        

              <br />
     
                
                                            <div class="card-header bg-white  d-flex flex-row align-items-center justify-content-between">
              <span class="m-0 font-weight-bold small text-primary text-uppercase">Expense</span>
             <div class="row align-items-center">
             </div>
                </div>
               
                <!-- Card Content - Collapse -->
     
                  <div class="card-body">
                          <main role="main" id="main" runat="server">

      <div class="starter-template">
          <center>        <p class="lead">
            <span class="fas fa-atom text-gray-400 fa-3x"></span></p>
        <span class="text-gray-400 mt-3 small" style="font-weight: bold">Sorry!! Nothing to show here.</span>

      </div>
          </center>


    </main>
                     <asp:Literal ID="ltChart" runat="server"></asp:Literal>        

                  </div>
                     <hr />
                     <div style="padding-right: 0px; padding-left: 26px">
                     <span><i id="infoicon" runat="server" class=" text-info fas fa-1x fa-info-circle"></i><span id="info" runat="server" class=" font-weight-light small"> The revenue value is in Thousands</span></span>
                     </div>
        
              

            </div>

          </div>
            </div>
                  
        </div>
        </div>
        </div>
</asp:Content>

