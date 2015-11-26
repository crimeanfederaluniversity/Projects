<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Document.aspx.cs" Inherits="KPIWeb.StatisticsDepartment.Document" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><span style="font-size: 30px">Редактирование нормативных документов</span></h2>
  
    <div>          
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Просмотр документов" Width="237px" />
        <br />
        <br />
        <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server" >           
             <Columns>               
                
                 <asp:TemplateField HeaderText="Имя файла" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="DocumentName" runat="server" Text='<%# Bind("DocumentName") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>   
                 
                  <asp:TemplateField HeaderText="Название для пользователей" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="DocumentLink" runat="server" Text='<%# Bind("DocumentLink") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                
                 <asp:TemplateField HeaderText="Удалить">
                        <ItemTemplate>  
                           <asp:Button ID="DeleteButton" runat="server" CommandName="Select" Text="Удалить" Width="200px" CommandArgument='<%# Eval("DocumentID") %>' OnClick="DeleteButtonClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>   
                      
                </Columns>
       </asp:GridView>
        <br />
        <h3><span style="font-size: 30px">Добавление нового документа</span></h3>
        <br />
  <asp:Label ID="Label1" runat="server" Text="Точное имя файла"></asp:Label>
        <br />
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Width="258px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Название для пользователей"></asp:Label>
        <br />
        <br />
        <asp:TextBox ID="TextBox2" runat="server" Width="254px"></asp:TextBox>
        <br />
        <br />
        <asp:FileUpload id="FileUpload1" runat="server"> </asp:FileUpload>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Сохранить" OnClick="Button1_Click" />
    </div>

</asp:Content>