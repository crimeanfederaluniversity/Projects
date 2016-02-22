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
        private int userId = 1;
        private int registerId = 1;
        private int cardId = 6;
        private int version = 100;
        protected void Page_Load(object sender, EventArgs e)
        {
            CardCreateView cardCreateView = new CardCreateView();
            cardMainDiv.Controls.Add(cardCreateView.CreateViewByRegisterAndCard(registerId, cardId, version, false));
            createdFields = cardCreateView.allFieldsInCard;
        }

        protected void CreateButton_Click(object sender, EventArgs e)
        {
            CardCreateEdit cardCreateEdit = new CardCreateEdit();
            cardCreateEdit.SaveCard(registerId,cardId, createdFields);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            cardId = Convert.ToInt32(TextBox1.Text);
            registerId = Convert.ToInt32(TextBox2.Text);
            version = Convert.ToInt32(TextBox3.Text);


            CardCreateView cardCreateView = new CardCreateView();
            cardMainDiv.Controls.Add(cardCreateView.CreateViewByRegisterAndCard(registerId, cardId, version, false));
            createdFields = cardCreateView.allFieldsInCard;
        }
    }
}