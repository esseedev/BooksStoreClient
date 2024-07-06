#### A simple library for an imaginary rest api book store

##### How to use:
- in your web api project in startup class pass `ApiBaseUrl` and `Token`
  ```
  builder.Services.AddBooksStoreClient(options =>
  {
      var config = builder.Configuration;
      options.ApiBaseUrl = config["BooksStoreSettings:ApiBaseUrl"];
      options.Token = config["BooksStoreSettings:Token"];
  });
  ``` 
- use `IBooksStoreFacade` in your controller
