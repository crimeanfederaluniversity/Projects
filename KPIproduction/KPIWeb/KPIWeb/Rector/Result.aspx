<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Result.aspx.cs" Inherits="KPIWeb.Rector.Result" %>
 <asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
     

    <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">        
      <asp:Button ID="GoBackButton" runat="server" OnClick="GoBackButton_Click" Text="Назад" Width="150px" />
      <asp:Button ID="GoForwardButton" runat="server" OnClick="GoForwardButton_Click" Text="Вперед" Width="150px" />
      <asp:Button ID="Button4" runat="server" Text="На главную" OnClick="Button4_Click" />     
        <asp:Button ID="Button5" runat="server" OnClick="Button4_Click" Text="Нормативные документы" Width="350px" />
        &nbsp;<asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="Button" />
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
     <asp:Label ID="PageName" runat="server" Text="PageName" Visible="False"></asp:Label>
     &nbsp;<asp:Label ID="SpecName" runat="server" Text="SpecName" Visible="False"></asp:Label>
     <br />
<asp:GridView ID="Grid" runat="server" 
            AutoGenerateColumns="False"
            style="margin-top: 0px">
             <Columns>                
                 <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />    
                 <asp:BoundField DataField="Number"   HeaderText="Номер" Visible="true" />
                 <asp:BoundField DataField="Abb"   HeaderText="Аббревиатура" Visible="True" />
                 <asp:BoundField DataField="Name" HeaderText="" Visible="True" />          
                 <asp:BoundField DataField="StartDate" HeaderText="Начальная дата отчёта" Visible="True" />
                 <asp:BoundField DataField="EndDate" HeaderText="Конечная дата отчёта" Visible="True" />
                 <asp:BoundField DataField="Value" HeaderText="Значение" Visible="True" />
                 
                 <asp:TemplateField HeaderText="Подтвердить данные">
                        <ItemTemplate>
                            <asp:Button ID="ConfirmButton" runat="server" CommandName="Select" Visible='<%# Bind("CanConfirm") %>' Text="Утвердить" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="ButtonConfirmClick"/>
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
