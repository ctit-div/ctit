<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UcMakeOffer.ascx.cs" Inherits="UcMakeOffer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>



    <style type="text/css">
        
        .modalpopup {
            background-color: #ADADAD;
            filter: Alpha(Opacity=70);
            opacity: 0.70;
            /*-moz-opacity: 0.70;*/
        }
    </style>
    <script>
        // Get the modal
        var modal = document.getElementById('myModal');

        // Get the button that opens the modal
        var btn = document.getElementById("myBtn");

        // Get the <span> element that closes the modal
        var span = document.getElementsByClassName("close")[0];

        // When the user clicks the button, open the modal 
        btn.onclick = function () {
            modal.style.display = "block";
        }

        // When the user clicks on <span> (x), close the modal
        span.onclick = function () {
            modal.style.display = "none";
        }

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == modal) {
                modal.style.display = "none";
            }
        }
    </script>

    <asp:UpdatePanel ID="hhh222" runat="server">
        <ContentTemplate>

           <div class="main" style="font-family:Noor, Tahoma, Geneva, Verdana, sans-serif">
                <asp:Label ID="LblMessage" runat="server" CssClass="LblMessage"></asp:Label>
                <div class="row">
                    <div class="col-md-8">
                        <asp:Label ID="Label1" runat="server" Text="Items Info" CssClass="Title"></asp:Label>
                        <asp:Image ID="Image1" runat="server" Height="34px" ImageUrl="~/TitleLogo/2.PNG"  Width="100%" />
                        &nbsp;(<asp:Label ID="LblStatus" runat="server" Font-Size="Large"></asp:Label>)

                    </div>
                    <div class="col-md-4">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                            CssClass="LblMessage" DisplayMode="List" Font-Size="Small" ForeColor="#FF3300" />

                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>


                                <asp:Panel ID="pnlTextBoxes" runat="server" Visible="false">
                                </asp:Panel>
                            </ContentTemplate>
                            
                        </asp:UpdatePanel>

                    </div>
                </div>
       

            </div>
        </ContentTemplate>

    </asp:UpdatePanel>

    <div id="myModal" class="modal" role="dialog" data-backdrop="false">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Search Categories</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div>

                                <asp:TreeView ID="TreeView1" Width="50%" runat="server" MultiColumn="true" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" ShowCheckBoxes="All" ShowLines="True" ExpandDepth="0" OnTreeNodeExpanded="TreeView1_TreeNodeExpanded" ViewStateMode="Enabled">
                                    <SelectedNodeStyle BackColor="#996600" ForeColor="White" />
                                </asp:TreeView>

                            </div>
                        </div>

                    </div>
                    <p></p>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="button" class="btn btn-default" OnClick="BtnClose_Click" CausesValidation="false" data-dismiss="modal" runat="server" Text="Close" />
                    <%--<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                </div>
            </div>

        </div>
    </div>


