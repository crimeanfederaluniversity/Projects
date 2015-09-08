<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="ColumnCreateEdit.aspx.cs" Inherits="Competitions.Admin.ColumnCreateEdit" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">        
    <div>
        <br />
    <asp:Button ID="GoBackButton" runat="server"  Text="Назад" Width="131px" style="height: 26px" OnClick="GoBackButton_Click" />
        <br />
        <br />
        Название<br />
        <asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox>
        <br />
        <br />
        Описание<br />
        <asp:TextBox ID="DescriptionTextBox" runat="server"></asp:TextBox>
        <br />
        <br />
        Тип вносимых данных<br />
        <asp:DropDownList ID="DataTypeDropDownList" runat="server" Height="19px" Width="122px" AutoPostBack="True" OnSelectedIndexChanged="DataTypeDropDownList_SelectedIndexChanged">
        </asp:DropDownList>
    
        <br />
        <br />
    
        <div id="ChooseColumnForDropDownDiv" runat="server" Visible="False">
            <asp:DropDownList ID="FkToColumnDropDown" runat="server" Height="19px" Width="122px" AutoPostBack="True">
            </asp:DropDownList>
            <br />
            
        </div>
        <br />
        <div id="ChooseConstantForDropDownDiv" runat="server" Visible="False">
            <asp:DropDownList ID="FkToConstantDropDown" runat="server" Height="19px" Width="122px" AutoPostBack="True">
            </asp:DropDownList>
              <br />
            
        </div>
        <br />
        <br />
        <asp:Button ID="CreateSaveButton"  runat="server" Text="Сохранить" OnClick="CreateSaveButton_Click" />
    </div>
</asp:Content>