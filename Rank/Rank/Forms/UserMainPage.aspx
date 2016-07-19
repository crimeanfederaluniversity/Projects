<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" EnableEventValidation ="false" CodeBehind="UserMainPage.aspx.cs" Inherits="Rank.Forms.UserMainPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <div>
          <h3>Добро пожаловать в систему "Рейтинги"</h3>
          <h3>Результаты показателей Вашего индивидуального рейтинга за 2016 год:</h3>
          <p>
&nbsp;<asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Подтвердить мое соавторство" Width="401px" />
          &nbsp;</p>
          <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server" >
             <Columns>
                  <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "false" >
                        <ItemTemplate> 
                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'  Visible="false"></asp:Label>
                        </ItemTemplate>
               <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 

                <asp:TemplateField HeaderText="Название параметра" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="Parametr" runat="server" Text='<%# Bind("Parametr") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
               <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 
                  <asp:TemplateField HeaderText="Баллы" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="Point" runat="server" Text='<%# Bind("Point") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                      <asp:TemplateField HeaderText="Перейти">
                        <ItemTemplate>
                            <asp:Button ID="EditButton" runat="server" CommandName="Select" Text="Перейти" Width="150px" CommandArgument='<%# Eval("ID") %>' OnClick="EditButtonClik" />
                        </ItemTemplate>
                    </asp:TemplateField>             
                 </Columns>
        </asp:GridView>

    </div>
</asp:Content>
