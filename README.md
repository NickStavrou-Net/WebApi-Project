# WebApi-Project
Use of ASP.NET MVC WebApi 2.2 

Test it with Postman or any other tool.

In order to access the Api use the AuthorizeApp Model in the database file to get the AppToken and AppSecret and make a post with the body like this:
```javascript
{
	"appToken": "The appToken value from database",
	"appSecret": "The appToken value from database"
}
```
If they have expired change the date in the database.

After getting your Token you can make a call to the api like this:
![Alt text](https://imgur.com/a/G23hueB "Optional")

[JWT.IO](https://jwt.io/) allows you to decode, verify and generate JWT.
