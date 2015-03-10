<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="HeadShowReportResult.aspx.cs" Inherits="KPIWeb.Head.HeadShowReportResult" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
        <br />
        <br />
        <br />
        Резульаты для вашей академии<br />
    <asp:GridView ID="IndicatorsTable" runat="server" ShowFooter="true" AutoGenerateColumns="false" Width="1000px" OnRowDataBound="IndicatorsTable_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="IndicatorName" HeaderText="Индикатор" />
                                <asp:BoundField DataField="IndicatorResult" HeaderText="Результат" />
                                <asp:TemplateField HeaderText="Подтверждение" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "false" >
                                    <ItemTemplate> 
                                        <asp:Label ID="ConfCnt"  runat="server" Visible="true" Text='<%# Bind("ConfCnt") %>'></asp:Label>
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
        <!--
            <asp:GridView ID="BasicParametrsTable" runat="server" ShowFooter="true" AutoGenerateColumns="false" Width="1000px" Visible="False">
                            <Columns>
                                <asp:BoundField DataField="BasicParametrsName" HeaderText="Базовый параметр" />
                                <asp:BoundField DataField="BasicParametrsResult" HeaderText="Результат(сумма)" /> 
                                <asp:TemplateField HeaderText="Подтверждение" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                                    <ItemTemplate> 
                                        <asp:CheckBox ID="checkBoxBasic" runat="server" Visible="True"></asp:CheckBox>
                                        <asp:Label ID="checkBoxBasicId"  runat="server" Visible="false" Text='<ПРОЦЕНТ# Bind("checkBoxBasicId") ПРОЦЕНТ>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                          
                            </Columns>
            </asp:GridView>-->
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" Width="340px" />
    </div>
</asp:Content>

