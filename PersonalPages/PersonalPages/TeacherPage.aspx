<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="TeacherPage.aspx.cs" Inherits="PersonalPages.TeacherPage" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
        <span style="font-size: x-large">Страница преподавателя<br />
        </span><span style="font-size: large">Мои группы:</span><span style="font-size: x-large"><br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Height="52px" Width="307px">
               <Columns>    
            <asp:TemplateField Visible="false"   HeaderText="Номер" >
                        <ItemTemplate>
                            <asp:Label ID="LabelID" runat="server"  Visible="false" Text='<%# Bind("ID") %>'  ></asp:Label> 
                               </ItemTemplate>
                    </asp:TemplateField> 
             <asp:TemplateField Visible="true"   HeaderText="Название группы">
                        <ItemTemplate>
                            <asp:Label ID="Name" runat="server"  Visible="true" Text='<%# Bind("Name") %>'  ></asp:Label> 
                               </ItemTemplate>
                    </asp:TemplateField> 
                   <asp:TemplateField HeaderText="Ведомость">
                        <ItemTemplate>  
                           <asp:Button ID="WatchButton" runat="server" CommandName="Select" Text="Скачать" Width="200px" CommandArgument='<%# Eval("WatchButton") %>' OnClick="WatchButtonClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>              
                      </Columns>    
        </asp:GridView>
        Название группы:<br />
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        Загрузить ведомость<br />
        <asp:FileUpload ID="FileUpload2" runat="server" Height="24px" Width="255px" />
        <br />
        </span>
        <asp:Button ID="Button1" runat="server" Height="28px" OnClick="Button1_Click" Text="Сохранить" Width="105px" />
 
    </asp:Content>
