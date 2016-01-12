<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BasicParametrsComments.aspx.cs" Inherits="KPIWeb.AutomationDepartment.BasicParametrsComments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    

    

    <p>
        <br />
        Отчет&nbsp;
        <asp:DropDownList ID="DropDownList1" runat="server" Height="16px" Width="290px">
        </asp:DropDownList>
    </p>
    <p>
        <asp:Button ID="Button1" runat="server" Text="Показать" Width="335px" OnClick="Button1_Click" />
    </p>
    <p>
        <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False">
            
             <Columns>    
                 <asp:TemplateField Visible="true" HeaderText="Номер показателя">
                        <ItemTemplate >
                            <asp:Label ID="BasicParamId"  runat="server" Visible="true"  Text='<%# Bind("BasicParamIdValue") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                 <asp:TemplateField Visible="true" HeaderText="CommentId">
                        <ItemTemplate >
                            <asp:Label ID="CommentId"  runat="server" Visible="true"  Text='<%# Bind("CommentIdValue") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                 <asp:BoundField DataField="BasicParamName" HeaderText="Название показателя" Visible="true" />

                 <asp:TemplateField Visible="true" HeaderText="Комментарий">
                        <ItemTemplate>                       
                            <asp:TextBox ID="Comment" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("CommentValue") %>'></asp:TextBox>             
                        </ItemTemplate>
                    </asp:TemplateField>
                 </Columns>
        </asp:GridView>
    </p>
    <p>
        <asp:Button ID="Button2" runat="server" Text="Сохранить" Width="342px" OnClick="Button2_Click" />
    </p>
    <p>
    </p>
    <p>
    </p>
    
    

    

</asp:Content>
