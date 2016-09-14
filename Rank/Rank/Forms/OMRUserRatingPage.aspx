<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OMRUserRatingPage.aspx.cs" Inherits="Rank.Forms.OMRUserRatingPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <br />
        <asp:Button ID="Button2" runat="server" Text="Назад" OnClick="Button2_Click" />
    
    <h3>Рейтинг сотрудников КФУ</h3>
    <span style="font-size: medium"><br />
        <span>По структурному подразделению:&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        </span>
        <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" Height="25px" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" Width="300px">
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" Height="25px" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged" Width="300px">
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="True" Height="25px" Width="300px">
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;<br />
        <br />
        <asp:DropDownList ID="DropDownList6" runat="server" AutoPostBack="true" Height="25px" Width="300px">
            <asp:ListItem Selected="True" Value="0">Выберите вид работника</asp:ListItem>
            <asp:ListItem Value="1">Преподаватели (ППС)</asp:ListItem>
            <asp:ListItem Value="2">Научники (НР)</asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;
        <asp:DropDownList ID="PPS" runat="server" Height="25px" Visible="False" Width="300px">
            <asp:ListItem Selected="True" Value="0">Выберите должность</asp:ListItem>
            <asp:ListItem Value="1">Доцент</asp:ListItem>
            <asp:ListItem Value="2">Профессор</asp:ListItem>
            <asp:ListItem Value="3">Преподаватель</asp:ListItem>
            <asp:ListItem Value="4">Старший преподаватель</asp:ListItem>
            <asp:ListItem Value="5">Ассистент</asp:ListItem>
            <asp:ListItem Value="6">Заведующий кафедрой</asp:ListItem>
            <asp:ListItem Value="7">Декан факультета</asp:ListItem>
            <asp:ListItem Value="8">Директор института</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="NR" runat="server" Height="25px" Visible="False" Width="300px">
            <asp:ListItem Selected="True" Value="0">Выберите должность</asp:ListItem>
            <asp:ListItem Value="1">Научный сотрудник</asp:ListItem>
            <asp:ListItem Value="2">Младший научный сотрудник</asp:ListItem>
            <asp:ListItem Value="3">Старший научный сотрудник</asp:ListItem>
            <asp:ListItem Value="4">Ведущий научный сотрудник</asp:ListItem>
            <asp:ListItem Value="5">Главный научный сотрудник</asp:ListItem>
            <asp:ListItem Value="6">Заведующий (начальник) отдела</asp:ListItem>
            <asp:ListItem Value="7">Заведующий (начальник) лаборатории</asp:ListItem>
            <asp:ListItem Value="8">Заведующий (начальник) центра</asp:ListItem>
            <asp:ListItem Value="9">Ученый секретарь</asp:ListItem>
            <asp:ListItem Value="1">Директор подразделения</asp:ListItem>
        </asp:DropDownList>
        <span>&nbsp;
        <asp:Button ID="Button1" runat="server" Height="25px" OnClick="Button1_Click" Text="Поиск" Width="100px" />
        </span>
        <br />
        <span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</span><br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="" Visible="false" />
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Код автора" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="userid" runat="server" Text='<%# Bind("userid") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Академия" Visible="True">
                    <ItemTemplate>
                        <asp:Label ID="firstlvl" runat="server" Text='<%# Bind("firstlvl") %>' Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Институт/факультет" Visible="True">
                    <ItemTemplate>
                        <asp:Label ID="secondlvl" runat="server" Text='<%# Bind("secondlvl") %>' Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Кафедра" Visible="True">
                    <ItemTemplate>
                        <asp:Label ID="thirdlvl" runat="server" Text='<%# Bind("thirdlvl") %>' Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Фамилия Имя Отчество" Visible="True">
                    <ItemTemplate>
                        <asp:Label ID="fio" runat="server" Text='<%# Bind("fio") %>' Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Должность" Visible="True">
                    <ItemTemplate>
                        <asp:Label ID="position" runat="server" Text='<%# Bind("position") %>' Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Рейтинг" Visible="True">
                    <ItemTemplate>
                        <asp:Label ID="point" runat="server" Text='<%# Bind("point") %>' Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
         </asp:Content>
