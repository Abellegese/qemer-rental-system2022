<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.master" AutoEventWireup="true" CodeBehind="CustomerDetails.aspx.cs" Inherits="advtech.Finance.Accounta.CustomerDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    <script type="text/javascript">
        window.addEventListener('load', (event) => {
            var x = document.getElementById("myDIV5");
            x.style.display = "none";
        });
        window.addEventListener('load', (event) => {
            var x = document.getElementById("myDIV55");
            x.style.display = "none";
        });
        window.addEventListener('load', (event) => {
            var x = document.getElementById("myDIV555");
            x.style.display = "none";
        });
    </script>

    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" rel="stylesheet">
    <style>
        ul.timeline {
            list-style-type: none;
            position: relative;
        }

            ul.timeline:before {
                content: ' ';
                background: #b374d9;
                display: inline-block;
                position: absolute;
                left: 29px;
                width: 2px;
                height: 100%;
                z-index: 400;
            }

            ul.timeline > li {
                margin: 20px 0;
                padding-left: 20px;
            }

                ul.timeline > li:before {
                    content: ' ';
                    background: white;
                    display: inline-block;
                    position: absolute;
                    border-radius: 20%;
                    border: 3px solid #b374d9;
                    left: 22px;
                    width: 15px;
                    height: 15px;
                    z-index: 400;
                }

        body {
            background-color: #eee
        }

        .mt-70 {
            margin-top: 70px
        }

        .mb-70 {
            margin-bottom: 70px
        }

        .card {
            box-shadow: 0 0.46875rem 2.1875rem rgba(4, 9, 20, 0.03), 0 0.9375rem 1.40625rem rgba(4, 9, 20, 0.03), 0 0.25rem 0.53125rem rgba(4, 9, 20, 0.05), 0 0.125rem 0.1875rem rgba(4, 9, 20, 0.03);
            border-width: 0;
            transition: all .2s
        }

        .card {
            position: relative;
            display: flex;
            flex-direction: column;
            min-width: 0;
            word-wrap: break-word;
            background-color: #fff;
            background-clip: border-box;
            border: 1px solid rgba(26, 54, 126, 0.125);
            border-radius: .25rem
        }

        .card-body {
            flex: 1 1 auto;
            padding: 1.25rem
        }

        .vertical-timeline {
            width: 100%;
            position: relative;
            padding: 1.5rem 0 1rem
        }

            .vertical-timeline::before {
                content: '';
                position: absolute;
                top: 0;
                left: 67px;
                height: 100%;
                width: 4px;
                background: #e9ecef;
                border-radius: .25rem
            }

        .vertical-timeline-element {
            position: relative;
            margin: 0 0 1rem
        }

        .vertical-timeline--animate .vertical-timeline-element-icon.bounce-in {
            visibility: visible;
            animation: cd-bounce-1 .8s
        }

        .vertical-timeline-element-icon {
            position: absolute;
            top: 0;
            left: 60px
        }

            .vertical-timeline-element-icon .badge-dot-xl {
                box-shadow: 0 0 0 5px #fff
            }

        .badge-dot-xl {
            width: 18px;
            height: 18px;
            position: relative
        }

        .badge:empty {
            display: none
        }

        .badge-dot-xl::before {
            content: '';
            width: 10px;
            height: 10px;
            border-radius: .25rem;
            position: absolute;
            left: 50%;
            top: 50%;
            margin: -5px 0 0 -5px;
            background: #fff
        }

        .vertical-timeline-element-content {
            position: relative;
            margin-left: 90px;
            font-size: .8rem
        }

            .vertical-timeline-element-content .timeline-title {
                font-size: .8rem;
                text-transform: uppercase;
                margin: 0 0 .5rem;
                padding: 2px 0 0;
                font-weight: bold
            }

            .vertical-timeline-element-content .vertical-timeline-element-date {
                display: block;
                position: absolute;
                left: -90px;
                top: 0;
                padding-right: 10px;
                text-align: right;
                color: #adb5bd;
                font-size: .7619rem;
                white-space: nowrap
            }

            .vertical-timeline-element-content:after {
                content: "";
                display: table;
                clear: both
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="modal fade bd-example-modal-lg" id="CashSummary" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-md">
    <div class="modal-content">
      <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
        <h5 class="modal-title text-gray-900 font-weight-bold" id="H1"><span class="fas fa-calendar mr-2" style="color:#ff2ccd"></span>Fetch Revenue data</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
                                    <div class="row" >
                                      


                                                                                                        <div class="col-6 ">
                                            <div class="form-group">
                                                <label class=font-weight-bold>From<span class=text-danger>*</span></label>
    
                                            
                                                 <br />
                                                           <div class="form-group mb-0">
            <div class="input-group input-group-alternative">
            <asp:TextBox ID="txtCHDateFromCash" class="form-control form-control-sm " TextMode=Date  runat="server"></asp:TextBox>
   
              
            </div>
          </div>
          </div>
          </div>
                                                                                                                  <div class="col-6">
                                            <div class="form-group">
                                                <label class=font-weight-bold>To<span class=text-danger>*</span></label>
    
                                            
                                                 <br />
                                                           <div class="form-group mb-0">
            <div class="input-group input-group-alternative">
            <asp:TextBox ID="txtCHDateToCash" class="form-control form-control-sm " TextMode=Date  runat="server"></asp:TextBox>

            </div>
          </div>
          </div>
          </div>

                                                 </div>  
      </div>
      <div class="modal-footer">
      <center>
                                                                        <button class="btn btn-primary btn-sm w-100 " style="background-color:#ff00bb;display:none"  type="button" disabled id="Pbutton">
  <span class="spinner-grow spinner-grow-sm" role="status" aria-hidden="true"></span>
  Binding Data
</button>
                                            <asp:Button ID="btnCashSummary" class="btn btn-sm btn-danger" OnClientClick="myFunctionshop222();" style=" background-color:#ff2ccd" OnClick="btnBindCashSumary_Click"   runat="server"  
                                        Text="Bind Summary"   />
                                        </div>
     </center>
      </div>
    </div>
  </div>




    <div class="modal fade bd-example-modal-lg" id="CustID" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
        <div class="modal-body">
                 <main role="main" id="main1" runat="server">

      <div class="starter-template">
          <center>
        
        <p class="lead text-primary">
            <span class="fas fa-image text-gray-300 fa-4x"></span></p>
      
          <h6 class="text-gray-300 text-xs" style="font-weight: bold">No Customer ID Image Found. <a class="btn btn-link text-primary" id="A8" runat=server href="#" data-toggle="modal" data-target="#exampleModal1111">Click to add</a></h6>
          </center>
 </div>

    </main>
            
<div id="carousel-example-generic" class="carousel slide carousel-fade" data-ride="carousel"" data-ride="carousel" >
                        <!-- Indicators -->
                        <ol class="carousel-indicators" id="prfdiv" runat="server">
                            <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
                            <li data-target="#carousel-example-generic" data-slide-to="1"></li>
                            <li data-target="#carousel-example-generic" data-slide-to="2"></li>

                        </ol>

                        <!-- Wrapper for slides -->
      
                        <div class="carousel-inner" id="showdefaultimagefromdatabase" runat="server">
                            <asp:Repeater ID="Repeater2" runat="server">
                                <ItemTemplate>
                                    <div class="carousel-item <%# GetActiveClass(Container.ItemIndex) %>">
                                        <img class=" w-100" src="../../asset/custID/<%# Eval("filename")%><%# Eval("extension")%>">
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <!-- Controls -->
                        <a class="carousel-control-prev text-gray-900" href="#carousel-example-generic" role="button" data-slide="prev">
                            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                            <span class="sr-only">Previous</span>
                        </a>
                        <a class="carousel-control-next" href="#carousel-example-generic" role="button" data-slide="next">
                            <span class="carousel-control-next-icon" aria-hidden="true"></span>
                            <span class="sr-only">Next</span>
                        </a>
                    </div>
         
 
            </div>
    </div>
  </div>
</div>
    <div class="container" id="CCF" runat="server" visible="false">
        <div class="text-center mt-5">
            <span class="fas fa-6x fa-user-alt-slash"></span>
            <h1>Customer Couldn't be Found</h1>
            <p class="lead">Enter a correct customer name and try again.</p>
            <p>type customer name in top-bar search</p>
        </div>
    </div>
<div class="container-fluid pr-3 pl-3  " id="container" runat="server" >
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
      <div class="row ">
          <div class="col">
              <div class="bg-white rounded-lg">
                  <div class="row">

                      <div class="col-lg-4 border-right ">

                          <!-- Default Card Example -->

                          <div class="card-header bg-white  font-weight-bold">
                              <div class="row">
                                  <div class="col-md-6 text-left">
                                      <span class=" text-xs font-weight-bold">
                                          <a class="btn btn-light btn-circle mr-2" id="buttonback" runat="server" href="Customer.aspx" data-toggle="tooltip" data-placement="bottom" title="Back to customer page">

                                              <span class="fa fa-arrow-left" style="color:#d46fe8"></span>

                                          </a>Personal Information</span>

                                  </div>
                                  <div class="col-md-6 text-right">

                                      <a class="btn btn-light btn-circle mr-2" id="A4" runat="server" data-toggle="modal" data-target="#CustID" title="Watch customer ID">

                                          <span class="fa fa-user" style="color: #d46fe8"></span>

                                      </a>
                                      <button class="btn btn-light btn-circle " type="button" data-toggle="modal" data-target="#exampleModal9v">

                                          <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Announce Customer">
                                              <div>
                                                  <i class="fas fa-bullhorn" style="color: #d46fe8"></i>

                                              </div>
                                          </a>

                                      </button>

                                  </div>
                              </div>
                              <asp:Label ID="lblMsg" runat="server"></asp:Label>
                          </div>
                          <div class="card-body ">
                              <center>
                                  <div id="profilediv" runat="server">
                                      <asp:Repeater ID="Repeater1" runat="server">
                                          <ItemTemplate>
                                              <img class="rounded-circle" src="../../asset/custProfile/<%#Eval("filename")%><%#Eval("extension")%>" height="70" width="70" />
                                          </ItemTemplate>
                                      </asp:Repeater>
                                  </div>
                                  <div id="defaultprofile" runat="server">


                                      <img class="rounded-circle" src="../../asset/Brand/images22.png" alt="" height="100" width="100">
                                      <br />
                                  </div>
                                  <div style="padding: 20px 0px 0px 0px">
                                      <span id="Span" runat="server" class="badge badge-light text-lg font-weight-bold text-gray-700"></span>
                                      <button runat="server" title="Update status" id="Button11" type="button" class="btn btn-sm btn-default btn-circle mb-1" data-toggle="modal" data-target="#exampleModal111">
                                          <div>
                                              <span class=" text-gray-500 fas fa-pencil-alt"></span>

                                          </div>
                                      </button>
                                      <br />
                                      <a href="#" data-toggle="modal" data-target="#exampleModalS"><span class="small text-danger ml-2" data-toggle="tooltip" title="TIN#">[TIN# <span class="ml-1" id="TINNumber" runat="server"></span>]</span></a>
                                      <br />
                                      <br />
                                      <span><i class="fas fa-dollar-sign text-white btn-circle btn-sm mx-2 mr-2 " style="background-color: #ff6a00"></i><span class="small text-danger text-uppercase">Overpayment:</span></span>  <span class="small text-gray-900 mt-1" id="OverPayAmount" runat="server"></span>
                                  </div>



                              </center>
                              <br />
                              <br />
                              <div>
                                  <span class="font-weight-bold text-gray-900">BASIC INFO</span>
                                  <hr />


                                  <div class=" mr-2" style="padding: 10px 0px 0px 0px">
                                      <span data-toggle="tooltip" data-placement="top" title="Email"><i class="fas fa-mail-bulk text-white btn-circle btn-sm mx-2 " style="background-color: #ff6a00"></i></span><span id="email" runat="server" class="small text-gray-900" title="Email Address bg-white"></span>
                                  </div>
                                  <div class=" mr-2" style="padding: 10px 0px 0px 0px">
                                      <span data-toggle="tooltip" data-placement="top" title="Mobile"><i class="fas fa-mobile text-white btn-circle btn-sm mx-2 " style="background-color: #ff6a00"></i></span><span id="mobile" runat="server" title="Mobile Number" class="bg-white small text-gray-900"></span>
                                      <button runat="server" title="Update status" id="Button2" type="button" class="btn btn-sm btn-circle btn-default " data-toggle="modal" data-target="#exampleModal1">
                                          <div>
                                              <i class="fas fa-pencil-alt text-danger"></i>

                                          </div>
                                      </button>
                                  </div>
                                  <div class=" mr-2" style="padding: 10px 0px 0px 0px">
                                      <span data-toggle="tooltip" data-placement="top" title="Website"><i class="fas fa-link text-white btn-circle btn-sm mx-2 " style="background-color: #ff6a00"></i></span><a target="_blank" id="L" class="small text-gray-900" runat="server"></a>
                                  </div>
                                  <div class=" mr-2" style="padding: 10px 0px 0px 0px">
                                      <span data-toggle="tooltip" data-placement="top" title="Work Phone"><i class="fas fa-mobile-alt text-white btn-circle btn-sm mx-2 " style="background-color: #ff6a00"></i></span><span id="work" class="small text-gray-900" runat="server" title="Work Phone"></span>
                                  </div>
                                  <div class=" mr-2" style="padding: 10px 0px 0px 0px">
                                      <span data-toggle="tooltip" data-placement="top" title="Address"><i class="fas fa-address-book text-white btn-circle btn-sm mx-2 " style="background-color: #ff6a00"></i></span><span id="CustAddressIcon" class="small text-gray-900" runat="server" title="Address"></span>
                                  </div>
                                  <div class=" mr-2" style="padding: 10px 0px 0px 0px">
                                      <span data-toggle="tooltip" data-placement="top" title="Business Type"><i class="fas fa-home text-white btn-circle btn-sm mx-2 " style="background-color: #ff6a00"></i></span><span id="BusType" class="small text-gray-900" runat="server" title="Business Type"></span>
                                  </div>
                                  <hr />
                                  <span class="font-weight-bold"></span>

                                  <div class="accordion" id="accordionExample">
                                      <div class="card">
                                          <div class="card-header text-left bg-white" id="headingOne">
                                              <h5 class="mb-0 text-left">
                                                  <button class="btn btn-link" type="button" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                                      OTHER DETAILS
                                                  </button>
                                              </h5>
                                          </div>

                                          <div id="collapseOne" class="collapse " aria-labelledby="headingOne" data-parent="#accordionExample">
                                              <div class="card-body">
                                                  <div class=" mr-2 mb-2">
                                                      <span class="text-xs font-weight-bold text-primary text-uppercase">Company Name<b>:</b>  </span><b class="text-right"><span class="text-xs font-weight-bold text-primary text-uppercase" id="Span1" runat="server"></span></b>
                                                  </div>
                                                  <div class="mr-2 ">
                                                      <span class="text-xs font-weight-bold text-primary text-uppercase">Credit Limit<b>:</b>  </span><b><span class="text-xs font-weight-bold text-gray-900 text-uppercase" id="Span2" runat="server"></span>
                                                          <button runat="server" title="Update status" id="btn_line_chart" type="button" class="btn btn-sm btn-default " data-toggle="modal" data-target="#exampleModal">
                                                              <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Update Status">
                                                                  <div>
                                                                      <i class="fas fa-pencil-alt text-primary"></i>

                                                                  </div>
                                                              </a>
                                                          </button>
                                                      </b>
                                                  </div>
                                                  <div class="mr-2">
                                                      <span class="text-xs font-weight-bold text-primary text-uppercase">Contigency cash<b>:</b>  </span><b><span class="text-xs font-weight-bold text-gray-900 text-uppercase" id="Span3" runat="server"></span>
                                                          <button runat="server" title="Update status" id="Button5" type="button" class="btn btn-sm btn-default " data-toggle="modal" data-target="#exampleModal4">
                                                              <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Incure Contigency Cash.">
                                                                  <div>
                                                                      <i class="fas fa-pencil-alt text-primary"></i>

                                                                  </div>
                                                              </a>
                                                          </button>
                                                      </b>
                                                  </div>
                                                  <div class="mr-2">
                                                      <span class="text-xs font-weight-bold text-primary text-uppercase">Due Date<b>:</b>  </span><b><span class="text-xs font-weight-bold text-gray-900 text-uppercase" id="Span5" runat="server"></span>
                                                          <button runat="server" title="Update status" id="Button8" type="button" class="btn btn-sm btn-default btn-circle " data-toggle="modal" data-target="#exampleModal95">
                                                              <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Update the due date.">
                                                                  <div>
                                                                      <i class="fas fa-pencil-alt text-primary"></i>

                                                                  </div>
                                                              </a>
                                                          </button>
                                                      </b>
                                                  </div>
                                              </div>
                                          </div>
                                      </div>
                                  </div>

                                  <hr />
                                  <span class="font-weight-bold text-gray-900">SHOP DETAILS</span>
                                  <hr />
                                  <div class="shadow-sm">
                                      <div class="row">
                                          <div class="col-md-4">
                                              <center>
                                                  <span class="fas fa-home text-danger btn-circle border-bottom border-right border-top border-danger"></span>
                                                  <h6 class="text-xs font-weight-bold text-gray-500">Shop No.</h6>
                                                  <td class="text-right"><b class="text-right"><span class="text-right" id="shopno" runat="server"></span></b></td>
                                              </center>
                                          </div>
                                          <div class="col-md-4">
                                              <center>
                                                  <span class="fas fa-location-arrow text-success btn-circle border-bottom border-right border-top border-danger"></span>
                                                  <h6 class="text-xs font-weight-bold text-gray-500">Location</h6>
                                                  <td class="text-right"><b class="text-right"><span class="text-right" id="location" runat="server"></span></b></td>
                                              </center>
                                          </div>
                                          <div class="col-md-4">
                                              <center>
                                                  <span class="fas fa-dollar-sign text-warning btn-circle border-bottom border-right border-top border-danger"></span>
                                                  <h6 class="text-xs font-weight-bold text-gray-500">Price(M)</h6>
                                                  <td><span class="text-right text-gray-900" id="Rate" runat="server"></span></td>

                                              </center>
                                          </div>

                                      </div>
                                      <div class="row mt-5">

                                          <div class="col-md-4">
                                              <center>
                                                  <span class="fas fa-3x fa-hand-holding-usd text-primary btn-circle btn-lg border-bottom border-right border-top border-danger"></span>
                                                  <h6 class="text-xs font-weight-bold text-gray-500">SC(M)</h6>
                                                  <td><span class="text-right text-gray-900" id="Span4" runat="server"></span></td>
                                              </center>
                                          </div>
                                          <div class="col-md-4">
                                              <center>
                                                  <span class="fas fa-3x fa-user-cog text-primary btn-circle btn-lg border-bottom border-right border-top border-danger"></span>
                                                  <td colspan="4">
                                                      <h6 id="status1" runat="server" text="Label" class="small font-weight-bolder"></h6>
                                                  </td>
                                                  <td class="">
                                                      <asp:Button ID="Button3" class="bg-white btn btn-sm" runat="server" Text="✔" OnClick="Button3_Click" /></td>

                                              </center>
                                          </div>
                                          <div class="col-md-4">
                                              <center>
                                                  <span class="fas fa-3x fa-map text-primary btn-circle btn-lg border-bottom border-right border-top border-danger"></span>
                                                  <h6 class="text-xs font-weight-bold text-gray-500">Area</h6>

                                                  <td><span class="text-right text-gray-900" id="Area1" runat="server"></span>m<sup>2</sup></td>
                                              </center>
                                          </div>
                                      </div>
                                  </div>

                              </div>

                          </div>



                      </div>

                      <div class="col-lg-8">

                          <!-- Dropdown Card Example -->

                          <!-- Card Header - Dropdown -->
                          <div class="card-header bg-white ">
                              <div class="row">
                                  <div class="col-8 text-left">
                                      <span class="m-0 h6 font-weight-light text-gray-500">Receivable&Credit Limit</span><span id="duedate2" runat="server"></span>
                                  </div>
                                  <div class="col-4 text-right">
                                      <div class="dropdown no-arrow">
                                          <button class="btn btn-light btn-circle dropdown-toggle" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">

                                              <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Options">
                                                  <div>
                                                      <i class="fas fa-caret-down text-danger"></i>

                                                  </div>
                                              </a>

                                          </button>


                                          <div class="dropdown-menu  dropdown-menu-right shadow animated--fade-in" aria-labelledby="dropdownMenuLink">
                                              <div class="dropdown-header text-gray-900">Payment Option:</div>


                                              <a class="dropdown-item  text-gray-900  text-danger" id="A2" runat="server"><span class="fas fa-hand-holding-usd mr-2 " style="color: #d46fe8"></span>Create Invoice</a>
                                              <a class="dropdown-item text-gray-500  text-danger" id="A3" runat="server"><span class="fas fa-dollar-sign mr-2" style="color: #d46fe8"></span>Provide Full Credit</a>
                                              <div class="dropdown-header text-gray-900">Tenant:</div>
                                              <a class="dropdown-item text-gray-500  text-danger" id="A5" runat="server" href="#" data-toggle="modal" data-target="#exampleModal1111"><span class="fas fa-image mr-2 " style="color: #d46fe8"></span>Attach Image</a>
                                              <a class="dropdown-item text-gray-500  text-danger" id="A6" runat="server" href="#" data-toggle="modal" data-target="#exampleModalG"><span class="fas fa-user-clock mr-2 " style="color: #d46fe8"></span>Bind Gurantor Info</a>
                                              <a class="dropdown-item text-gray-500  text-danger" id="A7" runat="server" href="#" data-toggle="modal" data-target="#exampleModalD "><span class="fas fa-exclamation-triangle mr-2" style="color: #d46fe8"></span>Add delinquency</a>
                                              <a class="dropdown-item border-top text-gray-500 text-danger" id="A12" runat="server" href="#" data-toggle="modal" data-target="#exampleModalMisc"><span class="fas fa-money-bill mr-2 " style="color: #d46fe8"></span>Bind Credits</a>
                                              <a class="dropdown-item   text-danger text-gray-500" id="A14" runat="server" href="#" data-toggle="modal" data-target="#exampleModalSer"><span class="fas fa-recycle mr-2 " style="color: #d46fe8"></span>Update Service charge</a>
                                              <a class="dropdown-item   text-danger text-gray-500   text-danger" target="_blank" id="A9" runat="server"><span class="fas fa-list-ol mr-2 " style="color: #d46fe8"></span>Tenant PIR</a>
                                              <a class="dropdown-item   text-danger text-gray-500" id="A10" runat="server" href="#" data-toggle="modal" data-target="#exampleModalT"><span class="fas fa-info-circle mr-2 " style="color: #d46fe8"></span>Bind Tenant Info.</a>
                                              <a class="dropdown-item   text-danger text-gray-500" id="A1" runat="server" href="#" data-toggle="modal" data-target="#exampleModalDelete"><span class="fas fa-trash mr-2 " style="color: #d46fe8"></span>Remove Tenant</a>

                                              <div class="dropdown-header text-gray-900">shop:</div>
                                              <a class="dropdown-item   text-danger text-gray-500" id="A16" runat="server" href="#" data-toggle="modal" data-target="#exampleModalShopAreaChange"><span class="fas fa-desktop mr-2" style="color: #d46fe8"></span>Change shop area</a>
                                              <a class="dropdown-item   text-danger text-gray-500" id="A15" runat="server" href="#" data-toggle="modal" data-target="#exampleModalShopMerge"><span class="fas fa-joint mr-2 " style="color: #d46fe8"></span>Merge shop</a>
                                              <a class="dropdown-item   text-danger text-gray-500" id="A11" runat="server" href="#" data-toggle="modal" data-target="#exampleModalShop"><span class="fas fa-location-arrow mr-2" style="color: #d46fe8"></span>Transfer shop</a>
                                              <a class="dropdown-item   text-danger text-gray-500" id="A22" runat="server" href="#" data-toggle="modal" data-target="#ShopTransfer"><span class="fas fa-exchange-alt mr-2" style="color: #d46fe8"></span>Shop Exchange</a>


                                              <div class="dropdown-header text-gray-900">Letter:</div>

                                              <a class="dropdown-item   text-danger text-gray-500" target="_blank" id="A13" runat="server"><span class="fas fa-stamp mr-2 " style="color: #d46fe8"></span>Notice Letter</a>
                                              <a class="dropdown-item   text-danger text-gray-500" target="_blank" id="CustLink" runat="server"><span class="fas fa-paper-plane mr-2" style="color: #d46fe8"></span>Statement Letter</a>
                                              <a class="dropdown-item  text-danger text-gray-500" target="_blank" id="A18" runat="server"><span class="fas fa-coins mr-2 " style="color: #d46fe8"></span>Collection Letter</a>
                                              <a class="dropdown-item  text-danger text-gray-500" target="_blank" id="A19" runat="server"><span class="fas fa-hand-holding-heart mr-2 " style="color: #d46fe8"></span>Welcome Letter</a>
                                          </div>
                                      </div>
                                  </div>
                              </div>


                          </div>
                          <!-- Card Body -->
                          <div class="card-body">
                              <div class="row">

                                  <!-- Earnings (Monthly) Card Example -->
                                  <div class="col-xl-4 col-md-12 mb-1 border-right">

                                      <div class="card-body">
                                          <div class="row ">

                                              <div class="text-xs font-weight-bold text-primary text-uppercase mb-1 mr-2">Outstanding Receivable</div>
                                              <div class="h5 mb-0 font-weight-light  text-gray-800">
                                                  <span id="Span9" runat="server"></span>

                                                  <a class="dropdown-toggle btn-sm text-warning font-weight-bolder" href="#" role="button" id="dropdownMenuLink2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></a>
                                                  <div class="dropdown-menu  dropdown-menu-left shadow animated--fade-in w-100 text-center" style="width: 200px" aria-labelledby="dropdownMenuLink2">
                                                      <div class="dropdown-header  font-weight-bolder text-primary text-uppercase">Aged Receivable</div>
                                                      <div class="dropdown-divider"></div>
                                                      <div class="row">
                                                          <div class="col-md-6 text-left">
                                                              <i class="fas fa-arrow-circle-right mx-3 text-primary"></i>1-30 DAYS
                                                          </div>
                                                          <div class="col-md-4 text-right mr-2">
                                                              <span id="one" runat="server" class="text-right">0.00</span>
                                                          </div>
                                                      </div>
                                                      <div class="dropdown-divider"></div>
                                                      <div class="row">
                                                          <div class="col-md-6 text-left">
                                                              <i class="fas fa-arrow-circle-right  mx-3 text-primary"></i>31-60 DAYS
                                                          </div>
                                                          <div class="col-md-4 text-right mr-2">
                                                              <span id="two" runat="server" class="text-right">0.00</span>
                                                          </div>
                                                      </div>

                                                      <div class="dropdown-divider"></div>
                                                      <div class="row">
                                                          <div class="col-md-6 text-left">
                                                              <i class="fas fa-arrow-circle-right  mx-3 text-primary"></i><b>></b> 61 DAYS
                                                          </div>
                                                          <div class="col-md-4 text-right mr-2">
                                                              <span id="three" runat="server" class="text-right">0.00</span>
                                                          </div>
                                                      </div>
                                                      <hr />
                                                      <span class="fas fa-dollar-sign   mr-2 text-primary"></span><a id="A21" target="_blank" href="#" data-toggle="modal" data-target="#modalTour" runat="server" class="text-primary mb-2">Analysis</a>

                                                      <hr />
                                                      <span class="fas fa-arrow-circle-right mr-1 text-danger"></span><a id="A20" target="_blank" runat="server" class="text-danger mb-2">Generate Collection Letter</a>
                                                      <hr />
                                                      <span class="fas  fa-location-arrow  mr-2 text-gray-500"></span><a id="A17" target="_blank" href="#" data-toggle="modal" data-target="#exampleModalSMS" runat="server" class="text-gray-500 mb-2">Alert Customer

                                                      </a>
                                                  </div>


                                              </div>

                                              <div class="col-auto text-right">
                                                  <i class="fas fa-dollar-sign  mx-2 fa-1x text-gray-300"></i>

                                              </div>
                                          </div>
                                      </div>

                                  </div>
                                  <!-- Earnings (Monthly) Card Example -->
                                  <div class="col-xl-4 col-md-6 mb-1 border-right">

                                      <div class="card-body">
                                          <div class="row no-gutters align-items-center">
                                              <div class="col mr-2">
                                                  <div class="text-xs font-weight-bold text-success text-uppercase mb-1">Unused Credit</div>
                                                  <div class="h5 mb-0 font-weight-light  text-gray-800"><span id="Credit" runat="server"></span></div>
                                              </div>
                                              <div class="col-auto">
                                                  <i class="fas fa-money-bill fa-1x text-gray-300"></i>
                                              </div>
                                          </div>
                                      </div>

                                  </div>
                                  <div class="col-xl-4 col-md-6 mb-1">

                                      <div class="card-body">
                                          <div class="row no-gutters align-items-center">
                                              <div class="col mr-2">
                                                  <div class="text-xs font-weight-bold text-danger text-uppercase mb-1">Due Amount</div>
                                                  <div class="h5 mb-0 font-weight-light  text-gray-800"><span id="TotalReceivable" runat="server"></span></div>
                                              </div>
                                              <div class="col-auto">
                                                  <i class="fas fa-hand-holding-usd fa-1x text-gray-300"></i>
                                              </div>
                                          </div>
                                      </div>

                                  </div>


                                  <!-- Earnings (Monthly) Card Example -->

                              </div>
                          </div>







                          <div class="card-header bg-white border-top  ">
                              <div class="row ">
                                  <div class=" col-8 text-left">
                                      <span class="m-0 font-weight-bold text-primary text-xs">Payment Patterns<span class="text-muted text-uppercase mx-2 text-gray-400 mr-1 small">(Thousands)</span> <span id="datFromc" runat="server" visible="false" class="mb-1 small font-italic text-danger"></span><span id="tomiddlec" visible="false" class="mb-1 mr-2 ml-2 mb-1 small font-italic text-danger" runat="server">-</span><span id="datToc" visible="false" class="mb-1 small font-italic text-danger" runat="server"></span></span>

                                  </div>
                                  <div class=" col-4 text-right">
                                      <button type="button" data-toggle="modal" data-target="#CashSummary" class="btn mt-1 mb-1 btn-circle btn-sm btn-warning"><span data-toggle="tooltip" title="Bind data based on date range" class="fas fa-calendar-week text-white"></span></button>

                                  </div>
                              </div>
                          </div>

                          <!-- Card Content - Collapse -->
                          <div class="card-body">
                              <div class="row">
                                  <div class="col-9">
                                      <main role="main" id="main" runat="server">

                                          <div class="starter-template">
                                              <center>


                                                  <p class="lead">

                                                      <i class="fas fa-trash-restore text-gray-300  fa-3x"></i>

                                                  </p>
                                                  <h6 class="text-gray-300" style="font-weight: bold">Sorry!! Nothing to show here.</h6>
                                              </center>
                                          </div>



                                      </main>
                                      <asp:Literal ID="ltChart" runat="server"></asp:Literal>
                                  </div>
                                  <div class="col-3 small text-left border-left">


                                      <span><i class="fas fa-donate text-white btn-circle btn-sm mr-2" style="background-color: #a20fb2"></i></span><span class="text-gray-900 font-weight-bold small" id="ShopFree" runat="server">0</span>
                                      <h6 class="text-muted small">#Invoiced</h6>
                                      <span><i class="fas fa-dollar-sign text-white btn-circle btn-sm mr-2" style="background-color: #10e469"></i></span><span class="text-gray-900 font-weight-bold small" id="ShopCustomer" runat="server">0</span>
                                      <h6 class="text-muted small">#Paid</h6>
                                      <div class="border-bottom mb-2"></div>
                                      <span><i class="fas fa-donate text-white btn-circle btn-sm mr-2" style="background-color: #ff6a00"></i></span><span class="text-gray-900 font-weight-bold small" id="ShopPercentage_Occupy" runat="server">0.00</span>
                                      <br />
                                      <span class="text-muted small">Balance</span><br />
                                  </div>
                              </div>



                          </div>

                          <div class="row mt-2 border-top mt-lg-5">

                              <!-- Earnings (Monthly) Card Example -->
                              <div class="col-xl-6 col-md-6 mb-1 border-right">

                                  <div class="card-body">
                                      <div class="row no-gutters align-items-center">
                                          <div class="col mr-2">
                                              <div class="row">
                                                  <div class="col-8 text-left">
                                                      <div class="text-xs font-weight-bold text-primary text-uppercase mb-3">Agreement Due Date</div>

                                                  </div>
                                                  <div class="col-4 text-right">
                                                  </div>
                                              </div>
                                              <div class="h5 mb-0 font-weight-light  text-gray-800">
                                                  <span id="agreemd" runat="server"></span>
                                                  <button runat="server" title="Update status" id="Button6" type="button" class="btn btn-sm btn-circle btn-default " data-toggle="modal" data-target="#exampleModal9">
                                                      <div>
                                                          <i class="fas fa-pencil-alt text-danger"></i>

                                                      </div>
                                                  </button>
                                              </div>
                                              <h6 id="agredate" runat="server"></h6>
                                          </div>
                                          <div class="col-auto">
                                              <i class="fas fa-calendar-check fa-2x text-gray-300"></i>

                                          </div>
                                      </div>
                                  </div>

                              </div>

                              <!-- Earnings (Monthly) Card Example -->
                              <div class="col-xl-6 col-md-6 mb-1">

                                  <div class="card-body">
                                      <div class="row no-gutters align-items-center">
                                          <div class="col mr-2">
                                              <div class="text-xs font-weight-bold text-danger text-uppercase mb-3">Terms</div>
                                              <div class="h5 mb-0 font-weight-light  text-gray-800">
                                                  <span id="termsda" runat="server"></span>
                                                  <button runat="server" title="Update status" id="Button7" type="button" class="btn btn-sm btn-circle btn-default " data-toggle="modal" data-target="#exampleModal8">
                                                      <div>
                                                          <i class="fas fa-pencil-alt text-danger"></i>

                                                      </div>
                                                  </button>
                                              </div>
                                          </div>
                                          <div class="col-auto">
                                              <i class="fas fa-calendar-week fa-2x text-gray-300"></i>
                                          </div>
                                      </div>
                                  </div>

                              </div>



                              <!-- Earnings (Monthly) Card Example -->

                          </div>
                          <div class="row  border-top mt-lg-5">

                              <!-- Earnings (Monthly) Card Example -->
                              <div class="col-xl-4 col-md-6 mb-1 border-right">

                                  <div class="card-body">
                                      <div class="row no-gutters align-items-center">
                                          <div class="col mr-2">
                                              <div class="text-xs font-weight-bold text-gray-600 text-uppercase mb-3">Guarantor Full Name</div>
                                              <div class="small mb-0 font-weight-light  text-gray-800">
                                                  <span id="Gurantorfullname" runat="server"></span>
                                              </div>
                                              <h6 id="H1" runat="server"></h6>
                                          </div>
                                          <div class="col-auto">
                                              <i class="fas fa-user fa-2x text-gray-300"></i>

                                          </div>
                                      </div>
                                  </div>

                              </div>

                              <!-- Earnings (Monthly) Card Example -->
                              <div class="col-xl-4 col-md-6 mb-1 border-right">

                                  <div class="card-body">
                                      <div class="row no-gutters align-items-center">
                                          <div class="col mr-2">
                                              <div class="text-xs font-weight-bold text-gray-600 text-uppercase mb-3">Address</div>
                                              <div class="small mb-0 font-weight-light  text-gray-800">
                                                  <span id="GurantorAddress" runat="server"></span>
                                              </div>
                                          </div>
                                          <div class="col-auto">
                                              <i class="fas fa-map-marker-alt fa-2x text-gray-300"></i>
                                          </div>
                                      </div>
                                  </div>

                              </div>

                              <div class="col-xl-4 col-md-6 mb-1">

                                  <div class="card-body">
                                      <div class="row no-gutters align-items-center">
                                          <div class="col mr-2">
                                              <div class="text-xs font-weight-bold text-gray-600 text-uppercase mb-3">Contact</div>
                                              <div class="small mb-0 font-weight-light  text-gray-800">
                                                  <span id="GurantorMobile" runat="server"></span>
                                              </div>
                                          </div>
                                          <div class="col-auto">
                                              <i class="fas fa-mobile-alt fa-2x text-gray-300"></i>
                                          </div>
                                      </div>
                                  </div>

                              </div>

                              <!-- Earnings (Monthly) Card Example -->

                          </div>
                          <div class="row  border-top mt-lg-5">

                              <!-- Earnings (Monthly) Card Example -->
                              <div class="col-xl-12 col-md-12 mb-1 ">

                                  <div class="card-body">
                                      <div class="row no-gutters ">

                                          <div class="col mr-2">
                                              <div class="text-xs font-weight-bold text-gray-600 text-uppercase mb-3">Delinquency</div>
                                              <div class="small mb-0 font-weight-light  text-gray-800">
                                                  <span id="Span7" runat="server"></span>
                                              </div>
                                          </div>
                                      </div>
                                      <div class="row">
                                          <div class="card-body">
                                              <div style="overflow-y: scroll; height: 450px" class="mb-3 border-bottom" id="delinquecyDiv" runat="server">
                                                  <asp:Repeater ID="Repeater4" runat="server">
                                                      <itemtemplate>

                                                          <ul class="timeline">

                                                              <li>

                                                                  <p>
                                                                      <asp:Label ID="Label5" class="small text-gray-900 font-weight-bold" runat="server"><%#Eval("delinquency")%></asp:Label>
                                                                  </p
                                                                  <span class="fas fa-edit text-gray-500"></span><span class="text-xs mx-1 font-italic text-gray-900"><%#Eval("datetime","{0: MMMM dd, yyyy}")%></span>
                                                              </li>
                                                          </ul>

                                                      </itemtemplate>
                                                  </asp:Repeater>
                                              </div>
                                             
                                              <center>

                                                  <main role="main" id="maind" runat="server">

                                                      <div class="starter-template">
                                                          <center>


                                                              <p class="lead">

                                                                  <i class="fas fa-exclamation-circle text-gray-300  fa-3x"></i>

                                                              </p>
                                                              <h6 class="text-gray-300" style="font-weight: bold">Sorry!! Nothing to show here.</h6>
                                                          </center>
                                                      </div>
                                                  </main>
                                              </center>
                                          </div>
                                      </div>
                                  </div>
                              </div>

                          </div>
                      </div>

                  </div>
              </div>
          </div>
          </div>
    </div>
    <div class="modal fade" id="ShopTransfer" tabindex="-1" role="dialog" aria-labelledby="exampleTransfer" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title text-gray-900 font-weight-bolder" id="exampleTransfer"><span class="fas fa-exchange-alt mr-2" style="color:#ff00bb"></span>Exchange Shop</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row mb-3">
                        <div class="col-md-12">

                            <span class="fas fa-arrow-circle-right text-danger mr-2"></span><label class="text-danger small">Select Customer to be Exchanged</label>
                            <asp:DropDownList ID="ddlExchangedShop" class="form-control form-control-sm" ClientIDMode="Static" runat="server">
                            </asp:DropDownList>

                            <br />
                            <asp:Label runat="server" class="small mt-2 mx-1" Style="color: #ff00bb" ID="lblSelectedText" ClientIDMode="Static"></asp:Label>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-1">
                            <div id="myDIV55" class="spinner-border text-danger spinner-border-sm  mr-4 mt-2" role="status">
                                <span class="sr-only">Loading.ffrfyyrg..</span>
                            </div>
                        </div>
                        <div class="col-md-12">

                            <center>
                                <asp:Button ID="btnExchangeShop"  runat="server" class="btn btn-sm btn-danger w-100" Text="Exchange shops..." OnClick="btnExchangeShop_Click" />
                            </center>


                        </div>
                    </div>
                </div>
                <center>
                    <div class="modal-footer">
                    </div>

                </center>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            //We are binding onchange event using jQuery built-in change event
            $('#ddlExchangedShop').change(function () {
                //get selected value and check if subject is selected else show alert box
                var SelectedValue = $("#ddlExchangedShop").val();
                if (SelectedValue > 0) {
                    //get selected text and set to label
                    let SelectedText = $("#ddlExchangedShop option:selected").val();

                    lblSelectedText.innerHTML = "Shop No " + SelectedText;
                    //set selected value to label
                } else {
                    //reset label values and show alert
                    lblSelectedText.innerHTML = "";
                }
            });
        });
    </script>

    <div class="modal fade" id="exampleModalShopAreaChange" tabindex="-1" role="dialog" aria-labelledby="exampleModalShopAreaChange" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title text-gray-900 font-weight-bolder" id="exampleModalLabel8shopM1">Change Shop Area</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row mb-3">
                        <div class="col-md-12">

                            <label class="text-gray-900">Area to be expanded or reduced</label>
                            <asp:TextBox ID="txtAreaExpand" placeholder="eg. +50 or  -50" runat="server" class=" form-control form-control-sm"></asp:TextBox>
                        </div>
                    </div>
                    <p class="text-danger small">*If the area is increased or decreased from another shop, select the shop below</p>
                    <div class="row mb-3">
                        <div class="col-md-12">

                            <label class="text-gray-900">Select Shop</label>
                            <asp:DropDownList ID="ddlSHopExpand" class=" form-control form-control-sm" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="row">

                        <div class="col-md-12">

                            <center>
                                <asp:Button ID="btnExpandArea" runat="server" class="btn btn-sm btn-success w-100" Text="Save..." OnClick="btnExpandArea_Click" />
                            </center>


                        </div>
                    </div>
                </div>
                <center>
                    <div class="modal-footer">
                    </div>

                </center>
            </div>
        </div>
    </div>
    <div class="modal fade" tabindex="-1" role="dialog" id="modalTour">
        <div class="modal-dialog" role="document">
            <div class="modal-content rounded-bottom ">
                <div class="modal-body p-5">
                    <h2 class="fw-bold font-weight-bold text-gray-900 mb-0">Want analysis?</h2>

                    <ul class="d-grid gap-4 my-5 list-unstyled">
                        <li class="d-flex gap-4 mt-4">
                            <span class="fas fa-2x fa-chart-line text-success mr-2"></span>
                            <div>
                                <h5 class="mb-0 text-gray-900 font-weight-bold">Bad Debt Estimation</h5>
                                <span class=""></span><span class="small text-gray-900" style="justify-content">Under the percentage-of-receivables basis,
