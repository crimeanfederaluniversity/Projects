<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Confirm.aspx.cs" Inherits="KPIWeb.StatisticsDepartment.Confirm" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
  <h2><span style="font-size: 30px">Просмотр информации о подтверждениях</span></h2>
  
    <div>          
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="По целевым показателям" Width="250px" />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="По расчетным показателям" Width="250px" />
        <br />
        <br />
  
    <asp:Label ID="Label1" runat="server" Text="Выберите отчет" ></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
    </asp:DropDownList>
        <br />
    <asp:Label ID="Label2" runat="server" Text="Выберите пользователя"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
    </asp:DropDownList>
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Загрузить" />
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
          <Columns>               
                
                 <asp:TemplateField HeaderText="Тип подтверждения" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="Name" runat="server" Text='<%# Bind("Name") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>   
                 
                  <asp:TemplateField HeaderText="Комментарий" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="Comment" runat="server" Text='<%# Bind("Comment") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                
              <asp:TemplateField HeaderText="Дата" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="Date" runat="server" Text='<%# Bind("Date") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                  
                      
                </Columns>
    </asp:GridView>
        <br />
    </div>


</asp:Content>