<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="UserMainPage.aspx.cs" Inherits="PersonalPages.UserMainPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  
        <h2>Добро пожаловать в личный кабинет пользователя!
            
        </h2>
      <div>
        <asp:GridView ID="GroupsGridView" runat="server" AutoGenerateColumns="False" >
            <Columns>    

                <asp:TemplateField Visible="true"   HeaderText="Название проекта" >
                    <ItemTemplate>
                        <asp:Label ID="projectName" runat="server"  Visible="true" Text='<%# Bind("ProjectName") %>'  ></asp:Label> 
                    </ItemTemplate>
                </asp:TemplateField> 


                <asp:TemplateField HeaderText="Переход">
                    <ItemTemplate>  
                        <asp:Button ID="RedirectToSubdomainButton" runat="server" CommandName="Select" Text='<%# Bind("GroupName") %>' Width="400px" CommandArgument='<%# Bind("GroupId") %>' OnClick="RedirectToSubdomain" />
                    </ItemTemplate>
                </asp:TemplateField>  
                            
            </Columns>    
        </asp:GridView>
          <br />
    <div>
         <br />
    </div>
        
    
   
</div>
            
    
   
</asp:Content>
