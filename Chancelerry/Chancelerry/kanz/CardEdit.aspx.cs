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
        private int _userId = 1;
        private int _registerId = 1;
        private int _cardId = 0;
        private int _version = 100;

        private void UpdateSessionValues()
        {
            var userIdSes =     Session["userId"];
            var registerIdSes = Session["registerId"];
            var versionSes =    Session["version"];
            var cardIdSes =     Session["cardId"];

            if (userIdSes != null)
            {
                _userId = (int)userIdSes;
            }
            if (registerIdSes != null)
            {
                _registerId = (int)registerIdSes;
            }
            if (versionSes != null)
            {
                _cardId = (int)versionSes;
            }
            if (cardIdSes != null)
            {
                _version = (int)cardIdSes;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateSessionValues();
            CardCreateView cardCreateView = new CardCreateView();
            cardMainDiv.Controls.Add(cardCreateView.CreateViewByRegisterAndCard(_registerId, _cardId, _version, false));
            createdFields = cardCreateView.allFieldsInCard;
        }

        protected void CreateButton_Click(object sender, EventArgs e)
        {
            UpdateSessionValues();
            CardCreateEdit cardCreateEdit = new CardCreateEdit();
            cardCreateEdit.SaveCard(_registerId, _cardId, createdFields);
        }
    }
}