<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<asp:Content ID="Content2" ContentPlaceHolderID="primaryContent" Runat="Server">
	<h2>Login</h2>
	<p>Login to access protected areas of this web site.</p>
	<p class="note">Pssst! Try using 'tperson' and 'password'.</p>

	<asp:Login ID="login" runat="server" TitleText="" OnAuthenticate="login_Authenticate" />
</asp:Content>