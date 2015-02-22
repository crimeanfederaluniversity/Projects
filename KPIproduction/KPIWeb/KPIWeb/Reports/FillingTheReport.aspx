<%@ Page Language="C#" Title="Заполение" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="FillingTheReport.aspx.cs" Inherits="KPIWeb.Reports.FillingTheReport" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
        <div>
            <h2>Ввведите значения в таблицу показателей и нажмите кнопку "Сохранить" внизу формы</h2>
            <br />
            <asp:GridView ID="GridviewCollectedBasicParameters" BorderStyle="Solid" runat="server" ShowFooter="true" AutoGenerateColumns="False" BorderColor="Black" BorderWidth="1px" CellPadding="0" OnDataBound="GridviewCollectedBasicParameters_DataBound" OnRowDataBound="GridviewCollectedBasicParameters_RowDataBound">
                <Columns>

                    <asp:BoundField DataField="CurrentReportArchiveID" HeaderText="Current Report ID" Visible="false" />
                    <asp:BoundField DataField="BasicParametersTableID" HeaderText="Basic Parameter ID" Visible="false" />

                    <asp:TemplateField Visible="false" InsertVisible="False">
                        <ItemTemplate>
                            <asp:Label ID="LabelCollectedBasicParametersTableID" runat="server" Visible="false" Text='<%# Bind("CollectedBasicParametersTableID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Name" HeaderText="Название показателя" />
                   
                    <asp:TemplateField HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="MyValue" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("MyValue") %>'></asp:TextBox>
                            <asp:Label ID="MyCollectId" runat="server" Visible="false" Text='<%# Bind("MyCollectId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 

                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value0" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value0") %>'></asp:TextBox>
                            <asp:Label ID="CollectId0" runat="server" Visible="false" Text='<%# Bind("CollectId0") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value1" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value1") %>'></asp:TextBox>
                            <asp:Label ID="CollectId1" runat="server" Visible="false" Text='<%# Bind("CollectId1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>               
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value2" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value2") %>'></asp:TextBox>
                             <asp:Label ID="CollectId2" runat="server" Visible="false" Text='<%# Bind("CollectId2") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value3" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value3") %>'></asp:TextBox>
                             <asp:Label ID="CollectId3" runat="server" Visible="false" Text='<%# Bind("CollectId3") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>     
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value4" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value4") %>'></asp:TextBox>
                             <asp:Label ID="CollectId4" runat="server" Visible="false" Text='<%# Bind("CollectId4") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value5" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value5") %>'></asp:TextBox>
                             <asp:Label ID="CollectId5" runat="server" Visible="false" Text='<%# Bind("CollectId5") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>               
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value6" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value6") %>'></asp:TextBox>
                             <asp:Label ID="CollectId6" runat="server" Visible="false" Text='<%# Bind("CollectId6") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value7" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value7") %>'></asp:TextBox>
                             <asp:Label ID="CollectId7" runat="server" Visible="false" Text='<%# Bind("CollectId7") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>     
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value8" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value8") %>'></asp:TextBox>
                             <asp:Label ID="CollectId8" runat="server" Visible="false" Text='<%# Bind("CollectId8") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value9" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value9") %>'></asp:TextBox>
                            <asp:Label ID="CollectId9" runat="server" Visible="false" Text='<%# Bind("CollectId9") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value10" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value10") %>'></asp:TextBox>
                            <asp:Label ID="CollectId10" runat="server" Visible="false" Text='<%# Bind("CollectId10") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>               
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value11" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value11") %>'></asp:TextBox>
                             <asp:Label ID="CollectId11" runat="server" Visible="false" Text='<%# Bind("CollectId11") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value12" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value12") %>'></asp:TextBox>
                             <asp:Label ID="CollectId12" runat="server" Visible="false" Text='<%# Bind("CollectId12") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>     
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value13" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value13") %>'></asp:TextBox>
                             <asp:Label ID="CollectId13" runat="server" Visible="false" Text='<%# Bind("CollectId13") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value14" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value14") %>'></asp:TextBox>
                             <asp:Label ID="CollectId14" runat="server" Visible="false" Text='<%# Bind("CollectId14") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>               
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value15" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value15") %>'></asp:TextBox>
                             <asp:Label ID="CollectId15" runat="server" Visible="false" Text='<%# Bind("CollectId15") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value16" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value16") %>'></asp:TextBox>
                             <asp:Label ID="CollectId16" runat="server" Visible="false" Text='<%# Bind("CollectId16") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>     
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value17" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value17") %>'></asp:TextBox>
                             <asp:Label ID="CollectId17" runat="server" Visible="false" Text='<%# Bind("CollectId17") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                             <asp:TemplateField  Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value18" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value18") %>'></asp:TextBox>
                             <asp:Label ID="CollectId18" runat="server" Visible="false" Text='<%# Bind("CollectId18") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>     
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value19" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value19") %>'></asp:TextBox>
                             <asp:Label ID="CollectId19" runat="server" Visible="false" Text='<%# Bind("CollectId19") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
         
                </Columns>
            </asp:GridView>
            <br />
            <asp:Button ID="ButtonSave" Width="400px" runat="server" Text="Сохранить" OnClick="ButtonSave_Click" />

            <br />

        </div>
        </asp:Content>
