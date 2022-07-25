<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="NoticeLetter.aspx.cs" Inherits="advtech.Finance.Accounta.NoticeLetter" %>

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
    <style type="text/css">
   .tox-statusbar__branding
	{
		display:none;
	}
	
	.tox-notification__body
	{
		display:none;
	}
	
	.tox-notifications-container
	{
		display:none;
	}
    </style>

    <script type="text/javascript">
        window.addEventListener('load', (event) => {
            var x = document.getElementById("myDIV5");
            x.style.display = "none";
        });
    </script>
    <script type="text/javascript">
        window.addEventListener('load', (event) => {
            var x = document.getElementById("myDIV5");
            x.style.display = "none";
        });
    </script>
    <script type="text/javascript">
        window.addEventListener('load', (event) => {
            var x = document.getElementById("myDIV5f");
            x.style.display = "none";
        });
    </script>
    <script type="text/javascript">
        window.addEventListener('load', (event) => {
            var x = document.getElementById("Pbutton1");
            x.style.display = "none";
        });
    </script>
    <style>
        .water {
            content: 'Raksym Trading PLC';
            align-content: center;
            justify-content: center;
            opacity: 0.6;
            z-index: 1;
            transform: rotate(-45deg);
        }
    </style>

    <title>Notice Letter</title>
    <script src="https://cdn.tiny.cloud/1/jvjf5ya7jggxhoovivd3w4jce92a4z405yj0gpwivw8pmut6/tinymce/6/tinymce.min.js" referrerpolicy="origin"></script>
    <script>
        tinymce.init({
            selector: 'textarea#editor2',
            browser_spellcheck: true,
            contextmenu: true,
            plugins: [
                'a11ychecker', 'advlist', 'advcode', 'advtable', 'autolink', 'checklist', 'export',
                'lists', 'link', 'image', 'charmap', 'preview', 'anchor', 'searchreplace', 'visualblocks',
                'powerpaste', 'fullscreen', 'formatpainter', 'insertdatetime', 'media', 'table', 'help', 'wordcount'
            ],
            toolbar: 'undo redo | formatpainter casechange blocks | bold italic backcolor | ' +
                'alignleft aligncenter alignright alignjustify | ' +
                'bullist numlist checklist outdent indent | removeformat | a11ycheck code table help'
        });
    </script>
    <script>

        var uri = 'http://localhost/api/let';
        function formatItem1(item) {
            return item.letter_name;
        }
        function formatItem2(item) {
            return item.heading;
        }
        function formatItem3(item) {
            return item.part1;
        }
        function formatItem4(item) {
            return item.part3;
        }
        function formatItem5(item) {
            return item.part5;
        }
        function formatItem6(item) {
            return item.part7;
        }
        function find() {
            var id = $("#ddlCustomLetterName").val();
            var z = document.getElementById("CustomLetterName");
            var loading = document.getElementById("Loading");
            loading.style.display = "block";
            $.getJSON(uri + '/' + id)
                .done(function (data) {
                    $("#txtLetterName").val(formatItem1(data));
                    $("#txtHeading").val(formatItem2(data));
                    $("#txtCustomePart1").val(formatItem3(data));
                    $("#txtCustomePart3").val(formatItem4(data));
                    $("#txtCustomePart5").val(formatItem5(data));

                    $("#txtCustomePart7").val(formatItem6(data));
                    z.innerText = formatItem1(data) + " Binded";
                    loading.style.display = "none";
                    HideOrShowButton();
                })
                .fail(function (jqXHR, textStatus, err) {
                    $('#product').text('Error: ' + err);
                });

        }
        function HideOrShowButton() {
            var x = document.getElementById("updateDiv");
            var y = document.getElementById("Bd1");
            var z = document.getElementById("CustomLetterName");
            
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
            if (z.style.display === "none") {
                z.style.display = "block";
            } else {
                z.style.display = "block";
            }
        }
    </script>

    <script>
        function GetSystemStatus() {
            document.getElementById("editor").innerText = "Fifth Avenue, New York City";
        }
    </script>
    <script>
        var x = getElementById("editor2").innerHTML;
        function getValues() {
            alert(x);
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID='ScriptManager1' runat='server' EnablePageMethods='true' />
    <div class="container-fluid pl-3 pr-3" style="position:absolute;">
        <div class="modal fade" id="ModalBody" tabindex="-1" role="dialog" aria-labelledby="MHeader" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h6 class="modal-title text-gray-900 h6 text-uppercase font-weight-bold" id="MHeader"><span class="fas fa-edit mr-2" style="color: #ff00bb"></span>Customize Body Content</h6>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">

                        <div class="card-body border-left border-success">
                            <div class="row mb-4">
                                <div class="col-4">
                                    <span class="small text-gray-900">Edit Name Section</span>
                                    <asp:TextBox ID="txtpart1" class="form-control form-control-sm" Height="30px" runat="server" Text="ስም" ReadOnly="true"></asp:TextBox>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-1">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="bn" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger font-weight-bolder" for="bn">Bold</label>
                                    </div>

                                </div>
                                <div class="col-1">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="in1" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger font-italic" for="in1">Italic</label>
                                    </div>

                                </div>
                                <div class="col-1">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="un" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger" for="un" style="text-decoration: underline">Underline</label>
                                    </div>

                                </div>
                                <div class="col-3 mx-4">

                                    <div class="form-group">

                                        <div class="input-group input-group-alternative">

                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <asp:TextBox ID="txtNameFontSize" class="form-control form-control-sm" data-toggle="tooltip" Style="border-color: #ff0000;" data-placement="bottom" title="font size" TextMode="Number" Height="25px" Width="70px" runat="server" Text="12"></asp:TextBox>

                                                    <div class="input-group-prepend " style="height: 25px">
                                                        <span class="input-group-text ">px</span>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>


                                </div>
                                <div class="col-3">

                                    <div class="form-group">

                                        <div class="input-group input-group-alternative">

                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <asp:TextBox ID="txtNameSize" class="form-control form-control-sm" data-toggle="tooltip" Style="border-color: #ff0000;" data-placement="bottom" title="line height" TextMode="Number" Height="25px" Width="70px" runat="server" Text="12"></asp:TextBox>

                                                    <div class="input-group-prepend " style="height: 25px">
                                                        <span class="input-group-text ">px</span>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="card-body border-left border-warning">
                            <div class="row mb-4">
                                <div class="col-12 text-center">
                                    <span class="small text-gray-900">Edit Headline Section</span>
                                    <asp:TextBox ID="txtHeadlineEdit" class="form-control form-control-sm" TextMode="MultiLine" Height="50px" runat="server"></asp:TextBox>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-1">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="bh" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger font-weight-bold" for="bh">Bold</label>
                                    </div>

                                </div>
                                <div class="col-1">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="ih" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger font-italic" for="ih">Italic</label>
                                    </div>

                                </div>
                                <div class="col-1">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="uh" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger" for="uh" style="text-decoration: underline">Underline</label>
                                    </div>

                                </div>

                                <div class="col-3 mx-4">

                                    <div class="form-group">

                                        <div class="input-group input-group-alternative">

                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <asp:TextBox ID="txtHeadFontSize" class="form-control form-control-sm" data-toggle="tooltip" Style="border-color: #ff0000;" data-placement="bottom" title="font size" TextMode="Number" Height="25px" Width="70px" runat="server" Text="12"></asp:TextBox>

                                                    <div class="input-group-prepend " style="height: 25px">
                                                        <span class="input-group-text ">px</span>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>


                                </div>
                                <div class="col-3">

                                    <div class="form-group">

                                        <div class="input-group input-group-alternative">

                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <asp:TextBox ID="txtHeadlineLine" class="form-control form-control-sm" data-toggle="tooltip" Style="border-color: #ff0000;" data-placement="bottom" title="line height" TextMode="Number" Height="25px" Width="70px" runat="server" Text="12"></asp:TextBox>

                                                    <div class="input-group-prepend " style="height: 25px">
                                                        <span class="input-group-text ">px</span>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>
                        <div class="card-body border-left border-primary">
                            <div class="row mb-4">
                                <div class="col-12 text-center">
                                    <span class="small text-gray-900">Edit Body Section</span>
                                    <asp:TextBox ID="txtpart2" ClientIDMode="Static" class="form-control form-control-sm" Height="50px" TextMode="MultiLine" runat="server" Text="በራክሲም ንግድ ስራ ኃ/የተ/የግል ማህበር ህንፃ ላይ ለሱቅ ግልጋሎት የሚሆን ቦታ ተከራይተው እየሰሩ መሆኑ ይታወቃል ፡፡ በመሆኑም ከ"></asp:TextBox>

                                </div>
                            </div>
                            <div class="row mb-4">
                                <div class="col-12">

                                    <asp:TextBox ID="txtpart4" ClientIDMode="Static" class="form-control form-control-sm" Height="40px" TextMode="MultiLine" runat="server" Text="የነበረው የክፍያ ጊዜ ስለተጠናቀቀ "></asp:TextBox>

                                </div>
                            </div>

                            <div class="row">
                                <div class="col-12">

                                    <asp:TextBox ID="txtpart6" ClientIDMode="Static" class="form-control form-control-sm" Height="40px" TextMode="MultiLine" runat="server" ReadOnly="false" Text="ያለውን የቤት ኪራይ ገቢ ከጠቅላላ አገልግሎት ክፍያ ጋር ብር" data-toggle="tooltip" title="Before Money"></asp:TextBox>

                                </div>
                            </div>
    
                            <span class="text-success text-xs border-top border-bottom">Money Section Goes Here</span>
       
                            <div class="row mb-4">
                                <div class="col-12">
                                  
                                    <asp:TextBox ID="txtpart7" ClientIDMode="Static" class="form-control form-control-sm" Height="60px" TextMode="MultiLine" runat="server" ReadOnly="false" Text="ውላችን ላይ ባለው አንቀፅ ሶስት መሰረት ቀጣዩን የ 3 ወር ክፍያ በቀጣዮቹ 15 ተከታታይ ቀናት ውስጥ እንድትከፍሉ እንጠይቃለን፡፡" data-toggle="tooltip" title="After Money"></asp:TextBox>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-1">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="bodyB" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger font-weight-bolder" for="bodyB">Bold</label>
                                    </div>

                                </div>
                                <div class="col-1">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="bodyI" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger font-italic" for="bodyI">Italic</label>
                                    </div>

                                </div>
                                <div class="col-1">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="bodyU" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger" for="bodyU" style="text-decoration: underline">Underline</label>
                                    </div>

                                </div>

                                <div class="col-1 mx-5">
                                    <div class="form-group">
                                        <div class="input-group input-group-alternative">

                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                        <button  class="btn btn-sm btn-light" Height="10" type="button" id="AddButton"  data-toggle="tooltip" data-placement="bottom" title="Add New Line."><span  class="fas  mb-4 fa-align-justify text-danger"></span></button>
                                                    </div>
                                                </div>
                                            </div>
                                    </div>
                                </div>
                                <div class="col-3 ">
                                    <div class="form-group">

                                        <div class="input-group input-group-alternative">

                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <asp:TextBox ID="txtBodySize" class="form-control form-control-sm" data-toggle="tooltip" Style="border-color: #ff0000;" data-placement="bottom" title="font size" TextMode="Number" Height="25px" Width="70px" runat="server" Text="12"></asp:TextBox>

                                                    <div class="input-group-prepend " style="height: 25px">
                                                        <span class="input-group-text ">px</span>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>


                                </div>
                                <div class="col-3">

                                    <div class="form-group">

                                        <div class="input-group input-group-alternative">

                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <asp:TextBox ID="txtBodyLine" class="form-control form-control-sm" data-toggle="tooltip" Style="border-color: #ff0000;" data-placement="bottom" title="line height" TextMode="Number" Height="25px" Width="70px" runat="server" Text="12"></asp:TextBox>

                                                    <div class="input-group-prepend " style="height: 25px">
                                                        <span class="input-group-text ">px</span>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>

                        <div class="card-body border-left border-danger">
                            <div class="row mb-4">
                                <div class="col-4">

                                    <asp:TextBox ID="txtpart3" class="form-control form-control-sm" Height="40px" ReadOnly="true" runat="server" Text="ነሃሴ እስከ ጥቅምት {period section}"></asp:TextBox>

                                </div>
                            </div>

                            <div class="row">
                                <div class="col-1">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="bp" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger font-weight-bolder" for="bp">Bold</label>
                                    </div>

                                </div>
                                <div class="col-1">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="ip" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger font-italic" for="ip">Italic</label>
                                    </div>

                                </div>
                                <div class="col-1">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="up" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger" for="up" style="text-decoration: underline">Underline</label>
                                    </div>

                                </div>
                                <div class="col-1 text-right mx-5">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="visPeriodSec" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger" for="visPeriodSec">Visible</label>
                                    </div>

                                </div>
                                <div class="col-3 mx-2">

                                    <div class="form-group">

                                        <div class="input-group input-group-alternative">

                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <asp:TextBox ID="txtPeriodSize" class="form-control form-control-sm" data-toggle="tooltip" Style="border-color: #ff0000;" data-placement="bottom" title="font size" TextMode="Number" Height="25px" Width="70px" runat="server" Text="12"></asp:TextBox>

                                                    <div class="input-group-prepend " style="height: 25px">
                                                        <span class="input-group-text ">px</span>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                                <div class="col-3">

                                    <div class="form-group">

                                        <div class="input-group input-group-alternative">

                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <asp:TextBox ID="txtPeriodLine" class="form-control form-control-sm" data-toggle="tooltip" Style="border-color: #ff0000;" data-placement="bottom" title="line height" TextMode="Number" Height="25px" Width="70px" runat="server" Text="12"></asp:TextBox>

                                                    <div class="input-group-prepend " style="height: 25px">
                                                        <span class="input-group-text ">px</span>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                            </div>


                            <div class="row mb-4 text-center">
                                <div class="col-12 ">
                                    <center>
                                        <span class="small text-gray-900 font-weight-bold">Edit Year Section</span>
                                    </center>
                                    <asp:TextBox ID="txtpart5" class="form-control form-control-sm" Height="40px" runat="server" ReadOnly="true" Text="2014 {year section}"></asp:TextBox>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-1">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="by" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger font-weight-bolder" for="by">Bold</label>
                                    </div>

                                </div>
                                <div class="col-1">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="iy" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger" style="font-style: italic" for="iy">Italic</label>
                                    </div>

                                </div>
                                <div class="col-1">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="uy" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger" for="uy" style="text-decoration: underline">Underline</label>
                                    </div>

                                </div>
                                <div class="col-1 text-right mx-5">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="visYearCheck" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger" for="visYearCheck">Visible</label>
                                    </div>

                                </div>
                                <div class="col-3 mx-2">

                                    <div class="form-group">

                                        <div class="input-group input-group-alternative">

                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <asp:TextBox ID="txtYearSize" class="form-control form-control-sm" data-toggle="tooltip" Style="border-color: #ff0000;" data-placement="bottom" title="font size" TextMode="Number" Height="25px" Width="70px" runat="server" Text="12"></asp:TextBox>

                                                    <div class="input-group-prepend " style="height: 25px">
                                                        <span class="input-group-text ">px</span>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-3">

                                    <div class="form-group">

                                        <div class="input-group input-group-alternative">

                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <asp:TextBox ID="txtYearLine" class="form-control form-control-sm" data-toggle="tooltip" Style="border-color: #ff0000;" data-placement="bottom" title="line height" TextMode="Number" Height="25px" Width="70px" runat="server" Text="12"></asp:TextBox>

                                                    <div class="input-group-prepend " style="height: 25px">
                                                        <span class="input-group-text ">px</span>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-body border-left border-info">
                            <h6 class="" style="color: #ff00bb"><span class="fas fa-cog mr-1 text-gray-300"></span>Margin</h6>
                            <div class="row">

                                <div class="col-3 ">
                                    <div class="form-group">

                                        <div class="input-group input-group-alternative">

                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <asp:TextBox ID="txtmariginleft" class="form-control form-control-sm" data-toggle="tooltip" Style="border-color: #ff0000;" data-placement="bottom" title="marigin left" MaxLength="2" TextMode="Number" Height="25px" Width="70px" runat="server" Text="12"></asp:TextBox>

                                                    <div class="input-group-prepend " style="height: 25px">
                                                        <span class="input-group-text ">px</span>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>


                                </div>
                                <div class="col-3">
                                    <div class="form-group">

                                        <div class="input-group input-group-alternative">

                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <asp:TextBox ID="txtmariginright" class="form-control form-control-sm" data-toggle="tooltip" Style="border-color: #ff0000;" data-placement="bottom" title="marigin right" TextMode="Number" Height="25px" Width="70px" runat="server" Text="12"></asp:TextBox>

                                                    <div class="input-group-prepend " style="height: 25px">
                                                        <span class="input-group-text ">px</span>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>

                        <div class="card-body border-left border-secondary">





                            <div class="row mb-4 text-center">
                                <div class="col-12 ">
                                    <center>
                                        <span class="small text-gray-900 font-weight-bold">Edit Money Section</span>
                                    </center>
                                    <asp:TextBox ID="txtMoney" class="form-control form-control-sm" Height="40px" runat="server" ReadOnly="true" Text="45,670.00 {money section}"></asp:TextBox>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-1">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="mb" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger font-weight-bolder" for="mb">Bold</label>
                                    </div>

                                </div>
                                <div class="col-1">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="mi" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger" style="font-style: italic" for="mi">Italic</label>
                                    </div>

                                </div>
                                <div class="col-1">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="mu" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger" for="mu" style="text-decoration: underline">Underline</label>
                                    </div>

                                </div>
                                <div class="col-1 text-right mx-5">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="visMonetCheck" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger" for="visMonetCheck">Visible</label>
                                    </div>

                                </div>
                                <div class="col-3 mx-2">

                                    <div class="form-group">

                                        <div class="input-group input-group-alternative">

                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <asp:TextBox ID="txtMoneyFontSize" class="form-control form-control-sm" data-toggle="tooltip" Style="border-color: #ff0000;" data-placement="bottom" title="font size" TextMode="Number" Height="25px" Width="70px" runat="server" Text="12"></asp:TextBox>

                                                    <div class="input-group-prepend " style="height: 25px">
                                                        <span class="input-group-text ">px</span>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-3">

                                    <div class="form-group">

                                        <div class="input-group input-group-alternative">

                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <asp:TextBox ID="txtMoneyLineHeight" class="form-control form-control-sm" data-toggle="tooltip" Style="border-color: #ff0000;" data-placement="bottom" title="line height" TextMode="Number" Height="25px" Width="70px" runat="server" Text="12"></asp:TextBox>

                                                    <div class="input-group-prepend " style="height: 25px">
                                                        <span class="input-group-text ">px</span>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <center>
                        <div class="modal-footer">
                            <div id="myDIV5" class="spinner-border text-danger spinner-border-sm  mr-4 mt-2" role="status">
                                <span class="sr-only">Loading.ffrfyyrg..</span>
                            </div>

                            <asp:Button ID="btnBodySave" runat="server" class="btn btn-sm btn-danger " OnClick="btnBodySave_Click" OnClientClick="myFunctionshop()" Text="Save Changes" />


                        </div>

                    </center>
                </div>
            </div>
        </div>


        <div class="modal fade" id="ModalHeader" tabindex="-1" role="dialog" aria-labelledby="MHeader" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h6 class="modal-title text-gray-900 h6 text-uppercase font-weight-bold" id="MHeader"><span class="fas fa-edit text-gray-300 mr-2 text-uppercase"></span>Edit HEADer and footer Content</h6>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="card-body border-left border-primary">
                            <div class="row mb-3">
                                <div class="col-md-12">
                                    <asp:TextBox ID="txtHeadingEdit" class="form-control form-control-sm" Height="70px" runat="server" Text="ራክሲም ንግድ ስራ ኃ/የ/የግል/ማ"></asp:TextBox>
                                    <hr />

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-1">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="bold" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger font-weight-bolder" for="bold">B</label>
                                    </div>

                                </div>
                                <div class="col-1">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="italic" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger font-italic" for="italic">I</label>
                                    </div>

                                </div>
                                <div class="col-1">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="underline" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger" for="underline" style="text-decoration: underline">U</label>
                                    </div>

                                </div>
                                <div class="col-3">
                                    <div class="form-group">

                                        <div class="input-group input-group-alternative">

                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <asp:TextBox ID="txtFontsize" class="form-control form-control-sm" data-toggle="tooltip" Style="border-color: #ff0000;" data-placement="bottom" title="font size" TextMode="Number" Height="25px" Width="70px" runat="server" Text="12"></asp:TextBox>

                                                    <div class="input-group-prepend " style="height: 25px">
                                                        <span class="input-group-text ">px</span>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                                <div class="col-3">
                                    <div class="form-group">

                                        <div class="input-group input-group-alternative">

                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <asp:TextBox ID="txtLineHeight" class="form-control form-control-sm" data-toggle="tooltip" Style="border-color: #ff0000;" data-placement="bottom" title="line height" TextMode="Number" Height="25px" Width="70px" runat="server" Text="12"></asp:TextBox>

                                                    <div class="input-group-prepend " style="height: 25px">
                                                        <span class="input-group-text ">px</span>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                                <div class="col-3">
                                    <div class="form-group">

                                        <div class="input-group input-group-alternative">

                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <div class="input-group-prepend " style="height: 30px">
                                                        <span class="input-group-text ">align</span>
                                                    </div>
                                                    <asp:DropDownList ID="ddlTextAlignment" class="form-control form-control-sm" Height="30px" runat="server">
                                                    </asp:DropDownList>

                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="card-body border-left border-danger">
                            <h6 class="text-gray-900 small mb-3">Edit Address and contact section</h6>
                            <div class="row">
                                <div class="col-1">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="ba" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger font-weight-bolder" for="ba">B</label>
                                    </div>

                                </div>
                                <div class="col-1">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="ia" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger font-italic" for="ia">I</label>
                                    </div>

                                </div>
                                <div class="col-1">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="ua" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger" for="ua" style="text-decoration: underline">U</label>
                                    </div>

                                </div>
                                <div class="col-3">
                                    <div class="form-group">

                                        <div class="input-group input-group-alternative">

                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <asp:TextBox ID="txtAddressFontSize" class="form-control form-control-sm" data-toggle="tooltip" Style="border-color: #ff0000;" data-placement="bottom" title="font size" TextMode="Number" Height="25px" Width="70px" runat="server" Text="12"></asp:TextBox>

                                                    <div class="input-group-prepend " style="height: 25px">
                                                        <span class="input-group-text ">px</span>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                                <div class="col-3">
                                    <div class="form-group">

                                        <div class="input-group input-group-alternative">

                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <asp:TextBox ID="txtAddressLineHeight" class="form-control form-control-sm" data-toggle="tooltip" Style="border-color: #ff0000;" data-placement="bottom" title="line height" TextMode="Number" Height="25px" Width="70px" runat="server" Text="12"></asp:TextBox>

                                                    <div class="input-group-prepend " style="height: 25px">
                                                        <span class="input-group-text ">px</span>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                                <div class="col-3">
                                    <div class="form-group">

                                        <div class="input-group input-group-alternative">

                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <div class="input-group-prepend " style="height: 30px">
                                                        <span class="input-group-text ">border</span>
                                                    </div>
                                                    <asp:DropDownList ID="ddlBordertype" class="form-control form-control-sm" Height="30px" runat="server">

                                                        <asp:ListItem>dashed</asp:ListItem>
                                                        <asp:ListItem>dotted</asp:ListItem>
                                                        <asp:ListItem>double</asp:ListItem>
                                                        <asp:ListItem>groove</asp:ListItem>
                                                        <asp:ListItem>hidden</asp:ListItem>
                                                        <asp:ListItem>solid</asp:ListItem>
                                                        <asp:ListItem>none</asp:ListItem>

                                                    </asp:DropDownList>

                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="card-body border-left border-danger">
                            <h6 class="text-gray-900 small mb-3">Logo Visibility</h6>
                            <div class="row">
                                <div class="col-1">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="logoCheck" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger font-weight-bolder" for="logoCheck">Visible</label>
                                    </div>

                                </div>


                            </div>
                        </div>
                        <div class="card-body border-left border-info">
                            <h6 class="text-gray-900 small mb-3">Edit Footer Section</h6>
                            <div class="row">
                                <div class="col-1">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="fb" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger font-weight-bolder" for="fb">B</label>
                                    </div>

                                </div>
                                <div class="col-1">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="fi" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger font-fi" for="ia">I</label>
                                    </div>

                                </div>
                                <div class="col-1">
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="fu" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label small text-danger" for="fu" style="text-decoration: underline">U</label>
                                    </div>

                                </div>
                                <div class="col-3">
                                    <div class="form-group">

                                        <div class="input-group input-group-alternative">

                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <asp:TextBox ID="txtFooterFontSize" class="form-control form-control-sm" data-toggle="tooltip" Style="border-color: #ff0000;" data-placement="bottom" title="font size" TextMode="Number" Height="25px" Width="70px" runat="server" Text="12"></asp:TextBox>

                                                    <div class="input-group-prepend " style="height: 25px">
                                                        <span class="input-group-text ">px</span>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                                <div class="col-3">
                                    <div class="form-group">

                                        <div class="input-group input-group-alternative">

                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <asp:TextBox ID="txtFooterLineHeight" class="form-control form-control-sm" data-toggle="tooltip" Style="border-color: #ff0000;" data-placement="bottom" title="line height" TextMode="Number" Height="25px" Width="70px" runat="server" Text="12"></asp:TextBox>

                                                    <div class="input-group-prepend " style="height: 25px">
                                                        <span class="input-group-text ">px</span>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                    </div>
                    <center>
                        <div class="modal-footer">
                            <div id="myDIV5f" class="spinner-border text-danger spinner-border-sm  mr-4 mt-2" role="status">
                                <span class="sr-only">Loading.ffrfyyrg..</span>
                            </div>
                            <asp:Button ID="btnHeaderSave" runat="server" class="btn btn-sm btn-success " OnClick="btnHeaderSave_Click" OnClientClick="myFunctionshop1()" Text="Save Changes" />


                        </div>

                    </center>
                </div>
            </div>
        </div>


        <div class="modal fade" id="exampleModal4" tabindex="-1" role="dialog" aria-labelledby="MHeader" aria-hidden="true">
            <div class="modal-dialog modal-sm" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h6 class="modal-title text-gray-900 small font-weight-bold" id="exampleModalLabel4"><span class="fas fa-book-open mr-2" style="color: #9d469d"></span>Generate Notice Letter</h6>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <label class="text-gray-900 small text-uppercase">Letter Type</label>
                                <asp:DropDownList ID="ddlLettrType" class="form-control form-control-sm mb-2" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <label class="text-gray-900 small text-uppercase">Current Period</label>
                                <asp:DropDownList ID="DropDownList1" class="form-control form-control-sm mb-2" runat="server">
                                    <asp:ListItem>ነሃሴ እስከ ጥቅምት</asp:ListItem>
                                    <asp:ListItem>ህዳር እስከ ጥር</asp:ListItem>
                                    <asp:ListItem>የካቲት እስከ ሚያዚያ</asp:ListItem>
                                    <asp:ListItem>ግንቦት እስከ ሐምሌ</asp:ListItem>


                                    <asp:ListItem>ሚያዚያ እስከ ሰኔ</asp:ListItem>
                                    <asp:ListItem>ሐምሌ እስከ መስከረም</asp:ListItem>
                                    <asp:ListItem>ጥቅምት እስከ ታህሳስ</asp:ListItem>
                                    <asp:ListItem>ጥር እስከ መጋቢት</asp:ListItem>

                                </asp:DropDownList>
                                <hr />
                                <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                    <input type="checkbox" class="custom-control-input" id="Checkbox2" runat="server" clientidmode="Static" />
                                    <label class="custom-control-label text-xs text-gray-900" for="Checkbox2">Generate for all</label>
                                </div>
                            </div>
                        </div>

                    </div>
                    <center>
                        <div class="modal-footer">
                            <button class="btn btn-sm text-uppercase text-white" type="button" disabled id="myDIV5f1" style="display: none; background-color: #d46fe8">
                                <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                Generating
                            </button>
                            <div id="Gdiv">
                                <asp:Button ID="contreturn" runat="server" class="btn btn-sm btn-success w-100" OnClick="contreturn_Click" OnClientClick="myFunctionshop101()" Text="Generate..." />
                            </div>

                        </div>

                    </center>
                </div>
            </div>
        </div>

        <div class="modal fade" id="ModalReset" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel4" aria-hidden="true">
            <div class="modal-dialog modal-sm" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h6 class="modal-title text-gray-900" id="exampleModalLabel4"><span class="fas fa-reply mr-2 " style="color: #d46fe8"></span>Reset Content</h6>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row mb-1">
                            <div class="col-md-12">
                                <label class="text-dark small text-center">The text content will be returned back to its original content.</label>
                            </div>
                        </div>

                    </div>
                    <center>
                        <div class="modal-footer">

                            <asp:Button ID="btnReset" runat="server" class="btn btn-sm btn-danger w-100" OnClick="btnReset_Click" Text="Reset..." />


                        </div>

                    </center>
                </div>
            </div>
        </div>

        

        <div class="modal fade" id="CustomLetter" tabindex="-1" role="dialog" data-backdrop="static" aria-labelledby="exampleModalLongTitle3" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title h6 text-uppercase font-weight-bold text-gray-900" data-toggle="tooltip" data-placement="top" title="Update custom letter" id="exampleModalLongTitle3"><a data-toggle="modal" class="btn btn-sm btn-default" data-target="#ModalCustomLetterDropDown"><span class="fas fa-book-open mr-2 " style="color: #ff00bb"></span>Create Custom Letter</a></h5>
                        <span class="badge mt-2 text-uppercase badge-danger mx-2" id="CustomLetterName" style="display: none;"></span>
                        <span class="badge mt-2  badge-info mx-2" id="Loading" style="display: none;">Loading...</span>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-12">
                                <div class="form-group">
                                    <asp:TextBox ID="txtLetterName" ClientIDMode="Static" class="form-control form-control-sm" Style="border-color: #9867a1" placeholder="Letter Name" runat="server"></asp:TextBox>

                                </div>
                            </div>

                        </div>
                        <br />
                        <div class="row text-center">
                            <div class="col-12 text-center">
                                <div class="form-group text-center">
                                    <asp:TextBox ID="txtHeading" ClientIDMode="Static" class="form-control text-center form-control-sm" Style="border-color: #dc72d6" Text="ጉዳዩ፤- ትብብር ስለመጠየቅ" runat="server"></asp:TextBox>

                                </div>
                            </div>

                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-12">
                                <div class="form-group">
                                    <asp:TextBox ID="txtCustomePart1" ClientIDMode="Static" TextMode="MultiLine" Height="31px" data-toggle="tooltip" title="1" class="form-control form-control-sm" Style="border-color: #ffd800" Text="በድርጅታችን ራክሲም ንግድ ስራ ኃ/የተ/የግል ማህበር ህንፃ ላይ ለ ሱቅ ግልጋሎት የሚሆን ቦታ ተከራይተው እየሰሩ መሆኑ ይታወቃል ፡፡በመሆኑም ከ " runat="server"></asp:TextBox>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-3">
                                <div class="form-group">
                                    <asp:TextBox ID="TextBox2" ClientIDMode="Static" data-toggle="tooltip" title="2" class="form-control form-control-sm" Style="border-color: #ff00bb" ReadOnly="true" Text="previous period {year}" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtCustomePart3" ClientIDMode="Static" Height="31px" TextMode="MultiLine" data-toggle="tooltip" title="3" class="form-control form-control-sm" Style="border-color: #1fe109" Text=" የነበረው የክፍያ ጊዜ ስለተጠናቀቀ ከ " runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <asp:TextBox ID="TextBox4" ClientIDMode="Static" data-toggle="tooltip" title="4" class="form-control form-control-sm" Style="border-color: #ff00bb" ReadOnly="true" Text="current period {year}" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtCustomePart5" ClientIDMode="Static" TextMode="MultiLine" Height="31px" class="form-control form-control-sm" data-toggle="tooltip" title="5" Style="border-color: #ff0000" Text="ያለውን የቤት ኪራይ ገቢ ከጠቅላላ አገልግሎት ክፍያ ብር " runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                                <div class="form-group">
                                    <asp:TextBox ID="TextBox6" ClientIDMode="Static" class="form-control form-control-sm" data-toggle="tooltip" title="6" ReadOnly="true" Style="border-color: #0f7029" Text="money section {37,123.00}" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-10">
                                <div class="form-group">
                                    <asp:TextBox ID="txtCustomePart7" ClientIDMode="Static" TextMode="MultiLine" Height="31px" class="form-control form-control-sm" data-toggle="tooltip" title="7" ReadOnly="false" Style="border-color: #11f3df" Text="ጋር ውላችን ላይ ባለው አንቀፅ ሶስት መሰረት ቀጣዩን የ 3 ወር ክፍያ በቀጣዮቹ 15 ተከታታይ ቀናት ውስጥ እንድትከፍሉ እንጠይቃለን፡፡  " runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                                <div class="col-12">
                                    <div class="form-group">
                                        <div class="input-group input-group-alternative">

                                            <div class="form-group mb-0">
                                                <div class="input-group input-group-alternative input-group-sm">
                                                    <button class="btn btn-sm btn-light" height="10" type="button" id="AddButton2" ><span class="fas  mb-4 fa-align-justify text-danger"></span></button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                        </div>
                        <div class="row mt-5">
                            <div class="col-12">
                                <div class="form-group">
                                    <center>
                                        <button class="btn btn-sm text-uppercase text-white" type="button" disabled id="Pbutton1" style="display: none; background-color: #d46fe8">
                                            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                            Creating Custome Letter
                                        </button>
                                        <div id="Bd1">

                                            <asp:LinkButton ID="btnCreateCustomLetter" ClientIDMode="Static" OnClientClick="myFunctionshop1vb()" Style="background-color: #d46fe8" class="btn btn-sm text-uppercase text-white" runat="server" OnClick="btnCreateCustomLetter_Click"><span class="fas fa-book-open mr-2 text-white "></span>Create Custom Letter</asp:LinkButton>



                                        </div>
                                        <div id="updateDiv" style="display: none">
                                            <asp:LinkButton ID="btnUpdateCustomLetter" ClientIDMode="Static" Style="background-color: #d46fe8" class="btn btn-sm text-uppercase text-white" runat="server" OnClick="btnUpdateCustomLetter_Click"><span class="fas fa-upload mr-2 text-white "></span>Update Custom Letter</asp:LinkButton>
                                        </div>
                                    </center>



                                </div>
                            </div>
                        </div>




                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="ModalCustomLetterDropDown" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel4" aria-hidden="true">
            <div class="modal-dialog modal-sm" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h6 class="modal-title text-gray-900" id="exampleModalLabel4">Bind Custom Letter Content</h6>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <label class="text-gray-900 small text-center">Select Custom Letter</label>

                                <asp:DropDownList ID="ddlCustomLetterName" ClientIDMode="Static" class="form-control form-control-sm" runat="server"></asp:DropDownList>
                                <hr />

                            </div>
                        </div>

                    </div>
                    <center>
                        <div class="modal-footer">
                            <button type="button" onclick="find();" class="btn btn-sm text-white w-100" data-dismiss="modal" style="background-color: #dc72d6">Bind letter</button>



                        </div>

                    </center>
                </div>
            </div>
        </div>

        <div class="modal rounded fade" id="ModalReference" tabindex="-1" role="dialog" aria-labelledby="MHeader" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-sm" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h6 class="modal-title text-gray-900 small font-weight-bold" id="exampleModalLabel4"><span class="fas fa-filter mr-2" style="color: #ff00bb"></span>Filter Notice Letter</h6>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <label class="text-gray-900 small text-uppercase">Select Date</label>
                                <asp:DropDownList ID="ddlDateofLetterRecorded" class="form-control form-control-sm mb-2" style="border-color:#ff00bb" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <label class="text-gray-900 small text-uppercase">Select Reference Number</label>
                                <asp:DropDownList ID="ddlReferenceofLetterRecorded" Style="border-color: #ff00bb" class="form-control form-control-sm mb-2" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>
                    <center>
                        <div class="modal-footer">
                            <button class="btn btn-sm text-uppercase text-white" type="button" disabled id="myDIV5f11" style="display: none; background-color: #d46fe8">
                                <span class="spinner-grow spinner-grow-sm" role="status" aria-hidden="true"></span>
                               Binding
                            </button>
                            <div id="Gdiv1">
                                <asp:Button ID="btnBindReferencedLetterRecord" runat="server" class="btn text-white btn-sm w-100" Style="background-color: #d46fe8"
                                    OnClick="btnBindReferencedLetterRecord_Click" OnClientClick="myFunctionshop1011()" Text="Bind Letter..." />
                            </div>

                        </div>

                    </center>
                </div>
            </div>
        </div>
        <div class="modal fade" id="ModalCreateSpecialLetter" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel4" aria-hidden="true">
            <div class="modal-dialog modal-xl" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h6 class="modal-title text-gray-900" id="exampleModalLabel4"><span class="fas fa-file mr-2 " style="color: #d46fe8"></span>Create Special Letter</h6>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                            <div class="row mb-1">

                                <div class="col-md-12">
                                    <textarea id="editor2"></textarea>


                                </div>
                            </div>
                            <button onclick="getValues()"  type="button" id="letterSubmit" style="background-color: #d46fe8" class="btn btn-sm text-uppercase text-white"><span class="fas fa-save mr-2 text-white "></span>Create Special Letter</button>

                    </div>
                    <center>
                        <div class="modal-footer">
                        </div>

                    </center>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-12">
                <div class="bg-white rounded-lg mb-2 ">
                    <div class="card-header mb-3 bg-white ">
                        <div class="row">
                            <div class="col-6 text-left">

                                <a class="btn btn-light btn-circle mr-2" id="buttonback" runat="server" href="NoticeLetter.aspx" data-toggle="tooltip" data-placement="bottom" title="Back to Home">

                                    <span class="fa fa-arrow-left text-danger"></span>

                                </a>
                                <a class="btn-circle border-bottom border-top border-right border-danger border-left border- mr-2" id="A1" runat="server">

                                    <span id="counter" runat="server">0</span>

                                </a>
                                <span class="badge text-uppercase text-white" style="background-color:#9d469d">Customer</span>

                                <span class="badge badge-info mx-3" visible="false" id="periodSpan" runat="server"></span>
                                <span class="badge text-white text-uppercase" style="background-color:#48ac4d" visible="false" id="letterType" runat="server"></span>

                            </div>
                            <div class="col-6 text-right">



                                      <div class="dropdown no-arrow">
                                                                      <button type="button" runat="server" id="Sp2" class="mr-2 btn btn-sm btn-warning btn-circle" data-toggle="modal" data-target="#exampleModal4">
                                    <div>
                                        <i data-toggle="tooltip" title="Generate Letter" class="fas fa-calendar-check text-white font-weight-bold"></i>
                                        <span></span>
                                    </div>
                                </button>



                                <button name="b_print" onclick="printdiv('div_print');" type="button" title="Print" class=" btn btn-sm btn-danger btn-circle" data-toggle="modal" data-target="#exampleModalCenter">
                                    <div>
                                        <i class="fas fa-print text-white font-weight-bold"></i>

                                    </div>
                                </button>
                                          <button class="btn btn-light btn-circle mx-2 dropdown-toggle" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">

                                              <a class="nav-link btn btn-sm" data-toggle="tooltip" data-placement="bottom" title="Options">
                                                  <div>
                                                      <i class="fas fa-caret-down text-danger"></i>

                                                  </div>
                                              </a>

                                          </button>


                                          <div class="dropdown-menu  dropdown-menu-right shadow animated--fade-in" aria-labelledby="dropdownMenuLink">
                                              <div class="dropdown-header text-gray-900">Letter Option:</div>
                                               <a href="#" class="dropdown-item  text-gray-900  text-danger"  data-toggle="modal" data-target="#CustomLetter" id="A2" runat="server"><span class="fas fa-book-open mr-2 " style="color: #d46fe8"></span>Create Custom Letter</a>
                                               <a  href="#" class="dropdown-item  text-gray-900  text-danger"  data-toggle="modal" data-target="#ModalReference" id="LR" runat="server"><span class="fas fa-filter mr-2 " style="color: #d46fe8"></span>Filter Letter Record</a>
                                               <a  href="#" class="dropdown-item  text-gray-900  text-danger"  data-toggle="modal" data-target="#ModalBody" id="LR1" runat="server"><span class="fas fa-edit mr-2 " style="color: #d46fe8"></span>Customize Body</a>
                                               <a  href="#" class="dropdown-item  text-gray-900  text-danger"  data-toggle="modal" data-target="#ModalHeader" id="LR2" runat="server"><span class="fas fa-edit mr-2 " style="color: #d46fe8"></span>Customize Header and Footer</a>
                                               <a  href="#" class="dropdown-item  text-gray-900  text-danger"  data-toggle="modal" data-target="#ModalReset" id="LR3" runat="server"><span class="fas fa-reply mr-2 " style="color: #d46fe8"></span>Reset Content</a>
                                                                                              <hr/>
                                               <a href="#" class="dropdown-item  text-gray-900  text-danger" data-toggle="modal" data-target="#ModalCreateSpecialLetter" id="LR4" runat="server"><span class="fas fa-file mr-2 " style="color: #d46fe8"></span>Create Special Letter</a>
                                               <a  href="#" class="dropdown-item  text-gray-900  text-danger"  data-toggle="modal" data-target="#ModalReset" id="LR5" runat="server"><span class="fas fa-spinner mr-2 " style="color: #d46fe8"></span>Generate Special Letter</a>
                                          </div>
                                      </div>

                            </div>
                        </div>

                    </div>
                    <div id="con" runat="server">
                        <div id="div_print">
                            <div class="row " id="letterRecordedDiv" visible="false" runat="server">
                                <div class="col-1">
                                </div>
                                <div class="col-10">
                                    <asp:Repeater ID="rptLetterRecorded" runat="server">
                                        <ItemTemplate>
                                            <div>
                                                <div class="card-header text-black bg-white font-weight-bold">
                                                    <div class="row ">
                                                        <div class="col-4 text-left">
                                                            <img class="" src="../../asset/Brand/gh.jpg" alt="" width="110" <%#bind_logo_visibility() %>>
                                                        </div>
                                                        <div <%#bind_text_alignment() %>>

                                                            <a href="#" class="btn btn-default btn-sm" data-toggle="modal" data-target="#ModalHeader">
                                                                <span class="text-gray-900" id="headtext" <%#bind_heading_first()%>><%#bind_heading_first1() %></span>
                                                            </a>
                                                        </div>

                                                    </div>
                                                    <div class="text-gray-900" <%# bind_border_all()%>>
                                                        <div class="row  mb-3 text-gray-900" <%#bind_address() %>>
                                                            <div class="col-md-6 text-left">
                                                                <span translate="no">Ethiopia:-Addis Abeba</span>
                                                            </div>
                                                            <div class="col-md-6 text-right">
                                                                <span translate="no">Tell: - 0991-12121</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="margin-top: 20px;color:black">
                                                        <div class="row  ">
                                                            <div class="col-md-12 text-right" style="color:black">
                                                                <h6 class="mb-2 font-weight-bolder">Date/ቀን፡- <%# GetLetterRecordedData().Item1 %></h6>
                                                                <h6 class=" font-weight-bolder" contenteditable="true">Ref No./የደ.ቁ፤- ራክሲ፡- <%# GetLetterRecordedData().Item2 %></h6>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row  ">
                                                        <div class="col-md-12 text-left">
                                                            <h5 contenteditable="true" translate="yes" <%#bind_headline() %>>ለ<%# Eval("customer_amharic")%>/<%# Eval("buisnesstype")%></h5>
                                                            <h5  <%#bind_headline() %>>የሱቅ ቁጥር <%# Eval("shopno")%> </h5>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="card-body " <%#bind_borderbottom() %>>
                                                    <center>
                                                        <span <%#bind_headgudayu() %> contenteditable="true" ><%#GetLetterRecordedData().Item5 %></span>
                                                    </center>
                                                    <p class="text-gray-900" style="text-align: justify;" contenteditable="true">

                                                        <span <%#bind_bodyies() %>><%#bind_body1_text() %></span> <span <%#bind_period() %>><%#GetLetterRecordedData().Item3 %></span></span> <span <%#bind_bodyies() %>><%#bind_body2_text() %></span>
                                                        <span <%#bind_period() %>><%#GetLetterRecordedData().Item4 %></span>  <span <%#bind_bodyies() %>><%#bind_body3_text() %></span> <span <%#bind_money() %>><%# Eval("currentperiodue", "{0:N2}")%></span>
                                                        <span <%#bind_bodyies() %>><%#bind_body4_text() %></span>
                                                        <center>
                                                            <h1 class="water h1  font-weight-bolder" style="font-size: 60px">ራክሲም ንግድ ስራ ኃ/የ/የግል/ማ</h1>
                                                        </center>
                                                    </p>
                                                    <div class="row" contenteditable="true">
                                                        <div class="col-md-12 text-right">
                                                            <h5 class="mb-2 text-gray-900" <%#bind_footer() %>>ከሰላምታ ጋር</h5>
                                                            <h5 class="text-gray-900" <%#bind_footer() %>>አስተዳደር ቢሮ</h5>

                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>


                                </div>

                                <div class="col-1">
                                </div>
                            </div>
                            <div class="row " id="div1" runat="server">
                                <div class="col-1">
                                </div>
                                <div class="col-10">
                                    <asp:Repeater ID="Repeater1" runat="server">
                                        <ItemTemplate>
                                            <div>
                                                <div class="card-header text-black bg-white font-weight-bold">



                                                    <div class="row ">
                                                        <div class="col-4 text-left">
                                                            <img class="" src="../../asset/Brand/gh.jpg" alt="" width="110" <%#bind_logo_visibility() %>>
                                                        </div>
                                                        <div <%#bind_text_alignment() %>>

                                                            <a href="#" class="btn btn-default btn-sm" data-toggle="modal" data-target="#ModalHeader">
                                                                <span id="headtext" <%#bind_heading_first()%>><%#bind_heading_first1() %></span>
                                                            </a>
                                                        </div>

                                                    </div>
                                                    <div  <%# bind_border_all()%>>
                                                        <div class="row " <%#bind_address() %>>
                                                            <div class="col-md-6 text-left">
                                                                <span translate="no">Ethiopia:-Addis Abeba</span>
                                                            </div>
                                                            <div class="col-md-6 text-right">
                                                                <span translate="no">Tell: - 0991-12121</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="margin-top: 20px;color:black">
                                                        <div class="row  ">
                                                            <div class="col-md-12 text-right">
                                                                <h6 class="mb-2  font-weight-bolder">Date/ቀን፡- <%# getethiopianDate() %></h6>
                                                                <h6 class=" font-weight-bolder" contenteditable="true">Ref No./የደ.ቁ፤- ራክሲ፡- <%# GetActiveClass() %></h6>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row  ">
                                                        <div class="col-md-12 text-left" style="color:black">
                                                            <h5 class="mb-2" contenteditable="true" translate="yes" <%#bind_headline() %>>ለ<%# Eval("customer_amharic")%>/<%# Eval("buisnesstype")%></h5>
                                                            <h5  <%#bind_headline() %>>የሱቅ ቁጥር <%# Eval("shopno")%> </h5>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="card-body " <%#bind_borderbottom() %>>
                                                    <center>
                                                        <span <%#bind_headgudayu() %> contenteditable="true"><%#bind_headgudayu_text() %></span>
                                                    </center>
                                                    <p  style="text-align: justify;color:black" contenteditable="true">

                                                        <span <%#bind_bodyies() %>><%#bind_body1_text() %></span> <span <%#bind_period() %>>ግንቦት እስከ ሐምሌ</span> <span class="mx-2" <%#bind_year() %>><%# GetEthYear()%></span> <span <%#bind_bodyies() %>><%#bind_body2_text() %></span>
                                                        <span <%#bind_period() %>>ነሃሴ እስከ ጥቅምት</span> <span class="mx-2" <%#bind_year() %>><%# GetEthYear()+1%></span> <span <%#bind_bodyies() %>><%#bind_body3_text() %></span> <span <%#bind_money() %>><%# Eval("currentperiodue", "{0:N2}")%></span>
                                                        <span <%#bind_bodyies() %>><%#bind_body4_text() %></span>
                                                        <center>
                                                            <h1 class="water h1  font-weight-bolder" style="font-size: 60px">ራክሲም ንግድ ስራ ኃ/የ/የግል/ማ</h1>
                                                        </center>
                                                    </p>
                                                    <div class="row" contenteditable="true">
                                                        <div class="col-md-12 text-right">
                                                            <h5 class="mb-2 " <%#bind_footer() %>>ከሰላምታ ጋር</h5>
                                                            <h5 class="" <%#bind_footer() %>>አስተዳደር ቢሮ</h5>

                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>


                                </div>

                                <div class="col-1">
                                </div>
                            </div>
                            <div class="row" id="div2" runat="server">
                                <div class="col-1">
                                </div>
                                <div class="col-10">

                                    <asp:Repeater ID="Repeater2" runat="server">
                                        <ItemTemplate>
                                            <div>
                                                <div class="card-header text-black bg-white font-weight-bold">



                                                    <div class="row ">
                                                        <div class="col-md-4 text-left">
                                                            <img class="" src="../../asset/Brand/gh.jpg" alt="" width="110" <%#bind_logo_visibility() %>>
                                                        </div>
                                                        <div <%#bind_text_alignment() %>>


                                                            <span id="headtext" data-toggle="modal" data-target="#ModalHeader" <%#bind_heading_first()%>><%#bind_heading_first1() %></span>
                                                        </div>

                                                    </div>
                                                    <div class="text-gray-900" <%# bind_border_all()%>>
                                                        <div class="row  mb-3 text-gray-900" <%#bind_address() %>>
                                                            <div class="col-md-6 text-left">
                                                                <span translate="no">Ethiopia:-Addis Abeba</span>
                                                            </div>
                                                            <div class="col-md-6 text-right">
                                                                <span translate="no">Tell: - 0991-12121</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row  " style="margin-top: 20px;color:black">
                                                        <div class="col-md-12 text-right">
                                                            <h6 class="mb-2  font-weight-bolder">Date/ቀን፡- <%# getethiopianDate() %></h6>
                                                            <h6 class=" font-weight-bolder" contenteditable="true">Ref No./የደ.ቁ፤- ራክሲ፡- <%# GetActiveClass() %></h6>
                                                        </div>
                                                    </div>
                                                    <div class="row  ">
                                                        <div class="col-md-12 text-left" style="color: black">
                                                            <h5 class="mb-2 " <%#bind_headline() %> contenteditable="true" translate="yes">ለ<%# Eval("customer_amharic")%>/<%# Eval("buisnesstype")%></h5>
                                                            <h5 class=" " <%#bind_headline() %>>የሱቅ ቁጥር <%# Eval("shopno")%> </h5>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="card-body " <%#bind_borderbottom() %>>
                                                    <center>
                                                        <span <%#bind_headgudayu() %> contenteditable="true"><%#bind_headgudayu_text() %></span>
                                                    </center>
                                                    <p style="text-align: justify;color:black" contenteditable="true">

                                                        <span <%#bind_bodyies() %>><%#bind_body1_text() %></span> <span <%#bind_period() %>>ነሃሴ እስከ ጥቅምት</span> <span class="mx-2" <%#bind_year() %>><%# GetEthYear()%></span> <span <%#bind_bodyies() %>><%#bind_body2_text() %></span>
                                                        <span <%#bind_period() %>>ህዳር እስከ ጥር </span><span class="mx-2" <%#bind_year() %>><%# GetEthYear()%></span> <span <%#bind_bodyies() %>><%#bind_body3_text() %></span> <span <%#bind_money() %>><%# Eval("currentperiodue", "{0:N2}")%></span>
                                                        <span <%#bind_bodyies() %>><%#bind_body4_text() %></span>
                                                        <center>
                                                            <h1 class="water h1  font-weight-bolder" style="font-size: 60px">ራክሲም ንግድ ስራ ኃ/የ/የግል/ማ</h1>
                                                        </center>
                                                    </p>
                                                    <div class="row" contenteditable="true">
                                                        <div class="col-md-12 text-right">
                                                            <h5 class="mb-2 " <%#bind_footer() %>>ከሰላምታ ጋር</h5>
                                                            <h5 class="" <%#bind_footer() %>>አስተዳደር ቢሮ</h5>

                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                        </ItemTemplate>
                                    </asp:Repeater>


                                </div>

                                <div class="col-1">
                                </div>
                            </div>
                            <div class="row" id="div3" runat="server">
                                <div class="col-1">
                                </div>
                                <div class="col-10">
                                    <asp:Repeater ID="Repeater3" runat="server">
                                        <ItemTemplate>
                                            <div>
                                                <div class="card-header text-black bg-white font-weight-bold">



                                                    <div class="row ">
                                                        <div class="col-md-4 text-left">
                                                            <img class="" src="../../asset/Brand/gh.jpg" alt="" width="110" <%#bind_logo_visibility() %>>
                                                        </div>
                                                        <div <%#bind_text_alignment() %>>


                                                            <span  id="headtext" data-toggle="modal" data-target="#ModalHeader" <%#bind_heading_first()%>><%#bind_heading_first1() %></span>
                                                        </div>

                                                    </div>
                                                    <div class="text-gray-900" <%# bind_border_all()%>>
                                                        <div class="row mb-3  text-gray-900" <%#bind_address() %>>
                                                            <div class="col-md-6 text-left">
                                                                <span translate="no">Ethiopia:-Addis Abeba</span>
                                                            </div>
                                                            <div class="col-md-6 text-right">
                                                                <span translate="no">Tell: - 0991-12121</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row  " style="margin-top: 20px;color:black">
                                                        <div class="col-md-12 text-right">
                                                            <h6 class="mb-2 font-weight-bolder">Date/ቀን፡- <%# getethiopianDate() %></h6>
                                                            <h6 class=" font-weight-bolder" contenteditable="true">Ref No./የደ.ቁ፤- ራክሲ፡- <%# GetActiveClass() %></h6>
                                                        </div>
                                                    </div>
                                                    <div class="row  ">
                                                        <div class="col-md-12 text-left">
                                                            <h5 class="mb-2" <%#bind_headline() %> contenteditable="true" translate="yes">ለ<%# Eval("customer_amharic")%>/<%# Eval("buisnesstype")%></h5>
                                                            <h5 class="" <%#bind_headline() %>>የሱቅ ቁጥር <%# Eval("shopno")%> </h5>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="card-body " <%#bind_borderbottom() %>>
                                                    <center>
                                                        <span <%#bind_headgudayu() %> contenteditable="true" ><%#bind_headgudayu_text() %></span>
                                                    </center>
                                                    <p  style="text-align: justify;color:black" contenteditable="true">

                                                        <span <%#bind_bodyies() %>><%#bind_body1_text() %></span> <span <%#bind_period() %>>ህዳር እስከ ጥር</span> <span class="mx-2" <%#bind_year() %>><%# GetEthYear()%></span> <span <%#bind_bodyies() %>><%#bind_body2_text() %></span>
                                                        <span <%#bind_period() %>>የካቲት እስከ ሚያዚያ</span> <span class="mx-2" <%#bind_year() %>><%# GetEthYear()%></span> <span <%#bind_bodyies() %>><%#bind_body3_text() %></span> <span <%#bind_money() %>><%# Eval("currentperiodue", "{0:N2}")%></span>
                                                        <span <%#bind_bodyies() %>><%#bind_body4_text() %></span>
                                                        <center>
                                                            <h1 class="water h1  font-weight-bolder" style="font-size: 60px">ራክሲም ንግድ ስራ ኃ/የ/የግል/ማ</h1>
                                                        </center>
                                                    </p>
                                                    <div class="row" contenteditable="true">
                                                        <div class="col-md-12 text-right">
                                                            <h5 class="mb-2" <%#bind_footer() %>>ከሰላምታ ጋር</h5>
                                                            <h5 class="" <%#bind_footer() %>>አስተዳደር ቢሮ</h5>

                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                        </ItemTemplate>
                                    </asp:Repeater>

                                </div>

                                <div class="col-1">
                                </div>
                            </div>
                            <div class="row" id="div4" runat="server">
                                <div class="col-1">
                                </div>
                                <div class="col-10">
                                    <asp:Repeater ID="Repeater4" runat="server">

                                        <ItemTemplate>
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <div>
                                                            <div class="card-header text-black bg-white font-weight-bold">



                                                                <div class="row ">
                                                                    <div class="col-md-4 text-left">
                                                                        <img class="" src="../../asset/Brand/gh.jpg" alt="" width="110" <%#bind_logo_visibility() %>>
                                                                    </div>
                                                                    <div <%#bind_text_alignment() %>>


                                                                        <span class="text-gray-900" id="headtext" data-toggle="modal" data-target="#ModalHeader" <%#bind_heading_first()%>><%#bind_heading_first1() %></span>
                                                                    </div>

                                                                </div>
                                                                <div class="text-gray-900" <%# bind_border_all()%>>
                                                                    <div class="row  mb-3 text-gray-900" <%#bind_address() %>>
                                                                        <div class="col-md-6 text-left">
                                                                            <span translate="no">Ethiopia:-Addis Abeba</span>
                                                                        </div>
                                                                        <div class="col-md-6 text-right">
                                                                            <span translate="no">Tell: - 0991-12121</span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row  " style="margin-top: 20px;color:black">
                                                                    <div class="col-md-12 text-right">
                                                                        <h6 class="mb-2  font-weight-bolder">Date/ቀን፡- <%# getethiopianDate() %></h6>
                                                                        <h6 class=" font-weight-bolder" contenteditable="true">Ref No./የደ.ቁ፤- ራክሲ፡- <%# GetActiveClass() %></h6>
                                                                    </div>
                                                                </div>
                                                                <div class="row  ">
                                                                    <div class="col-md-12 text-left" style="color:black">
                                                                        <h5 class="mb-2 " <%#bind_headline() %> contenteditable="true" translate="yes">ለ<%# Eval("customer_amharic")%>/<%# Eval("buisnesstype")%></h5>
                                                                        <h5 class="" <%#bind_headline() %>>የሱቅ ቁጥር <%# Eval("shopno")%> </h5>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="card-body " <%#bind_borderbottom() %>>
                                                                <center>
                                                                    <span <%#bind_headgudayu() %> contenteditable="true"><%#bind_headgudayu_text() %></span>
                                                                </center>
                                                                <p  style="text-align: justify;color:black" contenteditable="true">

                                                                    <span <%#bind_bodyies() %>><%#bind_body1_text() %></span> <span <%#bind_period() %>>የካቲት እስከ ሚያዚያ </span><span class="mx-2" <%#bind_year() %>><%# GetEthYear()%></span> <span <%#bind_bodyies() %>><%#bind_body2_text() %></span>
                                                                    <span <%#bind_period() %>>ግንቦት እስከ ሐምሌ</span> <span class="mx-2" <%#bind_year() %>><%# GetEthYear()%></span> <span <%#bind_bodyies() %>><%#bind_body3_text() %></span> <span <%#bind_money() %>><%# Eval("currentperiodue", "{0:N2}")%></span>
                                                                    <span <%#bind_bodyies() %>><%#bind_body4_text() %></span>
                                                                    <center>
                                                                        <h1 class="water h1  font-weight-bolder" style="font-size: 60px">ራክሲም ንግድ ስራ ኃ/የ/የግል/ማ</h1>
                                                                    </center>
                                                                </p>
                                                                <div class="row" contenteditable="true">
                                                                    <div class="col-md-12 text-right">
                                                                        <h5 class="mb-2 " <%#bind_footer() %>>ከሰላምታ ጋር</h5>
                                                                        <h5 class="" <%#bind_footer() %>>አስተዳደር ቢሮ</h5>

                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>

                                    </asp:Repeater>

                                </div>

                                <div class="col-1">
                                </div>
                            </div>
                            <div class="row" id="div5" runat="server">
                                <div class="col-1">
                                </div>
                                <div class="col-10">
                                    <asp:Repeater ID="Repeater5" runat="server">

                                        <ItemTemplate>
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <div>
                                                            <div class="card-header text-black bg-white font-weight-bold">



                                                                <div class="row ">
                                                                    <div class="col-md-4 text-left">
                                                                        <img class="" src="../../asset/Brand/gh.jpg" alt="" width="110" <%#bind_logo_visibility() %>>
                                                                    </div>
                                                                    <div <%#bind_text_alignment() %>>


                                                                        <span  id="headtext" data-toggle="modal" data-target="#ModalHeader" <%#bind_heading_first()%>><%#bind_heading_first1() %></span>
                                                                    </div>

                                                                </div>
                                                                <div class="text-gray-900" <%# bind_border_all()%>>
                                                                    <div class="row  mb-3 text-gray-900" <%#bind_address() %>>
                                                                        <div class="col-md-6 text-left">
                                                                            <span translate="no">Ethiopia:-Addis Abeba</span>
                                                                        </div>
                                                                        <div class="col-md-6 text-right">
                                                                            <span translate="no">Tell: - 0991-12121</span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row  " style="margin-top: 20px;color:black">
                                                                    <div class="col-md-12 text-right">
                                                                        <h6 class="mb-2  font-weight-bolder">Date/ቀን፡- <%# getethiopianDate() %></h6>
                                                                        <h6 class=" font-weight-bolder" contenteditable="true">Ref No./የደ.ቁ፤- ራክሲ፡- <%# GetActiveClass() %></h6>
                                                                    </div>
                                                                </div>
                                                                <div class="row  ">
                                                                    <div class="col-md-12 text-left" style="color:black">
                                                                        <h5 class="mb-2 " <%#bind_headline() %> contenteditable="true" translate="yes">ለ<%# Eval("customer_amharic")%>/<%# Eval("buisnesstype")%></h5>
                                                                        <h5 class="" <%#bind_headline() %>>የሱቅ ቁጥር <%# Eval("shopno")%> </h5>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="card-body " <%#bind_borderbottom() %>>
                                                                <center>
                                                                    <span <%#bind_headgudayu() %> contenteditable="true"><%#bind_heading_Custom() %></span>
                                                                </center>
                                                                <p style="text-align: justify;color:black" contenteditable="true">

                                                                    <span <%#bind_bodyies() %>><%#bind_part1_Custom() %></span> <span <%#bind_period() %>>ግንቦት እስከ ሐምሌ</span> <span class="mx-2" <%#bind_year() %>><%# GetEthYear()%></span> <span <%#bind_bodyies() %>><%#bind_part2_Custom() %></span>
                                                                    <span <%#bind_period() %>>ነሃሴ እስከ ጥቅምት</span> <span class="mx-2" <%#bind_year() %>><%# GetEthYear()+1%></span> <span <%#bind_bodyies() %>><%#bind_part3_Custom() %></span> <span <%#bind_money() %>><%# Eval("currentperiodue", "{0:N2}")%></span>
                                                                    <span <%#bind_bodyies() %>><%#bind_part4_Custom() %></span>
                                                                    <center>
                                                                        <h1 class="water h1  font-weight-bolder" style="font-size: 60px">ራክሲም ንግድ ስራ ኃ/የ/የግል/ማ</h1>
                                                                    </center>
                                                                </p>
                                                                <div class="row" contenteditable="true">
                                                                    <div class="col-md-12 text-right">
                                                                        <h5 class="mb-2 " <%#bind_footer() %>>ከሰላምታ ጋር</h5>
                                                                        <h5 class="" <%#bind_footer() %>>አስተዳደር ቢሮ</h5>

                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>

                                    </asp:Repeater>

                                </div>

                                <div class="col-1">
                                </div>
                            </div>
                            <div class="row" id="div6" runat="server">
                                <div class="col-1">
                                </div>
                                <div class="col-10">
                                    <asp:Repeater ID="Repeater6" runat="server">

                                        <ItemTemplate>
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <div>
                                                            <div class="card-header text-black bg-white font-weight-bold">



                                                                <div class="row ">
                                                                    <div class="col-md-4 text-left">
                                                                        <img class="" src="../../asset/Brand/gh.jpg" alt="" width="110" <%#bind_logo_visibility() %>>
                                                                    </div>
                                                                    <div <%#bind_text_alignment() %>>


                                                                        <span id="headtext" data-toggle="modal" data-target="#ModalHeader" <%#bind_heading_first()%>><%#bind_heading_first1() %></span>
                                                                    </div>

                                                                </div>
                                                                <div class="text-gray-900" <%# bind_border_all()%>>
                                                                    <div class="row  mb-3 text-gray-900" <%#bind_address() %>>
                                                                        <div class="col-md-6 text-left">
                                                                            <span translate="no">Ethiopia:-Addis Abeba</span>
                                                                        </div>
                                                                        <div class="col-md-6 text-right">
                                                                            <span translate="no">Tell: - 0991-12121</span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row  " style="margin-top: 20px;color:black">
                                                                    <div class="col-md-12 text-right">
                                                                        <h6 class="mb-2 font-weight-bolder">Date/ቀን፡- <%# getethiopianDate() %></h6>
                                                                        <h6 class=" font-weight-bolder" contenteditable="true">Ref No./የደ.ቁ፤- ራክሲ፡- <%# GetActiveClass() %></h6>
                                                                    </div>
                                                                </div>
                                                                <div class="row  ">
                                                                    <div class="col-md-12 text-left" style="color:black">
                                                                        <h5 class="mb-2 " <%#bind_headline() %> contenteditable="true" translate="yes">ለ<%# Eval("customer_amharic")%>/<%# Eval("buisnesstype")%></h5>
                                                                        <h5 class="" <%#bind_headline() %>>የሱቅ ቁጥር <%# Eval("shopno")%> </h5>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="card-body " <%#bind_borderbottom() %>>
                                                                <center>
                                                                    <span <%#bind_headgudayu() %> contenteditable="true"><%#bind_heading_Custom() %></span>
                                                                </center>
                                                                <p  style="text-align: justify;color:black" contenteditable="true">

                                                                    <span <%#bind_bodyies() %>><%#bind_part1_Custom() %></span> <span <%#bind_period() %>>ግንቦት እስከ ሐምሌ</span> <span class="mx-2" <%#bind_year() %>><%# GetEthYear()%></span> <span <%#bind_bodyies() %>><%#bind_part2_Custom() %></span>
                                                                    <span <%#bind_period() %>>ነሃሴ እስከ ጥቅምት</span> <span class="mx-2" <%#bind_year() %>><%# GetEthYear()+1%></span> <span <%#bind_bodyies() %>><%#bind_part3_Custom() %></span> <span <%#bind_money() %>><%# Eval("currentperiodue", "{0:N2}")%></span>
                                                                    <span <%#bind_bodyies() %>><%#bind_part4_Custom() %></span>
                                                                    <center>
                                                                        <h1 class="water h1  font-weight-bolder" style="font-size: 60px">ራክሲም ንግድ ስራ ኃ/የ/የግል/ማ</h1>
                                                                    </center>
                                                                </p>
                                                                <div class="row" contenteditable="true">
                                                                    <div class="col-md-12 text-right">
                                                                        <h5 class="mb-2 " <%#bind_footer() %>>ከሰላምታ ጋር</h5>
                                                                        <h5 class="" <%#bind_footer() %>>አስተዳደር ቢሮ</h5>

                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>

                                    </asp:Repeater>

                                </div>

                                <div class="col-1">
                                </div>
                            </div>
                            <div class="row" id="div7" runat="server">
                                <div class="col-1">
                                </div>
                                <div class="col-10">
                                    <asp:Repeater ID="Repeater7" runat="server">

                                        <ItemTemplate>
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <div>
                                                            <div class="card-header text-black bg-white font-weight-bold">



                                                                <div class="row ">
                                                                    <div class="col-md-4 text-left">
                                                                        <img class="" src="../../asset/Brand/gh.jpg" alt="" width="110" <%#bind_logo_visibility() %>>
                                                                    </div>
                                                                    <div <%#bind_text_alignment() %>>


                                                                        <span  id="headtext" data-toggle="modal" data-target="#ModalHeader" <%#bind_heading_first()%>><%#bind_heading_first1() %></span>
                                                                    </div>

                                                                </div>
                                                                <div class="text-gray-900" <%# bind_border_all()%>>
                                                                    <div class="row  mb-3 text-gray-900" <%#bind_address() %>>
                                                                        <div class="col-md-6 text-left">
                                                                            <span translate="no">Ethiopia:-Addis Abeba</span>
                                                                        </div>
                                                                        <div class="col-md-6 text-right">
                                                                            <span translate="no">Tell: - 0991-12121</span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row  " style="margin-top: 20px;color:black">
                                                                    <div class="col-md-12 text-right">
                                                                        <h6 class="mb-2 font-weight-bolder">Date/ቀን፡- <%# getethiopianDate() %></h6>
                                                                        <h6 class="font-weight-bolder" contenteditable="true">Ref No./የደ.ቁ፤- ራክሲ፡- <%# GetActiveClass() %></h6>
                                                                    </div>
                                                                </div>
                                                                <div class="row  ">
                                                                    <div class="col-md-12 text-left" style="color:black">
                                                                        <h5 class="mb-2 " <%#bind_headline() %> contenteditable="true" translate="yes">ለ<%# Eval("customer_amharic")%>/<%# Eval("buisnesstype")%></h5>
                                                                        <h5 class="" <%#bind_headline() %>>የሱቅ ቁጥር <%# Eval("shopno")%> </h5>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="card-body " <%#bind_borderbottom() %>>
                                                                <center>
                                                                    <span <%#bind_headgudayu() %> contenteditable="true"><%#bind_heading_Custom() %></span>
                                                                </center>
                                                                <p  style="text-align: justify;color:black" contenteditable="true">

                                                                    <span <%#bind_bodyies() %>><%#bind_part1_Custom() %></span> <span <%#bind_period() %>>ግንቦት እስከ ሐምሌ</span> <span class="mx-2" <%#bind_year() %>><%# GetEthYear()%></span> <span <%#bind_bodyies() %>><%#bind_part2_Custom() %></span>
                                                                    <span <%#bind_period() %>>ነሃሴ እስከ ጥቅምት</span> <span class="mx-2" <%#bind_year() %>><%# GetEthYear()+1%></span> <span <%#bind_bodyies() %>><%#bind_part3_Custom() %></span> <span <%#bind_money() %>><%# Eval("currentperiodue", "{0:N2}")%></span>
                                                                    <span <%#bind_bodyies() %>><%#bind_part4_Custom() %></span>
                                                                    <center>
                                                                        <h1 class="water h1  font-weight-bolder" style="font-size: 60px">ራክሲም ንግድ ስራ ኃ/የ/የግል/ማ</h1>
                                                                    </center>
                                                                </p>
                                                                <div class="row" contenteditable="true">
                                                                    <div class="col-md-12 text-right">
                                                                        <h5 class="mb-2" <%#bind_footer() %>>ከሰላምታ ጋር</h5>
                                                                        <h5 class="" <%#bind_footer() %>>አስተዳደር ቢሮ</h5>

                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>

                                    </asp:Repeater>

                                </div>

                                <div class="col-1">
                                </div>
                            </div>
                            <div class="row" id="div8" runat="server">
                                <div class="col-1">
                                </div>
                                <div class="col-10">
                                    <asp:Repeater ID="Repeater8" runat="server">

                                        <ItemTemplate>
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <div>
                                                            <div class="card-header text-black bg-white font-weight-bold">



                                                                <div class="row ">
                                                                    <div class="col-md-4 text-left">
                                                                        <img class="" src="../../asset/Brand/gh.jpg" alt="" width="110" <%#bind_logo_visibility() %>>
                                                                    </div>
                                                                    <div <%#bind_text_alignment() %>>


                                                                        <span class="text-gray-900" id="headtext" data-toggle="modal" data-target="#ModalHeader" <%#bind_heading_first()%>><%#bind_heading_first1() %></span>
                                                                    </div>

                                                                </div>
                                                                <div class="text-gray-900" <%# bind_border_all()%>>
                                                                    <div class="row  mb-3 text-gray-900" <%#bind_address() %>>
                                                                        <div class="col-md-6 text-left">
                                                                            <span translate="no">Ethiopia:-Addis Abeba</span>
                                                                        </div>
                                                                        <div class="col-md-6 text-right">
                                                                            <span translate="no">Tell: - 0991-12121</span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row  " style="margin-top: 20px;color:black">
                                                                    <div class="col-md-12 text-right">
                                                                        <h6 class="mb-2  font-weight-bolder">Date/ቀን፡- <%# getethiopianDate() %></h6>
                                                                        <h6 class=" font-weight-bolder" contenteditable="true">Ref No./የደ.ቁ፤- ራክሲ፡- <%# GetActiveClass() %></h6>
                                                                    </div>
                                                                </div>
                                                                <div class="row  ">
                                                                    <div class="col-md-12 text-left" style="color:black">
                                                                        <h5 class="mb-2 " <%#bind_headline() %> contenteditable="true" translate="yes">ለ<%# Eval("customer_amharic")%>/<%# Eval("buisnesstype")%></h5>
                                                                        <h5 class="" <%#bind_headline() %>>የሱቅ ቁጥር <%# Eval("shopno")%> </h5>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="card-body " <%#bind_borderbottom() %>>
                                                                <center>
                                                                    <span <%#bind_headgudayu() %> contenteditable="true" ><%#bind_heading_Custom() %></span>
                                                                </center>
                                                                <p  style="text-align: justify;color:black" contenteditable="true">

                                                                    <span <%#bind_bodyies() %>><%#bind_part1_Custom() %></span> <span <%#bind_period() %>>ግንቦት እስከ ሐምሌ</span> <span class="mx-2" <%#bind_year() %>><%# GetEthYear()%></span> <span <%#bind_bodyies() %>><%#bind_part2_Custom() %></span>
                                                                    <span <%#bind_period() %>>ነሃሴ እስከ ጥቅምት</span> <span class="mx-2" <%#bind_year() %>><%# GetEthYear()+1%></span> <span <%#bind_bodyies() %>><%#bind_part3_Custom() %></span> <span <%#bind_money() %>><%# Eval("currentperiodue", "{0:N2}")%></span>
                                                                    <span <%#bind_bodyies() %>><%#bind_part4_Custom() %></span>
                                                                    <center>
                                                                        <h1 class="water h1  font-weight-bolder" style="font-size: 60px">ራክሲም ንግድ ስራ ኃ/የ/የግል/ማ</h1>
                                                                    </center>
                                                                </p>
                                                                <div class="row" contenteditable="true">
                                                                    <div class="col-md-12 text-right">
                                                                        <h5 class="mb-2" <%#bind_footer() %>>ከሰላምታ ጋር</h5>
                                                                        <h5 class="" <%#bind_footer() %>>አስተዳደር ቢሮ</h5>

                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ItemTemplate>

                                    </asp:Repeater>

                                </div>

                                <div class="col-1">
                                </div>
                            </div>


                            <div class="row " id="secondPeriodDiv" runat="server" visible="false">
                                <div class="col-1">
                                </div>
                                <div class="col-10">
                                    <asp:Repeater ID="rptrSecondPeriod" runat="server">
                                        <ItemTemplate>
                                            <div>
                                                <div class="card-header text-black bg-white font-weight-bold">
                                                    <div class="row ">
                                                        <div class="col-4 text-left">
                                                            <img class="" src="../../asset/Brand/gh.jpg" alt="" width="110" <%#bind_logo_visibility() %>>
                                                        </div>
                                                        <div <%#bind_text_alignment() %>>

                                                            <a href="#" class="btn btn-default btn-sm" data-toggle="modal" data-target="#ModalHeader">
                                                                <span id="headtext" <%#bind_heading_first()%>><%#bind_heading_first1() %></span>
                                                            </a>
                                                        </div>

                                                    </div>
                                                    <div class="text-gray-900" <%# bind_border_all()%>>
                                                        <div class="row  mb-3 text-gray-900" <%#bind_address() %>>
                                                            <div class="col-md-6 text-left">
                                                                <span translate="no">Ethiopia:-Addis Abeba</span>
                                                            </div>
                                                            <div class="col-md-6 text-right">
                                                                <span translate="no">Tell: - 0991-12121</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="margin-top: 20px;color:black">
                                                        <div class="row  ">
                                                            <div class="col-md-12 text-right">
                                                                <h6 class="mb-2  font-weight-bolder">Date/ቀን፡- <%# getethiopianDate() %></h6>
                                                                <h6 class="font-weight-bolder" contenteditable="true">Ref No./የደ.ቁ፤- ራክሲ፡- <%# GetActiveClass() %></h6>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row  ">
                                                        <div class="col-md-12 text-left" style="color:black">
                                                            <h5 class="mb-2 " contenteditable="true" translate="yes" <%#bind_headline() %>>ለ<%# Eval("customer_amharic")%>/<%# Eval("buisnesstype")%></h5>
                                                            <h5 class="" <%#bind_headline() %>>የሱቅ ቁጥር <%# Eval("shopno")%> </h5>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="card-body " <%#bind_borderbottom() %>>
                                                    <center>
                                                        <span <%#bind_headgudayu() %> contenteditable="true"><%#bind_headgudayu_text() %></span>
                                                    </center>
                                                    <p  style="text-align: justify;color:black" contenteditable="true">

                                                        <span <%#bind_bodyies() %>><%#bind_body1_text() %></span> <span <%#bind_period() %>><%#GetPeriods().Item2 %></span> <span class="mx-2" <%#bind_year() %>><%# GetEthYear()%></span> <span <%#bind_bodyies() %>><%#bind_body2_text() %></span>
                                                        <span <%#bind_period() %>><%#GetPeriods().Item1 %></span> <span class="mx-2" <%#bind_year() %>><%# GetEthYear()%></span> <span <%#bind_bodyies() %>><%#bind_body3_text() %></span> <span <%#bind_money() %>><%# Eval("currentperiodue", "{0:N2}")%></span>
                                                        <span <%#bind_bodyies() %>><%#bind_body4_text() %></span>
                                                        <center>
                                                            <h1 class="water h1  font-weight-bolder" style="font-size: 60px">ራክሲም ንግድ ስራ ኃ/የ/የግል/ማ</h1>
                                                        </center>
                                                    </p>
                                                    <div class="row" contenteditable="true">
                                                        <div class="col-md-12 text-right">
                                                            <h5 class="mb-2" <%#bind_footer() %>>ከሰላምታ ጋር</h5>
                                                            <h5 class="" <%#bind_footer() %>>አስተዳደር ቢሮ</h5>

                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>


                                </div>

                                <div class="col-1">
                                </div>
                            </div>
                            <div class="row " id="secondPeriodCustom" runat="server" visible="false">
                                <div class="col-1">
                                </div>
                                <div class="col-10">
                                    <asp:Repeater ID="rptrSecondCustom" runat="server">
                                        <ItemTemplate>
                                            <div>
                                                <div class="card-header text-black bg-white font-weight-bold">



                                                    <div class="row ">
                                                        <div class="col-md-4 text-left">
                                                            <img class="" src="../../asset/Brand/gh.jpg" alt="" width="110" <%#bind_logo_visibility() %>>
                                                        </div>
                                                        <div <%#bind_text_alignment() %>>


                                                            <span  id="headtext" data-toggle="modal" data-target="#ModalHeader" <%#bind_heading_first()%>><%#bind_heading_first1() %></span>
                                                        </div>

                                                    </div>
                                                    <div class="text-gray-900" <%# bind_border_all()%>>
                                                        <div class="row  mb-3 text-gray-900" <%#bind_address() %>>
                                                            <div class="col-md-6 text-left">
                                                                <span translate="no">Ethiopia:-Addis Abeba</span>
                                                            </div>
                                                            <div class="col-md-6 text-right">
                                                                <span translate="no">Tell: - 0991-12121</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row  " style="margin-top: 20px;color:black">
                                                        <div class="col-md-12 text-right">
                                                            <h6 class="mb-2 font-weight-bolder">Date/ቀን፡- <%# getethiopianDate() %></h6>
                                                            <h6 class=" font-weight-bolder" contenteditable="true">Ref No./የደ.ቁ፤- ራክሲ፡- <%# GetActiveClass() %></h6>
                                                        </div>
                                                    </div>
                                                    <div class="row  ">
                                                        <div class="col-md-12 text-left" style="color:black">
                                                            <h5 class="mb-2 " <%#bind_headline() %> contenteditable="true" translate="yes">ለ<%# Eval("customer_amharic")%>/<%# Eval("buisnesstype")%></h5>
                                                            <h5 class="" <%#bind_headline() %>>የሱቅ ቁጥር <%# Eval("shopno")%> </h5>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="card-body " <%#bind_borderbottom() %>>
                                                    <center>
                                                        <span <%#bind_headgudayu() %> contenteditable="true" ><%#bind_heading_Custom() %></span>
                                                    </center>
                                                    <p style="text-align: justify; color: black" contenteditable="true">

                                                        <span <%#bind_bodyies() %>><%#bind_part1_Custom() %></span> <span <%#bind_period() %>><%#GetPeriods().Item2 %></span></span> <span class="mx-2" <%#bind_year() %>><%# GetEthYear()%></span> <span <%#bind_bodyies() %>><%#bind_part2_Custom() %></span>
                                                        <span <%#bind_period() %>><%#GetPeriods().Item1 %></span></span> <span class="mx-2" <%#bind_year() %>><%# GetEthYear()%></span> <span <%#bind_bodyies() %>><%#bind_part3_Custom() %></span> <span <%#bind_money() %>><%# Eval("currentperiodue", "{0:N2}")%></span>
                                                        <span <%#bind_bodyies() %>><%#bind_part4_Custom() %></span>
                                                        <center>
                                                            <h1 class="water h1  font-weight-bolder" style="font-size: 60px">ራክሲም ንግድ ስራ ኃ/የ/የግል/ማ</h1>
                                                        </center>
                                                    </p>
                                                    <div class="row" contenteditable="true">
                                                        <div class="col-md-12 text-right">
                                                            <h5 class="mb-2 " <%#bind_footer() %>>ከሰላምታ ጋር</h5>
                                                            <h5 class="" <%#bind_footer() %>>አስተዳደር ቢሮ</h5>

                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>


                                </div>

                                <div class="col-1">
                                </div>
                            </div>

                        </div>
                    </div>
                    <div id="div9" runat="server" style="height: 500px">
                        <div class="mt-lg-5">
                            <center>
                                <span class="fas fa-book-open mb-5  fa-9x" style="color: #d46fe8" id ="Icon" runat="server"></span>
                                <h6 class="text-gray-300 mt-4 font-weight-bold small" id="InfoText" runat="server">No Notice Letter Generated</h6>
                            </center>
                        </div>
                    </div>

                </div>

            </div>
        </div>
    </div>
    <script type="text/javascript">
        $("[id*=AddButton]").bind('click', function () {
            var cursorPos1 = $('#txtpart7').prop('selectionStart');
            if (cursorPos1 != 0) {
                var v1 = $('#txtpart7').val();
                var textBefore1 = v1.substring(0, cursorPos1);
                var textAfter1 = v1.substring(cursorPos1, v1.length);


                $('#txtpart7').val(textBefore1 + '*NewLine*' + textAfter1);
            }
            var cursorPos2 = $('#txtpart6').prop('selectionStart');
            if (cursorPos2 != 0) {

                var v2 = $('#txtpart6').val();
                var textBefore2 = v2.substring(0, cursorPos2);
                var textAfter2 = v2.substring(cursorPos2, v2.length);

                $('#txtpart6').val(textBefore2 + '*NewLine*' + textAfter2);

            }
            var cursorPos3 = $('#txtpart4').prop('selectionStart');
            if (cursorPos3 != 0) {

                var v3 = $('#txtpart4').val();
                var textBefore3 = v3.substring(0, cursorPos3);
                var textAfter3 = v3.substring(cursorPos3, v3.length);

                $('#txtpart4').val(textBefore3 + '*NewLine*' + textAfter3);
            }
            var cursorPos4 = $('#txtpart2').prop('selectionStart');
            if (cursorPos4 != 0) {

                var v4 = $('#txtpart2').val();
                var textBefore4 = v4.substring(0, cursorPos4);
                var textAfter4 = v4.substring(cursorPos4, v4.length);

                $('#txtpart2').val(textBefore4 + '*NewLine*' + textAfter4);
            }
        });
    </script>
    <script type="text/javascript">
        $("[id*=AddButton2]").bind('click', function () {
            var cursorPos1 = $('#txtCustomePart1').prop('selectionStart');
            if (cursorPos1 != 0) {
                var v1 = $('#txtCustomePart1').val();
                var textBefore1 = v1.substring(0, cursorPos1);
                var textAfter1 = v1.substring(cursorPos1, v1.length);


                $('#txtCustomePart1').val(textBefore1 + '*NewLine*' + textAfter1);
            }
            var cursorPos2 = $('#txtCustomePart3').prop('selectionStart');
            if (cursorPos2 != 0) {

                var v2 = $('#txtCustomePart3').val();
                var textBefore2 = v2.substring(0, cursorPos2);
                var textAfter2 = v2.substring(cursorPos2, v2.length);

                $('#txtCustomePart3').val(textBefore2 + '*NewLine*' + textAfter2);

            }
            var cursorPos3 = $('#txtCustomePart5').prop('selectionStart');
            if (cursorPos3 != 0) {

                var v3 = $('#txtCustomePart5').val();
                var textBefore3 = v3.substring(0, cursorPos3);
                var textAfter3 = v3.substring(cursorPos3, v3.length);

                $('#txtCustomePart5').val(textBefore3 + '*NewLine*' + textAfter3);
            }
            var cursorPos4 = $('#txtCustomePart7').prop('selectionStart');
            if (cursorPos4 != 0) {

                var v4 = $('#txtCustomePart7').val();
                var textBefore4 = v4.substring(0, cursorPos4);
                var textAfter4 = v4.substring(cursorPos4, v4.length);

                $('#txtCustomePart7').val(textBefore4 + '*NewLine*' + textAfter4);
            }
        });
    </script>
    <script>
        function myFunctionshop1vb() {
            var x = document.getElementById("Bd1"); var y = document.getElementById("Pbutton1");
            if (x.style.display == "none") {
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
            var x = document.getElementById("myDIV5");
            if (x.style.display === "none") {
                x.style.display = "block";
            } else {
                x.style.display = "none";
            }
        }
    </script>
    <script>
        function myFunctionshop1() {
            var x = document.getElementById("myDIV5f");
            if (x.style.display === "none") {
                x.style.display = "block";
            } else {
                x.style.display = "none";
            }
        }
    </script>
    <script>
        function myFunctionshop101() {
            var x = document.getElementById("myDIV5f1"); var y = document.getElementById("Gdiv");
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
        function myFunctionshop1011() {
            var x = document.getElementById("myDIV5f11"); var y = document.getElementById("Gdiv1");
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

</asp:Content>
