﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Customer.Master" AutoEventWireup="true" CodeBehind="employeeLogin.aspx.cs" Inherits="mainCoursework.employeeLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	Employee Login
	<br />
	<div class="login">
		<asp:TextBox ID="employeeUsernameBox" runat="server" placeholder ="Username"></asp:TextBox>
		<br />
		<asp:TextBox ID="employeePasswordBox" runat="server" TextMode="Password" placeholder ="Password"></asp:TextBox>
		<p><asp:Label ID="employeeLoginReturnLabel" runat="server" CssClass="returnLabel"></asp:Label></p>
		<asp:Button ID="employeeSubmitCredentialsButton" runat="server" Text="Log In" OnClick="submitEmployeeCredentialsButton_Click"/>
		<br />
		<asp:Button ID="customerRedirect" runat="server" Text="Customer Login" OnClick="customerRedirect_Click" />
	</div>
</asp:Content>
