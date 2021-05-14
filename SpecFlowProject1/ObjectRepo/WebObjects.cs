using System;
using System.Collections.Generic;
using System.Text;

namespace SpecFlowProject1.ObjectRepo
{
    public class WebObjects
    {
        public static string giihubSignin = "//input[@name='commit']";
        public static string githubUsername = "//*[@id='login_field']";
        public static string githbPwd = "//*[@id='password']"; 
        public static string pullRequstLink = "//span[text()='Pull requests']";
        public static string newPullRequestButton = "//span[text()='New pull request']";
        public static string sourceSelect = "//*[@id='repo-content-pjax-container']/div[2]/div[2]/details[2]/summary";
        public static string createPRButton = "//*[@id='repo-content-pjax-container']/div[4]/div/button";
        public static string targetSelect = "//*[@id='repo-content-pjax-container']/div[3]/div[1]/details[2]/summary";
        public static string targetBranch = "(//div[contains(@id,'commitish-filter-field-branches-')]//a//span[text()='{0}'])";
        public static string sourceBranch = "(//div[contains(@id,'commitish-filter-field-branches-')]//a//span[text()='{0}'])";
        public static string changedFilesButton = "//button[contains(text(),'changed file')]";
        public static string openPullRequests = "//*[@id='repo-content-pjax-container']/div/div[3]/div/a[1]";
        public static string ViewPullRequests = "//*[@id='repo-content-pjax-container']/div[4]/ul/li/a";
        public static string msgEmptyRepo = "//*[@id='repo-content-pjax-container']/div/h3";
        public static string msgSameBranch = "//*[@id='repo-content-pjax-container']/div[4]/p";
        public static string msgIdenticialBranch = "//*[@id='repo-content-pjax-container']/div[4]/p";
        public static string msgSwitchBase = "//*[@id='repo-content-pjax-container']/div[4]/p";
    }
}
