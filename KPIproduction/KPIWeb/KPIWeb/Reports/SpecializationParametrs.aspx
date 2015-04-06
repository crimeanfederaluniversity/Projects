<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="SpecializationParametrs.aspx.cs" Inherits="KPIWeb.Reports.SpecializationParametrs" %>
    <asp:Content runat="server" ID="BodyContent"  ContentPlaceHolderID="MainContent">
        <div>   
            <span style="font-size: 30px">Добавление направлений подготовки, определение параметров</span><br />
        <br />
        <asp:CheckBox ID="CheckBox1" runat="server" Text="Кафедра является выпускающей" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Список направлений подготовки, приклепненных к кафедре"></asp:Label>
        <br />
    </div>
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
                            <asp:Checkbox  Text=" "  ID="IsModern" runat="server" CommandName="Select" CommandArgument='<%# Eval("Param1CheckBox") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>     

                     <asp:TemplateField HeaderText="Осуществляется сетевое взаимодействие"  HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:Label ID="ParamLabel2" runat="server" Text='<%# Bind("Param2Label") %>' Visible="false"></asp:Label>
                            <asp:Checkbox  Text=" "  ID="IsNetwork" runat="server" CommandName="Select" CommandArgument='<%# Eval("Param2CheckBox") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>   

                    <asp:TemplateField HeaderText="Осуществляется обучение студентов с особыми потребностяим" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:Label ID="ParamLabel3" runat="server" Text='<%# Bind("Param3Label") %>' Visible="false"></asp:Label>
                            <asp:Checkbox  Text=" "  ID="IsInvalid" runat="server" CommandName="Select" CommandArgument='<%# Eval("Param3CheckBox") %>' />
                        </ItemTemplate>
                    </asp:TemplateField> 
                     
                  <asp:TemplateField HeaderText="Предусмотрено обучение иностранных студентов" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:Label ID="ParamLabel4" runat="server" Text='<%# Bind("Param4Label") %>' Visible="false"></asp:Label>
                            <asp:Checkbox  Text=" "  ID="IsForeign" runat="server" CommandName="Select" CommandArgument='<%# Eval("Param4Label") %>' />
                        </ItemTemplate>
                    </asp:TemplateField> 
                 
                  <asp:TemplateField HeaderText="Param5" Visible = "False">
                        <ItemTemplate>
                            <asp:Label ID="ParamLabel5" runat="server" Text='<%# Bind("Param5Label") %>' Visible="false"></asp:Label>
                            <asp:Checkbox  Text=" "  ID="ParamCheckBox5" runat="server" CommandName="Select" CommandArgument='<%# Eval("Param5CheckBox") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>      
                 
                 <asp:TemplateField HeaderText="Удалить направление подготовки из списка">
                        <ItemTemplate>
                            <asp:Label ID="DeleteSpecializationLabel" runat="server" Text='<%# Bind("DeleteSpecializationLabel") %>' Visible="false"></asp:Label>
                            <asp:Button ID="DeleteSpecializationButton" runat="server" CommandName="Select" Text="Удалить" Width="200px" CommandArgument='<%# Eval("FourthlvlId") %>' OnClick="DeleteSpecializationButtonClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>   
                                           
       
                </Columns>
       </asp:GridView>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Сохранить" Width="702px" OnClick="Button1_Click" />
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Поиск направления подготовки по коду и названию"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Width="271px"></asp:TextBox>
        <br />
        <asp:Button ID="Button2" runat="server" Text="Поиск" Width="277px" OnClick="Button2_Click" />
        <br />
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False">
             <Columns>           
                 <asp:BoundField DataField="SpecializationID"   HeaderText="Current Report ID" Visible="false" />  
                 <asp:BoundField DataField="SpecializationNumber" HeaderText="Код направления подготовки" Visible="True" />   
                 <asp:BoundField DataField="SpecializationName" HeaderText="Название направления подготовки" Visible="True" /> 
                 <asp:TemplateField HeaderText="Добавление направления подготовки">
                        <ItemTemplate>
                            <asp:Label ID="AddSpecializationLabel" runat="server" Text='<%# Bind("AddSpecializationLabel") %>' Visible="false"></asp:Label>
                            <asp:Button ID="AddSpecializationButton" runat="server" CommandName="Select" Text="Добавить" Width="200px" CommandArgument='<%# Eval("SpecializationNumber") %>' OnClick="AddSpecializationButtonClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>            
               </Columns>              

        </asp:GridView>
       </asp:Content>