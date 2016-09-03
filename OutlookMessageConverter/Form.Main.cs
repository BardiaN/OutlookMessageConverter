using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private void MessagesTreeView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void MessagesTreeView_DragDrop(object sender, DragEventArgs e)
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

        #endregion

        #region Methods
        private void LoadMsgToTree(OutlookStorage.Message message, TreeNode rootNode)
        {
            rootNode.Text = message.Subject;
            rootNode.Nodes.Add("Subject: " + message.Subject);
            TreeNode bodyNode = rootNode.Nodes.Add("Body: " + GetShortMessage(message.BodyText)  + "(click to view)");
            bodyNode.Tag = new string[] { message.BodyText, message.BodyRTF };

            TreeNode recipientNode = rootNode.Nodes.Add("Recipients: " + message.Recipients.Count);
            foreach (OutlookStorage.Recipient recipient in message.Recipients)
            {
                recipientNode.Nodes.Add(recipient.Type + ": " + recipient.Email);
            }

            TreeNode attachmentNode = rootNode.Nodes.Add("Attachments: " + message.Attachments.Count);
            foreach (OutlookStorage.Attachment attachment in message.Attachments)
            {
                attachmentNode.Nodes.Add(attachment.Filename + ": " + attachment.Data.Length + "b");
            }

            TreeNode subMessageNode = rootNode.Nodes.Add("Sub Messages: " + message.Messages.Count);
            foreach (OutlookStorage.Message subMessage in message.Messages)
            {
                this.LoadMsgToTree(subMessage, subMessageNode.Nodes.Add("MSG"));
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
    }
}
