<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="PlannedIndicator.aspx.cs" Inherits="KPIWeb.PlannedIndicator" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
     <h2><span style="font-size: 30px">Редактирование плановых значений</span></h2>
    <div>
    
        <br />
    
    </div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Загрузить" Width="193px" />
        <br />
        <br />
        <asp:GridView ID="GridView1" AutoGenerateColumns="False"  runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"  >
                    <Columns>
                        <asp:TemplateField HeaderText="Номер планового" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="PlanedIndicatorID" runat="server" Text='<%# Bind("PlanedIndicatorID") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                
                        <asp:TemplateField HeaderText="Значение" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="Value" runat="server" Text='<%# Bind("Value") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                        <asp:TemplateField HeaderText="Дата" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="Date" runat="server" Text='<%# Bind("Date") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                        <asp:TemplateField HeaderText="Номер целевого показателя" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="FK_IndicatorsTable" runat="server" Text='<%# Bind("FK_IndicatorsTable") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                          <asp:TemplateField HeaderText="Удалить">
                        <ItemTemplate>
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Select" Text="Удалить" Width="200px" CommandArgument='<%# Bind("PlanedIndicatorID") %>' OnClick="DeleteButtonClick" />
                        </ItemTemplate>
                    </asp:TemplateField>   

                         </Columns>      
            </asp:GridView>                    
   
    <br />
                 <asp:Label ID="addtitle" runat="server" Font-Size="X-Large" Text="Редактирование "></asp:Label>
                 <br   />
                <br   />
                <asp:Label ID="Label1" runat="server" Text="Выберите целевой показатель"></asp:Label>
                <br />
                <br   />
                <asp:DropDownList ID="DropDownList1" runat="server" Height="22px" Width="541px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">

    </asp:DropDownList>
    <br />
                <br   />
                <asp:Label ID="Label3" runat="server" Text="Значение"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="IndicatorMeasure" runat="server"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                
                <asp:CheckBox ID="CheckBox1" Text="Активен" runat="server" />
                 <br />
                 <br />
    <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
    <br />
    <br />
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Сохранить" />
                 <br   />
                    
</asp:Content>
