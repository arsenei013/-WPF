using System.Windows;

namespace Технология_WPF
{
    public partial class ChildWindow : Window
    {
        public string FilePath { get; set; }

        public string DocumentText
        {
            get { return MemoBox.Text; }
            set { MemoBox.Text = value; }
        }

        public ChildWindow(string title, string content = "", string filePath = null)
        {
            InitializeComponent();
            this.Title = title;
            this.DocumentText = content;
            this.FilePath = filePath;
        }
    }
}

