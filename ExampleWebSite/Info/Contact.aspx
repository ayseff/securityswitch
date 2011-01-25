<%@ Page Title="Contact Fictitious Company" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="ExampleWebSite.Info.Contact" Codebehind="Contact.aspx.cs" %>
<asp:Content ID="Content2" ContentPlaceHolderID="primaryContent" Runat="Server">
	<h2>Contact Fictitious Company</h2>
	<p>You don't <em>really</em> want to contact a fictitious company; do you?</p>

	<fieldset>
		<legend>Your Information</legend>
		<div class="field">
			<asp:Label runat="server" Text="Your Name" AssociatedControlID="textName" />
			<asp:TextBox ID="textName" runat="server" />
		</div>
		<div class="field">
			<asp:Label runat="server" Text="Your E-mail" AssociatedControlID="textEmail" />
			<asp:TextBox ID="textEmail" runat="server" />
		</div>
		<div class="field stacked">
			<asp:Label runat="server" Text="Your Message" AssociatedControlID="textMessage" />
			<asp:TextBox ID="textMessage" runat="server" TextMode="MultiLine" />
		</div>

		<div>
			<asp:Button it="buttonSend" runat="server" Text="Send" />
		</div>
	</fieldset>
</asp:Content>