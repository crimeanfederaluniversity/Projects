<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="HeadShowResult.aspx.cs" Inherits="KPIWeb.Head.HeadShowResult" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
        <br />
        <br />
        <br />
        Резульаты<br />
    <asp:GridView ID="IndicatorsTable" runat="server" ShowFooter="true" AutoGenerateColumns="false" Width="1000px" OnRowDataBound="IndicatorsTable_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="IndicatorName" HeaderText="Индикатор" />
                                <asp:BoundField DataField="IndicatorResult" HeaderText="Результат" />
                                <asp:TemplateField HeaderText="Подтверждено рассчетных показателей" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                                    <ItemTemplate> 
                                        <asp:Label ID="info0"  runat="server" Visible="true" Text='<%# Bind("info0") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
            </asp:GridView>
        <br />
            
             <asp:GridView ID="CalculatedParametrsTable" runat="server" ShowFooter="true" AutoGenerateColumns="false" Width="1000px" OnRowDataBound="CalculatedParametrsTable_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="CalculatedParametrsName" HeaderText="Расчетный параметр" />
                                <asp:BoundField DataField="CalculatedParametrsResult" HeaderText="Результат" />   
                                <asp:TemplateField HeaderText="Подтверждение" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "False" >
                                    <ItemTemplate> 
                                        <asp:CheckBox ID="checkBoxCalc" runat="server" Visible="True"></asp:CheckBox>
                                        <asp:Label ID="checkBoxCalcId"  runat="server" Visible="false" Text='<%# Bind("checkBoxCalcId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                
                                <asp:TemplateField HeaderText="Внесенные данные" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                                    <ItemTemplate>                                      
                                        <asp:Label ID="info0"  runat="server" Visible="true" Text='<%# Bind("info0") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField> 

                            </Columns>
            </asp:GridView>

            <br />
        <asp:Button ID="Button8" runat="server" Enabled="False" />
&nbsp;<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Button ID="Button7" runat="server" Enabled="False" />
&nbsp;<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Button ID="Button4" runat="server" Enabled="False" />
&nbsp;<asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Button ID="Button5" runat="server" Enabled="False" />
&nbsp;<asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Button ID="Button6" runat="server" Enabled="False" />
&nbsp;<asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Button ID="Button9" runat="server" Enabled="False" />
&nbsp;<asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" Width="340px" />
    </div>
</asp:Content>