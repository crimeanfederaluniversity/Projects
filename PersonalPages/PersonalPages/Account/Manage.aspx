<%@ Page Title="Учетные данные" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="PersonalPages.Account.Manage" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
     
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text=""></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text=""></asp:Label>
        <br />
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text=""></asp:Label>
        <br />
        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text=""></asp:Label>
        <br />
        <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text=""></asp:Label>
        <br />
        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text=""></asp:Label>
        <br />
        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text=""></asp:Label>
        <br />
        <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text=""></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text="Варианты написания Вашего имени (в Scopus, WebOfSince):"></asp:Label>
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Height="52px" Width="307px">
               <Columns>    
            <asp:TemplateField Visible="false"   HeaderText="Номер" >
                        <ItemTemplate>
                            <asp:Label ID="LabelID" runat="server"  Visible="false" Text='<%# Bind("ID") %>'  ></asp:Label> 
                               </ItemTemplate>
                    </asp:TemplateField> 
             <asp:TemplateField Visible="true"   HeaderText="Фамилия">
                        <ItemTemplate>
                            <asp:TextBox ID="FIO" runat="server"  Visible="true" Text='<%# Bind("FIO") %>'  ></asp:TextBox> 
                               </ItemTemplate>
                    </asp:TemplateField> 
                     <asp:TemplateField HeaderText="Удалить">
                        <ItemTemplate>  
                           <asp:Button ID="DeleteButton" runat="server" CommandName="Select" Text="Удалить" Width="200px" CommandArgument='<%# Eval("ID") %>' OnClick="DeleteButtonClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>               
                      </Columns>    
        </asp:GridView>
        <br />
        &nbsp;<asp:Button ID="AddRowButton" runat="server" OnClick="AddRowButton_Click" Text="Добавить строку" Width="150px" />
        &nbsp;
        <asp:Button ID="SaveFIOButton" runat="server" OnClick="SaveFIOButton_Click" Text="Сохранить" Width="150px" />
        <br />
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Сменить пароль" CssClass="form-control" />
        <br />
        <asp:Label ID="Label9" runat="server" Text="Текущий пароль" Visible="False"></asp:Label>
&nbsp;&nbsp;
        <asp:TextBox ID="TextBox1" TextMode="Password" runat="server" Visible="False" CssClass="form-control"></asp:TextBox>
        <br />
        <asp:Label ID="Label10" runat="server" Text="Новый пароль" Visible="False"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox2" TextMode="Password" runat="server" Visible="False" CssClass="form-control"></asp:TextBox>
        <br />
        <asp:Label ID="Label11" runat="server" Text="Подтверждение" Visible="False"></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox3" TextMode="Password" runat="server" Visible="False" CssClass="form-control"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" Text="Сохранить изменения" Visible="False" CssClass="form-control" OnClick="Button2_Click" />
        <br />
        <br />
        <br />
    
    </div>
</asp:Content>
