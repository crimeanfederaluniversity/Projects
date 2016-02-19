<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AcademicMobileRequest.aspx.cs" Inherits="PersonalPages.AcademicMobileRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Заявка на академическую мобильность.<br />
        </h3>
      
         <h3>Анкета конкурсанта на участие в академической мобильности:</h3>
    Название сети: 
    <br />
    <asp:TextBox ID="TextBox3" runat="server"   Width="600px"></asp:TextBox>
  
         <style>
	.demo {
		border:1px solid #C0C0C0;
		border-collapse:collapse;
		padding:5px;
	}
	.demo th {
		border:1px solid #C0C0C0;
		padding:5px;
		background:#F0F0F0;
	}
	.demo td {
		border:1px solid #C0C0C0;
		padding:5px;
	}
</style>
    <table class="demo">
	    <caption>Срок академической мобильности:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </caption>
	<thead>
	<tr>
		<th>Начало</th>
		<th>Окончание</th>
	</tr>
	</thead>
	<tbody>
	<tr>
		<td><asp:Calendar ID="Calendar2" runat="server"></asp:Calendar></td>
		<td><asp:Calendar ID="Calendar3" runat="server"  ></asp:Calendar></td>
	</tr>
	</tbody>
</table>

    <br />

    Партнерская организация(в порядке приоритетности):<br />
    <asp:TextBox ID="TextBox14" runat="server" TextMode="MultiLine" Width="801px" Height="40px"></asp:TextBox>
    <br />
    <br />
    Обоснование целей и мотивирование реализации академической мобильности:<span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: medium; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none;"><br />
    <asp:TextBox ID="TextBox5" runat="server" Height="40px" TextMode="MultiLine" Width="800px"></asp:TextBox>
    <br />
    <br />
    Ожидаемые результаты академической и научной активности (участие в конференциях, публикации, сертификаты и т.д.):<br />
    <asp:TextBox ID="TextBox6" runat="server" Height="40px" TextMode="MultiLine" Width="800px"></asp:TextBox>
    <br />
    <br />
    План работы в принимающей организации(задача и период выполнения):&nbsp; <br />
        <asp:TextBox ID="TextBox15" runat="server" Height="40px" TextMode="MultiLine" Width="800px"></asp:TextBox>
    </span>
    <h4>Персональные данные: </h4>
    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
    &nbsp;
                 <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
&nbsp;<asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
    &nbsp;<div class="wrapper">
            <div class="left_block">Дата рождения:<br />
    <asp:TextBox ID="TextBox12" runat="server" Width="159px"></asp:TextBox>
    <br />
                Контактный телефон:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
    <asp:TextBox ID="TextBox1" runat="server" Width="163px"></asp:TextBox>
    <br />
                Электронный адрес:<br />
    <asp:TextBox ID="TextBox4" runat="server" Width="162px"></asp:TextBox>
    <br />
                Постоянный почтовый адрес(адрес регистрации):&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
    <asp:TextBox ID="TextBox2" runat="server" Height="35px" TextMode="MultiLine" Width="473px"></asp:TextBox>
    <br />
                <asp:Label ID="Label7" runat="server" Text="Сфера научных интересов:"></asp:Label>
    <br />
    <asp:TextBox ID="TextBox11" runat="server" Height="35px" TextMode="MultiLine" Width="475px"></asp:TextBox>
            </div>
<div class="right_block">
    <asp:Label ID="Label3" runat="server" Text="Уровень образования:"></asp:Label>
    <br />
    <asp:TextBox ID="TextBox7" runat="server" Width="310px"></asp:TextBox>
    <br />
    <asp:Label ID="Label4" runat="server" Text="Направление подготовки:"></asp:Label>
    <br />
    <asp:TextBox ID="TextBox8" runat="server" Width="310px"></asp:TextBox>
    <br />
    <asp:Label ID="Label5" runat="server" Text="Направленность:"></asp:Label>
    <br />
    <asp:TextBox ID="TextBox9" runat="server" Width="310px"></asp:TextBox>
    <br />
    <asp:Label ID="Label6" runat="server" Text="Средний балл:"></asp:Label>
    <br />
    <asp:TextBox ID="TextBox10" runat="server" Width="310px"></asp:TextBox>
    <br />
    <asp:Label ID="Label10" runat="server" Text="Укажите кафедру:"></asp:Label>
    <br />
    <asp:TextBox ID="TextBox13" runat="server" Width="310px"></asp:TextBox>
        
            </div>
</div>
    &nbsp;<br />
    <br />
&nbsp;Примечание: Для отправки заявки необходимо прикрепить следующие документы:<br />
    <br />
<table class="demo">
	<thead>
	<tr>
		<th>Список необходимых документов</th>
		<th>Прикрепление</th>
	</tr>
	</thead>
	<tbody>
	<tr>
		<td>план работы</td>
		<td> 
    <asp:FileUpload ID="FileUpload1" runat="server" Width="323px" /> 
        </td>
	</tr>
	<tr>
		<td>ходатайство</td>
		<td> 
    <asp:FileUpload ID="FileUpload2" runat="server" Width="323px"   /> 
        </td>
	</tr>
	<tr>
		<td>потребность в фин.поддержке</td>
		<td> 
    <asp:FileUpload ID="FileUpload3" runat="server" Width="323px"   /> 
        </td>
	</tr>
	<tr>
		<td>перечень опубликованных учебных изданий и научных трудов</td>
		<td> 
    <asp:FileUpload ID="FileUpload4" runat="server" Width="323px"   /> 
        </td>
	</tr>
	<tr>
		<td>приглашение от принимающей стороны</td>
		<td> 
    <asp:FileUpload ID="FileUpload5" runat="server" Width="323px"   /> 
        </td>
	</tr>
	<tr>
		<td>согласие на обработку персональных данных</td>
		<td> 
    <asp:FileUpload ID="FileUpload6" runat="server" Width="323px"   /> 
        </td>
	</tr>
	<tr>
		<td>копия паспорта</td>
		<td> 
    <asp:FileUpload ID="FileUpload7" runat="server" Width="323px"   /> 
        
            </td>
	</tr>
	<tbody>
</table>


 
        &nbsp;<br />
    <asp:Button ID="Button2" runat="server" Text="Отправить заявку " OnClick="Button2_Click" />
    <br />

</asp:Content>
