<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreatePersonalPage.aspx.cs" Inherits="KPIWeb.PersonalPagesAdmin.CreatePersonalPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Назад" />
        <span style="font-size: 20px">
        <br />
        <br />
        Регистрация нового пользователя в системе:</span><br />
        Выберите статус пользователя<br />
        <asp:DropDownList ID="DropDownList4" runat="server" Height="40px" Width="400px" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged">
            <asp:ListItem Value="0">Сотрудник КФУ</asp:ListItem>
            <asp:ListItem Value="11" Selected="True">Студент бакалавриата</asp:ListItem>
            <asp:ListItem Value="12">Студент специалитета</asp:ListItem>
            <asp:ListItem Value="13">Студент магистратуры</asp:ListItem>
            <asp:ListItem Value="14">Студент аспирантуры</asp:ListItem>
        </asp:DropDownList>
        <br />
        Фамилия<asp:TextBox ID="Surname" CssClass="form-control" runat="server" Height="40px" Width="400px"></asp:TextBox>
         Имя<asp:TextBox ID="Name" CssClass="form-control" runat="server" Height="40px" Width="400px"></asp:TextBox>
         Отчество<asp:TextBox ID="Patronimyc" CssClass="form-control" runat="server" Height="40px" Width="400px"></asp:TextBox>
        Адрес электронной почты<asp:TextBox ID="EmailText" CssClass="form-control" runat="server" Height="40px" Width="400px"></asp:TextBox>
        <div>
            <asp:Label ID="Label4" runat="server" Text="Выберите университет"></asp:Label>
            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" CssClass="form-control" Height="40px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="400px">
            </asp:DropDownList>
            <br />
        </div>
        <div>
            <asp:Label ID="Label2" runat="server" Text="Выберите факультет"></asp:Label>
            <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" CssClass="form-control" Height="40px" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" Width="400px">
            </asp:DropDownList>
            <br />
        </div>
        <div>
            <asp:Label ID="Label3" runat="server" Text="Выберите кафедру"></asp:Label>
            <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" CssClass="form-control" Height="40px" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" Width="400px">
            </asp:DropDownList>
            <br />
            <br />
            <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
         <asp:TextBox ID="Textbox1" CssClass="form-control" runat="server" Height="40px" Width="400px"></asp:TextBox>
            <br />
            <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
         <asp:TextBox ID="Textbox2" CssClass="form-control" runat="server" Height="40px" Width="400px"></asp:TextBox>
        </div>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Выберите необходимые модули для пользователя:"></asp:Label>
        <br />
        <div>
       </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Название группы">
                    <ItemTemplate>
                        <asp:Label ID="Name0" runat="server" Text='<%# Bind("Name") %>' Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Доступ к системам">
                    <ItemTemplate>
                        <asp:CheckBox ID="Access" runat="server"   Text=" " />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        
        <br />
             <asp:CheckBox ID="CheckBox1" runat="server" Checked="True"   Text="Отправить email?" />
             <br />
             <br />
             <asp:Button ID="Button1" runat="server" CssClass="form-control" OnClientClick="showLoadPanel()" Text="Создать пользователя" Height="40px" Width="400px" OnClick="Button1_Click" />
         </div>
&nbsp;
</asp:Content>
