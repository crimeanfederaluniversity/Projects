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
        height:200px;    
        border-color: black;
        border-width: medium;
        background-color:beige;
        z-index:3;
        right: 0;
    }
   .commentSectionStyle
    {
        position:fixed;    
        width:400px;
        height:400px;    
        background-color:beige;
        z-index:3;
        top: 50%;
        left: 50%;
        margin-top: -200px;
        margin-left: -200px;
       visibility: hidden;
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
       width: 390px;
       height: 340px;
       max-width: 400px;
       max-height: 340px;
       margin-top: 5px;
       margin-left: 5px;
       
   }
</style>     
         
        <script type="text/javascript" language="javascript">
            function DoPostBack()
            {
                __doPostBack('cmd', 'thesearemyarguments');
            }
        </script>

        <script type="text/javascript">
                 var y = 0;
                 function legendChange()
                 {            
                     if (document.getElementById('sidePanel').style.width == '20px')
                     {
                         document.getElementById('sidePanel').style.width = '200px';
                     }
                     else
                     {
                         document.getElementById('sidePanel').style.width = '20px';
                     }
                 }
                 function closeCommentSection()
                 {
                     document.getElementById('comment_Section').style.visibility = 'hidden';
                 }
                 function showCommentSection(i)
                 {
                     document.getElementById('comment_Section').style.visibility = 'visible';
                     y = i;
                     return false;
                 }
                 function showAlert()
                 {
                     alert(y);
                 }
                 function commentSendButtonClick()
                 {
                     __doPostBack( 'ButtonClickParam', y );
                 }
        </script>
        <div id="sidePanel" class='side_legend' onclick="legendChange()">    
            <button type="button" onclick="legendChange()">
            X</button>
        </div>  
        <div id="comment_Section" class='commentSectionStyle'>    
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
                &nbsp; &nbsp;
                <asp:Button ID="Button6" runat="server" CssClass="button_right" OnClick="Button6_Click" Text="Button" Width="200px" />
                &nbsp; &nbsp;
                </div>

            </asp:Panel>
     <br /><br /><br />
     <asp:Label ID="ReportTitle" runat="server" Text="ReportTitle" Font-Size="20pt"></asp:Label>
     <br />
     <asp:Label ID="PageFullName" runat="server" Text="PageFullName" Font-Size="15pt"></asp:Label>
     <br />
     --------------------------------------------------<br />
     <asp:TreeView ID="TreeView1" runat="server" NodeWrap="True" ShowExpandCollapse="False" ShowLines="True">
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
