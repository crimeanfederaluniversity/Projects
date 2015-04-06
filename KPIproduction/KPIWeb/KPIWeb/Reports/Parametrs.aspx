<%@ Page Title="Параметры" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Parametrs.aspx.cs" Inherits="KPIWeb.Reports.Parametrs" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <span style="font-size: 30px">Настройка параметров напраления подготовки<br />
        </span><br />
    <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server" OnRowDataBound="GridView1_RowDataBound">           
             <Columns>               
                 <asp:BoundField DataField="SpecializationID"   HeaderText="Current Report ID" Visible="false" />    
                 <asp:BoundField DataField="SpecializationName" HeaderText="Название направления подготовки" Visible="True" />         
               
                   
                 <asp:TemplateField HeaderText="Id направления подготовки" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "False" >
                        <ItemTemplate> 
                            <asp:Label ID="FourthlvlId" runat="server" Text='<%# Bind("FourthlvlId") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>   
                 
                  <asp:TemplateField HeaderText="Номер направления подготовки" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="SpecNumber" runat="server" Text='<%# Bind("SpecNumber") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  

                   <asp:TemplateField HeaderText="Используются совеременные образовательные технологии" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="ParamLabel1" runat="server" Text='<%# Bind("Param1Label") %>' Visible="false"></asp:Label>
                            <asp:Checkbox  Text=" " ID="IsModern" runat="server" CommandName="Select" CommandArgument='<%# Eval("Param1CheckBox") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>     

                     <asp:TemplateField HeaderText="Осуществляется сетевое взаимодействие"  HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:Label ID="ParamLabel2" runat="server" Text='<%# Bind("Param2Label") %>' Visible="false"></asp:Label>
                            <asp:Checkbox  Text=" " ID="IsNetwork" runat="server" CommandName="Select" CommandArgument='<%# Eval("Param2CheckBox") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>   

                    <asp:TemplateField HeaderText="Осуществляется обучение студентов с особыми потребностяим" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:Label ID="ParamLabel3" runat="server" Text='<%# Bind("Param3Label") %>' Visible="false"></asp:Label>
                            <asp:Checkbox  Text=" " ID="IsInvalid" runat="server" CommandName="Select" CommandArgument='<%# Eval("Param3CheckBox") %>' />
                        </ItemTemplate>
                    </asp:TemplateField> 
                     
                  <asp:TemplateField HeaderText="Предусмотрено обучение иностранных студентов" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:Label ID="ParamLabel4" runat="server" Text='<%# Bind("Param4Label") %>' Visible="false"></asp:Label>
                            <asp:Checkbox  Text=" " ID="IsForeign" runat="server" CommandName="Select" CommandArgument='<%# Eval("Param4Label") %>' />
                        </ItemTemplate>
                    </asp:TemplateField> 
                 
                  <asp:TemplateField HeaderText="Param5" Visible = "False">
                        <ItemTemplate>
                            <asp:Label ID="ParamLabel5" runat="server" Text='<%# Bind("Param5Label") %>' Visible="false"></asp:Label>
                            <asp:Checkbox  Text=" " ID="ParamCheckBox5" runat="server" CommandName="Select" CommandArgument='<%# Eval("Param5CheckBox") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>      
                    
                                           
       
                </Columns>
       </asp:GridView>
        
                <br />
        
                <asp:Button ID="Button1" runat="server" Text="Сохранить и перейти к заполнению" Width="702px" OnClick="Button1_Click" />
        
    </div>
</asp:Content>
