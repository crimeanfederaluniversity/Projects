<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChooseApplicationAction.aspx.cs" Inherits="Competitions.User.ChooseApplicationAction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
    <div>    
                <asp:ImageButton ID="GoBackButton" runat="server" OnClientClick="showLoadPanel()" Width="30px" OnClick="GoBackButton_Click" ImageAlign="Middle" ImageUrl="~/Images/Back.png" ToolTip="Назад" />
         <asp:ImageButton ID="ImageButton1" runat="server" OnClientClick="showLoadPanel()"  Width="30px" OnClick="Button2_Click" ImageUrl="~/Images/Home.png" ImageAlign="Middle" ToolTip="На главную" />
        &nbsp;      <asp:Label ID="CountDownLabel" runat="server" Text="Label"></asp:Label>
    </div> 
</asp:Panel> 
    
    <br />
    <br />


        <style type="text/css">
            
            .top_panel {
            position:fixed;
            left:0;
            top:3.5em;
            width:100%;
            height:30px;
            background-color:#222222;
            z-index:10;
            color:#05ff01;  
            padding-top:5px;
            font-weight:bold;
        }
        </style>
        <br />
    <br />
    <asp:Label ID="Label1" style="font-size: 20px" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:Label ID="Label2" style="font-size: 20px" runat="server" Text="Label"></asp:Label>
    <br />
    
    



    <br />
    <script src="../Calendar/datepicker.js" type="text/javascript" charset="UTF-8" ></script>
    <link rel="stylesheet" type="text/css" href="../Calendar/datepicker.css" />
    <b>Укажите даты начала и окончания реализации Вашего проекта:</b>
    
   
   
	<br/>
    Стартовая дата:<input id="startdata" type="text" runat="server"/> 
    <input type="button" style="background: url('../Calendar/datepicker.jpg') no-repeat; width: 30px; border: 0px;" onclick="displayDatePicker('ctl00$MainContent$startdata', false, 'ymd', '-');">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Конечная дата:<input id="enddata" type="text" runat="server"/> 
	<input type="button" style="background: url('../Calendar/datepicker.jpg') no-repeat; width: 30px; border: 0px;" onclick="displayDatePicker('ctl00$MainContent$enddata', false, 'ymd', '-');">
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click1" Text="Сохранить" />
    <br />
    <br />
    <br />
    <p>
        <asp:GridView ID="BlockGV" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                <asp:BoundField DataField="BlockName"   HeaderText="Название" Visible="true" />   
                <asp:BoundField DataField="Status"   HeaderText="Статус" Visible="true" />    
                <asp:TemplateField HeaderText="Перейти к заполнению">
                        <ItemTemplate>
                            <asp:Button ID="FillButton" runat="server" CommandName="Select" Text="Заполнить" CausesValidation="False" CommandArgument='<%# Eval("ID") %>' Enabled='<%# Bind("EnableButton") %>' Width="200px" OnClick="FillButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
                           
            </Columns>
        </asp:GridView>
    </p>
    <p>
        &nbsp;</p>
    <p>
        Прикрепление документов к заявке (не более 5 файлов)</p>
<p>
        <asp:GridView ID="DocumentsGV" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                <asp:BoundField DataField="Name" HeaderText="Название" Visible="true" />   
                <asp:BoundField DataField="CreateDate"   HeaderText="Дата загрузки" Visible="true" />     
                <asp:TemplateField HeaderText="Скачать документ">
                        <ItemTemplate>
                            <asp:Button ID="OpenButton" runat="server" CommandName="Select" Text="Скачать" CausesValidation="False" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="OpenButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Удалить документ">
                        <ItemTemplate>
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Select" CausesValidation="False" OnClientClick="return confirm('Вы уверены что хотите удалить документ?');" Text="Удалить" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="DeleteButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
                          
            </Columns>
        </asp:GridView>
    <p>
        
        <script>
            function countFiles() {
                if (document.getElementById('FileUpload1').files.count() > 5)
                    return false;
                return true;
            }
            
        </script>

        &nbsp;<p>
        
        <asp:TextBox ID="LinkToFileTextBox" runat="server" Width="299px"></asp:TextBox>
            &nbsp;&nbsp;Укажите прямую ссылку на документ:<p>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="LinkToFileTextBox"
 ErrorMessage="Неправильная ссылка. Пример: 'https://www.google.com'"  ForeColor="Red"
 ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></asp:RegularExpressionValidator>

        <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="True"  Width="392px" />
            

            <asp:RegularExpressionValidator ID="uplValidator" runat="server" ControlToValidate="FileUpload1"
 ErrorMessage="Неверный формат файла"  ForeColor="Red"
 ValidationExpression="(.+\.([Jj][Pp][Gg])|.+\.([Pp][Nn][Gg])|.+\.([Dd][Oo][Cc])|.+\.([Dd][Oo][Cc][Xx])|.+\.([Xx][Ll][Ss])|.+\.([Xx][Ll][Ss][Xx])|.+\.(Rr][Tt][Ff]))"></asp:RegularExpressionValidator>

    &nbsp;
        <asp:Label ID="ToManyFilesLabelError" runat="server" ForeColor="Red" Visible="False" Text="Не более 5 файлов"></asp:Label>

    </p>
    <p>
        &nbsp;</p>
<p>
        <asp:Button ID="AddDocumentsButton" runat="server" CausesValidation="True"  OnClick="AddDocumentsButton_Click" Text="Загрузить выбранные файлы на сервер и прикрепить ссылки" Width="497px" />
    </p>
    <p>
        &nbsp;</p>
</asp:Content>
