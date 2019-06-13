<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Route66_SKP_SKAL_Assignment.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">

        <div class="col-md-4 RemoveWhenSmall">

        </div>

        <div class="col-md-4">
            <div class="jumbotron">

                <asp:Label runat="server" Text="Username"></asp:Label>
                <br />
                <asp:TextBox runat="server" ID="USERNAME_TEXT"></asp:TextBox>
                <br />
                <br />
                <asp:Label runat="server" Text="Password"></asp:Label>
                <br />
                <asp:TextBox runat="server" ID="PASSWORD_TEXT" Textmode="Password"></asp:TextBox>
                <br />
                <br />
                <asp:Button runat="server" ID="LOGIN_BTN" Text="Login" OnClick="Login_Click" CssClass="btn btn-primary"/>
                <br />
                <asp:Label runat="server" ID="ERROR_LABEL"></asp:Label>

            </div>
        </div>

        <div class="col-md-4 RemoveWhenSmall">

        </div>

    </div>

</asp:Content>
