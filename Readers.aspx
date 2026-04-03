<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Readers.aspx.cs" Inherits="LibraryWebServiceProject.Readers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Oxucuların idarə olunması</h2>

    <asp:TextBox ID="txtFullName" runat="server" placeholder="Ad Soyad"></asp:TextBox><br /><br />
    <asp:TextBox ID="txtPhone" runat="server" placeholder="Telefon"></asp:TextBox><br /><br />
    <asp:TextBox ID="txtEmail" runat="server" placeholder="Email"></asp:TextBox><br /><br />
    <asp:TextBox ID="txtAddress" runat="server" placeholder="Ünvan"></asp:TextBox><br /><br />

    <asp:Button ID="btnAddReader" runat="server" Text="Oxucu əlavə et" OnClick="btnAddReader_Click" /><br /><br />

    <asp:Label ID="lblMessage" runat="server"></asp:Label>

    <hr />

    <asp:GridView ID="gvReaders" runat="server" AutoGenerateColumns="true"></asp:GridView>

</asp:Content>