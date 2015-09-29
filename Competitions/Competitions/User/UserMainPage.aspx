<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserMainPage.aspx.cs" Inherits="Competitions.User.UserMainPage" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>


<asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
    <div>    
        <asp:Button ID="Button2" runat="server" OnClientClick="showLoadPanel()" Text="На главную" Width="125px" OnClick="Button2_Click" />   
    </div> 
</asp:Panel> 
        

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

            .CalendarPic
            {
                top: 0px;
                width: 30px;
                height: 30px;
            }
            .CalendarTextBox
            {
                top: 0px;
            }
             .Initial
             {
                 display: block;
                 padding: 4px 18px 4px 18px;
                 float: left;
                 background: url("../images/InitialImage.png") no-repeat right top;
                 color: Black;
                 font-weight: bold;
             }
            .Initial:hover
            {
                color: White;
                background: url("../images/SelectedButton.png") no-repeat right top;
            }
            .Clicked
            {
                float: left;
                display: block;
                background: url("../images/SelectedButton.png") no-repeat right top;
                padding: 4px 18px 4px 18px;
                color: Black;
                font-weight: bold;
                color: White;


            }
        </style>
         <br />
        <br />
        <table width="100%" align="center">
      <tr>
        <td>
            <br />
          <asp:Button Text="Открытые конкурсы" BorderStyle="None" ID="Tab1" CssClass="Initial" runat="server"
              OnClick="Tab1_Click" />
          <asp:Button Text="Мои активные заявки" BorderStyle="None" ID="Tab2" CssClass="Initial" runat="server"
              OnClick="Tab2_Click" />
          <asp:Button Text="Мои архивные заявки" BorderStyle="None" ID="Tab3" CssClass="Initial" runat="server"
              OnClick="Tab3_Click" />
          <asp:MultiView ID="MainView" runat="server">
            <asp:View ID="View1" runat="server">
              <table style="width: 100%; border-style: hidden">
                <tr>
                  <td>
                      <h2><span style="font-size: 20px">На данный момент для подачи заявак доступны следующие конкурсы:</span></h2>
                        <br />
                         <asp:GridView ID="MainGV" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                                <asp:BoundField DataField="Number"   HeaderText="Шифр" Visible="true" />
                                <asp:BoundField DataField="Name"   HeaderText="Конкурс" Visible="true" />
                                <asp:BoundField DataField="Budjet"   HeaderText="Бюджет" Visible="true" />
                                <asp:BoundField DataField="StartDate"   HeaderText="Дата начала" Visible="true" />
                                <asp:BoundField DataField="EndDate"   HeaderText="Дата окончания" Visible="true" />
                                <asp:TemplateField HeaderText="Подать заявку">
                                        <ItemTemplate>
                                           <asp:Button ID="NewApplication" runat="server" OnClientClick="return confirm('Вы уверены, что хотите подать заявку?');" Text="Подать заявку" CommandArgument='<%# Eval("ID") %>' OnClick="NewApplication_Click1" />
                                        </ItemTemplate>
                                </asp:TemplateField>
              
                            </Columns>
                        </asp:GridView>
                  </td>
                </tr>
              </table>
            </asp:View>
            <asp:View ID="View2" runat="server">
              <table style="width: 100%; border-width: 1px; border-color: #666; border-style: hidden">
                <tr>
                  <td>
<h2><span style="font-size:20px">Мои заявки </span></h2>
        <br />
        <asp:GridView ID="ApplicationGV" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                <asp:BoundField DataField="Name"   HeaderText="Название" Visible="true" />
                <asp:BoundField DataField="CompetitionName"   HeaderText="Конкурс" Visible="true" />
            
                <asp:TemplateField HeaderText="Заполнить">
                        <ItemTemplate>
                            <asp:Button ID="FillButton" runat="server" CommandName="Select" Text="Заполнить" CommandArgument='<%# Eval("ID") %>'   Width="200px" OnClick="FillButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Отправить на рассмотрение">
                        <ItemTemplate>
                            <asp:Button ID="SendButton" runat="server" CommandName="Select" OnClientClick="return confirm('Вы уверены, что хотите отправить заявку на рассмотрение?');" Text="Отправить" CommandArgument='<%# Eval("ID") %>'   Width="200px" OnClick="SendButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Скачать заявку">
                        <ItemTemplate>
                            <asp:Button ID="GetDocButton" runat="server" CommandName="Select" Text="Загрузить" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="GetDocButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
             
            </Columns>
        </asp:GridView>

        <br />
                  </td>
                </tr>
              </table>
            </asp:View>
            <asp:View ID="View3" runat="server">
              <table style="width: 100%; border-width: 1px; border-color: #666; border-style: hidden">
                <tr>
                  <td>
                      <h2><span style="font-size: 30px">Архив заявок </span></h2>
                    <br />
                    <asp:GridView ID="ArchiveApplicationGV" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                            <asp:BoundField DataField="Name"   HeaderText="Название" Visible="true" />
                            <asp:BoundField DataField="CompetitionName"   HeaderText="Конкурс" Visible="true" />
                                       
                            <asp:TemplateField HeaderText="Скачать заявку">
                                    <ItemTemplate>
                                        <asp:Button ID="GetDocButton" runat="server" CommandName="Select" Text="Загрузить" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="GetDocButtonClick"/>
                                    </ItemTemplate>
                            </asp:TemplateField>
             
                        </Columns>
                    </asp:GridView>
                    <br />
                  </td>
                </tr>
              </table>
            </asp:View>
          </asp:MultiView>
        </td>
      </tr>
    </table>
       
    
    </div>
</asp:Content>
