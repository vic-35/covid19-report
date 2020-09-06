<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="V1_1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Отделение</h1>
        <p class="lead">Сайт отчетов</p>
         <p <asp:Label ID="Label1" runat="server" Text=""></asp:Label></p>
         <p <asp:Label ID="Label2" runat="server" Text=""></asp:Label></p>

    </div>
    <asp:Panel ID="Panel2" runat="server">
    <div class="row">
        <div class="col-md-4">
            <h2>Регистрация нового пользователя</h2>
            
            <p>
                <a class="btn btn-default" href="Account/Register">Регистрация &raquo;</a>
            </p>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <h2>Вход зарегистированных пользователей</h2>

            <p>
                <a class="btn btn-default" href="Account/Login">Вход  &raquo;</a>
            </p>
        </div>
    </div>

    </asp:Panel> 
    
    <asp:Panel ID="Panel1" runat="server">
    <div class="row">
        <div class="col-md-4">
            <h2>Ввод ежедневной анкеты сотрудника о здоровье</h2>
            
            <p>
                <a class="btn btn-default" href="info1">Заполнить &raquo;</a>
            </p>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <h2>Ввод ежедневной анкеты сотрудника об удаленной работе</h2>

            <p>
                <a class="btn btn-default" href="info2">Заполнить &raquo;</a>
            </p>
        </div>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Административные отчеты </h2>

            <p>
                <a class="btn btn-default" href="Contact">Перейти &raquo;</a>
            </p>
        </div>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Поддержка ОИ</h2>
            <p>
                <a class="btn btn-default" href="about">Перейти &raquo;</a>
            </p>
        </div>
    </div>

    </asp:Panel>




</asp:Content>
