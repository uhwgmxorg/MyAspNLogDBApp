using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace MyAspNLogDBApp.Pages
{
    /// <summary>
    /// class NLogTestModel
    /// We need:
    /// Install-Package NLog -Version 5.1.0
    /// Install-Package System.Data.SqlClient -Version 4.8.5
    /// Install-Package NLog.Database -Version 5.1.0  
    /// Install-Package NLog.Web.AspNetCore -Version 5.2.0
    /// </summary>
    public class NLogTestModel : PageModel
    {
        private readonly NLog.Logger _logger;

        public string? MessageText { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        public NLogTestModel()
        {
            _logger = NLog.LogManager.GetCurrentClassLogger();
            NLog.GlobalDiagnosticsContext.Set("Application", "This is an example to store some text or other data in NLog");

            string message = "We are entering the Constructor of NLogTestModel : PageModel";
            _logger.Info(message);
        }

        /// <summary>
        /// OnGet
        /// </summary>
        public void OnGet()
        {
        }

        /******************************/
        /*       Button Events        */
        /******************************/
        #region Button Events

        /// <summary>
        /// OnGetButton_6
        /// </summary>
        public void OnGetButton_6()
        {
            CatchStackOverflow1();

            Debug.WriteLine($"Button_6");
        }
        public void CatchStackOverflow1()
        {
            try
            {
                throw new StackOverflowException();
            }
            catch (StackOverflowException ex)
            {
                // Executes and handles the exception.
                // User code continues
                _logger.Error(ex.ToString());
            }
        }

        /// <summary>
        /// OnGetButton_5
        /// This doesn't really work in C# and .Net see:
        /// https://learn.microsoft.com/en-us/archive/blogs/jaredpar/when-can-you-catch-a-stackoverflowexception
        /// </summary>
        public void OnGetButton_5()
        {
            try
            {
                Foo();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());                
            }

            Debug.WriteLine($"Button_5");
        }

        /// <summary>
        /// OnGetButton_4
        /// </summary>
        public void OnGetButton_4()
        {
            int b = 1, c = 0;

            try
            {
                int a = b / c;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }

            Debug.WriteLine($"Button_4");
        }

        /// <summary>
        /// OnGetButton_3
        /// </summary>
        public void OnGetButton_3()
        {
            Exception myExcetion = new("MyException");

            try
            {
                throw myExcetion;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }

            Debug.WriteLine($"Button_3");
        }

        /// <summary>
        /// OnGetButton_2
        /// </summary>
        public void OnGetButton_2()
        {
            string message = "You pressed Button #2 and create an Error Message Beep()";
            _logger.Error(message);

            Debug.WriteLine($"Button_2");
        }

        /// <summary>
        /// OnGetButton_1
        /// </summary>
        public void OnGetButton_1()
        {
            string message = "You pressed Button #1";
            _logger.Info(message);

            Debug.WriteLine($"Button_1");
        }

        #endregion
        /******************************/
        /*      Other Events          */
        /******************************/
        #region Other Events

        /// <summary>
        /// OnPost
        /// </summary>
        public void OnPost()
        {
            var messageText = Request.Form["MessageText"];
            if (!String.IsNullOrEmpty(messageText))
                Debug.WriteLine($"MessageText is {messageText}");

            string message = messageText;
            _logger.Info(message);
        }

        #endregion
        /******************************/
        /*      Other Functions       */
        /******************************/
        #region Other Functions

        /// <summary>
        /// foo
        /// Just create a SackOverflow
        /// </summary>
        private void Foo()
        {
            Foo();
        }

        #endregion

    }
}
