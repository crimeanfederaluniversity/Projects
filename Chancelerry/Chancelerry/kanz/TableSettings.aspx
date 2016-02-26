<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TableSettings.aspx.cs" Inherits="Chancelerry.kanz.TableSettings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server">           
             <Columns>               
                           
                 <asp:TemplateField HeaderText="ID" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "False" >
                        <ItemTemplate> 
                            <asp:Label ID="fieldId" runat="server" Text='<%# Bind("fieldId") %>'  Visible="false"></asp:Label>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField>   
                 
                 

                   <asp:TemplateField HeaderText="Название колонки" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                           <asp:TextBox ID="fieldName" style="text-align:center" BorderWidth="0" ReadOnly="True"  runat="server" Text='<%# Bind("fieldName") %>'></asp:TextBox>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField>     

                     <asp:TemplateField HeaderText="Вес"  HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="fieldWeight" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("fieldWeight") %>'></asp:TextBox>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField>   

                    <asp:TemplateField HeaderText="Добавить" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:CheckBox ID="fieldIsAdd" style="text-align:center" BorderWidth="0" runat="server" Checked='<%# Bind("fieldIsAdd") %>'></asp:CheckBox>
                        </ItemTemplate>
                         </asp:TemplateField>  
                    </Columns>
    </asp:GridView>
    <br />
    <asp:Button ID="Button1" runat="server" Text="Сохранить" OnClick="Button1_Click" />
    -
</asp:Content>
