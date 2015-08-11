<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master"  CodeBehind="DAllInOne.aspx.cs" Inherits="KPIWeb.Director.DAllInOne" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server"> 
    
    
        <style type="text/css">
   .button_right 
   {
       float:right
   }     
</style> 
<asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
	<div>    
		  <asp:Button ID="GoBackButton" runat="server" OnClientClick="JavaScript:window.location.href='../Director/DReportView'; return false; showLoadPanel();"  Text="Назад" Width="125px" Enabled="True" />
		  <asp:Button ID="GoForwardButton" runat="server" OnClientClick="JavaScript:window.history.forward(1); return false; showLoadPanel();"  Text="Вперед" Width="125px" />
		  <asp:Button ID="Button22" OnClientClick="showLoadPanel()" runat="server"  Text="На главную" Width="125px" Enabled="True" OnClick="Button22_Click" />
		  <asp:Button ID="Button5" runat="server" OnClientClick="showLoadPanel()" CssClass="button_right" OnClick="Button5_Click" Text="Нормативные документы" Width="300px" />
	</div>
</asp:Panel>
    
    
     
    <br />
        <br />

    <H2> Сводная таблица значений показателей.</H2>

&nbsp;<br />
    <br />
    <asp:GridView ID="GridviewCollectedBasicParameters"  BorderStyle="Solid" runat="server" CssClass="Grid_view_V_style Grid_view_style GridHeader" ShowFooter="true" AutoGenerateColumns="False" 
                BorderColor="Black"  BorderWidth="1px" CellPadding="0" >
            
                <Columns>                  
                     <asp:BoundField DataField="BasicParametersTableID" HeaderText="Код показателя" Visible="true" />                              
                     <asp:BoundField DataField="Name"  HeaderText="Название показателя" Visible="true" />
                     <asp:BoundField DataField="Comment"  HeaderText="Комментарий" Visible="true" />
                                
                     <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value0"  Width="95%" style="text-align:center;" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value0") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false"  HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value1" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value1") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>               
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value2" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value2") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value3" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value3") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>     
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value4" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value4") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value5" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value5") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>               
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value6" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value6") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value7" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value7") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>     
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value8" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value8") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>                   
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value9" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value9") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value10" Width="95%" style="text-align:center"  ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value10") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>     
                     <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value11" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value11") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value12" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value12") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value13" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value13") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value14" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value14") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value15" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value15") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value16" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value16") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value17" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value17") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value18" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value18") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value19" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value19") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value20" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value20") %>'></asp:TextBox>                           
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value21" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value21") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value22" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value22") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value23" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value23") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value24" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value24") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value25" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value25") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField>                            
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value26" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value26") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField>            
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value27" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value27") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value28" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value28") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value29" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value29") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value30" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value30") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value31" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value31") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value32" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value32") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value33" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value33") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value34" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value34") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value35" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value35") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value36" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value36") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value37" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value37") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value38" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value38") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value39" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value39") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value40" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value40") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value41"  Width="95%" style="text-align:center;" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value41") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false"  HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value42" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value42") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>               
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value43" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value43") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value44" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value44") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>     
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value45" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value45") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value46" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value46") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>               
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value47" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value47") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value48" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value48") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>     
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value49" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value49") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>                   
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value50" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value50") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="Value51" Width="95%" style="text-align:center"  ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value51") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>     
                     <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value52" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value52") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value53" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value53") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value54" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value54") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value55" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value55") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value56" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value56") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value57" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value57") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value58" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value58") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value59" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value59") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value60" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value60") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value61" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value61") %>'></asp:TextBox>                           
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value62" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value62") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value63" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value63") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value64" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value64") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value65" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value65") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value66" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value66") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField>                            
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value67" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value67") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField>            
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value68" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value68") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value69" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value69") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value70" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value70") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value71" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value71") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value72" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value72") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value73" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value73") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value74" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value74") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value75" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value75") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value76" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value76") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value77" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value77") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value78" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value78") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value79" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value79") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value80" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value80") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:TextBox ID="Value81" Width="95%" style="text-align:center" ReadOnly="true" BackColor="Transparent" BorderWidth="0" runat="server" Text='<%# Bind("Value81") %>'></asp:TextBox>                            
                        </ItemTemplate>
                    </asp:TemplateField> 
                </Columns>
            </asp:GridView>
    <br />

    <style type="text/css">
   .button_right 
   {
       float:right
   }     