it is estimated what percentage of receivables will result in losses from
uncollectible accounts.</span>
                                <br />
                                <span class="fas fa-arrow-alt-circle-right text-gray-400"></span><span class="small text-success">[2%x]</span><span class="text-gray-400 small ml-2">Not Due</span>
                                <span class="text-gray-900 small ml-2" id="NotDue" runat="server">0.00</span>
                                <br />
                                <span class="fas fa-arrow-alt-circle-right text-gray-400"></span><span class="small text-warning">[4%x]</span><span class="text-gray-400 small ml-2">1-30</span>
                                <span class="text-gray-900 small text-right ml-2" id="Aged30" runat="server">0.00</span>
                                <br />
                                <span class="fas fa-arrow-alt-circle-right text-gray-400"></span><span class="small text-danger">[10%x]</span><span class="text-gray-400 small ml-2">31-60</span>
                                <span class="text-gray-900 small text-right ml-2" id="Aged60" runat="server">0.00</span>
                                <br />
                                <span class="fas fa-arrow-alt-circle-right text-gray-400"></span><span class="small text-gray-900">[20%x]</span><span class="text-gray-400 small ml-2">61-90</span>
                                <span class="text-gray-900 small text-right ml-2" id="Aged90" runat="server">0.00</span>
                                <br />
                                <span class="fas fa-arrow-alt-circle-right text-gray-400"></span><span class="small text-gray-900">[40%x]</span><span class="text-gray-400 small ml-2">> 90 Days</span>
                                <span class="text-gray-900 small text-right ml-2" id="AgedGr90" runat="server">0.00</span>
                                <hr />
                                <span class="text-gray-900 h6 text-uppercase mr-5 font-weight-bold">Total BAD DEBT [ETB]</span><span class="text-gray-900 h6 text-uppercase mr-5" id="TotalBadEstimated" runat="server">0.00 ETB</span>
                                <hr />
                            </div>
                        </li>
                        <li class="d-flex gap-4 mt-4">
                            <span class="fas fa-2x fa-chart-line text-secondary mr-2"></span>
                            <div>
                                <h5 class="mb-0 text-gray-900 font-weight-bold">Write off amount <span class="text-danger">[≥260 Days]</span></h5>
                                <span class="text-gray-900 small">Amount of recievable that past due 260 days considered as compeletely uncollectible.</span>
                                <hr />
                                <span class="text-gray-900 h6 text-uppercase mr-5 font-weight-bold">Total write off [ETB]</span><span class="text-gray-900 h6 text-uppercase mr-5" id="TotalWriteOff" runat="server">0.00 ETB</span>

                                <hr />
                            </div>
                        </li>
                    </ul>
                    <button type="button" class="btn btn-lg btn-link  mt-5 w-100" data-dismiss="modal">Great, thanks!</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="exampleModalSMS" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel8shopSMS" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content rounded-4 shadow">
                <div class="modal-body p-4 text-center">
                    <h5 class="mb-0 text-gray-800 font-weight-bold">Send Alert SMS?</h5>
                    <span class="fas fa-edit text-gray-600 mr-2"></span><span class="text-gray-600 small">Dear customer you have credit amount of <span id="CA" class="text-gray-900 font-weight-bold small mr-1" runat="server">0.00</span> Please pay the credit soon.</span>
                    <center>
                        <div id="myDIV555" class="spinner-border text-danger spinner-border-sm  mr-4 mt-2" role="status">
                            <span class="sr-only">Loading.ffrfyyrg..</span>
                        </div>
                    </center>


                </div>
                <div class="modal-footer flex-nowrap p-0">

                    <asp:Button ID="btnSendAlert" runat="server" class="btn btn-lg font-weight-bold  btn-link fs-6 text-decoration-none col-6 m-0 rounded-0 border-right" Text="Yes, send" OnClick="btnSendAlert_Click" OnClientClick="myFunctionSMS()" />

                    <button type="button" class="btn btn-lg btn-link fs-6 text-decoration-none col-6 m-0 rounded-0" data-dismiss="modal">No thanks</button>
                </div>
            </div>
        </div>

    </div>
    <div class="modal fade" id="exampleModalShopMerge" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel8shopM" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title text-gray-900 font-weight-bolder" id="exampleModalLabel8shopM">Merge Shop</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row mb-3">
                        <div class="col-md-12">

                            <label class="text-gray-900">Select Shop to be merged</label>
                            <asp:DropDownList ID="ddlMergedShop" class="form-control form-control-sm" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-1">
                            <div id="myDIV551" class="spinner-border text-danger spinner-border-sm  mr-4 mt-2" style="display:none" role="status">
                                <span class="sr-only">Loading.ffrfyyrg..</span>
                            </div>
                        </div>
                        <div class="col-md-11">

                            <center>
                                <asp:Button ID="btnMergeShop" runat="server" class="btn btn-sm btn-danger w-100" Text="Merge shops..." OnClick="btnMergeShop_Click" OnClientClick="myFunctionshop11()" />
                            </center>


                        </div>
                    </div>
                </div>
                <center>
                    <div class="modal-footer">
                    </div>

                </center>
            </div>
        </div>
    </div>
    <div class="modal fade" id="exampleModalShop" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel8shop" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title text-gray-900 font-weight-bolder" id="exampleModalLabel8shop">Transfer from current shop to..</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row mb-3">
                        <div class="col-md-12">
                            <p class="small text-danger">All properties of the seleted shop  will be binded to the current customer. Make sure you select the right shop.</p>
                            <label class="text-gray-900">Select Shop</label>
                            <asp:DropDownList ID="ddlShops" class="form-control form-control-sm" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-1">
                            <div id="myDIV5" class="spinner-border text-danger spinner-border-sm  mr-4 mt-2" role="status">
                                <span class="sr-only">Loading.ffrfyyrg..</span>
                            </div>
                        </div>
                        <div class="col-md-11">

                            <center>
                                <asp:Button ID="btnTransferSHOP" runat="server" class="btn btn-sm btn-success w-100" Text="Transfer" OnClick="btnTransferSHOP_Click" OnClientClick="myFunctionshop()" />
                            </center>


                        </div>
                    </div>
                </div>
                <center>
                    <div class="modal-footer">
                    </div>

                </center>
            </div>
        </div>
    </div>
    <script>
        function myFunctionshop() {
            var x = document.getElementById("myDIV5");
            if (x.style.display === "none") {
                x.style.display = "block";
            } else {
                x.style.display = "none";
            }
        }
    </script>
    <script>
        function myFunctionSMS() {
            var x = document.getElementById("myDIV555");
            if (x.style.display === "none") {
                x.style.display = "block";
            } else {
                x.style.display = "none";
            }
        }
    </script>
    <script>
        function myFunctionshop1() {
            var x = document.getElementById("myDIV55");
            if (x.style.display === "none") {
                x.style.display = "block";
            } else {
                x.style.display = "none";
            }
        }
        function myFunctionshop11() {
            var x = document.getElementById("myDIV551");
            if (x.style.display === "none") {
                x.style.display = "block";
            } else {
                x.style.display = "none";
            }
        }
    </script>
    <div class="modal fade" id="exampleModalDelete" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabelT" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title text-gray-900" id="exampleModalLabelT"><span class="fas fa-trash mr-2" style="color:#ff00bb"></span>Remove Tenant</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-12">
                            <span class="fas fa-arrow-circle-right text-danger mr-3"></span><span class="text-danger small text-center">All the data associated with the customer will be permanently removed</span>
                            <asp:Button ID="btnRemoveCustomer" runat="server" class="btn btn-danger mt-3 btn-sm w-100" Text="Remove" OnClick="btnRemoveCustomer_Click" />

                        </div>
                    </div>

                </div>

                <center>
                    <div class="modal-footer">
                    </div>

                </center>
            </div>
        </div>
    </div>
    <div class="modal fade" id="exampleModalT" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabelT" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title text-gray-900" id="exampleModalLabelT">Bind Tenant Info</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">

                    <div class="row mb-3">
                        <div class="col-md-12 ">
                            <asp:TextBox ID="txtCustomerName" class="form-control form-control-sm" placeholder="Update Customer Name" runat="server" data-toggle="tooltip" data-placement="top" title="Customer Name"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-12 ">
                            <asp:TextBox ID="txtAddress" class="form-control form-control-sm" placeholder="Update Customer Address" runat="server" Style="border-color: #ff6a00" data-toggle="tooltip" data-placement="top" title="Address"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-12 ">
                            <asp:TextBox ID="txtBusinessType" class="form-control form-control-sm" placeholder="Business type" runat="server" data-toggle="tooltip" data-placement="top" title="Business Type"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-12 ">
                            <asp:TextBox ID="txtContigency" data-toggle="tooltip" title="Contingency cash" class="form-control form-control-sm" placeholder="Contingency" runat="server" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-12 ">
                            <asp:TextBox ID="txtDateofJoiningUpdate" data-toggle="tooltip" title="Customer joining date" TextMode="Date" class="form-control form-control-sm" placeholder="Date of Joining" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-12 ">
                            <asp:TextBox ID="txtTINNumber" data-toggle="tooltip" title="TIN Number" Style="border-color: #ff0000" class="form-control form-control-sm" placeholder="TIN#" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-12 ">
                            <asp:TextBox ID="txtVatRegNumber" data-toggle="tooltip" title="Vat Reg. Number" Style="border-color: #ff0000" class="form-control form-control-sm" placeholder="Vat Reg. Number" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-12 ">
                            <asp:TextBox ID="txtEmail" class="form-control form-control-sm" placeholder="Email" runat="server" data-toggle="tooltip" data-placement="top" title="Email"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row  mb-3">
                        <div class="col-md-12">
                            <asp:TextBox ID="txtTenantComoany" class="form-control form-control-sm" placeholder="Company Name" runat="server" data-toggle="tooltip" data-placement="top" title="Company Name"></asp:TextBox>

                        </div>
                    </div>
                    <div class="row  mb-3">
                        <div class="col-md-12">
                            <asp:TextBox ID="txtTenantWebsites" class="form-control form-control-sm" runat="server" placeholder="Websites" data-toggle="tooltip" data-placement="top" title="Website"></asp:TextBox>

                        </div>
                    </div>
                    <div class="row  mb-3">
                        <div class="col-md-12">
                            <asp:TextBox ID="txtAmharic_name" class="form-control form-control-sm" runat="server" placeholder="amharic name" data-toggle="tooltip" data-placement="top" title="Amharic Name"></asp:TextBox>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Button ID="Button18" runat="server" class="btn btn-danger btn-sm w-100" Text="Save" OnClick="Button18_Click" />

                        </div>
                    </div>

                </div>

                <center>
                    <div class="modal-footer">
                    </div>

                </center>
            </div>
        </div>
    </div>

    <div class="modal fade" id="exampleModalMisc" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabelD1" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title text-gray-900 font-weight-bold h5" id="exampleModalLabelD1">Add Credit</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row mb-3">
                        <div class="col-md-12 ">
                            <asp:TextBox ID="txtCost" class="form-control form-control-sm" placeholder="Credit Amount" runat="server"></asp:TextBox>

                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-12 ">
                            <asp:TextBox ID="txtCostRemark" class="form-control form-control-sm" TextMode="MultiLine" Height="100px" placeholder="Remark/Reference" runat="server"></asp:TextBox>

                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-md-12 ">
                            <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                <input type="checkbox" class="custom-control-input" id="Checkbox2" runat="server" clientidmode="Static" />
                                <label class="custom-control-label text-xs text-gray-900" for="Checkbox2">Recognize as Revenue & Receivable (Accrual Basis)</label>
                            </div>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Button ID="btnMiscCost" runat="server" class="btn btn-sm w-100 btn-danger" Text="Save" OnClick="btnMiscCost_Click" />

                        </div>
                    </div>

                </div>

                <center>
                    <div class="modal-footer">
                    </div>

                </center>
            </div>
        </div>
    </div>

    <div class="modal fade" id="exampleModalD" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabelD" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title text-gray-900" id="exampleModalLabelD">Bind Customer Delinquency</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row mb-3">
                        <div class="col-md-12 ">
                            <asp:TextBox ID="txtDelinquency" class="form-control form-control-sm" Height="250px" TextMode="MultiLine" placeholder="Here by, the customer held accounted for making a payment passing his/her due date..." runat="server"></asp:TextBox>

                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">

                        </div>
                    </div>

                </div>

                <center>
                    <div class="modal-footer">
                        <asp:Button ID="Button17" runat="server" class="btn btn-sm text-white" style="background-color:#d46fe8" Text="Save Delinquency" OnClick="Button17_Click" />

                    </div>

                </center>
            </div>
        </div>
    </div>
    <div class="modal fade" id="exampleModalG" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabelG" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title text-gray-900" id="exampleModalLabelG">Bind Gurantor Info</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row mb-3">
                        <div class="col-md-12 ">
                            <asp:TextBox ID="txtGFullName" class="form-control " placeholder="Gurantor Full Name" runat="server"></asp:TextBox>

                        </div>
                    </div>
                    <div class="row  mb-3">
                        <div class="col-md-12">
                            <asp:TextBox ID="txtGAdress" class="form-control " placeholder="Address" runat="server"></asp:TextBox>

                        </div>
                    </div>
                    <div class="row  mb-3">
                        <div class="col-md-12">
                            <asp:TextBox ID="txtContact" class="form-control " runat="server" placeholder="Contact"></asp:TextBox>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Button ID="Button16" runat="server" class="btn btn-primary" Text="Save" OnClick="Button16_Click" />

                        </div>
                    </div>

                </div>

                <center>
                    <div class="modal-footer">
                    </div>

                </center>
            </div>
        </div>
    </div>
    <div class="modal fade" id="exampleModalSer" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title text-gray-900 font-weight-bold" id="exampleModalLabelSer">Service Charge Update</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-7">
                            <asp:TextBox ID="txtServiceCharge" class="form-control mx-2" runat="server"></asp:TextBox>

                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnUpdateServiceCharge" runat="server" class="btn btn-danger" Text="Save" OnClick="btnUpdateServiceCharge_Click" />

                        </div>
                    </div>
                </div>
                <center>
                    <div class="modal-footer">
                    </div>

                </center>
            </div>
        </div>
    </div>

    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title text-gray-900" id="exampleModalLabel">Update Credit Limit</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-7">
                            <asp:TextBox ID="txtcreditlimit" class="form-control mx-2" runat="server"></asp:TextBox>

                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Save" OnClick="Button1_Click" />

                        </div>
                    </div>
                </div>
                <center>
                    <div class="modal-footer">
                    </div>

                </center>
            </div>
        </div>
    </div>
    <div class="modal fade" id="exampleModal8" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel8" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title text-gray-900" id="exampleModalLabel8">Update Billing Terms</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row mb-3">
                        <div class="col-md-12">
                            <p class="small text-danger">Billing terms updates is possible only when the customer reaches its final due date. Make sure before updating the terms.</p>
                            <label class="text-gray-900">Select billibg terms</label>
                            <asp:DropDownList ID="ddlBillingTerms" class="form-control" runat="server">
                                <asp:ListItem>Monthly</asp:ListItem>
                                <asp:ListItem>Every Three Month</asp:ListItem>
                                <asp:ListItem>Every Six Month</asp:ListItem>
                                <asp:ListItem>Yearly</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">


                                <div class="custom-control custom-checkbox font-weight-300">
                                    <input type="checkbox" class="custom-control-input" id="Checkbox12" runat="server" clientidmode="Static" />
                                    <label class="custom-control-label text-gray-900 " for="Checkbox12">Include SMS Notification</label>
                                </div>

                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <center>
                                <asp:Button ID="btnBillingTerms" runat="server" class="btn btn-sm btn-light" OnClick="btnBillingTerms_Click" Text="Update" />
                            </center>


                        </div>
                    </div>
                </div>
                <center>
                    <div class="modal-footer">
                    </div>

                </center>
            </div>
        </div>
    </div>

    <div class="modal fade" id="exampleModal95" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel95" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title text-gray-900" id="exampleModalLabel95">Update Due Date</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row mb-3">
                        <div class="col-md-12">
                            <label class="text-gray-900">Select Date</label>
                            <asp:TextBox ID="txtDueDateUpdate" TextMode="Date" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <center>
                                <asp:Button ID="Button9" runat="server" class="btn btn-sm btn-light" OnClick="Button9_Click" Text="Update" />
                            </center>


                        </div>
                    </div>
                </div>
                <center>
                    <div class="modal-footer">
                    </div>

                </center>
            </div>
        </div>
    </div>

    <div class="modal fade" id="exampleModal1111" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel1111" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title text-gray-900" id="exampleModalLabel1111">Upload Profile</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row mb-3">
                        <div class="col-md-12">
                            <label class="text-gray-900">ID Card Front</label>
                            <asp:FileUpload ID="FileUpload2" class="form-control-sm" runat="server" />
                        </div>


                    </div>
                    <div class="row mb-3">
                        <div class="col-md-12">
                            <label class="text-gray-900">ID Card Back</label>
                            <asp:FileUpload ID="FileUpload3" class="form-control-sm" runat="server" />
                        </div>

                    </div>
                    <div class="row mb-3">
                        <div class="col-md-12">
                            <label class="text-gray-900">Gurantor ID Card Front Only (If any)</label>
                            <asp:FileUpload ID="FileUpload4" class="form-control-sm" runat="server" />
                        </div>

                    </div>
                    <div class="col-md-12">

                        <asp:Button ID="Button15" runat="server" Text="Save" OnClick="Button15_Click" class="btn btn-block btn-sm btn-warning" />

                    </div>
                </div>
                <center>
                    <div class="modal-footer">
                    </div>

                </center>
            </div>
        </div>
    </div>
    <div class="modal fade" id="exampleModal9v" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel9v" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title font-weight-bolder text-gray-900" id="exampleModalLabel9v">Put Your Message Below</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row mb-3">
                        <div class="col-md-12">

                            <asp:TextBox ID="txtMessage" TextMode="MultiLine" placeholder="Messages" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <center>
                                <asp:Button ID="Button14" runat="server" class="btn btn-sm btn-light" OnClick="Button14_Click" Text="Broadcast" />
                            </center>


                        </div>
                    </div>
                </div>
                <center>
                    <div class="modal-footer">
                    </div>

                </center>
            </div>
        </div>
    </div>

    <div class="modal fade" id="exampleModal9" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel9" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title text-gray-900" id="exampleModalLabel9">Renew Agreement</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row mb-3">
                        <div class="col-md-12">
                            <label class="text-gray-900">Select Date</label>
                            <asp:TextBox ID="txtAgreementDate" TextMode="Date" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <center>
                                <asp:Button ID="btnAgreement" runat="server" class="btn btn-sm btn-light" OnClick="btnAgreement_Click" Text="Update" />
                            </center>


                        </div>
                    </div>
                </div>
                <center>
                    <div class="modal-footer">
                    </div>

                </center>
            </div>
        </div>
    </div>
    <div class="modal fade" id="exampleModal111" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel111" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title text-gray-900" id="exampleModalLabel111">Upload Profile</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row mb-3">
                        <div class="col-md-5">
                            <label class="text-gray-900">Select Image</label>
                            <asp:FileUpload ID="FileUpload1" class="form-control-sm" runat="server" />
                        </div>

                    </div>
                    <div class="col-md-5">

                        <asp:Button ID="Button13" runat="server" Text="Save" OnClick="Button2_Click1" class="btn btn-block btn-sm btn-warning" />

                    </div>
                </div>
                <center>
                    <div class="modal-footer">
                    </div>

                </center>
            </div>
        </div>
    </div>
    <div class="modal fade" id="exampleModal4" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel4" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title text-gray-900" id="exampleModalLabel4">Incure Contigency</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row mb-3">
                        <div class="col-md-12">
                            <label class="text-gray-900">Select Deposit account</label>
                            <asp:DropDownList ID="DropDownList1" class="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Button ID="btncontig" runat="server" class="btn btn-sm btn-success" Text="Incure" OnClick="btncontig_Click" />

                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <center>
                                <asp:Button ID="contreturn" runat="server" class="btn btn-sm btn-danger" OnClick="contreturn_Click" Text="Return" />
                            </center>


                        </div>
                    </div>
                </div>
                <center>
                    <div class="modal-footer">
                    </div>

                </center>
            </div>
        </div>
    </div>
    <div class="modal fade" id="exampleModal1" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel1" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title text-gray-900" id="exampleModalLabel1">Update Phone Number</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-7">
                            <asp:TextBox ID="txtMob" class="form-control mx-2" runat="server"></asp:TextBox>

                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="Button4" runat="server" class="btn btn-primary" Text="Save" OnClick="Button4_Click" />

                        </div>
                    </div>
                </div>
                <center>
                    <div class="modal-footer">
                    </div>

                </center>
            </div>
        </div>
    </div>
</asp:Content>

