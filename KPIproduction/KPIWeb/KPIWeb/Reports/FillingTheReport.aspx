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

                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value20" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value20") %>'></asp:TextBox>
                             <asp:Label ID="CollectId20" runat="server" Visible="false" Text='<%# Bind("CollectId20") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value21" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value21") %>'></asp:TextBox>
                             <asp:Label ID="CollectId21" runat="server" Visible="false" Text='<%# Bind("CollectId21") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value22" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value22") %>'></asp:TextBox>
                             <asp:Label ID="CollectId22" runat="server" Visible="false" Text='<%# Bind("CollectId22") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value23" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value23") %>'></asp:TextBox>
                             <asp:Label ID="CollectId23" runat="server" Visible="false" Text='<%# Bind("CollectId23") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value24" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value24") %>'></asp:TextBox>
                             <asp:Label ID="CollectId24" runat="server" Visible="false" Text='<%# Bind("CollectId24") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value25" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value25") %>'></asp:TextBox>
                             <asp:Label ID="CollectId25" runat="server" Visible="false" Text='<%# Bind("CollectId25") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value26" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value26") %>'></asp:TextBox>
                             <asp:Label ID="CollectId26" runat="server" Visible="false" Text='<%# Bind("CollectId26") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value27" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value27") %>'></asp:TextBox>
                             <asp:Label ID="CollectId27" runat="server" Visible="false" Text='<%# Bind("CollectId27") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value28" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value28") %>'></asp:TextBox>
                             <asp:Label ID="CollectId28" runat="server" Visible="false" Text='<%# Bind("CollectId28") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value29" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value29") %>'></asp:TextBox>
                             <asp:Label ID="CollectId29" runat="server" Visible="false" Text='<%# Bind("CollectId29") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value30" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value30") %>'></asp:TextBox>
                             <asp:Label ID="CollectId30" runat="server" Visible="false" Text='<%# Bind("CollectId30") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value31" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value31") %>'></asp:TextBox>
                             <asp:Label ID="CollectId31" runat="server" Visible="false" Text='<%# Bind("CollectId31") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value32" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value32") %>'></asp:TextBox>
                             <asp:Label ID="CollectId32" runat="server" Visible="false" Text='<%# Bind("CollectId32") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value33" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value33") %>'></asp:TextBox>
                             <asp:Label ID="CollectId33" runat="server" Visible="false" Text='<%# Bind("CollectId33") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value34" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value34") %>'></asp:TextBox>
                             <asp:Label ID="CollectId34" runat="server" Visible="false" Text='<%# Bind("CollectId34") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value35" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value35") %>'></asp:TextBox>
                             <asp:Label ID="CollectId35" runat="server" Visible="false" Text='<%# Bind("CollectId35") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value36" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value36") %>'></asp:TextBox>
                             <asp:Label ID="CollectId36" runat="server" Visible="false" Text='<%# Bind("CollectId36") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value37" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value37") %>'></asp:TextBox>
                             <asp:Label ID="CollectId37" runat="server" Visible="false" Text='<%# Bind("CollectId37") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value38" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value38") %>'></asp:TextBox>
                             <asp:Label ID="CollectId38" runat="server" Visible="false" Text='<%# Bind("CollectId38") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value39" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value39") %>'></asp:TextBox>
                             <asp:Label ID="CollectId39" runat="server" Visible="false" Text='<%# Bind("CollectId39") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value40" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value40") %>'></asp:TextBox>
                             <asp:Label ID="CollectId40" runat="server" Visible="false" Text='<%# Bind("CollectId40") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value41" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value41") %>'></asp:TextBox>
                             <asp:Label ID="CollectId41" runat="server" Visible="false" Text='<%# Bind("CollectId41") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value42" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value42") %>'></asp:TextBox>
                             <asp:Label ID="CollectId42" runat="server" Visible="false" Text='<%# Bind("CollectId42") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value43" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value43") %>'></asp:TextBox>
                             <asp:Label ID="CollectId43" runat="server" Visible="false" Text='<%# Bind("CollectId43") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value44" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value44") %>'></asp:TextBox>
                             <asp:Label ID="CollectId44" runat="server" Visible="false" Text='<%# Bind("CollectId44") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value45" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value45") %>'></asp:TextBox>
                             <asp:Label ID="CollectId45" runat="server" Visible="false" Text='<%# Bind("CollectId45") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value46" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value46") %>'></asp:TextBox>
                             <asp:Label ID="CollectId46" runat="server" Visible="false" Text='<%# Bind("CollectId46") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value47" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value47") %>'></asp:TextBox>
                             <asp:Label ID="CollectId47" runat="server" Visible="false" Text='<%# Bind("CollectId47") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value48" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value48") %>'></asp:TextBox>
                             <asp:Label ID="CollectId48" runat="server" Visible="false" Text='<%# Bind("CollectId48") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value49" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value49") %>'></asp:TextBox>
                             <asp:Label ID="CollectId49" runat="server" Visible="false" Text='<%# Bind("CollectId49") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value50" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value50") %>'></asp:TextBox>
                             <asp:Label ID="CollectId50" runat="server" Visible="false" Text='<%# Bind("CollectId50") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value51" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value51") %>'></asp:TextBox>
                             <asp:Label ID="CollectId51" runat="server" Visible="false" Text='<%# Bind("CollectId51") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value52" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value52") %>'></asp:TextBox>
                             <asp:Label ID="CollectId52" runat="server" Visible="false" Text='<%# Bind("CollectId52") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value53" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value53") %>'></asp:TextBox>
                             <asp:Label ID="CollectId53" runat="server" Visible="false" Text='<%# Bind("CollectId53") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value54" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value54") %>'></asp:TextBox>
                             <asp:Label ID="CollectId54" runat="server" Visible="false" Text='<%# Bind("CollectId54") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value55" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value55") %>'></asp:TextBox>
                             <asp:Label ID="CollectId55" runat="server" Visible="false" Text='<%# Bind("CollectId55") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value56" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value56") %>'></asp:TextBox>
                             <asp:Label ID="CollectId56" runat="server" Visible="false" Text='<%# Bind("CollectId56") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value57" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value57") %>'></asp:TextBox>
                             <asp:Label ID="CollectId57" runat="server" Visible="false" Text='<%# Bind("CollectId57") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value58" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value58") %>'></asp:TextBox>
                             <asp:Label ID="CollectId58" runat="server" Visible="false" Text='<%# Bind("CollectId58") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value59" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value59") %>'></asp:TextBox>
                             <asp:Label ID="CollectId59" runat="server" Visible="false" Text='<%# Bind("CollectId59") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value60" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value60") %>'></asp:TextBox>
                             <asp:Label ID="CollectId60" runat="server" Visible="false" Text='<%# Bind("CollectId60") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value61" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value61") %>'></asp:TextBox>
                             <asp:Label ID="CollectId61" runat="server" Visible="false" Text='<%# Bind("CollectId61") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value62" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value62") %>'></asp:TextBox>
                             <asp:Label ID="CollectId62" runat="server" Visible="false" Text='<%# Bind("CollectId62") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value63" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value63") %>'></asp:TextBox>
                             <asp:Label ID="CollectId63" runat="server" Visible="false" Text='<%# Bind("CollectId63") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value64" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value64") %>'></asp:TextBox>
                             <asp:Label ID="CollectId64" runat="server" Visible="false" Text='<%# Bind("CollectId64") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value65" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value65") %>'></asp:TextBox>
                             <asp:Label ID="CollectId65" runat="server" Visible="false" Text='<%# Bind("CollectId65") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value66" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value66") %>'></asp:TextBox>
                             <asp:Label ID="CollectId66" runat="server" Visible="false" Text='<%# Bind("CollectId66") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value67" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value67") %>'></asp:TextBox>
                             <asp:Label ID="CollectId67" runat="server" Visible="false" Text='<%# Bind("CollectId67") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value68" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value68") %>'></asp:TextBox>
                             <asp:Label ID="CollectId68" runat="server" Visible="false" Text='<%# Bind("CollectId68") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value69" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value69") %>'></asp:TextBox>
                             <asp:Label ID="CollectId69" runat="server" Visible="false" Text='<%# Bind("CollectId69") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value70" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value70") %>'></asp:TextBox>
                             <asp:Label ID="CollectId70" runat="server" Visible="false" Text='<%# Bind("CollectId70") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value71" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value71") %>'></asp:TextBox>
                             <asp:Label ID="CollectId71" runat="server" Visible="false" Text='<%# Bind("CollectId71") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value72" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value72") %>'></asp:TextBox>
                             <asp:Label ID="CollectId72" runat="server" Visible="false" Text='<%# Bind("CollectId72") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value73" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value73") %>'></asp:TextBox>
                             <asp:Label ID="CollectId73" runat="server" Visible="false" Text='<%# Bind("CollectId73") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value74" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value74") %>'></asp:TextBox>
                             <asp:Label ID="CollectId74" runat="server" Visible="false" Text='<%# Bind("CollectId74") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value75" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value75") %>'></asp:TextBox>
                             <asp:Label ID="CollectId75" runat="server" Visible="false" Text='<%# Bind("CollectId75") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value76" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value76") %>'></asp:TextBox>
                             <asp:Label ID="CollectId76" runat="server" Visible="false" Text='<%# Bind("CollectId76") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value77" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value77") %>'></asp:TextBox>
                             <asp:Label ID="CollectId77" runat="server" Visible="false" Text='<%# Bind("CollectId77") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value78" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value78") %>'></asp:TextBox>
                             <asp:Label ID="CollectId78" runat="server" Visible="false" Text='<%# Bind("CollectId78") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value79" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value79") %>'></asp:TextBox>
                             <asp:Label ID="CollectId79" runat="server" Visible="false" Text='<%# Bind("CollectId79") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value80" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value80") %>'></asp:TextBox>
                             <asp:Label ID="CollectId80" runat="server" Visible="false" Text='<%# Bind("CollectId80") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value81" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value81") %>'></asp:TextBox>
                             <asp:Label ID="CollectId81" runat="server" Visible="false" Text='<%# Bind("CollectId81") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value82" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value82") %>'></asp:TextBox>
                             <asp:Label ID="CollectId82" runat="server" Visible="false" Text='<%# Bind("CollectId82") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value83" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value83") %>'></asp:TextBox>
                             <asp:Label ID="CollectId83" runat="server" Visible="false" Text='<%# Bind("CollectId83") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value84" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value84") %>'></asp:TextBox>
                             <asp:Label ID="CollectId84" runat="server" Visible="false" Text='<%# Bind("CollectId84") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value85" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value85") %>'></asp:TextBox>
                             <asp:Label ID="CollectId85" runat="server" Visible="false" Text='<%# Bind("CollectId85") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value86" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value86") %>'></asp:TextBox>
                             <asp:Label ID="CollectId86" runat="server" Visible="false" Text='<%# Bind("CollectId86") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value87" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value87") %>'></asp:TextBox>
                             <asp:Label ID="CollectId87" runat="server" Visible="false" Text='<%# Bind("CollectId87") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value88" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value88") %>'></asp:TextBox>
                             <asp:Label ID="CollectId88" runat="server" Visible="false" Text='<%# Bind("CollectId88") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value89" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value89") %>'></asp:TextBox>
                             <asp:Label ID="CollectId89" runat="server" Visible="false" Text='<%# Bind("CollectId89") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value90" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value90") %>'></asp:TextBox>
                             <asp:Label ID="CollectId90" runat="server" Visible="false" Text='<%# Bind("CollectId90") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value91" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value91") %>'></asp:TextBox>
                             <asp:Label ID="CollectId91" runat="server" Visible="false" Text='<%# Bind("CollectId91") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value92" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value92") %>'></asp:TextBox>
                             <asp:Label ID="CollectId92" runat="server" Visible="false" Text='<%# Bind("CollectId92") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value93" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value93") %>'></asp:TextBox>
                             <asp:Label ID="CollectId93" runat="server" Visible="false" Text='<%# Bind("CollectId93") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value94" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value94") %>'></asp:TextBox>
                             <asp:Label ID="CollectId94" runat="server" Visible="false" Text='<%# Bind("CollectId94") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value95" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value95") %>'></asp:TextBox>
                             <asp:Label ID="CollectId95" runat="server" Visible="false" Text='<%# Bind("CollectId95") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value96" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value96") %>'></asp:TextBox>
                             <asp:Label ID="CollectId96" runat="server" Visible="false" Text='<%# Bind("CollectId96") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value97" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value97") %>'></asp:TextBox>
                             <asp:Label ID="CollectId97" runat="server" Visible="false" Text='<%# Bind("CollectId97") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value98" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value98") %>'></asp:TextBox>
                             <asp:Label ID="CollectId98" runat="server" Visible="false" Text='<%# Bind("CollectId98") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value99" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value99") %>'></asp:TextBox>
                             <asp:Label ID="CollectId99" runat="server" Visible="false" Text='<%# Bind("CollectId99") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value100" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value100") %>'></asp:TextBox>
                             <asp:Label ID="CollectId100" runat="server" Visible="false" Text='<%# Bind("CollectId100") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>



                </Columns>
            </asp:GridView>
            <br />
            <asp:Button ID="ButtonSave" Width="400px" runat="server" Text="Сохранить" OnClick="ButtonSave_Click" />

            <br />

        </div>
        </asp:Content>
