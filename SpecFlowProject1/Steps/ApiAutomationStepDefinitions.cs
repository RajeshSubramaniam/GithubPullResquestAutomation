using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;

namespace SpecFlowProject1.Steps
{
    [Binding]
    class ApiAutomationStepDefinitions
    {
        
        private readonly ScenarioContext _scenarioContext;

        public ApiAutomationStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        
    }
}
