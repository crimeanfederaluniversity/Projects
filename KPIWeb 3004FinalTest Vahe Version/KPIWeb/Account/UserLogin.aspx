<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="UserLogin.aspx.cs" Inherits="KPIWeb.Account.UserLogin" %>
<%@ MasterType  virtualPath="~/Site.Master"%>
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
          background-color: whitesmoke;
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
        </div>
    
    
    
    <link href="/App_Themes/theme_1/css/login.css" rel="stylesheet" type="text/css" />
     <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Visible="false">

    </asp:Panel>
     
    <div class="first_div_on_login">
        <div class="login_image_gerb"></div>
        <br />
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Font-Size="18pt" Text="Пожалуйста,&nbsp;авторизуйтесь&nbsp;для&nbsp;начала&nbsp;работы"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Адрес электронной почты"></asp:Label>
  
        <br />
                        <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                        </asp:PlaceHolder>

       <asp:TextBox ID="UserName" CssClass="form-control" runat="server" Height="40px" Width="300px"></asp:TextBox>
       <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                                CssClass="text-danger" ErrorMessage="Поле &quot;Адрес электронной почты&quot; обязательное." />
        <br />
 
        <asp:Label ID="Label2" runat="server" Text="Пароль"></asp:Label>

      

        <br />

        <asp:TextBox ID="Password" CssClass="form-control" TextMode="Password" runat="server" Height="40px" Width="300px"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="Поле &quot;Пароль&quot; обязательное." />
        <br />
        <asp:Button ID="Button1" CssClass="form-control" OnClientClick="showLoadPanel()" runat="server" Text="Войти" Height="40px" OnClick="Button1_Click" Width="300px" />
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Account/RestorePassword.aspx">Забыли пароль?</asp:HyperLink>
        <br />
 </div>
   
</asp:Content>