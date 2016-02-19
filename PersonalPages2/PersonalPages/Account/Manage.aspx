<%@ Page Title="Учетные данные" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="PersonalPages.Account.Manage" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
 

        <h2><asp:Label ID="Label2" Visible="False" runat="server" Text="Статус данных:"/></h2>
        <br />
&nbsp;E-mail:
        <asp:Label ID="Label14" runat="server" Text="Label" Font-Bold="True"></asp:Label>
        <br />
        <asp:TextBox ID="Email" runat="server"  Text="" Height="20px" Width="300px" Visible="False"  ></asp:TextBox>
        <br />
        &nbsp;Фамилия: <asp:Label ID="Label15" runat="server" Text="Label" Font-Bold="True"></asp:Label>
        <br />
        <asp:TextBox ID="Surname" runat="server"   Text=" " Height="20px" Width="300px" Visible="False"></asp:TextBox>
        <br />
        &nbsp;Имя:
        <asp:Label ID="Label16" runat="server" Text="Label" Font-Bold="True"></asp:Label>
        <br />
        <asp:TextBox ID="Name" runat="server"  Text=" " Height="20px" Width="300px" Visible="False"></asp:TextBox>
        <br />
        &nbsp;Отчество:
        <asp:Label ID="Label17" runat="server" Text="Label" Font-Bold="True"></asp:Label>
        <br />
        <asp:TextBox ID="Patronimyc" runat="server"   Text=" " Width="300px" Height="20px" Visible="False"></asp:TextBox>
        <br />
        &nbsp;<asp:Label ID="Label12" runat="server" Text="Label"></asp:Label>
        :
        <asp:Label ID="Label18" runat="server" Text="Label" Font-Bold="True"></asp:Label>
        <br />
        <asp:TextBox ID="PositionKurs" runat="server" Height="20px" Width="300px" Visible="False"/>
        <br />
        &nbsp;<asp:Label ID="Label13" runat="server" Text="Label"></asp:Label>
        :
        <asp:Label ID="Label19" runat="server" Text="Label" Font-Bold="True"></asp:Label>
        <br />
        <asp:TextBox ID="DegreeYear" runat="server" Height="20px" Width="300px" Visible="False"></asp:TextBox>
        <br />
        <br />
&nbsp;Ваше структурное подразделение:<br />
&nbsp;<asp:Label ID="Label20" runat="server" Text="Label" Font-Bold="True"></asp:Label>
&nbsp;
        <asp:Label ID="Label21" runat="server" Text="Label" Font-Bold="True"></asp:Label>
&nbsp;
        <asp:Label ID="Label22" runat="server" Text="Label" Font-Bold="True"></asp:Label>
        <br />
      
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Height="20px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="300px" Visible="False">
        </asp:DropDownList>
        &nbsp;<asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" Height="20px" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" Width="300px" Visible="False">
        </asp:DropDownList>
        &nbsp;<asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" Height="20px" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" Width="300px" Visible="False">
        </asp:DropDownList>
        <br />
        <asp:Button ID="Button5" runat="server" Text="Запрос на изменение  учетных данных" Width="475px" OnClick="Button5_Click" />
        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Запрос на изменение прав доступа к ИС" Width="475px" />
        <br />
        <asp:Button ID="SendChange" runat="server" OnClick="SendChange_Click" Text="Отправить запрос на изменение данных" Width="472px" Visible="False" />
        <br />
        <asp:Label ID="Label23" runat="server" Text="Здесь Вы можете добавить варианты написания Вашего имени на иностранных языках для возможности поиска Ваших научных публикаций в системах индексирования Scopus и WebOfSince." Visible="False" Font-Italic="True"></asp:Label>
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Height="52px" Width="506px" Visible="False">
               <Columns>    
            <asp:TemplateField Visible="false"   HeaderText="Номер" >
                        <ItemTemplate>
                            <asp:Label ID="LabelID" runat="server"  Visible="false" Text='<%# Bind("ID") %>'  ></asp:Label> 
                               </ItemTemplate>
                    </asp:TemplateField> 
             <asp:TemplateField Visible="true"   HeaderText="Фамилия и инициалы">
                        <ItemTemplate>
                            <asp:TextBox ID="FIO" runat="server"  Visible="true" Text='<%# Bind("FIO") %>'  ></asp:TextBox> 
                               </ItemTemplate>
                    </asp:TemplateField> 
                     <asp:TemplateField HeaderText="Удалить">
                        <ItemTemplate>  
                           <asp:Button ID="DeleteButton" runat="server" CommandName="Select" Text="Удалить вариант" Width="200px" CommandArgument='<%# Eval("ID") %>' OnClick="DeleteButtonClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>               
                      </Columns>    
        </asp:GridView>
        &nbsp;<br />
        <asp:Button ID="AddRowButton" runat="server" OnClick="AddRowButton_Click" Text="Добавить вариант " Width="299px" Visible="False" />
        &nbsp;
        <asp:Button ID="SaveFIOButton" runat="server" OnClick="SaveFIOButton_Click" Text="Сохранить варианты" Width="299px" Visible="False" />
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
        <asp:Button ID="Button2" runat="server" Text="Сохранить изменения" Visible="False" CssClass="form-control" OnClick="Button2_Click" />
        <br />
        <br />
        <br />
 
</asp:Content>
