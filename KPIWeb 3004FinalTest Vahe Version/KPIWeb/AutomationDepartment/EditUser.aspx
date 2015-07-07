 <%@ Page  Title="Форма для редактирования базы пользователей" Language="C#" EnableViewStateMac="false" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="KPIWeb.AutomationDepartment.EditUser" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
        <h2><%: Title %>Просмотр и создание пользователей</h2>
    
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
       &nbsp;<asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Добавить нового пользователя" Width="594px" />
        &nbsp;&nbsp;
        <br />
        <br />
        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Объединить пользователей" Width="594px" />
        <br />
        <br />
       <asp:Label ID="Label1" runat="server" Visible="False" Text="Пароль доступа"></asp:Label>

 &nbsp;

 <asp:TextBox runat="server" ID="TextBox1" Visible="False" TextMode="Password"  Width="325px" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label2" runat="server" Text="Ключевое слово"></asp:Label>
&nbsp;<asp:TextBox ID="TextBox2" runat="server" Height="21px" Width="251px"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Поиск" OnClick="Button1_Click" Width="173px" />
        &nbsp;&nbsp;<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox ID="CheckBox1" Visible="False" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged1" Text="Показать пароли и e-mail пользователей" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
         
    
    </div>
         
    
        <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">           
             <Columns>               
                           
                 <asp:TemplateField HeaderText="ID пользователя" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="FourthlvlId" runat="server" Text='<%# Bind("FourthlvlId") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField>   
                 
                 

                   <asp:TemplateField HeaderText="Логин" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                           <asp:TextBox ID="Login" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Login") %>'></asp:TextBox>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField>     

                     <asp:TemplateField HeaderText="Пароль"  HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Password" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Password") %>'></asp:TextBox>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField>   

                    <asp:TemplateField HeaderText="Адрес почты" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Email" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 
                     
                  <asp:TemplateField HeaderText="Первый уровень" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Firstlvl" Enabled="False" ReadOnly="True" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Firstlvl") %>'></asp:TextBox>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 
                 
                  <asp:TemplateField HeaderText="Второй уровень" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Secondlvl" Enabled="False" ReadOnly="True" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Secondlvl") %>'></asp:TextBox>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField>   
                 
                 <asp:TemplateField HeaderText="Третий уровень" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Thirdlvl" Enabled="False" ReadOnly="True" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Thirdlvl") %>'></asp:TextBox>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 
                 
                 <asp:TemplateField HeaderText="Четвертый уровень" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Fourthlvl" Enabled="False" ReadOnly="True" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Fourthlvl") %>'></asp:TextBox>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 
                 
                     <asp:TemplateField HeaderText="Пятый уровень" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Fifthlvl" Enabled="False" ReadOnly="True" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Fifthlvl") %>'></asp:TextBox>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 
                 
                 <asp:TemplateField HeaderText="Уровень доступа" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Acceslvl" Enabled="False" ReadOnly="True" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Acceslvl") %>'></asp:TextBox>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 

                 <asp:TemplateField HeaderText="Нулевой уровень" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Zerolvl" Enabled="False" ReadOnly="True" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Zerolvl") %>'></asp:TextBox>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 

                 <asp:TemplateField HeaderText="Почта подтверждена" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Confirmed" Enabled="False" ReadOnly="True" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Confirmed") %>'></asp:TextBox>
                        </ItemTemplate>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 

                 <asp:TemplateField HeaderText="Должность" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Position" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Position") %>'></asp:TextBox>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 

                 <asp:TemplateField HeaderText="Удалить пользователя">
                        <ItemTemplate>
                            <asp:Button ID="DeleteUserButton" runat="server" CommandName="Select" Text="Удалить" Width="200px" CommandArgument='<%# Eval("FourthlvlId") %>' OnClick="DeleteUserButtonClick" />
                        </ItemTemplate>
                    </asp:TemplateField>   
                 
                 <asp:TemplateField HeaderText="Сохранить изменения">
                        <ItemTemplate>
                            <asp:Button ID="SaveUserButton" runat="server" CommandName="Select" Text="Сохранить" Width="200px" CommandArgument='<%# Eval("FourthlvlId") %>' OnClick="SaveUserButtonClick" />
                        </ItemTemplate>
                    </asp:TemplateField>  
                 
                 <asp:TemplateField HeaderText="Изменить права пользователя">
                        <ItemTemplate>
                            <asp:Button ID="ChangeUserButton" runat="server" CommandName="Select" Text="Изменить" Width="200px" CommandArgument='<%# Eval("ChangeUserButton") %>' OnClick="ChangeUserButtonClick" />
                        </ItemTemplate>
                    </asp:TemplateField>  
                                           
       
                </Columns>
       </asp:GridView>
    
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" OnClientClick="showLoadPanel()" Text="Разослать всем незарагистрированным пользователям" Width="597px" />
        <asp:CheckBox ID="CheckBox2" runat="server" Checked="True" Text="Предохранитель" />
 </asp:Content>