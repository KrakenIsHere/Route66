<%@ Page Title="Admin" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Route66_SKP_SKAL_Assignment.Admin" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link type="text/css" href="~/Content/AdminPage.css" rel="stylesheet"/>

    <br />

    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12">
                <div class="jumbotron">
                    <div class="SetStart">
                        <asp:Label runat="server" Text="Question: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:Label> 
                        <asp:TextBox runat="server" CssClass="SubmitQuestionTextbox" ID="QUESTION_TEXT"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Label runat="server" Text="Answer #1: &nbsp;&nbsp;"></asp:Label>
                        <asp:TextBox runat="server" CssClass="SubmitQuestionTextbox" ID="ANSWER1_TEXT"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Label runat="server" Text="Answer #2: &nbsp;&nbsp;"></asp:Label>
                        <asp:TextBox runat="server" CssClass="SubmitQuestionTextbox" ID="ANSWER2_TEXT"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Label runat="server" Text="Answer #3: &nbsp;&nbsp;"></asp:Label>
                        <asp:TextBox runat="server" CssClass="SubmitQuestionTextbox" ID="ANSWER3_TEXT"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Label runat="server" Text="Correct Answer: "></asp:Label>
                        <asp:DropDownList runat="server" CssClass="btn btn-primary" ID="ANSWER_DROP">
                            <asp:ListItem Value="0" Text="Select Answer"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Answer #1"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Answer #2"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Answer #3"></asp:ListItem>
                        </asp:DropDownList>

                        <asp:Button runat="server" CssClass="btn btn-primary" OnClick="SubmitNewQuestionBtn" ID="QuestionBtn" Text="Submit New Question"/>
                        <br />
                        <asp:Label runat="server" ID="QUESTION_ERROR_LABEL"></asp:Label>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-3">
                <div class="jumbotron">
                    <div class="SetStart">
                        <asp:Label runat="server" Text="Month: "></asp:Label> 
                        <br />
                        <asp:DropDownList ID="MONTH_LIST" runat="server">
                        </asp:DropDownList>
                        <br />
                        <br />
                        <asp:Label runat="server" Text="Year: &nbsp;&nbsp;"></asp:Label>
                        <br />
                        <asp:DropDownList ID="YEAR_LIST" runat="server">
                        </asp:DropDownList>
                        <br />
                        <br />
                        <asp:Button runat="server" CssClass="btn btn-primary" OnClick="SubmitNewStartBtn" ID="StartBtn" Text="Set New Start Date"/>
                        <br />
                        <asp:Label runat="server" ID="ERROR_LABEL"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="col-md-9">
                <div class="jumbotron">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:Label runat="server" Text="Message sender: &nbsp;&nbsp;&nbsp;"></asp:Label>
                            <asp:Textbox ID="txtFrom" CssClass="SendEmailTextbox" TextMode="Email" Text="krakendev122@gmail.com" Enabled=" false" runat="server" />
                            <br>
                            <br>
                            <asp:Label runat="server" Text="Message recipient: &nbsp;"></asp:Label>
                            <asp:Textbox ID="txtTo" CssClass="SendEmailTextbox" TextMode="Email" runat="server" ToolTip="Insert ',' to seperate E-Mails" />
                            <br>
                            <br>
                            <asp:Label runat="server" Text="Message subject: &nbsp;&nbsp;&nbsp;"></asp:Label>
                            <asp:Textbox ID="txtSubject" CssClass="SendEmailTextbox" Text="Route66 competition WINNER" runat="server" />
                            <br>
                            <br>
                            <asp:Label runat="server" Text="Message:"></asp:Label>
                            <br/>
                            <br>
                            <asp:Textbox ID="txtBody" CssClass="SendEmailTextBody" runat="server" Height="150px" Text="Test Mail" TextMode="multiline" /><br>
                            <asp:Button ID="btn_SendMessage" CssClass="btn btn-primary" runat="server" OnClick="SendMessage_Click" Text="Send message" /><br>
                            <asp:Label ID="Label1" runat="server" Text="" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-3">
                <asp:Button runat="server" CssClass="btn btn-primary" OnClick="SearchAllAnswersBtn" Text="All Answers"></asp:Button>
                <asp:Button runat="server" CssClass="btn btn-primary" OnClick="SearchAllCorrectAnswersBtn" Text="Correct Answers"></asp:Button>
            </div>

            <div class="col-md-5">
                <asp:Button runat="server" CssClass="btn btn-primary" OnClick="SearchAllCorrectAnswersByQuestionBtn" Text="#Question Correct"></asp:Button>
                <asp:Button runat="server" CssClass="btn btn-primary" OnClick="SearchAllAnswersByQuestionBtn" Text="#Question Answers"></asp:Button>
                <asp:DropDownList runat="server" CssClass="btn btn-primary" ID="CORRECT_BY_QUESTION_DROP" ToolTip="Question Number/ID">

                </asp:DropDownList>
            </div>
            <div class="col-md-4">
                <asp:Button runat="server" CssClass="btn btn-primary" OnClick="SearchAllAnswersByMonthBtn" Text="#Question Answers"></asp:Button>
                <asp:DropDownList runat="server" CssClass="btn btn-primary" ID="QUESTION_MONTHS_DROP" ToolTip="Questions From Month">
                    
                </asp:DropDownList>
            </div>
        </div>


        <div class="row">
            <div class="col-md-12">
                <div class="jumbotron">
                    <asp:GridView ID="DATA_GRID" runat="server" CellPadding="4" ForeColor="#333333" GridLines="Both" Width="100%">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"/>
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"/>
                        <PagerStyle BackColor="#284775" ForeColor="White"/>
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
