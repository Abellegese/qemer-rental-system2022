<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.master" AutoEventWireup="true" CodeBehind="backupdatabase.aspx.cs" Inherits="advtech.Finance.Accounta.backupdatabase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Create database backup</title>
    <style>
        .switch {
            position: relative;
            display: inline-block;
            width: 60px;
            height: 34px;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 26px;
                width: 26px;
                left: 4px;
                bottom: 4px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(26px);
            -ms-transform: translateX(26px);
            transform: translateX(26px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 34px;
        }

            .slider.round:before {
                border-radius: 50%;
            }
    </style>
    <style>

        #snackbar1 {
  visibility: hidden; /* Hidden by default. Visible on click */
  min-width: 190px; /* Set a default minimum width */
  margin-left: -110px; /* Divide value of min-width by 2 */
  background-color: #2196F3; /* Black background color */
  color: #fff; /* White text color */
  text-align: center; /* Centered text */
  border-radius: 2px; /* Rounded borders */
  padding: 16px; /* Padding */
  position: fixed; /* Sit on top of the screen */
  z-index: 1; /* Add a z-index if needed */
  left: 40%; /* Center the snackbar */
  bottom: 30px; /* 30px from the bottom */
}

/* Show the snackbar when clicking on a button (class added with JavaScript) */
#snackbar1.show {
  visibility: visible; /* Show the snackbar */
  /* Add animation: Take 0.5 seconds to fade in and out the snackbar.
  However, delay the fade out process for 2.5 seconds */
  -webkit-animation: fadein 0.5s, fadeout 0.5s 2.5s;
  animation: fadein 0.5s, fadeout 0.5s 2.5s;
}

/* Animations to fade the snackbar in and out */
@-webkit-keyframes fadein {
  from {bottom: 0; opacity: 0;}
  to {bottom: 30px; opacity: 1;}
}

@keyframes fadein {
  from {bottom: 0; opacity: 0;}
  to {bottom: 30px; opacity: 1;}
}

@-webkit-keyframes fadeout {
  from {bottom: 30px; opacity: 1;}
  to {bottom: 0; opacity: 0;}
}

@keyframes fadeout {
  from {bottom: 30px; opacity: 1;}
  to {bottom: 0; opacity: 0;}
}
    </style>
    <style>

        #snackbar {
  visibility: hidden; /* Hidden by default. Visible on click */
  min-width: 250px; /* Set a default minimum width */
  margin-left: -150px; /* Divide value of min-width by 2 */
  background-color: #ff6a00; /* Black background color */
  color: #fff; /* White text color */
  text-align: center; /* Centered text */
  border-radius: 2px; /* Rounded borders */
  padding: 16px; /* Padding */
  position: fixed; /* Sit on top of the screen */
  z-index: 1; /* Add a z-index if needed */
  left: 50%; /* Center the snackbar */
  bottom: 30px; /* 30px from the bottom */
}

/* Show the snackbar when clicking on a button (class added with JavaScript) */
#snackbar.show {
  visibility: visible; /* Show the snackbar */
  /* Add animation: Take 0.5 seconds to fade in and out the snackbar.
  However, delay the fade out process for 2.5 seconds */
  -webkit-animation: fadein 0.5s, fadeout 0.5s 2.5s;
  animation: fadein 0.5s, fadeout 0.5s 2.5s;
}

/* Animations to fade the snackbar in and out */
@-webkit-keyframes fadein {
  from {bottom: 0; opacity: 0;}
  to {bottom: 30px; opacity: 1;}
}

@keyframes fadein {
  from {bottom: 0; opacity: 0;}
  to {bottom: 30px; opacity: 1;}
}

@-webkit-keyframes fadeout {
  from {bottom: 30px; opacity: 1;}
  to {bottom: 0; opacity: 0;}
}

