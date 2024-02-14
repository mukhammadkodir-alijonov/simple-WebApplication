# simple-WebApplication
@everyone

**Task #4 (ALL GROUPS) **

Use language and platform **FOR YOU GROUP**: 

* JavaScript or TypeScript, use React (you may use _anything_ to store user data, e.g. implement back on Node.js+Express+MySQL; you may use some SaaS like Firebase - be careful  if you decide to use "out-of-the-box" users, it may be problematic to delete them).

* C#, .NET, some kind ASP.NET, SQL Server (or any database).

Create a working and deployed Web application with registration and authentication.
Non-authenticated users should not have access to the user management (admin panel).
Only authenticated users should have access the user management **table**: id, name, e-mail, last login time, registration time, status (active/blocked).

**The leftmost column** of the table should contains checkboxes without labels for multiple selection (table header contains only checkbox without label that selects or deselects all records).

There must be a **toolbar** over the table with the following actions: Block (red button with text), Unblock (icon), Delete (icon).

You have to use any **CSS framework** (Bootstrap is recommended, but you can choose any CSS framework).

All users should be able to block or delete _themselves_ or any other user.

If user account is blocked or deleted any next user’s request should redirect to the login page.

**User can use any non-empty password (even one character).** If you use 3rd-party service to store users, you may 1) either implement your own "users" there or 2) accept that some requirement cannot be implemented (but you get results faster).

Blocked user should not be able to login, deleted user can re-register.