</style> 
<asp:Panel runat="server" ID="Panel1" CssClass="top_panel" Height="40" Visible="true">    
	<div>    
		  <asp:Button ID="Button1" runat="server" OnClientClick="JavaScript:window.history.back(1); return false; showLoadPanel();"  Text="Назад" Width="125px" Enabled="True" />
		  <asp:Button ID="Button2" runat="server" OnClientClick="JavaScript:window.history.forward(1); return false; showLoadPanel();"  Text="Вперед" Width="125px" />
		  <asp:Button ID="Button3" OnClientClick="showLoadPanel()" runat="server"  Text="На главную" Width="125px" Enabled="True" OnClick="Button22_Click" />
		  <asp:Button ID="Button4" runat="server" OnClientClick="showLoadPanel()" CssClass="button_right" OnClick="Button5_Click" Text="Нормативные документы" Width="300px" />
	</div>
</asp:Panel>



        <script type="text/javascript">
            function showLoadPanel() {
                document.getElementById('LoadPanel_').style.visibility = 'visible';
            }
    </script>
    <style>  
        body {
        top: 50px;
    }
        .LoadPanel 
   {
          position: fixed;
          z-index: 10;
          background-color: whitesmoke;
          top: 0;
          left: 0;
          width: 100%;
          height: 100%;
          opacity: 0.9;
          visibility: hidden;
   }
        
#floatingCirclesG{
position:relative;
width:128px;
height:128px;
-moz-transform:scale(0.6);
-webkit-transform:scale(0.6);
-ms-transform:scale(0.6);
-o-transform:scale(0.6);
transform:scale(0.6);

z-index:11;
top: 50%;
left: 50%;
        margin-top: -64px;
        margin-left: -64px;
}

.f_circleG{
position:absolute;
background-color:#FFFFFF;
height:23px;
width:23px;
-moz-border-radius:12px;
-moz-animation-name:f_fadeG;
-moz-animation-duration:1.04s;
-moz-animation-iteration-count:infinite;
-moz-animation-direction:normal;
-webkit-border-radius:12px;
-webkit-animation-name:f_fadeG;
-webkit-animation-duration:1.04s;
-webkit-animation-iteration-count:infinite;
-webkit-animation-direction:normal;
-ms-border-radius:12px;
-ms-animation-name:f_fadeG;
-ms-animation-duration:1.04s;
-ms-animation-iteration-count:infinite;
-ms-animation-direction:normal;
-o-border-radius:12px;
-o-animation-name:f_fadeG;
-o-animation-duration:1.04s;
-o-animation-iteration-count:infinite;
-o-animation-direction:normal;
border-radius:12px;
animation-name:f_fadeG;
animation-duration:1.04s;
animation-iteration-count:infinite;
animation-direction:normal;
}

#frotateG_01{
left:0;
top:52px;
-moz-animation-delay:0.39s;
-webkit-animation-delay:0.39s;
-ms-animation-delay:0.39s;
-o-animation-delay:0.39s;
animation-delay:0.39s;
}

#frotateG_02{
left:15px;
top:15px;
-moz-animation-delay:0.52s;
-webkit-animation-delay:0.52s;
-ms-animation-delay:0.52s;
-o-animation-delay:0.52s;
animation-delay:0.52s;
}

#frotateG_03{
left:52px;
top:0;
-moz-animation-delay:0.65s;
-webkit-animation-delay:0.65s;
-ms-animation-delay:0.65s;
-o-animation-delay:0.65s;
animation-delay:0.65s;
}

#frotateG_04{
right:15px;
top:15px;
-moz-animation-delay:0.78s;
-webkit-animation-delay:0.78s;
-ms-animation-delay:0.78s;
-o-animation-delay:0.78s;
animation-delay:0.78s;
}

#frotateG_05{
right:0;
top:52px;
-moz-animation-delay:0.91s;
-webkit-animation-delay:0.91s;
-ms-animation-delay:0.91s;
-o-animation-delay:0.91s;
animation-delay:0.91s;
}

#frotateG_06{
right:15px;
bottom:15px;
-moz-animation-delay:1.04s;
-webkit-animation-delay:1.04s;
-ms-animation-delay:1.04s;
-o-animation-delay:1.04s;
animation-delay:1.04s;
}

