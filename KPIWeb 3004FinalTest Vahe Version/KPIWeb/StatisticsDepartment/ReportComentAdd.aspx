<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportComentAdd.aspx.cs" Inherits="KPIWeb.StatisticsDepartment.ReportComentAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

       <h2><span style="font-size: 20px">Добавить/изменить комментарий к базовому показателю:</span></h2>
       <p>
           <asp:Button ID="SaveButton" runat="server" OnClick="SaveButton_Click" Text="Сохранить изменения" />
       </p>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="Название базового показателя">
                                    <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Visible="False" Text='<%# Bind("CommentId") %>'></asp:Label>
                                            </ItemTemplate>
                              </asp:TemplateField>
            <asp:TemplateField HeaderText="Название базового показателя">
                                    <ItemTemplate>                                       
                                        <asp:Label ID="Label1" runat="server" Visible="True" Text='<%# Bind("Name") %>'></asp:Label>
                                    </ItemTemplate>
                              </asp:TemplateField>
            <asp:TemplateField HeaderText="Текст комментария">
                            <ItemTemplate>
                                 <asp:TextBox ID="ComentTextBox" runat="server"  Visible="True" Text='<%# Bind("Comment") %>' ></asp:TextBox>                                                  
                          </ItemTemplate>
            </asp:TemplateField>
                    </Columns>
        </asp:GridView>
</asp:Content>
