﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="SpecializationParametrs.aspx.cs" Inherits="KPIWeb.Reports.SpecializationParametrs" %>
    <asp:Content runat="server" ID="BodyContent"  ContentPlaceHolderID="MainContent">
    <div>   
        <br />
        <br />
        Список специальностей приклепненных к кафедре<br />
    </div>
        <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server" OnRowDataBound="GridView1_RowDataBound">           
             <Columns>               
                 <asp:BoundField DataField="SpecializationID"   HeaderText="Current Report ID" Visible="false" />    
                 <asp:BoundField DataField="SpecializationName" HeaderText="Название специальности" Visible="True" />         
               
                   
                 <asp:TemplateField HeaderText="Id специальности" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="FourthlvlId" runat="server" Text='<%# Bind("FourthlvlId") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>   

                   <asp:TemplateField HeaderText="Совеременные образовательные технологии" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="ParamLabel1" runat="server" Text='<%# Bind("Param1Label") %>' Visible="false"></asp:Label>
                            <asp:CheckBox ID="IsModern" runat="server" CommandName="Select" CommandArgument='<%# Eval("Param1CheckBox") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>     

                     <asp:TemplateField HeaderText="Интернет" Visible = "True">
                        <ItemTemplate>
                            <asp:Label ID="ParamLabel2" runat="server" Text='<%# Bind("Param2Label") %>' Visible="false"></asp:Label>
                            <asp:CheckBox ID="IsNetwork" runat="server" CommandName="Select" CommandArgument='<%# Eval("Param2CheckBox") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>   

                    <asp:TemplateField HeaderText="С ограничеными возможнос" Visible = "True">
                        <ItemTemplate>
                            <asp:Label ID="ParamLabel3" runat="server" Text='<%# Bind("Param3Label") %>' Visible="false"></asp:Label>
                            <asp:CheckBox ID="IsInvalid" runat="server" CommandName="Select" CommandArgument='<%# Eval("Param3CheckBox") %>' />
                        </ItemTemplate>
                    </asp:TemplateField> 
                     
                  <asp:TemplateField HeaderText="Иностранные" Visible = "True">
                        <ItemTemplate>
                            <asp:Label ID="ParamLabel4" runat="server" Text='<%# Bind("Param4Label") %>' Visible="false"></asp:Label>
                            <asp:CheckBox ID="IsForeign" runat="server" CommandName="Select" CommandArgument='<%# Eval("Param4Label") %>' />
                        </ItemTemplate>
                    </asp:TemplateField> 
                 
                  <asp:TemplateField HeaderText="Param5" Visible = "False">
                        <ItemTemplate>
                            <asp:Label ID="ParamLabel5" runat="server" Text='<%# Bind("Param5Label") %>' Visible="false"></asp:Label>
                            <asp:CheckBox ID="ParamCheckBox5" runat="server" CommandName="Select" CommandArgument='<%# Eval("Param5CheckBox") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>      
                 
                 <asp:TemplateField HeaderText="Удалить специальность из списка">
                        <ItemTemplate>
                            <asp:Label ID="DeleteSpecializationLabel" runat="server" Text='<%# Bind("DeleteSpecializationLabel") %>' Visible="false"></asp:Label>
                            <asp:Button ID="DeleteSpecializationButton" runat="server" CommandName="Select" Text="Удалить" Width="200px" CommandArgument='<%# Eval("SpecializationID") %>' OnClick="DeleteSpecializationButtonClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>   
                                           
       
                </Columns>
       </asp:GridView>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Сохранить" Width="702px" OnClick="Button1_Click" />
        <br />
        <br />
        Поиск по коду и названию специальност<br />
        <asp:TextBox ID="TextBox1" runat="server" Width="271px"></asp:TextBox>
        <br />
        <asp:Button ID="Button2" runat="server" Text="Поиск" Width="277px" OnClick="Button2_Click" />
        <br />
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False">
             <Columns>           
                 <asp:BoundField DataField="SpecializationID"   HeaderText="Current Report ID" Visible="false" />  
                 <asp:BoundField DataField="SpecializationNumber" HeaderText="Код специальности" Visible="True" />   
                 <asp:BoundField DataField="SpecializationName" HeaderText="Название специальности" Visible="True" /> 
                 <asp:TemplateField HeaderText="Добавление специальности">
                        <ItemTemplate>
                            <asp:Label ID="AddSpecializationLabel" runat="server" Text='<%# Bind("AddSpecializationLabel") %>' Visible="false"></asp:Label>
                            <asp:Button ID="AddSpecializationButton" runat="server" CommandName="Select" Text="Добавить" Width="200px" CommandArgument='<%# Eval("SpecializationNumber") %>' OnClick="AddSpecializationButtonClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>            
               </Columns>              

        </asp:GridView>
       </asp:Content>