<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.master" AutoEventWireup="true" CodeBehind="Journal.aspx.cs" Inherits="advtech.Finance.Accounta.Journal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>General Journal</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <!-- Navbar -->

    <!-- End Navbar -->
    <!-- Header -->
                             <div class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
        <h5 class="modal-title text-gray-900" id="H1">Filter Record</h5>
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
            <asp:TextBox ID="txtDateform" class="form-control form-control-sm" TextMode=Date  runat="server"></asp:TextBox>
              <div class="input-group-prepend">
                <span class="input-group-text"><i class=" fas fa-calendar"></i></span>
              </div>
              
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
            <asp:TextBox ID="txtDateto" class="form-control  form-control-sm" TextMode=Date  runat="server"></asp:TextBox>
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

                                            <asp:Button ID="btnUpdate" class="btn btn-danger btn-sm" OnClick="btnUpdate_Click1" CausesValidation=false   runat="server"  
                                        Text="Search..."   />
                                        </div>
     </center>
      </div>
    </div>
  </div>

    <div class="container-fluid pl-3 pr-3">
      <!-- Table -->
      <div class="row">
              

               

        <div class="col">
                        <div class="card shadow-none">
            <div class="card-header bg-white ">
                <div class="row">
                    <div class="col-5 text-left ">
<h5 class="m-0 font-weight-bold text-primary">General Journal</h5>
                    </div>
                
                         <div class="col-7 text-right ">
            
                                             <button type="button"  runat=server id="Button4" class="mx-1  btn btn-sm btn-danger btn-circle" data-toggle="modal" data-target=".bd-example-modal-lg" >
                    <div>
                      <i class="fas fa-search text-white font-weight-bold"></i>
                   <span></span>
                    </div>
                  </button>
                                             <button type="button" visible="false"  runat=server id="Button13" class="mx-1 border-primary border-left border-top border-right border-bottom btn btn-sm btn-default btn-circle" data-toggle="modal" data-target="#exampleModalCenter2" >
                    <a title="You can pay opening bills(Accounts Payable) or any other payment for the vendor here!" data-toggle="tooltip" data-placement="bottom">
<div>

                      <i class="fas fa-donate text-primary font-weight-bold"></i>
                   <span></span>
                    </div>
                    </a>
                                                 

                  </button>
                                                                      
                    <button title="Add Transaction" id="modalMain" type="button" class="btn btn-sm btn-warning btn-circle" data-toggle="modal" data-target="#exampleModalCenter" >
                    <div>
                      <i class="fas fa-plus"></i>
                      <span></span>
                    </div>
                  </button>
                  
      

                    </div>
                </div>
                
                   
                            
            </div>
            
     <div class="row">
         <div class="col-8 border-right">
            <div class="card-header bg-white">
                <asp:Label ID="lblMsg" runat="server" ></asp:Label>
                            <div class="row align-items-center">


              </div>
            </div>
                            <div class="card-body small">
      <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                         
                <HeaderTemplate>
            <div class="table-responsive">

<table class="table align-items-center table-sm " style="empty-cells: hide">
                <thead >
                  <tr>
                 <th scope="col" class="text-left" >Date </th>
                  <th scope="col" class="text-center" style="color: #FF9900">Account</th>
                  <th scope="col" class="text-center">Explanation</th>

                    <th scope="col">Debit</th>
                    <th scope="col" class=text-center>Credit</th>
                   
          
                   
                  </tr>
                </thead>
                <tbody>
                                                                                        </HeaderTemplate>
                <ItemTemplate>
                  <tr >                                        <td>
                      <asp:Label ID="Label7" runat="server" Text=<%# Eval("Date", "{0: dd/MM/yyyy}")%>></asp:Label>
                    
                    </td>
                            <th  style="color: #0000CC">
                            <a title="Show the details" href="LedgerDetail.aspx?led=<%# Eval("Account1")%>"" style="text-align: left;">
                                <asp:Label ID="Label6" title="Debited Account" runat="server" Text=<%# Eval("Account1")%> ForeColor="#009900"></asp:Label>
                                                </a>
                                                <a title="Show the details" href="LedgerDetail.aspx?led=<%# Eval("Account2")%>""  style="text-align: right;">