#frotateG_07{
left:52px;
bottom:0;
-moz-animation-delay:1.17s;
-webkit-animation-delay:1.17s;
-ms-animation-delay:1.17s;
-o-animation-delay:1.17s;
animation-delay:1.17s;
}

#frotateG_08{
left:15px;
bottom:15px;
-moz-animation-delay:1.3s;
-webkit-animation-delay:1.3s;
-ms-animation-delay:1.3s;
-o-animation-delay:1.3s;
animation-delay:1.3s;
}

@-moz-keyframes f_fadeG{
0%{
background-color:#000000}

100%{
background-color:#FFFFFF}

}

@-webkit-keyframes f_fadeG{
0%{
background-color:#000000}

100%{
background-color:#FFFFFF}

}

@-ms-keyframes f_fadeG{
0%{
background-color:#000000}

100%{
background-color:#FFFFFF}

}

@-o-keyframes f_fadeG{
0%{
background-color:#000000}

100%{
background-color:#FFFFFF}

}

@keyframes f_fadeG{
0%{
background-color:#000000}

100%{
background-color:#FFFFFF}

}
</style>     
    <div id="LoadPanel_" class='LoadPanel'>               
            <div id="floatingCirclesG">
            <div class="f_circleG" id="frotateG_01">
            </div>
            <div class="f_circleG" id="frotateG_02">
            </div>
            <div class="f_circleG" id="frotateG_03">
            </div>
            <div class="f_circleG" id="frotateG_04">
            </div>
            <div class="f_circleG" id="frotateG_05">
            </div>
            <div class="f_circleG" id="frotateG_06">
            </div>
            <div class="f_circleG" id="frotateG_07">
            </div>
            <div class="f_circleG" id="frotateG_08">
            </div>
            </div>
        </div>


        <script type="text/javascript" >
            var counter = 0;
            var masoftables = [];
            function parseGW() {
                // document.body.style.backgroundColor = gw_name;
                windowheighter = window.screen.availWidth;
                $(window).load(function () {

                    var texts = "def";
                    var i = 0;
                    var ownerdiv = [];
                    var cthead = "ControlHead";

                    var gridHeader;
                    var s;
                    for (i = 0; i < document.all.length; i++) {
                        if (((document.all[i].tagName.toString()).toLowerCase()) == 'table') {
                            masoftables[counter] = document.all[i].getAttribute("id").toString();
                            counter++;
                        }
                    }
                    $('#cdf').text(masoftables[1]);
                    for (i = 0; i < counter; i++) {

                        ownerdiv[i] = "diva"; cthead = "ControlHead";
                        ownerdiv[i] += '_' + i.toString();
                        cthead += '_' + i.toString();
                        $('#' + masoftables[i]).wrap(("<div id='" + ownerdiv[i] + "' style='position:absolute;'></div>"));
                        $(('#' + ownerdiv[i])).prepend(("<div id='" + cthead + "'></div>"));

                        $('#' + ownerdiv[i]).wrap(("<div id='" + "pre_" + ownerdiv[i] + "' style='position:absolute;'></div>"));
                        $("#pre_" + ownerdiv[i]).css('position', 'relative');
                        $("#pre_" + ownerdiv[i]).css('height', (document.getElementById("diva_" + (i).toString()).getBoundingClientRect().bottom - document.getElementById("diva_" + (i).toString()).getBoundingClientRect().top).toString() + "px");

                        // if (i == 0) { $('#' + ownerdiv).before("<div id ='SHEET' style='position:relative;'>"); }
                        // if ((i + 1) == counter) { $('#' + ownerdiv).after("</div>"); }
                        //  if (i > 0) { $(("#" + ownerdiv[i])).css('top', (document.getElementById(ownerdiv[i - 1]).getBoundingClientRect().bottom + window.pageYOffset + (window.pageYOffset - document.getElementById(ownerdiv[i - 1]).getBoundingClientRect().top)).toString() + "px"); }


                        gridHeader = $('#' + masoftables[i]).clone(true);
                        // document.getElementById(masoftables[i]).id = "FF_" + i.toString();
                        $(gridHeader).find("tr:gt(0)").remove();
                        // $(gridHeader).id = "FFF";

                        $('#' + masoftables[i] + ' tr th').each(function (s) {

                            $("th:nth-child(" + (s + 1) + ")", gridHeader).css('width', ($(this).width() +21).toString() + "px");
                        });


                        $(("#" + cthead)).append(gridHeader);
                        document.getElementById(masoftables[i]).id = "ffg" + i.toString();
                        $(("#" + cthead)).css('position', 'absolute');

                        $(("#" + cthead)).css('z-index', '222');
                        //  $(("#" + cthead + " tr")).css('left', ($('#' + 'ffg' + i.toString()).offset().left + 1).toString() + "px");



                        // $('#cdf').text(i.toString());
                    }
                    //$('#cdf').text((((document.getElementById("diva_" + (0).toString()).getBoundingClientRect().bottom - document.getElementById("diva_" + (0).toString()).getBoundingClientRect().top).toString() + "px")));
                    //$('#cdf').text(gridHeader.id.toString());
                    $("#aroundgwsdivALL").css('position', 'relative');


                });

            }

            parseGW();
            mtb();
            function rth() {
                var cthead = "ControlHead";


                for (i = 0; i < counter; i++) {
                    cthead += '_' + i.toString();
                    $('#' + masoftables[i] + ' tr th').each(function (f) {
                        cthead = "ControlHead";
                        cthead += '_' + i.toString();
                        //$("th:nth-child(" + (s + 1) + ")", $("#" + cthead)).css('width', ($(this).width() + 3).toString() + "px");
                        // $("th:nth-child(" + (s + 1) + ")", $("#" + cthead)).css('color', "#ff0000");
                        if (($('#' + masoftables[i] + ' tr th:nth-child(' + ((f + 1).toString()) + ')').width()) != ($('#' + cthead + ' tr th:nth-child(' + ((f + 1).toString()) + ')').width()))
                        { $("#" + cthead + " tr th:nth-child(" + ((f + 1).toString()) + ")").css('width', (($('#' + masoftables[i] + ' tr th:nth-child(' + ((f + 1).toString()) + ')').width() + 21).toString()) + "px"); }
                        //$('#cdf').text("ws= "+window.screen.availWidth.toString() + "\nwh= " + windowheighter.toString());

                    });
                    // if (i == 0) break;
                }
                //document.body.style.backgroundColor = "#80ff00";
                //  windowheighter = window.screen.availWidth;
                $('#cdf').text(($('#' + masoftables[0] + ' tr th:nth-child(' + ((2).toString()) + ')').width().toString()));



            }

            var windowheighter;
            function mtb() {
                window.setTimeout("mtb()", 50);

                // if (window.screen.availWidth != windowheighter) rth();
                rth();

                //$('#cdf').text("WW " + window.screen.availWidth + "\n\n"+"SW " + screen.width);
                //$('#cdf').text(((document.getElementById("diva_0").getBoundingClientRect().right - document.getElementById("diva_0").getBoundingClientRect().left).toString()));
                for (i = 0; i < counter; i++) {
                    ownerdiv = "diva"; cthead = "ControlHead";
                    ownerdiv += '_' + i.toString();
                    cthead += '_' + i.toString();

                    if ((document.getElementById(ownerdiv).getBoundingClientRect().top) < 90) {


                        if ((document.getElementById(ownerdiv).getBoundingClientRect().bottom) < 140) {
                            $('#' + cthead).css('position', 'absolute');
                        }
                        else {
                            $('#' + cthead).css('position', 'fixed');
                            $('#' + cthead).css('width', (((document.getElementById(ownerdiv).getBoundingClientRect().right - document.getElementById(ownerdiv).getBoundingClientRect().left).toString()) + "px"));
                            $('#' + cthead).css('top', '88px');
                            $('#' + cthead).css('left', ((document.getElementById(ownerdiv).getBoundingClientRect().left + 1).toString()) + "px");
                        }
                    }
                    else {
                        $('#' + cthead).css('position', 'absolute');
                        $('#' + cthead).css('width', (((document.getElementById(ownerdiv).getBoundingClientRect().right - document.getElementById(ownerdiv).getBoundingClientRect().left).toString()) + "px"));
                        $('#' + cthead).css('left', ((0).toString()) + "px");
                        $('#' + cthead).css('top', '0');
                    }


                }
            }

       </script>
</asp:Content>
