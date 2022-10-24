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
    public partial class DetailsForm : Form
    {
        public Book globalBook = new Book();
        public DetailsForm()
        {
            InitializeComponent();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        public void showDetails(Book book)
        {
            id_label.Text = book.id.ToString();
            title_label.Text = book.title;
            author_label.Text = book.author;
            publisher_label.Text = book.publisher;

            edition_label.Text = book.edition.ToString();
            altAuthor_label.Text = book.altAuthor.ToString();
            page_label.Text = book.page.ToString();
            location_label.Text = book.location;
            year_label.Text = book.year.ToString();
            checkBox1.Checked = book.readed;
            note_label.Text = book.note;
            subject_label.Text = book.subject;

            globalBook = book;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddEditForm editForm = new AddEditForm();
            editForm.printBookData(globalBook);
            editForm.Show();
            this.Close();
        }
    }
}
