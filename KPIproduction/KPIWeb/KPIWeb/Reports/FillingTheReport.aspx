<%@ Page Language="C#" Title="Заполение" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="FillingTheReport.aspx.cs" Inherits="KPIWeb.Reports.FillingTheReport" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
        <div>
            <h2>Ввведите значения в таблицу показателей и нажмите кнопку "Сохранить" внизу формы</h2>
            <br />
            <asp:GridView ID="GridviewCollectedBasicParameters" BorderStyle="Solid" runat="server" ShowFooter="true" AutoGenerateColumns="False" BorderColor="Black" BorderWidth="1px" CellPadding="0">
                <Columns>

                    <asp:BoundField DataField="CurrentReportArchiveID" HeaderText="Current Report ID" Visible="false" />
                    <asp:BoundField DataField="BasicParametersTableID" HeaderText="Basic Parameter ID" Visible="false" />

                    <asp:TemplateField Visible="false" InsertVisible="False">
                        <ItemTemplate>
                            <asp:Label ID="LabelCollectedBasicParametersTableID" runat="server" Visible="false" Text='<%# Bind("CollectedBasicParametersTableID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Name" HeaderText="Название показателя" />
                   
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value0" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value0") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value1" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value1") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>               
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value2" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value2") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value3" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value3") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>     
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value4" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value4") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value5" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value5") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>               
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value6" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value6") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value7" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value7") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>     
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value8" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value8") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
         
                </Columns>
            </asp:GridView>
            <br />
            <asp:Button ID="ButtonSave" Width="400px" runat="server" Text="Сохранить" OnClick="ButtonSave_Click" />

            <br />

        </div>
        </asp:Content>
