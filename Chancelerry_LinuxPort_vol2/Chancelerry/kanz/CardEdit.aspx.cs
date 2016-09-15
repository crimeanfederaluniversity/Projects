using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Chancelerry.kanz
{
    public partial class CardEdit : System.Web.UI.Page
    {
        private List<TextBox> createdFields;
        private List<FileUpload> createdFileUploads;
        private int _registerId;// = 1;
        private int _cardId;// = 0;
        private int _version;// = int.MaxValue;
        private bool _canEdit;
        private void UpdateSessionValues()
        {
            var userIdSes = Session["userID"];
            var registerIdSes = Session["registerID"];
            var versionSes = Session["version"];
            var cardIdSes = Session["cardID"];
            var _canEditSes = Session["canEdit"];
            _canEdit = true;
            if (_canEditSes != null)
                _canEdit = (bool)_canEditSes;

            if (userIdSes != null && registerIdSes != null && versionSes != null && cardIdSes != null)
            {     
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
            var userID = Session["userID"];

            if (userID == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            UpdateSessionValues();
            CardCreateView cardCreateView = new CardCreateView();
            Session["searchList"] = new List<TableActions.SearchValues>(); // сессия поиска 
            cardMainDiv.Controls.Add(cardCreateView.CreateViewByRegisterAndCard(_registerId, _cardId, _version, !_canEdit));
            if (!_canEdit)
            {
                LinkButton1.Visible = true;
                CreateButton.Visible = false;
            }
            createdFields = cardCreateView.allFieldsInCard;
            createdFileUploads = cardCreateView.allFileUploadsInCard;
        }

        protected void CreateButton_Click(object sender, EventArgs e)
        {
            UpdateSessionValues();
            
            CardCreateEdit cardCreateEdit = new CardCreateEdit();
           
            cardCreateEdit.SaveCard(_registerId, _cardId, createdFields, createdFileUploads,true);
            string lastPage = (string) Session["currentPage"];
            lastPage = "RegisterView.aspx"+lastPage;
            Response.Redirect(lastPage);
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Print.aspx");
        }
    }
}