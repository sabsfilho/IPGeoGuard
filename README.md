# IPGeoGuard
AWS Lambda Function microservice to control IP requests and web resources using Geolocation by Country and City.<br/>
![IPGeoGuardHighLevelDiagram](https://sabsfilho.github.io/dev/assets/img/pcb/IPGeoGuard.jpg)

This project is a microservice that provides a Geolocation protection layer verifying an incoming request and using virtual geographic perimeters to determine if its IP address is allowed to access a restricted service resource. It is very useful when the ip address is available, but not the coordinate.<br/>

Another great future is to be capable of showing audience locations to see where the biggest audience exists. It is possible to create reports grouping user traffic by country and city.<br/>

These are the main targets of this project:
- Block requests from restricted geographic areas<br/>
- Allow requests from permitted geographic areas<br/>
- Enable/disable features for specific regions<br/>
- Keep and report audience traffic by location (Country, City...)<br/>
- This project is a .NET 8 microservice hosted in AWS Lambda Function and can be easily integrated consuming a file in GeoJSON format.<br/>

The incoming request IP address is translated into a geographic location and verified in the whitelist or blacklist files configured by the system administrator.<br/>

This IPGeoGuard microservice consumes the IP Geolocation API tool to determine a user's location and use the geolocation information.<br/>
[IP2Location API](https://www.ip2location.io/)<br/>

In this project, I am also using [Redis Database, in-memory storage](https://redis.io/) to cache Geolocation metadata, so I can significantly reduce IP2Location API requests. Redis is the world's fastest in-memory database and extremely easy to integrate.<br/>

I recently submitted this project to the [IP2Location.io Programming Contest](https://contest.ip2location.com/#ipinfodb-invitation). I found it would be a perfect opportunity to test my abilities, learn new techniques and share my knowledge.<br/>

I also created a minimal Web API project with ASP.NET Core. I named it SimpleApi and it is used solely for demonstration. I designed these functions:
- GetCurrentTime => first check if the requested IP address can access this service using the IPGeoGuard. If it is allowed, then print the server current time. Otherwise, returns the region restriction warning message.<br/>
- GetMapViews => print the stored requested IPs aggregated  by Country and City.<br/>
- PutRestriction => set Country restriction for GetCurrentTime function.<br/>
- DeleteRestriction => remove Country restriction for GetCurrentTime function.<br/>

![SimpleApiSwagger](https://sabsfilho.github.io/dev/assets/img/pcb/SimpleApiSwagger.jpg)

<br/><br/>

**document under construction**
