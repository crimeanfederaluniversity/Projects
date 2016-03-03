<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateNewModule.aspx.cs" Inherits="KPIWeb.CreateNewModule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <asp:Button ID="Button2" runat="server" Text="Назад" OnClick="Button2_Click" />
    </p>
    <p>
        <span style="font-size: 20px">Активные модули:</span></p>
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
        <span style="font-size: 20px">Создать новый модуль:</span></p>
    <p>
        Название для пользователей (название кнопки):<asp:TextBox ID="ModuleName" CssClass="form-control" runat="server" Height="40px" Width="400px"></asp:TextBox>
         </p>
    <p>
         Cсылка на модуль:<asp:TextBox ID="ModuleLink" CssClass="form-control" runat="server" Height="40px" Width="400px"></asp:TextBox>
         </p>
    <p>
             <asp:Button ID="Button1" runat="server" CssClass="form-control" OnClientClick="showLoadPanel()" Text="Создать модуль" Height="40px" Width="400px" OnClick="Button1_Click" />
         </p>
</asp:Content>
