using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using System.IO;

namespace library
{
    public partial class Form1 : Form
    {

        IFirebaseConfig fcon = new FirebaseConfig
        {
            AuthSecret = "TOKEN",
            BasePath = "URL"
        };

        public IFirebaseClient client;
        public FirebaseResponse response;

        public Form1()
        {
            InitializeComponent();
        }
        
        public List<Book> getBooks()
        {
            var result = client.Get("books");
            List<Book> bookList = result.ResultAs<List<Book>>();
            return bookList;
        }
        public void printBooks(List<Book> bookList) {
            dataGridView1.Rows.Clear();
            foreach (Book book in bookList)
            {
                try
                {
                    if (book == null || book.title == "title") continue;
                    addBook(book);
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                
            }
        }
        public void addColumn(string name, string header, int width)
        {
            DataGridViewTextBoxColumn newColumn = new DataGridViewTextBoxColumn();
            newColumn.Name = name;
            newColumn.HeaderText = header;
            newColumn.Width = width;

            dataGridView1.Columns.Add(newColumn);
        }
        public void addBook(Book book)
        {
            List<string> values = new List<string>();

            DataGridViewRow row = dataGridView1.Rows[dataGridView1.Rows.Add()];
            foreach(DataGridViewCell cell in row.Cells)
            {
                if (cell.ColumnIndex == 0) cell.Value = book.id;
                switch (dataGridView1.Columns[cell.ColumnIndex].Name)
                {
                    case "title":
                        cell.Value = book.title;
                        break;
                    case "author":
                        cell.Value = book.author;
                        break;
                    case "publisher":
                        cell.Value = book.publisher;
                        break;
                    case "subject":
                        cell.Value = book.subject;
                        break;
                    case "page":
                        cell.Value = book.page;
                        break;
                    case "year":
                        cell.Value = book.year;
                        break;
                    case "alt_author":
                        cell.Value = book.altAuthor;
                        break;
                    case "location":
                        cell.Value = book.location;
                        break;
                    case "readed":
                        cell.Value = book.readed;
                        break;
                }
            }
        }

        public Book takeDetails(int id)
        {
            var result = client.Get("books/" + id.ToString());
            Book book = new Book();
            book = result.ResultAs<Book>();
            return book;
        }
        public void deleteBook(int ID)
        {
            client.Delete("books/" + ID.ToString());
            printBooks(getBooks());
            MessageBox.Show("Deleted");
        }
        /* --------------Form Functions--------------- */
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(fcon);
            }
            catch
            {
                MessageBox.Show("There was problem in the internet.");

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            /*
            DataGridViewRow newRow = new DataGridViewRow();
            //newRow.SetValues("a");

            //DataGridViewCell cell = new DataGridViewCell();

            newRow.CreateCells(dataGridView1);
            
            newRow.SetValues("a");
            
            dataGridView1.Rows.Add(newRow);
            //newRow.SetValues("b","v","x","d","e");*/

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Book book = new Book();
                var result = client.Get("books/0");
                book = result.ResultAs<Book>();

                richTextBox1.Text = book.year.ToString();

                book.id = 1;
                client.Set("books/1", book);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            printBooks(getBooks());
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        

        private void details_button_Click(object sender, EventArgs e)
        {
            /*
            if(dataGridView1.SelectedCells.Count != 5)
            {
                MessageBox.Show("Please select all line");
                MessageBox.Show(dataGridView1.SelectedColumns.Count.ToString());
                return;
            }*/
            int ID = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            DetailsForm detailForm = new DetailsForm();
            detailForm.showDetails(takeDetails(ID));
            detailForm.Show();
        }

        private void edit_button_Click(object sender, EventArgs e)
        {
            /*
            if (dataGridView1.SelectedCells.Count != 5)
            {
                MessageBox.Show("Please select all line");
                MessageBox.Show(dataGridView1.SelectedColumns.Count.ToString());
                return;
            }*/
                int ID = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());

                AddEditForm editForm = new AddEditForm();
                editForm.printBookData(takeDetails(ID));
                editForm.Show();
            
        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            /*
            if (dataGridView1.SelectedCells.Count != 5)
            {
                MessageBox.Show("Please select all line");
                MessageBox.Show(dataGridView1.SelectedColumns.Count.ToString());
                return;
            }*/
         
            int ID = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            deleteBook(ID);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddEditForm editForm = new AddEditForm();
            Book book = new Book();
            
            editForm.printBookData(book);
            editForm.Show();
        }

