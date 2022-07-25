<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="shop_details.aspx.cs" Inherits="advtech.Finance.Accounta.shop_details" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Shop details</title>
    <script type="text/javascript">
        function HideORshowDiv() {
            var div = document.getElementById('leftDiv');
            var div2 = document.getElementById('rightleftDiv');
            div.className = "col-12 border-right";
            div.className = "invisible";

        }

    </script>
    <script type="text/javascript">

        window.addEventListener('load', (event) => {
            var x = document.getElementById("Pbutton");
            x.style.display = "none";
    </script>
    <script type="text/javascript">

            window.addEventListener('load', (event) => {
                var x = document.getElementById("Pbutton1");
                x.style.display = "none";
    </script>
    <script type="text/javascript">

                window.addEventListener('load', (event) => {
                    var x = document.getElementById("Pbutton3");
                    x.style.display = "none";
    </script>
    <style>
        .file-upload {
            display: block;
            text-align: center;
            font-family: Helvetica, Arial, sans-serif;
            font-size: 12px;
        }

            .file-upload .file-select {
                display: block;
                border: 2px solid #dce4ec;
                color: #34495e;
                cursor: pointer;
                height: 40px;
                line-height: 40px;
                text-align: left;
                background: #FFFFFF;
                overflow: hidden;
                position: relative;
            }

                .file-upload .file-select .file-select-button {
                    background: #dce4ec;
                    padding: 0 10px;
                    display: inline-block;
                    height: 40px;
                    line-height: 40px;
                }

                .file-upload .file-select .file-select-name {
                    line-height: 40px;
                    display: inline-block;
                    padding: 0 10px;
                }

                .file-upload .file-select:hover {
                    border-color: #34495e;
                    transition: all .2s ease-in-out;
                    -moz-transition: all .2s ease-in-out;
                    -webkit-transition: all .2s ease-in-out;
                    -o-transition: all .2s ease-in-out;
                }

                    .file-upload .file-select:hover .file-select-button {
                        background: #34495e;
                        color: #FFFFFF;
                        transition: all .2s ease-in-out;
                        -moz-transition: all .2s ease-in-out;
                        -webkit-transition: all .2s ease-in-out;
                        -o-transition: all .2s ease-in-out;
                    }

            .file-upload.active .file-select {
                border-color: #3fa46a;
                transition: all .2s ease-in-out;
                -moz-transition: all .2s ease-in-out;
                -webkit-transition: all .2s ease-in-out;
                -o-transition: all .2s ease-in-out;
            }

                .file-upload.active .file-select .file-select-button {
                    background: #3fa46a;
                    color: #FFFFFF;
                    transition: all .2s ease-in-out;
                    -moz-transition: all .2s ease-in-out;
                    -webkit-transition: all .2s ease-in-out;
                    -o-transition: all .2s ease-in-out;
                }

            .file-upload .file-select input[type=file] {
                z-index: 100;
                cursor: pointer;
                position: absolute;
                height: 100%;
                width: 100%;
                top: 0;
                left: 0;
                opacity: 0;
                filter: alpha(opacity=0);
            }

            .file-upload .file-select.file-select-disabled {
                opacity: 0.65;
            }

                .file-upload .file-select.file-select-disabled:hover {
                    cursor: default;
                    display: block;
                    border: 2px solid #dce4ec;
                    color: #34495e;
                    cursor: pointer;
                    height: 40px;
                    line-height: 40px;
                    margin-top: 5px;
                    text-align: left;
                    background: #FFFFFF;
                    overflow: hidden;
                    position: relative;
                }

                    .file-upload .file-select.file-select-disabled:hover .file-select-button {
                        background: #dce4ec;
                        color: #666666;
                        padding: 0 10px;
                        display: inline-block;
                        height: 40px;
                        line-height: 40px;
                    }

                    .file-upload .file-select.file-select-disabled:hover .file-select-name {
                        line-height: 40px;
                        display: inline-block;
                        padding: 0 10px;
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
    <script type="text/javascript">
                    $(document).ready(function () {
                        //We are binding onchange event using jQuery built-in change event
                        $('#ddlSHopExpand').change(function () {
                            //get selected value and check if subject is selected else show alert box
                            var SelectedValue = $("#ddlSHopExpand").val();
                            if (SelectedValue > 0) {
                                //get selected text and set to label
                                let SelectedText = $("#ddlSHopExpand option:selected").val();

                                lblSelectedText.innerHTML = SelectedText + " square meter";
                                //set selected value to label

                            } else {
                                //reset label values and show alert
                                lblSelectedText.innerHTML = "";

                            }
                        });
                    });
    </script>
    <script type="text/javascript">
                    function Set() {
                        PageMethods.SetToPrimary(ShopNo.innerHTML, SI1.innerHTML, Success, error);
                    }
                    function Success(result) {
                        alert("Successfully completed.");
                        window.location.reload();
                    }
                    function error(error) {
                        alert("Error.");
                        window.location.reload();
                    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container " id="CCF" runat="server" visible="false">
        <div class="text-center mt-5">
            <span class="fas fa-6x fa-home"></span>
            <h1>Shop Couldn't be Found</h1>
            <p class="lead">Enter a correct shop name and try again.</p>
            <p>eg: start with " s- " and add shop name name in top-bar search</p>
        </div>
    </div>



    <div class="container-fluid pl-3 pr-3" id="container" runat="server">
        <asp:ScriptManager ID='ScriptManager1' runat='server' EnablePageMethods='true' />

        <div class="row">
            <div class="col">
                <div class="bg-white rounded-3">
                    <div class="row">

                        <div class="col-lg-7 border-right border-bottom  border-info" id="leftDiv" style="border-color: #ff00bb">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            <div class="card-header bg-white border-bottom border-info  font-weight-bold">
                                <div class="row">
                                    <div class="col-md-10 text-left">
                                        <span class=" h5 font-weight-bold">
                                            <a class="btn btn-light btn-circle mr-2" id="buttonback" runat="server" href="addshop.aspx" data-toggle="tooltip" data-placement="bottom" title="Back to Shop page">

                                                <span class="fa fa-arrow-left text-danger"></span>

                                            </a>
                                            <asp:Label ID="ShopNo" ClientIDMode="Static" runat="server" Style="color: #ff00bb" class="badge badge-light mr-2"></asp:Label>

                                            <span id="lblMsg1" class="font-weight-bold text-danger mx-2 small" runat="server"></span>
                                            </span>
                                        <span id="SI" runat="server" visible="false" class="fas fa-user text-white btn-circle btn-sm mx-2 mr-2" style="background-color: #ff6a00"></span>

                                        <asp:Label ID="SI1" ClientIDMode="Static" runat="server" data-toggle="tooltip" data-placement="top" title="Shop occupied customer" Visible="false" class="badge badge-light"></asp:Label>

                                        <a href="#" ><span id="ShopStat" runat="server" class="badge text-white" style="background-color: #5f27ba"></span></a>
                                    </div>
                                    <div class="col-md-2 text-right">

                                        <button class="btn btn-light btn-circle " type="button" data-toggle="modal" data-target="#exampleModal1111">

                                            <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Upload Shop Images">
                                                <div>
                                                    <i class="fas fa-image text-danger"></i>

                                                </div>
                                            </a>

                                        </button>
                                        <button class="btn btn-light btn-circle " type="button" onclick="HideORshowDiv()">
                                            <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Resize">
                                                <div>
                                                    <i class="fas fa-arrows-alt-h text-danger" id="DivIcon"></i>

                                                </div>
                                            </a>
                                        </button>
                                    </div>
                                </div>

                            </div>
                            <div class="card-body">
                                <main role="main" id="main2" class="mt-5 align-content-center" runat="server">

                                    <div class="starter-template">
                                        <center>

                                            <p class="lead text-primary">
                                                <span class="fas fa-images  fa-9x" style="color: #ff00bb"></span>
                                            </p>

                                            <h6 class="text-gray-300 text-xs" style="font-weight: bold">No Shop Image Found. <a class="btn btn-link text-primary" id="A8" runat="server" href="#" data-toggle="modal" data-target="#exampleModal1111">Click to add</a></h6>
                                        </center>
                                    </div>

                                </main>




                            </div>
                            <div id="carousel-example-generic" class="carousel slide carousel-fade pl-2 pr-2" data-ride="carousel" data-ride="carousel">
                                <!-- Indicators -->
                                <div id="Repiv" runat="server">
                                    <ol class="carousel-indicators" id="prfdiv" runat="server">
                                        <li class="text-warning" data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
                                        <li data-target="#carousel-example-generic" data-slide-to="1"></li>
                                        <li data-target="#carousel-example-generic" data-slide-to="2"></li>

                                    </ol>

                                    <!-- Wrapper for slides -->

                                    <div class="carousel-inner" id="showdefaultimagefromdatabase" runat="server">
                                        <div>
                                            <asp:Repeater ID="Repeater2" runat="server">
                                                <ItemTemplate>
                                                    <div class="carousel-item <%# GetActiveClass(Container.ItemIndex) %>">
                                                        <img class=" w-100" src="../../asset/shp/<%# Eval("filename")%><%# Eval("extension")%>" height="900">
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>

                                    </div>

                                    <!-- Controls -->
                                    <a class="carousel-control-prev text-gray-900" href="#carousel-example-generic" role="button" data-slide="prev">
                                        <span class="fas fa-arrow-left fa-1x btn-circle  border-left border-bottom border-right border-top" style="color: #ff00bb; border-color: #ff00bb;"></span>
                                        <span class="sr-only">Previous</span>
                                    </a>
                                    <a class="carousel-control-next" href="#carousel-example-generic" role="button" data-slide="next">
                                        <span class="fas fa-arrow-right fa-1x btn-circle  border-bottom border-right border-left border-top" style="color: #ff00bb; border-color: #ff00bb;"></span>
                                        <span class="sr-only">Next</span>
                                    </a>
                                </div>
                            </div>
                            <div class="card-body border-top">

                                <div class="row">
                                    <div class="col-6">
                                        <span class="fas fa-book-open text-gray-500 mr-2"></span><span class="m-0 h6 font-weight-light text-gray-500">Description</span>

                                    </div>
                                    <div class="col-6 text-right">
           
                                    </div>
                                    <table class="table table-sm mt-3" id="descIcon" runat="server">
                                        <tbody>
                                            <tr>
                                                <td><span class="fas fa-edit text-danger mr-2"></span></td>
                                                <td><span id="Desc" runat="server" class="small text-danger"></span></td>
                                            </tr>
                                        </tbody>

                                    </table>
                                </div>
                                <hr />
                                <main role="main" id="main3" class="mt-5 align-content-center" runat="server">

                                    <div class="starter-template">
                                        <center>

                                            <p class="lead text-primary">
                                                <span class="fas fa-book-open text-gray-300  fa-2x"></span>
                                            </p>

                                            <h6 class="text-gray-300 text-xs" style="font-weight: bold">No Description Found.</h6>
                                        </center>
                                    </div>

                                </main>
                            </div>
                        </div>





                        <div class="col-lg-5" id="rightDiv">

                            <!-- Card Header - Dropdown -->
                            <div class="card-header bg-white">
                                <div class="row">
                                    <div class="col-8 text-left">

                                        <span class="m-0 h6 font-weight-light text-gray-500">Shop Summary</span><a href="#" data-toggle="modal" data-target="#SM"><span class="badge badge-danger mx-2" id="Status" runat="server"></span>
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
                                                <div class="dropdown-header text-gray-900">Option:</div>

                                                <a href="#" class="dropdown-item  text-gray-700  text-danger" id="A1" runat="server" data-toggle="modal" data-target="#exampleModalShopAreaChange"><span class="fas fa-location-arrow  mr-2" style="color: #ff00bb"></span>Change Area</a>
                                                <a href="#" class="dropdown-item  text-gray-700  text-danger" id="A4" runat="server" data-toggle="modal" data-target="#exampleModalShopMerge"><span class="fas fa-code-branch  mr-2 " style="color: #ff00bb"></span>Merge shop</a>
                                                <a href="#" class="dropdown-item  text-gray-700  text-danger" visible="false" id="A2" runat="server" data-toggle="modal" data-target="#exampleModalAssignShop"><span class="fas fa-plus-circle  mr-2 " style="color: #ff00bb"></span>Assign Suspended Shop</a>
                                                <a href="#" class="dropdown-item  text-gray-700  text-danger" visible="true" id="A9" runat="server" data-toggle="modal" data-target="#DescriptionShop"><span class="fas fa-edit  mr-2 " style="color: #ff00bb"></span>Add description</a>

                                                <a href="#" class="dropdown-item  text-gray-700  text-danger" id="A3" runat="server" data-toggle="modal" data-target="#DeleteShop"><span class="fas fa-trash  mr-2 " style="color: #ff00bb"></span>Remove Shop</a>
                                                <a href="#" class="dropdown-item  text-gray-700  text-danger" id="A5" runat="server" data-toggle="modal" data-target="#RenameShop"><span class="fas fa-edit  mr-2 " style="color: #ff00bb"></span>Rename</a>
                                                <a href="#" class="dropdown-item  text-gray-700  text-danger" id="A6" runat="server" data-toggle="modal" data-target="#DelModal"><span class="fas fa-image  mr-2 " style="color: #ff00bb"></span>Remove Image</a>
                                                <a href="#" class="dropdown-item  text-gray-700  text-danger" id="A7" runat="server" data-toggle="modal" data-target="#RemoveModal"><span class="fas fa-wave-square  mr-2 " style="color: #ff00bb"></span>Remove Comment & Activity</a>
                                                <a href="#" class="dropdown-item  text-gray-700  text-danger" id="A11" runat="server" data-toggle="modal" data-target="#LocModal"><span class="fas fa-dot-circle  mr-2 " style="color: #ff00bb"></span>Update Location</a>

                                                <div class="dropdown-header text-gray-900">Operation:</div>
                                                <a href="#" class="dropdown-item  text-gray-700  text-danger" id="A10" runat="server" data-toggle="modal" data-target="#ShopFree"><span class="fas fa-arrow-alt-circle-left  mr-2 " style="color: #ff00bb"></span>Set to Free</a>

                                                <a onclick="Set();" href="#" class="dropdown-item  text-gray-700"><span class="fas fa-check-circle  mr-2 " style="color: #3fa46a"></span>Set to Primary</a>

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
                                                <div class="col-9 text-left">
                                                    <div class="text-xs font-weight-bold text-uppercase mb-1 mr-2" style="color: #ff00bb">Area</div>
                                                    <div class="h6 mb-0 font-weight-light  text-gray-800">
                                                        <a href="#" data-toggle="modal" data-target="#exampleModalShopAreaChange"><span id="area" runat="server"></span></a><span class="mx-1">m<sup>2</sup></span>




                                                    </div>
                                                </div>
                                                <div class="col-3 text-right">
                                                    <i class="fas fa-map  mx-2 fa-1x text-gray-300"></i>

                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <!-- Earnings (Monthly) Card Example -->
                                    <div class="col-xl-4 col-md-6 mb-1 border-right">

                                        <div class="card-body" data-toggle="tooltip" title="Amount Per unit area">
                                            <div class="row ">
                                                <div class="col-9 text-left">
                                                    <div class="text-xs font-weight-bold text-uppercase mb-1 mr-2" style="color: #ff00bb">Rate</div>
                                                    <div class="h6 mb-0 font-weight-light  text-gray-800">
                                                        <a href="#" data-toggle="modal" data-target="#exampleModalUpdatePrice"><span id="rate" runat="server"></span></a>


                                                    </div>
                                                </div>
                                                <div class="col-3 text-right">
                                                    <i class="fas fa-dollar-sign  mx-2 fa-1x text-gray-300"></i>

                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-xl-4 col-md-6 mb-1">

                                        <div class="card-body" data-toggle="tooltip" title="Amount Per unit area per month">
                                            <div class="row ">
                                                <div class="col-9 text-left">
                                                    <div class="text-xs font-weight-bold text-uppercase mb-1 mr-2" style="color: #ff00bb">Total</div>
                                                    <div class="h6 mb-0 font-weight-light  text-gray-800"><span id="MonthlyRate" runat="server"></span></div>
                                                </div>
                                                <div class="col-3 text-right">
                                                    <i class="fas fa-hand-holding-usd fa-1x text-gray-300"></i>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <br />

                            <div class="card-header bg-white border-top  ">

                                <div class="row">
                                    <div class=" col-6 text-left">
                                        <span class="fas fa-comment text-gray-500 mr-2"></span><span class="m-0 h6 font-weight-light text-gray-500">Comments <span class="badge badge-counter badge-danger  badge-pill mx-1" id="CommentCounter" runat="server">0</span></span>

                                    </div>
                                    <div class="col-md-6 text-right" data-toggle="tooltip" title="Put your comment here">
                                        <a class=" btn-sm text-warning font-weight-bolder" href="#" role="button" id="dropdownMenuLink2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span class="fas fa-plus text-danger"></span></a>



                                        <div class="dropdown-menu  dropdown-menu-right shadow animated--fade-in text-center" style="width: 300px" aria-labelledby="dropdownMenuLink2">


                                            <div class="row">
                                                <div class="col-1 ">
                                                </div>
                                                <div class="col-10 ">


                                                    <asp:TextBox ID="txtComment" class="form-control form-control-sm mr-2 " Style="border-color: #ff00bb" placeholder="Comment here..." TextMode="MultiLine" Height="110px" runat="server"></asp:TextBox>
                                                    <button class="btn btn-primary btn-sm w-50 mt-2" style="background-color: #ff00bb; display: none" type="button" disabled id="Pbutton1">
                                                        <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                                        Commenting
                                                    </button>
                                                    <asp:Button ID="B1" runat="server" Text="Comment" class="btn text-white text-left mt-2 btn-sm" OnClick="B1_Click" Style="background-color: #ff00bb; margin: 0px 0px 0px -165px" OnClientClick="myFunctionshop2()" />
                                                </div>
                                                <div class="col-1 ">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- Card Content - Collapse -->

                            <div class="card-body border-bottom mx-3">
                                <div style="overflow-y: scroll; height: 300px" class=" mx-3 " id="ComDiv" runat="server">
                                    <div class="page-content page-container" id="page-content">

                                        <div class="timeline  block ">
                                            <asp:Repeater ID="Repeater1" runat="server">
                                                <ItemTemplate>
                                                    <div class="tl-item">
                                                        <div class="tl-dot b-danger"></div>
                                                        <div class="tl-content">
                                                            <div class="text-gray-500 font-italic small"><%#Eval("comment")%></div>
                                                            <div class="small tl-date  mt-1"><span class="text-danger"><%#Eval("date","{0:dd/MM/yyyy}")%></span> by <span><%#Eval("bywho")%></span></div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>

                                    </div>
                                </div>
                                <main role="main" id="main" runat="server">

                                    <div class="starter-template">
                                        <center>


                                            <p class="lead">

                                                <i class="fas fa-comment-alt text-gray-300  fa-3x"></i>

                                            </p>
                                            <h6 class=" small font-italic" style="color: #ff00bb">No comment.</h6>
                                        </center>
                                    </div>



                                </main>


                            </div>
                            <br />

                            <div>
                                <div class="row">
                                    <div class="col-12 mx-3">
                                        <span class="fas fa-wave-square text-gray-500 mr-2"></span><span class="m-0 h6 font-weight-light text-gray-500">Activity <span class="badge badge-counter badge-warning badge-pill mx-1" id="ActiveCounter" runat="server">0</span></span>

                                    </div>
                                </div>
                                <hr />
                                <div style="overflow-y: scroll; height: 300px" class=" mx-3 ">
                                    <asp:Repeater ID="Repeater3" runat="server">
                                        <ItemTemplate>
                                            <div class="vertical-timeline vertical-timeline--animate vertical-timeline--one-column" style="height: 100px">

                                                <div class="vertical-timeline-item vertical-timeline-element">
                                                    <div>
                                                        <span class="vertical-timeline-element-icon bounce-in"><i class="badge badge-dot badge-dot-xl <%#Eval("badge")%>"></i></span>
                                                        <div class="vertical-timeline-element-content bounce-in">
                                                            <h4 class="timeline-title text-gray-900 font-weight-bold"><%#Eval("headline")%></h4>
                                                            <p><%#Eval("action")%></p>
                                                            at <span class="text-primary"><%#Eval("date","{0: HH:mm}")%></span> by <span class="text-warning font-italic"><%#Eval("bywho")%></span> <span class="vertical-timeline-element-date text-danger mx-2"><%#Eval("date","{0:dd/MM/yy}")%></span>
                                                        </div>
                                                    </div>
                                                </div>


                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <main role="main" id="main1" class="mt-3" runat="server">

                                        <div class="starter-template">
                                            <center>


                                                <p class="lead">

                                                    <i class="fas fa-wave-square text-gray-300  fa-3x"></i>

                                                </p>
                                                <h6 class="text-gray-300 font-weight-bold">No Activity</h6>
                                            </center>
                                        </div>



                                    </main>
                                </div>

                            </div>
                            <!-- Card Content - Collapse -->




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
    <div class="modal fade" id="exampleModal1111" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel1111" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <span class="fas fa-image mr-2" style="color: #ff00bb"></span><span class="h6 modal-title text-gray-900 font-weight-bolder" id="exampleModalLabel1111">Upload Shop Image</span>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row mb-3">
                        <div class="col-md-12">
                            <label class="text-gray-900">1--</label>
                            <asp:FileUpload ID="FileUpload2" class="file-select-name file-upload file-select file-select-button" runat="server" />
                        </div>


                    </div>
                    <div class="row mb-3">
                        <div class="col-md-12">
                            <label class="text-gray-900">2--</label>
                            <asp:FileUpload ID="FileUpload3" class="file-select-name file-upload file-select file-select-button" runat="server" />
                        </div>

                    </div>
                    <div class="row mb-3">
                        <div class="col-md-12">
                            <label class="text-gray-900">3--</label>
                            <asp:FileUpload ID="FileUpload4" class="file-select-name file-upload file-select file-select-button" runat="server" />
                        </div>

                    </div>
                    <div class="col-md-12 mt-4">

                        <asp:Button ID="Button15" runat="server" Text="Save" OnClick="Button15_Click" class="btn btn-block text-white btn-sm" Style="background-color: #ff00bb;" />

                    </div>
                </div>
                <center>
                    <div class="modal-footer">
                    </div>

                </center>
            </div>
        </div>
    </div>
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
                            <asp:TextBox ID="txtAreaExpand" placeholder="eg. +50 or  -50" runat="server" Style="border-color: #ff00bb" class=" form-control form-control-sm"></asp:TextBox>
                        </div>
                    </div>
                    <p class="text-danger small">*If the area is increased or decreased from another shop, select the shop below</p>
                    <div class="row mb-3">
                        <div class="col-md-12">

                            <span class="fas fa-arrow-alt-circle-right mr-2 mb-4" style="color: #ff00bb"></span><span class="small mb-4 text-danger">[ Select Shop ]</span>
                            <asp:DropDownList ID="ddlSHopExpand" class=" form-control form-control-sm" ClientIDMode="Static" runat="server">
                            </asp:DropDownList>
                            <asp:Label runat="server" class="small mt-2 mx-1" Style="color: #ff00bb" ID="lblSelectedText" ClientIDMode="Static"></asp:Label>

                        </div>
                    </div>

                    <div class="row">

                        <div class="col-md-12">

                            <center>
                                <button class="btn btn-primary btn-sm w-100" type="button" disabled id="Pbutton" style="display: none">
                                    <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                    Changing Area...
                                </button>
                                <asp:Button ID="btnExpandArea" runat="server" class="btn btn-sm btn-primary w-100" Text="Save..." OnClick="btnExpandArea_Click" OnClientClick="myFunctionshop()" />
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
    <div class="modal fade" id="exampleModalShopMerge" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel8shopM" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <span class="fas fa-code-branch mr-2" style="color: #ff00bb"></span><span class="h6 modal-title mb-1 text-gray-900 font-weight-bolder" id="exampleModalLabel8shopM">Merge Shop</span>
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
                    <div class="row"></div>
                    <div class="col-md-12">

                        <center>
                            <button class="btn btn-danger btn-sm w-100" type="button" disabled id="Pbutton3" style="display: none">
                                <span class="spinner-grow spinner-grow-sm" role="status" aria-hidden="true"></span>
                                Merging...
                            </button>
                            <asp:Button ID="btnMergeShop" runat="server" class="btn btn-sm btn-danger w-100" Text="Merge shops..." OnClick="btnMergeShop_Click" OnClientClick="myFunctionshop5()()" />
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
    <div class="modal fade" id="exampleModalAssignShop" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel8shopM" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <span class="fas fa-plus-circle mr-2" style="color: #ff00bb"></span><span class="h6 modal-title mb-1 text-gray-900 font-weight-bolder" id="exampleModalLabel8shopM">Assign Shop</span>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row mb-3">
                        <div class="col-md-12">
                            <asp:TextBox ID="txtNewArea" class="form-control form-control-sm" Style="border-color: #ff00bb" placeholder="New area" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-12">
                            <asp:TextBox ID="txtRate" class="form-control form-control-sm" Style="border-color: #ff00bb" placeholder="New rate [vat free]" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row"></div>
                    <div class="col-md-12">

                        <center>
                            <button class="btn btn-primary btn-sm w-100" type="button" disabled id="Pbutton35" style="display: none">
                                <span class="spinner-grow spinner-grow-sm" role="status" aria-hidden="true"></span>
                                Assigning...
                            </button>
                            <asp:Button ID="btnAssignArea" runat="server" class="btn btn-sm btn-primary w-100" Text="Assign shop" OnClick="btnAssignArea_Click" OnClientClick="myFunctionshop5()()" />
                        </center>


                    </div>
                </div>
                <center>
                    <div class="modal-footer">
                    </div>

                </center>
            </div>

        </div>
    </div>
    <div class="modal fade" id="exampleModalUpdatePrice" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel8shopM" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <span class="fas fa-plus-circle mr-2" style="color: #ff00bb"></span><span class="h6 modal-title mb-1 text-gray-900 font-weight-bolder" id="exampleModalLabel8shopM">Update Price</span>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row mb-3">
                        <div class="col-md-12">
                            <asp:TextBox ID="txtUpdatePrice" class="form-control form-control-sm" Style="border-color: #ff00bb" placeholder="New price" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row"></div>
                    <div class="col-md-12">

                        <center>
                            <button class="btn btn-primary btn-sm w-100" type="button" disabled id="Pbutton35" style="display: none">
                                <span class="spinner-grow spinner-grow-sm" role="status" aria-hidden="true"></span>
                                Assigning...
                            </button>
                            <asp:Button ID="btnUpdatePrice" runat="server" class="btn btn-sm btn-primary w-100" Text="Update price" OnClick="btnUpdatePrice_Click" OnClientClick="myFunctionshop5()()" />
                        </center>


                    </div>
                </div>
                <center>
                    <div class="modal-footer">
                    </div>

                </center>
            </div>

        </div>
    </div>

    <div class="modal fade" id="DeleteShop" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel8shopM" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <span class="fas fa-trash mr-2" style="color: #ff00bb"></span><span class="h6 modal-title mb-1 text-gray-900 font-weight-bolder" id="exampleModalLabel8shopM">Remove Shop</span>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <span class="fas fa-info-circle text-gray-300 mr-2"></span><span class="small text-danger text-center" style="text-align: center">All the data associated with the shop will be removed</span>
                    <div class="col-md-12 mt-2">

                        <center>
                            <button class="btn btn-primary btn-sm w-100" type="button" disabled id="Pbutton35" style="display: none">
                                <span class="spinner-grow spinner-grow-sm" role="status" aria-hidden="true"></span>
                                Assigning...
                            </button>
                            <asp:Button ID="btnRemovrShop" runat="server" class="btn btn-sm btn-danger w-100" Text="Remove" OnClick="btnRemovrShop_Click" />
                        </center>


                    </div>
                </div>
                <center>
                    <div class="modal-footer">
                    </div>

                </center>
            </div>

        </div>
    </div>
    <div class="modal fade" id="RenameShop" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel8shopM" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <span class="fas fa-edit mr-2" style="color: #ff00bb"></span><span class="h6 modal-title mb-1 text-gray-900 font-weight-bolder" id="exampleModalLabel8shopM">Rename Shop</span>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">

                            <asp:TextBox ID="txtRenameShop" class="form-control form-control-sm" Style="border-color: #ff00bb" placeholder="New Name" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 mt-4">

                            <center>
                                <button class="btn btn-primary btn-sm w-100" type="button" disabled id="Pbutton35" style="display: none">
                                    <span class="spinner-grow spinner-grow-sm" role="status" aria-hidden="true"></span>
                                    Assigning...
                                </button>
                                <asp:Button ID="btnRenameShop" runat="server" class="btn btn-sm btn-primary w-100" Text="Rename Shop" OnClick="btnRenameShop_Click" />
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
    <div class="modal fade" id="RemoveModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel8shopM" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <span class="h6 modal-title mb-1 text-gray-900 font-weight-bolder" id="exampleModalLabel8shopM"><span class="fas fa-trash mr-2" style="color: #ff00bb"></span>Remove Activity and Comment</span>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-12 mt-4">

                            <center>

                                <asp:Button ID="btnActivityRemove" runat="server" class="btn btn-sm btn-danger w-100" Text="Delete" OnClick="btnActivityRemove_Click" />
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
    <div class="modal fade" id="DelModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel8shopM" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <span class="h6 modal-title mb-1 text-gray-900 font-weight-bolder" id="exampleModalLabel8shopM"><span class="fas fa-trash mr-2" style="color: #ff00bb"></span>Delete All Image</span>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-12 mt-4">

                            <center>

                                <asp:Button ID="btnRemoveImage" runat="server" class="btn btn-sm btn-danger w-100" Text="Delete" OnClick="btnRemoveImage_Click" />
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
    <div class="modal fade" id="LocModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel8shopM" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <span class="fas fa-edit mr-2" style="color: #ff00bb"></span><span class="h6 modal-title mb-1 text-gray-900 font-weight-bolder" id="exampleModalLabel8shopM">Update Location</span>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">

                            <asp:TextBox ID="txtLocation" class="form-control form-control-sm" Style="border-color: #ff00bb" placeholder="Put Location" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 mt-4">

                            <center>
                                <button class="btn btn-primary btn-sm w-100" type="button" disabled id="Pbutton35" style="display: none">
                                    <span class="spinner-grow spinner-grow-sm" role="status" aria-hidden="true"></span>
                                    Assigning...
                                </button>
                                <asp:Button ID="btnUpdateLocation" runat="server" class="btn btn-sm btn-danger w-100" Text="Save Location" OnClick="btnUpdateLocation_Click" />
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
    <div class="modal fade" id="DescriptionShop" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel8shopM" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <span class="fas fa-edit mr-2" style="color: #ff00bb"></span><span class="h6 modal-title mb-1 text-gray-900 font-weight-bolder" id="exampleModalLabel8shopM">Shop description</span>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">

                            <asp:TextBox ID="txtShopDescription" class="form-control form-control-sm" TextMode="MultiLine" Height="100px" Style="border-color: #ff00bb" placeholder="Put description" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 mt-4">

                            <center>
                                <button class="btn btn-primary btn-sm w-100" type="button" disabled id="Pbutton35" style="display: none">
                                    <span class="spinner-grow spinner-grow-sm" role="status" aria-hidden="true"></span>
                                    Assigning...
                                </button>
                                <asp:Button ID="btnDescription" runat="server" class="btn btn-sm btn-danger w-100" Text="Save Description" OnClick="btnDescription_Click" />
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
    <div class="modal fade" id="ShopFree" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel8shopM" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <span class="fas fa-arrow-alt-circle-left mr-2 mt-2" style="color: #ff00bb"></span><span class="h6 modal-title mb-1 text-gray-900 font-weight-bolder" id="exampleModalLabel8shopM">Free This Shop</span>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 mx-4 card-body border-left-primary">
                            <p class="text-xs">
                                The customer who occupied this shop will be removed. 
                                The amount associated with this shop will be deducted from the due amount.
                            </p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 mt-1">

                            <center>
                                <button class="btn btn-primary btn-sm w-100" type="button" disabled id="Pbutton35" style="display: none">
                                    <span class="spinner-grow spinner-grow-sm" role="status" aria-hidden="true"></span>
                                    Assigning...
                                </button>
                                <asp:LinkButton ID="btnSetShopFree" runat="server" class="btn btn-sm btn-danger w-50" OnClick="btnSetShopFree_Click"><span class="fas fa-arrow-alt-circle-right mr-2"></span>Proceed..</asp:LinkButton>
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
                    function myFunctionshop5() {
                        var y = document.getElementById("<%=btnMergeShop.ClientID %>"); var x = document.getElementById("Pbutton3");
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
    <script>
                    function myFunctionshop() {
                        var y = document.getElementById("<%=btnExpandArea.ClientID %>"); var x = document.getElementById("Pbutton");
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
    <script>
                    function myFunctionshop2() {
                        var y = document.getElementById("<%=B1.ClientID %>"); var x = document.getElementById("Pbutton1");
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
    <script type='text/javascript'>
                    function openModal() {
                        $('#exampleModalShopMerge').modal('show');
                    }
    </script>
    <script type="text/javascript">
                    function HideORshowDiv() {
                        var div = document.getElementById('leftDiv');
                        var div2 = document.getElementById('rightDiv');
                        var div3 = document.getElementById('DivIcon');
                        if (div.className == "col-12 border-right") {

                            document.getElementById("leftDiv").style.transition = "all 0.1s";

                            div2.className = "col-lg-5"; div.className = "col-lg-7 border-right";
                            div3.className = "fas fa-arrows-alt-h  text-danger";
                        }
                        else {
                            div.className = "col-12 border-right";
                            document.getElementById("leftDiv").style.transition = "all 0.5s";

                            div2.className = "invisible";
                            div3.className = "fas fa-compress  text-danger";
                        }


                    }

    </script>
    <style>
        body {
            background-color: #f9f9fa
        }

        @media (min-width:992px) {
            .page-container {
                max-width: 1140px;
                margin: 0 auto
            }

            .page-sidenav {
                display: block !important
            }
        }

        .padding {
            padding: 2rem
        }

        .w-32 {
            width: 32px !important;
            height: 32px !important;
            font-size: .85em
        }

        .tl-item .avatar {
            z-index: 2
        }

        .circle {
            border-radius: 500px
        }

        .gd-warning {
            color: #fff;
            border: none;
            background: #f4c414 linear-gradient(45deg, #f4c414, #f45414)
        }

        .timeline {
            position: relative;
            border-color: rgba(160, 175, 185, .15);
            padding: 0;
            margin: 0
        }

        .p-4 {
            padding: 1.5rem !important
        }

        .block,
        .card {
            background: #fff;
            border-width: 0;
            border-radius: .25rem;
            box-shadow: 0 1px 3px rgba(0, 0, 0, .05);
            margin-bottom: 1.5rem
        }

        .mb-4,
        .my-4 {
            margin-bottom: 1.5rem !important
        }

        .tl-item {
            border-radius: 3px;
            position: relative;
            display: -ms-flexbox;
            display: flex
        }

            .tl-item > * {
                padding: 10px
            }

            .tl-item .avatar {
                z-index: 2
            }

            .tl-item:last-child .tl-dot:after {
                display: none
            }

            .tl-item.active .tl-dot:before {
                border-color: #448bff;
                box-shadow: 0 0 0 4px rgba(68, 139, 255, .2)
            }

            .tl-item:last-child .tl-dot:after {
                display: none
            }

            .tl-item.active .tl-dot:before {
                border-color: #448bff;
                box-shadow: 0 0 0 4px rgba(68, 139, 255, .2)
            }

        .tl-dot {
            position: relative;
            border-color: rgba(160, 175, 185, .15)
        }

            .tl-dot:after,
            .tl-dot:before {
                content: '';
                position: absolute;
                border-color: inherit;
                border-width: 2px;
                border-style: solid;
                border-radius: 50%;
                width: 10px;
                height: 10px;
                top: 15px;
                left: 50%;
                transform: translateX(-50%)
            }

            .tl-dot:after {
                width: 0;
                height: auto;
                top: 25px;
                bottom: -15px;
                border-right-width: 0;
                border-top-width: 0;
                border-bottom-width: 0;
                border-radius: 0
            }

        tl-item.active .tl-dot:before {
            border-color: #448bff;
            box-shadow: 0 0 0 4px rgba(68, 139, 255, .2)
        }

        .tl-dot {
            position: relative;
            border-color: rgba(160, 175, 185, .15)
        }

            .tl-dot:after,
            .tl-dot:before {
                content: '';
                position: absolute;
                border-color: inherit;
                border-width: 2px;
                border-style: solid;
                border-radius: 50%;
                width: 10px;
                height: 10px;
                top: 15px;
                left: 50%;
                transform: translateX(-50%)
            }

            .tl-dot:after {
                width: 0;
                height: auto;
                top: 25px;
                bottom: -15px;
                border-right-width: 0;
                border-top-width: 0;
                border-bottom-width: 0;
                border-radius: 0
            }

        .tl-content p:last-child {
            margin-bottom: 0
        }

        .tl-date {
            font-size: .85em;
            margin-top: 2px;
            min-width: 100px;
            max-width: 100px
        }

        .avatar {
            position: relative;
            line-height: 1;
            border-radius: 500px;
            white-space: nowrap;
            font-weight: 700;
            border-radius: 100%;
            display: -ms-flexbox;
            display: flex;
            -ms-flex-pack: center;
            justify-content: center;
            -ms-flex-align: center;
            align-items: center;
            -ms-flex-negative: 0;
            flex-shrink: 0;
            border-radius: 500px;
            box-shadow: 0 5px 10px 0 rgba(50, 50, 50, .15)
        }

        .b-warning {
            border-color: #f4c414 !important
        }

        .b-primary {
            border-color: #448bff !important
        }

        .b-danger {
            border-color: #f54394 !important
        }
    </style>
    <style>
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
            padding: 2rem 0 1rem
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
            margin-left: 100px;
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
                left: -100px;
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
