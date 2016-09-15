<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation ="false" CodeBehind="HeadAcceptFirst.aspx.cs" Inherits="Rank.Forms.StructUserPointsForAccept" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Рейтинговый балл подчиненного структурного подразделения: <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h3>
    <p>&nbsp;</p>

    <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server" OnRowDataBound ="GridView1_RowDataBound" >
        <Columns>
            <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "false" >
                <ItemTemplate>
                    <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'  Visible="false"></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ФИО сотрудника" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                <ItemTemplate>
                    <asp:Label ID="User" runat="server" Text='<%# Bind("User") %>'  Visible="True"></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Должность" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                <ItemTemplate>
                    <asp:Label ID="Position" runat="server" Text='<%# Bind("Position") %>'  Visible="True"></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Баллы" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                <ItemTemplate>
                    <asp:Label ID="Point" runat="server" Text='<%# Bind("Point") %>'  Visible="True"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
               <asp:TemplateField HeaderText="Статус" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                <ItemTemplate>
                    <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>'  Visible="True"></asp:Label>
                     <asp:Label ID="Color"  runat="server" Visible="false" Text='<%# Bind("Color") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Просмотр и верификация данных работника">
                <ItemTemplate>
                    <asp:Button ID="ShowButton" runat="server" CommandName="Select" Text="Просмотреть и верифицировать данные" Width="300px" CommandArgument='<%# Eval("ID") %>' OnClick="ShowButtonClik" />
                </ItemTemplate>
            </asp:TemplateField>
              
        </Columns>
    </asp:GridView>
</asp:Content>
