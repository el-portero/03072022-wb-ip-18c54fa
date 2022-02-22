Supplylogix Architect Interview Project


INSTRUCTIONS:
For this project, you will need to implement an API that satisfies all the assertions made by the source code in the ArchitectInterviewProject project. Your API needs to include instructions to allow us to run it locally so that the ArchitectInterviewProject application successfully against it using the following argument format:

$ dotnet run --project ArchitectInterviewProject [base-url] [bearer-token]

You will notice that a bearer token is a required argument to the executable. Your API will need to implement bearer token authentication, but it does not have to be dynamic. You can provide us with a "valid" bearer token that we will use when we execute the ArchitectInterviewProject executable against your API.


REQUIREMENTS:
Your solution must meet the following requirements:
* Include instructions for running the project locally.
* Must NOT hard-code any API responses to satisfy the ArchitectInterviewProject driver assertions.


NOTES:
* You can use whatever programming language you would like
* You DO NOT need to host your API on the public internet
* You DO NOT need to persist your data to a database (an in-memory store is fine)
* You DO NOT need to include error handling or unit tests - we will discuss these with you during your on-site project overview
* One API endpoint will need to accept a sample quantity-on-hand file. This is a plain text file that is pipe-delimited and is in the following format:

NPI|NDC|Units|UnitCost

An NPI is a unique identifier that represents a single pharmacy. Every single pharmacy in the US
has its own NPI and that value is unique to that pharmacy.

An NDC identifies a drug. Like NPI, this value is unique to each drug.

Units represents the number of units a given pharmacy has on the shelf at the moment the file was
generated. For this project, units will always be integers.

UnitCost represents the cost per unit for this particular drug.


DELIVERABLE:
Please send a zip file containing the entirety of your solution source code, along with a file that contains the base URL of your API and the bearer token you would like for us to use.


SCORING CRITERIA:
Your project submission will be scored based on the following criteria -
* Does the solution fulfill all the assertions made in the ArchitectInterviewProject project?
* Does the solution meet the requirements listed above?
* Is the source code clean and maintainable?


ADDITIONAL NOTES:
* Please do not spend more than a couple hours on this project. If you are unable to complete it in that amount of time, send what you have and we can discuss the code you have written.

Thank you and good luck! If you have any questions at all about these instructions please email us at:

suhendra.utet@supplylogix.com
burton.caulkins@supplylogix.com
