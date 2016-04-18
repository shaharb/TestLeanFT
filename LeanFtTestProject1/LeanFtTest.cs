using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HP.LFT.SDK;
using HP.LFT.Verifications;
using HP.LFT.SDK.StdWin;
using System.IO;

namespace LeanFtTestProject1
{
    [TestClass]
    public class LeanFtTest : UnitTestClassBase<LeanFtTest>
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            GlobalSetup(context);
        }

        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod]
        public void TestMethod1()
        {

            // Following is the program flow: 
            // 1. Launch notepad.
            // 2. Open the Format > Font menu item.
            // 3. Select "Arial" from the Font combobox.
            // 4. Use the Assert statement to verify that "Arial" is the selected font in the combobox.

            // Locate the Notepad window and assign it to an IWindow object.
            IWindow notepadWindow = Desktop.Describe<IWindow>(new WindowDescription
            {
                WindowClassRegExp = "Notepad",
                WindowTitleRegExp = " Notepad"
            });

            // Locate the Notepad menu and assign it to an IMenu object.
            IMenu notepadMenu = notepadWindow.Describe<IMenu>(new MenuDescription(MenuType.Menu));

            // Build the path for the Font menu item. (The second item in the Format menu in Notepad)
            var path = notepadMenu.BuildMenuPath("Format", 2);

            // Use the path to retrieve the actual Font menu item object. 
            var menuItem = notepadMenu.GetItem(path);

            //Open the Font dialog using the font menu item. 
            notepadMenu.Select(menuItem);

            // Locate the Font dialog box and assign it to an IDialog object.
            IDialog notepadFontDialog = notepadWindow.Describe<IDialog>(new DialogDescription
            {
                WindowTitleRegExp = "Font"
            });

            // Locate the Font combobox in the Font dialog box and assign it to an IComboBox object. 
            IComboBox fontsComboBox = notepadFontDialog.Describe<IComboBox>(new ComboBoxDescription
            {
                AttachedText = @"&Font:",
                NativeClass = @"ComboBox"
            });

            // Select "Arial" font in the combobox
            fontsComboBox.Select("Arial");

            // Get the selected combobox item
            var selectedFont = fontsComboBox.SelectedItem;

            // Verify the selected combobox item is "Arial"
            Assert.AreEqual("Arial", selectedFont);

            // Locate the Cancel button in the dialog box and assign it to an IButton object.
            IButton cancelButton = notepadFontDialog.Describe<IButton>(new ButtonDescription
            {
                Text = @"Cancel",
                NativeClass = @"Button"
            });

            // Clicks "Cancel" in the dialog box.
            cancelButton.Click();

            IEditor theEditor = notepadWindow.Describe<IEditor>(new EditorDescription
            {
                NativeClass = @"Edit"
            });

            String junk_str = "Hi Meir,\nHow are you?\nThe Download & Installation of LeanFT and VS2013 went without \ntoo much"
                +" trouble....... \nAnd it works nicely too! \nBye (-:";
           
            char[] charsRead = new char[junk_str.Length];            
            StringReader sr = new StringReader(junk_str);
            sr.Read(charsRead, 0, junk_str.Length);
               
            foreach (char c in charsRead)                                     
                theEditor.SendKeys(c.ToString());            

            
            // Build the path for the Exit menu item. (The seventh item in the File menu in Notepad)
            /*
            path = notepadMenu.BuildMenuPath("File", 7);
            menuItem = notepadMenu.GetItem(path);

            // Exits and closes Notepad. 
            notepadMenu.Select(menuItem); 
            */
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            GlobalTearDown();
        }
    }
}
