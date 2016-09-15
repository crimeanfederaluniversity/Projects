<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" EnableEventValidation ="false" CodeBehind="UserMainPage.aspx.cs" Inherits="Rank.Forms.UserMainPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <div>
          <h3>Индивидуальный рейтинг научно-педагогических работников, подразделений, СП(Ф) высшего образования и научных СП(Ф) и их руководителей ФГАОУ ВО «КФУ им. В.И.Вернадского»</h3>  
          <h3><asp:Label ID="Label2" runat="server" Text="Мой индивидуальный рейтинг за 2016 год:"></asp:Label>
&nbsp;<asp:Label ID="Label1" runat="server" Text="Label" ></asp:Label>
          </h3>
          <p>
              <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Подтвердить мое соавторство" Width="401px" />
          &nbsp;</p>
          <asp:Label ID="Label3" runat="server" Font-Bold="True" Text="Внести мои данные по показателям рейтинга"></asp:Label>
          <asp:GridView ID="GridView2" AutoGenerateColumns="false" runat="server"   >
             <Columns>
                  <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "false" >
                        <ItemTemplate> 
                            <asp:Label ID="ID0" runat="server" Text='<%# Bind("ID") %>'  Visible="false"></asp:Label>
                        </ItemTemplate>
               <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 
                     <asp:TemplateField HeaderText="№ показателя" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                        <ItemTemplate> 
                            <asp:Label ID="Number" runat="server" Text='<%# Bind("Number") %>'  Visible="true"></asp:Label>
                        </ItemTemplate>
               <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 
                <asp:TemplateField HeaderText="Название показателя" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="Parametr0" runat="server" Text='<%# Bind("Parametr") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
               <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 
                  <asp:TemplateField HeaderText="Баллы" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="Point0" runat="server" Text='<%# Bind("Point") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                      <asp:TemplateField HeaderText="Перейти">
                        <ItemTemplate>
                            <asp:Button ID="EditButton0" runat="server" CommandName="Select" Text ="Внести данные"  Width="150px" CommandArgument='<%# Eval("ID") %>' OnClick="EditButtonClik" />
                        </ItemTemplate>
                    </asp:TemplateField>             
                 </Columns>
        </asp:GridView>

          <br />
          <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Просмотреть мои данные по показателям рейтинга"></asp:Label>

          <br />
          <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server"  OnRowDataBound ="OnRowDataBound" >
             <Columns>
                  <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "false" >
                        <ItemTemplate> 
                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'  Visible="false"></asp:Label>
                        </ItemTemplate>
               <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="№ показателя" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                        <ItemTemplate> 
                            <asp:Label ID="Number" runat="server" Text='<%# Bind("Number") %>'  Visible="true"></asp:Label>
                        </ItemTemplate>
               <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 
                <asp:TemplateField HeaderText="Название показателя" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
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
                            <asp:Button ID="EditButton" runat="server" CommandName="Select" Text =""  Width="150px" CommandArgument='<%# Eval("ID") %>' OnClick="EditButtonClik" />
                        </ItemTemplate>
                    </asp:TemplateField>             
                 </Columns>
        </asp:GridView>

    </div>
</asp:Content>
