<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConfirmCheckBoxes.aspx.cs" Inherits="KPIWeb.Director.ConfirmCheckBoxes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    
    
<style type="text/css">
   .button_right 
   {
       float:right
   }    


</style> 

<asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
	<div>    
		  <asp:Button ID="GoBackButton" runat="server" OnClientClick="JavaScript:window.history.back(1); return false; showLoadPanel();"  Text="Назад" Width="125px" Enabled="True" OnClick="GoBackButton_Click" />
		  &nbsp;
		  <asp:Button ID="Button22" OnClientClick="showLoadPanel()" runat="server"  Text="На главную" Width="125px" Enabled="True" OnClick="Button22_Click" />
		  <asp:Button ID="Button5" runat="server" OnClientClick="showLoadPanel()" CssClass="button_right" OnClick="Button5_Click" Text="Нормативные документы" Width="300px" />
	</div>
</asp:Panel>

    <br />

    <h2>Параметры направлений подготовки</h2>

        <br />
        <h3>    <asp:Label ID="statusLabel" runat="server" Text="Label"></asp:Label> </h3>
        <br />
        <br />

    

     <asp:GridView ID="GridView1" runat="server" 
            AutoGenerateColumns="False"
            style="margin-top: 0px" OnRowDataBound="GridView1_RowDataBound"  >
             <Columns>  
                 <asp:BoundField DataField="ID" HeaderText="Код" Visible="false" />          
                 <asp:BoundField DataField="Number" HeaderText="Шифр направления подготовки" Visible="True" />
                 <asp:BoundField DataField="Name" HeaderText="Название направления подготовки" Visible="True" />
                  <asp:BoundField DataField="Faculty" HeaderText="Факультет" Visible="True" />
                  <asp:BoundField DataField="Kafedra" HeaderText="Кафедра" Visible="false" />

                     <asp:TemplateField Visible="true"   HeaderText="Используются совеременные образовательные технологии">
                        <ItemTemplate >
                            <asp:Label ID="color" runat="server" Visible="False" Text='<%# Eval("color") %>'/>
                            <asp:Image ID="Image0" Width="20px" Height="20px" ImageUrl="~/Director/Checked.png" Visible='<%# Eval("Checked0") %>'  runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>  

                    <asp:TemplateField Visible="true"   HeaderText="Осуществляется сетевое взаимодействие">
                        <ItemTemplate >
                            <asp:Image ID="Image1" Width="20px" Height="20px" ImageUrl="~/Director/Checked.png" Visible='<%# Eval("Checked1") %>'  runat="server" />                        </ItemTemplate>
                    </asp:TemplateField>  

                   <asp:TemplateField Visible="true"   HeaderText="Осуществляется обучение студентов с особыми потребностяим">
                        <ItemTemplate >
                            <asp:Image ID="Image2" Width="20px" Height="20px" ImageUrl="~/Director/Checked.png" Visible='<%# Eval("Checked2") %>'  runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>  

                   <asp:TemplateField Visible="true"   HeaderText="Предусмотрено обучение иностранных студентов">
                        <ItemTemplate >
                            <asp:Image ID="Image3" Width="20px" Height="20px" ImageUrl="~/Director/Checked.png" Visible='<%# Eval("Checked3") %>'  runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>  

                </Columns>
        </asp:GridView>
    <br />
    <br />
    <asp:Button ID="Button23" runat="server" Text="Утвердить" Width="328px" OnClick="Button23_Click" />
</asp:Content>