<asp:Label ID="Label5" title="Credited Account" runat="server" Text=<%# Eval("Account2")%> ForeColor="DarkRed"></asp:Label>
</a>

                    
                            </th>     
                                       

                    <td style="vertical-align: bottom" class="text-center">
                              <asp:Label ID="Label8" runat="server" Text=<%# Eval("Explanation")%>></asp:Label>
                    </td>
          
                                        
                   




                                               <th>
                                                   <asp:Label ID="Label2" runat="server" Text= <%# Eval("Debit", "{0:N2}")%>></asp:Label>
                   
                    </th>

                                        <th>
                                            <asp:Label ID="Label3" runat="server" Text=<%# Eval("Credit", "{0:N2}")%>></asp:Label>
                    
                    </th>
</tr>
                                                                            </ItemTemplate>
                <FooterTemplate>
                </tbody>
              </table>
                                                                  </FooterTemplate>
                                                     
            </asp:Repeater>
                    
                                           <asp:Repeater ID="Repeater3" runat="server">
                                <ItemTemplate>
                                <table class="table align-items-center table-sm table-bordered ">


                <thead >
                                <tr>
                                <th scope="col">Total Balance:</th>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th
                                ><%# Eval("Credit", "{0:N2}")%> 
                                
                                </th>
                   <th><%# Eval("Debit", "{0:N2}")%>
                   
                   </th>
                                
                                    </tr>
                                    </thead>
                                   
                                    </table>
                                    </div>
                                </ItemTemplate>

                                </asp:Repeater>
                            </div>
         </div>
           <div class="col-4 small">
               <h5 class="m-0 font-weight-light text-danger mt-2 border-bottom">Attachment</h5>
               <asp:Repeater ID="Repeater2" runat="server">
                         
                <HeaderTemplate>
        
              <table class="table align-items-center table-sm ">
                <thead >
                  <tr>
                
                 
                    <th scope="col" class="text-gray-900 font-weight-bold">Attch. Name</th>
            
                    <th scope="col" class="text-right text-danger small">File Name</th>


                                          <th scope="col" class="text-gray-900 font-weight-bold">REMARK</th>
            
                    <th scope="col" class="text-right text-danger small">Date</th>
             
                  </tr>
                </thead>
                <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    
                  <tr>
           
                                        <td>
                    <%# Eval("attachname")%>
                    </td>
                    <td>
                    <a id="attachlink" data-toggle="tooltip" data-placement="bottom" title="Doenload file" target="_parent" href="deleteattachment.aspx?download=true&&file=<%# Eval("FileName")%>&&exte=<%# Eval("extension")%>" class="mx-2" ><%# Eval("FileName")%></a>
                    </td>                                                       
                                                                       <td>
                    <%# Eval("explanation")%>
                    </td>
                    <td>
                    <%# Eval("Date")%>
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
            <div class="card-footer bg-white py-4">
              <nav aria-label="...">
                <ul class="pagination justify-content-end mb-0">
                             <br /> 
                   <td>  <asp:label id="Label1" runat="server" class="m-1 text-primary"></asp:label></td>
                   <br /> 
                  <li class="page-item active">

                  <asp:Button ID="btnPrevious" class="btn btn-sm btn-primary btn-circle" runat="server" Text="<"  onclick="btnPrevious_Click"/>
                    
                  </li>

             
                                    <li class="page-item active">

                  <asp:Button ID="btnNext" class="btn btn-sm btn-primary btn-circle mx-2" runat="server" Text=">" onclick="btnNext_Click"/>
                    
                  </li>

                </ul>
              </nav>
            </div>
          </div>
        </div>
      </div>
      <div class="modal fade" id="Div1" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="Div1">Filter record</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
                            
                            <div class="row align-items-center">
                <div class="col-6 has-error">
                <label class="form-control-label" for="input-email">From:</label>
                <asp:TextBox ID="TextBox5" runat="server" class="form-control alert-danger " placeholder="Remark" TextMode="Date" ValidationGroup="A" BackColor="White" ForeColor="#999999"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="This Field is required"  ControlToValidate="TextBox5"></asp:RequiredFieldValidator>
                </div>
                                <div class="col-6">
                                <label class="form-control-label" for="input-email">TO:</label>
                <asp:TextBox ID="TextBox7" runat="server" class="form-control alert-danger" placeholder="Remark" TextMode="Date" ValidationGroup="A" BackColor="White" ForeColor="#666666"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="This Field is required" ControlToValidate="TextBox7"></asp:RequiredFieldValidator>
                </div>

                      

                
          
      </div>
      
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary btn-sm" data-dismiss="modal">Close</button>

                                      <asp:Button ID="Button7" runat="server"  class="btn btn-sm btn-default" 
                        Text="Search..."   onclick="Button7_Click"/>
                        
      </div>
      Option
