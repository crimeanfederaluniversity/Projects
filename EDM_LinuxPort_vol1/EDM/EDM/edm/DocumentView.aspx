<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DocumentView.aspx.cs" Inherits="EDM.edm.DocumentView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">


        .fixedHistoryPanel {
           background-color:rgba(0, 0, 0, 0.85);
             z-index: 2100;
          position: fixed;
          top: 0;
          left: 0;
          width: 100%;
          height: 100%;
          visibility: hidden;
          
           
        }

        .fixedHistoryPanel2 {
            top: 50%;
            left: 50%;
            margin: -300px 0px 0px -600px;
            border: 1px solid black;
            z-index: 21;
            position: fixed;
            background-color: white;
            
            height: 600px;
            width: 1200px;
        }
        .leftButton {
             left: 0px;
             width: 49%;
            
         }
        .rightButton {
            left: 50%;
            width: 49%;
            
        }

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
        

        function showHistory() {
            document.getElementById('ctl00_MainContent_historyDiv').style.visibility = 'visible';
        }
        function CloseHistoryDiv() {
            document.getElementById('ctl00_MainContent_historyDiv').style.visibility = 'hidden';
        }
        
        function buttonClickValidate()
        {
            var selectedvalue = $('#<%= RadioButtonList1.ClientID %> input:checked').val();
            if (selectedvalue == 2)
            {
                if (document.getElementById('ctl00_MainContent_CommentTextBox').value.length < 1)
                {
                    alert('Введите Ваши замечания');
                    return false;
                }
            }
            return true;
        }

        function setValues() {
            setComment();
            setDocument();
            document.getElementById('ctl00_MainContent_chooseInnerProcPanel').style.visibility = 'hidden';
        }

        function setComment() {
            var mainDiv = document.getElementById('ctl00_MainContent_chooseInnerProcPanelScrollComment');
                if (mainDiv.children.length > 0) {
                    var firstChild = mainDiv.children[0];
                    if (firstChild.children.length > 0) {
                        var secondChild = firstChild.children[0];
                        var commentValue = "";
                        for (var i = 0; i < secondChild.children.length; i++) {
                            var thirdChild = secondChild.children[i];
                            var cell = thirdChild.children[0];

                            var radio = cell.children[0];
                            var text = cell.children[1];
                            if (radio.checked) {
                                commentValue += text.textContent + '\n';
                            }
                        }
                        if (commentValue.length > 2)
                            document.getElementById('ctl00_MainContent_CommentTextBox').value = commentValue;
                    }
                }      
        }

        function setDocument() {
            var mainDiv = document.getElementById('ctl00_MainContent_chooseInnerProcPanelScrollDocs');
            if (mainDiv.children.length > 0) {
                var firstChild = mainDiv.children[0];
                if (firstChild.children.length > 0) {
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
                    if (documentId != '') {
                        document.getElementById('ctl00_MainContent_AddStepFileFileUpload').style.visibility = 'hidden';
                        document.getElementById('ctl00_MainContent_ExistingDocNameLabel').style.visibility = 'visible';
                        document.getElementById('ctl00_MainContent_ExistingDocNameLabel').innerHTML = documentValue;
                        document.getElementById('ctl00_MainContent_ExistingDocIdTextBox').value = documentId;
                    }
                }
            }
        }

    </script>
    <script src="https://code.jquery.com/jquery-1.11.3.js"></script>
    <script>
        $(document).ready(function() {
            $("#ctl00_MainContent_RadioButtonList1").click(function () {

                $("#ctl00_MainContent_RadioButtonList1 input").each(function (i, radio_btn) {
                    var rb_element = $(radio_btn);

                    if ( rb_element.prop('checked') ) {
                        if ( rb_element.val() == '1') {
                            $("#ctl00_MainContent_ApproveButton").prop('value', 'Согласовать');
                            $("#ctl00_MainContent_Label2").text("Ваш комментарий");
                            $("#ctl00_MainContent_CommentTextBox").prop('placeholder', 'Введите комментарий к процессу');
                        } else {

                            $("#ctl00_MainContent_ApproveButton").prop('value', 'Согласовать с замечанием');
                            $("#ctl00_MainContent_Label2").text("Ваше замечание");
                            $("#ctl00_MainContent_CommentTextBox").prop('placeholder', 'Введите замечание к процессу');
                        }
                    }

                                
                });
            });
        });
    </script>
    <br />
    <br />
  
    <h3 style="text-align: center;">  <asp:Label ID="InitiatorLabel" runat="server" Text="" ></asp:Label> </h3>

    <br />
    <div class="row centered-content">        
        <div class="edm-document-view-content input-group">
            <!--<asp:Label ID="Label1" runat="server" Text="Документы" CssClass="header"></asp:Label>
            <br /> -->
            
            <asp:GridView ID="docGridView" runat="server" AutoGenerateColumns="False" OnRowCommand="docGridView_RowCommand" Width="100%" CssClass="table table-striped edm-table">
            </asp:GridView>
            <h4 style="text-align: center;">   Комментарий инициатора согласования </h4>
            
            <asp:TextBox ID="LabelComment" TextMode="MultiLine" runat="server" ReadOnly="True" Height="100px" Width="100%" Text="Label" CssClass="edm-document-view-comment-block"></asp:TextBox>
            <br />
            <div id="useInnerProc" runat="server" Visible="False">
                <br />
                <asp:Button ID="OpenFixedPanelButton" runat="server" Text="Прикрепить комментарий и документы из внутренненго согласования"  Width="100%" />
                </div>
             <asp:Button ID="ShowHistory" runat="server" Text="Показать историю согласования" Width="100%"  OnClientClick="showHistory(); return false;" />
            <div runat="server" id="historyDiv" class="fixedHistoryPanel" onclick="CloseHistoryDiv();">
                <asp:Button ID="CloseHistoryDiv" runat="server" Text="Закрыть" Height="30px" Width="100%"  OnClientClick="CloseHistoryDiv();return false;"/>


            </div>
            <br />
            <div class="input-group-lg">                              
                <br />
                <asp:TextBox ID="LabelPrevComment"  runat="server" ReadOnly="True" TextMode="MultiLine" Visible="false"  cssClass="form-control" Height="100px" Text="Label" ></asp:TextBox>
                <br />
            <!--    <asp:Button ID="ButtonPrevComment" runat="server" Text="Показать Ваш комментарий из предыдущей версии процесса" OnClientClick="javascript:showSimpleLoadingScreen();" CausesValidation="False" Visible="False" Width="100%" OnClick="ButtonPrevComment_Click"/>
                <br />
                -->
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="1">Комментарий</asp:ListItem>
                    <asp:ListItem Value="2">Замечание</asp:ListItem>
                </asp:RadioButtonList>
         
                
                <asp:TextBox ID="CommentTextBox" runat="server" TextMode="MultiLine"  cssClass="form-control"  placeholder="Введите комментарий к процессу" Height="100px" ></asp:TextBox>
               
                
                <table>
                    <tr>
                        <td>
                            Прикрепление документа(не обязательно)
                            </td>
                         <td>
                            <asp:FileUpload ID="AddStepFileFileUpload" runat="server" Width="532px" />
                            </td>
                        </tr>
                    </table>    
                                     
                <asp:Label ID="ExistingDocNameLabel" Visible="true" runat="server" ></asp:Label>
                <asp:TextBox ID="ExistingDocIdTextBox" CssClass="invisible"  runat="server"></asp:TextBox>
                <br />
                <div> 
                    <asp:Button ID="RejectButton" runat="server"  Text="Отправить на доработку" OnClientClick="javascript: if (buttonClickValidate()==false) return false; if ( confirm('Вы уверены что хотите отправить на доработку?') == true ) {showLoadingScreenWithText('Возвращаем на доработку. Дождитесь завершения процесса!');} else return false;" OnClick="RejectButton_Click"  CssClass="btn btn-default leftButton"/>
                    <asp:Button ID="ApproveButton" runat="server" Text="Согласовать" OnClientClick="javascript:if (buttonClickValidate()==false) return false; if ( confirm('Вы уверены что хотите согласовать процесс?') == true ) {showLoadingScreenWithText('Утверждаем процесс. Дождитесь завершения!');} else return false;" OnClick="ApproveButton_Click" CssClass="btn btn-success rightButton" />
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
