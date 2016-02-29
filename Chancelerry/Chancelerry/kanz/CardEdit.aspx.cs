using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chancelerry.kanz
{
    public partial class CardEdit : System.Web.UI.Page
    {
        private List<TextBox> createdFields;
        private int _userId;//= 1;
        private int _registerId;// = 1;
        private int _cardId;// = 0;
        private int _version;// = int.MaxValue;
        private bool _canEdit;
        private void UpdateSessionValues()
        {
            var userIdSes =     Session["userID"];
            var registerIdSes = Session["registerID"];
            var versionSes =    Session["version"];
            var cardIdSes =     Session["cardID"];
            var _canEditSes =   Session["canEdit"];
            _canEdit = true;
            if (_canEditSes != null)
                _canEdit = (bool)_canEditSes;

            if (userIdSes != null && registerIdSes != null && versionSes != null && cardIdSes != null)
            {
                _userId = (int)userIdSes;
                _registerId = (int)registerIdSes; 
                _version = (int)versionSes;
                _cardId = (int)cardIdSes;
            }
            else
            {
                Response.Redirect("Dashboard.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateSessionValues();
            CardCreateView cardCreateView = new CardCreateView();

            Session["searchList"] = new List<TableActions.SearchValues>(); // сессия поиска 
            var canEditSession =   Session["userID"];
            cardMainDiv.Controls.Add(cardCreateView.CreateViewByRegisterAndCard(_registerId, _cardId, _version, !_canEdit));
            if (!_canEdit)
            {
                CreateButton.Visible = false;
            }
            createdFields = cardCreateView.allFieldsInCard;
        }

        protected void CreateButton_Click(object sender, EventArgs e)
        {
            UpdateSessionValues();
            CardCreateEdit cardCreateEdit = new CardCreateEdit();
            cardCreateEdit.SaveCard(_registerId, _cardId, createdFields);
            Response.Redirect("RegisterView.aspx");
        }
    }
}