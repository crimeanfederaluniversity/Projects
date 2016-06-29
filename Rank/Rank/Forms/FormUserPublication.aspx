<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormUserPublication.aspx.cs" Inherits="Rank.Forms.FormUserPublication" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="Button1" runat="server" Text="Назад" OnClick="Button1_Click" />
    <br />Добавление/редактирование для показателя: 
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </span>&nbsp;<br />
    <br />
    <asp:Label ID="Label10" runat="server" Text="Выберите тип публикации"></asp:Label>
&nbsp;&nbsp;
         <span style="font-size: medium">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>
    <br />
    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" Height="30px" Width="225px" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
    </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;<br />&nbsp;<div id="TableDiv" runat="server">
    </div>
    <br />Авторы:     
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
                    <asp:DropDownList ID="ddlPoint" runat="server"  OnSelectedIndexChanged="ddlPoint_SelectedIndexChanged" >
                    </asp:DropDownList>
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
    <span class="auto-style2"><span style="font-size: medium">Добавление нового автора/соавтора:</span></span><span style="font-size: medium"><br />
    <asp:Label ID="Label2" runat="server" Text="Академия"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label3" runat="server" Text="Факультет"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label4" runat="server" Text="Кафедра"></asp:Label>
&nbsp;&nbsp;&nbsp;</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span class="auto-style2"><span style="font-size: medium">Введите фамилию соавтора: 
        &nbsp;<asp:Label ID="searchError" runat="server" Font-Size="X-Small" ForeColor="Red" Text="Пользователь не найден!" Visible="False"></asp:Label>
    </span></span>
    <br />
    <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" Height="25px"  Width="250px" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
    </asp:DropDownList>
&nbsp;&nbsp;<asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" Height="25px"  Width="250px" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged">
    </asp:DropDownList>
&nbsp;&nbsp;<asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="True" Height="25px" Width="250px" >
    </asp:DropDownList>
        &nbsp;
        <asp:TextBox ID="TextBox2" runat="server" Width="200px"></asp:TextBox>
        &nbsp;&nbsp; 
    <asp:Button ID="NewAuthorButton" runat="server" Text="Поиск" OnClick="NewAuthorButtonClick" />
    <br />
    <asp:CheckBox ID="CheckBox1" runat="server" Text="Нет в системе" Font-Size="Small" />
&nbsp;
         <asp:Button ID="AddNotSystemUserButton" runat="server" Text="Добавить" OnClick="AddNotSystemUserButtonClick" />
    <br />Результаты поиска:<span class="auto-style2"><span style="font-size: medium">
    <asp:GridView ID="GridView2" AutoGenerateColumns="false"  runat="server">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="" Visible="false" />
            <asp:TemplateField HeaderText="Код автора" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                <ItemTemplate>
                    <asp:Label ID="userid0" runat="server" Text='<%# Bind("userid") %>'  Visible="True"></asp:Label>
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
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </span></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<br />Пожалуйста прикрепите лист с библиографическими данными издания<asp:FileUpload ID="FileUpload1" runat="server" Width="500px" />
         Пожалуйста прикрепите скан выписки<asp:FileUpload ID="FileUpload2" runat="server" Width="500px" />
    <br />
    <br />
    <asp:Button ID="SaveButton" runat="server" Text="Сохранить" OnClick="SaveButtonClick" Height="35px" Width="200px" />
        &nbsp;&nbsp;&nbsp; 
    <asp:Button ID="SendButton" runat="server" Text="Отправить" OnClick="SendButtonClick" Height="35px" Width="200px" />
</asp:Content>
