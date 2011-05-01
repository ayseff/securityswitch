<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteMap.ascx.cs" Inherits="ExampleWebSite.SiteMap" %>
<asp:Menu ID="menuSiteMap" runat="server" Orientation="Vertical">
	<StaticMenuItemStyle CssClass="menuItem" />
	<StaticHoverStyle CssClass="hoverMenuItem" />

	<DynamicMenuItemStyle CssClass="menuItem" />
	<DynamicHoverStyle CssClass="hoverMenuItem" />
</asp:Menu>