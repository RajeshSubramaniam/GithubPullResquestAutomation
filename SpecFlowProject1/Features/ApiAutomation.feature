Feature: PullRequestUsingAPI

@apiAutomation
Scenario Outline: PullRequests_List
Given I setup the API Uri with <Onwer> and <Repository>
And I setup the Authentication token
Then I setup accept parameter as application/vnd.github.v3+json
Then I send API <Method> Request to get the pull requests
Then I validate <StatusCode> of the response
And I validate <PullRequestID> and <PullRequestTitle> in response data

Examples: 
| Method  | Onwer                   | Repository             | StatusCode | PullRequestID | PullRequestTitle |
| GET     | sympli-coding-challenge | QA-CC-V1-Campbelltown  | 200        | 526127243     | Updated Readme   |
| GET     | sympli-coding-challenge | QA-CC-V1-OperaHouse    | 200        | 530127845     | Updated README   |
| GET     | sympli-coding-challenge | QA-CC-V1-HarbourBridge | 200        | NULL          | NULL             |
| GET     | sympli-coding-challenge | Dummy                  | 404        | NULL          | NULL             |
| OPTIONS | sympli-coding-challenge | QA-CC-V1-Uluru         | 204        | NULL          | NULL             |

@apiAutomation

Scenario Outline: PullRequests_ByBaseBranch
Given I setup the API Uri with <Onwer> and <Repository>
And I setup the Authentication token
Then I setup accept parameter as application/vnd.github.v3+json
Then I setup base parameter as <Branch>
Then I send API <Method> Request to get the pull requests
Then I validate <StatusCode> of the response
And I validate <PullRequestID> and <PullRequestTitle> in response data

Examples: 
| Method | Onwer                   | Repository            | Branch  | StatusCode | PullRequestID | PullRequestTitle |
| GET    | sympli-coding-challenge | QA-CC-V1-Campbelltown | master  | 200        | 526127243     | Updated Readme   |
| GET    | sympli-coding-challenge | QA-CC-V1-OperaHouse   | develop | 200        | NULL          | NULL             |

@apiAutomation
Scenario Outline: PullRequests_ByState
Given I setup the API Uri with <Onwer> and <Repository>
And I setup the Authentication token
Then I setup accept parameter as application/vnd.github.v3+json
Then I setup state parameter as <State>
Then I send API <Method> Request to get the pull requests
Then I validate <StatusCode> of the response
And I validate <PullRequestID> and <PullRequestTitle> in response data

Examples: 
| Method | Onwer                   | Repository            | StatusCode | State  | PullRequestID | PullRequestTitle |
| GET    | sympli-coding-challenge | QA-CC-V1-OperaHouse   | 200        | open   | 530127845     | Updated README   |
| GET    | sympli-coding-challenge | QA-CC-V1-Uluru        | 200        | open   | NULL          | NULL             |
| GET    | sympli-coding-challenge | QA-CC-V1-Campbelltown | 200        | closed | NULL          | NULL             |

@apiAutomation
Scenario Outline: PullRequests_ByPage
Given I setup the API Uri with <Onwer> and <Repository>
And I setup the Authentication token
Then I setup accept parameter as application/vnd.github.v3+json
Then I setup page parameter as <Page>
Then I setup state parameter as <State>
Then I setup base parameter as <Branch>
Then I send API <Method> Request to get the pull requests
Then I validate <StatusCode> of the response
And I validate <PullRequestID> and <PullRequestTitle> in response data

Examples: 
| Method | Onwer                   | Repository            | Page | State | Branch | StatusCode | PullRequestID | PullRequestTitle |
| GET    | sympli-coding-challenge | QA-CC-V1-OperaHouse   | 1    | open  | master | 200        | 530127845     | Updated README   |
| GET    | sympli-coding-challenge | QA-CC-V1-Campbelltown | 2    | open  | master | 200        | NULL          | NULL             |


@apiAutomation
Scenario Outline: PullRequests_NegativeMethods
Given I setup the API Uri with <Onwer> and <Repository>
And I setup the Authentication token
Then I setup accept parameter as application/vnd.github.v3+json
Then I send API <Method> Request to get the pull requests
Then I validate <StatusCode> of the response
And I validate <PullRequestID> and <PullRequestTitle> in response data

Examples: 
| Method  | Onwer                   | Repository            | StatusCode | PullRequestID | PullRequestTitle |
| COPY    | sympli-coding-challenge | QA-CC-V1-Campbelltown | 404        | NULL          | NULL             |
| OPTIONS | sympli-coding-challenge | QA-CC-V1-Uluru        | 204        | NULL          | NULL             |
| DELETE  | sympli-coding-challenge | QA-CC-V1-OperaHouse   | 404        | NULL          | NULL             |