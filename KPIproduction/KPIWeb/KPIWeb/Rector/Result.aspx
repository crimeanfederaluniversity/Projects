<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Result.aspx.cs" Inherits="KPIWeb.Rector.Result" %>
 <asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
     
<style type="text/css">
   .button_right 
   {
       float:right
   }     
</style>

    <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
        <div>    
      <asp:Button ID="GoBackButton" runat="server" OnClick="GoBackButton_Click" Text="Назад" Width="125px" />
      <asp:Button ID="GoForwardButton" runat="server" OnClick="GoForwardButton_Click" Text="Вперед" Width="125px" />
        &nbsp; &nbsp; <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="На главную" Width="125px" />
        &nbsp; &nbsp;       
        <asp:Button ID="Button5" runat="server" CssClass="button_right" OnClick="Button5_Click" Text="Нормативные документы" Width="225px" />
        &nbsp; &nbsp;
        <asp:Button ID="Button6" runat="server" CssClass="button_right" OnClick="Button6_Click" Text="Button" Width="150px" />
        &nbsp; &nbsp;
        <asp:Button ID="Help" runat="server" CssClass="button_right"  Text="Помощь" Width="150px" />
        </div>

    </asp:Panel>

     <br />

     <br />
     <asp:Label ID="ReportTitle" runat="server" Text="ReportTitle" Font-Size="20pt"></asp:Label>
     <br />
     <asp:Label ID="PageFullName" runat="server" Text="PageFullName"></asp:Label>
     <br />
     --------------------------------------------------<br />
     <asp:TreeView ID="TreeView1" runat="server" NodeWrap="True" ShowExpandCollapse="False" ShowLines="True">
     </asp:TreeView>
     &nbsp;<asp:Button ID="Button7" runat="server" OnClick="Button7_Click" Text="Показать значение показателей по имеющимся данным" Width="558px" />
     <br />
<asp:GridView ID="Grid" runat="server" 
            AutoGenerateColumns="False"
            style="margin-top: 0px" OnSelectedIndexChanged="Grid_SelectedIndexChanged">
             <Columns>                
                 <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />    
                 <asp:BoundField DataField="Number"   HeaderText="Номер" Visible="true" />
                 <asp:BoundField DataField="Abb"   HeaderText="Аббревиатура" Visible="True" />
                 <asp:BoundField DataField="Name" HeaderText="" Visible="True" />          
                 <asp:BoundField DataField="StartDate" HeaderText="Начальная дата отчёта" Visible="True" />
                 <asp:BoundField DataField="EndDate" HeaderText="Конечная дата отчёта" Visible="True" />
                 <asp:BoundField DataField="Value" HeaderText="Значение" Visible="True" />
                 <asp:BoundField DataField="PlannedValue" HeaderText="Плановое значение" Visible="True" />
                 <asp:TemplateField HeaderText="Подтвердить данные">
                        <ItemTemplate>
                            <asp:Button ID="ConfirmButton" runat="server" CommandName="Select" Visible='<%# Bind("CanConfirm") %>' Text="Утвердить" CommandArgument='<%# Eval("ID") %>' Width="200px"  
                                OnClientClick="javascript:return confirm('Подтвердить значение?');" OnClick="ButtonConfirmClick"/>
                            <asp:Label ID="StatusLable"  runat="server" Visible='<%# Bind("ShowLable") %>' Text='<%# Eval("LableText") %>' ></asp:Label>
                             </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Разложение данных по структуре ">
                        <ItemTemplate>
                            <asp:Button ID="Button1" runat="server" CommandName="Select" Text="Разложить" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="Button1Click"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Разложение данных по формуле">
                        <ItemTemplate>
                            <asp:Button ID="Button2" runat="server" CommandName="Select" Text="Разложить" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="Button2Click"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                  <asp:TemplateField HeaderText="Разложение данных по специальностям">
                        <ItemTemplate>
                            <asp:Button ID="Button3" runat="server" CommandName="Select" Text="Разложить"  CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="Button3Click"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
        </asp:GridView>
       <br />
     <asp:Label ID="FormulaLable" runat="server" Text="FormulaLable" Visible="False"></asp:Label>
     <br />
       </asp:Content>
