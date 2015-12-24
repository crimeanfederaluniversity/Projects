<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="StudentsTasks.aspx.cs" Inherits="PersonalPages.StudentsTasks" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
     <span style="font-size: large">Мои задания:</span><span style="font-size: x-large"><br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Height="52px" Width="307px">
               <Columns>    
            <asp:TemplateField Visible="false"   HeaderText="Номер" >
                        <ItemTemplate>
                            <asp:Label ID="LabelID" runat="server"  Visible="false" Text='<%# Bind("ID") %>'  ></asp:Label> 
                               </ItemTemplate>
                    </asp:TemplateField> 
             <asp:TemplateField Visible="true"   HeaderText="Название дисциплины">
                        <ItemTemplate>
                            <asp:Label ID="Name" runat="server"  Visible="true" Text='<%# Bind("Name") %>'  ></asp:Label> 
                               </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Дата сдачи">
                        <ItemTemplate>  
                            <asp:Label ID="Date" runat="server"  Visible="true" Text='<%# Bind("Date") %>'  ></asp:Label> 
                        </ItemTemplate>
                    </asp:TemplateField>    
                   <asp:TemplateField HeaderText="Задание">
                        <ItemTemplate>  
                           <asp:Button ID="WatchButton" runat="server" CommandName="Select" Text="Скачать задание" Width="200px" CommandArgument='<%# Eval("WatchButton") %>' OnClick="WatchButtonClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>                            
                      </Columns>    
        </asp:GridView>
</span></asp:Content>