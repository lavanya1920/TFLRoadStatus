#### TFLRoadStatus Solution
This has been developed using VS 2019, .net core 3.1

TFLRoadStatus.Client :-- This is console application developed in .net core 3.1 with appsettings.json where you can update AppId and AppKey.

TFLRoadStatus.API :-- Class libary to communicate with TFL Road API. Included appsettings.test.json ,you can update AppId and AppKey here as well.

TFLRoadStatus.Tests :-- Test Project developed using Moq and NUnit.


#### HOW TO BUILD AND RUN THE CODE:
Follow these steps to clone this repo and build it.
- Edit appsettings.json with your TFL API App ID and App Key in TFLRoadStatus.Client and appsettings.test.json in TFLRoadStatus.Tests project. But it will work without these Key and Id
- Open TFLRoadStatus.Client project root directory
- Copy the full path 
- Open command prompt(search cmd from Start) and type command to change directory--> cd TFLRoadStatus.Client(the project path)
- Then to build with command --> dotnet publish -r win-x86 -c Release 
- Now change directory to exe path --> cd .\bin\Release\netcoreapp3.1\win-x86\publish (the path of TFLRoadStatus.Client.exe)
- Run the executable as per below sample
   TFLRoadStatus.Client.exe A2
   TFLRoadStatus.Client.exe A233

#### HOW TO RUN THE TESTS:
  Below steps can be followed to run tests in command prompt
  - Copy Solution root directory 
  - Type command --> cd solutionpath 
  - dotnet test .\TFLRoadStatus.Tests\TFLRoadStatus.Tests.csproj

 Alternatively, In Visual studio you can test. Go to View on top menu bar and select Test Explorer and click on Run which with run all the available tests.

#### ASSUMPTIONS/NOTES
The above assumes that the app is being built on a Windows x86 box.

Though registered in https://api-portal.tfl.gov.uk/, Couldn't get AppId and AppKey. But tested without these keys and it worked.

Before going live, still need to have logging and error handling yet to be implented.
