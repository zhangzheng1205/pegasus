﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class SystemPickListeAddressType : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_Corp")]
        public void systemPickListeAddressType()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");
            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var system_PicklistsHelper = new System_PicklistsHelper(GetWebDriver());

            // Variable
            var AddressType = "Test" + RandomNumber(1, 99);
            var Test = "New" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("SystemPickListAddressType", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("SystemPickListAddressType", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("SystemPickListAddressType", "Redirect To PickList");
                VisitOffice("pick-lists");

                executionLog.Log("SystemPickListAddressType", "Verify Page title");
                VerifyTitle("Picklists");

                executionLog.Log("SystemPickListeAddressType", "Click On Link Address Type");
                system_PicklistsHelper.ClickElement("eAddressType");

                executionLog.Log("SystemPickListeAddressType", "Click On Add New Item");
                system_PicklistsHelper.ClickElement("AddNewItem");

                executionLog.Log("SystemPickListeAddressType", "Add New Item");
                system_PicklistsHelper.TypeText("PicklistItem", AddressType);

                executionLog.Log("SystemPickListeAddressType", "Click on Save ");
                system_PicklistsHelper.ClickElement("Save");

                executionLog.Log("SystemPickListeAddressType", "Click on Cancel");
                system_PicklistsHelper.ClickElement("Cancel");
                system_PicklistsHelper.WaitForWorkAround(4000);

                executionLog.Log("SystemPickListeAddressType", "Verfiy Text");
                system_PicklistsHelper.VerifyPageText(AddressType);

                executionLog.Log("SystemPickListeAddressType", "Click delete Button");
                system_PicklistsHelper.ClickElement("DeletePick");
                system_PicklistsHelper.WaitForWorkAround(3000);

                executionLog.Log("SystemPickListeAddressType", "Click on item to deleted");
                system_PicklistsHelper.DeletePickList(AddressType);

                executionLog.Log("SystemPickListeAddressType", "Selelct replace with item.");
                system_PicklistsHelper.SelectText("ReplacePiclist", "IM");

                executionLog.Log("SystemPickListeAddressType", "Click PickList Save Button");
                system_PicklistsHelper.ClickElement("PickListSaveBtn");

                executionLog.Log("SystemPickListeAddressType", "Accept alert message.");
                system_PicklistsHelper.AcceptAlert();
                system_PicklistsHelper.WaitForWorkAround(4000);

            }
            catch (Exception e)
            {

                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("SystemPickListeAddressType");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("System Pick Liste Address Type");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("System Pick Liste Address Type", "Bug", "Medium", "System pick page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("System Pick Liste Address Type");
                        TakeScreenshot("SystemPickListeAddressType");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SystemPickListeAddressType.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("SystemPickListeAddressType");
                        string id = loginHelper.getIssueID("System Pick Liste Address Type");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SystemPickListeAddressType.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("System Pick Liste Address Type"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("System Pick Liste Address Type");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("SystemPickListeAddressType");
                executionLog.WriteInExcel("System Pick Liste Address Type", Status, JIRA, "Pick List Management");
            }
        }
    }
}