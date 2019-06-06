<%@ Page Title="Admin" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Route66_SKP_SKAL_Assignment.Admin" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <div class="col-md-12">
            <div class="jumbotron">
                <asp:Button runat="server" CssClass="btn btn-primary" Text="Correct Answers"></asp:Button>
                <asp:Button runat="server" CssClass="btn btn-primary" Text="Correct Answers"></asp:Button>
                <asp:Button runat="server" CssClass="btn btn-primary" Text="Correct Answers"></asp:Button>
                <asp:Button runat="server" CssClass="btn btn-primary" Text="Correct Answers"></asp:Button>
                <asp:Button runat="server" CssClass="btn btn-primary" Text="Correct Answers"></asp:Button>
                <asp:Button runat="server" CssClass="btn btn-primary" Text="Correct Answers"></asp:Button>
                <asp:Button runat="server" CssClass="btn btn-primary" Text="Correct Answers"></asp:Button>

                <br />
                <br />

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

    <div class="container-fluid">
        <div class="col-md-4">
            <div class="jumbotron">
                <div class="SetStart">
                    <asp:Label runat="server" Text="Month: "></asp:Label> 
                    <asp:DropDownList ID="MONTH_LIST" runat="server">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:Label runat="server" Text="Year: &nbsp;&nbsp;"></asp:Label>
                    <asp:DropDownList ID="YEAR_LIST" runat="server">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:Button runat="server" CssClass="btn btn-primary" OnClick="Submitbtn" ID="StartBtn" Text="Set New Start Date"/>
                    <br />
                    <asp:Label runat="server" ID="ERROR_LABEL"></asp:Label>
                </div>
            </div>
        </div>
    </div>


    <asp:Table runat="server" ID="INFO_TABLE">

    </asp:Table>

</asp:Content>
