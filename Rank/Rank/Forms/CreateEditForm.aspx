<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation ="false" CodeBehind="CreateEditForm.aspx.cs" Inherits="Rank.Forms.UserFillFormPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div style="font-size: medium">
        <asp:Button ID="Button1" runat="server" Text="Назад" OnClick="Button1_Click" />
        <br /> 
          <script src="calendar_ru.js" type="text/javascript"> </script>
         <br />
         <asp:Label ID="Label1" runat="server" Text="Label" Font-Bold="True"></asp:Label></span>&nbsp;<br />
         <br />
         <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack ="true" Height="30px" Width="225px" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
        </asp:DropDownList>
         <asp:Label ID="Label17" runat="server" Text="Label" Visible="False"></asp:Label>
         <br />
         <div id="TableDiv" runat="server">
        </div>
         <asp:Label ID="Label15" runat="server" Text="Сотрудники КФУ (зарегистрированные в системе пользователи)" Visible="False"></asp:Label>
        <br />
        <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server" OnRowDataBound ="OnRowDataBound"  >
            <Columns>
                 <asp:BoundField DataField="ID" HeaderText="" Visible="false" />
                <asp:TemplateField HeaderText="Код автора" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "false" >
                    <ItemTemplate>
                        <asp:Label ID="userid" runat="server" Text='<%# Bind("userid") %>'  Visible="false"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                </asp:TemplateField>
                   <asp:TemplateField HeaderText="Фамилия Имя Отчество" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                        <asp:Label ID="fio" runat="server" Text='<%# Bind("fio") %>'  Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                       <asp:TemplateField HeaderText="Академия" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                        <asp:Label ID="firstlvl" runat="server" Text='<%# Bind("firstlvl") %>'  Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                       <asp:TemplateField HeaderText="Институт/факультет" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                        <asp:Label ID="secondlvl" runat="server" Text='<%# Bind("secondlvl") %>'  Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                       <asp:TemplateField HeaderText="Кафедра" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                        <asp:Label ID="thirdlvl" runat="server" Text='<%# Bind("thirdlvl") %>'  Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 
                <asp:TemplateField HeaderText="Коэффициент сложности" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                         <asp:Label ID="point" runat="server" Text='<%# Bind("point") %>'  Visible="true"></asp:Label>     
                        <asp:DropDownList ID="ddlPoint" runat="server"  OnSelectedIndexChanged="ddlPoint_SelectedIndexChanged" Width ="300"> </asp:DropDownList>                        
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Сохранить">
                    <ItemTemplate>
                        <asp:Button ID="RankPointSaveButton" runat="server" CommandName="Select" Text="Сохранить" Width="150px" CommandArgument='<%# Eval("userid") %>'  OnClick="RankPointSaveButtonClik" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Удалить">
                    <ItemTemplate>
                        <asp:Button ID="Rank_DeleteAutorButton" runat="server" CommandName="Select" Text="Удалить" Width="150px" CommandArgument='<%# Eval("userid") %>' OnClientClick="return confirm('Вы уверены что хотите удалить автора?');" OnClick="Rank_DeleteAutorButtonClik" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
              <asp:Label ID="Label16" runat="server" Text="Не относятся к КФУ (нет в системе)" Visible="False"></asp:Label>
              <asp:GridView ID="GridView3" AutoGenerateColumns="false"  runat="server" Height="40px" Width="732px">
                <Columns>
                 <asp:BoundField DataField="ID" HeaderText="" Visible="false" />
                <asp:TemplateField HeaderText="Код автора" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "false" >
                    <ItemTemplate>
                        <asp:Label ID="userid" runat="server" Text='<%# Bind("notsystemuserid") %>'  Visible="false"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                </asp:TemplateField>
                       <asp:TemplateField HeaderText="Фамилия и инициалы" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                        <asp:Label ID="surname" runat="server" Text='<%# Bind("notsystemuserfio") %>'  Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>              
                <asp:TemplateField HeaderText="Коэффициент сложности" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                         <asp:Label ID="point" runat="server" Text='<%# Bind("notsystemuserpoint") %>'  Visible="True"></asp:Label>                        
                    </ItemTemplate>
                </asp:TemplateField>
                         <asp:TemplateField HeaderText="Удалить">
                    <ItemTemplate>
                        <asp:Button ID="DeleteNotSystemAutorButton" runat="server" CommandName="Select" Text="Удалить" Width="150px" CommandArgument='<%# Eval("notsystemuserid") %>' OnClientClick="return confirm('Вы уверены что хотите удалить автора?');" OnClick="DeleteNotSystemAutorButtonClick" />
                    </ItemTemplate>
                </asp:TemplateField>
                       </Columns>
         </asp:GridView>
         <br />
         <table>
             <tr>
                 <td>
         <asp:Panel ID="Panel2" runat="server" Width="550px" >
             <span style="font-size: medium"><asp:Label ID="Label2" runat="server" Text="Добавление человека (автора), не являющегося сотрудником КФУ :" Visible="False" Font-Bold="True"></asp:Label>
                 <span style="font-size: medium"> <asp:Label ID="Label14" runat="server" Text="Введите фамилию и инициалы: " Visible="False"></asp:Label>
                     <asp:TextBox ID="TextBox3" runat="server" Width="500px" Visible="False" ToolTip="Фамилия и инициалы человека не являющегося сотрудником КФУ" Height="20px"></asp:TextBox>
                      <asp:DropDownList ID="DropDownList6" runat="server" AutoPostBack="True" Height="20px"  Width="500px"  Visible="False">
        </asp:DropDownList>
         <asp:Button ID="AddNotSystemUserButton" runat="server" Text="Добавить" OnClick="AddNotSystemUserButtonClick" Visible="False" />
                     </asp:Panel>
                     </td>
                 <td>
         <asp:Panel ID="Panel1" runat="server" Width="550px">
               <asp:Label ID="Label13" runat="server" Text="Добавление человека (автора), являющегося сотрудником КФУ:" Visible="False" ForeColor="Black" Font-Bold="True"></asp:Label>
             <br /><asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" Height="20px"  Width="500px" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" Visible="False">
        </asp:DropDownList>
             <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" Height="20px"  Width="500px" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged" Visible="False">
        </asp:DropDownList>
              <asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="True" Height="20px" Width="500px" Visible="False" >
        </asp:DropDownList>
               <asp:Label ID="Label12" runat="server" Text="Введите фамилию: " Visible="False"></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server" Width="276px" Visible="False" Height="20px"></asp:TextBox>
        &nbsp;&nbsp; <asp:Button ID="NewAuthorButton" runat="server" Text="Поиск" OnClick="NewAuthorButtonClick" Visible="False" />
         </asp:Panel>
                     </td>
                 </tr>
             </table>
         <span class="auto-style2"><span style="font-size: medium">
     <asp:Label ID="searchError" runat="server" Font-Size="Medium" ForeColor="Red" Text="Пользователь не найден!" Visible="False"></asp:Label>
         <asp:GridView ID="GridView2" AutoGenerateColumns="false"  runat="server" Height="40px" Width="732px">
                <Columns>
                 <asp:BoundField DataField="ID" HeaderText="" Visible="false" />
                <asp:TemplateField HeaderText="Код автора" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                        <asp:Label ID="userid" runat="server" Text='<%# Bind("userid") %>'  Visible="True"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                </asp:TemplateField>
                       <asp:TemplateField HeaderText="Фамилия" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                        <asp:Label ID="surname" runat="server" Text='<%# Bind("surname") %>'  Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:TemplateField HeaderText="Имя" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                        <asp:Label ID="name" runat="server" Text='<%# Bind("name") %>'  Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:TemplateField HeaderText="Отчество" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                        <asp:Label ID="patronimyc" runat="server" Text='<%# Bind("patronimyc") %>'  Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:TemplateField HeaderText="Должность" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                        <asp:Label ID="position" runat="server" Text='<%# Bind("position") %>'  Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                       <asp:TemplateField HeaderText="Добавить соавтора">
                    <ItemTemplate>
                        <asp:Button ID="AddAutorButton" runat="server" CommandName="Select" Text="Добавить" Width="150px" CommandArgument='<%# Eval("userid") %>' OnClick="AddAutorButtonClik" />
                    </ItemTemplate>
                </asp:TemplateField>
                       </Columns>
         </asp:GridView>
        </span>
         </span>
        <br />
        <asp:Button ID="SaveButton" runat="server" Text="Сохранить" OnClick="SaveButtonClick" Height="35px" Width="200px" />
        &nbsp;&nbsp;&nbsp; <asp:Button ID="SendButton" runat="server" Text="Отправить" OnClick="SendButtonClick" Height="35px" Width="200px" />

    </div>
     </span></span>
</asp:Content>
