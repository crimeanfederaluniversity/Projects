<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CalendarPlan.aspx.cs" MasterPageFile="~/Site.Master" Inherits="Competition.CalendarPlan1" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent" >
 
    <h2><span style="font-size: 30px">План мероприятий</span></h2>
 
        <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server">
             <Columns>
                   <asp:TemplateField HeaderText="Номер" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                        <ItemTemplate> 
                            <asp:Label ID="ID_Event" runat="server" Text='<%# Bind("ID_Event") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Мероприятие" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                        <ItemTemplate> 
                            <asp:TextBox ID="Event" runat="server" Text='<%# Bind("Event") %>'  Visible="True"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                 <asp:TemplateField HeaderText="Длительность" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                        <ItemTemplate> 
                            <asp:TextBox ID="Period" runat="server" Text='<%# Bind("Period") %>'  Visible="True"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                  <asp:TemplateField HeaderText="Начало" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                        <ItemTemplate> 
                            <asp:TextBox ID="StartDate" runat="server" Text='<%# Bind("StartDate") %>'  Visible="True"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                  <asp:TemplateField HeaderText="Окончание" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                        <ItemTemplate> 
                            <asp:TextBox ID="EndDate" runat="server" Text='<%# Bind("EndDate") %>'  Visible="True"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                  <asp:TemplateField HeaderText="Стоимость(тыс.руб)" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                        <ItemTemplate> 
                            <asp:TextBox ID="Cost1000" runat="server" Text='<%# Bind("Cost1000") %>'  Visible="True"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                  <asp:TemplateField HeaderText="Источник финансирования" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                        <ItemTemplate> 
                            <asp:TextBox ID="SourceNull" runat="server" Text='<%# Bind("SourceNull") %>'  Visible="True"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                  <asp:TemplateField HeaderText="Период расходования" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                        <ItemTemplate> 
                            <asp:TextBox ID="TimeNull" runat="server" Text='<%# Bind("TimeNull") %>'  Visible="True"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                 </Columns>
        </asp:GridView>
        <br />
    <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Сохранить" Width="101px" OnClick="Button1_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Text="Добавить строку" Width="128px" OnClick="Button2_Click" />
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="Далее" />
  </asp:Content>    
