<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Indicators.aspx.cs" Inherits="KPIWeb.StatisticsDepartment.Indicators" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><span style="font-size: 30px">Редактирование целевых показателей</span></h2>
    <div>          
        <br />
        <br />
        <asp:Menu ID="Menu1" runat="server"  Width="168px"  Orientation="Horizontal" StaticEnableDefaultPopOutImage="False"   OnMenuItemClick="Menu1_MenuItemClick" >
             <Items>
                <asp:MenuItem Text="Добавление и редактирование целевого показателя" Value="0">
                </asp:MenuItem>
                <asp:MenuItem Text="Добавление и редактирование расчётного показателя" Value="1">            
                </asp:MenuItem>
                <asp:MenuItem Text="Тестирование формулы с аббревиатурами" Value="2">           
                </asp:MenuItem>
                 <asp:MenuItem Text="Поиск базовых показателей по названию и аббревиатуре" Value="3">           
                </asp:MenuItem>
            </Items>
        </asp:Menu>

        <br />
        <br />

        <asp:MultiView ID="MultiView1" runat="server">           
            <asp:View ID="Tab1" runat="server"  >
                 <asp:Label ID="addtitle" runat="server" Font-Size="X-Large" Text="Форма создания"></asp:Label>
                 <br />
                 <br />
                 <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Height="26px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="500px">
                </asp:DropDownList>
                <br />
                <asp:Label ID="Label5" runat="server" Text="0" Visible="False"></asp:Label>
                <br />
                <asp:Label ID="Label1" runat="server" Text="Название целевого показателя"></asp:Label>
                <br />
                <asp:TextBox ID="IndicatorName" runat="server" Height="70px" TextMode="MultiLine" Width="500px"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label3" runat="server" Text="Единица измерения"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="IndicatorMeasure" runat="server"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                
                <asp:CheckBox ID="CheckBox1" runat="server" Text="Активен" />
                 <br />
                 <br />
                 <asp:Label ID="LabelAbb" runat="server" Text="Аббревиатура"></asp:Label>
                 <br />
                 <asp:TextBox ID="TextBoxAbb" runat="server" Width="294px" AutoPostBack="True" OnTextChanged="TextBoxAbb_TextChanged"></asp:TextBox>
                 &nbsp;&nbsp;&nbsp;
                 <br />
                 <asp:Label ID="LabelAbbError" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                 <br />
                <br />
                <asp:Label ID="Label2" runat="server" Text="Формула рассчета"></asp:Label>
                <br />
                <asp:TextBox ID="IndicatorFormula" runat="server" Height="70px" TextMode="MultiLine" Width="500px"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Сохранить все изменения" Width="500px" />
                <br />
            </asp:View>
            <asp:View ID="Tab2" runat="server"  >
                  <asp:TextBox ID="IndicatorFormula0" runat="server" Height="70px" TextMode="MultiLine" Width="500px"></asp:TextBox>
                <br />
                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Посчитать поле формулы с тестовыми данными" Width="500px" />
                <br />
                <br />
                <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Рассчитать для подраздления" Width="284px" />
                <asp:TextBox ID="TextBox2" runat="server" Width="27px"></asp:TextBox>
                <asp:TextBox ID="TextBox3" runat="server" Width="27px"></asp:TextBox>
                <asp:TextBox ID="TextBox4" runat="server" Width="27px"></asp:TextBox>
                <asp:TextBox ID="TextBox5" runat="server" Width="27px"></asp:TextBox>
                <asp:TextBox ID="TextBox6" runat="server" Width="27px"></asp:TextBox>
                <asp:TextBox ID="TextBox7" runat="server" Width="27px"></asp:TextBox>
                <br />
                <asp:Label ID="Label8" runat="server" Text="Результат или список ошибок"></asp:Label>
                <br />
                <asp:TextBox ID="TextBox1" runat="server" Height="100px" ReadOnly="True" TextMode="MultiLine" Width="500px"></asp:TextBox>
                <br />   
            </asp:View>
            <asp:View ID="Tab3" runat="server"  >
                <asp:Label ID="Label7" runat="server" Text="Поиск базовых показателей"></asp:Label>
                <br />
                <asp:TextBox ID="SearchBox" runat="server" Width="500px"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Искать" Width="500px" style="height: 25px" />
                <br />
                <br />
                <asp:GridView ID="GridView1" AutoGenerateColumns="False"  runat="server">
                    <Columns>
                        <asp:BoundField DataField="Name"  />
                        <asp:BoundField DataField="AbbreviationRU"  />
                    </Columns>                          
                </asp:GridView>
            </asp:View>
        </asp:MultiView>
        <br />
        <br />
    
    </div>                   

</asp:Content>

