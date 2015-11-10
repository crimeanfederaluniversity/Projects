<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="ColumnCreateEdit.aspx.cs" Inherits="Competitions.Admin.ColumnCreateEdit" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">        
    <div>
        <br />
    <asp:Button ID="GoBackButton" runat="server"  Text="Назад" Width="131px" style="height: 26px" OnClick="GoBackButton_Click" />
        <br />
        Название<br />
        <asp:TextBox ID="NameTextBox" runat="server" Width="545px"></asp:TextBox>
        <br />
        Уникальная метка<br />
        <asp:TextBox ID="UniqueMarkTextBox" runat="server" Width="542px"></asp:TextBox>
        <br />
        Описание<br />
        <asp:TextBox ID="DescriptionTextBox" runat="server" Width="545px"></asp:TextBox>
        <br />
        Тип вносимых данных<br />
        <asp:DropDownList ID="DataTypeDropDownList" runat="server" Height="25px" Width="554px" AutoPostBack="True" OnSelectedIndexChanged="DataTypeDropDownList_SelectedIndexChanged">
        </asp:DropDownList>
    
        <div id="ChooseColumnForDropDownDiv" runat="server" Visible="false">
            <asp:DropDownList ID="FkToColumnDropDown" runat="server" Height="28px" Width="556px" AutoPostBack="True">
            </asp:DropDownList>           
        </div>

        <div id="ChooseConstantForDropDownDiv" runat="server" Visible="false">
                    <asp:DropDownList ID="FkToConstantDropDown" runat="server"  Height="21px" Width="554px" OnSelectedIndexChanged="FkToConstantDropDown_SelectedIndexChanged">
        </asp:DropDownList>
        </div>
        <div id="ChooseBitForDepend" runat="server" Visible="false">
                    <asp:DropDownList ID="BitColumnsDropDown" runat="server"  Height="21px" Width="554px" >
        </asp:DropDownList>
        </div>

        <asp:Panel ID="Panel1" runat="server" Visible="false">
            По какой колонке привязваем&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; К какой колонке привязываем
            <br />(колонка в этой секции)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (колонка на секцию где хранятся сами данные)
            <br />
            <asp:DropDownList ID="Fk_ColumnConnectFromDropDown" runat="server" AutoPostBack="True" Height="31px" Width="188px">
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="Fk_ColumnConnectToDropDown" runat="server" AutoPostBack="True" Height="26px" Width="190px">
            </asp:DropDownList>
        </asp:Panel>
        
        <asp:Panel ID="PanelForConnectionToTwoInSameSection" runat="server" Visible="false">
            Операнд слева от знака
            <br />
            <asp:DropDownList ID="FK_ColumnOneInSameRow" runat="server" AutoPostBack="True" Height="31px" Width="188px">
            </asp:DropDownList>
            <br />
            Операнд справа от знака<br /> &nbsp;<asp:DropDownList ID="FK_ColumnTwoInSameRow" runat="server" AutoPostBack="True" Height="26px" Width="190px">
            </asp:DropDownList>
        </asp:Panel>
        
        
        <asp:Panel ID="PanelForMaxEndMinValue" runat="server" Visible="false">
            Минимальное значение
            <br />
            (Пока только 0 - если надо чтобы минимальное значение было не раньше чем начало заявки.<br /> Если ограничений нет, оставить пустым)
            <br />
            <asp:TextBox ID="MinValueTextBox" runat="server"></asp:TextBox>
            <br />
            Максимальное значение
            <br />
            (Пока только 0 - если надо чтобы максимальное значение было не позже чем окончание заявки.
            <br />
            Если ограничений нет, оставить пустым)
            <br />
            <asp:TextBox ID="MaxValueTextBox" runat="server"></asp:TextBox>
            <br /> 
        </asp:Panel>

        <br />

        <br />

        <asp:CheckBox ID="SortByCheckBox" runat="server" Text="Сортировать по этому показателю" />
        <br />

        <asp:CheckBox ID="ColumnVisibleCheckBox" runat="server" Text="Visible" Checked="True" />
        <br />

        <asp:CheckBox ID="TotalUpCheckBox" runat="server" Text="Итого" />

        <br />
        <br />

        <br />

        <asp:Button ID="CreateSaveButton"  runat="server" Text="Сохранить" OnClick="CreateSaveButton_Click" />
    
    </div>
</asp:Content>