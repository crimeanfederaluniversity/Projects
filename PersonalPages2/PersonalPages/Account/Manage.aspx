<%@ Page Title="Учетные данные" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="PersonalPages.Account.Manage" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>

      

        <h2>Ваши учетные данные:</h2>
          <asp:Label ID="Label2" Visible="False" runat="server" Text="Статус данных:"/>
        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Права доступа" Width="207px" />
        <br />
&nbsp;E-mail:<br />
        <asp:TextBox ID="Text4" runat="server"  Text="" Height="20px" Width="300px"  ></asp:TextBox>
        <br />
        &nbsp;Фамилия:<br />
        <asp:TextBox ID="Text2" runat="server"   Text=" " Height="20px" Width="300px"></asp:TextBox>
        <br />
        &nbsp;Имя:<br />
        <asp:TextBox ID="Text1" runat="server"  Text=" " Height="20px" Width="300px"></asp:TextBox>
        <br />
        &nbsp;Отчество:<br />
        <asp:TextBox ID="Text3" runat="server"   Text=" " Width="300px" Height="20px"></asp:TextBox>
        <br />
        <br />
&nbsp;Укажиете Ваше структурное подразделение:<br />
      
        &nbsp;<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Height="20px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="300px">
        </asp:DropDownList>
        &nbsp;<asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" Height="20px" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" Width="300px">
        </asp:DropDownList>
        &nbsp;<asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" Height="20px" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" Width="300px">
        </asp:DropDownList>
        <br />
&nbsp;<br />
        <asp:TextBox ID="Text5" runat="server" Height="20px" Width="300px"/>
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Отправить запрос на изменение данных" Width="612px" />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server"   Text="Варианты написания Вашего имени (в Scopus, WebOfSince):" Width="824px"/>
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
        &nbsp;<asp:Button ID="AddRowButton" runat="server" OnClick="AddRowButton_Click" Text="Добавить строку" Width="299px" />
        &nbsp;
        <asp:Button ID="SaveFIOButton" runat="server" OnClick="SaveFIOButton_Click" Text="Сохранить" Width="299px" />
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
