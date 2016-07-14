<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation ="false" CodeBehind="CreateEditForm.aspx.cs" Inherits="Rank.Forms.UserFillFormPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div style="font-size: medium">
        <asp:Button ID="Button1" runat="server" Text="Назад" OnClick="Button1_Click" />
        <br /> 
          <script src="calendar_ru.js" type="text/javascript"> </script>
         <br />
         <asp:Label ID="Label1" runat="server" Text="Label" Font-Bold="True"></asp:Label></span>&nbsp;<br />
         <asp:Label ID="Label11" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
         <br />
         <span style="font-size: medium">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><br />
         <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack ="false" Height="30px" Width="225px" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;<br />
        &nbsp;<div id="TableDiv" runat="server">
        </div>
        <br />
        <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server" OnRowDataBound ="OnRowDataBound"  >
            <Columns>
                 <asp:BoundField DataField="ID" HeaderText="" Visible="false" />
                <asp:TemplateField HeaderText="Код автора" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                        <asp:Label ID="userid" runat="server" Text='<%# Bind("userid") %>'  Visible="True"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                </asp:TemplateField>
                       <asp:TemplateField HeaderText="Структурное подразделение/филиал" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
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
                <asp:TemplateField HeaderText="ФИО" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                        <asp:Label ID="fio" runat="server" Text='<%# Bind("fio") %>'  Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>     
                <asp:TemplateField HeaderText="Коэффициент сложности" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                         <asp:Label ID="point" runat="server" Text='<%# Bind("point") %>'  Visible="false"></asp:Label>     
                        <asp:DropDownList ID="ddlPoint" runat="server"  OnSelectedIndexChanged="ddlPoint_SelectedIndexChanged" > </asp:DropDownList>                        
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Сохранить">
                    <ItemTemplate>
                        <asp:Button ID="RankPointSaveButton" runat="server" CommandName="Select" Text="Сохранить" Width="150px" CommandArgument='<%# Eval("userid") %>'  OnClick="RankPointSaveButtonClik" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Удаление автора">
                    <ItemTemplate>
                        <asp:Button ID="Rank_DeleteAutorButton" runat="server" CommandName="Select" Text="Удалить" Width="150px" CommandArgument='<%# Eval("userid") %>' OnClientClick="return confirm('Вы уверены что хотите удалить автора?');" OnClick="Rank_DeleteAutorButtonClik" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
         <asp:Label ID="Label13" runat="server" Text="Прикрепить пользователя:" Visible="False"></asp:Label>
         <span style="font-size: medium"><br /><asp:Label ID="Label2" runat="server" Text="Академия" Visible="False"></asp:Label>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label3" runat="server" Text="Факультет" Visible="False"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:Label ID="Label4" runat="server" Text="Кафедра" Visible="False"></asp:Label>
&nbsp;&nbsp;&nbsp;</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; 
         <asp:Label ID="Label12" runat="server" Text="Введите фамилию соавтора: " Visible="False"></asp:Label>
         <span class="auto-style2"><span style="font-size: medium">
        &nbsp;<asp:Label ID="searchError" runat="server" Font-Size="X-Small" ForeColor="Red" Text="Пользователь не найден!" Visible="False"></asp:Label>
        </span>
         </span>
        <br /><asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" Height="25px"  Width="250px" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" Visible="False">
        </asp:DropDownList>
&nbsp;&nbsp;<asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" Height="25px"  Width="250px" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged" Visible="False">
        </asp:DropDownList>
&nbsp;&nbsp;<asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="True" Height="25px" Width="250px" Visible="False" >
        </asp:DropDownList>
        &nbsp;
        <asp:TextBox ID="TextBox2" runat="server" Width="200px" Visible="False"></asp:TextBox>
        &nbsp;&nbsp; <asp:Button ID="NewAuthorButton" runat="server" Text="Поиск" OnClick="NewAuthorButtonClick" Visible="False" />
         <asp:CheckBox ID="CheckBox1" runat="server" Text="Нет в системе" Font-Size="Small" Visible="False" />
&nbsp;
         <asp:Button ID="AddNotSystemUserButton" runat="server" Text="Добавить" OnClick="AddNotSystemUserButtonClick" Visible="False" />
        <br />
         <span class="auto-style2"><span style="font-size: medium">
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
        </span></span>
        <br />
        <asp:Button ID="SaveButton" runat="server" Text="Сохранить" OnClick="SaveButtonClick" Height="35px" Width="200px" />
        &nbsp;&nbsp;&nbsp; <asp:Button ID="SendButton" runat="server" Text="Отправить" OnClick="SendButtonClick" Height="35px" Width="200px" />

    </div>
</asp:Content>
