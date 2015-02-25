<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="SpecializationParametrs.aspx.cs" Inherits="KPIWeb.Reports.SpecializationParametrs" %>
    <asp:Content runat="server" ID="BodyContent"  ContentPlaceHolderID="MainContent">
    <div>   
        <br />
        <br />
        Список специальностей приклепненных к кафедре<br />
    </div>
        <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server">           
             <Columns>               
                 <asp:BoundField DataField="SpecializationID"   HeaderText="Current Report ID" Visible="false" />    
                 <asp:BoundField DataField="SpecializationName" HeaderText="Название специальности" Visible="True" />          
               
                    <asp:TemplateField HeaderText="Param1">
                        <ItemTemplate>
                            <asp:Label ID="Param1Label" runat="server" Text='<%# Bind("Param1Label") %>' Visible="false"></asp:Label>
                            <asp:CheckBox ID="Param1CheckBox" runat="server" CommandName="Select" CommandArgument='<%# Eval("Param1CheckBox") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>     

                     <asp:TemplateField HeaderText="Param2">
                        <ItemTemplate>
                            <asp:Label ID="Param2Label" runat="server" Text='<%# Bind("Param2Label") %>' Visible="false"></asp:Label>
                            <asp:CheckBox ID="Param2CheckBox" runat="server" CommandName="Select" CommandArgument='<%# Eval("Param2CheckBox") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>   

                    <asp:TemplateField HeaderText="Param3">
                        <ItemTemplate>
                            <asp:Label ID="Param3Label" runat="server" Text='<%# Bind("Param3Label") %>' Visible="false"></asp:Label>
                            <asp:CheckBox ID="Param3CheckBox" runat="server" CommandName="Select" CommandArgument='<%# Eval("Param3CheckBox") %>' />
                        </ItemTemplate>
                    </asp:TemplateField> 
                  <asp:TemplateField HeaderText="Param4">
                        <ItemTemplate>
                            <asp:Label ID="Param4Label" runat="server" Text='<%# Bind("Param4Label") %>' Visible="false"></asp:Label>
                            <asp:CheckBox ID="Param4CheckBox" runat="server" CommandName="Select" CommandArgument='<%# Eval("Param4CheckBox") %>' />
                        </ItemTemplate>
                    </asp:TemplateField> 
                  <asp:TemplateField HeaderText="Param5">
                        <ItemTemplate>
                            <asp:Label ID="Param5Label" runat="server" Text='<%# Bind("Param5Label") %>' Visible="false"></asp:Label>
                            <asp:CheckBox ID="Param5CheckBox" runat="server" CommandName="Select" CommandArgument='<%# Eval("Param5CheckBox") %>' />
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