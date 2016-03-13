<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DocumentView.aspx.cs" Inherits="EDM.edm.DocumentView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
 
    <div class="row centered-content">
        <div class="edm-document-view-content input-group">
            <asp:Label ID="Label1" runat="server" Text="Документы" CssClass="header"></asp:Label>
            <br />
   
    
            <asp:GridView ID="docGridView" runat="server" AutoGenerateColumns="False" OnRowCommand="docGridView_RowCommand" CssClass="table table-striped edm-table">
            </asp:GridView>
    
            <br />
    
            <asp:Label ID="LabelComment" runat="server" Text="Label"></asp:Label>

            <br />
            <br />
            
        <%--<asp:TextBox ID="CommentTextBox" runat="server" TextMode="MultiLine" Height="54px" Width="160px"></asp:TextBox>--%>
            <div class="input-group-lg">
                <asp:TextBox ID="CommentTextBox" runat="server" TextMode="MultiLine" cssClass="form-control" ></asp:TextBox>
                <br />
                <br />
                <div class="btn-group float-right">
                    <asp:Button ID="RejectButton" runat="server" Text="Отправить на доработку" OnClick="RejectButton_Click" CssClass="btn btn-default"/>
                    <asp:Button ID="ApproveButton" runat="server" Text="Согласовать" OnClick="ApproveButton_Click" CssClass="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
