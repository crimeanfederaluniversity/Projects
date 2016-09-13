<%@ Page Language="C#" Title="Данные о пользователе" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PersonalInfo.aspx.cs" Inherits="Registration.Account.PersonalInfo" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
     
        <h3>Здравствуйте! Вы перешли на страницу регистрации системы "Рейтинг научно-педагогических работников КФУ". </h3>
        <h3>Пожалуйста, заполните и отправьте данную форму. Все поля, кроме указанных, являются обязательными для заполнения!</h3>
        <p>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text="Фамилия:"></asp:Label>
        &nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox2" runat="server" Width="200px"></asp:TextBox>
        &nbsp;<br />
        <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text="Имя:"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
        <asp:TextBox ID="TextBox3" runat="server" Width="200px"></asp:TextBox>
        <br />
        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text="Отчество:"></asp:Label>
        <asp:TextBox ID="TextBox4" runat="server" Width="200px"></asp:TextBox>
        </p>
        <p>
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text="E-mail: "></asp:Label>
        &nbsp;&nbsp; &nbsp;
        <asp:TextBox ID="TextBox1" runat="server" Width="200px"></asp:TextBox>
        </p>
        <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text="Вид деятельности:"></asp:Label>
        &nbsp;<asp:DropDownList ID="DropDownList6" runat="server" Width="200px" AutoPostBack ="true">
            <asp:ListItem  Value="0">Преподавательская (ППС)</asp:ListItem>
            <asp:ListItem Value="1">Научная (НР)</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text="Должность: "></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="PPS" runat="server" Width="200px" Visible="False">
            <asp:ListItem    Value="0">Доцент</asp:ListItem>
            <asp:ListItem  Value="0">Профессор</asp:ListItem>
            <asp:ListItem Value="0">Преподаватель</asp:ListItem>
            <asp:ListItem Value="0">Старший преподаватель</asp:ListItem>
            <asp:ListItem Value="0">Ассистент</asp:ListItem>
            <asp:ListItem Value="1">Заведующий кафедрой</asp:ListItem>
            <asp:ListItem Value="2">Декан факультета</asp:ListItem>
            <asp:ListItem Value="5">Директор института</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="NR" runat="server" Width="200px" Visible="False">
            <asp:ListItem   Value="0">Научный сотрудник</asp:ListItem>
            <asp:ListItem Value="0">Младший научный сотрудник</asp:ListItem>
            <asp:ListItem Value="0">Старший научный сотрудник</asp:ListItem>
            <asp:ListItem Value="0">Ведущий научный сотрудник</asp:ListItem>
            <asp:ListItem Value="0">Главный научный сотрудник</asp:ListItem>
            <asp:ListItem Value="1">Заведующий (начальник) отдела</asp:ListItem>
            <asp:ListItem Value="1">Заведующий (начальник) лаборатории</asp:ListItem>
            <asp:ListItem Value="1">Заведующий (начальник) центра</asp:ListItem>
            <asp:ListItem Value="0">Ученый секретарь</asp:ListItem>
            <asp:ListItem Value="5">Директор подразделения</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text="Ставка(основная):"></asp:Label>
        &nbsp;<asp:DropDownList ID="stavka" runat="server">
              <asp:ListItem>1,0</asp:ListItem>
            <asp:ListItem>1,5</asp:ListItem>
            <asp:ListItem>1,25</asp:ListItem>        
            <asp:ListItem>0,75</asp:ListItem>
            <asp:ListItem>0,5</asp:ListItem>
            <asp:ListItem>0,25</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text="Ученая степень, звание (необязательное поле): "></asp:Label>
        <br />

        <asp:DropDownList ID="degree" runat="server" Width="200px">
                 <asp:ListItem   Value="0">Нет звания</asp:ListItem>
            <asp:ListItem>Доцент</asp:ListItem>
            <asp:ListItem>Профессор</asp:ListItem>      
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text="Структурное подразделение: " ></asp:Label>
        <br />
        <asp:DropDownList ID="DropDownList1" runat="server" Width="500px" AutoPostBack ="true" OnSelectedIndexChanged ="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
        &nbsp;<br />
        <br />
        <asp:DropDownList ID="DropDownList2" runat="server" Width="500px" AutoPostBack ="true" OnSelectedIndexChanged ="DropDownList2_SelectedIndexChanged">
        </asp:DropDownList>
        &nbsp;
        &nbsp;<br />
        <br />
        <asp:DropDownList ID="DropDownList3" runat="server" Width="500px">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text="Фамилия и инициалы на иностранном (для поиска Ваших работ в системах публикаций): "></asp:Label>
        <br />
        <asp:TextBox ID="TextBox6" runat="server" TextMode="MultiLine" Width="497px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Отправить" Width="200px" Font-Bold="True" Font-Italic="False" Font-Size="Medium" Height="50px" />
        <br />
    
    </div>

       </asp:Content>