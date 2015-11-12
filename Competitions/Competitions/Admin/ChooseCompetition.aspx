﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ChooseCompetition.aspx.cs" Inherits="Competitions.Admin.ChooseCompetition" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">    <div>
        <br />
        <asp:Button ID="GoBackButton" runat="server" OnClick="GoBackButton_Click" Text="Назад" />
        <br />
       <h2><span style="font-size: 20px">Список всех имеющихся конкурсов: </span></h2>
        <br />
        <asp:Button ID="NewCompetitionButton" runat="server" OnClick="NewCompetitionButton_Click" Text="Создать новый конкурс" Width="200px" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Экспертные группы" Width="200px" />
        <br />
        <br />
        <asp:GridView ID="CompetitionsGV" runat="server" AutoGenerateColumns="False" OnRowDataBound="CompetitionsGVRowDataBound">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                <asp:BoundField DataField="Number"   HeaderText="Шифр конкурса" Visible="true" />
                <asp:BoundField DataField="Name"   HeaderText="Название конкурса" Visible="true" />                
               
                 <asp:TemplateField HeaderText="Создать или редактировать формы конкурса">
                        <ItemTemplate>
                             <asp:Button ID="OpenButton"  runat="server" CommandName="Select"  CommandArgument='<%# Eval("ID") %>'  Width="150px" OnClick="OpenButtonClick"  Text="Открыть" />
                            
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Редактировать информацию о конкурсе">
                        <ItemTemplate>
                             <asp:Button ID="ChangeButton"  runat="server" CommandName="Select" CommandArgument='<%# Eval("ID") %>'  Width="150px" OnClick="ChangeButtonClick"  Text="Редактировать"/>
                             
                        </ItemTemplate>
                </asp:TemplateField>
            
                <asp:TemplateField HeaderText="Статус конкурса">
                        <ItemTemplate>                         
                                <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>'  Visible="True"></asp:Label>
                            <asp:Button ID="StartStopButton" runat="server" CommandName="Select" Text="Изменить статус" CommandArgument='<%# Eval("ID") %>' Width="150px" OnClick="StartStopButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
               <asp:TemplateField HeaderText="Метки в конкурсе">
                        <ItemTemplate>
                             <asp:Button ID="MarksButton"  runat="server" CommandName="Select"  CommandArgument='<%# Eval("ID") %>'  Width="150px" OnClick="MarksButtonClick"   Text="Просмотреть" />
 
                        </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Удалить конкурс">
                        <ItemTemplate>
                             <asp:Button ID="DeleteButton"  runat="server"  CommandArgument='<%# Eval("ID") %>'   OnClick="DeleteButtonClick" Text="Удалить" Width="150px"/>
                           
                        </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
    </div>
</asp:Content>