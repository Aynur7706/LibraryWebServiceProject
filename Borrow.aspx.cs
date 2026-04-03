using System;
using System.Web.UI.WebControls;

namespace LibraryWebServiceProject
{
    public partial class Borrow : System.Web.UI.Page
    {
        LibraryService service = new LibraryService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadBooks();
                LoadReaders();
                LoadBorrowedBooks();
            }
        }

        private void LoadBooks()
        {
            ddlBooks.DataSource = service.GetAllBooks();
            ddlBooks.DataTextField = "Title";
            ddlBooks.DataValueField = "BookId";
            ddlBooks.DataBind();
        }

        private void LoadReaders()
        {
            ddlReaders.DataSource = service.GetAllReaders();
            ddlReaders.DataTextField = "FullName";
            ddlReaders.DataValueField = "ReaderId";
            ddlReaders.DataBind();
        }

        private void LoadBorrowedBooks()
        {
            gvBorrowedBooks.DataSource = service.GetBorrowedBooks();
            gvBorrowedBooks.DataBind();
        }

        protected void btnBorrow_Click(object sender, EventArgs e)
        {
            int bookId = Convert.ToInt32(ddlBooks.SelectedValue);
            int readerId = Convert.ToInt32(ddlReaders.SelectedValue);

            lblMessage.Text = service.BorrowBook(bookId, readerId);

            LoadBooks();
            LoadBorrowedBooks();
        }

        protected void gvBorrowedBooks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ReturnBook")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int borrowId = Convert.ToInt32(gvBorrowedBooks.Rows[rowIndex].Cells[0].Text);

                lblMessage.Text = service.ReturnBook(borrowId);

                LoadBooks();
                LoadBorrowedBooks();
            }
        }
    }
}