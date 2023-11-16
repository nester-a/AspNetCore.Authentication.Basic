# AspNetCore.Authentication.Basic

This project contains an implementation of **Basic Authentication Scheme** for ASP.NET Core. See the [RFC-7617](https://www.ietf.org/rfc/rfc7617.txt).

## Add Basic Authentication

To add Basic authentication in .NET Core, we need to modify `Program.cs` file. **If you are using .NET Core version 5 or less, you have to add the modifications in the** `Startup.cs` **file inside the** `ConfigureServices` **method.**

Add the code to configure Basic authentication right above the `builder.Services.AddAuthentication()` line:

```c#
builder.Services.AddAuthentication()
                .AddBasic(BasicDefaults.AuthenticationScheme);
```

## Basic Authentication Configuration

To configure Basic authentication, we need use delegate from overloaded method `AddBasic(string authenticationScheme, Action<BasicOptions> configure)`:

```c#
builder.Services.AddAuthentication()
                .AddBasic(BasicDefaults.AuthenticationScheme, configure => {
                    //some options will be here
                });
```


### User credentials separator
User credentials (user-id and password) constructs by concatenating the user-id, a single colon (':') character, and the password.
If your credentials is separated by another symbol, then it can be configured with the option `CredentialsSeparator`:

```c#
builder.Services.AddAuthentication()
                .AddBasic(BasicDefaults.AuthenticationScheme, configure => {
                    //Default option value is single colon (':')
                    configure.CredentialsSeparator = '~'
                });
```


### Credentials encoding scheme
By default user credentials encoded by `Base64` into a sequence of US-ASCII characters.
If your credentials is by another algorithm or scheme, then it can be configured with the option `EncodedHeaderDecoder`:

```c#
builder.Services.AddAuthentication()
                .AddBasic(BasicDefaults.AuthenticationScheme, configure => {
                    configure.EncodedHeaderDecoder = credentials => DecodeCretentialsToString(credentials);
                });
```
Or you can use `EncodedHeaderAsyncDecoder` for asynchronous decode:
```c#
builder.Services.AddAuthentication()
                .AddBasic(BasicDefaults.AuthenticationScheme, configure => {
                    configure.EncodedHeaderAsyncDecoder = async (credentials, cancellationToken) => await DecodeCretentialsToStringAsync(credentials, cancellationToken);
                });
```
If both the `EncodedHeaderAsyncDecoder` and `EncodedHeaderDecoder` options are implemented, `BasicHandler` will use only `EncodedHeaderAsyncDecoder` to work:
```c#
builder.Services.AddAuthentication()
                .AddBasic(BasicDefaults.AuthenticationScheme, configure => {
                    //This one will be used
                    configure.EncodedHeaderAsyncDecoder = async (credentials, cancellationToken) => await DecodeCretentialsToStringAsync(credentials, cancellationToken);

                    //This one will be ignored
                    configure.EncodedHeaderDecoder = credentials => DecodeCretentialsToString(credentials);
                });
```


### ClaimsPrincipal object creation

