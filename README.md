Integrating ASP.NET Core and ASP.NET 4.X 
=====================
The goal of this project is sharing the same cookie between two applications using ASP.NET Core 1.1 and ASP.NET MVC 5

##Nuget Packages
- ASP.NET MVC 5
 - [Microsoft.Owin.Security.Interop](https://www.nuget.org/packages/Microsoft.Owin.Security.Interop/)
- ASP.NET Core
 - [Microsoft.AspNetCore.DataProtection.Extensions](https://www.nuget.org/packages/Microsoft.AspNetCore.DataProtection.Extensions)
 
##Disclaimer:
This is a sample application using some beta/pre-release versions (including ASP.NET Core 1.1)
- **NOT** intended to be a definitive solution
- Beware to use in production way

##References
This project is an improvement of the [blowdart/idunno.CookieSharing](https://github.com/blowdart/idunno.CookieSharing) repository.
The improvements is:

- Using ASP.NET Identity
- Using Authorization filters with claims in both applications
- Sharing the Cookie in AntiForgery
- Entities CRUD's for better demonstration of concerns
- Integrated navigation so users do not feel the difference

=====================
The AspNetInterop was developed by [Eduardo Pires](http://eduardopires.net.br) under the [MIT license](LICENSE).
