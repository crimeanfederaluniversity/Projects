﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DocumentView.aspx.cs" Inherits="EDM.edm.DocumentView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css">
    .top_panel {
    position:fixed;
    left:0;
    top:3.5em;
    width:100%;
    height:30px;
    background-color:#70463A !important;
    z-index:10;
    color:#05ff01;  
    padding-top:5px;
    font-weight:bold;
}
   .button_right 
   {
       float:right
   }     
</style> 
<asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
    <div>          
        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="goBackButton" CausesValidation="false"   runat="server" Enabled="true" CssClass="btn btn-default" Text="Назад" Width="150" Height="30" OnClick="goBackButton_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="goForwardButton" CausesValidation="false" Enabled="false" CssClass="btn btn-default" runat="server" Text="Вперед" Width="150" Height="30" OnClientClick="history.forward ()"/>
    </div>
</asp:Panel>
<br />
<br />
 
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
                <asp:TextBox ID="LabelPrevComment"  runat="server" ReadOnly="True" TextMode="MultiLine" Visible="false" cssClass="form-control" Height="100px" Text="Label" ></asp:TextBox>

                <br />
                <asp:Button ID="ButtonPrevComment" runat="server" Text="Показать Ваш комментарий из предыдущей версии процесса" OnClientClick="javascript:showSimpleLoadingScreen()" Visible="False" Width="100%" OnClick="ButtonPrevComment_Click"/>

                <asp:TextBox ID="CommentTextBox" runat="server" TextMode="MultiLine"  cssClass="form-control"  Height="100px" ></asp:TextBox>
                <br />
                <br />
                <div class="btn-group float-right">
                    <asp:Button ID="RejectButton" runat="server" Text="Отправить на доработку" OnClientClick="javascript: if ( confirm('Вы уверены что хотите отправить на доработку?') == true ) {showLoadingScreenWithText('Возвращаем на доработку. Дождитесь завершения процесса!');} else return false;" OnClick="RejectButton_Click" CssClass="btn btn-default"/>
                    <asp:Button ID="ApproveButton" runat="server" Text="Согласовать" OnClientClick="javascript: if ( confirm('Вы уверены что хотите согласовать процесс?') == true ) {showLoadingScreenWithText('Утверждаем процесс. Дождитесь завершения!');} else return false;" OnClick="ApproveButton_Click" CssClass="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
