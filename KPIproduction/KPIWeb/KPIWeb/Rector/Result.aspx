<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Result.aspx.cs" Inherits="KPIWeb.Rector.Result" %>
 <asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
     <br />
     <asp:Label ID="ReportTitle" runat="server" Text="ReportTitle"></asp:Label>
     <br />
     <asp:Label ID="PageName" runat="server" Text="PageName"></asp:Label>
     <br />
     <asp:Label ID="SpecName" runat="server" Text="SpecName" Visible="False"></asp:Label>
     <br />
     <asp:Button ID="GoBackButton" runat="server" OnClick="GoBackButton_Click" Text="Назад" Width="150px" />
&nbsp;<asp:Button ID="GoForwardButton" runat="server" OnClick="GoForwardButton_Click" Text="Вперед" Width="150px" />
     <br />
     <br />
<asp:GridView ID="Grid" runat="server" 
            AutoGenerateColumns="False"
            style="margin-top: 0px">
             <Columns>                
                 <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />    
                 <asp:BoundField DataField="Number"   HeaderText="Номер" Visible="true" />
                 <asp:BoundField DataField="Name" HeaderText="" Visible="True" />          
                 <asp:BoundField DataField="StartDate" HeaderText="Начальная дата отчёта" Visible="True" />
                 <asp:BoundField DataField="EndDate" HeaderText="Конечная дата отчёта" Visible="True" />
                 <asp:BoundField DataField="Value" HeaderText="Значение" Visible="True" />

                    <asp:TemplateField HeaderText="Функционал 1">
                        <ItemTemplate>
                            <asp:Button ID="Button1" runat="server" CommandName="Select" Text="По структуре" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="Button1Click"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Функционал 2">
                        <ItemTemplate>
                            <asp:Button ID="Button2" runat="server" CommandName="Select" Text="По разложению" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="Button2Click"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                  <asp:TemplateField HeaderText="Функционал 3">
                        <ItemTemplate>
                            <asp:Button ID="Button3" runat="server" CommandName="Select" Text="Функционал 3"  CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="Button3Click"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
        </asp:GridView>
       </asp:Content>
