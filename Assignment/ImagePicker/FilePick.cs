using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ImagePicker
{
    public partial class FilePick : Form
    {
        private DirectoryInfo d;
        private int file_counter = 0; //how many objects are in current directory
        private int gsize; //used for returning puzzle size
        private string img_path; //used for returning path to image
        private bool display_size = false; //toggle feature to show sizes of directories
        private int folders = 0; //how many directories are in a directory
        private bool text_en = false; //toggle feature to enable url browsing
        private bool date_col_sel = false; //is the date column sorted in ascending order
        private bool size_col_sel = false; //is the size column sorted in ascending order
        private bool name_col_sel = true; //is the name column sorted in ascending order, defaults to true

        public FilePick()
        {
            InitializeComponent();
            d = new DirectoryInfo("C:\\"); //root directory
            pictureBox1.Image = res(new Bitmap("..\\..\\Resources\\default-image.jpg"), new Size(pictureBox1.Width, pictureBox1.Height)); //a nice default picture to display
            update();
        }


        //Sorts date in ascending order using bubble sort
        //Sorts all in descending order
        private void asc(bool p)
        {
            if (p) //sort ascending
            {
                ListViewItem[] lvls = new ListViewItem[folders + 1]; //grabs all folders
                ListViewItem[] files = new ListViewItem[file_counter - (folders + 1)]; //grabs all files
                int f_count = folders, t_count = file_counter; //temporary int for number of folders and files, we must clear the directory and remember this info
                listView1.SelectedItems.Clear(); //fixed glitch; when an item is selected it would not sort it

                for (int i = 0; i < folders + 1; i++) //grab all the folders
                    lvls[i] = listView1.Items[i];

                for (int i = 0; i < file_counter - (folders + 1); i++) //grab all the files
                    files[i] = listView1.Items[(folders + 1) + i];

                //sort the folders
                for (int i = 0; i < folders + 1; i++)
                {
                    for (int j = 1; j < folders; j++)
                    {
                        //converts contents of column to datetime objects
                        DateTime d1 = Convert.ToDateTime(lvls[j].SubItems[2].Text);
                        DateTime d2 = Convert.ToDateTime(lvls[j + 1].SubItems[2].Text);
                        if (d1 > d2)
                        {
                            ListViewItem temp = lvls[j];
                            lvls[j] = lvls[j + 1];
                            lvls[j + 1] = temp;
                        }
                    }
                }

                clear_directory(); //clears the directory
                file_counter = t_count; //restores file_counter
                folders = f_count; //restores folders

                //adds folders back in sorted order
                for (int i = 0; i < folders + 1; i++)
                    listView1.Items.Add(lvls[i]);

                //bubble sort of files
                for (int i = 0; i < file_counter - (folders) + 1; i++)
                {
                    for (int j = 0; j < file_counter - folders - 2; j++)
                    {
                        DateTime d1 = Convert.ToDateTime(files[j].SubItems[2].Text);
                        DateTime d2 = Convert.ToDateTime(files[j + 1].SubItems[2].Text);
                        if (d1 > d2)
                        {
                            ListViewItem temp = files[j];
                            files[j] = files[j + 1];
                            files[j + 1] = temp;
                        }
                    }
                }

                //adds all the files back
                foreach (ListViewItem ts in files)
                    listView1.Items.Add(ts);
            }

            else //sort descending
            {
                ListViewItem[] lvls = new ListViewItem[folders + 1]; //grabs all the folders
                ListViewItem[] files = new ListViewItem[file_counter - (folders + 1)]; //grabs all the files
                ListViewItem temp = listView1.Items[0]; //keeps subdirectory item as a temp
                int f_count = folders, t_count = file_counter; //saves contents of variables
                listView1.SelectedItems.Clear();

                //same process as before
                for (int i = 1; i < folders + 1; i++)
                    lvls[i] = listView1.Items[i];

                for (int i = 0; i < file_counter - (folders + 1); i++)
                    files[i] = listView1.Items[(folders + 1) + i];

                clear_directory();
                file_counter = t_count;
                folders = f_count;
                listView1.Items.Add(temp); //adds the subdirectory object back first because it shouldnt be affected

                for (int i = folders; i > 0; i--)
                    listView1.Items.Add(lvls[i]);

                for (int i = file_counter - (folders + 1) - 1; i >= 0; i--)
                    listView1.Items.Add(files[i]);
            }
        }

        private void i_asc() //displays size column in ascending order
        { 
            //same process as in asc()
            ListViewItem[] lvls = new ListViewItem[folders + 1];
            ListViewItem[] files = new ListViewItem[file_counter - (folders + 1)];
            int f_count = folders, t_count = file_counter;
            listView1.SelectedItems.Clear();
                
            for (int i = 0; i < folders + 1; i++)
                lvls[i] = listView1.Items[i];

            for (int i = 0; i < file_counter - (folders + 1); i++)
                files[i] = listView1.Items[(folders + 1) + i];

            //this time we are doing a bubble sort using integers and not datetimes
            for (int i = 0; i < folders + 1; i++)
            {
                for (int j = 1; j < folders; j++)
                {
                    //convert contents of size column to strings
                    string s1 = lvls[j].SubItems[1].Text;
                    string s2 = lvls[j + 1].SubItems[1].Text;
                    int v1 = 0, v2 = 0;

                    //determining sizes, since we have a suffix in the column sizes
                    if (s1.Contains("GB"))
                        v1 = Convert.ToInt32(s1.Substring(0, s1.Length - 2)) * 1000000;
                    else if (s1.Contains("MB"))
                        v1 = Convert.ToInt32(s1.Substring(0, s1.Length - 2)) * 1000;
                    else if(s1.Contains("KB"))
                        v1 = Convert.ToInt32(s1.Substring(0, s1.Length - 2));

                    if (s2.Contains("GB"))
                        v2 = Convert.ToInt32(s2.Substring(0, s2.Length - 2)) * 1000000;
                    else if (s1.Contains("MB"))
                        v2 = Convert.ToInt32(s2.Substring(0, s2.Length - 2)) * 1000;
                    else if (s2.Contains("KB"))
                        v2 = Convert.ToInt32(s2.Substring(0, s2.Length - 2));

                    //bubble sort
                    if (v1 > v2)
                    {
                        ListViewItem temp = lvls[j];
                        lvls[j] = lvls[j + 1];
                        lvls[j + 1] = temp;
                    }
                }
            }

                //restoring defaults
                clear_directory();
                file_counter = t_count;
                folders = f_count;

                //adding back contents
                for (int i = 0; i < folders + 1; i++)
                    listView1.Items.Add(lvls[i]);

                foreach (ListViewItem ts in files)
                    listView1.Items.Add(ts);
            
        }

        //used to update the listview contents after changing directories
        private void update()
        {
            try
            {
                //Setting variables used, puts url of directory in textbox, adds a subdirectory ".."
                FileInfo[] f = d.GetFiles();
                DirectoryInfo[] dd = d.GetDirectories();
                textBox1.Text = d.ToString();
                listView1.Items.Add("..");
                listView1.Items[file_counter++].ImageIndex = 0; //folder image

                long n;
                string un;
                ListViewItem lv;
                //construct listview items for all the folders
                for (int i = 0; i < dd.Length; i++)
                {
                    lv = new ListViewItem();
                    lv.Text = dd[i].ToString(); //filename
                    lv.ImageIndex = 0; //folder image

                    if (display_size) //if we toggled on to display size
                    {
                        n = directorycall((textBox1.Text + "\\" + dd[i].ToString())); //recursively gets size of directory
                        un = "KB"; //default suffix of data size

                        if (n > 1000000) //determines if we have gigabytes of data
                        {
                            //sets size and suffix accordingly
                            n /= 1000000;
                            un = "GB";
                        }

                        //!if we have a terabyte of information in a single directory we will run into errors here
                        else if (n > 1000) //determines if we have megabytes of data
                        {
                            //sets size and suffix accordingly
                            n /= 1000;
                            un = "MB";
                        }

                        //displays size
                        lv.SubItems.Add("" + n + un);
                        SuspendLayout();
                    }

                    else //if we don't want to display size just show nothing
                        lv.SubItems.Add("");

                    lv.SubItems.Add("" + dd[i].LastWriteTime); //display the date

                    //add the item, since we are adding directories increment file_counter and folders
                    listView1.Items.Add(lv);
                    file_counter++;
                    folders++;
                }

                for (int i = 0; i < f.Length; i++)
                {
                    lv = new ListViewItem();
                    lv.Text = f[i].ToString(); //gets file name

                    //if we have an image with the below image tags then display an image icon next to it, if we have an unsuported image type
                    // nothing will be displayed.
                    if (f[i].Extension == ".jpg" || f[i].Extension == ".jpeg" || f[i].Extension == ".bmp" || f[i].Extension == ".gif"
                        || f[i].Extension == ".png")
                        lv.ImageIndex = 2;
                    else //we have a normal non-image file
                        lv.ImageIndex = 1;

                    lv.SubItems.Add(""); //don't add size of files
                    lv.SubItems.Add("" + f[i].LastAccessTime); //adds the date modified

                    //adds to listview and increments file_counter 
                    listView1.Items.Add(lv);
                    file_counter++;
                }
            }
            catch (DirectoryNotFoundException e) //if we try to navigate to an unknown directory
            {
                Console.WriteLine("No Files in Directory");
            }

            catch (UnauthorizedAccessException e) //if we get unauthorized access then we want to go back a directory
            {
                Console.WriteLine("UnauthorizedAccessException");
                button1_Click(null, null);
            }

            catch (IOException e) //if we are unable to open a file due to read permissions
            {
                Console.WriteLine("Unable to Open file...");
                button1_Click(null, null);
            }
        }

        //unpopulates listview
        private void clear_directory()
        {
            //goes through all files and removes, sets file_counter and folders equal to zero
            for (; file_counter > 0; --file_counter)
            {
                listView1.Items[file_counter - 1].Remove();
            }

            folders = 0;
        }

        //Uses a try catch to determin if an image can be opened
        private bool isImage(string path)
        {
            try
            {
                Bitmap b = new Bitmap(path); //if it can create a bitmap return true
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        //used to open files or directories
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //sets the column sorted variables to default
            size_col_sel = date_col_sel = false;
            name_col_sel = true;
            try
            {
                if (listView1.SelectedItems[0].Text == "..") //if we go back a directory click the up level button
                    button1_Click(null, null);

                else if (isImage(d.ToString() + "\\" + listView1.SelectedItems[0].Text)) //if we double clicked on an image 
                {
                    if (comboBox1.Text != "") //if then we're done here
                    {
                        //sets up path and gsize for reading by main form
                        gsize = Int32.Parse("" + comboBox1.Text[0]);
                        img_path = d.ToString() + "\\" + listView1.SelectedItems[0].Text;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else //inform user
                        MessageBox.Show("You need to select a puzzle size at the bottom...", "No sized picked", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

                else //if we clicked on a directory
                {
                    try
                    {
                        if (d.ToString() != "C:\\" && d.ToString() != "E:\\" && d.ToString() != "D:\\" && d.ToString() != "F:\\") //if we are not at root
                            d = new DirectoryInfo(d.ToString() + "\\" + listView1.SelectedItems[0].Text);

                        else //if we are at root
                            d = new DirectoryInfo(d.ToString() + listView1.SelectedItems[0].Text);
                    }
                    catch (UnauthorizedAccessException foo) //cannot view folder
                    {
                        d = new DirectoryInfo(d.ToString().TrimEnd('\\'));
                    }

                    clear_directory();
                    update();
                }
            }
            catch (ArgumentOutOfRangeException oo) //handler
            { }
        }

        //calls directory size function
        public long directorycall(string p)
        {
            nume_size = 0;
            directorysize(p);
            return nume_size;
        }

        private static long nume_size = 0; //directory size

        //attempts to recursively call a directory up by its subcontents to get file size
        private static void directorysize(string p)
        {
            try
            {
                nume_size += ( new DirectoryInfo(p).GetFiles("*.*", SearchOption.AllDirectories).Sum(file => file.Length) / 1000); //recursively returns size in killobytes
            }
            catch (Exception e) //we run into a read problem
            {
                return;
            }
        }

        //if we chose to go up a level
        private void button1_Click(object sender, EventArgs e)
        {
            if (d.ToString() != "C:\\" && d.ToString() != "D:\\" && d.ToString() != "F:\\" && d.ToString() != "E:\\") //make sure we are not at a root
            {
                string[] tokens = d.ToString().Split('\\'); //tokenize our path
                string res = ""; //append to create new path

                //add tokens back into new path
                for (int i = 0; i < tokens.Length - 1; i++)
                    res += tokens[i] + "\\";

                //if our new path isn't a root directory then we want to trim the end
                if (res != "C:\\" && res != "D:\\" && res != "E:\\" && res != "F:\\")
                    res = res.TrimEnd('\\');

                //clear directory, make our global directory equal to the one we just created and update
                clear_directory();
                d = new DirectoryInfo(res);
                update();
            }

            else
                Console.WriteLine("Cannot go back from Root Directory");
        }

        //used to resive bitmap for displaying in the picturebox
        private Bitmap res(Image i, Size s)
        {
            return new Bitmap(i, s);
        }

        //if we single click on any of the listview contents
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = res(new Bitmap("..\\..\\Resources\\default-image.jpg"), new Size(pictureBox1.Width, pictureBox1.Height)); //default image

            try
            {
                Bitmap b = new Bitmap(d.ToString() + "\\" + listView1.SelectedItems[0].Text); //trys to create the bitmap if it is unable to create then the default image is displayed
                //need floating point variables to handle floating point arithmatic
                float h = b.Height; 
                float w = b.Width;
                float ratio;

                //determines orientation of the picture
                //landscape orientation
                if (b.Width >= b.Height)
                {
                    ratio = h / w; //calculates the ratio
                    b = res(b, new Size(pictureBox1.Width, (int)(ratio * pictureBox1.Height))); //resizes according to the ratio
                }

                else //portrait orientation
                {
                    ratio = w / h; //same as before reversed
                    b = res(b, new Size((int)(ratio * (float)pictureBox1.Width), pictureBox1.Height));
                }

                pictureBox1.Image = b; //puts our resized bitmap into the picturebox

            }
            catch (Exception i) //if we are not on a valid picture
            {
                Console.WriteLine("File, Directory, or Unsupported picture clicked on");
            }
        }

        //used to allow keypress events to enter and back out of folders
        private void listView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if we click backspace then click the up level button
            if (e.KeyChar == (char)Keys.Back)
                button1_Click(null, null);

            //if we clicked enter we want to change the path and repopulate
            else if (e.KeyChar == (char)Keys.Enter)
            {
                listView1_MouseDoubleClick(null, null);
            }

        }

        //if we are using arrow keys to go through the files we want to try and load each into a picturebox
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView1_MouseClick(null, null);
        }

        //if we click the open button we want it to have the same functionality as the open click
        private void button2_Click(object sender, EventArgs e)
        {
            listView1_MouseDoubleClick(null, null);
        }

        //sets our path variable for retrieval
        public string path
        {
                get
                {
                    return img_path;
                }
        }

        //sets our size variable for retrieval
        public int getsize
        {
            get
            {
                return gsize;
            }
        }

        //if we select a column to sort it
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            //if we selected the first column and it is sorted
            //sort in descending order
            if (e.Column.ToString() == "0" && name_col_sel)
            {
                asc(false);

                //these can be true but most likely are not true
                name_col_sel = false; 
                date_col_sel = size_col_sel = false;
            }

            //if we selected the first column and it is sorted descending
            //sort in ascending
            else if (e.Column.ToString() == "0" && !name_col_sel)
            {
                //took advantage of the fact that update defaults to sorted ascending
                clear_directory();
                update();
                name_col_sel = true;
            }

            //if we clicked on the size column and it is not sorted
            else if (e.Column.ToString() == "1" && !size_col_sel)
            {
                //if we are enabling the size option
                if (checkBox1.Enabled)
                {
                    i_asc(); //sort in ascending 

                    //sets these accordingly
                    size_col_sel = true;
                    name_col_sel = date_col_sel = false;
                }
            }

            //if we clicked on the size column and it is sorted
            //sort descending
            else if (e.Column.ToString() == "1" && size_col_sel)
            {
                if (checkBox1.Enabled) //if we are displaying the sizes
                {
                    asc(false); //sort descending
                    date_col_sel = false; 
                }
            }

            //if we clicked on the date modifed column
            //sort in ascending order
            else if (e.Column.ToString() == "2" && !date_col_sel)
            {
                asc(true); //sorts in ascending

                //sets accordingly
                date_col_sel = true;
                name_col_sel = size_col_sel = false;
            }

            //if we already sorted the date modifed column and it is clicked again
            //then sort it in descending order
            else if (e.Column.ToString() == "2" && date_col_sel)
            {
                asc(false);
                date_col_sel = false;
            }

            else //should never be hit
            {
                Console.WriteLine("Invalid column clicked on.");
            }
        }

        //if we want to display the size
        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            display_size = !display_size; //toggle the boolean
            //clears and updates directory
            clear_directory();
            update();
        }

        //if certain keys get pressed from inside the textbox
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) //if the enter key is pressed
            {
                DirectoryInfo temp = d; //store the directory incase of error

                try
                {
                    if (textBox1.Text == "C:" || textBox1.Text == "E:" || textBox1.Text == "D:" || textBox1.Text == "F:")
                        textBox1.Text += "\\";

                    d = new DirectoryInfo(textBox1.Text); //takes the contents of the url bar and makes global directory equal to it
                    d.GetDirectories(); //gets the directory, if the directory isn't valid then this will crash it into the catch instruction

                    //clear and update display
                    clear_directory();
                    update();
                }

                catch (Exception foo)
                {
                    d = temp; //restores the directory to the previous

                    //clears and reloads old directory
                    clear_directory();
                    update();

                    textBox1.Text = d.ToString(); //restores url bar's text
                }
            }
        }

        //if we want to enable the url bar
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            text_en = !text_en; //toggle the url bar

            if (text_en)
                textBox1.Enabled = true;
            else
                textBox1.Enabled = false;
        }
    }
}
