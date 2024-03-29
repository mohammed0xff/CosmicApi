# CosmicApi
![issues-open](https://img.shields.io/github/issues/mohammed0xff/CosmicApi) 
![issues-closed](https://img.shields.io/github/issues-closed/mohammed0xff/CosmicApi) 
![visitors](https://visitor-badge.laobi.icu/badge?page_id=mohammed0xff.cosmicapi) 
[![Licence](https://img.shields.io/greasyfork/l/407466)](./LICENSE)

## What is this project about? 
This project sotres and provides pictures and information about galaxies, planets, stars and pretty much everything in the cosmos.


⠀⠀⠀⠀⠀⠀⠈⠉⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠖⠀⠀⠀⠀⠀<br />
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⣄⠀⠀⠀⠀⠀⠀⠀⠔⠀<br />
⠀⣷⣶⣦⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀<br />
⠀⣿⣿⣿⣿⣷⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠁⠀⠀⠀<br />
⠀⣿⣿⣿⣿⣿⣿⣧⡀⠤⠤⣤⣤⣀⣀⣀⣀⡀⠀⠀⠀⠀⠀⠀⠠⠀⠀⠀⠀⠀<br />
⠀⣿⣿⣿⣿⣿⣿⣿⣇⠀⣦⣤⣤⣄⣈⡉⠉⠛⠛⠷⢶⠄⢠⣴⣦⡀⠀⠀⠀⠀<br />
⠀⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⠀⠉⠉⠛⠛⠷⣦⣀⠀⠀⢻⣿⣿⣿⡀⠀⠀⠀<br />
⠀⣿⣿⣿⣿⣿⣿⣿⡏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣹⡆⠀⠀⠈⠉⠘⡇⠀⠀  <br />
⠀⣿⣿⣿⣿⣿⣿⡟⠁⠀⠀⢀⠀⢀⣀⣠⣤⡴⠾⠋⠁⠀⠀⢀⣠⡾⠃⠀⠀⠀<br />
⠀⣿⣿⣿⣿⣿⣿⠶⠶⠶⠀⠿⠃⠘⠉⠉⠀⠀⢀⣀⣤⣵⠾⠛⠉⠀⠀⠀⠀⠀<br />
⠀⣿⣿⣟⣉⣀⣀⣀⣀⣠⣤⣤⣤⣴⡶⠶⠿⠛⠛⠉⠁⠄⠀⠀⠀⠀⠀⠀⠀⠀<br />
⠀⠛⠛⠋⠉⠉⠉⠉⠉⠉⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠄⠀⠀⠀⠀⠀⠀⠀<br />
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠁⠀⠀⠀⠀⠀⠀⠐⡀⠀⠀⠀⠀⠀⠀<br />
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠄⠀⠀⠀⠀⠀⠀<br />
## Technologies and Libraries used :

- 🌠 **[`.NET 7`](https://dotnet.microsoft.com/download)** 
- 🌠 **[`MediatR`](https://github.com/jbogard/MediatR)**  
- 🌠 **[`Microsoft.AspNetCore.Authentication.JwtBearer`](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer)** - Jwt Authentication and authorization.
- 🌠 **[`AutoMapper`](https://github.com/AutoMapper/AutoMapper)** - object-object mapper.
- 🌠 **[`Swagger & Swagger UI`](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)** - for API documentation.
- 🌠 **[`FluentValidation`](https://github.com/FluentValidation/FluentValidation)** - for request validation.
- 🌠 **[`Serilog.AspNetCore`](https://github.com/serilog/serilog-aspnetcore)** - for diagnostic logging.
- 🌠 **[`Xunit`](https://github.com/xunit/xunit)** - for unit testing.
- 🌠 **[`Moq`](https://github.com/moq/moq4)** - for mocking interfaces and classes.

## Docker
1. Run `docker-compose up -d` in the root directory 
This will start the application as well as sql-server database.
2. Go to `http://localhost:5000/swagger/index.html`
to access swagger.

## Testing
#### An access token is required to access authorized endpoints that modify the data.
 - to obtain one you can log in with : 
  Email : `admin1@gmail.com` Password : `unique-password`

#### An ApiKey is required to consume and retrieve the data.
 - add `X-Api-Key` header key with value ``` 31E197D4-2912-44E6-B210-4077C7A66738 ``` to your request headers.

