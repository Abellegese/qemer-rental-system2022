<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.master" AutoEventWireup="true" CodeBehind="help.aspx.cs" Inherits="advtech.Finance.Accounta.help" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Help</title>
    <link href="../../asset/bootstrap.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col">
                <div class="card shadow-none">
                    <div class="card-header mb-2 bg-white">
                        <div class="row align-content-center">
                            <div class="col-12">
                                <center>
                                    <span class="fas fa-lightbulb text-warning fa-1x"></span><span class="mx-2 h5 text-primary font-weight-bold">Help Center</span>
                                </center>

                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-1">
                        </div>
                        <div class="col-10 mb-2">
                            <div class="accordion accordion-flush" id="accordionFlushExample">
                                <div class="accordion-item">
                                    <h2 class="accordion-header" id="headingOne">
                                        <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                            Rent Module
                                        </button>
                                    </h2>
                                    <div id="collapseOne" class="accordion-collapse collapse show" aria-labelledby="headingOne" data-bs-parent="#accordionExample">
                                        <div class="accordion-body">
                                            <strong>What is Rent Service module and how can we can use it in QemerBook?</strong>
                                            <p>Rent service module provides a handy and easy way to deal with customer periodic rent. It contains Rent status which helps to view your costomer latest due status, Credit Notes: The credit you gave for your customer, Report of customer unpaid money, Customer details  information, Cash receipt module which shows cash recived from customer and shop details.</p>
                                            <strong>How can we use it?</strong>
                                            <p>Step 1: Add shop information <a class="btn-link" href="addshop.aspx">here</a></p>
                                            <p>Step 2: Add Customer information <a class="btn-link" href="Customer.aspx">here</a></p>
                                            <p>Step 2: Add Bank Account <a class="btn-link" href="AddAcounting.aspx">here</a></p>
                                            <p>Step 3: Go to Rent status page and see your customer current due status <a class="btn-link" href="rentstatus1.aspx">here</a></p>
                                            <p>Step 4: Their is four option at the end of table rows->Mark as recivable->Bank pay->Cash pay->Customer statment</p>
                                            <p>Click mark as receivable if you want to provide a full credit to your customer.</p>
                                        </div>
                                    </div>
                                </div>
                                
                                
                                
                                <div class="accordion-item">
                                    <h2 class="accordion-header" id="headingOne1">
                                        <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne1" aria-expanded="true" aria-controls="collapseOne1">
                                            Date Range Search
                                        </button>
                                    </h2>
                                    <div id="collapseOne1" class="accordion-collapse collapse " aria-labelledby="headingOne1" data-bs-parent="#accordionExample">
                                        <div class="accordion-body">
                                            <strong>How to search data using Date Range Search Function?</strong>
                                            <p class="mb-3">
                                                As we now that there are a difference in day, month and year calendar system between Ethiopian and GC. Software uses the computer system
              DateTime while the computer system uses GC calendar. So it is almost impossible to design and operate using ethiopian calendar in software world, at least for now.

                                            </p>
                                            <p class="font-weight-bold mb-2">To make effiecient and accurate search for filtering data use some techniques.</p>
                                            <p>Lets say that you want to filter data for one year of Ethiopian calendar</p>
                                            <p>GC-September-11  ===  መስከረም 1   EC</p>
                                            <p>GC-September 9  ===  ጳጉሜ 4   EC</p>
                                            <p>Doing that you will get the exact filtered data. Generally the date range appeared in the textbox is GC(Gregorian Calendar) so must select the date value with corresponding ethiopian calendar date.</p>
                                            <p>If you need an online Ethiopian to GC or GC to Ethiopian you can find it <a target="_blank" href="https://www.metaappz.com/Ethiopian_Date_Converter/Default.aspx">here</a></p>
                                        </div>
                                    </div>
                                </div>
                                <div class="accordion-item">
                                    <h2 class="accordion-header" id="headingOne2">
                                        <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne2" aria-expanded="true" aria-controls="collapseOne2">
                                            Possible Error
                                        </button>
                                    </h2>
                                    <div id="collapseOne2" class="accordion-collapse collapse " aria-labelledby="headingOne2" data-bs-parent="#accordionExample">
                                        <div class="accordion-body">
                                            <strong>There are some possible eeror might happen in the software.</strong>
                                            <p class="font-italic text-danger font-weight-bold h4 mb-1">1) The remote name could not be resolved: 'smtp.gmail.com'</p>
                                            <p class="font-italic text-danger font-weight-bold h4">2) The remote name could not be resolved: 'api.twilio.com'</p>
                                            --------------------------------------------------------------------------------------------------------------------------------
          <p class="mb-3">The above two error implies that there is  <b>no Internet Connection or Weak Internet Connection</b> </p>
                                            <strong>The other possible eeror might happen in the software is.</strong>
                                            <p class="font-italic text-danger font-weight-bold h4">Server Error in '/' Application. => Input string was not in a correct format.</p>
                                            <p class="font-italic mt-2 text-danger font-weight-bold h4">Server Error in '/' Application. => There is already an open DataReader associated with this Command which must be closed first.</p>
                                            -------------------------------------------------------------------------------------------------------------------------------------
          <p>If you encounter such an error. Contact us...0980174281 or <span class="text-primary small">abellegese5@gmail.com</span></p>
                                        </div>
                                    </div>
                                </div>
                                <div class="accordion-item">
                                    <h2 class="accordion-header" id="headingOne4">
                                        <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne4" aria-expanded="true" aria-controls="collapseOne4">
                                            Accounting System
                                        </button>
                                    </h2>
                                    <div id="collapseOne4" class="accordion-collapse collapse " aria-labelledby="headingOne4" data-bs-parent="#accordionExample">
                                        <div class="accordion-body">
                                            <strong>Accrual or Cash basis?</strong>
                                            <p>
                                                Based on the standard that has given in modern IFRS approach, Accrual accounting system is prefered. Qemerbook built in system uses accrual accounting system. Accrual accounting system recognizes revenues when they are earned not when the cash is received.
              When the customer provided with a certain a credit:
                                            </p>

                                            Debit
              -------
              Accounts Receivable
          <div></div>
                                            Credit
                                         -----------
                                         Rent Income
              ----------------------------------------------------
          <div>When the cash is received:</div>


                                            Credit
              -------
              Accounts Receivable
          <div></div>
                                            Debit
                                         -----------
                                         Cash
              ----------------------------------------------------
          <div></div>
                                            Accrual Basis accounting helps to determine the actual cash flow of the company.
          
 <h5 class="text-gray-900">Default Account in qemerbook</h5>
                                            <p>1) Accounts Receivable</p>
                                            <p>2) Accounts Payable</p>
                                            <p>3) Cash on Hand</p>
                                            <p>4) Cash at Bank</p>
                                            <p class="mt-3">Those account is necessary to run the general ledger system.</p>
                                            <strong>Refund and Bad Debt Expense</strong>
                                            <p>If the quantity in purchase order or bill got wrong. You can edit them in bill report page of the bill you want edit. After you edit the item number it automatically adjust the general ledger account.</p>
                                            <p>=>When you make sure that some of the customer does not pay the credit they owe, you can record it as bad debt expense---Credit Account Receivable and Debit Bad Debt expense</p>

                                            <p>If you are incorrectly inserted account value, you can adjust them in ledger details page or by reverse journal entry in general journal.</p>

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
    <script src="../../asset/bootstrap.min.js"></script>
</asp:Content>
