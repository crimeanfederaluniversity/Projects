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
    <asp:Label ID="Label1" style="font-size: 20px" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:Label ID="Label2" style="font-size: 20px" runat="server" Text="Label"></asp:Label>
    <br />
    
    



    <br />
    <script src="../Calendar/datepicker.js" type="text/javascript" charset="UTF-8" ></script>
    <link rel="stylesheet" type="text/css" href="../Calendar/datepicker.css" />
    <b>Укажите даты начала и окончания реализации Вашего проекта:</b>
    
   
   
	<br/>
    Стартовая дата:<input id="startdata" type="date"  runat="server"/> 
    <input type="button" style="background: url('../Calendar/datepicker.jpg') no-repeat; width: 30px; border: 0px;" onclick="displayDatePicker('ctl00$MainContent$startdata', false, 'ymd', '-');">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    Конечная дата:<input id="enddata" type="date" runat="server"/> 
	<input type="button" style="background: url('../Calendar/datepicker.jpg') no-repeat; width: 30px; border: 0px;" onclick="displayDatePicker('ctl00$MainContent$enddata', false, 'ymd', '-');">
    <br />
    <b>
    <br />
    Укажите всех участников проекта:<br />
    </b>(Примечание:Работник КФУ может быть руководителем не более 1 команды (соискателей) проекта или единоличным участником не более 1 проекта, и участником не более 2 проектов.) 
	&nbsp;<asp:GridView ID="PartnersGV" runat="server" Width="500px" AutoGenerateColumns="False" BorderColor="Black"  BorderWidth="1px" CellPadding="0" OnRowDataBound="PartnersGV_RowDataBound">
	     <Columns>                             
                
             <asp:TemplateField Visible="true"   HeaderText="">
                        <ItemTemplate >                 
                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>' ></asp:Label>  
                            </ItemTemplate >  
                         </asp:TemplateField>  
                     <asp:TemplateField Visible="true"   HeaderText="Фамилия">
                        <ItemTemplate >                 
                            <asp:TextBox ID="Surname" runat="server" Text='<%# Bind("Surname") %>' ></asp:TextBox>  
                            </ItemTemplate >  
                         </asp:TemplateField>  
             <asp:TemplateField Visible="true"   HeaderText="Имя">
                        <ItemTemplate >                 
                            <asp:TextBox ID="Name" runat="server" Text='<%# Bind("Name") %>' ></asp:TextBox>  
                            </ItemTemplate >  
                         </asp:TemplateField>  
             <asp:TemplateField Visible="true"   HeaderText="Отчество">
                        <ItemTemplate >                 
                            <asp:TextBox ID="Patronymic" runat="server" Text='<%# Bind("Patronymic") %>' ></asp:TextBox>  
                            </ItemTemplate >  
                         </asp:TemplateField> 
             <asp:TemplateField Visible="true"   HeaderText="Укажите галочкой кто является руководителем">
                        <ItemTemplate >                 
                            <asp:CheckBox  ID="Role" runat="server" Checked='<%# Bind("Role") %>' ></asp:CheckBox>  
                            <asp:Label ID="Error1" runat="server" ForeColor="Red" Text="Этот сотрудник уже является руководителем другого проекта!" Visible="false"></asp:Label>  
                            <asp:Label ID="Error2" runat="server" ForeColor="Red" Text="Этот сотрудник уже участвует в двух других проектах!" Visible="false"></asp:Label> 
                            </ItemTemplate >  
                         </asp:TemplateField>  
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" 
                         HeaderText="Удалить">
                        <ItemTemplate >
                            <asp:ImageButton ID="DeleteRowButton" runat="server"  CommandName="Select" CommandArgument='<%# Eval("ID") %>'  CausesValidation="False" Width="20px" OnClick="DeleteRowButtonClick" ImageUrl="~/Images/Delete.png" ImageAlign="Middle" />
                        </ItemTemplate>
                    </asp:TemplateField>
                   </Columns>
    </asp:GridView>
    <asp:Button ID="AddRowButton" runat="server" OnClick="AddRowButton_Click" Text="Добавить строку" />
    &nbsp;&nbsp;
    <asp:Button ID="SavePartners" runat="server" OnClick="SavePartners_Click" Text="Сохранить" />
    <br />

    <br />
    <p>
        <asp:GridView ID="BlockGV" runat="server" AutoGenerateColumns="False" Width="500px">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                <asp:BoundField DataField="BlockName"   HeaderText="Название" Visible="true" />   
                <asp:BoundField DataField="Status"   HeaderText="Статус" Visible="true" />    
                <asp:TemplateField HeaderText="Перейти к заполнению">
                        <ItemTemplate>
                            <asp:Button ID="FillButton" runat="server" CommandName="Select" Text="Заполнить" CausesValidation="False" CommandArgument='<%# Eval("ID") %>' Enabled='<%# Bind("EnableButton") %>' Width="150px" OnClick="FillButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
                           
            </Columns>
        </asp:GridView>
    </p>
    <p>
        &nbsp;</p>
    <p>
        <b>Дополнительные файлы прикрепленные к заявке </b>(Для загрузки доступны следующие форматы: pdf, doc, docx, rtf, xml, png, jpg, jpeg):</p>
    <p>
   (Примечание: Вы можете прикрепить не более 5 файлов размером до 20 Мб каждый. Пожалуйста, укажите ссылку на файлы более 20 Мб.)<asp:GridView ID="DocumentsGV" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                <asp:BoundField DataField="Name" HeaderText="Название" Visible="true" />   
                <asp:BoundField DataField="CreateDate"   HeaderText="Дата загрузки" Visible="true" />     
                <asp:TemplateField HeaderText="Скачать документ">
                        <ItemTemplate>
                            <asp:Button ID="OpenButton" runat="server" CommandName="Select" Text="Скачать" CausesValidation="False" CommandArgument='<%# Eval("ID") %>' Width="150px" OnClick="OpenButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Удалить документ">
                        <ItemTemplate>
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Select" CausesValidation="False" OnClientClick="return confirm('Вы уверены что хотите удалить документ?');" Text="Удалить" CommandArgument='<%# Eval("ID") %>' Width="150px" OnClick="DeleteButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
                          
            </Columns>
        </asp:GridView>
    </p>
    <p>

     
             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="LinkToFileTextBox"
 ErrorMessage="Неправильная ссылка. Пример: 'https://www.google.com'"  ForeColor="Red"
 ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></asp:RegularExpressionValidator>

        <asp:Label ID="ToManyFilesLabelError" runat="server" ForeColor="Red" Visible="False" Text="Не более 5 файлов"></asp:Label>


            <asp:RegularExpressionValidator ID="uplValidator" runat="server" ControlToValidate="FileUpload1"
 ErrorMessage="Неверный формат файла"  ForeColor="Red"
 ValidationExpression="(.+\.([Jj][Pp][Gg])|.+\.([Pp][Nn][Gg])|.+\.([Dd][Oo][Cc])|.+\.([Dd][Oo][Cc][Xx])|.+\.([Xx][Ll][Ss])|.+\.([Xx][Ll][Ss][Xx])|.+\.(Rr][Tt][Ff]))"></asp:RegularExpressionValidator>

    
    </p>

    
    <p>

        <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="True"  Width="500px" />
            

    </p>
    <p>
        
            Прямая ссылка на файл более 20 Мб:0 Мб:<asp:TextBox ID="LinkToFileTextBox" runat="server" Width="400px"></asp:TextBox>
<p>
        <asp:Button ID="AddDocumentsButton" runat="server" CausesValidation="True"  OnClick="AddDocumentsButton_Click" Text="Загрузить " Width="150px" />
    </p>
    <p>
        &nbsp;<p>
</asp:Content>
