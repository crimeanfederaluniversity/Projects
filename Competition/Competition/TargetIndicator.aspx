<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TargetIndicator.aspx.cs"  MasterPageFile="~/masterpage.Master" Inherits="Competition.TargetIndicator" %>

 <asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" >
    <div>
    
    </div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Загрузить" />
        <br />
        <br />
         <asp:GridView ID="GridView2" AutoGenerateColumns="False" runat="server">
              <Columns>
                    <asp:BoundField DataField="ID_TargetIndicator" HeaderText="Код ЦП" Visible="false" />
                    <asp:BoundField DataField="TargetIndicator" HeaderText="Целевой показатель" Visible="true" />               
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
 </asp:Content>