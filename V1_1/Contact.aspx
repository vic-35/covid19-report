<%@ Page Title="Отчеты" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="V1_1.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <h2><%: Title %>.</h2>
         <p <asp:Label ID="Label1" runat="server" Text=""></asp:Label></p>
         <p <asp:Label ID="Label2" runat="server" Text=""></asp:Label></p>
           <asp:Label ID="LabelU" runat="server" Text="0" Visible="False"></asp:Label> 

    <div class="form-horizontal">

     
        <asp:Panel ID="Panel1" runat="server">
            <h4>Доступ запрещен</h4>
        </asp:Panel>

        <asp:Panel ID="Panel2" runat="server" Visible="False">
            <div class="form-group">
                
                <asp:Label runat="server" AssociatedControlID="Calendar1" CssClass="col-md-2 control-label">Дата отчета</asp:Label>
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
                    <asp:Label runat="server" AssociatedControlID="DropDownList1" CssClass="col-md-2 control-label">Отдел</asp:Label>
                    <div class="col-md-10">
                        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="id" CssClass="form-control_DD"  ></asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [name], [id] FROM [Spr_otdel] ORDER BY [ordr]"></asp:SqlDataSource>
                        <br />

                    </div>
                </div>   
                <h4>Отчет о здоровье сотрудников за дату</h4>
                <div class="form-group">  
                    <asp:Label runat="server" AssociatedControlID="Button1" CssClass="col-md-2 control-label"></asp:Label>
                    <div class="col-md-10">
                        
                        

                        <table style=" text-align: left;">
                            <tr>
                                <td><br /><asp:Button ID="Button1" runat="server" Text="Отчет MicrosoftOffice" OnClick="Button1_Click"  Width="250px"/></td>
                                <td> &nbsp;</td>
                                <td><br /><asp:Button ID="Button9" runat="server" OnClick="Button9_Click" Text="Отчет HTML"  Width="250px"/></td>
                            </tr>                          
                        </table>

                        <asp:Panel ID="Panel4" runat="server" Visible="False">

                            <table style=" text-align: left;">
                                <tr>
                                    <td><br /><asp:Button ID="Button7" runat="server" Text="Все Отделение Отчет MicrosoftOffice" OnClick="Button7_Click"  Width="250px"/></td>
                                    <td> &nbsp;</td>
                                    <td><br /><asp:Button ID="Button10" runat="server" OnClick="Button10_Click" Text="Все Отделение Отчет  HTML"  Width="250px"/></td>
                                </tr>                          
                            </table>
                            

                        </asp:Panel>   
 
                    </div>
                </div>



  
                
            <h4>Отчет об удаленной работе сотрудников за дату</h4>

            <div class="form-group">  
                    <asp:Label runat="server" AssociatedControlID="Button2" CssClass="col-md-2 control-label"></asp:Label>
                    <div class="col-md-10">
                        <table style=" text-align: left;">
                            <tr>
                                <td><br /><asp:Button ID="Button2" runat="server" Text="Отчет MicrosoftOffice" OnClick="Button2_Click" Width="250px"/></td>
                                <td> &nbsp;</td>
                                <td><br /><asp:Button ID="Button12" runat="server" OnClick="Button12_Click" Text="Отчет HTML"  Width="250px"/></td>
                            </tr>
                            <tr>
                                <td><br /><asp:Button ID="Button11" runat="server" OnClick="Button11_Click" Text="Отчет HTML для смартфона"  Width="250px"/></td>
                                <td> &nbsp; </td>
                                <td><br /><asp:Button ID="Button14" runat="server" OnClick="Button14_Click" Text="Курсы/вебинары"  Width="250px"/></td>
                            </tr>                            
                             <tr>
                                <td><br /><asp:Button ID="Button4" runat="server"  Text="Отчет о дистанционной работе"  Width="250px" OnClick="Button4_Click1"/></td>
                                <td> &nbsp; </td>
                                <td></td>
                            </tr> 

                        </table>

                       <asp:Panel ID="Panel3" runat="server" Visible="False">
                           
                             <table style=" text-align: left;">
                                <tr>
                                    <td><br /><asp:Button ID="Button5" runat="server" Text="Отделение Отчет MicrosoftOffice" OnClick="Button5_Click"  Width="250px"/></td>
                                   <td>
                                       &nbsp;
                                    </td>
                                    <td>
                                           <br /><asp:Button ID="Button15" runat="server" OnClick="Button15_Click" Text="Отделение курсы/вебинары"  Width="250px"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                         <br /><asp:Button ID="Button13" runat="server" OnClick="Button13_Click" Text="Отделение Отчет  HTML"  Width="250px"/>
                                    </td>
                                     <td>
                                         &nbsp;
                                    </td>
                                    <td>
                                            <br /><asp:Button ID="Button3" runat="server"   Text="Сводный отчет за период"  Width="250px" OnClick="Button3_Click1" />
                                    </td>
                                </tr>
                            </table>

                            </asp:Panel>   
                    </div>
            </div>   

 



        </asp:Panel>




    </div>



</asp:Content>
