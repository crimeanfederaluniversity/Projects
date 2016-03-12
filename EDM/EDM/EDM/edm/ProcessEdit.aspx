<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ProcessEdit.aspx.cs" Inherits="EDM.edm.ProcessEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <script src="calendar_ru.js" type="text/javascript"> </script>
    
    <div id="createNewProcessDiv" runat="server" >
    <table>
        <tr>
		   <th>Тип согласования</th>
            <th>Название</th>
            <th>Кол-во согласующих</th>
            <th>Создать</th>
            
        </tr>
        <tr>
		   <td>
            <asp:DropDownList ID="ProcessTypeDropDown" runat="server" AutoPostBack="False">
                <asp:ListItem Value="parallel">Параллельное согласование</asp:ListItem>
                <asp:ListItem Value="serial">Последовательное согласование</asp:ListItem>
                <asp:ListItem Value="review">Рецензия</asp:ListItem>
            </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="ProcessNameTextBox"  runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="ParticipantsCountTextBox" TextMode="Number" runat="server"></asp:TextBox>
            </td>
            
            <td>
                <asp:Button ID="CreateProcessButton" runat="server" OnClick="CreateNewProcess" Text="Создать" />
            </td>           
        </tr>
        </table>
    </div>

    <br/>
    
    <div runat="server" id="existingProcessTitleDiv">
        <asp:Label ID="ProcessIdLabel" runat="server" Text=""></asp:Label>
    </div>
    
    <br/>

    <div runat="server" id="ParticipantsDiv"> 
    
    </div>
    
    <br/>
    
    <div runat="server" id ="documentsDiv">
        </div>
    
    <br/>

    <div runat="server" id="SaveAllDiv">
        <asp:Button ID="SaveAllButton"  runat="server" Text="Сохранить" OnClick="SaveAllButton_Click" />
        </div>
   

</asp:Content>
