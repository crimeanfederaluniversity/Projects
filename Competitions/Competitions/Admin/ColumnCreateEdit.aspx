<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="ColumnCreateEdit.aspx.cs" Inherits="Competitions.Admin.ColumnCreateEdit" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">        
    <div>
        <br />
    <asp:Button ID="GoBackButton" runat="server"  Text="Назад" Width="131px" style="height: 26px" OnClick="GoBackButton_Click" />
        <br />
        Название<br />
        <asp:TextBox ID="NameTextBox" runat="server" Width="545px"></asp:TextBox>
        <br />
        Описание<br />
        <asp:TextBox ID="DescriptionTextBox" runat="server" Width="545px"></asp:TextBox>
        <br />
        Тип вносимых данных<br />
        <asp:DropDownList ID="DataTypeDropDownList" runat="server" Height="25px" Width="554px" AutoPostBack="True" OnSelectedIndexChanged="DataTypeDropDownList_SelectedIndexChanged">
        </asp:DropDownList>
    
        <br />
        <br />
    
        <div id="ChooseColumnForDropDownDiv" runat="server" Visible="False">
            <asp:DropDownList ID="FkToColumnDropDown" runat="server" Height="28px" Width="556px" AutoPostBack="True">
            </asp:DropDownList>
            <br />
            
        </div>
        
        
        


        <br />
        <div id="ChooseConstantForDropDownDiv" runat="server" Visible="False">
                    <asp:DropDownList ID="FkToConstantDropDown" runat="server"  Height="21px" Width="554px">
        </asp:DropDownList>
              <br />
            
        </div>
        <asp:Panel ID="Panel1" runat="server" Visible="False">
            По какой колонке привязваем&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; К какой колонке привязываем<br /> (колонка в этой секции)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (колонка на секцию где хранятся сами данные)<br />
            <asp:DropDownList ID="Fk_ColumnConnectFromDropDown" runat="server" AutoPostBack="True" Height="31px" Width="188px">
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="Fk_ColumnConnectToDropDown" runat="server" AutoPostBack="True" Height="26px" Width="190px">
            </asp:DropDownList>
        </asp:Panel>
        <asp:CheckBox ID="TotalUpCheckBox" runat="server" Text="Итого" />
        <br />
        <asp:Button ID="CreateSaveButton"  runat="server" Text="Сохранить" OnClick="CreateSaveButton_Click" />
    </div>
</asp:Content>