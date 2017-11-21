﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="configuration.aspx.cs" Inherits="mainCoursework.configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	

	Configure Users<br />
	

	<asp:GridView ID="usersDisplayTable" runat="server" AutoGenerateColumns="False" DataSourceID="usersDataSource" EmptyDataText="There are no data records to display." onrowcommand="usersDisplayTable_RowCommand">
		<Columns>
			<asp:BoundField DataField="username" HeaderText="User Name" SortExpression="username" />
			<asp:BoundField DataField="clearanceLevel" HeaderText="Access Level" SortExpression="clearanceLevel" />
			<asp:BoundField DataField="creator" HeaderText="Created By" />
			<asp:TemplateField>
				<ItemTemplate>
					<asp:Button ID="deleteButton" ButtonType="Button" CommandName="deleteUser" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Delete" runat="server" />
					<asp:Button ID="passwordChange" ButtonType="Button" CommandName="changeUserPassword" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Change Password" runat="server" />
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>

	<asp:TextBox ID="passwordBox" runat="server" TextType ="" placeholder ="New Password" TextMode="Password"></asp:TextBox>
	<br />
	<asp:TextBox ID="confirmPassword" runat="server" TextMode="Password" placeholder="Confirm Password"></asp:TextBox>
	<br />
	<asp:Label ID="returnLabel" runat="server" Text=""></asp:Label>
	<asp:AccessDataSource ID="usersDataSource" runat="server" DataFile="App_Data\main.accdb" SelectCommand="SELECT [username], [clearanceLevel], [creator] FROM [users]"></asp:AccessDataSource>
	<br />
	<br />
	Register a New User:<br />
	<asp:TextBox ID="submittedUsernameBox" runat="server" placeholder = "Username"></asp:TextBox>
	<br />
	<asp:TextBox ID="submittedPasswordBox" runat="server" TextMode="Password" placeholder ="Password"></asp:TextBox>
	<br />
	<asp:TextBox ID="submittedConfirmPasswordBox" runat="server" TextMode="Password" placeholder ="Confirm Password"></asp:TextBox>
	<br />
	<asp:TextBox ID="submittedAccessLevelBox" runat="server" placeholder ="Access Level" TextMode ="Number"></asp:TextBox>
	<br />
	<asp:Button ID="newUser" runat="server" Text="Register New User" OnClick="newUser_Click" />
	<asp:Label ID="registerReturn" runat="server" Text=""></asp:Label>
</asp:Content>
