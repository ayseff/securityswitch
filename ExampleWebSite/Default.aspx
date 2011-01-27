<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="ExampleWebSite.Default" Codebehind="Default.aspx.cs" %>
<asp:Content ID="Content2" ContentPlaceHolderID="primaryContent" Runat="Server">
	<h2>Welcome</h2>
	<p>This is an example site used to demonstrate (and manually verify) the SecuritySwitch module.</p>

	<h3>How to Use This Site to Test the Module</h3>
	<p>
		If you want to actually see the switch between HTTP and HTTPS happen on this website, you'll need to run it in IIS or IIS Express. 
		Below is an excerpt of an example configuration file for IIS Express. You can add the site elements to your own applicationhost.config 
		file in your Documents or My Documents folder, under IISExpress\config. Be sure to change the physical paths to match your environment.
	</p>

	<pre>...
    &lt;listenerAdapters&gt;
        &lt;add name="http" /&gt;
        &lt;add name="https" /&gt;
    &lt;/listenerAdapters&gt;

    &lt;sites&gt;
        ...
        &lt;site name="SecuritySwitchExampleSiteAsRoot" id="42" serverAutoStart="true"&gt;
            &lt;application path="/" applicationPool="Clr2IntegratedAppPool"&gt;
                &lt;virtualDirectory path="/" physicalPath="C:\Development\HTTP Modules\SecuritySwitch\ExampleWebSite" /&gt;
            &lt;/application&gt;
            &lt;bindings&gt;
                &lt;binding protocol="http" bindingInformation=":8042:localhost" /&gt;
                &lt;binding protocol="https" bindingInformation=":44342:localhost" /&gt;
            &lt;/bindings&gt;
        &lt;/site&gt;
        &lt;site name="SecuritySwitchExampleSiteWithApp" id="43" serverAutoStart="true"&gt;
            &lt;application path="/" applicationPool="Clr2IntegratedAppPool" enabledProtocols="http"&gt;
                &lt;virtualDirectory path="/" physicalPath="%IIS_SITES_HOME%\WebSite1" /&gt;
            &lt;/application&gt;
            &lt;application path="/ExampleWebsite" applicationPool="Clr2IntegratedAppPool" enabledProtocols="http"&gt;
                &lt;virtualDirectory path="/" physicalPath="C:\Development\HTTP Modules\SecuritySwitch\ExampleWebSite" /&gt;
            &lt;/application&gt;
            &lt;application path="/SecureExampleWebsite" applicationPool="Clr2IntegratedAppPool" enabledProtocols="https"&gt;
                &lt;virtualDirectory path="/" physicalPath="C:\Development\HTTP Modules\SecuritySwitch\ExampleWebSite" /&gt;
            &lt;/application&gt;
            &lt;bindings&gt;
                &lt;binding protocol="http" bindingInformation=":8042:localhost" /&gt;
                &lt;binding protocol="https" bindingInformation=":44342:localhost" /&gt;
            &lt;/bindings&gt;
        &lt;/site&gt;
        ...
    &lt;/sites&gt;
...</pre>
</asp:Content>