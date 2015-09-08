<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EvaluateApplication.aspx.cs" Inherits="Competitions.Expert.EvaluateApplication" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <p>
        &nbsp;</p>
    <p>
        <asp:Button ID="GoBackButton" runat="server" OnClick="GoBackButton_Click" Text="Назад" />
    </p>
    <p>
        &nbsp;</p>
    <p>
        Основные критерии оценки заявки</p>
    <br />
    <asp:GridView ID="EvaluateGV"  BorderStyle="Solid" runat="server" AutoGenerateColumns="False" 
                BorderColor="Black"  BorderWidth="1px" CellPadding="0" EnableTheming="True">
               <Columns>         
                     
                     <asp:BoundField DataField="Name"   HeaderText="Название" Visible="true" />
                     <asp:TemplateField Visible="true"   HeaderText="Значение">
                        <ItemTemplate > 
                            <asp:Label      ID="ID" runat="server"  Visible="false"  Text='<%# Bind("ID") %>'></asp:Label>                         
                            <asp:TextBox ID="ValueTextBox" runat="server" Text='<%# Bind("Text") %>' ></asp:TextBox>                                             
                        </ItemTemplate>
                    </asp:TemplateField>  
                   </Columns>
     </asp:GridView>
    <br />
    <p>
        Комментарий эксперта</p>
    <p>
        <asp:TextBox ID="CommentTextBox" runat="server" Height="113px" TextMode="MultiLine" Width="630px"></asp:TextBox>
    </p>
    
    <br />
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Сохранить" Width="638px" OnClick="Button1_Click" />
    <br />
</asp:Content>
