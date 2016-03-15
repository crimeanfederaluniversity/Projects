<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DocumentView.aspx.cs" Inherits="EDM.edm.DocumentView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
 
    <div class="row centered-content">
        <div class="edm-document-view-content input-group">
            <asp:Label ID="Label1" runat="server" Text="Документы" CssClass="header"></asp:Label>
            <br />
   
    
            <asp:GridView ID="docGridView" runat="server" AutoGenerateColumns="False" OnRowCommand="docGridView_RowCommand" CssClass="table table-striped edm-table">
            </asp:GridView>
    
            <br />

  
                    Комментарий инициатора согласования  
                <asp:TextBox ID="LabelComment" TextMode="MultiLine" runat="server" ReadOnly="True" Height="300px" Width="600px" Text="Label" CssClass="edm-document-view-comment-block"></asp:TextBox>

           

            <br />
            <br />
            
        <%--<asp:TextBox ID="CommentTextBox" runat="server" TextMode="MultiLine" Height="54px" Width="160px"></asp:TextBox>--%> 
            <div class="input-group-lg">
                <asp:TextBox ID="CommentTextBox" runat="server" TextMode="MultiLine"  cssClass="form-control" Height="100px" ></asp:TextBox>
                <br />
                <br />
                <div class="btn-group float-right">
                    <asp:Button ID="RejectButton" runat="server" Text="Отправить на доработку" OnClientClick="javascript: if ( confirm('Вы уверены что хотите отправить на доработку?') == true ) {showSimpleLoadingScreen();} else return false;" OnClick="RejectButton_Click" CssClass="btn btn-default"/>
                    <asp:Button ID="ApproveButton" runat="server" Text="Согласовать" OnClientClick="javascript: if ( confirm('Вы уверены что хотите согласовать процесс?') == true ) {showSimpleLoadingScreen();} else return false;" OnClick="ApproveButton_Click" CssClass="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
