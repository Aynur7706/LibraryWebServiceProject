<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Books.aspx.cs" Inherits="LibraryWebServiceProject.Books" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Kitabların idarə olunması</h2>

    <asp:TextBox ID="txtTitle" runat="server" placeholder="Kitab adı"></asp:TextBox><br /><br />
    <asp:TextBox ID="txtAuthor" runat="server" placeholder="Müəllif"></asp:TextBox><br /><br />
    <asp:TextBox ID="txtCategory" runat="server" placeholder="Kateqoriya"></asp:TextBox><br /><br />
    <asp:TextBox ID="txtPublishYear" runat="server" placeholder="Nəşr ili"></asp:TextBox><br /><br />
    <asp:TextBox ID="txtQuantity" runat="server" placeholder="Say"></asp:TextBox><br /><br />

    <asp:Button ID="btnAddBook" runat="server" Text="Kitab əlavə et" OnClick="btnAddBook_Click" /><br /><br />

    <asp:Label ID="lblMessage" runat="server"></asp:Label>

    <hr />

    <asp:GridView ID="gvBooks" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="BookId" HeaderText="ID" />
            <asp:BoundField DataField="Title" HeaderText="Kitab adı" />
            <asp:BoundField DataField="Author" HeaderText="Müəllif" />
            <asp:BoundField DataField="Category" HeaderText="Kateqoriya" />
            <asp:BoundField DataField="PublishYear" HeaderText="Nəşr ili" />
            <asp:BoundField DataField="Quantity" HeaderText="Say" />
            <asp:BoundField DataField="AvailableCount" HeaderText="Mövcud say" />
        </Columns>
    </asp:GridView>

</asp:Content>