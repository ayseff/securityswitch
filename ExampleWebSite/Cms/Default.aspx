<%@ Page Title="Untitled" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ExampleWebSite.Cms.Default" %>
<asp:Content ID="Content2" ContentPlaceHolderID="primaryContent" runat="server">
	<h2><asp:Literal ID="litTitle" runat="server" Text="Untitled" /></h2>
	<asp:Literal ID="litContent" runat="server" />

	<div id="meta">
		<asp:Literal ID="litMeta" runat="server" />
	</div>
</asp:Content>