<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ModernAdd2.aspx.cs" Inherits="KPIWeb.Rector.NewInt.ModernAddStuff.ModernAdd2" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">   
   
            <asp:Panel runat="server" ID="top_panel1212" CssClass="top_panel" Height="40" Visible="true">    
                <div>    
              <asp:Button ID="GoBackButton142" runat="server" OnClientClick="showLoadPanel()" OnClick="GoBackButton_Click" Text="Назад" Width="125px" />
              <asp:Button ID="GoForwardButton152" runat="server" OnClientClick="showLoadPanel()" OnClick="GoForwardButton_Click" Text="Вперед" Width="125px" />
                &nbsp; &nbsp; <asp:Button ID="Button1264" runat="server" OnClientClick="showLoadPanel()" OnClick="Button4_Click" Text="На главную" Width="125px" />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                </div>

            </asp:Panel>

     <div>
    
         <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="OOPmagistr_1.png">ООП Магистратуры </asp:HyperLink>
         <br />
         <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="OOPbakalavr_1.png">ООП Бакалавриата </asp:HyperLink>
         <br />
         <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="DopProf_1.png">Дополнительные профессиональные программы</asp:HyperLink>
    
    </div>

    </asp:Content>