<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpertPage.aspx.cs" MasterPageFile="~/Site.Master" Inherits="Competition.ExpertPage" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent" >
 
<h2><span style="font-size: 30px">Заявки пользователей</span></h2>
     
    <div>
    
    </div>
        <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <Columns>  
           
                 <asp:TemplateField HeaderText="ID" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "False" >                                      
                  <ItemTemplate> 
                            <asp:Label ID="ID_Bid" runat="server" Text='<%# Bind("ID_Bid") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                        <asp:TemplateField HeaderText="Имя заявки" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="BidName" runat="server" Text='<%# Bind("BidName") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                
                        <asp:TemplateField HeaderText="Конкурс" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="Konkurs" runat="server" Text='<%# Bind("FK_Konkurs") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                        <asp:TemplateField HeaderText="Дата подачи" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="Date" runat="server" Text='<%# Bind("Date") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                        <asp:TemplateField HeaderText="Статус" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>                          
                   </asp:TemplateField>

                 <asp:TemplateField HeaderText="Просмотр">
                        <ItemTemplate>                                   
                            <asp:Button ID="Bid" runat="server" CommandName="Select" Text="Просмотреть" Width="200px" CommandArgument='<%# Eval("ID_Bid") %>' OnClick="BidFill_Click"/>
                        </ItemTemplate>
                    </asp:TemplateField>
              
                </Columns>
        </asp:GridView>
 
</asp:Content>    