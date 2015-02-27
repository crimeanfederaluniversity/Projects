<%@ Page Title="Резистрация нового пользователя" Language="C#" EnableViewStateMac="false" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="KPIWeb.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>Создание нового аккаунта.</h4>
        <hr />

        <asp:ValidationSummary runat="server" CssClass="text-danger" />



        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Выберите университет</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Выберите факультет</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Выберите кафедру</asp:Label>
            <div class="col-md-10">
                <br />
                <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </div>
        </div>
        
             <div class="form-group">
                 Пользователь руководства<br />
             <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" />      
                 <br />
            </div>
        
              

        <div class="form-group">
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Выберите шаблон</asp:Label>
                <div class="col-md-10">
                    <asp:DropDownList ID="DropDownList4" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
            <asp:GridView ID="GridviewRoles" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="Название">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Visible="False" Text='<%# Bind("BasicId") %>'></asp:Label>
                            <asp:Label ID="Label1" runat="server" Visible="True" Text='<%# Bind("Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Редак">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBoxCanEdit" runat="server" Checked='<%# Bind("EditChecked") %>'></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Просм">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBoxCanView" runat="server" Checked='<%# Bind("ViewChecked") %>'></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Подтв">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBoxVerify" runat="server" Checked='<%# Bind("VerifyChecked") %>'></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            
            
       </div>
    </div>

    <div class="form-group">
        <asp:Label runat="server" AssociatedControlID="UserName" CssClass="col-md-2 control-label">Имя пользователя</asp:Label>
        <div class="col-md-10">
            <asp:TextBox runat="server" ID="UserName" CssClass="form-control" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                CssClass="text-danger" ErrorMessage="Введите имя пользователя." />
        </div>
    </div>

    <div class="form-group">
        <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Почтовый адрес</asp:Label>
        <div class="col-md-10">
            <asp:TextBox runat="server" ID="Email" CssClass="form-control" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                CssClass="text-danger" ErrorMessage="Введите почтовый адрес." />
        </div>
    </div>
    <div class="form-group">
        <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Пароль</asp:Label>
        <div class="col-md-10">
            <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                CssClass="text-danger" ErrorMessage="Введите пароль." />
        </div>
    </div>
    <div class="form-group">
        <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Подтвердите пароль</asp:Label>

        <div class="col-md-10">
            <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                CssClass="text-danger" Display="Dynamic" ErrorMessage="Введите подтверждение пароля." />
            <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                CssClass="text-danger" Display="Dynamic" ErrorMessage="Пароли не совпадают." />
        </div>
    </div>



    <div class="form-group">
        <div class="col-md-offset-2 col-md-10">
            <asp:Button runat="server" OnClick="CreateUser_Click" Text="Зарегистрировать" CssClass="btn btn-default" />
        </div>
    </div>

</asp:Content>
