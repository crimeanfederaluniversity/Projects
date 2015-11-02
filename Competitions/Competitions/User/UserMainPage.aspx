<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserMainPage.aspx.cs" Inherits="Competitions.User.UserMainPage" %>
<asp:Content runat="server" ID="BodyContent"  ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" type="text/css" href="../Spinner.css"> 
    
    <script>
        window.onload = function () {
            document.getElementById('LoadPanel_').style.visibility = 'hidden';
        }
        document.onload = function () {
            document.getElementById('LoadPanel_').style.visibility = 'hidden';
        }
</script>

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
     
    

    

    <div>
        <style type="text/css">
.Initial
{
  display: block;
  padding: 4px 18px 4px 18px;
  float: left;
  background: url("../Images/InitialImage.png") no-repeat right top;
  color: Black;
  font-weight: bold;
}
.Initial:hover
{
  color: White;
  background: url("../Images/SelectedButton.png") no-repeat right top;
}
.Clicked
{
  float: left;
  display: block;
  background: url("../Images/SelectedButton.png") no-repeat right top;
  padding: 4px 18px 4px 18px;
  color: Black;
  font-weight: bold;
  color: White;
}

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

<asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
    <div>    
        <asp:Button ID="Button2" runat="server" OnClientClick="showLoadPanel()" Text="На главную" Width="125px" OnClick="Button2_Click" />   
    </div> 
