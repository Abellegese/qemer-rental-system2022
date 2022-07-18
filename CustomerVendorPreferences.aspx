<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.master" AutoEventWireup="true" CodeBehind="CustomerVendorPreferences.aspx.cs" Inherits="advtech.Finance.Accounta.CustomerVendorPreferences" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Preferences</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="container-fluid pl-3 pr-3 small text-gray-900">
        <div class="row">
            <div class="col">
                <div id="div_print">

                    <div class="bg-white rounded-lg mb-4">
                        <div class="card-header bg-white">
                            <div class="row">
                                <div class="col-xl-6 text-left">
                                    <h5 class="m-0 font-weight-bold text-uppercase text-gray-900">Preferences</h5>
                                </div>
                                <div class="col-xl-6 text-right">
                                    <asp:LinkButton ID="LinkButton1" class="btn btn-sm btn-secondary btn-danger" runat="server"
                                        OnClick="LinkButton1_Click">Save Setting</asp:LinkButton>
                                </div>
                            </div>


                        </div>
                        <div class="card-body">



                            <div class="row">
                                <div class="col-md-6">

                                    <div class="form-group has-error">
                                        <label class="font-weight-bold">Select Payroll Expense Account<span class="text-danger">*</span></label>
                                        <asp:DropDownList ID="ddlSalaryExpense" runat="server"
                                            class="form-control form-control-sm ">
                                        </asp:DropDownList>
                                    </div>


                                    <div class="form-group has-error">
                                        <label class="font-weight-bold">Select Salaries Payable Account Account<span class="text-danger">*</span></label>
                                        <asp:DropDownList ID="ddlSalaryPayable" runat="server"
                                            class="form-control form-control-sm ">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group has-error">
                                        <label class="font-weight-bold">Select Income Tax Payable Account<span class="text-danger">*</span></label>
                                        <asp:DropDownList ID="ddlIncomeTaxPayable" runat="server"
                                            class="form-control form-control-sm ">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group has-error">
                                        <label class="font-weight-bold">Select Bank Name<span class="text-danger"></span></label>
                                        <asp:DropDownList ID="ddlBankAccount" runat="server"
                                            class="form-control form-control-sm ">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group has-error">
                                        <label class="font-weight-bold">Select Cash Account<span class="text-danger">*</span></label>
                                        <asp:DropDownList ID="ddlCashaccount" runat="server" class="form-control form-control-sm ">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group has-error">
                                        <label class="font-weight-bold">Select Pension Expense<span class="text-danger">*</span></label>
                                        <asp:DropDownList ID="ddlPensionExpense" runat="server" class="form-control form-control-sm ">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6 border-left">

                                    <div class="card-body">
                                        <div class="form-group has-error">
                                            <label class="font-weight-bold">Select Sales Account<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlAcountsales" runat="server" class="form-control form-control-sm "></asp:DropDownList>
                                        </div>


                                        <div class="form-group has-error">
                                            <label class="font-weight-bold">Select purchase expense Account<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlaccountcogs" runat="server"
                                                class="form-control form-control-sm ">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group has-error">
                                            <label class="font-weight-bold">Select Inventory Account<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlinventory" runat="server"
                                                class="form-control form-control-sm ">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group has-error">
                                            <label class="font-weight-bold">Select Sales Tax Account<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddltaxpayable" runat="server"
                                                class="form-control form-control-sm">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group has-error">
                                            <label class="font-weight-bold">Select Pension Liability Account<span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlPensionLiability" runat="server"
                                                class="form-control form-control-sm">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </div>
                            </div>


                            <hr />

                            <div class="row" id="AutomationDiv" runat="server" visible="false">
                                <div class="col-xl-4 " style="font-weight: bold">
                                    <div class="custom-control custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="customCheck1" runat="server" clientidmode="Static" autopostback="True" oncheckedchanged="CheckBox1_CheckedChanged" />
                                        <label class="custom-control-label " for="customCheck1">Enable Credit Limit</label>

                                    </div>
                                    <div class="custom-control custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="Checkbox1" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label " for="Checkbox1">Enable Payroll Timesheet Integration</label>
                                    </div>
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="Checkbox2" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label " for="Checkbox2">Enable Payroll Bank Payment</label>
                                    </div>
                                    <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                        <input type="checkbox" class="custom-control-input" id="Checkbox3" runat="server" clientidmode="Static" />
                                        <label class="custom-control-label " for="Checkbox3">Enable Customer Payment with portal</label>
                                    </div>

                                    <div class="form-group mb-0">
                                        <label class="font-weight-bold mx-2">Pick Payroll Date(mm/dd/yyyy)<span class="text-danger">*</span></label>
                                        <div class="input-group input-group-alternative">
                                            <asp:TextBox ID="txtPayrollDate" class="form-control mx-2" runat="server"></asp:TextBox>


                                        </div>
                                    </div>

                                </div>

                                <div class="col-xl-8 text-right" style="font-weight: bold">

                                    <table class="table-bordered table-sm">
                                        <tbody>
                                            <tr>
                                                <td>SMS Automation</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="Button1" class="btn btn-sm btn-warning" OnClick="Button1_Click" runat="server" Text="Change" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Enable General Automation</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="Button3" class="btn btn-sm btn-warning" runat="server" Text="Change" OnClick="Button3_Click" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>


                                </div>


                            </div>

                        </div>
                        <hr />
                        <div class="card-body" id="AccountAuthDiv" runat="server" visible="false">
                            <h5 class="text-danger font-weight-light ">Accountant Access</h5>

                            <table class="table-bordered table-sm">
                                <thead>
                                    <th>Setting</th>
                                    <th>Fixed Asset</th>
                                    <th>Expense</th>
                                    <th>Banking</th>
                                    <th>CRM</th>
                                    <th>Employee info</th>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td class="text-center">
                                            <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                                <input type="checkbox" class="custom-control-input" id="settingCheck" runat="server" clientidmode="Static" />
                                                <label class="custom-control-label " for="settingCheck"></label>
                                            </div>
                                        </td>
                                        <td class="text-center">
                                            <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                                <input type="checkbox" class="custom-control-input" id="FixedCheck" runat="server" clientidmode="Static" />
                                                <label class="custom-control-label " for="FixedCheck"></label>
                                            </div>
                                        </td>
                                        <td class="text-center">
                                            <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                                <input type="checkbox" class="custom-control-input" id="ExpenseCheck" runat="server" clientidmode="Static" />
                                                <label class="custom-control-label " for="ExpenseCheck"></label>
                                            </div>
                                        </td>
                                        <td class="text-center">
                                            <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                                <input type="checkbox" class="custom-control-input" id="BankCheck" runat="server" clientidmode="Static" />
                                                <label class="custom-control-label " for="BankCheck"></label>
                                            </div>
                                        </td>

                                        <td class="text-center">
                                            <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                                <input type="checkbox" class="custom-control-input" id="CRMCheck" runat="server" clientidmode="Static" />
                                                <label class="custom-control-label " for="CRMCheck"></label>
                                            </div>
                                        </td>


                                        <td class="text-center">
                                            <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                                <input type="checkbox" class="custom-control-input" id="EmployeeCheck" runat="server" clientidmode="Static" />
                                                <label class="custom-control-label " for="EmployeeCheck"></label>
                                            </div>
                                        </td>
                                    </tr>

                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

