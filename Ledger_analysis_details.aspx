<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="Ledger_analysis_details.aspx.cs" Inherits="advtech.Finance.Accounta.Ledger_analysis_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Ledger Account Analysis</title>
    <script type="text/javascript">

        window.addEventListener('load', (event) => {
            var x = document.getElementById("Pbutton");
            x.style.display = "none";
    </script>
    <script src="../../asset/js/chart.min.js"></script>
    <style>
        .bc {
            border-color: #ff6a00;
        }

        .bd {
            border-color: #00ff21;
        }
    </style>
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


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" id="CCF" runat="server" visible="false">
        <div class="text-center mt-5">
            <span class="fas fa-6x fa-chart-pie"></span>
            <h1>Account Couldn't be Found</h1>
            <p class="lead">Enter a correct account name and try again.</p>
            <p>eg: start with " l- " and add account name in top-bar search</p>
        </div>
    </div>
    <!--Big blue-->

    <div class="container-fluid pl-3 pr-3" id="container" runat="server">
        <div class="modal fade" id="RenameShop" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel8shopM" aria-hidden="true">
            <div class="modal-dialog modal-sm" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <span class="fas fa-edit mr-2" style="color: #ff00bb"></span><span class="h6 modal-title mb-1 text-gray-900 font-weight-bolder" id="exampleModalLabel8shopM">Rename Account</span>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">

                                <asp:TextBox ID="txtRenameAccount" class="form-control form-control-sm" Style="border-color: #ff00bb" placeholder="New Account Name" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 mt-4">

                                <center>

                                    <asp:Button ID="btnRenameAccount" runat="server" class="btn text-white btn-sm w-100" Style="background-color: #d46fe8" Text="Rename Account" OnClick="btnRenameAccount_Click" />
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
        <div class="modal fade" id="exampleModalDelete" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-sm" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title text-gray-900 h6" id="exampleModalLabel"><span class="fas fa-edit btn btn-circle btn-sm text-white mr-2" style="background-color: #9d469d"></span>Delete Accounts</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">

                            <div class="col-md-12">
                                <asp:Button ID="btnDelete1" runat="server" class="btn btn-sm text-white w-100" Style="background-color: #9d469d" Text="Remove..." OnClick="btnDelete1_Click" />

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
        <div class="modal fade bd-example-modal-lg" id="SM" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
                        <h5 class="modal-title text-gray-900 font-weight-bold" id="H1">Bind Summary</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">



                            <div class="col-6 ">
                                <div class="form-group">
                                    <label class="font-weight-bold">From<span class="text-danger">*</span></label>


                                    <br />
                                    <div class="form-group mb-0">
                                        <div class="input-group input-group-alternative">
                                            <asp:TextBox ID="txtSmDateFrom" class="form-control form-control-sm " TextMode="Date" runat="server"></asp:TextBox>
                                            <div class="input-group-prepend">
                                                <span class="input-group-text"><i class=" fas fa-calendar"></i></span>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label class="font-weight-bold">To<span class="text-danger">*</span></label>


                                    <br />
                                    <div class="form-group mb-0">
                                        <div class="input-group input-group-alternative">
                                            <asp:TextBox ID="txtSmDateTo" class="form-control form-control-sm " TextMode="Date" runat="server"></asp:TextBox>
                                            <div class="input-group-prepend">
                                                <span class="input-group-text"><i class=" fas fa-calendar"></i></span>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <center>

                            <asp:Button ID="btnSearchSummary" class="btn btn-sm btn-danger" OnClick="btnSearchSummary_Click" runat="server"
                                Text="Bind" />
                    </div>
                    </center>
                </div>
            </div>
        </div>
        <div class="modal fade bd-example-modal-lg" id="SMChart" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
                        <h5 class="modal-title text-gray-900 font-weight-bold" id="H1"><span class="fas fa-calendar mr-2" style="color: #ff2ccd"></span>Fetch data</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">



                            <div class="col-6 ">
                                <div class="form-group">
                                    <label class="font-weight-bold">From<span class="text-danger">*</span></label>


                                    <br />
                                    <div class="form-group mb-0">
                                        <div class="input-group input-group-alternative">
                                            <asp:TextBox ID="txtCHDateFrom" class="form-control form-control-sm " TextMode="Date" runat="server"></asp:TextBox>


                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label class="font-weight-bold">To<span class="text-danger">*</span></label>


                                    <br />
                                    <div class="form-group mb-0">
                                        <div class="input-group input-group-alternative">
                                            <asp:TextBox ID="txtCHDateTo" class="form-control form-control-sm " TextMode="Date" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <center>
                            <button class="btn btn-primary btn-sm w-100 " style="background-color: #ff00bb; display: none" type="button" disabled id="Pbutton">
                                <span class="spinner-grow spinner-grow-sm" role="status" aria-hidden="true"></span>
                                Binding Data
                            </button>
                            <asp:Button ID="btnBindChart" class="btn btn-sm btn-danger" OnClientClick="myFunctionshop22();" Style="background-color: #ff2ccd" OnClick="btnBindChart_Click" runat="server"
                                Text="Bind Date" />
                    </div>
                    </center>
                </div>
            </div>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="row">
            <div class="col">
                <div class="bg-white rounded-lg">
                    <div class="row">

                        <div class="col-lg-4 border-right">

                            <div class="card-header bg-white  font-weight-bold">
                                <div class="row">
                                    <div class="col-md-6 text-left">
                                        <span class=" text-xs font-weight-bold">
                                            <a class="btn btn-light btn-circle mr-2" id="buttonback" runat="server" href="Ledger.aspx" data-toggle="tooltip" data-placement="bottom" title="Back to Ledger page">

                                                <span class="fa fa-arrow-left text-danger"></span>

                                            </a><span id="AccName" runat="server"></span></span><span id="lblMsg1" class="font-weight-bold text-danger mx-2 small" runat="server"></span>

                                    </div>
                                    <div class="col-md-6 text-right">

                                        <button class="btn btn-light btn-circle " type="button" data-toggle="modal" data-target="#exampleModal9v">

                                            <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Adjust Account">
                                                <div>
                                                    <i class="fas fa-plus text-danger"></i>

                                                </div>
                                            </a>

                                        </button>

                                    </div>
                                </div>

                            </div>
                            <div class="card-body">
                                <center>

                                    <span class="fas fa-chart-pie text-gray-500 fa-3x"></span>
                                    <br />
                                    <div style="padding: 20px 0px 0px 0px">
                                        <span id="AccountName" runat="server" class="badge badge-light text-lg font-weight-bold text-gray-700"></span>
                                        <br />
                                        <span class="fas fa-bezier-curve text-gray-400 mt-2 mr-2"></span><span class="text-gray-900 small" id="AccType" runat="server"></span>
                                    </div>

                                </center>
                                <br />
                                <br />
                                <div>

                                    <h5 class="font-weight-light text-danger">Recent Activity</h5>
                                    <hr />
                                    <div style="overflow-y: scroll; height: 450px" class="mb-3 border-bottom">
                                        <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                                            <ItemTemplate>

                                                <asp:Label ID="Label4" runat="server" Visible="false" Text='<%#Eval("LedID")%>'></asp:Label>
                                                <asp:Label ID="Label2" runat="server" Visible="false" Text='<%#Eval("Account")%>'></asp:Label>

                                                <ul class="timeline">

                                                    <li>
                                                        <a target="_blank" href="LedgerDetail.aspx?led=<%#Eval("Account")%>"
                                                            class="text-uppercase small">
                                                            <asp:Label ID="Label3" runat="server"></asp:Label>
                                                        </a>

                                                        <p>
                                                            <asp:Label ID="Label5" class="small text-gray-900 font-weight-bold" runat="server"></asp:Label>
                                                            by <span class="text-success small"><%#Eval("Date","{0: MMMM dd, yyyy}")%></span>
                                                        </p>
                                                        <span class="fas fa-edit text-gray-500"></span><span class="text-xs mx-1 font-italic text-gray-900"><%#Eval("Explanation")%></span>
                                                    </li>
                                                </ul>

                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>

                            </div>
                        </div>



                        <div class="col-lg-8">

                            <!-- Card Header - Dropdown -->
                            <div class="card-header bg-white">
                                <div class="row">
                                    <div class="col-8 text-left">
                                        <a href="#" data-toggle="modal" data-target="#SM">
                                            <span class="m-0 h6 font-weight-light text-gray-500">Account Summary</span><span class="mx-2">[<span id="datFrom" class="small text-danger font-italic" runat="server"></span><span>-</span><span id="datTo" class="small text-danger font-italic" runat="server"></span>]</span><span id="OpBalance" runat="server" class="m-0 h6 font-weight-light text-gray-500"></span>
                                        </a>

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

                                                <a href="#" class="dropdown-item  text-gray-700  text-danger9" id="A1" runat="server" data-toggle="modal" data-target="#RecAcc"><span class="fas fa-chart-bar  mr-2 text-gray-400  text-danger"></span>Find Transaction</a>
                                                <a href="#" class="dropdown-item  text-gray-700  text-danger" id="A4" runat="server" data-toggle="modal" data-target="#exampleModal9v"><span class="fas fa-plus  mr-2 text-gray-400  text-danger"></span>Adjust Account</a>
                                                <a href="#" class="dropdown-item  text-gray-700  text-danger" id="A5" runat="server" data-toggle="modal" data-target="#SM"><span class="fas fa-calendar-day  mr-2 text-gray-400  text-danger"></span>Date Range summary</a>
                                                <a href="#" class="dropdown-item  text-gray-700  text-danger" id="A6" runat="server"><span class="fas fa-flag-checkered  mr-2 text-gray-400  text-danger"></span>Account Report</a>
                                                <a href="#" class="dropdown-item  text-gray-700  text-danger" id="renameLink" runat="server" data-toggle="modal" data-target="#RenameShop"><span class="fas fa-edit  mr-2 text-gray-400  text-danger"></span>Rename Account</a>
                                                <a href="#" class="dropdown-item  text-gray-700  text-danger" id="deleteLink" runat="server" data-toggle="modal" data-target="#exampleModalDelete"><span class="fas fa-trash  mr-2 text-gray-400  text-danger"></span>Delete Account</a>
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

                                                <div class="text-xs font-weight-bold text-success text-uppercase mb-1 mr-2">Account Increased by</div>
                                                <div class="h5 mb-0 font-weight-light  text-gray-800">
                                                    <span id="TotDebitor" runat="server"></span>

                                                    <a class="dropdown-toggle btn-sm text-warning font-weight-bolder" href="#" role="button" id="dropdownMenuLink2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></a>



                                                    <div class="dropdown-menu  dropdown-menu-left shadow animated--fade-in text-center" data-backdrop="static" style="width: 600px" aria-labelledby="dropdownMenuLink2">
                                                        <div class="dropdown-header  font-weight-bolder text-success">Monthly Account Increase Trends</div>
                                                        <span id="Dec" runat="server" class="mx-bar2 small text-gray-bg-gray-600"></span>
                                                        <div class="dropdown-divider"></div>
                                                        <div class="row">
                                                            <div class="col-12">
                                                                <div class="chart-area" id="IncDiv" runat="server">
                                                                    <asp:Literal ID="ltrIncreaseTrends" runat="server"></asp:Literal>
                                                                </div>

                                                                <main role="main" id="main2" runat="server">

                                                                    <div class="starter-template">
                                                                        <center>


                                                                            <p class="lead">

                                                                                <i class="fas fa-chart-line text-gray-300  fa-3x"></i>

                                                                            </p>
                                                                            <h6 class="text-gray-700 small font-italic">Sorry!! Nothing to show here.</h6>
                                                                        </center>
                                                                    </div>



                                                                </main>
                                                                <span id="VIT2" runat="server"><span class="fas fa-info-circle text-primary mr-2"></span><small>Values are in million</small></span>

                                                            </div>
                                                        </div>
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
                                            <div class="row ">

                                                <div class="text-xs font-weight-bold text-danger text-uppercase mb-1 mr-2">Account Decreased by</div>
                                                <div class="h5 mb-0 font-weight-light  text-gray-800">
                                                    <span id="TotalCreditor" runat="server"></span>

                                                    <a class="dropdown-toggle btn-sm text-warning font-weight-bolder" href="#" role="button" id="dropdownMenuLink2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></a>



                                                    <div class="dropdown-menu  dropdown-menu-right shadow animated--fade-in text-center" style="width: 600px" aria-labelledby="dropdownMenuLink2">
                                                        <div class="dropdown-header  font-weight-bolder text-danger">Monthly Account Decrease Trends</div>
                                                        <span id="InTrendDate" runat="server" class="mx-bar2 small text-gray-bg-gray-600"></span>
                                                        <div class="dropdown-divider"></div>
                                                        <div class="row">
                                                            <div class="col-12">
                                                                <div class="chart-area" id="DecDiv" runat="server">
                                                                    <asp:Literal ID="ltrDecrease" runat="server"></asp:Literal>
                                                                </div>

                                                                <main role="main" id="main3" runat="server">

                                                                    <div class="starter-template">
                                                                        <center>


                                                                            <p class="lead">

                                                                                <i class="fas fa-chart-line text-gray-300  fa-3x"></i>

                                                                            </p>
                                                                            <h6 class="text-gray-700 small font-italic">Sorry!! Nothing to show here.</h6>
                                                                        </center>
                                                                    </div>



                                                                </main>
                                                                <span id="VIT" runat="server"><span class="fas fa-info-circle text-primary mr-2"></span><small>Values are in thousands</small></span>
                                                            </div>
                                                        </div>
                                                    </div>


                                                </div>

                                                <div class="col-auto text-right">
                                                    <i class="fas fa-dollar-sign  mx-2 fa-1x text-gray-300"></i>
                                                    <span id="BegSpan" runat="server" visible="false">+ Beg.</span>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-xl-4 col-md-6 mb-1">

                                        <div class="card-body">
                                            <div class="row no-gutters align-items-center">
                                                <div class="col mr-2">
                                                    <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">Leaving Balance to</div>
                                                    <div class="h5 mb-0 font-weight-light  text-gray-800"><span id="TotBala" runat="server"></span></div>
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



                            <br />

                            <div class="card-header bg-white border-top  ">

                                <div class="row">
                                    <div class=" col-6 text-left">
                                        <div class="nav-item dropdown">
                                            <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown2"
                                                role="button" data-toggle="dropdown" aria-haspopup="true"
                                                aria-expanded="false">
                                                <span id="Span4" class="m-0 font-weight-bold text-danger text-xs" runat="server">Account Trends</span>
                                            </a>
                                            <div class="dropdown-menu dropdown-menu-left animated--fade-in"
                                                aria-labelledby="navbarDropdown2">
                                                <a class="dropdown-item" href="#">
                                                    <asp:Button ID="btnMonthly" runat="server" OnClick="btnMonthly_Click" class="btn btn-link btn-sm w-100" Text="Monthly" /></a>
                                                <a class="dropdown-item" href="#">
                                                    <asp:Button ID="btnQuarter" runat="server" OnClick="btnQuarterClick" class="btn btn-link btn-sm w-100" Text="Quarterly" /></a>
                                                <a class="dropdown-item" href="#">
                                                    <asp:Button ID="btnYearly" OnClick="btnYearly_Click" runat="server" class="btn btn-link btn-sm w-100" Text="Yearly" /></a>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-4 text-right">
                                    </div>
                                    <div class="col-2 text-right">
                                        <div class="nav-item dropdown">
                                            <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown22"
                                                role="button" data-toggle="dropdown" aria-haspopup="true"
                                                aria-expanded="false">
                                                <span id="PeriodType" class="small" runat="server">This Year</span>
                                            </a>
                                            <div class="dropdown-menu dropdown-menu-right animated--fade-in"
                                                aria-labelledby="navbarDropdown22">
                                                <a class="dropdown-item" href="#">
                                                    <asp:Button ID="btnPeriodType" OnClick="btnPeriodType_Click" runat="server" class="btn btn-link btn-sm w-100 text-danger" Text="Change" /></a>
                                                <a class="dropdown-item" href="#">
                                                    <button type="button" data-toggle="modal" data-target="#SMChart" class="btn btn-sm btn-prm w-100">Customize</button>
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- Card Content - Collapse -->

                            <div class="card-body border-bottom">
                                <center>
                                    <span id="CHDateFrom" class="small text-danger font-italic" runat="server"></span><span>-</span><span id="CHDateTo" class="small text-danger font-italic" runat="server"></span>

                                </center>
                                <main role="main" id="main" runat="server">

                                    <div class="starter-template">
                                        <center>


                                            <p class="lead">

                                                <i class="fas fa-chart-line text-gray-300  fa-3x"></i>

                                            </p>
                                            <h6 class="text-gray-700 small font-italic">Sorry!! Nothing to show here.</h6>
                                        </center>
                                    </div>



                                </main>
                                <div class="chart-area">
                                    <asp:Literal ID="ltChart" runat="server"></asp:Literal>
                                </div>


                            </div>
                            <br />
                            <div class="card-header bg-white border-top">

                                <div class="row align-items-center">
                                    <div class=" col-6 text-left">
                                        <span class="m-0 font-weight-bold text-danger text-xs">Account Reconcilation<span class="text-muted text-uppercase mx-2 text-gray-400 small"></span></span>
                                    </div>
                                    <div class="col-6 text-right">
                                        <div class="dropdown no-arrow">
                                            <button class="btn btn-sm btn-light btn-circle dropdown-toggle" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">

                                                <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Options">
                                                    <div>
                                                        <i class="fas fa-caret-down text-danger"></i>

                                                    </div>
                                                </a>

                                            </button>


                                            <div class="dropdown-menu  dropdown-menu-right shadow animated--fade-in" aria-labelledby="dropdownMenuLink">
                                                <div class="dropdown-header text-gray-900">Option:</div>

                                                <a href="#" class="dropdown-item  text-gray-700  text-danger" id="A2" runat="server" data-toggle="modal" data-target="#RecAcc"><span class="fas fa-chart-bar  mr-2 text-gray-400  text-danger"></span>Find Transaction</a>
                                                <a href="#" class="dropdown-item  text-gray-700  text-danger" id="A3" runat="server" data-toggle="modal" data-target="#exampleModal9v"><span class="fas fa-plus  mr-2 text-gray-400  text-danger"></span>Adjust Account</a>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- Card Content - Collapse -->

                            <div class="card-body">
                                <main role="main" id="main1" class="mt-3" runat="server">

                                    <div class="starter-template">
                                        <center>


                                            <p class="lead">

                                                <i class="fas fa-chart-bar text-gray-300  fa-3x"></i>

                                            </p>
                                            <h6 class="text-gray-300 font-weight-bold">No Reconcilation Found</h6>
                                        </center>
                                    </div>



                                </main>
                                <div id="InvoiceDiv" class="small" runat="server">
                                    <div class="row mb-3">
                                        <div class="col-12">
                                            <h5 class="font-weight-normal border-bottom text-warning">Matched Transaction</h5>
                                            <h6 class="text-gray-900 font-weight-bold border-bottom">Search Criteria</h6>
                                            <span id="d1" class=" text-success font-italic mb-3" runat="server"></span><span>-</span><span id="d2" class=" text-success font-italic" runat="server"></span>
                                            [<span class=" text-gray-900 font-italic mb-3 font-weight-bold" id="Camount" runat="server"></span>]<br />
                                            <span class=" text-gray-900 font-italic mt-2 font-weight-bold" id="Span2" runat="server">Trans. Type:</span><span class=" text-danger font-italic mx-2 font-weight-bold" id="Span3" runat="server">General Ledger</span>-<span class=" text-gray-900 font-italic mx-2 font-weight-bold" id="TrType" runat="server"></span>
                                        </div>

                                    </div>
                                    <div class="row">

                                        <div class="col-6">
                                            <asp:Repeater ID="Repeater3" runat="server">

                                                <HeaderTemplate>

                                                    <table class="table align-items-center table-sm  table-hover" id="dataTable" width="100%" cellspacing="8">
                                                        <thead class="">
                                                            <tr>


                                                                <th scope="col" class="text-gray-900">Debit</th>
                                                                <th scope="col" class="text-gray-900">Credit</th>
                                                                <th scope="col" class="text-gray-900">Balance
                                                                </th>
                                                                <th scope="col" class="text-gray-900">Explanation</th>
                                                                <th scope="col" class="text-gray-900 text-right">Date</th>


                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>




                                                        <td class="text-gray-900">
                                                            <%# Eval("Debit", "{0:N2}")%>
                                                        </td>

                                                        <td class="text-gray-900">
                                                            <%# Eval("Credit", "{0:N2}")%>
                                                        </td>

                                                        <td class="text-gray-900">
                                                            <%# Eval("Balance", "{0:N2}")%>
                                                        </td>
                                                        <td class="text-gray-900">
                                                            <%# Eval("Explanation")%>
                                                        </td>
                                                        <td class="text-gray-900  text-right">
                                                            <%# Eval("Date", "{0: MMMM dd,yyyy}")%>
                                                        </td>



                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </tbody>
              </table>

                                                </FooterTemplate>

                                            </asp:Repeater>
                                        </div>
                                        <div class="col-6" id="DivInvoice" runat="server" visible="false">
                                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">

                                                <HeaderTemplate>

                                                    <table class="table align-items-center table-sm">
                                                        <thead class="">
                                                            <tr>
                                                                <th scope="col" class="">#</th>
                                                                <th scope="col" class="">Customer</th>
                                                                <th scope="col" class="text-left text-danger">Invoice Date</th>


                                                                <th scope="col" class=" text-right">Total</th>

                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>

                                                        <td class="text-primary text-left">
                                                            <%# Eval("id")%>
                    
                                                        </td>

                                                        <td class="text-primary text-left">
                                                            <a href="rentinvoicereport.aspx?id= <%# Eval("id2")%>&&cust=<%# Eval("customer")%>"><%# Eval("customer")%></a>

                                                        </td>
                                                        <td class="text-gray-900 text-left">
                                                            <%# Eval("date", "{0: MMMM dd,yyyy}")%>
                       
                    
                                                        </td>

                                                        <td class="text-gray-900 text-right">
                                                            <asp:Label ID="lblvatplus" Visible="false" runat="server" Text='<%# Eval("paid")%>'></asp:Label>
                                                            <%# Eval("paid", "{0:N2}")%>
                                                        </td>
                                                    </tr>

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </tbody>
              </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <div class="col-6" id="DivBills" runat="server" visible="false">
                                            <asp:Repeater ID="rptrBills" runat="server" OnItemDataBound="rptrBills_ItemDataBound">


                                                <HeaderTemplate>

                                                    <table class="table align-items-center table-sm table-bordered ">

                                                        <thead class="">
                                                            <tr>


                                                                <th scope="col" class="text-left ">Item Name</th>
                                                                <th scope="col" class="text-left">Unit </th>
                                                                <th scope="col" class="text-left">Unit Price</th>
                                                                <th scope="col" class="text-left">Quantity</th>
                                                                <th scope="col" class="text-left">Pre-Tax</th>
                                                                <th scope="col" class="text-left">VAT(15%)</th>
                                                                <th scope="col" class="text-right">Total</th>

                                                            </tr>
                                                        </thead>

                                                        <tbody>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>


                                                        <td class="text-left text-gray-900">
                                                            <a class="font-weight-light" title="Show the details" href="billupdate.aspx?prd=<%# Eval("PName")%>&&on=<%# Eval("OrderNumber")%>&&cust=<%# Eval("customer")%>" style="color: #0066FF;"><%# Eval("PName")%></a>

                                                        </td>
                                                        <td class="text-left text-gray-900">
                                                            <%# Eval("Unit")%>
                                                        </td>
                                                        <td class="text-left text-gray-900">
                                                            <%# Eval("UnitCost", "{0:N2}")%>
                                                        </td>
                                                        <td class="text-left text-gray-900">
                                                            <%# Eval("Quantity", "{0:N2}")%>
                                                        </td>
                                                        <td class="text-left text-gray-900">
                                                            <asp:Label ID="lblVAT" runat="server" Visible="false" Text='<%# Eval("VAT")%>'></asp:Label>
                                                            <%# Eval("VAT", "{0:N2}")%>
                                                        </td>

                                                        <td class="text-left text-gray-900">
                                                            <asp:Label ID="lblTAX" runat="server"></asp:Label>

                                                        </td>
                                                        <td class="text-right text-gray-900">
                                                            <asp:Label ID="lblTotal" runat="server" Visible="false" Text='<%# Eval("Total")%>'></asp:Label>
                                                            <%# Eval("Total", "{0:N2}")%>
                                                        </td>


                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </tbody>
              </table>
              <hr class="text-gray-700 font-weight-bold" />
                                                </FooterTemplate>

                                            </asp:Repeater>
                                        </div>
                                        <div class="col-6" id="DivAssetBills" runat="server" visible="false">
                                            <asp:Repeater ID="rptAssetBills" runat="server" OnItemDataBound="rptAssetBills_ItemDataBound">


                                                <HeaderTemplate>

                                                    <div class="table-responsive">
                                                        <table class="table align-items-center table-sm table-bordered ">

                                                            <thead>
                                                                <tr>

                                                                    <th scope="col" class="text-left ">Item Name</th>
                                                                    <th scope="col" class="text-left">Unit </th>
                                                                    <th scope="col" class="text-left">Unit Cost</th>
                                                                    <th scope="col" class="text-left">Quantity</th>
                                                                    <th scope="col" class="text-left">Pre-Tax</th>
                                                                    <th scope="col" class="text-left">VAT(15%)</th>
                                                                    <th scope="col" class="text-right">Total</th>

                                                                </tr>
                                                            </thead>

                                                            <tbody>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>

                                                        <td class="text-left text-gray-900">
                                                            <a class="font-weight-light" title="Show the details" href="purchasesorderupdateasser.aspx?prd=<%# Eval("name")%>&&on=<%# Eval("orderno")%>" style="color: #0066FF;"><%# Eval("name")%></a>

                                                        </td>
                                                        <td class="text-left text-gray-900">
                                                            <%# Eval("unit")%>
                                                        </td>
                                                        <td class="text-left text-gray-900">
                                                            <%# Eval("unitcost", "{0:N2}")%>
                                                        </td>
                                                        <td class="text-left text-gray-900">

                                                            <%# Eval("qty", "{0:N2}")%>
                                                        </td>
                                                        <td class="text-left text-gray-900">
                                                            <asp:Label ID="lblVAT" Visible="false" runat="server" Text='<%# Eval("pretax")%>'></asp:Label>
                                                            <%# Eval("pretax", "{0:N2}")%>
                                                        </td>
                                                        <td class="text-left text-gray-900">
                                                            <asp:Label ID="lblTAX" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td class="text-right text-gray-900">
                                                            <asp:Label ID="lblTotal" runat="server" Visible="false" Text='<%# Eval("total")%>'></asp:Label>
                                                            <%# Eval("total", "{0:N2}")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </tbody>
              </table>
              <hr class="text-gray-700 font-weight-bold" />
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <div class="col-6" id="DivBankTransaction" runat="server" visible="false">
                                            <asp:Repeater ID="rptrBank" runat="server">


                                                <HeaderTemplate>

                                                    <div class="table-responsive">
                                                        <table class="table align-items-center table-sm">

                                                            <thead class=" thead-white">
                                                                <tr>

                                                                    <th scope="col" class="text-left">Deposit</th>
                                                                    <th scope="col" class="text-left">Withdrawl</th>

                                                                    <th scope="col" class="text-right">Date</th>

                                                                </tr>
                                                            </thead>

                                                            <tbody>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="text-left text-gray-900">
                                                            <%# Eval("cashin", "{0:N2}")%>
                                                        </td>
                                                        <td class="text-left text-gray-900">
                                                            <%# Eval("cashout", "{0:N2}")%>
                                                        </td>

                                                        <td class="text-right text-gray-900">
                                                            <%# Eval("date", "{0: MMMM dd,yyyy}")%>
                                                        </td>


                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </tbody>
              </table>
              <hr class="text-gray-700 font-weight-bold" />
                                                </FooterTemplate>

                                            </asp:Repeater>
                                        </div>
                                        <div class="col-6" id="DivOtherInvoice" runat="server" visible="false">
                                            <asp:Repeater ID="rptrOtherInvoice" runat="server">

                                                <HeaderTemplate>

                                                    <table class="table align-items-center table-sm ">
                                                        <thead>
                                                            <tr>


                                                                <th scope="col" class="text-gray-900 font-weight-bold">Invoice Type</th>

                                                                <th scope="col" class="text-right text-danger ">Amount</th>
                                                                <th scope="col" class="text-right text-danger ">Date</th>




                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                </HeaderTemplate>
                                                <ItemTemplate>

                                                    <tr>

                                                        <td>
                                                            <a class=" text-primary" href="OtherInvoices.aspx?ref2=<%# Eval("id")%>&&expname=<%# Eval("incomename")%>"><span><%# Eval("incomename")%></span></a>


                                                        </td>
                                                        <td class="text-right">
                                                            <span id="Span1" class="mx-1  text-gray-900" runat="server"><%# Eval("amount","{0:N2}")%></span>
                                                        </td>

                                                        <td class="text-right">
                                                            <span id="Label1" class="mx-1  text-gray-900" runat="server"><%# Eval("date", "{0: dd/MM/yyyy}")%></span>
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
                </div>
            </div>

        </div>
    </div>
    <div class="modal fade" id="exampleModal9v" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel9v" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title font-weight-bolder text-gray-900" id="exampleModalLabel9v">Adjust Your Account</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row mb-3">
                        <div class="col-md-12">
                            <input type="range" class="custom-range" min="-100000" max="100000" step="1" value="1000" id="customRange2">

                            <span class="fas fa-arrow-circle-right text-primary"></span><span class="small text-danger mx-1">set amount to be adjusted</span><a href="#" data-toggle="tooltip" data-placement="top" title="Calculate balance"><span onclick="calculateBalanceamount()" class="fas fa-calculator text-success"></span></a>
                            <asp:TextBox ID="txtAmount" onchange="tSpeedValue(this)" placeholder="Amount" class="form-control form-control-sm" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <hr />
                            <asp:Label ID="Label6" runat="server" ClientIDMode="Static"></asp:Label>


                            <asp:TextBox ID="TextBox2" placeholder="Set balance" ReadOnly="true" class="form-control form-control-sm" runat="server" ClientIDMode="Static"></asp:TextBox>

                            <hr />
                            <span class="fas fa-arrow-circle-right text-primary"></span><span class="small text-danger mx-1">Or set adjusted balance</span><a href="#" data-toggle="tooltip" data-placement="top" title="Calculate amount"><span onclick="calculateamount()" class="fas fa-calculator text-success"></span></a>
                            <asp:TextBox ID="TextBox1" placeholder="Set balance" class="form-control form-control-sm" Style="border-color: #ff6a00" runat="server" ClientIDMode="Static"></asp:TextBox>

                        </div>
                    </div>
                    <script type="text/javascript">
            var slider = document.getElementById("customRange2");
            var txtAm = document.getElementById("#<%=txtAmount.ClientID%>");

            slider.oninput = function () {
                $("#txtAmount").val(this.value);
                const balance = $("#TextBox2").val();
                const balance1 = $("#txtAmount").val();
                const bl2 = -(-balance - balance1);
                $("#TextBox1").val(bl2);
            }
            function calculateBalanceamount() {
                $("#txtAmount").val();
                const balance = $("#TextBox2").val();
                const balance1 = $("#txtAmount").val();
                const bl2 = -(-balance - balance1);
                $("#TextBox1").val(bl2);
            }
            function calculateamount() {
                $("#txtAmount").val();
                const balance = $("#TextBox2").val();
                const balance1 = $("#TextBox1").val();
                const bl2 = balance1 - balance;
                $("#txtAmount").val(bl2);
            }
                    </script>
                    <div class="row mb-3">
                        <div class="col-md-12">

                            <asp:TextBox ID="txtRemark" placeholder="Remark" TextMode="MultiLine" Height="100px" class="form-control form-control-sm" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <center>
                                <asp:Button ID="btnAdjustAccount" runat="server" class="btn btn-sm btn-primary w-100" Text="Adjust" />
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

    <div class="modal fade bd-example-modal-lg" tabindex="-1" id="RecAcc" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
                    <h5 class="modal-title text-gray-900 font-weight-bold" id="H1">Search Transaction</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <hr />
                        <div class="col-md-12 border-bottom border-top align-items-center">
                            <span class="fas fa-arrow-alt-circle-right text-gray-400 mr-1"></span>
                            <span class="text-gray-400 small mr-5">Transaction amount</span>
                            <div class="custom-control custom-radio custom-control-inline">
                                <input type="radio" id="equal" name="customRadioInline1" class="custom-control-input" checked="true" runat="server" clientidmode="Static" />
                                <label class="custom-control-label text-danger small font-weight-bolder " for="equal">=</label>
                            </div>
                            <div class="custom-control custom-radio custom-control-inline">


                                <input type="radio" id="greater" name="customRadioInline1" class="custom-control-input" runat="server" clientidmode="Static" />
                                <label class="custom-control-label text-danger small font-weight-bolder " for="greater">></label>
                            </div>
                            <div class="custom-control custom-radio custom-control-inline">
                                <input type="radio" id="less" name="customRadioInline1" class="custom-control-input" runat="server" clientidmode="Static" />
                                <label class="custom-control-label text-danger small font-weight-bolder " for="less"><</label>
                            </div>

                            <div class="custom-control custom-radio custom-control-inline">
                                <input type="radio" id="or" name="customRadioInline1" class="custom-control-input" runat="server" clientidmode="Static" />
                                <label class="custom-control-label text-success small font-weight-bold " for="or">OR</label>
                            </div>

                        </div>
                        <hr />
                    </div>
                    <div class="row">



                        <div class="col-4 ">
                            <div class="form-group">
                                <label class="font-weight-bold">From<span class="text-danger">*</span></label>


                                <br />
                                <div class="form-group mb-0">
                                    <div class="input-group input-group-alternative">
                                        <asp:TextBox ID="txtDateform" class="form-control  form-control-sm" TextMode="Date" runat="server"></asp:TextBox>
                                        <div class="input-group-prepend">
                                            <span class="input-group-text"><i class=" fas fa-calendar"></i></span>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label class="font-weight-bold">To<span class="text-danger">*</span></label>


                                <br />
                                <div class="form-group mb-0">
                                    <div class="input-group input-group-alternative">
                                        <asp:TextBox ID="txtDateto" class="form-control form-control-sm " TextMode="Date" runat="server"></asp:TextBox>
                                        <div class="input-group-prepend">
                                            <span class="input-group-text"><i class=" fas fa-calendar"></i></span>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label class="font-weight-bold">Amount<span class="text-danger">*</span></label>


                                <br />
                                <div class="form-group mb-0">
                                    <div class="input-group input-group-alternative">
                                        <asp:TextBox ID="txtReconcileAmount" class="form-control bc form-control-sm " placeholder="Reconcile Amount" value="0" runat="server"></asp:TextBox>


                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <hr />
                        <div class="col-md-12 border-top align-items-center">
                            <span class="fas fa-arrow-alt-circle-right text-gray-400 mr-1"></span>
                            <span class="text-gray-600 small mr-5">Transaction amount in range</span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-5">
                            <div class="form-group">
                                <div class="form-group mb-0">
                                    <div class="input-group input-group-alternative">
                                        <asp:TextBox ID="txtRangeMin" class="form-control bd form-control-sm " placeholder="Min Amount" runat="server"></asp:TextBox>


                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-2  text-center">
                            <span class="small text-danger text-center font-weight-bold">&</span>
                        </div>
                        <div class="col-5">
                            <div class="form-group">

                                <div class="form-group mb-0">
                                    <div class="input-group input-group-alternative">
                                        <asp:TextBox ID="txtRangeMax" class="form-control bd form-control-sm " placeholder="Max Amount" runat="server"></asp:TextBox>


                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <asp:DropDownList ID="ddlRecType" CssClass="form-control form-control-sm" runat="server">
                                <asp:ListItem>Cash Invoice</asp:ListItem>
                                <asp:ListItem>Bills</asp:ListItem>
                                <asp:ListItem>Asset Bills</asp:ListItem>
                                <asp:ListItem>Bank Transaction</asp:ListItem>
                                <asp:ListItem>Other Invoice</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-2 text-left">
                            <div class="spinner-border  text-warning spinner-border-sm" id="myDIV5" role="status">
                                <span class="sr-only">Loading...</span>
                            </div>
                        </div>
                        <div class="col-10 text-right">
                            <asp:Button ID="btnReconcilation" class="btn btn-sm btn-danger" OnClick="btnReconcilation_Click" OnClientClick="myFunctionshop()" runat="server"
                                Text="Reconcile Account" />
                        </div>
                    </div>


                </div>

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
            function myFunctionshop22() {
                var y = document.getElementById("<%=btnBindChart.ClientID %>"); var x = document.getElementById("Pbutton");
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
    <script type="text/javascript" src="http://cdn.jsdelivr.net/json2/0.1/json2.js"></script>
    <script type="text/javascript">
            $(function () {
                $("[id*=btnAdjustAccount]").bind("click", function () {
                    var user = {};
                    user.Username = $("[id*=txtAmount]").val();
                    user.Password = $("[id*=txtRemark]").val();
                    $.ajax({
                        type: "POST",
                        url: "Ledger_analysis_details.aspx/SaveUser",
                        data: '{user: ' + JSON.stringify(user) + '}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            alert("Account has been successfully adjusted.");
                            window.location.reload();
                        }
                    });
                    return false;
                });
            });
    </script>
</asp:Content>
