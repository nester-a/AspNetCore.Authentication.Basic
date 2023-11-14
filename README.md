# AspNetCore.Authentication.Basic

This project contains an implementation of **Basic Authentication Scheme** for ASP.NET Core. See the [RFC-7617](https://www.ietf.org/rfc/rfc7617.txt).

### Add Basic Authentication

To configure Basic authentication in .NET Core, we need to modify `Program.cs` file. **If you are using .NET Core version 5 or less, you have to add the modifications in the** `Startup.cs` **file inside the** `ConfigureServices` **method.**

Add the code to configure Basic authentication right above the `builder.Services.AddAuthentication()` line:

```
builder.Services.AddAuthentication()
                .AddBasic(BasicDefaults.AuthenticationScheme);
```

### Basic Authentication Configuration
