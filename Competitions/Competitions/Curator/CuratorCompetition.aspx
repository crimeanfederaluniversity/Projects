<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CuratorCompetition.aspx.cs" Inherits="Competitions.Curator.CuratorCompetition" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">   
     <div>
              <br />
            <asp:Button ID="GoBackButton" runat="server"  Text="Назад" Width="131px" style="height: 26px" OnClick="GoBackButton_Click" />
        <br />  
          <h2><span style="font-size: 20px">Конкурсы, куратором которых вы являетесь: </span></h2>
         <br />  
        <asp:Button ID="NewCompetitionButton" runat="server" OnClick="NewCompetitionButton_Click" Text="Создать новый конкурс" />
        <br />
        <br />
        <asp:GridView ID="CompetitionsGV" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                <asp:BoundField DataField="Number"   HeaderText="Шифр конкурса" Visible="true" />
                <asp:BoundField DataField="Name"   HeaderText="Название" Visible="true" />                
                <asp:BoundField DataField="Budjet"   HeaderText="Бюджет" Visible="true" />
                <asp:BoundField DataField="StartDate"   HeaderText="Дата начала" Visible="true" />
                <asp:BoundField DataField="EndDate"   HeaderText="Дата окончания" Visible="true" />
                 <asp:BoundField DataField="Sended"   HeaderText="Статус" Visible="true" />
                <asp:TemplateField HeaderText="Редактировать информацию о конкурсе">
                        <ItemTemplate>
                            <asp:Button ID="ChangeButton" runat="server" CommandName="Select" Text="Редактировать" CommandArgument='<%# Eval("ID") %>' Width="150px" OnClick="ChangeButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
              <asp:TemplateField HeaderText="Отправить на рассмотрение">
                        <ItemTemplate>
                            <asp:Button ID="OpenButton" runat="server" CommandName="Select" Text="Отправить" OnClientClick="return confirm('Вы уверены, что хотите отправить конкурс на рассмотрение?');" CommandArgument='<%# Eval("ID") %>' Width="150px" OnClick="OpenButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
               
            </Columns>
        </asp:GridView>
        <br />
    </div>
</asp:Content>