        private void search_button_Click(object sender, EventArgs e)
        {
            List<Book> allBooks = getBooks();
            List<Book> searchedBooks = new List<Book>();
           
            foreach (var book in allBooks)
            {
                if (book != null)
                {
                    if(search_readed_combobox.Text == "Readed")
                    {
                        if (!book.readed) continue;
                    }
                    else if(search_readed_combobox.Text == "Unreaded")
                    {
                        if (book.readed) continue;
                    }
                    if (search_category_combobox.Text == "Title" )
                    {
                        if(search_equality_combobox.Text == "Include")
                        {
                            if (book.title.ToLower().Contains(search_content_textbox.Text.ToLower()))
                            {
                                searchedBooks.Add(book);
                            }
                        }                    
                    }
                    else if(search_category_combobox.Text == "Author")
                    {
                        if (search_equality_combobox.Text == "Include")
                        {
                            if (book.author.ToLower().Contains(search_content_textbox.Text.ToLower()))
                            {
                                searchedBooks.Add(book);
                            }
                        }
                            
                    }
                    else if(search_category_combobox.Text == "Publisher")
                    {
                        if (search_equality_combobox.Text == "Include")
                        {  
                            if (book.publisher.ToLower().Contains(search_content_textbox.Text.ToLower()))
                            {
                                searchedBooks.Add(book);
                            }
                        }
                    }
                    else if(search_category_combobox.Text == "Year")
                    {
                        if (search_equality_combobox.Text == "Equal")
                        {
                            if (book.year == int.Parse(search_content_textbox.Text))
                            {
                                searchedBooks.Add(book);
                            }
                        }
                        else if(search_equality_combobox.Text == "Greater than")
                        {
                            if(book.year > int.Parse(search_content_textbox.Text))
                            {
                                searchedBooks.Add(book);
                            }
                        }
                        else if (search_equality_combobox.Text == "Less than")
                        {
                            if (book.year < int.Parse(search_content_textbox.Text))
                            {
                                searchedBooks.Add(book);
                            }
                        }
                    }
                    else if (search_category_combobox.Text == "Page")
                    {
                        if (search_equality_combobox.Text == "Equal")
                        {
                            if (book.page == int.Parse(search_content_textbox.Text))
                            {
                                searchedBooks.Add(book);
                            }
                        }
                        else if (search_equality_combobox.Text == "Greater than")
                        {
                            if (book.page > int.Parse(search_content_textbox.Text))
                            {
                                searchedBooks.Add(book);
                            }
                        }
                        else if (search_equality_combobox.Text == "Less than")
                        {
                            if (book.page < int.Parse(search_content_textbox.Text))
                            {
                                searchedBooks.Add(book);
                            }
                        }
                    }
                    else if (search_category_combobox.Text == "Subjects")
                    {
                        if(search_equality_combobox.Text == "Include")
                        {
                            if (book.subject.Contains(search_content_textbox.Text))
                            {
                                searchedBooks.Add(book);
                            }
                        }
                    }
                }        
            }
            
            printBooks(searchedBooks);

        }

        private void settings_button_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                addColumn("title", "Title", 250);
                printBooks(getBooks());
            }
            else
            {
                dataGridView1.Columns.Remove("title");
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                addColumn("publisher", "Publisher", 250);
                printBooks(getBooks());
            }
            else
            {
                dataGridView1.Columns.Remove("publisher");
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                addColumn("author", "Author", 250);
                printBooks(getBooks());
            }
            else
            {
                dataGridView1.Columns.Remove("author");
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                addColumn("subject", "Subjects", 200);
                printBooks(getBooks());
            }
            else
            {
                dataGridView1.Columns.Remove("subject");
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                addColumn("page", "Page", 75);
                printBooks(getBooks());
            }
            else
            {
                dataGridView1.Columns.Remove("page");
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                addColumn("year", "Year", 75);
                printBooks(getBooks());
            }
            else
            {
                dataGridView1.Columns.Remove("year");
            }
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked)
            {
                addColumn("alt_author", "Alt Author", 250);
                printBooks(getBooks());
            }
            else
            {
                dataGridView1.Columns.Remove("alt_author");
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
            {
                addColumn("location", "Location", 150);
                printBooks(getBooks());
            }
            else
            {
                dataGridView1.Columns.Remove("location");
            }
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked)
            {
                addColumn("readed", "Readed", 100);
                printBooks(getBooks());
            }
            else
            {
                dataGridView1.Columns.Remove("readed");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
