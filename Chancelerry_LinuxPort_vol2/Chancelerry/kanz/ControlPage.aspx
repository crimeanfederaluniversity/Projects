<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ControlPage.aspx.cs" Inherits="Chancelerry.kanz.ControlPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <script src="toggleLoadingScreen.js" type="text/javascript"></script>
    <script src="calendar_ru.js" type="text/javascript"></script>
    
    <script>
        function onPageSubmit()
        {
            this.action += top.location.hash;
        }
    </script>

    <style type="text/css">
          
           .resultTable {
               border-collapse: collapse; 
           }
          .resultTable TH, TD {
              border: 1px solid black; 
              text-align: left; 
              padding: 4px; 
          }
          .resultTable TH {
              background: #fc0; 
              height: 40px; 
              vertical-align: bottom; 
              padding: 0;
          }

          .bad1 {
             color: red;
          }
          .bad2 {
              color: orange;
          }
          .good1 {
              color: green;
          }
          .good2 {
              color: black;
          }


      </style>

                  <div class="menu1">
                      <br />
                      <br id="tab3"/>
                      <a href="#tab1">Шаблон 1</a><a href="#tab2">Шаблон 2</a><a href="#tab3">Шаблон 3</a>
                      <div>    
                       <asp:DropDownList ID="T2ListOfIncomingDropDownList" runat="server" Height="20px" Width="400px"></asp:DropDownList>
                      <br />
                      <br />
                      <asp:TextBox ID="T2CntlFilterStartDateTextBox" runat="server" Height="20px" Width="400px" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)" placeholder="Начальная дата контроля (включительно)"></asp:TextBox>
                      <br />
                      <br />
                      <asp:TextBox ID="T2CntrlFilterEndDateTextBox" runat="server" Height="20px" Width="400px" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)" placeholder="Конечнпая дата контроля (включительно)"></asp:TextBox>
                      <br />
                      <br />
                      <asp:TextBox ID="T2CompareDateTextBox" runat="server" Height="20px" Width="400px" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)" placeholder="Дата для сравнения"></asp:TextBox>
                      <br />
                      <asp:RadioButtonList ID="T2RadioButtonList" runat="server" Visible="False" Height="100px" Width="400px">
                          <asp:ListItem Selected="True" Value="0">Неисполненные (срок прошел)</asp:ListItem>
                          <asp:ListItem Value="1">Неисполненные (срок не прошел)</asp:ListItem>
                          <asp:ListItem Value="2">Исполненные с нарушением срока</asp:ListItem>
                          <asp:ListItem Value="3">Исполненные без нарушения срока</asp:ListItem>
                        </asp:RadioButtonList>     
                      <br />
                      <asp:LinkButton ID="T2CreateTableButton" runat="server" Text="Показать" Height="20px" Width="400px" OnClick="T2CreateTableButton_Click"  />      
                      <br />
                      <br />
                          <div id="T2ResultDiv" runat="server">      
                          </div>
                      </div>
                      <div>
                          
                          
                     

                      </div>
                      <div>
                          <asp:DropDownList ID="T1ListOfIncomingDropDownList" runat="server" Height="20px" Width="400px"></asp:DropDownList>
                      <br />
                      <br />
                      <asp:TextBox ID="T1StartDateTextBox" runat="server" Height="20px" Width="400px" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)" placeholder="Начальная дата поступления документа (включительно)"></asp:TextBox>
                      <br />
                      <br />
                      <asp:TextBox ID="T1EndDateTextBox" runat="server" Height="20px" Width="400px" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)" placeholder="Конечнпая дата поступления документа(включительно)"></asp:TextBox>
                      <br />
                      <br />
                      <asp:TextBox ID="T1CompareDateTextBox" runat="server" Height="20px" Width="400px" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)" placeholder="Дата для сравнения"></asp:TextBox>
                      <br />
                      <asp:RadioButtonList ID="T1RadioButtonList1" runat="server" Visible="False" Height="100px" Width="400px">
                          <asp:ListItem Selected="True" Value="0">Неисполненные (срок прошел)</asp:ListItem>
                          <asp:ListItem Value="1">Неисполненные (срок не прошел)</asp:ListItem>
                          <asp:ListItem Value="2">Исполненные с нарушением срока</asp:ListItem>
                          <asp:ListItem Value="3">Исполненные без нарушения срока</asp:ListItem>
                        </asp:RadioButtonList>     
                      <br />
                      <asp:LinkButton ID="T1CreateTableButton" runat="server" Text="Показать" Height="20px" Width="400px" OnClick="T1CreateTableButton_Click" />      
                      <br />
                      <br />
                          <div id="T1ResultDiv" runat="server">      
                          </div>
                      </div>
                      </div>  
                      <style>
                  #tab2, #tab3 {position: fixed; }

                  .menu1 > a,
                  .menu1 #tab2:target ~ a:nth-of-type(1),
                  .menu1 #tab3:target ~ a:nth-of-type(1),
                  .menu1 > div { padding: 5px; border: 1px solid #aaa; }

                  .menu1 > a { line-height: 28px; background: #fff; text-decoration: none; }

                  #tab2,
                  #tab3,
                  .menu1 > div,
                  .menu1 #tab2:target ~ div:nth-of-type(1),
                  .menu1 #tab3:target ~ div:nth-of-type(1) {display: none; }

                  .menu1 > div:nth-of-type(1),
                  .menu1 #tab2:target ~ div:nth-of-type(2),
                  .menu1 #tab3:target ~ div:nth-of-type(3) { display: block; }

                  .menu1 > a:nth-of-type(1),
                  .menu1 #tab2:target ~ a:nth-of-type(2),
                  .menu1 #tab3:target ~ a:nth-of-type(3) { border-bottom: 2px solid #fff; }
                  </style>
                      </asp:Content>
                  <asp:Content ID="Content2" ContentPlaceHolderID="TableContent" runat="server">
</asp:Content>
              