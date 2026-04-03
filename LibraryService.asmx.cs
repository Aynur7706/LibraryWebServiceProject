using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;

namespace LibraryWebServiceProject
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class LibraryService : System.Web.Services.WebService
    {
        string connStr = ConfigurationManager.ConnectionStrings["LibraryDbConnection"].ConnectionString;

        [WebMethod]
        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Books";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    books.Add(new Book
                    {
                        BookId = Convert.ToInt32(dr["BookId"]),
                        Title = dr["Title"].ToString(),
                        Author = dr["Author"].ToString(),
                        Category = dr["Category"].ToString(),
                        PublishYear = Convert.ToInt32(dr["PublishYear"]),
                        Quantity = Convert.ToInt32(dr["Quantity"]),
                        AvailableCount = Convert.ToInt32(dr["AvailableCount"])
                    });
                }
            }

            return books;
        }

        [WebMethod]
        public string AddBook(string title, string author, string category, int publishYear, int quantity)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"INSERT INTO Books
                                 (Title, Author, Category, PublishYear, Quantity, AvailableCount)
                                 VALUES
                                 (@Title, @Author, @Category, @PublishYear, @Quantity, @AvailableCount)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@Author", author);
                cmd.Parameters.AddWithValue("@Category", category);
                cmd.Parameters.AddWithValue("@PublishYear", publishYear);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@AvailableCount", quantity);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return "Kitab əlavə olundu";
        }

        [WebMethod]
        public List<Reader> GetAllReaders()
        {
            List<Reader> readers = new List<Reader>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Readers";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    readers.Add(new Reader
                    {
                        ReaderId = Convert.ToInt32(dr["ReaderId"]),
                        FullName = dr["FullName"].ToString(),
                        Phone = dr["Phone"].ToString(),
                        Email = dr["Email"].ToString(),
                        Address = dr["Address"].ToString()
                    });
                }
            }

            return readers;
        }

        [WebMethod]
        public string AddReader(string fullName, string phone, string email, string address)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"INSERT INTO Readers (FullName, Phone, Email, Address)
                                 VALUES (@FullName, @Phone, @Email, @Address)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Address", address);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return "Oxucu əlavə olundu";
        }

        [WebMethod]
        public string BorrowBook(int bookId, int readerId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string checkQuery = "SELECT AvailableCount FROM Books WHERE BookId=@BookId";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@BookId", bookId);

                object result = checkCmd.ExecuteScalar();

                if (result == null)
                {
                    return "Kitab tapılmadı";
                }

                int availableCount = Convert.ToInt32(result);

                if (availableCount <= 0)
                {
                    return "Bu kitab hazırda mövcud deyil";
                }

                string insertQuery = @"INSERT INTO BorrowedBooks (BookId, ReaderId, BorrowDate, IsReturned)
                                       VALUES (@BookId, @ReaderId, GETDATE(), 0)";
                SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                insertCmd.Parameters.AddWithValue("@BookId", bookId);
                insertCmd.Parameters.AddWithValue("@ReaderId", readerId);
                insertCmd.ExecuteNonQuery();

                string updateQuery = "UPDATE Books SET AvailableCount = AvailableCount - 1 WHERE BookId=@BookId";
                SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                updateCmd.Parameters.AddWithValue("@BookId", bookId);
                updateCmd.ExecuteNonQuery();
            }

            return "Kitab uğurla verildi";
        }

        [WebMethod]
        public string ReturnBook(int borrowId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string getBookIdQuery = "SELECT BookId FROM BorrowedBooks WHERE BorrowId=@BorrowId AND IsReturned=0";
                SqlCommand getBookIdCmd = new SqlCommand(getBookIdQuery, conn);
                getBookIdCmd.Parameters.AddWithValue("@BorrowId", borrowId);

                object result = getBookIdCmd.ExecuteScalar();

                if (result == null)
                {
                    return "Qaytarılacaq aktiv qeyd tapılmadı";
                }

                int bookId = Convert.ToInt32(result);

                string returnQuery = @"UPDATE BorrowedBooks
                                       SET IsReturned = 1, ReturnDate = GETDATE()
                                       WHERE BorrowId=@BorrowId";
                SqlCommand returnCmd = new SqlCommand(returnQuery, conn);
                returnCmd.Parameters.AddWithValue("@BorrowId", borrowId);
                returnCmd.ExecuteNonQuery();

                string updateBookQuery = "UPDATE Books SET AvailableCount = AvailableCount + 1 WHERE BookId=@BookId";
                SqlCommand updateBookCmd = new SqlCommand(updateBookQuery, conn);
                updateBookCmd.Parameters.AddWithValue("@BookId", bookId);
                updateBookCmd.ExecuteNonQuery();
            }

            return "Kitab geri qaytarıldı";
        }
        [WebMethod]
        public List<Book> SearchBooks(string keyword)
        {
            List<Book> books = new List<Book>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Books WHERE Title LIKE @Keyword OR Author LIKE @Keyword";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    books.Add(new Book
                    {
                        BookId = Convert.ToInt32(dr["BookId"]),
                        Title = dr["Title"].ToString(),
                        Author = dr["Author"].ToString(),
                        Category = dr["Category"].ToString(),
                        PublishYear = Convert.ToInt32(dr["PublishYear"]),
                        Quantity = Convert.ToInt32(dr["Quantity"]),
                        AvailableCount = Convert.ToInt32(dr["AvailableCount"])
                    });
                }
            }

            return books;
        }
        [WebMethod]
        public List<BorrowView> GetBorrowedBooks()
        {
            List<BorrowView> borrowedBooks = new List<BorrowView>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT 
                        bb.BorrowId,
                        b.Title AS BookTitle,
                        r.FullName AS ReaderName,
                        bb.BorrowDate,
                        bb.ReturnDate,
                        bb.IsReturned
                    FROM BorrowedBooks bb
                    INNER JOIN Books b ON bb.BookId = b.BookId
                    INNER JOIN Readers r ON bb.ReaderId = r.ReaderId";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    borrowedBooks.Add(new BorrowView
                    {
                        BorrowId = Convert.ToInt32(dr["BorrowId"]),
                        BookTitle = dr["BookTitle"].ToString(),
                        ReaderName = dr["ReaderName"].ToString(),
                        BorrowDate = Convert.ToDateTime(dr["BorrowDate"]),
                        ReturnDate = dr["ReturnDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["ReturnDate"]),
                        IsReturned = Convert.ToBoolean(dr["IsReturned"])
                    });
                }
            }

            return borrowedBooks;
        }
    }
}