<hr />
<asp:Button ID="Button12" runat="server" class="btn btn-round btn-sm btn-warning" CausesValidation=false Text="~Today" BackColor="#FDFDFD" ForeColor="#999966" />
          <asp:Button ID="Button8" runat="server" class="btn btn-round btn-sm btn-warning" CausesValidation=false Text="~month" OnClick="Month" BackColor="#FDFDFD" ForeColor="#999966" />
          <asp:Button ID="Button9" runat="server" class="btn btn-round btn-sm btn-warning" CausesValidation=false OnClick="PrevMonth" Text="Prev. Month" BackColor="#FDFDFD" ForeColor="#999966" />
          <asp:Button ID="Button10" runat="server" class="btn btn-round btn-sm btn-warning" CausesValidation=false Text="~Year" OnClick="year" BackColor="#FDFDFD" ForeColor="#999966" />
          <asp:Button ID="Button11" runat="server" class="btn btn-round btn-sm btn-warning" CausesValidation=false Text="Prv. Year" OnClick="year1" BackColor="#FDFDFD" ForeColor="#999966" />

    </div>
  </div>
</div>
    </div>
      <!-- Dark table -->
      <div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title h6 text-uppercase text-gray-900 font-weight-bold" id="exampleModalLongTitle">Add Records</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
       <div class="row" >
                                                                                <div class="col-md-6 ">
                                            <div class="form-group">
                                             <label class="text-gray-900 small">Debit Account</label>
                                                                       <asp:DropDownList ID="ddl1" runat="server" 
                                                    class="form-control form-control-sm " 
                                                   >
                   
                                                    
                                                </asp:DropDownList>
                </div>
                 </div>
                                                                                                 <div class="col-md-6 ">
                                            <div class="form-group">
                                             <label class="text-gray-900 small">Credit Account</label>
                                                                       <asp:DropDownList ID="DropDownList1" runat="server" 
                                                    class="form-control  form-control-sm" 
                                                   >
                   
                                                    
                                                </asp:DropDownList>
                </div>
                 </div>
                
                 

                

                                              <div class="col-md-6 ">
                                            <div class="form-group">

                                                <label class="text-gray-900 small">Explanation</label>
    
                                            
                                                 <br />
  <asp:TextBox ID="txtExplanation"  class="form-control  form-control-sm" placeholder="Explanation" runat="server"></asp:TextBox>
                                            </div>
                                        </div>


                                                                                                                        <div class="col-md-6 ">
                                            <div class="form-group">
                                                <label class="text-gray-900 small">Amount</label>
    
                                            
                                                 <br />
  <asp:TextBox ID="txtAmount" class="form-control form-control-sm" placeholder="Amount" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                                                                <div class="col-md-12 ">
                                            <div class="form-group">
                                                <label class="text-gray-900 small">Date</label>
    
                                            
                                                 <br />
  <asp:TextBox ID="txtDate" TextMode="Date" class="form-control  form-control-sm" placeholder="Date" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                                                                                                                                                        
                                    </div>

                <div class="modal-footer">
      <center>

                                            <asp:Button ID="Button3" class="btn btn-danger btn-sm"   runat="server" 
                                        Text="Save changes" OnClick="Yes"  CausesValidation=false/>
                                        </div>
     </center>

          </div>
      </div>

      </div>
    </div>
  </div>
      
        <div class="modal fade" id="exampleModalCenter2" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle2" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title text-gray-900 font-weight-bolder" id="exampleModalLongTitle2">Record your payables here</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
       <div class="row" >
                                                                                <div class="col-md-6 ">
                                            <div class="form-group">
                                             <label>Debit Account</label>
                                                                       <asp:DropDownList ID="ddlPayableDebit" runat="server" 
                                                    class="form-control " 
                                                   >
                   
                                                    
                                                </asp:DropDownList>
                </div>
                 </div>
                                                                                                 <div class="col-md-6 ">
                                            <div class="form-group">
                                             <label>Credit Account</label>
                                                                       <asp:DropDownList ID="ddlCashCredit" runat="server" 
                                                    class="form-control " 
                                                   >
                   
                                                    
                                                </asp:DropDownList>
                </div>
                 </div>
                
                 
