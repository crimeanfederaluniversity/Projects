<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master"  CodeBehind="ViewBasicParams.aspx.cs" Inherits="KPIWeb.Director.ViewBasicParams" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">


     <br />


     <asp:GridView ID="GridviewCollectedBasicParameters"  BorderStyle="Solid" runat="server" CssClass="Grid_view_V_style Grid_view_style GridHeader" ShowFooter="true" AutoGenerateColumns="False" 
                BorderColor="Black"  BorderWidth="1px" CellPadding="0">
            
                <Columns>                  
                     <asp:BoundField DataField="BasicParametersTableID" HeaderText="Код показателя" Visible="true" />                              
                     <asp:BoundField DataField="Name"  HeaderText="Название показателя" Visible="true" />
                     <asp:BoundField DataField="Comment"  HeaderText="Комментарий" Visible="true" />
                                
                     <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value0"  Width="95%" style="text-align:center;" BorderWidth="0" runat="server" Text='<%# Bind("Value0") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false"  HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value1" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value1") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>               
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value2" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value2") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value3" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value3") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>     
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value4" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value4") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value5" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value5") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>               
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value6" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value6") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value7" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value7") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>     
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value8" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value8") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value9" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value9") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value10" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value10") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>     

                     <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value11" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value11") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>  

                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value12" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value12") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value13" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value13") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value14" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value14") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value15" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value15") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value16" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value16") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value17" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value17") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value18" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value18") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value19" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value19") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value20" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value20") %>'></asp:TextBox>                           
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value21" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value21") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value22" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value22") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value23" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value23") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value24" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value24") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 

                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value25" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value25") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField>                            
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value26" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value26") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField>            
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value27" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value27") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value28" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value28") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value29" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value29") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value30" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value30") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value31" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value31") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value32" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value32") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value33" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value33") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value34" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value34") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value35" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value35") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value36" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value36") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value37" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value37") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value38" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value38") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value39" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value39") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value40" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value40") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                </Columns>
            </asp:GridView>

</asp:Content>
