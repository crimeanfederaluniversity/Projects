<%@ Page Title="Резистрация нового пользователя" Language="C#" EnableViewStateMac="false" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="KPIWeb.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <h4>Создание нового аккаунта.</h4>
    <asp:ValidationSummary runat="server" CssClass="text-danger" />
    </br>
    </br>
    </br>
    </br>
    </br>
    </br>
    </br>
    </br>
    </br>
    </br>
        <div>
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Выберите университет</asp:Label>
            </br>
            <div class="col-md-10">
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>
     </br>
     </br>
        <div>
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Выберите факультет</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>
     </br>
     </br>
        <div>
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Выберите кафедру</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="DropDownList3" CssClass="form-control" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </div>
        </div>
     </br>
     </br>
        <div>
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Тип пользователя</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="DropDownList5" runat="server" CssClass="form-control" OnSelectedIndexChanged="DropDownList5_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem Value="-1">Выберите статус</asp:ListItem>
                    <asp:ListItem Value="0">Исполнитель</asp:ListItem>
                    <asp:ListItem Value="5">Руководство</asp:ListItem>
                    <asp:ListItem Value="9">Администратор</asp:ListItem>
                    
                </asp:DropDownList>
            </div>
        </div>
    </br>
    </br>
        <div>
            <asp:Label ID="Label24" runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Шаблон</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="DropDownList4" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>
    </br>
        <div>
            <asp:Label ID="Label25" runat="server" Text="Индикаторы"></asp:Label>
            <asp:GridView ID="Gridview1" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="Название">
                        <ItemTemplate>
                            <asp:Label ID="IndicatorID" runat="server" Visible="False" Text='<%# Bind("IndicatorID") %>'></asp:Label>
                            <asp:Label ID="IndicatorName" runat="server" Visible="True" Text='<%# Bind("IndicatorName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Редак">
                        <ItemTemplate>
                            <asp:CheckBox ID="IndicatorEditCheckBox" runat="server" Checked='<%# Bind("IndicatorEditCheckBox") %>'></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Просм">
                        <ItemTemplate>
                            <asp:CheckBox ID="IndicatorViewCheckBox" runat="server" Checked='<%# Bind("IndicatorViewCheckBox") %>'></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Подтв">
                        <ItemTemplate>
                            <asp:CheckBox ID="IndicatorConfirmCheckBox" runat="server" Checked='<%# Bind("IndicatorConfirmCheckBox") %>'></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </br>
    
     <div>
         <asp:Label ID="Label26" runat="server" Text="Рассчетные показатели"></asp:Label>
         <asp:GridView ID="Gridview2" runat="server" AutoGenerateColumns="False">
             <Columns>
                 <asp:TemplateField HeaderText="Название">
                     <ItemTemplate>
                         <asp:Label ID="CalculatedParametrsID" runat="server" Visible="False" Text='<%# Bind("CalculatedParametrsID") %>'></asp:Label>
                         <asp:Label ID="CalculatedParametrsName" runat="server" Visible="True" Text='<%# Bind("CalculatedParametrsName") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Редак">
                     <ItemTemplate>
                         <asp:CheckBox ID="CalculatedParametrsEditCheckBox" runat="server" Checked='<%# Bind("CalculatedParametrsEditCheckBox") %>'></asp:CheckBox>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Просм">
                     <ItemTemplate>
                         <asp:CheckBox ID="CalculatedParametrsViewCheckBox" runat="server" Checked='<%# Bind("CalculatedParametrsViewCheckBox") %>'></asp:CheckBox>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Подтв">
                     <ItemTemplate>
                         <asp:CheckBox ID="CalculatedParametrsConfirmCheckBox" runat="server" Checked='<%# Bind("CalculatedParametrsConfirmCheckBox") %>'></asp:CheckBox>
                     </ItemTemplate>
                 </asp:TemplateField>
             </Columns>
         </asp:GridView>
     </div>
    </br>
   
    <div>
        <asp:Label ID="Label27" runat="server" Text="Базовые показатели"></asp:Label>
        <asp:GridView ID="Gridview3" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="Gridview3_SelectedIndexChanged">
            <Columns>
                <asp:TemplateField HeaderText="Название">
                    <ItemTemplate>
                        <asp:Label ID="BasicParametrsID" runat="server" Visible="False" Text='<%# Bind("BasicParametrsID") %>'></asp:Label>
                        <asp:Label ID="BasicParametrsName" runat="server" Visible="True" Text='<%# Bind("BasicParametrsName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Редак">
                    <ItemTemplate>
                        <asp:CheckBox ID="BasicParametrsEditCheckBox" runat="server" Checked='<%# Bind("BasicParametrsEditCheckBox") %>'></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Просм">
                    <ItemTemplate>
                        <asp:CheckBox ID="BasicParametrsViewCheckBox" runat="server" Checked='<%# Bind("BasicParametrsViewCheckBox") %>'></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Подтв">
                    <ItemTemplate>
                        <asp:CheckBox ID="BasicParametrsConfirmCheckBox" runat="server" Checked='<%# Bind("BasicParametrsConfirmCheckBox") %>'></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </br>
    </br>
    </br>
    </br>
    </br>
    </br>
    </br>
    </br>
    </br>
    <div>
        <asp:Label runat="server" AssociatedControlID="UserName" CssClass="col-md-2 control-label">Имя пользователя</asp:Label>
        <div class="col-md-10">
            <asp:TextBox runat="server" ID="UserName" CssClass="form-control" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                CssClass="text-danger" ErrorMessage="Введите имя пользователя." />
        </div>
    </div>

    <div>
        <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Почтовый адрес</asp:Label>
        <div class="col-md-10">
            <asp:TextBox runat="server" ID="Email" CssClass="form-control" />
            <asp:RegularExpressionValidator ID="valid_email" runat="server"
                ErrorMessage="Неверный почтовый адрес" CssClass="text-danger"
                ControlToValidate="Email"
                ValidationExpression="^([A-Za-z0-9_-]+\.)*[A-Za-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$" />

        </div>
    </div>
    <div>
        <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Пароль</asp:Label>
        <div class="col-md-10">
            <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                CssClass="text-danger" ErrorMessage="Введите пароль." />
        </div>
    </div>
    <div>
        <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Подтвердите пароль</asp:Label>

        <div class="col-md-10">
            <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
            <asp:RequiredFieldValidator runat="server"
                ControlToValidate="ConfirmPassword"
                CssClass="text-danger" Display="Dynamic" ErrorMessage="Введите подтверждение пароля." />
            <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                CssClass="text-danger" Display="Dynamic" ErrorMessage="Пароли не совпадают." />
        </div>
    </div>

    <div>
        <div class="col-md-offset-2 col-md-10">
            <br />
            <asp:Button runat="server" OnClick="CreateUser_Click" Text="Зарегистрировать" CssClass="btn btn-default" />
            <br />
            <br />
        </div>
    </div>

</asp:Content>