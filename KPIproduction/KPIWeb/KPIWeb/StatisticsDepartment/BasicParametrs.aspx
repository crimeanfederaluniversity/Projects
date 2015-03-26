<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="BasicParametrs.aspx.cs" Inherits="KPIWeb.StatisticsDepartment.BasicParametrs" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <div>   
        <asp:Menu ID="Menu1" runat="server"  Width="168px"  Orientation="Horizontal" StaticEnableDefaultPopOutImage="False"   OnMenuItemClick="Menu1_MenuItemClick" >
             <Items>
        <asp:MenuItem Text="Проверка базовых показателей" Value="0">
        </asp:MenuItem>

        <asp:MenuItem Text="Редактирование базовых показателей " Value="1">            
        </asp:MenuItem>

        <asp:MenuItem Text="Добавление новых базовых показателей" Value="2">           
        </asp:MenuItem>
    </Items>
        </asp:Menu>

        <br />
        <br />
        <br />
  <asp:MultiView  ID="MultiView1" runat="server" ActiveViewIndex="0"  >
   <asp:View ID="Tab1" runat="server"  >
        <table width="600" height="400" cellpadding=0 cellspacing=0>
            <tr valign="top">
                <td class="TabArea" style="width: 600px">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Отчёт по существующим показателям" Width="375px" />
                    <br />
                    <br />
                    <asp:TextBox ID="TextBox1" runat="server" Height="238px" TextMode="MultiLine" Width="368px"></asp:TextBox>
                </td>
            </tr>
        </table>
     </asp:View>
    <asp:View ID="Tab2" runat="server">
        <table width="600px" height="400px" cellpadding=0 cellspacing=0>
            <tr valign="top">
                <td class="TabArea" style="width: 600px">
                    Введите ID<br />
                    <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                    <asp:Button ID="Button2" runat="server" Text="Загрузить" Width="181px" OnClick="Button2_Click" />
                    <br />
                    <br />
                    ID<br />
                    <asp:TextBox ID="TextBox2" runat="server" Width="39px" ReadOnly="True"></asp:TextBox>
                    <br />
                    <br />
                    Актив<br />
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                    <br />
                    <br />
                    Название<br />
                    <asp:TextBox ID="TextBox4" runat="server" Width="200px"></asp:TextBox>
                    <br />
                    <br />
                    Аббревиатура англ<br />
                    <asp:TextBox ID="TextBox5" runat="server" Width="200px"></asp:TextBox>
                    <br />
                    <br />
                    Аббревиатура рус<br />
                    <asp:TextBox ID="TextBox6" runat="server" Width="200px"></asp:TextBox>
                    <br />
                    <br />
                    Ед. измерения<br />
                    <asp:TextBox ID="TextBox7" runat="server" Width="200px"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Button ID="Button3" runat="server" Text="Сохранить изменения" Width="205px" OnClick="Button3_Click" />
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="Tab3" runat="server">
        <table width="600px" height="400px" cellpadding=0 cellspacing=0>
            <tr valign="top">
                <td class="TabArea" style="width: 600px">
                    Вставьте в текстовое поле
                    <br />
                    Название параметра#Аббревиатура англ#Аббревиатура рус#единица измерения#уровень вводящего#только для иностранных<br />
                    <asp:TextBox ID="TextBox9" runat="server" Height="332px" TextMode="MultiLine" Width="595px"></asp:TextBox>
                    <br />
                    <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Добавить введенные данные" Width="596px" />
                </td>
            </tr>
        </table>
    </asp:View>
</asp:MultiView>
        <br />
        <br />
    
        <br />
    
        <br />
    
    </div>
        <br />
        <br />
</asp:Content>