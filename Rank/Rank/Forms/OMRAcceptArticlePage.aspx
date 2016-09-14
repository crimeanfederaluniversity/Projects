<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OMRAcceptArticlePage.aspx.cs" Inherits="Rank.Forms.OMRAcceptArticlePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Button ID="Button2" runat="server" Text="Назад" OnClick="Button2_Click" />
    <h3>Верифицировать данные сотрудников КФУ:</h3>
    <span>Выберите структурное подразделение:<br />
    </span>
    <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" Height="25px" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" Width="300px">
    </asp:DropDownList>
    &nbsp;
    <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" Height="25px" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged" Width="300px">
    </asp:DropDownList>
    &nbsp;
    <asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="True" Height="25px" Width="300px">
    </asp:DropDownList>
    <br />
    <br />
    &nbsp;&nbsp;<asp:CheckBox ID="CheckBox1" runat="server" Text="Только неверифицированные данные" />
    &nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" Font-Bold="True" Height="30px" OnClick="Button1_Click" Text="Поиск" Width="100px" />
    <br />
    <br />
          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound">
              <Columns>
                  <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="" Visible="false">
                      <ItemTemplate>
                          <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Показатель" Visible="True">
                      <ItemTemplate>
                          <asp:Label ID="Parametr" runat="server" Text='<%# Bind("Parametr") %>' Visible="True"></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Данные" Visible="True">
                      <ItemTemplate>
                          <asp:Label ID="Name" runat="server" Text='<%# Bind("Name") %>' Visible="True"></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Текущий статус" Visible="True">
                      <ItemTemplate>
                          <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>' Visible="True"></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Дата добавления" Visible="True">
                      <ItemTemplate>
                          <asp:Label ID="Date" runat="server" Text='<%# Bind("Date") %>' Visible="True"></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Просмотреть">
                      <ItemTemplate>
                          <asp:Button ID="EditButton" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Select" OnClick="EditButtonClik" Text="Просмотреть" Width="150px" />
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Утвердить">
                      <ItemTemplate>
                          <asp:Label ID="Color" runat="server" Text='<%# Bind("Color") %>' Visible="false"></asp:Label>
                          <asp:Button ID="AcceptButton" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Select" OnClick="AcceptButtonClik" Text="Утвердить" Width="150px" />
                      </ItemTemplate>
                  </asp:TemplateField>
              </Columns>
    </asp:GridView>

    </asp:Content>
