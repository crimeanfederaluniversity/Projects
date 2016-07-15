<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CardEdit.aspx.cs" Inherits="Chancelerry.kanz.CardEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/Site.css" rel="stylesheet" />
            <script src="calendar_ru.js" type="text/javascript">
            </script>
    <script src="toggleLoadingScreen.js" type="text/javascript"></script>
    <script src="moment.min.js"></script>
    <script>
        
        function getAllElementsWithAttribute(attribute) {
            var matchingElements = [];
            var allElements = document.getElementsByTagName('*');
            for (var i = 0, n = allElements.length; i < n; i++) {
                if (allElements[i].getAttribute(attribute) !== null) {
                    matchingElements.push(allElements[i]);
                }
            }
            return matchingElements;
        }

        function putValueAndClose(val, fieldId, panelId) {
            document.getElementById(fieldId).value = val;
            document.getElementById(panelId).style.visibility = 'hidden';
        }

        function addDays(textboxId, days) {
            var textBox = document.getElementById(textboxId);
            var textBoxValue = textBox.value;
            if (textBoxValue == "") return false;
            var choosenDate = moment(textBoxValue);
            var resDate = moment(choosenDate).add(days, 'day');
            textBox.value = moment(resDate).format('YYYY[-]MM[-]DD');
            return false;
        }

        function multiFieldRefresh() {
            var allMultiField = getAllElementsWithAttribute('multiField');
            
            for (var i = 0; i < allMultiField.length; i++) {
                var spanWeNeed = allMultiField[i].parentElement.firstChild;
                var result='';
                for (var j = 0; j < spanWeNeed.children.length; j++) {
                    if (spanWeNeed.children[j].type == 'text') {
                        if (spanWeNeed.children[j].value.length>0) {
                            result += spanWeNeed.children[j].value + ',';
                        }                       
                    }                   
                }
                if (result[result.length-1] == ',') {
                    result = result.slice(0, result.length - 1);
                }
                allMultiField[i].value = result;
            }        
        }

        function addInputControl(cntrlId) {
            var multiFieldDiv = document.getElementById(cntrlId);
            var newInput = document.createElement("input");
            newInput.type = "text";
          //  newInput.addEventListener("click", function () { event.cancelBubble = true; this.select(); lcs(this); }, false);
            newInput.onclick = function (evt) { evt.cancelBubble = true; this.select(); lcs(this); };
            newInput.onfocus = function (evt) { this.select(); lcs(this); };
            multiFieldDiv.appendChild(document.createElement("br"));
            multiFieldDiv.appendChild(newInput);
        }
    </script>
    
    <script>

        function search(controlM, value) {

            if (controlM.tagName == 'A') {
                var text = controlM.innerText.toLowerCase();
                var lowValue = value.toLowerCase();
                if (text.indexOf(lowValue) != -1)
                {
                    controlM.focus();
                    return 1;
                }
            }

            for (var i = 0; i < controlM.children.length; i++) {
                if (search(controlM.children[i], value) == 1)
                    return 1;
            }

            return 0;
        }

        function findInControl(controlId, textToFindBoxId) {        
            var myElem = document.getElementById(controlId);
            var textBox = document.getElementById(textToFindBoxId);
            search(myElem, textBox.value);
        }

    </script>
    
            <script type="text/javascript">
                function pageLoad() {
                    $("textarea").each(function () {
                        this.value = this.value.trim();
                    });
                }
            </script>

    <div id="cardMainDiv" runat="server">
    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Visible="False">Версия для печати</asp:LinkButton>
    </div>
    <br />
    <asp:Button ID="CreateButton" runat="server" Text="Сохранить" OnClick="CreateButton_Click" OnClientClick="multiFieldRefresh();" Width="100%" />
</asp:Content>