</asp:Panel> 
         <br />
        <h2>&nbsp;<span style="font-size: 20px">Добро пожаловать в систему управления заявками модуля &quot;Конкурсы и проекты Программы развития&quot; </span></h2>
        
          
        <table width="100%" align="center">
      <tr>
        <td>
            <br />                      
          <asp:Button Text="Все открытые конкурсы" BorderStyle="None" ID="Tab1" CssClass="Initial" runat="server"
              OnClick="Tab1_Click" Width="262px" />
          <asp:Button Text="Заявки в работе" BorderStyle="None" ID="Tab2" CssClass="Initial" runat="server"
              OnClick="Tab2_Click" />
          <asp:Button Text="Поданные заявки" BorderStyle="None" ID="Tab3" CssClass="Initial" runat="server"
              OnClick="Tab3_Click" />
          <asp:Button Text="Черновики" BorderStyle="None" ID="Tab4" CssClass="Initial" runat="server"
              OnClick="Tab4_Click" />
          <asp:MultiView ID="MainView" runat="server">
            <asp:View ID="View1" runat="server">
              <table style="width: 100%; border-style: hidden">
                <tr>
                  <td>
                      <h2><span style="font-size: 20px"> Примечание:Вы можете отправить не более одной заявки! Вы не можете подавать заявки на конкурсы если&nbsp; участвуете в реализации другой заявки в текущее время!</span></h2>
                  
                            <asp:Label ID="Label1" runat="server" Visible = "False" Text="У Вас уже есть поданная на конкурс заявка!"></asp:Label>
                   
                        <h2><span style="font-size: 20px">На данный момент для подачи заявок доступны следующие конкурсы:</span></h2>
                        
                         <asp:GridView ID="MainGV" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                                <asp:BoundField DataField="Number"   HeaderText="Шифр" Visible="true" />
                                <asp:BoundField DataField="Name"   HeaderText="Конкурс" Visible="true" />
                                <asp:BoundField DataField="Budjet"   HeaderText="Бюджет" Visible="true" />
                                <asp:BoundField DataField="StartDate"   HeaderText="Дата начала" Visible="true" />
                                <asp:BoundField DataField="EndDate"   HeaderText="Дата окончания" Visible="true" />
                                <asp:TemplateField HeaderText="Подать заявку">
                                        <ItemTemplate>
                                           <asp:Button ID="NewApplication" runat="server" OnClientClick="return confirm('Вы уверены, что хотите создать новую заявку?');" Text="Подать заявку" CommandArgument='<%# Eval("ID") %>' OnClick="NewApplication_Click1" />
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
            
                <asp:TemplateField HeaderText="Редактировать заявку">
                        <ItemTemplate>
                            <asp:Button ID="FillButton" runat="server" CommandName="Select" Text="Заполнить" CommandArgument='<%# Eval("ID") %>'   Width="200px" OnClick="FillButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Отправить на рассмотрение">
                        <ItemTemplate>
                            <asp:Label ID="StatusLabel" runat="server"  Enabled='<%# Bind("StatusLabelEnabled") %>' Visible='<%# Bind("StatusLabelEnabled") %>'  Text="Для отправки на рассмотрение необходимо полностью заполнить заявку!"></asp:Label>
                            <asp:Button ID="SendButton" runat="server"  Enabled='<%# Bind("SendButtonEnabled") %>'  Visible='<%# Bind("SendButtonEnabled") %>' CommandName="Select" OnClientClick="return confirm('Внимание! Вы можете отправить только одну заявку. Оставшиеся заявки будут перемещены в раздел черновики.Вы уверены, что хотите продолжить??');" Text="Отправить" CommandArgument='<%# Eval("ID") %>'   Width="200px" OnClick="SendButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Скачать заявку">
                        <ItemTemplate>
                            
                            <asp:RadioButtonList ID="RadioButtonList1" RepeatColumns="3" runat="server">
                                <asp:ListItem Value="0" Selected="True">doc</asp:ListItem>
                                <asp:ListItem Value="1">docx</asp:ListItem>
                                <asp:ListItem Value="2">rtf</asp:ListItem>
                                <asp:ListItem Value="3">pdf</asp:ListItem>
                                <asp:ListItem Value="4">odt</asp:ListItem>
                            </asp:RadioButtonList>

                            <asp:Button ID="GetDocButton2" runat="server"  CommandName="Select" Text="Скачать" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="GetDocButtonClick"/>
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
                      <h2><span style="font-size: 20px">Поданные заявки: </span></h2>
                    <br />
                    <asp:GridView ID="ArchiveApplicationGV" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                            <asp:BoundField DataField="Name"   HeaderText="Название" Visible="true" />
                            <asp:BoundField DataField="CompetitionName"   HeaderText="Конкурс" Visible="true" />
                            <asp:BoundField DataField="SendedDate"   HeaderText="Дата отправки на рассмотрение" Visible="true" />   
                            <asp:BoundField DataField="Accept"   HeaderText="Статус заявки" Visible="true" />
                            <asp:TemplateField HeaderText="Скачать заявку">
                                    <ItemTemplate>
                                                                    <asp:RadioButtonList ID="RadioButtonList1" RepeatColumns="3" runat="server">
                                <asp:ListItem Value="0" Selected="True">doc</asp:ListItem>
                                <asp:ListItem Value="1">docx</asp:ListItem>
                                <asp:ListItem Value="2">rtf</asp:ListItem>
                                <asp:ListItem Value="3">pdf</asp:ListItem>
                                <asp:ListItem Value="4">odt</asp:ListItem>
                            </asp:RadioButtonList>
                                        <asp:Button ID="GetDocButton" runat="server" Text="Скачать"  CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="GetDocButtonClick"/>
                                    </ItemTemplate>
                            </asp:TemplateField>
             
                        </Columns>
                    </asp:GridView>
                    <br />
                  </td>
                </tr>
              </table>
            </asp:View>
            <asp:View ID="View4" runat="server">
              <table style="width: 100%; border-width: 1px; border-color: #666; border-style: hidden">
                <tr>
                  <td>
                      <h2><span style="font-size: 20px">Черновики: </span></h2>
                    <br />
                    <asp:GridView ID="DraftGridView" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                            <asp:BoundField DataField="Name"   HeaderText="Название" Visible="true" />
                            <asp:BoundField DataField="CompetitionName"   HeaderText="Конкурс" Visible="true" />                           
                            
                            <asp:TemplateField HeaderText="Редактировать заявку">
                                <ItemTemplate>
                                    <asp:Button ID="FillButton" runat="server" CommandName="Select" Text="Заполнить" CommandArgument='<%# Eval("ID") %>'   Width="200px" OnClick="FillButtonClick"/>
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
