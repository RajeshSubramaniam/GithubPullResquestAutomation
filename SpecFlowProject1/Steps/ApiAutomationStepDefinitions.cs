using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharp;
using SpecFlowProject1.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;

namespace SpecFlowProject1.Steps
{
    [Binding]
    class ApiAutomationStepDefinitions
    {
        
        Utilities _utility = new Utilities();
        private readonly ScenarioContext _scenarioContext;
        string apiURI = string.Empty;
        string apiAuthToken = string.Empty;
        List<string> apiParamList=new List<string>();
        RestResponse apiResponse;
        string responseCode = "";

        public ApiAutomationStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        [Given(@"I setup the API Uri with (.*) and (.*)")]
        public void GivenISetupTheAPIUri(string owner, string repo)
        {
         apiURI = string.Format(  _utility.FetchData("Uri"),owner,repo);
        }

        [Given(@"I setup the Authentication token")]
        public void GivenISetupTheAuthenticationToken()
        {
           apiAuthToken = _utility.FetchData("AuthToken");
        }

        [Then(@"I setup (.*) parameter as (.*)")]
        public void ThenISetupAcceptParameterAsApplicationVnd_Github_VJson(string pname, string pvalue)
        {
            string param = pname +"="+ pvalue;
            apiParamList.Add(param);
        }


        [Then(@"I send API Request to get the pull requests")]
        public void ThenISendAPIRequestToGetThePullRequests()
        {
            string collectiveParam = "";
            foreach (var param in apiParamList)
            {
                collectiveParam += param + "&";
            }
            collectiveParam = collectiveParam.Remove(collectiveParam.Length - 1);
            string updateUri = apiURI + "?"+collectiveParam;
            apiResponse= _utility.GetResponseViaAPICall(updateUri, apiAuthToken);
        }

        [Then(@"I validate (.*) of the response")]
        public void ThenIValidateStatusOfTheResponse(int code)
        {
            Assert.AreEqual((int)apiResponse.StatusCode, code);
        }

        [Then(@"I validate (.*) and (.*) in response data")]
        public void ThenIVerifyTheResponseData(string prID, string prTitle)
        {

            string jsonDataString = apiResponse.Content;
            Regex regBase = new Regex("([A-Za-z0-9\":/.\\-,_ ]+)\"user");
            Match baseMatch = regBase.Match(jsonDataString);
            if (prID == "NULL")
            {

                VerifyNegativeResponse(jsonDataString);
            }
            else
            {
                Regex regTargetID = new Regex("id\\\"\\:\\d+");
                Match targetIDMatch = regTargetID.Match(baseMatch.ToString());
                Assert.IsTrue(targetIDMatch.ToString().Contains(prID));
            }

            if (prTitle == "NULL")
            {
                VerifyNegativeResponse(jsonDataString);
            }
            else
            {
                Regex regTargetTitle = new Regex("title.*\\,");
                Match targetTitleMatch = regTargetTitle.Match(baseMatch.ToString());
                Assert.IsTrue(targetTitleMatch.ToString().Contains(prTitle));
            }

        }

        public void VerifyNegativeResponse(string response)
        {
            string status = apiResponse.StatusCode.ToString();
            switch (status)
            {
                case "OK":
                    Assert.IsTrue(response.Contains("[]"));
                    break;

                case "Not Found":
                    Assert.IsTrue(response == "Not Found");
                    break;
            }
        }
        

    }
}
