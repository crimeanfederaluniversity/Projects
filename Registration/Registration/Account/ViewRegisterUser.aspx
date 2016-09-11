<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewRegisterUser.aspx.cs" Inherits="Registration.Account.ViewRegisterUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <h3>Одобренные заявки на регистрацию в системе &quot;Рейтинги научно-педагогических работников КФУ&quot;. </h3>
    <br />
    <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server"   >
        <Columns>    
            <asp:TemplateField HeaderText="Код автора" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "false" >
                <ItemTemplate>
                    <asp:Label ID="userid" runat="server" Text='<%# Bind("userid") %>'  Visible="false"></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="ФИО" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                <ItemTemplate>
                    <asp:Label ID="fio" runat="server" Text='<%# Bind("fio") %>'  Visible="True"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>  
                 <asp:TemplateField HeaderText="Email" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                <ItemTemplate>
                    <asp:Label ID="email" runat="server" Text='<%# Bind("email") %>'  Visible="True"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>   
            <asp:TemplateField HeaderText="Структурное подразделение/филиал" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                <ItemTemplate>
                    <asp:Label ID="firstlvl" runat="server" Text='<%# Bind("firstlvl") %>'  Visible="True"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Институт/факультет" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                <ItemTemplate>
                    <asp:Label ID="secondlvl" runat="server" Text='<%# Bind("secondlvl") %>'  Visible="True"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Кафедра" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                <ItemTemplate>
                    <asp:Label ID="thirdlvl" runat="server" Text='<%# Bind("thirdlvl") %>'  Visible="True"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Должность" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                <ItemTemplate>
                    <asp:Label ID="position" runat="server" Text='<%# Bind("position") %>'  Visible="True"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Ставка" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                <ItemTemplate>
                    <asp:Label ID="stavka" runat="server" Text='<%# Bind("stavka") %>'  Visible="True"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Ученая степень" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                <ItemTemplate>
                    <asp:Label ID="degree" runat="server" Text='<%# Bind("degree") %>'  Visible="True"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>   
        </Columns>
    </asp:GridView>
</asp:Content>
