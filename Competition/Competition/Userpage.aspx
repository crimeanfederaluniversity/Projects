<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Userpage.aspx.cs" EnableEventValidation="false" MasterPageFile="~/masterpage.Master" Inherits="Competition.Userpage" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" >
    <div>
    
        
        <br />
    
        
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Мои заявки" />
    
        
        <br />
        <br />
    
    </div>
        <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
             <Columns>
                  
                 <asp:TemplateField HeaderText="ID" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "False" >
                        <ItemTemplate> 
                            <asp:Label ID="ID_Konkurs" runat="server" Text='<%# Bind("ID_Konkurs") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                        <asp:TemplateField HeaderText="Шифр конкурса" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="Number" runat="server" Text='<%# Bind("Number") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                
                        <asp:TemplateField HeaderText="Название" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="Name" runat="server" Text='<%# Bind("Name") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                        <asp:TemplateField HeaderText="Дата начала" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="StartDate" runat="server" Text='<%# Bind("StartDate") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                        <asp:TemplateField HeaderText="Дата окончания" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="EndDate" runat="server" Text='<%# Bind("EndDate") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                         <asp:TemplateField HeaderText="Подать заявку">
                        <ItemTemplate>                           
                            <asp:Button ID="Bid" runat="server" CommandName="Select" Text="Подать заявку" Width="200px" CommandArgument='<%# Eval("ID_Konkurs") %>' OnClick="Bid_Click"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                  
                  </Columns>
        </asp:GridView>
        <br />
        <br />
 </asp:Content>