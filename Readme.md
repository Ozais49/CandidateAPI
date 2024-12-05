### Ways of Improvements:
	- Add a Cache to store created / modified candidates so that when an upsert is called, it can be directly checked on cache instead of the querying db.
	- Extend the upsert candidate method to accept CV & cover letter and properly handle files.
	- Add a Generic layer on top of Entity Framework to reuse the common operations like Get, Add, SaveChanges etc. This makes it easier when application scope is extended and other operatoins are added.
	- A middleware error logger to properly handle and maintain uniformity of the logged and returned error and error response.
### Assumptions
	- A proper ui is available for the API
	- The Firstname and LastName fields have max allowed input up to 50 characters only.
	- No Partial updated, whole payload is required and whole entity fields is updated.
	- The current project is a like a POC, thus Sqlite is used, which can be later switched to a larger sql database like MS SqlServer or Postgress.
	
	
