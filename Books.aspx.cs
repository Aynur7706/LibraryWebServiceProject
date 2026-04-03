using System;

namespace LibraryWebServiceProject
{
    public partial class Books : System.Web.UI.Page
    {
        LibraryService service = new LibraryService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadBooks();
            }
        }

        private void LoadBooks()
        {
            gvBooks.DataSource = service.GetAllBooks();
            gvBooks.DataBind();
        }

        protected void btnAddBook_Click(object sender, EventArgs e)
        {
            try
            {
                string title = txtTitle.Text.Trim();
                string author = txtAuthor.Text.Trim();
                string category = txtCategory.Text.Trim();
                int publishYear = Convert.ToInt32(txtPublishYear.Text);
                int quantity = Convert.ToInt32(txtQuantity.Text);

                string result = service.AddBook(title, author, category, publishYear, quantity);
                lblMessage.Text = result;

                LoadBooks();

                txtTitle.Text = "";
                txtAuthor.Text = "";
                txtCategory.Text = "";
                txtPublishYear.Text = "";
                txtQuantity.Text = "";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Xəta: " + ex.Message;
            }
        }
    }
}