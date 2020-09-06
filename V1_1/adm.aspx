<%@ Page Title="Adm" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="adm.aspx.cs" Inherits="V1_1.adm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <h2><%: Title %></h2>
         <p <asp:Label ID="Label1" runat="server" Text=""></asp:Label></p>
         <p <asp:Label ID="Label2" runat="server" Text=""></asp:Label></p>
         <asp:Label ID="LabelU" runat="server" Text="0" Visible="False"></asp:Label> 
    <asp:TextBox ID="TextBox1" runat="server" CssClass="wide" Height="100px" TextMode="MultiLine" ></asp:TextBox>
    <asp:Button ID="Button1" runat="server" Text="Exec SQL" OnClick="Button1_Click" />
    <asp:TextBox ID="TextBox2" runat="server" CssClass="wide" Height="300px" TextMode="MultiLine" Font-Names="Courier New" ></asp:TextBox>


</asp:Content>
