<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewArticleForm.aspx.cs" Inherits="Rank.Forms.ViewArticleForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <div style="font-size: medium">
        <asp:Button ID="Button1" runat="server" Text="Назад" OnClick="Button1_Click" />
        <br /> 
         <br />
         <asp:Label ID="Label1" runat="server" Text="Label" Font-Bold="True"></asp:Label></span>&nbsp;<br />
         <span style="font-size: medium">
          <asp:Label ID="Label3" runat="server" Text="Label" Visible="False"></asp:Label>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><br />
          <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
          <br />
          <div id="TableDiv" runat="server">
        </div>
        <br />
        <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server"   >
            <Columns>
                 <asp:BoundField DataField="ID" HeaderText="" Visible="false" />
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
            
                <asp:TemplateField HeaderText="Коэффициент сложности" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                    <ItemTemplate>
                         <asp:Label ID="point" runat="server" Text='<%# Bind("point") %>'  Visible="true"></asp:Label>                   
                    </ItemTemplate>
                </asp:TemplateField>   
            </Columns>
        </asp:GridView>
          <br />
          <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Утвердить " Visible="False" />
        <br />
    </div>
</asp:Content>
