<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EvaluateApplication.aspx.cs" Inherits="Competitions.Expert.EvaluateApplication" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
        <asp:Button ID="GoBackButton" runat="server" OnClick="GoBackButton_Click" Text="Назад" />
     <br />
    <h2><span style="font-size: 30px">Основные критерии оценки заявки: </span></h2>
    <br />
    <asp:GridView ID="EvaluateGV"  BorderStyle="Solid" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound"
                BorderColor="Black"  BorderWidth="1px" CellPadding="0" EnableTheming="True">
               <Columns>         
                     
                     <asp:BoundField DataField="Name"   HeaderText="Название" Visible="true" />
                     <asp:TemplateField Visible="true"   HeaderText="Значение">
                        <ItemTemplate > 
                            <asp:Label      ID="ID" runat="server"  Visible="false"  Text='<%# Bind("ID") %>'></asp:Label>       
                                          
                            <asp:TextBox ID="ValueTextBox" runat="server" Text='<%# Bind("Text") %>' ></asp:TextBox>  
                            <asp:RangeValidator runat="server" ID="TextBoxValidate1" ControlToValidate="ValueTextBox" 
                                Enabled =    "true"
                                MaximumValue= '<%# Bind("MaxValue") %>' 
                                MinimumValue='<%# Bind("MinValue") %>' 
                                Text='<%# Bind("ErrorText") %>' 

                                Type=        "Integer"
                            ErrorMessage='<%# Bind("ErrorText") %>'  ForeColor="Red" Display="Dynamic" 
                            SetFocusOnError="True">
                        </asp:RangeValidator>                                              
                        </ItemTemplate>
                    </asp:TemplateField>  
                   </Columns>
     </asp:GridView>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <br />
    <p>
        Комментарий эксперта</p>
    <p>
        <asp:TextBox ID="CommentTextBox" runat="server" Height="113px" TextMode="MultiLine" Width="630px"></asp:TextBox>
    </p>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Сохранить" Width="154px" OnClick="Button1_Click" />
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" OnClientClick="return confirm('Вы уверены, что хотите отправить на рассмотрение? После отправки возможности редактировать не будет!');" Text="Отправить на рассмотрение" Width="241px" />
    <br />
</asp:Content>
