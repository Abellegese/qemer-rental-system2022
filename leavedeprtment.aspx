﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="leavedeprtment.aspx.cs" Inherits="SignalR_Research.Finance.Accounta.leavedeprtment" %>
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
    <title>Leave Request</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container-fluid" style="position: relative;">
            <div class="modal fade" id="exampleModal1" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel1" aria-hidden="true">
  <div class="modal-dialog modal-sm" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h6 class="modal-title text-gray-900" id="exampleModalLabel1">Add permited No. of Days</h6>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        <div class="row">
            <div class="col-md-7">
                <asp:TextBox ID="txtAllowedDate"  class="form-control mx-2" runat="server"></asp:TextBox>

            </div>
            <div class="col-md-3">
             <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="Save"  OnClick="Button2_Click" />

            </div>

        </div>
          <hr />
          <div class="row">
              <div class="col-md-12">
                                                        <div class="custom-control mb-2 custom-checkbox font-weight-300">
                                                                <input type="checkbox" class="custom-control-input" id="Checkbox2" runat="server" clientidmode="Static"/>
  <label class="custom-control-label " for="Checkbox2">Reduce Empl. leave days</label>
                     </div>
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
<div class="row" >
	<div class="col">
		                        <div class="card shadow-none">
            <div class="card-header bg-white ">
                <div class="row">
                    <div class="col-5 text-left ">
