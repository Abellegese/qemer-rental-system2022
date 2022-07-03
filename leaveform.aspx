<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="leaveform.aspx.cs" Inherits="SignalR_Research.Finance.Accounta.leaveform" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Leave Form</title>
        <style>

.vr {
  display: inline-block;
  position:center;

  width: 1px;
  min-height: 1em;
  background-color:black;
  opacity: 0.25;
}
</style>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid pr-3 pl-3" style="position: relative;">
            <div class="modal fade bd-example-modal-lg" tabindex="-1" id="exampleModal1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
        <h5 class="modal-title" id="H1">Request Leave</h5>
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
            <asp:TextBox ID="txtDateform" class="form-control " TextMode=Date  runat="server"></asp:TextBox>
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
            <asp:TextBox ID="txtDateto" class="form-control " TextMode=Date  runat="server"></asp:TextBox>
              <div class="input-group-prepend">
                <span class="input-group-text"><i class=" fas fa-calendar"></i></span>
              </div>
              
            </div>
          </div>
          </div>
          </div>

                                                 </div>  
          <div class="row" >
                                      


                                                                                                        <div class="col-12 ">
                                            <div class="form-group">
                                                <label class=font-weight-bold>Reason<span class=text-danger>*</span></label>
    
                                            
                                                 <br />
                                                           <div class="form-group mb-0">
            <div class="input-group input-group-alternative">
            <asp:TextBox ID="txtReason" class="form-control " Height="150px" TextMode="MultiLine" runat="server"></asp:TextBox>

            </div>
          </div>
          </div>
          </div>
                                                  </div>
                    <div class="row" >
                                      


                                                                                                        <div class="col-12 ">
                                            <div class="form-group">
                                                <label class=font-weight-bold>Attachment<span class=text-danger>*</span></label>
    
                                            
                                                 <br />
                                                           <div class="form-group mb-0">
            <div class="input-group input-group-alternative">
<asp:FileUpload ID="FileUpload1" class="form-control mb-3" runat="server" />

            </div>
          </div>
          </div>
          </div>
                                                  </div>
      </div>
      <div class="modal-footer">
      <center>
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                            <asp:Button ID="btnUpdate" class="btn btn-primary" OnClick="btnUpdate_Click" runat="server"  
                                        Text="Send Request"   />
                                        </div>
     </center>
      </div>
    </div>
  </div>
<div class="row" >
	<div class="col">
		                        <div class="card shadow-none">
            <div class="card-header shadow-none bg-white ">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
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
                                             <button type="button"  runat=server id="Button4" class="mx-1 border-primary border-left border-top border-right border-bottom btn btn-sm btn-default btn-circle" data-toggle="modal" data-target="#exampleModal1" >
                    <div>
                      <i class="fas fa-plus text-primary font-weight-bold"></i>
                   <span></span>
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
                <a class=" text-primary" href="leaveform.aspx?ref2=<%# Eval("id")%>"><span class="small text-primary">REQ#<%# Eval("id")%></span></a>
                                            
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

                    <div id="leavecard" runat="server" class="card shadow-sm mt-2 p-2 h-100">
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
                   <span class="mb-2 text-gray-900 small " id="ad" runat="server">Approved Date<span class="h6 mx-1 mb-2 text-gray-900 small " id="Span2" runat="server"></span></span>
      <h6 id="approveddate" runat="server"><span class="mb-2 text-success small ">Duration:</span><span class=" mx-1 mb-2 text-gray-900 small text-right  " id="alldatefrom" runat="server"></span> <span class="small text-danger">To</span> <span id="allodateto"  class=" mx-1 mb-2 text-gray-900 small text-right" runat="server"></span></h6>
             <span class="text-danger small" id="appstat" runat="server">Not Decided Yet.</span>
     <h6 id="approvershow" runat="server"><span class="small text-gray-600">Checked By:</span><span class="mx-2 text-danger small" id="approver" runat="server"></span></h6>
    <p class="text-xs text-gray-900 " id="reasongh" runat="server"></p>
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
