<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/Site.Master" CodeBehind="MultiUser.aspx.cs" Inherits="KPIWeb.MultiUser1" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">  


    <h1>Выберите под какой ролью войти в систему</h1>
    <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server">
                    <Columns> 
                        
                    <asp:BoundField DataField="Name" HeaderText="Роль" />

                    <asp:TemplateField HeaderText="Перейти">
                      <ItemTemplate>
                          <asp:Button ID="GoToMain" runat="server"  Text="Перейти" CommandArgument='<%# Eval("UserID") %>' Width="200px"  
                          OnClick="GoToMainClick" />           
                          <br />                                         
                       </ItemTemplate>                 
                 </asp:TemplateField>

                    </Columns> 
                </asp:GridView>

</asp:Content>