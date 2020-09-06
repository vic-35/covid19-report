<%@ Page Title="Ввод ежедневной анкеты сотрудника о здоровье" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Info1.aspx.cs" Inherits="V1_1.Info1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
         <p <asp:Label ID="Label1" runat="server" Text=""></asp:Label></p>
         <p <asp:Label ID="Label2" runat="server" Text=""></asp:Label></p>
           <asp:Label ID="LabelU" runat="server" Text="0" Visible="False"></asp:Label> 


    <div class="form-horizontal">
        <hr />
     
        <asp:Panel ID="Panel1" runat="server">
         <div class="form-group">  
            <asp:Label runat="server" AssociatedControlID="DropDownList1" CssClass="col-md-2 control-label">Оценка состояния</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="name" CssClass="form-control_DD" AutoPostBack="True"  ></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [name] FROM [Spr_info1] ORDER BY [ordr]"></asp:SqlDataSource>
             <br />
           <asp:CheckBox ID="CheckBox1" runat="server" Text="Заболевание не в связи с ОРВИ, простудой ,карантином" Visible="False" />
            </div>
         </div>   
        <br />
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
            </div>
        </div>   
        <br />

        <div class="form-group">

            <asp:Label runat="server" AssociatedControlID="DropDownList2" CssClass="col-md-2 control-label">Имел контакт с зараженным</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control_DD"></asp:DropDownList>
            </div>
        </div>   
                    <br />
        <div class="form-group">

            <asp:Label runat="server" AssociatedControlID="DropDownList3" CssClass="col-md-2 control-label">Состояние здоровья членов семьи - наличие заболевших ОРВИ</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control_DD"></asp:DropDownList>
            </div>
        </div>  
            <br />
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button ID="Button1" runat="server" Text="Сохранить отчет" CssClass="btn btn-default" OnClick="Button1_Click" />    
                <p class="text-danger">
                     <asp:Literal runat="server" ID="ErrorMessage" />
                </p>
            </div>
        </div>

        </asp:Panel>

        <asp:Panel ID="Panel2" runat="server" Visible="False">
            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">      
                    <h4>Отчет принят</h4>
                </div>
             </div>
        </asp:Panel>

        <div class="form-group"> 

                        <asp:Label runat="server" AssociatedControlID="GridView1" CssClass="col-md-2 control-label">Статистика</asp:Label>

            <div class="col-md-offset-2 col-md-10">
 
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT TOP 3 [usr], [diag], [msg],  CONVERT ( char,dt_usr, 103) as dt_usr , CONVERT ( char,dt, 103) as dt,[diag1] FROM [Info1] WHERE ([id_usr] = @id_usr) order by dt_usr desc">
                <SelectParameters>
                    <asp:ControlParameter ControlID="LabelU" Name="id_usr" PropertyName="Text" Type="String" DefaultValue="0" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource2" ForeColor="#333333" GridLines="Horizontal" CellSpacing="10">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="usr" HeaderText="Пользователь" SortExpression="usr" />
                    <asp:BoundField DataField="diag" HeaderText="Диагноз" SortExpression="diag" />
                    <asp:TemplateField HeaderText="Не ОРВИ" SortExpression="diag1">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("diag1") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# P.I.yesno( P.I.to_int(Eval("diag1")))  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Имел контакт с зараженным" SortExpression="msg">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("msg") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("msg") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Дата отчета" SortExpression="dt_usr">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("dt_usr") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
<%--                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("dt_usr", "{0:d}") %>'></asp:Label>--%>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("dt_usr").ToString().Replace("/",".")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>

           </div>


         </div>  



    </div>



</asp:Content>
