<%@ Page Title="Сводный отчет за период" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Info3.aspx.cs" Inherits="V1_1.Info3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <h2><%: Title %></h2>
         <p <asp:Label ID="Label1" runat="server" Text=""></asp:Label></p>
         <p <asp:Label ID="Label2" runat="server" Text=""></asp:Label></p>
         <asp:Label ID="LabelU" runat="server" Text="0" Visible="False"></asp:Label> 
<div class="form-horizontal">

     
        <asp:Panel ID="Panel1" runat="server">
            <h4>Доступ запрещен</h4>
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server" Visible="False">
            <div class="form-group">                
                <asp:Label runat="server" AssociatedControlID="Calendar1" CssClass="col-md-2 control-label">Дата начала периода</asp:Label>
                <div class="col-md-10">
                <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px" >
                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                    <NextPrevStyle VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#808080" />
                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                    <SelectorStyle BackColor="#CCCCCC" />
                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                    <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <WeekendDayStyle BackColor="#FFFFCC" />
                    </asp:Calendar>
                    <br />
                </div>
            </div>
           <div class="form-group">                
                <asp:Label runat="server" AssociatedControlID="Calendar2" CssClass="col-md-2 control-label">Дата окончания периода</asp:Label>
                <div class="col-md-10">
                <asp:Calendar ID="Calendar2" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px" >
                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                    <NextPrevStyle VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#808080" />
                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                    <SelectorStyle BackColor="#CCCCCC" />
                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                    <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <WeekendDayStyle BackColor="#FFFFCC" />
                    </asp:Calendar>
                    <br />
                </div>
            </div>
                <h4>Сбор данных о работе отделений СЗГУ</h4>
                <div class="form-group">  
                    <asp:Label runat="server" AssociatedControlID="Button1" CssClass="col-md-2 control-label"></asp:Label>
                    <div class="col-md-10">
                        <table style=" text-align: left;">
                            <tr>
                                <td><br /><asp:Button ID="Button1" runat="server" Text="Отчет"   Width="250px" OnClick="Button1_Click"/></td>
                            </tr>                          
                        </table> 
                    </div>
                </div>










        </asp:Panel>
</div>
</asp:Content>
