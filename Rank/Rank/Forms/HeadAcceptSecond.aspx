<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation ="false" CodeBehind="HeadAcceptSecond.aspx.cs" Inherits="Rank.Forms.StructPointsForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3> <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label> </h3>
    <h4>Рейтинговый балл работника за 2016 год:<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>  </h4>
    
    <br />
    Просмотр и верификация данных работника:<asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server" OnRowDataBound ="GridView1_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "false" >
                <ItemTemplate>
                    <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'  Visible="false"></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
            </asp:TemplateField>
               <asp:TemplateField HeaderText="№ показателя" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                <ItemTemplate>
                    <asp:Label ID="Number" runat="server" Text='<%# Bind("Number") %>'  Visible="True"></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Название показателя" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                <ItemTemplate>
                    <asp:Label ID="Parametr" runat="server" Text='<%# Bind("Parametr") %>'  Visible="True"></asp:Label>
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
            <asp:TemplateField HeaderText="Перейти">
                <ItemTemplate>
                    <asp:Button ID="ShowButton" runat="server" CommandName="Select" Text="Верифицировать данные" Width="200px" CommandArgument='<%# Eval("ID") %>' OnClick="ShowButtonClik" />
                </ItemTemplate>
            </asp:TemplateField>
 
        </Columns>
    </asp:GridView>
        <br />
    Просмотр данных работника:<asp:GridView ID="GridView2" AutoGenerateColumns="false" runat="server"  >
        <Columns>
            <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "false" >
                <ItemTemplate>
                    <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'  Visible="false"></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
            </asp:TemplateField>
               <asp:TemplateField HeaderText="№ показателя" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                <ItemTemplate>
                    <asp:Label ID="Number" runat="server" Text='<%# Bind("Number") %>'  Visible="True"></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Название показателя" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                <ItemTemplate>
                    <asp:Label ID="Parametr" runat="server" Text='<%# Bind("Parametr") %>'  Visible="True"></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>-
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Баллы" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                <ItemTemplate>
                    <asp:Label ID="Point" runat="server" Text='<%# Bind("Point") %>'  Visible="True"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
 
            <asp:TemplateField HeaderText="Перейти">
                <ItemTemplate>
                    <asp:Button ID="ShowButton" runat="server" CommandName="Select" Text="Просмотреть данные" Width="200px" CommandArgument='<%# Eval("ID") %>' OnClick="ShowButtonClik" />
                </ItemTemplate>
            </asp:TemplateField>
 
        </Columns>
    </asp:GridView>
</asp:Content>
