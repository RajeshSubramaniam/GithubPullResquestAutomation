using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpecFlowProject1.ObjectRepo;
using SpecFlowProject1.Utility;
using System;
using TechTalk.SpecFlow;

namespace SpecFlowProject1.Steps
{
    [Binding]
    class UIAutomationStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        Utilities _utility = new Utilities();

        public UIAutomationStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I navigate to (.*) address")]
        public void GivenINavigateToHttpsGithub_ComAddress(string uri)
        {
            _utility.LaunchWebDriver(uri);
        }

        [Given(@"I navigate to (.*) in github")]
        public void GivenINavigateToQA_CC_V_OperaHouseInGithub(string uri)
        {
            _utility.Navigate(uri);
        }

        [Given(@"I login when prompted")]
        public void GivenILoginWhenPrompted()
        {
            string githubuname = WebObjects.githubUsername;
            string githubpwd = WebObjects.githbPwd;
            string githubsignin = WebObjects.giihubSignin;
            _utility.login(githubuname, githubpwd, githubsignin);
        }

        [When(@"I attempt to create a Pull Request")]
        public void WhenIAttemptToCreateAPullRequest()
        {
            string prTab = WebObjects.pullRequstLink;
            string newPRButton = WebObjects.newPullRequestButton;
            _utility.Click(prTab);
            _utility.Click(newPRButton);
        }

        [When(@"Choose the (.*) and the (.*) branches")]
        public void WhenChooseTheDevelopAndTheMaster(string source, string target)
        {
            string sourceSelect = WebObjects.sourceSelect;
            string sourceBranch = WebObjects.sourceBranch;
            string targetSelect = WebObjects.targetSelect;
            string targetBranch = WebObjects.targetBranch;

            _utility.Click(sourceSelect);
            _utility.ClickUsingElementName(sourceBranch, source);
            _utility.Click(targetSelect);
            _utility.ClickUsingElementName(targetBranch, target);
            _utility.WaitForElement();
        }

        [When(@"I confirm that the commits are differnt in soure and the target")]
        public void WhenIConfirmThatTheCommitsAreDifferntInSoureAndTheTarget()
        {
            string changedFilesLocator = WebObjects.changedFilesButton;
            string  changedFiles =_utility.GetElementText(changedFilesLocator);
            changedFiles = changedFiles.TrimStart().TrimEnd();
            Assert.IsTrue(changedFiles == "0 changed files", "There are no changed files, Change the test data or this test will fail");
        }

        [Then(@"I confirm that there are one ore more open Pull Requests")]
        public void WhenIConfirmThatThereAreNoOpenPullRequests()
        {
            string prTab = WebObjects.pullRequstLink;
            _utility.Click(prTab);
            string existingPRLocator = WebObjects.openPullRequests;
            string existingPR = _utility.GetElementText(existingPRLocator);
            existingPR = existingPR.TrimStart().TrimEnd();
            Assert.IsTrue(existingPR != "0 Open", "There are no open pull requests, So this test has failed. Please change the test data");

        }

        [Then(@"I should be able to create a Pull Request (.*)")]
        public void ThenIShouldBeAbleToCreateAPullRequestSuccessfully(string expected)
        {
            string prButtonLocator = WebObjects.createPRButton;

            if (expected == "Successfully")
            {
                bool enableState = _utility.CheckIfElementIsEnabled(prButtonLocator);
                Assert.IsTrue(enableState == true, "Recheck your Repo, Branch, Commits, Open Pull Requests");
            }
            else
            {
                bool existState = _utility.CheckIfElementExists(prButtonLocator);
                Assert.IsTrue(existState == false, "Ideally Pull Requests can not be raise for same branches, Please check what has gone wrong! ");
            }
        }

        [Then(@"I am only able to View the open Pull Requests rather than creating one")]
        public void ThenIAmOnlyAbleToViewTheOpenPullRequestsRatherThanCreatingOne()
        {

            string viewPrButtonLocator = WebObjects.ViewPullRequests;
            bool enableState = _utility.CheckIfElementIsEnabled(viewPrButtonLocator);
            Assert.IsTrue(enableState == true, "Creating a duplicate pull request is not allowed when a pull request is already open. So Check your TestData.");
            _utility.Click(viewPrButtonLocator);
        }

        [Then(@"I should find '(.*)' message")]
        public void ThenIDo(string msg)
        {
            string locator=""; 
            switch (msg)
            {
                case "This repository is empty.":
                    locator = WebObjects.msgEmptyRepo;
                    break;

                case "master and develop are identical.":
                    locator = WebObjects.msgIdenticialBranch;
                    break;

                case "You’ll need to use two different branch names to get a valid comparison.":
                    locator = WebObjects.msgSameBranch;
                    break;

                case "develop is up to date with all commits from main.":
                    locator = WebObjects.msgSwitchBase;
                    break;

                case "No Error":
                    return;
                    
            }
            string errorMsg = _utility.GetElementText(locator);
            Assert.IsTrue(errorMsg.Contains(msg),"Error/Warning Message is not displayed");
        }

    }
}
