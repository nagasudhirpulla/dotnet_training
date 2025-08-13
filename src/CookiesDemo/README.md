## Create / Update a cookie

```cs
Response.Cookies.Append("dataKey", "dataValStr");
```

## Read a cookie
```cs
var cookieValue = Request.Cookies["dataKey"];
```

## References
* https://www.learnrazorpages.com/razor-pages/tutorial/bakery7/cookies 