using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace library
{
    public partial class AddEditForm : Form
    {

        IFirebaseConfig fcon = new FirebaseConfig
        {
            AuthSecret = "JobFCbH6SpLxDQFHb5yEiZWnc9eiKmmnJbcXtie0",
            BasePath = "https://mylibrary-01-default-rtdb.firebaseio.com/"
        };
        public IFirebaseClient client;
        public FirebaseResponse response;

        public AddEditForm()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        public void printBookData(Book book)
        {
            id_textbox.Text = book.id.ToString();
            if(id_textbox.Text == "0")
            {
                id_textbox.Text = "";
            }
            author_textbox.Text = book.author;
            location_textbox.Text = book.location;
            note_textbox.Text = book.note;
            page_textbox.Text = book.page.ToString();
            publisher_textbox.Text = book.publisher;
            checkBox1.Checked = book.readed;
            subject_textbox.Text = book.subject;
            title_textbox.Text = book.title;
            year_textbox.Text = book.year.ToString();
            altAuthor_textbox.Text = book.altAuthor;
            edition_textbox.Text = book.edition.ToString();   
        }
        public void sendBookData()
        {
            try
            {
                Book book = new Book();
                if(id_textbox.Text == "")
                {
                    var lastId = client.Get("config/lastID").Body;
                    book.id = int.Parse(lastId) + 1;
                    client.Set("config/lastID", book.id);
                }
                else
                {
                    book.id = int.Parse(id_textbox.Text);
                }
                
                book.author = author_textbox.Text;
                book.location = location_textbox.Text;
                book.note = note_textbox.Text;
                book.page = int.Parse(page_textbox.Text);
                book.publisher = publisher_textbox.Text;
                book.readed = checkBox1.Checked;
                book.subject = subject_textbox.Text;
                book.title = title_textbox.Text;
                book.year = int.Parse(year_textbox.Text);
                book.altAuthor = altAuthor_textbox.Text;
                book.edition = int.Parse(edition_textbox.Text);

                client.Set("books/" + book.id.ToString(), book);
                MessageBox.Show("OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            sendBookData();
        }

        private void AddEditForm_Load(object sender, EventArgs e)
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

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Book book = new Book();
            printBookData(book);
            id_textbox.Text = "";
        }
    }
}