<h5 class="m-0 font-weight-bold text-primary">Leave Request Form</h5>
                    </div>
                
                         <div class="col-7 text-right ">
                                                         <button name="b_print" onclick="printdiv('div_print');" type="button" title="Print" class="border-primary border-left border-top border-right border-bottom btn btn-sm btn-default btn-circle" data-toggle="modal" data-target="#exampleModalCenter" >
                    <div>
                      <i class="fas fa-print text-primary font-weight-bold"></i>
                   
                    </div>
                  </button>
                                                                        <button type="button"  runat=server id="btnApprove" class="mx-1 btn btn-sm btn-success" data-toggle="modal" data-target="#exampleModal1" >
                    <div>
                      
                   <span>Approve</span>
                    </div>
                  </button>
                                                                                                     <button type="button"  runat=server id="btnDeny" class="mx-1 btn btn-sm btn-danger" data-toggle="modal" data-target="#exampleModal12" >
                    <div>
                      
                   <span>Deny</span>
                    </div>
                  </button>
            

 
                  
      

                    </div>
                </div>
                
                   
                            
            </div>
            </div>
          <div class="card">
			<div class="row g-0">
				<div class="col-12 col-lg-5 col-xl-4 border-right">
                  

						<div class="chat-messages p-4">

                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                         
                <HeaderTemplate>
        
              <table class="table align-items-center table-sm ">
                <thead >
                  <tr>
                
                 
                    <th scope="col" class="text-gray-900 font-weight-bold">REQ#</th>
            
                    <th scope="col" class="text-right text-danger small">STATUS</th>



             
                  </tr>
                </thead>
                <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    
                  <tr>
           
                  <td>
                <a class=" text-primary" href="leavedeprtment.aspx?ref2=<%# Eval("id")%>"><span class="small text-primary">REQ#<%# Eval("id")%></span></a>
                                            
                                            (<span ID="Label1" class="mx-1 small text-primary" runat="server" ><%# Eval("date_requested", "{0: dd/MM/yyyy}")%></span>)
                    </td>


                  <td class="text-right">
<asp:Label ID="Label2" runat="server" Text=<%# Eval("status")%>></asp:Label>
                  </td>                                                             
                                                         
                  </tr>
                  
                   </ItemTemplate>
                <FooterTemplate>
                </tbody>
              </table>
                                                                  </FooterTemplate>
                                                     
            </asp:Repeater>
						</div>
					

					<hr class="d-block d-lg-none mt-1 mb-0">
				</div>
                <div class="col-12 col-lg-7 col-xl-8">
                                        <div class="mt-lg-5" id="leaveempt" runat="server">
                    <center>
                    <span class="fas fa-id-card fa-4x text-gray-400"></span>
                        <h6 class="text-gray-500">Select leave to show</h6>
                    </center>
                    </div>

                    <div id="div_print">
                    <div id="leavecard" runat="server" class="card shadow-sm mt-2 p-2">
<div class="row">
                        <div class="col-md-6">
                            <div class="card-header text-left text-black bg-white font-weight-bold">
        
                    <h5 id="oname" runat="server" class="mb-1 text-gray-600 font-weight-bold "></h5>
<h6 class="mb-1 h4 text-uppercase text-gray-900 ">LEAVE form<span class="small"><sup id="Status2" class="mx-2" runat="server"></sup></span></h6>
                   <span class="mb-2 text-gray-900 small ">Date of Requested:</span><span class=" mx-1 mb-2 text-gray-900 small text-right  " id="RequestedDate" runat="server"></span><div class="vr"></div><span class="mb-2 text-primary small ">REQ#:</span><span class=" mx-1 mb-2 text-gray-900 small text-right  " id="Reqno" runat="server"></span>
                                <h6><span class="mb-2 text-primary small ">Applicant:</span><span class=" mx-1 mb-2 text-primary small text-right  " id="applicant" runat="server"></span>(<span class="text-danger small" id="position2" runat="server"></span>:<span class="small text-primary" id="phoneno" runat="server"></span>)</h6>
                                 <h6 ><span class="mb-2 text-success small ">Duration:</span><span class=" mx-1 mb-2 text-gray-900 small text-right  " id="durationfrom" runat="server"></span> <span class="small text-danger">To</span> <span id="durationto"  class=" mx-1 mb-2 text-gray-900 small text-right" runat="server"></span></h6>
                                <h6 class="small text-gray-900 " >Current Leave Days:(<span id="currentleave" runat="server" class=" text-primary small"></span>)</h6>
              <asp:Label ID="Label3" runat="server" ></asp:Label>
                            <div class="row align-items-center">
                                
                <div class="col-12 text-right">
                </div>
              </div>
            </div>
                        </div>
                        <div class="vr"></div>
                          <div class="col-md-5">
<div class="card-header text-right text-black bg-white font-weight-bold">
        
                    <h5 id="H3" runat="server" class="mb-1 text-gray-600 font-weight-bold "></h5>
<h6 class="mb-1 h4 text-uppercase text-gray-600 ">Status</h6>
                   <span class="mb-2 text-gray-900 small ">Approved Date<span class="h6 mx-1 mb-2 text-gray-900 small " id="Span2" runat="server"></span></span>
      <h6 id="approveddate" runat="server"><span class="mb-2 text-success small ">Duration:</span><span class=" mx-1 mb-2 text-gray-900 small text-right  " id="Span1" runat="server"></span> <span class="small text-danger">To</span> <span id="Span3"  class=" mx-1 mb-2 text-gray-900 small text-right" runat="server"></span></h6>
             <span class="text-danger small" id="appstat" runat="server">Not Decided Yet.</span>
     
            </div>
                        </div>
                    </div>
                        <hr />
                        <div class="card-body">
                            <center>
                                <span class="text-gray-900 h5 text-uppercase font-weight-bold mb-3" style="text-decoration:underline">APPLICANT REASON</span>
                            </center>
                            <p class="text-gray-900" style="text-align: justify;" id="reason2" runat="server"></p>
                        </div>
                        <hr />
                    </div>
                         <div id="attachmnetdiv" runat="server">
<div class="row">
         <div class="col-md-12" >
             <hr />
             <span class="small text-gray-900 font-weight-bolder">Attachment</span>:<a id="attachlink" target="_parent" class="mx-2" runat="server"><span class="small text-primary" id="attachname" runat="server"></span></a>
             <hr />
             
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
	
    <div class="modal fade" id="exampleModal12" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel12" aria-hidden="true">
  <div class="modal-dialog modal-sm" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h6 class="modal-title text-gray-900" id="exampleModalLabel12">Deniel Reason</h6>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        <div class="row">
            <div class="col-md-12">
                <asp:TextBox ID="txtMob" TextMode="MultiLine"  class="form-control mx-2" runat="server"></asp:TextBox>

            </div></div>
          <div class="row">
            <div class="col-md-12 ">
             <asp:Button ID="btnDeny1" runat="server" class="btn mx-2 mt-2 btn-danger form-control" Text="Deny" OnClick="btnDeny1_Click"  />

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
<style type="text/css">


.chat-online {
    color: #34ce57
}

.chat-offline {
    color: #e4606d
}

.chat-messages {
    display: flex;
    flex-direction: column;
    max-height: 800px;
    overflow-y: scroll
}

.chat-message-left,
.chat-message-right {
    display: flex;
    flex-shrink: 0
}

.chat-message-left {
    margin-right: auto
}

.chat-message-right {
    flex-direction: row-reverse;
    margin-left: auto
}
.py-3 {
    padding-top: 1rem!important;
    padding-bottom: 1rem!important;
}
.px-4 {
    padding-right: 1.5rem!important;
    padding-left: 1.5rem!important;
}
.flex-grow-0 {
    flex-grow: 0!important;
}
.border-top {
    border-top: 1px solid #dee2e6!important;
}
</style>
</asp:Content>
