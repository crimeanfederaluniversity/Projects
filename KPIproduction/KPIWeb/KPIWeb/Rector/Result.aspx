<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Result.aspx.cs" Inherits="KPIWeb.Rector.Result" %>
 <asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">     
<style type="text/css">
   .button_right 
   {
       float: right;
       margin-right: 10px;
   }     
   .side_legend 
   {
        position:fixed;    
        top:10em;
        width:200px;
        height:150px;    
        border-color: black;
        border-width: medium;
        background-color:azure;
        z-index:5;
        right: 0px;
        border-style: solid;
       border-width: 1px;
       border-color: black;
    }
   .commentSectionStyle
    {
        position:fixed;    
        width:400px;
        height:400px;    
        background-color:beige;
        z-index:11;
        top: 50%;
        left: 50%;
        margin-top: -200px;
        margin-left: -200px;
        visibility: hidden;
       border-style: solid;
       border-width: 3px;
       border-color: black;
     }
   .commentLeftButton 
   {
       width: 185px;
        height: 40px;
       margin-left: 5px;     
       margin-top: 5px;
   }
   .commentRightButton 
   {
       width: 185px;
       height: 40px;
       margin-left: 5px;      
       margin-top: 5px;
   }
   .commentTextBox 
   {
       min-height: 300px;
       min-width: 385px;
       width: 385px;
       height: 300px;
       max-width: 385px;
       max-height: 300px;
       margin-top: 5px;
       margin-left: 5px;       
   }
   .ColorPanelLegend 
   {       
       height: 17px;
        
   }
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
<script type="text/javascript" language="javascript">
            function DoPostBack() {
                __doPostBack('cmd', 'thesearemyarguments');
            }
     </script>
<script type="text/javascript">
            var y = 0;
            function legendChange() {

                if (document.getElementById('sidePanel').style.right == '0px') {
                    document.getElementById('sidePanel').style.right = '-175px';
                }
                else {
                    document.getElementById('sidePanel').style.right = '0px';
                }
            }
            function showLoadPanel() {
                document.getElementById('LoadPanel_').style.visibility = 'visible';
            }

            function closeLoadPanel() {
                document.getElementById('LoadPanel_').style.visibility = 'hidden';
            }

            function closeCommentSection() {
                document.getElementById('comment_Section').style.visibility = 'hidden';
                closeLoadPanel();
            }
            function showCommentSection(i) {
                
                document.getElementById('comment_Section').style.visibility = 'visible';
                showLoadPanel();
                y = i;
                return false;
            }
            function showAlert() {
                alert(y);
            }

            function commentSendButtonClick() {
                __doPostBack('ButtonClickParam', y);

               
            }
</script>
<div id="sidePanel" class='side_legend' onclick="legendChange()">    
            <!--<button type="button" onclick="legendChange()">
            O</button>-->
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label7" runat="server" Text="Легенда" ValidateRequestMode="Enabled"></asp:Label>
            <br />
            <br />
            <asp:Panel ID="Panel5" runat="server" CssClass="ColorPanelLegend">
                <br />
            </asp:Panel>
            <asp:Label ID="Label8" runat="server" Text=".....Утверждено"></asp:Label>
            <br />
            <asp:Panel ID="Panel6" runat="server" Height="17" BackColor="#000099" BorderColor="#000099">
            </asp:Panel>
            <asp:Label ID="Label9" runat="server" Text=".....Готов к утверждению"></asp:Label>
            <br />
            <asp:Panel ID="Panel7" runat="server" Height="17" BackColor="#CC0000" BorderColor="#CC0000">
            </asp:Panel>
            <asp:Label ID="Label10" runat="server" style="text-align: center" Text=".....Неутвержденные данные"></asp:Label>
        </div>  
<div id="comment_Section" class='commentSectionStyle'>   
            <h4 style="text-align:center">Введите комментрий</h4>
            <asp:TextBox ID="TextBox1" runat="server"  CssClass="commentTextBox" TextMode="MultiLine" ></asp:TextBox>
            <button type="button"  class="commentLeftButton" onclick="closeCommentSection()">Отмена</button>
            <button type="button" class="commentRightButton" onclick="commentSendButtonClick()">Отправить</button>
        </div>
<asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
                <div>    
              <asp:Button ID="GoBackButton" runat="server" OnClick="GoBackButton_Click" Text="Назад" Width="125px" />
              <asp:Button ID="GoForwardButton" runat="server" OnClick="GoForwardButton_Click" Text="Вперед" Width="125px" />
                &nbsp; &nbsp; <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="На главную" Width="125px" />
                &nbsp; &nbsp;       
                <asp:Button ID="Button5" runat="server" CssClass="button_right" OnClick="Button5_Click" Text="Нормативные документы" Width="250px" />
                    &nbsp; &nbsp; &nbsp; &nbsp;
                </div>

            </asp:Panel>
     
      <div id="LoadPanel_" class='LoadPanel'>    
           
        </div>

     <br /><br /><br />
     <asp:Label ID="ReportTitle" runat="server" Text="ReportTitle" Font-Size="20pt"></asp:Label>
     <br />
     <asp:Label ID="PageFullName" runat="server" Text="PageFullName" Font-Size="15pt"></asp:Label>
     <br />
     <asp:TreeView ID="TreeView1" runat="server" NodeWrap="True" Visible="False" ShowExpandCollapse="False" ShowLines="True">
     </asp:TreeView>
     &nbsp;<asp:Button ID="Button7" runat="server" OnClick="Button7_Click" Text="Показать значение показателей по имеющимся данным" Width="558px" />
     <br />
     <br />
<asp:GridView ID="Grid" runat="server" 
            AutoGenerateColumns="False"
            style="margin-top: 0px" OnSelectedIndexChanged="Grid_SelectedIndexChanged" OnRowDataBound="Grid_RowDataBound">
             <Columns>                
                 <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />    
                 <asp:BoundField DataField="Number"   HeaderText="Номер" Visible="true" />
                 <asp:BoundField DataField="Abb"   HeaderText="Аббревиатура" Visible="True" />
                 <asp:BoundField DataField="Name" HeaderText="Get" Visible="True" />          
                 <asp:BoundField DataField="StartDate" HeaderText="Начальная дата отчёта" Visible="True" />
                 <asp:BoundField DataField="EndDate" HeaderText="Конечная дата отчёта" Visible="True" />
                 <asp:BoundField DataField="Value" HeaderText="Значение" Visible="True" />
                 <asp:BoundField DataField="PlannedValue" HeaderText="Плановое значение ЦП" Visible="True" />
                 <asp:BoundField DataField="Progress" HeaderText="Get" Visible="True" />
                 <asp:TemplateField HeaderText="Утвердить данные">
                        <ItemTemplate>
                            <asp:Button ID="ConfirmButton" runat="server" CommandName="Select" Visible='<%# Bind("CanConfirm") %>' Text="Утвердить" CommandArgument='<%# Eval("ID") %>' Width="200px"  
                               OnClick="ButtonConfirmClick"/>                                                    
                            <asp:Label ID="StatusLable"  runat="server" Visible='<%# Bind("ShowLable") %>' Text='<%# Eval("LableText") %>' ></asp:Label>
                             </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Детализация базового показателя по структурным подразделениям">
                        <ItemTemplate>
                            <asp:Button ID="Button1" runat="server" CommandName="Select" Text="Детализовать" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="Button1Click"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="get">
                        <ItemTemplate>
                            <asp:Button ID="Button2" runat="server" CommandName="Select" Text="Просмотреть" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="Button2Click"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                  <asp:TemplateField HeaderText="Детализация базового показателя по специальностям">
                        <ItemTemplate>
                            <asp:Button ID="Button3" runat="server" CommandName="Select" Text="Детализовать"  CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="Button3Click"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                 
                 <asp:TemplateField HeaderText="Цвет" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="Color"  runat="server" Visible="false" Text='<%# Bind("Color") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
        </asp:GridView>
     <br />
     <asp:Label ID="FormulaLable" runat="server" Text="FormulaLable" Visible="False"></asp:Label>
     <br />
     </asp:Content>
