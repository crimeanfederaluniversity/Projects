<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AcademicMobileRequest.aspx.cs" Inherits="PersonalPages.AcademicMobileRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <h3> Пожалуйста, заполните анкету для участия в академической мобильности:<br />
        </h3>
     <p> Название сети: <asp:TextBox ID="TextBox3" runat="server"   Width="336px"></asp:TextBox>
 
     <p> Срок академической мобильности: 
        <br />
        <div class="wrapper">
            <div class="left_block">Начало:<asp:Calendar ID="Calendar2" runat="server"></asp:Calendar>      
            </div>
<div class="right_block">Окончание:<asp:Calendar ID="Calendar3" runat="server"  ></asp:Calendar>
        
            </div>
</div>

    <p> &nbsp;<p> Партнерская организация(в порядке приоритетности):<p> 
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Height="52px" Width="604px">
               <Columns>    
            <asp:TemplateField Visible="false"   HeaderText="Номер" >
                        <ItemTemplate>
                            <asp:Label ID="LabelID" runat="server"  Visible="false" Text='<%# Bind("ID") %>'  ></asp:Label> 
                               </ItemTemplate>
                    </asp:TemplateField> 
             <asp:TemplateField Visible="true"   HeaderText="Название организации">
                        <ItemTemplate>
                            <asp:TextBox ID="FIO" runat="server"  Visible="true" Text='<%# Bind("FIO") %>'  ></asp:TextBox> 
                               </ItemTemplate>
                    </asp:TemplateField> 
                     <asp:TemplateField HeaderText="Удалить">
                        <ItemTemplate>  
                           <asp:Button ID="DeleteButton" runat="server" CommandName="Select" Text="Удалить" Width="200px" CommandArgument='<%# Eval("ID") %>' OnClick="DeleteButtonClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>               
                      </Columns>    
        </asp:GridView>
        &nbsp;<br />
        &nbsp;<asp:Button ID="AddRowButton" runat="server" OnClick="AddRowButton_Click" Text="Добавить строку" Width="299px" />
        &nbsp;
        <asp:Button ID="SaveFIOButton" runat="server" OnClick="SaveFIOButton_Click" Text="Сохранить" Width="299px" />
    <br />
    <br />
    Обоснование целей и мотивирование реализации академической мобильности:<span style="color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: medium; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none;"><br />
    <asp:TextBox ID="TextBox5" runat="server" Height="53px" TextMode="MultiLine" Width="535px"></asp:TextBox>
    <br />
    <br />
    Ожидаемые результаты академической и научной активности (участие в конференциях, публикации, сертификаты и т.д.):<br />
    <asp:TextBox ID="TextBox6" runat="server" Height="53px" TextMode="MultiLine" Width="535px"></asp:TextBox>
    <br />
    <br />
    План работы в принимающей организации:<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Height="52px" Width="604px"  >
               <Columns>    
            <asp:TemplateField Visible="false"   HeaderText="Номер" >
                        <ItemTemplate>
                            <asp:Label ID="LabelID0" runat="server"  Visible="false" Text='<%# Bind("ID") %>'  ></asp:Label> 
                               </ItemTemplate>
                    </asp:TemplateField> 
             <asp:TemplateField Visible="true"   HeaderText="Период выполнения">
                        <ItemTemplate>
                            <asp:TextBox ID="FIO0" runat="server"  Visible="true" Text='<%# Bind("FIO") %>'  ></asp:TextBox> 
                               </ItemTemplate>
                    </asp:TemplateField> 
                   <asp:TemplateField Visible="true"   HeaderText="Задача">
                        <ItemTemplate>
                            <asp:TextBox ID="FIO0" runat="server"  Visible="true" Text='<%# Bind("FIO") %>'  ></asp:TextBox> 
                               </ItemTemplate>
                    </asp:TemplateField> 
                     <asp:TemplateField HeaderText="Удалить">
                        <ItemTemplate>  
                           <asp:Button ID="DeleteButton0" runat="server" CommandName="Select" Text="Удалить" Width="200px" CommandArgument='<%# Eval("ID") %>' OnClick="DeleteButtonClick" />
                        </ItemTemplate>
                    </asp:TemplateField>               
                      </Columns>    
        </asp:GridView>
        &nbsp;<br />
        &nbsp;<asp:Button ID="AddRowButton0" runat="server" OnClick="AddRowButton_Click" Text="Добавить строку" Width="299px" />
        &nbsp;
        <asp:Button ID="SaveFIOButton0" runat="server" OnClick="SaveFIOButton_Click" Text="Сохранить" Width="299px" />
    <br />
    </span>
    <h4>Персональные данные: </h4>
&nbsp;<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
    &nbsp;
    <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
&nbsp;
    <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
    <br />
    <br />
    Дата рождения:<br />
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
    <asp:TextBox ID="TextBox2" runat="server" Height="35px" TextMode="MultiLine" Width="322px"></asp:TextBox>
    <br />
&nbsp;<asp:Label ID="Label7" runat="server" Text="Сфера научных интересов:"></asp:Label>
    <br />
    <asp:TextBox ID="TextBox11" runat="server" Height="35px" TextMode="MultiLine" Width="322px"></asp:TextBox>
    <br />
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
    <br />
    <br />
    <asp:Button ID="Button2" runat="server" Text="Отправить заявку " />
    <br />

</asp:Content>
