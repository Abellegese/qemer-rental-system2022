<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/Accounta/AccountMasterPage.Master" AutoEventWireup="true" CodeBehind="users.aspx.cs" Inherits="advtech.Finance.Accounta.users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>User</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="modal fade" id="exampleModal4" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel4" aria-hidden="true">
  <div class="modal-dialog modal-sm" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h6 class="modal-title text-gray-900" id="exampleModalLabel4">Upload Profile</h6>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        <div class="row mb-3">
            <div class="col-md-5">
                <label class="text-gray-900">Select Image</label>
                <asp:FileUpload ID="FileUpload1" class="form-control-sm" runat="server" />
            </div>

            </div>
                        <div class="col-md-5">

                            <asp:Button ID="Button2" runat="server" Text="Save" OnClick="Button2_Click1" class="btn btn-block btn-sm btn-warning" />
         
            </div>
      </div>
        <center>
      <div class="modal-footer">
       

       
       
      </div>

        </center>
    </div>
  </div>
</div>

    <div class="modal fade" id="exampleModal45" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel45" aria-hidden="true">
  <div class="modal-dialog modal-sm" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h6 class="modal-title text-gray-900" id="exampleModalLabel45">Upload Email Adress</h6>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        <div class="row mb-3">
            <div class="col-md-12">
             
                <asp:TextBox ID="txtEmail" class="form-control" placeholder="Email" runat="server"></asp:TextBox>
            </div>

            </div>
                        <div class="col-md-5">

                            <asp:Button ID="Button4" runat="server" Text="Save" OnClick="Button4_Click" class="btn btn-block btn-sm btn-warning" />
         
            </div>
      </div>
        <center>
      <div class="modal-footer">
       

       
       
      </div>

        </center>
    </div>
  </div>
