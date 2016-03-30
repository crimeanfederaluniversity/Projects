<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DocumentView.aspx.cs" Inherits="EDM.edm.DocumentView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        
        .invisible {
            visibility: hidden;
        }
            
         .RBL label
         {
             display: inline;
         }
        .RBL td
        {
            text-align: left;
            width: 100%;
        }
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
    
    
    <script>


        function setValues() {
            setComment();
            setDocument();
            document.getElementById('MainContent_chooseInnerProcPanel').style.visibility = 'hidden';
        }

        function setComment() {

            var mainDiv = document.getElementById('MainContent_chooseInnerProcPanelScrollComment');
            var firstChild = mainDiv.children[0];
            var secondChild = firstChild.children[0];
            var commentValue = "";
            for (var i = 0; i < secondChild.children.length; i++)
            {
                var thirdChild = secondChild.children[i];
                var cell = thirdChild.children[0];

                var radio = cell.children[0];
                var text = cell.children[1];

                if (radio.checked)
                {
                    commentValue = text.textContent;
                }
            }
            if (commentValue.length > 2)
                document.getElementById('MainContent_CommentTextBox').value = commentValue;
        }

        function setDocument() {

            var mainDiv = document.getElementById('MainContent_chooseInnerProcPanelScrollDocs');
            var firstChild = mainDiv.children[0];
            var secondChild = firstChild.children[0];
            var documentValue = '';
            var documentId = '';
            for (var i = 0; i < secondChild.children.length; i++) {
                var thirdChild = secondChild.children[i];
                var cell = thirdChild.children[0];

                var radio = cell.children[0];
                var text = cell.children[1];

                if (radio.checked) {
                    documentValue = text.textContent;
                    documentId = radio.value;
                }
            }
            if (documentId!='')
            {
                document.getElementById('MainContent_AddStepFileFileUpload').style.visibility = 'hidden';
                document.getElementById('MainContent_ExistingDocNameLabel').style.visibility = 'visible';
                document.getElementById('MainContent_ExistingDocNameLabel').innerHTML = documentValue;
               // alert(documentId);
               // document.getElementById('MainContent_ExistingDocIdTextBox').text = documentId;
                document.getElementById('MainContent_ExistingDocIdTextBox').value = documentId;
                //
            }         
        }

    </script>
    
   
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
            <div id="useInnerProc" runat="server" Visible="False">
                <br />
                <asp:Button ID="OpenFixedPanelButton" runat="server" Text="Прикрепить комментарий и документы из внутренненго согласования" Width="790px" />
                </div>
            <br />
            
        <%--<asp:TextBox ID="CommentTextBox" runat="server" TextMode="MultiLine" Height="54px" Width="160px"></asp:TextBox>--%> 
            Ваш комментарий 
            <asp:RequiredFieldValidator runat="server" SetFocusOnError="True" ControlToValidate="CommentTextBox" ErrorMessage="Введите комментарий!" ForeColor="red"/>
            <div class="input-group-lg">
                <br />
                <asp:TextBox ID="LabelPrevComment"  runat="server" ReadOnly="True" TextMode="MultiLine" Visible="false"  cssClass="form-control" Height="100px" Text="Label" ></asp:TextBox>

                <br />
                <asp:Button ID="ButtonPrevComment" runat="server" Text="Показать Ваш комментарий из предыдущей версии процесса" OnClientClick="javascript:showSimpleLoadingScreen()" CausesValidation="False" Visible="False" Width="100%" OnClick="ButtonPrevComment_Click"/>

                <asp:TextBox ID="CommentTextBox" runat="server" TextMode="MultiLine"  cssClass="form-control"  Height="100px" ></asp:TextBox>

                <br />

                Прикрепление документа(не обязательно)
                <asp:FileUpload ID="AddStepFileFileUpload" runat="server" Width="532px" />

                <asp:Label ID="ExistingDocNameLabel" Visible="true" runat="server" ></asp:Label>
                <br />
                <asp:TextBox ID="ExistingDocIdTextBox" CssClass="invisible"  runat="server"></asp:TextBox>

                <br />

                <div class="btn-group float-right">
                    <br />
                    <asp:Button ID="RejectButton" runat="server" Text="Отправить на доработку" OnClientClick="javascript:Page_ClientValidate(); if (Page_IsValid==true) if ( confirm('Вы уверены что хотите отправить на доработку?') == true ) {showLoadingScreenWithText('Возвращаем на доработку. Дождитесь завершения процесса!');} else return false;" OnClick="RejectButton_Click" CssClass="btn btn-default"/>
                    <asp:Button ID="ApproveButton" runat="server" Text="Согласовать" OnClientClick="javascript:Page_ClientValidate(); if (Page_IsValid==true) if ( confirm('Вы уверены что хотите согласовать процесс?') == true ) {showLoadingScreenWithText('Утверждаем процесс. Дождитесь завершения!');} else return false;" OnClick="ApproveButton_Click" CssClass="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
