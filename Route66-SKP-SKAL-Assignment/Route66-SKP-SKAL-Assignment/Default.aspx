<%@ Page Title="Route 66" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Route66_SKP_SKAL_Assignment.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="col-md-2 RemoveWhenSmall">
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <img src="Assets/Images/Logo.png" style="height: 100%; width: 100%"/>
                </div>


                <div class="col-md-8">
                    <div class="jumbotron" style="border-bottom: 0;">
                        <p style="font-size:small;">New questions every month</p>
                        <!-- Bootstrap CSS -->
                        <div style="width:100%; height:100%;">
		
                            <!-- Carousel container -->
                            <div id="my-pics" class="carousel slide" data-ride="carousel" style="margin:auto;">

                                <!-- Content -->
                                <div runat="server" id="QuestionMenu" class="carousel-inner" role="listbox">

                                    <!-- Content 1 -->
                                    <div class="item active">
                                        <div style="text-align:center">
                                            <hr />
            
                                            <asp:Label runat="server" CssClass="QuestionText" ID="Question_Text1" Text="Is this a question about route 66?"></asp:Label>

                                            <br />
                                            <br />

                                            <asp:RadioButtonList runat="server" ID="AnswerList1" CssClass="AnswerList">
                                                <asp:ListItem Text="&nbsp;&nbsp; Answer 1"></asp:ListItem>
                                                <asp:ListItem Text="&nbsp;&nbsp; Answer 2"></asp:ListItem>
                                                <asp:ListItem Text="&nbsp;&nbsp; Answer 3"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>

                                    <!-- Content 2 -->
                                    <div class="item">
                                        <div style="text-align:center">
                                            <hr />
            
                                            <asp:Label runat="server" CssClass="QuestionText" ID="Question_Text2" Text="Is this another question about route 66?"></asp:Label>

                                            <br />
                                            <br />

                                            <asp:RadioButtonList runat="server" ID="AnswerList2" CssClass="AnswerList">
                                                <asp:ListItem Text="&nbsp;&nbsp; Answer 4"></asp:ListItem>
                                                <asp:ListItem Text="&nbsp;&nbsp; Answer 5"></asp:ListItem>
                                                <asp:ListItem Text="&nbsp;&nbsp; Answer 6"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>

                                    <!-- Content 3 -->
                                    <div class="item">
                                        <div  style="text-align:center">
                                            <hr />
            
                                            <asp:Label runat="server" CssClass="QuestionText" ID="Question_Text3" Text="Is this third question about route 66?"></asp:Label>

                                            <br />
                                            <br />

                                            <asp:RadioButtonList runat="server" ID="AnswerList3" CssClass="AnswerList">
                                                <asp:ListItem Text="&nbsp;&nbsp; Answer 7"></asp:ListItem>
                                                <asp:ListItem Text="&nbsp;&nbsp; Answer 8"></asp:ListItem>
                                                <asp:ListItem Text="&nbsp;&nbsp; Answer 9"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>

                                <!-- Previous/Next controls -->
                                <a class="left carousel-control" href="#my-pics" role="button" data-slide="prev">
                                    <span class="icon-prev" aria-hidden="true"></span>
                                    <span class="sr-only">Previous</span>
                                </a>
                                <a class="right carousel-control" href="#my-pics" role="button" data-slide="next">
                                    <span class="icon-next" aria-hidden="true"></span>
                                    <span class="sr-only">Next</span>
                                </a>

                            </div>
                        </div>

                        <!-- Initialize Bootstrap functionality -->
                        <script>
                            // Initialize tooltip component
                            $(function() {
                                $('[data-toggle="tooltip"]').tooltip();
                            });

                            // Initialize popover component
                            $(function() {
                                $('[data-toggle="popover"]').popover();
                            });
                        </script>
                    </div>

                    <div class="jumbotron">
                        <div>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>            
                                    <asp:Label runat="server" Text="Firstname: "></asp:Label> <asp:TextBox ID="FIRSTNAME_TEXTBOX" runat="server"></asp:TextBox>
                                    <br />
                                    <br />
                                    <asp:Label runat="server" Text="Lastname: "></asp:Label> <asp:TextBox ID="LASTNAME_TEXTBOX" runat="server"></asp:TextBox>
                                    <br />
                                    <br />
                                    <asp:Label runat="server" Text="E-Mail:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:Label> <asp:TextBox ID="EMAIL_TEXTBOX" TextMode="Email" runat="server"></asp:TextBox>
                                    <asp:Button runat="server" CssClass="btn btn-primary pull-right" ID="SubmitBtn" OnClick="SubmitBtn_Clicked" Text="Submit"/>
                                    <br />
                                    <asp:Label runat="server" ID="ERROR_LABEL" CssClass="ErrorLabel"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>

                <div class="col-md-2 RemoveWhenSmall">
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <img src="Assets/Images/Logo.png" style="height: 100%; width: 100%" alt="Logo"/>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
