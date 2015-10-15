<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserMainPage.aspx.cs" Inherits="Competitions.User.UserMainPage" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>


<asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
    <div>    
        <asp:Button ID="Button2" runat="server" OnClientClick="showLoadPanel()" Text="На главную" Width="125px" OnClick="Button2_Click" />   
    </div> 
</asp:Panel> 
        

        <h2><span style="font-size: 30px">Добро пожаловать в систему &quot;Конкурсы и проекты Программы развития&quot; </span></h2>
        

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
                      <h2><span style="font-size: 20px">Заявки, доступные для заполнения:</span></h2>
 
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
                      <h2><span style="font-size: 20px">Архив заявок: </span></h2>
                    <br />
                    <asp:GridView ID="ArchiveApplicationGV" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                            <asp:BoundField DataField="Name"   HeaderText="Название" Visible="true" />
                            <asp:BoundField DataField="CompetitionName"   HeaderText="Конкурс" Visible="true" />
                            <asp:BoundField DataField="SendedDate"   HeaderText="Дата отправки на рассмотрение" Visible="true" />   
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
