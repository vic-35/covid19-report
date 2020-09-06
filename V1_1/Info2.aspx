<%@ Page Title="Ввод ежедневной анкеты сотрудника об удаленной работе" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Info2.aspx.cs" Inherits="V1_1.Info2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
         <p <asp:Label ID="Label1" runat="server" Text=""></asp:Label></p>
         <p <asp:Label ID="Label2" runat="server" Text=""></asp:Label></p>
           <asp:Label ID="LabelU" runat="server" Text="0" Visible="False"></asp:Label> 


    <div class="form-horizontal">
          <hr />
     
     <asp:Panel ID="Panel1" runat="server">
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
            <asp:Label runat="server" AssociatedControlID="DropDownList4" CssClass="col-md-2 control-label">Работали очно в офисе 8 часов</asp:Label>
            <div class="col-md-10">

                <asp:DropDownList ID="DropDownList4" runat="server"  CssClass="form-control_DD"  AutoPostBack="True"></asp:DropDownList>
                

            </div>
         </div>
                 <br />

         <asp:Panel ID="Panel4" runat="server">
        <div class="form-group" >  
            <asp:Label runat="server" AssociatedControlID="DropDownList1" CssClass="col-md-2 control-label">Привлекались к работе в офисе</asp:Label>
            <div class="col-md-10">

                <asp:DropDownList ID="DropDownList1" runat="server" DataTextField="name" DataValueField="name" CssClass="form-control_DD"  ></asp:DropDownList>
                        <br />


            </div>
         </div>   
            <div class="form-group">  
                <asp:Label runat="server" AssociatedControlID="DropDownList4" CssClass="col-md-2 control-label">Выполняли задачи удаленной работы</asp:Label>
                <div class="col-md-10">

                    <asp:DropDownList ID="DropDownList5" runat="server"  CssClass="form-control_DD"  AutoPostBack="True"></asp:DropDownList>
                

                </div>
             </div>
            </asp:Panel>
                 <br />

         <asp:Panel ID="Panel3" runat="server">
         
         <div class="form-group">  
            <asp:Label runat="server" AssociatedControlID="DropDownList2" CssClass="col-md-2 control-label" Visible="False">Укажите количество Ваших выходов в офис в отчетном периоде?</asp:Label>
            <div class="col-md-10">

                <asp:DropDownList ID="DropDownList2" runat="server" DataTextField="name" DataValueField="name" CssClass="form-control_DD" Visible="False" ></asp:DropDownList>
             <asp:Label runat="server" Visible="False">Если на предыдущий вопрос Вы ответили "Да" отразите конкретное количество выходов в офис.</asp:Label>
               

            </div>
         </div>  


        <div class="form-group">  
            <asp:Label runat="server" AssociatedControlID="CheckBox1" CssClass="col-md-2 control-label">Виды работ</asp:Label>
            <div class="col-md-10">
                <asp:CheckBox ID="CheckBox1" runat="server" Text="Прошел(прошла) дистанционный курс" AutoPostBack="True"/><br />
                 <asp:TextBox runat="server" ID="CheckBox1_txt" CssClass="form-control"  MaxLength="124" Visible="False"/><br />  
                <asp:CheckBox ID="CheckBox2" runat="server" Text="Принял(а) участие в вебинаре" AutoPostBack="True" /><br />
                <asp:TextBox runat="server" ID="CheckBox2_txt" CssClass="form-control"  MaxLength="124" Visible="False" /><br />
                <asp:CheckBox ID="CheckBox3" runat="server" Text="Изучил(а) нормативные акты по профессии" AutoPostBack="True"/><br />
                <asp:TextBox runat="server" ID="CheckBox3_txt" CssClass="form-control"  MaxLength="124" Visible="False" /><br />
                <asp:CheckBox ID="CheckBox4" runat="server" Text="Проводил(а)сбор и анализ данных" AutoPostBack="True" /><br />
                <asp:TextBox runat="server" ID="CheckBox4_txt" CssClass="form-control"  MaxLength="124" Visible="False" /><br />
                <asp:CheckBox ID="CheckBox5" runat="server" Text="Мониторинг лучших практик" AutoPostBack="True"/><br />
                <asp:TextBox runat="server" ID="CheckBox5_txt" CssClass="form-control"  MaxLength="124" Visible="False" /><br />
                <asp:CheckBox ID="CheckBox6" runat="server" Text="Работы по заданию руководства" AutoPostBack="True"/><br />
                <asp:TextBox runat="server" ID="CheckBox6_txt" CssClass="form-control"  MaxLength="124" Visible="False" /><br />
                <asp:CheckBox ID="CheckBox7" runat="server" Text="Работал в удаленной команде по направлению" AutoPostBack="True"/><br />
                <asp:TextBox runat="server" ID="CheckBox7_txt" CssClass="form-control"  MaxLength="124" Visible="False" /><br />
                <asp:CheckBox ID="CheckBox8" runat="server" Text="Организовывал работу удаленных групп/команд" AutoPostBack="True"/><br />
                <asp:TextBox runat="server" ID="CheckBox8_txt" CssClass="form-control"  MaxLength="124" Visible="False" /><br />
                <asp:CheckBox ID="CheckBox9" runat="server" Text="Выполнение задач в рамках ф-й подразделений" AutoPostBack="True"/><br />
                <asp:TextBox runat="server" ID="CheckBox9_txt" CssClass="form-control"  MaxLength="124" Visible="False" /><br />
                <asp:CheckBox ID="CheckBox10" runat="server" Text="Мероприятия относящиеся к саморазвитию" AutoPostBack="True"/><br />
                <asp:TextBox runat="server" ID="CheckBox10_txt" CssClass="form-control" MaxLength="124" Visible="False" /><br />


            </div>
        </div>   

         <div class="form-group">  
            <asp:Label runat="server" AssociatedControlID="DropDownList3" CssClass="col-md-2 control-label" Visible="False">Укажите сведения о Вашей трудоспособности</asp:Label>
            <div class="col-md-10">

                <asp:DropDownList ID="DropDownList3" runat="server" DataTextField="name" DataValueField="name" CssClass="form-control_DD" Visible="False" ></asp:DropDownList>
               <asp:Label runat="server" Visible="False">Укажите сведения о Вашей трудоспособности
Если Вы нетрудоспособны по любой причине (собственная болезнь, плохое самочувствие, уход за членом семьи, нахождение на карантине, уход за ребенком по причине карантина школ и детских садов и т.д. Неважно оформлена ли временная нетрудоспособность или нет - указывается "Нетрудоспособен")</asp:Label>
  

            </div>
         </div>   

        <div class="form-group">

            <asp:Label runat="server" AssociatedControlID="Msg" CssClass="col-md-2 control-label">Дополнительная информация (при необходимости)</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Msg" CssClass="form-control"  MaxLength="124" />
            </div>
        </div> 
            
 </asp:Panel>

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
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT  TOP 3 [usr],  CONVERT ( char,dt_usr, 103) as dt_usr, [p5] FROM [Info2] WHERE ([id_usr] = @id_usr) ORDER BY [dt_usr] DESC">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="LabelU" DefaultValue="0" Name="id_usr" PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" CellSpacing="4" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="usr" HeaderText="Пользователь" SortExpression="usr" />
                        <asp:TemplateField HeaderText="Дата отчета" SortExpression="dt_usr">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("dt_usr") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("dt_usr").ToString().Replace("/",".") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Работал удаленно" SortExpression="p5">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("p5") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# P.I.yesno( P.I.to_int(Eval("p5"))) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
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

                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Скачать полный отчет" />
                <br />

           </div>


         </div>  



    </div>


</asp:Content>
