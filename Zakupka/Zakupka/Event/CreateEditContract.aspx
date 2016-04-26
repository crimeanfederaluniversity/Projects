<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="CreateEditContract.aspx.cs" Inherits="Zakupka.Event.CreateEditContract" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
    <asp:Button ID="Back" runat="server" Text="Назад" CssClass="btn btn-default" OnClick="Back_Click" />
     
           <h2> <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>  </h2>
     
     <script src="calendar_ru.js" type="text/javascript"> </script>
    <div id="TableDiv" runat="server">
        </div>
    <br />
  
    <asp:DropDownList ID="CostClass" runat="server" Height="20px"   Visible="true" Width="400px">
    </asp:DropDownList>
    <br />
    <br />
     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="877px" >
          <Columns>    
                 <asp:TemplateField Visible="false"   HeaderText="">
                        <ItemTemplate>
                            <asp:Label ID="Id" runat="server"  Visible="false" Text='<%# Bind("Id") %>'   ></asp:Label> 
                               </ItemTemplate>
                    </asp:TemplateField> 
              <asp:TemplateField Visible="false"   HeaderText="Вид КОСГУ" >
                        <ItemTemplate>         
                            <asp:Label ID="kosgutype" runat="server"  Visible="false" Text='<%# Bind("type") %>'   ></asp:Label>                                              
                               </ItemTemplate>
                    </asp:TemplateField> 
            <asp:TemplateField Visible="true"   HeaderText="КОСГУ" >
                        <ItemTemplate>         
                            <asp:Label ID="kosgu" runat="server"  Visible="true" Text='<%# Bind("kosgu") %>'   ></asp:Label>                   
                               </ItemTemplate>
                    </asp:TemplateField> 
             <asp:TemplateField Visible="true"   HeaderText="Cумма">
                        <ItemTemplate>
                            <asp:TextBox ID="Sum" runat="server"  Visible="true" Text='<%# Bind("Sum") %>'    ></asp:TextBox> 
                               </ItemTemplate>
                    </asp:TemplateField> 
               <asp:TemplateField HeaderText="Сохранить">
                        <ItemTemplate>  
                           <asp:Button ID="SaveButton" runat="server" CommandName="Select" Text="Сохранить" Width="150px" CommandArgument='<%# Eval("Id") %>' OnClick="SaveButtonClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>   
                     <asp:TemplateField HeaderText="Удалить">
                        <ItemTemplate>  
                           <asp:Button ID="DeleteButton" runat="server" CommandName="Select" Text="Удалить" Width="150px" CommandArgument='<%# Eval("Id") %>' OnClick="DeleteButtonClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>               
                      </Columns>    
    </asp:GridView>
    <br />
    Добавить новый КОСГУ:<br />
     <asp:DropDownList ID="kosgutype" runat="server"  Visible="true"    AutoPostBack="true" OnSelectedIndexChanged="kosgutype_SelectedIndexChanged" Height="20px" Width="300px" >   
                                <asp:ListItem  Selected ="true" Value="0">Выберите вид КОСГУ</asp:ListItem>
                                  <asp:ListItem  Value="1">Заработная плата</asp:ListItem>
                                  <asp:ListItem Value="2">Прочие выплаты</asp:ListItem>
                                <asp:ListItem Value="3">Начисления на выплаты по оплате труда</asp:ListItem>
                                <asp:ListItem Value="4">Услуги связи</asp:ListItem>
                                <asp:ListItem Value="5">Транспортные услуги</asp:ListItem>
                                <asp:ListItem Value="6">Коммунальные услуги</asp:ListItem>
                                <asp:ListItem Value="7">Арендная плата за пользование имуществом</asp:ListItem>
                                  <asp:ListItem Value="8">Работы, услуги по содержанию имущества</asp:ListItem>
                                <asp:ListItem Value="9">Прочие работы, услуги</asp:ListItem>
                                <asp:ListItem Value="10">Прочие расходы</asp:ListItem>
                                 <asp:ListItem Value="11">Увеличение стоимости основных средств</asp:ListItem>
                                 <asp:ListItem Value="12">Увеличение стоимости материальных запасов</asp:ListItem>    
                            </asp:DropDownList>
    &nbsp;
    <asp:DropDownList ID="kosgu"  runat="server" Visible="true" AutoPostBack="true" Height="20px" Width="400px">   </asp:DropDownList>
    &nbsp;<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>

&nbsp;<asp:Button ID="AddRowButton" runat="server" OnClick="AddRowButton_Click" Text="+"     />
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="kosguError" runat="server" Font-Size="X-Small" ForeColor="Red" Text="Превышает КОСГУ по проекту!" Visible="False"></asp:Label>
    <br />
 
    <!-- javascript:Page_ClientValidate(); if (Page_IsValid==true) { this.disabled=true; } -->
    <asp:Button ID="Button1" runat="server" Text="Сохранить" CssClass="btn btn-default"  OnClientClick="showSimpleLoadingScreen();"  OnClick="Button1_Click" />
</asp:Content>
