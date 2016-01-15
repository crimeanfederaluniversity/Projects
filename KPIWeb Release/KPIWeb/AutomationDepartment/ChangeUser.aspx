<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ChangeUser.aspx.cs" Inherits="KPIWeb.AutomationDepartment.ChangeUser" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">  
 
        Изменение параметров доступа и заполнения целевых показателей пользователями<br />
        Выберите шаблон<br />
        <br />
    

        <asp:DropDownList ID="DropDownList1" runat="server" Height="22px" Width="218px">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Применить шаблон" Width="218px" />
        <br />
    

        <br />
        <asp:Button ID="Button1" runat="server" Text="Сохранить изменения" Width="226px" OnClick="Button1_Click" />
        <br />
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
                                        <asp:CheckBox ID="CheckBoxCanEdit" Text=" " runat="server" Checked='<%# Bind("EditChecked") %>'></asp:CheckBox>                                                                                                                   
                                    </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Просм">
                                    <ItemTemplate>
                                         <asp:CheckBox ID="CheckBoxCanView" Text=" " runat="server" Checked='<%# Bind("ViewChecked") %>'></asp:CheckBox>                          
                                    </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Подтв">
                                    <ItemTemplate>
                                         <asp:CheckBox ID="CheckBoxVerify" Text=" " runat="server"  Checked='<%# Bind("VerifyChecked") %>'></asp:CheckBox>                        
                                    </ItemTemplate>
            </asp:TemplateField>
            </Columns>
        </asp:GridView>
    
     <br />
    Рассчетные показатели<asp:GridView ID="CalcGrid" runat="server" AutoGenerateColumns="False" >
            <Columns>
            <asp:TemplateField HeaderText="Название">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Visible="False" Text='<%# Bind("CalcID") %>'></asp:Label>
                                        <asp:Label ID="Label4" runat="server" Visible="True" Text='<%# Bind("Name1") %>'></asp:Label>
                                    </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Просм">
                                    <ItemTemplate>
                                         <asp:CheckBox ID="CheckBoxCanView1" Text=" "  runat="server" Checked='<%# Bind("ViewChecked1") %>'></asp:CheckBox>                          
                                    </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Подтв">
                                    <ItemTemplate>
                                         <asp:CheckBox ID="CheckBoxVerify1" Text=" "  runat="server"  Checked='<%# Bind("VerifyChecked1") %>'></asp:CheckBox>                        
                                    </ItemTemplate>
            </asp:TemplateField>
            </Columns>
        </asp:GridView>
    
        <br />
    Целевые показатели<asp:GridView ID="IndicatorGrid" runat="server" AutoGenerateColumns="False">
            <Columns>
            <asp:TemplateField HeaderText="Название">
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Visible="False" Text='<%# Bind("IndID") %>'></asp:Label>
                                        <asp:Label ID="Label6" runat="server" Visible="True" Text='<%# Bind("Name2") %>'></asp:Label>
                                    </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Просм">
                                    <ItemTemplate>
                                         <asp:CheckBox ID="CheckBoxCanView2" Text=" "  runat="server" Checked='<%# Bind("ViewChecked2") %>'></asp:CheckBox>                          
                                    </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Подтв">
                                    <ItemTemplate>
                                         <asp:CheckBox ID="CheckBoxVerify2" Text=" "  runat="server"  Checked='<%# Bind("VerifyChecked2") %>'></asp:CheckBox>                        
                                    </ItemTemplate>
            </asp:TemplateField>
            </Columns>
        </asp:GridView>


</asp:Content>
