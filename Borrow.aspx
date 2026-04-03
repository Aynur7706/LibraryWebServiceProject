<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Borrow.aspx.cs" Inherits="LibraryWebServiceProject.Borrow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Kitab verilməsi və qaytarılması</h2>

    <asp:DropDownList ID="ddlBooks" runat="server"></asp:DropDownList><br /><br />
    <asp:DropDownList ID="ddlReaders" runat="server"></asp:DropDownList><br /><br />

    <asp:Button ID="btnBorrow" runat="server" Text="Kitabı ver" OnClick="btnBorrow_Click" /><br /><br />

    <asp:Label ID="lblMessage" runat="server"></asp:Label>

    <hr />

    <asp:GridView ID="gvBorrowedBooks" runat="server" AutoGenerateColumns="false" OnRowCommand="gvBorrowedBooks_RowCommand">
        <Columns>
            <asp:BoundField DataField="BorrowId" HeaderText="Borrow ID" />
            <asp:BoundField DataField="BookTitle" HeaderText="Kitab" />
            <asp:BoundField DataField="ReaderName" HeaderText="Oxucu" />
            <asp:BoundField DataField="BorrowDate" HeaderText="Verilmə tarixi" />
            <asp:BoundField DataField="ReturnDate" HeaderText="Qaytarılma tarixi" />
            <asp:BoundField DataField="IsReturned" HeaderText="Qaytarılıb?" />
            <asp:ButtonField Text="Qaytar" CommandName="ReturnBook" ButtonType="Button" />
        </Columns>
    </asp:GridView>

</asp:Content>