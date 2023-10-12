**How to start the solution?**
----------------
On this project I had used .Net 8 (pre-release) please check your version before run.
Go to Command Prompt 
```
dotnet --version
```
your version should be over 8.0.xxx
if your version is lower than 8. please install the pre-release version [DotNet8-Pre-Release](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

Please certify that you have installed mongoDb in your local environment
If you do not have please install from this website: https://www.mongodb.com/docs/manual/installation/
after installation go to Cytidel.API/appsettings.json
replace the connection string on the section mongo.

mongodb://YOUR_USERNAME:YOUR_PASSWORD@localhost:27017

if you have not enabled the authentication on mongo you can use the connection string bellow

mongodb://localhost:27017

any issues to connect on your local environment please contact me so I can provide a live connection string.
You can start the service [https] to be able to connect the frontend UI

You can also collect the logs that going to be available on the folder "logs" on the root folder of [Cytidel.API](https://github.com/ThiagoSantos-dev/Cytidel.API/tree/master/scr/Cytidel.API)

**What HTTP requests can be sent to the API?**
----------------

You can find the list of all HTTP requests in [Cytidel.API.http](https://github.com/ThiagoSantos-dev/Cytidel.API/tree/master/scr/Cytidel.API/Cytidel.API.http) file placed in the root folder of [Cytidel.API](https://github.com/ThiagoSantos-dev/Cytidel.API/tree/master/scr/Cytidel.API) repository. 
This file is compatible with [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) plugin for [Visual Studio Code](https://code.visualstudio.com). 


**What Test can be executed?**
----------------

On the root folder you going to find Five folder on the folter [Cytidel.Tests.Unit](https://github.com/ThiagoSantos-dev/Cytidel.API/tree/master/scr/Cytidel.Tests.Unit) there are tests on the Core Entities and on the Application Level testing the Sign-In Sign-Up Service.

To execute you should open the Test Explorer on the Visual Studio 2022 or inside of the test files [Core Tests](https://github.com/ThiagoSantos-dev/Cytidel.API/tree/master/scr/Cytidel.Tests.Unit/Core/Entities) or [Application Tests](https://github.com/ThiagoSantos-dev/Cytidel.API/tree/master/scr/Cytidel.Tests.Unit/Application/Services).
just press above the class to RUN each or all tests.