@keyframes fadeout {
  from {bottom: 30px; opacity: 1;}
  to {bottom: 0; opacity: 0;}
}
    </style>
    <link href="../../asset/css/starter-template.css" rel="stylesheet" />
    <script type="text/javascript">
        function GetSystemStatus() {
            PageMethods.bindBackupInfoCheck(SuccessStatus, FailureStatus);
        }
        function SuccessStatus(result) {
            var y = document.getElementById("HideOrShow");
            var checkAuto1 = document.getElementById("AutoCheckBox");
            if (result == "On") {
                checkAuto1.checked = true;
                y.style.display = "block";
            }
            else {
                checkAuto1.checked = false;
                y.style.display = "none";
            }
        }
        function FailureStatus(error) {
            alert(error);
        }
        $(document).ready(function () {
            GetSystemStatus();
        });
    </script>
    <script>
        function UpdateAutomationStatusF() {
            PageMethods.UpdateAutomationStatus();
        }
        function myFunctionSnackv() {
            // Get the snackbar DIV
            var x = document.getElementById("snackbar1");

            // Add the "show" class to DIV
            x.className = "show";

            // After 3 seconds, remove the show class from DIV
            setTimeout(function () { x.className = x.className.replace("show", ""); }, 6000);
        }
        $(function () {
            $("[id*=AutoCheckBox]").bind("click", function () {
                UpdateAutomationStatusF();
                $("#HideOrShow").toggle(this.checked);
                myFunctionSnackv();
            });
        });
    </script>
    <script>
        function myFunctionSnack() {
            // Get the snackbar DIV
            var x = document.getElementById("snackbar1");

            // Add the "show" class to DIV
            x.className = "show";

            // After 3 seconds, remove the show class from DIV
            setTimeout(function () { x.className = x.className.replace("show", ""); }, 6000);
        }
    </script>
  

    <script type="text/javascript">

                window.addEventListener('load', (event) => {
                    var x = document.getElementById("Pbutton");
                    x.style.display = "none";
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID='ScriptManager1' runat='server' EnablePageMethods='true' />

        <div class="container-fluid ">
            <span id="GetDirectory" runat="server" visible="false"></span>
        <div id="snackbar1">Auto backup system changes. The system will be adjusted automatically.</div>
        <br />
        <div class="modal fade" id="exampleModalEdit" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-sm" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title text-gray-900 font-weight-bold h6" id="exampleModalLabel"><span class="fas fa-database mr-2" style="color: #9d469d"></span>Choose Backup Cycle</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <asp:DropDownList ID="ddlCycle" class="form-control form-control-sm" style="border-color:#c80eb9" ClientIDMode="Static" runat="server">
                                    <asp:ListItem>Daily</asp:ListItem>
                                    <asp:ListItem>Weekly</asp:ListItem>
                                    <asp:ListItem>Monthly</asp:ListItem>
                                    <asp:ListItem>Yearly</asp:ListItem>
                                    <asp:ListItem>Minutely</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="row">


                            <div class="col-md-12">
           
                            </div>
                        </div>

                        <center>
                            <div class="modal-footer">                   
                                <button type="button" id="updatePeriodButton" onclick="UpdateAutomationPeriod();" class="btn btn-sm btn-success"><span class="fas fa-save text-white mr-2"></span>Save Cycle</button>
                            </div>

                        </center>
                    </div>
                </div>
            </div>

        </div>
        <main role="main" class="container">

            <div class="starter-template">
                <h2 style="font-weight: bold" class="text-gray-900">Welcome to Database Backup withward</h2>
                <p class="lead">Creating your backup periodially to avoid data loss</p>

                <label class="switch mt-2 mb-1" data-toggle="tooltip" title="Turn On/Off Automatic backup.">
                    <input type="checkbox" id="AutoCheckBox" >
                    <span class="slider round"></span>
                </label>
                <br />
                <div id="HideOrShow">
                    <span class="badge badge-success mt-3 text-uppercase" id="cycleSpan" runat="server" clientidmode="Static" data-toggle="tooltip" title="Database backup cycle"></span>
               
                 
                    <button data-toggle="modal" id="cycleSetting" runat="server" data-target="#exampleModalEdit" type="button" class="btn btn-default"><span class="fas fa-cog text-gray-400 ml-2"></span></button>
                </div>

                <center>
                    <button class="w-25 mt-2 btn btn-lg btn-danger" type="button" disabled id="Pbutton" style="display: none">
                        <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                        Creating Backup...
                    </button>
                </center>

                <asp:Button ID="btnCreateDatabase" class=" mt-2  btn btn-lg btn-danger" runat="server" Text="Create database backup manually" OnClick="btnCreateDatabase_Click" OnClientClick="myFunctionshop()" />

            </div>

        </main>

        
        <script type="text/javascript">
                    function GetPeriod() {
                        PageMethods.bindBackupInfo(Success, Failure);
                    }

                    function Success(result) {
                        var x = document.getElementById("cycleSpan");
                        x.innerHTML = result;
                    }

                    function Failure(error) {
                        alert(error);
                    }
        
                    function UpdateAutomationPeriod() {
                        var period = $("#ddlCycle option:selected").text();
                        PageMethods.UpdateAutomationPeriod(period);
                        $('#exampleModalEdit').modal('hide');
                        GetPeriod();

                    }
        </script>
        <script>
                    function myFunctionshop() {
                        var y = document.getElementById("<%=btnCreateDatabase.ClientID %>");
                        var x = document.getElementById("Pbutton");
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

            
            <script type="text/javascript">

                    function GetPeriod() {
                        PageMethods.bindBackupInfo(Success, Failure);
                    }

                    function Success(result) {
                        var x = document.getElementById("cycleSpan");
                        x.innerHTML = result;
                    }

                    function Failure(error) {
                        alert(error);
                    }
                    $(document).ready(function () {
                        GetPeriod();
                    });
            </script>
    </div>
</asp:Content>
