<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangeUserAccessLevel.aspx.cs" Inherits="KPIWeb.PersonalPagesAdmin.ChangeUserAccessLevel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <span style="font-size: 20px">Редактирование прав доступа пользователя:</span><br />
    <br />Выберите необходимые модули для пользователя:<br />
    <div>
       <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server"  >           
             <Columns>                                         
                 <asp:TemplateField HeaderText="ID модуля" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="UsersTableId" runat="server" Text='<%# Bind("UsersTableId") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField>    
                 <asp:TemplateField HeaderText="Название" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="Name" runat="server" Text='<%# Bind("Name") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 
                 <asp:TemplateField HeaderText="Права доступа" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:CheckBox ID="CheckedBox" runat="server"  Visible="True" Text=" " Checked='<%# Bind("CheckedBox") %>'></asp:CheckBox>
                        </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField>                                             
                </Columns>
       </asp:GridView>
    </div>
&nbsp;<br />
    <asp:Button ID="Button1" runat="server" CssClass="form-control" OnClientClick="showLoadPanel()" Text="Сохранить" Height="40px" Width="400px" OnClick="Button1_Click" />
</asp:Content>
