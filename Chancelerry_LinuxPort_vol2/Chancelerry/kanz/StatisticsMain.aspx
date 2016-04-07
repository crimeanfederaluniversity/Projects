<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StatisticsMain.aspx.cs" Inherits="Chancelerry.kanz.StatisticsMain" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     <script src="calendar_ru.js" type="text/javascript">
    </script>
    
    <br/>
        <br/>
    <div id="FieldOptions">
        
        <table>
            <tr>
                <td>
                    Реестр
                </td>
                <td>
                    Поле
                </td>
                <td>
                    Поле фильтра по дате
                </td>
                <td>
                    Начальная дата(включительно)
                </td>
                <td>
                    Конечная дата(включительно)
                </td>
            </tr>
            <tr>
                <td>
                   <asp:DropDownList ID="RegistersDropoDownList" runat="server" Height="20px" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="SumByRegistersDropoDownList_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="FiedsDropDownList" runat="server" Enabled="False" Height="20px" Width="250px"></asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DateForFilter" runat="server" Enabled="False" Height="20px" Width="250px"></asp:DropDownList>
                </td>
                <td>
                   <asp:TextBox ID="StartDateTextBox" runat="server"></asp:TextBox>
                </td>
                <td>
                     <asp:TextBox ID="EndDateTextBox" runat="server"></asp:TextBox>
                </td>
            </tr>    
        </table>

        <br/>
        <br/>
         <table>
            <tr>
                <td>
                    <asp:Button ID="SumByButton" runat="server" Text="Просуммировать по выбранному полю" Width="300px" Enabled="False" OnClick="SumByButton_Click" />     
                </td>
                <td>
                     &nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:Label ID="SumByResultLabel" runat="server" Text=""></asp:Label>
                </td>
            </tr>   
              <tr>
                <td>
                    <asp:Button ID="CreateTableButton" runat="server" Text="Сформировать таблицу" Width="300px" Enabled="False" OnClick="CreateTableButton_Click"/>     
                </td>
                <td>
                </td>
            </tr>  
        </table>    

        
        <br />
                         

        
    </div>
    
    <div runat="server" id="resultDiv">
        </div>

</asp:Content>