</div>
          <div class="row border-top border-bottom">
                                             <div class="col-md-12 ">
                                            <div class="form-group">
                                             <label>Select Vendor</label>
                                                                       <asp:DropDownList ID="ddlVendor" runat="server" 
                                                    class="form-control " 
                                                   >
                   
                                                    
                                                </asp:DropDownList>
                </div>
                 </div>
                
                 
                 </div>
                <div class="row" >

                                              <div class="col-md-12 ">
                                            <div class="form-group">

                                                <label>Explanation</label>
    
                                            
                                                 <br />
  <asp:TextBox ID="txtPayableExplanation"  class="form-control" placeholder="Explanation" TextMode="MultiLine" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                    </div>
          <div class="row" >
                                      <div class="col-md-12 ">
                                            <div class="form-group">
                                                <label>Amount</label>
    
                                            
                                                 <br />
  <asp:TextBox ID="txtPayableAmount" class="form-control" placeholder="Amount" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
              </div>
                  <div class="row border-top border-bottom" >
                                      <div class="col-md-12 ">
                                            <div class="form-group">
                                               
    
                                            
                                                 <br />
  <asp:TextBox ID="txtAttachmentName" class="form-control" placeholder="Attachment Name" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
              </div>
                            <div class="row border-top border-bottom" >
                                      <div class="col-md-12 ">
                                            <div class="form-group">
                                               
    
                                            
                                                 <br />
                                                <asp:FileUpload ID="FileUpload1" class="form-control-sm" runat="server" />
                                            </div>
                                        </div>
              </div>
          <div class="row" >
                                                                                <div class="col-md-12 ">
                                            <div class="form-group">
                                                <label>Date</label>
    
                                            
                                                 <br />
  <asp:TextBox ID="txtPayableDate" TextMode="Date" class="form-control "  placeholder="Date" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                                </div>                                                                                                                        
                                    
      </div>
      <div class="modal-footer">
      <center>
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                            <asp:Button ID="btnPyable" class="btn btn-default btn-icon-split"   runat="server" 
                                        Text="Save changes" OnClick="btnPyable_Click"  CausesValidation=false/>
                                        </div>
     </center>
      </div>
    </div>
              <div class="row invisible">
              <span class="fas fa-arrow-circle-right text-danger mr-1 mx-2"  ></span><p class="small mx-2 font-weight-bold text-gray-900">If you are recording pension transaction, check the option below.</p>
              <div class="col-md-6">
               <div class="custom-control mb-2 custom-checkbox font-weight-300">
                     <input type="checkbox" class="custom-control-input" id="Checkbox2" runat="server" clientidmode="Static"/>
  <label class="custom-control-label text-xs text-gray-900" for="Checkbox2">Reset Employee Pension balance</label>
                     </div>
              </div>
                <div class="col-md-6">
                                   <div class="custom-control mb-2 custom-checkbox font-weight-300">
                     <input type="checkbox" class="custom-control-input" id="Checkbox1" runat="server" clientidmode="Static"/>
  <label class="custom-control-label text-xs text-gray-900" for="Checkbox1">Reset Company Pension balance</label>
                     </div>
                    </div>
  </div>



         
</asp:Content>

