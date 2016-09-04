# Outlook Message Converter
It is a C#.Net desktop project for converting outlook messages to PDF
You can simply drag and drop your outlook messages into the application and then export it as a PDF file
# Customize output format
For extra flexibility, there is a HTML file in the project named "HtmlTemplate.html", using that file you can customize your output PDF file. There are some dictionary items to use in the HTML file:
- @REPEATAREA@: between two repeating area element you have to put what you need to be repeated for each messagee
- @FROM@: Sender name
- @SUBJECT@: Email subject
- @BODY@: Email body
