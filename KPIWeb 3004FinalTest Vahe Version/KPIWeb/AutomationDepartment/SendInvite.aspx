<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="SendInvite.aspx.cs" Inherits="KPIWeb.AutomationDepartment.SendInvite" %>
 <asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">  
     
     <link rel="stylesheet" type="text/css" href="../Spinner.css">     

    <script type="text/javascript">
        function ConfirmSubmit1() {
            var msg = confirm('Вы уверены что хотите отправить email каждому незарегистрированному пользователю?');
            if (msg == true) {
                document.getElementById('LoadPanel_').style.visibility = 'visible'
                return true;
            }
            else {
                document.getElementById('LoadPanel_').style.visibility = 'hidden'
                return false;
            }
        }

        function ConfirmSubmit2() {
            var msg = confirm('Вы уверены что хотите отправить email пользователям прикрепленным к отмеченным академиям?');
            if (msg == true) {
                document.getElementById('LoadPanel_').style.visibility = 'visible'
                return true;
            }
            else {
                document.getElementById('LoadPanel_').style.visibility = 'hidden'
                return false;
            }
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

     <br /><br />

     <asp:Button ID="Button1" runat="server" Text="Отправить всем незарегистрированным" OnClientClick="ConfirmSubmit1()" OnClick="Button1_Click" Width="494px" />

     <br />
     <br />


     <asp:Button ID="Button2" runat="server" Text="Отправить всем отмеченным ниже" OnClientClick="ConfirmSubmit2()" OnClick="Button2_Click" />

     <br />
     <br />
     <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" OnClientClick="ConfirmSubmit2()" Text="Отправить по списку" Width="495px" />

     <br />
     <br />

     <asp:CheckBoxList ID="CheckBoxList1" runat="server">
     </asp:CheckBoxList>


     <br />
     <asp:Label ID="Label2" runat="server" Text="Список"></asp:Label>
     <br />
     <asp:ListBox ID="ListBox1" runat="server" Height="234px" Width="290px"></asp:ListBox>
     <br />
     <br />
     <br />
     <asp:Label ID="Label1" runat="server" Text="Добавить email"></asp:Label>
&nbsp;<br />
     <asp:TextBox ID="TextBox1" runat="server" Width="292px"></asp:TextBox>
     <br />
     <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Добавить" />
     <br />
     <br />
</asp:Content>