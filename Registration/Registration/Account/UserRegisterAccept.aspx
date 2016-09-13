<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserRegisterAccept.aspx.cs" Inherits="Registration.Account.FormUserPublication" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Заявки на регистрацию в системе &quot;Рейтинги научно-педагогических работников КФУ&quot;. </h3>
    <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server"  OnRowDataBound ="GVRowDataBound" >
        <Columns>    
            <asp:TemplateField HeaderText="Код автора" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "false" >
                <ItemTemplate>
                    <asp:Label ID="userid" runat="server" Text='<%# Bind("userid") %>'  Visible="false"></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="ФИО" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                <ItemTemplate>
                    <asp:Label ID="fio" runat="server" Text='<%# Bind("fio") %>'  Visible="True"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>  
                 <asp:TemplateField HeaderText="Email" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                <ItemTemplate>
                    <asp:Label ID="email" runat="server" Text='<%# Bind("email") %>'  Visible="True"></asp:Label>
                </ItemTemplate>
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
                 <asp:TemplateField HeaderText="Должность" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                <ItemTemplate>
                    <asp:Label ID="position" runat="server" Text='<%# Bind("position") %>'  Visible="True"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Ставка" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                <ItemTemplate>
                    <asp:Label ID="stavka" runat="server" Text='<%# Bind("stavka") %>'  Visible="True"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                      <asp:TemplateField HeaderText="Ученая степень" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                <ItemTemplate>
                    <asp:Label ID="degree" runat="server" Text='<%# Bind("degree") %>'  Visible="True"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
           <asp:TemplateField HeaderText="Изменить">
                <ItemTemplate>
                    <asp:Button ID="EditButton" runat="server" CommandName="Select" Text="Изменить" Width="150px" CommandArgument='<%# Eval("userid") %>'  OnClick="EditButtonClick" />
                </ItemTemplate>
            </asp:TemplateField> 
            <asp:TemplateField HeaderText="Подтвердить">
                <ItemTemplate>
                    <asp:Button ID="GoButton" runat="server" CommandName="Select" Text="Подтвердить" Width="150px" CommandArgument='<%# Eval("userid") %>'  OnClick="GoButtonClik" />
                </ItemTemplate>
            </asp:TemplateField>   
                       <asp:TemplateField HeaderText="Отклонить">
                <ItemTemplate>
                    <asp:Button ID="FailButton" runat="server" CommandName="Select" Text="Отклонить" Width="150px" CommandArgument='<%# Eval("userid") %>'  OnClick="FailButtonClick" />
                </ItemTemplate>
            </asp:TemplateField>     
        </Columns>
    </asp:GridView>
     <div id="coomentEndP" style="visibility: hidden; background-color: blanchedalmond">
        <asp:Panel runat="server" ID="comment_panel" Visible="true">
            <asp:Label ID="Label11" runat="server" Text="Напишите причину отказа:" Font-Bold="True"></asp:Label>
            <div id="loop"style="visibility: hidden; height: 0px; width: 0px">
                <asp:TextBox ID="textBoxId" runat="server"></asp:TextBox>
            </div>
            <br/>
            <br/>
            <asp:TextBox ID="commentTextBox" TextMode="MultiLine" placeholder="Комментарий..." Height="270" Width="885px" runat="server"></asp:TextBox>
            <br/>
            <div>
            <asp:Button ID="Button5" runat="server" Text="Отправить" OnClientClick="showLoadingScreenWithText('Письмо отправлено!');" OnClick="Button5_Click"/>
                <asp:Button ID="Button6" runat="server" OnClientClick="document.getElementById('coomentEndP').style.visibility='hidden'; return false;" Text="Отменить" />
           </div>
        </asp:Panel>
    </div>
</asp:Content>