</div>
     <div class="container-fluid">
           <div class="row">
        <div class="col">
                  <div class="row">

            <div class="col-xl-6 col-md-6">

              <!-- Default Card Example -->
              <div class="card shadow-none mb-2">
                <div class="card-header bg-white  font-weight-bold">
                <div class=row>
                   <div class="col-md-6 text-left">
                     <asp:Label ID="lblMsg" runat="server" ></asp:Label>
                    
                </div>
                <div class="col-md-6 text-right">


                  </div>
                </div>

                </div>
                <div class="card-body">
                  <center>
                  
                 <span class="">
                     <asp:Repeater ID="Repeater1" runat="server">
                         <ItemTemplate>
               <img class="rounded-circle" src="../../asset/userprofile/<%#Eval("FileName")%><%#Eval("Extension")%>"   height="70" width="70" /></span>
                         </ItemTemplate>
                     </asp:Repeater>
                     
                     
                  <br />
                  <div style="padding: 20px 0px 0px 0px">
                  <span id="name" runat="server" class="badge badge-light text-primary text-lg font-weight-bold"></span>
                      <button runat="server" title="Update status" id="Button5" type="button" class="btn btn-sm btn-default " data-toggle="modal" data-target="#exampleModal4">
                     <a data-toggle="tooltip" data-placement="bottom" title="Update profile">
                      <div>
                      <i class="fas fa-pencil-alt text-primary"></i>
                      
                    </div>
                         </a>
                  </button>
                  </div>
                  
                  </center>
                  <hr />
    
                  <div >
                  <span class="mb-2 font-weight-bolder text-xs text-uppercase text-gray-300 ">Basic Information</span>
         
                  
                  <div class=" mr-2 mb-2" >
                  <span><i class="fas fa-envelope-open-text text-center text-gray-900 "></i></span>  <span id="emailwork" class="small text-gray-800" runat="server"></span>
                  <button runat="server" title="Update status" id="Button3" type="button" class="btn btn-sm btn-default " data-toggle="modal" data-target="#exampleModal45">
                     <a data-toggle="tooltip" data-placement="bottom" title="Update Email">
                      <div>
                      <i class="fas fa-pencil-alt text-danger"></i>
                      
                    </div>
                         </a>
                  </button>
                  </div>
                   <div class=" mr-2 mb-2" >
                  <span><i class="fas fa-calendar-plus text-center text-gray-900 "></i></span>  <span id="datejoining" class="small text-gray-800" runat="server"></span>
                  </div>
                  
                                    
                                                    <div class=" mr-3">
                  <span><i class=" font-weight-bolder fas fa-bezier-curve text-center text-gray-900"></i></span>  <span id="department" class="small text-gray-800" runat=server title="Work Phone" ></span>
                  </div>

                  </div>
                </div>
              </div>
               
                
            </div>

            <div class="col-xl-6 col-md-6">

              <!-- Dropdown Card Example -->
              <div class="card mb-2 shadow-none">
                <!-- Card Header - Dropdown -->
                <div class="card-header bg-white py-3 d-flex flex-row align-items-center justify-content-between">
                  <h6 class="m-0 font-weight-bold text-gray-800">Personal Information</h6>

                </div>
                <!-- Card Body -->
          <div class=card-body>
             <div class="row">

            <!-- Earnings (Monthly) Card Example -->
            <div class="col-md-6 mb-1">
              <div >
                  <span class="mb-2 font-weight-bolder text-xs text-uppercase text-gray-300 ">Basic Information</span>
         
                  
                  
                  <div class=" mr-2 mb-2" >
                  <span><i class="text-center text-gray-900 text-xs ">Father's Name</i></span>  <span id="Span4" class="small text-gray-800" runat="server"></span>
                  </div>

                  <div class=" mr-2 mb-2" >
                  <span><i class=" text-center text-gray-900 text-xs ">Date of Birth</i></span>  <span id="Span5" class="small text-gray-800" runat="server"></span>
                  </div>

                   <div class=" mr-2 mb-2" >
                  <span><i class=" text-center text-gray-900 text-xs ">Contact Email</i></span>  <span id="Span2" class="small text-gray-800" runat="server"></span>
                  </div>
    
                     <div class=" mr-3 mb-2">
                  <span><i class="  text-center text-gray-900 text-xs">Residential Address</i></span>  <span id="Span3" class="small text-gray-800" runat=server title="Work Phone" ></span>
                  </div>
    <div class=" mr-2 mb-2" >
                  <span><i class=" text-center text-gray-900  text-xs">Mobile</i></span>  <span id="Span1" class="small text-gray-800" runat="server"></span>
                  </div>
                  </div>
            </div>

            <!-- Earnings (Monthly) Card Example -->
            <div class="col-md-6 mb-1">
              <div >
     <span class="mb-2 invisible">sd</span>
         
                  
                  
                  <div class=" mr-2 mb-2" >
                  <span><span class="text-center font-weight-bold text-xs  text-gray-900 "></span></span>  <span id="father" class="small font-weight-bold text-gray-800" runat="server"></span>
                  </div>

                  <div class=" mr-2 mb-2" >
                  <span><span class=" text-center font-weight-bold text-xs   text-gray-900 "></span></span>  <span id="birthdate" class="small font-weight-bold text-gray-800" runat="server"></span>
                  </div>

                   <div class=" mr-2 mb-2" >
                  <span><span class=" text-center font-weight-bold text-xs   text-gray-900 "></span></span>  <span id="contemail" class="small font-weight-bold text-gray-800" runat="server"></span>
                  </div>
    
                     <div class=" mr-3 mb-2">
                  <span><span class="  text-center font-weight-bold text-xs text-gray-900"></span></span>  <span id="addressres" class="small font-weight-bold text-gray-800" runat=server title="Work Phone" ></span>
                  </div>
    <div class=" mr-2 mb-2" >
                  <span><span class=" text-center font-weight-bold text-xs text-gray-900 "></span></span>  <span id="mobile" class="small font-weight-bold text-gray-800" runat="server"></span>
                  </div>
                  </div>
            </div>
             
            <!-- Earnings (Monthly) Card Example -->

          </div>
               </div>           

              </div>


              <div class="card shadow-none mb-2">

               <div class="card-header bg-white  d-flex flex-row align-items-center justify-content-between">
              <h6 class="m-0 font-weight-bold text-gray-800">Change Username and Password</h6>
             <div class="row align-items-center">
             <div class=" col-12 text-right">

             </div>
             </div>
                </div>
               
                <!-- Card Content - Collapse -->
     
                  <div class="card-body">
            <div class="row ">
            <div class="col-md-4 ">
                <div class="form-group">
                  <div class="input-group input-group-alternative mb-3">

                                <asp:TextBox ID="txtNewPass" class="form-control" placeholder="New Password"  runat="server" 
                          TextMode="Password"></asp:TextBox>
                  </div>
                    </div>
                </div>
                <div class="col-md-4 ">
                <div class="form-group">
                  <div class="input-group input-group-alternative">

                          <asp:TextBox ID="txtComfirmPassword" class="form-control" placeholder="Confirm Password"  runat="server" 
                          TextMode="Password"></asp:TextBox>

                  </div>
                </div>
                </div>
                            <div class="col-md-4 ">
                <div class="form-group">
                  <div class="input-group input-group-alternative mb-3">

                                <asp:TextBox ID="txtUname" class="form-control  text-gray-900" placeholder="Username"  runat="server" 
                          ></asp:TextBox>

                  </div>
                </div>
                </div>


                </div>
                   
                <center>
                    
                <asp:Button ID="Button1" class="btn btn-primary btn-sm" runat="server" Text="Update" OnClick="Button2_Click" />
        
                </center>
        
              </div>
                
            </div>

          </div>
        </div>
        </div>

 </div>
</asp:Content>
