**How to start the solution?**
----------------

Please certify that you have installed mongoDb in your local environment
If you do not have please install from this website: https://www.mongodb.com/docs/manual/installation/
after installation go to Cytidel.API/appsettings.json
replace the connection string on the section mongo.

mongodb://YOUR_USERNAME:YOUR_PASSWORD@localhost:27017

if you have not enabled the authentication on mongo you can use the connection string bellow

mongodb://localhost:27017

any issues to connect on your local environment please contact me so I can provide a live connection string.
You can start the service [https] to be able to connect the frontend UI

You can also collect the logs that going to be available on the folder "logs" on the root folder of [Cytidel.API](https://github.com/ThiagoSantos-dev/Cytidel.API/src/Cytidel.API)

**What HTTP requests can be sent to the API?**
----------------

You can find the list of all HTTP requests in [Cytidel.API.http](https://github.com/ThiagoSantos-dev/Cytidel.API/src/Cytidel.API/Cytidel.API.http) file placed in the root folder of [Cytidel.API](https://github.com/ThiagoSantos-dev/Cytidel.API) repository. 
This file is compatible with [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) plugin for [Visual Studio Code](https://code.visualstudio.com). 


