<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
    Home - My Site
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <main class="d-flex justify-content-center align-items-center vh-100">
        <div class="text-center">
            <h1>Welcome to My Site!</h1>
            <p>
                This is a simple example using Bootstrap's responsive grid layout and
                styling.
            </p>
        </div>
    </main>
</asp:Content>
