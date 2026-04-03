using System;

namespace LibraryWebServiceProject
{
    public partial class Readers : System.Web.UI.Page
    {
        LibraryService service = new LibraryService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadReaders();
            }
        }

        private void LoadReaders()
        {
            gvReaders.DataSource = service.GetAllReaders();
            gvReaders.DataBind();
        }

        protected void btnAddReader_Click(object sender, EventArgs e)
        {
            try
            {
                string result = service.AddReader(
                    txtFullName.Text,
                    txtPhone.Text,
                    txtEmail.Text,
                    txtAddress.Text
                );

                lblMessage.Text = result;
                LoadReaders();

                txtFullName.Text = "";
                txtPhone.Text = "";
                txtEmail.Text = "";
                txtAddress.Text = "";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Xəta: " + ex.Message;
            }
        }
    }
}   