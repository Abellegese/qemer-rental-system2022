<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="advtech.Finance.Accounta.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../../asset/js/jquery-3.2.1.min.js"></script>
    <link href="../../asset/js/jquery-ui.css" rel="stylesheet" />
    <script src="../../asset/js/jquery-ui.min.js"></script>
    <link href="../../asset/css/sb-admin-2.css" rel="stylesheet" />
    <script type="text/javascript">  
        $(document).ready(function () {
            SearchText();
        });
        function SearchText() {
            $("#txtEmpName").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "WebForm1.aspx/GetEmployeeName",
                        data: "{'empName':'" + document.getElementById('txtEmpName').value + "'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (result) {
                            alert("No Match");
                        }
                    });
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table cellpadding="10" cellspacing="10" style="border: solid 15px Green; background-color: SkyBlue;"
            width="50%" align="center">
            <tr>
                <td>
                    <span style="color: Red; font-weight: bold; font-size: 18pt;">Enter Employee Name:</span>
                    <asp:TextBox ID="txtEmpName" runat="server" Width="160px" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
