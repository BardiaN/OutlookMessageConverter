using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OutlookMessageConverter.InfraStructure;


namespace OutlookMessageConverter
{
    public partial class FormMain : Form
    {
        #region Constructor and Events
        public FormMain()
        {
            InitializeComponent();
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            alwaysOnTopToolStripMenuItem.Checked = this.TopMost;
        }
        private void MessagesTreeView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void MessagesTreeView_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) != null) // Some files have been dragged to application
            {
                LoadFilesToTree(e.Data.GetData(DataFormats.FileDrop) as string[]);
            }
            else // Some Emails from outlook have been dragged to application
            {
                //wrap standard IDataObject in OutlookDataObject
                OutlookDataObject dataObject = new OutlookDataObject(e.Data);

                //get the names and data streams of the files dropped
                string[] filenames = (string[])dataObject.GetData("FileGroupDescriptor");
                MemoryStream[] filestreams = (MemoryStream[])dataObject.GetData("FileContents");

                for (int fileIndex = 0; fileIndex < filenames.Length; fileIndex++)
                {
                    //use the fileindex to get the name and data stream
                    string filename = filenames[fileIndex];
                    MemoryStream filestream = filestreams[fileIndex];

                    OutlookStorage.Message message = new OutlookStorage.Message(filestream);
                    this.LoadMsgToTree(message, this.MessagesTreeView.Nodes.Add("root"));
                    message.Dispose();
                }
            }
        }

        private void alwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurationHelper.AlwaysOnTop = !ConfigurationHelper.AlwaysOnTop;
            alwaysOnTopToolStripMenuItem.Checked = ConfigurationHelper.AlwaysOnTop;
            this.TopMost = alwaysOnTopToolStripMenuItem.Checked;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void importFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialogMessages.ShowDialog() == DialogResult.OK)
            {
                LoadFilesToTree(this.openFileDialogMessages.FileNames);
            }
        }

        #endregion

        #region Methods

        private void LoadFilesToTree(string[] fileNames)
        {
            foreach (string msgfile in fileNames)
            {
                Stream messageStream = File.Open(msgfile, FileMode.Open, FileAccess.Read);
                OutlookStorage.Message message = new OutlookStorage.Message(messageStream);
                messageStream.Close();

                this.LoadMsgToTree(message, this.MessagesTreeView.Nodes.Add("root"));
                message.Dispose();
            }
        }

        private void LoadMsgToTree(OutlookStorage.Message message, TreeNode rootNode)
        {
            rootNode.Text = message.Subject;
            rootNode.Nodes.Add("Subject: " + message.Subject);
            TreeNode bodyNode = rootNode.Nodes.Add("Body: " + GetShortMessage(message.BodyText)  + "(double click to view)");
            bodyNode.Tag = new string[] { message.BodyText, message.BodyRTF };

            if (message.Recipients.Count > 1)
            {
                TreeNode recipientNode = rootNode.Nodes.Add("Recipients: " + message.Recipients.Count);
                foreach (OutlookStorage.Recipient recipient in message.Recipients)
                {
                    recipientNode.Nodes.Add(recipient.Type + ": " + recipient.Email);
                }
            }
            else if(message.Recipients.Count > 0)
            {
                TreeNode recipientNode = rootNode.Nodes.Add("Recipient: " + message.Recipients.First().Email);
            }

            if (message.Attachments.Count > 0)
            {
                TreeNode attachmentNode = rootNode.Nodes.Add("Attachments: " + message.Attachments.Count);
                foreach (OutlookStorage.Attachment attachment in message.Attachments)
                {
                    attachmentNode.Nodes.Add(attachment.Filename + ": " + attachment.Data.Length + "b");
                }
            }

            if (message.Messages.Count > 0)
            {
                TreeNode subMessageNode = rootNode.Nodes.Add("Sub Messages: " + message.Messages.Count);
                foreach (OutlookStorage.Message subMessage in message.Messages)
                {
                    this.LoadMsgToTree(subMessage, subMessageNode.Nodes.Add("MSG"));
                }
            }
        }

        private string GetShortMessage(string messageBody)
        {
            if(!string.IsNullOrEmpty(messageBody) && messageBody.Length > 10)
            {
                return messageBody.Substring(0, 7) + "...";
            }
            return messageBody;
        }


        #endregion

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                if(MessagesTreeView.SelectedNode != null)
                {
                    MessagesTreeView.Nodes.Remove(MessagesTreeView.SelectedNode);
                }
            }
        }
    }
}
