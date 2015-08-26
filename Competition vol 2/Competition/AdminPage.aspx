<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" MasterPageFile="~/Site.Master" Inherits="Competition.AdminPage" %>

 

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent" >
 
<h2><span style="font-size: 30px">Заявки пользователей</span></h2>
     
    <div>
    
        <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
    
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
                   
                <asp:TemplateField HeaderText = "Эксперт">           
                     <ItemTemplate>
                         <asp:Label ID="FK_Expert" runat="server" Text='<%# Eval("FK_Expert") %>' Visible = "true" />
                         <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                         </asp:DropDownList>
                  </ItemTemplate>
           </asp:TemplateField>
   
                <asp:TemplateField HeaderText="Действие">
                        <ItemTemplate>                                   
                            <asp:Button ID="BidDelete" runat="server" CommandName="Select" Text="Удалить" Width="200px" CommandArgument='<%# Eval("ID_Bid") %>' OnClick="BidDelete_Click"/>
                        </ItemTemplate>
                    </asp:TemplateField>
             
                </Columns>
        </asp:GridView>
</asp:Content>    
