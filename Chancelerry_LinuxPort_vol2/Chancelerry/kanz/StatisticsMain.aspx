<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StatisticsMain.aspx.cs" Inherits="Chancelerry.kanz.StatisticsMain" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="toggleLoadingScreen.js" type="text/javascript"></script>
     <script src="calendar_ru.js" type="text/javascript">
    </script>
      <style type="text/css">
          
           .resultTable {
               border-collapse: collapse; 
           }
          .resultTable TH, TD {
              border: 1px solid black; 
              text-align: left; 
              padding: 4px; 
          }
          .resultTable TH {
              background: #fc0; 
              height: 40px; 
              vertical-align: bottom; 
              padding: 0;
          }

      </style>
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
                    Поле фильтра по значению
                </td>

            </tr>

            <tr>
                <td rowspan="3">
                   <asp:DropDownList ID="RegistersDropoDownList" runat="server" Height="20px" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="SumByRegistersDropoDownList_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td rowspan="3">
                    <asp:DropDownList ID="FiedsDropDownList" runat="server" Enabled="False" Height="20px" Width="250px"></asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DateForFilter" runat="server" Enabled="False" Height="20px" Width="250px"></asp:DropDownList>
                </td>
                <td>
                   <asp:DropDownList ID="FilterFiedsDropDownList" runat="server" Enabled="False" Height="20px" Width="250px"></asp:DropDownList>
                </td>


                 </tr>    
            <tr>
                
                    <td>
                        Начальная дата(включительно)
                     <asp:TextBox ID="StartDateTextBox" runat="server"></asp:TextBox>
                 </td>
                    <td rowspan="2">
                       Поиск (без учета регистра)
                      <asp:TextBox ID="SearchInFieldTextBox" runat="server"></asp:TextBox>
                 </td>
                
            </tr>
             <tr>

                    <td>
                       Конечная дата(включительно)
                     <asp:TextBox ID="EndDateTextBox" runat="server"></asp:TextBox>
                 </td>
               
            </tr>
        </table>

        <br/>
        <br/>
         <table>
            <tr>
                <td>
                    <asp:Button ID="SumByButton" runat="server" Text="Просуммировать по выбранному полю" Width="300px" Enabled="False" OnClick="SumByButton_Click"  OnClientClick="showLoadingScreen()" />     
                </td>
                <td>
                     &nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:Label ID="SumByResultLabel" runat="server" Text=""></asp:Label>
                </td>
            </tr>   
              <tr>
                <td>
                    <asp:Button ID="CreateTableButton" runat="server" Text="Сформировать таблицу" Width="300px" Enabled="False" OnClick="CreateTableButton_Click"  OnClientClick="showLoadingScreen()"/>     
                </td>
                <td>
                </td>
            </tr>  
        </table>    

        
        <br />
        

        
        <br />
                         

        
    </div>
    
    <div runat="server" id="resultDiv">
        </div>
    <asp:Button ID="PrintFinded" runat="server" Text="Печать" Width="469px" OnClick="PrintFinded_Click" OnClientClick="showLoadingScreen()"/>
</asp:Content>
