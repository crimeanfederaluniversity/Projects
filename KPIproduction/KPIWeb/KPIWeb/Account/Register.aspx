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
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"> </asp:DropDownList>
            </div>
        </div>
         <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Выберите факультет</asp:Label>
            <div class="col-md-10">               
                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"> </asp:DropDownList>
            </div>
        </div>
         <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Выберите кафедру</asp:Label>
            <div class="col-md-10">               
                <asp:DropDownList ID="DropDownList3" CssClass="form-control" runat="server"  AutoPostBack="True"> </asp:DropDownList>
            </div>
        </div>

         <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Выберите роли</asp:Label>
            <div class="col-md-10">               
                <asp:CheckBoxList ID="CheckBoxList1" CssClass="form-control" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged" >
                </asp:CheckBoxList>
                <br />
</div>  
                 <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Выберите шаблон</asp:Label>
            <div class="col-md-10">               
                <asp:DropDownList ID="DropDownList4" CssClass="form-control" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged"> </asp:DropDownList>
            </div>
        </div>
               <asp:GridView ID="GridviewRoles" runat="server" ShowFooter="true" AutoGenerateColumns="false">
                            <Columns>

                                <asp:TemplateField HeaderText="Ред|Просм">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelRolesTableID" runat="server" Visible="false" Text='<%# Bind("Name") %>'></asp:Label>
                                        <asp:CheckBox ID="CheckBoxCanEdit" runat="server" Checked='<%# Bind("EditChecked") %>'></asp:CheckBox>
                                        <asp:CheckBox ID="CheckBoxCanView" runat="server" Checked='<%# Bind("ViewChecked") %>'></asp:CheckBox>
                                        <asp:CheckBox ID="CheckBoxVerify" runat="server"  Checked='<%# Bind("VerifyChecked") %>'></asp:CheckBox>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Name" HeaderText="Роль" />

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
    </div>
    </div>
</asp:Content>
