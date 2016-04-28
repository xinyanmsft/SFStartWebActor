# About this sample

This is a sample project for creating a typical web application using Service Fabric actors programming model. The application consists of the following pieces:

1. An application gateway service. This project consumes incoming web requests and routes to actors. 

2. A service hosting Service Fabric actors. Typically an application may have one or more such projects, each implementing various functionalities.

3. [Script](https://github.com/xinyanmsft/SFStartWebActor/blob/master/Tools/Setup_AppInsights.ps1) to setup Application Insight and generate the companion 
WAD configuration files.

4. [Script](https://github.com/xinyanmsft/SFStartWebActor/blob/master/Tools/Setup_CI_CD_VSO.ps1) to setup continuous integration and continuous deployment in 
Visual Studio Online.





