<%@ Page Title="Регистрация" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="V1_1.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>Создание новой учетной записи</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />

        <div class="form-group">

            <asp:Label runat="server" AssociatedControlID="User" CssClass="col-md-2 control-label">Номер приглашения</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Ticket" CssClass="form-control"  />
            </div>
        </div> 

        <div class="form-group">

            <asp:Label runat="server" AssociatedControlID="User" CssClass="col-md-2 control-label">ФИО (фамилия и инициалы)</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="User" CssClass="form-control"  />
            </div>
        </div>   
        


         <div class="form-group">   
            <asp:Label runat="server" AssociatedControlID="info1" CssClass="col-md-2 control-label">Отделение</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="info1"  Text="Отделение" CssClass="form-control" ReadOnly="True"  />
            </div>
        </div>    


         <div class="form-group">  
            <asp:Label runat="server" AssociatedControlID="DropDownList1" CssClass="col-md-2 control-label">Отдел</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="id" CssClass="form-control_DD"  ></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [name], [id] FROM [Spr_otdel] ORDER BY [ordr]"></asp:SqlDataSource>
            </div>
         </div>    
     <hr />

    
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Пароль</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                    CssClass="text-danger" ErrorMessage="Поле пароля заполнять обязательно." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Подтверждение пароля</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="Поле подтверждения пароля заполнять обязательно." />
                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="Пароль и его подтверждение не совпадают." />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Регистрация" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>
