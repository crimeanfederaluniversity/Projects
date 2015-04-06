<%@ Page Language="C#" Title="Добавление роли" CodeBehind="AddRole.aspx.cs" MasterPageFile="~/Site.Master"  Inherits="KPIWeb.AutomationDepartment.AddRole"  AutoEventWireup="true" EnableViewStateMac="false" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
    
        <span style="font-size: 30pt">Создание новой роли</span><br />
    
    </div>
        Название роли
        <asp:TextBox ID="TextBox1" runat="server" Width="139px"></asp:TextBox>
&nbsp;
        <asp:CheckBox Text="Активна" ID="CheckBox1" runat="server" />
&nbsp;&nbsp;<asp:CheckBox Text="Руководство" ID="CheckBox2" runat="server" />
&nbsp;<asp:Button ID="Button1" runat="server" Text="Создать" Width="89px" OnClick="Button1_Click" Height="29px" />
        <br />
        _________________________________________________________________<br />
        <br />
        Изменение роли<br />
        _________________________________________________________________<br />
        <br />
        Привязка показателей к роли<br />
        <asp:DropDownList ID="DropDownList1" runat="server" Height="28px" Width="328px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
        </asp:DropDownList>
        <asp:Button ID="Button2" runat="server" Text="Загрузить в таблицу" Width="202px" OnClick="Button2_Click" Height="34px" />
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Сохранить изменения" Width="497px" />
        <br />
        Базовые показатели<br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
            <asp:TemplateField HeaderText="Название">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Visible="False" Text='<%# Bind("BasicId") %>'></asp:Label>
                                        <asp:Label ID="Label1" runat="server" Visible="True" Text='<%# Bind("Name") %>'></asp:Label>
                                    </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ред">
                                    <ItemTemplate>
                                        
                                        <asp:CheckBox Text=" " ID="CheckBoxCanEdit" runat="server"  Checked='<%# Bind("EditChecked") %>'></asp:CheckBox>   
                                       
                                        </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Просм">
                                    <ItemTemplate>
                                         <asp:CheckBox Text=" " ID="CheckBoxCanView" runat="server" Checked='<%# Bind("ViewChecked") %>'></asp:CheckBox>                          
                                    </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Подтв">
                                    <ItemTemplate>
                                         <asp:CheckBox Text=" " ID="CheckBoxVerify" runat="server"  Checked='<%# Bind("VerifyChecked") %>'></asp:CheckBox>                        
                                    </ItemTemplate>
            </asp:TemplateField>
            </Columns>
        </asp:GridView>
    
     <br />
    Рассчетные показатели<asp:GridView ID="CalcGrid" runat="server" AutoGenerateColumns="False">
            <Columns>
            <asp:TemplateField HeaderText="Название">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Visible="False" Text='<%# Bind("CalcID") %>'></asp:Label>
                                        <asp:Label ID="Label4" runat="server" Visible="True" Text='<%# Bind("Name1") %>'></asp:Label>
                                    </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Просм">
                                    <ItemTemplate>
                                         <asp:CheckBox Text=" " ID="CheckBoxCanView1" runat="server" Checked='<%# Bind("ViewChecked1") %>'></asp:CheckBox>                          
                                    
                                    </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Подтв">
                                    <ItemTemplate>
                                         <asp:CheckBox Text=" " ID="CheckBoxVerify1" runat="server"  Checked='<%# Bind("VerifyChecked1") %>'></asp:CheckBox>   
                                                             
                                    </ItemTemplate>
            </asp:TemplateField>
            </Columns>
        </asp:GridView>
    
        <br />
    Индикаторы<asp:GridView ID="IndicatorGrid" runat="server" AutoGenerateColumns="False">
            <Columns>
            <asp:TemplateField HeaderText="Название">
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Visible="False" Text='<%# Bind("IndID") %>'></asp:Label>
                                        <asp:Label ID="Label6" runat="server" Visible="True" Text='<%# Bind("Name2") %>'></asp:Label>
                                    </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Просм">
                                    <ItemTemplate>
                                         <asp:CheckBox Text=" " ID="CheckBoxCanView2" runat="server" Checked='<%# Bind("ViewChecked2") %>'></asp:CheckBox>                          
                                    </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Подтв">
                                    <ItemTemplate>
                                         <asp:CheckBox Text=" " ID="CheckBoxVerify2" runat="server"  Checked='<%# Bind("VerifyChecked2") %>'></asp:CheckBox>                        
                                    </ItemTemplate>
            </asp:TemplateField>
            </Columns>
        </asp:GridView>

</asp:Content>