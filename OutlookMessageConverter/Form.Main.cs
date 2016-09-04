using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OutlookMessageConverter.InfraStructure;
using OutlookMessageConverter.Properties;

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
            this.TopMost = ConfigurationHelper.AlwaysOnTop;
            alwaysOnTopToolStripMenuItem.Checked = this.TopMost;
            showDeleteConfirmToolStripMenuItem.Checked = ConfigurationHelper.ConfirmDelete;
            askEveryTimeToolStripMenuItem.Checked = ConfigurationHelper.TreeNodeOverride == TreeNodeOverrideEnum.AskEveryTime;
            appendMessagesToolStripMenuItem.Checked = ConfigurationHelper.TreeNodeOverride == TreeNodeOverrideEnum.Append;
            overwriteMessagesToolStripMenuItem.Checked = ConfigurationHelper.TreeNodeOverride == TreeNodeOverrideEnum.Overwrite;
            if (ConfigurationHelper.ShowHintOnLoad)
            {
                ShowHelp();
            }
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
                CheckTreeMessages();

                for (int fileIndex = 0; fileIndex < filenames.Length; fileIndex++)
                {
                    //use the fileindex to get the name and data stream
                    string filename = filenames[fileIndex];
                    using (MemoryStream filestream = filestreams[fileIndex])
                    {
                        using (MsgReader.Outlook.Storage.Message message = new MsgReader.Outlook.Storage.Message(filestream, FileAccess.Read))
                        {
                            this.LoadMsgToTree(message, this.MessagesTreeView.Nodes.Add("root"));
                        }
                    }
                }
            }
        }

        private void alwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurationHelper.AlwaysOnTop = !ConfigurationHelper.AlwaysOnTop;
            alwaysOnTopToolStripMenuItem.Checked = ConfigurationHelper.AlwaysOnTop;
            this.TopMost = alwaysOnTopToolStripMenuItem.Checked;
        }

        private void showDeleteConfirmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurationHelper.ConfirmDelete = !ConfigurationHelper.ConfirmDelete;
            showDeleteConfirmToolStripMenuItem.Checked = ConfigurationHelper.ConfirmDelete;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void importFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogMessages.ShowDialog() == DialogResult.OK)
            {
                LoadFilesToTree(this.openFileDialogMessages.FileNames);
            }
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedNode();
            }
        }

        private void UpButton_Click(object sender, EventArgs e)
        {
            if (MessagesTreeView.SelectedNode != null)
            {
                TreeNode parentNode = MessagesTreeView.SelectedNode;
                while (parentNode.Parent != null)
                {
                    parentNode = parentNode.Parent;
                }
                if (parentNode.PrevNode != null)
                {
                    int newIndexNode = parentNode.PrevNode.Index;
                    parentNode.Remove();
                    MessagesTreeView.Nodes.Insert(newIndexNode, parentNode);
                    MessagesTreeView.SelectedNode = parentNode;
                }
            }
            else
            {
                MessageBox.Show("Please select a message in the treeview first");
            }
        }

        private void DownButton_Click(object sender, EventArgs e)
        {
            if (MessagesTreeView.SelectedNode != null)
            {
                TreeNode parentNode = MessagesTreeView.SelectedNode;
                while (parentNode.Parent != null)
                {
                    parentNode = parentNode.Parent;
                }
                if (parentNode.NextNode != null)
                {
                    int newIndexNode = parentNode.NextNode.Index;
                    parentNode.Remove();
                    MessagesTreeView.Nodes.Insert(newIndexNode, parentNode);
                    MessagesTreeView.SelectedNode = parentNode;
                }
            }
            else
            {
                MessageBox.Show("Please select a message in the treeview first");
            }
        }

        private void buttonExportPDF_Click(object sender, EventArgs e)
        {
            if (MessagesTreeView.Nodes.Count > 0)
            {
                saveFileDialogExport.FileName = "ExportedMessages" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm");
                if (saveFileDialogExport.ShowDialog() == DialogResult.OK)
                {
                    var messages =
                        MessagesTreeView.Nodes
                            .OfType<TreeNode>()
                            .Select(
                                node => new EmailModel
                                {
                                    From = node.Nodes["fromNode"] != null ? (node.Nodes["fromNode"].Tag.ToString()) : (string.Empty),
                                    Subject = node.Text,
                                    Body = node.Tag.ToString(),
                                    ReceivedOn = node.Nodes["ReceivedOn"].Tag.ToString()
                                })
                            .ToList();
                    string errorMessage;
                    if (!PdfGenerator.GeneratePdf(messages, saveFileDialogExport.FileName, out errorMessage))
                    {
                        MessageBox.Show("Error in generating pdf:" + errorMessage);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please add some messages first");
            }
        }

        private void UpButton_Enter(object sender, EventArgs e)
        {
            MessagesTreeView.Focus();
        }

        private void DownButton_Enter(object sender, EventArgs e)
        {
            MessagesTreeView.Focus();
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessagesTreeView.ExpandAll();
        }

        private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessagesTreeView.CollapseAll();
        }

        private void deleteNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedNode();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelp();
        }

        #endregion

        #region Methods

        private void LoadFilesToTree(string[] fileNames)
        {
            CheckTreeMessages();
            foreach (string msgfile in fileNames)
            {
                using (FileStream messageStream = File.Open(msgfile, FileMode.Open, FileAccess.Read))
                {
                    using (MsgReader.Outlook.Storage.Message message =
                        new MsgReader.Outlook.Storage.Message(messageStream, FileAccess.Read))
                    {
                        this.LoadMsgToTree(message, this.MessagesTreeView.Nodes.Add("root"));
                    }
                }
            }
        }

        private void CheckTreeMessages()
        {
            if (MessagesTreeView.Nodes.Count > 0)
            {
                bool deleteNodes = false;

                if (ConfigurationHelper.TreeNodeOverride == TreeNodeOverrideEnum.AskEveryTime)
                {
                    this.TopMost = false;
                    deleteNodes = MessageBox.Show("Do you want to clear tree messages first?", "Adding new messages confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes;
                    this.TopMost = ConfigurationHelper.AlwaysOnTop;
                }
                else if (ConfigurationHelper.TreeNodeOverride == TreeNodeOverrideEnum.Overwrite)
                {
                    deleteNodes = true;
                }

                if (deleteNodes)
                {
                    MessagesTreeView.Nodes.Clear();
                }
            }
        }

        private void LoadMsgToTree(MsgReader.Outlook.Storage.Message message, TreeNode rootNode)
        {
            rootNode.Text = message.Subject;
            rootNode.Nodes.Add("Subject: " + message.Subject);
            TreeNode bodyNode = rootNode.Nodes.Add("Body: " + GetShortMessage(message.BodyText));
            bodyNode.Tag = new string[] { message.BodyText, message.BodyRtf };
            rootNode.Tag = message.BodyText;

            TreeNode dateNode = rootNode.Nodes.Add("Received On: " + message.ReceivedOn);
            dateNode.Tag = message.ReceivedOn;
            dateNode.Name = "ReceivedOn";

            if (!string.IsNullOrEmpty(message.Sender.DisplayName) || !string.IsNullOrEmpty(message.Sender.Email))
            {
                string fromText = message.Sender.DisplayName + "<" + message.Sender.Email + ">";
                TreeNode fromNode = rootNode.Nodes.Add("From: " + fromText);
                fromNode.Tag = fromText;
                fromNode.Name = "fromNode";
            }

            if (message.Recipients.Count > 1)
            {
                TreeNode recipientNode = rootNode.Nodes.Add("Recipients: " + message.Recipients.Count);
                foreach (MsgReader.Outlook.Storage.Recipient recipient in message.Recipients)
                {
                    recipientNode.Nodes.Add(recipient.Type + ": " + recipient.Email);
                }
            }
            else if (message.Recipients.Count > 0)
            {
                TreeNode recipientNode = rootNode.Nodes.Add("Recipient: " + message.Recipients.First().Email);
            }

            if (message.Attachments.Count > 0)
            {
                TreeNode attachmentNode = rootNode.Nodes.Add("Attachments: " + message.Attachments.Count);
                foreach (MsgReader.Outlook.Storage.Attachment.Attachment attachment in message.Attachments)
                {
                    attachmentNode.Nodes.Add(attachment.FileName + ": " + attachment.Data.Length + "b");
                }
            }

            if (MessagesTreeView.Nodes.Count > 0 &&
                !UpButton.Enabled &&
                !DownButton.Enabled)
            {
                ChangeUpDownButtonsEnable(true);
            }
            rootNode.Expand();
        }

        private void ChangeUpDownButtonsEnable(bool enabled)
        {
            UpButton.BackgroundImage = enabled ? Resources.sort_up : Resources.sort_up_disabled;
            UpButton.Enabled = enabled;
            DownButton.BackgroundImage = enabled ? Resources.sort_down : Resources.sort_down_disabled;
            DownButton.Enabled = enabled;
            buttonExportPDF.Enabled = enabled;
        }

        private string GetShortMessage(string messageBody)
        {
            if (!string.IsNullOrEmpty(messageBody) && messageBody.Length > 30)
            {
                return messageBody.Substring(0, 27) + "...";
            }
            return messageBody;
        }

        private void DeleteSelectedNode()
        {
            if (MessagesTreeView.SelectedNode != null)
            {
                if (!ConfigurationHelper.ConfirmDelete
                    ||
                    MessageBox.Show("Are you sure?", "Delete confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    TreeNode parentNode = MessagesTreeView.SelectedNode;
                    while (parentNode.Parent != null)
                    {
                        parentNode = parentNode.Parent;
                    }
                    MessagesTreeView.Nodes.Remove(parentNode);
                    if (MessagesTreeView.Nodes.Count == 0)
                    {
                        ChangeUpDownButtonsEnable(false);
                    }
                }
            }
        }

        private void ShowHelp()
        {
            new Thread(new ThreadStart(ShowHelpThreadSafe)).Start();
        }

        private void ShowHelpThreadSafe()
        {
            using (FormHelp help = new FormHelp())
            {
                help.StartPosition = FormStartPosition.CenterScreen;
                help.ShowDialog();
            }
        }


        #endregion

        private void TreeNodeOverride_click(object sender, EventArgs e)
        {
            ConfigurationHelper.TreeNodeOverride = (TreeNodeOverrideEnum)(short.Parse((sender as ToolStripMenuItem).Tag.ToString()));
            askEveryTimeToolStripMenuItem.Checked = false;
            overwriteMessagesToolStripMenuItem.Checked = false;
            appendMessagesToolStripMenuItem.Checked = false;
            (sender as ToolStripMenuItem).Checked = true;

        }
    }
}
