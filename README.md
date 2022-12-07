**Installation Guide**

 1. Clone the solution from: [https://github.com/JoshuaRHolden/BugTracker](https://github.com/JoshuaRHolden/BugTracker)  

> (Please note sometimes the following file, once cloned to local
> machine: **“../BugTrack_UI\.config\**  **dotnet-tools.json**” may be
> blocked, please navigate to this file, right-click, select properties
> and ensure the file does not say blocked, if it does, simply check the
> “unblock” radio button and accept the change.)

 2. Open up a command prompt and change to the directory containing the solution cloned from git.
 3. In the root of the directory run the command: _**“dotnet restore”**_, when that completes subsequently run “dotnet build”
 4. Change directory to the subdirectory: **"BUGTRACK_WEB_API"**
 5. Type: _**“dotnet run”**_ and press enter.  
 6. Open up a _new_ command prompt whilst keeping the original one open, change directory to the project solution again, but this time to the directory: **“/BUGTRACK_UI”**
 7. o create the compact database run the following command: **“dotnet ef database update”**.
 8.  Once the database has finished creating, type _**“dotnet run”**_ and press enter.  

If the above steps were completed with success this will run both the API and the UI in kestrel web server, navigate to: **[https://localhost:7041/](https://localhost:7041/)** to start using the application.

**ARCHITECTURE** 
BugTrack uses WEBAPI with entity framework (hooked up to SQL Compact, but this can easily be changed if required) for the back-end data access.
The API is currently un-authenticated as with this being a BLAZOR server application all API calls are made from the server side, as such in a production environment the API would be tied down with firewall rules and bearer authorisation would be overkill.

For the client side, the application uses BLAZOR server and fluxor to create a single state application store which operates in much the same way as Redux would with react for example.
Authorisation is performed using ASP.net Identity.

**TESTS**
The application has a suite of tests for the WEB API covering both "happy" and "sad" paths, but due to time limitations I omitted any UI tests, because at this point BLAZOR UI unit testing is time intensive to set up using BUnit.

**USAGE**
The application when first visited will bounce you to the login page, a new account will need to be created, in a production environment this would send emails to request confirmation of email account etc but to streamline development and for ease of use this has been disabled.

Once an account has been created you may then log in and use the application.

The application consists of 2 "pages", Users, and Bugs.
On the bug page you can add new bugs, edit bugs and search in real time, there are also filters that can be enabled to show closed bugs and display only ones assigned to yourself.
On the user page, you can reset passwords for existing users and change the users name and other properties as well as being able to delete users from the database.


**Ideal Enhancements**  
  
i: Delete bug (although cancelling allows a bug to be hidden It would probably be useful to be able to fully delete a bug, but this was not included in the specification) 

ii: bug attachments, allow users to upload screenshots to assist with dealing with the bug.

iii:  Bug updates, 1 to many linkage to allow bugs to have updates added.

iiii: Client only view page, restricted to view only bugs personally logged (or add multi tenancy support to allow bugs to be viewed raised by all members of that tenancy and updates only.  

iiiii: Multi tenancy support.

Iiiiii: Email support to email updates to bugs to the entity email who raised the bug.