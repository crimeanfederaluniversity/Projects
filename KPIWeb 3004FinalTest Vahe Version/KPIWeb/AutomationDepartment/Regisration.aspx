<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Regisration.aspx.cs" Inherits="KPIWeb.AutomationDepartment.Regisration" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">  
    
    <link rel="stylesheet" type="text/css" href="../Spinner.css"> 
     
    <script type="text/javascript">
        function showLoadPanel() {
            document.getElementById('LoadPanel_').style.visibility = 'visible';
        }
    </script>
    <style>  

        .LoadPanel 
   {
          position: fixed;
          z-index: 10;
          background-color: #101010;
          top: 0;
          left: 0;
          width: 100%;
          height: 100%;
          opacity: 0.9;
          visibility: hidden;
   }
</style>     
    <div id="LoadPanel_" class='LoadPanel'>               
            <div id="floatingCirclesG">
            <div class="f_circleG" id="frotateG_01">
            </div>
            <div class="f_circleG" id="frotateG_02">
            </div>
            <div class="f_circleG" id="frotateG_03">
            </div>
            <div class="f_circleG" id="frotateG_04">
            </div>
            <div class="f_circleG" id="frotateG_05">
            </div>
            <div class="f_circleG" id="frotateG_06">
            </div>
            <div class="f_circleG" id="frotateG_07">
            </div>
            <div class="f_circleG" id="frotateG_08">
            </div>
            </div>
          <div style="  
   
    position: fixed; left: 38%; top: 60%;text-align:center;
    "><font style=" 
    color:#7fff00;
    font-size:20px;
    font-style:normal;
    font-weight:900;
    text-shadow: 1px 1px 1px black, 0 0 1em #00ffff;
    ">Происходит обработка данных</font><br/>
      <font style=" 
    color:#ff0000;
    font-size:20px;
    font-style:normal;
    font-weight:900;
     text-shadow: 1px 1px 1px black, 0 0 1em #ffffff;"
          >Дождитесь завершения процесса</font></div>
        </div>
          
         <div>
        <span style="font-size: 30px">Регистрация нового пользователя</span><br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Выберите университет"></asp:Label>
        <br />
        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True" Height="40px" Width="400px"></asp:DropDownList>
        <br />
      </div>  
         <div>
        <asp:Label ID="Label2" runat="server" Text="Выберите факультет"></asp:Label>
         <br />
        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged1" Height="40px" Width="400px"></asp:DropDownList>
         <br />
      </div> 
         <div>
            <asp:Label ID="Label3" runat="server" Text="Выберите кафедру"></asp:Label>
             <br />
            <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control" AutoPostBack="True" Height="40px" Width="400px" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged"></asp:DropDownList>
             <br />
          </div>   
         <div>
        <asp:Label ID="Label4" runat="server" Text="Выберите статус пользователя"></asp:Label>
         <br />
                <asp:DropDownList ID="DropDownList5" runat="server" CssClass="form-control" OnSelectedIndexChanged="DropDownList5_SelectedIndexChanged" AutoPostBack="True" Height="40px" Width="400px">
                    <asp:ListItem Value="-1">Выберите статус</asp:ListItem>   
                    <asp:ListItem Value="0">Исполнитель</asp:ListItem>              
                    <asp:ListItem Value="5">Проректор</asp:ListItem>          
                     <asp:ListItem Value="2">Деканат</asp:ListItem>
                    <asp:ListItem Value="4">Директор академии</asp:ListItem>  
                    <asp:ListItem Value="9">Администратор</asp:ListItem>               
                </asp:DropDownList>
         <br />
      </div>    
         <div>
        <asp:Label ID="Label24" runat="server" Text="Выберите шаблон"></asp:Label>
         <br />
        <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control" AutoPostBack="True" Height="40px" Width="400px" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged1"></asp:DropDownList>
         <br />

      </div> 
         <div>
                <div>
                    <asp:Label ID="Label25" runat="server" Text="Целевые показатели"></asp:Label>
                    <asp:GridView ID="Gridview1" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Название">
                                <ItemTemplate>
                                    <asp:Label ID="IndicatorID" runat="server" Visible="False" Text='<%# Bind("IndicatorID") %>'></asp:Label>
                                    <asp:Label ID="IndicatorName" runat="server" Visible="True" Text='<%# Bind("IndicatorName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField  HeaderText="Просм">
                                <ItemTemplate>
                                    <asp:CheckBox Text=" " ID="IndicatorViewCheckBox" runat="server" Checked='<%# Bind("IndicatorViewCheckBox") %>'></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Подтв">
                                <ItemTemplate>
                                    <asp:CheckBox Text=" " ID="IndicatorConfirmCheckBox" runat="server" Checked='<%# Bind("IndicatorConfirmCheckBox") %>'></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
             <div>
                 <asp:Label ID="Label26" runat="server" Text="Рассчетные показатели"></asp:Label>
                 <asp:GridView ID="Gridview2" runat="server" AutoGenerateColumns="False">
                     <Columns>
                         <asp:TemplateField HeaderText="Название">
                             <ItemTemplate>
                                 <asp:Label ID="CalculatedParametrsID" runat="server" Visible="False" Text='<%# Bind("CalculatedParametrsID") %>'></asp:Label>
                                 <asp:Label ID="CalculatedParametrsName" runat="server" Visible="True" Text='<%# Bind("CalculatedParametrsName") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                                 
                         <asp:TemplateField HeaderText="Просм">
                             <ItemTemplate>
                                 <asp:CheckBox Text=" " ID="CalculatedParametrsViewCheckBox" runat="server" Checked='<%# Bind("CalculatedParametrsViewCheckBox") %>'></asp:CheckBox>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Подтв">
                             <ItemTemplate>
                                 <asp:CheckBox Text=" " ID="CalculatedParametrsConfirmCheckBox" runat="server" Checked='<%# Bind("CalculatedParametrsConfirmCheckBox") %>'></asp:CheckBox>
                             </ItemTemplate>
                         </asp:TemplateField>
                     </Columns>
                 </asp:GridView>
             </div>
   
            <div >
                <asp:Label ID="Label27" runat="server" Text="Базовые показатели"></asp:Label>
                <asp:GridView ID="Gridview3" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="Gridview3_SelectedIndexChanged" OnRowDataBound="Gridview3_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Название">
                            <ItemTemplate>
                                <asp:Label ID="BasicParametrsID" runat="server" Visible="False" Text='<%# Bind("BasicParametrsID") %>'></asp:Label>
                                <asp:Label ID="BasicParametrsName" runat="server" Visible="True" Text='<%# Bind("BasicParametrsName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                

                        <asp:TemplateField HeaderText="Редак">
                            <ItemTemplate>
                                <asp:CheckBox Text=" " ID="BasicParametrsEditCheckBox" runat="server" Checked='<%# Bind("BasicParametrsEditCheckBox") %>'></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Просм">
                            <ItemTemplate>
                                <asp:CheckBox Text=" " ID="BasicParametrsViewCheckBox" runat="server" Checked='<%# Bind("BasicParametrsViewCheckBox") %>'></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Подтв">
                            <ItemTemplate>
                                <asp:CheckBox Text=" " ID="BasicParametrsConfirmCheckBox" runat="server" Checked='<%# Bind("BasicParametrsConfirmCheckBox") %>'></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
                <br />
                Должность<asp:TextBox ID="PositionText" CssClass="form-control" runat="server" Height="40px" Width="400px"></asp:TextBox>
            </div>
            </div>   
         <div>
        <asp:Label ID="UserNameLabel" runat="server" Text="Имя пользователя"></asp:Label>
         <br />
         <asp:TextBox ID="UserNameText" CssClass="form-control" runat="server" Height="40px" Width="400px"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ControlToValidate="UserNameText"
                CssClass="text-danger" ErrorMessage="Введите имя пользователя." ID="errorNoName" />
         <br />
      </div> 
         <div>
        <asp:Label ID="EmailLabel" runat="server" Text="Адрес электронной почты "></asp:Label>
         <br />
         <asp:TextBox ID="EmailText" CssClass="form-control" runat="server" Height="40px" Width="400px"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ControlToValidate="EmailText"
                CssClass="text-danger" ErrorMessage="Введите почтовый адрес" ID="errorNoEmailText" />

         <asp:RegularExpressionValidator ID="errorInvalidEmail" runat="server"
                ErrorMessage="Неверный почтовый адрес" CssClass="text-danger"
                ControlToValidate="EmailText"
                ValidationExpression="^([A-Za-z0-9_-]+\.)*[A-Za-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$" />

         <br />
      </div> 
         <div>
        <asp:Label ID="PassLabel" runat="server" Text="Пароль"></asp:Label>
         <br />
         <asp:TextBox ID="PasswordText" TextMode="Password" CssClass="form-control" runat="server" Height="40px" Width="400px"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ControlToValidate="PasswordText"
                CssClass="text-danger" ErrorMessage="Введите пароль." ID="errorNoPass" />
         <br />
      </div> 
         <div>
        <asp:Label ID="ConfPassLabel" runat="server" Text="Подтверждение пароля"></asp:Label>
         <br />
         <asp:TextBox ID="ConfirmPasswordText" TextMode="Password" CssClass="form-control" runat="server" Height="40px" Width="400px"></asp:TextBox>
          
              <asp:RequiredFieldValidator runat="server"
                ControlToValidate="ConfirmPasswordText"
                CssClass="text-danger" Display="Dynamic" ErrorMessage="Введите подтверждение пароля." ID="errorNoConfirm" />
            <asp:CompareValidator runat="server" ControlToCompare="PasswordText" ControlToValidate="ConfirmPasswordText"
                CssClass="text-danger" Display="Dynamic" ErrorMessage="Пароли не совпадают." ID="ErrorWrongConfirm" />
             <br />
             <br />
             <asp:CheckBox ID="CheckBox1" runat="server" Checked="True" OnCheckedChanged="CheckBox1_CheckedChanged1" Text="Отправить email?" />
             <br />
             <br />
             <asp:Button ID="Button1" runat="server" CssClass="form-control" OnClientClick="showLoadPanel()" Text="Создать пользователя" Height="40px" Width="400px" OnClick="Button1_Click" />
         <br />
      </div>           
</asp:Content>