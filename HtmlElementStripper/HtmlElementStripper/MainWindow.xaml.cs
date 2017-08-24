#region Usings

using System.Linq;
using System.Windows;
using HtmlAgilityPack;

#endregion

namespace HtmlElementStripper
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private HtmlDocument LoadHtmlDocumentFromTextBox()
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(txtInput.Text);
            return htmlDocument;
        }

        private void btnStripImages_Click(object sender, RoutedEventArgs e)
        {
            var doc = LoadHtmlDocumentFromTextBox();

            var docNode = doc.DocumentNode;
            var images = docNode.Descendants("img").Select(i=>i.Attributes.FirstOrDefault(a=>a.Name=="src").Value).ToList();

            HtmlNode galleryNode;
            if (images.Any())
                galleryNode = GallerBuilder.BuildImageGalleryNode(images);
        }

        private void btnStripCscripts_Click(object sender, RoutedEventArgs e)
        {
            RemoveNodeByNameAndUpdateTextBox("script");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var txt = txtOutput.Text;
            if (string.IsNullOrEmpty(txt) && string.IsNullOrWhiteSpace(txt))
                return;

            Clipboard.SetText(txt);
        }

        private void RemoveNodeByNameAndUpdateTextBox(string nodeName)
        {
            var doc = LoadHtmlDocumentFromTextBox();

            var docNode = doc.DocumentNode;
            var xpaths = docNode.Descendants(nodeName).Select(node => node.XPath).ToList();

            foreach (var xpath in xpaths)
                docNode.SelectSingleNode(xpath).Remove();
            txtOutput.Text = docNode.OuterHtml;
        }
    }
}