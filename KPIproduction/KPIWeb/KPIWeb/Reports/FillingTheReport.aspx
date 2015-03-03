<%@ Page Language="C#" Title="Заполение" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="FillingTheReport.aspx.cs" Inherits="KPIWeb.Reports.FillingTheReport" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
        <div>
            <h2>
                <asp:Label ID="Label1" runat="server" Text="Ввведите значения в таблицу показателей и нажмите кнопку &quot;Сохранить&quot; внизу формы"></asp:Label>
            </h2>
            <br />


            <asp:GridView ID="GridviewCollectedBasicParameters" BorderStyle="Solid" runat="server" ShowFooter="true" AutoGenerateColumns="False" 
                BorderColor="Black" BorderWidth="1px" CellPadding="0" 
          
                OnRowDataBound="GridviewCollectedBasicParameters_RowDataBound" OnSelectedIndexChanged="GridviewCollectedBasicParameters_SelectedIndexChanged" OnSelectedIndexChanging="GridviewCollectedBasicParameters_SelectedIndexChanging" OnPageIndexChanging="GridviewCollectedBasicParameters_PageIndexChanging">
            
                <Columns>

                    <asp:BoundField DataField="CurrentReportArchiveID" HeaderText="Current Report ID" Visible="false" />
                    <asp:BoundField DataField="BasicParametersTableID" HeaderText="Basic Parameter ID" Visible="false" />

                    <asp:TemplateField Visible="false"  InsertVisible="False">
                        <ItemTemplate>
                            <asp:Label ID="LabelCollectedBasicParametersTableID"  runat="server" Visible="false" Text='<%# Bind("CollectedBasicParametersTableID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField  HeaderText="Название показателя" >
                        <ItemTemplate>
                            <asp:Label ID="Name"  runat="server" Visible="True" Text='<%# Bind("Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                             
                     <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull0"  runat="server" Visible="false" Text='<%# Bind("NotNull0") %>'></asp:Label>
                            <asp:TextBox ID="Value0" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value0") %>'></asp:TextBox>
                             <asp:RangeValidator runat="server" ID="Validate0" ControlToValidate="Value0" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="ERRorr">
                        </asp:RangeValidator>
                            <asp:Label ID="CollectId0" runat="server" Visible="false" Text='<%# Bind("CollectId0") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  

                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull1"  runat="server" Visible="false" Text='<%# Bind("NotNull1") %>'></asp:Label>
                            <asp:TextBox ID="Value1" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value1") %>'></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="Validate1" ControlToValidate="Value1" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                            <asp:Label ID="CollectId1" runat="server" Visible="false" Text='<%# Bind("CollectId1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>               
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull2"  runat="server" Visible="false" Text='<%# Bind("NotNull2") %>'></asp:Label>
                            <asp:TextBox ID="Value2" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value2") %>'></asp:TextBox>
                             <asp:RangeValidator runat="server" ID="Validate2" ControlToValidate="Value2" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                            <asp:Label ID="CollectId2" runat="server" Visible="false" Text='<%# Bind("CollectId2") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull3"  runat="server" Visible="false" Text='<%# Bind("NotNull3") %>'></asp:Label>
                            <asp:TextBox ID="Value3" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value3") %>'></asp:TextBox>
                             <asp:RangeValidator runat="server" ID="Validate3" ControlToValidate="Value3" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                            <asp:Label ID="CollectId3" runat="server" Visible="false" Text='<%# Bind("CollectId3") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>     
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull4"  runat="server" Visible="false" Text='<%# Bind("NotNull4") %>'></asp:Label>
                            <asp:TextBox ID="Value4" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value4") %>'></asp:TextBox>
                             <asp:RangeValidator runat="server" ID="Validate4" ControlToValidate="Value4" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                            <asp:Label ID="CollectId4" runat="server" Visible="false" Text='<%# Bind("CollectId4") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull5"  runat="server" Visible="false" Text='<%# Bind("NotNull5") %>'></asp:Label>
                            <asp:TextBox ID="Value5" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value5") %>'></asp:TextBox>
                             <asp:RangeValidator runat="server" ID="Validate5" ControlToValidate="Value5" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                            <asp:Label ID="CollectId5" runat="server" Visible="false" Text='<%# Bind("CollectId5") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>               
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull6"  runat="server" Visible="false" Text='<%# Bind("NotNull6") %>'></asp:Label>
                            <asp:TextBox ID="Value6" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value6") %>'></asp:TextBox>
                             <asp:RangeValidator runat="server" ID="Validate6" ControlToValidate="Value6" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                            <asp:Label ID="CollectId6" runat="server" Visible="false" Text='<%# Bind("CollectId6") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull7"  runat="server" Visible="false" Text='<%# Bind("NotNull7") %>'></asp:Label>
                            <asp:TextBox ID="Value7" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value7") %>'></asp:TextBox>
                             <asp:RangeValidator runat="server" ID="Validate7" ControlToValidate="Value7" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                            <asp:Label ID="CollectId7" runat="server" Visible="false" Text='<%# Bind("CollectId7") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>     
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull8"  runat="server" Visible="false" Text='<%# Bind("NotNull8") %>'></asp:Label>
                            <asp:TextBox ID="Value8" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value8") %>'></asp:TextBox>
                             <asp:RangeValidator runat="server" ID="Validate8" ControlToValidate="Value8" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                            <asp:Label ID="CollectId8" runat="server" Visible="false" Text='<%# Bind("CollectId8") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull9"  runat="server" Visible="false" Text='<%# Bind("NotNull9") %>'></asp:Label>
                            <asp:TextBox ID="Value9" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value9") %>'></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="Validate9" ControlToValidate="Value9" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                            <asp:Label ID="CollectId9" runat="server" Visible="false" Text='<%# Bind("CollectId9") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull10"  runat="server" Visible="false" Text='<%# Bind("NotNull10") %>'></asp:Label>
                            <asp:TextBox ID="Value10" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value10") %>'></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="Validate10" ControlToValidate="Value10" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"

                            SetFocusOnError="True">
                        </asp:RangeValidator><asp:Label ID="CollectId10" runat="server" Visible="false" Text='<%# Bind("CollectId10") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                                 
                </Columns>
            </asp:GridView>
            <br />
            <asp:Button ID="ButtonSave" Width="400px" runat="server" Text="Сохранить" OnClick="ButtonSave_Click" />

            <br />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Экспорт в excell" Width="400px" />

            <br />

        </div>
        </asp:Content>
