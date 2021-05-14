Feature: PullRequestUsingUI

@iuAutomation
Scenario: PullRequest_WithNoChanges
Given I navigate to https://github.com/login address
And I login when prompted
And I navigate to https://github.com/sympli-coding-challenge/QA-CC-V1-HarbourBridge in github	
When I attempt to create a Pull Request
And Choose the develop and the master branches
And I confirm that the commits are differnt in soure and the target
Then I should be able to create a Pull Request Unsuccessfully
And I should find 'master and develop are identical.' message

@iuAutomation
Scenario Outline: PullRequest_BranchSelection
Given I navigate to https://github.com/login address
And I login when prompted
And I navigate to <Repository> in github	
When I attempt to create a Pull Request
And Choose the <SourceBranch> and the <TargetBranch> branches
Then I should be able to create a Pull Request <AsExpected>
And I should find '<ErrorMessage>' message

Examples: 
| Repository                                                 | SourceBranch | TargetBranch | AsExpected     | ErrorMessage                                                             |
| https://github.com/sympli-coding-challenge/QA-CC-V1-Kakadu | develop      | main         | Successfully   | No Error                                                                 |
| https://github.com/sympli-coding-challenge/QA-CC-V1-Kakadu | main         | develop      | Unsuccessfully | develop is up to date with all commits from main.                        |
| https://github.com/sympli-coding-challenge/QA-CC-V1-Kakadu | main         | main         | Unsuccessfully | You’ll need to use two different branch names to get a valid comparison. |

@iuAutomation
Scenario Outline: PullRequest_Duplicate
Given I navigate to https://github.com/login address
And I login when prompted
And I navigate to <Repository> in github
Then I confirm that there are one ore more open Pull Requests 
When I attempt to create a Pull Request
And Choose the <SourceBranch> and the <TargetBranch> branches
Then I am only able to View the open Pull Requests rather than creating one

Examples: 
| Repository                                                       | SourceBranch | TargetBranch | AsExpected     |
| https://github.com/sympli-coding-challenge/QA-CC-V1-OperaHouse   | develop      | master       | Unsuccessfully |
| https://github.com/sympli-coding-challenge/QA-CC-V1-Campbelltown | develop      | master       | Unsuccessfully |


@iuAutomation
Scenario Outline: PullRequest_NoBranch
Given I navigate to https://github.com/login address
And I login when prompted
And I navigate to <Repository> in github	
When I attempt to create a Pull Request
Then I should find 'This repository is empty.' message
Examples: 
| Repository                                                           |
| https://github.com/sympli-coding-challenge/QA-CC-V1-GreatBarrierReef |
| https://github.com/sympli-coding-challenge/QA-CC-V1-Uluru            |                                                              
