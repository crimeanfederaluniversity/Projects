<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZapolnenieForm.aspx.cs"  EnableEventValidation="false"  MasterPageFile="~/Site.Master" Inherits="Competition.ZapolnenieForm" %>

 
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent" >
     <h2><span style="font-size: 30px">Заполнение заявки на конкурс</span></h2>
     <div>
    
        &nbsp;
    
        <br />
    
    </div>
        <br />
        <asp:GridView ID="GridView1" AutoGenerateColumns="False" OnRowDataBound="Questions_RowDataBound" runat="server">
             <Columns>
                    <asp:BoundField DataField="ID_Question" HeaderText="Код вопроса" Visible="false" />
                    <asp:BoundField DataField="Question" HeaderText="Вопрос" Visible="true" />               
                  <asp:TemplateField Visible="true"   HeaderText="Ответ">
                        <ItemTemplate >
                         <asp:TextBox ID="Answer" style="text-align:center" BorderWidth="0" Height="95%" runat="server" Text='<%# Bind("Answer") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>  
                            
                    <asp:TemplateField Visible="false" HeaderText="Id" >
                        <ItemTemplate>
                        <asp:label ID="Id" runat="server" Visible="false" Text='<%# Bind("Id") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                 </Columns>
        </asp:GridView>
         <br />
         <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Сохранить" />
         &nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Далее" />
        <br />
        <br />
        <br />
  </asp:Content>