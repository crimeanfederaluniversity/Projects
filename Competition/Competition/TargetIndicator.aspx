<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TargetIndicator.aspx.cs"  MasterPageFile="~/masterpage.Master" Inherits="Competition.TargetIndicator" %>

 <asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" >
     <h2>Целевые показатели</h2>
        <br />
        <asp:GridView ID="GridView2" AutoGenerateColumns="False" runat="server">
              <Columns>
                    <asp:BoundField DataField="ID_TargetIndicator" HeaderText="Код ЦП" Visible="false" />
                    <asp:BoundField DataField="TargetIndicator" HeaderText="Описание механизма достижения целевых показателей Программы развития ФГАОУ ВО «КФУ им. В.И. Вернадского» посредством реализации проекта:" Visible="true" />               
                  <asp:TemplateField Visible="true"   HeaderText="Ответ">
                        <ItemTemplate >
                         <asp:TextBox ID="PurchaseValue" style="text-align:center" BorderWidth="0" Height="95%" runat="server" Text='<%# Bind("PurchaseValue") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>  
                            
                    <asp:TemplateField Visible="false" HeaderText="Id" >
                        <ItemTemplate>
                        <asp:label ID="Id_Value" runat="server" Visible="false" Text='<%# Bind("Id_Value") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                 </Columns>
         </asp:GridView>
         <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Сохранить" />
 &nbsp;&nbsp;&nbsp;&nbsp;
     <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" style="height: 25px" Text="Далее" />
 </asp:Content>