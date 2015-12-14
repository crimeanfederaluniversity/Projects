<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateNewModule.aspx.cs" Inherits="KPIWeb.CreateNewModule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <span style="font-size: 30px">Редактирование модулей в системе личных кабинетов:</span></p>
    <p>
        <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server">
             <Columns>                                          
                 <asp:TemplateField HeaderText="ID " HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "False" >
                        <ItemTemplate> 
                            <asp:Label ID="Id" runat="server" Text='<%# Bind("Id") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField>                   
                    <asp:TemplateField HeaderText="Название модуля" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="ProjectName" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("ProjectName") %>'></asp:TextBox>
                        </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField>        
                                                                                    
                 <asp:TemplateField HeaderText="Удалить модуль">
                        <ItemTemplate>
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Select" Text="Удалить" Width="200px" CommandArgument='<%# Eval("Id") %>' OnClick="DeleteButtonClick" />
                        </ItemTemplate>
                    </asp:TemplateField>                                                   
                </Columns>
        </asp:GridView>
         </p>
    <br />
    <p>
        Добавить новый модуль</p>
    <p>
        Название для пользователей (название кнопки):<asp:TextBox ID="ModuleName" CssClass="form-control" runat="server" Height="40px" Width="400px"></asp:TextBox>
         </p>
    <p>
         Cсылка на модуль:<asp:TextBox ID="ModuleLink" CssClass="form-control" runat="server" Height="40px" Width="400px"></asp:TextBox>
         </p>
    <p>
         Выберите кому будет доступен новый модуль:</p>
    <p>
         <asp:CheckBox ID="CheckBox1" runat="server" Text="Студенты" />
    </p>
    <p>
         <asp:CheckBox ID="CheckBox2" runat="server" Text="Сотрудники " />
    </p>
    <p>
             <asp:Button ID="Button1" runat="server" CssClass="form-control" OnClientClick="showLoadPanel()" Text="Создать модуль" Height="40px" Width="400px" OnClick="Button1_Click" />
         </p>
</asp:Content>
