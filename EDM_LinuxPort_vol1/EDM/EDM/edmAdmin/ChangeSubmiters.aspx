<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeSubmiters.aspx.cs" MasterPageFile="~/Site.Master" Inherits="EDM.edmAdmin.ChangeSubmiters" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <span style="font-size: large">Редактирование пользователей, имеющих доступ к отчетам по распечатке </span>
    <asp:Button ID="Button2" runat="server" Height="25px" OnClick="Button2_Click" Text="На главную" Width="124px" />
    <br />
&nbsp;<asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server" CssClass="table edm-table edm-history-table centered-block" Height="215px" Width="1427px">           
            
         <Columns>               
                           
                 <asp:TemplateField HeaderText="ID пользователя" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField>   
                                   
                    <asp:TemplateField HeaderText="Адрес почты" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Email" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                        </ItemTemplate>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 

                 <asp:TemplateField HeaderText="Удалить пользователя">
                        <ItemTemplate>
                            <asp:Button ID="DeleteUserButton" runat="server" CommandName="Select" Text="Удалить" Width="200px" CommandArgument='<%# Eval("DeleteUser") %>' OnClick="DeleteUserButtonClick" />
                        </ItemTemplate>
                    </asp:TemplateField>   
             </Columns>
         </asp:GridView>

     <br />
    Укажите e-mail для нового пользователя&nbsp;&nbsp; <asp:TextBox runat="server" ID="AddNewUser" CssClass="form-control" Height="22px" Width="689px" />
    <br />
    <asp:Button ID="Button1" runat="server" Height="27px" Text="Добавить" Width="201px" OnClick="Button1_Click" />
    </asp:Content>