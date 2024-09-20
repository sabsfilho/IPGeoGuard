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

If you need any help´to get started with AWS Lambda development, I strongly recommend my another all-in-one article that explains in depth how to create a boilerplate for AWS Lambda Serverless Function from Zero to Hero.<br/>
[click here to open this all-in-one guide](https://www.linkedin.com/pulse/publish-net-8-microservice-aws-lambda-function-using-cost-santos-vsiqe)<br/>

I created an AWS API Gateway to invoke the IP GeoGuard Lambda function using its REST API. Using AWS API Gateway provides a secure HTTP endpoint to invoke this Lambda function and it helps manage large volumes of calls by throttling traffic and automatically validating and authorizing API calls. So, it's good practice to implement this connectivity layer.<br/>

In this project, for my experimental purposes, I am also using [Redis Database, in-memory storage](https://redis.io/) to cache Geolocation metadata, so I can significantly reduce IP2Location API requests. Redis is the world's fastest in-memory database and extremely easy to integrate. For the sake of simplicity and seamless integration, I decided to use [AWS MemoryDB](https://aws.amazon.com/memorydb/), Redis OSS-compatible service for ultra-fast performance, but being aware of related infrastructure costs, a trade-off analysis is strongly recommended. I let this feature disabled on this project.<br/>

This project is intended to be published in AWS Lambda Serverless Function and it uses AWS S3 to store the geolocation data. In order to test locally, I recommend to use the local file system storage enabling the parameter USE_LOCAL_FILE_STORAGE_CACHE in IPGeoGuard/IPGeoGuard/src/IPGeoGuard/Function.cs.<br/>

I recently submitted this project to the [IP2Location.io Programming Contest](https://contest.ip2location.com/#ipinfodb-invitation). I found it would be a perfect opportunity to test my abilities, learn new techniques and share my knowledge. #ProgrammingContest #IP2LocationContest<br/>

![SimpleApiSwagger](https://sabsfilho.github.io/dev/assets/img/pcb/IP2LocationContest.jpg)

I also created a minimal Web API project with ASP.NET Core. I named it SimpleApi and it is used solely for demonstration. I designed these functions:
- GetCurrentTime => first check if the requested IP address can access this service using the IPGeoGuard. If it is allowed, then print the server current time. Otherwise, returns the region restriction warning message.<br/>
- GetMapViews => print the stored requested IPs aggregated  by Country and City.<br/>
- PutRestriction => set Country restriction for GetCurrentTime function.<br/>
- DeleteRestriction => remove Country restriction for GetCurrentTime function.<br/>

![SimpleApiSwagger](https://sabsfilho.github.io/dev/assets/img/pcb/SimpleApiSwagger.jpg)

<br/><br/>
Before publishing to AWS cloud, we can test the IPGeoGuard project by running the Lambda Function using the AWS .NET 8 Mock Lambda Test Tool by going to the Lambda Function project directory.<br/>
**cd IPGeoGuard/src/IPGeoGuard**<br/>
build project:<br/>
**dotnet build**<br/>
And then typing this command:<br/>
**dotnet lambda-test-tool-8.0**<br/>
use this json request payload test in Function Input:<br/>
**{"ActionType":1,"ServiceName":"ServiceName","IP":"15.228.198.239"}**<br/>
should get this response after function execution:<br/>
**{"Allowed":true,"Country":"BR","City":"Sao Paulo"}**

<br/><br/>

**document under construction**
