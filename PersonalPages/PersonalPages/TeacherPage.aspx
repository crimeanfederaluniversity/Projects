<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" EnableEventValidation="false" CodeBehind="TeacherPage.aspx.cs" Inherits="PersonalPages.TeacherPage" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Назад" Width="304px" />
        <span style="font-size: x-large">
        <br />
        <br />
        Страница преподавателя<br />
        </span><span style="font-size: large">Мои группы:</span><span style="font-size: x-large"><br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Height="52px" Width="307px">
               <Columns>    
            <asp:TemplateField  HeaderText="Код" >
                        <ItemTemplate>
                            <asp:Label ID="ID" runat="server"  Visible="True" Text='<%# Bind("ID") %>'  ></asp:Label> 
                               </ItemTemplate>
                    </asp:TemplateField> 
             <asp:TemplateField Visible="true"   HeaderText="Название">
                        <ItemTemplate>
                            <asp:Label ID="Name" runat="server"  Visible="true" Text='<%# Bind("Name") %>'  ></asp:Label> 
                               </ItemTemplate>
                    </asp:TemplateField> 
                   <asp:TemplateField HeaderText="Ведомость" Visible="false">
                        <ItemTemplate>  
                           <asp:Button ID="WatchButton" runat="server" CommandName="Select" Text="Скачать" Width="200px" CommandArgument='<%# Eval("WatchButton") %>' OnClick="WatchButtonClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>              
                      </Columns>    
        </asp:GridView>
        Название группы:<br />
        <asp:TextBox ID="TextBox1" runat="server" Width="360px"></asp:TextBox>
        <br />
        Название для файла и дисциплина (задание, ведомость...)<br />
        <asp:TextBox ID="TextBox2" runat="server" Width="360px"></asp:TextBox>
        <br />
        <br />
        Выберите файл<asp:FileUpload ID="FileUpload3" runat="server" Height="32px" Width="463px" />
        <br />
        Выполнить до:<br />
        <asp:TextBox ID="TextBox3" runat="server" TextMode="Date" Width="244px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" OnClick="Button11_Click" Text="Сохранить" Width="304px" />
        <br />
        <br />
 
        </span></asp:Content>
