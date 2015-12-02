﻿<%@ Page Title="Добавление направления подготовки к кафедре" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddSpecialization.aspx.cs" Inherits="KPIWeb.StatisticsDepartment.AddSpecialization" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">   
        <h2><span style="font-size: 30px">Добавление направления подготовки к кафедре, определение параметров</span></h2>
        <div>
  
    <asp:Label ID="Label1" runat="server" Text="Выберите университет" ></asp:Label>
            <br />
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
    </asp:DropDownList>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Выберите факультет"></asp:Label>
            <br />
    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
    </asp:DropDownList>
            <br />
            <asp:Label ID="Label3" runat="server" Text="Выберите кафедру"></asp:Label>
            <br />
            <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
            </asp:DropDownList>
            <br />
            <br />
            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" Text="Кафедра является выпускающей" />
            <br />
            <br />
            <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox2_CheckedChanged" Text="Кафедра является базовой" />
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Text="Список направлений подготовки, прикрепленных к кафедре"></asp:Label>
<asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server" >           
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
                
                 <asp:TemplateField HeaderText="Удалить направление подготовки из списка">
                        <ItemTemplate>
                            <asp:Label ID="DeleteSpecializationLabel" runat="server" Text='<%# Bind("DeleteSpecializationLabel") %>' Visible="false"></asp:Label>
                            <asp:Button ID="DeleteSpecializationButton" runat="server" CommandName="Select" Text="Удалить" Width="200px" CommandArgument='<%# Eval("FourthlvlId") %>' OnClick="DeleteSpecializationButtonClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>   
                                           
       
                </Columns>
       </asp:GridView>
            <br />
            <br />
        <asp:Button ID="Button1" runat="server" Text="Сохранить" Width="702px" OnClick="Button1_Click" />
            <br />
            <br />
        <asp:Label ID="Label5" runat="server" Text="Поиск направления подготовки по коду и названию"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Width="271px"></asp:TextBox>
        <br />
        <asp:Button ID="Button2" runat="server" Text="Поиск" Width="277px" OnClick="Button2_Click" />
            <br />
            <br />
            </div>
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