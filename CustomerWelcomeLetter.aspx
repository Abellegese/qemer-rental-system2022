<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="CustomerWelcomeLetter.aspx.cs" Inherits="advtech.Finance.Accounta.CustomerWelcomeLetter" %>

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
        .water {
            content: 'Raksym Trading PLC';
            align-content: center;
            justify-content: center;
            opacity: 0.2;
            z-index: -1;
            transform: rotate(-45deg);
        }
    </style>
    <title>Welcome Letter</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid pl-3 pr-3">

        <!-- Navbar -->

        <!-- End Navbar -->
        <!-- Header -->


        <!-- Table -->

        <div class="row">
            <div class="col-12">
                <div class="bg-white rounded-lg  mb-2 ">
                    <div class="card-header bg-white ">
                        <div class="row">
                            <div class="col-4 text-left">
                                <a class="btn btn-light btn-circle mr-2" id="buttonback" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back to customer details">

                                    <span class="fa fa-arrow-left text-danger"></span>

                                </a>
                            </div>
                            <div class="col-8 text-right">

                                <button name="b_print" onclick="printdiv('div_print');" type="button" title="Print" class="btn btn-sm btn-circle" style="background-color: #d46fe8" data-toggle="modal" data-target="#exampleModalCenter">
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



                                    <div class="row border-bottom" style="height: 90px">
                                        <div class="col-md-4 text-left">
                                            <img class="" src="../../asset/Brand/gh.jpg" alt="" width="110" height="80">
                                        </div>
                                        <div class="col-md-8 text-right">


                                            <h4 class="text-gray-900 font-weight-bold">WELCOME LETTER</h4>
                                        </div>

                                    </div>
                                    <div class="row " style="height: 60px">
                                        <div class="col-md-6 text-left"> 
                                            <span translate="no" class="h5 text-gray-900 font-weight-bold" id="campName" runat="server">RAKSYM TRADING PLC</span>
                                            <hr />
                                            <span class="fas fa-phone text-gray-500 text-left mr-1 mt-2"></span><span class="h6 small text-left font-weight-bold text-gray-900 mt-1" id="Contact" runat="server"></span>
                                            <br />
                                            <span class="fas fa-address-book text-left text-gray-500 mr-1 mb-3"></span><span class="h6 text-left small font-weight-bold text-gray-900 mt-1" id="CompAddress" runat="server"></span>
                                        
                                        </div>
                                        <div class="col-md-6 text-right">
                                            <span translate="no" class="small text-gray-900 font-weight-bold">February 13, 2022</span>
                                        </div>
                                    </div>
                                         <br />
                                    <div class="row  mt-5">
                                        <div class="col-md-12 text-left">
                                            <span class=" h6 text-gray-900 " style="height: 100px">To: </span><span style="height: 100px" class="h6 mx-2 text-gray-900 font-weight-bold font-italic" id="Name" runat="server"></span>
                                            [<span class="text-gray-900 small font-weight-bold mt-3">Shop No: </span><span class="text-gray-900 small mx-1 font-weight-bold mt-3" id="ShopNo" runat="server"></span>]
                                        </div>
                                    </div>
                                </div>

                                <div class="card-body " style="height: 1300px">

                                    <h6 class="text-gray-900 font-weight-bold text-left mb-3">Dear</h6>

                                    <p class="h6  text-gray-900 mb-4" contenteditable="true" style="text-align: justify; text-indent: 0px; line-height: 35px;font-size:20px">
                                        I wanted to write and thank you again for choosing Raksym Trading PLC. We would like to remind that you are occupied shop# <span id="shopNumber" class="text-gray-900 font-weight-bold" runat="server"></span>
                                        has an area of <span id="areaSpan" class="text-gray-900 font-weight-bold" runat="server"></span> m<sup>2</sup> and monthly vat included rate of <span id="rate" class="text-gray-900 font-weight-bold" runat="server"></span> ETB/month.
                                        
                                        You are agreed to pay service charge {<span id="ServiceCharge" class="text-gray-900 font-weight-bold" runat="server"></span>/Month} included amount of <span class="font-weight-bold text-gray-900" id="TotalReceivable" runat="server"></span> <span id="period" runat="server"></span>.  We will work hard to ensure your satisfaction and value the trust you have placed in us.  I wish you success and hope that we can service your needs in the future! 

                                    </p>

                                    <div class="row mt-xl-4">
                                        <div class="col-md-12 text-right">
                                            <h5 class="font-italic mb-2 text-gray-900 font-weight-bold">Sincerely</h5>
                                            <h5 class="font-italic text-gray-900 font-weight-bold">Adminstration Office</h5>
                                            <center>
                                                <h1 class="water h1  font-weight-bolder" style="font-size: 75px">Raksym Trading PLC</h1>
                                            </center>
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
